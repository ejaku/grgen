// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\alternatives\Alternatives.grg" on Thu Jan 15 21:53:57 CET 2009

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using de.unika.ipd.grGen.Model_Alternatives;

namespace de.unika.ipd.grGen.Action_Alternatives
{
	public class Pattern_toAorB : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_toAorB instance = null;
		public static Pattern_toAorB Instance { get { if (instance==null) { instance = new Pattern_toAorB(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] toAorB_node_x_AllowedTypes = null;
		public static bool[] toAorB_node_x_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] toAorB_edge_y_AllowedTypes = null;
		public static bool[] toAorB_edge_y_IsAllowedType = null;
		public enum toAorB_NodeNums { @x, };
		public enum toAorB_EdgeNums { @y, };
		public enum toAorB_VariableNums { };
		public enum toAorB_SubNums { };
		public enum toAorB_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_toAorB;

		public enum toAorB_alt_0_CaseNums { @toA, @toB, };
		public static GRGEN_LIBGR.NodeType[] toAorB_alt_0_toA_node_a_AllowedTypes = null;
		public static bool[] toAorB_alt_0_toA_node_a_IsAllowedType = null;
		public enum toAorB_alt_0_toA_NodeNums { @a, };
		public enum toAorB_alt_0_toA_EdgeNums { @y, };
		public enum toAorB_alt_0_toA_VariableNums { };
		public enum toAorB_alt_0_toA_SubNums { };
		public enum toAorB_alt_0_toA_AltNums { };


		GRGEN_LGSP.PatternGraph toAorB_alt_0_toA;

		public static GRGEN_LIBGR.NodeType[] toAorB_alt_0_toB_node_b_AllowedTypes = null;
		public static bool[] toAorB_alt_0_toB_node_b_IsAllowedType = null;
		public enum toAorB_alt_0_toB_NodeNums { @b, };
		public enum toAorB_alt_0_toB_EdgeNums { @y, };
		public enum toAorB_alt_0_toB_VariableNums { };
		public enum toAorB_alt_0_toB_SubNums { };
		public enum toAorB_alt_0_toB_AltNums { };


		GRGEN_LGSP.PatternGraph toAorB_alt_0_toB;


#if INITIAL_WARMUP
		public Pattern_toAorB()
#else
		private Pattern_toAorB()
#endif
		{
			name = "toAorB";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "toAorB_node_x", };
		}
		public override void initialize()
		{
			bool[,] toAorB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] toAorB_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode toAorB_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "toAorB_node_x", "x", toAorB_node_x_AllowedTypes, toAorB_node_x_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternEdge toAorB_edge_y = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "toAorB_edge_y", "y", toAorB_edge_y_AllowedTypes, toAorB_edge_y_IsAllowedType, 5.5F, -1);
			bool[,] toAorB_alt_0_toA_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] toAorB_alt_0_toA_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode toAorB_alt_0_toA_node_a = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "toAorB_alt_0_toA_node_a", "a", toAorB_alt_0_toA_node_a_AllowedTypes, toAorB_alt_0_toA_node_a_IsAllowedType, 5.5F, -1);
			toAorB_alt_0_toA = new GRGEN_LGSP.PatternGraph(
				"toA",
				"toAorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { toAorB_alt_0_toA_node_a }, 
				new GRGEN_LGSP.PatternEdge[] { toAorB_edge_y }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				toAorB_alt_0_toA_isNodeHomomorphicGlobal,
				toAorB_alt_0_toA_isEdgeHomomorphicGlobal
			);
			toAorB_alt_0_toA.edgeToTargetNode.Add(toAorB_edge_y, toAorB_alt_0_toA_node_a);

			bool[,] toAorB_alt_0_toB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] toAorB_alt_0_toB_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode toAorB_alt_0_toB_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "toAorB_alt_0_toB_node_b", "b", toAorB_alt_0_toB_node_b_AllowedTypes, toAorB_alt_0_toB_node_b_IsAllowedType, 5.5F, -1);
			toAorB_alt_0_toB = new GRGEN_LGSP.PatternGraph(
				"toB",
				"toAorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { toAorB_alt_0_toB_node_b }, 
				new GRGEN_LGSP.PatternEdge[] { toAorB_edge_y }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				toAorB_alt_0_toB_isNodeHomomorphicGlobal,
				toAorB_alt_0_toB_isEdgeHomomorphicGlobal
			);
			toAorB_alt_0_toB.edgeToTargetNode.Add(toAorB_edge_y, toAorB_alt_0_toB_node_b);

			GRGEN_LGSP.Alternative toAorB_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "toAorB_", new GRGEN_LGSP.PatternGraph[] { toAorB_alt_0_toA, toAorB_alt_0_toB } );

			pat_toAorB = new GRGEN_LGSP.PatternGraph(
				"toAorB",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { toAorB_node_x }, 
				new GRGEN_LGSP.PatternEdge[] { toAorB_edge_y }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { toAorB_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				toAorB_isNodeHomomorphicGlobal,
				toAorB_isEdgeHomomorphicGlobal
			);
			pat_toAorB.edgeToSourceNode.Add(toAorB_edge_y, toAorB_node_x);
			toAorB_alt_0_toA.embeddingGraph = pat_toAorB;
			toAorB_alt_0_toB.embeddingGraph = pat_toAorB;

			toAorB_node_x.PointOfDefinition = null;
			toAorB_edge_y.PointOfDefinition = pat_toAorB;
			toAorB_alt_0_toA_node_a.PointOfDefinition = toAorB_alt_0_toA;
			toAorB_alt_0_toB_node_b.PointOfDefinition = toAorB_alt_0_toB;

			patternGraph = pat_toAorB;
		}


		public void toAorB_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_x)
		{
			graph.SettingAddedNodeNames( create_toAorB_addedNodeNames );
			graph.SettingAddedEdgeNames( create_toAorB_addedEdgeNames );
		}
		private static String[] create_toAorB_addedNodeNames = new String[] {  };
		private static String[] create_toAorB_addedEdgeNames = new String[] { "y" };

		public void toAorB_Delete(GRGEN_LGSP.LGSPGraph graph, Match_toAorB curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge_y = curMatch._edge_y;
			IMatch_toAorB_alt_0 alternative_alt_0 = curMatch._alt_0;
			toAorB_alt_0_Delete(graph, alternative_alt_0);
			graph.Remove(edge_y);
		}

		public void toAorB_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_toAorB_alt_0 curMatch)
		{
			if(curMatch.Pattern == toAorB_alt_0_toA) {
				toAorB_alt_0_toA_Delete(graph, (Match_toAorB_alt_0_toA)curMatch);
				return;
			}
			else if(curMatch.Pattern == toAorB_alt_0_toB) {
				toAorB_alt_0_toB_Delete(graph, (Match_toAorB_alt_0_toB)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void toAorB_alt_0_toA_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_toAorB_alt_0_toA_addedNodeNames );
			@A node_a = @A.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_toAorB_alt_0_toA_addedEdgeNames );
		}
		private static String[] create_toAorB_alt_0_toA_addedNodeNames = new String[] { "a" };
		private static String[] create_toAorB_alt_0_toA_addedEdgeNames = new String[] { "y" };

		public void toAorB_alt_0_toA_Delete(GRGEN_LGSP.LGSPGraph graph, Match_toAorB_alt_0_toA curMatch)
		{
			GRGEN_LGSP.LGSPNode node_a = curMatch._node_a;
			graph.RemoveEdges(node_a);
			graph.Remove(node_a);
		}

		public void toAorB_alt_0_toB_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_toAorB_alt_0_toB_addedNodeNames );
			@B node_b = @B.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_toAorB_alt_0_toB_addedEdgeNames );
		}
		private static String[] create_toAorB_alt_0_toB_addedNodeNames = new String[] { "b" };
		private static String[] create_toAorB_alt_0_toB_addedEdgeNames = new String[] { "y" };

		public void toAorB_alt_0_toB_Delete(GRGEN_LGSP.LGSPGraph graph, Match_toAorB_alt_0_toB curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
		}

		static Pattern_toAorB() {
		}

		public interface IMatch_toAorB : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge_y { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_toAorB_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_toAorB_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_toAorB_alt_0_toA : IMatch_toAorB_alt_0
		{
			//Nodes
			IA node_a { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge_y { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_toAorB_alt_0_toB : IMatch_toAorB_alt_0
		{
			//Nodes
			IB node_b { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge_y { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_toAorB : GRGEN_LGSP.ListElement<Match_toAorB>, IMatch_toAorB
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public enum toAorB_NodeNums { @x, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)toAorB_NodeNums.@x: return _node_x;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge_y { get { return (GRGEN_LIBGR.IEdge)_edge_y; } }
			public GRGEN_LGSP.LGSPEdge _edge_y;
			public enum toAorB_EdgeNums { @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)toAorB_EdgeNums.@y: return _edge_y;
				default: return null;
				}
			}
			
			public enum toAorB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum toAorB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_toAorB_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_toAorB_alt_0 _alt_0;
			public enum toAorB_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)toAorB_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum toAorB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_toAorB.instance.pat_toAorB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_toAorB_alt_0_toA : GRGEN_LGSP.ListElement<Match_toAorB_alt_0_toA>, IMatch_toAorB_alt_0_toA
		{
			public IA node_a { get { return (IA)_node_a; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public enum toAorB_alt_0_toA_NodeNums { @a, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)toAorB_alt_0_toA_NodeNums.@a: return _node_a;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge_y { get { return (GRGEN_LIBGR.IEdge)_edge_y; } }
			public GRGEN_LGSP.LGSPEdge _edge_y;
			public enum toAorB_alt_0_toA_EdgeNums { @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)toAorB_alt_0_toA_EdgeNums.@y: return _edge_y;
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toA_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toA_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toA_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toA_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_toAorB.instance.toAorB_alt_0_toA; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_toAorB_alt_0_toB : GRGEN_LGSP.ListElement<Match_toAorB_alt_0_toB>, IMatch_toAorB_alt_0_toB
		{
			public IB node_b { get { return (IB)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum toAorB_alt_0_toB_NodeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)toAorB_alt_0_toB_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge_y { get { return (GRGEN_LIBGR.IEdge)_edge_y; } }
			public GRGEN_LGSP.LGSPEdge _edge_y;
			public enum toAorB_alt_0_toB_EdgeNums { @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)toAorB_alt_0_toB_EdgeNums.@y: return _edge_y;
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toB_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum toAorB_alt_0_toB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_toAorB.instance.toAorB_alt_0_toB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createA : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createA instance = null;
		public static Rule_createA Instance { get { if (instance==null) { instance = new Rule_createA(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum createA_NodeNums { };
		public enum createA_EdgeNums { };
		public enum createA_VariableNums { };
		public enum createA_SubNums { };
		public enum createA_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createA;


#if INITIAL_WARMUP
		public Rule_createA()
#else
		private Rule_createA()
#endif
		{
			name = "createA";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] createA_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createA_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createA = new GRGEN_LGSP.PatternGraph(
				"createA",
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
				createA_isNodeHomomorphicGlobal,
				createA_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createA;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createA curMatch = (Match_createA)_curMatch;
			graph.SettingAddedNodeNames( createA_addedNodeNames );
			@A node__node0 = @A.CreateNode(graph);
			graph.SettingAddedEdgeNames( createA_addedEdgeNames );
			return EmptyReturnElements;
		}
		private static String[] createA_addedNodeNames = new String[] { "_node0" };
		private static String[] createA_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createA curMatch = (Match_createA)_curMatch;
			graph.SettingAddedNodeNames( createA_addedNodeNames );
			@A node__node0 = @A.CreateNode(graph);
			graph.SettingAddedEdgeNames( createA_addedEdgeNames );
			return EmptyReturnElements;
		}

		static Rule_createA() {
		}

		public interface IMatch_createA : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_createA : GRGEN_LGSP.ListElement<Match_createA>, IMatch_createA
		{
			public enum createA_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createA_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createA_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createA_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createA_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createA_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createA.instance.pat_createA; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createB : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createB instance = null;
		public static Rule_createB Instance { get { if (instance==null) { instance = new Rule_createB(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum createB_NodeNums { };
		public enum createB_EdgeNums { };
		public enum createB_VariableNums { };
		public enum createB_SubNums { };
		public enum createB_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createB;


#if INITIAL_WARMUP
		public Rule_createB()
#else
		private Rule_createB()
#endif
		{
			name = "createB";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] createB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createB = new GRGEN_LGSP.PatternGraph(
				"createB",
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
				createB_isNodeHomomorphicGlobal,
				createB_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createB;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createB curMatch = (Match_createB)_curMatch;
			graph.SettingAddedNodeNames( createB_addedNodeNames );
			@B node__node0 = @B.CreateNode(graph);
			graph.SettingAddedEdgeNames( createB_addedEdgeNames );
			return EmptyReturnElements;
		}
		private static String[] createB_addedNodeNames = new String[] { "_node0" };
		private static String[] createB_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createB curMatch = (Match_createB)_curMatch;
			graph.SettingAddedNodeNames( createB_addedNodeNames );
			@B node__node0 = @B.CreateNode(graph);
			graph.SettingAddedEdgeNames( createB_addedEdgeNames );
			return EmptyReturnElements;
		}

		static Rule_createB() {
		}

		public interface IMatch_createB : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_createB : GRGEN_LGSP.ListElement<Match_createB>, IMatch_createB
		{
			public enum createB_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createB_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createB_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createB.instance.pat_createB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createC : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createC instance = null;
		public static Rule_createC Instance { get { if (instance==null) { instance = new Rule_createC(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum createC_NodeNums { };
		public enum createC_EdgeNums { };
		public enum createC_VariableNums { };
		public enum createC_SubNums { };
		public enum createC_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createC;


#if INITIAL_WARMUP
		public Rule_createC()
#else
		private Rule_createC()
#endif
		{
			name = "createC";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] createC_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createC_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createC = new GRGEN_LGSP.PatternGraph(
				"createC",
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
				createC_isNodeHomomorphicGlobal,
				createC_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createC;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createC curMatch = (Match_createC)_curMatch;
			graph.SettingAddedNodeNames( createC_addedNodeNames );
			@C node__node0 = @C.CreateNode(graph);
			graph.SettingAddedEdgeNames( createC_addedEdgeNames );
			return EmptyReturnElements;
		}
		private static String[] createC_addedNodeNames = new String[] { "_node0" };
		private static String[] createC_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createC curMatch = (Match_createC)_curMatch;
			graph.SettingAddedNodeNames( createC_addedNodeNames );
			@C node__node0 = @C.CreateNode(graph);
			graph.SettingAddedEdgeNames( createC_addedEdgeNames );
			return EmptyReturnElements;
		}

		static Rule_createC() {
		}

		public interface IMatch_createC : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_createC : GRGEN_LGSP.ListElement<Match_createC>, IMatch_createC
		{
			public enum createC_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createC_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createC_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createC_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createC_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createC_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createC.instance.pat_createC; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createAtoB : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createAtoB instance = null;
		public static Rule_createAtoB Instance { get { if (instance==null) { instance = new Rule_createAtoB(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum createAtoB_NodeNums { };
		public enum createAtoB_EdgeNums { };
		public enum createAtoB_VariableNums { };
		public enum createAtoB_SubNums { };
		public enum createAtoB_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createAtoB;


#if INITIAL_WARMUP
		public Rule_createAtoB()
#else
		private Rule_createAtoB()
#endif
		{
			name = "createAtoB";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] createAtoB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createAtoB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createAtoB = new GRGEN_LGSP.PatternGraph(
				"createAtoB",
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
				createAtoB_isNodeHomomorphicGlobal,
				createAtoB_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createAtoB;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createAtoB curMatch = (Match_createAtoB)_curMatch;
			graph.SettingAddedNodeNames( createAtoB_addedNodeNames );
			@A node__node0 = @A.CreateNode(graph);
			@B node__node1 = @B.CreateNode(graph);
			graph.SettingAddedEdgeNames( createAtoB_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}
		private static String[] createAtoB_addedNodeNames = new String[] { "_node0", "_node1" };
		private static String[] createAtoB_addedEdgeNames = new String[] { "_edge0" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createAtoB curMatch = (Match_createAtoB)_curMatch;
			graph.SettingAddedNodeNames( createAtoB_addedNodeNames );
			@A node__node0 = @A.CreateNode(graph);
			@B node__node1 = @B.CreateNode(graph);
			graph.SettingAddedEdgeNames( createAtoB_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}

		static Rule_createAtoB() {
		}

		public interface IMatch_createAtoB : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_createAtoB : GRGEN_LGSP.ListElement<Match_createAtoB>, IMatch_createAtoB
		{
			public enum createAtoB_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createAtoB_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createAtoB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createAtoB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createAtoB_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createAtoB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createAtoB.instance.pat_createAtoB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_leer : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_leer instance = null;
		public static Rule_leer Instance { get { if (instance==null) { instance = new Rule_leer(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum leer_NodeNums { };
		public enum leer_EdgeNums { };
		public enum leer_VariableNums { };
		public enum leer_SubNums { };
		public enum leer_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_leer;

		public enum leer_alt_0_CaseNums { @altleer, };
		public enum leer_alt_0_altleer_NodeNums { };
		public enum leer_alt_0_altleer_EdgeNums { };
		public enum leer_alt_0_altleer_VariableNums { };
		public enum leer_alt_0_altleer_SubNums { };
		public enum leer_alt_0_altleer_AltNums { };


		GRGEN_LGSP.PatternGraph leer_alt_0_altleer;


#if INITIAL_WARMUP
		public Rule_leer()
#else
		private Rule_leer()
#endif
		{
			name = "leer";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] leer_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] leer_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] leer_alt_0_altleer_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] leer_alt_0_altleer_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			leer_alt_0_altleer = new GRGEN_LGSP.PatternGraph(
				"altleer",
				"leer_alt_0_",
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
				leer_alt_0_altleer_isNodeHomomorphicGlobal,
				leer_alt_0_altleer_isEdgeHomomorphicGlobal
			);

			GRGEN_LGSP.Alternative leer_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "leer_", new GRGEN_LGSP.PatternGraph[] { leer_alt_0_altleer } );

			pat_leer = new GRGEN_LGSP.PatternGraph(
				"leer",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { leer_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				leer_isNodeHomomorphicGlobal,
				leer_isEdgeHomomorphicGlobal
			);
			leer_alt_0_altleer.embeddingGraph = pat_leer;


			patternGraph = pat_leer;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_leer curMatch = (Match_leer)_curMatch;
			IMatch_leer_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_leer curMatch = (Match_leer)_curMatch;
			IMatch_leer_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void leer_alt_0_altleer_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_leer_alt_0_altleer curMatch = (Match_leer_alt_0_altleer)_curMatch;
		}

		public void leer_alt_0_altleer_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_leer_alt_0_altleer curMatch = (Match_leer_alt_0_altleer)_curMatch;
		}

		static Rule_leer() {
		}

		public interface IMatch_leer : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_leer_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_leer_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_leer_alt_0_altleer : IMatch_leer_alt_0
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_leer : GRGEN_LGSP.ListElement<Match_leer>, IMatch_leer
		{
			public enum leer_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_leer_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_leer_alt_0 _alt_0;
			public enum leer_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)leer_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum leer_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_leer.instance.pat_leer; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_leer_alt_0_altleer : GRGEN_LGSP.ListElement<Match_leer_alt_0_altleer>, IMatch_leer_alt_0_altleer
		{
			public enum leer_alt_0_altleer_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_alt_0_altleer_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_alt_0_altleer_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_alt_0_altleer_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_alt_0_altleer_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum leer_alt_0_altleer_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_leer.instance.leer_alt_0_altleer; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_AorB : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_AorB instance = null;
		public static Rule_AorB Instance { get { if (instance==null) { instance = new Rule_AorB(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum AorB_NodeNums { };
		public enum AorB_EdgeNums { };
		public enum AorB_VariableNums { };
		public enum AorB_SubNums { };
		public enum AorB_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_AorB;

		public enum AorB_alt_0_CaseNums { @A, @B, };
		public static GRGEN_LIBGR.NodeType[] AorB_alt_0_A_node__node0_AllowedTypes = null;
		public static bool[] AorB_alt_0_A_node__node0_IsAllowedType = null;
		public enum AorB_alt_0_A_NodeNums { @_node0, };
		public enum AorB_alt_0_A_EdgeNums { };
		public enum AorB_alt_0_A_VariableNums { };
		public enum AorB_alt_0_A_SubNums { };
		public enum AorB_alt_0_A_AltNums { };


		GRGEN_LGSP.PatternGraph AorB_alt_0_A;

		public static GRGEN_LIBGR.NodeType[] AorB_alt_0_B_node__node0_AllowedTypes = null;
		public static bool[] AorB_alt_0_B_node__node0_IsAllowedType = null;
		public enum AorB_alt_0_B_NodeNums { @_node0, };
		public enum AorB_alt_0_B_EdgeNums { };
		public enum AorB_alt_0_B_VariableNums { };
		public enum AorB_alt_0_B_SubNums { };
		public enum AorB_alt_0_B_AltNums { };


		GRGEN_LGSP.PatternGraph AorB_alt_0_B;


#if INITIAL_WARMUP
		public Rule_AorB()
#else
		private Rule_AorB()
#endif
		{
			name = "AorB";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] AorB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AorB_alt_0_A_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorB_alt_0_A_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AorB_alt_0_A_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "AorB_alt_0_A_node__node0", "_node0", AorB_alt_0_A_node__node0_AllowedTypes, AorB_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			AorB_alt_0_A = new GRGEN_LGSP.PatternGraph(
				"A",
				"AorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AorB_alt_0_A_node__node0 }, 
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
				AorB_alt_0_A_isNodeHomomorphicGlobal,
				AorB_alt_0_A_isEdgeHomomorphicGlobal
			);

			bool[,] AorB_alt_0_B_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorB_alt_0_B_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AorB_alt_0_B_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "AorB_alt_0_B_node__node0", "_node0", AorB_alt_0_B_node__node0_AllowedTypes, AorB_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AorB_alt_0_B = new GRGEN_LGSP.PatternGraph(
				"B",
				"AorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AorB_alt_0_B_node__node0 }, 
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
				AorB_alt_0_B_isNodeHomomorphicGlobal,
				AorB_alt_0_B_isEdgeHomomorphicGlobal
			);

			GRGEN_LGSP.Alternative AorB_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "AorB_", new GRGEN_LGSP.PatternGraph[] { AorB_alt_0_A, AorB_alt_0_B } );

			pat_AorB = new GRGEN_LGSP.PatternGraph(
				"AorB",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { AorB_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				AorB_isNodeHomomorphicGlobal,
				AorB_isEdgeHomomorphicGlobal
			);
			AorB_alt_0_A.embeddingGraph = pat_AorB;
			AorB_alt_0_B.embeddingGraph = pat_AorB;

			AorB_alt_0_A_node__node0.PointOfDefinition = AorB_alt_0_A;
			AorB_alt_0_B_node__node0.PointOfDefinition = AorB_alt_0_B;

			patternGraph = pat_AorB;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorB curMatch = (Match_AorB)_curMatch;
			IMatch_AorB_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorB curMatch = (Match_AorB)_curMatch;
			IMatch_AorB_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void AorB_alt_0_A_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorB_alt_0_A curMatch = (Match_AorB_alt_0_A)_curMatch;
		}

		public void AorB_alt_0_A_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorB_alt_0_A curMatch = (Match_AorB_alt_0_A)_curMatch;
		}

		public void AorB_alt_0_B_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorB_alt_0_B curMatch = (Match_AorB_alt_0_B)_curMatch;
		}

		public void AorB_alt_0_B_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorB_alt_0_B curMatch = (Match_AorB_alt_0_B)_curMatch;
		}

		static Rule_AorB() {
		}

		public interface IMatch_AorB : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_AorB_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_AorB_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_AorB_alt_0_A : IMatch_AorB_alt_0
		{
			//Nodes
			IA node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_AorB_alt_0_B : IMatch_AorB_alt_0
		{
			//Nodes
			IB node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_AorB : GRGEN_LGSP.ListElement<Match_AorB>, IMatch_AorB
		{
			public enum AorB_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_AorB_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_AorB_alt_0 _alt_0;
			public enum AorB_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)AorB_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum AorB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AorB.instance.pat_AorB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AorB_alt_0_A : GRGEN_LGSP.ListElement<Match_AorB_alt_0_A>, IMatch_AorB_alt_0_A
		{
			public IA node__node0 { get { return (IA)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AorB_alt_0_A_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AorB_alt_0_A_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AorB_alt_0_A_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_A_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_A_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_A_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_A_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AorB.instance.AorB_alt_0_A; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AorB_alt_0_B : GRGEN_LGSP.ListElement<Match_AorB_alt_0_B>, IMatch_AorB_alt_0_B
		{
			public IB node__node0 { get { return (IB)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AorB_alt_0_B_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AorB_alt_0_B_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AorB_alt_0_B_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_B_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_B_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_B_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorB_alt_0_B_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AorB.instance.AorB_alt_0_B; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_AandnotCorB : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_AandnotCorB instance = null;
		public static Rule_AandnotCorB Instance { get { if (instance==null) { instance = new Rule_AandnotCorB(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum AandnotCorB_NodeNums { };
		public enum AandnotCorB_EdgeNums { };
		public enum AandnotCorB_VariableNums { };
		public enum AandnotCorB_SubNums { };
		public enum AandnotCorB_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_AandnotCorB;

		public enum AandnotCorB_alt_0_CaseNums { @A, @B, };
		public static GRGEN_LIBGR.NodeType[] AandnotCorB_alt_0_A_node__node0_AllowedTypes = null;
		public static bool[] AandnotCorB_alt_0_A_node__node0_IsAllowedType = null;
		public enum AandnotCorB_alt_0_A_NodeNums { @_node0, };
		public enum AandnotCorB_alt_0_A_EdgeNums { };
		public enum AandnotCorB_alt_0_A_VariableNums { };
		public enum AandnotCorB_alt_0_A_SubNums { };
		public enum AandnotCorB_alt_0_A_AltNums { };


		GRGEN_LGSP.PatternGraph AandnotCorB_alt_0_A;

		public static GRGEN_LIBGR.NodeType[] AandnotCorB_alt_0_A_neg_0_node__node0_AllowedTypes = null;
		public static bool[] AandnotCorB_alt_0_A_neg_0_node__node0_IsAllowedType = null;
		public enum AandnotCorB_alt_0_A_neg_0_NodeNums { @_node0, };
		public enum AandnotCorB_alt_0_A_neg_0_EdgeNums { };
		public enum AandnotCorB_alt_0_A_neg_0_VariableNums { };
		public enum AandnotCorB_alt_0_A_neg_0_SubNums { };
		public enum AandnotCorB_alt_0_A_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph AandnotCorB_alt_0_A_neg_0;

		public static GRGEN_LIBGR.NodeType[] AandnotCorB_alt_0_B_node__node0_AllowedTypes = null;
		public static bool[] AandnotCorB_alt_0_B_node__node0_IsAllowedType = null;
		public enum AandnotCorB_alt_0_B_NodeNums { @_node0, };
		public enum AandnotCorB_alt_0_B_EdgeNums { };
		public enum AandnotCorB_alt_0_B_VariableNums { };
		public enum AandnotCorB_alt_0_B_SubNums { };
		public enum AandnotCorB_alt_0_B_AltNums { };


		GRGEN_LGSP.PatternGraph AandnotCorB_alt_0_B;


#if INITIAL_WARMUP
		public Rule_AandnotCorB()
#else
		private Rule_AandnotCorB()
#endif
		{
			name = "AandnotCorB";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] AandnotCorB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AandnotCorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AandnotCorB_alt_0_A_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AandnotCorB_alt_0_A_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AandnotCorB_alt_0_A_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "AandnotCorB_alt_0_A_node__node0", "_node0", AandnotCorB_alt_0_A_node__node0_AllowedTypes, AandnotCorB_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			bool[,] AandnotCorB_alt_0_A_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AandnotCorB_alt_0_A_neg_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AandnotCorB_alt_0_A_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "AandnotCorB_alt_0_A_neg_0_node__node0", "_node0", AandnotCorB_alt_0_A_neg_0_node__node0_AllowedTypes, AandnotCorB_alt_0_A_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			AandnotCorB_alt_0_A_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"AandnotCorB_alt_0_A_",
				false,
				new GRGEN_LGSP.PatternNode[] { AandnotCorB_alt_0_A_neg_0_node__node0 }, 
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
				AandnotCorB_alt_0_A_neg_0_isNodeHomomorphicGlobal,
				AandnotCorB_alt_0_A_neg_0_isEdgeHomomorphicGlobal
			);

			AandnotCorB_alt_0_A = new GRGEN_LGSP.PatternGraph(
				"A",
				"AandnotCorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AandnotCorB_alt_0_A_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { AandnotCorB_alt_0_A_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				AandnotCorB_alt_0_A_isNodeHomomorphicGlobal,
				AandnotCorB_alt_0_A_isEdgeHomomorphicGlobal
			);
			AandnotCorB_alt_0_A_neg_0.embeddingGraph = AandnotCorB_alt_0_A;

			bool[,] AandnotCorB_alt_0_B_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AandnotCorB_alt_0_B_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AandnotCorB_alt_0_B_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "AandnotCorB_alt_0_B_node__node0", "_node0", AandnotCorB_alt_0_B_node__node0_AllowedTypes, AandnotCorB_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AandnotCorB_alt_0_B = new GRGEN_LGSP.PatternGraph(
				"B",
				"AandnotCorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AandnotCorB_alt_0_B_node__node0 }, 
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
				AandnotCorB_alt_0_B_isNodeHomomorphicGlobal,
				AandnotCorB_alt_0_B_isEdgeHomomorphicGlobal
			);

			GRGEN_LGSP.Alternative AandnotCorB_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "AandnotCorB_", new GRGEN_LGSP.PatternGraph[] { AandnotCorB_alt_0_A, AandnotCorB_alt_0_B } );

			pat_AandnotCorB = new GRGEN_LGSP.PatternGraph(
				"AandnotCorB",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { AandnotCorB_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				AandnotCorB_isNodeHomomorphicGlobal,
				AandnotCorB_isEdgeHomomorphicGlobal
			);
			AandnotCorB_alt_0_A.embeddingGraph = pat_AandnotCorB;
			AandnotCorB_alt_0_B.embeddingGraph = pat_AandnotCorB;

			AandnotCorB_alt_0_A_node__node0.PointOfDefinition = AandnotCorB_alt_0_A;
			AandnotCorB_alt_0_A_neg_0_node__node0.PointOfDefinition = AandnotCorB_alt_0_A_neg_0;
			AandnotCorB_alt_0_B_node__node0.PointOfDefinition = AandnotCorB_alt_0_B;

			patternGraph = pat_AandnotCorB;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AandnotCorB curMatch = (Match_AandnotCorB)_curMatch;
			IMatch_AandnotCorB_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AandnotCorB curMatch = (Match_AandnotCorB)_curMatch;
			IMatch_AandnotCorB_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void AandnotCorB_alt_0_A_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AandnotCorB_alt_0_A curMatch = (Match_AandnotCorB_alt_0_A)_curMatch;
		}

		public void AandnotCorB_alt_0_A_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AandnotCorB_alt_0_A curMatch = (Match_AandnotCorB_alt_0_A)_curMatch;
		}

		public void AandnotCorB_alt_0_B_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AandnotCorB_alt_0_B curMatch = (Match_AandnotCorB_alt_0_B)_curMatch;
		}

		public void AandnotCorB_alt_0_B_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AandnotCorB_alt_0_B curMatch = (Match_AandnotCorB_alt_0_B)_curMatch;
		}

		static Rule_AandnotCorB() {
		}

		public interface IMatch_AandnotCorB : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_AandnotCorB_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_AandnotCorB_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_AandnotCorB_alt_0_A : IMatch_AandnotCorB_alt_0
		{
			//Nodes
			IA node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_AandnotCorB_alt_0_A_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IC node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_AandnotCorB_alt_0_B : IMatch_AandnotCorB_alt_0
		{
			//Nodes
			IB node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_AandnotCorB : GRGEN_LGSP.ListElement<Match_AandnotCorB>, IMatch_AandnotCorB
		{
			public enum AandnotCorB_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_AandnotCorB_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_AandnotCorB_alt_0 _alt_0;
			public enum AandnotCorB_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)AandnotCorB_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum AandnotCorB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AandnotCorB.instance.pat_AandnotCorB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AandnotCorB_alt_0_A : GRGEN_LGSP.ListElement<Match_AandnotCorB_alt_0_A>, IMatch_AandnotCorB_alt_0_A
		{
			public IA node__node0 { get { return (IA)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AandnotCorB_alt_0_A_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AandnotCorB_alt_0_A_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AandnotCorB.instance.AandnotCorB_alt_0_A; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AandnotCorB_alt_0_A_neg_0 : GRGEN_LGSP.ListElement<Match_AandnotCorB_alt_0_A_neg_0>, IMatch_AandnotCorB_alt_0_A_neg_0
		{
			public IC node__node0 { get { return (IC)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AandnotCorB_alt_0_A_neg_0_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AandnotCorB_alt_0_A_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_neg_0_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_A_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AandnotCorB.instance.AandnotCorB_alt_0_A_neg_0; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AandnotCorB_alt_0_B : GRGEN_LGSP.ListElement<Match_AandnotCorB_alt_0_B>, IMatch_AandnotCorB_alt_0_B
		{
			public IB node__node0 { get { return (IB)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AandnotCorB_alt_0_B_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AandnotCorB_alt_0_B_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_B_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_B_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_B_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_B_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AandnotCorB_alt_0_B_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AandnotCorB.instance.AandnotCorB_alt_0_B; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_AorBorC : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_AorBorC instance = null;
		public static Rule_AorBorC Instance { get { if (instance==null) { instance = new Rule_AorBorC(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum AorBorC_NodeNums { };
		public enum AorBorC_EdgeNums { };
		public enum AorBorC_VariableNums { };
		public enum AorBorC_SubNums { };
		public enum AorBorC_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_AorBorC;

		public enum AorBorC_alt_0_CaseNums { @A, @B, @C, };
		public static GRGEN_LIBGR.NodeType[] AorBorC_alt_0_A_node__node0_AllowedTypes = null;
		public static bool[] AorBorC_alt_0_A_node__node0_IsAllowedType = null;
		public enum AorBorC_alt_0_A_NodeNums { @_node0, };
		public enum AorBorC_alt_0_A_EdgeNums { };
		public enum AorBorC_alt_0_A_VariableNums { };
		public enum AorBorC_alt_0_A_SubNums { };
		public enum AorBorC_alt_0_A_AltNums { };


		GRGEN_LGSP.PatternGraph AorBorC_alt_0_A;

		public static GRGEN_LIBGR.NodeType[] AorBorC_alt_0_B_node__node0_AllowedTypes = null;
		public static bool[] AorBorC_alt_0_B_node__node0_IsAllowedType = null;
		public enum AorBorC_alt_0_B_NodeNums { @_node0, };
		public enum AorBorC_alt_0_B_EdgeNums { };
		public enum AorBorC_alt_0_B_VariableNums { };
		public enum AorBorC_alt_0_B_SubNums { };
		public enum AorBorC_alt_0_B_AltNums { };


		GRGEN_LGSP.PatternGraph AorBorC_alt_0_B;

		public static GRGEN_LIBGR.NodeType[] AorBorC_alt_0_C_node__node0_AllowedTypes = null;
		public static bool[] AorBorC_alt_0_C_node__node0_IsAllowedType = null;
		public enum AorBorC_alt_0_C_NodeNums { @_node0, };
		public enum AorBorC_alt_0_C_EdgeNums { };
		public enum AorBorC_alt_0_C_VariableNums { };
		public enum AorBorC_alt_0_C_SubNums { };
		public enum AorBorC_alt_0_C_AltNums { };


		GRGEN_LGSP.PatternGraph AorBorC_alt_0_C;


#if INITIAL_WARMUP
		public Rule_AorBorC()
#else
		private Rule_AorBorC()
#endif
		{
			name = "AorBorC";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] AorBorC_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AorBorC_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AorBorC_alt_0_A_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorBorC_alt_0_A_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AorBorC_alt_0_A_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "AorBorC_alt_0_A_node__node0", "_node0", AorBorC_alt_0_A_node__node0_AllowedTypes, AorBorC_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_A = new GRGEN_LGSP.PatternGraph(
				"A",
				"AorBorC_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AorBorC_alt_0_A_node__node0 }, 
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
				AorBorC_alt_0_A_isNodeHomomorphicGlobal,
				AorBorC_alt_0_A_isEdgeHomomorphicGlobal
			);

			bool[,] AorBorC_alt_0_B_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorBorC_alt_0_B_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AorBorC_alt_0_B_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "AorBorC_alt_0_B_node__node0", "_node0", AorBorC_alt_0_B_node__node0_AllowedTypes, AorBorC_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_B = new GRGEN_LGSP.PatternGraph(
				"B",
				"AorBorC_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AorBorC_alt_0_B_node__node0 }, 
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
				AorBorC_alt_0_B_isNodeHomomorphicGlobal,
				AorBorC_alt_0_B_isEdgeHomomorphicGlobal
			);

			bool[,] AorBorC_alt_0_C_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorBorC_alt_0_C_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AorBorC_alt_0_C_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "AorBorC_alt_0_C_node__node0", "_node0", AorBorC_alt_0_C_node__node0_AllowedTypes, AorBorC_alt_0_C_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_C = new GRGEN_LGSP.PatternGraph(
				"C",
				"AorBorC_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AorBorC_alt_0_C_node__node0 }, 
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
				AorBorC_alt_0_C_isNodeHomomorphicGlobal,
				AorBorC_alt_0_C_isEdgeHomomorphicGlobal
			);

			GRGEN_LGSP.Alternative AorBorC_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "AorBorC_", new GRGEN_LGSP.PatternGraph[] { AorBorC_alt_0_A, AorBorC_alt_0_B, AorBorC_alt_0_C } );

			pat_AorBorC = new GRGEN_LGSP.PatternGraph(
				"AorBorC",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { AorBorC_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				AorBorC_isNodeHomomorphicGlobal,
				AorBorC_isEdgeHomomorphicGlobal
			);
			AorBorC_alt_0_A.embeddingGraph = pat_AorBorC;
			AorBorC_alt_0_B.embeddingGraph = pat_AorBorC;
			AorBorC_alt_0_C.embeddingGraph = pat_AorBorC;

			AorBorC_alt_0_A_node__node0.PointOfDefinition = AorBorC_alt_0_A;
			AorBorC_alt_0_B_node__node0.PointOfDefinition = AorBorC_alt_0_B;
			AorBorC_alt_0_C_node__node0.PointOfDefinition = AorBorC_alt_0_C;

			patternGraph = pat_AorBorC;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC curMatch = (Match_AorBorC)_curMatch;
			IMatch_AorBorC_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC curMatch = (Match_AorBorC)_curMatch;
			IMatch_AorBorC_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void AorBorC_alt_0_A_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC_alt_0_A curMatch = (Match_AorBorC_alt_0_A)_curMatch;
		}

		public void AorBorC_alt_0_A_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC_alt_0_A curMatch = (Match_AorBorC_alt_0_A)_curMatch;
		}

		public void AorBorC_alt_0_B_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC_alt_0_B curMatch = (Match_AorBorC_alt_0_B)_curMatch;
		}

		public void AorBorC_alt_0_B_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC_alt_0_B curMatch = (Match_AorBorC_alt_0_B)_curMatch;
		}

		public void AorBorC_alt_0_C_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC_alt_0_C curMatch = (Match_AorBorC_alt_0_C)_curMatch;
		}

		public void AorBorC_alt_0_C_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AorBorC_alt_0_C curMatch = (Match_AorBorC_alt_0_C)_curMatch;
		}

		static Rule_AorBorC() {
		}

		public interface IMatch_AorBorC : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_AorBorC_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_AorBorC_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_AorBorC_alt_0_A : IMatch_AorBorC_alt_0
		{
			//Nodes
			IA node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_AorBorC_alt_0_B : IMatch_AorBorC_alt_0
		{
			//Nodes
			IB node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_AorBorC_alt_0_C : IMatch_AorBorC_alt_0
		{
			//Nodes
			IC node__node0 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_AorBorC : GRGEN_LGSP.ListElement<Match_AorBorC>, IMatch_AorBorC
		{
			public enum AorBorC_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_AorBorC_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_AorBorC_alt_0 _alt_0;
			public enum AorBorC_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)AorBorC_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum AorBorC_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AorBorC.instance.pat_AorBorC; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AorBorC_alt_0_A : GRGEN_LGSP.ListElement<Match_AorBorC_alt_0_A>, IMatch_AorBorC_alt_0_A
		{
			public IA node__node0 { get { return (IA)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AorBorC_alt_0_A_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AorBorC_alt_0_A_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_A_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_A_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_A_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_A_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_A_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AorBorC.instance.AorBorC_alt_0_A; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AorBorC_alt_0_B : GRGEN_LGSP.ListElement<Match_AorBorC_alt_0_B>, IMatch_AorBorC_alt_0_B
		{
			public IB node__node0 { get { return (IB)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AorBorC_alt_0_B_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AorBorC_alt_0_B_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_B_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_B_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_B_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_B_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_B_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AorBorC.instance.AorBorC_alt_0_B; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AorBorC_alt_0_C : GRGEN_LGSP.ListElement<Match_AorBorC_alt_0_C>, IMatch_AorBorC_alt_0_C
		{
			public IC node__node0 { get { return (IC)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AorBorC_alt_0_C_NodeNums { @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AorBorC_alt_0_C_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_C_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_C_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_C_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_C_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AorBorC_alt_0_C_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AorBorC.instance.AorBorC_alt_0_C; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_AtoAorB : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_AtoAorB instance = null;
		public static Rule_AtoAorB Instance { get { if (instance==null) { instance = new Rule_AtoAorB(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] AtoAorB_node_a_AllowedTypes = null;
		public static bool[] AtoAorB_node_a_IsAllowedType = null;
		public enum AtoAorB_NodeNums { @a, };
		public enum AtoAorB_EdgeNums { };
		public enum AtoAorB_VariableNums { };
		public enum AtoAorB_SubNums { };
		public enum AtoAorB_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_AtoAorB;

		public enum AtoAorB_alt_0_CaseNums { @toA, @toB, };
		public static GRGEN_LIBGR.NodeType[] AtoAorB_alt_0_toA_node__node0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toA_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] AtoAorB_alt_0_toA_edge__edge0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toA_edge__edge0_IsAllowedType = null;
		public enum AtoAorB_alt_0_toA_NodeNums { @a, @_node0, };
		public enum AtoAorB_alt_0_toA_EdgeNums { @_edge0, };
		public enum AtoAorB_alt_0_toA_VariableNums { };
		public enum AtoAorB_alt_0_toA_SubNums { };
		public enum AtoAorB_alt_0_toA_AltNums { };


		GRGEN_LGSP.PatternGraph AtoAorB_alt_0_toA;

		public static GRGEN_LIBGR.NodeType[] AtoAorB_alt_0_toB_node__node0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toB_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] AtoAorB_alt_0_toB_edge__edge0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toB_edge__edge0_IsAllowedType = null;
		public enum AtoAorB_alt_0_toB_NodeNums { @a, @_node0, };
		public enum AtoAorB_alt_0_toB_EdgeNums { @_edge0, };
		public enum AtoAorB_alt_0_toB_VariableNums { };
		public enum AtoAorB_alt_0_toB_SubNums { };
		public enum AtoAorB_alt_0_toB_AltNums { };


		GRGEN_LGSP.PatternGraph AtoAorB_alt_0_toB;


#if INITIAL_WARMUP
		public Rule_AtoAorB()
#else
		private Rule_AtoAorB()
#endif
		{
			name = "AtoAorB";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] AtoAorB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AtoAorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode AtoAorB_node_a = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "AtoAorB_node_a", "a", AtoAorB_node_a_AllowedTypes, AtoAorB_node_a_IsAllowedType, 5.5F, -1);
			bool[,] AtoAorB_alt_0_toA_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] AtoAorB_alt_0_toA_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode AtoAorB_alt_0_toA_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "AtoAorB_alt_0_toA_node__node0", "_node0", AtoAorB_alt_0_toA_node__node0_AllowedTypes, AtoAorB_alt_0_toA_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge AtoAorB_alt_0_toA_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "AtoAorB_alt_0_toA_edge__edge0", "_edge0", AtoAorB_alt_0_toA_edge__edge0_AllowedTypes, AtoAorB_alt_0_toA_edge__edge0_IsAllowedType, 5.5F, -1);
			AtoAorB_alt_0_toA = new GRGEN_LGSP.PatternGraph(
				"toA",
				"AtoAorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AtoAorB_node_a, AtoAorB_alt_0_toA_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { AtoAorB_alt_0_toA_edge__edge0 }, 
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
				AtoAorB_alt_0_toA_isNodeHomomorphicGlobal,
				AtoAorB_alt_0_toA_isEdgeHomomorphicGlobal
			);
			AtoAorB_alt_0_toA.edgeToSourceNode.Add(AtoAorB_alt_0_toA_edge__edge0, AtoAorB_node_a);
			AtoAorB_alt_0_toA.edgeToTargetNode.Add(AtoAorB_alt_0_toA_edge__edge0, AtoAorB_alt_0_toA_node__node0);

			bool[,] AtoAorB_alt_0_toB_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] AtoAorB_alt_0_toB_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode AtoAorB_alt_0_toB_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "AtoAorB_alt_0_toB_node__node0", "_node0", AtoAorB_alt_0_toB_node__node0_AllowedTypes, AtoAorB_alt_0_toB_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge AtoAorB_alt_0_toB_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "AtoAorB_alt_0_toB_edge__edge0", "_edge0", AtoAorB_alt_0_toB_edge__edge0_AllowedTypes, AtoAorB_alt_0_toB_edge__edge0_IsAllowedType, 5.5F, -1);
			AtoAorB_alt_0_toB = new GRGEN_LGSP.PatternGraph(
				"toB",
				"AtoAorB_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { AtoAorB_node_a, AtoAorB_alt_0_toB_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { AtoAorB_alt_0_toB_edge__edge0 }, 
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
				AtoAorB_alt_0_toB_isNodeHomomorphicGlobal,
				AtoAorB_alt_0_toB_isEdgeHomomorphicGlobal
			);
			AtoAorB_alt_0_toB.edgeToSourceNode.Add(AtoAorB_alt_0_toB_edge__edge0, AtoAorB_node_a);
			AtoAorB_alt_0_toB.edgeToTargetNode.Add(AtoAorB_alt_0_toB_edge__edge0, AtoAorB_alt_0_toB_node__node0);

			GRGEN_LGSP.Alternative AtoAorB_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "AtoAorB_", new GRGEN_LGSP.PatternGraph[] { AtoAorB_alt_0_toA, AtoAorB_alt_0_toB } );

			pat_AtoAorB = new GRGEN_LGSP.PatternGraph(
				"AtoAorB",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { AtoAorB_node_a }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { AtoAorB_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				AtoAorB_isNodeHomomorphicGlobal,
				AtoAorB_isEdgeHomomorphicGlobal
			);
			AtoAorB_alt_0_toA.embeddingGraph = pat_AtoAorB;
			AtoAorB_alt_0_toB.embeddingGraph = pat_AtoAorB;

			AtoAorB_node_a.PointOfDefinition = pat_AtoAorB;
			AtoAorB_alt_0_toA_node__node0.PointOfDefinition = AtoAorB_alt_0_toA;
			AtoAorB_alt_0_toA_edge__edge0.PointOfDefinition = AtoAorB_alt_0_toA;
			AtoAorB_alt_0_toB_node__node0.PointOfDefinition = AtoAorB_alt_0_toB;
			AtoAorB_alt_0_toB_edge__edge0.PointOfDefinition = AtoAorB_alt_0_toB;

			patternGraph = pat_AtoAorB;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AtoAorB curMatch = (Match_AtoAorB)_curMatch;
			IMatch_AtoAorB_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AtoAorB curMatch = (Match_AtoAorB)_curMatch;
			IMatch_AtoAorB_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void AtoAorB_alt_0_toA_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AtoAorB_alt_0_toA curMatch = (Match_AtoAorB_alt_0_toA)_curMatch;
		}

		public void AtoAorB_alt_0_toA_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AtoAorB_alt_0_toA curMatch = (Match_AtoAorB_alt_0_toA)_curMatch;
		}

		public void AtoAorB_alt_0_toB_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AtoAorB_alt_0_toB curMatch = (Match_AtoAorB_alt_0_toB)_curMatch;
		}

		public void AtoAorB_alt_0_toB_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_AtoAorB_alt_0_toB curMatch = (Match_AtoAorB_alt_0_toB)_curMatch;
		}

		static Rule_AtoAorB() {
		}

		public interface IMatch_AtoAorB : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IA node_a { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_AtoAorB_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_AtoAorB_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_AtoAorB_alt_0_toA : IMatch_AtoAorB_alt_0
		{
			//Nodes
			IA node_a { get; }
			IA node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_AtoAorB_alt_0_toB : IMatch_AtoAorB_alt_0
		{
			//Nodes
			IA node_a { get; }
			IB node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_AtoAorB : GRGEN_LGSP.ListElement<Match_AtoAorB>, IMatch_AtoAorB
		{
			public IA node_a { get { return (IA)_node_a; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public enum AtoAorB_NodeNums { @a, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AtoAorB_NodeNums.@a: return _node_a;
				default: return null;
				}
			}
			
			public enum AtoAorB_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_AtoAorB_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_AtoAorB_alt_0 _alt_0;
			public enum AtoAorB_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)AtoAorB_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum AtoAorB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AtoAorB.instance.pat_AtoAorB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AtoAorB_alt_0_toA : GRGEN_LGSP.ListElement<Match_AtoAorB_alt_0_toA>, IMatch_AtoAorB_alt_0_toA
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IA node__node0 { get { return (IA)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AtoAorB_alt_0_toA_NodeNums { @a, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AtoAorB_alt_0_toA_NodeNums.@a: return _node_a;
				case (int)AtoAorB_alt_0_toA_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum AtoAorB_alt_0_toA_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)AtoAorB_alt_0_toA_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toA_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toA_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toA_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toA_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AtoAorB.instance.AtoAorB_alt_0_toA; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_AtoAorB_alt_0_toB : GRGEN_LGSP.ListElement<Match_AtoAorB_alt_0_toB>, IMatch_AtoAorB_alt_0_toB
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node__node0 { get { return (IB)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum AtoAorB_alt_0_toB_NodeNums { @a, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)AtoAorB_alt_0_toB_NodeNums.@a: return _node_a;
				case (int)AtoAorB_alt_0_toB_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum AtoAorB_alt_0_toB_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)AtoAorB_alt_0_toB_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toB_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toB_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum AtoAorB_alt_0_toB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_AtoAorB.instance.AtoAorB_alt_0_toB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createComplex : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createComplex instance = null;
		public static Rule_createComplex Instance { get { if (instance==null) { instance = new Rule_createComplex(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum createComplex_NodeNums { };
		public enum createComplex_EdgeNums { };
		public enum createComplex_VariableNums { };
		public enum createComplex_SubNums { };
		public enum createComplex_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createComplex;


#if INITIAL_WARMUP
		public Rule_createComplex()
#else
		private Rule_createComplex()
#endif
		{
			name = "createComplex";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] createComplex_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createComplex_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createComplex = new GRGEN_LGSP.PatternGraph(
				"createComplex",
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
				createComplex_isNodeHomomorphicGlobal,
				createComplex_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createComplex;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createComplex curMatch = (Match_createComplex)_curMatch;
			graph.SettingAddedNodeNames( createComplex_addedNodeNames );
			@A node_a = @A.CreateNode(graph);
			@B node_b = @B.CreateNode(graph);
			@B node_b2 = @B.CreateNode(graph);
			@C node__node0 = @C.CreateNode(graph);
			@C node__node1 = @C.CreateNode(graph);
			@C node__node2 = @C.CreateNode(graph);
			graph.SettingAddedEdgeNames( createComplex_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_a, node_b);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_b, node_a);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_a, node_b2);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_b2, node_a);
			@Edge edge__edge4 = @Edge.CreateEdge(graph, node_b, node__node0);
			@Edge edge__edge5 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge6 = @Edge.CreateEdge(graph, node__node1, node__node2);
			return EmptyReturnElements;
		}
		private static String[] createComplex_addedNodeNames = new String[] { "a", "b", "b2", "_node0", "_node1", "_node2" };
		private static String[] createComplex_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createComplex curMatch = (Match_createComplex)_curMatch;
			graph.SettingAddedNodeNames( createComplex_addedNodeNames );
			@A node_a = @A.CreateNode(graph);
			@B node_b = @B.CreateNode(graph);
			@B node_b2 = @B.CreateNode(graph);
			@C node__node0 = @C.CreateNode(graph);
			@C node__node1 = @C.CreateNode(graph);
			@C node__node2 = @C.CreateNode(graph);
			graph.SettingAddedEdgeNames( createComplex_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_a, node_b);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_b, node_a);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_a, node_b2);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_b2, node_a);
			@Edge edge__edge4 = @Edge.CreateEdge(graph, node_b, node__node0);
			@Edge edge__edge5 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge6 = @Edge.CreateEdge(graph, node__node1, node__node2);
			return EmptyReturnElements;
		}

		static Rule_createComplex() {
		}

		public interface IMatch_createComplex : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_createComplex : GRGEN_LGSP.ListElement<Match_createComplex>, IMatch_createComplex
		{
			public enum createComplex_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createComplex_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createComplex_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createComplex_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createComplex_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createComplex_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createComplex.instance.pat_createComplex; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_Complex : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_Complex instance = null;
		public static Rule_Complex Instance { get { if (instance==null) { instance = new Rule_Complex(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] Complex_node_a_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_node_b_AllowedTypes = null;
		public static bool[] Complex_node_a_IsAllowedType = null;
		public static bool[] Complex_node_b_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_edge__edge1_AllowedTypes = null;
		public static bool[] Complex_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_edge__edge1_IsAllowedType = null;
		public enum Complex_NodeNums { @a, @b, };
		public enum Complex_EdgeNums { @_edge0, @_edge1, };
		public enum Complex_VariableNums { };
		public enum Complex_SubNums { };
		public enum Complex_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_Complex;

		public enum Complex_alt_0_CaseNums { @ExtendAv, @ExtendAv2, @ExtendNA2, };
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendAv_node_b2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendAv_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendAv_node__node1_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv_node_b2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_node__node0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_node__node1_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv_edge__edge3_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge3_IsAllowedType = null;
		public enum Complex_alt_0_ExtendAv_NodeNums { @a, @b2, @b, @_node0, @_node1, };
		public enum Complex_alt_0_ExtendAv_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum Complex_alt_0_ExtendAv_VariableNums { };
		public enum Complex_alt_0_ExtendAv_SubNums { };
		public enum Complex_alt_0_ExtendAv_AltNums { };


		GRGEN_LGSP.PatternGraph Complex_alt_0_ExtendAv;

		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendAv2_node_b2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendAv2_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendAv2_node__node1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendAv2_node__node2_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv2_node_b2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_node__node0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_node__node1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_node__node2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv2_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv2_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv2_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv2_edge__edge3_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendAv2_edge__edge4_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge3_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge4_IsAllowedType = null;
		public enum Complex_alt_0_ExtendAv2_NodeNums { @a, @b2, @b, @_node0, @_node1, @_node2, };
		public enum Complex_alt_0_ExtendAv2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, @_edge4, };
		public enum Complex_alt_0_ExtendAv2_VariableNums { };
		public enum Complex_alt_0_ExtendAv2_SubNums { };
		public enum Complex_alt_0_ExtendAv2_AltNums { };


		GRGEN_LGSP.PatternGraph Complex_alt_0_ExtendAv2;

		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendNA2_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendNA2_node__node1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Complex_alt_0_ExtendNA2_node_b2_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendNA2_node__node0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_node__node1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_node_b2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendNA2_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendNA2_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendNA2_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Complex_alt_0_ExtendNA2_edge__edge3_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge3_IsAllowedType = null;
		public enum Complex_alt_0_ExtendNA2_NodeNums { @a, @_node0, @_node1, @b, @b2, };
		public enum Complex_alt_0_ExtendNA2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum Complex_alt_0_ExtendNA2_VariableNums { };
		public enum Complex_alt_0_ExtendNA2_SubNums { };
		public enum Complex_alt_0_ExtendNA2_AltNums { };


		GRGEN_LGSP.PatternGraph Complex_alt_0_ExtendNA2;


#if INITIAL_WARMUP
		public Rule_Complex()
#else
		private Rule_Complex()
#endif
		{
			name = "Complex";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] Complex_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Complex_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode Complex_node_a = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "Complex_node_a", "a", Complex_node_a_AllowedTypes, Complex_node_a_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "Complex_node_b", "b", Complex_node_b_AllowedTypes, Complex_node_b_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_edge__edge0", "_edge0", Complex_edge__edge0_AllowedTypes, Complex_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_edge__edge1", "_edge1", Complex_edge__edge1_AllowedTypes, Complex_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] Complex_alt_0_ExtendAv_isNodeHomomorphicGlobal = new bool[5, 5] {
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
			};
			bool[,] Complex_alt_0_ExtendAv_isEdgeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendAv_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "Complex_alt_0_ExtendAv_node_b2", "b2", Complex_alt_0_ExtendAv_node_b2_AllowedTypes, Complex_alt_0_ExtendAv_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendAv_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "Complex_alt_0_ExtendAv_node__node0", "_node0", Complex_alt_0_ExtendAv_node__node0_AllowedTypes, Complex_alt_0_ExtendAv_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendAv_node__node1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "Complex_alt_0_ExtendAv_node__node1", "_node1", Complex_alt_0_ExtendAv_node__node1_AllowedTypes, Complex_alt_0_ExtendAv_node__node1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv_edge__edge0", "_edge0", Complex_alt_0_ExtendAv_edge__edge0_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv_edge__edge1", "_edge1", Complex_alt_0_ExtendAv_edge__edge1_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv_edge__edge2", "_edge2", Complex_alt_0_ExtendAv_edge__edge2_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv_edge__edge3", "_edge3", Complex_alt_0_ExtendAv_edge__edge3_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge3_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendAv = new GRGEN_LGSP.PatternGraph(
				"ExtendAv",
				"Complex_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Complex_node_a, Complex_alt_0_ExtendAv_node_b2, Complex_node_b, Complex_alt_0_ExtendAv_node__node0, Complex_alt_0_ExtendAv_node__node1 }, 
				new GRGEN_LGSP.PatternEdge[] { Complex_alt_0_ExtendAv_edge__edge0, Complex_alt_0_ExtendAv_edge__edge1, Complex_alt_0_ExtendAv_edge__edge2, Complex_alt_0_ExtendAv_edge__edge3 }, 
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
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				Complex_alt_0_ExtendAv_isNodeHomomorphicGlobal,
				Complex_alt_0_ExtendAv_isEdgeHomomorphicGlobal
			);
			Complex_alt_0_ExtendAv.edgeToSourceNode.Add(Complex_alt_0_ExtendAv_edge__edge0, Complex_node_a);
			Complex_alt_0_ExtendAv.edgeToTargetNode.Add(Complex_alt_0_ExtendAv_edge__edge0, Complex_alt_0_ExtendAv_node_b2);
			Complex_alt_0_ExtendAv.edgeToSourceNode.Add(Complex_alt_0_ExtendAv_edge__edge1, Complex_alt_0_ExtendAv_node_b2);
			Complex_alt_0_ExtendAv.edgeToTargetNode.Add(Complex_alt_0_ExtendAv_edge__edge1, Complex_node_a);
			Complex_alt_0_ExtendAv.edgeToSourceNode.Add(Complex_alt_0_ExtendAv_edge__edge2, Complex_node_b);
			Complex_alt_0_ExtendAv.edgeToTargetNode.Add(Complex_alt_0_ExtendAv_edge__edge2, Complex_alt_0_ExtendAv_node__node0);
			Complex_alt_0_ExtendAv.edgeToSourceNode.Add(Complex_alt_0_ExtendAv_edge__edge3, Complex_alt_0_ExtendAv_node__node0);
			Complex_alt_0_ExtendAv.edgeToTargetNode.Add(Complex_alt_0_ExtendAv_edge__edge3, Complex_alt_0_ExtendAv_node__node1);

			bool[,] Complex_alt_0_ExtendAv2_isNodeHomomorphicGlobal = new bool[6, 6] {
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
			};
			bool[,] Complex_alt_0_ExtendAv2_isEdgeHomomorphicGlobal = new bool[5, 5] {
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
			};
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendAv2_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "Complex_alt_0_ExtendAv2_node_b2", "b2", Complex_alt_0_ExtendAv2_node_b2_AllowedTypes, Complex_alt_0_ExtendAv2_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendAv2_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "Complex_alt_0_ExtendAv2_node__node0", "_node0", Complex_alt_0_ExtendAv2_node__node0_AllowedTypes, Complex_alt_0_ExtendAv2_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendAv2_node__node1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "Complex_alt_0_ExtendAv2_node__node1", "_node1", Complex_alt_0_ExtendAv2_node__node1_AllowedTypes, Complex_alt_0_ExtendAv2_node__node1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendAv2_node__node2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "Complex_alt_0_ExtendAv2_node__node2", "_node2", Complex_alt_0_ExtendAv2_node__node2_AllowedTypes, Complex_alt_0_ExtendAv2_node__node2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv2_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv2_edge__edge0", "_edge0", Complex_alt_0_ExtendAv2_edge__edge0_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv2_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv2_edge__edge1", "_edge1", Complex_alt_0_ExtendAv2_edge__edge1_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv2_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv2_edge__edge2", "_edge2", Complex_alt_0_ExtendAv2_edge__edge2_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv2_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv2_edge__edge3", "_edge3", Complex_alt_0_ExtendAv2_edge__edge3_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge3_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendAv2_edge__edge4 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendAv2_edge__edge4", "_edge4", Complex_alt_0_ExtendAv2_edge__edge4_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge4_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendAv2 = new GRGEN_LGSP.PatternGraph(
				"ExtendAv2",
				"Complex_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Complex_node_a, Complex_alt_0_ExtendAv2_node_b2, Complex_node_b, Complex_alt_0_ExtendAv2_node__node0, Complex_alt_0_ExtendAv2_node__node1, Complex_alt_0_ExtendAv2_node__node2 }, 
				new GRGEN_LGSP.PatternEdge[] { Complex_alt_0_ExtendAv2_edge__edge0, Complex_alt_0_ExtendAv2_edge__edge1, Complex_alt_0_ExtendAv2_edge__edge2, Complex_alt_0_ExtendAv2_edge__edge3, Complex_alt_0_ExtendAv2_edge__edge4 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[6, 6] {
					{ true, false, false, false, false, false, },
					{ false, true, false, false, false, false, },
					{ false, false, true, false, false, false, },
					{ false, false, false, true, false, false, },
					{ false, false, false, false, true, false, },
					{ false, false, false, false, false, true, },
				},
				new bool[5, 5] {
					{ true, false, false, false, false, },
					{ false, true, false, false, false, },
					{ false, false, true, false, false, },
					{ false, false, false, true, false, },
					{ false, false, false, false, true, },
				},
				Complex_alt_0_ExtendAv2_isNodeHomomorphicGlobal,
				Complex_alt_0_ExtendAv2_isEdgeHomomorphicGlobal
			);
			Complex_alt_0_ExtendAv2.edgeToSourceNode.Add(Complex_alt_0_ExtendAv2_edge__edge0, Complex_node_a);
			Complex_alt_0_ExtendAv2.edgeToTargetNode.Add(Complex_alt_0_ExtendAv2_edge__edge0, Complex_alt_0_ExtendAv2_node_b2);
			Complex_alt_0_ExtendAv2.edgeToSourceNode.Add(Complex_alt_0_ExtendAv2_edge__edge1, Complex_alt_0_ExtendAv2_node_b2);
			Complex_alt_0_ExtendAv2.edgeToTargetNode.Add(Complex_alt_0_ExtendAv2_edge__edge1, Complex_node_a);
			Complex_alt_0_ExtendAv2.edgeToSourceNode.Add(Complex_alt_0_ExtendAv2_edge__edge2, Complex_node_b);
			Complex_alt_0_ExtendAv2.edgeToTargetNode.Add(Complex_alt_0_ExtendAv2_edge__edge2, Complex_alt_0_ExtendAv2_node__node0);
			Complex_alt_0_ExtendAv2.edgeToSourceNode.Add(Complex_alt_0_ExtendAv2_edge__edge3, Complex_alt_0_ExtendAv2_node__node0);
			Complex_alt_0_ExtendAv2.edgeToTargetNode.Add(Complex_alt_0_ExtendAv2_edge__edge3, Complex_alt_0_ExtendAv2_node__node1);
			Complex_alt_0_ExtendAv2.edgeToSourceNode.Add(Complex_alt_0_ExtendAv2_edge__edge4, Complex_alt_0_ExtendAv2_node__node1);
			Complex_alt_0_ExtendAv2.edgeToTargetNode.Add(Complex_alt_0_ExtendAv2_edge__edge4, Complex_alt_0_ExtendAv2_node__node2);

			bool[,] Complex_alt_0_ExtendNA2_isNodeHomomorphicGlobal = new bool[5, 5] {
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
			};
			bool[,] Complex_alt_0_ExtendNA2_isEdgeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendNA2_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "Complex_alt_0_ExtendNA2_node__node0", "_node0", Complex_alt_0_ExtendNA2_node__node0_AllowedTypes, Complex_alt_0_ExtendNA2_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendNA2_node__node1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "Complex_alt_0_ExtendNA2_node__node1", "_node1", Complex_alt_0_ExtendNA2_node__node1_AllowedTypes, Complex_alt_0_ExtendNA2_node__node1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Complex_alt_0_ExtendNA2_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "Complex_alt_0_ExtendNA2_node_b2", "b2", Complex_alt_0_ExtendNA2_node_b2_AllowedTypes, Complex_alt_0_ExtendNA2_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendNA2_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendNA2_edge__edge0", "_edge0", Complex_alt_0_ExtendNA2_edge__edge0_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendNA2_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendNA2_edge__edge1", "_edge1", Complex_alt_0_ExtendNA2_edge__edge1_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendNA2_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendNA2_edge__edge2", "_edge2", Complex_alt_0_ExtendNA2_edge__edge2_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Complex_alt_0_ExtendNA2_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "Complex_alt_0_ExtendNA2_edge__edge3", "_edge3", Complex_alt_0_ExtendNA2_edge__edge3_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge3_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendNA2 = new GRGEN_LGSP.PatternGraph(
				"ExtendNA2",
				"Complex_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Complex_node_a, Complex_alt_0_ExtendNA2_node__node0, Complex_alt_0_ExtendNA2_node__node1, Complex_node_b, Complex_alt_0_ExtendNA2_node_b2 }, 
				new GRGEN_LGSP.PatternEdge[] { Complex_alt_0_ExtendNA2_edge__edge0, Complex_alt_0_ExtendNA2_edge__edge1, Complex_alt_0_ExtendNA2_edge__edge2, Complex_alt_0_ExtendNA2_edge__edge3 }, 
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
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				Complex_alt_0_ExtendNA2_isNodeHomomorphicGlobal,
				Complex_alt_0_ExtendNA2_isEdgeHomomorphicGlobal
			);
			Complex_alt_0_ExtendNA2.edgeToSourceNode.Add(Complex_alt_0_ExtendNA2_edge__edge0, Complex_node_a);
			Complex_alt_0_ExtendNA2.edgeToTargetNode.Add(Complex_alt_0_ExtendNA2_edge__edge0, Complex_alt_0_ExtendNA2_node__node0);
			Complex_alt_0_ExtendNA2.edgeToSourceNode.Add(Complex_alt_0_ExtendNA2_edge__edge1, Complex_alt_0_ExtendNA2_node__node0);
			Complex_alt_0_ExtendNA2.edgeToTargetNode.Add(Complex_alt_0_ExtendNA2_edge__edge1, Complex_alt_0_ExtendNA2_node__node1);
			Complex_alt_0_ExtendNA2.edgeToSourceNode.Add(Complex_alt_0_ExtendNA2_edge__edge2, Complex_node_b);
			Complex_alt_0_ExtendNA2.edgeToTargetNode.Add(Complex_alt_0_ExtendNA2_edge__edge2, Complex_alt_0_ExtendNA2_node_b2);
			Complex_alt_0_ExtendNA2.edgeToSourceNode.Add(Complex_alt_0_ExtendNA2_edge__edge3, Complex_alt_0_ExtendNA2_node_b2);
			Complex_alt_0_ExtendNA2.edgeToTargetNode.Add(Complex_alt_0_ExtendNA2_edge__edge3, Complex_node_b);

			GRGEN_LGSP.Alternative Complex_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "Complex_", new GRGEN_LGSP.PatternGraph[] { Complex_alt_0_ExtendAv, Complex_alt_0_ExtendAv2, Complex_alt_0_ExtendNA2 } );

			pat_Complex = new GRGEN_LGSP.PatternGraph(
				"Complex",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Complex_node_a, Complex_node_b }, 
				new GRGEN_LGSP.PatternEdge[] { Complex_edge__edge0, Complex_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { Complex_alt_0,  }, 
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
				Complex_isNodeHomomorphicGlobal,
				Complex_isEdgeHomomorphicGlobal
			);
			pat_Complex.edgeToSourceNode.Add(Complex_edge__edge0, Complex_node_a);
			pat_Complex.edgeToTargetNode.Add(Complex_edge__edge0, Complex_node_b);
			pat_Complex.edgeToSourceNode.Add(Complex_edge__edge1, Complex_node_b);
			pat_Complex.edgeToTargetNode.Add(Complex_edge__edge1, Complex_node_a);
			Complex_alt_0_ExtendAv.embeddingGraph = pat_Complex;
			Complex_alt_0_ExtendAv2.embeddingGraph = pat_Complex;
			Complex_alt_0_ExtendNA2.embeddingGraph = pat_Complex;

			Complex_node_a.PointOfDefinition = pat_Complex;
			Complex_node_b.PointOfDefinition = pat_Complex;
			Complex_edge__edge0.PointOfDefinition = pat_Complex;
			Complex_edge__edge1.PointOfDefinition = pat_Complex;
			Complex_alt_0_ExtendAv_node_b2.PointOfDefinition = Complex_alt_0_ExtendAv;
			Complex_alt_0_ExtendAv_node__node0.PointOfDefinition = Complex_alt_0_ExtendAv;
			Complex_alt_0_ExtendAv_node__node1.PointOfDefinition = Complex_alt_0_ExtendAv;
			Complex_alt_0_ExtendAv_edge__edge0.PointOfDefinition = Complex_alt_0_ExtendAv;
			Complex_alt_0_ExtendAv_edge__edge1.PointOfDefinition = Complex_alt_0_ExtendAv;
			Complex_alt_0_ExtendAv_edge__edge2.PointOfDefinition = Complex_alt_0_ExtendAv;
			Complex_alt_0_ExtendAv_edge__edge3.PointOfDefinition = Complex_alt_0_ExtendAv;
			Complex_alt_0_ExtendAv2_node_b2.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_node__node0.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_node__node1.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_node__node2.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_edge__edge0.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_edge__edge1.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_edge__edge2.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_edge__edge3.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendAv2_edge__edge4.PointOfDefinition = Complex_alt_0_ExtendAv2;
			Complex_alt_0_ExtendNA2_node__node0.PointOfDefinition = Complex_alt_0_ExtendNA2;
			Complex_alt_0_ExtendNA2_node__node1.PointOfDefinition = Complex_alt_0_ExtendNA2;
			Complex_alt_0_ExtendNA2_node_b2.PointOfDefinition = Complex_alt_0_ExtendNA2;
			Complex_alt_0_ExtendNA2_edge__edge0.PointOfDefinition = Complex_alt_0_ExtendNA2;
			Complex_alt_0_ExtendNA2_edge__edge1.PointOfDefinition = Complex_alt_0_ExtendNA2;
			Complex_alt_0_ExtendNA2_edge__edge2.PointOfDefinition = Complex_alt_0_ExtendNA2;
			Complex_alt_0_ExtendNA2_edge__edge3.PointOfDefinition = Complex_alt_0_ExtendNA2;

			patternGraph = pat_Complex;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex curMatch = (Match_Complex)_curMatch;
			IMatch_Complex_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex curMatch = (Match_Complex)_curMatch;
			IMatch_Complex_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void Complex_alt_0_ExtendAv_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex_alt_0_ExtendAv curMatch = (Match_Complex_alt_0_ExtendAv)_curMatch;
		}

		public void Complex_alt_0_ExtendAv_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex_alt_0_ExtendAv curMatch = (Match_Complex_alt_0_ExtendAv)_curMatch;
		}

		public void Complex_alt_0_ExtendAv2_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex_alt_0_ExtendAv2 curMatch = (Match_Complex_alt_0_ExtendAv2)_curMatch;
		}

		public void Complex_alt_0_ExtendAv2_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex_alt_0_ExtendAv2 curMatch = (Match_Complex_alt_0_ExtendAv2)_curMatch;
		}

		public void Complex_alt_0_ExtendNA2_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex_alt_0_ExtendNA2 curMatch = (Match_Complex_alt_0_ExtendNA2)_curMatch;
		}

		public void Complex_alt_0_ExtendNA2_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Complex_alt_0_ExtendNA2 curMatch = (Match_Complex_alt_0_ExtendNA2)_curMatch;
		}

		static Rule_Complex() {
		}

		public interface IMatch_Complex : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IA node_a { get; }
			IB node_b { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_Complex_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_Complex_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_Complex_alt_0_ExtendAv : IMatch_Complex_alt_0
		{
			//Nodes
			IA node_a { get; }
			IB node_b2 { get; }
			IB node_b { get; }
			IC node__node0 { get; }
			IC node__node1 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			GRGEN_LIBGR.IEdge edge__edge3 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_Complex_alt_0_ExtendAv2 : IMatch_Complex_alt_0
		{
			//Nodes
			IA node_a { get; }
			IB node_b2 { get; }
			IB node_b { get; }
			IC node__node0 { get; }
			IC node__node1 { get; }
			IC node__node2 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			GRGEN_LIBGR.IEdge edge__edge3 { get; }
			GRGEN_LIBGR.IEdge edge__edge4 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_Complex_alt_0_ExtendNA2 : IMatch_Complex_alt_0
		{
			//Nodes
			IA node_a { get; }
			IC node__node0 { get; }
			IC node__node1 { get; }
			IB node_b { get; }
			IB node_b2 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			GRGEN_LIBGR.IEdge edge__edge3 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_Complex : GRGEN_LGSP.ListElement<Match_Complex>, IMatch_Complex
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b { get { return (IB)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum Complex_NodeNums { @a, @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Complex_NodeNums.@a: return _node_a;
				case (int)Complex_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum Complex_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Complex_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Complex_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum Complex_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_Complex_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_Complex_alt_0 _alt_0;
			public enum Complex_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)Complex_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum Complex_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_Complex.instance.pat_Complex; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Complex_alt_0_ExtendAv : GRGEN_LGSP.ListElement<Match_Complex_alt_0_ExtendAv>, IMatch_Complex_alt_0_ExtendAv
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public IB node_b { get { return (IB)_node_b; } }
			public IC node__node0 { get { return (IC)_node__node0; } }
			public IC node__node1 { get { return (IC)_node__node1; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public enum Complex_alt_0_ExtendAv_NodeNums { @a, @b2, @b, @_node0, @_node1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 5;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Complex_alt_0_ExtendAv_NodeNums.@a: return _node_a;
				case (int)Complex_alt_0_ExtendAv_NodeNums.@b2: return _node_b2;
				case (int)Complex_alt_0_ExtendAv_NodeNums.@b: return _node_b;
				case (int)Complex_alt_0_ExtendAv_NodeNums.@_node0: return _node__node0;
				case (int)Complex_alt_0_ExtendAv_NodeNums.@_node1: return _node__node1;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LIBGR.IEdge edge__edge3 { get { return (GRGEN_LIBGR.IEdge)_edge__edge3; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public enum Complex_alt_0_ExtendAv_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 4;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Complex_alt_0_ExtendAv_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Complex_alt_0_ExtendAv_EdgeNums.@_edge1: return _edge__edge1;
				case (int)Complex_alt_0_ExtendAv_EdgeNums.@_edge2: return _edge__edge2;
				case (int)Complex_alt_0_ExtendAv_EdgeNums.@_edge3: return _edge__edge3;
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_Complex.instance.Complex_alt_0_ExtendAv; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Complex_alt_0_ExtendAv2 : GRGEN_LGSP.ListElement<Match_Complex_alt_0_ExtendAv2>, IMatch_Complex_alt_0_ExtendAv2
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public IB node_b { get { return (IB)_node_b; } }
			public IC node__node0 { get { return (IC)_node__node0; } }
			public IC node__node1 { get { return (IC)_node__node1; } }
			public IC node__node2 { get { return (IC)_node__node2; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public GRGEN_LGSP.LGSPNode _node__node2;
			public enum Complex_alt_0_ExtendAv2_NodeNums { @a, @b2, @b, @_node0, @_node1, @_node2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 6;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Complex_alt_0_ExtendAv2_NodeNums.@a: return _node_a;
				case (int)Complex_alt_0_ExtendAv2_NodeNums.@b2: return _node_b2;
				case (int)Complex_alt_0_ExtendAv2_NodeNums.@b: return _node_b;
				case (int)Complex_alt_0_ExtendAv2_NodeNums.@_node0: return _node__node0;
				case (int)Complex_alt_0_ExtendAv2_NodeNums.@_node1: return _node__node1;
				case (int)Complex_alt_0_ExtendAv2_NodeNums.@_node2: return _node__node2;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LIBGR.IEdge edge__edge3 { get { return (GRGEN_LIBGR.IEdge)_edge__edge3; } }
			public GRGEN_LIBGR.IEdge edge__edge4 { get { return (GRGEN_LIBGR.IEdge)_edge__edge4; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public GRGEN_LGSP.LGSPEdge _edge__edge4;
			public enum Complex_alt_0_ExtendAv2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, @_edge4, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 5;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Complex_alt_0_ExtendAv2_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Complex_alt_0_ExtendAv2_EdgeNums.@_edge1: return _edge__edge1;
				case (int)Complex_alt_0_ExtendAv2_EdgeNums.@_edge2: return _edge__edge2;
				case (int)Complex_alt_0_ExtendAv2_EdgeNums.@_edge3: return _edge__edge3;
				case (int)Complex_alt_0_ExtendAv2_EdgeNums.@_edge4: return _edge__edge4;
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv2_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv2_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv2_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendAv2_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_Complex.instance.Complex_alt_0_ExtendAv2; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Complex_alt_0_ExtendNA2 : GRGEN_LGSP.ListElement<Match_Complex_alt_0_ExtendNA2>, IMatch_Complex_alt_0_ExtendNA2
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IC node__node0 { get { return (IC)_node__node0; } }
			public IC node__node1 { get { return (IC)_node__node1; } }
			public IB node_b { get { return (IB)_node_b; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public enum Complex_alt_0_ExtendNA2_NodeNums { @a, @_node0, @_node1, @b, @b2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 5;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Complex_alt_0_ExtendNA2_NodeNums.@a: return _node_a;
				case (int)Complex_alt_0_ExtendNA2_NodeNums.@_node0: return _node__node0;
				case (int)Complex_alt_0_ExtendNA2_NodeNums.@_node1: return _node__node1;
				case (int)Complex_alt_0_ExtendNA2_NodeNums.@b: return _node_b;
				case (int)Complex_alt_0_ExtendNA2_NodeNums.@b2: return _node_b2;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LIBGR.IEdge edge__edge3 { get { return (GRGEN_LIBGR.IEdge)_edge__edge3; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public enum Complex_alt_0_ExtendNA2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 4;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Complex_alt_0_ExtendNA2_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Complex_alt_0_ExtendNA2_EdgeNums.@_edge1: return _edge__edge1;
				case (int)Complex_alt_0_ExtendNA2_EdgeNums.@_edge2: return _edge__edge2;
				case (int)Complex_alt_0_ExtendNA2_EdgeNums.@_edge3: return _edge__edge3;
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendNA2_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendNA2_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendNA2_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Complex_alt_0_ExtendNA2_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_Complex.instance.Complex_alt_0_ExtendNA2; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_ComplexMax : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ComplexMax instance = null;
		public static Rule_ComplexMax Instance { get { if (instance==null) { instance = new Rule_ComplexMax(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] ComplexMax_node_a_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_node_b_AllowedTypes = null;
		public static bool[] ComplexMax_node_a_IsAllowedType = null;
		public static bool[] ComplexMax_node_b_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_edge__edge1_AllowedTypes = null;
		public static bool[] ComplexMax_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_edge__edge1_IsAllowedType = null;
		public enum ComplexMax_NodeNums { @a, @b, };
		public enum ComplexMax_EdgeNums { @_edge0, @_edge1, };
		public enum ComplexMax_VariableNums { };
		public enum ComplexMax_SubNums { };
		public enum ComplexMax_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_ComplexMax;

		public enum ComplexMax_alt_0_CaseNums { @ExtendAv, @ExtendAv2, @ExtendNA2, };
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv_node_b2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv_node_c_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_node_b2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_node__node0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_node_c_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge3_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge3_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendAv_NodeNums { @a, @b2, @b, @_node0, @c, };
		public enum ComplexMax_alt_0_ExtendAv_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum ComplexMax_alt_0_ExtendAv_VariableNums { };
		public enum ComplexMax_alt_0_ExtendAv_SubNums { };
		public enum ComplexMax_alt_0_ExtendAv_AltNums { };


		GRGEN_LGSP.PatternGraph ComplexMax_alt_0_ExtendAv;

		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendAv_neg_0_NodeNums { @c, @_node0, };
		public enum ComplexMax_alt_0_ExtendAv_neg_0_EdgeNums { @_edge0, };
		public enum ComplexMax_alt_0_ExtendAv_neg_0_VariableNums { };
		public enum ComplexMax_alt_0_ExtendAv_neg_0_SubNums { };
		public enum ComplexMax_alt_0_ExtendAv_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph ComplexMax_alt_0_ExtendAv_neg_0;

		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv2_node_b2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv2_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv2_node__node1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendAv2_node__node2_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node_b2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node__node0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node__node1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node__node2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge3_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge4_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge3_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge4_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendAv2_NodeNums { @a, @b2, @b, @_node0, @_node1, @_node2, };
		public enum ComplexMax_alt_0_ExtendAv2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, @_edge4, };
		public enum ComplexMax_alt_0_ExtendAv2_VariableNums { };
		public enum ComplexMax_alt_0_ExtendAv2_SubNums { };
		public enum ComplexMax_alt_0_ExtendAv2_AltNums { };


		GRGEN_LGSP.PatternGraph ComplexMax_alt_0_ExtendAv2;

		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendNA2_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendNA2_node__node1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ComplexMax_alt_0_ExtendNA2_node_b2_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_node__node0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_node__node1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_node_b2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge3_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge3_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendNA2_NodeNums { @a, @_node0, @_node1, @b, @b2, };
		public enum ComplexMax_alt_0_ExtendNA2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum ComplexMax_alt_0_ExtendNA2_VariableNums { };
		public enum ComplexMax_alt_0_ExtendNA2_SubNums { };
		public enum ComplexMax_alt_0_ExtendNA2_AltNums { };


		GRGEN_LGSP.PatternGraph ComplexMax_alt_0_ExtendNA2;


#if INITIAL_WARMUP
		public Rule_ComplexMax()
#else
		private Rule_ComplexMax()
#endif
		{
			name = "ComplexMax";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] ComplexMax_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ComplexMax_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode ComplexMax_node_a = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "ComplexMax_node_a", "a", ComplexMax_node_a_AllowedTypes, ComplexMax_node_a_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "ComplexMax_node_b", "b", ComplexMax_node_b_AllowedTypes, ComplexMax_node_b_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_edge__edge0", "_edge0", ComplexMax_edge__edge0_AllowedTypes, ComplexMax_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_edge__edge1", "_edge1", ComplexMax_edge__edge1_AllowedTypes, ComplexMax_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] ComplexMax_alt_0_ExtendAv_isNodeHomomorphicGlobal = new bool[5, 5] {
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
			};
			bool[,] ComplexMax_alt_0_ExtendAv_isEdgeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "ComplexMax_alt_0_ExtendAv_node_b2", "b2", ComplexMax_alt_0_ExtendAv_node_b2_AllowedTypes, ComplexMax_alt_0_ExtendAv_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendAv_node__node0", "_node0", ComplexMax_alt_0_ExtendAv_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendAv_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv_node_c = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendAv_node_c", "c", ComplexMax_alt_0_ExtendAv_node_c_AllowedTypes, ComplexMax_alt_0_ExtendAv_node_c_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendAv_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv_edge__edge1", "_edge1", ComplexMax_alt_0_ExtendAv_edge__edge1_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv_edge__edge2", "_edge2", ComplexMax_alt_0_ExtendAv_edge__edge2_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv_edge__edge3", "_edge3", ComplexMax_alt_0_ExtendAv_edge__edge3_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge3_IsAllowedType, 5.5F, -1);
			bool[,] ComplexMax_alt_0_ExtendAv_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ComplexMax_alt_0_ExtendAv_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendAv_neg_0_node__node0", "_node0", ComplexMax_alt_0_ExtendAv_neg_0_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendAv_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			ComplexMax_alt_0_ExtendAv_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ComplexMax_alt_0_ExtendAv_",
				false,
				new GRGEN_LGSP.PatternNode[] { ComplexMax_alt_0_ExtendAv_node_c, ComplexMax_alt_0_ExtendAv_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 }, 
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
				ComplexMax_alt_0_ExtendAv_neg_0_isNodeHomomorphicGlobal,
				ComplexMax_alt_0_ExtendAv_neg_0_isEdgeHomomorphicGlobal
			);
			ComplexMax_alt_0_ExtendAv_neg_0.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0, ComplexMax_alt_0_ExtendAv_node_c);
			ComplexMax_alt_0_ExtendAv_neg_0.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0, ComplexMax_alt_0_ExtendAv_neg_0_node__node0);

			ComplexMax_alt_0_ExtendAv = new GRGEN_LGSP.PatternGraph(
				"ExtendAv",
				"ComplexMax_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ComplexMax_node_a, ComplexMax_alt_0_ExtendAv_node_b2, ComplexMax_node_b, ComplexMax_alt_0_ExtendAv_node__node0, ComplexMax_alt_0_ExtendAv_node_c }, 
				new GRGEN_LGSP.PatternEdge[] { ComplexMax_alt_0_ExtendAv_edge__edge0, ComplexMax_alt_0_ExtendAv_edge__edge1, ComplexMax_alt_0_ExtendAv_edge__edge2, ComplexMax_alt_0_ExtendAv_edge__edge3 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ComplexMax_alt_0_ExtendAv_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[5, 5] {
					{ true, false, false, false, false, },
					{ false, true, false, false, false, },
					{ false, false, true, false, false, },
					{ false, false, false, true, false, },
					{ false, false, false, false, true, },
				},
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				ComplexMax_alt_0_ExtendAv_isNodeHomomorphicGlobal,
				ComplexMax_alt_0_ExtendAv_isEdgeHomomorphicGlobal
			);
			ComplexMax_alt_0_ExtendAv.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge0, ComplexMax_node_a);
			ComplexMax_alt_0_ExtendAv.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge0, ComplexMax_alt_0_ExtendAv_node_b2);
			ComplexMax_alt_0_ExtendAv.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge1, ComplexMax_alt_0_ExtendAv_node_b2);
			ComplexMax_alt_0_ExtendAv.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge1, ComplexMax_node_a);
			ComplexMax_alt_0_ExtendAv.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge2, ComplexMax_node_b);
			ComplexMax_alt_0_ExtendAv.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge2, ComplexMax_alt_0_ExtendAv_node__node0);
			ComplexMax_alt_0_ExtendAv.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge3, ComplexMax_alt_0_ExtendAv_node__node0);
			ComplexMax_alt_0_ExtendAv.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv_edge__edge3, ComplexMax_alt_0_ExtendAv_node_c);
			ComplexMax_alt_0_ExtendAv_neg_0.embeddingGraph = ComplexMax_alt_0_ExtendAv;

			bool[,] ComplexMax_alt_0_ExtendAv2_isNodeHomomorphicGlobal = new bool[6, 6] {
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
				{ false, false, false, false, false, false, },
			};
			bool[,] ComplexMax_alt_0_ExtendAv2_isEdgeHomomorphicGlobal = new bool[5, 5] {
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
			};
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv2_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "ComplexMax_alt_0_ExtendAv2_node_b2", "b2", ComplexMax_alt_0_ExtendAv2_node_b2_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv2_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendAv2_node__node0", "_node0", ComplexMax_alt_0_ExtendAv2_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv2_node__node1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendAv2_node__node1", "_node1", ComplexMax_alt_0_ExtendAv2_node__node1_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node__node1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendAv2_node__node2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendAv2_node__node2", "_node2", ComplexMax_alt_0_ExtendAv2_node__node2_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node__node2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv2_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendAv2_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv2_edge__edge1", "_edge1", ComplexMax_alt_0_ExtendAv2_edge__edge1_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv2_edge__edge2", "_edge2", ComplexMax_alt_0_ExtendAv2_edge__edge2_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv2_edge__edge3", "_edge3", ComplexMax_alt_0_ExtendAv2_edge__edge3_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge3_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge4 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendAv2_edge__edge4", "_edge4", ComplexMax_alt_0_ExtendAv2_edge__edge4_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge4_IsAllowedType, 5.5F, -1);
			ComplexMax_alt_0_ExtendAv2 = new GRGEN_LGSP.PatternGraph(
				"ExtendAv2",
				"ComplexMax_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ComplexMax_node_a, ComplexMax_alt_0_ExtendAv2_node_b2, ComplexMax_node_b, ComplexMax_alt_0_ExtendAv2_node__node0, ComplexMax_alt_0_ExtendAv2_node__node1, ComplexMax_alt_0_ExtendAv2_node__node2 }, 
				new GRGEN_LGSP.PatternEdge[] { ComplexMax_alt_0_ExtendAv2_edge__edge0, ComplexMax_alt_0_ExtendAv2_edge__edge1, ComplexMax_alt_0_ExtendAv2_edge__edge2, ComplexMax_alt_0_ExtendAv2_edge__edge3, ComplexMax_alt_0_ExtendAv2_edge__edge4 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[6, 6] {
					{ true, false, false, false, false, false, },
					{ false, true, false, false, false, false, },
					{ false, false, true, false, false, false, },
					{ false, false, false, true, false, false, },
					{ false, false, false, false, true, false, },
					{ false, false, false, false, false, true, },
				},
				new bool[5, 5] {
					{ true, false, false, false, false, },
					{ false, true, false, false, false, },
					{ false, false, true, false, false, },
					{ false, false, false, true, false, },
					{ false, false, false, false, true, },
				},
				ComplexMax_alt_0_ExtendAv2_isNodeHomomorphicGlobal,
				ComplexMax_alt_0_ExtendAv2_isEdgeHomomorphicGlobal
			);
			ComplexMax_alt_0_ExtendAv2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge0, ComplexMax_node_a);
			ComplexMax_alt_0_ExtendAv2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge0, ComplexMax_alt_0_ExtendAv2_node_b2);
			ComplexMax_alt_0_ExtendAv2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge1, ComplexMax_alt_0_ExtendAv2_node_b2);
			ComplexMax_alt_0_ExtendAv2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge1, ComplexMax_node_a);
			ComplexMax_alt_0_ExtendAv2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge2, ComplexMax_node_b);
			ComplexMax_alt_0_ExtendAv2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge2, ComplexMax_alt_0_ExtendAv2_node__node0);
			ComplexMax_alt_0_ExtendAv2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge3, ComplexMax_alt_0_ExtendAv2_node__node0);
			ComplexMax_alt_0_ExtendAv2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge3, ComplexMax_alt_0_ExtendAv2_node__node1);
			ComplexMax_alt_0_ExtendAv2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge4, ComplexMax_alt_0_ExtendAv2_node__node1);
			ComplexMax_alt_0_ExtendAv2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv2_edge__edge4, ComplexMax_alt_0_ExtendAv2_node__node2);

			bool[,] ComplexMax_alt_0_ExtendNA2_isNodeHomomorphicGlobal = new bool[5, 5] {
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
			};
			bool[,] ComplexMax_alt_0_ExtendNA2_isEdgeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendNA2_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendNA2_node__node0", "_node0", ComplexMax_alt_0_ExtendNA2_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendNA2_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendNA2_node__node1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@C, "IC", "ComplexMax_alt_0_ExtendNA2_node__node1", "_node1", ComplexMax_alt_0_ExtendNA2_node__node1_AllowedTypes, ComplexMax_alt_0_ExtendNA2_node__node1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ComplexMax_alt_0_ExtendNA2_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "ComplexMax_alt_0_ExtendNA2_node_b2", "b2", ComplexMax_alt_0_ExtendNA2_node_b2_AllowedTypes, ComplexMax_alt_0_ExtendNA2_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendNA2_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendNA2_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendNA2_edge__edge1", "_edge1", ComplexMax_alt_0_ExtendNA2_edge__edge1_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendNA2_edge__edge2", "_edge2", ComplexMax_alt_0_ExtendNA2_edge__edge2_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "ComplexMax_alt_0_ExtendNA2_edge__edge3", "_edge3", ComplexMax_alt_0_ExtendNA2_edge__edge3_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge3_IsAllowedType, 5.5F, -1);
			ComplexMax_alt_0_ExtendNA2 = new GRGEN_LGSP.PatternGraph(
				"ExtendNA2",
				"ComplexMax_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ComplexMax_node_a, ComplexMax_alt_0_ExtendNA2_node__node0, ComplexMax_alt_0_ExtendNA2_node__node1, ComplexMax_node_b, ComplexMax_alt_0_ExtendNA2_node_b2 }, 
				new GRGEN_LGSP.PatternEdge[] { ComplexMax_alt_0_ExtendNA2_edge__edge0, ComplexMax_alt_0_ExtendNA2_edge__edge1, ComplexMax_alt_0_ExtendNA2_edge__edge2, ComplexMax_alt_0_ExtendNA2_edge__edge3 }, 
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
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				ComplexMax_alt_0_ExtendNA2_isNodeHomomorphicGlobal,
				ComplexMax_alt_0_ExtendNA2_isEdgeHomomorphicGlobal
			);
			ComplexMax_alt_0_ExtendNA2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge0, ComplexMax_node_a);
			ComplexMax_alt_0_ExtendNA2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge0, ComplexMax_alt_0_ExtendNA2_node__node0);
			ComplexMax_alt_0_ExtendNA2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge1, ComplexMax_alt_0_ExtendNA2_node__node0);
			ComplexMax_alt_0_ExtendNA2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge1, ComplexMax_alt_0_ExtendNA2_node__node1);
			ComplexMax_alt_0_ExtendNA2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge2, ComplexMax_node_b);
			ComplexMax_alt_0_ExtendNA2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge2, ComplexMax_alt_0_ExtendNA2_node_b2);
			ComplexMax_alt_0_ExtendNA2.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge3, ComplexMax_alt_0_ExtendNA2_node_b2);
			ComplexMax_alt_0_ExtendNA2.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendNA2_edge__edge3, ComplexMax_node_b);

			GRGEN_LGSP.Alternative ComplexMax_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ComplexMax_", new GRGEN_LGSP.PatternGraph[] { ComplexMax_alt_0_ExtendAv, ComplexMax_alt_0_ExtendAv2, ComplexMax_alt_0_ExtendNA2 } );

			pat_ComplexMax = new GRGEN_LGSP.PatternGraph(
				"ComplexMax",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ComplexMax_node_a, ComplexMax_node_b }, 
				new GRGEN_LGSP.PatternEdge[] { ComplexMax_edge__edge0, ComplexMax_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ComplexMax_alt_0,  }, 
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
				ComplexMax_isNodeHomomorphicGlobal,
				ComplexMax_isEdgeHomomorphicGlobal
			);
			pat_ComplexMax.edgeToSourceNode.Add(ComplexMax_edge__edge0, ComplexMax_node_a);
			pat_ComplexMax.edgeToTargetNode.Add(ComplexMax_edge__edge0, ComplexMax_node_b);
			pat_ComplexMax.edgeToSourceNode.Add(ComplexMax_edge__edge1, ComplexMax_node_b);
			pat_ComplexMax.edgeToTargetNode.Add(ComplexMax_edge__edge1, ComplexMax_node_a);
			ComplexMax_alt_0_ExtendAv.embeddingGraph = pat_ComplexMax;
			ComplexMax_alt_0_ExtendAv2.embeddingGraph = pat_ComplexMax;
			ComplexMax_alt_0_ExtendNA2.embeddingGraph = pat_ComplexMax;

			ComplexMax_node_a.PointOfDefinition = pat_ComplexMax;
			ComplexMax_node_b.PointOfDefinition = pat_ComplexMax;
			ComplexMax_edge__edge0.PointOfDefinition = pat_ComplexMax;
			ComplexMax_edge__edge1.PointOfDefinition = pat_ComplexMax;
			ComplexMax_alt_0_ExtendAv_node_b2.PointOfDefinition = ComplexMax_alt_0_ExtendAv;
			ComplexMax_alt_0_ExtendAv_node__node0.PointOfDefinition = ComplexMax_alt_0_ExtendAv;
			ComplexMax_alt_0_ExtendAv_node_c.PointOfDefinition = ComplexMax_alt_0_ExtendAv;
			ComplexMax_alt_0_ExtendAv_edge__edge0.PointOfDefinition = ComplexMax_alt_0_ExtendAv;
			ComplexMax_alt_0_ExtendAv_edge__edge1.PointOfDefinition = ComplexMax_alt_0_ExtendAv;
			ComplexMax_alt_0_ExtendAv_edge__edge2.PointOfDefinition = ComplexMax_alt_0_ExtendAv;
			ComplexMax_alt_0_ExtendAv_edge__edge3.PointOfDefinition = ComplexMax_alt_0_ExtendAv;
			ComplexMax_alt_0_ExtendAv_neg_0_node__node0.PointOfDefinition = ComplexMax_alt_0_ExtendAv_neg_0;
			ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.PointOfDefinition = ComplexMax_alt_0_ExtendAv_neg_0;
			ComplexMax_alt_0_ExtendAv2_node_b2.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_node__node0.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_node__node1.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_node__node2.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_edge__edge0.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_edge__edge1.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_edge__edge2.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_edge__edge3.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendAv2_edge__edge4.PointOfDefinition = ComplexMax_alt_0_ExtendAv2;
			ComplexMax_alt_0_ExtendNA2_node__node0.PointOfDefinition = ComplexMax_alt_0_ExtendNA2;
			ComplexMax_alt_0_ExtendNA2_node__node1.PointOfDefinition = ComplexMax_alt_0_ExtendNA2;
			ComplexMax_alt_0_ExtendNA2_node_b2.PointOfDefinition = ComplexMax_alt_0_ExtendNA2;
			ComplexMax_alt_0_ExtendNA2_edge__edge0.PointOfDefinition = ComplexMax_alt_0_ExtendNA2;
			ComplexMax_alt_0_ExtendNA2_edge__edge1.PointOfDefinition = ComplexMax_alt_0_ExtendNA2;
			ComplexMax_alt_0_ExtendNA2_edge__edge2.PointOfDefinition = ComplexMax_alt_0_ExtendNA2;
			ComplexMax_alt_0_ExtendNA2_edge__edge3.PointOfDefinition = ComplexMax_alt_0_ExtendNA2;

			patternGraph = pat_ComplexMax;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax curMatch = (Match_ComplexMax)_curMatch;
			IMatch_ComplexMax_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax curMatch = (Match_ComplexMax)_curMatch;
			IMatch_ComplexMax_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void ComplexMax_alt_0_ExtendAv_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax_alt_0_ExtendAv curMatch = (Match_ComplexMax_alt_0_ExtendAv)_curMatch;
		}

		public void ComplexMax_alt_0_ExtendAv_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax_alt_0_ExtendAv curMatch = (Match_ComplexMax_alt_0_ExtendAv)_curMatch;
		}

		public void ComplexMax_alt_0_ExtendAv2_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax_alt_0_ExtendAv2 curMatch = (Match_ComplexMax_alt_0_ExtendAv2)_curMatch;
		}

		public void ComplexMax_alt_0_ExtendAv2_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax_alt_0_ExtendAv2 curMatch = (Match_ComplexMax_alt_0_ExtendAv2)_curMatch;
		}

		public void ComplexMax_alt_0_ExtendNA2_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax_alt_0_ExtendNA2 curMatch = (Match_ComplexMax_alt_0_ExtendNA2)_curMatch;
		}

		public void ComplexMax_alt_0_ExtendNA2_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ComplexMax_alt_0_ExtendNA2 curMatch = (Match_ComplexMax_alt_0_ExtendNA2)_curMatch;
		}

		static Rule_ComplexMax() {
		}

		public interface IMatch_ComplexMax : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IA node_a { get; }
			IB node_b { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ComplexMax_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_ComplexMax_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_ComplexMax_alt_0_ExtendAv : IMatch_ComplexMax_alt_0
		{
			//Nodes
			IA node_a { get; }
			IB node_b2 { get; }
			IB node_b { get; }
			IC node__node0 { get; }
			IC node_c { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			GRGEN_LIBGR.IEdge edge__edge3 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_ComplexMax_alt_0_ExtendAv_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IC node_c { get; }
			IC node__node0 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_ComplexMax_alt_0_ExtendAv2 : IMatch_ComplexMax_alt_0
		{
			//Nodes
			IA node_a { get; }
			IB node_b2 { get; }
			IB node_b { get; }
			IC node__node0 { get; }
			IC node__node1 { get; }
			IC node__node2 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			GRGEN_LIBGR.IEdge edge__edge3 { get; }
			GRGEN_LIBGR.IEdge edge__edge4 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_ComplexMax_alt_0_ExtendNA2 : IMatch_ComplexMax_alt_0
		{
			//Nodes
			IA node_a { get; }
			IC node__node0 { get; }
			IC node__node1 { get; }
			IB node_b { get; }
			IB node_b2 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			GRGEN_LIBGR.IEdge edge__edge3 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_ComplexMax : GRGEN_LGSP.ListElement<Match_ComplexMax>, IMatch_ComplexMax
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b { get { return (IB)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum ComplexMax_NodeNums { @a, @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_NodeNums.@a: return _node_a;
				case (int)ComplexMax_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum ComplexMax_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_EdgeNums.@_edge0: return _edge__edge0;
				case (int)ComplexMax_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum ComplexMax_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ComplexMax_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ComplexMax_alt_0 _alt_0;
			public enum ComplexMax_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ComplexMax_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ComplexMax.instance.pat_ComplexMax; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ComplexMax_alt_0_ExtendAv : GRGEN_LGSP.ListElement<Match_ComplexMax_alt_0_ExtendAv>, IMatch_ComplexMax_alt_0_ExtendAv
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public IB node_b { get { return (IB)_node_b; } }
			public IC node__node0 { get { return (IC)_node__node0; } }
			public IC node_c { get { return (IC)_node_c; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_c;
			public enum ComplexMax_alt_0_ExtendAv_NodeNums { @a, @b2, @b, @_node0, @c, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 5;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendAv_NodeNums.@a: return _node_a;
				case (int)ComplexMax_alt_0_ExtendAv_NodeNums.@b2: return _node_b2;
				case (int)ComplexMax_alt_0_ExtendAv_NodeNums.@b: return _node_b;
				case (int)ComplexMax_alt_0_ExtendAv_NodeNums.@_node0: return _node__node0;
				case (int)ComplexMax_alt_0_ExtendAv_NodeNums.@c: return _node_c;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LIBGR.IEdge edge__edge3 { get { return (GRGEN_LIBGR.IEdge)_edge__edge3; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public enum ComplexMax_alt_0_ExtendAv_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 4;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge0: return _edge__edge0;
				case (int)ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge1: return _edge__edge1;
				case (int)ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge2: return _edge__edge2;
				case (int)ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge3: return _edge__edge3;
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ComplexMax.instance.ComplexMax_alt_0_ExtendAv; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ComplexMax_alt_0_ExtendAv_neg_0 : GRGEN_LGSP.ListElement<Match_ComplexMax_alt_0_ExtendAv_neg_0>, IMatch_ComplexMax_alt_0_ExtendAv_neg_0
		{
			public IC node_c { get { return (IC)_node_c; } }
			public IC node__node0 { get { return (IC)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum ComplexMax_alt_0_ExtendAv_neg_0_NodeNums { @c, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendAv_neg_0_NodeNums.@c: return _node_c;
				case (int)ComplexMax_alt_0_ExtendAv_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ComplexMax_alt_0_ExtendAv_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendAv_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ComplexMax.instance.ComplexMax_alt_0_ExtendAv_neg_0; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ComplexMax_alt_0_ExtendAv2 : GRGEN_LGSP.ListElement<Match_ComplexMax_alt_0_ExtendAv2>, IMatch_ComplexMax_alt_0_ExtendAv2
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public IB node_b { get { return (IB)_node_b; } }
			public IC node__node0 { get { return (IC)_node__node0; } }
			public IC node__node1 { get { return (IC)_node__node1; } }
			public IC node__node2 { get { return (IC)_node__node2; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public GRGEN_LGSP.LGSPNode _node__node2;
			public enum ComplexMax_alt_0_ExtendAv2_NodeNums { @a, @b2, @b, @_node0, @_node1, @_node2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 6;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendAv2_NodeNums.@a: return _node_a;
				case (int)ComplexMax_alt_0_ExtendAv2_NodeNums.@b2: return _node_b2;
				case (int)ComplexMax_alt_0_ExtendAv2_NodeNums.@b: return _node_b;
				case (int)ComplexMax_alt_0_ExtendAv2_NodeNums.@_node0: return _node__node0;
				case (int)ComplexMax_alt_0_ExtendAv2_NodeNums.@_node1: return _node__node1;
				case (int)ComplexMax_alt_0_ExtendAv2_NodeNums.@_node2: return _node__node2;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LIBGR.IEdge edge__edge3 { get { return (GRGEN_LIBGR.IEdge)_edge__edge3; } }
			public GRGEN_LIBGR.IEdge edge__edge4 { get { return (GRGEN_LIBGR.IEdge)_edge__edge4; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public GRGEN_LGSP.LGSPEdge _edge__edge4;
			public enum ComplexMax_alt_0_ExtendAv2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, @_edge4, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 5;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge0: return _edge__edge0;
				case (int)ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge1: return _edge__edge1;
				case (int)ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge2: return _edge__edge2;
				case (int)ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge3: return _edge__edge3;
				case (int)ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge4: return _edge__edge4;
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv2_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv2_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv2_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendAv2_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ComplexMax.instance.ComplexMax_alt_0_ExtendAv2; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ComplexMax_alt_0_ExtendNA2 : GRGEN_LGSP.ListElement<Match_ComplexMax_alt_0_ExtendNA2>, IMatch_ComplexMax_alt_0_ExtendNA2
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IC node__node0 { get { return (IC)_node__node0; } }
			public IC node__node1 { get { return (IC)_node__node1; } }
			public IB node_b { get { return (IB)_node_b; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public enum ComplexMax_alt_0_ExtendNA2_NodeNums { @a, @_node0, @_node1, @b, @b2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 5;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendNA2_NodeNums.@a: return _node_a;
				case (int)ComplexMax_alt_0_ExtendNA2_NodeNums.@_node0: return _node__node0;
				case (int)ComplexMax_alt_0_ExtendNA2_NodeNums.@_node1: return _node__node1;
				case (int)ComplexMax_alt_0_ExtendNA2_NodeNums.@b: return _node_b;
				case (int)ComplexMax_alt_0_ExtendNA2_NodeNums.@b2: return _node_b2;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LIBGR.IEdge edge__edge3 { get { return (GRGEN_LIBGR.IEdge)_edge__edge3; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public enum ComplexMax_alt_0_ExtendNA2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 4;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge0: return _edge__edge0;
				case (int)ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge1: return _edge__edge1;
				case (int)ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge2: return _edge__edge2;
				case (int)ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge3: return _edge__edge3;
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendNA2_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendNA2_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendNA2_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ComplexMax_alt_0_ExtendNA2_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ComplexMax.instance.ComplexMax_alt_0_ExtendNA2; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createABA : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createABA instance = null;
		public static Rule_createABA Instance { get { if (instance==null) { instance = new Rule_createABA(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum createABA_NodeNums { };
		public enum createABA_EdgeNums { };
		public enum createABA_VariableNums { };
		public enum createABA_SubNums { };
		public enum createABA_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createABA;


#if INITIAL_WARMUP
		public Rule_createABA()
#else
		private Rule_createABA()
#endif
		{
			name = "createABA";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] createABA_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createABA_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createABA = new GRGEN_LGSP.PatternGraph(
				"createABA",
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
				createABA_isNodeHomomorphicGlobal,
				createABA_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createABA;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createABA curMatch = (Match_createABA)_curMatch;
			graph.SettingAddedNodeNames( createABA_addedNodeNames );
			@A node_a = @A.CreateNode(graph);
			@B node_b = @B.CreateNode(graph);
			graph.SettingAddedEdgeNames( createABA_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_a, node_b);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_b, node_a);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_a, node_b);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_b, node_a);
			return EmptyReturnElements;
		}
		private static String[] createABA_addedNodeNames = new String[] { "a", "b" };
		private static String[] createABA_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createABA curMatch = (Match_createABA)_curMatch;
			graph.SettingAddedNodeNames( createABA_addedNodeNames );
			@A node_a = @A.CreateNode(graph);
			@B node_b = @B.CreateNode(graph);
			graph.SettingAddedEdgeNames( createABA_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_a, node_b);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_b, node_a);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_a, node_b);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_b, node_a);
			return EmptyReturnElements;
		}

		static Rule_createABA() {
		}

		public interface IMatch_createABA : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_createABA : GRGEN_LGSP.ListElement<Match_createABA>, IMatch_createABA
		{
			public enum createABA_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createABA_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createABA_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createABA_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createABA_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createABA_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createABA.instance.pat_createABA; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_homm : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_homm instance = null;
		public static Rule_homm Instance { get { if (instance==null) { instance = new Rule_homm(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] homm_node_a_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] homm_node_b_AllowedTypes = null;
		public static bool[] homm_node_a_IsAllowedType = null;
		public static bool[] homm_node_b_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] homm_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] homm_edge__edge1_AllowedTypes = null;
		public static bool[] homm_edge__edge0_IsAllowedType = null;
		public static bool[] homm_edge__edge1_IsAllowedType = null;
		public enum homm_NodeNums { @a, @b, };
		public enum homm_EdgeNums { @_edge0, @_edge1, };
		public enum homm_VariableNums { };
		public enum homm_SubNums { };
		public enum homm_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_homm;

		public enum homm_alt_0_CaseNums { @case1, @case2, };
		public static GRGEN_LIBGR.NodeType[] homm_alt_0_case1_node_b2_AllowedTypes = null;
		public static bool[] homm_alt_0_case1_node_b2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] homm_alt_0_case1_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] homm_alt_0_case1_edge__edge1_AllowedTypes = null;
		public static bool[] homm_alt_0_case1_edge__edge0_IsAllowedType = null;
		public static bool[] homm_alt_0_case1_edge__edge1_IsAllowedType = null;
		public enum homm_alt_0_case1_NodeNums { @a, @b2, @b, };
		public enum homm_alt_0_case1_EdgeNums { @_edge0, @_edge1, };
		public enum homm_alt_0_case1_VariableNums { };
		public enum homm_alt_0_case1_SubNums { };
		public enum homm_alt_0_case1_AltNums { };


		GRGEN_LGSP.PatternGraph homm_alt_0_case1;

		public static GRGEN_LIBGR.NodeType[] homm_alt_0_case2_node_b2_AllowedTypes = null;
		public static bool[] homm_alt_0_case2_node_b2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] homm_alt_0_case2_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] homm_alt_0_case2_edge__edge1_AllowedTypes = null;
		public static bool[] homm_alt_0_case2_edge__edge0_IsAllowedType = null;
		public static bool[] homm_alt_0_case2_edge__edge1_IsAllowedType = null;
		public enum homm_alt_0_case2_NodeNums { @a, @b2, };
		public enum homm_alt_0_case2_EdgeNums { @_edge0, @_edge1, };
		public enum homm_alt_0_case2_VariableNums { };
		public enum homm_alt_0_case2_SubNums { };
		public enum homm_alt_0_case2_AltNums { };


		GRGEN_LGSP.PatternGraph homm_alt_0_case2;


#if INITIAL_WARMUP
		public Rule_homm()
#else
		private Rule_homm()
#endif
		{
			name = "homm";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] homm_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] homm_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode homm_node_a = new GRGEN_LGSP.PatternNode((int) NodeTypes.@A, "IA", "homm_node_a", "a", homm_node_a_AllowedTypes, homm_node_a_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode homm_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "homm_node_b", "b", homm_node_b_AllowedTypes, homm_node_b_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge homm_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "homm_edge__edge0", "_edge0", homm_edge__edge0_AllowedTypes, homm_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge homm_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "homm_edge__edge1", "_edge1", homm_edge__edge1_AllowedTypes, homm_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] homm_alt_0_case1_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, true, },
				{ false, true, false, },
			};
			bool[,] homm_alt_0_case1_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode homm_alt_0_case1_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "homm_alt_0_case1_node_b2", "b2", homm_alt_0_case1_node_b2_AllowedTypes, homm_alt_0_case1_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge homm_alt_0_case1_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "homm_alt_0_case1_edge__edge0", "_edge0", homm_alt_0_case1_edge__edge0_AllowedTypes, homm_alt_0_case1_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge homm_alt_0_case1_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "homm_alt_0_case1_edge__edge1", "_edge1", homm_alt_0_case1_edge__edge1_AllowedTypes, homm_alt_0_case1_edge__edge1_IsAllowedType, 5.5F, -1);
			homm_alt_0_case1 = new GRGEN_LGSP.PatternGraph(
				"case1",
				"homm_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { homm_node_a, homm_alt_0_case1_node_b2, homm_node_b }, 
				new GRGEN_LGSP.PatternEdge[] { homm_alt_0_case1_edge__edge0, homm_alt_0_case1_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				homm_alt_0_case1_isNodeHomomorphicGlobal,
				homm_alt_0_case1_isEdgeHomomorphicGlobal
			);
			homm_alt_0_case1.edgeToSourceNode.Add(homm_alt_0_case1_edge__edge0, homm_node_a);
			homm_alt_0_case1.edgeToTargetNode.Add(homm_alt_0_case1_edge__edge0, homm_alt_0_case1_node_b2);
			homm_alt_0_case1.edgeToSourceNode.Add(homm_alt_0_case1_edge__edge1, homm_alt_0_case1_node_b2);
			homm_alt_0_case1.edgeToTargetNode.Add(homm_alt_0_case1_edge__edge1, homm_node_a);

			bool[,] homm_alt_0_case2_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] homm_alt_0_case2_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode homm_alt_0_case2_node_b2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@B, "IB", "homm_alt_0_case2_node_b2", "b2", homm_alt_0_case2_node_b2_AllowedTypes, homm_alt_0_case2_node_b2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge homm_alt_0_case2_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "homm_alt_0_case2_edge__edge0", "_edge0", homm_alt_0_case2_edge__edge0_AllowedTypes, homm_alt_0_case2_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge homm_alt_0_case2_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "homm_alt_0_case2_edge__edge1", "_edge1", homm_alt_0_case2_edge__edge1_AllowedTypes, homm_alt_0_case2_edge__edge1_IsAllowedType, 5.5F, -1);
			homm_alt_0_case2 = new GRGEN_LGSP.PatternGraph(
				"case2",
				"homm_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { homm_node_a, homm_alt_0_case2_node_b2 }, 
				new GRGEN_LGSP.PatternEdge[] { homm_alt_0_case2_edge__edge0, homm_alt_0_case2_edge__edge1 }, 
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
				homm_alt_0_case2_isNodeHomomorphicGlobal,
				homm_alt_0_case2_isEdgeHomomorphicGlobal
			);
			homm_alt_0_case2.edgeToSourceNode.Add(homm_alt_0_case2_edge__edge0, homm_node_a);
			homm_alt_0_case2.edgeToTargetNode.Add(homm_alt_0_case2_edge__edge0, homm_alt_0_case2_node_b2);
			homm_alt_0_case2.edgeToSourceNode.Add(homm_alt_0_case2_edge__edge1, homm_alt_0_case2_node_b2);
			homm_alt_0_case2.edgeToTargetNode.Add(homm_alt_0_case2_edge__edge1, homm_node_a);

			GRGEN_LGSP.Alternative homm_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "homm_", new GRGEN_LGSP.PatternGraph[] { homm_alt_0_case1, homm_alt_0_case2 } );

			pat_homm = new GRGEN_LGSP.PatternGraph(
				"homm",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { homm_node_a, homm_node_b }, 
				new GRGEN_LGSP.PatternEdge[] { homm_edge__edge0, homm_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { homm_alt_0,  }, 
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
				homm_isNodeHomomorphicGlobal,
				homm_isEdgeHomomorphicGlobal
			);
			pat_homm.edgeToSourceNode.Add(homm_edge__edge0, homm_node_a);
			pat_homm.edgeToTargetNode.Add(homm_edge__edge0, homm_node_b);
			pat_homm.edgeToSourceNode.Add(homm_edge__edge1, homm_node_b);
			pat_homm.edgeToTargetNode.Add(homm_edge__edge1, homm_node_a);
			homm_alt_0_case1.embeddingGraph = pat_homm;
			homm_alt_0_case2.embeddingGraph = pat_homm;

			homm_node_a.PointOfDefinition = pat_homm;
			homm_node_b.PointOfDefinition = pat_homm;
			homm_edge__edge0.PointOfDefinition = pat_homm;
			homm_edge__edge1.PointOfDefinition = pat_homm;
			homm_alt_0_case1_node_b2.PointOfDefinition = homm_alt_0_case1;
			homm_alt_0_case1_edge__edge0.PointOfDefinition = homm_alt_0_case1;
			homm_alt_0_case1_edge__edge1.PointOfDefinition = homm_alt_0_case1;
			homm_alt_0_case2_node_b2.PointOfDefinition = homm_alt_0_case2;
			homm_alt_0_case2_edge__edge0.PointOfDefinition = homm_alt_0_case2;
			homm_alt_0_case2_edge__edge1.PointOfDefinition = homm_alt_0_case2;

			patternGraph = pat_homm;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_homm curMatch = (Match_homm)_curMatch;
			IMatch_homm_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_homm curMatch = (Match_homm)_curMatch;
			IMatch_homm_alt_0 alternative_alt_0 = curMatch._alt_0;
			return EmptyReturnElements;
		}

		public void homm_alt_0_case1_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_homm_alt_0_case1 curMatch = (Match_homm_alt_0_case1)_curMatch;
		}

		public void homm_alt_0_case1_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_homm_alt_0_case1 curMatch = (Match_homm_alt_0_case1)_curMatch;
		}

		public void homm_alt_0_case2_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_homm_alt_0_case2 curMatch = (Match_homm_alt_0_case2)_curMatch;
		}

		public void homm_alt_0_case2_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_homm_alt_0_case2 curMatch = (Match_homm_alt_0_case2)_curMatch;
		}

		static Rule_homm() {
		}

		public interface IMatch_homm : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IA node_a { get; }
			IB node_b { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_homm_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_homm_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_homm_alt_0_case1 : IMatch_homm_alt_0
		{
			//Nodes
			IA node_a { get; }
			IB node_b2 { get; }
			IB node_b { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_homm_alt_0_case2 : IMatch_homm_alt_0
		{
			//Nodes
			IA node_a { get; }
			IB node_b2 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_homm : GRGEN_LGSP.ListElement<Match_homm>, IMatch_homm
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b { get { return (IB)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum homm_NodeNums { @a, @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)homm_NodeNums.@a: return _node_a;
				case (int)homm_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum homm_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)homm_EdgeNums.@_edge0: return _edge__edge0;
				case (int)homm_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum homm_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum homm_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_homm_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_homm_alt_0 _alt_0;
			public enum homm_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)homm_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum homm_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_homm.instance.pat_homm; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_homm_alt_0_case1 : GRGEN_LGSP.ListElement<Match_homm_alt_0_case1>, IMatch_homm_alt_0_case1
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public IB node_b { get { return (IB)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum homm_alt_0_case1_NodeNums { @a, @b2, @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)homm_alt_0_case1_NodeNums.@a: return _node_a;
				case (int)homm_alt_0_case1_NodeNums.@b2: return _node_b2;
				case (int)homm_alt_0_case1_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum homm_alt_0_case1_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)homm_alt_0_case1_EdgeNums.@_edge0: return _edge__edge0;
				case (int)homm_alt_0_case1_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum homm_alt_0_case1_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum homm_alt_0_case1_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum homm_alt_0_case1_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum homm_alt_0_case1_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_homm.instance.homm_alt_0_case1; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_homm_alt_0_case2 : GRGEN_LGSP.ListElement<Match_homm_alt_0_case2>, IMatch_homm_alt_0_case2
		{
			public IA node_a { get { return (IA)_node_a; } }
			public IB node_b2 { get { return (IB)_node_b2; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_b2;
			public enum homm_alt_0_case2_NodeNums { @a, @b2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)homm_alt_0_case2_NodeNums.@a: return _node_a;
				case (int)homm_alt_0_case2_NodeNums.@b2: return _node_b2;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum homm_alt_0_case2_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)homm_alt_0_case2_EdgeNums.@_edge0: return _edge__edge0;
				case (int)homm_alt_0_case2_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum homm_alt_0_case2_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum homm_alt_0_case2_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum homm_alt_0_case2_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum homm_alt_0_case2_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_homm.instance.homm_alt_0_case2; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_XtoAorB : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_XtoAorB instance = null;
		public static Rule_XtoAorB Instance { get { if (instance==null) { instance = new Rule_XtoAorB(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] XtoAorB_node_x_AllowedTypes = null;
		public static bool[] XtoAorB_node_x_IsAllowedType = null;
		public enum XtoAorB_NodeNums { @x, };
		public enum XtoAorB_EdgeNums { };
		public enum XtoAorB_VariableNums { };
		public enum XtoAorB_SubNums { @_subpattern0, };
		public enum XtoAorB_AltNums { };


		GRGEN_LGSP.PatternGraph pat_XtoAorB;


#if INITIAL_WARMUP
		public Rule_XtoAorB()
#else
		private Rule_XtoAorB()
#endif
		{
			name = "XtoAorB";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] XtoAorB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] XtoAorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode XtoAorB_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "XtoAorB_node_x", "x", XtoAorB_node_x_AllowedTypes, XtoAorB_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding XtoAorB__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_toAorB.Instance, new GRGEN_LGSP.PatternElement[] { XtoAorB_node_x });
			pat_XtoAorB = new GRGEN_LGSP.PatternGraph(
				"XtoAorB",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { XtoAorB_node_x }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { XtoAorB__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				XtoAorB_isNodeHomomorphicGlobal,
				XtoAorB_isEdgeHomomorphicGlobal
			);

			XtoAorB_node_x.PointOfDefinition = pat_XtoAorB;
			XtoAorB__subpattern0.PointOfDefinition = pat_XtoAorB;

			patternGraph = pat_XtoAorB;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_XtoAorB curMatch = (Match_XtoAorB)_curMatch;
			Pattern_toAorB.Match_toAorB subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_XtoAorB curMatch = (Match_XtoAorB)_curMatch;
			Pattern_toAorB.Match_toAorB subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		static Rule_XtoAorB() {
		}

		public interface IMatch_XtoAorB : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_toAorB.Match_toAorB @_subpattern0 { get; }
			//Alternatives
			//Independents
		}

		public class Match_XtoAorB : GRGEN_LGSP.ListElement<Match_XtoAorB>, IMatch_XtoAorB
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public enum XtoAorB_NodeNums { @x, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)XtoAorB_NodeNums.@x: return _node_x;
				default: return null;
				}
			}
			
			public enum XtoAorB_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum XtoAorB_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_toAorB.Match_toAorB @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_toAorB.Match_toAorB @__subpattern0;
			public enum XtoAorB_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)XtoAorB_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum XtoAorB_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum XtoAorB_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_XtoAorB.instance.pat_XtoAorB; } }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}


    public class PatternAction_toAorB : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_toAorB(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_toAorB.Instance.patternGraph;
        }

        public static PatternAction_toAorB getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_toAorB newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_toAorB(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_toAorB oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_toAorB freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_toAorB next = null;

        public GRGEN_LGSP.LGSPNode toAorB_node_x;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset toAorB_node_x 
            GRGEN_LGSP.LGSPNode candidate_toAorB_node_x = toAorB_node_x;
            // Extend Outgoing toAorB_edge_y from toAorB_node_x 
            GRGEN_LGSP.LGSPEdge head_candidate_toAorB_edge_y = candidate_toAorB_node_x.outhead;
            if(head_candidate_toAorB_edge_y != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_toAorB_edge_y = head_candidate_toAorB_edge_y;
                do
                {
                    if(candidate_toAorB_edge_y.type.TypeID!=1) {
                        continue;
                    }
                    if((candidate_toAorB_edge_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Push alternative matching task for toAorB_alt_0
                    AlternativeAction_toAorB_alt_0 taskFor_alt_0 = AlternativeAction_toAorB_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_toAorB.toAorB_AltNums.@alt_0].alternativeCases);
                    taskFor_alt_0.toAorB_edge_y = candidate_toAorB_edge_y;
                    openTasks.Push(taskFor_alt_0);
                    uint prevGlobal__candidate_toAorB_edge_y;
                    prevGlobal__candidate_toAorB_edge_y = candidate_toAorB_edge_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_toAorB_edge_y.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for alt_0
                    openTasks.Pop();
                    AlternativeAction_toAorB_alt_0.releaseTask(taskFor_alt_0);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Pattern_toAorB.Match_toAorB match = new Pattern_toAorB.Match_toAorB();
                            match._node_x = candidate_toAorB_node_x;
                            match._edge_y = candidate_toAorB_edge_y;
                            match._alt_0 = (Pattern_toAorB.IMatch_toAorB_alt_0)currentFoundPartialMatch.Pop();
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
                            candidate_toAorB_edge_y.flags = candidate_toAorB_edge_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_edge_y;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_toAorB_edge_y.flags = candidate_toAorB_edge_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_edge_y;
                        continue;
                    }
                    candidate_toAorB_edge_y.flags = candidate_toAorB_edge_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_edge_y;
                }
                while( (candidate_toAorB_edge_y = candidate_toAorB_edge_y.outNext) != head_candidate_toAorB_edge_y );
            }
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_toAorB_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_toAorB_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_toAorB_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_toAorB_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_toAorB_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_toAorB_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_toAorB_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_toAorB_alt_0 next = null;

        public GRGEN_LGSP.LGSPEdge toAorB_edge_y;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case toAorB_alt_0_toA 
            do {
                patternGraph = patternGraphs[(int)Pattern_toAorB.toAorB_alt_0_CaseNums.@toA];
                // SubPreset toAorB_edge_y 
                GRGEN_LGSP.LGSPEdge candidate_toAorB_edge_y = toAorB_edge_y;
                // Implicit Target toAorB_alt_0_toA_node_a from toAorB_edge_y 
                GRGEN_LGSP.LGSPNode candidate_toAorB_alt_0_toA_node_a = candidate_toAorB_edge_y.target;
                if(candidate_toAorB_alt_0_toA_node_a.type.TypeID!=1) {
                    continue;
                }
                if((candidate_toAorB_alt_0_toA_node_a.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                {
                    continue;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_toAorB.Match_toAorB_alt_0_toA match = new Pattern_toAorB.Match_toAorB_alt_0_toA();
                    match._node_a = candidate_toAorB_alt_0_toA_node_a;
                    match._edge_y = candidate_toAorB_edge_y;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    continue;
                }
                uint prevGlobal__candidate_toAorB_alt_0_toA_node_a;
                prevGlobal__candidate_toAorB_alt_0_toA_node_a = candidate_toAorB_alt_0_toA_node_a.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_toAorB_alt_0_toA_node_a.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_toAorB.Match_toAorB_alt_0_toA match = new Pattern_toAorB.Match_toAorB_alt_0_toA();
                        match._node_a = candidate_toAorB_alt_0_toA_node_a;
                        match._edge_y = candidate_toAorB_edge_y;
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
                        candidate_toAorB_alt_0_toA_node_a.flags = candidate_toAorB_alt_0_toA_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toA_node_a;
                        openTasks.Push(this);
                        return;
                    }
                    candidate_toAorB_alt_0_toA_node_a.flags = candidate_toAorB_alt_0_toA_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toA_node_a;
                    continue;
                }
                candidate_toAorB_alt_0_toA_node_a.flags = candidate_toAorB_alt_0_toA_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toA_node_a;
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
            // Alternative case toAorB_alt_0_toB 
            do {
                patternGraph = patternGraphs[(int)Pattern_toAorB.toAorB_alt_0_CaseNums.@toB];
                // SubPreset toAorB_edge_y 
                GRGEN_LGSP.LGSPEdge candidate_toAorB_edge_y = toAorB_edge_y;
                // Implicit Target toAorB_alt_0_toB_node_b from toAorB_edge_y 
                GRGEN_LGSP.LGSPNode candidate_toAorB_alt_0_toB_node_b = candidate_toAorB_edge_y.target;
                if(candidate_toAorB_alt_0_toB_node_b.type.TypeID!=2) {
                    continue;
                }
                if((candidate_toAorB_alt_0_toB_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                {
                    continue;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_toAorB.Match_toAorB_alt_0_toB match = new Pattern_toAorB.Match_toAorB_alt_0_toB();
                    match._node_b = candidate_toAorB_alt_0_toB_node_b;
                    match._edge_y = candidate_toAorB_edge_y;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    continue;
                }
                uint prevGlobal__candidate_toAorB_alt_0_toB_node_b;
                prevGlobal__candidate_toAorB_alt_0_toB_node_b = candidate_toAorB_alt_0_toB_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_toAorB_alt_0_toB_node_b.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_toAorB.Match_toAorB_alt_0_toB match = new Pattern_toAorB.Match_toAorB_alt_0_toB();
                        match._node_b = candidate_toAorB_alt_0_toB_node_b;
                        match._edge_y = candidate_toAorB_edge_y;
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
                        candidate_toAorB_alt_0_toB_node_b.flags = candidate_toAorB_alt_0_toB_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toB_node_b;
                        openTasks.Push(this);
                        return;
                    }
                    candidate_toAorB_alt_0_toB_node_b.flags = candidate_toAorB_alt_0_toB_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toB_node_b;
                    continue;
                }
                candidate_toAorB_alt_0_toB_node_b.flags = candidate_toAorB_alt_0_toB_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toB_node_b;
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_createA : GRGEN_LGSP.LGSPAction
    {
        public Action_createA() {
            rulePattern = Rule_createA.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createA.Match_createA>(this);
        }

        public override string Name { get { return "createA"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createA.Match_createA> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createA instance = new Action_createA();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createA.Match_createA match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_createB : GRGEN_LGSP.LGSPAction
    {
        public Action_createB() {
            rulePattern = Rule_createB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createB.Match_createB>(this);
        }

        public override string Name { get { return "createB"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createB.Match_createB> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createB instance = new Action_createB();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createB.Match_createB match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_createC : GRGEN_LGSP.LGSPAction
    {
        public Action_createC() {
            rulePattern = Rule_createC.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createC.Match_createC>(this);
        }

        public override string Name { get { return "createC"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createC.Match_createC> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createC instance = new Action_createC();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createC.Match_createC match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_createAtoB : GRGEN_LGSP.LGSPAction
    {
        public Action_createAtoB() {
            rulePattern = Rule_createAtoB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createAtoB.Match_createAtoB>(this);
        }

        public override string Name { get { return "createAtoB"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createAtoB.Match_createAtoB> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createAtoB instance = new Action_createAtoB();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createAtoB.Match_createAtoB match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_leer : GRGEN_LGSP.LGSPAction
    {
        public Action_leer() {
            rulePattern = Rule_leer.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_leer.Match_leer>(this);
        }

        public override string Name { get { return "leer"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_leer.Match_leer> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_leer instance = new Action_leer();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for leer_alt_0
            AlternativeAction_leer_alt_0 taskFor_alt_0 = AlternativeAction_leer_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_leer.leer_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_leer_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_leer.Match_leer match = matches.GetNextUnfilledPosition();
                    match._alt_0 = (Rule_leer.IMatch_leer_alt_0)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_leer_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_leer_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_leer_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_leer_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_leer_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_leer_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_leer_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_leer_alt_0 next = null;

        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case leer_alt_0_altleer 
            do {
                patternGraph = patternGraphs[(int)Rule_leer.leer_alt_0_CaseNums.@altleer];
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Rule_leer.Match_leer_alt_0_altleer match = new Rule_leer.Match_leer_alt_0_altleer();
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
                        Rule_leer.Match_leer_alt_0_altleer match = new Rule_leer.Match_leer_alt_0_altleer();
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
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AorB : GRGEN_LGSP.LGSPAction
    {
        public Action_AorB() {
            rulePattern = Rule_AorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_AorB.Match_AorB>(this);
        }

        public override string Name { get { return "AorB"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_AorB.Match_AorB> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_AorB instance = new Action_AorB();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AorB_alt_0
            AlternativeAction_AorB_alt_0 taskFor_alt_0 = AlternativeAction_AorB_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_AorB.AorB_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_AorB_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_AorB.Match_AorB match = matches.GetNextUnfilledPosition();
                    match._alt_0 = (Rule_AorB.IMatch_AorB_alt_0)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_AorB_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_AorB_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_AorB_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_AorB_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_AorB_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_AorB_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_AorB_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_AorB_alt_0 next = null;

        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AorB_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AorB.AorB_alt_0_CaseNums.@A];
                // Lookup AorB_alt_0_A_node__node0 
                int type_id_candidate_AorB_alt_0_A_node__node0 = 1;
                for(GRGEN_LGSP.LGSPNode head_candidate_AorB_alt_0_A_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorB_alt_0_A_node__node0], candidate_AorB_alt_0_A_node__node0 = head_candidate_AorB_alt_0_A_node__node0.typeNext; candidate_AorB_alt_0_A_node__node0 != head_candidate_AorB_alt_0_A_node__node0; candidate_AorB_alt_0_A_node__node0 = candidate_AorB_alt_0_A_node__node0.typeNext)
                {
                    if((candidate_AorB_alt_0_A_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        Rule_AorB.Match_AorB_alt_0_A match = new Rule_AorB.Match_AorB_alt_0_A();
                        match._node__node0 = candidate_AorB_alt_0_A_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    uint prevGlobal__candidate_AorB_alt_0_A_node__node0;
                    prevGlobal__candidate_AorB_alt_0_A_node__node0 = candidate_AorB_alt_0_A_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorB_alt_0_A_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_AorB.Match_AorB_alt_0_A match = new Rule_AorB.Match_AorB_alt_0_A();
                            match._node__node0 = candidate_AorB_alt_0_A_node__node0;
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
                            candidate_AorB_alt_0_A_node__node0.flags = candidate_AorB_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_A_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorB_alt_0_A_node__node0.flags = candidate_AorB_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_A_node__node0;
                        continue;
                    }
                    candidate_AorB_alt_0_A_node__node0.flags = candidate_AorB_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_A_node__node0;
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
            // Alternative case AorB_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AorB.AorB_alt_0_CaseNums.@B];
                // Lookup AorB_alt_0_B_node__node0 
                int type_id_candidate_AorB_alt_0_B_node__node0 = 2;
                for(GRGEN_LGSP.LGSPNode head_candidate_AorB_alt_0_B_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorB_alt_0_B_node__node0], candidate_AorB_alt_0_B_node__node0 = head_candidate_AorB_alt_0_B_node__node0.typeNext; candidate_AorB_alt_0_B_node__node0 != head_candidate_AorB_alt_0_B_node__node0; candidate_AorB_alt_0_B_node__node0 = candidate_AorB_alt_0_B_node__node0.typeNext)
                {
                    if((candidate_AorB_alt_0_B_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        Rule_AorB.Match_AorB_alt_0_B match = new Rule_AorB.Match_AorB_alt_0_B();
                        match._node__node0 = candidate_AorB_alt_0_B_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    uint prevGlobal__candidate_AorB_alt_0_B_node__node0;
                    prevGlobal__candidate_AorB_alt_0_B_node__node0 = candidate_AorB_alt_0_B_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorB_alt_0_B_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_AorB.Match_AorB_alt_0_B match = new Rule_AorB.Match_AorB_alt_0_B();
                            match._node__node0 = candidate_AorB_alt_0_B_node__node0;
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
                            candidate_AorB_alt_0_B_node__node0.flags = candidate_AorB_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_B_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorB_alt_0_B_node__node0.flags = candidate_AorB_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_B_node__node0;
                        continue;
                    }
                    candidate_AorB_alt_0_B_node__node0.flags = candidate_AorB_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_B_node__node0;
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AandnotCorB : GRGEN_LGSP.LGSPAction
    {
        public Action_AandnotCorB() {
            rulePattern = Rule_AandnotCorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_AandnotCorB.Match_AandnotCorB>(this);
        }

        public override string Name { get { return "AandnotCorB"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_AandnotCorB.Match_AandnotCorB> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_AandnotCorB instance = new Action_AandnotCorB();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AandnotCorB_alt_0
            AlternativeAction_AandnotCorB_alt_0 taskFor_alt_0 = AlternativeAction_AandnotCorB_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_AandnotCorB.AandnotCorB_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_AandnotCorB_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_AandnotCorB.Match_AandnotCorB match = matches.GetNextUnfilledPosition();
                    match._alt_0 = (Rule_AandnotCorB.IMatch_AandnotCorB_alt_0)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_AandnotCorB_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_AandnotCorB_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_AandnotCorB_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_AandnotCorB_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_AandnotCorB_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_AandnotCorB_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_AandnotCorB_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_AandnotCorB_alt_0 next = null;

        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AandnotCorB_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AandnotCorB.AandnotCorB_alt_0_CaseNums.@A];
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    // Lookup AandnotCorB_alt_0_A_neg_0_node__node0 
                    int type_id_candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AandnotCorB_alt_0_A_neg_0_node__node0], candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = head_candidate_AandnotCorB_alt_0_A_neg_0_node__node0.typeNext; candidate_AandnotCorB_alt_0_A_neg_0_node__node0 != head_candidate_AandnotCorB_alt_0_A_neg_0_node__node0; candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = candidate_AandnotCorB_alt_0_A_neg_0_node__node0.typeNext)
                    {
                        if((candidate_AandnotCorB_alt_0_A_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // negative pattern found
                        if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                        }
                        --negLevel;
                        goto label0;
                    }
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                    }
                    --negLevel;
                }
                // Lookup AandnotCorB_alt_0_A_node__node0 
                int type_id_candidate_AandnotCorB_alt_0_A_node__node0 = 1;
                for(GRGEN_LGSP.LGSPNode head_candidate_AandnotCorB_alt_0_A_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AandnotCorB_alt_0_A_node__node0], candidate_AandnotCorB_alt_0_A_node__node0 = head_candidate_AandnotCorB_alt_0_A_node__node0.typeNext; candidate_AandnotCorB_alt_0_A_node__node0 != head_candidate_AandnotCorB_alt_0_A_node__node0; candidate_AandnotCorB_alt_0_A_node__node0 = candidate_AandnotCorB_alt_0_A_node__node0.typeNext)
                {
                    if((candidate_AandnotCorB_alt_0_A_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        Rule_AandnotCorB.Match_AandnotCorB_alt_0_A match = new Rule_AandnotCorB.Match_AandnotCorB_alt_0_A();
                        match._node__node0 = candidate_AandnotCorB_alt_0_A_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    uint prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0;
                    prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0 = candidate_AandnotCorB_alt_0_A_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AandnotCorB_alt_0_A_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_AandnotCorB.Match_AandnotCorB_alt_0_A match = new Rule_AandnotCorB.Match_AandnotCorB_alt_0_A();
                            match._node__node0 = candidate_AandnotCorB_alt_0_A_node__node0;
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
                            candidate_AandnotCorB_alt_0_A_node__node0.flags = candidate_AandnotCorB_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AandnotCorB_alt_0_A_node__node0.flags = candidate_AandnotCorB_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0;
                        continue;
                    }
                    candidate_AandnotCorB_alt_0_A_node__node0.flags = candidate_AandnotCorB_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0;
                }
label0: ;
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
            // Alternative case AandnotCorB_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AandnotCorB.AandnotCorB_alt_0_CaseNums.@B];
                // Lookup AandnotCorB_alt_0_B_node__node0 
                int type_id_candidate_AandnotCorB_alt_0_B_node__node0 = 2;
                for(GRGEN_LGSP.LGSPNode head_candidate_AandnotCorB_alt_0_B_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AandnotCorB_alt_0_B_node__node0], candidate_AandnotCorB_alt_0_B_node__node0 = head_candidate_AandnotCorB_alt_0_B_node__node0.typeNext; candidate_AandnotCorB_alt_0_B_node__node0 != head_candidate_AandnotCorB_alt_0_B_node__node0; candidate_AandnotCorB_alt_0_B_node__node0 = candidate_AandnotCorB_alt_0_B_node__node0.typeNext)
                {
                    if((candidate_AandnotCorB_alt_0_B_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        Rule_AandnotCorB.Match_AandnotCorB_alt_0_B match = new Rule_AandnotCorB.Match_AandnotCorB_alt_0_B();
                        match._node__node0 = candidate_AandnotCorB_alt_0_B_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    uint prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0;
                    prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0 = candidate_AandnotCorB_alt_0_B_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AandnotCorB_alt_0_B_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_AandnotCorB.Match_AandnotCorB_alt_0_B match = new Rule_AandnotCorB.Match_AandnotCorB_alt_0_B();
                            match._node__node0 = candidate_AandnotCorB_alt_0_B_node__node0;
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
                            candidate_AandnotCorB_alt_0_B_node__node0.flags = candidate_AandnotCorB_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AandnotCorB_alt_0_B_node__node0.flags = candidate_AandnotCorB_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0;
                        continue;
                    }
                    candidate_AandnotCorB_alt_0_B_node__node0.flags = candidate_AandnotCorB_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0;
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AorBorC : GRGEN_LGSP.LGSPAction
    {
        public Action_AorBorC() {
            rulePattern = Rule_AorBorC.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_AorBorC.Match_AorBorC>(this);
        }

        public override string Name { get { return "AorBorC"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_AorBorC.Match_AorBorC> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_AorBorC instance = new Action_AorBorC();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AorBorC_alt_0
            AlternativeAction_AorBorC_alt_0 taskFor_alt_0 = AlternativeAction_AorBorC_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_AorBorC.AorBorC_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_AorBorC_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_AorBorC.Match_AorBorC match = matches.GetNextUnfilledPosition();
                    match._alt_0 = (Rule_AorBorC.IMatch_AorBorC_alt_0)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_AorBorC_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_AorBorC_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_AorBorC_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_AorBorC_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_AorBorC_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_AorBorC_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_AorBorC_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_AorBorC_alt_0 next = null;

        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AorBorC_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@A];
                // Lookup AorBorC_alt_0_A_node__node0 
                int type_id_candidate_AorBorC_alt_0_A_node__node0 = 1;
                for(GRGEN_LGSP.LGSPNode head_candidate_AorBorC_alt_0_A_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorBorC_alt_0_A_node__node0], candidate_AorBorC_alt_0_A_node__node0 = head_candidate_AorBorC_alt_0_A_node__node0.typeNext; candidate_AorBorC_alt_0_A_node__node0 != head_candidate_AorBorC_alt_0_A_node__node0; candidate_AorBorC_alt_0_A_node__node0 = candidate_AorBorC_alt_0_A_node__node0.typeNext)
                {
                    if((candidate_AorBorC_alt_0_A_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        Rule_AorBorC.Match_AorBorC_alt_0_A match = new Rule_AorBorC.Match_AorBorC_alt_0_A();
                        match._node__node0 = candidate_AorBorC_alt_0_A_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    uint prevGlobal__candidate_AorBorC_alt_0_A_node__node0;
                    prevGlobal__candidate_AorBorC_alt_0_A_node__node0 = candidate_AorBorC_alt_0_A_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorBorC_alt_0_A_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_AorBorC.Match_AorBorC_alt_0_A match = new Rule_AorBorC.Match_AorBorC_alt_0_A();
                            match._node__node0 = candidate_AorBorC_alt_0_A_node__node0;
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
                            candidate_AorBorC_alt_0_A_node__node0.flags = candidate_AorBorC_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_A_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorBorC_alt_0_A_node__node0.flags = candidate_AorBorC_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_A_node__node0;
                        continue;
                    }
                    candidate_AorBorC_alt_0_A_node__node0.flags = candidate_AorBorC_alt_0_A_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_A_node__node0;
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
            // Alternative case AorBorC_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@B];
                // Lookup AorBorC_alt_0_B_node__node0 
                int type_id_candidate_AorBorC_alt_0_B_node__node0 = 2;
                for(GRGEN_LGSP.LGSPNode head_candidate_AorBorC_alt_0_B_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorBorC_alt_0_B_node__node0], candidate_AorBorC_alt_0_B_node__node0 = head_candidate_AorBorC_alt_0_B_node__node0.typeNext; candidate_AorBorC_alt_0_B_node__node0 != head_candidate_AorBorC_alt_0_B_node__node0; candidate_AorBorC_alt_0_B_node__node0 = candidate_AorBorC_alt_0_B_node__node0.typeNext)
                {
                    if((candidate_AorBorC_alt_0_B_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        Rule_AorBorC.Match_AorBorC_alt_0_B match = new Rule_AorBorC.Match_AorBorC_alt_0_B();
                        match._node__node0 = candidate_AorBorC_alt_0_B_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    uint prevGlobal__candidate_AorBorC_alt_0_B_node__node0;
                    prevGlobal__candidate_AorBorC_alt_0_B_node__node0 = candidate_AorBorC_alt_0_B_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorBorC_alt_0_B_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_AorBorC.Match_AorBorC_alt_0_B match = new Rule_AorBorC.Match_AorBorC_alt_0_B();
                            match._node__node0 = candidate_AorBorC_alt_0_B_node__node0;
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
                            candidate_AorBorC_alt_0_B_node__node0.flags = candidate_AorBorC_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_B_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorBorC_alt_0_B_node__node0.flags = candidate_AorBorC_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_B_node__node0;
                        continue;
                    }
                    candidate_AorBorC_alt_0_B_node__node0.flags = candidate_AorBorC_alt_0_B_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_B_node__node0;
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
            // Alternative case AorBorC_alt_0_C 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@C];
                // Lookup AorBorC_alt_0_C_node__node0 
                int type_id_candidate_AorBorC_alt_0_C_node__node0 = 3;
                for(GRGEN_LGSP.LGSPNode head_candidate_AorBorC_alt_0_C_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorBorC_alt_0_C_node__node0], candidate_AorBorC_alt_0_C_node__node0 = head_candidate_AorBorC_alt_0_C_node__node0.typeNext; candidate_AorBorC_alt_0_C_node__node0 != head_candidate_AorBorC_alt_0_C_node__node0; candidate_AorBorC_alt_0_C_node__node0 = candidate_AorBorC_alt_0_C_node__node0.typeNext)
                {
                    if((candidate_AorBorC_alt_0_C_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        Rule_AorBorC.Match_AorBorC_alt_0_C match = new Rule_AorBorC.Match_AorBorC_alt_0_C();
                        match._node__node0 = candidate_AorBorC_alt_0_C_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    uint prevGlobal__candidate_AorBorC_alt_0_C_node__node0;
                    prevGlobal__candidate_AorBorC_alt_0_C_node__node0 = candidate_AorBorC_alt_0_C_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorBorC_alt_0_C_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_AorBorC.Match_AorBorC_alt_0_C match = new Rule_AorBorC.Match_AorBorC_alt_0_C();
                            match._node__node0 = candidate_AorBorC_alt_0_C_node__node0;
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
                            candidate_AorBorC_alt_0_C_node__node0.flags = candidate_AorBorC_alt_0_C_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_C_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorBorC_alt_0_C_node__node0.flags = candidate_AorBorC_alt_0_C_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_C_node__node0;
                        continue;
                    }
                    candidate_AorBorC_alt_0_C_node__node0.flags = candidate_AorBorC_alt_0_C_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_C_node__node0;
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AtoAorB : GRGEN_LGSP.LGSPAction
    {
        public Action_AtoAorB() {
            rulePattern = Rule_AtoAorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_AtoAorB.Match_AtoAorB>(this);
        }

        public override string Name { get { return "AtoAorB"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_AtoAorB.Match_AtoAorB> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_AtoAorB instance = new Action_AtoAorB();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Lookup AtoAorB_node_a 
            int type_id_candidate_AtoAorB_node_a = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_AtoAorB_node_a = graph.nodesByTypeHeads[type_id_candidate_AtoAorB_node_a], candidate_AtoAorB_node_a = head_candidate_AtoAorB_node_a.typeNext; candidate_AtoAorB_node_a != head_candidate_AtoAorB_node_a; candidate_AtoAorB_node_a = candidate_AtoAorB_node_a.typeNext)
            {
                // Push alternative matching task for AtoAorB_alt_0
                AlternativeAction_AtoAorB_alt_0 taskFor_alt_0 = AlternativeAction_AtoAorB_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_AtoAorB.AtoAorB_AltNums.@alt_0].alternativeCases);
                taskFor_alt_0.AtoAorB_node_a = candidate_AtoAorB_node_a;
                openTasks.Push(taskFor_alt_0);
                uint prevGlobal__candidate_AtoAorB_node_a;
                prevGlobal__candidate_AtoAorB_node_a = candidate_AtoAorB_node_a.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_AtoAorB_node_a.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for alt_0
                openTasks.Pop();
                AlternativeAction_AtoAorB_alt_0.releaseTask(taskFor_alt_0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_AtoAorB.Match_AtoAorB match = matches.GetNextUnfilledPosition();
                        match._node_a = candidate_AtoAorB_node_a;
                        match._alt_0 = (Rule_AtoAorB.IMatch_AtoAorB_alt_0)currentFoundPartialMatch.Pop();
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_AtoAorB_node_a.flags = candidate_AtoAorB_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_node_a;
                        return matches;
                    }
                    candidate_AtoAorB_node_a.flags = candidate_AtoAorB_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_node_a;
                    continue;
                }
                candidate_AtoAorB_node_a.flags = candidate_AtoAorB_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_node_a;
            }
            return matches;
        }
    }

    public class AlternativeAction_AtoAorB_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_AtoAorB_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_AtoAorB_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_AtoAorB_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_AtoAorB_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_AtoAorB_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_AtoAorB_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_AtoAorB_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode AtoAorB_node_a;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AtoAorB_alt_0_toA 
            do {
                patternGraph = patternGraphs[(int)Rule_AtoAorB.AtoAorB_alt_0_CaseNums.@toA];
                // SubPreset AtoAorB_node_a 
                GRGEN_LGSP.LGSPNode candidate_AtoAorB_node_a = AtoAorB_node_a;
                // Extend Outgoing AtoAorB_alt_0_toA_edge__edge0 from AtoAorB_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_AtoAorB_alt_0_toA_edge__edge0 = candidate_AtoAorB_node_a.outhead;
                if(head_candidate_AtoAorB_alt_0_toA_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_AtoAorB_alt_0_toA_edge__edge0 = head_candidate_AtoAorB_alt_0_toA_edge__edge0;
                    do
                    {
                        if(candidate_AtoAorB_alt_0_toA_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toA_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target AtoAorB_alt_0_toA_node__node0 from AtoAorB_alt_0_toA_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_AtoAorB_alt_0_toA_node__node0 = candidate_AtoAorB_alt_0_toA_edge__edge0.target;
                        if(candidate_AtoAorB_alt_0_toA_node__node0.type.TypeID!=1) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_AtoAorB_alt_0_toA_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_AtoAorB_alt_0_toA_node__node0)))
                        {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toA_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Rule_AtoAorB.Match_AtoAorB_alt_0_toA match = new Rule_AtoAorB.Match_AtoAorB_alt_0_toA();
                            match._node_a = candidate_AtoAorB_node_a;
                            match._node__node0 = candidate_AtoAorB_alt_0_toA_node__node0;
                            match._edge__edge0 = candidate_AtoAorB_alt_0_toA_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0;
                        prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0 = candidate_AtoAorB_alt_0_toA_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toA_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                        prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0 = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toA_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Rule_AtoAorB.Match_AtoAorB_alt_0_toA match = new Rule_AtoAorB.Match_AtoAorB_alt_0_toA();
                                match._node_a = candidate_AtoAorB_node_a;
                                match._node__node0 = candidate_AtoAorB_alt_0_toA_node__node0;
                                match._edge__edge0 = candidate_AtoAorB_alt_0_toA_edge__edge0;
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
                                candidate_AtoAorB_alt_0_toA_edge__edge0.flags = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                                candidate_AtoAorB_alt_0_toA_node__node0.flags = candidate_AtoAorB_alt_0_toA_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_AtoAorB_alt_0_toA_edge__edge0.flags = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                            candidate_AtoAorB_alt_0_toA_node__node0.flags = candidate_AtoAorB_alt_0_toA_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0;
                            continue;
                        }
                        candidate_AtoAorB_alt_0_toA_node__node0.flags = candidate_AtoAorB_alt_0_toA_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0;
                        candidate_AtoAorB_alt_0_toA_edge__edge0.flags = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                    }
                    while( (candidate_AtoAorB_alt_0_toA_edge__edge0 = candidate_AtoAorB_alt_0_toA_edge__edge0.outNext) != head_candidate_AtoAorB_alt_0_toA_edge__edge0 );
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
            // Alternative case AtoAorB_alt_0_toB 
            do {
                patternGraph = patternGraphs[(int)Rule_AtoAorB.AtoAorB_alt_0_CaseNums.@toB];
                // SubPreset AtoAorB_node_a 
                GRGEN_LGSP.LGSPNode candidate_AtoAorB_node_a = AtoAorB_node_a;
                // Extend Outgoing AtoAorB_alt_0_toB_edge__edge0 from AtoAorB_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_AtoAorB_alt_0_toB_edge__edge0 = candidate_AtoAorB_node_a.outhead;
                if(head_candidate_AtoAorB_alt_0_toB_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_AtoAorB_alt_0_toB_edge__edge0 = head_candidate_AtoAorB_alt_0_toB_edge__edge0;
                    do
                    {
                        if(candidate_AtoAorB_alt_0_toB_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toB_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target AtoAorB_alt_0_toB_node__node0 from AtoAorB_alt_0_toB_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_AtoAorB_alt_0_toB_node__node0 = candidate_AtoAorB_alt_0_toB_edge__edge0.target;
                        if(candidate_AtoAorB_alt_0_toB_node__node0.type.TypeID!=2) {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toB_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Rule_AtoAorB.Match_AtoAorB_alt_0_toB match = new Rule_AtoAorB.Match_AtoAorB_alt_0_toB();
                            match._node_a = candidate_AtoAorB_node_a;
                            match._node__node0 = candidate_AtoAorB_alt_0_toB_node__node0;
                            match._edge__edge0 = candidate_AtoAorB_alt_0_toB_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0;
                        prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0 = candidate_AtoAorB_alt_0_toB_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toB_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                        prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0 = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toB_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Rule_AtoAorB.Match_AtoAorB_alt_0_toB match = new Rule_AtoAorB.Match_AtoAorB_alt_0_toB();
                                match._node_a = candidate_AtoAorB_node_a;
                                match._node__node0 = candidate_AtoAorB_alt_0_toB_node__node0;
                                match._edge__edge0 = candidate_AtoAorB_alt_0_toB_edge__edge0;
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
                                candidate_AtoAorB_alt_0_toB_edge__edge0.flags = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                                candidate_AtoAorB_alt_0_toB_node__node0.flags = candidate_AtoAorB_alt_0_toB_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_AtoAorB_alt_0_toB_edge__edge0.flags = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                            candidate_AtoAorB_alt_0_toB_node__node0.flags = candidate_AtoAorB_alt_0_toB_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0;
                            continue;
                        }
                        candidate_AtoAorB_alt_0_toB_node__node0.flags = candidate_AtoAorB_alt_0_toB_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0;
                        candidate_AtoAorB_alt_0_toB_edge__edge0.flags = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                    }
                    while( (candidate_AtoAorB_alt_0_toB_edge__edge0 = candidate_AtoAorB_alt_0_toB_edge__edge0.outNext) != head_candidate_AtoAorB_alt_0_toB_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_createComplex : GRGEN_LGSP.LGSPAction
    {
        public Action_createComplex() {
            rulePattern = Rule_createComplex.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createComplex.Match_createComplex>(this);
        }

        public override string Name { get { return "createComplex"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createComplex.Match_createComplex> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createComplex instance = new Action_createComplex();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createComplex.Match_createComplex match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_Complex : GRGEN_LGSP.LGSPAction
    {
        public Action_Complex() {
            rulePattern = Rule_Complex.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_Complex.Match_Complex>(this);
        }

        public override string Name { get { return "Complex"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_Complex.Match_Complex> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_Complex instance = new Action_Complex();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Lookup Complex_edge__edge0 
            int type_id_candidate_Complex_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_Complex_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_Complex_edge__edge0], candidate_Complex_edge__edge0 = head_candidate_Complex_edge__edge0.typeNext; candidate_Complex_edge__edge0 != head_candidate_Complex_edge__edge0; candidate_Complex_edge__edge0 = candidate_Complex_edge__edge0.typeNext)
            {
                uint prev__candidate_Complex_edge__edge0;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_Complex_edge__edge0 = candidate_Complex_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_Complex_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_Complex_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_edge__edge0) ? 1U : 0U;
                    if(prev__candidate_Complex_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_edge__edge0,candidate_Complex_edge__edge0);
                }
                // Implicit Source Complex_node_a from Complex_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_a = candidate_Complex_edge__edge0.source;
                if(candidate_Complex_node_a.type.TypeID!=1) {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_edge__edge0;
                    } else { 
                        if(prev__candidate_Complex_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_edge__edge0);
                        }
                    }
                    continue;
                }
                // Implicit Target Complex_node_b from Complex_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_b = candidate_Complex_edge__edge0.target;
                if(candidate_Complex_node_b.type.TypeID!=2) {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_edge__edge0;
                    } else { 
                        if(prev__candidate_Complex_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_edge__edge0);
                        }
                    }
                    continue;
                }
                // Extend Outgoing Complex_edge__edge1 from Complex_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Complex_edge__edge1 = candidate_Complex_node_b.outhead;
                if(head_candidate_Complex_edge__edge1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Complex_edge__edge1 = head_candidate_Complex_edge__edge1;
                    do
                    {
                        if(candidate_Complex_edge__edge1.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_Complex_edge__edge1.target != candidate_Complex_node_a) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_edge__edge1)))
                        {
                            continue;
                        }
                        // Push alternative matching task for Complex_alt_0
                        AlternativeAction_Complex_alt_0 taskFor_alt_0 = AlternativeAction_Complex_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_Complex.Complex_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.Complex_node_a = candidate_Complex_node_a;
                        taskFor_alt_0.Complex_node_b = candidate_Complex_node_b;
                        openTasks.Push(taskFor_alt_0);
                        uint prevGlobal__candidate_Complex_node_a;
                        prevGlobal__candidate_Complex_node_a = candidate_Complex_node_a.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_node_a.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Complex_node_b;
                        prevGlobal__candidate_Complex_node_b = candidate_Complex_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_node_b.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Complex_edge__edge0;
                        prevGlobal__candidate_Complex_edge__edge0 = candidate_Complex_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Complex_edge__edge1;
                        prevGlobal__candidate_Complex_edge__edge1 = candidate_Complex_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for alt_0
                        openTasks.Pop();
                        AlternativeAction_Complex_alt_0.releaseTask(taskFor_alt_0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Rule_Complex.Match_Complex match = matches.GetNextUnfilledPosition();
                                match._node_a = candidate_Complex_node_a;
                                match._node_b = candidate_Complex_node_b;
                                match._edge__edge0 = candidate_Complex_edge__edge0;
                                match._edge__edge1 = candidate_Complex_edge__edge1;
                                match._alt_0 = (Rule_Complex.IMatch_Complex_alt_0)currentFoundPartialMatch.Pop();
                                matches.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_Complex_edge__edge1.flags = candidate_Complex_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge1;
                                candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge0;
                                candidate_Complex_node_b.flags = candidate_Complex_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_b;
                                candidate_Complex_node_a.flags = candidate_Complex_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_a;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_edge__edge0;
                                } else { 
                                    if(prev__candidate_Complex_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_edge__edge0);
                                    }
                                }
                                return matches;
                            }
                            candidate_Complex_edge__edge1.flags = candidate_Complex_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge1;
                            candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge0;
                            candidate_Complex_node_b.flags = candidate_Complex_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_b;
                            candidate_Complex_node_a.flags = candidate_Complex_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_a;
                            continue;
                        }
                        candidate_Complex_node_a.flags = candidate_Complex_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_a;
                        candidate_Complex_node_b.flags = candidate_Complex_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_b;
                        candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge0;
                        candidate_Complex_edge__edge1.flags = candidate_Complex_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge1;
                    }
                    while( (candidate_Complex_edge__edge1 = candidate_Complex_edge__edge1.outNext) != head_candidate_Complex_edge__edge1 );
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_edge__edge0;
                } else { 
                    if(prev__candidate_Complex_edge__edge0 == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_edge__edge0);
                    }
                }
            }
            return matches;
        }
    }

    public class AlternativeAction_Complex_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_Complex_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_Complex_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_Complex_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_Complex_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_Complex_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_Complex_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_Complex_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode Complex_node_a;
        public GRGEN_LGSP.LGSPNode Complex_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Complex_alt_0_ExtendAv 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendAv];
                // SubPreset Complex_node_a 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_b = Complex_node_b;
                // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge0 from Complex_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_node_a.outhead;
                if(head_candidate_Complex_alt_0_ExtendAv_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge0 = head_candidate_Complex_alt_0_ExtendAv_edge__edge0;
                    do
                    {
                        if(candidate_Complex_alt_0_ExtendAv_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendAv_edge__edge0,candidate_Complex_alt_0_ExtendAv_edge__edge0);
                        }
                        // Implicit Target Complex_alt_0_ExtendAv_node_b2 from Complex_alt_0_ExtendAv_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendAv_node_b2 = candidate_Complex_alt_0_ExtendAv_edge__edge0.target;
                        if(candidate_Complex_alt_0_ExtendAv_node_b2.type.TypeID!=2) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv_node_b2)))
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge2 from Complex_node_b 
                        GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_node_b.outhead;
                        if(head_candidate_Complex_alt_0_ExtendAv_edge__edge2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge2 = head_candidate_Complex_alt_0_ExtendAv_edge__edge2;
                            do
                            {
                                if(candidate_Complex_alt_0_ExtendAv_edge__edge2.type.TypeID!=1) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge2)))
                                {
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_Complex_alt_0_ExtendAv_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendAv_edge__edge2,candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                }
                                // Implicit Target Complex_alt_0_ExtendAv_node__node0 from Complex_alt_0_ExtendAv_edge__edge2 
                                GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendAv_node__node0 = candidate_Complex_alt_0_ExtendAv_edge__edge2.target;
                                if(candidate_Complex_alt_0_ExtendAv_node__node0.type.TypeID!=3) {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv_node__node0 = candidate_Complex_alt_0_ExtendAv_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_Complex_alt_0_ExtendAv_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv_node__node0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv_node__node0) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv_node__node0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Complex_alt_0_ExtendAv_node__node0,candidate_Complex_alt_0_ExtendAv_node__node0);
                                }
                                // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge1 from Complex_alt_0_ExtendAv_node_b2 
                                GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_node_b2.outhead;
                                if(head_candidate_Complex_alt_0_ExtendAv_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge1 = head_candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                    do
                                    {
                                        if(candidate_Complex_alt_0_ExtendAv_edge__edge1.type.TypeID!=1) {
                                            continue;
                                        }
                                        if(candidate_Complex_alt_0_ExtendAv_edge__edge1.target != candidate_Complex_node_a) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge1)))
                                        {
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        } else {
                                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendAv_edge__edge1,candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                        }
                                        // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge3 from Complex_alt_0_ExtendAv_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge3 = candidate_Complex_alt_0_ExtendAv_node__node0.outhead;
                                        if(head_candidate_Complex_alt_0_ExtendAv_edge__edge3 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge3 = head_candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                            do
                                            {
                                                if(candidate_Complex_alt_0_ExtendAv_edge__edge3.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge3)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // Implicit Target Complex_alt_0_ExtendAv_node__node1 from Complex_alt_0_ExtendAv_edge__edge3 
                                                GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendAv_node__node1 = candidate_Complex_alt_0_ExtendAv_edge__edge3.target;
                                                if(candidate_Complex_alt_0_ExtendAv_node__node1.type.TypeID!=3) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv_node__node1)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // Check whether there are subpattern matching tasks left to execute
                                                if(openTasks.Count==0)
                                                {
                                                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                                    Rule_Complex.Match_Complex_alt_0_ExtendAv match = new Rule_Complex.Match_Complex_alt_0_ExtendAv();
                                                    match._node_a = candidate_Complex_node_a;
                                                    match._node_b2 = candidate_Complex_alt_0_ExtendAv_node_b2;
                                                    match._node_b = candidate_Complex_node_b;
                                                    match._node__node0 = candidate_Complex_alt_0_ExtendAv_node__node0;
                                                    match._node__node1 = candidate_Complex_alt_0_ExtendAv_node__node1;
                                                    match._edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                    match._edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                    match._edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                    match._edge__edge3 = candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2 = candidate_Complex_alt_0_ExtendAv_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0 = candidate_Complex_alt_0_ExtendAv_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1 = candidate_Complex_alt_0_ExtendAv_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3 = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        Rule_Complex.Match_Complex_alt_0_ExtendAv match = new Rule_Complex.Match_Complex_alt_0_ExtendAv();
                                                        match._node_a = candidate_Complex_node_a;
                                                        match._node_b2 = candidate_Complex_alt_0_ExtendAv_node_b2;
                                                        match._node_b = candidate_Complex_node_b;
                                                        match._node__node0 = candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        match._node__node1 = candidate_Complex_alt_0_ExtendAv_node__node1;
                                                        match._edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        match._edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        match._edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        match._edge__edge3 = candidate_Complex_alt_0_ExtendAv_edge__edge3;
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
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        candidate_Complex_alt_0_ExtendAv_node__node1.flags = candidate_Complex_alt_0_ExtendAv_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                        candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        candidate_Complex_alt_0_ExtendAv_node_b2.flags = candidate_Complex_alt_0_ExtendAv_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                    candidate_Complex_alt_0_ExtendAv_node__node1.flags = candidate_Complex_alt_0_ExtendAv_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                    candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                    candidate_Complex_alt_0_ExtendAv_node_b2.flags = candidate_Complex_alt_0_ExtendAv_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                    continue;
                                                }
                                                candidate_Complex_alt_0_ExtendAv_node_b2.flags = candidate_Complex_alt_0_ExtendAv_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                candidate_Complex_alt_0_ExtendAv_node__node1.flags = candidate_Complex_alt_0_ExtendAv_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                            }
                                            while( (candidate_Complex_alt_0_ExtendAv_edge__edge3 = candidate_Complex_alt_0_ExtendAv_edge__edge3.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge3 );
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                        } else { 
                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge1 );
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv_node__node0 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv_node__node0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge2 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge0 );
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
            // Alternative case Complex_alt_0_ExtendAv2 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendAv2];
                // SubPreset Complex_node_a 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_b = Complex_node_b;
                // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge0 from Complex_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_node_a.outhead;
                if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge0 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                    do
                    {
                        if(candidate_Complex_alt_0_ExtendAv2_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge0,candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                        }
                        // Implicit Target Complex_alt_0_ExtendAv2_node_b2 from Complex_alt_0_ExtendAv2_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendAv2_node_b2 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.target;
                        if(candidate_Complex_alt_0_ExtendAv2_node_b2.type.TypeID!=2) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node_b2)))
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge2 from Complex_node_b 
                        GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_node_b.outhead;
                        if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge2 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                            do
                            {
                                if(candidate_Complex_alt_0_ExtendAv2_edge__edge2.type.TypeID!=1) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge2)))
                                {
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge2,candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                }
                                // Implicit Target Complex_alt_0_ExtendAv2_node__node0 from Complex_alt_0_ExtendAv2_edge__edge2 
                                GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendAv2_node__node0 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.target;
                                if(candidate_Complex_alt_0_ExtendAv2_node__node0.type.TypeID!=3) {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node0 = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node0) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Complex_alt_0_ExtendAv2_node__node0,candidate_Complex_alt_0_ExtendAv2_node__node0);
                                }
                                // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge1 from Complex_alt_0_ExtendAv2_node_b2 
                                GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_node_b2.outhead;
                                if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge1 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                    do
                                    {
                                        if(candidate_Complex_alt_0_ExtendAv2_edge__edge1.type.TypeID!=1) {
                                            continue;
                                        }
                                        if(candidate_Complex_alt_0_ExtendAv2_edge__edge1.target != candidate_Complex_node_a) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge1)))
                                        {
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                            candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        } else {
                                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge1,candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                        }
                                        // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge3 from Complex_alt_0_ExtendAv2_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_node__node0.outhead;
                                        if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge3 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge3 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                            do
                                            {
                                                if(candidate_Complex_alt_0_ExtendAv2_edge__edge3.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge3)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                } else {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge3) ? 1U : 0U;
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge3,candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                }
                                                // Implicit Target Complex_alt_0_ExtendAv2_node__node1 from Complex_alt_0_ExtendAv2_edge__edge3 
                                                GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendAv2_node__node1 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.target;
                                                if(candidate_Complex_alt_0_ExtendAv2_node__node1.type.TypeID!=3) {
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node1)))
                                                {
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                uint prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node1 = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                } else {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node1) ? 1U : 0U;
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Complex_alt_0_ExtendAv2_node__node1,candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                }
                                                // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge4 from Complex_alt_0_ExtendAv2_node__node1 
                                                GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge4 = candidate_Complex_alt_0_ExtendAv2_node__node1.outhead;
                                                if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge4 != null)
                                                {
                                                    GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge4 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                    do
                                                    {
                                                        if(candidate_Complex_alt_0_ExtendAv2_edge__edge4.type.TypeID!=1) {
                                                            continue;
                                                        }
                                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge4)))
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                        {
                                                            continue;
                                                        }
                                                        // Implicit Target Complex_alt_0_ExtendAv2_node__node2 from Complex_alt_0_ExtendAv2_edge__edge4 
                                                        GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendAv2_node__node2 = candidate_Complex_alt_0_ExtendAv2_edge__edge4.target;
                                                        if(candidate_Complex_alt_0_ExtendAv2_node__node2.type.TypeID!=3) {
                                                            continue;
                                                        }
                                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_node__node2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node2)))
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_Complex_alt_0_ExtendAv2_node__node2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                        {
                                                            continue;
                                                        }
                                                        // Check whether there are subpattern matching tasks left to execute
                                                        if(openTasks.Count==0)
                                                        {
                                                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                                            foundPartialMatches.Add(currentFoundPartialMatch);
                                                            Rule_Complex.Match_Complex_alt_0_ExtendAv2 match = new Rule_Complex.Match_Complex_alt_0_ExtendAv2();
                                                            match._node_a = candidate_Complex_node_a;
                                                            match._node_b2 = candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                            match._node_b = candidate_Complex_node_b;
                                                            match._node__node0 = candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                            match._node__node1 = candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                            match._node__node2 = candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                            match._edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                            match._edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                            match._edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                            match._edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                            match._edge__edge4 = candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                            currentFoundPartialMatch.Push(match);
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            continue;
                                                        }
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2 = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0 = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1 = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2 = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4 = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        // Match subpatterns 
                                                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                        // Check whether subpatterns were found 
                                                        if(matchesList.Count>0) {
                                                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                                            {
                                                                Rule_Complex.Match_Complex_alt_0_ExtendAv2 match = new Rule_Complex.Match_Complex_alt_0_ExtendAv2();
                                                                match._node_a = candidate_Complex_node_a;
                                                                match._node_b2 = candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                                match._node_b = candidate_Complex_node_b;
                                                                match._node__node0 = candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                match._node__node1 = candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                match._node__node2 = candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                                match._edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                match._edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                match._edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                match._edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                match._edge__edge4 = candidate_Complex_alt_0_ExtendAv2_edge__edge4;
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
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                candidate_Complex_alt_0_ExtendAv2_node__node2.flags = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                                candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                candidate_Complex_alt_0_ExtendAv2_node_b2.flags = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                            candidate_Complex_alt_0_ExtendAv2_node__node2.flags = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                            candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                            candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                            candidate_Complex_alt_0_ExtendAv2_node_b2.flags = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                            continue;
                                                        }
                                                        candidate_Complex_alt_0_ExtendAv2_node_b2.flags = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node2.flags = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                    }
                                                    while( (candidate_Complex_alt_0_ExtendAv2_edge__edge4 = candidate_Complex_alt_0_ExtendAv2_edge__edge4.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge4 );
                                                }
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                } else { 
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1 == 0) {
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                    }
                                                }
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                } else { 
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                    }
                                                }
                                            }
                                            while( (candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge3 );
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge1 );
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge2 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge0 );
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
            // Alternative case Complex_alt_0_ExtendNA2 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendNA2];
                // SubPreset Complex_node_a 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                GRGEN_LGSP.LGSPNode candidate_Complex_node_b = Complex_node_b;
                // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge0 from Complex_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge0 = candidate_Complex_node_a.outhead;
                if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge0 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                    do
                    {
                        if(candidate_Complex_alt_0_ExtendNA2_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendNA2_edge__edge0,candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                        }
                        // Implicit Target Complex_alt_0_ExtendNA2_node__node0 from Complex_alt_0_ExtendNA2_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendNA2_node__node0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0.target;
                        if(candidate_Complex_alt_0_ExtendNA2_node__node0.type.TypeID!=3) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendNA2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendNA2_node__node0 = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendNA2_node__node0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Complex_alt_0_ExtendNA2_node__node0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Complex_alt_0_ExtendNA2_node__node0,candidate_Complex_alt_0_ExtendNA2_node__node0);
                        }
                        // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge2 from Complex_node_b 
                        GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_node_b.outhead;
                        if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge2 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                            do
                            {
                                if(candidate_Complex_alt_0_ExtendNA2_edge__edge2.type.TypeID!=1) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge2)))
                                {
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendNA2_edge__edge2,candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                }
                                // Implicit Target Complex_alt_0_ExtendNA2_node_b2 from Complex_alt_0_ExtendNA2_edge__edge2 
                                GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendNA2_node_b2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.target;
                                if(candidate_Complex_alt_0_ExtendNA2_node_b2.type.TypeID!=2) {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendNA2_node_b2)))
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendNA2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge1 from Complex_alt_0_ExtendNA2_node__node0 
                                GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_node__node0.outhead;
                                if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge1 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                    do
                                    {
                                        if(candidate_Complex_alt_0_ExtendNA2_edge__edge1.type.TypeID!=1) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge1)))
                                        {
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        } else {
                                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Complex_alt_0_ExtendNA2_edge__edge1,candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                        }
                                        // Implicit Target Complex_alt_0_ExtendNA2_node__node1 from Complex_alt_0_ExtendNA2_edge__edge1 
                                        GRGEN_LGSP.LGSPNode candidate_Complex_alt_0_ExtendNA2_node__node1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.target;
                                        if(candidate_Complex_alt_0_ExtendNA2_node__node1.type.TypeID!=3) {
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendNA2_node__node1)))
                                        {
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendNA2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge3 from Complex_alt_0_ExtendNA2_node_b2 
                                        GRGEN_LGSP.LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge3 = candidate_Complex_alt_0_ExtendNA2_node_b2.outhead;
                                        if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge3 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge3 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                            do
                                            {
                                                if(candidate_Complex_alt_0_ExtendNA2_edge__edge3.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if(candidate_Complex_alt_0_ExtendNA2_edge__edge3.target != candidate_Complex_node_b) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge3)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // Check whether there are subpattern matching tasks left to execute
                                                if(openTasks.Count==0)
                                                {
                                                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                                    Rule_Complex.Match_Complex_alt_0_ExtendNA2 match = new Rule_Complex.Match_Complex_alt_0_ExtendNA2();
                                                    match._node_a = candidate_Complex_node_a;
                                                    match._node__node0 = candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                    match._node__node1 = candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                    match._node_b = candidate_Complex_node_b;
                                                    match._node_b2 = candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                    match._edge__edge0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                    match._edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                    match._edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                    match._edge__edge3 = candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0 = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1 = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2 = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3 = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        Rule_Complex.Match_Complex_alt_0_ExtendNA2 match = new Rule_Complex.Match_Complex_alt_0_ExtendNA2();
                                                        match._node_a = candidate_Complex_node_a;
                                                        match._node__node0 = candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        match._node__node1 = candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                        match._node_b = candidate_Complex_node_b;
                                                        match._node_b2 = candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                        match._edge__edge0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        match._edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        match._edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        match._edge__edge3 = candidate_Complex_alt_0_ExtendNA2_edge__edge3;
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
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        candidate_Complex_alt_0_ExtendNA2_node_b2.flags = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                        candidate_Complex_alt_0_ExtendNA2_node__node1.flags = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                        candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                    candidate_Complex_alt_0_ExtendNA2_node_b2.flags = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                    candidate_Complex_alt_0_ExtendNA2_node__node1.flags = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                    candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                    continue;
                                                }
                                                candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                candidate_Complex_alt_0_ExtendNA2_node__node1.flags = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                candidate_Complex_alt_0_ExtendNA2_node_b2.flags = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                            }
                                            while( (candidate_Complex_alt_0_ExtendNA2_edge__edge3 = candidate_Complex_alt_0_ExtendNA2_edge__edge3.outNext) != head_candidate_Complex_alt_0_ExtendNA2_edge__edge3 );
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.outNext) != head_candidate_Complex_alt_0_ExtendNA2_edge__edge1 );
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.outNext) != head_candidate_Complex_alt_0_ExtendNA2_edge__edge2 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Complex_alt_0_ExtendNA2_node__node0);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_Complex_alt_0_ExtendNA2_edge__edge0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0.outNext) != head_candidate_Complex_alt_0_ExtendNA2_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_ComplexMax : GRGEN_LGSP.LGSPAction
    {
        public Action_ComplexMax() {
            rulePattern = Rule_ComplexMax.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ComplexMax.Match_ComplexMax>(this);
        }

        public override string Name { get { return "ComplexMax"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ComplexMax.Match_ComplexMax> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_ComplexMax instance = new Action_ComplexMax();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Lookup ComplexMax_edge__edge0 
            int type_id_candidate_ComplexMax_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_ComplexMax_edge__edge0], candidate_ComplexMax_edge__edge0 = head_candidate_ComplexMax_edge__edge0.typeNext; candidate_ComplexMax_edge__edge0 != head_candidate_ComplexMax_edge__edge0; candidate_ComplexMax_edge__edge0 = candidate_ComplexMax_edge__edge0.typeNext)
            {
                uint prev__candidate_ComplexMax_edge__edge0;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_ComplexMax_edge__edge0 = candidate_ComplexMax_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ComplexMax_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_ComplexMax_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_edge__edge0) ? 1U : 0U;
                    if(prev__candidate_ComplexMax_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_edge__edge0,candidate_ComplexMax_edge__edge0);
                }
                // Implicit Source ComplexMax_node_a from ComplexMax_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_a = candidate_ComplexMax_edge__edge0.source;
                if(candidate_ComplexMax_node_a.type.TypeID!=1) {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_edge__edge0;
                    } else { 
                        if(prev__candidate_ComplexMax_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_edge__edge0);
                        }
                    }
                    continue;
                }
                // Implicit Target ComplexMax_node_b from ComplexMax_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_b = candidate_ComplexMax_edge__edge0.target;
                if(candidate_ComplexMax_node_b.type.TypeID!=2) {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_edge__edge0;
                    } else { 
                        if(prev__candidate_ComplexMax_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_edge__edge0);
                        }
                    }
                    continue;
                }
                // Extend Outgoing ComplexMax_edge__edge1 from ComplexMax_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_edge__edge1 = candidate_ComplexMax_node_b.outhead;
                if(head_candidate_ComplexMax_edge__edge1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_edge__edge1 = head_candidate_ComplexMax_edge__edge1;
                    do
                    {
                        if(candidate_ComplexMax_edge__edge1.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_ComplexMax_edge__edge1.target != candidate_ComplexMax_node_a) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_edge__edge1)))
                        {
                            continue;
                        }
                        // Push alternative matching task for ComplexMax_alt_0
                        AlternativeAction_ComplexMax_alt_0 taskFor_alt_0 = AlternativeAction_ComplexMax_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_ComplexMax.ComplexMax_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.ComplexMax_node_a = candidate_ComplexMax_node_a;
                        taskFor_alt_0.ComplexMax_node_b = candidate_ComplexMax_node_b;
                        openTasks.Push(taskFor_alt_0);
                        uint prevGlobal__candidate_ComplexMax_node_a;
                        prevGlobal__candidate_ComplexMax_node_a = candidate_ComplexMax_node_a.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_node_a.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ComplexMax_node_b;
                        prevGlobal__candidate_ComplexMax_node_b = candidate_ComplexMax_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_node_b.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ComplexMax_edge__edge0;
                        prevGlobal__candidate_ComplexMax_edge__edge0 = candidate_ComplexMax_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ComplexMax_edge__edge1;
                        prevGlobal__candidate_ComplexMax_edge__edge1 = candidate_ComplexMax_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for alt_0
                        openTasks.Pop();
                        AlternativeAction_ComplexMax_alt_0.releaseTask(taskFor_alt_0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Rule_ComplexMax.Match_ComplexMax match = matches.GetNextUnfilledPosition();
                                match._node_a = candidate_ComplexMax_node_a;
                                match._node_b = candidate_ComplexMax_node_b;
                                match._edge__edge0 = candidate_ComplexMax_edge__edge0;
                                match._edge__edge1 = candidate_ComplexMax_edge__edge1;
                                match._alt_0 = (Rule_ComplexMax.IMatch_ComplexMax_alt_0)currentFoundPartialMatch.Pop();
                                matches.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_ComplexMax_edge__edge1.flags = candidate_ComplexMax_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge1;
                                candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge0;
                                candidate_ComplexMax_node_b.flags = candidate_ComplexMax_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_b;
                                candidate_ComplexMax_node_a.flags = candidate_ComplexMax_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_a;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_edge__edge0;
                                } else { 
                                    if(prev__candidate_ComplexMax_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_edge__edge0);
                                    }
                                }
                                return matches;
                            }
                            candidate_ComplexMax_edge__edge1.flags = candidate_ComplexMax_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge1;
                            candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge0;
                            candidate_ComplexMax_node_b.flags = candidate_ComplexMax_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_b;
                            candidate_ComplexMax_node_a.flags = candidate_ComplexMax_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_a;
                            continue;
                        }
                        candidate_ComplexMax_node_a.flags = candidate_ComplexMax_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_a;
                        candidate_ComplexMax_node_b.flags = candidate_ComplexMax_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_b;
                        candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge0;
                        candidate_ComplexMax_edge__edge1.flags = candidate_ComplexMax_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge1;
                    }
                    while( (candidate_ComplexMax_edge__edge1 = candidate_ComplexMax_edge__edge1.outNext) != head_candidate_ComplexMax_edge__edge1 );
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_edge__edge0;
                } else { 
                    if(prev__candidate_ComplexMax_edge__edge0 == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_edge__edge0);
                    }
                }
            }
            return matches;
        }
    }

    public class AlternativeAction_ComplexMax_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ComplexMax_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ComplexMax_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ComplexMax_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ComplexMax_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ComplexMax_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ComplexMax_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ComplexMax_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ComplexMax_node_a;
        public GRGEN_LGSP.LGSPNode ComplexMax_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ComplexMax_alt_0_ExtendAv 
            do {
                patternGraph = patternGraphs[(int)Rule_ComplexMax.ComplexMax_alt_0_CaseNums.@ExtendAv];
                // SubPreset ComplexMax_node_a 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_a = ComplexMax_node_a;
                // SubPreset ComplexMax_node_b 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_b = ComplexMax_node_b;
                // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge0 from ComplexMax_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_node_a.outhead;
                if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                    do
                    {
                        if(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0,candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                        }
                        // Implicit Target ComplexMax_alt_0_ExtendAv_node_b2 from ComplexMax_alt_0_ExtendAv_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv_node_b2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.target;
                        if(candidate_ComplexMax_alt_0_ExtendAv_node_b2.type.TypeID!=2) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node_b2)))
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge2 from ComplexMax_node_b 
                        GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_node_b.outhead;
                        if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                            do
                            {
                                if(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.type.TypeID!=1) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2)))
                                {
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2,candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                }
                                // Implicit Target ComplexMax_alt_0_ExtendAv_node__node0 from ComplexMax_alt_0_ExtendAv_edge__edge2 
                                GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.target;
                                if(candidate_ComplexMax_alt_0_ExtendAv_node__node0.type.TypeID!=3) {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node__node0) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv_node__node0,candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                }
                                // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge1 from ComplexMax_alt_0_ExtendAv_node_b2 
                                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_node_b2.outhead;
                                if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                    do
                                    {
                                        if(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.type.TypeID!=1) {
                                            continue;
                                        }
                                        if(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.target != candidate_ComplexMax_node_a) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1)))
                                        {
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        } else {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1,candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                        }
                                        // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge3 from ComplexMax_alt_0_ExtendAv_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_node__node0.outhead;
                                        if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                            do
                                            {
                                                if(candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge3)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // Implicit Target ComplexMax_alt_0_ExtendAv_node_c from ComplexMax_alt_0_ExtendAv_edge__edge3 
                                                GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv_node_c = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.target;
                                                if(candidate_ComplexMax_alt_0_ExtendAv_node_c.type.TypeID!=3) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node_c)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // NegativePattern 
                                                {
                                                    ++negLevel;
                                                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                                                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                                                    }
                                                    uint prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_c.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                    } else {
                                                        prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node_c) ? 1U : 0U;
                                                        if(prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv_node_c,candidate_ComplexMax_alt_0_ExtendAv_node_c);
                                                    }
                                                    // Extend Outgoing ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 from ComplexMax_alt_0_ExtendAv_node_c 
                                                    GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_node_c.outhead;
                                                    if(head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 != null)
                                                    {
                                                        GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0;
                                                        do
                                                        {
                                                            if(candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.type.TypeID!=1) {
                                                                continue;
                                                            }
                                                            if((candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                            {
                                                                continue;
                                                            }
                                                            // Implicit Target ComplexMax_alt_0_ExtendAv_neg_0_node__node0 from ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 
                                                            GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.target;
                                                            if(candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0.type.TypeID!=3) {
                                                                continue;
                                                            }
                                                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0)))
                                                            {
                                                                continue;
                                                            }
                                                            if((candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                            {
                                                                continue;
                                                            }
                                                            // negative pattern found
                                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                            } else { 
                                                                if(prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c == 0) {
                                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node_c);
                                                                }
                                                            }
                                                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                                                            }
                                                            --negLevel;
                                                            goto label1;
                                                        }
                                                        while( (candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 );
                                                    }
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    } else { 
                                                        if(prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c == 0) {
                                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node_c);
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
                                                    Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv match = new Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv();
                                                    match._node_a = candidate_ComplexMax_node_a;
                                                    match._node_b2 = candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                    match._node_b = candidate_ComplexMax_node_b;
                                                    match._node__node0 = candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                    match._node_c = candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    match._edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                    match._edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                    match._edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                    match._edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    goto label2;
                                                }
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2 = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_node_c.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv match = new Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv();
                                                        match._node_a = candidate_ComplexMax_node_a;
                                                        match._node_b2 = candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                        match._node_b = candidate_ComplexMax_node_b;
                                                        match._node__node0 = candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        match._node_c = candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                        match._edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        match._edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        match._edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        match._edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
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
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                    candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                    candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                    goto label3;
                                                }
                                                candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
