using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_edge1;

namespace de.unika.ipd.grGen.Action_edge1
{
	public class Rule_init : LGSPRulePattern
	{
		private static Rule_init instance = null;
		public static Rule_init Instance { get { if (instance==null) { instance = new Rule_init(); instance.initialize(); } return instance; } }

		public enum init_NodeNums { };
		public enum init_EdgeNums { };
		public enum init_SubNums { };
		public enum init_AltNums { };

#if INITIAL_WARMUP
		public Rule_init()
#else
		private Rule_init()
#endif
		{
			name = "init";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_init;
			pat_init = new PatternGraph(
				"init",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] 			);

			patternGraph = pat_init;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node_x = Node_Node.CreateNode(graph);
			Node_Node node_z = Node_Node.CreateNode(graph);
			Node_Node node_y = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_y, node_y);
			Edge_UEdge edge__edge1 = Edge_UEdge.CreateEdge(graph, node_y, node_z);
			Edge_UEdge edge__edge0 = Edge_UEdge.CreateEdge(graph, node_x, node_y);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node_x = Node_Node.CreateNode(graph);
			Node_Node node_z = Node_Node.CreateNode(graph);
			Node_Node node_y = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge2 = Edge_Edge.CreateEdge(graph, node_y, node_y);
			Edge_UEdge edge__edge1 = Edge_UEdge.CreateEdge(graph, node_y, node_z);
			Edge_UEdge edge__edge0 = Edge_UEdge.CreateEdge(graph, node_x, node_y);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "x", "z", "y" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge2", "_edge1", "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_init2 : LGSPRulePattern
	{
		private static Rule_init2 instance = null;
		public static Rule_init2 Instance { get { if (instance==null) { instance = new Rule_init2(); instance.initialize(); } return instance; } }

		public enum init2_NodeNums { };
		public enum init2_EdgeNums { };
		public enum init2_SubNums { };
		public enum init2_AltNums { };

#if INITIAL_WARMUP
		public Rule_init2()
#else
		private Rule_init2()
#endif
		{
			name = "init2";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_init2;
			pat_init2 = new PatternGraph(
				"init2",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] 			);

			patternGraph = pat_init2;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node__node1 = Node_Node.CreateNode(graph);
			Node_Node node__node2 = Node_Node.CreateNode(graph);
			Node_Node node__node0 = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node__node2, node__node1);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node__node1 = Node_Node.CreateNode(graph);
			Node_Node node__node2 = Node_Node.CreateNode(graph);
			Node_Node node__node0 = Node_Node.CreateNode(graph);
			Edge_Edge edge__edge1 = Edge_Edge.CreateEdge(graph, node__node2, node__node1);
			Edge_Edge edge__edge0 = Edge_Edge.CreateEdge(graph, node__node0, node__node1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node1", "_node2", "_node0" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1", "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_init3 : LGSPRulePattern
	{
		private static Rule_init3 instance = null;
		public static Rule_init3 Instance { get { if (instance==null) { instance = new Rule_init3(); instance.initialize(); } return instance; } }

		public enum init3_NodeNums { };
		public enum init3_EdgeNums { };
		public enum init3_SubNums { };
		public enum init3_AltNums { };

#if INITIAL_WARMUP
		public Rule_init3()
#else
		private Rule_init3()
#endif
		{
			name = "init3";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_init3;
			pat_init3 = new PatternGraph(
				"init3",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] 			);

			patternGraph = pat_init3;
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node_x = Node_Node.CreateNode(graph);
			Node_Node node_z = Node_Node.CreateNode(graph);
			Node_Node node_y = Node_Node.CreateNode(graph);
			Edge_UEdge edge__edge2 = Edge_UEdge.CreateEdge(graph, node_z, node_x);
			Edge_UEdge edge__edge1 = Edge_UEdge.CreateEdge(graph, node_y, node_z);
			Edge_UEdge edge__edge0 = Edge_UEdge.CreateEdge(graph, node_x, node_y);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			Node_Node node_x = Node_Node.CreateNode(graph);
			Node_Node node_z = Node_Node.CreateNode(graph);
			Node_Node node_y = Node_Node.CreateNode(graph);
			Edge_UEdge edge__edge2 = Edge_UEdge.CreateEdge(graph, node_z, node_x);
			Edge_UEdge edge__edge1 = Edge_UEdge.CreateEdge(graph, node_y, node_z);
			Edge_UEdge edge__edge0 = Edge_UEdge.CreateEdge(graph, node_x, node_y);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "x", "z", "y" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge2", "_edge1", "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_findUndirectedEdge : LGSPRulePattern
	{
		private static Rule_findUndirectedEdge instance = null;
		public static Rule_findUndirectedEdge Instance { get { if (instance==null) { instance = new Rule_findUndirectedEdge(); instance.initialize(); } return instance; } }

		public static NodeType[] findUndirectedEdge_node_x_AllowedTypes = null;
		public static NodeType[] findUndirectedEdge_node_y_AllowedTypes = null;
		public static bool[] findUndirectedEdge_node_x_IsAllowedType = null;
		public static bool[] findUndirectedEdge_node_y_IsAllowedType = null;
		public static EdgeType[] findUndirectedEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findUndirectedEdge_edge__edge0_IsAllowedType = null;
		public enum findUndirectedEdge_NodeNums { @x, @y, };
		public enum findUndirectedEdge_EdgeNums { @_edge0, };
		public enum findUndirectedEdge_SubNums { };
		public enum findUndirectedEdge_AltNums { };

#if INITIAL_WARMUP
		public Rule_findUndirectedEdge()
#else
		private Rule_findUndirectedEdge()
#endif
		{
			name = "findUndirectedEdge";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findUndirectedEdge;
			PatternNode findUndirectedEdge_node_x = new PatternNode((int) NodeTypes.@Node, "findUndirectedEdge_node_x", "x", findUndirectedEdge_node_x_AllowedTypes, findUndirectedEdge_node_x_IsAllowedType, 5.5F, -1);
			PatternNode findUndirectedEdge_node_y = new PatternNode((int) NodeTypes.@Node, "findUndirectedEdge_node_y", "y", findUndirectedEdge_node_y_AllowedTypes, findUndirectedEdge_node_y_IsAllowedType, 5.5F, -1);
			PatternEdge findUndirectedEdge_edge__edge0 = new PatternEdge(findUndirectedEdge_node_x, findUndirectedEdge_node_y, false, (int) EdgeTypes.@UEdge, "findUndirectedEdge_edge__edge0", "_edge0", findUndirectedEdge_edge__edge0_AllowedTypes, findUndirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1);
			pat_findUndirectedEdge = new PatternGraph(
				"findUndirectedEdge",
				"",
				false,
				new PatternNode[] { findUndirectedEdge_node_x, findUndirectedEdge_node_y }, 
				new PatternEdge[] { findUndirectedEdge_edge__edge0 }, 
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
				}
			);
			findUndirectedEdge_node_x.PointOfDefinition = pat_findUndirectedEdge;
			findUndirectedEdge_node_y.PointOfDefinition = pat_findUndirectedEdge;
			findUndirectedEdge_edge__edge0.PointOfDefinition = pat_findUndirectedEdge;

			patternGraph = pat_findUndirectedEdge;
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

	public class Rule_findArbitraryEdge : LGSPRulePattern
	{
		private static Rule_findArbitraryEdge instance = null;
		public static Rule_findArbitraryEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryEdge(); instance.initialize(); } return instance; } }

		public static NodeType[] findArbitraryEdge_node_x_AllowedTypes = null;
		public static NodeType[] findArbitraryEdge_node_y_AllowedTypes = null;
		public static bool[] findArbitraryEdge_node_x_IsAllowedType = null;
		public static bool[] findArbitraryEdge_node_y_IsAllowedType = null;
		public static EdgeType[] findArbitraryEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findArbitraryEdge_edge__edge0_IsAllowedType = null;
		public enum findArbitraryEdge_NodeNums { @x, @y, };
		public enum findArbitraryEdge_EdgeNums { @_edge0, };
		public enum findArbitraryEdge_SubNums { };
		public enum findArbitraryEdge_AltNums { };

#if INITIAL_WARMUP
		public Rule_findArbitraryEdge()
#else
		private Rule_findArbitraryEdge()
#endif
		{
			name = "findArbitraryEdge";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findArbitraryEdge;
			PatternNode findArbitraryEdge_node_x = new PatternNode((int) NodeTypes.@Node, "findArbitraryEdge_node_x", "x", findArbitraryEdge_node_x_AllowedTypes, findArbitraryEdge_node_x_IsAllowedType, 5.5F, -1);
			PatternNode findArbitraryEdge_node_y = new PatternNode((int) NodeTypes.@Node, "findArbitraryEdge_node_y", "y", findArbitraryEdge_node_y_AllowedTypes, findArbitraryEdge_node_y_IsAllowedType, 5.5F, -1);
			PatternEdge findArbitraryEdge_edge__edge0 = new PatternEdge(findArbitraryEdge_node_x, findArbitraryEdge_node_y, false, (int) EdgeTypes.@AEdge, "findArbitraryEdge_edge__edge0", "_edge0", findArbitraryEdge_edge__edge0_AllowedTypes, findArbitraryEdge_edge__edge0_IsAllowedType, 5.5F, -1);
			pat_findArbitraryEdge = new PatternGraph(
				"findArbitraryEdge",
				"",
				false,
				new PatternNode[] { findArbitraryEdge_node_x, findArbitraryEdge_node_y }, 
				new PatternEdge[] { findArbitraryEdge_edge__edge0 }, 
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
				}
			);
			findArbitraryEdge_node_x.PointOfDefinition = pat_findArbitraryEdge;
			findArbitraryEdge_node_y.PointOfDefinition = pat_findArbitraryEdge;
			findArbitraryEdge_edge__edge0.PointOfDefinition = pat_findArbitraryEdge;

			patternGraph = pat_findArbitraryEdge;
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

	public class Rule_findArbitraryDirectedEdge : LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedEdge instance = null;
		public static Rule_findArbitraryDirectedEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedEdge(); instance.initialize(); } return instance; } }

		public static NodeType[] findArbitraryDirectedEdge_node_x_AllowedTypes = null;
		public static NodeType[] findArbitraryDirectedEdge_node_y_AllowedTypes = null;
		public static bool[] findArbitraryDirectedEdge_node_x_IsAllowedType = null;
		public static bool[] findArbitraryDirectedEdge_node_y_IsAllowedType = null;
		public static EdgeType[] findArbitraryDirectedEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findArbitraryDirectedEdge_edge__edge0_IsAllowedType = null;
		public enum findArbitraryDirectedEdge_NodeNums { @x, @y, };
		public enum findArbitraryDirectedEdge_EdgeNums { @_edge0, };
		public enum findArbitraryDirectedEdge_SubNums { };
		public enum findArbitraryDirectedEdge_AltNums { };

