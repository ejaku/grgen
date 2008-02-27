using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_Alternatives;

namespace de.unika.ipd.grGen.Action_Alternatives
{
	public class Pattern_toAorB : LGSPRulePattern
	{
		private static Pattern_toAorB instance = null;
		public static Pattern_toAorB Instance { get { if (instance==null) instance = new Pattern_toAorB(); return instance; } }

		public static NodeType[] toAorB_node_x_AllowedTypes = null;
		public static bool[] toAorB_node_x_IsAllowedType = null;
		public static EdgeType[] toAorB_edge_y_AllowedTypes = null;
		public static bool[] toAorB_edge_y_IsAllowedType = null;
		public enum toAorB_NodeNums { @x, };
		public enum toAorB_EdgeNums { @y, };
		public enum toAorB_SubNums { };
		public enum toAorB_AltNums { @alt_0, };
		public enum toAorB_alt_0_CaseNums { @toA, @toB, };
		public static NodeType[] toAorB_alt_0_toA_node_a_AllowedTypes = null;
		public static bool[] toAorB_alt_0_toA_node_a_IsAllowedType = null;
		public enum toAorB_alt_0_toA_NodeNums { @a, };
		public enum toAorB_alt_0_toA_EdgeNums { @y, };
		public enum toAorB_alt_0_toA_SubNums { };
		public enum toAorB_alt_0_toA_AltNums { };
		public static NodeType[] toAorB_alt_0_toB_node_b_AllowedTypes = null;
		public static bool[] toAorB_alt_0_toB_node_b_IsAllowedType = null;
		public enum toAorB_alt_0_toB_NodeNums { @b, };
		public enum toAorB_alt_0_toB_EdgeNums { @y, };
		public enum toAorB_alt_0_toB_SubNums { };
		public enum toAorB_alt_0_toB_AltNums { };

#if INITIAL_WARMUP
		public Pattern_toAorB()
#else
		private Pattern_toAorB()
#endif
		{
			name = "toAorB";
			isSubpattern = true;

			PatternGraph pat_toAorB;
			PatternNode toAorB_node_x = new PatternNode((int) NodeTypes.@Node, "toAorB_node_x", "x", toAorB_node_x_AllowedTypes, toAorB_node_x_IsAllowedType, 5.5F, 0);
			PatternEdge toAorB_edge_y = new PatternEdge(toAorB_node_x, null, (int) EdgeTypes.@Edge, "toAorB_edge_y", "y", toAorB_edge_y_AllowedTypes, toAorB_edge_y_IsAllowedType, 5.5F, -1);
			PatternGraph toAorB_alt_0_toA;
			PatternNode toAorB_alt_0_toA_node_a = new PatternNode((int) NodeTypes.@A, "toAorB_alt_0_toA_node_a", "a", toAorB_alt_0_toA_node_a_AllowedTypes, toAorB_alt_0_toA_node_a_IsAllowedType, 5.5F, -1);
			toAorB_alt_0_toA = new PatternGraph(
				"toA",
				"toAorB_alt_0_",
				new PatternNode[] { toAorB_alt_0_toA_node_a }, 
				new PatternEdge[] { toAorB_edge_y }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, },
				new bool[] {
					false, },
				new bool[] {
					true, },
				new bool[] {
					true, }
			);
			PatternGraph toAorB_alt_0_toB;
			PatternNode toAorB_alt_0_toB_node_b = new PatternNode((int) NodeTypes.@B, "toAorB_alt_0_toB_node_b", "b", toAorB_alt_0_toB_node_b_AllowedTypes, toAorB_alt_0_toB_node_b_IsAllowedType, 5.5F, -1);
			toAorB_alt_0_toB = new PatternGraph(
				"toB",
				"toAorB_alt_0_",
				new PatternNode[] { toAorB_alt_0_toB_node_b }, 
				new PatternEdge[] { toAorB_edge_y }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, },
				new bool[] {
					false, },
				new bool[] {
					true, },
				new bool[] {
					true, }
			);
			Alternative toAorB_alt_0 = new Alternative( "alt_0", "toAorB_", new PatternGraph[] { toAorB_alt_0_toA, toAorB_alt_0_toB } );

			pat_toAorB = new PatternGraph(
				"toAorB",
				"",
				new PatternNode[] { toAorB_node_x }, 
				new PatternEdge[] { toAorB_edge_y }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { toAorB_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, },
				new bool[] {
					false, },
				new bool[] {
					true, },
				new bool[] {
					true, }
			);
			toAorB_node_x.PointOfDefinition = null;
			toAorB_edge_y.PointOfDefinition = pat_toAorB;
			toAorB_alt_0_toA_node_a.PointOfDefinition = toAorB_alt_0_toA;
			toAorB_alt_0_toB_node_b.PointOfDefinition = toAorB_alt_0_toB;