label1: ;
label2: ;
label3: ;
                                            }
                                            while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 );
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                        } else { 
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 );
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 );
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
            // Alternative case ComplexMax_alt_0_ExtendAv2 
            do {
                patternGraph = patternGraphs[(int)Rule_ComplexMax.ComplexMax_alt_0_CaseNums.@ExtendAv2];
                // SubPreset ComplexMax_node_a 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_a = ComplexMax_node_a;
                // SubPreset ComplexMax_node_b 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_b = ComplexMax_node_b;
                // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge0 from ComplexMax_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_node_a.outhead;
                if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                    do
                    {
                        if(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                        }
                        // Implicit Target ComplexMax_alt_0_ExtendAv2_node_b2 from ComplexMax_alt_0_ExtendAv2_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node_b2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.target;
                        if(candidate_ComplexMax_alt_0_ExtendAv2_node_b2.type.TypeID!=2) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node_b2)))
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge2 from ComplexMax_node_b 
                        GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_node_b.outhead;
                        if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                            do
                            {
                                if(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.type.TypeID!=1) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2)))
                                {
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                }
                                // Implicit Target ComplexMax_alt_0_ExtendAv2_node__node0 from ComplexMax_alt_0_ExtendAv2_edge__edge2 
                                GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.target;
                                if(candidate_ComplexMax_alt_0_ExtendAv2_node__node0.type.TypeID!=3) {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node0) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv2_node__node0,candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                }
                                // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge1 from ComplexMax_alt_0_ExtendAv2_node_b2 
                                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.outhead;
                                if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                    do
                                    {
                                        if(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.type.TypeID!=1) {
                                            continue;
                                        }
                                        if(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.target != candidate_ComplexMax_node_a) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1)))
                                        {
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        } else {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                        }
                                        // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge3 from ComplexMax_alt_0_ExtendAv2_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.outhead;
                                        if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                            do
                                            {
                                                if(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                } else {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3) ? 1U : 0U;
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                }
                                                // Implicit Target ComplexMax_alt_0_ExtendAv2_node__node1 from ComplexMax_alt_0_ExtendAv2_edge__edge3 
                                                GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.target;
                                                if(candidate_ComplexMax_alt_0_ExtendAv2_node__node1.type.TypeID!=3) {
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node1)))
                                                {
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                                } else {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node1) ? 1U : 0U;
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv2_node__node1,candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                }
                                                // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge4 from ComplexMax_alt_0_ExtendAv2_node__node1 
                                                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.outhead;
                                                if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 != null)
                                                {
                                                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                    do
                                                    {
                                                        if(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.type.TypeID!=1) {
                                                            continue;
                                                        }
                                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4)))
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                        {
                                                            continue;
                                                        }
                                                        // Implicit Target ComplexMax_alt_0_ExtendAv2_node__node2 from ComplexMax_alt_0_ExtendAv2_edge__edge4 
                                                        GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node__node2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.target;
                                                        if(candidate_ComplexMax_alt_0_ExtendAv2_node__node2.type.TypeID!=3) {
                                                            continue;
                                                        }
                                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node2)))
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                        {
                                                            continue;
                                                        }
                                                        // Check whether there are subpattern matching tasks left to execute
                                                        if(openTasks.Count==0)
                                                        {
                                                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                                            foundPartialMatches.Add(currentFoundPartialMatch);
                                                            Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv2 match = new Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv2();
                                                            match._node_a = candidate_ComplexMax_node_a;
                                                            match._node_b2 = candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                            match._node_b = candidate_ComplexMax_node_b;
                                                            match._node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                            match._node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                            match._node__node2 = candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                            match._edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                            match._edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                            match._edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                            match._edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                            match._edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                            currentFoundPartialMatch.Push(match);
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            continue;
                                                        }
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2 = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2 = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        // Match subpatterns 
                                                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                        // Check whether subpatterns were found 
                                                        if(matchesList.Count>0) {
                                                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                                            {
                                                                Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv2 match = new Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendAv2();
                                                                match._node_a = candidate_ComplexMax_node_a;
                                                                match._node_b2 = candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                                match._node_b = candidate_ComplexMax_node_b;
                                                                match._node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                match._node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                match._node__node2 = candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                                match._edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                match._edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                match._edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                match._edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                match._edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
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
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 == 0) {
                                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                            continue;
                                                        }
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                    }
                                                    while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 );
                                                }
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                } else { 
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 == 0) {
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                    }
                                                }
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                } else { 
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 == 0) {
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                    }
                                                }
                                            }
                                            while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 );
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 );
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 );
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
            // Alternative case ComplexMax_alt_0_ExtendNA2 
            do {
                patternGraph = patternGraphs[(int)Rule_ComplexMax.ComplexMax_alt_0_CaseNums.@ExtendNA2];
                // SubPreset ComplexMax_node_a 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_a = ComplexMax_node_a;
                // SubPreset ComplexMax_node_b 
                GRGEN_LGSP.LGSPNode candidate_ComplexMax_node_b = ComplexMax_node_b;
                // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge0 from ComplexMax_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = candidate_ComplexMax_node_a.outhead;
                if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                    do
                    {
                        if(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0,candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                        }
                        // Implicit Target ComplexMax_alt_0_ExtendNA2_node__node0 from ComplexMax_alt_0_ExtendNA2_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.target;
                        if(candidate_ComplexMax_alt_0_ExtendNA2_node__node0.type.TypeID!=3) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_node__node0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_ComplexMax_alt_0_ExtendNA2_node__node0,candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                        }
                        // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge2 from ComplexMax_node_b 
                        GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_node_b.outhead;
                        if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                            do
                            {
                                if(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.type.TypeID!=1) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2)))
                                {
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2,candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                }
                                // Implicit Target ComplexMax_alt_0_ExtendNA2_node_b2 from ComplexMax_alt_0_ExtendNA2_edge__edge2 
                                GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendNA2_node_b2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.target;
                                if(candidate_ComplexMax_alt_0_ExtendNA2_node_b2.type.TypeID!=2) {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_node_b2)))
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge1 from ComplexMax_alt_0_ExtendNA2_node__node0 
                                GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.outhead;
                                if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                    do
                                    {
                                        if(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.type.TypeID!=1) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1)))
                                        {
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        } else {
                                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1,candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                        }
                                        // Implicit Target ComplexMax_alt_0_ExtendNA2_node__node1 from ComplexMax_alt_0_ExtendNA2_edge__edge1 
                                        GRGEN_LGSP.LGSPNode candidate_ComplexMax_alt_0_ExtendNA2_node__node1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.target;
                                        if(candidate_ComplexMax_alt_0_ExtendNA2_node__node1.type.TypeID!=3) {
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_node__node1)))
                                        {
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge3 from ComplexMax_alt_0_ExtendNA2_node_b2 
                                        GRGEN_LGSP.LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.outhead;
                                        if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                            do
                                            {
                                                if(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.target != candidate_ComplexMax_node_b) {
                                                    continue;
                                                }
                                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3)))
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // Check whether there are subpattern matching tasks left to execute
                                                if(openTasks.Count==0)
                                                {
                                                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                                    Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendNA2 match = new Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendNA2();
                                                    match._node_a = candidate_ComplexMax_node_a;
                                                    match._node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                    match._node__node1 = candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                    match._node_b = candidate_ComplexMax_node_b;
                                                    match._node_b2 = candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                    match._edge__edge0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                    match._edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                    match._edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                    match._edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1 = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2 = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendNA2 match = new Rule_ComplexMax.Match_ComplexMax_alt_0_ExtendNA2();
                                                        match._node_a = candidate_ComplexMax_node_a;
                                                        match._node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        match._node__node1 = candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                        match._node_b = candidate_ComplexMax_node_b;
                                                        match._node_b2 = candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                        match._edge__edge0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        match._edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        match._edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        match._edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
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
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 == 0) {
                                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                    continue;
                                                }
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                            }
                                            while( (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.outNext) != head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 );
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.outNext) != head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 );
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.outNext) != head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.outNext) != head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_createABA : GRGEN_LGSP.LGSPAction
    {
        public Action_createABA() {
            rulePattern = Rule_createABA.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createABA.Match_createABA>(this);
        }

        public override string Name { get { return "createABA"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createABA.Match_createABA> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createABA instance = new Action_createABA();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createABA.Match_createABA match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_homm : GRGEN_LGSP.LGSPAction
    {
        public Action_homm() {
            rulePattern = Rule_homm.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_homm.Match_homm>(this);
        }

        public override string Name { get { return "homm"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_homm.Match_homm> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_homm instance = new Action_homm();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Lookup homm_edge__edge0 
            int type_id_candidate_homm_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_homm_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_homm_edge__edge0], candidate_homm_edge__edge0 = head_candidate_homm_edge__edge0.typeNext; candidate_homm_edge__edge0 != head_candidate_homm_edge__edge0; candidate_homm_edge__edge0 = candidate_homm_edge__edge0.typeNext)
            {
                uint prev__candidate_homm_edge__edge0;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_homm_edge__edge0 = candidate_homm_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_homm_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_homm_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_homm_edge__edge0) ? 1U : 0U;
                    if(prev__candidate_homm_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_homm_edge__edge0,candidate_homm_edge__edge0);
                }
                // Implicit Source homm_node_a from homm_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_homm_node_a = candidate_homm_edge__edge0.source;
                if(candidate_homm_node_a.type.TypeID!=1) {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_edge__edge0;
                    } else { 
                        if(prev__candidate_homm_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_edge__edge0);
                        }
                    }
                    continue;
                }
                // Implicit Target homm_node_b from homm_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_homm_node_b = candidate_homm_edge__edge0.target;
                if(candidate_homm_node_b.type.TypeID!=2) {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_edge__edge0;
                    } else { 
                        if(prev__candidate_homm_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_edge__edge0);
                        }
                    }
                    continue;
                }
                // Extend Outgoing homm_edge__edge1 from homm_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_homm_edge__edge1 = candidate_homm_node_b.outhead;
                if(head_candidate_homm_edge__edge1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_homm_edge__edge1 = head_candidate_homm_edge__edge1;
                    do
                    {
                        if(candidate_homm_edge__edge1.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_homm_edge__edge1.target != candidate_homm_node_a) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_homm_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_edge__edge1)))
                        {
                            continue;
                        }
                        // Push alternative matching task for homm_alt_0
                        AlternativeAction_homm_alt_0 taskFor_alt_0 = AlternativeAction_homm_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Rule_homm.homm_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.homm_node_a = candidate_homm_node_a;
                        taskFor_alt_0.homm_node_b = candidate_homm_node_b;
                        openTasks.Push(taskFor_alt_0);
                        uint prevGlobal__candidate_homm_node_a;
                        prevGlobal__candidate_homm_node_a = candidate_homm_node_a.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_node_a.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_homm_node_b;
                        prevGlobal__candidate_homm_node_b = candidate_homm_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_node_b.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_homm_edge__edge0;
                        prevGlobal__candidate_homm_edge__edge0 = candidate_homm_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_homm_edge__edge1;
                        prevGlobal__candidate_homm_edge__edge1 = candidate_homm_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for alt_0
                        openTasks.Pop();
                        AlternativeAction_homm_alt_0.releaseTask(taskFor_alt_0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Rule_homm.Match_homm match = matches.GetNextUnfilledPosition();
                                match._node_a = candidate_homm_node_a;
                                match._node_b = candidate_homm_node_b;
                                match._edge__edge0 = candidate_homm_edge__edge0;
                                match._edge__edge1 = candidate_homm_edge__edge1;
                                match._alt_0 = (Rule_homm.IMatch_homm_alt_0)currentFoundPartialMatch.Pop();
                                matches.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_homm_edge__edge1.flags = candidate_homm_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge1;
                                candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge0;
                                candidate_homm_node_b.flags = candidate_homm_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_b;
                                candidate_homm_node_a.flags = candidate_homm_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_a;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_edge__edge0;
                                } else { 
                                    if(prev__candidate_homm_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_edge__edge0);
                                    }
                                }
                                return matches;
                            }
                            candidate_homm_edge__edge1.flags = candidate_homm_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge1;
                            candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge0;
                            candidate_homm_node_b.flags = candidate_homm_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_b;
                            candidate_homm_node_a.flags = candidate_homm_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_a;
                            continue;
                        }
                        candidate_homm_node_a.flags = candidate_homm_node_a.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_a;
                        candidate_homm_node_b.flags = candidate_homm_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_b;
                        candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge0;
                        candidate_homm_edge__edge1.flags = candidate_homm_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge1;
                    }
                    while( (candidate_homm_edge__edge1 = candidate_homm_edge__edge1.outNext) != head_candidate_homm_edge__edge1 );
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_edge__edge0;
                } else { 
                    if(prev__candidate_homm_edge__edge0 == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_edge__edge0);
                    }
                }
            }
            return matches;
        }
    }

    public class AlternativeAction_homm_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_homm_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_homm_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_homm_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_homm_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_homm_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_homm_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_homm_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode homm_node_a;
        public GRGEN_LGSP.LGSPNode homm_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case homm_alt_0_case1 
            do {
                patternGraph = patternGraphs[(int)Rule_homm.homm_alt_0_CaseNums.@case1];
                // SubPreset homm_node_a 
                GRGEN_LGSP.LGSPNode candidate_homm_node_a = homm_node_a;
                // SubPreset homm_node_b 
                GRGEN_LGSP.LGSPNode candidate_homm_node_b = homm_node_b;
                // Extend Outgoing homm_alt_0_case1_edge__edge0 from homm_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_node_a.outhead;
                if(head_candidate_homm_alt_0_case1_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_homm_alt_0_case1_edge__edge0 = head_candidate_homm_alt_0_case1_edge__edge0;
                    do
                    {
                        if(candidate_homm_alt_0_case1_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_homm_alt_0_case1_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_homm_alt_0_case1_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_alt_0_case1_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_homm_alt_0_case1_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_homm_alt_0_case1_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_homm_alt_0_case1_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_homm_alt_0_case1_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_homm_alt_0_case1_edge__edge0,candidate_homm_alt_0_case1_edge__edge0);
                        }
                        // Implicit Target homm_alt_0_case1_node_b2 from homm_alt_0_case1_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_homm_alt_0_case1_node_b2 = candidate_homm_alt_0_case1_edge__edge0.target;
                        if(candidate_homm_alt_0_case1_node_b2.type.TypeID!=2) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case1_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_homm_alt_0_case1_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN && candidate_homm_alt_0_case1_node_b2!=candidate_homm_node_b)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case1_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing homm_alt_0_case1_edge__edge1 from homm_alt_0_case1_node_b2 
                        GRGEN_LGSP.LGSPEdge head_candidate_homm_alt_0_case1_edge__edge1 = candidate_homm_alt_0_case1_node_b2.outhead;
                        if(head_candidate_homm_alt_0_case1_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_homm_alt_0_case1_edge__edge1 = head_candidate_homm_alt_0_case1_edge__edge1;
                            do
                            {
                                if(candidate_homm_alt_0_case1_edge__edge1.type.TypeID!=1) {
                                    continue;
                                }
                                if(candidate_homm_alt_0_case1_edge__edge1.target != candidate_homm_node_a) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_homm_alt_0_case1_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_alt_0_case1_edge__edge1)))
                                {
                                    continue;
                                }
                                if((candidate_homm_alt_0_case1_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    Rule_homm.Match_homm_alt_0_case1 match = new Rule_homm.Match_homm_alt_0_case1();
                                    match._node_a = candidate_homm_node_a;
                                    match._node_b2 = candidate_homm_alt_0_case1_node_b2;
                                    match._node_b = candidate_homm_node_b;
                                    match._edge__edge0 = candidate_homm_alt_0_case1_edge__edge0;
                                    match._edge__edge1 = candidate_homm_alt_0_case1_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case1_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                prevGlobal__candidate_homm_alt_0_case1_node_b2 = candidate_homm_alt_0_case1_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case1_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                prevGlobal__candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_alt_0_case1_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case1_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                                prevGlobal__candidate_homm_alt_0_case1_edge__edge1 = candidate_homm_alt_0_case1_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case1_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        Rule_homm.Match_homm_alt_0_case1 match = new Rule_homm.Match_homm_alt_0_case1();
                                        match._node_a = candidate_homm_node_a;
                                        match._node_b2 = candidate_homm_alt_0_case1_node_b2;
                                        match._node_b = candidate_homm_node_b;
                                        match._edge__edge0 = candidate_homm_alt_0_case1_edge__edge0;
                                        match._edge__edge1 = candidate_homm_alt_0_case1_edge__edge1;
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
                                        candidate_homm_alt_0_case1_edge__edge1.flags = candidate_homm_alt_0_case1_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                                        candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                        candidate_homm_alt_0_case1_node_b2.flags = candidate_homm_alt_0_case1_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case1_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_homm_alt_0_case1_edge__edge1.flags = candidate_homm_alt_0_case1_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                                    candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                    candidate_homm_alt_0_case1_node_b2.flags = candidate_homm_alt_0_case1_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                    continue;
                                }
                                candidate_homm_alt_0_case1_node_b2.flags = candidate_homm_alt_0_case1_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                candidate_homm_alt_0_case1_edge__edge1.flags = candidate_homm_alt_0_case1_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                            }
                            while( (candidate_homm_alt_0_case1_edge__edge1 = candidate_homm_alt_0_case1_edge__edge1.outNext) != head_candidate_homm_alt_0_case1_edge__edge1 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                        } else { 
                            if(prev__candidate_homm_alt_0_case1_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_alt_0_case1_edge__edge0.outNext) != head_candidate_homm_alt_0_case1_edge__edge0 );
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
            // Alternative case homm_alt_0_case2 
            do {
                patternGraph = patternGraphs[(int)Rule_homm.homm_alt_0_CaseNums.@case2];
                // SubPreset homm_node_a 
                GRGEN_LGSP.LGSPNode candidate_homm_node_a = homm_node_a;
                // Extend Outgoing homm_alt_0_case2_edge__edge0 from homm_node_a 
                GRGEN_LGSP.LGSPEdge head_candidate_homm_alt_0_case2_edge__edge0 = candidate_homm_node_a.outhead;
                if(head_candidate_homm_alt_0_case2_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_homm_alt_0_case2_edge__edge0 = head_candidate_homm_alt_0_case2_edge__edge0;
                    do
                    {
                        if(candidate_homm_alt_0_case2_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_homm_alt_0_case2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_homm_alt_0_case2_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_homm_alt_0_case2_edge__edge0 = candidate_homm_alt_0_case2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_homm_alt_0_case2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_homm_alt_0_case2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_homm_alt_0_case2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_homm_alt_0_case2_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_homm_alt_0_case2_edge__edge0,candidate_homm_alt_0_case2_edge__edge0);
                        }
                        // Implicit Target homm_alt_0_case2_node_b2 from homm_alt_0_case2_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_homm_alt_0_case2_node_b2 = candidate_homm_alt_0_case2_edge__edge0.target;
                        if(candidate_homm_alt_0_case2_node_b2.type.TypeID!=2) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_homm_alt_0_case2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case2_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing homm_alt_0_case2_edge__edge1 from homm_alt_0_case2_node_b2 
                        GRGEN_LGSP.LGSPEdge head_candidate_homm_alt_0_case2_edge__edge1 = candidate_homm_alt_0_case2_node_b2.outhead;
                        if(head_candidate_homm_alt_0_case2_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_homm_alt_0_case2_edge__edge1 = head_candidate_homm_alt_0_case2_edge__edge1;
                            do
                            {
                                if(candidate_homm_alt_0_case2_edge__edge1.type.TypeID!=1) {
                                    continue;
                                }
                                if(candidate_homm_alt_0_case2_edge__edge1.target != candidate_homm_node_a) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_homm_alt_0_case2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_alt_0_case2_edge__edge1)))
                                {
                                    continue;
                                }
                                if((candidate_homm_alt_0_case2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    Rule_homm.Match_homm_alt_0_case2 match = new Rule_homm.Match_homm_alt_0_case2();
                                    match._node_a = candidate_homm_node_a;
                                    match._node_b2 = candidate_homm_alt_0_case2_node_b2;
                                    match._edge__edge0 = candidate_homm_alt_0_case2_edge__edge0;
                                    match._edge__edge1 = candidate_homm_alt_0_case2_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case2_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                prevGlobal__candidate_homm_alt_0_case2_node_b2 = candidate_homm_alt_0_case2_node_b2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case2_node_b2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                prevGlobal__candidate_homm_alt_0_case2_edge__edge0 = candidate_homm_alt_0_case2_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case2_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                                prevGlobal__candidate_homm_alt_0_case2_edge__edge1 = candidate_homm_alt_0_case2_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case2_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        Rule_homm.Match_homm_alt_0_case2 match = new Rule_homm.Match_homm_alt_0_case2();
                                        match._node_a = candidate_homm_node_a;
                                        match._node_b2 = candidate_homm_alt_0_case2_node_b2;
                                        match._edge__edge0 = candidate_homm_alt_0_case2_edge__edge0;
                                        match._edge__edge1 = candidate_homm_alt_0_case2_edge__edge1;
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
                                        candidate_homm_alt_0_case2_edge__edge1.flags = candidate_homm_alt_0_case2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                                        candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                        candidate_homm_alt_0_case2_node_b2.flags = candidate_homm_alt_0_case2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case2_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_homm_alt_0_case2_edge__edge1.flags = candidate_homm_alt_0_case2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                                    candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                    candidate_homm_alt_0_case2_node_b2.flags = candidate_homm_alt_0_case2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                    continue;
                                }
                                candidate_homm_alt_0_case2_node_b2.flags = candidate_homm_alt_0_case2_node_b2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                candidate_homm_alt_0_case2_edge__edge1.flags = candidate_homm_alt_0_case2_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                            }
                            while( (candidate_homm_alt_0_case2_edge__edge1 = candidate_homm_alt_0_case2_edge__edge1.outNext) != head_candidate_homm_alt_0_case2_edge__edge1 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                        } else { 
                            if(prev__candidate_homm_alt_0_case2_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_homm_alt_0_case2_edge__edge0 = candidate_homm_alt_0_case2_edge__edge0.outNext) != head_candidate_homm_alt_0_case2_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_XtoAorB : GRGEN_LGSP.LGSPAction
    {
        public Action_XtoAorB() {
            rulePattern = Rule_XtoAorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_XtoAorB.Match_XtoAorB>(this);
        }

        public override string Name { get { return "XtoAorB"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_XtoAorB.Match_XtoAorB> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_XtoAorB instance = new Action_XtoAorB();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Lookup XtoAorB_node_x 
            foreach(GRGEN_LIBGR.NodeType type_candidate_XtoAorB_node_x in NodeType_Node.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_XtoAorB_node_x = type_candidate_XtoAorB_node_x.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_XtoAorB_node_x = graph.nodesByTypeHeads[type_id_candidate_XtoAorB_node_x], candidate_XtoAorB_node_x = head_candidate_XtoAorB_node_x.typeNext; candidate_XtoAorB_node_x != head_candidate_XtoAorB_node_x; candidate_XtoAorB_node_x = candidate_XtoAorB_node_x.typeNext)
                {
                    // Push subpattern matching task for _subpattern0
                    PatternAction_toAorB taskFor__subpattern0 = PatternAction_toAorB.getNewTask(graph, openTasks);
                    taskFor__subpattern0.toAorB_node_x = candidate_XtoAorB_node_x;
                    openTasks.Push(taskFor__subpattern0);
                    uint prevGlobal__candidate_XtoAorB_node_x;
                    prevGlobal__candidate_XtoAorB_node_x = candidate_XtoAorB_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_XtoAorB_node_x.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for _subpattern0
                    openTasks.Pop();
                    PatternAction_toAorB.releaseTask(taskFor__subpattern0);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_XtoAorB.Match_XtoAorB match = matches.GetNextUnfilledPosition();
                            match._node_x = candidate_XtoAorB_node_x;
                            match.__subpattern0 = (@Pattern_toAorB.Match_toAorB)currentFoundPartialMatch.Pop();
                            matches.PositionWasFilledFixIt();
                        }
                        matchesList.Clear();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_XtoAorB_node_x.flags = candidate_XtoAorB_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_XtoAorB_node_x;
                            return matches;
                        }
                        candidate_XtoAorB_node_x.flags = candidate_XtoAorB_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_XtoAorB_node_x;
                        continue;
                    }
                    candidate_XtoAorB_node_x.flags = candidate_XtoAorB_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_XtoAorB_node_x;
                }
            }
            return matches;
        }
    }


    public class AlternativesActions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public AlternativesActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public AlternativesActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("createA", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createA.Instance);
            actions.Add("createB", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createB.Instance);
            actions.Add("createC", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createC.Instance);
            actions.Add("createAtoB", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createAtoB.Instance);
            actions.Add("leer", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_leer.Instance);
            actions.Add("AorB", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_AorB.Instance);
            actions.Add("AandnotCorB", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_AandnotCorB.Instance);
            actions.Add("AorBorC", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_AorBorC.Instance);
            actions.Add("AtoAorB", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_AtoAorB.Instance);
            actions.Add("createComplex", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createComplex.Instance);
            actions.Add("Complex", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_Complex.Instance);
            actions.Add("ComplexMax", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_ComplexMax.Instance);
            actions.Add("createABA", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createABA.Instance);
            actions.Add("homm", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_homm.Instance);
            actions.Add("XtoAorB", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_XtoAorB.Instance);
        }

        public override String Name { get { return "AlternativesActions"; } }
        public override String ModelMD5Hash { get { return "9318fc8b892e7676373a2a9f05e2f491"; } }
    }
}