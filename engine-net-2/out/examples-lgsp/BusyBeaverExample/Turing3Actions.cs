// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Turing3\Turing3.grg" on Thu Mar 26 22:28:47 GMT+01:00 2009

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

		private static object[] ReturnArray = new object[1];

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
			GRGEN_LGSP.PatternNode readZeroRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "readZeroRule_node_s", "s", readZeroRule_node_s_AllowedTypes, readZeroRule_node_s_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode readZeroRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "readZeroRule_node_wv", "wv", readZeroRule_node_wv_AllowedTypes, readZeroRule_node_wv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode readZeroRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "readZeroRule_node_bp", "bp", readZeroRule_node_bp_AllowedTypes, readZeroRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternEdge readZeroRule_edge_rv = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@readZero, "GRGEN_MODEL.IreadZero", "readZeroRule_edge_rv", "rv", readZeroRule_edge_rv_AllowedTypes, readZeroRule_edge_rv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", "readZeroRule_node_bp", "value"), new GRGEN_EXPR.Constant("0")),
				new string[] { "readZeroRule_node_bp" }, new string[] {  }, new string[] {  });
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
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
			ReturnArray[0] = node_wv;
			return ReturnArray;
		}
		private static string[] readZeroRule_addedNodeNames = new string[] {  };
		private static string[] readZeroRule_addedEdgeNames = new string[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
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
			ReturnArray[0] = node_wv;
			return ReturnArray;
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
			//Independents
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

		private static object[] ReturnArray = new object[1];

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
			GRGEN_LGSP.PatternNode readOneRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, "GRGEN_MODEL.IState", "readOneRule_node_s", "s", readOneRule_node_s_AllowedTypes, readOneRule_node_s_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode readOneRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, "GRGEN_MODEL.IWriteValue", "readOneRule_node_wv", "wv", readOneRule_node_wv_AllowedTypes, readOneRule_node_wv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode readOneRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, "GRGEN_MODEL.IBandPosition", "readOneRule_node_bp", "bp", readOneRule_node_bp_AllowedTypes, readOneRule_node_bp_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternEdge readOneRule_edge_rv = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@readOne, "GRGEN_MODEL.IreadOne", "readOneRule_edge_rv", "rv", readOneRule_edge_rv_AllowedTypes, readOneRule_edge_rv_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", "readOneRule_node_bp", "value"), new GRGEN_EXPR.Constant("1")),
				new string[] { "readOneRule_node_bp" }, new string[] {  }, new string[] {  });
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
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
			ReturnArray[0] = node_wv;
			return ReturnArray;
		}
		private static string[] readOneRule_addedNodeNames = new string[] {  };
		private static string[] readOneRule_addedEdgeNames = new string[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
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
			ReturnArray[0] = node_wv;
			return ReturnArray;
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
			//Independents
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

		private static object[] ReturnArray = new object[0];

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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveLeftValidRule curMatch = (Match_ensureMoveLeftValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveLeftValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveLeftValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node__node1, node_bp);
			return EmptyReturnElements;
		}
		private static string[] ensureMoveLeftValidRule_addedNodeNames = new string[] { "_node1" };
		private static string[] ensureMoveLeftValidRule_addedEdgeNames = new string[] { "_edge1" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveLeftValidRule curMatch = (Match_ensureMoveLeftValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveLeftValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveLeftValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node__node1, node_bp);
			return EmptyReturnElements;
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
			//Independents
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
			//Independents
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

		private static object[] ReturnArray = new object[0];

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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveRightValidRule curMatch = (Match_ensureMoveRightValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveRightValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveRightValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node_bp, node__node1);
			return EmptyReturnElements;
		}
		private static string[] ensureMoveRightValidRule_addedNodeNames = new string[] { "_node1" };
		private static string[] ensureMoveRightValidRule_addedEdgeNames = new string[] { "_edge1" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ensureMoveRightValidRule curMatch = (Match_ensureMoveRightValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveRightValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveRightValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node_bp, node__node1);
			return EmptyReturnElements;
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
			//Independents
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
			//Independents
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

		private static object[] ReturnArray = new object[2];

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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_moveLeftRule curMatch = (Match_moveLeftRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_lbp = curMatch._node_lbp;
			graph.SettingAddedNodeNames( moveLeftRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveLeftRule_addedEdgeNames );
			ReturnArray[0] = node_s;
			ReturnArray[1] = node_lbp;
			return ReturnArray;
		}
		private static string[] moveLeftRule_addedNodeNames = new string[] {  };
		private static string[] moveLeftRule_addedEdgeNames = new string[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_moveLeftRule curMatch = (Match_moveLeftRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_lbp = curMatch._node_lbp;
			graph.SettingAddedNodeNames( moveLeftRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveLeftRule_addedEdgeNames );
			ReturnArray[0] = node_s;
			ReturnArray[1] = node_lbp;
			return ReturnArray;
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
			//Independents
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

		private static object[] ReturnArray = new object[2];

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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_moveRightRule curMatch = (Match_moveRightRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_rbp = curMatch._node_rbp;
			graph.SettingAddedNodeNames( moveRightRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveRightRule_addedEdgeNames );
			ReturnArray[0] = node_s;
			ReturnArray[1] = node_rbp;
			return ReturnArray;
		}
		private static string[] moveRightRule_addedNodeNames = new string[] {  };
		private static string[] moveRightRule_addedEdgeNames = new string[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_moveRightRule curMatch = (Match_moveRightRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_rbp = curMatch._node_rbp;
			graph.SettingAddedNodeNames( moveRightRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveRightRule_addedEdgeNames );
			ReturnArray[0] = node_s;
			ReturnArray[1] = node_rbp;
			return ReturnArray;
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
			//Independents
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

	public class Turing3_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public Turing3_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[6];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+6];
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
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
	}


    public class Action_readZeroRule : GRGEN_LGSP.LGSPAction
    {
        public Action_readZeroRule() {
            rulePattern = Rule_readZeroRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_readZeroRule.Match_readZeroRule>(this);
        }

        public override string Name { get { return "readZeroRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_readZeroRule.Match_readZeroRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_readZeroRule instance = new Action_readZeroRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset readZeroRule_node_s 
            GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_s = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_readZeroRule_node_s == null) {
                MissingPreset_readZeroRule_node_s(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readZeroRule_node_s.type.TypeID!=2) {
                return matches;
            }
            // Preset readZeroRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_readZeroRule_node_bp == null) {
                MissingPreset_readZeroRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_readZeroRule_node_s);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readZeroRule_node_bp.type.TypeID!=1) {
                return matches;
            }
            // Condition 
            if(!((((GRGEN_MODEL.IBandPosition)candidate_readZeroRule_node_bp).@value == 0))) {
                return matches;
            }
            // Extend Outgoing readZeroRule_edge_rv from readZeroRule_node_s 
            GRGEN_LGSP.LGSPEdge head_candidate_readZeroRule_edge_rv = candidate_readZeroRule_node_s.outhead;
            if(head_candidate_readZeroRule_edge_rv != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_readZeroRule_edge_rv = head_candidate_readZeroRule_edge_rv;
                do
                {
                    if(candidate_readZeroRule_edge_rv.type.TypeID!=4) {
                        continue;
                    }
                    // Implicit Target readZeroRule_node_wv from readZeroRule_edge_rv 
                    GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_wv = candidate_readZeroRule_edge_rv.target;
                    if(candidate_readZeroRule_node_wv.type.TypeID!=3) {
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
                while( (candidate_readZeroRule_edge_rv = candidate_readZeroRule_edge_rv.outNext) != head_candidate_readZeroRule_edge_rv );
            }
            return matches;
        }
        public void MissingPreset_readZeroRule_node_s(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup readZeroRule_node_s 
            int type_id_candidate_readZeroRule_node_s = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_readZeroRule_node_s = graph.nodesByTypeHeads[type_id_candidate_readZeroRule_node_s], candidate_readZeroRule_node_s = head_candidate_readZeroRule_node_s.typeNext; candidate_readZeroRule_node_s != head_candidate_readZeroRule_node_s; candidate_readZeroRule_node_s = candidate_readZeroRule_node_s.typeNext)
            {
                // Preset readZeroRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_readZeroRule_node_bp == null) {
                    MissingPreset_readZeroRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_readZeroRule_node_s);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_readZeroRule_node_bp.type.TypeID!=1) {
                    continue;
                }
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readZeroRule_node_bp).@value == 0))) {
                    continue;
                }
                // Extend Outgoing readZeroRule_edge_rv from readZeroRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readZeroRule_edge_rv = candidate_readZeroRule_node_s.outhead;
                if(head_candidate_readZeroRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readZeroRule_edge_rv = head_candidate_readZeroRule_edge_rv;
                    do
                    {
                        if(candidate_readZeroRule_edge_rv.type.TypeID!=4) {
                            continue;
                        }
                        // Implicit Target readZeroRule_node_wv from readZeroRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_wv = candidate_readZeroRule_edge_rv.target;
                        if(candidate_readZeroRule_node_wv.type.TypeID!=3) {
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
                    while( (candidate_readZeroRule_edge_rv = candidate_readZeroRule_edge_rv.outNext) != head_candidate_readZeroRule_edge_rv );
                }
            }
            return;
        }
        public void MissingPreset_readZeroRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_s)
        {
            int negLevel = 0;
            // Lookup readZeroRule_node_bp 
            int type_id_candidate_readZeroRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_readZeroRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_readZeroRule_node_bp], candidate_readZeroRule_node_bp = head_candidate_readZeroRule_node_bp.typeNext; candidate_readZeroRule_node_bp != head_candidate_readZeroRule_node_bp; candidate_readZeroRule_node_bp = candidate_readZeroRule_node_bp.typeNext)
            {
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readZeroRule_node_bp).@value == 0))) {
                    continue;
                }
                // Extend Outgoing readZeroRule_edge_rv from readZeroRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readZeroRule_edge_rv = candidate_readZeroRule_node_s.outhead;
                if(head_candidate_readZeroRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readZeroRule_edge_rv = head_candidate_readZeroRule_edge_rv;
                    do
                    {
                        if(candidate_readZeroRule_edge_rv.type.TypeID!=4) {
                            continue;
                        }
                        // Implicit Target readZeroRule_node_wv from readZeroRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_wv = candidate_readZeroRule_edge_rv.target;
                        if(candidate_readZeroRule_node_wv.type.TypeID!=3) {
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
                    while( (candidate_readZeroRule_edge_rv = candidate_readZeroRule_edge_rv.outNext) != head_candidate_readZeroRule_edge_rv );
                }
            }
            return;
        }
    }

    public class Action_readOneRule : GRGEN_LGSP.LGSPAction
    {
        public Action_readOneRule() {
            rulePattern = Rule_readOneRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_readOneRule.Match_readOneRule>(this);
        }

        public override string Name { get { return "readOneRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_readOneRule.Match_readOneRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_readOneRule instance = new Action_readOneRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset readOneRule_node_s 
            GRGEN_LGSP.LGSPNode candidate_readOneRule_node_s = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_readOneRule_node_s == null) {
                MissingPreset_readOneRule_node_s(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readOneRule_node_s.type.TypeID!=2) {
                return matches;
            }
            // Preset readOneRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_readOneRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_readOneRule_node_bp == null) {
                MissingPreset_readOneRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_readOneRule_node_s);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_readOneRule_node_bp.type.TypeID!=1) {
                return matches;
            }
            // Condition 
            if(!((((GRGEN_MODEL.IBandPosition)candidate_readOneRule_node_bp).@value == 1))) {
                return matches;
            }
            // Extend Outgoing readOneRule_edge_rv from readOneRule_node_s 
            GRGEN_LGSP.LGSPEdge head_candidate_readOneRule_edge_rv = candidate_readOneRule_node_s.outhead;
            if(head_candidate_readOneRule_edge_rv != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_readOneRule_edge_rv = head_candidate_readOneRule_edge_rv;
                do
                {
                    if(candidate_readOneRule_edge_rv.type.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target readOneRule_node_wv from readOneRule_edge_rv 
                    GRGEN_LGSP.LGSPNode candidate_readOneRule_node_wv = candidate_readOneRule_edge_rv.target;
                    if(candidate_readOneRule_node_wv.type.TypeID!=3) {
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
                while( (candidate_readOneRule_edge_rv = candidate_readOneRule_edge_rv.outNext) != head_candidate_readOneRule_edge_rv );
            }
            return matches;
        }
        public void MissingPreset_readOneRule_node_s(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup readOneRule_node_s 
            int type_id_candidate_readOneRule_node_s = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_readOneRule_node_s = graph.nodesByTypeHeads[type_id_candidate_readOneRule_node_s], candidate_readOneRule_node_s = head_candidate_readOneRule_node_s.typeNext; candidate_readOneRule_node_s != head_candidate_readOneRule_node_s; candidate_readOneRule_node_s = candidate_readOneRule_node_s.typeNext)
            {
                // Preset readOneRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_readOneRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_readOneRule_node_bp == null) {
                    MissingPreset_readOneRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_readOneRule_node_s);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_readOneRule_node_bp.type.TypeID!=1) {
                    continue;
                }
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readOneRule_node_bp).@value == 1))) {
                    continue;
                }
                // Extend Outgoing readOneRule_edge_rv from readOneRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readOneRule_edge_rv = candidate_readOneRule_node_s.outhead;
                if(head_candidate_readOneRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readOneRule_edge_rv = head_candidate_readOneRule_edge_rv;
                    do
                    {
                        if(candidate_readOneRule_edge_rv.type.TypeID!=5) {
                            continue;
                        }
                        // Implicit Target readOneRule_node_wv from readOneRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readOneRule_node_wv = candidate_readOneRule_edge_rv.target;
                        if(candidate_readOneRule_node_wv.type.TypeID!=3) {
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
                    while( (candidate_readOneRule_edge_rv = candidate_readOneRule_edge_rv.outNext) != head_candidate_readOneRule_edge_rv );
                }
            }
            return;
        }
        public void MissingPreset_readOneRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_readOneRule_node_s)
        {
            int negLevel = 0;
            // Lookup readOneRule_node_bp 
            int type_id_candidate_readOneRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_readOneRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_readOneRule_node_bp], candidate_readOneRule_node_bp = head_candidate_readOneRule_node_bp.typeNext; candidate_readOneRule_node_bp != head_candidate_readOneRule_node_bp; candidate_readOneRule_node_bp = candidate_readOneRule_node_bp.typeNext)
            {
                // Condition 
                if(!((((GRGEN_MODEL.IBandPosition)candidate_readOneRule_node_bp).@value == 1))) {
                    continue;
                }
                // Extend Outgoing readOneRule_edge_rv from readOneRule_node_s 
                GRGEN_LGSP.LGSPEdge head_candidate_readOneRule_edge_rv = candidate_readOneRule_node_s.outhead;
                if(head_candidate_readOneRule_edge_rv != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_readOneRule_edge_rv = head_candidate_readOneRule_edge_rv;
                    do
                    {
                        if(candidate_readOneRule_edge_rv.type.TypeID!=5) {
                            continue;
                        }
                        // Implicit Target readOneRule_node_wv from readOneRule_edge_rv 
                        GRGEN_LGSP.LGSPNode candidate_readOneRule_node_wv = candidate_readOneRule_edge_rv.target;
                        if(candidate_readOneRule_node_wv.type.TypeID!=3) {
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
                    while( (candidate_readOneRule_edge_rv = candidate_readOneRule_edge_rv.outNext) != head_candidate_readOneRule_edge_rv );
                }
            }
            return;
        }
    }

    public class Action_ensureMoveLeftValidRule : GRGEN_LGSP.LGSPAction
    {
        public Action_ensureMoveLeftValidRule() {
            rulePattern = Rule_ensureMoveLeftValidRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule>(this);
        }

        public override string Name { get { return "ensureMoveLeftValidRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_ensureMoveLeftValidRule instance = new Action_ensureMoveLeftValidRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset ensureMoveLeftValidRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_wv = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_ensureMoveLeftValidRule_node_wv == null) {
                MissingPreset_ensureMoveLeftValidRule_node_wv(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveLeftValidRule_node_wv.type.TypeID!=3) {
                return matches;
            }
            // Preset ensureMoveLeftValidRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_ensureMoveLeftValidRule_node_bp == null) {
                MissingPreset_ensureMoveLeftValidRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_ensureMoveLeftValidRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveLeftValidRule_node_bp.type.TypeID!=1) {
                return matches;
            }
            // NegativePattern 
            {
                ++negLevel;
                uint prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_ensureMoveLeftValidRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming ensureMoveLeftValidRule_neg_0_edge__edge0 from ensureMoveLeftValidRule_node_bp 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_node_bp.inhead;
                if(head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        // Implicit Source ensureMoveLeftValidRule_neg_0_node__node0 from ensureMoveLeftValidRule_neg_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_neg_0_node__node0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.source;
                        if(candidate_ensureMoveLeftValidRule_neg_0_node__node0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ensureMoveLeftValidRule_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // negative pattern found
                        candidate_ensureMoveLeftValidRule_node_bp.flags = candidate_ensureMoveLeftValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                        --negLevel;
                        return matches;
                    }
                    while( (candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.inNext) != head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 );
                }
                candidate_ensureMoveLeftValidRule_node_bp.flags = candidate_ensureMoveLeftValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                --negLevel;
            }
            // Extend Outgoing ensureMoveLeftValidRule_edge__edge0 from ensureMoveLeftValidRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_node_wv.outhead;
            if(head_candidate_ensureMoveLeftValidRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_edge__edge0 = head_candidate_ensureMoveLeftValidRule_edge__edge0;
                do
                {
                    if(candidate_ensureMoveLeftValidRule_edge__edge0.type.TypeID!=6) {
                        continue;
                    }
                    // Implicit Target ensureMoveLeftValidRule_node__node0 from ensureMoveLeftValidRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node__node0 = candidate_ensureMoveLeftValidRule_edge__edge0.target;
                    if(candidate_ensureMoveLeftValidRule_node__node0.type.TypeID!=2) {
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
                while( (candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0.outNext) != head_candidate_ensureMoveLeftValidRule_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_ensureMoveLeftValidRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup ensureMoveLeftValidRule_node_wv 
            int type_id_candidate_ensureMoveLeftValidRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveLeftValidRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_ensureMoveLeftValidRule_node_wv], candidate_ensureMoveLeftValidRule_node_wv = head_candidate_ensureMoveLeftValidRule_node_wv.typeNext; candidate_ensureMoveLeftValidRule_node_wv != head_candidate_ensureMoveLeftValidRule_node_wv; candidate_ensureMoveLeftValidRule_node_wv = candidate_ensureMoveLeftValidRule_node_wv.typeNext)
            {
                // Preset ensureMoveLeftValidRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_ensureMoveLeftValidRule_node_bp == null) {
                    MissingPreset_ensureMoveLeftValidRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_ensureMoveLeftValidRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_ensureMoveLeftValidRule_node_bp.type.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveLeftValidRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Incoming ensureMoveLeftValidRule_neg_0_edge__edge0 from ensureMoveLeftValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_node_bp.inhead;
                    if(head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            // Implicit Source ensureMoveLeftValidRule_neg_0_node__node0 from ensureMoveLeftValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_neg_0_node__node0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.source;
                            if(candidate_ensureMoveLeftValidRule_neg_0_node__node0.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveLeftValidRule_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveLeftValidRule_node_bp.flags = candidate_ensureMoveLeftValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                            --negLevel;
                            goto label0;
                        }
                        while( (candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.inNext) != head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveLeftValidRule_node_bp.flags = candidate_ensureMoveLeftValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveLeftValidRule_edge__edge0 from ensureMoveLeftValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_node_wv.outhead;
                if(head_candidate_ensureMoveLeftValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_edge__edge0 = head_candidate_ensureMoveLeftValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveLeftValidRule_edge__edge0.type.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target ensureMoveLeftValidRule_node__node0 from ensureMoveLeftValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node__node0 = candidate_ensureMoveLeftValidRule_edge__edge0.target;
                        if(candidate_ensureMoveLeftValidRule_node__node0.type.TypeID!=2) {
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
                    while( (candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0.outNext) != head_candidate_ensureMoveLeftValidRule_edge__edge0 );
                }
label0: ;
            }
            return;
        }
        public void MissingPreset_ensureMoveLeftValidRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_wv)
        {
            int negLevel = 0;
            // Lookup ensureMoveLeftValidRule_node_bp 
            int type_id_candidate_ensureMoveLeftValidRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveLeftValidRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_ensureMoveLeftValidRule_node_bp], candidate_ensureMoveLeftValidRule_node_bp = head_candidate_ensureMoveLeftValidRule_node_bp.typeNext; candidate_ensureMoveLeftValidRule_node_bp != head_candidate_ensureMoveLeftValidRule_node_bp; candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.typeNext)
            {
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveLeftValidRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Incoming ensureMoveLeftValidRule_neg_0_edge__edge0 from ensureMoveLeftValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_node_bp.inhead;
                    if(head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            // Implicit Source ensureMoveLeftValidRule_neg_0_node__node0 from ensureMoveLeftValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_neg_0_node__node0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.source;
                            if(candidate_ensureMoveLeftValidRule_neg_0_node__node0.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveLeftValidRule_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveLeftValidRule_node_bp.flags = candidate_ensureMoveLeftValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                            --negLevel;
                            goto label1;
                        }
                        while( (candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.inNext) != head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveLeftValidRule_node_bp.flags = candidate_ensureMoveLeftValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveLeftValidRule_edge__edge0 from ensureMoveLeftValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_node_wv.outhead;
                if(head_candidate_ensureMoveLeftValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveLeftValidRule_edge__edge0 = head_candidate_ensureMoveLeftValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveLeftValidRule_edge__edge0.type.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target ensureMoveLeftValidRule_node__node0 from ensureMoveLeftValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node__node0 = candidate_ensureMoveLeftValidRule_edge__edge0.target;
                        if(candidate_ensureMoveLeftValidRule_node__node0.type.TypeID!=2) {
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
                    while( (candidate_ensureMoveLeftValidRule_edge__edge0 = candidate_ensureMoveLeftValidRule_edge__edge0.outNext) != head_candidate_ensureMoveLeftValidRule_edge__edge0 );
                }
label1: ;
            }
            return;
        }
    }

    public class Action_ensureMoveRightValidRule : GRGEN_LGSP.LGSPAction
    {
        public Action_ensureMoveRightValidRule() {
            rulePattern = Rule_ensureMoveRightValidRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule>(this);
        }

        public override string Name { get { return "ensureMoveRightValidRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_ensureMoveRightValidRule instance = new Action_ensureMoveRightValidRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset ensureMoveRightValidRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_wv = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_ensureMoveRightValidRule_node_wv == null) {
                MissingPreset_ensureMoveRightValidRule_node_wv(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveRightValidRule_node_wv.type.TypeID!=3) {
                return matches;
            }
            // Preset ensureMoveRightValidRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_ensureMoveRightValidRule_node_bp == null) {
                MissingPreset_ensureMoveRightValidRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_ensureMoveRightValidRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_ensureMoveRightValidRule_node_bp.type.TypeID!=1) {
                return matches;
            }
            // NegativePattern 
            {
                ++negLevel;
                uint prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                prev_neg_0__candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_ensureMoveRightValidRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing ensureMoveRightValidRule_neg_0_edge__edge0 from ensureMoveRightValidRule_node_bp 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_node_bp.outhead;
                if(head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveRightValidRule_neg_0_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        // Implicit Target ensureMoveRightValidRule_neg_0_node__node0 from ensureMoveRightValidRule_neg_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_neg_0_node__node0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.target;
                        if(candidate_ensureMoveRightValidRule_neg_0_node__node0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_ensureMoveRightValidRule_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // negative pattern found
                        candidate_ensureMoveRightValidRule_node_bp.flags = candidate_ensureMoveRightValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                        --negLevel;
                        return matches;
                    }
                    while( (candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.outNext) != head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 );
                }
                candidate_ensureMoveRightValidRule_node_bp.flags = candidate_ensureMoveRightValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                --negLevel;
            }
            // Extend Outgoing ensureMoveRightValidRule_edge__edge0 from ensureMoveRightValidRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_node_wv.outhead;
            if(head_candidate_ensureMoveRightValidRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_edge__edge0 = head_candidate_ensureMoveRightValidRule_edge__edge0;
                do
                {
                    if(candidate_ensureMoveRightValidRule_edge__edge0.type.TypeID!=7) {
                        continue;
                    }
                    // Implicit Target ensureMoveRightValidRule_node__node0 from ensureMoveRightValidRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node__node0 = candidate_ensureMoveRightValidRule_edge__edge0.target;
                    if(candidate_ensureMoveRightValidRule_node__node0.type.TypeID!=2) {
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
                while( (candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0.outNext) != head_candidate_ensureMoveRightValidRule_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_ensureMoveRightValidRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup ensureMoveRightValidRule_node_wv 
            int type_id_candidate_ensureMoveRightValidRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveRightValidRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_ensureMoveRightValidRule_node_wv], candidate_ensureMoveRightValidRule_node_wv = head_candidate_ensureMoveRightValidRule_node_wv.typeNext; candidate_ensureMoveRightValidRule_node_wv != head_candidate_ensureMoveRightValidRule_node_wv; candidate_ensureMoveRightValidRule_node_wv = candidate_ensureMoveRightValidRule_node_wv.typeNext)
            {
                // Preset ensureMoveRightValidRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_ensureMoveRightValidRule_node_bp == null) {
                    MissingPreset_ensureMoveRightValidRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_ensureMoveRightValidRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_ensureMoveRightValidRule_node_bp.type.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveRightValidRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Outgoing ensureMoveRightValidRule_neg_0_edge__edge0 from ensureMoveRightValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_node_bp.outhead;
                    if(head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveRightValidRule_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            // Implicit Target ensureMoveRightValidRule_neg_0_node__node0 from ensureMoveRightValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_neg_0_node__node0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.target;
                            if(candidate_ensureMoveRightValidRule_neg_0_node__node0.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveRightValidRule_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveRightValidRule_node_bp.flags = candidate_ensureMoveRightValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                            --negLevel;
                            goto label2;
                        }
                        while( (candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.outNext) != head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveRightValidRule_node_bp.flags = candidate_ensureMoveRightValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveRightValidRule_edge__edge0 from ensureMoveRightValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_node_wv.outhead;
                if(head_candidate_ensureMoveRightValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_edge__edge0 = head_candidate_ensureMoveRightValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveRightValidRule_edge__edge0.type.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target ensureMoveRightValidRule_node__node0 from ensureMoveRightValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node__node0 = candidate_ensureMoveRightValidRule_edge__edge0.target;
                        if(candidate_ensureMoveRightValidRule_node__node0.type.TypeID!=2) {
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
                    while( (candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0.outNext) != head_candidate_ensureMoveRightValidRule_edge__edge0 );
                }
label2: ;
            }
            return;
        }
        public void MissingPreset_ensureMoveRightValidRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_wv)
        {
            int negLevel = 0;
            // Lookup ensureMoveRightValidRule_node_bp 
            int type_id_candidate_ensureMoveRightValidRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_ensureMoveRightValidRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_ensureMoveRightValidRule_node_bp], candidate_ensureMoveRightValidRule_node_bp = head_candidate_ensureMoveRightValidRule_node_bp.typeNext; candidate_ensureMoveRightValidRule_node_bp != head_candidate_ensureMoveRightValidRule_node_bp; candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.typeNext)
            {
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    prev_neg_0__candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_ensureMoveRightValidRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Outgoing ensureMoveRightValidRule_neg_0_edge__edge0 from ensureMoveRightValidRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_node_bp.outhead;
                    if(head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ensureMoveRightValidRule_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            // Implicit Target ensureMoveRightValidRule_neg_0_node__node0 from ensureMoveRightValidRule_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_neg_0_node__node0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.target;
                            if(candidate_ensureMoveRightValidRule_neg_0_node__node0.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_ensureMoveRightValidRule_neg_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // negative pattern found
                            candidate_ensureMoveRightValidRule_node_bp.flags = candidate_ensureMoveRightValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.outNext) != head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 );
                    }
                    candidate_ensureMoveRightValidRule_node_bp.flags = candidate_ensureMoveRightValidRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                    --negLevel;
                }
                // Extend Outgoing ensureMoveRightValidRule_edge__edge0 from ensureMoveRightValidRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_node_wv.outhead;
                if(head_candidate_ensureMoveRightValidRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ensureMoveRightValidRule_edge__edge0 = head_candidate_ensureMoveRightValidRule_edge__edge0;
                    do
                    {
                        if(candidate_ensureMoveRightValidRule_edge__edge0.type.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target ensureMoveRightValidRule_node__node0 from ensureMoveRightValidRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node__node0 = candidate_ensureMoveRightValidRule_edge__edge0.target;
                        if(candidate_ensureMoveRightValidRule_node__node0.type.TypeID!=2) {
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
                    while( (candidate_ensureMoveRightValidRule_edge__edge0 = candidate_ensureMoveRightValidRule_edge__edge0.outNext) != head_candidate_ensureMoveRightValidRule_edge__edge0 );
                }
label3: ;
            }
            return;
        }
    }

    public class Action_moveLeftRule : GRGEN_LGSP.LGSPAction
    {
        public Action_moveLeftRule() {
            rulePattern = Rule_moveLeftRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_moveLeftRule.Match_moveLeftRule>(this);
        }

        public override string Name { get { return "moveLeftRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_moveLeftRule.Match_moveLeftRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_moveLeftRule instance = new Action_moveLeftRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset moveLeftRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_wv = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_moveLeftRule_node_wv == null) {
                MissingPreset_moveLeftRule_node_wv(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveLeftRule_node_wv.type.TypeID!=3) {
                return matches;
            }
            // Preset moveLeftRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_moveLeftRule_node_bp == null) {
                MissingPreset_moveLeftRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_moveLeftRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveLeftRule_node_bp.type.TypeID!=1) {
                return matches;
            }
            uint prev__candidate_moveLeftRule_node_bp;
            prev__candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_moveLeftRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Extend Outgoing moveLeftRule_edge__edge0 from moveLeftRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_node_wv.outhead;
            if(head_candidate_moveLeftRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge0 = head_candidate_moveLeftRule_edge__edge0;
                do
                {
                    if(candidate_moveLeftRule_edge__edge0.type.TypeID!=6) {
                        continue;
                    }
                    // Implicit Target moveLeftRule_node_s from moveLeftRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_s = candidate_moveLeftRule_edge__edge0.target;
                    if(candidate_moveLeftRule_node_s.type.TypeID!=2) {
                        continue;
                    }
                    // Extend Incoming moveLeftRule_edge__edge1 from moveLeftRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_node_bp.inhead;
                    if(head_candidate_moveLeftRule_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge1 = head_candidate_moveLeftRule_edge__edge1;
                        do
                        {
                            if(candidate_moveLeftRule_edge__edge1.type.TypeID!=3) {
                                continue;
                            }
                            // Implicit Source moveLeftRule_node_lbp from moveLeftRule_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_lbp = candidate_moveLeftRule_edge__edge1.source;
                            if(candidate_moveLeftRule_node_lbp.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_moveLeftRule_node_lbp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                candidate_moveLeftRule_node_bp.flags = candidate_moveLeftRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
                                return matches;
                            }
                        }
                        while( (candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_edge__edge1.inNext) != head_candidate_moveLeftRule_edge__edge1 );
                    }
                }
                while( (candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_edge__edge0.outNext) != head_candidate_moveLeftRule_edge__edge0 );
            }
            candidate_moveLeftRule_node_bp.flags = candidate_moveLeftRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
            return matches;
        }
        public void MissingPreset_moveLeftRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup moveLeftRule_node_wv 
            int type_id_candidate_moveLeftRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveLeftRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_moveLeftRule_node_wv], candidate_moveLeftRule_node_wv = head_candidate_moveLeftRule_node_wv.typeNext; candidate_moveLeftRule_node_wv != head_candidate_moveLeftRule_node_wv; candidate_moveLeftRule_node_wv = candidate_moveLeftRule_node_wv.typeNext)
            {
                // Preset moveLeftRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_moveLeftRule_node_bp == null) {
                    MissingPreset_moveLeftRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_moveLeftRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_moveLeftRule_node_bp.type.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_moveLeftRule_node_bp;
                prev__candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveLeftRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveLeftRule_edge__edge0 from moveLeftRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_node_wv.outhead;
                if(head_candidate_moveLeftRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge0 = head_candidate_moveLeftRule_edge__edge0;
                    do
                    {
                        if(candidate_moveLeftRule_edge__edge0.type.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target moveLeftRule_node_s from moveLeftRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_s = candidate_moveLeftRule_edge__edge0.target;
                        if(candidate_moveLeftRule_node_s.type.TypeID!=2) {
                            continue;
                        }
                        // Extend Incoming moveLeftRule_edge__edge1 from moveLeftRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_node_bp.inhead;
                        if(head_candidate_moveLeftRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge1 = head_candidate_moveLeftRule_edge__edge1;
                            do
                            {
                                if(candidate_moveLeftRule_edge__edge1.type.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Source moveLeftRule_node_lbp from moveLeftRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_lbp = candidate_moveLeftRule_edge__edge1.source;
                                if(candidate_moveLeftRule_node_lbp.type.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveLeftRule_node_lbp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                    candidate_moveLeftRule_node_bp.flags = candidate_moveLeftRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_edge__edge1.inNext) != head_candidate_moveLeftRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_edge__edge0.outNext) != head_candidate_moveLeftRule_edge__edge0 );
                }
                candidate_moveLeftRule_node_bp.flags = candidate_moveLeftRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
            }
            return;
        }
        public void MissingPreset_moveLeftRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_wv)
        {
            int negLevel = 0;
            // Lookup moveLeftRule_node_bp 
            int type_id_candidate_moveLeftRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveLeftRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_moveLeftRule_node_bp], candidate_moveLeftRule_node_bp = head_candidate_moveLeftRule_node_bp.typeNext; candidate_moveLeftRule_node_bp != head_candidate_moveLeftRule_node_bp; candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.typeNext)
            {
                uint prev__candidate_moveLeftRule_node_bp;
                prev__candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveLeftRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveLeftRule_edge__edge0 from moveLeftRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_node_wv.outhead;
                if(head_candidate_moveLeftRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge0 = head_candidate_moveLeftRule_edge__edge0;
                    do
                    {
                        if(candidate_moveLeftRule_edge__edge0.type.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target moveLeftRule_node_s from moveLeftRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_s = candidate_moveLeftRule_edge__edge0.target;
                        if(candidate_moveLeftRule_node_s.type.TypeID!=2) {
                            continue;
                        }
                        // Extend Incoming moveLeftRule_edge__edge1 from moveLeftRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_node_bp.inhead;
                        if(head_candidate_moveLeftRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveLeftRule_edge__edge1 = head_candidate_moveLeftRule_edge__edge1;
                            do
                            {
                                if(candidate_moveLeftRule_edge__edge1.type.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Source moveLeftRule_node_lbp from moveLeftRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_lbp = candidate_moveLeftRule_edge__edge1.source;
                                if(candidate_moveLeftRule_node_lbp.type.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveLeftRule_node_lbp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                    candidate_moveLeftRule_node_bp.flags = candidate_moveLeftRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_edge__edge1.inNext) != head_candidate_moveLeftRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_edge__edge0.outNext) != head_candidate_moveLeftRule_edge__edge0 );
                }
                candidate_moveLeftRule_node_bp.flags = candidate_moveLeftRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveLeftRule_node_bp;
            }
            return;
        }
    }

    public class Action_moveRightRule : GRGEN_LGSP.LGSPAction
    {
        public Action_moveRightRule() {
            rulePattern = Rule_moveRightRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_moveRightRule.Match_moveRightRule>(this);
        }

        public override string Name { get { return "moveRightRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_moveRightRule.Match_moveRightRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_moveRightRule instance = new Action_moveRightRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Preset moveRightRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_wv = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_moveRightRule_node_wv == null) {
                MissingPreset_moveRightRule_node_wv(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveRightRule_node_wv.type.TypeID!=3) {
                return matches;
            }
            // Preset moveRightRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_moveRightRule_node_bp == null) {
                MissingPreset_moveRightRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_moveRightRule_node_wv);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_moveRightRule_node_bp.type.TypeID!=1) {
                return matches;
            }
            uint prev__candidate_moveRightRule_node_bp;
            prev__candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_moveRightRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Extend Outgoing moveRightRule_edge__edge0 from moveRightRule_node_wv 
            GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_node_wv.outhead;
            if(head_candidate_moveRightRule_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge0 = head_candidate_moveRightRule_edge__edge0;
                do
                {
                    if(candidate_moveRightRule_edge__edge0.type.TypeID!=7) {
                        continue;
                    }
                    // Implicit Target moveRightRule_node_s from moveRightRule_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_s = candidate_moveRightRule_edge__edge0.target;
                    if(candidate_moveRightRule_node_s.type.TypeID!=2) {
                        continue;
                    }
                    // Extend Outgoing moveRightRule_edge__edge1 from moveRightRule_node_bp 
                    GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_node_bp.outhead;
                    if(head_candidate_moveRightRule_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge1 = head_candidate_moveRightRule_edge__edge1;
                        do
                        {
                            if(candidate_moveRightRule_edge__edge1.type.TypeID!=3) {
                                continue;
                            }
                            // Implicit Target moveRightRule_node_rbp from moveRightRule_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_rbp = candidate_moveRightRule_edge__edge1.target;
                            if(candidate_moveRightRule_node_rbp.type.TypeID!=1) {
                                continue;
                            }
                            if((candidate_moveRightRule_node_rbp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                candidate_moveRightRule_node_bp.flags = candidate_moveRightRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
                                return matches;
                            }
                        }
                        while( (candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_edge__edge1.outNext) != head_candidate_moveRightRule_edge__edge1 );
                    }
                }
                while( (candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_edge__edge0.outNext) != head_candidate_moveRightRule_edge__edge0 );
            }
            candidate_moveRightRule_node_bp.flags = candidate_moveRightRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
            return matches;
        }
        public void MissingPreset_moveRightRule_node_wv(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup moveRightRule_node_wv 
            int type_id_candidate_moveRightRule_node_wv = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveRightRule_node_wv = graph.nodesByTypeHeads[type_id_candidate_moveRightRule_node_wv], candidate_moveRightRule_node_wv = head_candidate_moveRightRule_node_wv.typeNext; candidate_moveRightRule_node_wv != head_candidate_moveRightRule_node_wv; candidate_moveRightRule_node_wv = candidate_moveRightRule_node_wv.typeNext)
            {
                // Preset moveRightRule_node_bp 
                GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_bp = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_moveRightRule_node_bp == null) {
                    MissingPreset_moveRightRule_node_bp(graph, maxMatches, parameters, null, null, null, candidate_moveRightRule_node_wv);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(candidate_moveRightRule_node_bp.type.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_moveRightRule_node_bp;
                prev__candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveRightRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveRightRule_edge__edge0 from moveRightRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_node_wv.outhead;
                if(head_candidate_moveRightRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge0 = head_candidate_moveRightRule_edge__edge0;
                    do
                    {
                        if(candidate_moveRightRule_edge__edge0.type.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target moveRightRule_node_s from moveRightRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_s = candidate_moveRightRule_edge__edge0.target;
                        if(candidate_moveRightRule_node_s.type.TypeID!=2) {
                            continue;
                        }
                        // Extend Outgoing moveRightRule_edge__edge1 from moveRightRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_node_bp.outhead;
                        if(head_candidate_moveRightRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge1 = head_candidate_moveRightRule_edge__edge1;
                            do
                            {
                                if(candidate_moveRightRule_edge__edge1.type.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Target moveRightRule_node_rbp from moveRightRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_rbp = candidate_moveRightRule_edge__edge1.target;
                                if(candidate_moveRightRule_node_rbp.type.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveRightRule_node_rbp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                    candidate_moveRightRule_node_bp.flags = candidate_moveRightRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_edge__edge1.outNext) != head_candidate_moveRightRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_edge__edge0.outNext) != head_candidate_moveRightRule_edge__edge0 );
                }
                candidate_moveRightRule_node_bp.flags = candidate_moveRightRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
            }
            return;
        }
        public void MissingPreset_moveRightRule_node_bp(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_wv)
        {
            int negLevel = 0;
            // Lookup moveRightRule_node_bp 
            int type_id_candidate_moveRightRule_node_bp = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_moveRightRule_node_bp = graph.nodesByTypeHeads[type_id_candidate_moveRightRule_node_bp], candidate_moveRightRule_node_bp = head_candidate_moveRightRule_node_bp.typeNext; candidate_moveRightRule_node_bp != head_candidate_moveRightRule_node_bp; candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.typeNext)
            {
                uint prev__candidate_moveRightRule_node_bp;
                prev__candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_moveRightRule_node_bp.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing moveRightRule_edge__edge0 from moveRightRule_node_wv 
                GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_node_wv.outhead;
                if(head_candidate_moveRightRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge0 = head_candidate_moveRightRule_edge__edge0;
                    do
                    {
                        if(candidate_moveRightRule_edge__edge0.type.TypeID!=7) {
                            continue;
                        }
                        // Implicit Target moveRightRule_node_s from moveRightRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_s = candidate_moveRightRule_edge__edge0.target;
                        if(candidate_moveRightRule_node_s.type.TypeID!=2) {
                            continue;
                        }
                        // Extend Outgoing moveRightRule_edge__edge1 from moveRightRule_node_bp 
                        GRGEN_LGSP.LGSPEdge head_candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_node_bp.outhead;
                        if(head_candidate_moveRightRule_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_moveRightRule_edge__edge1 = head_candidate_moveRightRule_edge__edge1;
                            do
                            {
                                if(candidate_moveRightRule_edge__edge1.type.TypeID!=3) {
                                    continue;
                                }
                                // Implicit Target moveRightRule_node_rbp from moveRightRule_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_rbp = candidate_moveRightRule_edge__edge1.target;
                                if(candidate_moveRightRule_node_rbp.type.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_moveRightRule_node_rbp.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                    candidate_moveRightRule_node_bp.flags = candidate_moveRightRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
                                    return;
                                }
                            }
                            while( (candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_edge__edge1.outNext) != head_candidate_moveRightRule_edge__edge1 );
                        }
                    }
                    while( (candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_edge__edge0.outNext) != head_candidate_moveRightRule_edge__edge0 );
                }
                candidate_moveRightRule_node_bp.flags = candidate_moveRightRule_node_bp.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_moveRightRule_node_bp;
            }
            return;
        }
    }


    // class which instantiates and stores all the compiled actions of the module in a dictionary,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class Turing3Actions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public Turing3Actions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public Turing3Actions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer analyzer = new de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Rule_readZeroRule.Instance);
            actions.Add("readZeroRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_readZeroRule.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_readOneRule.Instance);
            actions.Add("readOneRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_readOneRule.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_ensureMoveLeftValidRule.Instance);
            actions.Add("ensureMoveLeftValidRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_ensureMoveLeftValidRule.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_ensureMoveRightValidRule.Instance);
            actions.Add("ensureMoveRightValidRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_ensureMoveRightValidRule.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_moveLeftRule.Instance);
            actions.Add("moveLeftRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_moveLeftRule.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_moveRightRule.Instance);
            actions.Add("moveRightRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_moveRightRule.Instance);
            analyzer.ComputeInterPatternRelations();
        }

        public override string Name { get { return "Turing3Actions"; } }
        public override string ModelMD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
    }
}