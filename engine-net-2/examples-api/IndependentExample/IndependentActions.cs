// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\independent\Independent.grg" on Sun Jan 12 22:15:13 CET 2020

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Independent;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_Independent;

namespace de.unika.ipd.grGen.Action_Independent
{
	public class Pattern_iteratedPath : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_iteratedPath instance = null;
		public static Pattern_iteratedPath Instance { get { if (instance==null) { instance = new Pattern_iteratedPath(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] iteratedPath_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] iteratedPath_node_end_AllowedTypes = null;
		public static bool[] iteratedPath_node_beg_IsAllowedType = null;
		public static bool[] iteratedPath_node_end_IsAllowedType = null;
		public enum iteratedPath_NodeNums { @beg, @end, };
		public enum iteratedPath_EdgeNums { };
		public enum iteratedPath_VariableNums { };
		public enum iteratedPath_SubNums { };
		public enum iteratedPath_AltNums { @alt_0, };
		public enum iteratedPath_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_iteratedPath;

		public enum iteratedPath_alt_0_CaseNums { @base, @recursive, };
		public static GRGEN_LIBGR.EdgeType[] iteratedPath_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPath_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum iteratedPath_alt_0_base_NodeNums { @beg, @end, };
		public enum iteratedPath_alt_0_base_EdgeNums { @_edge0, };
		public enum iteratedPath_alt_0_base_VariableNums { };
		public enum iteratedPath_alt_0_base_SubNums { };
		public enum iteratedPath_alt_0_base_AltNums { };
		public enum iteratedPath_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph iteratedPath_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] iteratedPath_alt_0_recursive_node_intermediate_AllowedTypes = null;
		public static bool[] iteratedPath_alt_0_recursive_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] iteratedPath_alt_0_recursive_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPath_alt_0_recursive_edge__edge0_IsAllowedType = null;
		public enum iteratedPath_alt_0_recursive_NodeNums { @beg, @intermediate, @end, };
		public enum iteratedPath_alt_0_recursive_EdgeNums { @_edge0, };
		public enum iteratedPath_alt_0_recursive_VariableNums { };
		public enum iteratedPath_alt_0_recursive_SubNums { @_sub0, };
		public enum iteratedPath_alt_0_recursive_AltNums { };
		public enum iteratedPath_alt_0_recursive_IterNums { };



		public GRGEN_LGSP.PatternGraph iteratedPath_alt_0_recursive;