			patternGraph = pat_toAorB;

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "toAorB_node_x", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // currently empty
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_AandnotCorB : LGSPRulePattern
	{
		private static Rule_AandnotCorB instance = null;
		public static Rule_AandnotCorB Instance { get { if (instance==null) instance = new Rule_AandnotCorB(); return instance; } }

		public enum AandnotCorB_NodeNums { };
		public enum AandnotCorB_EdgeNums { };
		public enum AandnotCorB_SubNums { };
		public enum AandnotCorB_AltNums { @alt_0, };
		public enum AandnotCorB_alt_0_CaseNums { @A, @B, };
		public static NodeType[] AandnotCorB_alt_0_A_node__node0_AllowedTypes = null;
		public static bool[] AandnotCorB_alt_0_A_node__node0_IsAllowedType = null;
		public enum AandnotCorB_alt_0_A_NodeNums { @_node0, };
		public enum AandnotCorB_alt_0_A_EdgeNums { };
		public enum AandnotCorB_alt_0_A_SubNums { };
		public enum AandnotCorB_alt_0_A_AltNums { };
		public static NodeType[] AandnotCorB_alt_0_A_neg_0_node__node0_AllowedTypes = null;
		public static bool[] AandnotCorB_alt_0_A_neg_0_node__node0_IsAllowedType = null;
		public enum AandnotCorB_alt_0_A_neg_0_NodeNums { @_node0, };
		public enum AandnotCorB_alt_0_A_neg_0_EdgeNums { };
		public enum AandnotCorB_alt_0_A_neg_0_SubNums { };
		public enum AandnotCorB_alt_0_A_neg_0_AltNums { };
		public static NodeType[] AandnotCorB_alt_0_B_node__node0_AllowedTypes = null;
		public static bool[] AandnotCorB_alt_0_B_node__node0_IsAllowedType = null;
		public enum AandnotCorB_alt_0_B_NodeNums { @_node0, };
		public enum AandnotCorB_alt_0_B_EdgeNums { };
		public enum AandnotCorB_alt_0_B_SubNums { };
		public enum AandnotCorB_alt_0_B_AltNums { };

#if INITIAL_WARMUP
		public Rule_AandnotCorB()
#else
		private Rule_AandnotCorB()
#endif
		{
			name = "AandnotCorB";
			isSubpattern = false;

			PatternGraph pat_AandnotCorB;
			PatternGraph AandnotCorB_alt_0_A;
			PatternNode AandnotCorB_alt_0_A_node__node0 = new PatternNode((int) NodeTypes.@A, "AandnotCorB_alt_0_A_node__node0", "_node0", AandnotCorB_alt_0_A_node__node0_AllowedTypes, AandnotCorB_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			PatternGraph AandnotCorB_alt_0_A_neg_0;
			PatternNode AandnotCorB_alt_0_A_neg_0_node__node0 = new PatternNode((int) NodeTypes.@C, "AandnotCorB_alt_0_A_neg_0_node__node0", "_node0", AandnotCorB_alt_0_A_neg_0_node__node0_AllowedTypes, AandnotCorB_alt_0_A_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			AandnotCorB_alt_0_A_neg_0 = new PatternGraph(
				"neg_0",
				"AandnotCorB_alt_0_A_",
				new PatternNode[] { AandnotCorB_alt_0_A_neg_0_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			AandnotCorB_alt_0_A = new PatternGraph(
				"A",
				"AandnotCorB_alt_0_",
				new PatternNode[] { AandnotCorB_alt_0_A_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { AandnotCorB_alt_0_A_neg_0,  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			PatternGraph AandnotCorB_alt_0_B;
			PatternNode AandnotCorB_alt_0_B_node__node0 = new PatternNode((int) NodeTypes.@B, "AandnotCorB_alt_0_B_node__node0", "_node0", AandnotCorB_alt_0_B_node__node0_AllowedTypes, AandnotCorB_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AandnotCorB_alt_0_B = new PatternGraph(
				"B",
				"AandnotCorB_alt_0_",
				new PatternNode[] { AandnotCorB_alt_0_B_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			Alternative AandnotCorB_alt_0 = new Alternative( "alt_0", "AandnotCorB_", new PatternGraph[] { AandnotCorB_alt_0_A, AandnotCorB_alt_0_B } );

			pat_AandnotCorB = new PatternGraph(
				"AandnotCorB",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { AandnotCorB_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);
			AandnotCorB_alt_0_A_node__node0.PointOfDefinition = AandnotCorB_alt_0_A;
			AandnotCorB_alt_0_A_neg_0_node__node0.PointOfDefinition = AandnotCorB_alt_0_A_neg_0;
			AandnotCorB_alt_0_B_node__node0.PointOfDefinition = AandnotCorB_alt_0_B;

			patternGraph = pat_AandnotCorB;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_AorB : LGSPRulePattern
	{
		private static Rule_AorB instance = null;
		public static Rule_AorB Instance { get { if (instance==null) instance = new Rule_AorB(); return instance; } }

		public enum AorB_NodeNums { };
		public enum AorB_EdgeNums { };
		public enum AorB_SubNums { };
		public enum AorB_AltNums { @alt_0, };
		public enum AorB_alt_0_CaseNums { @A, @B, };
		public static NodeType[] AorB_alt_0_A_node__node0_AllowedTypes = null;
		public static bool[] AorB_alt_0_A_node__node0_IsAllowedType = null;
		public enum AorB_alt_0_A_NodeNums { @_node0, };
		public enum AorB_alt_0_A_EdgeNums { };
		public enum AorB_alt_0_A_SubNums { };
		public enum AorB_alt_0_A_AltNums { };
		public static NodeType[] AorB_alt_0_B_node__node0_AllowedTypes = null;
		public static bool[] AorB_alt_0_B_node__node0_IsAllowedType = null;
		public enum AorB_alt_0_B_NodeNums { @_node0, };
		public enum AorB_alt_0_B_EdgeNums { };
		public enum AorB_alt_0_B_SubNums { };
		public enum AorB_alt_0_B_AltNums { };

#if INITIAL_WARMUP
		public Rule_AorB()
#else
		private Rule_AorB()
#endif
		{
			name = "AorB";
			isSubpattern = false;

			PatternGraph pat_AorB;
			PatternGraph AorB_alt_0_A;
			PatternNode AorB_alt_0_A_node__node0 = new PatternNode((int) NodeTypes.@A, "AorB_alt_0_A_node__node0", "_node0", AorB_alt_0_A_node__node0_AllowedTypes, AorB_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			AorB_alt_0_A = new PatternGraph(
				"A",
				"AorB_alt_0_",
				new PatternNode[] { AorB_alt_0_A_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			PatternGraph AorB_alt_0_B;
			PatternNode AorB_alt_0_B_node__node0 = new PatternNode((int) NodeTypes.@B, "AorB_alt_0_B_node__node0", "_node0", AorB_alt_0_B_node__node0_AllowedTypes, AorB_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AorB_alt_0_B = new PatternGraph(
				"B",
				"AorB_alt_0_",
				new PatternNode[] { AorB_alt_0_B_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			Alternative AorB_alt_0 = new Alternative( "alt_0", "AorB_", new PatternGraph[] { AorB_alt_0_A, AorB_alt_0_B } );

			pat_AorB = new PatternGraph(
				"AorB",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { AorB_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);
			AorB_alt_0_A_node__node0.PointOfDefinition = AorB_alt_0_A;
			AorB_alt_0_B_node__node0.PointOfDefinition = AorB_alt_0_B;

			patternGraph = pat_AorB;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_AorBorC : LGSPRulePattern
	{
		private static Rule_AorBorC instance = null;
		public static Rule_AorBorC Instance { get { if (instance==null) instance = new Rule_AorBorC(); return instance; } }

		public enum AorBorC_NodeNums { };
		public enum AorBorC_EdgeNums { };
		public enum AorBorC_SubNums { };
		public enum AorBorC_AltNums { @alt_0, };
		public enum AorBorC_alt_0_CaseNums { @A, @B, @C, };
		public static NodeType[] AorBorC_alt_0_A_node__node0_AllowedTypes = null;
		public static bool[] AorBorC_alt_0_A_node__node0_IsAllowedType = null;
		public enum AorBorC_alt_0_A_NodeNums { @_node0, };
		public enum AorBorC_alt_0_A_EdgeNums { };
		public enum AorBorC_alt_0_A_SubNums { };
		public enum AorBorC_alt_0_A_AltNums { };
		public static NodeType[] AorBorC_alt_0_B_node__node0_AllowedTypes = null;
		public static bool[] AorBorC_alt_0_B_node__node0_IsAllowedType = null;
		public enum AorBorC_alt_0_B_NodeNums { @_node0, };
		public enum AorBorC_alt_0_B_EdgeNums { };
		public enum AorBorC_alt_0_B_SubNums { };
		public enum AorBorC_alt_0_B_AltNums { };
		public static NodeType[] AorBorC_alt_0_C_node__node0_AllowedTypes = null;
		public static bool[] AorBorC_alt_0_C_node__node0_IsAllowedType = null;
		public enum AorBorC_alt_0_C_NodeNums { @_node0, };
		public enum AorBorC_alt_0_C_EdgeNums { };
		public enum AorBorC_alt_0_C_SubNums { };
		public enum AorBorC_alt_0_C_AltNums { };

#if INITIAL_WARMUP
		public Rule_AorBorC()
#else
		private Rule_AorBorC()
#endif
		{
			name = "AorBorC";
			isSubpattern = false;

			PatternGraph pat_AorBorC;
			PatternGraph AorBorC_alt_0_A;
			PatternNode AorBorC_alt_0_A_node__node0 = new PatternNode((int) NodeTypes.@A, "AorBorC_alt_0_A_node__node0", "_node0", AorBorC_alt_0_A_node__node0_AllowedTypes, AorBorC_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_A = new PatternGraph(
				"A",
				"AorBorC_alt_0_",
				new PatternNode[] { AorBorC_alt_0_A_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			PatternGraph AorBorC_alt_0_B;
			PatternNode AorBorC_alt_0_B_node__node0 = new PatternNode((int) NodeTypes.@B, "AorBorC_alt_0_B_node__node0", "_node0", AorBorC_alt_0_B_node__node0_AllowedTypes, AorBorC_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_B = new PatternGraph(
				"B",
				"AorBorC_alt_0_",
				new PatternNode[] { AorBorC_alt_0_B_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			PatternGraph AorBorC_alt_0_C;
			PatternNode AorBorC_alt_0_C_node__node0 = new PatternNode((int) NodeTypes.@C, "AorBorC_alt_0_C_node__node0", "_node0", AorBorC_alt_0_C_node__node0_AllowedTypes, AorBorC_alt_0_C_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_C = new PatternGraph(
				"C",
				"AorBorC_alt_0_",
				new PatternNode[] { AorBorC_alt_0_C_node__node0 }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			Alternative AorBorC_alt_0 = new Alternative( "alt_0", "AorBorC_", new PatternGraph[] { AorBorC_alt_0_A, AorBorC_alt_0_B, AorBorC_alt_0_C } );

			pat_AorBorC = new PatternGraph(
				"AorBorC",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { AorBorC_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);
			AorBorC_alt_0_A_node__node0.PointOfDefinition = AorBorC_alt_0_A;
			AorBorC_alt_0_B_node__node0.PointOfDefinition = AorBorC_alt_0_B;
			AorBorC_alt_0_C_node__node0.PointOfDefinition = AorBorC_alt_0_C;

			patternGraph = pat_AorBorC;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_AtoAorB : LGSPRulePattern
	{
		private static Rule_AtoAorB instance = null;
		public static Rule_AtoAorB Instance { get { if (instance==null) instance = new Rule_AtoAorB(); return instance; } }

		public static NodeType[] AtoAorB_node_a_AllowedTypes = null;
		public static bool[] AtoAorB_node_a_IsAllowedType = null;
		public enum AtoAorB_NodeNums { @a, };
		public enum AtoAorB_EdgeNums { };
		public enum AtoAorB_SubNums { };
		public enum AtoAorB_AltNums { @alt_0, };
		public enum AtoAorB_alt_0_CaseNums { @toA, @toB, };
		public static NodeType[] AtoAorB_alt_0_toA_node__node0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toA_node__node0_IsAllowedType = null;
		public static EdgeType[] AtoAorB_alt_0_toA_edge__edge0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toA_edge__edge0_IsAllowedType = null;
		public enum AtoAorB_alt_0_toA_NodeNums { @a, @_node0, };
		public enum AtoAorB_alt_0_toA_EdgeNums { @_edge0, };
		public enum AtoAorB_alt_0_toA_SubNums { };
		public enum AtoAorB_alt_0_toA_AltNums { };
		public static NodeType[] AtoAorB_alt_0_toB_node__node0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toB_node__node0_IsAllowedType = null;
		public static EdgeType[] AtoAorB_alt_0_toB_edge__edge0_AllowedTypes = null;
		public static bool[] AtoAorB_alt_0_toB_edge__edge0_IsAllowedType = null;
		public enum AtoAorB_alt_0_toB_NodeNums { @a, @_node0, };
		public enum AtoAorB_alt_0_toB_EdgeNums { @_edge0, };
		public enum AtoAorB_alt_0_toB_SubNums { };
		public enum AtoAorB_alt_0_toB_AltNums { };

#if INITIAL_WARMUP
		public Rule_AtoAorB()
#else
		private Rule_AtoAorB()
#endif
		{
			name = "AtoAorB";
			isSubpattern = false;

			PatternGraph pat_AtoAorB;
			PatternNode AtoAorB_node_a = new PatternNode((int) NodeTypes.@A, "AtoAorB_node_a", "a", AtoAorB_node_a_AllowedTypes, AtoAorB_node_a_IsAllowedType, 5.5F, -1);
			PatternGraph AtoAorB_alt_0_toA;
			PatternNode AtoAorB_alt_0_toA_node__node0 = new PatternNode((int) NodeTypes.@A, "AtoAorB_alt_0_toA_node__node0", "_node0", AtoAorB_alt_0_toA_node__node0_AllowedTypes, AtoAorB_alt_0_toA_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge AtoAorB_alt_0_toA_edge__edge0 = new PatternEdge(AtoAorB_node_a, AtoAorB_alt_0_toA_node__node0, (int) EdgeTypes.@Edge, "AtoAorB_alt_0_toA_edge__edge0", "_edge0", AtoAorB_alt_0_toA_edge__edge0_AllowedTypes, AtoAorB_alt_0_toA_edge__edge0_IsAllowedType, 5.5F, -1);
			AtoAorB_alt_0_toA = new PatternGraph(
				"toA",
				"AtoAorB_alt_0_",
				new PatternNode[] { AtoAorB_node_a, AtoAorB_alt_0_toA_node__node0 }, 
				new PatternEdge[] { AtoAorB_alt_0_toA_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			PatternGraph AtoAorB_alt_0_toB;
			PatternNode AtoAorB_alt_0_toB_node__node0 = new PatternNode((int) NodeTypes.@B, "AtoAorB_alt_0_toB_node__node0", "_node0", AtoAorB_alt_0_toB_node__node0_AllowedTypes, AtoAorB_alt_0_toB_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge AtoAorB_alt_0_toB_edge__edge0 = new PatternEdge(AtoAorB_node_a, AtoAorB_alt_0_toB_node__node0, (int) EdgeTypes.@Edge, "AtoAorB_alt_0_toB_edge__edge0", "_edge0", AtoAorB_alt_0_toB_edge__edge0_AllowedTypes, AtoAorB_alt_0_toB_edge__edge0_IsAllowedType, 5.5F, -1);
			AtoAorB_alt_0_toB = new PatternGraph(
				"toB",
				"AtoAorB_alt_0_",
				new PatternNode[] { AtoAorB_node_a, AtoAorB_alt_0_toB_node__node0 }, 
				new PatternEdge[] { AtoAorB_alt_0_toB_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			Alternative AtoAorB_alt_0 = new Alternative( "alt_0", "AtoAorB_", new PatternGraph[] { AtoAorB_alt_0_toA, AtoAorB_alt_0_toB } );

			pat_AtoAorB = new PatternGraph(
				"AtoAorB",
				"",
				new PatternNode[] { AtoAorB_node_a }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { AtoAorB_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			AtoAorB_node_a.PointOfDefinition = pat_AtoAorB;
			AtoAorB_alt_0_toA_node__node0.PointOfDefinition = AtoAorB_alt_0_toA;
			AtoAorB_alt_0_toA_edge__edge0.PointOfDefinition = AtoAorB_alt_0_toA;
			AtoAorB_alt_0_toB_node__node0.PointOfDefinition = AtoAorB_alt_0_toB;
			AtoAorB_alt_0_toB_edge__edge0.PointOfDefinition = AtoAorB_alt_0_toB;

			patternGraph = pat_AtoAorB;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_Complex : LGSPRulePattern
	{
		private static Rule_Complex instance = null;
		public static Rule_Complex Instance { get { if (instance==null) instance = new Rule_Complex(); return instance; } }

		public static NodeType[] Complex_node_a_AllowedTypes = null;
		public static NodeType[] Complex_node_b_AllowedTypes = null;
		public static bool[] Complex_node_a_IsAllowedType = null;
		public static bool[] Complex_node_b_IsAllowedType = null;
		public static EdgeType[] Complex_edge__edge0_AllowedTypes = null;
		public static EdgeType[] Complex_edge__edge1_AllowedTypes = null;
		public static bool[] Complex_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_edge__edge1_IsAllowedType = null;
		public enum Complex_NodeNums { @a, @b, };
		public enum Complex_EdgeNums { @_edge0, @_edge1, };
		public enum Complex_SubNums { };
		public enum Complex_AltNums { @alt_0, };
		public enum Complex_alt_0_CaseNums { @ExtendAv, @ExtendAv2, @ExtendNA2, };
		public static NodeType[] Complex_alt_0_ExtendAv_node_b2_AllowedTypes = null;
		public static NodeType[] Complex_alt_0_ExtendAv_node__node0_AllowedTypes = null;
		public static NodeType[] Complex_alt_0_ExtendAv_node__node1_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv_node_b2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_node__node0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_node__node1_IsAllowedType = null;
		public static EdgeType[] Complex_alt_0_ExtendAv_edge__edge0_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendAv_edge__edge1_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendAv_edge__edge2_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendAv_edge__edge3_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv_edge__edge3_IsAllowedType = null;
		public enum Complex_alt_0_ExtendAv_NodeNums { @a, @b2, @b, @_node0, @_node1, };
		public enum Complex_alt_0_ExtendAv_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum Complex_alt_0_ExtendAv_SubNums { };
		public enum Complex_alt_0_ExtendAv_AltNums { };
		public static NodeType[] Complex_alt_0_ExtendAv2_node_b2_AllowedTypes = null;
		public static NodeType[] Complex_alt_0_ExtendAv2_node__node0_AllowedTypes = null;
		public static NodeType[] Complex_alt_0_ExtendAv2_node__node1_AllowedTypes = null;
		public static NodeType[] Complex_alt_0_ExtendAv2_node__node2_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv2_node_b2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_node__node0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_node__node1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_node__node2_IsAllowedType = null;
		public static EdgeType[] Complex_alt_0_ExtendAv2_edge__edge0_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendAv2_edge__edge1_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendAv2_edge__edge2_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendAv2_edge__edge3_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendAv2_edge__edge4_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge3_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendAv2_edge__edge4_IsAllowedType = null;
		public enum Complex_alt_0_ExtendAv2_NodeNums { @a, @b2, @b, @_node0, @_node1, @_node2, };
		public enum Complex_alt_0_ExtendAv2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, @_edge4, };
		public enum Complex_alt_0_ExtendAv2_SubNums { };
		public enum Complex_alt_0_ExtendAv2_AltNums { };
		public static NodeType[] Complex_alt_0_ExtendNA2_node__node0_AllowedTypes = null;
		public static NodeType[] Complex_alt_0_ExtendNA2_node__node1_AllowedTypes = null;
		public static NodeType[] Complex_alt_0_ExtendNA2_node_b2_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendNA2_node__node0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_node__node1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_node_b2_IsAllowedType = null;
		public static EdgeType[] Complex_alt_0_ExtendNA2_edge__edge0_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendNA2_edge__edge1_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendNA2_edge__edge2_AllowedTypes = null;
		public static EdgeType[] Complex_alt_0_ExtendNA2_edge__edge3_AllowedTypes = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge0_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge1_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge2_IsAllowedType = null;
		public static bool[] Complex_alt_0_ExtendNA2_edge__edge3_IsAllowedType = null;
		public enum Complex_alt_0_ExtendNA2_NodeNums { @a, @_node0, @_node1, @b, @b2, };
		public enum Complex_alt_0_ExtendNA2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum Complex_alt_0_ExtendNA2_SubNums { };
		public enum Complex_alt_0_ExtendNA2_AltNums { };

#if INITIAL_WARMUP
		public Rule_Complex()
#else
		private Rule_Complex()
#endif
		{
			name = "Complex";
			isSubpattern = false;

			PatternGraph pat_Complex;
			PatternNode Complex_node_a = new PatternNode((int) NodeTypes.@A, "Complex_node_a", "a", Complex_node_a_AllowedTypes, Complex_node_a_IsAllowedType, 5.5F, -1);
			PatternNode Complex_node_b = new PatternNode((int) NodeTypes.@B, "Complex_node_b", "b", Complex_node_b_AllowedTypes, Complex_node_b_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_edge__edge0 = new PatternEdge(Complex_node_a, Complex_node_b, (int) EdgeTypes.@Edge, "Complex_edge__edge0", "_edge0", Complex_edge__edge0_AllowedTypes, Complex_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_edge__edge1 = new PatternEdge(Complex_node_b, Complex_node_a, (int) EdgeTypes.@Edge, "Complex_edge__edge1", "_edge1", Complex_edge__edge1_AllowedTypes, Complex_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternGraph Complex_alt_0_ExtendAv;
			PatternNode Complex_alt_0_ExtendAv_node_b2 = new PatternNode((int) NodeTypes.@B, "Complex_alt_0_ExtendAv_node_b2", "b2", Complex_alt_0_ExtendAv_node_b2_AllowedTypes, Complex_alt_0_ExtendAv_node_b2_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv_node__node0 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv_node__node0", "_node0", Complex_alt_0_ExtendAv_node__node0_AllowedTypes, Complex_alt_0_ExtendAv_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv_node__node1 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv_node__node1", "_node1", Complex_alt_0_ExtendAv_node__node1_AllowedTypes, Complex_alt_0_ExtendAv_node__node1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge0 = new PatternEdge(Complex_node_a, Complex_alt_0_ExtendAv_node_b2, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge0", "_edge0", Complex_alt_0_ExtendAv_edge__edge0_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge1 = new PatternEdge(Complex_alt_0_ExtendAv_node_b2, Complex_node_a, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge1", "_edge1", Complex_alt_0_ExtendAv_edge__edge1_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge2 = new PatternEdge(Complex_node_b, Complex_alt_0_ExtendAv_node__node0, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge2", "_edge2", Complex_alt_0_ExtendAv_edge__edge2_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge3 = new PatternEdge(Complex_alt_0_ExtendAv_node__node0, Complex_alt_0_ExtendAv_node__node1, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge3", "_edge3", Complex_alt_0_ExtendAv_edge__edge3_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge3_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendAv = new PatternGraph(
				"ExtendAv",
				"Complex_alt_0_",
				new PatternNode[] { Complex_node_a, Complex_alt_0_ExtendAv_node_b2, Complex_node_b, Complex_alt_0_ExtendAv_node__node0, Complex_alt_0_ExtendAv_node__node1 }, 
				new PatternEdge[] { Complex_alt_0_ExtendAv_edge__edge0, Complex_alt_0_ExtendAv_edge__edge1, Complex_alt_0_ExtendAv_edge__edge2, Complex_alt_0_ExtendAv_edge__edge3 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
				new bool[] {
					false, false, false, false, false, },
				new bool[] {
					false, false, false, false, },
				new bool[] {
					true, true, true, true, true, },
				new bool[] {
					true, true, true, true, }
			);
			PatternGraph Complex_alt_0_ExtendAv2;
			PatternNode Complex_alt_0_ExtendAv2_node_b2 = new PatternNode((int) NodeTypes.@B, "Complex_alt_0_ExtendAv2_node_b2", "b2", Complex_alt_0_ExtendAv2_node_b2_AllowedTypes, Complex_alt_0_ExtendAv2_node_b2_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv2_node__node0 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv2_node__node0", "_node0", Complex_alt_0_ExtendAv2_node__node0_AllowedTypes, Complex_alt_0_ExtendAv2_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv2_node__node1 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv2_node__node1", "_node1", Complex_alt_0_ExtendAv2_node__node1_AllowedTypes, Complex_alt_0_ExtendAv2_node__node1_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv2_node__node2 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv2_node__node2", "_node2", Complex_alt_0_ExtendAv2_node__node2_AllowedTypes, Complex_alt_0_ExtendAv2_node__node2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge0 = new PatternEdge(Complex_node_a, Complex_alt_0_ExtendAv2_node_b2, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge0", "_edge0", Complex_alt_0_ExtendAv2_edge__edge0_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge1 = new PatternEdge(Complex_alt_0_ExtendAv2_node_b2, Complex_node_a, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge1", "_edge1", Complex_alt_0_ExtendAv2_edge__edge1_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge2 = new PatternEdge(Complex_node_b, Complex_alt_0_ExtendAv2_node__node0, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge2", "_edge2", Complex_alt_0_ExtendAv2_edge__edge2_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge3 = new PatternEdge(Complex_alt_0_ExtendAv2_node__node0, Complex_alt_0_ExtendAv2_node__node1, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge3", "_edge3", Complex_alt_0_ExtendAv2_edge__edge3_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge3_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge4 = new PatternEdge(Complex_alt_0_ExtendAv2_node__node1, Complex_alt_0_ExtendAv2_node__node2, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge4", "_edge4", Complex_alt_0_ExtendAv2_edge__edge4_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge4_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendAv2 = new PatternGraph(
				"ExtendAv2",
				"Complex_alt_0_",
				new PatternNode[] { Complex_node_a, Complex_alt_0_ExtendAv2_node_b2, Complex_node_b, Complex_alt_0_ExtendAv2_node__node0, Complex_alt_0_ExtendAv2_node__node1, Complex_alt_0_ExtendAv2_node__node2 }, 
				new PatternEdge[] { Complex_alt_0_ExtendAv2_edge__edge0, Complex_alt_0_ExtendAv2_edge__edge1, Complex_alt_0_ExtendAv2_edge__edge2, Complex_alt_0_ExtendAv2_edge__edge3, Complex_alt_0_ExtendAv2_edge__edge4 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
				new bool[] {
					false, false, false, false, false, false, },
				new bool[] {
					false, false, false, false, false, },
				new bool[] {
					true, true, true, true, true, true, },
				new bool[] {
					true, true, true, true, true, }
			);
			PatternGraph Complex_alt_0_ExtendNA2;
			PatternNode Complex_alt_0_ExtendNA2_node__node0 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendNA2_node__node0", "_node0", Complex_alt_0_ExtendNA2_node__node0_AllowedTypes, Complex_alt_0_ExtendNA2_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendNA2_node__node1 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendNA2_node__node1", "_node1", Complex_alt_0_ExtendNA2_node__node1_AllowedTypes, Complex_alt_0_ExtendNA2_node__node1_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendNA2_node_b2 = new PatternNode((int) NodeTypes.@B, "Complex_alt_0_ExtendNA2_node_b2", "b2", Complex_alt_0_ExtendNA2_node_b2_AllowedTypes, Complex_alt_0_ExtendNA2_node_b2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge0 = new PatternEdge(Complex_node_a, Complex_alt_0_ExtendNA2_node__node0, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge0", "_edge0", Complex_alt_0_ExtendNA2_edge__edge0_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge1 = new PatternEdge(Complex_alt_0_ExtendNA2_node__node0, Complex_alt_0_ExtendNA2_node__node1, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge1", "_edge1", Complex_alt_0_ExtendNA2_edge__edge1_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge2 = new PatternEdge(Complex_node_b, Complex_alt_0_ExtendNA2_node_b2, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge2", "_edge2", Complex_alt_0_ExtendNA2_edge__edge2_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge3 = new PatternEdge(Complex_alt_0_ExtendNA2_node_b2, Complex_node_b, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge3", "_edge3", Complex_alt_0_ExtendNA2_edge__edge3_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge3_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendNA2 = new PatternGraph(
				"ExtendNA2",
				"Complex_alt_0_",
				new PatternNode[] { Complex_node_a, Complex_alt_0_ExtendNA2_node__node0, Complex_alt_0_ExtendNA2_node__node1, Complex_node_b, Complex_alt_0_ExtendNA2_node_b2 }, 
				new PatternEdge[] { Complex_alt_0_ExtendNA2_edge__edge0, Complex_alt_0_ExtendNA2_edge__edge1, Complex_alt_0_ExtendNA2_edge__edge2, Complex_alt_0_ExtendNA2_edge__edge3 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
				new bool[] {
					false, false, false, false, false, },
				new bool[] {
					false, false, false, false, },
				new bool[] {
					true, true, true, true, true, },
				new bool[] {
					true, true, true, true, }
			);
			Alternative Complex_alt_0 = new Alternative( "alt_0", "Complex_", new PatternGraph[] { Complex_alt_0_ExtendAv, Complex_alt_0_ExtendAv2, Complex_alt_0_ExtendNA2 } );

			pat_Complex = new PatternGraph(
				"Complex",
				"",
				new PatternNode[] { Complex_node_a, Complex_node_b }, 
				new PatternEdge[] { Complex_edge__edge0, Complex_edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { Complex_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, true, }
			);
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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_XtoAorB : LGSPRulePattern
	{
		private static Rule_XtoAorB instance = null;
		public static Rule_XtoAorB Instance { get { if (instance==null) instance = new Rule_XtoAorB(); return instance; } }

		public static NodeType[] XtoAorB_node_x_AllowedTypes = null;
		public static bool[] XtoAorB_node_x_IsAllowedType = null;
		public enum XtoAorB_NodeNums { @x, };
		public enum XtoAorB_EdgeNums { };
		public enum XtoAorB_SubNums { @_subpattern0, };
		public enum XtoAorB_AltNums { };

#if INITIAL_WARMUP
		public Rule_XtoAorB()
#else
		private Rule_XtoAorB()
#endif
		{
			name = "XtoAorB";
			isSubpattern = false;

			PatternGraph pat_XtoAorB;
			PatternNode XtoAorB_node_x = new PatternNode((int) NodeTypes.@Node, "XtoAorB_node_x", "x", XtoAorB_node_x_AllowedTypes, XtoAorB_node_x_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding XtoAorB__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_toAorB.Instance, new PatternElement[] { XtoAorB_node_x });
			pat_XtoAorB = new PatternGraph(
				"XtoAorB",
				"",
				new PatternNode[] { XtoAorB_node_x }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] { XtoAorB__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);
			XtoAorB_node_x.PointOfDefinition = pat_XtoAorB;
			XtoAorB__subpattern0.PointOfDefinition = pat_XtoAorB;

			patternGraph = pat_XtoAorB;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createA : LGSPRulePattern
	{
		private static Rule_createA instance = null;
		public static Rule_createA Instance { get { if (instance==null) instance = new Rule_createA(); return instance; } }

		public enum createA_NodeNums { };
		public enum createA_EdgeNums { };
		public enum createA_SubNums { };
		public enum createA_AltNums { };

#if INITIAL_WARMUP
		public Rule_createA()
#else
		private Rule_createA()
#endif
		{
			name = "createA";
			isSubpattern = false;

			PatternGraph pat_createA;
			pat_createA = new PatternGraph(
				"createA",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);

			patternGraph = pat_createA;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_A node__node0 = Node_A.CreateNode(graph);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_A node__node0 = Node_A.CreateNode(graph);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node0" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createABA : LGSPRulePattern
	{
		private static Rule_createABA instance = null;
		public static Rule_createABA Instance { get { if (instance==null) instance = new Rule_createABA(); return instance; } }

		public enum createABA_NodeNums { };
		public enum createABA_EdgeNums { };
		public enum createABA_SubNums { };
		public enum createABA_AltNums { };

#if INITIAL_WARMUP
		public Rule_createABA()
#else
		private Rule_createABA()
#endif
		{
			name = "createABA";
			isSubpattern = false;

			PatternGraph pat_createABA;
			pat_createABA = new PatternGraph(
				"createABA",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);

			patternGraph = pat_createABA;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node__node0 = Node_B.CreateNode(graph);
			Node_A node_a = Node_A.CreateNode(graph);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node__node0, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node__node0);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node__node0 = Node_B.CreateNode(graph);
			Node_A node_a = Node_A.CreateNode(graph);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node__node0, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node__node0);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node0", "a" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1", "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createAtoB : LGSPRulePattern
	{
		private static Rule_createAtoB instance = null;
		public static Rule_createAtoB Instance { get { if (instance==null) instance = new Rule_createAtoB(); return instance; } }

		public enum createAtoB_NodeNums { };
		public enum createAtoB_EdgeNums { };
		public enum createAtoB_SubNums { };
		public enum createAtoB_AltNums { };

#if INITIAL_WARMUP
		public Rule_createAtoB()
#else
		private Rule_createAtoB()
#endif
		{
			name = "createAtoB";
			isSubpattern = false;

			PatternGraph pat_createAtoB;
			pat_createAtoB = new PatternGraph(
				"createAtoB",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);

			patternGraph = pat_createAtoB;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_A node__node0 = Node_A.CreateNode(graph);
			Node_B node__node1 = Node_B.CreateNode(graph);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_A node__node0 = Node_A.CreateNode(graph);
			Node_B node__node1 = Node_B.CreateNode(graph);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node0", "_node1" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createB : LGSPRulePattern
	{
		private static Rule_createB instance = null;
		public static Rule_createB Instance { get { if (instance==null) instance = new Rule_createB(); return instance; } }

		public enum createB_NodeNums { };
		public enum createB_EdgeNums { };
		public enum createB_SubNums { };
		public enum createB_AltNums { };

#if INITIAL_WARMUP
		public Rule_createB()
#else
		private Rule_createB()
#endif
		{
			name = "createB";
			isSubpattern = false;

			PatternGraph pat_createB;
			pat_createB = new PatternGraph(
				"createB",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);

			patternGraph = pat_createB;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node__node0 = Node_B.CreateNode(graph);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node__node0 = Node_B.CreateNode(graph);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node0" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createC : LGSPRulePattern
	{
		private static Rule_createC instance = null;
		public static Rule_createC Instance { get { if (instance==null) instance = new Rule_createC(); return instance; } }

		public enum createC_NodeNums { };
		public enum createC_EdgeNums { };
		public enum createC_SubNums { };
		public enum createC_AltNums { };

#if INITIAL_WARMUP
		public Rule_createC()
#else
		private Rule_createC()
#endif
		{
			name = "createC";
			isSubpattern = false;

			PatternGraph pat_createC;
			pat_createC = new PatternGraph(
				"createC",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);

			patternGraph = pat_createC;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_C node__node0 = Node_C.CreateNode(graph);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_C node__node0 = Node_C.CreateNode(graph);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node0" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_createComplex : LGSPRulePattern
	{
		private static Rule_createComplex instance = null;
		public static Rule_createComplex Instance { get { if (instance==null) instance = new Rule_createComplex(); return instance; } }

		public enum createComplex_NodeNums { };
		public enum createComplex_EdgeNums { };
		public enum createComplex_SubNums { };
		public enum createComplex_AltNums { };

#if INITIAL_WARMUP
		public Rule_createComplex()
#else
		private Rule_createComplex()
#endif
		{
			name = "createComplex";
			isSubpattern = false;

			PatternGraph pat_createComplex;
			pat_createComplex = new PatternGraph(
				"createComplex",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);

			patternGraph = pat_createComplex;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_C node__node0 = Node_C.CreateNode(graph);
			Node_C node__node2 = Node_C.CreateNode(graph);
			Node_B node_b2 = Node_B.CreateNode(graph);
			Node_C node__node1 = Node_C.CreateNode(graph);
			Node_B node_b = Node_B.CreateNode(graph);
			Node_A node_a = Node_A.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_b2, node_a);
			Edge_Edge edge__edge5 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			Edge_Edge edge__edge6 = Edge_Edge.CreateEdge(graph, node__node1, node__node2);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_a, node_b2);
			Edge_Edge edge__edge4 = Edge_Edge.CreateEdge(graph, node_b, node__node0);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_C node__node0 = Node_C.CreateNode(graph);
			Node_C node__node2 = Node_C.CreateNode(graph);
			Node_B node_b2 = Node_B.CreateNode(graph);
			Node_C node__node1 = Node_C.CreateNode(graph);
			Node_B node_b = Node_B.CreateNode(graph);
			Node_A node_a = Node_A.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_b2, node_a);
			Edge_Edge edge__edge5 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			Edge_Edge edge__edge6 = Edge_Edge.CreateEdge(graph, node__node1, node__node2);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_a, node_b2);
			Edge_Edge edge__edge4 = Edge_Edge.CreateEdge(graph, node_b, node__node0);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node0", "_node2", "b2", "_node1", "b", "a" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge3", "_edge5", "_edge1", "_edge0", "_edge6", "_edge2", "_edge4" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_homm : LGSPRulePattern
	{
		private static Rule_homm instance = null;
		public static Rule_homm Instance { get { if (instance==null) instance = new Rule_homm(); return instance; } }

		public static NodeType[] homm_node_a_AllowedTypes = null;
		public static NodeType[] homm_node_b_AllowedTypes = null;
		public static bool[] homm_node_a_IsAllowedType = null;
		public static bool[] homm_node_b_IsAllowedType = null;
		public static EdgeType[] homm_edge__edge0_AllowedTypes = null;
		public static EdgeType[] homm_edge__edge1_AllowedTypes = null;
		public static bool[] homm_edge__edge0_IsAllowedType = null;
		public static bool[] homm_edge__edge1_IsAllowedType = null;
		public enum homm_NodeNums { @a, @b, };
		public enum homm_EdgeNums { @_edge0, @_edge1, };
		public enum homm_SubNums { };
		public enum homm_AltNums { @alt_0, };
		public enum homm_alt_0_CaseNums { @case1, @case2, };
		public static NodeType[] homm_alt_0_case1_node_b2_AllowedTypes = null;
		public static bool[] homm_alt_0_case1_node_b2_IsAllowedType = null;
		public static EdgeType[] homm_alt_0_case1_edge__edge0_AllowedTypes = null;
		public static EdgeType[] homm_alt_0_case1_edge__edge1_AllowedTypes = null;
		public static bool[] homm_alt_0_case1_edge__edge0_IsAllowedType = null;
		public static bool[] homm_alt_0_case1_edge__edge1_IsAllowedType = null;
		public enum homm_alt_0_case1_NodeNums { @a, @b2, };
		public enum homm_alt_0_case1_EdgeNums { @_edge0, @_edge1, };
		public enum homm_alt_0_case1_SubNums { };
		public enum homm_alt_0_case1_AltNums { };
		public static NodeType[] homm_alt_0_case2_node_b2_AllowedTypes = null;
		public static bool[] homm_alt_0_case2_node_b2_IsAllowedType = null;
		public static EdgeType[] homm_alt_0_case2_edge__edge0_AllowedTypes = null;
		public static EdgeType[] homm_alt_0_case2_edge__edge1_AllowedTypes = null;
		public static bool[] homm_alt_0_case2_edge__edge0_IsAllowedType = null;
		public static bool[] homm_alt_0_case2_edge__edge1_IsAllowedType = null;
		public enum homm_alt_0_case2_NodeNums { @a, @b2, };
		public enum homm_alt_0_case2_EdgeNums { @_edge0, @_edge1, };
		public enum homm_alt_0_case2_SubNums { };
		public enum homm_alt_0_case2_AltNums { };

#if INITIAL_WARMUP
		public Rule_homm()
#else
		private Rule_homm()
#endif
		{
			name = "homm";
			isSubpattern = false;

			PatternGraph pat_homm;
			PatternNode homm_node_a = new PatternNode((int) NodeTypes.@A, "homm_node_a", "a", homm_node_a_AllowedTypes, homm_node_a_IsAllowedType, 5.5F, -1);
			PatternNode homm_node_b = new PatternNode((int) NodeTypes.@B, "homm_node_b", "b", homm_node_b_AllowedTypes, homm_node_b_IsAllowedType, 5.5F, -1);
			PatternEdge homm_edge__edge0 = new PatternEdge(homm_node_a, homm_node_b, (int) EdgeTypes.@Edge, "homm_edge__edge0", "_edge0", homm_edge__edge0_AllowedTypes, homm_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge homm_edge__edge1 = new PatternEdge(homm_node_b, homm_node_a, (int) EdgeTypes.@Edge, "homm_edge__edge1", "_edge1", homm_edge__edge1_AllowedTypes, homm_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternGraph homm_alt_0_case1;
			PatternNode homm_alt_0_case1_node_b2 = new PatternNode((int) NodeTypes.@B, "homm_alt_0_case1_node_b2", "b2", homm_alt_0_case1_node_b2_AllowedTypes, homm_alt_0_case1_node_b2_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case1_edge__edge0 = new PatternEdge(homm_node_a, homm_alt_0_case1_node_b2, (int) EdgeTypes.@Edge, "homm_alt_0_case1_edge__edge0", "_edge0", homm_alt_0_case1_edge__edge0_AllowedTypes, homm_alt_0_case1_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case1_edge__edge1 = new PatternEdge(homm_alt_0_case1_node_b2, homm_node_a, (int) EdgeTypes.@Edge, "homm_alt_0_case1_edge__edge1", "_edge1", homm_alt_0_case1_edge__edge1_AllowedTypes, homm_alt_0_case1_edge__edge1_IsAllowedType, 5.5F, -1);
			homm_alt_0_case1 = new PatternGraph(
				"case1",
				"homm_alt_0_",
				new PatternNode[] { homm_node_a, homm_alt_0_case1_node_b2 }, 
				new PatternEdge[] { homm_alt_0_case1_edge__edge0, homm_alt_0_case1_edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, false, },
				new bool[] {
					true, false, },
				new bool[] {
					true, true, }
			);
			PatternGraph homm_alt_0_case2;
			PatternNode homm_alt_0_case2_node_b2 = new PatternNode((int) NodeTypes.@B, "homm_alt_0_case2_node_b2", "b2", homm_alt_0_case2_node_b2_AllowedTypes, homm_alt_0_case2_node_b2_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case2_edge__edge0 = new PatternEdge(homm_node_a, homm_alt_0_case2_node_b2, (int) EdgeTypes.@Edge, "homm_alt_0_case2_edge__edge0", "_edge0", homm_alt_0_case2_edge__edge0_AllowedTypes, homm_alt_0_case2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case2_edge__edge1 = new PatternEdge(homm_alt_0_case2_node_b2, homm_node_a, (int) EdgeTypes.@Edge, "homm_alt_0_case2_edge__edge1", "_edge1", homm_alt_0_case2_edge__edge1_AllowedTypes, homm_alt_0_case2_edge__edge1_IsAllowedType, 5.5F, -1);
			homm_alt_0_case2 = new PatternGraph(
				"case2",
				"homm_alt_0_",
				new PatternNode[] { homm_node_a, homm_alt_0_case2_node_b2 }, 
				new PatternEdge[] { homm_alt_0_case2_edge__edge0, homm_alt_0_case2_edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, true, }
			);
			Alternative homm_alt_0 = new Alternative( "alt_0", "homm_", new PatternGraph[] { homm_alt_0_case1, homm_alt_0_case2 } );

			pat_homm = new PatternGraph(
				"homm",
				"",
				new PatternNode[] { homm_node_a, homm_node_b }, 
				new PatternEdge[] { homm_edge__edge0, homm_edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { homm_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, true, }
			);
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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_leer : LGSPRulePattern
	{
		private static Rule_leer instance = null;
		public static Rule_leer Instance { get { if (instance==null) instance = new Rule_leer(); return instance; } }

		public enum leer_NodeNums { };
		public enum leer_EdgeNums { };
		public enum leer_SubNums { };
		public enum leer_AltNums { @alt_0, };
		public enum leer_alt_0_CaseNums { @leer, };
		public enum leer_alt_0_leer_NodeNums { };
		public enum leer_alt_0_leer_EdgeNums { };
		public enum leer_alt_0_leer_SubNums { };
		public enum leer_alt_0_leer_AltNums { };

#if INITIAL_WARMUP
		public Rule_leer()
#else
		private Rule_leer()
#endif
		{
			name = "leer";
			isSubpattern = false;

			PatternGraph pat_leer;
			PatternGraph leer_alt_0_leer;
			leer_alt_0_leer = new PatternGraph(
				"leer",
				"leer_alt_0_",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);
			Alternative leer_alt_0 = new Alternative( "alt_0", "leer_", new PatternGraph[] { leer_alt_0_leer } );

			pat_leer = new PatternGraph(
				"leer",
				"",
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { leer_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				new bool[] {},
				new bool[] {},
				new bool[] {},
				new bool[] {}
			);

			patternGraph = pat_leer;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{  // test does not have modifications
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {};
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {};
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}


    public class PatternAction_toAorB : LGSPSubpatternAction
    {
        public PatternAction_toAorB(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_toAorB.Instance.patternGraph;
        }

        public LGSPNode toAorB_node_x;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset toAorB_node_x 
            LGSPNode node_cur_toAorB_node_x = toAorB_node_x;
            // Extend outgoing toAorB_edge_y from toAorB_node_x 
            LGSPEdge edge_head_toAorB_edge_y = node_cur_toAorB_node_x.outhead;
            if(edge_head_toAorB_edge_y != null)
            {
                LGSPEdge edge_cur_toAorB_edge_y = edge_head_toAorB_edge_y;
                do
                {
                    if(!EdgeType_Edge.isMyType[edge_cur_toAorB_edge_y.type.TypeID]) {
                        continue;
                    }
                    if(edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Push alternative matching task for toAorB_alt_0
                    AlternativeAction_toAorB_alt_0 taskFor_alt_0 = new AlternativeAction_toAorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Pattern_toAorB.toAorB_AltNums.@alt_0].alternativeCases);
                    taskFor_alt_0.toAorB_edge_y = edge_cur_toAorB_edge_y;
                    openTasks.Push(taskFor_alt_0);
                    node_cur_toAorB_node_x.isMatchedByEnclosingPattern = true;
                    edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_toAorB.toAorB_NodeNums.@x] = node_cur_toAorB_node_x;
                            match.Edges[(int)Pattern_toAorB.toAorB_EdgeNums.@y] = edge_cur_toAorB_edge_y;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                            node_cur_toAorB_node_x.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                        node_cur_toAorB_node_x.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_toAorB_node_x.isMatchedByEnclosingPattern = false;
                    edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                }
                while( (edge_cur_toAorB_edge_y = edge_cur_toAorB_edge_y.outNext) != edge_head_toAorB_edge_y );
            }
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_toAorB_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_toAorB_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPEdge toAorB_edge_y;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case toAorB_alt_0_toA 
            do {
                patternGraph = patternGraphs[(int)Pattern_toAorB.toAorB_alt_0_CaseNums.@toA];
                // SubPreset toAorB_edge_y 
                LGSPEdge edge_cur_toAorB_edge_y = toAorB_edge_y;
                // Lookup toAorB_alt_0_toA_node_a 
                int node_type_id_toAorB_alt_0_toA_node_a = 1;
                for(LGSPNode node_head_toAorB_alt_0_toA_node_a = graph.nodesByTypeHeads[node_type_id_toAorB_alt_0_toA_node_a], node_cur_toAorB_alt_0_toA_node_a = node_head_toAorB_alt_0_toA_node_a.typeNext; node_cur_toAorB_alt_0_toA_node_a != node_head_toAorB_alt_0_toA_node_a; node_cur_toAorB_alt_0_toA_node_a = node_cur_toAorB_alt_0_toA_node_a.typeNext)
                {
                    if(node_cur_toAorB_alt_0_toA_node_a.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toA_NodeNums.@a] = node_cur_toAorB_alt_0_toA_node_a;
                        match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toA_EdgeNums.@y] = edge_cur_toAorB_edge_y;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_toAorB_alt_0_toA_node_a.isMatchedByEnclosingPattern = true;
                    edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toA_NodeNums.@a] = node_cur_toAorB_alt_0_toA_node_a;
                            match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toA_EdgeNums.@y] = edge_cur_toAorB_edge_y;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                            node_cur_toAorB_alt_0_toA_node_a.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                        node_cur_toAorB_alt_0_toA_node_a.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_toAorB_alt_0_toA_node_a.isMatchedByEnclosingPattern = false;
                    edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            // Alternative case toAorB_alt_0_toB 
            do {
                patternGraph = patternGraphs[(int)Pattern_toAorB.toAorB_alt_0_CaseNums.@toB];
                // SubPreset toAorB_edge_y 
                LGSPEdge edge_cur_toAorB_edge_y = toAorB_edge_y;
                // Lookup toAorB_alt_0_toB_node_b 
                int node_type_id_toAorB_alt_0_toB_node_b = 2;
                for(LGSPNode node_head_toAorB_alt_0_toB_node_b = graph.nodesByTypeHeads[node_type_id_toAorB_alt_0_toB_node_b], node_cur_toAorB_alt_0_toB_node_b = node_head_toAorB_alt_0_toB_node_b.typeNext; node_cur_toAorB_alt_0_toB_node_b != node_head_toAorB_alt_0_toB_node_b; node_cur_toAorB_alt_0_toB_node_b = node_cur_toAorB_alt_0_toB_node_b.typeNext)
                {
                    if(node_cur_toAorB_alt_0_toB_node_b.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toB_NodeNums.@b] = node_cur_toAorB_alt_0_toB_node_b;
                        match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toB_EdgeNums.@y] = edge_cur_toAorB_edge_y;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_toAorB_alt_0_toB_node_b.isMatchedByEnclosingPattern = true;
                    edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toB_NodeNums.@b] = node_cur_toAorB_alt_0_toB_node_b;
                            match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toB_EdgeNums.@y] = edge_cur_toAorB_edge_y;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                            node_cur_toAorB_alt_0_toB_node_b.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                        node_cur_toAorB_alt_0_toB_node_b.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_toAorB_alt_0_toB_node_b.isMatchedByEnclosingPattern = false;
                    edge_cur_toAorB_edge_y.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AandnotCorB : LGSPAction
    {
        public Action_AandnotCorB() {
            rulePattern = Rule_AandnotCorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "AandnotCorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AandnotCorB instance = new Action_AandnotCorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AandnotCorB_alt_0
            AlternativeAction_AandnotCorB_alt_0 taskFor_alt_0 = new AlternativeAction_AandnotCorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AandnotCorB.AandnotCorB_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_AandnotCorB_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_AandnotCorB_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AandnotCorB_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AandnotCorB.AandnotCorB_alt_0_CaseNums.@A];
                // NegativePattern 
                {
                    // Lookup AandnotCorB_alt_0_A_neg_0_node__node0 
                    int node_type_id_AandnotCorB_alt_0_A_neg_0_node__node0 = 3;
                    for(LGSPNode node_head_AandnotCorB_alt_0_A_neg_0_node__node0 = graph.nodesByTypeHeads[node_type_id_AandnotCorB_alt_0_A_neg_0_node__node0], node_cur_AandnotCorB_alt_0_A_neg_0_node__node0 = node_head_AandnotCorB_alt_0_A_neg_0_node__node0.typeNext; node_cur_AandnotCorB_alt_0_A_neg_0_node__node0 != node_head_AandnotCorB_alt_0_A_neg_0_node__node0; node_cur_AandnotCorB_alt_0_A_neg_0_node__node0 = node_cur_AandnotCorB_alt_0_A_neg_0_node__node0.typeNext)
                    {
                        if(node_cur_AandnotCorB_alt_0_A_neg_0_node__node0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        goto label0;
                    }
                }
                // Lookup AandnotCorB_alt_0_A_node__node0 
                int node_type_id_AandnotCorB_alt_0_A_node__node0 = 1;
                for(LGSPNode node_head_AandnotCorB_alt_0_A_node__node0 = graph.nodesByTypeHeads[node_type_id_AandnotCorB_alt_0_A_node__node0], node_cur_AandnotCorB_alt_0_A_node__node0 = node_head_AandnotCorB_alt_0_A_node__node0.typeNext; node_cur_AandnotCorB_alt_0_A_node__node0 != node_head_AandnotCorB_alt_0_A_node__node0; node_cur_AandnotCorB_alt_0_A_node__node0 = node_cur_AandnotCorB_alt_0_A_node__node0.typeNext)
                {
                    if(node_cur_AandnotCorB_alt_0_A_node__node0.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_A_NodeNums.@_node0] = node_cur_AandnotCorB_alt_0_A_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_AandnotCorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_A_NodeNums.@_node0] = node_cur_AandnotCorB_alt_0_A_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            node_cur_AandnotCorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        node_cur_AandnotCorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_AandnotCorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                }
label0: ;
            } while(false);
            // Alternative case AandnotCorB_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AandnotCorB.AandnotCorB_alt_0_CaseNums.@B];
                // Lookup AandnotCorB_alt_0_B_node__node0 
                int node_type_id_AandnotCorB_alt_0_B_node__node0 = 2;
                for(LGSPNode node_head_AandnotCorB_alt_0_B_node__node0 = graph.nodesByTypeHeads[node_type_id_AandnotCorB_alt_0_B_node__node0], node_cur_AandnotCorB_alt_0_B_node__node0 = node_head_AandnotCorB_alt_0_B_node__node0.typeNext; node_cur_AandnotCorB_alt_0_B_node__node0 != node_head_AandnotCorB_alt_0_B_node__node0; node_cur_AandnotCorB_alt_0_B_node__node0 = node_cur_AandnotCorB_alt_0_B_node__node0.typeNext)
                {
                    if(node_cur_AandnotCorB_alt_0_B_node__node0.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_B_NodeNums.@_node0] = node_cur_AandnotCorB_alt_0_B_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_AandnotCorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_B_NodeNums.@_node0] = node_cur_AandnotCorB_alt_0_B_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            node_cur_AandnotCorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        node_cur_AandnotCorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_AandnotCorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AorB : LGSPAction
    {
        public Action_AorB() {
            rulePattern = Rule_AorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "AorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AorB instance = new Action_AorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AorB_alt_0
            AlternativeAction_AorB_alt_0 taskFor_alt_0 = new AlternativeAction_AorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AorB.AorB_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_AorB_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_AorB_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AorB_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AorB.AorB_alt_0_CaseNums.@A];
                // Lookup AorB_alt_0_A_node__node0 
                int node_type_id_AorB_alt_0_A_node__node0 = 1;
                for(LGSPNode node_head_AorB_alt_0_A_node__node0 = graph.nodesByTypeHeads[node_type_id_AorB_alt_0_A_node__node0], node_cur_AorB_alt_0_A_node__node0 = node_head_AorB_alt_0_A_node__node0.typeNext; node_cur_AorB_alt_0_A_node__node0 != node_head_AorB_alt_0_A_node__node0; node_cur_AorB_alt_0_A_node__node0 = node_cur_AorB_alt_0_A_node__node0.typeNext)
                {
                    if(node_cur_AorB_alt_0_A_node__node0.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Rule_AorB.AorB_alt_0_A_NodeNums.@_node0] = node_cur_AorB_alt_0_A_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_AorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorB.AorB_alt_0_A_NodeNums.@_node0] = node_cur_AorB_alt_0_A_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            node_cur_AorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        node_cur_AorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_AorB_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            // Alternative case AorB_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AorB.AorB_alt_0_CaseNums.@B];
                // Lookup AorB_alt_0_B_node__node0 
                int node_type_id_AorB_alt_0_B_node__node0 = 2;
                for(LGSPNode node_head_AorB_alt_0_B_node__node0 = graph.nodesByTypeHeads[node_type_id_AorB_alt_0_B_node__node0], node_cur_AorB_alt_0_B_node__node0 = node_head_AorB_alt_0_B_node__node0.typeNext; node_cur_AorB_alt_0_B_node__node0 != node_head_AorB_alt_0_B_node__node0; node_cur_AorB_alt_0_B_node__node0 = node_cur_AorB_alt_0_B_node__node0.typeNext)
                {
                    if(node_cur_AorB_alt_0_B_node__node0.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Rule_AorB.AorB_alt_0_B_NodeNums.@_node0] = node_cur_AorB_alt_0_B_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_AorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorB.AorB_alt_0_B_NodeNums.@_node0] = node_cur_AorB_alt_0_B_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            node_cur_AorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        node_cur_AorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_AorB_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AorBorC : LGSPAction
    {
        public Action_AorBorC() {
            rulePattern = Rule_AorBorC.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "AorBorC"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AorBorC instance = new Action_AorBorC();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AorBorC_alt_0
            AlternativeAction_AorBorC_alt_0 taskFor_alt_0 = new AlternativeAction_AorBorC_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AorBorC.AorBorC_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_AorBorC_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_AorBorC_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AorBorC_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@A];
                // Lookup AorBorC_alt_0_A_node__node0 
                int node_type_id_AorBorC_alt_0_A_node__node0 = 1;
                for(LGSPNode node_head_AorBorC_alt_0_A_node__node0 = graph.nodesByTypeHeads[node_type_id_AorBorC_alt_0_A_node__node0], node_cur_AorBorC_alt_0_A_node__node0 = node_head_AorBorC_alt_0_A_node__node0.typeNext; node_cur_AorBorC_alt_0_A_node__node0 != node_head_AorBorC_alt_0_A_node__node0; node_cur_AorBorC_alt_0_A_node__node0 = node_cur_AorBorC_alt_0_A_node__node0.typeNext)
                {
                    if(node_cur_AorBorC_alt_0_A_node__node0.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_A_NodeNums.@_node0] = node_cur_AorBorC_alt_0_A_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_AorBorC_alt_0_A_node__node0.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_A_NodeNums.@_node0] = node_cur_AorBorC_alt_0_A_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            node_cur_AorBorC_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        node_cur_AorBorC_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_AorBorC_alt_0_A_node__node0.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            // Alternative case AorBorC_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@B];
                // Lookup AorBorC_alt_0_B_node__node0 
                int node_type_id_AorBorC_alt_0_B_node__node0 = 2;
                for(LGSPNode node_head_AorBorC_alt_0_B_node__node0 = graph.nodesByTypeHeads[node_type_id_AorBorC_alt_0_B_node__node0], node_cur_AorBorC_alt_0_B_node__node0 = node_head_AorBorC_alt_0_B_node__node0.typeNext; node_cur_AorBorC_alt_0_B_node__node0 != node_head_AorBorC_alt_0_B_node__node0; node_cur_AorBorC_alt_0_B_node__node0 = node_cur_AorBorC_alt_0_B_node__node0.typeNext)
                {
                    if(node_cur_AorBorC_alt_0_B_node__node0.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_B_NodeNums.@_node0] = node_cur_AorBorC_alt_0_B_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_AorBorC_alt_0_B_node__node0.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_B_NodeNums.@_node0] = node_cur_AorBorC_alt_0_B_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            node_cur_AorBorC_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        node_cur_AorBorC_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_AorBorC_alt_0_B_node__node0.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            // Alternative case AorBorC_alt_0_C 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@C];
                // Lookup AorBorC_alt_0_C_node__node0 
                int node_type_id_AorBorC_alt_0_C_node__node0 = 3;
                for(LGSPNode node_head_AorBorC_alt_0_C_node__node0 = graph.nodesByTypeHeads[node_type_id_AorBorC_alt_0_C_node__node0], node_cur_AorBorC_alt_0_C_node__node0 = node_head_AorBorC_alt_0_C_node__node0.typeNext; node_cur_AorBorC_alt_0_C_node__node0 != node_head_AorBorC_alt_0_C_node__node0; node_cur_AorBorC_alt_0_C_node__node0 = node_cur_AorBorC_alt_0_C_node__node0.typeNext)
                {
                    if(node_cur_AorBorC_alt_0_C_node__node0.isMatchedByEnclosingPattern)
                    {
                        continue;
                    }
                    // Check whether there are subpattern matching tasks left to execute
                    if(openTasks.Count==0)
                    {
                        Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                        foundPartialMatches.Add(currentFoundPartialMatch);
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_C_NodeNums.@_node0] = node_cur_AorBorC_alt_0_C_node__node0;
                        currentFoundPartialMatch.Push(match);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            openTasks.Push(this);
                            return;
                        }
                        continue;
                    }
                    node_cur_AorBorC_alt_0_C_node__node0.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_C_NodeNums.@_node0] = node_cur_AorBorC_alt_0_C_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList)
                            {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            node_cur_AorBorC_alt_0_C_node__node0.isMatchedByEnclosingPattern = false;
                            openTasks.Push(this);
                            return;
                        }
                        node_cur_AorBorC_alt_0_C_node__node0.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_AorBorC_alt_0_C_node__node0.isMatchedByEnclosingPattern = false;
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_AtoAorB : LGSPAction
    {
        public Action_AtoAorB() {
            rulePattern = Rule_AtoAorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 0);
        }

        public override string Name { get { return "AtoAorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AtoAorB instance = new Action_AtoAorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup AtoAorB_node_a 
            int node_type_id_AtoAorB_node_a = 1;
            for(LGSPNode node_head_AtoAorB_node_a = graph.nodesByTypeHeads[node_type_id_AtoAorB_node_a], node_cur_AtoAorB_node_a = node_head_AtoAorB_node_a.typeNext; node_cur_AtoAorB_node_a != node_head_AtoAorB_node_a; node_cur_AtoAorB_node_a = node_cur_AtoAorB_node_a.typeNext)
            {
                // Push alternative matching task for AtoAorB_alt_0
                AlternativeAction_AtoAorB_alt_0 taskFor_alt_0 = new AlternativeAction_AtoAorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AtoAorB.AtoAorB_AltNums.@alt_0].alternativeCases);
                taskFor_alt_0.AtoAorB_node_a = node_cur_AtoAorB_node_a;
                openTasks.Push(taskFor_alt_0);
                node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = true;
                // Match subpatterns
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_AtoAorB.AtoAorB_NodeNums.@a] = node_cur_AtoAorB_node_a;
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                        return matches;
                    }
                    node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                    continue;
                }
                node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
            }
            return matches;
        }
    }

    public class AlternativeAction_AtoAorB_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_AtoAorB_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode AtoAorB_node_a;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AtoAorB_alt_0_toA 
            do {
                patternGraph = patternGraphs[(int)Rule_AtoAorB.AtoAorB_alt_0_CaseNums.@toA];
                // SubPreset AtoAorB_node_a 
                LGSPNode node_cur_AtoAorB_node_a = AtoAorB_node_a;
                // Extend outgoing AtoAorB_alt_0_toA_edge__edge0 from AtoAorB_node_a 
                LGSPEdge edge_head_AtoAorB_alt_0_toA_edge__edge0 = node_cur_AtoAorB_node_a.outhead;
                if(edge_head_AtoAorB_alt_0_toA_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_AtoAorB_alt_0_toA_edge__edge0 = edge_head_AtoAorB_alt_0_toA_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_AtoAorB_alt_0_toA_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_AtoAorB_alt_0_toA_edge__edge0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        // Implicit target AtoAorB_alt_0_toA_node__node0 from AtoAorB_alt_0_toA_edge__edge0 
                        LGSPNode node_cur_AtoAorB_alt_0_toA_node__node0 = edge_cur_AtoAorB_alt_0_toA_edge__edge0.target;
                        if(!NodeType_A.isMyType[node_cur_AtoAorB_alt_0_toA_node__node0.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_AtoAorB_alt_0_toA_node__node0.isMatched
                            && node_cur_AtoAorB_alt_0_toA_node__node0==node_cur_AtoAorB_node_a
                            )
                        {
                            continue;
                        }
                        if(node_cur_AtoAorB_alt_0_toA_node__node0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@a] = node_cur_AtoAorB_node_a;
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@_node0] = node_cur_AtoAorB_alt_0_toA_node__node0;
                            match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_EdgeNums.@_edge0] = edge_cur_AtoAorB_alt_0_toA_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = true;
                        node_cur_AtoAorB_alt_0_toA_node__node0.isMatchedByEnclosingPattern = true;
                        edge_cur_AtoAorB_alt_0_toA_edge__edge0.isMatchedByEnclosingPattern = true;
                        // Match subpatterns
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@a] = node_cur_AtoAorB_node_a;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@_node0] = node_cur_AtoAorB_alt_0_toA_node__node0;
                                match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_EdgeNums.@_edge0] = edge_cur_AtoAorB_alt_0_toA_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList)
                                {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                edge_cur_AtoAorB_alt_0_toA_edge__edge0.isMatchedByEnclosingPattern = false;
                                node_cur_AtoAorB_alt_0_toA_node__node0.isMatchedByEnclosingPattern = false;
                                node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                                openTasks.Push(this);
                                return;
                            }
                            edge_cur_AtoAorB_alt_0_toA_edge__edge0.isMatchedByEnclosingPattern = false;
                            node_cur_AtoAorB_alt_0_toA_node__node0.isMatchedByEnclosingPattern = false;
                            node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                            continue;
                        }
                        node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                        node_cur_AtoAorB_alt_0_toA_node__node0.isMatchedByEnclosingPattern = false;
                        edge_cur_AtoAorB_alt_0_toA_edge__edge0.isMatchedByEnclosingPattern = false;
                    }
                    while( (edge_cur_AtoAorB_alt_0_toA_edge__edge0 = edge_cur_AtoAorB_alt_0_toA_edge__edge0.outNext) != edge_head_AtoAorB_alt_0_toA_edge__edge0 );
                }
            } while(false);
            // Alternative case AtoAorB_alt_0_toB 
            do {
                patternGraph = patternGraphs[(int)Rule_AtoAorB.AtoAorB_alt_0_CaseNums.@toB];
                // SubPreset AtoAorB_node_a 
                LGSPNode node_cur_AtoAorB_node_a = AtoAorB_node_a;
                // Extend outgoing AtoAorB_alt_0_toB_edge__edge0 from AtoAorB_node_a 
                LGSPEdge edge_head_AtoAorB_alt_0_toB_edge__edge0 = node_cur_AtoAorB_node_a.outhead;
                if(edge_head_AtoAorB_alt_0_toB_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_AtoAorB_alt_0_toB_edge__edge0 = edge_head_AtoAorB_alt_0_toB_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_AtoAorB_alt_0_toB_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_AtoAorB_alt_0_toB_edge__edge0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        // Implicit target AtoAorB_alt_0_toB_node__node0 from AtoAorB_alt_0_toB_edge__edge0 
                        LGSPNode node_cur_AtoAorB_alt_0_toB_node__node0 = edge_cur_AtoAorB_alt_0_toB_edge__edge0.target;
                        if(!NodeType_B.isMyType[node_cur_AtoAorB_alt_0_toB_node__node0.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_AtoAorB_alt_0_toB_node__node0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@a] = node_cur_AtoAorB_node_a;
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@_node0] = node_cur_AtoAorB_alt_0_toB_node__node0;
                            match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_EdgeNums.@_edge0] = edge_cur_AtoAorB_alt_0_toB_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = true;
                        node_cur_AtoAorB_alt_0_toB_node__node0.isMatchedByEnclosingPattern = true;
                        edge_cur_AtoAorB_alt_0_toB_edge__edge0.isMatchedByEnclosingPattern = true;
                        // Match subpatterns
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@a] = node_cur_AtoAorB_node_a;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@_node0] = node_cur_AtoAorB_alt_0_toB_node__node0;
                                match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_EdgeNums.@_edge0] = edge_cur_AtoAorB_alt_0_toB_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList)
                                {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                edge_cur_AtoAorB_alt_0_toB_edge__edge0.isMatchedByEnclosingPattern = false;
                                node_cur_AtoAorB_alt_0_toB_node__node0.isMatchedByEnclosingPattern = false;
                                node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                                openTasks.Push(this);
                                return;
                            }
                            edge_cur_AtoAorB_alt_0_toB_edge__edge0.isMatchedByEnclosingPattern = false;
                            node_cur_AtoAorB_alt_0_toB_node__node0.isMatchedByEnclosingPattern = false;
                            node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                            continue;
                        }
                        node_cur_AtoAorB_node_a.isMatchedByEnclosingPattern = false;
                        node_cur_AtoAorB_alt_0_toB_node__node0.isMatchedByEnclosingPattern = false;
                        edge_cur_AtoAorB_alt_0_toB_edge__edge0.isMatchedByEnclosingPattern = false;
                    }
                    while( (edge_cur_AtoAorB_alt_0_toB_edge__edge0 = edge_cur_AtoAorB_alt_0_toB_edge__edge0.outNext) != edge_head_AtoAorB_alt_0_toB_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_Complex : LGSPAction
    {
        public Action_Complex() {
            rulePattern = Rule_Complex.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0);
        }

        public override string Name { get { return "Complex"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_Complex instance = new Action_Complex();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup Complex_edge__edge0 
            int edge_type_id_Complex_edge__edge0 = 1;
            for(LGSPEdge edge_head_Complex_edge__edge0 = graph.edgesByTypeHeads[edge_type_id_Complex_edge__edge0], edge_cur_Complex_edge__edge0 = edge_head_Complex_edge__edge0.typeNext; edge_cur_Complex_edge__edge0 != edge_head_Complex_edge__edge0; edge_cur_Complex_edge__edge0 = edge_cur_Complex_edge__edge0.typeNext)
            {
                bool edge_cur_Complex_edge__edge0_prevIsMatched = edge_cur_Complex_edge__edge0.isMatched;
                edge_cur_Complex_edge__edge0.isMatched = true;
                // Implicit source Complex_node_a from Complex_edge__edge0 
                LGSPNode node_cur_Complex_node_a = edge_cur_Complex_edge__edge0.source;
                if(!NodeType_A.isMyType[node_cur_Complex_node_a.type.TypeID]) {
                    edge_cur_Complex_edge__edge0.isMatched = edge_cur_Complex_edge__edge0_prevIsMatched;
                    continue;
                }
                // Implicit target Complex_node_b from Complex_edge__edge0 
                LGSPNode node_cur_Complex_node_b = edge_cur_Complex_edge__edge0.target;
                if(!NodeType_B.isMyType[node_cur_Complex_node_b.type.TypeID]) {
                    edge_cur_Complex_edge__edge0.isMatched = edge_cur_Complex_edge__edge0_prevIsMatched;
                    continue;
                }
                // Extend outgoing Complex_edge__edge1 from Complex_node_b 
                LGSPEdge edge_head_Complex_edge__edge1 = node_cur_Complex_node_b.outhead;
                if(edge_head_Complex_edge__edge1 != null)
                {
                    LGSPEdge edge_cur_Complex_edge__edge1 = edge_head_Complex_edge__edge1;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_edge__edge1.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_Complex_edge__edge1.target != node_cur_Complex_node_a) {
                            continue;
                        }
                        if(edge_cur_Complex_edge__edge1.isMatched
                            && edge_cur_Complex_edge__edge1==edge_cur_Complex_edge__edge0
                            )
                        {
                            continue;
                        }
                        // Push alternative matching task for Complex_alt_0
                        AlternativeAction_Complex_alt_0 taskFor_alt_0 = new AlternativeAction_Complex_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_Complex.Complex_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.Complex_node_a = node_cur_Complex_node_a;
                        taskFor_alt_0.Complex_node_b = node_cur_Complex_node_b;
                        openTasks.Push(taskFor_alt_0);
                        node_cur_Complex_node_a.isMatchedByEnclosingPattern = true;
                        node_cur_Complex_node_b.isMatchedByEnclosingPattern = true;
                        edge_cur_Complex_edge__edge0.isMatchedByEnclosingPattern = true;
                        edge_cur_Complex_edge__edge1.isMatchedByEnclosingPattern = true;
                        // Match subpatterns
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_Complex.Complex_NodeNums.@a] = node_cur_Complex_node_a;
                                match.Nodes[(int)Rule_Complex.Complex_NodeNums.@b] = node_cur_Complex_node_b;
                                match.Edges[(int)Rule_Complex.Complex_EdgeNums.@_edge0] = edge_cur_Complex_edge__edge0;
                                match.Edges[(int)Rule_Complex.Complex_EdgeNums.@_edge1] = edge_cur_Complex_edge__edge1;
                                matches.matchesList.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                            {
                                edge_cur_Complex_edge__edge1.isMatchedByEnclosingPattern = false;
                                edge_cur_Complex_edge__edge0.isMatchedByEnclosingPattern = false;
                                node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                edge_cur_Complex_edge__edge0.isMatched = edge_cur_Complex_edge__edge0_prevIsMatched;
                                return matches;
                            }
                            edge_cur_Complex_edge__edge1.isMatchedByEnclosingPattern = false;
                            edge_cur_Complex_edge__edge0.isMatchedByEnclosingPattern = false;
                            node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                            node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                            continue;
                        }
                        node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                        node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                        edge_cur_Complex_edge__edge0.isMatchedByEnclosingPattern = false;
                        edge_cur_Complex_edge__edge1.isMatchedByEnclosingPattern = false;
                    }
                    while( (edge_cur_Complex_edge__edge1 = edge_cur_Complex_edge__edge1.outNext) != edge_head_Complex_edge__edge1 );
                }
                edge_cur_Complex_edge__edge0.isMatched = edge_cur_Complex_edge__edge0_prevIsMatched;
            }
            return matches;
        }
    }

    public class AlternativeAction_Complex_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_Complex_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode Complex_node_a;
        public LGSPNode Complex_node_b;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Complex_alt_0_ExtendAv 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendAv];
                // SubPreset Complex_node_a 
                LGSPNode node_cur_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                LGSPNode node_cur_Complex_node_b = Complex_node_b;
                // Extend outgoing Complex_alt_0_ExtendAv_edge__edge0 from Complex_node_a 
                LGSPEdge edge_head_Complex_alt_0_ExtendAv_edge__edge0 = node_cur_Complex_node_a.outhead;
                if(edge_head_Complex_alt_0_ExtendAv_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_Complex_alt_0_ExtendAv_edge__edge0 = edge_head_Complex_alt_0_ExtendAv_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        bool edge_cur_Complex_alt_0_ExtendAv_edge__edge0_prevIsMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched;
                        edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched = true;
                        // Implicit target Complex_alt_0_ExtendAv_node_b2 from Complex_alt_0_ExtendAv_edge__edge0 
                        LGSPNode node_cur_Complex_alt_0_ExtendAv_node_b2 = edge_cur_Complex_alt_0_ExtendAv_edge__edge0.target;
                        if(!NodeType_B.isMyType[node_cur_Complex_alt_0_ExtendAv_node_b2.type.TypeID]) {
                            edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge0_prevIsMatched;
                            continue;
                        }
                        if(node_cur_Complex_alt_0_ExtendAv_node_b2.isMatched
                            && node_cur_Complex_alt_0_ExtendAv_node_b2==node_cur_Complex_node_b
                            )
                        {
                            edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge0_prevIsMatched;
                            continue;
                        }
                        if(node_cur_Complex_alt_0_ExtendAv_node_b2.isMatchedByEnclosingPattern)
                        {
                            edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge0_prevIsMatched;
                            continue;
                        }
                        // Extend outgoing Complex_alt_0_ExtendAv_edge__edge2 from Complex_node_b 
                        LGSPEdge edge_head_Complex_alt_0_ExtendAv_edge__edge2 = node_cur_Complex_node_b.outhead;
                        if(edge_head_Complex_alt_0_ExtendAv_edge__edge2 != null)
                        {
                            LGSPEdge edge_cur_Complex_alt_0_ExtendAv_edge__edge2 = edge_head_Complex_alt_0_ExtendAv_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched
                                    && edge_cur_Complex_alt_0_ExtendAv_edge__edge2==edge_cur_Complex_alt_0_ExtendAv_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if(edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatchedByEnclosingPattern)
                                {
                                    continue;
                                }
                                bool edge_cur_Complex_alt_0_ExtendAv_edge__edge2_prevIsMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched;
                                edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched = true;
                                // Implicit target Complex_alt_0_ExtendAv_node__node0 from Complex_alt_0_ExtendAv_edge__edge2 
                                LGSPNode node_cur_Complex_alt_0_ExtendAv_node__node0 = edge_cur_Complex_alt_0_ExtendAv_edge__edge2.target;
                                if(!NodeType_C.isMyType[node_cur_Complex_alt_0_ExtendAv_node__node0.type.TypeID]) {
                                    edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge2_prevIsMatched;
                                    continue;
                                }
                                if(node_cur_Complex_alt_0_ExtendAv_node__node0.isMatchedByEnclosingPattern)
                                {
                                    edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge2_prevIsMatched;
                                    continue;
                                }
                                bool node_cur_Complex_alt_0_ExtendAv_node__node0_prevIsMatched = node_cur_Complex_alt_0_ExtendAv_node__node0.isMatched;
                                node_cur_Complex_alt_0_ExtendAv_node__node0.isMatched = true;
                                // Extend outgoing Complex_alt_0_ExtendAv_edge__edge1 from Complex_alt_0_ExtendAv_node_b2 
                                LGSPEdge edge_head_Complex_alt_0_ExtendAv_edge__edge1 = node_cur_Complex_alt_0_ExtendAv_node_b2.outhead;
                                if(edge_head_Complex_alt_0_ExtendAv_edge__edge1 != null)
                                {
                                    LGSPEdge edge_cur_Complex_alt_0_ExtendAv_edge__edge1 = edge_head_Complex_alt_0_ExtendAv_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendAv_edge__edge1.target != node_cur_Complex_node_a) {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatched
                                            && (edge_cur_Complex_alt_0_ExtendAv_edge__edge1==edge_cur_Complex_alt_0_ExtendAv_edge__edge0
                                                || edge_cur_Complex_alt_0_ExtendAv_edge__edge1==edge_cur_Complex_alt_0_ExtendAv_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatchedByEnclosingPattern)
                                        {
                                            continue;
                                        }
                                        bool edge_cur_Complex_alt_0_ExtendAv_edge__edge1_prevIsMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatched;
                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatched = true;
                                        // Extend outgoing Complex_alt_0_ExtendAv_edge__edge3 from Complex_alt_0_ExtendAv_node__node0 
                                        LGSPEdge edge_head_Complex_alt_0_ExtendAv_edge__edge3 = node_cur_Complex_alt_0_ExtendAv_node__node0.outhead;
                                        if(edge_head_Complex_alt_0_ExtendAv_edge__edge3 != null)
                                        {
                                            LGSPEdge edge_cur_Complex_alt_0_ExtendAv_edge__edge3 = edge_head_Complex_alt_0_ExtendAv_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if(edge_cur_Complex_alt_0_ExtendAv_edge__edge3.isMatched
                                                    && (edge_cur_Complex_alt_0_ExtendAv_edge__edge3==edge_cur_Complex_alt_0_ExtendAv_edge__edge0
                                                        || edge_cur_Complex_alt_0_ExtendAv_edge__edge3==edge_cur_Complex_alt_0_ExtendAv_edge__edge2
                                                        || edge_cur_Complex_alt_0_ExtendAv_edge__edge3==edge_cur_Complex_alt_0_ExtendAv_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if(edge_cur_Complex_alt_0_ExtendAv_edge__edge3.isMatchedByEnclosingPattern)
                                                {
                                                    continue;
                                                }
                                                // Implicit target Complex_alt_0_ExtendAv_node__node1 from Complex_alt_0_ExtendAv_edge__edge3 
                                                LGSPNode node_cur_Complex_alt_0_ExtendAv_node__node1 = edge_cur_Complex_alt_0_ExtendAv_edge__edge3.target;
                                                if(!NodeType_C.isMyType[node_cur_Complex_alt_0_ExtendAv_node__node1.type.TypeID]) {
                                                    continue;
                                                }
                                                if(node_cur_Complex_alt_0_ExtendAv_node__node1.isMatched
                                                    && node_cur_Complex_alt_0_ExtendAv_node__node1==node_cur_Complex_alt_0_ExtendAv_node__node0
                                                    )
                                                {
                                                    continue;
                                                }
                                                if(node_cur_Complex_alt_0_ExtendAv_node__node1.isMatchedByEnclosingPattern)
                                                {
                                                    continue;
                                                }
                                                // Check whether there are subpattern matching tasks left to execute
                                                if(openTasks.Count==0)
                                                {
                                                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                                    LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0]);
                                                    match.patternGraph = patternGraph;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@a] = node_cur_Complex_node_a;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b2] = node_cur_Complex_alt_0_ExtendAv_node_b2;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b] = node_cur_Complex_node_b;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node0] = node_cur_Complex_alt_0_ExtendAv_node__node0;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node1] = node_cur_Complex_alt_0_ExtendAv_node__node1;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge0] = edge_cur_Complex_alt_0_ExtendAv_edge__edge0;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge1] = edge_cur_Complex_alt_0_ExtendAv_edge__edge1;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge2] = edge_cur_Complex_alt_0_ExtendAv_edge__edge2;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge3] = edge_cur_Complex_alt_0_ExtendAv_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge1_prevIsMatched;
                                                        node_cur_Complex_alt_0_ExtendAv_node__node0.isMatched = node_cur_Complex_alt_0_ExtendAv_node__node0_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge2_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge0_prevIsMatched;
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                node_cur_Complex_node_a.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_alt_0_ExtendAv_node_b2.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_node_b.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_alt_0_ExtendAv_node__node0.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_alt_0_ExtendAv_node__node1.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge3.isMatchedByEnclosingPattern = true;
                                                // Match subpatterns
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns were found, extend the partial matches by our local match object
                                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0]);
                                                        match.patternGraph = patternGraph;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@a] = node_cur_Complex_node_a;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b2] = node_cur_Complex_alt_0_ExtendAv_node_b2;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b] = node_cur_Complex_node_b;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node0] = node_cur_Complex_alt_0_ExtendAv_node__node0;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node1] = node_cur_Complex_alt_0_ExtendAv_node__node1;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge0] = edge_cur_Complex_alt_0_ExtendAv_edge__edge0;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge1] = edge_cur_Complex_alt_0_ExtendAv_edge__edge1;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge2] = edge_cur_Complex_alt_0_ExtendAv_edge__edge2;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge3] = edge_cur_Complex_alt_0_ExtendAv_edge__edge3;
                                                        currentFoundPartialMatch.Push(match);
                                                    }
                                                    if(matchesList==foundPartialMatches) {
                                                        matchesList = new List<Stack<LGSPMatch>>();
                                                    } else {
                                                        foreach(Stack<LGSPMatch> match in matchesList)
                                                        {
                                                            foundPartialMatches.Add(match);
                                                        }
                                                        matchesList.Clear();
                                                    }
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge3.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendAv_node__node1.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendAv_node__node0.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendAv_node_b2.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge1_prevIsMatched;
                                                        node_cur_Complex_alt_0_ExtendAv_node__node0.isMatched = node_cur_Complex_alt_0_ExtendAv_node__node0_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge2_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge0_prevIsMatched;
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    edge_cur_Complex_alt_0_ExtendAv_edge__edge3.isMatchedByEnclosingPattern = false;
                                                    edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatchedByEnclosingPattern = false;
                                                    edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatchedByEnclosingPattern = false;
                                                    edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_alt_0_ExtendAv_node__node1.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_alt_0_ExtendAv_node__node0.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_alt_0_ExtendAv_node_b2.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                    continue;
                                                }
                                                node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_alt_0_ExtendAv_node_b2.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_alt_0_ExtendAv_node__node0.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_alt_0_ExtendAv_node__node1.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendAv_edge__edge3.isMatchedByEnclosingPattern = false;
                                            }
                                            while( (edge_cur_Complex_alt_0_ExtendAv_edge__edge3 = edge_cur_Complex_alt_0_ExtendAv_edge__edge3.outNext) != edge_head_Complex_alt_0_ExtendAv_edge__edge3 );
                                        }
                                        edge_cur_Complex_alt_0_ExtendAv_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge1_prevIsMatched;
                                    }
                                    while( (edge_cur_Complex_alt_0_ExtendAv_edge__edge1 = edge_cur_Complex_alt_0_ExtendAv_edge__edge1.outNext) != edge_head_Complex_alt_0_ExtendAv_edge__edge1 );
                                }
                                node_cur_Complex_alt_0_ExtendAv_node__node0.isMatched = node_cur_Complex_alt_0_ExtendAv_node__node0_prevIsMatched;
                                edge_cur_Complex_alt_0_ExtendAv_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge2_prevIsMatched;
                            }
                            while( (edge_cur_Complex_alt_0_ExtendAv_edge__edge2 = edge_cur_Complex_alt_0_ExtendAv_edge__edge2.outNext) != edge_head_Complex_alt_0_ExtendAv_edge__edge2 );
                        }
                        edge_cur_Complex_alt_0_ExtendAv_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv_edge__edge0_prevIsMatched;
                    }
                    while( (edge_cur_Complex_alt_0_ExtendAv_edge__edge0 = edge_cur_Complex_alt_0_ExtendAv_edge__edge0.outNext) != edge_head_Complex_alt_0_ExtendAv_edge__edge0 );
                }
            } while(false);
            // Alternative case Complex_alt_0_ExtendAv2 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendAv2];
                // SubPreset Complex_node_a 
                LGSPNode node_cur_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                LGSPNode node_cur_Complex_node_b = Complex_node_b;
                // Extend outgoing Complex_alt_0_ExtendAv2_edge__edge0 from Complex_node_a 
                LGSPEdge edge_head_Complex_alt_0_ExtendAv2_edge__edge0 = node_cur_Complex_node_a.outhead;
                if(edge_head_Complex_alt_0_ExtendAv2_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_Complex_alt_0_ExtendAv2_edge__edge0 = edge_head_Complex_alt_0_ExtendAv2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        bool edge_cur_Complex_alt_0_ExtendAv2_edge__edge0_prevIsMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched;
                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched = true;
                        // Implicit target Complex_alt_0_ExtendAv2_node_b2 from Complex_alt_0_ExtendAv2_edge__edge0 
                        LGSPNode node_cur_Complex_alt_0_ExtendAv2_node_b2 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.target;
                        if(!NodeType_B.isMyType[node_cur_Complex_alt_0_ExtendAv2_node_b2.type.TypeID]) {
                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0_prevIsMatched;
                            continue;
                        }
                        if(node_cur_Complex_alt_0_ExtendAv2_node_b2.isMatched
                            && node_cur_Complex_alt_0_ExtendAv2_node_b2==node_cur_Complex_node_b
                            )
                        {
                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0_prevIsMatched;
                            continue;
                        }
                        if(node_cur_Complex_alt_0_ExtendAv2_node_b2.isMatchedByEnclosingPattern)
                        {
                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0_prevIsMatched;
                            continue;
                        }
                        // Extend outgoing Complex_alt_0_ExtendAv2_edge__edge2 from Complex_node_b 
                        LGSPEdge edge_head_Complex_alt_0_ExtendAv2_edge__edge2 = node_cur_Complex_node_b.outhead;
                        if(edge_head_Complex_alt_0_ExtendAv2_edge__edge2 != null)
                        {
                            LGSPEdge edge_cur_Complex_alt_0_ExtendAv2_edge__edge2 = edge_head_Complex_alt_0_ExtendAv2_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched
                                    && edge_cur_Complex_alt_0_ExtendAv2_edge__edge2==edge_cur_Complex_alt_0_ExtendAv2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatchedByEnclosingPattern)
                                {
                                    continue;
                                }
                                bool edge_cur_Complex_alt_0_ExtendAv2_edge__edge2_prevIsMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched;
                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched = true;
                                // Implicit target Complex_alt_0_ExtendAv2_node__node0 from Complex_alt_0_ExtendAv2_edge__edge2 
                                LGSPNode node_cur_Complex_alt_0_ExtendAv2_node__node0 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.target;
                                if(!NodeType_C.isMyType[node_cur_Complex_alt_0_ExtendAv2_node__node0.type.TypeID]) {
                                    edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2_prevIsMatched;
                                    continue;
                                }
                                if(node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatchedByEnclosingPattern)
                                {
                                    edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2_prevIsMatched;
                                    continue;
                                }
                                bool node_cur_Complex_alt_0_ExtendAv2_node__node0_prevIsMatched = node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatched;
                                node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatched = true;
                                // Extend outgoing Complex_alt_0_ExtendAv2_edge__edge1 from Complex_alt_0_ExtendAv2_node_b2 
                                LGSPEdge edge_head_Complex_alt_0_ExtendAv2_edge__edge1 = node_cur_Complex_alt_0_ExtendAv2_node_b2.outhead;
                                if(edge_head_Complex_alt_0_ExtendAv2_edge__edge1 != null)
                                {
                                    LGSPEdge edge_cur_Complex_alt_0_ExtendAv2_edge__edge1 = edge_head_Complex_alt_0_ExtendAv2_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.target != node_cur_Complex_node_a) {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatched
                                            && (edge_cur_Complex_alt_0_ExtendAv2_edge__edge1==edge_cur_Complex_alt_0_ExtendAv2_edge__edge0
                                                || edge_cur_Complex_alt_0_ExtendAv2_edge__edge1==edge_cur_Complex_alt_0_ExtendAv2_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatchedByEnclosingPattern)
                                        {
                                            continue;
                                        }
                                        bool edge_cur_Complex_alt_0_ExtendAv2_edge__edge1_prevIsMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatched;
                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatched = true;
                                        // Extend outgoing Complex_alt_0_ExtendAv2_edge__edge3 from Complex_alt_0_ExtendAv2_node__node0 
                                        LGSPEdge edge_head_Complex_alt_0_ExtendAv2_edge__edge3 = node_cur_Complex_alt_0_ExtendAv2_node__node0.outhead;
                                        if(edge_head_Complex_alt_0_ExtendAv2_edge__edge3 != null)
                                        {
                                            LGSPEdge edge_cur_Complex_alt_0_ExtendAv2_edge__edge3 = edge_head_Complex_alt_0_ExtendAv2_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched
                                                    && (edge_cur_Complex_alt_0_ExtendAv2_edge__edge3==edge_cur_Complex_alt_0_ExtendAv2_edge__edge0
                                                        || edge_cur_Complex_alt_0_ExtendAv2_edge__edge3==edge_cur_Complex_alt_0_ExtendAv2_edge__edge2
                                                        || edge_cur_Complex_alt_0_ExtendAv2_edge__edge3==edge_cur_Complex_alt_0_ExtendAv2_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatchedByEnclosingPattern)
                                                {
                                                    continue;
                                                }
                                                bool edge_cur_Complex_alt_0_ExtendAv2_edge__edge3_prevIsMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched;
                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched = true;
                                                // Implicit target Complex_alt_0_ExtendAv2_node__node1 from Complex_alt_0_ExtendAv2_edge__edge3 
                                                LGSPNode node_cur_Complex_alt_0_ExtendAv2_node__node1 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.target;
                                                if(!NodeType_C.isMyType[node_cur_Complex_alt_0_ExtendAv2_node__node1.type.TypeID]) {
                                                    edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3_prevIsMatched;
                                                    continue;
                                                }
                                                if(node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatched
                                                    && node_cur_Complex_alt_0_ExtendAv2_node__node1==node_cur_Complex_alt_0_ExtendAv2_node__node0
                                                    )
                                                {
                                                    edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3_prevIsMatched;
                                                    continue;
                                                }
                                                if(node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatchedByEnclosingPattern)
                                                {
                                                    edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3_prevIsMatched;
                                                    continue;
                                                }
                                                bool node_cur_Complex_alt_0_ExtendAv2_node__node1_prevIsMatched = node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatched;
                                                node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatched = true;
                                                // Extend outgoing Complex_alt_0_ExtendAv2_edge__edge4 from Complex_alt_0_ExtendAv2_node__node1 
                                                LGSPEdge edge_head_Complex_alt_0_ExtendAv2_edge__edge4 = node_cur_Complex_alt_0_ExtendAv2_node__node1.outhead;
                                                if(edge_head_Complex_alt_0_ExtendAv2_edge__edge4 != null)
                                                {
                                                    LGSPEdge edge_cur_Complex_alt_0_ExtendAv2_edge__edge4 = edge_head_Complex_alt_0_ExtendAv2_edge__edge4;
                                                    do
                                                    {
                                                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.type.TypeID]) {
                                                            continue;
                                                        }
                                                        if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.isMatched
                                                            && (edge_cur_Complex_alt_0_ExtendAv2_edge__edge4==edge_cur_Complex_alt_0_ExtendAv2_edge__edge0
                                                                || edge_cur_Complex_alt_0_ExtendAv2_edge__edge4==edge_cur_Complex_alt_0_ExtendAv2_edge__edge2
                                                                || edge_cur_Complex_alt_0_ExtendAv2_edge__edge4==edge_cur_Complex_alt_0_ExtendAv2_edge__edge1
                                                                || edge_cur_Complex_alt_0_ExtendAv2_edge__edge4==edge_cur_Complex_alt_0_ExtendAv2_edge__edge3
                                                                )
                                                            )
                                                        {
                                                            continue;
                                                        }
                                                        if(edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.isMatchedByEnclosingPattern)
                                                        {
                                                            continue;
                                                        }
                                                        // Implicit target Complex_alt_0_ExtendAv2_node__node2 from Complex_alt_0_ExtendAv2_edge__edge4 
                                                        LGSPNode node_cur_Complex_alt_0_ExtendAv2_node__node2 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.target;
                                                        if(!NodeType_C.isMyType[node_cur_Complex_alt_0_ExtendAv2_node__node2.type.TypeID]) {
                                                            continue;
                                                        }
                                                        if(node_cur_Complex_alt_0_ExtendAv2_node__node2.isMatched
                                                            && (node_cur_Complex_alt_0_ExtendAv2_node__node2==node_cur_Complex_alt_0_ExtendAv2_node__node0
                                                                || node_cur_Complex_alt_0_ExtendAv2_node__node2==node_cur_Complex_alt_0_ExtendAv2_node__node1
                                                                )
                                                            )
                                                        {
                                                            continue;
                                                        }
                                                        if(node_cur_Complex_alt_0_ExtendAv2_node__node2.isMatchedByEnclosingPattern)
                                                        {
                                                            continue;
                                                        }
                                                        // Check whether there are subpattern matching tasks left to execute
                                                        if(openTasks.Count==0)
                                                        {
                                                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                                            foundPartialMatches.Add(currentFoundPartialMatch);
                                                            LGSPMatch match = new LGSPMatch(new LGSPNode[6], new LGSPEdge[5], new LGSPMatch[0]);
                                                            match.patternGraph = patternGraph;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@a] = node_cur_Complex_node_a;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b2] = node_cur_Complex_alt_0_ExtendAv2_node_b2;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b] = node_cur_Complex_node_b;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node0] = node_cur_Complex_alt_0_ExtendAv2_node__node0;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node1] = node_cur_Complex_alt_0_ExtendAv2_node__node1;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node2] = node_cur_Complex_alt_0_ExtendAv2_node__node2;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge0] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge1] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge1;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge2] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge3] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge4] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge4;
                                                            currentFoundPartialMatch.Push(match);
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatched = node_cur_Complex_alt_0_ExtendAv2_node__node1_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge1_prevIsMatched;
                                                                node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatched = node_cur_Complex_alt_0_ExtendAv2_node__node0_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0_prevIsMatched;
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            continue;
                                                        }
                                                        node_cur_Complex_node_a.isMatchedByEnclosingPattern = true;
                                                        node_cur_Complex_alt_0_ExtendAv2_node_b2.isMatchedByEnclosingPattern = true;
                                                        node_cur_Complex_node_b.isMatchedByEnclosingPattern = true;
                                                        node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatchedByEnclosingPattern = true;
                                                        node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatchedByEnclosingPattern = true;
                                                        node_cur_Complex_alt_0_ExtendAv2_node__node2.isMatchedByEnclosingPattern = true;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatchedByEnclosingPattern = true;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatchedByEnclosingPattern = true;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatchedByEnclosingPattern = true;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatchedByEnclosingPattern = true;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.isMatchedByEnclosingPattern = true;
                                                        // Match subpatterns
                                                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                                                        // Check whether subpatterns were found 
                                                        if(matchesList.Count>0) {
                                                            // subpatterns were found, extend the partial matches by our local match object
                                                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                            {
                                                                LGSPMatch match = new LGSPMatch(new LGSPNode[6], new LGSPEdge[5], new LGSPMatch[0]);
                                                                match.patternGraph = patternGraph;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@a] = node_cur_Complex_node_a;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b2] = node_cur_Complex_alt_0_ExtendAv2_node_b2;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b] = node_cur_Complex_node_b;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node0] = node_cur_Complex_alt_0_ExtendAv2_node__node0;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node1] = node_cur_Complex_alt_0_ExtendAv2_node__node1;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node2] = node_cur_Complex_alt_0_ExtendAv2_node__node2;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge0] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge1] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge2] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge3] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge4] = edge_cur_Complex_alt_0_ExtendAv2_edge__edge4;
                                                                currentFoundPartialMatch.Push(match);
                                                            }
                                                            if(matchesList==foundPartialMatches) {
                                                                matchesList = new List<Stack<LGSPMatch>>();
                                                            } else {
                                                                foreach(Stack<LGSPMatch> match in matchesList)
                                                                {
                                                                    foundPartialMatches.Add(match);
                                                                }
                                                                matchesList.Clear();
                                                            }
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.isMatchedByEnclosingPattern = false;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatchedByEnclosingPattern = false;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatchedByEnclosingPattern = false;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatchedByEnclosingPattern = false;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatchedByEnclosingPattern = false;
                                                                node_cur_Complex_alt_0_ExtendAv2_node__node2.isMatchedByEnclosingPattern = false;
                                                                node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatchedByEnclosingPattern = false;
                                                                node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatchedByEnclosingPattern = false;
                                                                node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                                node_cur_Complex_alt_0_ExtendAv2_node_b2.isMatchedByEnclosingPattern = false;
                                                                node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                                node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatched = node_cur_Complex_alt_0_ExtendAv2_node__node1_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge1_prevIsMatched;
                                                                node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatched = node_cur_Complex_alt_0_ExtendAv2_node__node0_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2_prevIsMatched;
                                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0_prevIsMatched;
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.isMatchedByEnclosingPattern = false;
                                                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatchedByEnclosingPattern = false;
                                                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatchedByEnclosingPattern = false;
                                                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatchedByEnclosingPattern = false;
                                                            edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatchedByEnclosingPattern = false;
                                                            node_cur_Complex_alt_0_ExtendAv2_node__node2.isMatchedByEnclosingPattern = false;
                                                            node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatchedByEnclosingPattern = false;
                                                            node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatchedByEnclosingPattern = false;
                                                            node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                            node_cur_Complex_alt_0_ExtendAv2_node_b2.isMatchedByEnclosingPattern = false;
                                                            node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                            continue;
                                                        }
                                                        node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendAv2_node_b2.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendAv2_node__node2.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.isMatchedByEnclosingPattern = false;
                                                    }
                                                    while( (edge_cur_Complex_alt_0_ExtendAv2_edge__edge4 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge4.outNext) != edge_head_Complex_alt_0_ExtendAv2_edge__edge4 );
                                                }
                                                node_cur_Complex_alt_0_ExtendAv2_node__node1.isMatched = node_cur_Complex_alt_0_ExtendAv2_node__node1_prevIsMatched;
                                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3_prevIsMatched;
                                            }
                                            while( (edge_cur_Complex_alt_0_ExtendAv2_edge__edge3 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge3.outNext) != edge_head_Complex_alt_0_ExtendAv2_edge__edge3 );
                                        }
                                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge1_prevIsMatched;
                                    }
                                    while( (edge_cur_Complex_alt_0_ExtendAv2_edge__edge1 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge1.outNext) != edge_head_Complex_alt_0_ExtendAv2_edge__edge1 );
                                }
                                node_cur_Complex_alt_0_ExtendAv2_node__node0.isMatched = node_cur_Complex_alt_0_ExtendAv2_node__node0_prevIsMatched;
                                edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2_prevIsMatched;
                            }
                            while( (edge_cur_Complex_alt_0_ExtendAv2_edge__edge2 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge2.outNext) != edge_head_Complex_alt_0_ExtendAv2_edge__edge2 );
                        }
                        edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0_prevIsMatched;
                    }
                    while( (edge_cur_Complex_alt_0_ExtendAv2_edge__edge0 = edge_cur_Complex_alt_0_ExtendAv2_edge__edge0.outNext) != edge_head_Complex_alt_0_ExtendAv2_edge__edge0 );
                }
            } while(false);
            // Alternative case Complex_alt_0_ExtendNA2 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendNA2];
                // SubPreset Complex_node_a 
                LGSPNode node_cur_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                LGSPNode node_cur_Complex_node_b = Complex_node_b;
                // Extend outgoing Complex_alt_0_ExtendNA2_edge__edge0 from Complex_node_a 
                LGSPEdge edge_head_Complex_alt_0_ExtendNA2_edge__edge0 = node_cur_Complex_node_a.outhead;
                if(edge_head_Complex_alt_0_ExtendNA2_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_Complex_alt_0_ExtendNA2_edge__edge0 = edge_head_Complex_alt_0_ExtendNA2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        bool edge_cur_Complex_alt_0_ExtendNA2_edge__edge0_prevIsMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatched;
                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatched = true;
                        // Implicit target Complex_alt_0_ExtendNA2_node__node0 from Complex_alt_0_ExtendNA2_edge__edge0 
                        LGSPNode node_cur_Complex_alt_0_ExtendNA2_node__node0 = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.target;
                        if(!NodeType_C.isMyType[node_cur_Complex_alt_0_ExtendNA2_node__node0.type.TypeID]) {
                            edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0_prevIsMatched;
                            continue;
                        }
                        if(node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatchedByEnclosingPattern)
                        {
                            edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0_prevIsMatched;
                            continue;
                        }
                        bool node_cur_Complex_alt_0_ExtendNA2_node__node0_prevIsMatched = node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatched;
                        node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatched = true;
                        // Extend outgoing Complex_alt_0_ExtendNA2_edge__edge2 from Complex_node_b 
                        LGSPEdge edge_head_Complex_alt_0_ExtendNA2_edge__edge2 = node_cur_Complex_node_b.outhead;
                        if(edge_head_Complex_alt_0_ExtendNA2_edge__edge2 != null)
                        {
                            LGSPEdge edge_cur_Complex_alt_0_ExtendNA2_edge__edge2 = edge_head_Complex_alt_0_ExtendNA2_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched
                                    && edge_cur_Complex_alt_0_ExtendNA2_edge__edge2==edge_cur_Complex_alt_0_ExtendNA2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatchedByEnclosingPattern)
                                {
                                    continue;
                                }
                                bool edge_cur_Complex_alt_0_ExtendNA2_edge__edge2_prevIsMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched;
                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched = true;
                                // Implicit target Complex_alt_0_ExtendNA2_node_b2 from Complex_alt_0_ExtendNA2_edge__edge2 
                                LGSPNode node_cur_Complex_alt_0_ExtendNA2_node_b2 = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.target;
                                if(!NodeType_B.isMyType[node_cur_Complex_alt_0_ExtendNA2_node_b2.type.TypeID]) {
                                    edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2_prevIsMatched;
                                    continue;
                                }
                                if(node_cur_Complex_alt_0_ExtendNA2_node_b2.isMatched
                                    && node_cur_Complex_alt_0_ExtendNA2_node_b2==node_cur_Complex_node_b
                                    )
                                {
                                    edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2_prevIsMatched;
                                    continue;
                                }
                                if(node_cur_Complex_alt_0_ExtendNA2_node_b2.isMatchedByEnclosingPattern)
                                {
                                    edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2_prevIsMatched;
                                    continue;
                                }
                                // Extend outgoing Complex_alt_0_ExtendNA2_edge__edge1 from Complex_alt_0_ExtendNA2_node__node0 
                                LGSPEdge edge_head_Complex_alt_0_ExtendNA2_edge__edge1 = node_cur_Complex_alt_0_ExtendNA2_node__node0.outhead;
                                if(edge_head_Complex_alt_0_ExtendNA2_edge__edge1 != null)
                                {
                                    LGSPEdge edge_cur_Complex_alt_0_ExtendNA2_edge__edge1 = edge_head_Complex_alt_0_ExtendNA2_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched
                                            && (edge_cur_Complex_alt_0_ExtendNA2_edge__edge1==edge_cur_Complex_alt_0_ExtendNA2_edge__edge0
                                                || edge_cur_Complex_alt_0_ExtendNA2_edge__edge1==edge_cur_Complex_alt_0_ExtendNA2_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatchedByEnclosingPattern)
                                        {
                                            continue;
                                        }
                                        bool edge_cur_Complex_alt_0_ExtendNA2_edge__edge1_prevIsMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched;
                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched = true;
                                        // Implicit target Complex_alt_0_ExtendNA2_node__node1 from Complex_alt_0_ExtendNA2_edge__edge1 
                                        LGSPNode node_cur_Complex_alt_0_ExtendNA2_node__node1 = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.target;
                                        if(!NodeType_C.isMyType[node_cur_Complex_alt_0_ExtendNA2_node__node1.type.TypeID]) {
                                            edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1_prevIsMatched;
                                            continue;
                                        }
                                        if(node_cur_Complex_alt_0_ExtendNA2_node__node1.isMatched
                                            && node_cur_Complex_alt_0_ExtendNA2_node__node1==node_cur_Complex_alt_0_ExtendNA2_node__node0
                                            )
                                        {
                                            edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1_prevIsMatched;
                                            continue;
                                        }
                                        if(node_cur_Complex_alt_0_ExtendNA2_node__node1.isMatchedByEnclosingPattern)
                                        {
                                            edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1_prevIsMatched;
                                            continue;
                                        }
                                        // Extend outgoing Complex_alt_0_ExtendNA2_edge__edge3 from Complex_alt_0_ExtendNA2_node_b2 
                                        LGSPEdge edge_head_Complex_alt_0_ExtendNA2_edge__edge3 = node_cur_Complex_alt_0_ExtendNA2_node_b2.outhead;
                                        if(edge_head_Complex_alt_0_ExtendNA2_edge__edge3 != null)
                                        {
                                            LGSPEdge edge_cur_Complex_alt_0_ExtendNA2_edge__edge3 = edge_head_Complex_alt_0_ExtendNA2_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.target != node_cur_Complex_node_b) {
                                                    continue;
                                                }
                                                if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.isMatched
                                                    && (edge_cur_Complex_alt_0_ExtendNA2_edge__edge3==edge_cur_Complex_alt_0_ExtendNA2_edge__edge0
                                                        || edge_cur_Complex_alt_0_ExtendNA2_edge__edge3==edge_cur_Complex_alt_0_ExtendNA2_edge__edge2
                                                        || edge_cur_Complex_alt_0_ExtendNA2_edge__edge3==edge_cur_Complex_alt_0_ExtendNA2_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if(edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.isMatchedByEnclosingPattern)
                                                {
                                                    continue;
                                                }
                                                // Check whether there are subpattern matching tasks left to execute
                                                if(openTasks.Count==0)
                                                {
                                                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                                    LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0]);
                                                    match.patternGraph = patternGraph;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@a] = node_cur_Complex_node_a;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node0] = node_cur_Complex_alt_0_ExtendNA2_node__node0;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node1] = node_cur_Complex_alt_0_ExtendNA2_node__node1;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b] = node_cur_Complex_node_b;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b2] = node_cur_Complex_alt_0_ExtendNA2_node_b2;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge0] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge1] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge2] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge3] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2_prevIsMatched;
                                                        node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatched = node_cur_Complex_alt_0_ExtendNA2_node__node0_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0_prevIsMatched;
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                node_cur_Complex_node_a.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_alt_0_ExtendNA2_node__node1.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_node_b.isMatchedByEnclosingPattern = true;
                                                node_cur_Complex_alt_0_ExtendNA2_node_b2.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatchedByEnclosingPattern = true;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.isMatchedByEnclosingPattern = true;
                                                // Match subpatterns
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns were found, extend the partial matches by our local match object
                                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0]);
                                                        match.patternGraph = patternGraph;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@a] = node_cur_Complex_node_a;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node0] = node_cur_Complex_alt_0_ExtendNA2_node__node0;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node1] = node_cur_Complex_alt_0_ExtendNA2_node__node1;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b] = node_cur_Complex_node_b;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b2] = node_cur_Complex_alt_0_ExtendNA2_node_b2;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge0] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge1] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge2] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge3] = edge_cur_Complex_alt_0_ExtendNA2_edge__edge3;
                                                        currentFoundPartialMatch.Push(match);
                                                    }
                                                    if(matchesList==foundPartialMatches) {
                                                        matchesList = new List<Stack<LGSPMatch>>();
                                                    } else {
                                                        foreach(Stack<LGSPMatch> match in matchesList)
                                                        {
                                                            foundPartialMatches.Add(match);
                                                        }
                                                        matchesList.Clear();
                                                    }
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendNA2_node_b2.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendNA2_node__node1.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatchedByEnclosingPattern = false;
                                                        node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2_prevIsMatched;
                                                        node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatched = node_cur_Complex_alt_0_ExtendNA2_node__node0_prevIsMatched;
                                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0_prevIsMatched;
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.isMatchedByEnclosingPattern = false;
                                                    edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatchedByEnclosingPattern = false;
                                                    edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatchedByEnclosingPattern = false;
                                                    edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_alt_0_ExtendNA2_node_b2.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_alt_0_ExtendNA2_node__node1.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatchedByEnclosingPattern = false;
                                                    node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                    continue;
                                                }
                                                node_cur_Complex_node_a.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_alt_0_ExtendNA2_node__node1.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_node_b.isMatchedByEnclosingPattern = false;
                                                node_cur_Complex_alt_0_ExtendNA2_node_b2.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatchedByEnclosingPattern = false;
                                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.isMatchedByEnclosingPattern = false;
                                            }
                                            while( (edge_cur_Complex_alt_0_ExtendNA2_edge__edge3 = edge_cur_Complex_alt_0_ExtendNA2_edge__edge3.outNext) != edge_head_Complex_alt_0_ExtendNA2_edge__edge3 );
                                        }
                                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1_prevIsMatched;
                                    }
                                    while( (edge_cur_Complex_alt_0_ExtendNA2_edge__edge1 = edge_cur_Complex_alt_0_ExtendNA2_edge__edge1.outNext) != edge_head_Complex_alt_0_ExtendNA2_edge__edge1 );
                                }
                                edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2_prevIsMatched;
                            }
                            while( (edge_cur_Complex_alt_0_ExtendNA2_edge__edge2 = edge_cur_Complex_alt_0_ExtendNA2_edge__edge2.outNext) != edge_head_Complex_alt_0_ExtendNA2_edge__edge2 );
                        }
                        node_cur_Complex_alt_0_ExtendNA2_node__node0.isMatched = node_cur_Complex_alt_0_ExtendNA2_node__node0_prevIsMatched;
                        edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.isMatched = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0_prevIsMatched;
                    }
                    while( (edge_cur_Complex_alt_0_ExtendNA2_edge__edge0 = edge_cur_Complex_alt_0_ExtendNA2_edge__edge0.outNext) != edge_head_Complex_alt_0_ExtendNA2_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_XtoAorB : LGSPAction
    {
        public Action_XtoAorB() {
            rulePattern = Rule_XtoAorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 1);
        }

        public override string Name { get { return "XtoAorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_XtoAorB instance = new Action_XtoAorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup XtoAorB_node_x 
            foreach(NodeType node_type_XtoAorB_node_x in NodeType_Node.typeVar.SubOrSameTypes)
            {
                int node_type_id_XtoAorB_node_x = node_type_XtoAorB_node_x.TypeID;
                for(LGSPNode node_head_XtoAorB_node_x = graph.nodesByTypeHeads[node_type_id_XtoAorB_node_x], node_cur_XtoAorB_node_x = node_head_XtoAorB_node_x.typeNext; node_cur_XtoAorB_node_x != node_head_XtoAorB_node_x; node_cur_XtoAorB_node_x = node_cur_XtoAorB_node_x.typeNext)
                {
                    // Push subpattern matching task for _subpattern0
                    PatternAction_toAorB taskFor__subpattern0 = new PatternAction_toAorB(graph, openTasks);
                    taskFor__subpattern0.toAorB_node_x = node_cur_XtoAorB_node_x;
                    openTasks.Push(taskFor__subpattern0);
                    node_cur_XtoAorB_node_x.isMatchedByEnclosingPattern = true;
                    // Match subpatterns
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                    //Pop subpattern matching task for _subpattern0
                    openTasks.Pop();
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                            match.patternGraph = rulePattern.patternGraph;
                            match.Nodes[(int)Rule_XtoAorB.XtoAorB_NodeNums.@x] = node_cur_XtoAorB_node_x;
                            match.EmbeddedGraphs[(int)Rule_XtoAorB.XtoAorB_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                            matches.matchesList.PositionWasFilledFixIt();
                        }
                        matchesList.Clear();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            node_cur_XtoAorB_node_x.isMatchedByEnclosingPattern = false;
                            return matches;
                        }
                        node_cur_XtoAorB_node_x.isMatchedByEnclosingPattern = false;
                        continue;
                    }
                    node_cur_XtoAorB_node_x.isMatchedByEnclosingPattern = false;
                }
            }
            return matches;
        }
    }

    public class Action_createA : LGSPAction
    {
        public Action_createA() {
            rulePattern = Rule_createA.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "createA"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createA instance = new Action_createA();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
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

    public class Action_createABA : LGSPAction
    {
        public Action_createABA() {
            rulePattern = Rule_createABA.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "createABA"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createABA instance = new Action_createABA();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
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

    public class Action_createAtoB : LGSPAction
    {
        public Action_createAtoB() {
            rulePattern = Rule_createAtoB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "createAtoB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createAtoB instance = new Action_createAtoB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
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

    public class Action_createB : LGSPAction
    {
        public Action_createB() {
            rulePattern = Rule_createB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "createB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createB instance = new Action_createB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
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

    public class Action_createC : LGSPAction
    {
        public Action_createC() {
            rulePattern = Rule_createC.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "createC"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createC instance = new Action_createC();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
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

    public class Action_createComplex : LGSPAction
    {
        public Action_createComplex() {
            rulePattern = Rule_createComplex.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "createComplex"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createComplex instance = new Action_createComplex();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
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

    public class Action_homm : LGSPAction
    {
        public Action_homm() {
            rulePattern = Rule_homm.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0);
        }

        public override string Name { get { return "homm"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_homm instance = new Action_homm();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup homm_edge__edge0 
            int edge_type_id_homm_edge__edge0 = 1;
            for(LGSPEdge edge_head_homm_edge__edge0 = graph.edgesByTypeHeads[edge_type_id_homm_edge__edge0], edge_cur_homm_edge__edge0 = edge_head_homm_edge__edge0.typeNext; edge_cur_homm_edge__edge0 != edge_head_homm_edge__edge0; edge_cur_homm_edge__edge0 = edge_cur_homm_edge__edge0.typeNext)
            {
                bool edge_cur_homm_edge__edge0_prevIsMatched = edge_cur_homm_edge__edge0.isMatched;
                edge_cur_homm_edge__edge0.isMatched = true;
                // Implicit source homm_node_a from homm_edge__edge0 
                LGSPNode node_cur_homm_node_a = edge_cur_homm_edge__edge0.source;
                if(!NodeType_A.isMyType[node_cur_homm_node_a.type.TypeID]) {
                    edge_cur_homm_edge__edge0.isMatched = edge_cur_homm_edge__edge0_prevIsMatched;
                    continue;
                }
                // Implicit target homm_node_b from homm_edge__edge0 
                LGSPNode node_cur_homm_node_b = edge_cur_homm_edge__edge0.target;
                if(!NodeType_B.isMyType[node_cur_homm_node_b.type.TypeID]) {
                    edge_cur_homm_edge__edge0.isMatched = edge_cur_homm_edge__edge0_prevIsMatched;
                    continue;
                }
                // Extend outgoing homm_edge__edge1 from homm_node_b 
                LGSPEdge edge_head_homm_edge__edge1 = node_cur_homm_node_b.outhead;
                if(edge_head_homm_edge__edge1 != null)
                {
                    LGSPEdge edge_cur_homm_edge__edge1 = edge_head_homm_edge__edge1;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_homm_edge__edge1.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_homm_edge__edge1.target != node_cur_homm_node_a) {
                            continue;
                        }
                        if(edge_cur_homm_edge__edge1.isMatched
                            && edge_cur_homm_edge__edge1==edge_cur_homm_edge__edge0
                            )
                        {
                            continue;
                        }
                        // Push alternative matching task for homm_alt_0
                        AlternativeAction_homm_alt_0 taskFor_alt_0 = new AlternativeAction_homm_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_homm.homm_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.homm_node_a = node_cur_homm_node_a;
                        openTasks.Push(taskFor_alt_0);
                        node_cur_homm_node_a.isMatchedByEnclosingPattern = true;
                        node_cur_homm_node_b.isMatchedByEnclosingPattern = true;
                        edge_cur_homm_edge__edge0.isMatchedByEnclosingPattern = true;
                        edge_cur_homm_edge__edge1.isMatchedByEnclosingPattern = true;
                        // Match subpatterns
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_homm.homm_NodeNums.@a] = node_cur_homm_node_a;
                                match.Nodes[(int)Rule_homm.homm_NodeNums.@b] = node_cur_homm_node_b;
                                match.Edges[(int)Rule_homm.homm_EdgeNums.@_edge0] = edge_cur_homm_edge__edge0;
                                match.Edges[(int)Rule_homm.homm_EdgeNums.@_edge1] = edge_cur_homm_edge__edge1;
                                matches.matchesList.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                            {
                                edge_cur_homm_edge__edge1.isMatchedByEnclosingPattern = false;
                                edge_cur_homm_edge__edge0.isMatchedByEnclosingPattern = false;
                                node_cur_homm_node_b.isMatchedByEnclosingPattern = false;
                                node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                                edge_cur_homm_edge__edge0.isMatched = edge_cur_homm_edge__edge0_prevIsMatched;
                                return matches;
                            }
                            edge_cur_homm_edge__edge1.isMatchedByEnclosingPattern = false;
                            edge_cur_homm_edge__edge0.isMatchedByEnclosingPattern = false;
                            node_cur_homm_node_b.isMatchedByEnclosingPattern = false;
                            node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                            continue;
                        }
                        node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                        node_cur_homm_node_b.isMatchedByEnclosingPattern = false;
                        edge_cur_homm_edge__edge0.isMatchedByEnclosingPattern = false;
                        edge_cur_homm_edge__edge1.isMatchedByEnclosingPattern = false;
                    }
                    while( (edge_cur_homm_edge__edge1 = edge_cur_homm_edge__edge1.outNext) != edge_head_homm_edge__edge1 );
                }
                edge_cur_homm_edge__edge0.isMatched = edge_cur_homm_edge__edge0_prevIsMatched;
            }
            return matches;
        }
    }

    public class AlternativeAction_homm_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_homm_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode homm_node_a;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case homm_alt_0_case1 
            do {
                patternGraph = patternGraphs[(int)Rule_homm.homm_alt_0_CaseNums.@case1];
                // SubPreset homm_node_a 
                LGSPNode node_cur_homm_node_a = homm_node_a;
                // Extend outgoing homm_alt_0_case1_edge__edge0 from homm_node_a 
                LGSPEdge edge_head_homm_alt_0_case1_edge__edge0 = node_cur_homm_node_a.outhead;
                if(edge_head_homm_alt_0_case1_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_homm_alt_0_case1_edge__edge0 = edge_head_homm_alt_0_case1_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_homm_alt_0_case1_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_homm_alt_0_case1_edge__edge0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        bool edge_cur_homm_alt_0_case1_edge__edge0_prevIsMatched = edge_cur_homm_alt_0_case1_edge__edge0.isMatched;
                        edge_cur_homm_alt_0_case1_edge__edge0.isMatched = true;
                        // Implicit target homm_alt_0_case1_node_b2 from homm_alt_0_case1_edge__edge0 
                        LGSPNode node_cur_homm_alt_0_case1_node_b2 = edge_cur_homm_alt_0_case1_edge__edge0.target;
                        if(!NodeType_B.isMyType[node_cur_homm_alt_0_case1_node_b2.type.TypeID]) {
                            edge_cur_homm_alt_0_case1_edge__edge0.isMatched = edge_cur_homm_alt_0_case1_edge__edge0_prevIsMatched;
                            continue;
                        }
                        if(node_cur_homm_alt_0_case1_node_b2.isMatchedByEnclosingPattern)
                        {
                            edge_cur_homm_alt_0_case1_edge__edge0.isMatched = edge_cur_homm_alt_0_case1_edge__edge0_prevIsMatched;
                            continue;
                        }
                        // Extend outgoing homm_alt_0_case1_edge__edge1 from homm_alt_0_case1_node_b2 
                        LGSPEdge edge_head_homm_alt_0_case1_edge__edge1 = node_cur_homm_alt_0_case1_node_b2.outhead;
                        if(edge_head_homm_alt_0_case1_edge__edge1 != null)
                        {
                            LGSPEdge edge_cur_homm_alt_0_case1_edge__edge1 = edge_head_homm_alt_0_case1_edge__edge1;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[edge_cur_homm_alt_0_case1_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_homm_alt_0_case1_edge__edge1.target != node_cur_homm_node_a) {
                                    continue;
                                }
                                if(edge_cur_homm_alt_0_case1_edge__edge1.isMatched
                                    && edge_cur_homm_alt_0_case1_edge__edge1==edge_cur_homm_alt_0_case1_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if(edge_cur_homm_alt_0_case1_edge__edge1.isMatchedByEnclosingPattern)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[2], new LGSPMatch[0]);
                                    match.patternGraph = patternGraph;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@a] = node_cur_homm_node_a;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@b2] = node_cur_homm_alt_0_case1_node_b2;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge0] = edge_cur_homm_alt_0_case1_edge__edge0;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge1] = edge_cur_homm_alt_0_case1_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        edge_cur_homm_alt_0_case1_edge__edge0.isMatched = edge_cur_homm_alt_0_case1_edge__edge0_prevIsMatched;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                node_cur_homm_node_a.isMatchedByEnclosingPattern = true;
                                node_cur_homm_alt_0_case1_node_b2.isMatchedByEnclosingPattern = true;
                                edge_cur_homm_alt_0_case1_edge__edge0.isMatchedByEnclosingPattern = true;
                                edge_cur_homm_alt_0_case1_edge__edge1.isMatchedByEnclosingPattern = true;
                                // Match subpatterns
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns were found, extend the partial matches by our local match object
                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[2], new LGSPMatch[0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@a] = node_cur_homm_node_a;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@b2] = node_cur_homm_alt_0_case1_node_b2;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge0] = edge_cur_homm_alt_0_case1_edge__edge0;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge1] = edge_cur_homm_alt_0_case1_edge__edge1;
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<LGSPMatch>>();
                                    } else {
                                        foreach(Stack<LGSPMatch> match in matchesList)
                                        {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        edge_cur_homm_alt_0_case1_edge__edge1.isMatchedByEnclosingPattern = false;
                                        edge_cur_homm_alt_0_case1_edge__edge0.isMatchedByEnclosingPattern = false;
                                        node_cur_homm_alt_0_case1_node_b2.isMatchedByEnclosingPattern = false;
                                        node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                                        edge_cur_homm_alt_0_case1_edge__edge0.isMatched = edge_cur_homm_alt_0_case1_edge__edge0_prevIsMatched;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    edge_cur_homm_alt_0_case1_edge__edge1.isMatchedByEnclosingPattern = false;
                                    edge_cur_homm_alt_0_case1_edge__edge0.isMatchedByEnclosingPattern = false;
                                    node_cur_homm_alt_0_case1_node_b2.isMatchedByEnclosingPattern = false;
                                    node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                                    continue;
                                }
                                node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                                node_cur_homm_alt_0_case1_node_b2.isMatchedByEnclosingPattern = false;
                                edge_cur_homm_alt_0_case1_edge__edge0.isMatchedByEnclosingPattern = false;
                                edge_cur_homm_alt_0_case1_edge__edge1.isMatchedByEnclosingPattern = false;
                            }
                            while( (edge_cur_homm_alt_0_case1_edge__edge1 = edge_cur_homm_alt_0_case1_edge__edge1.outNext) != edge_head_homm_alt_0_case1_edge__edge1 );
                        }
                        edge_cur_homm_alt_0_case1_edge__edge0.isMatched = edge_cur_homm_alt_0_case1_edge__edge0_prevIsMatched;
                    }
                    while( (edge_cur_homm_alt_0_case1_edge__edge0 = edge_cur_homm_alt_0_case1_edge__edge0.outNext) != edge_head_homm_alt_0_case1_edge__edge0 );
                }
            } while(false);
            // Alternative case homm_alt_0_case2 
            do {
                patternGraph = patternGraphs[(int)Rule_homm.homm_alt_0_CaseNums.@case2];
                // SubPreset homm_node_a 
                LGSPNode node_cur_homm_node_a = homm_node_a;
                // Extend outgoing homm_alt_0_case2_edge__edge0 from homm_node_a 
                LGSPEdge edge_head_homm_alt_0_case2_edge__edge0 = node_cur_homm_node_a.outhead;
                if(edge_head_homm_alt_0_case2_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_homm_alt_0_case2_edge__edge0 = edge_head_homm_alt_0_case2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[edge_cur_homm_alt_0_case2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_homm_alt_0_case2_edge__edge0.isMatchedByEnclosingPattern)
                        {
                            continue;
                        }
                        bool edge_cur_homm_alt_0_case2_edge__edge0_prevIsMatched = edge_cur_homm_alt_0_case2_edge__edge0.isMatched;
                        edge_cur_homm_alt_0_case2_edge__edge0.isMatched = true;
                        // Implicit target homm_alt_0_case2_node_b2 from homm_alt_0_case2_edge__edge0 
                        LGSPNode node_cur_homm_alt_0_case2_node_b2 = edge_cur_homm_alt_0_case2_edge__edge0.target;
                        if(!NodeType_B.isMyType[node_cur_homm_alt_0_case2_node_b2.type.TypeID]) {
                            edge_cur_homm_alt_0_case2_edge__edge0.isMatched = edge_cur_homm_alt_0_case2_edge__edge0_prevIsMatched;
                            continue;
                        }
                        if(node_cur_homm_alt_0_case2_node_b2.isMatchedByEnclosingPattern)
                        {
                            edge_cur_homm_alt_0_case2_edge__edge0.isMatched = edge_cur_homm_alt_0_case2_edge__edge0_prevIsMatched;
                            continue;
                        }
                        // Extend outgoing homm_alt_0_case2_edge__edge1 from homm_alt_0_case2_node_b2 
                        LGSPEdge edge_head_homm_alt_0_case2_edge__edge1 = node_cur_homm_alt_0_case2_node_b2.outhead;
                        if(edge_head_homm_alt_0_case2_edge__edge1 != null)
                        {
                            LGSPEdge edge_cur_homm_alt_0_case2_edge__edge1 = edge_head_homm_alt_0_case2_edge__edge1;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[edge_cur_homm_alt_0_case2_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_homm_alt_0_case2_edge__edge1.target != node_cur_homm_node_a) {
                                    continue;
                                }
                                if(edge_cur_homm_alt_0_case2_edge__edge1.isMatched
                                    && edge_cur_homm_alt_0_case2_edge__edge1==edge_cur_homm_alt_0_case2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if(edge_cur_homm_alt_0_case2_edge__edge1.isMatchedByEnclosingPattern)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[2], new LGSPMatch[0]);
                                    match.patternGraph = patternGraph;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@a] = node_cur_homm_node_a;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@b2] = node_cur_homm_alt_0_case2_node_b2;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge0] = edge_cur_homm_alt_0_case2_edge__edge0;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge1] = edge_cur_homm_alt_0_case2_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        edge_cur_homm_alt_0_case2_edge__edge0.isMatched = edge_cur_homm_alt_0_case2_edge__edge0_prevIsMatched;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                node_cur_homm_node_a.isMatchedByEnclosingPattern = true;
                                node_cur_homm_alt_0_case2_node_b2.isMatchedByEnclosingPattern = true;
                                edge_cur_homm_alt_0_case2_edge__edge0.isMatchedByEnclosingPattern = true;
                                edge_cur_homm_alt_0_case2_edge__edge1.isMatchedByEnclosingPattern = true;
                                // Match subpatterns
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns were found, extend the partial matches by our local match object
                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[2], new LGSPMatch[0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@a] = node_cur_homm_node_a;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@b2] = node_cur_homm_alt_0_case2_node_b2;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge0] = edge_cur_homm_alt_0_case2_edge__edge0;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge1] = edge_cur_homm_alt_0_case2_edge__edge1;
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<LGSPMatch>>();
                                    } else {
                                        foreach(Stack<LGSPMatch> match in matchesList)
                                        {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        edge_cur_homm_alt_0_case2_edge__edge1.isMatchedByEnclosingPattern = false;
                                        edge_cur_homm_alt_0_case2_edge__edge0.isMatchedByEnclosingPattern = false;
                                        node_cur_homm_alt_0_case2_node_b2.isMatchedByEnclosingPattern = false;
                                        node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                                        edge_cur_homm_alt_0_case2_edge__edge0.isMatched = edge_cur_homm_alt_0_case2_edge__edge0_prevIsMatched;
                                        openTasks.Push(this);
                                        return;
                                    }
                                    edge_cur_homm_alt_0_case2_edge__edge1.isMatchedByEnclosingPattern = false;
                                    edge_cur_homm_alt_0_case2_edge__edge0.isMatchedByEnclosingPattern = false;
                                    node_cur_homm_alt_0_case2_node_b2.isMatchedByEnclosingPattern = false;
                                    node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                                    continue;
                                }
                                node_cur_homm_node_a.isMatchedByEnclosingPattern = false;
                                node_cur_homm_alt_0_case2_node_b2.isMatchedByEnclosingPattern = false;
                                edge_cur_homm_alt_0_case2_edge__edge0.isMatchedByEnclosingPattern = false;
                                edge_cur_homm_alt_0_case2_edge__edge1.isMatchedByEnclosingPattern = false;
                            }
                            while( (edge_cur_homm_alt_0_case2_edge__edge1 = edge_cur_homm_alt_0_case2_edge__edge1.outNext) != edge_head_homm_alt_0_case2_edge__edge1 );
                        }
                        edge_cur_homm_alt_0_case2_edge__edge0.isMatched = edge_cur_homm_alt_0_case2_edge__edge0_prevIsMatched;
                    }
                    while( (edge_cur_homm_alt_0_case2_edge__edge0 = edge_cur_homm_alt_0_case2_edge__edge0.outNext) != edge_head_homm_alt_0_case2_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_leer : LGSPAction
    {
        public Action_leer() {
            rulePattern = Rule_leer.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0);
        }

        public override string Name { get { return "leer"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_leer instance = new Action_leer();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for leer_alt_0
            AlternativeAction_leer_alt_0 taskFor_alt_0 = new AlternativeAction_leer_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_leer.leer_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            return matches;
        }
    }

    public class AlternativeAction_leer_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_leer_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches)
        {
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case leer_alt_0_leer 
            do {
                patternGraph = patternGraphs[(int)Rule_leer.leer_alt_0_CaseNums.@leer];
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    LGSPMatch match = new LGSPMatch(new LGSPNode[0], new LGSPEdge[0], new LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[0], new LGSPEdge[0], new LGSPMatch[0]);
                        match.patternGraph = patternGraph;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList)
                        {
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
            } while(false);
            openTasks.Push(this);
            return;
        }
    }


    public class AlternativesActions : LGSPActions
    {
        public AlternativesActions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, dumperfactory, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public AlternativesActions(LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("AandnotCorB", (LGSPAction) Action_AandnotCorB.Instance);
            actions.Add("AorB", (LGSPAction) Action_AorB.Instance);
            actions.Add("AorBorC", (LGSPAction) Action_AorBorC.Instance);
            actions.Add("AtoAorB", (LGSPAction) Action_AtoAorB.Instance);
            actions.Add("Complex", (LGSPAction) Action_Complex.Instance);
            actions.Add("XtoAorB", (LGSPAction) Action_XtoAorB.Instance);
            actions.Add("createA", (LGSPAction) Action_createA.Instance);
            actions.Add("createABA", (LGSPAction) Action_createABA.Instance);
            actions.Add("createAtoB", (LGSPAction) Action_createAtoB.Instance);
            actions.Add("createB", (LGSPAction) Action_createB.Instance);
            actions.Add("createC", (LGSPAction) Action_createC.Instance);
            actions.Add("createComplex", (LGSPAction) Action_createComplex.Instance);
            actions.Add("homm", (LGSPAction) Action_homm.Instance);
            actions.Add("leer", (LGSPAction) Action_leer.Instance);
        }

        public override String Name { get { return "AlternativesActions"; } }
        public override String ModelMD5Hash { get { return "e6c5215b8441bdc431ed9a3538e41e73"; } }
    }
}