// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequences.grg" on Sun Jun 17 17:17:18 CEST 2012

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalFiltersAndSequences;

namespace de.unika.ipd.grGen.Action_ExternalFiltersAndSequences
{
	public class Rule_filterBase : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_filterBase instance = null;
		public static Rule_filterBase Instance { get { if (instance==null) { instance = new Rule_filterBase(); instance.initialize(); } return instance; } }

		public enum filterBase_NodeNums { };
		public enum filterBase_EdgeNums { };
		public enum filterBase_VariableNums { };
		public enum filterBase_SubNums { };
		public enum filterBase_AltNums { };
		public enum filterBase_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_filterBase;


		private Rule_filterBase()
		{
			name = "filterBase";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { "f1", "nomnomnom", };

		}
		private void initialize()
		{
			bool[,] filterBase_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] filterBase_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] filterBase_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] filterBase_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_filterBase = new GRGEN_LGSP.PatternGraph(
				"filterBase",
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
				filterBase_isNodeHomomorphicGlobal,
				filterBase_isEdgeHomomorphicGlobal,
				filterBase_isNodeTotallyHomomorphic,
				filterBase_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_filterBase;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_filterBase curMatch = (Match_filterBase)_curMatch;
			graph.SettingAddedNodeNames( filterBase_addedNodeNames );
			graph.SettingAddedEdgeNames( filterBase_addedEdgeNames );
			return;
		}
		private static string[] filterBase_addedNodeNames = new string[] {  };
		private static string[] filterBase_addedEdgeNames = new string[] {  };

		static Rule_filterBase() {
		}

		public interface IMatch_filterBase : GRGEN_LIBGR.IMatch
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

		public class Match_filterBase : GRGEN_LGSP.ListElement<Match_filterBase>, IMatch_filterBase
		{
			public enum filterBase_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBase_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBase_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBase_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBase_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBase_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBase_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_filterBase.instance.pat_filterBase; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_filterBase(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_filterBase(Match_filterBase that)
			{
			}
			public Match_filterBase()
			{
			}
		}

	}

	public class Rule_filterBass : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_filterBass instance = null;
		public static Rule_filterBass Instance { get { if (instance==null) { instance = new Rule_filterBass(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] filterBass_node_n_AllowedTypes = null;
		public static bool[] filterBass_node_n_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] filterBass_edge_e_AllowedTypes = null;
		public static bool[] filterBass_edge_e_IsAllowedType = null;
		public enum filterBass_NodeNums { @n, };
		public enum filterBass_EdgeNums { @e, };
		public enum filterBass_VariableNums { };
		public enum filterBass_SubNums { };
		public enum filterBass_AltNums { };
		public enum filterBass_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_filterBass;


		private Rule_filterBass()
		{
			name = "filterBass";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { "f2", "f3", };

		}
		private void initialize()
		{
			bool[,] filterBass_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] filterBass_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] filterBass_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] filterBass_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode filterBass_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, "GRGEN_MODEL.IN", "filterBass_node_n", "n", filterBass_node_n_AllowedTypes, filterBass_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge filterBass_edge_e = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@E, "GRGEN_MODEL.IE", "filterBass_edge_e", "e", filterBass_edge_e_AllowedTypes, filterBass_edge_e_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_filterBass = new GRGEN_LGSP.PatternGraph(
				"filterBass",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { filterBass_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { filterBass_edge_e }, 
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
				filterBass_isNodeHomomorphicGlobal,
				filterBass_isEdgeHomomorphicGlobal,
				filterBass_isNodeTotallyHomomorphic,
				filterBass_isEdgeTotallyHomomorphic
			);
			pat_filterBass.edgeToSourceNode.Add(filterBass_edge_e, filterBass_node_n);
			pat_filterBass.edgeToTargetNode.Add(filterBass_edge_e, filterBass_node_n);

			filterBass_node_n.pointOfDefinition = pat_filterBass;
			filterBass_edge_e.pointOfDefinition = pat_filterBass;

			patternGraph = pat_filterBass;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_filterBass curMatch = (Match_filterBass)_curMatch;
			graph.SettingAddedNodeNames( filterBass_addedNodeNames );
			graph.SettingAddedEdgeNames( filterBass_addedEdgeNames );
			return;
		}
		private static string[] filterBass_addedNodeNames = new string[] {  };
		private static string[] filterBass_addedEdgeNames = new string[] {  };

		static Rule_filterBass() {
		}

		public interface IMatch_filterBass : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IN node_n { get; }
			//Edges
			GRGEN_MODEL.IE edge_e { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_filterBass : GRGEN_LGSP.ListElement<Match_filterBass>, IMatch_filterBass
		{
			public GRGEN_MODEL.IN node_n { get { return (GRGEN_MODEL.IN)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum filterBass_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)filterBass_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IE edge_e { get { return (GRGEN_MODEL.IE)_edge_e; } }
			public GRGEN_LGSP.LGSPEdge _edge_e;
			public enum filterBass_EdgeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)filterBass_EdgeNums.@e: return _edge_e;
				default: return null;
				}
			}
			
			public enum filterBass_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBass_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBass_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBass_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterBass_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_filterBass.instance.pat_filterBass; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_filterBass(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_filterBass(Match_filterBass that)
			{
				_node_n = that._node_n;
				_edge_e = that._edge_e;
			}
			public Match_filterBass()
			{
			}
		}

	}

	public class Rule_filterHass : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_filterHass instance = null;
		public static Rule_filterHass Instance { get { if (instance==null) { instance = new Rule_filterHass(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] filterHass_node_n_AllowedTypes = null;
		public static bool[] filterHass_node_n_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] filterHass_edge_e_AllowedTypes = null;
		public static bool[] filterHass_edge_e_IsAllowedType = null;
		public enum filterHass_NodeNums { @n, };
		public enum filterHass_EdgeNums { @e, };
		public enum filterHass_VariableNums { };
		public enum filterHass_SubNums { };
		public enum filterHass_AltNums { };
		public enum filterHass_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_filterHass;


		private Rule_filterHass()
		{
			name = "filterHass";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, };
			inputNames = new string[] { "filterHass_node_n", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.EdgeType_E.typeVar, };
			filters = new String[] { "f4", };

		}
		private void initialize()
		{
			bool[,] filterHass_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] filterHass_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] filterHass_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] filterHass_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode filterHass_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, "GRGEN_MODEL.IN", "filterHass_node_n", "n", filterHass_node_n_AllowedTypes, filterHass_node_n_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge filterHass_edge_e = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@E, "GRGEN_MODEL.IE", "filterHass_edge_e", "e", filterHass_edge_e_AllowedTypes, filterHass_edge_e_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_filterHass = new GRGEN_LGSP.PatternGraph(
				"filterHass",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { filterHass_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { filterHass_edge_e }, 
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
				filterHass_isNodeHomomorphicGlobal,
				filterHass_isEdgeHomomorphicGlobal,
				filterHass_isNodeTotallyHomomorphic,
				filterHass_isEdgeTotallyHomomorphic
			);
			pat_filterHass.edgeToSourceNode.Add(filterHass_edge_e, filterHass_node_n);
			pat_filterHass.edgeToTargetNode.Add(filterHass_edge_e, filterHass_node_n);

			filterHass_node_n.pointOfDefinition = null;
			filterHass_edge_e.pointOfDefinition = pat_filterHass;

			patternGraph = pat_filterHass;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IE output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_filterHass curMatch = (Match_filterHass)_curMatch;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			output_0 = (GRGEN_MODEL.IE)(edge_e);
			return;
		}

		static Rule_filterHass() {
		}

		public interface IMatch_filterHass : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IN node_n { get; }
			//Edges
			GRGEN_MODEL.IE edge_e { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_filterHass : GRGEN_LGSP.ListElement<Match_filterHass>, IMatch_filterHass
		{
			public GRGEN_MODEL.IN node_n { get { return (GRGEN_MODEL.IN)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum filterHass_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)filterHass_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IE edge_e { get { return (GRGEN_MODEL.IE)_edge_e; } }
			public GRGEN_LGSP.LGSPEdge _edge_e;
			public enum filterHass_EdgeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)filterHass_EdgeNums.@e: return _edge_e;
				default: return null;
				}
			}
			
			public enum filterHass_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterHass_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterHass_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterHass_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum filterHass_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_filterHass.instance.pat_filterHass; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_filterHass(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_filterHass(Match_filterHass that)
			{
				_node_n = that._node_n;
				_edge_e = that._edge_e;
			}
			public Match_filterHass()
			{
			}
		}

	}

	public class Rule_init : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init instance = null;
		public static Rule_init Instance { get { if (instance==null) { instance = new Rule_init(); instance.initialize(); } return instance; } }

		public enum init_NodeNums { };
		public enum init_EdgeNums { };
		public enum init_VariableNums { };
		public enum init_SubNums { };
		public enum init_AltNums { };
		public enum init_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_init;


		private Rule_init()
		{
			name = "init";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] init_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] init_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] init_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_init = new GRGEN_LGSP.PatternGraph(
				"init",
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
				init_isNodeHomomorphicGlobal,
				init_isEdgeHomomorphicGlobal,
				init_isNodeTotallyHomomorphic,
				init_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_init;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IN output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_init curMatch = (Match_init)_curMatch;
			graph.SettingAddedNodeNames( init_addedNodeNames );
			GRGEN_MODEL.@N node_n1 = GRGEN_MODEL.@N.CreateNode(graph);
			GRGEN_MODEL.@N node_n2 = GRGEN_MODEL.@N.CreateNode(graph);
			graph.SettingAddedEdgeNames( init_addedEdgeNames );
			GRGEN_MODEL.@E edge__edge0 = GRGEN_MODEL.@E.CreateEdge(graph, node_n1, node_n1);
			GRGEN_MODEL.@E edge__edge1 = GRGEN_MODEL.@E.CreateEdge(graph, node_n2, node_n2);
			output_0 = (GRGEN_MODEL.IN)(node_n1);
			return;
		}
		private static string[] init_addedNodeNames = new string[] { "n1", "n2" };
		private static string[] init_addedEdgeNames = new string[] { "_edge0", "_edge1" };

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

		public class Match_init : GRGEN_LGSP.ListElement<Match_init>, IMatch_init
		{
			public enum init_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_init.instance.pat_init; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_init(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_init(Match_init that)
			{
			}
			public Match_init()
			{
			}
		}

	}

	public class Rule_r : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_r instance = null;
		public static Rule_r Instance { get { if (instance==null) { instance = new Rule_r(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] r_node_n_AllowedTypes = null;
		public static bool[] r_node_n_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] r_edge_e_AllowedTypes = null;
		public static bool[] r_edge_e_IsAllowedType = null;
		public enum r_NodeNums { @n, };
		public enum r_EdgeNums { @e, };
		public enum r_VariableNums { };
		public enum r_SubNums { };
		public enum r_AltNums { };
		public enum r_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_r;


		private Rule_r()
		{
			name = "r";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { };

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
			GRGEN_LGSP.PatternNode r_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, "GRGEN_MODEL.IN", "r_node_n", "n", r_node_n_AllowedTypes, r_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge r_edge_e = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@E, "GRGEN_MODEL.IE", "r_edge_e", "e", r_edge_e_AllowedTypes, r_edge_e_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_r = new GRGEN_LGSP.PatternGraph(
				"r",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { r_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { r_edge_e }, 
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
			pat_r.edgeToSourceNode.Add(r_edge_e, r_node_n);
			pat_r.edgeToTargetNode.Add(r_edge_e, r_node_n);

			r_node_n.pointOfDefinition = pat_r;
			r_edge_e.pointOfDefinition = pat_r;

			patternGraph = pat_r;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_r curMatch = (Match_r)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			GRGEN_MODEL.IN inode_n = curMatch.node_n;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			graph.SettingAddedNodeNames( r_addedNodeNames );
			GRGEN_MODEL.@N node_m = GRGEN_MODEL.@N.CreateNode(graph);
			graph.SettingAddedEdgeNames( r_addedEdgeNames );
			int tempvar_node_n_i = inode_n.@i;
			double tempvar_node_n_d = inode_n.@d;
			GRGEN_MODEL.ENUM_Enu tempvar_node_n_enu = inode_n.@enu;
			string tempvar_node_n_s = inode_n.@s;
			bool tempvar_node_n_b = inode_n.@b;
			Object tempvar_node_n_o = inode_n.@o;
			GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;
			ApplyXGRS_r_0(procEnv, (GRGEN_MODEL.IN)node_n, (GRGEN_MODEL.IN)node_m, (GRGEN_MODEL.IE)edge_e);
			ApplyXGRS_r_1(procEnv, (GRGEN_MODEL.IN)node_n);
			return;
		}
		private static string[] r_addedNodeNames = new string[] { "m" };
		private static string[] r_addedEdgeNames = new string[] {  };

        public static bool ApplyXGRS_r_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IN var_n, GRGEN_MODEL.IN var_m, GRGEN_MODEL.IE var_e)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            bool res_46;
            bool res_44;
            bool res_38;
            bool res_32;
            bool res_28;
            bool res_22;
            bool res_10;
                                                                        bool res_21;
            bool res_27;
                        bool res_31;
            bool res_37;
                                    bool res_43;
            bool res_45;
            int tmpvar_0x = 0;double tmpvar_1y = 0.0;GRGEN_MODEL.ENUM_Enu tmpvar_2z = (GRGEN_MODEL.ENUM_Enu)0;string tmpvar_3u = "";bool tmpvar_4v = false;
            if(Sequence_foo.ApplyXGRS_foo(procEnv, (int)42, (double)3.141, (GRGEN_MODEL.ENUM_Enu)de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.ENUM_Enu.hurz, (string)"S21-heiteitei", (bool)true, ref tmpvar_0x, ref tmpvar_1y, ref tmpvar_2z, ref tmpvar_3u, ref tmpvar_4v)) {
                procEnv.SetVariableValue("x", tmpvar_0x);
procEnv.SetVariableValue("y", tmpvar_1y);
procEnv.SetVariableValue("z", tmpvar_2z);
procEnv.SetVariableValue("u", tmpvar_3u);
procEnv.SetVariableValue("v", tmpvar_4v);

                res_10 = (bool)(true);
            } else {
                res_10 = (bool)(false);
            }
            int tmpvar_5 = 0;double tmpvar_6 = 0.0;GRGEN_MODEL.ENUM_Enu tmpvar_7 = (GRGEN_MODEL.ENUM_Enu)0;string tmpvar_8 = "";bool tmpvar_9 = false;
            if(Sequence_foo.ApplyXGRS_foo(procEnv, (int)GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(((GRGEN_LIBGR.IGraphElement)var_n), "i", ((GRGEN_LIBGR.IGraphElement)var_n).GetAttribute("i")), (double)GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(((GRGEN_LIBGR.IGraphElement)var_n), "d", ((GRGEN_LIBGR.IGraphElement)var_n).GetAttribute("d")), (GRGEN_MODEL.ENUM_Enu)GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(((GRGEN_LIBGR.IGraphElement)var_n), "enu", ((GRGEN_LIBGR.IGraphElement)var_n).GetAttribute("enu")), (string)GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(((GRGEN_LIBGR.IGraphElement)var_n), "s", ((GRGEN_LIBGR.IGraphElement)var_n).GetAttribute("s")), (bool)GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(((GRGEN_LIBGR.IGraphElement)var_n), "b", ((GRGEN_LIBGR.IGraphElement)var_n).GetAttribute("b")), ref tmpvar_5, ref tmpvar_6, ref tmpvar_7, ref tmpvar_8, ref tmpvar_9)) {
                res_21 = (bool)(true);
            } else {
                res_21 = (bool)(false);
            }
            res_22 = (bool)(res_21);
            object tmpvar_10nul = null;
            if(Sequence_bar.ApplyXGRS_bar(procEnv, (object)null, (object)GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(((GRGEN_LIBGR.IGraphElement)var_n), "o", ((GRGEN_LIBGR.IGraphElement)var_n).GetAttribute("o")), ref tmpvar_10nul)) {
                procEnv.SetVariableValue("nul", tmpvar_10nul);

                res_27 = (bool)(true);
            } else {
                res_27 = (bool)(false);
            }
            res_28 = (bool)(res_27);
            if(Sequence_isnull.ApplyXGRS_isnull(procEnv, (object)procEnv.GetVariableValue("x"))) {
                res_31 = (bool)(true);
            } else {
                res_31 = (bool)(false);
            }
            res_32 = (bool)(res_31);
            GRGEN_MODEL.IN tmpvar_11a = null;GRGEN_MODEL.IE tmpvar_12b = null;
            if(Sequence_bla.ApplyXGRS_bla(procEnv, (GRGEN_MODEL.IN)var_m, (GRGEN_MODEL.IE)var_e, ref tmpvar_11a, ref tmpvar_12b)) {
                procEnv.SetVariableValue("a", tmpvar_11a);
procEnv.SetVariableValue("b", tmpvar_12b);

                res_37 = (bool)(true);
            } else {
                res_37 = (bool)(false);
            }
            res_38 = (bool)(res_37);
            GRGEN_LIBGR.INode tmpvar_13a = null;GRGEN_LIBGR.IEdge tmpvar_14b = null;
            if(Sequence_blo.ApplyXGRS_blo(procEnv, (GRGEN_LIBGR.INode)var_m, (GRGEN_LIBGR.IEdge)var_e, ref tmpvar_13a, ref tmpvar_14b)) {
                procEnv.SetVariableValue("a", tmpvar_13a);
procEnv.SetVariableValue("b", tmpvar_14b);

                res_43 = (bool)(true);
            } else {
                res_43 = (bool)(false);
            }
            res_44 = (bool)(res_43);
            if(Sequence_huh.ApplyXGRS_huh(procEnv)) {
                res_45 = (bool)(true);
            } else {
                res_45 = (bool)(false);
            }
            res_46 = (bool)(res_45);
            return res_46;
        }

        public static bool ApplyXGRS_r_1(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IN var_n)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            bool res_65;
            bool res_61;
            bool res_57;
            bool res_53;
            bool res_51;
            bool res_49;
            bool res_47;
            Action_filterBase rule_filterBase = Action_filterBase.Instance;
            bool res_48;
            bool res_50;
            Action_filterBass rule_filterBass = Action_filterBass.Instance;
            bool res_52;
            bool res_56;
            Action_filterHass rule_filterHass = Action_filterHass.Instance;
                        bool res_60;
            bool res_58;
            bool res_59;
            bool res_64;
            bool res_62;
            bool res_63;
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches_47 = rule_filterBase.Match(procEnv, 1);
            MatchFilters.Filter_f1(procEnv, matches_47);
            procEnv.Matched(matches_47, false);
            if(matches_47.Count==0) {
                res_47 = (bool)(false);
            } else {
                res_47 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_47.Count;
                procEnv.Finishing(matches_47, false);
                Rule_filterBase.IMatch_filterBase match_47 = matches_47.FirstExact;
                rule_filterBase.Modify(procEnv, match_47);
                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_47, false);
            }
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches_48 = rule_filterBase.Match(procEnv, 1);
            MatchFilters.Filter_nomnomnom(procEnv, matches_48);
            procEnv.Matched(matches_48, false);
            if(matches_48.Count==0) {
                res_48 = (bool)(false);
            } else {
                res_48 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_48.Count;
                procEnv.Finishing(matches_48, false);
                Rule_filterBase.IMatch_filterBase match_48 = matches_48.FirstExact;
                rule_filterBase.Modify(procEnv, match_48);
                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_48, false);
            }
            res_49 = (bool)(res_48);
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches_50 = rule_filterBass.Match(procEnv, procEnv.MaxMatches);
            MatchFilters.Filter_f2(procEnv, matches_50);
            procEnv.Matched(matches_50, false);
            if(matches_50.Count==0) {
                res_50 = (bool)(false);
            } else {
                res_50 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_50.Count;
                procEnv.Finishing(matches_50, false);
                IEnumerator<Rule_filterBass.IMatch_filterBass> enum_50 = matches_50.GetEnumeratorExact();
                while(enum_50.MoveNext())
                {
                    Rule_filterBass.IMatch_filterBass match_50 = enum_50.Current;
                    if(match_50!=matches_50.FirstExact) procEnv.RewritingNextMatch();
                    rule_filterBass.Modify(procEnv, match_50);
                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;
                }
                procEnv.Finished(matches_50, false);
            }
            res_51 = (bool)(res_50);
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches_52 = rule_filterBass.Match(procEnv, procEnv.MaxMatches);
            MatchFilters.Filter_f3(procEnv, matches_52);
            procEnv.Matched(matches_52, false);
            if(matches_52.Count==0) {
                res_52 = (bool)(false);
            } else {
                res_52 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_52.Count;
                procEnv.Finishing(matches_52, false);
                int numchooserandomvar_52 = (int)procEnv.GetVariableValue("x");
                if(matches_52.Count < numchooserandomvar_52) numchooserandomvar_52 = matches_52.Count;
                for(int i = 0; i < numchooserandomvar_52; ++i)
                {
                    if(i != 0) procEnv.RewritingNextMatch();
                    Rule_filterBass.IMatch_filterBass match_52 = matches_52.RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(matches_52.Count));
                    rule_filterBass.Modify(procEnv, match_52);
                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;
                }
                procEnv.Finished(matches_52, false);
            }
            res_53 = (bool)(res_52);
            GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches_56 = rule_filterHass.Match(procEnv, 1, (GRGEN_MODEL.IN)procEnv.GetVariableValue("n"));
            MatchFilters.Filter_f4(procEnv, matches_56);
            procEnv.Matched(matches_56, false);
            if(matches_56.Count==0) {
                res_56 = (bool)(false);
            } else {
                res_56 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_56.Count;
                procEnv.Finishing(matches_56, false);
                Rule_filterHass.IMatch_filterHass match_56 = matches_56.FirstExact;
                GRGEN_MODEL.IE tmpvar_15ee; 
                rule_filterHass.Modify(procEnv, match_56, out tmpvar_15ee);
                procEnv.SetVariableValue("ee", tmpvar_15ee);

                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_56, false);
            }
            res_57 = (bool)(res_56);
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches_60 = rule_filterBase.Match(procEnv, procEnv.MaxMatches);
            MatchFilters.Filter_f1(procEnv, matches_60);
            if(matches_60.Count==0) {
                res_60 = (bool)(false);
            } else {
                res_60 = (bool)(true);
                matches_60 = (GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase>)matches_60.Clone();
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_60.Count;
                procEnv.Finishing(matches_60, false);
                int matchesTried_60 = 0;
                IEnumerator<Rule_filterBase.IMatch_filterBase> enum_60 = matches_60.GetEnumeratorExact();
                while(enum_60.MoveNext())
                {
                    Rule_filterBase.IMatch_filterBase match_60 = enum_60.Current;
                    ++matchesTried_60;
                    int transID_60 = procEnv.TransactionManager.StartTransaction();
                    int oldRewritesPerformed_60 = -1;
                    if(procEnv.PerformanceInfo!=null) oldRewritesPerformed_60 = procEnv.PerformanceInfo.RewritesPerformed;
                    procEnv.Matched(matches_60, false);
                    rule_filterBase.Modify(procEnv, match_60);
                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_60, false);
                    GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches_59 = rule_filterBase.Match(procEnv, 1);
                    MatchFilters.Filter_f1(procEnv, matches_59);
                    procEnv.Matched(matches_59, false);
                    if(matches_59.Count==0) {
                        res_59 = (bool)(false);
                    } else {
                        res_59 = (bool)(true);
                        if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_59.Count;
                        procEnv.Finishing(matches_59, false);
                        Rule_filterBase.IMatch_filterBase match_59 = matches_59.FirstExact;
                        rule_filterBase.Modify(procEnv, match_59);
                        if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                        procEnv.Finished(matches_59, false);
                    }
                    if(!res_59) {
                        procEnv.TransactionManager.Rollback(transID_60);
                        if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed_60;
                        if(matchesTried_60 < matches_60.Count) {
                            continue;
                        } else {
                            res_60 = (bool)(false);
                            break;
                        }
                    }
                    procEnv.TransactionManager.Commit(transID_60);
                    res_60 = (bool)(true);
                    break;
                }
            }
            res_61 = (bool)(res_60);
            res_64 = (bool)(false);
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches_62 = rule_filterBase.Match(procEnv, 1);
            MatchFilters.Filter_f1(procEnv, matches_62);
            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_62.Count;
            if(matches_62.Count!=0) {
                res_64 = (bool)(true);
            }
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches_63 = rule_filterBass.Match(procEnv, procEnv.MaxMatches);
            MatchFilters.Filter_f2(procEnv, matches_63);
            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_63.Count;
            if(matches_63.Count!=0) {
                res_64 = (bool)(true);
            }
            int total_match_to_apply_64 = 0;
            total_match_to_apply_64 += matches_62.Count;
            if(matches_63.Count>0) ++total_match_to_apply_64;
            total_match_to_apply_64 = GRGEN_LIBGR.Sequence.randomGenerator.Next(total_match_to_apply_64);
            int cur_total_match_64 = 0;
            bool first_rewrite_64 = true;
            if(matches_62.Count!=0 && cur_total_match_64<=total_match_to_apply_64) {
                if(cur_total_match_64==total_match_to_apply_64) {
                    Rule_filterBase.IMatch_filterBase match_62 = matches_62.FirstExact;
                    procEnv.Matched(matches_62, false);
                    procEnv.Finishing(matches_62, false);
                    if(!first_rewrite_64) procEnv.RewritingNextMatch();
                    rule_filterBase.Modify(procEnv, match_62);
                    if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                    first_rewrite_64 = false;
                }
                ++cur_total_match_64;
                procEnv.Finished(matches_62, false);
            }
            if(matches_63.Count!=0 && cur_total_match_64<=total_match_to_apply_64) {
                if(cur_total_match_64==total_match_to_apply_64) {
                    IEnumerator<Rule_filterBass.IMatch_filterBass> enum_63 = matches_63.GetEnumeratorExact();
                    while(enum_63.MoveNext())
                    {
                        Rule_filterBass.IMatch_filterBass match_63 = enum_63.Current;
                        procEnv.Matched(matches_63, false);
                        procEnv.Finishing(matches_63, false);
                        if(!first_rewrite_64) procEnv.RewritingNextMatch();
                        rule_filterBass.Modify(procEnv, match_63);
                        if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;
                        first_rewrite_64 = false;
                    }
                }
                ++cur_total_match_64;
                procEnv.Finished(matches_63, false);
            }
            res_65 = (bool)(res_64);
            return res_65;
        }

		static Rule_r() {
		}

		public interface IMatch_r : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IN node_n { get; }
			//Edges
			GRGEN_MODEL.IE edge_e { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_r : GRGEN_LGSP.ListElement<Match_r>, IMatch_r
		{
			public GRGEN_MODEL.IN node_n { get { return (GRGEN_MODEL.IN)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum r_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)r_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IE edge_e { get { return (GRGEN_MODEL.IE)_edge_e; } }
			public GRGEN_LGSP.LGSPEdge _edge_e;
			public enum r_EdgeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)r_EdgeNums.@e: return _edge_e;
				default: return null;
				}
			}
			
			public enum r_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum r_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum r_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum r_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum r_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_r.instance.pat_r; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_r(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_r(Match_r that)
			{
				_node_n = that._node_n;
				_edge_e = that._edge_e;
			}
			public Match_r()
			{
			}
		}

	}

	public class SequenceInfo_foo : GRGEN_LIBGR.ExternalDefinedSequenceInfo
	{
		private static SequenceInfo_foo instance = null;
		public static SequenceInfo_foo Instance { get { if (instance==null) { instance = new SequenceInfo_foo(); } return instance; } }

		private SequenceInfo_foo()
					: base(
						new String[] { "v1", "v2", "v3", "v4", "v5",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)), GRGEN_LIBGR.VarType.GetVarType(typeof(double)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.ENUM_Enu)), GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(bool)),  },
						new String[] { "r1", "r2", "r3", "r4", "r5",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)), GRGEN_LIBGR.VarType.GetVarType(typeof(double)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.ENUM_Enu)), GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(bool)),  },
						"foo",
						5
					  )
		{
		}
	}

	public class SequenceInfo_bar : GRGEN_LIBGR.ExternalDefinedSequenceInfo
	{
		private static SequenceInfo_bar instance = null;
		public static SequenceInfo_bar Instance { get { if (instance==null) { instance = new SequenceInfo_bar(); } return instance; } }

		private SequenceInfo_bar()
					: base(
						new String[] { "v1", "v2",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)), GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  },
						new String[] { "r1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  },
						"bar",
						6
					  )
		{
		}
	}

	public class SequenceInfo_isnull : GRGEN_LIBGR.ExternalDefinedSequenceInfo
	{
		private static SequenceInfo_isnull instance = null;
		public static SequenceInfo_isnull Instance { get { if (instance==null) { instance = new SequenceInfo_isnull(); } return instance; } }

		private SequenceInfo_isnull()
					: base(
						new String[] { "v1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  },
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						"isnull",
						7
					  )
		{
		}
	}

	public class SequenceInfo_bla : GRGEN_LIBGR.ExternalDefinedSequenceInfo
	{
		private static SequenceInfo_bla instance = null;
		public static SequenceInfo_bla Instance { get { if (instance==null) { instance = new SequenceInfo_bla(); } return instance; } }

		private SequenceInfo_bla()
					: base(
						new String[] { "v1", "v2",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, GRGEN_MODEL.EdgeType_E.typeVar,  },
						new String[] { "r1", "r2",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, GRGEN_MODEL.EdgeType_E.typeVar,  },
						"bla",
						8
					  )
		{
		}
	}

	public class SequenceInfo_blo : GRGEN_LIBGR.ExternalDefinedSequenceInfo
	{
		private static SequenceInfo_blo instance = null;
		public static SequenceInfo_blo Instance { get { if (instance==null) { instance = new SequenceInfo_blo(); } return instance; } }

		private SequenceInfo_blo()
					: base(
						new String[] { "v1", "v2",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.EdgeType_Edge.typeVar,  },
						new String[] { "r1", "r2",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.EdgeType_Edge.typeVar,  },
						"blo",
						9
					  )
		{
		}
	}

	public class SequenceInfo_huh : GRGEN_LIBGR.ExternalDefinedSequenceInfo
	{
		private static SequenceInfo_huh instance = null;
		public static SequenceInfo_huh Instance { get { if (instance==null) { instance = new SequenceInfo_huh(); } return instance; } }

		private SequenceInfo_huh()
					: base(
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						"huh",
						10
					  )
		{
		}
	}

	public class SequenceInfo_counterExample1 : GRGEN_LIBGR.DefinedSequenceInfo
	{
		private static SequenceInfo_counterExample1 instance = null;
		public static SequenceInfo_counterExample1 Instance { get { if (instance==null) { instance = new SequenceInfo_counterExample1(); } return instance; } }

		private SequenceInfo_counterExample1()
					: base(
						new String[] { "v1", "v2",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)), GRGEN_MODEL.NodeType_Node.typeVar,  },
						new String[] { "r1", "r2",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)), GRGEN_MODEL.NodeType_Node.typeVar,  },
						"counterExample1",
						"{r1=v1;r2=v2}",
						12
					  )
		{
		}
	}

	public class SequenceInfo_counterExample2 : GRGEN_LIBGR.DefinedSequenceInfo
	{
		private static SequenceInfo_counterExample2 instance = null;
		public static SequenceInfo_counterExample2 Instance { get { if (instance==null) { instance = new SequenceInfo_counterExample2(); } return instance; } }

		private SequenceInfo_counterExample2()
					: base(
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						"counterExample2",
						"true",
						13
					  )
		{
		}
	}

	public class ExternalFiltersAndSequences_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public ExternalFiltersAndSequences_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[5];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+5];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[8];
			rules[0] = Rule_filterBase.Instance;
			rulesAndSubpatterns[0+0] = Rule_filterBase.Instance;
			rules[1] = Rule_filterBass.Instance;
			rulesAndSubpatterns[0+1] = Rule_filterBass.Instance;
			rules[2] = Rule_filterHass.Instance;
			rulesAndSubpatterns[0+2] = Rule_filterHass.Instance;
			rules[3] = Rule_init.Instance;
			rulesAndSubpatterns[0+3] = Rule_init.Instance;
			rules[4] = Rule_r.Instance;
			rulesAndSubpatterns[0+4] = Rule_r.Instance;
			definedSequences[0] = SequenceInfo_foo.Instance;
			definedSequences[1] = SequenceInfo_bar.Instance;
			definedSequences[2] = SequenceInfo_isnull.Instance;
			definedSequences[3] = SequenceInfo_bla.Instance;
			definedSequences[4] = SequenceInfo_blo.Instance;
			definedSequences[5] = SequenceInfo_huh.Instance;
			definedSequences[6] = SequenceInfo_counterExample1.Instance;
			definedSequences[7] = SequenceInfo_counterExample2.Instance;
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
    public interface IAction_filterBase
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_filterBase.IMatch_filterBase match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches);
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
    
    public class Action_filterBase : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_filterBase
    {
        public Action_filterBase() {
            _rulePattern = Rule_filterBase.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_filterBase.Match_filterBase, Rule_filterBase.IMatch_filterBase>(this);
        }

        public Rule_filterBase _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "filterBase"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_filterBase.Match_filterBase, Rule_filterBase.IMatch_filterBase> matches;

        public static Action_filterBase Instance { get { return instance; } }
        private static Action_filterBase instance = new Action_filterBase();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Rule_filterBase.Match_filterBase match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_filterBase.IMatch_filterBase match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
        {
            foreach(Rule_filterBase.IMatch_filterBase match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_filterBase.IMatch_filterBase match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches;
            
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
            
            Modify(actionEnv, (Rule_filterBase.IMatch_filterBase)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase>)matches);
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                case "f1": MatchFilters.Filter_f1((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase>)matches); break;
                case "nomnomnom": MatchFilters.Filter_nomnomnom((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase>)matches); break;
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_filterBass
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_filterBass.IMatch_filterBass match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches);
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
    
    public class Action_filterBass : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_filterBass
    {
        public Action_filterBass() {
            _rulePattern = Rule_filterBass.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_filterBass.Match_filterBass, Rule_filterBass.IMatch_filterBass>(this);
        }

        public Rule_filterBass _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "filterBass"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_filterBass.Match_filterBass, Rule_filterBass.IMatch_filterBass> matches;

        public static Action_filterBass Instance { get { return instance; } }
        private static Action_filterBass instance = new Action_filterBass();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Lookup filterBass_edge_e 
            int type_id_candidate_filterBass_edge_e = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_filterBass_edge_e = graph.edgesByTypeHeads[type_id_candidate_filterBass_edge_e], candidate_filterBass_edge_e = head_candidate_filterBass_edge_e.lgspTypeNext; candidate_filterBass_edge_e != head_candidate_filterBass_edge_e; candidate_filterBass_edge_e = candidate_filterBass_edge_e.lgspTypeNext)
            {
                // Implicit Source filterBass_node_n from filterBass_edge_e 
                GRGEN_LGSP.LGSPNode candidate_filterBass_node_n = candidate_filterBass_edge_e.lgspSource;
                if(candidate_filterBass_node_n.lgspType.TypeID!=1) {
                    continue;
                }
                if(candidate_filterBass_edge_e.lgspSource != candidate_filterBass_node_n) {
                    continue;
                }
                if(candidate_filterBass_edge_e.lgspTarget != candidate_filterBass_node_n) {
                    continue;
                }
                Rule_filterBass.Match_filterBass match = matches.GetNextUnfilledPosition();
                match._node_n = candidate_filterBass_node_n;
                match._edge_e = candidate_filterBass_edge_e;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_filterBass_edge_e);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_filterBass.IMatch_filterBass match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
        {
            foreach(Rule_filterBass.IMatch_filterBass match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_filterBass.IMatch_filterBass match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches;
            
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
            
            Modify(actionEnv, (Rule_filterBass.IMatch_filterBass)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass>)matches);
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                case "f2": MatchFilters.Filter_f2((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass>)matches); break;
                case "f3": MatchFilters.Filter_f3((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass>)matches); break;
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_filterHass
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IN filterHass_node_n);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_filterHass.IMatch_filterHass match, out GRGEN_MODEL.IE output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches, out GRGEN_MODEL.IE output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n, ref GRGEN_MODEL.IE output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n, ref GRGEN_MODEL.IE output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IN filterHass_node_n);
    }
    
    public class Action_filterHass : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_filterHass
    {
        public Action_filterHass() {
            _rulePattern = Rule_filterHass.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_filterHass.Match_filterHass, Rule_filterHass.IMatch_filterHass>(this);
        }

        public Rule_filterHass _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "filterHass"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_filterHass.Match_filterHass, Rule_filterHass.IMatch_filterHass> matches;

        public static Action_filterHass Instance { get { return instance; } }
        private static Action_filterHass instance = new Action_filterHass();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IN filterHass_node_n)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Preset filterHass_node_n 
            GRGEN_LGSP.LGSPNode candidate_filterHass_node_n = (GRGEN_LGSP.LGSPNode)filterHass_node_n;
            if(candidate_filterHass_node_n.lgspType.TypeID!=1) {
                return matches;
            }
            // Extend Outgoing filterHass_edge_e from filterHass_node_n 
            GRGEN_LGSP.LGSPEdge head_candidate_filterHass_edge_e = candidate_filterHass_node_n.lgspOuthead;
            if(head_candidate_filterHass_edge_e != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_filterHass_edge_e = head_candidate_filterHass_edge_e;
                do
                {
                    if(candidate_filterHass_edge_e.lgspType.TypeID!=3) {
                        continue;
                    }
                    if(candidate_filterHass_edge_e.lgspTarget != candidate_filterHass_node_n) {
                        continue;
                    }
                    Rule_filterHass.Match_filterHass match = matches.GetNextUnfilledPosition();
                    match._node_n = candidate_filterHass_node_n;
                    match._edge_e = candidate_filterHass_edge_e;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_filterHass_node_n.MoveOutHeadAfter(candidate_filterHass_edge_e);
                        return matches;
                    }
                }
                while( (candidate_filterHass_edge_e = candidate_filterHass_edge_e.lgspOutNext) != head_candidate_filterHass_edge_e );
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IN filterHass_node_n);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IN filterHass_node_n)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, filterHass_node_n);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_filterHass.IMatch_filterHass match, out GRGEN_MODEL.IE output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches, out GRGEN_MODEL.IE output_0)
        {
            output_0 = null;
            foreach(Rule_filterHass.IMatch_filterHass match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n, ref GRGEN_MODEL.IE output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, filterHass_node_n);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n, ref GRGEN_MODEL.IE output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, filterHass_node_n);
            if(matches.Count <= 0) return false;
            foreach(Rule_filterHass.IMatch_filterHass match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches;
            GRGEN_MODEL.IE output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, filterHass_node_n);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IN filterHass_node_n)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, filterHass_node_n);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IE output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, filterHass_node_n);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IN filterHass_node_n)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches;
            GRGEN_MODEL.IE output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, filterHass_node_n);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IN) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IE output_0; 
            Modify(actionEnv, (Rule_filterHass.IMatch_filterHass)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IE output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IE output_0 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IN) parameters[0], ref output_0)) {
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
            GRGEN_MODEL.IE output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IN) parameters[0], ref output_0)) {
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
            return ApplyStar(actionEnv, (GRGEN_MODEL.IN) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IN) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IN) parameters[0]);
        }
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                case "f4": MatchFilters.Filter_f4((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass>)matches); break;
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_init
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_init.IMatch_init match, out GRGEN_MODEL.IN output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches, out GRGEN_MODEL.IN output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IN output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IN output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_init : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_init
    {
        public Action_init() {
            _rulePattern = Rule_init.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_init.Match_init, Rule_init.IMatch_init>(this);
        }

        public Rule_init _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "init"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_init.Match_init, Rule_init.IMatch_init> matches;

        public static Action_init Instance { get { return instance; } }
        private static Action_init instance = new Action_init();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_init.IMatch_init match, out GRGEN_MODEL.IN output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches, out GRGEN_MODEL.IN output_0)
        {
            output_0 = null;
            foreach(Rule_init.IMatch_init match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IN output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IN output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_init.IMatch_init match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches;
            GRGEN_MODEL.IN output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IN output_0; 
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
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches;
            GRGEN_MODEL.IN output_0; 
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
            GRGEN_MODEL.IN output_0; 
            Modify(actionEnv, (Rule_init.IMatch_init)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IN output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_MODEL.IN output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IN output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_MODEL.IN output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IN output_0 = null; 
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_r : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_r
    {
        public Action_r() {
            _rulePattern = Rule_r.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_r.Match_r, Rule_r.IMatch_r>(this);
        }

        public Rule_r _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "r"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_r.Match_r, Rule_r.IMatch_r> matches;

        public static Action_r Instance { get { return instance; } }
        private static Action_r instance = new Action_r();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Lookup r_edge_e 
            int type_id_candidate_r_edge_e = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_r_edge_e = graph.edgesByTypeHeads[type_id_candidate_r_edge_e], candidate_r_edge_e = head_candidate_r_edge_e.lgspTypeNext; candidate_r_edge_e != head_candidate_r_edge_e; candidate_r_edge_e = candidate_r_edge_e.lgspTypeNext)
            {
                // Implicit Source r_node_n from r_edge_e 
                GRGEN_LGSP.LGSPNode candidate_r_node_n = candidate_r_edge_e.lgspSource;
                if(candidate_r_node_n.lgspType.TypeID!=1) {
                    continue;
                }
                if(candidate_r_edge_e.lgspSource != candidate_r_node_n) {
                    continue;
                }
                if(candidate_r_edge_e.lgspTarget != candidate_r_node_n) {
                    continue;
                }
                Rule_r.Match_r match = matches.GetNextUnfilledPosition();
                match._node_n = candidate_r_node_n;
                match._edge_e = candidate_r_edge_e;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_r_edge_e);
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
            foreach(Rule_r.IMatch_r match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_r.IMatch_r match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r>)matches);
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }


    public class Sequence_counterExample1 : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_counterExample1 instance = null;
        public static Sequence_counterExample1 Instance { get { if(instance==null) instance = new Sequence_counterExample1(); return instance; } }
        private Sequence_counterExample1() : base("counterExample1", SequenceInfo_counterExample1.Instance) { }

        public static bool ApplyXGRS_counterExample1(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int var_v1, GRGEN_LIBGR.INode var_v2, ref int var_r1, ref GRGEN_LIBGR.INode var_r2)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            bool res_75;
            object res_74;
            object res_69;
            object res_66;
            object res_73;
            object res_70;
            var_r1 = (int)(var_v1);
            res_66 = var_r1;
            res_69 = res_66;
            var_r2 = (GRGEN_LIBGR.INode)(var_v2);
            res_70 = var_r2;
            res_73 = res_70;
            res_74 = res_73;
            res_75 = (bool)(true);
            return res_75;
        }

        public static bool Apply_counterExample1(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int var_v1, GRGEN_LIBGR.INode var_v2, ref int var_r1, ref GRGEN_LIBGR.INode var_r2)
        {
            int vari_r1 = 0;
            GRGEN_LIBGR.INode vari_r2 = null;
            bool result = ApplyXGRS_counterExample1((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            if(result) {
                var_r1 = vari_r1;
                var_r2 = vari_r2;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;
            int var_v1 = (int)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_LIBGR.INode var_v2 = (GRGEN_LIBGR.INode)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            int var_r1 = 0;
            GRGEN_LIBGR.INode var_r2 = null;
            bool result = ApplyXGRS_counterExample1((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_r1, procEnv);
                sequenceInvocation.ReturnVars[1].SetVariableValue(var_r2, procEnv);
            }
            return result;
        }
    }

    public class Sequence_counterExample2 : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_counterExample2 instance = null;
        public static Sequence_counterExample2 Instance { get { if(instance==null) instance = new Sequence_counterExample2(); return instance; } }
        private Sequence_counterExample2() : base("counterExample2", SequenceInfo_counterExample2.Instance) { }

        public static bool ApplyXGRS_counterExample2(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            bool res_78;
            object res_77;
            res_77 = true;
            res_78 = (bool)(!GRGEN_LIBGR.TypesHelper.IsDefaultValue(res_77));
            return res_78;
        }

        public static bool Apply_counterExample2(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            bool result = ApplyXGRS_counterExample2((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;
            bool result = ApplyXGRS_counterExample2((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            return result;
        }
    }

    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class ExternalFiltersAndSequencesActions : GRGEN_LGSP.LGSPActions
    {
        public ExternalFiltersAndSequencesActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public ExternalFiltersAndSequencesActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(Rule_filterBase.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_filterBase.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_filterBase.Instance);
            actions.Add("filterBase", (GRGEN_LGSP.LGSPAction) Action_filterBase.Instance);
            @filterBase = Action_filterBase.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_filterBass.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_filterBass.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_filterBass.Instance);
            actions.Add("filterBass", (GRGEN_LGSP.LGSPAction) Action_filterBass.Instance);
            @filterBass = Action_filterBass.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_filterHass.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_filterHass.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_filterHass.Instance);
            actions.Add("filterHass", (GRGEN_LGSP.LGSPAction) Action_filterHass.Instance);
            @filterHass = Action_filterHass.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_init.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_init.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_init.Instance);
            actions.Add("init", (GRGEN_LGSP.LGSPAction) Action_init.Instance);
            @init = Action_init.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_r.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_r.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_r.Instance);
            actions.Add("r", (GRGEN_LGSP.LGSPAction) Action_r.Instance);
            @r = Action_r.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_filterBase.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_filterBass.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_filterHass.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_init.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_r.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_filterBase.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_filterBass.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_filterHass.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_init.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_r.Instance.patternGraph);
            Rule_filterBase.Instance.patternGraph.maxNegLevel = 0;
            Rule_filterBass.Instance.patternGraph.maxNegLevel = 0;
            Rule_filterHass.Instance.patternGraph.maxNegLevel = 0;
            Rule_init.Instance.patternGraph.maxNegLevel = 0;
            Rule_r.Instance.patternGraph.maxNegLevel = 0;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_filterBase.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_filterBass.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_filterHass.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_init.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_r.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
            RegisterGraphRewriteSequenceDefinition(Sequence_foo.Instance);
            @foo = Sequence_foo.Instance;
            RegisterGraphRewriteSequenceDefinition(Sequence_bar.Instance);
            @bar = Sequence_bar.Instance;
            RegisterGraphRewriteSequenceDefinition(Sequence_isnull.Instance);
            @isnull = Sequence_isnull.Instance;
            RegisterGraphRewriteSequenceDefinition(Sequence_bla.Instance);
            @bla = Sequence_bla.Instance;
            RegisterGraphRewriteSequenceDefinition(Sequence_blo.Instance);
            @blo = Sequence_blo.Instance;
            RegisterGraphRewriteSequenceDefinition(Sequence_huh.Instance);
            @huh = Sequence_huh.Instance;
            RegisterGraphRewriteSequenceDefinition(Sequence_counterExample1.Instance);
            @counterExample1 = Sequence_counterExample1.Instance;
            RegisterGraphRewriteSequenceDefinition(Sequence_counterExample2.Instance);
            @counterExample2 = Sequence_counterExample2.Instance;
        }
        
        public IAction_filterBase @filterBase;
        public IAction_filterBass @filterBass;
        public IAction_filterHass @filterHass;
        public IAction_init @init;
        public IAction_r @r;
        
        public Sequence_foo @foo;
        public Sequence_bar @bar;
        public Sequence_isnull @isnull;
        public Sequence_bla @bla;
        public Sequence_blo @blo;
        public Sequence_huh @huh;
        public Sequence_counterExample1 @counterExample1;
        public Sequence_counterExample2 @counterExample2;
        
        public override string Name { get { return "ExternalFiltersAndSequencesActions"; } }
        public override string ModelMD5Hash { get { return "26c6289e588caaff2e0c8ff5315f5a38"; } }
    }
}