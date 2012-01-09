// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\Recursive\Recursive.grg" on Sun Jan 08 18:57:48 CET 2012

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_Recursive
{
	public class Pattern_ChainFromTo : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromTo instance = null;
		public static Pattern_ChainFromTo Instance { get { if (instance==null) { instance = new Pattern_ChainFromTo(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ChainFromTo_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ChainFromTo_node_to_AllowedTypes = null;
		public static bool[] ChainFromTo_node_from_IsAllowedType = null;
		public static bool[] ChainFromTo_node_to_IsAllowedType = null;
		public enum ChainFromTo_NodeNums { @from, @to, };
		public enum ChainFromTo_EdgeNums { };
		public enum ChainFromTo_VariableNums { };
		public enum ChainFromTo_SubNums { };
		public enum ChainFromTo_AltNums { @alt_0, };
		public enum ChainFromTo_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ChainFromTo;

		public enum ChainFromTo_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ChainFromTo_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromTo_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_base_VariableNums { };
		public enum ChainFromTo_alt_0_base_SubNums { };
		public enum ChainFromTo_alt_0_base_AltNums { };
		public enum ChainFromTo_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromTo_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromTo_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_rec_VariableNums { };
		public enum ChainFromTo_alt_0_rec_SubNums { @_sub0, };
		public enum ChainFromTo_alt_0_rec_AltNums { };
		public enum ChainFromTo_alt_0_rec_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromTo_alt_0_rec;


		private Pattern_ChainFromTo()
		{
			name = "ChainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromTo_node_from", "ChainFromTo_node_to", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromTo_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromTo_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ChainFromTo_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromTo_node_from", "from", ChainFromTo_node_from_AllowedTypes, ChainFromTo_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ChainFromTo_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromTo_node_to", "to", ChainFromTo_node_to_AllowedTypes, ChainFromTo_node_to_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			bool[,] ChainFromTo_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromTo_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromTo_alt_0_base_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromTo_alt_0_base_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ChainFromTo_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromTo_alt_0_base_edge__edge0", "_edge0", ChainFromTo_alt_0_base_edge__edge0_AllowedTypes, ChainFromTo_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ChainFromTo_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromTo_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromTo_alt_0_base_edge__edge0 }, 
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
				ChainFromTo_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromTo_alt_0_base_isEdgeHomomorphicGlobal,
				ChainFromTo_alt_0_base_isNodeTotallyHomomorphic,
				ChainFromTo_alt_0_base_isEdgeTotallyHomomorphic
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
			bool[] ChainFromTo_alt_0_rec_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ChainFromTo_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromTo_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromTo_alt_0_rec_node_intermediate", "intermediate", ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes, ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromTo_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromTo_alt_0_rec_edge__edge0", "_edge0", ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes, ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromTo_alt_0_rec__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromTo.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ChainFromTo_alt_0_rec_node_intermediate"),
					new GRGEN_EXPR.GraphEntityExpression("ChainFromTo_node_to"),
				}, 
				new string[] { }, new string[] { "ChainFromTo_alt_0_rec_node_intermediate", "ChainFromTo_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ChainFromTo_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromTo_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromTo_node_from, ChainFromTo_alt_0_rec_node_intermediate, ChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromTo_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromTo_alt_0_rec__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromTo_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal,
				ChainFromTo_alt_0_rec_isNodeTotallyHomomorphic,
				ChainFromTo_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ChainFromTo_alt_0_rec.edgeToSourceNode.Add(ChainFromTo_alt_0_rec_edge__edge0, ChainFromTo_node_from);
			ChainFromTo_alt_0_rec.edgeToTargetNode.Add(ChainFromTo_alt_0_rec_edge__edge0, ChainFromTo_alt_0_rec_node_intermediate);

			GRGEN_LGSP.Alternative ChainFromTo_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromTo_", new GRGEN_LGSP.PatternGraph[] { ChainFromTo_alt_0_base, ChainFromTo_alt_0_rec } );

			pat_ChainFromTo = new GRGEN_LGSP.PatternGraph(
				"ChainFromTo",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromTo_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ChainFromTo_isNodeHomomorphicGlobal,
				ChainFromTo_isEdgeHomomorphicGlobal,
				ChainFromTo_isNodeTotallyHomomorphic,
				ChainFromTo_isEdgeTotallyHomomorphic
			);
			ChainFromTo_alt_0_base.embeddingGraph = pat_ChainFromTo;
			ChainFromTo_alt_0_rec.embeddingGraph = pat_ChainFromTo;

			ChainFromTo_node_from.pointOfDefinition = null;
			ChainFromTo_node_to.pointOfDefinition = null;
			ChainFromTo_alt_0_base_edge__edge0.pointOfDefinition = ChainFromTo_alt_0_base;
			ChainFromTo_alt_0_rec_node_intermediate.pointOfDefinition = ChainFromTo_alt_0_rec;
			ChainFromTo_alt_0_rec_edge__edge0.pointOfDefinition = ChainFromTo_alt_0_rec;
			ChainFromTo_alt_0_rec__sub0.PointOfDefinition = ChainFromTo_alt_0_rec;

			patternGraph = pat_ChainFromTo;
		}


		public void ChainFromTo_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ChainFromTo_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromTo_addedEdgeNames );
		}
		private static string[] create_ChainFromTo_addedNodeNames = new string[] {  };
		private static string[] create_ChainFromTo_addedEdgeNames = new string[] {  };

		public void ChainFromTo_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromTo curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ChainFromTo_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromTo_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ChainFromTo_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromTo_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromTo_alt_0_base) {
				ChainFromTo_alt_0_base_Delete(procEnv, (Match_ChainFromTo_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromTo_alt_0_rec) {
				ChainFromTo_alt_0_rec_Delete(procEnv, (Match_ChainFromTo_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromTo_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromTo_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
		}

		public void ChainFromTo_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromTo_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromTo.Match_ChainFromTo subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromTo.Instance.ChainFromTo_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_ChainFromTo() {
		}

		public interface IMatch_ChainFromTo : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ChainFromTo_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromTo_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromTo_alt_0_base : IMatch_ChainFromTo_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromTo_alt_0_rec : IMatch_ChainFromTo_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_intermediate { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromTo.Match_ChainFromTo @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ChainFromTo : GRGEN_LGSP.ListElement<Match_ChainFromTo>, IMatch_ChainFromTo
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromTo_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_NodeNums.@from: return _node_from;
				case (int)ChainFromTo_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public enum ChainFromTo_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ChainFromTo_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ChainFromTo_alt_0 _alt_0;
			public enum ChainFromTo_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ChainFromTo_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromTo.instance.pat_ChainFromTo; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromTo_alt_0_base : GRGEN_LGSP.ListElement<Match_ChainFromTo_alt_0_base>, IMatch_ChainFromTo_alt_0_base
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromTo_alt_0_base_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_alt_0_base_NodeNums.@from: return _node_from;
				case (int)ChainFromTo_alt_0_base_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromTo_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromTo.instance.ChainFromTo_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromTo_alt_0_rec : GRGEN_LGSP.ListElement<Match_ChainFromTo_alt_0_rec>, IMatch_ChainFromTo_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromTo_alt_0_rec_NodeNums { @from, @intermediate, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ChainFromTo_alt_0_rec_NodeNums.@intermediate: return _node_intermediate;
				case (int)ChainFromTo_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromTo_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromTo.Match_ChainFromTo @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromTo.Match_ChainFromTo @__sub0;
			public enum ChainFromTo_alt_0_rec_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_alt_0_rec_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromTo_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromTo.instance.ChainFromTo_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFrom : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFrom instance = null;
		public static Pattern_ChainFrom Instance { get { if (instance==null) { instance = new Pattern_ChainFrom(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ChainFrom_node_from_AllowedTypes = null;
		public static bool[] ChainFrom_node_from_IsAllowedType = null;
		public enum ChainFrom_NodeNums { @from, };
		public enum ChainFrom_EdgeNums { };
		public enum ChainFrom_VariableNums { };
		public enum ChainFrom_SubNums { };
		public enum ChainFrom_AltNums { @alt_0, };
		public enum ChainFrom_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ChainFrom;

		public enum ChainFrom_alt_0_CaseNums { @base, @rec, };
		public enum ChainFrom_alt_0_base_NodeNums { };
		public enum ChainFrom_alt_0_base_EdgeNums { };
		public enum ChainFrom_alt_0_base_VariableNums { };
		public enum ChainFrom_alt_0_base_SubNums { };
		public enum ChainFrom_alt_0_base_AltNums { };
		public enum ChainFrom_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFrom_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFrom_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_node_to_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFrom_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFrom_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFrom_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFrom_alt_0_rec_VariableNums { };
		public enum ChainFrom_alt_0_rec_SubNums { @_sub0, };
		public enum ChainFrom_alt_0_rec_AltNums { };
		public enum ChainFrom_alt_0_rec_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFrom_alt_0_rec;


		private Pattern_ChainFrom()
		{
			name = "ChainFrom";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFrom_node_from", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ChainFrom_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFrom_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFrom_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ChainFrom_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ChainFrom_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFrom_node_from", "from", ChainFrom_node_from_AllowedTypes, ChainFrom_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			bool[,] ChainFrom_alt_0_base_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] ChainFrom_alt_0_base_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFrom_alt_0_base_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] ChainFrom_alt_0_base_isEdgeTotallyHomomorphic = new bool[0] ;
			ChainFrom_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFrom_alt_0_",
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
				ChainFrom_alt_0_base_isNodeHomomorphicGlobal,
				ChainFrom_alt_0_base_isEdgeHomomorphicGlobal,
				ChainFrom_alt_0_base_isNodeTotallyHomomorphic,
				ChainFrom_alt_0_base_isEdgeTotallyHomomorphic
			);

			bool[,] ChainFrom_alt_0_rec_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFrom_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFrom_alt_0_rec_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFrom_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFrom_alt_0_rec_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFrom_alt_0_rec_node_to", "to", ChainFrom_alt_0_rec_node_to_AllowedTypes, ChainFrom_alt_0_rec_node_to_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFrom_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFrom_alt_0_rec_edge__edge0", "_edge0", ChainFrom_alt_0_rec_edge__edge0_AllowedTypes, ChainFrom_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ChainFrom_alt_0_rec__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFrom.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ChainFrom_alt_0_rec_node_to"),
				}, 
				new string[] { }, new string[] { "ChainFrom_alt_0_rec_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ChainFrom_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFrom_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFrom_node_from, ChainFrom_alt_0_rec_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFrom_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFrom_alt_0_rec__sub0 }, 
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
				ChainFrom_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFrom_alt_0_rec_isEdgeHomomorphicGlobal,
				ChainFrom_alt_0_rec_isNodeTotallyHomomorphic,
				ChainFrom_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ChainFrom_alt_0_rec.edgeToSourceNode.Add(ChainFrom_alt_0_rec_edge__edge0, ChainFrom_node_from);
			ChainFrom_alt_0_rec.edgeToTargetNode.Add(ChainFrom_alt_0_rec_edge__edge0, ChainFrom_alt_0_rec_node_to);

			GRGEN_LGSP.Alternative ChainFrom_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFrom_", new GRGEN_LGSP.PatternGraph[] { ChainFrom_alt_0_base, ChainFrom_alt_0_rec } );

			pat_ChainFrom = new GRGEN_LGSP.PatternGraph(
				"ChainFrom",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFrom_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFrom_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFrom_isNodeHomomorphicGlobal,
				ChainFrom_isEdgeHomomorphicGlobal,
				ChainFrom_isNodeTotallyHomomorphic,
				ChainFrom_isEdgeTotallyHomomorphic
			);
			ChainFrom_alt_0_base.embeddingGraph = pat_ChainFrom;
			ChainFrom_alt_0_rec.embeddingGraph = pat_ChainFrom;

			ChainFrom_node_from.pointOfDefinition = null;
			ChainFrom_alt_0_rec_node_to.pointOfDefinition = ChainFrom_alt_0_rec;
			ChainFrom_alt_0_rec_edge__edge0.pointOfDefinition = ChainFrom_alt_0_rec;
			ChainFrom_alt_0_rec__sub0.PointOfDefinition = ChainFrom_alt_0_rec;

			patternGraph = pat_ChainFrom;
		}


		public void ChainFrom_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ChainFrom_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFrom_addedEdgeNames );
		}
		private static string[] create_ChainFrom_addedNodeNames = new string[] {  };
		private static string[] create_ChainFrom_addedEdgeNames = new string[] {  };

		public void ChainFrom_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFrom curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ChainFrom_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFrom_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ChainFrom_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFrom_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFrom_alt_0_base) {
				ChainFrom_alt_0_base_Delete(procEnv, (Match_ChainFrom_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFrom_alt_0_rec) {
				ChainFrom_alt_0_rec_Delete(procEnv, (Match_ChainFrom_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFrom_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFrom_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
		}

		public void ChainFrom_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFrom_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFrom.Match_ChainFrom subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFrom.Instance.ChainFrom_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_ChainFrom() {
		}

		public interface IMatch_ChainFrom : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ChainFrom_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFrom_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFrom_alt_0_base : IMatch_ChainFrom_alt_0
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFrom_alt_0_rec : IMatch_ChainFrom_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFrom.Match_ChainFrom @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ChainFrom : GRGEN_LGSP.ListElement<Match_ChainFrom>, IMatch_ChainFrom
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ChainFrom_NodeNums { @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFrom_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public enum ChainFrom_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ChainFrom_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ChainFrom_alt_0 _alt_0;
			public enum ChainFrom_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ChainFrom_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ChainFrom_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFrom.instance.pat_ChainFrom; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFrom_alt_0_base : GRGEN_LGSP.ListElement<Match_ChainFrom_alt_0_base>, IMatch_ChainFrom_alt_0_base
		{
			public enum ChainFrom_alt_0_base_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_base_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFrom.instance.ChainFrom_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFrom_alt_0_rec : GRGEN_LGSP.ListElement<Match_ChainFrom_alt_0_rec>, IMatch_ChainFrom_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFrom_alt_0_rec_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFrom_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ChainFrom_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFrom_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFrom_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFrom.Match_ChainFrom @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFrom.Match_ChainFrom @__sub0;
			public enum ChainFrom_alt_0_rec_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFrom_alt_0_rec_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFrom_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFrom.instance.ChainFrom_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromComplete : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromComplete instance = null;
		public static Pattern_ChainFromComplete Instance { get { if (instance==null) { instance = new Pattern_ChainFromComplete(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ChainFromComplete_node_from_AllowedTypes = null;
		public static bool[] ChainFromComplete_node_from_IsAllowedType = null;
		public enum ChainFromComplete_NodeNums { @from, };
		public enum ChainFromComplete_EdgeNums { };
		public enum ChainFromComplete_VariableNums { };
		public enum ChainFromComplete_SubNums { };
		public enum ChainFromComplete_AltNums { @alt_0, };
		public enum ChainFromComplete_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ChainFromComplete;

		public enum ChainFromComplete_alt_0_CaseNums { @base, @rec, };
		public enum ChainFromComplete_alt_0_base_NodeNums { @from, };
		public enum ChainFromComplete_alt_0_base_EdgeNums { };
		public enum ChainFromComplete_alt_0_base_VariableNums { };
		public enum ChainFromComplete_alt_0_base_SubNums { };
		public enum ChainFromComplete_alt_0_base_AltNums { };
		public enum ChainFromComplete_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromComplete_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_base_neg_0_NodeNums { @from, @_node0, };
		public enum ChainFromComplete_alt_0_base_neg_0_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_base_neg_0_VariableNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_SubNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_AltNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph ChainFromComplete_alt_0_base_neg_0;

		public static GRGEN_LIBGR.NodeType[] ChainFromComplete_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_node_to_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFromComplete_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_rec_VariableNums { };
		public enum ChainFromComplete_alt_0_rec_SubNums { @_sub0, };
		public enum ChainFromComplete_alt_0_rec_AltNums { };
		public enum ChainFromComplete_alt_0_rec_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromComplete_alt_0_rec;


		private Pattern_ChainFromComplete()
		{
			name = "ChainFromComplete";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromComplete_node_from", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ChainFromComplete_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromComplete_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromComplete_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ChainFromComplete_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ChainFromComplete_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromComplete_node_from", "from", ChainFromComplete_node_from_AllowedTypes, ChainFromComplete_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			bool[,] ChainFromComplete_alt_0_base_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromComplete_alt_0_base_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromComplete_alt_0_base_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ChainFromComplete_alt_0_base_isEdgeTotallyHomomorphic = new bool[0] ;
			bool[,] ChainFromComplete_alt_0_base_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromComplete_alt_0_base_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromComplete_alt_0_base_neg_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromComplete_alt_0_base_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromComplete_alt_0_base_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromComplete_alt_0_base_neg_0_node__node0", "_node0", ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromComplete_alt_0_base_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromComplete_alt_0_base_neg_0_edge__edge0", "_edge0", ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ChainFromComplete_alt_0_base_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ChainFromComplete_alt_0_base_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_base_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromComplete_alt_0_base_neg_0_edge__edge0 }, 
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
				ChainFromComplete_alt_0_base_neg_0_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_base_neg_0_isEdgeHomomorphicGlobal,
				ChainFromComplete_alt_0_base_neg_0_isNodeTotallyHomomorphic,
				ChainFromComplete_alt_0_base_neg_0_isEdgeTotallyHomomorphic
			);
			ChainFromComplete_alt_0_base_neg_0.edgeToSourceNode.Add(ChainFromComplete_alt_0_base_neg_0_edge__edge0, ChainFromComplete_node_from);
			ChainFromComplete_alt_0_base_neg_0.edgeToTargetNode.Add(ChainFromComplete_alt_0_base_neg_0_edge__edge0, ChainFromComplete_alt_0_base_neg_0_node__node0);

			ChainFromComplete_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromComplete_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ChainFromComplete_alt_0_base_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromComplete_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_base_isEdgeHomomorphicGlobal,
				ChainFromComplete_alt_0_base_isNodeTotallyHomomorphic,
				ChainFromComplete_alt_0_base_isEdgeTotallyHomomorphic
			);
			ChainFromComplete_alt_0_base_neg_0.embeddingGraph = ChainFromComplete_alt_0_base;

			bool[,] ChainFromComplete_alt_0_rec_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromComplete_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromComplete_alt_0_rec_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromComplete_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromComplete_alt_0_rec_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromComplete_alt_0_rec_node_to", "to", ChainFromComplete_alt_0_rec_node_to_AllowedTypes, ChainFromComplete_alt_0_rec_node_to_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromComplete_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromComplete_alt_0_rec_edge__edge0", "_edge0", ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromComplete_alt_0_rec__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromComplete.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ChainFromComplete_alt_0_rec_node_to"),
				}, 
				new string[] { }, new string[] { "ChainFromComplete_alt_0_rec_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ChainFromComplete_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromComplete_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_rec_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromComplete_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromComplete_alt_0_rec__sub0 }, 
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
				ChainFromComplete_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_rec_isEdgeHomomorphicGlobal,
				ChainFromComplete_alt_0_rec_isNodeTotallyHomomorphic,
				ChainFromComplete_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ChainFromComplete_alt_0_rec.edgeToSourceNode.Add(ChainFromComplete_alt_0_rec_edge__edge0, ChainFromComplete_node_from);
			ChainFromComplete_alt_0_rec.edgeToTargetNode.Add(ChainFromComplete_alt_0_rec_edge__edge0, ChainFromComplete_alt_0_rec_node_to);

			GRGEN_LGSP.Alternative ChainFromComplete_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromComplete_", new GRGEN_LGSP.PatternGraph[] { ChainFromComplete_alt_0_base, ChainFromComplete_alt_0_rec } );

			pat_ChainFromComplete = new GRGEN_LGSP.PatternGraph(
				"ChainFromComplete",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromComplete_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromComplete_isNodeHomomorphicGlobal,
				ChainFromComplete_isEdgeHomomorphicGlobal,
				ChainFromComplete_isNodeTotallyHomomorphic,
				ChainFromComplete_isEdgeTotallyHomomorphic
			);
			ChainFromComplete_alt_0_base.embeddingGraph = pat_ChainFromComplete;
			ChainFromComplete_alt_0_rec.embeddingGraph = pat_ChainFromComplete;

			ChainFromComplete_node_from.pointOfDefinition = null;
			ChainFromComplete_alt_0_base_neg_0_node__node0.pointOfDefinition = ChainFromComplete_alt_0_base_neg_0;
			ChainFromComplete_alt_0_base_neg_0_edge__edge0.pointOfDefinition = ChainFromComplete_alt_0_base_neg_0;
			ChainFromComplete_alt_0_rec_node_to.pointOfDefinition = ChainFromComplete_alt_0_rec;
			ChainFromComplete_alt_0_rec_edge__edge0.pointOfDefinition = ChainFromComplete_alt_0_rec;
			ChainFromComplete_alt_0_rec__sub0.PointOfDefinition = ChainFromComplete_alt_0_rec;

			patternGraph = pat_ChainFromComplete;
		}


		public void ChainFromComplete_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ChainFromComplete_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromComplete_addedEdgeNames );
		}
		private static string[] create_ChainFromComplete_addedNodeNames = new string[] {  };
		private static string[] create_ChainFromComplete_addedEdgeNames = new string[] {  };

		public void ChainFromComplete_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromComplete curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ChainFromComplete_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromComplete_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ChainFromComplete_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromComplete_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromComplete_alt_0_base) {
				ChainFromComplete_alt_0_base_Delete(procEnv, (Match_ChainFromComplete_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromComplete_alt_0_rec) {
				ChainFromComplete_alt_0_rec_Delete(procEnv, (Match_ChainFromComplete_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromComplete_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromComplete_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
		}

		public void ChainFromComplete_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromComplete_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromComplete.Match_ChainFromComplete subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromComplete.Instance.ChainFromComplete_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_ChainFromComplete() {
		}

		public interface IMatch_ChainFromComplete : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ChainFromComplete_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromComplete_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromComplete_alt_0_base : IMatch_ChainFromComplete_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromComplete_alt_0_base_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromComplete_alt_0_rec : IMatch_ChainFromComplete_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromComplete.Match_ChainFromComplete @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ChainFromComplete : GRGEN_LGSP.ListElement<Match_ChainFromComplete>, IMatch_ChainFromComplete
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ChainFromComplete_NodeNums { @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public enum ChainFromComplete_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ChainFromComplete_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ChainFromComplete_alt_0 _alt_0;
			public enum ChainFromComplete_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ChainFromComplete_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromComplete.instance.pat_ChainFromComplete; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromComplete_alt_0_base : GRGEN_LGSP.ListElement<Match_ChainFromComplete_alt_0_base>, IMatch_ChainFromComplete_alt_0_base
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ChainFromComplete_alt_0_base_NodeNums { @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_alt_0_base_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromComplete.instance.ChainFromComplete_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromComplete_alt_0_base_neg_0 : GRGEN_LGSP.ListElement<Match_ChainFromComplete_alt_0_base_neg_0>, IMatch_ChainFromComplete_alt_0_base_neg_0
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum ChainFromComplete_alt_0_base_neg_0_NodeNums { @from, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_alt_0_base_neg_0_NodeNums.@from: return _node_from;
				case (int)ChainFromComplete_alt_0_base_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromComplete_alt_0_base_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_alt_0_base_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_base_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromComplete.instance.ChainFromComplete_alt_0_base_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromComplete_alt_0_rec : GRGEN_LGSP.ListElement<Match_ChainFromComplete_alt_0_rec>, IMatch_ChainFromComplete_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromComplete_alt_0_rec_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ChainFromComplete_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromComplete_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @__sub0;
			public enum ChainFromComplete_alt_0_rec_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_alt_0_rec_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromComplete_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromComplete.instance.ChainFromComplete_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromCompleteArbitraryPatternpathLocked : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromCompleteArbitraryPatternpathLocked instance = null;
		public static Pattern_ChainFromCompleteArbitraryPatternpathLocked Instance { get { if (instance==null) { instance = new Pattern_ChainFromCompleteArbitraryPatternpathLocked(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ChainFromCompleteArbitraryPatternpathLocked_node_from_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryPatternpathLocked_node_from_IsAllowedType = null;
		public enum ChainFromCompleteArbitraryPatternpathLocked_NodeNums { @from, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_EdgeNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_VariableNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_SubNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_AltNums { @alt_0, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ChainFromCompleteArbitraryPatternpathLocked;

		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_CaseNums { @base, @rec, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_NodeNums { @from, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_EdgeNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_VariableNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_SubNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_AltNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromCompleteArbitraryPatternpathLocked_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0_IsAllowedType = null;
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_NodeNums { @from, @_node0, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_EdgeNums { @_edge0, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_VariableNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_SubNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_AltNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0;

		public static GRGEN_LIBGR.NodeType[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_VariableNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_SubNums { @_sub0, };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_AltNums { };
		public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec;


		private Pattern_ChainFromCompleteArbitraryPatternpathLocked()
		{
			name = "ChainFromCompleteArbitraryPatternpathLocked";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromCompleteArbitraryPatternpathLocked_node_from", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ChainFromCompleteArbitraryPatternpathLocked_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromCompleteArbitraryPatternpathLocked_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromCompleteArbitraryPatternpathLocked_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ChainFromCompleteArbitraryPatternpathLocked_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ChainFromCompleteArbitraryPatternpathLocked_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromCompleteArbitraryPatternpathLocked_node_from", "from", ChainFromCompleteArbitraryPatternpathLocked_node_from_AllowedTypes, ChainFromCompleteArbitraryPatternpathLocked_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			bool[,] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isEdgeTotallyHomomorphic = new bool[0] ;
			bool[,] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0", "_node0", ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0_AllowedTypes, ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@AEdge, "GRGEN_LIBGR.IEdge", "ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0", "_edge0", ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0_AllowedTypes, ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_",
				true, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryPatternpathLocked_node_from, ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 }, 
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
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0.edgeToSourceNode.Add(ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0, ChainFromCompleteArbitraryPatternpathLocked_node_from);
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0.edgeToTargetNode.Add(ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0, ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0);

			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromCompleteArbitraryPatternpathLocked_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryPatternpathLocked_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0.embeddingGraph = ChainFromCompleteArbitraryPatternpathLocked_alt_0_base;

			bool[,] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to", "to", ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to_AllowedTypes, ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0", "_edge0", ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0_AllowedTypes, ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to"),
				}, 
				new string[] { }, new string[] { "ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromCompleteArbitraryPatternpathLocked_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryPatternpathLocked_node_from, ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec__sub0 }, 
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
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec.edgeToSourceNode.Add(ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0, ChainFromCompleteArbitraryPatternpathLocked_node_from);
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec.edgeToTargetNode.Add(ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0, ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to);

			GRGEN_LGSP.Alternative ChainFromCompleteArbitraryPatternpathLocked_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromCompleteArbitraryPatternpathLocked_", new GRGEN_LGSP.PatternGraph[] { ChainFromCompleteArbitraryPatternpathLocked_alt_0_base, ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec } );

			pat_ChainFromCompleteArbitraryPatternpathLocked = new GRGEN_LGSP.PatternGraph(
				"ChainFromCompleteArbitraryPatternpathLocked",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryPatternpathLocked_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromCompleteArbitraryPatternpathLocked_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromCompleteArbitraryPatternpathLocked_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryPatternpathLocked_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryPatternpathLocked_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base.embeddingGraph = pat_ChainFromCompleteArbitraryPatternpathLocked;
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec.embeddingGraph = pat_ChainFromCompleteArbitraryPatternpathLocked;

			ChainFromCompleteArbitraryPatternpathLocked_node_from.pointOfDefinition = null;
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.pointOfDefinition = ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0;
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.pointOfDefinition = ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0;
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.pointOfDefinition = ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec;
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.pointOfDefinition = ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec;
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec__sub0.PointOfDefinition = ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec;

			patternGraph = pat_ChainFromCompleteArbitraryPatternpathLocked;
		}


		public void ChainFromCompleteArbitraryPatternpathLocked_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ChainFromCompleteArbitraryPatternpathLocked_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromCompleteArbitraryPatternpathLocked_addedEdgeNames );
		}
		private static string[] create_ChainFromCompleteArbitraryPatternpathLocked_addedNodeNames = new string[] {  };
		private static string[] create_ChainFromCompleteArbitraryPatternpathLocked_addedEdgeNames = new string[] {  };

		public void ChainFromCompleteArbitraryPatternpathLocked_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromCompleteArbitraryPatternpathLocked curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromCompleteArbitraryPatternpathLocked_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ChainFromCompleteArbitraryPatternpathLocked_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromCompleteArbitraryPatternpathLocked_alt_0_base) {
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_Delete(procEnv, (Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec) {
				ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_Delete(procEnv, (Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
		}

		public void ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance.ChainFromCompleteArbitraryPatternpathLocked_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_ChainFromCompleteArbitraryPatternpathLocked() {
		}

		public interface IMatch_ChainFromCompleteArbitraryPatternpathLocked : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base : IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec : IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ChainFromCompleteArbitraryPatternpathLocked : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryPatternpathLocked>, IMatch_ChainFromCompleteArbitraryPatternpathLocked
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ChainFromCompleteArbitraryPatternpathLocked_NodeNums { @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0 _alt_0;
			public enum ChainFromCompleteArbitraryPatternpathLocked_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryPatternpathLocked.instance.pat_ChainFromCompleteArbitraryPatternpathLocked; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base>, IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_NodeNums { @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryPatternpathLocked.instance.ChainFromCompleteArbitraryPatternpathLocked_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0 : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0>, IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_NodeNums { @from, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_NodeNums.@from: return _node_from;
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryPatternpathLocked.instance.ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec>, IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked @__sub0;
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryPatternpathLocked.instance.ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards instance = null;
		public static Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards Instance { get { if (instance==null) { instance = new Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from_IsAllowedType = null;
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_NodeNums { @from, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_EdgeNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_VariableNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_SubNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_AltNums { @alt_0, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;

		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_CaseNums { @base, @rec, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_NodeNums { @from, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_EdgeNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_VariableNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_SubNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_AltNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0_IsAllowedType = null;
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_NodeNums { @from, @_node0, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_EdgeNums { @_edge0, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_VariableNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_SubNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_AltNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0;

		public static GRGEN_LIBGR.NodeType[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_VariableNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_SubNums { @_sub0, };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_AltNums { };
		public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_IterNums { };



		public GRGEN_LGSP.PatternGraph ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec;


		private Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards()
		{
			name = "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from", "from", ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from_AllowedTypes, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isEdgeTotallyHomomorphic = new bool[0] ;
			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0", "_node0", ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0_AllowedTypes, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@AEdge, "GRGEN_LIBGR.IEdge", "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0", "_edge0", ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0_AllowedTypes, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 }, 
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
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0.edgeToSourceNode.Add(ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from);
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0.edgeToTargetNode.Add(ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0);

			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0.embeddingGraph = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base;

			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to", "to", ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to_AllowedTypes, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0", "_edge0", ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0_AllowedTypes, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to"),
				}, 
				new string[] { }, new string[] { "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec__sub0 }, 
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
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec.edgeToSourceNode.Add(ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from);
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec.edgeToTargetNode.Add(ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to);

			GRGEN_LGSP.Alternative ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_", new GRGEN_LGSP.PatternGraph[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base, ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec } );

			pat_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards = new GRGEN_LGSP.PatternGraph(
				"ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeHomomorphicGlobal,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeTotallyHomomorphic,
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeTotallyHomomorphic
			);
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base.embeddingGraph = pat_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec.embeddingGraph = pat_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;

			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.pointOfDefinition = null;
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0.pointOfDefinition = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0;
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0.pointOfDefinition = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0;
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.pointOfDefinition = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec;
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.pointOfDefinition = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec;
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec__sub0.PointOfDefinition = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec;

			patternGraph = pat_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;
		}


		public void ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_addedEdgeNames );
		}
		private static string[] create_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_addedNodeNames = new string[] {  };
		private static string[] create_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_addedEdgeNames = new string[] {  };

		public void ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base) {
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_Delete(procEnv, (Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec) {
				ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_Delete(procEnv, (Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
		}

		public void ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards() {
		}

		public interface IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base : IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec : IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards>, IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_NodeNums { @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 _alt_0;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.instance.pat_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base>, IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_NodeNums { @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.instance.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0 : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0>, IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_NodeNums { @from, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_NodeNums.@from: return _node_from;
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.instance.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec : GRGEN_LGSP.ListElement<Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec>, IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards @__sub0;
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.instance.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Blowball : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Blowball instance = null;
		public static Pattern_Blowball Instance { get { if (instance==null) { instance = new Pattern_Blowball(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Blowball_node_head_AllowedTypes = null;
		public static bool[] Blowball_node_head_IsAllowedType = null;
		public enum Blowball_NodeNums { @head, };
		public enum Blowball_EdgeNums { };
		public enum Blowball_VariableNums { };
		public enum Blowball_SubNums { };
		public enum Blowball_AltNums { @alt_0, };
		public enum Blowball_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_Blowball;

		public enum Blowball_alt_0_CaseNums { @end, @further, };
		public enum Blowball_alt_0_end_NodeNums { @head, };
		public enum Blowball_alt_0_end_EdgeNums { };
		public enum Blowball_alt_0_end_VariableNums { };
		public enum Blowball_alt_0_end_SubNums { };
		public enum Blowball_alt_0_end_AltNums { };
		public enum Blowball_alt_0_end_IterNums { };



		public GRGEN_LGSP.PatternGraph Blowball_alt_0_end;

		public static GRGEN_LIBGR.NodeType[] Blowball_alt_0_end_neg_0_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_end_neg_0_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_end_neg_0_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_end_neg_0_VariableNums { };
		public enum Blowball_alt_0_end_neg_0_SubNums { };
		public enum Blowball_alt_0_end_neg_0_AltNums { };
		public enum Blowball_alt_0_end_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph Blowball_alt_0_end_neg_0;

		public static GRGEN_LIBGR.NodeType[] Blowball_alt_0_further_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Blowball_alt_0_further_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_further_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_further_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_further_VariableNums { };
		public enum Blowball_alt_0_further_SubNums { @_sub0, };
		public enum Blowball_alt_0_further_AltNums { };
		public enum Blowball_alt_0_further_IterNums { };



		public GRGEN_LGSP.PatternGraph Blowball_alt_0_further;


		private Pattern_Blowball()
		{
			name = "Blowball";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "Blowball_node_head", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] Blowball_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Blowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] Blowball_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] Blowball_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode Blowball_node_head = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "Blowball_node_head", "head", Blowball_node_head_AllowedTypes, Blowball_node_head_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			bool[,] Blowball_alt_0_end_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Blowball_alt_0_end_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] Blowball_alt_0_end_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] Blowball_alt_0_end_isEdgeTotallyHomomorphic = new bool[0] ;
			bool[,] Blowball_alt_0_end_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Blowball_alt_0_end_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] Blowball_alt_0_end_neg_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] Blowball_alt_0_end_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode Blowball_alt_0_end_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "Blowball_alt_0_end_neg_0_node__node0", "_node0", Blowball_alt_0_end_neg_0_node__node0_AllowedTypes, Blowball_alt_0_end_neg_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge Blowball_alt_0_end_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Blowball_alt_0_end_neg_0_edge__edge0", "_edge0", Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes, Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			Blowball_alt_0_end_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"Blowball_alt_0_end_",
				true, false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head, Blowball_alt_0_end_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { Blowball_alt_0_end_neg_0_edge__edge0 }, 
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
				Blowball_alt_0_end_neg_0_isNodeHomomorphicGlobal,
				Blowball_alt_0_end_neg_0_isEdgeHomomorphicGlobal,
				Blowball_alt_0_end_neg_0_isNodeTotallyHomomorphic,
				Blowball_alt_0_end_neg_0_isEdgeTotallyHomomorphic
			);
			Blowball_alt_0_end_neg_0.edgeToSourceNode.Add(Blowball_alt_0_end_neg_0_edge__edge0, Blowball_node_head);
			Blowball_alt_0_end_neg_0.edgeToTargetNode.Add(Blowball_alt_0_end_neg_0_edge__edge0, Blowball_alt_0_end_neg_0_node__node0);

			Blowball_alt_0_end = new GRGEN_LGSP.PatternGraph(
				"end",
				"Blowball_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Blowball_alt_0_end_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Blowball_alt_0_end_isNodeHomomorphicGlobal,
				Blowball_alt_0_end_isEdgeHomomorphicGlobal,
				Blowball_alt_0_end_isNodeTotallyHomomorphic,
				Blowball_alt_0_end_isEdgeTotallyHomomorphic
			);
			Blowball_alt_0_end_neg_0.embeddingGraph = Blowball_alt_0_end;

			bool[,] Blowball_alt_0_further_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Blowball_alt_0_further_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] Blowball_alt_0_further_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] Blowball_alt_0_further_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode Blowball_alt_0_further_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "Blowball_alt_0_further_node__node0", "_node0", Blowball_alt_0_further_node__node0_AllowedTypes, Blowball_alt_0_further_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge Blowball_alt_0_further_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Blowball_alt_0_further_edge__edge0", "_edge0", Blowball_alt_0_further_edge__edge0_AllowedTypes, Blowball_alt_0_further_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding Blowball_alt_0_further__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Blowball.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Blowball_node_head"),
				}, 
				new string[] { }, new string[] { "Blowball_node_head" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Blowball_alt_0_further = new GRGEN_LGSP.PatternGraph(
				"further",
				"Blowball_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head, Blowball_alt_0_further_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { Blowball_alt_0_further_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Blowball_alt_0_further__sub0 }, 
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
				Blowball_alt_0_further_isNodeHomomorphicGlobal,
				Blowball_alt_0_further_isEdgeHomomorphicGlobal,
				Blowball_alt_0_further_isNodeTotallyHomomorphic,
				Blowball_alt_0_further_isEdgeTotallyHomomorphic
			);
			Blowball_alt_0_further.edgeToSourceNode.Add(Blowball_alt_0_further_edge__edge0, Blowball_node_head);
			Blowball_alt_0_further.edgeToTargetNode.Add(Blowball_alt_0_further_edge__edge0, Blowball_alt_0_further_node__node0);

			GRGEN_LGSP.Alternative Blowball_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "Blowball_", new GRGEN_LGSP.PatternGraph[] { Blowball_alt_0_end, Blowball_alt_0_further } );

			pat_Blowball = new GRGEN_LGSP.PatternGraph(
				"Blowball",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { Blowball_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Blowball_isNodeHomomorphicGlobal,
				Blowball_isEdgeHomomorphicGlobal,
				Blowball_isNodeTotallyHomomorphic,
				Blowball_isEdgeTotallyHomomorphic
			);
			Blowball_alt_0_end.embeddingGraph = pat_Blowball;
			Blowball_alt_0_further.embeddingGraph = pat_Blowball;

			Blowball_node_head.pointOfDefinition = null;
			Blowball_alt_0_end_neg_0_node__node0.pointOfDefinition = Blowball_alt_0_end_neg_0;
			Blowball_alt_0_end_neg_0_edge__edge0.pointOfDefinition = Blowball_alt_0_end_neg_0;
			Blowball_alt_0_further_node__node0.pointOfDefinition = Blowball_alt_0_further;
			Blowball_alt_0_further_edge__edge0.pointOfDefinition = Blowball_alt_0_further;
			Blowball_alt_0_further__sub0.PointOfDefinition = Blowball_alt_0_further;

			patternGraph = pat_Blowball;
		}


		public void Blowball_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_head)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_Blowball_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Blowball_addedEdgeNames );
		}
		private static string[] create_Blowball_addedNodeNames = new string[] {  };
		private static string[] create_Blowball_addedEdgeNames = new string[] {  };

		public void Blowball_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_Blowball curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_Blowball_alt_0 alternative_alt_0 = curMatch._alt_0;
			Blowball_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void Blowball_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_Blowball_alt_0 curMatch)
		{
			if(curMatch.Pattern == Blowball_alt_0_end) {
				Blowball_alt_0_end_Delete(procEnv, (Match_Blowball_alt_0_end)curMatch);
				return;
			}
			else if(curMatch.Pattern == Blowball_alt_0_further) {
				Blowball_alt_0_further_Delete(procEnv, (Match_Blowball_alt_0_further)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void Blowball_alt_0_end_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_Blowball_alt_0_end curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_head = curMatch._node_head;
			graph.RemoveEdges(node_head);
			graph.Remove(node_head);
		}

		public void Blowball_alt_0_further_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_Blowball_alt_0_further curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_head = curMatch._node_head;
			GRGEN_LGSP.LGSPNode node__node0 = curMatch._node__node0;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_Blowball.Match_Blowball subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_head);
			graph.Remove(node_head);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
			Pattern_Blowball.Instance.Blowball_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_Blowball() {
		}

		public interface IMatch_Blowball : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_head { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_Blowball_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Blowball_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Blowball_alt_0_end : IMatch_Blowball_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_head { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Blowball_alt_0_end_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_head { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Blowball_alt_0_further : IMatch_Blowball_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_head { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_Blowball.Match_Blowball @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Blowball : GRGEN_LGSP.ListElement<Match_Blowball>, IMatch_Blowball
		{
			public GRGEN_LIBGR.INode node_head { get { return (GRGEN_LIBGR.INode)_node_head; } }
			public GRGEN_LGSP.LGSPNode _node_head;
			public enum Blowball_NodeNums { @head, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Blowball_NodeNums.@head: return _node_head;
				default: return null;
				}
			}
			
			public enum Blowball_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_Blowball_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_Blowball_alt_0 _alt_0;
			public enum Blowball_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)Blowball_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum Blowball_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Blowball.instance.pat_Blowball; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Blowball_alt_0_end : GRGEN_LGSP.ListElement<Match_Blowball_alt_0_end>, IMatch_Blowball_alt_0_end
		{
			public GRGEN_LIBGR.INode node_head { get { return (GRGEN_LIBGR.INode)_node_head; } }
			public GRGEN_LGSP.LGSPNode _node_head;
			public enum Blowball_alt_0_end_NodeNums { @head, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Blowball_alt_0_end_NodeNums.@head: return _node_head;
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Blowball.instance.Blowball_alt_0_end; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Blowball_alt_0_end_neg_0 : GRGEN_LGSP.ListElement<Match_Blowball_alt_0_end_neg_0>, IMatch_Blowball_alt_0_end_neg_0
		{
			public GRGEN_LIBGR.INode node_head { get { return (GRGEN_LIBGR.INode)_node_head; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_head;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum Blowball_alt_0_end_neg_0_NodeNums { @head, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Blowball_alt_0_end_neg_0_NodeNums.@head: return _node_head;
				case (int)Blowball_alt_0_end_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum Blowball_alt_0_end_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Blowball_alt_0_end_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_end_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Blowball.instance.Blowball_alt_0_end_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Blowball_alt_0_further : GRGEN_LGSP.ListElement<Match_Blowball_alt_0_further>, IMatch_Blowball_alt_0_further
		{
			public GRGEN_LIBGR.INode node_head { get { return (GRGEN_LIBGR.INode)_node_head; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_head;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum Blowball_alt_0_further_NodeNums { @head, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Blowball_alt_0_further_NodeNums.@head: return _node_head;
				case (int)Blowball_alt_0_further_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum Blowball_alt_0_further_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Blowball_alt_0_further_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_further_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Blowball.Match_Blowball @_sub0 { get { return @__sub0; } }
			public @Pattern_Blowball.Match_Blowball @__sub0;
			public enum Blowball_alt_0_further_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Blowball_alt_0_further_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_further_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_further_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Blowball_alt_0_further_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Blowball.instance.Blowball_alt_0_further; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ReverseChainFromTo : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ReverseChainFromTo instance = null;
		public static Pattern_ReverseChainFromTo Instance { get { if (instance==null) { instance = new Pattern_ReverseChainFromTo(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ReverseChainFromTo_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ReverseChainFromTo_node_to_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_node_from_IsAllowedType = null;
		public static bool[] ReverseChainFromTo_node_to_IsAllowedType = null;
		public enum ReverseChainFromTo_NodeNums { @from, @to, };
		public enum ReverseChainFromTo_EdgeNums { };
		public enum ReverseChainFromTo_VariableNums { };
		public enum ReverseChainFromTo_SubNums { };
		public enum ReverseChainFromTo_AltNums { @alt_0, };
		public enum ReverseChainFromTo_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ReverseChainFromTo;

		public enum ReverseChainFromTo_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromTo_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ReverseChainFromTo_alt_0_base_NodeNums { @to, @from, };
		public enum ReverseChainFromTo_alt_0_base_EdgeNums { @_edge0, };
		public enum ReverseChainFromTo_alt_0_base_VariableNums { };
		public enum ReverseChainFromTo_alt_0_base_SubNums { };
		public enum ReverseChainFromTo_alt_0_base_AltNums { };
		public enum ReverseChainFromTo_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph ReverseChainFromTo_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ReverseChainFromTo_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromTo_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ReverseChainFromTo_alt_0_rec_NodeNums { @intermediate, @from, @to, };
		public enum ReverseChainFromTo_alt_0_rec_EdgeNums { @_edge0, };
		public enum ReverseChainFromTo_alt_0_rec_VariableNums { };
		public enum ReverseChainFromTo_alt_0_rec_SubNums { @_sub0, };
		public enum ReverseChainFromTo_alt_0_rec_AltNums { };
		public enum ReverseChainFromTo_alt_0_rec_IterNums { };



		public GRGEN_LGSP.PatternGraph ReverseChainFromTo_alt_0_rec;


		private Pattern_ReverseChainFromTo()
		{
			name = "ReverseChainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ReverseChainFromTo_node_from", "ReverseChainFromTo_node_to", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ReverseChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReverseChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ReverseChainFromTo_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ReverseChainFromTo_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ReverseChainFromTo_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromTo_node_from", "from", ReverseChainFromTo_node_from_AllowedTypes, ReverseChainFromTo_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ReverseChainFromTo_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromTo_node_to", "to", ReverseChainFromTo_node_to_AllowedTypes, ReverseChainFromTo_node_to_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			bool[,] ReverseChainFromTo_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReverseChainFromTo_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ReverseChainFromTo_alt_0_base_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ReverseChainFromTo_alt_0_base_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ReverseChainFromTo_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromTo_alt_0_base_edge__edge0", "_edge0", ReverseChainFromTo_alt_0_base_edge__edge0_AllowedTypes, ReverseChainFromTo_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ReverseChainFromTo_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ReverseChainFromTo_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromTo_node_to, ReverseChainFromTo_node_from }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromTo_alt_0_base_edge__edge0 }, 
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
				ReverseChainFromTo_alt_0_base_isNodeHomomorphicGlobal,
				ReverseChainFromTo_alt_0_base_isEdgeHomomorphicGlobal,
				ReverseChainFromTo_alt_0_base_isNodeTotallyHomomorphic,
				ReverseChainFromTo_alt_0_base_isEdgeTotallyHomomorphic
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
			bool[] ReverseChainFromTo_alt_0_rec_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ReverseChainFromTo_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ReverseChainFromTo_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromTo_alt_0_rec_node_intermediate", "intermediate", ReverseChainFromTo_alt_0_rec_node_intermediate_AllowedTypes, ReverseChainFromTo_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ReverseChainFromTo_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromTo_alt_0_rec_edge__edge0", "_edge0", ReverseChainFromTo_alt_0_rec_edge__edge0_AllowedTypes, ReverseChainFromTo_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ReverseChainFromTo_alt_0_rec__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ReverseChainFromTo.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ReverseChainFromTo_alt_0_rec_node_intermediate"),
					new GRGEN_EXPR.GraphEntityExpression("ReverseChainFromTo_node_to"),
				}, 
				new string[] { }, new string[] { "ReverseChainFromTo_alt_0_rec_node_intermediate", "ReverseChainFromTo_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ReverseChainFromTo_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ReverseChainFromTo_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromTo_alt_0_rec_node_intermediate, ReverseChainFromTo_node_from, ReverseChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromTo_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ReverseChainFromTo_alt_0_rec__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ReverseChainFromTo_alt_0_rec_isNodeHomomorphicGlobal,
				ReverseChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal,
				ReverseChainFromTo_alt_0_rec_isNodeTotallyHomomorphic,
				ReverseChainFromTo_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ReverseChainFromTo_alt_0_rec.edgeToSourceNode.Add(ReverseChainFromTo_alt_0_rec_edge__edge0, ReverseChainFromTo_alt_0_rec_node_intermediate);
			ReverseChainFromTo_alt_0_rec.edgeToTargetNode.Add(ReverseChainFromTo_alt_0_rec_edge__edge0, ReverseChainFromTo_node_from);

			GRGEN_LGSP.Alternative ReverseChainFromTo_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ReverseChainFromTo_", new GRGEN_LGSP.PatternGraph[] { ReverseChainFromTo_alt_0_base, ReverseChainFromTo_alt_0_rec } );

			pat_ReverseChainFromTo = new GRGEN_LGSP.PatternGraph(
				"ReverseChainFromTo",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromTo_node_from, ReverseChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ReverseChainFromTo_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ReverseChainFromTo_isNodeHomomorphicGlobal,
				ReverseChainFromTo_isEdgeHomomorphicGlobal,
				ReverseChainFromTo_isNodeTotallyHomomorphic,
				ReverseChainFromTo_isEdgeTotallyHomomorphic
			);
			ReverseChainFromTo_alt_0_base.embeddingGraph = pat_ReverseChainFromTo;
			ReverseChainFromTo_alt_0_rec.embeddingGraph = pat_ReverseChainFromTo;

			ReverseChainFromTo_node_from.pointOfDefinition = null;
			ReverseChainFromTo_node_to.pointOfDefinition = null;
			ReverseChainFromTo_alt_0_base_edge__edge0.pointOfDefinition = ReverseChainFromTo_alt_0_base;
			ReverseChainFromTo_alt_0_rec_node_intermediate.pointOfDefinition = ReverseChainFromTo_alt_0_rec;
			ReverseChainFromTo_alt_0_rec_edge__edge0.pointOfDefinition = ReverseChainFromTo_alt_0_rec;
			ReverseChainFromTo_alt_0_rec__sub0.PointOfDefinition = ReverseChainFromTo_alt_0_rec;

			patternGraph = pat_ReverseChainFromTo;
		}


		public void ReverseChainFromTo_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ReverseChainFromTo_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ReverseChainFromTo_addedEdgeNames );
		}
		private static string[] create_ReverseChainFromTo_addedNodeNames = new string[] {  };
		private static string[] create_ReverseChainFromTo_addedEdgeNames = new string[] {  };

		public void ReverseChainFromTo_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ReverseChainFromTo curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ReverseChainFromTo_alt_0 alternative_alt_0 = curMatch._alt_0;
			ReverseChainFromTo_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ReverseChainFromTo_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ReverseChainFromTo_alt_0 curMatch)
		{
			if(curMatch.Pattern == ReverseChainFromTo_alt_0_base) {
				ReverseChainFromTo_alt_0_base_Delete(procEnv, (Match_ReverseChainFromTo_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ReverseChainFromTo_alt_0_rec) {
				ReverseChainFromTo_alt_0_rec_Delete(procEnv, (Match_ReverseChainFromTo_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ReverseChainFromTo_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ReverseChainFromTo_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
		}

		public void ReverseChainFromTo_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ReverseChainFromTo_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ReverseChainFromTo.Match_ReverseChainFromTo subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ReverseChainFromTo.Instance.ReverseChainFromTo_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_ReverseChainFromTo() {
		}

		public interface IMatch_ReverseChainFromTo : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ReverseChainFromTo_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReverseChainFromTo_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReverseChainFromTo_alt_0_base : IMatch_ReverseChainFromTo_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_to { get; }
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReverseChainFromTo_alt_0_rec : IMatch_ReverseChainFromTo_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_intermediate { get; }
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ReverseChainFromTo : GRGEN_LGSP.ListElement<Match_ReverseChainFromTo>, IMatch_ReverseChainFromTo
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ReverseChainFromTo_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_NodeNums.@from: return _node_from;
				case (int)ReverseChainFromTo_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ReverseChainFromTo_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ReverseChainFromTo_alt_0 _alt_0;
			public enum ReverseChainFromTo_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ReverseChainFromTo.instance.pat_ReverseChainFromTo; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ReverseChainFromTo_alt_0_base : GRGEN_LGSP.ListElement<Match_ReverseChainFromTo_alt_0_base>, IMatch_ReverseChainFromTo_alt_0_base
		{
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LGSP.LGSPNode _node_to;
			public GRGEN_LGSP.LGSPNode _node_from;
			public enum ReverseChainFromTo_alt_0_base_NodeNums { @to, @from, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_alt_0_base_NodeNums.@to: return _node_to;
				case (int)ReverseChainFromTo_alt_0_base_NodeNums.@from: return _node_from;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReverseChainFromTo_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ReverseChainFromTo.instance.ReverseChainFromTo_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ReverseChainFromTo_alt_0_rec : GRGEN_LGSP.ListElement<Match_ReverseChainFromTo_alt_0_rec>, IMatch_ReverseChainFromTo_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } }
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ReverseChainFromTo_alt_0_rec_NodeNums { @intermediate, @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_alt_0_rec_NodeNums.@intermediate: return _node_intermediate;
				case (int)ReverseChainFromTo_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ReverseChainFromTo_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReverseChainFromTo_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_sub0 { get { return @__sub0; } }
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @__sub0;
			public enum ReverseChainFromTo_alt_0_rec_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_alt_0_rec_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromTo_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ReverseChainFromTo.instance.ReverseChainFromTo_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromToReverse : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromToReverse instance = null;
		public static Pattern_ChainFromToReverse Instance { get { if (instance==null) { instance = new Pattern_ChainFromToReverse(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverse_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ChainFromToReverse_node_to_AllowedTypes = null;
		public static bool[] ChainFromToReverse_node_from_IsAllowedType = null;
		public static bool[] ChainFromToReverse_node_to_IsAllowedType = null;
		public enum ChainFromToReverse_NodeNums { @from, @to, };
		public enum ChainFromToReverse_EdgeNums { };
		public enum ChainFromToReverse_VariableNums { };
		public enum ChainFromToReverse_SubNums { };
		public enum ChainFromToReverse_AltNums { @alt_0, };
		public enum ChainFromToReverse_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_ChainFromToReverse;

		public enum ChainFromToReverse_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverse_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverse_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromToReverse_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromToReverse_alt_0_base_VariableNums { };
		public enum ChainFromToReverse_alt_0_base_SubNums { };
		public enum ChainFromToReverse_alt_0_base_AltNums { };
		public enum ChainFromToReverse_alt_0_base_IterNums { };




		public GRGEN_LGSP.PatternGraph ChainFromToReverse_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverse_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverse_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverse_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromToReverse_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromToReverse_alt_0_rec_VariableNums { };
		public enum ChainFromToReverse_alt_0_rec_SubNums { @cftr, };
		public enum ChainFromToReverse_alt_0_rec_AltNums { };
		public enum ChainFromToReverse_alt_0_rec_IterNums { };




		public GRGEN_LGSP.PatternGraph ChainFromToReverse_alt_0_rec;


		private Pattern_ChainFromToReverse()
		{
			name = "ChainFromToReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromToReverse_node_from", "ChainFromToReverse_node_to", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ChainFromToReverse_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromToReverse_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromToReverse_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ChainFromToReverse_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverse_node_from", "from", ChainFromToReverse_node_from_AllowedTypes, ChainFromToReverse_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ChainFromToReverse_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverse_node_to", "to", ChainFromToReverse_node_to_AllowedTypes, ChainFromToReverse_node_to_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			bool[,] ChainFromToReverse_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverse_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromToReverse_alt_0_base_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromToReverse_alt_0_base_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ChainFromToReverse_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverse_alt_0_base_edge__edge0", "_edge0", ChainFromToReverse_alt_0_base_edge__edge0_AllowedTypes, ChainFromToReverse_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ChainFromToReverse_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromToReverse_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverse_alt_0_base_edge__edge0 }, 
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
				ChainFromToReverse_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromToReverse_alt_0_base_isEdgeHomomorphicGlobal,
				ChainFromToReverse_alt_0_base_isNodeTotallyHomomorphic,
				ChainFromToReverse_alt_0_base_isEdgeTotallyHomomorphic
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
			bool[] ChainFromToReverse_alt_0_rec_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ChainFromToReverse_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromToReverse_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverse_alt_0_rec_node_intermediate", "intermediate", ChainFromToReverse_alt_0_rec_node_intermediate_AllowedTypes, ChainFromToReverse_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromToReverse_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverse_alt_0_rec_edge__edge0", "_edge0", ChainFromToReverse_alt_0_rec_edge__edge0_AllowedTypes, ChainFromToReverse_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromToReverse_alt_0_rec_cftr = new GRGEN_LGSP.PatternGraphEmbedding("cftr", Pattern_ChainFromToReverse.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ChainFromToReverse_alt_0_rec_node_intermediate"),
					new GRGEN_EXPR.GraphEntityExpression("ChainFromToReverse_node_to"),
				}, 
				new string[] { }, new string[] { "ChainFromToReverse_alt_0_rec_node_intermediate", "ChainFromToReverse_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ChainFromToReverse_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromToReverse_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_alt_0_rec_node_intermediate, ChainFromToReverse_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverse_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromToReverse_alt_0_rec_cftr }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromToReverse_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromToReverse_alt_0_rec_isEdgeHomomorphicGlobal,
				ChainFromToReverse_alt_0_rec_isNodeTotallyHomomorphic,
				ChainFromToReverse_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ChainFromToReverse_alt_0_rec.edgeToSourceNode.Add(ChainFromToReverse_alt_0_rec_edge__edge0, ChainFromToReverse_node_from);
			ChainFromToReverse_alt_0_rec.edgeToTargetNode.Add(ChainFromToReverse_alt_0_rec_edge__edge0, ChainFromToReverse_alt_0_rec_node_intermediate);

			GRGEN_LGSP.Alternative ChainFromToReverse_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromToReverse_", new GRGEN_LGSP.PatternGraph[] { ChainFromToReverse_alt_0_base, ChainFromToReverse_alt_0_rec } );

			pat_ChainFromToReverse = new GRGEN_LGSP.PatternGraph(
				"ChainFromToReverse",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromToReverse_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ChainFromToReverse_isNodeHomomorphicGlobal,
				ChainFromToReverse_isEdgeHomomorphicGlobal,
				ChainFromToReverse_isNodeTotallyHomomorphic,
				ChainFromToReverse_isEdgeTotallyHomomorphic
			);
			ChainFromToReverse_alt_0_base.embeddingGraph = pat_ChainFromToReverse;
			ChainFromToReverse_alt_0_rec.embeddingGraph = pat_ChainFromToReverse;

			ChainFromToReverse_node_from.pointOfDefinition = null;
			ChainFromToReverse_node_to.pointOfDefinition = null;
			ChainFromToReverse_alt_0_base_edge__edge0.pointOfDefinition = ChainFromToReverse_alt_0_base;
			ChainFromToReverse_alt_0_rec_node_intermediate.pointOfDefinition = ChainFromToReverse_alt_0_rec;
			ChainFromToReverse_alt_0_rec_edge__edge0.pointOfDefinition = ChainFromToReverse_alt_0_rec;
			ChainFromToReverse_alt_0_rec_cftr.PointOfDefinition = ChainFromToReverse_alt_0_rec;

			patternGraph = pat_ChainFromToReverse;
		}


		public void ChainFromToReverse_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_ChainFromToReverse curMatch = (Match_ChainFromToReverse)_curMatch;
			IMatch_ChainFromToReverse_alt_0 alternative_alt_0 = curMatch._alt_0;
			graph.SettingAddedNodeNames( ChainFromToReverse_addedNodeNames );
			ChainFromToReverse_alt_0_Modify(procEnv, alternative_alt_0);
			graph.SettingAddedEdgeNames( ChainFromToReverse_addedEdgeNames );
		}
		private static string[] ChainFromToReverse_addedNodeNames = new string[] {  };
		private static string[] ChainFromToReverse_addedEdgeNames = new string[] {  };

		public void ChainFromToReverse_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ChainFromToReverse_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromToReverse_addedEdgeNames );
		}
		private static string[] create_ChainFromToReverse_addedNodeNames = new string[] {  };
		private static string[] create_ChainFromToReverse_addedEdgeNames = new string[] {  };

		public void ChainFromToReverse_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromToReverse curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ChainFromToReverse_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromToReverse_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ChainFromToReverse_alt_0_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromToReverse_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_Modify(procEnv, (Match_ChainFromToReverse_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_Modify(procEnv, (Match_ChainFromToReverse_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromToReverse_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_Delete(procEnv, (Match_ChainFromToReverse_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_Delete(procEnv, (Match_ChainFromToReverse_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_base_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_ChainFromToReverse_alt_0_base curMatch = (Match_ChainFromToReverse_alt_0_base)_curMatch;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_base_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_to, node_from);
			graph.Remove(edge__edge0);
		}
		private static string[] ChainFromToReverse_alt_0_base_addedNodeNames = new string[] {  };
		private static string[] ChainFromToReverse_alt_0_base_addedEdgeNames = new string[] { "_edge1" };

		public void ChainFromToReverse_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromToReverse_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
		}

		public void ChainFromToReverse_alt_0_rec_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_ChainFromToReverse_alt_0_rec curMatch = (Match_ChainFromToReverse_alt_0_rec)_curMatch;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(procEnv, subpattern_cftr);
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_rec_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_intermediate, node_from);
			graph.Remove(edge__edge0);
		}
		private static string[] ChainFromToReverse_alt_0_rec_addedNodeNames = new string[] {  };
		private static string[] ChainFromToReverse_alt_0_rec_addedEdgeNames = new string[] { "_edge1" };

		public void ChainFromToReverse_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromToReverse_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Delete(procEnv, subpattern_cftr);
		}

		static Pattern_ChainFromToReverse() {
		}

		public interface IMatch_ChainFromToReverse : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ChainFromToReverse_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromToReverse_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromToReverse_alt_0_base : IMatch_ChainFromToReverse_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromToReverse_alt_0_rec : IMatch_ChainFromToReverse_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_intermediate { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromToReverse.Match_ChainFromToReverse @cftr { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ChainFromToReverse : GRGEN_LGSP.ListElement<Match_ChainFromToReverse>, IMatch_ChainFromToReverse
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromToReverse_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverse_NodeNums.@from: return _node_from;
				case (int)ChainFromToReverse_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ChainFromToReverse_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ChainFromToReverse_alt_0 _alt_0;
			public enum ChainFromToReverse_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverse_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromToReverse.instance.pat_ChainFromToReverse; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromToReverse_alt_0_base : GRGEN_LGSP.ListElement<Match_ChainFromToReverse_alt_0_base>, IMatch_ChainFromToReverse_alt_0_base
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromToReverse_alt_0_base_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverse_alt_0_base_NodeNums.@from: return _node_from;
				case (int)ChainFromToReverse_alt_0_base_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromToReverse_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverse_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromToReverse.instance.ChainFromToReverse_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromToReverse_alt_0_rec : GRGEN_LGSP.ListElement<Match_ChainFromToReverse_alt_0_rec>, IMatch_ChainFromToReverse_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromToReverse_alt_0_rec_NodeNums { @from, @intermediate, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverse_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ChainFromToReverse_alt_0_rec_NodeNums.@intermediate: return _node_intermediate;
				case (int)ChainFromToReverse_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromToReverse_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverse_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromToReverse.Match_ChainFromToReverse @cftr { get { return @_cftr; } }
			public @Pattern_ChainFromToReverse.Match_ChainFromToReverse @_cftr;
			public enum ChainFromToReverse_alt_0_rec_SubNums { @cftr, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverse_alt_0_rec_SubNums.@cftr: return _cftr;
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverse_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromToReverse.instance.ChainFromToReverse_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromToReverseToCommon : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromToReverseToCommon instance = null;
		public static Pattern_ChainFromToReverseToCommon Instance { get { if (instance==null) { instance = new Pattern_ChainFromToReverseToCommon(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverseToCommon_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ChainFromToReverseToCommon_node_to_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_node_from_IsAllowedType = null;
		public static bool[] ChainFromToReverseToCommon_node_to_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_NodeNums { @from, @to, };
		public enum ChainFromToReverseToCommon_EdgeNums { };
		public enum ChainFromToReverseToCommon_VariableNums { };
		public enum ChainFromToReverseToCommon_SubNums { };
		public enum ChainFromToReverseToCommon_AltNums { @alt_0, };
		public enum ChainFromToReverseToCommon_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_ChainFromToReverseToCommon;

		public enum ChainFromToReverseToCommon_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverseToCommon_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromToReverseToCommon_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromToReverseToCommon_alt_0_base_VariableNums { };
		public enum ChainFromToReverseToCommon_alt_0_base_SubNums { };
		public enum ChainFromToReverseToCommon_alt_0_base_AltNums { };
		public enum ChainFromToReverseToCommon_alt_0_base_IterNums { };




		public GRGEN_LGSP.PatternGraph ChainFromToReverseToCommon_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverseToCommon_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverseToCommon_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromToReverseToCommon_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromToReverseToCommon_alt_0_rec_VariableNums { };
		public enum ChainFromToReverseToCommon_alt_0_rec_SubNums { @cftrtc, };
		public enum ChainFromToReverseToCommon_alt_0_rec_AltNums { };
		public enum ChainFromToReverseToCommon_alt_0_rec_IterNums { };




		public GRGEN_LGSP.PatternGraph ChainFromToReverseToCommon_alt_0_rec;


		private Pattern_ChainFromToReverseToCommon()
		{
			name = "ChainFromToReverseToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromToReverseToCommon_node_from", "ChainFromToReverseToCommon_node_to", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ChainFromToReverseToCommon_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverseToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ChainFromToReverseToCommon_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromToReverseToCommon_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ChainFromToReverseToCommon_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverseToCommon_node_from", "from", ChainFromToReverseToCommon_node_from_AllowedTypes, ChainFromToReverseToCommon_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ChainFromToReverseToCommon_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverseToCommon_node_to", "to", ChainFromToReverseToCommon_node_to_AllowedTypes, ChainFromToReverseToCommon_node_to_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			bool[,] ChainFromToReverseToCommon_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverseToCommon_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ChainFromToReverseToCommon_alt_0_base_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ChainFromToReverseToCommon_alt_0_base_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ChainFromToReverseToCommon_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverseToCommon_alt_0_base_edge__edge0", "_edge0", ChainFromToReverseToCommon_alt_0_base_edge__edge0_AllowedTypes, ChainFromToReverseToCommon_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ChainFromToReverseToCommon_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromToReverseToCommon_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverseToCommon_alt_0_base_edge__edge0 }, 
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
				ChainFromToReverseToCommon_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromToReverseToCommon_alt_0_base_isEdgeHomomorphicGlobal,
				ChainFromToReverseToCommon_alt_0_base_isNodeTotallyHomomorphic,
				ChainFromToReverseToCommon_alt_0_base_isEdgeTotallyHomomorphic
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
			bool[] ChainFromToReverseToCommon_alt_0_rec_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ChainFromToReverseToCommon_alt_0_rec_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ChainFromToReverseToCommon_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverseToCommon_alt_0_rec_node_intermediate", "intermediate", ChainFromToReverseToCommon_alt_0_rec_node_intermediate_AllowedTypes, ChainFromToReverseToCommon_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverseToCommon_alt_0_rec_edge__edge0", "_edge0", ChainFromToReverseToCommon_alt_0_rec_edge__edge0_AllowedTypes, ChainFromToReverseToCommon_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromToReverseToCommon_alt_0_rec_cftrtc = new GRGEN_LGSP.PatternGraphEmbedding("cftrtc", Pattern_ChainFromToReverseToCommon.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ChainFromToReverseToCommon_alt_0_rec_node_intermediate"),
					new GRGEN_EXPR.GraphEntityExpression("ChainFromToReverseToCommon_node_to"),
				}, 
				new string[] { }, new string[] { "ChainFromToReverseToCommon_alt_0_rec_node_intermediate", "ChainFromToReverseToCommon_node_to" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ChainFromToReverseToCommon_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromToReverseToCommon_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_alt_0_rec_node_intermediate, ChainFromToReverseToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverseToCommon_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromToReverseToCommon_alt_0_rec_cftrtc }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromToReverseToCommon_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromToReverseToCommon_alt_0_rec_isEdgeHomomorphicGlobal,
				ChainFromToReverseToCommon_alt_0_rec_isNodeTotallyHomomorphic,
				ChainFromToReverseToCommon_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ChainFromToReverseToCommon_alt_0_rec.edgeToSourceNode.Add(ChainFromToReverseToCommon_alt_0_rec_edge__edge0, ChainFromToReverseToCommon_node_from);
			ChainFromToReverseToCommon_alt_0_rec.edgeToTargetNode.Add(ChainFromToReverseToCommon_alt_0_rec_edge__edge0, ChainFromToReverseToCommon_alt_0_rec_node_intermediate);

			GRGEN_LGSP.Alternative ChainFromToReverseToCommon_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromToReverseToCommon_", new GRGEN_LGSP.PatternGraph[] { ChainFromToReverseToCommon_alt_0_base, ChainFromToReverseToCommon_alt_0_rec } );

			pat_ChainFromToReverseToCommon = new GRGEN_LGSP.PatternGraph(
				"ChainFromToReverseToCommon",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromToReverseToCommon_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ChainFromToReverseToCommon_isNodeHomomorphicGlobal,
				ChainFromToReverseToCommon_isEdgeHomomorphicGlobal,
				ChainFromToReverseToCommon_isNodeTotallyHomomorphic,
				ChainFromToReverseToCommon_isEdgeTotallyHomomorphic
			);
			ChainFromToReverseToCommon_alt_0_base.embeddingGraph = pat_ChainFromToReverseToCommon;
			ChainFromToReverseToCommon_alt_0_rec.embeddingGraph = pat_ChainFromToReverseToCommon;

			ChainFromToReverseToCommon_node_from.pointOfDefinition = null;
			ChainFromToReverseToCommon_node_to.pointOfDefinition = null;
			ChainFromToReverseToCommon_alt_0_base_edge__edge0.pointOfDefinition = ChainFromToReverseToCommon_alt_0_base;
			ChainFromToReverseToCommon_alt_0_rec_node_intermediate.pointOfDefinition = ChainFromToReverseToCommon_alt_0_rec;
			ChainFromToReverseToCommon_alt_0_rec_edge__edge0.pointOfDefinition = ChainFromToReverseToCommon_alt_0_rec;
			ChainFromToReverseToCommon_alt_0_rec_cftrtc.PointOfDefinition = ChainFromToReverseToCommon_alt_0_rec;

			patternGraph = pat_ChainFromToReverseToCommon;
		}


		public void ChainFromToReverseToCommon_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_ChainFromToReverseToCommon curMatch = (Match_ChainFromToReverseToCommon)_curMatch;
			IMatch_ChainFromToReverseToCommon_alt_0 alternative_alt_0 = curMatch._alt_0;
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_addedNodeNames );
			ChainFromToReverseToCommon_alt_0_Modify(procEnv, alternative_alt_0, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_addedEdgeNames );
		}
		private static string[] ChainFromToReverseToCommon_addedNodeNames = new string[] {  };
		private static string[] ChainFromToReverseToCommon_addedEdgeNames = new string[] {  };

		public void ChainFromToReverseToCommon_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ChainFromToReverseToCommon_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromToReverseToCommon_addedEdgeNames );
		}
		private static string[] create_ChainFromToReverseToCommon_addedNodeNames = new string[] {  };
		private static string[] create_ChainFromToReverseToCommon_addedEdgeNames = new string[] {  };

		public void ChainFromToReverseToCommon_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromToReverseToCommon curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ChainFromToReverseToCommon_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromToReverseToCommon_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ChainFromToReverseToCommon_alt_0_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromToReverseToCommon_alt_0 curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_Modify(procEnv, (Match_ChainFromToReverseToCommon_alt_0_base)curMatch, node_common);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_Modify(procEnv, (Match_ChainFromToReverseToCommon_alt_0_rec)curMatch, node_common);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ChainFromToReverseToCommon_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_Delete(procEnv, (Match_ChainFromToReverseToCommon_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_Delete(procEnv, (Match_ChainFromToReverseToCommon_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_base_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_ChainFromToReverseToCommon_alt_0_base curMatch = (Match_ChainFromToReverseToCommon_alt_0_base)_curMatch;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_base_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_to, node_from);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_from, node_common);
			GRGEN_MODEL.@Edge edge__edge3 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_to, node_common);
			graph.Remove(edge__edge0);
		}
		private static string[] ChainFromToReverseToCommon_alt_0_base_addedNodeNames = new string[] {  };
		private static string[] ChainFromToReverseToCommon_alt_0_base_addedEdgeNames = new string[] { "_edge1", "_edge2", "_edge3" };

		public void ChainFromToReverseToCommon_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromToReverseToCommon_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
		}

		public void ChainFromToReverseToCommon_alt_0_rec_Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_ChainFromToReverseToCommon_alt_0_rec curMatch = (Match_ChainFromToReverseToCommon_alt_0_rec)_curMatch;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(procEnv, subpattern_cftrtc, (GRGEN_LGSP.LGSPNode)(node_common));
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_intermediate, node_from);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_from, node_common);
			graph.Remove(edge__edge0);
		}
		private static string[] ChainFromToReverseToCommon_alt_0_rec_addedNodeNames = new string[] {  };
		private static string[] ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames = new string[] { "_edge1", "_edge2" };

		public void ChainFromToReverseToCommon_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ChainFromToReverseToCommon_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Delete(procEnv, subpattern_cftrtc);
		}

		static Pattern_ChainFromToReverseToCommon() {
		}

		public interface IMatch_ChainFromToReverseToCommon : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ChainFromToReverseToCommon_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromToReverseToCommon_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromToReverseToCommon_alt_0_base : IMatch_ChainFromToReverseToCommon_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ChainFromToReverseToCommon_alt_0_rec : IMatch_ChainFromToReverseToCommon_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_intermediate { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon @cftrtc { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ChainFromToReverseToCommon : GRGEN_LGSP.ListElement<Match_ChainFromToReverseToCommon>, IMatch_ChainFromToReverseToCommon
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromToReverseToCommon_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverseToCommon_NodeNums.@from: return _node_from;
				case (int)ChainFromToReverseToCommon_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ChainFromToReverseToCommon_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ChainFromToReverseToCommon_alt_0 _alt_0;
			public enum ChainFromToReverseToCommon_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverseToCommon_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromToReverseToCommon.instance.pat_ChainFromToReverseToCommon; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromToReverseToCommon_alt_0_base : GRGEN_LGSP.ListElement<Match_ChainFromToReverseToCommon_alt_0_base>, IMatch_ChainFromToReverseToCommon_alt_0_base
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromToReverseToCommon_alt_0_base_NodeNums { @from, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverseToCommon_alt_0_base_NodeNums.@from: return _node_from;
				case (int)ChainFromToReverseToCommon_alt_0_base_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromToReverseToCommon_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverseToCommon_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromToReverseToCommon.instance.ChainFromToReverseToCommon_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ChainFromToReverseToCommon_alt_0_rec : GRGEN_LGSP.ListElement<Match_ChainFromToReverseToCommon_alt_0_rec>, IMatch_ChainFromToReverseToCommon_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ChainFromToReverseToCommon_alt_0_rec_NodeNums { @from, @intermediate, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@intermediate: return _node_intermediate;
				case (int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ChainFromToReverseToCommon_alt_0_rec_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverseToCommon_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon @cftrtc { get { return @_cftrtc; } }
			public @Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon @_cftrtc;
			public enum ChainFromToReverseToCommon_alt_0_rec_SubNums { @cftrtc, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromToReverseToCommon_alt_0_rec_SubNums.@cftrtc: return _cftrtc;
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ChainFromToReverseToCommon_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ChainFromToReverseToCommon.instance.ChainFromToReverseToCommon_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ReverseChainFromToToCommon : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ReverseChainFromToToCommon instance = null;
		public static Pattern_ReverseChainFromToToCommon Instance { get { if (instance==null) { instance = new Pattern_ReverseChainFromToToCommon(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ReverseChainFromToToCommon_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ReverseChainFromToToCommon_node_to_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ReverseChainFromToToCommon_node_common_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_node_from_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_node_to_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_node_common_IsAllowedType = null;
		public enum ReverseChainFromToToCommon_NodeNums { @from, @to, @common, };
		public enum ReverseChainFromToToCommon_EdgeNums { };
		public enum ReverseChainFromToToCommon_VariableNums { };
		public enum ReverseChainFromToToCommon_SubNums { };
		public enum ReverseChainFromToToCommon_AltNums { @alt_0, };
		public enum ReverseChainFromToToCommon_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ReverseChainFromToToCommon;

		public enum ReverseChainFromToToCommon_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromToToCommon_alt_0_base_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromToToCommon_alt_0_base_edge__edge1_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_base_edge__edge0_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_base_edge__edge1_IsAllowedType = null;
		public enum ReverseChainFromToToCommon_alt_0_base_NodeNums { @to, @from, @common, };
		public enum ReverseChainFromToToCommon_alt_0_base_EdgeNums { @_edge0, @_edge1, };
		public enum ReverseChainFromToToCommon_alt_0_base_VariableNums { };
		public enum ReverseChainFromToToCommon_alt_0_base_SubNums { };
		public enum ReverseChainFromToToCommon_alt_0_base_AltNums { };
		public enum ReverseChainFromToToCommon_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph ReverseChainFromToToCommon_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ReverseChainFromToToCommon_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromToToCommon_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromToToCommon_alt_0_rec_edge__edge1_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_edge__edge0_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_edge__edge1_IsAllowedType = null;
		public enum ReverseChainFromToToCommon_alt_0_rec_NodeNums { @intermediate, @from, @common, @to, };
		public enum ReverseChainFromToToCommon_alt_0_rec_EdgeNums { @_edge0, @_edge1, };
		public enum ReverseChainFromToToCommon_alt_0_rec_VariableNums { };
		public enum ReverseChainFromToToCommon_alt_0_rec_SubNums { @_sub0, };
		public enum ReverseChainFromToToCommon_alt_0_rec_AltNums { };
		public enum ReverseChainFromToToCommon_alt_0_rec_IterNums { };



		public GRGEN_LGSP.PatternGraph ReverseChainFromToToCommon_alt_0_rec;


		private Pattern_ReverseChainFromToToCommon()
		{
			name = "ReverseChainFromToToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "ReverseChainFromToToCommon_node_from", "ReverseChainFromToToCommon_node_to", "ReverseChainFromToToCommon_node_common", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] ReverseChainFromToToCommon_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ReverseChainFromToToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] ReverseChainFromToToCommon_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ReverseChainFromToToCommon_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_node_from = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_node_from", "from", ReverseChainFromToToCommon_node_from_AllowedTypes, ReverseChainFromToToCommon_node_from_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_node_to = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_node_to", "to", ReverseChainFromToToCommon_node_to_AllowedTypes, ReverseChainFromToToCommon_node_to_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_node_common = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_node_common", "common", ReverseChainFromToToCommon_node_common_AllowedTypes, ReverseChainFromToToCommon_node_common_IsAllowedType, 5.5F, 2, false, null, null, null, null, null, false);
			bool[,] ReverseChainFromToToCommon_alt_0_base_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ReverseChainFromToToCommon_alt_0_base_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] ReverseChainFromToToCommon_alt_0_base_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ReverseChainFromToToCommon_alt_0_base_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_base_edge__edge0", "_edge0", ReverseChainFromToToCommon_alt_0_base_edge__edge0_AllowedTypes, ReverseChainFromToToCommon_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_base_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_base_edge__edge1", "_edge1", ReverseChainFromToToCommon_alt_0_base_edge__edge1_AllowedTypes, ReverseChainFromToToCommon_alt_0_base_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ReverseChainFromToToCommon_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ReverseChainFromToToCommon_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_common }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromToToCommon_alt_0_base_edge__edge0, ReverseChainFromToToCommon_alt_0_base_edge__edge1 }, 
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
				ReverseChainFromToToCommon_alt_0_base_isNodeHomomorphicGlobal,
				ReverseChainFromToToCommon_alt_0_base_isEdgeHomomorphicGlobal,
				ReverseChainFromToToCommon_alt_0_base_isNodeTotallyHomomorphic,
				ReverseChainFromToToCommon_alt_0_base_isEdgeTotallyHomomorphic
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
			bool[] ReverseChainFromToToCommon_alt_0_rec_isNodeTotallyHomomorphic = new bool[4] { false, false, false, false,  };
			bool[] ReverseChainFromToToCommon_alt_0_rec_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_alt_0_rec_node_intermediate", "intermediate", ReverseChainFromToToCommon_alt_0_rec_node_intermediate_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_rec_edge__edge0", "_edge0", ReverseChainFromToToCommon_alt_0_rec_edge__edge0_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_rec_edge__edge1", "_edge1", ReverseChainFromToToCommon_alt_0_rec_edge__edge1_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding ReverseChainFromToToCommon_alt_0_rec__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ReverseChainFromToToCommon.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ReverseChainFromToToCommon_alt_0_rec_node_intermediate"),
					new GRGEN_EXPR.GraphEntityExpression("ReverseChainFromToToCommon_node_to"),
					new GRGEN_EXPR.GraphEntityExpression("ReverseChainFromToToCommon_node_common"),
				}, 
				new string[] { }, new string[] { "ReverseChainFromToToCommon_alt_0_rec_node_intermediate", "ReverseChainFromToToCommon_node_to", "ReverseChainFromToToCommon_node_common" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ReverseChainFromToToCommon_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ReverseChainFromToToCommon_alt_0_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromToToCommon_alt_0_rec_node_intermediate, ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_common, ReverseChainFromToToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromToToCommon_alt_0_rec_edge__edge0, ReverseChainFromToToCommon_alt_0_rec_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ReverseChainFromToToCommon_alt_0_rec__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
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
				ReverseChainFromToToCommon_alt_0_rec_isEdgeHomomorphicGlobal,
				ReverseChainFromToToCommon_alt_0_rec_isNodeTotallyHomomorphic,
				ReverseChainFromToToCommon_alt_0_rec_isEdgeTotallyHomomorphic
			);
			ReverseChainFromToToCommon_alt_0_rec.edgeToSourceNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge0, ReverseChainFromToToCommon_alt_0_rec_node_intermediate);
			ReverseChainFromToToCommon_alt_0_rec.edgeToTargetNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge0, ReverseChainFromToToCommon_node_from);
			ReverseChainFromToToCommon_alt_0_rec.edgeToSourceNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge1, ReverseChainFromToToCommon_node_from);
			ReverseChainFromToToCommon_alt_0_rec.edgeToTargetNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge1, ReverseChainFromToToCommon_node_common);

			GRGEN_LGSP.Alternative ReverseChainFromToToCommon_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ReverseChainFromToToCommon_", new GRGEN_LGSP.PatternGraph[] { ReverseChainFromToToCommon_alt_0_base, ReverseChainFromToToCommon_alt_0_rec } );

			pat_ReverseChainFromToToCommon = new GRGEN_LGSP.PatternGraph(
				"ReverseChainFromToToCommon",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_common }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ReverseChainFromToToCommon_alt_0,  }, 
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
				new bool[0, 0] ,
				ReverseChainFromToToCommon_isNodeHomomorphicGlobal,
				ReverseChainFromToToCommon_isEdgeHomomorphicGlobal,
				ReverseChainFromToToCommon_isNodeTotallyHomomorphic,
				ReverseChainFromToToCommon_isEdgeTotallyHomomorphic
			);
			ReverseChainFromToToCommon_alt_0_base.embeddingGraph = pat_ReverseChainFromToToCommon;
			ReverseChainFromToToCommon_alt_0_rec.embeddingGraph = pat_ReverseChainFromToToCommon;

			ReverseChainFromToToCommon_node_from.pointOfDefinition = null;
			ReverseChainFromToToCommon_node_to.pointOfDefinition = null;
			ReverseChainFromToToCommon_node_common.pointOfDefinition = null;
			ReverseChainFromToToCommon_alt_0_base_edge__edge0.pointOfDefinition = ReverseChainFromToToCommon_alt_0_base;
			ReverseChainFromToToCommon_alt_0_base_edge__edge1.pointOfDefinition = ReverseChainFromToToCommon_alt_0_base;
			ReverseChainFromToToCommon_alt_0_rec_node_intermediate.pointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;
			ReverseChainFromToToCommon_alt_0_rec_edge__edge0.pointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;
			ReverseChainFromToToCommon_alt_0_rec_edge__edge1.pointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;
			ReverseChainFromToToCommon_alt_0_rec__sub0.PointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;

			patternGraph = pat_ReverseChainFromToToCommon;
		}


		public void ReverseChainFromToToCommon_Create(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to, GRGEN_LGSP.LGSPNode node_common)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			graph.SettingAddedNodeNames( create_ReverseChainFromToToCommon_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ReverseChainFromToToCommon_addedEdgeNames );
		}
		private static string[] create_ReverseChainFromToToCommon_addedNodeNames = new string[] {  };
		private static string[] create_ReverseChainFromToToCommon_addedEdgeNames = new string[] {  };

		public void ReverseChainFromToToCommon_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ReverseChainFromToToCommon curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			IMatch_ReverseChainFromToToCommon_alt_0 alternative_alt_0 = curMatch._alt_0;
			ReverseChainFromToToCommon_alt_0_Delete(procEnv, alternative_alt_0);
		}

		public void ReverseChainFromToToCommon_alt_0_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IMatch_ReverseChainFromToToCommon_alt_0 curMatch)
		{
			if(curMatch.Pattern == ReverseChainFromToToCommon_alt_0_base) {
				ReverseChainFromToToCommon_alt_0_base_Delete(procEnv, (Match_ReverseChainFromToToCommon_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ReverseChainFromToToCommon_alt_0_rec) {
				ReverseChainFromToToCommon_alt_0_rec_Delete(procEnv, (Match_ReverseChainFromToToCommon_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ReverseChainFromToToCommon_alt_0_base_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ReverseChainFromToToCommon_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_common = curMatch._node_common;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_common);
			graph.Remove(node_common);
		}

		public void ReverseChainFromToToCommon_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, Match_ReverseChainFromToToCommon_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPNode node_common = curMatch._node_common;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			graph.RemoveEdges(node_from);
			graph.Remove(node_from);
			graph.RemoveEdges(node_common);
			graph.Remove(node_common);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ReverseChainFromToToCommon.Instance.ReverseChainFromToToCommon_Delete(procEnv, subpattern__sub0);
		}

		static Pattern_ReverseChainFromToToCommon() {
		}

		public interface IMatch_ReverseChainFromToToCommon : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_to { get; }
			GRGEN_LIBGR.INode node_common { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ReverseChainFromToToCommon_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReverseChainFromToToCommon_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReverseChainFromToToCommon_alt_0_base : IMatch_ReverseChainFromToToCommon_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_to { get; }
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_common { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReverseChainFromToToCommon_alt_0_rec : IMatch_ReverseChainFromToToCommon_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_intermediate { get; }
			GRGEN_LIBGR.INode node_from { get; }
			GRGEN_LIBGR.INode node_common { get; }
			GRGEN_LIBGR.INode node_to { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ReverseChainFromToToCommon : GRGEN_LGSP.ListElement<Match_ReverseChainFromToToCommon>, IMatch_ReverseChainFromToToCommon
		{
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LIBGR.INode node_common { get { return (GRGEN_LIBGR.INode)_node_common; } }
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_to;
			public GRGEN_LGSP.LGSPNode _node_common;
			public enum ReverseChainFromToToCommon_NodeNums { @from, @to, @common, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_NodeNums.@from: return _node_from;
				case (int)ReverseChainFromToToCommon_NodeNums.@to: return _node_to;
				case (int)ReverseChainFromToToCommon_NodeNums.@common: return _node_common;
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ReverseChainFromToToCommon_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ReverseChainFromToToCommon_alt_0 _alt_0;
			public enum ReverseChainFromToToCommon_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ReverseChainFromToToCommon.instance.pat_ReverseChainFromToToCommon; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ReverseChainFromToToCommon_alt_0_base : GRGEN_LGSP.ListElement<Match_ReverseChainFromToToCommon_alt_0_base>, IMatch_ReverseChainFromToToCommon_alt_0_base
		{
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_common { get { return (GRGEN_LIBGR.INode)_node_common; } }
			public GRGEN_LGSP.LGSPNode _node_to;
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_common;
			public enum ReverseChainFromToToCommon_alt_0_base_NodeNums { @to, @from, @common, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_alt_0_base_NodeNums.@to: return _node_to;
				case (int)ReverseChainFromToToCommon_alt_0_base_NodeNums.@from: return _node_from;
				case (int)ReverseChainFromToToCommon_alt_0_base_NodeNums.@common: return _node_common;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum ReverseChainFromToToCommon_alt_0_base_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
				case (int)ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_base_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ReverseChainFromToToCommon.instance.ReverseChainFromToToCommon_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ReverseChainFromToToCommon_alt_0_rec : GRGEN_LGSP.ListElement<Match_ReverseChainFromToToCommon_alt_0_rec>, IMatch_ReverseChainFromToToCommon_alt_0_rec
		{
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } }
			public GRGEN_LIBGR.INode node_from { get { return (GRGEN_LIBGR.INode)_node_from; } }
			public GRGEN_LIBGR.INode node_common { get { return (GRGEN_LIBGR.INode)_node_common; } }
			public GRGEN_LIBGR.INode node_to { get { return (GRGEN_LIBGR.INode)_node_to; } }
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public GRGEN_LGSP.LGSPNode _node_from;
			public GRGEN_LGSP.LGSPNode _node_common;
			public GRGEN_LGSP.LGSPNode _node_to;
			public enum ReverseChainFromToToCommon_alt_0_rec_NodeNums { @intermediate, @from, @common, @to, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_alt_0_rec_NodeNums.@intermediate: return _node_intermediate;
				case (int)ReverseChainFromToToCommon_alt_0_rec_NodeNums.@from: return _node_from;
				case (int)ReverseChainFromToToCommon_alt_0_rec_NodeNums.@common: return _node_common;
				case (int)ReverseChainFromToToCommon_alt_0_rec_NodeNums.@to: return _node_to;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum ReverseChainFromToToCommon_alt_0_rec_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_alt_0_rec_EdgeNums.@_edge0: return _edge__edge0;
				case (int)ReverseChainFromToToCommon_alt_0_rec_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_rec_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_sub0 { get { return @__sub0; } }
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @__sub0;
			public enum ReverseChainFromToToCommon_alt_0_rec_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_alt_0_rec_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_rec_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_rec_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReverseChainFromToToCommon_alt_0_rec_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ReverseChainFromToToCommon.instance.ReverseChainFromToToCommon_alt_0_rec; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createChain : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createChain instance = null;
		public static Rule_createChain Instance { get { if (instance==null) { instance = new Rule_createChain(); instance.initialize(); } return instance; } }

		public enum createChain_NodeNums { };
		public enum createChain_EdgeNums { };
		public enum createChain_VariableNums { };
		public enum createChain_SubNums { };
		public enum createChain_AltNums { };
		public enum createChain_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_createChain;


		private Rule_createChain()
		{
			name = "createChain";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };

		}
		private void initialize()
		{
			bool[,] createChain_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createChain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] createChain_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] createChain_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_createChain = new GRGEN_LGSP.PatternGraph(
				"createChain",
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
				createChain_isNodeHomomorphicGlobal,
				createChain_isEdgeHomomorphicGlobal,
				createChain_isNodeTotallyHomomorphic,
				createChain_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_createChain;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_createChain curMatch = (Match_createChain)_curMatch;
			graph.SettingAddedNodeNames( createChain_addedNodeNames );
			GRGEN_MODEL.@Node node_beg = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_end = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createChain_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_beg, node__node0);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node0, node__node1);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node1, node_end);
			output_0 = (GRGEN_LIBGR.INode)(node_beg);
			output_1 = (GRGEN_LIBGR.INode)(node_end);
			return;
		}
		private static string[] createChain_addedNodeNames = new string[] { "beg", "_node0", "_node1", "end" };
		private static string[] createChain_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2" };

		static Rule_createChain() {
		}

		public interface IMatch_createChain : GRGEN_LIBGR.IMatch
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

		public class Match_createChain : GRGEN_LGSP.ListElement<Match_createChain>, IMatch_createChain
		{
			public enum createChain_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createChain_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createChain_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createChain_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createChain_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createChain_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createChain_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createChain.instance.pat_createChain; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromTo : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromTo instance = null;
		public static Rule_chainFromTo Instance { get { if (instance==null) { instance = new Rule_chainFromTo(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] chainFromTo_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] chainFromTo_node_end_AllowedTypes = null;
		public static bool[] chainFromTo_node_beg_IsAllowedType = null;
		public static bool[] chainFromTo_node_end_IsAllowedType = null;
		public enum chainFromTo_NodeNums { @beg, @end, };
		public enum chainFromTo_EdgeNums { };
		public enum chainFromTo_VariableNums { };
		public enum chainFromTo_SubNums { @_sub0, };
		public enum chainFromTo_AltNums { };
		public enum chainFromTo_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_chainFromTo;


		private Rule_chainFromTo()
		{
			name = "chainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromTo_node_beg", "chainFromTo_node_end", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] chainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] chainFromTo_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] chainFromTo_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode chainFromTo_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromTo_node_beg", "beg", chainFromTo_node_beg_AllowedTypes, chainFromTo_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode chainFromTo_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromTo_node_end", "end", chainFromTo_node_end_AllowedTypes, chainFromTo_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding chainFromTo__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromTo.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("chainFromTo_node_beg"),
					new GRGEN_EXPR.GraphEntityExpression("chainFromTo_node_end"),
				}, 
				new string[] { }, new string[] { "chainFromTo_node_beg", "chainFromTo_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_chainFromTo = new GRGEN_LGSP.PatternGraph(
				"chainFromTo",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { chainFromTo_node_beg, chainFromTo_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromTo__sub0 }, 
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
				new bool[0, 0] ,
				chainFromTo_isNodeHomomorphicGlobal,
				chainFromTo_isEdgeHomomorphicGlobal,
				chainFromTo_isNodeTotallyHomomorphic,
				chainFromTo_isEdgeTotallyHomomorphic
			);

			chainFromTo_node_beg.pointOfDefinition = null;
			chainFromTo_node_end.pointOfDefinition = null;
			chainFromTo__sub0.PointOfDefinition = pat_chainFromTo;

			patternGraph = pat_chainFromTo;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_chainFromTo curMatch = (Match_chainFromTo)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			Pattern_ChainFromTo.Match_ChainFromTo subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_chainFromTo() {
		}

		public interface IMatch_chainFromTo : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromTo.Match_ChainFromTo @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_chainFromTo : GRGEN_LGSP.ListElement<Match_chainFromTo>, IMatch_chainFromTo
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum chainFromTo_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)chainFromTo_NodeNums.@beg: return _node_beg;
				case (int)chainFromTo_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum chainFromTo_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromTo_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromTo.Match_ChainFromTo @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromTo.Match_ChainFromTo @__sub0;
			public enum chainFromTo_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromTo_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum chainFromTo_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromTo_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromTo_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_chainFromTo.instance.pat_chainFromTo; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFrom : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFrom instance = null;
		public static Rule_chainFrom Instance { get { if (instance==null) { instance = new Rule_chainFrom(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] chainFrom_node_beg_AllowedTypes = null;
		public static bool[] chainFrom_node_beg_IsAllowedType = null;
		public enum chainFrom_NodeNums { @beg, };
		public enum chainFrom_EdgeNums { };
		public enum chainFrom_VariableNums { };
		public enum chainFrom_SubNums { @_sub0, };
		public enum chainFrom_AltNums { };
		public enum chainFrom_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_chainFrom;


		private Rule_chainFrom()
		{
			name = "chainFrom";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFrom_node_beg", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] chainFrom_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFrom_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] chainFrom_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] chainFrom_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode chainFrom_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFrom_node_beg", "beg", chainFrom_node_beg_AllowedTypes, chainFrom_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding chainFrom__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFrom.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("chainFrom_node_beg"),
				}, 
				new string[] { }, new string[] { "chainFrom_node_beg" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_chainFrom = new GRGEN_LGSP.PatternGraph(
				"chainFrom",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { chainFrom_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFrom__sub0 }, 
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
				chainFrom_isNodeHomomorphicGlobal,
				chainFrom_isEdgeHomomorphicGlobal,
				chainFrom_isNodeTotallyHomomorphic,
				chainFrom_isEdgeTotallyHomomorphic
			);

			chainFrom_node_beg.pointOfDefinition = null;
			chainFrom__sub0.PointOfDefinition = pat_chainFrom;

			patternGraph = pat_chainFrom;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_chainFrom curMatch = (Match_chainFrom)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			Pattern_ChainFrom.Match_ChainFrom subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_chainFrom() {
		}

		public interface IMatch_chainFrom : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFrom.Match_ChainFrom @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_chainFrom : GRGEN_LGSP.ListElement<Match_chainFrom>, IMatch_chainFrom
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public enum chainFrom_NodeNums { @beg, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)chainFrom_NodeNums.@beg: return _node_beg;
				default: return null;
				}
			}
			
			public enum chainFrom_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFrom_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFrom.Match_ChainFrom @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFrom.Match_ChainFrom @__sub0;
			public enum chainFrom_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFrom_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum chainFrom_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFrom_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFrom_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_chainFrom.instance.pat_chainFrom; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromComplete : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromComplete instance = null;
		public static Rule_chainFromComplete Instance { get { if (instance==null) { instance = new Rule_chainFromComplete(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] chainFromComplete_node_beg_AllowedTypes = null;
		public static bool[] chainFromComplete_node_beg_IsAllowedType = null;
		public enum chainFromComplete_NodeNums { @beg, };
		public enum chainFromComplete_EdgeNums { };
		public enum chainFromComplete_VariableNums { };
		public enum chainFromComplete_SubNums { @_sub0, };
		public enum chainFromComplete_AltNums { };
		public enum chainFromComplete_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_chainFromComplete;


		private Rule_chainFromComplete()
		{
			name = "chainFromComplete";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromComplete_node_beg", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] chainFromComplete_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFromComplete_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] chainFromComplete_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] chainFromComplete_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode chainFromComplete_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromComplete_node_beg", "beg", chainFromComplete_node_beg_AllowedTypes, chainFromComplete_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding chainFromComplete__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromComplete.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("chainFromComplete_node_beg"),
				}, 
				new string[] { }, new string[] { "chainFromComplete_node_beg" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_chainFromComplete = new GRGEN_LGSP.PatternGraph(
				"chainFromComplete",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { chainFromComplete_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromComplete__sub0 }, 
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
				chainFromComplete_isNodeHomomorphicGlobal,
				chainFromComplete_isEdgeHomomorphicGlobal,
				chainFromComplete_isNodeTotallyHomomorphic,
				chainFromComplete_isEdgeTotallyHomomorphic
			);

			chainFromComplete_node_beg.pointOfDefinition = null;
			chainFromComplete__sub0.PointOfDefinition = pat_chainFromComplete;

			patternGraph = pat_chainFromComplete;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_chainFromComplete curMatch = (Match_chainFromComplete)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			Pattern_ChainFromComplete.Match_ChainFromComplete subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_chainFromComplete() {
		}

		public interface IMatch_chainFromComplete : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromComplete.Match_ChainFromComplete @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_chainFromComplete : GRGEN_LGSP.ListElement<Match_chainFromComplete>, IMatch_chainFromComplete
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public enum chainFromComplete_NodeNums { @beg, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)chainFromComplete_NodeNums.@beg: return _node_beg;
				default: return null;
				}
			}
			
			public enum chainFromComplete_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromComplete_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @__sub0;
			public enum chainFromComplete_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromComplete_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum chainFromComplete_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromComplete_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromComplete_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_chainFromComplete.instance.pat_chainFromComplete; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromCompleteArbitraryPatternpathLocked : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromCompleteArbitraryPatternpathLocked instance = null;
		public static Rule_chainFromCompleteArbitraryPatternpathLocked Instance { get { if (instance==null) { instance = new Rule_chainFromCompleteArbitraryPatternpathLocked(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] chainFromCompleteArbitraryPatternpathLocked_node_beg_AllowedTypes = null;
		public static bool[] chainFromCompleteArbitraryPatternpathLocked_node_beg_IsAllowedType = null;
		public enum chainFromCompleteArbitraryPatternpathLocked_NodeNums { @beg, };
		public enum chainFromCompleteArbitraryPatternpathLocked_EdgeNums { };
		public enum chainFromCompleteArbitraryPatternpathLocked_VariableNums { };
		public enum chainFromCompleteArbitraryPatternpathLocked_SubNums { @_sub0, };
		public enum chainFromCompleteArbitraryPatternpathLocked_AltNums { };
		public enum chainFromCompleteArbitraryPatternpathLocked_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_chainFromCompleteArbitraryPatternpathLocked;


		private Rule_chainFromCompleteArbitraryPatternpathLocked()
		{
			name = "chainFromCompleteArbitraryPatternpathLocked";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromCompleteArbitraryPatternpathLocked_node_beg", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] chainFromCompleteArbitraryPatternpathLocked_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFromCompleteArbitraryPatternpathLocked_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] chainFromCompleteArbitraryPatternpathLocked_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] chainFromCompleteArbitraryPatternpathLocked_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode chainFromCompleteArbitraryPatternpathLocked_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromCompleteArbitraryPatternpathLocked_node_beg", "beg", chainFromCompleteArbitraryPatternpathLocked_node_beg_AllowedTypes, chainFromCompleteArbitraryPatternpathLocked_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding chainFromCompleteArbitraryPatternpathLocked__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("chainFromCompleteArbitraryPatternpathLocked_node_beg"),
				}, 
				new string[] { }, new string[] { "chainFromCompleteArbitraryPatternpathLocked_node_beg" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_chainFromCompleteArbitraryPatternpathLocked = new GRGEN_LGSP.PatternGraph(
				"chainFromCompleteArbitraryPatternpathLocked",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { chainFromCompleteArbitraryPatternpathLocked_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromCompleteArbitraryPatternpathLocked__sub0 }, 
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
				chainFromCompleteArbitraryPatternpathLocked_isNodeHomomorphicGlobal,
				chainFromCompleteArbitraryPatternpathLocked_isEdgeHomomorphicGlobal,
				chainFromCompleteArbitraryPatternpathLocked_isNodeTotallyHomomorphic,
				chainFromCompleteArbitraryPatternpathLocked_isEdgeTotallyHomomorphic
			);

			chainFromCompleteArbitraryPatternpathLocked_node_beg.pointOfDefinition = null;
			chainFromCompleteArbitraryPatternpathLocked__sub0.PointOfDefinition = pat_chainFromCompleteArbitraryPatternpathLocked;

			patternGraph = pat_chainFromCompleteArbitraryPatternpathLocked;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_chainFromCompleteArbitraryPatternpathLocked curMatch = (Match_chainFromCompleteArbitraryPatternpathLocked)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_chainFromCompleteArbitraryPatternpathLocked() {
		}

		public interface IMatch_chainFromCompleteArbitraryPatternpathLocked : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_chainFromCompleteArbitraryPatternpathLocked : GRGEN_LGSP.ListElement<Match_chainFromCompleteArbitraryPatternpathLocked>, IMatch_chainFromCompleteArbitraryPatternpathLocked
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public enum chainFromCompleteArbitraryPatternpathLocked_NodeNums { @beg, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)chainFromCompleteArbitraryPatternpathLocked_NodeNums.@beg: return _node_beg;
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryPatternpathLocked_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryPatternpathLocked_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked @__sub0;
			public enum chainFromCompleteArbitraryPatternpathLocked_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromCompleteArbitraryPatternpathLocked_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryPatternpathLocked_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryPatternpathLocked_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryPatternpathLocked_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_chainFromCompleteArbitraryPatternpathLocked.instance.pat_chainFromCompleteArbitraryPatternpathLocked; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards instance = null;
		public static Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards Instance { get { if (instance==null) { instance = new Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg_AllowedTypes = null;
		public static bool[] chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg_IsAllowedType = null;
		public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_NodeNums { @beg, };
		public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_EdgeNums { };
		public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_VariableNums { };
		public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_SubNums { @_sub0, };
		public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_AltNums { };
		public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;


		private Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards()
		{
			name = "chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg", "beg", chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg_AllowedTypes, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg"),
				}, 
				new string[] { }, new string[] { "chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards = new GRGEN_LGSP.PatternGraph(
				"chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards__sub0 }, 
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
				chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeHomomorphicGlobal,
				chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeHomomorphicGlobal,
				chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isNodeTotallyHomomorphic,
				chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_isEdgeTotallyHomomorphic
			);

			chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.pointOfDefinition = null;
			chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards__sub0.PointOfDefinition = pat_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;

			patternGraph = pat_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards curMatch = (Match_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards() {
		}

		public interface IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LGSP.ListElement<Match_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards>, IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_NodeNums { @beg, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_NodeNums.@beg: return _node_beg;
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards @_sub0 { get { return @__sub0; } }
			public @Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards @__sub0;
			public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.instance.pat_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createBlowball : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createBlowball instance = null;
		public static Rule_createBlowball Instance { get { if (instance==null) { instance = new Rule_createBlowball(); instance.initialize(); } return instance; } }

		public enum createBlowball_NodeNums { };
		public enum createBlowball_EdgeNums { };
		public enum createBlowball_VariableNums { };
		public enum createBlowball_SubNums { };
		public enum createBlowball_AltNums { };
		public enum createBlowball_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_createBlowball;


		private Rule_createBlowball()
		{
			name = "createBlowball";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };

		}
		private void initialize()
		{
			bool[,] createBlowball_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createBlowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] createBlowball_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] createBlowball_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_createBlowball = new GRGEN_LGSP.PatternGraph(
				"createBlowball",
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
				createBlowball_isNodeHomomorphicGlobal,
				createBlowball_isEdgeHomomorphicGlobal,
				createBlowball_isNodeTotallyHomomorphic,
				createBlowball_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_createBlowball;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_createBlowball curMatch = (Match_createBlowball)_curMatch;
			graph.SettingAddedNodeNames( createBlowball_addedNodeNames );
			GRGEN_MODEL.@Node node_head = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node3 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createBlowball_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_head, node__node0);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_head, node__node1);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_head, node__node2);
			GRGEN_MODEL.@Edge edge__edge3 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_head, node__node3);
			output_0 = (GRGEN_LIBGR.INode)(node_head);
			return;
		}
		private static string[] createBlowball_addedNodeNames = new string[] { "head", "_node0", "_node1", "_node2", "_node3" };
		private static string[] createBlowball_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3" };

		static Rule_createBlowball() {
		}

		public interface IMatch_createBlowball : GRGEN_LIBGR.IMatch
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

		public class Match_createBlowball : GRGEN_LGSP.ListElement<Match_createBlowball>, IMatch_createBlowball
		{
			public enum createBlowball_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createBlowball_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createBlowball_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createBlowball_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createBlowball_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createBlowball_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createBlowball_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createBlowball.instance.pat_createBlowball; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_blowball : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_blowball instance = null;
		public static Rule_blowball Instance { get { if (instance==null) { instance = new Rule_blowball(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] blowball_node_head_AllowedTypes = null;
		public static bool[] blowball_node_head_IsAllowedType = null;
		public enum blowball_NodeNums { @head, };
		public enum blowball_EdgeNums { };
		public enum blowball_VariableNums { };
		public enum blowball_SubNums { @_sub0, };
		public enum blowball_AltNums { };
		public enum blowball_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_blowball;


		private Rule_blowball()
		{
			name = "blowball";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "blowball_node_head", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] blowball_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] blowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] blowball_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] blowball_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode blowball_node_head = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "blowball_node_head", "head", blowball_node_head_AllowedTypes, blowball_node_head_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding blowball__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Blowball.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("blowball_node_head"),
				}, 
				new string[] { }, new string[] { "blowball_node_head" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_blowball = new GRGEN_LGSP.PatternGraph(
				"blowball",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { blowball_node_head }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { blowball__sub0 }, 
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
				blowball_isNodeHomomorphicGlobal,
				blowball_isEdgeHomomorphicGlobal,
				blowball_isNodeTotallyHomomorphic,
				blowball_isEdgeTotallyHomomorphic
			);

			blowball_node_head.pointOfDefinition = null;
			blowball__sub0.PointOfDefinition = pat_blowball;

			patternGraph = pat_blowball;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_blowball curMatch = (Match_blowball)_curMatch;
			GRGEN_LGSP.LGSPNode node_head = curMatch._node_head;
			Pattern_Blowball.Match_Blowball subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_blowball() {
		}

		public interface IMatch_blowball : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_head { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_Blowball.Match_Blowball @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_blowball : GRGEN_LGSP.ListElement<Match_blowball>, IMatch_blowball
		{
			public GRGEN_LIBGR.INode node_head { get { return (GRGEN_LIBGR.INode)_node_head; } }
			public GRGEN_LGSP.LGSPNode _node_head;
			public enum blowball_NodeNums { @head, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)blowball_NodeNums.@head: return _node_head;
				default: return null;
				}
			}
			
			public enum blowball_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum blowball_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Blowball.Match_Blowball @_sub0 { get { return @__sub0; } }
			public @Pattern_Blowball.Match_Blowball @__sub0;
			public enum blowball_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)blowball_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum blowball_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum blowball_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum blowball_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_blowball.instance.pat_blowball; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_reverseChainFromTo : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_reverseChainFromTo instance = null;
		public static Rule_reverseChainFromTo Instance { get { if (instance==null) { instance = new Rule_reverseChainFromTo(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] reverseChainFromTo_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] reverseChainFromTo_node_end_AllowedTypes = null;
		public static bool[] reverseChainFromTo_node_beg_IsAllowedType = null;
		public static bool[] reverseChainFromTo_node_end_IsAllowedType = null;
		public enum reverseChainFromTo_NodeNums { @beg, @end, };
		public enum reverseChainFromTo_EdgeNums { };
		public enum reverseChainFromTo_VariableNums { };
		public enum reverseChainFromTo_SubNums { @_sub0, };
		public enum reverseChainFromTo_AltNums { };
		public enum reverseChainFromTo_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_reverseChainFromTo;


		private Rule_reverseChainFromTo()
		{
			name = "reverseChainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "reverseChainFromTo_node_beg", "reverseChainFromTo_node_end", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] reverseChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] reverseChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] reverseChainFromTo_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] reverseChainFromTo_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode reverseChainFromTo_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromTo_node_beg", "beg", reverseChainFromTo_node_beg_AllowedTypes, reverseChainFromTo_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode reverseChainFromTo_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromTo_node_end", "end", reverseChainFromTo_node_end_AllowedTypes, reverseChainFromTo_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding reverseChainFromTo__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ReverseChainFromTo.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("reverseChainFromTo_node_beg"),
					new GRGEN_EXPR.GraphEntityExpression("reverseChainFromTo_node_end"),
				}, 
				new string[] { }, new string[] { "reverseChainFromTo_node_beg", "reverseChainFromTo_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_reverseChainFromTo = new GRGEN_LGSP.PatternGraph(
				"reverseChainFromTo",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { reverseChainFromTo_node_beg, reverseChainFromTo_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { reverseChainFromTo__sub0 }, 
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
				new bool[0, 0] ,
				reverseChainFromTo_isNodeHomomorphicGlobal,
				reverseChainFromTo_isEdgeHomomorphicGlobal,
				reverseChainFromTo_isNodeTotallyHomomorphic,
				reverseChainFromTo_isEdgeTotallyHomomorphic
			);

			reverseChainFromTo_node_beg.pointOfDefinition = null;
			reverseChainFromTo_node_end.pointOfDefinition = null;
			reverseChainFromTo__sub0.PointOfDefinition = pat_reverseChainFromTo;

			patternGraph = pat_reverseChainFromTo;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_reverseChainFromTo curMatch = (Match_reverseChainFromTo)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			Pattern_ReverseChainFromTo.Match_ReverseChainFromTo subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_reverseChainFromTo() {
		}

		public interface IMatch_reverseChainFromTo : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_reverseChainFromTo : GRGEN_LGSP.ListElement<Match_reverseChainFromTo>, IMatch_reverseChainFromTo
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum reverseChainFromTo_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)reverseChainFromTo_NodeNums.@beg: return _node_beg;
				case (int)reverseChainFromTo_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum reverseChainFromTo_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum reverseChainFromTo_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_sub0 { get { return @__sub0; } }
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @__sub0;
			public enum reverseChainFromTo_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)reverseChainFromTo_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum reverseChainFromTo_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum reverseChainFromTo_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum reverseChainFromTo_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_reverseChainFromTo.instance.pat_reverseChainFromTo; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createReverseChain : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createReverseChain instance = null;
		public static Rule_createReverseChain Instance { get { if (instance==null) { instance = new Rule_createReverseChain(); instance.initialize(); } return instance; } }

		public enum createReverseChain_NodeNums { };
		public enum createReverseChain_EdgeNums { };
		public enum createReverseChain_VariableNums { };
		public enum createReverseChain_SubNums { };
		public enum createReverseChain_AltNums { };
		public enum createReverseChain_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_createReverseChain;


		private Rule_createReverseChain()
		{
			name = "createReverseChain";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };

		}
		private void initialize()
		{
			bool[,] createReverseChain_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createReverseChain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] createReverseChain_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] createReverseChain_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_createReverseChain = new GRGEN_LGSP.PatternGraph(
				"createReverseChain",
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
				createReverseChain_isNodeHomomorphicGlobal,
				createReverseChain_isEdgeHomomorphicGlobal,
				createReverseChain_isNodeTotallyHomomorphic,
				createReverseChain_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_createReverseChain;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_createReverseChain curMatch = (Match_createReverseChain)_curMatch;
			graph.SettingAddedNodeNames( createReverseChain_addedNodeNames );
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_beg = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_end = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createReverseChain_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node0, node_beg);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node1, node__node0);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_end, node__node1);
			output_0 = (GRGEN_LIBGR.INode)(node_beg);
			output_1 = (GRGEN_LIBGR.INode)(node_end);
			return;
		}
		private static string[] createReverseChain_addedNodeNames = new string[] { "_node0", "beg", "_node1", "end" };
		private static string[] createReverseChain_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2" };

		static Rule_createReverseChain() {
		}

		public interface IMatch_createReverseChain : GRGEN_LIBGR.IMatch
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

		public class Match_createReverseChain : GRGEN_LGSP.ListElement<Match_createReverseChain>, IMatch_createReverseChain
		{
			public enum createReverseChain_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createReverseChain_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createReverseChain_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createReverseChain_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createReverseChain_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createReverseChain_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createReverseChain_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createReverseChain.instance.pat_createReverseChain; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromToReverse : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromToReverse instance = null;
		public static Rule_chainFromToReverse Instance { get { if (instance==null) { instance = new Rule_chainFromToReverse(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] chainFromToReverse_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] chainFromToReverse_node_end_AllowedTypes = null;
		public static bool[] chainFromToReverse_node_beg_IsAllowedType = null;
		public static bool[] chainFromToReverse_node_end_IsAllowedType = null;
		public enum chainFromToReverse_NodeNums { @beg, @end, };
		public enum chainFromToReverse_EdgeNums { };
		public enum chainFromToReverse_VariableNums { };
		public enum chainFromToReverse_SubNums { @cftr, };
		public enum chainFromToReverse_AltNums { };
		public enum chainFromToReverse_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_chainFromToReverse;


		private Rule_chainFromToReverse()
		{
			name = "chainFromToReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromToReverse_node_beg", "chainFromToReverse_node_end", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] chainFromToReverse_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromToReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] chainFromToReverse_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] chainFromToReverse_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode chainFromToReverse_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverse_node_beg", "beg", chainFromToReverse_node_beg_AllowedTypes, chainFromToReverse_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode chainFromToReverse_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverse_node_end", "end", chainFromToReverse_node_end_AllowedTypes, chainFromToReverse_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding chainFromToReverse_cftr = new GRGEN_LGSP.PatternGraphEmbedding("cftr", Pattern_ChainFromToReverse.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("chainFromToReverse_node_beg"),
					new GRGEN_EXPR.GraphEntityExpression("chainFromToReverse_node_end"),
				}, 
				new string[] { }, new string[] { "chainFromToReverse_node_beg", "chainFromToReverse_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_chainFromToReverse = new GRGEN_LGSP.PatternGraph(
				"chainFromToReverse",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { chainFromToReverse_node_beg, chainFromToReverse_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromToReverse_cftr }, 
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
				new bool[0, 0] ,
				chainFromToReverse_isNodeHomomorphicGlobal,
				chainFromToReverse_isEdgeHomomorphicGlobal,
				chainFromToReverse_isNodeTotallyHomomorphic,
				chainFromToReverse_isEdgeTotallyHomomorphic
			);

			chainFromToReverse_node_beg.pointOfDefinition = null;
			chainFromToReverse_node_end.pointOfDefinition = null;
			chainFromToReverse_cftr.PointOfDefinition = pat_chainFromToReverse;

			patternGraph = pat_chainFromToReverse;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_chainFromToReverse curMatch = (Match_chainFromToReverse)_curMatch;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
			graph.SettingAddedNodeNames( chainFromToReverse_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(procEnv, subpattern_cftr);
			graph.SettingAddedEdgeNames( chainFromToReverse_addedEdgeNames );
			return;
		}
		private static string[] chainFromToReverse_addedNodeNames = new string[] {  };
		private static string[] chainFromToReverse_addedEdgeNames = new string[] {  };

		static Rule_chainFromToReverse() {
		}

		public interface IMatch_chainFromToReverse : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromToReverse.Match_ChainFromToReverse @cftr { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_chainFromToReverse : GRGEN_LGSP.ListElement<Match_chainFromToReverse>, IMatch_chainFromToReverse
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum chainFromToReverse_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)chainFromToReverse_NodeNums.@beg: return _node_beg;
				case (int)chainFromToReverse_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum chainFromToReverse_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromToReverse_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromToReverse.Match_ChainFromToReverse @cftr { get { return @_cftr; } }
			public @Pattern_ChainFromToReverse.Match_ChainFromToReverse @_cftr;
			public enum chainFromToReverse_SubNums { @cftr, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromToReverse_SubNums.@cftr: return _cftr;
				default: return null;
				}
			}
			
			public enum chainFromToReverse_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromToReverse_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromToReverse_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_chainFromToReverse.instance.pat_chainFromToReverse; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromToReverseToCommon : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromToReverseToCommon instance = null;
		public static Rule_chainFromToReverseToCommon Instance { get { if (instance==null) { instance = new Rule_chainFromToReverseToCommon(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] chainFromToReverseToCommon_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] chainFromToReverseToCommon_node_end_AllowedTypes = null;
		public static bool[] chainFromToReverseToCommon_node_beg_IsAllowedType = null;
		public static bool[] chainFromToReverseToCommon_node_end_IsAllowedType = null;
		public enum chainFromToReverseToCommon_NodeNums { @beg, @end, };
		public enum chainFromToReverseToCommon_EdgeNums { };
		public enum chainFromToReverseToCommon_VariableNums { };
		public enum chainFromToReverseToCommon_SubNums { @cftrtc, };
		public enum chainFromToReverseToCommon_AltNums { };
		public enum chainFromToReverseToCommon_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_chainFromToReverseToCommon;


		private Rule_chainFromToReverseToCommon()
		{
			name = "chainFromToReverseToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromToReverseToCommon_node_beg", "chainFromToReverseToCommon_node_end", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };

		}
		private void initialize()
		{
			bool[,] chainFromToReverseToCommon_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromToReverseToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] chainFromToReverseToCommon_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] chainFromToReverseToCommon_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode chainFromToReverseToCommon_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverseToCommon_node_beg", "beg", chainFromToReverseToCommon_node_beg_AllowedTypes, chainFromToReverseToCommon_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode chainFromToReverseToCommon_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverseToCommon_node_end", "end", chainFromToReverseToCommon_node_end_AllowedTypes, chainFromToReverseToCommon_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding chainFromToReverseToCommon_cftrtc = new GRGEN_LGSP.PatternGraphEmbedding("cftrtc", Pattern_ChainFromToReverseToCommon.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("chainFromToReverseToCommon_node_beg"),
					new GRGEN_EXPR.GraphEntityExpression("chainFromToReverseToCommon_node_end"),
				}, 
				new string[] { }, new string[] { "chainFromToReverseToCommon_node_beg", "chainFromToReverseToCommon_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_chainFromToReverseToCommon = new GRGEN_LGSP.PatternGraph(
				"chainFromToReverseToCommon",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromToReverseToCommon_cftrtc }, 
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
				new bool[0, 0] ,
				chainFromToReverseToCommon_isNodeHomomorphicGlobal,
				chainFromToReverseToCommon_isEdgeHomomorphicGlobal,
				chainFromToReverseToCommon_isNodeTotallyHomomorphic,
				chainFromToReverseToCommon_isEdgeTotallyHomomorphic
			);

			chainFromToReverseToCommon_node_beg.pointOfDefinition = null;
			chainFromToReverseToCommon_node_end.pointOfDefinition = null;
			chainFromToReverseToCommon_cftrtc.PointOfDefinition = pat_chainFromToReverseToCommon;

			patternGraph = pat_chainFromToReverseToCommon;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_chainFromToReverseToCommon curMatch = (Match_chainFromToReverseToCommon)_curMatch;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
			graph.SettingAddedNodeNames( chainFromToReverseToCommon_addedNodeNames );
			GRGEN_MODEL.@Node node_common = GRGEN_MODEL.@Node.CreateNode(graph);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(procEnv, subpattern_cftrtc, (GRGEN_LGSP.LGSPNode)(node_common));
			graph.SettingAddedEdgeNames( chainFromToReverseToCommon_addedEdgeNames );
			output_0 = (GRGEN_LIBGR.INode)(node_common);
			return;
		}
		private static string[] chainFromToReverseToCommon_addedNodeNames = new string[] { "common" };
		private static string[] chainFromToReverseToCommon_addedEdgeNames = new string[] {  };

		static Rule_chainFromToReverseToCommon() {
		}

		public interface IMatch_chainFromToReverseToCommon : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon @cftrtc { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_chainFromToReverseToCommon : GRGEN_LGSP.ListElement<Match_chainFromToReverseToCommon>, IMatch_chainFromToReverseToCommon
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum chainFromToReverseToCommon_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)chainFromToReverseToCommon_NodeNums.@beg: return _node_beg;
				case (int)chainFromToReverseToCommon_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum chainFromToReverseToCommon_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromToReverseToCommon_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon @cftrtc { get { return @_cftrtc; } }
			public @Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon @_cftrtc;
			public enum chainFromToReverseToCommon_SubNums { @cftrtc, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromToReverseToCommon_SubNums.@cftrtc: return _cftrtc;
				default: return null;
				}
			}
			
			public enum chainFromToReverseToCommon_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromToReverseToCommon_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum chainFromToReverseToCommon_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_chainFromToReverseToCommon.instance.pat_chainFromToReverseToCommon; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_reverseChainFromToToCommon : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_reverseChainFromToToCommon instance = null;
		public static Rule_reverseChainFromToToCommon Instance { get { if (instance==null) { instance = new Rule_reverseChainFromToToCommon(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] reverseChainFromToToCommon_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] reverseChainFromToToCommon_node_end_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] reverseChainFromToToCommon_node_common_AllowedTypes = null;
		public static bool[] reverseChainFromToToCommon_node_beg_IsAllowedType = null;
		public static bool[] reverseChainFromToToCommon_node_end_IsAllowedType = null;
		public static bool[] reverseChainFromToToCommon_node_common_IsAllowedType = null;
		public enum reverseChainFromToToCommon_NodeNums { @beg, @end, @common, };
		public enum reverseChainFromToToCommon_EdgeNums { };
		public enum reverseChainFromToToCommon_VariableNums { };
		public enum reverseChainFromToToCommon_SubNums { @_sub0, };
		public enum reverseChainFromToToCommon_AltNums { };
		public enum reverseChainFromToToCommon_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_reverseChainFromToToCommon;


		private Rule_reverseChainFromToToCommon()
		{
			name = "reverseChainFromToToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "reverseChainFromToToCommon_node_beg", "reverseChainFromToToCommon_node_end", "reverseChainFromToToCommon_node_common", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] reverseChainFromToToCommon_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] reverseChainFromToToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] reverseChainFromToToCommon_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] reverseChainFromToToCommon_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode reverseChainFromToToCommon_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromToToCommon_node_beg", "beg", reverseChainFromToToCommon_node_beg_AllowedTypes, reverseChainFromToToCommon_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode reverseChainFromToToCommon_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromToToCommon_node_end", "end", reverseChainFromToToCommon_node_end_AllowedTypes, reverseChainFromToToCommon_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode reverseChainFromToToCommon_node_common = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromToToCommon_node_common", "common", reverseChainFromToToCommon_node_common_AllowedTypes, reverseChainFromToToCommon_node_common_IsAllowedType, 5.5F, 2, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternGraphEmbedding reverseChainFromToToCommon__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ReverseChainFromToToCommon.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("reverseChainFromToToCommon_node_beg"),
					new GRGEN_EXPR.GraphEntityExpression("reverseChainFromToToCommon_node_end"),
					new GRGEN_EXPR.GraphEntityExpression("reverseChainFromToToCommon_node_common"),
				}, 
				new string[] { }, new string[] { "reverseChainFromToToCommon_node_beg", "reverseChainFromToToCommon_node_end", "reverseChainFromToToCommon_node_common" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_reverseChainFromToToCommon = new GRGEN_LGSP.PatternGraph(
				"reverseChainFromToToCommon",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { reverseChainFromToToCommon__sub0 }, 
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
				new bool[0, 0] ,
				reverseChainFromToToCommon_isNodeHomomorphicGlobal,
				reverseChainFromToToCommon_isEdgeHomomorphicGlobal,
				reverseChainFromToToCommon_isNodeTotallyHomomorphic,
				reverseChainFromToToCommon_isEdgeTotallyHomomorphic
			);

			reverseChainFromToToCommon_node_beg.pointOfDefinition = null;
			reverseChainFromToToCommon_node_end.pointOfDefinition = null;
			reverseChainFromToToCommon_node_common.pointOfDefinition = null;
			reverseChainFromToToCommon__sub0.PointOfDefinition = pat_reverseChainFromToToCommon;

			patternGraph = pat_reverseChainFromToToCommon;
		}


		public void Modify(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
			Match_reverseChainFromToToCommon curMatch = (Match_reverseChainFromToToCommon)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			GRGEN_LGSP.LGSPNode node_common = curMatch._node_common;
			Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_reverseChainFromToToCommon() {
		}

		public interface IMatch_reverseChainFromToToCommon : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			GRGEN_LIBGR.INode node_common { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_reverseChainFromToToCommon : GRGEN_LGSP.ListElement<Match_reverseChainFromToToCommon>, IMatch_reverseChainFromToToCommon
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LIBGR.INode node_common { get { return (GRGEN_LIBGR.INode)_node_common; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public GRGEN_LGSP.LGSPNode _node_common;
			public enum reverseChainFromToToCommon_NodeNums { @beg, @end, @common, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)reverseChainFromToToCommon_NodeNums.@beg: return _node_beg;
				case (int)reverseChainFromToToCommon_NodeNums.@end: return _node_end;
				case (int)reverseChainFromToToCommon_NodeNums.@common: return _node_common;
				default: return null;
				}
			}
			
			public enum reverseChainFromToToCommon_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum reverseChainFromToToCommon_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_sub0 { get { return @__sub0; } }
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @__sub0;
			public enum reverseChainFromToToCommon_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)reverseChainFromToToCommon_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum reverseChainFromToToCommon_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum reverseChainFromToToCommon_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum reverseChainFromToToCommon_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_reverseChainFromToToCommon.instance.pat_reverseChainFromToToCommon; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Recursive_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public Recursive_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[10];
			rules = new GRGEN_LGSP.LGSPRulePattern[13];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[10+13];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			subpatterns[0] = Pattern_ChainFromTo.Instance;
			rulesAndSubpatterns[0] = Pattern_ChainFromTo.Instance;
			subpatterns[1] = Pattern_ChainFrom.Instance;
			rulesAndSubpatterns[1] = Pattern_ChainFrom.Instance;
			subpatterns[2] = Pattern_ChainFromComplete.Instance;
			rulesAndSubpatterns[2] = Pattern_ChainFromComplete.Instance;
			subpatterns[3] = Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance;
			rulesAndSubpatterns[3] = Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance;
			subpatterns[4] = Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance;
			rulesAndSubpatterns[4] = Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance;
			subpatterns[5] = Pattern_Blowball.Instance;
			rulesAndSubpatterns[5] = Pattern_Blowball.Instance;
			subpatterns[6] = Pattern_ReverseChainFromTo.Instance;
			rulesAndSubpatterns[6] = Pattern_ReverseChainFromTo.Instance;
			subpatterns[7] = Pattern_ChainFromToReverse.Instance;
			rulesAndSubpatterns[7] = Pattern_ChainFromToReverse.Instance;
			subpatterns[8] = Pattern_ChainFromToReverseToCommon.Instance;
			rulesAndSubpatterns[8] = Pattern_ChainFromToReverseToCommon.Instance;
			subpatterns[9] = Pattern_ReverseChainFromToToCommon.Instance;
			rulesAndSubpatterns[9] = Pattern_ReverseChainFromToToCommon.Instance;
			rules[0] = Rule_createChain.Instance;
			rulesAndSubpatterns[10+0] = Rule_createChain.Instance;
			rules[1] = Rule_chainFromTo.Instance;
			rulesAndSubpatterns[10+1] = Rule_chainFromTo.Instance;
			rules[2] = Rule_chainFrom.Instance;
			rulesAndSubpatterns[10+2] = Rule_chainFrom.Instance;
			rules[3] = Rule_chainFromComplete.Instance;
			rulesAndSubpatterns[10+3] = Rule_chainFromComplete.Instance;
			rules[4] = Rule_chainFromCompleteArbitraryPatternpathLocked.Instance;
			rulesAndSubpatterns[10+4] = Rule_chainFromCompleteArbitraryPatternpathLocked.Instance;
			rules[5] = Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance;
			rulesAndSubpatterns[10+5] = Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance;
			rules[6] = Rule_createBlowball.Instance;
			rulesAndSubpatterns[10+6] = Rule_createBlowball.Instance;
			rules[7] = Rule_blowball.Instance;
			rulesAndSubpatterns[10+7] = Rule_blowball.Instance;
			rules[8] = Rule_reverseChainFromTo.Instance;
			rulesAndSubpatterns[10+8] = Rule_reverseChainFromTo.Instance;
			rules[9] = Rule_createReverseChain.Instance;
			rulesAndSubpatterns[10+9] = Rule_createReverseChain.Instance;
			rules[10] = Rule_chainFromToReverse.Instance;
			rulesAndSubpatterns[10+10] = Rule_chainFromToReverse.Instance;
			rules[11] = Rule_chainFromToReverseToCommon.Instance;
			rulesAndSubpatterns[10+11] = Rule_chainFromToReverseToCommon.Instance;
			rules[12] = Rule_reverseChainFromToToCommon.Instance;
			rulesAndSubpatterns[10+12] = Rule_reverseChainFromToToCommon.Instance;
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


    public class PatternAction_ChainFromTo : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromTo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromTo.Instance.patternGraph;
        }

        public static PatternAction_ChainFromTo getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromTo newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromTo(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromTo oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromTo freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromTo next = null;

        public GRGEN_LGSP.LGSPNode ChainFromTo_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromTo_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
            // SubPreset ChainFromTo_node_to 
            GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
            // Push alternative matching task for ChainFromTo_alt_0
            AlternativeAction_ChainFromTo_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromTo_alt_0.getNewTask(procEnv, openTasks, Pattern_ChainFromTo.Instance.patternGraph.alternatives[(int)Pattern_ChainFromTo.ChainFromTo_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromTo_node_from = candidate_ChainFromTo_node_from;
            taskFor_alt_0.ChainFromTo_node_to = candidate_ChainFromTo_node_to;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ChainFromTo_alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromTo_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromTo.Match_ChainFromTo match = new Pattern_ChainFromTo.Match_ChainFromTo();
                    match._node_from = candidate_ChainFromTo_node_from;
                    match._node_to = candidate_ChainFromTo_node_to;
                    match._alt_0 = (Pattern_ChainFromTo.IMatch_ChainFromTo_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ChainFromTo_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromTo_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromTo_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromTo_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromTo_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromTo_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromTo_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromTo_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ChainFromTo_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromTo_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_CaseNums.@base];
                // SubPreset ChainFromTo_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
                // SubPreset ChainFromTo_node_to 
                GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
                // Extend Outgoing ChainFromTo_alt_0_base_edge__edge0 from ChainFromTo_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_node_from.lgspOuthead;
                if(head_candidate_ChainFromTo_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromTo_alt_0_base_edge__edge0 = head_candidate_ChainFromTo_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromTo_alt_0_base_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ChainFromTo_alt_0_base_edge__edge0.lgspTarget != candidate_ChainFromTo_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_ChainFromTo.Match_ChainFromTo_alt_0_base match = new Pattern_ChainFromTo.Match_ChainFromTo_alt_0_base();
                            match._node_from = candidate_ChainFromTo_node_from;
                            match._node_to = candidate_ChainFromTo_node_to;
                            match._edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0;
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
                        prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromTo.Match_ChainFromTo_alt_0_base match = new Pattern_ChainFromTo.Match_ChainFromTo_alt_0_base();
                                match._node_from = candidate_ChainFromTo_node_from;
                                match._node_to = candidate_ChainFromTo_node_to;
                                match._edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0;
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
                                candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromTo_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0.lgspOutNext) != head_candidate_ChainFromTo_alt_0_base_edge__edge0 );
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
            // Alternative case ChainFromTo_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_CaseNums.@rec];
                // SubPreset ChainFromTo_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
                // SubPreset ChainFromTo_node_to 
                GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
                // Extend Outgoing ChainFromTo_alt_0_rec_edge__edge0 from ChainFromTo_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_node_from.lgspOuthead;
                if(head_candidate_ChainFromTo_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromTo_alt_0_rec_edge__edge0 = head_candidate_ChainFromTo_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromTo_alt_0_rec_node_intermediate from ChainFromTo_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromTo_alt_0_rec_node_intermediate = candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspTarget;
                        if((candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0
                            && candidate_ChainFromTo_alt_0_rec_node_intermediate==candidate_ChainFromTo_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0
                        PatternAction_ChainFromTo taskFor__sub0 = PatternAction_ChainFromTo.getNewTask(procEnv, openTasks);
                        taskFor__sub0.ChainFromTo_node_from = candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        taskFor__sub0.ChainFromTo_node_to = candidate_ChainFromTo_node_to;
                        taskFor__sub0.searchPatternpath = false;
                        taskFor__sub0.matchOfNestingPattern = null;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate = candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_ChainFromTo.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromTo.Match_ChainFromTo_alt_0_rec match = new Pattern_ChainFromTo.Match_ChainFromTo_alt_0_rec();
                                match._node_from = candidate_ChainFromTo_node_from;
                                match._node_intermediate = candidate_ChainFromTo_alt_0_rec_node_intermediate;
                                match._node_to = candidate_ChainFromTo_node_to;
                                match._edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0;
                                match.__sub0 = (@Pattern_ChainFromTo.Match_ChainFromTo)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
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
                                candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                                candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                            candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromTo_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0.lgspOutNext) != head_candidate_ChainFromTo_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFrom : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFrom(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFrom.Instance.patternGraph;
        }

        public static PatternAction_ChainFrom getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFrom newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFrom(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ChainFrom oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFrom freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFrom next = null;

        public GRGEN_LGSP.LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFrom_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFrom_node_from = ChainFrom_node_from;
            // Push alternative matching task for ChainFrom_alt_0
            AlternativeAction_ChainFrom_alt_0 taskFor_alt_0 = AlternativeAction_ChainFrom_alt_0.getNewTask(procEnv, openTasks, Pattern_ChainFrom.Instance.patternGraph.alternatives[(int)Pattern_ChainFrom.ChainFrom_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFrom_node_from = candidate_ChainFrom_node_from;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ChainFrom_alt_0
            openTasks.Pop();
            AlternativeAction_ChainFrom_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFrom.Match_ChainFrom match = new Pattern_ChainFrom.Match_ChainFrom();
                    match._node_from = candidate_ChainFrom_node_from;
                    match._alt_0 = (Pattern_ChainFrom.IMatch_ChainFrom_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ChainFrom_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ChainFrom_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFrom_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFrom_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFrom_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFrom_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFrom_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFrom_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFrom_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_CaseNums.@base];
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_ChainFrom.Match_ChainFrom_alt_0_base match = new Pattern_ChainFrom.Match_ChainFrom_alt_0_base();
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
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_ChainFrom.Match_ChainFrom_alt_0_base match = new Pattern_ChainFrom.Match_ChainFrom_alt_0_base();
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
                    continue;
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
            // Alternative case ChainFrom_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_CaseNums.@rec];
                // SubPreset ChainFrom_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFrom_node_from = ChainFrom_node_from;
                // Extend Outgoing ChainFrom_alt_0_rec_edge__edge0 from ChainFrom_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_node_from.lgspOuthead;
                if(head_candidate_ChainFrom_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFrom_alt_0_rec_edge__edge0 = head_candidate_ChainFrom_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFrom_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target ChainFrom_alt_0_rec_node_to from ChainFrom_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFrom_alt_0_rec_node_to = candidate_ChainFrom_alt_0_rec_edge__edge0.lgspTarget;
                        if((candidate_ChainFrom_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0
                        PatternAction_ChainFrom taskFor__sub0 = PatternAction_ChainFrom.getNewTask(procEnv, openTasks);
                        taskFor__sub0.ChainFrom_node_from = candidate_ChainFrom_alt_0_rec_node_to;
                        taskFor__sub0.searchPatternpath = false;
                        taskFor__sub0.matchOfNestingPattern = null;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFrom_alt_0_rec_node_to = candidate_ChainFrom_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFrom_alt_0_rec_node_to.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_ChainFrom.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFrom.Match_ChainFrom_alt_0_rec match = new Pattern_ChainFrom.Match_ChainFrom_alt_0_rec();
                                match._node_from = candidate_ChainFrom_node_from;
                                match._node_to = candidate_ChainFrom_alt_0_rec_node_to;
                                match._edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0;
                                match.__sub0 = (@Pattern_ChainFrom.Match_ChainFrom)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
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
                                candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                                candidate_ChainFrom_alt_0_rec_node_to.lgspFlags = candidate_ChainFrom_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                            candidate_ChainFrom_alt_0_rec_node_to.lgspFlags = candidate_ChainFrom_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFrom_alt_0_rec_node_to.lgspFlags = candidate_ChainFrom_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFrom_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0.lgspOutNext) != head_candidate_ChainFrom_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromComplete : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromComplete(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromComplete.Instance.patternGraph;
        }

        public static PatternAction_ChainFromComplete getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromComplete newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromComplete(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromComplete oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromComplete freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromComplete next = null;

        public GRGEN_LGSP.LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromComplete_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
            // Push alternative matching task for ChainFromComplete_alt_0
            AlternativeAction_ChainFromComplete_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromComplete_alt_0.getNewTask(procEnv, openTasks, Pattern_ChainFromComplete.Instance.patternGraph.alternatives[(int)Pattern_ChainFromComplete.ChainFromComplete_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromComplete_node_from = candidate_ChainFromComplete_node_from;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ChainFromComplete_alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromComplete_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromComplete.Match_ChainFromComplete match = new Pattern_ChainFromComplete.Match_ChainFromComplete();
                    match._node_from = candidate_ChainFromComplete_node_from;
                    match._alt_0 = (Pattern_ChainFromComplete.IMatch_ChainFromComplete_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ChainFromComplete_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromComplete_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromComplete_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromComplete_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromComplete_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromComplete_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromComplete_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromComplete_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromComplete_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_CaseNums.@base];
                // SubPreset ChainFromComplete_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ChainFromComplete_node_from;
                    prev_neg_0__candidate_ChainFromComplete_node_from = candidate_ChainFromComplete_node_from.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ChainFromComplete_node_from.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Outgoing ChainFromComplete_alt_0_base_neg_0_edge__edge0 from ChainFromComplete_node_from 
                    GRGEN_LGSP.LGSPEdge head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_node_from.lgspOuthead;
                    if(head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // Implicit Target ChainFromComplete_alt_0_base_neg_0_node__node0 from ChainFromComplete_alt_0_base_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_alt_0_base_neg_0_node__node0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.lgspTarget;
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ChainFromComplete_node_from.lgspFlags = candidate_ChainFromComplete_node_from.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ChainFromComplete_node_from;
                            --negLevel;
                            goto label0;
                        }
                        while( (candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.lgspOutNext) != head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 );
                    }
                    candidate_ChainFromComplete_node_from.lgspFlags = candidate_ChainFromComplete_node_from.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ChainFromComplete_node_from;
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_base match = new Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_base();
                    match._node_from = candidate_ChainFromComplete_node_from;
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
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_base match = new Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_base();
                        match._node_from = candidate_ChainFromComplete_node_from;
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
                    goto label2;
                }
label0: ;
label1: ;
label2: ;
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
            // Alternative case ChainFromComplete_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_CaseNums.@rec];
                // SubPreset ChainFromComplete_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
                // Extend Outgoing ChainFromComplete_alt_0_rec_edge__edge0 from ChainFromComplete_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_node_from.lgspOuthead;
                if(head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromComplete_alt_0_rec_edge__edge0 = head_candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromComplete_alt_0_rec_node_to from ChainFromComplete_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_alt_0_rec_node_to = candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspTarget;
                        if((candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0
                        PatternAction_ChainFromComplete taskFor__sub0 = PatternAction_ChainFromComplete.getNewTask(procEnv, openTasks);
                        taskFor__sub0.ChainFromComplete_node_from = candidate_ChainFromComplete_alt_0_rec_node_to;
                        taskFor__sub0.searchPatternpath = false;
                        taskFor__sub0.matchOfNestingPattern = null;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to = candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_ChainFromComplete.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_rec match = new Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_rec();
                                match._node_from = candidate_ChainFromComplete_node_from;
                                match._node_to = candidate_ChainFromComplete_alt_0_rec_node_to;
                                match._edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                                match.__sub0 = (@Pattern_ChainFromComplete.Match_ChainFromComplete)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
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
                                candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                                candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags = candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                            candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags = candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags = candidate_ChainFromComplete_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0.lgspOutNext) != head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromCompleteArbitraryPatternpathLocked : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromCompleteArbitraryPatternpathLocked(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance.patternGraph;
        }

        public static PatternAction_ChainFromCompleteArbitraryPatternpathLocked getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromCompleteArbitraryPatternpathLocked newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromCompleteArbitraryPatternpathLocked(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromCompleteArbitraryPatternpathLocked oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromCompleteArbitraryPatternpathLocked freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromCompleteArbitraryPatternpathLocked next = null;

        public GRGEN_LGSP.LGSPNode ChainFromCompleteArbitraryPatternpathLocked_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked patternpath_match_ChainFromCompleteArbitraryPatternpathLocked = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromCompleteArbitraryPatternpathLocked_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from = ChainFromCompleteArbitraryPatternpathLocked_node_from;
            // build match of ChainFromCompleteArbitraryPatternpathLocked for patternpath checks
            if(patternpath_match_ChainFromCompleteArbitraryPatternpathLocked==null) patternpath_match_ChainFromCompleteArbitraryPatternpathLocked = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked();
            patternpath_match_ChainFromCompleteArbitraryPatternpathLocked._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_ChainFromCompleteArbitraryPatternpathLocked._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
            // Push alternative matching task for ChainFromCompleteArbitraryPatternpathLocked_alt_0
            AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0.getNewTask(procEnv, openTasks, Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance.patternGraph.alternatives[(int)Pattern_ChainFromCompleteArbitraryPatternpathLocked.ChainFromCompleteArbitraryPatternpathLocked_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromCompleteArbitraryPatternpathLocked_node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
            taskFor_alt_0.searchPatternpath = searchPatternpath;
            taskFor_alt_0.matchOfNestingPattern = patternpath_match_ChainFromCompleteArbitraryPatternpathLocked;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ChainFromCompleteArbitraryPatternpathLocked_alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked match = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked();
                    match._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                    match._alt_0 = (Pattern_ChainFromCompleteArbitraryPatternpathLocked.IMatch_ChainFromCompleteArbitraryPatternpathLocked_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromCompleteArbitraryPatternpathLocked_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ChainFromCompleteArbitraryPatternpathLocked_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0 patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0 = null;
            Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base = null;
            Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromCompleteArbitraryPatternpathLocked_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromCompleteArbitraryPatternpathLocked.ChainFromCompleteArbitraryPatternpathLocked_alt_0_CaseNums.@base];
                // SubPreset ChainFromCompleteArbitraryPatternpathLocked_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from = ChainFromCompleteArbitraryPatternpathLocked_node_from;
                // build match of ChainFromCompleteArbitraryPatternpathLocked_alt_0_base for patternpath checks
                if(patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base==null) patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base();
                patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base._matchOfEnclosingPattern = matchOfNestingPattern;
                patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                    prev_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // both directions of ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0
                    for(int directionRunCounterOf_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 = 0; directionRunCounterOf_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 < 2; ++directionRunCounterOf_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0)
                    {
                        // Extend IncomingOrOutgoing ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 from ChainFromCompleteArbitraryPatternpathLocked_node_from 
                        GRGEN_LGSP.LGSPEdge head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 = directionRunCounterOf_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0==0 ? candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspInhead : candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspOuthead;
                        if(head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 = head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0;
                            do
                            {
                                if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                                {
                                    continue;
                                }
                                if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0, patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base))
                                {
                                    continue;
                                }
                                // Implicit TheOther ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0 from ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 
                                GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from==candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspSource ? candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspTarget : candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspSource;
                                if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                                {
                                    continue;
                                }
                                if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0, patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base))
                                {
                                    continue;
                                }
                                // build match of ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0 for patternpath checks
                                if(patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0==null) patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0 = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0();
                                patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0._matchOfEnclosingPattern = patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base;
                                patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                                patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0._node__node0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0;
                                patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0._edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0;
                                uint prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0;
                                prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                uint prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0;
                                prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                // negative pattern found
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                                --negLevel;
                                goto label3;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_node__node0;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0;
                            }
                            while( (directionRunCounterOf_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0==0 ? candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspInNext : candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0.lgspOutNext) != head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base_neg_0_edge__edge0 );
                        }
                    }
                    candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base match = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base();
                    match._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
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
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base match = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_base();
                        match._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
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
                    goto label5;
                }
label3: ;
label4: ;
label5: ;
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
            // Alternative case ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromCompleteArbitraryPatternpathLocked.ChainFromCompleteArbitraryPatternpathLocked_alt_0_CaseNums.@rec];
                // SubPreset ChainFromCompleteArbitraryPatternpathLocked_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from = ChainFromCompleteArbitraryPatternpathLocked_node_from;
                // Extend Outgoing ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 from ChainFromCompleteArbitraryPatternpathLocked_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from.lgspOuthead;
                if(head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 = head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to from ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspTarget;
                        if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // build match of ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec for patternpath checks
                        if(patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec==null) patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec();
                        patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec._matchOfEnclosingPattern = matchOfNestingPattern;
                        patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                        patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec._node_to = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                        patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec._edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                        uint prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                        prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        uint prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                        prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        // Push subpattern matching task for _sub0
                        PatternAction_ChainFromCompleteArbitraryPatternpathLocked taskFor__sub0 = PatternAction_ChainFromCompleteArbitraryPatternpathLocked.getNewTask(procEnv, openTasks);
                        taskFor__sub0.ChainFromCompleteArbitraryPatternpathLocked_node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                        taskFor__sub0.searchPatternpath = searchPatternpath;
                        taskFor__sub0.matchOfNestingPattern = patternpath_match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_ChainFromCompleteArbitraryPatternpathLocked.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec match = new Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec();
                                match._node_from = candidate_ChainFromCompleteArbitraryPatternpathLocked_node_from;
                                match._node_to = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                                match._edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                                match.__sub0 = (@Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
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
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                                candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                            candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                            candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                            candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_node_to;
                        candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 = candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0.lgspOutNext) != head_candidate_ChainFromCompleteArbitraryPatternpathLocked_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance.patternGraph;
        }

        public static PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards next = null;

        public GRGEN_LGSP.LGSPNode ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
            // Push alternative matching task for ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0
            AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0.getNewTask(procEnv, openTasks, Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance.patternGraph.alternatives[(int)Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards match = new Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards();
                    match._node_from = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                    match._alt_0 = (Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_CaseNums.@base];
                // SubPreset ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                    prev_neg_0__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // both directions of ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0
                    for(int directionRunCounterOf_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 = 0; directionRunCounterOf_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 < 2; ++directionRunCounterOf_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0)
                    {
                        // Extend IncomingOrOutgoing ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 from ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from 
                        GRGEN_LGSP.LGSPEdge head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 = directionRunCounterOf_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0==0 ? candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspInhead : candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspOuthead;
                        if(head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 = head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0;
                            do
                            {
                                if((candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                                {
                                    continue;
                                }
                                // Implicit TheOther ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0 from ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 
                                GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0 = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from==candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0.lgspSource ? candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0.lgspTarget : candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0.lgspSource;
                                if((candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                if((candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                                {
                                    continue;
                                }
                                // negative pattern found
                                candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                                --negLevel;
                                goto label6;
                            }
                            while( (directionRunCounterOf_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0==0 ? candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0.lgspInNext : candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0.lgspOutNext) != head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base_neg_0_edge__edge0 );
                        }
                    }
                    candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base match = new Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base();
                    match._node_from = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label7;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base match = new Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_base();
                        match._node_from = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
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
                    goto label8;
                }
label6: ;
label7: ;
label8: ;
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
            // Alternative case ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_CaseNums.@rec];
                // SubPreset ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                // Extend Outgoing ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 from ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from.lgspOuthead;
                if(head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 = head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to from ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspTarget;
                        if((candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0
                        PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards taskFor__sub0 = PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.getNewTask(procEnv, openTasks);
                        taskFor__sub0.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to;
                        taskFor__sub0.searchPatternpath = false;
                        taskFor__sub0.matchOfNestingPattern = null;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec match = new Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec();
                                match._node_from = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from;
                                match._node_to = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to;
                                match._edge__edge0 = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0;
                                match.__sub0 = (@Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
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
                                candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0;
                                candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0;
                            candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_node_to;
                        candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 = candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0.lgspOutNext) != head_candidate_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_Blowball : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Blowball(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_Blowball.Instance.patternGraph;
        }

        public static PatternAction_Blowball getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Blowball newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Blowball(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Blowball oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Blowball freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Blowball next = null;

        public GRGEN_LGSP.LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            Pattern_Blowball.Match_Blowball patternpath_match_Blowball = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Blowball_node_head 
            GRGEN_LGSP.LGSPNode candidate_Blowball_node_head = Blowball_node_head;
            // build match of Blowball for patternpath checks
            if(patternpath_match_Blowball==null) patternpath_match_Blowball = new Pattern_Blowball.Match_Blowball();
            patternpath_match_Blowball._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Blowball._node_head = candidate_Blowball_node_head;
            // Push alternative matching task for Blowball_alt_0
            AlternativeAction_Blowball_alt_0 taskFor_alt_0 = AlternativeAction_Blowball_alt_0.getNewTask(procEnv, openTasks, Pattern_Blowball.Instance.patternGraph.alternatives[(int)Pattern_Blowball.Blowball_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.Blowball_node_head = candidate_Blowball_node_head;
            taskFor_alt_0.searchPatternpath = searchPatternpath;
            taskFor_alt_0.matchOfNestingPattern = patternpath_match_Blowball;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for Blowball_alt_0
            openTasks.Pop();
            AlternativeAction_Blowball_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Blowball.Match_Blowball match = new Pattern_Blowball.Match_Blowball();
                    match._node_head = candidate_Blowball_node_head;
                    match._alt_0 = (Pattern_Blowball.IMatch_Blowball_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_Blowball_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_Blowball_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_Blowball_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_Blowball_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_Blowball_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_Blowball_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_Blowball_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_Blowball_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            Pattern_Blowball.Match_Blowball_alt_0_end_neg_0 patternpath_match_Blowball_alt_0_end_neg_0 = null;
            Pattern_Blowball.Match_Blowball_alt_0_end patternpath_match_Blowball_alt_0_end = null;
            Pattern_Blowball.Match_Blowball_alt_0_further patternpath_match_Blowball_alt_0_further = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Blowball_alt_0_end 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@end];
                // SubPreset Blowball_node_head 
                GRGEN_LGSP.LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // build match of Blowball_alt_0_end for patternpath checks
                if(patternpath_match_Blowball_alt_0_end==null) patternpath_match_Blowball_alt_0_end = new Pattern_Blowball.Match_Blowball_alt_0_end();
                patternpath_match_Blowball_alt_0_end._matchOfEnclosingPattern = matchOfNestingPattern;
                patternpath_match_Blowball_alt_0_end._node_head = candidate_Blowball_node_head;
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_Blowball_node_head;
                    prev_neg_0__candidate_Blowball_node_head = candidate_Blowball_node_head.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_Blowball_node_head.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Outgoing Blowball_alt_0_end_neg_0_edge__edge0 from Blowball_node_head 
                    GRGEN_LGSP.LGSPEdge head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_node_head.lgspOuthead;
                    if(head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_Blowball_alt_0_end_neg_0_edge__edge0 = head_candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Blowball_alt_0_end_neg_0_edge__edge0, patternpath_match_Blowball_alt_0_end))
                            {
                                continue;
                            }
                            // Implicit Target Blowball_alt_0_end_neg_0_node__node0 from Blowball_alt_0_end_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_Blowball_alt_0_end_neg_0_node__node0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspTarget;
                            if((candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Blowball_alt_0_end_neg_0_node__node0, patternpath_match_Blowball_alt_0_end))
                            {
                                continue;
                            }
                            // build match of Blowball_alt_0_end_neg_0 for patternpath checks
                            if(patternpath_match_Blowball_alt_0_end_neg_0==null) patternpath_match_Blowball_alt_0_end_neg_0 = new Pattern_Blowball.Match_Blowball_alt_0_end_neg_0();
                            patternpath_match_Blowball_alt_0_end_neg_0._matchOfEnclosingPattern = patternpath_match_Blowball_alt_0_end;
                            patternpath_match_Blowball_alt_0_end_neg_0._node_head = candidate_Blowball_node_head;
                            patternpath_match_Blowball_alt_0_end_neg_0._node__node0 = candidate_Blowball_alt_0_end_neg_0_node__node0;
                            patternpath_match_Blowball_alt_0_end_neg_0._edge__edge0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                            uint prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_node__node0;
                            prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_node__node0 = candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                            candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                            uint prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                            prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                            candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                            // negative pattern found
                            candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags = candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                            candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags = candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_node__node0;
                            candidate_Blowball_node_head.lgspFlags = candidate_Blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_Blowball_node_head;
                            --negLevel;
                            goto label9;
                            candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags = candidate_Blowball_alt_0_end_neg_0_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_node__node0;
                            candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags = candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal_neg_0__candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                        }
                        while( (candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.lgspOutNext) != head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 );
                    }
                    candidate_Blowball_node_head.lgspFlags = candidate_Blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_Blowball_node_head;
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_Blowball.Match_Blowball_alt_0_end match = new Pattern_Blowball.Match_Blowball_alt_0_end();
                    match._node_head = candidate_Blowball_node_head;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label10;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Blowball.Match_Blowball_alt_0_end match = new Pattern_Blowball.Match_Blowball_alt_0_end();
                        match._node_head = candidate_Blowball_node_head;
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
                    goto label11;
                }
label9: ;
label10: ;
label11: ;
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
            // Alternative case Blowball_alt_0_further 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@further];
                // SubPreset Blowball_node_head 
                GRGEN_LGSP.LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // Extend Outgoing Blowball_alt_0_further_edge__edge0 from Blowball_node_head 
                GRGEN_LGSP.LGSPEdge head_candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_node_head.lgspOuthead;
                if(head_candidate_Blowball_alt_0_further_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Blowball_alt_0_further_edge__edge0 = head_candidate_Blowball_alt_0_further_edge__edge0;
                    do
                    {
                        if(candidate_Blowball_alt_0_further_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target Blowball_alt_0_further_node__node0 from Blowball_alt_0_further_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_edge__edge0.lgspTarget;
                        if((candidate_Blowball_alt_0_further_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // build match of Blowball_alt_0_further for patternpath checks
                        if(patternpath_match_Blowball_alt_0_further==null) patternpath_match_Blowball_alt_0_further = new Pattern_Blowball.Match_Blowball_alt_0_further();
                        patternpath_match_Blowball_alt_0_further._matchOfEnclosingPattern = matchOfNestingPattern;
                        patternpath_match_Blowball_alt_0_further._node_head = candidate_Blowball_node_head;
                        patternpath_match_Blowball_alt_0_further._node__node0 = candidate_Blowball_alt_0_further_node__node0;
                        patternpath_match_Blowball_alt_0_further._edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0;
                        uint prevSomeGlobal__candidate_Blowball_alt_0_further_node__node0;
                        prevSomeGlobal__candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_node__node0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        uint prevSomeGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                        prevSomeGlobal__candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        // Push subpattern matching task for _sub0
                        PatternAction_Blowball taskFor__sub0 = PatternAction_Blowball.getNewTask(procEnv, openTasks);
                        taskFor__sub0.Blowball_node_head = candidate_Blowball_node_head;
                        taskFor__sub0.searchPatternpath = searchPatternpath;
                        taskFor__sub0.matchOfNestingPattern = patternpath_match_Blowball_alt_0_further;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                        prevGlobal__candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_Blowball_alt_0_further_node__node0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                        prevGlobal__candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_Blowball_alt_0_further_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_Blowball.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_Blowball.Match_Blowball_alt_0_further match = new Pattern_Blowball.Match_Blowball_alt_0_further();
                                match._node_head = candidate_Blowball_node_head;
                                match._node__node0 = candidate_Blowball_alt_0_further_node__node0;
                                match._edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0;
                                match.__sub0 = (@Pattern_Blowball.Match_Blowball)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
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
                                candidate_Blowball_alt_0_further_edge__edge0.lgspFlags = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                                candidate_Blowball_alt_0_further_node__node0.lgspFlags = candidate_Blowball_alt_0_further_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                                candidate_Blowball_alt_0_further_edge__edge0.lgspFlags = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                                candidate_Blowball_alt_0_further_node__node0.lgspFlags = candidate_Blowball_alt_0_further_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Blowball_alt_0_further_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_Blowball_alt_0_further_edge__edge0.lgspFlags = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                            candidate_Blowball_alt_0_further_node__node0.lgspFlags = candidate_Blowball_alt_0_further_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                            candidate_Blowball_alt_0_further_edge__edge0.lgspFlags = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                            candidate_Blowball_alt_0_further_node__node0.lgspFlags = candidate_Blowball_alt_0_further_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Blowball_alt_0_further_node__node0;
                            continue;
                        }
                        candidate_Blowball_alt_0_further_node__node0.lgspFlags = candidate_Blowball_alt_0_further_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                        candidate_Blowball_alt_0_further_edge__edge0.lgspFlags = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                        candidate_Blowball_alt_0_further_node__node0.lgspFlags = candidate_Blowball_alt_0_further_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Blowball_alt_0_further_node__node0;
                        candidate_Blowball_alt_0_further_edge__edge0.lgspFlags = candidate_Blowball_alt_0_further_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                    }
                    while( (candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.lgspOutNext) != head_candidate_Blowball_alt_0_further_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ReverseChainFromTo : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ReverseChainFromTo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ReverseChainFromTo.Instance.patternGraph;
        }

        public static PatternAction_ReverseChainFromTo getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ReverseChainFromTo newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ReverseChainFromTo(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ReverseChainFromTo oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ReverseChainFromTo freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ReverseChainFromTo next = null;

        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_from;
        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ReverseChainFromTo_node_from 
            GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_from = ReverseChainFromTo_node_from;
            // SubPreset ReverseChainFromTo_node_to 
            GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_to = ReverseChainFromTo_node_to;
            // Push alternative matching task for ReverseChainFromTo_alt_0
            AlternativeAction_ReverseChainFromTo_alt_0 taskFor_alt_0 = AlternativeAction_ReverseChainFromTo_alt_0.getNewTask(procEnv, openTasks, Pattern_ReverseChainFromTo.Instance.patternGraph.alternatives[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ReverseChainFromTo_node_to = candidate_ReverseChainFromTo_node_to;
            taskFor_alt_0.ReverseChainFromTo_node_from = candidate_ReverseChainFromTo_node_from;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ReverseChainFromTo_alt_0
            openTasks.Pop();
            AlternativeAction_ReverseChainFromTo_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ReverseChainFromTo.Match_ReverseChainFromTo match = new Pattern_ReverseChainFromTo.Match_ReverseChainFromTo();
                    match._node_from = candidate_ReverseChainFromTo_node_from;
                    match._node_to = candidate_ReverseChainFromTo_node_to;
                    match._alt_0 = (Pattern_ReverseChainFromTo.IMatch_ReverseChainFromTo_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ReverseChainFromTo_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ReverseChainFromTo_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ReverseChainFromTo_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ReverseChainFromTo_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ReverseChainFromTo_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ReverseChainFromTo_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ReverseChainFromTo_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ReverseChainFromTo_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_to;
        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ReverseChainFromTo_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_CaseNums.@base];
                // SubPreset ReverseChainFromTo_node_to 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_to = ReverseChainFromTo_node_to;
                // SubPreset ReverseChainFromTo_node_from 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_from = ReverseChainFromTo_node_from;
                // Extend Outgoing ReverseChainFromTo_alt_0_base_edge__edge0 from ReverseChainFromTo_node_to 
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_node_to.lgspOuthead;
                if(head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspTarget != candidate_ReverseChainFromTo_node_from) {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_ReverseChainFromTo.Match_ReverseChainFromTo_alt_0_base match = new Pattern_ReverseChainFromTo.Match_ReverseChainFromTo_alt_0_base();
                            match._node_to = candidate_ReverseChainFromTo_node_to;
                            match._node_from = candidate_ReverseChainFromTo_node_from;
                            match._edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
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
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ReverseChainFromTo.Match_ReverseChainFromTo_alt_0_base match = new Pattern_ReverseChainFromTo.Match_ReverseChainFromTo_alt_0_base();
                                match._node_to = candidate_ReverseChainFromTo_node_to;
                                match._node_from = candidate_ReverseChainFromTo_node_from;
                                match._edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
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
                                candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.lgspOutNext) != head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 );
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
            // Alternative case ReverseChainFromTo_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_CaseNums.@rec];
                // SubPreset ReverseChainFromTo_node_from 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_from = ReverseChainFromTo_node_from;
                // SubPreset ReverseChainFromTo_node_to 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_to = ReverseChainFromTo_node_to;
                // Extend Incoming ReverseChainFromTo_alt_0_rec_edge__edge0 from ReverseChainFromTo_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_node_from.lgspInhead;
                if(head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Source ReverseChainFromTo_alt_0_rec_node_intermediate from ReverseChainFromTo_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_alt_0_rec_node_intermediate = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspSource;
                        if((candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0
                            && candidate_ReverseChainFromTo_alt_0_rec_node_intermediate==candidate_ReverseChainFromTo_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0
                        PatternAction_ReverseChainFromTo taskFor__sub0 = PatternAction_ReverseChainFromTo.getNewTask(procEnv, openTasks);
                        taskFor__sub0.ReverseChainFromTo_node_from = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        taskFor__sub0.ReverseChainFromTo_node_to = candidate_ReverseChainFromTo_node_to;
                        taskFor__sub0.searchPatternpath = false;
                        taskFor__sub0.matchOfNestingPattern = null;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_ReverseChainFromTo.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ReverseChainFromTo.Match_ReverseChainFromTo_alt_0_rec match = new Pattern_ReverseChainFromTo.Match_ReverseChainFromTo_alt_0_rec();
                                match._node_intermediate = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                                match._node_from = candidate_ReverseChainFromTo_node_from;
                                match._node_to = candidate_ReverseChainFromTo_node_to;
                                match._edge__edge0 = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                                match.__sub0 = (@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
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
                                candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                                candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                            candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.lgspInNext) != head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromToReverse : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromToReverse(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromToReverse.Instance.patternGraph;
        }

        public static PatternAction_ChainFromToReverse getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromToReverse newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromToReverse(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromToReverse oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromToReverse freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromToReverse next = null;

        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromToReverse_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_from = ChainFromToReverse_node_from;
            // SubPreset ChainFromToReverse_node_to 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_to = ChainFromToReverse_node_to;
            // Push alternative matching task for ChainFromToReverse_alt_0
            AlternativeAction_ChainFromToReverse_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromToReverse_alt_0.getNewTask(procEnv, openTasks, Pattern_ChainFromToReverse.Instance.patternGraph.alternatives[(int)Pattern_ChainFromToReverse.ChainFromToReverse_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromToReverse_node_from = candidate_ChainFromToReverse_node_from;
            taskFor_alt_0.ChainFromToReverse_node_to = candidate_ChainFromToReverse_node_to;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ChainFromToReverse_alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromToReverse_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromToReverse.Match_ChainFromToReverse match = new Pattern_ChainFromToReverse.Match_ChainFromToReverse();
                    match._node_from = candidate_ChainFromToReverse_node_from;
                    match._node_to = candidate_ChainFromToReverse_node_to;
                    match._alt_0 = (Pattern_ChainFromToReverse.IMatch_ChainFromToReverse_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ChainFromToReverse_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromToReverse_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromToReverse_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromToReverse_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromToReverse_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromToReverse_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromToReverse_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromToReverse_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromToReverse_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_CaseNums.@base];
                // SubPreset ChainFromToReverse_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_from = ChainFromToReverse_node_from;
                // SubPreset ChainFromToReverse_node_to 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_to = ChainFromToReverse_node_to;
                // Extend Outgoing ChainFromToReverse_alt_0_base_edge__edge0 from ChainFromToReverse_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_node_from.lgspOuthead;
                if(head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverse_alt_0_base_edge__edge0 = head_candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspTarget != candidate_ChainFromToReverse_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_ChainFromToReverse.Match_ChainFromToReverse_alt_0_base match = new Pattern_ChainFromToReverse.Match_ChainFromToReverse_alt_0_base();
                            match._node_from = candidate_ChainFromToReverse_node_from;
                            match._node_to = candidate_ChainFromToReverse_node_to;
                            match._edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0;
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
                        prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromToReverse.Match_ChainFromToReverse_alt_0_base match = new Pattern_ChainFromToReverse.Match_ChainFromToReverse_alt_0_base();
                                match._node_from = candidate_ChainFromToReverse_node_from;
                                match._node_to = candidate_ChainFromToReverse_node_to;
                                match._edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0;
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
                                candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0.lgspOutNext) != head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 );
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
            // Alternative case ChainFromToReverse_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_CaseNums.@rec];
                // SubPreset ChainFromToReverse_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_from = ChainFromToReverse_node_from;
                // SubPreset ChainFromToReverse_node_to 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_to = ChainFromToReverse_node_to;
                // Extend Outgoing ChainFromToReverse_alt_0_rec_edge__edge0 from ChainFromToReverse_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_node_from.lgspOuthead;
                if(head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromToReverse_alt_0_rec_node_intermediate from ChainFromToReverse_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_alt_0_rec_node_intermediate = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspTarget;
                        if((candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0
                            && candidate_ChainFromToReverse_alt_0_rec_node_intermediate==candidate_ChainFromToReverse_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Push subpattern matching task for cftr
                        PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(procEnv, openTasks);
                        taskFor_cftr.ChainFromToReverse_node_from = candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        taskFor_cftr.ChainFromToReverse_node_to = candidate_ChainFromToReverse_node_to;
                        taskFor_cftr.searchPatternpath = false;
                        taskFor_cftr.matchOfNestingPattern = null;
                        taskFor_cftr.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_cftr);
                        uint prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for cftr
                        openTasks.Pop();
                        PatternAction_ChainFromToReverse.releaseTask(taskFor_cftr);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromToReverse.Match_ChainFromToReverse_alt_0_rec match = new Pattern_ChainFromToReverse.Match_ChainFromToReverse_alt_0_rec();
                                match._node_from = candidate_ChainFromToReverse_node_from;
                                match._node_intermediate = candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                                match._node_to = candidate_ChainFromToReverse_node_to;
                                match._edge__edge0 = candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                                match._cftr = (@Pattern_ChainFromToReverse.Match_ChainFromToReverse)currentFoundPartialMatch.Pop();
                                match._cftr._matchOfEnclosingPattern = match;
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
                                candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                                candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                            candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.lgspOutNext) != head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromToReverseToCommon : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromToReverseToCommon(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromToReverseToCommon.Instance.patternGraph;
        }

        public static PatternAction_ChainFromToReverseToCommon getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromToReverseToCommon newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromToReverseToCommon(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromToReverseToCommon oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromToReverseToCommon freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromToReverseToCommon next = null;

        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromToReverseToCommon_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_from = ChainFromToReverseToCommon_node_from;
            // SubPreset ChainFromToReverseToCommon_node_to 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_to = ChainFromToReverseToCommon_node_to;
            // Push alternative matching task for ChainFromToReverseToCommon_alt_0
            AlternativeAction_ChainFromToReverseToCommon_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromToReverseToCommon_alt_0.getNewTask(procEnv, openTasks, Pattern_ChainFromToReverseToCommon.Instance.patternGraph.alternatives[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromToReverseToCommon_node_from = candidate_ChainFromToReverseToCommon_node_from;
            taskFor_alt_0.ChainFromToReverseToCommon_node_to = candidate_ChainFromToReverseToCommon_node_to;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ChainFromToReverseToCommon_alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromToReverseToCommon_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon match = new Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon();
                    match._node_from = candidate_ChainFromToReverseToCommon_node_from;
                    match._node_to = candidate_ChainFromToReverseToCommon_node_to;
                    match._alt_0 = (Pattern_ChainFromToReverseToCommon.IMatch_ChainFromToReverseToCommon_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ChainFromToReverseToCommon_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromToReverseToCommon_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromToReverseToCommon_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromToReverseToCommon_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromToReverseToCommon_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromToReverseToCommon_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromToReverseToCommon_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromToReverseToCommon_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromToReverseToCommon_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_CaseNums.@base];
                // SubPreset ChainFromToReverseToCommon_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_from = ChainFromToReverseToCommon_node_from;
                // SubPreset ChainFromToReverseToCommon_node_to 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_to = ChainFromToReverseToCommon_node_to;
                // Extend Outgoing ChainFromToReverseToCommon_alt_0_base_edge__edge0 from ChainFromToReverseToCommon_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_node_from.lgspOuthead;
                if(head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspTarget != candidate_ChainFromToReverseToCommon_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon_alt_0_base match = new Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon_alt_0_base();
                            match._node_from = candidate_ChainFromToReverseToCommon_node_from;
                            match._node_to = candidate_ChainFromToReverseToCommon_node_to;
                            match._edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
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
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon_alt_0_base match = new Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon_alt_0_base();
                                match._node_from = candidate_ChainFromToReverseToCommon_node_from;
                                match._node_to = candidate_ChainFromToReverseToCommon_node_to;
                                match._edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
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
                                candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.lgspOutNext) != head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 );
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
            // Alternative case ChainFromToReverseToCommon_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_CaseNums.@rec];
                // SubPreset ChainFromToReverseToCommon_node_from 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_from = ChainFromToReverseToCommon_node_from;
                // SubPreset ChainFromToReverseToCommon_node_to 
                GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_to = ChainFromToReverseToCommon_node_to;
                // Extend Outgoing ChainFromToReverseToCommon_alt_0_rec_edge__edge0 from ChainFromToReverseToCommon_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_node_from.lgspOuthead;
                if(head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromToReverseToCommon_alt_0_rec_node_intermediate from ChainFromToReverseToCommon_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspTarget;
                        if((candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0
                            && candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate==candidate_ChainFromToReverseToCommon_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Push subpattern matching task for cftrtc
                        PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(procEnv, openTasks);
                        taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_ChainFromToReverseToCommon_node_to;
                        taskFor_cftrtc.searchPatternpath = false;
                        taskFor_cftrtc.matchOfNestingPattern = null;
                        taskFor_cftrtc.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_cftrtc);
                        uint prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for cftrtc
                        openTasks.Pop();
                        PatternAction_ChainFromToReverseToCommon.releaseTask(taskFor_cftrtc);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon_alt_0_rec match = new Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon_alt_0_rec();
                                match._node_from = candidate_ChainFromToReverseToCommon_node_from;
                                match._node_intermediate = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                                match._node_to = candidate_ChainFromToReverseToCommon_node_to;
                                match._edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                                match._cftrtc = (@Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon)currentFoundPartialMatch.Pop();
                                match._cftrtc._matchOfEnclosingPattern = match;
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
                                candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                                candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                            candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.lgspOutNext) != head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ReverseChainFromToToCommon : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ReverseChainFromToToCommon(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraph = Pattern_ReverseChainFromToToCommon.Instance.patternGraph;
        }

        public static PatternAction_ReverseChainFromToToCommon getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ReverseChainFromToToCommon newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ReverseChainFromToToCommon(procEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ReverseChainFromToToCommon oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ReverseChainFromToToCommon freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ReverseChainFromToToCommon next = null;

        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_to;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_common;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ReverseChainFromToToCommon_node_from 
            GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_from = ReverseChainFromToToCommon_node_from;
            // SubPreset ReverseChainFromToToCommon_node_to 
            GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_to = ReverseChainFromToToCommon_node_to;
            // SubPreset ReverseChainFromToToCommon_node_common 
            GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_common = ReverseChainFromToToCommon_node_common;
            // Push alternative matching task for ReverseChainFromToToCommon_alt_0
            AlternativeAction_ReverseChainFromToToCommon_alt_0 taskFor_alt_0 = AlternativeAction_ReverseChainFromToToCommon_alt_0.getNewTask(procEnv, openTasks, Pattern_ReverseChainFromToToCommon.Instance.patternGraph.alternatives[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ReverseChainFromToToCommon_node_to = candidate_ReverseChainFromToToCommon_node_to;
            taskFor_alt_0.ReverseChainFromToToCommon_node_from = candidate_ReverseChainFromToToCommon_node_from;
            taskFor_alt_0.ReverseChainFromToToCommon_node_common = candidate_ReverseChainFromToToCommon_node_common;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for ReverseChainFromToToCommon_alt_0
            openTasks.Pop();
            AlternativeAction_ReverseChainFromToToCommon_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon match = new Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon();
                    match._node_from = candidate_ReverseChainFromToToCommon_node_from;
                    match._node_to = candidate_ReverseChainFromToToCommon_node_to;
                    match._node_common = candidate_ReverseChainFromToToCommon_node_common;
                    match._alt_0 = (Pattern_ReverseChainFromToToCommon.IMatch_ReverseChainFromToToCommon_alt_0)currentFoundPartialMatch.Pop();
                    match._alt_0.SetMatchOfEnclosingPattern(match);
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

    public class AlternativeAction_ReverseChainFromToToCommon_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ReverseChainFromToToCommon_alt_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            procEnv = procEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ReverseChainFromToToCommon_alt_0 getNewTask(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ReverseChainFromToToCommon_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.procEnv = procEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ReverseChainFromToToCommon_alt_0(procEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ReverseChainFromToToCommon_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.procEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ReverseChainFromToToCommon_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ReverseChainFromToToCommon_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_to;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_common;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ReverseChainFromToToCommon_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_CaseNums.@base];
                // SubPreset ReverseChainFromToToCommon_node_to 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_to = ReverseChainFromToToCommon_node_to;
                // SubPreset ReverseChainFromToToCommon_node_from 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_from = ReverseChainFromToToCommon_node_from;
                // SubPreset ReverseChainFromToToCommon_node_common 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_common = ReverseChainFromToToCommon_node_common;
                // Extend Outgoing ReverseChainFromToToCommon_alt_0_base_edge__edge0 from ReverseChainFromToToCommon_node_to 
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_node_to.lgspOuthead;
                if(head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspTarget != candidate_ReverseChainFromToToCommon_node_from) {
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        uint prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                        prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        // Extend Outgoing ReverseChainFromToToCommon_alt_0_base_edge__edge1 from ReverseChainFromToToCommon_node_from 
                        GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_node_from.lgspOuthead;
                        if(head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                            do
                            {
                                if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspTarget != candidate_ReverseChainFromToToCommon_node_common) {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon_alt_0_base match = new Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon_alt_0_base();
                                    match._node_to = candidate_ReverseChainFromToToCommon_node_to;
                                    match._node_from = candidate_ReverseChainFromToToCommon_node_from;
                                    match._node_common = candidate_ReverseChainFromToToCommon_node_common;
                                    match._edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                    match._edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon_alt_0_base match = new Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon_alt_0_base();
                                        match._node_to = candidate_ReverseChainFromToToCommon_node_to;
                                        match._node_from = candidate_ReverseChainFromToToCommon_node_from;
                                        match._node_common = candidate_ReverseChainFromToToCommon_node_common;
                                        match._edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        match._edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
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
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                    candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                    continue;
                                }
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                            }
                            while( (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.lgspOutNext) != head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 );
                        }
                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.lgspOutNext) != head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 );
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
            // Alternative case ReverseChainFromToToCommon_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_CaseNums.@rec];
                // SubPreset ReverseChainFromToToCommon_node_from 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_from = ReverseChainFromToToCommon_node_from;
                // SubPreset ReverseChainFromToToCommon_node_common 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_common = ReverseChainFromToToCommon_node_common;
                // SubPreset ReverseChainFromToToCommon_node_to 
                GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_node_to = ReverseChainFromToToCommon_node_to;
                // Extend Incoming ReverseChainFromToToCommon_alt_0_rec_edge__edge0 from ReverseChainFromToToCommon_node_from 
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_node_from.lgspInhead;
                if(head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        uint prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                        prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        // Implicit Source ReverseChainFromToToCommon_alt_0_rec_node_intermediate from ReverseChainFromToToCommon_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspSource;
                        if((candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0
                            && (candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate==candidate_ReverseChainFromToToCommon_node_from
                                || candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate==candidate_ReverseChainFromToToCommon_node_common
                                )
                            )
                        {
                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                            continue;
                        }
                        // Extend Outgoing ReverseChainFromToToCommon_alt_0_rec_edge__edge1 from ReverseChainFromToToCommon_node_from 
                        GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_node_from.lgspOuthead;
                        if(head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                            do
                            {
                                if(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspTarget != candidate_ReverseChainFromToToCommon_node_common) {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                                {
                                    continue;
                                }
                                // Push subpattern matching task for _sub0
                                PatternAction_ReverseChainFromToToCommon taskFor__sub0 = PatternAction_ReverseChainFromToToCommon.getNewTask(procEnv, openTasks);
                                taskFor__sub0.ReverseChainFromToToCommon_node_from = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                taskFor__sub0.ReverseChainFromToToCommon_node_to = candidate_ReverseChainFromToToCommon_node_to;
                                taskFor__sub0.ReverseChainFromToToCommon_node_common = candidate_ReverseChainFromToToCommon_node_common;
                                taskFor__sub0.searchPatternpath = false;
                                taskFor__sub0.matchOfNestingPattern = null;
                                taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                                openTasks.Push(taskFor__sub0);
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Pop subpattern matching task for _sub0
                                openTasks.Pop();
                                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__sub0);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon_alt_0_rec match = new Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon_alt_0_rec();
                                        match._node_intermediate = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                        match._node_from = candidate_ReverseChainFromToToCommon_node_from;
                                        match._node_common = candidate_ReverseChainFromToToCommon_node_common;
                                        match._node_to = candidate_ReverseChainFromToToCommon_node_to;
                                        match._edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        match._edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                        match.__sub0 = (@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon)currentFoundPartialMatch.Pop();
                                        match.__sub0._matchOfEnclosingPattern = match;
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
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                    continue;
                                }
                                candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                            }
                            while( (candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.lgspOutNext) != head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 );
                        }
                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.lgspInNext) != head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_createChain
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_createChain.IMatch_createChain match, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> matches, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max);
    }
    
    public class Action_createChain : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_createChain
    {
        public Action_createChain() {
            _rulePattern = Rule_createChain.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createChain.Match_createChain, Rule_createChain.IMatch_createChain>(this);
        }

        public Rule_createChain _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "createChain"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createChain.Match_createChain, Rule_createChain.IMatch_createChain> matches;

        public static Action_createChain Instance { get { return instance; } }
        private static Action_createChain instance = new Action_createChain();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Rule_createChain.Match_createChain match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_createChain.IMatch_createChain match, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> matches, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
        {
            output_0 = null;
            output_1 = null;
            foreach(Rule_createChain.IMatch_createChain match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0, out output_1);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_createChain.IMatch_createChain match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0, out output_1);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> matches;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain> matches;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            Modify(procEnv, (Rule_createChain.IMatch_createChain)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_createChain.IMatch_createChain>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(ApplyAll(maxMatches, procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(ApplyAll(maxMatches, procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            return ApplyStar(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            return ApplyPlus(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            return ApplyMinMax(procEnv, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_chainFromTo
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromTo.IMatch_chainFromTo match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end);
    }
    
    public class Action_chainFromTo : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_chainFromTo
    {
        public Action_chainFromTo() {
            _rulePattern = Rule_chainFromTo.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromTo.Match_chainFromTo, Rule_chainFromTo.IMatch_chainFromTo>(this);
        }

        public Rule_chainFromTo _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "chainFromTo"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromTo.Match_chainFromTo, Rule_chainFromTo.IMatch_chainFromTo> matches;

        public static Action_chainFromTo Instance { get { return instance; } }
        private static Action_chainFromTo instance = new Action_chainFromTo();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromTo_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromTo_node_beg = (GRGEN_LGSP.LGSPNode)chainFromTo_node_beg;
            uint prev__candidate_chainFromTo_node_beg;
            prev__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_chainFromTo_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Preset chainFromTo_node_end 
            GRGEN_LGSP.LGSPNode candidate_chainFromTo_node_end = (GRGEN_LGSP.LGSPNode)chainFromTo_node_end;
            if((candidate_chainFromTo_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_chainFromTo_node_beg.lgspFlags = candidate_chainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                return matches;
            }
            // Push subpattern matching task for _sub0
            PatternAction_ChainFromTo taskFor__sub0 = PatternAction_ChainFromTo.getNewTask(procEnv, openTasks);
            taskFor__sub0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
            taskFor__sub0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = null;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_chainFromTo_node_beg;
            prevGlobal__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromTo_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            uint prevGlobal__candidate_chainFromTo_node_end;
            prevGlobal__candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromTo_node_end.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_ChainFromTo.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromTo.Match_chainFromTo match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromTo_node_beg;
                    match._node_end = candidate_chainFromTo_node_end;
                    match.__sub0 = (@Pattern_ChainFromTo.Match_ChainFromTo)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromTo_node_end.lgspFlags = candidate_chainFromTo_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromTo_node_end;
                    candidate_chainFromTo_node_beg.lgspFlags = candidate_chainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromTo_node_beg;
                    candidate_chainFromTo_node_beg.lgspFlags = candidate_chainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                    return matches;
                }
                candidate_chainFromTo_node_end.lgspFlags = candidate_chainFromTo_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromTo_node_end;
                candidate_chainFromTo_node_beg.lgspFlags = candidate_chainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromTo_node_beg;
                candidate_chainFromTo_node_beg.lgspFlags = candidate_chainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                return matches;
            }
            candidate_chainFromTo_node_beg.lgspFlags = candidate_chainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromTo_node_beg;
            candidate_chainFromTo_node_end.lgspFlags = candidate_chainFromTo_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromTo_node_end;
            candidate_chainFromTo_node_beg.lgspFlags = candidate_chainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromTo_node_beg, chainFromTo_node_end);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromTo.IMatch_chainFromTo match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> matches)
        {
            foreach(Rule_chainFromTo.IMatch_chainFromTo match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromTo_node_beg, chainFromTo_node_end);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromTo_node_beg, chainFromTo_node_end);
            if(matches.Count <= 0) return false;
            foreach(Rule_chainFromTo.IMatch_chainFromTo match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromTo_node_beg, chainFromTo_node_end);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromTo_node_beg, chainFromTo_node_end);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromTo_node_beg, chainFromTo_node_end);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromTo_node_beg, GRGEN_LIBGR.INode chainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromTo_node_beg, chainFromTo_node_end);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_chainFromTo.IMatch_chainFromTo)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_chainFromTo.IMatch_chainFromTo>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_chainFrom
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFrom_node_beg);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFrom.IMatch_chainFrom match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFrom_node_beg);
    }
    
    public class Action_chainFrom : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_chainFrom
    {
        public Action_chainFrom() {
            _rulePattern = Rule_chainFrom.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFrom.Match_chainFrom, Rule_chainFrom.IMatch_chainFrom>(this);
        }

        public Rule_chainFrom _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "chainFrom"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFrom.Match_chainFrom, Rule_chainFrom.IMatch_chainFrom> matches;

        public static Action_chainFrom Instance { get { return instance; } }
        private static Action_chainFrom instance = new Action_chainFrom();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFrom_node_beg)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFrom_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFrom_node_beg = (GRGEN_LGSP.LGSPNode)chainFrom_node_beg;
            // Push subpattern matching task for _sub0
            PatternAction_ChainFrom taskFor__sub0 = PatternAction_ChainFrom.getNewTask(procEnv, openTasks);
            taskFor__sub0.ChainFrom_node_from = candidate_chainFrom_node_beg;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = null;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_chainFrom_node_beg;
            prevGlobal__candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFrom_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_ChainFrom.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFrom.Match_chainFrom match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFrom_node_beg;
                    match.__sub0 = (@Pattern_ChainFrom.Match_ChainFrom)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFrom_node_beg.lgspFlags = candidate_chainFrom_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFrom_node_beg;
                    return matches;
                }
                candidate_chainFrom_node_beg.lgspFlags = candidate_chainFrom_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFrom_node_beg;
                return matches;
            }
            candidate_chainFrom_node_beg.lgspFlags = candidate_chainFrom_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFrom_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFrom_node_beg);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFrom_node_beg)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFrom_node_beg);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFrom.IMatch_chainFrom match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> matches)
        {
            foreach(Rule_chainFrom.IMatch_chainFrom match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFrom_node_beg);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFrom_node_beg);
            if(matches.Count <= 0) return false;
            foreach(Rule_chainFrom.IMatch_chainFrom match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFrom_node_beg);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFrom_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFrom_node_beg);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFrom_node_beg);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFrom_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFrom_node_beg);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_chainFrom.IMatch_chainFrom)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_chainFrom.IMatch_chainFrom>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_chainFromComplete
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromComplete_node_beg);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromComplete.IMatch_chainFromComplete match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromComplete_node_beg);
    }
    
    public class Action_chainFromComplete : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_chainFromComplete
    {
        public Action_chainFromComplete() {
            _rulePattern = Rule_chainFromComplete.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromComplete.Match_chainFromComplete, Rule_chainFromComplete.IMatch_chainFromComplete>(this);
        }

        public Rule_chainFromComplete _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "chainFromComplete"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromComplete.Match_chainFromComplete, Rule_chainFromComplete.IMatch_chainFromComplete> matches;

        public static Action_chainFromComplete Instance { get { return instance; } }
        private static Action_chainFromComplete instance = new Action_chainFromComplete();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromComplete_node_beg)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromComplete_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromComplete_node_beg = (GRGEN_LGSP.LGSPNode)chainFromComplete_node_beg;
            // Push subpattern matching task for _sub0
            PatternAction_ChainFromComplete taskFor__sub0 = PatternAction_ChainFromComplete.getNewTask(procEnv, openTasks);
            taskFor__sub0.ChainFromComplete_node_from = candidate_chainFromComplete_node_beg;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = null;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_chainFromComplete_node_beg;
            prevGlobal__candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromComplete_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_ChainFromComplete.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromComplete.Match_chainFromComplete match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromComplete_node_beg;
                    match.__sub0 = (@Pattern_ChainFromComplete.Match_ChainFromComplete)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromComplete_node_beg.lgspFlags = candidate_chainFromComplete_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromComplete_node_beg;
                    return matches;
                }
                candidate_chainFromComplete_node_beg.lgspFlags = candidate_chainFromComplete_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromComplete_node_beg;
                return matches;
            }
            candidate_chainFromComplete_node_beg.lgspFlags = candidate_chainFromComplete_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromComplete_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromComplete_node_beg);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromComplete_node_beg)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromComplete_node_beg);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromComplete.IMatch_chainFromComplete match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> matches)
        {
            foreach(Rule_chainFromComplete.IMatch_chainFromComplete match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromComplete_node_beg);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromComplete_node_beg);
            if(matches.Count <= 0) return false;
            foreach(Rule_chainFromComplete.IMatch_chainFromComplete match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromComplete_node_beg);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromComplete_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromComplete_node_beg);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromComplete_node_beg);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromComplete_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromComplete_node_beg);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_chainFromComplete.IMatch_chainFromComplete)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_chainFromComplete.IMatch_chainFromComplete>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_chainFromCompleteArbitraryPatternpathLocked
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg);
    }
    
    public class Action_chainFromCompleteArbitraryPatternpathLocked : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_chainFromCompleteArbitraryPatternpathLocked
    {
        public Action_chainFromCompleteArbitraryPatternpathLocked() {
            _rulePattern = Rule_chainFromCompleteArbitraryPatternpathLocked.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromCompleteArbitraryPatternpathLocked.Match_chainFromCompleteArbitraryPatternpathLocked, Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked>(this);
        }

        public Rule_chainFromCompleteArbitraryPatternpathLocked _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "chainFromCompleteArbitraryPatternpathLocked"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromCompleteArbitraryPatternpathLocked.Match_chainFromCompleteArbitraryPatternpathLocked, Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches;

        public static Action_chainFromCompleteArbitraryPatternpathLocked Instance { get { return instance; } }
        private static Action_chainFromCompleteArbitraryPatternpathLocked instance = new Action_chainFromCompleteArbitraryPatternpathLocked();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            bool searchPatternpath = false;
            Rule_chainFromCompleteArbitraryPatternpathLocked.Match_chainFromCompleteArbitraryPatternpathLocked patternpath_match_chainFromCompleteArbitraryPatternpathLocked = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromCompleteArbitraryPatternpathLocked_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg = (GRGEN_LGSP.LGSPNode)chainFromCompleteArbitraryPatternpathLocked_node_beg;
            // build match of chainFromCompleteArbitraryPatternpathLocked for patternpath checks
            if(patternpath_match_chainFromCompleteArbitraryPatternpathLocked==null) patternpath_match_chainFromCompleteArbitraryPatternpathLocked = new Rule_chainFromCompleteArbitraryPatternpathLocked.Match_chainFromCompleteArbitraryPatternpathLocked();
            patternpath_match_chainFromCompleteArbitraryPatternpathLocked._matchOfEnclosingPattern = null;
            patternpath_match_chainFromCompleteArbitraryPatternpathLocked._node_beg = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
            uint prevSomeGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
            prevSomeGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            // Push subpattern matching task for _sub0
            PatternAction_ChainFromCompleteArbitraryPatternpathLocked taskFor__sub0 = PatternAction_ChainFromCompleteArbitraryPatternpathLocked.getNewTask(procEnv, openTasks);
            taskFor__sub0.ChainFromCompleteArbitraryPatternpathLocked_node_from = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = patternpath_match_chainFromCompleteArbitraryPatternpathLocked;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
            prevGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_ChainFromCompleteArbitraryPatternpathLocked.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromCompleteArbitraryPatternpathLocked.Match_chainFromCompleteArbitraryPatternpathLocked match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
                    match.__sub0 = (@Pattern_ChainFromCompleteArbitraryPatternpathLocked.Match_ChainFromCompleteArbitraryPatternpathLocked)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
                    candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
                    return matches;
                }
                candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
                candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
                return matches;
            }
            candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
            candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_chainFromCompleteArbitraryPatternpathLocked_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromCompleteArbitraryPatternpathLocked_node_beg);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches)
        {
            foreach(Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryPatternpathLocked_node_beg);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromCompleteArbitraryPatternpathLocked_node_beg);
            if(matches.Count <= 0) return false;
            foreach(Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryPatternpathLocked_node_beg);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryPatternpathLocked_node_beg);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryPatternpathLocked_node_beg);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromCompleteArbitraryPatternpathLocked_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryPatternpathLocked_node_beg);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryPatternpathLocked.IMatch_chainFromCompleteArbitraryPatternpathLocked>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
    }
    
    public class Action_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards
    {
        public Action_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards() {
            _rulePattern = Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards, Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards>(this);
        }

        public Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards, Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches;

        public static Action_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards Instance { get { return instance; } }
        private static Action_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards instance = new Action_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg = (GRGEN_LGSP.LGSPNode)chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg;
            // Push subpattern matching task for _sub0
            PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards taskFor__sub0 = PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.getNewTask(procEnv, openTasks);
            taskFor__sub0.ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_from = candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = null;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg;
            prevGlobal__candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg = candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg;
                    match.__sub0 = (@Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg;
                    return matches;
                }
                candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg;
                return matches;
            }
            candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags = candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches)
        {
            foreach(Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
            if(matches.Count <= 0) return false;
            foreach(Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards_node_beg);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.IMatch_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_createBlowball
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_createBlowball.IMatch_createBlowball match, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> matches, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max);
    }
    
    public class Action_createBlowball : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_createBlowball
    {
        public Action_createBlowball() {
            _rulePattern = Rule_createBlowball.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createBlowball.Match_createBlowball, Rule_createBlowball.IMatch_createBlowball>(this);
        }

        public Rule_createBlowball _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "createBlowball"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createBlowball.Match_createBlowball, Rule_createBlowball.IMatch_createBlowball> matches;

        public static Action_createBlowball Instance { get { return instance; } }
        private static Action_createBlowball instance = new Action_createBlowball();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Rule_createBlowball.Match_createBlowball match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_createBlowball.IMatch_createBlowball match, out GRGEN_LIBGR.INode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> matches, out GRGEN_LIBGR.INode output_0)
        {
            output_0 = null;
            foreach(Rule_createBlowball.IMatch_createBlowball match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_createBlowball.IMatch_createBlowball match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> matches;
            GRGEN_LIBGR.INode output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball> matches;
            GRGEN_LIBGR.INode output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_LIBGR.INode output_0; 
            Modify(procEnv, (Rule_createBlowball.IMatch_createBlowball)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_LIBGR.INode output_0; 
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_createBlowball.IMatch_createBlowball>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(procEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(procEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, procEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, procEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            return ApplyStar(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            return ApplyPlus(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            return ApplyMinMax(procEnv, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_blowball
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode blowball_node_head);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_blowball.IMatch_blowball match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode blowball_node_head);
    }
    
    public class Action_blowball : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_blowball
    {
        public Action_blowball() {
            _rulePattern = Rule_blowball.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_blowball.Match_blowball, Rule_blowball.IMatch_blowball>(this);
        }

        public Rule_blowball _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "blowball"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_blowball.Match_blowball, Rule_blowball.IMatch_blowball> matches;

        public static Action_blowball Instance { get { return instance; } }
        private static Action_blowball instance = new Action_blowball();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode blowball_node_head)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            bool searchPatternpath = false;
            Rule_blowball.Match_blowball patternpath_match_blowball = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset blowball_node_head 
            GRGEN_LGSP.LGSPNode candidate_blowball_node_head = (GRGEN_LGSP.LGSPNode)blowball_node_head;
            // build match of blowball for patternpath checks
            if(patternpath_match_blowball==null) patternpath_match_blowball = new Rule_blowball.Match_blowball();
            patternpath_match_blowball._matchOfEnclosingPattern = null;
            patternpath_match_blowball._node_head = candidate_blowball_node_head;
            uint prevSomeGlobal__candidate_blowball_node_head;
            prevSomeGlobal__candidate_blowball_node_head = candidate_blowball_node_head.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            candidate_blowball_node_head.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            // Push subpattern matching task for _sub0
            PatternAction_Blowball taskFor__sub0 = PatternAction_Blowball.getNewTask(procEnv, openTasks);
            taskFor__sub0.Blowball_node_head = candidate_blowball_node_head;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = patternpath_match_blowball;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_blowball_node_head;
            prevGlobal__candidate_blowball_node_head = candidate_blowball_node_head.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_blowball_node_head.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_Blowball.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_blowball.Match_blowball match = matches.GetNextUnfilledPosition();
                    match._node_head = candidate_blowball_node_head;
                    match.__sub0 = (@Pattern_Blowball.Match_Blowball)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_blowball_node_head.lgspFlags = candidate_blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_blowball_node_head;
                    candidate_blowball_node_head.lgspFlags = candidate_blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_blowball_node_head;
                    return matches;
                }
                candidate_blowball_node_head.lgspFlags = candidate_blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_blowball_node_head;
                candidate_blowball_node_head.lgspFlags = candidate_blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_blowball_node_head;
                return matches;
            }
            candidate_blowball_node_head.lgspFlags = candidate_blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_blowball_node_head;
            candidate_blowball_node_head.lgspFlags = candidate_blowball_node_head.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_blowball_node_head;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode blowball_node_head);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode blowball_node_head)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, blowball_node_head);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_blowball.IMatch_blowball match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> matches)
        {
            foreach(Rule_blowball.IMatch_blowball match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, blowball_node_head);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, blowball_node_head);
            if(matches.Count <= 0) return false;
            foreach(Rule_blowball.IMatch_blowball match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, blowball_node_head);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode blowball_node_head)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, blowball_node_head);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, blowball_node_head);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode blowball_node_head)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, blowball_node_head);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_blowball.IMatch_blowball)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_blowball.IMatch_blowball>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_reverseChainFromTo
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_reverseChainFromTo.IMatch_reverseChainFromTo match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end);
    }
    
    public class Action_reverseChainFromTo : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_reverseChainFromTo
    {
        public Action_reverseChainFromTo() {
            _rulePattern = Rule_reverseChainFromTo.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromTo.Match_reverseChainFromTo, Rule_reverseChainFromTo.IMatch_reverseChainFromTo>(this);
        }

        public Rule_reverseChainFromTo _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "reverseChainFromTo"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromTo.Match_reverseChainFromTo, Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches;

        public static Action_reverseChainFromTo Instance { get { return instance; } }
        private static Action_reverseChainFromTo instance = new Action_reverseChainFromTo();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset reverseChainFromTo_node_beg 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromTo_node_beg = (GRGEN_LGSP.LGSPNode)reverseChainFromTo_node_beg;
            uint prev__candidate_reverseChainFromTo_node_beg;
            prev__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_reverseChainFromTo_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Preset reverseChainFromTo_node_end 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromTo_node_end = (GRGEN_LGSP.LGSPNode)reverseChainFromTo_node_end;
            if((candidate_reverseChainFromTo_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_reverseChainFromTo_node_beg.lgspFlags = candidate_reverseChainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                return matches;
            }
            // Push subpattern matching task for _sub0
            PatternAction_ReverseChainFromTo taskFor__sub0 = PatternAction_ReverseChainFromTo.getNewTask(procEnv, openTasks);
            taskFor__sub0.ReverseChainFromTo_node_from = candidate_reverseChainFromTo_node_beg;
            taskFor__sub0.ReverseChainFromTo_node_to = candidate_reverseChainFromTo_node_end;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = null;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_reverseChainFromTo_node_beg;
            prevGlobal__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_reverseChainFromTo_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            uint prevGlobal__candidate_reverseChainFromTo_node_end;
            prevGlobal__candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_reverseChainFromTo_node_end.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_ReverseChainFromTo.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_reverseChainFromTo.Match_reverseChainFromTo match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_reverseChainFromTo_node_beg;
                    match._node_end = candidate_reverseChainFromTo_node_end;
                    match.__sub0 = (@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_reverseChainFromTo_node_end.lgspFlags = candidate_reverseChainFromTo_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromTo_node_end;
                    candidate_reverseChainFromTo_node_beg.lgspFlags = candidate_reverseChainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                    candidate_reverseChainFromTo_node_beg.lgspFlags = candidate_reverseChainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    return matches;
                }
                candidate_reverseChainFromTo_node_end.lgspFlags = candidate_reverseChainFromTo_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromTo_node_end;
                candidate_reverseChainFromTo_node_beg.lgspFlags = candidate_reverseChainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                candidate_reverseChainFromTo_node_beg.lgspFlags = candidate_reverseChainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                return matches;
            }
            candidate_reverseChainFromTo_node_beg.lgspFlags = candidate_reverseChainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromTo_node_beg;
            candidate_reverseChainFromTo_node_end.lgspFlags = candidate_reverseChainFromTo_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromTo_node_end;
            candidate_reverseChainFromTo_node_beg.lgspFlags = candidate_reverseChainFromTo_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, reverseChainFromTo_node_beg, reverseChainFromTo_node_end);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_reverseChainFromTo.IMatch_reverseChainFromTo match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches)
        {
            foreach(Rule_reverseChainFromTo.IMatch_reverseChainFromTo match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromTo_node_beg, reverseChainFromTo_node_end);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, reverseChainFromTo_node_beg, reverseChainFromTo_node_end);
            if(matches.Count <= 0) return false;
            foreach(Rule_reverseChainFromTo.IMatch_reverseChainFromTo match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromTo_node_beg, reverseChainFromTo_node_end);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromTo_node_beg, reverseChainFromTo_node_end);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromTo_node_beg, reverseChainFromTo_node_end);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode reverseChainFromTo_node_beg, GRGEN_LIBGR.INode reverseChainFromTo_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromTo_node_beg, reverseChainFromTo_node_end);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_reverseChainFromTo.IMatch_reverseChainFromTo)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromTo.IMatch_reverseChainFromTo>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_createReverseChain
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_createReverseChain.IMatch_createReverseChain match, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> matches, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max);
    }
    
    public class Action_createReverseChain : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_createReverseChain
    {
        public Action_createReverseChain() {
            _rulePattern = Rule_createReverseChain.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createReverseChain.Match_createReverseChain, Rule_createReverseChain.IMatch_createReverseChain>(this);
        }

        public Rule_createReverseChain _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "createReverseChain"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createReverseChain.Match_createReverseChain, Rule_createReverseChain.IMatch_createReverseChain> matches;

        public static Action_createReverseChain Instance { get { return instance; } }
        private static Action_createReverseChain instance = new Action_createReverseChain();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Rule_createReverseChain.Match_createReverseChain match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_createReverseChain.IMatch_createReverseChain match, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> matches, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
        {
            output_0 = null;
            output_1 = null;
            foreach(Rule_createReverseChain.IMatch_createReverseChain match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0, out output_1);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_createReverseChain.IMatch_createReverseChain match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0, out output_1);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> matches;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain> matches;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            Modify(procEnv, (Rule_createReverseChain.IMatch_createReverseChain)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_createReverseChain.IMatch_createReverseChain>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(ApplyAll(maxMatches, procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(ApplyAll(maxMatches, procEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            return ApplyStar(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            return ApplyPlus(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            return ApplyMinMax(procEnv, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_chainFromToReverse
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromToReverse.IMatch_chainFromToReverse match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end);
    }
    
    public class Action_chainFromToReverse : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_chainFromToReverse
    {
        public Action_chainFromToReverse() {
            _rulePattern = Rule_chainFromToReverse.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverse.Match_chainFromToReverse, Rule_chainFromToReverse.IMatch_chainFromToReverse>(this);
        }

        public Rule_chainFromToReverse _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "chainFromToReverse"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverse.Match_chainFromToReverse, Rule_chainFromToReverse.IMatch_chainFromToReverse> matches;

        public static Action_chainFromToReverse Instance { get { return instance; } }
        private static Action_chainFromToReverse instance = new Action_chainFromToReverse();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromToReverse_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverse_node_beg = (GRGEN_LGSP.LGSPNode)chainFromToReverse_node_beg;
            uint prev__candidate_chainFromToReverse_node_beg;
            prev__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_chainFromToReverse_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Preset chainFromToReverse_node_end 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverse_node_end = (GRGEN_LGSP.LGSPNode)chainFromToReverse_node_end;
            if((candidate_chainFromToReverse_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_chainFromToReverse_node_beg.lgspFlags = candidate_chainFromToReverse_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                return matches;
            }
            // Push subpattern matching task for cftr
            PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(procEnv, openTasks);
            taskFor_cftr.ChainFromToReverse_node_from = candidate_chainFromToReverse_node_beg;
            taskFor_cftr.ChainFromToReverse_node_to = candidate_chainFromToReverse_node_end;
            taskFor_cftr.searchPatternpath = false;
            taskFor_cftr.matchOfNestingPattern = null;
            taskFor_cftr.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_cftr);
            uint prevGlobal__candidate_chainFromToReverse_node_beg;
            prevGlobal__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromToReverse_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            uint prevGlobal__candidate_chainFromToReverse_node_end;
            prevGlobal__candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromToReverse_node_end.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for cftr
            openTasks.Pop();
            PatternAction_ChainFromToReverse.releaseTask(taskFor_cftr);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromToReverse.Match_chainFromToReverse match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromToReverse_node_beg;
                    match._node_end = candidate_chainFromToReverse_node_end;
                    match._cftr = (@Pattern_ChainFromToReverse.Match_ChainFromToReverse)currentFoundPartialMatch.Pop();
                    match._cftr._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromToReverse_node_end.lgspFlags = candidate_chainFromToReverse_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverse_node_end;
                    candidate_chainFromToReverse_node_beg.lgspFlags = candidate_chainFromToReverse_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverse_node_beg;
                    candidate_chainFromToReverse_node_beg.lgspFlags = candidate_chainFromToReverse_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    return matches;
                }
                candidate_chainFromToReverse_node_end.lgspFlags = candidate_chainFromToReverse_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverse_node_end;
                candidate_chainFromToReverse_node_beg.lgspFlags = candidate_chainFromToReverse_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverse_node_beg;
                candidate_chainFromToReverse_node_beg.lgspFlags = candidate_chainFromToReverse_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                return matches;
            }
            candidate_chainFromToReverse_node_beg.lgspFlags = candidate_chainFromToReverse_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverse_node_beg;
            candidate_chainFromToReverse_node_end.lgspFlags = candidate_chainFromToReverse_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverse_node_end;
            candidate_chainFromToReverse_node_beg.lgspFlags = candidate_chainFromToReverse_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromToReverse_node_beg, chainFromToReverse_node_end);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromToReverse.IMatch_chainFromToReverse match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> matches)
        {
            foreach(Rule_chainFromToReverse.IMatch_chainFromToReverse match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverse_node_beg, chainFromToReverse_node_end);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromToReverse_node_beg, chainFromToReverse_node_end);
            if(matches.Count <= 0) return false;
            foreach(Rule_chainFromToReverse.IMatch_chainFromToReverse match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverse_node_beg, chainFromToReverse_node_end);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverse_node_beg, chainFromToReverse_node_end);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverse_node_beg, chainFromToReverse_node_end);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromToReverse_node_beg, GRGEN_LIBGR.INode chainFromToReverse_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverse_node_beg, chainFromToReverse_node_end);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_chainFromToReverse.IMatch_chainFromToReverse)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverse.IMatch_chainFromToReverse>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_chainFromToReverseToCommon
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon match, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end);
    }
    
    public class Action_chainFromToReverseToCommon : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_chainFromToReverseToCommon
    {
        public Action_chainFromToReverseToCommon() {
            _rulePattern = Rule_chainFromToReverseToCommon.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon, Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon>(this);
        }

        public Rule_chainFromToReverseToCommon _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "chainFromToReverseToCommon"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon, Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches;

        public static Action_chainFromToReverseToCommon Instance { get { return instance; } }
        private static Action_chainFromToReverseToCommon instance = new Action_chainFromToReverseToCommon();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromToReverseToCommon_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverseToCommon_node_beg = (GRGEN_LGSP.LGSPNode)chainFromToReverseToCommon_node_beg;
            uint prev__candidate_chainFromToReverseToCommon_node_beg;
            prev__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_chainFromToReverseToCommon_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Preset chainFromToReverseToCommon_node_end 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverseToCommon_node_end = (GRGEN_LGSP.LGSPNode)chainFromToReverseToCommon_node_end;
            if((candidate_chainFromToReverseToCommon_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_chainFromToReverseToCommon_node_beg.lgspFlags = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                return matches;
            }
            // Push subpattern matching task for cftrtc
            PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(procEnv, openTasks);
            taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_chainFromToReverseToCommon_node_beg;
            taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_chainFromToReverseToCommon_node_end;
            taskFor_cftrtc.searchPatternpath = false;
            taskFor_cftrtc.matchOfNestingPattern = null;
            taskFor_cftrtc.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_cftrtc);
            uint prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
            prevGlobal__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromToReverseToCommon_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            uint prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            prevGlobal__candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_chainFromToReverseToCommon_node_end.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for cftrtc
            openTasks.Pop();
            PatternAction_ChainFromToReverseToCommon.releaseTask(taskFor_cftrtc);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromToReverseToCommon_node_beg;
                    match._node_end = candidate_chainFromToReverseToCommon_node_end;
                    match._cftrtc = (@Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon)currentFoundPartialMatch.Pop();
                    match._cftrtc._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromToReverseToCommon_node_end.lgspFlags = candidate_chainFromToReverseToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                    candidate_chainFromToReverseToCommon_node_beg.lgspFlags = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                    candidate_chainFromToReverseToCommon_node_beg.lgspFlags = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    return matches;
                }
                candidate_chainFromToReverseToCommon_node_end.lgspFlags = candidate_chainFromToReverseToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                candidate_chainFromToReverseToCommon_node_beg.lgspFlags = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                candidate_chainFromToReverseToCommon_node_beg.lgspFlags = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                return matches;
            }
            candidate_chainFromToReverseToCommon_node_beg.lgspFlags = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
            candidate_chainFromToReverseToCommon_node_end.lgspFlags = candidate_chainFromToReverseToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            candidate_chainFromToReverseToCommon_node_beg.lgspFlags = candidate_chainFromToReverseToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon match, out GRGEN_LIBGR.INode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches, out GRGEN_LIBGR.INode output_0)
        {
            output_0 = null;
            foreach(Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end);
            if(matches.Count <= 0) return false;
            foreach(Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match, out output_0);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches;
            GRGEN_LIBGR.INode output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_beg, GRGEN_LIBGR.INode chainFromToReverseToCommon_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon> matches;
            GRGEN_LIBGR.INode output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_LIBGR.INode output_0; 
            Modify(procEnv, (Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_LIBGR.INode output_0; 
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_chainFromToReverseToCommon.IMatch_chainFromToReverseToCommon>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_reverseChainFromToToCommon
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common);
    }
    
    public class Action_reverseChainFromToToCommon : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_reverseChainFromToToCommon
    {
        public Action_reverseChainFromToToCommon() {
            _rulePattern = Rule_reverseChainFromToToCommon.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon, Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon>(this);
        }

        public Rule_reverseChainFromToToCommon _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "reverseChainFromToToCommon"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon, Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches;

        public static Action_reverseChainFromToToCommon Instance { get { return instance; } }
        private static Action_reverseChainFromToToCommon instance = new Action_reverseChainFromToToCommon();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> myMatch(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset reverseChainFromToToCommon_node_beg 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_beg = (GRGEN_LGSP.LGSPNode)reverseChainFromToToCommon_node_beg;
            uint prev__candidate_reverseChainFromToToCommon_node_beg;
            prev__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_reverseChainFromToToCommon_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Preset reverseChainFromToToCommon_node_end 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_end = (GRGEN_LGSP.LGSPNode)reverseChainFromToToCommon_node_end;
            if((candidate_reverseChainFromToToCommon_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                return matches;
            }
            uint prev__candidate_reverseChainFromToToCommon_node_end;
            prev__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_reverseChainFromToToCommon_node_end.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Preset reverseChainFromToToCommon_node_common 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_common = (GRGEN_LGSP.LGSPNode)reverseChainFromToToCommon_node_common;
            if((candidate_reverseChainFromToToCommon_node_common.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_reverseChainFromToToCommon_node_end.lgspFlags = candidate_reverseChainFromToToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                return matches;
            }
            // Push subpattern matching task for _sub0
            PatternAction_ReverseChainFromToToCommon taskFor__sub0 = PatternAction_ReverseChainFromToToCommon.getNewTask(procEnv, openTasks);
            taskFor__sub0.ReverseChainFromToToCommon_node_from = candidate_reverseChainFromToToCommon_node_beg;
            taskFor__sub0.ReverseChainFromToToCommon_node_to = candidate_reverseChainFromToToCommon_node_end;
            taskFor__sub0.ReverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = null;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
            prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_reverseChainFromToToCommon_node_beg.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
            prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_reverseChainFromToToCommon_node_end.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_reverseChainFromToToCommon_node_common.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_reverseChainFromToToCommon_node_beg;
                    match._node_end = candidate_reverseChainFromToToCommon_node_end;
                    match._node_common = candidate_reverseChainFromToToCommon_node_common;
                    match.__sub0 = (@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_reverseChainFromToToCommon_node_common.lgspFlags = candidate_reverseChainFromToToCommon_node_common.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.lgspFlags = candidate_reverseChainFromToToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    candidate_reverseChainFromToToCommon_node_end.lgspFlags = candidate_reverseChainFromToToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    return matches;
                }
                candidate_reverseChainFromToToCommon_node_common.lgspFlags = candidate_reverseChainFromToToCommon_node_common.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                candidate_reverseChainFromToToCommon_node_end.lgspFlags = candidate_reverseChainFromToToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                candidate_reverseChainFromToToCommon_node_end.lgspFlags = candidate_reverseChainFromToToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                return matches;
            }
            candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
            candidate_reverseChainFromToToCommon_node_end.lgspFlags = candidate_reverseChainFromToToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
            candidate_reverseChainFromToToCommon_node_common.lgspFlags = candidate_reverseChainFromToToCommon_node_common.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            candidate_reverseChainFromToToCommon_node_end.lgspFlags = candidate_reverseChainFromToToCommon_node_end.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
            candidate_reverseChainFromToToCommon_node_beg.lgspFlags = candidate_reverseChainFromToToCommon_node_beg.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> MatchInvoker(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common);
        }
        public void Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches)
        {
            foreach(Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, maxMatches, reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common);
            if(matches.Count <= 0) return false;
            foreach(Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_beg, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_end, GRGEN_LIBGR.INode reverseChainFromToToCommon_node_common)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, 1, reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int maxMatches, object[] parameters)
        {
            return Match(procEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], (GRGEN_LIBGR.INode) parameters[2]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(procEnv, (Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(procEnv, (GRGEN_LIBGR.IMatchesExact<Rule_reverseChainFromToToCommon.IMatch_reverseChainFromToToCommon>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(Apply(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], (GRGEN_LIBGR.INode) parameters[2])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], (GRGEN_LIBGR.INode) parameters[2])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyStar(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], (GRGEN_LIBGR.INode) parameters[2]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, params object[] parameters)
        {
            return ApplyPlus(procEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], (GRGEN_LIBGR.INode) parameters[2]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(procEnv, min, max, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1], (GRGEN_LIBGR.INode) parameters[2]);
        }
    }


    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class RecursiveActions : GRGEN_LGSP.LGSPActions
    {
        public RecursiveActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public RecursiveActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromTo.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFrom.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromComplete.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Blowball.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ReverseChainFromTo.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromToReverse.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromToReverseToCommon.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ReverseChainFromToToCommon.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_createChain.Instance);
            actions.Add("createChain", (GRGEN_LGSP.LGSPAction) Action_createChain.Instance);
            @createChain = Action_createChain.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromTo.Instance);
            actions.Add("chainFromTo", (GRGEN_LGSP.LGSPAction) Action_chainFromTo.Instance);
            @chainFromTo = Action_chainFromTo.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFrom.Instance);
            actions.Add("chainFrom", (GRGEN_LGSP.LGSPAction) Action_chainFrom.Instance);
            @chainFrom = Action_chainFrom.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromComplete.Instance);
            actions.Add("chainFromComplete", (GRGEN_LGSP.LGSPAction) Action_chainFromComplete.Instance);
            @chainFromComplete = Action_chainFromComplete.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromCompleteArbitraryPatternpathLocked.Instance);
            actions.Add("chainFromCompleteArbitraryPatternpathLocked", (GRGEN_LGSP.LGSPAction) Action_chainFromCompleteArbitraryPatternpathLocked.Instance);
            @chainFromCompleteArbitraryPatternpathLocked = Action_chainFromCompleteArbitraryPatternpathLocked.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance);
            actions.Add("chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards", (GRGEN_LGSP.LGSPAction) Action_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance);
            @chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards = Action_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_createBlowball.Instance);
            actions.Add("createBlowball", (GRGEN_LGSP.LGSPAction) Action_createBlowball.Instance);
            @createBlowball = Action_createBlowball.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_blowball.Instance);
            actions.Add("blowball", (GRGEN_LGSP.LGSPAction) Action_blowball.Instance);
            @blowball = Action_blowball.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_reverseChainFromTo.Instance);
            actions.Add("reverseChainFromTo", (GRGEN_LGSP.LGSPAction) Action_reverseChainFromTo.Instance);
            @reverseChainFromTo = Action_reverseChainFromTo.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_createReverseChain.Instance);
            actions.Add("createReverseChain", (GRGEN_LGSP.LGSPAction) Action_createReverseChain.Instance);
            @createReverseChain = Action_createReverseChain.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromToReverse.Instance);
            actions.Add("chainFromToReverse", (GRGEN_LGSP.LGSPAction) Action_chainFromToReverse.Instance);
            @chainFromToReverse = Action_chainFromToReverse.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromToReverseToCommon.Instance);
            actions.Add("chainFromToReverseToCommon", (GRGEN_LGSP.LGSPAction) Action_chainFromToReverseToCommon.Instance);
            @chainFromToReverseToCommon = Action_chainFromToReverseToCommon.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_reverseChainFromToToCommon.Instance);
            actions.Add("reverseChainFromToToCommon", (GRGEN_LGSP.LGSPAction) Action_reverseChainFromToToCommon.Instance);
            @reverseChainFromToToCommon = Action_reverseChainFromToToCommon.Instance;
            analyzer.ComputeInterPatternRelations();
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ChainFromTo.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ChainFrom.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ChainFromComplete.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ChainFromCompleteArbitraryPatternpathLocked.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ChainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_Blowball.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ReverseChainFromTo.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ChainFromToReverse.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ChainFromToReverseToCommon.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_ReverseChainFromToToCommon.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_createChain.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_chainFromTo.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_chainFrom.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_chainFromComplete.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_chainFromCompleteArbitraryPatternpathLocked.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_createBlowball.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_blowball.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_reverseChainFromTo.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_createReverseChain.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_chainFromToReverse.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_chainFromToReverseToCommon.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_reverseChainFromToToCommon.Instance);
        }
        
        public IAction_createChain @createChain;
        public IAction_chainFromTo @chainFromTo;
        public IAction_chainFrom @chainFrom;
        public IAction_chainFromComplete @chainFromComplete;
        public IAction_chainFromCompleteArbitraryPatternpathLocked @chainFromCompleteArbitraryPatternpathLocked;
        public IAction_chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards @chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards;
        public IAction_createBlowball @createBlowball;
        public IAction_blowball @blowball;
        public IAction_reverseChainFromTo @reverseChainFromTo;
        public IAction_createReverseChain @createReverseChain;
        public IAction_chainFromToReverse @chainFromToReverse;
        public IAction_chainFromToReverseToCommon @chainFromToReverseToCommon;
        public IAction_reverseChainFromToToCommon @reverseChainFromToToCommon;
        
        
        public override string Name { get { return "RecursiveActions"; } }
        public override string ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}