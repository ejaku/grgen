// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Turing3\Turing3.grg" on Mon Nov 02 15:03:52 CET 2009

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Turing3;

namespace de.unika.ipd.grGen.Action_Turing3
{
	public class Rule_readZeroRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_readZeroRule instance = null;
		public static Rule_readZeroRule Instance { get { if (instance==null) { instance = new Rule_readZeroRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] readZeroRule_node_s_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] readZeroRule_node_wv_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] readZeroRule_node_bp_AllowedTypes = null;
		public static bool[] readZeroRule_node_s_IsAllowedType = null;
		public static bool[] readZeroRule_node_wv_IsAllowedType = null;
		public static bool[] readZeroRule_node_bp_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] readZeroRule_edge_rv_AllowedTypes = null;
		public static bool[] readZeroRule_edge_rv_IsAllowedType = null;
		public enum readZeroRule_NodeNums { @s, @wv, @bp, };
		public enum readZeroRule_EdgeNums { @rv, };
		public enum readZeroRule_VariableNums { };
		public enum readZeroRule_SubNums { };
		public enum readZeroRule_AltNums { };
		public enum readZeroRule_IterNums { };



		GRGEN_LGSP.PatternGraph pat_readZeroRule;


		private Rule_readZeroRule()
		{
			name = "readZeroRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
			inputNames = new string[] { "readZeroRule_node_s", "readZeroRule_node_bp", };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, };
		}
		private void initialize()
		{
			bool[,] readZeroRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] readZeroRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] readZeroRule_minMatches = new int[0] ;
			int[] readZeroRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode readZeroRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "readZeroRule_node_s", "s", readZeroRule_node_s_AllowedTypes, readZeroRule_node_s_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode readZeroRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "readZeroRule_node_wv", "wv", readZeroRule_node_wv_AllowedTypes, readZeroRule_node_wv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode readZeroRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "readZeroRule_node_bp", "bp", readZeroRule_node_bp_AllowedTypes, readZeroRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternEdge readZeroRule_edge_rv = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@readZero, "GRGEN_MODEL.IreadZero", "readZeroRule_edge_rv", "rv", readZeroRule_edge_rv_AllowedTypes, readZeroRule_edge_rv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", "readZeroRule_node_bp", "value"), new GRGEN_EXPR.Constant("0")),
				new string[] { "readZeroRule_node_bp" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_readZeroRule = new GRGEN_LGSP.PatternGraph(
				"readZeroRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { readZeroRule_node_s, readZeroRule_node_wv, readZeroRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { readZeroRule_edge_rv }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				readZeroRule_minMatches,
				readZeroRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { cond_0,  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				readZeroRule_isNodeHomomorphicGlobal,
				readZeroRule_isEdgeHomomorphicGlobal
			);
			pat_readZeroRule.edgeToSourceNode.Add(readZeroRule_edge_rv, readZeroRule_node_s);
			pat_readZeroRule.edgeToTargetNode.Add(readZeroRule_edge_rv, readZeroRule_node_wv);

			readZeroRule_node_s.PointOfDefinition = null;
			readZeroRule_node_wv.PointOfDefinition = pat_readZeroRule;
			readZeroRule_node_bp.PointOfDefinition = null;
			readZeroRule_edge_rv.PointOfDefinition = pat_readZeroRule;

			patternGraph = pat_readZeroRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IWriteValue output_0)
		{
			Match_readZeroRule curMatch = (Match_readZeroRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_wv = curMatch._node_wv;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			GRGEN_MODEL.IBandPosition inode_bp = curMatch.node_bp;
			GRGEN_MODEL.IWriteValue inode_wv = curMatch.node_wv;
			graph.SettingAddedNodeNames( readZeroRule_addedNodeNames );
			graph.SettingAddedEdgeNames( readZeroRule_addedEdgeNames );
			int tempvar_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_i, null);
			inode_bp.@value = tempvar_i;
			output_0 = (GRGEN_MODEL.IWriteValue)(node_wv);
			return;
		}
		private static string[] readZeroRule_addedNodeNames = new string[] {  };
		private static string[] readZeroRule_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IWriteValue output_0)
		{
			Match_readZeroRule curMatch = (Match_readZeroRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_wv = curMatch._node_wv;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			GRGEN_MODEL.IBandPosition inode_bp = curMatch.node_bp;
			GRGEN_MODEL.IWriteValue inode_wv = curMatch.node_wv;
			graph.SettingAddedNodeNames( readZeroRule_addedNodeNames );
			graph.SettingAddedEdgeNames( readZeroRule_addedEdgeNames );
			int tempvar_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_i, null);
			inode_bp.@value = tempvar_i;
			output_0 = (GRGEN_MODEL.IWriteValue)(node_wv);
			return;
		}

		static Rule_readZeroRule() {
		}

		public interface IMatch_readZeroRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IState node_s { get; }
			GRGEN_MODEL.IWriteValue node_wv { get; }
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			GRGEN_MODEL.IreadZero edge_rv { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_readZeroRule : GRGEN_LGSP.ListElement<Match_readZeroRule>, IMatch_readZeroRule
		{
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } }
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum readZeroRule_NodeNums { @s, @wv, @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)readZeroRule_NodeNums.@s: return _node_s;
				case (int)readZeroRule_NodeNums.@wv: return _node_wv;
				case (int)readZeroRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IreadZero edge_rv { get { return (GRGEN_MODEL.IreadZero)_edge_rv; } }
			public GRGEN_LGSP.LGSPEdge _edge_rv;
			public enum readZeroRule_EdgeNums { @rv, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)readZeroRule_EdgeNums.@rv: return _edge_rv;
				default: return null;
				}
			}
			
			public enum readZeroRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readZeroRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readZeroRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readZeroRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readZeroRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_readZeroRule.instance.pat_readZeroRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_readOneRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_readOneRule instance = null;
		public static Rule_readOneRule Instance { get { if (instance==null) { instance = new Rule_readOneRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] readOneRule_node_s_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] readOneRule_node_wv_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] readOneRule_node_bp_AllowedTypes = null;
		public static bool[] readOneRule_node_s_IsAllowedType = null;
		public static bool[] readOneRule_node_wv_IsAllowedType = null;
		public static bool[] readOneRule_node_bp_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] readOneRule_edge_rv_AllowedTypes = null;
		public static bool[] readOneRule_edge_rv_IsAllowedType = null;
		public enum readOneRule_NodeNums { @s, @wv, @bp, };
		public enum readOneRule_EdgeNums { @rv, };
		public enum readOneRule_VariableNums { };
		public enum readOneRule_SubNums { };
		public enum readOneRule_AltNums { };
		public enum readOneRule_IterNums { };



		GRGEN_LGSP.PatternGraph pat_readOneRule;


		private Rule_readOneRule()
		{
			name = "readOneRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
			inputNames = new string[] { "readOneRule_node_s", "readOneRule_node_bp", };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, };
		}
		private void initialize()
		{
			bool[,] readOneRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] readOneRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] readOneRule_minMatches = new int[0] ;
			int[] readOneRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode readOneRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "readOneRule_node_s", "s", readOneRule_node_s_AllowedTypes, readOneRule_node_s_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode readOneRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "readOneRule_node_wv", "wv", readOneRule_node_wv_AllowedTypes, readOneRule_node_wv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode readOneRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "readOneRule_node_bp", "bp", readOneRule_node_bp_AllowedTypes, readOneRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternEdge readOneRule_edge_rv = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@readOne, "GRGEN_MODEL.IreadOne", "readOneRule_edge_rv", "rv", readOneRule_edge_rv_AllowedTypes, readOneRule_edge_rv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", "readOneRule_node_bp", "value"), new GRGEN_EXPR.Constant("1")),
				new string[] { "readOneRule_node_bp" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_readOneRule = new GRGEN_LGSP.PatternGraph(
				"readOneRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { readOneRule_node_s, readOneRule_node_wv, readOneRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { readOneRule_edge_rv }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				readOneRule_minMatches,
				readOneRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { cond_0,  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				readOneRule_isNodeHomomorphicGlobal,
				readOneRule_isEdgeHomomorphicGlobal
			);
			pat_readOneRule.edgeToSourceNode.Add(readOneRule_edge_rv, readOneRule_node_s);
			pat_readOneRule.edgeToTargetNode.Add(readOneRule_edge_rv, readOneRule_node_wv);

			readOneRule_node_s.PointOfDefinition = null;
			readOneRule_node_wv.PointOfDefinition = pat_readOneRule;
			readOneRule_node_bp.PointOfDefinition = null;
			readOneRule_edge_rv.PointOfDefinition = pat_readOneRule;

			patternGraph = pat_readOneRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IWriteValue output_0)
		{
			Match_readOneRule curMatch = (Match_readOneRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_wv = curMatch._node_wv;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			GRGEN_MODEL.IBandPosition inode_bp = curMatch.node_bp;
			GRGEN_MODEL.IWriteValue inode_wv = curMatch.node_wv;
			graph.SettingAddedNodeNames( readOneRule_addedNodeNames );
			graph.SettingAddedEdgeNames( readOneRule_addedEdgeNames );
			int tempvar_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_i, null);
			inode_bp.@value = tempvar_i;
			output_0 = (GRGEN_MODEL.IWriteValue)(node_wv);
			return;
		}
		private static string[] readOneRule_addedNodeNames = new string[] {  };
		private static string[] readOneRule_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IWriteValue output_0)
		{
			Match_readOneRule curMatch = (Match_readOneRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_wv = curMatch._node_wv;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			GRGEN_MODEL.IBandPosition inode_bp = curMatch.node_bp;
			GRGEN_MODEL.IWriteValue inode_wv = curMatch.node_wv;
			graph.SettingAddedNodeNames( readOneRule_addedNodeNames );
			graph.SettingAddedEdgeNames( readOneRule_addedEdgeNames );
			int tempvar_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_i, null);
			inode_bp.@value = tempvar_i;
			output_0 = (GRGEN_MODEL.IWriteValue)(node_wv);
			return;
		}

		static Rule_readOneRule() {
		}

		public interface IMatch_readOneRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IState node_s { get; }
			GRGEN_MODEL.IWriteValue node_wv { get; }
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			GRGEN_MODEL.IreadOne edge_rv { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_readOneRule : GRGEN_LGSP.ListElement<Match_readOneRule>, IMatch_readOneRule
		{
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } }
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum readOneRule_NodeNums { @s, @wv, @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)readOneRule_NodeNums.@s: return _node_s;
				case (int)readOneRule_NodeNums.@wv: return _node_wv;
				case (int)readOneRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IreadOne edge_rv { get { return (GRGEN_MODEL.IreadOne)_edge_rv; } }
			public GRGEN_LGSP.LGSPEdge _edge_rv;
			public enum readOneRule_EdgeNums { @rv, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)readOneRule_EdgeNums.@rv: return _edge_rv;
				default: return null;
				}
			}
			
			public enum readOneRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readOneRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readOneRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readOneRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum readOneRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_readOneRule.instance.pat_readOneRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_ensureMoveLeftValidRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ensureMoveLeftValidRule instance = null;
		public static Rule_ensureMoveLeftValidRule Instance { get { if (instance==null) { instance = new Rule_ensureMoveLeftValidRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ensureMoveLeftValidRule_node_wv_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ensureMoveLeftValidRule_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ensureMoveLeftValidRule_node_bp_AllowedTypes = null;
		public static bool[] ensureMoveLeftValidRule_node_wv_IsAllowedType = null;
		public static bool[] ensureMoveLeftValidRule_node__node0_IsAllowedType = null;
		public static bool[] ensureMoveLeftValidRule_node_bp_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ensureMoveLeftValidRule_edge__edge0_AllowedTypes = null;
		public static bool[] ensureMoveLeftValidRule_edge__edge0_IsAllowedType = null;
		public enum ensureMoveLeftValidRule_NodeNums { @wv, @_node0, @bp, };
		public enum ensureMoveLeftValidRule_EdgeNums { @_edge0, };
		public enum ensureMoveLeftValidRule_VariableNums { };
		public enum ensureMoveLeftValidRule_SubNums { };
		public enum ensureMoveLeftValidRule_AltNums { };
		public enum ensureMoveLeftValidRule_IterNums { };



		GRGEN_LGSP.PatternGraph pat_ensureMoveLeftValidRule;

		public static GRGEN_LIBGR.NodeType[] ensureMoveLeftValidRule_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ensureMoveLeftValidRule_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ensureMoveLeftValidRule_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ensureMoveLeftValidRule_neg_0_edge__edge0_IsAllowedType = null;
		public enum ensureMoveLeftValidRule_neg_0_NodeNums { @_node0, @bp, };
		public enum ensureMoveLeftValidRule_neg_0_EdgeNums { @_edge0, };
		public enum ensureMoveLeftValidRule_neg_0_VariableNums { };
		public enum ensureMoveLeftValidRule_neg_0_SubNums { };
		public enum ensureMoveLeftValidRule_neg_0_AltNums { };
		public enum ensureMoveLeftValidRule_neg_0_IterNums { };

		GRGEN_LGSP.PatternGraph ensureMoveLeftValidRule_neg_0;


		private Rule_ensureMoveLeftValidRule()
		{
			name = "ensureMoveLeftValidRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
			inputNames = new string[] { "ensureMoveLeftValidRule_node_wv", "ensureMoveLeftValidRule_node_bp", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] ensureMoveLeftValidRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ensureMoveLeftValidRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ensureMoveLeftValidRule_minMatches = new int[0] ;
			int[] ensureMoveLeftValidRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "ensureMoveLeftValidRule_node_wv", "wv", ensureMoveLeftValidRule_node_wv_AllowedTypes, ensureMoveLeftValidRule_node_wv_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "ensureMoveLeftValidRule_node__node0", "_node0", ensureMoveLeftValidRule_node__node0_AllowedTypes, ensureMoveLeftValidRule_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "ensureMoveLeftValidRule_node_bp", "bp", ensureMoveLeftValidRule_node_bp_AllowedTypes, ensureMoveLeftValidRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternEdge ensureMoveLeftValidRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveLeft, "GRGEN_MODEL.ImoveLeft", "ensureMoveLeftValidRule_edge__edge0", "_edge0", ensureMoveLeftValidRule_edge__edge0_AllowedTypes, ensureMoveLeftValidRule_edge__edge0_IsAllowedType, 5.5F, -1);
			bool[,] ensureMoveLeftValidRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ensureMoveLeftValidRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ensureMoveLeftValidRule_neg_0_minMatches = new int[0] ;
			int[] ensureMoveLeftValidRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "ensureMoveLeftValidRule_neg_0_node__node0", "_node0", ensureMoveLeftValidRule_neg_0_node__node0_AllowedTypes, ensureMoveLeftValidRule_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ensureMoveLeftValidRule_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, "GRGEN_MODEL.Iright", "ensureMoveLeftValidRule_neg_0_edge__edge0", "_edge0", ensureMoveLeftValidRule_neg_0_edge__edge0_AllowedTypes, ensureMoveLeftValidRule_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			ensureMoveLeftValidRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ensureMoveLeftValidRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveLeftValidRule_neg_0_node__node0, ensureMoveLeftValidRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveLeftValidRule_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ensureMoveLeftValidRule_neg_0_minMatches,
				ensureMoveLeftValidRule_neg_0_maxMatches,
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
				ensureMoveLeftValidRule_neg_0_isNodeHomomorphicGlobal,
				ensureMoveLeftValidRule_neg_0_isEdgeHomomorphicGlobal
			);
			ensureMoveLeftValidRule_neg_0.edgeToSourceNode.Add(ensureMoveLeftValidRule_neg_0_edge__edge0, ensureMoveLeftValidRule_neg_0_node__node0);
			ensureMoveLeftValidRule_neg_0.edgeToTargetNode.Add(ensureMoveLeftValidRule_neg_0_edge__edge0, ensureMoveLeftValidRule_node_bp);

			pat_ensureMoveLeftValidRule = new GRGEN_LGSP.PatternGraph(
				"ensureMoveLeftValidRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node__node0, ensureMoveLeftValidRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveLeftValidRule_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ensureMoveLeftValidRule_minMatches,
				ensureMoveLeftValidRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { ensureMoveLeftValidRule_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ensureMoveLeftValidRule_isNodeHomomorphicGlobal,
				ensureMoveLeftValidRule_isEdgeHomomorphicGlobal
			);
			pat_ensureMoveLeftValidRule.edgeToSourceNode.Add(ensureMoveLeftValidRule_edge__edge0, ensureMoveLeftValidRule_node_wv);
			pat_ensureMoveLeftValidRule.edgeToTargetNode.Add(ensureMoveLeftValidRule_edge__edge0, ensureMoveLeftValidRule_node__node0);
			ensureMoveLeftValidRule_neg_0.embeddingGraph = pat_ensureMoveLeftValidRule;

			ensureMoveLeftValidRule_node_wv.PointOfDefinition = null;
			ensureMoveLeftValidRule_node__node0.PointOfDefinition = pat_ensureMoveLeftValidRule;
			ensureMoveLeftValidRule_node_bp.PointOfDefinition = null;
			ensureMoveLeftValidRule_edge__edge0.PointOfDefinition = pat_ensureMoveLeftValidRule;
			ensureMoveLeftValidRule_neg_0_node__node0.PointOfDefinition = ensureMoveLeftValidRule_neg_0;
			ensureMoveLeftValidRule_neg_0_edge__edge0.PointOfDefinition = ensureMoveLeftValidRule_neg_0;

			patternGraph = pat_ensureMoveLeftValidRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveLeftValidRule curMatch = (Match_ensureMoveLeftValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveLeftValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveLeftValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node__node1, node_bp);
			return;
		}
		private static string[] ensureMoveLeftValidRule_addedNodeNames = new string[] { "_node1" };
		private static string[] ensureMoveLeftValidRule_addedEdgeNames = new string[] { "_edge1" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveLeftValidRule curMatch = (Match_ensureMoveLeftValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveLeftValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveLeftValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node__node1, node_bp);
			return;
		}

		static Rule_ensureMoveLeftValidRule() {
		}

		public interface IMatch_ensureMoveLeftValidRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; }
			GRGEN_MODEL.IState node__node0 { get; }
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			GRGEN_MODEL.ImoveLeft edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ensureMoveLeftValidRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IBandPosition node__node0 { get; }
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			GRGEN_MODEL.Iright edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ensureMoveLeftValidRule : GRGEN_LGSP.ListElement<Match_ensureMoveLeftValidRule>, IMatch_ensureMoveLeftValidRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } }
			public GRGEN_MODEL.IState node__node0 { get { return (GRGEN_MODEL.IState)_node__node0; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum ensureMoveLeftValidRule_NodeNums { @wv, @_node0, @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_NodeNums.@wv: return _node_wv;
				case (int)ensureMoveLeftValidRule_NodeNums.@_node0: return _node__node0;
				case (int)ensureMoveLeftValidRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImoveLeft edge__edge0 { get { return (GRGEN_MODEL.ImoveLeft)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveLeftValidRule_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveLeftValidRule.instance.pat_ensureMoveLeftValidRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ensureMoveLeftValidRule_neg_0 : GRGEN_LGSP.ListElement<Match_ensureMoveLeftValidRule_neg_0>, IMatch_ensureMoveLeftValidRule_neg_0
		{
			public GRGEN_MODEL.IBandPosition node__node0 { get { return (GRGEN_MODEL.IBandPosition)_node__node0; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum ensureMoveLeftValidRule_neg_0_NodeNums { @_node0, @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_neg_0_NodeNums.@_node0: return _node__node0;
				case (int)ensureMoveLeftValidRule_neg_0_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iright edge__edge0 { get { return (GRGEN_MODEL.Iright)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveLeftValidRule_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveLeftValidRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveLeftValidRule.instance.ensureMoveLeftValidRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_ensureMoveRightValidRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ensureMoveRightValidRule instance = null;
		public static Rule_ensureMoveRightValidRule Instance { get { if (instance==null) { instance = new Rule_ensureMoveRightValidRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ensureMoveRightValidRule_node_wv_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ensureMoveRightValidRule_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ensureMoveRightValidRule_node_bp_AllowedTypes = null;
		public static bool[] ensureMoveRightValidRule_node_wv_IsAllowedType = null;
		public static bool[] ensureMoveRightValidRule_node__node0_IsAllowedType = null;
		public static bool[] ensureMoveRightValidRule_node_bp_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ensureMoveRightValidRule_edge__edge0_AllowedTypes = null;
		public static bool[] ensureMoveRightValidRule_edge__edge0_IsAllowedType = null;
		public enum ensureMoveRightValidRule_NodeNums { @wv, @_node0, @bp, };
		public enum ensureMoveRightValidRule_EdgeNums { @_edge0, };
		public enum ensureMoveRightValidRule_VariableNums { };
		public enum ensureMoveRightValidRule_SubNums { };
		public enum ensureMoveRightValidRule_AltNums { };
		public enum ensureMoveRightValidRule_IterNums { };



		GRGEN_LGSP.PatternGraph pat_ensureMoveRightValidRule;

		public static GRGEN_LIBGR.NodeType[] ensureMoveRightValidRule_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ensureMoveRightValidRule_neg_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ensureMoveRightValidRule_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ensureMoveRightValidRule_neg_0_edge__edge0_IsAllowedType = null;
		public enum ensureMoveRightValidRule_neg_0_NodeNums { @bp, @_node0, };
		public enum ensureMoveRightValidRule_neg_0_EdgeNums { @_edge0, };
		public enum ensureMoveRightValidRule_neg_0_VariableNums { };
		public enum ensureMoveRightValidRule_neg_0_SubNums { };
		public enum ensureMoveRightValidRule_neg_0_AltNums { };
		public enum ensureMoveRightValidRule_neg_0_IterNums { };

		GRGEN_LGSP.PatternGraph ensureMoveRightValidRule_neg_0;


		private Rule_ensureMoveRightValidRule()
		{
			name = "ensureMoveRightValidRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
			inputNames = new string[] { "ensureMoveRightValidRule_node_wv", "ensureMoveRightValidRule_node_bp", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] ensureMoveRightValidRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ensureMoveRightValidRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ensureMoveRightValidRule_minMatches = new int[0] ;
			int[] ensureMoveRightValidRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "ensureMoveRightValidRule_node_wv", "wv", ensureMoveRightValidRule_node_wv_AllowedTypes, ensureMoveRightValidRule_node_wv_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "ensureMoveRightValidRule_node__node0", "_node0", ensureMoveRightValidRule_node__node0_AllowedTypes, ensureMoveRightValidRule_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "ensureMoveRightValidRule_node_bp", "bp", ensureMoveRightValidRule_node_bp_AllowedTypes, ensureMoveRightValidRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternEdge ensureMoveRightValidRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveRight, "GRGEN_MODEL.ImoveRight", "ensureMoveRightValidRule_edge__edge0", "_edge0", ensureMoveRightValidRule_edge__edge0_AllowedTypes, ensureMoveRightValidRule_edge__edge0_IsAllowedType, 5.5F, -1);
			bool[,] ensureMoveRightValidRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ensureMoveRightValidRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ensureMoveRightValidRule_neg_0_minMatches = new int[0] ;
			int[] ensureMoveRightValidRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "ensureMoveRightValidRule_neg_0_node__node0", "_node0", ensureMoveRightValidRule_neg_0_node__node0_AllowedTypes, ensureMoveRightValidRule_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ensureMoveRightValidRule_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, "GRGEN_MODEL.Iright", "ensureMoveRightValidRule_neg_0_edge__edge0", "_edge0", ensureMoveRightValidRule_neg_0_edge__edge0_AllowedTypes, ensureMoveRightValidRule_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			ensureMoveRightValidRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ensureMoveRightValidRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveRightValidRule_node_bp, ensureMoveRightValidRule_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveRightValidRule_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ensureMoveRightValidRule_neg_0_minMatches,
				ensureMoveRightValidRule_neg_0_maxMatches,
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
				ensureMoveRightValidRule_neg_0_isNodeHomomorphicGlobal,
				ensureMoveRightValidRule_neg_0_isEdgeHomomorphicGlobal
			);
			ensureMoveRightValidRule_neg_0.edgeToSourceNode.Add(ensureMoveRightValidRule_neg_0_edge__edge0, ensureMoveRightValidRule_node_bp);
			ensureMoveRightValidRule_neg_0.edgeToTargetNode.Add(ensureMoveRightValidRule_neg_0_edge__edge0, ensureMoveRightValidRule_neg_0_node__node0);

			pat_ensureMoveRightValidRule = new GRGEN_LGSP.PatternGraph(
				"ensureMoveRightValidRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node__node0, ensureMoveRightValidRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveRightValidRule_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ensureMoveRightValidRule_minMatches,
				ensureMoveRightValidRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { ensureMoveRightValidRule_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ensureMoveRightValidRule_isNodeHomomorphicGlobal,
				ensureMoveRightValidRule_isEdgeHomomorphicGlobal
			);
			pat_ensureMoveRightValidRule.edgeToSourceNode.Add(ensureMoveRightValidRule_edge__edge0, ensureMoveRightValidRule_node_wv);
			pat_ensureMoveRightValidRule.edgeToTargetNode.Add(ensureMoveRightValidRule_edge__edge0, ensureMoveRightValidRule_node__node0);
			ensureMoveRightValidRule_neg_0.embeddingGraph = pat_ensureMoveRightValidRule;

			ensureMoveRightValidRule_node_wv.PointOfDefinition = null;
			ensureMoveRightValidRule_node__node0.PointOfDefinition = pat_ensureMoveRightValidRule;
			ensureMoveRightValidRule_node_bp.PointOfDefinition = null;
			ensureMoveRightValidRule_edge__edge0.PointOfDefinition = pat_ensureMoveRightValidRule;
			ensureMoveRightValidRule_neg_0_node__node0.PointOfDefinition = ensureMoveRightValidRule_neg_0;
			ensureMoveRightValidRule_neg_0_edge__edge0.PointOfDefinition = ensureMoveRightValidRule_neg_0;

			patternGraph = pat_ensureMoveRightValidRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveRightValidRule curMatch = (Match_ensureMoveRightValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveRightValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveRightValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node_bp, node__node1);
			return;
		}
		private static string[] ensureMoveRightValidRule_addedNodeNames = new string[] { "_node1" };
		private static string[] ensureMoveRightValidRule_addedEdgeNames = new string[] { "_edge1" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveRightValidRule curMatch = (Match_ensureMoveRightValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveRightValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveRightValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node_bp, node__node1);
			return;
		}

		static Rule_ensureMoveRightValidRule() {
		}

		public interface IMatch_ensureMoveRightValidRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; }
			GRGEN_MODEL.IState node__node0 { get; }
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			GRGEN_MODEL.ImoveRight edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ensureMoveRightValidRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IBandPosition node_bp { get; }
			GRGEN_MODEL.IBandPosition node__node0 { get; }
			//Edges
			GRGEN_MODEL.Iright edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ensureMoveRightValidRule : GRGEN_LGSP.ListElement<Match_ensureMoveRightValidRule>, IMatch_ensureMoveRightValidRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } }
			public GRGEN_MODEL.IState node__node0 { get { return (GRGEN_MODEL.IState)_node__node0; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum ensureMoveRightValidRule_NodeNums { @wv, @_node0, @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_NodeNums.@wv: return _node_wv;
				case (int)ensureMoveRightValidRule_NodeNums.@_node0: return _node__node0;
				case (int)ensureMoveRightValidRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImoveRight edge__edge0 { get { return (GRGEN_MODEL.ImoveRight)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveRightValidRule_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveRightValidRule.instance.pat_ensureMoveRightValidRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ensureMoveRightValidRule_neg_0 : GRGEN_LGSP.ListElement<Match_ensureMoveRightValidRule_neg_0>, IMatch_ensureMoveRightValidRule_neg_0
		{
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_MODEL.IBandPosition node__node0 { get { return (GRGEN_MODEL.IBandPosition)_node__node0; } }
			public GRGEN_LGSP.LGSPNode _node_bp;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum ensureMoveRightValidRule_neg_0_NodeNums { @bp, @_node0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_neg_0_NodeNums.@bp: return _node_bp;
				case (int)ensureMoveRightValidRule_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iright edge__edge0 { get { return (GRGEN_MODEL.Iright)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveRightValidRule_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ensureMoveRightValidRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveRightValidRule.instance.ensureMoveRightValidRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_moveLeftRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_moveLeftRule instance = null;
		public static Rule_moveLeftRule Instance { get { if (instance==null) { instance = new Rule_moveLeftRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] moveLeftRule_node_wv_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] moveLeftRule_node_s_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] moveLeftRule_node_lbp_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] moveLeftRule_node_bp_AllowedTypes = null;
		public static bool[] moveLeftRule_node_wv_IsAllowedType = null;
		public static bool[] moveLeftRule_node_s_IsAllowedType = null;
		public static bool[] moveLeftRule_node_lbp_IsAllowedType = null;
		public static bool[] moveLeftRule_node_bp_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] moveLeftRule_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] moveLeftRule_edge__edge1_AllowedTypes = null;
		public static bool[] moveLeftRule_edge__edge0_IsAllowedType = null;
		public static bool[] moveLeftRule_edge__edge1_IsAllowedType = null;
		public enum moveLeftRule_NodeNums { @wv, @s, @lbp, @bp, };
		public enum moveLeftRule_EdgeNums { @_edge0, @_edge1, };
		public enum moveLeftRule_VariableNums { };
		public enum moveLeftRule_SubNums { };
		public enum moveLeftRule_AltNums { };
		public enum moveLeftRule_IterNums { };



		GRGEN_LGSP.PatternGraph pat_moveLeftRule;


		private Rule_moveLeftRule()
		{
			name = "moveLeftRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
			inputNames = new string[] { "moveLeftRule_node_wv", "moveLeftRule_node_bp", };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
		}
		private void initialize()
		{
			bool[,] moveLeftRule_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] moveLeftRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] moveLeftRule_minMatches = new int[0] ;
			int[] moveLeftRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode moveLeftRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "moveLeftRule_node_wv", "wv", moveLeftRule_node_wv_AllowedTypes, moveLeftRule_node_wv_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode moveLeftRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "moveLeftRule_node_s", "s", moveLeftRule_node_s_AllowedTypes, moveLeftRule_node_s_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode moveLeftRule_node_lbp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "moveLeftRule_node_lbp", "lbp", moveLeftRule_node_lbp_AllowedTypes, moveLeftRule_node_lbp_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode moveLeftRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "moveLeftRule_node_bp", "bp", moveLeftRule_node_bp_AllowedTypes, moveLeftRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternEdge moveLeftRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveLeft, "GRGEN_MODEL.ImoveLeft", "moveLeftRule_edge__edge0", "_edge0", moveLeftRule_edge__edge0_AllowedTypes, moveLeftRule_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge moveLeftRule_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, "GRGEN_MODEL.Iright", "moveLeftRule_edge__edge1", "_edge1", moveLeftRule_edge__edge1_AllowedTypes, moveLeftRule_edge__edge1_IsAllowedType, 5.5F, -1);
			pat_moveLeftRule = new GRGEN_LGSP.PatternGraph(
				"moveLeftRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { moveLeftRule_node_wv, moveLeftRule_node_s, moveLeftRule_node_lbp, moveLeftRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { moveLeftRule_edge__edge0, moveLeftRule_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				moveLeftRule_minMatches,
				moveLeftRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				moveLeftRule_isNodeHomomorphicGlobal,
				moveLeftRule_isEdgeHomomorphicGlobal
			);
			pat_moveLeftRule.edgeToSourceNode.Add(moveLeftRule_edge__edge0, moveLeftRule_node_wv);
			pat_moveLeftRule.edgeToTargetNode.Add(moveLeftRule_edge__edge0, moveLeftRule_node_s);
			pat_moveLeftRule.edgeToSourceNode.Add(moveLeftRule_edge__edge1, moveLeftRule_node_lbp);
			pat_moveLeftRule.edgeToTargetNode.Add(moveLeftRule_edge__edge1, moveLeftRule_node_bp);

			moveLeftRule_node_wv.PointOfDefinition = null;
			moveLeftRule_node_s.PointOfDefinition = pat_moveLeftRule;
			moveLeftRule_node_lbp.PointOfDefinition = pat_moveLeftRule;
			moveLeftRule_node_bp.PointOfDefinition = null;
			moveLeftRule_edge__edge0.PointOfDefinition = pat_moveLeftRule;
			moveLeftRule_edge__edge1.PointOfDefinition = pat_moveLeftRule;

			patternGraph = pat_moveLeftRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
		{
			Match_moveLeftRule curMatch = (Match_moveLeftRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_lbp = curMatch._node_lbp;
			graph.SettingAddedNodeNames( moveLeftRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveLeftRule_addedEdgeNames );
			output_0 = (GRGEN_MODEL.IState)(node_s);
			output_1 = (GRGEN_MODEL.IBandPosition)(node_lbp);
			return;
		}
		private static string[] moveLeftRule_addedNodeNames = new string[] {  };
		private static string[] moveLeftRule_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
		{
			Match_moveLeftRule curMatch = (Match_moveLeftRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_lbp = curMatch._node_lbp;
			graph.SettingAddedNodeNames( moveLeftRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveLeftRule_addedEdgeNames );
			output_0 = (GRGEN_MODEL.IState)(node_s);
			output_1 = (GRGEN_MODEL.IBandPosition)(node_lbp);
			return;
		}

		static Rule_moveLeftRule() {
		}

		public interface IMatch_moveLeftRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; }
			GRGEN_MODEL.IState node_s { get; }
			GRGEN_MODEL.IBandPosition node_lbp { get; }
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			GRGEN_MODEL.ImoveLeft edge__edge0 { get; }
			GRGEN_MODEL.Iright edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_moveLeftRule : GRGEN_LGSP.ListElement<Match_moveLeftRule>, IMatch_moveLeftRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } }
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } }
			public GRGEN_MODEL.IBandPosition node_lbp { get { return (GRGEN_MODEL.IBandPosition)_node_lbp; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_lbp;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum moveLeftRule_NodeNums { @wv, @s, @lbp, @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)moveLeftRule_NodeNums.@wv: return _node_wv;
				case (int)moveLeftRule_NodeNums.@s: return _node_s;
				case (int)moveLeftRule_NodeNums.@lbp: return _node_lbp;
				case (int)moveLeftRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImoveLeft edge__edge0 { get { return (GRGEN_MODEL.ImoveLeft)_edge__edge0; } }
			public GRGEN_MODEL.Iright edge__edge1 { get { return (GRGEN_MODEL.Iright)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum moveLeftRule_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)moveLeftRule_EdgeNums.@_edge0: return _edge__edge0;
				case (int)moveLeftRule_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum moveLeftRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveLeftRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveLeftRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveLeftRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveLeftRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_moveLeftRule.instance.pat_moveLeftRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_moveRightRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_moveRightRule instance = null;
		public static Rule_moveRightRule Instance { get { if (instance==null) { instance = new Rule_moveRightRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] moveRightRule_node_wv_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] moveRightRule_node_s_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] moveRightRule_node_bp_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] moveRightRule_node_rbp_AllowedTypes = null;
		public static bool[] moveRightRule_node_wv_IsAllowedType = null;
		public static bool[] moveRightRule_node_s_IsAllowedType = null;
		public static bool[] moveRightRule_node_bp_IsAllowedType = null;
		public static bool[] moveRightRule_node_rbp_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] moveRightRule_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] moveRightRule_edge__edge1_AllowedTypes = null;
		public static bool[] moveRightRule_edge__edge0_IsAllowedType = null;
		public static bool[] moveRightRule_edge__edge1_IsAllowedType = null;
		public enum moveRightRule_NodeNums { @wv, @s, @bp, @rbp, };
		public enum moveRightRule_EdgeNums { @_edge0, @_edge1, };
		public enum moveRightRule_VariableNums { };
		public enum moveRightRule_SubNums { };
		public enum moveRightRule_AltNums { };
		public enum moveRightRule_IterNums { };



		GRGEN_LGSP.PatternGraph pat_moveRightRule;


		private Rule_moveRightRule()
		{
			name = "moveRightRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
			inputNames = new string[] { "moveRightRule_node_wv", "moveRightRule_node_bp", };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, };
		}
		private void initialize()
		{
			bool[,] moveRightRule_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] moveRightRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] moveRightRule_minMatches = new int[0] ;
			int[] moveRightRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode moveRightRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "moveRightRule_node_wv", "wv", moveRightRule_node_wv_AllowedTypes, moveRightRule_node_wv_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode moveRightRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "moveRightRule_node_s", "s", moveRightRule_node_s_AllowedTypes, moveRightRule_node_s_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode moveRightRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "moveRightRule_node_bp", "bp", moveRightRule_node_bp_AllowedTypes, moveRightRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternNode moveRightRule_node_rbp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "moveRightRule_node_rbp", "rbp", moveRightRule_node_rbp_AllowedTypes, moveRightRule_node_rbp_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge moveRightRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveRight, "GRGEN_MODEL.ImoveRight", "moveRightRule_edge__edge0", "_edge0", moveRightRule_edge__edge0_AllowedTypes, moveRightRule_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge moveRightRule_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, "GRGEN_MODEL.Iright", "moveRightRule_edge__edge1", "_edge1", moveRightRule_edge__edge1_AllowedTypes, moveRightRule_edge__edge1_IsAllowedType, 5.5F, -1);
			pat_moveRightRule = new GRGEN_LGSP.PatternGraph(
				"moveRightRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { moveRightRule_node_wv, moveRightRule_node_s, moveRightRule_node_bp, moveRightRule_node_rbp }, 
				new GRGEN_LGSP.PatternEdge[] { moveRightRule_edge__edge0, moveRightRule_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				moveRightRule_minMatches,
				moveRightRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				moveRightRule_isNodeHomomorphicGlobal,
				moveRightRule_isEdgeHomomorphicGlobal
			);
			pat_moveRightRule.edgeToSourceNode.Add(moveRightRule_edge__edge0, moveRightRule_node_wv);
			pat_moveRightRule.edgeToTargetNode.Add(moveRightRule_edge__edge0, moveRightRule_node_s);
			pat_moveRightRule.edgeToSourceNode.Add(moveRightRule_edge__edge1, moveRightRule_node_bp);
			pat_moveRightRule.edgeToTargetNode.Add(moveRightRule_edge__edge1, moveRightRule_node_rbp);

			moveRightRule_node_wv.PointOfDefinition = null;
			moveRightRule_node_s.PointOfDefinition = pat_moveRightRule;
			moveRightRule_node_bp.PointOfDefinition = null;
			moveRightRule_node_rbp.PointOfDefinition = pat_moveRightRule;
			moveRightRule_edge__edge0.PointOfDefinition = pat_moveRightRule;
			moveRightRule_edge__edge1.PointOfDefinition = pat_moveRightRule;

			patternGraph = pat_moveRightRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
		{
			Match_moveRightRule curMatch = (Match_moveRightRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_rbp = curMatch._node_rbp;
			graph.SettingAddedNodeNames( moveRightRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveRightRule_addedEdgeNames );
			output_0 = (GRGEN_MODEL.IState)(node_s);
			output_1 = (GRGEN_MODEL.IBandPosition)(node_rbp);
			return;
		}
		private static string[] moveRightRule_addedNodeNames = new string[] {  };
		private static string[] moveRightRule_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
		{
			Match_moveRightRule curMatch = (Match_moveRightRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_rbp = curMatch._node_rbp;
			graph.SettingAddedNodeNames( moveRightRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveRightRule_addedEdgeNames );
			output_0 = (GRGEN_MODEL.IState)(node_s);
			output_1 = (GRGEN_MODEL.IBandPosition)(node_rbp);
			return;
		}

		static Rule_moveRightRule() {
		}

		public interface IMatch_moveRightRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; }
			GRGEN_MODEL.IState node_s { get; }
			GRGEN_MODEL.IBandPosition node_bp { get; }
			GRGEN_MODEL.IBandPosition node_rbp { get; }
			//Edges
			GRGEN_MODEL.ImoveRight edge__edge0 { get; }
			GRGEN_MODEL.Iright edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_moveRightRule : GRGEN_LGSP.ListElement<Match_moveRightRule>, IMatch_moveRightRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } }
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_MODEL.IBandPosition node_rbp { get { return (GRGEN_MODEL.IBandPosition)_node_rbp; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public GRGEN_LGSP.LGSPNode _node_rbp;
			public enum moveRightRule_NodeNums { @wv, @s, @bp, @rbp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)moveRightRule_NodeNums.@wv: return _node_wv;
				case (int)moveRightRule_NodeNums.@s: return _node_s;
				case (int)moveRightRule_NodeNums.@bp: return _node_bp;
				case (int)moveRightRule_NodeNums.@rbp: return _node_rbp;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.ImoveRight edge__edge0 { get { return (GRGEN_MODEL.ImoveRight)_edge__edge0; } }
			public GRGEN_MODEL.Iright edge__edge1 { get { return (GRGEN_MODEL.Iright)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum moveRightRule_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)moveRightRule_EdgeNums.@_edge0: return _edge__edge0;
				case (int)moveRightRule_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum moveRightRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveRightRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveRightRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveRightRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum moveRightRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_moveRightRule.instance.pat_moveRightRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_countZeros : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_countZeros instance = null;
		public static Rule_countZeros Instance { get { if (instance==null) { instance = new Rule_countZeros(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] countZeros_node_bp_AllowedTypes = null;
		public static bool[] countZeros_node_bp_IsAllowedType = null;
		public enum countZeros_NodeNums { @bp, };
		public enum countZeros_EdgeNums { };
		public enum countZeros_VariableNums { };
		public enum countZeros_SubNums { };
		public enum countZeros_AltNums { };
		public enum countZeros_IterNums { };



		GRGEN_LGSP.PatternGraph pat_countZeros;


		private Rule_countZeros()
		{
			name = "countZeros";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] countZeros_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] countZeros_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] countZeros_minMatches = new int[0] ;
			int[] countZeros_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode countZeros_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "countZeros_node_bp", "bp", countZeros_node_bp_AllowedTypes, countZeros_node_bp_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", "countZeros_node_bp", "value"), new GRGEN_EXPR.Constant("0")),
				new string[] { "countZeros_node_bp" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_countZeros = new GRGEN_LGSP.PatternGraph(
				"countZeros",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { countZeros_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				countZeros_minMatches,
				countZeros_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { cond_0,  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				countZeros_isNodeHomomorphicGlobal,
				countZeros_isEdgeHomomorphicGlobal
			);

			countZeros_node_bp.PointOfDefinition = pat_countZeros;

			patternGraph = pat_countZeros;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_countZeros curMatch = (Match_countZeros)_curMatch;
			graph.SettingAddedNodeNames( countZeros_addedNodeNames );
			graph.SettingAddedEdgeNames( countZeros_addedEdgeNames );
			return;
		}
		private static string[] countZeros_addedNodeNames = new string[] {  };
		private static string[] countZeros_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_countZeros curMatch = (Match_countZeros)_curMatch;
			graph.SettingAddedNodeNames( countZeros_addedNodeNames );
			graph.SettingAddedEdgeNames( countZeros_addedEdgeNames );
			return;
		}

		static Rule_countZeros() {
		}

		public interface IMatch_countZeros : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_countZeros : GRGEN_LGSP.ListElement<Match_countZeros>, IMatch_countZeros
		{
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum countZeros_NodeNums { @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)countZeros_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public enum countZeros_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countZeros_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countZeros_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countZeros_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countZeros_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countZeros_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_countZeros.instance.pat_countZeros; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_countOnes : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_countOnes instance = null;
		public static Rule_countOnes Instance { get { if (instance==null) { instance = new Rule_countOnes(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] countOnes_node_bp_AllowedTypes = null;
		public static bool[] countOnes_node_bp_IsAllowedType = null;
		public enum countOnes_NodeNums { @bp, };
		public enum countOnes_EdgeNums { };
		public enum countOnes_VariableNums { };
		public enum countOnes_SubNums { };
		public enum countOnes_AltNums { };
		public enum countOnes_IterNums { };



		GRGEN_LGSP.PatternGraph pat_countOnes;


		private Rule_countOnes()
		{
			name = "countOnes";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] countOnes_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] countOnes_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] countOnes_minMatches = new int[0] ;
			int[] countOnes_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode countOnes_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "countOnes_node_bp", "bp", countOnes_node_bp_AllowedTypes, countOnes_node_bp_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", "countOnes_node_bp", "value"), new GRGEN_EXPR.Constant("1")),
				new string[] { "countOnes_node_bp" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_countOnes = new GRGEN_LGSP.PatternGraph(
				"countOnes",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { countOnes_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				countOnes_minMatches,
				countOnes_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { cond_0,  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				countOnes_isNodeHomomorphicGlobal,
				countOnes_isEdgeHomomorphicGlobal
			);

			countOnes_node_bp.PointOfDefinition = pat_countOnes;

			patternGraph = pat_countOnes;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_countOnes curMatch = (Match_countOnes)_curMatch;
			graph.SettingAddedNodeNames( countOnes_addedNodeNames );
			graph.SettingAddedEdgeNames( countOnes_addedEdgeNames );
			return;
		}
		private static string[] countOnes_addedNodeNames = new string[] {  };
		private static string[] countOnes_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_countOnes curMatch = (Match_countOnes)_curMatch;
			graph.SettingAddedNodeNames( countOnes_addedNodeNames );
			graph.SettingAddedEdgeNames( countOnes_addedEdgeNames );
			return;
		}

		static Rule_countOnes() {
		}

		public interface IMatch_countOnes : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IBandPosition node_bp { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_countOnes : GRGEN_LGSP.ListElement<Match_countOnes>, IMatch_countOnes
		{
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } }
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum countOnes_NodeNums { @bp, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)countOnes_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			
			public enum countOnes_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countOnes_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countOnes_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countOnes_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countOnes_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum countOnes_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_countOnes.instance.pat_countOnes; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Turing3_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public Turing3_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[8];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+8];
			rules[0] = Rule_readZeroRule.Instance;
			rulesAndSubpatterns[0+0] = Rule_readZeroRule.Instance;
			rules[1] = Rule_readOneRule.Instance;
			rulesAndSubpatterns[0+1] = Rule_readOneRule.Instance;
			rules[2] = Rule_ensureMoveLeftValidRule.Instance;
			rulesAndSubpatterns[0+2] = Rule_ensureMoveLeftValidRule.Instance;
			rules[3] = Rule_ensureMoveRightValidRule.Instance;
			rulesAndSubpatterns[0+3] = Rule_ensureMoveRightValidRule.Instance;
			rules[4] = Rule_moveLeftRule.Instance;
			rulesAndSubpatterns[0+4] = Rule_moveLeftRule.Instance;
			rules[5] = Rule_moveRightRule.Instance;
			rulesAndSubpatterns[0+5] = Rule_moveRightRule.Instance;
			rules[6] = Rule_countZeros.Instance;
			rulesAndSubpatterns[0+6] = Rule_countZeros.Instance;
			rules[7] = Rule_countOnes.Instance;
			rulesAndSubpatterns[0+7] = Rule_countOnes.Instance;
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
	}


    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_readZeroRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_readZeroRule.IMatch_readZeroRule match, out GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches, out GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
    }
    
    public class Action_readZeroRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_readZeroRule
    {
        public Action_readZeroRule() {
            _rulePattern = Rule_readZeroRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_readZeroRule.Match_readZeroRule, Rule_readZeroRule.IMatch_readZeroRule>(this);
        }

        public Rule_readZeroRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "readZeroRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_readZeroRule.Match_readZeroRule, Rule_readZeroRule.IMatch_readZeroRule> matches;

        public static Action_readZeroRule Instance { get { return instance; } }
        private static Action_readZeroRule instance = new Action_readZeroRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset readZeroRule_node_s 
            GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_s = (GRGEN_LGSP.LGSPNode)readZeroRule_node_s;
            if(candidate_readZeroRule_node_s == null) {
                MissingPreset_readZeroRule_node_s(graph, maxMatches, readZeroRule_node_s, readZeroRule_node_bp, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readZeroRule_node_s.lgspType.TypeID!=2) {
                return matches;
            }
            // Preset readZeroRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_bp = (GRGEN_LGSP.LGSPNode)readZeroRule_node_bp;
            if(candidate_readZeroRule_node_bp == null) {
                MissingPreset_readZeroRule_node_bp(graph, maxMatches, readZeroRule_node_s, readZeroRule_node_bp, null, null, null, candidate_readZeroRule_node_s);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readZeroRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            // Condition 
            if(!((((GRGEN_MODEL.IBandPosition)candidate_readZeroRule_node_bp).@value == 0))) {
                return matches;
            }
            // Extend Outgoing readZeroRule_edge_rv from readZeroRule_node_s 
            GRGEN_LGSP.LGSPEdge head_candidate_readZeroRule_edge_rv = candidate_readZeroRule_node_s.lgspOuthead;
            if(head_candidate_readZeroRule_edge_rv != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_readZeroRule_edge_rv = head_candidate_readZeroRule_edge_rv;
                do
                {
                    if(candidate_readZeroRule_edge_rv.lgspType.TypeID!=4) {
                        continue;
                    }
                    // Implicit Target readZeroRule_node_wv from readZeroRule_edge_rv 
                    GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_wv = candidate_readZeroRule_edge_rv.lgspTarget;
                    if(candidate_readZeroRule_node_wv.lgspType.TypeID!=3) {
                        continue;
                    }
                    Rule_readZeroRule.Match_readZeroRule match = matches.GetNextUnfilledPosition();
                    match._node_s = candidate_readZeroRule_node_s;
                    match._node_wv = candidate_readZeroRule_node_wv;
                    match._node_bp = candidate_readZeroRule_node_bp;
                    match._edge_rv = candidate_readZeroRule_edge_rv;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_readZeroRule_node_s.MoveOutHeadAfter(candidate_readZeroRule_edge_rv);
                        return matches;
                    }
                }
                while( (candidate_readZeroRule_edge_rv = candidate_readZeroRule_edge_rv.lgspOutNext) != head_candidate_readZeroRule_edge_rv );
            }
            return matches;
        }
        public void MissingPreset_readZeroRule_node_s(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup readZeroRule_node_s 
            int type_id_candidate_readZeroRule_node_s = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_readZeroRule_node_s = graph.nodesByTypeHeads[type_id_candidate_readZeroRule_node_s], candidate_readZeroRule_node_s = head_candidate_readZeroRule_node_s.lgspTypeNext; candidate_readZeroRule_node_s != head_candidate_readZeroRule_node_s; candidate_readZeroRule_node_s = candidate_readZeroRule_node_s.lgspTypeNext)
            {
                // Preset readZeroRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_bp = (GRGEN_LGSP.LGSPNode)readZeroRule_node_bp;
                if(candidate_readZeroRule_node_bp == null) {
                    MissingPreset_readZeroRule_node_bp(graph, maxMatches, readZeroRule_node_s, readZeroRule_node_bp, null, null, null, candidate_readZeroRule_node_s);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_readZeroRule_node_bp.lgspType.TypeID!=1) {
                    continue;
                }
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readZeroRule_node_bp).@value == 0))) {
                    continue;
                }
                // Extend Outgoing readZeroRule_edge_rv from readZeroRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readZeroRule_edge_rv = candidate_readZeroRule_node_s.lgspOuthead;
                if(head_candidate_readZeroRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readZeroRule_edge_rv = head_candidate_readZeroRule_edge_rv;
                    do
                    {
                        if(candidate_readZeroRule_edge_rv.lgspType.TypeID!=4) {
                            continue;
                        }
                        // Implicit Target readZeroRule_node_wv from readZeroRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_wv = candidate_readZeroRule_edge_rv.lgspTarget;
                        if(candidate_readZeroRule_node_wv.lgspType.TypeID!=3) {
                            continue;
                        }
                        Rule_readZeroRule.Match_readZeroRule match = matches.GetNextUnfilledPosition();
                        match._node_s = candidate_readZeroRule_node_s;
                        match._node_wv = candidate_readZeroRule_node_wv;
                        match._node_bp = candidate_readZeroRule_node_bp;
                        match._edge_rv = candidate_readZeroRule_edge_rv;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_readZeroRule_node_s.MoveOutHeadAfter(candidate_readZeroRule_edge_rv);
                            graph.MoveHeadAfter(candidate_readZeroRule_node_s);
                            return;
                        }
                    }
                    while( (candidate_readZeroRule_edge_rv = candidate_readZeroRule_edge_rv.lgspOutNext) != head_candidate_readZeroRule_edge_rv );
                }
            }
            return;
        }
        public void MissingPreset_readZeroRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_s)
        {
            int negLevel = 0;
            // Lookup readZeroRule_node_bp 
            int type_id_candidate_readZeroRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_readZeroRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_readZeroRule_node_bp], candidate_readZeroRule_node_bp = head_candidate_readZeroRule_node_bp.lgspTypeNext; candidate_readZeroRule_node_bp != head_candidate_readZeroRule_node_bp; candidate_readZeroRule_node_bp = candidate_readZeroRule_node_bp.lgspTypeNext)
            {
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readZeroRule_node_bp).@value == 0))) {
                    continue;
                }
                // Extend Outgoing readZeroRule_edge_rv from readZeroRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readZeroRule_edge_rv = candidate_readZeroRule_node_s.lgspOuthead;
                if(head_candidate_readZeroRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readZeroRule_edge_rv = head_candidate_readZeroRule_edge_rv;
                    do
                    {
                        if(candidate_readZeroRule_edge_rv.lgspType.TypeID!=4) {
                            continue;
                        }
                        // Implicit Target readZeroRule_node_wv from readZeroRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_wv = candidate_readZeroRule_edge_rv.lgspTarget;
                        if(candidate_readZeroRule_node_wv.lgspType.TypeID!=3) {
                            continue;
                        }
                        Rule_readZeroRule.Match_readZeroRule match = matches.GetNextUnfilledPosition();
                        match._node_s = candidate_readZeroRule_node_s;
                        match._node_wv = candidate_readZeroRule_node_wv;
                        match._node_bp = candidate_readZeroRule_node_bp;
                        match._edge_rv = candidate_readZeroRule_edge_rv;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_readZeroRule_node_s.MoveOutHeadAfter(candidate_readZeroRule_edge_rv);
                            graph.MoveHeadAfter(candidate_readZeroRule_node_bp);
                            return;
                        }
                    }
                    while( (candidate_readZeroRule_edge_rv = candidate_readZeroRule_edge_rv.lgspOutNext) != head_candidate_readZeroRule_edge_rv );
                }
            }
            return;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, readZeroRule_node_s, readZeroRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_readZeroRule.IMatch_readZeroRule match, out GRGEN_MODEL.IWriteValue output_0)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches, out GRGEN_MODEL.IWriteValue output_0)
        {
            output_0 = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_readZeroRule.IMatch_readZeroRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            } else {
                foreach(Rule_readZeroRule.IMatch_readZeroRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readZeroRule_node_s, readZeroRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, readZeroRule_node_s, readZeroRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_readZeroRule.IMatch_readZeroRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            } else {
                foreach(Rule_readZeroRule.IMatch_readZeroRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readZeroRule_node_s, readZeroRule_node_bp);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readZeroRule_node_s, readZeroRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IWriteValue output_0; 
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readZeroRule_node_s, readZeroRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readZeroRule_node_s, readZeroRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IWriteValue output_0; 
            Modify(graph, (Rule_readZeroRule.IMatch_readZeroRule)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IWriteValue output_0; 
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            GRGEN_MODEL.IWriteValue output_0 = null; 
            if(Apply(graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
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
            GRGEN_MODEL.IWriteValue output_0 = null; 
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
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
            return ApplyStar(graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_readOneRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_readOneRule.IMatch_readOneRule match, out GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches, out GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
    }
    
    public class Action_readOneRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_readOneRule
    {
        public Action_readOneRule() {
            _rulePattern = Rule_readOneRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_readOneRule.Match_readOneRule, Rule_readOneRule.IMatch_readOneRule>(this);
        }

        public Rule_readOneRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "readOneRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_readOneRule.Match_readOneRule, Rule_readOneRule.IMatch_readOneRule> matches;

        public static Action_readOneRule Instance { get { return instance; } }
        private static Action_readOneRule instance = new Action_readOneRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset readOneRule_node_s 
            GRGEN_LGSP.LGSPNode candidate_readOneRule_node_s = (GRGEN_LGSP.LGSPNode)readOneRule_node_s;
            if(candidate_readOneRule_node_s == null) {
                MissingPreset_readOneRule_node_s(graph, maxMatches, readOneRule_node_s, readOneRule_node_bp, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readOneRule_node_s.lgspType.TypeID!=2) {
                return matches;
            }
            // Preset readOneRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_readOneRule_node_bp = (GRGEN_LGSP.LGSPNode)readOneRule_node_bp;
            if(candidate_readOneRule_node_bp == null) {
                MissingPreset_readOneRule_node_bp(graph, maxMatches, readOneRule_node_s, readOneRule_node_bp, null, null, null, candidate_readOneRule_node_s);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readOneRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            // Condition 
            if(!((((GRGEN_MODEL.IBandPosition)candidate_readOneRule_node_bp).@value == 1))) {
                return matches;
            }
            // Extend Outgoing readOneRule_edge_rv from readOneRule_node_s 
            GRGEN_LGSP.LGSPEdge head_candidate_readOneRule_edge_rv = candidate_readOneRule_node_s.lgspOuthead;
            if(head_candidate_readOneRule_edge_rv != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_readOneRule_edge_rv = head_candidate_readOneRule_edge_rv;
                do
                {
                    if(candidate_readOneRule_edge_rv.lgspType.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target readOneRule_node_wv from readOneRule_edge_rv 
                    GRGEN_LGSP.LGSPNode candidate_readOneRule_node_wv = candidate_readOneRule_edge_rv.lgspTarget;
                    if(candidate_readOneRule_node_wv.lgspType.TypeID!=3) {
                        continue;
                    }
                    Rule_readOneRule.Match_readOneRule match = matches.GetNextUnfilledPosition();
                    match._node_s = candidate_readOneRule_node_s;
                    match._node_wv = candidate_readOneRule_node_wv;
                    match._node_bp = candidate_readOneRule_node_bp;
                    match._edge_rv = candidate_readOneRule_edge_rv;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_readOneRule_node_s.MoveOutHeadAfter(candidate_readOneRule_edge_rv);
                        return matches;
                    }
                }
                while( (candidate_readOneRule_edge_rv = candidate_readOneRule_edge_rv.lgspOutNext) != head_candidate_readOneRule_edge_rv );
            }
            return matches;
        }
        public void MissingPreset_readOneRule_node_s(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup readOneRule_node_s 
            int type_id_candidate_readOneRule_node_s = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_readOneRule_node_s = graph.nodesByTypeHeads[type_id_candidate_readOneRule_node_s], candidate_readOneRule_node_s = head_candidate_readOneRule_node_s.lgspTypeNext; candidate_readOneRule_node_s != head_candidate_readOneRule_node_s; candidate_readOneRule_node_s = candidate_readOneRule_node_s.lgspTypeNext)
            {
                // Preset readOneRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_readOneRule_node_bp = (GRGEN_LGSP.LGSPNode)readOneRule_node_bp;
                if(candidate_readOneRule_node_bp == null) {
                    MissingPreset_readOneRule_node_bp(graph, maxMatches, readOneRule_node_s, readOneRule_node_bp, null, null, null, candidate_readOneRule_node_s);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_readOneRule_node_bp.lgspType.TypeID!=1) {
                    continue;
                }
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readOneRule_node_bp).@value == 1))) {
                    continue;
                }
                // Extend Outgoing readOneRule_edge_rv from readOneRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readOneRule_edge_rv = candidate_readOneRule_node_s.lgspOuthead;
                if(head_candidate_readOneRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readOneRule_edge_rv = head_candidate_readOneRule_edge_rv;
                    do
                    {
                        if(candidate_readOneRule_edge_rv.lgspType.TypeID!=5) {
                            continue;
                        }
                        // Implicit Target readOneRule_node_wv from readOneRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readOneRule_node_wv = candidate_readOneRule_edge_rv.lgspTarget;
                        if(candidate_readOneRule_node_wv.lgspType.TypeID!=3) {
                            continue;
                        }
                        Rule_readOneRule.Match_readOneRule match = matches.GetNextUnfilledPosition();
                        match._node_s = candidate_readOneRule_node_s;
                        match._node_wv = candidate_readOneRule_node_wv;
                        match._node_bp = candidate_readOneRule_node_bp;
                        match._edge_rv = candidate_readOneRule_edge_rv;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_readOneRule_node_s.MoveOutHeadAfter(candidate_readOneRule_edge_rv);
                            graph.MoveHeadAfter(candidate_readOneRule_node_s);
                            return;
                        }
                    }
                    while( (candidate_readOneRule_edge_rv = candidate_readOneRule_edge_rv.lgspOutNext) != head_candidate_readOneRule_edge_rv );
                }
            }
            return;
        }
        public void MissingPreset_readOneRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_readOneRule_node_s)
        {
            int negLevel = 0;
            // Lookup readOneRule_node_bp 
            int type_id_candidate_readOneRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_readOneRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_readOneRule_node_bp], candidate_readOneRule_node_bp = head_candidate_readOneRule_node_bp.lgspTypeNext; candidate_readOneRule_node_bp != head_candidate_readOneRule_node_bp; candidate_readOneRule_node_bp = candidate_readOneRule_node_bp.lgspTypeNext)
            {
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readOneRule_node_bp).@value == 1))) {
                    continue;
                }
                // Extend Outgoing readOneRule_edge_rv from readOneRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readOneRule_edge_rv = candidate_readOneRule_node_s.lgspOuthead;
                if(head_candidate_readOneRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readOneRule_edge_rv = head_candidate_readOneRule_edge_rv;
                    do
                    {
                        if(candidate_readOneRule_edge_rv.lgspType.TypeID!=5) {
                            continue;
                        }
                        // Implicit Target readOneRule_node_wv from readOneRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readOneRule_node_wv = candidate_readOneRule_edge_rv.lgspTarget;
                        if(candidate_readOneRule_node_wv.lgspType.TypeID!=3) {
                            continue;
                        }
                        Rule_readOneRule.Match_readOneRule match = matches.GetNextUnfilledPosition();
                        match._node_s = candidate_readOneRule_node_s;
                        match._node_wv = candidate_readOneRule_node_wv;
                        match._node_bp = candidate_readOneRule_node_bp;
                        match._edge_rv = candidate_readOneRule_edge_rv;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_readOneRule_node_s.MoveOutHeadAfter(candidate_readOneRule_edge_rv);
                            graph.MoveHeadAfter(candidate_readOneRule_node_bp);
                            return;
                        }
                    }
                    while( (candidate_readOneRule_edge_rv = candidate_readOneRule_edge_rv.lgspOutNext) != head_candidate_readOneRule_edge_rv );
                }
            }
            return;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, readOneRule_node_s, readOneRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_readOneRule.IMatch_readOneRule match, out GRGEN_MODEL.IWriteValue output_0)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches, out GRGEN_MODEL.IWriteValue output_0)
        {
            output_0 = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_readOneRule.IMatch_readOneRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            } else {
                foreach(Rule_readOneRule.IMatch_readOneRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readOneRule_node_s, readOneRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, readOneRule_node_s, readOneRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_readOneRule.IMatch_readOneRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            } else {
                foreach(Rule_readOneRule.IMatch_readOneRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readOneRule_node_s, readOneRule_node_bp);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readOneRule_node_s, readOneRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IWriteValue output_0; 
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readOneRule_node_s, readOneRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, readOneRule_node_s, readOneRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IWriteValue output_0; 
            Modify(graph, (Rule_readOneRule.IMatch_readOneRule)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IWriteValue output_0; 
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            GRGEN_MODEL.IWriteValue output_0 = null; 
            if(Apply(graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
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
            GRGEN_MODEL.IWriteValue output_0 = null; 
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
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
            return ApplyStar(graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_ensureMoveLeftValidRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
    }
    
    public class Action_ensureMoveLeftValidRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ensureMoveLeftValidRule
    {
        public Action_ensureMoveLeftValidRule() {
            _rulePattern = Rule_ensureMoveLeftValidRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>(this);
        }

        public Rule_ensureMoveLeftValidRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ensureMoveLeftValidRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches;

        public static Action_ensureMoveLeftValidRule Instance { get { return instance; } }
        private static Action_ensureMoveLeftValidRule instance = new Action_ensureMoveLeftValidRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset ensureMoveLeftValidRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_wv = (GRGEN_LGSP.LGSPNode)ensureMoveLeftValidRule_node_wv;
            if(candidate_ensureMoveLeftValidRule_node_wv == null) {
                MissingPreset_ensureMoveLeftValidRule_node_wv(graph, maxMatches, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveLeftValidRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset ensureMoveLeftValidRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_bp = (GRGEN_LGSP.LGSPNode)ensureMoveLeftValidRule_node_bp;
            if(candidate_ensureMoveLeftValidRule_node_bp == null) {
                MissingPreset_ensureMoveLeftValidRule_node_bp(graph, maxMatches, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp, null, null, null, candidate_ensureMoveLeftValidRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveLeftValidRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            // NegativePattern 
            {
                ++negLevel;
                uint prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_ensureMoveLeftValidRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming ensureMoveLeftValidRule_neg_0_edge__edge0 from ensureMoveLeftValidRule_node_bp 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_node_bp.lgspInhead;
                if(head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspType.TypeID!=3) {
                            continue;
                        }
                        // Implicit Source ensureMoveLeftValidRule_neg_0_node__node0 from ensureMoveLeftValidRule_neg_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_neg_0_node__node0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspSource;
                        if(candidate_ensureMoveLeftValidRule_neg_0_node__node0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ensureMoveLeftValidRule_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // negative pattern found
                        candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                        --negLevel;
                        return matches;
                    }
                    while( (candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspInNext) != head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 );
                }
                candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                --negLevel;
            }
            // Extend Outgoing ensureMoveLeftValidRule_edge__edge0 from ensureMoveLeftValidRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_node_wv.lgspOuthead;
            if(head_candidate_ensureMoveLeftValidRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_edge__edge0 = head_candidate_ensureMoveLeftValidRule_edge__edge0;
                do
                {
                    if(candidate_ensureMoveLeftValidRule_edge__edge0.lgspType.TypeID!=6) {
                        continue;
                    }
                    // Implicit Target ensureMoveLeftValidRule_node__node0 from ensureMoveLeftValidRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node__node0 = candidate_ensureMoveLeftValidRule_edge__edge0.lgspTarget;
                    if(candidate_ensureMoveLeftValidRule_node__node0.lgspType.TypeID!=2) {
                        continue;
                    }
                    Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule match = matches.GetNextUnfilledPosition();
                    match._node_wv = candidate_ensureMoveLeftValidRule_node_wv;
                    match._node__node0 = candidate_ensureMoveLeftValidRule_node__node0;
                    match._node_bp = candidate_ensureMoveLeftValidRule_node_bp;
                    match._edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_ensureMoveLeftValidRule_node_wv.MoveOutHeadAfter(candidate_ensureMoveLeftValidRule_edge__edge0);
                        return matches;
                    }
                }
                while( (candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0.lgspOutNext) != head_candidate_ensureMoveLeftValidRule_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_ensureMoveLeftValidRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup ensureMoveLeftValidRule_node_wv 
            int type_id_candidate_ensureMoveLeftValidRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveLeftValidRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_ensureMoveLeftValidRule_node_wv], candidate_ensureMoveLeftValidRule_node_wv = head_candidate_ensureMoveLeftValidRule_node_wv.lgspTypeNext; candidate_ensureMoveLeftValidRule_node_wv != head_candidate_ensureMoveLeftValidRule_node_wv; candidate_ensureMoveLeftValidRule_node_wv = candidate_ensureMoveLeftValidRule_node_wv.lgspTypeNext)
            {
                // Preset ensureMoveLeftValidRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_bp = (GRGEN_LGSP.LGSPNode)ensureMoveLeftValidRule_node_bp;
                if(candidate_ensureMoveLeftValidRule_node_bp == null) {
                    MissingPreset_ensureMoveLeftValidRule_node_bp(graph, maxMatches, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp, null, null, null, candidate_ensureMoveLeftValidRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_ensureMoveLeftValidRule_node_bp.lgspType.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveLeftValidRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Incoming ensureMoveLeftValidRule_neg_0_edge__edge0 from ensureMoveLeftValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_node_bp.lgspInhead;
                    if(head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspType.TypeID!=3) {
                                continue;
                            }
                            // Implicit Source ensureMoveLeftValidRule_neg_0_node__node0 from ensureMoveLeftValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_neg_0_node__node0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspSource;
                            if(candidate_ensureMoveLeftValidRule_neg_0_node__node0.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveLeftValidRule_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                            --negLevel;
                            goto label0;
                        }
                        while( (candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspInNext) != head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveLeftValidRule_edge__edge0 from ensureMoveLeftValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_node_wv.lgspOuthead;
                if(head_candidate_ensureMoveLeftValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_edge__edge0 = head_candidate_ensureMoveLeftValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveLeftValidRule_edge__edge0.lgspType.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target ensureMoveLeftValidRule_node__node0 from ensureMoveLeftValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node__node0 = candidate_ensureMoveLeftValidRule_edge__edge0.lgspTarget;
                        if(candidate_ensureMoveLeftValidRule_node__node0.lgspType.TypeID!=2) {
                            continue;
                        }
                        Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule match = matches.GetNextUnfilledPosition();
                        match._node_wv = candidate_ensureMoveLeftValidRule_node_wv;
                        match._node__node0 = candidate_ensureMoveLeftValidRule_node__node0;
                        match._node_bp = candidate_ensureMoveLeftValidRule_node_bp;
                        match._edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_ensureMoveLeftValidRule_node_wv.MoveOutHeadAfter(candidate_ensureMoveLeftValidRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_ensureMoveLeftValidRule_node_wv);
                            return;
                        }
                    }
                    while( (candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0.lgspOutNext) != head_candidate_ensureMoveLeftValidRule_edge__edge0 );
                }
label0: ;
            }
            return;
        }
        public void MissingPreset_ensureMoveLeftValidRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_wv)
        {
            int negLevel = 0;
            // Lookup ensureMoveLeftValidRule_node_bp 
            int type_id_candidate_ensureMoveLeftValidRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveLeftValidRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_ensureMoveLeftValidRule_node_bp], candidate_ensureMoveLeftValidRule_node_bp = head_candidate_ensureMoveLeftValidRule_node_bp.lgspTypeNext; candidate_ensureMoveLeftValidRule_node_bp != head_candidate_ensureMoveLeftValidRule_node_bp; candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.lgspTypeNext)
            {
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveLeftValidRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Incoming ensureMoveLeftValidRule_neg_0_edge__edge0 from ensureMoveLeftValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_node_bp.lgspInhead;
                    if(head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspType.TypeID!=3) {
                                continue;
                            }
                            // Implicit Source ensureMoveLeftValidRule_neg_0_node__node0 from ensureMoveLeftValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_neg_0_node__node0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspSource;
                            if(candidate_ensureMoveLeftValidRule_neg_0_node__node0.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveLeftValidRule_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                            --negLevel;
                            goto label1;
                        }
                        while( (candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspInNext) != head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveLeftValidRule_edge__edge0 from ensureMoveLeftValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_node_wv.lgspOuthead;
                if(head_candidate_ensureMoveLeftValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_edge__edge0 = head_candidate_ensureMoveLeftValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveLeftValidRule_edge__edge0.lgspType.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target ensureMoveLeftValidRule_node__node0 from ensureMoveLeftValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node__node0 = candidate_ensureMoveLeftValidRule_edge__edge0.lgspTarget;
                        if(candidate_ensureMoveLeftValidRule_node__node0.lgspType.TypeID!=2) {
                            continue;
                        }
                        Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule match = matches.GetNextUnfilledPosition();
                        match._node_wv = candidate_ensureMoveLeftValidRule_node_wv;
                        match._node__node0 = candidate_ensureMoveLeftValidRule_node__node0;
                        match._node_bp = candidate_ensureMoveLeftValidRule_node_bp;
                        match._edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_ensureMoveLeftValidRule_node_wv.MoveOutHeadAfter(candidate_ensureMoveLeftValidRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_ensureMoveLeftValidRule_node_bp);
                            return;
                        }
                    }
                    while( (candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0.lgspOutNext) != head_candidate_ensureMoveLeftValidRule_edge__edge0 );
                }
label1: ;
            }
            return;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1])) {
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
            
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1])) {
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
            return ApplyStar(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_ensureMoveRightValidRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
    }
    
    public class Action_ensureMoveRightValidRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ensureMoveRightValidRule
    {
        public Action_ensureMoveRightValidRule() {
            _rulePattern = Rule_ensureMoveRightValidRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>(this);
        }

        public Rule_ensureMoveRightValidRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ensureMoveRightValidRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches;

        public static Action_ensureMoveRightValidRule Instance { get { return instance; } }
        private static Action_ensureMoveRightValidRule instance = new Action_ensureMoveRightValidRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset ensureMoveRightValidRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_wv = (GRGEN_LGSP.LGSPNode)ensureMoveRightValidRule_node_wv;
            if(candidate_ensureMoveRightValidRule_node_wv == null) {
                MissingPreset_ensureMoveRightValidRule_node_wv(graph, maxMatches, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveRightValidRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset ensureMoveRightValidRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_bp = (GRGEN_LGSP.LGSPNode)ensureMoveRightValidRule_node_bp;
            if(candidate_ensureMoveRightValidRule_node_bp == null) {
                MissingPreset_ensureMoveRightValidRule_node_bp(graph, maxMatches, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp, null, null, null, candidate_ensureMoveRightValidRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveRightValidRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            // NegativePattern 
            {
                ++negLevel;
                uint prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                prev_neg_0__candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_ensureMoveRightValidRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing ensureMoveRightValidRule_neg_0_edge__edge0 from ensureMoveRightValidRule_node_bp 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_node_bp.lgspOuthead;
                if(head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspType.TypeID!=3) {
                            continue;
                        }
                        // Implicit Target ensureMoveRightValidRule_neg_0_node__node0 from ensureMoveRightValidRule_neg_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_neg_0_node__node0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspTarget;
                        if(candidate_ensureMoveRightValidRule_neg_0_node__node0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ensureMoveRightValidRule_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // negative pattern found
                        candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                        --negLevel;
                        return matches;
                    }
                    while( (candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspOutNext) != head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 );
                }
                candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                --negLevel;
            }
            // Extend Outgoing ensureMoveRightValidRule_edge__edge0 from ensureMoveRightValidRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_node_wv.lgspOuthead;
            if(head_candidate_ensureMoveRightValidRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_edge__edge0 = head_candidate_ensureMoveRightValidRule_edge__edge0;
                do
                {
                    if(candidate_ensureMoveRightValidRule_edge__edge0.lgspType.TypeID!=7) {
                        continue;
                    }
                    // Implicit Target ensureMoveRightValidRule_node__node0 from ensureMoveRightValidRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node__node0 = candidate_ensureMoveRightValidRule_edge__edge0.lgspTarget;
                    if(candidate_ensureMoveRightValidRule_node__node0.lgspType.TypeID!=2) {
                        continue;
                    }
                    Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule match = matches.GetNextUnfilledPosition();
                    match._node_wv = candidate_ensureMoveRightValidRule_node_wv;
                    match._node__node0 = candidate_ensureMoveRightValidRule_node__node0;
                    match._node_bp = candidate_ensureMoveRightValidRule_node_bp;
                    match._edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_ensureMoveRightValidRule_node_wv.MoveOutHeadAfter(candidate_ensureMoveRightValidRule_edge__edge0);
                        return matches;
                    }
                }
                while( (candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0.lgspOutNext) != head_candidate_ensureMoveRightValidRule_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_ensureMoveRightValidRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup ensureMoveRightValidRule_node_wv 
            int type_id_candidate_ensureMoveRightValidRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveRightValidRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_ensureMoveRightValidRule_node_wv], candidate_ensureMoveRightValidRule_node_wv = head_candidate_ensureMoveRightValidRule_node_wv.lgspTypeNext; candidate_ensureMoveRightValidRule_node_wv != head_candidate_ensureMoveRightValidRule_node_wv; candidate_ensureMoveRightValidRule_node_wv = candidate_ensureMoveRightValidRule_node_wv.lgspTypeNext)
            {
                // Preset ensureMoveRightValidRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_bp = (GRGEN_LGSP.LGSPNode)ensureMoveRightValidRule_node_bp;
                if(candidate_ensureMoveRightValidRule_node_bp == null) {
                    MissingPreset_ensureMoveRightValidRule_node_bp(graph, maxMatches, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp, null, null, null, candidate_ensureMoveRightValidRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_ensureMoveRightValidRule_node_bp.lgspType.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveRightValidRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Outgoing ensureMoveRightValidRule_neg_0_edge__edge0 from ensureMoveRightValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_node_bp.lgspOuthead;
                    if(head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspType.TypeID!=3) {
                                continue;
                            }
                            // Implicit Target ensureMoveRightValidRule_neg_0_node__node0 from ensureMoveRightValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_neg_0_node__node0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspTarget;
                            if(candidate_ensureMoveRightValidRule_neg_0_node__node0.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveRightValidRule_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                            --negLevel;
                            goto label2;
                        }
                        while( (candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspOutNext) != head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveRightValidRule_edge__edge0 from ensureMoveRightValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_node_wv.lgspOuthead;
                if(head_candidate_ensureMoveRightValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_edge__edge0 = head_candidate_ensureMoveRightValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveRightValidRule_edge__edge0.lgspType.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target ensureMoveRightValidRule_node__node0 from ensureMoveRightValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node__node0 = candidate_ensureMoveRightValidRule_edge__edge0.lgspTarget;
                        if(candidate_ensureMoveRightValidRule_node__node0.lgspType.TypeID!=2) {
                            continue;
                        }
                        Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule match = matches.GetNextUnfilledPosition();
                        match._node_wv = candidate_ensureMoveRightValidRule_node_wv;
                        match._node__node0 = candidate_ensureMoveRightValidRule_node__node0;
                        match._node_bp = candidate_ensureMoveRightValidRule_node_bp;
                        match._edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_ensureMoveRightValidRule_node_wv.MoveOutHeadAfter(candidate_ensureMoveRightValidRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_ensureMoveRightValidRule_node_wv);
                            return;
                        }
                    }
                    while( (candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0.lgspOutNext) != head_candidate_ensureMoveRightValidRule_edge__edge0 );
                }
label2: ;
            }
            return;
        }
        public void MissingPreset_ensureMoveRightValidRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_wv)
        {
            int negLevel = 0;
            // Lookup ensureMoveRightValidRule_node_bp 
            int type_id_candidate_ensureMoveRightValidRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveRightValidRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_ensureMoveRightValidRule_node_bp], candidate_ensureMoveRightValidRule_node_bp = head_candidate_ensureMoveRightValidRule_node_bp.lgspTypeNext; candidate_ensureMoveRightValidRule_node_bp != head_candidate_ensureMoveRightValidRule_node_bp; candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.lgspTypeNext)
            {
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveRightValidRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Outgoing ensureMoveRightValidRule_neg_0_edge__edge0 from ensureMoveRightValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_node_bp.lgspOuthead;
                    if(head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspType.TypeID!=3) {
                                continue;
                            }
                            // Implicit Target ensureMoveRightValidRule_neg_0_node__node0 from ensureMoveRightValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_neg_0_node__node0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspTarget;
                            if(candidate_ensureMoveRightValidRule_neg_0_node__node0.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveRightValidRule_neg_0_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspOutNext) != head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveRightValidRule_edge__edge0 from ensureMoveRightValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_node_wv.lgspOuthead;
                if(head_candidate_ensureMoveRightValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_edge__edge0 = head_candidate_ensureMoveRightValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveRightValidRule_edge__edge0.lgspType.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target ensureMoveRightValidRule_node__node0 from ensureMoveRightValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node__node0 = candidate_ensureMoveRightValidRule_edge__edge0.lgspTarget;
                        if(candidate_ensureMoveRightValidRule_node__node0.lgspType.TypeID!=2) {
                            continue;
                        }
                        Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule match = matches.GetNextUnfilledPosition();
                        match._node_wv = candidate_ensureMoveRightValidRule_node_wv;
                        match._node__node0 = candidate_ensureMoveRightValidRule_node__node0;
                        match._node_bp = candidate_ensureMoveRightValidRule_node_bp;
                        match._edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_ensureMoveRightValidRule_node_wv.MoveOutHeadAfter(candidate_ensureMoveRightValidRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_ensureMoveRightValidRule_node_bp);
                            return;
                        }
                    }
                    while( (candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0.lgspOutNext) != head_candidate_ensureMoveRightValidRule_edge__edge0 );
                }
label3: ;
            }
            return;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1])) {
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
            
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1])) {
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
            return ApplyStar(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_moveLeftRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_moveLeftRule.IMatch_moveLeftRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
    }
    
    public class Action_moveLeftRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_moveLeftRule
    {
        public Action_moveLeftRule() {
            _rulePattern = Rule_moveLeftRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_moveLeftRule.Match_moveLeftRule, Rule_moveLeftRule.IMatch_moveLeftRule>(this);
        }

        public Rule_moveLeftRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "moveLeftRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_moveLeftRule.Match_moveLeftRule, Rule_moveLeftRule.IMatch_moveLeftRule> matches;

        public static Action_moveLeftRule Instance { get { return instance; } }
        private static Action_moveLeftRule instance = new Action_moveLeftRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset moveLeftRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_wv = (GRGEN_LGSP.LGSPNode)moveLeftRule_node_wv;
            if(candidate_moveLeftRule_node_wv == null) {
                MissingPreset_moveLeftRule_node_wv(graph, maxMatches, moveLeftRule_node_wv, moveLeftRule_node_bp, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveLeftRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset moveLeftRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_bp = (GRGEN_LGSP.LGSPNode)moveLeftRule_node_bp;
            if(candidate_moveLeftRule_node_bp == null) {
                MissingPreset_moveLeftRule_node_bp(graph, maxMatches, moveLeftRule_node_wv, moveLeftRule_node_bp, null, null, null, candidate_moveLeftRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveLeftRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            uint prev__candidate_moveLeftRule_node_bp;
            prev__candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_moveLeftRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Extend Outgoing moveLeftRule_edge__edge0 from moveLeftRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_node_wv.lgspOuthead;
            if(head_candidate_moveLeftRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge0 = head_candidate_moveLeftRule_edge__edge0;
                do
                {
                    if(candidate_moveLeftRule_edge__edge0.lgspType.TypeID!=6) {
                        continue;
                    }
                    // Implicit Target moveLeftRule_node_s from moveLeftRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_s = candidate_moveLeftRule_edge__edge0.lgspTarget;
                    if(candidate_moveLeftRule_node_s.lgspType.TypeID!=2) {
                        continue;
                    }
                    // Extend Incoming moveLeftRule_edge__edge1 from moveLeftRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_node_bp.lgspInhead;
                    if(head_candidate_moveLeftRule_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge1 = head_candidate_moveLeftRule_edge__edge1;
                        do
                        {
                            if(candidate_moveLeftRule_edge__edge1.lgspType.TypeID!=3) {
                                continue;
                            }
                            // Implicit Source moveLeftRule_node_lbp from moveLeftRule_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_lbp = candidate_moveLeftRule_edge__edge1.lgspSource;
                            if(candidate_moveLeftRule_node_lbp.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_moveLeftRule_node_lbp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            Rule_moveLeftRule.Match_moveLeftRule match = matches.GetNextUnfilledPosition();
                            match._node_wv = candidate_moveLeftRule_node_wv;
                            match._node_s = candidate_moveLeftRule_node_s;
                            match._node_lbp = candidate_moveLeftRule_node_lbp;
                            match._node_bp = candidate_moveLeftRule_node_bp;
                            match._edge__edge0 = candidate_moveLeftRule_edge__edge0;
                            match._edge__edge1 = candidate_moveLeftRule_edge__edge1;
                            matches.PositionWasFilledFixIt();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_moveLeftRule_node_bp.MoveInHeadAfter(candidate_moveLeftRule_edge__edge1);
                                candidate_moveLeftRule_node_wv.MoveOutHeadAfter(candidate_moveLeftRule_edge__edge0);
                                candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
                                return matches;
                            }
                        }
                        while( (candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_edge__edge1.lgspInNext) != head_candidate_moveLeftRule_edge__edge1 );
                    }
                }
                while( (candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_edge__edge0.lgspOutNext) != head_candidate_moveLeftRule_edge__edge0 );
            }
            candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
            return matches;
        }
        public void MissingPreset_moveLeftRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup moveLeftRule_node_wv 
            int type_id_candidate_moveLeftRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveLeftRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_moveLeftRule_node_wv], candidate_moveLeftRule_node_wv = head_candidate_moveLeftRule_node_wv.lgspTypeNext; candidate_moveLeftRule_node_wv != head_candidate_moveLeftRule_node_wv; candidate_moveLeftRule_node_wv = candidate_moveLeftRule_node_wv.lgspTypeNext)
            {
                // Preset moveLeftRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_bp = (GRGEN_LGSP.LGSPNode)moveLeftRule_node_bp;
                if(candidate_moveLeftRule_node_bp == null) {
                    MissingPreset_moveLeftRule_node_bp(graph, maxMatches, moveLeftRule_node_wv, moveLeftRule_node_bp, null, null, null, candidate_moveLeftRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_moveLeftRule_node_bp.lgspType.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_moveLeftRule_node_bp;
                prev__candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveLeftRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveLeftRule_edge__edge0 from moveLeftRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_node_wv.lgspOuthead;
                if(head_candidate_moveLeftRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge0 = head_candidate_moveLeftRule_edge__edge0;
                    do
                    {
                        if(candidate_moveLeftRule_edge__edge0.lgspType.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target moveLeftRule_node_s from moveLeftRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_s = candidate_moveLeftRule_edge__edge0.lgspTarget;
                        if(candidate_moveLeftRule_node_s.lgspType.TypeID!=2) {
                            continue;
                        }
                        // Extend Incoming moveLeftRule_edge__edge1 from moveLeftRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_node_bp.lgspInhead;
                        if(head_candidate_moveLeftRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge1 = head_candidate_moveLeftRule_edge__edge1;
                            do
                            {
                                if(candidate_moveLeftRule_edge__edge1.lgspType.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Source moveLeftRule_node_lbp from moveLeftRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_lbp = candidate_moveLeftRule_edge__edge1.lgspSource;
                                if(candidate_moveLeftRule_node_lbp.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveLeftRule_node_lbp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                Rule_moveLeftRule.Match_moveLeftRule match = matches.GetNextUnfilledPosition();
                                match._node_wv = candidate_moveLeftRule_node_wv;
                                match._node_s = candidate_moveLeftRule_node_s;
                                match._node_lbp = candidate_moveLeftRule_node_lbp;
                                match._node_bp = candidate_moveLeftRule_node_bp;
                                match._edge__edge0 = candidate_moveLeftRule_edge__edge0;
                                match._edge__edge1 = candidate_moveLeftRule_edge__edge1;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    candidate_moveLeftRule_node_bp.MoveInHeadAfter(candidate_moveLeftRule_edge__edge1);
                                    candidate_moveLeftRule_node_wv.MoveOutHeadAfter(candidate_moveLeftRule_edge__edge0);
                                    graph.MoveHeadAfter(candidate_moveLeftRule_node_wv);
                                    candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_edge__edge1.lgspInNext) != head_candidate_moveLeftRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_edge__edge0.lgspOutNext) != head_candidate_moveLeftRule_edge__edge0 );
                }
                candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
            }
            return;
        }
        public void MissingPreset_moveLeftRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_wv)
        {
            int negLevel = 0;
            // Lookup moveLeftRule_node_bp 
            int type_id_candidate_moveLeftRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveLeftRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_moveLeftRule_node_bp], candidate_moveLeftRule_node_bp = head_candidate_moveLeftRule_node_bp.lgspTypeNext; candidate_moveLeftRule_node_bp != head_candidate_moveLeftRule_node_bp; candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.lgspTypeNext)
            {
                uint prev__candidate_moveLeftRule_node_bp;
                prev__candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveLeftRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveLeftRule_edge__edge0 from moveLeftRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_node_wv.lgspOuthead;
                if(head_candidate_moveLeftRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge0 = head_candidate_moveLeftRule_edge__edge0;
                    do
                    {
                        if(candidate_moveLeftRule_edge__edge0.lgspType.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target moveLeftRule_node_s from moveLeftRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_s = candidate_moveLeftRule_edge__edge0.lgspTarget;
                        if(candidate_moveLeftRule_node_s.lgspType.TypeID!=2) {
                            continue;
                        }
                        // Extend Incoming moveLeftRule_edge__edge1 from moveLeftRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_node_bp.lgspInhead;
                        if(head_candidate_moveLeftRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge1 = head_candidate_moveLeftRule_edge__edge1;
                            do
                            {
                                if(candidate_moveLeftRule_edge__edge1.lgspType.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Source moveLeftRule_node_lbp from moveLeftRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_lbp = candidate_moveLeftRule_edge__edge1.lgspSource;
                                if(candidate_moveLeftRule_node_lbp.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveLeftRule_node_lbp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                Rule_moveLeftRule.Match_moveLeftRule match = matches.GetNextUnfilledPosition();
                                match._node_wv = candidate_moveLeftRule_node_wv;
                                match._node_s = candidate_moveLeftRule_node_s;
                                match._node_lbp = candidate_moveLeftRule_node_lbp;
                                match._node_bp = candidate_moveLeftRule_node_bp;
                                match._edge__edge0 = candidate_moveLeftRule_edge__edge0;
                                match._edge__edge1 = candidate_moveLeftRule_edge__edge1;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    candidate_moveLeftRule_node_bp.MoveInHeadAfter(candidate_moveLeftRule_edge__edge1);
                                    candidate_moveLeftRule_node_wv.MoveOutHeadAfter(candidate_moveLeftRule_edge__edge0);
                                    graph.MoveHeadAfter(candidate_moveLeftRule_node_bp);
                                    candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_edge__edge1.lgspInNext) != head_candidate_moveLeftRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_edge__edge0.lgspOutNext) != head_candidate_moveLeftRule_edge__edge0 );
                }
                candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
            }
            return;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, moveLeftRule_node_wv, moveLeftRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_moveLeftRule.IMatch_moveLeftRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
        {
            output_0 = null;
            output_1 = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_moveLeftRule.IMatch_moveLeftRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            } else {
                foreach(Rule_moveLeftRule.IMatch_moveLeftRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, moveLeftRule_node_wv, moveLeftRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_moveLeftRule.IMatch_moveLeftRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            } else {
                foreach(Rule_moveLeftRule.IMatch_moveLeftRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            Modify(graph, (Rule_moveLeftRule.IMatch_moveLeftRule)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            GRGEN_MODEL.IState output_0 = null; GRGEN_MODEL.IBandPosition output_1 = null; 
            if(Apply(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
            GRGEN_MODEL.IState output_0 = null; GRGEN_MODEL.IBandPosition output_1 = null; 
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
            return ApplyStar(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_moveRightRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_moveRightRule.IMatch_moveRightRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
    }
    
    public class Action_moveRightRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_moveRightRule
    {
        public Action_moveRightRule() {
            _rulePattern = Rule_moveRightRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_moveRightRule.Match_moveRightRule, Rule_moveRightRule.IMatch_moveRightRule>(this);
        }

        public Rule_moveRightRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "moveRightRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_moveRightRule.Match_moveRightRule, Rule_moveRightRule.IMatch_moveRightRule> matches;

        public static Action_moveRightRule Instance { get { return instance; } }
        private static Action_moveRightRule instance = new Action_moveRightRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset moveRightRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_wv = (GRGEN_LGSP.LGSPNode)moveRightRule_node_wv;
            if(candidate_moveRightRule_node_wv == null) {
                MissingPreset_moveRightRule_node_wv(graph, maxMatches, moveRightRule_node_wv, moveRightRule_node_bp, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveRightRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset moveRightRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_bp = (GRGEN_LGSP.LGSPNode)moveRightRule_node_bp;
            if(candidate_moveRightRule_node_bp == null) {
                MissingPreset_moveRightRule_node_bp(graph, maxMatches, moveRightRule_node_wv, moveRightRule_node_bp, null, null, null, candidate_moveRightRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveRightRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            uint prev__candidate_moveRightRule_node_bp;
            prev__candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_moveRightRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Extend Outgoing moveRightRule_edge__edge0 from moveRightRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_node_wv.lgspOuthead;
            if(head_candidate_moveRightRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge0 = head_candidate_moveRightRule_edge__edge0;
                do
                {
                    if(candidate_moveRightRule_edge__edge0.lgspType.TypeID!=7) {
                        continue;
                    }
                    // Implicit Target moveRightRule_node_s from moveRightRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_s = candidate_moveRightRule_edge__edge0.lgspTarget;
                    if(candidate_moveRightRule_node_s.lgspType.TypeID!=2) {
                        continue;
                    }
                    // Extend Outgoing moveRightRule_edge__edge1 from moveRightRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_node_bp.lgspOuthead;
                    if(head_candidate_moveRightRule_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge1 = head_candidate_moveRightRule_edge__edge1;
                        do
                        {
                            if(candidate_moveRightRule_edge__edge1.lgspType.TypeID!=3) {
                                continue;
                            }
                            // Implicit Target moveRightRule_node_rbp from moveRightRule_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_rbp = candidate_moveRightRule_edge__edge1.lgspTarget;
                            if(candidate_moveRightRule_node_rbp.lgspType.TypeID!=1) {
                                continue;
                            }
                            if((candidate_moveRightRule_node_rbp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            Rule_moveRightRule.Match_moveRightRule match = matches.GetNextUnfilledPosition();
                            match._node_wv = candidate_moveRightRule_node_wv;
                            match._node_s = candidate_moveRightRule_node_s;
                            match._node_bp = candidate_moveRightRule_node_bp;
                            match._node_rbp = candidate_moveRightRule_node_rbp;
                            match._edge__edge0 = candidate_moveRightRule_edge__edge0;
                            match._edge__edge1 = candidate_moveRightRule_edge__edge1;
                            matches.PositionWasFilledFixIt();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_moveRightRule_node_bp.MoveOutHeadAfter(candidate_moveRightRule_edge__edge1);
                                candidate_moveRightRule_node_wv.MoveOutHeadAfter(candidate_moveRightRule_edge__edge0);
                                candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
                                return matches;
                            }
                        }
                        while( (candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_edge__edge1.lgspOutNext) != head_candidate_moveRightRule_edge__edge1 );
                    }
                }
                while( (candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_edge__edge0.lgspOutNext) != head_candidate_moveRightRule_edge__edge0 );
            }
            candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
            return matches;
        }
        public void MissingPreset_moveRightRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup moveRightRule_node_wv 
            int type_id_candidate_moveRightRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveRightRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_moveRightRule_node_wv], candidate_moveRightRule_node_wv = head_candidate_moveRightRule_node_wv.lgspTypeNext; candidate_moveRightRule_node_wv != head_candidate_moveRightRule_node_wv; candidate_moveRightRule_node_wv = candidate_moveRightRule_node_wv.lgspTypeNext)
            {
                // Preset moveRightRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_bp = (GRGEN_LGSP.LGSPNode)moveRightRule_node_bp;
                if(candidate_moveRightRule_node_bp == null) {
                    MissingPreset_moveRightRule_node_bp(graph, maxMatches, moveRightRule_node_wv, moveRightRule_node_bp, null, null, null, candidate_moveRightRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_moveRightRule_node_bp.lgspType.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_moveRightRule_node_bp;
                prev__candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveRightRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveRightRule_edge__edge0 from moveRightRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_node_wv.lgspOuthead;
                if(head_candidate_moveRightRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge0 = head_candidate_moveRightRule_edge__edge0;
                    do
                    {
                        if(candidate_moveRightRule_edge__edge0.lgspType.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target moveRightRule_node_s from moveRightRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_s = candidate_moveRightRule_edge__edge0.lgspTarget;
                        if(candidate_moveRightRule_node_s.lgspType.TypeID!=2) {
                            continue;
                        }
                        // Extend Outgoing moveRightRule_edge__edge1 from moveRightRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_node_bp.lgspOuthead;
                        if(head_candidate_moveRightRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge1 = head_candidate_moveRightRule_edge__edge1;
                            do
                            {
                                if(candidate_moveRightRule_edge__edge1.lgspType.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Target moveRightRule_node_rbp from moveRightRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_rbp = candidate_moveRightRule_edge__edge1.lgspTarget;
                                if(candidate_moveRightRule_node_rbp.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveRightRule_node_rbp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                Rule_moveRightRule.Match_moveRightRule match = matches.GetNextUnfilledPosition();
                                match._node_wv = candidate_moveRightRule_node_wv;
                                match._node_s = candidate_moveRightRule_node_s;
                                match._node_bp = candidate_moveRightRule_node_bp;
                                match._node_rbp = candidate_moveRightRule_node_rbp;
                                match._edge__edge0 = candidate_moveRightRule_edge__edge0;
                                match._edge__edge1 = candidate_moveRightRule_edge__edge1;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    candidate_moveRightRule_node_bp.MoveOutHeadAfter(candidate_moveRightRule_edge__edge1);
                                    candidate_moveRightRule_node_wv.MoveOutHeadAfter(candidate_moveRightRule_edge__edge0);
                                    graph.MoveHeadAfter(candidate_moveRightRule_node_wv);
                                    candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_edge__edge1.lgspOutNext) != head_candidate_moveRightRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_edge__edge0.lgspOutNext) != head_candidate_moveRightRule_edge__edge0 );
                }
                candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
            }
            return;
        }
        public void MissingPreset_moveRightRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_wv)
        {
            int negLevel = 0;
            // Lookup moveRightRule_node_bp 
            int type_id_candidate_moveRightRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveRightRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_moveRightRule_node_bp], candidate_moveRightRule_node_bp = head_candidate_moveRightRule_node_bp.lgspTypeNext; candidate_moveRightRule_node_bp != head_candidate_moveRightRule_node_bp; candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.lgspTypeNext)
            {
                uint prev__candidate_moveRightRule_node_bp;
                prev__candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveRightRule_node_bp.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveRightRule_edge__edge0 from moveRightRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_node_wv.lgspOuthead;
                if(head_candidate_moveRightRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge0 = head_candidate_moveRightRule_edge__edge0;
                    do
                    {
                        if(candidate_moveRightRule_edge__edge0.lgspType.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target moveRightRule_node_s from moveRightRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_s = candidate_moveRightRule_edge__edge0.lgspTarget;
                        if(candidate_moveRightRule_node_s.lgspType.TypeID!=2) {
                            continue;
                        }
                        // Extend Outgoing moveRightRule_edge__edge1 from moveRightRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_node_bp.lgspOuthead;
                        if(head_candidate_moveRightRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge1 = head_candidate_moveRightRule_edge__edge1;
                            do
                            {
                                if(candidate_moveRightRule_edge__edge1.lgspType.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Target moveRightRule_node_rbp from moveRightRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_rbp = candidate_moveRightRule_edge__edge1.lgspTarget;
                                if(candidate_moveRightRule_node_rbp.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveRightRule_node_rbp.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                Rule_moveRightRule.Match_moveRightRule match = matches.GetNextUnfilledPosition();
                                match._node_wv = candidate_moveRightRule_node_wv;
                                match._node_s = candidate_moveRightRule_node_s;
                                match._node_bp = candidate_moveRightRule_node_bp;
                                match._node_rbp = candidate_moveRightRule_node_rbp;
                                match._edge__edge0 = candidate_moveRightRule_edge__edge0;
                                match._edge__edge1 = candidate_moveRightRule_edge__edge1;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    candidate_moveRightRule_node_bp.MoveOutHeadAfter(candidate_moveRightRule_edge__edge1);
                                    candidate_moveRightRule_node_wv.MoveOutHeadAfter(candidate_moveRightRule_edge__edge0);
                                    graph.MoveHeadAfter(candidate_moveRightRule_node_bp);
                                    candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_edge__edge1.lgspOutNext) != head_candidate_moveRightRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_edge__edge0.lgspOutNext) != head_candidate_moveRightRule_edge__edge0 );
                }
                candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
            }
            return;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, moveRightRule_node_wv, moveRightRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_moveRightRule.IMatch_moveRightRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
        {
            output_0 = null;
            output_1 = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_moveRightRule.IMatch_moveRightRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            } else {
                foreach(Rule_moveRightRule.IMatch_moveRightRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveRightRule_node_wv, moveRightRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches, moveRightRule_node_wv, moveRightRule_node_bp);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_moveRightRule.IMatch_moveRightRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            } else {
                foreach(Rule_moveRightRule.IMatch_moveRightRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match, out output_0, out output_1);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveRightRule_node_wv, moveRightRule_node_bp);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveRightRule_node_wv, moveRightRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveRightRule_node_wv, moveRightRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1, moveRightRule_node_wv, moveRightRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            Modify(graph, (Rule_moveRightRule.IMatch_moveRightRule)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            GRGEN_MODEL.IState output_0 = null; GRGEN_MODEL.IBandPosition output_1 = null; 
            if(Apply(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
            GRGEN_MODEL.IState output_0 = null; GRGEN_MODEL.IBandPosition output_1 = null; 
            if(ApplyAll(maxMatches, graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
            return ApplyStar(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_countZeros
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_countZeros.IMatch_countZeros match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches);
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
    
    public class Action_countZeros : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_countZeros
    {
        public Action_countZeros() {
            _rulePattern = Rule_countZeros.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_countZeros.Match_countZeros, Rule_countZeros.IMatch_countZeros>(this);
        }

        public Rule_countZeros _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "countZeros"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_countZeros.Match_countZeros, Rule_countZeros.IMatch_countZeros> matches;

        public static Action_countZeros Instance { get { return instance; } }
        private static Action_countZeros instance = new Action_countZeros();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup countZeros_node_bp 
            int type_id_candidate_countZeros_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_countZeros_node_bp = graph.nodesByTypeHeads[type_id_candidate_countZeros_node_bp], candidate_countZeros_node_bp = head_candidate_countZeros_node_bp.lgspTypeNext; candidate_countZeros_node_bp != head_candidate_countZeros_node_bp; candidate_countZeros_node_bp = candidate_countZeros_node_bp.lgspTypeNext)
            {
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_countZeros_node_bp).@value == 0))) {
                    continue;
                }
                Rule_countZeros.Match_countZeros match = matches.GetNextUnfilledPosition();
                match._node_bp = candidate_countZeros_node_bp;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_countZeros_node_bp);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_countZeros.IMatch_countZeros match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_countZeros.IMatch_countZeros match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_countZeros.IMatch_countZeros match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_countZeros.IMatch_countZeros match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_countZeros.IMatch_countZeros match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_countZeros.IMatch_countZeros)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros>)matches);
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
    public interface IAction_countOnes
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_countOnes.IMatch_countOnes match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches);
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
    
    public class Action_countOnes : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_countOnes
    {
        public Action_countOnes() {
            _rulePattern = Rule_countOnes.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_countOnes.Match_countOnes, Rule_countOnes.IMatch_countOnes>(this);
        }

        public Rule_countOnes _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "countOnes"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_countOnes.Match_countOnes, Rule_countOnes.IMatch_countOnes> matches;

        public static Action_countOnes Instance { get { return instance; } }
        private static Action_countOnes instance = new Action_countOnes();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup countOnes_node_bp 
            int type_id_candidate_countOnes_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_countOnes_node_bp = graph.nodesByTypeHeads[type_id_candidate_countOnes_node_bp], candidate_countOnes_node_bp = head_candidate_countOnes_node_bp.lgspTypeNext; candidate_countOnes_node_bp != head_candidate_countOnes_node_bp; candidate_countOnes_node_bp = candidate_countOnes_node_bp.lgspTypeNext)
            {
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_countOnes_node_bp).@value == 1))) {
                    continue;
                }
                Rule_countOnes.Match_countOnes match = matches.GetNextUnfilledPosition();
                match._node_bp = candidate_countOnes_node_bp;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_countOnes_node_bp);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_countOnes.IMatch_countOnes match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_countOnes.IMatch_countOnes match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_countOnes.IMatch_countOnes match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_countOnes.IMatch_countOnes match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_countOnes.IMatch_countOnes match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_countOnes.IMatch_countOnes)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes>)matches);
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
    public class Turing3Actions : GRGEN_LGSP.LGSPActions
    {
        public Turing3Actions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public Turing3Actions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Rule_readZeroRule.Instance);
            actions.Add("readZeroRule", (GRGEN_LGSP.LGSPAction) Action_readZeroRule.Instance);
            @readZeroRule = Action_readZeroRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_readOneRule.Instance);
            actions.Add("readOneRule", (GRGEN_LGSP.LGSPAction) Action_readOneRule.Instance);
            @readOneRule = Action_readOneRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_ensureMoveLeftValidRule.Instance);
            actions.Add("ensureMoveLeftValidRule", (GRGEN_LGSP.LGSPAction) Action_ensureMoveLeftValidRule.Instance);
            @ensureMoveLeftValidRule = Action_ensureMoveLeftValidRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_ensureMoveRightValidRule.Instance);
            actions.Add("ensureMoveRightValidRule", (GRGEN_LGSP.LGSPAction) Action_ensureMoveRightValidRule.Instance);
            @ensureMoveRightValidRule = Action_ensureMoveRightValidRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_moveLeftRule.Instance);
            actions.Add("moveLeftRule", (GRGEN_LGSP.LGSPAction) Action_moveLeftRule.Instance);
            @moveLeftRule = Action_moveLeftRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_moveRightRule.Instance);
            actions.Add("moveRightRule", (GRGEN_LGSP.LGSPAction) Action_moveRightRule.Instance);
            @moveRightRule = Action_moveRightRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_countZeros.Instance);
            actions.Add("countZeros", (GRGEN_LGSP.LGSPAction) Action_countZeros.Instance);
            @countZeros = Action_countZeros.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_countOnes.Instance);
            actions.Add("countOnes", (GRGEN_LGSP.LGSPAction) Action_countOnes.Instance);
            @countOnes = Action_countOnes.Instance;
            analyzer.ComputeInterPatternRelations();
        }
        
        public IAction_readZeroRule @readZeroRule;
        public IAction_readOneRule @readOneRule;
        public IAction_ensureMoveLeftValidRule @ensureMoveLeftValidRule;
        public IAction_ensureMoveRightValidRule @ensureMoveRightValidRule;
        public IAction_moveLeftRule @moveLeftRule;
        public IAction_moveRightRule @moveRightRule;
        public IAction_countZeros @countZeros;
        public IAction_countOnes @countOnes;
        
        public override string Name { get { return "Turing3Actions"; } }
        public override string ModelMD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
    }
}