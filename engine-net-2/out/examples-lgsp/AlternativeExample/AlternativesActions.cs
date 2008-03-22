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
		public static Pattern_toAorB Instance { get { if (instance==null) { instance = new Pattern_toAorB(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "toAorB_node_x", };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_toAorB;
			bool[,] toAorB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] toAorB_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode toAorB_node_x = new PatternNode((int) NodeTypes.@Node, "toAorB_node_x", "x", toAorB_node_x_AllowedTypes, toAorB_node_x_IsAllowedType, 5.5F, 0);
			PatternEdge toAorB_edge_y = new PatternEdge(true, (int) EdgeTypes.@Edge, "toAorB_edge_y", "y", toAorB_edge_y_AllowedTypes, toAorB_edge_y_IsAllowedType, 5.5F, -1);
			PatternGraph toAorB_alt_0_toA;
			bool[,] toAorB_alt_0_toA_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] toAorB_alt_0_toA_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode toAorB_alt_0_toA_node_a = new PatternNode((int) NodeTypes.@A, "toAorB_alt_0_toA_node_a", "a", toAorB_alt_0_toA_node_a_AllowedTypes, toAorB_alt_0_toA_node_a_IsAllowedType, 5.5F, -1);
			toAorB_alt_0_toA = new PatternGraph(
				"toA",
				"toAorB_alt_0_",
				false,
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
				toAorB_alt_0_toA_isNodeHomomorphicGlobal,
				toAorB_alt_0_toA_isEdgeHomomorphicGlobal
			);
			toAorB_alt_0_toA.edgeToTargetNode.Add(toAorB_edge_y, toAorB_alt_0_toA_node_a);
			PatternGraph toAorB_alt_0_toB;
			bool[,] toAorB_alt_0_toB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] toAorB_alt_0_toB_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode toAorB_alt_0_toB_node_b = new PatternNode((int) NodeTypes.@B, "toAorB_alt_0_toB_node_b", "b", toAorB_alt_0_toB_node_b_AllowedTypes, toAorB_alt_0_toB_node_b_IsAllowedType, 5.5F, -1);
			toAorB_alt_0_toB = new PatternGraph(
				"toB",
				"toAorB_alt_0_",
				false,
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
				toAorB_alt_0_toB_isNodeHomomorphicGlobal,
				toAorB_alt_0_toB_isEdgeHomomorphicGlobal
			);
			toAorB_alt_0_toB.edgeToTargetNode.Add(toAorB_edge_y, toAorB_alt_0_toB_node_b);
			Alternative toAorB_alt_0 = new Alternative( "alt_0", "toAorB_", new PatternGraph[] { toAorB_alt_0_toA, toAorB_alt_0_toB } );

			pat_toAorB = new PatternGraph(
				"toAorB",
				"",
				false,
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

	public class Rule_createA : LGSPRulePattern
	{
		private static Rule_createA instance = null;
		public static Rule_createA Instance { get { if (instance==null) { instance = new Rule_createA(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_createA;
			bool[,] createA_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createA_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createA = new PatternGraph(
				"createA",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createA_isNodeHomomorphicGlobal,
				createA_isEdgeHomomorphicGlobal
			);

			patternGraph = pat_createA;
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

	public class Rule_createB : LGSPRulePattern
	{
		private static Rule_createB instance = null;
		public static Rule_createB Instance { get { if (instance==null) { instance = new Rule_createB(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_createB;
			bool[,] createB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createB = new PatternGraph(
				"createB",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createB_isNodeHomomorphicGlobal,
				createB_isEdgeHomomorphicGlobal
			);

			patternGraph = pat_createB;
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
		public static Rule_createC Instance { get { if (instance==null) { instance = new Rule_createC(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_createC;
			bool[,] createC_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createC_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createC = new PatternGraph(
				"createC",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createC_isNodeHomomorphicGlobal,
				createC_isEdgeHomomorphicGlobal
			);

			patternGraph = pat_createC;
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

	public class Rule_createAtoB : LGSPRulePattern
	{
		private static Rule_createAtoB instance = null;
		public static Rule_createAtoB Instance { get { if (instance==null) { instance = new Rule_createAtoB(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_createAtoB;
			bool[,] createAtoB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createAtoB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createAtoB = new PatternGraph(
				"createAtoB",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createAtoB_isNodeHomomorphicGlobal,
				createAtoB_isEdgeHomomorphicGlobal
			);

			patternGraph = pat_createAtoB;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node__node1 = Node_B.CreateNode(graph);
			Node_A node__node0 = Node_A.CreateNode(graph);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node__node1 = Node_B.CreateNode(graph);
			Node_A node__node0 = Node_A.CreateNode(graph);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node1", "_node0" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_leer : LGSPRulePattern
	{
		private static Rule_leer instance = null;
		public static Rule_leer Instance { get { if (instance==null) { instance = new Rule_leer(); instance.initialize(); } return instance; } }

		public enum leer_NodeNums { };
		public enum leer_EdgeNums { };
		public enum leer_SubNums { };
		public enum leer_AltNums { @alt_0, };
		public enum leer_alt_0_CaseNums { @altleer, };
		public enum leer_alt_0_altleer_NodeNums { };
		public enum leer_alt_0_altleer_EdgeNums { };
		public enum leer_alt_0_altleer_SubNums { };
		public enum leer_alt_0_altleer_AltNums { };

#if INITIAL_WARMUP
		public Rule_leer()
#else
		private Rule_leer()
#endif
		{
			name = "leer";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_leer;
			bool[,] leer_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] leer_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternGraph leer_alt_0_altleer;
			bool[,] leer_alt_0_altleer_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] leer_alt_0_altleer_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			leer_alt_0_altleer = new PatternGraph(
				"altleer",
				"leer_alt_0_",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				leer_alt_0_altleer_isNodeHomomorphicGlobal,
				leer_alt_0_altleer_isEdgeHomomorphicGlobal
			);
			Alternative leer_alt_0 = new Alternative( "alt_0", "leer_", new PatternGraph[] { leer_alt_0_altleer } );

			pat_leer = new PatternGraph(
				"leer",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { leer_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				leer_isNodeHomomorphicGlobal,
				leer_isEdgeHomomorphicGlobal
			);
			leer_alt_0_altleer.embeddingGraph = pat_leer;

			patternGraph = pat_leer;
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
		public static Rule_AorB Instance { get { if (instance==null) { instance = new Rule_AorB(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_AorB;
			bool[,] AorB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternGraph AorB_alt_0_A;
			bool[,] AorB_alt_0_A_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorB_alt_0_A_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AorB_alt_0_A_node__node0 = new PatternNode((int) NodeTypes.@A, "AorB_alt_0_A_node__node0", "_node0", AorB_alt_0_A_node__node0_AllowedTypes, AorB_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			AorB_alt_0_A = new PatternGraph(
				"A",
				"AorB_alt_0_",
				false,
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
				AorB_alt_0_A_isNodeHomomorphicGlobal,
				AorB_alt_0_A_isEdgeHomomorphicGlobal
			);
			PatternGraph AorB_alt_0_B;
			bool[,] AorB_alt_0_B_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorB_alt_0_B_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AorB_alt_0_B_node__node0 = new PatternNode((int) NodeTypes.@B, "AorB_alt_0_B_node__node0", "_node0", AorB_alt_0_B_node__node0_AllowedTypes, AorB_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AorB_alt_0_B = new PatternGraph(
				"B",
				"AorB_alt_0_",
				false,
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
				AorB_alt_0_B_isNodeHomomorphicGlobal,
				AorB_alt_0_B_isEdgeHomomorphicGlobal
			);
			Alternative AorB_alt_0 = new Alternative( "alt_0", "AorB_", new PatternGraph[] { AorB_alt_0_A, AorB_alt_0_B } );

			pat_AorB = new PatternGraph(
				"AorB",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { AorB_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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

	public class Rule_AandnotCorB : LGSPRulePattern
	{
		private static Rule_AandnotCorB instance = null;
		public static Rule_AandnotCorB Instance { get { if (instance==null) { instance = new Rule_AandnotCorB(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_AandnotCorB;
			bool[,] AandnotCorB_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AandnotCorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternGraph AandnotCorB_alt_0_A;
			bool[,] AandnotCorB_alt_0_A_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AandnotCorB_alt_0_A_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AandnotCorB_alt_0_A_node__node0 = new PatternNode((int) NodeTypes.@A, "AandnotCorB_alt_0_A_node__node0", "_node0", AandnotCorB_alt_0_A_node__node0_AllowedTypes, AandnotCorB_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			PatternGraph AandnotCorB_alt_0_A_neg_0;
			bool[,] AandnotCorB_alt_0_A_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AandnotCorB_alt_0_A_neg_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AandnotCorB_alt_0_A_neg_0_node__node0 = new PatternNode((int) NodeTypes.@C, "AandnotCorB_alt_0_A_neg_0_node__node0", "_node0", AandnotCorB_alt_0_A_neg_0_node__node0_AllowedTypes, AandnotCorB_alt_0_A_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			AandnotCorB_alt_0_A_neg_0 = new PatternGraph(
				"neg_0",
				"AandnotCorB_alt_0_A_",
				false,
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
				AandnotCorB_alt_0_A_neg_0_isNodeHomomorphicGlobal,
				AandnotCorB_alt_0_A_neg_0_isEdgeHomomorphicGlobal
			);
			AandnotCorB_alt_0_A = new PatternGraph(
				"A",
				"AandnotCorB_alt_0_",
				false,
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
				AandnotCorB_alt_0_A_isNodeHomomorphicGlobal,
				AandnotCorB_alt_0_A_isEdgeHomomorphicGlobal
			);
			AandnotCorB_alt_0_A_neg_0.embeddingGraph = AandnotCorB_alt_0_A;
			PatternGraph AandnotCorB_alt_0_B;
			bool[,] AandnotCorB_alt_0_B_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AandnotCorB_alt_0_B_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AandnotCorB_alt_0_B_node__node0 = new PatternNode((int) NodeTypes.@B, "AandnotCorB_alt_0_B_node__node0", "_node0", AandnotCorB_alt_0_B_node__node0_AllowedTypes, AandnotCorB_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AandnotCorB_alt_0_B = new PatternGraph(
				"B",
				"AandnotCorB_alt_0_",
				false,
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
				AandnotCorB_alt_0_B_isNodeHomomorphicGlobal,
				AandnotCorB_alt_0_B_isEdgeHomomorphicGlobal
			);
			Alternative AandnotCorB_alt_0 = new Alternative( "alt_0", "AandnotCorB_", new PatternGraph[] { AandnotCorB_alt_0_A, AandnotCorB_alt_0_B } );

			pat_AandnotCorB = new PatternGraph(
				"AandnotCorB",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { AandnotCorB_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
		public static Rule_AorBorC Instance { get { if (instance==null) { instance = new Rule_AorBorC(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_AorBorC;
			bool[,] AorBorC_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] AorBorC_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternGraph AorBorC_alt_0_A;
			bool[,] AorBorC_alt_0_A_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorBorC_alt_0_A_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AorBorC_alt_0_A_node__node0 = new PatternNode((int) NodeTypes.@A, "AorBorC_alt_0_A_node__node0", "_node0", AorBorC_alt_0_A_node__node0_AllowedTypes, AorBorC_alt_0_A_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_A = new PatternGraph(
				"A",
				"AorBorC_alt_0_",
				false,
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
				AorBorC_alt_0_A_isNodeHomomorphicGlobal,
				AorBorC_alt_0_A_isEdgeHomomorphicGlobal
			);
			PatternGraph AorBorC_alt_0_B;
			bool[,] AorBorC_alt_0_B_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorBorC_alt_0_B_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AorBorC_alt_0_B_node__node0 = new PatternNode((int) NodeTypes.@B, "AorBorC_alt_0_B_node__node0", "_node0", AorBorC_alt_0_B_node__node0_AllowedTypes, AorBorC_alt_0_B_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_B = new PatternGraph(
				"B",
				"AorBorC_alt_0_",
				false,
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
				AorBorC_alt_0_B_isNodeHomomorphicGlobal,
				AorBorC_alt_0_B_isEdgeHomomorphicGlobal
			);
			PatternGraph AorBorC_alt_0_C;
			bool[,] AorBorC_alt_0_C_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AorBorC_alt_0_C_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AorBorC_alt_0_C_node__node0 = new PatternNode((int) NodeTypes.@C, "AorBorC_alt_0_C_node__node0", "_node0", AorBorC_alt_0_C_node__node0_AllowedTypes, AorBorC_alt_0_C_node__node0_IsAllowedType, 5.5F, -1);
			AorBorC_alt_0_C = new PatternGraph(
				"C",
				"AorBorC_alt_0_",
				false,
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
				AorBorC_alt_0_C_isNodeHomomorphicGlobal,
				AorBorC_alt_0_C_isEdgeHomomorphicGlobal
			);
			Alternative AorBorC_alt_0 = new Alternative( "alt_0", "AorBorC_", new PatternGraph[] { AorBorC_alt_0_A, AorBorC_alt_0_B, AorBorC_alt_0_C } );

			pat_AorBorC = new PatternGraph(
				"AorBorC",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { AorBorC_alt_0,  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
		public static Rule_AtoAorB Instance { get { if (instance==null) { instance = new Rule_AtoAorB(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_AtoAorB;
			bool[,] AtoAorB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] AtoAorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode AtoAorB_node_a = new PatternNode((int) NodeTypes.@A, "AtoAorB_node_a", "a", AtoAorB_node_a_AllowedTypes, AtoAorB_node_a_IsAllowedType, 5.5F, -1);
			PatternGraph AtoAorB_alt_0_toA;
			bool[,] AtoAorB_alt_0_toA_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] AtoAorB_alt_0_toA_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode AtoAorB_alt_0_toA_node__node0 = new PatternNode((int) NodeTypes.@A, "AtoAorB_alt_0_toA_node__node0", "_node0", AtoAorB_alt_0_toA_node__node0_AllowedTypes, AtoAorB_alt_0_toA_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge AtoAorB_alt_0_toA_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "AtoAorB_alt_0_toA_edge__edge0", "_edge0", AtoAorB_alt_0_toA_edge__edge0_AllowedTypes, AtoAorB_alt_0_toA_edge__edge0_IsAllowedType, 5.5F, -1);
			AtoAorB_alt_0_toA = new PatternGraph(
				"toA",
				"AtoAorB_alt_0_",
				false,
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
				AtoAorB_alt_0_toA_isNodeHomomorphicGlobal,
				AtoAorB_alt_0_toA_isEdgeHomomorphicGlobal
			);
			AtoAorB_alt_0_toA.edgeToSourceNode.Add(AtoAorB_alt_0_toA_edge__edge0, AtoAorB_node_a);
			AtoAorB_alt_0_toA.edgeToTargetNode.Add(AtoAorB_alt_0_toA_edge__edge0, AtoAorB_alt_0_toA_node__node0);
			PatternGraph AtoAorB_alt_0_toB;
			bool[,] AtoAorB_alt_0_toB_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] AtoAorB_alt_0_toB_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode AtoAorB_alt_0_toB_node__node0 = new PatternNode((int) NodeTypes.@B, "AtoAorB_alt_0_toB_node__node0", "_node0", AtoAorB_alt_0_toB_node__node0_AllowedTypes, AtoAorB_alt_0_toB_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge AtoAorB_alt_0_toB_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "AtoAorB_alt_0_toB_edge__edge0", "_edge0", AtoAorB_alt_0_toB_edge__edge0_AllowedTypes, AtoAorB_alt_0_toB_edge__edge0_IsAllowedType, 5.5F, -1);
			AtoAorB_alt_0_toB = new PatternGraph(
				"toB",
				"AtoAorB_alt_0_",
				false,
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
				AtoAorB_alt_0_toB_isNodeHomomorphicGlobal,
				AtoAorB_alt_0_toB_isEdgeHomomorphicGlobal
			);
			AtoAorB_alt_0_toB.edgeToSourceNode.Add(AtoAorB_alt_0_toB_edge__edge0, AtoAorB_node_a);
			AtoAorB_alt_0_toB.edgeToTargetNode.Add(AtoAorB_alt_0_toB_edge__edge0, AtoAorB_alt_0_toB_node__node0);
			Alternative AtoAorB_alt_0 = new Alternative( "alt_0", "AtoAorB_", new PatternGraph[] { AtoAorB_alt_0_toA, AtoAorB_alt_0_toB } );

			pat_AtoAorB = new PatternGraph(
				"AtoAorB",
				"",
				false,
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

	public class Rule_createComplex : LGSPRulePattern
	{
		private static Rule_createComplex instance = null;
		public static Rule_createComplex Instance { get { if (instance==null) { instance = new Rule_createComplex(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_createComplex;
			bool[,] createComplex_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createComplex_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createComplex = new PatternGraph(
				"createComplex",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createComplex_isNodeHomomorphicGlobal,
				createComplex_isEdgeHomomorphicGlobal
			);

			patternGraph = pat_createComplex;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node_b2 = Node_B.CreateNode(graph);
			Node_A node_a = Node_A.CreateNode(graph);
			Node_B node_b = Node_B.CreateNode(graph);
			Node_C node__node1 = Node_C.CreateNode(graph);
			Node_C node__node2 = Node_C.CreateNode(graph);
			Node_C node__node0 = Node_C.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_b2, node_a);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_a, node_b2);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			Edge_Edge edge__edge6 = Edge_Edge.CreateEdge(graph, node__node1, node__node2);
			Edge_Edge edge__edge5 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			Edge_Edge edge__edge4 = Edge_Edge.CreateEdge(graph, node_b, node__node0);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_B node_b2 = Node_B.CreateNode(graph);
			Node_A node_a = Node_A.CreateNode(graph);
			Node_B node_b = Node_B.CreateNode(graph);
			Node_C node__node1 = Node_C.CreateNode(graph);
			Node_C node__node2 = Node_C.CreateNode(graph);
			Node_C node__node0 = Node_C.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_b2, node_a);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_a, node_b2);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			Edge_Edge edge__edge6 = Edge_Edge.CreateEdge(graph, node__node1, node__node2);
			Edge_Edge edge__edge5 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			Edge_Edge edge__edge4 = Edge_Edge.CreateEdge(graph, node_b, node__node0);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "b2", "a", "b", "_node1", "_node2", "_node0" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge3", "_edge2", "_edge1", "_edge0", "_edge6", "_edge5", "_edge4" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_Complex : LGSPRulePattern
	{
		private static Rule_Complex instance = null;
		public static Rule_Complex Instance { get { if (instance==null) { instance = new Rule_Complex(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_Complex;
			bool[,] Complex_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Complex_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternNode Complex_node_a = new PatternNode((int) NodeTypes.@A, "Complex_node_a", "a", Complex_node_a_AllowedTypes, Complex_node_a_IsAllowedType, 5.5F, -1);
			PatternNode Complex_node_b = new PatternNode((int) NodeTypes.@B, "Complex_node_b", "b", Complex_node_b_AllowedTypes, Complex_node_b_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_edge__edge0", "_edge0", Complex_edge__edge0_AllowedTypes, Complex_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_edge__edge1", "_edge1", Complex_edge__edge1_AllowedTypes, Complex_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternGraph Complex_alt_0_ExtendAv;
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
			PatternNode Complex_alt_0_ExtendAv_node_b2 = new PatternNode((int) NodeTypes.@B, "Complex_alt_0_ExtendAv_node_b2", "b2", Complex_alt_0_ExtendAv_node_b2_AllowedTypes, Complex_alt_0_ExtendAv_node_b2_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv_node__node0 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv_node__node0", "_node0", Complex_alt_0_ExtendAv_node__node0_AllowedTypes, Complex_alt_0_ExtendAv_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv_node__node1 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv_node__node1", "_node1", Complex_alt_0_ExtendAv_node__node1_AllowedTypes, Complex_alt_0_ExtendAv_node__node1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge0", "_edge0", Complex_alt_0_ExtendAv_edge__edge0_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge1", "_edge1", Complex_alt_0_ExtendAv_edge__edge1_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge2 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge2", "_edge2", Complex_alt_0_ExtendAv_edge__edge2_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv_edge__edge3 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv_edge__edge3", "_edge3", Complex_alt_0_ExtendAv_edge__edge3_AllowedTypes, Complex_alt_0_ExtendAv_edge__edge3_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendAv = new PatternGraph(
				"ExtendAv",
				"Complex_alt_0_",
				false,
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
			PatternGraph Complex_alt_0_ExtendAv2;
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
			PatternNode Complex_alt_0_ExtendAv2_node_b2 = new PatternNode((int) NodeTypes.@B, "Complex_alt_0_ExtendAv2_node_b2", "b2", Complex_alt_0_ExtendAv2_node_b2_AllowedTypes, Complex_alt_0_ExtendAv2_node_b2_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv2_node__node0 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv2_node__node0", "_node0", Complex_alt_0_ExtendAv2_node__node0_AllowedTypes, Complex_alt_0_ExtendAv2_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv2_node__node1 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv2_node__node1", "_node1", Complex_alt_0_ExtendAv2_node__node1_AllowedTypes, Complex_alt_0_ExtendAv2_node__node1_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendAv2_node__node2 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendAv2_node__node2", "_node2", Complex_alt_0_ExtendAv2_node__node2_AllowedTypes, Complex_alt_0_ExtendAv2_node__node2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge0", "_edge0", Complex_alt_0_ExtendAv2_edge__edge0_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge1", "_edge1", Complex_alt_0_ExtendAv2_edge__edge1_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge2 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge2", "_edge2", Complex_alt_0_ExtendAv2_edge__edge2_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge3 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge3", "_edge3", Complex_alt_0_ExtendAv2_edge__edge3_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge3_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendAv2_edge__edge4 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendAv2_edge__edge4", "_edge4", Complex_alt_0_ExtendAv2_edge__edge4_AllowedTypes, Complex_alt_0_ExtendAv2_edge__edge4_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendAv2 = new PatternGraph(
				"ExtendAv2",
				"Complex_alt_0_",
				false,
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
			PatternGraph Complex_alt_0_ExtendNA2;
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
			PatternNode Complex_alt_0_ExtendNA2_node__node0 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendNA2_node__node0", "_node0", Complex_alt_0_ExtendNA2_node__node0_AllowedTypes, Complex_alt_0_ExtendNA2_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendNA2_node__node1 = new PatternNode((int) NodeTypes.@C, "Complex_alt_0_ExtendNA2_node__node1", "_node1", Complex_alt_0_ExtendNA2_node__node1_AllowedTypes, Complex_alt_0_ExtendNA2_node__node1_IsAllowedType, 5.5F, -1);
			PatternNode Complex_alt_0_ExtendNA2_node_b2 = new PatternNode((int) NodeTypes.@B, "Complex_alt_0_ExtendNA2_node_b2", "b2", Complex_alt_0_ExtendNA2_node_b2_AllowedTypes, Complex_alt_0_ExtendNA2_node_b2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge0", "_edge0", Complex_alt_0_ExtendNA2_edge__edge0_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge1", "_edge1", Complex_alt_0_ExtendNA2_edge__edge1_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge2 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge2", "_edge2", Complex_alt_0_ExtendNA2_edge__edge2_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge Complex_alt_0_ExtendNA2_edge__edge3 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Complex_alt_0_ExtendNA2_edge__edge3", "_edge3", Complex_alt_0_ExtendNA2_edge__edge3_AllowedTypes, Complex_alt_0_ExtendNA2_edge__edge3_IsAllowedType, 5.5F, -1);
			Complex_alt_0_ExtendNA2 = new PatternGraph(
				"ExtendNA2",
				"Complex_alt_0_",
				false,
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
			Alternative Complex_alt_0 = new Alternative( "alt_0", "Complex_", new PatternGraph[] { Complex_alt_0_ExtendAv, Complex_alt_0_ExtendAv2, Complex_alt_0_ExtendNA2 } );

			pat_Complex = new PatternGraph(
				"Complex",
				"",
				false,
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

	public class Rule_ComplexMax : LGSPRulePattern
	{
		private static Rule_ComplexMax instance = null;
		public static Rule_ComplexMax Instance { get { if (instance==null) { instance = new Rule_ComplexMax(); instance.initialize(); } return instance; } }

		public static NodeType[] ComplexMax_node_a_AllowedTypes = null;
		public static NodeType[] ComplexMax_node_b_AllowedTypes = null;
		public static bool[] ComplexMax_node_a_IsAllowedType = null;
		public static bool[] ComplexMax_node_b_IsAllowedType = null;
		public static EdgeType[] ComplexMax_edge__edge0_AllowedTypes = null;
		public static EdgeType[] ComplexMax_edge__edge1_AllowedTypes = null;
		public static bool[] ComplexMax_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_edge__edge1_IsAllowedType = null;
		public enum ComplexMax_NodeNums { @a, @b, };
		public enum ComplexMax_EdgeNums { @_edge0, @_edge1, };
		public enum ComplexMax_SubNums { };
		public enum ComplexMax_AltNums { @alt_0, };
		public enum ComplexMax_alt_0_CaseNums { @ExtendAv, @ExtendAv2, @ExtendNA2, };
		public static NodeType[] ComplexMax_alt_0_ExtendAv_node_b2_AllowedTypes = null;
		public static NodeType[] ComplexMax_alt_0_ExtendAv_node__node0_AllowedTypes = null;
		public static NodeType[] ComplexMax_alt_0_ExtendAv_node_c_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_node_b2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_node__node0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_node_c_IsAllowedType = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge0_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge1_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge2_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv_edge__edge3_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_edge__edge3_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendAv_NodeNums { @a, @b2, @b, @_node0, @c, };
		public enum ComplexMax_alt_0_ExtendAv_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum ComplexMax_alt_0_ExtendAv_SubNums { };
		public enum ComplexMax_alt_0_ExtendAv_AltNums { };
		public static NodeType[] ComplexMax_alt_0_ExtendAv_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_neg_0_node__node0_IsAllowedType = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendAv_neg_0_NodeNums { @c, @_node0, };
		public enum ComplexMax_alt_0_ExtendAv_neg_0_EdgeNums { @_edge0, };
		public enum ComplexMax_alt_0_ExtendAv_neg_0_SubNums { };
		public enum ComplexMax_alt_0_ExtendAv_neg_0_AltNums { };
		public static NodeType[] ComplexMax_alt_0_ExtendAv2_node_b2_AllowedTypes = null;
		public static NodeType[] ComplexMax_alt_0_ExtendAv2_node__node0_AllowedTypes = null;
		public static NodeType[] ComplexMax_alt_0_ExtendAv2_node__node1_AllowedTypes = null;
		public static NodeType[] ComplexMax_alt_0_ExtendAv2_node__node2_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node_b2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node__node0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node__node1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_node__node2_IsAllowedType = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge0_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge1_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge2_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge3_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendAv2_edge__edge4_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge3_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendAv2_edge__edge4_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendAv2_NodeNums { @a, @b2, @b, @_node0, @_node1, @_node2, };
		public enum ComplexMax_alt_0_ExtendAv2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, @_edge4, };
		public enum ComplexMax_alt_0_ExtendAv2_SubNums { };
		public enum ComplexMax_alt_0_ExtendAv2_AltNums { };
		public static NodeType[] ComplexMax_alt_0_ExtendNA2_node__node0_AllowedTypes = null;
		public static NodeType[] ComplexMax_alt_0_ExtendNA2_node__node1_AllowedTypes = null;
		public static NodeType[] ComplexMax_alt_0_ExtendNA2_node_b2_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_node__node0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_node__node1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_node_b2_IsAllowedType = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge0_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge1_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge2_AllowedTypes = null;
		public static EdgeType[] ComplexMax_alt_0_ExtendNA2_edge__edge3_AllowedTypes = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge0_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge1_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge2_IsAllowedType = null;
		public static bool[] ComplexMax_alt_0_ExtendNA2_edge__edge3_IsAllowedType = null;
		public enum ComplexMax_alt_0_ExtendNA2_NodeNums { @a, @_node0, @_node1, @b, @b2, };
		public enum ComplexMax_alt_0_ExtendNA2_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum ComplexMax_alt_0_ExtendNA2_SubNums { };
		public enum ComplexMax_alt_0_ExtendNA2_AltNums { };

#if INITIAL_WARMUP
		public Rule_ComplexMax()
#else
		private Rule_ComplexMax()
#endif
		{
			name = "ComplexMax";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_ComplexMax;
			bool[,] ComplexMax_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ComplexMax_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternNode ComplexMax_node_a = new PatternNode((int) NodeTypes.@A, "ComplexMax_node_a", "a", ComplexMax_node_a_AllowedTypes, ComplexMax_node_a_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_node_b = new PatternNode((int) NodeTypes.@B, "ComplexMax_node_b", "b", ComplexMax_node_b_AllowedTypes, ComplexMax_node_b_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_edge__edge0", "_edge0", ComplexMax_edge__edge0_AllowedTypes, ComplexMax_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_edge__edge1", "_edge1", ComplexMax_edge__edge1_AllowedTypes, ComplexMax_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternGraph ComplexMax_alt_0_ExtendAv;
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
			PatternNode ComplexMax_alt_0_ExtendAv_node_b2 = new PatternNode((int) NodeTypes.@B, "ComplexMax_alt_0_ExtendAv_node_b2", "b2", ComplexMax_alt_0_ExtendAv_node_b2_AllowedTypes, ComplexMax_alt_0_ExtendAv_node_b2_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_alt_0_ExtendAv_node__node0 = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendAv_node__node0", "_node0", ComplexMax_alt_0_ExtendAv_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendAv_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_alt_0_ExtendAv_node_c = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendAv_node_c", "c", ComplexMax_alt_0_ExtendAv_node_c_AllowedTypes, ComplexMax_alt_0_ExtendAv_node_c_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendAv_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv_edge__edge1", "_edge1", ComplexMax_alt_0_ExtendAv_edge__edge1_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge2 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv_edge__edge2", "_edge2", ComplexMax_alt_0_ExtendAv_edge__edge2_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv_edge__edge3 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv_edge__edge3", "_edge3", ComplexMax_alt_0_ExtendAv_edge__edge3_AllowedTypes, ComplexMax_alt_0_ExtendAv_edge__edge3_IsAllowedType, 5.5F, -1);
			PatternGraph ComplexMax_alt_0_ExtendAv_neg_0;
			bool[,] ComplexMax_alt_0_ExtendAv_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ComplexMax_alt_0_ExtendAv_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ComplexMax_alt_0_ExtendAv_neg_0_node__node0 = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendAv_neg_0_node__node0", "_node0", ComplexMax_alt_0_ExtendAv_neg_0_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendAv_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			ComplexMax_alt_0_ExtendAv_neg_0 = new PatternGraph(
				"neg_0",
				"ComplexMax_alt_0_ExtendAv_",
				false,
				new PatternNode[] { ComplexMax_alt_0_ExtendAv_node_c, ComplexMax_alt_0_ExtendAv_neg_0_node__node0 }, 
				new PatternEdge[] { ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 }, 
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
				ComplexMax_alt_0_ExtendAv_neg_0_isNodeHomomorphicGlobal,
				ComplexMax_alt_0_ExtendAv_neg_0_isEdgeHomomorphicGlobal
			);
			ComplexMax_alt_0_ExtendAv_neg_0.edgeToSourceNode.Add(ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0, ComplexMax_alt_0_ExtendAv_node_c);
			ComplexMax_alt_0_ExtendAv_neg_0.edgeToTargetNode.Add(ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0, ComplexMax_alt_0_ExtendAv_neg_0_node__node0);
			ComplexMax_alt_0_ExtendAv = new PatternGraph(
				"ExtendAv",
				"ComplexMax_alt_0_",
				false,
				new PatternNode[] { ComplexMax_node_a, ComplexMax_alt_0_ExtendAv_node_b2, ComplexMax_node_b, ComplexMax_alt_0_ExtendAv_node__node0, ComplexMax_alt_0_ExtendAv_node_c }, 
				new PatternEdge[] { ComplexMax_alt_0_ExtendAv_edge__edge0, ComplexMax_alt_0_ExtendAv_edge__edge1, ComplexMax_alt_0_ExtendAv_edge__edge2, ComplexMax_alt_0_ExtendAv_edge__edge3 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { ComplexMax_alt_0_ExtendAv_neg_0,  }, 
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
			PatternGraph ComplexMax_alt_0_ExtendAv2;
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
			PatternNode ComplexMax_alt_0_ExtendAv2_node_b2 = new PatternNode((int) NodeTypes.@B, "ComplexMax_alt_0_ExtendAv2_node_b2", "b2", ComplexMax_alt_0_ExtendAv2_node_b2_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node_b2_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_alt_0_ExtendAv2_node__node0 = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendAv2_node__node0", "_node0", ComplexMax_alt_0_ExtendAv2_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_alt_0_ExtendAv2_node__node1 = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendAv2_node__node1", "_node1", ComplexMax_alt_0_ExtendAv2_node__node1_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node__node1_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_alt_0_ExtendAv2_node__node2 = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendAv2_node__node2", "_node2", ComplexMax_alt_0_ExtendAv2_node__node2_AllowedTypes, ComplexMax_alt_0_ExtendAv2_node__node2_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv2_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendAv2_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv2_edge__edge1", "_edge1", ComplexMax_alt_0_ExtendAv2_edge__edge1_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge2 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv2_edge__edge2", "_edge2", ComplexMax_alt_0_ExtendAv2_edge__edge2_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge3 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv2_edge__edge3", "_edge3", ComplexMax_alt_0_ExtendAv2_edge__edge3_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge3_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendAv2_edge__edge4 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendAv2_edge__edge4", "_edge4", ComplexMax_alt_0_ExtendAv2_edge__edge4_AllowedTypes, ComplexMax_alt_0_ExtendAv2_edge__edge4_IsAllowedType, 5.5F, -1);
			ComplexMax_alt_0_ExtendAv2 = new PatternGraph(
				"ExtendAv2",
				"ComplexMax_alt_0_",
				false,
				new PatternNode[] { ComplexMax_node_a, ComplexMax_alt_0_ExtendAv2_node_b2, ComplexMax_node_b, ComplexMax_alt_0_ExtendAv2_node__node0, ComplexMax_alt_0_ExtendAv2_node__node1, ComplexMax_alt_0_ExtendAv2_node__node2 }, 
				new PatternEdge[] { ComplexMax_alt_0_ExtendAv2_edge__edge0, ComplexMax_alt_0_ExtendAv2_edge__edge1, ComplexMax_alt_0_ExtendAv2_edge__edge2, ComplexMax_alt_0_ExtendAv2_edge__edge3, ComplexMax_alt_0_ExtendAv2_edge__edge4 }, 
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
			PatternGraph ComplexMax_alt_0_ExtendNA2;
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
			PatternNode ComplexMax_alt_0_ExtendNA2_node__node0 = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendNA2_node__node0", "_node0", ComplexMax_alt_0_ExtendNA2_node__node0_AllowedTypes, ComplexMax_alt_0_ExtendNA2_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_alt_0_ExtendNA2_node__node1 = new PatternNode((int) NodeTypes.@C, "ComplexMax_alt_0_ExtendNA2_node__node1", "_node1", ComplexMax_alt_0_ExtendNA2_node__node1_AllowedTypes, ComplexMax_alt_0_ExtendNA2_node__node1_IsAllowedType, 5.5F, -1);
			PatternNode ComplexMax_alt_0_ExtendNA2_node_b2 = new PatternNode((int) NodeTypes.@B, "ComplexMax_alt_0_ExtendNA2_node_b2", "b2", ComplexMax_alt_0_ExtendNA2_node_b2_AllowedTypes, ComplexMax_alt_0_ExtendNA2_node_b2_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendNA2_edge__edge0", "_edge0", ComplexMax_alt_0_ExtendNA2_edge__edge0_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendNA2_edge__edge1", "_edge1", ComplexMax_alt_0_ExtendNA2_edge__edge1_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge2 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendNA2_edge__edge2", "_edge2", ComplexMax_alt_0_ExtendNA2_edge__edge2_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge2_IsAllowedType, 5.5F, -1);
			PatternEdge ComplexMax_alt_0_ExtendNA2_edge__edge3 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ComplexMax_alt_0_ExtendNA2_edge__edge3", "_edge3", ComplexMax_alt_0_ExtendNA2_edge__edge3_AllowedTypes, ComplexMax_alt_0_ExtendNA2_edge__edge3_IsAllowedType, 5.5F, -1);
			ComplexMax_alt_0_ExtendNA2 = new PatternGraph(
				"ExtendNA2",
				"ComplexMax_alt_0_",
				false,
				new PatternNode[] { ComplexMax_node_a, ComplexMax_alt_0_ExtendNA2_node__node0, ComplexMax_alt_0_ExtendNA2_node__node1, ComplexMax_node_b, ComplexMax_alt_0_ExtendNA2_node_b2 }, 
				new PatternEdge[] { ComplexMax_alt_0_ExtendNA2_edge__edge0, ComplexMax_alt_0_ExtendNA2_edge__edge1, ComplexMax_alt_0_ExtendNA2_edge__edge2, ComplexMax_alt_0_ExtendNA2_edge__edge3 }, 
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
			Alternative ComplexMax_alt_0 = new Alternative( "alt_0", "ComplexMax_", new PatternGraph[] { ComplexMax_alt_0_ExtendAv, ComplexMax_alt_0_ExtendAv2, ComplexMax_alt_0_ExtendNA2 } );

			pat_ComplexMax = new PatternGraph(
				"ComplexMax",
				"",
				false,
				new PatternNode[] { ComplexMax_node_a, ComplexMax_node_b }, 
				new PatternEdge[] { ComplexMax_edge__edge0, ComplexMax_edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ComplexMax_alt_0,  }, 
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

	public class Rule_createABA : LGSPRulePattern
	{
		private static Rule_createABA instance = null;
		public static Rule_createABA Instance { get { if (instance==null) { instance = new Rule_createABA(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_createABA;
			bool[,] createABA_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createABA_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createABA = new PatternGraph(
				"createABA",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createABA_isNodeHomomorphicGlobal,
				createABA_isEdgeHomomorphicGlobal
			);

			patternGraph = pat_createABA;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_A node_a = Node_A.CreateNode(graph);
			Node_B node_b = Node_B.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_A node_a = Node_A.CreateNode(graph);
			Node_B node_b = Node_B.CreateNode(graph);
			Edge_Edge edge__edge3 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node_b, node_a);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node_a, node_b);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "a", "b" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge3", "_edge2", "_edge1", "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_homm : LGSPRulePattern
	{
		private static Rule_homm instance = null;
		public static Rule_homm Instance { get { if (instance==null) { instance = new Rule_homm(); instance.initialize(); } return instance; } }

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
		public enum homm_alt_0_case1_NodeNums { @a, @b2, @b, };
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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_homm;
			bool[,] homm_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] homm_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternNode homm_node_a = new PatternNode((int) NodeTypes.@A, "homm_node_a", "a", homm_node_a_AllowedTypes, homm_node_a_IsAllowedType, 5.5F, -1);
			PatternNode homm_node_b = new PatternNode((int) NodeTypes.@B, "homm_node_b", "b", homm_node_b_AllowedTypes, homm_node_b_IsAllowedType, 5.5F, -1);
			PatternEdge homm_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "homm_edge__edge0", "_edge0", homm_edge__edge0_AllowedTypes, homm_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge homm_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "homm_edge__edge1", "_edge1", homm_edge__edge1_AllowedTypes, homm_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternGraph homm_alt_0_case1;
			bool[,] homm_alt_0_case1_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, true, },
				{ false, true, false, },
			};
			bool[,] homm_alt_0_case1_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternNode homm_alt_0_case1_node_b2 = new PatternNode((int) NodeTypes.@B, "homm_alt_0_case1_node_b2", "b2", homm_alt_0_case1_node_b2_AllowedTypes, homm_alt_0_case1_node_b2_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case1_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "homm_alt_0_case1_edge__edge0", "_edge0", homm_alt_0_case1_edge__edge0_AllowedTypes, homm_alt_0_case1_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case1_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "homm_alt_0_case1_edge__edge1", "_edge1", homm_alt_0_case1_edge__edge1_AllowedTypes, homm_alt_0_case1_edge__edge1_IsAllowedType, 5.5F, -1);
			homm_alt_0_case1 = new PatternGraph(
				"case1",
				"homm_alt_0_",
				false,
				new PatternNode[] { homm_node_a, homm_alt_0_case1_node_b2, homm_node_b }, 
				new PatternEdge[] { homm_alt_0_case1_edge__edge0, homm_alt_0_case1_edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			PatternGraph homm_alt_0_case2;
			bool[,] homm_alt_0_case2_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] homm_alt_0_case2_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternNode homm_alt_0_case2_node_b2 = new PatternNode((int) NodeTypes.@B, "homm_alt_0_case2_node_b2", "b2", homm_alt_0_case2_node_b2_AllowedTypes, homm_alt_0_case2_node_b2_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case2_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "homm_alt_0_case2_edge__edge0", "_edge0", homm_alt_0_case2_edge__edge0_AllowedTypes, homm_alt_0_case2_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge homm_alt_0_case2_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "homm_alt_0_case2_edge__edge1", "_edge1", homm_alt_0_case2_edge__edge1_AllowedTypes, homm_alt_0_case2_edge__edge1_IsAllowedType, 5.5F, -1);
			homm_alt_0_case2 = new PatternGraph(
				"case2",
				"homm_alt_0_",
				false,
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
				homm_alt_0_case2_isNodeHomomorphicGlobal,
				homm_alt_0_case2_isEdgeHomomorphicGlobal
			);
			homm_alt_0_case2.edgeToSourceNode.Add(homm_alt_0_case2_edge__edge0, homm_node_a);
			homm_alt_0_case2.edgeToTargetNode.Add(homm_alt_0_case2_edge__edge0, homm_alt_0_case2_node_b2);
			homm_alt_0_case2.edgeToSourceNode.Add(homm_alt_0_case2_edge__edge1, homm_alt_0_case2_node_b2);
			homm_alt_0_case2.edgeToTargetNode.Add(homm_alt_0_case2_edge__edge1, homm_node_a);
			Alternative homm_alt_0 = new Alternative( "alt_0", "homm_", new PatternGraph[] { homm_alt_0_case1, homm_alt_0_case2 } );

			pat_homm = new PatternGraph(
				"homm",
				"",
				false,
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
		public static Rule_XtoAorB Instance { get { if (instance==null) { instance = new Rule_XtoAorB(); instance.initialize(); } return instance; } }

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

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_XtoAorB;
			bool[,] XtoAorB_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] XtoAorB_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode XtoAorB_node_x = new PatternNode((int) NodeTypes.@Node, "XtoAorB_node_x", "x", XtoAorB_node_x_AllowedTypes, XtoAorB_node_x_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding XtoAorB__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_toAorB.Instance, new PatternElement[] { XtoAorB_node_x });
			pat_XtoAorB = new PatternGraph(
				"XtoAorB",
				"",
				false,
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
				XtoAorB_isNodeHomomorphicGlobal,
				XtoAorB_isEdgeHomomorphicGlobal
			);
			XtoAorB_node_x.PointOfDefinition = pat_XtoAorB;
			XtoAorB__subpattern0.PointOfDefinition = pat_XtoAorB;

			patternGraph = pat_XtoAorB;
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
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset toAorB_node_x 
            LGSPNode candidate_toAorB_node_x = toAorB_node_x;
            // Extend Outgoing toAorB_edge_y from toAorB_node_x 
            LGSPEdge head_candidate_toAorB_edge_y = candidate_toAorB_node_x.outhead;
            if(head_candidate_toAorB_edge_y != null)
            {
                LGSPEdge candidate_toAorB_edge_y = head_candidate_toAorB_edge_y;
                do
                {
                    if(!EdgeType_Edge.isMyType[candidate_toAorB_edge_y.type.TypeID]) {
                        continue;
                    }
                    if((candidate_toAorB_edge_y.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                    {
                        continue;
                    }
                    // Push alternative matching task for toAorB_alt_0
                    AlternativeAction_toAorB_alt_0 taskFor_alt_0 = new AlternativeAction_toAorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Pattern_toAorB.toAorB_AltNums.@alt_0].alternativeCases);
                    taskFor_alt_0.toAorB_edge_y = candidate_toAorB_edge_y;
                    openTasks.Push(taskFor_alt_0);
                    uint prevGlobal__candidate_toAorB_edge_y;
                    prevGlobal__candidate_toAorB_edge_y = candidate_toAorB_edge_y.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_toAorB_edge_y.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for alt_0
                    openTasks.Pop();
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0+1]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_toAorB.toAorB_NodeNums.@x] = candidate_toAorB_node_x;
                            match.Edges[(int)Pattern_toAorB.toAorB_EdgeNums.@y] = candidate_toAorB_edge_y;
                            match.EmbeddedGraphs[((int)Pattern_toAorB.toAorB_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_toAorB_edge_y.flags = candidate_toAorB_edge_y.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_edge_y;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_toAorB_edge_y.flags = candidate_toAorB_edge_y.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_edge_y;
                        continue;
                    }
                    candidate_toAorB_edge_y.flags = candidate_toAorB_edge_y.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_edge_y;
                }
                while( (candidate_toAorB_edge_y = candidate_toAorB_edge_y.outNext) != head_candidate_toAorB_edge_y );
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
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case toAorB_alt_0_toA 
            do {
                patternGraph = patternGraphs[(int)Pattern_toAorB.toAorB_alt_0_CaseNums.@toA];
                // SubPreset toAorB_edge_y 
                LGSPEdge candidate_toAorB_edge_y = toAorB_edge_y;
                // Implicit Target toAorB_alt_0_toA_node_a from toAorB_edge_y 
                LGSPNode candidate_toAorB_alt_0_toA_node_a = candidate_toAorB_edge_y.target;
                if(!NodeType_A.isMyType[candidate_toAorB_alt_0_toA_node_a.type.TypeID]) {
                    continue;
                }
                if((candidate_toAorB_alt_0_toA_node_a.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                    match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toA_NodeNums.@a] = candidate_toAorB_alt_0_toA_node_a;
                    match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toA_EdgeNums.@y] = candidate_toAorB_edge_y;
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
                prevGlobal__candidate_toAorB_alt_0_toA_node_a = candidate_toAorB_alt_0_toA_node_a.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_toAorB_alt_0_toA_node_a.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toA_NodeNums.@a] = candidate_toAorB_alt_0_toA_node_a;
                        match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toA_EdgeNums.@y] = candidate_toAorB_edge_y;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
                            foundPartialMatches.Add(match);
                        }
                        matchesList.Clear();
                    }
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        candidate_toAorB_alt_0_toA_node_a.flags = candidate_toAorB_alt_0_toA_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toA_node_a;
                        openTasks.Push(this);
                        return;
                    }
                    candidate_toAorB_alt_0_toA_node_a.flags = candidate_toAorB_alt_0_toA_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toA_node_a;
                    continue;
                }
                candidate_toAorB_alt_0_toA_node_a.flags = candidate_toAorB_alt_0_toA_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toA_node_a;
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case toAorB_alt_0_toB 
            do {
                patternGraph = patternGraphs[(int)Pattern_toAorB.toAorB_alt_0_CaseNums.@toB];
                // SubPreset toAorB_edge_y 
                LGSPEdge candidate_toAorB_edge_y = toAorB_edge_y;
                // Implicit Target toAorB_alt_0_toB_node_b from toAorB_edge_y 
                LGSPNode candidate_toAorB_alt_0_toB_node_b = candidate_toAorB_edge_y.target;
                if(!NodeType_B.isMyType[candidate_toAorB_alt_0_toB_node_b.type.TypeID]) {
                    continue;
                }
                if((candidate_toAorB_alt_0_toB_node_b.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                    match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toB_NodeNums.@b] = candidate_toAorB_alt_0_toB_node_b;
                    match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toB_EdgeNums.@y] = candidate_toAorB_edge_y;
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
                prevGlobal__candidate_toAorB_alt_0_toB_node_b = candidate_toAorB_alt_0_toB_node_b.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_toAorB_alt_0_toB_node_b.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[1], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_toAorB.toAorB_alt_0_toB_NodeNums.@b] = candidate_toAorB_alt_0_toB_node_b;
                        match.Edges[(int)Pattern_toAorB.toAorB_alt_0_toB_EdgeNums.@y] = candidate_toAorB_edge_y;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
                            foundPartialMatches.Add(match);
                        }
                        matchesList.Clear();
                    }
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        candidate_toAorB_alt_0_toB_node_b.flags = candidate_toAorB_alt_0_toB_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toB_node_b;
                        openTasks.Push(this);
                        return;
                    }
                    candidate_toAorB_alt_0_toB_node_b.flags = candidate_toAorB_alt_0_toB_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toB_node_b;
                    continue;
                }
                candidate_toAorB_alt_0_toB_node_b.flags = candidate_toAorB_alt_0_toB_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_toAorB_alt_0_toB_node_b;
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_createA : LGSPAction
    {
        public Action_createA() {
            rulePattern = Rule_createA.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createA"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createA instance = new Action_createA();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
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
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createB instance = new Action_createB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
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
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createC"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createC instance = new Action_createC();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
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
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createAtoB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createAtoB instance = new Action_createAtoB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
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

    public class Action_leer : LGSPAction
    {
        public Action_leer() {
            rulePattern = Rule_leer.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+1);
        }

        public override string Name { get { return "leer"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_leer instance = new Action_leer();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for leer_alt_0
            AlternativeAction_leer_alt_0 taskFor_alt_0 = new AlternativeAction_leer_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_leer.leer_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.EmbeddedGraphs[((int)Rule_leer.leer_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
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

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case leer_alt_0_altleer 
            do {
                patternGraph = patternGraphs[(int)Rule_leer.leer_alt_0_CaseNums.@altleer];
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
                    continue;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[0], new LGSPEdge[0], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class Action_AorB : LGSPAction
    {
        public Action_AorB() {
            rulePattern = Rule_AorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+1);
        }

        public override string Name { get { return "AorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AorB instance = new Action_AorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AorB_alt_0
            AlternativeAction_AorB_alt_0 taskFor_alt_0 = new AlternativeAction_AorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AorB.AorB_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.EmbeddedGraphs[((int)Rule_AorB.AorB_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
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

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AorB_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AorB.AorB_alt_0_CaseNums.@A];
                // Lookup AorB_alt_0_A_node__node0 
                int type_id_candidate_AorB_alt_0_A_node__node0 = 1;
                for(LGSPNode head_candidate_AorB_alt_0_A_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorB_alt_0_A_node__node0], candidate_AorB_alt_0_A_node__node0 = head_candidate_AorB_alt_0_A_node__node0.typeNext; candidate_AorB_alt_0_A_node__node0 != head_candidate_AorB_alt_0_A_node__node0; candidate_AorB_alt_0_A_node__node0 = candidate_AorB_alt_0_A_node__node0.typeNext)
                {
                    if((candidate_AorB_alt_0_A_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        match.Nodes[(int)Rule_AorB.AorB_alt_0_A_NodeNums.@_node0] = candidate_AorB_alt_0_A_node__node0;
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
                    prevGlobal__candidate_AorB_alt_0_A_node__node0 = candidate_AorB_alt_0_A_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorB_alt_0_A_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorB.AorB_alt_0_A_NodeNums.@_node0] = candidate_AorB_alt_0_A_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_AorB_alt_0_A_node__node0.flags = candidate_AorB_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_A_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorB_alt_0_A_node__node0.flags = candidate_AorB_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_A_node__node0;
                        continue;
                    }
                    candidate_AorB_alt_0_A_node__node0.flags = candidate_AorB_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_A_node__node0;
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case AorB_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AorB.AorB_alt_0_CaseNums.@B];
                // Lookup AorB_alt_0_B_node__node0 
                int type_id_candidate_AorB_alt_0_B_node__node0 = 2;
                for(LGSPNode head_candidate_AorB_alt_0_B_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorB_alt_0_B_node__node0], candidate_AorB_alt_0_B_node__node0 = head_candidate_AorB_alt_0_B_node__node0.typeNext; candidate_AorB_alt_0_B_node__node0 != head_candidate_AorB_alt_0_B_node__node0; candidate_AorB_alt_0_B_node__node0 = candidate_AorB_alt_0_B_node__node0.typeNext)
                {
                    if((candidate_AorB_alt_0_B_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        match.Nodes[(int)Rule_AorB.AorB_alt_0_B_NodeNums.@_node0] = candidate_AorB_alt_0_B_node__node0;
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
                    prevGlobal__candidate_AorB_alt_0_B_node__node0 = candidate_AorB_alt_0_B_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorB_alt_0_B_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorB.AorB_alt_0_B_NodeNums.@_node0] = candidate_AorB_alt_0_B_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_AorB_alt_0_B_node__node0.flags = candidate_AorB_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_B_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorB_alt_0_B_node__node0.flags = candidate_AorB_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_B_node__node0;
                        continue;
                    }
                    candidate_AorB_alt_0_B_node__node0.flags = candidate_AorB_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorB_alt_0_B_node__node0;
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
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+1);
        }

        public override string Name { get { return "AandnotCorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AandnotCorB instance = new Action_AandnotCorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AandnotCorB_alt_0
            AlternativeAction_AandnotCorB_alt_0 taskFor_alt_0 = new AlternativeAction_AandnotCorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AandnotCorB.AandnotCorB_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.EmbeddedGraphs[((int)Rule_AandnotCorB.AandnotCorB_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
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

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AandnotCorB_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AandnotCorB.AandnotCorB_alt_0_CaseNums.@A];
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > MAX_NEG_LEVEL && negLevel-MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst = new Dictionary<LGSPNode, LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd = new Dictionary<LGSPEdge, LGSPEdge>();
                    }
                    // Lookup AandnotCorB_alt_0_A_neg_0_node__node0 
                    int type_id_candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = 3;
                    for(LGSPNode head_candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AandnotCorB_alt_0_A_neg_0_node__node0], candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = head_candidate_AandnotCorB_alt_0_A_neg_0_node__node0.typeNext; candidate_AandnotCorB_alt_0_A_neg_0_node__node0 != head_candidate_AandnotCorB_alt_0_A_neg_0_node__node0; candidate_AandnotCorB_alt_0_A_neg_0_node__node0 = candidate_AandnotCorB_alt_0_A_neg_0_node__node0.typeNext)
                    {
                        if((candidate_AandnotCorB_alt_0_A_neg_0_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // negative pattern found
                        if(negLevel > MAX_NEG_LEVEL) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                        }
                        --negLevel;
                        goto label0;
                    }
                    if(negLevel > MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                    }
                    --negLevel;
                }
                // Lookup AandnotCorB_alt_0_A_node__node0 
                int type_id_candidate_AandnotCorB_alt_0_A_node__node0 = 1;
                for(LGSPNode head_candidate_AandnotCorB_alt_0_A_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AandnotCorB_alt_0_A_node__node0], candidate_AandnotCorB_alt_0_A_node__node0 = head_candidate_AandnotCorB_alt_0_A_node__node0.typeNext; candidate_AandnotCorB_alt_0_A_node__node0 != head_candidate_AandnotCorB_alt_0_A_node__node0; candidate_AandnotCorB_alt_0_A_node__node0 = candidate_AandnotCorB_alt_0_A_node__node0.typeNext)
                {
                    if((candidate_AandnotCorB_alt_0_A_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_A_NodeNums.@_node0] = candidate_AandnotCorB_alt_0_A_node__node0;
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
                    prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0 = candidate_AandnotCorB_alt_0_A_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AandnotCorB_alt_0_A_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_A_NodeNums.@_node0] = candidate_AandnotCorB_alt_0_A_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_AandnotCorB_alt_0_A_node__node0.flags = candidate_AandnotCorB_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AandnotCorB_alt_0_A_node__node0.flags = candidate_AandnotCorB_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0;
                        continue;
                    }
                    candidate_AandnotCorB_alt_0_A_node__node0.flags = candidate_AandnotCorB_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_A_node__node0;
                }
label0: ;
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case AandnotCorB_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AandnotCorB.AandnotCorB_alt_0_CaseNums.@B];
                // Lookup AandnotCorB_alt_0_B_node__node0 
                int type_id_candidate_AandnotCorB_alt_0_B_node__node0 = 2;
                for(LGSPNode head_candidate_AandnotCorB_alt_0_B_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AandnotCorB_alt_0_B_node__node0], candidate_AandnotCorB_alt_0_B_node__node0 = head_candidate_AandnotCorB_alt_0_B_node__node0.typeNext; candidate_AandnotCorB_alt_0_B_node__node0 != head_candidate_AandnotCorB_alt_0_B_node__node0; candidate_AandnotCorB_alt_0_B_node__node0 = candidate_AandnotCorB_alt_0_B_node__node0.typeNext)
                {
                    if((candidate_AandnotCorB_alt_0_B_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_B_NodeNums.@_node0] = candidate_AandnotCorB_alt_0_B_node__node0;
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
                    prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0 = candidate_AandnotCorB_alt_0_B_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AandnotCorB_alt_0_B_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AandnotCorB.AandnotCorB_alt_0_B_NodeNums.@_node0] = candidate_AandnotCorB_alt_0_B_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_AandnotCorB_alt_0_B_node__node0.flags = candidate_AandnotCorB_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AandnotCorB_alt_0_B_node__node0.flags = candidate_AandnotCorB_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0;
                        continue;
                    }
                    candidate_AandnotCorB_alt_0_B_node__node0.flags = candidate_AandnotCorB_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AandnotCorB_alt_0_B_node__node0;
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
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+1);
        }

        public override string Name { get { return "AorBorC"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AorBorC instance = new Action_AorBorC();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Push alternative matching task for AorBorC_alt_0
            AlternativeAction_AorBorC_alt_0 taskFor_alt_0 = new AlternativeAction_AorBorC_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AorBorC.AorBorC_AltNums.@alt_0].alternativeCases);
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.EmbeddedGraphs[((int)Rule_AorBorC.AorBorC_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
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

        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AorBorC_alt_0_A 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@A];
                // Lookup AorBorC_alt_0_A_node__node0 
                int type_id_candidate_AorBorC_alt_0_A_node__node0 = 1;
                for(LGSPNode head_candidate_AorBorC_alt_0_A_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorBorC_alt_0_A_node__node0], candidate_AorBorC_alt_0_A_node__node0 = head_candidate_AorBorC_alt_0_A_node__node0.typeNext; candidate_AorBorC_alt_0_A_node__node0 != head_candidate_AorBorC_alt_0_A_node__node0; candidate_AorBorC_alt_0_A_node__node0 = candidate_AorBorC_alt_0_A_node__node0.typeNext)
                {
                    if((candidate_AorBorC_alt_0_A_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_A_NodeNums.@_node0] = candidate_AorBorC_alt_0_A_node__node0;
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
                    prevGlobal__candidate_AorBorC_alt_0_A_node__node0 = candidate_AorBorC_alt_0_A_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorBorC_alt_0_A_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_A_NodeNums.@_node0] = candidate_AorBorC_alt_0_A_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_AorBorC_alt_0_A_node__node0.flags = candidate_AorBorC_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_A_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorBorC_alt_0_A_node__node0.flags = candidate_AorBorC_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_A_node__node0;
                        continue;
                    }
                    candidate_AorBorC_alt_0_A_node__node0.flags = candidate_AorBorC_alt_0_A_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_A_node__node0;
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case AorBorC_alt_0_B 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@B];
                // Lookup AorBorC_alt_0_B_node__node0 
                int type_id_candidate_AorBorC_alt_0_B_node__node0 = 2;
                for(LGSPNode head_candidate_AorBorC_alt_0_B_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorBorC_alt_0_B_node__node0], candidate_AorBorC_alt_0_B_node__node0 = head_candidate_AorBorC_alt_0_B_node__node0.typeNext; candidate_AorBorC_alt_0_B_node__node0 != head_candidate_AorBorC_alt_0_B_node__node0; candidate_AorBorC_alt_0_B_node__node0 = candidate_AorBorC_alt_0_B_node__node0.typeNext)
                {
                    if((candidate_AorBorC_alt_0_B_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_B_NodeNums.@_node0] = candidate_AorBorC_alt_0_B_node__node0;
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
                    prevGlobal__candidate_AorBorC_alt_0_B_node__node0 = candidate_AorBorC_alt_0_B_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorBorC_alt_0_B_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_B_NodeNums.@_node0] = candidate_AorBorC_alt_0_B_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_AorBorC_alt_0_B_node__node0.flags = candidate_AorBorC_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_B_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorBorC_alt_0_B_node__node0.flags = candidate_AorBorC_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_B_node__node0;
                        continue;
                    }
                    candidate_AorBorC_alt_0_B_node__node0.flags = candidate_AorBorC_alt_0_B_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_B_node__node0;
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case AorBorC_alt_0_C 
            do {
                patternGraph = patternGraphs[(int)Rule_AorBorC.AorBorC_alt_0_CaseNums.@C];
                // Lookup AorBorC_alt_0_C_node__node0 
                int type_id_candidate_AorBorC_alt_0_C_node__node0 = 3;
                for(LGSPNode head_candidate_AorBorC_alt_0_C_node__node0 = graph.nodesByTypeHeads[type_id_candidate_AorBorC_alt_0_C_node__node0], candidate_AorBorC_alt_0_C_node__node0 = head_candidate_AorBorC_alt_0_C_node__node0.typeNext; candidate_AorBorC_alt_0_C_node__node0 != head_candidate_AorBorC_alt_0_C_node__node0; candidate_AorBorC_alt_0_C_node__node0 = candidate_AorBorC_alt_0_C_node__node0.typeNext)
                {
                    if((candidate_AorBorC_alt_0_C_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                        match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_C_NodeNums.@_node0] = candidate_AorBorC_alt_0_C_node__node0;
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
                    prevGlobal__candidate_AorBorC_alt_0_C_node__node0 = candidate_AorBorC_alt_0_C_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_AorBorC_alt_0_C_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new LGSPMatch[0+0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Rule_AorBorC.AorBorC_alt_0_C_NodeNums.@_node0] = candidate_AorBorC_alt_0_C_node__node0;
                            currentFoundPartialMatch.Push(match);
                        }
                        if(matchesList==foundPartialMatches) {
                            matchesList = new List<Stack<LGSPMatch>>();
                        } else {
                            foreach(Stack<LGSPMatch> match in matchesList) {
                                foundPartialMatches.Add(match);
                            }
                            matchesList.Clear();
                        }
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                        {
                            candidate_AorBorC_alt_0_C_node__node0.flags = candidate_AorBorC_alt_0_C_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_C_node__node0;
                            openTasks.Push(this);
                            return;
                        }
                        candidate_AorBorC_alt_0_C_node__node0.flags = candidate_AorBorC_alt_0_C_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_C_node__node0;
                        continue;
                    }
                    candidate_AorBorC_alt_0_C_node__node0.flags = candidate_AorBorC_alt_0_C_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AorBorC_alt_0_C_node__node0;
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
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 0+1);
        }

        public override string Name { get { return "AtoAorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_AtoAorB instance = new Action_AtoAorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup AtoAorB_node_a 
            int type_id_candidate_AtoAorB_node_a = 1;
            for(LGSPNode head_candidate_AtoAorB_node_a = graph.nodesByTypeHeads[type_id_candidate_AtoAorB_node_a], candidate_AtoAorB_node_a = head_candidate_AtoAorB_node_a.typeNext; candidate_AtoAorB_node_a != head_candidate_AtoAorB_node_a; candidate_AtoAorB_node_a = candidate_AtoAorB_node_a.typeNext)
            {
                // Push alternative matching task for AtoAorB_alt_0
                AlternativeAction_AtoAorB_alt_0 taskFor_alt_0 = new AlternativeAction_AtoAorB_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_AtoAorB.AtoAorB_AltNums.@alt_0].alternativeCases);
                taskFor_alt_0.AtoAorB_node_a = candidate_AtoAorB_node_a;
                openTasks.Push(taskFor_alt_0);
                uint prevGlobal__candidate_AtoAorB_node_a;
                prevGlobal__candidate_AtoAorB_node_a = candidate_AtoAorB_node_a.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_AtoAorB_node_a.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for alt_0
                openTasks.Pop();
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_AtoAorB.AtoAorB_NodeNums.@a] = candidate_AtoAorB_node_a;
                        match.EmbeddedGraphs[((int)Rule_AtoAorB.AtoAorB_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_AtoAorB_node_a.flags = candidate_AtoAorB_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_node_a;
                        return matches;
                    }
                    candidate_AtoAorB_node_a.flags = candidate_AtoAorB_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_node_a;
                    continue;
                }
                candidate_AtoAorB_node_a.flags = candidate_AtoAorB_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_node_a;
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
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case AtoAorB_alt_0_toA 
            do {
                patternGraph = patternGraphs[(int)Rule_AtoAorB.AtoAorB_alt_0_CaseNums.@toA];
                // SubPreset AtoAorB_node_a 
                LGSPNode candidate_AtoAorB_node_a = AtoAorB_node_a;
                // Extend Outgoing AtoAorB_alt_0_toA_edge__edge0 from AtoAorB_node_a 
                LGSPEdge head_candidate_AtoAorB_alt_0_toA_edge__edge0 = candidate_AtoAorB_node_a.outhead;
                if(head_candidate_AtoAorB_alt_0_toA_edge__edge0 != null)
                {
                    LGSPEdge candidate_AtoAorB_alt_0_toA_edge__edge0 = head_candidate_AtoAorB_alt_0_toA_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_AtoAorB_alt_0_toA_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toA_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target AtoAorB_alt_0_toA_node__node0 from AtoAorB_alt_0_toA_edge__edge0 
                        LGSPNode candidate_AtoAorB_alt_0_toA_node__node0 = candidate_AtoAorB_alt_0_toA_edge__edge0.target;
                        if(!NodeType_A.isMyType[candidate_AtoAorB_alt_0_toA_node__node0.type.TypeID]) {
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_AtoAorB_alt_0_toA_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_AtoAorB_alt_0_toA_node__node0))
                            && candidate_AtoAorB_alt_0_toA_node__node0==candidate_AtoAorB_node_a
                            )
                        {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toA_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@a] = candidate_AtoAorB_node_a;
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@_node0] = candidate_AtoAorB_alt_0_toA_node__node0;
                            match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_EdgeNums.@_edge0] = candidate_AtoAorB_alt_0_toA_edge__edge0;
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
                        prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0 = candidate_AtoAorB_alt_0_toA_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toA_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                        prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0 = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toA_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@a] = candidate_AtoAorB_node_a;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_NodeNums.@_node0] = candidate_AtoAorB_alt_0_toA_node__node0;
                                match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toA_EdgeNums.@_edge0] = candidate_AtoAorB_alt_0_toA_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_AtoAorB_alt_0_toA_edge__edge0.flags = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                                candidate_AtoAorB_alt_0_toA_node__node0.flags = candidate_AtoAorB_alt_0_toA_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_AtoAorB_alt_0_toA_edge__edge0.flags = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                            candidate_AtoAorB_alt_0_toA_node__node0.flags = candidate_AtoAorB_alt_0_toA_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0;
                            continue;
                        }
                        candidate_AtoAorB_alt_0_toA_node__node0.flags = candidate_AtoAorB_alt_0_toA_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_node__node0;
                        candidate_AtoAorB_alt_0_toA_edge__edge0.flags = candidate_AtoAorB_alt_0_toA_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toA_edge__edge0;
                    }
                    while( (candidate_AtoAorB_alt_0_toA_edge__edge0 = candidate_AtoAorB_alt_0_toA_edge__edge0.outNext) != head_candidate_AtoAorB_alt_0_toA_edge__edge0 );
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case AtoAorB_alt_0_toB 
            do {
                patternGraph = patternGraphs[(int)Rule_AtoAorB.AtoAorB_alt_0_CaseNums.@toB];
                // SubPreset AtoAorB_node_a 
                LGSPNode candidate_AtoAorB_node_a = AtoAorB_node_a;
                // Extend Outgoing AtoAorB_alt_0_toB_edge__edge0 from AtoAorB_node_a 
                LGSPEdge head_candidate_AtoAorB_alt_0_toB_edge__edge0 = candidate_AtoAorB_node_a.outhead;
                if(head_candidate_AtoAorB_alt_0_toB_edge__edge0 != null)
                {
                    LGSPEdge candidate_AtoAorB_alt_0_toB_edge__edge0 = head_candidate_AtoAorB_alt_0_toB_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_AtoAorB_alt_0_toB_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toB_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target AtoAorB_alt_0_toB_node__node0 from AtoAorB_alt_0_toB_edge__edge0 
                        LGSPNode candidate_AtoAorB_alt_0_toB_node__node0 = candidate_AtoAorB_alt_0_toB_edge__edge0.target;
                        if(!NodeType_B.isMyType[candidate_AtoAorB_alt_0_toB_node__node0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_AtoAorB_alt_0_toB_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@a] = candidate_AtoAorB_node_a;
                            match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@_node0] = candidate_AtoAorB_alt_0_toB_node__node0;
                            match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_EdgeNums.@_edge0] = candidate_AtoAorB_alt_0_toB_edge__edge0;
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
                        prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0 = candidate_AtoAorB_alt_0_toB_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toB_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                        prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0 = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_AtoAorB_alt_0_toB_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@a] = candidate_AtoAorB_node_a;
                                match.Nodes[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_NodeNums.@_node0] = candidate_AtoAorB_alt_0_toB_node__node0;
                                match.Edges[(int)Rule_AtoAorB.AtoAorB_alt_0_toB_EdgeNums.@_edge0] = candidate_AtoAorB_alt_0_toB_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_AtoAorB_alt_0_toB_edge__edge0.flags = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                                candidate_AtoAorB_alt_0_toB_node__node0.flags = candidate_AtoAorB_alt_0_toB_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_AtoAorB_alt_0_toB_edge__edge0.flags = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                            candidate_AtoAorB_alt_0_toB_node__node0.flags = candidate_AtoAorB_alt_0_toB_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0;
                            continue;
                        }
                        candidate_AtoAorB_alt_0_toB_node__node0.flags = candidate_AtoAorB_alt_0_toB_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_node__node0;
                        candidate_AtoAorB_alt_0_toB_edge__edge0.flags = candidate_AtoAorB_alt_0_toB_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_AtoAorB_alt_0_toB_edge__edge0;
                    }
                    while( (candidate_AtoAorB_alt_0_toB_edge__edge0 = candidate_AtoAorB_alt_0_toB_edge__edge0.outNext) != head_candidate_AtoAorB_alt_0_toB_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_createComplex : LGSPAction
    {
        public Action_createComplex() {
            rulePattern = Rule_createComplex.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createComplex"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createComplex instance = new Action_createComplex();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
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

    public class Action_Complex : LGSPAction
    {
        public Action_Complex() {
            rulePattern = Rule_Complex.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0+1);
        }

        public override string Name { get { return "Complex"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_Complex instance = new Action_Complex();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup Complex_edge__edge0 
            int type_id_candidate_Complex_edge__edge0 = 1;
            for(LGSPEdge head_candidate_Complex_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_Complex_edge__edge0], candidate_Complex_edge__edge0 = head_candidate_Complex_edge__edge0.typeNext; candidate_Complex_edge__edge0 != head_candidate_Complex_edge__edge0; candidate_Complex_edge__edge0 = candidate_Complex_edge__edge0.typeNext)
            {
                uint prev__candidate_Complex_edge__edge0;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_Complex_edge__edge0 = candidate_Complex_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                    candidate_Complex_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_Complex_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_edge__edge0) ? 1U : 0U;
                    if(prev__candidate_Complex_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_edge__edge0,candidate_Complex_edge__edge0);
                }
                // Implicit Source Complex_node_a from Complex_edge__edge0 
                LGSPNode candidate_Complex_node_a = candidate_Complex_edge__edge0.source;
                if(!NodeType_A.isMyType[candidate_Complex_node_a.type.TypeID]) {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_edge__edge0;
                    } else { 
                        if(prev__candidate_Complex_edge__edge0==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_edge__edge0);
                        }
                    }
                    continue;
                }
                // Implicit Target Complex_node_b from Complex_edge__edge0 
                LGSPNode candidate_Complex_node_b = candidate_Complex_edge__edge0.target;
                if(!NodeType_B.isMyType[candidate_Complex_node_b.type.TypeID]) {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_edge__edge0;
                    } else { 
                        if(prev__candidate_Complex_edge__edge0==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_edge__edge0);
                        }
                    }
                    continue;
                }
                // Extend Outgoing Complex_edge__edge1 from Complex_node_b 
                LGSPEdge head_candidate_Complex_edge__edge1 = candidate_Complex_node_b.outhead;
                if(head_candidate_Complex_edge__edge1 != null)
                {
                    LGSPEdge candidate_Complex_edge__edge1 = head_candidate_Complex_edge__edge1;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_Complex_edge__edge1.type.TypeID]) {
                            continue;
                        }
                        if(candidate_Complex_edge__edge1.target != candidate_Complex_node_a) {
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_edge__edge1))
                            && candidate_Complex_edge__edge1==candidate_Complex_edge__edge0
                            )
                        {
                            continue;
                        }
                        // Push alternative matching task for Complex_alt_0
                        AlternativeAction_Complex_alt_0 taskFor_alt_0 = new AlternativeAction_Complex_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_Complex.Complex_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.Complex_node_a = candidate_Complex_node_a;
                        taskFor_alt_0.Complex_node_b = candidate_Complex_node_b;
                        openTasks.Push(taskFor_alt_0);
                        uint prevGlobal__candidate_Complex_node_a;
                        prevGlobal__candidate_Complex_node_a = candidate_Complex_node_a.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_node_a.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Complex_node_b;
                        prevGlobal__candidate_Complex_node_b = candidate_Complex_node_b.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_node_b.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Complex_edge__edge0;
                        prevGlobal__candidate_Complex_edge__edge0 = candidate_Complex_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Complex_edge__edge1;
                        prevGlobal__candidate_Complex_edge__edge1 = candidate_Complex_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Complex_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for alt_0
                        openTasks.Pop();
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_Complex.Complex_NodeNums.@a] = candidate_Complex_node_a;
                                match.Nodes[(int)Rule_Complex.Complex_NodeNums.@b] = candidate_Complex_node_b;
                                match.Edges[(int)Rule_Complex.Complex_EdgeNums.@_edge0] = candidate_Complex_edge__edge0;
                                match.Edges[(int)Rule_Complex.Complex_EdgeNums.@_edge1] = candidate_Complex_edge__edge1;
                                match.EmbeddedGraphs[((int)Rule_Complex.Complex_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                                matches.matchesList.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                            {
                                candidate_Complex_edge__edge1.flags = candidate_Complex_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge1;
                                candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge0;
                                candidate_Complex_node_b.flags = candidate_Complex_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_b;
                                candidate_Complex_node_a.flags = candidate_Complex_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_a;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_edge__edge0;
                                } else { 
                                    if(prev__candidate_Complex_edge__edge0==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_edge__edge0);
                                    }
                                }
                                return matches;
                            }
                            candidate_Complex_edge__edge1.flags = candidate_Complex_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge1;
                            candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge0;
                            candidate_Complex_node_b.flags = candidate_Complex_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_b;
                            candidate_Complex_node_a.flags = candidate_Complex_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_a;
                            continue;
                        }
                        candidate_Complex_node_a.flags = candidate_Complex_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_a;
                        candidate_Complex_node_b.flags = candidate_Complex_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_node_b;
                        candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge0;
                        candidate_Complex_edge__edge1.flags = candidate_Complex_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_edge__edge1;
                    }
                    while( (candidate_Complex_edge__edge1 = candidate_Complex_edge__edge1.outNext) != head_candidate_Complex_edge__edge1 );
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_Complex_edge__edge0.flags = candidate_Complex_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_edge__edge0;
                } else { 
                    if(prev__candidate_Complex_edge__edge0==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_edge__edge0);
                    }
                }
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
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Complex_alt_0_ExtendAv 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendAv];
                // SubPreset Complex_node_a 
                LGSPNode candidate_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                LGSPNode candidate_Complex_node_b = Complex_node_b;
                // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge0 from Complex_node_a 
                LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_node_a.outhead;
                if(head_candidate_Complex_alt_0_ExtendAv_edge__edge0 != null)
                {
                    LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge0 = head_candidate_Complex_alt_0_ExtendAv_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendAv_edge__edge0,candidate_Complex_alt_0_ExtendAv_edge__edge0);
                        }
                        // Implicit Target Complex_alt_0_ExtendAv_node_b2 from Complex_alt_0_ExtendAv_edge__edge0 
                        LGSPNode candidate_Complex_alt_0_ExtendAv_node_b2 = candidate_Complex_alt_0_ExtendAv_edge__edge0.target;
                        if(!NodeType_B.isMyType[candidate_Complex_alt_0_ExtendAv_node_b2.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_node_b2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv_node_b2))
                            && candidate_Complex_alt_0_ExtendAv_node_b2==candidate_Complex_node_b
                            )
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge2 from Complex_node_b 
                        LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_node_b.outhead;
                        if(head_candidate_Complex_alt_0_ExtendAv_edge__edge2 != null)
                        {
                            LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge2 = head_candidate_Complex_alt_0_ExtendAv_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge2))
                                    && candidate_Complex_alt_0_ExtendAv_edge__edge2==candidate_Complex_alt_0_ExtendAv_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                    candidate_Complex_alt_0_ExtendAv_edge__edge2.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv_edge__edge2 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendAv_edge__edge2,candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                }
                                // Implicit Target Complex_alt_0_ExtendAv_node__node0 from Complex_alt_0_ExtendAv_edge__edge2 
                                LGSPNode candidate_Complex_alt_0_ExtendAv_node__node0 = candidate_Complex_alt_0_ExtendAv_edge__edge2.target;
                                if(!NodeType_C.isMyType[candidate_Complex_alt_0_ExtendAv_node__node0.type.TypeID]) {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv_node__node0 = candidate_Complex_alt_0_ExtendAv_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel;
                                    candidate_Complex_alt_0_ExtendAv_node__node0.flags |= LGSPNode.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv_node__node0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv_node__node0) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv_node__node0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_Complex_alt_0_ExtendAv_node__node0,candidate_Complex_alt_0_ExtendAv_node__node0);
                                }
                                // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge1 from Complex_alt_0_ExtendAv_node_b2 
                                LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_node_b2.outhead;
                                if(head_candidate_Complex_alt_0_ExtendAv_edge__edge1 != null)
                                {
                                    LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge1 = head_candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if(candidate_Complex_alt_0_ExtendAv_edge__edge1.target != candidate_Complex_node_a) {
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge1))
                                            && (candidate_Complex_alt_0_ExtendAv_edge__edge1==candidate_Complex_alt_0_ExtendAv_edge__edge0
                                                || candidate_Complex_alt_0_ExtendAv_edge__edge1==candidate_Complex_alt_0_ExtendAv_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                        } else {
                                            prev__candidate_Complex_alt_0_ExtendAv_edge__edge1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendAv_edge__edge1,candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                        }
                                        // Extend Outgoing Complex_alt_0_ExtendAv_edge__edge3 from Complex_alt_0_ExtendAv_node__node0 
                                        LGSPEdge head_candidate_Complex_alt_0_ExtendAv_edge__edge3 = candidate_Complex_alt_0_ExtendAv_node__node0.outhead;
                                        if(head_candidate_Complex_alt_0_ExtendAv_edge__edge3 != null)
                                        {
                                            LGSPEdge candidate_Complex_alt_0_ExtendAv_edge__edge3 = head_candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv_edge__edge3))
                                                    && (candidate_Complex_alt_0_ExtendAv_edge__edge3==candidate_Complex_alt_0_ExtendAv_edge__edge0
                                                        || candidate_Complex_alt_0_ExtendAv_edge__edge3==candidate_Complex_alt_0_ExtendAv_edge__edge2
                                                        || candidate_Complex_alt_0_ExtendAv_edge__edge3==candidate_Complex_alt_0_ExtendAv_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // Implicit Target Complex_alt_0_ExtendAv_node__node1 from Complex_alt_0_ExtendAv_edge__edge3 
                                                LGSPNode candidate_Complex_alt_0_ExtendAv_node__node1 = candidate_Complex_alt_0_ExtendAv_edge__edge3.target;
                                                if(!NodeType_C.isMyType[candidate_Complex_alt_0_ExtendAv_node__node1.type.TypeID]) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv_node__node1))
                                                    && candidate_Complex_alt_0_ExtendAv_node__node1==candidate_Complex_alt_0_ExtendAv_node__node0
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@a] = candidate_Complex_node_a;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b2] = candidate_Complex_alt_0_ExtendAv_node_b2;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b] = candidate_Complex_node_b;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node0] = candidate_Complex_alt_0_ExtendAv_node__node0;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node1] = candidate_Complex_alt_0_ExtendAv_node__node1;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge0] = candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge1] = candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge2] = candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge3] = candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2 = candidate_Complex_alt_0_ExtendAv_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0 = candidate_Complex_alt_0_ExtendAv_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1 = candidate_Complex_alt_0_ExtendAv_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_node__node1.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge2.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3 = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge3.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0+0]);
                                                        match.patternGraph = patternGraph;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@a] = candidate_Complex_node_a;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b2] = candidate_Complex_alt_0_ExtendAv_node_b2;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@b] = candidate_Complex_node_b;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node0] = candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv_NodeNums.@_node1] = candidate_Complex_alt_0_ExtendAv_node__node1;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge0] = candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge1] = candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge2] = candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv_EdgeNums.@_edge3] = candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                        currentFoundPartialMatch.Push(match);
                                                    }
                                                    if(matchesList==foundPartialMatches) {
                                                        matchesList = new List<Stack<LGSPMatch>>();
                                                    } else {
                                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                                            foundPartialMatches.Add(match);
                                                        }
                                                        matchesList.Clear();
                                                    }
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        candidate_Complex_alt_0_ExtendAv_node__node1.flags = candidate_Complex_alt_0_ExtendAv_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                        candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        candidate_Complex_alt_0_ExtendAv_node_b2.flags = candidate_Complex_alt_0_ExtendAv_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                    candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                    candidate_Complex_alt_0_ExtendAv_node__node1.flags = candidate_Complex_alt_0_ExtendAv_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                    candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                    candidate_Complex_alt_0_ExtendAv_node_b2.flags = candidate_Complex_alt_0_ExtendAv_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                    continue;
                                                }
                                                candidate_Complex_alt_0_ExtendAv_node_b2.flags = candidate_Complex_alt_0_ExtendAv_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node_b2;
                                                candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node0;
                                                candidate_Complex_alt_0_ExtendAv_node__node1.flags = candidate_Complex_alt_0_ExtendAv_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_node__node1;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                                candidate_Complex_alt_0_ExtendAv_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv_edge__edge3;
                                            }
                                            while( (candidate_Complex_alt_0_ExtendAv_edge__edge3 = candidate_Complex_alt_0_ExtendAv_edge__edge3.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge3 );
                                        }
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_Complex_alt_0_ExtendAv_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge1;
                                        } else { 
                                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge1==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_Complex_alt_0_ExtendAv_edge__edge1 = candidate_Complex_alt_0_ExtendAv_edge__edge1.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge1 );
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv_node__node0.flags = candidate_Complex_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_node__node0;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv_node__node0==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv_node__node0);
                                    }
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge2;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge2==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_Complex_alt_0_ExtendAv_edge__edge2 = candidate_Complex_alt_0_ExtendAv_edge__edge2.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge2 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendAv_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv_edge__edge0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendAv_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_Complex_alt_0_ExtendAv_edge__edge0 = candidate_Complex_alt_0_ExtendAv_edge__edge0.outNext) != head_candidate_Complex_alt_0_ExtendAv_edge__edge0 );
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case Complex_alt_0_ExtendAv2 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendAv2];
                // SubPreset Complex_node_a 
                LGSPNode candidate_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                LGSPNode candidate_Complex_node_b = Complex_node_b;
                // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge0 from Complex_node_a 
                LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_node_a.outhead;
                if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge0 != null)
                {
                    LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge0 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge0,candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                        }
                        // Implicit Target Complex_alt_0_ExtendAv2_node_b2 from Complex_alt_0_ExtendAv2_edge__edge0 
                        LGSPNode candidate_Complex_alt_0_ExtendAv2_node_b2 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.target;
                        if(!NodeType_B.isMyType[candidate_Complex_alt_0_ExtendAv2_node_b2.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_node_b2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node_b2))
                            && candidate_Complex_alt_0_ExtendAv2_node_b2==candidate_Complex_node_b
                            )
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendAv2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge2 from Complex_node_b 
                        LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_node_b.outhead;
                        if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge2 != null)
                        {
                            LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge2 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv2_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge2))
                                    && candidate_Complex_alt_0_ExtendAv2_edge__edge2==candidate_Complex_alt_0_ExtendAv2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge2,candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                }
                                // Implicit Target Complex_alt_0_ExtendAv2_node__node0 from Complex_alt_0_ExtendAv2_edge__edge2 
                                LGSPNode candidate_Complex_alt_0_ExtendAv2_node__node0 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.target;
                                if(!NodeType_C.isMyType[candidate_Complex_alt_0_ExtendAv2_node__node0.type.TypeID]) {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendAv2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node0 = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel;
                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags |= LGSPNode.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node0) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_Complex_alt_0_ExtendAv2_node__node0,candidate_Complex_alt_0_ExtendAv2_node__node0);
                                }
                                // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge1 from Complex_alt_0_ExtendAv2_node_b2 
                                LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_node_b2.outhead;
                                if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge1 != null)
                                {
                                    LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge1 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv2_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if(candidate_Complex_alt_0_ExtendAv2_edge__edge1.target != candidate_Complex_node_a) {
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge1))
                                            && (candidate_Complex_alt_0_ExtendAv2_edge__edge1==candidate_Complex_alt_0_ExtendAv2_edge__edge0
                                                || candidate_Complex_alt_0_ExtendAv2_edge__edge1==candidate_Complex_alt_0_ExtendAv2_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                            candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                        } else {
                                            prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge1,candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                        }
                                        // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge3 from Complex_alt_0_ExtendAv2_node__node0 
                                        LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_node__node0.outhead;
                                        if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge3 != null)
                                        {
                                            LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge3 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv2_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge3))
                                                    && (candidate_Complex_alt_0_ExtendAv2_edge__edge3==candidate_Complex_alt_0_ExtendAv2_edge__edge0
                                                        || candidate_Complex_alt_0_ExtendAv2_edge__edge3==candidate_Complex_alt_0_ExtendAv2_edge__edge2
                                                        || candidate_Complex_alt_0_ExtendAv2_edge__edge3==candidate_Complex_alt_0_ExtendAv2_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                uint prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                                } else {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge3) ? 1U : 0U;
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendAv2_edge__edge3,candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                }
                                                // Implicit Target Complex_alt_0_ExtendAv2_node__node1 from Complex_alt_0_ExtendAv2_edge__edge3 
                                                LGSPNode candidate_Complex_alt_0_ExtendAv2_node__node1 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.target;
                                                if(!NodeType_C.isMyType[candidate_Complex_alt_0_ExtendAv2_node__node1.type.TypeID]) {
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3==0) {
                                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node1))
                                                    && candidate_Complex_alt_0_ExtendAv2_node__node1==candidate_Complex_alt_0_ExtendAv2_node__node0
                                                    )
                                                {
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3==0) {
                                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3==0) {
                                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                uint prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node1 = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel;
                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags |= LGSPNode.IS_MATCHED<<negLevel;
                                                } else {
                                                    prev__candidate_Complex_alt_0_ExtendAv2_node__node1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node1) ? 1U : 0U;
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_Complex_alt_0_ExtendAv2_node__node1,candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                }
                                                // Extend Outgoing Complex_alt_0_ExtendAv2_edge__edge4 from Complex_alt_0_ExtendAv2_node__node1 
                                                LGSPEdge head_candidate_Complex_alt_0_ExtendAv2_edge__edge4 = candidate_Complex_alt_0_ExtendAv2_node__node1.outhead;
                                                if(head_candidate_Complex_alt_0_ExtendAv2_edge__edge4 != null)
                                                {
                                                    LGSPEdge candidate_Complex_alt_0_ExtendAv2_edge__edge4 = head_candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                    do
                                                    {
                                                        if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendAv2_edge__edge4.type.TypeID]) {
                                                            continue;
                                                        }
                                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendAv2_edge__edge4))
                                                            && (candidate_Complex_alt_0_ExtendAv2_edge__edge4==candidate_Complex_alt_0_ExtendAv2_edge__edge0
                                                                || candidate_Complex_alt_0_ExtendAv2_edge__edge4==candidate_Complex_alt_0_ExtendAv2_edge__edge2
                                                                || candidate_Complex_alt_0_ExtendAv2_edge__edge4==candidate_Complex_alt_0_ExtendAv2_edge__edge1
                                                                || candidate_Complex_alt_0_ExtendAv2_edge__edge4==candidate_Complex_alt_0_ExtendAv2_edge__edge3
                                                                )
                                                            )
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                        {
                                                            continue;
                                                        }
                                                        // Implicit Target Complex_alt_0_ExtendAv2_node__node2 from Complex_alt_0_ExtendAv2_edge__edge4 
                                                        LGSPNode candidate_Complex_alt_0_ExtendAv2_node__node2 = candidate_Complex_alt_0_ExtendAv2_edge__edge4.target;
                                                        if(!NodeType_C.isMyType[candidate_Complex_alt_0_ExtendAv2_node__node2.type.TypeID]) {
                                                            continue;
                                                        }
                                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendAv2_node__node2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendAv2_node__node2))
                                                            && (candidate_Complex_alt_0_ExtendAv2_node__node2==candidate_Complex_alt_0_ExtendAv2_node__node0
                                                                || candidate_Complex_alt_0_ExtendAv2_node__node2==candidate_Complex_alt_0_ExtendAv2_node__node1
                                                                )
                                                            )
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_Complex_alt_0_ExtendAv2_node__node2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@a] = candidate_Complex_node_a;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b2] = candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b] = candidate_Complex_node_b;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node0] = candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node1] = candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                            match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node2] = candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge0] = candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge1] = candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge2] = candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge3] = candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                            match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge4] = candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                            currentFoundPartialMatch.Push(match);
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            continue;
                                                        }
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2 = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0 = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1 = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node1.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2 = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                        prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4 = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        // Match subpatterns 
                                                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                        // Check whether subpatterns were found 
                                                        if(matchesList.Count>0) {
                                                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                            {
                                                                LGSPMatch match = new LGSPMatch(new LGSPNode[6], new LGSPEdge[5], new LGSPMatch[0+0]);
                                                                match.patternGraph = patternGraph;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@a] = candidate_Complex_node_a;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b2] = candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@b] = candidate_Complex_node_b;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node0] = candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node1] = candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendAv2_NodeNums.@_node2] = candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge0] = candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge1] = candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge2] = candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge3] = candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendAv2_EdgeNums.@_edge4] = candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                                currentFoundPartialMatch.Push(match);
                                                            }
                                                            if(matchesList==foundPartialMatches) {
                                                                matchesList = new List<Stack<LGSPMatch>>();
                                                            } else {
                                                                foreach(Stack<LGSPMatch> match in matchesList) {
                                                                    foundPartialMatches.Add(match);
                                                                }
                                                                matchesList.Clear();
                                                            }
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                candidate_Complex_alt_0_ExtendAv2_node__node2.flags = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                                candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                candidate_Complex_alt_0_ExtendAv2_node_b2.flags = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                            candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                            candidate_Complex_alt_0_ExtendAv2_node__node2.flags = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                            candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                            candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                            candidate_Complex_alt_0_ExtendAv2_node_b2.flags = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                            continue;
                                                        }
                                                        candidate_Complex_alt_0_ExtendAv2_node_b2.flags = candidate_Complex_alt_0_ExtendAv2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node_b2;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                        candidate_Complex_alt_0_ExtendAv2_node__node2.flags = candidate_Complex_alt_0_ExtendAv2_node__node2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_node__node2;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                        candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge4.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendAv2_edge__edge4;
                                                    }
                                                    while( (candidate_Complex_alt_0_ExtendAv2_edge__edge4 = candidate_Complex_alt_0_ExtendAv2_edge__edge4.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge4 );
                                                }
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    candidate_Complex_alt_0_ExtendAv2_node__node1.flags = candidate_Complex_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node1;
                                                } else { 
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node1==0) {
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node1);
                                                    }
                                                }
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3;
                                                } else { 
                                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge3==0) {
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge3);
                                                    }
                                                }
                                            }
                                            while( (candidate_Complex_alt_0_ExtendAv2_edge__edge3 = candidate_Complex_alt_0_ExtendAv2_edge__edge3.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge3 );
                                        }
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge1==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_Complex_alt_0_ExtendAv2_edge__edge1 = candidate_Complex_alt_0_ExtendAv2_edge__edge1.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge1 );
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv2_node__node0.flags = candidate_Complex_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_node__node0;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_node__node0==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendAv2_node__node0);
                                    }
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge2==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_Complex_alt_0_ExtendAv2_edge__edge2 = candidate_Complex_alt_0_ExtendAv2_edge__edge2.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge2 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags = candidate_Complex_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendAv2_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendAv2_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_Complex_alt_0_ExtendAv2_edge__edge0 = candidate_Complex_alt_0_ExtendAv2_edge__edge0.outNext) != head_candidate_Complex_alt_0_ExtendAv2_edge__edge0 );
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case Complex_alt_0_ExtendNA2 
            do {
                patternGraph = patternGraphs[(int)Rule_Complex.Complex_alt_0_CaseNums.@ExtendNA2];
                // SubPreset Complex_node_a 
                LGSPNode candidate_Complex_node_a = Complex_node_a;
                // SubPreset Complex_node_b 
                LGSPNode candidate_Complex_node_b = Complex_node_b;
                // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge0 from Complex_node_a 
                LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge0 = candidate_Complex_node_a.outhead;
                if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge0 != null)
                {
                    LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge0 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendNA2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendNA2_edge__edge0,candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                        }
                        // Implicit Target Complex_alt_0_ExtendNA2_node__node0 from Complex_alt_0_ExtendNA2_edge__edge0 
                        LGSPNode candidate_Complex_alt_0_ExtendNA2_node__node0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0.target;
                        if(!NodeType_C.isMyType[candidate_Complex_alt_0_ExtendNA2_node__node0.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_Complex_alt_0_ExtendNA2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        uint prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_Complex_alt_0_ExtendNA2_node__node0 = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel;
                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags |= LGSPNode.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_Complex_alt_0_ExtendNA2_node__node0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendNA2_node__node0) ? 1U : 0U;
                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_Complex_alt_0_ExtendNA2_node__node0,candidate_Complex_alt_0_ExtendNA2_node__node0);
                        }
                        // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge2 from Complex_node_b 
                        LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_node_b.outhead;
                        if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge2 != null)
                        {
                            LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge2 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendNA2_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge2))
                                    && candidate_Complex_alt_0_ExtendNA2_edge__edge2==candidate_Complex_alt_0_ExtendNA2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                    candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendNA2_edge__edge2,candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                }
                                // Implicit Target Complex_alt_0_ExtendNA2_node_b2 from Complex_alt_0_ExtendNA2_edge__edge2 
                                LGSPNode candidate_Complex_alt_0_ExtendNA2_node_b2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.target;
                                if(!NodeType_B.isMyType[candidate_Complex_alt_0_ExtendNA2_node_b2.type.TypeID]) {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_node_b2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendNA2_node_b2))
                                    && candidate_Complex_alt_0_ExtendNA2_node_b2==candidate_Complex_node_b
                                    )
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_Complex_alt_0_ExtendNA2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge1 from Complex_alt_0_ExtendNA2_node__node0 
                                LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_node__node0.outhead;
                                if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge1 != null)
                                {
                                    LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge1 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendNA2_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge1))
                                            && (candidate_Complex_alt_0_ExtendNA2_edge__edge1==candidate_Complex_alt_0_ExtendNA2_edge__edge0
                                                || candidate_Complex_alt_0_ExtendNA2_edge__edge1==candidate_Complex_alt_0_ExtendNA2_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                        } else {
                                            prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_Complex_alt_0_ExtendNA2_edge__edge1,candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                        }
                                        // Implicit Target Complex_alt_0_ExtendNA2_node__node1 from Complex_alt_0_ExtendNA2_edge__edge1 
                                        LGSPNode candidate_Complex_alt_0_ExtendNA2_node__node1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.target;
                                        if(!NodeType_C.isMyType[candidate_Complex_alt_0_ExtendNA2_node__node1.type.TypeID]) {
                                            if(negLevel <= MAX_NEG_LEVEL) {
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1==0) {
                                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Complex_alt_0_ExtendNA2_node__node1))
                                            && candidate_Complex_alt_0_ExtendNA2_node__node1==candidate_Complex_alt_0_ExtendNA2_node__node0
                                            )
                                        {
                                            if(negLevel <= MAX_NEG_LEVEL) {
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1==0) {
                                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((candidate_Complex_alt_0_ExtendNA2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            if(negLevel <= MAX_NEG_LEVEL) {
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1==0) {
                                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        // Extend Outgoing Complex_alt_0_ExtendNA2_edge__edge3 from Complex_alt_0_ExtendNA2_node_b2 
                                        LGSPEdge head_candidate_Complex_alt_0_ExtendNA2_edge__edge3 = candidate_Complex_alt_0_ExtendNA2_node_b2.outhead;
                                        if(head_candidate_Complex_alt_0_ExtendNA2_edge__edge3 != null)
                                        {
                                            LGSPEdge candidate_Complex_alt_0_ExtendNA2_edge__edge3 = head_candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[candidate_Complex_alt_0_ExtendNA2_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if(candidate_Complex_alt_0_ExtendNA2_edge__edge3.target != candidate_Complex_node_b) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_Complex_alt_0_ExtendNA2_edge__edge3))
                                                    && (candidate_Complex_alt_0_ExtendNA2_edge__edge3==candidate_Complex_alt_0_ExtendNA2_edge__edge0
                                                        || candidate_Complex_alt_0_ExtendNA2_edge__edge3==candidate_Complex_alt_0_ExtendNA2_edge__edge2
                                                        || candidate_Complex_alt_0_ExtendNA2_edge__edge3==candidate_Complex_alt_0_ExtendNA2_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@a] = candidate_Complex_node_a;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node0] = candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node1] = candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b] = candidate_Complex_node_b;
                                                    match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b2] = candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge0] = candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge1] = candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge2] = candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                    match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge3] = candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0 = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1 = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_node__node1.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2 = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0 = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3 = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0+0]);
                                                        match.patternGraph = patternGraph;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@a] = candidate_Complex_node_a;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node0] = candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@_node1] = candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b] = candidate_Complex_node_b;
                                                        match.Nodes[(int)Rule_Complex.Complex_alt_0_ExtendNA2_NodeNums.@b2] = candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge0] = candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge1] = candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge2] = candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        match.Edges[(int)Rule_Complex.Complex_alt_0_ExtendNA2_EdgeNums.@_edge3] = candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                        currentFoundPartialMatch.Push(match);
                                                    }
                                                    if(matchesList==foundPartialMatches) {
                                                        matchesList = new List<Stack<LGSPMatch>>();
                                                    } else {
                                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                                            foundPartialMatches.Add(match);
                                                        }
                                                        matchesList.Clear();
                                                    }
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        candidate_Complex_alt_0_ExtendNA2_node_b2.flags = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                        candidate_Complex_alt_0_ExtendNA2_node__node1.flags = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                        candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                    candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                    candidate_Complex_alt_0_ExtendNA2_node_b2.flags = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                    candidate_Complex_alt_0_ExtendNA2_node__node1.flags = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                    candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                    continue;
                                                }
                                                candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node0;
                                                candidate_Complex_alt_0_ExtendNA2_node__node1.flags = candidate_Complex_alt_0_ExtendNA2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node__node1;
                                                candidate_Complex_alt_0_ExtendNA2_node_b2.flags = candidate_Complex_alt_0_ExtendNA2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_node_b2;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                                candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Complex_alt_0_ExtendNA2_edge__edge3;
                                            }
                                            while( (candidate_Complex_alt_0_ExtendNA2_edge__edge3 = candidate_Complex_alt_0_ExtendNA2_edge__edge3.outNext) != head_candidate_Complex_alt_0_ExtendNA2_edge__edge3 );
                                        }
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge1==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_Complex_alt_0_ExtendNA2_edge__edge1 = candidate_Complex_alt_0_ExtendNA2_edge__edge1.outNext) != head_candidate_Complex_alt_0_ExtendNA2_edge__edge1 );
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2;
                                } else { 
                                    if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge2==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_Complex_alt_0_ExtendNA2_edge__edge2 = candidate_Complex_alt_0_ExtendNA2_edge__edge2.outNext) != head_candidate_Complex_alt_0_ExtendNA2_edge__edge2 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendNA2_node__node0.flags = candidate_Complex_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_node__node0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendNA2_node__node0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Complex_alt_0_ExtendNA2_node__node0);
                            }
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags = candidate_Complex_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0;
                        } else { 
                            if(prev__candidate_Complex_alt_0_ExtendNA2_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_Complex_alt_0_ExtendNA2_edge__edge0);
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

    public class Action_ComplexMax : LGSPAction
    {
        public Action_ComplexMax() {
            rulePattern = Rule_ComplexMax.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0+1);
        }

        public override string Name { get { return "ComplexMax"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_ComplexMax instance = new Action_ComplexMax();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup ComplexMax_edge__edge0 
            int type_id_candidate_ComplexMax_edge__edge0 = 1;
            for(LGSPEdge head_candidate_ComplexMax_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_ComplexMax_edge__edge0], candidate_ComplexMax_edge__edge0 = head_candidate_ComplexMax_edge__edge0.typeNext; candidate_ComplexMax_edge__edge0 != head_candidate_ComplexMax_edge__edge0; candidate_ComplexMax_edge__edge0 = candidate_ComplexMax_edge__edge0.typeNext)
            {
                uint prev__candidate_ComplexMax_edge__edge0;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_ComplexMax_edge__edge0 = candidate_ComplexMax_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                    candidate_ComplexMax_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_ComplexMax_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_edge__edge0) ? 1U : 0U;
                    if(prev__candidate_ComplexMax_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_edge__edge0,candidate_ComplexMax_edge__edge0);
                }
                // Implicit Source ComplexMax_node_a from ComplexMax_edge__edge0 
                LGSPNode candidate_ComplexMax_node_a = candidate_ComplexMax_edge__edge0.source;
                if(!NodeType_A.isMyType[candidate_ComplexMax_node_a.type.TypeID]) {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_edge__edge0;
                    } else { 
                        if(prev__candidate_ComplexMax_edge__edge0==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_edge__edge0);
                        }
                    }
                    continue;
                }
                // Implicit Target ComplexMax_node_b from ComplexMax_edge__edge0 
                LGSPNode candidate_ComplexMax_node_b = candidate_ComplexMax_edge__edge0.target;
                if(!NodeType_B.isMyType[candidate_ComplexMax_node_b.type.TypeID]) {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_edge__edge0;
                    } else { 
                        if(prev__candidate_ComplexMax_edge__edge0==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_edge__edge0);
                        }
                    }
                    continue;
                }
                // Extend Outgoing ComplexMax_edge__edge1 from ComplexMax_node_b 
                LGSPEdge head_candidate_ComplexMax_edge__edge1 = candidate_ComplexMax_node_b.outhead;
                if(head_candidate_ComplexMax_edge__edge1 != null)
                {
                    LGSPEdge candidate_ComplexMax_edge__edge1 = head_candidate_ComplexMax_edge__edge1;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_edge__edge1.type.TypeID]) {
                            continue;
                        }
                        if(candidate_ComplexMax_edge__edge1.target != candidate_ComplexMax_node_a) {
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_edge__edge1))
                            && candidate_ComplexMax_edge__edge1==candidate_ComplexMax_edge__edge0
                            )
                        {
                            continue;
                        }
                        // Push alternative matching task for ComplexMax_alt_0
                        AlternativeAction_ComplexMax_alt_0 taskFor_alt_0 = new AlternativeAction_ComplexMax_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_ComplexMax.ComplexMax_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.ComplexMax_node_a = candidate_ComplexMax_node_a;
                        taskFor_alt_0.ComplexMax_node_b = candidate_ComplexMax_node_b;
                        openTasks.Push(taskFor_alt_0);
                        uint prevGlobal__candidate_ComplexMax_node_a;
                        prevGlobal__candidate_ComplexMax_node_a = candidate_ComplexMax_node_a.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_node_a.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ComplexMax_node_b;
                        prevGlobal__candidate_ComplexMax_node_b = candidate_ComplexMax_node_b.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_node_b.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ComplexMax_edge__edge0;
                        prevGlobal__candidate_ComplexMax_edge__edge0 = candidate_ComplexMax_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ComplexMax_edge__edge1;
                        prevGlobal__candidate_ComplexMax_edge__edge1 = candidate_ComplexMax_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ComplexMax_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for alt_0
                        openTasks.Pop();
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_NodeNums.@a] = candidate_ComplexMax_node_a;
                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_NodeNums.@b] = candidate_ComplexMax_node_b;
                                match.Edges[(int)Rule_ComplexMax.ComplexMax_EdgeNums.@_edge0] = candidate_ComplexMax_edge__edge0;
                                match.Edges[(int)Rule_ComplexMax.ComplexMax_EdgeNums.@_edge1] = candidate_ComplexMax_edge__edge1;
                                match.EmbeddedGraphs[((int)Rule_ComplexMax.ComplexMax_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                                matches.matchesList.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                            {
                                candidate_ComplexMax_edge__edge1.flags = candidate_ComplexMax_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge1;
                                candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge0;
                                candidate_ComplexMax_node_b.flags = candidate_ComplexMax_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_b;
                                candidate_ComplexMax_node_a.flags = candidate_ComplexMax_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_a;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_edge__edge0;
                                } else { 
                                    if(prev__candidate_ComplexMax_edge__edge0==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_edge__edge0);
                                    }
                                }
                                return matches;
                            }
                            candidate_ComplexMax_edge__edge1.flags = candidate_ComplexMax_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge1;
                            candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge0;
                            candidate_ComplexMax_node_b.flags = candidate_ComplexMax_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_b;
                            candidate_ComplexMax_node_a.flags = candidate_ComplexMax_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_a;
                            continue;
                        }
                        candidate_ComplexMax_node_a.flags = candidate_ComplexMax_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_a;
                        candidate_ComplexMax_node_b.flags = candidate_ComplexMax_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_node_b;
                        candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge0;
                        candidate_ComplexMax_edge__edge1.flags = candidate_ComplexMax_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_edge__edge1;
                    }
                    while( (candidate_ComplexMax_edge__edge1 = candidate_ComplexMax_edge__edge1.outNext) != head_candidate_ComplexMax_edge__edge1 );
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_ComplexMax_edge__edge0.flags = candidate_ComplexMax_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_edge__edge0;
                } else { 
                    if(prev__candidate_ComplexMax_edge__edge0==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_edge__edge0);
                    }
                }
            }
            return matches;
        }
    }

    public class AlternativeAction_ComplexMax_alt_0 : LGSPSubpatternAction
    {
        public AlternativeAction_ComplexMax_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public LGSPNode ComplexMax_node_a;
        public LGSPNode ComplexMax_node_b;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ComplexMax_alt_0_ExtendAv 
            do {
                patternGraph = patternGraphs[(int)Rule_ComplexMax.ComplexMax_alt_0_CaseNums.@ExtendAv];
                // SubPreset ComplexMax_node_a 
                LGSPNode candidate_ComplexMax_node_a = ComplexMax_node_a;
                // SubPreset ComplexMax_node_b 
                LGSPNode candidate_ComplexMax_node_b = ComplexMax_node_b;
                // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge0 from ComplexMax_node_a 
                LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_node_a.outhead;
                if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 != null)
                {
                    LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0,candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                        }
                        // Implicit Target ComplexMax_alt_0_ExtendAv_node_b2 from ComplexMax_alt_0_ExtendAv_edge__edge0 
                        LGSPNode candidate_ComplexMax_alt_0_ExtendAv_node_b2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.target;
                        if(!NodeType_B.isMyType[candidate_ComplexMax_alt_0_ExtendAv_node_b2.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node_b2))
                            && candidate_ComplexMax_alt_0_ExtendAv_node_b2==candidate_ComplexMax_node_b
                            )
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge2 from ComplexMax_node_b 
                        LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_node_b.outhead;
                        if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 != null)
                        {
                            LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2))
                                    && candidate_ComplexMax_alt_0_ExtendAv_edge__edge2==candidate_ComplexMax_alt_0_ExtendAv_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2,candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                }
                                // Implicit Target ComplexMax_alt_0_ExtendAv_node__node0 from ComplexMax_alt_0_ExtendAv_edge__edge2 
                                LGSPNode candidate_ComplexMax_alt_0_ExtendAv_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.target;
                                if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendAv_node__node0.type.TypeID]) {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags |= LGSPNode.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node__node0) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv_node__node0,candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                }
                                // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge1 from ComplexMax_alt_0_ExtendAv_node_b2 
                                LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_node_b2.outhead;
                                if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 != null)
                                {
                                    LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.target != candidate_ComplexMax_node_a) {
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1))
                                            && (candidate_ComplexMax_alt_0_ExtendAv_edge__edge1==candidate_ComplexMax_alt_0_ExtendAv_edge__edge0
                                                || candidate_ComplexMax_alt_0_ExtendAv_edge__edge1==candidate_ComplexMax_alt_0_ExtendAv_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                        } else {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1,candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                        }
                                        // Extend Outgoing ComplexMax_alt_0_ExtendAv_edge__edge3 from ComplexMax_alt_0_ExtendAv_node__node0 
                                        LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_node__node0.outhead;
                                        if(head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 != null)
                                        {
                                            LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_edge__edge3))
                                                    && (candidate_ComplexMax_alt_0_ExtendAv_edge__edge3==candidate_ComplexMax_alt_0_ExtendAv_edge__edge0
                                                        || candidate_ComplexMax_alt_0_ExtendAv_edge__edge3==candidate_ComplexMax_alt_0_ExtendAv_edge__edge2
                                                        || candidate_ComplexMax_alt_0_ExtendAv_edge__edge3==candidate_ComplexMax_alt_0_ExtendAv_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // Implicit Target ComplexMax_alt_0_ExtendAv_node_c from ComplexMax_alt_0_ExtendAv_edge__edge3 
                                                LGSPNode candidate_ComplexMax_alt_0_ExtendAv_node_c = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.target;
                                                if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendAv_node_c.type.TypeID]) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node_c))
                                                    && candidate_ComplexMax_alt_0_ExtendAv_node_c==candidate_ComplexMax_alt_0_ExtendAv_node__node0
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                // NegativePattern 
                                                {
                                                    ++negLevel;
                                                    if(negLevel > MAX_NEG_LEVEL && negLevel-MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                                                        graph.atNegLevelMatchedElements.Add(new Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>());
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst = new Dictionary<LGSPNode, LGSPNode>();
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd = new Dictionary<LGSPEdge, LGSPEdge>();
                                                    }
                                                    uint prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & LGSPNode.IS_MATCHED<<negLevel;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_c.flags |= LGSPNode.IS_MATCHED<<negLevel;
                                                    } else {
                                                        prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_node_c) ? 1U : 0U;
                                                        if(prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv_node_c,candidate_ComplexMax_alt_0_ExtendAv_node_c);
                                                    }
                                                    // Extend Outgoing ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 from ComplexMax_alt_0_ExtendAv_node_c 
                                                    LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_node_c.outhead;
                                                    if(head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 != null)
                                                    {
                                                        LGSPEdge candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0;
                                                        do
                                                        {
                                                            if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.type.TypeID]) {
                                                                continue;
                                                            }
                                                            if((candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                            {
                                                                continue;
                                                            }
                                                            // Implicit Target ComplexMax_alt_0_ExtendAv_neg_0_node__node0 from ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 
                                                            LGSPNode candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.target;
                                                            if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0.type.TypeID]) {
                                                                continue;
                                                            }
                                                            if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0))
                                                                && candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0==candidate_ComplexMax_alt_0_ExtendAv_node_c
                                                                )
                                                            {
                                                                continue;
                                                            }
                                                            if((candidate_ComplexMax_alt_0_ExtendAv_neg_0_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                            {
                                                                continue;
                                                            }
                                                            // negative pattern found
                                                            if(negLevel <= MAX_NEG_LEVEL) {
                                                                candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                            } else { 
                                                                if(prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c==0) {
                                                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node_c);
                                                                }
                                                            }
                                                            if(negLevel > MAX_NEG_LEVEL) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                                                            }
                                                            --negLevel;
                                                            goto label1;
                                                        }
                                                        while( (candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_neg_0_edge__edge0 );
                                                    }
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    } else { 
                                                        if(prev_neg_0__candidate_ComplexMax_alt_0_ExtendAv_node_c==0) {
                                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node_c);
                                                        }
                                                    }
                                                    if(negLevel > MAX_NEG_LEVEL) {
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                                                    }
                                                    --negLevel;
                                                }
                                                // Check whether there are subpattern matching tasks left to execute
                                                if(openTasks.Count==0)
                                                {
                                                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                                    LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0]);
                                                    match.patternGraph = patternGraph;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@a] = candidate_ComplexMax_node_a;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@b2] = candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@b] = candidate_ComplexMax_node_b;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@_node0] = candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@c] = candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge0] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge1] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge2] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge3] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    goto label2;
                                                }
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2 = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0 = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_node_c.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0+0]);
                                                        match.patternGraph = patternGraph;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@a] = candidate_ComplexMax_node_a;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@b2] = candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@b] = candidate_ComplexMax_node_b;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@_node0] = candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_NodeNums.@c] = candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge0] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge1] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge2] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv_EdgeNums.@_edge3] = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                        currentFoundPartialMatch.Push(match);
                                                    }
                                                    if(matchesList==foundPartialMatches) {
                                                        matchesList = new List<Stack<LGSPMatch>>();
                                                    } else {
                                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                                            foundPartialMatches.Add(match);
                                                        }
                                                        matchesList.Clear();
                                                    }
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                    candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                    candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                    candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                    goto label3;
                                                }
                                                candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_b2;
                                                candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                                candidate_ComplexMax_alt_0_ExtendAv_node_c.flags = candidate_ComplexMax_alt_0_ExtendAv_node_c.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_node_c;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                                candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv_edge__edge3;
