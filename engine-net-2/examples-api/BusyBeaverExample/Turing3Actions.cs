// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Turing3\Turing3.grg" on Mon Nov 18 19:45:14 CET 2024

//#pragma warning disable CS0219, CS0162
#pragma warning disable 219, 162

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Turing3;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_Turing3;

namespace de.unika.ipd.grGen.Action_Turing3
{
	public class Rule_readZeroRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_readZeroRule instance = null;
		public static Rule_readZeroRule Instance { get { if(instance==null) { instance = new Rule_readZeroRule(); instance.initialize(); } return instance; } }

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






		public GRGEN_LGSP.PatternGraph pat_readZeroRule;


		private Rule_readZeroRule()
			: base("readZeroRule",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new string[] { "readZeroRule_node_s", "readZeroRule_node_bp", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_readZeroRule+IMatch_readZeroRule",
				"de.unika.ipd.grGen.Action_Turing3.Rule_readZeroRule+Match_readZeroRule"
			)
		{
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
			bool[] readZeroRule_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] readZeroRule_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode readZeroRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, GRGEN_MODEL.NodeType_State.typeVar, "GRGEN_MODEL.IState", "readZeroRule_node_s", "s", readZeroRule_node_s_AllowedTypes, readZeroRule_node_s_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode readZeroRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, GRGEN_MODEL.NodeType_WriteValue.typeVar, "GRGEN_MODEL.IWriteValue", "readZeroRule_node_wv", "wv", readZeroRule_node_wv_AllowedTypes, readZeroRule_node_wv_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode readZeroRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "readZeroRule_node_bp", "bp", readZeroRule_node_bp_AllowedTypes, readZeroRule_node_bp_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge readZeroRule_edge_rv = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@readZero, GRGEN_MODEL.EdgeType_readZero.typeVar, "GRGEN_MODEL.IreadZero", "readZeroRule_edge_rv", "rv", readZeroRule_edge_rv_AllowedTypes, readZeroRule_edge_rv_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternCondition readZeroRule_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", true, "readZeroRule_node_bp", "value"), new GRGEN_EXPR.Constant("0")),
				new string[] { "readZeroRule_node_bp" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { readZeroRule_node_bp }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_readZeroRule = new GRGEN_LGSP.PatternGraph(
				"readZeroRule",
				"",
				null, "readZeroRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { readZeroRule_node_s, readZeroRule_node_wv, readZeroRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { readZeroRule_edge_rv }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { readZeroRule_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				readZeroRule_isNodeHomomorphicGlobal,
				readZeroRule_isEdgeHomomorphicGlobal,
				readZeroRule_isNodeTotallyHomomorphic,
				readZeroRule_isEdgeTotallyHomomorphic
			);
			pat_readZeroRule.edgeToSourceNode.Add(readZeroRule_edge_rv, readZeroRule_node_s);
			pat_readZeroRule.edgeToTargetNode.Add(readZeroRule_edge_rv, readZeroRule_node_wv);

			readZeroRule_node_s.pointOfDefinition = null;
			readZeroRule_node_wv.pointOfDefinition = pat_readZeroRule;
			readZeroRule_node_bp.pointOfDefinition = null;
			readZeroRule_edge_rv.pointOfDefinition = pat_readZeroRule;

			patternGraph = pat_readZeroRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IWriteValue output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_readZeroRule curMatch = (Match_readZeroRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_wv = curMatch._node_wv;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			GRGEN_MODEL.IBandPosition inode_bp = curMatch.node_bp;
			GRGEN_MODEL.IWriteValue inode_wv = curMatch.node_wv;
			graph.SettingAddedNodeNames( readZeroRule_addedNodeNames );
			graph.SettingAddedEdgeNames( readZeroRule_addedEdgeNames );
			{ // eval_0
				int tempvar_0 = (int )inode_wv.@value;
				graph.ChangingNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_bp.@value = tempvar_0;
				graph.ChangedNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value);
			}
			actionEnv.SelectedMatchRewritten();
			output_0 = (GRGEN_MODEL.IWriteValue)(node_wv);
			return;
		}
		private static string[] readZeroRule_addedNodeNames = new string[] {  };
		private static string[] readZeroRule_addedEdgeNames = new string[] {  };

		static Rule_readZeroRule() {
		}

		public interface IMatch_readZeroRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IState node_s { get; set; }
			GRGEN_MODEL.IWriteValue node_wv { get; set; }
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			GRGEN_MODEL.IreadZero edge_rv { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_readZeroRule : GRGEN_LGSP.MatchListElement<Match_readZeroRule>, IMatch_readZeroRule
		{
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } set { _node_s = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } set { _node_wv = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum readZeroRule_NodeNums { @s, @wv, @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)readZeroRule_NodeNums.@s: return _node_s;
				case (int)readZeroRule_NodeNums.@wv: return _node_wv;
				case (int)readZeroRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "s": return _node_s;
				case "wv": return _node_wv;
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "s": _node_s = (GRGEN_LGSP.LGSPNode)value; break;
				case "wv": _node_wv = (GRGEN_LGSP.LGSPNode)value; break;
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.IreadZero edge_rv { get { return (GRGEN_MODEL.IreadZero)_edge_rv; } set { _edge_rv = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_rv;
			public enum readZeroRule_EdgeNums { @rv, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)readZeroRule_EdgeNums.@rv: return _edge_rv;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "rv": return _edge_rv;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "rv": _edge_rv = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum readZeroRule_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum readZeroRule_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum readZeroRule_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum readZeroRule_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum readZeroRule_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_readZeroRule.instance.pat_readZeroRule; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_readZeroRule(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_readZeroRule(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_readZeroRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_readZeroRule cur = this;
				while(cur != null) {
					Match_readZeroRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_readZeroRule that)
			{
				_node_s = that._node_s;
				_node_wv = that._node_wv;
				_node_bp = that._node_bp;
				_edge_rv = that._edge_rv;
			}

			public Match_readZeroRule(Match_readZeroRule that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_readZeroRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_s = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_s];
				_node_wv = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_wv];
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_edge_rv = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge_rv];
			}

			public Match_readZeroRule(Match_readZeroRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_readZeroRule()
			{
			}

			public bool IsEqual(Match_readZeroRule that)
			{
				if(that==null) return false;
				if(_node_s != that._node_s) return false;
				if(_node_wv != that._node_wv) return false;
				if(_node_bp != that._node_bp) return false;
				if(_edge_rv != that._edge_rv) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IState> Extract_s(List<IMatch_readZeroRule> matchList)
			{
				List<GRGEN_MODEL.IState> resultList = new List<GRGEN_MODEL.IState>(matchList.Count);
				foreach(IMatch_readZeroRule match in matchList)
					resultList.Add(match.node_s);
				return resultList;
			}
			public static List<GRGEN_MODEL.IWriteValue> Extract_wv(List<IMatch_readZeroRule> matchList)
			{
				List<GRGEN_MODEL.IWriteValue> resultList = new List<GRGEN_MODEL.IWriteValue>(matchList.Count);
				foreach(IMatch_readZeroRule match in matchList)
					resultList.Add(match.node_wv);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_readZeroRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_readZeroRule match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
			public static List<GRGEN_MODEL.IreadZero> Extract_rv(List<IMatch_readZeroRule> matchList)
			{
				List<GRGEN_MODEL.IreadZero> resultList = new List<GRGEN_MODEL.IreadZero>(matchList.Count);
				foreach(IMatch_readZeroRule match in matchList)
					resultList.Add(match.edge_rv);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>)
				return ((List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule instanceBearingAttributeForSearch_readZeroRule = new GRGEN_ACTIONS.Rule_readZeroRule.Match_readZeroRule();
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_groupBy_s(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>> seenValues = new Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_s)) {
					seenValues[list[pos].@node_s].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> tempList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_s, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_keepOneForEachBy_s(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_s)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_s, null);
				}
			}
			return newList;
		}
		public static int Array_readZeroRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_groupBy_wv(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>> seenValues = new Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_wv)) {
					seenValues[list[pos].@node_wv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> tempList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_wv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_keepOneForEachBy_wv(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_wv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_wv, null);
				}
			}
			return newList;
		}
		public static int Array_readZeroRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_groupBy_bp(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> tempList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_readZeroRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_groupBy_rv(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			Dictionary<GRGEN_MODEL.IreadZero, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>> seenValues = new Dictionary<GRGEN_MODEL.IreadZero, List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge_rv)) {
					seenValues[list[pos].@edge_rv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> tempList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge_rv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> Array_readZeroRule_keepOneForEachBy_rv(List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> newList = new List<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule>();
			Dictionary<GRGEN_MODEL.IreadZero, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IreadZero, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge_rv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge_rv, null);
				}
			}
			return newList;
		}
		public static int Array_readZeroRule_indexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IreadZero entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_indexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IreadZero entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IreadZero entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readZeroRule_lastIndexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readZeroRule.IMatch_readZeroRule> list, GRGEN_MODEL.IreadZero entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
	}

	public class Rule_readOneRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_readOneRule instance = null;
		public static Rule_readOneRule Instance { get { if(instance==null) { instance = new Rule_readOneRule(); instance.initialize(); } return instance; } }

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






		public GRGEN_LGSP.PatternGraph pat_readOneRule;


		private Rule_readOneRule()
			: base("readOneRule",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new string[] { "readOneRule_node_s", "readOneRule_node_bp", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_readOneRule+IMatch_readOneRule",
				"de.unika.ipd.grGen.Action_Turing3.Rule_readOneRule+Match_readOneRule"
			)
		{
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
			bool[] readOneRule_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] readOneRule_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode readOneRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, GRGEN_MODEL.NodeType_State.typeVar, "GRGEN_MODEL.IState", "readOneRule_node_s", "s", readOneRule_node_s_AllowedTypes, readOneRule_node_s_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode readOneRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, GRGEN_MODEL.NodeType_WriteValue.typeVar, "GRGEN_MODEL.IWriteValue", "readOneRule_node_wv", "wv", readOneRule_node_wv_AllowedTypes, readOneRule_node_wv_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode readOneRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "readOneRule_node_bp", "bp", readOneRule_node_bp_AllowedTypes, readOneRule_node_bp_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge readOneRule_edge_rv = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@readOne, GRGEN_MODEL.EdgeType_readOne.typeVar, "GRGEN_MODEL.IreadOne", "readOneRule_edge_rv", "rv", readOneRule_edge_rv_AllowedTypes, readOneRule_edge_rv_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternCondition readOneRule_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", true, "readOneRule_node_bp", "value"), new GRGEN_EXPR.Constant("1")),
				new string[] { "readOneRule_node_bp" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { readOneRule_node_bp }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_readOneRule = new GRGEN_LGSP.PatternGraph(
				"readOneRule",
				"",
				null, "readOneRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { readOneRule_node_s, readOneRule_node_wv, readOneRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { readOneRule_edge_rv }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { readOneRule_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				readOneRule_isNodeHomomorphicGlobal,
				readOneRule_isEdgeHomomorphicGlobal,
				readOneRule_isNodeTotallyHomomorphic,
				readOneRule_isEdgeTotallyHomomorphic
			);
			pat_readOneRule.edgeToSourceNode.Add(readOneRule_edge_rv, readOneRule_node_s);
			pat_readOneRule.edgeToTargetNode.Add(readOneRule_edge_rv, readOneRule_node_wv);

			readOneRule_node_s.pointOfDefinition = null;
			readOneRule_node_wv.pointOfDefinition = pat_readOneRule;
			readOneRule_node_bp.pointOfDefinition = null;
			readOneRule_edge_rv.pointOfDefinition = pat_readOneRule;

			patternGraph = pat_readOneRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IWriteValue output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_readOneRule curMatch = (Match_readOneRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_wv = curMatch._node_wv;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			GRGEN_MODEL.IBandPosition inode_bp = curMatch.node_bp;
			GRGEN_MODEL.IWriteValue inode_wv = curMatch.node_wv;
			graph.SettingAddedNodeNames( readOneRule_addedNodeNames );
			graph.SettingAddedEdgeNames( readOneRule_addedEdgeNames );
			{ // eval_0
				int tempvar_0 = (int )inode_wv.@value;
				graph.ChangingNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_bp.@value = tempvar_0;
				graph.ChangedNodeAttribute(node_bp, GRGEN_MODEL.NodeType_BandPosition.AttributeType_value);
			}
			actionEnv.SelectedMatchRewritten();
			output_0 = (GRGEN_MODEL.IWriteValue)(node_wv);
			return;
		}
		private static string[] readOneRule_addedNodeNames = new string[] {  };
		private static string[] readOneRule_addedEdgeNames = new string[] {  };

		static Rule_readOneRule() {
		}

		public interface IMatch_readOneRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IState node_s { get; set; }
			GRGEN_MODEL.IWriteValue node_wv { get; set; }
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			GRGEN_MODEL.IreadOne edge_rv { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_readOneRule : GRGEN_LGSP.MatchListElement<Match_readOneRule>, IMatch_readOneRule
		{
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } set { _node_s = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } set { _node_wv = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum readOneRule_NodeNums { @s, @wv, @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)readOneRule_NodeNums.@s: return _node_s;
				case (int)readOneRule_NodeNums.@wv: return _node_wv;
				case (int)readOneRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "s": return _node_s;
				case "wv": return _node_wv;
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "s": _node_s = (GRGEN_LGSP.LGSPNode)value; break;
				case "wv": _node_wv = (GRGEN_LGSP.LGSPNode)value; break;
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.IreadOne edge_rv { get { return (GRGEN_MODEL.IreadOne)_edge_rv; } set { _edge_rv = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_rv;
			public enum readOneRule_EdgeNums { @rv, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)readOneRule_EdgeNums.@rv: return _edge_rv;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "rv": return _edge_rv;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "rv": _edge_rv = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum readOneRule_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum readOneRule_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum readOneRule_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum readOneRule_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum readOneRule_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_readOneRule.instance.pat_readOneRule; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_readOneRule(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_readOneRule(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_readOneRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_readOneRule cur = this;
				while(cur != null) {
					Match_readOneRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_readOneRule that)
			{
				_node_s = that._node_s;
				_node_wv = that._node_wv;
				_node_bp = that._node_bp;
				_edge_rv = that._edge_rv;
			}

			public Match_readOneRule(Match_readOneRule that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_readOneRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_s = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_s];
				_node_wv = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_wv];
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_edge_rv = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge_rv];
			}

			public Match_readOneRule(Match_readOneRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_readOneRule()
			{
			}

			public bool IsEqual(Match_readOneRule that)
			{
				if(that==null) return false;
				if(_node_s != that._node_s) return false;
				if(_node_wv != that._node_wv) return false;
				if(_node_bp != that._node_bp) return false;
				if(_edge_rv != that._edge_rv) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IState> Extract_s(List<IMatch_readOneRule> matchList)
			{
				List<GRGEN_MODEL.IState> resultList = new List<GRGEN_MODEL.IState>(matchList.Count);
				foreach(IMatch_readOneRule match in matchList)
					resultList.Add(match.node_s);
				return resultList;
			}
			public static List<GRGEN_MODEL.IWriteValue> Extract_wv(List<IMatch_readOneRule> matchList)
			{
				List<GRGEN_MODEL.IWriteValue> resultList = new List<GRGEN_MODEL.IWriteValue>(matchList.Count);
				foreach(IMatch_readOneRule match in matchList)
					resultList.Add(match.node_wv);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_readOneRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_readOneRule match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
			public static List<GRGEN_MODEL.IreadOne> Extract_rv(List<IMatch_readOneRule> matchList)
			{
				List<GRGEN_MODEL.IreadOne> resultList = new List<GRGEN_MODEL.IreadOne>(matchList.Count);
				foreach(IMatch_readOneRule match in matchList)
					resultList.Add(match.edge_rv);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>)
				return ((List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule instanceBearingAttributeForSearch_readOneRule = new GRGEN_ACTIONS.Rule_readOneRule.Match_readOneRule();
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_groupBy_s(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>> seenValues = new Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_s)) {
					seenValues[list[pos].@node_s].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> tempList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_s, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_keepOneForEachBy_s(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_s)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_s, null);
				}
			}
			return newList;
		}
		public static int Array_readOneRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_groupBy_wv(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>> seenValues = new Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_wv)) {
					seenValues[list[pos].@node_wv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> tempList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_wv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_keepOneForEachBy_wv(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_wv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_wv, null);
				}
			}
			return newList;
		}
		public static int Array_readOneRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_groupBy_bp(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> tempList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_readOneRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_groupBy_rv(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			Dictionary<GRGEN_MODEL.IreadOne, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>> seenValues = new Dictionary<GRGEN_MODEL.IreadOne, List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge_rv)) {
					seenValues[list[pos].@edge_rv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> tempList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge_rv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			foreach(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> Array_readOneRule_keepOneForEachBy_rv(List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list)
		{
			List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> newList = new List<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule>();
			Dictionary<GRGEN_MODEL.IreadOne, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IreadOne, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge_rv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge_rv, null);
				}
			}
			return newList;
		}
		public static int Array_readOneRule_indexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IreadOne entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_indexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IreadOne entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IreadOne entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_readOneRule_lastIndexOfBy_rv(IList<GRGEN_ACTIONS.Rule_readOneRule.IMatch_readOneRule> list, GRGEN_MODEL.IreadOne entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge_rv.Equals(entry))
					return i;
			return -1;
		}
	}

	public class Rule_ensureMoveLeftValidRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ensureMoveLeftValidRule instance = null;
		public static Rule_ensureMoveLeftValidRule Instance { get { if(instance==null) { instance = new Rule_ensureMoveLeftValidRule(); instance.initialize(); } return instance; } }

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





		public GRGEN_LGSP.PatternGraph pat_ensureMoveLeftValidRule;

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


		public GRGEN_LGSP.PatternGraph ensureMoveLeftValidRule_neg_0;


		private Rule_ensureMoveLeftValidRule()
			: base("ensureMoveLeftValidRule",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new string[] { "ensureMoveLeftValidRule_node_wv", "ensureMoveLeftValidRule_node_bp", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_ensureMoveLeftValidRule+IMatch_ensureMoveLeftValidRule",
				"de.unika.ipd.grGen.Action_Turing3.Rule_ensureMoveLeftValidRule+Match_ensureMoveLeftValidRule"
			)
		{
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
			bool[] ensureMoveLeftValidRule_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ensureMoveLeftValidRule_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, GRGEN_MODEL.NodeType_WriteValue.typeVar, "GRGEN_MODEL.IWriteValue", "ensureMoveLeftValidRule_node_wv", "wv", ensureMoveLeftValidRule_node_wv_AllowedTypes, ensureMoveLeftValidRule_node_wv_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, GRGEN_MODEL.NodeType_State.typeVar, "GRGEN_MODEL.IState", "ensureMoveLeftValidRule_node__node0", "_node0", ensureMoveLeftValidRule_node__node0_AllowedTypes, ensureMoveLeftValidRule_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "ensureMoveLeftValidRule_node_bp", "bp", ensureMoveLeftValidRule_node_bp_AllowedTypes, ensureMoveLeftValidRule_node_bp_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge ensureMoveLeftValidRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveLeft, GRGEN_MODEL.EdgeType_moveLeft.typeVar, "GRGEN_MODEL.ImoveLeft", "ensureMoveLeftValidRule_edge__edge0", "_edge0", ensureMoveLeftValidRule_edge__edge0_AllowedTypes, ensureMoveLeftValidRule_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			bool[,] ensureMoveLeftValidRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ensureMoveLeftValidRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ensureMoveLeftValidRule_neg_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ensureMoveLeftValidRule_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ensureMoveLeftValidRule_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "ensureMoveLeftValidRule_neg_0_node__node0", "_node0", ensureMoveLeftValidRule_neg_0_node__node0_AllowedTypes, ensureMoveLeftValidRule_neg_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge ensureMoveLeftValidRule_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, GRGEN_MODEL.EdgeType_right.typeVar, "GRGEN_MODEL.Iright", "ensureMoveLeftValidRule_neg_0_edge__edge0", "_edge0", ensureMoveLeftValidRule_neg_0_edge__edge0_AllowedTypes, ensureMoveLeftValidRule_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			ensureMoveLeftValidRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ensureMoveLeftValidRule_",
				null, "neg_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveLeftValidRule_neg_0_node__node0, ensureMoveLeftValidRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveLeftValidRule_neg_0_edge__edge0 }, 
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
				ensureMoveLeftValidRule_neg_0_isNodeHomomorphicGlobal,
				ensureMoveLeftValidRule_neg_0_isEdgeHomomorphicGlobal,
				ensureMoveLeftValidRule_neg_0_isNodeTotallyHomomorphic,
				ensureMoveLeftValidRule_neg_0_isEdgeTotallyHomomorphic
			);
			ensureMoveLeftValidRule_neg_0.edgeToSourceNode.Add(ensureMoveLeftValidRule_neg_0_edge__edge0, ensureMoveLeftValidRule_neg_0_node__node0);
			ensureMoveLeftValidRule_neg_0.edgeToTargetNode.Add(ensureMoveLeftValidRule_neg_0_edge__edge0, ensureMoveLeftValidRule_node_bp);

			pat_ensureMoveLeftValidRule = new GRGEN_LGSP.PatternGraph(
				"ensureMoveLeftValidRule",
				"",
				null, "ensureMoveLeftValidRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node__node0, ensureMoveLeftValidRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveLeftValidRule_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ensureMoveLeftValidRule_neg_0,  }, 
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
				ensureMoveLeftValidRule_isNodeHomomorphicGlobal,
				ensureMoveLeftValidRule_isEdgeHomomorphicGlobal,
				ensureMoveLeftValidRule_isNodeTotallyHomomorphic,
				ensureMoveLeftValidRule_isEdgeTotallyHomomorphic
			);
			pat_ensureMoveLeftValidRule.edgeToSourceNode.Add(ensureMoveLeftValidRule_edge__edge0, ensureMoveLeftValidRule_node_wv);
			pat_ensureMoveLeftValidRule.edgeToTargetNode.Add(ensureMoveLeftValidRule_edge__edge0, ensureMoveLeftValidRule_node__node0);
			ensureMoveLeftValidRule_neg_0.embeddingGraph = pat_ensureMoveLeftValidRule;

			ensureMoveLeftValidRule_node_wv.pointOfDefinition = null;
			ensureMoveLeftValidRule_node__node0.pointOfDefinition = pat_ensureMoveLeftValidRule;
			ensureMoveLeftValidRule_node_bp.pointOfDefinition = null;
			ensureMoveLeftValidRule_edge__edge0.pointOfDefinition = pat_ensureMoveLeftValidRule;
			ensureMoveLeftValidRule_neg_0_node__node0.pointOfDefinition = ensureMoveLeftValidRule_neg_0;
			ensureMoveLeftValidRule_neg_0_edge__edge0.pointOfDefinition = ensureMoveLeftValidRule_neg_0;

			patternGraph = pat_ensureMoveLeftValidRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_ensureMoveLeftValidRule curMatch = (Match_ensureMoveLeftValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveLeftValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveLeftValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node__node1, node_bp);
			actionEnv.SelectedMatchRewritten();
			return;
		}
		private static string[] ensureMoveLeftValidRule_addedNodeNames = new string[] { "_node1" };
		private static string[] ensureMoveLeftValidRule_addedEdgeNames = new string[] { "_edge1" };

		static Rule_ensureMoveLeftValidRule() {
		}

		public interface IMatch_ensureMoveLeftValidRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; set; }
			GRGEN_MODEL.IState node__node0 { get; set; }
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			GRGEN_MODEL.ImoveLeft edge__edge0 { get; set; }
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
			GRGEN_MODEL.IBandPosition node__node0 { get; set; }
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			GRGEN_MODEL.Iright edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ensureMoveLeftValidRule : GRGEN_LGSP.MatchListElement<Match_ensureMoveLeftValidRule>, IMatch_ensureMoveLeftValidRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } set { _node_wv = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IState node__node0 { get { return (GRGEN_MODEL.IState)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum ensureMoveLeftValidRule_NodeNums { @wv, @_node0, @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_NodeNums.@wv: return _node_wv;
				case (int)ensureMoveLeftValidRule_NodeNums.@_node0: return _node__node0;
				case (int)ensureMoveLeftValidRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "wv": return _node_wv;
				case "_node0": return _node__node0;
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "wv": _node_wv = (GRGEN_LGSP.LGSPNode)value; break;
				case "_node0": _node__node0 = (GRGEN_LGSP.LGSPNode)value; break;
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.ImoveLeft edge__edge0 { get { return (GRGEN_MODEL.ImoveLeft)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveLeftValidRule_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "_edge0": _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum ensureMoveLeftValidRule_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum ensureMoveLeftValidRule_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveLeftValidRule_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveLeftValidRule_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveLeftValidRule_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveLeftValidRule.instance.pat_ensureMoveLeftValidRule; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ensureMoveLeftValidRule(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_ensureMoveLeftValidRule(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ensureMoveLeftValidRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ensureMoveLeftValidRule cur = this;
				while(cur != null) {
					Match_ensureMoveLeftValidRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_ensureMoveLeftValidRule that)
			{
				_node_wv = that._node_wv;
				_node__node0 = that._node__node0;
				_node_bp = that._node_bp;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ensureMoveLeftValidRule(Match_ensureMoveLeftValidRule that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_ensureMoveLeftValidRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_wv = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_wv];
				_node__node0 = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node__node0];
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_edge__edge0 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge0];
			}

			public Match_ensureMoveLeftValidRule(Match_ensureMoveLeftValidRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_ensureMoveLeftValidRule()
			{
			}

			public bool IsEqual(Match_ensureMoveLeftValidRule that)
			{
				if(that==null) return false;
				if(_node_wv != that._node_wv) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_bp != that._node_bp) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_ensureMoveLeftValidRule_neg_0 : GRGEN_LGSP.MatchListElement<Match_ensureMoveLeftValidRule_neg_0>, IMatch_ensureMoveLeftValidRule_neg_0
		{
			public GRGEN_MODEL.IBandPosition node__node0 { get { return (GRGEN_MODEL.IBandPosition)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum ensureMoveLeftValidRule_neg_0_NodeNums { @_node0, @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_neg_0_NodeNums.@_node0: return _node__node0;
				case (int)ensureMoveLeftValidRule_neg_0_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "_node0": return _node__node0;
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "_node0": _node__node0 = (GRGEN_LGSP.LGSPNode)value; break;
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.Iright edge__edge0 { get { return (GRGEN_MODEL.Iright)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveLeftValidRule_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveLeftValidRule_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "_edge0": _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum ensureMoveLeftValidRule_neg_0_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum ensureMoveLeftValidRule_neg_0_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveLeftValidRule_neg_0_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveLeftValidRule_neg_0_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveLeftValidRule_neg_0_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveLeftValidRule.instance.ensureMoveLeftValidRule_neg_0; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ensureMoveLeftValidRule_neg_0(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_ensureMoveLeftValidRule_neg_0(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ensureMoveLeftValidRule_neg_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ensureMoveLeftValidRule_neg_0 cur = this;
				while(cur != null) {
					Match_ensureMoveLeftValidRule_neg_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_ensureMoveLeftValidRule_neg_0 that)
			{
				_node__node0 = that._node__node0;
				_node_bp = that._node_bp;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ensureMoveLeftValidRule_neg_0(Match_ensureMoveLeftValidRule_neg_0 that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_ensureMoveLeftValidRule_neg_0 that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node__node0 = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node__node0];
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_edge__edge0 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge0];
			}

			public Match_ensureMoveLeftValidRule_neg_0(Match_ensureMoveLeftValidRule_neg_0 that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_ensureMoveLeftValidRule_neg_0()
			{
			}

			public bool IsEqual(Match_ensureMoveLeftValidRule_neg_0 that)
			{
				if(that==null) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_bp != that._node_bp) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IWriteValue> Extract_wv(List<IMatch_ensureMoveLeftValidRule> matchList)
			{
				List<GRGEN_MODEL.IWriteValue> resultList = new List<GRGEN_MODEL.IWriteValue>(matchList.Count);
				foreach(IMatch_ensureMoveLeftValidRule match in matchList)
					resultList.Add(match.node_wv);
				return resultList;
			}
			public static List<GRGEN_MODEL.IState> Extract__node0(List<IMatch_ensureMoveLeftValidRule> matchList)
			{
				List<GRGEN_MODEL.IState> resultList = new List<GRGEN_MODEL.IState>(matchList.Count);
				foreach(IMatch_ensureMoveLeftValidRule match in matchList)
					resultList.Add(match.node__node0);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_ensureMoveLeftValidRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_ensureMoveLeftValidRule match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
			public static List<GRGEN_MODEL.ImoveLeft> Extract__edge0(List<IMatch_ensureMoveLeftValidRule> matchList)
			{
				List<GRGEN_MODEL.ImoveLeft> resultList = new List<GRGEN_MODEL.ImoveLeft>(matchList.Count);
				foreach(IMatch_ensureMoveLeftValidRule match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>)
				return ((List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule instanceBearingAttributeForSearch_ensureMoveLeftValidRule = new GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule();
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_groupBy_wv(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>> seenValues = new Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_wv)) {
					seenValues[list[pos].@node_wv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_wv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_keepOneForEachBy_wv(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_wv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_wv, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_groupBy__node0(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>> seenValues = new Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node__node0)) {
					seenValues[list[pos].@node__node0].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node__node0, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_keepOneForEachBy__node0(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node__node0)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node__node0, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_groupBy_bp(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_groupBy__edge0(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			Dictionary<GRGEN_MODEL.ImoveLeft, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>> seenValues = new Dictionary<GRGEN_MODEL.ImoveLeft, List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge0)) {
					seenValues[list[pos].@edge__edge0].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge0, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Array_ensureMoveLeftValidRule_keepOneForEachBy__edge0(List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>();
			Dictionary<GRGEN_MODEL.ImoveLeft, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.ImoveLeft, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge0)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge0, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.ImoveLeft entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.ImoveLeft entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.ImoveLeft entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveLeftValidRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> list, GRGEN_MODEL.ImoveLeft entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
	}

	public class Rule_ensureMoveRightValidRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ensureMoveRightValidRule instance = null;
		public static Rule_ensureMoveRightValidRule Instance { get { if(instance==null) { instance = new Rule_ensureMoveRightValidRule(); instance.initialize(); } return instance; } }

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





		public GRGEN_LGSP.PatternGraph pat_ensureMoveRightValidRule;

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


		public GRGEN_LGSP.PatternGraph ensureMoveRightValidRule_neg_0;


		private Rule_ensureMoveRightValidRule()
			: base("ensureMoveRightValidRule",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new string[] { "ensureMoveRightValidRule_node_wv", "ensureMoveRightValidRule_node_bp", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_ensureMoveRightValidRule+IMatch_ensureMoveRightValidRule",
				"de.unika.ipd.grGen.Action_Turing3.Rule_ensureMoveRightValidRule+Match_ensureMoveRightValidRule"
			)
		{
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
			bool[] ensureMoveRightValidRule_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] ensureMoveRightValidRule_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, GRGEN_MODEL.NodeType_WriteValue.typeVar, "GRGEN_MODEL.IWriteValue", "ensureMoveRightValidRule_node_wv", "wv", ensureMoveRightValidRule_node_wv_AllowedTypes, ensureMoveRightValidRule_node_wv_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, GRGEN_MODEL.NodeType_State.typeVar, "GRGEN_MODEL.IState", "ensureMoveRightValidRule_node__node0", "_node0", ensureMoveRightValidRule_node__node0_AllowedTypes, ensureMoveRightValidRule_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "ensureMoveRightValidRule_node_bp", "bp", ensureMoveRightValidRule_node_bp_AllowedTypes, ensureMoveRightValidRule_node_bp_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge ensureMoveRightValidRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveRight, GRGEN_MODEL.EdgeType_moveRight.typeVar, "GRGEN_MODEL.ImoveRight", "ensureMoveRightValidRule_edge__edge0", "_edge0", ensureMoveRightValidRule_edge__edge0_AllowedTypes, ensureMoveRightValidRule_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			bool[,] ensureMoveRightValidRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ensureMoveRightValidRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ensureMoveRightValidRule_neg_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ensureMoveRightValidRule_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ensureMoveRightValidRule_neg_0_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "ensureMoveRightValidRule_neg_0_node__node0", "_node0", ensureMoveRightValidRule_neg_0_node__node0_AllowedTypes, ensureMoveRightValidRule_neg_0_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge ensureMoveRightValidRule_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, GRGEN_MODEL.EdgeType_right.typeVar, "GRGEN_MODEL.Iright", "ensureMoveRightValidRule_neg_0_edge__edge0", "_edge0", ensureMoveRightValidRule_neg_0_edge__edge0_AllowedTypes, ensureMoveRightValidRule_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			ensureMoveRightValidRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ensureMoveRightValidRule_",
				null, "neg_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveRightValidRule_node_bp, ensureMoveRightValidRule_neg_0_node__node0 }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveRightValidRule_neg_0_edge__edge0 }, 
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
				ensureMoveRightValidRule_neg_0_isNodeHomomorphicGlobal,
				ensureMoveRightValidRule_neg_0_isEdgeHomomorphicGlobal,
				ensureMoveRightValidRule_neg_0_isNodeTotallyHomomorphic,
				ensureMoveRightValidRule_neg_0_isEdgeTotallyHomomorphic
			);
			ensureMoveRightValidRule_neg_0.edgeToSourceNode.Add(ensureMoveRightValidRule_neg_0_edge__edge0, ensureMoveRightValidRule_node_bp);
			ensureMoveRightValidRule_neg_0.edgeToTargetNode.Add(ensureMoveRightValidRule_neg_0_edge__edge0, ensureMoveRightValidRule_neg_0_node__node0);

			pat_ensureMoveRightValidRule = new GRGEN_LGSP.PatternGraph(
				"ensureMoveRightValidRule",
				"",
				null, "ensureMoveRightValidRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node__node0, ensureMoveRightValidRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { ensureMoveRightValidRule_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ensureMoveRightValidRule_neg_0,  }, 
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
				ensureMoveRightValidRule_isNodeHomomorphicGlobal,
				ensureMoveRightValidRule_isEdgeHomomorphicGlobal,
				ensureMoveRightValidRule_isNodeTotallyHomomorphic,
				ensureMoveRightValidRule_isEdgeTotallyHomomorphic
			);
			pat_ensureMoveRightValidRule.edgeToSourceNode.Add(ensureMoveRightValidRule_edge__edge0, ensureMoveRightValidRule_node_wv);
			pat_ensureMoveRightValidRule.edgeToTargetNode.Add(ensureMoveRightValidRule_edge__edge0, ensureMoveRightValidRule_node__node0);
			ensureMoveRightValidRule_neg_0.embeddingGraph = pat_ensureMoveRightValidRule;

			ensureMoveRightValidRule_node_wv.pointOfDefinition = null;
			ensureMoveRightValidRule_node__node0.pointOfDefinition = pat_ensureMoveRightValidRule;
			ensureMoveRightValidRule_node_bp.pointOfDefinition = null;
			ensureMoveRightValidRule_edge__edge0.pointOfDefinition = pat_ensureMoveRightValidRule;
			ensureMoveRightValidRule_neg_0_node__node0.pointOfDefinition = ensureMoveRightValidRule_neg_0;
			ensureMoveRightValidRule_neg_0_edge__edge0.pointOfDefinition = ensureMoveRightValidRule_neg_0;

			patternGraph = pat_ensureMoveRightValidRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_ensureMoveRightValidRule curMatch = (Match_ensureMoveRightValidRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_bp = curMatch._node_bp;
			graph.SettingAddedNodeNames( ensureMoveRightValidRule_addedNodeNames );
			GRGEN_MODEL.@BandPosition node__node1 = GRGEN_MODEL.@BandPosition.CreateNode(graph);
			graph.SettingAddedEdgeNames( ensureMoveRightValidRule_addedEdgeNames );
			GRGEN_MODEL.@right edge__edge1 = GRGEN_MODEL.@right.CreateEdge(graph, node_bp, node__node1);
			actionEnv.SelectedMatchRewritten();
			return;
		}
		private static string[] ensureMoveRightValidRule_addedNodeNames = new string[] { "_node1" };
		private static string[] ensureMoveRightValidRule_addedEdgeNames = new string[] { "_edge1" };

		static Rule_ensureMoveRightValidRule() {
		}

		public interface IMatch_ensureMoveRightValidRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; set; }
			GRGEN_MODEL.IState node__node0 { get; set; }
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			GRGEN_MODEL.ImoveRight edge__edge0 { get; set; }
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
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			GRGEN_MODEL.IBandPosition node__node0 { get; set; }
			//Edges
			GRGEN_MODEL.Iright edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ensureMoveRightValidRule : GRGEN_LGSP.MatchListElement<Match_ensureMoveRightValidRule>, IMatch_ensureMoveRightValidRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } set { _node_wv = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IState node__node0 { get { return (GRGEN_MODEL.IState)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum ensureMoveRightValidRule_NodeNums { @wv, @_node0, @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_NodeNums.@wv: return _node_wv;
				case (int)ensureMoveRightValidRule_NodeNums.@_node0: return _node__node0;
				case (int)ensureMoveRightValidRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "wv": return _node_wv;
				case "_node0": return _node__node0;
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "wv": _node_wv = (GRGEN_LGSP.LGSPNode)value; break;
				case "_node0": _node__node0 = (GRGEN_LGSP.LGSPNode)value; break;
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.ImoveRight edge__edge0 { get { return (GRGEN_MODEL.ImoveRight)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveRightValidRule_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "_edge0": _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum ensureMoveRightValidRule_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum ensureMoveRightValidRule_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveRightValidRule_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveRightValidRule_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveRightValidRule_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveRightValidRule.instance.pat_ensureMoveRightValidRule; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ensureMoveRightValidRule(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_ensureMoveRightValidRule(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ensureMoveRightValidRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ensureMoveRightValidRule cur = this;
				while(cur != null) {
					Match_ensureMoveRightValidRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_ensureMoveRightValidRule that)
			{
				_node_wv = that._node_wv;
				_node__node0 = that._node__node0;
				_node_bp = that._node_bp;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ensureMoveRightValidRule(Match_ensureMoveRightValidRule that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_ensureMoveRightValidRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_wv = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_wv];
				_node__node0 = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node__node0];
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_edge__edge0 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge0];
			}

			public Match_ensureMoveRightValidRule(Match_ensureMoveRightValidRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_ensureMoveRightValidRule()
			{
			}

			public bool IsEqual(Match_ensureMoveRightValidRule that)
			{
				if(that==null) return false;
				if(_node_wv != that._node_wv) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_bp != that._node_bp) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_ensureMoveRightValidRule_neg_0 : GRGEN_LGSP.MatchListElement<Match_ensureMoveRightValidRule_neg_0>, IMatch_ensureMoveRightValidRule_neg_0
		{
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node__node0 { get { return (GRGEN_MODEL.IBandPosition)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_bp;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public enum ensureMoveRightValidRule_neg_0_NodeNums { @bp, @_node0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_neg_0_NodeNums.@bp: return _node_bp;
				case (int)ensureMoveRightValidRule_neg_0_NodeNums.@_node0: return _node__node0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "bp": return _node_bp;
				case "_node0": return _node__node0;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				case "_node0": _node__node0 = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.Iright edge__edge0 { get { return (GRGEN_MODEL.Iright)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ensureMoveRightValidRule_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ensureMoveRightValidRule_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "_edge0": _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum ensureMoveRightValidRule_neg_0_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum ensureMoveRightValidRule_neg_0_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveRightValidRule_neg_0_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveRightValidRule_neg_0_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum ensureMoveRightValidRule_neg_0_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ensureMoveRightValidRule.instance.ensureMoveRightValidRule_neg_0; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ensureMoveRightValidRule_neg_0(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_ensureMoveRightValidRule_neg_0(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ensureMoveRightValidRule_neg_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ensureMoveRightValidRule_neg_0 cur = this;
				while(cur != null) {
					Match_ensureMoveRightValidRule_neg_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_ensureMoveRightValidRule_neg_0 that)
			{
				_node_bp = that._node_bp;
				_node__node0 = that._node__node0;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ensureMoveRightValidRule_neg_0(Match_ensureMoveRightValidRule_neg_0 that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_ensureMoveRightValidRule_neg_0 that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_node__node0 = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node__node0];
				_edge__edge0 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge0];
			}

			public Match_ensureMoveRightValidRule_neg_0(Match_ensureMoveRightValidRule_neg_0 that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_ensureMoveRightValidRule_neg_0()
			{
			}

			public bool IsEqual(Match_ensureMoveRightValidRule_neg_0 that)
			{
				if(that==null) return false;
				if(_node_bp != that._node_bp) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IWriteValue> Extract_wv(List<IMatch_ensureMoveRightValidRule> matchList)
			{
				List<GRGEN_MODEL.IWriteValue> resultList = new List<GRGEN_MODEL.IWriteValue>(matchList.Count);
				foreach(IMatch_ensureMoveRightValidRule match in matchList)
					resultList.Add(match.node_wv);
				return resultList;
			}
			public static List<GRGEN_MODEL.IState> Extract__node0(List<IMatch_ensureMoveRightValidRule> matchList)
			{
				List<GRGEN_MODEL.IState> resultList = new List<GRGEN_MODEL.IState>(matchList.Count);
				foreach(IMatch_ensureMoveRightValidRule match in matchList)
					resultList.Add(match.node__node0);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_ensureMoveRightValidRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_ensureMoveRightValidRule match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
			public static List<GRGEN_MODEL.ImoveRight> Extract__edge0(List<IMatch_ensureMoveRightValidRule> matchList)
			{
				List<GRGEN_MODEL.ImoveRight> resultList = new List<GRGEN_MODEL.ImoveRight>(matchList.Count);
				foreach(IMatch_ensureMoveRightValidRule match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>)
				return ((List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule instanceBearingAttributeForSearch_ensureMoveRightValidRule = new GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule();
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_groupBy_wv(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>> seenValues = new Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_wv)) {
					seenValues[list[pos].@node_wv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_wv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_keepOneForEachBy_wv(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_wv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_wv, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_groupBy__node0(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>> seenValues = new Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node__node0)) {
					seenValues[list[pos].@node__node0].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node__node0, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_keepOneForEachBy__node0(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node__node0)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node__node0, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy__node0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node__node0.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_groupBy_bp(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_groupBy__edge0(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			Dictionary<GRGEN_MODEL.ImoveRight, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>> seenValues = new Dictionary<GRGEN_MODEL.ImoveRight, List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge0)) {
					seenValues[list[pos].@edge__edge0].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> tempList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge0, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			foreach(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Array_ensureMoveRightValidRule_keepOneForEachBy__edge0(List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list)
		{
			List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> newList = new List<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>();
			Dictionary<GRGEN_MODEL.ImoveRight, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.ImoveRight, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge0)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge0, null);
				}
			}
			return newList;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.ImoveRight entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.ImoveRight entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.ImoveRight entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_ensureMoveRightValidRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> list, GRGEN_MODEL.ImoveRight entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
	}

	public class Rule_moveLeftRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_moveLeftRule instance = null;
		public static Rule_moveLeftRule Instance { get { if(instance==null) { instance = new Rule_moveLeftRule(); instance.initialize(); } return instance; } }

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





		public GRGEN_LGSP.PatternGraph pat_moveLeftRule;


		private Rule_moveLeftRule()
			: base("moveLeftRule",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new string[] { "moveLeftRule_node_wv", "moveLeftRule_node_bp", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_moveLeftRule+IMatch_moveLeftRule",
				"de.unika.ipd.grGen.Action_Turing3.Rule_moveLeftRule+Match_moveLeftRule"
			)
		{
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
			bool[] moveLeftRule_isNodeTotallyHomomorphic = new bool[4] { false, false, false, false,  };
			bool[] moveLeftRule_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode moveLeftRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, GRGEN_MODEL.NodeType_WriteValue.typeVar, "GRGEN_MODEL.IWriteValue", "moveLeftRule_node_wv", "wv", moveLeftRule_node_wv_AllowedTypes, moveLeftRule_node_wv_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode moveLeftRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, GRGEN_MODEL.NodeType_State.typeVar, "GRGEN_MODEL.IState", "moveLeftRule_node_s", "s", moveLeftRule_node_s_AllowedTypes, moveLeftRule_node_s_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode moveLeftRule_node_lbp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "moveLeftRule_node_lbp", "lbp", moveLeftRule_node_lbp_AllowedTypes, moveLeftRule_node_lbp_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode moveLeftRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "moveLeftRule_node_bp", "bp", moveLeftRule_node_bp_AllowedTypes, moveLeftRule_node_bp_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge moveLeftRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveLeft, GRGEN_MODEL.EdgeType_moveLeft.typeVar, "GRGEN_MODEL.ImoveLeft", "moveLeftRule_edge__edge0", "_edge0", moveLeftRule_edge__edge0_AllowedTypes, moveLeftRule_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge moveLeftRule_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, GRGEN_MODEL.EdgeType_right.typeVar, "GRGEN_MODEL.Iright", "moveLeftRule_edge__edge1", "_edge1", moveLeftRule_edge__edge1_AllowedTypes, moveLeftRule_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			pat_moveLeftRule = new GRGEN_LGSP.PatternGraph(
				"moveLeftRule",
				"",
				null, "moveLeftRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { moveLeftRule_node_wv, moveLeftRule_node_s, moveLeftRule_node_lbp, moveLeftRule_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] { moveLeftRule_edge__edge0, moveLeftRule_edge__edge1 }, 
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
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				moveLeftRule_isNodeHomomorphicGlobal,
				moveLeftRule_isEdgeHomomorphicGlobal,
				moveLeftRule_isNodeTotallyHomomorphic,
				moveLeftRule_isEdgeTotallyHomomorphic
			);
			pat_moveLeftRule.edgeToSourceNode.Add(moveLeftRule_edge__edge0, moveLeftRule_node_wv);
			pat_moveLeftRule.edgeToTargetNode.Add(moveLeftRule_edge__edge0, moveLeftRule_node_s);
			pat_moveLeftRule.edgeToSourceNode.Add(moveLeftRule_edge__edge1, moveLeftRule_node_lbp);
			pat_moveLeftRule.edgeToTargetNode.Add(moveLeftRule_edge__edge1, moveLeftRule_node_bp);

			moveLeftRule_node_wv.pointOfDefinition = null;
			moveLeftRule_node_s.pointOfDefinition = pat_moveLeftRule;
			moveLeftRule_node_lbp.pointOfDefinition = pat_moveLeftRule;
			moveLeftRule_node_bp.pointOfDefinition = null;
			moveLeftRule_edge__edge0.pointOfDefinition = pat_moveLeftRule;
			moveLeftRule_edge__edge1.pointOfDefinition = pat_moveLeftRule;

			patternGraph = pat_moveLeftRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_moveLeftRule curMatch = (Match_moveLeftRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_lbp = curMatch._node_lbp;
			graph.SettingAddedNodeNames( moveLeftRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveLeftRule_addedEdgeNames );
			actionEnv.SelectedMatchRewritten();
			output_0 = (GRGEN_MODEL.IState)(node_s);
			output_1 = (GRGEN_MODEL.IBandPosition)(node_lbp);
			return;
		}
		private static string[] moveLeftRule_addedNodeNames = new string[] {  };
		private static string[] moveLeftRule_addedEdgeNames = new string[] {  };

		static Rule_moveLeftRule() {
		}

		public interface IMatch_moveLeftRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; set; }
			GRGEN_MODEL.IState node_s { get; set; }
			GRGEN_MODEL.IBandPosition node_lbp { get; set; }
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			GRGEN_MODEL.ImoveLeft edge__edge0 { get; set; }
			GRGEN_MODEL.Iright edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_moveLeftRule : GRGEN_LGSP.MatchListElement<Match_moveLeftRule>, IMatch_moveLeftRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } set { _node_wv = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } set { _node_s = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_lbp { get { return (GRGEN_MODEL.IBandPosition)_node_lbp; } set { _node_lbp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_lbp;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum moveLeftRule_NodeNums { @wv, @s, @lbp, @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 4; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)moveLeftRule_NodeNums.@wv: return _node_wv;
				case (int)moveLeftRule_NodeNums.@s: return _node_s;
				case (int)moveLeftRule_NodeNums.@lbp: return _node_lbp;
				case (int)moveLeftRule_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "wv": return _node_wv;
				case "s": return _node_s;
				case "lbp": return _node_lbp;
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "wv": _node_wv = (GRGEN_LGSP.LGSPNode)value; break;
				case "s": _node_s = (GRGEN_LGSP.LGSPNode)value; break;
				case "lbp": _node_lbp = (GRGEN_LGSP.LGSPNode)value; break;
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.ImoveLeft edge__edge0 { get { return (GRGEN_MODEL.ImoveLeft)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_MODEL.Iright edge__edge1 { get { return (GRGEN_MODEL.Iright)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum moveLeftRule_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 2; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)moveLeftRule_EdgeNums.@_edge0: return _edge__edge0;
				case (int)moveLeftRule_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "_edge0": _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; break;
				case "_edge1": _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum moveLeftRule_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum moveLeftRule_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum moveLeftRule_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum moveLeftRule_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum moveLeftRule_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_moveLeftRule.instance.pat_moveLeftRule; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_moveLeftRule(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_moveLeftRule(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_moveLeftRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_moveLeftRule cur = this;
				while(cur != null) {
					Match_moveLeftRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_moveLeftRule that)
			{
				_node_wv = that._node_wv;
				_node_s = that._node_s;
				_node_lbp = that._node_lbp;
				_node_bp = that._node_bp;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
			}

			public Match_moveLeftRule(Match_moveLeftRule that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_moveLeftRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_wv = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_wv];
				_node_s = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_s];
				_node_lbp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_lbp];
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_edge__edge0 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge0];
				_edge__edge1 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge1];
			}

			public Match_moveLeftRule(Match_moveLeftRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_moveLeftRule()
			{
			}

			public bool IsEqual(Match_moveLeftRule that)
			{
				if(that==null) return false;
				if(_node_wv != that._node_wv) return false;
				if(_node_s != that._node_s) return false;
				if(_node_lbp != that._node_lbp) return false;
				if(_node_bp != that._node_bp) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IWriteValue> Extract_wv(List<IMatch_moveLeftRule> matchList)
			{
				List<GRGEN_MODEL.IWriteValue> resultList = new List<GRGEN_MODEL.IWriteValue>(matchList.Count);
				foreach(IMatch_moveLeftRule match in matchList)
					resultList.Add(match.node_wv);
				return resultList;
			}
			public static List<GRGEN_MODEL.IState> Extract_s(List<IMatch_moveLeftRule> matchList)
			{
				List<GRGEN_MODEL.IState> resultList = new List<GRGEN_MODEL.IState>(matchList.Count);
				foreach(IMatch_moveLeftRule match in matchList)
					resultList.Add(match.node_s);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_lbp(List<IMatch_moveLeftRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_moveLeftRule match in matchList)
					resultList.Add(match.node_lbp);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_moveLeftRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_moveLeftRule match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
			public static List<GRGEN_MODEL.ImoveLeft> Extract__edge0(List<IMatch_moveLeftRule> matchList)
			{
				List<GRGEN_MODEL.ImoveLeft> resultList = new List<GRGEN_MODEL.ImoveLeft>(matchList.Count);
				foreach(IMatch_moveLeftRule match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
			public static List<GRGEN_MODEL.Iright> Extract__edge1(List<IMatch_moveLeftRule> matchList)
			{
				List<GRGEN_MODEL.Iright> resultList = new List<GRGEN_MODEL.Iright>(matchList.Count);
				foreach(IMatch_moveLeftRule match in matchList)
					resultList.Add(match.edge__edge1);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>)
				return ((List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule instanceBearingAttributeForSearch_moveLeftRule = new GRGEN_ACTIONS.Rule_moveLeftRule.Match_moveLeftRule();
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_groupBy_wv(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>> seenValues = new Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_wv)) {
					seenValues[list[pos].@node_wv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> tempList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_wv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_keepOneForEachBy_wv(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_wv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_wv, null);
				}
			}
			return newList;
		}
		public static int Array_moveLeftRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_groupBy_s(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>> seenValues = new Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_s)) {
					seenValues[list[pos].@node_s].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> tempList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_s, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_keepOneForEachBy_s(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_s)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_s, null);
				}
			}
			return newList;
		}
		public static int Array_moveLeftRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_groupBy_lbp(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_lbp)) {
					seenValues[list[pos].@node_lbp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> tempList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_lbp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_keepOneForEachBy_lbp(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_lbp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_lbp, null);
				}
			}
			return newList;
		}
		public static int Array_moveLeftRule_indexOfBy_lbp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_lbp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_indexOfBy_lbp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_lbp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_lbp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_lbp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_lbp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_lbp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_groupBy_bp(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> tempList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_moveLeftRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_groupBy__edge0(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			Dictionary<GRGEN_MODEL.ImoveLeft, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>> seenValues = new Dictionary<GRGEN_MODEL.ImoveLeft, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge0)) {
					seenValues[list[pos].@edge__edge0].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> tempList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge0, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_keepOneForEachBy__edge0(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			Dictionary<GRGEN_MODEL.ImoveLeft, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.ImoveLeft, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge0)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge0, null);
				}
			}
			return newList;
		}
		public static int Array_moveLeftRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.ImoveLeft entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.ImoveLeft entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.ImoveLeft entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.ImoveLeft entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_groupBy__edge1(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			Dictionary<GRGEN_MODEL.Iright, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>> seenValues = new Dictionary<GRGEN_MODEL.Iright, List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge1)) {
					seenValues[list[pos].@edge__edge1].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> tempList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge1, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> Array_moveLeftRule_keepOneForEachBy__edge1(List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> newList = new List<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule>();
			Dictionary<GRGEN_MODEL.Iright, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.Iright, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge1)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge1, null);
				}
			}
			return newList;
		}
		public static int Array_moveLeftRule_indexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.Iright entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_indexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.Iright entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.Iright entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveLeftRule_lastIndexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveLeftRule.IMatch_moveLeftRule> list, GRGEN_MODEL.Iright entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
	}

	public class Rule_moveRightRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_moveRightRule instance = null;
		public static Rule_moveRightRule Instance { get { if(instance==null) { instance = new Rule_moveRightRule(); instance.initialize(); } return instance; } }

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





		public GRGEN_LGSP.PatternGraph pat_moveRightRule;


		private Rule_moveRightRule()
			: base("moveRightRule",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_WriteValue.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new string[] { "moveRightRule_node_wv", "moveRightRule_node_bp", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_State.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_moveRightRule+IMatch_moveRightRule",
				"de.unika.ipd.grGen.Action_Turing3.Rule_moveRightRule+Match_moveRightRule"
			)
		{
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
			bool[] moveRightRule_isNodeTotallyHomomorphic = new bool[4] { false, false, false, false,  };
			bool[] moveRightRule_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode moveRightRule_node_wv = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@WriteValue, GRGEN_MODEL.NodeType_WriteValue.typeVar, "GRGEN_MODEL.IWriteValue", "moveRightRule_node_wv", "wv", moveRightRule_node_wv_AllowedTypes, moveRightRule_node_wv_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode moveRightRule_node_s = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@State, GRGEN_MODEL.NodeType_State.typeVar, "GRGEN_MODEL.IState", "moveRightRule_node_s", "s", moveRightRule_node_s_AllowedTypes, moveRightRule_node_s_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode moveRightRule_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "moveRightRule_node_bp", "bp", moveRightRule_node_bp_AllowedTypes, moveRightRule_node_bp_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode moveRightRule_node_rbp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "moveRightRule_node_rbp", "rbp", moveRightRule_node_rbp_AllowedTypes, moveRightRule_node_rbp_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge moveRightRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@moveRight, GRGEN_MODEL.EdgeType_moveRight.typeVar, "GRGEN_MODEL.ImoveRight", "moveRightRule_edge__edge0", "_edge0", moveRightRule_edge__edge0_AllowedTypes, moveRightRule_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge moveRightRule_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@right, GRGEN_MODEL.EdgeType_right.typeVar, "GRGEN_MODEL.Iright", "moveRightRule_edge__edge1", "_edge1", moveRightRule_edge__edge1_AllowedTypes, moveRightRule_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			pat_moveRightRule = new GRGEN_LGSP.PatternGraph(
				"moveRightRule",
				"",
				null, "moveRightRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { moveRightRule_node_wv, moveRightRule_node_s, moveRightRule_node_bp, moveRightRule_node_rbp }, 
				new GRGEN_LGSP.PatternEdge[] { moveRightRule_edge__edge0, moveRightRule_edge__edge1 }, 
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
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				moveRightRule_isNodeHomomorphicGlobal,
				moveRightRule_isEdgeHomomorphicGlobal,
				moveRightRule_isNodeTotallyHomomorphic,
				moveRightRule_isEdgeTotallyHomomorphic
			);
			pat_moveRightRule.edgeToSourceNode.Add(moveRightRule_edge__edge0, moveRightRule_node_wv);
			pat_moveRightRule.edgeToTargetNode.Add(moveRightRule_edge__edge0, moveRightRule_node_s);
			pat_moveRightRule.edgeToSourceNode.Add(moveRightRule_edge__edge1, moveRightRule_node_bp);
			pat_moveRightRule.edgeToTargetNode.Add(moveRightRule_edge__edge1, moveRightRule_node_rbp);

			moveRightRule_node_wv.pointOfDefinition = null;
			moveRightRule_node_s.pointOfDefinition = pat_moveRightRule;
			moveRightRule_node_bp.pointOfDefinition = null;
			moveRightRule_node_rbp.pointOfDefinition = pat_moveRightRule;
			moveRightRule_edge__edge0.pointOfDefinition = pat_moveRightRule;
			moveRightRule_edge__edge1.pointOfDefinition = pat_moveRightRule;

			patternGraph = pat_moveRightRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_moveRightRule curMatch = (Match_moveRightRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_s = curMatch._node_s;
			GRGEN_LGSP.LGSPNode node_rbp = curMatch._node_rbp;
			graph.SettingAddedNodeNames( moveRightRule_addedNodeNames );
			graph.SettingAddedEdgeNames( moveRightRule_addedEdgeNames );
			actionEnv.SelectedMatchRewritten();
			output_0 = (GRGEN_MODEL.IState)(node_s);
			output_1 = (GRGEN_MODEL.IBandPosition)(node_rbp);
			return;
		}
		private static string[] moveRightRule_addedNodeNames = new string[] {  };
		private static string[] moveRightRule_addedEdgeNames = new string[] {  };

		static Rule_moveRightRule() {
		}

		public interface IMatch_moveRightRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IWriteValue node_wv { get; set; }
			GRGEN_MODEL.IState node_s { get; set; }
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			GRGEN_MODEL.IBandPosition node_rbp { get; set; }
			//Edges
			GRGEN_MODEL.ImoveRight edge__edge0 { get; set; }
			GRGEN_MODEL.Iright edge__edge1 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_moveRightRule : GRGEN_LGSP.MatchListElement<Match_moveRightRule>, IMatch_moveRightRule
		{
			public GRGEN_MODEL.IWriteValue node_wv { get { return (GRGEN_MODEL.IWriteValue)_node_wv; } set { _node_wv = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IState node_s { get { return (GRGEN_MODEL.IState)_node_s; } set { _node_s = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IBandPosition node_rbp { get { return (GRGEN_MODEL.IBandPosition)_node_rbp; } set { _node_rbp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_wv;
			public GRGEN_LGSP.LGSPNode _node_s;
			public GRGEN_LGSP.LGSPNode _node_bp;
			public GRGEN_LGSP.LGSPNode _node_rbp;
			public enum moveRightRule_NodeNums { @wv, @s, @bp, @rbp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 4; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)moveRightRule_NodeNums.@wv: return _node_wv;
				case (int)moveRightRule_NodeNums.@s: return _node_s;
				case (int)moveRightRule_NodeNums.@bp: return _node_bp;
				case (int)moveRightRule_NodeNums.@rbp: return _node_rbp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "wv": return _node_wv;
				case "s": return _node_s;
				case "bp": return _node_bp;
				case "rbp": return _node_rbp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "wv": _node_wv = (GRGEN_LGSP.LGSPNode)value; break;
				case "s": _node_s = (GRGEN_LGSP.LGSPNode)value; break;
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				case "rbp": _node_rbp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_MODEL.ImoveRight edge__edge0 { get { return (GRGEN_MODEL.ImoveRight)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_MODEL.Iright edge__edge1 { get { return (GRGEN_MODEL.Iright)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum moveRightRule_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 2; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)moveRightRule_EdgeNums.@_edge0: return _edge__edge0;
				case (int)moveRightRule_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				case "_edge0": _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; break;
				case "_edge1": _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; break;
				default: break;
				}
			}

			public enum moveRightRule_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum moveRightRule_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum moveRightRule_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum moveRightRule_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum moveRightRule_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_moveRightRule.instance.pat_moveRightRule; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_moveRightRule(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_moveRightRule(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_moveRightRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_moveRightRule cur = this;
				while(cur != null) {
					Match_moveRightRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_moveRightRule that)
			{
				_node_wv = that._node_wv;
				_node_s = that._node_s;
				_node_bp = that._node_bp;
				_node_rbp = that._node_rbp;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
			}

			public Match_moveRightRule(Match_moveRightRule that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_moveRightRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_wv = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_wv];
				_node_s = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_s];
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
				_node_rbp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_rbp];
				_edge__edge0 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge0];
				_edge__edge1 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge1];
			}

			public Match_moveRightRule(Match_moveRightRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_moveRightRule()
			{
			}

			public bool IsEqual(Match_moveRightRule that)
			{
				if(that==null) return false;
				if(_node_wv != that._node_wv) return false;
				if(_node_s != that._node_s) return false;
				if(_node_bp != that._node_bp) return false;
				if(_node_rbp != that._node_rbp) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IWriteValue> Extract_wv(List<IMatch_moveRightRule> matchList)
			{
				List<GRGEN_MODEL.IWriteValue> resultList = new List<GRGEN_MODEL.IWriteValue>(matchList.Count);
				foreach(IMatch_moveRightRule match in matchList)
					resultList.Add(match.node_wv);
				return resultList;
			}
			public static List<GRGEN_MODEL.IState> Extract_s(List<IMatch_moveRightRule> matchList)
			{
				List<GRGEN_MODEL.IState> resultList = new List<GRGEN_MODEL.IState>(matchList.Count);
				foreach(IMatch_moveRightRule match in matchList)
					resultList.Add(match.node_s);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_moveRightRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_moveRightRule match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
			public static List<GRGEN_MODEL.IBandPosition> Extract_rbp(List<IMatch_moveRightRule> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_moveRightRule match in matchList)
					resultList.Add(match.node_rbp);
				return resultList;
			}
			public static List<GRGEN_MODEL.ImoveRight> Extract__edge0(List<IMatch_moveRightRule> matchList)
			{
				List<GRGEN_MODEL.ImoveRight> resultList = new List<GRGEN_MODEL.ImoveRight>(matchList.Count);
				foreach(IMatch_moveRightRule match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
			public static List<GRGEN_MODEL.Iright> Extract__edge1(List<IMatch_moveRightRule> matchList)
			{
				List<GRGEN_MODEL.Iright> resultList = new List<GRGEN_MODEL.Iright>(matchList.Count);
				foreach(IMatch_moveRightRule match in matchList)
					resultList.Add(match.edge__edge1);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>)
				return ((List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule instanceBearingAttributeForSearch_moveRightRule = new GRGEN_ACTIONS.Rule_moveRightRule.Match_moveRightRule();
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_groupBy_wv(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>> seenValues = new Dictionary<GRGEN_MODEL.IWriteValue, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_wv)) {
					seenValues[list[pos].@node_wv].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> tempList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_wv, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_keepOneForEachBy_wv(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IWriteValue, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_wv)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_wv, null);
				}
			}
			return newList;
		}
		public static int Array_moveRightRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_indexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IWriteValue entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_wv(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IWriteValue entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_wv.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_groupBy_s(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>> seenValues = new Dictionary<GRGEN_MODEL.IState, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_s)) {
					seenValues[list[pos].@node_s].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> tempList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_s, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_keepOneForEachBy_s(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IState, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_s)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_s, null);
				}
			}
			return newList;
		}
		public static int Array_moveRightRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_indexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IState entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_s(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IState entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_s.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_groupBy_bp(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> tempList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_moveRightRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_groupBy_rbp(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_rbp)) {
					seenValues[list[pos].@node_rbp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> tempList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_rbp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_keepOneForEachBy_rbp(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_rbp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_rbp, null);
				}
			}
			return newList;
		}
		public static int Array_moveRightRule_indexOfBy_rbp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_rbp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_indexOfBy_rbp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_rbp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_rbp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_rbp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy_rbp(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_rbp.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_groupBy__edge0(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			Dictionary<GRGEN_MODEL.ImoveRight, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>> seenValues = new Dictionary<GRGEN_MODEL.ImoveRight, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge0)) {
					seenValues[list[pos].@edge__edge0].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> tempList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge0, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_keepOneForEachBy__edge0(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			Dictionary<GRGEN_MODEL.ImoveRight, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.ImoveRight, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge0)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge0, null);
				}
			}
			return newList;
		}
		public static int Array_moveRightRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.ImoveRight entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.ImoveRight entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.ImoveRight entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.ImoveRight entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_groupBy__edge1(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			Dictionary<GRGEN_MODEL.Iright, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>> seenValues = new Dictionary<GRGEN_MODEL.Iright, List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge1)) {
					seenValues[list[pos].@edge__edge1].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> tempList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge1, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			foreach(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> Array_moveRightRule_keepOneForEachBy__edge1(List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list)
		{
			List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> newList = new List<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule>();
			Dictionary<GRGEN_MODEL.Iright, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.Iright, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge1)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge1, null);
				}
			}
			return newList;
		}
		public static int Array_moveRightRule_indexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.Iright entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_indexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.Iright entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.Iright entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_moveRightRule_lastIndexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_moveRightRule.IMatch_moveRightRule> list, GRGEN_MODEL.Iright entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
	}

	public class Rule_countZeros : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_countZeros instance = null;
		public static Rule_countZeros Instance { get { if(instance==null) { instance = new Rule_countZeros(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] countZeros_node_bp_AllowedTypes = null;
		public static bool[] countZeros_node_bp_IsAllowedType = null;
		public enum countZeros_NodeNums { @bp, };
		public enum countZeros_EdgeNums { };
		public enum countZeros_VariableNums { };
		public enum countZeros_SubNums { };
		public enum countZeros_AltNums { };
		public enum countZeros_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_countZeros;


		private Rule_countZeros()
			: base("countZeros",
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_countZeros+IMatch_countZeros",
				"de.unika.ipd.grGen.Action_Turing3.Rule_countZeros+Match_countZeros"
			)
		{
		}
		private void initialize()
		{
			bool[,] countZeros_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] countZeros_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] countZeros_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] countZeros_isEdgeTotallyHomomorphic = new bool[0];
			GRGEN_LGSP.PatternNode countZeros_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "countZeros_node_bp", "bp", countZeros_node_bp_AllowedTypes, countZeros_node_bp_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternCondition countZeros_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", true, "countZeros_node_bp", "value"), new GRGEN_EXPR.Constant("0")),
				new string[] { "countZeros_node_bp" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { countZeros_node_bp }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_countZeros = new GRGEN_LGSP.PatternGraph(
				"countZeros",
				"",
				null, "countZeros",
				false, false,
				new GRGEN_LGSP.PatternNode[] { countZeros_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { countZeros_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				countZeros_isNodeHomomorphicGlobal,
				countZeros_isEdgeHomomorphicGlobal,
				countZeros_isNodeTotallyHomomorphic,
				countZeros_isEdgeTotallyHomomorphic
			);

			countZeros_node_bp.pointOfDefinition = pat_countZeros;

			patternGraph = pat_countZeros;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_countZeros curMatch = (Match_countZeros)_curMatch;
			graph.SettingAddedNodeNames( countZeros_addedNodeNames );
			graph.SettingAddedEdgeNames( countZeros_addedEdgeNames );
			actionEnv.SelectedMatchRewritten();
			return;
		}
		private static string[] countZeros_addedNodeNames = new string[] {  };
		private static string[] countZeros_addedEdgeNames = new string[] {  };

		static Rule_countZeros() {
		}

		public interface IMatch_countZeros : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_countZeros : GRGEN_LGSP.MatchListElement<Match_countZeros>, IMatch_countZeros
		{
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum countZeros_NodeNums { @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)countZeros_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public enum countZeros_EdgeNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 0; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum countZeros_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum countZeros_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum countZeros_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum countZeros_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum countZeros_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_countZeros.instance.pat_countZeros; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_countZeros(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_countZeros(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_countZeros nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_countZeros cur = this;
				while(cur != null) {
					Match_countZeros next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_countZeros that)
			{
				_node_bp = that._node_bp;
			}

			public Match_countZeros(Match_countZeros that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_countZeros that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
			}

			public Match_countZeros(Match_countZeros that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_countZeros()
			{
			}

			public bool IsEqual(Match_countZeros that)
			{
				if(that==null) return false;
				if(_node_bp != that._node_bp) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_countZeros> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_countZeros match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>)
				return ((List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros instanceBearingAttributeForSearch_countZeros = new GRGEN_ACTIONS.Rule_countZeros.Match_countZeros();
		public static List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> Array_countZeros_groupBy_bp(List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> tempList = new List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> newList = new List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>();
			foreach(List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> Array_countZeros_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> list)
		{
			List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> newList = new List<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_countZeros_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_countZeros_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_countZeros_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_countZeros_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countZeros.IMatch_countZeros> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
	}

	public class Rule_countOnes : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_countOnes instance = null;
		public static Rule_countOnes Instance { get { if(instance==null) { instance = new Rule_countOnes(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] countOnes_node_bp_AllowedTypes = null;
		public static bool[] countOnes_node_bp_IsAllowedType = null;
		public enum countOnes_NodeNums { @bp, };
		public enum countOnes_EdgeNums { };
		public enum countOnes_VariableNums { };
		public enum countOnes_SubNums { };
		public enum countOnes_AltNums { };
		public enum countOnes_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_countOnes;


		private Rule_countOnes()
			: base("countOnes",
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { },
				"de.unika.ipd.grGen.Action_Turing3.Rule_countOnes+IMatch_countOnes",
				"de.unika.ipd.grGen.Action_Turing3.Rule_countOnes+Match_countOnes"
			)
		{
		}
		private void initialize()
		{
			bool[,] countOnes_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] countOnes_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] countOnes_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] countOnes_isEdgeTotallyHomomorphic = new bool[0];
			GRGEN_LGSP.PatternNode countOnes_node_bp = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@BandPosition, GRGEN_MODEL.NodeType_BandPosition.typeVar, "GRGEN_MODEL.IBandPosition", "countOnes_node_bp", "bp", countOnes_node_bp_AllowedTypes, countOnes_node_bp_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternCondition countOnes_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IBandPosition", true, "countOnes_node_bp", "value"), new GRGEN_EXPR.Constant("1")),
				new string[] { "countOnes_node_bp" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { countOnes_node_bp }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_countOnes = new GRGEN_LGSP.PatternGraph(
				"countOnes",
				"",
				null, "countOnes",
				false, false,
				new GRGEN_LGSP.PatternNode[] { countOnes_node_bp }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { countOnes_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				countOnes_isNodeHomomorphicGlobal,
				countOnes_isEdgeHomomorphicGlobal,
				countOnes_isNodeTotallyHomomorphic,
				countOnes_isEdgeTotallyHomomorphic
			);

			countOnes_node_bp.pointOfDefinition = pat_countOnes;

			patternGraph = pat_countOnes;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_countOnes curMatch = (Match_countOnes)_curMatch;
			graph.SettingAddedNodeNames( countOnes_addedNodeNames );
			graph.SettingAddedEdgeNames( countOnes_addedEdgeNames );
			actionEnv.SelectedMatchRewritten();
			return;
		}
		private static string[] countOnes_addedNodeNames = new string[] {  };
		private static string[] countOnes_addedEdgeNames = new string[] {  };

		static Rule_countOnes() {
		}

		public interface IMatch_countOnes : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IBandPosition node_bp { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_countOnes : GRGEN_LGSP.MatchListElement<Match_countOnes>, IMatch_countOnes
		{
			public GRGEN_MODEL.IBandPosition node_bp { get { return (GRGEN_MODEL.IBandPosition)_node_bp; } set { _node_bp = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_bp;
			public enum countOnes_NodeNums { @bp, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)countOnes_NodeNums.@bp: return _node_bp;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "bp": return _node_bp;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "bp": _node_bp = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public enum countOnes_EdgeNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 0; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetEdge(string name, GRGEN_LIBGR.IEdge value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum countOnes_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0; } }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			public override void SetVariable(string name, object value)
			{
				switch(name) {
				default: break;
				}
			}

			public enum countOnes_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum countOnes_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum countOnes_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0; } }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum countOnes_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0; } }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_countOnes.instance.pat_countOnes; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_countOnes(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_countOnes(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_countOnes nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_countOnes cur = this;
				while(cur != null) {
					Match_countOnes next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_countOnes that)
			{
				_node_bp = that._node_bp;
			}

			public Match_countOnes(Match_countOnes that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_countOnes that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_bp = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_bp];
			}

			public Match_countOnes(Match_countOnes that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_countOnes()
			{
			}

			public bool IsEqual(Match_countOnes that)
			{
				if(that==null) return false;
				if(_node_bp != that._node_bp) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IBandPosition> Extract_bp(List<IMatch_countOnes> matchList)
			{
				List<GRGEN_MODEL.IBandPosition> resultList = new List<GRGEN_MODEL.IBandPosition>(matchList.Count);
				foreach(IMatch_countOnes match in matchList)
					resultList.Add(match.node_bp);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>)
				return ((List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes instanceBearingAttributeForSearch_countOnes = new GRGEN_ACTIONS.Rule_countOnes.Match_countOnes();
		public static List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> Array_countOnes_groupBy_bp(List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> list)
		{
			Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>> seenValues = new Dictionary<GRGEN_MODEL.IBandPosition, List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_bp)) {
					seenValues[list[pos].@node_bp].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> tempList = new List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_bp, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> newList = new List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>();
			foreach(List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> Array_countOnes_keepOneForEachBy_bp(List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> list)
		{
			List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> newList = new List<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes>();
			Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IBandPosition, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_bp)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_bp, null);
				}
			}
			return newList;
		}
		public static int Array_countOnes_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_countOnes_indexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_countOnes_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> list, GRGEN_MODEL.IBandPosition entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_countOnes_lastIndexOfBy_bp(IList<GRGEN_ACTIONS.Rule_countOnes.IMatch_countOnes> list, GRGEN_MODEL.IBandPosition entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_bp.Equals(entry))
					return i;
			return -1;
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

	public partial class MatchClassFilters
	{

		static MatchClassFilters() {
		}

	}



	//-----------------------------------------------------------

	public class Turing3_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public Turing3_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[8];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+8];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			functions = new GRGEN_LIBGR.FunctionInfo[0+0];
			procedures = new GRGEN_LIBGR.ProcedureInfo[0+0];
			matchClasses = new GRGEN_LIBGR.MatchClassInfo[0];
			packages = new string[0];
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
		public override GRGEN_LIBGR.DefinedSequenceInfo[] DefinedSequences { get { return definedSequences; } }
		private GRGEN_LIBGR.DefinedSequenceInfo[] definedSequences;
		public override GRGEN_LIBGR.FunctionInfo[] Functions { get { return functions; } }
		private GRGEN_LIBGR.FunctionInfo[] functions;
		public override GRGEN_LIBGR.ProcedureInfo[] Procedures { get { return procedures; } }
		private GRGEN_LIBGR.ProcedureInfo[] procedures;
		public override GRGEN_LIBGR.MatchClassInfo[] MatchClasses { get { return matchClasses; } }
		private GRGEN_LIBGR.MatchClassInfo[] matchClasses;
		public override string[] Packages { get { return packages; } }
		private string[] packages;
	}


    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_readZeroRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_readZeroRule.IMatch_readZeroRule match, out GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches, List<GRGEN_MODEL.IWriteValue> output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, List<GRGEN_MODEL.IWriteValue> output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
    }
    
    public class Action_readZeroRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_readZeroRule
    {
        public Action_readZeroRule()
            : base(Rule_readZeroRule.Instance.patternGraph)
        {
            _rulePattern = Rule_readZeroRule.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_readZeroRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "readZeroRule"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_readZeroRule.Match_readZeroRule, Rule_readZeroRule.IMatch_readZeroRule> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_readZeroRule Instance { get { return instance; } set { instance = value; } }
        private static Action_readZeroRule instance = new Action_readZeroRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_readZeroRule.Match_readZeroRule, Rule_readZeroRule.IMatch_readZeroRule>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[1];
            int isoSpace = 0;
            // Preset readZeroRule_node_s 
            GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_s = (GRGEN_LGSP.LGSPNode)readZeroRule_node_s;
            if(candidate_readZeroRule_node_s.lgspType.TypeID!=2) {
                return matches;
            }
            // Preset readZeroRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_readZeroRule_node_bp = (GRGEN_LGSP.LGSPNode)readZeroRule_node_bp;
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IWriteValue> output_list_0 = new List<GRGEN_MODEL.IWriteValue>();
        public GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, readZeroRule_node_s, readZeroRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_readZeroRule.IMatch_readZeroRule match, out GRGEN_MODEL.IWriteValue output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches, List<GRGEN_MODEL.IWriteValue> output_0)
        {
            foreach(Rule_readZeroRule.IMatch_readZeroRule match in matches)
            {
                GRGEN_MODEL.IWriteValue output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readZeroRule_node_s, readZeroRule_node_bp);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp, List<GRGEN_MODEL.IWriteValue> output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, readZeroRule_node_s, readZeroRule_node_bp);
            if(matches.Count <= 0) return 0;
            foreach(Rule_readZeroRule.IMatch_readZeroRule match in matches)
            {
                GRGEN_MODEL.IWriteValue output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readZeroRule_node_s, readZeroRule_node_bp);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readZeroRule_node_s, readZeroRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IWriteValue output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readZeroRule_node_s, readZeroRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IState readZeroRule_node_s, GRGEN_MODEL.IBandPosition readZeroRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readZeroRule_node_s, readZeroRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IWriteValue output_0; 
            Modify(actionEnv, (Rule_readZeroRule.IMatch_readZeroRule)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_readZeroRule.IMatch_readZeroRule>)matches, output_list_0);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IWriteValue output_0 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[1]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_readOneRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_readOneRule.IMatch_readOneRule match, out GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches, List<GRGEN_MODEL.IWriteValue> output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, List<GRGEN_MODEL.IWriteValue> output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
    }
    
    public class Action_readOneRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_readOneRule
    {
        public Action_readOneRule()
            : base(Rule_readOneRule.Instance.patternGraph)
        {
            _rulePattern = Rule_readOneRule.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_readOneRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "readOneRule"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_readOneRule.Match_readOneRule, Rule_readOneRule.IMatch_readOneRule> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_readOneRule Instance { get { return instance; } set { instance = value; } }
        private static Action_readOneRule instance = new Action_readOneRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_readOneRule.Match_readOneRule, Rule_readOneRule.IMatch_readOneRule>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[1];
            int isoSpace = 0;
            // Preset readOneRule_node_s 
            GRGEN_LGSP.LGSPNode candidate_readOneRule_node_s = (GRGEN_LGSP.LGSPNode)readOneRule_node_s;
            if(candidate_readOneRule_node_s.lgspType.TypeID!=2) {
                return matches;
            }
            // Preset readOneRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_readOneRule_node_bp = (GRGEN_LGSP.LGSPNode)readOneRule_node_bp;
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IWriteValue> output_list_0 = new List<GRGEN_MODEL.IWriteValue>();
        public GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, readOneRule_node_s, readOneRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_readOneRule.IMatch_readOneRule match, out GRGEN_MODEL.IWriteValue output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches, List<GRGEN_MODEL.IWriteValue> output_0)
        {
            foreach(Rule_readOneRule.IMatch_readOneRule match in matches)
            {
                GRGEN_MODEL.IWriteValue output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, ref GRGEN_MODEL.IWriteValue output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readOneRule_node_s, readOneRule_node_bp);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp, List<GRGEN_MODEL.IWriteValue> output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, readOneRule_node_s, readOneRule_node_bp);
            if(matches.Count <= 0) return 0;
            foreach(Rule_readOneRule.IMatch_readOneRule match in matches)
            {
                GRGEN_MODEL.IWriteValue output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readOneRule_node_s, readOneRule_node_bp);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readOneRule_node_s, readOneRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IWriteValue output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readOneRule_node_s, readOneRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IState readOneRule_node_s, GRGEN_MODEL.IBandPosition readOneRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule> matches;
            GRGEN_MODEL.IWriteValue output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, readOneRule_node_s, readOneRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IWriteValue output_0; 
            Modify(actionEnv, (Rule_readOneRule.IMatch_readOneRule)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_readOneRule.IMatch_readOneRule>)matches, output_list_0);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IWriteValue output_0 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[1]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IState) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_ensureMoveLeftValidRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
    }
    
    public class Action_ensureMoveLeftValidRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ensureMoveLeftValidRule
    {
        public Action_ensureMoveLeftValidRule()
            : base(Rule_ensureMoveLeftValidRule.Instance.patternGraph)
        {
            _rulePattern = Rule_ensureMoveLeftValidRule.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_ensureMoveLeftValidRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ensureMoveLeftValidRule"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_ensureMoveLeftValidRule Instance { get { return instance; } set { instance = value; } }
        private static Action_ensureMoveLeftValidRule instance = new Action_ensureMoveLeftValidRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveLeftValidRule.Match_ensureMoveLeftValidRule, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[0];
            int isoSpace = 0;
            // Preset ensureMoveLeftValidRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_wv = (GRGEN_LGSP.LGSPNode)ensureMoveLeftValidRule_node_wv;
            if(candidate_ensureMoveLeftValidRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset ensureMoveLeftValidRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveLeftValidRule_node_bp = (GRGEN_LGSP.LGSPNode)ensureMoveLeftValidRule_node_bp;
            if(candidate_ensureMoveLeftValidRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            // NegativePattern 
            {
                ++isoSpace;
                uint prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_ensureMoveLeftValidRule_node_bp.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
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
                        if((candidate_ensureMoveLeftValidRule_neg_0_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        // negative pattern found
                        candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                        --isoSpace;
                        return matches;
                    }
                    while( (candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 = candidate_ensureMoveLeftValidRule_neg_0_edge__edge0.lgspInNext) != head_candidate_ensureMoveLeftValidRule_neg_0_edge__edge0 );
                }
                candidate_ensureMoveLeftValidRule_node_bp.lgspFlags = candidate_ensureMoveLeftValidRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_neg_0__candidate_ensureMoveLeftValidRule_node_bp;
                --isoSpace;
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches)
        {
            foreach(Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            if(matches.Count <= 0) return 0;
            foreach(Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveLeftValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveLeftValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveLeftValidRule_node_wv, ensureMoveLeftValidRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveLeftValidRule.IMatch_ensureMoveLeftValidRule>)matches);
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
            
            if(Apply(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[0]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
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
            return ApplyStar(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_ensureMoveRightValidRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
    }
    
    public class Action_ensureMoveRightValidRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ensureMoveRightValidRule
    {
        public Action_ensureMoveRightValidRule()
            : base(Rule_ensureMoveRightValidRule.Instance.patternGraph)
        {
            _rulePattern = Rule_ensureMoveRightValidRule.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_ensureMoveRightValidRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ensureMoveRightValidRule"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_ensureMoveRightValidRule Instance { get { return instance; } set { instance = value; } }
        private static Action_ensureMoveRightValidRule instance = new Action_ensureMoveRightValidRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ensureMoveRightValidRule.Match_ensureMoveRightValidRule, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[0];
            int isoSpace = 0;
            // Preset ensureMoveRightValidRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_wv = (GRGEN_LGSP.LGSPNode)ensureMoveRightValidRule_node_wv;
            if(candidate_ensureMoveRightValidRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset ensureMoveRightValidRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_ensureMoveRightValidRule_node_bp = (GRGEN_LGSP.LGSPNode)ensureMoveRightValidRule_node_bp;
            if(candidate_ensureMoveRightValidRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            // NegativePattern 
            {
                ++isoSpace;
                uint prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                prev_neg_0__candidate_ensureMoveRightValidRule_node_bp = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_ensureMoveRightValidRule_node_bp.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
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
                        if((candidate_ensureMoveRightValidRule_neg_0_node__node0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        // negative pattern found
                        candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                        --isoSpace;
                        return matches;
                    }
                    while( (candidate_ensureMoveRightValidRule_neg_0_edge__edge0 = candidate_ensureMoveRightValidRule_neg_0_edge__edge0.lgspOutNext) != head_candidate_ensureMoveRightValidRule_neg_0_edge__edge0 );
                }
                candidate_ensureMoveRightValidRule_node_bp.lgspFlags = candidate_ensureMoveRightValidRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev_neg_0__candidate_ensureMoveRightValidRule_node_bp;
                --isoSpace;
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches)
        {
            foreach(Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            if(matches.Count <= 0) return 0;
            foreach(Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue ensureMoveRightValidRule_node_wv, GRGEN_MODEL.IBandPosition ensureMoveRightValidRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ensureMoveRightValidRule_node_wv, ensureMoveRightValidRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_ensureMoveRightValidRule.IMatch_ensureMoveRightValidRule>)matches);
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
            
            if(Apply(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1])) {
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[0]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
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
            return ApplyStar(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_moveLeftRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_moveLeftRule.IMatch_moveLeftRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
    }
    
    public class Action_moveLeftRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_moveLeftRule
    {
        public Action_moveLeftRule()
            : base(Rule_moveLeftRule.Instance.patternGraph)
        {
            _rulePattern = Rule_moveLeftRule.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_moveLeftRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "moveLeftRule"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_moveLeftRule.Match_moveLeftRule, Rule_moveLeftRule.IMatch_moveLeftRule> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_moveLeftRule Instance { get { return instance; } set { instance = value; } }
        private static Action_moveLeftRule instance = new Action_moveLeftRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_moveLeftRule.Match_moveLeftRule, Rule_moveLeftRule.IMatch_moveLeftRule>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[2];
            int isoSpace = 0;
            // Preset moveLeftRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_wv = (GRGEN_LGSP.LGSPNode)moveLeftRule_node_wv;
            if(candidate_moveLeftRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset moveLeftRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_moveLeftRule_node_bp = (GRGEN_LGSP.LGSPNode)moveLeftRule_node_bp;
            if(candidate_moveLeftRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            uint prev__candidate_moveLeftRule_node_bp;
            prev__candidate_moveLeftRule_node_bp = candidate_moveLeftRule_node_bp.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_moveLeftRule_node_bp.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
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
                            if((candidate_moveLeftRule_node_lbp.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
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
                                candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_moveLeftRule_node_bp;
                                return matches;
                            }
                        }
                        while( (candidate_moveLeftRule_edge__edge1 = candidate_moveLeftRule_edge__edge1.lgspInNext) != head_candidate_moveLeftRule_edge__edge1 );
                    }
                }
                while( (candidate_moveLeftRule_edge__edge0 = candidate_moveLeftRule_edge__edge0.lgspOutNext) != head_candidate_moveLeftRule_edge__edge0 );
            }
            candidate_moveLeftRule_node_bp.lgspFlags = candidate_moveLeftRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_moveLeftRule_node_bp;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IState> output_list_0 = new List<GRGEN_MODEL.IState>();
        List<GRGEN_MODEL.IBandPosition> output_list_1 = new List<GRGEN_MODEL.IBandPosition>();
        public GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, moveLeftRule_node_wv, moveLeftRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_moveLeftRule.IMatch_moveLeftRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1)
        {
            foreach(Rule_moveLeftRule.IMatch_moveLeftRule match in matches)
            {
                GRGEN_MODEL.IState output_local_0; GRGEN_MODEL.IBandPosition output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, moveLeftRule_node_wv, moveLeftRule_node_bp);
            if(matches.Count <= 0) return 0;
            foreach(Rule_moveLeftRule.IMatch_moveLeftRule match in matches)
            {
                GRGEN_MODEL.IState output_local_0; GRGEN_MODEL.IBandPosition output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue moveLeftRule_node_wv, GRGEN_MODEL.IBandPosition moveLeftRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveLeftRule_node_wv, moveLeftRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            Modify(actionEnv, (Rule_moveLeftRule.IMatch_moveLeftRule)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_moveLeftRule.IMatch_moveLeftRule>)matches, output_list_0, output_list_1);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IState output_0 = null; GRGEN_MODEL.IBandPosition output_1 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[2]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], output_list_0, output_list_1);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_moveRightRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_moveRightRule.IMatch_moveRightRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
    }
    
    public class Action_moveRightRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_moveRightRule
    {
        public Action_moveRightRule()
            : base(Rule_moveRightRule.Instance.patternGraph)
        {
            _rulePattern = Rule_moveRightRule.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_moveRightRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "moveRightRule"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_moveRightRule.Match_moveRightRule, Rule_moveRightRule.IMatch_moveRightRule> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_moveRightRule Instance { get { return instance; } set { instance = value; } }
        private static Action_moveRightRule instance = new Action_moveRightRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_moveRightRule.Match_moveRightRule, Rule_moveRightRule.IMatch_moveRightRule>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[2];
            int isoSpace = 0;
            // Preset moveRightRule_node_wv 
            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_wv = (GRGEN_LGSP.LGSPNode)moveRightRule_node_wv;
            if(candidate_moveRightRule_node_wv.lgspType.TypeID!=3) {
                return matches;
            }
            // Preset moveRightRule_node_bp 
            GRGEN_LGSP.LGSPNode candidate_moveRightRule_node_bp = (GRGEN_LGSP.LGSPNode)moveRightRule_node_bp;
            if(candidate_moveRightRule_node_bp.lgspType.TypeID!=1) {
                return matches;
            }
            uint prev__candidate_moveRightRule_node_bp;
            prev__candidate_moveRightRule_node_bp = candidate_moveRightRule_node_bp.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_moveRightRule_node_bp.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
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
                            if((candidate_moveRightRule_node_rbp.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
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
                                candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_moveRightRule_node_bp;
                                return matches;
                            }
                        }
                        while( (candidate_moveRightRule_edge__edge1 = candidate_moveRightRule_edge__edge1.lgspOutNext) != head_candidate_moveRightRule_edge__edge1 );
                    }
                }
                while( (candidate_moveRightRule_edge__edge0 = candidate_moveRightRule_edge__edge0.lgspOutNext) != head_candidate_moveRightRule_edge__edge0 );
            }
            candidate_moveRightRule_node_bp.lgspFlags = candidate_moveRightRule_node_bp.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_moveRightRule_node_bp;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IState> output_list_0 = new List<GRGEN_MODEL.IState>();
        List<GRGEN_MODEL.IBandPosition> output_list_1 = new List<GRGEN_MODEL.IBandPosition>();
        public GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, moveRightRule_node_wv, moveRightRule_node_bp);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_moveRightRule.IMatch_moveRightRule match, out GRGEN_MODEL.IState output_0, out GRGEN_MODEL.IBandPosition output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1)
        {
            foreach(Rule_moveRightRule.IMatch_moveRightRule match in matches)
            {
                GRGEN_MODEL.IState output_local_0; GRGEN_MODEL.IBandPosition output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, ref GRGEN_MODEL.IState output_0, ref GRGEN_MODEL.IBandPosition output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveRightRule_node_wv, moveRightRule_node_bp);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp, List<GRGEN_MODEL.IState> output_0, List<GRGEN_MODEL.IBandPosition> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, moveRightRule_node_wv, moveRightRule_node_bp);
            if(matches.Count <= 0) return 0;
            foreach(Rule_moveRightRule.IMatch_moveRightRule match in matches)
            {
                GRGEN_MODEL.IState output_local_0; GRGEN_MODEL.IBandPosition output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveRightRule_node_wv, moveRightRule_node_bp);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveRightRule_node_wv, moveRightRule_node_bp);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveRightRule_node_wv, moveRightRule_node_bp);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IWriteValue moveRightRule_node_wv, GRGEN_MODEL.IBandPosition moveRightRule_node_bp)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule> matches;
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, moveRightRule_node_wv, moveRightRule_node_bp);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IState output_0; GRGEN_MODEL.IBandPosition output_1; 
            Modify(actionEnv, (Rule_moveRightRule.IMatch_moveRightRule)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_moveRightRule.IMatch_moveRightRule>)matches, output_list_0, output_list_1);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IState output_0 = null; GRGEN_MODEL.IBandPosition output_1 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[2]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1], output_list_0, output_list_1);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IWriteValue) parameters[0], (GRGEN_MODEL.IBandPosition) parameters[1]);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_countZeros
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_countZeros.IMatch_countZeros match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches);
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
    
    public class Action_countZeros : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_countZeros
    {
        public Action_countZeros()
            : base(Rule_countZeros.Instance.patternGraph)
        {
            _rulePattern = Rule_countZeros.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_countZeros _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "countZeros"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_countZeros.Match_countZeros, Rule_countZeros.IMatch_countZeros> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_countZeros Instance { get { return instance; } set { instance = value; } }
        private static Action_countZeros instance = new Action_countZeros();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_countZeros.Match_countZeros, Rule_countZeros.IMatch_countZeros>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[0];
            int isoSpace = 0;
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_countZeros.IMatch_countZeros match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches)
        {
            foreach(Rule_countZeros.IMatch_countZeros match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_countZeros.IMatch_countZeros match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros> matches;
            
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
            
            Modify(actionEnv, (Rule_countZeros.IMatch_countZeros)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_countZeros.IMatch_countZeros>)matches);
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
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[0]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_countOnes
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_countOnes.IMatch_countOnes match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches);
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
    
    public class Action_countOnes : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_countOnes
    {
        public Action_countOnes()
            : base(Rule_countOnes.Instance.patternGraph)
        {
            _rulePattern = Rule_countOnes.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_countOnes _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "countOnes"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_countOnes.Match_countOnes, Rule_countOnes.IMatch_countOnes> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_countOnes Instance { get { return instance; } set { instance = value; } }
        private static Action_countOnes instance = new Action_countOnes();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_countOnes.Match_countOnes, Rule_countOnes.IMatch_countOnes>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[0];
            int isoSpace = 0;
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_countOnes.IMatch_countOnes match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches)
        {
            foreach(Rule_countOnes.IMatch_countOnes match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_countOnes.IMatch_countOnes match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes> matches;
            
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
            
            Modify(actionEnv, (Rule_countOnes.IMatch_countOnes)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_countOnes.IMatch_countOnes>)matches);
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
            if(AvailableReturnArrays == null)
                AvailableReturnArrays = new List<object[]>();
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[0]);
            if(ReturnArrayListForAll == null)
                ReturnArrayListForAll = new List<object[]>();
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
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)
        {
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
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
            packages = new string[0];
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_readZeroRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_readZeroRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_readZeroRule.Instance);
            actions.Add("readZeroRule", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_readZeroRule.Instance);
            @readZeroRule = GRGEN_ACTIONS.Action_readZeroRule.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_readOneRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_readOneRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_readOneRule.Instance);
            actions.Add("readOneRule", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_readOneRule.Instance);
            @readOneRule = GRGEN_ACTIONS.Action_readOneRule.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Instance);
            actions.Add("ensureMoveLeftValidRule", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_ensureMoveLeftValidRule.Instance);
            @ensureMoveLeftValidRule = GRGEN_ACTIONS.Action_ensureMoveLeftValidRule.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Instance);
            actions.Add("ensureMoveRightValidRule", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_ensureMoveRightValidRule.Instance);
            @ensureMoveRightValidRule = GRGEN_ACTIONS.Action_ensureMoveRightValidRule.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_moveLeftRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_moveLeftRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_moveLeftRule.Instance);
            actions.Add("moveLeftRule", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_moveLeftRule.Instance);
            @moveLeftRule = GRGEN_ACTIONS.Action_moveLeftRule.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_moveRightRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_moveRightRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_moveRightRule.Instance);
            actions.Add("moveRightRule", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_moveRightRule.Instance);
            @moveRightRule = GRGEN_ACTIONS.Action_moveRightRule.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_countZeros.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_countZeros.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_countZeros.Instance);
            actions.Add("countZeros", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_countZeros.Instance);
            @countZeros = GRGEN_ACTIONS.Action_countZeros.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_countOnes.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_countOnes.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_countOnes.Instance);
            actions.Add("countOnes", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_countOnes.Instance);
            @countOnes = GRGEN_ACTIONS.Action_countOnes.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_readZeroRule.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_readOneRule.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_moveLeftRule.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_moveRightRule.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_countZeros.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_countOnes.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_readZeroRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_readOneRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_moveLeftRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_moveRightRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_countZeros.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_countOnes.Instance.patternGraph);
            GRGEN_ACTIONS.Rule_readZeroRule.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_readOneRule.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_moveLeftRule.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_moveRightRule.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_countZeros.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_countOnes.Instance.patternGraph.maxIsoSpace = 0;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_readZeroRule.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_readOneRule.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_moveLeftRule.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_moveRightRule.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_countZeros.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_countOnes.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
        }
        
        public GRGEN_ACTIONS.IAction_readZeroRule @readZeroRule;
        public GRGEN_ACTIONS.IAction_readOneRule @readOneRule;
        public GRGEN_ACTIONS.IAction_ensureMoveLeftValidRule @ensureMoveLeftValidRule;
        public GRGEN_ACTIONS.IAction_ensureMoveRightValidRule @ensureMoveRightValidRule;
        public GRGEN_ACTIONS.IAction_moveLeftRule @moveLeftRule;
        public GRGEN_ACTIONS.IAction_moveRightRule @moveRightRule;
        public GRGEN_ACTIONS.IAction_countZeros @countZeros;
        public GRGEN_ACTIONS.IAction_countOnes @countOnes;
        
        
        public override string[] Packages { get { return packages; } }
        private string[] packages;
        
        public override string Name { get { return "Turing3Actions"; } }
        public override string StatisticsPath { get { return null; } }
        public override bool LazyNIC { get { return false; } }
        public override bool InlineIndependents { get { return true; } }
        public override bool Profile { get { return false; } }

        public override IList ArrayOrderAscendingBy(IList array, string member)
        {
            if(array.Count == 0)
                return array;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return null;
            if(array[0] == null)
                return null;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return null;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "countZeros":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "countOnes":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                default:
                    return null;
                }
            }
        }

        public override IList ArrayOrderDescendingBy(IList array, string member)
        {
            if(array.Count == 0)
                return array;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return null;
            if(array[0] == null)
                return null;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return null;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "countZeros":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                case "countOnes":
                    switch(member)
                    {
                    default:
                        return null;
                    }
                default:
                    return null;
                }
            }
        }

        public override IList ArrayGroupBy(IList array, string member)
        {
            if(array.Count == 0)
                return array;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return null;
            if(array[0] == null)
                return null;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return null;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readZeroRule_groupBy_s(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    case "wv":
                        return ArrayHelper.Array_readZeroRule_groupBy_wv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_readZeroRule_groupBy_bp(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    case "rv":
                        return ArrayHelper.Array_readZeroRule_groupBy_rv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readOneRule_groupBy_s(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    case "wv":
                        return ArrayHelper.Array_readOneRule_groupBy_wv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_readOneRule_groupBy_bp(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    case "rv":
                        return ArrayHelper.Array_readOneRule_groupBy_rv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_groupBy_wv(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_groupBy__node0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_groupBy_bp(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_groupBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveRightValidRule_groupBy_wv(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_groupBy__node0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_ensureMoveRightValidRule_groupBy_bp(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_groupBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveLeftRule_groupBy_wv(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "s":
                        return ArrayHelper.Array_moveLeftRule_groupBy_s(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "lbp":
                        return ArrayHelper.Array_moveLeftRule_groupBy_lbp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_moveLeftRule_groupBy_bp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_moveLeftRule_groupBy__edge0(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "_edge1":
                        return ArrayHelper.Array_moveLeftRule_groupBy__edge1(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveRightRule_groupBy_wv(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "s":
                        return ArrayHelper.Array_moveRightRule_groupBy_s(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_moveRightRule_groupBy_bp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "rbp":
                        return ArrayHelper.Array_moveRightRule_groupBy_rbp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_moveRightRule_groupBy__edge0(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "_edge1":
                        return ArrayHelper.Array_moveRightRule_groupBy__edge1(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "countZeros":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countZeros_groupBy_bp(GRGEN_ACTIONS.Rule_countZeros.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "countOnes":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countOnes_groupBy_bp(GRGEN_ACTIONS.Rule_countOnes.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                default:
                    return null;
                }
            }
        }

        public override IList ArrayKeepOneForEach(IList array, string member)
        {
            if(array.Count == 0)
                return array;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return null;
            if(array[0] == null)
                return null;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return null;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readZeroRule_keepOneForEachBy_s(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    case "wv":
                        return ArrayHelper.Array_readZeroRule_keepOneForEachBy_wv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_readZeroRule_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    case "rv":
                        return ArrayHelper.Array_readZeroRule_keepOneForEachBy_rv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readOneRule_keepOneForEachBy_s(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    case "wv":
                        return ArrayHelper.Array_readOneRule_keepOneForEachBy_wv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_readOneRule_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    case "rv":
                        return ArrayHelper.Array_readOneRule_keepOneForEachBy_rv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_keepOneForEachBy_wv(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_keepOneForEachBy__node0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_keepOneForEachBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveRightValidRule_keepOneForEachBy_wv(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_keepOneForEachBy__node0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_ensureMoveRightValidRule_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_keepOneForEachBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveLeftRule_keepOneForEachBy_wv(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "s":
                        return ArrayHelper.Array_moveLeftRule_keepOneForEachBy_s(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "lbp":
                        return ArrayHelper.Array_moveLeftRule_keepOneForEachBy_lbp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_moveLeftRule_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_moveLeftRule_keepOneForEachBy__edge0(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    case "_edge1":
                        return ArrayHelper.Array_moveLeftRule_keepOneForEachBy__edge1(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveRightRule_keepOneForEachBy_wv(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "s":
                        return ArrayHelper.Array_moveRightRule_keepOneForEachBy_s(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "bp":
                        return ArrayHelper.Array_moveRightRule_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "rbp":
                        return ArrayHelper.Array_moveRightRule_keepOneForEachBy_rbp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_moveRightRule_keepOneForEachBy__edge0(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    case "_edge1":
                        return ArrayHelper.Array_moveRightRule_keepOneForEachBy__edge1(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "countZeros":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countZeros_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_countZeros.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                case "countOnes":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countOnes_keepOneForEachBy_bp(GRGEN_ACTIONS.Rule_countOnes.ConvertAsNeeded(array));
                    default:
                        return null;
                    }
                default:
                    return null;
                }
            }
        }

        public override int ArrayIndexOfBy(IList array, string member, object value)
        {
            if(array.Count == 0)
                return -1;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return -1;
            if(array[0] == null)
                return -1;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return -1;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_s(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "wv":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "bp":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "rv":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_rv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadZero)value);
                    default:
                        return -1;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readOneRule_indexOfBy_s(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "wv":
                        return ArrayHelper.Array_readOneRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "bp":
                        return ArrayHelper.Array_readOneRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "rv":
                        return ArrayHelper.Array_readOneRule_indexOfBy_rv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadOne)value);
                    default:
                        return -1;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value);
                    default:
                        return -1;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value);
                    default:
                        return -1;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "s":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_s(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "lbp":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_lbp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "bp":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value);
                    case "_edge1":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy__edge1(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value);
                    default:
                        return -1;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "s":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_s(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "bp":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "rbp":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_rbp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_moveRightRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value);
                    case "_edge1":
                        return ArrayHelper.Array_moveRightRule_indexOfBy__edge1(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value);
                    default:
                        return -1;
                    }
                case "countZeros":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countZeros_indexOfBy_bp(GRGEN_ACTIONS.Rule_countZeros.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    default:
                        return -1;
                    }
                case "countOnes":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countOnes_indexOfBy_bp(GRGEN_ACTIONS.Rule_countOnes.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    default:
                        return -1;
                    }
                default:
                    return -1;
                }
            }
        }

        public override int ArrayIndexOfBy(IList array, string member, object value, int startIndex)
        {
            if(array.Count == 0)
                return -1;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return -1;
            if(array[0] == null)
                return -1;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return -1;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_s(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "wv":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "rv":
                        return ArrayHelper.Array_readZeroRule_indexOfBy_rv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadZero)value, startIndex);
                    default:
                        return -1;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readOneRule_indexOfBy_s(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "wv":
                        return ArrayHelper.Array_readOneRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_readOneRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "rv":
                        return ArrayHelper.Array_readOneRule_indexOfBy_rv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadOne)value, startIndex);
                    default:
                        return -1;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value, startIndex);
                    default:
                        return -1;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value, startIndex);
                    default:
                        return -1;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "s":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_s(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "lbp":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_lbp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value, startIndex);
                    case "_edge1":
                        return ArrayHelper.Array_moveLeftRule_indexOfBy__edge1(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value, startIndex);
                    default:
                        return -1;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_wv(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "s":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_s(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_bp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "rbp":
                        return ArrayHelper.Array_moveRightRule_indexOfBy_rbp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_moveRightRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value, startIndex);
                    case "_edge1":
                        return ArrayHelper.Array_moveRightRule_indexOfBy__edge1(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value, startIndex);
                    default:
                        return -1;
                    }
                case "countZeros":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countZeros_indexOfBy_bp(GRGEN_ACTIONS.Rule_countZeros.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    default:
                        return -1;
                    }
                case "countOnes":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countOnes_indexOfBy_bp(GRGEN_ACTIONS.Rule_countOnes.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    default:
                        return -1;
                    }
                default:
                    return -1;
                }
            }
        }

        public override int ArrayLastIndexOfBy(IList array, string member, object value)
        {
            if(array.Count == 0)
                return -1;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return -1;
            if(array[0] == null)
                return -1;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return -1;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "wv":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "bp":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "rv":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_rv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadZero)value);
                    default:
                        return -1;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "wv":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "bp":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "rv":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_rv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadOne)value);
                    default:
                        return -1;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value);
                    default:
                        return -1;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value);
                    default:
                        return -1;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "s":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "lbp":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_lbp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "bp":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value);
                    case "_edge1":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy__edge1(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value);
                    default:
                        return -1;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value);
                    case "s":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value);
                    case "bp":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "rbp":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_rbp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    case "_edge0":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value);
                    case "_edge1":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy__edge1(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value);
                    default:
                        return -1;
                    }
                case "countZeros":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countZeros_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_countZeros.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    default:
                        return -1;
                    }
                case "countOnes":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countOnes_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_countOnes.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value);
                    default:
                        return -1;
                    }
                default:
                    return -1;
                }
            }
        }

        public override int ArrayLastIndexOfBy(IList array, string member, object value, int startIndex)
        {
            if(array.Count == 0)
                return -1;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return -1;
            if(array[0] == null)
                return -1;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return -1;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "wv":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "rv":
                        return ArrayHelper.Array_readZeroRule_lastIndexOfBy_rv(GRGEN_ACTIONS.Rule_readZeroRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadZero)value, startIndex);
                    default:
                        return -1;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    case "s":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "wv":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "rv":
                        return ArrayHelper.Array_readOneRule_lastIndexOfBy_rv(GRGEN_ACTIONS.Rule_readOneRule.ConvertAsNeeded(array), (GRGEN_MODEL.IreadOne)value, startIndex);
                    default:
                        return -1;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveLeftValidRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveLeftValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value, startIndex);
                    default:
                        return -1;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "_node0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy__node0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_ensureMoveRightValidRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_ensureMoveRightValidRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value, startIndex);
                    default:
                        return -1;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "s":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "lbp":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_lbp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveLeft)value, startIndex);
                    case "_edge1":
                        return ArrayHelper.Array_moveLeftRule_lastIndexOfBy__edge1(GRGEN_ACTIONS.Rule_moveLeftRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value, startIndex);
                    default:
                        return -1;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    case "wv":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_wv(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IWriteValue)value, startIndex);
                    case "s":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_s(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IState)value, startIndex);
                    case "bp":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "rbp":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy_rbp(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.ImoveRight)value, startIndex);
                    case "_edge1":
                        return ArrayHelper.Array_moveRightRule_lastIndexOfBy__edge1(GRGEN_ACTIONS.Rule_moveRightRule.ConvertAsNeeded(array), (GRGEN_MODEL.Iright)value, startIndex);
                    default:
                        return -1;
                    }
                case "countZeros":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countZeros_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_countZeros.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    default:
                        return -1;
                    }
                case "countOnes":
                    switch(member)
                    {
                    case "bp":
                        return ArrayHelper.Array_countOnes_lastIndexOfBy_bp(GRGEN_ACTIONS.Rule_countOnes.ConvertAsNeeded(array), (GRGEN_MODEL.IBandPosition)value, startIndex);
                    default:
                        return -1;
                    }
                default:
                    return -1;
                }
            }
        }

        public override int ArrayIndexOfOrderedBy(IList array, string member, object value)
        {
            if(array.Count == 0)
                return -1;
            string arrayType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array.GetType());
            string arrayValueType = GRGEN_LIBGR.TypesHelper.ExtractSrc(arrayType);
            if(!arrayValueType.StartsWith("match<"))
                return -1;
            if(array[0] == null)
                return -1;
            if(arrayValueType == "match<>")
                arrayValueType = GRGEN_LIBGR.TypesHelper.DotNetTypeToXgrsType(array[0].GetType());
            if(arrayValueType.StartsWith("match<class "))
            {
                switch(arrayValueType.Substring(12, arrayValueType.Length - 12 - 1))
                {
                default:
                    return -1;
                }
            }
            else
            {
                switch(arrayValueType.Substring(6, arrayValueType.Length - 6 - 1))
                {
                case "readZeroRule":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                case "readOneRule":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                case "ensureMoveLeftValidRule":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                case "ensureMoveRightValidRule":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                case "moveLeftRule":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                case "moveRightRule":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                case "countZeros":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                case "countOnes":
                    switch(member)
                    {
                    default:
                        return -1;
                    }
                default:
                    return -1;
                }
            }
        }
        public override void FailAssertion() { Debug.Assert(false); }
        public override string ModelMD5Hash { get { return "24df59ad26cdf459c8c89162a70ce265"; } }
    }
}