// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\Recursive\Recursive.grg" on Sun Jan 25 17:24:50 CET 2009

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_Recursive
{
	public class Pattern_ChainFromTo : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromTo instance = null;
		public static Pattern_ChainFromTo Instance { get { if (instance==null) { instance = new Pattern_ChainFromTo(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] ChainFromTo_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ChainFromTo_node_to_AllowedTypes = null;
		public static bool[] ChainFromTo_node_from_IsAllowedType = null;
		public static bool[] ChainFromTo_node_to_IsAllowedType = null;
		public enum ChainFromTo_NodeNums { @from, @to, };
		public enum ChainFromTo_EdgeNums { };
		public enum ChainFromTo_VariableNums { };
		public enum ChainFromTo_SubNums { };
		public enum ChainFromTo_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_ChainFromTo;

		public enum ChainFromTo_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ChainFromTo_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromTo_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_base_VariableNums { };
		public enum ChainFromTo_alt_0_base_SubNums { };
		public enum ChainFromTo_alt_0_base_AltNums { };


		GRGEN_LGSP.PatternGraph ChainFromTo_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromTo_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_rec_VariableNums { };
		public enum ChainFromTo_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFromTo_alt_0_rec_AltNums { };


		GRGEN_LGSP.PatternGraph ChainFromTo_alt_0_rec;


		private Pattern_ChainFromTo()
		{
			name = "ChainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromTo_node_from", "ChainFromTo_node_to", };
		}
		private void initialize()
		{
			bool[,] ChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode ChainFromTo_node_from = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromTo_node_from", "from", ChainFromTo_node_from_AllowedTypes, ChainFromTo_node_from_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ChainFromTo_node_to = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromTo_node_to", "to", ChainFromTo_node_to_AllowedTypes, ChainFromTo_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ChainFromTo_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromTo_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge ChainFromTo_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromTo_alt_0_base_edge__edge0", "_edge0", ChainFromTo_alt_0_base_edge__edge0_AllowedTypes, ChainFromTo_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromTo_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromTo_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromTo_alt_0_base_edge__edge0 }, 
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
			GRGEN_LGSP.PatternNode ChainFromTo_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromTo_alt_0_rec_node_intermediate", "intermediate", ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes, ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ChainFromTo_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromTo_alt_0_rec_edge__edge0", "_edge0", ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes, ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromTo_alt_0_rec__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ChainFromTo.Instance, new GRGEN_LGSP.PatternElement[] { ChainFromTo_alt_0_rec_node_intermediate, ChainFromTo_node_to });
			ChainFromTo_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromTo_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromTo_node_from, ChainFromTo_alt_0_rec_node_intermediate, ChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromTo_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromTo_alt_0_rec__subpattern0 }, 
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
				ChainFromTo_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromTo_alt_0_rec.edgeToSourceNode.Add(ChainFromTo_alt_0_rec_edge__edge0, ChainFromTo_node_from);
			ChainFromTo_alt_0_rec.edgeToTargetNode.Add(ChainFromTo_alt_0_rec_edge__edge0, ChainFromTo_alt_0_rec_node_intermediate);

			GRGEN_LGSP.Alternative ChainFromTo_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromTo_", new GRGEN_LGSP.PatternGraph[] { ChainFromTo_alt_0_base, ChainFromTo_alt_0_rec } );

			pat_ChainFromTo = new GRGEN_LGSP.PatternGraph(
				"ChainFromTo",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromTo_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void ChainFromTo_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ChainFromTo_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromTo_addedEdgeNames );
		}
		private static String[] create_ChainFromTo_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromTo_addedEdgeNames = new String[] {  };

		public void ChainFromTo_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromTo curMatch)
		{
			IMatch_ChainFromTo_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromTo_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromTo_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromTo_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromTo_alt_0_base) {
				ChainFromTo_alt_0_base_Delete(graph, (Match_ChainFromTo_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromTo_alt_0_rec) {
				ChainFromTo_alt_0_rec_Delete(graph, (Match_ChainFromTo_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromTo_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromTo_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromTo_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
		}
		private static String[] create_ChainFromTo_alt_0_base_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFromTo_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromTo_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromTo_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
		}

		public void ChainFromTo_alt_0_rec_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ChainFromTo_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromTo_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromTo.Match_ChainFromTo subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ChainFromTo.Instance.ChainFromTo_Delete(graph, subpattern__subpattern0);
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
			//Independents
		}

		public interface IMatch_ChainFromTo_alt_0 : GRGEN_LIBGR.IMatch
		{
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
			//Independents
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
			@Pattern_ChainFromTo.Match_ChainFromTo @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ChainFromTo.Match_ChainFromTo @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ChainFromTo.Match_ChainFromTo @__subpattern0;
			public enum ChainFromTo_alt_0_rec_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromTo_alt_0_rec_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFrom : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFrom instance = null;
		public static Pattern_ChainFrom Instance { get { if (instance==null) { instance = new Pattern_ChainFrom(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] ChainFrom_node_from_AllowedTypes = null;
		public static bool[] ChainFrom_node_from_IsAllowedType = null;
		public enum ChainFrom_NodeNums { @from, };
		public enum ChainFrom_EdgeNums { };
		public enum ChainFrom_VariableNums { };
		public enum ChainFrom_SubNums { };
		public enum ChainFrom_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_ChainFrom;

		public enum ChainFrom_alt_0_CaseNums { @base, @rec, };
		public enum ChainFrom_alt_0_base_NodeNums { };
		public enum ChainFrom_alt_0_base_EdgeNums { };
		public enum ChainFrom_alt_0_base_VariableNums { };
		public enum ChainFrom_alt_0_base_SubNums { };
		public enum ChainFrom_alt_0_base_AltNums { };


		GRGEN_LGSP.PatternGraph ChainFrom_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFrom_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_node_to_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFrom_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFrom_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFrom_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFrom_alt_0_rec_VariableNums { };
		public enum ChainFrom_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFrom_alt_0_rec_AltNums { };


		GRGEN_LGSP.PatternGraph ChainFrom_alt_0_rec;


		private Pattern_ChainFrom()
		{
			name = "ChainFrom";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFrom_node_from", };
		}
		private void initialize()
		{
			bool[,] ChainFrom_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFrom_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode ChainFrom_node_from = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFrom_node_from", "from", ChainFrom_node_from_AllowedTypes, ChainFrom_node_from_IsAllowedType, 5.5F, 0);
			bool[,] ChainFrom_alt_0_base_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] ChainFrom_alt_0_base_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			ChainFrom_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFrom_alt_0_",
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
			GRGEN_LGSP.PatternNode ChainFrom_alt_0_rec_node_to = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFrom_alt_0_rec_node_to", "to", ChainFrom_alt_0_rec_node_to_AllowedTypes, ChainFrom_alt_0_rec_node_to_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ChainFrom_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFrom_alt_0_rec_edge__edge0", "_edge0", ChainFrom_alt_0_rec_edge__edge0_AllowedTypes, ChainFrom_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ChainFrom_alt_0_rec__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ChainFrom.Instance, new GRGEN_LGSP.PatternElement[] { ChainFrom_alt_0_rec_node_to });
			ChainFrom_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFrom_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFrom_node_from, ChainFrom_alt_0_rec_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFrom_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFrom_alt_0_rec__subpattern0 }, 
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
				ChainFrom_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFrom_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFrom_alt_0_rec.edgeToSourceNode.Add(ChainFrom_alt_0_rec_edge__edge0, ChainFrom_node_from);
			ChainFrom_alt_0_rec.edgeToTargetNode.Add(ChainFrom_alt_0_rec_edge__edge0, ChainFrom_alt_0_rec_node_to);

			GRGEN_LGSP.Alternative ChainFrom_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFrom_", new GRGEN_LGSP.PatternGraph[] { ChainFrom_alt_0_base, ChainFrom_alt_0_rec } );

			pat_ChainFrom = new GRGEN_LGSP.PatternGraph(
				"ChainFrom",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFrom_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFrom_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void ChainFrom_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_from)
		{
			graph.SettingAddedNodeNames( create_ChainFrom_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFrom_addedEdgeNames );
		}
		private static String[] create_ChainFrom_addedNodeNames = new String[] {  };
		private static String[] create_ChainFrom_addedEdgeNames = new String[] {  };

		public void ChainFrom_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFrom curMatch)
		{
			IMatch_ChainFrom_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFrom_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFrom_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFrom_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFrom_alt_0_base) {
				ChainFrom_alt_0_base_Delete(graph, (Match_ChainFrom_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFrom_alt_0_rec) {
				ChainFrom_alt_0_rec_Delete(graph, (Match_ChainFrom_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFrom_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFrom_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFrom_alt_0_base_addedEdgeNames );
		}
		private static String[] create_ChainFrom_alt_0_base_addedNodeNames = new String[] {  };
		private static String[] create_ChainFrom_alt_0_base_addedEdgeNames = new String[] {  };

		public void ChainFrom_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFrom_alt_0_base curMatch)
		{
		}

		public void ChainFrom_alt_0_rec_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ChainFrom_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFrom_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFrom.Match_ChainFrom subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFrom.Instance.ChainFrom_Delete(graph, subpattern__subpattern0);
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
			//Independents
		}

		public interface IMatch_ChainFrom_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_ChainFrom_alt_0_base : IMatch_ChainFrom_alt_0
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			@Pattern_ChainFrom.Match_ChainFrom @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ChainFrom.Match_ChainFrom @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ChainFrom.Match_ChainFrom @__subpattern0;
			public enum ChainFrom_alt_0_rec_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFrom_alt_0_rec_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromComplete : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromComplete instance = null;
		public static Pattern_ChainFromComplete Instance { get { if (instance==null) { instance = new Pattern_ChainFromComplete(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] ChainFromComplete_node_from_AllowedTypes = null;
		public static bool[] ChainFromComplete_node_from_IsAllowedType = null;
		public enum ChainFromComplete_NodeNums { @from, };
		public enum ChainFromComplete_EdgeNums { };
		public enum ChainFromComplete_VariableNums { };
		public enum ChainFromComplete_SubNums { };
		public enum ChainFromComplete_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_ChainFromComplete;

		public enum ChainFromComplete_alt_0_CaseNums { @base, @rec, };
		public enum ChainFromComplete_alt_0_base_NodeNums { @from, };
		public enum ChainFromComplete_alt_0_base_EdgeNums { };
		public enum ChainFromComplete_alt_0_base_VariableNums { };
		public enum ChainFromComplete_alt_0_base_SubNums { };
		public enum ChainFromComplete_alt_0_base_AltNums { };


		GRGEN_LGSP.PatternGraph ChainFromComplete_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_base_neg_0_NodeNums { @from, @_node0, };
		public enum ChainFromComplete_alt_0_base_neg_0_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_base_neg_0_VariableNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_SubNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph ChainFromComplete_alt_0_base_neg_0;

		public static GRGEN_LIBGR.NodeType[] ChainFromComplete_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_node_to_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFromComplete_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_rec_VariableNums { };
		public enum ChainFromComplete_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFromComplete_alt_0_rec_AltNums { };


		GRGEN_LGSP.PatternGraph ChainFromComplete_alt_0_rec;


		private Pattern_ChainFromComplete()
		{
			name = "ChainFromComplete";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromComplete_node_from", };
		}
		private void initialize()
		{
			bool[,] ChainFromComplete_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromComplete_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode ChainFromComplete_node_from = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromComplete_node_from", "from", ChainFromComplete_node_from_AllowedTypes, ChainFromComplete_node_from_IsAllowedType, 5.5F, 0);
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
			GRGEN_LGSP.PatternNode ChainFromComplete_alt_0_base_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromComplete_alt_0_base_neg_0_node__node0", "_node0", ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ChainFromComplete_alt_0_base_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromComplete_alt_0_base_neg_0_edge__edge0", "_edge0", ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromComplete_alt_0_base_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ChainFromComplete_alt_0_base_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_base_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromComplete_alt_0_base_neg_0_edge__edge0 }, 
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
				ChainFromComplete_alt_0_base_neg_0_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_base_neg_0_isEdgeHomomorphicGlobal
			);
			ChainFromComplete_alt_0_base_neg_0.edgeToSourceNode.Add(ChainFromComplete_alt_0_base_neg_0_edge__edge0, ChainFromComplete_node_from);
			ChainFromComplete_alt_0_base_neg_0.edgeToTargetNode.Add(ChainFromComplete_alt_0_base_neg_0_edge__edge0, ChainFromComplete_alt_0_base_neg_0_node__node0);

			ChainFromComplete_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromComplete_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ChainFromComplete_alt_0_base_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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
			GRGEN_LGSP.PatternNode ChainFromComplete_alt_0_rec_node_to = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromComplete_alt_0_rec_node_to", "to", ChainFromComplete_alt_0_rec_node_to_AllowedTypes, ChainFromComplete_alt_0_rec_node_to_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ChainFromComplete_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromComplete_alt_0_rec_edge__edge0", "_edge0", ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromComplete_alt_0_rec__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ChainFromComplete.Instance, new GRGEN_LGSP.PatternElement[] { ChainFromComplete_alt_0_rec_node_to });
			ChainFromComplete_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromComplete_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_rec_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromComplete_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromComplete_alt_0_rec__subpattern0 }, 
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
				ChainFromComplete_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromComplete_alt_0_rec.edgeToSourceNode.Add(ChainFromComplete_alt_0_rec_edge__edge0, ChainFromComplete_node_from);
			ChainFromComplete_alt_0_rec.edgeToTargetNode.Add(ChainFromComplete_alt_0_rec_edge__edge0, ChainFromComplete_alt_0_rec_node_to);

			GRGEN_LGSP.Alternative ChainFromComplete_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromComplete_", new GRGEN_LGSP.PatternGraph[] { ChainFromComplete_alt_0_base, ChainFromComplete_alt_0_rec } );

			pat_ChainFromComplete = new GRGEN_LGSP.PatternGraph(
				"ChainFromComplete",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromComplete_node_from }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromComplete_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void ChainFromComplete_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_from)
		{
			graph.SettingAddedNodeNames( create_ChainFromComplete_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromComplete_addedEdgeNames );
		}
		private static String[] create_ChainFromComplete_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromComplete_addedEdgeNames = new String[] {  };

		public void ChainFromComplete_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromComplete curMatch)
		{
			IMatch_ChainFromComplete_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromComplete_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromComplete_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromComplete_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromComplete_alt_0_base) {
				ChainFromComplete_alt_0_base_Delete(graph, (Match_ChainFromComplete_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromComplete_alt_0_rec) {
				ChainFromComplete_alt_0_rec_Delete(graph, (Match_ChainFromComplete_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromComplete_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromComplete_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromComplete_alt_0_base_addedEdgeNames );
		}
		private static String[] create_ChainFromComplete_alt_0_base_addedNodeNames = new String[] { "from" };
		private static String[] create_ChainFromComplete_alt_0_base_addedEdgeNames = new String[] {  };

		public void ChainFromComplete_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromComplete_alt_0_base curMatch)
		{
		}

		public void ChainFromComplete_alt_0_rec_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ChainFromComplete_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromComplete_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromComplete.Match_ChainFromComplete subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromComplete.Instance.ChainFromComplete_Delete(graph, subpattern__subpattern0);
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
			//Independents
		}

		public interface IMatch_ChainFromComplete_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_ChainFromComplete_alt_0_base : IMatch_ChainFromComplete_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_from { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			//Independents
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
			@Pattern_ChainFromComplete.Match_ChainFromComplete @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @__subpattern0;
			public enum ChainFromComplete_alt_0_rec_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ChainFromComplete_alt_0_rec_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Blowball : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Blowball instance = null;
		public static Pattern_Blowball Instance { get { if (instance==null) { instance = new Pattern_Blowball(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] Blowball_node_head_AllowedTypes = null;
		public static bool[] Blowball_node_head_IsAllowedType = null;
		public enum Blowball_NodeNums { @head, };
		public enum Blowball_EdgeNums { };
		public enum Blowball_VariableNums { };
		public enum Blowball_SubNums { };
		public enum Blowball_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_Blowball;

		public enum Blowball_alt_0_CaseNums { @end, @further, };
		public enum Blowball_alt_0_end_NodeNums { @head, };
		public enum Blowball_alt_0_end_EdgeNums { };
		public enum Blowball_alt_0_end_VariableNums { };
		public enum Blowball_alt_0_end_SubNums { };
		public enum Blowball_alt_0_end_AltNums { };


		GRGEN_LGSP.PatternGraph Blowball_alt_0_end;

		public static GRGEN_LIBGR.NodeType[] Blowball_alt_0_end_neg_0_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_end_neg_0_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_end_neg_0_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_end_neg_0_VariableNums { };
		public enum Blowball_alt_0_end_neg_0_SubNums { };
		public enum Blowball_alt_0_end_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph Blowball_alt_0_end_neg_0;

		public static GRGEN_LIBGR.NodeType[] Blowball_alt_0_further_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Blowball_alt_0_further_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_further_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_further_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_further_VariableNums { };
		public enum Blowball_alt_0_further_SubNums { @_subpattern0, };
		public enum Blowball_alt_0_further_AltNums { };


		GRGEN_LGSP.PatternGraph Blowball_alt_0_further;


		private Pattern_Blowball()
		{
			name = "Blowball";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "Blowball_node_head", };
		}
		private void initialize()
		{
			bool[,] Blowball_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Blowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode Blowball_node_head = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "Blowball_node_head", "head", Blowball_node_head_AllowedTypes, Blowball_node_head_IsAllowedType, 5.5F, 0);
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
			GRGEN_LGSP.PatternNode Blowball_alt_0_end_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "Blowball_alt_0_end_neg_0_node__node0", "_node0", Blowball_alt_0_end_neg_0_node__node0_AllowedTypes, Blowball_alt_0_end_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Blowball_alt_0_end_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Blowball_alt_0_end_neg_0_edge__edge0", "_edge0", Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes, Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			Blowball_alt_0_end_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"Blowball_alt_0_end_",
				false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head, Blowball_alt_0_end_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { Blowball_alt_0_end_neg_0_edge__edge0 }, 
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
				Blowball_alt_0_end_neg_0_isNodeHomomorphicGlobal,
				Blowball_alt_0_end_neg_0_isEdgeHomomorphicGlobal
			);
			Blowball_alt_0_end_neg_0.edgeToSourceNode.Add(Blowball_alt_0_end_neg_0_edge__edge0, Blowball_node_head);
			Blowball_alt_0_end_neg_0.edgeToTargetNode.Add(Blowball_alt_0_end_neg_0_edge__edge0, Blowball_alt_0_end_neg_0_node__node0);

			Blowball_alt_0_end = new GRGEN_LGSP.PatternGraph(
				"end",
				"Blowball_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Blowball_alt_0_end_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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
			GRGEN_LGSP.PatternNode Blowball_alt_0_further_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "Blowball_alt_0_further_node__node0", "_node0", Blowball_alt_0_further_node__node0_AllowedTypes, Blowball_alt_0_further_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Blowball_alt_0_further_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Blowball_alt_0_further_edge__edge0", "_edge0", Blowball_alt_0_further_edge__edge0_AllowedTypes, Blowball_alt_0_further_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding Blowball_alt_0_further__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_Blowball.Instance, new GRGEN_LGSP.PatternElement[] { Blowball_node_head });
			Blowball_alt_0_further = new GRGEN_LGSP.PatternGraph(
				"further",
				"Blowball_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head, Blowball_alt_0_further_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { Blowball_alt_0_further_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Blowball_alt_0_further__subpattern0 }, 
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
				Blowball_alt_0_further_isNodeHomomorphicGlobal,
				Blowball_alt_0_further_isEdgeHomomorphicGlobal
			);
			Blowball_alt_0_further.edgeToSourceNode.Add(Blowball_alt_0_further_edge__edge0, Blowball_node_head);
			Blowball_alt_0_further.edgeToTargetNode.Add(Blowball_alt_0_further_edge__edge0, Blowball_alt_0_further_node__node0);

			GRGEN_LGSP.Alternative Blowball_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "Blowball_", new GRGEN_LGSP.PatternGraph[] { Blowball_alt_0_end, Blowball_alt_0_further } );

			pat_Blowball = new GRGEN_LGSP.PatternGraph(
				"Blowball",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Blowball_node_head }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { Blowball_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void Blowball_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_head)
		{
			graph.SettingAddedNodeNames( create_Blowball_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Blowball_addedEdgeNames );
		}
		private static String[] create_Blowball_addedNodeNames = new String[] {  };
		private static String[] create_Blowball_addedEdgeNames = new String[] {  };

		public void Blowball_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Blowball curMatch)
		{
			IMatch_Blowball_alt_0 alternative_alt_0 = curMatch._alt_0;
			Blowball_alt_0_Delete(graph, alternative_alt_0);
		}

		public void Blowball_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_Blowball_alt_0 curMatch)
		{
			if(curMatch.Pattern == Blowball_alt_0_end) {
				Blowball_alt_0_end_Delete(graph, (Match_Blowball_alt_0_end)curMatch);
				return;
			}
			else if(curMatch.Pattern == Blowball_alt_0_further) {
				Blowball_alt_0_further_Delete(graph, (Match_Blowball_alt_0_further)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void Blowball_alt_0_end_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Blowball_alt_0_end_addedNodeNames );
			@Node node_head = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Blowball_alt_0_end_addedEdgeNames );
		}
		private static String[] create_Blowball_alt_0_end_addedNodeNames = new String[] { "head" };
		private static String[] create_Blowball_alt_0_end_addedEdgeNames = new String[] {  };

		public void Blowball_alt_0_end_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Blowball_alt_0_end curMatch)
		{
		}

		public void Blowball_alt_0_further_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void Blowball_alt_0_further_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Blowball_alt_0_further curMatch)
		{
			GRGEN_LGSP.LGSPNode node__node0 = curMatch._node__node0;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_Blowball.Match_Blowball subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
			Pattern_Blowball.Instance.Blowball_Delete(graph, subpattern__subpattern0);
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
			//Independents
		}

		public interface IMatch_Blowball_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_Blowball_alt_0_end : IMatch_Blowball_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_head { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			//Independents
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
			@Pattern_Blowball.Match_Blowball @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_Blowball.Match_Blowball @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_Blowball.Match_Blowball @__subpattern0;
			public enum Blowball_alt_0_further_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Blowball_alt_0_further_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ReverseChainFromTo : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ReverseChainFromTo instance = null;
		public static Pattern_ReverseChainFromTo Instance { get { if (instance==null) { instance = new Pattern_ReverseChainFromTo(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] ReverseChainFromTo_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ReverseChainFromTo_node_to_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_node_from_IsAllowedType = null;
		public static bool[] ReverseChainFromTo_node_to_IsAllowedType = null;
		public enum ReverseChainFromTo_NodeNums { @from, @to, };
		public enum ReverseChainFromTo_EdgeNums { };
		public enum ReverseChainFromTo_VariableNums { };
		public enum ReverseChainFromTo_SubNums { };
		public enum ReverseChainFromTo_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_ReverseChainFromTo;

		public enum ReverseChainFromTo_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromTo_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ReverseChainFromTo_alt_0_base_NodeNums { @to, @from, };
		public enum ReverseChainFromTo_alt_0_base_EdgeNums { @_edge0, };
		public enum ReverseChainFromTo_alt_0_base_VariableNums { };
		public enum ReverseChainFromTo_alt_0_base_SubNums { };
		public enum ReverseChainFromTo_alt_0_base_AltNums { };


		GRGEN_LGSP.PatternGraph ReverseChainFromTo_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ReverseChainFromTo_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromTo_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ReverseChainFromTo_alt_0_rec_NodeNums { @intermediate, @from, @to, };
		public enum ReverseChainFromTo_alt_0_rec_EdgeNums { @_edge0, };
		public enum ReverseChainFromTo_alt_0_rec_VariableNums { };
		public enum ReverseChainFromTo_alt_0_rec_SubNums { @_subpattern0, };
		public enum ReverseChainFromTo_alt_0_rec_AltNums { };


		GRGEN_LGSP.PatternGraph ReverseChainFromTo_alt_0_rec;


		private Pattern_ReverseChainFromTo()
		{
			name = "ReverseChainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ReverseChainFromTo_node_from", "ReverseChainFromTo_node_to", };
		}
		private void initialize()
		{
			bool[,] ReverseChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReverseChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode ReverseChainFromTo_node_from = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromTo_node_from", "from", ReverseChainFromTo_node_from_AllowedTypes, ReverseChainFromTo_node_from_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ReverseChainFromTo_node_to = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromTo_node_to", "to", ReverseChainFromTo_node_to_AllowedTypes, ReverseChainFromTo_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ReverseChainFromTo_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReverseChainFromTo_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge ReverseChainFromTo_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromTo_alt_0_base_edge__edge0", "_edge0", ReverseChainFromTo_alt_0_base_edge__edge0_AllowedTypes, ReverseChainFromTo_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ReverseChainFromTo_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ReverseChainFromTo_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromTo_node_to, ReverseChainFromTo_node_from }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromTo_alt_0_base_edge__edge0 }, 
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
			GRGEN_LGSP.PatternNode ReverseChainFromTo_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromTo_alt_0_rec_node_intermediate", "intermediate", ReverseChainFromTo_alt_0_rec_node_intermediate_AllowedTypes, ReverseChainFromTo_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ReverseChainFromTo_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromTo_alt_0_rec_edge__edge0", "_edge0", ReverseChainFromTo_alt_0_rec_edge__edge0_AllowedTypes, ReverseChainFromTo_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ReverseChainFromTo_alt_0_rec__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromTo.Instance, new GRGEN_LGSP.PatternElement[] { ReverseChainFromTo_alt_0_rec_node_intermediate, ReverseChainFromTo_node_to });
			ReverseChainFromTo_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ReverseChainFromTo_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromTo_alt_0_rec_node_intermediate, ReverseChainFromTo_node_from, ReverseChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromTo_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ReverseChainFromTo_alt_0_rec__subpattern0 }, 
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
				ReverseChainFromTo_alt_0_rec_isNodeHomomorphicGlobal,
				ReverseChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ReverseChainFromTo_alt_0_rec.edgeToSourceNode.Add(ReverseChainFromTo_alt_0_rec_edge__edge0, ReverseChainFromTo_alt_0_rec_node_intermediate);
			ReverseChainFromTo_alt_0_rec.edgeToTargetNode.Add(ReverseChainFromTo_alt_0_rec_edge__edge0, ReverseChainFromTo_node_from);

			GRGEN_LGSP.Alternative ReverseChainFromTo_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ReverseChainFromTo_", new GRGEN_LGSP.PatternGraph[] { ReverseChainFromTo_alt_0_base, ReverseChainFromTo_alt_0_rec } );

			pat_ReverseChainFromTo = new GRGEN_LGSP.PatternGraph(
				"ReverseChainFromTo",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromTo_node_from, ReverseChainFromTo_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ReverseChainFromTo_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void ReverseChainFromTo_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromTo_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ReverseChainFromTo_addedEdgeNames );
		}
		private static String[] create_ReverseChainFromTo_addedNodeNames = new String[] {  };
		private static String[] create_ReverseChainFromTo_addedEdgeNames = new String[] {  };

		public void ReverseChainFromTo_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ReverseChainFromTo curMatch)
		{
			IMatch_ReverseChainFromTo_alt_0 alternative_alt_0 = curMatch._alt_0;
			ReverseChainFromTo_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ReverseChainFromTo_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ReverseChainFromTo_alt_0 curMatch)
		{
			if(curMatch.Pattern == ReverseChainFromTo_alt_0_base) {
				ReverseChainFromTo_alt_0_base_Delete(graph, (Match_ReverseChainFromTo_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ReverseChainFromTo_alt_0_rec) {
				ReverseChainFromTo_alt_0_rec_Delete(graph, (Match_ReverseChainFromTo_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ReverseChainFromTo_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromTo_alt_0_base_addedNodeNames );
			@Node node_to = @Node.CreateNode(graph);
			@Node node_from = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ReverseChainFromTo_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_to, node_from);
		}
		private static String[] create_ReverseChainFromTo_alt_0_base_addedNodeNames = new String[] { "to", "from" };
		private static String[] create_ReverseChainFromTo_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ReverseChainFromTo_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ReverseChainFromTo_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
		}

		public void ReverseChainFromTo_alt_0_rec_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ReverseChainFromTo_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ReverseChainFromTo_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ReverseChainFromTo.Match_ReverseChainFromTo subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ReverseChainFromTo.Instance.ReverseChainFromTo_Delete(graph, subpattern__subpattern0);
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
			//Independents
		}

		public interface IMatch_ReverseChainFromTo_alt_0 : GRGEN_LIBGR.IMatch
		{
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
			//Independents
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
			@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @__subpattern0;
			public enum ReverseChainFromTo_alt_0_rec_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromTo_alt_0_rec_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromToReverse : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromToReverse instance = null;
		public static Pattern_ChainFromToReverse Instance { get { if (instance==null) { instance = new Pattern_ChainFromToReverse(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverse_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ChainFromToReverse_node_to_AllowedTypes = null;
		public static bool[] ChainFromToReverse_node_from_IsAllowedType = null;
		public static bool[] ChainFromToReverse_node_to_IsAllowedType = null;
		public enum ChainFromToReverse_NodeNums { @from, @to, };
		public enum ChainFromToReverse_EdgeNums { };
		public enum ChainFromToReverse_VariableNums { };
		public enum ChainFromToReverse_SubNums { };
		public enum ChainFromToReverse_AltNums { @alt_0, };



		GRGEN_LGSP.PatternGraph pat_ChainFromToReverse;

		public enum ChainFromToReverse_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverse_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverse_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromToReverse_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromToReverse_alt_0_base_VariableNums { };
		public enum ChainFromToReverse_alt_0_base_SubNums { };
		public enum ChainFromToReverse_alt_0_base_AltNums { };



		GRGEN_LGSP.PatternGraph ChainFromToReverse_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverse_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverse_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverse_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromToReverse_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromToReverse_alt_0_rec_VariableNums { };
		public enum ChainFromToReverse_alt_0_rec_SubNums { @cftr, };
		public enum ChainFromToReverse_alt_0_rec_AltNums { };



		GRGEN_LGSP.PatternGraph ChainFromToReverse_alt_0_rec;


		private Pattern_ChainFromToReverse()
		{
			name = "ChainFromToReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromToReverse_node_from", "ChainFromToReverse_node_to", };
		}
		private void initialize()
		{
			bool[,] ChainFromToReverse_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode ChainFromToReverse_node_from = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverse_node_from", "from", ChainFromToReverse_node_from_AllowedTypes, ChainFromToReverse_node_from_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ChainFromToReverse_node_to = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverse_node_to", "to", ChainFromToReverse_node_to_AllowedTypes, ChainFromToReverse_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ChainFromToReverse_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverse_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge ChainFromToReverse_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverse_alt_0_base_edge__edge0", "_edge0", ChainFromToReverse_alt_0_base_edge__edge0_AllowedTypes, ChainFromToReverse_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromToReverse_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromToReverse_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverse_alt_0_base_edge__edge0 }, 
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
			GRGEN_LGSP.PatternNode ChainFromToReverse_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverse_alt_0_rec_node_intermediate", "intermediate", ChainFromToReverse_alt_0_rec_node_intermediate_AllowedTypes, ChainFromToReverse_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ChainFromToReverse_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverse_alt_0_rec_edge__edge0", "_edge0", ChainFromToReverse_alt_0_rec_edge__edge0_AllowedTypes, ChainFromToReverse_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromToReverse_alt_0_rec_cftr = new GRGEN_LGSP.PatternGraphEmbedding("cftr", Pattern_ChainFromToReverse.Instance, new GRGEN_LGSP.PatternElement[] { ChainFromToReverse_alt_0_rec_node_intermediate, ChainFromToReverse_node_to });
			ChainFromToReverse_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromToReverse_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_alt_0_rec_node_intermediate, ChainFromToReverse_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverse_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromToReverse_alt_0_rec_cftr }, 
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
				ChainFromToReverse_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromToReverse_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromToReverse_alt_0_rec.edgeToSourceNode.Add(ChainFromToReverse_alt_0_rec_edge__edge0, ChainFromToReverse_node_from);
			ChainFromToReverse_alt_0_rec.edgeToTargetNode.Add(ChainFromToReverse_alt_0_rec_edge__edge0, ChainFromToReverse_alt_0_rec_node_intermediate);

			GRGEN_LGSP.Alternative ChainFromToReverse_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromToReverse_", new GRGEN_LGSP.PatternGraph[] { ChainFromToReverse_alt_0_base, ChainFromToReverse_alt_0_rec } );

			pat_ChainFromToReverse = new GRGEN_LGSP.PatternGraph(
				"ChainFromToReverse",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromToReverse_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void ChainFromToReverse_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ChainFromToReverse curMatch = (Match_ChainFromToReverse)_curMatch;
			IMatch_ChainFromToReverse_alt_0 alternative_alt_0 = curMatch._alt_0;
			graph.SettingAddedNodeNames( ChainFromToReverse_addedNodeNames );
			ChainFromToReverse_alt_0_Modify(graph, alternative_alt_0);
			graph.SettingAddedEdgeNames( ChainFromToReverse_addedEdgeNames );
		}
		private static String[] ChainFromToReverse_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverse_addedEdgeNames = new String[] {  };

		public void ChainFromToReverse_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ChainFromToReverse curMatch = (Match_ChainFromToReverse)_curMatch;
			IMatch_ChainFromToReverse_alt_0 alternative_alt_0 = curMatch._alt_0;
			graph.SettingAddedNodeNames( ChainFromToReverse_addedNodeNames );
			ChainFromToReverse_alt_0_ModifyNoReuse(graph, alternative_alt_0);
			graph.SettingAddedEdgeNames( ChainFromToReverse_addedEdgeNames );
		}

		public void ChainFromToReverse_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverse_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromToReverse_addedEdgeNames );
		}
		private static String[] create_ChainFromToReverse_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromToReverse_addedEdgeNames = new String[] {  };

		public void ChainFromToReverse_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromToReverse curMatch)
		{
			IMatch_ChainFromToReverse_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromToReverse_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromToReverse_alt_0_Modify(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromToReverse_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_Modify(graph, (Match_ChainFromToReverse_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_Modify(graph, (Match_ChainFromToReverse_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromToReverse_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_ModifyNoReuse(graph, (Match_ChainFromToReverse_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_ModifyNoReuse(graph, (Match_ChainFromToReverse_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromToReverse_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_Delete(graph, (Match_ChainFromToReverse_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_Delete(graph, (Match_ChainFromToReverse_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_base_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ChainFromToReverse_alt_0_base curMatch = (Match_ChainFromToReverse_alt_0_base)_curMatch;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
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

		public void ChainFromToReverse_alt_0_base_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ChainFromToReverse_alt_0_base curMatch = (Match_ChainFromToReverse_alt_0_base)_curMatch;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_base_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_to, node_from);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverse_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverse_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromToReverse_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
		}
		private static String[] create_ChainFromToReverse_alt_0_base_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFromToReverse_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromToReverse_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromToReverse_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverse_alt_0_rec_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ChainFromToReverse_alt_0_rec curMatch = (Match_ChainFromToReverse_alt_0_rec)_curMatch;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
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

		public void ChainFromToReverse_alt_0_rec_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ChainFromToReverse_alt_0_rec curMatch = (Match_ChainFromToReverse_alt_0_rec)_curMatch;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(graph, subpattern_cftr);
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_rec_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverse_alt_0_rec_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ChainFromToReverse_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromToReverse_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Delete(graph, subpattern_cftr);
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
			//Independents
		}

		public interface IMatch_ChainFromToReverse_alt_0 : GRGEN_LIBGR.IMatch
		{
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
			//Independents
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ChainFromToReverseToCommon : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ChainFromToReverseToCommon instance = null;
		public static Pattern_ChainFromToReverseToCommon Instance { get { if (instance==null) { instance = new Pattern_ChainFromToReverseToCommon(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverseToCommon_node_from_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ChainFromToReverseToCommon_node_to_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_node_from_IsAllowedType = null;
		public static bool[] ChainFromToReverseToCommon_node_to_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_NodeNums { @from, @to, };
		public enum ChainFromToReverseToCommon_EdgeNums { };
		public enum ChainFromToReverseToCommon_VariableNums { };
		public enum ChainFromToReverseToCommon_SubNums { };
		public enum ChainFromToReverseToCommon_AltNums { @alt_0, };



		GRGEN_LGSP.PatternGraph pat_ChainFromToReverseToCommon;

		public enum ChainFromToReverseToCommon_alt_0_CaseNums { @base, @rec, };
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverseToCommon_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromToReverseToCommon_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromToReverseToCommon_alt_0_base_VariableNums { };
		public enum ChainFromToReverseToCommon_alt_0_base_SubNums { };
		public enum ChainFromToReverseToCommon_alt_0_base_AltNums { };



		GRGEN_LGSP.PatternGraph ChainFromToReverseToCommon_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ChainFromToReverseToCommon_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ChainFromToReverseToCommon_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromToReverseToCommon_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromToReverseToCommon_alt_0_rec_VariableNums { };
		public enum ChainFromToReverseToCommon_alt_0_rec_SubNums { @cftrtc, };
		public enum ChainFromToReverseToCommon_alt_0_rec_AltNums { };



		GRGEN_LGSP.PatternGraph ChainFromToReverseToCommon_alt_0_rec;


		private Pattern_ChainFromToReverseToCommon()
		{
			name = "ChainFromToReverseToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromToReverseToCommon_node_from", "ChainFromToReverseToCommon_node_to", };
		}
		private void initialize()
		{
			bool[,] ChainFromToReverseToCommon_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverseToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode ChainFromToReverseToCommon_node_from = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverseToCommon_node_from", "from", ChainFromToReverseToCommon_node_from_AllowedTypes, ChainFromToReverseToCommon_node_from_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ChainFromToReverseToCommon_node_to = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverseToCommon_node_to", "to", ChainFromToReverseToCommon_node_to_AllowedTypes, ChainFromToReverseToCommon_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ChainFromToReverseToCommon_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverseToCommon_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge ChainFromToReverseToCommon_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverseToCommon_alt_0_base_edge__edge0", "_edge0", ChainFromToReverseToCommon_alt_0_base_edge__edge0_AllowedTypes, ChainFromToReverseToCommon_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromToReverseToCommon_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ChainFromToReverseToCommon_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverseToCommon_alt_0_base_edge__edge0 }, 
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
			GRGEN_LGSP.PatternNode ChainFromToReverseToCommon_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ChainFromToReverseToCommon_alt_0_rec_node_intermediate", "intermediate", ChainFromToReverseToCommon_alt_0_rec_node_intermediate_AllowedTypes, ChainFromToReverseToCommon_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ChainFromToReverseToCommon_alt_0_rec_edge__edge0", "_edge0", ChainFromToReverseToCommon_alt_0_rec_edge__edge0_AllowedTypes, ChainFromToReverseToCommon_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ChainFromToReverseToCommon_alt_0_rec_cftrtc = new GRGEN_LGSP.PatternGraphEmbedding("cftrtc", Pattern_ChainFromToReverseToCommon.Instance, new GRGEN_LGSP.PatternElement[] { ChainFromToReverseToCommon_alt_0_rec_node_intermediate, ChainFromToReverseToCommon_node_to });
			ChainFromToReverseToCommon_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ChainFromToReverseToCommon_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_alt_0_rec_node_intermediate, ChainFromToReverseToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ChainFromToReverseToCommon_alt_0_rec_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ChainFromToReverseToCommon_alt_0_rec_cftrtc }, 
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
				ChainFromToReverseToCommon_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromToReverseToCommon_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromToReverseToCommon_alt_0_rec.edgeToSourceNode.Add(ChainFromToReverseToCommon_alt_0_rec_edge__edge0, ChainFromToReverseToCommon_node_from);
			ChainFromToReverseToCommon_alt_0_rec.edgeToTargetNode.Add(ChainFromToReverseToCommon_alt_0_rec_edge__edge0, ChainFromToReverseToCommon_alt_0_rec_node_intermediate);

			GRGEN_LGSP.Alternative ChainFromToReverseToCommon_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ChainFromToReverseToCommon_", new GRGEN_LGSP.PatternGraph[] { ChainFromToReverseToCommon_alt_0_base, ChainFromToReverseToCommon_alt_0_rec } );

			pat_ChainFromToReverseToCommon = new GRGEN_LGSP.PatternGraph(
				"ChainFromToReverseToCommon",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ChainFromToReverseToCommon_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void ChainFromToReverseToCommon_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			Match_ChainFromToReverseToCommon curMatch = (Match_ChainFromToReverseToCommon)_curMatch;
			IMatch_ChainFromToReverseToCommon_alt_0 alternative_alt_0 = curMatch._alt_0;
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_addedNodeNames );
			ChainFromToReverseToCommon_alt_0_Modify(graph, alternative_alt_0, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_addedEdgeNames );
		}
		private static String[] ChainFromToReverseToCommon_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverseToCommon_addedEdgeNames = new String[] {  };

		public void ChainFromToReverseToCommon_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			Match_ChainFromToReverseToCommon curMatch = (Match_ChainFromToReverseToCommon)_curMatch;
			IMatch_ChainFromToReverseToCommon_alt_0 alternative_alt_0 = curMatch._alt_0;
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_addedNodeNames );
			ChainFromToReverseToCommon_alt_0_ModifyNoReuse(graph, alternative_alt_0, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_addedEdgeNames );
		}

		public void ChainFromToReverseToCommon_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverseToCommon_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromToReverseToCommon_addedEdgeNames );
		}
		private static String[] create_ChainFromToReverseToCommon_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromToReverseToCommon_addedEdgeNames = new String[] {  };

		public void ChainFromToReverseToCommon_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromToReverseToCommon curMatch)
		{
			IMatch_ChainFromToReverseToCommon_alt_0 alternative_alt_0 = curMatch._alt_0;
			ChainFromToReverseToCommon_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromToReverseToCommon_alt_0_Modify(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromToReverseToCommon_alt_0 curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_Modify(graph, (Match_ChainFromToReverseToCommon_alt_0_base)curMatch, node_common);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_Modify(graph, (Match_ChainFromToReverseToCommon_alt_0_rec)curMatch, node_common);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromToReverseToCommon_alt_0 curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_ModifyNoReuse(graph, (Match_ChainFromToReverseToCommon_alt_0_base)curMatch, node_common);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_ModifyNoReuse(graph, (Match_ChainFromToReverseToCommon_alt_0_rec)curMatch, node_common);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ChainFromToReverseToCommon_alt_0 curMatch)
		{
			if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_Delete(graph, (Match_ChainFromToReverseToCommon_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_Delete(graph, (Match_ChainFromToReverseToCommon_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_base_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			Match_ChainFromToReverseToCommon_alt_0_base curMatch = (Match_ChainFromToReverseToCommon_alt_0_base)_curMatch;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
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

		public void ChainFromToReverseToCommon_alt_0_base_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			Match_ChainFromToReverseToCommon_alt_0_base curMatch = (Match_ChainFromToReverseToCommon_alt_0_base)_curMatch;
			GRGEN_LGSP.LGSPNode node_to = curMatch._node_to;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_base_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_to, node_from);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_from, node_common);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_to, node_common);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverseToCommon_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverseToCommon_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromToReverseToCommon_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
		}
		private static String[] create_ChainFromToReverseToCommon_alt_0_base_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFromToReverseToCommon_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromToReverseToCommon_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromToReverseToCommon_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverseToCommon_alt_0_rec_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			Match_ChainFromToReverseToCommon_alt_0_rec curMatch = (Match_ChainFromToReverseToCommon_alt_0_rec)_curMatch;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
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

		public void ChainFromToReverseToCommon_alt_0_rec_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, GRGEN_LGSP.LGSPNode node_common)
		{
			Match_ChainFromToReverseToCommon_alt_0_rec curMatch = (Match_ChainFromToReverseToCommon_alt_0_rec)_curMatch;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_from = curMatch._node_from;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(graph, subpattern_cftrtc, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_from, node_common);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverseToCommon_alt_0_rec_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ChainFromToReverseToCommon_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ChainFromToReverseToCommon_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Delete(graph, subpattern_cftrtc);
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
			//Independents
		}

		public interface IMatch_ChainFromToReverseToCommon_alt_0 : GRGEN_LIBGR.IMatch
		{
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
			//Independents
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ReverseChainFromToToCommon : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ReverseChainFromToToCommon instance = null;
		public static Pattern_ReverseChainFromToToCommon Instance { get { if (instance==null) { instance = new Pattern_ReverseChainFromToToCommon(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_ReverseChainFromToToCommon;

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


		GRGEN_LGSP.PatternGraph ReverseChainFromToToCommon_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] ReverseChainFromToToCommon_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromToToCommon_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ReverseChainFromToToCommon_alt_0_rec_edge__edge1_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_edge__edge0_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_edge__edge1_IsAllowedType = null;
		public enum ReverseChainFromToToCommon_alt_0_rec_NodeNums { @intermediate, @from, @common, @to, };
		public enum ReverseChainFromToToCommon_alt_0_rec_EdgeNums { @_edge0, @_edge1, };
		public enum ReverseChainFromToToCommon_alt_0_rec_VariableNums { };
		public enum ReverseChainFromToToCommon_alt_0_rec_SubNums { @_subpattern0, };
		public enum ReverseChainFromToToCommon_alt_0_rec_AltNums { };


		GRGEN_LGSP.PatternGraph ReverseChainFromToToCommon_alt_0_rec;


		private Pattern_ReverseChainFromToToCommon()
		{
			name = "ReverseChainFromToToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ReverseChainFromToToCommon_node_from", "ReverseChainFromToToCommon_node_to", "ReverseChainFromToToCommon_node_common", };
		}
		private void initialize()
		{
			bool[,] ReverseChainFromToToCommon_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ReverseChainFromToToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_node_from = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_node_from", "from", ReverseChainFromToToCommon_node_from_AllowedTypes, ReverseChainFromToToCommon_node_from_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_node_to = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_node_to", "to", ReverseChainFromToToCommon_node_to_AllowedTypes, ReverseChainFromToToCommon_node_to_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_node_common = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_node_common", "common", ReverseChainFromToToCommon_node_common_AllowedTypes, ReverseChainFromToToCommon_node_common_IsAllowedType, 5.5F, 2);
			bool[,] ReverseChainFromToToCommon_alt_0_base_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ReverseChainFromToToCommon_alt_0_base_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_base_edge__edge0", "_edge0", ReverseChainFromToToCommon_alt_0_base_edge__edge0_AllowedTypes, ReverseChainFromToToCommon_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_base_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_base_edge__edge1", "_edge1", ReverseChainFromToToCommon_alt_0_base_edge__edge1_AllowedTypes, ReverseChainFromToToCommon_alt_0_base_edge__edge1_IsAllowedType, 5.5F, -1);
			ReverseChainFromToToCommon_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"ReverseChainFromToToCommon_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_common }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromToToCommon_alt_0_base_edge__edge0, ReverseChainFromToToCommon_alt_0_base_edge__edge1 }, 
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
			GRGEN_LGSP.PatternNode ReverseChainFromToToCommon_alt_0_rec_node_intermediate = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "ReverseChainFromToToCommon_alt_0_rec_node_intermediate", "intermediate", ReverseChainFromToToCommon_alt_0_rec_node_intermediate_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_rec_edge__edge0", "_edge0", ReverseChainFromToToCommon_alt_0_rec_edge__edge0_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ReverseChainFromToToCommon_alt_0_rec_edge__edge1", "_edge1", ReverseChainFromToToCommon_alt_0_rec_edge__edge1_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ReverseChainFromToToCommon_alt_0_rec__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromToToCommon.Instance, new GRGEN_LGSP.PatternElement[] { ReverseChainFromToToCommon_alt_0_rec_node_intermediate, ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_common });
			ReverseChainFromToToCommon_alt_0_rec = new GRGEN_LGSP.PatternGraph(
				"rec",
				"ReverseChainFromToToCommon_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromToToCommon_alt_0_rec_node_intermediate, ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_common, ReverseChainFromToToCommon_node_to }, 
				new GRGEN_LGSP.PatternEdge[] { ReverseChainFromToToCommon_alt_0_rec_edge__edge0, ReverseChainFromToToCommon_alt_0_rec_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ReverseChainFromToToCommon_alt_0_rec__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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

			GRGEN_LGSP.Alternative ReverseChainFromToToCommon_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ReverseChainFromToToCommon_", new GRGEN_LGSP.PatternGraph[] { ReverseChainFromToToCommon_alt_0_base, ReverseChainFromToToCommon_alt_0_rec } );

			pat_ReverseChainFromToToCommon = new GRGEN_LGSP.PatternGraph(
				"ReverseChainFromToToCommon",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_common }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ReverseChainFromToToCommon_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void ReverseChainFromToToCommon_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_from, GRGEN_LGSP.LGSPNode node_to, GRGEN_LGSP.LGSPNode node_common)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromToToCommon_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ReverseChainFromToToCommon_addedEdgeNames );
		}
		private static String[] create_ReverseChainFromToToCommon_addedNodeNames = new String[] {  };
		private static String[] create_ReverseChainFromToToCommon_addedEdgeNames = new String[] {  };

		public void ReverseChainFromToToCommon_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ReverseChainFromToToCommon curMatch)
		{
			IMatch_ReverseChainFromToToCommon_alt_0 alternative_alt_0 = curMatch._alt_0;
			ReverseChainFromToToCommon_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ReverseChainFromToToCommon_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ReverseChainFromToToCommon_alt_0 curMatch)
		{
			if(curMatch.Pattern == ReverseChainFromToToCommon_alt_0_base) {
				ReverseChainFromToToCommon_alt_0_base_Delete(graph, (Match_ReverseChainFromToToCommon_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == ReverseChainFromToToCommon_alt_0_rec) {
				ReverseChainFromToToCommon_alt_0_rec_Delete(graph, (Match_ReverseChainFromToToCommon_alt_0_rec)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ReverseChainFromToToCommon_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ReverseChainFromToToCommon_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ReverseChainFromToToCommon_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
		}

		public void ReverseChainFromToToCommon_alt_0_rec_Create(GRGEN_LGSP.LGSPGraph graph)
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

		public void ReverseChainFromToToCommon_alt_0_rec_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ReverseChainFromToToCommon_alt_0_rec curMatch)
		{
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ReverseChainFromToToCommon.Instance.ReverseChainFromToToCommon_Delete(graph, subpattern__subpattern0);
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
			//Independents
		}

		public interface IMatch_ReverseChainFromToToCommon_alt_0 : GRGEN_LIBGR.IMatch
		{
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
			//Independents
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
			@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @__subpattern0;
			public enum ReverseChainFromToToCommon_alt_0_rec_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ReverseChainFromToToCommon_alt_0_rec_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createChain : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createChain instance = null;
		public static Rule_createChain Instance { get { if (instance==null) { instance = new Rule_createChain(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[2];

		public enum createChain_NodeNums { };
		public enum createChain_EdgeNums { };
		public enum createChain_VariableNums { };
		public enum createChain_SubNums { };
		public enum createChain_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createChain;


		private Rule_createChain()
		{
			name = "createChain";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] createChain_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createChain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createChain = new GRGEN_LGSP.PatternGraph(
				"createChain",
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
				createChain_isNodeHomomorphicGlobal,
				createChain_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createChain;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createChain curMatch = (Match_createChain)_curMatch;
			graph.SettingAddedNodeNames( createChain_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node__node0);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node__node1, node_end);
			ReturnArray[0] = node_beg;
			ReturnArray[1] = node_end;
			return ReturnArray;
		}
		private static String[] createChain_addedNodeNames = new String[] { "beg", "_node0", "_node1", "end" };
		private static String[] createChain_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createChain curMatch = (Match_createChain)_curMatch;
			graph.SettingAddedNodeNames( createChain_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node__node0);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node__node1, node_end);
			ReturnArray[0] = node_beg;
			ReturnArray[1] = node_end;
			return ReturnArray;
		}

		static Rule_createChain() {
		}

		public interface IMatch_createChain : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromTo : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromTo instance = null;
		public static Rule_chainFromTo Instance { get { if (instance==null) { instance = new Rule_chainFromTo(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] chainFromTo_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] chainFromTo_node_end_AllowedTypes = null;
		public static bool[] chainFromTo_node_beg_IsAllowedType = null;
		public static bool[] chainFromTo_node_end_IsAllowedType = null;
		public enum chainFromTo_NodeNums { @beg, @end, };
		public enum chainFromTo_EdgeNums { };
		public enum chainFromTo_VariableNums { };
		public enum chainFromTo_SubNums { @_subpattern0, };
		public enum chainFromTo_AltNums { };


		GRGEN_LGSP.PatternGraph pat_chainFromTo;


		private Rule_chainFromTo()
		{
			name = "chainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromTo_node_beg", "chainFromTo_node_end", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] chainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode chainFromTo_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromTo_node_beg", "beg", chainFromTo_node_beg_AllowedTypes, chainFromTo_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode chainFromTo_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromTo_node_end", "end", chainFromTo_node_end_AllowedTypes, chainFromTo_node_end_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternGraphEmbedding chainFromTo__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ChainFromTo.Instance, new GRGEN_LGSP.PatternElement[] { chainFromTo_node_beg, chainFromTo_node_end });
			pat_chainFromTo = new GRGEN_LGSP.PatternGraph(
				"chainFromTo",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { chainFromTo_node_beg, chainFromTo_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromTo__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromTo curMatch = (Match_chainFromTo)_curMatch;
			Pattern_ChainFromTo.Match_ChainFromTo subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromTo curMatch = (Match_chainFromTo)_curMatch;
			Pattern_ChainFromTo.Match_ChainFromTo subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
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
			@Pattern_ChainFromTo.Match_ChainFromTo @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ChainFromTo.Match_ChainFromTo @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ChainFromTo.Match_ChainFromTo @__subpattern0;
			public enum chainFromTo_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromTo_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFrom : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFrom instance = null;
		public static Rule_chainFrom Instance { get { if (instance==null) { instance = new Rule_chainFrom(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] chainFrom_node_beg_AllowedTypes = null;
		public static bool[] chainFrom_node_beg_IsAllowedType = null;
		public enum chainFrom_NodeNums { @beg, };
		public enum chainFrom_EdgeNums { };
		public enum chainFrom_VariableNums { };
		public enum chainFrom_SubNums { @_subpattern0, };
		public enum chainFrom_AltNums { };


		GRGEN_LGSP.PatternGraph pat_chainFrom;


		private Rule_chainFrom()
		{
			name = "chainFrom";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFrom_node_beg", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] chainFrom_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFrom_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode chainFrom_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFrom_node_beg", "beg", chainFrom_node_beg_AllowedTypes, chainFrom_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternGraphEmbedding chainFrom__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ChainFrom.Instance, new GRGEN_LGSP.PatternElement[] { chainFrom_node_beg });
			pat_chainFrom = new GRGEN_LGSP.PatternGraph(
				"chainFrom",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { chainFrom_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFrom__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFrom curMatch = (Match_chainFrom)_curMatch;
			Pattern_ChainFrom.Match_ChainFrom subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFrom curMatch = (Match_chainFrom)_curMatch;
			Pattern_ChainFrom.Match_ChainFrom subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
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
			@Pattern_ChainFrom.Match_ChainFrom @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ChainFrom.Match_ChainFrom @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ChainFrom.Match_ChainFrom @__subpattern0;
			public enum chainFrom_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFrom_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromComplete : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromComplete instance = null;
		public static Rule_chainFromComplete Instance { get { if (instance==null) { instance = new Rule_chainFromComplete(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] chainFromComplete_node_beg_AllowedTypes = null;
		public static bool[] chainFromComplete_node_beg_IsAllowedType = null;
		public enum chainFromComplete_NodeNums { @beg, };
		public enum chainFromComplete_EdgeNums { };
		public enum chainFromComplete_VariableNums { };
		public enum chainFromComplete_SubNums { @_subpattern0, };
		public enum chainFromComplete_AltNums { };


		GRGEN_LGSP.PatternGraph pat_chainFromComplete;


		private Rule_chainFromComplete()
		{
			name = "chainFromComplete";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromComplete_node_beg", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] chainFromComplete_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFromComplete_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode chainFromComplete_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromComplete_node_beg", "beg", chainFromComplete_node_beg_AllowedTypes, chainFromComplete_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternGraphEmbedding chainFromComplete__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ChainFromComplete.Instance, new GRGEN_LGSP.PatternElement[] { chainFromComplete_node_beg });
			pat_chainFromComplete = new GRGEN_LGSP.PatternGraph(
				"chainFromComplete",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { chainFromComplete_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromComplete__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromComplete curMatch = (Match_chainFromComplete)_curMatch;
			Pattern_ChainFromComplete.Match_ChainFromComplete subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromComplete curMatch = (Match_chainFromComplete)_curMatch;
			Pattern_ChainFromComplete.Match_ChainFromComplete subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
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
			@Pattern_ChainFromComplete.Match_ChainFromComplete @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ChainFromComplete.Match_ChainFromComplete @__subpattern0;
			public enum chainFromComplete_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)chainFromComplete_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createBlowball : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createBlowball instance = null;
		public static Rule_createBlowball Instance { get { if (instance==null) { instance = new Rule_createBlowball(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[1];

		public enum createBlowball_NodeNums { };
		public enum createBlowball_EdgeNums { };
		public enum createBlowball_VariableNums { };
		public enum createBlowball_SubNums { };
		public enum createBlowball_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createBlowball;


		private Rule_createBlowball()
		{
			name = "createBlowball";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] createBlowball_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createBlowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createBlowball = new GRGEN_LGSP.PatternGraph(
				"createBlowball",
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
				createBlowball_isNodeHomomorphicGlobal,
				createBlowball_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createBlowball;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createBlowball curMatch = (Match_createBlowball)_curMatch;
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
			ReturnArray[0] = node_head;
			return ReturnArray;
		}
		private static String[] createBlowball_addedNodeNames = new String[] { "head", "_node0", "_node1", "_node2", "_node3" };
		private static String[] createBlowball_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createBlowball curMatch = (Match_createBlowball)_curMatch;
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
			ReturnArray[0] = node_head;
			return ReturnArray;
		}

		static Rule_createBlowball() {
		}

		public interface IMatch_createBlowball : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_blowball : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_blowball instance = null;
		public static Rule_blowball Instance { get { if (instance==null) { instance = new Rule_blowball(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] blowball_node_head_AllowedTypes = null;
		public static bool[] blowball_node_head_IsAllowedType = null;
		public enum blowball_NodeNums { @head, };
		public enum blowball_EdgeNums { };
		public enum blowball_VariableNums { };
		public enum blowball_SubNums { @_subpattern0, };
		public enum blowball_AltNums { };


		GRGEN_LGSP.PatternGraph pat_blowball;


		private Rule_blowball()
		{
			name = "blowball";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "blowball_node_head", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] blowball_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] blowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode blowball_node_head = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "blowball_node_head", "head", blowball_node_head_AllowedTypes, blowball_node_head_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternGraphEmbedding blowball__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_Blowball.Instance, new GRGEN_LGSP.PatternElement[] { blowball_node_head });
			pat_blowball = new GRGEN_LGSP.PatternGraph(
				"blowball",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { blowball_node_head }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { blowball__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_blowball curMatch = (Match_blowball)_curMatch;
			Pattern_Blowball.Match_Blowball subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_blowball curMatch = (Match_blowball)_curMatch;
			Pattern_Blowball.Match_Blowball subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
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
			@Pattern_Blowball.Match_Blowball @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_Blowball.Match_Blowball @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_Blowball.Match_Blowball @__subpattern0;
			public enum blowball_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)blowball_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_reverseChainFromTo : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_reverseChainFromTo instance = null;
		public static Rule_reverseChainFromTo Instance { get { if (instance==null) { instance = new Rule_reverseChainFromTo(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] reverseChainFromTo_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] reverseChainFromTo_node_end_AllowedTypes = null;
		public static bool[] reverseChainFromTo_node_beg_IsAllowedType = null;
		public static bool[] reverseChainFromTo_node_end_IsAllowedType = null;
		public enum reverseChainFromTo_NodeNums { @beg, @end, };
		public enum reverseChainFromTo_EdgeNums { };
		public enum reverseChainFromTo_VariableNums { };
		public enum reverseChainFromTo_SubNums { @_subpattern0, };
		public enum reverseChainFromTo_AltNums { };


		GRGEN_LGSP.PatternGraph pat_reverseChainFromTo;


		private Rule_reverseChainFromTo()
		{
			name = "reverseChainFromTo";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "reverseChainFromTo_node_beg", "reverseChainFromTo_node_end", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] reverseChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] reverseChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode reverseChainFromTo_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromTo_node_beg", "beg", reverseChainFromTo_node_beg_AllowedTypes, reverseChainFromTo_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode reverseChainFromTo_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromTo_node_end", "end", reverseChainFromTo_node_end_AllowedTypes, reverseChainFromTo_node_end_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternGraphEmbedding reverseChainFromTo__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromTo.Instance, new GRGEN_LGSP.PatternElement[] { reverseChainFromTo_node_beg, reverseChainFromTo_node_end });
			pat_reverseChainFromTo = new GRGEN_LGSP.PatternGraph(
				"reverseChainFromTo",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { reverseChainFromTo_node_beg, reverseChainFromTo_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { reverseChainFromTo__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_reverseChainFromTo curMatch = (Match_reverseChainFromTo)_curMatch;
			Pattern_ReverseChainFromTo.Match_ReverseChainFromTo subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_reverseChainFromTo curMatch = (Match_reverseChainFromTo)_curMatch;
			Pattern_ReverseChainFromTo.Match_ReverseChainFromTo subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
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
			@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ReverseChainFromTo.Match_ReverseChainFromTo @__subpattern0;
			public enum reverseChainFromTo_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)reverseChainFromTo_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createReverseChain : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createReverseChain instance = null;
		public static Rule_createReverseChain Instance { get { if (instance==null) { instance = new Rule_createReverseChain(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[2];

		public enum createReverseChain_NodeNums { };
		public enum createReverseChain_EdgeNums { };
		public enum createReverseChain_VariableNums { };
		public enum createReverseChain_SubNums { };
		public enum createReverseChain_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createReverseChain;


		private Rule_createReverseChain()
		{
			name = "createReverseChain";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] createReverseChain_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createReverseChain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createReverseChain = new GRGEN_LGSP.PatternGraph(
				"createReverseChain",
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
				createReverseChain_isNodeHomomorphicGlobal,
				createReverseChain_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createReverseChain;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createReverseChain curMatch = (Match_createReverseChain)_curMatch;
			graph.SettingAddedNodeNames( createReverseChain_addedNodeNames );
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createReverseChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node_beg);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node1, node__node0);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_end, node__node1);
			ReturnArray[0] = node_beg;
			ReturnArray[1] = node_end;
			return ReturnArray;
		}
		private static String[] createReverseChain_addedNodeNames = new String[] { "_node0", "beg", "_node1", "end" };
		private static String[] createReverseChain_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createReverseChain curMatch = (Match_createReverseChain)_curMatch;
			graph.SettingAddedNodeNames( createReverseChain_addedNodeNames );
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createReverseChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node_beg);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node1, node__node0);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_end, node__node1);
			ReturnArray[0] = node_beg;
			ReturnArray[1] = node_end;
			return ReturnArray;
		}

		static Rule_createReverseChain() {
		}

		public interface IMatch_createReverseChain : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromToReverse : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromToReverse instance = null;
		public static Rule_chainFromToReverse Instance { get { if (instance==null) { instance = new Rule_chainFromToReverse(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] chainFromToReverse_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] chainFromToReverse_node_end_AllowedTypes = null;
		public static bool[] chainFromToReverse_node_beg_IsAllowedType = null;
		public static bool[] chainFromToReverse_node_end_IsAllowedType = null;
		public enum chainFromToReverse_NodeNums { @beg, @end, };
		public enum chainFromToReverse_EdgeNums { };
		public enum chainFromToReverse_VariableNums { };
		public enum chainFromToReverse_SubNums { @cftr, };
		public enum chainFromToReverse_AltNums { };



		GRGEN_LGSP.PatternGraph pat_chainFromToReverse;


		private Rule_chainFromToReverse()
		{
			name = "chainFromToReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromToReverse_node_beg", "chainFromToReverse_node_end", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] chainFromToReverse_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromToReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode chainFromToReverse_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverse_node_beg", "beg", chainFromToReverse_node_beg_AllowedTypes, chainFromToReverse_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode chainFromToReverse_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverse_node_end", "end", chainFromToReverse_node_end_AllowedTypes, chainFromToReverse_node_end_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternGraphEmbedding chainFromToReverse_cftr = new GRGEN_LGSP.PatternGraphEmbedding("cftr", Pattern_ChainFromToReverse.Instance, new GRGEN_LGSP.PatternElement[] { chainFromToReverse_node_beg, chainFromToReverse_node_end });
			pat_chainFromToReverse = new GRGEN_LGSP.PatternGraph(
				"chainFromToReverse",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { chainFromToReverse_node_beg, chainFromToReverse_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromToReverse_cftr }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromToReverse curMatch = (Match_chainFromToReverse)_curMatch;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
			graph.SettingAddedNodeNames( chainFromToReverse_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(graph, subpattern_cftr);
			graph.SettingAddedEdgeNames( chainFromToReverse_addedEdgeNames );
			return EmptyReturnElements;
		}
		private static String[] chainFromToReverse_addedNodeNames = new String[] {  };
		private static String[] chainFromToReverse_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromToReverse curMatch = (Match_chainFromToReverse)_curMatch;
			Pattern_ChainFromToReverse.Match_ChainFromToReverse subpattern_cftr = curMatch.@_cftr;
			graph.SettingAddedNodeNames( chainFromToReverse_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(graph, subpattern_cftr);
			graph.SettingAddedEdgeNames( chainFromToReverse_addedEdgeNames );
			return EmptyReturnElements;
		}

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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_chainFromToReverseToCommon : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_chainFromToReverseToCommon instance = null;
		public static Rule_chainFromToReverseToCommon Instance { get { if (instance==null) { instance = new Rule_chainFromToReverseToCommon(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[1];

		public static GRGEN_LIBGR.NodeType[] chainFromToReverseToCommon_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] chainFromToReverseToCommon_node_end_AllowedTypes = null;
		public static bool[] chainFromToReverseToCommon_node_beg_IsAllowedType = null;
		public static bool[] chainFromToReverseToCommon_node_end_IsAllowedType = null;
		public enum chainFromToReverseToCommon_NodeNums { @beg, @end, };
		public enum chainFromToReverseToCommon_EdgeNums { };
		public enum chainFromToReverseToCommon_VariableNums { };
		public enum chainFromToReverseToCommon_SubNums { @cftrtc, };
		public enum chainFromToReverseToCommon_AltNums { };



		GRGEN_LGSP.PatternGraph pat_chainFromToReverseToCommon;


		private Rule_chainFromToReverseToCommon()
		{
			name = "chainFromToReverseToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromToReverseToCommon_node_beg", "chainFromToReverseToCommon_node_end", };
			outputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] chainFromToReverseToCommon_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromToReverseToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode chainFromToReverseToCommon_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverseToCommon_node_beg", "beg", chainFromToReverseToCommon_node_beg_AllowedTypes, chainFromToReverseToCommon_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode chainFromToReverseToCommon_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "chainFromToReverseToCommon_node_end", "end", chainFromToReverseToCommon_node_end_AllowedTypes, chainFromToReverseToCommon_node_end_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternGraphEmbedding chainFromToReverseToCommon_cftrtc = new GRGEN_LGSP.PatternGraphEmbedding("cftrtc", Pattern_ChainFromToReverseToCommon.Instance, new GRGEN_LGSP.PatternElement[] { chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end });
			pat_chainFromToReverseToCommon = new GRGEN_LGSP.PatternGraph(
				"chainFromToReverseToCommon",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { chainFromToReverseToCommon_cftrtc }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromToReverseToCommon curMatch = (Match_chainFromToReverseToCommon)_curMatch;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
			graph.SettingAddedNodeNames( chainFromToReverseToCommon_addedNodeNames );
			@Node node_common = @Node.CreateNode(graph);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(graph, subpattern_cftrtc, node_common);
			graph.SettingAddedEdgeNames( chainFromToReverseToCommon_addedEdgeNames );
			ReturnArray[0] = node_common;
			return ReturnArray;
		}
		private static String[] chainFromToReverseToCommon_addedNodeNames = new String[] { "common" };
		private static String[] chainFromToReverseToCommon_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_chainFromToReverseToCommon curMatch = (Match_chainFromToReverseToCommon)_curMatch;
			Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon subpattern_cftrtc = curMatch.@_cftrtc;
			graph.SettingAddedNodeNames( chainFromToReverseToCommon_addedNodeNames );
			@Node node_common = @Node.CreateNode(graph);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(graph, subpattern_cftrtc, node_common);
			graph.SettingAddedEdgeNames( chainFromToReverseToCommon_addedEdgeNames );
			ReturnArray[0] = node_common;
			return ReturnArray;
		}

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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_reverseChainFromToToCommon : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_reverseChainFromToToCommon instance = null;
		public static Rule_reverseChainFromToToCommon Instance { get { if (instance==null) { instance = new Rule_reverseChainFromToToCommon(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] reverseChainFromToToCommon_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] reverseChainFromToToCommon_node_end_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] reverseChainFromToToCommon_node_common_AllowedTypes = null;
		public static bool[] reverseChainFromToToCommon_node_beg_IsAllowedType = null;
		public static bool[] reverseChainFromToToCommon_node_end_IsAllowedType = null;
		public static bool[] reverseChainFromToToCommon_node_common_IsAllowedType = null;
		public enum reverseChainFromToToCommon_NodeNums { @beg, @end, @common, };
		public enum reverseChainFromToToCommon_EdgeNums { };
		public enum reverseChainFromToToCommon_VariableNums { };
		public enum reverseChainFromToToCommon_SubNums { @_subpattern0, };
		public enum reverseChainFromToToCommon_AltNums { };


		GRGEN_LGSP.PatternGraph pat_reverseChainFromToToCommon;


		private Rule_reverseChainFromToToCommon()
		{
			name = "reverseChainFromToToCommon";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "reverseChainFromToToCommon_node_beg", "reverseChainFromToToCommon_node_end", "reverseChainFromToToCommon_node_common", };
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
			GRGEN_LGSP.PatternNode reverseChainFromToToCommon_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromToToCommon_node_beg", "beg", reverseChainFromToToCommon_node_beg_AllowedTypes, reverseChainFromToToCommon_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode reverseChainFromToToCommon_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromToToCommon_node_end", "end", reverseChainFromToToCommon_node_end_AllowedTypes, reverseChainFromToToCommon_node_end_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternNode reverseChainFromToToCommon_node_common = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "reverseChainFromToToCommon_node_common", "common", reverseChainFromToToCommon_node_common_AllowedTypes, reverseChainFromToToCommon_node_common_IsAllowedType, 5.5F, 2);
			GRGEN_LGSP.PatternGraphEmbedding reverseChainFromToToCommon__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromToToCommon.Instance, new GRGEN_LGSP.PatternElement[] { reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common });
			pat_reverseChainFromToToCommon = new GRGEN_LGSP.PatternGraph(
				"reverseChainFromToToCommon",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { reverseChainFromToToCommon__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_reverseChainFromToToCommon curMatch = (Match_reverseChainFromToToCommon)_curMatch;
			Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_reverseChainFromToToCommon curMatch = (Match_reverseChainFromToToCommon)_curMatch;
			Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
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
			@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_subpattern0 { get; }
			//Alternatives
			//Independents
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
			
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon @__subpattern0;
			public enum reverseChainFromToToCommon_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)reverseChainFromToToCommon_SubNums.@_subpattern0: return __subpattern0;
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Recursive_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public Recursive_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[8];
			rules = new GRGEN_LGSP.LGSPRulePattern[11];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[8+11];
			subpatterns[0] = Pattern_ChainFromTo.Instance;
			rulesAndSubpatterns[0] = Pattern_ChainFromTo.Instance;
			subpatterns[1] = Pattern_ChainFrom.Instance;
			rulesAndSubpatterns[1] = Pattern_ChainFrom.Instance;
			subpatterns[2] = Pattern_ChainFromComplete.Instance;
			rulesAndSubpatterns[2] = Pattern_ChainFromComplete.Instance;
			subpatterns[3] = Pattern_Blowball.Instance;
			rulesAndSubpatterns[3] = Pattern_Blowball.Instance;
			subpatterns[4] = Pattern_ReverseChainFromTo.Instance;
			rulesAndSubpatterns[4] = Pattern_ReverseChainFromTo.Instance;
			subpatterns[5] = Pattern_ChainFromToReverse.Instance;
			rulesAndSubpatterns[5] = Pattern_ChainFromToReverse.Instance;
			subpatterns[6] = Pattern_ChainFromToReverseToCommon.Instance;
			rulesAndSubpatterns[6] = Pattern_ChainFromToReverseToCommon.Instance;
			subpatterns[7] = Pattern_ReverseChainFromToToCommon.Instance;
			rulesAndSubpatterns[7] = Pattern_ReverseChainFromToToCommon.Instance;
			rules[0] = Rule_createChain.Instance;
			rulesAndSubpatterns[8+0] = Rule_createChain.Instance;
			rules[1] = Rule_chainFromTo.Instance;
			rulesAndSubpatterns[8+1] = Rule_chainFromTo.Instance;
			rules[2] = Rule_chainFrom.Instance;
			rulesAndSubpatterns[8+2] = Rule_chainFrom.Instance;
			rules[3] = Rule_chainFromComplete.Instance;
			rulesAndSubpatterns[8+3] = Rule_chainFromComplete.Instance;
			rules[4] = Rule_createBlowball.Instance;
			rulesAndSubpatterns[8+4] = Rule_createBlowball.Instance;
			rules[5] = Rule_blowball.Instance;
			rulesAndSubpatterns[8+5] = Rule_blowball.Instance;
			rules[6] = Rule_reverseChainFromTo.Instance;
			rulesAndSubpatterns[8+6] = Rule_reverseChainFromTo.Instance;
			rules[7] = Rule_createReverseChain.Instance;
			rulesAndSubpatterns[8+7] = Rule_createReverseChain.Instance;
			rules[8] = Rule_chainFromToReverse.Instance;
			rulesAndSubpatterns[8+8] = Rule_chainFromToReverse.Instance;
			rules[9] = Rule_chainFromToReverseToCommon.Instance;
			rulesAndSubpatterns[8+9] = Rule_chainFromToReverseToCommon.Instance;
			rules[10] = Rule_reverseChainFromToToCommon.Instance;
			rulesAndSubpatterns[8+10] = Rule_reverseChainFromToToCommon.Instance;
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
	}


    public class PatternAction_ChainFromTo : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromTo(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromTo.Instance.patternGraph;
        }

        public static PatternAction_ChainFromTo getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromTo_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromTo.Match_ChainFromTo patternpath_match_ChainFromTo = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromTo_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
            // SubPreset ChainFromTo_node_to 
            GRGEN_LGSP.LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromTo.Match_ChainFromTo match = new Pattern_ChainFromTo.Match_ChainFromTo();
                    match._node_from = candidate_ChainFromTo_node_from;
                    match._node_to = candidate_ChainFromTo_node_to;
                    match._alt_0 = (Pattern_ChainFromTo.IMatch_ChainFromTo_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_ChainFromTo_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromTo_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromTo_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromTo.Match_ChainFromTo_alt_0_rec patternpath_match_ChainFromTo_alt_0_rec = null;
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_node_from.outhead;
                if(head_candidate_ChainFromTo_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromTo_alt_0_base_edge__edge0 = head_candidate_ChainFromTo_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromTo_alt_0_base_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ChainFromTo_alt_0_base_edge__edge0.target != candidate_ChainFromTo_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                                candidate_ChainFromTo_alt_0_base_edge__edge0.flags = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_base_edge__edge0.flags = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromTo_alt_0_base_edge__edge0.flags = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0.outNext) != head_candidate_ChainFromTo_alt_0_base_edge__edge0 );
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_node_from.outhead;
                if(head_candidate_ChainFromTo_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromTo_alt_0_rec_edge__edge0 = head_candidate_ChainFromTo_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromTo_alt_0_rec_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromTo_alt_0_rec_node_intermediate from ChainFromTo_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromTo_alt_0_rec_node_intermediate = candidate_ChainFromTo_alt_0_rec_edge__edge0.target;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromTo_alt_0_rec_node_intermediate))
                            && candidate_ChainFromTo_alt_0_rec_node_intermediate==candidate_ChainFromTo_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFromTo taskFor__subpattern0 = PatternAction_ChainFromTo.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ChainFromTo_node_from = candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        taskFor__subpattern0.ChainFromTo_node_to = candidate_ChainFromTo_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
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
                                match.__subpattern0 = (@Pattern_ChainFromTo.Match_ChainFromTo)currentFoundPartialMatch.Pop();
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
                                candidate_ChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                                candidate_ChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                            candidate_ChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromTo_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFrom : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFrom(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFrom.Instance.patternGraph;
        }

        public static PatternAction_ChainFrom getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFrom.Match_ChainFrom patternpath_match_ChainFrom = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFrom_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFrom_node_from = ChainFrom_node_from;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFrom.Match_ChainFrom match = new Pattern_ChainFrom.Match_ChainFrom();
                    match._node_from = candidate_ChainFrom_node_from;
                    match._alt_0 = (Pattern_ChainFrom.IMatch_ChainFrom_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_ChainFrom_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFrom_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFrom.Match_ChainFrom_alt_0_rec patternpath_match_ChainFrom_alt_0_rec = null;
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_node_from.outhead;
                if(head_candidate_ChainFrom_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFrom_alt_0_rec_edge__edge0 = head_candidate_ChainFrom_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFrom_alt_0_rec_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFrom_alt_0_rec_node_to from ChainFrom_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFrom_alt_0_rec_node_to = candidate_ChainFrom_alt_0_rec_edge__edge0.target;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ChainFrom_alt_0_rec_node_to.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFrom_alt_0_rec_node_to)))
                        {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_node_to.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFrom taskFor__subpattern0 = PatternAction_ChainFrom.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ChainFrom_node_from = candidate_ChainFrom_alt_0_rec_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFrom_alt_0_rec_node_to = candidate_ChainFrom_alt_0_rec_node_to.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFrom_alt_0_rec_node_to.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ChainFrom.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFrom.Match_ChainFrom_alt_0_rec match = new Pattern_ChainFrom.Match_ChainFrom_alt_0_rec();
                                match._node_from = candidate_ChainFrom_node_from;
                                match._node_to = candidate_ChainFrom_alt_0_rec_node_to;
                                match._edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0;
                                match.__subpattern0 = (@Pattern_ChainFrom.Match_ChainFrom)currentFoundPartialMatch.Pop();
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
                                candidate_ChainFrom_alt_0_rec_edge__edge0.flags = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                                candidate_ChainFrom_alt_0_rec_node_to.flags = candidate_ChainFrom_alt_0_rec_node_to.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFrom_alt_0_rec_edge__edge0.flags = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                            candidate_ChainFrom_alt_0_rec_node_to.flags = candidate_ChainFrom_alt_0_rec_node_to.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFrom_alt_0_rec_node_to.flags = candidate_ChainFrom_alt_0_rec_node_to.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.flags = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFrom_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromComplete : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromComplete(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromComplete.Instance.patternGraph;
        }

        public static PatternAction_ChainFromComplete getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromComplete.Match_ChainFromComplete patternpath_match_ChainFromComplete = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromComplete_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromComplete.Match_ChainFromComplete match = new Pattern_ChainFromComplete.Match_ChainFromComplete();
                    match._node_from = candidate_ChainFromComplete_node_from;
                    match._alt_0 = (Pattern_ChainFromComplete.IMatch_ChainFromComplete_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_ChainFromComplete_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromComplete_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_rec patternpath_match_ChainFromComplete_alt_0_rec = null;
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
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    uint prev_base__candidate_ChainFromComplete_node_from;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev_base__candidate_ChainFromComplete_node_from = candidate_ChainFromComplete_node_from.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_ChainFromComplete_node_from.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev_base__candidate_ChainFromComplete_node_from = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_ChainFromComplete_node_from) ? 1U : 0U;
                        if(prev_base__candidate_ChainFromComplete_node_from == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_ChainFromComplete_node_from,candidate_ChainFromComplete_node_from);
                    }
                    // Extend Outgoing ChainFromComplete_alt_0_base_neg_0_edge__edge0 from ChainFromComplete_node_from 
                    GRGEN_LGSP.LGSPEdge head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_node_from.outhead;
                    if(head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target ChainFromComplete_alt_0_base_neg_0_node__node0 from ChainFromComplete_alt_0_base_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_alt_0_base_neg_0_node__node0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.target;
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_alt_0_base_neg_0_node__node0)))
                            {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ChainFromComplete_node_from.flags = candidate_ChainFromComplete_node_from.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_base__candidate_ChainFromComplete_node_from;
                            } else { 
                                if(prev_base__candidate_ChainFromComplete_node_from == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ChainFromComplete_node_from);
                                }
                            }
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label0;
                        }
                        while( (candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.outNext) != head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_ChainFromComplete_node_from.flags = candidate_ChainFromComplete_node_from.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_base__candidate_ChainFromComplete_node_from;
                    } else { 
                        if(prev_base__candidate_ChainFromComplete_node_from == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ChainFromComplete_node_from);
                        }
                    }
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                    }
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_node_from.outhead;
                if(head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromComplete_alt_0_rec_edge__edge0 = head_candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromComplete_alt_0_rec_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromComplete_alt_0_rec_node_to from ChainFromComplete_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromComplete_alt_0_rec_node_to = candidate_ChainFromComplete_alt_0_rec_edge__edge0.target;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ChainFromComplete_alt_0_rec_node_to.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_alt_0_rec_node_to)))
                        {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_node_to.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFromComplete taskFor__subpattern0 = PatternAction_ChainFromComplete.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ChainFromComplete_node_from = candidate_ChainFromComplete_alt_0_rec_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to = candidate_ChainFromComplete_alt_0_rec_node_to.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromComplete_alt_0_rec_node_to.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ChainFromComplete.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_rec match = new Pattern_ChainFromComplete.Match_ChainFromComplete_alt_0_rec();
                                match._node_from = candidate_ChainFromComplete_node_from;
                                match._node_to = candidate_ChainFromComplete_alt_0_rec_node_to;
                                match._edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                                match.__subpattern0 = (@Pattern_ChainFromComplete.Match_ChainFromComplete)currentFoundPartialMatch.Pop();
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
                                candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                                candidate_ChainFromComplete_alt_0_rec_node_to.flags = candidate_ChainFromComplete_alt_0_rec_node_to.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                            candidate_ChainFromComplete_alt_0_rec_node_to.flags = candidate_ChainFromComplete_alt_0_rec_node_to.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFromComplete_alt_0_rec_node_to.flags = candidate_ChainFromComplete_alt_0_rec_node_to.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_Blowball : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Blowball(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Blowball.Instance.patternGraph;
        }

        public static PatternAction_Blowball getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Blowball.Match_Blowball patternpath_match_Blowball = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Blowball_node_head 
            GRGEN_LGSP.LGSPNode candidate_Blowball_node_head = Blowball_node_head;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Blowball.Match_Blowball match = new Pattern_Blowball.Match_Blowball();
                    match._node_head = candidate_Blowball_node_head;
                    match._alt_0 = (Pattern_Blowball.IMatch_Blowball_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_Blowball_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_Blowball_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Blowball.Match_Blowball_alt_0_further patternpath_match_Blowball_alt_0_further = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Blowball_alt_0_end 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@end];
                // SubPreset Blowball_node_head 
                GRGEN_LGSP.LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    uint prev_end__candidate_Blowball_node_head;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev_end__candidate_Blowball_node_head = candidate_Blowball_node_head.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_Blowball_node_head.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev_end__candidate_Blowball_node_head = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Blowball_node_head) ? 1U : 0U;
                        if(prev_end__candidate_Blowball_node_head == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Blowball_node_head,candidate_Blowball_node_head);
                    }
                    // Extend Outgoing Blowball_alt_0_end_neg_0_edge__edge0 from Blowball_node_head 
                    GRGEN_LGSP.LGSPEdge head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_node_head.outhead;
                    if(head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_Blowball_alt_0_end_neg_0_edge__edge0 = head_candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_Blowball_alt_0_end_neg_0_edge__edge0.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target Blowball_alt_0_end_neg_0_node__node0 from Blowball_alt_0_end_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_Blowball_alt_0_end_neg_0_node__node0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.target;
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Blowball_alt_0_end_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_alt_0_end_neg_0_node__node0)))
                            {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Blowball_node_head.flags = candidate_Blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_end__candidate_Blowball_node_head;
                            } else { 
                                if(prev_end__candidate_Blowball_node_head == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Blowball_node_head);
                                }
                            }
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.outNext) != head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_Blowball_node_head.flags = candidate_Blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_end__candidate_Blowball_node_head;
                    } else { 
                        if(prev_end__candidate_Blowball_node_head == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Blowball_node_head);
                        }
                    }
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                    }
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
                    goto label4;
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
            // Alternative case Blowball_alt_0_further 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@further];
                // SubPreset Blowball_node_head 
                GRGEN_LGSP.LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // Extend Outgoing Blowball_alt_0_further_edge__edge0 from Blowball_node_head 
                GRGEN_LGSP.LGSPEdge head_candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_node_head.outhead;
                if(head_candidate_Blowball_alt_0_further_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Blowball_alt_0_further_edge__edge0 = head_candidate_Blowball_alt_0_further_edge__edge0;
                    do
                    {
                        if(candidate_Blowball_alt_0_further_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target Blowball_alt_0_further_node__node0 from Blowball_alt_0_further_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_edge__edge0.target;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Blowball_alt_0_further_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_alt_0_further_node__node0)))
                        {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_Blowball taskFor__subpattern0 = PatternAction_Blowball.getNewTask(graph, openTasks);
                        taskFor__subpattern0.Blowball_node_head = candidate_Blowball_node_head;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                        prevGlobal__candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                        prevGlobal__candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_Blowball.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_Blowball.Match_Blowball_alt_0_further match = new Pattern_Blowball.Match_Blowball_alt_0_further();
                                match._node_head = candidate_Blowball_node_head;
                                match._node__node0 = candidate_Blowball_alt_0_further_node__node0;
                                match._edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0;
                                match.__subpattern0 = (@Pattern_Blowball.Match_Blowball)currentFoundPartialMatch.Pop();
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
                                candidate_Blowball_alt_0_further_edge__edge0.flags = candidate_Blowball_alt_0_further_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                                candidate_Blowball_alt_0_further_node__node0.flags = candidate_Blowball_alt_0_further_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_Blowball_alt_0_further_edge__edge0.flags = candidate_Blowball_alt_0_further_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                            candidate_Blowball_alt_0_further_node__node0.flags = candidate_Blowball_alt_0_further_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                            continue;
                        }
                        candidate_Blowball_alt_0_further_node__node0.flags = candidate_Blowball_alt_0_further_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                        candidate_Blowball_alt_0_further_edge__edge0.flags = candidate_Blowball_alt_0_further_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                    }
                    while( (candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.outNext) != head_candidate_Blowball_alt_0_further_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ReverseChainFromTo : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ReverseChainFromTo(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ReverseChainFromTo.Instance.patternGraph;
        }

        public static PatternAction_ReverseChainFromTo getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_from;
        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ReverseChainFromTo.Match_ReverseChainFromTo patternpath_match_ReverseChainFromTo = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ReverseChainFromTo_node_from 
            GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_from = ReverseChainFromTo_node_from;
            // SubPreset ReverseChainFromTo_node_to 
            GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_node_to = ReverseChainFromTo_node_to;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ReverseChainFromTo.Match_ReverseChainFromTo match = new Pattern_ReverseChainFromTo.Match_ReverseChainFromTo();
                    match._node_from = candidate_ReverseChainFromTo_node_from;
                    match._node_to = candidate_ReverseChainFromTo_node_to;
                    match._alt_0 = (Pattern_ReverseChainFromTo.IMatch_ReverseChainFromTo_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_ReverseChainFromTo_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ReverseChainFromTo_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_to;
        public GRGEN_LGSP.LGSPNode ReverseChainFromTo_node_from;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ReverseChainFromTo.Match_ReverseChainFromTo_alt_0_rec patternpath_match_ReverseChainFromTo_alt_0_rec = null;
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
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_node_to.outhead;
                if(head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromTo_alt_0_base_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ReverseChainFromTo_alt_0_base_edge__edge0.target != candidate_ReverseChainFromTo_node_from) {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                                candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.outNext) != head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 );
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
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_node_from.inhead;
                if(head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Source ReverseChainFromTo_alt_0_rec_node_intermediate from ReverseChainFromTo_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ReverseChainFromTo_alt_0_rec_node_intermediate = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.source;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ReverseChainFromTo_alt_0_rec_node_intermediate))
                            && candidate_ReverseChainFromTo_alt_0_rec_node_intermediate==candidate_ReverseChainFromTo_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ReverseChainFromTo taskFor__subpattern0 = PatternAction_ReverseChainFromTo.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ReverseChainFromTo_node_from = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        taskFor__subpattern0.ReverseChainFromTo_node_to = candidate_ReverseChainFromTo_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
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
                                match.__subpattern0 = (@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo)currentFoundPartialMatch.Pop();
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
                                candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                                candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                            candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.inNext) != head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromToReverse : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromToReverse(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromToReverse.Instance.patternGraph;
        }

        public static PatternAction_ChainFromToReverse getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromToReverse.Match_ChainFromToReverse patternpath_match_ChainFromToReverse = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromToReverse_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_from = ChainFromToReverse_node_from;
            // SubPreset ChainFromToReverse_node_to 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_node_to = ChainFromToReverse_node_to;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromToReverse.Match_ChainFromToReverse match = new Pattern_ChainFromToReverse.Match_ChainFromToReverse();
                    match._node_from = candidate_ChainFromToReverse_node_from;
                    match._node_to = candidate_ChainFromToReverse_node_to;
                    match._alt_0 = (Pattern_ChainFromToReverse.IMatch_ChainFromToReverse_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_ChainFromToReverse_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromToReverse_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverse_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromToReverse.Match_ChainFromToReverse_alt_0_rec patternpath_match_ChainFromToReverse_alt_0_rec = null;
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_node_from.outhead;
                if(head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverse_alt_0_base_edge__edge0 = head_candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverse_alt_0_base_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ChainFromToReverse_alt_0_base_edge__edge0.target != candidate_ChainFromToReverse_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                                candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0.outNext) != head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 );
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_node_from.outhead;
                if(head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverse_alt_0_rec_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromToReverse_alt_0_rec_node_intermediate from ChainFromToReverse_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromToReverse_alt_0_rec_node_intermediate = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.target;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromToReverse_alt_0_rec_node_intermediate))
                            && candidate_ChainFromToReverse_alt_0_rec_node_intermediate==candidate_ChainFromToReverse_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for cftr
                        PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(graph, openTasks);
                        taskFor_cftr.ChainFromToReverse_node_from = candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        taskFor_cftr.ChainFromToReverse_node_to = candidate_ChainFromToReverse_node_to;
                        openTasks.Push(taskFor_cftr);
                        uint prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                                candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                                candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                            candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromToReverseToCommon : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ChainFromToReverseToCommon(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromToReverseToCommon.Instance.patternGraph;
        }

        public static PatternAction_ChainFromToReverseToCommon getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon patternpath_match_ChainFromToReverseToCommon = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromToReverseToCommon_node_from 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_from = ChainFromToReverseToCommon_node_from;
            // SubPreset ChainFromToReverseToCommon_node_to 
            GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_node_to = ChainFromToReverseToCommon_node_to;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon match = new Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon();
                    match._node_from = candidate_ChainFromToReverseToCommon_node_from;
                    match._node_to = candidate_ChainFromToReverseToCommon_node_to;
                    match._alt_0 = (Pattern_ChainFromToReverseToCommon.IMatch_ChainFromToReverseToCommon_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_ChainFromToReverseToCommon_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromToReverseToCommon_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ChainFromToReverseToCommon_node_to;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ChainFromToReverseToCommon.Match_ChainFromToReverseToCommon_alt_0_rec patternpath_match_ChainFromToReverseToCommon_alt_0_rec = null;
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_node_from.outhead;
                if(head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.target != candidate_ChainFromToReverseToCommon_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                                candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.outNext) != head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 );
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
                GRGEN_LGSP.LGSPEdge head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_node_from.outhead;
                if(head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromToReverseToCommon_alt_0_rec_node_intermediate from ChainFromToReverseToCommon_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.target;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate))
                            && candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate==candidate_ChainFromToReverseToCommon_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for cftrtc
                        PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(graph, openTasks);
                        taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_ChainFromToReverseToCommon_node_to;
                        openTasks.Push(taskFor_cftrtc);
                        uint prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                                candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                                candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                            candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ReverseChainFromToToCommon : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ReverseChainFromToToCommon(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ReverseChainFromToToCommon.Instance.patternGraph;
        }

        public static PatternAction_ReverseChainFromToToCommon getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
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

        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_to;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_common;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon patternpath_match_ReverseChainFromToToCommon = null;
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
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon match = new Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon();
                    match._node_from = candidate_ReverseChainFromToToCommon_node_from;
                    match._node_to = candidate_ReverseChainFromToToCommon_node_to;
                    match._node_common = candidate_ReverseChainFromToToCommon_node_common;
                    match._alt_0 = (Pattern_ReverseChainFromToToCommon.IMatch_ReverseChainFromToToCommon_alt_0)currentFoundPartialMatch.Pop();
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
        private AlternativeAction_ReverseChainFromToToCommon_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ReverseChainFromToToCommon_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
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

        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_to;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_from;
        public GRGEN_LGSP.LGSPNode ReverseChainFromToToCommon_node_common;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon_alt_0_rec patternpath_match_ReverseChainFromToToCommon_alt_0_rec = null;
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
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_node_to.outhead;
                if(head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.target != candidate_ReverseChainFromToToCommon_node_from) {
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0,candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                        }
                        // Extend Outgoing ReverseChainFromToToCommon_alt_0_base_edge__edge1 from ReverseChainFromToToCommon_node_from 
                        GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_node_from.outhead;
                        if(head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                            do
                            {
                                if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.type.TypeID!=1) {
                                    continue;
                                }
                                if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.target != candidate_ReverseChainFromToToCommon_node_common) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1)))
                                {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        } else { 
                                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        } else { 
                                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                    candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                    continue;
                                }
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                            }
                            while( (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.outNext) != head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                        } else { 
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.outNext) != head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 );
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
                GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_node_from.inhead;
                if(head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                    do
                    {
                        if(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0,candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                        }
                        // Implicit Source ReverseChainFromToToCommon_alt_0_rec_node_intermediate from ReverseChainFromToToCommon_alt_0_rec_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.source;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate))
                            && (candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate==candidate_ReverseChainFromToToCommon_node_from
                                || candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate==candidate_ReverseChainFromToToCommon_node_common
                                )
                            )
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                            } else { 
                                if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                            } else { 
                                if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing ReverseChainFromToToCommon_alt_0_rec_edge__edge1 from ReverseChainFromToToCommon_node_from 
                        GRGEN_LGSP.LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_node_from.outhead;
                        if(head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                            do
                            {
                                if(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.type.TypeID!=1) {
                                    continue;
                                }
                                if(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.target != candidate_ReverseChainFromToToCommon_node_common) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1)))
                                {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Pop subpattern matching task for _subpattern0
                                openTasks.Pop();
                                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
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
                                        match.__subpattern0 = (@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon)currentFoundPartialMatch.Pop();
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
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        } else { 
                                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                    continue;
                                }
                                candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                            }
                            while( (candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.outNext) != head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                        } else { 
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
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

    public class Action_createChain : GRGEN_LGSP.LGSPAction
    {
        public Action_createChain() {
            rulePattern = Rule_createChain.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createChain.Match_createChain>(this);
        }

        public override string Name { get { return "createChain"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createChain.Match_createChain> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createChain instance = new Action_createChain();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
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
    }

    public class Action_chainFromTo : GRGEN_LGSP.LGSPAction
    {
        public Action_chainFromTo() {
            rulePattern = Rule_chainFromTo.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromTo.Match_chainFromTo>(this);
        }

        public override string Name { get { return "chainFromTo"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromTo.Match_chainFromTo> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_chainFromTo instance = new Action_chainFromTo();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_chainFromTo.Match_chainFromTo patternpath_match_chainFromTo = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromTo_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromTo_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_chainFromTo_node_beg == null) {
                MissingPreset_chainFromTo_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_chainFromTo_node_beg;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_chainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_chainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_chainFromTo_node_beg) ? 1U : 0U;
                if(prev__candidate_chainFromTo_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_chainFromTo_node_beg,candidate_chainFromTo_node_beg);
            }
            // Preset chainFromTo_node_end 
            GRGEN_LGSP.LGSPNode candidate_chainFromTo_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_chainFromTo_node_end == null) {
                MissingPreset_chainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromTo_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
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
            prevGlobal__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_chainFromTo_node_end;
            prevGlobal__candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromTo_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromTo.Match_chainFromTo match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromTo_node_beg;
                    match._node_end = candidate_chainFromTo_node_end;
                    match.__subpattern0 = (@Pattern_ChainFromTo.Match_ChainFromTo)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
            candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
            } else { 
                if(prev__candidate_chainFromTo_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_chainFromTo_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_chainFromTo.Match_chainFromTo patternpath_match_chainFromTo = null;
            // Lookup chainFromTo_node_beg 
            int type_id_candidate_chainFromTo_node_beg = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFromTo_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromTo_node_beg], candidate_chainFromTo_node_beg = head_candidate_chainFromTo_node_beg.typeNext; candidate_chainFromTo_node_beg != head_candidate_chainFromTo_node_beg; candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.typeNext)
            {
                uint prev__candidate_chainFromTo_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_chainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_chainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_chainFromTo_node_beg) ? 1U : 0U;
                    if(prev__candidate_chainFromTo_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_chainFromTo_node_beg,candidate_chainFromTo_node_beg);
                }
                // Preset chainFromTo_node_end 
                GRGEN_LGSP.LGSPNode candidate_chainFromTo_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_chainFromTo_node_end == null) {
                    MissingPreset_chainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromTo_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_chainFromTo_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end)))
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
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
                prevGlobal__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromTo_node_end;
                prevGlobal__candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_chainFromTo.Match_chainFromTo match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_chainFromTo_node_beg;
                        match._node_end = candidate_chainFromTo_node_end;
                        match.__subpattern0 = (@Pattern_ChainFromTo.Match_ChainFromTo)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_chainFromTo_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_chainFromTo_node_end(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_chainFromTo_node_beg)
        {
            int negLevel = 0;
            Rule_chainFromTo.Match_chainFromTo patternpath_match_chainFromTo = null;
            // Lookup chainFromTo_node_end 
            int type_id_candidate_chainFromTo_node_end = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFromTo_node_end = graph.nodesByTypeHeads[type_id_candidate_chainFromTo_node_end], candidate_chainFromTo_node_end = head_candidate_chainFromTo_node_end.typeNext; candidate_chainFromTo_node_end != head_candidate_chainFromTo_node_end; candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.typeNext)
            {
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end)))
                {
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromTo taskFor__subpattern0 = PatternAction_ChainFromTo.getNewTask(graph, openTasks);
                taskFor__subpattern0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
                taskFor__subpattern0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_chainFromTo_node_beg;
                prevGlobal__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromTo_node_end;
                prevGlobal__candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_chainFromTo.Match_chainFromTo match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_chainFromTo_node_beg;
                        match._node_end = candidate_chainFromTo_node_end;
                        match.__subpattern0 = (@Pattern_ChainFromTo.Match_ChainFromTo)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                        return;
                    }
                    candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                    continue;
                }
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
            }
            return;
        }
    }

    public class Action_chainFrom : GRGEN_LGSP.LGSPAction
    {
        public Action_chainFrom() {
            rulePattern = Rule_chainFrom.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFrom.Match_chainFrom>(this);
        }

        public override string Name { get { return "chainFrom"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFrom.Match_chainFrom> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_chainFrom instance = new Action_chainFrom();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_chainFrom.Match_chainFrom patternpath_match_chainFrom = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFrom_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFrom_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_chainFrom_node_beg == null) {
                MissingPreset_chainFrom_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
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
            prevGlobal__candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFrom_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ChainFrom.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFrom.Match_chainFrom match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFrom_node_beg;
                    match.__subpattern0 = (@Pattern_ChainFrom.Match_ChainFrom)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                    return matches;
                }
                candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                return matches;
            }
            candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
            return matches;
        }
        public void MissingPreset_chainFrom_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_chainFrom.Match_chainFrom patternpath_match_chainFrom = null;
            // Lookup chainFrom_node_beg 
            int type_id_candidate_chainFrom_node_beg = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFrom_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFrom_node_beg], candidate_chainFrom_node_beg = head_candidate_chainFrom_node_beg.typeNext; candidate_chainFrom_node_beg != head_candidate_chainFrom_node_beg; candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFrom taskFor__subpattern0 = PatternAction_ChainFrom.getNewTask(graph, openTasks);
                taskFor__subpattern0.ChainFrom_node_from = candidate_chainFrom_node_beg;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_chainFrom_node_beg;
                prevGlobal__candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFrom_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFrom.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_chainFrom.Match_chainFrom match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_chainFrom_node_beg;
                        match.__subpattern0 = (@Pattern_ChainFrom.Match_ChainFrom)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                        return;
                    }
                    candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                    continue;
                }
                candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
            }
            return;
        }
    }

    public class Action_chainFromComplete : GRGEN_LGSP.LGSPAction
    {
        public Action_chainFromComplete() {
            rulePattern = Rule_chainFromComplete.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromComplete.Match_chainFromComplete>(this);
        }

        public override string Name { get { return "chainFromComplete"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromComplete.Match_chainFromComplete> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_chainFromComplete instance = new Action_chainFromComplete();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_chainFromComplete.Match_chainFromComplete patternpath_match_chainFromComplete = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromComplete_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromComplete_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_chainFromComplete_node_beg == null) {
                MissingPreset_chainFromComplete_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
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
            prevGlobal__candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromComplete_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ChainFromComplete.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_chainFromComplete.Match_chainFromComplete match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_chainFromComplete_node_beg;
                    match.__subpattern0 = (@Pattern_ChainFromComplete.Match_ChainFromComplete)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                    return matches;
                }
                candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                return matches;
            }
            candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
            return matches;
        }
        public void MissingPreset_chainFromComplete_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_chainFromComplete.Match_chainFromComplete patternpath_match_chainFromComplete = null;
            // Lookup chainFromComplete_node_beg 
            int type_id_candidate_chainFromComplete_node_beg = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFromComplete_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromComplete_node_beg], candidate_chainFromComplete_node_beg = head_candidate_chainFromComplete_node_beg.typeNext; candidate_chainFromComplete_node_beg != head_candidate_chainFromComplete_node_beg; candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromComplete taskFor__subpattern0 = PatternAction_ChainFromComplete.getNewTask(graph, openTasks);
                taskFor__subpattern0.ChainFromComplete_node_from = candidate_chainFromComplete_node_beg;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_chainFromComplete_node_beg;
                prevGlobal__candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromComplete_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFromComplete.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_chainFromComplete.Match_chainFromComplete match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_chainFromComplete_node_beg;
                        match.__subpattern0 = (@Pattern_ChainFromComplete.Match_ChainFromComplete)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                        return;
                    }
                    candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                    continue;
                }
                candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
            }
            return;
        }
    }

    public class Action_createBlowball : GRGEN_LGSP.LGSPAction
    {
        public Action_createBlowball() {
            rulePattern = Rule_createBlowball.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createBlowball.Match_createBlowball>(this);
        }

        public override string Name { get { return "createBlowball"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createBlowball.Match_createBlowball> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createBlowball instance = new Action_createBlowball();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
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
    }

    public class Action_blowball : GRGEN_LGSP.LGSPAction
    {
        public Action_blowball() {
            rulePattern = Rule_blowball.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_blowball.Match_blowball>(this);
        }

        public override string Name { get { return "blowball"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_blowball.Match_blowball> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_blowball instance = new Action_blowball();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_blowball.Match_blowball patternpath_match_blowball = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset blowball_node_head 
            GRGEN_LGSP.LGSPNode candidate_blowball_node_head = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_blowball_node_head == null) {
                MissingPreset_blowball_node_head(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
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
            prevGlobal__candidate_blowball_node_head = candidate_blowball_node_head.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_blowball_node_head.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_Blowball.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_blowball.Match_blowball match = matches.GetNextUnfilledPosition();
                    match._node_head = candidate_blowball_node_head;
                    match.__subpattern0 = (@Pattern_Blowball.Match_Blowball)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                    return matches;
                }
                candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                return matches;
            }
            candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
            return matches;
        }
        public void MissingPreset_blowball_node_head(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_blowball.Match_blowball patternpath_match_blowball = null;
            // Lookup blowball_node_head 
            int type_id_candidate_blowball_node_head = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_blowball_node_head = graph.nodesByTypeHeads[type_id_candidate_blowball_node_head], candidate_blowball_node_head = head_candidate_blowball_node_head.typeNext; candidate_blowball_node_head != head_candidate_blowball_node_head; candidate_blowball_node_head = candidate_blowball_node_head.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_Blowball taskFor__subpattern0 = PatternAction_Blowball.getNewTask(graph, openTasks);
                taskFor__subpattern0.Blowball_node_head = candidate_blowball_node_head;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_blowball_node_head;
                prevGlobal__candidate_blowball_node_head = candidate_blowball_node_head.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_blowball_node_head.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_Blowball.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_blowball.Match_blowball match = matches.GetNextUnfilledPosition();
                        match._node_head = candidate_blowball_node_head;
                        match.__subpattern0 = (@Pattern_Blowball.Match_Blowball)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                        return;
                    }
                    candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                    continue;
                }
                candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
            }
            return;
        }
    }

    public class Action_reverseChainFromTo : GRGEN_LGSP.LGSPAction
    {
        public Action_reverseChainFromTo() {
            rulePattern = Rule_reverseChainFromTo.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromTo.Match_reverseChainFromTo>(this);
        }

        public override string Name { get { return "reverseChainFromTo"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromTo.Match_reverseChainFromTo> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_reverseChainFromTo instance = new Action_reverseChainFromTo();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_reverseChainFromTo.Match_reverseChainFromTo patternpath_match_reverseChainFromTo = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset reverseChainFromTo_node_beg 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromTo_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_reverseChainFromTo_node_beg == null) {
                MissingPreset_reverseChainFromTo_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_reverseChainFromTo_node_beg;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_reverseChainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_reverseChainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_reverseChainFromTo_node_beg) ? 1U : 0U;
                if(prev__candidate_reverseChainFromTo_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_reverseChainFromTo_node_beg,candidate_reverseChainFromTo_node_beg);
            }
            // Preset reverseChainFromTo_node_end 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromTo_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_reverseChainFromTo_node_end == null) {
                MissingPreset_reverseChainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromTo_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_end)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
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
            prevGlobal__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_reverseChainFromTo_node_end;
            prevGlobal__candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromTo_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_reverseChainFromTo.Match_reverseChainFromTo match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_reverseChainFromTo_node_beg;
                    match._node_end = candidate_reverseChainFromTo_node_end;
                    match.__subpattern0 = (@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                    }
                }
                return matches;
            }
            candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
            candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
            } else { 
                if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_reverseChainFromTo_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_reverseChainFromTo.Match_reverseChainFromTo patternpath_match_reverseChainFromTo = null;
            // Lookup reverseChainFromTo_node_beg 
            int type_id_candidate_reverseChainFromTo_node_beg = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_reverseChainFromTo_node_beg = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromTo_node_beg], candidate_reverseChainFromTo_node_beg = head_candidate_reverseChainFromTo_node_beg.typeNext; candidate_reverseChainFromTo_node_beg != head_candidate_reverseChainFromTo_node_beg; candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.typeNext)
            {
                uint prev__candidate_reverseChainFromTo_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_reverseChainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_reverseChainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_reverseChainFromTo_node_beg) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromTo_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_reverseChainFromTo_node_beg,candidate_reverseChainFromTo_node_beg);
                }
                // Preset reverseChainFromTo_node_end 
                GRGEN_LGSP.LGSPNode candidate_reverseChainFromTo_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_reverseChainFromTo_node_end == null) {
                    MissingPreset_reverseChainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromTo_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_end)))
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
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
                prevGlobal__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromTo_node_end;
                prevGlobal__candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_reverseChainFromTo.Match_reverseChainFromTo match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_reverseChainFromTo_node_beg;
                        match._node_end = candidate_reverseChainFromTo_node_end;
                        match.__subpattern0 = (@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_reverseChainFromTo_node_end(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_reverseChainFromTo_node_beg)
        {
            int negLevel = 0;
            Rule_reverseChainFromTo.Match_reverseChainFromTo patternpath_match_reverseChainFromTo = null;
            // Lookup reverseChainFromTo_node_end 
            int type_id_candidate_reverseChainFromTo_node_end = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_reverseChainFromTo_node_end = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromTo_node_end], candidate_reverseChainFromTo_node_end = head_candidate_reverseChainFromTo_node_end.typeNext; candidate_reverseChainFromTo_node_end != head_candidate_reverseChainFromTo_node_end; candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.typeNext)
            {
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_end)))
                {
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ReverseChainFromTo taskFor__subpattern0 = PatternAction_ReverseChainFromTo.getNewTask(graph, openTasks);
                taskFor__subpattern0.ReverseChainFromTo_node_from = candidate_reverseChainFromTo_node_beg;
                taskFor__subpattern0.ReverseChainFromTo_node_to = candidate_reverseChainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_reverseChainFromTo_node_beg;
                prevGlobal__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromTo_node_end;
                prevGlobal__candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_reverseChainFromTo.Match_reverseChainFromTo match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_reverseChainFromTo_node_beg;
                        match._node_end = candidate_reverseChainFromTo_node_end;
                        match.__subpattern0 = (@Pattern_ReverseChainFromTo.Match_ReverseChainFromTo)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                        return;
                    }
                    candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                    continue;
                }
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
            }
            return;
        }
    }

    public class Action_createReverseChain : GRGEN_LGSP.LGSPAction
    {
        public Action_createReverseChain() {
            rulePattern = Rule_createReverseChain.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createReverseChain.Match_createReverseChain>(this);
        }

        public override string Name { get { return "createReverseChain"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createReverseChain.Match_createReverseChain> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createReverseChain instance = new Action_createReverseChain();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
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
    }

    public class Action_chainFromToReverse : GRGEN_LGSP.LGSPAction
    {
        public Action_chainFromToReverse() {
            rulePattern = Rule_chainFromToReverse.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverse.Match_chainFromToReverse>(this);
        }

        public override string Name { get { return "chainFromToReverse"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverse.Match_chainFromToReverse> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_chainFromToReverse instance = new Action_chainFromToReverse();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_chainFromToReverse.Match_chainFromToReverse patternpath_match_chainFromToReverse = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromToReverse_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverse_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_chainFromToReverse_node_beg == null) {
                MissingPreset_chainFromToReverse_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_chainFromToReverse_node_beg;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_chainFromToReverse_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_chainFromToReverse_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_chainFromToReverse_node_beg) ? 1U : 0U;
                if(prev__candidate_chainFromToReverse_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_chainFromToReverse_node_beg,candidate_chainFromToReverse_node_beg);
            }
            // Preset chainFromToReverse_node_end 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverse_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_chainFromToReverse_node_end == null) {
                MissingPreset_chainFromToReverse_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverse_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromToReverse_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_end)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
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
            prevGlobal__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverse_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_chainFromToReverse_node_end;
            prevGlobal__candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverse_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                    }
                }
                return matches;
            }
            candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
            candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
            } else { 
                if(prev__candidate_chainFromToReverse_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_chainFromToReverse_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_chainFromToReverse.Match_chainFromToReverse patternpath_match_chainFromToReverse = null;
            // Lookup chainFromToReverse_node_beg 
            int type_id_candidate_chainFromToReverse_node_beg = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFromToReverse_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverse_node_beg], candidate_chainFromToReverse_node_beg = head_candidate_chainFromToReverse_node_beg.typeNext; candidate_chainFromToReverse_node_beg != head_candidate_chainFromToReverse_node_beg; candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.typeNext)
            {
                uint prev__candidate_chainFromToReverse_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_chainFromToReverse_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_chainFromToReverse_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_chainFromToReverse_node_beg) ? 1U : 0U;
                    if(prev__candidate_chainFromToReverse_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_chainFromToReverse_node_beg,candidate_chainFromToReverse_node_beg);
                }
                // Preset chainFromToReverse_node_end 
                GRGEN_LGSP.LGSPNode candidate_chainFromToReverse_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_chainFromToReverse_node_end == null) {
                    MissingPreset_chainFromToReverse_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverse_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverse_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromToReverse_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_end)))
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
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
                prevGlobal__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverse_node_end;
                prevGlobal__candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverse_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    continue;
                }
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverse_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_chainFromToReverse_node_end(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_chainFromToReverse_node_beg)
        {
            int negLevel = 0;
            Rule_chainFromToReverse.Match_chainFromToReverse patternpath_match_chainFromToReverse = null;
            // Lookup chainFromToReverse_node_end 
            int type_id_candidate_chainFromToReverse_node_end = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFromToReverse_node_end = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverse_node_end], candidate_chainFromToReverse_node_end = head_candidate_chainFromToReverse_node_end.typeNext; candidate_chainFromToReverse_node_end != head_candidate_chainFromToReverse_node_end; candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.typeNext)
            {
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromToReverse_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_end)))
                {
                    continue;
                }
                // Push subpattern matching task for cftr
                PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(graph, openTasks);
                taskFor_cftr.ChainFromToReverse_node_from = candidate_chainFromToReverse_node_beg;
                taskFor_cftr.ChainFromToReverse_node_to = candidate_chainFromToReverse_node_end;
                openTasks.Push(taskFor_cftr);
                uint prevGlobal__candidate_chainFromToReverse_node_beg;
                prevGlobal__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverse_node_end;
                prevGlobal__candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                        return;
                    }
                    candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                    continue;
                }
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
            }
            return;
        }
    }

    public class Action_chainFromToReverseToCommon : GRGEN_LGSP.LGSPAction
    {
        public Action_chainFromToReverseToCommon() {
            rulePattern = Rule_chainFromToReverseToCommon.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon>(this);
        }

        public override string Name { get { return "chainFromToReverseToCommon"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_chainFromToReverseToCommon instance = new Action_chainFromToReverseToCommon();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon patternpath_match_chainFromToReverseToCommon = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset chainFromToReverseToCommon_node_beg 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverseToCommon_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_chainFromToReverseToCommon_node_beg == null) {
                MissingPreset_chainFromToReverseToCommon_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_chainFromToReverseToCommon_node_beg;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_chainFromToReverseToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_chainFromToReverseToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_beg) ? 1U : 0U;
                if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_chainFromToReverseToCommon_node_beg,candidate_chainFromToReverseToCommon_node_beg);
            }
            // Preset chainFromToReverseToCommon_node_end 
            GRGEN_LGSP.LGSPNode candidate_chainFromToReverseToCommon_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_chainFromToReverseToCommon_node_end == null) {
                MissingPreset_chainFromToReverseToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverseToCommon_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromToReverseToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_end)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
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
            prevGlobal__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverseToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            prevGlobal__candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverseToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                    }
                }
                return matches;
            }
            candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
            candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
            } else { 
                if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_chainFromToReverseToCommon_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon patternpath_match_chainFromToReverseToCommon = null;
            // Lookup chainFromToReverseToCommon_node_beg 
            int type_id_candidate_chainFromToReverseToCommon_node_beg = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFromToReverseToCommon_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverseToCommon_node_beg], candidate_chainFromToReverseToCommon_node_beg = head_candidate_chainFromToReverseToCommon_node_beg.typeNext; candidate_chainFromToReverseToCommon_node_beg != head_candidate_chainFromToReverseToCommon_node_beg; candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.typeNext)
            {
                uint prev__candidate_chainFromToReverseToCommon_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_chainFromToReverseToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_chainFromToReverseToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_beg) ? 1U : 0U;
                    if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_chainFromToReverseToCommon_node_beg,candidate_chainFromToReverseToCommon_node_beg);
                }
                // Preset chainFromToReverseToCommon_node_end 
                GRGEN_LGSP.LGSPNode candidate_chainFromToReverseToCommon_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_chainFromToReverseToCommon_node_end == null) {
                    MissingPreset_chainFromToReverseToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverseToCommon_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromToReverseToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_end)))
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
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
                prevGlobal__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                prevGlobal__candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    continue;
                }
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_chainFromToReverseToCommon_node_end(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_chainFromToReverseToCommon_node_beg)
        {
            int negLevel = 0;
            Rule_chainFromToReverseToCommon.Match_chainFromToReverseToCommon patternpath_match_chainFromToReverseToCommon = null;
            // Lookup chainFromToReverseToCommon_node_end 
            int type_id_candidate_chainFromToReverseToCommon_node_end = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_chainFromToReverseToCommon_node_end = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverseToCommon_node_end], candidate_chainFromToReverseToCommon_node_end = head_candidate_chainFromToReverseToCommon_node_end.typeNext; candidate_chainFromToReverseToCommon_node_end != head_candidate_chainFromToReverseToCommon_node_end; candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.typeNext)
            {
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_chainFromToReverseToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_end)))
                {
                    continue;
                }
                // Push subpattern matching task for cftrtc
                PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(graph, openTasks);
                taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_chainFromToReverseToCommon_node_beg;
                taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_chainFromToReverseToCommon_node_end;
                openTasks.Push(taskFor_cftrtc);
                uint prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                prevGlobal__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                prevGlobal__candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
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
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                        return;
                    }
                    candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                    continue;
                }
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            }
            return;
        }
    }

    public class Action_reverseChainFromToToCommon : GRGEN_LGSP.LGSPAction
    {
        public Action_reverseChainFromToToCommon() {
            rulePattern = Rule_reverseChainFromToToCommon.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon>(this);
        }

        public override string Name { get { return "reverseChainFromToToCommon"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_reverseChainFromToToCommon instance = new Action_reverseChainFromToToCommon();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon patternpath_match_reverseChainFromToToCommon = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset reverseChainFromToToCommon_node_beg 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_reverseChainFromToToCommon_node_beg == null) {
                MissingPreset_reverseChainFromToToCommon_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_reverseChainFromToToCommon_node_beg;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_reverseChainFromToToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_reverseChainFromToToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_beg) ? 1U : 0U;
                if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_reverseChainFromToToCommon_node_beg,candidate_reverseChainFromToToCommon_node_beg);
            }
            // Preset reverseChainFromToToCommon_node_end 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_reverseChainFromToToCommon_node_end == null) {
                MissingPreset_reverseChainFromToToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            uint prev__candidate_reverseChainFromToToCommon_node_end;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_reverseChainFromToToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_reverseChainFromToToCommon_node_end = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end) ? 1U : 0U;
                if(prev__candidate_reverseChainFromToToCommon_node_end == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_reverseChainFromToToCommon_node_end,candidate_reverseChainFromToToCommon_node_end);
            }
            // Preset reverseChainFromToToCommon_node_common 
            GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_common = (GRGEN_LGSP.LGSPNode) parameters[2];
            if(candidate_reverseChainFromToToCommon_node_common == null) {
                MissingPreset_reverseChainFromToToCommon_node_common(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg, candidate_reverseChainFromToToCommon_node_end);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
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
            prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromToToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
            prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromToToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromToToCommon_node_common.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_reverseChainFromToToCommon_node_beg;
                    match._node_end = candidate_reverseChainFromToToCommon_node_end;
                    match._node_common = candidate_reverseChainFromToToCommon_node_common;
                    match.__subpattern0 = (@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
            candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
            } else { 
                if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                }
            }
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
            } else { 
                if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_reverseChainFromToToCommon_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon patternpath_match_reverseChainFromToToCommon = null;
            // Lookup reverseChainFromToToCommon_node_beg 
            int type_id_candidate_reverseChainFromToToCommon_node_beg = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_reverseChainFromToToCommon_node_beg = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromToToCommon_node_beg], candidate_reverseChainFromToToCommon_node_beg = head_candidate_reverseChainFromToToCommon_node_beg.typeNext; candidate_reverseChainFromToToCommon_node_beg != head_candidate_reverseChainFromToToCommon_node_beg; candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.typeNext)
            {
                uint prev__candidate_reverseChainFromToToCommon_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_reverseChainFromToToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_reverseChainFromToToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_beg) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_reverseChainFromToToCommon_node_beg,candidate_reverseChainFromToToCommon_node_beg);
                }
                // Preset reverseChainFromToToCommon_node_end 
                GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_reverseChainFromToToCommon_node_end == null) {
                    MissingPreset_reverseChainFromToToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end)))
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                uint prev__candidate_reverseChainFromToToCommon_node_end;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_reverseChainFromToToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_reverseChainFromToToCommon_node_end = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromToToCommon_node_end == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_reverseChainFromToToCommon_node_end,candidate_reverseChainFromToToCommon_node_end);
                }
                // Preset reverseChainFromToToCommon_node_common 
                GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_common = (GRGEN_LGSP.LGSPNode) parameters[2];
                if(candidate_reverseChainFromToToCommon_node_common == null) {
                    MissingPreset_reverseChainFromToToCommon_node_common(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg, candidate_reverseChainFromToToCommon_node_end);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common)))
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
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
                prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_common.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_reverseChainFromToToCommon_node_beg;
                        match._node_end = candidate_reverseChainFromToToCommon_node_end;
                        match._node_common = candidate_reverseChainFromToToCommon_node_common;
                        match.__subpattern0 = (@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_reverseChainFromToToCommon_node_end(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_beg)
        {
            int negLevel = 0;
            Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon patternpath_match_reverseChainFromToToCommon = null;
            // Lookup reverseChainFromToToCommon_node_end 
            int type_id_candidate_reverseChainFromToToCommon_node_end = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_reverseChainFromToToCommon_node_end = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromToToCommon_node_end], candidate_reverseChainFromToToCommon_node_end = head_candidate_reverseChainFromToToCommon_node_end.typeNext; candidate_reverseChainFromToToCommon_node_end != head_candidate_reverseChainFromToToCommon_node_end; candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.typeNext)
            {
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end)))
                {
                    continue;
                }
                uint prev__candidate_reverseChainFromToToCommon_node_end;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_reverseChainFromToToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_reverseChainFromToToCommon_node_end = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromToToCommon_node_end == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_reverseChainFromToToCommon_node_end,candidate_reverseChainFromToToCommon_node_end);
                }
                // Preset reverseChainFromToToCommon_node_common 
                GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_common = (GRGEN_LGSP.LGSPNode) parameters[2];
                if(candidate_reverseChainFromToToCommon_node_common == null) {
                    MissingPreset_reverseChainFromToToCommon_node_common(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg, candidate_reverseChainFromToToCommon_node_end);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    continue;
                }
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common)))
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
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
                prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_common.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_reverseChainFromToToCommon_node_beg;
                        match._node_end = candidate_reverseChainFromToToCommon_node_end;
                        match._node_common = candidate_reverseChainFromToToCommon_node_common;
                        match.__subpattern0 = (@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        return;
                    }
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    continue;
                }
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
            }
            return;
        }
        public void MissingPreset_reverseChainFromToToCommon_node_common(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_beg, GRGEN_LGSP.LGSPNode candidate_reverseChainFromToToCommon_node_end)
        {
            int negLevel = 0;
            Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon patternpath_match_reverseChainFromToToCommon = null;
            // Lookup reverseChainFromToToCommon_node_common 
            int type_id_candidate_reverseChainFromToToCommon_node_common = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_reverseChainFromToToCommon_node_common = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromToToCommon_node_common], candidate_reverseChainFromToToCommon_node_common = head_candidate_reverseChainFromToToCommon_node_common.typeNext; candidate_reverseChainFromToToCommon_node_common != head_candidate_reverseChainFromToToCommon_node_common; candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.typeNext)
            {
                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common)))
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
                prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_common.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_reverseChainFromToToCommon.Match_reverseChainFromToToCommon match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_reverseChainFromToToCommon_node_beg;
                        match._node_end = candidate_reverseChainFromToToCommon_node_end;
                        match._node_common = candidate_reverseChainFromToToCommon_node_common;
                        match.__subpattern0 = (@Pattern_ReverseChainFromToToCommon.Match_ReverseChainFromToToCommon)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                        return;
                    }
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    continue;
                }
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            }
            return;
        }
    }


    // class which instantiates and stores all the compiled actions of the module in a dictionary,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class RecursiveActions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public RecursiveActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public RecursiveActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer analyzer = new de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromTo.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFrom.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromComplete.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Blowball.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ReverseChainFromTo.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromToReverse.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ChainFromToReverseToCommon.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ReverseChainFromToToCommon.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_createChain.Instance);
            actions.Add("createChain", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createChain.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromTo.Instance);
            actions.Add("chainFromTo", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_chainFromTo.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFrom.Instance);
            actions.Add("chainFrom", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_chainFrom.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromComplete.Instance);
            actions.Add("chainFromComplete", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_chainFromComplete.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_createBlowball.Instance);
            actions.Add("createBlowball", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createBlowball.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_blowball.Instance);
            actions.Add("blowball", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_blowball.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_reverseChainFromTo.Instance);
            actions.Add("reverseChainFromTo", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_reverseChainFromTo.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_createReverseChain.Instance);
            actions.Add("createReverseChain", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createReverseChain.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromToReverse.Instance);
            actions.Add("chainFromToReverse", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_chainFromToReverse.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_chainFromToReverseToCommon.Instance);
            actions.Add("chainFromToReverseToCommon", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_chainFromToReverseToCommon.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_reverseChainFromToToCommon.Instance);
            actions.Add("reverseChainFromToToCommon", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_reverseChainFromToToCommon.Instance);
            analyzer.ComputeInterPatternRelations();
        }

        public override String Name { get { return "RecursiveActions"; } }
        public override String ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}