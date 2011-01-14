// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ProgramGraphs\ProgramGraphsOriginal.grg" on Mon Jan 10 21:50:09 CET 2011

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ProgramGraphsOriginal;

namespace de.unika.ipd.grGen.Action_ProgramGraphsOriginal
{
	public class Pattern_Subclasses : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Subclasses instance = null;
		public static Pattern_Subclasses Instance { get { if (instance==null) { instance = new Pattern_Subclasses(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Subclasses_node_c_AllowedTypes = null;
		public static bool[] Subclasses_node_c_IsAllowedType = null;
		public enum Subclasses_NodeNums { @c, };
		public enum Subclasses_EdgeNums { };
		public enum Subclasses_VariableNums { };
		public enum Subclasses_SubNums { };
		public enum Subclasses_AltNums { };
		public enum Subclasses_IterNums { @iter_0, };



		public GRGEN_LGSP.PatternGraph pat_Subclasses;

		public static GRGEN_LIBGR.NodeType[] Subclasses_iter_0_node_sub_AllowedTypes = null;
		public static bool[] Subclasses_iter_0_node_sub_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Subclasses_iter_0_edge__edge0_AllowedTypes = null;
		public static bool[] Subclasses_iter_0_edge__edge0_IsAllowedType = null;
		public enum Subclasses_iter_0_NodeNums { @c, @sub, };
		public enum Subclasses_iter_0_EdgeNums { @_edge0, };
		public enum Subclasses_iter_0_VariableNums { };
		public enum Subclasses_iter_0_SubNums { @_sub0, };
		public enum Subclasses_iter_0_AltNums { };
		public enum Subclasses_iter_0_IterNums { };



		public GRGEN_LGSP.PatternGraph Subclasses_iter_0;


		private Pattern_Subclasses()
		{
			name = "Subclasses";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Class.typeVar, };
			inputNames = new string[] { "Subclasses_node_c", };

		}
		private void initialize()
		{
			bool[,] Subclasses_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Subclasses_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Subclasses_minMatches = new int[1] {
				0, 
			};
			int[] Subclasses_maxMatches = new int[1] {
				0, 
			};
			GRGEN_LGSP.PatternNode Subclasses_node_c = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "Subclasses_node_c", "c", Subclasses_node_c_AllowedTypes, Subclasses_node_c_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] Subclasses_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Subclasses_iter_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] Subclasses_iter_0_minMatches = new int[0] ;
			int[] Subclasses_iter_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Subclasses_iter_0_node_sub = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "Subclasses_iter_0_node_sub", "sub", Subclasses_iter_0_node_sub_AllowedTypes, Subclasses_iter_0_node_sub_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Subclasses_iter_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Subclasses_iter_0_edge__edge0", "_edge0", Subclasses_iter_0_edge__edge0_AllowedTypes, Subclasses_iter_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding Subclasses_iter_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Subclass.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Subclasses_iter_0_node_sub"),
				}, 
				new string[] { "Subclasses_iter_0_node_sub" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Subclasses_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"Subclasses_",
				false,
				new GRGEN_LGSP.PatternNode[] { Subclasses_node_c, Subclasses_iter_0_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { Subclasses_iter_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Subclasses_iter_0__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Subclasses_iter_0_minMatches,
				Subclasses_iter_0_maxMatches,
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
				Subclasses_iter_0_isNodeHomomorphicGlobal,
				Subclasses_iter_0_isEdgeHomomorphicGlobal
			);
			Subclasses_iter_0.edgeToSourceNode.Add(Subclasses_iter_0_edge__edge0, Subclasses_node_c);
			Subclasses_iter_0.edgeToTargetNode.Add(Subclasses_iter_0_edge__edge0, Subclasses_iter_0_node_sub);

			pat_Subclasses = new GRGEN_LGSP.PatternGraph(
				"Subclasses",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Subclasses_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Subclasses_iter_0,  }, 
				Subclasses_minMatches,
				Subclasses_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Subclasses_isNodeHomomorphicGlobal,
				Subclasses_isEdgeHomomorphicGlobal
			);
			Subclasses_iter_0.embeddingGraph = pat_Subclasses;

			Subclasses_node_c.pointOfDefinition = null;
			Subclasses_iter_0_node_sub.pointOfDefinition = Subclasses_iter_0;
			Subclasses_iter_0_edge__edge0.pointOfDefinition = Subclasses_iter_0;
			Subclasses_iter_0__sub0.PointOfDefinition = Subclasses_iter_0;

			patternGraph = pat_Subclasses;
		}


		public void Subclasses_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_c)
		{
			graph.SettingAddedNodeNames( create_Subclasses_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Subclasses_addedEdgeNames );
		}
		private static string[] create_Subclasses_addedNodeNames = new string[] {  };
		private static string[] create_Subclasses_addedEdgeNames = new string[] {  };

		public void Subclasses_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Subclasses curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_Subclasses_iter_0, IMatch_Subclasses_iter_0> iterated_iter_0 = curMatch._iter_0;
			Subclasses_iter_0_Delete(graph, iterated_iter_0);
		}

		public void Subclasses_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_Subclasses_iter_0, IMatch_Subclasses_iter_0> curMatches)
		{
			for(Match_Subclasses_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				Subclasses_iter_0_Delete(graph, curMatch);
			}
		}

		public void Subclasses_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Subclasses_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch._node_c;
			GRGEN_LGSP.LGSPNode node_sub = curMatch._node_sub;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_Subclass.Match_Subclass subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
			graph.RemoveEdges(node_sub);
			graph.Remove(node_sub);
			Pattern_Subclass.Instance.Subclass_Delete(graph, subpattern__sub0);
		}

		static Pattern_Subclasses() {
		}

		public interface IMatch_Subclasses : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_Subclasses_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Subclasses_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			GRGEN_MODEL.IClass node_sub { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_Subclass.Match_Subclass @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Subclasses : GRGEN_LGSP.ListElement<Match_Subclasses>, IMatch_Subclasses
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public enum Subclasses_NodeNums { @c, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Subclasses_NodeNums.@c: return _node_c;
				default: return null;
				}
			}
			
			public enum Subclasses_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclasses_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclasses_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclasses_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_Subclasses_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_Subclasses_iter_0, IMatch_Subclasses_iter_0> _iter_0;
			public enum Subclasses_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)Subclasses_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum Subclasses_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Subclasses.instance.pat_Subclasses; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Subclasses_iter_0 : GRGEN_LGSP.ListElement<Match_Subclasses_iter_0>, IMatch_Subclasses_iter_0
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_MODEL.IClass node_sub { get { return (GRGEN_MODEL.IClass)_node_sub; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public GRGEN_LGSP.LGSPNode _node_sub;
			public enum Subclasses_iter_0_NodeNums { @c, @sub, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Subclasses_iter_0_NodeNums.@c: return _node_c;
				case (int)Subclasses_iter_0_NodeNums.@sub: return _node_sub;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum Subclasses_iter_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Subclasses_iter_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum Subclasses_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Subclass.Match_Subclass @_sub0 { get { return @__sub0; } }
			public @Pattern_Subclass.Match_Subclass @__sub0;
			public enum Subclasses_iter_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Subclasses_iter_0_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Subclasses_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclasses_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclasses_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Subclasses.instance.Subclasses_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Subclass : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Subclass instance = null;
		public static Pattern_Subclass Instance { get { if (instance==null) { instance = new Pattern_Subclass(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Subclass_node_sub_AllowedTypes = null;
		public static bool[] Subclass_node_sub_IsAllowedType = null;
		public enum Subclass_NodeNums { @sub, };
		public enum Subclass_EdgeNums { };
		public enum Subclass_VariableNums { };
		public enum Subclass_SubNums { @_sub0, @_sub1, };
		public enum Subclass_AltNums { };
		public enum Subclass_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_Subclass;


		private Pattern_Subclass()
		{
			name = "Subclass";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Class.typeVar, };
			inputNames = new string[] { "Subclass_node_sub", };

		}
		private void initialize()
		{
			bool[,] Subclass_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Subclass_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Subclass_minMatches = new int[0] ;
			int[] Subclass_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Subclass_node_sub = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "Subclass_node_sub", "sub", Subclass_node_sub_AllowedTypes, Subclass_node_sub_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding Subclass__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Features.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Subclass_node_sub"),
				}, 
				new string[] { "Subclass_node_sub" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternGraphEmbedding Subclass__sub1 = new GRGEN_LGSP.PatternGraphEmbedding("_sub1", Pattern_Subclasses.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Subclass_node_sub"),
				}, 
				new string[] { "Subclass_node_sub" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_Subclass = new GRGEN_LGSP.PatternGraph(
				"Subclass",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Subclass_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Subclass__sub0, Subclass__sub1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Subclass_minMatches,
				Subclass_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Subclass_isNodeHomomorphicGlobal,
				Subclass_isEdgeHomomorphicGlobal
			);

			Subclass_node_sub.pointOfDefinition = null;
			Subclass__sub0.PointOfDefinition = pat_Subclass;
			Subclass__sub1.PointOfDefinition = pat_Subclass;

			patternGraph = pat_Subclass;
		}


		public void Subclass_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_sub)
		{
			graph.SettingAddedNodeNames( create_Subclass_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Subclass_addedEdgeNames );
			Pattern_Features.Instance.Features_Create(graph, (GRGEN_MODEL.@Class)(node_sub));
			Pattern_Subclasses.Instance.Subclasses_Create(graph, (GRGEN_MODEL.@Class)(node_sub));
		}
		private static string[] create_Subclass_addedNodeNames = new string[] {  };
		private static string[] create_Subclass_addedEdgeNames = new string[] {  };

		public void Subclass_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Subclass curMatch)
		{
			Pattern_Features.Match_Features subpattern__sub0 = curMatch.@__sub0;
			Pattern_Subclasses.Match_Subclasses subpattern__sub1 = curMatch.@__sub1;
			Pattern_Features.Instance.Features_Delete(graph, subpattern__sub0);
			Pattern_Subclasses.Instance.Subclasses_Delete(graph, subpattern__sub1);
		}

		static Pattern_Subclass() {
		}

		public interface IMatch_Subclass : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_sub { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_Features.Match_Features @_sub0 { get; }
			@Pattern_Subclasses.Match_Subclasses @_sub1 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Subclass : GRGEN_LGSP.ListElement<Match_Subclass>, IMatch_Subclass
		{
			public GRGEN_MODEL.IClass node_sub { get { return (GRGEN_MODEL.IClass)_node_sub; } }
			public GRGEN_LGSP.LGSPNode _node_sub;
			public enum Subclass_NodeNums { @sub, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Subclass_NodeNums.@sub: return _node_sub;
				default: return null;
				}
			}
			
			public enum Subclass_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclass_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Features.Match_Features @_sub0 { get { return @__sub0; } }
			public @Pattern_Subclasses.Match_Subclasses @_sub1 { get { return @__sub1; } }
			public @Pattern_Features.Match_Features @__sub0;
			public @Pattern_Subclasses.Match_Subclasses @__sub1;
			public enum Subclass_SubNums { @_sub0, @_sub1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 2;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Subclass_SubNums.@_sub0: return __sub0;
				case (int)Subclass_SubNums.@_sub1: return __sub1;
				default: return null;
				}
			}
			
			public enum Subclass_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclass_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Subclass_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Subclass.instance.pat_Subclass; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Features : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Features instance = null;
		public static Pattern_Features Instance { get { if (instance==null) { instance = new Pattern_Features(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Features_node_c_AllowedTypes = null;
		public static bool[] Features_node_c_IsAllowedType = null;
		public enum Features_NodeNums { @c, };
		public enum Features_EdgeNums { };
		public enum Features_VariableNums { };
		public enum Features_SubNums { };
		public enum Features_AltNums { };
		public enum Features_IterNums { @iter_0, };



		public GRGEN_LGSP.PatternGraph pat_Features;

		public enum Features_iter_0_NodeNums { @c, };
		public enum Features_iter_0_EdgeNums { };
		public enum Features_iter_0_VariableNums { };
		public enum Features_iter_0_SubNums { @_sub0, };
		public enum Features_iter_0_AltNums { };
		public enum Features_iter_0_IterNums { };



		public GRGEN_LGSP.PatternGraph Features_iter_0;


		private Pattern_Features()
		{
			name = "Features";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Class.typeVar, };
			inputNames = new string[] { "Features_node_c", };

		}
		private void initialize()
		{
			bool[,] Features_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Features_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Features_minMatches = new int[1] {
				0, 
			};
			int[] Features_maxMatches = new int[1] {
				0, 
			};
			GRGEN_LGSP.PatternNode Features_node_c = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "Features_node_c", "c", Features_node_c_AllowedTypes, Features_node_c_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] Features_iter_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Features_iter_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Features_iter_0_minMatches = new int[0] ;
			int[] Features_iter_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternGraphEmbedding Features_iter_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_FeaturePattern.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Features_node_c"),
				}, 
				new string[] { "Features_node_c" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Features_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"Features_",
				false,
				new GRGEN_LGSP.PatternNode[] { Features_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Features_iter_0__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Features_iter_0_minMatches,
				Features_iter_0_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Features_iter_0_isNodeHomomorphicGlobal,
				Features_iter_0_isEdgeHomomorphicGlobal
			);

			pat_Features = new GRGEN_LGSP.PatternGraph(
				"Features",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Features_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Features_iter_0,  }, 
				Features_minMatches,
				Features_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Features_isNodeHomomorphicGlobal,
				Features_isEdgeHomomorphicGlobal
			);
			Features_iter_0.embeddingGraph = pat_Features;

			Features_node_c.pointOfDefinition = null;
			Features_iter_0__sub0.PointOfDefinition = Features_iter_0;

			patternGraph = pat_Features;
		}


		public void Features_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_c)
		{
			graph.SettingAddedNodeNames( create_Features_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Features_addedEdgeNames );
		}
		private static string[] create_Features_addedNodeNames = new string[] {  };
		private static string[] create_Features_addedEdgeNames = new string[] {  };

		public void Features_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Features curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_Features_iter_0, IMatch_Features_iter_0> iterated_iter_0 = curMatch._iter_0;
			Features_iter_0_Delete(graph, iterated_iter_0);
		}

		public void Features_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_Features_iter_0, IMatch_Features_iter_0> curMatches)
		{
			for(Match_Features_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				Features_iter_0_Delete(graph, curMatch);
			}
		}

		public void Features_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Features_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch._node_c;
			Pattern_FeaturePattern.Match_FeaturePattern subpattern__sub0 = curMatch.@__sub0;
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
			Pattern_FeaturePattern.Instance.FeaturePattern_Delete(graph, subpattern__sub0);
		}

		static Pattern_Features() {
		}

		public interface IMatch_Features : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_Features_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Features_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_FeaturePattern.Match_FeaturePattern @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Features : GRGEN_LGSP.ListElement<Match_Features>, IMatch_Features
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public enum Features_NodeNums { @c, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Features_NodeNums.@c: return _node_c;
				default: return null;
				}
			}
			
			public enum Features_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Features_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Features_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Features_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_Features_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_Features_iter_0, IMatch_Features_iter_0> _iter_0;
			public enum Features_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)Features_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum Features_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Features.instance.pat_Features; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Features_iter_0 : GRGEN_LGSP.ListElement<Match_Features_iter_0>, IMatch_Features_iter_0
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public enum Features_iter_0_NodeNums { @c, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Features_iter_0_NodeNums.@c: return _node_c;
				default: return null;
				}
			}
			
			public enum Features_iter_0_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Features_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_FeaturePattern.Match_FeaturePattern @_sub0 { get { return @__sub0; } }
			public @Pattern_FeaturePattern.Match_FeaturePattern @__sub0;
			public enum Features_iter_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Features_iter_0_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Features_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Features_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Features_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Features.instance.Features_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_FeaturePattern : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_FeaturePattern instance = null;
		public static Pattern_FeaturePattern Instance { get { if (instance==null) { instance = new Pattern_FeaturePattern(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_node_c_AllowedTypes = null;
		public static bool[] FeaturePattern_node_c_IsAllowedType = null;
		public enum FeaturePattern_NodeNums { @c, };
		public enum FeaturePattern_EdgeNums { };
		public enum FeaturePattern_VariableNums { };
		public enum FeaturePattern_SubNums { };
		public enum FeaturePattern_AltNums { @alt_0, };
		public enum FeaturePattern_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_FeaturePattern;

		public enum FeaturePattern_alt_0_CaseNums { @MethodBody, @MethodSignature, @Variable, @Konstante, };
		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_MethodBody_node_b_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodBody_node_b_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_MethodBody_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodBody_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_MethodBody_NodeNums { @c, @b, };
		public enum FeaturePattern_alt_0_MethodBody_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_MethodBody_VariableNums { };
		public enum FeaturePattern_alt_0_MethodBody_SubNums { @_sub0, @_sub1, };
		public enum FeaturePattern_alt_0_MethodBody_AltNums { };
		public enum FeaturePattern_alt_0_MethodBody_IterNums { };



		public GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_MethodBody;

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_MethodSignature_node__node0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodSignature_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_MethodSignature_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodSignature_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_MethodSignature_NodeNums { @c, @_node0, };
		public enum FeaturePattern_alt_0_MethodSignature_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_MethodSignature_VariableNums { };
		public enum FeaturePattern_alt_0_MethodSignature_SubNums { };
		public enum FeaturePattern_alt_0_MethodSignature_AltNums { };
		public enum FeaturePattern_alt_0_MethodSignature_IterNums { };



		public GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_MethodSignature;

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_Variable_node__node0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Variable_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_Variable_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Variable_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_Variable_NodeNums { @c, @_node0, };
		public enum FeaturePattern_alt_0_Variable_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_Variable_VariableNums { };
		public enum FeaturePattern_alt_0_Variable_SubNums { };
		public enum FeaturePattern_alt_0_Variable_AltNums { };
		public enum FeaturePattern_alt_0_Variable_IterNums { };



		public GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_Variable;

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_Konstante_node__node0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Konstante_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_Konstante_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Konstante_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_Konstante_NodeNums { @c, @_node0, };
		public enum FeaturePattern_alt_0_Konstante_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_Konstante_VariableNums { };
		public enum FeaturePattern_alt_0_Konstante_SubNums { };
		public enum FeaturePattern_alt_0_Konstante_AltNums { };
		public enum FeaturePattern_alt_0_Konstante_IterNums { };



		public GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_Konstante;


		private Pattern_FeaturePattern()
		{
			name = "FeaturePattern";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Class.typeVar, };
			inputNames = new string[] { "FeaturePattern_node_c", };

		}
		private void initialize()
		{
			bool[,] FeaturePattern_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] FeaturePattern_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] FeaturePattern_minMatches = new int[0] ;
			int[] FeaturePattern_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode FeaturePattern_node_c = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "FeaturePattern_node_c", "c", FeaturePattern_node_c_AllowedTypes, FeaturePattern_node_c_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] FeaturePattern_alt_0_MethodBody_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] FeaturePattern_alt_0_MethodBody_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] FeaturePattern_alt_0_MethodBody_minMatches = new int[0] ;
			int[] FeaturePattern_alt_0_MethodBody_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_MethodBody_node_b = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "FeaturePattern_alt_0_MethodBody_node_b", "b", FeaturePattern_alt_0_MethodBody_node_b_AllowedTypes, FeaturePattern_alt_0_MethodBody_node_b_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_MethodBody_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "FeaturePattern_alt_0_MethodBody_edge__edge0", "_edge0", FeaturePattern_alt_0_MethodBody_edge__edge0_AllowedTypes, FeaturePattern_alt_0_MethodBody_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding FeaturePattern_alt_0_MethodBody__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Parameters.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("FeaturePattern_alt_0_MethodBody_node_b"),
				}, 
				new string[] { "FeaturePattern_alt_0_MethodBody_node_b" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternGraphEmbedding FeaturePattern_alt_0_MethodBody__sub1 = new GRGEN_LGSP.PatternGraphEmbedding("_sub1", Pattern_Statements.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("FeaturePattern_alt_0_MethodBody_node_b"),
				}, 
				new string[] { "FeaturePattern_alt_0_MethodBody_node_b" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			FeaturePattern_alt_0_MethodBody = new GRGEN_LGSP.PatternGraph(
				"MethodBody",
				"FeaturePattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { FeaturePattern_node_c, FeaturePattern_alt_0_MethodBody_node_b }, 
				new GRGEN_LGSP.PatternEdge[] { FeaturePattern_alt_0_MethodBody_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { FeaturePattern_alt_0_MethodBody__sub0, FeaturePattern_alt_0_MethodBody__sub1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				FeaturePattern_alt_0_MethodBody_minMatches,
				FeaturePattern_alt_0_MethodBody_maxMatches,
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
				FeaturePattern_alt_0_MethodBody_isNodeHomomorphicGlobal,
				FeaturePattern_alt_0_MethodBody_isEdgeHomomorphicGlobal
			);
			FeaturePattern_alt_0_MethodBody.edgeToSourceNode.Add(FeaturePattern_alt_0_MethodBody_edge__edge0, FeaturePattern_node_c);
			FeaturePattern_alt_0_MethodBody.edgeToTargetNode.Add(FeaturePattern_alt_0_MethodBody_edge__edge0, FeaturePattern_alt_0_MethodBody_node_b);

			bool[,] FeaturePattern_alt_0_MethodSignature_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] FeaturePattern_alt_0_MethodSignature_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] FeaturePattern_alt_0_MethodSignature_minMatches = new int[0] ;
			int[] FeaturePattern_alt_0_MethodSignature_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_MethodSignature_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodSignature, "GRGEN_MODEL.IMethodSignature", "FeaturePattern_alt_0_MethodSignature_node__node0", "_node0", FeaturePattern_alt_0_MethodSignature_node__node0_AllowedTypes, FeaturePattern_alt_0_MethodSignature_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_MethodSignature_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "FeaturePattern_alt_0_MethodSignature_edge__edge0", "_edge0", FeaturePattern_alt_0_MethodSignature_edge__edge0_AllowedTypes, FeaturePattern_alt_0_MethodSignature_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			FeaturePattern_alt_0_MethodSignature = new GRGEN_LGSP.PatternGraph(
				"MethodSignature",
				"FeaturePattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { FeaturePattern_node_c, FeaturePattern_alt_0_MethodSignature_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { FeaturePattern_alt_0_MethodSignature_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				FeaturePattern_alt_0_MethodSignature_minMatches,
				FeaturePattern_alt_0_MethodSignature_maxMatches,
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
				FeaturePattern_alt_0_MethodSignature_isNodeHomomorphicGlobal,
				FeaturePattern_alt_0_MethodSignature_isEdgeHomomorphicGlobal
			);
			FeaturePattern_alt_0_MethodSignature.edgeToSourceNode.Add(FeaturePattern_alt_0_MethodSignature_edge__edge0, FeaturePattern_node_c);
			FeaturePattern_alt_0_MethodSignature.edgeToTargetNode.Add(FeaturePattern_alt_0_MethodSignature_edge__edge0, FeaturePattern_alt_0_MethodSignature_node__node0);

			bool[,] FeaturePattern_alt_0_Variable_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] FeaturePattern_alt_0_Variable_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] FeaturePattern_alt_0_Variable_minMatches = new int[0] ;
			int[] FeaturePattern_alt_0_Variable_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_Variable_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Variabel, "GRGEN_MODEL.IVariabel", "FeaturePattern_alt_0_Variable_node__node0", "_node0", FeaturePattern_alt_0_Variable_node__node0_AllowedTypes, FeaturePattern_alt_0_Variable_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_Variable_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "FeaturePattern_alt_0_Variable_edge__edge0", "_edge0", FeaturePattern_alt_0_Variable_edge__edge0_AllowedTypes, FeaturePattern_alt_0_Variable_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			FeaturePattern_alt_0_Variable = new GRGEN_LGSP.PatternGraph(
				"Variable",
				"FeaturePattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { FeaturePattern_node_c, FeaturePattern_alt_0_Variable_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { FeaturePattern_alt_0_Variable_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				FeaturePattern_alt_0_Variable_minMatches,
				FeaturePattern_alt_0_Variable_maxMatches,
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
				FeaturePattern_alt_0_Variable_isNodeHomomorphicGlobal,
				FeaturePattern_alt_0_Variable_isEdgeHomomorphicGlobal
			);
			FeaturePattern_alt_0_Variable.edgeToSourceNode.Add(FeaturePattern_alt_0_Variable_edge__edge0, FeaturePattern_node_c);
			FeaturePattern_alt_0_Variable.edgeToTargetNode.Add(FeaturePattern_alt_0_Variable_edge__edge0, FeaturePattern_alt_0_Variable_node__node0);

			bool[,] FeaturePattern_alt_0_Konstante_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] FeaturePattern_alt_0_Konstante_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] FeaturePattern_alt_0_Konstante_minMatches = new int[0] ;
			int[] FeaturePattern_alt_0_Konstante_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_Konstante_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Constant, "GRGEN_MODEL.IConstant", "FeaturePattern_alt_0_Konstante_node__node0", "_node0", FeaturePattern_alt_0_Konstante_node__node0_AllowedTypes, FeaturePattern_alt_0_Konstante_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_Konstante_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "FeaturePattern_alt_0_Konstante_edge__edge0", "_edge0", FeaturePattern_alt_0_Konstante_edge__edge0_AllowedTypes, FeaturePattern_alt_0_Konstante_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			FeaturePattern_alt_0_Konstante = new GRGEN_LGSP.PatternGraph(
				"Konstante",
				"FeaturePattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { FeaturePattern_node_c, FeaturePattern_alt_0_Konstante_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { FeaturePattern_alt_0_Konstante_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				FeaturePattern_alt_0_Konstante_minMatches,
				FeaturePattern_alt_0_Konstante_maxMatches,
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
				FeaturePattern_alt_0_Konstante_isNodeHomomorphicGlobal,
				FeaturePattern_alt_0_Konstante_isEdgeHomomorphicGlobal
			);
			FeaturePattern_alt_0_Konstante.edgeToSourceNode.Add(FeaturePattern_alt_0_Konstante_edge__edge0, FeaturePattern_node_c);
			FeaturePattern_alt_0_Konstante.edgeToTargetNode.Add(FeaturePattern_alt_0_Konstante_edge__edge0, FeaturePattern_alt_0_Konstante_node__node0);

			GRGEN_LGSP.Alternative FeaturePattern_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "FeaturePattern_", new GRGEN_LGSP.PatternGraph[] { FeaturePattern_alt_0_MethodBody, FeaturePattern_alt_0_MethodSignature, FeaturePattern_alt_0_Variable, FeaturePattern_alt_0_Konstante } );

			pat_FeaturePattern = new GRGEN_LGSP.PatternGraph(
				"FeaturePattern",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { FeaturePattern_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { FeaturePattern_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				FeaturePattern_minMatches,
				FeaturePattern_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				FeaturePattern_isNodeHomomorphicGlobal,
				FeaturePattern_isEdgeHomomorphicGlobal
			);
			FeaturePattern_alt_0_MethodBody.embeddingGraph = pat_FeaturePattern;
			FeaturePattern_alt_0_MethodSignature.embeddingGraph = pat_FeaturePattern;
			FeaturePattern_alt_0_Variable.embeddingGraph = pat_FeaturePattern;
			FeaturePattern_alt_0_Konstante.embeddingGraph = pat_FeaturePattern;

			FeaturePattern_node_c.pointOfDefinition = null;
			FeaturePattern_alt_0_MethodBody_node_b.pointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodBody_edge__edge0.pointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodBody__sub0.PointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodBody__sub1.PointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodSignature_node__node0.pointOfDefinition = FeaturePattern_alt_0_MethodSignature;
			FeaturePattern_alt_0_MethodSignature_edge__edge0.pointOfDefinition = FeaturePattern_alt_0_MethodSignature;
			FeaturePattern_alt_0_Variable_node__node0.pointOfDefinition = FeaturePattern_alt_0_Variable;
			FeaturePattern_alt_0_Variable_edge__edge0.pointOfDefinition = FeaturePattern_alt_0_Variable;
			FeaturePattern_alt_0_Konstante_node__node0.pointOfDefinition = FeaturePattern_alt_0_Konstante;
			FeaturePattern_alt_0_Konstante_edge__edge0.pointOfDefinition = FeaturePattern_alt_0_Konstante;

			patternGraph = pat_FeaturePattern;
		}


		public void FeaturePattern_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_c)
		{
			graph.SettingAddedNodeNames( create_FeaturePattern_addedNodeNames );
			graph.SettingAddedEdgeNames( create_FeaturePattern_addedEdgeNames );
		}
		private static string[] create_FeaturePattern_addedNodeNames = new string[] {  };
		private static string[] create_FeaturePattern_addedEdgeNames = new string[] {  };

		public void FeaturePattern_Delete(GRGEN_LGSP.LGSPGraph graph, Match_FeaturePattern curMatch)
		{
			IMatch_FeaturePattern_alt_0 alternative_alt_0 = curMatch._alt_0;
			FeaturePattern_alt_0_Delete(graph, alternative_alt_0);
		}

		public void FeaturePattern_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_FeaturePattern_alt_0 curMatch)
		{
			if(curMatch.Pattern == FeaturePattern_alt_0_MethodBody) {
				FeaturePattern_alt_0_MethodBody_Delete(graph, (Match_FeaturePattern_alt_0_MethodBody)curMatch);
				return;
			}
			else if(curMatch.Pattern == FeaturePattern_alt_0_MethodSignature) {
				FeaturePattern_alt_0_MethodSignature_Delete(graph, (Match_FeaturePattern_alt_0_MethodSignature)curMatch);
				return;
			}
			else if(curMatch.Pattern == FeaturePattern_alt_0_Variable) {
				FeaturePattern_alt_0_Variable_Delete(graph, (Match_FeaturePattern_alt_0_Variable)curMatch);
				return;
			}
			else if(curMatch.Pattern == FeaturePattern_alt_0_Konstante) {
				FeaturePattern_alt_0_Konstante_Delete(graph, (Match_FeaturePattern_alt_0_Konstante)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void FeaturePattern_alt_0_MethodBody_Delete(GRGEN_LGSP.LGSPGraph graph, Match_FeaturePattern_alt_0_MethodBody curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch._node_c;
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_Parameters.Match_Parameters subpattern__sub0 = curMatch.@__sub0;
			Pattern_Statements.Match_Statements subpattern__sub1 = curMatch.@__sub1;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_Parameters.Instance.Parameters_Delete(graph, subpattern__sub0);
			Pattern_Statements.Instance.Statements_Delete(graph, subpattern__sub1);
		}

		public void FeaturePattern_alt_0_MethodSignature_Delete(GRGEN_LGSP.LGSPGraph graph, Match_FeaturePattern_alt_0_MethodSignature curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch._node_c;
			GRGEN_LGSP.LGSPNode node__node0 = curMatch._node__node0;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
		}

		public void FeaturePattern_alt_0_Variable_Delete(GRGEN_LGSP.LGSPGraph graph, Match_FeaturePattern_alt_0_Variable curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch._node_c;
			GRGEN_LGSP.LGSPNode node__node0 = curMatch._node__node0;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
		}

		public void FeaturePattern_alt_0_Konstante_Delete(GRGEN_LGSP.LGSPGraph graph, Match_FeaturePattern_alt_0_Konstante curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch._node_c;
			GRGEN_LGSP.LGSPNode node__node0 = curMatch._node__node0;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
		}

		static Pattern_FeaturePattern() {
		}

		public interface IMatch_FeaturePattern : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_FeaturePattern_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_FeaturePattern_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_FeaturePattern_alt_0_MethodBody : IMatch_FeaturePattern_alt_0
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			GRGEN_MODEL.IMethodBody node_b { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_Parameters.Match_Parameters @_sub0 { get; }
			@Pattern_Statements.Match_Statements @_sub1 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_FeaturePattern_alt_0_MethodSignature : IMatch_FeaturePattern_alt_0
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			GRGEN_MODEL.IMethodSignature node__node0 { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_FeaturePattern_alt_0_Variable : IMatch_FeaturePattern_alt_0
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			GRGEN_MODEL.IVariabel node__node0 { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_FeaturePattern_alt_0_Konstante : IMatch_FeaturePattern_alt_0
		{
			//Nodes
			GRGEN_MODEL.IClass node_c { get; }
			GRGEN_MODEL.IConstant node__node0 { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_FeaturePattern : GRGEN_LGSP.ListElement<Match_FeaturePattern>, IMatch_FeaturePattern
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public enum FeaturePattern_NodeNums { @c, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_NodeNums.@c: return _node_c;
				default: return null;
				}
			}
			
			public enum FeaturePattern_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_FeaturePattern_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_FeaturePattern_alt_0 _alt_0;
			public enum FeaturePattern_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum FeaturePattern_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_FeaturePattern.instance.pat_FeaturePattern; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_FeaturePattern_alt_0_MethodBody : GRGEN_LGSP.ListElement<Match_FeaturePattern_alt_0_MethodBody>, IMatch_FeaturePattern_alt_0_MethodBody
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum FeaturePattern_alt_0_MethodBody_NodeNums { @c, @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_MethodBody_NodeNums.@c: return _node_c;
				case (int)FeaturePattern_alt_0_MethodBody_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum FeaturePattern_alt_0_MethodBody_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_MethodBody_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodBody_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Parameters.Match_Parameters @_sub0 { get { return @__sub0; } }
			public @Pattern_Statements.Match_Statements @_sub1 { get { return @__sub1; } }
			public @Pattern_Parameters.Match_Parameters @__sub0;
			public @Pattern_Statements.Match_Statements @__sub1;
			public enum FeaturePattern_alt_0_MethodBody_SubNums { @_sub0, @_sub1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 2;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_MethodBody_SubNums.@_sub0: return __sub0;
				case (int)FeaturePattern_alt_0_MethodBody_SubNums.@_sub1: return __sub1;
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodBody_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodBody_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodBody_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_FeaturePattern.instance.FeaturePattern_alt_0_MethodBody; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_FeaturePattern_alt_0_MethodSignature : GRGEN_LGSP.ListElement<Match_FeaturePattern_alt_0_MethodSignature>, IMatch_FeaturePattern_alt_0_MethodSignature
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_MODEL.IMethodSignature node__node0 { get { return (GRGEN_MODEL.IMethodSignature)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum FeaturePattern_alt_0_MethodSignature_NodeNums { @c, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_MethodSignature_NodeNums.@c: return _node_c;
				case (int)FeaturePattern_alt_0_MethodSignature_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum FeaturePattern_alt_0_MethodSignature_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_MethodSignature_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodSignature_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodSignature_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodSignature_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodSignature_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_MethodSignature_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_FeaturePattern.instance.FeaturePattern_alt_0_MethodSignature; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_FeaturePattern_alt_0_Variable : GRGEN_LGSP.ListElement<Match_FeaturePattern_alt_0_Variable>, IMatch_FeaturePattern_alt_0_Variable
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_MODEL.IVariabel node__node0 { get { return (GRGEN_MODEL.IVariabel)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum FeaturePattern_alt_0_Variable_NodeNums { @c, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_Variable_NodeNums.@c: return _node_c;
				case (int)FeaturePattern_alt_0_Variable_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum FeaturePattern_alt_0_Variable_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_Variable_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Variable_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Variable_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Variable_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Variable_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Variable_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_FeaturePattern.instance.FeaturePattern_alt_0_Variable; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_FeaturePattern_alt_0_Konstante : GRGEN_LGSP.ListElement<Match_FeaturePattern_alt_0_Konstante>, IMatch_FeaturePattern_alt_0_Konstante
		{
			public GRGEN_MODEL.IClass node_c { get { return (GRGEN_MODEL.IClass)_node_c; } }
			public GRGEN_MODEL.IConstant node__node0 { get { return (GRGEN_MODEL.IConstant)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_c;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum FeaturePattern_alt_0_Konstante_NodeNums { @c, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_Konstante_NodeNums.@c: return _node_c;
				case (int)FeaturePattern_alt_0_Konstante_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum FeaturePattern_alt_0_Konstante_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)FeaturePattern_alt_0_Konstante_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Konstante_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Konstante_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Konstante_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Konstante_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum FeaturePattern_alt_0_Konstante_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_FeaturePattern.instance.FeaturePattern_alt_0_Konstante; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Parameters : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Parameters instance = null;
		public static Pattern_Parameters Instance { get { if (instance==null) { instance = new Pattern_Parameters(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Parameters_node_b_AllowedTypes = null;
		public static bool[] Parameters_node_b_IsAllowedType = null;
		public enum Parameters_NodeNums { @b, };
		public enum Parameters_EdgeNums { };
		public enum Parameters_VariableNums { };
		public enum Parameters_SubNums { };
		public enum Parameters_AltNums { };
		public enum Parameters_IterNums { @iter_0, };



		public GRGEN_LGSP.PatternGraph pat_Parameters;

		public enum Parameters_iter_0_NodeNums { @b, };
		public enum Parameters_iter_0_EdgeNums { };
		public enum Parameters_iter_0_VariableNums { };
		public enum Parameters_iter_0_SubNums { @_sub0, };
		public enum Parameters_iter_0_AltNums { };
		public enum Parameters_iter_0_IterNums { };



		public GRGEN_LGSP.PatternGraph Parameters_iter_0;


		private Pattern_Parameters()
		{
			name = "Parameters";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "Parameters_node_b", };

		}
		private void initialize()
		{
			bool[,] Parameters_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Parameters_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Parameters_minMatches = new int[1] {
				0, 
			};
			int[] Parameters_maxMatches = new int[1] {
				0, 
			};
			GRGEN_LGSP.PatternNode Parameters_node_b = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "Parameters_node_b", "b", Parameters_node_b_AllowedTypes, Parameters_node_b_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] Parameters_iter_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Parameters_iter_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Parameters_iter_0_minMatches = new int[0] ;
			int[] Parameters_iter_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternGraphEmbedding Parameters_iter_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Parameter.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Parameters_node_b"),
				}, 
				new string[] { "Parameters_node_b" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Parameters_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"Parameters_",
				false,
				new GRGEN_LGSP.PatternNode[] { Parameters_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Parameters_iter_0__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Parameters_iter_0_minMatches,
				Parameters_iter_0_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Parameters_iter_0_isNodeHomomorphicGlobal,
				Parameters_iter_0_isEdgeHomomorphicGlobal
			);

			pat_Parameters = new GRGEN_LGSP.PatternGraph(
				"Parameters",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Parameters_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Parameters_iter_0,  }, 
				Parameters_minMatches,
				Parameters_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Parameters_isNodeHomomorphicGlobal,
				Parameters_isEdgeHomomorphicGlobal
			);
			Parameters_iter_0.embeddingGraph = pat_Parameters;

			Parameters_node_b.pointOfDefinition = null;
			Parameters_iter_0__sub0.PointOfDefinition = Parameters_iter_0;

			patternGraph = pat_Parameters;
		}


		public void Parameters_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_Parameters_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Parameters_addedEdgeNames );
		}
		private static string[] create_Parameters_addedNodeNames = new string[] {  };
		private static string[] create_Parameters_addedEdgeNames = new string[] {  };

		public void Parameters_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Parameters curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_Parameters_iter_0, IMatch_Parameters_iter_0> iterated_iter_0 = curMatch._iter_0;
			Parameters_iter_0_Delete(graph, iterated_iter_0);
		}

		public void Parameters_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_Parameters_iter_0, IMatch_Parameters_iter_0> curMatches)
		{
			for(Match_Parameters_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				Parameters_iter_0_Delete(graph, curMatch);
			}
		}

		public void Parameters_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Parameters_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			Pattern_Parameter.Match_Parameter subpattern__sub0 = curMatch.@__sub0;
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_Parameter.Instance.Parameter_Delete(graph, subpattern__sub0);
		}

		static Pattern_Parameters() {
		}

		public interface IMatch_Parameters : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_Parameters_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Parameters_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_Parameter.Match_Parameter @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Parameters : GRGEN_LGSP.ListElement<Match_Parameters>, IMatch_Parameters
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum Parameters_NodeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Parameters_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public enum Parameters_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameters_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameters_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameters_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_Parameters_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_Parameters_iter_0, IMatch_Parameters_iter_0> _iter_0;
			public enum Parameters_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)Parameters_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum Parameters_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Parameters.instance.pat_Parameters; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Parameters_iter_0 : GRGEN_LGSP.ListElement<Match_Parameters_iter_0>, IMatch_Parameters_iter_0
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum Parameters_iter_0_NodeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Parameters_iter_0_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public enum Parameters_iter_0_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameters_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Parameter.Match_Parameter @_sub0 { get { return @__sub0; } }
			public @Pattern_Parameter.Match_Parameter @__sub0;
			public enum Parameters_iter_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Parameters_iter_0_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Parameters_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameters_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameters_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Parameters.instance.Parameters_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Parameter : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Parameter instance = null;
		public static Pattern_Parameter Instance { get { if (instance==null) { instance = new Pattern_Parameter(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Parameter_node_b_AllowedTypes = null;
		public static bool[] Parameter_node_b_IsAllowedType = null;
		public enum Parameter_NodeNums { @b, };
		public enum Parameter_EdgeNums { };
		public enum Parameter_VariableNums { };
		public enum Parameter_SubNums { };
		public enum Parameter_AltNums { @alt_0, };
		public enum Parameter_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_Parameter;

		public enum Parameter_alt_0_CaseNums { @Variable, @Konstante, };
		public static GRGEN_LIBGR.NodeType[] Parameter_alt_0_Variable_node_v_AllowedTypes = null;
		public static bool[] Parameter_alt_0_Variable_node_v_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Parameter_alt_0_Variable_edge__edge0_AllowedTypes = null;
		public static bool[] Parameter_alt_0_Variable_edge__edge0_IsAllowedType = null;
		public enum Parameter_alt_0_Variable_NodeNums { @b, @v, };
		public enum Parameter_alt_0_Variable_EdgeNums { @_edge0, };
		public enum Parameter_alt_0_Variable_VariableNums { };
		public enum Parameter_alt_0_Variable_SubNums { };
		public enum Parameter_alt_0_Variable_AltNums { };
		public enum Parameter_alt_0_Variable_IterNums { };



		public GRGEN_LGSP.PatternGraph Parameter_alt_0_Variable;

		public static GRGEN_LIBGR.NodeType[] Parameter_alt_0_Konstante_node_c_AllowedTypes = null;
		public static bool[] Parameter_alt_0_Konstante_node_c_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Parameter_alt_0_Konstante_edge__edge0_AllowedTypes = null;
		public static bool[] Parameter_alt_0_Konstante_edge__edge0_IsAllowedType = null;
		public enum Parameter_alt_0_Konstante_NodeNums { @b, @c, };
		public enum Parameter_alt_0_Konstante_EdgeNums { @_edge0, };
		public enum Parameter_alt_0_Konstante_VariableNums { };
		public enum Parameter_alt_0_Konstante_SubNums { };
		public enum Parameter_alt_0_Konstante_AltNums { };
		public enum Parameter_alt_0_Konstante_IterNums { };



		public GRGEN_LGSP.PatternGraph Parameter_alt_0_Konstante;


		private Pattern_Parameter()
		{
			name = "Parameter";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "Parameter_node_b", };

		}
		private void initialize()
		{
			bool[,] Parameter_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Parameter_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Parameter_minMatches = new int[0] ;
			int[] Parameter_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Parameter_node_b = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "Parameter_node_b", "b", Parameter_node_b_AllowedTypes, Parameter_node_b_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] Parameter_alt_0_Variable_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Parameter_alt_0_Variable_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] Parameter_alt_0_Variable_minMatches = new int[0] ;
			int[] Parameter_alt_0_Variable_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Parameter_alt_0_Variable_node_v = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Variabel, "GRGEN_MODEL.IVariabel", "Parameter_alt_0_Variable_node_v", "v", Parameter_alt_0_Variable_node_v_AllowedTypes, Parameter_alt_0_Variable_node_v_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Parameter_alt_0_Variable_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Parameter_alt_0_Variable_edge__edge0", "_edge0", Parameter_alt_0_Variable_edge__edge0_AllowedTypes, Parameter_alt_0_Variable_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			Parameter_alt_0_Variable = new GRGEN_LGSP.PatternGraph(
				"Variable",
				"Parameter_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Parameter_node_b, Parameter_alt_0_Variable_node_v }, 
				new GRGEN_LGSP.PatternEdge[] { Parameter_alt_0_Variable_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Parameter_alt_0_Variable_minMatches,
				Parameter_alt_0_Variable_maxMatches,
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
				Parameter_alt_0_Variable_isNodeHomomorphicGlobal,
				Parameter_alt_0_Variable_isEdgeHomomorphicGlobal
			);
			Parameter_alt_0_Variable.edgeToSourceNode.Add(Parameter_alt_0_Variable_edge__edge0, Parameter_node_b);
			Parameter_alt_0_Variable.edgeToTargetNode.Add(Parameter_alt_0_Variable_edge__edge0, Parameter_alt_0_Variable_node_v);

			bool[,] Parameter_alt_0_Konstante_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Parameter_alt_0_Konstante_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] Parameter_alt_0_Konstante_minMatches = new int[0] ;
			int[] Parameter_alt_0_Konstante_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Parameter_alt_0_Konstante_node_c = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Constant, "GRGEN_MODEL.IConstant", "Parameter_alt_0_Konstante_node_c", "c", Parameter_alt_0_Konstante_node_c_AllowedTypes, Parameter_alt_0_Konstante_node_c_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Parameter_alt_0_Konstante_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Parameter_alt_0_Konstante_edge__edge0", "_edge0", Parameter_alt_0_Konstante_edge__edge0_AllowedTypes, Parameter_alt_0_Konstante_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			Parameter_alt_0_Konstante = new GRGEN_LGSP.PatternGraph(
				"Konstante",
				"Parameter_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Parameter_node_b, Parameter_alt_0_Konstante_node_c }, 
				new GRGEN_LGSP.PatternEdge[] { Parameter_alt_0_Konstante_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Parameter_alt_0_Konstante_minMatches,
				Parameter_alt_0_Konstante_maxMatches,
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
				Parameter_alt_0_Konstante_isNodeHomomorphicGlobal,
				Parameter_alt_0_Konstante_isEdgeHomomorphicGlobal
			);
			Parameter_alt_0_Konstante.edgeToSourceNode.Add(Parameter_alt_0_Konstante_edge__edge0, Parameter_node_b);
			Parameter_alt_0_Konstante.edgeToTargetNode.Add(Parameter_alt_0_Konstante_edge__edge0, Parameter_alt_0_Konstante_node_c);

			GRGEN_LGSP.Alternative Parameter_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "Parameter_", new GRGEN_LGSP.PatternGraph[] { Parameter_alt_0_Variable, Parameter_alt_0_Konstante } );

			pat_Parameter = new GRGEN_LGSP.PatternGraph(
				"Parameter",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Parameter_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { Parameter_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Parameter_minMatches,
				Parameter_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Parameter_isNodeHomomorphicGlobal,
				Parameter_isEdgeHomomorphicGlobal
			);
			Parameter_alt_0_Variable.embeddingGraph = pat_Parameter;
			Parameter_alt_0_Konstante.embeddingGraph = pat_Parameter;

			Parameter_node_b.pointOfDefinition = null;
			Parameter_alt_0_Variable_node_v.pointOfDefinition = Parameter_alt_0_Variable;
			Parameter_alt_0_Variable_edge__edge0.pointOfDefinition = Parameter_alt_0_Variable;
			Parameter_alt_0_Konstante_node_c.pointOfDefinition = Parameter_alt_0_Konstante;
			Parameter_alt_0_Konstante_edge__edge0.pointOfDefinition = Parameter_alt_0_Konstante;

			patternGraph = pat_Parameter;
		}


		public void Parameter_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_Parameter_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Parameter_addedEdgeNames );
		}
		private static string[] create_Parameter_addedNodeNames = new string[] {  };
		private static string[] create_Parameter_addedEdgeNames = new string[] {  };

		public void Parameter_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Parameter curMatch)
		{
			IMatch_Parameter_alt_0 alternative_alt_0 = curMatch._alt_0;
			Parameter_alt_0_Delete(graph, alternative_alt_0);
		}

		public void Parameter_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_Parameter_alt_0 curMatch)
		{
			if(curMatch.Pattern == Parameter_alt_0_Variable) {
				Parameter_alt_0_Variable_Delete(graph, (Match_Parameter_alt_0_Variable)curMatch);
				return;
			}
			else if(curMatch.Pattern == Parameter_alt_0_Konstante) {
				Parameter_alt_0_Konstante_Delete(graph, (Match_Parameter_alt_0_Konstante)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void Parameter_alt_0_Variable_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Parameter_alt_0_Variable curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPNode node_v = curMatch._node_v;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			graph.RemoveEdges(node_v);
			graph.Remove(node_v);
		}

		public void Parameter_alt_0_Konstante_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Parameter_alt_0_Konstante curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPNode node_c = curMatch._node_c;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
		}

		static Pattern_Parameter() {
		}

		public interface IMatch_Parameter : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_Parameter_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Parameter_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Parameter_alt_0_Variable : IMatch_Parameter_alt_0
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			GRGEN_MODEL.IVariabel node_v { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Parameter_alt_0_Konstante : IMatch_Parameter_alt_0
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			GRGEN_MODEL.IConstant node_c { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Parameter : GRGEN_LGSP.ListElement<Match_Parameter>, IMatch_Parameter
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum Parameter_NodeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Parameter_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public enum Parameter_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_Parameter_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_Parameter_alt_0 _alt_0;
			public enum Parameter_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)Parameter_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum Parameter_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Parameter.instance.pat_Parameter; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Parameter_alt_0_Variable : GRGEN_LGSP.ListElement<Match_Parameter_alt_0_Variable>, IMatch_Parameter_alt_0_Variable
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_MODEL.IVariabel node_v { get { return (GRGEN_MODEL.IVariabel)_node_v; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_v;
			public enum Parameter_alt_0_Variable_NodeNums { @b, @v, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Parameter_alt_0_Variable_NodeNums.@b: return _node_b;
				case (int)Parameter_alt_0_Variable_NodeNums.@v: return _node_v;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum Parameter_alt_0_Variable_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Parameter_alt_0_Variable_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Variable_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Variable_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Variable_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Variable_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Variable_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Parameter.instance.Parameter_alt_0_Variable; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Parameter_alt_0_Konstante : GRGEN_LGSP.ListElement<Match_Parameter_alt_0_Konstante>, IMatch_Parameter_alt_0_Konstante
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_MODEL.IConstant node_c { get { return (GRGEN_MODEL.IConstant)_node_c; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_c;
			public enum Parameter_alt_0_Konstante_NodeNums { @b, @c, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Parameter_alt_0_Konstante_NodeNums.@b: return _node_b;
				case (int)Parameter_alt_0_Konstante_NodeNums.@c: return _node_c;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum Parameter_alt_0_Konstante_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Parameter_alt_0_Konstante_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Konstante_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Konstante_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Konstante_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Konstante_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Parameter_alt_0_Konstante_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Parameter.instance.Parameter_alt_0_Konstante; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Statements : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Statements instance = null;
		public static Pattern_Statements Instance { get { if (instance==null) { instance = new Pattern_Statements(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Statements_node_b_AllowedTypes = null;
		public static bool[] Statements_node_b_IsAllowedType = null;
		public enum Statements_NodeNums { @b, };
		public enum Statements_EdgeNums { };
		public enum Statements_VariableNums { };
		public enum Statements_SubNums { };
		public enum Statements_AltNums { };
		public enum Statements_IterNums { @iter_0, };



		public GRGEN_LGSP.PatternGraph pat_Statements;

		public enum Statements_iter_0_NodeNums { @b, };
		public enum Statements_iter_0_EdgeNums { };
		public enum Statements_iter_0_VariableNums { };
		public enum Statements_iter_0_SubNums { @_sub0, };
		public enum Statements_iter_0_AltNums { };
		public enum Statements_iter_0_IterNums { };



		public GRGEN_LGSP.PatternGraph Statements_iter_0;


		private Pattern_Statements()
		{
			name = "Statements";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "Statements_node_b", };

		}
		private void initialize()
		{
			bool[,] Statements_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Statements_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Statements_minMatches = new int[1] {
				0, 
			};
			int[] Statements_maxMatches = new int[1] {
				0, 
			};
			GRGEN_LGSP.PatternNode Statements_node_b = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "Statements_node_b", "b", Statements_node_b_AllowedTypes, Statements_node_b_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] Statements_iter_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Statements_iter_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Statements_iter_0_minMatches = new int[0] ;
			int[] Statements_iter_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternGraphEmbedding Statements_iter_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Statement.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Statements_node_b"),
				}, 
				new string[] { "Statements_node_b" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Statements_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"Statements_",
				false,
				new GRGEN_LGSP.PatternNode[] { Statements_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Statements_iter_0__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Statements_iter_0_minMatches,
				Statements_iter_0_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Statements_iter_0_isNodeHomomorphicGlobal,
				Statements_iter_0_isEdgeHomomorphicGlobal
			);

			pat_Statements = new GRGEN_LGSP.PatternGraph(
				"Statements",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Statements_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Statements_iter_0,  }, 
				Statements_minMatches,
				Statements_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Statements_isNodeHomomorphicGlobal,
				Statements_isEdgeHomomorphicGlobal
			);
			Statements_iter_0.embeddingGraph = pat_Statements;

			Statements_node_b.pointOfDefinition = null;
			Statements_iter_0__sub0.PointOfDefinition = Statements_iter_0;

			patternGraph = pat_Statements;
		}


		public void Statements_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_Statements_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Statements_addedEdgeNames );
		}
		private static string[] create_Statements_addedNodeNames = new string[] {  };
		private static string[] create_Statements_addedEdgeNames = new string[] {  };

		public void Statements_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Statements curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_Statements_iter_0, IMatch_Statements_iter_0> iterated_iter_0 = curMatch._iter_0;
			Statements_iter_0_Delete(graph, iterated_iter_0);
		}

		public void Statements_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_Statements_iter_0, IMatch_Statements_iter_0> curMatches)
		{
			for(Match_Statements_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				Statements_iter_0_Delete(graph, curMatch);
			}
		}

		public void Statements_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Statements_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			Pattern_Statement.Match_Statement subpattern__sub0 = curMatch.@__sub0;
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_Statement.Instance.Statement_Delete(graph, subpattern__sub0);
		}

		static Pattern_Statements() {
		}

		public interface IMatch_Statements : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_Statements_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Statements_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_Statement.Match_Statement @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Statements : GRGEN_LGSP.ListElement<Match_Statements>, IMatch_Statements
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum Statements_NodeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Statements_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public enum Statements_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statements_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statements_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statements_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_Statements_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_Statements_iter_0, IMatch_Statements_iter_0> _iter_0;
			public enum Statements_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)Statements_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum Statements_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Statements.instance.pat_Statements; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Statements_iter_0 : GRGEN_LGSP.ListElement<Match_Statements_iter_0>, IMatch_Statements_iter_0
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum Statements_iter_0_NodeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Statements_iter_0_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public enum Statements_iter_0_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statements_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Statement.Match_Statement @_sub0 { get { return @__sub0; } }
			public @Pattern_Statement.Match_Statement @__sub0;
			public enum Statements_iter_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Statements_iter_0_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Statements_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statements_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statements_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Statements.instance.Statements_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Statement : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Statement instance = null;
		public static Pattern_Statement Instance { get { if (instance==null) { instance = new Pattern_Statement(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Statement_node_b_AllowedTypes = null;
		public static bool[] Statement_node_b_IsAllowedType = null;
		public enum Statement_NodeNums { @b, };
		public enum Statement_EdgeNums { };
		public enum Statement_VariableNums { };
		public enum Statement_SubNums { };
		public enum Statement_AltNums { @alt_0, };
		public enum Statement_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_Statement;

		public enum Statement_alt_0_CaseNums { @Assignment, @Call, @Return, };
		public static GRGEN_LIBGR.NodeType[] Statement_alt_0_Assignment_node_e_AllowedTypes = null;
		public static bool[] Statement_alt_0_Assignment_node_e_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Assignment_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Assignment_edge__edge1_AllowedTypes = null;
		public static bool[] Statement_alt_0_Assignment_edge__edge0_IsAllowedType = null;
		public static bool[] Statement_alt_0_Assignment_edge__edge1_IsAllowedType = null;
		public enum Statement_alt_0_Assignment_NodeNums { @b, @e, };
		public enum Statement_alt_0_Assignment_EdgeNums { @_edge0, @_edge1, };
		public enum Statement_alt_0_Assignment_VariableNums { };
		public enum Statement_alt_0_Assignment_SubNums { @_sub0, };
		public enum Statement_alt_0_Assignment_AltNums { };
		public enum Statement_alt_0_Assignment_IterNums { };



		public GRGEN_LGSP.PatternGraph Statement_alt_0_Assignment;

		public static GRGEN_LIBGR.NodeType[] Statement_alt_0_Call_node_e_AllowedTypes = null;
		public static bool[] Statement_alt_0_Call_node_e_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Call_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Call_edge__edge1_AllowedTypes = null;
		public static bool[] Statement_alt_0_Call_edge__edge0_IsAllowedType = null;
		public static bool[] Statement_alt_0_Call_edge__edge1_IsAllowedType = null;
		public enum Statement_alt_0_Call_NodeNums { @b, @e, };
		public enum Statement_alt_0_Call_EdgeNums { @_edge0, @_edge1, };
		public enum Statement_alt_0_Call_VariableNums { };
		public enum Statement_alt_0_Call_SubNums { @_sub0, };
		public enum Statement_alt_0_Call_AltNums { };
		public enum Statement_alt_0_Call_IterNums { };



		public GRGEN_LGSP.PatternGraph Statement_alt_0_Call;

		public static GRGEN_LIBGR.NodeType[] Statement_alt_0_Return_node_e_AllowedTypes = null;
		public static bool[] Statement_alt_0_Return_node_e_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Return_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Return_edge__edge1_AllowedTypes = null;
		public static bool[] Statement_alt_0_Return_edge__edge0_IsAllowedType = null;
		public static bool[] Statement_alt_0_Return_edge__edge1_IsAllowedType = null;
		public enum Statement_alt_0_Return_NodeNums { @b, @e, };
		public enum Statement_alt_0_Return_EdgeNums { @_edge0, @_edge1, };
		public enum Statement_alt_0_Return_VariableNums { };
		public enum Statement_alt_0_Return_SubNums { };
		public enum Statement_alt_0_Return_AltNums { };
		public enum Statement_alt_0_Return_IterNums { };



		public GRGEN_LGSP.PatternGraph Statement_alt_0_Return;


		private Pattern_Statement()
		{
			name = "Statement";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "Statement_node_b", };

		}
		private void initialize()
		{
			bool[,] Statement_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Statement_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Statement_minMatches = new int[0] ;
			int[] Statement_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Statement_node_b = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "Statement_node_b", "b", Statement_node_b_AllowedTypes, Statement_node_b_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] Statement_alt_0_Assignment_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Statement_alt_0_Assignment_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] Statement_alt_0_Assignment_minMatches = new int[0] ;
			int[] Statement_alt_0_Assignment_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Statement_alt_0_Assignment_node_e = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "Statement_alt_0_Assignment_node_e", "e", Statement_alt_0_Assignment_node_e_AllowedTypes, Statement_alt_0_Assignment_node_e_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Assignment_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Statement_alt_0_Assignment_edge__edge0", "_edge0", Statement_alt_0_Assignment_edge__edge0_AllowedTypes, Statement_alt_0_Assignment_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Assignment_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@writesTo, "GRGEN_MODEL.IwritesTo", "Statement_alt_0_Assignment_edge__edge1", "_edge1", Statement_alt_0_Assignment_edge__edge1_AllowedTypes, Statement_alt_0_Assignment_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding Statement_alt_0_Assignment__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ExpressionPattern.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Statement_alt_0_Assignment_node_e"),
				}, 
				new string[] { "Statement_alt_0_Assignment_node_e" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Statement_alt_0_Assignment = new GRGEN_LGSP.PatternGraph(
				"Assignment",
				"Statement_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Statement_node_b, Statement_alt_0_Assignment_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { Statement_alt_0_Assignment_edge__edge0, Statement_alt_0_Assignment_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Statement_alt_0_Assignment__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Statement_alt_0_Assignment_minMatches,
				Statement_alt_0_Assignment_maxMatches,
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
				Statement_alt_0_Assignment_isNodeHomomorphicGlobal,
				Statement_alt_0_Assignment_isEdgeHomomorphicGlobal
			);
			Statement_alt_0_Assignment.edgeToSourceNode.Add(Statement_alt_0_Assignment_edge__edge0, Statement_node_b);
			Statement_alt_0_Assignment.edgeToTargetNode.Add(Statement_alt_0_Assignment_edge__edge0, Statement_alt_0_Assignment_node_e);
			Statement_alt_0_Assignment.edgeToSourceNode.Add(Statement_alt_0_Assignment_edge__edge1, Statement_alt_0_Assignment_node_e);

			bool[,] Statement_alt_0_Call_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Statement_alt_0_Call_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] Statement_alt_0_Call_minMatches = new int[0] ;
			int[] Statement_alt_0_Call_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Statement_alt_0_Call_node_e = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "Statement_alt_0_Call_node_e", "e", Statement_alt_0_Call_node_e_AllowedTypes, Statement_alt_0_Call_node_e_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Call_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Statement_alt_0_Call_edge__edge0", "_edge0", Statement_alt_0_Call_edge__edge0_AllowedTypes, Statement_alt_0_Call_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Call_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@calls, "GRGEN_MODEL.Icalls", "Statement_alt_0_Call_edge__edge1", "_edge1", Statement_alt_0_Call_edge__edge1_AllowedTypes, Statement_alt_0_Call_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding Statement_alt_0_Call__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Expressions.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Statement_alt_0_Call_node_e"),
				}, 
				new string[] { "Statement_alt_0_Call_node_e" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Statement_alt_0_Call = new GRGEN_LGSP.PatternGraph(
				"Call",
				"Statement_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Statement_node_b, Statement_alt_0_Call_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { Statement_alt_0_Call_edge__edge0, Statement_alt_0_Call_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Statement_alt_0_Call__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Statement_alt_0_Call_minMatches,
				Statement_alt_0_Call_maxMatches,
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
				Statement_alt_0_Call_isNodeHomomorphicGlobal,
				Statement_alt_0_Call_isEdgeHomomorphicGlobal
			);
			Statement_alt_0_Call.edgeToSourceNode.Add(Statement_alt_0_Call_edge__edge0, Statement_node_b);
			Statement_alt_0_Call.edgeToTargetNode.Add(Statement_alt_0_Call_edge__edge0, Statement_alt_0_Call_node_e);
			Statement_alt_0_Call.edgeToSourceNode.Add(Statement_alt_0_Call_edge__edge1, Statement_alt_0_Call_node_e);

			bool[,] Statement_alt_0_Return_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Statement_alt_0_Return_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] Statement_alt_0_Return_minMatches = new int[0] ;
			int[] Statement_alt_0_Return_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Statement_alt_0_Return_node_e = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "Statement_alt_0_Return_node_e", "e", Statement_alt_0_Return_node_e_AllowedTypes, Statement_alt_0_Return_node_e_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Return_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Statement_alt_0_Return_edge__edge0", "_edge0", Statement_alt_0_Return_edge__edge0_AllowedTypes, Statement_alt_0_Return_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Return_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@uses, "GRGEN_MODEL.Iuses", "Statement_alt_0_Return_edge__edge1", "_edge1", Statement_alt_0_Return_edge__edge1_AllowedTypes, Statement_alt_0_Return_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			Statement_alt_0_Return = new GRGEN_LGSP.PatternGraph(
				"Return",
				"Statement_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Statement_node_b, Statement_alt_0_Return_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { Statement_alt_0_Return_edge__edge0, Statement_alt_0_Return_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Statement_alt_0_Return_minMatches,
				Statement_alt_0_Return_maxMatches,
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
				Statement_alt_0_Return_isNodeHomomorphicGlobal,
				Statement_alt_0_Return_isEdgeHomomorphicGlobal
			);
			Statement_alt_0_Return.edgeToSourceNode.Add(Statement_alt_0_Return_edge__edge0, Statement_node_b);
			Statement_alt_0_Return.edgeToTargetNode.Add(Statement_alt_0_Return_edge__edge0, Statement_alt_0_Return_node_e);
			Statement_alt_0_Return.edgeToSourceNode.Add(Statement_alt_0_Return_edge__edge1, Statement_alt_0_Return_node_e);

			GRGEN_LGSP.Alternative Statement_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "Statement_", new GRGEN_LGSP.PatternGraph[] { Statement_alt_0_Assignment, Statement_alt_0_Call, Statement_alt_0_Return } );

			pat_Statement = new GRGEN_LGSP.PatternGraph(
				"Statement",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Statement_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { Statement_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Statement_minMatches,
				Statement_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Statement_isNodeHomomorphicGlobal,
				Statement_isEdgeHomomorphicGlobal
			);
			Statement_alt_0_Assignment.embeddingGraph = pat_Statement;
			Statement_alt_0_Call.embeddingGraph = pat_Statement;
			Statement_alt_0_Return.embeddingGraph = pat_Statement;

			Statement_node_b.pointOfDefinition = null;
			Statement_alt_0_Assignment_node_e.pointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Assignment_edge__edge0.pointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Assignment_edge__edge1.pointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Assignment__sub0.PointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Call_node_e.pointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Call_edge__edge0.pointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Call_edge__edge1.pointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Call__sub0.PointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Return_node_e.pointOfDefinition = Statement_alt_0_Return;
			Statement_alt_0_Return_edge__edge0.pointOfDefinition = Statement_alt_0_Return;
			Statement_alt_0_Return_edge__edge1.pointOfDefinition = Statement_alt_0_Return;

			patternGraph = pat_Statement;
		}


		public void Statement_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_Statement_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Statement_addedEdgeNames );
		}
		private static string[] create_Statement_addedNodeNames = new string[] {  };
		private static string[] create_Statement_addedEdgeNames = new string[] {  };

		public void Statement_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Statement curMatch)
		{
			IMatch_Statement_alt_0 alternative_alt_0 = curMatch._alt_0;
			Statement_alt_0_Delete(graph, alternative_alt_0);
		}

		public void Statement_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_Statement_alt_0 curMatch)
		{
			if(curMatch.Pattern == Statement_alt_0_Assignment) {
				Statement_alt_0_Assignment_Delete(graph, (Match_Statement_alt_0_Assignment)curMatch);
				return;
			}
			else if(curMatch.Pattern == Statement_alt_0_Call) {
				Statement_alt_0_Call_Delete(graph, (Match_Statement_alt_0_Call)curMatch);
				return;
			}
			else if(curMatch.Pattern == Statement_alt_0_Return) {
				Statement_alt_0_Return_Delete(graph, (Match_Statement_alt_0_Return)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void Statement_alt_0_Assignment_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Statement_alt_0_Assignment curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPNode node_e = curMatch._node_e;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			Pattern_ExpressionPattern.Match_ExpressionPattern subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			graph.RemoveEdges(node_e);
			graph.Remove(node_e);
			Pattern_ExpressionPattern.Instance.ExpressionPattern_Delete(graph, subpattern__sub0);
		}

		public void Statement_alt_0_Call_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Statement_alt_0_Call curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPNode node_e = curMatch._node_e;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			Pattern_Expressions.Match_Expressions subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			graph.RemoveEdges(node_e);
			graph.Remove(node_e);
			Pattern_Expressions.Instance.Expressions_Delete(graph, subpattern__sub0);
		}

		public void Statement_alt_0_Return_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Statement_alt_0_Return curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPNode node_e = curMatch._node_e;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			graph.RemoveEdges(node_e);
			graph.Remove(node_e);
		}

		static Pattern_Statement() {
		}

		public interface IMatch_Statement : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_Statement_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Statement_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Statement_alt_0_Assignment : IMatch_Statement_alt_0
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			GRGEN_MODEL.IExpression node_e { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			GRGEN_MODEL.IwritesTo edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_ExpressionPattern.Match_ExpressionPattern @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Statement_alt_0_Call : IMatch_Statement_alt_0
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			GRGEN_MODEL.IExpression node_e { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			GRGEN_MODEL.Icalls edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_Expressions.Match_Expressions @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Statement_alt_0_Return : IMatch_Statement_alt_0
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_b { get; }
			GRGEN_MODEL.IExpression node_e { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			GRGEN_MODEL.Iuses edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Statement : GRGEN_LGSP.ListElement<Match_Statement>, IMatch_Statement
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public enum Statement_NodeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Statement_NodeNums.@b: return _node_b;
				default: return null;
				}
			}
			
			public enum Statement_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_Statement_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_Statement_alt_0 _alt_0;
			public enum Statement_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)Statement_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum Statement_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Statement.instance.pat_Statement; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Statement_alt_0_Assignment : GRGEN_LGSP.ListElement<Match_Statement_alt_0_Assignment>, IMatch_Statement_alt_0_Assignment
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_MODEL.IExpression node_e { get { return (GRGEN_MODEL.IExpression)_node_e; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_e;
			public enum Statement_alt_0_Assignment_NodeNums { @b, @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Assignment_NodeNums.@b: return _node_b;
				case (int)Statement_alt_0_Assignment_NodeNums.@e: return _node_e;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_MODEL.IwritesTo edge__edge1 { get { return (GRGEN_MODEL.IwritesTo)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum Statement_alt_0_Assignment_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Assignment_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Statement_alt_0_Assignment_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Assignment_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ExpressionPattern.Match_ExpressionPattern @_sub0 { get { return @__sub0; } }
			public @Pattern_ExpressionPattern.Match_ExpressionPattern @__sub0;
			public enum Statement_alt_0_Assignment_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Assignment_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Assignment_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Assignment_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Assignment_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Statement.instance.Statement_alt_0_Assignment; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Statement_alt_0_Call : GRGEN_LGSP.ListElement<Match_Statement_alt_0_Call>, IMatch_Statement_alt_0_Call
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_MODEL.IExpression node_e { get { return (GRGEN_MODEL.IExpression)_node_e; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_e;
			public enum Statement_alt_0_Call_NodeNums { @b, @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Call_NodeNums.@b: return _node_b;
				case (int)Statement_alt_0_Call_NodeNums.@e: return _node_e;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_MODEL.Icalls edge__edge1 { get { return (GRGEN_MODEL.Icalls)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum Statement_alt_0_Call_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Call_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Statement_alt_0_Call_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Call_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Expressions.Match_Expressions @_sub0 { get { return @__sub0; } }
			public @Pattern_Expressions.Match_Expressions @__sub0;
			public enum Statement_alt_0_Call_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Call_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Call_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Call_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Call_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Statement.instance.Statement_alt_0_Call; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Statement_alt_0_Return : GRGEN_LGSP.ListElement<Match_Statement_alt_0_Return>, IMatch_Statement_alt_0_Return
		{
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_MODEL.IExpression node_e { get { return (GRGEN_MODEL.IExpression)_node_e; } }
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_e;
			public enum Statement_alt_0_Return_NodeNums { @b, @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Return_NodeNums.@b: return _node_b;
				case (int)Statement_alt_0_Return_NodeNums.@e: return _node_e;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_MODEL.Iuses edge__edge1 { get { return (GRGEN_MODEL.Iuses)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum Statement_alt_0_Return_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Statement_alt_0_Return_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Statement_alt_0_Return_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Return_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Return_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Return_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Return_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Statement_alt_0_Return_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Statement.instance.Statement_alt_0_Return; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Expressions : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Expressions instance = null;
		public static Pattern_Expressions Instance { get { if (instance==null) { instance = new Pattern_Expressions(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Expressions_node_e_AllowedTypes = null;
		public static bool[] Expressions_node_e_IsAllowedType = null;
		public enum Expressions_NodeNums { @e, };
		public enum Expressions_EdgeNums { };
		public enum Expressions_VariableNums { };
		public enum Expressions_SubNums { };
		public enum Expressions_AltNums { };
		public enum Expressions_IterNums { @iter_0, };



		public GRGEN_LGSP.PatternGraph pat_Expressions;

		public enum Expressions_iter_0_NodeNums { @e, };
		public enum Expressions_iter_0_EdgeNums { };
		public enum Expressions_iter_0_VariableNums { };
		public enum Expressions_iter_0_SubNums { @_sub0, };
		public enum Expressions_iter_0_AltNums { };
		public enum Expressions_iter_0_IterNums { };



		public GRGEN_LGSP.PatternGraph Expressions_iter_0;


		private Pattern_Expressions()
		{
			name = "Expressions";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Expression.typeVar, };
			inputNames = new string[] { "Expressions_node_e", };

		}
		private void initialize()
		{
			bool[,] Expressions_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Expressions_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Expressions_minMatches = new int[1] {
				0, 
			};
			int[] Expressions_maxMatches = new int[1] {
				0, 
			};
			GRGEN_LGSP.PatternNode Expressions_node_e = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "Expressions_node_e", "e", Expressions_node_e_AllowedTypes, Expressions_node_e_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			bool[,] Expressions_iter_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Expressions_iter_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Expressions_iter_0_minMatches = new int[0] ;
			int[] Expressions_iter_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternGraphEmbedding Expressions_iter_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_ExpressionPattern.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Expressions_node_e"),
				}, 
				new string[] { "Expressions_node_e" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Expressions_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"Expressions_",
				false,
				new GRGEN_LGSP.PatternNode[] { Expressions_node_e }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Expressions_iter_0__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Expressions_iter_0_minMatches,
				Expressions_iter_0_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Expressions_iter_0_isNodeHomomorphicGlobal,
				Expressions_iter_0_isEdgeHomomorphicGlobal
			);

			pat_Expressions = new GRGEN_LGSP.PatternGraph(
				"Expressions",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Expressions_node_e }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Expressions_iter_0,  }, 
				Expressions_minMatches,
				Expressions_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Expressions_isNodeHomomorphicGlobal,
				Expressions_isEdgeHomomorphicGlobal
			);
			Expressions_iter_0.embeddingGraph = pat_Expressions;

			Expressions_node_e.pointOfDefinition = null;
			Expressions_iter_0__sub0.PointOfDefinition = Expressions_iter_0;

			patternGraph = pat_Expressions;
		}


		public void Expressions_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_e)
		{
			graph.SettingAddedNodeNames( create_Expressions_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Expressions_addedEdgeNames );
		}
		private static string[] create_Expressions_addedNodeNames = new string[] {  };
		private static string[] create_Expressions_addedEdgeNames = new string[] {  };

		public void Expressions_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Expressions curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_Expressions_iter_0, IMatch_Expressions_iter_0> iterated_iter_0 = curMatch._iter_0;
			Expressions_iter_0_Delete(graph, iterated_iter_0);
		}

		public void Expressions_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_Expressions_iter_0, IMatch_Expressions_iter_0> curMatches)
		{
			for(Match_Expressions_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				Expressions_iter_0_Delete(graph, curMatch);
			}
		}

		public void Expressions_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Expressions_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_e = curMatch._node_e;
			Pattern_ExpressionPattern.Match_ExpressionPattern subpattern__sub0 = curMatch.@__sub0;
			graph.RemoveEdges(node_e);
			graph.Remove(node_e);
			Pattern_ExpressionPattern.Instance.ExpressionPattern_Delete(graph, subpattern__sub0);
		}

		static Pattern_Expressions() {
		}

		public interface IMatch_Expressions : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IExpression node_e { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_Expressions_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Expressions_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IExpression node_e { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_ExpressionPattern.Match_ExpressionPattern @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Expressions : GRGEN_LGSP.ListElement<Match_Expressions>, IMatch_Expressions
		{
			public GRGEN_MODEL.IExpression node_e { get { return (GRGEN_MODEL.IExpression)_node_e; } }
			public GRGEN_LGSP.LGSPNode _node_e;
			public enum Expressions_NodeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Expressions_NodeNums.@e: return _node_e;
				default: return null;
				}
			}
			
			public enum Expressions_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Expressions_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Expressions_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Expressions_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_Expressions_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_Expressions_iter_0, IMatch_Expressions_iter_0> _iter_0;
			public enum Expressions_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)Expressions_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum Expressions_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Expressions.instance.pat_Expressions; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Expressions_iter_0 : GRGEN_LGSP.ListElement<Match_Expressions_iter_0>, IMatch_Expressions_iter_0
		{
			public GRGEN_MODEL.IExpression node_e { get { return (GRGEN_MODEL.IExpression)_node_e; } }
			public GRGEN_LGSP.LGSPNode _node_e;
			public enum Expressions_iter_0_NodeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Expressions_iter_0_NodeNums.@e: return _node_e;
				default: return null;
				}
			}
			
			public enum Expressions_iter_0_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Expressions_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_ExpressionPattern.Match_ExpressionPattern @_sub0 { get { return @__sub0; } }
			public @Pattern_ExpressionPattern.Match_ExpressionPattern @__sub0;
			public enum Expressions_iter_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Expressions_iter_0_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum Expressions_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Expressions_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Expressions_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Expressions.instance.Expressions_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_ExpressionPattern : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ExpressionPattern instance = null;
		public static Pattern_ExpressionPattern Instance { get { if (instance==null) { instance = new Pattern_ExpressionPattern(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ExpressionPattern_node_e_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ExpressionPattern_node_sub_AllowedTypes = null;
		public static bool[] ExpressionPattern_node_e_IsAllowedType = null;
		public static bool[] ExpressionPattern_node_sub_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ExpressionPattern_edge__edge0_AllowedTypes = null;
		public static bool[] ExpressionPattern_edge__edge0_IsAllowedType = null;
		public enum ExpressionPattern_NodeNums { @e, @sub, };
		public enum ExpressionPattern_EdgeNums { @_edge0, };
		public enum ExpressionPattern_VariableNums { };
		public enum ExpressionPattern_SubNums { };
		public enum ExpressionPattern_AltNums { @alt_0, };
		public enum ExpressionPattern_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ExpressionPattern;

		public enum ExpressionPattern_alt_0_CaseNums { @Call, @Use, };
		public static GRGEN_LIBGR.EdgeType[] ExpressionPattern_alt_0_Call_edge__edge0_AllowedTypes = null;
		public static bool[] ExpressionPattern_alt_0_Call_edge__edge0_IsAllowedType = null;
		public enum ExpressionPattern_alt_0_Call_NodeNums { @sub, };
		public enum ExpressionPattern_alt_0_Call_EdgeNums { @_edge0, };
		public enum ExpressionPattern_alt_0_Call_VariableNums { };
		public enum ExpressionPattern_alt_0_Call_SubNums { @_sub0, };
		public enum ExpressionPattern_alt_0_Call_AltNums { };
		public enum ExpressionPattern_alt_0_Call_IterNums { };



		public GRGEN_LGSP.PatternGraph ExpressionPattern_alt_0_Call;

		public static GRGEN_LIBGR.EdgeType[] ExpressionPattern_alt_0_Use_edge__edge0_AllowedTypes = null;
		public static bool[] ExpressionPattern_alt_0_Use_edge__edge0_IsAllowedType = null;
		public enum ExpressionPattern_alt_0_Use_NodeNums { @sub, };
		public enum ExpressionPattern_alt_0_Use_EdgeNums { @_edge0, };
		public enum ExpressionPattern_alt_0_Use_VariableNums { };
		public enum ExpressionPattern_alt_0_Use_SubNums { };
		public enum ExpressionPattern_alt_0_Use_AltNums { };
		public enum ExpressionPattern_alt_0_Use_IterNums { };



		public GRGEN_LGSP.PatternGraph ExpressionPattern_alt_0_Use;


		private Pattern_ExpressionPattern()
		{
			name = "ExpressionPattern";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Expression.typeVar, };
			inputNames = new string[] { "ExpressionPattern_node_e", };

		}
		private void initialize()
		{
			bool[,] ExpressionPattern_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ExpressionPattern_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ExpressionPattern_minMatches = new int[0] ;
			int[] ExpressionPattern_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode ExpressionPattern_node_e = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "ExpressionPattern_node_e", "e", ExpressionPattern_node_e_AllowedTypes, ExpressionPattern_node_e_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			GRGEN_LGSP.PatternNode ExpressionPattern_node_sub = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "ExpressionPattern_node_sub", "sub", ExpressionPattern_node_sub_AllowedTypes, ExpressionPattern_node_sub_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge ExpressionPattern_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "ExpressionPattern_edge__edge0", "_edge0", ExpressionPattern_edge__edge0_AllowedTypes, ExpressionPattern_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			bool[,] ExpressionPattern_alt_0_Call_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ExpressionPattern_alt_0_Call_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ExpressionPattern_alt_0_Call_minMatches = new int[0] ;
			int[] ExpressionPattern_alt_0_Call_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge ExpressionPattern_alt_0_Call_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@calls, "GRGEN_MODEL.Icalls", "ExpressionPattern_alt_0_Call_edge__edge0", "_edge0", ExpressionPattern_alt_0_Call_edge__edge0_AllowedTypes, ExpressionPattern_alt_0_Call_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding ExpressionPattern_alt_0_Call__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Expressions.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("ExpressionPattern_node_sub"),
				}, 
				new string[] { "ExpressionPattern_node_sub" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			ExpressionPattern_alt_0_Call = new GRGEN_LGSP.PatternGraph(
				"Call",
				"ExpressionPattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ExpressionPattern_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { ExpressionPattern_alt_0_Call_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ExpressionPattern_alt_0_Call__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ExpressionPattern_alt_0_Call_minMatches,
				ExpressionPattern_alt_0_Call_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ExpressionPattern_alt_0_Call_isNodeHomomorphicGlobal,
				ExpressionPattern_alt_0_Call_isEdgeHomomorphicGlobal
			);
			ExpressionPattern_alt_0_Call.edgeToSourceNode.Add(ExpressionPattern_alt_0_Call_edge__edge0, ExpressionPattern_node_sub);

			bool[,] ExpressionPattern_alt_0_Use_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ExpressionPattern_alt_0_Use_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ExpressionPattern_alt_0_Use_minMatches = new int[0] ;
			int[] ExpressionPattern_alt_0_Use_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge ExpressionPattern_alt_0_Use_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@uses, "GRGEN_MODEL.Iuses", "ExpressionPattern_alt_0_Use_edge__edge0", "_edge0", ExpressionPattern_alt_0_Use_edge__edge0_AllowedTypes, ExpressionPattern_alt_0_Use_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			ExpressionPattern_alt_0_Use = new GRGEN_LGSP.PatternGraph(
				"Use",
				"ExpressionPattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ExpressionPattern_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { ExpressionPattern_alt_0_Use_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ExpressionPattern_alt_0_Use_minMatches,
				ExpressionPattern_alt_0_Use_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ExpressionPattern_alt_0_Use_isNodeHomomorphicGlobal,
				ExpressionPattern_alt_0_Use_isEdgeHomomorphicGlobal
			);
			ExpressionPattern_alt_0_Use.edgeToSourceNode.Add(ExpressionPattern_alt_0_Use_edge__edge0, ExpressionPattern_node_sub);

			GRGEN_LGSP.Alternative ExpressionPattern_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "ExpressionPattern_", new GRGEN_LGSP.PatternGraph[] { ExpressionPattern_alt_0_Call, ExpressionPattern_alt_0_Use } );

			pat_ExpressionPattern = new GRGEN_LGSP.PatternGraph(
				"ExpressionPattern",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ExpressionPattern_node_e, ExpressionPattern_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { ExpressionPattern_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { ExpressionPattern_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ExpressionPattern_minMatches,
				ExpressionPattern_maxMatches,
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
				ExpressionPattern_isNodeHomomorphicGlobal,
				ExpressionPattern_isEdgeHomomorphicGlobal
			);
			pat_ExpressionPattern.edgeToSourceNode.Add(ExpressionPattern_edge__edge0, ExpressionPattern_node_e);
			pat_ExpressionPattern.edgeToTargetNode.Add(ExpressionPattern_edge__edge0, ExpressionPattern_node_sub);
			ExpressionPattern_alt_0_Call.embeddingGraph = pat_ExpressionPattern;
			ExpressionPattern_alt_0_Use.embeddingGraph = pat_ExpressionPattern;

			ExpressionPattern_node_e.pointOfDefinition = null;
			ExpressionPattern_node_sub.pointOfDefinition = pat_ExpressionPattern;
			ExpressionPattern_edge__edge0.pointOfDefinition = pat_ExpressionPattern;
			ExpressionPattern_alt_0_Call_edge__edge0.pointOfDefinition = ExpressionPattern_alt_0_Call;
			ExpressionPattern_alt_0_Call__sub0.PointOfDefinition = ExpressionPattern_alt_0_Call;
			ExpressionPattern_alt_0_Use_edge__edge0.pointOfDefinition = ExpressionPattern_alt_0_Use;

			patternGraph = pat_ExpressionPattern;
		}


		public void ExpressionPattern_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_e)
		{
			graph.SettingAddedNodeNames( create_ExpressionPattern_addedNodeNames );
			GRGEN_MODEL.@Expression node_sub = GRGEN_MODEL.@Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ExpressionPattern_addedEdgeNames );
			GRGEN_MODEL.@contains edge__edge0 = GRGEN_MODEL.@contains.CreateEdge(graph, node_e, node_sub);
		}
		private static string[] create_ExpressionPattern_addedNodeNames = new string[] { "sub" };
		private static string[] create_ExpressionPattern_addedEdgeNames = new string[] { "_edge0" };

		public void ExpressionPattern_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ExpressionPattern curMatch)
		{
			GRGEN_LGSP.LGSPNode node_sub = curMatch._node_sub;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			IMatch_ExpressionPattern_alt_0 alternative_alt_0 = curMatch._alt_0;
			ExpressionPattern_alt_0_Delete(graph, alternative_alt_0);
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_sub);
			graph.Remove(node_sub);
		}

		public void ExpressionPattern_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_ExpressionPattern_alt_0 curMatch)
		{
			if(curMatch.Pattern == ExpressionPattern_alt_0_Call) {
				ExpressionPattern_alt_0_Call_Delete(graph, (Match_ExpressionPattern_alt_0_Call)curMatch);
				return;
			}
			else if(curMatch.Pattern == ExpressionPattern_alt_0_Use) {
				ExpressionPattern_alt_0_Use_Delete(graph, (Match_ExpressionPattern_alt_0_Use)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ExpressionPattern_alt_0_Call_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ExpressionPattern_alt_0_Call curMatch)
		{
			GRGEN_LGSP.LGSPNode node_sub = curMatch._node_sub;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_Expressions.Match_Expressions subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_sub);
			graph.Remove(node_sub);
			Pattern_Expressions.Instance.Expressions_Delete(graph, subpattern__sub0);
		}

		public void ExpressionPattern_alt_0_Use_Delete(GRGEN_LGSP.LGSPGraph graph, Match_ExpressionPattern_alt_0_Use curMatch)
		{
			GRGEN_LGSP.LGSPNode node_sub = curMatch._node_sub;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_sub);
			graph.Remove(node_sub);
		}

		static Pattern_ExpressionPattern() {
		}

		public interface IMatch_ExpressionPattern : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IExpression node_e { get; }
			GRGEN_MODEL.IExpression node_sub { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_ExpressionPattern_alt_0 alt_0 { get; }
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ExpressionPattern_alt_0 : GRGEN_LIBGR.IMatch
		{
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ExpressionPattern_alt_0_Call : IMatch_ExpressionPattern_alt_0
		{
			//Nodes
			GRGEN_MODEL.IExpression node_sub { get; }
			//Edges
			GRGEN_MODEL.Icalls edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_Expressions.Match_Expressions @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ExpressionPattern_alt_0_Use : IMatch_ExpressionPattern_alt_0
		{
			//Nodes
			GRGEN_MODEL.IExpression node_sub { get; }
			//Edges
			GRGEN_MODEL.Iuses edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ExpressionPattern : GRGEN_LGSP.ListElement<Match_ExpressionPattern>, IMatch_ExpressionPattern
		{
			public GRGEN_MODEL.IExpression node_e { get { return (GRGEN_MODEL.IExpression)_node_e; } }
			public GRGEN_MODEL.IExpression node_sub { get { return (GRGEN_MODEL.IExpression)_node_sub; } }
			public GRGEN_LGSP.LGSPNode _node_e;
			public GRGEN_LGSP.LGSPNode _node_sub;
			public enum ExpressionPattern_NodeNums { @e, @sub, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_NodeNums.@e: return _node_e;
				case (int)ExpressionPattern_NodeNums.@sub: return _node_sub;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ExpressionPattern_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ExpressionPattern_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_ExpressionPattern_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_ExpressionPattern_alt_0 _alt_0;
			public enum ExpressionPattern_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum ExpressionPattern_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ExpressionPattern.instance.pat_ExpressionPattern; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ExpressionPattern_alt_0_Call : GRGEN_LGSP.ListElement<Match_ExpressionPattern_alt_0_Call>, IMatch_ExpressionPattern_alt_0_Call
		{
			public GRGEN_MODEL.IExpression node_sub { get { return (GRGEN_MODEL.IExpression)_node_sub; } }
			public GRGEN_LGSP.LGSPNode _node_sub;
			public enum ExpressionPattern_alt_0_Call_NodeNums { @sub, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_alt_0_Call_NodeNums.@sub: return _node_sub;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icalls edge__edge0 { get { return (GRGEN_MODEL.Icalls)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ExpressionPattern_alt_0_Call_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_alt_0_Call_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Call_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Expressions.Match_Expressions @_sub0 { get { return @__sub0; } }
			public @Pattern_Expressions.Match_Expressions @__sub0;
			public enum ExpressionPattern_alt_0_Call_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_alt_0_Call_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Call_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Call_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Call_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ExpressionPattern.instance.ExpressionPattern_alt_0_Call; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ExpressionPattern_alt_0_Use : GRGEN_LGSP.ListElement<Match_ExpressionPattern_alt_0_Use>, IMatch_ExpressionPattern_alt_0_Use
		{
			public GRGEN_MODEL.IExpression node_sub { get { return (GRGEN_MODEL.IExpression)_node_sub; } }
			public GRGEN_LGSP.LGSPNode _node_sub;
			public enum ExpressionPattern_alt_0_Use_NodeNums { @sub, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_alt_0_Use_NodeNums.@sub: return _node_sub;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iuses edge__edge0 { get { return (GRGEN_MODEL.Iuses)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ExpressionPattern_alt_0_Use_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ExpressionPattern_alt_0_Use_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Use_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Use_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Use_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Use_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ExpressionPattern_alt_0_Use_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_ExpressionPattern.instance.ExpressionPattern_alt_0_Use; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Bodies : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Bodies instance = null;
		public static Pattern_Bodies Instance { get { if (instance==null) { instance = new Pattern_Bodies(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Bodies_node_m5_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Bodies_node_c1_AllowedTypes = null;
		public static bool[] Bodies_node_m5_IsAllowedType = null;
		public static bool[] Bodies_node_c1_IsAllowedType = null;
		public enum Bodies_NodeNums { @m5, @c1, };
		public enum Bodies_EdgeNums { };
		public enum Bodies_VariableNums { };
		public enum Bodies_SubNums { };
		public enum Bodies_AltNums { };
		public enum Bodies_IterNums { @iter_0, };




		public GRGEN_LGSP.PatternGraph pat_Bodies;

		public enum Bodies_iter_0_NodeNums { @m5, @c1, };
		public enum Bodies_iter_0_EdgeNums { };
		public enum Bodies_iter_0_VariableNums { };
		public enum Bodies_iter_0_SubNums { @b, };
		public enum Bodies_iter_0_AltNums { };
		public enum Bodies_iter_0_IterNums { };




		public GRGEN_LGSP.PatternGraph Bodies_iter_0;


		private Pattern_Bodies()
		{
			name = "Bodies";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_MethodSignature.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, };
			inputNames = new string[] { "Bodies_node_m5", "Bodies_node_c1", };

		}
		private void initialize()
		{
			bool[,] Bodies_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Bodies_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Bodies_minMatches = new int[1] {
				0, 
			};
			int[] Bodies_maxMatches = new int[1] {
				0, 
			};
			GRGEN_LGSP.PatternNode Bodies_node_m5 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodSignature, "GRGEN_MODEL.IMethodSignature", "Bodies_node_m5", "m5", Bodies_node_m5_AllowedTypes, Bodies_node_m5_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			GRGEN_LGSP.PatternNode Bodies_node_c1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "Bodies_node_c1", "c1", Bodies_node_c1_AllowedTypes, Bodies_node_c1_IsAllowedType, 5.5F, 1, false, null, null, null, null);
			bool[,] Bodies_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Bodies_iter_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] Bodies_iter_0_minMatches = new int[0] ;
			int[] Bodies_iter_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternGraphEmbedding Bodies_iter_0_b = new GRGEN_LGSP.PatternGraphEmbedding("b", Pattern_Body.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Bodies_node_m5"),
					new GRGEN_EXPR.GraphEntityExpression("Bodies_node_c1"),
				}, 
				new string[] { "Bodies_node_m5", "Bodies_node_c1" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			Bodies_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"Bodies_",
				false,
				new GRGEN_LGSP.PatternNode[] { Bodies_node_m5, Bodies_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Bodies_iter_0_b }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Bodies_iter_0_minMatches,
				Bodies_iter_0_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, true, },
					{ true, true, },
				},
				new bool[0, 0] ,
				Bodies_iter_0_isNodeHomomorphicGlobal,
				Bodies_iter_0_isEdgeHomomorphicGlobal
			);

			pat_Bodies = new GRGEN_LGSP.PatternGraph(
				"Bodies",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Bodies_node_m5, Bodies_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { Bodies_iter_0,  }, 
				Bodies_minMatches,
				Bodies_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				Bodies_isNodeHomomorphicGlobal,
				Bodies_isEdgeHomomorphicGlobal
			);
			Bodies_iter_0.embeddingGraph = pat_Bodies;

			Bodies_node_m5.pointOfDefinition = null;
			Bodies_node_c1.pointOfDefinition = null;
			Bodies_iter_0_b.PointOfDefinition = Bodies_iter_0;

			patternGraph = pat_Bodies;
		}


		public void Bodies_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Bodies curMatch = (Match_Bodies)_curMatch;
			GRGEN_LGSP.LGSPMatchesList<Match_Bodies_iter_0, IMatch_Bodies_iter_0> iterated_iter_0 = curMatch._iter_0;
			graph.SettingAddedNodeNames( Bodies_addedNodeNames );
			Bodies_iter_0_Modify(graph, iterated_iter_0);
			graph.SettingAddedEdgeNames( Bodies_addedEdgeNames );
		}
		private static string[] Bodies_addedNodeNames = new string[] {  };
		private static string[] Bodies_addedEdgeNames = new string[] {  };

		public void Bodies_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_m5, GRGEN_LGSP.LGSPNode node_c1)
		{
			graph.SettingAddedNodeNames( create_Bodies_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Bodies_addedEdgeNames );
		}
		private static string[] create_Bodies_addedNodeNames = new string[] {  };
		private static string[] create_Bodies_addedEdgeNames = new string[] {  };

		public void Bodies_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Bodies curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_Bodies_iter_0, IMatch_Bodies_iter_0> iterated_iter_0 = curMatch._iter_0;
			Bodies_iter_0_Delete(graph, iterated_iter_0);
		}

		public void Bodies_iter_0_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_Bodies_iter_0, IMatch_Bodies_iter_0> curMatches)
		{
			for(Match_Bodies_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				Bodies_iter_0_Modify(graph, curMatch);
			}
		}

		public void Bodies_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_Bodies_iter_0, IMatch_Bodies_iter_0> curMatches)
		{
			for(Match_Bodies_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				Bodies_iter_0_Delete(graph, curMatch);
			}
		}

		public void Bodies_iter_0_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Bodies_iter_0 curMatch = (Match_Bodies_iter_0)_curMatch;
			Pattern_Body.Match_Body subpattern_b = curMatch.@_b;
			graph.SettingAddedNodeNames( Bodies_iter_0_addedNodeNames );
			Pattern_Body.Instance.Body_Modify(graph, subpattern_b);
			graph.SettingAddedEdgeNames( Bodies_iter_0_addedEdgeNames );
		}
		private static string[] Bodies_iter_0_addedNodeNames = new string[] {  };
		private static string[] Bodies_iter_0_addedEdgeNames = new string[] {  };

		public void Bodies_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Bodies_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_m5 = curMatch._node_m5;
			GRGEN_LGSP.LGSPNode node_c1 = curMatch._node_c1;
			Pattern_Body.Match_Body subpattern_b = curMatch.@_b;
			graph.RemoveEdges(node_m5);
			graph.Remove(node_m5);
			graph.RemoveEdges(node_c1);
			graph.Remove(node_c1);
			Pattern_Body.Instance.Body_Delete(graph, subpattern_b);
		}

		static Pattern_Bodies() {
		}

		public interface IMatch_Bodies : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodSignature node_m5 { get; }
			GRGEN_MODEL.IClass node_c1 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_Bodies_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_Bodies_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodSignature node_m5 { get; }
			GRGEN_MODEL.IClass node_c1 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_Body.Match_Body @b { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Bodies : GRGEN_LGSP.ListElement<Match_Bodies>, IMatch_Bodies
		{
			public GRGEN_MODEL.IMethodSignature node_m5 { get { return (GRGEN_MODEL.IMethodSignature)_node_m5; } }
			public GRGEN_MODEL.IClass node_c1 { get { return (GRGEN_MODEL.IClass)_node_c1; } }
			public GRGEN_LGSP.LGSPNode _node_m5;
			public GRGEN_LGSP.LGSPNode _node_c1;
			public enum Bodies_NodeNums { @m5, @c1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Bodies_NodeNums.@m5: return _node_m5;
				case (int)Bodies_NodeNums.@c1: return _node_c1;
				default: return null;
				}
			}
			
			public enum Bodies_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Bodies_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Bodies_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Bodies_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_Bodies_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_Bodies_iter_0, IMatch_Bodies_iter_0> _iter_0;
			public enum Bodies_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)Bodies_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum Bodies_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Bodies.instance.pat_Bodies; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_Bodies_iter_0 : GRGEN_LGSP.ListElement<Match_Bodies_iter_0>, IMatch_Bodies_iter_0
		{
			public GRGEN_MODEL.IMethodSignature node_m5 { get { return (GRGEN_MODEL.IMethodSignature)_node_m5; } }
			public GRGEN_MODEL.IClass node_c1 { get { return (GRGEN_MODEL.IClass)_node_c1; } }
			public GRGEN_LGSP.LGSPNode _node_m5;
			public GRGEN_LGSP.LGSPNode _node_c1;
			public enum Bodies_iter_0_NodeNums { @m5, @c1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Bodies_iter_0_NodeNums.@m5: return _node_m5;
				case (int)Bodies_iter_0_NodeNums.@c1: return _node_c1;
				default: return null;
				}
			}
			
			public enum Bodies_iter_0_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Bodies_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Body.Match_Body @b { get { return @_b; } }
			public @Pattern_Body.Match_Body @_b;
			public enum Bodies_iter_0_SubNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Bodies_iter_0_SubNums.@b: return _b;
				default: return null;
				}
			}
			
			public enum Bodies_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Bodies_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Bodies_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Bodies.instance.Bodies_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_Body : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Body instance = null;
		public static Pattern_Body Instance { get { if (instance==null) { instance = new Pattern_Body(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Body_node_c1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Body_node_c2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Body_node_b_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Body_node_m5_AllowedTypes = null;
		public static bool[] Body_node_c1_IsAllowedType = null;
		public static bool[] Body_node_c2_IsAllowedType = null;
		public static bool[] Body_node_b_IsAllowedType = null;
		public static bool[] Body_node_m5_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Body_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Body_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Body_edge__edge2_AllowedTypes = null;
		public static bool[] Body_edge__edge0_IsAllowedType = null;
		public static bool[] Body_edge__edge1_IsAllowedType = null;
		public static bool[] Body_edge__edge2_IsAllowedType = null;
		public enum Body_NodeNums { @c1, @c2, @b, @m5, };
		public enum Body_EdgeNums { @_edge0, @_edge1, @_edge2, };
		public enum Body_VariableNums { };
		public enum Body_SubNums { @p, @s, };
		public enum Body_AltNums { };
		public enum Body_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_Body;


		private Pattern_Body()
		{
			name = "Body";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_MethodSignature.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, };
			inputNames = new string[] { "Body_node_m5", "Body_node_c1", };

		}
		private void initialize()
		{
			bool[,] Body_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] Body_isEdgeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			int[] Body_minMatches = new int[0] ;
			int[] Body_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode Body_node_c1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "Body_node_c1", "c1", Body_node_c1_AllowedTypes, Body_node_c1_IsAllowedType, 5.5F, 1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode Body_node_c2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "Body_node_c2", "c2", Body_node_c2_AllowedTypes, Body_node_c2_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode Body_node_b = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "Body_node_b", "b", Body_node_b_AllowedTypes, Body_node_b_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode Body_node_m5 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodSignature, "GRGEN_MODEL.IMethodSignature", "Body_node_m5", "m5", Body_node_m5_AllowedTypes, Body_node_m5_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Body_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Body_edge__edge0", "_edge0", Body_edge__edge0_AllowedTypes, Body_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Body_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "Body_edge__edge1", "_edge1", Body_edge__edge1_AllowedTypes, Body_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge Body_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@bindsTo, "GRGEN_MODEL.IbindsTo", "Body_edge__edge2", "_edge2", Body_edge__edge2_AllowedTypes, Body_edge__edge2_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding Body_p = new GRGEN_LGSP.PatternGraphEmbedding("p", Pattern_Parameters.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Body_node_b"),
				}, 
				new string[] { "Body_node_b" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternGraphEmbedding Body_s = new GRGEN_LGSP.PatternGraphEmbedding("s", Pattern_Statements.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("Body_node_b"),
				}, 
				new string[] { "Body_node_b" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_Body = new GRGEN_LGSP.PatternGraph(
				"Body",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Body_node_c1, Body_node_c2, Body_node_b, Body_node_m5 }, 
				new GRGEN_LGSP.PatternEdge[] { Body_edge__edge0, Body_edge__edge1, Body_edge__edge2 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Body_p, Body_s }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				Body_minMatches,
				Body_maxMatches,
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
				Body_isNodeHomomorphicGlobal,
				Body_isEdgeHomomorphicGlobal
			);
			pat_Body.edgeToSourceNode.Add(Body_edge__edge0, Body_node_c1);
			pat_Body.edgeToTargetNode.Add(Body_edge__edge0, Body_node_c2);
			pat_Body.edgeToSourceNode.Add(Body_edge__edge1, Body_node_c2);
			pat_Body.edgeToTargetNode.Add(Body_edge__edge1, Body_node_b);
			pat_Body.edgeToSourceNode.Add(Body_edge__edge2, Body_node_b);
			pat_Body.edgeToTargetNode.Add(Body_edge__edge2, Body_node_m5);

			Body_node_c1.pointOfDefinition = null;
			Body_node_c2.pointOfDefinition = pat_Body;
			Body_node_b.pointOfDefinition = pat_Body;
			Body_node_m5.pointOfDefinition = null;
			Body_edge__edge0.pointOfDefinition = pat_Body;
			Body_edge__edge1.pointOfDefinition = pat_Body;
			Body_edge__edge2.pointOfDefinition = pat_Body;
			Body_p.PointOfDefinition = pat_Body;
			Body_s.PointOfDefinition = pat_Body;

			patternGraph = pat_Body;
		}


		public void Body_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_Body curMatch = (Match_Body)_curMatch;
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			GRGEN_LGSP.LGSPEdge edge__edge2 = curMatch._edge__edge2;
			Pattern_Parameters.Match_Parameters subpattern_p = curMatch.@_p;
			Pattern_Statements.Match_Statements subpattern_s = curMatch.@_s;
			graph.SettingAddedNodeNames( Body_addedNodeNames );
			graph.SettingAddedEdgeNames( Body_addedEdgeNames );
			graph.Remove(edge__edge1);
			graph.Remove(edge__edge2);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_Parameters.Instance.Parameters_Delete(graph, subpattern_p);
			Pattern_Statements.Instance.Statements_Delete(graph, subpattern_s);
		}
		private static string[] Body_addedNodeNames = new string[] {  };
		private static string[] Body_addedEdgeNames = new string[] {  };

		public void Body_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_m5, GRGEN_LGSP.LGSPNode node_c1)
		{
			graph.SettingAddedNodeNames( create_Body_addedNodeNames );
			GRGEN_MODEL.@Class node_c2 = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_b = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Body_addedEdgeNames );
			GRGEN_MODEL.@contains edge__edge0 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c1, node_c2);
			GRGEN_MODEL.@contains edge__edge1 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c2, node_b);
			GRGEN_MODEL.@bindsTo edge__edge2 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_b, node_m5);
			Pattern_Parameters.Instance.Parameters_Create(graph, (GRGEN_MODEL.@MethodBody)(node_b));
			Pattern_Statements.Instance.Statements_Create(graph, (GRGEN_MODEL.@MethodBody)(node_b));
		}
		private static string[] create_Body_addedNodeNames = new string[] { "c2", "b" };
		private static string[] create_Body_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2" };

		public void Body_Delete(GRGEN_LGSP.LGSPGraph graph, Match_Body curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c2 = curMatch._node_c2;
			GRGEN_LGSP.LGSPNode node_b = curMatch._node_b;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch._edge__edge1;
			GRGEN_LGSP.LGSPEdge edge__edge2 = curMatch._edge__edge2;
			Pattern_Parameters.Match_Parameters subpattern_p = curMatch.@_p;
			Pattern_Statements.Match_Statements subpattern_s = curMatch.@_s;
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.Remove(edge__edge2);
			graph.RemoveEdges(node_c2);
			graph.Remove(node_c2);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_Parameters.Instance.Parameters_Delete(graph, subpattern_p);
			Pattern_Statements.Instance.Statements_Delete(graph, subpattern_s);
		}

		static Pattern_Body() {
		}

		public interface IMatch_Body : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c1 { get; }
			GRGEN_MODEL.IClass node_c2 { get; }
			GRGEN_MODEL.IMethodBody node_b { get; }
			GRGEN_MODEL.IMethodSignature node_m5 { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			GRGEN_MODEL.Icontains edge__edge1 { get; }
			GRGEN_MODEL.IbindsTo edge__edge2 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_Parameters.Match_Parameters @p { get; }
			@Pattern_Statements.Match_Statements @s { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Body : GRGEN_LGSP.ListElement<Match_Body>, IMatch_Body
		{
			public GRGEN_MODEL.IClass node_c1 { get { return (GRGEN_MODEL.IClass)_node_c1; } }
			public GRGEN_MODEL.IClass node_c2 { get { return (GRGEN_MODEL.IClass)_node_c2; } }
			public GRGEN_MODEL.IMethodBody node_b { get { return (GRGEN_MODEL.IMethodBody)_node_b; } }
			public GRGEN_MODEL.IMethodSignature node_m5 { get { return (GRGEN_MODEL.IMethodSignature)_node_m5; } }
			public GRGEN_LGSP.LGSPNode _node_c1;
			public GRGEN_LGSP.LGSPNode _node_c2;
			public GRGEN_LGSP.LGSPNode _node_b;
			public GRGEN_LGSP.LGSPNode _node_m5;
			public enum Body_NodeNums { @c1, @c2, @b, @m5, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Body_NodeNums.@c1: return _node_c1;
				case (int)Body_NodeNums.@c2: return _node_c2;
				case (int)Body_NodeNums.@b: return _node_b;
				case (int)Body_NodeNums.@m5: return _node_m5;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_MODEL.Icontains edge__edge1 { get { return (GRGEN_MODEL.Icontains)_edge__edge1; } }
			public GRGEN_MODEL.IbindsTo edge__edge2 { get { return (GRGEN_MODEL.IbindsTo)_edge__edge2; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public enum Body_EdgeNums { @_edge0, @_edge1, @_edge2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 3;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)Body_EdgeNums.@_edge0: return _edge__edge0;
				case (int)Body_EdgeNums.@_edge1: return _edge__edge1;
				case (int)Body_EdgeNums.@_edge2: return _edge__edge2;
				default: return null;
				}
			}
			
			public enum Body_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Parameters.Match_Parameters @p { get { return @_p; } }
			public @Pattern_Statements.Match_Statements @s { get { return @_s; } }
			public @Pattern_Parameters.Match_Parameters @_p;
			public @Pattern_Statements.Match_Statements @_s;
			public enum Body_SubNums { @p, @s, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 2;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)Body_SubNums.@p: return _p;
				case (int)Body_SubNums.@s: return _s;
				default: return null;
				}
			}
			
			public enum Body_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Body_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Body_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_Body.instance.pat_Body; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createProgramGraphExample : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createProgramGraphExample instance = null;
		public static Rule_createProgramGraphExample Instance { get { if (instance==null) { instance = new Rule_createProgramGraphExample(); instance.initialize(); } return instance; } }

		public enum createProgramGraphExample_NodeNums { };
		public enum createProgramGraphExample_EdgeNums { };
		public enum createProgramGraphExample_VariableNums { };
		public enum createProgramGraphExample_SubNums { };
		public enum createProgramGraphExample_AltNums { };
		public enum createProgramGraphExample_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_createProgramGraphExample;


		private Rule_createProgramGraphExample()
		{
			name = "createProgramGraphExample";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] createProgramGraphExample_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createProgramGraphExample_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] createProgramGraphExample_minMatches = new int[0] ;
			int[] createProgramGraphExample_maxMatches = new int[0] ;
			pat_createProgramGraphExample = new GRGEN_LGSP.PatternGraph(
				"createProgramGraphExample",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				createProgramGraphExample_minMatches,
				createProgramGraphExample_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createProgramGraphExample_isNodeHomomorphicGlobal,
				createProgramGraphExample_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createProgramGraphExample;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createProgramGraphExample curMatch = (Match_createProgramGraphExample)_curMatch;
			graph.SettingAddedNodeNames( createProgramGraphExample_addedNodeNames );
			GRGEN_MODEL.@Class node_any = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@Class node_cell = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@Class node_recell = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@MethodSignature node_getS = GRGEN_MODEL.@MethodSignature.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_getB = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			GRGEN_MODEL.@Variabel node_cts = GRGEN_MODEL.@Variabel.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex1 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@MethodSignature node_setS = GRGEN_MODEL.@MethodSignature.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_setB = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			GRGEN_MODEL.@Constant node_n = GRGEN_MODEL.@Constant.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex2 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex3 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_setB2 = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			GRGEN_MODEL.@Constant node_n2 = GRGEN_MODEL.@Constant.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex4 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex5 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Variabel node_backup = GRGEN_MODEL.@Variabel.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex6 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex7 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@MethodSignature node_restoreS = GRGEN_MODEL.@MethodSignature.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_restoreB = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex8 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex9 = GRGEN_MODEL.@Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( createProgramGraphExample_addedEdgeNames );
			GRGEN_MODEL.@contains edge__edge0 = GRGEN_MODEL.@contains.CreateEdge(graph, node_any, node_cell);
			GRGEN_MODEL.@contains edge__edge1 = GRGEN_MODEL.@contains.CreateEdge(graph, node_cell, node_recell);
			GRGEN_MODEL.@contains edge__edge2 = GRGEN_MODEL.@contains.CreateEdge(graph, node_cell, node_getS);
			GRGEN_MODEL.@contains edge__edge3 = GRGEN_MODEL.@contains.CreateEdge(graph, node_cell, node_getB);
			GRGEN_MODEL.@bindsTo edge__edge4 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_getB, node_getS);
			GRGEN_MODEL.@contains edge__edge5 = GRGEN_MODEL.@contains.CreateEdge(graph, node_cell, node_cts);
			GRGEN_MODEL.@hasType edge__edge6 = GRGEN_MODEL.@hasType.CreateEdge(graph, node_cts, node_any);
			GRGEN_MODEL.@contains edge__edge7 = GRGEN_MODEL.@contains.CreateEdge(graph, node_getB, node_ex1);
			GRGEN_MODEL.@uses edge__edge8 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex1, node_cts);
			GRGEN_MODEL.@contains edge__edge9 = GRGEN_MODEL.@contains.CreateEdge(graph, node_cell, node_setS);
			GRGEN_MODEL.@contains edge__edge10 = GRGEN_MODEL.@contains.CreateEdge(graph, node_cell, node_setB);
			GRGEN_MODEL.@bindsTo edge__edge11 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_setB, node_setS);
			GRGEN_MODEL.@contains edge__edge12 = GRGEN_MODEL.@contains.CreateEdge(graph, node_setB, node_n);
			GRGEN_MODEL.@hasType edge__edge13 = GRGEN_MODEL.@hasType.CreateEdge(graph, node_n, node_any);
			GRGEN_MODEL.@contains edge__edge14 = GRGEN_MODEL.@contains.CreateEdge(graph, node_setB, node_ex2);
			GRGEN_MODEL.@writesTo edge__edge15 = GRGEN_MODEL.@writesTo.CreateEdge(graph, node_ex2, node_cts);
			GRGEN_MODEL.@contains edge__edge16 = GRGEN_MODEL.@contains.CreateEdge(graph, node_ex2, node_ex3);
			GRGEN_MODEL.@uses edge__edge17 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex3, node_n);
			GRGEN_MODEL.@contains edge__edge18 = GRGEN_MODEL.@contains.CreateEdge(graph, node_recell, node_setB2);
			GRGEN_MODEL.@bindsTo edge__edge19 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_setB2, node_setS);
			GRGEN_MODEL.@contains edge__edge20 = GRGEN_MODEL.@contains.CreateEdge(graph, node_setB2, node_n2);
			GRGEN_MODEL.@hasType edge__edge21 = GRGEN_MODEL.@hasType.CreateEdge(graph, node_n2, node_any);
			GRGEN_MODEL.@contains edge__edge22 = GRGEN_MODEL.@contains.CreateEdge(graph, node_setB2, node_ex4);
			GRGEN_MODEL.@calls edge__edge23 = GRGEN_MODEL.@calls.CreateEdge(graph, node_ex4, node_setS);
			GRGEN_MODEL.@contains edge__edge24 = GRGEN_MODEL.@contains.CreateEdge(graph, node_ex4, node_ex5);
			GRGEN_MODEL.@uses edge__edge25 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex5, node_n2);
			GRGEN_MODEL.@contains edge__edge26 = GRGEN_MODEL.@contains.CreateEdge(graph, node_recell, node_backup);
			GRGEN_MODEL.@hasType edge__edge27 = GRGEN_MODEL.@hasType.CreateEdge(graph, node_backup, node_any);
			GRGEN_MODEL.@contains edge__edge28 = GRGEN_MODEL.@contains.CreateEdge(graph, node_setB2, node_ex6);
			GRGEN_MODEL.@writesTo edge__edge29 = GRGEN_MODEL.@writesTo.CreateEdge(graph, node_ex6, node_backup);
			GRGEN_MODEL.@contains edge__edge30 = GRGEN_MODEL.@contains.CreateEdge(graph, node_ex6, node_ex7);
			GRGEN_MODEL.@uses edge__edge31 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex7, node_cts);
			GRGEN_MODEL.@contains edge__edge32 = GRGEN_MODEL.@contains.CreateEdge(graph, node_recell, node_restoreS);
			GRGEN_MODEL.@contains edge__edge33 = GRGEN_MODEL.@contains.CreateEdge(graph, node_recell, node_restoreB);
			GRGEN_MODEL.@bindsTo edge__edge34 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_restoreB, node_restoreS);
			GRGEN_MODEL.@contains edge__edge35 = GRGEN_MODEL.@contains.CreateEdge(graph, node_restoreB, node_ex8);
			GRGEN_MODEL.@writesTo edge__edge36 = GRGEN_MODEL.@writesTo.CreateEdge(graph, node_ex8, node_cts);
			GRGEN_MODEL.@contains edge__edge37 = GRGEN_MODEL.@contains.CreateEdge(graph, node_ex8, node_ex9);
			GRGEN_MODEL.@uses edge__edge38 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex9, node_backup);
			return;
		}
		private static string[] createProgramGraphExample_addedNodeNames = new string[] { "any", "cell", "recell", "getS", "getB", "cts", "ex1", "setS", "setB", "n", "ex2", "ex3", "setB2", "n2", "ex4", "ex5", "backup", "ex6", "ex7", "restoreS", "restoreB", "ex8", "ex9" };
		private static string[] createProgramGraphExample_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7", "_edge8", "_edge9", "_edge10", "_edge11", "_edge12", "_edge13", "_edge14", "_edge15", "_edge16", "_edge17", "_edge18", "_edge19", "_edge20", "_edge21", "_edge22", "_edge23", "_edge24", "_edge25", "_edge26", "_edge27", "_edge28", "_edge29", "_edge30", "_edge31", "_edge32", "_edge33", "_edge34", "_edge35", "_edge36", "_edge37", "_edge38" };

		static Rule_createProgramGraphExample() {
		}

		public interface IMatch_createProgramGraphExample : GRGEN_LIBGR.IMatch
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

		public class Match_createProgramGraphExample : GRGEN_LGSP.ListElement<Match_createProgramGraphExample>, IMatch_createProgramGraphExample
		{
			public enum createProgramGraphExample_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphExample_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphExample_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphExample_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphExample_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphExample_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphExample_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createProgramGraphExample.instance.pat_createProgramGraphExample; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createProgramGraphPullUp : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createProgramGraphPullUp instance = null;
		public static Rule_createProgramGraphPullUp Instance { get { if (instance==null) { instance = new Rule_createProgramGraphPullUp(); instance.initialize(); } return instance; } }

		public enum createProgramGraphPullUp_NodeNums { };
		public enum createProgramGraphPullUp_EdgeNums { };
		public enum createProgramGraphPullUp_VariableNums { };
		public enum createProgramGraphPullUp_SubNums { };
		public enum createProgramGraphPullUp_AltNums { };
		public enum createProgramGraphPullUp_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_createProgramGraphPullUp;


		private Rule_createProgramGraphPullUp()
		{
			name = "createProgramGraphPullUp";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Class.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, };

		}
		private void initialize()
		{
			bool[,] createProgramGraphPullUp_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createProgramGraphPullUp_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] createProgramGraphPullUp_minMatches = new int[0] ;
			int[] createProgramGraphPullUp_maxMatches = new int[0] ;
			pat_createProgramGraphPullUp = new GRGEN_LGSP.PatternGraph(
				"createProgramGraphPullUp",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				createProgramGraphPullUp_minMatches,
				createProgramGraphPullUp_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createProgramGraphPullUp_isNodeHomomorphicGlobal,
				createProgramGraphPullUp_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createProgramGraphPullUp;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IClass output_0, out GRGEN_MODEL.IMethodBody output_1)
		{
			Match_createProgramGraphPullUp curMatch = (Match_createProgramGraphPullUp)_curMatch;
			graph.SettingAddedNodeNames( createProgramGraphPullUp_addedNodeNames );
			GRGEN_MODEL.@Class node_c1 = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@Class node_c2 = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@Class node_c3 = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@Class node_c4 = GRGEN_MODEL.@Class.CreateNode(graph);
			GRGEN_MODEL.@MethodSignature node_m5 = GRGEN_MODEL.@MethodSignature.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_b2 = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			GRGEN_MODEL.@Variabel node_v7a = GRGEN_MODEL.@Variabel.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_b3 = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			GRGEN_MODEL.@Variabel node_v7b = GRGEN_MODEL.@Variabel.CreateNode(graph);
			GRGEN_MODEL.@MethodBody node_b4 = GRGEN_MODEL.@MethodBody.CreateNode(graph);
			GRGEN_MODEL.@MethodSignature node_m8 = GRGEN_MODEL.@MethodSignature.CreateNode(graph);
			GRGEN_MODEL.@Variabel node_v9 = GRGEN_MODEL.@Variabel.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex1 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex2 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex3 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex4 = GRGEN_MODEL.@Expression.CreateNode(graph);
			GRGEN_MODEL.@Expression node_ex5 = GRGEN_MODEL.@Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( createProgramGraphPullUp_addedEdgeNames );
			GRGEN_MODEL.@contains edge__edge0 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c1, node_c2);
			GRGEN_MODEL.@contains edge__edge1 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c1, node_c3);
			GRGEN_MODEL.@contains edge__edge2 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c1, node_c4);
			GRGEN_MODEL.@contains edge__edge3 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c1, node_m5);
			GRGEN_MODEL.@contains edge__edge4 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c2, node_b2);
			GRGEN_MODEL.@contains edge__edge5 = GRGEN_MODEL.@contains.CreateEdge(graph, node_b2, node_v7a);
			GRGEN_MODEL.@hasType edge__edge6 = GRGEN_MODEL.@hasType.CreateEdge(graph, node_v7a, node_c4);
			GRGEN_MODEL.@contains edge__edge7 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c3, node_b3);
			GRGEN_MODEL.@contains edge__edge8 = GRGEN_MODEL.@contains.CreateEdge(graph, node_b3, node_v7b);
			GRGEN_MODEL.@hasType edge__edge9 = GRGEN_MODEL.@hasType.CreateEdge(graph, node_v7b, node_c4);
			GRGEN_MODEL.@contains edge__edge10 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c4, node_b4);
			GRGEN_MODEL.@bindsTo edge__edge11 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_b2, node_m5);
			GRGEN_MODEL.@bindsTo edge__edge12 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_b3, node_m5);
			GRGEN_MODEL.@bindsTo edge__edge13 = GRGEN_MODEL.@bindsTo.CreateEdge(graph, node_b4, node_m5);
			GRGEN_MODEL.@contains edge__edge14 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c1, node_m8);
			GRGEN_MODEL.@contains edge__edge15 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c2, node_v9);
			GRGEN_MODEL.@hasType edge__edge16 = GRGEN_MODEL.@hasType.CreateEdge(graph, node_v9, node_c4);
			GRGEN_MODEL.@contains edge__edge17 = GRGEN_MODEL.@contains.CreateEdge(graph, node_b2, node_ex1);
			GRGEN_MODEL.@writesTo edge__edge18 = GRGEN_MODEL.@writesTo.CreateEdge(graph, node_ex1, node_v9);
			GRGEN_MODEL.@contains edge__edge19 = GRGEN_MODEL.@contains.CreateEdge(graph, node_ex1, node_ex);
			GRGEN_MODEL.@uses edge__edge20 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex, node_v7a);
			GRGEN_MODEL.@contains edge__edge21 = GRGEN_MODEL.@contains.CreateEdge(graph, node_b2, node_ex2);
			GRGEN_MODEL.@calls edge__edge22 = GRGEN_MODEL.@calls.CreateEdge(graph, node_ex2, node_m8);
			GRGEN_MODEL.@contains edge__edge23 = GRGEN_MODEL.@contains.CreateEdge(graph, node_ex2, node_ex3);
			GRGEN_MODEL.@uses edge__edge24 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex3, node_v9);
			GRGEN_MODEL.@contains edge__edge25 = GRGEN_MODEL.@contains.CreateEdge(graph, node_b3, node_ex4);
			GRGEN_MODEL.@calls edge__edge26 = GRGEN_MODEL.@calls.CreateEdge(graph, node_ex4, node_m8);
			GRGEN_MODEL.@contains edge__edge27 = GRGEN_MODEL.@contains.CreateEdge(graph, node_ex4, node_ex5);
			GRGEN_MODEL.@uses edge__edge28 = GRGEN_MODEL.@uses.CreateEdge(graph, node_ex5, node_v7b);
			output_0 = (GRGEN_MODEL.IClass)(node_c1);
			output_1 = (GRGEN_MODEL.IMethodBody)(node_b4);
			return;
		}
		private static string[] createProgramGraphPullUp_addedNodeNames = new string[] { "c1", "c2", "c3", "c4", "m5", "b2", "v7a", "b3", "v7b", "b4", "m8", "v9", "ex1", "ex", "ex2", "ex3", "ex4", "ex5" };
		private static string[] createProgramGraphPullUp_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7", "_edge8", "_edge9", "_edge10", "_edge11", "_edge12", "_edge13", "_edge14", "_edge15", "_edge16", "_edge17", "_edge18", "_edge19", "_edge20", "_edge21", "_edge22", "_edge23", "_edge24", "_edge25", "_edge26", "_edge27", "_edge28" };

		static Rule_createProgramGraphPullUp() {
		}

		public interface IMatch_createProgramGraphPullUp : GRGEN_LIBGR.IMatch
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

		public class Match_createProgramGraphPullUp : GRGEN_LGSP.ListElement<Match_createProgramGraphPullUp>, IMatch_createProgramGraphPullUp
		{
			public enum createProgramGraphPullUp_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphPullUp_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphPullUp_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphPullUp_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphPullUp_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphPullUp_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createProgramGraphPullUp_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createProgramGraphPullUp.instance.pat_createProgramGraphPullUp; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_pullUpMethod : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_pullUpMethod instance = null;
		public static Rule_pullUpMethod Instance { get { if (instance==null) { instance = new Rule_pullUpMethod(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] pullUpMethod_node_c1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] pullUpMethod_node_c3_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] pullUpMethod_node_b4_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] pullUpMethod_node_m5_AllowedTypes = null;
		public static bool[] pullUpMethod_node_c1_IsAllowedType = null;
		public static bool[] pullUpMethod_node_c3_IsAllowedType = null;
		public static bool[] pullUpMethod_node_b4_IsAllowedType = null;
		public static bool[] pullUpMethod_node_m5_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] pullUpMethod_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] pullUpMethod_edge_m_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] pullUpMethod_edge__edge1_AllowedTypes = null;
		public static bool[] pullUpMethod_edge__edge0_IsAllowedType = null;
		public static bool[] pullUpMethod_edge_m_IsAllowedType = null;
		public static bool[] pullUpMethod_edge__edge1_IsAllowedType = null;
		public enum pullUpMethod_NodeNums { @c1, @c3, @b4, @m5, };
		public enum pullUpMethod_EdgeNums { @_edge0, @m, @_edge1, };
		public enum pullUpMethod_VariableNums { };
		public enum pullUpMethod_SubNums { @bs, };
		public enum pullUpMethod_AltNums { };
		public enum pullUpMethod_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_pullUpMethod;


		private Rule_pullUpMethod()
		{
			name = "pullUpMethod";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Class.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "pullUpMethod_node_c1", "pullUpMethod_node_b4", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] pullUpMethod_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] pullUpMethod_isEdgeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			int[] pullUpMethod_minMatches = new int[0] ;
			int[] pullUpMethod_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode pullUpMethod_node_c1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "pullUpMethod_node_c1", "c1", pullUpMethod_node_c1_AllowedTypes, pullUpMethod_node_c1_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			GRGEN_LGSP.PatternNode pullUpMethod_node_c3 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "pullUpMethod_node_c3", "c3", pullUpMethod_node_c3_AllowedTypes, pullUpMethod_node_c3_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode pullUpMethod_node_b4 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "pullUpMethod_node_b4", "b4", pullUpMethod_node_b4_AllowedTypes, pullUpMethod_node_b4_IsAllowedType, 5.5F, 1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode pullUpMethod_node_m5 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodSignature, "GRGEN_MODEL.IMethodSignature", "pullUpMethod_node_m5", "m5", pullUpMethod_node_m5_AllowedTypes, pullUpMethod_node_m5_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge pullUpMethod_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "pullUpMethod_edge__edge0", "_edge0", pullUpMethod_edge__edge0_AllowedTypes, pullUpMethod_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge pullUpMethod_edge_m = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "pullUpMethod_edge_m", "m", pullUpMethod_edge_m_AllowedTypes, pullUpMethod_edge_m_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge pullUpMethod_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@bindsTo, "GRGEN_MODEL.IbindsTo", "pullUpMethod_edge__edge1", "_edge1", pullUpMethod_edge__edge1_AllowedTypes, pullUpMethod_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding pullUpMethod_bs = new GRGEN_LGSP.PatternGraphEmbedding("bs", Pattern_Bodies.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("pullUpMethod_node_m5"),
					new GRGEN_EXPR.GraphEntityExpression("pullUpMethod_node_c1"),
				}, 
				new string[] { "pullUpMethod_node_m5", "pullUpMethod_node_c1" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_pullUpMethod = new GRGEN_LGSP.PatternGraph(
				"pullUpMethod",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { pullUpMethod_node_c1, pullUpMethod_node_c3, pullUpMethod_node_b4, pullUpMethod_node_m5 }, 
				new GRGEN_LGSP.PatternEdge[] { pullUpMethod_edge__edge0, pullUpMethod_edge_m, pullUpMethod_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { pullUpMethod_bs }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				pullUpMethod_minMatches,
				pullUpMethod_maxMatches,
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
				pullUpMethod_isNodeHomomorphicGlobal,
				pullUpMethod_isEdgeHomomorphicGlobal
			);
			pat_pullUpMethod.edgeToSourceNode.Add(pullUpMethod_edge__edge0, pullUpMethod_node_c1);
			pat_pullUpMethod.edgeToTargetNode.Add(pullUpMethod_edge__edge0, pullUpMethod_node_c3);
			pat_pullUpMethod.edgeToSourceNode.Add(pullUpMethod_edge_m, pullUpMethod_node_c3);
			pat_pullUpMethod.edgeToTargetNode.Add(pullUpMethod_edge_m, pullUpMethod_node_b4);
			pat_pullUpMethod.edgeToSourceNode.Add(pullUpMethod_edge__edge1, pullUpMethod_node_b4);
			pat_pullUpMethod.edgeToTargetNode.Add(pullUpMethod_edge__edge1, pullUpMethod_node_m5);

			pullUpMethod_node_c1.pointOfDefinition = null;
			pullUpMethod_node_c3.pointOfDefinition = pat_pullUpMethod;
			pullUpMethod_node_b4.pointOfDefinition = null;
			pullUpMethod_node_m5.pointOfDefinition = pat_pullUpMethod;
			pullUpMethod_edge__edge0.pointOfDefinition = pat_pullUpMethod;
			pullUpMethod_edge_m.pointOfDefinition = pat_pullUpMethod;
			pullUpMethod_edge__edge1.pointOfDefinition = pat_pullUpMethod;
			pullUpMethod_bs.PointOfDefinition = pat_pullUpMethod;

			patternGraph = pat_pullUpMethod;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_pullUpMethod curMatch = (Match_pullUpMethod)_curMatch;
			GRGEN_LGSP.LGSPNode node_c1 = curMatch._node_c1;
			GRGEN_LGSP.LGSPNode node_b4 = curMatch._node_b4;
			GRGEN_LGSP.LGSPEdge edge_m = curMatch._edge_m;
			Pattern_Bodies.Match_Bodies subpattern_bs = curMatch.@_bs;
			graph.SettingAddedNodeNames( pullUpMethod_addedNodeNames );
			Pattern_Bodies.Instance.Bodies_Modify(graph, subpattern_bs);
			graph.SettingAddedEdgeNames( pullUpMethod_addedEdgeNames );
			GRGEN_MODEL.@contains edge__edge2 = GRGEN_MODEL.@contains.CreateEdge(graph, node_c1, node_b4);
			graph.Remove(edge_m);
			return;
		}
		private static string[] pullUpMethod_addedNodeNames = new string[] {  };
		private static string[] pullUpMethod_addedEdgeNames = new string[] { "_edge2" };

		static Rule_pullUpMethod() {
		}

		public interface IMatch_pullUpMethod : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c1 { get; }
			GRGEN_MODEL.IClass node_c3 { get; }
			GRGEN_MODEL.IMethodBody node_b4 { get; }
			GRGEN_MODEL.IMethodSignature node_m5 { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			GRGEN_MODEL.Icontains edge_m { get; }
			GRGEN_MODEL.IbindsTo edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_Bodies.Match_Bodies @bs { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_pullUpMethod : GRGEN_LGSP.ListElement<Match_pullUpMethod>, IMatch_pullUpMethod
		{
			public GRGEN_MODEL.IClass node_c1 { get { return (GRGEN_MODEL.IClass)_node_c1; } }
			public GRGEN_MODEL.IClass node_c3 { get { return (GRGEN_MODEL.IClass)_node_c3; } }
			public GRGEN_MODEL.IMethodBody node_b4 { get { return (GRGEN_MODEL.IMethodBody)_node_b4; } }
			public GRGEN_MODEL.IMethodSignature node_m5 { get { return (GRGEN_MODEL.IMethodSignature)_node_m5; } }
			public GRGEN_LGSP.LGSPNode _node_c1;
			public GRGEN_LGSP.LGSPNode _node_c3;
			public GRGEN_LGSP.LGSPNode _node_b4;
			public GRGEN_LGSP.LGSPNode _node_m5;
			public enum pullUpMethod_NodeNums { @c1, @c3, @b4, @m5, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)pullUpMethod_NodeNums.@c1: return _node_c1;
				case (int)pullUpMethod_NodeNums.@c3: return _node_c3;
				case (int)pullUpMethod_NodeNums.@b4: return _node_b4;
				case (int)pullUpMethod_NodeNums.@m5: return _node_m5;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_MODEL.Icontains edge_m { get { return (GRGEN_MODEL.Icontains)_edge_m; } }
			public GRGEN_MODEL.IbindsTo edge__edge1 { get { return (GRGEN_MODEL.IbindsTo)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge_m;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum pullUpMethod_EdgeNums { @_edge0, @m, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 3;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)pullUpMethod_EdgeNums.@_edge0: return _edge__edge0;
				case (int)pullUpMethod_EdgeNums.@m: return _edge_m;
				case (int)pullUpMethod_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum pullUpMethod_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Bodies.Match_Bodies @bs { get { return @_bs; } }
			public @Pattern_Bodies.Match_Bodies @_bs;
			public enum pullUpMethod_SubNums { @bs, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)pullUpMethod_SubNums.@bs: return _bs;
				default: return null;
				}
			}
			
			public enum pullUpMethod_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum pullUpMethod_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum pullUpMethod_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_pullUpMethod.instance.pat_pullUpMethod; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_matchAll : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_matchAll instance = null;
		public static Rule_matchAll Instance { get { if (instance==null) { instance = new Rule_matchAll(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] matchAll_node_c1_AllowedTypes = null;
		public static bool[] matchAll_node_c1_IsAllowedType = null;
		public enum matchAll_NodeNums { @c1, };
		public enum matchAll_EdgeNums { };
		public enum matchAll_VariableNums { };
		public enum matchAll_SubNums { @_sub0, };
		public enum matchAll_AltNums { };
		public enum matchAll_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_matchAll;


		private Rule_matchAll()
		{
			name = "matchAll";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Class.typeVar, };
			inputNames = new string[] { "matchAll_node_c1", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] matchAll_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] matchAll_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] matchAll_minMatches = new int[0] ;
			int[] matchAll_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode matchAll_node_c1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "matchAll_node_c1", "c1", matchAll_node_c1_AllowedTypes, matchAll_node_c1_IsAllowedType, 5.5F, 0, false, null, null, null, null);
			GRGEN_LGSP.PatternGraphEmbedding matchAll__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_Subclass.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("matchAll_node_c1"),
				}, 
				new string[] { "matchAll_node_c1" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_matchAll = new GRGEN_LGSP.PatternGraph(
				"matchAll",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { matchAll_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { matchAll__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				matchAll_minMatches,
				matchAll_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				matchAll_isNodeHomomorphicGlobal,
				matchAll_isEdgeHomomorphicGlobal
			);

			matchAll_node_c1.pointOfDefinition = null;
			matchAll__sub0.PointOfDefinition = pat_matchAll;

			patternGraph = pat_matchAll;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_matchAll curMatch = (Match_matchAll)_curMatch;
			GRGEN_LGSP.LGSPNode node_c1 = curMatch._node_c1;
			Pattern_Subclass.Match_Subclass subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_matchAll() {
		}

		public interface IMatch_matchAll : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c1 { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_Subclass.Match_Subclass @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_matchAll : GRGEN_LGSP.ListElement<Match_matchAll>, IMatch_matchAll
		{
			public GRGEN_MODEL.IClass node_c1 { get { return (GRGEN_MODEL.IClass)_node_c1; } }
			public GRGEN_LGSP.LGSPNode _node_c1;
			public enum matchAll_NodeNums { @c1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)matchAll_NodeNums.@c1: return _node_c1;
				default: return null;
				}
			}
			
			public enum matchAll_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum matchAll_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_Subclass.Match_Subclass @_sub0 { get { return @__sub0; } }
			public @Pattern_Subclass.Match_Subclass @__sub0;
			public enum matchAll_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)matchAll_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			
			public enum matchAll_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum matchAll_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum matchAll_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_matchAll.instance.pat_matchAll; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_InsertHelperEdgesForNestedLayout : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_InsertHelperEdgesForNestedLayout instance = null;
		public static Rule_InsertHelperEdgesForNestedLayout Instance { get { if (instance==null) { instance = new Rule_InsertHelperEdgesForNestedLayout(); instance.initialize(); } return instance; } }

		public enum InsertHelperEdgesForNestedLayout_NodeNums { };
		public enum InsertHelperEdgesForNestedLayout_EdgeNums { };
		public enum InsertHelperEdgesForNestedLayout_VariableNums { };
		public enum InsertHelperEdgesForNestedLayout_SubNums { };
		public enum InsertHelperEdgesForNestedLayout_AltNums { };
		public enum InsertHelperEdgesForNestedLayout_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_InsertHelperEdgesForNestedLayout;


		private Rule_InsertHelperEdgesForNestedLayout()
		{
			name = "InsertHelperEdgesForNestedLayout";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] InsertHelperEdgesForNestedLayout_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] InsertHelperEdgesForNestedLayout_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] InsertHelperEdgesForNestedLayout_minMatches = new int[0] ;
			int[] InsertHelperEdgesForNestedLayout_maxMatches = new int[0] ;
			pat_InsertHelperEdgesForNestedLayout = new GRGEN_LGSP.PatternGraph(
				"InsertHelperEdgesForNestedLayout",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				InsertHelperEdgesForNestedLayout_minMatches,
				InsertHelperEdgesForNestedLayout_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				InsertHelperEdgesForNestedLayout_isNodeHomomorphicGlobal,
				InsertHelperEdgesForNestedLayout_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_InsertHelperEdgesForNestedLayout;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_InsertHelperEdgesForNestedLayout curMatch = (Match_InsertHelperEdgesForNestedLayout)_curMatch;
			graph.SettingAddedNodeNames( InsertHelperEdgesForNestedLayout_addedNodeNames );
			graph.SettingAddedEdgeNames( InsertHelperEdgesForNestedLayout_addedEdgeNames );
			ApplyXGRS_InsertHelperEdgesForNestedLayout_0(graph);
			return;
		}
		private static string[] InsertHelperEdgesForNestedLayout_addedNodeNames = new string[] {  };
		private static string[] InsertHelperEdgesForNestedLayout_addedEdgeNames = new string[] {  };

        public static bool ApplyXGRS_InsertHelperEdgesForNestedLayout_0(GRGEN_LGSP.LGSPGraph graph)
        {
            GRGEN_LGSP.LGSPActions actions = graph.curActions;
            bool res_9;
            bool res_1;
            bool res_0;
            Action_LinkMethodBodyToContainedEntity rule_LinkMethodBodyToContainedEntity = Action_LinkMethodBodyToContainedEntity.Instance;
            bool res_8;
            bool res_3;
            bool res_2;
            Action_LinkMethodBodyToContainedExpressionTransitive rule_LinkMethodBodyToContainedExpressionTransitive = Action_LinkMethodBodyToContainedExpressionTransitive.Instance;
            bool res_7;
            bool res_4;
            Action_RemoveMethodBodyContainsBetweenExpressions rule_RemoveMethodBodyContainsBetweenExpressions = Action_RemoveMethodBodyContainsBetweenExpressions.Instance;
            bool res_6;
            bool res_5;
            Action_RetypeClassContainment rule_RetypeClassContainment = Action_RetypeClassContainment.Instance;
            long i_1 = 0;
            while(true)
            {
                GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches_0 = rule_LinkMethodBodyToContainedEntity.Match(graph, 1);
                graph.Matched(matches_0, false);
                if(matches_0.Count==0) {
                    res_0 = (bool)(false);
                } else {
                    res_0 = (bool)(true);
                    if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += matches_0.Count;
                    graph.Finishing(matches_0, false);
                    Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity match_0 = matches_0.FirstExact;
                    rule_LinkMethodBodyToContainedEntity.Modify(graph, match_0);
                    if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                    graph.Finished(matches_0, false);
                }
                if(!res_0) break;
                i_1++;
            }
            res_1 = (bool)(i_1 >= 0);
            if(!res_1)
                res_9 = (bool)(false);
            else
            {
                long i_3 = 0;
                while(true)
                {
                    GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches_2 = rule_LinkMethodBodyToContainedExpressionTransitive.Match(graph, 1);
                    graph.Matched(matches_2, false);
                    if(matches_2.Count==0) {
                        res_2 = (bool)(false);
                    } else {
                        res_2 = (bool)(true);
                        if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += matches_2.Count;
                        graph.Finishing(matches_2, false);
                        Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive match_2 = matches_2.FirstExact;
                        rule_LinkMethodBodyToContainedExpressionTransitive.Modify(graph, match_2);
                        if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                        graph.Finished(matches_2, false);
                    }
                    if(!res_2) break;
                    i_3++;
                }
                res_3 = (bool)(i_3 >= 0);
                if(!res_3)
                    res_8 = (bool)(false);
                else
                {
                    GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches_4 = rule_RemoveMethodBodyContainsBetweenExpressions.Match(graph, graph.MaxMatches);
                    graph.Matched(matches_4, false);
                    if(matches_4.Count==0) {
                        res_4 = (bool)(false);
                    } else {
                        res_4 = (bool)(true);
                        if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += matches_4.Count;
                        graph.Finishing(matches_4, false);
                        IEnumerator<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> enum_4 = matches_4.GetEnumeratorExact();
                        while(enum_4.MoveNext())
                        {
                            Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions match_4 = enum_4.Current;
                            if(match_4!=matches_4.FirstExact) graph.RewritingNextMatch();
                            rule_RemoveMethodBodyContainsBetweenExpressions.Modify(graph, match_4);
                            if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;
                        }
                        graph.Finished(matches_4, false);
                    }
                    if(!res_4)
                        res_7 = (bool)(false);
                    else
                    {
                        long i_6 = 0;
                        while(true)
                        {
                            GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches_5 = rule_RetypeClassContainment.Match(graph, 1);
                            graph.Matched(matches_5, false);
                            if(matches_5.Count==0) {
                                res_5 = (bool)(false);
                            } else {
                                res_5 = (bool)(true);
                                if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += matches_5.Count;
                                graph.Finishing(matches_5, false);
                                Rule_RetypeClassContainment.IMatch_RetypeClassContainment match_5 = matches_5.FirstExact;
                                rule_RetypeClassContainment.Modify(graph, match_5);
                                if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                                graph.Finished(matches_5, false);
                            }
                            if(!res_5) break;
                            i_6++;
                        }
                        res_6 = (bool)(i_6 >= 0);
                        res_7 = (bool)(res_6);
                    }
                    res_8 = (bool)(res_7);
                }
                res_9 = (bool)(res_8);
            }
            return res_9;
        }

		static Rule_InsertHelperEdgesForNestedLayout() {
		}

		public interface IMatch_InsertHelperEdgesForNestedLayout : GRGEN_LIBGR.IMatch
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

		public class Match_InsertHelperEdgesForNestedLayout : GRGEN_LGSP.ListElement<Match_InsertHelperEdgesForNestedLayout>, IMatch_InsertHelperEdgesForNestedLayout
		{
			public enum InsertHelperEdgesForNestedLayout_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InsertHelperEdgesForNestedLayout_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InsertHelperEdgesForNestedLayout_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InsertHelperEdgesForNestedLayout_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InsertHelperEdgesForNestedLayout_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InsertHelperEdgesForNestedLayout_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InsertHelperEdgesForNestedLayout_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_InsertHelperEdgesForNestedLayout.instance.pat_InsertHelperEdgesForNestedLayout; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_LinkMethodBodyToContainedEntity : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_LinkMethodBodyToContainedEntity instance = null;
		public static Rule_LinkMethodBodyToContainedEntity Instance { get { if (instance==null) { instance = new Rule_LinkMethodBodyToContainedEntity(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] LinkMethodBodyToContainedEntity_node_mb_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] LinkMethodBodyToContainedEntity_node_e_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedEntity_node_mb_IsAllowedType = null;
		public static bool[] LinkMethodBodyToContainedEntity_node_e_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] LinkMethodBodyToContainedEntity_edge__edge0_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedEntity_edge__edge0_IsAllowedType = null;
		public enum LinkMethodBodyToContainedEntity_NodeNums { @mb, @e, };
		public enum LinkMethodBodyToContainedEntity_EdgeNums { @_edge0, };
		public enum LinkMethodBodyToContainedEntity_VariableNums { };
		public enum LinkMethodBodyToContainedEntity_SubNums { };
		public enum LinkMethodBodyToContainedEntity_AltNums { };
		public enum LinkMethodBodyToContainedEntity_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_LinkMethodBodyToContainedEntity;

		public static GRGEN_LIBGR.EdgeType[] LinkMethodBodyToContainedEntity_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedEntity_neg_0_edge__edge0_IsAllowedType = null;
		public enum LinkMethodBodyToContainedEntity_neg_0_NodeNums { @mb, @e, };
		public enum LinkMethodBodyToContainedEntity_neg_0_EdgeNums { @_edge0, };
		public enum LinkMethodBodyToContainedEntity_neg_0_VariableNums { };
		public enum LinkMethodBodyToContainedEntity_neg_0_SubNums { };
		public enum LinkMethodBodyToContainedEntity_neg_0_AltNums { };
		public enum LinkMethodBodyToContainedEntity_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph LinkMethodBodyToContainedEntity_neg_0;


		private Rule_LinkMethodBodyToContainedEntity()
		{
			name = "LinkMethodBodyToContainedEntity";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] LinkMethodBodyToContainedEntity_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkMethodBodyToContainedEntity_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] LinkMethodBodyToContainedEntity_minMatches = new int[0] ;
			int[] LinkMethodBodyToContainedEntity_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedEntity_node_mb = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "LinkMethodBodyToContainedEntity_node_mb", "mb", LinkMethodBodyToContainedEntity_node_mb_AllowedTypes, LinkMethodBodyToContainedEntity_node_mb_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedEntity_node_e = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Entity, "GRGEN_MODEL.IEntity", "LinkMethodBodyToContainedEntity_node_e", "e", LinkMethodBodyToContainedEntity_node_e_AllowedTypes, LinkMethodBodyToContainedEntity_node_e_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedEntity_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "LinkMethodBodyToContainedEntity_edge__edge0", "_edge0", LinkMethodBodyToContainedEntity_edge__edge0_AllowedTypes, LinkMethodBodyToContainedEntity_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			bool[,] LinkMethodBodyToContainedEntity_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkMethodBodyToContainedEntity_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] LinkMethodBodyToContainedEntity_neg_0_minMatches = new int[0] ;
			int[] LinkMethodBodyToContainedEntity_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@methodBodyContains, "GRGEN_MODEL.ImethodBodyContains", "LinkMethodBodyToContainedEntity_neg_0_edge__edge0", "_edge0", LinkMethodBodyToContainedEntity_neg_0_edge__edge0_AllowedTypes, LinkMethodBodyToContainedEntity_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			LinkMethodBodyToContainedEntity_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"LinkMethodBodyToContainedEntity_",
				false,
				new GRGEN_LGSP.PatternNode[] { LinkMethodBodyToContainedEntity_node_mb, LinkMethodBodyToContainedEntity_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { LinkMethodBodyToContainedEntity_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				LinkMethodBodyToContainedEntity_neg_0_minMatches,
				LinkMethodBodyToContainedEntity_neg_0_maxMatches,
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
				LinkMethodBodyToContainedEntity_neg_0_isNodeHomomorphicGlobal,
				LinkMethodBodyToContainedEntity_neg_0_isEdgeHomomorphicGlobal
			);
			LinkMethodBodyToContainedEntity_neg_0.edgeToSourceNode.Add(LinkMethodBodyToContainedEntity_neg_0_edge__edge0, LinkMethodBodyToContainedEntity_node_mb);
			LinkMethodBodyToContainedEntity_neg_0.edgeToTargetNode.Add(LinkMethodBodyToContainedEntity_neg_0_edge__edge0, LinkMethodBodyToContainedEntity_node_e);

			pat_LinkMethodBodyToContainedEntity = new GRGEN_LGSP.PatternGraph(
				"LinkMethodBodyToContainedEntity",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { LinkMethodBodyToContainedEntity_node_mb, LinkMethodBodyToContainedEntity_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { LinkMethodBodyToContainedEntity_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				LinkMethodBodyToContainedEntity_minMatches,
				LinkMethodBodyToContainedEntity_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { LinkMethodBodyToContainedEntity_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				LinkMethodBodyToContainedEntity_isNodeHomomorphicGlobal,
				LinkMethodBodyToContainedEntity_isEdgeHomomorphicGlobal
			);
			pat_LinkMethodBodyToContainedEntity.edgeToSourceNode.Add(LinkMethodBodyToContainedEntity_edge__edge0, LinkMethodBodyToContainedEntity_node_mb);
			pat_LinkMethodBodyToContainedEntity.edgeToTargetNode.Add(LinkMethodBodyToContainedEntity_edge__edge0, LinkMethodBodyToContainedEntity_node_e);
			LinkMethodBodyToContainedEntity_neg_0.embeddingGraph = pat_LinkMethodBodyToContainedEntity;

			LinkMethodBodyToContainedEntity_node_mb.pointOfDefinition = pat_LinkMethodBodyToContainedEntity;
			LinkMethodBodyToContainedEntity_node_e.pointOfDefinition = pat_LinkMethodBodyToContainedEntity;
			LinkMethodBodyToContainedEntity_edge__edge0.pointOfDefinition = pat_LinkMethodBodyToContainedEntity;
			LinkMethodBodyToContainedEntity_neg_0_edge__edge0.pointOfDefinition = LinkMethodBodyToContainedEntity_neg_0;

			patternGraph = pat_LinkMethodBodyToContainedEntity;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_LinkMethodBodyToContainedEntity curMatch = (Match_LinkMethodBodyToContainedEntity)_curMatch;
			GRGEN_LGSP.LGSPNode node_mb = curMatch._node_mb;
			GRGEN_LGSP.LGSPNode node_e = curMatch._node_e;
			graph.SettingAddedNodeNames( LinkMethodBodyToContainedEntity_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkMethodBodyToContainedEntity_addedEdgeNames );
			GRGEN_MODEL.@methodBodyContains edge__edge1 = GRGEN_MODEL.@methodBodyContains.CreateEdge(graph, node_mb, node_e);
			return;
		}
		private static string[] LinkMethodBodyToContainedEntity_addedNodeNames = new string[] {  };
		private static string[] LinkMethodBodyToContainedEntity_addedEdgeNames = new string[] { "_edge1" };

		static Rule_LinkMethodBodyToContainedEntity() {
		}

		public interface IMatch_LinkMethodBodyToContainedEntity : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_mb { get; }
			GRGEN_MODEL.IEntity node_e { get; }
			//Edges
			GRGEN_MODEL.Icontains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_LinkMethodBodyToContainedEntity_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_mb { get; }
			GRGEN_MODEL.IEntity node_e { get; }
			//Edges
			GRGEN_MODEL.ImethodBodyContains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_LinkMethodBodyToContainedEntity : GRGEN_LGSP.ListElement<Match_LinkMethodBodyToContainedEntity>, IMatch_LinkMethodBodyToContainedEntity
		{
			public GRGEN_MODEL.IMethodBody node_mb { get { return (GRGEN_MODEL.IMethodBody)_node_mb; } }
			public GRGEN_MODEL.IEntity node_e { get { return (GRGEN_MODEL.IEntity)_node_e; } }
			public GRGEN_LGSP.LGSPNode _node_mb;
			public GRGEN_LGSP.LGSPNode _node_e;
			public enum LinkMethodBodyToContainedEntity_NodeNums { @mb, @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedEntity_NodeNums.@mb: return _node_mb;
				case (int)LinkMethodBodyToContainedEntity_NodeNums.@e: return _node_e;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge__edge0 { get { return (GRGEN_MODEL.Icontains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum LinkMethodBodyToContainedEntity_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedEntity_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_LinkMethodBodyToContainedEntity.instance.pat_LinkMethodBodyToContainedEntity; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_LinkMethodBodyToContainedEntity_neg_0 : GRGEN_LGSP.ListElement<Match_LinkMethodBodyToContainedEntity_neg_0>, IMatch_LinkMethodBodyToContainedEntity_neg_0
		{
			public GRGEN_MODEL.IMethodBody node_mb { get { return (GRGEN_MODEL.IMethodBody)_node_mb; } }
			public GRGEN_MODEL.IEntity node_e { get { return (GRGEN_MODEL.IEntity)_node_e; } }
			public GRGEN_LGSP.LGSPNode _node_mb;
			public GRGEN_LGSP.LGSPNode _node_e;
			public enum LinkMethodBodyToContainedEntity_neg_0_NodeNums { @mb, @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedEntity_neg_0_NodeNums.@mb: return _node_mb;
				case (int)LinkMethodBodyToContainedEntity_neg_0_NodeNums.@e: return _node_e;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImethodBodyContains edge__edge0 { get { return (GRGEN_MODEL.ImethodBodyContains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum LinkMethodBodyToContainedEntity_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedEntity_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedEntity_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_LinkMethodBodyToContainedEntity.instance.LinkMethodBodyToContainedEntity_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_LinkMethodBodyToContainedExpressionTransitive : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_LinkMethodBodyToContainedExpressionTransitive instance = null;
		public static Rule_LinkMethodBodyToContainedExpressionTransitive Instance { get { if (instance==null) { instance = new Rule_LinkMethodBodyToContainedExpressionTransitive(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] LinkMethodBodyToContainedExpressionTransitive_node_mb_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] LinkMethodBodyToContainedExpressionTransitive_node_e1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] LinkMethodBodyToContainedExpressionTransitive_node_e2_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedExpressionTransitive_node_mb_IsAllowedType = null;
		public static bool[] LinkMethodBodyToContainedExpressionTransitive_node_e1_IsAllowedType = null;
		public static bool[] LinkMethodBodyToContainedExpressionTransitive_node_e2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] LinkMethodBodyToContainedExpressionTransitive_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] LinkMethodBodyToContainedExpressionTransitive_edge__edge1_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedExpressionTransitive_edge__edge0_IsAllowedType = null;
		public static bool[] LinkMethodBodyToContainedExpressionTransitive_edge__edge1_IsAllowedType = null;
		public enum LinkMethodBodyToContainedExpressionTransitive_NodeNums { @mb, @e1, @e2, };
		public enum LinkMethodBodyToContainedExpressionTransitive_EdgeNums { @_edge0, @_edge1, };
		public enum LinkMethodBodyToContainedExpressionTransitive_VariableNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_SubNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_AltNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_LinkMethodBodyToContainedExpressionTransitive;

		public static GRGEN_LIBGR.EdgeType[] LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_IsAllowedType = null;
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_NodeNums { @e1, @e2, };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_EdgeNums { @_edge0, };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_VariableNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_SubNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_AltNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph LinkMethodBodyToContainedExpressionTransitive_neg_0;


		private Rule_LinkMethodBodyToContainedExpressionTransitive()
		{
			name = "LinkMethodBodyToContainedExpressionTransitive";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] LinkMethodBodyToContainedExpressionTransitive_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] LinkMethodBodyToContainedExpressionTransitive_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] LinkMethodBodyToContainedExpressionTransitive_minMatches = new int[0] ;
			int[] LinkMethodBodyToContainedExpressionTransitive_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedExpressionTransitive_node_mb = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@MethodBody, "GRGEN_MODEL.IMethodBody", "LinkMethodBodyToContainedExpressionTransitive_node_mb", "mb", LinkMethodBodyToContainedExpressionTransitive_node_mb_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_node_mb_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedExpressionTransitive_node_e1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "LinkMethodBodyToContainedExpressionTransitive_node_e1", "e1", LinkMethodBodyToContainedExpressionTransitive_node_e1_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_node_e1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedExpressionTransitive_node_e2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "LinkMethodBodyToContainedExpressionTransitive_node_e2", "e2", LinkMethodBodyToContainedExpressionTransitive_node_e2_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_node_e2_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@methodBodyContains, "GRGEN_MODEL.ImethodBodyContains", "LinkMethodBodyToContainedExpressionTransitive_edge__edge0", "_edge0", LinkMethodBodyToContainedExpressionTransitive_edge__edge0_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "LinkMethodBodyToContainedExpressionTransitive_edge__edge1", "_edge1", LinkMethodBodyToContainedExpressionTransitive_edge__edge1_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			bool[,] LinkMethodBodyToContainedExpressionTransitive_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkMethodBodyToContainedExpressionTransitive_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] LinkMethodBodyToContainedExpressionTransitive_neg_0_minMatches = new int[0] ;
			int[] LinkMethodBodyToContainedExpressionTransitive_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@methodBodyContains, "GRGEN_MODEL.ImethodBodyContains", "LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0", "_edge0", LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			LinkMethodBodyToContainedExpressionTransitive_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"LinkMethodBodyToContainedExpressionTransitive_",
				false,
				new GRGEN_LGSP.PatternNode[] { LinkMethodBodyToContainedExpressionTransitive_node_e1, LinkMethodBodyToContainedExpressionTransitive_node_e2 }, 
				new GRGEN_LGSP.PatternEdge[] { LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				LinkMethodBodyToContainedExpressionTransitive_neg_0_minMatches,
				LinkMethodBodyToContainedExpressionTransitive_neg_0_maxMatches,
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
				LinkMethodBodyToContainedExpressionTransitive_neg_0_isNodeHomomorphicGlobal,
				LinkMethodBodyToContainedExpressionTransitive_neg_0_isEdgeHomomorphicGlobal
			);
			LinkMethodBodyToContainedExpressionTransitive_neg_0.edgeToSourceNode.Add(LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0, LinkMethodBodyToContainedExpressionTransitive_node_e1);
			LinkMethodBodyToContainedExpressionTransitive_neg_0.edgeToTargetNode.Add(LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0, LinkMethodBodyToContainedExpressionTransitive_node_e2);

			pat_LinkMethodBodyToContainedExpressionTransitive = new GRGEN_LGSP.PatternGraph(
				"LinkMethodBodyToContainedExpressionTransitive",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { LinkMethodBodyToContainedExpressionTransitive_node_mb, LinkMethodBodyToContainedExpressionTransitive_node_e1, LinkMethodBodyToContainedExpressionTransitive_node_e2 }, 
				new GRGEN_LGSP.PatternEdge[] { LinkMethodBodyToContainedExpressionTransitive_edge__edge0, LinkMethodBodyToContainedExpressionTransitive_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				LinkMethodBodyToContainedExpressionTransitive_minMatches,
				LinkMethodBodyToContainedExpressionTransitive_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { LinkMethodBodyToContainedExpressionTransitive_neg_0,  }, 
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
				LinkMethodBodyToContainedExpressionTransitive_isNodeHomomorphicGlobal,
				LinkMethodBodyToContainedExpressionTransitive_isEdgeHomomorphicGlobal
			);
			pat_LinkMethodBodyToContainedExpressionTransitive.edgeToSourceNode.Add(LinkMethodBodyToContainedExpressionTransitive_edge__edge0, LinkMethodBodyToContainedExpressionTransitive_node_mb);
			pat_LinkMethodBodyToContainedExpressionTransitive.edgeToTargetNode.Add(LinkMethodBodyToContainedExpressionTransitive_edge__edge0, LinkMethodBodyToContainedExpressionTransitive_node_e1);
			pat_LinkMethodBodyToContainedExpressionTransitive.edgeToSourceNode.Add(LinkMethodBodyToContainedExpressionTransitive_edge__edge1, LinkMethodBodyToContainedExpressionTransitive_node_e1);
			pat_LinkMethodBodyToContainedExpressionTransitive.edgeToTargetNode.Add(LinkMethodBodyToContainedExpressionTransitive_edge__edge1, LinkMethodBodyToContainedExpressionTransitive_node_e2);
			LinkMethodBodyToContainedExpressionTransitive_neg_0.embeddingGraph = pat_LinkMethodBodyToContainedExpressionTransitive;

			LinkMethodBodyToContainedExpressionTransitive_node_mb.pointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_node_e1.pointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_node_e2.pointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_edge__edge0.pointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_edge__edge1.pointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.pointOfDefinition = LinkMethodBodyToContainedExpressionTransitive_neg_0;

			patternGraph = pat_LinkMethodBodyToContainedExpressionTransitive;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_LinkMethodBodyToContainedExpressionTransitive curMatch = (Match_LinkMethodBodyToContainedExpressionTransitive)_curMatch;
			GRGEN_LGSP.LGSPNode node_e1 = curMatch._node_e1;
			GRGEN_LGSP.LGSPNode node_e2 = curMatch._node_e2;
			GRGEN_LGSP.LGSPNode node_mb = curMatch._node_mb;
			graph.SettingAddedNodeNames( LinkMethodBodyToContainedExpressionTransitive_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkMethodBodyToContainedExpressionTransitive_addedEdgeNames );
			GRGEN_MODEL.@methodBodyContains edge__edge2 = GRGEN_MODEL.@methodBodyContains.CreateEdge(graph, node_e1, node_e2);
			GRGEN_MODEL.@methodBodyContains edge__edge3 = GRGEN_MODEL.@methodBodyContains.CreateEdge(graph, node_mb, node_e2);
			return;
		}
		private static string[] LinkMethodBodyToContainedExpressionTransitive_addedNodeNames = new string[] {  };
		private static string[] LinkMethodBodyToContainedExpressionTransitive_addedEdgeNames = new string[] { "_edge2", "_edge3" };

		static Rule_LinkMethodBodyToContainedExpressionTransitive() {
		}

		public interface IMatch_LinkMethodBodyToContainedExpressionTransitive : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IMethodBody node_mb { get; }
			GRGEN_MODEL.IExpression node_e1 { get; }
			GRGEN_MODEL.IExpression node_e2 { get; }
			//Edges
			GRGEN_MODEL.ImethodBodyContains edge__edge0 { get; }
			GRGEN_MODEL.Icontains edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_LinkMethodBodyToContainedExpressionTransitive_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IExpression node_e1 { get; }
			GRGEN_MODEL.IExpression node_e2 { get; }
			//Edges
			GRGEN_MODEL.ImethodBodyContains edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_LinkMethodBodyToContainedExpressionTransitive : GRGEN_LGSP.ListElement<Match_LinkMethodBodyToContainedExpressionTransitive>, IMatch_LinkMethodBodyToContainedExpressionTransitive
		{
			public GRGEN_MODEL.IMethodBody node_mb { get { return (GRGEN_MODEL.IMethodBody)_node_mb; } }
			public GRGEN_MODEL.IExpression node_e1 { get { return (GRGEN_MODEL.IExpression)_node_e1; } }
			public GRGEN_MODEL.IExpression node_e2 { get { return (GRGEN_MODEL.IExpression)_node_e2; } }
			public GRGEN_LGSP.LGSPNode _node_mb;
			public GRGEN_LGSP.LGSPNode _node_e1;
			public GRGEN_LGSP.LGSPNode _node_e2;
			public enum LinkMethodBodyToContainedExpressionTransitive_NodeNums { @mb, @e1, @e2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@mb: return _node_mb;
				case (int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e1: return _node_e1;
				case (int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e2: return _node_e2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImethodBodyContains edge__edge0 { get { return (GRGEN_MODEL.ImethodBodyContains)_edge__edge0; } }
			public GRGEN_MODEL.Icontains edge__edge1 { get { return (GRGEN_MODEL.Icontains)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum LinkMethodBodyToContainedExpressionTransitive_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedExpressionTransitive_EdgeNums.@_edge0: return _edge__edge0;
				case (int)LinkMethodBodyToContainedExpressionTransitive_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_LinkMethodBodyToContainedExpressionTransitive.instance.pat_LinkMethodBodyToContainedExpressionTransitive; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_LinkMethodBodyToContainedExpressionTransitive_neg_0 : GRGEN_LGSP.ListElement<Match_LinkMethodBodyToContainedExpressionTransitive_neg_0>, IMatch_LinkMethodBodyToContainedExpressionTransitive_neg_0
		{
			public GRGEN_MODEL.IExpression node_e1 { get { return (GRGEN_MODEL.IExpression)_node_e1; } }
			public GRGEN_MODEL.IExpression node_e2 { get { return (GRGEN_MODEL.IExpression)_node_e2; } }
			public GRGEN_LGSP.LGSPNode _node_e1;
			public GRGEN_LGSP.LGSPNode _node_e2;
			public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_NodeNums { @e1, @e2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedExpressionTransitive_neg_0_NodeNums.@e1: return _node_e1;
				case (int)LinkMethodBodyToContainedExpressionTransitive_neg_0_NodeNums.@e2: return _node_e2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImethodBodyContains edge__edge0 { get { return (GRGEN_MODEL.ImethodBodyContains)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)LinkMethodBodyToContainedExpressionTransitive_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_LinkMethodBodyToContainedExpressionTransitive.instance.LinkMethodBodyToContainedExpressionTransitive_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_RemoveMethodBodyContainsBetweenExpressions : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_RemoveMethodBodyContainsBetweenExpressions instance = null;
		public static Rule_RemoveMethodBodyContainsBetweenExpressions Instance { get { if (instance==null) { instance = new Rule_RemoveMethodBodyContainsBetweenExpressions(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] RemoveMethodBodyContainsBetweenExpressions_node_e1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] RemoveMethodBodyContainsBetweenExpressions_node_e2_AllowedTypes = null;
		public static bool[] RemoveMethodBodyContainsBetweenExpressions_node_e1_IsAllowedType = null;
		public static bool[] RemoveMethodBodyContainsBetweenExpressions_node_e2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] RemoveMethodBodyContainsBetweenExpressions_edge_mbc_AllowedTypes = null;
		public static bool[] RemoveMethodBodyContainsBetweenExpressions_edge_mbc_IsAllowedType = null;
		public enum RemoveMethodBodyContainsBetweenExpressions_NodeNums { @e1, @e2, };
		public enum RemoveMethodBodyContainsBetweenExpressions_EdgeNums { @mbc, };
		public enum RemoveMethodBodyContainsBetweenExpressions_VariableNums { };
		public enum RemoveMethodBodyContainsBetweenExpressions_SubNums { };
		public enum RemoveMethodBodyContainsBetweenExpressions_AltNums { };
		public enum RemoveMethodBodyContainsBetweenExpressions_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_RemoveMethodBodyContainsBetweenExpressions;


		private Rule_RemoveMethodBodyContainsBetweenExpressions()
		{
			name = "RemoveMethodBodyContainsBetweenExpressions";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] RemoveMethodBodyContainsBetweenExpressions_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] RemoveMethodBodyContainsBetweenExpressions_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] RemoveMethodBodyContainsBetweenExpressions_minMatches = new int[0] ;
			int[] RemoveMethodBodyContainsBetweenExpressions_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode RemoveMethodBodyContainsBetweenExpressions_node_e1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "RemoveMethodBodyContainsBetweenExpressions_node_e1", "e1", RemoveMethodBodyContainsBetweenExpressions_node_e1_AllowedTypes, RemoveMethodBodyContainsBetweenExpressions_node_e1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode RemoveMethodBodyContainsBetweenExpressions_node_e2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Expression, "GRGEN_MODEL.IExpression", "RemoveMethodBodyContainsBetweenExpressions_node_e2", "e2", RemoveMethodBodyContainsBetweenExpressions_node_e2_AllowedTypes, RemoveMethodBodyContainsBetweenExpressions_node_e2_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge RemoveMethodBodyContainsBetweenExpressions_edge_mbc = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@methodBodyContains, "GRGEN_MODEL.ImethodBodyContains", "RemoveMethodBodyContainsBetweenExpressions_edge_mbc", "mbc", RemoveMethodBodyContainsBetweenExpressions_edge_mbc_AllowedTypes, RemoveMethodBodyContainsBetweenExpressions_edge_mbc_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			pat_RemoveMethodBodyContainsBetweenExpressions = new GRGEN_LGSP.PatternGraph(
				"RemoveMethodBodyContainsBetweenExpressions",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { RemoveMethodBodyContainsBetweenExpressions_node_e1, RemoveMethodBodyContainsBetweenExpressions_node_e2 }, 
				new GRGEN_LGSP.PatternEdge[] { RemoveMethodBodyContainsBetweenExpressions_edge_mbc }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				RemoveMethodBodyContainsBetweenExpressions_minMatches,
				RemoveMethodBodyContainsBetweenExpressions_maxMatches,
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
				RemoveMethodBodyContainsBetweenExpressions_isNodeHomomorphicGlobal,
				RemoveMethodBodyContainsBetweenExpressions_isEdgeHomomorphicGlobal
			);
			pat_RemoveMethodBodyContainsBetweenExpressions.edgeToSourceNode.Add(RemoveMethodBodyContainsBetweenExpressions_edge_mbc, RemoveMethodBodyContainsBetweenExpressions_node_e1);
			pat_RemoveMethodBodyContainsBetweenExpressions.edgeToTargetNode.Add(RemoveMethodBodyContainsBetweenExpressions_edge_mbc, RemoveMethodBodyContainsBetweenExpressions_node_e2);

			RemoveMethodBodyContainsBetweenExpressions_node_e1.pointOfDefinition = pat_RemoveMethodBodyContainsBetweenExpressions;
			RemoveMethodBodyContainsBetweenExpressions_node_e2.pointOfDefinition = pat_RemoveMethodBodyContainsBetweenExpressions;
			RemoveMethodBodyContainsBetweenExpressions_edge_mbc.pointOfDefinition = pat_RemoveMethodBodyContainsBetweenExpressions;

			patternGraph = pat_RemoveMethodBodyContainsBetweenExpressions;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_RemoveMethodBodyContainsBetweenExpressions curMatch = (Match_RemoveMethodBodyContainsBetweenExpressions)_curMatch;
			GRGEN_LGSP.LGSPEdge edge_mbc = curMatch._edge_mbc;
			graph.SettingAddedNodeNames( RemoveMethodBodyContainsBetweenExpressions_addedNodeNames );
			graph.SettingAddedEdgeNames( RemoveMethodBodyContainsBetweenExpressions_addedEdgeNames );
			graph.Remove(edge_mbc);
			return;
		}
		private static string[] RemoveMethodBodyContainsBetweenExpressions_addedNodeNames = new string[] {  };
		private static string[] RemoveMethodBodyContainsBetweenExpressions_addedEdgeNames = new string[] {  };

		static Rule_RemoveMethodBodyContainsBetweenExpressions() {
		}

		public interface IMatch_RemoveMethodBodyContainsBetweenExpressions : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IExpression node_e1 { get; }
			GRGEN_MODEL.IExpression node_e2 { get; }
			//Edges
			GRGEN_MODEL.ImethodBodyContains edge_mbc { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_RemoveMethodBodyContainsBetweenExpressions : GRGEN_LGSP.ListElement<Match_RemoveMethodBodyContainsBetweenExpressions>, IMatch_RemoveMethodBodyContainsBetweenExpressions
		{
			public GRGEN_MODEL.IExpression node_e1 { get { return (GRGEN_MODEL.IExpression)_node_e1; } }
			public GRGEN_MODEL.IExpression node_e2 { get { return (GRGEN_MODEL.IExpression)_node_e2; } }
			public GRGEN_LGSP.LGSPNode _node_e1;
			public GRGEN_LGSP.LGSPNode _node_e2;
			public enum RemoveMethodBodyContainsBetweenExpressions_NodeNums { @e1, @e2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)RemoveMethodBodyContainsBetweenExpressions_NodeNums.@e1: return _node_e1;
				case (int)RemoveMethodBodyContainsBetweenExpressions_NodeNums.@e2: return _node_e2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImethodBodyContains edge_mbc { get { return (GRGEN_MODEL.ImethodBodyContains)_edge_mbc; } }
			public GRGEN_LGSP.LGSPEdge _edge_mbc;
			public enum RemoveMethodBodyContainsBetweenExpressions_EdgeNums { @mbc, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)RemoveMethodBodyContainsBetweenExpressions_EdgeNums.@mbc: return _edge_mbc;
				default: return null;
				}
			}
			
			public enum RemoveMethodBodyContainsBetweenExpressions_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RemoveMethodBodyContainsBetweenExpressions_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RemoveMethodBodyContainsBetweenExpressions_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RemoveMethodBodyContainsBetweenExpressions_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RemoveMethodBodyContainsBetweenExpressions_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_RemoveMethodBodyContainsBetweenExpressions.instance.pat_RemoveMethodBodyContainsBetweenExpressions; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_RetypeClassContainment : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_RetypeClassContainment instance = null;
		public static Rule_RetypeClassContainment Instance { get { if (instance==null) { instance = new Rule_RetypeClassContainment(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] RetypeClassContainment_node_c1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] RetypeClassContainment_node_c2_AllowedTypes = null;
		public static bool[] RetypeClassContainment_node_c1_IsAllowedType = null;
		public static bool[] RetypeClassContainment_node_c2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] RetypeClassContainment_edge_c_AllowedTypes = { GRGEN_MODEL.EdgeType_contains.typeVar, };
		public static bool[] RetypeClassContainment_edge_c_IsAllowedType = { false, false, false, true, false, false, false, false, false, false, false, false, };
		public enum RetypeClassContainment_NodeNums { @c1, @c2, };
		public enum RetypeClassContainment_EdgeNums { @c, };
		public enum RetypeClassContainment_VariableNums { };
		public enum RetypeClassContainment_SubNums { };
		public enum RetypeClassContainment_AltNums { };
		public enum RetypeClassContainment_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_RetypeClassContainment;


		private Rule_RetypeClassContainment()
		{
			name = "RetypeClassContainment";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] RetypeClassContainment_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] RetypeClassContainment_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] RetypeClassContainment_minMatches = new int[0] ;
			int[] RetypeClassContainment_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode RetypeClassContainment_node_c1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "RetypeClassContainment_node_c1", "c1", RetypeClassContainment_node_c1_AllowedTypes, RetypeClassContainment_node_c1_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternNode RetypeClassContainment_node_c2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Class, "GRGEN_MODEL.IClass", "RetypeClassContainment_node_c2", "c2", RetypeClassContainment_node_c2_AllowedTypes, RetypeClassContainment_node_c2_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			GRGEN_LGSP.PatternEdge RetypeClassContainment_edge_c = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@contains, "GRGEN_MODEL.Icontains", "RetypeClassContainment_edge_c", "c", RetypeClassContainment_edge_c_AllowedTypes, RetypeClassContainment_edge_c_IsAllowedType, 5.5F, -1, false, null, null, null, null);
			pat_RetypeClassContainment = new GRGEN_LGSP.PatternGraph(
				"RetypeClassContainment",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { RetypeClassContainment_node_c1, RetypeClassContainment_node_c2 }, 
				new GRGEN_LGSP.PatternEdge[] { RetypeClassContainment_edge_c }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				RetypeClassContainment_minMatches,
				RetypeClassContainment_maxMatches,
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
				RetypeClassContainment_isNodeHomomorphicGlobal,
				RetypeClassContainment_isEdgeHomomorphicGlobal
			);
			pat_RetypeClassContainment.edgeToSourceNode.Add(RetypeClassContainment_edge_c, RetypeClassContainment_node_c1);
			pat_RetypeClassContainment.edgeToTargetNode.Add(RetypeClassContainment_edge_c, RetypeClassContainment_node_c2);

			RetypeClassContainment_node_c1.pointOfDefinition = pat_RetypeClassContainment;
			RetypeClassContainment_node_c2.pointOfDefinition = pat_RetypeClassContainment;
			RetypeClassContainment_edge_c.pointOfDefinition = pat_RetypeClassContainment;

			patternGraph = pat_RetypeClassContainment;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_RetypeClassContainment curMatch = (Match_RetypeClassContainment)_curMatch;
			GRGEN_LGSP.LGSPEdge edge_c = curMatch._edge_c;
			graph.SettingAddedNodeNames( RetypeClassContainment_addedNodeNames );
			graph.SettingAddedEdgeNames( RetypeClassContainment_addedEdgeNames );
			GRGEN_LGSP.LGSPEdge edge__edge0 = graph.Retype(edge_c, GRGEN_MODEL.EdgeType_classContainsClass.typeVar);
			return;
		}
		private static string[] RetypeClassContainment_addedNodeNames = new string[] {  };
		private static string[] RetypeClassContainment_addedEdgeNames = new string[] {  };

		static Rule_RetypeClassContainment() {
		}

		public interface IMatch_RetypeClassContainment : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IClass node_c1 { get; }
			GRGEN_MODEL.IClass node_c2 { get; }
			//Edges
			GRGEN_MODEL.Icontains edge_c { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_RetypeClassContainment : GRGEN_LGSP.ListElement<Match_RetypeClassContainment>, IMatch_RetypeClassContainment
		{
			public GRGEN_MODEL.IClass node_c1 { get { return (GRGEN_MODEL.IClass)_node_c1; } }
			public GRGEN_MODEL.IClass node_c2 { get { return (GRGEN_MODEL.IClass)_node_c2; } }
			public GRGEN_LGSP.LGSPNode _node_c1;
			public GRGEN_LGSP.LGSPNode _node_c2;
			public enum RetypeClassContainment_NodeNums { @c1, @c2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)RetypeClassContainment_NodeNums.@c1: return _node_c1;
				case (int)RetypeClassContainment_NodeNums.@c2: return _node_c2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Icontains edge_c { get { return (GRGEN_MODEL.Icontains)_edge_c; } }
			public GRGEN_LGSP.LGSPEdge _edge_c;
			public enum RetypeClassContainment_EdgeNums { @c, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)RetypeClassContainment_EdgeNums.@c: return _edge_c;
				default: return null;
				}
			}
			
			public enum RetypeClassContainment_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RetypeClassContainment_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RetypeClassContainment_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RetypeClassContainment_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum RetypeClassContainment_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_RetypeClassContainment.instance.pat_RetypeClassContainment; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class ProgramGraphsOriginal_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public ProgramGraphsOriginal_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[12];
			rules = new GRGEN_LGSP.LGSPRulePattern[9];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[12+9];
			subpatterns[0] = Pattern_Subclasses.Instance;
			rulesAndSubpatterns[0] = Pattern_Subclasses.Instance;
			subpatterns[1] = Pattern_Subclass.Instance;
			rulesAndSubpatterns[1] = Pattern_Subclass.Instance;
			subpatterns[2] = Pattern_Features.Instance;
			rulesAndSubpatterns[2] = Pattern_Features.Instance;
			subpatterns[3] = Pattern_FeaturePattern.Instance;
			rulesAndSubpatterns[3] = Pattern_FeaturePattern.Instance;
			subpatterns[4] = Pattern_Parameters.Instance;
			rulesAndSubpatterns[4] = Pattern_Parameters.Instance;
			subpatterns[5] = Pattern_Parameter.Instance;
			rulesAndSubpatterns[5] = Pattern_Parameter.Instance;
			subpatterns[6] = Pattern_Statements.Instance;
			rulesAndSubpatterns[6] = Pattern_Statements.Instance;
			subpatterns[7] = Pattern_Statement.Instance;
			rulesAndSubpatterns[7] = Pattern_Statement.Instance;
			subpatterns[8] = Pattern_Expressions.Instance;
			rulesAndSubpatterns[8] = Pattern_Expressions.Instance;
			subpatterns[9] = Pattern_ExpressionPattern.Instance;
			rulesAndSubpatterns[9] = Pattern_ExpressionPattern.Instance;
			subpatterns[10] = Pattern_Bodies.Instance;
			rulesAndSubpatterns[10] = Pattern_Bodies.Instance;
			subpatterns[11] = Pattern_Body.Instance;
			rulesAndSubpatterns[11] = Pattern_Body.Instance;
			rules[0] = Rule_createProgramGraphExample.Instance;
			rulesAndSubpatterns[12+0] = Rule_createProgramGraphExample.Instance;
			rules[1] = Rule_createProgramGraphPullUp.Instance;
			rulesAndSubpatterns[12+1] = Rule_createProgramGraphPullUp.Instance;
			rules[2] = Rule_pullUpMethod.Instance;
			rulesAndSubpatterns[12+2] = Rule_pullUpMethod.Instance;
			rules[3] = Rule_matchAll.Instance;
			rulesAndSubpatterns[12+3] = Rule_matchAll.Instance;
			rules[4] = Rule_InsertHelperEdgesForNestedLayout.Instance;
			rulesAndSubpatterns[12+4] = Rule_InsertHelperEdgesForNestedLayout.Instance;
			rules[5] = Rule_LinkMethodBodyToContainedEntity.Instance;
			rulesAndSubpatterns[12+5] = Rule_LinkMethodBodyToContainedEntity.Instance;
			rules[6] = Rule_LinkMethodBodyToContainedExpressionTransitive.Instance;
			rulesAndSubpatterns[12+6] = Rule_LinkMethodBodyToContainedExpressionTransitive.Instance;
			rules[7] = Rule_RemoveMethodBodyContainsBetweenExpressions.Instance;
			rulesAndSubpatterns[12+7] = Rule_RemoveMethodBodyContainsBetweenExpressions.Instance;
			rules[8] = Rule_RetypeClassContainment.Instance;
			rulesAndSubpatterns[12+8] = Rule_RetypeClassContainment.Instance;
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
	}


    public class PatternAction_Subclasses : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Subclasses(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Subclasses.Instance.patternGraph;
        }

        public static PatternAction_Subclasses getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Subclasses newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Subclasses(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Subclasses oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Subclasses freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Subclasses next = null;

        public GRGEN_LGSP.LGSPNode Subclasses_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Subclasses.Match_Subclasses_iter_0 patternpath_match_Subclasses_iter_0 = null;
            Pattern_Subclasses.Match_Subclasses patternpath_match_Subclasses = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Subclasses_node_c 
            GRGEN_LGSP.LGSPNode candidate_Subclasses_node_c = Subclasses_node_c;
            // build match of Subclasses for patternpath checks
            if(patternpath_match_Subclasses==null) patternpath_match_Subclasses = new Pattern_Subclasses.Match_Subclasses();
            patternpath_match_Subclasses._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Subclasses._node_c = candidate_Subclasses_node_c;
            // Push iterated matching task for Subclasses_iter_0
            IteratedAction_Subclasses_iter_0 taskFor_iter_0 = IteratedAction_Subclasses_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.Subclasses_node_c = candidate_Subclasses_node_c;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_Subclasses;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for Subclasses_iter_0
            openTasks.Pop();
            IteratedAction_Subclasses_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Subclasses.Match_Subclasses match = new Pattern_Subclasses.Match_Subclasses();
                    match._node_c = candidate_Subclasses_node_c;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_Subclasses.Match_Subclasses_iter_0, Pattern_Subclasses.IMatch_Subclasses_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_Subclasses.IMatch_Subclasses_iter_0) {
                        Pattern_Subclasses.Match_Subclasses_iter_0 cfpm = (Pattern_Subclasses.Match_Subclasses_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
                    }
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

    public class IteratedAction_Subclasses_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_Subclasses_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Subclasses.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_Subclasses_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_Subclasses_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_Subclasses_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_Subclasses_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_Subclasses_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_Subclasses_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode Subclasses_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_Subclasses.Match_Subclasses_iter_0 patternpath_match_Subclasses_iter_0 = null;
            Pattern_Subclasses.Match_Subclasses patternpath_match_Subclasses = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset Subclasses_node_c 
                GRGEN_LGSP.LGSPNode candidate_Subclasses_node_c = Subclasses_node_c;
                // Extend Outgoing Subclasses_iter_0_edge__edge0 from Subclasses_node_c 
                GRGEN_LGSP.LGSPEdge head_candidate_Subclasses_iter_0_edge__edge0 = candidate_Subclasses_node_c.lgspOuthead;
                if(head_candidate_Subclasses_iter_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Subclasses_iter_0_edge__edge0 = head_candidate_Subclasses_iter_0_edge__edge0;
                    do
                    {
                        if(candidate_Subclasses_iter_0_edge__edge0.lgspType.TypeID!=3 && candidate_Subclasses_iter_0_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Subclasses_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Subclasses_iter_0_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Subclasses_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Subclasses_iter_0_edge__edge0, null))
                        {
                            continue;
                        }
                        // Implicit Target Subclasses_iter_0_node_sub from Subclasses_iter_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Subclasses_iter_0_node_sub = candidate_Subclasses_iter_0_edge__edge0.lgspTarget;
                        if(candidate_Subclasses_iter_0_node_sub.lgspType.TypeID!=5) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Subclasses_iter_0_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Subclasses_iter_0_node_sub)))
                        {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Subclasses_iter_0_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Subclasses_iter_0_node_sub)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Subclasses_iter_0_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Subclasses_iter_0_node_sub, null))
                        {
                            continue;
                        }
                        // build match of Subclasses_iter_0 for patternpath checks
                        if(patternpath_match_Subclasses_iter_0==null) patternpath_match_Subclasses_iter_0 = new Pattern_Subclasses.Match_Subclasses_iter_0();
                        patternpath_match_Subclasses_iter_0._matchOfEnclosingPattern = null;
                        patternpath_match_Subclasses_iter_0._node_c = candidate_Subclasses_node_c;
                        patternpath_match_Subclasses_iter_0._node_sub = candidate_Subclasses_iter_0_node_sub;
                        patternpath_match_Subclasses_iter_0._edge__edge0 = candidate_Subclasses_iter_0_edge__edge0;
                        uint prevSomeGlobal__candidate_Subclasses_iter_0_node_sub;
                        prevSomeGlobal__candidate_Subclasses_iter_0_node_sub = candidate_Subclasses_iter_0_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_Subclasses_iter_0_node_sub.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        uint prevSomeGlobal__candidate_Subclasses_iter_0_edge__edge0;
                        prevSomeGlobal__candidate_Subclasses_iter_0_edge__edge0 = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_Subclasses_iter_0_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push subpattern matching task for _sub0
                        PatternAction_Subclass taskFor__sub0 = PatternAction_Subclass.getNewTask(graph, openTasks);
                        taskFor__sub0.Subclass_node_sub = candidate_Subclasses_iter_0_node_sub;
                        taskFor__sub0.searchPatternpath = false;
                        taskFor__sub0.matchOfNestingPattern = patternpath_match_Subclasses_iter_0;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_Subclasses_iter_0_node_sub;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_Subclasses_iter_0_node_sub = candidate_Subclasses_iter_0_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_Subclasses_iter_0_node_sub.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_Subclasses_iter_0_node_sub = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Subclasses_iter_0_node_sub) ? 1U : 0U;
                            if(prevGlobal__candidate_Subclasses_iter_0_node_sub == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Subclasses_iter_0_node_sub,candidate_Subclasses_iter_0_node_sub);
                        }
                        uint prevGlobal__candidate_Subclasses_iter_0_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_Subclasses_iter_0_edge__edge0 = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_Subclasses_iter_0_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_Subclasses_iter_0_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Subclasses_iter_0_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_Subclasses_iter_0_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Subclasses_iter_0_edge__edge0,candidate_Subclasses_iter_0_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_Subclass.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_Subclasses.Match_Subclasses_iter_0 match = new Pattern_Subclasses.Match_Subclasses_iter_0();
                                match._node_c = candidate_Subclasses_node_c;
                                match._node_sub = candidate_Subclasses_iter_0_node_sub;
                                match._edge__edge0 = candidate_Subclasses_iter_0_edge__edge0;
                                match.__sub0 = (@Pattern_Subclass.Match_Subclass)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Subclasses_iter_0_edge__edge0.lgspFlags = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Subclasses_iter_0_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_Subclasses_iter_0_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Subclasses_iter_0_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Subclasses_iter_0_node_sub.lgspFlags = candidate_Subclasses_iter_0_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Subclasses_iter_0_node_sub;
                                } else { 
                                    if(prevGlobal__candidate_Subclasses_iter_0_node_sub == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Subclasses_iter_0_node_sub);
                                    }
                                }
                                --numMatchesIter;
                                candidate_Subclasses_iter_0_edge__edge0.lgspFlags = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Subclasses_iter_0_edge__edge0;
                                candidate_Subclasses_iter_0_node_sub.lgspFlags = candidate_Subclasses_iter_0_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Subclasses_iter_0_node_sub;
                                goto maxMatchesIterReached;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Subclasses_iter_0_edge__edge0.lgspFlags = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Subclasses_iter_0_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_Subclasses_iter_0_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Subclasses_iter_0_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Subclasses_iter_0_node_sub.lgspFlags = candidate_Subclasses_iter_0_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Subclasses_iter_0_node_sub;
                            } else { 
                                if(prevGlobal__candidate_Subclasses_iter_0_node_sub == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Subclasses_iter_0_node_sub);
                                }
                            }
                            --numMatchesIter;
                            candidate_Subclasses_iter_0_edge__edge0.lgspFlags = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Subclasses_iter_0_edge__edge0;
                            candidate_Subclasses_iter_0_node_sub.lgspFlags = candidate_Subclasses_iter_0_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Subclasses_iter_0_node_sub;
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Subclasses_iter_0_node_sub.lgspFlags = candidate_Subclasses_iter_0_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Subclasses_iter_0_node_sub;
                        } else { 
                            if(prevGlobal__candidate_Subclasses_iter_0_node_sub == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Subclasses_iter_0_node_sub);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Subclasses_iter_0_edge__edge0.lgspFlags = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Subclasses_iter_0_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_Subclasses_iter_0_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Subclasses_iter_0_edge__edge0);
                            }
                        }
                        --numMatchesIter;
                        candidate_Subclasses_iter_0_node_sub.lgspFlags = candidate_Subclasses_iter_0_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Subclasses_iter_0_node_sub;
                        candidate_Subclasses_iter_0_edge__edge0.lgspFlags = candidate_Subclasses_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Subclasses_iter_0_edge__edge0;
                    }
                    while( (candidate_Subclasses_iter_0_edge__edge0 = candidate_Subclasses_iter_0_edge__edge0.lgspOutNext) != head_candidate_Subclasses_iter_0_edge__edge0 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_Subclasses.Match_Subclasses_iter_0 match = new Pattern_Subclasses.Match_Subclasses_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Subclasses.Match_Subclasses_iter_0 match = new Pattern_Subclasses.Match_Subclasses_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_Subclass : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Subclass(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Subclass.Instance.patternGraph;
        }

        public static PatternAction_Subclass getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Subclass newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Subclass(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Subclass oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Subclass freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Subclass next = null;

        public GRGEN_LGSP.LGSPNode Subclass_node_sub;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Subclass.Match_Subclass patternpath_match_Subclass = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Subclass_node_sub 
            GRGEN_LGSP.LGSPNode candidate_Subclass_node_sub = Subclass_node_sub;
            // build match of Subclass for patternpath checks
            if(patternpath_match_Subclass==null) patternpath_match_Subclass = new Pattern_Subclass.Match_Subclass();
            patternpath_match_Subclass._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Subclass._node_sub = candidate_Subclass_node_sub;
            // Push subpattern matching task for _sub1
            PatternAction_Subclasses taskFor__sub1 = PatternAction_Subclasses.getNewTask(graph, openTasks);
            taskFor__sub1.Subclasses_node_c = candidate_Subclass_node_sub;
            taskFor__sub1.searchPatternpath = searchPatternpath;
            taskFor__sub1.matchOfNestingPattern = patternpath_match_Subclass;
            taskFor__sub1.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor__sub1);
            // Push subpattern matching task for _sub0
            PatternAction_Features taskFor__sub0 = PatternAction_Features.getNewTask(graph, openTasks);
            taskFor__sub0.Features_node_c = candidate_Subclass_node_sub;
            taskFor__sub0.searchPatternpath = searchPatternpath;
            taskFor__sub0.matchOfNestingPattern = patternpath_match_Subclass;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor__sub0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_Features.releaseTask(taskFor__sub0);
            // Pop subpattern matching task for _sub1
            openTasks.Pop();
            PatternAction_Subclasses.releaseTask(taskFor__sub1);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Subclass.Match_Subclass match = new Pattern_Subclass.Match_Subclass();
                    match._node_sub = candidate_Subclass_node_sub;
                    match.__sub0 = (@Pattern_Features.Match_Features)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    match.__sub1 = (@Pattern_Subclasses.Match_Subclasses)currentFoundPartialMatch.Pop();
                    match.__sub1._matchOfEnclosingPattern = match;
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

    public class PatternAction_Features : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Features(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Features.Instance.patternGraph;
        }

        public static PatternAction_Features getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Features newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Features(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Features oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Features freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Features next = null;

        public GRGEN_LGSP.LGSPNode Features_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Features.Match_Features_iter_0 patternpath_match_Features_iter_0 = null;
            Pattern_Features.Match_Features patternpath_match_Features = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Features_node_c 
            GRGEN_LGSP.LGSPNode candidate_Features_node_c = Features_node_c;
            // build match of Features for patternpath checks
            if(patternpath_match_Features==null) patternpath_match_Features = new Pattern_Features.Match_Features();
            patternpath_match_Features._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Features._node_c = candidate_Features_node_c;
            // Push iterated matching task for Features_iter_0
            IteratedAction_Features_iter_0 taskFor_iter_0 = IteratedAction_Features_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.Features_node_c = candidate_Features_node_c;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_Features;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for Features_iter_0
            openTasks.Pop();
            IteratedAction_Features_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Features.Match_Features match = new Pattern_Features.Match_Features();
                    match._node_c = candidate_Features_node_c;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_Features.Match_Features_iter_0, Pattern_Features.IMatch_Features_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_Features.IMatch_Features_iter_0) {
                        Pattern_Features.Match_Features_iter_0 cfpm = (Pattern_Features.Match_Features_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
                    }
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

    public class IteratedAction_Features_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_Features_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Features.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_Features_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_Features_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_Features_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_Features_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_Features_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_Features_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode Features_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_Features.Match_Features_iter_0 patternpath_match_Features_iter_0 = null;
            Pattern_Features.Match_Features patternpath_match_Features = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset Features_node_c 
                GRGEN_LGSP.LGSPNode candidate_Features_node_c = Features_node_c;
                // build match of Features_iter_0 for patternpath checks
                if(patternpath_match_Features_iter_0==null) patternpath_match_Features_iter_0 = new Pattern_Features.Match_Features_iter_0();
                patternpath_match_Features_iter_0._matchOfEnclosingPattern = null;
                patternpath_match_Features_iter_0._node_c = candidate_Features_node_c;
                // accept iterated instance match
                ++numMatchesIter;
                // Push subpattern matching task for _sub0
                PatternAction_FeaturePattern taskFor__sub0 = PatternAction_FeaturePattern.getNewTask(graph, openTasks);
                taskFor__sub0.FeaturePattern_node_c = candidate_Features_node_c;
                taskFor__sub0.searchPatternpath = false;
                taskFor__sub0.matchOfNestingPattern = patternpath_match_Features_iter_0;
                taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                openTasks.Push(taskFor__sub0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _sub0
                openTasks.Pop();
                PatternAction_FeaturePattern.releaseTask(taskFor__sub0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    patternFound = true;
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Features.Match_Features_iter_0 match = new Pattern_Features.Match_Features_iter_0();
                        match._node_c = candidate_Features_node_c;
                        match.__sub0 = (@Pattern_FeaturePattern.Match_FeaturePattern)currentFoundPartialMatch.Pop();
                        match.__sub0._matchOfEnclosingPattern = match;
                        currentFoundPartialMatch.Push(match);
                    }
                    // if enough matches were found, we leave
                    if(true) // as soon as there's a match, it's enough for iterated
                    {
                        --numMatchesIter;
                        continue;
                    }
                    --numMatchesIter;
                    continue;
                }
                --numMatchesIter;
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_Features.Match_Features_iter_0 match = new Pattern_Features.Match_Features_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Features.Match_Features_iter_0 match = new Pattern_Features.Match_Features_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_FeaturePattern : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_FeaturePattern(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_FeaturePattern.Instance.patternGraph;
        }

        public static PatternAction_FeaturePattern getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_FeaturePattern newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_FeaturePattern(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_FeaturePattern oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_FeaturePattern freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_FeaturePattern next = null;

        public GRGEN_LGSP.LGSPNode FeaturePattern_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodBody patternpath_match_FeaturePattern_alt_0_MethodBody = null;
            Pattern_FeaturePattern.Match_FeaturePattern patternpath_match_FeaturePattern = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset FeaturePattern_node_c 
            GRGEN_LGSP.LGSPNode candidate_FeaturePattern_node_c = FeaturePattern_node_c;
            // build match of FeaturePattern for patternpath checks
            if(patternpath_match_FeaturePattern==null) patternpath_match_FeaturePattern = new Pattern_FeaturePattern.Match_FeaturePattern();
            patternpath_match_FeaturePattern._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_FeaturePattern._node_c = candidate_FeaturePattern_node_c;
            // Push alternative matching task for FeaturePattern_alt_0
            AlternativeAction_FeaturePattern_alt_0 taskFor_alt_0 = AlternativeAction_FeaturePattern_alt_0.getNewTask(graph, openTasks, Pattern_FeaturePattern.Instance.patternGraph.alternatives[(int)Pattern_FeaturePattern.FeaturePattern_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.FeaturePattern_node_c = candidate_FeaturePattern_node_c;
            taskFor_alt_0.searchPatternpath = searchPatternpath;
            taskFor_alt_0.matchOfNestingPattern = patternpath_match_FeaturePattern;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for FeaturePattern_alt_0
            openTasks.Pop();
            AlternativeAction_FeaturePattern_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_FeaturePattern.Match_FeaturePattern match = new Pattern_FeaturePattern.Match_FeaturePattern();
                    match._node_c = candidate_FeaturePattern_node_c;
                    match._alt_0 = (Pattern_FeaturePattern.IMatch_FeaturePattern_alt_0)currentFoundPartialMatch.Pop();
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

    public class AlternativeAction_FeaturePattern_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_FeaturePattern_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_FeaturePattern_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_FeaturePattern_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_FeaturePattern_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_FeaturePattern_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_FeaturePattern_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_FeaturePattern_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode FeaturePattern_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodBody patternpath_match_FeaturePattern_alt_0_MethodBody = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case FeaturePattern_alt_0_MethodBody 
            do {
                patternGraph = patternGraphs[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_CaseNums.@MethodBody];
                // SubPreset FeaturePattern_node_c 
                GRGEN_LGSP.LGSPNode candidate_FeaturePattern_node_c = FeaturePattern_node_c;
                // Extend Outgoing FeaturePattern_alt_0_MethodBody_edge__edge0 from FeaturePattern_node_c 
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = candidate_FeaturePattern_node_c.lgspOuthead;
                if(head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspType.TypeID!=3 && candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_MethodBody_node_b from FeaturePattern_alt_0_MethodBody_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_MethodBody_node_b = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspTarget;
                        if(candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspType.TypeID!=2) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_FeaturePattern_alt_0_MethodBody_node_b)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_MethodBody_node_b, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // build match of FeaturePattern_alt_0_MethodBody for patternpath checks
                        if(patternpath_match_FeaturePattern_alt_0_MethodBody==null) patternpath_match_FeaturePattern_alt_0_MethodBody = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodBody();
                        patternpath_match_FeaturePattern_alt_0_MethodBody._matchOfEnclosingPattern = matchOfNestingPattern;
                        patternpath_match_FeaturePattern_alt_0_MethodBody._node_c = candidate_FeaturePattern_node_c;
                        patternpath_match_FeaturePattern_alt_0_MethodBody._node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        patternpath_match_FeaturePattern_alt_0_MethodBody._edge__edge0 = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                        uint prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        uint prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                        prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        // Push subpattern matching task for _sub1
                        PatternAction_Statements taskFor__sub1 = PatternAction_Statements.getNewTask(graph, openTasks);
                        taskFor__sub1.Statements_node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        taskFor__sub1.searchPatternpath = searchPatternpath;
                        taskFor__sub1.matchOfNestingPattern = patternpath_match_FeaturePattern_alt_0_MethodBody;
                        taskFor__sub1.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                        openTasks.Push(taskFor__sub1);
                        // Push subpattern matching task for _sub0
                        PatternAction_Parameters taskFor__sub0 = PatternAction_Parameters.getNewTask(graph, openTasks);
                        taskFor__sub0.Parameters_node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        taskFor__sub0.searchPatternpath = searchPatternpath;
                        taskFor__sub0.matchOfNestingPattern = patternpath_match_FeaturePattern_alt_0_MethodBody;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_FeaturePattern_alt_0_MethodBody_node_b) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_FeaturePattern_alt_0_MethodBody_node_b,candidate_FeaturePattern_alt_0_MethodBody_node_b);
                        }
                        uint prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0,candidate_FeaturePattern_alt_0_MethodBody_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_Parameters.releaseTask(taskFor__sub0);
                        // Pop subpattern matching task for _sub1
                        openTasks.Pop();
                        PatternAction_Statements.releaseTask(taskFor__sub1);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodBody match = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodBody();
                                match._node_c = candidate_FeaturePattern_node_c;
                                match._node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b;
                                match._edge__edge0 = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                                match.__sub0 = (@Pattern_Parameters.Match_Parameters)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
                                match.__sub1 = (@Pattern_Statements.Match_Statements)currentFoundPartialMatch.Pop();
                                match.__sub1._matchOfEnclosingPattern = match;
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_MethodBody_node_b);
                                    }
                                }
                                candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                                candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_MethodBody_node_b);
                                }
                            }
                            candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                            candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_MethodBody_node_b);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0);
                            }
                        }
                        candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                    }
                    while( (candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.lgspOutNext) != head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 );
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
            // Alternative case FeaturePattern_alt_0_MethodSignature 
            do {
                patternGraph = patternGraphs[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_CaseNums.@MethodSignature];
                // SubPreset FeaturePattern_node_c 
                GRGEN_LGSP.LGSPNode candidate_FeaturePattern_node_c = FeaturePattern_node_c;
                // Extend Outgoing FeaturePattern_alt_0_MethodSignature_edge__edge0 from FeaturePattern_node_c 
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = candidate_FeaturePattern_node_c.lgspOuthead;
                if(head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspType.TypeID!=3 && candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_MethodSignature_node__node0 from FeaturePattern_alt_0_MethodSignature_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_MethodSignature_node__node0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspTarget;
                        if(candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspType.TypeID!=7) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_FeaturePattern_alt_0_MethodSignature_node__node0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_MethodSignature_node__node0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodSignature match = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodSignature();
                            match._node_c = candidate_FeaturePattern_node_c;
                            match._node__node0 = candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                            match._edge__edge0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0 = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_FeaturePattern_alt_0_MethodSignature_node__node0) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_FeaturePattern_alt_0_MethodSignature_node__node0,candidate_FeaturePattern_alt_0_MethodSignature_node__node0);
                        }
                        uint prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0,candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodSignature match = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_MethodSignature();
                                match._node_c = candidate_FeaturePattern_node_c;
                                match._node__node0 = candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                                match._edge__edge0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_MethodSignature_node__node0);
                                    }
                                }
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_MethodSignature_node__node0);
                                }
                            }
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_MethodSignature_node__node0);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.lgspOutNext) != head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 );
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
            // Alternative case FeaturePattern_alt_0_Variable 
            do {
                patternGraph = patternGraphs[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_CaseNums.@Variable];
                // SubPreset FeaturePattern_node_c 
                GRGEN_LGSP.LGSPNode candidate_FeaturePattern_node_c = FeaturePattern_node_c;
                // Extend Outgoing FeaturePattern_alt_0_Variable_edge__edge0 from FeaturePattern_node_c 
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_Variable_edge__edge0 = candidate_FeaturePattern_node_c.lgspOuthead;
                if(head_candidate_FeaturePattern_alt_0_Variable_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_Variable_edge__edge0 = head_candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspType.TypeID!=3 && candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_FeaturePattern_alt_0_Variable_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_Variable_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_Variable_node__node0 from FeaturePattern_alt_0_Variable_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_Variable_node__node0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspTarget;
                        if(candidate_FeaturePattern_alt_0_Variable_node__node0.lgspType.TypeID!=10) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_FeaturePattern_alt_0_Variable_node__node0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_Variable_node__node0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Variable match = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Variable();
                            match._node_c = candidate_FeaturePattern_node_c;
                            match._node__node0 = candidate_FeaturePattern_alt_0_Variable_node__node0;
                            match._edge__edge0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0 = candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_FeaturePattern_alt_0_Variable_node__node0) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_FeaturePattern_alt_0_Variable_node__node0,candidate_FeaturePattern_alt_0_Variable_node__node0);
                        }
                        uint prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_FeaturePattern_alt_0_Variable_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_FeaturePattern_alt_0_Variable_edge__edge0,candidate_FeaturePattern_alt_0_Variable_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Variable match = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Variable();
                                match._node_c = candidate_FeaturePattern_node_c;
                                match._node__node0 = candidate_FeaturePattern_alt_0_Variable_node__node0;
                                match._edge__edge0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0;
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_Variable_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_Variable_node__node0);
                                    }
                                }
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_Variable_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_Variable_node__node0);
                                }
                            }
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_Variable_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_Variable_node__node0);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_Variable_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_FeaturePattern_alt_0_Variable_edge__edge0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0.lgspOutNext) != head_candidate_FeaturePattern_alt_0_Variable_edge__edge0 );
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
            // Alternative case FeaturePattern_alt_0_Konstante 
            do {
                patternGraph = patternGraphs[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_CaseNums.@Konstante];
                // SubPreset FeaturePattern_node_c 
                GRGEN_LGSP.LGSPNode candidate_FeaturePattern_node_c = FeaturePattern_node_c;
                // Extend Outgoing FeaturePattern_alt_0_Konstante_edge__edge0 from FeaturePattern_node_c 
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = candidate_FeaturePattern_node_c.lgspOuthead;
                if(head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspType.TypeID!=3 && candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_FeaturePattern_alt_0_Konstante_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_Konstante_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_Konstante_node__node0 from FeaturePattern_alt_0_Konstante_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_Konstante_node__node0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspTarget;
                        if(candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspType.TypeID!=9) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_FeaturePattern_alt_0_Konstante_node__node0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_FeaturePattern_alt_0_Konstante_node__node0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Konstante match = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Konstante();
                            match._node_c = candidate_FeaturePattern_node_c;
                            match._node__node0 = candidate_FeaturePattern_alt_0_Konstante_node__node0;
                            match._edge__edge0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0 = candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_FeaturePattern_alt_0_Konstante_node__node0) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_FeaturePattern_alt_0_Konstante_node__node0,candidate_FeaturePattern_alt_0_Konstante_node__node0);
                        }
                        uint prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_FeaturePattern_alt_0_Konstante_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_FeaturePattern_alt_0_Konstante_edge__edge0,candidate_FeaturePattern_alt_0_Konstante_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Konstante match = new Pattern_FeaturePattern.Match_FeaturePattern_alt_0_Konstante();
                                match._node_c = candidate_FeaturePattern_node_c;
                                match._node__node0 = candidate_FeaturePattern_alt_0_Konstante_node__node0;
                                match._edge__edge0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_Konstante_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0;
                                } else { 
                                    if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_Konstante_node__node0);
                                    }
                                }
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_Konstante_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0;
                            } else { 
                                if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_Konstante_node__node0);
                                }
                            }
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags = candidate_FeaturePattern_alt_0_Konstante_node__node0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_FeaturePattern_alt_0_Konstante_node__node0);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_FeaturePattern_alt_0_Konstante_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.lgspOutNext) != head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_Parameters : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Parameters(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Parameters.Instance.patternGraph;
        }

        public static PatternAction_Parameters getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Parameters newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Parameters(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Parameters oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Parameters freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Parameters next = null;

        public GRGEN_LGSP.LGSPNode Parameters_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Parameters.Match_Parameters_iter_0 patternpath_match_Parameters_iter_0 = null;
            Pattern_Parameters.Match_Parameters patternpath_match_Parameters = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Parameters_node_b 
            GRGEN_LGSP.LGSPNode candidate_Parameters_node_b = Parameters_node_b;
            // build match of Parameters for patternpath checks
            if(patternpath_match_Parameters==null) patternpath_match_Parameters = new Pattern_Parameters.Match_Parameters();
            patternpath_match_Parameters._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Parameters._node_b = candidate_Parameters_node_b;
            // Push iterated matching task for Parameters_iter_0
            IteratedAction_Parameters_iter_0 taskFor_iter_0 = IteratedAction_Parameters_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.Parameters_node_b = candidate_Parameters_node_b;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_Parameters;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for Parameters_iter_0
            openTasks.Pop();
            IteratedAction_Parameters_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Parameters.Match_Parameters match = new Pattern_Parameters.Match_Parameters();
                    match._node_b = candidate_Parameters_node_b;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_Parameters.Match_Parameters_iter_0, Pattern_Parameters.IMatch_Parameters_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_Parameters.IMatch_Parameters_iter_0) {
                        Pattern_Parameters.Match_Parameters_iter_0 cfpm = (Pattern_Parameters.Match_Parameters_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
                    }
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

    public class IteratedAction_Parameters_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_Parameters_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Parameters.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_Parameters_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_Parameters_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_Parameters_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_Parameters_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_Parameters_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_Parameters_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode Parameters_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_Parameters.Match_Parameters_iter_0 patternpath_match_Parameters_iter_0 = null;
            Pattern_Parameters.Match_Parameters patternpath_match_Parameters = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset Parameters_node_b 
                GRGEN_LGSP.LGSPNode candidate_Parameters_node_b = Parameters_node_b;
                // build match of Parameters_iter_0 for patternpath checks
                if(patternpath_match_Parameters_iter_0==null) patternpath_match_Parameters_iter_0 = new Pattern_Parameters.Match_Parameters_iter_0();
                patternpath_match_Parameters_iter_0._matchOfEnclosingPattern = null;
                patternpath_match_Parameters_iter_0._node_b = candidate_Parameters_node_b;
                // accept iterated instance match
                ++numMatchesIter;
                // Push subpattern matching task for _sub0
                PatternAction_Parameter taskFor__sub0 = PatternAction_Parameter.getNewTask(graph, openTasks);
                taskFor__sub0.Parameter_node_b = candidate_Parameters_node_b;
                taskFor__sub0.searchPatternpath = false;
                taskFor__sub0.matchOfNestingPattern = patternpath_match_Parameters_iter_0;
                taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                openTasks.Push(taskFor__sub0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _sub0
                openTasks.Pop();
                PatternAction_Parameter.releaseTask(taskFor__sub0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    patternFound = true;
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Parameters.Match_Parameters_iter_0 match = new Pattern_Parameters.Match_Parameters_iter_0();
                        match._node_b = candidate_Parameters_node_b;
                        match.__sub0 = (@Pattern_Parameter.Match_Parameter)currentFoundPartialMatch.Pop();
                        match.__sub0._matchOfEnclosingPattern = match;
                        currentFoundPartialMatch.Push(match);
                    }
                    // if enough matches were found, we leave
                    if(true) // as soon as there's a match, it's enough for iterated
                    {
                        --numMatchesIter;
                        continue;
                    }
                    --numMatchesIter;
                    continue;
                }
                --numMatchesIter;
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_Parameters.Match_Parameters_iter_0 match = new Pattern_Parameters.Match_Parameters_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Parameters.Match_Parameters_iter_0 match = new Pattern_Parameters.Match_Parameters_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_Parameter : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Parameter(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Parameter.Instance.patternGraph;
        }

        public static PatternAction_Parameter getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Parameter newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Parameter(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Parameter oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Parameter freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Parameter next = null;

        public GRGEN_LGSP.LGSPNode Parameter_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Parameter.Match_Parameter patternpath_match_Parameter = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Parameter_node_b 
            GRGEN_LGSP.LGSPNode candidate_Parameter_node_b = Parameter_node_b;
            // build match of Parameter for patternpath checks
            if(patternpath_match_Parameter==null) patternpath_match_Parameter = new Pattern_Parameter.Match_Parameter();
            patternpath_match_Parameter._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Parameter._node_b = candidate_Parameter_node_b;
            // Push alternative matching task for Parameter_alt_0
            AlternativeAction_Parameter_alt_0 taskFor_alt_0 = AlternativeAction_Parameter_alt_0.getNewTask(graph, openTasks, Pattern_Parameter.Instance.patternGraph.alternatives[(int)Pattern_Parameter.Parameter_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.Parameter_node_b = candidate_Parameter_node_b;
            taskFor_alt_0.searchPatternpath = searchPatternpath;
            taskFor_alt_0.matchOfNestingPattern = patternpath_match_Parameter;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for Parameter_alt_0
            openTasks.Pop();
            AlternativeAction_Parameter_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Parameter.Match_Parameter match = new Pattern_Parameter.Match_Parameter();
                    match._node_b = candidate_Parameter_node_b;
                    match._alt_0 = (Pattern_Parameter.IMatch_Parameter_alt_0)currentFoundPartialMatch.Pop();
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

    public class AlternativeAction_Parameter_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_Parameter_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_Parameter_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_Parameter_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_Parameter_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_Parameter_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_Parameter_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_Parameter_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode Parameter_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Parameter_alt_0_Variable 
            do {
                patternGraph = patternGraphs[(int)Pattern_Parameter.Parameter_alt_0_CaseNums.@Variable];
                // SubPreset Parameter_node_b 
                GRGEN_LGSP.LGSPNode candidate_Parameter_node_b = Parameter_node_b;
                // Extend Outgoing Parameter_alt_0_Variable_edge__edge0 from Parameter_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Parameter_alt_0_Variable_edge__edge0 = candidate_Parameter_node_b.lgspOuthead;
                if(head_candidate_Parameter_alt_0_Variable_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Parameter_alt_0_Variable_edge__edge0 = head_candidate_Parameter_alt_0_Variable_edge__edge0;
                    do
                    {
                        if(candidate_Parameter_alt_0_Variable_edge__edge0.lgspType.TypeID!=3 && candidate_Parameter_alt_0_Variable_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Parameter_alt_0_Variable_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Parameter_alt_0_Variable_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target Parameter_alt_0_Variable_node_v from Parameter_alt_0_Variable_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Parameter_alt_0_Variable_node_v = candidate_Parameter_alt_0_Variable_edge__edge0.lgspTarget;
                        if(candidate_Parameter_alt_0_Variable_node_v.lgspType.TypeID!=10) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Parameter_alt_0_Variable_node_v.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Parameter_alt_0_Variable_node_v)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Parameter_alt_0_Variable_node_v.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Parameter_alt_0_Variable_node_v, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_Parameter.Match_Parameter_alt_0_Variable match = new Pattern_Parameter.Match_Parameter_alt_0_Variable();
                            match._node_b = candidate_Parameter_node_b;
                            match._node_v = candidate_Parameter_alt_0_Variable_node_v;
                            match._edge__edge0 = candidate_Parameter_alt_0_Variable_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_Parameter_alt_0_Variable_node_v;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_Parameter_alt_0_Variable_node_v = candidate_Parameter_alt_0_Variable_node_v.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_Parameter_alt_0_Variable_node_v.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_Parameter_alt_0_Variable_node_v = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Parameter_alt_0_Variable_node_v) ? 1U : 0U;
                            if(prevGlobal__candidate_Parameter_alt_0_Variable_node_v == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Parameter_alt_0_Variable_node_v,candidate_Parameter_alt_0_Variable_node_v);
                        }
                        uint prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0 = candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Parameter_alt_0_Variable_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Parameter_alt_0_Variable_edge__edge0,candidate_Parameter_alt_0_Variable_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_Parameter.Match_Parameter_alt_0_Variable match = new Pattern_Parameter.Match_Parameter_alt_0_Variable();
                                match._node_b = candidate_Parameter_node_b;
                                match._node_v = candidate_Parameter_alt_0_Variable_node_v;
                                match._edge__edge0 = candidate_Parameter_alt_0_Variable_edge__edge0;
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags = candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Parameter_alt_0_Variable_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Parameter_alt_0_Variable_node_v.lgspFlags = candidate_Parameter_alt_0_Variable_node_v.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Variable_node_v;
                                } else { 
                                    if(prevGlobal__candidate_Parameter_alt_0_Variable_node_v == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Parameter_alt_0_Variable_node_v);
                                    }
                                }
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags = candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Parameter_alt_0_Variable_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Parameter_alt_0_Variable_node_v.lgspFlags = candidate_Parameter_alt_0_Variable_node_v.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Variable_node_v;
                            } else { 
                                if(prevGlobal__candidate_Parameter_alt_0_Variable_node_v == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Parameter_alt_0_Variable_node_v);
                                }
                            }
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Parameter_alt_0_Variable_node_v.lgspFlags = candidate_Parameter_alt_0_Variable_node_v.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Variable_node_v;
                        } else { 
                            if(prevGlobal__candidate_Parameter_alt_0_Variable_node_v == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Parameter_alt_0_Variable_node_v);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags = candidate_Parameter_alt_0_Variable_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Parameter_alt_0_Variable_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_Parameter_alt_0_Variable_edge__edge0 = candidate_Parameter_alt_0_Variable_edge__edge0.lgspOutNext) != head_candidate_Parameter_alt_0_Variable_edge__edge0 );
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
            // Alternative case Parameter_alt_0_Konstante 
            do {
                patternGraph = patternGraphs[(int)Pattern_Parameter.Parameter_alt_0_CaseNums.@Konstante];
                // SubPreset Parameter_node_b 
                GRGEN_LGSP.LGSPNode candidate_Parameter_node_b = Parameter_node_b;
                // Extend Outgoing Parameter_alt_0_Konstante_edge__edge0 from Parameter_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Parameter_alt_0_Konstante_edge__edge0 = candidate_Parameter_node_b.lgspOuthead;
                if(head_candidate_Parameter_alt_0_Konstante_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Parameter_alt_0_Konstante_edge__edge0 = head_candidate_Parameter_alt_0_Konstante_edge__edge0;
                    do
                    {
                        if(candidate_Parameter_alt_0_Konstante_edge__edge0.lgspType.TypeID!=3 && candidate_Parameter_alt_0_Konstante_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Parameter_alt_0_Konstante_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Parameter_alt_0_Konstante_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target Parameter_alt_0_Konstante_node_c from Parameter_alt_0_Konstante_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Parameter_alt_0_Konstante_node_c = candidate_Parameter_alt_0_Konstante_edge__edge0.lgspTarget;
                        if(candidate_Parameter_alt_0_Konstante_node_c.lgspType.TypeID!=9) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Parameter_alt_0_Konstante_node_c.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Parameter_alt_0_Konstante_node_c)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Parameter_alt_0_Konstante_node_c.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Parameter_alt_0_Konstante_node_c, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_Parameter.Match_Parameter_alt_0_Konstante match = new Pattern_Parameter.Match_Parameter_alt_0_Konstante();
                            match._node_b = candidate_Parameter_node_b;
                            match._node_c = candidate_Parameter_alt_0_Konstante_node_c;
                            match._edge__edge0 = candidate_Parameter_alt_0_Konstante_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_Parameter_alt_0_Konstante_node_c;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_Parameter_alt_0_Konstante_node_c = candidate_Parameter_alt_0_Konstante_node_c.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_Parameter_alt_0_Konstante_node_c.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_Parameter_alt_0_Konstante_node_c = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Parameter_alt_0_Konstante_node_c) ? 1U : 0U;
                            if(prevGlobal__candidate_Parameter_alt_0_Konstante_node_c == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Parameter_alt_0_Konstante_node_c,candidate_Parameter_alt_0_Konstante_node_c);
                        }
                        uint prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0 = candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Parameter_alt_0_Konstante_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Parameter_alt_0_Konstante_edge__edge0,candidate_Parameter_alt_0_Konstante_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_Parameter.Match_Parameter_alt_0_Konstante match = new Pattern_Parameter.Match_Parameter_alt_0_Konstante();
                                match._node_b = candidate_Parameter_node_b;
                                match._node_c = candidate_Parameter_alt_0_Konstante_node_c;
                                match._edge__edge0 = candidate_Parameter_alt_0_Konstante_edge__edge0;
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags = candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Parameter_alt_0_Konstante_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Parameter_alt_0_Konstante_node_c.lgspFlags = candidate_Parameter_alt_0_Konstante_node_c.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Konstante_node_c;
                                } else { 
                                    if(prevGlobal__candidate_Parameter_alt_0_Konstante_node_c == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Parameter_alt_0_Konstante_node_c);
                                    }
                                }
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags = candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Parameter_alt_0_Konstante_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_Parameter_alt_0_Konstante_node_c.lgspFlags = candidate_Parameter_alt_0_Konstante_node_c.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Konstante_node_c;
                            } else { 
                                if(prevGlobal__candidate_Parameter_alt_0_Konstante_node_c == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Parameter_alt_0_Konstante_node_c);
                                }
                            }
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Parameter_alt_0_Konstante_node_c.lgspFlags = candidate_Parameter_alt_0_Konstante_node_c.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Konstante_node_c;
                        } else { 
                            if(prevGlobal__candidate_Parameter_alt_0_Konstante_node_c == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Parameter_alt_0_Konstante_node_c);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags = candidate_Parameter_alt_0_Konstante_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Parameter_alt_0_Konstante_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_Parameter_alt_0_Konstante_edge__edge0 = candidate_Parameter_alt_0_Konstante_edge__edge0.lgspOutNext) != head_candidate_Parameter_alt_0_Konstante_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_Statements : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Statements(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Statements.Instance.patternGraph;
        }

        public static PatternAction_Statements getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Statements newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Statements(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Statements oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Statements freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Statements next = null;

        public GRGEN_LGSP.LGSPNode Statements_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Statements.Match_Statements_iter_0 patternpath_match_Statements_iter_0 = null;
            Pattern_Statements.Match_Statements patternpath_match_Statements = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Statements_node_b 
            GRGEN_LGSP.LGSPNode candidate_Statements_node_b = Statements_node_b;
            // build match of Statements for patternpath checks
            if(patternpath_match_Statements==null) patternpath_match_Statements = new Pattern_Statements.Match_Statements();
            patternpath_match_Statements._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Statements._node_b = candidate_Statements_node_b;
            // Push iterated matching task for Statements_iter_0
            IteratedAction_Statements_iter_0 taskFor_iter_0 = IteratedAction_Statements_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.Statements_node_b = candidate_Statements_node_b;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_Statements;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for Statements_iter_0
            openTasks.Pop();
            IteratedAction_Statements_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Statements.Match_Statements match = new Pattern_Statements.Match_Statements();
                    match._node_b = candidate_Statements_node_b;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_Statements.Match_Statements_iter_0, Pattern_Statements.IMatch_Statements_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_Statements.IMatch_Statements_iter_0) {
                        Pattern_Statements.Match_Statements_iter_0 cfpm = (Pattern_Statements.Match_Statements_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
                    }
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

    public class IteratedAction_Statements_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_Statements_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Statements.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_Statements_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_Statements_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_Statements_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_Statements_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_Statements_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_Statements_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode Statements_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_Statements.Match_Statements_iter_0 patternpath_match_Statements_iter_0 = null;
            Pattern_Statements.Match_Statements patternpath_match_Statements = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset Statements_node_b 
                GRGEN_LGSP.LGSPNode candidate_Statements_node_b = Statements_node_b;
                // build match of Statements_iter_0 for patternpath checks
                if(patternpath_match_Statements_iter_0==null) patternpath_match_Statements_iter_0 = new Pattern_Statements.Match_Statements_iter_0();
                patternpath_match_Statements_iter_0._matchOfEnclosingPattern = null;
                patternpath_match_Statements_iter_0._node_b = candidate_Statements_node_b;
                // accept iterated instance match
                ++numMatchesIter;
                // Push subpattern matching task for _sub0
                PatternAction_Statement taskFor__sub0 = PatternAction_Statement.getNewTask(graph, openTasks);
                taskFor__sub0.Statement_node_b = candidate_Statements_node_b;
                taskFor__sub0.searchPatternpath = false;
                taskFor__sub0.matchOfNestingPattern = patternpath_match_Statements_iter_0;
                taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                openTasks.Push(taskFor__sub0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _sub0
                openTasks.Pop();
                PatternAction_Statement.releaseTask(taskFor__sub0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    patternFound = true;
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Statements.Match_Statements_iter_0 match = new Pattern_Statements.Match_Statements_iter_0();
                        match._node_b = candidate_Statements_node_b;
                        match.__sub0 = (@Pattern_Statement.Match_Statement)currentFoundPartialMatch.Pop();
                        match.__sub0._matchOfEnclosingPattern = match;
                        currentFoundPartialMatch.Push(match);
                    }
                    // if enough matches were found, we leave
                    if(true) // as soon as there's a match, it's enough for iterated
                    {
                        --numMatchesIter;
                        continue;
                    }
                    --numMatchesIter;
                    continue;
                }
                --numMatchesIter;
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_Statements.Match_Statements_iter_0 match = new Pattern_Statements.Match_Statements_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Statements.Match_Statements_iter_0 match = new Pattern_Statements.Match_Statements_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_Statement : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Statement(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Statement.Instance.patternGraph;
        }

        public static PatternAction_Statement getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Statement newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Statement(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Statement oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Statement freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Statement next = null;

        public GRGEN_LGSP.LGSPNode Statement_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Statement.Match_Statement_alt_0_Assignment patternpath_match_Statement_alt_0_Assignment = null;
            Pattern_Statement.Match_Statement_alt_0_Call patternpath_match_Statement_alt_0_Call = null;
            Pattern_Statement.Match_Statement patternpath_match_Statement = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Statement_node_b 
            GRGEN_LGSP.LGSPNode candidate_Statement_node_b = Statement_node_b;
            // build match of Statement for patternpath checks
            if(patternpath_match_Statement==null) patternpath_match_Statement = new Pattern_Statement.Match_Statement();
            patternpath_match_Statement._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Statement._node_b = candidate_Statement_node_b;
            // Push alternative matching task for Statement_alt_0
            AlternativeAction_Statement_alt_0 taskFor_alt_0 = AlternativeAction_Statement_alt_0.getNewTask(graph, openTasks, Pattern_Statement.Instance.patternGraph.alternatives[(int)Pattern_Statement.Statement_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.Statement_node_b = candidate_Statement_node_b;
            taskFor_alt_0.searchPatternpath = searchPatternpath;
            taskFor_alt_0.matchOfNestingPattern = patternpath_match_Statement;
            taskFor_alt_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop alternative matching task for Statement_alt_0
            openTasks.Pop();
            AlternativeAction_Statement_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Statement.Match_Statement match = new Pattern_Statement.Match_Statement();
                    match._node_b = candidate_Statement_node_b;
                    match._alt_0 = (Pattern_Statement.IMatch_Statement_alt_0)currentFoundPartialMatch.Pop();
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

    public class AlternativeAction_Statement_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_Statement_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_Statement_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_Statement_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_Statement_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_Statement_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_Statement_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_Statement_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode Statement_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Statement.Match_Statement_alt_0_Assignment patternpath_match_Statement_alt_0_Assignment = null;
            Pattern_Statement.Match_Statement_alt_0_Call patternpath_match_Statement_alt_0_Call = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Statement_alt_0_Assignment 
            do {
                patternGraph = patternGraphs[(int)Pattern_Statement.Statement_alt_0_CaseNums.@Assignment];
                // SubPreset Statement_node_b 
                GRGEN_LGSP.LGSPNode candidate_Statement_node_b = Statement_node_b;
                // Extend Outgoing Statement_alt_0_Assignment_edge__edge0 from Statement_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Assignment_edge__edge0 = candidate_Statement_node_b.lgspOuthead;
                if(head_candidate_Statement_alt_0_Assignment_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Assignment_edge__edge0 = head_candidate_Statement_alt_0_Assignment_edge__edge0;
                    do
                    {
                        if(candidate_Statement_alt_0_Assignment_edge__edge0.lgspType.TypeID!=3 && candidate_Statement_alt_0_Assignment_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Statement_alt_0_Assignment_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Assignment_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target Statement_alt_0_Assignment_node_e from Statement_alt_0_Assignment_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Statement_alt_0_Assignment_node_e = candidate_Statement_alt_0_Assignment_edge__edge0.lgspTarget;
                        if(candidate_Statement_alt_0_Assignment_node_e.lgspType.TypeID!=3) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Assignment_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Statement_alt_0_Assignment_node_e)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Statement_alt_0_Assignment_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Assignment_node_e, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Extend Outgoing Statement_alt_0_Assignment_edge__edge1 from Statement_alt_0_Assignment_node_e 
                        GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Assignment_edge__edge1 = candidate_Statement_alt_0_Assignment_node_e.lgspOuthead;
                        if(head_candidate_Statement_alt_0_Assignment_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Assignment_edge__edge1 = head_candidate_Statement_alt_0_Assignment_edge__edge1;
                            do
                            {
                                if(candidate_Statement_alt_0_Assignment_edge__edge1.lgspType.TypeID!=8) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Statement_alt_0_Assignment_edge__edge1)))
                                {
                                    continue;
                                }
                                if(searchPatternpath && (candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Assignment_edge__edge1, lastMatchAtPreviousNestingLevel))
                                {
                                    continue;
                                }
                                // build match of Statement_alt_0_Assignment for patternpath checks
                                if(patternpath_match_Statement_alt_0_Assignment==null) patternpath_match_Statement_alt_0_Assignment = new Pattern_Statement.Match_Statement_alt_0_Assignment();
                                patternpath_match_Statement_alt_0_Assignment._matchOfEnclosingPattern = matchOfNestingPattern;
                                patternpath_match_Statement_alt_0_Assignment._node_b = candidate_Statement_node_b;
                                patternpath_match_Statement_alt_0_Assignment._node_e = candidate_Statement_alt_0_Assignment_node_e;
                                patternpath_match_Statement_alt_0_Assignment._edge__edge0 = candidate_Statement_alt_0_Assignment_edge__edge0;
                                patternpath_match_Statement_alt_0_Assignment._edge__edge1 = candidate_Statement_alt_0_Assignment_edge__edge1;
                                uint prevSomeGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                prevSomeGlobal__candidate_Statement_alt_0_Assignment_node_e = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Assignment_node_e.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                uint prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                uint prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                // Push subpattern matching task for _sub0
                                PatternAction_ExpressionPattern taskFor__sub0 = PatternAction_ExpressionPattern.getNewTask(graph, openTasks);
                                taskFor__sub0.ExpressionPattern_node_e = candidate_Statement_alt_0_Assignment_node_e;
                                taskFor__sub0.searchPatternpath = searchPatternpath;
                                taskFor__sub0.matchOfNestingPattern = patternpath_match_Statement_alt_0_Assignment;
                                taskFor__sub0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                                openTasks.Push(taskFor__sub0);
                                uint prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Assignment_node_e = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Assignment_node_e.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Assignment_node_e = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Statement_alt_0_Assignment_node_e) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Assignment_node_e == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Statement_alt_0_Assignment_node_e,candidate_Statement_alt_0_Assignment_node_e);
                                }
                                uint prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Statement_alt_0_Assignment_edge__edge0) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Statement_alt_0_Assignment_edge__edge0,candidate_Statement_alt_0_Assignment_edge__edge0);
                                }
                                uint prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Statement_alt_0_Assignment_edge__edge1) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Statement_alt_0_Assignment_edge__edge1,candidate_Statement_alt_0_Assignment_edge__edge1);
                                }
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Pop subpattern matching task for _sub0
                                openTasks.Pop();
                                PatternAction_ExpressionPattern.releaseTask(taskFor__sub0);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        Pattern_Statement.Match_Statement_alt_0_Assignment match = new Pattern_Statement.Match_Statement_alt_0_Assignment();
                                        match._node_b = candidate_Statement_node_b;
                                        match._node_e = candidate_Statement_alt_0_Assignment_node_e;
                                        match._edge__edge0 = candidate_Statement_alt_0_Assignment_edge__edge0;
                                        match._edge__edge1 = candidate_Statement_alt_0_Assignment_edge__edge1;
                                        match.__sub0 = (@Pattern_ExpressionPattern.Match_ExpressionPattern)currentFoundPartialMatch.Pop();
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
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Assignment_edge__edge1);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Assignment_edge__edge0);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Assignment_node_e.lgspFlags = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Assignment_node_e == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Assignment_node_e);
                                            }
                                        }
                                        candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                        candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                        candidate_Statement_alt_0_Assignment_node_e.lgspFlags = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Assignment_edge__edge1);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Assignment_edge__edge0);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Assignment_node_e.lgspFlags = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Assignment_node_e == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Assignment_node_e);
                                        }
                                    }
                                    candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                    candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                    candidate_Statement_alt_0_Assignment_node_e.lgspFlags = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                    continue;
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Assignment_node_e.lgspFlags = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Assignment_node_e == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Assignment_node_e);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Assignment_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Assignment_edge__edge1);
                                    }
                                }
                                candidate_Statement_alt_0_Assignment_node_e.lgspFlags = candidate_Statement_alt_0_Assignment_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags = candidate_Statement_alt_0_Assignment_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                            }
                            while( (candidate_Statement_alt_0_Assignment_edge__edge1 = candidate_Statement_alt_0_Assignment_edge__edge1.lgspOutNext) != head_candidate_Statement_alt_0_Assignment_edge__edge1 );
                        }
                    }
                    while( (candidate_Statement_alt_0_Assignment_edge__edge0 = candidate_Statement_alt_0_Assignment_edge__edge0.lgspOutNext) != head_candidate_Statement_alt_0_Assignment_edge__edge0 );
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
            // Alternative case Statement_alt_0_Call 
            do {
                patternGraph = patternGraphs[(int)Pattern_Statement.Statement_alt_0_CaseNums.@Call];
                // SubPreset Statement_node_b 
                GRGEN_LGSP.LGSPNode candidate_Statement_node_b = Statement_node_b;
                // Extend Outgoing Statement_alt_0_Call_edge__edge0 from Statement_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Call_edge__edge0 = candidate_Statement_node_b.lgspOuthead;
                if(head_candidate_Statement_alt_0_Call_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Call_edge__edge0 = head_candidate_Statement_alt_0_Call_edge__edge0;
                    do
                    {
                        if(candidate_Statement_alt_0_Call_edge__edge0.lgspType.TypeID!=3 && candidate_Statement_alt_0_Call_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Statement_alt_0_Call_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Call_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target Statement_alt_0_Call_node_e from Statement_alt_0_Call_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Statement_alt_0_Call_node_e = candidate_Statement_alt_0_Call_edge__edge0.lgspTarget;
                        if(candidate_Statement_alt_0_Call_node_e.lgspType.TypeID!=3) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Call_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Statement_alt_0_Call_node_e)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Statement_alt_0_Call_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Call_node_e, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Extend Outgoing Statement_alt_0_Call_edge__edge1 from Statement_alt_0_Call_node_e 
                        GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Call_edge__edge1 = candidate_Statement_alt_0_Call_node_e.lgspOuthead;
                        if(head_candidate_Statement_alt_0_Call_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Call_edge__edge1 = head_candidate_Statement_alt_0_Call_edge__edge1;
                            do
                            {
                                if(candidate_Statement_alt_0_Call_edge__edge1.lgspType.TypeID!=9) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Statement_alt_0_Call_edge__edge1)))
                                {
                                    continue;
                                }
                                if(searchPatternpath && (candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Call_edge__edge1, lastMatchAtPreviousNestingLevel))
                                {
                                    continue;
                                }
                                // build match of Statement_alt_0_Call for patternpath checks
                                if(patternpath_match_Statement_alt_0_Call==null) patternpath_match_Statement_alt_0_Call = new Pattern_Statement.Match_Statement_alt_0_Call();
                                patternpath_match_Statement_alt_0_Call._matchOfEnclosingPattern = matchOfNestingPattern;
                                patternpath_match_Statement_alt_0_Call._node_b = candidate_Statement_node_b;
                                patternpath_match_Statement_alt_0_Call._node_e = candidate_Statement_alt_0_Call_node_e;
                                patternpath_match_Statement_alt_0_Call._edge__edge0 = candidate_Statement_alt_0_Call_edge__edge0;
                                patternpath_match_Statement_alt_0_Call._edge__edge1 = candidate_Statement_alt_0_Call_edge__edge1;
                                uint prevSomeGlobal__candidate_Statement_alt_0_Call_node_e;
                                prevSomeGlobal__candidate_Statement_alt_0_Call_node_e = candidate_Statement_alt_0_Call_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Call_node_e.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                uint prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge0 = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Call_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                uint prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge1 = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Call_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                // Push subpattern matching task for _sub0
                                PatternAction_Expressions taskFor__sub0 = PatternAction_Expressions.getNewTask(graph, openTasks);
                                taskFor__sub0.Expressions_node_e = candidate_Statement_alt_0_Call_node_e;
                                taskFor__sub0.searchPatternpath = searchPatternpath;
                                taskFor__sub0.matchOfNestingPattern = patternpath_match_Statement_alt_0_Call;
                                taskFor__sub0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                                openTasks.Push(taskFor__sub0);
                                uint prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Call_node_e = candidate_Statement_alt_0_Call_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Call_node_e.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Call_node_e = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Statement_alt_0_Call_node_e) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Call_node_e == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Statement_alt_0_Call_node_e,candidate_Statement_alt_0_Call_node_e);
                                }
                                uint prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Call_edge__edge0 = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Call_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Call_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Statement_alt_0_Call_edge__edge0) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Statement_alt_0_Call_edge__edge0,candidate_Statement_alt_0_Call_edge__edge0);
                                }
                                uint prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Call_edge__edge1 = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Call_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Call_edge__edge1 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Statement_alt_0_Call_edge__edge1) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge1 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Statement_alt_0_Call_edge__edge1,candidate_Statement_alt_0_Call_edge__edge1);
                                }
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Pop subpattern matching task for _sub0
                                openTasks.Pop();
                                PatternAction_Expressions.releaseTask(taskFor__sub0);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        Pattern_Statement.Match_Statement_alt_0_Call match = new Pattern_Statement.Match_Statement_alt_0_Call();
                                        match._node_b = candidate_Statement_node_b;
                                        match._node_e = candidate_Statement_alt_0_Call_node_e;
                                        match._edge__edge0 = candidate_Statement_alt_0_Call_edge__edge0;
                                        match._edge__edge1 = candidate_Statement_alt_0_Call_edge__edge1;
                                        match.__sub0 = (@Pattern_Expressions.Match_Expressions)currentFoundPartialMatch.Pop();
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
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Call_edge__edge1.lgspFlags = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Call_edge__edge1);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Call_edge__edge0.lgspFlags = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Call_edge__edge0);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Call_node_e.lgspFlags = candidate_Statement_alt_0_Call_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Call_node_e == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Call_node_e);
                                            }
                                        }
                                        candidate_Statement_alt_0_Call_edge__edge1.lgspFlags = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                        candidate_Statement_alt_0_Call_edge__edge0.lgspFlags = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                        candidate_Statement_alt_0_Call_node_e.lgspFlags = candidate_Statement_alt_0_Call_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_node_e;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Call_edge__edge1.lgspFlags = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge1 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Call_edge__edge1);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Call_edge__edge0.lgspFlags = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge0 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Call_edge__edge0);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Call_node_e.lgspFlags = candidate_Statement_alt_0_Call_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Call_node_e == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Call_node_e);
                                        }
                                    }
                                    candidate_Statement_alt_0_Call_edge__edge1.lgspFlags = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                    candidate_Statement_alt_0_Call_edge__edge0.lgspFlags = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                    candidate_Statement_alt_0_Call_node_e.lgspFlags = candidate_Statement_alt_0_Call_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_node_e;
                                    continue;
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Call_node_e.lgspFlags = candidate_Statement_alt_0_Call_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Call_node_e == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Call_node_e);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Call_edge__edge0.lgspFlags = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Call_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Call_edge__edge1.lgspFlags = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Call_edge__edge1 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Call_edge__edge1);
                                    }
                                }
                                candidate_Statement_alt_0_Call_node_e.lgspFlags = candidate_Statement_alt_0_Call_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_node_e;
                                candidate_Statement_alt_0_Call_edge__edge0.lgspFlags = candidate_Statement_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                candidate_Statement_alt_0_Call_edge__edge1.lgspFlags = candidate_Statement_alt_0_Call_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                            }
                            while( (candidate_Statement_alt_0_Call_edge__edge1 = candidate_Statement_alt_0_Call_edge__edge1.lgspOutNext) != head_candidate_Statement_alt_0_Call_edge__edge1 );
                        }
                    }
                    while( (candidate_Statement_alt_0_Call_edge__edge0 = candidate_Statement_alt_0_Call_edge__edge0.lgspOutNext) != head_candidate_Statement_alt_0_Call_edge__edge0 );
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
            // Alternative case Statement_alt_0_Return 
            do {
                patternGraph = patternGraphs[(int)Pattern_Statement.Statement_alt_0_CaseNums.@Return];
                // SubPreset Statement_node_b 
                GRGEN_LGSP.LGSPNode candidate_Statement_node_b = Statement_node_b;
                // Extend Outgoing Statement_alt_0_Return_edge__edge0 from Statement_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Return_edge__edge0 = candidate_Statement_node_b.lgspOuthead;
                if(head_candidate_Statement_alt_0_Return_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Return_edge__edge0 = head_candidate_Statement_alt_0_Return_edge__edge0;
                    do
                    {
                        if(candidate_Statement_alt_0_Return_edge__edge0.lgspType.TypeID!=3 && candidate_Statement_alt_0_Return_edge__edge0.lgspType.TypeID!=11) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Return_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Statement_alt_0_Return_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Statement_alt_0_Return_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Return_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Implicit Target Statement_alt_0_Return_node_e from Statement_alt_0_Return_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Statement_alt_0_Return_node_e = candidate_Statement_alt_0_Return_edge__edge0.lgspTarget;
                        if(candidate_Statement_alt_0_Return_node_e.lgspType.TypeID!=3) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Return_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Statement_alt_0_Return_node_e)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_Statement_alt_0_Return_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Return_node_e, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Extend Outgoing Statement_alt_0_Return_edge__edge1 from Statement_alt_0_Return_node_e 
                        GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Return_edge__edge1 = candidate_Statement_alt_0_Return_node_e.lgspOuthead;
                        if(head_candidate_Statement_alt_0_Return_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Return_edge__edge1 = head_candidate_Statement_alt_0_Return_edge__edge1;
                            do
                            {
                                if(candidate_Statement_alt_0_Return_edge__edge1.lgspType.TypeID!=7) {
                                    continue;
                                }
                                if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Statement_alt_0_Return_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Statement_alt_0_Return_edge__edge1)))
                                {
                                    continue;
                                }
                                if(searchPatternpath && (candidate_Statement_alt_0_Return_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Statement_alt_0_Return_edge__edge1, lastMatchAtPreviousNestingLevel))
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    Pattern_Statement.Match_Statement_alt_0_Return match = new Pattern_Statement.Match_Statement_alt_0_Return();
                                    match._node_b = candidate_Statement_node_b;
                                    match._node_e = candidate_Statement_alt_0_Return_node_e;
                                    match._edge__edge0 = candidate_Statement_alt_0_Return_edge__edge0;
                                    match._edge__edge1 = candidate_Statement_alt_0_Return_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_Statement_alt_0_Return_node_e;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Return_node_e = candidate_Statement_alt_0_Return_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Return_node_e.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Return_node_e = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Statement_alt_0_Return_node_e) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Return_node_e == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Statement_alt_0_Return_node_e,candidate_Statement_alt_0_Return_node_e);
                                }
                                uint prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Return_edge__edge0 = candidate_Statement_alt_0_Return_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Return_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Return_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Statement_alt_0_Return_edge__edge0) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Statement_alt_0_Return_edge__edge0,candidate_Statement_alt_0_Return_edge__edge0);
                                }
                                uint prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    prevGlobal__candidate_Statement_alt_0_Return_edge__edge1 = candidate_Statement_alt_0_Return_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    candidate_Statement_alt_0_Return_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                } else {
                                    prevGlobal__candidate_Statement_alt_0_Return_edge__edge1 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Statement_alt_0_Return_edge__edge1) ? 1U : 0U;
                                    if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge1 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Statement_alt_0_Return_edge__edge1,candidate_Statement_alt_0_Return_edge__edge1);
                                }
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        Pattern_Statement.Match_Statement_alt_0_Return match = new Pattern_Statement.Match_Statement_alt_0_Return();
                                        match._node_b = candidate_Statement_node_b;
                                        match._node_e = candidate_Statement_alt_0_Return_node_e;
                                        match._edge__edge0 = candidate_Statement_alt_0_Return_edge__edge0;
                                        match._edge__edge1 = candidate_Statement_alt_0_Return_edge__edge1;
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
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Return_edge__edge1.lgspFlags = candidate_Statement_alt_0_Return_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Return_edge__edge1);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Return_edge__edge0.lgspFlags = candidate_Statement_alt_0_Return_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Return_edge__edge0);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Statement_alt_0_Return_node_e.lgspFlags = candidate_Statement_alt_0_Return_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_node_e;
                                        } else { 
                                            if(prevGlobal__candidate_Statement_alt_0_Return_node_e == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Return_node_e);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Return_edge__edge1.lgspFlags = candidate_Statement_alt_0_Return_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge1 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Return_edge__edge1);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Return_edge__edge0.lgspFlags = candidate_Statement_alt_0_Return_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge0 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Return_edge__edge0);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Statement_alt_0_Return_node_e.lgspFlags = candidate_Statement_alt_0_Return_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_node_e;
                                    } else { 
                                        if(prevGlobal__candidate_Statement_alt_0_Return_node_e == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Return_node_e);
                                        }
                                    }
                                    continue;
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Return_node_e.lgspFlags = candidate_Statement_alt_0_Return_node_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_node_e;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Return_node_e == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Statement_alt_0_Return_node_e);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Return_edge__edge0.lgspFlags = candidate_Statement_alt_0_Return_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Return_edge__edge0);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_Statement_alt_0_Return_edge__edge1.lgspFlags = candidate_Statement_alt_0_Return_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                                } else { 
                                    if(prevGlobal__candidate_Statement_alt_0_Return_edge__edge1 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Statement_alt_0_Return_edge__edge1);
                                    }
                                }
                            }
                            while( (candidate_Statement_alt_0_Return_edge__edge1 = candidate_Statement_alt_0_Return_edge__edge1.lgspOutNext) != head_candidate_Statement_alt_0_Return_edge__edge1 );
                        }
                    }
                    while( (candidate_Statement_alt_0_Return_edge__edge0 = candidate_Statement_alt_0_Return_edge__edge0.lgspOutNext) != head_candidate_Statement_alt_0_Return_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_Expressions : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Expressions(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Expressions.Instance.patternGraph;
        }

        public static PatternAction_Expressions getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Expressions newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Expressions(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Expressions oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Expressions freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Expressions next = null;

        public GRGEN_LGSP.LGSPNode Expressions_node_e;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Expressions.Match_Expressions_iter_0 patternpath_match_Expressions_iter_0 = null;
            Pattern_Expressions.Match_Expressions patternpath_match_Expressions = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Expressions_node_e 
            GRGEN_LGSP.LGSPNode candidate_Expressions_node_e = Expressions_node_e;
            // build match of Expressions for patternpath checks
            if(patternpath_match_Expressions==null) patternpath_match_Expressions = new Pattern_Expressions.Match_Expressions();
            patternpath_match_Expressions._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Expressions._node_e = candidate_Expressions_node_e;
            // Push iterated matching task for Expressions_iter_0
            IteratedAction_Expressions_iter_0 taskFor_iter_0 = IteratedAction_Expressions_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.Expressions_node_e = candidate_Expressions_node_e;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_Expressions;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for Expressions_iter_0
            openTasks.Pop();
            IteratedAction_Expressions_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Expressions.Match_Expressions match = new Pattern_Expressions.Match_Expressions();
                    match._node_e = candidate_Expressions_node_e;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_Expressions.Match_Expressions_iter_0, Pattern_Expressions.IMatch_Expressions_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_Expressions.IMatch_Expressions_iter_0) {
                        Pattern_Expressions.Match_Expressions_iter_0 cfpm = (Pattern_Expressions.Match_Expressions_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
                    }
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

    public class IteratedAction_Expressions_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_Expressions_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Expressions.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_Expressions_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_Expressions_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_Expressions_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_Expressions_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_Expressions_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_Expressions_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode Expressions_node_e;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_Expressions.Match_Expressions_iter_0 patternpath_match_Expressions_iter_0 = null;
            Pattern_Expressions.Match_Expressions patternpath_match_Expressions = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset Expressions_node_e 
                GRGEN_LGSP.LGSPNode candidate_Expressions_node_e = Expressions_node_e;
                // build match of Expressions_iter_0 for patternpath checks
                if(patternpath_match_Expressions_iter_0==null) patternpath_match_Expressions_iter_0 = new Pattern_Expressions.Match_Expressions_iter_0();
                patternpath_match_Expressions_iter_0._matchOfEnclosingPattern = null;
                patternpath_match_Expressions_iter_0._node_e = candidate_Expressions_node_e;
                // accept iterated instance match
                ++numMatchesIter;
                // Push subpattern matching task for _sub0
                PatternAction_ExpressionPattern taskFor__sub0 = PatternAction_ExpressionPattern.getNewTask(graph, openTasks);
                taskFor__sub0.ExpressionPattern_node_e = candidate_Expressions_node_e;
                taskFor__sub0.searchPatternpath = false;
                taskFor__sub0.matchOfNestingPattern = patternpath_match_Expressions_iter_0;
                taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
                openTasks.Push(taskFor__sub0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _sub0
                openTasks.Pop();
                PatternAction_ExpressionPattern.releaseTask(taskFor__sub0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    patternFound = true;
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Expressions.Match_Expressions_iter_0 match = new Pattern_Expressions.Match_Expressions_iter_0();
                        match._node_e = candidate_Expressions_node_e;
                        match.__sub0 = (@Pattern_ExpressionPattern.Match_ExpressionPattern)currentFoundPartialMatch.Pop();
                        match.__sub0._matchOfEnclosingPattern = match;
                        currentFoundPartialMatch.Push(match);
                    }
                    // if enough matches were found, we leave
                    if(true) // as soon as there's a match, it's enough for iterated
                    {
                        --numMatchesIter;
                        continue;
                    }
                    --numMatchesIter;
                    continue;
                }
                --numMatchesIter;
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_Expressions.Match_Expressions_iter_0 match = new Pattern_Expressions.Match_Expressions_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Expressions.Match_Expressions_iter_0 match = new Pattern_Expressions.Match_Expressions_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_ExpressionPattern : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_ExpressionPattern(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ExpressionPattern.Instance.patternGraph;
        }

        public static PatternAction_ExpressionPattern getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_ExpressionPattern newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ExpressionPattern(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_ExpressionPattern oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ExpressionPattern freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ExpressionPattern next = null;

        public GRGEN_LGSP.LGSPNode ExpressionPattern_node_e;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Call patternpath_match_ExpressionPattern_alt_0_Call = null;
            Pattern_ExpressionPattern.Match_ExpressionPattern patternpath_match_ExpressionPattern = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ExpressionPattern_node_e 
            GRGEN_LGSP.LGSPNode candidate_ExpressionPattern_node_e = ExpressionPattern_node_e;
            // Extend Outgoing ExpressionPattern_edge__edge0 from ExpressionPattern_node_e 
            GRGEN_LGSP.LGSPEdge head_candidate_ExpressionPattern_edge__edge0 = candidate_ExpressionPattern_node_e.lgspOuthead;
            if(head_candidate_ExpressionPattern_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_ExpressionPattern_edge__edge0 = head_candidate_ExpressionPattern_edge__edge0;
                do
                {
                    if(candidate_ExpressionPattern_edge__edge0.lgspType.TypeID!=3 && candidate_ExpressionPattern_edge__edge0.lgspType.TypeID!=11) {
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ExpressionPattern_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ExpressionPattern_edge__edge0)))
                    {
                        continue;
                    }
                    if(searchPatternpath && (candidate_ExpressionPattern_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_ExpressionPattern_edge__edge0, lastMatchAtPreviousNestingLevel))
                    {
                        continue;
                    }
                    // Implicit Target ExpressionPattern_node_sub from ExpressionPattern_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_ExpressionPattern_node_sub = candidate_ExpressionPattern_edge__edge0.lgspTarget;
                    if(candidate_ExpressionPattern_node_sub.lgspType.TypeID!=3) {
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ExpressionPattern_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ExpressionPattern_node_sub)))
                    {
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ExpressionPattern_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ExpressionPattern_node_sub)))
                    {
                        continue;
                    }
                    if(searchPatternpath && (candidate_ExpressionPattern_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_ExpressionPattern_node_sub, lastMatchAtPreviousNestingLevel))
                    {
                        continue;
                    }
                    // build match of ExpressionPattern for patternpath checks
                    if(patternpath_match_ExpressionPattern==null) patternpath_match_ExpressionPattern = new Pattern_ExpressionPattern.Match_ExpressionPattern();
                    patternpath_match_ExpressionPattern._matchOfEnclosingPattern = matchOfNestingPattern;
                    patternpath_match_ExpressionPattern._node_e = candidate_ExpressionPattern_node_e;
                    patternpath_match_ExpressionPattern._node_sub = candidate_ExpressionPattern_node_sub;
                    patternpath_match_ExpressionPattern._edge__edge0 = candidate_ExpressionPattern_edge__edge0;
                    uint prevSomeGlobal__candidate_ExpressionPattern_node_sub;
                    prevSomeGlobal__candidate_ExpressionPattern_node_sub = candidate_ExpressionPattern_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    candidate_ExpressionPattern_node_sub.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    uint prevSomeGlobal__candidate_ExpressionPattern_edge__edge0;
                    prevSomeGlobal__candidate_ExpressionPattern_edge__edge0 = candidate_ExpressionPattern_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    candidate_ExpressionPattern_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    // Push alternative matching task for ExpressionPattern_alt_0
                    AlternativeAction_ExpressionPattern_alt_0 taskFor_alt_0 = AlternativeAction_ExpressionPattern_alt_0.getNewTask(graph, openTasks, Pattern_ExpressionPattern.Instance.patternGraph.alternatives[(int)Pattern_ExpressionPattern.ExpressionPattern_AltNums.@alt_0].alternativeCases);
                    taskFor_alt_0.ExpressionPattern_node_sub = candidate_ExpressionPattern_node_sub;
                    taskFor_alt_0.searchPatternpath = searchPatternpath;
                    taskFor_alt_0.matchOfNestingPattern = patternpath_match_ExpressionPattern;
                    taskFor_alt_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                    openTasks.Push(taskFor_alt_0);
                    uint prevGlobal__candidate_ExpressionPattern_node_sub;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prevGlobal__candidate_ExpressionPattern_node_sub = candidate_ExpressionPattern_node_sub.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ExpressionPattern_node_sub.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                    } else {
                        prevGlobal__candidate_ExpressionPattern_node_sub = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_ExpressionPattern_node_sub) ? 1U : 0U;
                        if(prevGlobal__candidate_ExpressionPattern_node_sub == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_ExpressionPattern_node_sub,candidate_ExpressionPattern_node_sub);
                    }
                    uint prevGlobal__candidate_ExpressionPattern_edge__edge0;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prevGlobal__candidate_ExpressionPattern_edge__edge0 = candidate_ExpressionPattern_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_ExpressionPattern_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                    } else {
                        prevGlobal__candidate_ExpressionPattern_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ExpressionPattern_edge__edge0) ? 1U : 0U;
                        if(prevGlobal__candidate_ExpressionPattern_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ExpressionPattern_edge__edge0,candidate_ExpressionPattern_edge__edge0);
                    }
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop alternative matching task for ExpressionPattern_alt_0
                    openTasks.Pop();
                    AlternativeAction_ExpressionPattern_alt_0.releaseTask(taskFor_alt_0);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Pattern_ExpressionPattern.Match_ExpressionPattern match = new Pattern_ExpressionPattern.Match_ExpressionPattern();
                            match._node_e = candidate_ExpressionPattern_node_e;
                            match._node_sub = candidate_ExpressionPattern_node_sub;
                            match._edge__edge0 = candidate_ExpressionPattern_edge__edge0;
                            match._alt_0 = (Pattern_ExpressionPattern.IMatch_ExpressionPattern_alt_0)currentFoundPartialMatch.Pop();
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
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ExpressionPattern_edge__edge0.lgspFlags = candidate_ExpressionPattern_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_ExpressionPattern_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ExpressionPattern_node_sub.lgspFlags = candidate_ExpressionPattern_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_node_sub;
                            } else { 
                                if(prevGlobal__candidate_ExpressionPattern_node_sub == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ExpressionPattern_node_sub);
                                }
                            }
                            candidate_ExpressionPattern_edge__edge0.lgspFlags = candidate_ExpressionPattern_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_edge__edge0;
                            candidate_ExpressionPattern_node_sub.lgspFlags = candidate_ExpressionPattern_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_node_sub;
                            openTasks.Push(this);
                            return;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ExpressionPattern_edge__edge0.lgspFlags = candidate_ExpressionPattern_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_ExpressionPattern_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_edge__edge0);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ExpressionPattern_node_sub.lgspFlags = candidate_ExpressionPattern_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_node_sub;
                        } else { 
                            if(prevGlobal__candidate_ExpressionPattern_node_sub == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ExpressionPattern_node_sub);
                            }
                        }
                        candidate_ExpressionPattern_edge__edge0.lgspFlags = candidate_ExpressionPattern_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_edge__edge0;
                        candidate_ExpressionPattern_node_sub.lgspFlags = candidate_ExpressionPattern_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_node_sub;
                        continue;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_ExpressionPattern_node_sub.lgspFlags = candidate_ExpressionPattern_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_node_sub;
                    } else { 
                        if(prevGlobal__candidate_ExpressionPattern_node_sub == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_ExpressionPattern_node_sub);
                        }
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_ExpressionPattern_edge__edge0.lgspFlags = candidate_ExpressionPattern_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_edge__edge0;
                    } else { 
                        if(prevGlobal__candidate_ExpressionPattern_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_edge__edge0);
                        }
                    }
                    candidate_ExpressionPattern_node_sub.lgspFlags = candidate_ExpressionPattern_node_sub.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_node_sub;
                    candidate_ExpressionPattern_edge__edge0.lgspFlags = candidate_ExpressionPattern_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_edge__edge0;
                }
                while( (candidate_ExpressionPattern_edge__edge0 = candidate_ExpressionPattern_edge__edge0.lgspOutNext) != head_candidate_ExpressionPattern_edge__edge0 );
            }
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_ExpressionPattern_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_ExpressionPattern_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ExpressionPattern_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_ExpressionPattern_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ExpressionPattern_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_ExpressionPattern_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ExpressionPattern_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ExpressionPattern_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode ExpressionPattern_node_sub;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Call patternpath_match_ExpressionPattern_alt_0_Call = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ExpressionPattern_alt_0_Call 
            do {
                patternGraph = patternGraphs[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_CaseNums.@Call];
                // SubPreset ExpressionPattern_node_sub 
                GRGEN_LGSP.LGSPNode candidate_ExpressionPattern_node_sub = ExpressionPattern_node_sub;
                // Extend Outgoing ExpressionPattern_alt_0_Call_edge__edge0 from ExpressionPattern_node_sub 
                GRGEN_LGSP.LGSPEdge head_candidate_ExpressionPattern_alt_0_Call_edge__edge0 = candidate_ExpressionPattern_node_sub.lgspOuthead;
                if(head_candidate_ExpressionPattern_alt_0_Call_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ExpressionPattern_alt_0_Call_edge__edge0 = head_candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                    do
                    {
                        if(candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspType.TypeID!=9) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ExpressionPattern_alt_0_Call_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_ExpressionPattern_alt_0_Call_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // build match of ExpressionPattern_alt_0_Call for patternpath checks
                        if(patternpath_match_ExpressionPattern_alt_0_Call==null) patternpath_match_ExpressionPattern_alt_0_Call = new Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Call();
                        patternpath_match_ExpressionPattern_alt_0_Call._matchOfEnclosingPattern = matchOfNestingPattern;
                        patternpath_match_ExpressionPattern_alt_0_Call._node_sub = candidate_ExpressionPattern_node_sub;
                        patternpath_match_ExpressionPattern_alt_0_Call._edge__edge0 = candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                        uint prevSomeGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                        prevSomeGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        // Push subpattern matching task for _sub0
                        PatternAction_Expressions taskFor__sub0 = PatternAction_Expressions.getNewTask(graph, openTasks);
                        taskFor__sub0.Expressions_node_e = candidate_ExpressionPattern_node_sub;
                        taskFor__sub0.searchPatternpath = searchPatternpath;
                        taskFor__sub0.matchOfNestingPattern = patternpath_match_ExpressionPattern_alt_0_Call;
                        taskFor__sub0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                        openTasks.Push(taskFor__sub0);
                        uint prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ExpressionPattern_alt_0_Call_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ExpressionPattern_alt_0_Call_edge__edge0,candidate_ExpressionPattern_alt_0_Call_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0
                        openTasks.Pop();
                        PatternAction_Expressions.releaseTask(taskFor__sub0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Call match = new Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Call();
                                match._node_sub = candidate_ExpressionPattern_node_sub;
                                match._edge__edge0 = candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                                match.__sub0 = (@Pattern_Expressions.Match_Expressions)currentFoundPartialMatch.Pop();
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_alt_0_Call_edge__edge0);
                                    }
                                }
                                candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_alt_0_Call_edge__edge0);
                                }
                            }
                            candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_alt_0_Call_edge__edge0);
                            }
                        }
                        candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                    }
                    while( (candidate_ExpressionPattern_alt_0_Call_edge__edge0 = candidate_ExpressionPattern_alt_0_Call_edge__edge0.lgspOutNext) != head_candidate_ExpressionPattern_alt_0_Call_edge__edge0 );
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
            // Alternative case ExpressionPattern_alt_0_Use 
            do {
                patternGraph = patternGraphs[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_CaseNums.@Use];
                // SubPreset ExpressionPattern_node_sub 
                GRGEN_LGSP.LGSPNode candidate_ExpressionPattern_node_sub = ExpressionPattern_node_sub;
                // Extend Outgoing ExpressionPattern_alt_0_Use_edge__edge0 from ExpressionPattern_node_sub 
                GRGEN_LGSP.LGSPEdge head_candidate_ExpressionPattern_alt_0_Use_edge__edge0 = candidate_ExpressionPattern_node_sub.lgspOuthead;
                if(head_candidate_ExpressionPattern_alt_0_Use_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ExpressionPattern_alt_0_Use_edge__edge0 = head_candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                    do
                    {
                        if(candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspType.TypeID!=7) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ExpressionPattern_alt_0_Use_edge__edge0)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_ExpressionPattern_alt_0_Use_edge__edge0, lastMatchAtPreviousNestingLevel))
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Use match = new Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Use();
                            match._node_sub = candidate_ExpressionPattern_node_sub;
                            match._edge__edge0 = candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0 = candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_ExpressionPattern_alt_0_Use_edge__edge0) ? 1U : 0U;
                            if(prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_ExpressionPattern_alt_0_Use_edge__edge0,candidate_ExpressionPattern_alt_0_Use_edge__edge0);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Use match = new Pattern_ExpressionPattern.Match_ExpressionPattern_alt_0_Use();
                                match._node_sub = candidate_ExpressionPattern_node_sub;
                                match._edge__edge0 = candidate_ExpressionPattern_alt_0_Use_edge__edge0;
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
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                                } else { 
                                    if(prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0 == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_alt_0_Use_edge__edge0);
                                    }
                                }
                                openTasks.Push(this);
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_alt_0_Use_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags = candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_ExpressionPattern_alt_0_Use_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ExpressionPattern_alt_0_Use_edge__edge0 = candidate_ExpressionPattern_alt_0_Use_edge__edge0.lgspOutNext) != head_candidate_ExpressionPattern_alt_0_Use_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_Bodies : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Bodies(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Bodies.Instance.patternGraph;
        }

        public static PatternAction_Bodies getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Bodies newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Bodies(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Bodies oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Bodies freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Bodies next = null;

        public GRGEN_LGSP.LGSPNode Bodies_node_m5;
        public GRGEN_LGSP.LGSPNode Bodies_node_c1;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Bodies.Match_Bodies_iter_0 patternpath_match_Bodies_iter_0 = null;
            Pattern_Bodies.Match_Bodies patternpath_match_Bodies = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Bodies_node_m5 
            GRGEN_LGSP.LGSPNode candidate_Bodies_node_m5 = Bodies_node_m5;
            // SubPreset Bodies_node_c1 
            GRGEN_LGSP.LGSPNode candidate_Bodies_node_c1 = Bodies_node_c1;
            // build match of Bodies for patternpath checks
            if(patternpath_match_Bodies==null) patternpath_match_Bodies = new Pattern_Bodies.Match_Bodies();
            patternpath_match_Bodies._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_Bodies._node_m5 = candidate_Bodies_node_m5;
            patternpath_match_Bodies._node_c1 = candidate_Bodies_node_c1;
            // Push iterated matching task for Bodies_iter_0
            IteratedAction_Bodies_iter_0 taskFor_iter_0 = IteratedAction_Bodies_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.Bodies_node_m5 = candidate_Bodies_node_m5;
            taskFor_iter_0.Bodies_node_c1 = candidate_Bodies_node_c1;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_Bodies;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for Bodies_iter_0
            openTasks.Pop();
            IteratedAction_Bodies_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_Bodies.Match_Bodies match = new Pattern_Bodies.Match_Bodies();
                    match._node_m5 = candidate_Bodies_node_m5;
                    match._node_c1 = candidate_Bodies_node_c1;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_Bodies.Match_Bodies_iter_0, Pattern_Bodies.IMatch_Bodies_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_Bodies.IMatch_Bodies_iter_0) {
                        Pattern_Bodies.Match_Bodies_iter_0 cfpm = (Pattern_Bodies.Match_Bodies_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
                    }
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

    public class IteratedAction_Bodies_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_Bodies_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Bodies.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_Bodies_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_Bodies_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_Bodies_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_Bodies_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_Bodies_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_Bodies_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode Bodies_node_m5;
        public GRGEN_LGSP.LGSPNode Bodies_node_c1;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_Bodies.Match_Bodies_iter_0 patternpath_match_Bodies_iter_0 = null;
            Pattern_Bodies.Match_Bodies patternpath_match_Bodies = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset Bodies_node_m5 
                GRGEN_LGSP.LGSPNode candidate_Bodies_node_m5 = Bodies_node_m5;
                // SubPreset Bodies_node_c1 
                GRGEN_LGSP.LGSPNode candidate_Bodies_node_c1 = Bodies_node_c1;
                // build match of Bodies_iter_0 for patternpath checks
                if(patternpath_match_Bodies_iter_0==null) patternpath_match_Bodies_iter_0 = new Pattern_Bodies.Match_Bodies_iter_0();
                patternpath_match_Bodies_iter_0._matchOfEnclosingPattern = null;
                patternpath_match_Bodies_iter_0._node_m5 = candidate_Bodies_node_m5;
                patternpath_match_Bodies_iter_0._node_c1 = candidate_Bodies_node_c1;
                // accept iterated instance match
                ++numMatchesIter;
                // Push subpattern matching task for b
                PatternAction_Body taskFor_b = PatternAction_Body.getNewTask(graph, openTasks);
                taskFor_b.Body_node_m5 = candidate_Bodies_node_m5;
                taskFor_b.Body_node_c1 = candidate_Bodies_node_c1;
                taskFor_b.searchPatternpath = false;
                taskFor_b.matchOfNestingPattern = patternpath_match_Bodies_iter_0;
                taskFor_b.lastMatchAtPreviousNestingLevel = null;
                openTasks.Push(taskFor_b);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for b
                openTasks.Pop();
                PatternAction_Body.releaseTask(taskFor_b);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    patternFound = true;
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Bodies.Match_Bodies_iter_0 match = new Pattern_Bodies.Match_Bodies_iter_0();
                        match._node_m5 = candidate_Bodies_node_m5;
                        match._node_c1 = candidate_Bodies_node_c1;
                        match._b = (@Pattern_Body.Match_Body)currentFoundPartialMatch.Pop();
                        match._b._matchOfEnclosingPattern = match;
                        currentFoundPartialMatch.Push(match);
                    }
                    // if enough matches were found, we leave
                    if(true) // as soon as there's a match, it's enough for iterated
                    {
                        --numMatchesIter;
                        continue;
                    }
                    --numMatchesIter;
                    continue;
                }
                --numMatchesIter;
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_Bodies.Match_Bodies_iter_0 match = new Pattern_Bodies.Match_Bodies_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_Bodies.Match_Bodies_iter_0 match = new Pattern_Bodies.Match_Bodies_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_Body : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_Body(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Body.Instance.patternGraph;
        }

        public static PatternAction_Body getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_Body newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Body(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_Body oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Body freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Body next = null;

        public GRGEN_LGSP.LGSPNode Body_node_c1;
        public GRGEN_LGSP.LGSPNode Body_node_m5;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_Body.Match_Body patternpath_match_Body = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Body_node_c1 
            GRGEN_LGSP.LGSPNode candidate_Body_node_c1 = Body_node_c1;
            // SubPreset Body_node_m5 
            GRGEN_LGSP.LGSPNode candidate_Body_node_m5 = Body_node_m5;
            // Extend Outgoing Body_edge__edge0 from Body_node_c1 
            GRGEN_LGSP.LGSPEdge head_candidate_Body_edge__edge0 = candidate_Body_node_c1.lgspOuthead;
            if(head_candidate_Body_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_Body_edge__edge0 = head_candidate_Body_edge__edge0;
                do
                {
                    if(candidate_Body_edge__edge0.lgspType.TypeID!=3 && candidate_Body_edge__edge0.lgspType.TypeID!=11) {
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Body_edge__edge0)))
                    {
                        continue;
                    }
                    if(searchPatternpath && (candidate_Body_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Body_edge__edge0, lastMatchAtPreviousNestingLevel))
                    {
                        continue;
                    }
                    uint prev__candidate_Body_edge__edge0;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev__candidate_Body_edge__edge0 = candidate_Body_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_Body_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev__candidate_Body_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Body_edge__edge0) ? 1U : 0U;
                        if(prev__candidate_Body_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Body_edge__edge0,candidate_Body_edge__edge0);
                    }
                    // Implicit Target Body_node_c2 from Body_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_Body_node_c2 = candidate_Body_edge__edge0.lgspTarget;
                    if(candidate_Body_node_c2.lgspType.TypeID!=5) {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                        } else { 
                            if(prev__candidate_Body_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                            }
                        }
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_node_c2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Body_node_c2)))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                        } else { 
                            if(prev__candidate_Body_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                            }
                        }
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_node_c2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Body_node_c2)))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                        } else { 
                            if(prev__candidate_Body_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                            }
                        }
                        continue;
                    }
                    if(searchPatternpath && (candidate_Body_node_c2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Body_node_c2, lastMatchAtPreviousNestingLevel))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                        } else { 
                            if(prev__candidate_Body_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                            }
                        }
                        continue;
                    }
                    // Extend Outgoing Body_edge__edge1 from Body_node_c2 
                    GRGEN_LGSP.LGSPEdge head_candidate_Body_edge__edge1 = candidate_Body_node_c2.lgspOuthead;
                    if(head_candidate_Body_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_Body_edge__edge1 = head_candidate_Body_edge__edge1;
                        do
                        {
                            if(candidate_Body_edge__edge1.lgspType.TypeID!=3 && candidate_Body_edge__edge1.lgspType.TypeID!=11) {
                                continue;
                            }
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Body_edge__edge1)))
                            {
                                continue;
                            }
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Body_edge__edge1)))
                            {
                                continue;
                            }
                            if(searchPatternpath && (candidate_Body_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Body_edge__edge1, lastMatchAtPreviousNestingLevel))
                            {
                                continue;
                            }
                            // Implicit Target Body_node_b from Body_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_Body_node_b = candidate_Body_edge__edge1.lgspTarget;
                            if(candidate_Body_node_b.lgspType.TypeID!=2) {
                                continue;
                            }
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Body_node_b)))
                            {
                                continue;
                            }
                            if(searchPatternpath && (candidate_Body_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Body_node_b, lastMatchAtPreviousNestingLevel))
                            {
                                continue;
                            }
                            // Extend Outgoing Body_edge__edge2 from Body_node_b 
                            GRGEN_LGSP.LGSPEdge head_candidate_Body_edge__edge2 = candidate_Body_node_b.lgspOuthead;
                            if(head_candidate_Body_edge__edge2 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_Body_edge__edge2 = head_candidate_Body_edge__edge2;
                                do
                                {
                                    if(candidate_Body_edge__edge2.lgspType.TypeID!=6) {
                                        continue;
                                    }
                                    if(candidate_Body_edge__edge2.lgspTarget != candidate_Body_node_m5) {
                                        continue;
                                    }
                                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_edge__edge2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Body_edge__edge2)))
                                    {
                                        continue;
                                    }
                                    if(searchPatternpath && (candidate_Body_edge__edge2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_Body_edge__edge2, lastMatchAtPreviousNestingLevel))
                                    {
                                        continue;
                                    }
                                    // build match of Body for patternpath checks
                                    if(patternpath_match_Body==null) patternpath_match_Body = new Pattern_Body.Match_Body();
                                    patternpath_match_Body._matchOfEnclosingPattern = matchOfNestingPattern;
                                    patternpath_match_Body._node_c1 = candidate_Body_node_c1;
                                    patternpath_match_Body._node_c2 = candidate_Body_node_c2;
                                    patternpath_match_Body._node_b = candidate_Body_node_b;
                                    patternpath_match_Body._node_m5 = candidate_Body_node_m5;
                                    patternpath_match_Body._edge__edge0 = candidate_Body_edge__edge0;
                                    patternpath_match_Body._edge__edge1 = candidate_Body_edge__edge1;
                                    patternpath_match_Body._edge__edge2 = candidate_Body_edge__edge2;
                                    uint prevSomeGlobal__candidate_Body_node_c2;
                                    prevSomeGlobal__candidate_Body_node_c2 = candidate_Body_node_c2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_Body_node_c2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_Body_node_b;
                                    prevSomeGlobal__candidate_Body_node_b = candidate_Body_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_Body_node_b.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_Body_edge__edge0;
                                    prevSomeGlobal__candidate_Body_edge__edge0 = candidate_Body_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_Body_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_Body_edge__edge1;
                                    prevSomeGlobal__candidate_Body_edge__edge1 = candidate_Body_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_Body_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_Body_edge__edge2;
                                    prevSomeGlobal__candidate_Body_edge__edge2 = candidate_Body_edge__edge2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_Body_edge__edge2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    // Push subpattern matching task for s
                                    PatternAction_Statements taskFor_s = PatternAction_Statements.getNewTask(graph, openTasks);
                                    taskFor_s.Statements_node_b = candidate_Body_node_b;
                                    taskFor_s.searchPatternpath = searchPatternpath;
                                    taskFor_s.matchOfNestingPattern = patternpath_match_Body;
                                    taskFor_s.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                                    openTasks.Push(taskFor_s);
                                    // Push subpattern matching task for p
                                    PatternAction_Parameters taskFor_p = PatternAction_Parameters.getNewTask(graph, openTasks);
                                    taskFor_p.Parameters_node_b = candidate_Body_node_b;
                                    taskFor_p.searchPatternpath = searchPatternpath;
                                    taskFor_p.matchOfNestingPattern = patternpath_match_Body;
                                    taskFor_p.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
                                    openTasks.Push(taskFor_p);
                                    uint prevGlobal__candidate_Body_node_c2;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_Body_node_c2 = candidate_Body_node_c2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_Body_node_c2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_Body_node_c2 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Body_node_c2) ? 1U : 0U;
                                        if(prevGlobal__candidate_Body_node_c2 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Body_node_c2,candidate_Body_node_c2);
                                    }
                                    uint prevGlobal__candidate_Body_node_b;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_Body_node_b = candidate_Body_node_b.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_Body_node_b.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_Body_node_b = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_Body_node_b) ? 1U : 0U;
                                        if(prevGlobal__candidate_Body_node_b == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_Body_node_b,candidate_Body_node_b);
                                    }
                                    uint prevGlobal__candidate_Body_edge__edge0;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_Body_edge__edge0 = candidate_Body_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_Body_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_Body_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Body_edge__edge0) ? 1U : 0U;
                                        if(prevGlobal__candidate_Body_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Body_edge__edge0,candidate_Body_edge__edge0);
                                    }
                                    uint prevGlobal__candidate_Body_edge__edge1;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_Body_edge__edge1 = candidate_Body_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_Body_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_Body_edge__edge1 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Body_edge__edge1) ? 1U : 0U;
                                        if(prevGlobal__candidate_Body_edge__edge1 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Body_edge__edge1,candidate_Body_edge__edge1);
                                    }
                                    uint prevGlobal__candidate_Body_edge__edge2;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_Body_edge__edge2 = candidate_Body_edge__edge2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_Body_edge__edge2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_Body_edge__edge2 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Body_edge__edge2) ? 1U : 0U;
                                        if(prevGlobal__candidate_Body_edge__edge2 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Body_edge__edge2,candidate_Body_edge__edge2);
                                    }
                                    // Match subpatterns 
                                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                    // Pop subpattern matching task for p
                                    openTasks.Pop();
                                    PatternAction_Parameters.releaseTask(taskFor_p);
                                    // Pop subpattern matching task for s
                                    openTasks.Pop();
                                    PatternAction_Statements.releaseTask(taskFor_s);
                                    // Check whether subpatterns were found 
                                    if(matchesList.Count>0) {
                                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                        {
                                            Pattern_Body.Match_Body match = new Pattern_Body.Match_Body();
                                            match._node_c1 = candidate_Body_node_c1;
                                            match._node_c2 = candidate_Body_node_c2;
                                            match._node_b = candidate_Body_node_b;
                                            match._node_m5 = candidate_Body_node_m5;
                                            match._edge__edge0 = candidate_Body_edge__edge0;
                                            match._edge__edge1 = candidate_Body_edge__edge1;
                                            match._edge__edge2 = candidate_Body_edge__edge2;
                                            match._p = (@Pattern_Parameters.Match_Parameters)currentFoundPartialMatch.Pop();
                                            match._p._matchOfEnclosingPattern = match;
                                            match._s = (@Pattern_Statements.Match_Statements)currentFoundPartialMatch.Pop();
                                            match._s._matchOfEnclosingPattern = match;
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
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Body_edge__edge2.lgspFlags = candidate_Body_edge__edge2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge2;
                                            } else { 
                                                if(prevGlobal__candidate_Body_edge__edge2 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge2);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Body_edge__edge1.lgspFlags = candidate_Body_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge1;
                                            } else { 
                                                if(prevGlobal__candidate_Body_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge1);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge0;
                                            } else { 
                                                if(prevGlobal__candidate_Body_edge__edge0 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Body_node_b.lgspFlags = candidate_Body_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_node_b;
                                            } else { 
                                                if(prevGlobal__candidate_Body_node_b == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Body_node_b);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Body_node_c2.lgspFlags = candidate_Body_node_c2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_node_c2;
                                            } else { 
                                                if(prevGlobal__candidate_Body_node_c2 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Body_node_c2);
                                                }
                                            }
                                            candidate_Body_edge__edge2.lgspFlags = candidate_Body_edge__edge2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge2;
                                            candidate_Body_edge__edge1.lgspFlags = candidate_Body_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge1;
                                            candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge0;
                                            candidate_Body_node_b.lgspFlags = candidate_Body_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_node_b;
                                            candidate_Body_node_c2.lgspFlags = candidate_Body_node_c2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_node_c2;
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                                            } else { 
                                                if(prev__candidate_Body_edge__edge0 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                                                }
                                            }
                                            openTasks.Push(this);
                                            return;
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Body_edge__edge2.lgspFlags = candidate_Body_edge__edge2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge2;
                                        } else { 
                                            if(prevGlobal__candidate_Body_edge__edge2 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge2);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Body_edge__edge1.lgspFlags = candidate_Body_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge1;
                                        } else { 
                                            if(prevGlobal__candidate_Body_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge1);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge0;
                                        } else { 
                                            if(prevGlobal__candidate_Body_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Body_node_b.lgspFlags = candidate_Body_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_node_b;
                                        } else { 
                                            if(prevGlobal__candidate_Body_node_b == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Body_node_b);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_Body_node_c2.lgspFlags = candidate_Body_node_c2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_node_c2;
                                        } else { 
                                            if(prevGlobal__candidate_Body_node_c2 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Body_node_c2);
                                            }
                                        }
                                        candidate_Body_edge__edge2.lgspFlags = candidate_Body_edge__edge2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge2;
                                        candidate_Body_edge__edge1.lgspFlags = candidate_Body_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge1;
                                        candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge0;
                                        candidate_Body_node_b.lgspFlags = candidate_Body_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_node_b;
                                        candidate_Body_node_c2.lgspFlags = candidate_Body_node_c2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_node_c2;
                                        continue;
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Body_node_c2.lgspFlags = candidate_Body_node_c2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_node_c2;
                                    } else { 
                                        if(prevGlobal__candidate_Body_node_c2 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Body_node_c2);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Body_node_b.lgspFlags = candidate_Body_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_node_b;
                                    } else { 
                                        if(prevGlobal__candidate_Body_node_b == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_Body_node_b);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge0;
                                    } else { 
                                        if(prevGlobal__candidate_Body_edge__edge0 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Body_edge__edge1.lgspFlags = candidate_Body_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge1;
                                    } else { 
                                        if(prevGlobal__candidate_Body_edge__edge1 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge1);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_Body_edge__edge2.lgspFlags = candidate_Body_edge__edge2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_Body_edge__edge2;
                                    } else { 
                                        if(prevGlobal__candidate_Body_edge__edge2 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge2);
                                        }
                                    }
                                    candidate_Body_node_c2.lgspFlags = candidate_Body_node_c2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_node_c2;
                                    candidate_Body_node_b.lgspFlags = candidate_Body_node_b.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_node_b;
                                    candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge0;
                                    candidate_Body_edge__edge1.lgspFlags = candidate_Body_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge1;
                                    candidate_Body_edge__edge2.lgspFlags = candidate_Body_edge__edge2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_Body_edge__edge2;
                                }
                                while( (candidate_Body_edge__edge2 = candidate_Body_edge__edge2.lgspOutNext) != head_candidate_Body_edge__edge2 );
                            }
                        }
                        while( (candidate_Body_edge__edge1 = candidate_Body_edge__edge1.lgspOutNext) != head_candidate_Body_edge__edge1 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_Body_edge__edge0.lgspFlags = candidate_Body_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                    } else { 
                        if(prev__candidate_Body_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                        }
                    }
                }
                while( (candidate_Body_edge__edge0 = candidate_Body_edge__edge0.lgspOutNext) != head_candidate_Body_edge__edge0 );
            }
            openTasks.Push(this);
            return;
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_createProgramGraphExample
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_createProgramGraphExample.IMatch_createProgramGraphExample match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_createProgramGraphExample : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_createProgramGraphExample
    {
        public Action_createProgramGraphExample() {
            _rulePattern = Rule_createProgramGraphExample.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createProgramGraphExample.Match_createProgramGraphExample, Rule_createProgramGraphExample.IMatch_createProgramGraphExample>(this);
        }

        public Rule_createProgramGraphExample _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "createProgramGraphExample"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createProgramGraphExample.Match_createProgramGraphExample, Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches;

        public static Action_createProgramGraphExample Instance { get { return instance; } }
        private static Action_createProgramGraphExample instance = new Action_createProgramGraphExample();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createProgramGraphExample.Match_createProgramGraphExample match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_createProgramGraphExample.IMatch_createProgramGraphExample match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches)
        {
            foreach(Rule_createProgramGraphExample.IMatch_createProgramGraphExample match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_createProgramGraphExample.IMatch_createProgramGraphExample match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_createProgramGraphExample.IMatch_createProgramGraphExample)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphExample.IMatch_createProgramGraphExample>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_createProgramGraphPullUp
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp match, out GRGEN_MODEL.IClass output_0, out GRGEN_MODEL.IMethodBody output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches, out GRGEN_MODEL.IClass output_0, out GRGEN_MODEL.IMethodBody output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, ref GRGEN_MODEL.IClass output_0, ref GRGEN_MODEL.IMethodBody output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, ref GRGEN_MODEL.IClass output_0, ref GRGEN_MODEL.IMethodBody output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_createProgramGraphPullUp : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_createProgramGraphPullUp
    {
        public Action_createProgramGraphPullUp() {
            _rulePattern = Rule_createProgramGraphPullUp.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createProgramGraphPullUp.Match_createProgramGraphPullUp, Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp>(this);
        }

        public Rule_createProgramGraphPullUp _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "createProgramGraphPullUp"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createProgramGraphPullUp.Match_createProgramGraphPullUp, Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches;

        public static Action_createProgramGraphPullUp Instance { get { return instance; } }
        private static Action_createProgramGraphPullUp instance = new Action_createProgramGraphPullUp();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createProgramGraphPullUp.Match_createProgramGraphPullUp match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp match, out GRGEN_MODEL.IClass output_0, out GRGEN_MODEL.IMethodBody output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches, out GRGEN_MODEL.IClass output_0, out GRGEN_MODEL.IMethodBody output_1)
        {
            output_0 = null;
            output_1 = null;
            foreach(Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, ref GRGEN_MODEL.IClass output_0, ref GRGEN_MODEL.IMethodBody output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, ref GRGEN_MODEL.IClass output_0, ref GRGEN_MODEL.IMethodBody output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches;
            GRGEN_MODEL.IClass output_0; GRGEN_MODEL.IMethodBody output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IClass output_0; GRGEN_MODEL.IMethodBody output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp> matches;
            GRGEN_MODEL.IClass output_0; GRGEN_MODEL.IMethodBody output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IClass output_0; GRGEN_MODEL.IMethodBody output_1; 
            Modify(graph, (Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IClass output_0; GRGEN_MODEL.IMethodBody output_1; 
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_createProgramGraphPullUp.IMatch_createProgramGraphPullUp>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_MODEL.IClass output_0 = null; GRGEN_MODEL.IMethodBody output_1 = null; 
            if(Apply(graph, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            GRGEN_MODEL.IClass output_0 = null; GRGEN_MODEL.IMethodBody output_1 = null; 
            if(Apply(graph, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_MODEL.IClass output_0 = null; GRGEN_MODEL.IMethodBody output_1 = null; 
            if(ApplyAll(maxMatches, graph, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            GRGEN_MODEL.IClass output_0 = null; GRGEN_MODEL.IMethodBody output_1 = null; 
            if(ApplyAll(maxMatches, graph, ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_pullUpMethod
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_pullUpMethod.IMatch_pullUpMethod match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4);
    }
    
    public class Action_pullUpMethod : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_pullUpMethod
    {
        public Action_pullUpMethod() {
            _rulePattern = Rule_pullUpMethod.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_pullUpMethod.Match_pullUpMethod, Rule_pullUpMethod.IMatch_pullUpMethod>(this);
        }

        public Rule_pullUpMethod _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "pullUpMethod"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_pullUpMethod.Match_pullUpMethod, Rule_pullUpMethod.IMatch_pullUpMethod> matches;

        public static Action_pullUpMethod Instance { get { return instance; } }
        private static Action_pullUpMethod instance = new Action_pullUpMethod();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_pullUpMethod.Match_pullUpMethod patternpath_match_pullUpMethod = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset pullUpMethod_node_c1 
            GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_c1 = (GRGEN_LGSP.LGSPNode)pullUpMethod_node_c1;
            if(candidate_pullUpMethod_node_c1.lgspType.TypeID!=5) {
                return matches;
            }
            uint prev__candidate_pullUpMethod_node_c1;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_pullUpMethod_node_c1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_pullUpMethod_node_c1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_pullUpMethod_node_c1) ? 1U : 0U;
                if(prev__candidate_pullUpMethod_node_c1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_pullUpMethod_node_c1,candidate_pullUpMethod_node_c1);
            }
            // Preset pullUpMethod_node_b4 
            GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_b4 = (GRGEN_LGSP.LGSPNode)pullUpMethod_node_b4;
            if(candidate_pullUpMethod_node_b4.lgspType.TypeID!=2) {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                } else { 
                    if(prev__candidate_pullUpMethod_node_c1 == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                    }
                }
                return matches;
            }
            // Extend Outgoing pullUpMethod_edge__edge0 from pullUpMethod_node_c1 
            GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_node_c1.lgspOuthead;
            if(head_candidate_pullUpMethod_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge0 = head_candidate_pullUpMethod_edge__edge0;
                do
                {
                    if(candidate_pullUpMethod_edge__edge0.lgspType.TypeID!=3 && candidate_pullUpMethod_edge__edge0.lgspType.TypeID!=11) {
                        continue;
                    }
                    uint prev__candidate_pullUpMethod_edge__edge0;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_pullUpMethod_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev__candidate_pullUpMethod_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_pullUpMethod_edge__edge0) ? 1U : 0U;
                        if(prev__candidate_pullUpMethod_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_pullUpMethod_edge__edge0,candidate_pullUpMethod_edge__edge0);
                    }
                    // Implicit Target pullUpMethod_node_c3 from pullUpMethod_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_edge__edge0.lgspTarget;
                    if(candidate_pullUpMethod_node_c3.lgspType.TypeID!=5) {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                        } else { 
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                            }
                        }
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_node_c3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_pullUpMethod_node_c3)))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                        } else { 
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                            }
                        }
                        continue;
                    }
                    // Extend Outgoing pullUpMethod_edge__edge1 from pullUpMethod_node_b4 
                    GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_node_b4.lgspOuthead;
                    if(head_candidate_pullUpMethod_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge1 = head_candidate_pullUpMethod_edge__edge1;
                        do
                        {
                            if(candidate_pullUpMethod_edge__edge1.lgspType.TypeID!=6) {
                                continue;
                            }
                            // Implicit Target pullUpMethod_node_m5 from pullUpMethod_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_edge__edge1.lgspTarget;
                            if(candidate_pullUpMethod_node_m5.lgspType.TypeID!=7) {
                                continue;
                            }
                            // Extend Outgoing pullUpMethod_edge_m from pullUpMethod_node_c3 
                            GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge_m = candidate_pullUpMethod_node_c3.lgspOuthead;
                            if(head_candidate_pullUpMethod_edge_m != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge_m = head_candidate_pullUpMethod_edge_m;
                                do
                                {
                                    if(candidate_pullUpMethod_edge_m.lgspType.TypeID!=3 && candidate_pullUpMethod_edge_m.lgspType.TypeID!=11) {
                                        continue;
                                    }
                                    if(candidate_pullUpMethod_edge_m.lgspTarget != candidate_pullUpMethod_node_b4) {
                                        continue;
                                    }
                                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_edge_m.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_pullUpMethod_edge_m)))
                                    {
                                        continue;
                                    }
                                    // build match of pullUpMethod for patternpath checks
                                    if(patternpath_match_pullUpMethod==null) patternpath_match_pullUpMethod = new Rule_pullUpMethod.Match_pullUpMethod();
                                    patternpath_match_pullUpMethod._matchOfEnclosingPattern = null;
                                    patternpath_match_pullUpMethod._node_c1 = candidate_pullUpMethod_node_c1;
                                    patternpath_match_pullUpMethod._node_c3 = candidate_pullUpMethod_node_c3;
                                    patternpath_match_pullUpMethod._node_b4 = candidate_pullUpMethod_node_b4;
                                    patternpath_match_pullUpMethod._node_m5 = candidate_pullUpMethod_node_m5;
                                    patternpath_match_pullUpMethod._edge__edge0 = candidate_pullUpMethod_edge__edge0;
                                    patternpath_match_pullUpMethod._edge_m = candidate_pullUpMethod_edge_m;
                                    patternpath_match_pullUpMethod._edge__edge1 = candidate_pullUpMethod_edge__edge1;
                                    uint prevSomeGlobal__candidate_pullUpMethod_node_c1;
                                    prevSomeGlobal__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_c1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_pullUpMethod_node_c3;
                                    prevSomeGlobal__candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_node_c3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_c3.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_pullUpMethod_node_b4;
                                    prevSomeGlobal__candidate_pullUpMethod_node_b4 = candidate_pullUpMethod_node_b4.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_b4.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_pullUpMethod_node_m5;
                                    prevSomeGlobal__candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_node_m5.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_m5.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_pullUpMethod_edge__edge0;
                                    prevSomeGlobal__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_pullUpMethod_edge_m;
                                    prevSomeGlobal__candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_edge_m.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    uint prevSomeGlobal__candidate_pullUpMethod_edge__edge1;
                                    prevSomeGlobal__candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                                    // Push subpattern matching task for bs
                                    PatternAction_Bodies taskFor_bs = PatternAction_Bodies.getNewTask(graph, openTasks);
                                    taskFor_bs.Bodies_node_m5 = candidate_pullUpMethod_node_m5;
                                    taskFor_bs.Bodies_node_c1 = candidate_pullUpMethod_node_c1;
                                    taskFor_bs.searchPatternpath = false;
                                    taskFor_bs.matchOfNestingPattern = patternpath_match_pullUpMethod;
                                    taskFor_bs.lastMatchAtPreviousNestingLevel = null;
                                    openTasks.Push(taskFor_bs);
                                    uint prevGlobal__candidate_pullUpMethod_node_c1;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_pullUpMethod_node_c1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_pullUpMethod_node_c1 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_pullUpMethod_node_c1) ? 1U : 0U;
                                        if(prevGlobal__candidate_pullUpMethod_node_c1 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_pullUpMethod_node_c1,candidate_pullUpMethod_node_c1);
                                    }
                                    uint prevGlobal__candidate_pullUpMethod_node_c3;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_node_c3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_pullUpMethod_node_c3.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_pullUpMethod_node_c3 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_pullUpMethod_node_c3) ? 1U : 0U;
                                        if(prevGlobal__candidate_pullUpMethod_node_c3 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_pullUpMethod_node_c3,candidate_pullUpMethod_node_c3);
                                    }
                                    uint prevGlobal__candidate_pullUpMethod_node_b4;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_pullUpMethod_node_b4 = candidate_pullUpMethod_node_b4.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_pullUpMethod_node_b4.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_pullUpMethod_node_b4 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_pullUpMethod_node_b4) ? 1U : 0U;
                                        if(prevGlobal__candidate_pullUpMethod_node_b4 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_pullUpMethod_node_b4,candidate_pullUpMethod_node_b4);
                                    }
                                    uint prevGlobal__candidate_pullUpMethod_node_m5;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_node_m5.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_pullUpMethod_node_m5.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_pullUpMethod_node_m5 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_pullUpMethod_node_m5) ? 1U : 0U;
                                        if(prevGlobal__candidate_pullUpMethod_node_m5 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_pullUpMethod_node_m5,candidate_pullUpMethod_node_m5);
                                    }
                                    uint prevGlobal__candidate_pullUpMethod_edge__edge0;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_pullUpMethod_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_pullUpMethod_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_pullUpMethod_edge__edge0) ? 1U : 0U;
                                        if(prevGlobal__candidate_pullUpMethod_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_pullUpMethod_edge__edge0,candidate_pullUpMethod_edge__edge0);
                                    }
                                    uint prevGlobal__candidate_pullUpMethod_edge_m;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_pullUpMethod_edge_m.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_pullUpMethod_edge_m = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_pullUpMethod_edge_m) ? 1U : 0U;
                                        if(prevGlobal__candidate_pullUpMethod_edge_m == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_pullUpMethod_edge_m,candidate_pullUpMethod_edge_m);
                                    }
                                    uint prevGlobal__candidate_pullUpMethod_edge__edge1;
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        prevGlobal__candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                        candidate_pullUpMethod_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                                    } else {
                                        prevGlobal__candidate_pullUpMethod_edge__edge1 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_pullUpMethod_edge__edge1) ? 1U : 0U;
                                        if(prevGlobal__candidate_pullUpMethod_edge__edge1 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_pullUpMethod_edge__edge1,candidate_pullUpMethod_edge__edge1);
                                    }
                                    // Match subpatterns 
                                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                    // Pop subpattern matching task for bs
                                    openTasks.Pop();
                                    PatternAction_Bodies.releaseTask(taskFor_bs);
                                    // Check whether subpatterns were found 
                                    if(matchesList.Count>0) {
                                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                        {
                                            Rule_pullUpMethod.Match_pullUpMethod match = matches.GetNextUnfilledPosition();
                                            match._node_c1 = candidate_pullUpMethod_node_c1;
                                            match._node_c3 = candidate_pullUpMethod_node_c3;
                                            match._node_b4 = candidate_pullUpMethod_node_b4;
                                            match._node_m5 = candidate_pullUpMethod_node_m5;
                                            match._edge__edge0 = candidate_pullUpMethod_edge__edge0;
                                            match._edge_m = candidate_pullUpMethod_edge_m;
                                            match._edge__edge1 = candidate_pullUpMethod_edge__edge1;
                                            match._bs = (@Pattern_Bodies.Match_Bodies)currentFoundPartialMatch.Pop();
                                            match._bs._matchOfEnclosingPattern = match;
                                            matches.PositionWasFilledFixIt();
                                        }
                                        matchesList.Clear();
                                        // if enough matches were found, we leave
                                        if(maxMatches > 0 && matches.Count >= maxMatches)
                                        {
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_edge__edge1.lgspFlags = candidate_pullUpMethod_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                            } else { 
                                                if(prevGlobal__candidate_pullUpMethod_edge__edge1 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge1);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_edge_m.lgspFlags = candidate_pullUpMethod_edge_m.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge_m;
                                            } else { 
                                                if(prevGlobal__candidate_pullUpMethod_edge_m == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge_m);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                            } else { 
                                                if(prevGlobal__candidate_pullUpMethod_edge__edge0 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_node_m5.lgspFlags = candidate_pullUpMethod_node_m5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_m5;
                                            } else { 
                                                if(prevGlobal__candidate_pullUpMethod_node_m5 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_m5);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_node_b4.lgspFlags = candidate_pullUpMethod_node_b4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_b4;
                                            } else { 
                                                if(prevGlobal__candidate_pullUpMethod_node_b4 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_b4);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_node_c3.lgspFlags = candidate_pullUpMethod_node_c3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_c3;
                                            } else { 
                                                if(prevGlobal__candidate_pullUpMethod_node_c3 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c3);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_c1;
                                            } else { 
                                                if(prevGlobal__candidate_pullUpMethod_node_c1 == 0) {
                                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                                                }
                                            }
                                            candidate_pullUpMethod_edge__edge1.lgspFlags = candidate_pullUpMethod_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge__edge1;
                                            candidate_pullUpMethod_edge_m.lgspFlags = candidate_pullUpMethod_edge_m.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge_m;
                                            candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge__edge0;
                                            candidate_pullUpMethod_node_m5.lgspFlags = candidate_pullUpMethod_node_m5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_m5;
                                            candidate_pullUpMethod_node_b4.lgspFlags = candidate_pullUpMethod_node_b4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_b4;
                                            candidate_pullUpMethod_node_c3.lgspFlags = candidate_pullUpMethod_node_c3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_c3;
                                            candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_c1;
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                                            } else { 
                                                if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                                            } else { 
                                                if(prev__candidate_pullUpMethod_node_c1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                                                }
                                            }
                                            return matches;
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_pullUpMethod_edge__edge1.lgspFlags = candidate_pullUpMethod_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                        } else { 
                                            if(prevGlobal__candidate_pullUpMethod_edge__edge1 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge1);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_pullUpMethod_edge_m.lgspFlags = candidate_pullUpMethod_edge_m.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge_m;
                                        } else { 
                                            if(prevGlobal__candidate_pullUpMethod_edge_m == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge_m);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                        } else { 
                                            if(prevGlobal__candidate_pullUpMethod_edge__edge0 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_pullUpMethod_node_m5.lgspFlags = candidate_pullUpMethod_node_m5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_m5;
                                        } else { 
                                            if(prevGlobal__candidate_pullUpMethod_node_m5 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_m5);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_pullUpMethod_node_b4.lgspFlags = candidate_pullUpMethod_node_b4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_b4;
                                        } else { 
                                            if(prevGlobal__candidate_pullUpMethod_node_b4 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_b4);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_pullUpMethod_node_c3.lgspFlags = candidate_pullUpMethod_node_c3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_c3;
                                        } else { 
                                            if(prevGlobal__candidate_pullUpMethod_node_c3 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c3);
                                            }
                                        }
                                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                            candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_c1;
                                        } else { 
                                            if(prevGlobal__candidate_pullUpMethod_node_c1 == 0) {
                                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                                            }
                                        }
                                        candidate_pullUpMethod_edge__edge1.lgspFlags = candidate_pullUpMethod_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge__edge1;
                                        candidate_pullUpMethod_edge_m.lgspFlags = candidate_pullUpMethod_edge_m.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge_m;
                                        candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge__edge0;
                                        candidate_pullUpMethod_node_m5.lgspFlags = candidate_pullUpMethod_node_m5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_m5;
                                        candidate_pullUpMethod_node_b4.lgspFlags = candidate_pullUpMethod_node_b4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_b4;
                                        candidate_pullUpMethod_node_c3.lgspFlags = candidate_pullUpMethod_node_c3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_c3;
                                        candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_c1;
                                        continue;
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_c1;
                                    } else { 
                                        if(prevGlobal__candidate_pullUpMethod_node_c1 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_pullUpMethod_node_c3.lgspFlags = candidate_pullUpMethod_node_c3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_c3;
                                    } else { 
                                        if(prevGlobal__candidate_pullUpMethod_node_c3 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c3);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_pullUpMethod_node_b4.lgspFlags = candidate_pullUpMethod_node_b4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_b4;
                                    } else { 
                                        if(prevGlobal__candidate_pullUpMethod_node_b4 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_b4);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_pullUpMethod_node_m5.lgspFlags = candidate_pullUpMethod_node_m5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_node_m5;
                                    } else { 
                                        if(prevGlobal__candidate_pullUpMethod_node_m5 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_m5);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                    } else { 
                                        if(prevGlobal__candidate_pullUpMethod_edge__edge0 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_pullUpMethod_edge_m.lgspFlags = candidate_pullUpMethod_edge_m.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge_m;
                                    } else { 
                                        if(prevGlobal__candidate_pullUpMethod_edge_m == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge_m);
                                        }
                                    }
                                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                        candidate_pullUpMethod_edge__edge1.lgspFlags = candidate_pullUpMethod_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                    } else { 
                                        if(prevGlobal__candidate_pullUpMethod_edge__edge1 == 0) {
                                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge1);
                                        }
                                    }
                                    candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_c1;
                                    candidate_pullUpMethod_node_c3.lgspFlags = candidate_pullUpMethod_node_c3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_c3;
                                    candidate_pullUpMethod_node_b4.lgspFlags = candidate_pullUpMethod_node_b4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_b4;
                                    candidate_pullUpMethod_node_m5.lgspFlags = candidate_pullUpMethod_node_m5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_node_m5;
                                    candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge__edge0;
                                    candidate_pullUpMethod_edge_m.lgspFlags = candidate_pullUpMethod_edge_m.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge_m;
                                    candidate_pullUpMethod_edge__edge1.lgspFlags = candidate_pullUpMethod_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_pullUpMethod_edge__edge1;
                                }
                                while( (candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.lgspOutNext) != head_candidate_pullUpMethod_edge_m );
                            }
                        }
                        while( (candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.lgspOutNext) != head_candidate_pullUpMethod_edge__edge1 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_pullUpMethod_edge__edge0.lgspFlags = candidate_pullUpMethod_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                    } else { 
                        if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                        }
                    }
                }
                while( (candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.lgspOutNext) != head_candidate_pullUpMethod_edge__edge0 );
            }
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_pullUpMethod_node_c1.lgspFlags = candidate_pullUpMethod_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
            } else { 
                if(prev__candidate_pullUpMethod_node_c1 == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, pullUpMethod_node_c1, pullUpMethod_node_b4);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_pullUpMethod.IMatch_pullUpMethod match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matches)
        {
            foreach(Rule_pullUpMethod.IMatch_pullUpMethod match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, pullUpMethod_node_c1, pullUpMethod_node_b4);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, pullUpMethod_node_c1, pullUpMethod_node_b4);
            if(matches.Count <= 0) return false;
            foreach(Rule_pullUpMethod.IMatch_pullUpMethod match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, pullUpMethod_node_c1, pullUpMethod_node_b4);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, pullUpMethod_node_c1, pullUpMethod_node_b4);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, pullUpMethod_node_c1, pullUpMethod_node_b4);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IClass pullUpMethod_node_c1, GRGEN_MODEL.IMethodBody pullUpMethod_node_b4)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, pullUpMethod_node_c1, pullUpMethod_node_b4);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IClass) parameters[0], (GRGEN_MODEL.IMethodBody) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_pullUpMethod.IMatch_pullUpMethod)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph, (GRGEN_MODEL.IClass) parameters[0], (GRGEN_MODEL.IMethodBody) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IClass) parameters[0], (GRGEN_MODEL.IMethodBody) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph, (GRGEN_MODEL.IClass) parameters[0], (GRGEN_MODEL.IMethodBody) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IClass) parameters[0], (GRGEN_MODEL.IMethodBody) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IClass) parameters[0], (GRGEN_MODEL.IMethodBody) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_matchAll
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IClass matchAll_node_c1);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_matchAll.IMatch_matchAll match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IClass matchAll_node_c1);
    }
    
    public class Action_matchAll : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_matchAll
    {
        public Action_matchAll() {
            _rulePattern = Rule_matchAll.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_matchAll.Match_matchAll, Rule_matchAll.IMatch_matchAll>(this);
        }

        public Rule_matchAll _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "matchAll"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_matchAll.Match_matchAll, Rule_matchAll.IMatch_matchAll> matches;

        public static Action_matchAll Instance { get { return instance; } }
        private static Action_matchAll instance = new Action_matchAll();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IClass matchAll_node_c1)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_matchAll.Match_matchAll patternpath_match_matchAll = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset matchAll_node_c1 
            GRGEN_LGSP.LGSPNode candidate_matchAll_node_c1 = (GRGEN_LGSP.LGSPNode)matchAll_node_c1;
            if(candidate_matchAll_node_c1.lgspType.TypeID!=5) {
                return matches;
            }
            // build match of matchAll for patternpath checks
            if(patternpath_match_matchAll==null) patternpath_match_matchAll = new Rule_matchAll.Match_matchAll();
            patternpath_match_matchAll._matchOfEnclosingPattern = null;
            patternpath_match_matchAll._node_c1 = candidate_matchAll_node_c1;
            uint prevSomeGlobal__candidate_matchAll_node_c1;
            prevSomeGlobal__candidate_matchAll_node_c1 = candidate_matchAll_node_c1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            candidate_matchAll_node_c1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            // Push subpattern matching task for _sub0
            PatternAction_Subclass taskFor__sub0 = PatternAction_Subclass.getNewTask(graph, openTasks);
            taskFor__sub0.Subclass_node_sub = candidate_matchAll_node_c1;
            taskFor__sub0.searchPatternpath = false;
            taskFor__sub0.matchOfNestingPattern = patternpath_match_matchAll;
            taskFor__sub0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__sub0);
            uint prevGlobal__candidate_matchAll_node_c1;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prevGlobal__candidate_matchAll_node_c1 = candidate_matchAll_node_c1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                candidate_matchAll_node_c1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            } else {
                prevGlobal__candidate_matchAll_node_c1 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_matchAll_node_c1) ? 1U : 0U;
                if(prevGlobal__candidate_matchAll_node_c1 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_matchAll_node_c1,candidate_matchAll_node_c1);
            }
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _sub0
            openTasks.Pop();
            PatternAction_Subclass.releaseTask(taskFor__sub0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_matchAll.Match_matchAll match = matches.GetNextUnfilledPosition();
                    match._node_c1 = candidate_matchAll_node_c1;
                    match.__sub0 = (@Pattern_Subclass.Match_Subclass)currentFoundPartialMatch.Pop();
                    match.__sub0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_matchAll_node_c1.lgspFlags = candidate_matchAll_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_matchAll_node_c1;
                    } else { 
                        if(prevGlobal__candidate_matchAll_node_c1 == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_matchAll_node_c1);
                        }
                    }
                    candidate_matchAll_node_c1.lgspFlags = candidate_matchAll_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_matchAll_node_c1;
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_matchAll_node_c1.lgspFlags = candidate_matchAll_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_matchAll_node_c1;
                } else { 
                    if(prevGlobal__candidate_matchAll_node_c1 == 0) {
                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_matchAll_node_c1);
                    }
                }
                candidate_matchAll_node_c1.lgspFlags = candidate_matchAll_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_matchAll_node_c1;
                return matches;
            }
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_matchAll_node_c1.lgspFlags = candidate_matchAll_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_matchAll_node_c1;
            } else { 
                if(prevGlobal__candidate_matchAll_node_c1 == 0) {
                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_matchAll_node_c1);
                }
            }
            candidate_matchAll_node_c1.lgspFlags = candidate_matchAll_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_matchAll_node_c1;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IClass matchAll_node_c1);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IClass matchAll_node_c1)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, matchAll_node_c1);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_matchAll.IMatch_matchAll match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> matches)
        {
            foreach(Rule_matchAll.IMatch_matchAll match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, matchAll_node_c1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, matchAll_node_c1);
            if(matches.Count <= 0) return false;
            foreach(Rule_matchAll.IMatch_matchAll match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, matchAll_node_c1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IClass matchAll_node_c1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, matchAll_node_c1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, matchAll_node_c1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IClass matchAll_node_c1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, matchAll_node_c1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IClass) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_matchAll.IMatch_matchAll)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_matchAll.IMatch_matchAll>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph, (GRGEN_MODEL.IClass) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IClass) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph, (GRGEN_MODEL.IClass) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IClass) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IClass) parameters[0]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_InsertHelperEdgesForNestedLayout
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_InsertHelperEdgesForNestedLayout : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_InsertHelperEdgesForNestedLayout
    {
        public Action_InsertHelperEdgesForNestedLayout() {
            _rulePattern = Rule_InsertHelperEdgesForNestedLayout.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_InsertHelperEdgesForNestedLayout.Match_InsertHelperEdgesForNestedLayout, Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout>(this);
        }

        public Rule_InsertHelperEdgesForNestedLayout _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "InsertHelperEdgesForNestedLayout"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_InsertHelperEdgesForNestedLayout.Match_InsertHelperEdgesForNestedLayout, Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches;

        public static Action_InsertHelperEdgesForNestedLayout Instance { get { return instance; } }
        private static Action_InsertHelperEdgesForNestedLayout instance = new Action_InsertHelperEdgesForNestedLayout();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_InsertHelperEdgesForNestedLayout.Match_InsertHelperEdgesForNestedLayout match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches)
        {
            foreach(Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_InsertHelperEdgesForNestedLayout.IMatch_InsertHelperEdgesForNestedLayout>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_LinkMethodBodyToContainedEntity
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_LinkMethodBodyToContainedEntity : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_LinkMethodBodyToContainedEntity
    {
        public Action_LinkMethodBodyToContainedEntity() {
            _rulePattern = Rule_LinkMethodBodyToContainedEntity.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_LinkMethodBodyToContainedEntity.Match_LinkMethodBodyToContainedEntity, Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity>(this);
        }

        public Rule_LinkMethodBodyToContainedEntity _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "LinkMethodBodyToContainedEntity"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_LinkMethodBodyToContainedEntity.Match_LinkMethodBodyToContainedEntity, Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches;

        public static Action_LinkMethodBodyToContainedEntity Instance { get { return instance; } }
        private static Action_LinkMethodBodyToContainedEntity instance = new Action_LinkMethodBodyToContainedEntity();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup LinkMethodBodyToContainedEntity_edge__edge0 
            foreach(GRGEN_LIBGR.EdgeType type_candidate_LinkMethodBodyToContainedEntity_edge__edge0 in GRGEN_MODEL.EdgeType_contains.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_LinkMethodBodyToContainedEntity_edge__edge0 = type_candidate_LinkMethodBodyToContainedEntity_edge__edge0.TypeID;
                for(GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedEntity_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_LinkMethodBodyToContainedEntity_edge__edge0], candidate_LinkMethodBodyToContainedEntity_edge__edge0 = head_candidate_LinkMethodBodyToContainedEntity_edge__edge0.lgspTypeNext; candidate_LinkMethodBodyToContainedEntity_edge__edge0 != head_candidate_LinkMethodBodyToContainedEntity_edge__edge0; candidate_LinkMethodBodyToContainedEntity_edge__edge0 = candidate_LinkMethodBodyToContainedEntity_edge__edge0.lgspTypeNext)
                {
                    // Implicit Source LinkMethodBodyToContainedEntity_node_mb from LinkMethodBodyToContainedEntity_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedEntity_node_mb = candidate_LinkMethodBodyToContainedEntity_edge__edge0.lgspSource;
                    if(candidate_LinkMethodBodyToContainedEntity_node_mb.lgspType.TypeID!=2) {
                        continue;
                    }
                    uint prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                    prev__candidate_LinkMethodBodyToContainedEntity_node_mb = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit Target LinkMethodBodyToContainedEntity_node_e from LinkMethodBodyToContainedEntity_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedEntity_node_e = candidate_LinkMethodBodyToContainedEntity_edge__edge0.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_Entity.isMyType[candidate_LinkMethodBodyToContainedEntity_node_e.lgspType.TypeID]) {
                        candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                        continue;
                    }
                    if((candidate_LinkMethodBodyToContainedEntity_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                        continue;
                    }
                    // NegativePattern 
                    {
                        ++negLevel;
                        uint prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                        prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        if((candidate_LinkMethodBodyToContainedEntity_node_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                            --negLevel;
                            goto label0;
                        }
                        // Extend Outgoing LinkMethodBodyToContainedEntity_neg_0_edge__edge0 from LinkMethodBodyToContainedEntity_node_mb 
                        GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspOuthead;
                        if(head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0;
                            do
                            {
                                if(candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0.lgspType.TypeID!=10) {
                                    continue;
                                }
                                if(candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0.lgspTarget != candidate_LinkMethodBodyToContainedEntity_node_e) {
                                    continue;
                                }
                                // negative pattern found
                                candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                                --negLevel;
                                candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                                goto label1;
                            }
                            while( (candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0.lgspOutNext) != head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 );
                        }
                        candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                        --negLevel;
                    }
label0: ;
                    Rule_LinkMethodBodyToContainedEntity.Match_LinkMethodBodyToContainedEntity match = matches.GetNextUnfilledPosition();
                    match._node_mb = candidate_LinkMethodBodyToContainedEntity_node_mb;
                    match._node_e = candidate_LinkMethodBodyToContainedEntity_node_e;
                    match._edge__edge0 = candidate_LinkMethodBodyToContainedEntity_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(candidate_LinkMethodBodyToContainedEntity_edge__edge0);
                        candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                        return matches;
                    }
                    candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags = candidate_LinkMethodBodyToContainedEntity_node_mb.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
label1: ;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches)
        {
            foreach(Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedEntity.IMatch_LinkMethodBodyToContainedEntity>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_LinkMethodBodyToContainedExpressionTransitive
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_LinkMethodBodyToContainedExpressionTransitive : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_LinkMethodBodyToContainedExpressionTransitive
    {
        public Action_LinkMethodBodyToContainedExpressionTransitive() {
            _rulePattern = Rule_LinkMethodBodyToContainedExpressionTransitive.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_LinkMethodBodyToContainedExpressionTransitive.Match_LinkMethodBodyToContainedExpressionTransitive, Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive>(this);
        }

        public Rule_LinkMethodBodyToContainedExpressionTransitive _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "LinkMethodBodyToContainedExpressionTransitive"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_LinkMethodBodyToContainedExpressionTransitive.Match_LinkMethodBodyToContainedExpressionTransitive, Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches;

        public static Action_LinkMethodBodyToContainedExpressionTransitive Instance { get { return instance; } }
        private static Action_LinkMethodBodyToContainedExpressionTransitive instance = new Action_LinkMethodBodyToContainedExpressionTransitive();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup LinkMethodBodyToContainedExpressionTransitive_edge__edge1 
            foreach(GRGEN_LIBGR.EdgeType type_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 in GRGEN_MODEL.EdgeType_contains.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = type_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.TypeID;
                for(GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1], candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.lgspTypeNext; candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 != head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1; candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.lgspTypeNext)
                {
                    // Implicit Source LinkMethodBodyToContainedExpressionTransitive_node_e1 from LinkMethodBodyToContainedExpressionTransitive_edge__edge1 
                    GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.lgspSource;
                    if(candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspType.TypeID!=3) {
                        continue;
                    }
                    uint prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                    prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit Target LinkMethodBodyToContainedExpressionTransitive_node_e2 from LinkMethodBodyToContainedExpressionTransitive_edge__edge1 
                    GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.lgspTarget;
                    if(candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2.lgspType.TypeID!=3) {
                        candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                        continue;
                    }
                    if((candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                        continue;
                    }
                    // NegativePattern 
                    {
                        ++negLevel;
                        uint prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                        prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        if((candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                            --negLevel;
                            goto label2;
                        }
                        // Extend Outgoing LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 from LinkMethodBodyToContainedExpressionTransitive_node_e1 
                        GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspOuthead;
                        if(head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0;
                            do
                            {
                                if(candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.lgspType.TypeID!=10) {
                                    continue;
                                }
                                if(candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.lgspTarget != candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2) {
                                    continue;
                                }
                                // negative pattern found
                                candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                                --negLevel;
                                candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                                goto label3;
                            }
                            while( (candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.lgspOutNext) != head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 );
                        }
                        candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                        --negLevel;
                    }
label2: ;
                    // Extend Incoming LinkMethodBodyToContainedExpressionTransitive_edge__edge0 from LinkMethodBodyToContainedExpressionTransitive_node_e1 
                    GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspInhead;
                    if(head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0;
                        do
                        {
                            if(candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0.lgspType.TypeID!=10) {
                                continue;
                            }
                            // Implicit Source LinkMethodBodyToContainedExpressionTransitive_node_mb from LinkMethodBodyToContainedExpressionTransitive_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedExpressionTransitive_node_mb = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0.lgspSource;
                            if(candidate_LinkMethodBodyToContainedExpressionTransitive_node_mb.lgspType.TypeID!=2) {
                                continue;
                            }
                            Rule_LinkMethodBodyToContainedExpressionTransitive.Match_LinkMethodBodyToContainedExpressionTransitive match = matches.GetNextUnfilledPosition();
                            match._node_mb = candidate_LinkMethodBodyToContainedExpressionTransitive_node_mb;
                            match._node_e1 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                            match._node_e2 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2;
                            match._edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0;
                            match._edge__edge1 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1;
                            matches.PositionWasFilledFixIt();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.MoveInHeadAfter(candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0);
                                graph.MoveHeadAfter(candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1);
                                candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                                return matches;
                            }
                        }
                        while( (candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0.lgspInNext) != head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 );
                    }
                    candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
label3: ;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches)
        {
            foreach(Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_LinkMethodBodyToContainedExpressionTransitive.IMatch_LinkMethodBodyToContainedExpressionTransitive>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_RemoveMethodBodyContainsBetweenExpressions
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_RemoveMethodBodyContainsBetweenExpressions : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_RemoveMethodBodyContainsBetweenExpressions
    {
        public Action_RemoveMethodBodyContainsBetweenExpressions() {
            _rulePattern = Rule_RemoveMethodBodyContainsBetweenExpressions.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_RemoveMethodBodyContainsBetweenExpressions.Match_RemoveMethodBodyContainsBetweenExpressions, Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions>(this);
        }

        public Rule_RemoveMethodBodyContainsBetweenExpressions _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "RemoveMethodBodyContainsBetweenExpressions"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_RemoveMethodBodyContainsBetweenExpressions.Match_RemoveMethodBodyContainsBetweenExpressions, Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches;

        public static Action_RemoveMethodBodyContainsBetweenExpressions Instance { get { return instance; } }
        private static Action_RemoveMethodBodyContainsBetweenExpressions instance = new Action_RemoveMethodBodyContainsBetweenExpressions();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup RemoveMethodBodyContainsBetweenExpressions_edge_mbc 
            int type_id_candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc = 10;
            for(GRGEN_LGSP.LGSPEdge head_candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc = graph.edgesByTypeHeads[type_id_candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc], candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc = head_candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc.lgspTypeNext; candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc != head_candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc; candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc = candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc.lgspTypeNext)
            {
                // Implicit Source RemoveMethodBodyContainsBetweenExpressions_node_e1 from RemoveMethodBodyContainsBetweenExpressions_edge_mbc 
                GRGEN_LGSP.LGSPNode candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1 = candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc.lgspSource;
                if(candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspType.TypeID!=3) {
                    continue;
                }
                uint prev__candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1;
                prev__candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1 = candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target RemoveMethodBodyContainsBetweenExpressions_node_e2 from RemoveMethodBodyContainsBetweenExpressions_edge_mbc 
                GRGEN_LGSP.LGSPNode candidate_RemoveMethodBodyContainsBetweenExpressions_node_e2 = candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc.lgspTarget;
                if(candidate_RemoveMethodBodyContainsBetweenExpressions_node_e2.lgspType.TypeID!=3) {
                    candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags = candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1;
                    continue;
                }
                if((candidate_RemoveMethodBodyContainsBetweenExpressions_node_e2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags = candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1;
                    continue;
                }
                Rule_RemoveMethodBodyContainsBetweenExpressions.Match_RemoveMethodBodyContainsBetweenExpressions match = matches.GetNextUnfilledPosition();
                match._node_e1 = candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1;
                match._node_e2 = candidate_RemoveMethodBodyContainsBetweenExpressions_node_e2;
                match._edge_mbc = candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_RemoveMethodBodyContainsBetweenExpressions_edge_mbc);
                    candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags = candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1;
                    return matches;
                }
                candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags = candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RemoveMethodBodyContainsBetweenExpressions_node_e1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches)
        {
            foreach(Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_RemoveMethodBodyContainsBetweenExpressions.IMatch_RemoveMethodBodyContainsBetweenExpressions>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_RetypeClassContainment
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_RetypeClassContainment.IMatch_RetypeClassContainment match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_RetypeClassContainment : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_RetypeClassContainment
    {
        public Action_RetypeClassContainment() {
            _rulePattern = Rule_RetypeClassContainment.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_RetypeClassContainment.Match_RetypeClassContainment, Rule_RetypeClassContainment.IMatch_RetypeClassContainment>(this);
        }

        public Rule_RetypeClassContainment _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "RetypeClassContainment"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_RetypeClassContainment.Match_RetypeClassContainment, Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches;

        public static Action_RetypeClassContainment Instance { get { return instance; } }
        private static Action_RetypeClassContainment instance = new Action_RetypeClassContainment();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup RetypeClassContainment_edge_c 
            int type_id_candidate_RetypeClassContainment_edge_c = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_RetypeClassContainment_edge_c = graph.edgesByTypeHeads[type_id_candidate_RetypeClassContainment_edge_c], candidate_RetypeClassContainment_edge_c = head_candidate_RetypeClassContainment_edge_c.lgspTypeNext; candidate_RetypeClassContainment_edge_c != head_candidate_RetypeClassContainment_edge_c; candidate_RetypeClassContainment_edge_c = candidate_RetypeClassContainment_edge_c.lgspTypeNext)
            {
                // Implicit Source RetypeClassContainment_node_c1 from RetypeClassContainment_edge_c 
                GRGEN_LGSP.LGSPNode candidate_RetypeClassContainment_node_c1 = candidate_RetypeClassContainment_edge_c.lgspSource;
                if(candidate_RetypeClassContainment_node_c1.lgspType.TypeID!=5) {
                    continue;
                }
                uint prev__candidate_RetypeClassContainment_node_c1;
                prev__candidate_RetypeClassContainment_node_c1 = candidate_RetypeClassContainment_node_c1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_RetypeClassContainment_node_c1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target RetypeClassContainment_node_c2 from RetypeClassContainment_edge_c 
                GRGEN_LGSP.LGSPNode candidate_RetypeClassContainment_node_c2 = candidate_RetypeClassContainment_edge_c.lgspTarget;
                if(candidate_RetypeClassContainment_node_c2.lgspType.TypeID!=5) {
                    candidate_RetypeClassContainment_node_c1.lgspFlags = candidate_RetypeClassContainment_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RetypeClassContainment_node_c1;
                    continue;
                }
                if((candidate_RetypeClassContainment_node_c2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_RetypeClassContainment_node_c1.lgspFlags = candidate_RetypeClassContainment_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RetypeClassContainment_node_c1;
                    continue;
                }
                Rule_RetypeClassContainment.Match_RetypeClassContainment match = matches.GetNextUnfilledPosition();
                match._node_c1 = candidate_RetypeClassContainment_node_c1;
                match._node_c2 = candidate_RetypeClassContainment_node_c2;
                match._edge_c = candidate_RetypeClassContainment_edge_c;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_RetypeClassContainment_edge_c);
                    candidate_RetypeClassContainment_node_c1.lgspFlags = candidate_RetypeClassContainment_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RetypeClassContainment_node_c1;
                    return matches;
                }
                candidate_RetypeClassContainment_node_c1.lgspFlags = candidate_RetypeClassContainment_node_c1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_RetypeClassContainment_node_c1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_RetypeClassContainment.IMatch_RetypeClassContainment match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches)
        {
            foreach(Rule_RetypeClassContainment.IMatch_RetypeClassContainment match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_RetypeClassContainment.IMatch_RetypeClassContainment match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_RetypeClassContainment.IMatch_RetypeClassContainment)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_RetypeClassContainment.IMatch_RetypeClassContainment>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }


    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class ProgramGraphsOriginalActions : GRGEN_LGSP.LGSPActions
    {
        public ProgramGraphsOriginalActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public ProgramGraphsOriginalActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Subclasses.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Subclass.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Features.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_FeaturePattern.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Parameters.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Parameter.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Statements.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Statement.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Expressions.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_ExpressionPattern.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Bodies.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_Body.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_createProgramGraphExample.Instance);
            actions.Add("createProgramGraphExample", (GRGEN_LGSP.LGSPAction) Action_createProgramGraphExample.Instance);
            @createProgramGraphExample = Action_createProgramGraphExample.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_createProgramGraphPullUp.Instance);
            actions.Add("createProgramGraphPullUp", (GRGEN_LGSP.LGSPAction) Action_createProgramGraphPullUp.Instance);
            @createProgramGraphPullUp = Action_createProgramGraphPullUp.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_pullUpMethod.Instance);
            actions.Add("pullUpMethod", (GRGEN_LGSP.LGSPAction) Action_pullUpMethod.Instance);
            @pullUpMethod = Action_pullUpMethod.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_matchAll.Instance);
            actions.Add("matchAll", (GRGEN_LGSP.LGSPAction) Action_matchAll.Instance);
            @matchAll = Action_matchAll.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_InsertHelperEdgesForNestedLayout.Instance);
            actions.Add("InsertHelperEdgesForNestedLayout", (GRGEN_LGSP.LGSPAction) Action_InsertHelperEdgesForNestedLayout.Instance);
            @InsertHelperEdgesForNestedLayout = Action_InsertHelperEdgesForNestedLayout.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_LinkMethodBodyToContainedEntity.Instance);
            actions.Add("LinkMethodBodyToContainedEntity", (GRGEN_LGSP.LGSPAction) Action_LinkMethodBodyToContainedEntity.Instance);
            @LinkMethodBodyToContainedEntity = Action_LinkMethodBodyToContainedEntity.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_LinkMethodBodyToContainedExpressionTransitive.Instance);
            actions.Add("LinkMethodBodyToContainedExpressionTransitive", (GRGEN_LGSP.LGSPAction) Action_LinkMethodBodyToContainedExpressionTransitive.Instance);
            @LinkMethodBodyToContainedExpressionTransitive = Action_LinkMethodBodyToContainedExpressionTransitive.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_RemoveMethodBodyContainsBetweenExpressions.Instance);
            actions.Add("RemoveMethodBodyContainsBetweenExpressions", (GRGEN_LGSP.LGSPAction) Action_RemoveMethodBodyContainsBetweenExpressions.Instance);
            @RemoveMethodBodyContainsBetweenExpressions = Action_RemoveMethodBodyContainsBetweenExpressions.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_RetypeClassContainment.Instance);
            actions.Add("RetypeClassContainment", (GRGEN_LGSP.LGSPAction) Action_RetypeClassContainment.Instance);
            @RetypeClassContainment = Action_RetypeClassContainment.Instance;
            analyzer.ComputeInterPatternRelations();
        }
        
        public IAction_createProgramGraphExample @createProgramGraphExample;
        public IAction_createProgramGraphPullUp @createProgramGraphPullUp;
        public IAction_pullUpMethod @pullUpMethod;
        public IAction_matchAll @matchAll;
        public IAction_InsertHelperEdgesForNestedLayout @InsertHelperEdgesForNestedLayout;
        public IAction_LinkMethodBodyToContainedEntity @LinkMethodBodyToContainedEntity;
        public IAction_LinkMethodBodyToContainedExpressionTransitive @LinkMethodBodyToContainedExpressionTransitive;
        public IAction_RemoveMethodBodyContainsBetweenExpressions @RemoveMethodBodyContainsBetweenExpressions;
        public IAction_RetypeClassContainment @RetypeClassContainment;
        
        public override string Name { get { return "ProgramGraphsOriginalActions"; } }
        public override string ModelMD5Hash { get { return "367e59eecca3afadcdac0347954e2ede"; } }
    }
}