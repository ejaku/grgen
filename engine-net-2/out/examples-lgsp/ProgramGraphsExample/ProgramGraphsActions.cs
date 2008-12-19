// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ProgramGraphs\ProgramGraphs.grg" on Fri Dec 19 20:14:20 CET 2008

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using de.unika.ipd.grGen.Model_ProgramGraphs;

namespace de.unika.ipd.grGen.Action_ProgramGraphs
{
	public class Pattern_MultipleSubclasses : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_MultipleSubclasses instance = null;
		public static Pattern_MultipleSubclasses Instance { get { if (instance==null) { instance = new Pattern_MultipleSubclasses(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] MultipleSubclasses_node_c_AllowedTypes = null;
		public static bool[] MultipleSubclasses_node_c_IsAllowedType = null;
		public enum MultipleSubclasses_NodeNums { @c, };
		public enum MultipleSubclasses_EdgeNums { };
		public enum MultipleSubclasses_VariableNums { };
		public enum MultipleSubclasses_SubNums { };
		public enum MultipleSubclasses_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_MultipleSubclasses;

		public enum MultipleSubclasses_alt_0_CaseNums { @OneAndAgain, @NoSubclassLeft, };
		public static GRGEN_LIBGR.NodeType[] MultipleSubclasses_alt_0_OneAndAgain_node_sub_AllowedTypes = null;
		public static bool[] MultipleSubclasses_alt_0_OneAndAgain_node_sub_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] MultipleSubclasses_alt_0_OneAndAgain_edge__edge0_AllowedTypes = null;
		public static bool[] MultipleSubclasses_alt_0_OneAndAgain_edge__edge0_IsAllowedType = null;
		public enum MultipleSubclasses_alt_0_OneAndAgain_NodeNums { @c, @sub, };
		public enum MultipleSubclasses_alt_0_OneAndAgain_EdgeNums { @_edge0, };
		public enum MultipleSubclasses_alt_0_OneAndAgain_VariableNums { };
		public enum MultipleSubclasses_alt_0_OneAndAgain_SubNums { @_subpattern0, @_subpattern1, };
		public enum MultipleSubclasses_alt_0_OneAndAgain_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleSubclasses_alt_0_OneAndAgain;

		public enum MultipleSubclasses_alt_0_NoSubclassLeft_NodeNums { @c, };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_EdgeNums { };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_VariableNums { };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_SubNums { };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleSubclasses_alt_0_NoSubclassLeft;

		public static GRGEN_LIBGR.NodeType[] MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub_AllowedTypes = null;
		public static bool[] MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0_IsAllowedType = null;
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_NodeNums { @c, @sub, };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_EdgeNums { @_edge0, };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_VariableNums { };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_SubNums { };
		public enum MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph MultipleSubclasses_alt_0_NoSubclassLeft_neg_0;


#if INITIAL_WARMUP
		public Pattern_MultipleSubclasses()
#else
		private Pattern_MultipleSubclasses()
#endif
		{
			name = "MultipleSubclasses";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Class.typeVar, };
			inputNames = new string[] { "MultipleSubclasses_node_c", };
		}
		public override void initialize()
		{
			bool[,] MultipleSubclasses_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleSubclasses_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode MultipleSubclasses_node_c = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "MultipleSubclasses_node_c", "c", MultipleSubclasses_node_c_AllowedTypes, MultipleSubclasses_node_c_IsAllowedType, 5.5F, 0);
			bool[,] MultipleSubclasses_alt_0_OneAndAgain_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleSubclasses_alt_0_OneAndAgain_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode MultipleSubclasses_alt_0_OneAndAgain_node_sub = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "MultipleSubclasses_alt_0_OneAndAgain_node_sub", "sub", MultipleSubclasses_alt_0_OneAndAgain_node_sub_AllowedTypes, MultipleSubclasses_alt_0_OneAndAgain_node_sub_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "MultipleSubclasses_alt_0_OneAndAgain_edge__edge0", "_edge0", MultipleSubclasses_alt_0_OneAndAgain_edge__edge0_AllowedTypes, MultipleSubclasses_alt_0_OneAndAgain_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding MultipleSubclasses_alt_0_OneAndAgain__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_Subclass.Instance, new GRGEN_LGSP.PatternElement[] { MultipleSubclasses_alt_0_OneAndAgain_node_sub });
			GRGEN_LGSP.PatternGraphEmbedding MultipleSubclasses_alt_0_OneAndAgain__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_MultipleSubclasses.Instance, new GRGEN_LGSP.PatternElement[] { MultipleSubclasses_node_c });
			MultipleSubclasses_alt_0_OneAndAgain = new GRGEN_LGSP.PatternGraph(
				"OneAndAgain",
				"MultipleSubclasses_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleSubclasses_node_c, MultipleSubclasses_alt_0_OneAndAgain_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { MultipleSubclasses_alt_0_OneAndAgain__subpattern0, MultipleSubclasses_alt_0_OneAndAgain__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				MultipleSubclasses_alt_0_OneAndAgain_isNodeHomomorphicGlobal,
				MultipleSubclasses_alt_0_OneAndAgain_isEdgeHomomorphicGlobal
			);
			MultipleSubclasses_alt_0_OneAndAgain.edgeToSourceNode.Add(MultipleSubclasses_alt_0_OneAndAgain_edge__edge0, MultipleSubclasses_node_c);
			MultipleSubclasses_alt_0_OneAndAgain.edgeToTargetNode.Add(MultipleSubclasses_alt_0_OneAndAgain_edge__edge0, MultipleSubclasses_alt_0_OneAndAgain_node_sub);

			bool[,] MultipleSubclasses_alt_0_NoSubclassLeft_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleSubclasses_alt_0_NoSubclassLeft_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub", "sub", MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub_AllowedTypes, MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0", "_edge0", MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0_AllowedTypes, MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			MultipleSubclasses_alt_0_NoSubclassLeft_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"MultipleSubclasses_alt_0_NoSubclassLeft_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleSubclasses_node_c, MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_isNodeHomomorphicGlobal,
				MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_isEdgeHomomorphicGlobal
			);
			MultipleSubclasses_alt_0_NoSubclassLeft_neg_0.edgeToSourceNode.Add(MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0, MultipleSubclasses_node_c);
			MultipleSubclasses_alt_0_NoSubclassLeft_neg_0.edgeToTargetNode.Add(MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0, MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub);

			MultipleSubclasses_alt_0_NoSubclassLeft = new GRGEN_LGSP.PatternGraph(
				"NoSubclassLeft",
				"MultipleSubclasses_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleSubclasses_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { MultipleSubclasses_alt_0_NoSubclassLeft_neg_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleSubclasses_alt_0_NoSubclassLeft_isNodeHomomorphicGlobal,
				MultipleSubclasses_alt_0_NoSubclassLeft_isEdgeHomomorphicGlobal
			);
			MultipleSubclasses_alt_0_NoSubclassLeft_neg_0.embeddingGraph = MultipleSubclasses_alt_0_NoSubclassLeft;

			GRGEN_LGSP.Alternative MultipleSubclasses_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "MultipleSubclasses_", new GRGEN_LGSP.PatternGraph[] { MultipleSubclasses_alt_0_OneAndAgain, MultipleSubclasses_alt_0_NoSubclassLeft } );

			pat_MultipleSubclasses = new GRGEN_LGSP.PatternGraph(
				"MultipleSubclasses",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleSubclasses_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { MultipleSubclasses_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleSubclasses_isNodeHomomorphicGlobal,
				MultipleSubclasses_isEdgeHomomorphicGlobal
			);
			MultipleSubclasses_alt_0_OneAndAgain.embeddingGraph = pat_MultipleSubclasses;
			MultipleSubclasses_alt_0_NoSubclassLeft.embeddingGraph = pat_MultipleSubclasses;

			MultipleSubclasses_node_c.PointOfDefinition = null;
			MultipleSubclasses_alt_0_OneAndAgain_node_sub.PointOfDefinition = MultipleSubclasses_alt_0_OneAndAgain;
			MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.PointOfDefinition = MultipleSubclasses_alt_0_OneAndAgain;
			MultipleSubclasses_alt_0_OneAndAgain__subpattern0.PointOfDefinition = MultipleSubclasses_alt_0_OneAndAgain;
			MultipleSubclasses_alt_0_OneAndAgain__subpattern1.PointOfDefinition = MultipleSubclasses_alt_0_OneAndAgain;
			MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub.PointOfDefinition = MultipleSubclasses_alt_0_NoSubclassLeft_neg_0;
			MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0.PointOfDefinition = MultipleSubclasses_alt_0_NoSubclassLeft_neg_0;

			patternGraph = pat_MultipleSubclasses;
		}


		public void MultipleSubclasses_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_c)
		{
			graph.SettingAddedNodeNames( create_MultipleSubclasses_addedNodeNames );
			graph.SettingAddedEdgeNames( create_MultipleSubclasses_addedEdgeNames );
		}
		private static String[] create_MultipleSubclasses_addedNodeNames = new String[] {  };
		private static String[] create_MultipleSubclasses_addedEdgeNames = new String[] {  };

		public void MultipleSubclasses_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleSubclasses_AltNums.@alt_0 + 0];
			MultipleSubclasses_alt_0_Delete(graph, alternative_alt_0);
		}

		public void MultipleSubclasses_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleSubclasses_alt_0_OneAndAgain) {
				MultipleSubclasses_alt_0_OneAndAgain_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleSubclasses_alt_0_NoSubclassLeft) {
				MultipleSubclasses_alt_0_NoSubclassLeft_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleSubclasses_alt_0_OneAndAgain_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleSubclasses_alt_0_OneAndAgain_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			@Class node_sub = @Class.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleSubclasses_alt_0_OneAndAgain_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c, node_sub);
			Pattern_Subclass.Instance.Subclass_Create(graph, node_sub);
			Pattern_MultipleSubclasses.Instance.MultipleSubclasses_Create(graph, node_c);
		}
		private static String[] create_MultipleSubclasses_alt_0_OneAndAgain_addedNodeNames = new String[] { "c", "sub" };
		private static String[] create_MultipleSubclasses_alt_0_OneAndAgain_addedEdgeNames = new String[] { "_edge0" };

		public void MultipleSubclasses_alt_0_OneAndAgain_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_sub = curMatch.Nodes[(int)MultipleSubclasses_alt_0_OneAndAgain_NodeNums.@sub];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)MultipleSubclasses_alt_0_OneAndAgain_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)MultipleSubclasses_alt_0_OneAndAgain_SubNums.@_subpattern0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern1 = curMatch.EmbeddedGraphs[(int)MultipleSubclasses_alt_0_OneAndAgain_SubNums.@_subpattern1];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_sub);
			graph.Remove(node_sub);
			Pattern_Subclass.Instance.Subclass_Delete(graph, subpattern__subpattern0);
			Pattern_MultipleSubclasses.Instance.MultipleSubclasses_Delete(graph, subpattern__subpattern1);
		}

		public void MultipleSubclasses_alt_0_NoSubclassLeft_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleSubclasses_alt_0_NoSubclassLeft_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleSubclasses_alt_0_NoSubclassLeft_addedEdgeNames );
		}
		private static String[] create_MultipleSubclasses_alt_0_NoSubclassLeft_addedNodeNames = new String[] { "c" };
		private static String[] create_MultipleSubclasses_alt_0_NoSubclassLeft_addedEdgeNames = new String[] {  };

		public void MultipleSubclasses_alt_0_NoSubclassLeft_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
		}

		static Pattern_MultipleSubclasses() {
		}
	}

	public class Pattern_Subclass : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Subclass instance = null;
		public static Pattern_Subclass Instance { get { if (instance==null) { instance = new Pattern_Subclass(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] Subclass_node_sub_AllowedTypes = null;
		public static bool[] Subclass_node_sub_IsAllowedType = null;
		public enum Subclass_NodeNums { @sub, };
		public enum Subclass_EdgeNums { };
		public enum Subclass_VariableNums { };
		public enum Subclass_SubNums { @_subpattern0, @_subpattern1, };
		public enum Subclass_AltNums { };


		GRGEN_LGSP.PatternGraph pat_Subclass;


#if INITIAL_WARMUP
		public Pattern_Subclass()
#else
		private Pattern_Subclass()
#endif
		{
			name = "Subclass";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Class.typeVar, };
			inputNames = new string[] { "Subclass_node_sub", };
		}
		public override void initialize()
		{
			bool[,] Subclass_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Subclass_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode Subclass_node_sub = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "Subclass_node_sub", "sub", Subclass_node_sub_AllowedTypes, Subclass_node_sub_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternGraphEmbedding Subclass__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_MultipleSubclasses.Instance, new GRGEN_LGSP.PatternElement[] { Subclass_node_sub });
			GRGEN_LGSP.PatternGraphEmbedding Subclass__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_MultipleFeatures.Instance, new GRGEN_LGSP.PatternElement[] { Subclass_node_sub });
			pat_Subclass = new GRGEN_LGSP.PatternGraph(
				"Subclass",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Subclass_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Subclass__subpattern0, Subclass__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Subclass_isNodeHomomorphicGlobal,
				Subclass_isEdgeHomomorphicGlobal
			);

			Subclass_node_sub.PointOfDefinition = null;
			Subclass__subpattern0.PointOfDefinition = pat_Subclass;
			Subclass__subpattern1.PointOfDefinition = pat_Subclass;

			patternGraph = pat_Subclass;
		}


		public void Subclass_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_sub)
		{
			graph.SettingAddedNodeNames( create_Subclass_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Subclass_addedEdgeNames );
			Pattern_MultipleSubclasses.Instance.MultipleSubclasses_Create(graph, node_sub);
			Pattern_MultipleFeatures.Instance.MultipleFeatures_Create(graph, node_sub);
		}
		private static String[] create_Subclass_addedNodeNames = new String[] {  };
		private static String[] create_Subclass_addedEdgeNames = new String[] {  };

		public void Subclass_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)Subclass_SubNums.@_subpattern0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern1 = curMatch.EmbeddedGraphs[(int)Subclass_SubNums.@_subpattern1];
			Pattern_MultipleSubclasses.Instance.MultipleSubclasses_Delete(graph, subpattern__subpattern0);
			Pattern_MultipleFeatures.Instance.MultipleFeatures_Delete(graph, subpattern__subpattern1);
		}

		static Pattern_Subclass() {
		}
	}

	public class Pattern_MultipleFeatures : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_MultipleFeatures instance = null;
		public static Pattern_MultipleFeatures Instance { get { if (instance==null) { instance = new Pattern_MultipleFeatures(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] MultipleFeatures_node_c_AllowedTypes = null;
		public static bool[] MultipleFeatures_node_c_IsAllowedType = null;
		public enum MultipleFeatures_NodeNums { @c, };
		public enum MultipleFeatures_EdgeNums { };
		public enum MultipleFeatures_VariableNums { };
		public enum MultipleFeatures_SubNums { };
		public enum MultipleFeatures_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_MultipleFeatures;

		public enum MultipleFeatures_alt_0_CaseNums { @OneAndAgain, @NoFeatureLeft, };
		public enum MultipleFeatures_alt_0_OneAndAgain_NodeNums { @c, };
		public enum MultipleFeatures_alt_0_OneAndAgain_EdgeNums { };
		public enum MultipleFeatures_alt_0_OneAndAgain_VariableNums { };
		public enum MultipleFeatures_alt_0_OneAndAgain_SubNums { @_subpattern0, @_subpattern1, };
		public enum MultipleFeatures_alt_0_OneAndAgain_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleFeatures_alt_0_OneAndAgain;

		public enum MultipleFeatures_alt_0_NoFeatureLeft_NodeNums { @c, };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_EdgeNums { };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_VariableNums { };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_SubNums { };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleFeatures_alt_0_NoFeatureLeft;

		public static GRGEN_LIBGR.NodeType[] MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f_AllowedTypes = null;
		public static bool[] MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0_IsAllowedType = null;
		public enum MultipleFeatures_alt_0_NoFeatureLeft_neg_0_NodeNums { @c, @f, };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_neg_0_EdgeNums { @_edge0, };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_neg_0_VariableNums { };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_neg_0_SubNums { };
		public enum MultipleFeatures_alt_0_NoFeatureLeft_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph MultipleFeatures_alt_0_NoFeatureLeft_neg_0;


#if INITIAL_WARMUP
		public Pattern_MultipleFeatures()
#else
		private Pattern_MultipleFeatures()
#endif
		{
			name = "MultipleFeatures";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Class.typeVar, };
			inputNames = new string[] { "MultipleFeatures_node_c", };
		}
		public override void initialize()
		{
			bool[,] MultipleFeatures_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleFeatures_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode MultipleFeatures_node_c = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "MultipleFeatures_node_c", "c", MultipleFeatures_node_c_AllowedTypes, MultipleFeatures_node_c_IsAllowedType, 5.5F, 0);
			bool[,] MultipleFeatures_alt_0_OneAndAgain_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleFeatures_alt_0_OneAndAgain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternGraphEmbedding MultipleFeatures_alt_0_OneAndAgain__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_FeaturePattern.Instance, new GRGEN_LGSP.PatternElement[] { MultipleFeatures_node_c });
			GRGEN_LGSP.PatternGraphEmbedding MultipleFeatures_alt_0_OneAndAgain__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_MultipleFeatures.Instance, new GRGEN_LGSP.PatternElement[] { MultipleFeatures_node_c });
			MultipleFeatures_alt_0_OneAndAgain = new GRGEN_LGSP.PatternGraph(
				"OneAndAgain",
				"MultipleFeatures_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleFeatures_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { MultipleFeatures_alt_0_OneAndAgain__subpattern0, MultipleFeatures_alt_0_OneAndAgain__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleFeatures_alt_0_OneAndAgain_isNodeHomomorphicGlobal,
				MultipleFeatures_alt_0_OneAndAgain_isEdgeHomomorphicGlobal
			);

			bool[,] MultipleFeatures_alt_0_NoFeatureLeft_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleFeatures_alt_0_NoFeatureLeft_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] MultipleFeatures_alt_0_NoFeatureLeft_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleFeatures_alt_0_NoFeatureLeft_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Feature, "MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f", "f", MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f_AllowedTypes, MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0", "_edge0", MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0_AllowedTypes, MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			MultipleFeatures_alt_0_NoFeatureLeft_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"MultipleFeatures_alt_0_NoFeatureLeft_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleFeatures_node_c, MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f }, 
				new GRGEN_LGSP.PatternEdge[] { MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				MultipleFeatures_alt_0_NoFeatureLeft_neg_0_isNodeHomomorphicGlobal,
				MultipleFeatures_alt_0_NoFeatureLeft_neg_0_isEdgeHomomorphicGlobal
			);
			MultipleFeatures_alt_0_NoFeatureLeft_neg_0.edgeToSourceNode.Add(MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0, MultipleFeatures_node_c);
			MultipleFeatures_alt_0_NoFeatureLeft_neg_0.edgeToTargetNode.Add(MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0, MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f);

			MultipleFeatures_alt_0_NoFeatureLeft = new GRGEN_LGSP.PatternGraph(
				"NoFeatureLeft",
				"MultipleFeatures_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleFeatures_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { MultipleFeatures_alt_0_NoFeatureLeft_neg_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleFeatures_alt_0_NoFeatureLeft_isNodeHomomorphicGlobal,
				MultipleFeatures_alt_0_NoFeatureLeft_isEdgeHomomorphicGlobal
			);
			MultipleFeatures_alt_0_NoFeatureLeft_neg_0.embeddingGraph = MultipleFeatures_alt_0_NoFeatureLeft;

			GRGEN_LGSP.Alternative MultipleFeatures_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "MultipleFeatures_", new GRGEN_LGSP.PatternGraph[] { MultipleFeatures_alt_0_OneAndAgain, MultipleFeatures_alt_0_NoFeatureLeft } );

			pat_MultipleFeatures = new GRGEN_LGSP.PatternGraph(
				"MultipleFeatures",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleFeatures_node_c }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { MultipleFeatures_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleFeatures_isNodeHomomorphicGlobal,
				MultipleFeatures_isEdgeHomomorphicGlobal
			);
			MultipleFeatures_alt_0_OneAndAgain.embeddingGraph = pat_MultipleFeatures;
			MultipleFeatures_alt_0_NoFeatureLeft.embeddingGraph = pat_MultipleFeatures;

			MultipleFeatures_node_c.PointOfDefinition = null;
			MultipleFeatures_alt_0_OneAndAgain__subpattern0.PointOfDefinition = MultipleFeatures_alt_0_OneAndAgain;
			MultipleFeatures_alt_0_OneAndAgain__subpattern1.PointOfDefinition = MultipleFeatures_alt_0_OneAndAgain;
			MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f.PointOfDefinition = MultipleFeatures_alt_0_NoFeatureLeft_neg_0;
			MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0.PointOfDefinition = MultipleFeatures_alt_0_NoFeatureLeft_neg_0;

			patternGraph = pat_MultipleFeatures;
		}


		public void MultipleFeatures_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_c)
		{
			graph.SettingAddedNodeNames( create_MultipleFeatures_addedNodeNames );
			graph.SettingAddedEdgeNames( create_MultipleFeatures_addedEdgeNames );
		}
		private static String[] create_MultipleFeatures_addedNodeNames = new String[] {  };
		private static String[] create_MultipleFeatures_addedEdgeNames = new String[] {  };

		public void MultipleFeatures_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleFeatures_AltNums.@alt_0 + 0];
			MultipleFeatures_alt_0_Delete(graph, alternative_alt_0);
		}

		public void MultipleFeatures_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleFeatures_alt_0_OneAndAgain) {
				MultipleFeatures_alt_0_OneAndAgain_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleFeatures_alt_0_NoFeatureLeft) {
				MultipleFeatures_alt_0_NoFeatureLeft_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleFeatures_alt_0_OneAndAgain_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleFeatures_alt_0_OneAndAgain_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleFeatures_alt_0_OneAndAgain_addedEdgeNames );
			Pattern_FeaturePattern.Instance.FeaturePattern_Create(graph, node_c);
			Pattern_MultipleFeatures.Instance.MultipleFeatures_Create(graph, node_c);
		}
		private static String[] create_MultipleFeatures_alt_0_OneAndAgain_addedNodeNames = new String[] { "c" };
		private static String[] create_MultipleFeatures_alt_0_OneAndAgain_addedEdgeNames = new String[] {  };

		public void MultipleFeatures_alt_0_OneAndAgain_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)MultipleFeatures_alt_0_OneAndAgain_SubNums.@_subpattern0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern1 = curMatch.EmbeddedGraphs[(int)MultipleFeatures_alt_0_OneAndAgain_SubNums.@_subpattern1];
			Pattern_FeaturePattern.Instance.FeaturePattern_Delete(graph, subpattern__subpattern0);
			Pattern_MultipleFeatures.Instance.MultipleFeatures_Delete(graph, subpattern__subpattern1);
		}

		public void MultipleFeatures_alt_0_NoFeatureLeft_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleFeatures_alt_0_NoFeatureLeft_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleFeatures_alt_0_NoFeatureLeft_addedEdgeNames );
		}
		private static String[] create_MultipleFeatures_alt_0_NoFeatureLeft_addedNodeNames = new String[] { "c" };
		private static String[] create_MultipleFeatures_alt_0_NoFeatureLeft_addedEdgeNames = new String[] {  };

		public void MultipleFeatures_alt_0_NoFeatureLeft_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
		}

		static Pattern_MultipleFeatures() {
		}
	}

	public class Pattern_FeaturePattern : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_FeaturePattern instance = null;
		public static Pattern_FeaturePattern Instance { get { if (instance==null) { instance = new Pattern_FeaturePattern(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_node_c_AllowedTypes = null;
		public static bool[] FeaturePattern_node_c_IsAllowedType = null;
		public enum FeaturePattern_NodeNums { @c, };
		public enum FeaturePattern_EdgeNums { };
		public enum FeaturePattern_VariableNums { };
		public enum FeaturePattern_SubNums { };
		public enum FeaturePattern_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_FeaturePattern;

		public enum FeaturePattern_alt_0_CaseNums { @MethodBody, @MethodSignature, @Variable, @Konstante, };
		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_MethodBody_node_b_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodBody_node_b_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_MethodBody_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodBody_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_MethodBody_NodeNums { @c, @b, };
		public enum FeaturePattern_alt_0_MethodBody_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_MethodBody_VariableNums { };
		public enum FeaturePattern_alt_0_MethodBody_SubNums { @_subpattern0, @_subpattern1, };
		public enum FeaturePattern_alt_0_MethodBody_AltNums { };


		GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_MethodBody;

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_MethodSignature_node__node0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodSignature_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_MethodSignature_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_MethodSignature_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_MethodSignature_NodeNums { @c, @_node0, };
		public enum FeaturePattern_alt_0_MethodSignature_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_MethodSignature_VariableNums { };
		public enum FeaturePattern_alt_0_MethodSignature_SubNums { };
		public enum FeaturePattern_alt_0_MethodSignature_AltNums { };


		GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_MethodSignature;

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_Variable_node__node0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Variable_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_Variable_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Variable_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_Variable_NodeNums { @c, @_node0, };
		public enum FeaturePattern_alt_0_Variable_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_Variable_VariableNums { };
		public enum FeaturePattern_alt_0_Variable_SubNums { };
		public enum FeaturePattern_alt_0_Variable_AltNums { };


		GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_Variable;

		public static GRGEN_LIBGR.NodeType[] FeaturePattern_alt_0_Konstante_node__node0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Konstante_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] FeaturePattern_alt_0_Konstante_edge__edge0_AllowedTypes = null;
		public static bool[] FeaturePattern_alt_0_Konstante_edge__edge0_IsAllowedType = null;
		public enum FeaturePattern_alt_0_Konstante_NodeNums { @c, @_node0, };
		public enum FeaturePattern_alt_0_Konstante_EdgeNums { @_edge0, };
		public enum FeaturePattern_alt_0_Konstante_VariableNums { };
		public enum FeaturePattern_alt_0_Konstante_SubNums { };
		public enum FeaturePattern_alt_0_Konstante_AltNums { };


		GRGEN_LGSP.PatternGraph FeaturePattern_alt_0_Konstante;


#if INITIAL_WARMUP
		public Pattern_FeaturePattern()
#else
		private Pattern_FeaturePattern()
#endif
		{
			name = "FeaturePattern";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Class.typeVar, };
			inputNames = new string[] { "FeaturePattern_node_c", };
		}
		public override void initialize()
		{
			bool[,] FeaturePattern_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] FeaturePattern_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode FeaturePattern_node_c = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "FeaturePattern_node_c", "c", FeaturePattern_node_c_AllowedTypes, FeaturePattern_node_c_IsAllowedType, 5.5F, 0);
			bool[,] FeaturePattern_alt_0_MethodBody_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] FeaturePattern_alt_0_MethodBody_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_MethodBody_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "FeaturePattern_alt_0_MethodBody_node_b", "b", FeaturePattern_alt_0_MethodBody_node_b_AllowedTypes, FeaturePattern_alt_0_MethodBody_node_b_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_MethodBody_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "FeaturePattern_alt_0_MethodBody_edge__edge0", "_edge0", FeaturePattern_alt_0_MethodBody_edge__edge0_AllowedTypes, FeaturePattern_alt_0_MethodBody_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding FeaturePattern_alt_0_MethodBody__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_MultipleParameters.Instance, new GRGEN_LGSP.PatternElement[] { FeaturePattern_alt_0_MethodBody_node_b });
			GRGEN_LGSP.PatternGraphEmbedding FeaturePattern_alt_0_MethodBody__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_MultipleStatements.Instance, new GRGEN_LGSP.PatternElement[] { FeaturePattern_alt_0_MethodBody_node_b });
			FeaturePattern_alt_0_MethodBody = new GRGEN_LGSP.PatternGraph(
				"MethodBody",
				"FeaturePattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { FeaturePattern_node_c, FeaturePattern_alt_0_MethodBody_node_b }, 
				new GRGEN_LGSP.PatternEdge[] { FeaturePattern_alt_0_MethodBody_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { FeaturePattern_alt_0_MethodBody__subpattern0, FeaturePattern_alt_0_MethodBody__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
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
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_MethodSignature_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodSignature, "FeaturePattern_alt_0_MethodSignature_node__node0", "_node0", FeaturePattern_alt_0_MethodSignature_node__node0_AllowedTypes, FeaturePattern_alt_0_MethodSignature_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_MethodSignature_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "FeaturePattern_alt_0_MethodSignature_edge__edge0", "_edge0", FeaturePattern_alt_0_MethodSignature_edge__edge0_AllowedTypes, FeaturePattern_alt_0_MethodSignature_edge__edge0_IsAllowedType, 5.5F, -1);
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
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_Variable_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Variabel, "FeaturePattern_alt_0_Variable_node__node0", "_node0", FeaturePattern_alt_0_Variable_node__node0_AllowedTypes, FeaturePattern_alt_0_Variable_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_Variable_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "FeaturePattern_alt_0_Variable_edge__edge0", "_edge0", FeaturePattern_alt_0_Variable_edge__edge0_AllowedTypes, FeaturePattern_alt_0_Variable_edge__edge0_IsAllowedType, 5.5F, -1);
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
			GRGEN_LGSP.PatternNode FeaturePattern_alt_0_Konstante_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Constant, "FeaturePattern_alt_0_Konstante_node__node0", "_node0", FeaturePattern_alt_0_Konstante_node__node0_AllowedTypes, FeaturePattern_alt_0_Konstante_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge FeaturePattern_alt_0_Konstante_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "FeaturePattern_alt_0_Konstante_edge__edge0", "_edge0", FeaturePattern_alt_0_Konstante_edge__edge0_AllowedTypes, FeaturePattern_alt_0_Konstante_edge__edge0_IsAllowedType, 5.5F, -1);
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

			FeaturePattern_node_c.PointOfDefinition = null;
			FeaturePattern_alt_0_MethodBody_node_b.PointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodBody_edge__edge0.PointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodBody__subpattern0.PointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodBody__subpattern1.PointOfDefinition = FeaturePattern_alt_0_MethodBody;
			FeaturePattern_alt_0_MethodSignature_node__node0.PointOfDefinition = FeaturePattern_alt_0_MethodSignature;
			FeaturePattern_alt_0_MethodSignature_edge__edge0.PointOfDefinition = FeaturePattern_alt_0_MethodSignature;
			FeaturePattern_alt_0_Variable_node__node0.PointOfDefinition = FeaturePattern_alt_0_Variable;
			FeaturePattern_alt_0_Variable_edge__edge0.PointOfDefinition = FeaturePattern_alt_0_Variable;
			FeaturePattern_alt_0_Konstante_node__node0.PointOfDefinition = FeaturePattern_alt_0_Konstante;
			FeaturePattern_alt_0_Konstante_edge__edge0.PointOfDefinition = FeaturePattern_alt_0_Konstante;

			patternGraph = pat_FeaturePattern;
		}


		public void FeaturePattern_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_c)
		{
			graph.SettingAddedNodeNames( create_FeaturePattern_addedNodeNames );
			graph.SettingAddedEdgeNames( create_FeaturePattern_addedEdgeNames );
		}
		private static String[] create_FeaturePattern_addedNodeNames = new String[] {  };
		private static String[] create_FeaturePattern_addedEdgeNames = new String[] {  };

		public void FeaturePattern_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)FeaturePattern_AltNums.@alt_0 + 0];
			FeaturePattern_alt_0_Delete(graph, alternative_alt_0);
		}

		public void FeaturePattern_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == FeaturePattern_alt_0_MethodBody) {
				FeaturePattern_alt_0_MethodBody_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == FeaturePattern_alt_0_MethodSignature) {
				FeaturePattern_alt_0_MethodSignature_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == FeaturePattern_alt_0_Variable) {
				FeaturePattern_alt_0_Variable_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == FeaturePattern_alt_0_Konstante) {
				FeaturePattern_alt_0_Konstante_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void FeaturePattern_alt_0_MethodBody_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_FeaturePattern_alt_0_MethodBody_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_FeaturePattern_alt_0_MethodBody_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c, node_b);
			Pattern_MultipleParameters.Instance.MultipleParameters_Create(graph, node_b);
			Pattern_MultipleStatements.Instance.MultipleStatements_Create(graph, node_b);
		}
		private static String[] create_FeaturePattern_alt_0_MethodBody_addedNodeNames = new String[] { "c", "b" };
		private static String[] create_FeaturePattern_alt_0_MethodBody_addedEdgeNames = new String[] { "_edge0" };

		public void FeaturePattern_alt_0_MethodBody_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch.Nodes[(int)FeaturePattern_alt_0_MethodBody_NodeNums.@b];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)FeaturePattern_alt_0_MethodBody_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)FeaturePattern_alt_0_MethodBody_SubNums.@_subpattern0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern1 = curMatch.EmbeddedGraphs[(int)FeaturePattern_alt_0_MethodBody_SubNums.@_subpattern1];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_MultipleParameters.Instance.MultipleParameters_Delete(graph, subpattern__subpattern0);
			Pattern_MultipleStatements.Instance.MultipleStatements_Delete(graph, subpattern__subpattern1);
		}

		public void FeaturePattern_alt_0_MethodSignature_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_FeaturePattern_alt_0_MethodSignature_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			@MethodSignature node__node0 = @MethodSignature.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_FeaturePattern_alt_0_MethodSignature_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c, node__node0);
		}
		private static String[] create_FeaturePattern_alt_0_MethodSignature_addedNodeNames = new String[] { "c", "_node0" };
		private static String[] create_FeaturePattern_alt_0_MethodSignature_addedEdgeNames = new String[] { "_edge0" };

		public void FeaturePattern_alt_0_MethodSignature_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node__node0 = curMatch.Nodes[(int)FeaturePattern_alt_0_MethodSignature_NodeNums.@_node0];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)FeaturePattern_alt_0_MethodSignature_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
		}

		public void FeaturePattern_alt_0_Variable_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_FeaturePattern_alt_0_Variable_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			@Variabel node__node0 = @Variabel.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_FeaturePattern_alt_0_Variable_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c, node__node0);
		}
		private static String[] create_FeaturePattern_alt_0_Variable_addedNodeNames = new String[] { "c", "_node0" };
		private static String[] create_FeaturePattern_alt_0_Variable_addedEdgeNames = new String[] { "_edge0" };

		public void FeaturePattern_alt_0_Variable_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node__node0 = curMatch.Nodes[(int)FeaturePattern_alt_0_Variable_NodeNums.@_node0];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)FeaturePattern_alt_0_Variable_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
		}

		public void FeaturePattern_alt_0_Konstante_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_FeaturePattern_alt_0_Konstante_addedNodeNames );
			@Class node_c = @Class.CreateNode(graph);
			@Constant node__node0 = @Constant.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_FeaturePattern_alt_0_Konstante_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c, node__node0);
		}
		private static String[] create_FeaturePattern_alt_0_Konstante_addedNodeNames = new String[] { "c", "_node0" };
		private static String[] create_FeaturePattern_alt_0_Konstante_addedEdgeNames = new String[] { "_edge0" };

		public void FeaturePattern_alt_0_Konstante_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node__node0 = curMatch.Nodes[(int)FeaturePattern_alt_0_Konstante_NodeNums.@_node0];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)FeaturePattern_alt_0_Konstante_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
		}

		static Pattern_FeaturePattern() {
		}
	}

	public class Pattern_MultipleParameters : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_MultipleParameters instance = null;
		public static Pattern_MultipleParameters Instance { get { if (instance==null) { instance = new Pattern_MultipleParameters(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] MultipleParameters_node_b_AllowedTypes = null;
		public static bool[] MultipleParameters_node_b_IsAllowedType = null;
		public enum MultipleParameters_NodeNums { @b, };
		public enum MultipleParameters_EdgeNums { };
		public enum MultipleParameters_VariableNums { };
		public enum MultipleParameters_SubNums { };
		public enum MultipleParameters_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_MultipleParameters;

		public enum MultipleParameters_alt_0_CaseNums { @OneAndAgain, @NoStatementLeft, };
		public enum MultipleParameters_alt_0_OneAndAgain_NodeNums { @b, };
		public enum MultipleParameters_alt_0_OneAndAgain_EdgeNums { };
		public enum MultipleParameters_alt_0_OneAndAgain_VariableNums { };
		public enum MultipleParameters_alt_0_OneAndAgain_SubNums { @_subpattern0, @_subpattern1, };
		public enum MultipleParameters_alt_0_OneAndAgain_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleParameters_alt_0_OneAndAgain;

		public enum MultipleParameters_alt_0_NoStatementLeft_NodeNums { @b, };
		public enum MultipleParameters_alt_0_NoStatementLeft_EdgeNums { };
		public enum MultipleParameters_alt_0_NoStatementLeft_VariableNums { };
		public enum MultipleParameters_alt_0_NoStatementLeft_SubNums { };
		public enum MultipleParameters_alt_0_NoStatementLeft_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleParameters_alt_0_NoStatementLeft;

		public static GRGEN_LIBGR.NodeType[] MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a_AllowedTypes = null;
		public static bool[] MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0_IsAllowedType = null;
		public enum MultipleParameters_alt_0_NoStatementLeft_neg_0_NodeNums { @b, @a, };
		public enum MultipleParameters_alt_0_NoStatementLeft_neg_0_EdgeNums { @_edge0, };
		public enum MultipleParameters_alt_0_NoStatementLeft_neg_0_VariableNums { };
		public enum MultipleParameters_alt_0_NoStatementLeft_neg_0_SubNums { };
		public enum MultipleParameters_alt_0_NoStatementLeft_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph MultipleParameters_alt_0_NoStatementLeft_neg_0;


#if INITIAL_WARMUP
		public Pattern_MultipleParameters()
#else
		private Pattern_MultipleParameters()
#endif
		{
			name = "MultipleParameters";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "MultipleParameters_node_b", };
		}
		public override void initialize()
		{
			bool[,] MultipleParameters_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleParameters_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode MultipleParameters_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "MultipleParameters_node_b", "b", MultipleParameters_node_b_AllowedTypes, MultipleParameters_node_b_IsAllowedType, 5.5F, 0);
			bool[,] MultipleParameters_alt_0_OneAndAgain_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleParameters_alt_0_OneAndAgain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternGraphEmbedding MultipleParameters_alt_0_OneAndAgain__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_Parameter.Instance, new GRGEN_LGSP.PatternElement[] { MultipleParameters_node_b });
			GRGEN_LGSP.PatternGraphEmbedding MultipleParameters_alt_0_OneAndAgain__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_MultipleParameters.Instance, new GRGEN_LGSP.PatternElement[] { MultipleParameters_node_b });
			MultipleParameters_alt_0_OneAndAgain = new GRGEN_LGSP.PatternGraph(
				"OneAndAgain",
				"MultipleParameters_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleParameters_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { MultipleParameters_alt_0_OneAndAgain__subpattern0, MultipleParameters_alt_0_OneAndAgain__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleParameters_alt_0_OneAndAgain_isNodeHomomorphicGlobal,
				MultipleParameters_alt_0_OneAndAgain_isEdgeHomomorphicGlobal
			);

			bool[,] MultipleParameters_alt_0_NoStatementLeft_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleParameters_alt_0_NoStatementLeft_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] MultipleParameters_alt_0_NoStatementLeft_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleParameters_alt_0_NoStatementLeft_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Attribute, "MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a", "a", MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a_AllowedTypes, MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0", "_edge0", MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0_AllowedTypes, MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			MultipleParameters_alt_0_NoStatementLeft_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"MultipleParameters_alt_0_NoStatementLeft_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleParameters_node_b, MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a }, 
				new GRGEN_LGSP.PatternEdge[] { MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				MultipleParameters_alt_0_NoStatementLeft_neg_0_isNodeHomomorphicGlobal,
				MultipleParameters_alt_0_NoStatementLeft_neg_0_isEdgeHomomorphicGlobal
			);
			MultipleParameters_alt_0_NoStatementLeft_neg_0.edgeToSourceNode.Add(MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0, MultipleParameters_node_b);
			MultipleParameters_alt_0_NoStatementLeft_neg_0.edgeToTargetNode.Add(MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0, MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a);

			MultipleParameters_alt_0_NoStatementLeft = new GRGEN_LGSP.PatternGraph(
				"NoStatementLeft",
				"MultipleParameters_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleParameters_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { MultipleParameters_alt_0_NoStatementLeft_neg_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleParameters_alt_0_NoStatementLeft_isNodeHomomorphicGlobal,
				MultipleParameters_alt_0_NoStatementLeft_isEdgeHomomorphicGlobal
			);
			MultipleParameters_alt_0_NoStatementLeft_neg_0.embeddingGraph = MultipleParameters_alt_0_NoStatementLeft;

			GRGEN_LGSP.Alternative MultipleParameters_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "MultipleParameters_", new GRGEN_LGSP.PatternGraph[] { MultipleParameters_alt_0_OneAndAgain, MultipleParameters_alt_0_NoStatementLeft } );

			pat_MultipleParameters = new GRGEN_LGSP.PatternGraph(
				"MultipleParameters",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleParameters_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { MultipleParameters_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleParameters_isNodeHomomorphicGlobal,
				MultipleParameters_isEdgeHomomorphicGlobal
			);
			MultipleParameters_alt_0_OneAndAgain.embeddingGraph = pat_MultipleParameters;
			MultipleParameters_alt_0_NoStatementLeft.embeddingGraph = pat_MultipleParameters;

			MultipleParameters_node_b.PointOfDefinition = null;
			MultipleParameters_alt_0_OneAndAgain__subpattern0.PointOfDefinition = MultipleParameters_alt_0_OneAndAgain;
			MultipleParameters_alt_0_OneAndAgain__subpattern1.PointOfDefinition = MultipleParameters_alt_0_OneAndAgain;
			MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a.PointOfDefinition = MultipleParameters_alt_0_NoStatementLeft_neg_0;
			MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0.PointOfDefinition = MultipleParameters_alt_0_NoStatementLeft_neg_0;

			patternGraph = pat_MultipleParameters;
		}


		public void MultipleParameters_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_MultipleParameters_addedNodeNames );
			graph.SettingAddedEdgeNames( create_MultipleParameters_addedEdgeNames );
		}
		private static String[] create_MultipleParameters_addedNodeNames = new String[] {  };
		private static String[] create_MultipleParameters_addedEdgeNames = new String[] {  };

		public void MultipleParameters_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleParameters_AltNums.@alt_0 + 0];
			MultipleParameters_alt_0_Delete(graph, alternative_alt_0);
		}

		public void MultipleParameters_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleParameters_alt_0_OneAndAgain) {
				MultipleParameters_alt_0_OneAndAgain_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleParameters_alt_0_NoStatementLeft) {
				MultipleParameters_alt_0_NoStatementLeft_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleParameters_alt_0_OneAndAgain_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleParameters_alt_0_OneAndAgain_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleParameters_alt_0_OneAndAgain_addedEdgeNames );
			Pattern_Parameter.Instance.Parameter_Create(graph, node_b);
			Pattern_MultipleParameters.Instance.MultipleParameters_Create(graph, node_b);
		}
		private static String[] create_MultipleParameters_alt_0_OneAndAgain_addedNodeNames = new String[] { "b" };
		private static String[] create_MultipleParameters_alt_0_OneAndAgain_addedEdgeNames = new String[] {  };

		public void MultipleParameters_alt_0_OneAndAgain_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)MultipleParameters_alt_0_OneAndAgain_SubNums.@_subpattern0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern1 = curMatch.EmbeddedGraphs[(int)MultipleParameters_alt_0_OneAndAgain_SubNums.@_subpattern1];
			Pattern_Parameter.Instance.Parameter_Delete(graph, subpattern__subpattern0);
			Pattern_MultipleParameters.Instance.MultipleParameters_Delete(graph, subpattern__subpattern1);
		}

		public void MultipleParameters_alt_0_NoStatementLeft_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleParameters_alt_0_NoStatementLeft_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleParameters_alt_0_NoStatementLeft_addedEdgeNames );
		}
		private static String[] create_MultipleParameters_alt_0_NoStatementLeft_addedNodeNames = new String[] { "b" };
		private static String[] create_MultipleParameters_alt_0_NoStatementLeft_addedEdgeNames = new String[] {  };

		public void MultipleParameters_alt_0_NoStatementLeft_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
		}

		static Pattern_MultipleParameters() {
		}
	}

	public class Pattern_Parameter : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Parameter instance = null;
		public static Pattern_Parameter Instance { get { if (instance==null) { instance = new Pattern_Parameter(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] Parameter_node_b_AllowedTypes = null;
		public static bool[] Parameter_node_b_IsAllowedType = null;
		public enum Parameter_NodeNums { @b, };
		public enum Parameter_EdgeNums { };
		public enum Parameter_VariableNums { };
		public enum Parameter_SubNums { };
		public enum Parameter_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_Parameter;

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


		GRGEN_LGSP.PatternGraph Parameter_alt_0_Variable;

		public static GRGEN_LIBGR.NodeType[] Parameter_alt_0_Konstante_node_c_AllowedTypes = null;
		public static bool[] Parameter_alt_0_Konstante_node_c_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Parameter_alt_0_Konstante_edge__edge0_AllowedTypes = null;
		public static bool[] Parameter_alt_0_Konstante_edge__edge0_IsAllowedType = null;
		public enum Parameter_alt_0_Konstante_NodeNums { @b, @c, };
		public enum Parameter_alt_0_Konstante_EdgeNums { @_edge0, };
		public enum Parameter_alt_0_Konstante_VariableNums { };
		public enum Parameter_alt_0_Konstante_SubNums { };
		public enum Parameter_alt_0_Konstante_AltNums { };


		GRGEN_LGSP.PatternGraph Parameter_alt_0_Konstante;


#if INITIAL_WARMUP
		public Pattern_Parameter()
#else
		private Pattern_Parameter()
#endif
		{
			name = "Parameter";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "Parameter_node_b", };
		}
		public override void initialize()
		{
			bool[,] Parameter_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Parameter_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode Parameter_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "Parameter_node_b", "b", Parameter_node_b_AllowedTypes, Parameter_node_b_IsAllowedType, 5.5F, 0);
			bool[,] Parameter_alt_0_Variable_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Parameter_alt_0_Variable_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode Parameter_alt_0_Variable_node_v = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Variabel, "Parameter_alt_0_Variable_node_v", "v", Parameter_alt_0_Variable_node_v_AllowedTypes, Parameter_alt_0_Variable_node_v_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Parameter_alt_0_Variable_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "Parameter_alt_0_Variable_edge__edge0", "_edge0", Parameter_alt_0_Variable_edge__edge0_AllowedTypes, Parameter_alt_0_Variable_edge__edge0_IsAllowedType, 5.5F, -1);
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
			GRGEN_LGSP.PatternNode Parameter_alt_0_Konstante_node_c = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Constant, "Parameter_alt_0_Konstante_node_c", "c", Parameter_alt_0_Konstante_node_c_AllowedTypes, Parameter_alt_0_Konstante_node_c_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Parameter_alt_0_Konstante_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "Parameter_alt_0_Konstante_edge__edge0", "_edge0", Parameter_alt_0_Konstante_edge__edge0_AllowedTypes, Parameter_alt_0_Konstante_edge__edge0_IsAllowedType, 5.5F, -1);
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

			Parameter_node_b.PointOfDefinition = null;
			Parameter_alt_0_Variable_node_v.PointOfDefinition = Parameter_alt_0_Variable;
			Parameter_alt_0_Variable_edge__edge0.PointOfDefinition = Parameter_alt_0_Variable;
			Parameter_alt_0_Konstante_node_c.PointOfDefinition = Parameter_alt_0_Konstante;
			Parameter_alt_0_Konstante_edge__edge0.PointOfDefinition = Parameter_alt_0_Konstante;

			patternGraph = pat_Parameter;
		}


		public void Parameter_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_Parameter_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Parameter_addedEdgeNames );
		}
		private static String[] create_Parameter_addedNodeNames = new String[] {  };
		private static String[] create_Parameter_addedEdgeNames = new String[] {  };

		public void Parameter_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)Parameter_AltNums.@alt_0 + 0];
			Parameter_alt_0_Delete(graph, alternative_alt_0);
		}

		public void Parameter_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == Parameter_alt_0_Variable) {
				Parameter_alt_0_Variable_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == Parameter_alt_0_Konstante) {
				Parameter_alt_0_Konstante_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void Parameter_alt_0_Variable_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Parameter_alt_0_Variable_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			@Variabel node_v = @Variabel.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Parameter_alt_0_Variable_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_b, node_v);
		}
		private static String[] create_Parameter_alt_0_Variable_addedNodeNames = new String[] { "b", "v" };
		private static String[] create_Parameter_alt_0_Variable_addedEdgeNames = new String[] { "_edge0" };

		public void Parameter_alt_0_Variable_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_v = curMatch.Nodes[(int)Parameter_alt_0_Variable_NodeNums.@v];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)Parameter_alt_0_Variable_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_v);
			graph.Remove(node_v);
		}

		public void Parameter_alt_0_Konstante_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Parameter_alt_0_Konstante_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			@Constant node_c = @Constant.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Parameter_alt_0_Konstante_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_b, node_c);
		}
		private static String[] create_Parameter_alt_0_Konstante_addedNodeNames = new String[] { "b", "c" };
		private static String[] create_Parameter_alt_0_Konstante_addedEdgeNames = new String[] { "_edge0" };

		public void Parameter_alt_0_Konstante_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch.Nodes[(int)Parameter_alt_0_Konstante_NodeNums.@c];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)Parameter_alt_0_Konstante_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_c);
			graph.Remove(node_c);
		}

		static Pattern_Parameter() {
		}
	}

	public class Pattern_MultipleStatements : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_MultipleStatements instance = null;
		public static Pattern_MultipleStatements Instance { get { if (instance==null) { instance = new Pattern_MultipleStatements(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] MultipleStatements_node_b_AllowedTypes = null;
		public static bool[] MultipleStatements_node_b_IsAllowedType = null;
		public enum MultipleStatements_NodeNums { @b, };
		public enum MultipleStatements_EdgeNums { };
		public enum MultipleStatements_VariableNums { };
		public enum MultipleStatements_SubNums { };
		public enum MultipleStatements_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_MultipleStatements;

		public enum MultipleStatements_alt_0_CaseNums { @OneAndAgain, @NoStatementLeft, };
		public enum MultipleStatements_alt_0_OneAndAgain_NodeNums { @b, };
		public enum MultipleStatements_alt_0_OneAndAgain_EdgeNums { };
		public enum MultipleStatements_alt_0_OneAndAgain_VariableNums { };
		public enum MultipleStatements_alt_0_OneAndAgain_SubNums { @_subpattern0, @_subpattern1, };
		public enum MultipleStatements_alt_0_OneAndAgain_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleStatements_alt_0_OneAndAgain;

		public enum MultipleStatements_alt_0_NoStatementLeft_NodeNums { @b, };
		public enum MultipleStatements_alt_0_NoStatementLeft_EdgeNums { };
		public enum MultipleStatements_alt_0_NoStatementLeft_VariableNums { };
		public enum MultipleStatements_alt_0_NoStatementLeft_SubNums { };
		public enum MultipleStatements_alt_0_NoStatementLeft_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleStatements_alt_0_NoStatementLeft;

		public static GRGEN_LIBGR.NodeType[] MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e_AllowedTypes = null;
		public static bool[] MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0_IsAllowedType = null;
		public enum MultipleStatements_alt_0_NoStatementLeft_neg_0_NodeNums { @b, @e, };
		public enum MultipleStatements_alt_0_NoStatementLeft_neg_0_EdgeNums { @_edge0, };
		public enum MultipleStatements_alt_0_NoStatementLeft_neg_0_VariableNums { };
		public enum MultipleStatements_alt_0_NoStatementLeft_neg_0_SubNums { };
		public enum MultipleStatements_alt_0_NoStatementLeft_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph MultipleStatements_alt_0_NoStatementLeft_neg_0;


#if INITIAL_WARMUP
		public Pattern_MultipleStatements()
#else
		private Pattern_MultipleStatements()
#endif
		{
			name = "MultipleStatements";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "MultipleStatements_node_b", };
		}
		public override void initialize()
		{
			bool[,] MultipleStatements_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleStatements_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode MultipleStatements_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "MultipleStatements_node_b", "b", MultipleStatements_node_b_AllowedTypes, MultipleStatements_node_b_IsAllowedType, 5.5F, 0);
			bool[,] MultipleStatements_alt_0_OneAndAgain_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleStatements_alt_0_OneAndAgain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternGraphEmbedding MultipleStatements_alt_0_OneAndAgain__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_Statement.Instance, new GRGEN_LGSP.PatternElement[] { MultipleStatements_node_b });
			GRGEN_LGSP.PatternGraphEmbedding MultipleStatements_alt_0_OneAndAgain__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_MultipleStatements.Instance, new GRGEN_LGSP.PatternElement[] { MultipleStatements_node_b });
			MultipleStatements_alt_0_OneAndAgain = new GRGEN_LGSP.PatternGraph(
				"OneAndAgain",
				"MultipleStatements_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleStatements_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { MultipleStatements_alt_0_OneAndAgain__subpattern0, MultipleStatements_alt_0_OneAndAgain__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleStatements_alt_0_OneAndAgain_isNodeHomomorphicGlobal,
				MultipleStatements_alt_0_OneAndAgain_isEdgeHomomorphicGlobal
			);

			bool[,] MultipleStatements_alt_0_NoStatementLeft_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleStatements_alt_0_NoStatementLeft_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] MultipleStatements_alt_0_NoStatementLeft_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleStatements_alt_0_NoStatementLeft_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e", "e", MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e_AllowedTypes, MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0", "_edge0", MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0_AllowedTypes, MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			MultipleStatements_alt_0_NoStatementLeft_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"MultipleStatements_alt_0_NoStatementLeft_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleStatements_node_b, MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				MultipleStatements_alt_0_NoStatementLeft_neg_0_isNodeHomomorphicGlobal,
				MultipleStatements_alt_0_NoStatementLeft_neg_0_isEdgeHomomorphicGlobal
			);
			MultipleStatements_alt_0_NoStatementLeft_neg_0.edgeToSourceNode.Add(MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0, MultipleStatements_node_b);
			MultipleStatements_alt_0_NoStatementLeft_neg_0.edgeToTargetNode.Add(MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0, MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e);

			MultipleStatements_alt_0_NoStatementLeft = new GRGEN_LGSP.PatternGraph(
				"NoStatementLeft",
				"MultipleStatements_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleStatements_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { MultipleStatements_alt_0_NoStatementLeft_neg_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleStatements_alt_0_NoStatementLeft_isNodeHomomorphicGlobal,
				MultipleStatements_alt_0_NoStatementLeft_isEdgeHomomorphicGlobal
			);
			MultipleStatements_alt_0_NoStatementLeft_neg_0.embeddingGraph = MultipleStatements_alt_0_NoStatementLeft;

			GRGEN_LGSP.Alternative MultipleStatements_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "MultipleStatements_", new GRGEN_LGSP.PatternGraph[] { MultipleStatements_alt_0_OneAndAgain, MultipleStatements_alt_0_NoStatementLeft } );

			pat_MultipleStatements = new GRGEN_LGSP.PatternGraph(
				"MultipleStatements",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleStatements_node_b }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { MultipleStatements_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleStatements_isNodeHomomorphicGlobal,
				MultipleStatements_isEdgeHomomorphicGlobal
			);
			MultipleStatements_alt_0_OneAndAgain.embeddingGraph = pat_MultipleStatements;
			MultipleStatements_alt_0_NoStatementLeft.embeddingGraph = pat_MultipleStatements;

			MultipleStatements_node_b.PointOfDefinition = null;
			MultipleStatements_alt_0_OneAndAgain__subpattern0.PointOfDefinition = MultipleStatements_alt_0_OneAndAgain;
			MultipleStatements_alt_0_OneAndAgain__subpattern1.PointOfDefinition = MultipleStatements_alt_0_OneAndAgain;
			MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e.PointOfDefinition = MultipleStatements_alt_0_NoStatementLeft_neg_0;
			MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0.PointOfDefinition = MultipleStatements_alt_0_NoStatementLeft_neg_0;

			patternGraph = pat_MultipleStatements;
		}


		public void MultipleStatements_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_MultipleStatements_addedNodeNames );
			graph.SettingAddedEdgeNames( create_MultipleStatements_addedEdgeNames );
		}
		private static String[] create_MultipleStatements_addedNodeNames = new String[] {  };
		private static String[] create_MultipleStatements_addedEdgeNames = new String[] {  };

		public void MultipleStatements_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleStatements_AltNums.@alt_0 + 0];
			MultipleStatements_alt_0_Delete(graph, alternative_alt_0);
		}

		public void MultipleStatements_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleStatements_alt_0_OneAndAgain) {
				MultipleStatements_alt_0_OneAndAgain_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleStatements_alt_0_NoStatementLeft) {
				MultipleStatements_alt_0_NoStatementLeft_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleStatements_alt_0_OneAndAgain_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleStatements_alt_0_OneAndAgain_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleStatements_alt_0_OneAndAgain_addedEdgeNames );
			Pattern_Statement.Instance.Statement_Create(graph, node_b);
			Pattern_MultipleStatements.Instance.MultipleStatements_Create(graph, node_b);
		}
		private static String[] create_MultipleStatements_alt_0_OneAndAgain_addedNodeNames = new String[] { "b" };
		private static String[] create_MultipleStatements_alt_0_OneAndAgain_addedEdgeNames = new String[] {  };

		public void MultipleStatements_alt_0_OneAndAgain_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)MultipleStatements_alt_0_OneAndAgain_SubNums.@_subpattern0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern1 = curMatch.EmbeddedGraphs[(int)MultipleStatements_alt_0_OneAndAgain_SubNums.@_subpattern1];
			Pattern_Statement.Instance.Statement_Delete(graph, subpattern__subpattern0);
			Pattern_MultipleStatements.Instance.MultipleStatements_Delete(graph, subpattern__subpattern1);
		}

		public void MultipleStatements_alt_0_NoStatementLeft_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleStatements_alt_0_NoStatementLeft_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleStatements_alt_0_NoStatementLeft_addedEdgeNames );
		}
		private static String[] create_MultipleStatements_alt_0_NoStatementLeft_addedNodeNames = new String[] { "b" };
		private static String[] create_MultipleStatements_alt_0_NoStatementLeft_addedEdgeNames = new String[] {  };

		public void MultipleStatements_alt_0_NoStatementLeft_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
		}

		static Pattern_MultipleStatements() {
		}
	}

	public class Pattern_Statement : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Statement instance = null;
		public static Pattern_Statement Instance { get { if (instance==null) { instance = new Pattern_Statement(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] Statement_node_b_AllowedTypes = null;
		public static bool[] Statement_node_b_IsAllowedType = null;
		public enum Statement_NodeNums { @b, };
		public enum Statement_EdgeNums { };
		public enum Statement_VariableNums { };
		public enum Statement_SubNums { };
		public enum Statement_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_Statement;

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
		public enum Statement_alt_0_Assignment_SubNums { @_subpattern0, };
		public enum Statement_alt_0_Assignment_AltNums { };


		GRGEN_LGSP.PatternGraph Statement_alt_0_Assignment;

		public static GRGEN_LIBGR.NodeType[] Statement_alt_0_Call_node_e_AllowedTypes = null;
		public static bool[] Statement_alt_0_Call_node_e_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Call_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] Statement_alt_0_Call_edge__edge1_AllowedTypes = null;
		public static bool[] Statement_alt_0_Call_edge__edge0_IsAllowedType = null;
		public static bool[] Statement_alt_0_Call_edge__edge1_IsAllowedType = null;
		public enum Statement_alt_0_Call_NodeNums { @b, @e, };
		public enum Statement_alt_0_Call_EdgeNums { @_edge0, @_edge1, };
		public enum Statement_alt_0_Call_VariableNums { };
		public enum Statement_alt_0_Call_SubNums { @_subpattern0, };
		public enum Statement_alt_0_Call_AltNums { };


		GRGEN_LGSP.PatternGraph Statement_alt_0_Call;

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


		GRGEN_LGSP.PatternGraph Statement_alt_0_Return;


#if INITIAL_WARMUP
		public Pattern_Statement()
#else
		private Pattern_Statement()
#endif
		{
			name = "Statement";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "Statement_node_b", };
		}
		public override void initialize()
		{
			bool[,] Statement_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Statement_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode Statement_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "Statement_node_b", "b", Statement_node_b_AllowedTypes, Statement_node_b_IsAllowedType, 5.5F, 0);
			bool[,] Statement_alt_0_Assignment_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Statement_alt_0_Assignment_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode Statement_alt_0_Assignment_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "Statement_alt_0_Assignment_node_e", "e", Statement_alt_0_Assignment_node_e_AllowedTypes, Statement_alt_0_Assignment_node_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Assignment_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "Statement_alt_0_Assignment_edge__edge0", "_edge0", Statement_alt_0_Assignment_edge__edge0_AllowedTypes, Statement_alt_0_Assignment_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Assignment_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@writesTo, "Statement_alt_0_Assignment_edge__edge1", "_edge1", Statement_alt_0_Assignment_edge__edge1_AllowedTypes, Statement_alt_0_Assignment_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding Statement_alt_0_Assignment__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ExpressionPattern.Instance, new GRGEN_LGSP.PatternElement[] { Statement_alt_0_Assignment_node_e });
			Statement_alt_0_Assignment = new GRGEN_LGSP.PatternGraph(
				"Assignment",
				"Statement_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Statement_node_b, Statement_alt_0_Assignment_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { Statement_alt_0_Assignment_edge__edge0, Statement_alt_0_Assignment_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Statement_alt_0_Assignment__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
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
			GRGEN_LGSP.PatternNode Statement_alt_0_Call_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "Statement_alt_0_Call_node_e", "e", Statement_alt_0_Call_node_e_AllowedTypes, Statement_alt_0_Call_node_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Call_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "Statement_alt_0_Call_edge__edge0", "_edge0", Statement_alt_0_Call_edge__edge0_AllowedTypes, Statement_alt_0_Call_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Call_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@calls, "Statement_alt_0_Call_edge__edge1", "_edge1", Statement_alt_0_Call_edge__edge1_AllowedTypes, Statement_alt_0_Call_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding Statement_alt_0_Call__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_MultipleExpressions.Instance, new GRGEN_LGSP.PatternElement[] { Statement_alt_0_Call_node_e });
			Statement_alt_0_Call = new GRGEN_LGSP.PatternGraph(
				"Call",
				"Statement_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { Statement_node_b, Statement_alt_0_Call_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { Statement_alt_0_Call_edge__edge0, Statement_alt_0_Call_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Statement_alt_0_Call__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
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
			GRGEN_LGSP.PatternNode Statement_alt_0_Return_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "Statement_alt_0_Return_node_e", "e", Statement_alt_0_Return_node_e_AllowedTypes, Statement_alt_0_Return_node_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Return_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "Statement_alt_0_Return_edge__edge0", "_edge0", Statement_alt_0_Return_edge__edge0_AllowedTypes, Statement_alt_0_Return_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Statement_alt_0_Return_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@uses, "Statement_alt_0_Return_edge__edge1", "_edge1", Statement_alt_0_Return_edge__edge1_AllowedTypes, Statement_alt_0_Return_edge__edge1_IsAllowedType, 5.5F, -1);
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

			Statement_node_b.PointOfDefinition = null;
			Statement_alt_0_Assignment_node_e.PointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Assignment_edge__edge0.PointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Assignment_edge__edge1.PointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Assignment__subpattern0.PointOfDefinition = Statement_alt_0_Assignment;
			Statement_alt_0_Call_node_e.PointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Call_edge__edge0.PointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Call_edge__edge1.PointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Call__subpattern0.PointOfDefinition = Statement_alt_0_Call;
			Statement_alt_0_Return_node_e.PointOfDefinition = Statement_alt_0_Return;
			Statement_alt_0_Return_edge__edge0.PointOfDefinition = Statement_alt_0_Return;
			Statement_alt_0_Return_edge__edge1.PointOfDefinition = Statement_alt_0_Return;

			patternGraph = pat_Statement;
		}


		public void Statement_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_b)
		{
			graph.SettingAddedNodeNames( create_Statement_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Statement_addedEdgeNames );
		}
		private static String[] create_Statement_addedNodeNames = new String[] {  };
		private static String[] create_Statement_addedEdgeNames = new String[] {  };

		public void Statement_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)Statement_AltNums.@alt_0 + 0];
			Statement_alt_0_Delete(graph, alternative_alt_0);
		}

		public void Statement_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == Statement_alt_0_Assignment) {
				Statement_alt_0_Assignment_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == Statement_alt_0_Call) {
				Statement_alt_0_Call_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == Statement_alt_0_Return) {
				Statement_alt_0_Return_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void Statement_alt_0_Assignment_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Statement_alt_0_Assignment_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			@Expression node_e = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Statement_alt_0_Assignment_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_b, node_e);
			Pattern_ExpressionPattern.Instance.ExpressionPattern_Create(graph, node_e);
		}
		private static String[] create_Statement_alt_0_Assignment_addedNodeNames = new String[] { "b", "e" };
		private static String[] create_Statement_alt_0_Assignment_addedEdgeNames = new String[] { "_edge0", "_edge1" };

		public void Statement_alt_0_Assignment_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_e = curMatch.Nodes[(int)Statement_alt_0_Assignment_NodeNums.@e];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)Statement_alt_0_Assignment_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch.Edges[(int)Statement_alt_0_Assignment_EdgeNums.@_edge1];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)Statement_alt_0_Assignment_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_e);
			graph.Remove(node_e);
			Pattern_ExpressionPattern.Instance.ExpressionPattern_Delete(graph, subpattern__subpattern0);
		}

		public void Statement_alt_0_Call_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Statement_alt_0_Call_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			@Expression node_e = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Statement_alt_0_Call_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_b, node_e);
			Pattern_MultipleExpressions.Instance.MultipleExpressions_Create(graph, node_e);
		}
		private static String[] create_Statement_alt_0_Call_addedNodeNames = new String[] { "b", "e" };
		private static String[] create_Statement_alt_0_Call_addedEdgeNames = new String[] { "_edge0", "_edge1" };

		public void Statement_alt_0_Call_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_e = curMatch.Nodes[(int)Statement_alt_0_Call_NodeNums.@e];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)Statement_alt_0_Call_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch.Edges[(int)Statement_alt_0_Call_EdgeNums.@_edge1];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)Statement_alt_0_Call_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_e);
			graph.Remove(node_e);
			Pattern_MultipleExpressions.Instance.MultipleExpressions_Delete(graph, subpattern__subpattern0);
		}

		public void Statement_alt_0_Return_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Statement_alt_0_Return_addedNodeNames );
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			@Expression node_e = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Statement_alt_0_Return_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_b, node_e);
		}
		private static String[] create_Statement_alt_0_Return_addedNodeNames = new String[] { "b", "e" };
		private static String[] create_Statement_alt_0_Return_addedEdgeNames = new String[] { "_edge0", "_edge1" };

		public void Statement_alt_0_Return_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_e = curMatch.Nodes[(int)Statement_alt_0_Return_NodeNums.@e];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)Statement_alt_0_Return_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch.Edges[(int)Statement_alt_0_Return_EdgeNums.@_edge1];
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_e);
			graph.Remove(node_e);
		}

		static Pattern_Statement() {
		}
	}

	public class Pattern_MultipleExpressions : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_MultipleExpressions instance = null;
		public static Pattern_MultipleExpressions Instance { get { if (instance==null) { instance = new Pattern_MultipleExpressions(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] MultipleExpressions_node_e_AllowedTypes = null;
		public static bool[] MultipleExpressions_node_e_IsAllowedType = null;
		public enum MultipleExpressions_NodeNums { @e, };
		public enum MultipleExpressions_EdgeNums { };
		public enum MultipleExpressions_VariableNums { };
		public enum MultipleExpressions_SubNums { };
		public enum MultipleExpressions_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_MultipleExpressions;

		public enum MultipleExpressions_alt_0_CaseNums { @OneAndAgain, @NoExpressionLeft, };
		public enum MultipleExpressions_alt_0_OneAndAgain_NodeNums { @e, };
		public enum MultipleExpressions_alt_0_OneAndAgain_EdgeNums { };
		public enum MultipleExpressions_alt_0_OneAndAgain_VariableNums { };
		public enum MultipleExpressions_alt_0_OneAndAgain_SubNums { @_subpattern0, @_subpattern1, };
		public enum MultipleExpressions_alt_0_OneAndAgain_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleExpressions_alt_0_OneAndAgain;

		public enum MultipleExpressions_alt_0_NoExpressionLeft_NodeNums { @e, };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_EdgeNums { };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_VariableNums { };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_SubNums { };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_AltNums { };


		GRGEN_LGSP.PatternGraph MultipleExpressions_alt_0_NoExpressionLeft;

		public static GRGEN_LIBGR.NodeType[] MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub_AllowedTypes = null;
		public static bool[] MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0_IsAllowedType = null;
		public enum MultipleExpressions_alt_0_NoExpressionLeft_neg_0_NodeNums { @e, @sub, };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_neg_0_EdgeNums { @_edge0, };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_neg_0_VariableNums { };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_neg_0_SubNums { };
		public enum MultipleExpressions_alt_0_NoExpressionLeft_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph MultipleExpressions_alt_0_NoExpressionLeft_neg_0;


#if INITIAL_WARMUP
		public Pattern_MultipleExpressions()
#else
		private Pattern_MultipleExpressions()
#endif
		{
			name = "MultipleExpressions";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Expression.typeVar, };
			inputNames = new string[] { "MultipleExpressions_node_e", };
		}
		public override void initialize()
		{
			bool[,] MultipleExpressions_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleExpressions_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode MultipleExpressions_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "MultipleExpressions_node_e", "e", MultipleExpressions_node_e_AllowedTypes, MultipleExpressions_node_e_IsAllowedType, 5.5F, 0);
			bool[,] MultipleExpressions_alt_0_OneAndAgain_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleExpressions_alt_0_OneAndAgain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternGraphEmbedding MultipleExpressions_alt_0_OneAndAgain__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_ExpressionPattern.Instance, new GRGEN_LGSP.PatternElement[] { MultipleExpressions_node_e });
			GRGEN_LGSP.PatternGraphEmbedding MultipleExpressions_alt_0_OneAndAgain__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_MultipleExpressions.Instance, new GRGEN_LGSP.PatternElement[] { MultipleExpressions_node_e });
			MultipleExpressions_alt_0_OneAndAgain = new GRGEN_LGSP.PatternGraph(
				"OneAndAgain",
				"MultipleExpressions_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleExpressions_node_e }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { MultipleExpressions_alt_0_OneAndAgain__subpattern0, MultipleExpressions_alt_0_OneAndAgain__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleExpressions_alt_0_OneAndAgain_isNodeHomomorphicGlobal,
				MultipleExpressions_alt_0_OneAndAgain_isEdgeHomomorphicGlobal
			);

			bool[,] MultipleExpressions_alt_0_NoExpressionLeft_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] MultipleExpressions_alt_0_NoExpressionLeft_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] MultipleExpressions_alt_0_NoExpressionLeft_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleExpressions_alt_0_NoExpressionLeft_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub", "sub", MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub_AllowedTypes, MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0", "_edge0", MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0_AllowedTypes, MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			MultipleExpressions_alt_0_NoExpressionLeft_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"MultipleExpressions_alt_0_NoExpressionLeft_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleExpressions_node_e, MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				MultipleExpressions_alt_0_NoExpressionLeft_neg_0_isNodeHomomorphicGlobal,
				MultipleExpressions_alt_0_NoExpressionLeft_neg_0_isEdgeHomomorphicGlobal
			);
			MultipleExpressions_alt_0_NoExpressionLeft_neg_0.edgeToSourceNode.Add(MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0, MultipleExpressions_node_e);
			MultipleExpressions_alt_0_NoExpressionLeft_neg_0.edgeToTargetNode.Add(MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0, MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub);

			MultipleExpressions_alt_0_NoExpressionLeft = new GRGEN_LGSP.PatternGraph(
				"NoExpressionLeft",
				"MultipleExpressions_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleExpressions_node_e }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { MultipleExpressions_alt_0_NoExpressionLeft_neg_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleExpressions_alt_0_NoExpressionLeft_isNodeHomomorphicGlobal,
				MultipleExpressions_alt_0_NoExpressionLeft_isEdgeHomomorphicGlobal
			);
			MultipleExpressions_alt_0_NoExpressionLeft_neg_0.embeddingGraph = MultipleExpressions_alt_0_NoExpressionLeft;

			GRGEN_LGSP.Alternative MultipleExpressions_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "MultipleExpressions_", new GRGEN_LGSP.PatternGraph[] { MultipleExpressions_alt_0_OneAndAgain, MultipleExpressions_alt_0_NoExpressionLeft } );

			pat_MultipleExpressions = new GRGEN_LGSP.PatternGraph(
				"MultipleExpressions",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleExpressions_node_e }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { MultipleExpressions_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				MultipleExpressions_isNodeHomomorphicGlobal,
				MultipleExpressions_isEdgeHomomorphicGlobal
			);
			MultipleExpressions_alt_0_OneAndAgain.embeddingGraph = pat_MultipleExpressions;
			MultipleExpressions_alt_0_NoExpressionLeft.embeddingGraph = pat_MultipleExpressions;

			MultipleExpressions_node_e.PointOfDefinition = null;
			MultipleExpressions_alt_0_OneAndAgain__subpattern0.PointOfDefinition = MultipleExpressions_alt_0_OneAndAgain;
			MultipleExpressions_alt_0_OneAndAgain__subpattern1.PointOfDefinition = MultipleExpressions_alt_0_OneAndAgain;
			MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub.PointOfDefinition = MultipleExpressions_alt_0_NoExpressionLeft_neg_0;
			MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0.PointOfDefinition = MultipleExpressions_alt_0_NoExpressionLeft_neg_0;

			patternGraph = pat_MultipleExpressions;
		}


		public void MultipleExpressions_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_e)
		{
			graph.SettingAddedNodeNames( create_MultipleExpressions_addedNodeNames );
			graph.SettingAddedEdgeNames( create_MultipleExpressions_addedEdgeNames );
		}
		private static String[] create_MultipleExpressions_addedNodeNames = new String[] {  };
		private static String[] create_MultipleExpressions_addedEdgeNames = new String[] {  };

		public void MultipleExpressions_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleExpressions_AltNums.@alt_0 + 0];
			MultipleExpressions_alt_0_Delete(graph, alternative_alt_0);
		}

		public void MultipleExpressions_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleExpressions_alt_0_OneAndAgain) {
				MultipleExpressions_alt_0_OneAndAgain_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleExpressions_alt_0_NoExpressionLeft) {
				MultipleExpressions_alt_0_NoExpressionLeft_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleExpressions_alt_0_OneAndAgain_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleExpressions_alt_0_OneAndAgain_addedNodeNames );
			@Expression node_e = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleExpressions_alt_0_OneAndAgain_addedEdgeNames );
			Pattern_ExpressionPattern.Instance.ExpressionPattern_Create(graph, node_e);
			Pattern_MultipleExpressions.Instance.MultipleExpressions_Create(graph, node_e);
		}
		private static String[] create_MultipleExpressions_alt_0_OneAndAgain_addedNodeNames = new String[] { "e" };
		private static String[] create_MultipleExpressions_alt_0_OneAndAgain_addedEdgeNames = new String[] {  };

		public void MultipleExpressions_alt_0_OneAndAgain_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)MultipleExpressions_alt_0_OneAndAgain_SubNums.@_subpattern0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern1 = curMatch.EmbeddedGraphs[(int)MultipleExpressions_alt_0_OneAndAgain_SubNums.@_subpattern1];
			Pattern_ExpressionPattern.Instance.ExpressionPattern_Delete(graph, subpattern__subpattern0);
			Pattern_MultipleExpressions.Instance.MultipleExpressions_Delete(graph, subpattern__subpattern1);
		}

		public void MultipleExpressions_alt_0_NoExpressionLeft_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleExpressions_alt_0_NoExpressionLeft_addedNodeNames );
			@Expression node_e = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleExpressions_alt_0_NoExpressionLeft_addedEdgeNames );
		}
		private static String[] create_MultipleExpressions_alt_0_NoExpressionLeft_addedNodeNames = new String[] { "e" };
		private static String[] create_MultipleExpressions_alt_0_NoExpressionLeft_addedEdgeNames = new String[] {  };

		public void MultipleExpressions_alt_0_NoExpressionLeft_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
		}

		static Pattern_MultipleExpressions() {
		}
	}

	public class Pattern_ExpressionPattern : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_ExpressionPattern instance = null;
		public static Pattern_ExpressionPattern Instance { get { if (instance==null) { instance = new Pattern_ExpressionPattern(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_ExpressionPattern;

		public enum ExpressionPattern_alt_0_CaseNums { @Call, @Use, };
		public static GRGEN_LIBGR.EdgeType[] ExpressionPattern_alt_0_Call_edge__edge0_AllowedTypes = null;
		public static bool[] ExpressionPattern_alt_0_Call_edge__edge0_IsAllowedType = null;
		public enum ExpressionPattern_alt_0_Call_NodeNums { @sub, };
		public enum ExpressionPattern_alt_0_Call_EdgeNums { @_edge0, };
		public enum ExpressionPattern_alt_0_Call_VariableNums { };
		public enum ExpressionPattern_alt_0_Call_SubNums { @_subpattern0, };
		public enum ExpressionPattern_alt_0_Call_AltNums { };


		GRGEN_LGSP.PatternGraph ExpressionPattern_alt_0_Call;

		public static GRGEN_LIBGR.EdgeType[] ExpressionPattern_alt_0_Use_edge__edge0_AllowedTypes = null;
		public static bool[] ExpressionPattern_alt_0_Use_edge__edge0_IsAllowedType = null;
		public enum ExpressionPattern_alt_0_Use_NodeNums { @sub, };
		public enum ExpressionPattern_alt_0_Use_EdgeNums { @_edge0, };
		public enum ExpressionPattern_alt_0_Use_VariableNums { };
		public enum ExpressionPattern_alt_0_Use_SubNums { };
		public enum ExpressionPattern_alt_0_Use_AltNums { };


		GRGEN_LGSP.PatternGraph ExpressionPattern_alt_0_Use;


#if INITIAL_WARMUP
		public Pattern_ExpressionPattern()
#else
		private Pattern_ExpressionPattern()
#endif
		{
			name = "ExpressionPattern";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Expression.typeVar, };
			inputNames = new string[] { "ExpressionPattern_node_e", };
		}
		public override void initialize()
		{
			bool[,] ExpressionPattern_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ExpressionPattern_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode ExpressionPattern_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "ExpressionPattern_node_e", "e", ExpressionPattern_node_e_AllowedTypes, ExpressionPattern_node_e_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode ExpressionPattern_node_sub = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "ExpressionPattern_node_sub", "sub", ExpressionPattern_node_sub_AllowedTypes, ExpressionPattern_node_sub_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ExpressionPattern_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "ExpressionPattern_edge__edge0", "_edge0", ExpressionPattern_edge__edge0_AllowedTypes, ExpressionPattern_edge__edge0_IsAllowedType, 5.5F, -1);
			bool[,] ExpressionPattern_alt_0_Call_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ExpressionPattern_alt_0_Call_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge ExpressionPattern_alt_0_Call_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@calls, "ExpressionPattern_alt_0_Call_edge__edge0", "_edge0", ExpressionPattern_alt_0_Call_edge__edge0_AllowedTypes, ExpressionPattern_alt_0_Call_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding ExpressionPattern_alt_0_Call__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_MultipleExpressions.Instance, new GRGEN_LGSP.PatternElement[] { ExpressionPattern_node_sub });
			ExpressionPattern_alt_0_Call = new GRGEN_LGSP.PatternGraph(
				"Call",
				"ExpressionPattern_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { ExpressionPattern_node_sub }, 
				new GRGEN_LGSP.PatternEdge[] { ExpressionPattern_alt_0_Call_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { ExpressionPattern_alt_0_Call__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
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
			GRGEN_LGSP.PatternEdge ExpressionPattern_alt_0_Use_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@uses, "ExpressionPattern_alt_0_Use_edge__edge0", "_edge0", ExpressionPattern_alt_0_Use_edge__edge0_AllowedTypes, ExpressionPattern_alt_0_Use_edge__edge0_IsAllowedType, 5.5F, -1);
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

			ExpressionPattern_node_e.PointOfDefinition = null;
			ExpressionPattern_node_sub.PointOfDefinition = pat_ExpressionPattern;
			ExpressionPattern_edge__edge0.PointOfDefinition = pat_ExpressionPattern;
			ExpressionPattern_alt_0_Call_edge__edge0.PointOfDefinition = ExpressionPattern_alt_0_Call;
			ExpressionPattern_alt_0_Call__subpattern0.PointOfDefinition = ExpressionPattern_alt_0_Call;
			ExpressionPattern_alt_0_Use_edge__edge0.PointOfDefinition = ExpressionPattern_alt_0_Use;

			patternGraph = pat_ExpressionPattern;
		}


		public void ExpressionPattern_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_e)
		{
			graph.SettingAddedNodeNames( create_ExpressionPattern_addedNodeNames );
			@Expression node_sub = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ExpressionPattern_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_e, node_sub);
		}
		private static String[] create_ExpressionPattern_addedNodeNames = new String[] { "sub" };
		private static String[] create_ExpressionPattern_addedEdgeNames = new String[] { "_edge0" };

		public void ExpressionPattern_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_sub = curMatch.Nodes[(int)ExpressionPattern_NodeNums.@sub];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)ExpressionPattern_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)ExpressionPattern_AltNums.@alt_0 + 0];
			ExpressionPattern_alt_0_Delete(graph, alternative_alt_0);
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_sub);
			graph.Remove(node_sub);
		}

		public void ExpressionPattern_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == ExpressionPattern_alt_0_Call) {
				ExpressionPattern_alt_0_Call_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == ExpressionPattern_alt_0_Use) {
				ExpressionPattern_alt_0_Use_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ExpressionPattern_alt_0_Call_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ExpressionPattern_alt_0_Call_addedNodeNames );
			@Expression node_sub = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ExpressionPattern_alt_0_Call_addedEdgeNames );
			Pattern_MultipleExpressions.Instance.MultipleExpressions_Create(graph, node_sub);
		}
		private static String[] create_ExpressionPattern_alt_0_Call_addedNodeNames = new String[] { "sub" };
		private static String[] create_ExpressionPattern_alt_0_Call_addedEdgeNames = new String[] { "_edge0" };

		public void ExpressionPattern_alt_0_Call_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)ExpressionPattern_alt_0_Call_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)ExpressionPattern_alt_0_Call_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			Pattern_MultipleExpressions.Instance.MultipleExpressions_Delete(graph, subpattern__subpattern0);
		}

		public void ExpressionPattern_alt_0_Use_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ExpressionPattern_alt_0_Use_addedNodeNames );
			@Expression node_sub = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ExpressionPattern_alt_0_Use_addedEdgeNames );
		}
		private static String[] create_ExpressionPattern_alt_0_Use_addedNodeNames = new String[] { "sub" };
		private static String[] create_ExpressionPattern_alt_0_Use_addedEdgeNames = new String[] { "_edge0" };

		public void ExpressionPattern_alt_0_Use_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)ExpressionPattern_alt_0_Use_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
		}

		static Pattern_ExpressionPattern() {
		}
	}

	public class Pattern_MultipleBodies : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_MultipleBodies instance = null;
		public static Pattern_MultipleBodies Instance { get { if (instance==null) { instance = new Pattern_MultipleBodies(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] MultipleBodies_node_m5_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] MultipleBodies_node_c1_AllowedTypes = null;
		public static bool[] MultipleBodies_node_m5_IsAllowedType = null;
		public static bool[] MultipleBodies_node_c1_IsAllowedType = null;
		public enum MultipleBodies_NodeNums { @m5, @c1, };
		public enum MultipleBodies_EdgeNums { };
		public enum MultipleBodies_VariableNums { };
		public enum MultipleBodies_SubNums { };
		public enum MultipleBodies_AltNums { @alt_0, };



		GRGEN_LGSP.PatternGraph pat_MultipleBodies;

		public enum MultipleBodies_alt_0_CaseNums { @Rek, @Empty, };
		public enum MultipleBodies_alt_0_Rek_NodeNums { @m5, @c1, };
		public enum MultipleBodies_alt_0_Rek_EdgeNums { };
		public enum MultipleBodies_alt_0_Rek_VariableNums { };
		public enum MultipleBodies_alt_0_Rek_SubNums { @b, @mb, };
		public enum MultipleBodies_alt_0_Rek_AltNums { };



		GRGEN_LGSP.PatternGraph MultipleBodies_alt_0_Rek;

		public enum MultipleBodies_alt_0_Empty_NodeNums { @m5, @c1, };
		public enum MultipleBodies_alt_0_Empty_EdgeNums { };
		public enum MultipleBodies_alt_0_Empty_VariableNums { };
		public enum MultipleBodies_alt_0_Empty_SubNums { };
		public enum MultipleBodies_alt_0_Empty_AltNums { };



		GRGEN_LGSP.PatternGraph MultipleBodies_alt_0_Empty;

		public enum MultipleBodies_alt_0_Empty_neg_0_NodeNums { @m5, @c1, };
		public enum MultipleBodies_alt_0_Empty_neg_0_EdgeNums { };
		public enum MultipleBodies_alt_0_Empty_neg_0_VariableNums { };
		public enum MultipleBodies_alt_0_Empty_neg_0_SubNums { @_subpattern0, };
		public enum MultipleBodies_alt_0_Empty_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph MultipleBodies_alt_0_Empty_neg_0;


#if INITIAL_WARMUP
		public Pattern_MultipleBodies()
#else
		private Pattern_MultipleBodies()
#endif
		{
			name = "MultipleBodies";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_MethodSignature.typeVar, NodeType_Class.typeVar, };
			inputNames = new string[] { "MultipleBodies_node_m5", "MultipleBodies_node_c1", };
		}
		public override void initialize()
		{
			bool[,] MultipleBodies_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleBodies_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode MultipleBodies_node_m5 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodSignature, "MultipleBodies_node_m5", "m5", MultipleBodies_node_m5_AllowedTypes, MultipleBodies_node_m5_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode MultipleBodies_node_c1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "MultipleBodies_node_c1", "c1", MultipleBodies_node_c1_AllowedTypes, MultipleBodies_node_c1_IsAllowedType, 5.5F, 1);
			bool[,] MultipleBodies_alt_0_Rek_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleBodies_alt_0_Rek_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternGraphEmbedding MultipleBodies_alt_0_Rek_b = new GRGEN_LGSP.PatternGraphEmbedding("b", Pattern_Body.Instance, new GRGEN_LGSP.PatternElement[] { MultipleBodies_node_m5, MultipleBodies_node_c1 });
			GRGEN_LGSP.PatternGraphEmbedding MultipleBodies_alt_0_Rek_mb = new GRGEN_LGSP.PatternGraphEmbedding("mb", Pattern_MultipleBodies.Instance, new GRGEN_LGSP.PatternElement[] { MultipleBodies_node_m5, MultipleBodies_node_c1 });
			MultipleBodies_alt_0_Rek = new GRGEN_LGSP.PatternGraph(
				"Rek",
				"MultipleBodies_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleBodies_node_m5, MultipleBodies_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { MultipleBodies_alt_0_Rek_b, MultipleBodies_alt_0_Rek_mb }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, true, },
					{ true, true, },
				},
				new bool[0, 0] ,
				MultipleBodies_alt_0_Rek_isNodeHomomorphicGlobal,
				MultipleBodies_alt_0_Rek_isEdgeHomomorphicGlobal
			);

			bool[,] MultipleBodies_alt_0_Empty_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleBodies_alt_0_Empty_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] MultipleBodies_alt_0_Empty_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] MultipleBodies_alt_0_Empty_neg_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternGraphEmbedding MultipleBodies_alt_0_Empty_neg_0__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_Body.Instance, new GRGEN_LGSP.PatternElement[] { MultipleBodies_node_m5, MultipleBodies_node_c1 });
			MultipleBodies_alt_0_Empty_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"MultipleBodies_alt_0_Empty_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleBodies_node_m5, MultipleBodies_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { MultipleBodies_alt_0_Empty_neg_0__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, true, },
					{ true, true, },
				},
				new bool[0, 0] ,
				MultipleBodies_alt_0_Empty_neg_0_isNodeHomomorphicGlobal,
				MultipleBodies_alt_0_Empty_neg_0_isEdgeHomomorphicGlobal
			);

			MultipleBodies_alt_0_Empty = new GRGEN_LGSP.PatternGraph(
				"Empty",
				"MultipleBodies_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleBodies_node_m5, MultipleBodies_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { MultipleBodies_alt_0_Empty_neg_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, true, },
					{ true, true, },
				},
				new bool[0, 0] ,
				MultipleBodies_alt_0_Empty_isNodeHomomorphicGlobal,
				MultipleBodies_alt_0_Empty_isEdgeHomomorphicGlobal
			);
			MultipleBodies_alt_0_Empty_neg_0.embeddingGraph = MultipleBodies_alt_0_Empty;

			GRGEN_LGSP.Alternative MultipleBodies_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "MultipleBodies_", new GRGEN_LGSP.PatternGraph[] { MultipleBodies_alt_0_Rek, MultipleBodies_alt_0_Empty } );

			pat_MultipleBodies = new GRGEN_LGSP.PatternGraph(
				"MultipleBodies",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { MultipleBodies_node_m5, MultipleBodies_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { MultipleBodies_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				MultipleBodies_isNodeHomomorphicGlobal,
				MultipleBodies_isEdgeHomomorphicGlobal
			);
			MultipleBodies_alt_0_Rek.embeddingGraph = pat_MultipleBodies;
			MultipleBodies_alt_0_Empty.embeddingGraph = pat_MultipleBodies;

			MultipleBodies_node_m5.PointOfDefinition = null;
			MultipleBodies_node_c1.PointOfDefinition = null;
			MultipleBodies_alt_0_Rek_b.PointOfDefinition = MultipleBodies_alt_0_Rek;
			MultipleBodies_alt_0_Rek_mb.PointOfDefinition = MultipleBodies_alt_0_Rek;
			MultipleBodies_alt_0_Empty_neg_0__subpattern0.PointOfDefinition = MultipleBodies_alt_0_Empty_neg_0;

			patternGraph = pat_MultipleBodies;
		}


		public void MultipleBodies_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleBodies_AltNums.@alt_0 + 0];
			graph.SettingAddedNodeNames( MultipleBodies_addedNodeNames );
			MultipleBodies_alt_0_Modify(graph, alternative_alt_0);
			graph.SettingAddedEdgeNames( MultipleBodies_addedEdgeNames );
		}
		private static String[] MultipleBodies_addedNodeNames = new String[] {  };
		private static String[] MultipleBodies_addedEdgeNames = new String[] {  };

		public void MultipleBodies_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleBodies_AltNums.@alt_0 + 0];
			graph.SettingAddedNodeNames( MultipleBodies_addedNodeNames );
			MultipleBodies_alt_0_ModifyNoReuse(graph, alternative_alt_0);
			graph.SettingAddedEdgeNames( MultipleBodies_addedEdgeNames );
		}

		public void MultipleBodies_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_m5, GRGEN_LGSP.LGSPNode node_c1)
		{
			graph.SettingAddedNodeNames( create_MultipleBodies_addedNodeNames );
			graph.SettingAddedEdgeNames( create_MultipleBodies_addedEdgeNames );
		}
		private static String[] create_MultipleBodies_addedNodeNames = new String[] {  };
		private static String[] create_MultipleBodies_addedEdgeNames = new String[] {  };

		public void MultipleBodies_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch alternative_alt_0 = curMatch.EmbeddedGraphs[(int)MultipleBodies_AltNums.@alt_0 + 0];
			MultipleBodies_alt_0_Delete(graph, alternative_alt_0);
		}

		public void MultipleBodies_alt_0_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleBodies_alt_0_Rek) {
				MultipleBodies_alt_0_Rek_Modify(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleBodies_alt_0_Empty) {
				MultipleBodies_alt_0_Empty_Modify(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleBodies_alt_0_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleBodies_alt_0_Rek) {
				MultipleBodies_alt_0_Rek_ModifyNoReuse(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleBodies_alt_0_Empty) {
				MultipleBodies_alt_0_Empty_ModifyNoReuse(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleBodies_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			if(curMatch.patternGraph == MultipleBodies_alt_0_Rek) {
				MultipleBodies_alt_0_Rek_Delete(graph, curMatch);
				return;
			}
			else if(curMatch.patternGraph == MultipleBodies_alt_0_Empty) {
				MultipleBodies_alt_0_Empty_Delete(graph, curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void MultipleBodies_alt_0_Rek_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern_b = curMatch.EmbeddedGraphs[(int)MultipleBodies_alt_0_Rek_SubNums.@b];
			GRGEN_LGSP.LGSPMatch subpattern_mb = curMatch.EmbeddedGraphs[(int)MultipleBodies_alt_0_Rek_SubNums.@mb];
			graph.SettingAddedNodeNames( MultipleBodies_alt_0_Rek_addedNodeNames );
			Pattern_MultipleBodies.Instance.MultipleBodies_Modify(graph, subpattern_mb);
			Pattern_Body.Instance.Body_Modify(graph, subpattern_b);
			graph.SettingAddedEdgeNames( MultipleBodies_alt_0_Rek_addedEdgeNames );
		}
		private static String[] MultipleBodies_alt_0_Rek_addedNodeNames = new String[] {  };
		private static String[] MultipleBodies_alt_0_Rek_addedEdgeNames = new String[] {  };

		public void MultipleBodies_alt_0_Rek_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern_b = curMatch.EmbeddedGraphs[(int)MultipleBodies_alt_0_Rek_SubNums.@b];
			GRGEN_LGSP.LGSPMatch subpattern_mb = curMatch.EmbeddedGraphs[(int)MultipleBodies_alt_0_Rek_SubNums.@mb];
			graph.SettingAddedNodeNames( MultipleBodies_alt_0_Rek_addedNodeNames );
			Pattern_MultipleBodies.Instance.MultipleBodies_Modify(graph, subpattern_mb);
			Pattern_Body.Instance.Body_Modify(graph, subpattern_b);
			graph.SettingAddedEdgeNames( MultipleBodies_alt_0_Rek_addedEdgeNames );
		}

		public void MultipleBodies_alt_0_Rek_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleBodies_alt_0_Rek_addedNodeNames );
			@MethodSignature node_m5 = @MethodSignature.CreateNode(graph);
			@Class node_c1 = @Class.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleBodies_alt_0_Rek_addedEdgeNames );
			Pattern_Body.Instance.Body_Create(graph, node_m5, node_c1);
			Pattern_MultipleBodies.Instance.MultipleBodies_Create(graph, node_m5, node_c1);
		}
		private static String[] create_MultipleBodies_alt_0_Rek_addedNodeNames = new String[] { "m5", "c1" };
		private static String[] create_MultipleBodies_alt_0_Rek_addedEdgeNames = new String[] {  };

		public void MultipleBodies_alt_0_Rek_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern_b = curMatch.EmbeddedGraphs[(int)MultipleBodies_alt_0_Rek_SubNums.@b];
			GRGEN_LGSP.LGSPMatch subpattern_mb = curMatch.EmbeddedGraphs[(int)MultipleBodies_alt_0_Rek_SubNums.@mb];
			Pattern_Body.Instance.Body_Delete(graph, subpattern_b);
			Pattern_MultipleBodies.Instance.MultipleBodies_Delete(graph, subpattern_mb);
		}

		public void MultipleBodies_alt_0_Empty_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( MultipleBodies_alt_0_Empty_addedNodeNames );
			graph.SettingAddedEdgeNames( MultipleBodies_alt_0_Empty_addedEdgeNames );
		}
		private static String[] MultipleBodies_alt_0_Empty_addedNodeNames = new String[] {  };
		private static String[] MultipleBodies_alt_0_Empty_addedEdgeNames = new String[] {  };

		public void MultipleBodies_alt_0_Empty_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( MultipleBodies_alt_0_Empty_addedNodeNames );
			graph.SettingAddedEdgeNames( MultipleBodies_alt_0_Empty_addedEdgeNames );
		}

		public void MultipleBodies_alt_0_Empty_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_MultipleBodies_alt_0_Empty_addedNodeNames );
			@MethodSignature node_m5 = @MethodSignature.CreateNode(graph);
			@Class node_c1 = @Class.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_MultipleBodies_alt_0_Empty_addedEdgeNames );
		}
		private static String[] create_MultipleBodies_alt_0_Empty_addedNodeNames = new String[] { "m5", "c1" };
		private static String[] create_MultipleBodies_alt_0_Empty_addedEdgeNames = new String[] {  };

		public void MultipleBodies_alt_0_Empty_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
		}

		static Pattern_MultipleBodies() {
		}
	}

	public class Pattern_Body : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_Body instance = null;
		public static Pattern_Body Instance { get { if (instance==null) { instance = new Pattern_Body(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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
		public enum Body_SubNums { @mp, @ms, };
		public enum Body_AltNums { };



		GRGEN_LGSP.PatternGraph pat_Body;


#if INITIAL_WARMUP
		public Pattern_Body()
#else
		private Pattern_Body()
#endif
		{
			name = "Body";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_MethodSignature.typeVar, NodeType_Class.typeVar, };
			inputNames = new string[] { "Body_node_m5", "Body_node_c1", };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode Body_node_c1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "Body_node_c1", "c1", Body_node_c1_AllowedTypes, Body_node_c1_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternNode Body_node_c2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "Body_node_c2", "c2", Body_node_c2_AllowedTypes, Body_node_c2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Body_node_b = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "Body_node_b", "b", Body_node_b_AllowedTypes, Body_node_b_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode Body_node_m5 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodSignature, "Body_node_m5", "m5", Body_node_m5_AllowedTypes, Body_node_m5_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternEdge Body_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "Body_edge__edge0", "_edge0", Body_edge__edge0_AllowedTypes, Body_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Body_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "Body_edge__edge1", "_edge1", Body_edge__edge1_AllowedTypes, Body_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge Body_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@bindsTo, "Body_edge__edge2", "_edge2", Body_edge__edge2_AllowedTypes, Body_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding Body_mp = new GRGEN_LGSP.PatternGraphEmbedding("mp", Pattern_MultipleParameters.Instance, new GRGEN_LGSP.PatternElement[] { Body_node_b });
			GRGEN_LGSP.PatternGraphEmbedding Body_ms = new GRGEN_LGSP.PatternGraphEmbedding("ms", Pattern_MultipleStatements.Instance, new GRGEN_LGSP.PatternElement[] { Body_node_b });
			pat_Body = new GRGEN_LGSP.PatternGraph(
				"Body",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { Body_node_c1, Body_node_c2, Body_node_b, Body_node_m5 }, 
				new GRGEN_LGSP.PatternEdge[] { Body_edge__edge0, Body_edge__edge1, Body_edge__edge2 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { Body_mp, Body_ms }, 
				new GRGEN_LGSP.Alternative[] {  }, 
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

			Body_node_c1.PointOfDefinition = null;
			Body_node_c2.PointOfDefinition = pat_Body;
			Body_node_b.PointOfDefinition = pat_Body;
			Body_node_m5.PointOfDefinition = null;
			Body_edge__edge0.PointOfDefinition = pat_Body;
			Body_edge__edge1.PointOfDefinition = pat_Body;
			Body_edge__edge2.PointOfDefinition = pat_Body;
			Body_mp.PointOfDefinition = pat_Body;
			Body_ms.PointOfDefinition = pat_Body;

			patternGraph = pat_Body;
		}


		public void Body_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch.Nodes[(int)Body_NodeNums.@b];
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch.Edges[(int)Body_EdgeNums.@_edge1];
			GRGEN_LGSP.LGSPEdge edge__edge2 = curMatch.Edges[(int)Body_EdgeNums.@_edge2];
			GRGEN_LGSP.LGSPMatch subpattern_mp = curMatch.EmbeddedGraphs[(int)Body_SubNums.@mp];
			GRGEN_LGSP.LGSPMatch subpattern_ms = curMatch.EmbeddedGraphs[(int)Body_SubNums.@ms];
			graph.SettingAddedNodeNames( Body_addedNodeNames );
			graph.SettingAddedEdgeNames( Body_addedEdgeNames );
			graph.Remove(edge__edge1);
			graph.Remove(edge__edge2);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_MultipleParameters.Instance.MultipleParameters_Delete(graph, subpattern_mp);
			Pattern_MultipleStatements.Instance.MultipleStatements_Delete(graph, subpattern_ms);
		}
		private static String[] Body_addedNodeNames = new String[] {  };
		private static String[] Body_addedEdgeNames = new String[] {  };

		public void Body_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_b = curMatch.Nodes[(int)Body_NodeNums.@b];
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch.Edges[(int)Body_EdgeNums.@_edge1];
			GRGEN_LGSP.LGSPEdge edge__edge2 = curMatch.Edges[(int)Body_EdgeNums.@_edge2];
			GRGEN_LGSP.LGSPMatch subpattern_mp = curMatch.EmbeddedGraphs[(int)Body_SubNums.@mp];
			GRGEN_LGSP.LGSPMatch subpattern_ms = curMatch.EmbeddedGraphs[(int)Body_SubNums.@ms];
			graph.SettingAddedNodeNames( Body_addedNodeNames );
			graph.SettingAddedEdgeNames( Body_addedEdgeNames );
			graph.Remove(edge__edge1);
			graph.Remove(edge__edge2);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_MultipleParameters.Instance.MultipleParameters_Delete(graph, subpattern_mp);
			Pattern_MultipleStatements.Instance.MultipleStatements_Delete(graph, subpattern_ms);
		}

		public void Body_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_m5, GRGEN_LGSP.LGSPNode node_c1)
		{
			graph.SettingAddedNodeNames( create_Body_addedNodeNames );
			@Class node_c2 = @Class.CreateNode(graph);
			@MethodBody node_b = @MethodBody.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Body_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c1, node_c2);
			@contains edge__edge1 = @contains.CreateEdge(graph, node_c2, node_b);
			@bindsTo edge__edge2 = @bindsTo.CreateEdge(graph, node_b, node_m5);
			Pattern_MultipleParameters.Instance.MultipleParameters_Create(graph, node_b);
			Pattern_MultipleStatements.Instance.MultipleStatements_Create(graph, node_b);
		}
		private static String[] create_Body_addedNodeNames = new String[] { "c2", "b" };
		private static String[] create_Body_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2" };

		public void Body_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c2 = curMatch.Nodes[(int)Body_NodeNums.@c2];
			GRGEN_LGSP.LGSPNode node_b = curMatch.Nodes[(int)Body_NodeNums.@b];
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch.Edges[(int)Body_EdgeNums.@_edge0];
			GRGEN_LGSP.LGSPEdge edge__edge1 = curMatch.Edges[(int)Body_EdgeNums.@_edge1];
			GRGEN_LGSP.LGSPEdge edge__edge2 = curMatch.Edges[(int)Body_EdgeNums.@_edge2];
			GRGEN_LGSP.LGSPMatch subpattern_mp = curMatch.EmbeddedGraphs[(int)Body_SubNums.@mp];
			GRGEN_LGSP.LGSPMatch subpattern_ms = curMatch.EmbeddedGraphs[(int)Body_SubNums.@ms];
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.Remove(edge__edge2);
			graph.RemoveEdges(node_c2);
			graph.Remove(node_c2);
			graph.RemoveEdges(node_b);
			graph.Remove(node_b);
			Pattern_MultipleParameters.Instance.MultipleParameters_Delete(graph, subpattern_mp);
			Pattern_MultipleStatements.Instance.MultipleStatements_Delete(graph, subpattern_ms);
		}

		static Pattern_Body() {
		}
	}

	public class Rule_createProgramGraphExample : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createProgramGraphExample instance = null;
		public static Rule_createProgramGraphExample Instance { get { if (instance==null) { instance = new Rule_createProgramGraphExample(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum createProgramGraphExample_NodeNums { };
		public enum createProgramGraphExample_EdgeNums { };
		public enum createProgramGraphExample_VariableNums { };
		public enum createProgramGraphExample_SubNums { };
		public enum createProgramGraphExample_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createProgramGraphExample;


#if INITIAL_WARMUP
		public Rule_createProgramGraphExample()
#else
		private Rule_createProgramGraphExample()
#endif
		{
			name = "createProgramGraphExample";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] createProgramGraphExample_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createProgramGraphExample_isEdgeHomomorphicGlobal = new bool[0, 0] ;
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
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createProgramGraphExample_isNodeHomomorphicGlobal,
				createProgramGraphExample_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createProgramGraphExample;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( createProgramGraphExample_addedNodeNames );
			@Class node_any = @Class.CreateNode(graph);
			@Class node_cell = @Class.CreateNode(graph);
			@Class node_recell = @Class.CreateNode(graph);
			@MethodSignature node_getS = @MethodSignature.CreateNode(graph);
			@MethodBody node_getB = @MethodBody.CreateNode(graph);
			@Variabel node_cts = @Variabel.CreateNode(graph);
			@Expression node_ex1 = @Expression.CreateNode(graph);
			@MethodSignature node_setS = @MethodSignature.CreateNode(graph);
			@MethodBody node_setB = @MethodBody.CreateNode(graph);
			@Constant node_n = @Constant.CreateNode(graph);
			@Expression node_ex2 = @Expression.CreateNode(graph);
			@Expression node_ex3 = @Expression.CreateNode(graph);
			@MethodBody node_setB2 = @MethodBody.CreateNode(graph);
			@Constant node_n2 = @Constant.CreateNode(graph);
			@Expression node_ex4 = @Expression.CreateNode(graph);
			@Expression node_ex5 = @Expression.CreateNode(graph);
			@Variabel node_backup = @Variabel.CreateNode(graph);
			@Expression node_ex6 = @Expression.CreateNode(graph);
			@Expression node_ex7 = @Expression.CreateNode(graph);
			@MethodSignature node_restoreS = @MethodSignature.CreateNode(graph);
			@MethodBody node_restoreB = @MethodBody.CreateNode(graph);
			@Expression node_ex8 = @Expression.CreateNode(graph);
			@Expression node_ex9 = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( createProgramGraphExample_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_any, node_cell);
			@contains edge__edge1 = @contains.CreateEdge(graph, node_cell, node_recell);
			@contains edge__edge2 = @contains.CreateEdge(graph, node_cell, node_getS);
			@contains edge__edge3 = @contains.CreateEdge(graph, node_cell, node_getB);
			@bindsTo edge__edge4 = @bindsTo.CreateEdge(graph, node_getB, node_getS);
			@contains edge__edge5 = @contains.CreateEdge(graph, node_cell, node_cts);
			@hasType edge__edge6 = @hasType.CreateEdge(graph, node_cts, node_any);
			@contains edge__edge7 = @contains.CreateEdge(graph, node_getB, node_ex1);
			@uses edge__edge8 = @uses.CreateEdge(graph, node_ex1, node_cts);
			@contains edge__edge9 = @contains.CreateEdge(graph, node_cell, node_setS);
			@contains edge__edge10 = @contains.CreateEdge(graph, node_cell, node_setB);
			@bindsTo edge__edge11 = @bindsTo.CreateEdge(graph, node_setB, node_setS);
			@contains edge__edge12 = @contains.CreateEdge(graph, node_setB, node_n);
			@hasType edge__edge13 = @hasType.CreateEdge(graph, node_n, node_any);
			@contains edge__edge14 = @contains.CreateEdge(graph, node_setB, node_ex2);
			@writesTo edge__edge15 = @writesTo.CreateEdge(graph, node_ex2, node_cts);
			@contains edge__edge16 = @contains.CreateEdge(graph, node_ex2, node_ex3);
			@uses edge__edge17 = @uses.CreateEdge(graph, node_ex3, node_n);
			@contains edge__edge18 = @contains.CreateEdge(graph, node_recell, node_setB2);
			@bindsTo edge__edge19 = @bindsTo.CreateEdge(graph, node_setB2, node_setS);
			@contains edge__edge20 = @contains.CreateEdge(graph, node_setB2, node_n2);
			@hasType edge__edge21 = @hasType.CreateEdge(graph, node_n2, node_any);
			@contains edge__edge22 = @contains.CreateEdge(graph, node_setB2, node_ex4);
			@calls edge__edge23 = @calls.CreateEdge(graph, node_ex4, node_setS);
			@contains edge__edge24 = @contains.CreateEdge(graph, node_ex4, node_ex5);
			@uses edge__edge25 = @uses.CreateEdge(graph, node_ex5, node_n2);
			@contains edge__edge26 = @contains.CreateEdge(graph, node_recell, node_backup);
			@hasType edge__edge27 = @hasType.CreateEdge(graph, node_backup, node_any);
			@contains edge__edge28 = @contains.CreateEdge(graph, node_setB2, node_ex6);
			@writesTo edge__edge29 = @writesTo.CreateEdge(graph, node_ex6, node_backup);
			@contains edge__edge30 = @contains.CreateEdge(graph, node_ex6, node_ex7);
			@uses edge__edge31 = @uses.CreateEdge(graph, node_ex7, node_cts);
			@contains edge__edge32 = @contains.CreateEdge(graph, node_recell, node_restoreS);
			@contains edge__edge33 = @contains.CreateEdge(graph, node_recell, node_restoreB);
			@bindsTo edge__edge34 = @bindsTo.CreateEdge(graph, node_restoreB, node_restoreS);
			@contains edge__edge35 = @contains.CreateEdge(graph, node_restoreB, node_ex8);
			@writesTo edge__edge36 = @writesTo.CreateEdge(graph, node_ex8, node_cts);
			@contains edge__edge37 = @contains.CreateEdge(graph, node_ex8, node_ex9);
			@uses edge__edge38 = @uses.CreateEdge(graph, node_ex9, node_backup);
			return EmptyReturnElements;
		}
		private static String[] createProgramGraphExample_addedNodeNames = new String[] { "any", "cell", "recell", "getS", "getB", "cts", "ex1", "setS", "setB", "n", "ex2", "ex3", "setB2", "n2", "ex4", "ex5", "backup", "ex6", "ex7", "restoreS", "restoreB", "ex8", "ex9" };
		private static String[] createProgramGraphExample_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7", "_edge8", "_edge9", "_edge10", "_edge11", "_edge12", "_edge13", "_edge14", "_edge15", "_edge16", "_edge17", "_edge18", "_edge19", "_edge20", "_edge21", "_edge22", "_edge23", "_edge24", "_edge25", "_edge26", "_edge27", "_edge28", "_edge29", "_edge30", "_edge31", "_edge32", "_edge33", "_edge34", "_edge35", "_edge36", "_edge37", "_edge38" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( createProgramGraphExample_addedNodeNames );
			@Class node_any = @Class.CreateNode(graph);
			@Class node_cell = @Class.CreateNode(graph);
			@Class node_recell = @Class.CreateNode(graph);
			@MethodSignature node_getS = @MethodSignature.CreateNode(graph);
			@MethodBody node_getB = @MethodBody.CreateNode(graph);
			@Variabel node_cts = @Variabel.CreateNode(graph);
			@Expression node_ex1 = @Expression.CreateNode(graph);
			@MethodSignature node_setS = @MethodSignature.CreateNode(graph);
			@MethodBody node_setB = @MethodBody.CreateNode(graph);
			@Constant node_n = @Constant.CreateNode(graph);
			@Expression node_ex2 = @Expression.CreateNode(graph);
			@Expression node_ex3 = @Expression.CreateNode(graph);
			@MethodBody node_setB2 = @MethodBody.CreateNode(graph);
			@Constant node_n2 = @Constant.CreateNode(graph);
			@Expression node_ex4 = @Expression.CreateNode(graph);
			@Expression node_ex5 = @Expression.CreateNode(graph);
			@Variabel node_backup = @Variabel.CreateNode(graph);
			@Expression node_ex6 = @Expression.CreateNode(graph);
			@Expression node_ex7 = @Expression.CreateNode(graph);
			@MethodSignature node_restoreS = @MethodSignature.CreateNode(graph);
			@MethodBody node_restoreB = @MethodBody.CreateNode(graph);
			@Expression node_ex8 = @Expression.CreateNode(graph);
			@Expression node_ex9 = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( createProgramGraphExample_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_any, node_cell);
			@contains edge__edge1 = @contains.CreateEdge(graph, node_cell, node_recell);
			@contains edge__edge2 = @contains.CreateEdge(graph, node_cell, node_getS);
			@contains edge__edge3 = @contains.CreateEdge(graph, node_cell, node_getB);
			@bindsTo edge__edge4 = @bindsTo.CreateEdge(graph, node_getB, node_getS);
			@contains edge__edge5 = @contains.CreateEdge(graph, node_cell, node_cts);
			@hasType edge__edge6 = @hasType.CreateEdge(graph, node_cts, node_any);
			@contains edge__edge7 = @contains.CreateEdge(graph, node_getB, node_ex1);
			@uses edge__edge8 = @uses.CreateEdge(graph, node_ex1, node_cts);
			@contains edge__edge9 = @contains.CreateEdge(graph, node_cell, node_setS);
			@contains edge__edge10 = @contains.CreateEdge(graph, node_cell, node_setB);
			@bindsTo edge__edge11 = @bindsTo.CreateEdge(graph, node_setB, node_setS);
			@contains edge__edge12 = @contains.CreateEdge(graph, node_setB, node_n);
			@hasType edge__edge13 = @hasType.CreateEdge(graph, node_n, node_any);
			@contains edge__edge14 = @contains.CreateEdge(graph, node_setB, node_ex2);
			@writesTo edge__edge15 = @writesTo.CreateEdge(graph, node_ex2, node_cts);
			@contains edge__edge16 = @contains.CreateEdge(graph, node_ex2, node_ex3);
			@uses edge__edge17 = @uses.CreateEdge(graph, node_ex3, node_n);
			@contains edge__edge18 = @contains.CreateEdge(graph, node_recell, node_setB2);
			@bindsTo edge__edge19 = @bindsTo.CreateEdge(graph, node_setB2, node_setS);
			@contains edge__edge20 = @contains.CreateEdge(graph, node_setB2, node_n2);
			@hasType edge__edge21 = @hasType.CreateEdge(graph, node_n2, node_any);
			@contains edge__edge22 = @contains.CreateEdge(graph, node_setB2, node_ex4);
			@calls edge__edge23 = @calls.CreateEdge(graph, node_ex4, node_setS);
			@contains edge__edge24 = @contains.CreateEdge(graph, node_ex4, node_ex5);
			@uses edge__edge25 = @uses.CreateEdge(graph, node_ex5, node_n2);
			@contains edge__edge26 = @contains.CreateEdge(graph, node_recell, node_backup);
			@hasType edge__edge27 = @hasType.CreateEdge(graph, node_backup, node_any);
			@contains edge__edge28 = @contains.CreateEdge(graph, node_setB2, node_ex6);
			@writesTo edge__edge29 = @writesTo.CreateEdge(graph, node_ex6, node_backup);
			@contains edge__edge30 = @contains.CreateEdge(graph, node_ex6, node_ex7);
			@uses edge__edge31 = @uses.CreateEdge(graph, node_ex7, node_cts);
			@contains edge__edge32 = @contains.CreateEdge(graph, node_recell, node_restoreS);
			@contains edge__edge33 = @contains.CreateEdge(graph, node_recell, node_restoreB);
			@bindsTo edge__edge34 = @bindsTo.CreateEdge(graph, node_restoreB, node_restoreS);
			@contains edge__edge35 = @contains.CreateEdge(graph, node_restoreB, node_ex8);
			@writesTo edge__edge36 = @writesTo.CreateEdge(graph, node_ex8, node_cts);
			@contains edge__edge37 = @contains.CreateEdge(graph, node_ex8, node_ex9);
			@uses edge__edge38 = @uses.CreateEdge(graph, node_ex9, node_backup);
			return EmptyReturnElements;
		}

		static Rule_createProgramGraphExample() {
		}
	}

	public class Rule_createProgramGraphPullUp : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createProgramGraphPullUp instance = null;
		public static Rule_createProgramGraphPullUp Instance { get { if (instance==null) { instance = new Rule_createProgramGraphPullUp(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[2];

		public enum createProgramGraphPullUp_NodeNums { };
		public enum createProgramGraphPullUp_EdgeNums { };
		public enum createProgramGraphPullUp_VariableNums { };
		public enum createProgramGraphPullUp_SubNums { };
		public enum createProgramGraphPullUp_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createProgramGraphPullUp;


#if INITIAL_WARMUP
		public Rule_createProgramGraphPullUp()
#else
		private Rule_createProgramGraphPullUp()
#endif
		{
			name = "createProgramGraphPullUp";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Class.typeVar, NodeType_MethodBody.typeVar, };
		}
		public override void initialize()
		{
			bool[,] createProgramGraphPullUp_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createProgramGraphPullUp_isEdgeHomomorphicGlobal = new bool[0, 0] ;
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
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createProgramGraphPullUp_isNodeHomomorphicGlobal,
				createProgramGraphPullUp_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createProgramGraphPullUp;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( createProgramGraphPullUp_addedNodeNames );
			@Class node_c1 = @Class.CreateNode(graph);
			@Class node_c2 = @Class.CreateNode(graph);
			@Class node_c3 = @Class.CreateNode(graph);
			@Class node_c4 = @Class.CreateNode(graph);
			@MethodSignature node_m5 = @MethodSignature.CreateNode(graph);
			@MethodBody node_b2 = @MethodBody.CreateNode(graph);
			@Variabel node_v7a = @Variabel.CreateNode(graph);
			@MethodBody node_b3 = @MethodBody.CreateNode(graph);
			@Variabel node_v7b = @Variabel.CreateNode(graph);
			@MethodBody node_b4 = @MethodBody.CreateNode(graph);
			@MethodSignature node_m8 = @MethodSignature.CreateNode(graph);
			@Variabel node_v9 = @Variabel.CreateNode(graph);
			@Expression node_ex1 = @Expression.CreateNode(graph);
			@Expression node_ex = @Expression.CreateNode(graph);
			@Expression node_ex2 = @Expression.CreateNode(graph);
			@Expression node_ex3 = @Expression.CreateNode(graph);
			@Expression node_ex4 = @Expression.CreateNode(graph);
			@Expression node_ex5 = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( createProgramGraphPullUp_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c1, node_c2);
			@contains edge__edge1 = @contains.CreateEdge(graph, node_c1, node_c3);
			@contains edge__edge2 = @contains.CreateEdge(graph, node_c1, node_c4);
			@contains edge__edge3 = @contains.CreateEdge(graph, node_c1, node_m5);
			@contains edge__edge4 = @contains.CreateEdge(graph, node_c2, node_b2);
			@contains edge__edge5 = @contains.CreateEdge(graph, node_b2, node_v7a);
			@contains edge__edge6 = @contains.CreateEdge(graph, node_c3, node_b3);
			@contains edge__edge7 = @contains.CreateEdge(graph, node_b3, node_v7b);
			@contains edge__edge8 = @contains.CreateEdge(graph, node_c4, node_b4);
			@bindsTo edge__edge9 = @bindsTo.CreateEdge(graph, node_b2, node_m5);
			@bindsTo edge__edge10 = @bindsTo.CreateEdge(graph, node_b3, node_m5);
			@bindsTo edge__edge11 = @bindsTo.CreateEdge(graph, node_b4, node_m5);
			@contains edge__edge12 = @contains.CreateEdge(graph, node_c1, node_m8);
			@contains edge__edge13 = @contains.CreateEdge(graph, node_c2, node_v9);
			@contains edge__edge14 = @contains.CreateEdge(graph, node_b2, node_ex1);
			@writesTo edge__edge15 = @writesTo.CreateEdge(graph, node_ex1, node_v9);
			@contains edge__edge16 = @contains.CreateEdge(graph, node_ex1, node_ex);
			@uses edge__edge17 = @uses.CreateEdge(graph, node_ex, node_v7a);
			@contains edge__edge18 = @contains.CreateEdge(graph, node_b2, node_ex2);
			@calls edge__edge19 = @calls.CreateEdge(graph, node_ex2, node_m8);
			@contains edge__edge20 = @contains.CreateEdge(graph, node_ex2, node_ex3);
			@uses edge__edge21 = @uses.CreateEdge(graph, node_ex3, node_v9);
			@contains edge__edge22 = @contains.CreateEdge(graph, node_b3, node_ex4);
			@calls edge__edge23 = @calls.CreateEdge(graph, node_ex4, node_m8);
			@contains edge__edge24 = @contains.CreateEdge(graph, node_ex4, node_ex5);
			@uses edge__edge25 = @uses.CreateEdge(graph, node_ex5, node_v7b);
			ReturnArray[0] = node_c1;
			ReturnArray[1] = node_b4;
			return ReturnArray;
		}
		private static String[] createProgramGraphPullUp_addedNodeNames = new String[] { "c1", "c2", "c3", "c4", "m5", "b2", "v7a", "b3", "v7b", "b4", "m8", "v9", "ex1", "ex", "ex2", "ex3", "ex4", "ex5" };
		private static String[] createProgramGraphPullUp_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7", "_edge8", "_edge9", "_edge10", "_edge11", "_edge12", "_edge13", "_edge14", "_edge15", "_edge16", "_edge17", "_edge18", "_edge19", "_edge20", "_edge21", "_edge22", "_edge23", "_edge24", "_edge25" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( createProgramGraphPullUp_addedNodeNames );
			@Class node_c1 = @Class.CreateNode(graph);
			@Class node_c2 = @Class.CreateNode(graph);
			@Class node_c3 = @Class.CreateNode(graph);
			@Class node_c4 = @Class.CreateNode(graph);
			@MethodSignature node_m5 = @MethodSignature.CreateNode(graph);
			@MethodBody node_b2 = @MethodBody.CreateNode(graph);
			@Variabel node_v7a = @Variabel.CreateNode(graph);
			@MethodBody node_b3 = @MethodBody.CreateNode(graph);
			@Variabel node_v7b = @Variabel.CreateNode(graph);
			@MethodBody node_b4 = @MethodBody.CreateNode(graph);
			@MethodSignature node_m8 = @MethodSignature.CreateNode(graph);
			@Variabel node_v9 = @Variabel.CreateNode(graph);
			@Expression node_ex1 = @Expression.CreateNode(graph);
			@Expression node_ex = @Expression.CreateNode(graph);
			@Expression node_ex2 = @Expression.CreateNode(graph);
			@Expression node_ex3 = @Expression.CreateNode(graph);
			@Expression node_ex4 = @Expression.CreateNode(graph);
			@Expression node_ex5 = @Expression.CreateNode(graph);
			graph.SettingAddedEdgeNames( createProgramGraphPullUp_addedEdgeNames );
			@contains edge__edge0 = @contains.CreateEdge(graph, node_c1, node_c2);
			@contains edge__edge1 = @contains.CreateEdge(graph, node_c1, node_c3);
			@contains edge__edge2 = @contains.CreateEdge(graph, node_c1, node_c4);
			@contains edge__edge3 = @contains.CreateEdge(graph, node_c1, node_m5);
			@contains edge__edge4 = @contains.CreateEdge(graph, node_c2, node_b2);
			@contains edge__edge5 = @contains.CreateEdge(graph, node_b2, node_v7a);
			@contains edge__edge6 = @contains.CreateEdge(graph, node_c3, node_b3);
			@contains edge__edge7 = @contains.CreateEdge(graph, node_b3, node_v7b);
			@contains edge__edge8 = @contains.CreateEdge(graph, node_c4, node_b4);
			@bindsTo edge__edge9 = @bindsTo.CreateEdge(graph, node_b2, node_m5);
			@bindsTo edge__edge10 = @bindsTo.CreateEdge(graph, node_b3, node_m5);
			@bindsTo edge__edge11 = @bindsTo.CreateEdge(graph, node_b4, node_m5);
			@contains edge__edge12 = @contains.CreateEdge(graph, node_c1, node_m8);
			@contains edge__edge13 = @contains.CreateEdge(graph, node_c2, node_v9);
			@contains edge__edge14 = @contains.CreateEdge(graph, node_b2, node_ex1);
			@writesTo edge__edge15 = @writesTo.CreateEdge(graph, node_ex1, node_v9);
			@contains edge__edge16 = @contains.CreateEdge(graph, node_ex1, node_ex);
			@uses edge__edge17 = @uses.CreateEdge(graph, node_ex, node_v7a);
			@contains edge__edge18 = @contains.CreateEdge(graph, node_b2, node_ex2);
			@calls edge__edge19 = @calls.CreateEdge(graph, node_ex2, node_m8);
			@contains edge__edge20 = @contains.CreateEdge(graph, node_ex2, node_ex3);
			@uses edge__edge21 = @uses.CreateEdge(graph, node_ex3, node_v9);
			@contains edge__edge22 = @contains.CreateEdge(graph, node_b3, node_ex4);
			@calls edge__edge23 = @calls.CreateEdge(graph, node_ex4, node_m8);
			@contains edge__edge24 = @contains.CreateEdge(graph, node_ex4, node_ex5);
			@uses edge__edge25 = @uses.CreateEdge(graph, node_ex5, node_v7b);
			ReturnArray[0] = node_c1;
			ReturnArray[1] = node_b4;
			return ReturnArray;
		}

		static Rule_createProgramGraphPullUp() {
		}
	}

	public class Rule_pullUpMethod : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_pullUpMethod instance = null;
		public static Rule_pullUpMethod Instance { get { if (instance==null) { instance = new Rule_pullUpMethod(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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
		public enum pullUpMethod_SubNums { @mb, };
		public enum pullUpMethod_AltNums { };



		GRGEN_LGSP.PatternGraph pat_pullUpMethod;


#if INITIAL_WARMUP
		public Rule_pullUpMethod()
#else
		private Rule_pullUpMethod()
#endif
		{
			name = "pullUpMethod";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Class.typeVar, NodeType_MethodBody.typeVar, };
			inputNames = new string[] { "pullUpMethod_node_c1", "pullUpMethod_node_b4", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode pullUpMethod_node_c1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "pullUpMethod_node_c1", "c1", pullUpMethod_node_c1_AllowedTypes, pullUpMethod_node_c1_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode pullUpMethod_node_c3 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "pullUpMethod_node_c3", "c3", pullUpMethod_node_c3_AllowedTypes, pullUpMethod_node_c3_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode pullUpMethod_node_b4 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "pullUpMethod_node_b4", "b4", pullUpMethod_node_b4_AllowedTypes, pullUpMethod_node_b4_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternNode pullUpMethod_node_m5 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodSignature, "pullUpMethod_node_m5", "m5", pullUpMethod_node_m5_AllowedTypes, pullUpMethod_node_m5_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge pullUpMethod_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "pullUpMethod_edge__edge0", "_edge0", pullUpMethod_edge__edge0_AllowedTypes, pullUpMethod_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge pullUpMethod_edge_m = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "pullUpMethod_edge_m", "m", pullUpMethod_edge_m_AllowedTypes, pullUpMethod_edge_m_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge pullUpMethod_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@bindsTo, "pullUpMethod_edge__edge1", "_edge1", pullUpMethod_edge__edge1_AllowedTypes, pullUpMethod_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding pullUpMethod_mb = new GRGEN_LGSP.PatternGraphEmbedding("mb", Pattern_MultipleBodies.Instance, new GRGEN_LGSP.PatternElement[] { pullUpMethod_node_m5, pullUpMethod_node_c1 });
			pat_pullUpMethod = new GRGEN_LGSP.PatternGraph(
				"pullUpMethod",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { pullUpMethod_node_c1, pullUpMethod_node_c3, pullUpMethod_node_b4, pullUpMethod_node_m5 }, 
				new GRGEN_LGSP.PatternEdge[] { pullUpMethod_edge__edge0, pullUpMethod_edge_m, pullUpMethod_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { pullUpMethod_mb }, 
				new GRGEN_LGSP.Alternative[] {  }, 
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

			pullUpMethod_node_c1.PointOfDefinition = null;
			pullUpMethod_node_c3.PointOfDefinition = pat_pullUpMethod;
			pullUpMethod_node_b4.PointOfDefinition = null;
			pullUpMethod_node_m5.PointOfDefinition = pat_pullUpMethod;
			pullUpMethod_edge__edge0.PointOfDefinition = pat_pullUpMethod;
			pullUpMethod_edge_m.PointOfDefinition = pat_pullUpMethod;
			pullUpMethod_edge__edge1.PointOfDefinition = pat_pullUpMethod;
			pullUpMethod_mb.PointOfDefinition = pat_pullUpMethod;

			patternGraph = pat_pullUpMethod;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c1 = curMatch.Nodes[(int)pullUpMethod_NodeNums.@c1];
			GRGEN_LGSP.LGSPNode node_b4 = curMatch.Nodes[(int)pullUpMethod_NodeNums.@b4];
			GRGEN_LGSP.LGSPEdge edge_m = curMatch.Edges[(int)pullUpMethod_EdgeNums.@m];
			GRGEN_LGSP.LGSPMatch subpattern_mb = curMatch.EmbeddedGraphs[(int)pullUpMethod_SubNums.@mb];
			graph.SettingAddedNodeNames( pullUpMethod_addedNodeNames );
			Pattern_MultipleBodies.Instance.MultipleBodies_Modify(graph, subpattern_mb);
			graph.SettingAddedEdgeNames( pullUpMethod_addedEdgeNames );
			@contains edge__edge2;
			if(edge_m.type == EdgeType_contains.typeVar)
			{
				// re-using edge_m as edge__edge2
				edge__edge2 = (@contains) edge_m;
				graph.ReuseEdge(edge_m, node_c1, null);
			}
			else
			{
				graph.Remove(edge_m);
				edge__edge2 = @contains.CreateEdge(graph, node_c1, node_b4);
			}
			return EmptyReturnElements;
		}
		private static String[] pullUpMethod_addedNodeNames = new String[] {  };
		private static String[] pullUpMethod_addedEdgeNames = new String[] { "_edge2" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c1 = curMatch.Nodes[(int)pullUpMethod_NodeNums.@c1];
			GRGEN_LGSP.LGSPNode node_b4 = curMatch.Nodes[(int)pullUpMethod_NodeNums.@b4];
			GRGEN_LGSP.LGSPEdge edge_m = curMatch.Edges[(int)pullUpMethod_EdgeNums.@m];
			GRGEN_LGSP.LGSPMatch subpattern_mb = curMatch.EmbeddedGraphs[(int)pullUpMethod_SubNums.@mb];
			graph.SettingAddedNodeNames( pullUpMethod_addedNodeNames );
			Pattern_MultipleBodies.Instance.MultipleBodies_Modify(graph, subpattern_mb);
			graph.SettingAddedEdgeNames( pullUpMethod_addedEdgeNames );
			@contains edge__edge2 = @contains.CreateEdge(graph, node_c1, node_b4);
			graph.Remove(edge_m);
			return EmptyReturnElements;
		}

		static Rule_pullUpMethod() {
		}
	}

	public class Rule_matchAll : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_matchAll instance = null;
		public static Rule_matchAll Instance { get { if (instance==null) { instance = new Rule_matchAll(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] matchAll_node_c1_AllowedTypes = null;
		public static bool[] matchAll_node_c1_IsAllowedType = null;
		public enum matchAll_NodeNums { @c1, };
		public enum matchAll_EdgeNums { };
		public enum matchAll_VariableNums { };
		public enum matchAll_SubNums { @_subpattern0, };
		public enum matchAll_AltNums { };


		GRGEN_LGSP.PatternGraph pat_matchAll;


#if INITIAL_WARMUP
		public Rule_matchAll()
#else
		private Rule_matchAll()
#endif
		{
			name = "matchAll";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Class.typeVar, };
			inputNames = new string[] { "matchAll_node_c1", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] matchAll_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] matchAll_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode matchAll_node_c1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "matchAll_node_c1", "c1", matchAll_node_c1_AllowedTypes, matchAll_node_c1_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternGraphEmbedding matchAll__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_Subclass.Instance, new GRGEN_LGSP.PatternElement[] { matchAll_node_c1 });
			pat_matchAll = new GRGEN_LGSP.PatternGraph(
				"matchAll",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { matchAll_node_c1 }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { matchAll__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				matchAll_isNodeHomomorphicGlobal,
				matchAll_isEdgeHomomorphicGlobal
			);

			matchAll_node_c1.PointOfDefinition = null;
			matchAll__subpattern0.PointOfDefinition = pat_matchAll;

			patternGraph = pat_matchAll;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)matchAll_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPMatch subpattern__subpattern0 = curMatch.EmbeddedGraphs[(int)matchAll_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		static Rule_matchAll() {
		}
	}

	public class Rule_InsertHelperEdgesForNestedLayout : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_InsertHelperEdgesForNestedLayout instance = null;
		public static Rule_InsertHelperEdgesForNestedLayout Instance { get { if (instance==null) { instance = new Rule_InsertHelperEdgesForNestedLayout(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum InsertHelperEdgesForNestedLayout_NodeNums { };
		public enum InsertHelperEdgesForNestedLayout_EdgeNums { };
		public enum InsertHelperEdgesForNestedLayout_VariableNums { };
		public enum InsertHelperEdgesForNestedLayout_SubNums { };
		public enum InsertHelperEdgesForNestedLayout_AltNums { };



		GRGEN_LGSP.PatternGraph pat_InsertHelperEdgesForNestedLayout;


#if INITIAL_WARMUP
		public Rule_InsertHelperEdgesForNestedLayout()
#else
		private Rule_InsertHelperEdgesForNestedLayout()
#endif
		{
			name = "InsertHelperEdgesForNestedLayout";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] InsertHelperEdgesForNestedLayout_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] InsertHelperEdgesForNestedLayout_isEdgeHomomorphicGlobal = new bool[0, 0] ;
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
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				InsertHelperEdgesForNestedLayout_isNodeHomomorphicGlobal,
				InsertHelperEdgesForNestedLayout_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_InsertHelperEdgesForNestedLayout;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( InsertHelperEdgesForNestedLayout_addedNodeNames );
			graph.SettingAddedEdgeNames( InsertHelperEdgesForNestedLayout_addedEdgeNames );
			ApplyXGRS_0(graph);
			return EmptyReturnElements;
		}
		private static String[] InsertHelperEdgesForNestedLayout_addedNodeNames = new String[] {  };
		private static String[] InsertHelperEdgesForNestedLayout_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			graph.SettingAddedNodeNames( InsertHelperEdgesForNestedLayout_addedNodeNames );
			graph.SettingAddedEdgeNames( InsertHelperEdgesForNestedLayout_addedEdgeNames );
			ApplyXGRS_0(graph);
			return EmptyReturnElements;
		}
        public static bool ApplyXGRS_0(de.unika.ipd.grGen.lgsp.LGSPGraph graph)
        {
            de.unika.ipd.grGen.lgsp.LGSPActions actions = graph.curActions;
            bool res_0;
            bool res_1;
            bool res_2;
            de.unika.ipd.grGen.lgsp.LGSPAction rule_LinkMethodBodyToContainedEntity = actions.GetAction("LinkMethodBodyToContainedEntity");
            bool res_3;
            bool res_4;
            bool res_5;
            de.unika.ipd.grGen.lgsp.LGSPAction rule_LinkMethodBodyToContainedExpressionTransitive = actions.GetAction("LinkMethodBodyToContainedExpressionTransitive");
            bool res_6;
            bool res_7;
            de.unika.ipd.grGen.lgsp.LGSPAction rule_LinkClassToFeature = actions.GetAction("LinkClassToFeature");
            long i_1 = 0;
            while(true)
            {
                de.unika.ipd.grGen.lgsp.LGSPMatches mat_2 = rule_LinkMethodBodyToContainedEntity.Match(graph, 1, null);
                graph.Matched(mat_2, false);
                if(mat_2.Count == 0)
                	res_2 = false;
                else
                {
                    if(graph.PerformanceInfo != null) graph.PerformanceInfo.MatchesFound += mat_2.Count;
                    graph.Finishing(mat_2, false);
                    object[] ret_2 = rule_LinkMethodBodyToContainedEntity.Modify(graph, mat_2.matchesList.First);
                    if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                    graph.Finished(mat_2, false);
                    res_2 = ret_2 != null;
                }
                if(!res_2) break;
                i_1++;
            }
            res_1 = i_1 >= 0;
            if(!res_1) res_0 = false;
            else
            {
                long i_4 = 0;
                while(true)
                {
                    de.unika.ipd.grGen.lgsp.LGSPMatches mat_5 = rule_LinkMethodBodyToContainedExpressionTransitive.Match(graph, 1, null);
                    graph.Matched(mat_5, false);
                    if(mat_5.Count == 0)
                    	res_5 = false;
                    else
                    {
                        if(graph.PerformanceInfo != null) graph.PerformanceInfo.MatchesFound += mat_5.Count;
                        graph.Finishing(mat_5, false);
                        object[] ret_5 = rule_LinkMethodBodyToContainedExpressionTransitive.Modify(graph, mat_5.matchesList.First);
                        if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                        graph.Finished(mat_5, false);
                        res_5 = ret_5 != null;
                    }
                    if(!res_5) break;
                    i_4++;
                }
                res_4 = i_4 >= 0;
                if(!res_4) res_3 = false;
                else
                {
                    long i_6 = 0;
                    while(true)
                    {
                        de.unika.ipd.grGen.lgsp.LGSPMatches mat_7 = rule_LinkClassToFeature.Match(graph, 1, null);
                        graph.Matched(mat_7, false);
                        if(mat_7.Count == 0)
                        	res_7 = false;
                        else
                        {
                            if(graph.PerformanceInfo != null) graph.PerformanceInfo.MatchesFound += mat_7.Count;
                            graph.Finishing(mat_7, false);
                            object[] ret_7 = rule_LinkClassToFeature.Modify(graph, mat_7.matchesList.First);
                            if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                            graph.Finished(mat_7, false);
                            res_7 = ret_7 != null;
                        }
                        if(!res_7) break;
                        i_6++;
                    }
                    res_6 = i_6 >= 0;
                    res_3 = res_6;
                }
                res_0 = res_3;
            }
            return res_0;
        }
        private static object[] __xgrs_paramarray_0 = new object[0];

		static Rule_InsertHelperEdgesForNestedLayout() {
		}
	}

	public class Rule_LinkMethodBodyToContainedEntity : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_LinkMethodBodyToContainedEntity instance = null;
		public static Rule_LinkMethodBodyToContainedEntity Instance { get { if (instance==null) { instance = new Rule_LinkMethodBodyToContainedEntity(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_LinkMethodBodyToContainedEntity;

		public static GRGEN_LIBGR.EdgeType[] LinkMethodBodyToContainedEntity_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedEntity_neg_0_edge__edge0_IsAllowedType = null;
		public enum LinkMethodBodyToContainedEntity_neg_0_NodeNums { @mb, @e, };
		public enum LinkMethodBodyToContainedEntity_neg_0_EdgeNums { @_edge0, };
		public enum LinkMethodBodyToContainedEntity_neg_0_VariableNums { };
		public enum LinkMethodBodyToContainedEntity_neg_0_SubNums { };
		public enum LinkMethodBodyToContainedEntity_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph LinkMethodBodyToContainedEntity_neg_0;


#if INITIAL_WARMUP
		public Rule_LinkMethodBodyToContainedEntity()
#else
		private Rule_LinkMethodBodyToContainedEntity()
#endif
		{
			name = "LinkMethodBodyToContainedEntity";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] LinkMethodBodyToContainedEntity_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkMethodBodyToContainedEntity_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedEntity_node_mb = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "LinkMethodBodyToContainedEntity_node_mb", "mb", LinkMethodBodyToContainedEntity_node_mb_AllowedTypes, LinkMethodBodyToContainedEntity_node_mb_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedEntity_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Entity, "LinkMethodBodyToContainedEntity_node_e", "e", LinkMethodBodyToContainedEntity_node_e_AllowedTypes, LinkMethodBodyToContainedEntity_node_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedEntity_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "LinkMethodBodyToContainedEntity_edge__edge0", "_edge0", LinkMethodBodyToContainedEntity_edge__edge0_AllowedTypes, LinkMethodBodyToContainedEntity_edge__edge0_IsAllowedType, 5.5F, -1);
			bool[,] LinkMethodBodyToContainedEntity_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkMethodBodyToContainedEntity_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@containedInMethodBody, "LinkMethodBodyToContainedEntity_neg_0_edge__edge0", "_edge0", LinkMethodBodyToContainedEntity_neg_0_edge__edge0_AllowedTypes, LinkMethodBodyToContainedEntity_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
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
				new GRGEN_LGSP.PatternGraph[] { LinkMethodBodyToContainedEntity_neg_0,  }, 
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

			LinkMethodBodyToContainedEntity_node_mb.PointOfDefinition = pat_LinkMethodBodyToContainedEntity;
			LinkMethodBodyToContainedEntity_node_e.PointOfDefinition = pat_LinkMethodBodyToContainedEntity;
			LinkMethodBodyToContainedEntity_edge__edge0.PointOfDefinition = pat_LinkMethodBodyToContainedEntity;
			LinkMethodBodyToContainedEntity_neg_0_edge__edge0.PointOfDefinition = LinkMethodBodyToContainedEntity_neg_0;

			patternGraph = pat_LinkMethodBodyToContainedEntity;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_mb = curMatch.Nodes[(int)LinkMethodBodyToContainedEntity_NodeNums.@mb];
			GRGEN_LGSP.LGSPNode node_e = curMatch.Nodes[(int)LinkMethodBodyToContainedEntity_NodeNums.@e];
			graph.SettingAddedNodeNames( LinkMethodBodyToContainedEntity_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkMethodBodyToContainedEntity_addedEdgeNames );
			@containedInMethodBody edge__edge1 = @containedInMethodBody.CreateEdge(graph, node_mb, node_e);
			return EmptyReturnElements;
		}
		private static String[] LinkMethodBodyToContainedEntity_addedNodeNames = new String[] {  };
		private static String[] LinkMethodBodyToContainedEntity_addedEdgeNames = new String[] { "_edge1" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_mb = curMatch.Nodes[(int)LinkMethodBodyToContainedEntity_NodeNums.@mb];
			GRGEN_LGSP.LGSPNode node_e = curMatch.Nodes[(int)LinkMethodBodyToContainedEntity_NodeNums.@e];
			graph.SettingAddedNodeNames( LinkMethodBodyToContainedEntity_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkMethodBodyToContainedEntity_addedEdgeNames );
			@containedInMethodBody edge__edge1 = @containedInMethodBody.CreateEdge(graph, node_mb, node_e);
			return EmptyReturnElements;
		}

		static Rule_LinkMethodBodyToContainedEntity() {
		}
	}

	public class Rule_LinkMethodBodyToContainedExpressionTransitive : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_LinkMethodBodyToContainedExpressionTransitive instance = null;
		public static Rule_LinkMethodBodyToContainedExpressionTransitive Instance { get { if (instance==null) { instance = new Rule_LinkMethodBodyToContainedExpressionTransitive(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_LinkMethodBodyToContainedExpressionTransitive;

		public static GRGEN_LIBGR.EdgeType[] LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_IsAllowedType = null;
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_NodeNums { @e1, @e2, };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_EdgeNums { @_edge0, };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_VariableNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_SubNums { };
		public enum LinkMethodBodyToContainedExpressionTransitive_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph LinkMethodBodyToContainedExpressionTransitive_neg_0;


#if INITIAL_WARMUP
		public Rule_LinkMethodBodyToContainedExpressionTransitive()
#else
		private Rule_LinkMethodBodyToContainedExpressionTransitive()
#endif
		{
			name = "LinkMethodBodyToContainedExpressionTransitive";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedExpressionTransitive_node_mb = new GRGEN_LGSP.PatternNode((int) NodeTypes.@MethodBody, "LinkMethodBodyToContainedExpressionTransitive_node_mb", "mb", LinkMethodBodyToContainedExpressionTransitive_node_mb_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_node_mb_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedExpressionTransitive_node_e1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "LinkMethodBodyToContainedExpressionTransitive_node_e1", "e1", LinkMethodBodyToContainedExpressionTransitive_node_e1_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_node_e1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode LinkMethodBodyToContainedExpressionTransitive_node_e2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Expression, "LinkMethodBodyToContainedExpressionTransitive_node_e2", "e2", LinkMethodBodyToContainedExpressionTransitive_node_e2_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_node_e2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@containedInMethodBody, "LinkMethodBodyToContainedExpressionTransitive_edge__edge0", "_edge0", LinkMethodBodyToContainedExpressionTransitive_edge__edge0_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "LinkMethodBodyToContainedExpressionTransitive_edge__edge1", "_edge1", LinkMethodBodyToContainedExpressionTransitive_edge__edge1_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] LinkMethodBodyToContainedExpressionTransitive_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkMethodBodyToContainedExpressionTransitive_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@containedInMethodBody, "LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0", "_edge0", LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_AllowedTypes, LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
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
				new GRGEN_LGSP.PatternGraph[] { LinkMethodBodyToContainedExpressionTransitive_neg_0,  }, 
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

			LinkMethodBodyToContainedExpressionTransitive_node_mb.PointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_node_e1.PointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_node_e2.PointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_edge__edge0.PointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_edge__edge1.PointOfDefinition = pat_LinkMethodBodyToContainedExpressionTransitive;
			LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.PointOfDefinition = LinkMethodBodyToContainedExpressionTransitive_neg_0;

			patternGraph = pat_LinkMethodBodyToContainedExpressionTransitive;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_e1 = curMatch.Nodes[(int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e1];
			GRGEN_LGSP.LGSPNode node_e2 = curMatch.Nodes[(int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e2];
			GRGEN_LGSP.LGSPNode node_mb = curMatch.Nodes[(int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@mb];
			graph.SettingAddedNodeNames( LinkMethodBodyToContainedExpressionTransitive_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkMethodBodyToContainedExpressionTransitive_addedEdgeNames );
			@containedInMethodBody edge__edge2 = @containedInMethodBody.CreateEdge(graph, node_e1, node_e2);
			@containedInMethodBody edge__edge3 = @containedInMethodBody.CreateEdge(graph, node_mb, node_e2);
			return EmptyReturnElements;
		}
		private static String[] LinkMethodBodyToContainedExpressionTransitive_addedNodeNames = new String[] {  };
		private static String[] LinkMethodBodyToContainedExpressionTransitive_addedEdgeNames = new String[] { "_edge2", "_edge3" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_e1 = curMatch.Nodes[(int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e1];
			GRGEN_LGSP.LGSPNode node_e2 = curMatch.Nodes[(int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e2];
			GRGEN_LGSP.LGSPNode node_mb = curMatch.Nodes[(int)LinkMethodBodyToContainedExpressionTransitive_NodeNums.@mb];
			graph.SettingAddedNodeNames( LinkMethodBodyToContainedExpressionTransitive_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkMethodBodyToContainedExpressionTransitive_addedEdgeNames );
			@containedInMethodBody edge__edge2 = @containedInMethodBody.CreateEdge(graph, node_e1, node_e2);
			@containedInMethodBody edge__edge3 = @containedInMethodBody.CreateEdge(graph, node_mb, node_e2);
			return EmptyReturnElements;
		}

		static Rule_LinkMethodBodyToContainedExpressionTransitive() {
		}
	}

	public class Rule_LinkClassToFeature : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_LinkClassToFeature instance = null;
		public static Rule_LinkClassToFeature Instance { get { if (instance==null) { instance = new Rule_LinkClassToFeature(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] LinkClassToFeature_node_c_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] LinkClassToFeature_node_e_AllowedTypes = { NodeType_Entity.typeVar, NodeType_MethodBody.typeVar, NodeType_Expression.typeVar, NodeType_Declaration.typeVar, NodeType_Feature.typeVar, NodeType_MethodSignature.typeVar, NodeType_Attribute.typeVar, NodeType_Constant.typeVar, NodeType_Variabel.typeVar, };
		public static bool[] LinkClassToFeature_node_c_IsAllowedType = null;
		public static bool[] LinkClassToFeature_node_e_IsAllowedType = { false, true, true, true, true, false, true, true, true, true, true, };
		public static GRGEN_LIBGR.EdgeType[] LinkClassToFeature_edge__edge0_AllowedTypes = null;
		public static bool[] LinkClassToFeature_edge__edge0_IsAllowedType = null;
		public enum LinkClassToFeature_NodeNums { @c, @e, };
		public enum LinkClassToFeature_EdgeNums { @_edge0, };
		public enum LinkClassToFeature_VariableNums { };
		public enum LinkClassToFeature_SubNums { };
		public enum LinkClassToFeature_AltNums { };



		GRGEN_LGSP.PatternGraph pat_LinkClassToFeature;

		public static GRGEN_LIBGR.EdgeType[] LinkClassToFeature_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] LinkClassToFeature_neg_0_edge__edge0_IsAllowedType = null;
		public enum LinkClassToFeature_neg_0_NodeNums { @c, @e, };
		public enum LinkClassToFeature_neg_0_EdgeNums { @_edge0, };
		public enum LinkClassToFeature_neg_0_VariableNums { };
		public enum LinkClassToFeature_neg_0_SubNums { };
		public enum LinkClassToFeature_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph LinkClassToFeature_neg_0;


#if INITIAL_WARMUP
		public Rule_LinkClassToFeature()
#else
		private Rule_LinkClassToFeature()
#endif
		{
			name = "LinkClassToFeature";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] LinkClassToFeature_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkClassToFeature_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode LinkClassToFeature_node_c = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Class, "LinkClassToFeature_node_c", "c", LinkClassToFeature_node_c_AllowedTypes, LinkClassToFeature_node_c_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode LinkClassToFeature_node_e = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Entity, "LinkClassToFeature_node_e", "e", LinkClassToFeature_node_e_AllowedTypes, LinkClassToFeature_node_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge LinkClassToFeature_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@contains, "LinkClassToFeature_edge__edge0", "_edge0", LinkClassToFeature_edge__edge0_AllowedTypes, LinkClassToFeature_edge__edge0_IsAllowedType, 5.5F, -1);
			bool[,] LinkClassToFeature_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] LinkClassToFeature_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge LinkClassToFeature_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@containedInClass, "LinkClassToFeature_neg_0_edge__edge0", "_edge0", LinkClassToFeature_neg_0_edge__edge0_AllowedTypes, LinkClassToFeature_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			LinkClassToFeature_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"LinkClassToFeature_",
				false,
				new GRGEN_LGSP.PatternNode[] { LinkClassToFeature_node_c, LinkClassToFeature_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { LinkClassToFeature_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				LinkClassToFeature_neg_0_isNodeHomomorphicGlobal,
				LinkClassToFeature_neg_0_isEdgeHomomorphicGlobal
			);
			LinkClassToFeature_neg_0.edgeToSourceNode.Add(LinkClassToFeature_neg_0_edge__edge0, LinkClassToFeature_node_c);
			LinkClassToFeature_neg_0.edgeToTargetNode.Add(LinkClassToFeature_neg_0_edge__edge0, LinkClassToFeature_node_e);

			pat_LinkClassToFeature = new GRGEN_LGSP.PatternGraph(
				"LinkClassToFeature",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { LinkClassToFeature_node_c, LinkClassToFeature_node_e }, 
				new GRGEN_LGSP.PatternEdge[] { LinkClassToFeature_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { LinkClassToFeature_neg_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				LinkClassToFeature_isNodeHomomorphicGlobal,
				LinkClassToFeature_isEdgeHomomorphicGlobal
			);
			pat_LinkClassToFeature.edgeToSourceNode.Add(LinkClassToFeature_edge__edge0, LinkClassToFeature_node_c);
			pat_LinkClassToFeature.edgeToTargetNode.Add(LinkClassToFeature_edge__edge0, LinkClassToFeature_node_e);
			LinkClassToFeature_neg_0.embeddingGraph = pat_LinkClassToFeature;

			LinkClassToFeature_node_c.PointOfDefinition = pat_LinkClassToFeature;
			LinkClassToFeature_node_e.PointOfDefinition = pat_LinkClassToFeature;
			LinkClassToFeature_edge__edge0.PointOfDefinition = pat_LinkClassToFeature;
			LinkClassToFeature_neg_0_edge__edge0.PointOfDefinition = LinkClassToFeature_neg_0;

			patternGraph = pat_LinkClassToFeature;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch.Nodes[(int)LinkClassToFeature_NodeNums.@c];
			GRGEN_LGSP.LGSPNode node_e = curMatch.Nodes[(int)LinkClassToFeature_NodeNums.@e];
			graph.SettingAddedNodeNames( LinkClassToFeature_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkClassToFeature_addedEdgeNames );
			@containedInClass edge__edge1 = @containedInClass.CreateEdge(graph, node_c, node_e);
			return EmptyReturnElements;
		}
		private static String[] LinkClassToFeature_addedNodeNames = new String[] {  };
		private static String[] LinkClassToFeature_addedEdgeNames = new String[] { "_edge1" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatch curMatch)
		{
			GRGEN_LGSP.LGSPNode node_c = curMatch.Nodes[(int)LinkClassToFeature_NodeNums.@c];
			GRGEN_LGSP.LGSPNode node_e = curMatch.Nodes[(int)LinkClassToFeature_NodeNums.@e];
			graph.SettingAddedNodeNames( LinkClassToFeature_addedNodeNames );
			graph.SettingAddedEdgeNames( LinkClassToFeature_addedEdgeNames );
			@containedInClass edge__edge1 = @containedInClass.CreateEdge(graph, node_c, node_e);
			return EmptyReturnElements;
		}

		static Rule_LinkClassToFeature() {
		}
	}


    public class PatternAction_MultipleSubclasses : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_MultipleSubclasses(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_MultipleSubclasses.Instance.patternGraph;
        }

        public static PatternAction_MultipleSubclasses getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_MultipleSubclasses newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_MultipleSubclasses(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_MultipleSubclasses oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_MultipleSubclasses freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_MultipleSubclasses next = null;

        public GRGEN_LGSP.LGSPNode MultipleSubclasses_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset MultipleSubclasses_node_c 
            GRGEN_LGSP.LGSPNode candidate_MultipleSubclasses_node_c = MultipleSubclasses_node_c;
            // Push alternative matching task for MultipleSubclasses_alt_0
            AlternativeAction_MultipleSubclasses_alt_0 taskFor_alt_0 = AlternativeAction_MultipleSubclasses_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_MultipleSubclasses.MultipleSubclasses_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.MultipleSubclasses_node_c = candidate_MultipleSubclasses_node_c;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_MultipleSubclasses_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleSubclasses.MultipleSubclasses_NodeNums.@c] = candidate_MultipleSubclasses_node_c;
                    match.EmbeddedGraphs[((int)Pattern_MultipleSubclasses.MultipleSubclasses_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_MultipleSubclasses_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_MultipleSubclasses_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_MultipleSubclasses_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_MultipleSubclasses_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_MultipleSubclasses_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_MultipleSubclasses_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_MultipleSubclasses_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_MultipleSubclasses_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode MultipleSubclasses_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case MultipleSubclasses_alt_0_OneAndAgain 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_CaseNums.@OneAndAgain];
                // SubPreset MultipleSubclasses_node_c 
                GRGEN_LGSP.LGSPNode candidate_MultipleSubclasses_node_c = MultipleSubclasses_node_c;
                // Extend Outgoing MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 from MultipleSubclasses_node_c 
                GRGEN_LGSP.LGSPEdge head_candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 = candidate_MultipleSubclasses_node_c.outhead;
                if(head_candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 = head_candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0;
                    do
                    {
                        if(candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target MultipleSubclasses_alt_0_OneAndAgain_node_sub from MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub = candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.target;
                        if(candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.type.TypeID!=5) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub)))
                        {
                            continue;
                        }
                        if((candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern1
                        PatternAction_MultipleSubclasses taskFor__subpattern1 = PatternAction_MultipleSubclasses.getNewTask(graph, openTasks);
                        taskFor__subpattern1.MultipleSubclasses_node_c = candidate_MultipleSubclasses_node_c;
                        openTasks.Push(taskFor__subpattern1);
                        // Push subpattern matching task for _subpattern0
                        PatternAction_Subclass taskFor__subpattern0 = PatternAction_Subclass.getNewTask(graph, openTasks);
                        taskFor__subpattern0.Subclass_node_sub = candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub;
                        prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub = candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0;
                        prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 = candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_Subclass.releaseTask(taskFor__subpattern0);
                        // Pop subpattern matching task for _subpattern1
                        openTasks.Pop();
                        PatternAction_MultipleSubclasses.releaseTask(taskFor__subpattern1);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_OneAndAgain_NodeNums.@c] = candidate_MultipleSubclasses_node_c;
                                match.Nodes[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_OneAndAgain_NodeNums.@sub] = candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub;
                                match.Edges[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_OneAndAgain_EdgeNums.@_edge0] = candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_OneAndAgain_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                match.EmbeddedGraphs[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_OneAndAgain_SubNums.@_subpattern1] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags = candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0;
                                candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags = candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags = candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0;
                            candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags = candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub;
                            continue;
                        }
                        candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags = candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_node_sub;
                        candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags = candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0;
                    }
                    while( (candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 = candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0.outNext) != head_candidate_MultipleSubclasses_alt_0_OneAndAgain_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case MultipleSubclasses_alt_0_NoSubclassLeft 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_CaseNums.@NoSubclassLeft];
                // SubPreset MultipleSubclasses_node_c 
                GRGEN_LGSP.LGSPNode candidate_MultipleSubclasses_node_c = MultipleSubclasses_node_c;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    uint prev_neg_0__candidate_MultipleSubclasses_node_c;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev_neg_0__candidate_MultipleSubclasses_node_c = candidate_MultipleSubclasses_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_MultipleSubclasses_node_c.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev_neg_0__candidate_MultipleSubclasses_node_c = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_MultipleSubclasses_node_c) ? 1U : 0U;
                        if(prev_neg_0__candidate_MultipleSubclasses_node_c == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_MultipleSubclasses_node_c,candidate_MultipleSubclasses_node_c);
                    }
                    // Extend Outgoing MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 from MultipleSubclasses_node_c 
                    GRGEN_LGSP.LGSPEdge head_candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 = candidate_MultipleSubclasses_node_c.outhead;
                    if(head_candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 = head_candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            if((candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub from MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub = candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0.target;
                            if(candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub.type.TypeID!=5) {
                                continue;
                            }
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub)))
                            {
                                continue;
                            }
                            if((candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_MultipleSubclasses_node_c.flags = candidate_MultipleSubclasses_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_MultipleSubclasses_node_c;
                            } else { 
                                if(prev_neg_0__candidate_MultipleSubclasses_node_c == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_MultipleSubclasses_node_c);
                                }
                            }
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label0;
                        }
                        while( (candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 = candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0.outNext) != head_candidate_MultipleSubclasses_alt_0_NoSubclassLeft_neg_0_edge__edge0 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_MultipleSubclasses_node_c.flags = candidate_MultipleSubclasses_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_MultipleSubclasses_node_c;
                    } else { 
                        if(prev_neg_0__candidate_MultipleSubclasses_node_c == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_MultipleSubclasses_node_c);
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
                    Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_NoSubclassLeft_NodeNums.@c] = candidate_MultipleSubclasses_node_c;
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
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleSubclasses.MultipleSubclasses_alt_0_NoSubclassLeft_NodeNums.@c] = candidate_MultipleSubclasses_node_c;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
            openTasks.Push(this);
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Subclass_node_sub 
            GRGEN_LGSP.LGSPNode candidate_Subclass_node_sub = Subclass_node_sub;
            // Push subpattern matching task for _subpattern1
            PatternAction_MultipleFeatures taskFor__subpattern1 = PatternAction_MultipleFeatures.getNewTask(graph, openTasks);
            taskFor__subpattern1.MultipleFeatures_node_c = candidate_Subclass_node_sub;
            openTasks.Push(taskFor__subpattern1);
            // Push subpattern matching task for _subpattern0
            PatternAction_MultipleSubclasses taskFor__subpattern0 = PatternAction_MultipleSubclasses.getNewTask(graph, openTasks);
            taskFor__subpattern0.MultipleSubclasses_node_c = candidate_Subclass_node_sub;
            openTasks.Push(taskFor__subpattern0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_MultipleSubclasses.releaseTask(taskFor__subpattern0);
            // Pop subpattern matching task for _subpattern1
            openTasks.Pop();
            PatternAction_MultipleFeatures.releaseTask(taskFor__subpattern1);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_Subclass.Subclass_NodeNums.@sub] = candidate_Subclass_node_sub;
                    match.EmbeddedGraphs[(int)Pattern_Subclass.Subclass_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    match.EmbeddedGraphs[(int)Pattern_Subclass.Subclass_SubNums.@_subpattern1] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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

    public class PatternAction_MultipleFeatures : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_MultipleFeatures(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_MultipleFeatures.Instance.patternGraph;
        }

        public static PatternAction_MultipleFeatures getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_MultipleFeatures newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_MultipleFeatures(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_MultipleFeatures oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_MultipleFeatures freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_MultipleFeatures next = null;

        public GRGEN_LGSP.LGSPNode MultipleFeatures_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset MultipleFeatures_node_c 
            GRGEN_LGSP.LGSPNode candidate_MultipleFeatures_node_c = MultipleFeatures_node_c;
            // Push alternative matching task for MultipleFeatures_alt_0
            AlternativeAction_MultipleFeatures_alt_0 taskFor_alt_0 = AlternativeAction_MultipleFeatures_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_MultipleFeatures.MultipleFeatures_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.MultipleFeatures_node_c = candidate_MultipleFeatures_node_c;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_MultipleFeatures_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleFeatures.MultipleFeatures_NodeNums.@c] = candidate_MultipleFeatures_node_c;
                    match.EmbeddedGraphs[((int)Pattern_MultipleFeatures.MultipleFeatures_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_MultipleFeatures_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_MultipleFeatures_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_MultipleFeatures_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_MultipleFeatures_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_MultipleFeatures_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_MultipleFeatures_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_MultipleFeatures_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_MultipleFeatures_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode MultipleFeatures_node_c;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case MultipleFeatures_alt_0_OneAndAgain 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleFeatures.MultipleFeatures_alt_0_CaseNums.@OneAndAgain];
                // SubPreset MultipleFeatures_node_c 
                GRGEN_LGSP.LGSPNode candidate_MultipleFeatures_node_c = MultipleFeatures_node_c;
                // Push subpattern matching task for _subpattern1
                PatternAction_MultipleFeatures taskFor__subpattern1 = PatternAction_MultipleFeatures.getNewTask(graph, openTasks);
                taskFor__subpattern1.MultipleFeatures_node_c = candidate_MultipleFeatures_node_c;
                openTasks.Push(taskFor__subpattern1);
                // Push subpattern matching task for _subpattern0
                PatternAction_FeaturePattern taskFor__subpattern0 = PatternAction_FeaturePattern.getNewTask(graph, openTasks);
                taskFor__subpattern0.FeaturePattern_node_c = candidate_MultipleFeatures_node_c;
                openTasks.Push(taskFor__subpattern0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_FeaturePattern.releaseTask(taskFor__subpattern0);
                // Pop subpattern matching task for _subpattern1
                openTasks.Pop();
                PatternAction_MultipleFeatures.releaseTask(taskFor__subpattern1);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleFeatures.MultipleFeatures_alt_0_OneAndAgain_NodeNums.@c] = candidate_MultipleFeatures_node_c;
                        match.EmbeddedGraphs[(int)Pattern_MultipleFeatures.MultipleFeatures_alt_0_OneAndAgain_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        match.EmbeddedGraphs[(int)Pattern_MultipleFeatures.MultipleFeatures_alt_0_OneAndAgain_SubNums.@_subpattern1] = currentFoundPartialMatch.Pop();
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case MultipleFeatures_alt_0_NoFeatureLeft 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleFeatures.MultipleFeatures_alt_0_CaseNums.@NoFeatureLeft];
                // SubPreset MultipleFeatures_node_c 
                GRGEN_LGSP.LGSPNode candidate_MultipleFeatures_node_c = MultipleFeatures_node_c;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    // Extend Outgoing MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 from MultipleFeatures_node_c 
                    GRGEN_LGSP.LGSPEdge head_candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 = candidate_MultipleFeatures_node_c.outhead;
                    if(head_candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 = head_candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            if((candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f from MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f = candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0.target;
                            if(!NodeType_Feature.isMyType[candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f.type.TypeID]) {
                                continue;
                            }
                            if((candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_node_f.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 = candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0.outNext) != head_candidate_MultipleFeatures_alt_0_NoFeatureLeft_neg_0_edge__edge0 );
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
                    Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleFeatures.MultipleFeatures_alt_0_NoFeatureLeft_NodeNums.@c] = candidate_MultipleFeatures_node_c;
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
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleFeatures.MultipleFeatures_alt_0_NoFeatureLeft_NodeNums.@c] = candidate_MultipleFeatures_node_c;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
            openTasks.Push(this);
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset FeaturePattern_node_c 
            GRGEN_LGSP.LGSPNode candidate_FeaturePattern_node_c = FeaturePattern_node_c;
            // Push alternative matching task for FeaturePattern_alt_0
            AlternativeAction_FeaturePattern_alt_0 taskFor_alt_0 = AlternativeAction_FeaturePattern_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_FeaturePattern.FeaturePattern_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.FeaturePattern_node_c = candidate_FeaturePattern_node_c;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_FeaturePattern_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_NodeNums.@c] = candidate_FeaturePattern_node_c;
                    match.EmbeddedGraphs[((int)Pattern_FeaturePattern.FeaturePattern_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case FeaturePattern_alt_0_MethodBody 
            do {
                patternGraph = patternGraphs[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_CaseNums.@MethodBody];
                // SubPreset FeaturePattern_node_c 
                GRGEN_LGSP.LGSPNode candidate_FeaturePattern_node_c = FeaturePattern_node_c;
                // Extend Outgoing FeaturePattern_alt_0_MethodBody_edge__edge0 from FeaturePattern_node_c 
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = candidate_FeaturePattern_node_c.outhead;
                if(head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_MethodBody_node_b from FeaturePattern_alt_0_MethodBody_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_MethodBody_node_b = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.target;
                        if(candidate_FeaturePattern_alt_0_MethodBody_node_b.type.TypeID!=2) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_MethodBody_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern1
                        PatternAction_MultipleStatements taskFor__subpattern1 = PatternAction_MultipleStatements.getNewTask(graph, openTasks);
                        taskFor__subpattern1.MultipleStatements_node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        openTasks.Push(taskFor__subpattern1);
                        // Push subpattern matching task for _subpattern0
                        PatternAction_MultipleParameters taskFor__subpattern0 = PatternAction_MultipleParameters.getNewTask(graph, openTasks);
                        taskFor__subpattern0.MultipleParameters_node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b = candidate_FeaturePattern_alt_0_MethodBody_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_MethodBody_node_b.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                        prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_MultipleParameters.releaseTask(taskFor__subpattern0);
                        // Pop subpattern matching task for _subpattern1
                        openTasks.Pop();
                        PatternAction_MultipleStatements.releaseTask(taskFor__subpattern1);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodBody_NodeNums.@c] = candidate_FeaturePattern_node_c;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodBody_NodeNums.@b] = candidate_FeaturePattern_alt_0_MethodBody_node_b;
                                match.Edges[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodBody_EdgeNums.@_edge0] = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodBody_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                match.EmbeddedGraphs[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodBody_SubNums.@_subpattern1] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                                candidate_FeaturePattern_alt_0_MethodBody_node_b.flags = candidate_FeaturePattern_alt_0_MethodBody_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                            candidate_FeaturePattern_alt_0_MethodBody_node_b.flags = candidate_FeaturePattern_alt_0_MethodBody_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                            continue;
                        }
                        candidate_FeaturePattern_alt_0_MethodBody_node_b.flags = candidate_FeaturePattern_alt_0_MethodBody_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_node_b;
                        candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodBody_edge__edge0;
                    }
                    while( (candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 = candidate_FeaturePattern_alt_0_MethodBody_edge__edge0.outNext) != head_candidate_FeaturePattern_alt_0_MethodBody_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = candidate_FeaturePattern_node_c.outhead;
                if(head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_MethodSignature_node__node0 from FeaturePattern_alt_0_MethodSignature_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_MethodSignature_node__node0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.target;
                        if(candidate_FeaturePattern_alt_0_MethodSignature_node__node0.type.TypeID!=7) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodSignature_NodeNums.@c] = candidate_FeaturePattern_node_c;
                            match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodSignature_NodeNums.@_node0] = candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                            match.Edges[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodSignature_EdgeNums.@_edge0] = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
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
                        prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0 = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                        prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodSignature_NodeNums.@c] = candidate_FeaturePattern_node_c;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodSignature_NodeNums.@_node0] = candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                                match.Edges[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_MethodSignature_EdgeNums.@_edge0] = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                                candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                            candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                            continue;
                        }
                        candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags = candidate_FeaturePattern_alt_0_MethodSignature_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_node__node0;
                        candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0;
                    }
                    while( (candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 = candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0.outNext) != head_candidate_FeaturePattern_alt_0_MethodSignature_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_Variable_edge__edge0 = candidate_FeaturePattern_node_c.outhead;
                if(head_candidate_FeaturePattern_alt_0_Variable_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_Variable_edge__edge0 = head_candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_Variable_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_Variable_node__node0 from FeaturePattern_alt_0_Variable_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_Variable_node__node0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0.target;
                        if(candidate_FeaturePattern_alt_0_Variable_node__node0.type.TypeID!=10) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_Variable_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Variable_NodeNums.@c] = candidate_FeaturePattern_node_c;
                            match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Variable_NodeNums.@_node0] = candidate_FeaturePattern_alt_0_Variable_node__node0;
                            match.Edges[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Variable_EdgeNums.@_edge0] = candidate_FeaturePattern_alt_0_Variable_edge__edge0;
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
                        prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0 = candidate_FeaturePattern_alt_0_Variable_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_Variable_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                        prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Variable_NodeNums.@c] = candidate_FeaturePattern_node_c;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Variable_NodeNums.@_node0] = candidate_FeaturePattern_alt_0_Variable_node__node0;
                                match.Edges[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Variable_EdgeNums.@_edge0] = candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags = candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                                candidate_FeaturePattern_alt_0_Variable_node__node0.flags = candidate_FeaturePattern_alt_0_Variable_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags = candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                            candidate_FeaturePattern_alt_0_Variable_node__node0.flags = candidate_FeaturePattern_alt_0_Variable_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0;
                            continue;
                        }
                        candidate_FeaturePattern_alt_0_Variable_node__node0.flags = candidate_FeaturePattern_alt_0_Variable_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_node__node0;
                        candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags = candidate_FeaturePattern_alt_0_Variable_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Variable_edge__edge0;
                    }
                    while( (candidate_FeaturePattern_alt_0_Variable_edge__edge0 = candidate_FeaturePattern_alt_0_Variable_edge__edge0.outNext) != head_candidate_FeaturePattern_alt_0_Variable_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                GRGEN_LGSP.LGSPEdge head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = candidate_FeaturePattern_node_c.outhead;
                if(head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                    do
                    {
                        if(candidate_FeaturePattern_alt_0_Konstante_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target FeaturePattern_alt_0_Konstante_node__node0 from FeaturePattern_alt_0_Konstante_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_FeaturePattern_alt_0_Konstante_node__node0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.target;
                        if(candidate_FeaturePattern_alt_0_Konstante_node__node0.type.TypeID!=9) {
                            continue;
                        }
                        if((candidate_FeaturePattern_alt_0_Konstante_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Konstante_NodeNums.@c] = candidate_FeaturePattern_node_c;
                            match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Konstante_NodeNums.@_node0] = candidate_FeaturePattern_alt_0_Konstante_node__node0;
                            match.Edges[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Konstante_EdgeNums.@_edge0] = candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
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
                        prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0 = candidate_FeaturePattern_alt_0_Konstante_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_Konstante_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                        prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Konstante_NodeNums.@c] = candidate_FeaturePattern_node_c;
                                match.Nodes[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Konstante_NodeNums.@_node0] = candidate_FeaturePattern_alt_0_Konstante_node__node0;
                                match.Edges[(int)Pattern_FeaturePattern.FeaturePattern_alt_0_Konstante_EdgeNums.@_edge0] = candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                                candidate_FeaturePattern_alt_0_Konstante_node__node0.flags = candidate_FeaturePattern_alt_0_Konstante_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                            candidate_FeaturePattern_alt_0_Konstante_node__node0.flags = candidate_FeaturePattern_alt_0_Konstante_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0;
                            continue;
                        }
                        candidate_FeaturePattern_alt_0_Konstante_node__node0.flags = candidate_FeaturePattern_alt_0_Konstante_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_node__node0;
                        candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_FeaturePattern_alt_0_Konstante_edge__edge0;
                    }
                    while( (candidate_FeaturePattern_alt_0_Konstante_edge__edge0 = candidate_FeaturePattern_alt_0_Konstante_edge__edge0.outNext) != head_candidate_FeaturePattern_alt_0_Konstante_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_MultipleParameters : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_MultipleParameters(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_MultipleParameters.Instance.patternGraph;
        }

        public static PatternAction_MultipleParameters getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_MultipleParameters newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_MultipleParameters(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_MultipleParameters oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_MultipleParameters freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_MultipleParameters next = null;

        public GRGEN_LGSP.LGSPNode MultipleParameters_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset MultipleParameters_node_b 
            GRGEN_LGSP.LGSPNode candidate_MultipleParameters_node_b = MultipleParameters_node_b;
            // Push alternative matching task for MultipleParameters_alt_0
            AlternativeAction_MultipleParameters_alt_0 taskFor_alt_0 = AlternativeAction_MultipleParameters_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_MultipleParameters.MultipleParameters_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.MultipleParameters_node_b = candidate_MultipleParameters_node_b;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_MultipleParameters_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleParameters.MultipleParameters_NodeNums.@b] = candidate_MultipleParameters_node_b;
                    match.EmbeddedGraphs[((int)Pattern_MultipleParameters.MultipleParameters_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_MultipleParameters_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_MultipleParameters_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_MultipleParameters_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_MultipleParameters_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_MultipleParameters_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_MultipleParameters_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_MultipleParameters_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_MultipleParameters_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode MultipleParameters_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case MultipleParameters_alt_0_OneAndAgain 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleParameters.MultipleParameters_alt_0_CaseNums.@OneAndAgain];
                // SubPreset MultipleParameters_node_b 
                GRGEN_LGSP.LGSPNode candidate_MultipleParameters_node_b = MultipleParameters_node_b;
                // Push subpattern matching task for _subpattern1
                PatternAction_MultipleParameters taskFor__subpattern1 = PatternAction_MultipleParameters.getNewTask(graph, openTasks);
                taskFor__subpattern1.MultipleParameters_node_b = candidate_MultipleParameters_node_b;
                openTasks.Push(taskFor__subpattern1);
                // Push subpattern matching task for _subpattern0
                PatternAction_Parameter taskFor__subpattern0 = PatternAction_Parameter.getNewTask(graph, openTasks);
                taskFor__subpattern0.Parameter_node_b = candidate_MultipleParameters_node_b;
                openTasks.Push(taskFor__subpattern0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_Parameter.releaseTask(taskFor__subpattern0);
                // Pop subpattern matching task for _subpattern1
                openTasks.Pop();
                PatternAction_MultipleParameters.releaseTask(taskFor__subpattern1);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleParameters.MultipleParameters_alt_0_OneAndAgain_NodeNums.@b] = candidate_MultipleParameters_node_b;
                        match.EmbeddedGraphs[(int)Pattern_MultipleParameters.MultipleParameters_alt_0_OneAndAgain_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        match.EmbeddedGraphs[(int)Pattern_MultipleParameters.MultipleParameters_alt_0_OneAndAgain_SubNums.@_subpattern1] = currentFoundPartialMatch.Pop();
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case MultipleParameters_alt_0_NoStatementLeft 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleParameters.MultipleParameters_alt_0_CaseNums.@NoStatementLeft];
                // SubPreset MultipleParameters_node_b 
                GRGEN_LGSP.LGSPNode candidate_MultipleParameters_node_b = MultipleParameters_node_b;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    // Extend Outgoing MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 from MultipleParameters_node_b 
                    GRGEN_LGSP.LGSPEdge head_candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 = candidate_MultipleParameters_node_b.outhead;
                    if(head_candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 = head_candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            if((candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a from MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a = candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0.target;
                            if(!NodeType_Attribute.isMyType[candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a.type.TypeID]) {
                                continue;
                            }
                            if((candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_node_a.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label6;
                        }
                        while( (candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 = candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0.outNext) != head_candidate_MultipleParameters_alt_0_NoStatementLeft_neg_0_edge__edge0 );
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
                    Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleParameters.MultipleParameters_alt_0_NoStatementLeft_NodeNums.@b] = candidate_MultipleParameters_node_b;
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
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleParameters.MultipleParameters_alt_0_NoStatementLeft_NodeNums.@b] = candidate_MultipleParameters_node_b;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
            openTasks.Push(this);
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Parameter_node_b 
            GRGEN_LGSP.LGSPNode candidate_Parameter_node_b = Parameter_node_b;
            // Push alternative matching task for Parameter_alt_0
            AlternativeAction_Parameter_alt_0 taskFor_alt_0 = AlternativeAction_Parameter_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_Parameter.Parameter_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.Parameter_node_b = candidate_Parameter_node_b;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_Parameter_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_Parameter.Parameter_NodeNums.@b] = candidate_Parameter_node_b;
                    match.EmbeddedGraphs[((int)Pattern_Parameter.Parameter_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Parameter_alt_0_Variable 
            do {
                patternGraph = patternGraphs[(int)Pattern_Parameter.Parameter_alt_0_CaseNums.@Variable];
                // SubPreset Parameter_node_b 
                GRGEN_LGSP.LGSPNode candidate_Parameter_node_b = Parameter_node_b;
                // Extend Outgoing Parameter_alt_0_Variable_edge__edge0 from Parameter_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Parameter_alt_0_Variable_edge__edge0 = candidate_Parameter_node_b.outhead;
                if(head_candidate_Parameter_alt_0_Variable_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Parameter_alt_0_Variable_edge__edge0 = head_candidate_Parameter_alt_0_Variable_edge__edge0;
                    do
                    {
                        if(candidate_Parameter_alt_0_Variable_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Parameter_alt_0_Variable_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target Parameter_alt_0_Variable_node_v from Parameter_alt_0_Variable_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Parameter_alt_0_Variable_node_v = candidate_Parameter_alt_0_Variable_edge__edge0.target;
                        if(candidate_Parameter_alt_0_Variable_node_v.type.TypeID!=10) {
                            continue;
                        }
                        if((candidate_Parameter_alt_0_Variable_node_v.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Variable_NodeNums.@b] = candidate_Parameter_node_b;
                            match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Variable_NodeNums.@v] = candidate_Parameter_alt_0_Variable_node_v;
                            match.Edges[(int)Pattern_Parameter.Parameter_alt_0_Variable_EdgeNums.@_edge0] = candidate_Parameter_alt_0_Variable_edge__edge0;
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
                        prevGlobal__candidate_Parameter_alt_0_Variable_node_v = candidate_Parameter_alt_0_Variable_node_v.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Parameter_alt_0_Variable_node_v.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                        prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0 = candidate_Parameter_alt_0_Variable_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Parameter_alt_0_Variable_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Variable_NodeNums.@b] = candidate_Parameter_node_b;
                                match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Variable_NodeNums.@v] = candidate_Parameter_alt_0_Variable_node_v;
                                match.Edges[(int)Pattern_Parameter.Parameter_alt_0_Variable_EdgeNums.@_edge0] = candidate_Parameter_alt_0_Variable_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_Parameter_alt_0_Variable_edge__edge0.flags = candidate_Parameter_alt_0_Variable_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                                candidate_Parameter_alt_0_Variable_node_v.flags = candidate_Parameter_alt_0_Variable_node_v.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Variable_node_v;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_Parameter_alt_0_Variable_edge__edge0.flags = candidate_Parameter_alt_0_Variable_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                            candidate_Parameter_alt_0_Variable_node_v.flags = candidate_Parameter_alt_0_Variable_node_v.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Variable_node_v;
                            continue;
                        }
                        candidate_Parameter_alt_0_Variable_node_v.flags = candidate_Parameter_alt_0_Variable_node_v.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Variable_node_v;
                        candidate_Parameter_alt_0_Variable_edge__edge0.flags = candidate_Parameter_alt_0_Variable_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Variable_edge__edge0;
                    }
                    while( (candidate_Parameter_alt_0_Variable_edge__edge0 = candidate_Parameter_alt_0_Variable_edge__edge0.outNext) != head_candidate_Parameter_alt_0_Variable_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                GRGEN_LGSP.LGSPEdge head_candidate_Parameter_alt_0_Konstante_edge__edge0 = candidate_Parameter_node_b.outhead;
                if(head_candidate_Parameter_alt_0_Konstante_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Parameter_alt_0_Konstante_edge__edge0 = head_candidate_Parameter_alt_0_Konstante_edge__edge0;
                    do
                    {
                        if(candidate_Parameter_alt_0_Konstante_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Parameter_alt_0_Konstante_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target Parameter_alt_0_Konstante_node_c from Parameter_alt_0_Konstante_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Parameter_alt_0_Konstante_node_c = candidate_Parameter_alt_0_Konstante_edge__edge0.target;
                        if(candidate_Parameter_alt_0_Konstante_node_c.type.TypeID!=9) {
                            continue;
                        }
                        if((candidate_Parameter_alt_0_Konstante_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Konstante_NodeNums.@b] = candidate_Parameter_node_b;
                            match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Konstante_NodeNums.@c] = candidate_Parameter_alt_0_Konstante_node_c;
                            match.Edges[(int)Pattern_Parameter.Parameter_alt_0_Konstante_EdgeNums.@_edge0] = candidate_Parameter_alt_0_Konstante_edge__edge0;
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
                        prevGlobal__candidate_Parameter_alt_0_Konstante_node_c = candidate_Parameter_alt_0_Konstante_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Parameter_alt_0_Konstante_node_c.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                        prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0 = candidate_Parameter_alt_0_Konstante_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Parameter_alt_0_Konstante_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Konstante_NodeNums.@b] = candidate_Parameter_node_b;
                                match.Nodes[(int)Pattern_Parameter.Parameter_alt_0_Konstante_NodeNums.@c] = candidate_Parameter_alt_0_Konstante_node_c;
                                match.Edges[(int)Pattern_Parameter.Parameter_alt_0_Konstante_EdgeNums.@_edge0] = candidate_Parameter_alt_0_Konstante_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_Parameter_alt_0_Konstante_edge__edge0.flags = candidate_Parameter_alt_0_Konstante_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                                candidate_Parameter_alt_0_Konstante_node_c.flags = candidate_Parameter_alt_0_Konstante_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Konstante_node_c;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_Parameter_alt_0_Konstante_edge__edge0.flags = candidate_Parameter_alt_0_Konstante_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                            candidate_Parameter_alt_0_Konstante_node_c.flags = candidate_Parameter_alt_0_Konstante_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Konstante_node_c;
                            continue;
                        }
                        candidate_Parameter_alt_0_Konstante_node_c.flags = candidate_Parameter_alt_0_Konstante_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Konstante_node_c;
                        candidate_Parameter_alt_0_Konstante_edge__edge0.flags = candidate_Parameter_alt_0_Konstante_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Parameter_alt_0_Konstante_edge__edge0;
                    }
                    while( (candidate_Parameter_alt_0_Konstante_edge__edge0 = candidate_Parameter_alt_0_Konstante_edge__edge0.outNext) != head_candidate_Parameter_alt_0_Konstante_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_MultipleStatements : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_MultipleStatements(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_MultipleStatements.Instance.patternGraph;
        }

        public static PatternAction_MultipleStatements getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_MultipleStatements newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_MultipleStatements(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_MultipleStatements oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_MultipleStatements freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_MultipleStatements next = null;

        public GRGEN_LGSP.LGSPNode MultipleStatements_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset MultipleStatements_node_b 
            GRGEN_LGSP.LGSPNode candidate_MultipleStatements_node_b = MultipleStatements_node_b;
            // Push alternative matching task for MultipleStatements_alt_0
            AlternativeAction_MultipleStatements_alt_0 taskFor_alt_0 = AlternativeAction_MultipleStatements_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_MultipleStatements.MultipleStatements_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.MultipleStatements_node_b = candidate_MultipleStatements_node_b;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_MultipleStatements_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleStatements.MultipleStatements_NodeNums.@b] = candidate_MultipleStatements_node_b;
                    match.EmbeddedGraphs[((int)Pattern_MultipleStatements.MultipleStatements_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_MultipleStatements_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_MultipleStatements_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_MultipleStatements_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_MultipleStatements_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_MultipleStatements_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_MultipleStatements_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_MultipleStatements_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_MultipleStatements_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode MultipleStatements_node_b;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case MultipleStatements_alt_0_OneAndAgain 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleStatements.MultipleStatements_alt_0_CaseNums.@OneAndAgain];
                // SubPreset MultipleStatements_node_b 
                GRGEN_LGSP.LGSPNode candidate_MultipleStatements_node_b = MultipleStatements_node_b;
                // Push subpattern matching task for _subpattern1
                PatternAction_MultipleStatements taskFor__subpattern1 = PatternAction_MultipleStatements.getNewTask(graph, openTasks);
                taskFor__subpattern1.MultipleStatements_node_b = candidate_MultipleStatements_node_b;
                openTasks.Push(taskFor__subpattern1);
                // Push subpattern matching task for _subpattern0
                PatternAction_Statement taskFor__subpattern0 = PatternAction_Statement.getNewTask(graph, openTasks);
                taskFor__subpattern0.Statement_node_b = candidate_MultipleStatements_node_b;
                openTasks.Push(taskFor__subpattern0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_Statement.releaseTask(taskFor__subpattern0);
                // Pop subpattern matching task for _subpattern1
                openTasks.Pop();
                PatternAction_MultipleStatements.releaseTask(taskFor__subpattern1);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleStatements.MultipleStatements_alt_0_OneAndAgain_NodeNums.@b] = candidate_MultipleStatements_node_b;
                        match.EmbeddedGraphs[(int)Pattern_MultipleStatements.MultipleStatements_alt_0_OneAndAgain_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        match.EmbeddedGraphs[(int)Pattern_MultipleStatements.MultipleStatements_alt_0_OneAndAgain_SubNums.@_subpattern1] = currentFoundPartialMatch.Pop();
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case MultipleStatements_alt_0_NoStatementLeft 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleStatements.MultipleStatements_alt_0_CaseNums.@NoStatementLeft];
                // SubPreset MultipleStatements_node_b 
                GRGEN_LGSP.LGSPNode candidate_MultipleStatements_node_b = MultipleStatements_node_b;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    // Extend Outgoing MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 from MultipleStatements_node_b 
                    GRGEN_LGSP.LGSPEdge head_candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 = candidate_MultipleStatements_node_b.outhead;
                    if(head_candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 = head_candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            if((candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e from MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e = candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0.target;
                            if(candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e.type.TypeID!=3) {
                                continue;
                            }
                            if((candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label9;
                        }
                        while( (candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 = candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0.outNext) != head_candidate_MultipleStatements_alt_0_NoStatementLeft_neg_0_edge__edge0 );
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
                    Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleStatements.MultipleStatements_alt_0_NoStatementLeft_NodeNums.@b] = candidate_MultipleStatements_node_b;
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
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleStatements.MultipleStatements_alt_0_NoStatementLeft_NodeNums.@b] = candidate_MultipleStatements_node_b;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
            openTasks.Push(this);
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Statement_node_b 
            GRGEN_LGSP.LGSPNode candidate_Statement_node_b = Statement_node_b;
            // Push alternative matching task for Statement_alt_0
            AlternativeAction_Statement_alt_0 taskFor_alt_0 = AlternativeAction_Statement_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_Statement.Statement_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.Statement_node_b = candidate_Statement_node_b;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_Statement_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_Statement.Statement_NodeNums.@b] = candidate_Statement_node_b;
                    match.EmbeddedGraphs[((int)Pattern_Statement.Statement_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Statement_alt_0_Assignment 
            do {
                patternGraph = patternGraphs[(int)Pattern_Statement.Statement_alt_0_CaseNums.@Assignment];
                // SubPreset Statement_node_b 
                GRGEN_LGSP.LGSPNode candidate_Statement_node_b = Statement_node_b;
                // Extend Outgoing Statement_alt_0_Assignment_edge__edge0 from Statement_node_b 
                GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Assignment_edge__edge0 = candidate_Statement_node_b.outhead;
                if(head_candidate_Statement_alt_0_Assignment_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Assignment_edge__edge0 = head_candidate_Statement_alt_0_Assignment_edge__edge0;
                    do
                    {
                        if(candidate_Statement_alt_0_Assignment_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Statement_alt_0_Assignment_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target Statement_alt_0_Assignment_node_e from Statement_alt_0_Assignment_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Statement_alt_0_Assignment_node_e = candidate_Statement_alt_0_Assignment_edge__edge0.target;
                        if(candidate_Statement_alt_0_Assignment_node_e.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Statement_alt_0_Assignment_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Extend Outgoing Statement_alt_0_Assignment_edge__edge1 from Statement_alt_0_Assignment_node_e 
                        GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Assignment_edge__edge1 = candidate_Statement_alt_0_Assignment_node_e.outhead;
                        if(head_candidate_Statement_alt_0_Assignment_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Assignment_edge__edge1 = head_candidate_Statement_alt_0_Assignment_edge__edge1;
                            do
                            {
                                if(candidate_Statement_alt_0_Assignment_edge__edge1.type.TypeID!=8) {
                                    continue;
                                }
                                if((candidate_Statement_alt_0_Assignment_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Push subpattern matching task for _subpattern0
                                PatternAction_ExpressionPattern taskFor__subpattern0 = PatternAction_ExpressionPattern.getNewTask(graph, openTasks);
                                taskFor__subpattern0.ExpressionPattern_node_e = candidate_Statement_alt_0_Assignment_node_e;
                                openTasks.Push(taskFor__subpattern0);
                                uint prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                prevGlobal__candidate_Statement_alt_0_Assignment_node_e = candidate_Statement_alt_0_Assignment_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Assignment_node_e.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0 = candidate_Statement_alt_0_Assignment_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Assignment_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1 = candidate_Statement_alt_0_Assignment_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Assignment_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Pop subpattern matching task for _subpattern0
                                openTasks.Pop();
                                PatternAction_ExpressionPattern.releaseTask(taskFor__subpattern0);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[2], new object[0], new GRGEN_LGSP.LGSPMatch[1+0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Pattern_Statement.Statement_alt_0_Assignment_NodeNums.@b] = candidate_Statement_node_b;
                                        match.Nodes[(int)Pattern_Statement.Statement_alt_0_Assignment_NodeNums.@e] = candidate_Statement_alt_0_Assignment_node_e;
                                        match.Edges[(int)Pattern_Statement.Statement_alt_0_Assignment_EdgeNums.@_edge0] = candidate_Statement_alt_0_Assignment_edge__edge0;
                                        match.Edges[(int)Pattern_Statement.Statement_alt_0_Assignment_EdgeNums.@_edge1] = candidate_Statement_alt_0_Assignment_edge__edge1;
                                        match.EmbeddedGraphs[(int)Pattern_Statement.Statement_alt_0_Assignment_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                                    } else {
                                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_Statement_alt_0_Assignment_edge__edge1.flags = candidate_Statement_alt_0_Assignment_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                        candidate_Statement_alt_0_Assignment_edge__edge0.flags = candidate_Statement_alt_0_Assignment_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                        candidate_Statement_alt_0_Assignment_node_e.flags = candidate_Statement_alt_0_Assignment_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_Statement_alt_0_Assignment_edge__edge1.flags = candidate_Statement_alt_0_Assignment_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                                    candidate_Statement_alt_0_Assignment_edge__edge0.flags = candidate_Statement_alt_0_Assignment_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                    candidate_Statement_alt_0_Assignment_node_e.flags = candidate_Statement_alt_0_Assignment_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                    continue;
                                }
                                candidate_Statement_alt_0_Assignment_node_e.flags = candidate_Statement_alt_0_Assignment_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_node_e;
                                candidate_Statement_alt_0_Assignment_edge__edge0.flags = candidate_Statement_alt_0_Assignment_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge0;
                                candidate_Statement_alt_0_Assignment_edge__edge1.flags = candidate_Statement_alt_0_Assignment_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Assignment_edge__edge1;
                            }
                            while( (candidate_Statement_alt_0_Assignment_edge__edge1 = candidate_Statement_alt_0_Assignment_edge__edge1.outNext) != head_candidate_Statement_alt_0_Assignment_edge__edge1 );
                        }
                    }
                    while( (candidate_Statement_alt_0_Assignment_edge__edge0 = candidate_Statement_alt_0_Assignment_edge__edge0.outNext) != head_candidate_Statement_alt_0_Assignment_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Call_edge__edge0 = candidate_Statement_node_b.outhead;
                if(head_candidate_Statement_alt_0_Call_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Call_edge__edge0 = head_candidate_Statement_alt_0_Call_edge__edge0;
                    do
                    {
                        if(candidate_Statement_alt_0_Call_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Statement_alt_0_Call_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target Statement_alt_0_Call_node_e from Statement_alt_0_Call_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Statement_alt_0_Call_node_e = candidate_Statement_alt_0_Call_edge__edge0.target;
                        if(candidate_Statement_alt_0_Call_node_e.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Statement_alt_0_Call_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Extend Outgoing Statement_alt_0_Call_edge__edge1 from Statement_alt_0_Call_node_e 
                        GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Call_edge__edge1 = candidate_Statement_alt_0_Call_node_e.outhead;
                        if(head_candidate_Statement_alt_0_Call_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Call_edge__edge1 = head_candidate_Statement_alt_0_Call_edge__edge1;
                            do
                            {
                                if(candidate_Statement_alt_0_Call_edge__edge1.type.TypeID!=9) {
                                    continue;
                                }
                                if((candidate_Statement_alt_0_Call_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Push subpattern matching task for _subpattern0
                                PatternAction_MultipleExpressions taskFor__subpattern0 = PatternAction_MultipleExpressions.getNewTask(graph, openTasks);
                                taskFor__subpattern0.MultipleExpressions_node_e = candidate_Statement_alt_0_Call_node_e;
                                openTasks.Push(taskFor__subpattern0);
                                uint prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                prevGlobal__candidate_Statement_alt_0_Call_node_e = candidate_Statement_alt_0_Call_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Call_node_e.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                prevGlobal__candidate_Statement_alt_0_Call_edge__edge0 = candidate_Statement_alt_0_Call_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Call_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                prevGlobal__candidate_Statement_alt_0_Call_edge__edge1 = candidate_Statement_alt_0_Call_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Call_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Pop subpattern matching task for _subpattern0
                                openTasks.Pop();
                                PatternAction_MultipleExpressions.releaseTask(taskFor__subpattern0);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[2], new object[0], new GRGEN_LGSP.LGSPMatch[1+0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Pattern_Statement.Statement_alt_0_Call_NodeNums.@b] = candidate_Statement_node_b;
                                        match.Nodes[(int)Pattern_Statement.Statement_alt_0_Call_NodeNums.@e] = candidate_Statement_alt_0_Call_node_e;
                                        match.Edges[(int)Pattern_Statement.Statement_alt_0_Call_EdgeNums.@_edge0] = candidate_Statement_alt_0_Call_edge__edge0;
                                        match.Edges[(int)Pattern_Statement.Statement_alt_0_Call_EdgeNums.@_edge1] = candidate_Statement_alt_0_Call_edge__edge1;
                                        match.EmbeddedGraphs[(int)Pattern_Statement.Statement_alt_0_Call_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                                    } else {
                                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_Statement_alt_0_Call_edge__edge1.flags = candidate_Statement_alt_0_Call_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                        candidate_Statement_alt_0_Call_edge__edge0.flags = candidate_Statement_alt_0_Call_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                        candidate_Statement_alt_0_Call_node_e.flags = candidate_Statement_alt_0_Call_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_Statement_alt_0_Call_edge__edge1.flags = candidate_Statement_alt_0_Call_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                                    candidate_Statement_alt_0_Call_edge__edge0.flags = candidate_Statement_alt_0_Call_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                    candidate_Statement_alt_0_Call_node_e.flags = candidate_Statement_alt_0_Call_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                    continue;
                                }
                                candidate_Statement_alt_0_Call_node_e.flags = candidate_Statement_alt_0_Call_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_node_e;
                                candidate_Statement_alt_0_Call_edge__edge0.flags = candidate_Statement_alt_0_Call_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge0;
                                candidate_Statement_alt_0_Call_edge__edge1.flags = candidate_Statement_alt_0_Call_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Call_edge__edge1;
                            }
                            while( (candidate_Statement_alt_0_Call_edge__edge1 = candidate_Statement_alt_0_Call_edge__edge1.outNext) != head_candidate_Statement_alt_0_Call_edge__edge1 );
                        }
                    }
                    while( (candidate_Statement_alt_0_Call_edge__edge0 = candidate_Statement_alt_0_Call_edge__edge0.outNext) != head_candidate_Statement_alt_0_Call_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Return_edge__edge0 = candidate_Statement_node_b.outhead;
                if(head_candidate_Statement_alt_0_Return_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Return_edge__edge0 = head_candidate_Statement_alt_0_Return_edge__edge0;
                    do
                    {
                        if(candidate_Statement_alt_0_Return_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Statement_alt_0_Return_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target Statement_alt_0_Return_node_e from Statement_alt_0_Return_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_Statement_alt_0_Return_node_e = candidate_Statement_alt_0_Return_edge__edge0.target;
                        if(candidate_Statement_alt_0_Return_node_e.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_Statement_alt_0_Return_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Extend Outgoing Statement_alt_0_Return_edge__edge1 from Statement_alt_0_Return_node_e 
                        GRGEN_LGSP.LGSPEdge head_candidate_Statement_alt_0_Return_edge__edge1 = candidate_Statement_alt_0_Return_node_e.outhead;
                        if(head_candidate_Statement_alt_0_Return_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_Statement_alt_0_Return_edge__edge1 = head_candidate_Statement_alt_0_Return_edge__edge1;
                            do
                            {
                                if(candidate_Statement_alt_0_Return_edge__edge1.type.TypeID!=7) {
                                    continue;
                                }
                                if((candidate_Statement_alt_0_Return_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[2], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                                    match.patternGraph = patternGraph;
                                    match.Nodes[(int)Pattern_Statement.Statement_alt_0_Return_NodeNums.@b] = candidate_Statement_node_b;
                                    match.Nodes[(int)Pattern_Statement.Statement_alt_0_Return_NodeNums.@e] = candidate_Statement_alt_0_Return_node_e;
                                    match.Edges[(int)Pattern_Statement.Statement_alt_0_Return_EdgeNums.@_edge0] = candidate_Statement_alt_0_Return_edge__edge0;
                                    match.Edges[(int)Pattern_Statement.Statement_alt_0_Return_EdgeNums.@_edge1] = candidate_Statement_alt_0_Return_edge__edge1;
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
                                prevGlobal__candidate_Statement_alt_0_Return_node_e = candidate_Statement_alt_0_Return_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Return_node_e.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                prevGlobal__candidate_Statement_alt_0_Return_edge__edge0 = candidate_Statement_alt_0_Return_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Return_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                                prevGlobal__candidate_Statement_alt_0_Return_edge__edge1 = candidate_Statement_alt_0_Return_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_Statement_alt_0_Return_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[2], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Pattern_Statement.Statement_alt_0_Return_NodeNums.@b] = candidate_Statement_node_b;
                                        match.Nodes[(int)Pattern_Statement.Statement_alt_0_Return_NodeNums.@e] = candidate_Statement_alt_0_Return_node_e;
                                        match.Edges[(int)Pattern_Statement.Statement_alt_0_Return_EdgeNums.@_edge0] = candidate_Statement_alt_0_Return_edge__edge0;
                                        match.Edges[(int)Pattern_Statement.Statement_alt_0_Return_EdgeNums.@_edge1] = candidate_Statement_alt_0_Return_edge__edge1;
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                                    } else {
                                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_Statement_alt_0_Return_edge__edge1.flags = candidate_Statement_alt_0_Return_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                                        candidate_Statement_alt_0_Return_edge__edge0.flags = candidate_Statement_alt_0_Return_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                        candidate_Statement_alt_0_Return_node_e.flags = candidate_Statement_alt_0_Return_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_node_e;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_Statement_alt_0_Return_edge__edge1.flags = candidate_Statement_alt_0_Return_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                                    candidate_Statement_alt_0_Return_edge__edge0.flags = candidate_Statement_alt_0_Return_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                    candidate_Statement_alt_0_Return_node_e.flags = candidate_Statement_alt_0_Return_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_node_e;
                                    continue;
                                }
                                candidate_Statement_alt_0_Return_node_e.flags = candidate_Statement_alt_0_Return_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_node_e;
                                candidate_Statement_alt_0_Return_edge__edge0.flags = candidate_Statement_alt_0_Return_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge0;
                                candidate_Statement_alt_0_Return_edge__edge1.flags = candidate_Statement_alt_0_Return_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Statement_alt_0_Return_edge__edge1;
                            }
                            while( (candidate_Statement_alt_0_Return_edge__edge1 = candidate_Statement_alt_0_Return_edge__edge1.outNext) != head_candidate_Statement_alt_0_Return_edge__edge1 );
                        }
                    }
                    while( (candidate_Statement_alt_0_Return_edge__edge0 = candidate_Statement_alt_0_Return_edge__edge0.outNext) != head_candidate_Statement_alt_0_Return_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_MultipleExpressions : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_MultipleExpressions(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_MultipleExpressions.Instance.patternGraph;
        }

        public static PatternAction_MultipleExpressions getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_MultipleExpressions newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_MultipleExpressions(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_MultipleExpressions oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_MultipleExpressions freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_MultipleExpressions next = null;

        public GRGEN_LGSP.LGSPNode MultipleExpressions_node_e;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset MultipleExpressions_node_e 
            GRGEN_LGSP.LGSPNode candidate_MultipleExpressions_node_e = MultipleExpressions_node_e;
            // Push alternative matching task for MultipleExpressions_alt_0
            AlternativeAction_MultipleExpressions_alt_0 taskFor_alt_0 = AlternativeAction_MultipleExpressions_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_MultipleExpressions.MultipleExpressions_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.MultipleExpressions_node_e = candidate_MultipleExpressions_node_e;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_MultipleExpressions_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleExpressions.MultipleExpressions_NodeNums.@e] = candidate_MultipleExpressions_node_e;
                    match.EmbeddedGraphs[((int)Pattern_MultipleExpressions.MultipleExpressions_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_MultipleExpressions_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_MultipleExpressions_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_MultipleExpressions_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_MultipleExpressions_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_MultipleExpressions_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_MultipleExpressions_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_MultipleExpressions_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_MultipleExpressions_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode MultipleExpressions_node_e;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case MultipleExpressions_alt_0_OneAndAgain 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleExpressions.MultipleExpressions_alt_0_CaseNums.@OneAndAgain];
                // SubPreset MultipleExpressions_node_e 
                GRGEN_LGSP.LGSPNode candidate_MultipleExpressions_node_e = MultipleExpressions_node_e;
                // Push subpattern matching task for _subpattern1
                PatternAction_MultipleExpressions taskFor__subpattern1 = PatternAction_MultipleExpressions.getNewTask(graph, openTasks);
                taskFor__subpattern1.MultipleExpressions_node_e = candidate_MultipleExpressions_node_e;
                openTasks.Push(taskFor__subpattern1);
                // Push subpattern matching task for _subpattern0
                PatternAction_ExpressionPattern taskFor__subpattern0 = PatternAction_ExpressionPattern.getNewTask(graph, openTasks);
                taskFor__subpattern0.ExpressionPattern_node_e = candidate_MultipleExpressions_node_e;
                openTasks.Push(taskFor__subpattern0);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ExpressionPattern.releaseTask(taskFor__subpattern0);
                // Pop subpattern matching task for _subpattern1
                openTasks.Pop();
                PatternAction_MultipleExpressions.releaseTask(taskFor__subpattern1);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleExpressions.MultipleExpressions_alt_0_OneAndAgain_NodeNums.@e] = candidate_MultipleExpressions_node_e;
                        match.EmbeddedGraphs[(int)Pattern_MultipleExpressions.MultipleExpressions_alt_0_OneAndAgain_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        match.EmbeddedGraphs[(int)Pattern_MultipleExpressions.MultipleExpressions_alt_0_OneAndAgain_SubNums.@_subpattern1] = currentFoundPartialMatch.Pop();
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case MultipleExpressions_alt_0_NoExpressionLeft 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleExpressions.MultipleExpressions_alt_0_CaseNums.@NoExpressionLeft];
                // SubPreset MultipleExpressions_node_e 
                GRGEN_LGSP.LGSPNode candidate_MultipleExpressions_node_e = MultipleExpressions_node_e;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    uint prev_neg_0__candidate_MultipleExpressions_node_e;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev_neg_0__candidate_MultipleExpressions_node_e = candidate_MultipleExpressions_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_MultipleExpressions_node_e.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev_neg_0__candidate_MultipleExpressions_node_e = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_MultipleExpressions_node_e) ? 1U : 0U;
                        if(prev_neg_0__candidate_MultipleExpressions_node_e == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_MultipleExpressions_node_e,candidate_MultipleExpressions_node_e);
                    }
                    // Extend Outgoing MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 from MultipleExpressions_node_e 
                    GRGEN_LGSP.LGSPEdge head_candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 = candidate_MultipleExpressions_node_e.outhead;
                    if(head_candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 = head_candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0.type.TypeID!=3) {
                                continue;
                            }
                            if((candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub from MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub = candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0.target;
                            if(candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub.type.TypeID!=3) {
                                continue;
                            }
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub)))
                            {
                                continue;
                            }
                            if((candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_MultipleExpressions_node_e.flags = candidate_MultipleExpressions_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_MultipleExpressions_node_e;
                            } else { 
                                if(prev_neg_0__candidate_MultipleExpressions_node_e == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_MultipleExpressions_node_e);
                                }
                            }
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label12;
                        }
                        while( (candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 = candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0.outNext) != head_candidate_MultipleExpressions_alt_0_NoExpressionLeft_neg_0_edge__edge0 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_MultipleExpressions_node_e.flags = candidate_MultipleExpressions_node_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_MultipleExpressions_node_e;
                    } else { 
                        if(prev_neg_0__candidate_MultipleExpressions_node_e == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_MultipleExpressions_node_e);
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
                    Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleExpressions.MultipleExpressions_alt_0_NoExpressionLeft_NodeNums.@e] = candidate_MultipleExpressions_node_e;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label13;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleExpressions.MultipleExpressions_alt_0_NoExpressionLeft_NodeNums.@e] = candidate_MultipleExpressions_node_e;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                    goto label14;
                }
label12: ;
label13: ;
label14: ;
            } while(false);
            openTasks.Push(this);
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ExpressionPattern_node_e 
            GRGEN_LGSP.LGSPNode candidate_ExpressionPattern_node_e = ExpressionPattern_node_e;
            // Extend Outgoing ExpressionPattern_edge__edge0 from ExpressionPattern_node_e 
            GRGEN_LGSP.LGSPEdge head_candidate_ExpressionPattern_edge__edge0 = candidate_ExpressionPattern_node_e.outhead;
            if(head_candidate_ExpressionPattern_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_ExpressionPattern_edge__edge0 = head_candidate_ExpressionPattern_edge__edge0;
                do
                {
                    if(candidate_ExpressionPattern_edge__edge0.type.TypeID!=3) {
                        continue;
                    }
                    if((candidate_ExpressionPattern_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Implicit Target ExpressionPattern_node_sub from ExpressionPattern_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_ExpressionPattern_node_sub = candidate_ExpressionPattern_edge__edge0.target;
                    if(candidate_ExpressionPattern_node_sub.type.TypeID!=3) {
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_ExpressionPattern_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ExpressionPattern_node_sub)))
                    {
                        continue;
                    }
                    if((candidate_ExpressionPattern_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Push alternative matching task for ExpressionPattern_alt_0
                    AlternativeAction_ExpressionPattern_alt_0 taskFor_alt_0 = AlternativeAction_ExpressionPattern_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ExpressionPattern.ExpressionPattern_AltNums.@alt_0].alternativeCases);
                    taskFor_alt_0.ExpressionPattern_node_sub = candidate_ExpressionPattern_node_sub;
                    openTasks.Push(taskFor_alt_0);
                    uint prevGlobal__candidate_ExpressionPattern_node_sub;
                    prevGlobal__candidate_ExpressionPattern_node_sub = candidate_ExpressionPattern_node_sub.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_ExpressionPattern_node_sub.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    uint prevGlobal__candidate_ExpressionPattern_edge__edge0;
                    prevGlobal__candidate_ExpressionPattern_edge__edge0 = candidate_ExpressionPattern_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_ExpressionPattern_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for alt_0
                    openTasks.Pop();
                    AlternativeAction_ExpressionPattern_alt_0.releaseTask(taskFor_alt_0);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_ExpressionPattern.ExpressionPattern_NodeNums.@e] = candidate_ExpressionPattern_node_e;
                            match.Nodes[(int)Pattern_ExpressionPattern.ExpressionPattern_NodeNums.@sub] = candidate_ExpressionPattern_node_sub;
                            match.Edges[(int)Pattern_ExpressionPattern.ExpressionPattern_EdgeNums.@_edge0] = candidate_ExpressionPattern_edge__edge0;
                            match.EmbeddedGraphs[((int)Pattern_ExpressionPattern.ExpressionPattern_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                        } else {
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_ExpressionPattern_edge__edge0.flags = candidate_ExpressionPattern_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_edge__edge0;
                            candidate_ExpressionPattern_node_sub.flags = candidate_ExpressionPattern_node_sub.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_node_sub;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_ExpressionPattern_edge__edge0.flags = candidate_ExpressionPattern_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_edge__edge0;
                        candidate_ExpressionPattern_node_sub.flags = candidate_ExpressionPattern_node_sub.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_node_sub;
                        continue;
                    }
                    candidate_ExpressionPattern_node_sub.flags = candidate_ExpressionPattern_node_sub.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_node_sub;
                    candidate_ExpressionPattern_edge__edge0.flags = candidate_ExpressionPattern_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_edge__edge0;
                }
                while( (candidate_ExpressionPattern_edge__edge0 = candidate_ExpressionPattern_edge__edge0.outNext) != head_candidate_ExpressionPattern_edge__edge0 );
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ExpressionPattern_alt_0_Call 
            do {
                patternGraph = patternGraphs[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_CaseNums.@Call];
                // SubPreset ExpressionPattern_node_sub 
                GRGEN_LGSP.LGSPNode candidate_ExpressionPattern_node_sub = ExpressionPattern_node_sub;
                // Extend Outgoing ExpressionPattern_alt_0_Call_edge__edge0 from ExpressionPattern_node_sub 
                GRGEN_LGSP.LGSPEdge head_candidate_ExpressionPattern_alt_0_Call_edge__edge0 = candidate_ExpressionPattern_node_sub.outhead;
                if(head_candidate_ExpressionPattern_alt_0_Call_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ExpressionPattern_alt_0_Call_edge__edge0 = head_candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                    do
                    {
                        if(candidate_ExpressionPattern_alt_0_Call_edge__edge0.type.TypeID!=9) {
                            continue;
                        }
                        if((candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_MultipleExpressions taskFor__subpattern0 = PatternAction_MultipleExpressions.getNewTask(graph, openTasks);
                        taskFor__subpattern0.MultipleExpressions_node_e = candidate_ExpressionPattern_node_sub;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                        prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0 = candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_MultipleExpressions.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_Call_NodeNums.@sub] = candidate_ExpressionPattern_node_sub;
                                match.Edges[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_Call_EdgeNums.@_edge0] = candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_Call_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                            continue;
                        }
                        candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags = candidate_ExpressionPattern_alt_0_Call_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_alt_0_Call_edge__edge0;
                    }
                    while( (candidate_ExpressionPattern_alt_0_Call_edge__edge0 = candidate_ExpressionPattern_alt_0_Call_edge__edge0.outNext) != head_candidate_ExpressionPattern_alt_0_Call_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                GRGEN_LGSP.LGSPEdge head_candidate_ExpressionPattern_alt_0_Use_edge__edge0 = candidate_ExpressionPattern_node_sub.outhead;
                if(head_candidate_ExpressionPattern_alt_0_Use_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_ExpressionPattern_alt_0_Use_edge__edge0 = head_candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                    do
                    {
                        if(candidate_ExpressionPattern_alt_0_Use_edge__edge0.type.TypeID!=7) {
                            continue;
                        }
                        if((candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_Use_NodeNums.@sub] = candidate_ExpressionPattern_node_sub;
                            match.Edges[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_Use_EdgeNums.@_edge0] = candidate_ExpressionPattern_alt_0_Use_edge__edge0;
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
                        prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0 = candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[1], new GRGEN_LGSP.LGSPEdge[1], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_Use_NodeNums.@sub] = candidate_ExpressionPattern_node_sub;
                                match.Edges[(int)Pattern_ExpressionPattern.ExpressionPattern_alt_0_Use_EdgeNums.@_edge0] = candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags = candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags = candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                            continue;
                        }
                        candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags = candidate_ExpressionPattern_alt_0_Use_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ExpressionPattern_alt_0_Use_edge__edge0;
                    }
                    while( (candidate_ExpressionPattern_alt_0_Use_edge__edge0 = candidate_ExpressionPattern_alt_0_Use_edge__edge0.outNext) != head_candidate_ExpressionPattern_alt_0_Use_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_MultipleBodies : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_MultipleBodies(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_MultipleBodies.Instance.patternGraph;
        }

        public static PatternAction_MultipleBodies getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_MultipleBodies newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_MultipleBodies(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_MultipleBodies oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_MultipleBodies freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_MultipleBodies next = null;

        public GRGEN_LGSP.LGSPNode MultipleBodies_node_m5;
        public GRGEN_LGSP.LGSPNode MultipleBodies_node_c1;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset MultipleBodies_node_m5 
            GRGEN_LGSP.LGSPNode candidate_MultipleBodies_node_m5 = MultipleBodies_node_m5;
            // SubPreset MultipleBodies_node_c1 
            GRGEN_LGSP.LGSPNode candidate_MultipleBodies_node_c1 = MultipleBodies_node_c1;
            // Push alternative matching task for MultipleBodies_alt_0
            AlternativeAction_MultipleBodies_alt_0 taskFor_alt_0 = AlternativeAction_MultipleBodies_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_MultipleBodies.MultipleBodies_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.MultipleBodies_node_m5 = candidate_MultipleBodies_node_m5;
            taskFor_alt_0.MultipleBodies_node_c1 = candidate_MultipleBodies_node_c1;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_MultipleBodies_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_NodeNums.@m5] = candidate_MultipleBodies_node_m5;
                    match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_NodeNums.@c1] = candidate_MultipleBodies_node_c1;
                    match.EmbeddedGraphs[((int)Pattern_MultipleBodies.MultipleBodies_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_MultipleBodies_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_MultipleBodies_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_MultipleBodies_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_MultipleBodies_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_MultipleBodies_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_MultipleBodies_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_MultipleBodies_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_MultipleBodies_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode MultipleBodies_node_m5;
        public GRGEN_LGSP.LGSPNode MultipleBodies_node_c1;
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case MultipleBodies_alt_0_Rek 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_CaseNums.@Rek];
                // SubPreset MultipleBodies_node_m5 
                GRGEN_LGSP.LGSPNode candidate_MultipleBodies_node_m5 = MultipleBodies_node_m5;
                // SubPreset MultipleBodies_node_c1 
                GRGEN_LGSP.LGSPNode candidate_MultipleBodies_node_c1 = MultipleBodies_node_c1;
                // Push subpattern matching task for mb
                PatternAction_MultipleBodies taskFor_mb = PatternAction_MultipleBodies.getNewTask(graph, openTasks);
                taskFor_mb.MultipleBodies_node_m5 = candidate_MultipleBodies_node_m5;
                taskFor_mb.MultipleBodies_node_c1 = candidate_MultipleBodies_node_c1;
                openTasks.Push(taskFor_mb);
                // Push subpattern matching task for b
                PatternAction_Body taskFor_b = PatternAction_Body.getNewTask(graph, openTasks);
                taskFor_b.Body_node_m5 = candidate_MultipleBodies_node_m5;
                taskFor_b.Body_node_c1 = candidate_MultipleBodies_node_c1;
                openTasks.Push(taskFor_b);
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for b
                openTasks.Pop();
                PatternAction_Body.releaseTask(taskFor_b);
                // Pop subpattern matching task for mb
                openTasks.Pop();
                PatternAction_MultipleBodies.releaseTask(taskFor_mb);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Rek_NodeNums.@m5] = candidate_MultipleBodies_node_m5;
                        match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Rek_NodeNums.@c1] = candidate_MultipleBodies_node_c1;
                        match.EmbeddedGraphs[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Rek_SubNums.@b] = currentFoundPartialMatch.Pop();
                        match.EmbeddedGraphs[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Rek_SubNums.@mb] = currentFoundPartialMatch.Pop();
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                    matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                } else {
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case MultipleBodies_alt_0_Empty 
            do {
                patternGraph = patternGraphs[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_CaseNums.@Empty];
                // SubPreset MultipleBodies_node_m5 
                GRGEN_LGSP.LGSPNode candidate_MultipleBodies_node_m5 = MultipleBodies_node_m5;
                // SubPreset MultipleBodies_node_c1 
                GRGEN_LGSP.LGSPNode candidate_MultipleBodies_node_c1 = MultipleBodies_node_c1;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                    }
                    Stack<GRGEN_LGSP.LGSPSubpatternAction> neg_0_openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
                    List<Stack<GRGEN_LGSP.LGSPMatch>> neg_0_foundPartialMatches = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    List<Stack<GRGEN_LGSP.LGSPMatch>> neg_0_matchesList = neg_0_foundPartialMatches;
                    // Push subpattern matching task for _subpattern0
                    PatternAction_Body taskFor__subpattern0 = PatternAction_Body.getNewTask(graph, neg_0_openTasks);
                    taskFor__subpattern0.Body_node_m5 = candidate_MultipleBodies_node_m5;
                    taskFor__subpattern0.Body_node_c1 = candidate_MultipleBodies_node_c1;
                    neg_0_openTasks.Push(taskFor__subpattern0);
                    // Match subpatterns of neg_0_
                    neg_0_openTasks.Peek().myMatch(neg_0_matchesList, 1, negLevel);
                    // Pop subpattern matching task for _subpattern0
                    neg_0_openTasks.Pop();
                    PatternAction_Body.releaseTask(taskFor__subpattern0);
                    // Check whether subpatterns were found 
                    if(neg_0_matchesList.Count>0) {
                        // negative pattern with contained subpatterns found
                        neg_0_matchesList.Clear();
                        if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                        }
                        --negLevel;
                        goto label15;
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
                    Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch = new Stack<GRGEN_LGSP.LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Empty_NodeNums.@m5] = candidate_MultipleBodies_node_m5;
                    match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Empty_NodeNums.@c1] = candidate_MultipleBodies_node_c1;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label16;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[2], new GRGEN_LGSP.LGSPEdge[0], new object[0], new GRGEN_LGSP.LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Empty_NodeNums.@m5] = candidate_MultipleBodies_node_m5;
                        match.Nodes[(int)Pattern_MultipleBodies.MultipleBodies_alt_0_Empty_NodeNums.@c1] = candidate_MultipleBodies_node_c1;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                    } else {
                        foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
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
                    goto label17;
                }
label15: ;
label16: ;
label17: ;
            } while(false);
            openTasks.Push(this);
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
        
        public override void myMatch(List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            openTasks.Pop();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Body_node_c1 
            GRGEN_LGSP.LGSPNode candidate_Body_node_c1 = Body_node_c1;
            // SubPreset Body_node_m5 
            GRGEN_LGSP.LGSPNode candidate_Body_node_m5 = Body_node_m5;
            // Extend Outgoing Body_edge__edge0 from Body_node_c1 
            GRGEN_LGSP.LGSPEdge head_candidate_Body_edge__edge0 = candidate_Body_node_c1.outhead;
            if(head_candidate_Body_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_Body_edge__edge0 = head_candidate_Body_edge__edge0;
                do
                {
                    if(candidate_Body_edge__edge0.type.TypeID!=3) {
                        continue;
                    }
                    if((candidate_Body_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    uint prev__candidate_Body_edge__edge0;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev__candidate_Body_edge__edge0 = candidate_Body_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_Body_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev__candidate_Body_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_Body_edge__edge0) ? 1U : 0U;
                        if(prev__candidate_Body_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_Body_edge__edge0,candidate_Body_edge__edge0);
                    }
                    // Implicit Target Body_node_c2 from Body_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_Body_node_c2 = candidate_Body_edge__edge0.target;
                    if(candidate_Body_node_c2.type.TypeID!=5) {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                        } else { 
                            if(prev__candidate_Body_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                            }
                        }
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_node_c2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Body_node_c2)))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                        } else { 
                            if(prev__candidate_Body_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                            }
                        }
                        continue;
                    }
                    if((candidate_Body_node_c2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                        } else { 
                            if(prev__candidate_Body_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                            }
                        }
                        continue;
                    }
                    // Extend Outgoing Body_edge__edge1 from Body_node_c2 
                    GRGEN_LGSP.LGSPEdge head_candidate_Body_edge__edge1 = candidate_Body_node_c2.outhead;
                    if(head_candidate_Body_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_Body_edge__edge1 = head_candidate_Body_edge__edge1;
                        do
                        {
                            if(candidate_Body_edge__edge1.type.TypeID!=3) {
                                continue;
                            }
                            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_Body_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Body_edge__edge1)))
                            {
                                continue;
                            }
                            if((candidate_Body_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target Body_node_b from Body_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_Body_node_b = candidate_Body_edge__edge1.target;
                            if(candidate_Body_node_b.type.TypeID!=2) {
                                continue;
                            }
                            if((candidate_Body_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Extend Outgoing Body_edge__edge2 from Body_node_b 
                            GRGEN_LGSP.LGSPEdge head_candidate_Body_edge__edge2 = candidate_Body_node_b.outhead;
                            if(head_candidate_Body_edge__edge2 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_Body_edge__edge2 = head_candidate_Body_edge__edge2;
                                do
                                {
                                    if(candidate_Body_edge__edge2.type.TypeID!=6) {
                                        continue;
                                    }
                                    if(candidate_Body_edge__edge2.target != candidate_Body_node_m5) {
                                        continue;
                                    }
                                    if((candidate_Body_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                    {
                                        continue;
                                    }
                                    // Push subpattern matching task for ms
                                    PatternAction_MultipleStatements taskFor_ms = PatternAction_MultipleStatements.getNewTask(graph, openTasks);
                                    taskFor_ms.MultipleStatements_node_b = candidate_Body_node_b;
                                    openTasks.Push(taskFor_ms);
                                    // Push subpattern matching task for mp
                                    PatternAction_MultipleParameters taskFor_mp = PatternAction_MultipleParameters.getNewTask(graph, openTasks);
                                    taskFor_mp.MultipleParameters_node_b = candidate_Body_node_b;
                                    openTasks.Push(taskFor_mp);
                                    uint prevGlobal__candidate_Body_node_c2;
                                    prevGlobal__candidate_Body_node_c2 = candidate_Body_node_c2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_Body_node_c2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_Body_node_b;
                                    prevGlobal__candidate_Body_node_b = candidate_Body_node_b.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_Body_node_b.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_Body_edge__edge0;
                                    prevGlobal__candidate_Body_edge__edge0 = candidate_Body_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_Body_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_Body_edge__edge1;
                                    prevGlobal__candidate_Body_edge__edge1 = candidate_Body_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_Body_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_Body_edge__edge2;
                                    prevGlobal__candidate_Body_edge__edge2 = candidate_Body_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_Body_edge__edge2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    // Match subpatterns 
                                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                    // Pop subpattern matching task for mp
                                    openTasks.Pop();
                                    PatternAction_MultipleParameters.releaseTask(taskFor_mp);
                                    // Pop subpattern matching task for ms
                                    openTasks.Pop();
                                    PatternAction_MultipleStatements.releaseTask(taskFor_ms);
                                    // Check whether subpatterns were found 
                                    if(matchesList.Count>0) {
                                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                                        foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                                        {
                                            GRGEN_LGSP.LGSPMatch match = new GRGEN_LGSP.LGSPMatch(new GRGEN_LGSP.LGSPNode[4], new GRGEN_LGSP.LGSPEdge[3], new object[0], new GRGEN_LGSP.LGSPMatch[2+0]);
                                            match.patternGraph = patternGraph;
                                            match.Nodes[(int)Pattern_Body.Body_NodeNums.@c1] = candidate_Body_node_c1;
                                            match.Nodes[(int)Pattern_Body.Body_NodeNums.@c2] = candidate_Body_node_c2;
                                            match.Nodes[(int)Pattern_Body.Body_NodeNums.@b] = candidate_Body_node_b;
                                            match.Nodes[(int)Pattern_Body.Body_NodeNums.@m5] = candidate_Body_node_m5;
                                            match.Edges[(int)Pattern_Body.Body_EdgeNums.@_edge0] = candidate_Body_edge__edge0;
                                            match.Edges[(int)Pattern_Body.Body_EdgeNums.@_edge1] = candidate_Body_edge__edge1;
                                            match.Edges[(int)Pattern_Body.Body_EdgeNums.@_edge2] = candidate_Body_edge__edge2;
                                            match.EmbeddedGraphs[(int)Pattern_Body.Body_SubNums.@mp] = currentFoundPartialMatch.Pop();
                                            match.EmbeddedGraphs[(int)Pattern_Body.Body_SubNums.@ms] = currentFoundPartialMatch.Pop();
                                            currentFoundPartialMatch.Push(match);
                                        }
                                        if(matchesList==foundPartialMatches) {
                                            matchesList = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
                                        } else {
                                            foreach(Stack<GRGEN_LGSP.LGSPMatch> match in matchesList) {
                                                foundPartialMatches.Add(match);
                                            }
                                            matchesList.Clear();
                                        }
                                        // if enough matches were found, we leave
                                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                        {
                                            candidate_Body_edge__edge2.flags = candidate_Body_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge2;
                                            candidate_Body_edge__edge1.flags = candidate_Body_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge1;
                                            candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge0;
                                            candidate_Body_node_b.flags = candidate_Body_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_node_b;
                                            candidate_Body_node_c2.flags = candidate_Body_node_c2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_node_c2;
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                                            } else { 
                                                if(prev__candidate_Body_edge__edge0 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                                                }
                                            }
                                            openTasks.Push(this);
                                            return;
                                        }
                                        candidate_Body_edge__edge2.flags = candidate_Body_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge2;
                                        candidate_Body_edge__edge1.flags = candidate_Body_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge1;
                                        candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge0;
                                        candidate_Body_node_b.flags = candidate_Body_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_node_b;
                                        candidate_Body_node_c2.flags = candidate_Body_node_c2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_node_c2;
                                        continue;
                                    }
                                    candidate_Body_node_c2.flags = candidate_Body_node_c2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_node_c2;
                                    candidate_Body_node_b.flags = candidate_Body_node_b.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_node_b;
                                    candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge0;
                                    candidate_Body_edge__edge1.flags = candidate_Body_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge1;
                                    candidate_Body_edge__edge2.flags = candidate_Body_edge__edge2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Body_edge__edge2;
                                }
                                while( (candidate_Body_edge__edge2 = candidate_Body_edge__edge2.outNext) != head_candidate_Body_edge__edge2 );
                            }
                        }
                        while( (candidate_Body_edge__edge1 = candidate_Body_edge__edge1.outNext) != head_candidate_Body_edge__edge1 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_Body_edge__edge0.flags = candidate_Body_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_Body_edge__edge0;
                    } else { 
                        if(prev__candidate_Body_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_Body_edge__edge0);
                        }
                    }
                }
                while( (candidate_Body_edge__edge0 = candidate_Body_edge__edge0.outNext) != head_candidate_Body_edge__edge0 );
            }
            openTasks.Push(this);
            return;
        }
    }

    public class Action_createProgramGraphExample : GRGEN_LGSP.LGSPAction
    {
        public Action_createProgramGraphExample() {
            rulePattern = Rule_createProgramGraphExample.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 0, 0, 0, 0 + 0);
        }

        public override string Name { get { return "createProgramGraphExample"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createProgramGraphExample instance = new Action_createProgramGraphExample();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_createProgramGraphPullUp : GRGEN_LGSP.LGSPAction
    {
        public Action_createProgramGraphPullUp() {
            rulePattern = Rule_createProgramGraphPullUp.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 0, 0, 0, 0 + 0);
        }

        public override string Name { get { return "createProgramGraphPullUp"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createProgramGraphPullUp instance = new Action_createProgramGraphPullUp();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_pullUpMethod : GRGEN_LGSP.LGSPAction
    {
        public Action_pullUpMethod() {
            rulePattern = Rule_pullUpMethod.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 4, 3, 0, 1 + 0);
        }

        public override string Name { get { return "pullUpMethod"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_pullUpMethod instance = new Action_pullUpMethod();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            // Preset pullUpMethod_node_c1 
            GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_c1 = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_pullUpMethod_node_c1 == null) {
                MissingPreset_pullUpMethod_node_c1(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_pullUpMethod_node_c1.type.TypeID!=5) {
                return matches;
            }
            uint prev__candidate_pullUpMethod_node_c1;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_pullUpMethod_node_c1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_pullUpMethod_node_c1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_pullUpMethod_node_c1) ? 1U : 0U;
                if(prev__candidate_pullUpMethod_node_c1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_pullUpMethod_node_c1,candidate_pullUpMethod_node_c1);
            }
            // Preset pullUpMethod_node_b4 
            GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_b4 = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_pullUpMethod_node_b4 == null) {
                MissingPreset_pullUpMethod_node_b4(graph, maxMatches, parameters, null, null, null, candidate_pullUpMethod_node_c1);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                    } else { 
                        if(prev__candidate_pullUpMethod_node_c1 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                } else { 
                    if(prev__candidate_pullUpMethod_node_c1 == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                    }
                }
                return matches;
            }
            if(candidate_pullUpMethod_node_b4.type.TypeID!=2) {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                } else { 
                    if(prev__candidate_pullUpMethod_node_c1 == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                    }
                }
                return matches;
            }
            // Extend Outgoing pullUpMethod_edge__edge0 from pullUpMethod_node_c1 
            GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_node_c1.outhead;
            if(head_candidate_pullUpMethod_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge0 = head_candidate_pullUpMethod_edge__edge0;
                do
                {
                    if(candidate_pullUpMethod_edge__edge0.type.TypeID!=3) {
                        continue;
                    }
                    uint prev__candidate_pullUpMethod_edge__edge0;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_pullUpMethod_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev__candidate_pullUpMethod_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_pullUpMethod_edge__edge0) ? 1U : 0U;
                        if(prev__candidate_pullUpMethod_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_pullUpMethod_edge__edge0,candidate_pullUpMethod_edge__edge0);
                    }
                    // Implicit Target pullUpMethod_node_c3 from pullUpMethod_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_edge__edge0.target;
                    if(candidate_pullUpMethod_node_c3.type.TypeID!=5) {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                        } else { 
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                            }
                        }
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_node_c3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_pullUpMethod_node_c3)))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                        } else { 
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                            }
                        }
                        continue;
                    }
                    // Extend Outgoing pullUpMethod_edge__edge1 from pullUpMethod_node_b4 
                    GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_node_b4.outhead;
                    if(head_candidate_pullUpMethod_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge1 = head_candidate_pullUpMethod_edge__edge1;
                        do
                        {
                            if(candidate_pullUpMethod_edge__edge1.type.TypeID!=6) {
                                continue;
                            }
                            // Implicit Target pullUpMethod_node_m5 from pullUpMethod_edge__edge1 
                            GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_edge__edge1.target;
                            if(candidate_pullUpMethod_node_m5.type.TypeID!=7) {
                                continue;
                            }
                            // Extend Outgoing pullUpMethod_edge_m from pullUpMethod_node_c3 
                            GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge_m = candidate_pullUpMethod_node_c3.outhead;
                            if(head_candidate_pullUpMethod_edge_m != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge_m = head_candidate_pullUpMethod_edge_m;
                                do
                                {
                                    if(candidate_pullUpMethod_edge_m.type.TypeID!=3) {
                                        continue;
                                    }
                                    if(candidate_pullUpMethod_edge_m.target != candidate_pullUpMethod_node_b4) {
                                        continue;
                                    }
                                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_edge_m.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_pullUpMethod_edge_m)))
                                    {
                                        continue;
                                    }
                                    // Push subpattern matching task for mb
                                    PatternAction_MultipleBodies taskFor_mb = PatternAction_MultipleBodies.getNewTask(graph, openTasks);
                                    taskFor_mb.MultipleBodies_node_m5 = candidate_pullUpMethod_node_m5;
                                    taskFor_mb.MultipleBodies_node_c1 = candidate_pullUpMethod_node_c1;
                                    openTasks.Push(taskFor_mb);
                                    uint prevGlobal__candidate_pullUpMethod_node_c1;
                                    prevGlobal__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_c1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_pullUpMethod_node_c3;
                                    prevGlobal__candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_node_c3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_c3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_pullUpMethod_node_b4;
                                    prevGlobal__candidate_pullUpMethod_node_b4 = candidate_pullUpMethod_node_b4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_b4.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_pullUpMethod_node_m5;
                                    prevGlobal__candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_node_m5.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_node_m5.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_pullUpMethod_edge__edge0;
                                    prevGlobal__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_pullUpMethod_edge_m;
                                    prevGlobal__candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_edge_m.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    uint prevGlobal__candidate_pullUpMethod_edge__edge1;
                                    prevGlobal__candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    candidate_pullUpMethod_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                    // Match subpatterns 
                                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                    // Pop subpattern matching task for mb
                                    openTasks.Pop();
                                    PatternAction_MultipleBodies.releaseTask(taskFor_mb);
                                    // Check whether subpatterns were found 
                                    if(matchesList.Count>0) {
                                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                                        foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                                        {
                                            GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                            match.patternGraph = rulePattern.patternGraph;
                                            match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@c1] = candidate_pullUpMethod_node_c1;
                                            match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@c3] = candidate_pullUpMethod_node_c3;
                                            match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@b4] = candidate_pullUpMethod_node_b4;
                                            match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@m5] = candidate_pullUpMethod_node_m5;
                                            match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@_edge0] = candidate_pullUpMethod_edge__edge0;
                                            match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@m] = candidate_pullUpMethod_edge_m;
                                            match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@_edge1] = candidate_pullUpMethod_edge__edge1;
                                            match.EmbeddedGraphs[(int)Rule_pullUpMethod.pullUpMethod_SubNums.@mb] = currentFoundPartialMatch.Pop();
                                            matches.matchesList.PositionWasFilledFixIt();
                                        }
                                        matchesList.Clear();
                                        // if enough matches were found, we leave
                                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                                        {
                                            candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                            candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                            candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                            candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                            candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                            candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                            candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                                            } else { 
                                                if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                                }
                                            }
                                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                                            } else { 
                                                if(prev__candidate_pullUpMethod_node_c1 == 0) {
                                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                                                }
                                            }
                                            return matches;
                                        }
                                        candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                        candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                        candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                        candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                        candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                        candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                        candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                        continue;
                                    }
                                    candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                    candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                    candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                    candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                    candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                    candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                    candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                }
                                while( (candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.outNext) != head_candidate_pullUpMethod_edge_m );
                            }
                        }
                        while( (candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.outNext) != head_candidate_pullUpMethod_edge__edge1 );
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                    } else { 
                        if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                        }
                    }
                }
                while( (candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.outNext) != head_candidate_pullUpMethod_edge__edge0 );
            }
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
            } else { 
                if(prev__candidate_pullUpMethod_node_c1 == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                }
            }
            return matches;
        }
        public void MissingPreset_pullUpMethod_node_c1(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup pullUpMethod_node_c1 
            int type_id_candidate_pullUpMethod_node_c1 = 5;
            for(GRGEN_LGSP.LGSPNode head_candidate_pullUpMethod_node_c1 = graph.nodesByTypeHeads[type_id_candidate_pullUpMethod_node_c1], candidate_pullUpMethod_node_c1 = head_candidate_pullUpMethod_node_c1.typeNext; candidate_pullUpMethod_node_c1 != head_candidate_pullUpMethod_node_c1; candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.typeNext)
            {
                uint prev__candidate_pullUpMethod_node_c1;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prev__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_pullUpMethod_node_c1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                } else {
                    prev__candidate_pullUpMethod_node_c1 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_pullUpMethod_node_c1) ? 1U : 0U;
                    if(prev__candidate_pullUpMethod_node_c1 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_pullUpMethod_node_c1,candidate_pullUpMethod_node_c1);
                }
                // Preset pullUpMethod_node_b4 
                GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_b4 = (GRGEN_LGSP.LGSPNode) parameters[1];
                if(candidate_pullUpMethod_node_b4 == null) {
                    MissingPreset_pullUpMethod_node_b4(graph, maxMatches, parameters, null, null, null, candidate_pullUpMethod_node_c1);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                        } else { 
                            if(prev__candidate_pullUpMethod_node_c1 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                            }
                        }
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                    } else { 
                        if(prev__candidate_pullUpMethod_node_c1 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                        }
                    }
                    continue;
                }
                if(candidate_pullUpMethod_node_b4.type.TypeID!=2) {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                    } else { 
                        if(prev__candidate_pullUpMethod_node_c1 == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                        }
                    }
                    continue;
                }
                // Extend Outgoing pullUpMethod_edge__edge0 from pullUpMethod_node_c1 
                GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_node_c1.outhead;
                if(head_candidate_pullUpMethod_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge0 = head_candidate_pullUpMethod_edge__edge0;
                    do
                    {
                        if(candidate_pullUpMethod_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        uint prev__candidate_pullUpMethod_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_pullUpMethod_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_pullUpMethod_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_pullUpMethod_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_pullUpMethod_edge__edge0,candidate_pullUpMethod_edge__edge0);
                        }
                        // Implicit Target pullUpMethod_node_c3 from pullUpMethod_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_edge__edge0.target;
                        if(candidate_pullUpMethod_node_c3.type.TypeID!=5) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                            } else { 
                                if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_node_c3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_pullUpMethod_node_c3)))
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                            } else { 
                                if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing pullUpMethod_edge__edge1 from pullUpMethod_node_b4 
                        GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_node_b4.outhead;
                        if(head_candidate_pullUpMethod_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge1 = head_candidate_pullUpMethod_edge__edge1;
                            do
                            {
                                if(candidate_pullUpMethod_edge__edge1.type.TypeID!=6) {
                                    continue;
                                }
                                // Implicit Target pullUpMethod_node_m5 from pullUpMethod_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_edge__edge1.target;
                                if(candidate_pullUpMethod_node_m5.type.TypeID!=7) {
                                    continue;
                                }
                                // Extend Outgoing pullUpMethod_edge_m from pullUpMethod_node_c3 
                                GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge_m = candidate_pullUpMethod_node_c3.outhead;
                                if(head_candidate_pullUpMethod_edge_m != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge_m = head_candidate_pullUpMethod_edge_m;
                                    do
                                    {
                                        if(candidate_pullUpMethod_edge_m.type.TypeID!=3) {
                                            continue;
                                        }
                                        if(candidate_pullUpMethod_edge_m.target != candidate_pullUpMethod_node_b4) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_edge_m.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_pullUpMethod_edge_m)))
                                        {
                                            continue;
                                        }
                                        // Push subpattern matching task for mb
                                        PatternAction_MultipleBodies taskFor_mb = PatternAction_MultipleBodies.getNewTask(graph, openTasks);
                                        taskFor_mb.MultipleBodies_node_m5 = candidate_pullUpMethod_node_m5;
                                        taskFor_mb.MultipleBodies_node_c1 = candidate_pullUpMethod_node_c1;
                                        openTasks.Push(taskFor_mb);
                                        uint prevGlobal__candidate_pullUpMethod_node_c1;
                                        prevGlobal__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_c1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_node_c3;
                                        prevGlobal__candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_node_c3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_c3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_node_b4;
                                        prevGlobal__candidate_pullUpMethod_node_b4 = candidate_pullUpMethod_node_b4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_b4.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_node_m5;
                                        prevGlobal__candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_node_m5.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_m5.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_edge__edge0;
                                        prevGlobal__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_edge_m;
                                        prevGlobal__candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_edge_m.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_edge__edge1;
                                        prevGlobal__candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        // Match subpatterns 
                                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                        // Pop subpattern matching task for mb
                                        openTasks.Pop();
                                        PatternAction_MultipleBodies.releaseTask(taskFor_mb);
                                        // Check whether subpatterns were found 
                                        if(matchesList.Count>0) {
                                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                                            {
                                                GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                                match.patternGraph = rulePattern.patternGraph;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@c1] = candidate_pullUpMethod_node_c1;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@c3] = candidate_pullUpMethod_node_c3;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@b4] = candidate_pullUpMethod_node_b4;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@m5] = candidate_pullUpMethod_node_m5;
                                                match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@_edge0] = candidate_pullUpMethod_edge__edge0;
                                                match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@m] = candidate_pullUpMethod_edge_m;
                                                match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@_edge1] = candidate_pullUpMethod_edge__edge1;
                                                match.EmbeddedGraphs[(int)Rule_pullUpMethod.pullUpMethod_SubNums.@mb] = currentFoundPartialMatch.Pop();
                                                matches.matchesList.PositionWasFilledFixIt();
                                            }
                                            matchesList.Clear();
                                            // if enough matches were found, we leave
                                            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                                            {
                                                candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                                candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                                candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                                candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                                candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                                candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                                candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                                                } else { 
                                                    if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                                    }
                                                }
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                                                } else { 
                                                    if(prev__candidate_pullUpMethod_node_c1 == 0) {
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                                                    }
                                                }
                                                return;
                                            }
                                            candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                            candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                            candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                            candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                            candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                            candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                            candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                            continue;
                                        }
                                        candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                        candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                        candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                        candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                        candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                        candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                        candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                    }
                                    while( (candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.outNext) != head_candidate_pullUpMethod_edge_m );
                                }
                            }
                            while( (candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.outNext) != head_candidate_pullUpMethod_edge__edge1 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                        } else { 
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.outNext) != head_candidate_pullUpMethod_edge__edge0 );
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_node_c1;
                } else { 
                    if(prev__candidate_pullUpMethod_node_c1 == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_pullUpMethod_node_c1);
                    }
                }
            }
            return;
        }
        public void MissingPreset_pullUpMethod_node_b4(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_c1)
        {
            int negLevel = 0;
            // Lookup pullUpMethod_node_b4 
            int type_id_candidate_pullUpMethod_node_b4 = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_pullUpMethod_node_b4 = graph.nodesByTypeHeads[type_id_candidate_pullUpMethod_node_b4], candidate_pullUpMethod_node_b4 = head_candidate_pullUpMethod_node_b4.typeNext; candidate_pullUpMethod_node_b4 != head_candidate_pullUpMethod_node_b4; candidate_pullUpMethod_node_b4 = candidate_pullUpMethod_node_b4.typeNext)
            {
                // Extend Outgoing pullUpMethod_edge__edge0 from pullUpMethod_node_c1 
                GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_node_c1.outhead;
                if(head_candidate_pullUpMethod_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge0 = head_candidate_pullUpMethod_edge__edge0;
                    do
                    {
                        if(candidate_pullUpMethod_edge__edge0.type.TypeID!=3) {
                            continue;
                        }
                        uint prev__candidate_pullUpMethod_edge__edge0;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prev__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_pullUpMethod_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        } else {
                            prev__candidate_pullUpMethod_edge__edge0 = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_pullUpMethod_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_pullUpMethod_edge__edge0,candidate_pullUpMethod_edge__edge0);
                        }
                        // Implicit Target pullUpMethod_node_c3 from pullUpMethod_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_edge__edge0.target;
                        if(candidate_pullUpMethod_node_c3.type.TypeID!=5) {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                            } else { 
                                if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_node_c3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_pullUpMethod_node_c3)))
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                            } else { 
                                if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing pullUpMethod_edge__edge1 from pullUpMethod_node_b4 
                        GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_node_b4.outhead;
                        if(head_candidate_pullUpMethod_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge__edge1 = head_candidate_pullUpMethod_edge__edge1;
                            do
                            {
                                if(candidate_pullUpMethod_edge__edge1.type.TypeID!=6) {
                                    continue;
                                }
                                // Implicit Target pullUpMethod_node_m5 from pullUpMethod_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_edge__edge1.target;
                                if(candidate_pullUpMethod_node_m5.type.TypeID!=7) {
                                    continue;
                                }
                                // Extend Outgoing pullUpMethod_edge_m from pullUpMethod_node_c3 
                                GRGEN_LGSP.LGSPEdge head_candidate_pullUpMethod_edge_m = candidate_pullUpMethod_node_c3.outhead;
                                if(head_candidate_pullUpMethod_edge_m != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_pullUpMethod_edge_m = head_candidate_pullUpMethod_edge_m;
                                    do
                                    {
                                        if(candidate_pullUpMethod_edge_m.type.TypeID!=3) {
                                            continue;
                                        }
                                        if(candidate_pullUpMethod_edge_m.target != candidate_pullUpMethod_node_b4) {
                                            continue;
                                        }
                                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_pullUpMethod_edge_m.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_pullUpMethod_edge_m)))
                                        {
                                            continue;
                                        }
                                        // Push subpattern matching task for mb
                                        PatternAction_MultipleBodies taskFor_mb = PatternAction_MultipleBodies.getNewTask(graph, openTasks);
                                        taskFor_mb.MultipleBodies_node_m5 = candidate_pullUpMethod_node_m5;
                                        taskFor_mb.MultipleBodies_node_c1 = candidate_pullUpMethod_node_c1;
                                        openTasks.Push(taskFor_mb);
                                        uint prevGlobal__candidate_pullUpMethod_node_c1;
                                        prevGlobal__candidate_pullUpMethod_node_c1 = candidate_pullUpMethod_node_c1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_c1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_node_c3;
                                        prevGlobal__candidate_pullUpMethod_node_c3 = candidate_pullUpMethod_node_c3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_c3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_node_b4;
                                        prevGlobal__candidate_pullUpMethod_node_b4 = candidate_pullUpMethod_node_b4.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_b4.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_node_m5;
                                        prevGlobal__candidate_pullUpMethod_node_m5 = candidate_pullUpMethod_node_m5.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_node_m5.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_edge__edge0;
                                        prevGlobal__candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_edge_m;
                                        prevGlobal__candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_edge_m.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        uint prevGlobal__candidate_pullUpMethod_edge__edge1;
                                        prevGlobal__candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        candidate_pullUpMethod_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                        // Match subpatterns 
                                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                        // Pop subpattern matching task for mb
                                        openTasks.Pop();
                                        PatternAction_MultipleBodies.releaseTask(taskFor_mb);
                                        // Check whether subpatterns were found 
                                        if(matchesList.Count>0) {
                                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                                            foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                                            {
                                                GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                                match.patternGraph = rulePattern.patternGraph;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@c1] = candidate_pullUpMethod_node_c1;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@c3] = candidate_pullUpMethod_node_c3;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@b4] = candidate_pullUpMethod_node_b4;
                                                match.Nodes[(int)Rule_pullUpMethod.pullUpMethod_NodeNums.@m5] = candidate_pullUpMethod_node_m5;
                                                match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@_edge0] = candidate_pullUpMethod_edge__edge0;
                                                match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@m] = candidate_pullUpMethod_edge_m;
                                                match.Edges[(int)Rule_pullUpMethod.pullUpMethod_EdgeNums.@_edge1] = candidate_pullUpMethod_edge__edge1;
                                                match.EmbeddedGraphs[(int)Rule_pullUpMethod.pullUpMethod_SubNums.@mb] = currentFoundPartialMatch.Pop();
                                                matches.matchesList.PositionWasFilledFixIt();
                                            }
                                            matchesList.Clear();
                                            // if enough matches were found, we leave
                                            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                                            {
                                                candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                                candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                                candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                                candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                                candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                                candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                                candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                                    candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                                                } else { 
                                                    if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                                                    }
                                                }
                                                return;
                                            }
                                            candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                            candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                            candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                            candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                            candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                            candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                            candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                            continue;
                                        }
                                        candidate_pullUpMethod_node_c1.flags = candidate_pullUpMethod_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c1;
                                        candidate_pullUpMethod_node_c3.flags = candidate_pullUpMethod_node_c3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_c3;
                                        candidate_pullUpMethod_node_b4.flags = candidate_pullUpMethod_node_b4.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_b4;
                                        candidate_pullUpMethod_node_m5.flags = candidate_pullUpMethod_node_m5.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_node_m5;
                                        candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge0;
                                        candidate_pullUpMethod_edge_m.flags = candidate_pullUpMethod_edge_m.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge_m;
                                        candidate_pullUpMethod_edge__edge1.flags = candidate_pullUpMethod_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_pullUpMethod_edge__edge1;
                                    }
                                    while( (candidate_pullUpMethod_edge_m = candidate_pullUpMethod_edge_m.outNext) != head_candidate_pullUpMethod_edge_m );
                                }
                            }
                            while( (candidate_pullUpMethod_edge__edge1 = candidate_pullUpMethod_edge__edge1.outNext) != head_candidate_pullUpMethod_edge__edge1 );
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_pullUpMethod_edge__edge0.flags = candidate_pullUpMethod_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_pullUpMethod_edge__edge0;
                        } else { 
                            if(prev__candidate_pullUpMethod_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_pullUpMethod_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_pullUpMethod_edge__edge0 = candidate_pullUpMethod_edge__edge0.outNext) != head_candidate_pullUpMethod_edge__edge0 );
                }
            }
            return;
        }
    }

    public class Action_matchAll : GRGEN_LGSP.LGSPAction
    {
        public Action_matchAll() {
            rulePattern = Rule_matchAll.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 1, 0, 0, 1 + 0);
        }

        public override string Name { get { return "matchAll"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_matchAll instance = new Action_matchAll();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches = new List<Stack<GRGEN_LGSP.LGSPMatch>>();
            List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList = foundPartialMatches;
            // Preset matchAll_node_c1 
            GRGEN_LGSP.LGSPNode candidate_matchAll_node_c1 = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_matchAll_node_c1 == null) {
                MissingPreset_matchAll_node_c1(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(candidate_matchAll_node_c1.type.TypeID!=5) {
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_Subclass taskFor__subpattern0 = PatternAction_Subclass.getNewTask(graph, openTasks);
            taskFor__subpattern0.Subclass_node_sub = candidate_matchAll_node_c1;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_matchAll_node_c1;
            prevGlobal__candidate_matchAll_node_c1 = candidate_matchAll_node_c1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_matchAll_node_c1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_Subclass.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_matchAll.matchAll_NodeNums.@c1] = candidate_matchAll_node_c1;
                    match.EmbeddedGraphs[(int)Rule_matchAll.matchAll_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_matchAll_node_c1.flags = candidate_matchAll_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_matchAll_node_c1;
                    return matches;
                }
                candidate_matchAll_node_c1.flags = candidate_matchAll_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_matchAll_node_c1;
                return matches;
            }
            candidate_matchAll_node_c1.flags = candidate_matchAll_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_matchAll_node_c1;
            return matches;
        }
        public void MissingPreset_matchAll_node_c1(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LGSP.LGSPMatch>> foundPartialMatches, List<Stack<GRGEN_LGSP.LGSPMatch>> matchesList)
        {
            int negLevel = 0;
            // Lookup matchAll_node_c1 
            int type_id_candidate_matchAll_node_c1 = 5;
            for(GRGEN_LGSP.LGSPNode head_candidate_matchAll_node_c1 = graph.nodesByTypeHeads[type_id_candidate_matchAll_node_c1], candidate_matchAll_node_c1 = head_candidate_matchAll_node_c1.typeNext; candidate_matchAll_node_c1 != head_candidate_matchAll_node_c1; candidate_matchAll_node_c1 = candidate_matchAll_node_c1.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_Subclass taskFor__subpattern0 = PatternAction_Subclass.getNewTask(graph, openTasks);
                taskFor__subpattern0.Subclass_node_sub = candidate_matchAll_node_c1;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_matchAll_node_c1;
                prevGlobal__candidate_matchAll_node_c1 = candidate_matchAll_node_c1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_matchAll_node_c1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_Subclass.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LGSP.LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_matchAll.matchAll_NodeNums.@c1] = candidate_matchAll_node_c1;
                        match.EmbeddedGraphs[(int)Rule_matchAll.matchAll_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_matchAll_node_c1.flags = candidate_matchAll_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_matchAll_node_c1;
                        return;
                    }
                    candidate_matchAll_node_c1.flags = candidate_matchAll_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_matchAll_node_c1;
                    continue;
                }
                candidate_matchAll_node_c1.flags = candidate_matchAll_node_c1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_matchAll_node_c1;
            }
            return;
        }
    }

    public class Action_InsertHelperEdgesForNestedLayout : GRGEN_LGSP.LGSPAction
    {
        public Action_InsertHelperEdgesForNestedLayout() {
            rulePattern = Rule_InsertHelperEdgesForNestedLayout.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 0, 0, 0, 0 + 0);
        }

        public override string Name { get { return "InsertHelperEdgesForNestedLayout"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_InsertHelperEdgesForNestedLayout instance = new Action_InsertHelperEdgesForNestedLayout();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_LinkMethodBodyToContainedEntity : GRGEN_LGSP.LGSPAction
    {
        public Action_LinkMethodBodyToContainedEntity() {
            rulePattern = Rule_LinkMethodBodyToContainedEntity.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 2, 1, 0, 0 + 0);
        }

        public override string Name { get { return "LinkMethodBodyToContainedEntity"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_LinkMethodBodyToContainedEntity instance = new Action_LinkMethodBodyToContainedEntity();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            // Lookup LinkMethodBodyToContainedEntity_edge__edge0 
            int type_id_candidate_LinkMethodBodyToContainedEntity_edge__edge0 = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedEntity_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_LinkMethodBodyToContainedEntity_edge__edge0], candidate_LinkMethodBodyToContainedEntity_edge__edge0 = head_candidate_LinkMethodBodyToContainedEntity_edge__edge0.typeNext; candidate_LinkMethodBodyToContainedEntity_edge__edge0 != head_candidate_LinkMethodBodyToContainedEntity_edge__edge0; candidate_LinkMethodBodyToContainedEntity_edge__edge0 = candidate_LinkMethodBodyToContainedEntity_edge__edge0.typeNext)
            {
                // Implicit Source LinkMethodBodyToContainedEntity_node_mb from LinkMethodBodyToContainedEntity_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedEntity_node_mb = candidate_LinkMethodBodyToContainedEntity_edge__edge0.source;
                if(candidate_LinkMethodBodyToContainedEntity_node_mb.type.TypeID!=2) {
                    continue;
                }
                uint prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                prev__candidate_LinkMethodBodyToContainedEntity_node_mb = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_LinkMethodBodyToContainedEntity_node_mb.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target LinkMethodBodyToContainedEntity_node_e from LinkMethodBodyToContainedEntity_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedEntity_node_e = candidate_LinkMethodBodyToContainedEntity_edge__edge0.target;
                if(!NodeType_Entity.isMyType[candidate_LinkMethodBodyToContainedEntity_node_e.type.TypeID]) {
                    candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                    continue;
                }
                if((candidate_LinkMethodBodyToContainedEntity_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                    prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_LinkMethodBodyToContainedEntity_node_mb.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    if((candidate_LinkMethodBodyToContainedEntity_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                        --negLevel;
                        goto label18;
                    }
                    // Extend Outgoing LinkMethodBodyToContainedEntity_neg_0_edge__edge0 from LinkMethodBodyToContainedEntity_node_mb 
                    GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedEntity_node_mb.outhead;
                    if(head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0.type.TypeID!=11) {
                                continue;
                            }
                            if(candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0.target != candidate_LinkMethodBodyToContainedEntity_node_e) {
                                continue;
                            }
                            // negative pattern found
                            candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                            --negLevel;
                            candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                            goto label19;
                        }
                        while( (candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0.outNext) != head_candidate_LinkMethodBodyToContainedEntity_neg_0_edge__edge0 );
                    }
                    candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedEntity_node_mb;
                    --negLevel;
                }
label18: ;
                GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_LinkMethodBodyToContainedEntity.LinkMethodBodyToContainedEntity_NodeNums.@mb] = candidate_LinkMethodBodyToContainedEntity_node_mb;
                match.Nodes[(int)Rule_LinkMethodBodyToContainedEntity.LinkMethodBodyToContainedEntity_NodeNums.@e] = candidate_LinkMethodBodyToContainedEntity_node_e;
                match.Edges[(int)Rule_LinkMethodBodyToContainedEntity.LinkMethodBodyToContainedEntity_EdgeNums.@_edge0] = candidate_LinkMethodBodyToContainedEntity_edge__edge0;
                matches.matchesList.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_LinkMethodBodyToContainedEntity_edge__edge0);
                    candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
                    return matches;
                }
                candidate_LinkMethodBodyToContainedEntity_node_mb.flags = candidate_LinkMethodBodyToContainedEntity_node_mb.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedEntity_node_mb;