label1: ;
label2: ;
label3: ;
                                            }
                                            while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge3.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge3 );
                                        }
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1;
                                        } else { 
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge1==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge1.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge1 );
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_node__node0==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv_node__node0);
                                    }
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge2==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge2.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge2 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv_edge__edge0.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv_edge__edge0 );
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case ComplexMax_alt_0_ExtendAv2 
            do {
                patternGraph = patternGraphs[(int)Rule_ComplexMax.ComplexMax_alt_0_CaseNums.@ExtendAv2];
                // SubPreset ComplexMax_node_a 
                LGSPNode candidate_ComplexMax_node_a = ComplexMax_node_a;
                // SubPreset ComplexMax_node_b 
                LGSPNode candidate_ComplexMax_node_b = ComplexMax_node_b;
                // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge0 from ComplexMax_node_a 
                LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_node_a.outhead;
                if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 != null)
                {
                    LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                        }
                        // Implicit Target ComplexMax_alt_0_ExtendAv2_node_b2 from ComplexMax_alt_0_ExtendAv2_edge__edge0 
                        LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node_b2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.target;
                        if(!NodeType_B.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_node_b2.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node_b2))
                            && candidate_ComplexMax_alt_0_ExtendAv2_node_b2==candidate_ComplexMax_node_b
                            )
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge2 from ComplexMax_node_b 
                        LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_node_b.outhead;
                        if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 != null)
                        {
                            LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2))
                                    && candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                }
                                // Implicit Target ComplexMax_alt_0_ExtendAv2_node__node0 from ComplexMax_alt_0_ExtendAv2_edge__edge2 
                                LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.target;
                                if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_node__node0.type.TypeID]) {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel;
                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags |= LGSPNode.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node0) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv2_node__node0,candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                }
                                // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge1 from ComplexMax_alt_0_ExtendAv2_node_b2 
                                LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.outhead;
                                if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 != null)
                                {
                                    LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.target != candidate_ComplexMax_node_a) {
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1))
                                            && (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0
                                                || candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                        } else {
                                            prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                        }
                                        // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge3 from ComplexMax_alt_0_ExtendAv2_node__node0 
                                        LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.outhead;
                                        if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 != null)
                                        {
                                            LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3))
                                                    && (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0
                                                        || candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2
                                                        || candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    continue;
                                                }
                                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                                } else {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3) ? 1U : 0U;
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3,candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                }
                                                // Implicit Target ComplexMax_alt_0_ExtendAv2_node__node1 from ComplexMax_alt_0_ExtendAv2_edge__edge3 
                                                LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.target;
                                                if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_node__node1.type.TypeID]) {
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==0) {
                                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node1))
                                                    && candidate_ComplexMax_alt_0_ExtendAv2_node__node1==candidate_ComplexMax_alt_0_ExtendAv2_node__node0
                                                    )
                                                {
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==0) {
                                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                {
                                                    if(negLevel <= MAX_NEG_LEVEL) {
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                    } else { 
                                                        if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==0) {
                                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                        }
                                                    }
                                                    continue;
                                                }
                                                uint prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel;
                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags |= LGSPNode.IS_MATCHED<<negLevel;
                                                } else {
                                                    prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node1) ? 1U : 0U;
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_ComplexMax_alt_0_ExtendAv2_node__node1,candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                }
                                                // Extend Outgoing ComplexMax_alt_0_ExtendAv2_edge__edge4 from ComplexMax_alt_0_ExtendAv2_node__node1 
                                                LGSPEdge head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.outhead;
                                                if(head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 != null)
                                                {
                                                    LGSPEdge candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                    do
                                                    {
                                                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.type.TypeID]) {
                                                            continue;
                                                        }
                                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4))
                                                            && (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0
                                                                || candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2
                                                                || candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1
                                                                || candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4==candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3
                                                                )
                                                            )
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                                        {
                                                            continue;
                                                        }
                                                        // Implicit Target ComplexMax_alt_0_ExtendAv2_node__node2 from ComplexMax_alt_0_ExtendAv2_edge__edge4 
                                                        LGSPNode candidate_ComplexMax_alt_0_ExtendAv2_node__node2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.target;
                                                        if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendAv2_node__node2.type.TypeID]) {
                                                            continue;
                                                        }
                                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendAv2_node__node2))
                                                            && (candidate_ComplexMax_alt_0_ExtendAv2_node__node2==candidate_ComplexMax_alt_0_ExtendAv2_node__node0
                                                                || candidate_ComplexMax_alt_0_ExtendAv2_node__node2==candidate_ComplexMax_alt_0_ExtendAv2_node__node1
                                                                )
                                                            )
                                                        {
                                                            continue;
                                                        }
                                                        if((candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                                            match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@a] = candidate_ComplexMax_node_a;
                                                            match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@b2] = candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                            match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@b] = candidate_ComplexMax_node_b;
                                                            match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@_node0] = candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                            match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@_node1] = candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                            match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@_node2] = candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                            match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge0] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                            match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge1] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                            match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge2] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                            match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge3] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                            match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge4] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                            currentFoundPartialMatch.Push(match);
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            continue;
                                                        }
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2 = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0 = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1 = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2 = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        uint prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                        prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                        // Match subpatterns 
                                                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                        // Check whether subpatterns were found 
                                                        if(matchesList.Count>0) {
                                                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                            {
                                                                LGSPMatch match = new LGSPMatch(new LGSPNode[6], new LGSPEdge[5], new LGSPMatch[0+0]);
                                                                match.patternGraph = patternGraph;
                                                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@a] = candidate_ComplexMax_node_a;
                                                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@b2] = candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@b] = candidate_ComplexMax_node_b;
                                                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@_node0] = candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@_node1] = candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_NodeNums.@_node2] = candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                                match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge0] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge1] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge2] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge3] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendAv2_EdgeNums.@_edge4] = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                                currentFoundPartialMatch.Push(match);
                                                            }
                                                            if(matchesList==foundPartialMatches) {
                                                                matchesList = new List<Stack<LGSPMatch>>();
                                                            } else {
                                                                foreach(Stack<LGSPMatch> match in matchesList) {
                                                                    foundPartialMatches.Add(match);
                                                                }
                                                                matchesList.Clear();
                                                            }
                                                            // if enough matches were found, we leave
                                                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                            {
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                                                    }
                                                                }
                                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                                } else { 
                                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0==0) {
                                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                                                                    }
                                                                }
                                                                openTasks.Push(this);
                                                                return;
                                                            }
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                            candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                            continue;
                                                        }
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node_b2;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_node__node2;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                        candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4;
                                                    }
                                                    while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge4 );
                                                }
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node1.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1;
                                                } else { 
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node1==0) {
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node1);
                                                    }
                                                }
                                                if(negLevel <= MAX_NEG_LEVEL) {
                                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3;
                                                } else { 
                                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3==0) {
                                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3);
                                                    }
                                                }
                                            }
                                            while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge3 );
                                        }
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge1 );
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendAv2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_node__node0==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendAv2_node__node0);
                                    }
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge2 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0.outNext) != head_candidate_ComplexMax_alt_0_ExtendAv2_edge__edge0 );
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case ComplexMax_alt_0_ExtendNA2 
            do {
                patternGraph = patternGraphs[(int)Rule_ComplexMax.ComplexMax_alt_0_CaseNums.@ExtendNA2];
                // SubPreset ComplexMax_node_a 
                LGSPNode candidate_ComplexMax_node_a = ComplexMax_node_a;
                // SubPreset ComplexMax_node_b 
                LGSPNode candidate_ComplexMax_node_b = ComplexMax_node_b;
                // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge0 from ComplexMax_node_a 
                LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = candidate_ComplexMax_node_a.outhead;
                if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 != null)
                {
                    LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0,candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                        }
                        // Implicit Target ComplexMax_alt_0_ExtendNA2_node__node0 from ComplexMax_alt_0_ExtendNA2_edge__edge0 
                        LGSPNode candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.target;
                        if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendNA2_node__node0.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                            } else { 
                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        uint prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel;
                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags |= LGSPNode.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_node__node0) ? 1U : 0U;
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_ComplexMax_alt_0_ExtendNA2_node__node0,candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                        }
                        // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge2 from ComplexMax_node_b 
                        LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_node_b.outhead;
                        if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 != null)
                        {
                            LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2))
                                    && candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                uint prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                } else {
                                    prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2) ? 1U : 0U;
                                    if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2,candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                }
                                // Implicit Target ComplexMax_alt_0_ExtendNA2_node_b2 from ComplexMax_alt_0_ExtendNA2_edge__edge2 
                                LGSPNode candidate_ComplexMax_alt_0_ExtendNA2_node_b2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.target;
                                if(!NodeType_B.isMyType[candidate_ComplexMax_alt_0_ExtendNA2_node_b2.type.TypeID]) {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_node_b2))
                                    && candidate_ComplexMax_alt_0_ExtendNA2_node_b2==candidate_ComplexMax_node_b
                                    )
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                if((candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    if(negLevel <= MAX_NEG_LEVEL) {
                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                    } else { 
                                        if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==0) {
                                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                        }
                                    }
                                    continue;
                                }
                                // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge1 from ComplexMax_alt_0_ExtendNA2_node__node0 
                                LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.outhead;
                                if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 != null)
                                {
                                    LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                    do
                                    {
                                        if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.type.TypeID]) {
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1))
                                            && (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0
                                                || candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2
                                                )
                                            )
                                        {
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            continue;
                                        }
                                        uint prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                                        } else {
                                            prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1) ? 1U : 0U;
                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1,candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                        }
                                        // Implicit Target ComplexMax_alt_0_ExtendNA2_node__node1 from ComplexMax_alt_0_ExtendNA2_edge__edge1 
                                        LGSPNode candidate_ComplexMax_alt_0_ExtendNA2_node__node1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.target;
                                        if(!NodeType_C.isMyType[candidate_ComplexMax_alt_0_ExtendNA2_node__node1.type.TypeID]) {
                                            if(negLevel <= MAX_NEG_LEVEL) {
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==0) {
                                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_node__node1))
                                            && candidate_ComplexMax_alt_0_ExtendNA2_node__node1==candidate_ComplexMax_alt_0_ExtendNA2_node__node0
                                            )
                                        {
                                            if(negLevel <= MAX_NEG_LEVEL) {
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==0) {
                                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        if((candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                        {
                                            if(negLevel <= MAX_NEG_LEVEL) {
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                            } else { 
                                                if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==0) {
                                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                }
                                            }
                                            continue;
                                        }
                                        // Extend Outgoing ComplexMax_alt_0_ExtendNA2_edge__edge3 from ComplexMax_alt_0_ExtendNA2_node_b2 
                                        LGSPEdge head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.outhead;
                                        if(head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 != null)
                                        {
                                            LGSPEdge candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                            do
                                            {
                                                if(!EdgeType_Edge.isMyType[candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.type.TypeID]) {
                                                    continue;
                                                }
                                                if(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.target != candidate_ComplexMax_node_b) {
                                                    continue;
                                                }
                                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3))
                                                    && (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3==candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0
                                                        || candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3==candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2
                                                        || candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3==candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1
                                                        )
                                                    )
                                                {
                                                    continue;
                                                }
                                                if((candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@a] = candidate_ComplexMax_node_a;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@_node0] = candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@_node1] = candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@b] = candidate_ComplexMax_node_b;
                                                    match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@b2] = candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge0] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge1] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge2] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                    match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge3] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                    currentFoundPartialMatch.Push(match);
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    continue;
                                                }
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0 = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1 = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2 = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                uint prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                                // Match subpatterns 
                                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                                // Check whether subpatterns were found 
                                                if(matchesList.Count>0) {
                                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                                    {
                                                        LGSPMatch match = new LGSPMatch(new LGSPNode[5], new LGSPEdge[4], new LGSPMatch[0+0]);
                                                        match.patternGraph = patternGraph;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@a] = candidate_ComplexMax_node_a;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@_node0] = candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@_node1] = candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@b] = candidate_ComplexMax_node_b;
                                                        match.Nodes[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_NodeNums.@b2] = candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge0] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge1] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge2] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        match.Edges[(int)Rule_ComplexMax.ComplexMax_alt_0_ExtendNA2_EdgeNums.@_edge3] = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                        currentFoundPartialMatch.Push(match);
                                                    }
                                                    if(matchesList==foundPartialMatches) {
                                                        matchesList = new List<Stack<LGSPMatch>>();
                                                    } else {
                                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                                            foundPartialMatches.Add(match);
                                                        }
                                                        matchesList.Clear();
                                                    }
                                                    // if enough matches were found, we leave
                                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                                    {
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                        candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                                                            }
                                                        }
                                                        if(negLevel <= MAX_NEG_LEVEL) {
                                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                        } else { 
                                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0==0) {
                                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
                                                            }
                                                        }
                                                        openTasks.Push(this);
                                                        return;
                                                    }
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                    candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                    continue;
                                                }
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node1.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node__node1;
                                                candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags = candidate_ComplexMax_alt_0_ExtendNA2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_node_b2;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                                candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3;
                                            }
                                            while( (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3.outNext) != head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge3 );
                                        }
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1;
                                        } else { 
                                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1);
                                            }
                                        }
                                    }
                                    while( (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1.outNext) != head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge1 );
                                }
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2;
                                } else { 
                                    if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2);
                                    }
                                }
                            }
                            while( (candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2.outNext) != head_candidate_ComplexMax_alt_0_ExtendNA2_edge__edge2 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags = candidate_ComplexMax_alt_0_ExtendNA2_node__node0.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_node__node0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ComplexMax_alt_0_ExtendNA2_node__node0);
                            }
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags = candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0;
                        } else { 
                            if(prev__candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ComplexMax_alt_0_ExtendNA2_edge__edge0);
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

    public class Action_createABA : LGSPAction
    {
        public Action_createABA() {
            rulePattern = Rule_createABA.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "createABA"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createABA instance = new Action_createABA();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
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
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0+1);
        }

        public override string Name { get { return "homm"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_homm instance = new Action_homm();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup homm_edge__edge0 
            int type_id_candidate_homm_edge__edge0 = 1;
            for(LGSPEdge head_candidate_homm_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_homm_edge__edge0], candidate_homm_edge__edge0 = head_candidate_homm_edge__edge0.typeNext; candidate_homm_edge__edge0 != head_candidate_homm_edge__edge0; candidate_homm_edge__edge0 = candidate_homm_edge__edge0.typeNext)
            {
                uint prev__candidate_homm_edge__edge0;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_homm_edge__edge0 = candidate_homm_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                    candidate_homm_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_homm_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_edge__edge0) ? 1U : 0U;
                    if(prev__candidate_homm_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_homm_edge__edge0,candidate_homm_edge__edge0);
                }
                // Implicit Source homm_node_a from homm_edge__edge0 
                LGSPNode candidate_homm_node_a = candidate_homm_edge__edge0.source;
                if(!NodeType_A.isMyType[candidate_homm_node_a.type.TypeID]) {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_edge__edge0;
                    } else { 
                        if(prev__candidate_homm_edge__edge0==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_edge__edge0);
                        }
                    }
                    continue;
                }
                // Implicit Target homm_node_b from homm_edge__edge0 
                LGSPNode candidate_homm_node_b = candidate_homm_edge__edge0.target;
                if(!NodeType_B.isMyType[candidate_homm_node_b.type.TypeID]) {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_edge__edge0;
                    } else { 
                        if(prev__candidate_homm_edge__edge0==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_edge__edge0);
                        }
                    }
                    continue;
                }
                // Extend Outgoing homm_edge__edge1 from homm_node_b 
                LGSPEdge head_candidate_homm_edge__edge1 = candidate_homm_node_b.outhead;
                if(head_candidate_homm_edge__edge1 != null)
                {
                    LGSPEdge candidate_homm_edge__edge1 = head_candidate_homm_edge__edge1;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_homm_edge__edge1.type.TypeID]) {
                            continue;
                        }
                        if(candidate_homm_edge__edge1.target != candidate_homm_node_a) {
                            continue;
                        }
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_homm_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_edge__edge1))
                            && candidate_homm_edge__edge1==candidate_homm_edge__edge0
                            )
                        {
                            continue;
                        }
                        // Push alternative matching task for homm_alt_0
                        AlternativeAction_homm_alt_0 taskFor_alt_0 = new AlternativeAction_homm_alt_0(graph, openTasks, patternGraph.alternatives[(int)Rule_homm.homm_AltNums.@alt_0].alternativeCases);
                        taskFor_alt_0.homm_node_a = candidate_homm_node_a;
                        taskFor_alt_0.homm_node_b = candidate_homm_node_b;
                        openTasks.Push(taskFor_alt_0);
                        uint prevGlobal__candidate_homm_node_a;
                        prevGlobal__candidate_homm_node_a = candidate_homm_node_a.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_node_a.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_homm_node_b;
                        prevGlobal__candidate_homm_node_b = candidate_homm_node_b.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_node_b.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_homm_edge__edge0;
                        prevGlobal__candidate_homm_edge__edge0 = candidate_homm_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_homm_edge__edge1;
                        prevGlobal__candidate_homm_edge__edge1 = candidate_homm_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_homm_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for alt_0
                        openTasks.Pop();
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_homm.homm_NodeNums.@a] = candidate_homm_node_a;
                                match.Nodes[(int)Rule_homm.homm_NodeNums.@b] = candidate_homm_node_b;
                                match.Edges[(int)Rule_homm.homm_EdgeNums.@_edge0] = candidate_homm_edge__edge0;
                                match.Edges[(int)Rule_homm.homm_EdgeNums.@_edge1] = candidate_homm_edge__edge1;
                                match.EmbeddedGraphs[((int)Rule_homm.homm_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                                matches.matchesList.PositionWasFilledFixIt();
                            }
                            matchesList.Clear();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                            {
                                candidate_homm_edge__edge1.flags = candidate_homm_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge1;
                                candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge0;
                                candidate_homm_node_b.flags = candidate_homm_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_b;
                                candidate_homm_node_a.flags = candidate_homm_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_a;
                                if(negLevel <= MAX_NEG_LEVEL) {
                                    candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_edge__edge0;
                                } else { 
                                    if(prev__candidate_homm_edge__edge0==0) {
                                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_edge__edge0);
                                    }
                                }
                                return matches;
                            }
                            candidate_homm_edge__edge1.flags = candidate_homm_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge1;
                            candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge0;
                            candidate_homm_node_b.flags = candidate_homm_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_b;
                            candidate_homm_node_a.flags = candidate_homm_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_a;
                            continue;
                        }
                        candidate_homm_node_a.flags = candidate_homm_node_a.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_a;
                        candidate_homm_node_b.flags = candidate_homm_node_b.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_node_b;
                        candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge0;
                        candidate_homm_edge__edge1.flags = candidate_homm_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_edge__edge1;
                    }
                    while( (candidate_homm_edge__edge1 = candidate_homm_edge__edge1.outNext) != head_candidate_homm_edge__edge1 );
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_homm_edge__edge0.flags = candidate_homm_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_edge__edge0;
                } else { 
                    if(prev__candidate_homm_edge__edge0==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_edge__edge0);
                    }
                }
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
        public LGSPNode homm_node_b;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case homm_alt_0_case1 
            do {
                patternGraph = patternGraphs[(int)Rule_homm.homm_alt_0_CaseNums.@case1];
                // SubPreset homm_node_a 
                LGSPNode candidate_homm_node_a = homm_node_a;
                // SubPreset homm_node_b 
                LGSPNode candidate_homm_node_b = homm_node_b;
                // Extend Outgoing homm_alt_0_case1_edge__edge0 from homm_node_a 
                LGSPEdge head_candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_node_a.outhead;
                if(head_candidate_homm_alt_0_case1_edge__edge0 != null)
                {
                    LGSPEdge candidate_homm_alt_0_case1_edge__edge0 = head_candidate_homm_alt_0_case1_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_homm_alt_0_case1_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_homm_alt_0_case1_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_homm_alt_0_case1_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_alt_0_case1_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_homm_alt_0_case1_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_homm_alt_0_case1_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_alt_0_case1_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_homm_alt_0_case1_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_homm_alt_0_case1_edge__edge0,candidate_homm_alt_0_case1_edge__edge0);
                        }
                        // Implicit Target homm_alt_0_case1_node_b2 from homm_alt_0_case1_edge__edge0 
                        LGSPNode candidate_homm_alt_0_case1_node_b2 = candidate_homm_alt_0_case1_edge__edge0.target;
                        if(!NodeType_B.isMyType[candidate_homm_alt_0_case1_node_b2.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case1_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_homm_alt_0_case1_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN && candidate_homm_alt_0_case1_node_b2!=candidate_homm_node_b)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case1_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing homm_alt_0_case1_edge__edge1 from homm_alt_0_case1_node_b2 
                        LGSPEdge head_candidate_homm_alt_0_case1_edge__edge1 = candidate_homm_alt_0_case1_node_b2.outhead;
                        if(head_candidate_homm_alt_0_case1_edge__edge1 != null)
                        {
                            LGSPEdge candidate_homm_alt_0_case1_edge__edge1 = head_candidate_homm_alt_0_case1_edge__edge1;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_homm_alt_0_case1_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                if(candidate_homm_alt_0_case1_edge__edge1.target != candidate_homm_node_a) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_homm_alt_0_case1_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_alt_0_case1_edge__edge1))
                                    && candidate_homm_alt_0_case1_edge__edge1==candidate_homm_alt_0_case1_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_homm_alt_0_case1_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[2], new LGSPMatch[0]);
                                    match.patternGraph = patternGraph;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@a] = candidate_homm_node_a;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@b2] = candidate_homm_alt_0_case1_node_b2;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@b] = candidate_homm_node_b;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge0] = candidate_homm_alt_0_case1_edge__edge0;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge1] = candidate_homm_alt_0_case1_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case1_edge__edge0==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                prevGlobal__candidate_homm_alt_0_case1_node_b2 = candidate_homm_alt_0_case1_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case1_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                prevGlobal__candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_alt_0_case1_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case1_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                                prevGlobal__candidate_homm_alt_0_case1_edge__edge1 = candidate_homm_alt_0_case1_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case1_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[2], new LGSPMatch[0+0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@a] = candidate_homm_node_a;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@b2] = candidate_homm_alt_0_case1_node_b2;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case1_NodeNums.@b] = candidate_homm_node_b;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge0] = candidate_homm_alt_0_case1_edge__edge0;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case1_EdgeNums.@_edge1] = candidate_homm_alt_0_case1_edge__edge1;
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<LGSPMatch>>();
                                    } else {
                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_homm_alt_0_case1_edge__edge1.flags = candidate_homm_alt_0_case1_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                                        candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                        candidate_homm_alt_0_case1_node_b2.flags = candidate_homm_alt_0_case1_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case1_edge__edge0==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_homm_alt_0_case1_edge__edge1.flags = candidate_homm_alt_0_case1_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                                    candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                    candidate_homm_alt_0_case1_node_b2.flags = candidate_homm_alt_0_case1_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                    continue;
                                }
                                candidate_homm_alt_0_case1_node_b2.flags = candidate_homm_alt_0_case1_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_node_b2;
                                candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge0;
                                candidate_homm_alt_0_case1_edge__edge1.flags = candidate_homm_alt_0_case1_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case1_edge__edge1;
                            }
                            while( (candidate_homm_alt_0_case1_edge__edge1 = candidate_homm_alt_0_case1_edge__edge1.outNext) != head_candidate_homm_alt_0_case1_edge__edge1 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_homm_alt_0_case1_edge__edge0.flags = candidate_homm_alt_0_case1_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case1_edge__edge0;
                        } else { 
                            if(prev__candidate_homm_alt_0_case1_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case1_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_homm_alt_0_case1_edge__edge0 = candidate_homm_alt_0_case1_edge__edge0.outNext) != head_candidate_homm_alt_0_case1_edge__edge0 );
                }
            } while(false);
            if(matchesList==foundPartialMatches) {
                matchesList = new List<Stack<LGSPMatch>>();
            } else {
                foreach(Stack<LGSPMatch> match in matchesList) {
                    foundPartialMatches.Add(match);
                }
                matchesList.Clear();
            }
            // Alternative case homm_alt_0_case2 
            do {
                patternGraph = patternGraphs[(int)Rule_homm.homm_alt_0_CaseNums.@case2];
                // SubPreset homm_node_a 
                LGSPNode candidate_homm_node_a = homm_node_a;
                // Extend Outgoing homm_alt_0_case2_edge__edge0 from homm_node_a 
                LGSPEdge head_candidate_homm_alt_0_case2_edge__edge0 = candidate_homm_node_a.outhead;
                if(head_candidate_homm_alt_0_case2_edge__edge0 != null)
                {
                    LGSPEdge candidate_homm_alt_0_case2_edge__edge0 = head_candidate_homm_alt_0_case2_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_homm_alt_0_case2_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_homm_alt_0_case2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_homm_alt_0_case2_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_homm_alt_0_case2_edge__edge0 = candidate_homm_alt_0_case2_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_homm_alt_0_case2_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_homm_alt_0_case2_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_alt_0_case2_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_homm_alt_0_case2_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_homm_alt_0_case2_edge__edge0,candidate_homm_alt_0_case2_edge__edge0);
                        }
                        // Implicit Target homm_alt_0_case2_node_b2 from homm_alt_0_case2_edge__edge0 
                        LGSPNode candidate_homm_alt_0_case2_node_b2 = candidate_homm_alt_0_case2_edge__edge0.target;
                        if(!NodeType_B.isMyType[candidate_homm_alt_0_case2_node_b2.type.TypeID]) {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_homm_alt_0_case2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                            } else { 
                                if(prev__candidate_homm_alt_0_case2_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing homm_alt_0_case2_edge__edge1 from homm_alt_0_case2_node_b2 
                        LGSPEdge head_candidate_homm_alt_0_case2_edge__edge1 = candidate_homm_alt_0_case2_node_b2.outhead;
                        if(head_candidate_homm_alt_0_case2_edge__edge1 != null)
                        {
                            LGSPEdge candidate_homm_alt_0_case2_edge__edge1 = head_candidate_homm_alt_0_case2_edge__edge1;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_homm_alt_0_case2_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                if(candidate_homm_alt_0_case2_edge__edge1.target != candidate_homm_node_a) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_homm_alt_0_case2_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_homm_alt_0_case2_edge__edge1))
                                    && candidate_homm_alt_0_case2_edge__edge1==candidate_homm_alt_0_case2_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_homm_alt_0_case2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
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
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@a] = candidate_homm_node_a;
                                    match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@b2] = candidate_homm_alt_0_case2_node_b2;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge0] = candidate_homm_alt_0_case2_edge__edge0;
                                    match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge1] = candidate_homm_alt_0_case2_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case2_edge__edge0==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                prevGlobal__candidate_homm_alt_0_case2_node_b2 = candidate_homm_alt_0_case2_node_b2.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case2_node_b2.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                prevGlobal__candidate_homm_alt_0_case2_edge__edge0 = candidate_homm_alt_0_case2_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case2_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                                prevGlobal__candidate_homm_alt_0_case2_edge__edge1 = candidate_homm_alt_0_case2_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_homm_alt_0_case2_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[2], new LGSPMatch[0+0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@a] = candidate_homm_node_a;
                                        match.Nodes[(int)Rule_homm.homm_alt_0_case2_NodeNums.@b2] = candidate_homm_alt_0_case2_node_b2;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge0] = candidate_homm_alt_0_case2_edge__edge0;
                                        match.Edges[(int)Rule_homm.homm_alt_0_case2_EdgeNums.@_edge1] = candidate_homm_alt_0_case2_edge__edge1;
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<LGSPMatch>>();
                                    } else {
                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_homm_alt_0_case2_edge__edge1.flags = candidate_homm_alt_0_case2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                                        candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                        candidate_homm_alt_0_case2_node_b2.flags = candidate_homm_alt_0_case2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                                        } else { 
                                            if(prev__candidate_homm_alt_0_case2_edge__edge0==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_homm_alt_0_case2_edge__edge1.flags = candidate_homm_alt_0_case2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                                    candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                    candidate_homm_alt_0_case2_node_b2.flags = candidate_homm_alt_0_case2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                    continue;
                                }
                                candidate_homm_alt_0_case2_node_b2.flags = candidate_homm_alt_0_case2_node_b2.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_node_b2;
                                candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge0;
                                candidate_homm_alt_0_case2_edge__edge1.flags = candidate_homm_alt_0_case2_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_homm_alt_0_case2_edge__edge1;
                            }
                            while( (candidate_homm_alt_0_case2_edge__edge1 = candidate_homm_alt_0_case2_edge__edge1.outNext) != head_candidate_homm_alt_0_case2_edge__edge1 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_homm_alt_0_case2_edge__edge0.flags = candidate_homm_alt_0_case2_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_homm_alt_0_case2_edge__edge0;
                        } else { 
                            if(prev__candidate_homm_alt_0_case2_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_homm_alt_0_case2_edge__edge0);
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

    public class Action_XtoAorB : LGSPAction
    {
        public Action_XtoAorB() {
            rulePattern = Rule_XtoAorB.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 1+0);
        }

        public override string Name { get { return "XtoAorB"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_XtoAorB instance = new Action_XtoAorB();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Lookup XtoAorB_node_x 
            foreach(NodeType type_candidate_XtoAorB_node_x in NodeType_Node.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_XtoAorB_node_x = type_candidate_XtoAorB_node_x.TypeID;
                for(LGSPNode head_candidate_XtoAorB_node_x = graph.nodesByTypeHeads[type_id_candidate_XtoAorB_node_x], candidate_XtoAorB_node_x = head_candidate_XtoAorB_node_x.typeNext; candidate_XtoAorB_node_x != head_candidate_XtoAorB_node_x; candidate_XtoAorB_node_x = candidate_XtoAorB_node_x.typeNext)
                {
                    // Push subpattern matching task for _subpattern0
                    PatternAction_toAorB taskFor__subpattern0 = new PatternAction_toAorB(graph, openTasks);
                    taskFor__subpattern0.toAorB_node_x = candidate_XtoAorB_node_x;
                    openTasks.Push(taskFor__subpattern0);
                    uint prevGlobal__candidate_XtoAorB_node_x;
                    prevGlobal__candidate_XtoAorB_node_x = candidate_XtoAorB_node_x.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_XtoAorB_node_x.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for _subpattern0
                    openTasks.Pop();
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                        foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                        {
                            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                            match.patternGraph = rulePattern.patternGraph;
                            match.Nodes[(int)Rule_XtoAorB.XtoAorB_NodeNums.@x] = candidate_XtoAorB_node_x;
                            match.EmbeddedGraphs[(int)Rule_XtoAorB.XtoAorB_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                            matches.matchesList.PositionWasFilledFixIt();
                        }
                        matchesList.Clear();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            candidate_XtoAorB_node_x.flags = candidate_XtoAorB_node_x.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_XtoAorB_node_x;
                            return matches;
                        }
                        candidate_XtoAorB_node_x.flags = candidate_XtoAorB_node_x.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_XtoAorB_node_x;
                        continue;
                    }
                    candidate_XtoAorB_node_x.flags = candidate_XtoAorB_node_x.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_XtoAorB_node_x;
                }
            }
            return matches;
        }
    }


    public class AlternativesActions : LGSPActions
    {
        public AlternativesActions(LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
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
            actions.Add("createA", (LGSPAction) Action_createA.Instance);
            actions.Add("createB", (LGSPAction) Action_createB.Instance);
            actions.Add("createC", (LGSPAction) Action_createC.Instance);
            actions.Add("createAtoB", (LGSPAction) Action_createAtoB.Instance);
            actions.Add("leer", (LGSPAction) Action_leer.Instance);
            actions.Add("AorB", (LGSPAction) Action_AorB.Instance);
            actions.Add("AandnotCorB", (LGSPAction) Action_AandnotCorB.Instance);
            actions.Add("AorBorC", (LGSPAction) Action_AorBorC.Instance);
            actions.Add("AtoAorB", (LGSPAction) Action_AtoAorB.Instance);
            actions.Add("createComplex", (LGSPAction) Action_createComplex.Instance);
            actions.Add("Complex", (LGSPAction) Action_Complex.Instance);
            actions.Add("ComplexMax", (LGSPAction) Action_ComplexMax.Instance);
            actions.Add("createABA", (LGSPAction) Action_createABA.Instance);
            actions.Add("homm", (LGSPAction) Action_homm.Instance);
            actions.Add("XtoAorB", (LGSPAction) Action_XtoAorB.Instance);
        }

        public override String Name { get { return "AlternativesActions"; } }
        public override String ModelMD5Hash { get { return "e6c5215b8441bdc431ed9a3538e41e73"; } }
    }
}