// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "test.grg" on Mon Nov 18 19:45:18 CET 2024

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
using GRGEN_MODEL = de.unika.ipd.grGen.Model_complModel;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_test;

namespace de.unika.ipd.grGen.Action_test
{
	public class Rule_testRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_testRule instance = null;
		public static Rule_testRule Instance { get { if(instance==null) { instance = new Rule_testRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] testRule_node_a_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] testRule_node_f_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] testRule_node_m_AllowedTypes = null;
		public static bool[] testRule_node_a_IsAllowedType = null;
		public static bool[] testRule_node_f_IsAllowedType = null;
		public static bool[] testRule_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] testRule_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] testRule_edge__edge1_AllowedTypes = null;
		public static bool[] testRule_edge__edge0_IsAllowedType = null;
		public static bool[] testRule_edge__edge1_IsAllowedType = null;
		public enum testRule_NodeNums { @a, @f, @m, };
		public enum testRule_EdgeNums { @_edge0, @_edge1, };
		public enum testRule_VariableNums { };
		public enum testRule_SubNums { };
		public enum testRule_AltNums { };
		public enum testRule_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_testRule;


		private Rule_testRule()
			: base("testRule",
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
				"de.unika.ipd.grGen.Action_test.Rule_testRule+IMatch_testRule",
				"de.unika.ipd.grGen.Action_test.Rule_testRule+Match_testRule"
			)
		{
		}
		private void initialize()
		{
			bool[,] testRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] testRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] testRule_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] testRule_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode testRule_node_a = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@D231_4121, GRGEN_MODEL.NodeType_D231_4121.typeVar, "GRGEN_MODEL.ID231_4121", "testRule_node_a", "a", testRule_node_a_AllowedTypes, testRule_node_a_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode testRule_node_f = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@B21, GRGEN_MODEL.NodeType_B21.typeVar, "GRGEN_MODEL.IB21", "testRule_node_f", "f", testRule_node_f_AllowedTypes, testRule_node_f_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternNode testRule_node_m = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@D2211_2222_31, GRGEN_MODEL.NodeType_D2211_2222_31.typeVar, "GRGEN_MODEL.ID2211_2222_31", "testRule_node_m", "m", testRule_node_m_AllowedTypes, testRule_node_m_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge testRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "testRule_edge__edge0", "_edge0", testRule_edge__edge0_AllowedTypes, testRule_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			GRGEN_LGSP.PatternEdge testRule_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "testRule_edge__edge1", "_edge1", testRule_edge__edge1_AllowedTypes, testRule_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false, null);
			pat_testRule = new GRGEN_LGSP.PatternGraph(
				"testRule",
				"",
				null, "testRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { testRule_node_a, testRule_node_f, testRule_node_m }, 
				new GRGEN_LGSP.PatternEdge[] { testRule_edge__edge0, testRule_edge__edge1 }, 
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
				testRule_isNodeHomomorphicGlobal,
				testRule_isEdgeHomomorphicGlobal,
				testRule_isNodeTotallyHomomorphic,
				testRule_isEdgeTotallyHomomorphic
			);
			pat_testRule.edgeToSourceNode.Add(testRule_edge__edge0, testRule_node_a);
			pat_testRule.edgeToTargetNode.Add(testRule_edge__edge0, testRule_node_f);
			pat_testRule.edgeToSourceNode.Add(testRule_edge__edge1, testRule_node_f);
			pat_testRule.edgeToTargetNode.Add(testRule_edge__edge1, testRule_node_m);

			testRule_node_a.pointOfDefinition = pat_testRule;
			testRule_node_f.pointOfDefinition = pat_testRule;
			testRule_node_m.pointOfDefinition = pat_testRule;
			testRule_edge__edge0.pointOfDefinition = pat_testRule;
			testRule_edge__edge1.pointOfDefinition = pat_testRule;

			patternGraph = pat_testRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_testRule curMatch = (Match_testRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_a = curMatch._node_a;
			GRGEN_LGSP.LGSPNode node_f = curMatch._node_f;
			GRGEN_LGSP.LGSPNode node_m = curMatch._node_m;
			graph.SettingAddedNodeNames( testRule_addedNodeNames );
			GRGEN_LGSP.LGSPNode node_are = graph.Retype(node_a, GRGEN_MODEL.NodeType_D2211_2222_31.typeVar);
			GRGEN_MODEL.ID2211_2222_31 inode_are = (GRGEN_MODEL.ID2211_2222_31) node_are;
			GRGEN_LGSP.LGSPNode node_fre = graph.Retype(node_f, GRGEN_MODEL.NodeType_D231_4121.typeVar);
			GRGEN_MODEL.ID231_4121 inode_fre = (GRGEN_MODEL.ID231_4121) node_fre;
			GRGEN_LGSP.LGSPNode node_mre = graph.Retype(node_m, GRGEN_MODEL.NodeType_D11_2221.typeVar);
			GRGEN_MODEL.ID11_2221 inode_mre = (GRGEN_MODEL.ID11_2221) node_mre;
			graph.SettingAddedEdgeNames( testRule_addedEdgeNames );
			{ // eval_0
				int tempvar_0 = (int )1234;
				graph.ChangingNodeAttribute(node_are, GRGEN_MODEL.NodeType_D2211_2222_31.AttributeType_d2211_2222_31, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_are.@d2211_2222_31 = tempvar_0;
				graph.ChangedNodeAttribute(node_are, GRGEN_MODEL.NodeType_D2211_2222_31.AttributeType_d2211_2222_31);
				int tempvar_1 = (int )5678;
				graph.ChangingNodeAttribute(node_fre, GRGEN_MODEL.NodeType_D231_4121.AttributeType_d231_4121, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_1, null);
				inode_fre.@d231_4121 = tempvar_1;
				graph.ChangedNodeAttribute(node_fre, GRGEN_MODEL.NodeType_D231_4121.AttributeType_d231_4121);
				int tempvar_2 = (int )9012;
				graph.ChangingNodeAttribute(node_mre, GRGEN_MODEL.NodeType_D11_2221.AttributeType_d11_2221, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_2, null);
				inode_mre.@d11_2221 = tempvar_2;
				graph.ChangedNodeAttribute(node_mre, GRGEN_MODEL.NodeType_D11_2221.AttributeType_d11_2221);
			}
			actionEnv.SelectedMatchRewritten();
			return;
		}
		private static string[] testRule_addedNodeNames = new string[] {  };
		private static string[] testRule_addedEdgeNames = new string[] {  };

		static Rule_testRule() {
		}

		public interface IMatch_testRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.ID231_4121 node_a { get; set; }
			GRGEN_MODEL.IB21 node_f { get; set; }
			GRGEN_MODEL.ID2211_2222_31 node_m { get; set; }
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

		public class Match_testRule : GRGEN_LGSP.MatchListElement<Match_testRule>, IMatch_testRule
		{
			public GRGEN_MODEL.ID231_4121 node_a { get { return (GRGEN_MODEL.ID231_4121)_node_a; } set { _node_a = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IB21 node_f { get { return (GRGEN_MODEL.IB21)_node_f; } set { _node_f = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.ID2211_2222_31 node_m { get { return (GRGEN_MODEL.ID2211_2222_31)_node_m; } set { _node_m = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_f;
			public GRGEN_LGSP.LGSPNode _node_m;
			public enum testRule_NodeNums { @a, @f, @m, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3; } }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)testRule_NodeNums.@a: return _node_a;
				case (int)testRule_NodeNums.@f: return _node_f;
				case (int)testRule_NodeNums.@m: return _node_m;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "a": return _node_a;
				case "f": return _node_f;
				case "m": return _node_m;
				default: return null;
				}
			}
			public override void SetNode(string name, GRGEN_LIBGR.INode value)
			{
				switch(name) {
				case "a": _node_a = (GRGEN_LGSP.LGSPNode)value; break;
				case "f": _node_f = (GRGEN_LGSP.LGSPNode)value; break;
				case "m": _node_m = (GRGEN_LGSP.LGSPNode)value; break;
				default: break;
				}
			}

			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum testRule_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 2; } }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)testRule_EdgeNums.@_edge0: return _edge__edge0;
				case (int)testRule_EdgeNums.@_edge1: return _edge__edge1;
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

			public enum testRule_VariableNums { END_OF_ENUM };
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

			public enum testRule_SubNums { END_OF_ENUM };
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

			public enum testRule_AltNums { END_OF_ENUM };
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

			public enum testRule_IterNums { END_OF_ENUM };
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

			public enum testRule_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_testRule.instance.pat_testRule; } }
			public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_testRule(this); }
			public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new Match_testRule(this, oldToNewMap); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_testRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_testRule cur = this;
				while(cur != null) {
					Match_testRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void AssignContent(Match_testRule that)
			{
				_node_a = that._node_a;
				_node_f = that._node_f;
				_node_m = that._node_m;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
			}

			public Match_testRule(Match_testRule that)
			{
				AssignContent(that);
			}
			public void AssignContent(Match_testRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				_node_a = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_a];
				_node_f = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_f];
				_node_m = (GRGEN_LGSP.LGSPNode)oldToNewMap[that._node_m];
				_edge__edge0 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge0];
				_edge__edge1 = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that._edge__edge1];
			}

			public Match_testRule(Match_testRule that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
			{
				AssignContent(that, oldToNewMap);
			}
			public Match_testRule()
			{
			}

			public bool IsEqual(Match_testRule that)
			{
				if(that==null) return false;
				if(_node_a != that._node_a) return false;
				if(_node_f != that._node_f) return false;
				if(_node_m != that._node_m) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.ID231_4121> Extract_a(List<IMatch_testRule> matchList)
			{
				List<GRGEN_MODEL.ID231_4121> resultList = new List<GRGEN_MODEL.ID231_4121>(matchList.Count);
				foreach(IMatch_testRule match in matchList)
					resultList.Add(match.node_a);
				return resultList;
			}
			public static List<GRGEN_MODEL.IB21> Extract_f(List<IMatch_testRule> matchList)
			{
				List<GRGEN_MODEL.IB21> resultList = new List<GRGEN_MODEL.IB21>(matchList.Count);
				foreach(IMatch_testRule match in matchList)
					resultList.Add(match.node_f);
				return resultList;
			}
			public static List<GRGEN_MODEL.ID2211_2222_31> Extract_m(List<IMatch_testRule> matchList)
			{
				List<GRGEN_MODEL.ID2211_2222_31> resultList = new List<GRGEN_MODEL.ID2211_2222_31>(matchList.Count);
				foreach(IMatch_testRule match in matchList)
					resultList.Add(match.node_m);
				return resultList;
			}
			public static List<GRGEN_LIBGR.IDEdge> Extract__edge0(List<IMatch_testRule> matchList)
			{
				List<GRGEN_LIBGR.IDEdge> resultList = new List<GRGEN_LIBGR.IDEdge>(matchList.Count);
				foreach(IMatch_testRule match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
			public static List<GRGEN_LIBGR.IDEdge> Extract__edge1(List<IMatch_testRule> matchList)
			{
				List<GRGEN_LIBGR.IDEdge> resultList = new List<GRGEN_LIBGR.IDEdge>(matchList.Count);
				foreach(IMatch_testRule match in matchList)
					resultList.Add(match.edge__edge1);
				return resultList;
			}
		}


		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> ConvertAsNeeded(object parameter)
		{
			if(parameter is List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>)
				return ((List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>)parameter);
			else
				return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>((IList<GRGEN_LIBGR.IMatch>)parameter);
		}
	}

	
	public partial class ArrayHelper
	{
		private static GRGEN_ACTIONS.Rule_testRule.IMatch_testRule instanceBearingAttributeForSearch_testRule = new GRGEN_ACTIONS.Rule_testRule.Match_testRule();
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_groupBy_a(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			Dictionary<GRGEN_MODEL.ID231_4121, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>> seenValues = new Dictionary<GRGEN_MODEL.ID231_4121, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_a)) {
					seenValues[list[pos].@node_a].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> tempList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_a, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			foreach(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_keepOneForEachBy_a(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			Dictionary<GRGEN_MODEL.ID231_4121, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.ID231_4121, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_testRule.IMatch_testRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_a)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_a, null);
				}
			}
			return newList;
		}
		public static int Array_testRule_indexOfBy_a(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID231_4121 entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_a.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_indexOfBy_a(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID231_4121 entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_a.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy_a(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID231_4121 entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_a.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy_a(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID231_4121 entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_a.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_groupBy_f(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			Dictionary<GRGEN_MODEL.IB21, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>> seenValues = new Dictionary<GRGEN_MODEL.IB21, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_f)) {
					seenValues[list[pos].@node_f].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> tempList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_f, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			foreach(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_keepOneForEachBy_f(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			Dictionary<GRGEN_MODEL.IB21, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IB21, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_testRule.IMatch_testRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_f)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_f, null);
				}
			}
			return newList;
		}
		public static int Array_testRule_indexOfBy_f(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.IB21 entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_f.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_indexOfBy_f(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.IB21 entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_f.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy_f(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.IB21 entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_f.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy_f(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.IB21 entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_f.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_groupBy_m(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			Dictionary<GRGEN_MODEL.ID2211_2222_31, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>> seenValues = new Dictionary<GRGEN_MODEL.ID2211_2222_31, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@node_m)) {
					seenValues[list[pos].@node_m].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> tempList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@node_m, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			foreach(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_keepOneForEachBy_m(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			Dictionary<GRGEN_MODEL.ID2211_2222_31, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.ID2211_2222_31, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_testRule.IMatch_testRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_m)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_m, null);
				}
			}
			return newList;
		}
		public static int Array_testRule_indexOfBy_m(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID2211_2222_31 entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@node_m.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_indexOfBy_m(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID2211_2222_31 entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@node_m.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy_m(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID2211_2222_31 entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@node_m.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy_m(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_MODEL.ID2211_2222_31 entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@node_m.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_groupBy__edge0(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			Dictionary<GRGEN_LIBGR.IDEdge, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>> seenValues = new Dictionary<GRGEN_LIBGR.IDEdge, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge0)) {
					seenValues[list[pos].@edge__edge0].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> tempList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge0, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			foreach(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_keepOneForEachBy__edge0(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			Dictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_testRule.IMatch_testRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge0)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge0, null);
				}
			}
			return newList;
		}
		public static int Array_testRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_indexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy__edge0(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge0.Equals(entry))
					return i;
			return -1;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_groupBy__edge1(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			Dictionary<GRGEN_LIBGR.IDEdge, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>> seenValues = new Dictionary<GRGEN_LIBGR.IDEdge, List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@edge__edge1)) {
					seenValues[list[pos].@edge__edge1].Add(list[pos]);
				} else {
					List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> tempList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@edge__edge1, tempList);
				}
			}
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			foreach(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> Array_testRule_keepOneForEachBy__edge1(List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list)
		{
			List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> newList = new List<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule>();
			Dictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_testRule.IMatch_testRule element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge__edge1)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge__edge1, null);
				}
			}
			return newList;
		}
		public static int Array_testRule_indexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_indexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@edge__edge1.Equals(entry))
					return i;
			return -1;
		}
		public static int Array_testRule_lastIndexOfBy__edge1(IList<GRGEN_ACTIONS.Rule_testRule.IMatch_testRule> list, GRGEN_LIBGR.IDEdge entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@edge__edge1.Equals(entry))
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

	public class test_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public test_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[1];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+1];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			functions = new GRGEN_LIBGR.FunctionInfo[0+0];
			procedures = new GRGEN_LIBGR.ProcedureInfo[0+0];
			matchClasses = new GRGEN_LIBGR.MatchClassInfo[0];
			packages = new string[0];
			rules[0] = Rule_testRule.Instance;
			rulesAndSubpatterns[0+0] = Rule_testRule.Instance;
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
    public interface IAction_testRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testRule.IMatch_testRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches);
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
    
    public class Action_testRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_testRule
    {
        public Action_testRule()
            : base(Rule_testRule.Instance.patternGraph)
        {
            _rulePattern = Rule_testRule.Instance;
            DynamicMatch = myMatch;
        }

        public Rule_testRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "testRule"; } }
        [ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<Rule_testRule.Match_testRule, Rule_testRule.IMatch_testRule> matches;

        // Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.
        [ThreadStatic] public static object[] ReturnArray;
        [ThreadStatic] public static List<object[]> ReturnArrayListForAll;
        [ThreadStatic] public static List<object[]> AvailableReturnArrays;
        public static Action_testRule Instance { get { return instance; } set { instance = value; } }
        private static Action_testRule instance = new Action_testRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            if(matches == null)
                matches = new GRGEN_LGSP.LGSPMatchesList<Rule_testRule.Match_testRule, Rule_testRule.IMatch_testRule>(this);
            matches.Clear();
            if(ReturnArray == null)
                ReturnArray = new object[0];
            int isoSpace = 0;
            // Lookup testRule_edge__edge1 
            int type_id_candidate_testRule_edge__edge1 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_testRule_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_testRule_edge__edge1], candidate_testRule_edge__edge1 = head_candidate_testRule_edge__edge1.lgspTypeNext; candidate_testRule_edge__edge1 != head_candidate_testRule_edge__edge1; candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.lgspTypeNext)
            {
                uint prev__candidate_testRule_edge__edge1;
                prev__candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_testRule_edge__edge1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Source testRule_node_f from testRule_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_testRule_node_f = candidate_testRule_edge__edge1.lgspSource;
                if(candidate_testRule_node_f.lgspType.TypeID!=6) {
                    candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
                    continue;
                }
                // Implicit Target testRule_node_m from testRule_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_testRule_node_m = candidate_testRule_edge__edge1.lgspTarget;
                if(candidate_testRule_node_m.lgspType.TypeID!=17) {
                    candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
                    continue;
                }
                // Extend Incoming testRule_edge__edge0 from testRule_node_f 
                GRGEN_LGSP.LGSPEdge head_candidate_testRule_edge__edge0 = candidate_testRule_node_f.lgspInhead;
                if(head_candidate_testRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_testRule_edge__edge0 = head_candidate_testRule_edge__edge0;
                    do
                    {
                        if(candidate_testRule_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_testRule_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        // Implicit Source testRule_node_a from testRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_testRule_node_a = candidate_testRule_edge__edge0.lgspSource;
                        if(candidate_testRule_node_a.lgspType.TypeID!=18) {
                            continue;
                        }
                        Rule_testRule.Match_testRule match = matches.GetNextUnfilledPosition();
                        match._node_a = candidate_testRule_node_a;
                        match._node_f = candidate_testRule_node_f;
                        match._node_m = candidate_testRule_node_m;
                        match._edge__edge0 = candidate_testRule_edge__edge0;
                        match._edge__edge1 = candidate_testRule_edge__edge1;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_testRule_node_f.MoveInHeadAfter(candidate_testRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_testRule_edge__edge1);
                            candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
                            return matches;
                        }
                    }
                    while( (candidate_testRule_edge__edge0 = candidate_testRule_edge__edge0.lgspInNext) != head_candidate_testRule_edge__edge0 );
                }
                candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testRule.IMatch_testRule match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches)
        {
            foreach(Rule_testRule.IMatch_testRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_testRule.IMatch_testRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches;
            
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
            
            Modify(actionEnv, (Rule_testRule.IMatch_testRule)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule>)matches);
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
    public class testActions : GRGEN_LGSP.LGSPActions
    {
        public testActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public testActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            packages = new string[0];
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_testRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_testRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_testRule.Instance);
            actions.Add("testRule", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_testRule.Instance);
            @testRule = GRGEN_ACTIONS.Action_testRule.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_testRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_testRule.Instance.patternGraph);
            GRGEN_ACTIONS.Rule_testRule.Instance.patternGraph.maxIsoSpace = 0;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_testRule.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
        }
        
        public GRGEN_ACTIONS.IAction_testRule @testRule;
        
        
        public override string[] Packages { get { return packages; } }
        private string[] packages;
        
        public override string Name { get { return "testActions"; } }
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
                case "testRule":
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
                case "testRule":
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
                case "testRule":
                    switch(member)
                    {
                    case "a":
                        return ArrayHelper.Array_testRule_groupBy_a(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "f":
                        return ArrayHelper.Array_testRule_groupBy_f(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "m":
                        return ArrayHelper.Array_testRule_groupBy_m(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_testRule_groupBy__edge0(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "_edge1":
                        return ArrayHelper.Array_testRule_groupBy__edge1(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
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
                case "testRule":
                    switch(member)
                    {
                    case "a":
                        return ArrayHelper.Array_testRule_keepOneForEachBy_a(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "f":
                        return ArrayHelper.Array_testRule_keepOneForEachBy_f(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "m":
                        return ArrayHelper.Array_testRule_keepOneForEachBy_m(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "_edge0":
                        return ArrayHelper.Array_testRule_keepOneForEachBy__edge0(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
                    case "_edge1":
                        return ArrayHelper.Array_testRule_keepOneForEachBy__edge1(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array));
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
                case "testRule":
                    switch(member)
                    {
                    case "a":
                        return ArrayHelper.Array_testRule_indexOfBy_a(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID231_4121)value);
                    case "f":
                        return ArrayHelper.Array_testRule_indexOfBy_f(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.IB21)value);
                    case "m":
                        return ArrayHelper.Array_testRule_indexOfBy_m(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID2211_2222_31)value);
                    case "_edge0":
                        return ArrayHelper.Array_testRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value);
                    case "_edge1":
                        return ArrayHelper.Array_testRule_indexOfBy__edge1(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value);
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
                case "testRule":
                    switch(member)
                    {
                    case "a":
                        return ArrayHelper.Array_testRule_indexOfBy_a(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID231_4121)value, startIndex);
                    case "f":
                        return ArrayHelper.Array_testRule_indexOfBy_f(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.IB21)value, startIndex);
                    case "m":
                        return ArrayHelper.Array_testRule_indexOfBy_m(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID2211_2222_31)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_testRule_indexOfBy__edge0(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value, startIndex);
                    case "_edge1":
                        return ArrayHelper.Array_testRule_indexOfBy__edge1(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value, startIndex);
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
                case "testRule":
                    switch(member)
                    {
                    case "a":
                        return ArrayHelper.Array_testRule_lastIndexOfBy_a(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID231_4121)value);
                    case "f":
                        return ArrayHelper.Array_testRule_lastIndexOfBy_f(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.IB21)value);
                    case "m":
                        return ArrayHelper.Array_testRule_lastIndexOfBy_m(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID2211_2222_31)value);
                    case "_edge0":
                        return ArrayHelper.Array_testRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value);
                    case "_edge1":
                        return ArrayHelper.Array_testRule_lastIndexOfBy__edge1(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value);
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
                case "testRule":
                    switch(member)
                    {
                    case "a":
                        return ArrayHelper.Array_testRule_lastIndexOfBy_a(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID231_4121)value, startIndex);
                    case "f":
                        return ArrayHelper.Array_testRule_lastIndexOfBy_f(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.IB21)value, startIndex);
                    case "m":
                        return ArrayHelper.Array_testRule_lastIndexOfBy_m(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_MODEL.ID2211_2222_31)value, startIndex);
                    case "_edge0":
                        return ArrayHelper.Array_testRule_lastIndexOfBy__edge0(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value, startIndex);
                    case "_edge1":
                        return ArrayHelper.Array_testRule_lastIndexOfBy__edge1(GRGEN_ACTIONS.Rule_testRule.ConvertAsNeeded(array), (GRGEN_LIBGR.IDEdge)value, startIndex);
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
                case "testRule":
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
        public override string ModelMD5Hash { get { return "363c3af47059e8ddde9b24be11c17a1a"; } }
    }
}