label19: ;
            }
            return matches;
        }
    }

    public class Action_LinkMethodBodyToContainedExpressionTransitive : GRGEN_LGSP.LGSPAction
    {
        public Action_LinkMethodBodyToContainedExpressionTransitive() {
            rulePattern = Rule_LinkMethodBodyToContainedExpressionTransitive.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 3, 2, 0, 0 + 0);
        }

        public override string Name { get { return "LinkMethodBodyToContainedExpressionTransitive"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_LinkMethodBodyToContainedExpressionTransitive instance = new Action_LinkMethodBodyToContainedExpressionTransitive();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            // Lookup LinkMethodBodyToContainedExpressionTransitive_edge__edge1 
            int type_id_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1], candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.typeNext; candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 != head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1; candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.typeNext)
            {
                // Implicit Source LinkMethodBodyToContainedExpressionTransitive_node_e1 from LinkMethodBodyToContainedExpressionTransitive_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.source;
                if(candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.type.TypeID!=3) {
                    continue;
                }
                uint prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target LinkMethodBodyToContainedExpressionTransitive_node_e2 from LinkMethodBodyToContainedExpressionTransitive_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1.target;
                if(candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2.type.TypeID!=3) {
                    candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                    continue;
                }
                if((candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                    prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    if((candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                        --negLevel;
                        goto label20;
                    }
                    // Extend Outgoing LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 from LinkMethodBodyToContainedExpressionTransitive_node_e1 
                    GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.outhead;
                    if(head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.type.TypeID!=11) {
                                continue;
                            }
                            if(candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.target != candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2) {
                                continue;
                            }
                            // negative pattern found
                            candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                            --negLevel;
                            candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                            goto label21;
                        }
                        while( (candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0.outNext) != head_candidate_LinkMethodBodyToContainedExpressionTransitive_neg_0_edge__edge0 );
                    }
                    candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                    --negLevel;
                }
label20: ;
                // Extend Incoming LinkMethodBodyToContainedExpressionTransitive_edge__edge0 from LinkMethodBodyToContainedExpressionTransitive_node_e1 
                GRGEN_LGSP.LGSPEdge head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.inhead;
                if(head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0;
                    do
                    {
                        if(candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0.type.TypeID!=11) {
                            continue;
                        }
                        // Implicit Source LinkMethodBodyToContainedExpressionTransitive_node_mb from LinkMethodBodyToContainedExpressionTransitive_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_LinkMethodBodyToContainedExpressionTransitive_node_mb = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0.source;
                        if(candidate_LinkMethodBodyToContainedExpressionTransitive_node_mb.type.TypeID!=2) {
                            continue;
                        }
                        GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_LinkMethodBodyToContainedExpressionTransitive.LinkMethodBodyToContainedExpressionTransitive_NodeNums.@mb] = candidate_LinkMethodBodyToContainedExpressionTransitive_node_mb;
                        match.Nodes[(int)Rule_LinkMethodBodyToContainedExpressionTransitive.LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e1] = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                        match.Nodes[(int)Rule_LinkMethodBodyToContainedExpressionTransitive.LinkMethodBodyToContainedExpressionTransitive_NodeNums.@e2] = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e2;
                        match.Edges[(int)Rule_LinkMethodBodyToContainedExpressionTransitive.LinkMethodBodyToContainedExpressionTransitive_EdgeNums.@_edge0] = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0;
                        match.Edges[(int)Rule_LinkMethodBodyToContainedExpressionTransitive.LinkMethodBodyToContainedExpressionTransitive_EdgeNums.@_edge1] = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1;
                        matches.matchesList.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.MoveInHeadAfter(candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0);
                            graph.MoveHeadAfter(candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge1);
                            candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
                            return matches;
                        }
                    }
                    while( (candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 = candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0.inNext) != head_candidate_LinkMethodBodyToContainedExpressionTransitive_edge__edge0 );
                }
                candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags = candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkMethodBodyToContainedExpressionTransitive_node_e1;