		private Pattern_iteratedPath()
		{
			name = "iteratedPath";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "iteratedPath_node_beg", "iteratedPath_node_end", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] iteratedPath_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPath_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] iteratedPath_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] iteratedPath_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode iteratedPath_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "iteratedPath_node_beg", "beg", iteratedPath_node_beg_AllowedTypes, iteratedPath_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode iteratedPath_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "iteratedPath_node_end", "end", iteratedPath_node_end_AllowedTypes, iteratedPath_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false,null);
			bool[,] iteratedPath_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPath_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] iteratedPath_alt_0_base_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] iteratedPath_alt_0_base_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge iteratedPath_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "iteratedPath_alt_0_base_edge__edge0", "_edge0", iteratedPath_alt_0_base_edge__edge0_AllowedTypes, iteratedPath_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			iteratedPath_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"iteratedPath_alt_0_",
				null, "base",
				false, false,
				new GRGEN_LGSP.PatternNode[] { iteratedPath_node_beg, iteratedPath_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPath_alt_0_base_edge__edge0 }, 
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
				iteratedPath_alt_0_base_isNodeHomomorphicGlobal,
				iteratedPath_alt_0_base_isEdgeHomomorphicGlobal,
				iteratedPath_alt_0_base_isNodeTotallyHomomorphic,
				iteratedPath_alt_0_base_isEdgeTotallyHomomorphic
			);
			iteratedPath_alt_0_base.edgeToSourceNode.Add(iteratedPath_alt_0_base_edge__edge0, iteratedPath_node_beg);
			iteratedPath_alt_0_base.edgeToTargetNode.Add(iteratedPath_alt_0_base_edge__edge0, iteratedPath_node_end);

			bool[,] iteratedPath_alt_0_recursive_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] iteratedPath_alt_0_recursive_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] iteratedPath_alt_0_recursive_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] iteratedPath_alt_0_recursive_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode iteratedPath_alt_0_recursive_node_intermediate = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "iteratedPath_alt_0_recursive_node_intermediate", "intermediate", iteratedPath_alt_0_recursive_node_intermediate_AllowedTypes, iteratedPath_alt_0_recursive_node_intermediate_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge iteratedPath_alt_0_recursive_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "iteratedPath_alt_0_recursive_edge__edge0", "_edge0", iteratedPath_alt_0_recursive_edge__edge0_AllowedTypes, iteratedPath_alt_0_recursive_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding iteratedPath_alt_0_recursive__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_iteratedPath.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("iteratedPath_alt_0_recursive_node_intermediate"),
					new GRGEN_EXPR.GraphEntityExpression("iteratedPath_node_end"),
				}, 
				new string[] { }, new string[] { "iteratedPath_alt_0_recursive_node_intermediate", "iteratedPath_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			iteratedPath_alt_0_recursive = new GRGEN_LGSP.PatternGraph(
				"recursive",
				"iteratedPath_alt_0_",
				null, "recursive",
				false, false,
				new GRGEN_LGSP.PatternNode[] { iteratedPath_node_beg, iteratedPath_alt_0_recursive_node_intermediate, iteratedPath_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPath_alt_0_recursive_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { iteratedPath_alt_0_recursive__sub0 }, 
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
				iteratedPath_alt_0_recursive_isNodeHomomorphicGlobal,
				iteratedPath_alt_0_recursive_isEdgeHomomorphicGlobal,
				iteratedPath_alt_0_recursive_isNodeTotallyHomomorphic,
				iteratedPath_alt_0_recursive_isEdgeTotallyHomomorphic
			);
			iteratedPath_alt_0_recursive.edgeToSourceNode.Add(iteratedPath_alt_0_recursive_edge__edge0, iteratedPath_node_beg);
			iteratedPath_alt_0_recursive.edgeToTargetNode.Add(iteratedPath_alt_0_recursive_edge__edge0, iteratedPath_alt_0_recursive_node_intermediate);

			GRGEN_LGSP.Alternative iteratedPath_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "iteratedPath_", new GRGEN_LGSP.PatternGraph[] { iteratedPath_alt_0_base, iteratedPath_alt_0_recursive } );

			pat_iteratedPath = new GRGEN_LGSP.PatternGraph(
				"iteratedPath",
				"",
				null, "iteratedPath",
				false, false,
				new GRGEN_LGSP.PatternNode[] { iteratedPath_node_beg, iteratedPath_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { iteratedPath_alt_0,  }, 
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
				iteratedPath_isNodeHomomorphicGlobal,
				iteratedPath_isEdgeHomomorphicGlobal,
				iteratedPath_isNodeTotallyHomomorphic,
				iteratedPath_isEdgeTotallyHomomorphic
			);
			iteratedPath_alt_0_base.embeddingGraph = pat_iteratedPath;
			iteratedPath_alt_0_recursive.embeddingGraph = pat_iteratedPath;

			iteratedPath_node_beg.pointOfDefinition = null;
			iteratedPath_node_end.pointOfDefinition = null;
			iteratedPath_alt_0_base_edge__edge0.pointOfDefinition = iteratedPath_alt_0_base;
			iteratedPath_alt_0_recursive_node_intermediate.pointOfDefinition = iteratedPath_alt_0_recursive;
			iteratedPath_alt_0_recursive_edge__edge0.pointOfDefinition = iteratedPath_alt_0_recursive;
			iteratedPath_alt_0_recursive__sub0.PointOfDefinition = iteratedPath_alt_0_recursive;

			patternGraph = pat_iteratedPath;
		}


		public void iteratedPath_Create(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPNode node_beg, GRGEN_LGSP.LGSPNode node_end)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			graph.SettingAddedNodeNames( create_iteratedPath_addedNodeNames );
			graph.SettingAddedEdgeNames( create_iteratedPath_addedEdgeNames );
		}
		private static string[] create_iteratedPath_addedNodeNames = new string[] {  };
		private static string[] create_iteratedPath_addedEdgeNames = new string[] {  };

		public void iteratedPath_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_iteratedPath curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			IMatch_iteratedPath_alt_0 alternative_alt_0 = curMatch._alt_0;
			iteratedPath_alt_0_Delete(actionEnv, alternative_alt_0);
		}

		public void iteratedPath_alt_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, IMatch_iteratedPath_alt_0 curMatch)
		{
			if(curMatch.Pattern == iteratedPath_alt_0_base) {
				iteratedPath_alt_0_base_Delete(actionEnv, (Match_iteratedPath_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == iteratedPath_alt_0_recursive) {
				iteratedPath_alt_0_recursive_Delete(actionEnv, (Match_iteratedPath_alt_0_recursive)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void iteratedPath_alt_0_base_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_iteratedPath_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_beg);
			graph.Remove(node_beg);
			graph.RemoveEdges(node_end);
			graph.Remove(node_end);
		}

		public void iteratedPath_alt_0_recursive_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_iteratedPath_alt_0_recursive curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_iteratedPath.Match_iteratedPath subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_beg);
			graph.Remove(node_beg);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			graph.RemoveEdges(node_end);
			graph.Remove(node_end);
			Pattern_iteratedPath.Instance.iteratedPath_Delete(actionEnv, subpattern__sub0);
		}

		static Pattern_iteratedPath() {
		}

		public interface IMatch_iteratedPath : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_iteratedPath_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_iteratedPath_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_iteratedPath_alt_0_base : IMatch_iteratedPath_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_iteratedPath_alt_0_recursive : IMatch_iteratedPath_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_intermediate { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPath.Match_iteratedPath @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_iteratedPath : GRGEN_LGSP.ListElement<Match_iteratedPath>, IMatch_iteratedPath
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPath_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_NodeNums.@beg: return _node_beg;
				case (int)iteratedPath_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public enum iteratedPath_EdgeNums { END_OF_ENUM };
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
			
			public enum iteratedPath_VariableNums { END_OF_ENUM };
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
			
			public enum iteratedPath_SubNums { END_OF_ENUM };
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
			
			public IMatch_iteratedPath_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_iteratedPath_alt_0 _alt_0;
			public enum iteratedPath_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				case "alt_0": return _alt_0;
				default: return null;
				}
			}
			
			public enum iteratedPath_IterNums { END_OF_ENUM };
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
			
			public enum iteratedPath_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPath.instance.pat_iteratedPath; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_iteratedPath(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_iteratedPath nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_iteratedPath cur = this;
				while(cur != null) {
					Match_iteratedPath next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_iteratedPath that)
			{
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				_alt_0 = that._alt_0;
			}

			public Match_iteratedPath(Match_iteratedPath that)
			{
				CopyMatchContent(that);
			}
			public Match_iteratedPath()
			{
			}

			public bool IsEqual(Match_iteratedPath that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(_alt_0 is Match_iteratedPath_alt_0_base && !(_alt_0 as Match_iteratedPath_alt_0_base).IsEqual(that._alt_0 as Match_iteratedPath_alt_0_base)) return false;
				if(_alt_0 is Match_iteratedPath_alt_0_recursive && !(_alt_0 as Match_iteratedPath_alt_0_recursive).IsEqual(that._alt_0 as Match_iteratedPath_alt_0_recursive)) return false;
				return true;
			}
		}

		public class Match_iteratedPath_alt_0_base : GRGEN_LGSP.ListElement<Match_iteratedPath_alt_0_base>, IMatch_iteratedPath_alt_0_base
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPath_alt_0_base_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_base_NodeNums.@beg: return _node_beg;
				case (int)iteratedPath_alt_0_base_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPath_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
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
			
			public enum iteratedPath_alt_0_base_VariableNums { END_OF_ENUM };
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
			
			public enum iteratedPath_alt_0_base_SubNums { END_OF_ENUM };
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
			
			public enum iteratedPath_alt_0_base_AltNums { END_OF_ENUM };
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
			
			public enum iteratedPath_alt_0_base_IterNums { END_OF_ENUM };
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
			
			public enum iteratedPath_alt_0_base_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPath.instance.iteratedPath_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_iteratedPath_alt_0_base(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_iteratedPath_alt_0_base nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_iteratedPath_alt_0_base cur = this;
				while(cur != null) {
					Match_iteratedPath_alt_0_base next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_iteratedPath_alt_0_base that)
			{
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_iteratedPath_alt_0_base(Match_iteratedPath_alt_0_base that)
			{
				CopyMatchContent(that);
			}
			public Match_iteratedPath_alt_0_base()
			{
			}

			public bool IsEqual(Match_iteratedPath_alt_0_base that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_iteratedPath_alt_0_recursive : GRGEN_LGSP.ListElement<Match_iteratedPath_alt_0_recursive>, IMatch_iteratedPath_alt_0_recursive
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } set { _node_intermediate = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPath_alt_0_recursive_NodeNums { @beg, @intermediate, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_recursive_NodeNums.@beg: return _node_beg;
				case (int)iteratedPath_alt_0_recursive_NodeNums.@intermediate: return _node_intermediate;
				case (int)iteratedPath_alt_0_recursive_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "intermediate": return _node_intermediate;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPath_alt_0_recursive_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_recursive_EdgeNums.@_edge0: return _edge__edge0;
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
			
			public enum iteratedPath_alt_0_recursive_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_iteratedPath.Match_iteratedPath @_sub0 { get { return @__sub0; } }
			public @Pattern_iteratedPath.Match_iteratedPath @__sub0;
			public enum iteratedPath_alt_0_recursive_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_recursive_SubNums.@_sub0: return __sub0;
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
			
			public enum iteratedPath_alt_0_recursive_AltNums { END_OF_ENUM };
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
			
			public enum iteratedPath_alt_0_recursive_IterNums { END_OF_ENUM };
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
			
			public enum iteratedPath_alt_0_recursive_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPath.instance.iteratedPath_alt_0_recursive; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_iteratedPath_alt_0_recursive(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_iteratedPath_alt_0_recursive nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_iteratedPath_alt_0_recursive cur = this;
				while(cur != null) {
					Match_iteratedPath_alt_0_recursive next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_iteratedPath_alt_0_recursive that)
			{
				_node_beg = that._node_beg;
				_node_intermediate = that._node_intermediate;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				@__sub0 = that.@__sub0;
			}

			public Match_iteratedPath_alt_0_recursive(Match_iteratedPath_alt_0_recursive that)
			{
				CopyMatchContent(that);
			}
			public Match_iteratedPath_alt_0_recursive()
			{
			}

			public bool IsEqual(Match_iteratedPath_alt_0_recursive that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_intermediate != that._node_intermediate) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(!@__sub0.IsEqual(that.@__sub0)) return false;
				return true;
			}
		}

	}

	public class Pattern_iteratedPathToIntNode : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_iteratedPathToIntNode instance = null;
		public static Pattern_iteratedPathToIntNode Instance { get { if (instance==null) { instance = new Pattern_iteratedPathToIntNode(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] iteratedPathToIntNode_node_beg_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_node_beg_IsAllowedType = null;
		public enum iteratedPathToIntNode_NodeNums { @beg, };
		public enum iteratedPathToIntNode_EdgeNums { };
		public enum iteratedPathToIntNode_VariableNums { };
		public enum iteratedPathToIntNode_SubNums { };
		public enum iteratedPathToIntNode_AltNums { @alt_0, };
		public enum iteratedPathToIntNode_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_iteratedPathToIntNode;

		public enum iteratedPathToIntNode_alt_0_CaseNums { @base, @recursive, };
		public static GRGEN_LIBGR.NodeType[] iteratedPathToIntNode_alt_0_base_node_end_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_alt_0_base_node_end_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] iteratedPathToIntNode_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum iteratedPathToIntNode_alt_0_base_NodeNums { @beg, @end, };
		public enum iteratedPathToIntNode_alt_0_base_EdgeNums { @_edge0, };
		public enum iteratedPathToIntNode_alt_0_base_VariableNums { };
		public enum iteratedPathToIntNode_alt_0_base_SubNums { };
		public enum iteratedPathToIntNode_alt_0_base_AltNums { };
		public enum iteratedPathToIntNode_alt_0_base_IterNums { };



		public GRGEN_LGSP.PatternGraph iteratedPathToIntNode_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] iteratedPathToIntNode_alt_0_recursive_node_intermediate_AllowedTypes = { GRGEN_MODEL.NodeType_Node.typeVar, };
		public static bool[] iteratedPathToIntNode_alt_0_recursive_node_intermediate_IsAllowedType = { true, false, };
		public static GRGEN_LIBGR.EdgeType[] iteratedPathToIntNode_alt_0_recursive_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_alt_0_recursive_edge__edge0_IsAllowedType = null;
		public enum iteratedPathToIntNode_alt_0_recursive_NodeNums { @beg, @intermediate, };
		public enum iteratedPathToIntNode_alt_0_recursive_EdgeNums { @_edge0, };
		public enum iteratedPathToIntNode_alt_0_recursive_VariableNums { };
		public enum iteratedPathToIntNode_alt_0_recursive_SubNums { @_sub0, };
		public enum iteratedPathToIntNode_alt_0_recursive_AltNums { };
		public enum iteratedPathToIntNode_alt_0_recursive_IterNums { };



		public GRGEN_LGSP.PatternGraph iteratedPathToIntNode_alt_0_recursive;


		private Pattern_iteratedPathToIntNode()
		{
			name = "iteratedPathToIntNode";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "iteratedPathToIntNode_node_beg", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] iteratedPathToIntNode_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] iteratedPathToIntNode_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] iteratedPathToIntNode_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] iteratedPathToIntNode_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode iteratedPathToIntNode_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "iteratedPathToIntNode_node_beg", "beg", iteratedPathToIntNode_node_beg_AllowedTypes, iteratedPathToIntNode_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			bool[,] iteratedPathToIntNode_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPathToIntNode_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] iteratedPathToIntNode_alt_0_base_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] iteratedPathToIntNode_alt_0_base_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode iteratedPathToIntNode_alt_0_base_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@intNode, GRGEN_MODEL.NodeType_intNode.typeVar, "GRGEN_MODEL.IintNode", "iteratedPathToIntNode_alt_0_base_node_end", "end", iteratedPathToIntNode_alt_0_base_node_end_AllowedTypes, iteratedPathToIntNode_alt_0_base_node_end_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge iteratedPathToIntNode_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "iteratedPathToIntNode_alt_0_base_edge__edge0", "_edge0", iteratedPathToIntNode_alt_0_base_edge__edge0_AllowedTypes, iteratedPathToIntNode_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			iteratedPathToIntNode_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"iteratedPathToIntNode_alt_0_",
				null, "base",
				false, false,
				new GRGEN_LGSP.PatternNode[] { iteratedPathToIntNode_node_beg, iteratedPathToIntNode_alt_0_base_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPathToIntNode_alt_0_base_edge__edge0 }, 
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
				iteratedPathToIntNode_alt_0_base_isNodeHomomorphicGlobal,
				iteratedPathToIntNode_alt_0_base_isEdgeHomomorphicGlobal,
				iteratedPathToIntNode_alt_0_base_isNodeTotallyHomomorphic,
				iteratedPathToIntNode_alt_0_base_isEdgeTotallyHomomorphic
			);
			iteratedPathToIntNode_alt_0_base.edgeToSourceNode.Add(iteratedPathToIntNode_alt_0_base_edge__edge0, iteratedPathToIntNode_node_beg);
			iteratedPathToIntNode_alt_0_base.edgeToTargetNode.Add(iteratedPathToIntNode_alt_0_base_edge__edge0, iteratedPathToIntNode_alt_0_base_node_end);

			bool[,] iteratedPathToIntNode_alt_0_recursive_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPathToIntNode_alt_0_recursive_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] iteratedPathToIntNode_alt_0_recursive_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] iteratedPathToIntNode_alt_0_recursive_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode iteratedPathToIntNode_alt_0_recursive_node_intermediate = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "iteratedPathToIntNode_alt_0_recursive_node_intermediate", "intermediate", iteratedPathToIntNode_alt_0_recursive_node_intermediate_AllowedTypes, iteratedPathToIntNode_alt_0_recursive_node_intermediate_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge iteratedPathToIntNode_alt_0_recursive_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "iteratedPathToIntNode_alt_0_recursive_edge__edge0", "_edge0", iteratedPathToIntNode_alt_0_recursive_edge__edge0_AllowedTypes, iteratedPathToIntNode_alt_0_recursive_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding iteratedPathToIntNode_alt_0_recursive__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_iteratedPathToIntNode.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("iteratedPathToIntNode_alt_0_recursive_node_intermediate"),
				}, 
				new string[] { }, new string[] { "iteratedPathToIntNode_alt_0_recursive_node_intermediate" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			iteratedPathToIntNode_alt_0_recursive = new GRGEN_LGSP.PatternGraph(
				"recursive",
				"iteratedPathToIntNode_alt_0_",
				null, "recursive",
				false, false,
				new GRGEN_LGSP.PatternNode[] { iteratedPathToIntNode_node_beg, iteratedPathToIntNode_alt_0_recursive_node_intermediate }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPathToIntNode_alt_0_recursive_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { iteratedPathToIntNode_alt_0_recursive__sub0 }, 
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
				iteratedPathToIntNode_alt_0_recursive_isNodeHomomorphicGlobal,
				iteratedPathToIntNode_alt_0_recursive_isEdgeHomomorphicGlobal,
				iteratedPathToIntNode_alt_0_recursive_isNodeTotallyHomomorphic,
				iteratedPathToIntNode_alt_0_recursive_isEdgeTotallyHomomorphic
			);
			iteratedPathToIntNode_alt_0_recursive.edgeToSourceNode.Add(iteratedPathToIntNode_alt_0_recursive_edge__edge0, iteratedPathToIntNode_node_beg);
			iteratedPathToIntNode_alt_0_recursive.edgeToTargetNode.Add(iteratedPathToIntNode_alt_0_recursive_edge__edge0, iteratedPathToIntNode_alt_0_recursive_node_intermediate);

			GRGEN_LGSP.Alternative iteratedPathToIntNode_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "iteratedPathToIntNode_", new GRGEN_LGSP.PatternGraph[] { iteratedPathToIntNode_alt_0_base, iteratedPathToIntNode_alt_0_recursive } );

			pat_iteratedPathToIntNode = new GRGEN_LGSP.PatternGraph(
				"iteratedPathToIntNode",
				"",
				null, "iteratedPathToIntNode",
				false, false,
				new GRGEN_LGSP.PatternNode[] { iteratedPathToIntNode_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { iteratedPathToIntNode_alt_0,  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				iteratedPathToIntNode_isNodeHomomorphicGlobal,
				iteratedPathToIntNode_isEdgeHomomorphicGlobal,
				iteratedPathToIntNode_isNodeTotallyHomomorphic,
				iteratedPathToIntNode_isEdgeTotallyHomomorphic
			);
			iteratedPathToIntNode_alt_0_base.embeddingGraph = pat_iteratedPathToIntNode;
			iteratedPathToIntNode_alt_0_recursive.embeddingGraph = pat_iteratedPathToIntNode;

			iteratedPathToIntNode_node_beg.pointOfDefinition = null;
			iteratedPathToIntNode_alt_0_base_node_end.pointOfDefinition = iteratedPathToIntNode_alt_0_base;
			iteratedPathToIntNode_alt_0_base_edge__edge0.pointOfDefinition = iteratedPathToIntNode_alt_0_base;
			iteratedPathToIntNode_alt_0_recursive_node_intermediate.pointOfDefinition = iteratedPathToIntNode_alt_0_recursive;
			iteratedPathToIntNode_alt_0_recursive_edge__edge0.pointOfDefinition = iteratedPathToIntNode_alt_0_recursive;
			iteratedPathToIntNode_alt_0_recursive__sub0.PointOfDefinition = iteratedPathToIntNode_alt_0_recursive;

			patternGraph = pat_iteratedPathToIntNode;
		}


		public void iteratedPathToIntNode_Create(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPNode node_beg)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			graph.SettingAddedNodeNames( create_iteratedPathToIntNode_addedNodeNames );
			graph.SettingAddedEdgeNames( create_iteratedPathToIntNode_addedEdgeNames );
		}
		private static string[] create_iteratedPathToIntNode_addedNodeNames = new string[] {  };
		private static string[] create_iteratedPathToIntNode_addedEdgeNames = new string[] {  };

		public void iteratedPathToIntNode_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_iteratedPathToIntNode curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			IMatch_iteratedPathToIntNode_alt_0 alternative_alt_0 = curMatch._alt_0;
			iteratedPathToIntNode_alt_0_Delete(actionEnv, alternative_alt_0);
		}

		public void iteratedPathToIntNode_alt_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, IMatch_iteratedPathToIntNode_alt_0 curMatch)
		{
			if(curMatch.Pattern == iteratedPathToIntNode_alt_0_base) {
				iteratedPathToIntNode_alt_0_base_Delete(actionEnv, (Match_iteratedPathToIntNode_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == iteratedPathToIntNode_alt_0_recursive) {
				iteratedPathToIntNode_alt_0_recursive_Delete(actionEnv, (Match_iteratedPathToIntNode_alt_0_recursive)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void iteratedPathToIntNode_alt_0_base_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_iteratedPathToIntNode_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_beg);
			graph.Remove(node_beg);
			graph.RemoveEdges(node_end);
			graph.Remove(node_end);
		}

		public void iteratedPathToIntNode_alt_0_recursive_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_iteratedPathToIntNode_alt_0_recursive curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_beg);
			graph.Remove(node_beg);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_iteratedPathToIntNode.Instance.iteratedPathToIntNode_Delete(actionEnv, subpattern__sub0);
		}

		static Pattern_iteratedPathToIntNode() {
		}

		public interface IMatch_iteratedPathToIntNode : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_iteratedPathToIntNode_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_iteratedPathToIntNode_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_iteratedPathToIntNode_alt_0_base : IMatch_iteratedPathToIntNode_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_MODEL.IintNode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_iteratedPathToIntNode_alt_0_recursive : IMatch_iteratedPathToIntNode_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_intermediate { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_iteratedPathToIntNode : GRGEN_LGSP.ListElement<Match_iteratedPathToIntNode>, IMatch_iteratedPathToIntNode
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public enum iteratedPathToIntNode_NodeNums { @beg, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_NodeNums.@beg: return _node_beg;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_EdgeNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_VariableNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_SubNums { END_OF_ENUM };
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
			
			public IMatch_iteratedPathToIntNode_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_iteratedPathToIntNode_alt_0 _alt_0;
			public enum iteratedPathToIntNode_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				case "alt_0": return _alt_0;
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_IterNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPathToIntNode.instance.pat_iteratedPathToIntNode; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_iteratedPathToIntNode(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_iteratedPathToIntNode nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_iteratedPathToIntNode cur = this;
				while(cur != null) {
					Match_iteratedPathToIntNode next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_iteratedPathToIntNode that)
			{
				_node_beg = that._node_beg;
				_alt_0 = that._alt_0;
			}

			public Match_iteratedPathToIntNode(Match_iteratedPathToIntNode that)
			{
				CopyMatchContent(that);
			}
			public Match_iteratedPathToIntNode()
			{
			}

			public bool IsEqual(Match_iteratedPathToIntNode that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_alt_0 is Match_iteratedPathToIntNode_alt_0_base && !(_alt_0 as Match_iteratedPathToIntNode_alt_0_base).IsEqual(that._alt_0 as Match_iteratedPathToIntNode_alt_0_base)) return false;
				if(_alt_0 is Match_iteratedPathToIntNode_alt_0_recursive && !(_alt_0 as Match_iteratedPathToIntNode_alt_0_recursive).IsEqual(that._alt_0 as Match_iteratedPathToIntNode_alt_0_recursive)) return false;
				return true;
			}
		}

		public class Match_iteratedPathToIntNode_alt_0_base : GRGEN_LGSP.ListElement<Match_iteratedPathToIntNode_alt_0_base>, IMatch_iteratedPathToIntNode_alt_0_base
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IintNode node_end { get { return (GRGEN_MODEL.IintNode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPathToIntNode_alt_0_base_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_base_NodeNums.@beg: return _node_beg;
				case (int)iteratedPathToIntNode_alt_0_base_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPathToIntNode_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
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
			
			public enum iteratedPathToIntNode_alt_0_base_VariableNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_alt_0_base_SubNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_alt_0_base_AltNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_alt_0_base_IterNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_alt_0_base_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPathToIntNode.instance.iteratedPathToIntNode_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_iteratedPathToIntNode_alt_0_base(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_iteratedPathToIntNode_alt_0_base nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_iteratedPathToIntNode_alt_0_base cur = this;
				while(cur != null) {
					Match_iteratedPathToIntNode_alt_0_base next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_iteratedPathToIntNode_alt_0_base that)
			{
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_iteratedPathToIntNode_alt_0_base(Match_iteratedPathToIntNode_alt_0_base that)
			{
				CopyMatchContent(that);
			}
			public Match_iteratedPathToIntNode_alt_0_base()
			{
			}

			public bool IsEqual(Match_iteratedPathToIntNode_alt_0_base that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_iteratedPathToIntNode_alt_0_recursive : GRGEN_LGSP.ListElement<Match_iteratedPathToIntNode_alt_0_recursive>, IMatch_iteratedPathToIntNode_alt_0_recursive
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } set { _node_intermediate = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public enum iteratedPathToIntNode_alt_0_recursive_NodeNums { @beg, @intermediate, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_recursive_NodeNums.@beg: return _node_beg;
				case (int)iteratedPathToIntNode_alt_0_recursive_NodeNums.@intermediate: return _node_intermediate;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "intermediate": return _node_intermediate;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPathToIntNode_alt_0_recursive_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_recursive_EdgeNums.@_edge0: return _edge__edge0;
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
			
			public enum iteratedPathToIntNode_alt_0_recursive_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_sub0 { get { return @__sub0; } }
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @__sub0;
			public enum iteratedPathToIntNode_alt_0_recursive_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_recursive_SubNums.@_sub0: return __sub0;
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
			
			public enum iteratedPathToIntNode_alt_0_recursive_AltNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_alt_0_recursive_IterNums { END_OF_ENUM };
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
			
			public enum iteratedPathToIntNode_alt_0_recursive_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPathToIntNode.instance.iteratedPathToIntNode_alt_0_recursive; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_iteratedPathToIntNode_alt_0_recursive(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_iteratedPathToIntNode_alt_0_recursive nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_iteratedPathToIntNode_alt_0_recursive cur = this;
				while(cur != null) {
					Match_iteratedPathToIntNode_alt_0_recursive next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_iteratedPathToIntNode_alt_0_recursive that)
			{
				_node_beg = that._node_beg;
				_node_intermediate = that._node_intermediate;
				_edge__edge0 = that._edge__edge0;
				@__sub0 = that.@__sub0;
			}

			public Match_iteratedPathToIntNode_alt_0_recursive(Match_iteratedPathToIntNode_alt_0_recursive that)
			{
				CopyMatchContent(that);
			}
			public Match_iteratedPathToIntNode_alt_0_recursive()
			{
			}

			public bool IsEqual(Match_iteratedPathToIntNode_alt_0_recursive that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_intermediate != that._node_intermediate) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(!@__sub0.IsEqual(that.@__sub0)) return false;
				return true;
			}
		}

	}

	public class Rule_create : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_create instance = null;
		public static Rule_create Instance { get { if (instance==null) { instance = new Rule_create(); instance.initialize(); } return instance; } }

		public enum create_NodeNums { };
		public enum create_EdgeNums { };
		public enum create_VariableNums { };
		public enum create_SubNums { };
		public enum create_AltNums { };
		public enum create_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_create;


		private Rule_create()
		{
			name = "create";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] create_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] create_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] create_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] create_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_create = new GRGEN_LGSP.PatternGraph(
				"create",
				"",
				null, "create",
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
				create_isNodeHomomorphicGlobal,
				create_isEdgeHomomorphicGlobal,
				create_isNodeTotallyHomomorphic,
				create_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_create;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_create curMatch = (Match_create)_curMatch;
			graph.SettingAddedNodeNames( create_addedNodeNames );
			GRGEN_MODEL.@Node node_n1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n3 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n1, node_n2);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n2, node_n3);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n2, node_n1);
			GRGEN_MODEL.@Edge edge__edge3 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n3, node_n2);
			output_0 = (GRGEN_LIBGR.INode)(node_n1);
			output_1 = (GRGEN_LIBGR.INode)(node_n3);
			return;
		}
		private static string[] create_addedNodeNames = new string[] { "n1", "n2", "n3" };
		private static string[] create_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3" };

		static Rule_create() {
		}

		public interface IMatch_create : GRGEN_LIBGR.IMatch
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

		public class Match_create : GRGEN_LGSP.ListElement<Match_create>, IMatch_create
		{
			public enum create_NodeNums { END_OF_ENUM };
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
			
			public enum create_EdgeNums { END_OF_ENUM };
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
			
			public enum create_VariableNums { END_OF_ENUM };
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
			
			public enum create_SubNums { END_OF_ENUM };
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
			
			public enum create_AltNums { END_OF_ENUM };
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
			
			public enum create_IterNums { END_OF_ENUM };
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
			
			public enum create_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_create.instance.pat_create; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_create(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_create nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_create cur = this;
				while(cur != null) {
					Match_create next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_create that)
			{
			}

			public Match_create(Match_create that)
			{
				CopyMatchContent(that);
			}
			public Match_create()
			{
			}

			public bool IsEqual(Match_create that)
			{
				if(that==null) return false;
				return true;
			}
		}

	}

	public class Rule_find : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_find instance = null;
		public static Rule_find Instance { get { if (instance==null) { instance = new Rule_find(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] find_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] find_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] find_node_end_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] find_node__node1_AllowedTypes = null;
		public static bool[] find_node_beg_IsAllowedType = null;
		public static bool[] find_node__node0_IsAllowedType = null;
		public static bool[] find_node_end_IsAllowedType = null;
		public static bool[] find_node__node1_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge3_AllowedTypes = null;
		public static bool[] find_edge__edge0_IsAllowedType = null;
		public static bool[] find_edge__edge1_IsAllowedType = null;
		public static bool[] find_edge__edge2_IsAllowedType = null;
		public static bool[] find_edge__edge3_IsAllowedType = null;
		public enum find_NodeNums { @beg, @_node0, @end, @_node1, };
		public enum find_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum find_VariableNums { };
		public enum find_SubNums { };
		public enum find_AltNums { };
		public enum find_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_find;


		private Rule_find()
		{
			name = "find";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] find_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] find_isEdgeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[] find_isNodeTotallyHomomorphic = new bool[4] { false, false, false, false,  };
			bool[] find_isEdgeTotallyHomomorphic = new bool[4] { false, false, false, false,  };
			GRGEN_LGSP.PatternNode find_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "find_node_beg", "beg", find_node_beg_AllowedTypes, find_node_beg_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode find_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "find_node__node0", "_node0", find_node__node0_AllowedTypes, find_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode find_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "find_node_end", "end", find_node_end_AllowedTypes, find_node_end_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode find_node__node1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "find_node__node1", "_node1", find_node__node1_AllowedTypes, find_node__node1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge find_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "find_edge__edge0", "_edge0", find_edge__edge0_AllowedTypes, find_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge find_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "find_edge__edge1", "_edge1", find_edge__edge1_AllowedTypes, find_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge find_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "find_edge__edge2", "_edge2", find_edge__edge2_AllowedTypes, find_edge__edge2_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge find_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "find_edge__edge3", "_edge3", find_edge__edge3_AllowedTypes, find_edge__edge3_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_find = new GRGEN_LGSP.PatternGraph(
				"find",
				"",
				null, "find",
				false, false,
				new GRGEN_LGSP.PatternNode[] { find_node_beg, find_node__node0, find_node_end, find_node__node1 }, 
				new GRGEN_LGSP.PatternEdge[] { find_edge__edge0, find_edge__edge1, find_edge__edge2, find_edge__edge3 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				find_isNodeHomomorphicGlobal,
				find_isEdgeHomomorphicGlobal,
				find_isNodeTotallyHomomorphic,
				find_isEdgeTotallyHomomorphic
			);
			pat_find.edgeToSourceNode.Add(find_edge__edge0, find_node_beg);
			pat_find.edgeToTargetNode.Add(find_edge__edge0, find_node__node0);
			pat_find.edgeToSourceNode.Add(find_edge__edge1, find_node__node0);
			pat_find.edgeToTargetNode.Add(find_edge__edge1, find_node_end);
			pat_find.edgeToSourceNode.Add(find_edge__edge2, find_node__node1);
			pat_find.edgeToTargetNode.Add(find_edge__edge2, find_node_beg);
			pat_find.edgeToSourceNode.Add(find_edge__edge3, find_node_end);
			pat_find.edgeToTargetNode.Add(find_edge__edge3, find_node__node1);

			find_node_beg.pointOfDefinition = pat_find;
			find_node__node0.pointOfDefinition = pat_find;
			find_node_end.pointOfDefinition = pat_find;
			find_node__node1.pointOfDefinition = pat_find;
			find_edge__edge0.pointOfDefinition = pat_find;
			find_edge__edge1.pointOfDefinition = pat_find;
			find_edge__edge2.pointOfDefinition = pat_find;
			find_edge__edge3.pointOfDefinition = pat_find;

			patternGraph = pat_find;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_find curMatch = (Match_find)_curMatch;
			return;
		}

		static Rule_find() {
		}

		public interface IMatch_find : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			GRGEN_LIBGR.INode node__node1 { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge2 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge3 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_find : GRGEN_LGSP.ListElement<Match_find>, IMatch_find
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node__node1 { get { return (GRGEN_LIBGR.INode)_node__node1; } set { _node__node1 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public enum find_NodeNums { @beg, @_node0, @end, @_node1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)find_NodeNums.@beg: return _node_beg;
				case (int)find_NodeNums.@_node0: return _node__node0;
				case (int)find_NodeNums.@end: return _node_end;
				case (int)find_NodeNums.@_node1: return _node__node1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "_node0": return _node__node0;
				case "end": return _node_end;
				case "_node1": return _node__node1;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge2 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge2; } set { _edge__edge2 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge3 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge3; } set { _edge__edge3 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public enum find_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 4;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)find_EdgeNums.@_edge0: return _edge__edge0;
				case (int)find_EdgeNums.@_edge1: return _edge__edge1;
				case (int)find_EdgeNums.@_edge2: return _edge__edge2;
				case (int)find_EdgeNums.@_edge3: return _edge__edge3;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				case "_edge2": return _edge__edge2;
				case "_edge3": return _edge__edge3;
				default: return null;
				}
			}
			
			public enum find_VariableNums { END_OF_ENUM };
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
			
			public enum find_SubNums { END_OF_ENUM };
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
			
			public enum find_AltNums { END_OF_ENUM };
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
			
			public enum find_IterNums { END_OF_ENUM };
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
			
			public enum find_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_find.instance.pat_find; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_find(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_find nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_find cur = this;
				while(cur != null) {
					Match_find next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_find that)
			{
				_node_beg = that._node_beg;
				_node__node0 = that._node__node0;
				_node_end = that._node_end;
				_node__node1 = that._node__node1;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
				_edge__edge2 = that._edge__edge2;
				_edge__edge3 = that._edge__edge3;
			}

			public Match_find(Match_find that)
			{
				CopyMatchContent(that);
			}
			public Match_find()
			{
			}

			public bool IsEqual(Match_find that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_end != that._node_end) return false;
				if(_node__node1 != that._node__node1) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				if(_edge__edge2 != that._edge__edge2) return false;
				if(_edge__edge3 != that._edge__edge3) return false;
				return true;
			}
		}

	}

	public class Rule_findIndependent : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findIndependent instance = null;
		public static Rule_findIndependent Instance { get { if (instance==null) { instance = new Rule_findIndependent(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findIndependent_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findIndependent_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findIndependent_node_end_AllowedTypes = null;
		public static bool[] findIndependent_node_beg_IsAllowedType = null;
		public static bool[] findIndependent_node__node0_IsAllowedType = null;
		public static bool[] findIndependent_node_end_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_edge__edge1_AllowedTypes = null;
		public static bool[] findIndependent_edge__edge0_IsAllowedType = null;
		public static bool[] findIndependent_edge__edge1_IsAllowedType = null;
		public enum findIndependent_NodeNums { @beg, @_node0, @end, };
		public enum findIndependent_EdgeNums { @_edge0, @_edge1, };
		public enum findIndependent_VariableNums { };
		public enum findIndependent_SubNums { };
		public enum findIndependent_AltNums { };
		public enum findIndependent_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findIndependent;

		public static GRGEN_LIBGR.NodeType[] findIndependent_idpt_0_node__node0_AllowedTypes = null;
		public static bool[] findIndependent_idpt_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_idpt_0_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_idpt_0_edge__edge1_AllowedTypes = null;
		public static bool[] findIndependent_idpt_0_edge__edge0_IsAllowedType = null;
		public static bool[] findIndependent_idpt_0_edge__edge1_IsAllowedType = null;
		public enum findIndependent_idpt_0_NodeNums { @_node0, @beg, @end, };
		public enum findIndependent_idpt_0_EdgeNums { @_edge0, @_edge1, };
		public enum findIndependent_idpt_0_VariableNums { };
		public enum findIndependent_idpt_0_SubNums { };
		public enum findIndependent_idpt_0_AltNums { };
		public enum findIndependent_idpt_0_IterNums { };


		public GRGEN_LGSP.PatternGraph findIndependent_idpt_0;


		private Rule_findIndependent()
		{
			name = "findIndependent";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] findIndependent_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findIndependent_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] findIndependent_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] findIndependent_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode findIndependent_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findIndependent_node_beg", "beg", findIndependent_node_beg_AllowedTypes, findIndependent_node_beg_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode findIndependent_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findIndependent_node__node0", "_node0", findIndependent_node__node0_AllowedTypes, findIndependent_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode findIndependent_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findIndependent_node_end", "end", findIndependent_node_end_AllowedTypes, findIndependent_node_end_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findIndependent_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findIndependent_edge__edge0", "_edge0", findIndependent_edge__edge0_AllowedTypes, findIndependent_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findIndependent_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findIndependent_edge__edge1", "_edge1", findIndependent_edge__edge1_AllowedTypes, findIndependent_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] findIndependent_idpt_0_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findIndependent_idpt_0_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] findIndependent_idpt_0_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] findIndependent_idpt_0_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode findIndependent_idpt_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findIndependent_idpt_0_node__node0", "_node0", findIndependent_idpt_0_node__node0_AllowedTypes, findIndependent_idpt_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findIndependent_idpt_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findIndependent_idpt_0_edge__edge0", "_edge0", findIndependent_idpt_0_edge__edge0_AllowedTypes, findIndependent_idpt_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findIndependent_idpt_0_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findIndependent_idpt_0_edge__edge1", "_edge1", findIndependent_idpt_0_edge__edge1_AllowedTypes, findIndependent_idpt_0_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			findIndependent_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findIndependent_",
				null, "idpt_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findIndependent_idpt_0_node__node0, findIndependent_node_beg, findIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findIndependent_idpt_0_edge__edge0, findIndependent_idpt_0_edge__edge1 }, 
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
				findIndependent_idpt_0_isNodeHomomorphicGlobal,
				findIndependent_idpt_0_isEdgeHomomorphicGlobal,
				findIndependent_idpt_0_isNodeTotallyHomomorphic,
				findIndependent_idpt_0_isEdgeTotallyHomomorphic
			);
			findIndependent_idpt_0.edgeToSourceNode.Add(findIndependent_idpt_0_edge__edge0, findIndependent_idpt_0_node__node0);
			findIndependent_idpt_0.edgeToTargetNode.Add(findIndependent_idpt_0_edge__edge0, findIndependent_node_beg);
			findIndependent_idpt_0.edgeToSourceNode.Add(findIndependent_idpt_0_edge__edge1, findIndependent_node_end);
			findIndependent_idpt_0.edgeToTargetNode.Add(findIndependent_idpt_0_edge__edge1, findIndependent_idpt_0_node__node0);

			pat_findIndependent = new GRGEN_LGSP.PatternGraph(
				"findIndependent",
				"",
				null, "findIndependent",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findIndependent_node_beg, findIndependent_node__node0, findIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findIndependent_edge__edge0, findIndependent_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findIndependent_idpt_0,  }, 
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
				findIndependent_isNodeHomomorphicGlobal,
				findIndependent_isEdgeHomomorphicGlobal,
				findIndependent_isNodeTotallyHomomorphic,
				findIndependent_isEdgeTotallyHomomorphic
			);
			pat_findIndependent.edgeToSourceNode.Add(findIndependent_edge__edge0, findIndependent_node_beg);
			pat_findIndependent.edgeToTargetNode.Add(findIndependent_edge__edge0, findIndependent_node__node0);
			pat_findIndependent.edgeToSourceNode.Add(findIndependent_edge__edge1, findIndependent_node__node0);
			pat_findIndependent.edgeToTargetNode.Add(findIndependent_edge__edge1, findIndependent_node_end);
			findIndependent_idpt_0.embeddingGraph = pat_findIndependent;

			findIndependent_node_beg.pointOfDefinition = pat_findIndependent;
			findIndependent_node__node0.pointOfDefinition = pat_findIndependent;
			findIndependent_node_end.pointOfDefinition = pat_findIndependent;
			findIndependent_edge__edge0.pointOfDefinition = pat_findIndependent;
			findIndependent_edge__edge1.pointOfDefinition = pat_findIndependent;
			findIndependent_idpt_0_node__node0.pointOfDefinition = findIndependent_idpt_0;
			findIndependent_idpt_0_edge__edge0.pointOfDefinition = findIndependent_idpt_0;
			findIndependent_idpt_0_edge__edge1.pointOfDefinition = findIndependent_idpt_0;

			patternGraph = pat_findIndependent;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_findIndependent curMatch = (Match_findIndependent)_curMatch;
			return;
		}

		static Rule_findIndependent() {
		}

		public interface IMatch_findIndependent : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			IMatch_findIndependent_idpt_0 idpt_0 { get; }
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_findIndependent_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findIndependent : GRGEN_LGSP.ListElement<Match_findIndependent>, IMatch_findIndependent
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findIndependent_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_NodeNums.@beg: return _node_beg;
				case (int)findIndependent_NodeNums.@_node0: return _node__node0;
				case (int)findIndependent_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "_node0": return _node__node0;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findIndependent_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findIndependent_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findIndependent_VariableNums { END_OF_ENUM };
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
			
			public enum findIndependent_SubNums { END_OF_ENUM };
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
			
			public enum findIndependent_AltNums { END_OF_ENUM };
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
			
			public enum findIndependent_IterNums { END_OF_ENUM };
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
			
			public IMatch_findIndependent_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findIndependent_idpt_0 _idpt_0;
			public enum findIndependent_IdptNums { @idpt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findIndependent_IdptNums.@idpt_0: return _idpt_0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				case "idpt_0": return _idpt_0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findIndependent.instance.pat_findIndependent; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findIndependent(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findIndependent nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findIndependent cur = this;
				while(cur != null) {
					Match_findIndependent next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findIndependent that)
			{
				_node_beg = that._node_beg;
				_node__node0 = that._node__node0;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
				_idpt_0 = that._idpt_0;
			}

			public Match_findIndependent(Match_findIndependent that)
			{
				CopyMatchContent(that);
			}
			public Match_findIndependent()
			{
			}

			public bool IsEqual(Match_findIndependent that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

		public class Match_findIndependent_idpt_0 : GRGEN_LGSP.ListElement<Match_findIndependent_idpt_0>, IMatch_findIndependent_idpt_0
		{
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findIndependent_idpt_0_NodeNums { @_node0, @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_idpt_0_NodeNums.@_node0: return _node__node0;
				case (int)findIndependent_idpt_0_NodeNums.@beg: return _node_beg;
				case (int)findIndependent_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "_node0": return _node__node0;
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findIndependent_idpt_0_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_idpt_0_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findIndependent_idpt_0_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findIndependent_idpt_0_VariableNums { END_OF_ENUM };
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
			
			public enum findIndependent_idpt_0_SubNums { END_OF_ENUM };
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
			
			public enum findIndependent_idpt_0_AltNums { END_OF_ENUM };
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
			
			public enum findIndependent_idpt_0_IterNums { END_OF_ENUM };
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
			
			public enum findIndependent_idpt_0_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findIndependent.instance.findIndependent_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findIndependent_idpt_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findIndependent_idpt_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findIndependent_idpt_0 cur = this;
				while(cur != null) {
					Match_findIndependent_idpt_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findIndependent_idpt_0 that)
			{
				_node__node0 = that._node__node0;
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
			}

			public Match_findIndependent_idpt_0(Match_findIndependent_idpt_0 that)
			{
				CopyMatchContent(that);
			}
			public Match_findIndependent_idpt_0()
			{
			}

			public bool IsEqual(Match_findIndependent_idpt_0 that)
			{
				if(that==null) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

	}

	public class Rule_findMultiNested : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findMultiNested instance = null;
		public static Rule_findMultiNested Instance { get { if (instance==null) { instance = new Rule_findMultiNested(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findMultiNested_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findMultiNested_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findMultiNested_node_end_AllowedTypes = null;
		public static bool[] findMultiNested_node_beg_IsAllowedType = null;
		public static bool[] findMultiNested_node__node0_IsAllowedType = null;
		public static bool[] findMultiNested_node_end_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_NodeNums { @beg, @_node0, @end, };
		public enum findMultiNested_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_VariableNums { };
		public enum findMultiNested_SubNums { };
		public enum findMultiNested_AltNums { };
		public enum findMultiNested_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findMultiNested;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_0_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_0_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_0_NodeNums { @_node0, @beg, @end, };
		public enum findMultiNested_idpt_0_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_0_VariableNums { };
		public enum findMultiNested_idpt_0_SubNums { };
		public enum findMultiNested_idpt_0_AltNums { };
		public enum findMultiNested_idpt_0_IterNums { };


		public GRGEN_LGSP.PatternGraph findMultiNested_idpt_0;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_0_idpt_1_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_idpt_1_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_idpt_1_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_idpt_1_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_idpt_1_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_0_idpt_1_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_0_idpt_1_NodeNums { @beg, @_node0, @end, };
		public enum findMultiNested_idpt_0_idpt_1_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_0_idpt_1_VariableNums { };
		public enum findMultiNested_idpt_0_idpt_1_SubNums { };
		public enum findMultiNested_idpt_0_idpt_1_AltNums { };
		public enum findMultiNested_idpt_0_idpt_1_IterNums { };


		public GRGEN_LGSP.PatternGraph findMultiNested_idpt_0_idpt_1;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_2_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_2_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_2_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_2_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_2_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_2_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_2_NodeNums { @beg, @_node0, @end, };
		public enum findMultiNested_idpt_2_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_2_VariableNums { };
		public enum findMultiNested_idpt_2_SubNums { };
		public enum findMultiNested_idpt_2_AltNums { };
		public enum findMultiNested_idpt_2_IterNums { };


		public GRGEN_LGSP.PatternGraph findMultiNested_idpt_2;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_2_idpt_3_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_2_idpt_3_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_2_idpt_3_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_2_idpt_3_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_2_idpt_3_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_2_idpt_3_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_2_idpt_3_NodeNums { @_node0, @beg, @end, };
		public enum findMultiNested_idpt_2_idpt_3_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_2_idpt_3_VariableNums { };
		public enum findMultiNested_idpt_2_idpt_3_SubNums { };
		public enum findMultiNested_idpt_2_idpt_3_AltNums { };
		public enum findMultiNested_idpt_2_idpt_3_IterNums { };


		public GRGEN_LGSP.PatternGraph findMultiNested_idpt_2_idpt_3;


		private Rule_findMultiNested()
		{
			name = "findMultiNested";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] findMultiNested_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] findMultiNested_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] findMultiNested_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode findMultiNested_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findMultiNested_node_beg", "beg", findMultiNested_node_beg_AllowedTypes, findMultiNested_node_beg_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode findMultiNested_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findMultiNested_node__node0", "_node0", findMultiNested_node__node0_AllowedTypes, findMultiNested_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode findMultiNested_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findMultiNested_node_end", "end", findMultiNested_node_end_AllowedTypes, findMultiNested_node_end_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_edge__edge0", "_edge0", findMultiNested_edge__edge0_AllowedTypes, findMultiNested_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_edge__edge1", "_edge1", findMultiNested_edge__edge1_AllowedTypes, findMultiNested_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] findMultiNested_idpt_0_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_0_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] findMultiNested_idpt_0_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] findMultiNested_idpt_0_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode findMultiNested_idpt_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findMultiNested_idpt_0_node__node0", "_node0", findMultiNested_idpt_0_node__node0_AllowedTypes, findMultiNested_idpt_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_0_edge__edge0", "_edge0", findMultiNested_idpt_0_edge__edge0_AllowedTypes, findMultiNested_idpt_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_0_edge__edge1", "_edge1", findMultiNested_idpt_0_edge__edge1_AllowedTypes, findMultiNested_idpt_0_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] findMultiNested_idpt_0_idpt_1_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_0_idpt_1_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] findMultiNested_idpt_0_idpt_1_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] findMultiNested_idpt_0_idpt_1_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode findMultiNested_idpt_0_idpt_1_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findMultiNested_idpt_0_idpt_1_node__node0", "_node0", findMultiNested_idpt_0_idpt_1_node__node0_AllowedTypes, findMultiNested_idpt_0_idpt_1_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_idpt_1_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_0_idpt_1_edge__edge0", "_edge0", findMultiNested_idpt_0_idpt_1_edge__edge0_AllowedTypes, findMultiNested_idpt_0_idpt_1_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_idpt_1_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_0_idpt_1_edge__edge1", "_edge1", findMultiNested_idpt_0_idpt_1_edge__edge1_AllowedTypes, findMultiNested_idpt_0_idpt_1_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			findMultiNested_idpt_0_idpt_1 = new GRGEN_LGSP.PatternGraph(
				"idpt_1",
				"findMultiNested_idpt_0_",
				null, "idpt_1",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_node_beg, findMultiNested_idpt_0_idpt_1_node__node0, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_0_idpt_1_edge__edge0, findMultiNested_idpt_0_idpt_1_edge__edge1 }, 
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
				findMultiNested_idpt_0_idpt_1_isNodeHomomorphicGlobal,
				findMultiNested_idpt_0_idpt_1_isEdgeHomomorphicGlobal,
				findMultiNested_idpt_0_idpt_1_isNodeTotallyHomomorphic,
				findMultiNested_idpt_0_idpt_1_isEdgeTotallyHomomorphic
			);
			findMultiNested_idpt_0_idpt_1.edgeToSourceNode.Add(findMultiNested_idpt_0_idpt_1_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_0_idpt_1.edgeToTargetNode.Add(findMultiNested_idpt_0_idpt_1_edge__edge0, findMultiNested_idpt_0_idpt_1_node__node0);
			findMultiNested_idpt_0_idpt_1.edgeToSourceNode.Add(findMultiNested_idpt_0_idpt_1_edge__edge1, findMultiNested_idpt_0_idpt_1_node__node0);
			findMultiNested_idpt_0_idpt_1.edgeToTargetNode.Add(findMultiNested_idpt_0_idpt_1_edge__edge1, findMultiNested_node_end);

			findMultiNested_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findMultiNested_",
				null, "idpt_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_idpt_0_node__node0, findMultiNested_node_beg, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_0_edge__edge0, findMultiNested_idpt_0_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findMultiNested_idpt_0_idpt_1,  }, 
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
				findMultiNested_idpt_0_isNodeHomomorphicGlobal,
				findMultiNested_idpt_0_isEdgeHomomorphicGlobal,
				findMultiNested_idpt_0_isNodeTotallyHomomorphic,
				findMultiNested_idpt_0_isEdgeTotallyHomomorphic
			);
			findMultiNested_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_0_edge__edge0, findMultiNested_idpt_0_node__node0);
			findMultiNested_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_0_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_0_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_0_edge__edge1, findMultiNested_idpt_0_node__node0);
			findMultiNested_idpt_0_idpt_1.embeddingGraph = findMultiNested_idpt_0;

			bool[,] findMultiNested_idpt_2_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_2_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] findMultiNested_idpt_2_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] findMultiNested_idpt_2_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode findMultiNested_idpt_2_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findMultiNested_idpt_2_node__node0", "_node0", findMultiNested_idpt_2_node__node0_AllowedTypes, findMultiNested_idpt_2_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_2_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_2_edge__edge0", "_edge0", findMultiNested_idpt_2_edge__edge0_AllowedTypes, findMultiNested_idpt_2_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_2_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_2_edge__edge1", "_edge1", findMultiNested_idpt_2_edge__edge1_AllowedTypes, findMultiNested_idpt_2_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] findMultiNested_idpt_2_idpt_3_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_2_idpt_3_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] findMultiNested_idpt_2_idpt_3_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] findMultiNested_idpt_2_idpt_3_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode findMultiNested_idpt_2_idpt_3_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findMultiNested_idpt_2_idpt_3_node__node0", "_node0", findMultiNested_idpt_2_idpt_3_node__node0_AllowedTypes, findMultiNested_idpt_2_idpt_3_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_2_idpt_3_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_2_idpt_3_edge__edge0", "_edge0", findMultiNested_idpt_2_idpt_3_edge__edge0_AllowedTypes, findMultiNested_idpt_2_idpt_3_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_2_idpt_3_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "findMultiNested_idpt_2_idpt_3_edge__edge1", "_edge1", findMultiNested_idpt_2_idpt_3_edge__edge1_AllowedTypes, findMultiNested_idpt_2_idpt_3_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			findMultiNested_idpt_2_idpt_3 = new GRGEN_LGSP.PatternGraph(
				"idpt_3",
				"findMultiNested_idpt_2_",
				null, "idpt_3",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_idpt_2_idpt_3_node__node0, findMultiNested_node_beg, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_2_idpt_3_edge__edge0, findMultiNested_idpt_2_idpt_3_edge__edge1 }, 
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
				findMultiNested_idpt_2_idpt_3_isNodeHomomorphicGlobal,
				findMultiNested_idpt_2_idpt_3_isEdgeHomomorphicGlobal,
				findMultiNested_idpt_2_idpt_3_isNodeTotallyHomomorphic,
				findMultiNested_idpt_2_idpt_3_isEdgeTotallyHomomorphic
			);
			findMultiNested_idpt_2_idpt_3.edgeToSourceNode.Add(findMultiNested_idpt_2_idpt_3_edge__edge0, findMultiNested_idpt_2_idpt_3_node__node0);
			findMultiNested_idpt_2_idpt_3.edgeToTargetNode.Add(findMultiNested_idpt_2_idpt_3_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_2_idpt_3.edgeToSourceNode.Add(findMultiNested_idpt_2_idpt_3_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_2_idpt_3.edgeToTargetNode.Add(findMultiNested_idpt_2_idpt_3_edge__edge1, findMultiNested_idpt_2_idpt_3_node__node0);

			findMultiNested_idpt_2 = new GRGEN_LGSP.PatternGraph(
				"idpt_2",
				"findMultiNested_",
				null, "idpt_2",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_node_beg, findMultiNested_idpt_2_node__node0, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_2_edge__edge0, findMultiNested_idpt_2_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findMultiNested_idpt_2_idpt_3,  }, 
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
				findMultiNested_idpt_2_isNodeHomomorphicGlobal,
				findMultiNested_idpt_2_isEdgeHomomorphicGlobal,
				findMultiNested_idpt_2_isNodeTotallyHomomorphic,
				findMultiNested_idpt_2_isEdgeTotallyHomomorphic
			);
			findMultiNested_idpt_2.edgeToSourceNode.Add(findMultiNested_idpt_2_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_2.edgeToTargetNode.Add(findMultiNested_idpt_2_edge__edge0, findMultiNested_idpt_2_node__node0);
			findMultiNested_idpt_2.edgeToSourceNode.Add(findMultiNested_idpt_2_edge__edge1, findMultiNested_idpt_2_node__node0);
			findMultiNested_idpt_2.edgeToTargetNode.Add(findMultiNested_idpt_2_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_2_idpt_3.embeddingGraph = findMultiNested_idpt_2;

			pat_findMultiNested = new GRGEN_LGSP.PatternGraph(
				"findMultiNested",
				"",
				null, "findMultiNested",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_node_beg, findMultiNested_node__node0, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_edge__edge0, findMultiNested_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findMultiNested_idpt_0, findMultiNested_idpt_2,  }, 
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
				findMultiNested_isNodeHomomorphicGlobal,
				findMultiNested_isEdgeHomomorphicGlobal,
				findMultiNested_isNodeTotallyHomomorphic,
				findMultiNested_isEdgeTotallyHomomorphic
			);
			pat_findMultiNested.edgeToSourceNode.Add(findMultiNested_edge__edge0, findMultiNested_node_beg);
			pat_findMultiNested.edgeToTargetNode.Add(findMultiNested_edge__edge0, findMultiNested_node__node0);
			pat_findMultiNested.edgeToSourceNode.Add(findMultiNested_edge__edge1, findMultiNested_node__node0);
			pat_findMultiNested.edgeToTargetNode.Add(findMultiNested_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_0.embeddingGraph = pat_findMultiNested;
			findMultiNested_idpt_2.embeddingGraph = pat_findMultiNested;

			findMultiNested_node_beg.pointOfDefinition = pat_findMultiNested;
			findMultiNested_node__node0.pointOfDefinition = pat_findMultiNested;
			findMultiNested_node_end.pointOfDefinition = pat_findMultiNested;
			findMultiNested_edge__edge0.pointOfDefinition = pat_findMultiNested;
			findMultiNested_edge__edge1.pointOfDefinition = pat_findMultiNested;
			findMultiNested_idpt_0_node__node0.pointOfDefinition = findMultiNested_idpt_0;
			findMultiNested_idpt_0_edge__edge0.pointOfDefinition = findMultiNested_idpt_0;
			findMultiNested_idpt_0_edge__edge1.pointOfDefinition = findMultiNested_idpt_0;
			findMultiNested_idpt_0_idpt_1_node__node0.pointOfDefinition = findMultiNested_idpt_0_idpt_1;
			findMultiNested_idpt_0_idpt_1_edge__edge0.pointOfDefinition = findMultiNested_idpt_0_idpt_1;
			findMultiNested_idpt_0_idpt_1_edge__edge1.pointOfDefinition = findMultiNested_idpt_0_idpt_1;
			findMultiNested_idpt_2_node__node0.pointOfDefinition = findMultiNested_idpt_2;
			findMultiNested_idpt_2_edge__edge0.pointOfDefinition = findMultiNested_idpt_2;
			findMultiNested_idpt_2_edge__edge1.pointOfDefinition = findMultiNested_idpt_2;
			findMultiNested_idpt_2_idpt_3_node__node0.pointOfDefinition = findMultiNested_idpt_2_idpt_3;
			findMultiNested_idpt_2_idpt_3_edge__edge0.pointOfDefinition = findMultiNested_idpt_2_idpt_3;
			findMultiNested_idpt_2_idpt_3_edge__edge1.pointOfDefinition = findMultiNested_idpt_2_idpt_3;

			patternGraph = pat_findMultiNested;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_findMultiNested curMatch = (Match_findMultiNested)_curMatch;
			return;
		}

		static Rule_findMultiNested() {
		}

		public interface IMatch_findMultiNested : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			IMatch_findMultiNested_idpt_0 idpt_0 { get; }
			IMatch_findMultiNested_idpt_2 idpt_2 { get; }
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_findMultiNested_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			IMatch_findMultiNested_idpt_0_idpt_1 idpt_1 { get; }
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_findMultiNested_idpt_0_idpt_1 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_findMultiNested_idpt_2 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			IMatch_findMultiNested_idpt_2_idpt_3 idpt_3 { get; }
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_findMultiNested_idpt_2_idpt_3 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node__node0 { get; set; }
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findMultiNested : GRGEN_LGSP.ListElement<Match_findMultiNested>, IMatch_findMultiNested
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "_node0": return _node__node0;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_VariableNums { END_OF_ENUM };
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
			
			public enum findMultiNested_SubNums { END_OF_ENUM };
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
			
			public enum findMultiNested_AltNums { END_OF_ENUM };
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
			
			public enum findMultiNested_IterNums { END_OF_ENUM };
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
			
			public IMatch_findMultiNested_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findMultiNested_idpt_2 idpt_2 { get { return _idpt_2; } }
			public IMatch_findMultiNested_idpt_0 _idpt_0;
			public IMatch_findMultiNested_idpt_2 _idpt_2;
			public enum findMultiNested_IdptNums { @idpt_0, @idpt_2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 2;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_IdptNums.@idpt_0: return _idpt_0;
				case (int)findMultiNested_IdptNums.@idpt_2: return _idpt_2;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				case "idpt_0": return _idpt_0;
				case "idpt_2": return _idpt_2;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.pat_findMultiNested; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findMultiNested(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findMultiNested nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findMultiNested cur = this;
				while(cur != null) {
					Match_findMultiNested next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findMultiNested that)
			{
				_node_beg = that._node_beg;
				_node__node0 = that._node__node0;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
				_idpt_0 = that._idpt_0;
				_idpt_2 = that._idpt_2;
			}

			public Match_findMultiNested(Match_findMultiNested that)
			{
				CopyMatchContent(that);
			}
			public Match_findMultiNested()
			{
			}

			public bool IsEqual(Match_findMultiNested that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

		public class Match_findMultiNested_idpt_0 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_0>, IMatch_findMultiNested_idpt_0
		{
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_0_NodeNums { @_node0, @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_0_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "_node0": return _node__node0;
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_0_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_0_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_VariableNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_0_SubNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_0_AltNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_0_IterNums { END_OF_ENUM };
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
			
			public IMatch_findMultiNested_idpt_0_idpt_1 idpt_1 { get { return _idpt_1; } }
			public IMatch_findMultiNested_idpt_0_idpt_1 _idpt_1;
			public enum findMultiNested_idpt_0_IdptNums { @idpt_1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_IdptNums.@idpt_1: return _idpt_1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				case "idpt_1": return _idpt_1;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findMultiNested_idpt_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findMultiNested_idpt_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findMultiNested_idpt_0 cur = this;
				while(cur != null) {
					Match_findMultiNested_idpt_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findMultiNested_idpt_0 that)
			{
				_node__node0 = that._node__node0;
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
				_idpt_1 = that._idpt_1;
			}

			public Match_findMultiNested_idpt_0(Match_findMultiNested_idpt_0 that)
			{
				CopyMatchContent(that);
			}
			public Match_findMultiNested_idpt_0()
			{
			}

			public bool IsEqual(Match_findMultiNested_idpt_0 that)
			{
				if(that==null) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

		public class Match_findMultiNested_idpt_0_idpt_1 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_0_idpt_1>, IMatch_findMultiNested_idpt_0_idpt_1
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_0_idpt_1_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_idpt_1_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_0_idpt_1_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_0_idpt_1_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "_node0": return _node__node0;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_0_idpt_1_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_idpt_1_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_0_idpt_1_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_idpt_1_VariableNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_0_idpt_1_SubNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_0_idpt_1_AltNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_0_idpt_1_IterNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_0_idpt_1_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_0_idpt_1; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findMultiNested_idpt_0_idpt_1(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findMultiNested_idpt_0_idpt_1 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findMultiNested_idpt_0_idpt_1 cur = this;
				while(cur != null) {
					Match_findMultiNested_idpt_0_idpt_1 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findMultiNested_idpt_0_idpt_1 that)
			{
				_node_beg = that._node_beg;
				_node__node0 = that._node__node0;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
			}

			public Match_findMultiNested_idpt_0_idpt_1(Match_findMultiNested_idpt_0_idpt_1 that)
			{
				CopyMatchContent(that);
			}
			public Match_findMultiNested_idpt_0_idpt_1()
			{
			}

			public bool IsEqual(Match_findMultiNested_idpt_0_idpt_1 that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

		public class Match_findMultiNested_idpt_2 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_2>, IMatch_findMultiNested_idpt_2
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_2_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_2_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_2_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_2_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "_node0": return _node__node0;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_2_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_2_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_2_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_2_VariableNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_2_SubNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_2_AltNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_2_IterNums { END_OF_ENUM };
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
			
			public IMatch_findMultiNested_idpt_2_idpt_3 idpt_3 { get { return _idpt_3; } }
			public IMatch_findMultiNested_idpt_2_idpt_3 _idpt_3;
			public enum findMultiNested_idpt_2_IdptNums { @idpt_3, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_2_IdptNums.@idpt_3: return _idpt_3;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				case "idpt_3": return _idpt_3;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_2; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findMultiNested_idpt_2(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findMultiNested_idpt_2 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findMultiNested_idpt_2 cur = this;
				while(cur != null) {
					Match_findMultiNested_idpt_2 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findMultiNested_idpt_2 that)
			{
				_node_beg = that._node_beg;
				_node__node0 = that._node__node0;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
				_idpt_3 = that._idpt_3;
			}

			public Match_findMultiNested_idpt_2(Match_findMultiNested_idpt_2 that)
			{
				CopyMatchContent(that);
			}
			public Match_findMultiNested_idpt_2()
			{
			}

			public bool IsEqual(Match_findMultiNested_idpt_2 that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

		public class Match_findMultiNested_idpt_2_idpt_3 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_2_idpt_3>, IMatch_findMultiNested_idpt_2_idpt_3
		{
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_2_idpt_3_NodeNums { @_node0, @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_2_idpt_3_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_2_idpt_3_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_2_idpt_3_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "_node0": return _node__node0;
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_2_idpt_3_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_2_idpt_3_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_2_idpt_3_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_2_idpt_3_VariableNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_2_idpt_3_SubNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_2_idpt_3_AltNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_2_idpt_3_IterNums { END_OF_ENUM };
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
			
			public enum findMultiNested_idpt_2_idpt_3_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_2_idpt_3; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findMultiNested_idpt_2_idpt_3(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findMultiNested_idpt_2_idpt_3 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findMultiNested_idpt_2_idpt_3 cur = this;
				while(cur != null) {
					Match_findMultiNested_idpt_2_idpt_3 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findMultiNested_idpt_2_idpt_3 that)
			{
				_node__node0 = that._node__node0;
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
			}

			public Match_findMultiNested_idpt_2_idpt_3(Match_findMultiNested_idpt_2_idpt_3 that)
			{
				CopyMatchContent(that);
			}
			public Match_findMultiNested_idpt_2_idpt_3()
			{
			}

			public bool IsEqual(Match_findMultiNested_idpt_2_idpt_3 that)
			{
				if(that==null) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

	}

	public class Rule_createIterated : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createIterated instance = null;
		public static Rule_createIterated Instance { get { if (instance==null) { instance = new Rule_createIterated(); instance.initialize(); } return instance; } }

		public enum createIterated_NodeNums { };
		public enum createIterated_EdgeNums { };
		public enum createIterated_VariableNums { };
		public enum createIterated_SubNums { };
		public enum createIterated_AltNums { };
		public enum createIterated_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_createIterated;


		private Rule_createIterated()
		{
			name = "createIterated";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_intNode.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] createIterated_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createIterated_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] createIterated_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] createIterated_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_createIterated = new GRGEN_LGSP.PatternGraph(
				"createIterated",
				"",
				null, "createIterated",
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
				createIterated_isNodeHomomorphicGlobal,
				createIterated_isEdgeHomomorphicGlobal,
				createIterated_isNodeTotallyHomomorphic,
				createIterated_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_createIterated;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IintNode output_0, out GRGEN_LIBGR.INode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_createIterated curMatch = (Match_createIterated)_curMatch;
			graph.SettingAddedNodeNames( createIterated_addedNodeNames );
			GRGEN_MODEL.@intNode node_n1 = GRGEN_MODEL.@intNode.CreateNode(graph);
			GRGEN_MODEL.@Node node_n2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n3 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n4 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n5 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n3b = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createIterated_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n1, node_n2);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n2, node_n3);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n3, node_n4);
			GRGEN_MODEL.@Edge edge__edge3 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n4, node_n5);
			GRGEN_MODEL.@Edge edge__edge4 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n2, node_n1);
			GRGEN_MODEL.@Edge edge__edge5 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n3b, node_n2);
			GRGEN_MODEL.@Edge edge__edge6 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n4, node_n3b);
			GRGEN_MODEL.@Edge edge__edge7 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n5, node_n4);
			output_0 = (GRGEN_MODEL.IintNode)(node_n1);
			output_1 = (GRGEN_LIBGR.INode)(node_n5);
			return;
		}
		private static string[] createIterated_addedNodeNames = new string[] { "n1", "n2", "n3", "n4", "n5", "n3b" };
		private static string[] createIterated_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7" };

		static Rule_createIterated() {
		}

		public interface IMatch_createIterated : GRGEN_LIBGR.IMatch
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

		public class Match_createIterated : GRGEN_LGSP.ListElement<Match_createIterated>, IMatch_createIterated
		{
			public enum createIterated_NodeNums { END_OF_ENUM };
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
			
			public enum createIterated_EdgeNums { END_OF_ENUM };
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
			
			public enum createIterated_VariableNums { END_OF_ENUM };
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
			
			public enum createIterated_SubNums { END_OF_ENUM };
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
			
			public enum createIterated_AltNums { END_OF_ENUM };
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
			
			public enum createIterated_IterNums { END_OF_ENUM };
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
			
			public enum createIterated_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createIterated.instance.pat_createIterated; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_createIterated(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_createIterated nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_createIterated cur = this;
				while(cur != null) {
					Match_createIterated next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_createIterated that)
			{
			}

			public Match_createIterated(Match_createIterated that)
			{
				CopyMatchContent(that);
			}
			public Match_createIterated()
			{
			}

			public bool IsEqual(Match_createIterated that)
			{
				if(that==null) return false;
				return true;
			}
		}

	}

	public class Rule_findChainPlusChainToInt : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findChainPlusChainToInt instance = null;
		public static Rule_findChainPlusChainToInt Instance { get { if (instance==null) { instance = new Rule_findChainPlusChainToInt(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToInt_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToInt_node_end_AllowedTypes = null;
		public static bool[] findChainPlusChainToInt_node_beg_IsAllowedType = null;
		public static bool[] findChainPlusChainToInt_node_end_IsAllowedType = null;
		public enum findChainPlusChainToInt_NodeNums { @beg, @end, };
		public enum findChainPlusChainToInt_EdgeNums { };
		public enum findChainPlusChainToInt_VariableNums { };
		public enum findChainPlusChainToInt_SubNums { @_sub0, @_sub1, };
		public enum findChainPlusChainToInt_AltNums { };
		public enum findChainPlusChainToInt_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findChainPlusChainToInt;


		private Rule_findChainPlusChainToInt()
		{
			name = "findChainPlusChainToInt";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "findChainPlusChainToInt_node_beg", "findChainPlusChainToInt_node_end", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] findChainPlusChainToInt_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findChainPlusChainToInt_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] findChainPlusChainToInt_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] findChainPlusChainToInt_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode findChainPlusChainToInt_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findChainPlusChainToInt_node_beg", "beg", findChainPlusChainToInt_node_beg_AllowedTypes, findChainPlusChainToInt_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode findChainPlusChainToInt_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findChainPlusChainToInt_node_end", "end", findChainPlusChainToInt_node_end_AllowedTypes, findChainPlusChainToInt_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToInt__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_iteratedPath.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("findChainPlusChainToInt_node_beg"),
					new GRGEN_EXPR.GraphEntityExpression("findChainPlusChainToInt_node_end"),
				}, 
				new string[] { }, new string[] { "findChainPlusChainToInt_node_beg", "findChainPlusChainToInt_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToInt__sub1 = new GRGEN_LGSP.PatternGraphEmbedding("_sub1", Pattern_iteratedPathToIntNode.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("findChainPlusChainToInt_node_end"),
				}, 
				new string[] { }, new string[] { "findChainPlusChainToInt_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_findChainPlusChainToInt = new GRGEN_LGSP.PatternGraph(
				"findChainPlusChainToInt",
				"",
				null, "findChainPlusChainToInt",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { findChainPlusChainToInt__sub0, findChainPlusChainToInt__sub1 }, 
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
				findChainPlusChainToInt_isNodeHomomorphicGlobal,
				findChainPlusChainToInt_isEdgeHomomorphicGlobal,
				findChainPlusChainToInt_isNodeTotallyHomomorphic,
				findChainPlusChainToInt_isEdgeTotallyHomomorphic
			);

			findChainPlusChainToInt_node_beg.pointOfDefinition = null;
			findChainPlusChainToInt_node_end.pointOfDefinition = null;
			findChainPlusChainToInt__sub0.PointOfDefinition = pat_findChainPlusChainToInt;
			findChainPlusChainToInt__sub1.PointOfDefinition = pat_findChainPlusChainToInt;

			patternGraph = pat_findChainPlusChainToInt;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_findChainPlusChainToInt curMatch = (Match_findChainPlusChainToInt)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			Pattern_iteratedPath.Match_iteratedPath subpattern__sub0 = curMatch.@__sub0;
			Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode subpattern__sub1 = curMatch.@__sub1;
			return;
		}

		static Rule_findChainPlusChainToInt() {
		}

		public interface IMatch_findChainPlusChainToInt : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPath.Match_iteratedPath @_sub0 { get; }
			@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_sub1 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findChainPlusChainToInt : GRGEN_LGSP.ListElement<Match_findChainPlusChainToInt>, IMatch_findChainPlusChainToInt
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findChainPlusChainToInt_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToInt_NodeNums.@beg: return _node_beg;
				case (int)findChainPlusChainToInt_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToInt_EdgeNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToInt_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_iteratedPath.Match_iteratedPath @_sub0 { get { return @__sub0; } }
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_sub1 { get { return @__sub1; } }
			public @Pattern_iteratedPath.Match_iteratedPath @__sub0;
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @__sub1;
			public enum findChainPlusChainToInt_SubNums { @_sub0, @_sub1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 2;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToInt_SubNums.@_sub0: return __sub0;
				case (int)findChainPlusChainToInt_SubNums.@_sub1: return __sub1;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "_sub0": return __sub0;
				case "_sub1": return __sub1;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToInt_AltNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToInt_IterNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToInt_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findChainPlusChainToInt.instance.pat_findChainPlusChainToInt; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findChainPlusChainToInt(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findChainPlusChainToInt nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findChainPlusChainToInt cur = this;
				while(cur != null) {
					Match_findChainPlusChainToInt next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findChainPlusChainToInt that)
			{
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				@__sub0 = that.@__sub0;
				@__sub1 = that.@__sub1;
			}

			public Match_findChainPlusChainToInt(Match_findChainPlusChainToInt that)
			{
				CopyMatchContent(that);
			}
			public Match_findChainPlusChainToInt()
			{
			}

			public bool IsEqual(Match_findChainPlusChainToInt that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(!@__sub0.IsEqual(that.@__sub0)) return false;
				if(!@__sub1.IsEqual(that.@__sub1)) return false;
				return true;
			}
		}

	}

	public class Rule_findChainPlusChainToIntIndependent : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findChainPlusChainToIntIndependent instance = null;
		public static Rule_findChainPlusChainToIntIndependent Instance { get { if (instance==null) { instance = new Rule_findChainPlusChainToIntIndependent(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToIntIndependent_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToIntIndependent_node_end_AllowedTypes = null;
		public static bool[] findChainPlusChainToIntIndependent_node_beg_IsAllowedType = null;
		public static bool[] findChainPlusChainToIntIndependent_node_end_IsAllowedType = null;
		public enum findChainPlusChainToIntIndependent_NodeNums { @beg, @end, };
		public enum findChainPlusChainToIntIndependent_EdgeNums { };
		public enum findChainPlusChainToIntIndependent_VariableNums { };
		public enum findChainPlusChainToIntIndependent_SubNums { @_sub0, };
		public enum findChainPlusChainToIntIndependent_AltNums { };
		public enum findChainPlusChainToIntIndependent_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findChainPlusChainToIntIndependent;

		public enum findChainPlusChainToIntIndependent_idpt_0_NodeNums { @end, };
		public enum findChainPlusChainToIntIndependent_idpt_0_EdgeNums { };
		public enum findChainPlusChainToIntIndependent_idpt_0_VariableNums { };
		public enum findChainPlusChainToIntIndependent_idpt_0_SubNums { @_sub0, };
		public enum findChainPlusChainToIntIndependent_idpt_0_AltNums { };
		public enum findChainPlusChainToIntIndependent_idpt_0_IterNums { };


		public GRGEN_LGSP.PatternGraph findChainPlusChainToIntIndependent_idpt_0;


		private Rule_findChainPlusChainToIntIndependent()
		{
			name = "findChainPlusChainToIntIndependent";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "findChainPlusChainToIntIndependent_node_beg", "findChainPlusChainToIntIndependent_node_end", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] findChainPlusChainToIntIndependent_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findChainPlusChainToIntIndependent_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] findChainPlusChainToIntIndependent_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] findChainPlusChainToIntIndependent_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode findChainPlusChainToIntIndependent_node_beg = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findChainPlusChainToIntIndependent_node_beg", "beg", findChainPlusChainToIntIndependent_node_beg_AllowedTypes, findChainPlusChainToIntIndependent_node_beg_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode findChainPlusChainToIntIndependent_node_end = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "findChainPlusChainToIntIndependent_node_end", "end", findChainPlusChainToIntIndependent_node_end_AllowedTypes, findChainPlusChainToIntIndependent_node_end_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToIntIndependent__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_iteratedPath.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("findChainPlusChainToIntIndependent_node_beg"),
					new GRGEN_EXPR.GraphEntityExpression("findChainPlusChainToIntIndependent_node_end"),
				}, 
				new string[] { }, new string[] { "findChainPlusChainToIntIndependent_node_beg", "findChainPlusChainToIntIndependent_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			bool[,] findChainPlusChainToIntIndependent_idpt_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] findChainPlusChainToIntIndependent_idpt_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] findChainPlusChainToIntIndependent_idpt_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] findChainPlusChainToIntIndependent_idpt_0_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToIntIndependent_idpt_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_iteratedPathToIntNode.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("findChainPlusChainToIntIndependent_node_end"),
				}, 
				new string[] { }, new string[] { "findChainPlusChainToIntIndependent_node_end" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			findChainPlusChainToIntIndependent_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findChainPlusChainToIntIndependent_",
				null, "idpt_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findChainPlusChainToIntIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { findChainPlusChainToIntIndependent_idpt_0__sub0 }, 
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
				findChainPlusChainToIntIndependent_idpt_0_isNodeHomomorphicGlobal,
				findChainPlusChainToIntIndependent_idpt_0_isEdgeHomomorphicGlobal,
				findChainPlusChainToIntIndependent_idpt_0_isNodeTotallyHomomorphic,
				findChainPlusChainToIntIndependent_idpt_0_isEdgeTotallyHomomorphic
			);

			pat_findChainPlusChainToIntIndependent = new GRGEN_LGSP.PatternGraph(
				"findChainPlusChainToIntIndependent",
				"",
				null, "findChainPlusChainToIntIndependent",
				false, false,
				new GRGEN_LGSP.PatternNode[] { findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { findChainPlusChainToIntIndependent__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findChainPlusChainToIntIndependent_idpt_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				findChainPlusChainToIntIndependent_isNodeHomomorphicGlobal,
				findChainPlusChainToIntIndependent_isEdgeHomomorphicGlobal,
				findChainPlusChainToIntIndependent_isNodeTotallyHomomorphic,
				findChainPlusChainToIntIndependent_isEdgeTotallyHomomorphic
			);
			findChainPlusChainToIntIndependent_idpt_0.embeddingGraph = pat_findChainPlusChainToIntIndependent;

			findChainPlusChainToIntIndependent_node_beg.pointOfDefinition = null;
			findChainPlusChainToIntIndependent_node_end.pointOfDefinition = null;
			findChainPlusChainToIntIndependent__sub0.PointOfDefinition = pat_findChainPlusChainToIntIndependent;
			findChainPlusChainToIntIndependent_idpt_0__sub0.PointOfDefinition = findChainPlusChainToIntIndependent_idpt_0;

			patternGraph = pat_findChainPlusChainToIntIndependent;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_findChainPlusChainToIntIndependent curMatch = (Match_findChainPlusChainToIntIndependent)_curMatch;
			GRGEN_LGSP.LGSPNode node_beg = curMatch._node_beg;
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			Pattern_iteratedPath.Match_iteratedPath subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_findChainPlusChainToIntIndependent() {
		}

		public interface IMatch_findChainPlusChainToIntIndependent : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; set; }
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPath.Match_iteratedPath @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			IMatch_findChainPlusChainToIntIndependent_idpt_0 idpt_0 { get; }
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_findChainPlusChainToIntIndependent_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_end { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findChainPlusChainToIntIndependent : GRGEN_LGSP.ListElement<Match_findChainPlusChainToIntIndependent>, IMatch_findChainPlusChainToIntIndependent
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } set { _node_beg = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findChainPlusChainToIntIndependent_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_NodeNums.@beg: return _node_beg;
				case (int)findChainPlusChainToIntIndependent_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "beg": return _node_beg;
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_EdgeNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToIntIndependent_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_iteratedPath.Match_iteratedPath @_sub0 { get { return @__sub0; } }
			public @Pattern_iteratedPath.Match_iteratedPath @__sub0;
			public enum findChainPlusChainToIntIndependent_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_SubNums.@_sub0: return __sub0;
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
			
			public enum findChainPlusChainToIntIndependent_AltNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToIntIndependent_IterNums { END_OF_ENUM };
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
			
			public IMatch_findChainPlusChainToIntIndependent_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findChainPlusChainToIntIndependent_idpt_0 _idpt_0;
			public enum findChainPlusChainToIntIndependent_IdptNums { @idpt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_IdptNums.@idpt_0: return _idpt_0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				case "idpt_0": return _idpt_0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findChainPlusChainToIntIndependent.instance.pat_findChainPlusChainToIntIndependent; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findChainPlusChainToIntIndependent(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findChainPlusChainToIntIndependent nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findChainPlusChainToIntIndependent cur = this;
				while(cur != null) {
					Match_findChainPlusChainToIntIndependent next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findChainPlusChainToIntIndependent that)
			{
				_node_beg = that._node_beg;
				_node_end = that._node_end;
				@__sub0 = that.@__sub0;
				_idpt_0 = that._idpt_0;
			}

			public Match_findChainPlusChainToIntIndependent(Match_findChainPlusChainToIntIndependent that)
			{
				CopyMatchContent(that);
			}
			public Match_findChainPlusChainToIntIndependent()
			{
			}

			public bool IsEqual(Match_findChainPlusChainToIntIndependent that)
			{
				if(that==null) return false;
				if(_node_beg != that._node_beg) return false;
				if(_node_end != that._node_end) return false;
				if(!@__sub0.IsEqual(that.@__sub0)) return false;
				return true;
			}
		}

		public class Match_findChainPlusChainToIntIndependent_idpt_0 : GRGEN_LGSP.ListElement<Match_findChainPlusChainToIntIndependent_idpt_0>, IMatch_findChainPlusChainToIntIndependent_idpt_0
		{
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } set { _node_end = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findChainPlusChainToIntIndependent_idpt_0_NodeNums { @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "end": return _node_end;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_idpt_0_EdgeNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToIntIndependent_idpt_0_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_sub0 { get { return @__sub0; } }
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @__sub0;
			public enum findChainPlusChainToIntIndependent_idpt_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_idpt_0_SubNums.@_sub0: return __sub0;
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
			
			public enum findChainPlusChainToIntIndependent_idpt_0_AltNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToIntIndependent_idpt_0_IterNums { END_OF_ENUM };
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
			
			public enum findChainPlusChainToIntIndependent_idpt_0_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findChainPlusChainToIntIndependent.instance.findChainPlusChainToIntIndependent_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_findChainPlusChainToIntIndependent_idpt_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_findChainPlusChainToIntIndependent_idpt_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_findChainPlusChainToIntIndependent_idpt_0 cur = this;
				while(cur != null) {
					Match_findChainPlusChainToIntIndependent_idpt_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_findChainPlusChainToIntIndependent_idpt_0 that)
			{
				_node_end = that._node_end;
				@__sub0 = that.@__sub0;
			}

			public Match_findChainPlusChainToIntIndependent_idpt_0(Match_findChainPlusChainToIntIndependent_idpt_0 that)
			{
				CopyMatchContent(that);
			}
			public Match_findChainPlusChainToIntIndependent_idpt_0()
			{
			}

			public bool IsEqual(Match_findChainPlusChainToIntIndependent_idpt_0 that)
			{
				if(that==null) return false;
				if(_node_end != that._node_end) return false;
				if(!@__sub0.IsEqual(that.@__sub0)) return false;
				return true;
			}
		}

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


	//-----------------------------------------------------------

	public class Independent_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public Independent_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[2];
			rules = new GRGEN_LGSP.LGSPRulePattern[7];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[2+7];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			functions = new GRGEN_LIBGR.FunctionInfo[0+0];
			procedures = new GRGEN_LIBGR.ProcedureInfo[0+0];
			packages = new string[0];
			subpatterns[0] = Pattern_iteratedPath.Instance;
			rulesAndSubpatterns[0] = Pattern_iteratedPath.Instance;
			subpatterns[1] = Pattern_iteratedPathToIntNode.Instance;
			rulesAndSubpatterns[1] = Pattern_iteratedPathToIntNode.Instance;
			rules[0] = Rule_create.Instance;
			rulesAndSubpatterns[2+0] = Rule_create.Instance;
			rules[1] = Rule_find.Instance;
			rulesAndSubpatterns[2+1] = Rule_find.Instance;
			rules[2] = Rule_findIndependent.Instance;
			rulesAndSubpatterns[2+2] = Rule_findIndependent.Instance;
			rules[3] = Rule_findMultiNested.Instance;
			rulesAndSubpatterns[2+3] = Rule_findMultiNested.Instance;
			rules[4] = Rule_createIterated.Instance;
			rulesAndSubpatterns[2+4] = Rule_createIterated.Instance;
			rules[5] = Rule_findChainPlusChainToInt.Instance;
			rulesAndSubpatterns[2+5] = Rule_findChainPlusChainToInt.Instance;
			rules[6] = Rule_findChainPlusChainToIntIndependent.Instance;
			rulesAndSubpatterns[2+6] = Rule_findChainPlusChainToIntIndependent.Instance;
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
		public override string[] Packages { get { return packages; } }
		private string[] packages;
	}


    public class PatternAction_iteratedPath : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_iteratedPath(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_iteratedPath.Instance.patternGraph;
        }

        public static PatternAction_iteratedPath getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_iteratedPath newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_iteratedPath(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_iteratedPath oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_iteratedPath freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_iteratedPath next = null;

        public GRGEN_LGSP.LGSPNode iteratedPath_node_beg;
        public GRGEN_LGSP.LGSPNode iteratedPath_node_end;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset iteratedPath_node_beg 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg = iteratedPath_node_beg;
            // SubPreset iteratedPath_node_end 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end = iteratedPath_node_end;
            // Push alternative matching task for iteratedPath_alt_0
            AlternativeAction_iteratedPath_alt_0 taskFor_alt_0 = AlternativeAction_iteratedPath_alt_0.getNewTask(actionEnv, openTasks, Pattern_iteratedPath.Instance.patternGraph.alternatives[(int)Pattern_iteratedPath.iteratedPath_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.iteratedPath_node_beg = candidate_iteratedPath_node_beg;
            taskFor_alt_0.iteratedPath_node_end = candidate_iteratedPath_node_end;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
            // Pop alternative matching task for iteratedPath_alt_0
            openTasks.Pop();
            AlternativeAction_iteratedPath_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_iteratedPath.Match_iteratedPath match = new Pattern_iteratedPath.Match_iteratedPath();
                    match._node_beg = candidate_iteratedPath_node_beg;
                    match._node_end = candidate_iteratedPath_node_end;
                    match._alt_0 = (Pattern_iteratedPath.IMatch_iteratedPath_alt_0)currentFoundPartialMatch.Pop();
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
    
    public class AlternativeAction_iteratedPath_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_iteratedPath_alt_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_iteratedPath_alt_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_iteratedPath_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_iteratedPath_alt_0(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_iteratedPath_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_iteratedPath_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_iteratedPath_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode iteratedPath_node_beg;
        public GRGEN_LGSP.LGSPNode iteratedPath_node_end;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPath_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@base];
                // SubPreset iteratedPath_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg = iteratedPath_node_beg;
                // SubPreset iteratedPath_node_end 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end = iteratedPath_node_end;
                // Extend Outgoing iteratedPath_alt_0_base_edge__edge0 from iteratedPath_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_base_edge__edge0 = candidate_iteratedPath_node_beg.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_base_edge__edge0 = head_candidate_iteratedPath_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_base_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_iteratedPath_alt_0_base_edge__edge0.lgspTarget != candidate_iteratedPath_node_end) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                            match._node_beg = candidate_iteratedPath_node_beg;
                            match._node_end = candidate_iteratedPath_node_end;
                            match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                        prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                                match._node_beg = candidate_iteratedPath_node_beg;
                                match._node_end = candidate_iteratedPath_node_end;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0;
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
                                candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                    }
                    while( (candidate_iteratedPath_alt_0_base_edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0.lgspOutNext) != head_candidate_iteratedPath_alt_0_base_edge__edge0 );
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
            // Alternative case iteratedPath_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPath_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg = iteratedPath_node_beg;
                // SubPreset iteratedPath_node_end 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end = iteratedPath_node_end;
                // Element iteratedPath_node_end_inlined__sub0_0 assigned from other element iteratedPath_node_end 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_0 = candidate_iteratedPath_node_end;
                // Extend Outgoing iteratedPath_alt_0_recursive_edge__edge0 from iteratedPath_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_recursive_edge__edge0 = candidate_iteratedPath_node_beg.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_recursive_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_recursive_edge__edge0 = head_candidate_iteratedPath_alt_0_recursive_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPath_alt_0_recursive_node_intermediate from iteratedPath_alt_0_recursive_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPath_alt_0_recursive_node_intermediate = candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspTarget;
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0
                            && candidate_iteratedPath_alt_0_recursive_node_intermediate==candidate_iteratedPath_node_beg
                            )
                        {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Element iteratedPath_node_beg_inlined__sub0_0 assigned from other element iteratedPath_alt_0_recursive_node_intermediate 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_0 = candidate_iteratedPath_alt_0_recursive_node_intermediate;
                        // Push alternative matching task for iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive
                        AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive = AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive.getNewTask(actionEnv, openTasks, Pattern_iteratedPath.Instance.patternGraph.alternatives[(int)Pattern_iteratedPath.iteratedPath_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive.iteratedPath_node_beg_inlined__sub0_0 = candidate_iteratedPath_node_beg_inlined__sub0_0;
                        taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive.iteratedPath_node_end_inlined__sub0_0 = candidate_iteratedPath_node_end_inlined__sub0_0;
                        taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive.searchPatternpath = false;
                        taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive.matchOfNestingPattern = null;
                        taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive);
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate = candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop alternative matching task for iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive
                        openTasks.Pop();
                        AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive.releaseTask(taskFor_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive();
                                Pattern_iteratedPath.Match_iteratedPath match__sub0 = new Pattern_iteratedPath.Match_iteratedPath();
                                match__sub0.SetMatchOfEnclosingPattern(match);
                                match._node_beg = candidate_iteratedPath_node_beg;
                                match._node_intermediate = candidate_iteratedPath_alt_0_recursive_node_intermediate;
                                match._node_end = candidate_iteratedPath_node_end;
                                match__sub0._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_0;
                                match__sub0._node_end = candidate_iteratedPath_node_end_inlined__sub0_0;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0;
                                match.__sub0 = match__sub0;
                                match__sub0._alt_0 = (Pattern_iteratedPath.IMatch_iteratedPath_alt_0)currentFoundPartialMatch.Pop();
                                match__sub0._alt_0.SetMatchOfEnclosingPattern(match__sub0);
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
                                candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                                candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                            candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                    }
                    while( (candidate_iteratedPath_alt_0_recursive_edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0.lgspOutNext) != head_candidate_iteratedPath_alt_0_recursive_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    
    public class AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_iteratedPath_alt_0_recursive_alt_0_inlined__sub0_0_in_iteratedPath_alt_0_recursive next = null;

        public GRGEN_LGSP.LGSPNode iteratedPath_node_beg_inlined__sub0_0;
        public GRGEN_LGSP.LGSPNode iteratedPath_node_end_inlined__sub0_0;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPath_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@base];
                // SubPreset iteratedPath_node_beg_inlined__sub0_0 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_0 = iteratedPath_node_beg_inlined__sub0_0;
                // SubPreset iteratedPath_node_end_inlined__sub0_0 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_0 = iteratedPath_node_end_inlined__sub0_0;
                // Extend Outgoing iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0 from iteratedPath_node_beg_inlined__sub0_0 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0 = candidate_iteratedPath_node_beg_inlined__sub0_0.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0 = head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspTarget != candidate_iteratedPath_node_end_inlined__sub0_0) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                            match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_0;
                            match._node_end = candidate_iteratedPath_node_end_inlined__sub0_0;
                            match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0;
                        prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                                match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_0;
                                match._node_end = candidate_iteratedPath_node_end_inlined__sub0_0;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0;
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
                                candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0;
                    }
                    while( (candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0.lgspOutNext) != head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_0 );
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
            // Alternative case iteratedPath_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPath_node_beg_inlined__sub0_0 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_0 = iteratedPath_node_beg_inlined__sub0_0;
                // SubPreset iteratedPath_node_end_inlined__sub0_0 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_0 = iteratedPath_node_end_inlined__sub0_0;
                // Extend Outgoing iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 from iteratedPath_node_beg_inlined__sub0_0 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 = candidate_iteratedPath_node_beg_inlined__sub0_0.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 = head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0 from iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspTarget;
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0
                            && candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0==candidate_iteratedPath_node_beg_inlined__sub0_0
                            )
                        {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0_inlined__sub0_0
                        PatternAction_iteratedPath taskFor__sub0_inlined__sub0_0 = PatternAction_iteratedPath.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_0.iteratedPath_node_beg = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0;
                        taskFor__sub0_inlined__sub0_0.iteratedPath_node_end = candidate_iteratedPath_node_end_inlined__sub0_0;
                        taskFor__sub0_inlined__sub0_0.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_0.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_0);
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0 = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop subpattern matching task for _sub0_inlined__sub0_0
                        openTasks.Pop();
                        PatternAction_iteratedPath.releaseTask(taskFor__sub0_inlined__sub0_0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive();
                                match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_0;
                                match._node_intermediate = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0;
                                match._node_end = candidate_iteratedPath_node_end_inlined__sub0_0;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0;
                                match.__sub0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
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
                                candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0;
                                candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0;
                            candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_0;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0;
                    }
                    while( (candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0.lgspOutNext) != head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    
    public class PatternAction_iteratedPathToIntNode : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_iteratedPathToIntNode(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_iteratedPathToIntNode.Instance.patternGraph;
        }

        public static PatternAction_iteratedPathToIntNode getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_iteratedPathToIntNode newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_iteratedPathToIntNode(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_iteratedPathToIntNode oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_iteratedPathToIntNode freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_iteratedPathToIntNode next = null;

        public GRGEN_LGSP.LGSPNode iteratedPathToIntNode_node_beg;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset iteratedPathToIntNode_node_beg 
            GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg = iteratedPathToIntNode_node_beg;
            // Push alternative matching task for iteratedPathToIntNode_alt_0
            AlternativeAction_iteratedPathToIntNode_alt_0 taskFor_alt_0 = AlternativeAction_iteratedPathToIntNode_alt_0.getNewTask(actionEnv, openTasks, Pattern_iteratedPathToIntNode.Instance.patternGraph.alternatives[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.iteratedPathToIntNode_node_beg = candidate_iteratedPathToIntNode_node_beg;
            taskFor_alt_0.searchPatternpath = false;
            taskFor_alt_0.matchOfNestingPattern = null;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
            // Pop alternative matching task for iteratedPathToIntNode_alt_0
            openTasks.Pop();
            AlternativeAction_iteratedPathToIntNode_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode();
                    match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                    match._alt_0 = (Pattern_iteratedPathToIntNode.IMatch_iteratedPathToIntNode_alt_0)currentFoundPartialMatch.Pop();
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
    
    public class AlternativeAction_iteratedPathToIntNode_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_iteratedPathToIntNode_alt_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_iteratedPathToIntNode_alt_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_iteratedPathToIntNode_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_iteratedPathToIntNode_alt_0(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_iteratedPathToIntNode_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_iteratedPathToIntNode_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_iteratedPathToIntNode_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode iteratedPathToIntNode_node_beg;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPathToIntNode_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@base];
                // SubPreset iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg = iteratedPathToIntNode_node_beg;
                // Extend Outgoing iteratedPathToIntNode_alt_0_base_edge__edge0 from iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = candidate_iteratedPathToIntNode_node_beg.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_base_node_end from iteratedPathToIntNode_alt_0_base_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_base_node_end = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                            match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                            match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end;
                            match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                                match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
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
                                candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                                candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                            candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 );
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
            // Alternative case iteratedPathToIntNode_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg = iteratedPathToIntNode_node_beg;
                // Extend Outgoing iteratedPathToIntNode_alt_0_recursive_edge__edge0 from iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = candidate_iteratedPathToIntNode_node_beg.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_recursive_node_intermediate from iteratedPathToIntNode_alt_0_recursive_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspType.TypeID!=0) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Element iteratedPathToIntNode_node_beg_inlined__sub0_1 assigned from other element iteratedPathToIntNode_alt_0_recursive_node_intermediate 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                        // Push alternative matching task for iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive
                        AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive taskFor_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive = AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive.getNewTask(actionEnv, openTasks, Pattern_iteratedPathToIntNode.Instance.patternGraph.alternatives[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive.iteratedPathToIntNode_node_beg_inlined__sub0_1 = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1;
                        taskFor_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive.searchPatternpath = false;
                        taskFor_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive.matchOfNestingPattern = null;
                        taskFor_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive);
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop alternative matching task for iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive
                        openTasks.Pop();
                        AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive.releaseTask(taskFor_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive();
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode match__sub0 = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode();
                                match__sub0.SetMatchOfEnclosingPattern(match);
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                                match._node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                                match__sub0._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                                match.__sub0 = match__sub0;
                                match__sub0._alt_0 = (Pattern_iteratedPathToIntNode.IMatch_iteratedPathToIntNode_alt_0)currentFoundPartialMatch.Pop();
                                match__sub0._alt_0.SetMatchOfEnclosingPattern(match__sub0);
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
                                candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                                candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                            candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    
    public class AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_iteratedPathToIntNode_alt_0_recursive_alt_0_inlined__sub0_1_in_iteratedPathToIntNode_alt_0_recursive next = null;

        public GRGEN_LGSP.LGSPNode iteratedPathToIntNode_node_beg_inlined__sub0_1;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPathToIntNode_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@base];
                // SubPreset iteratedPathToIntNode_node_beg_inlined__sub0_1 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1 = iteratedPathToIntNode_node_beg_inlined__sub0_1;
                // Extend Outgoing iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 from iteratedPathToIntNode_node_beg_inlined__sub0_1 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 = head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1 from iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                            match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1;
                            match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1;
                            match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1;
                                match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1;
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
                                candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1;
                                candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1;
                            candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_1;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_1 );
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
            // Alternative case iteratedPathToIntNode_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPathToIntNode_node_beg_inlined__sub0_1 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1 = iteratedPathToIntNode_node_beg_inlined__sub0_1;
                // Extend Outgoing iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 from iteratedPathToIntNode_node_beg_inlined__sub0_1 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 = head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1 from iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspType.TypeID!=0) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0_inlined__sub0_1
                        PatternAction_iteratedPathToIntNode taskFor__sub0_inlined__sub0_1 = PatternAction_iteratedPathToIntNode.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_1.iteratedPathToIntNode_node_beg = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1;
                        taskFor__sub0_inlined__sub0_1.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_1.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_1.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_1);
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop subpattern matching task for _sub0_inlined__sub0_1
                        openTasks.Pop();
                        PatternAction_iteratedPathToIntNode.releaseTask(taskFor__sub0_inlined__sub0_1);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_1;
                                match._node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1;
                                match.__sub0 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
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
                                candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1;
                                candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1;
                            candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_1;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_1 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_create
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_create.IMatch_create match, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> matches, List<GRGEN_LIBGR.INode> output_0, List<GRGEN_LIBGR.INode> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_LIBGR.INode> output_0, List<GRGEN_LIBGR.INode> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_create : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_create
    {
        public Action_create() {
            _rulePattern = Rule_create.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_create.Match_create, Rule_create.IMatch_create>(this);
        }

        public Rule_create _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "create"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_create.Match_create, Rule_create.IMatch_create> matches;

        public static Action_create Instance { get { return instance; } set { instance = value; } }
        private static Action_create instance = new Action_create();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            Rule_create.Match_create match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_LIBGR.INode> output_list_0 = new List<GRGEN_LIBGR.INode>();
        List<GRGEN_LIBGR.INode> output_list_1 = new List<GRGEN_LIBGR.INode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_create.IMatch_create match, out GRGEN_LIBGR.INode output_0, out GRGEN_LIBGR.INode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> matches, List<GRGEN_LIBGR.INode> output_0, List<GRGEN_LIBGR.INode> output_1)
        {
            foreach(Rule_create.IMatch_create match in matches)
            {
                GRGEN_LIBGR.INode output_local_0; GRGEN_LIBGR.INode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0, ref GRGEN_LIBGR.INode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_LIBGR.INode> output_0, List<GRGEN_LIBGR.INode> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_create.IMatch_create match in matches)
            {
                GRGEN_LIBGR.INode output_local_0; GRGEN_LIBGR.INode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> matches;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create> matches;
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
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
            GRGEN_LIBGR.INode output_0; GRGEN_LIBGR.INode output_1; 
            Modify(actionEnv, (Rule_create.IMatch_create)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_create.IMatch_create>)matches, output_list_0, output_list_1);
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
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(actionEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(actionEnv, ref output_0, ref output_1)) {
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
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0, output_list_1);
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
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0, output_list_1);
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
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_find
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_find.IMatch_find match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> matches);
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
    
    public class Action_find : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_find
    {
        public Action_find() {
            _rulePattern = Rule_find.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_find.Match_find, Rule_find.IMatch_find>(this);
        }

        public Rule_find _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "find"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_find.Match_find, Rule_find.IMatch_find> matches;

        public static Action_find Instance { get { return instance; } set { instance = value; } }
        private static Action_find instance = new Action_find();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup find_edge__edge0 
            int type_id_candidate_find_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_find_edge__edge0], candidate_find_edge__edge0 = head_candidate_find_edge__edge0.lgspTypeNext; candidate_find_edge__edge0 != head_candidate_find_edge__edge0; candidate_find_edge__edge0 = candidate_find_edge__edge0.lgspTypeNext)
            {
                uint prev__candidate_find_edge__edge0;
                prev__candidate_find_edge__edge0 = candidate_find_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_find_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Source find_node_beg from find_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_find_node_beg = candidate_find_edge__edge0.lgspSource;
                uint prev__candidate_find_node_beg;
                prev__candidate_find_node_beg = candidate_find_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_find_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Target find_node__node0 from find_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_find_node__node0 = candidate_find_edge__edge0.lgspTarget;
                if((candidate_find_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                {
                    candidate_find_node_beg.lgspFlags = candidate_find_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_node_beg;
                    candidate_find_edge__edge0.lgspFlags = candidate_find_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge0;
                    continue;
                }
                uint prev__candidate_find_node__node0;
                prev__candidate_find_node__node0 = candidate_find_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_find_node__node0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Extend Outgoing find_edge__edge1 from find_node__node0 
                GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge1 = candidate_find_node__node0.lgspOuthead;
                if(head_candidate_find_edge__edge1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_find_edge__edge1 = head_candidate_find_edge__edge1;
                    do
                    {
                        if(candidate_find_edge__edge1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_find_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        uint prev__candidate_find_edge__edge1;
                        prev__candidate_find_edge__edge1 = candidate_find_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                        candidate_find_edge__edge1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                        // Implicit Target find_node_end from find_edge__edge1 
                        GRGEN_LGSP.LGSPNode candidate_find_node_end = candidate_find_edge__edge1.lgspTarget;
                        if((candidate_find_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            candidate_find_edge__edge1.lgspFlags = candidate_find_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge1;
                            continue;
                        }
                        uint prev__candidate_find_node_end;
                        prev__candidate_find_node_end = candidate_find_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                        candidate_find_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                        // Extend Outgoing find_edge__edge3 from find_node_end 
                        GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge3 = candidate_find_node_end.lgspOuthead;
                        if(head_candidate_find_edge__edge3 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_find_edge__edge3 = head_candidate_find_edge__edge3;
                            do
                            {
                                if(candidate_find_edge__edge3.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_find_edge__edge3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                {
                                    continue;
                                }
                                uint prev__candidate_find_edge__edge3;
                                prev__candidate_find_edge__edge3 = candidate_find_edge__edge3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                candidate_find_edge__edge3.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                // Implicit Target find_node__node1 from find_edge__edge3 
                                GRGEN_LGSP.LGSPNode candidate_find_node__node1 = candidate_find_edge__edge3.lgspTarget;
                                if((candidate_find_node__node1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                {
                                    candidate_find_edge__edge3.lgspFlags = candidate_find_edge__edge3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge3;
                                    continue;
                                }
                                // Extend Outgoing find_edge__edge2 from find_node__node1 
                                GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge2 = candidate_find_node__node1.lgspOuthead;
                                if(head_candidate_find_edge__edge2 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_find_edge__edge2 = head_candidate_find_edge__edge2;
                                    do
                                    {
                                        if(candidate_find_edge__edge2.lgspType.TypeID!=1) {
                                            continue;
                                        }
                                        if(candidate_find_edge__edge2.lgspTarget != candidate_find_node_beg) {
                                            continue;
                                        }
                                        if((candidate_find_edge__edge2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                        {
                                            continue;
                                        }
                                        Rule_find.Match_find match = matches.GetNextUnfilledPosition();
                                        match._node_beg = candidate_find_node_beg;
                                        match._node__node0 = candidate_find_node__node0;
                                        match._node_end = candidate_find_node_end;
                                        match._node__node1 = candidate_find_node__node1;
                                        match._edge__edge0 = candidate_find_edge__edge0;
                                        match._edge__edge1 = candidate_find_edge__edge1;
                                        match._edge__edge2 = candidate_find_edge__edge2;
                                        match._edge__edge3 = candidate_find_edge__edge3;
                                        matches.PositionWasFilledFixIt();
                                        // if enough matches were found, we leave
                                        if(maxMatches > 0 && matches.Count >= maxMatches)
                                        {
                                            candidate_find_node__node1.MoveOutHeadAfter(candidate_find_edge__edge2);
                                            candidate_find_node_end.MoveOutHeadAfter(candidate_find_edge__edge3);
                                            candidate_find_node__node0.MoveOutHeadAfter(candidate_find_edge__edge1);
                                            graph.MoveHeadAfter(candidate_find_edge__edge0);
                                            candidate_find_edge__edge3.lgspFlags = candidate_find_edge__edge3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge3;
                                            candidate_find_node_end.lgspFlags = candidate_find_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_node_end;
                                            candidate_find_edge__edge1.lgspFlags = candidate_find_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge1;
                                            candidate_find_node__node0.lgspFlags = candidate_find_node__node0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_node__node0;
                                            candidate_find_node_beg.lgspFlags = candidate_find_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_node_beg;
                                            candidate_find_edge__edge0.lgspFlags = candidate_find_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge0;
                                            return matches;
                                        }
                                    }
                                    while( (candidate_find_edge__edge2 = candidate_find_edge__edge2.lgspOutNext) != head_candidate_find_edge__edge2 );
                                }
                                candidate_find_edge__edge3.lgspFlags = candidate_find_edge__edge3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge3;
                            }
                            while( (candidate_find_edge__edge3 = candidate_find_edge__edge3.lgspOutNext) != head_candidate_find_edge__edge3 );
                        }
                        candidate_find_node_end.lgspFlags = candidate_find_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_node_end;
                        candidate_find_edge__edge1.lgspFlags = candidate_find_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge1;
                    }
                    while( (candidate_find_edge__edge1 = candidate_find_edge__edge1.lgspOutNext) != head_candidate_find_edge__edge1 );
                }
                candidate_find_node__node0.lgspFlags = candidate_find_node__node0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_node__node0;
                candidate_find_node_beg.lgspFlags = candidate_find_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_node_beg;
                candidate_find_edge__edge0.lgspFlags = candidate_find_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_find_edge__edge0;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_find.IMatch_find match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> matches)
        {
            foreach(Rule_find.IMatch_find match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_find.IMatch_find match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find> matches;
            
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
            
            Modify(actionEnv, (Rule_find.IMatch_find)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_find.IMatch_find>)matches);
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
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findIndependent
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findIndependent.IMatch_findIndependent match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> matches);
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
    
    public class Action_findIndependent : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findIndependent
    {
        public Action_findIndependent() {
            _rulePattern = Rule_findIndependent.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findIndependent.Match_findIndependent, Rule_findIndependent.IMatch_findIndependent>(this);
        }

        public Rule_findIndependent _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findIndependent"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findIndependent.Match_findIndependent, Rule_findIndependent.IMatch_findIndependent> matches;

        public static Action_findIndependent Instance { get { return instance; } set { instance = value; } }
        private static Action_findIndependent instance = new Action_findIndependent();
        private Rule_findIndependent.Match_findIndependent_idpt_0 matched_independent_findIndependent_idpt_0 = new Rule_findIndependent.Match_findIndependent_idpt_0();        
        public GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup findIndependent_edge__edge0 
            int type_id_candidate_findIndependent_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findIndependent_edge__edge0], candidate_findIndependent_edge__edge0 = head_candidate_findIndependent_edge__edge0.lgspTypeNext; candidate_findIndependent_edge__edge0 != head_candidate_findIndependent_edge__edge0; candidate_findIndependent_edge__edge0 = candidate_findIndependent_edge__edge0.lgspTypeNext)
            {
                uint prev__candidate_findIndependent_edge__edge0;
                prev__candidate_findIndependent_edge__edge0 = candidate_findIndependent_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_findIndependent_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Source findIndependent_node_beg from findIndependent_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findIndependent_node_beg = candidate_findIndependent_edge__edge0.lgspSource;
                uint prev__candidate_findIndependent_node_beg;
                prev__candidate_findIndependent_node_beg = candidate_findIndependent_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_findIndependent_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Target findIndependent_node__node0 from findIndependent_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findIndependent_node__node0 = candidate_findIndependent_edge__edge0.lgspTarget;
                if((candidate_findIndependent_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                {
                    candidate_findIndependent_node_beg.lgspFlags = candidate_findIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_node_beg;
                    candidate_findIndependent_edge__edge0.lgspFlags = candidate_findIndependent_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_edge__edge0;
                    continue;
                }
                uint prev__candidate_findIndependent_node__node0;
                prev__candidate_findIndependent_node__node0 = candidate_findIndependent_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_findIndependent_node__node0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Extend Outgoing findIndependent_edge__edge1 from findIndependent_node__node0 
                GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_edge__edge1 = candidate_findIndependent_node__node0.lgspOuthead;
                if(head_candidate_findIndependent_edge__edge1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_findIndependent_edge__edge1 = head_candidate_findIndependent_edge__edge1;
                    do
                    {
                        if(candidate_findIndependent_edge__edge1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_findIndependent_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        // Implicit Target findIndependent_node_end from findIndependent_edge__edge1 
                        GRGEN_LGSP.LGSPNode candidate_findIndependent_node_end = candidate_findIndependent_edge__edge1.lgspTarget;
                        if((candidate_findIndependent_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        // IndependentPattern 
                        {
                            ++isoSpace;
                            uint prev_idpt_0__candidate_findIndependent_node_beg;
                            prev_idpt_0__candidate_findIndependent_node_beg = candidate_findIndependent_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            candidate_findIndependent_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            if((candidate_findIndependent_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                            {
                                candidate_findIndependent_node_beg.lgspFlags = candidate_findIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_node_beg;
                                --isoSpace;
                                goto label0;
                            }
                            uint prev_idpt_0__candidate_findIndependent_node_end;
                            prev_idpt_0__candidate_findIndependent_node_end = candidate_findIndependent_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            candidate_findIndependent_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            // Extend Outgoing findIndependent_idpt_0_edge__edge1 from findIndependent_node_end 
                            GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_idpt_0_edge__edge1 = candidate_findIndependent_node_end.lgspOuthead;
                            if(head_candidate_findIndependent_idpt_0_edge__edge1 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_findIndependent_idpt_0_edge__edge1 = head_candidate_findIndependent_idpt_0_edge__edge1;
                                do
                                {
                                    if(candidate_findIndependent_idpt_0_edge__edge1.lgspType.TypeID!=1) {
                                        continue;
                                    }
                                    uint prev_idpt_0__candidate_findIndependent_idpt_0_edge__edge1;
                                    prev_idpt_0__candidate_findIndependent_idpt_0_edge__edge1 = candidate_findIndependent_idpt_0_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                    candidate_findIndependent_idpt_0_edge__edge1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                    // Implicit Target findIndependent_idpt_0_node__node0 from findIndependent_idpt_0_edge__edge1 
                                    GRGEN_LGSP.LGSPNode candidate_findIndependent_idpt_0_node__node0 = candidate_findIndependent_idpt_0_edge__edge1.lgspTarget;
                                    if((candidate_findIndependent_idpt_0_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                    {
                                        candidate_findIndependent_idpt_0_edge__edge1.lgspFlags = candidate_findIndependent_idpt_0_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_idpt_0_edge__edge1;
                                        continue;
                                    }
                                    // Extend Outgoing findIndependent_idpt_0_edge__edge0 from findIndependent_idpt_0_node__node0 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_idpt_0_edge__edge0 = candidate_findIndependent_idpt_0_node__node0.lgspOuthead;
                                    if(head_candidate_findIndependent_idpt_0_edge__edge0 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findIndependent_idpt_0_edge__edge0 = head_candidate_findIndependent_idpt_0_edge__edge0;
                                        do
                                        {
                                            if(candidate_findIndependent_idpt_0_edge__edge0.lgspType.TypeID!=1) {
                                                continue;
                                            }
                                            if(candidate_findIndependent_idpt_0_edge__edge0.lgspTarget != candidate_findIndependent_node_beg) {
                                                continue;
                                            }
                                            if((candidate_findIndependent_idpt_0_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                            {
                                                continue;
                                            }
                                            // independent pattern found
                                            matched_independent_findIndependent_idpt_0._node__node0 = candidate_findIndependent_idpt_0_node__node0;
                                            matched_independent_findIndependent_idpt_0._node_beg = candidate_findIndependent_node_beg;
                                            matched_independent_findIndependent_idpt_0._node_end = candidate_findIndependent_node_end;
                                            matched_independent_findIndependent_idpt_0._edge__edge0 = candidate_findIndependent_idpt_0_edge__edge0;
                                            matched_independent_findIndependent_idpt_0._edge__edge1 = candidate_findIndependent_idpt_0_edge__edge1;
                                            candidate_findIndependent_idpt_0_edge__edge1.lgspFlags = candidate_findIndependent_idpt_0_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_idpt_0_edge__edge1;
                                            candidate_findIndependent_node_end.lgspFlags = candidate_findIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_node_end;
                                            candidate_findIndependent_node_beg.lgspFlags = candidate_findIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_node_beg;
                                            --isoSpace;
                                            goto label1;
                                        }
                                        while( (candidate_findIndependent_idpt_0_edge__edge0 = candidate_findIndependent_idpt_0_edge__edge0.lgspOutNext) != head_candidate_findIndependent_idpt_0_edge__edge0 );
                                    }
                                    candidate_findIndependent_idpt_0_edge__edge1.lgspFlags = candidate_findIndependent_idpt_0_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_idpt_0_edge__edge1;
                                }
                                while( (candidate_findIndependent_idpt_0_edge__edge1 = candidate_findIndependent_idpt_0_edge__edge1.lgspOutNext) != head_candidate_findIndependent_idpt_0_edge__edge1 );
                            }
                            candidate_findIndependent_node_end.lgspFlags = candidate_findIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_node_end;
                            candidate_findIndependent_node_beg.lgspFlags = candidate_findIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findIndependent_node_beg;
                            --isoSpace;
                        }
label0: ;
                        goto label2;
label1: ;
                        Rule_findIndependent.Match_findIndependent match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_findIndependent_node_beg;
                        match._node__node0 = candidate_findIndependent_node__node0;
                        match._node_end = candidate_findIndependent_node_end;
                        match._edge__edge0 = candidate_findIndependent_edge__edge0;
                        match._edge__edge1 = candidate_findIndependent_edge__edge1;
                        match._idpt_0 = matched_independent_findIndependent_idpt_0;
                        matched_independent_findIndependent_idpt_0 = new Rule_findIndependent.Match_findIndependent_idpt_0(matched_independent_findIndependent_idpt_0);
                        match._idpt_0.SetMatchOfEnclosingPattern(match);
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findIndependent_node__node0.MoveOutHeadAfter(candidate_findIndependent_edge__edge1);
                            graph.MoveHeadAfter(candidate_findIndependent_edge__edge0);
                            candidate_findIndependent_node__node0.lgspFlags = candidate_findIndependent_node__node0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_node__node0;
                            candidate_findIndependent_node_beg.lgspFlags = candidate_findIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_node_beg;
                            candidate_findIndependent_edge__edge0.lgspFlags = candidate_findIndependent_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_edge__edge0;
                            return matches;
                        }
label2: ;
                    }
                    while( (candidate_findIndependent_edge__edge1 = candidate_findIndependent_edge__edge1.lgspOutNext) != head_candidate_findIndependent_edge__edge1 );
                }
                candidate_findIndependent_node__node0.lgspFlags = candidate_findIndependent_node__node0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_node__node0;
                candidate_findIndependent_node_beg.lgspFlags = candidate_findIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_node_beg;
                candidate_findIndependent_edge__edge0.lgspFlags = candidate_findIndependent_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findIndependent_edge__edge0;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findIndependent.IMatch_findIndependent match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> matches)
        {
            foreach(Rule_findIndependent.IMatch_findIndependent match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_findIndependent.IMatch_findIndependent match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent> matches;
            
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
            
            Modify(actionEnv, (Rule_findIndependent.IMatch_findIndependent)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_findIndependent.IMatch_findIndependent>)matches);
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
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findMultiNested
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findMultiNested.IMatch_findMultiNested match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> matches);
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
    
    public class Action_findMultiNested : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findMultiNested
    {
        public Action_findMultiNested() {
            _rulePattern = Rule_findMultiNested.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findMultiNested.Match_findMultiNested, Rule_findMultiNested.IMatch_findMultiNested>(this);
        }

        public Rule_findMultiNested _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findMultiNested"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findMultiNested.Match_findMultiNested, Rule_findMultiNested.IMatch_findMultiNested> matches;

        public static Action_findMultiNested Instance { get { return instance; } set { instance = value; } }
        private static Action_findMultiNested instance = new Action_findMultiNested();
        private Rule_findMultiNested.Match_findMultiNested_idpt_0 matched_independent_findMultiNested_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_0();        private Rule_findMultiNested.Match_findMultiNested_idpt_2 matched_independent_findMultiNested_idpt_2 = new Rule_findMultiNested.Match_findMultiNested_idpt_2();        private Rule_findMultiNested.Match_findMultiNested_idpt_0_idpt_1 matched_independent_findMultiNested_idpt_0_idpt_1 = new Rule_findMultiNested.Match_findMultiNested_idpt_0_idpt_1();        private Rule_findMultiNested.Match_findMultiNested_idpt_2_idpt_3 matched_independent_findMultiNested_idpt_2_idpt_3 = new Rule_findMultiNested.Match_findMultiNested_idpt_2_idpt_3();        
        public GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup findMultiNested_edge__edge0 
            int type_id_candidate_findMultiNested_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findMultiNested_edge__edge0], candidate_findMultiNested_edge__edge0 = head_candidate_findMultiNested_edge__edge0.lgspTypeNext; candidate_findMultiNested_edge__edge0 != head_candidate_findMultiNested_edge__edge0; candidate_findMultiNested_edge__edge0 = candidate_findMultiNested_edge__edge0.lgspTypeNext)
            {
                uint prev__candidate_findMultiNested_edge__edge0;
                prev__candidate_findMultiNested_edge__edge0 = candidate_findMultiNested_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_findMultiNested_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Source findMultiNested_node_beg from findMultiNested_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findMultiNested_node_beg = candidate_findMultiNested_edge__edge0.lgspSource;
                uint prev__candidate_findMultiNested_node_beg;
                prev__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_findMultiNested_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Target findMultiNested_node__node0 from findMultiNested_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findMultiNested_node__node0 = candidate_findMultiNested_edge__edge0.lgspTarget;
                if((candidate_findMultiNested_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                {
                    candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_node_beg;
                    candidate_findMultiNested_edge__edge0.lgspFlags = candidate_findMultiNested_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_edge__edge0;
                    continue;
                }
                uint prev__candidate_findMultiNested_node__node0;
                prev__candidate_findMultiNested_node__node0 = candidate_findMultiNested_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_findMultiNested_node__node0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Extend Outgoing findMultiNested_edge__edge1 from findMultiNested_node__node0 
                GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_edge__edge1 = candidate_findMultiNested_node__node0.lgspOuthead;
                if(head_candidate_findMultiNested_edge__edge1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_findMultiNested_edge__edge1 = head_candidate_findMultiNested_edge__edge1;
                    do
                    {
                        if(candidate_findMultiNested_edge__edge1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_findMultiNested_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        // Implicit Target findMultiNested_node_end from findMultiNested_edge__edge1 
                        GRGEN_LGSP.LGSPNode candidate_findMultiNested_node_end = candidate_findMultiNested_edge__edge1.lgspTarget;
                        if((candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        // IndependentPattern 
                        {
                            ++isoSpace;
                            uint prev_idpt_0__candidate_findMultiNested_node_beg;
                            prev_idpt_0__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            candidate_findMultiNested_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            if((candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                            {
                                candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_node_beg;
                                --isoSpace;
                                goto label3;
                            }
                            uint prev_idpt_0__candidate_findMultiNested_node_end;
                            prev_idpt_0__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            candidate_findMultiNested_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            // IndependentPattern 
                            {
                                ++isoSpace;
                                uint prev_idpt_0idpt_1__candidate_findMultiNested_node_beg;
                                prev_idpt_0idpt_1__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                candidate_findMultiNested_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                if((candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                {
                                    candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_node_beg;
                                    --isoSpace;
                                    goto label4;
                                }
                                uint prev_idpt_0idpt_1__candidate_findMultiNested_node_end;
                                prev_idpt_0idpt_1__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                candidate_findMultiNested_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                // Extend Outgoing findMultiNested_idpt_0_idpt_1_edge__edge0 from findMultiNested_node_beg 
                                GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge0 = candidate_findMultiNested_node_beg.lgspOuthead;
                                if(head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge0 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_idpt_1_edge__edge0 = head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge0;
                                    do
                                    {
                                        if(candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspType.TypeID!=1) {
                                            continue;
                                        }
                                        uint prev_idpt_0idpt_1__candidate_findMultiNested_idpt_0_idpt_1_edge__edge0;
                                        prev_idpt_0idpt_1__candidate_findMultiNested_idpt_0_idpt_1_edge__edge0 = candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                        candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                        // Implicit Target findMultiNested_idpt_0_idpt_1_node__node0 from findMultiNested_idpt_0_idpt_1_edge__edge0 
                                        GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_0_idpt_1_node__node0 = candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspTarget;
                                        if((candidate_findMultiNested_idpt_0_idpt_1_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                        {
                                            candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags = candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_idpt_0_idpt_1_edge__edge0;
                                            continue;
                                        }
                                        // Extend Outgoing findMultiNested_idpt_0_idpt_1_edge__edge1 from findMultiNested_idpt_0_idpt_1_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge1 = candidate_findMultiNested_idpt_0_idpt_1_node__node0.lgspOuthead;
                                        if(head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge1 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_idpt_1_edge__edge1 = head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge1;
                                            do
                                            {
                                                if(candidate_findMultiNested_idpt_0_idpt_1_edge__edge1.lgspType.TypeID!=1) {
                                                    continue;
                                                }
                                                if(candidate_findMultiNested_idpt_0_idpt_1_edge__edge1.lgspTarget != candidate_findMultiNested_node_end) {
                                                    continue;
                                                }
                                                if((candidate_findMultiNested_idpt_0_idpt_1_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                                {
                                                    continue;
                                                }
                                                // independent pattern found
                                                matched_independent_findMultiNested_idpt_0_idpt_1._node_beg = candidate_findMultiNested_node_beg;
                                                matched_independent_findMultiNested_idpt_0_idpt_1._node__node0 = candidate_findMultiNested_idpt_0_idpt_1_node__node0;
                                                matched_independent_findMultiNested_idpt_0_idpt_1._node_end = candidate_findMultiNested_node_end;
                                                matched_independent_findMultiNested_idpt_0_idpt_1._edge__edge0 = candidate_findMultiNested_idpt_0_idpt_1_edge__edge0;
                                                matched_independent_findMultiNested_idpt_0_idpt_1._edge__edge1 = candidate_findMultiNested_idpt_0_idpt_1_edge__edge1;
                                                candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags = candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_idpt_0_idpt_1_edge__edge0;
                                                candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_node_end;
                                                candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_node_beg;
                                                --isoSpace;
                                                goto label5;
                                            }
                                            while( (candidate_findMultiNested_idpt_0_idpt_1_edge__edge1 = candidate_findMultiNested_idpt_0_idpt_1_edge__edge1.lgspOutNext) != head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge1 );
                                        }
                                        candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags = candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_idpt_0_idpt_1_edge__edge0;
                                    }
                                    while( (candidate_findMultiNested_idpt_0_idpt_1_edge__edge0 = candidate_findMultiNested_idpt_0_idpt_1_edge__edge0.lgspOutNext) != head_candidate_findMultiNested_idpt_0_idpt_1_edge__edge0 );
                                }
                                candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_node_end;
                                candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0idpt_1__candidate_findMultiNested_node_beg;
                                --isoSpace;
                            }
label4: ;
                            candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_node_beg;
                            --isoSpace;
                            goto label6;
label5: ;
                            // Extend Outgoing findMultiNested_idpt_0_edge__edge1 from findMultiNested_node_end 
                            GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_edge__edge1 = candidate_findMultiNested_node_end.lgspOuthead;
                            if(head_candidate_findMultiNested_idpt_0_edge__edge1 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_edge__edge1 = head_candidate_findMultiNested_idpt_0_edge__edge1;
                                do
                                {
                                    if(candidate_findMultiNested_idpt_0_edge__edge1.lgspType.TypeID!=1) {
                                        continue;
                                    }
                                    uint prev_idpt_0__candidate_findMultiNested_idpt_0_edge__edge1;
                                    prev_idpt_0__candidate_findMultiNested_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                    candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                    // Implicit Target findMultiNested_idpt_0_node__node0 from findMultiNested_idpt_0_edge__edge1 
                                    GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_0_node__node0 = candidate_findMultiNested_idpt_0_edge__edge1.lgspTarget;
                                    if((candidate_findMultiNested_idpt_0_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                    {
                                        candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags = candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_idpt_0_edge__edge1;
                                        continue;
                                    }
                                    // Extend Outgoing findMultiNested_idpt_0_edge__edge0 from findMultiNested_idpt_0_node__node0 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_0_node__node0.lgspOuthead;
                                    if(head_candidate_findMultiNested_idpt_0_edge__edge0 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_edge__edge0 = head_candidate_findMultiNested_idpt_0_edge__edge0;
                                        do
                                        {
                                            if(candidate_findMultiNested_idpt_0_edge__edge0.lgspType.TypeID!=1) {
                                                continue;
                                            }
                                            if(candidate_findMultiNested_idpt_0_edge__edge0.lgspTarget != candidate_findMultiNested_node_beg) {
                                                continue;
                                            }
                                            if((candidate_findMultiNested_idpt_0_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                            {
                                                continue;
                                            }
                                            // independent pattern found
                                            matched_independent_findMultiNested_idpt_0._node__node0 = candidate_findMultiNested_idpt_0_node__node0;
                                            matched_independent_findMultiNested_idpt_0._node_beg = candidate_findMultiNested_node_beg;
                                            matched_independent_findMultiNested_idpt_0._node_end = candidate_findMultiNested_node_end;
                                            matched_independent_findMultiNested_idpt_0._edge__edge0 = candidate_findMultiNested_idpt_0_edge__edge0;
                                            matched_independent_findMultiNested_idpt_0._edge__edge1 = candidate_findMultiNested_idpt_0_edge__edge1;
                                            matched_independent_findMultiNested_idpt_0._idpt_1 = matched_independent_findMultiNested_idpt_0_idpt_1;
                                            matched_independent_findMultiNested_idpt_0_idpt_1 = new Rule_findMultiNested.Match_findMultiNested_idpt_0_idpt_1(matched_independent_findMultiNested_idpt_0_idpt_1);
                                            matched_independent_findMultiNested_idpt_0._idpt_1.SetMatchOfEnclosingPattern(matched_independent_findMultiNested_idpt_0);
                                            candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags = candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_idpt_0_edge__edge1;
                                            candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_node_end;
                                            candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_node_beg;
                                            --isoSpace;
                                            goto label7;
                                        }
                                        while( (candidate_findMultiNested_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_0_edge__edge0.lgspOutNext) != head_candidate_findMultiNested_idpt_0_edge__edge0 );
                                    }
                                    candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags = candidate_findMultiNested_idpt_0_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_idpt_0_edge__edge1;
                                }
                                while( (candidate_findMultiNested_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_0_edge__edge1.lgspOutNext) != head_candidate_findMultiNested_idpt_0_edge__edge1 );
                            }
                            candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_0__candidate_findMultiNested_node_beg;
                            --isoSpace;
                        }
label3: ;
                        goto label8;
label7: ;
                        // IndependentPattern 
                        {
                            ++isoSpace;
                            uint prev_idpt_2__candidate_findMultiNested_node_beg;
                            prev_idpt_2__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            candidate_findMultiNested_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            if((candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                            {
                                candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_node_beg;
                                --isoSpace;
                                goto label9;
                            }
                            uint prev_idpt_2__candidate_findMultiNested_node_end;
                            prev_idpt_2__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            candidate_findMultiNested_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                            // IndependentPattern 
                            {
                                ++isoSpace;
                                uint prev_idpt_2idpt_3__candidate_findMultiNested_node_beg;
                                prev_idpt_2idpt_3__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                candidate_findMultiNested_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                if((candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                {
                                    candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_node_beg;
                                    --isoSpace;
                                    goto label10;
                                }
                                uint prev_idpt_2idpt_3__candidate_findMultiNested_node_end;
                                prev_idpt_2idpt_3__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                candidate_findMultiNested_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                // Extend Outgoing findMultiNested_idpt_2_idpt_3_edge__edge1 from findMultiNested_node_end 
                                GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge1 = candidate_findMultiNested_node_end.lgspOuthead;
                                if(head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_2_idpt_3_edge__edge1 = head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge1;
                                    do
                                    {
                                        if(candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspType.TypeID!=1) {
                                            continue;
                                        }
                                        uint prev_idpt_2idpt_3__candidate_findMultiNested_idpt_2_idpt_3_edge__edge1;
                                        prev_idpt_2idpt_3__candidate_findMultiNested_idpt_2_idpt_3_edge__edge1 = candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                        candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                        // Implicit Target findMultiNested_idpt_2_idpt_3_node__node0 from findMultiNested_idpt_2_idpt_3_edge__edge1 
                                        GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_2_idpt_3_node__node0 = candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspTarget;
                                        if((candidate_findMultiNested_idpt_2_idpt_3_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                        {
                                            candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags = candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_idpt_2_idpt_3_edge__edge1;
                                            continue;
                                        }
                                        // Extend Outgoing findMultiNested_idpt_2_idpt_3_edge__edge0 from findMultiNested_idpt_2_idpt_3_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge0 = candidate_findMultiNested_idpt_2_idpt_3_node__node0.lgspOuthead;
                                        if(head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge0 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_2_idpt_3_edge__edge0 = head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge0;
                                            do
                                            {
                                                if(candidate_findMultiNested_idpt_2_idpt_3_edge__edge0.lgspType.TypeID!=1) {
                                                    continue;
                                                }
                                                if(candidate_findMultiNested_idpt_2_idpt_3_edge__edge0.lgspTarget != candidate_findMultiNested_node_beg) {
                                                    continue;
                                                }
                                                if((candidate_findMultiNested_idpt_2_idpt_3_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                                {
                                                    continue;
                                                }
                                                // independent pattern found
                                                matched_independent_findMultiNested_idpt_2_idpt_3._node__node0 = candidate_findMultiNested_idpt_2_idpt_3_node__node0;
                                                matched_independent_findMultiNested_idpt_2_idpt_3._node_beg = candidate_findMultiNested_node_beg;
                                                matched_independent_findMultiNested_idpt_2_idpt_3._node_end = candidate_findMultiNested_node_end;
                                                matched_independent_findMultiNested_idpt_2_idpt_3._edge__edge0 = candidate_findMultiNested_idpt_2_idpt_3_edge__edge0;
                                                matched_independent_findMultiNested_idpt_2_idpt_3._edge__edge1 = candidate_findMultiNested_idpt_2_idpt_3_edge__edge1;
                                                candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags = candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_idpt_2_idpt_3_edge__edge1;
                                                candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_node_end;
                                                candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_node_beg;
                                                --isoSpace;
                                                goto label11;
                                            }
                                            while( (candidate_findMultiNested_idpt_2_idpt_3_edge__edge0 = candidate_findMultiNested_idpt_2_idpt_3_edge__edge0.lgspOutNext) != head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge0 );
                                        }
                                        candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags = candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_idpt_2_idpt_3_edge__edge1;
                                    }
                                    while( (candidate_findMultiNested_idpt_2_idpt_3_edge__edge1 = candidate_findMultiNested_idpt_2_idpt_3_edge__edge1.lgspOutNext) != head_candidate_findMultiNested_idpt_2_idpt_3_edge__edge1 );
                                }
                                candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_node_end;
                                candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2idpt_3__candidate_findMultiNested_node_beg;
                                --isoSpace;
                            }
label10: ;
                            candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_node_beg;
                            --isoSpace;
                            goto label12;
label11: ;
                            // Extend Outgoing findMultiNested_idpt_2_edge__edge0 from findMultiNested_node_beg 
                            GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_2_edge__edge0 = candidate_findMultiNested_node_beg.lgspOuthead;
                            if(head_candidate_findMultiNested_idpt_2_edge__edge0 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_2_edge__edge0 = head_candidate_findMultiNested_idpt_2_edge__edge0;
                                do
                                {
                                    if(candidate_findMultiNested_idpt_2_edge__edge0.lgspType.TypeID!=1) {
                                        continue;
                                    }
                                    uint prev_idpt_2__candidate_findMultiNested_idpt_2_edge__edge0;
                                    prev_idpt_2__candidate_findMultiNested_idpt_2_edge__edge0 = candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                    candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                                    // Implicit Target findMultiNested_idpt_2_node__node0 from findMultiNested_idpt_2_edge__edge0 
                                    GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_2_node__node0 = candidate_findMultiNested_idpt_2_edge__edge0.lgspTarget;
                                    if((candidate_findMultiNested_idpt_2_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                    {
                                        candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags = candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_idpt_2_edge__edge0;
                                        continue;
                                    }
                                    // Extend Outgoing findMultiNested_idpt_2_edge__edge1 from findMultiNested_idpt_2_node__node0 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_2_edge__edge1 = candidate_findMultiNested_idpt_2_node__node0.lgspOuthead;
                                    if(head_candidate_findMultiNested_idpt_2_edge__edge1 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_2_edge__edge1 = head_candidate_findMultiNested_idpt_2_edge__edge1;
                                        do
                                        {
                                            if(candidate_findMultiNested_idpt_2_edge__edge1.lgspType.TypeID!=1) {
                                                continue;
                                            }
                                            if(candidate_findMultiNested_idpt_2_edge__edge1.lgspTarget != candidate_findMultiNested_node_end) {
                                                continue;
                                            }
                                            if((candidate_findMultiNested_idpt_2_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                            {
                                                continue;
                                            }
                                            // independent pattern found
                                            matched_independent_findMultiNested_idpt_2._node_beg = candidate_findMultiNested_node_beg;
                                            matched_independent_findMultiNested_idpt_2._node__node0 = candidate_findMultiNested_idpt_2_node__node0;
                                            matched_independent_findMultiNested_idpt_2._node_end = candidate_findMultiNested_node_end;
                                            matched_independent_findMultiNested_idpt_2._edge__edge0 = candidate_findMultiNested_idpt_2_edge__edge0;
                                            matched_independent_findMultiNested_idpt_2._edge__edge1 = candidate_findMultiNested_idpt_2_edge__edge1;
                                            matched_independent_findMultiNested_idpt_2._idpt_3 = matched_independent_findMultiNested_idpt_2_idpt_3;
                                            matched_independent_findMultiNested_idpt_2_idpt_3 = new Rule_findMultiNested.Match_findMultiNested_idpt_2_idpt_3(matched_independent_findMultiNested_idpt_2_idpt_3);
                                            matched_independent_findMultiNested_idpt_2._idpt_3.SetMatchOfEnclosingPattern(matched_independent_findMultiNested_idpt_2);
                                            candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags = candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_idpt_2_edge__edge0;
                                            candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_node_end;
                                            candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_node_beg;
                                            --isoSpace;
                                            goto label13;
                                        }
                                        while( (candidate_findMultiNested_idpt_2_edge__edge1 = candidate_findMultiNested_idpt_2_edge__edge1.lgspOutNext) != head_candidate_findMultiNested_idpt_2_edge__edge1 );
                                    }
                                    candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags = candidate_findMultiNested_idpt_2_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_idpt_2_edge__edge0;
                                }
                                while( (candidate_findMultiNested_idpt_2_edge__edge0 = candidate_findMultiNested_idpt_2_edge__edge0.lgspOutNext) != head_candidate_findMultiNested_idpt_2_edge__edge0 );
                            }
                            candidate_findMultiNested_node_end.lgspFlags = candidate_findMultiNested_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_idpt_2__candidate_findMultiNested_node_beg;
                            --isoSpace;
                        }
label9: ;
                        goto label14;
label13: ;
                        Rule_findMultiNested.Match_findMultiNested match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_findMultiNested_node_beg;
                        match._node__node0 = candidate_findMultiNested_node__node0;
                        match._node_end = candidate_findMultiNested_node_end;
                        match._edge__edge0 = candidate_findMultiNested_edge__edge0;
                        match._edge__edge1 = candidate_findMultiNested_edge__edge1;
                        match._idpt_0 = matched_independent_findMultiNested_idpt_0;
                        matched_independent_findMultiNested_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_0(matched_independent_findMultiNested_idpt_0);
                        match._idpt_0.SetMatchOfEnclosingPattern(match);
                        match._idpt_2 = matched_independent_findMultiNested_idpt_2;
                        matched_independent_findMultiNested_idpt_2 = new Rule_findMultiNested.Match_findMultiNested_idpt_2(matched_independent_findMultiNested_idpt_2);
                        match._idpt_2.SetMatchOfEnclosingPattern(match);
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findMultiNested_node__node0.MoveOutHeadAfter(candidate_findMultiNested_edge__edge1);
                            graph.MoveHeadAfter(candidate_findMultiNested_edge__edge0);
                            candidate_findMultiNested_node__node0.lgspFlags = candidate_findMultiNested_node__node0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_node__node0;
                            candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_node_beg;
                            candidate_findMultiNested_edge__edge0.lgspFlags = candidate_findMultiNested_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_edge__edge0;
                            return matches;
                        }
label6: ;
label8: ;
label12: ;
label14: ;
                    }
                    while( (candidate_findMultiNested_edge__edge1 = candidate_findMultiNested_edge__edge1.lgspOutNext) != head_candidate_findMultiNested_edge__edge1 );
                }
                candidate_findMultiNested_node__node0.lgspFlags = candidate_findMultiNested_node__node0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_node__node0;
                candidate_findMultiNested_node_beg.lgspFlags = candidate_findMultiNested_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_node_beg;
                candidate_findMultiNested_edge__edge0.lgspFlags = candidate_findMultiNested_edge__edge0.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findMultiNested_edge__edge0;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findMultiNested.IMatch_findMultiNested match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> matches)
        {
            foreach(Rule_findMultiNested.IMatch_findMultiNested match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_findMultiNested.IMatch_findMultiNested match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested> matches;
            
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
            
            Modify(actionEnv, (Rule_findMultiNested.IMatch_findMultiNested)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_findMultiNested.IMatch_findMultiNested>)matches);
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
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_createIterated
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_createIterated.IMatch_createIterated match, out GRGEN_MODEL.IintNode output_0, out GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> matches, List<GRGEN_MODEL.IintNode> output_0, List<GRGEN_LIBGR.INode> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IintNode output_0, ref GRGEN_LIBGR.INode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_MODEL.IintNode> output_0, List<GRGEN_LIBGR.INode> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_createIterated : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_createIterated
    {
        public Action_createIterated() {
            _rulePattern = Rule_createIterated.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createIterated.Match_createIterated, Rule_createIterated.IMatch_createIterated>(this);
        }

        public Rule_createIterated _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "createIterated"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createIterated.Match_createIterated, Rule_createIterated.IMatch_createIterated> matches;

        public static Action_createIterated Instance { get { return instance; } set { instance = value; } }
        private static Action_createIterated instance = new Action_createIterated();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            Rule_createIterated.Match_createIterated match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IintNode> output_list_0 = new List<GRGEN_MODEL.IintNode>();
        List<GRGEN_LIBGR.INode> output_list_1 = new List<GRGEN_LIBGR.INode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_createIterated.IMatch_createIterated match, out GRGEN_MODEL.IintNode output_0, out GRGEN_LIBGR.INode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> matches, List<GRGEN_MODEL.IintNode> output_0, List<GRGEN_LIBGR.INode> output_1)
        {
            foreach(Rule_createIterated.IMatch_createIterated match in matches)
            {
                GRGEN_MODEL.IintNode output_local_0; GRGEN_LIBGR.INode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IintNode output_0, ref GRGEN_LIBGR.INode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_MODEL.IintNode> output_0, List<GRGEN_LIBGR.INode> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_createIterated.IMatch_createIterated match in matches)
            {
                GRGEN_MODEL.IintNode output_local_0; GRGEN_LIBGR.INode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> matches;
            GRGEN_MODEL.IintNode output_0; GRGEN_LIBGR.INode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IintNode output_0; GRGEN_LIBGR.INode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated> matches;
            GRGEN_MODEL.IintNode output_0; GRGEN_LIBGR.INode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
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
            GRGEN_MODEL.IintNode output_0; GRGEN_LIBGR.INode output_1; 
            Modify(actionEnv, (Rule_createIterated.IMatch_createIterated)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_createIterated.IMatch_createIterated>)matches, output_list_0, output_list_1);
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
            GRGEN_MODEL.IintNode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(actionEnv, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IintNode output_0 = null; GRGEN_LIBGR.INode output_1 = null; 
            if(Apply(actionEnv, ref output_0, ref output_1)) {
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
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0, output_list_1);
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
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0, output_list_1);
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
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findChainPlusChainToInt
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end);
    }
    
    public class Action_findChainPlusChainToInt : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findChainPlusChainToInt
    {
        public Action_findChainPlusChainToInt() {
            _rulePattern = Rule_findChainPlusChainToInt.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt, Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt>(this);
        }

        public Rule_findChainPlusChainToInt _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findChainPlusChainToInt"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt, Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches;

        public static Action_findChainPlusChainToInt Instance { get { return instance; } set { instance = value; } }
        private static Action_findChainPlusChainToInt instance = new Action_findChainPlusChainToInt();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset findChainPlusChainToInt_node_beg 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToInt_node_beg = (GRGEN_LGSP.LGSPNode)findChainPlusChainToInt_node_beg;
            uint prev__candidate_findChainPlusChainToInt_node_beg;
            prev__candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_findChainPlusChainToInt_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Preset findChainPlusChainToInt_node_end 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToInt_node_end = (GRGEN_LGSP.LGSPNode)findChainPlusChainToInt_node_end;
            if((candidate_findChainPlusChainToInt_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
            {
                candidate_findChainPlusChainToInt_node_beg.lgspFlags = candidate_findChainPlusChainToInt_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToInt_node_beg;
                return matches;
            }
            uint prev__candidate_findChainPlusChainToInt_node_end;
            prev__candidate_findChainPlusChainToInt_node_end = candidate_findChainPlusChainToInt_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_findChainPlusChainToInt_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Element iteratedPathToIntNode_node_beg_inlined__sub1_3 assigned from other element findChainPlusChainToInt_node_end 
            GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3 = candidate_findChainPlusChainToInt_node_end;
            // Element iteratedPath_node_end_inlined__sub0_2 assigned from other element findChainPlusChainToInt_node_end 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_2 = candidate_findChainPlusChainToInt_node_end;
            // Element iteratedPath_node_beg_inlined__sub0_2 assigned from other element findChainPlusChainToInt_node_beg 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_2 = candidate_findChainPlusChainToInt_node_beg;
            // Push alternative matching task for findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt
            AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt taskFor_alt_0_inlined__sub1_3_in_findChainPlusChainToInt = AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt.getNewTask(actionEnv, openTasks, Pattern_iteratedPathToIntNode.Instance.patternGraph.alternatives[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0_inlined__sub1_3_in_findChainPlusChainToInt.iteratedPathToIntNode_node_beg_inlined__sub1_3 = candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3;
            taskFor_alt_0_inlined__sub1_3_in_findChainPlusChainToInt.searchPatternpath = false;
            taskFor_alt_0_inlined__sub1_3_in_findChainPlusChainToInt.matchOfNestingPattern = null;
            taskFor_alt_0_inlined__sub1_3_in_findChainPlusChainToInt.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0_inlined__sub1_3_in_findChainPlusChainToInt);
            // Push alternative matching task for findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt
            AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt = AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt.getNewTask(actionEnv, openTasks, Pattern_iteratedPath.Instance.patternGraph.alternatives[(int)Pattern_iteratedPath.iteratedPath_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt.iteratedPath_node_beg_inlined__sub0_2 = candidate_iteratedPath_node_beg_inlined__sub0_2;
            taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt.iteratedPath_node_end_inlined__sub0_2 = candidate_iteratedPath_node_end_inlined__sub0_2;
            taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt.searchPatternpath = false;
            taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt.matchOfNestingPattern = null;
            taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt);
            uint prevGlobal__candidate_findChainPlusChainToInt_node_beg;
            prevGlobal__candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            candidate_findChainPlusChainToInt_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            uint prevGlobal__candidate_findChainPlusChainToInt_node_end;
            prevGlobal__candidate_findChainPlusChainToInt_node_end = candidate_findChainPlusChainToInt_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            candidate_findChainPlusChainToInt_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
            // Pop alternative matching task for findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt
            openTasks.Pop();
            AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt.releaseTask(taskFor_alt_0_inlined__sub0_2_in_findChainPlusChainToInt);
            // Pop alternative matching task for findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt
            openTasks.Pop();
            AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt.releaseTask(taskFor_alt_0_inlined__sub1_3_in_findChainPlusChainToInt);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt match = matches.GetNextUnfilledPosition();
                    Pattern_iteratedPath.Match_iteratedPath match__sub0 = new Pattern_iteratedPath.Match_iteratedPath();
                    match__sub0.SetMatchOfEnclosingPattern(match);
                    Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode match__sub1 = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode();
                    match__sub1.SetMatchOfEnclosingPattern(match);
                    match._node_beg = candidate_findChainPlusChainToInt_node_beg;
                    match._node_end = candidate_findChainPlusChainToInt_node_end;
                    match__sub0._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_2;
                    match__sub0._node_end = candidate_iteratedPath_node_end_inlined__sub0_2;
                    match__sub1._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3;
                    match.__sub0 = match__sub0;
                    match.__sub1 = match__sub1;
                    match__sub0._alt_0 = (Pattern_iteratedPath.IMatch_iteratedPath_alt_0)currentFoundPartialMatch.Pop();
                    match__sub0._alt_0.SetMatchOfEnclosingPattern(match__sub0);
                    match__sub1._alt_0 = (Pattern_iteratedPathToIntNode.IMatch_iteratedPathToIntNode_alt_0)currentFoundPartialMatch.Pop();
                    match__sub1._alt_0.SetMatchOfEnclosingPattern(match__sub1);
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_findChainPlusChainToInt_node_end.lgspFlags = candidate_findChainPlusChainToInt_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                    candidate_findChainPlusChainToInt_node_beg.lgspFlags = candidate_findChainPlusChainToInt_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                    candidate_findChainPlusChainToInt_node_end.lgspFlags = candidate_findChainPlusChainToInt_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToInt_node_end;
                    candidate_findChainPlusChainToInt_node_beg.lgspFlags = candidate_findChainPlusChainToInt_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToInt_node_beg;
                    return matches;
                }
                candidate_findChainPlusChainToInt_node_end.lgspFlags = candidate_findChainPlusChainToInt_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                candidate_findChainPlusChainToInt_node_beg.lgspFlags = candidate_findChainPlusChainToInt_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                candidate_findChainPlusChainToInt_node_end.lgspFlags = candidate_findChainPlusChainToInt_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToInt_node_end;
                candidate_findChainPlusChainToInt_node_beg.lgspFlags = candidate_findChainPlusChainToInt_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToInt_node_beg;
                return matches;
            }
            candidate_findChainPlusChainToInt_node_beg.lgspFlags = candidate_findChainPlusChainToInt_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
            candidate_findChainPlusChainToInt_node_end.lgspFlags = candidate_findChainPlusChainToInt_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
            candidate_findChainPlusChainToInt_node_end.lgspFlags = candidate_findChainPlusChainToInt_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToInt_node_end;
            candidate_findChainPlusChainToInt_node_beg.lgspFlags = candidate_findChainPlusChainToInt_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToInt_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches)
        {
            foreach(Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end);
            if(matches.Count <= 0) return 0;
            foreach(Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode findChainPlusChainToInt_node_beg, GRGEN_LIBGR.INode findChainPlusChainToInt_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToInt.IMatch_findChainPlusChainToInt>)matches);
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
            
            if(Apply(actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
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
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
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
            return ApplyStar(actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCall filter)
        {
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
        }
    }
    
    public class AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub0_2_in_findChainPlusChainToInt next = null;

        public GRGEN_LGSP.LGSPNode iteratedPath_node_beg_inlined__sub0_2;
        public GRGEN_LGSP.LGSPNode iteratedPath_node_end_inlined__sub0_2;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPath_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@base];
                // SubPreset iteratedPath_node_beg_inlined__sub0_2 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_2 = iteratedPath_node_beg_inlined__sub0_2;
                // SubPreset iteratedPath_node_end_inlined__sub0_2 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_2 = iteratedPath_node_end_inlined__sub0_2;
                // Extend Outgoing iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2 from iteratedPath_node_beg_inlined__sub0_2 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2 = candidate_iteratedPath_node_beg_inlined__sub0_2.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2 = head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspTarget != candidate_iteratedPath_node_end_inlined__sub0_2) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                            match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_2;
                            match._node_end = candidate_iteratedPath_node_end_inlined__sub0_2;
                            match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2;
                        prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                                match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_2;
                                match._node_end = candidate_iteratedPath_node_end_inlined__sub0_2;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2;
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
                                candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2;
                    }
                    while( (candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2.lgspOutNext) != head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_2 );
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
            // Alternative case iteratedPath_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPath_node_beg_inlined__sub0_2 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_2 = iteratedPath_node_beg_inlined__sub0_2;
                // SubPreset iteratedPath_node_end_inlined__sub0_2 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_2 = iteratedPath_node_end_inlined__sub0_2;
                // Extend Outgoing iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 from iteratedPath_node_beg_inlined__sub0_2 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 = candidate_iteratedPath_node_beg_inlined__sub0_2.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 = head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2 from iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspTarget;
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0
                            && candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2==candidate_iteratedPath_node_beg_inlined__sub0_2
                            )
                        {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0_inlined__sub0_2
                        PatternAction_iteratedPath taskFor__sub0_inlined__sub0_2 = PatternAction_iteratedPath.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_2.iteratedPath_node_beg = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2;
                        taskFor__sub0_inlined__sub0_2.iteratedPath_node_end = candidate_iteratedPath_node_end_inlined__sub0_2;
                        taskFor__sub0_inlined__sub0_2.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_2.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_2.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_2);
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2 = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop subpattern matching task for _sub0_inlined__sub0_2
                        openTasks.Pop();
                        PatternAction_iteratedPath.releaseTask(taskFor__sub0_inlined__sub0_2);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive();
                                match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_2;
                                match._node_intermediate = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2;
                                match._node_end = candidate_iteratedPath_node_end_inlined__sub0_2;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2;
                                match.__sub0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
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
                                candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2;
                                candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2;
                            candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_2;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2;
                    }
                    while( (candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2.lgspOutNext) != head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_2 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    
    public class AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_findChainPlusChainToInt_alt_0_inlined__sub1_3_in_findChainPlusChainToInt next = null;

        public GRGEN_LGSP.LGSPNode iteratedPathToIntNode_node_beg_inlined__sub1_3;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPathToIntNode_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@base];
                // SubPreset iteratedPathToIntNode_node_beg_inlined__sub1_3 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3 = iteratedPathToIntNode_node_beg_inlined__sub1_3;
                // Extend Outgoing iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 from iteratedPathToIntNode_node_beg_inlined__sub1_3 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 = candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 = head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3 from iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                            match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3;
                            match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3;
                            match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3;
                                match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3;
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
                                candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3;
                                candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3;
                            candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub1_3;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub1_3 );
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
            // Alternative case iteratedPathToIntNode_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPathToIntNode_node_beg_inlined__sub1_3 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3 = iteratedPathToIntNode_node_beg_inlined__sub1_3;
                // Extend Outgoing iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 from iteratedPathToIntNode_node_beg_inlined__sub1_3 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 = candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 = head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3 from iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspType.TypeID!=0) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0_inlined__sub1_3
                        PatternAction_iteratedPathToIntNode taskFor__sub0_inlined__sub1_3 = PatternAction_iteratedPathToIntNode.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub1_3.iteratedPathToIntNode_node_beg = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3;
                        taskFor__sub0_inlined__sub1_3.searchPatternpath = false;
                        taskFor__sub0_inlined__sub1_3.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub1_3.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub1_3);
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop subpattern matching task for _sub0_inlined__sub1_3
                        openTasks.Pop();
                        PatternAction_iteratedPathToIntNode.releaseTask(taskFor__sub0_inlined__sub1_3);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub1_3;
                                match._node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3;
                                match.__sub0 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
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
                                candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3;
                                candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3;
                            candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub1_3;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub1_3 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findChainPlusChainToIntIndependent
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end);
    }
    
    public class Action_findChainPlusChainToIntIndependent : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findChainPlusChainToIntIndependent
    {
        public Action_findChainPlusChainToIntIndependent() {
            _rulePattern = Rule_findChainPlusChainToIntIndependent.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent, Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent>(this);
        }

        public Rule_findChainPlusChainToIntIndependent _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findChainPlusChainToIntIndependent"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent, Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches;

        public static Action_findChainPlusChainToIntIndependent Instance { get { return instance; } set { instance = value; } }
        private static Action_findChainPlusChainToIntIndependent instance = new Action_findChainPlusChainToIntIndependent();
        private Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0 matched_independent_findChainPlusChainToIntIndependent_idpt_0 = new Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0();        
        public GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset findChainPlusChainToIntIndependent_node_beg 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToIntIndependent_node_beg = (GRGEN_LGSP.LGSPNode)findChainPlusChainToIntIndependent_node_beg;
            uint prev__candidate_findChainPlusChainToIntIndependent_node_beg;
            prev__candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Preset findChainPlusChainToIntIndependent_node_end 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToIntIndependent_node_end = (GRGEN_LGSP.LGSPNode)findChainPlusChainToIntIndependent_node_end;
            if((candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
            {
                candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                return matches;
            }
            uint prev__candidate_findChainPlusChainToIntIndependent_node_end;
            prev__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // IndependentPattern 
            {
                ++isoSpace;
                Stack<GRGEN_LGSP.LGSPSubpatternAction> idpt_0_openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
                List<Stack<GRGEN_LIBGR.IMatch>> idpt_0_foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
                List<Stack<GRGEN_LIBGR.IMatch>> idpt_0_matchesList = idpt_0_foundPartialMatches;
                // Element iteratedPathToIntNode_node_beg_inlined__sub0_5 assigned from other element findChainPlusChainToIntIndependent_node_end 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5 = candidate_findChainPlusChainToIntIndependent_node_end;
                // Push alternative matching task for findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0
                AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 taskFor_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 = AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0.getNewTask(actionEnv, idpt_0_openTasks, Pattern_iteratedPathToIntNode.Instance.patternGraph.alternatives[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_AltNums.@alt_0].alternativeCases);
                taskFor_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0.iteratedPathToIntNode_node_beg_inlined__sub0_5 = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5;
                taskFor_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0.searchPatternpath = false;
                taskFor_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0.matchOfNestingPattern = null;
                taskFor_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0.lastMatchAtPreviousNestingLevel = null;
                idpt_0_openTasks.Push(taskFor_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0);
                uint prevGlobal_idpt_0__candidate_findChainPlusChainToIntIndependent_node_end;
                prevGlobal_idpt_0__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                // Match subpatterns of idpt_0_
                idpt_0_openTasks.Peek().myMatch(idpt_0_matchesList, 1, isoSpace);
                // Pop alternative matching task for findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0
                idpt_0_openTasks.Pop();
                AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0.releaseTask(taskFor_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0);
                // Check whether subpatterns were found 
                if(idpt_0_matchesList.Count>0) {
                    // independent pattern with contained subpatterns found
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = idpt_0_matchesList[0];
                    idpt_0_matchesList.Clear();
                    Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode match__sub0 = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode();
                    match__sub0.SetMatchOfEnclosingPattern(matched_independent_findChainPlusChainToIntIndependent_idpt_0);
                    matched_independent_findChainPlusChainToIntIndependent_idpt_0._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                    match__sub0._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5;
                    matched_independent_findChainPlusChainToIntIndependent_idpt_0.__sub0 = match__sub0;
                    match__sub0._alt_0 = (Pattern_iteratedPathToIntNode.IMatch_iteratedPathToIntNode_alt_0)currentFoundPartialMatch.Pop();
                    match__sub0._alt_0.SetMatchOfEnclosingPattern(match__sub0);
                    candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal_idpt_0__candidate_findChainPlusChainToIntIndependent_node_end;
                    --isoSpace;
                    goto label15;
                }
                candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal_idpt_0__candidate_findChainPlusChainToIntIndependent_node_end;
                --isoSpace;
            }
            candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_end;
            candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
            return matches;
label15: ;
            // Element iteratedPath_node_end_inlined__sub0_4 assigned from other element findChainPlusChainToIntIndependent_node_end 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_4 = candidate_findChainPlusChainToIntIndependent_node_end;
            // Element iteratedPath_node_beg_inlined__sub0_4 assigned from other element findChainPlusChainToIntIndependent_node_beg 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_4 = candidate_findChainPlusChainToIntIndependent_node_beg;
            // Push alternative matching task for findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent
            AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent = AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent.getNewTask(actionEnv, openTasks, Pattern_iteratedPath.Instance.patternGraph.alternatives[(int)Pattern_iteratedPath.iteratedPath_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent.iteratedPath_node_beg_inlined__sub0_4 = candidate_iteratedPath_node_beg_inlined__sub0_4;
            taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent.iteratedPath_node_end_inlined__sub0_4 = candidate_iteratedPath_node_end_inlined__sub0_4;
            taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent.searchPatternpath = false;
            taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent.matchOfNestingPattern = null;
            taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent);
            uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
            prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
            prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
            // Pop alternative matching task for findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent
            openTasks.Pop();
            AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent.releaseTask(taskFor_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent match = matches.GetNextUnfilledPosition();
                    Pattern_iteratedPath.Match_iteratedPath match__sub0 = new Pattern_iteratedPath.Match_iteratedPath();
                    match__sub0.SetMatchOfEnclosingPattern(match);
                    match._node_beg = candidate_findChainPlusChainToIntIndependent_node_beg;
                    match._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                    match__sub0._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_4;
                    match__sub0._node_end = candidate_iteratedPath_node_end_inlined__sub0_4;
                    match.__sub0 = match__sub0;
                    match__sub0._alt_0 = (Pattern_iteratedPath.IMatch_iteratedPath_alt_0)currentFoundPartialMatch.Pop();
                    match__sub0._alt_0.SetMatchOfEnclosingPattern(match__sub0);
                    match._idpt_0 = matched_independent_findChainPlusChainToIntIndependent_idpt_0;
                    matched_independent_findChainPlusChainToIntIndependent_idpt_0 = new Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0(matched_independent_findChainPlusChainToIntIndependent_idpt_0);
                    match._idpt_0.SetMatchOfEnclosingPattern(match);
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                    candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                    candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_end;
                    candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                    return matches;
                }
                candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_end;
                candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                return matches;
            }
            candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
            candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
            candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_end.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_end;
            candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags = candidate_findChainPlusChainToIntIndependent_node_beg.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches)
        {
            foreach(Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end);
            if(matches.Count <= 0) return 0;
            foreach(Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_beg, GRGEN_LIBGR.INode findChainPlusChainToIntIndependent_node_end)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent>)matches);
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
            
            if(Apply(actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1])) {
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
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
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
            return ApplyStar(actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_LIBGR.INode) parameters[0], (GRGEN_LIBGR.INode) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCall filter)
        {
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
        }
    }
    
    public class AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_findChainPlusChainToIntIndependent_alt_0_inlined__sub0_4_in_findChainPlusChainToIntIndependent next = null;

        public GRGEN_LGSP.LGSPNode iteratedPath_node_beg_inlined__sub0_4;
        public GRGEN_LGSP.LGSPNode iteratedPath_node_end_inlined__sub0_4;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPath_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@base];
                // SubPreset iteratedPath_node_beg_inlined__sub0_4 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_4 = iteratedPath_node_beg_inlined__sub0_4;
                // SubPreset iteratedPath_node_end_inlined__sub0_4 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_4 = iteratedPath_node_end_inlined__sub0_4;
                // Extend Outgoing iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4 from iteratedPath_node_beg_inlined__sub0_4 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4 = candidate_iteratedPath_node_beg_inlined__sub0_4.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4 = head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspTarget != candidate_iteratedPath_node_end_inlined__sub0_4) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                            match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_4;
                            match._node_end = candidate_iteratedPath_node_end_inlined__sub0_4;
                            match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4;
                        prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                                match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_4;
                                match._node_end = candidate_iteratedPath_node_end_inlined__sub0_4;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4;
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
                                candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4;
                    }
                    while( (candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4 = candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4.lgspOutNext) != head_candidate_iteratedPath_alt_0_base_edge__edge0_inlined__sub0_4 );
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
            // Alternative case iteratedPath_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPath_node_beg_inlined__sub0_4 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg_inlined__sub0_4 = iteratedPath_node_beg_inlined__sub0_4;
                // SubPreset iteratedPath_node_end_inlined__sub0_4 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end_inlined__sub0_4 = iteratedPath_node_end_inlined__sub0_4;
                // Extend Outgoing iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 from iteratedPath_node_beg_inlined__sub0_4 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 = candidate_iteratedPath_node_beg_inlined__sub0_4.lgspOuthead;
                if(head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 = head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4 from iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspTarget;
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0
                            && candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4==candidate_iteratedPath_node_beg_inlined__sub0_4
                            )
                        {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0_inlined__sub0_4
                        PatternAction_iteratedPath taskFor__sub0_inlined__sub0_4 = PatternAction_iteratedPath.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_4.iteratedPath_node_beg = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4;
                        taskFor__sub0_inlined__sub0_4.iteratedPath_node_end = candidate_iteratedPath_node_end_inlined__sub0_4;
                        taskFor__sub0_inlined__sub0_4.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_4.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_4.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_4);
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4 = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop subpattern matching task for _sub0_inlined__sub0_4
                        openTasks.Pop();
                        PatternAction_iteratedPath.releaseTask(taskFor__sub0_inlined__sub0_4);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive();
                                match._node_beg = candidate_iteratedPath_node_beg_inlined__sub0_4;
                                match._node_intermediate = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4;
                                match._node_end = candidate_iteratedPath_node_end_inlined__sub0_4;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4;
                                match.__sub0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
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
                                candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4;
                                candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4;
                            candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate_inlined__sub0_4;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4;
                    }
                    while( (candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 = candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4.lgspOutNext) != head_candidate_iteratedPath_alt_0_recursive_edge__edge0_inlined__sub0_4 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    
    public class AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0(actionEnv_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_findChainPlusChainToIntIndependent_idpt_0_alt_0_inlined__sub0_5_in_findChainPlusChainToIntIndependent_idpt_0 next = null;

        public GRGEN_LGSP.LGSPNode iteratedPathToIntNode_node_beg_inlined__sub0_5;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int isoSpace)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPathToIntNode_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@base];
                // SubPreset iteratedPathToIntNode_node_beg_inlined__sub0_5 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5 = iteratedPathToIntNode_node_beg_inlined__sub0_5;
                // Extend Outgoing iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 from iteratedPathToIntNode_node_beg_inlined__sub0_5 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 = head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5 from iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                            match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5;
                            match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5;
                            match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5;
                                match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5;
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
                                candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5;
                                candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5;
                            candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end_inlined__sub0_5;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0_inlined__sub0_5 );
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
            // Alternative case iteratedPathToIntNode_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPathToIntNode_node_beg_inlined__sub0_5 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5 = iteratedPathToIntNode_node_beg_inlined__sub0_5;
                // Extend Outgoing iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 from iteratedPathToIntNode_node_beg_inlined__sub0_5 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5.lgspOuthead;
                if(head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 = head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5 from iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspTarget;
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspType.TypeID!=0) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)==(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _sub0_inlined__sub0_5
                        PatternAction_iteratedPathToIntNode taskFor__sub0_inlined__sub0_5 = PatternAction_iteratedPathToIntNode.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_5.iteratedPathToIntNode_node_beg = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5;
                        taskFor__sub0_inlined__sub0_5.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_5.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_5.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_5);
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, isoSpace);
                        // Pop subpattern matching task for _sub0_inlined__sub0_5
                        openTasks.Pop();
                        PatternAction_iteratedPathToIntNode.releaseTask(taskFor__sub0_inlined__sub0_5);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg_inlined__sub0_5;
                                match._node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5;
                                match.__sub0 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
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
                                candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5;
                                candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5;
                            candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate_inlined__sub0_5;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5.lgspOutNext) != head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0_inlined__sub0_5 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }
    

    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class IndependentActions : GRGEN_LGSP.LGSPActions
    {
        public IndependentActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public IndependentActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            packages = new string[0];
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_iteratedPath.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Pattern_iteratedPath.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Pattern_iteratedPath.Instance);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_iteratedPathToIntNode.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Pattern_iteratedPathToIntNode.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Pattern_iteratedPathToIntNode.Instance);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_create.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_create.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_create.Instance);
            actions.Add("create", (GRGEN_LGSP.LGSPAction) Action_create.Instance);
            @create = Action_create.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_find.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_find.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_find.Instance);
            actions.Add("find", (GRGEN_LGSP.LGSPAction) Action_find.Instance);
            @find = Action_find.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findIndependent.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_findIndependent.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_findIndependent.Instance);
            actions.Add("findIndependent", (GRGEN_LGSP.LGSPAction) Action_findIndependent.Instance);
            @findIndependent = Action_findIndependent.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findMultiNested.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_findMultiNested.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_findMultiNested.Instance);
            actions.Add("findMultiNested", (GRGEN_LGSP.LGSPAction) Action_findMultiNested.Instance);
            @findMultiNested = Action_findMultiNested.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_createIterated.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_createIterated.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_createIterated.Instance);
            actions.Add("createIterated", (GRGEN_LGSP.LGSPAction) Action_createIterated.Instance);
            @createIterated = Action_createIterated.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findChainPlusChainToInt.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_findChainPlusChainToInt.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_findChainPlusChainToInt.Instance);
            actions.Add("findChainPlusChainToInt", (GRGEN_LGSP.LGSPAction) Action_findChainPlusChainToInt.Instance);
            @findChainPlusChainToInt = Action_findChainPlusChainToInt.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findChainPlusChainToIntIndependent.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_findChainPlusChainToIntIndependent.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_findChainPlusChainToIntIndependent.Instance);
            actions.Add("findChainPlusChainToIntIndependent", (GRGEN_LGSP.LGSPAction) Action_findChainPlusChainToIntIndependent.Instance);
            @findChainPlusChainToIntIndependent = Action_findChainPlusChainToIntIndependent.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_iteratedPath.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_iteratedPathToIntNode.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_create.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_find.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_findIndependent.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_findMultiNested.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_createIterated.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_findChainPlusChainToInt.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_findChainPlusChainToIntIndependent.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Pattern_iteratedPath.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Pattern_iteratedPathToIntNode.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_create.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_find.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_findIndependent.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_findMultiNested.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_createIterated.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_findChainPlusChainToInt.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_findChainPlusChainToIntIndependent.Instance.patternGraph);
            Pattern_iteratedPath.Instance.patternGraph.maxIsoSpace = 0;
            Pattern_iteratedPathToIntNode.Instance.patternGraph.maxIsoSpace = 0;
            Rule_create.Instance.patternGraph.maxIsoSpace = 0;
            Rule_find.Instance.patternGraph.maxIsoSpace = 0;
            Rule_findIndependent.Instance.patternGraph.maxIsoSpace = 0;
            Rule_findMultiNested.Instance.patternGraph.maxIsoSpace = 0;
            Rule_createIterated.Instance.patternGraph.maxIsoSpace = 0;
            Rule_findChainPlusChainToInt.Instance.patternGraph.maxIsoSpace = 0;
            Rule_findChainPlusChainToIntIndependent.Instance.patternGraph.maxIsoSpace = 0;
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_iteratedPath.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_iteratedPathToIntNode.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_create.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_find.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findIndependent.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findMultiNested.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_createIterated.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findChainPlusChainToInt.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_findChainPlusChainToIntIndependent.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
        }
        
        public IAction_create @create;
        public IAction_find @find;
        public IAction_findIndependent @findIndependent;
        public IAction_findMultiNested @findMultiNested;
        public IAction_createIterated @createIterated;
        public IAction_findChainPlusChainToInt @findChainPlusChainToInt;
        public IAction_findChainPlusChainToIntIndependent @findChainPlusChainToIntIndependent;
        
        
        public override string[] Packages { get { return packages; } }
        private string[] packages;
        
        public override string Name { get { return "IndependentActions"; } }
        public override string StatisticsPath { get { return null; } }
        public override bool LazyNIC { get { return false; } }
        public override bool InlineIndependents { get { return true; } }
        public override bool Profile { get { return false; } }

        public override string ModelMD5Hash { get { return "a5b70deb49575f4d0997a3b831be3dfa"; } }
    }
}