#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedEdge()
#else
		private Rule_findArbitraryDirectedEdge()
#endif
		{
			name = "findArbitraryDirectedEdge";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findArbitraryDirectedEdge;
			PatternNode findArbitraryDirectedEdge_node_x = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedEdge_node_x", "x", findArbitraryDirectedEdge_node_x_AllowedTypes, findArbitraryDirectedEdge_node_x_IsAllowedType, 5.5F, -1);
			PatternNode findArbitraryDirectedEdge_node_y = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedEdge_node_y", "y", findArbitraryDirectedEdge_node_y_AllowedTypes, findArbitraryDirectedEdge_node_y_IsAllowedType, 5.5F, -1);
			PatternEdge findArbitraryDirectedEdge_edge__edge0 = new PatternEdge(findArbitraryDirectedEdge_node_x, findArbitraryDirectedEdge_node_y, false, (int) EdgeTypes.@Edge, "findArbitraryDirectedEdge_edge__edge0", "_edge0", findArbitraryDirectedEdge_edge__edge0_AllowedTypes, findArbitraryDirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1);
			pat_findArbitraryDirectedEdge = new PatternGraph(
				"findArbitraryDirectedEdge",
				"",
				false,
				new PatternNode[] { findArbitraryDirectedEdge_node_x, findArbitraryDirectedEdge_node_y }, 
				new PatternEdge[] { findArbitraryDirectedEdge_edge__edge0 }, 
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
				}
			);
			findArbitraryDirectedEdge_node_x.PointOfDefinition = pat_findArbitraryDirectedEdge;
			findArbitraryDirectedEdge_node_y.PointOfDefinition = pat_findArbitraryDirectedEdge;
			findArbitraryDirectedEdge_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedEdge;

			patternGraph = pat_findArbitraryDirectedEdge;
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

	public class Rule_findArbitraryDirectedReflexiveEdge : LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedReflexiveEdge instance = null;
		public static Rule_findArbitraryDirectedReflexiveEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedReflexiveEdge(); instance.initialize(); } return instance; } }

		public static NodeType[] findArbitraryDirectedReflexiveEdge_node_x_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdge_node_x_IsAllowedType = null;
		public static EdgeType[] findArbitraryDirectedReflexiveEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdge_edge__edge0_IsAllowedType = null;
		public enum findArbitraryDirectedReflexiveEdge_NodeNums { @x, };
		public enum findArbitraryDirectedReflexiveEdge_EdgeNums { @_edge0, };
		public enum findArbitraryDirectedReflexiveEdge_SubNums { };
		public enum findArbitraryDirectedReflexiveEdge_AltNums { };

#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedReflexiveEdge()
#else
		private Rule_findArbitraryDirectedReflexiveEdge()
#endif
		{
			name = "findArbitraryDirectedReflexiveEdge";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findArbitraryDirectedReflexiveEdge;
			PatternNode findArbitraryDirectedReflexiveEdge_node_x = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedReflexiveEdge_node_x", "x", findArbitraryDirectedReflexiveEdge_node_x_AllowedTypes, findArbitraryDirectedReflexiveEdge_node_x_IsAllowedType, 5.5F, -1);
			PatternEdge findArbitraryDirectedReflexiveEdge_edge__edge0 = new PatternEdge(findArbitraryDirectedReflexiveEdge_node_x, findArbitraryDirectedReflexiveEdge_node_x, false, (int) EdgeTypes.@Edge, "findArbitraryDirectedReflexiveEdge_edge__edge0", "_edge0", findArbitraryDirectedReflexiveEdge_edge__edge0_AllowedTypes, findArbitraryDirectedReflexiveEdge_edge__edge0_IsAllowedType, 5.5F, -1);
			pat_findArbitraryDirectedReflexiveEdge = new PatternGraph(
				"findArbitraryDirectedReflexiveEdge",
				"",
				false,
				new PatternNode[] { findArbitraryDirectedReflexiveEdge_node_x }, 
				new PatternEdge[] { findArbitraryDirectedReflexiveEdge_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				}
			);
			findArbitraryDirectedReflexiveEdge_node_x.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdge;
			findArbitraryDirectedReflexiveEdge_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdge;

			patternGraph = pat_findArbitraryDirectedReflexiveEdge;
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

	public class Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne instance = null;
		public static Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne(); instance.initialize(); } return instance; } }

		public static NodeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_AllowedTypes = null;
		public static NodeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_IsAllowedType = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_IsAllowedType = null;
		public static EdgeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_AllowedTypes = null;
		public static EdgeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_IsAllowedType = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_IsAllowedType = null;
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_NodeNums { @x, @y, };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_EdgeNums { @_edge0, @_edge1, };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_SubNums { };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_AltNums { };

#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne()
#else
		private Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne()
#endif
		{
			name = "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
			PatternNode findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x", "x", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_IsAllowedType, 5.5F, -1);
			PatternNode findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y", "y", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_IsAllowedType, 5.5F, -1);
			PatternEdge findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = new PatternEdge(findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y, false, (int) EdgeTypes.@UEdge, "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0", "_edge0", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = new PatternEdge(findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y, false, (int) EdgeTypes.@Edge, "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1", "_edge1", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_IsAllowedType, 5.5F, -1);
			pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne = new PatternGraph(
				"findArbitraryDirectedReflexiveEdgeAfterUndirectedOne",
				"",
				false,
				new PatternNode[] { findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y }, 
				new PatternEdge[] { findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 }, 
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
				}
			);
			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;

			patternGraph = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
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

	public class Rule_findArbitraryDirectedTriple : LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedTriple instance = null;
		public static Rule_findArbitraryDirectedTriple Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedTriple(); instance.initialize(); } return instance; } }

		public static NodeType[] findArbitraryDirectedTriple_node__node0_AllowedTypes = null;
		public static NodeType[] findArbitraryDirectedTriple_node__node1_AllowedTypes = null;
		public static NodeType[] findArbitraryDirectedTriple_node__node2_AllowedTypes = null;
		public static bool[] findArbitraryDirectedTriple_node__node0_IsAllowedType = null;
		public static bool[] findArbitraryDirectedTriple_node__node1_IsAllowedType = null;
		public static bool[] findArbitraryDirectedTriple_node__node2_IsAllowedType = null;
		public static EdgeType[] findArbitraryDirectedTriple_edge__edge0_AllowedTypes = null;
		public static EdgeType[] findArbitraryDirectedTriple_edge__edge1_AllowedTypes = null;
		public static bool[] findArbitraryDirectedTriple_edge__edge0_IsAllowedType = null;
		public static bool[] findArbitraryDirectedTriple_edge__edge1_IsAllowedType = null;
		public enum findArbitraryDirectedTriple_NodeNums { @_node0, @_node1, @_node2, };
		public enum findArbitraryDirectedTriple_EdgeNums { @_edge0, @_edge1, };
		public enum findArbitraryDirectedTriple_SubNums { };
		public enum findArbitraryDirectedTriple_AltNums { };