label21: ;
            }
            return matches;
        }
    }

    public class Action_LinkClassToFeature : GRGEN_LGSP.LGSPAction
    {
        public Action_LinkClassToFeature() {
            rulePattern = Rule_LinkClassToFeature.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatches(this, 2, 1, 0, 0 + 0);
        }

        public override string Name { get { return "LinkClassToFeature"; } }
        private GRGEN_LGSP.LGSPMatches matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_LinkClassToFeature instance = new Action_LinkClassToFeature();

        public GRGEN_LGSP.LGSPMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            // Lookup LinkClassToFeature_edge__edge0 
            int type_id_candidate_LinkClassToFeature_edge__edge0 = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_LinkClassToFeature_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_LinkClassToFeature_edge__edge0], candidate_LinkClassToFeature_edge__edge0 = head_candidate_LinkClassToFeature_edge__edge0.typeNext; candidate_LinkClassToFeature_edge__edge0 != head_candidate_LinkClassToFeature_edge__edge0; candidate_LinkClassToFeature_edge__edge0 = candidate_LinkClassToFeature_edge__edge0.typeNext)
            {
                // Implicit Source LinkClassToFeature_node_c from LinkClassToFeature_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_LinkClassToFeature_node_c = candidate_LinkClassToFeature_edge__edge0.source;
                if(candidate_LinkClassToFeature_node_c.type.TypeID!=5) {
                    continue;
                }
                uint prev__candidate_LinkClassToFeature_node_c;
                prev__candidate_LinkClassToFeature_node_c = candidate_LinkClassToFeature_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_LinkClassToFeature_node_c.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target LinkClassToFeature_node_e from LinkClassToFeature_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_LinkClassToFeature_node_e = candidate_LinkClassToFeature_edge__edge0.target;
                if(!Rule_LinkClassToFeature.LinkClassToFeature_node_e_IsAllowedType[candidate_LinkClassToFeature_node_e.type.TypeID]) {
                    candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkClassToFeature_node_c;
                    continue;
                }
                if((candidate_LinkClassToFeature_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkClassToFeature_node_c;
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    uint prev_neg_0__candidate_LinkClassToFeature_node_c;
                    prev_neg_0__candidate_LinkClassToFeature_node_c = candidate_LinkClassToFeature_node_c.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_LinkClassToFeature_node_c.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    if((candidate_LinkClassToFeature_node_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkClassToFeature_node_c;
                        --negLevel;
                        goto label22;
                    }
                    // Extend Outgoing LinkClassToFeature_neg_0_edge__edge0 from LinkClassToFeature_node_c 
                    GRGEN_LGSP.LGSPEdge head_candidate_LinkClassToFeature_neg_0_edge__edge0 = candidate_LinkClassToFeature_node_c.outhead;
                    if(head_candidate_LinkClassToFeature_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_LinkClassToFeature_neg_0_edge__edge0 = head_candidate_LinkClassToFeature_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_LinkClassToFeature_neg_0_edge__edge0.type.TypeID!=10) {
                                continue;
                            }
                            if(candidate_LinkClassToFeature_neg_0_edge__edge0.target != candidate_LinkClassToFeature_node_e) {
                                continue;
                            }
                            // negative pattern found
                            candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkClassToFeature_node_c;
                            --negLevel;
                            candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkClassToFeature_node_c;
                            goto label23;
                        }
                        while( (candidate_LinkClassToFeature_neg_0_edge__edge0 = candidate_LinkClassToFeature_neg_0_edge__edge0.outNext) != head_candidate_LinkClassToFeature_neg_0_edge__edge0 );
                    }
                    candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_neg_0__candidate_LinkClassToFeature_node_c;
                    --negLevel;
                }
label22: ;
                GRGEN_LGSP.LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_LinkClassToFeature.LinkClassToFeature_NodeNums.@c] = candidate_LinkClassToFeature_node_c;
                match.Nodes[(int)Rule_LinkClassToFeature.LinkClassToFeature_NodeNums.@e] = candidate_LinkClassToFeature_node_e;
                match.Edges[(int)Rule_LinkClassToFeature.LinkClassToFeature_EdgeNums.@_edge0] = candidate_LinkClassToFeature_edge__edge0;
                matches.matchesList.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_LinkClassToFeature_edge__edge0);
                    candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkClassToFeature_node_c;
                    return matches;
                }
                candidate_LinkClassToFeature_node_c.flags = candidate_LinkClassToFeature_node_c.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_LinkClassToFeature_node_c;
label23: ;
            }
            return matches;
        }
    }


    public class ProgramGraphsActions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public ProgramGraphsActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public ProgramGraphsActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("createProgramGraphExample", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createProgramGraphExample.Instance);
            actions.Add("createProgramGraphPullUp", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createProgramGraphPullUp.Instance);
            actions.Add("pullUpMethod", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_pullUpMethod.Instance);
            actions.Add("matchAll", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_matchAll.Instance);
            actions.Add("InsertHelperEdgesForNestedLayout", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_InsertHelperEdgesForNestedLayout.Instance);
            actions.Add("LinkMethodBodyToContainedEntity", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_LinkMethodBodyToContainedEntity.Instance);
            actions.Add("LinkMethodBodyToContainedExpressionTransitive", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_LinkMethodBodyToContainedExpressionTransitive.Instance);
            actions.Add("LinkClassToFeature", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_LinkClassToFeature.Instance);
        }

        public override String Name { get { return "ProgramGraphsActions"; } }
        public override String ModelMD5Hash { get { return "e6271fc2f2794368b53b1fb118947e8d"; } }
    }
}