#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedTriple()
#else
		private Rule_findArbitraryDirectedTriple()
#endif
		{
			name = "findArbitraryDirectedTriple";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findArbitraryDirectedTriple;
			PatternNode findArbitraryDirectedTriple_node__node0 = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedTriple_node__node0", "_node0", findArbitraryDirectedTriple_node__node0_AllowedTypes, findArbitraryDirectedTriple_node__node0_IsAllowedType, 5.5F, -1);
			PatternNode findArbitraryDirectedTriple_node__node1 = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedTriple_node__node1", "_node1", findArbitraryDirectedTriple_node__node1_AllowedTypes, findArbitraryDirectedTriple_node__node1_IsAllowedType, 5.5F, -1);
			PatternNode findArbitraryDirectedTriple_node__node2 = new PatternNode((int) NodeTypes.@Node, "findArbitraryDirectedTriple_node__node2", "_node2", findArbitraryDirectedTriple_node__node2_AllowedTypes, findArbitraryDirectedTriple_node__node2_IsAllowedType, 5.5F, -1);
			PatternEdge findArbitraryDirectedTriple_edge__edge0 = new PatternEdge(findArbitraryDirectedTriple_node__node0, findArbitraryDirectedTriple_node__node1, false, (int) EdgeTypes.@Edge, "findArbitraryDirectedTriple_edge__edge0", "_edge0", findArbitraryDirectedTriple_edge__edge0_AllowedTypes, findArbitraryDirectedTriple_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge findArbitraryDirectedTriple_edge__edge1 = new PatternEdge(findArbitraryDirectedTriple_node__node1, findArbitraryDirectedTriple_node__node2, false, (int) EdgeTypes.@Edge, "findArbitraryDirectedTriple_edge__edge1", "_edge1", findArbitraryDirectedTriple_edge__edge1_AllowedTypes, findArbitraryDirectedTriple_edge__edge1_IsAllowedType, 5.5F, -1);
			pat_findArbitraryDirectedTriple = new PatternGraph(
				"findArbitraryDirectedTriple",
				"",
				false,
				new PatternNode[] { findArbitraryDirectedTriple_node__node0, findArbitraryDirectedTriple_node__node1, findArbitraryDirectedTriple_node__node2 }, 
				new PatternEdge[] { findArbitraryDirectedTriple_edge__edge0, findArbitraryDirectedTriple_edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				}
			);
			findArbitraryDirectedTriple_node__node0.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_node__node1.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_node__node2.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_edge__edge1.PointOfDefinition = pat_findArbitraryDirectedTriple;

			patternGraph = pat_findArbitraryDirectedTriple;
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

	public class Rule_findDirectedEdge : LGSPRulePattern
	{
		private static Rule_findDirectedEdge instance = null;
		public static Rule_findDirectedEdge Instance { get { if (instance==null) { instance = new Rule_findDirectedEdge(); instance.initialize(); } return instance; } }

		public static NodeType[] findDirectedEdge_node_x_AllowedTypes = null;
		public static NodeType[] findDirectedEdge_node_y_AllowedTypes = null;
		public static bool[] findDirectedEdge_node_x_IsAllowedType = null;
		public static bool[] findDirectedEdge_node_y_IsAllowedType = null;
		public static EdgeType[] findDirectedEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findDirectedEdge_edge__edge0_IsAllowedType = null;
		public enum findDirectedEdge_NodeNums { @x, @y, };
		public enum findDirectedEdge_EdgeNums { @_edge0, };
		public enum findDirectedEdge_SubNums { };
		public enum findDirectedEdge_AltNums { };

#if INITIAL_WARMUP
		public Rule_findDirectedEdge()
#else
		private Rule_findDirectedEdge()
#endif
		{
			name = "findDirectedEdge";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findDirectedEdge;
			PatternNode findDirectedEdge_node_x = new PatternNode((int) NodeTypes.@Node, "findDirectedEdge_node_x", "x", findDirectedEdge_node_x_AllowedTypes, findDirectedEdge_node_x_IsAllowedType, 5.5F, -1);
			PatternNode findDirectedEdge_node_y = new PatternNode((int) NodeTypes.@Node, "findDirectedEdge_node_y", "y", findDirectedEdge_node_y_AllowedTypes, findDirectedEdge_node_y_IsAllowedType, 5.5F, -1);
			PatternEdge findDirectedEdge_edge__edge0 = new PatternEdge(findDirectedEdge_node_x, findDirectedEdge_node_y, true, (int) EdgeTypes.@Edge, "findDirectedEdge_edge__edge0", "_edge0", findDirectedEdge_edge__edge0_AllowedTypes, findDirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1);
			pat_findDirectedEdge = new PatternGraph(
				"findDirectedEdge",
				"",
				false,
				new PatternNode[] { findDirectedEdge_node_x, findDirectedEdge_node_y }, 
				new PatternEdge[] { findDirectedEdge_edge__edge0 }, 
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
				}
			);
			findDirectedEdge_node_x.PointOfDefinition = pat_findDirectedEdge;
			findDirectedEdge_node_y.PointOfDefinition = pat_findDirectedEdge;
			findDirectedEdge_edge__edge0.PointOfDefinition = pat_findDirectedEdge;

			patternGraph = pat_findDirectedEdge;
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

	public class Rule_findTripleCircle : LGSPRulePattern
	{
		private static Rule_findTripleCircle instance = null;
		public static Rule_findTripleCircle Instance { get { if (instance==null) { instance = new Rule_findTripleCircle(); instance.initialize(); } return instance; } }

		public static NodeType[] findTripleCircle_node_x_AllowedTypes = null;
		public static NodeType[] findTripleCircle_node_y_AllowedTypes = null;
		public static NodeType[] findTripleCircle_node_z_AllowedTypes = null;
		public static bool[] findTripleCircle_node_x_IsAllowedType = null;
		public static bool[] findTripleCircle_node_y_IsAllowedType = null;
		public static bool[] findTripleCircle_node_z_IsAllowedType = null;
		public static EdgeType[] findTripleCircle_edge__edge0_AllowedTypes = null;
		public static EdgeType[] findTripleCircle_edge__edge1_AllowedTypes = null;
		public static EdgeType[] findTripleCircle_edge__edge2_AllowedTypes = null;
		public static bool[] findTripleCircle_edge__edge0_IsAllowedType = null;
		public static bool[] findTripleCircle_edge__edge1_IsAllowedType = null;
		public static bool[] findTripleCircle_edge__edge2_IsAllowedType = null;
		public enum findTripleCircle_NodeNums { @x, @y, @z, };
		public enum findTripleCircle_EdgeNums { @_edge0, @_edge1, @_edge2, };
		public enum findTripleCircle_SubNums { };
		public enum findTripleCircle_AltNums { };

#if INITIAL_WARMUP
		public Rule_findTripleCircle()
#else
		private Rule_findTripleCircle()
#endif
		{
			name = "findTripleCircle";
			isSubpattern = false;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}
		public override void initialize()
		{
			PatternGraph pat_findTripleCircle;
			PatternNode findTripleCircle_node_x = new PatternNode((int) NodeTypes.@Node, "findTripleCircle_node_x", "x", findTripleCircle_node_x_AllowedTypes, findTripleCircle_node_x_IsAllowedType, 5.5F, -1);
			PatternNode findTripleCircle_node_y = new PatternNode((int) NodeTypes.@Node, "findTripleCircle_node_y", "y", findTripleCircle_node_y_AllowedTypes, findTripleCircle_node_y_IsAllowedType, 5.5F, -1);
			PatternNode findTripleCircle_node_z = new PatternNode((int) NodeTypes.@Node, "findTripleCircle_node_z", "z", findTripleCircle_node_z_AllowedTypes, findTripleCircle_node_z_IsAllowedType, 5.5F, -1);
			PatternEdge findTripleCircle_edge__edge0 = new PatternEdge(findTripleCircle_node_x, findTripleCircle_node_y, false, (int) EdgeTypes.@UEdge, "findTripleCircle_edge__edge0", "_edge0", findTripleCircle_edge__edge0_AllowedTypes, findTripleCircle_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge findTripleCircle_edge__edge1 = new PatternEdge(findTripleCircle_node_y, findTripleCircle_node_z, false, (int) EdgeTypes.@UEdge, "findTripleCircle_edge__edge1", "_edge1", findTripleCircle_edge__edge1_AllowedTypes, findTripleCircle_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternEdge findTripleCircle_edge__edge2 = new PatternEdge(findTripleCircle_node_z, findTripleCircle_node_x, false, (int) EdgeTypes.@UEdge, "findTripleCircle_edge__edge2", "_edge2", findTripleCircle_edge__edge2_AllowedTypes, findTripleCircle_edge__edge2_IsAllowedType, 5.5F, -1);
			pat_findTripleCircle = new PatternGraph(
				"findTripleCircle",
				"",
				false,
				new PatternNode[] { findTripleCircle_node_x, findTripleCircle_node_y, findTripleCircle_node_z }, 
				new PatternEdge[] { findTripleCircle_edge__edge0, findTripleCircle_edge__edge1, findTripleCircle_edge__edge2 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				}
			);
			findTripleCircle_node_x.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_node_y.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_node_z.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_edge__edge0.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_edge__edge1.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_edge__edge2.PointOfDefinition = pat_findTripleCircle;

			patternGraph = pat_findTripleCircle;
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


    public class Action_init : LGSPAction
    {
        public Action_init() {
            rulePattern = Rule_init.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "init"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_init instance = new Action_init();

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

    public class Action_init2 : LGSPAction
    {
        public Action_init2() {
            rulePattern = Rule_init2.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "init2"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_init2 instance = new Action_init2();

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

    public class Action_init3 : LGSPAction
    {
        public Action_init3() {
            rulePattern = Rule_init3.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 0, 0, 0+0);
        }

        public override string Name { get { return "init3"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_init3 instance = new Action_init3();

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

    public class Action_findUndirectedEdge : LGSPAction
    {
        public Action_findUndirectedEdge() {
            rulePattern = Rule_findUndirectedEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0+0);
        }

        public override string Name { get { return "findUndirectedEdge"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findUndirectedEdge instance = new Action_findUndirectedEdge();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findUndirectedEdge_edge__edge0 
            int type_id_candidate_findUndirectedEdge_edge__edge0 = 2;
            for(LGSPEdge head_candidate_findUndirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findUndirectedEdge_edge__edge0], candidate_findUndirectedEdge_edge__edge0 = head_candidate_findUndirectedEdge_edge__edge0.typeNext; candidate_findUndirectedEdge_edge__edge0 != head_candidate_findUndirectedEdge_edge__edge0; candidate_findUndirectedEdge_edge__edge0 = candidate_findUndirectedEdge_edge__edge0.typeNext)
            {
                // Implicit SourceOrTarget findUndirectedEdge_node_y from findUndirectedEdge_edge__edge0 
                bool runOfOtherDirectionOf_findUndirectedEdge_edge__edge0_Underway = true;
runOfOtherDirectionOf_findUndirectedEdge_edge__edge0_label:
                LGSPNode candidate_findUndirectedEdge_node_y = runOfOtherDirectionOf_findUndirectedEdge_edge__edge0_Underway ? candidate_findUndirectedEdge_edge__edge0.source : candidate_findUndirectedEdge_edge__edge0.target;
                uint prev__candidate_findUndirectedEdge_node_y;
                prev__candidate_findUndirectedEdge_node_y = candidate_findUndirectedEdge_node_y.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findUndirectedEdge_node_y.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Implicit TheOther findUndirectedEdge_node_x from findUndirectedEdge_edge__edge0 
                LGSPNode candidate_findUndirectedEdge_node_x = candidate_findUndirectedEdge_node_y==candidate_findUndirectedEdge_edge__edge0.source ? candidate_findUndirectedEdge_edge__edge0.target : candidate_findUndirectedEdge_edge__edge0.source;
                if((candidate_findUndirectedEdge_node_x.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                    && candidate_findUndirectedEdge_node_x==candidate_findUndirectedEdge_node_y
                    )
                {
                    candidate_findUndirectedEdge_node_y.flags = candidate_findUndirectedEdge_node_y.flags & ~prev__candidate_findUndirectedEdge_node_y | prev__candidate_findUndirectedEdge_node_y;
                    continue;
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_findUndirectedEdge.findUndirectedEdge_NodeNums.@x] = candidate_findUndirectedEdge_node_x;
                match.Nodes[(int)Rule_findUndirectedEdge.findUndirectedEdge_NodeNums.@y] = candidate_findUndirectedEdge_node_y;
                match.Edges[(int)Rule_findUndirectedEdge.findUndirectedEdge_EdgeNums.@_edge0] = candidate_findUndirectedEdge_edge__edge0;
                matches.matchesList.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_findUndirectedEdge_edge__edge0);
                    candidate_findUndirectedEdge_node_y.flags = candidate_findUndirectedEdge_node_y.flags & ~prev__candidate_findUndirectedEdge_node_y | prev__candidate_findUndirectedEdge_node_y;
                    return matches;
                }
                candidate_findUndirectedEdge_node_y.flags = candidate_findUndirectedEdge_node_y.flags & ~prev__candidate_findUndirectedEdge_node_y | prev__candidate_findUndirectedEdge_node_y;
                // other direction of findUndirectedEdge_edge__edge0
                if(runOfOtherDirectionOf_findUndirectedEdge_edge__edge0_Underway) {
                    runOfOtherDirectionOf_findUndirectedEdge_edge__edge0_Underway = false;
                    goto runOfOtherDirectionOf_findUndirectedEdge_edge__edge0_label;
                }
            }
            return matches;
        }
    }

    public class Action_findArbitraryEdge : LGSPAction
    {
        public Action_findArbitraryEdge() {
            rulePattern = Rule_findArbitraryEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0+0);
        }

        public override string Name { get { return "findArbitraryEdge"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryEdge instance = new Action_findArbitraryEdge();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findArbitraryEdge_edge__edge0 
            foreach(EdgeType type_candidate_findArbitraryEdge_edge__edge0 in EdgeType_AEdge.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_findArbitraryEdge_edge__edge0 = type_candidate_findArbitraryEdge_edge__edge0.TypeID;
                for(LGSPEdge head_candidate_findArbitraryEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryEdge_edge__edge0], candidate_findArbitraryEdge_edge__edge0 = head_candidate_findArbitraryEdge_edge__edge0.typeNext; candidate_findArbitraryEdge_edge__edge0 != head_candidate_findArbitraryEdge_edge__edge0; candidate_findArbitraryEdge_edge__edge0 = candidate_findArbitraryEdge_edge__edge0.typeNext)
                {
                    // Implicit SourceOrTarget findArbitraryEdge_node_y from findArbitraryEdge_edge__edge0 
                    bool runOfOtherDirectionOf_findArbitraryEdge_edge__edge0_Underway = true;
runOfOtherDirectionOf_findArbitraryEdge_edge__edge0_label:
                    LGSPNode candidate_findArbitraryEdge_node_y = runOfOtherDirectionOf_findArbitraryEdge_edge__edge0_Underway ? candidate_findArbitraryEdge_edge__edge0.source : candidate_findArbitraryEdge_edge__edge0.target;
                    uint prev__candidate_findArbitraryEdge_node_y;
                    prev__candidate_findArbitraryEdge_node_y = candidate_findArbitraryEdge_node_y.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_findArbitraryEdge_node_y.flags |= LGSPNode.IS_MATCHED<<negLevel;
                    // Implicit TheOther findArbitraryEdge_node_x from findArbitraryEdge_edge__edge0 
                    LGSPNode candidate_findArbitraryEdge_node_x = candidate_findArbitraryEdge_node_y==candidate_findArbitraryEdge_edge__edge0.source ? candidate_findArbitraryEdge_edge__edge0.target : candidate_findArbitraryEdge_edge__edge0.source;
                    if((candidate_findArbitraryEdge_node_x.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                        && candidate_findArbitraryEdge_node_x==candidate_findArbitraryEdge_node_y
                        )
                    {
                        candidate_findArbitraryEdge_node_y.flags = candidate_findArbitraryEdge_node_y.flags & ~prev__candidate_findArbitraryEdge_node_y | prev__candidate_findArbitraryEdge_node_y;
                        continue;
                    }
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_findArbitraryEdge.findArbitraryEdge_NodeNums.@x] = candidate_findArbitraryEdge_node_x;
                    match.Nodes[(int)Rule_findArbitraryEdge.findArbitraryEdge_NodeNums.@y] = candidate_findArbitraryEdge_node_y;
                    match.Edges[(int)Rule_findArbitraryEdge.findArbitraryEdge_EdgeNums.@_edge0] = candidate_findArbitraryEdge_edge__edge0;
                    matches.matchesList.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(candidate_findArbitraryEdge_edge__edge0);
                        candidate_findArbitraryEdge_node_y.flags = candidate_findArbitraryEdge_node_y.flags & ~prev__candidate_findArbitraryEdge_node_y | prev__candidate_findArbitraryEdge_node_y;
                        return matches;
                    }
                    candidate_findArbitraryEdge_node_y.flags = candidate_findArbitraryEdge_node_y.flags & ~prev__candidate_findArbitraryEdge_node_y | prev__candidate_findArbitraryEdge_node_y;
                    // other direction of findArbitraryEdge_edge__edge0
                    if(runOfOtherDirectionOf_findArbitraryEdge_edge__edge0_Underway) {
                        runOfOtherDirectionOf_findArbitraryEdge_edge__edge0_Underway = false;
                        goto runOfOtherDirectionOf_findArbitraryEdge_edge__edge0_label;
                    }
                }
            }
            return matches;
        }
    }

    public class Action_findArbitraryDirectedEdge : LGSPAction
    {
        public Action_findArbitraryDirectedEdge() {
            rulePattern = Rule_findArbitraryDirectedEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0+0);
        }

        public override string Name { get { return "findArbitraryDirectedEdge"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedEdge instance = new Action_findArbitraryDirectedEdge();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findArbitraryDirectedEdge_edge__edge0 
            int type_id_candidate_findArbitraryDirectedEdge_edge__edge0 = 1;
            for(LGSPEdge head_candidate_findArbitraryDirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedEdge_edge__edge0], candidate_findArbitraryDirectedEdge_edge__edge0 = head_candidate_findArbitraryDirectedEdge_edge__edge0.typeNext; candidate_findArbitraryDirectedEdge_edge__edge0 != head_candidate_findArbitraryDirectedEdge_edge__edge0; candidate_findArbitraryDirectedEdge_edge__edge0 = candidate_findArbitraryDirectedEdge_edge__edge0.typeNext)
            {
                // Implicit SourceOrTarget findArbitraryDirectedEdge_node_y from findArbitraryDirectedEdge_edge__edge0 
                bool runOfOtherDirectionOf_findArbitraryDirectedEdge_edge__edge0_Underway = true;
runOfOtherDirectionOf_findArbitraryDirectedEdge_edge__edge0_label:
                LGSPNode candidate_findArbitraryDirectedEdge_node_y = runOfOtherDirectionOf_findArbitraryDirectedEdge_edge__edge0_Underway ? candidate_findArbitraryDirectedEdge_edge__edge0.source : candidate_findArbitraryDirectedEdge_edge__edge0.target;
                uint prev__candidate_findArbitraryDirectedEdge_node_y;
                prev__candidate_findArbitraryDirectedEdge_node_y = candidate_findArbitraryDirectedEdge_node_y.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findArbitraryDirectedEdge_node_y.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Implicit TheOther findArbitraryDirectedEdge_node_x from findArbitraryDirectedEdge_edge__edge0 
                LGSPNode candidate_findArbitraryDirectedEdge_node_x = candidate_findArbitraryDirectedEdge_node_y==candidate_findArbitraryDirectedEdge_edge__edge0.source ? candidate_findArbitraryDirectedEdge_edge__edge0.target : candidate_findArbitraryDirectedEdge_edge__edge0.source;
                if((candidate_findArbitraryDirectedEdge_node_x.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                    && candidate_findArbitraryDirectedEdge_node_x==candidate_findArbitraryDirectedEdge_node_y
                    )
                {
                    candidate_findArbitraryDirectedEdge_node_y.flags = candidate_findArbitraryDirectedEdge_node_y.flags & ~prev__candidate_findArbitraryDirectedEdge_node_y | prev__candidate_findArbitraryDirectedEdge_node_y;
                    continue;
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_findArbitraryDirectedEdge.findArbitraryDirectedEdge_NodeNums.@x] = candidate_findArbitraryDirectedEdge_node_x;
                match.Nodes[(int)Rule_findArbitraryDirectedEdge.findArbitraryDirectedEdge_NodeNums.@y] = candidate_findArbitraryDirectedEdge_node_y;
                match.Edges[(int)Rule_findArbitraryDirectedEdge.findArbitraryDirectedEdge_EdgeNums.@_edge0] = candidate_findArbitraryDirectedEdge_edge__edge0;
                matches.matchesList.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_findArbitraryDirectedEdge_edge__edge0);
                    candidate_findArbitraryDirectedEdge_node_y.flags = candidate_findArbitraryDirectedEdge_node_y.flags & ~prev__candidate_findArbitraryDirectedEdge_node_y | prev__candidate_findArbitraryDirectedEdge_node_y;
                    return matches;
                }
                candidate_findArbitraryDirectedEdge_node_y.flags = candidate_findArbitraryDirectedEdge_node_y.flags & ~prev__candidate_findArbitraryDirectedEdge_node_y | prev__candidate_findArbitraryDirectedEdge_node_y;
                // other direction of findArbitraryDirectedEdge_edge__edge0
                if(runOfOtherDirectionOf_findArbitraryDirectedEdge_edge__edge0_Underway) {
                    runOfOtherDirectionOf_findArbitraryDirectedEdge_edge__edge0_Underway = false;
                    goto runOfOtherDirectionOf_findArbitraryDirectedEdge_edge__edge0_label;
                }
            }
            return matches;
        }
    }

    public class Action_findArbitraryDirectedReflexiveEdge : LGSPAction
    {
        public Action_findArbitraryDirectedReflexiveEdge() {
            rulePattern = Rule_findArbitraryDirectedReflexiveEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 1, 0+0);
        }

        public override string Name { get { return "findArbitraryDirectedReflexiveEdge"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedReflexiveEdge instance = new Action_findArbitraryDirectedReflexiveEdge();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findArbitraryDirectedReflexiveEdge_edge__edge0 
            int type_id_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = 1;
            for(LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0], candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.typeNext; candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 != head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0; candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.typeNext)
            {
                // Implicit Source findArbitraryDirectedReflexiveEdge_node_x from findArbitraryDirectedReflexiveEdge_edge__edge0 
                LGSPNode candidate_findArbitraryDirectedReflexiveEdge_node_x = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.source;
                if( (candidate_findArbitraryDirectedReflexiveEdge_node_x==candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.source ? candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.target : candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.source) != candidate_findArbitraryDirectedReflexiveEdge_node_x) {
                    continue;
                }
                if( (candidate_findArbitraryDirectedReflexiveEdge_node_x==candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.source ? candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.target : candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.source) != candidate_findArbitraryDirectedReflexiveEdge_node_x) {
                    continue;
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_findArbitraryDirectedReflexiveEdge.findArbitraryDirectedReflexiveEdge_NodeNums.@x] = candidate_findArbitraryDirectedReflexiveEdge_node_x;
                match.Edges[(int)Rule_findArbitraryDirectedReflexiveEdge.findArbitraryDirectedReflexiveEdge_EdgeNums.@_edge0] = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0;
                matches.matchesList.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_findArbitraryDirectedReflexiveEdge_edge__edge0);
                    return matches;
                }
            }
            return matches;
        }
    }

    public class Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : LGSPAction
    {
        public Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne() {
            rulePattern = Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0+0);
        }

        public override string Name { get { return "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne instance = new Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
            int type_id_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = 2;
            for(LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0], candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.typeNext; candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 != head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0; candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.typeNext)
            {
                // Implicit SourceOrTarget findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
                bool runOfOtherDirectionOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_Underway = true;
runOfOtherDirectionOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_label:
                LGSPNode candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = runOfOtherDirectionOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_Underway ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.source : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.target;
                uint prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Implicit TheOther findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
                LGSPNode candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y==candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.source ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.target : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.source;
                if((candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                    && candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x==candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y
                    )
                {
                    candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & ~prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                    continue;
                }
                // Extend Incoming findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y 
                LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.inhead;
                if(head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 != null)
                {
                    LGSPEdge candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.type.TypeID]) {
                            continue;
                        }
                        if( (candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y==candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.source ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.target : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.source) != candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y) {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_NodeNums.@x] = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x;
                        match.Nodes[(int)Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_NodeNums.@y] = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                        match.Edges[(int)Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_EdgeNums.@_edge0] = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0;
                        match.Edges[(int)Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_EdgeNums.@_edge1] = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1;
                        matches.matchesList.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.MoveInHeadAfter(candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1);
                            graph.MoveHeadAfter(candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0);
                            candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & ~prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                            return matches;
                        }
                    }
                    while( (candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.inNext) != head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 );
                }
                candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & ~prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                // other direction of findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0
                if(runOfOtherDirectionOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_Underway) {
                    runOfOtherDirectionOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_Underway = false;
                    goto runOfOtherDirectionOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_label;
                }
            }
            return matches;
        }
    }

    public class Action_findArbitraryDirectedTriple : LGSPAction
    {
        public Action_findArbitraryDirectedTriple() {
            rulePattern = Rule_findArbitraryDirectedTriple.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2, 0+0);
        }

        public override string Name { get { return "findArbitraryDirectedTriple"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedTriple instance = new Action_findArbitraryDirectedTriple();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findArbitraryDirectedTriple_edge__edge1 
            int type_id_candidate_findArbitraryDirectedTriple_edge__edge1 = 1;
            for(LGSPEdge head_candidate_findArbitraryDirectedTriple_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedTriple_edge__edge1], candidate_findArbitraryDirectedTriple_edge__edge1 = head_candidate_findArbitraryDirectedTriple_edge__edge1.typeNext; candidate_findArbitraryDirectedTriple_edge__edge1 != head_candidate_findArbitraryDirectedTriple_edge__edge1; candidate_findArbitraryDirectedTriple_edge__edge1 = candidate_findArbitraryDirectedTriple_edge__edge1.typeNext)
            {
                uint prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                prev__candidate_findArbitraryDirectedTriple_edge__edge1 = candidate_findArbitraryDirectedTriple_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                candidate_findArbitraryDirectedTriple_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                // Implicit SourceOrTarget findArbitraryDirectedTriple_node__node2 from findArbitraryDirectedTriple_edge__edge1 
                bool runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge1_Underway = true;
runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge1_label:
                LGSPNode candidate_findArbitraryDirectedTriple_node__node2 = runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge1_Underway ? candidate_findArbitraryDirectedTriple_edge__edge1.source : candidate_findArbitraryDirectedTriple_edge__edge1.target;
                uint prev__candidate_findArbitraryDirectedTriple_node__node2;
                prev__candidate_findArbitraryDirectedTriple_node__node2 = candidate_findArbitraryDirectedTriple_node__node2.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findArbitraryDirectedTriple_node__node2.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Implicit TheOther findArbitraryDirectedTriple_node__node1 from findArbitraryDirectedTriple_edge__edge1 
                LGSPNode candidate_findArbitraryDirectedTriple_node__node1 = candidate_findArbitraryDirectedTriple_node__node2==candidate_findArbitraryDirectedTriple_edge__edge1.source ? candidate_findArbitraryDirectedTriple_edge__edge1.target : candidate_findArbitraryDirectedTriple_edge__edge1.source;
                if((candidate_findArbitraryDirectedTriple_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                    && candidate_findArbitraryDirectedTriple_node__node1==candidate_findArbitraryDirectedTriple_node__node2
                    )
                {
                    candidate_findArbitraryDirectedTriple_node__node2.flags = candidate_findArbitraryDirectedTriple_node__node2.flags & ~prev__candidate_findArbitraryDirectedTriple_node__node2 | prev__candidate_findArbitraryDirectedTriple_node__node2;
                    candidate_findArbitraryDirectedTriple_edge__edge1.flags = candidate_findArbitraryDirectedTriple_edge__edge1.flags & ~prev__candidate_findArbitraryDirectedTriple_edge__edge1 | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                    continue;
                }
                uint prev__candidate_findArbitraryDirectedTriple_node__node1;
                prev__candidate_findArbitraryDirectedTriple_node__node1 = candidate_findArbitraryDirectedTriple_node__node1.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findArbitraryDirectedTriple_node__node1.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Extend IncomingOrOutgoing findArbitraryDirectedTriple_edge__edge0 from findArbitraryDirectedTriple_node__node1 
                bool runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge0_Underway = true;
runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge0_label:
                LGSPEdge head_candidate_findArbitraryDirectedTriple_edge__edge0 = runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge0_Underway ? candidate_findArbitraryDirectedTriple_node__node1.inhead : candidate_findArbitraryDirectedTriple_node__node1.outhead;
                if(head_candidate_findArbitraryDirectedTriple_edge__edge0 != null)
                {
                    LGSPEdge candidate_findArbitraryDirectedTriple_edge__edge0 = head_candidate_findArbitraryDirectedTriple_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_findArbitraryDirectedTriple_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_findArbitraryDirectedTriple_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel
                            && candidate_findArbitraryDirectedTriple_edge__edge0==candidate_findArbitraryDirectedTriple_edge__edge1
                            )
                        {
                            continue;
                        }
                        // Implicit TheOther findArbitraryDirectedTriple_node__node0 from findArbitraryDirectedTriple_edge__edge0 
                        LGSPNode candidate_findArbitraryDirectedTriple_node__node0 = candidate_findArbitraryDirectedTriple_node__node1==candidate_findArbitraryDirectedTriple_edge__edge0.source ? candidate_findArbitraryDirectedTriple_edge__edge0.target : candidate_findArbitraryDirectedTriple_edge__edge0.source;
                        if((candidate_findArbitraryDirectedTriple_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                            && (candidate_findArbitraryDirectedTriple_node__node0==candidate_findArbitraryDirectedTriple_node__node2
                                || candidate_findArbitraryDirectedTriple_node__node0==candidate_findArbitraryDirectedTriple_node__node1
                                )
                            )
                        {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_findArbitraryDirectedTriple.findArbitraryDirectedTriple_NodeNums.@_node0] = candidate_findArbitraryDirectedTriple_node__node0;
                        match.Nodes[(int)Rule_findArbitraryDirectedTriple.findArbitraryDirectedTriple_NodeNums.@_node1] = candidate_findArbitraryDirectedTriple_node__node1;
                        match.Nodes[(int)Rule_findArbitraryDirectedTriple.findArbitraryDirectedTriple_NodeNums.@_node2] = candidate_findArbitraryDirectedTriple_node__node2;
                        match.Edges[(int)Rule_findArbitraryDirectedTriple.findArbitraryDirectedTriple_EdgeNums.@_edge0] = candidate_findArbitraryDirectedTriple_edge__edge0;
                        match.Edges[(int)Rule_findArbitraryDirectedTriple.findArbitraryDirectedTriple_EdgeNums.@_edge1] = candidate_findArbitraryDirectedTriple_edge__edge1;
                        matches.matchesList.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            candidate_findArbitraryDirectedTriple_node__node1.MoveOutHeadAfter(candidate_findArbitraryDirectedTriple_edge__edge0);
                            graph.MoveHeadAfter(candidate_findArbitraryDirectedTriple_edge__edge1);
                            candidate_findArbitraryDirectedTriple_node__node1.flags = candidate_findArbitraryDirectedTriple_node__node1.flags & ~prev__candidate_findArbitraryDirectedTriple_node__node1 | prev__candidate_findArbitraryDirectedTriple_node__node1;
                            candidate_findArbitraryDirectedTriple_node__node2.flags = candidate_findArbitraryDirectedTriple_node__node2.flags & ~prev__candidate_findArbitraryDirectedTriple_node__node2 | prev__candidate_findArbitraryDirectedTriple_node__node2;
                            candidate_findArbitraryDirectedTriple_edge__edge1.flags = candidate_findArbitraryDirectedTriple_edge__edge1.flags & ~prev__candidate_findArbitraryDirectedTriple_edge__edge1 | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                            return matches;
                        }
                        // other direction of findArbitraryDirectedTriple_edge__edge0
                        if(runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge0_Underway) {
                            runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge0_Underway = false;
                            goto runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge0_label;
                        }
                    }
                    while( (runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge0_Underway ? candidate_findArbitraryDirectedTriple_edge__edge0 = candidate_findArbitraryDirectedTriple_edge__edge0.inNext : candidate_findArbitraryDirectedTriple_edge__edge0 = candidate_findArbitraryDirectedTriple_edge__edge0.outNext) != head_candidate_findArbitraryDirectedTriple_edge__edge0 );
                }
                candidate_findArbitraryDirectedTriple_node__node1.flags = candidate_findArbitraryDirectedTriple_node__node1.flags & ~prev__candidate_findArbitraryDirectedTriple_node__node1 | prev__candidate_findArbitraryDirectedTriple_node__node1;
                candidate_findArbitraryDirectedTriple_node__node2.flags = candidate_findArbitraryDirectedTriple_node__node2.flags & ~prev__candidate_findArbitraryDirectedTriple_node__node2 | prev__candidate_findArbitraryDirectedTriple_node__node2;
                // other direction of findArbitraryDirectedTriple_edge__edge1
                if(runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge1_Underway) {
                    runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge1_Underway = false;
                    goto runOfOtherDirectionOf_findArbitraryDirectedTriple_edge__edge1_label;
                }
                candidate_findArbitraryDirectedTriple_edge__edge1.flags = candidate_findArbitraryDirectedTriple_edge__edge1.flags & ~prev__candidate_findArbitraryDirectedTriple_edge__edge1 | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
            }
            return matches;
        }
    }

    public class Action_findDirectedEdge : LGSPAction
    {
        public Action_findDirectedEdge() {
            rulePattern = Rule_findDirectedEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0+0);
        }

        public override string Name { get { return "findDirectedEdge"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findDirectedEdge instance = new Action_findDirectedEdge();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findDirectedEdge_edge__edge0 
            int type_id_candidate_findDirectedEdge_edge__edge0 = 1;
            for(LGSPEdge head_candidate_findDirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findDirectedEdge_edge__edge0], candidate_findDirectedEdge_edge__edge0 = head_candidate_findDirectedEdge_edge__edge0.typeNext; candidate_findDirectedEdge_edge__edge0 != head_candidate_findDirectedEdge_edge__edge0; candidate_findDirectedEdge_edge__edge0 = candidate_findDirectedEdge_edge__edge0.typeNext)
            {
                // Implicit Source findDirectedEdge_node_x from findDirectedEdge_edge__edge0 
                LGSPNode candidate_findDirectedEdge_node_x = candidate_findDirectedEdge_edge__edge0.source;
                uint prev__candidate_findDirectedEdge_node_x;
                prev__candidate_findDirectedEdge_node_x = candidate_findDirectedEdge_node_x.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findDirectedEdge_node_x.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Implicit Target findDirectedEdge_node_y from findDirectedEdge_edge__edge0 
                LGSPNode candidate_findDirectedEdge_node_y = candidate_findDirectedEdge_edge__edge0.target;
                if((candidate_findDirectedEdge_node_y.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                    && candidate_findDirectedEdge_node_y==candidate_findDirectedEdge_node_x
                    )
                {
                    candidate_findDirectedEdge_node_x.flags = candidate_findDirectedEdge_node_x.flags & ~prev__candidate_findDirectedEdge_node_x | prev__candidate_findDirectedEdge_node_x;
                    continue;
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_findDirectedEdge.findDirectedEdge_NodeNums.@x] = candidate_findDirectedEdge_node_x;
                match.Nodes[(int)Rule_findDirectedEdge.findDirectedEdge_NodeNums.@y] = candidate_findDirectedEdge_node_y;
                match.Edges[(int)Rule_findDirectedEdge.findDirectedEdge_EdgeNums.@_edge0] = candidate_findDirectedEdge_edge__edge0;
                matches.matchesList.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_findDirectedEdge_edge__edge0);
                    candidate_findDirectedEdge_node_x.flags = candidate_findDirectedEdge_node_x.flags & ~prev__candidate_findDirectedEdge_node_x | prev__candidate_findDirectedEdge_node_x;
                    return matches;
                }
                candidate_findDirectedEdge_node_x.flags = candidate_findDirectedEdge_node_x.flags & ~prev__candidate_findDirectedEdge_node_x | prev__candidate_findDirectedEdge_node_x;
            }
            return matches;
        }
    }

    public class Action_findTripleCircle : LGSPAction
    {
        public Action_findTripleCircle() {
            rulePattern = Rule_findTripleCircle.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 3, 0+0);
        }

        public override string Name { get { return "findTripleCircle"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_findTripleCircle instance = new Action_findTripleCircle();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup findTripleCircle_edge__edge0 
            int type_id_candidate_findTripleCircle_edge__edge0 = 2;
            for(LGSPEdge head_candidate_findTripleCircle_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findTripleCircle_edge__edge0], candidate_findTripleCircle_edge__edge0 = head_candidate_findTripleCircle_edge__edge0.typeNext; candidate_findTripleCircle_edge__edge0 != head_candidate_findTripleCircle_edge__edge0; candidate_findTripleCircle_edge__edge0 = candidate_findTripleCircle_edge__edge0.typeNext)
            {
                uint prev__candidate_findTripleCircle_edge__edge0;
                prev__candidate_findTripleCircle_edge__edge0 = candidate_findTripleCircle_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                candidate_findTripleCircle_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                // Implicit SourceOrTarget findTripleCircle_node_y from findTripleCircle_edge__edge0 
                bool runOfOtherDirectionOf_findTripleCircle_edge__edge0_Underway = true;
runOfOtherDirectionOf_findTripleCircle_edge__edge0_label:
                LGSPNode candidate_findTripleCircle_node_y = runOfOtherDirectionOf_findTripleCircle_edge__edge0_Underway ? candidate_findTripleCircle_edge__edge0.source : candidate_findTripleCircle_edge__edge0.target;
                uint prev__candidate_findTripleCircle_node_y;
                prev__candidate_findTripleCircle_node_y = candidate_findTripleCircle_node_y.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findTripleCircle_node_y.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Implicit TheOther findTripleCircle_node_x from findTripleCircle_edge__edge0 
                LGSPNode candidate_findTripleCircle_node_x = candidate_findTripleCircle_node_y==candidate_findTripleCircle_edge__edge0.source ? candidate_findTripleCircle_edge__edge0.target : candidate_findTripleCircle_edge__edge0.source;
                if((candidate_findTripleCircle_node_x.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                    && candidate_findTripleCircle_node_x==candidate_findTripleCircle_node_y
                    )
                {
                    candidate_findTripleCircle_node_y.flags = candidate_findTripleCircle_node_y.flags & ~prev__candidate_findTripleCircle_node_y | prev__candidate_findTripleCircle_node_y;
                    candidate_findTripleCircle_edge__edge0.flags = candidate_findTripleCircle_edge__edge0.flags & ~prev__candidate_findTripleCircle_edge__edge0 | prev__candidate_findTripleCircle_edge__edge0;
                    continue;
                }
                uint prev__candidate_findTripleCircle_node_x;
                prev__candidate_findTripleCircle_node_x = candidate_findTripleCircle_node_x.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_findTripleCircle_node_x.flags |= LGSPNode.IS_MATCHED<<negLevel;
                // Extend IncomingOrOutgoing findTripleCircle_edge__edge1 from findTripleCircle_node_y 
                bool runOfOtherDirectionOf_findTripleCircle_edge__edge1_Underway = true;
runOfOtherDirectionOf_findTripleCircle_edge__edge1_label:
                LGSPEdge head_candidate_findTripleCircle_edge__edge1 = runOfOtherDirectionOf_findTripleCircle_edge__edge1_Underway ? candidate_findTripleCircle_node_y.inhead : candidate_findTripleCircle_node_y.outhead;
                if(head_candidate_findTripleCircle_edge__edge1 != null)
                {
                    LGSPEdge candidate_findTripleCircle_edge__edge1 = head_candidate_findTripleCircle_edge__edge1;
                    do
                    {
                        if(!EdgeType_UEdge.isMyType[candidate_findTripleCircle_edge__edge1.type.TypeID]) {
                            continue;
                        }
                        if((candidate_findTripleCircle_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel
                            && candidate_findTripleCircle_edge__edge1==candidate_findTripleCircle_edge__edge0
                            )
                        {
                            continue;
                        }
                        uint prev__candidate_findTripleCircle_edge__edge1;
                        prev__candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel;
                        candidate_findTripleCircle_edge__edge1.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        // Implicit TheOther findTripleCircle_node_z from findTripleCircle_edge__edge1 
                        LGSPNode candidate_findTripleCircle_node_z = candidate_findTripleCircle_node_y==candidate_findTripleCircle_edge__edge1.source ? candidate_findTripleCircle_edge__edge1.target : candidate_findTripleCircle_edge__edge1.source;
                        if((candidate_findTripleCircle_node_z.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel
                            && (candidate_findTripleCircle_node_z==candidate_findTripleCircle_node_y
                                || candidate_findTripleCircle_node_z==candidate_findTripleCircle_node_x
                                )
                            )
                        {
                            candidate_findTripleCircle_edge__edge1.flags = candidate_findTripleCircle_edge__edge1.flags & ~prev__candidate_findTripleCircle_edge__edge1 | prev__candidate_findTripleCircle_edge__edge1;
                            continue;
                        }
                        // Extend IncomingOrOutgoing findTripleCircle_edge__edge2 from findTripleCircle_node_z 
                        bool runOfOtherDirectionOf_findTripleCircle_edge__edge2_Underway = true;
runOfOtherDirectionOf_findTripleCircle_edge__edge2_label:
                        LGSPEdge head_candidate_findTripleCircle_edge__edge2 = runOfOtherDirectionOf_findTripleCircle_edge__edge2_Underway ? candidate_findTripleCircle_node_z.inhead : candidate_findTripleCircle_node_z.outhead;
                        if(head_candidate_findTripleCircle_edge__edge2 != null)
                        {
                            LGSPEdge candidate_findTripleCircle_edge__edge2 = head_candidate_findTripleCircle_edge__edge2;
                            do
                            {
                                if(!EdgeType_UEdge.isMyType[candidate_findTripleCircle_edge__edge2.type.TypeID]) {
                                    continue;
                                }
                                if( (candidate_findTripleCircle_node_z==candidate_findTripleCircle_edge__edge2.source ? candidate_findTripleCircle_edge__edge2.target : candidate_findTripleCircle_edge__edge2.source) != candidate_findTripleCircle_node_x) {
                                    continue;
                                }
                                if((candidate_findTripleCircle_edge__edge2.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel
                                    && (candidate_findTripleCircle_edge__edge2==candidate_findTripleCircle_edge__edge0
                                        || candidate_findTripleCircle_edge__edge2==candidate_findTripleCircle_edge__edge1
                                        )
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_findTripleCircle.findTripleCircle_NodeNums.@x] = candidate_findTripleCircle_node_x;
                                match.Nodes[(int)Rule_findTripleCircle.findTripleCircle_NodeNums.@y] = candidate_findTripleCircle_node_y;
                                match.Nodes[(int)Rule_findTripleCircle.findTripleCircle_NodeNums.@z] = candidate_findTripleCircle_node_z;
                                match.Edges[(int)Rule_findTripleCircle.findTripleCircle_EdgeNums.@_edge0] = candidate_findTripleCircle_edge__edge0;
                                match.Edges[(int)Rule_findTripleCircle.findTripleCircle_EdgeNums.@_edge1] = candidate_findTripleCircle_edge__edge1;
                                match.Edges[(int)Rule_findTripleCircle.findTripleCircle_EdgeNums.@_edge2] = candidate_findTripleCircle_edge__edge2;
                                matches.matchesList.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                                {
                                    candidate_findTripleCircle_node_z.MoveOutHeadAfter(candidate_findTripleCircle_edge__edge2);
                                    candidate_findTripleCircle_node_y.MoveOutHeadAfter(candidate_findTripleCircle_edge__edge1);
                                    graph.MoveHeadAfter(candidate_findTripleCircle_edge__edge0);
                                    candidate_findTripleCircle_edge__edge1.flags = candidate_findTripleCircle_edge__edge1.flags & ~prev__candidate_findTripleCircle_edge__edge1 | prev__candidate_findTripleCircle_edge__edge1;
                                    candidate_findTripleCircle_node_x.flags = candidate_findTripleCircle_node_x.flags & ~prev__candidate_findTripleCircle_node_x | prev__candidate_findTripleCircle_node_x;
                                    candidate_findTripleCircle_node_y.flags = candidate_findTripleCircle_node_y.flags & ~prev__candidate_findTripleCircle_node_y | prev__candidate_findTripleCircle_node_y;
                                    candidate_findTripleCircle_edge__edge0.flags = candidate_findTripleCircle_edge__edge0.flags & ~prev__candidate_findTripleCircle_edge__edge0 | prev__candidate_findTripleCircle_edge__edge0;
                                    return matches;
                                }
                                // other direction of findTripleCircle_edge__edge2
                                if(runOfOtherDirectionOf_findTripleCircle_edge__edge2_Underway) {
                                    runOfOtherDirectionOf_findTripleCircle_edge__edge2_Underway = false;
                                    goto runOfOtherDirectionOf_findTripleCircle_edge__edge2_label;
                                }
                            }
                            while( (runOfOtherDirectionOf_findTripleCircle_edge__edge2_Underway ? candidate_findTripleCircle_edge__edge2 = candidate_findTripleCircle_edge__edge2.inNext : candidate_findTripleCircle_edge__edge2 = candidate_findTripleCircle_edge__edge2.outNext) != head_candidate_findTripleCircle_edge__edge2 );
                        }
                        candidate_findTripleCircle_edge__edge1.flags = candidate_findTripleCircle_edge__edge1.flags & ~prev__candidate_findTripleCircle_edge__edge1 | prev__candidate_findTripleCircle_edge__edge1;
                        // other direction of findTripleCircle_edge__edge1
                        if(runOfOtherDirectionOf_findTripleCircle_edge__edge1_Underway) {
                            runOfOtherDirectionOf_findTripleCircle_edge__edge1_Underway = false;
                            goto runOfOtherDirectionOf_findTripleCircle_edge__edge1_label;
                        }
                    }
                    while( (runOfOtherDirectionOf_findTripleCircle_edge__edge1_Underway ? candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.inNext : candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.outNext) != head_candidate_findTripleCircle_edge__edge1 );
                }
                candidate_findTripleCircle_node_x.flags = candidate_findTripleCircle_node_x.flags & ~prev__candidate_findTripleCircle_node_x | prev__candidate_findTripleCircle_node_x;
                candidate_findTripleCircle_node_y.flags = candidate_findTripleCircle_node_y.flags & ~prev__candidate_findTripleCircle_node_y | prev__candidate_findTripleCircle_node_y;
                // other direction of findTripleCircle_edge__edge0
                if(runOfOtherDirectionOf_findTripleCircle_edge__edge0_Underway) {
                    runOfOtherDirectionOf_findTripleCircle_edge__edge0_Underway = false;
                    goto runOfOtherDirectionOf_findTripleCircle_edge__edge0_label;
                }
                candidate_findTripleCircle_edge__edge0.flags = candidate_findTripleCircle_edge__edge0.flags & ~prev__candidate_findTripleCircle_edge__edge0 | prev__candidate_findTripleCircle_edge__edge0;
            }
            return matches;
        }
    }


    public class edge1Actions : LGSPActions
    {
        public edge1Actions(LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public edge1Actions(LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("init", (LGSPAction) Action_init.Instance);
            actions.Add("init2", (LGSPAction) Action_init2.Instance);
            actions.Add("init3", (LGSPAction) Action_init3.Instance);
            actions.Add("findUndirectedEdge", (LGSPAction) Action_findUndirectedEdge.Instance);
            actions.Add("findArbitraryEdge", (LGSPAction) Action_findArbitraryEdge.Instance);
            actions.Add("findArbitraryDirectedEdge", (LGSPAction) Action_findArbitraryDirectedEdge.Instance);
            actions.Add("findArbitraryDirectedReflexiveEdge", (LGSPAction) Action_findArbitraryDirectedReflexiveEdge.Instance);
            actions.Add("findArbitraryDirectedReflexiveEdgeAfterUndirectedOne", (LGSPAction) Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance);
            actions.Add("findArbitraryDirectedTriple", (LGSPAction) Action_findArbitraryDirectedTriple.Instance);
            actions.Add("findDirectedEdge", (LGSPAction) Action_findDirectedEdge.Instance);
            actions.Add("findTripleCircle", (LGSPAction) Action_findTripleCircle.Instance);
        }

        public override String Name { get { return "edge1Actions"; } }
        public override String ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}