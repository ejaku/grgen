//#define PRODUCE_UNSAFE_MATCHERS // todo: what for ?
#define CSHARPCODE_VERSION2
#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define NO_EDGE_LOOKUP
//#define NO_ADJUST_LIST_HEADS
//#define RANDOM_LOOKUP_LIST_START      // currently broken
//#define USE_INSTANCEOF_FOR_TYPECHECKS  // not implemented in new code - todo: needed ?
//#define DUMP_SEARCHPROGRAMS
#define OLDMAPPEDFIELDS
//#define OPCOST_WITH_GEO_MEAN
//#define VSTRUCT_VAL_FOR_EDGE_LOOKUP

using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.lgsp;
using System.IO;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace de.unika.ipd.grGen.lgsp
{
    public enum PlanNodeType { Root, Node, Edge };

    /// <summary>
    /// Common base class for the PlanNodes and PlanSuperNodes, 
    /// used for uniform access to the derived nodes within the minimum spanning arborescent computation.
    /// </summary>
    public abstract class PlanPseudoNode
    {
        public PlanSuperNode SuperNode;
        public PlanEdge IncomingMSAEdge;

        /// <summary>
        /// Returns all incoming plan edges.
        /// </summary>
        public abstract IEnumerable<PlanEdge> Incoming { get; }

        /// <summary>
        /// Returns the cheapest incoming plan edge and its cost,
        /// excluding nodes contained within the given top super node (if given)
        /// </summary>
        public abstract PlanEdge GetCheapestIncoming(PlanPseudoNode excludeTopNode, out float cost);

        /// <summary>
        /// outermost enclosing supernode, null if not contained within a supernode
        /// </summary>
        public PlanSuperNode TopSuperNode
        {
            get
            {
                if(SuperNode == null) return null;
                PlanSuperNode current = SuperNode;
                while(current.SuperNode != null)
                    current = current.SuperNode;
                return current;
            }
        }

        /// <summary>
        /// outermost enclosing supernode, the node itself if not contained within a supernode
        /// </summary>
        public PlanPseudoNode TopNode
        {
            get
            {
#if MONO_MULTIDIMARRAY_WORKAROUND
                return ((PlanPseudoNode) TopSuperNode) ?? this;
#else
                return TopSuperNode ?? this;
#endif
            }
        }

        /// <summary>
        /// Decides whether a new edge is better than a known best edge up to now.
        /// </summary>
        /// <returns>true if new edge should be chosen</returns>
        public bool PreferNewEdge(PlanEdge minEdge, float minEdgeCost, PlanEdge newEdge, float newEdgeCost)
        {
            float epsilon = 0.001f;
            float costDiff = minEdgeCost - newEdgeCost;
            if(costDiff < -epsilon) return false;
            if(costDiff > epsilon) return true;

            // Choose equally expensive operations in this order: implicit, non-lookup, edge lookups, node lookups

            if(minEdge.Source.NodeType == PlanNodeType.Edge) return false;      // minEdge is implicit op
            if(newEdge.Source.NodeType == PlanNodeType.Edge) return true;       // newEdge is implicit op

            if(minEdge.Source.NodeType != PlanNodeType.Root) return false;      // minEdge is non-lookup op
            if(newEdge.Source.NodeType != PlanNodeType.Root) return true;       // newEdge is non-lookup op

            if(minEdge.Target.NodeType == PlanNodeType.Edge) return false;      // minEdge is edge lookup
            if(newEdge.Target.NodeType == PlanNodeType.Edge) return true;       // newEdge is edge lookup

            return false;                                                       // minEdge and newEdge are node lookups, keep minEdge
        }
    }

    /// <summary>
    /// Element of the plan graph representing a node or edge within the pattern graph or a root node.
    /// </summary>
    [DebuggerDisplay("PlanNode {ToString()}")]
    public class PlanNode : PlanPseudoNode
    {
        public PlanNodeType NodeType;
        // MSA needs incoming edges, scheduling needs outgoing edges
        public List<PlanEdge> IncomingEdges = new List<PlanEdge>();
        public int ElementID;
        public bool IsPreset;

        public PatternElement PatternElement;

        /// <summary>
        /// Only valid if representing pattern edge
        /// </summary>
        public PlanNode PatternEdgeSource, PatternEdgeTarget;

        /// <summary>
        /// Instantiates a root plan node.
        /// </summary>
        /// <param name="rootName">The name for the root plan node.</param>
        public PlanNode(String rootName)
        {
            NodeType = PlanNodeType.Root;
            PatternElement = new PatternNode(-1, rootName, null, null);
        }

        /// <summary>
        /// Instantiates a node plan node.
        /// </summary>
        /// <param name="patNode">The pattern node for this plan node.</param>
        /// <param name="elemID">The element ID for this plan node.</param>
        /// <param name="isPreset">True, if this element is a known element.</param>
        public PlanNode(PatternNode patNode, int elemID, bool isPreset)
        {
            NodeType = PlanNodeType.Node;
            ElementID = elemID;
            IsPreset = isPreset;

            PatternElement = patNode;
        }

        /// <summary>
        /// Instantiates an edge plan node.
        /// </summary>
        /// <param name="patEdge">The pattern edge for this plan node.</param>
        /// <param name="elemID">The element ID for this plan node.</param>
        /// <param name="isPreset">True, if this element is a known element.</param>
        public PlanNode(PatternEdge patEdge, int elemID, bool isPreset)
        {
            NodeType = PlanNodeType.Edge;
            ElementID = elemID;
            IsPreset = isPreset;

            PatternElement = patEdge;

            PatternEdgeSource = patEdge.source != null ? patEdge.source.TempPlanMapping : null;
            PatternEdgeTarget = patEdge.target != null ? patEdge.target.TempPlanMapping : null;
        }

        /// <summary>
        /// Returns all incoming plan edges.
        /// </summary>
        public override IEnumerable<PlanEdge> Incoming
        {
            get { return IncomingEdges; }
        }

        /// <summary>
        /// Returns the cheapest incoming plan edge and its cost,
        /// excluding nodes contained within the given top super node (if given)
        /// </summary>
        public override PlanEdge GetCheapestIncoming(PlanPseudoNode excludeTopNode, out float minCost)
        {
            minCost = float.MaxValue;
            PlanEdge minEdge = null;

            foreach(PlanEdge edge in IncomingEdges)
            {
                if(excludeTopNode != null && edge.Source.TopNode == excludeTopNode) continue;
                if(PreferNewEdge(minEdge, minCost, edge, edge.mstCost))
                {
                    minCost = edge.mstCost;
                    minEdge = edge;
                }
            }
            return minEdge;
        }

        public override string ToString()
        {
            return PatternElement.Name;
        }
    }

    /// <summary>
    /// Element of the plan graph representing a strongly connected component within the pattern graph.
    /// Hierachically nested.
    /// </summary>
    public class PlanSuperNode : PlanPseudoNode
    {
        /// <summary>
        /// Representative element of the cycle as entry point. Target of cheapest incoming edge.
        /// </summary>
        public PlanPseudoNode Child;
        public float minCycleEdgeCost;

        public PlanSuperNode(PlanPseudoNode cycleNode)
        {
            Child = cycleNode;

            // Find cheapest edge within cycle
            PlanPseudoNode current = Child;
            minCycleEdgeCost = float.MaxValue;
            do
            {
                current.SuperNode = this;
                if(current.IncomingMSAEdge.mstCost < minCycleEdgeCost)
                    minCycleEdgeCost = current.IncomingMSAEdge.mstCost;
                current = current.IncomingMSAEdge.Source;
                while(current.SuperNode != null && current.SuperNode != this)
                    current = current.SuperNode;
            }
            while(current != Child);

            // Adjust costs of incoming edges
            foreach(PlanEdge edge in Incoming)
            {
                PlanPseudoNode curTarget = edge.Target;
                while(curTarget.SuperNode != this)
                    curTarget = curTarget.SuperNode;
                edge.mstCost -= curTarget.IncomingMSAEdge.mstCost - minCycleEdgeCost;
            }

            // Find cheapest incoming edge and set Child appropriately
            float cost;
            IncomingMSAEdge = GetCheapestIncoming(this, out cost);
            Child = IncomingMSAEdge.Target;
            while(Child.SuperNode != this)
                Child = Child.SuperNode;
        }

        /// <summary>
        /// Returns all incoming plan edges.
        /// </summary>
        public override IEnumerable<PlanEdge> Incoming
        {
            get
            {
                PlanPseudoNode current = Child;
                do
                {
                    PlanPseudoNode currentTopSuperNode = current.TopSuperNode;
                    foreach(PlanEdge edge in current.Incoming)
                    {
                        if(edge.Source.TopSuperNode != currentTopSuperNode)
                            yield return edge;
                    }
                    current = current.IncomingMSAEdge.Source;
                    while(current.SuperNode != this)
                        current = current.SuperNode;
                }
                while(current != Child);
            }
        }

        /// <summary>
        /// Returns the cheapest incoming plan edge and its cost,
        /// excluding nodes contained within the given top super node (if given)
        /// </summary>
        public override PlanEdge GetCheapestIncoming(PlanPseudoNode excludeTopNode, out float minCost)
        {
            minCost = float.MaxValue;
            PlanEdge minEdge = null;

            PlanPseudoNode current = Child;
            do
            {
                float curCost;
                PlanEdge curEdge = current.GetCheapestIncoming(excludeTopNode, out curCost);
                if(curEdge != null)
                {
                    PlanPseudoNode target = curEdge.Target;
                    while(target.SuperNode != this)
                        target = target.SuperNode;

                    if(PreferNewEdge(minEdge, minCost, curEdge, curCost))
                    {
                        minCost = curCost;
                        minEdge = curEdge;
                    }
                }
                current = current.IncomingMSAEdge.Source;
                while(current.SuperNode != this)
                    current = current.SuperNode;
            }
            while(current != Child);
            return minEdge;
        }
    }

    /// <summary>
    /// A plan edge represents a matching operation and its costs.
    /// </summary>
    [DebuggerDisplay("PlanEdge ({Source} -{Type}-> {Target} = {Cost})")]
    public class PlanEdge
    {
        public PlanNode Source;
        public PlanNode Target;
        public float Cost;

        /// <summary>
        /// Cost used by the operation selection.
        /// This represents max(log(Cost),1).
        /// This field is altered during the contraction algorithm.
        /// </summary>
        public float mstCost;

        public SearchOperationType Type;

        public PlanEdge(SearchOperationType type, PlanNode source, PlanNode target, float cost)
        {
            Source = source;
            Target = target;
            Cost = cost;
#if OPCOST_WITH_GEO_MEAN 
            mstCost = cost;
#else
            mstCost = (float) Math.Max(Math.Log(cost), 1);
//            mstCost = (float) Math.Max(Math.Log(cost), 0.0001);
//            mstCost = cost;
#endif
            Type = type;
        }
    }

    /// <summary>
    /// The plan graph data structure for the MSA-algorithm.
    /// </summary>
    public class PlanGraph
    {
        public PlanNode Root;
        public PlanNode[] Nodes;        // Nodes does not contain Root
        public PlanEdge[] Edges;
        public PatternGraph PatternGraph;

        public PlanGraph(PlanNode root, PlanNode[] nodes, PlanEdge[] edges, PatternGraph patternGraph)
        {
            Root = root;
            Nodes = nodes;
            Edges = edges;
            PatternGraph = patternGraph;
        }
    }

    /// <summary>
    /// Element of the search plan graph representing an element within the pattern graph or a root node.
    /// </summary>>
    [DebuggerDisplay("SearchPlanNode ({NodeType} {ToString()})")]
    public class SearchPlanNode
    {
        public PlanNodeType NodeType;
        // scheduling needs outgoing edges, MSA needs incoming edges
        public List<SearchPlanEdge> OutgoingEdges = new List<SearchPlanEdge>();
        public int ElementID;
        public bool IsPreset;
        public bool Visited; // flag needed in matcher program generation from the scheduled search plan 

        public PatternElement PatternElement;

        public SearchPlanNode(String rootName)
        {
            NodeType = PlanNodeType.Root;
            PatternElement = new PatternNode(-1, rootName, null, null);
        }

        protected SearchPlanNode(PlanNode planNode)
        {
            NodeType = planNode.NodeType;
            ElementID = planNode.ElementID;
            IsPreset = planNode.IsPreset;

            PatternElement = planNode.PatternElement;
        }

        protected SearchPlanNode(PlanNodeType nodeType, int elemID, bool isPreset, PatternElement patternElem)
        {
            NodeType = nodeType;
            ElementID = elemID;
            IsPreset = isPreset;
            PatternElement = patternElem;
        }

        public override string ToString()
        {
            return PatternElement.Name;
        }
    }

    /// <summary>
    /// Element of the search plan graph representing a node within the pattern graph.
    /// </summary>>
    public class SearchPlanNodeNode : SearchPlanNode
    {
        public LinkedList<SearchPlanEdgeNode> IncomingPatternEdges = new LinkedList<SearchPlanEdgeNode>();
        public LinkedList<SearchPlanEdgeNode> OutgoingPatternEdges = new LinkedList<SearchPlanEdgeNode>();

        public SearchPlanNodeNode(PlanNode planNode) : base(planNode) { }

        public SearchPlanNodeNode(PlanNodeType nodeType, int elemID, bool isPreset, PatternElement patternElem)
            : base(nodeType, elemID, isPreset, patternElem) { }
    }

    /// <summary>
    /// Element of the search plan graph representing an edge within the pattern graph.
    /// </summary>>
    public class SearchPlanEdgeNode : SearchPlanNode
    {
        public SearchPlanNodeNode PatternEdgeSource, PatternEdgeTarget;

        public SearchPlanEdgeNode(PlanNode planNode, SearchPlanNodeNode patEdgeSrc, SearchPlanNodeNode patEdgeTgt)
            : base(planNode)
        {
            PatternEdgeSource = patEdgeSrc;
            PatternEdgeTarget = patEdgeTgt;
        }

        public SearchPlanEdgeNode(PlanNodeType nodeType, int elemID, bool isPreset, PatternElement patternElem,
            SearchPlanNodeNode patEdgeSrc, SearchPlanNodeNode patEdgeTgt) : base(nodeType, elemID, isPreset, patternElem)
        {
            PatternEdgeSource = patEdgeSrc;
            PatternEdgeTarget = patEdgeTgt;
        }

    }

    /// <summary>
    /// A search plan edge represents a matching operation and its costs.
    /// </summary>
    public class SearchPlanEdge : IComparable<SearchPlanEdge>
    {
        public SearchPlanNode Source;
        public SearchPlanNode Target;
        public float Cost;
        public SearchOperationType Type;

        public float LocalCost;

        public SearchPlanEdge(SearchOperationType type, SearchPlanNode source, SearchPlanNode target, float cost)
        {
            Source = source;
            Target = target;
            Cost = cost;
            Type = type;
        }

        // order along costs, needed as priority for priority queue
        public int CompareTo(SearchPlanEdge other)
        {
            // Schedule implicit ops as early as possible
            if(Type == SearchOperationType.ImplicitSource || Type == SearchOperationType.ImplicitTarget) return -1;
            if(other.Type == SearchOperationType.ImplicitSource || other.Type == SearchOperationType.ImplicitTarget) return 1;

            float epsilon = 0.001f;
            float diff = Cost - other.Cost;
            if(diff < -epsilon) return -1;
            else if(diff > epsilon) return 1;

            // Choose equally expensive operations in this order: incoming/outgoing, edge lookup, node lookup

            if(Type == SearchOperationType.Incoming || Type == SearchOperationType.Outgoing) return -1;
            if(other.Type == SearchOperationType.Incoming || other.Type == SearchOperationType.Outgoing) return 1;

            if(Type == SearchOperationType.Lookup && Target.NodeType == PlanNodeType.Edge) return -1;
            if(other.Type == SearchOperationType.Lookup && other.Target.NodeType == PlanNodeType.Edge) return 1;

            // Both are node lookups...

            if(diff < 0) return -1;
            else if(diff > 0) return 1;
            else return 0;
        }
    }

    /// <summary>
    /// The search plan graph data structure for scheduling.
    /// </summary>
    public class SearchPlanGraph
    {
        public SearchPlanNode Root;
        public SearchPlanNode[] Nodes;
        public SearchPlanEdge[] Edges;
        public PatternGraph PatternGraph;
        public int NumPresetElements;

        public SearchPlanGraph(SearchPlanNode root, SearchPlanNode[] nodes, SearchPlanEdge[] edges, PatternGraph patternGraph)
        {
            Root = root;
            Nodes = nodes;
            Edges = edges;
            PatternGraph = patternGraph;
            NumPresetElements = 0;
            foreach(SearchPlanNode node in nodes)
                if(node.IsPreset)
                    NumPresetElements++;
        }
    }

    public enum SearchOperationType { 
        Void, // void operation; retype to void to delete operation from ssp quickly
        MaybePreset,
        NegPreset, 
        Lookup, 
        Outgoing, 
        Incoming, 
        ImplicitSource, 
        ImplicitTarget, 
        NegativePattern, 
        Condition 
    };

    public class IsomorphyInformation
    {
        public bool MustSetMapped = false; // if true, the graph element's (neg/)mappedTo field must be set
        public bool MustCheckMapped = false; // if true, the graph element's (neg/)mappedTo field must be checked
        public int HomomorphicID = 0;
    }

    /// <summary>
    /// Search operation with information about homomorphic mapping.
    /// Element of the scheduled search plan.
    /// </summary>
    [DebuggerDisplay("SearchOperation ({SourceSPNode} -{Type}-> {Element} = {CostToEnd})")]
    public class SearchOperation : IComparable<SearchOperation>
    {
        public SearchOperationType Type;
        /// <summary>
        /// If Type is NegativePattern, Element is a negative ScheduledSearchPlan object.
        /// If Type is Condition, Element is a Condition object.
        /// Otherwise Element is the target SearchPlanNode for this operation.
        /// </summary>
        public object Element;
        public SearchPlanNode SourceSPNode;
        public float CostToEnd;

        // used in check for isomorphic elements
        public IsomorphyInformation Isomorphy = new IsomorphyInformation();

        public SearchOperation(SearchOperationType type, object elem, SearchPlanNode srcSPNode, float costToEnd)
        {
            Type = type;
            Element = elem;
            SourceSPNode = srcSPNode;
            CostToEnd = costToEnd;
        }

        public static SearchOperation CreateMaybePreset(SearchPlanNode element)
        {
            return new SearchOperation(SearchOperationType.MaybePreset, element, null, 0);
        }

        public static SearchOperation CreateNegPreset(SearchPlanNode element)
        {
            return new SearchOperation(SearchOperationType.NegPreset, element, null, 0);
        }

        public static SearchOperation CreateLookup(SearchPlanNode element, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Lookup, element, null, costToEnd);
        }

        public static SearchOperation CreateOutgoing(SearchPlanNodeNode source, SearchPlanEdgeNode outgoingEdge, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Outgoing, outgoingEdge, source, costToEnd);
        }

        public static SearchOperation CreateIncoming(SearchPlanNodeNode source, SearchPlanEdgeNode outgoingEdge, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Incoming, outgoingEdge, source, costToEnd);
        }

        public static SearchOperation CreateImplicitSource(SearchPlanEdgeNode edge, SearchPlanNodeNode sourceNode, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.ImplicitSource, sourceNode, edge, costToEnd);
        }

        public static SearchOperation CreateImplicitTarget(SearchPlanEdgeNode edge, SearchPlanNodeNode targetNode, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.ImplicitTarget, targetNode, edge, costToEnd);
        }

        public static SearchOperation CreateNegativePattern(ScheduledSearchPlan schedSP, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.NegativePattern, schedSP, null, costToEnd);
        }

        public static SearchOperation CreateCondition(Condition condition, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Condition, condition, null, costToEnd);
        }

        public int CompareTo(SearchOperation other)
        {
            float diff = CostToEnd - other.CostToEnd;
            if(diff < 0) return -1;
            else if(diff > 0) return 1;
            else return 0;
        }
    }

    /// <summary>
    /// The scheduled search plan is a list of search operations, plus the information which nodes/edges are homomorph
    /// </summary>
    public class ScheduledSearchPlan
    {
        public float Cost; // (needed for scheduling nac-subgraphs into the full graph)
        public SearchOperation[] Operations;
        public PatternGraph PatternGraph;

        public ScheduledSearchPlan(SearchOperation[] ops, PatternGraph patternGraph, float cost)
        {
            Operations = ops;
            PatternGraph = patternGraph;
            Cost = cost;
        }
    }

    /// <summary>
    /// Class generating matcher programs out of rules.
    /// </summary>
    public class LGSPMatcherGenerator
    {
        /// <summary>
        /// The model for which the matcher functions shall be generated.
        /// </summary>
        private IGraphModel model;

        /// <summary>
        /// If true, the generated matcher functions are commented to improve understanding the source code.
        /// </summary>
        public bool CommentSourceCode = false;

        /// <summary>
        /// If true, the source code of dynamically generated matcher functions are dumped to a file in the current directory.
        /// </summary>
        public bool DumpDynSourceCode = false;

        /// <summary>
        /// If true, generated search plans are dumped in VCG and TXT files in the current directory.
        /// </summary>
        public bool DumpSearchPlan = false;

        /// <summary>
        /// Instantiates a new instance of LGSPMatcherGenerator with the given graph model.
        /// </summary>
        /// <param name="model">The model for which the matcher functions shall be generated.</param>
        public LGSPMatcherGenerator(IGraphModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Builds a plan graph out of a given pattern graph.
        /// </summary>
        /// <param name="graph">The host graph to optimize the matcher program for, 
        /// providing statistical information about its structure </param>
        public PlanGraph GeneratePlanGraph(LGSPGraph graph, PatternGraph patternGraph, bool negativePatternGraph)
        {
            PlanNode[] nodes = new PlanNode[patternGraph.Nodes.Length + patternGraph.Edges.Length];
            // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)
            List<PlanEdge> edges = new List<PlanEdge>(patternGraph.Nodes.Length + 5 * patternGraph.Edges.Length);

            int nodesIndex = 0;

            PlanNode root = new PlanNode("root");

            // create plan nodes and lookup plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.Nodes.Length; i++)
            {
                PatternNode node = patternGraph.nodes[i];
                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(node.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && node.PatternElementType == PatternElementType.Normal)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
#if OPCOST_WITH_GEO_MEAN
                    cost = graph.nodeLookupCosts[node.TypeID];
#else
                    cost = graph.nodeCounts[node.TypeID];
#endif
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                nodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                edges.Add(rootToNodeEdge);
                nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
                node.TempPlanMapping = nodes[nodesIndex];
                nodesIndex++;
            }

            // create plan nodes and necessary plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.Edges.Length; i++)
            {
                PatternEdge edge = patternGraph.edges[i];

                bool isPreset;
#if !NO_EDGE_LOOKUP
                float cost;
                SearchOperationType searchOperationType;
                if(edge.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
#if VSTRUCT_VAL_FOR_EDGE_LOOKUP
                    int sourceTypeID;
                    if(edge.source != null) sourceTypeID = edge.source.TypeID;
                    else sourceTypeID = model.NodeModel.RootType.TypeID;
                    int targetTypeID;
                    if(edge.target != null) targetTypeID = edge.target.TypeID;
                    else targetTypeID = model.NodeModel.RootType.TypeID;
  #if MONO_MULTIDIMARRAY_WORKAROUND
                    cost = graph.vstructs[((sourceTypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                        + targetTypeID) * 2 + (int) LGSPDir.Out];
  #else
                    cost = graph.vstructs[sourceTypeID, edge.TypeID, targetTypeID, (int) LGSPDir.Out];
  #endif
#elif OPCOST_WITH_GEO_MEAN 
                    cost = graph.edgeLookupCosts[edge.TypeID];
#else
                    cost = graph.edgeCounts[edge.TypeID];
#endif

                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset);
                PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                edges.Add(rootToNodeEdge);
                nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
#else
                SearchOperationType searchOperationType = SearchOperationType.Lookup;       // lookup as dummy
                if(edge.PatternElementType == PatternElementType.Preset)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else isPreset = false;
                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset);
                if(isPreset)
                {
                    PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], 0);
                    edges.Add(rootToNodeEdge);
                    nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
                }
#endif
                // only add implicit source operation if edge source is needed and the edge source is not a preset node
                if(edge.source != null && !edge.source.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implSrcEdge = new PlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], edge.source.TempPlanMapping, 0);
#else
                    PlanEdge implSrcEdge = new PlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], edge.source.TempPlanMapping, 1);
#endif
                    edges.Add(implSrcEdge);
                    edge.source.TempPlanMapping.IncomingEdges.Add(implSrcEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node
                if(edge.target != null && !edge.target.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implTgtEdge = new PlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], edge.target.TempPlanMapping, 0);
#else
                    PlanEdge implTgtEdge = new PlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], edge.target.TempPlanMapping, 1);
#endif
                    edges.Add(implTgtEdge);
                    edge.target.TempPlanMapping.IncomingEdges.Add(implTgtEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset
                if(!isPreset)
                {
                    // no outgoing if no source
                    if(edge.source != null)
                    {
                        int targetTypeID;
                        if(edge.target != null) targetTypeID = edge.target.TypeID;
                        else targetTypeID = model.NodeModel.RootType.TypeID;
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float normCost = graph.vstructs[((edge.source.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + targetTypeID) * 2 + (int) LGSPDir.Out];
#else
                        float normCost = graph.vstructs[edge.source.TypeID, edge.TypeID, targetTypeID, (int) LGSPDir.Out];
#endif
                        if(graph.nodeCounts[edge.source.TypeID] != 0)
                            normCost /= graph.nodeCounts[edge.source.TypeID];
                        PlanEdge outEdge = new PlanEdge(SearchOperationType.Outgoing, edge.source.TempPlanMapping, nodes[nodesIndex], normCost);
                        edges.Add(outEdge);
                        nodes[nodesIndex].IncomingEdges.Add(outEdge);
                    }

                    // no incoming if no target
                    if(edge.target != null)
                    {
                        int sourceTypeID;
                        if(edge.source != null) sourceTypeID = edge.source.TypeID;
                        else sourceTypeID = model.NodeModel.RootType.TypeID;
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float revCost = graph.vstructs[((edge.target.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + sourceTypeID) * 2 + (int) LGSPDir.In];
#else
                        float revCost = graph.vstructs[edge.target.TypeID, edge.TypeID, sourceTypeID, (int) LGSPDir.In];
#endif
                        if(graph.nodeCounts[edge.target.TypeID] != 0)
                            revCost /= graph.nodeCounts[edge.target.TypeID];
                        PlanEdge inEdge = new PlanEdge(SearchOperationType.Incoming, edge.target.TempPlanMapping, nodes[nodesIndex], revCost);
                        edges.Add(inEdge);
                        nodes[nodesIndex].IncomingEdges.Add(inEdge);
                    }
                }

                nodesIndex++;
            }
            return new PlanGraph(root, nodes, edges.ToArray(), patternGraph);
        }

        /// <summary>
        /// Marks the minimum spanning arborescence of a plan graph by setting the IncomingMSAEdge
        /// fields for all nodes
        /// </summary>
        /// <param name="planGraph">The plan graph to be marked</param>
        /// <param name="dumpName">Names the dump targets if dump compiler flags are set</param>
        public void MarkMinimumSpanningArborescence(PlanGraph planGraph, String dumpName)
        {
            if(DumpSearchPlan)
                DumpPlanGraph(planGraph, dumpName, "initial");

            // nodes not already looked at
            Dictionary<PlanNode, bool> leftNodes = new Dictionary<PlanNode, bool>(planGraph.Nodes.Length);
            foreach(PlanNode node in planGraph.Nodes)
                leftNodes.Add(node, true);

            // epoch = search run
            Dictionary<PlanPseudoNode, bool> epoch = new Dictionary<PlanPseudoNode, bool>();
            LinkedList<PlanSuperNode> superNodeStack = new LinkedList<PlanSuperNode>();

            // work left ?
            while(leftNodes.Count > 0)
            {
                // get first remaining node
                Dictionary<PlanNode, bool>.Enumerator enumerator = leftNodes.GetEnumerator();
                enumerator.MoveNext();
                PlanPseudoNode curNode = enumerator.Current.Key;

                // start a new search run
                epoch.Clear();
                do
                {
                    // next node in search run
                    epoch.Add(curNode, true);
                    if(curNode is PlanNode) leftNodes.Remove((PlanNode) curNode);

                    // cheapest incoming edge of current node
                    float cost;
                    PlanEdge cheapestEdge = curNode.GetCheapestIncoming(curNode, out cost);
                    if(cheapestEdge == null)
                        break;
                    curNode.IncomingMSAEdge = cheapestEdge;

                    // cycle found ?
                    while(epoch.ContainsKey(cheapestEdge.Source.TopNode))
                    {
                        // contract the cycle to a super node
                        PlanSuperNode superNode = new PlanSuperNode(cheapestEdge.Source.TopNode);
                        superNodeStack.AddFirst(superNode);
                        epoch.Add(superNode, true);
                        if(superNode.IncomingMSAEdge == null)
                            goto exitSecondLoop;
                        // continue with new super node as current node
                        curNode = superNode;
                        cheapestEdge = curNode.IncomingMSAEdge;
                    }
                    curNode = cheapestEdge.Source.TopNode;
                }
                while(true);
exitSecondLoop: ;
            }

            if(DumpSearchPlan)
            {
                DumpPlanGraph(planGraph, dumpName, "contracted");
                DumpContractedPlanGraph(planGraph, dumpName);
            }

            // breaks all cycles represented by setting the incoming msa edges of the 
            // representative nodes to the incoming msa edges according to the supernode
            /*no, not equivalent:
            foreach(PlanSuperNode superNode in superNodeStack)
                superNode.Child.IncomingMSAEdge = superNode.IncomingMSAEdge;*/
            foreach (PlanSuperNode superNode in superNodeStack)
            {
                PlanPseudoNode curNode = superNode.IncomingMSAEdge.Target;
                while (curNode.SuperNode != superNode) curNode = curNode.SuperNode;
                curNode.IncomingMSAEdge = superNode.IncomingMSAEdge;
            }

            if(DumpSearchPlan)
                DumpFinalPlanGraph(planGraph, dumpName);
        }

        #region Dump functions
        private String GetDumpName(PlanNode node)
        {
            if(node.NodeType == PlanNodeType.Root) return "root";
            else if(node.NodeType == PlanNodeType.Node) return "node_" + node.PatternElement.Name;
            else return "edge_" + node.PatternElement.Name;
        }

        public void DumpPlanGraph(PlanGraph planGraph, String dumpname, String namesuffix)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-plangraph-" + namesuffix + ".vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes:yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(PlanNode node in planGraph.Nodes)
            {
                DumpNode(sw, node);
            }
            foreach(PlanEdge edge in planGraph.Edges)
            {
                DumpEdge(sw, edge, true);
            }
            sw.WriteLine("}\n}\n");
            sw.Close();
        }

        private void DumpNode(StreamWriter sw, PlanNode node)
        {
            if(node.NodeType == PlanNodeType.Edge)
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\" shape:ellipse}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
            else
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\"}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
        }

        private void DumpEdge(StreamWriter sw, PlanEdge edge, bool markRed)
        {
            String typeStr = " #";
            switch(edge.Type)
            {
                case SearchOperationType.Outgoing: typeStr = "--"; break;
                case SearchOperationType.Incoming: typeStr = "->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
            }

            sw.WriteLine("edge:{{sourcename:\"{0}\" targetname:\"{1}\" label:\"{2} / {3:0.00} ({4:0.00}) \"{5}}}",
                GetDumpName(edge.Source), GetDumpName(edge.Target), typeStr, edge.mstCost, edge.Cost,
                markRed ? " color:red" : "");
        }

        private void DumpSuperNode(StreamWriter sw, PlanSuperNode superNode, Dictionary<PlanSuperNode, bool> dumpedSuperNodes)
        {
            PlanPseudoNode curNode = superNode.Child;
            sw.WriteLine("graph:{title:\"super node\" label:\"super node\" status:clustered color:lightgrey");
            dumpedSuperNodes.Add(superNode, true);
            do
            {
                if(curNode is PlanSuperNode)
                {
                    DumpSuperNode(sw, (PlanSuperNode) curNode, dumpedSuperNodes);
                    DumpEdge(sw, curNode.IncomingMSAEdge, true);
                }
                else
                {
                    DumpNode(sw, (PlanNode) curNode);
                    DumpEdge(sw, curNode.IncomingMSAEdge, false);
                }
                curNode = curNode.IncomingMSAEdge.Source;
                while(curNode.SuperNode != null && curNode.SuperNode != superNode)
                    curNode = curNode.SuperNode;
            }
            while(curNode != superNode.Child);
            sw.WriteLine("}");
        }

        private void DumpContractedPlanGraph(PlanGraph planGraph, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-contractedplangraph.vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes:yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            Dictionary<PlanSuperNode, bool> dumpedSuperNodes = new Dictionary<PlanSuperNode, bool>();

            foreach(PlanNode node in planGraph.Nodes)
            {
                PlanSuperNode superNode = node.TopSuperNode;
                if(superNode != null)
                {
                    if(dumpedSuperNodes.ContainsKey(superNode)) continue;
                    DumpSuperNode(sw, superNode, dumpedSuperNodes);
                    DumpEdge(sw, superNode.IncomingMSAEdge, true);
                }
                else
                {
                    DumpNode(sw, node);
                    DumpEdge(sw, node.IncomingMSAEdge, false);
                }
            }

            sw.WriteLine("}\n}");
            sw.Close();
        }

        private void DumpFinalPlanGraph(PlanGraph planGraph, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-finalplangraph.vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes:yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(PlanNode node in planGraph.Nodes)
            {
                DumpNode(sw, node);
                DumpEdge(sw, node.IncomingMSAEdge, false);
            }

            sw.WriteLine("}\n}");
            sw.Close();
        }

        private String GetDumpName(SearchPlanNode node)
        {
            if(node.NodeType == PlanNodeType.Root) return "root";
            else if(node.NodeType == PlanNodeType.Node) return "node_" + node.PatternElement.Name;
            else return "edge_" + node.PatternElement.Name;
        }

        private void DumpNode(StreamWriter sw, SearchPlanNode node)
        {
            if(node.NodeType == PlanNodeType.Edge)
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\" shape:ellipse}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
            else
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\"}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
        }

        private void DumpEdge(StreamWriter sw, SearchOperationType opType, SearchPlanNode source, SearchPlanNode target, float cost, bool markRed)
        {
            String typeStr = " #";
            switch(opType)
            {
                case SearchOperationType.Outgoing: typeStr = "--"; break;
                case SearchOperationType.Incoming: typeStr = "->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
            }

            sw.WriteLine("edge:{{sourcename:\"{0}\" targetname:\"{1}\" label:\"{2} / {3:0.00}\"{4}}}",
                GetDumpName(source), GetDumpName(target), typeStr, cost, markRed ? " color:red" : "");
        }

        private String SearchOpToString(SearchOperation op)
        {
            String typeStr = "  ";
            SearchPlanNode src = op.SourceSPNode as SearchPlanNode;
            SearchPlanNode tgt = op.Element as SearchPlanNode;
            switch(op.Type)
            {
                case SearchOperationType.Outgoing: typeStr = src.PatternElement.Name + "-" + tgt.PatternElement.Name + "->"; break;
                case SearchOperationType.Incoming: typeStr = src.PatternElement.Name + "<-" + tgt.PatternElement.Name + "-"; break;
                case SearchOperationType.ImplicitSource: typeStr = "<-" + src.PatternElement.Name + "-" + tgt.PatternElement.Name; break;
                case SearchOperationType.ImplicitTarget: typeStr = "-" + src.PatternElement.Name + "->" + tgt.PatternElement.Name; break;
                case SearchOperationType.Lookup: typeStr = "*" + tgt.PatternElement.Name; break;
                case SearchOperationType.MaybePreset: typeStr = "p" + tgt.PatternElement.Name; break;
                case SearchOperationType.NegPreset: typeStr = "np" + tgt.PatternElement.Name; break;
                case SearchOperationType.Condition:
                    typeStr = " ?(" + String.Join(",", ((Condition) op.Element).NeededNodes) + ")("
                        + String.Join(",", ((Condition) op.Element).NeededEdges) + ")";
                    break;
                case SearchOperationType.NegativePattern:
                    typeStr = " !(" + ScheduleToString(((ScheduledSearchPlan) op.Element).Operations) + " )";
                    break;
            }
            return typeStr;
        }

        private String ScheduleToString(IEnumerable<SearchOperation> schedule)
        {
            StringBuilder str = new StringBuilder();

            foreach(SearchOperation searchOp in schedule)
                str.Append(' ').Append(SearchOpToString(searchOp));

            return str.ToString();
        }

        private void DumpScheduledSearchPlan(ScheduledSearchPlan ssp, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-scheduledsp.txt", false);
            sw.WriteLine(ScheduleToString(ssp.Operations));
            sw.Close();

/*            StreamWriter sw = new StreamWriter(dumpname + "-scheduledsp.vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes: yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");
            SearchPlanNode root = new SearchPlanNode("root");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(SearchOperation op in ssp.Operations)
            {
                switch(op.Type)
                {
                    case SearchOperationType.Lookup:
                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                    {
                        SearchPlanNode spnode = (SearchPlanNode) op.Element;
                        DumpNode(sw, spnode);
                        SearchPlanNode src;
                        switch(op.Type)
                        {
                            case SearchOperationType.Lookup:
                            case SearchOperationType.MaybePreset:
                            case SearchOperationType.NegPreset:
                                src = root;
                                break;
                            default:
                                src = op.SourceSPNode;
                                break;
                        }
                        DumpEdge(sw, op.Type, src, spnode, op.CostToEnd, false);
                        break;
                    }
                    case SearchOperationType.Condition:
                        sw.WriteLine("node:{title:\"Condition\" label:\"CONDITION\"}\n");
                        break;
                    case SearchOperationType.NegativePattern:
                        sw.WriteLine("node:{title:\"NAC\" label:\"NAC\"}\n");
                        break;
                }
            }*/
        }
        #endregion Dump functions

        /// <summary>
        /// Generate search plan graph out of the plan graph, i.e. construct a new graph with outgoing
        /// edges for nodes and only tree edges
        /// </summary>
        /// <param name="planGraph">The source plan graph</param>
        /// <returns>A new search plan graph</returns>
        public SearchPlanGraph GenerateSearchPlanGraph(PlanGraph planGraph)
        {
            SearchPlanNode root = new SearchPlanNode("search plan root");
            SearchPlanNode[] nodes = new SearchPlanNode[planGraph.Nodes.Length];
            SearchPlanEdge[] edges = new SearchPlanEdge[planGraph.Nodes.Length - 1 + 1];    // +1 for root
            // for generating edges
            Dictionary<PlanNode, SearchPlanNode> planMap = new Dictionary<PlanNode, SearchPlanNode>(planGraph.Nodes.Length);
            planMap.Add(planGraph.Root, root);

            // generate the search plan graph nodes
            int i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                if(node.NodeType == PlanNodeType.Edge)
                    nodes[i] = new SearchPlanEdgeNode(node, null, null);
                else
                    nodes[i] = new SearchPlanNodeNode(node);
                planMap.Add(node, nodes[i]);
                i++;
            }

            // generate the search plan graph edges
            i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                PlanEdge edge = node.IncomingMSAEdge;
                edges[i] = new SearchPlanEdge(edge.Type, planMap[edge.Source], planMap[edge.Target], edge.Cost);
                planMap[edge.Source].OutgoingEdges.Add(edges[i]);
                if(node.NodeType == PlanNodeType.Edge)
                {
                    SearchPlanEdgeNode spedgenode = (SearchPlanEdgeNode) planMap[node];
                    SearchPlanNode patelem;
                    if(edge.Target.PatternEdgeSource != null && planMap.TryGetValue(edge.Target.PatternEdgeSource, out patelem))
                    {
                        spedgenode.PatternEdgeSource = (SearchPlanNodeNode) patelem;
                        spedgenode.PatternEdgeSource.OutgoingPatternEdges.AddLast(spedgenode);
                    }
                    if(edge.Target.PatternEdgeTarget != null && planMap.TryGetValue(edge.Target.PatternEdgeTarget, out patelem))
                    {
                        spedgenode.PatternEdgeTarget = (SearchPlanNodeNode) patelem;
                        spedgenode.PatternEdgeTarget.IncomingPatternEdges.AddLast(spedgenode);
                    }
                }
                i++;
            }
            return new SearchPlanGraph(root, nodes, edges, planGraph.PatternGraph);
        }

        /// <summary>
        /// Generates a scheduled search plan for a given search plan graph and optional negative search plan graphs
        /// </summary>
        /// <param name="negSpGraphs">a list - possibly empty - if a positive search plan graph is given,
        /// null if a negative search plan graph is scheduled </param>
        public ScheduledSearchPlan ScheduleSearchPlan(SearchPlanGraph spGraph, SearchPlanGraph[] negSpGraphs)
        {
            List<SearchOperation> operations = new List<SearchOperation>();

            //
            // schedule positive search plan
            //
            
            // a set of search plan edges representing the currently reachable not yet visited elements
            PriorityQueue<SearchPlanEdge> activeEdges = new PriorityQueue<SearchPlanEdge>();

            // first schedule all preset elements
            foreach(SearchPlanNode node in spGraph.Nodes)
            {
                if(!node.IsPreset) continue;

                foreach(SearchPlanEdge edge in node.OutgoingEdges)
                    activeEdges.Add(edge);

                SearchOperation newOp = new SearchOperation(
                    negSpGraphs == null ? SearchOperationType.NegPreset : SearchOperationType.MaybePreset,
                    node, spGraph.Root, 0);

                operations.Add(newOp);
            }

            // iterate over all reachable elements until the whole graph has been scheduled(/visited),
            // choose next cheapest operation, update the reachable elements and the search plan costs
            SearchPlanNode lastNode = spGraph.Root;
            for(int i = 0; i < spGraph.Nodes.Length - spGraph.NumPresetElements; i++)
            {
                foreach (SearchPlanEdge edge in lastNode.OutgoingEdges)
                {
                    /* no, that if is needed */
                    if(edge.Type == SearchOperationType.MaybePreset || edge.Type == SearchOperationType.NegPreset) continue;
                    activeEdges.Add(edge);
                }
                SearchPlanEdge minEdge = activeEdges.DequeueFirst();
                lastNode = minEdge.Target;

                SearchOperation newOp = new SearchOperation(minEdge.Type, lastNode,
                    minEdge.Source, minEdge.Cost);

                foreach(SearchOperation op in operations)
                    op.CostToEnd += minEdge.Cost;

                operations.Add(newOp);
            }

            // insert negative search plans into the operation schedule
            if(negSpGraphs != null)
            {
                // schedule each negative search plan

                ScheduledSearchPlan[] negScheduledSPs = new ScheduledSearchPlan[negSpGraphs.Length];
                for(int i = 0; i < negSpGraphs.Length; i++)
                    negScheduledSPs[i] = ScheduleSearchPlan(negSpGraphs[i], null);

                // which elements belong to the positive pattern

                Dictionary<String, bool> positiveElements = new Dictionary<String, bool>();
                foreach(SearchPlanNode spnode in spGraph.Nodes)
                    positiveElements.Add(spnode.PatternElement.Name, true);

                // iterate over all negative scheduled search plans (TODO: order?)
                Dictionary<String, bool> neededElements = new Dictionary<String, bool>();
                foreach(ScheduledSearchPlan negSSP in negScheduledSPs)
                {
                    int bestFitIndex = operations.Count;
                    float bestFitCostToEnd = 0;

                    // calculate needed elements for the negative pattern
                    // (elements from the positive graph needed in order to execute the nac)

                    neededElements.Clear();
                    foreach(SearchOperation op in negSSP.Operations)
                    {
                        if(op.Type == SearchOperationType.NegativePattern) continue;
                        if(op.Type == SearchOperationType.Condition)
                        {
                            Condition cond = (Condition) op.Element;
                            foreach(String neededNode in cond.NeededNodes)
                            {
                                if(positiveElements.ContainsKey(neededNode))
                                    neededElements[neededNode] = true;
                            }
                            foreach(String neededEdge in cond.NeededEdges)
                            {
                                if(positiveElements.ContainsKey(neededEdge))
                                    neededElements[neededEdge] = true;
                            }
                        }
                        else
                        {
                            SearchPlanNode spnode = (SearchPlanNode) op.Element;
                            if(positiveElements.ContainsKey(spnode.PatternElement.Name))
                                neededElements[spnode.PatternElement.Name] = true;
                        }
                    }

                    // find best place for this pattern
                    for(int i = operations.Count - 1; i >= 0; i--)
                    {
                        SearchOperation op = operations[i];
                        if(op.Type == SearchOperationType.NegativePattern 
                            || op.Type == SearchOperationType.Condition) continue;
                        // needed element matched at this operation?
                        if(neededElements.ContainsKey(((SearchPlanNode) op.Element).PatternElement.Name))
                            break;                      // yes, stop here
                        if(negSSP.Cost <= op.CostToEnd)
                        {
                            // best fit as CostToEnd is monotonously growing towards operation[0]
                            bestFitIndex = i;
                            bestFitCostToEnd = op.CostToEnd;
                        }
                    }

                    // and insert pattern there
                    operations.Insert(bestFitIndex, new SearchOperation(SearchOperationType.NegativePattern,
                        negSSP, null, bestFitCostToEnd + negSSP.Cost));

                    // update costs of operations before bestFitIndex
                    for(int i = 0; i < bestFitIndex; i++)
                        operations[i].CostToEnd += negSSP.Cost;
                }
            }

            // Schedule conditions at the earliest position possible

            Condition[] conditions = spGraph.PatternGraph.Conditions;
            for(int j = conditions.Length - 1; j >= 0; j--)            
            {
                Condition condition = conditions[j];
                int k;
                float costToEnd = 0;
                for(k = operations.Count - 1; k >= 0; k--)
                {
                    SearchOperation op = operations[k];
                    if(op.Type == SearchOperationType.NegativePattern || op.Type == SearchOperationType.Condition) continue;
                    String curElemName = ((SearchPlanNode) op.Element).PatternElement.Name;
                    bool isNeededElem = false;
                    if(((SearchPlanNode) op.Element).NodeType == PlanNodeType.Node)
                    {
                        foreach(String neededNode in condition.NeededNodes)
                        {
                            if(curElemName == neededNode)
                            {
                                isNeededElem = true;
                                costToEnd = op.CostToEnd;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach(String neededEdge in condition.NeededEdges)
                        {
                            if(curElemName == neededEdge)
                            {
                                isNeededElem = true;
                                costToEnd = op.CostToEnd;
                                break;
                            }
                        }
                    }
                    // does the current operation retrieve a needed element ?
                    if(isNeededElem)
                        break;          // yes, so the condition cannot be scheduled earlier
                }
                operations.Insert(k + 1, new SearchOperation(SearchOperationType.Condition, condition, null, costToEnd));
            }

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            return new ScheduledSearchPlan(operations.ToArray(), spGraph.PatternGraph, cost);
        }

        /// <summary>
        /// checks for each operation in the scheduled search plan whether the resulting host graph element 
        /// must be mapped to a pattern element, needed for isomorphism checks later on
        /// </summary>
        public void CalculateNeededMaps(ScheduledSearchPlan schedSP)
        {
            // examine all operations pairwise
            // find first element of pair here, second is searched for within CheckMapsForOperation
            for(int i = 0; i < schedSP.Operations.Length; i++)
            {
                SearchOperation op1 = schedSP.Operations[i];

                if(op1.Type == SearchOperationType.Condition) continue;
                if(op1.Type == SearchOperationType.NegativePattern)
                {
                    ScheduledSearchPlan curSSP = (ScheduledSearchPlan) op1.Element;
                    for(int j = 0; j < curSSP.Operations.Length - 1; j++)
                    {
                        op1 = curSSP.Operations[j];
                        if(op1.Type == SearchOperationType.Condition) continue;
                        CheckMapsForOperation(op1, curSSP, j + 1);
                    }
                }
                else
                {
                    if(i == schedSP.Operations.Length - 1) break;
                    CheckMapsForOperation(op1, schedSP, i + 1);
                }
            }
        }

        /// <summary>
        /// Checks whether two non-homomorphic pattern elements might be matched as the same host graph element.
        /// For any pair fulfilling this condition, flags are set letting the first operation(op1) set the
        /// mappedTo/negMappedTo-field of the host graph element and the second operation check this field
        /// </summary>
        private void CheckMapsForOperation(SearchOperation op1, ScheduledSearchPlan curSSP, int op2StartIndex)
        {
            IType[] types;
            bool[,] homArray;
            bool[] homToAllArray;
            SearchPlanNode elem1 = (SearchPlanNode) op1.Element;
            PlanNodeType pntype = elem1.NodeType;
            if(pntype == PlanNodeType.Node)
            {
                types = model.NodeModel.Types;
                homArray = curSSP.PatternGraph.HomomorphicNodes;
                homToAllArray = curSSP.PatternGraph.HomomorphicToAllNodes;
            }
            else
            {
                types = model.EdgeModel.Types;
                homArray = curSSP.PatternGraph.HomomorphicEdges;
                homToAllArray = curSSP.PatternGraph.HomomorphicToAllEdges;
            }

            if(homToAllArray[elem1.ElementID - 1]) return;

            IType type1 = types[elem1.PatternElement.TypeID];

            for(int j = op2StartIndex; j < curSSP.Operations.Length; j++)
            {
                SearchOperation op2 = curSSP.Operations[j];
                if(op2.Type == SearchOperationType.Condition) continue;
                if(op2.Type == SearchOperationType.NegativePattern) continue;

                SearchPlanNode elem2 = (SearchPlanNode) op2.Element;
                if(elem2.NodeType != pntype) continue;          // aren't both elements edges or both nodes?

                if(homToAllArray[elem2.ElementID - 1]) continue;

                if(homArray != null && homArray[elem1.ElementID - 1, elem2.ElementID - 1])
                {
                    if(op1.Isomorphy.HomomorphicID == 0) {
                        op1.Isomorphy.HomomorphicID = op2.Isomorphy.HomomorphicID = elem1.ElementID;
                    } else {
                        op2.Isomorphy.HomomorphicID = op1.Isomorphy.HomomorphicID;
                    }
                }


                // TODO: Check type constraints for optimization!!!
                IType type2 = types[elem2.PatternElement.TypeID];
                foreach(IType subtype1 in ((ITypeFramework) type1).subOrSameTypes)
                {
                    if((type2.IsA(subtype1) || subtype1.IsA(type2)) // IsA==IsSuperTypeOrSameType
                        && (homArray == null || !homArray[elem1.ElementID - 1, elem2.ElementID - 1]))
                    {
                        op1.Isomorphy.MustSetMapped = true;
                        op2.Isomorphy.MustCheckMapped = true;
                    }
                }
            }
        }

#if CSHARPCODE_VERSION2
        /// <summary>
        /// Pretty printing helper class for source code generation
        /// </summary>
        class SourceBuilder
        {
            StringBuilder builder = new StringBuilder();
            String indentation = "";

            /// <summary>
            /// If true, the source code should be generated with comments.
            /// </summary>
            public bool CommentSourceCode;

            public SourceBuilder(bool commentSourceCode)
            {
                CommentSourceCode = commentSourceCode;
            }

            public SourceBuilder Append(String str)
            {
                builder.Append(str);
                return this;
            }

            public SourceBuilder AppendFormat(String str, params object[] args)
            {
                builder.AppendFormat(str, args);
                return this;
            }

            public SourceBuilder AppendFront(String str)
            {
                builder.Append(indentation);
                builder.Append(str);
                return this;
            }

            public SourceBuilder AppendFrontFormat(String str, params object[] args)
            {
                builder.Append(indentation);
                builder.AppendFormat(str, args);
                return this;
            }

            public void Indent()
            {
                indentation += "    ";
            }

            public void Unindent()
            {
                indentation = indentation.Substring(4);
            }

            public override String ToString()
            {
                return builder.ToString();
            }
        }

        /// <summary>
        /// Code generated per operation consists of front part, cut by code for next operation, and code for tail part
        /// Operation state is necessary for generating tail part fitting front part, is remembered on the operation stack
        /// </summary>
        class OperationState
        {
            public static int labelid = 0; // to prevent problems with equally named labels 
            public SearchOperationType OperationType; // the type of the operation originating this state
            public SearchPlanNode TargetNode; // and its search plan node 
            public bool IsNode; // node/edge?
            public String CurPosVar; // name of local variable containing index into list with candidates
                                     // or name of local variable containing candidate itself
            public String ListVar; // name of local variable containing list of candidates (null if no list used)
            public String MappedVar; // name of local variable to which the graph element was mapped or null
                                     // curious way of encoding whether graph element was mapped to CurPosVar or not
            public String ContinueBeforeUnmapLabel; // label giving code position to continue execution with/before unmapping
            public bool ContinueBeforeUnmapLabelUsed; // label was used ?
            public String ContinueAfterUnmapLabel; // label giving code position to continue execution without/after unmapping
            public bool ContinueAfterUnmapLabelUsed; // label was used ?
            public SearchOperation Operation;

            public OperationState(SearchOperationType type, SearchPlanNode targetNode, String curPosVar, String listVar)
            {
                OperationType = type;
                TargetNode = targetNode;
                IsNode = targetNode != null && targetNode.NodeType == PlanNodeType.Node;
                CurPosVar = curPosVar;
                ListVar = listVar;
                ContinueBeforeUnmapLabel = "contunmap_" + curPosVar + "_" + labelid++;
                ContinueBeforeUnmapLabelUsed = false;
                ContinueAfterUnmapLabel = "cont_" + curPosVar + "_" + labelid++;
                ContinueAfterUnmapLabelUsed = false;
            }
        }

        class OpStateLookup : OperationState
        {
            public bool UsesLoop;

            public OpStateLookup(SearchPlanNode targetNode, String curPosVar, String listVar, bool usesLoop)
                : base(SearchOperationType.Lookup, targetNode, curPosVar, listVar)
            {
                UsesLoop = usesLoop;
            }
        }

        class OpStateImplicit : OperationState
        {
            public OpStateImplicit(SearchOperationType type, SearchPlanNode targetNode, String curPosVar)
                : base(type, targetNode, curPosVar, null) { }
        }

        class OpStateExtend : OperationState
        {
            public String HeadName;

            public OpStateExtend(SearchOperationType type, SearchPlanNode targetNode, String curPosVar, String listVar, String headName)
                : base(type, targetNode, curPosVar, listVar)
            {
                HeadName = headName;
            }
        }

        class OpStatePreset : OperationState
        {
            public String WasSetVar;

            public OpStatePreset(SearchPlanNode targetNode, String curPosVar, String listVar, String wasSetVar)
                : base(SearchOperationType.MaybePreset, targetNode, curPosVar, listVar)
            {
                WasSetVar = wasSetVar;
            }
        }

        class LoopStackItem
        {
            public String LabelName;
            public bool LabelUsed = false;

            public LoopStackItem(String labelName)
            {
                LabelName = labelName;
            }
        }

        /// <summary>
        /// Generates an LGSPAction object for the given scheduled search plan.
        /// </summary>
        /// <param name="action">Needed for the rule pattern and the name</param>
        /// <param name="sourceOutputFilename">null if no output file needed</param>
        public LGSPAction GenerateMatcher(ScheduledSearchPlan scheduledSearchPlan, LGSPAction action,
            String modelAssemblyLocation, String actionAssemblyLocation, String sourceOutputFilename)
        {
            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyLocation);
            compParams.ReferencedAssemblies.Add(actionAssemblyLocation);

            compParams.GenerateInMemory = true;
#if PRODUCE_UNSAFE_MATCHERS
            compParams.CompilerOptions = "/optimize /unsafe";
#else
            compParams.CompilerOptions = "/optimize";
#endif

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);

            // generate class setup
            sourceCode.Append("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + model.GetType().Namespace + ";\nusing " + action.RulePattern.GetType().Namespace + ";\n\n"
                + "namespace de.unika.ipd.grGen.lgspActions\n{\n    public class DynAction_" + action.Name + " : LGSPAction\n    {\n"
                + "        public DynAction_" + action.Name + "() { rulePattern = " + action.RulePattern.GetType().Name
                + ".Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, " + action.RulePattern.PatternGraph.Nodes.Length
                + ", " + action.RulePattern.PatternGraph.Edges.Length + "); matchesList = matches.matches; }\n"
                + "        public override string Name { get { return \"" + action.Name + "\"; } }\n"
                + "        private LGSPMatches matches;\n"
                + "        private LGSPMatchesList matchesList;\n");

            // generate the matching function
            sourceCode.Append(GenerateMatcherSourceCode(scheduledSearchPlan, action.Name, action.rulePattern));

            // close namespace   TODO: GenerateMatcherSourceCode should not close class
            sourceCode.Append("}");

            // write generated source to file if requested
            if(sourceOutputFilename != null)
            {
                StreamWriter writer = new StreamWriter(sourceOutputFilename);
                writer.Write(sourceCode.ToString());
                writer.Close();
            }
//            Stopwatch compilerWatch = Stopwatch.StartNew();

            // compile generated code
            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                throw new ArgumentException("Illegal dynamic C# source code produced for actions \"" + action.Name + "\": " + errorMsg);
            }

            // create action instance
            Object obj = compResults.CompiledAssembly.CreateInstance("de.unika.ipd.grGen.lgspActions.DynAction_" + action.Name);

//            long compSourceTicks = compilerWatch.ElapsedTicks;
//            Console.WriteLine("GenMatcher: Compile source: {0} us", compSourceTicks / (Stopwatch.Frequency / 1000000));
            return (LGSPAction) obj;
        }

        //  generated code for an operation looks like
        //  front (match)
        //  map (the mapping graph element -> pattern element saved within the graph itself (for isomorphism checks))
        //  recursive insert of code for further operations
        //beforeunmaplabel: // jump here at mismatch after mapping was written, in order to unmap
        //  unmap (undo the mapping)
        //afterunmaplabel: // jump here at mismatch before mapping was written, no unmapping necessary 
        //  tail (for closing loops)

        /// <summary>
        /// Generates the source code of the matcher function for the given scheduled search plan
        /// </summary>
        public String GenerateMatcherSourceCode(ScheduledSearchPlan scheduledSearchPlan, 
            String actionName, LGSPRulePattern rulePattern)
        {
            if(DumpSearchPlan)
                DumpScheduledSearchPlan(scheduledSearchPlan, actionName);

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            sourceCode.Indent();
            sourceCode.Indent();

#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

#if PRODUCE_UNSAFE_MATCHERS
            sourceCode.AppendFront("unsafe public Matches myMatch(BaseGraph graph, int maxMatches, IGraphElement[] parameters)\n");
#else
            sourceCode.AppendFront("public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)\n");
#endif
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

/*            // [0] Matches matches = new Matches(this);

            sourceCode.AppendFront("LGSPMatches matches = new LGSPMatches(this);\n");*/
            sourceCode.AppendFront("matches.matches.Clear();\n");

            LinkedList<OperationState> operationStack = new LinkedList<OperationState>();

            OperationState dummyOp = new OperationState((SearchOperationType) (-1), null, "", null);
            dummyOp.ContinueBeforeUnmapLabel = "returnLabel";
            operationStack.AddFirst(dummyOp);

            // iterate over all operations in scheduled search plan
            // for every operation emit front of code and remember tail as operation state to emit later on
            foreach(SearchOperation op in scheduledSearchPlan.Operations)
            {
                OperationState lastOpState = operationStack.First.Value;
                switch(op.Type)
                {
                    case SearchOperationType.MaybePreset:
                        operationStack.AddFirst(EmitMaybePresetFront((SearchPlanNode) op.Element, rulePattern, lastOpState, sourceCode));
                        break;

                    case SearchOperationType.Lookup:
                        operationStack.AddFirst(EmitLookupFront((SearchPlanNode) op.Element, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                        operationStack.AddFirst(EmitImplicit((SearchPlanEdgeNode) op.SourceSPNode, (SearchPlanNodeNode) op.Element,
                            op.Type, rulePattern, lastOpState, sourceCode));
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        operationStack.AddFirst(EmitExtendFront((SearchPlanNodeNode) op.SourceSPNode, (SearchPlanEdgeNode) op.Element,
                            op.Type, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.NegativePattern:
                        EmitNegativePattern((ScheduledSearchPlan) op.Element, rulePattern, lastOpState, sourceCode);
                        break;

                    case SearchOperationType.Condition:
                        EmitCondition((Condition) op.Element, rulePattern.GetType().Name, lastOpState, sourceCode);
                        break;
                }
                // mark element as visited if it was visitable (a SearchPlanNode)
                if(op.Type != SearchOperationType.NegativePattern && op.Type != SearchOperationType.Condition)
                {
                    ((SearchPlanNode) op.Element).Visited = true;
                    operationStack.First.Value.Operation = op;
                }

                // emit isomorphism checks
                if(op.Isomorphy.MustCheckMapped)
                {
#if OLDMAPPEDFIELDS
                    if(op.Isomorphy.HomomorphicID != 0)
                    {
                        sourceCode.AppendFrontFormat("if({0}.mappedTo != 0 && {0}.mappedTo != {1})",
                            operationStack.First.Value.CurPosVar, op.Isomorphy.HomomorphicID);
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("if({0}.mappedTo != 0)", operationStack.First.Value.CurPosVar);
                    }
#else
                    sourceCode.AppendFrontFormat("if({0}.isMapped", operationStack.First.Value.CurPosVar);
                    if(op.Isomorphy.HomomorphicID != 0)
                        EmitHomomorphicCheck((SearchPlanNode) op.Element, rulePattern.patternGraph, operationStack, sourceCode);
                    sourceCode.Append(")");
#endif
                    sourceCode.Append(" goto " + operationStack.First.Value.ContinueAfterUnmapLabel + ";\n");
                    operationStack.First.Value.ContinueAfterUnmapLabelUsed = true;
                }
                if(op.Isomorphy.MustSetMapped)    // can be only true, if op.Type != SearchOperationType.NegativePattern
                {                       // && op.Type != SearchOperationType.Condition
                    int mapid = op.Isomorphy.HomomorphicID;
                    if(mapid == 0) mapid = ((SearchPlanNode) op.Element).ElementID;
#if OLDMAPPEDFIELDS
                    else sourceCode.AppendFrontFormat("int {0}_prevMappedTo = {0}.mappedTo;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.mappedTo = {1};\n", operationStack.First.Value.CurPosVar, mapid);
#else
                    else sourceCode.AppendFrontFormat("bool {0}_prevIsMapped = {0}.isMapped;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.isMapped = true;\n", operationStack.First.Value.CurPosVar);
#endif
                    operationStack.First.Value.MappedVar = operationStack.First.Value.CurPosVar;
                }
            }

            // emit match object building code
            sourceCode.AppendFront("LGSPMatch match = matchesList.GetNewMatch();\n");
            PatternGraph patternGraph = (PatternGraph) rulePattern.PatternGraph;
            for(int i = 0; i < patternGraph.nodes.Length; i++)
            {
                PatternNode patNode = patternGraph.nodes[i];
                sourceCode.AppendFront("match.nodes[" + i + "] = node_cur_" + patNode.Name + ";\n");
            }
            for(int i = 0; i < patternGraph.edges.Length; i++)
            {
                PatternEdge patEdge = patternGraph.edges[i];
                sourceCode.AppendFront("match.edges[" + i + "] = edge_cur_" + patEdge.Name + ";\n");
            }
            sourceCode.AppendFront("matchesList.CommitMatch();\n");

            // emit match process aborting code
            sourceCode.AppendFront("if(maxMatches > 0 && matchesList.Count >= maxMatches)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit unmapping code for abort case
            foreach(OperationState opState in operationStack)
            {
                if(opState.MappedVar != null)
                {
#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.mappedTo = {0}_prevMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.mappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isMapped = {0}_prevIsMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isMapped = false;\n", opState.MappedVar);
#endif
                }
            }

#if !NO_ADJUST_LIST_HEADS
            // emit code to adjust list heads for abort case
            // (set list entry point to element after last element found)
            // TODO: avoid adjusting list heads twice for lookups of same type
            for(LinkedListNode<OperationState> opStackNode = operationStack.Last; opStackNode != null; opStackNode = opStackNode.Previous)
            {
                OperationState opState = opStackNode.Value;
                if(opState.ListVar != null && opState.OperationType != SearchOperationType.MaybePreset)
                {
                    if(opState.OperationType == SearchOperationType.Incoming)
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n", opState.ListVar, opState.CurPosVar);
                    else if(opState.OperationType == SearchOperationType.Outgoing)
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n", opState.ListVar, opState.CurPosVar);
                    else
                        sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n", opState.CurPosVar);
//                        sourceCode.AppendFrontFormat("{0}.MoveHeadAfter({1});\n", opState.ListVar, opState.CurPosVar);
                }
            }
#endif
            sourceCode.AppendFront("return matches;\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // emit unmapping code and tail of operation for operations remembered
            foreach(OperationState opState in operationStack)
            {
                if(opState.ContinueBeforeUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueBeforeUnmapLabel);
                if(opState.MappedVar != null)
                {
#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.mappedTo = {0}_prevMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.mappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isMapped = {0}_prevIsMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isMapped = false;\n", opState.MappedVar);
#endif
                }
                if(opState.ContinueAfterUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueAfterUnmapLabel);
                switch(opState.OperationType)
                {
                    case SearchOperationType.MaybePreset:
                        EmitMaybePresetTail(opState, sourceCode);
                        break;
                    case SearchOperationType.Lookup:
                        EmitLookupTail(opState, sourceCode);
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        EmitExtendTail(opState, sourceCode);
                        break;
                }
            }

            sourceCode.AppendFront("return matches;\n        }\n    }\n");

//            int compileGenMatcher = Environment.TickCount;
//            long genSourceTicks = startGenMatcher.ElapsedTicks;

            return sourceCode.ToString();
        }

        private void EmitHomomorphicCheck(SearchPlanNode spnode, PatternGraph patternGraph, LinkedList<OperationState> opStack, SourceBuilder sourceCode)
        {
            sourceCode.Append(" && (false");

            bool[,] homArray;
            IType[] types;
            if(spnode.NodeType == PlanNodeType.Node)
            {
                homArray = patternGraph.homomorphicNodes;
                types = model.NodeModel.Types;
            }
            else if(spnode.NodeType == PlanNodeType.Edge)
            {
                homArray = patternGraph.homomorphicEdges;
                types = model.EdgeModel.Types;
            }
            else
            {
                throw new ArgumentException("Illegal search plan node specified to EmitHomomorphicCheck!");
            }

            String curElemName = opStack.First.Value.CurPosVar;
            IType type1 = types[spnode.PatternElement.TypeID];

            foreach(OperationState opState in opStack)
            {
                if(opState.OperationType == SearchOperationType.NegativePattern
                    || opState.OperationType == SearchOperationType.Condition) continue;

                SearchPlanNode otherNode = opState.TargetNode;
                if(otherNode.NodeType != spnode.NodeType) continue;
                if(homArray != null && homArray[spnode.ElementID - 1, otherNode.ElementID - 1]) continue;

                // TODO: Check type constraints!!!
                IType type2 = types[otherNode.PatternElement.TypeID];
                foreach(IType subtype1 in ((ITypeFramework) type1).subOrSameTypes)
                {
                    if((type2.IsA(subtype1) || subtype1.IsA(type2)))        // IsA==IsSuperTypeOrSameType
                    {
                        sourceCode.Append(" || " + opState.CurPosVar + " == " + curElemName);
                        break;
                    }
                }
            }
            sourceCode.Append(")");
        }

        /// <summary>
        /// Emits code for front part of lookup operation
        /// </summary>
        private OperationState EmitLookupFront(SearchPlanNode target, LGSPRulePattern rulePattern, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Lookup(" + target.PatternElement.Name + ":"
                        + (target.NodeType == PlanNodeType.Node ? model.NodeModel : model.EdgeModel).Types[target.PatternElement.TypeID].Name + ")\n");

            String elemName = target.PatternElement.Name;
            bool isNode = (target.NodeType == PlanNodeType.Node);
            ITypeModel typeModel = isNode ? model.NodeModel : model.EdgeModel;
            String elemTypeName = isNode ? "LGSPNode" : "LGSPEdge";
            String nodeEdgeString = isNode ? "node" : "edge";
            String typeName = nodeEdgeString + "_type_" + elemName;
            String listName = nodeEdgeString + "_list_" + elemName;
            String headName = nodeEdgeString + "_head_" + elemName;
            String curName = nodeEdgeString + "_cur_" + elemName;

            // find out which types are to be examined for the target element to emit the lookup for
            String typeIDStr;
            bool usesLoop;
            if(target.PatternElement.AllowedTypes == null) 
            {
                // AllowedTypes == null -> all subtypes or the same type are allowed

                // need loop if there are subtypes
                usesLoop = typeModel.Types[target.PatternElement.TypeID].SubTypes.GetEnumerator().MoveNext();

                if(usesLoop)
                {
                    // foreach(ITypeFramework type in <element type>.typeVar.SubOrSameTypes)

                    sourceCode.AppendFrontFormat("foreach(ITypeFramework {0} in {1}.typeVar.SubOrSameTypes)\n",
                        typeName, typeModel.TypeTypes[target.PatternElement.TypeID].Name);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    typeIDStr = typeName + ".typeID";
                }
                else typeIDStr = target.PatternElement.TypeID.ToString();
            }
            else
            {
                // AllowedTypes != null -> exactly the allowed types are given

                if(target.PatternElement.AllowedTypes.Length == 0)
                {
                    // curious form of assert(false)
                    sourceCode.AppendFront("while(false)\n");
                    sourceCode.AppendFront("{\n");
                    typeIDStr = target.PatternElement.TypeID.ToString();    // dummy name, will not be used at runtime anyway
                    usesLoop = true;
                }
                else if(target.PatternElement.AllowedTypes.Length == 1)
                {
                    typeIDStr = target.PatternElement.AllowedTypes[0].TypeID.ToString();
                    usesLoop = false;
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(ITypeFramework {0} in {1})\n",
                        typeName, rulePattern.GetType().Name + "." + elemName + "_AllowedTypes");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    typeIDStr = typeName + ".typeID";
                    usesLoop = true;
                }
            }

/*            // LinkedGrList<LGSPNode/LGSPEdge> list = lgraph.nodes/edges[<<typeIDStr>>];

            sourceCode.AppendFrontFormat("LinkedGrList<{0}> {1} = lgraph.{2}s[{3}];\n",
                elemTypeName, listName, nodeEdgeString, typeIDStr);

#if RANDOM_LOOKUP_LIST_START
            String randomName = nodeEdgeString + "_random_" + elemName;
            sourceCode.AppendFront("int " + randomName + " = random.Next(" + listName + ".Count);\n");
            sourceCode.AppendFront(elemTypeName + " xx" + headName + " = " + listName + ".head;\n");
            sourceCode.AppendFront("for(int i = 0; i < " + randomName + "; i++) xx" + headName + " = xx" + headName + ".typeNext;\n");
            sourceCode.AppendFront(listName + ".MoveHeadAfter(xx" + headName + ");\n");
#endif

            // for(<LGSPNode/LGSPEdge> head = list.head, curpos = head.typeNext; curpos != head; curpos = curpos.typeNext)

            sourceCode.AppendFrontFormat("for({0} {1} = {2}.head, {3} = {1}.typeNext; {3} != {1}; {3} = {3}.typeNext)\n",
                elemTypeName, headName, listName, curName);*/

#if RANDOM_LOOKUP_LIST_START
            String randomName = nodeEdgeString + "_random_" + elemName;
            sourceCode.AppendFront("int " + randomName + " = random.Next(graph." + nodeEdgeString + "sByTypeCounts[" + typeIDStr + "]);\n");
            sourceCode.AppendFront(elemTypeName + " xx" + headName + " = graph." + nodeEdgeString + "sByTypeHeads[" + typeIDStr + "];\n");
            sourceCode.AppendFront("for(int i = 0; i < " + randomName + "; i++) xx" + headName + " = xx" + headName + ".typeNext;\n");
            sourceCode.AppendFront(listName + ".MoveHeadAfter(xx" + headName + ");\n");
#endif

            // leads to sth. like "for(<LGSPNode/LGSPEdge> head = lgraph.<<nodes/edges>>ByTypeHeads[<<typeIDStr>>], curpos = head.typeNext; curpos != head; curpos = curpos.typeNext)"
            sourceCode.AppendFrontFormat("for({0} {1} = graph.{2}sByTypeHeads[{3}], {4} = {1}.typeNext; {4} != {1}; {4} = {4}.typeNext)\n",
                elemTypeName, headName, nodeEdgeString, typeIDStr, curName);

            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(!isNode)
            {
                // some edge found by lookup, now emit: 
                // check whether source/target-nodes of the edge are the same as
                // the already found nodes to which the edge must be incident
                // don't need to if the edge is not required by the pattern to be incident to some given node
                // or that node is not matched by now (signaled by visited)
                SearchPlanEdgeNode edge = (SearchPlanEdgeNode) target;
                if(edge.PatternEdgeSource != null && edge.PatternEdgeSource.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.source != node_cur_{1}) continue;\n", curName,
                        edge.PatternEdgeSource.PatternElement.Name);
                }
                if(edge.PatternEdgeTarget != null && edge.PatternEdgeTarget.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.target != node_cur_{1}) continue;\n", curName,
                        edge.PatternEdgeTarget.PatternElement.Name);
                }
            }
            else //isNode
            {
                // some node found by lookup, now emit: 
                // check whether incoming/outgoing-edges of the node are the same as
                // the already found edges to which the node must be incident
                // only for the edges required by the pattern to be incident with the node
                // only if edge is already matched by now (signaled by visited)
                SearchPlanNodeNode node = (SearchPlanNodeNode) target;
                foreach(SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.source != {1}) continue;\n", edge.PatternElement.Name, curName);
                    }
                }
                foreach(SearchPlanEdgeNode edge in node.IncomingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.target != {1}) continue;\n", edge.PatternElement.Name, curName);
                    }
                }
            }

            return new OpStateLookup(target, curName, listName, usesLoop);
        }

        /// <summary>
        /// Emits code for tail part of lookup operation
        /// </summary>
        private void EmitLookupTail(OperationState state, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Tail of Lookup(" + state.CurPosVar + ")\n");

            sourceCode.Unindent();    
            sourceCode.AppendFront("}\n");
            if(((OpStateLookup) state).UsesLoop)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        /// <summary>
        /// Emits code for implicit source operation or implicit target operation on given edge
        /// </summary>
        private OperationState EmitImplicit(SearchPlanEdgeNode source, SearchPlanNodeNode target, SearchOperationType opType,
            LGSPRulePattern rulePattern, OperationState lastOpState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Implicit" + ((opType == SearchOperationType.ImplicitSource) ? "Source" : "Target")
                        + "(" + source.PatternElement.Name + " -> " + target.PatternElement.Name + ":"
                        + (target.NodeType == PlanNodeType.Node ? model.NodeModel : model.EdgeModel).Types[target.PatternElement.TypeID].Name
                        + ")\n");

            // curpos = edge.<source/target>

            sourceCode.AppendFrontFormat("LGSPNode node_cur_{0} = edge_cur_{1}.{2};\n", target.PatternElement.Name,
                source.PatternElement.Name, (opType == SearchOperationType.ImplicitSource) ? "source" : "target");

            if(target.PatternElement.IsAllowedType != null)         // some types allowed
            {
                sourceCode.AppendFrontFormat("if(!{0}_IsAllowedType[node_cur_{1}.type.typeID]) goto {2};\n",
                    rulePattern.GetType().Name + "." + target.PatternElement.Name,
                    target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
            }
            else if(target.PatternElement.AllowedTypes == null)     // all subtypes of pattern element type allowed
            {
                if(model.NodeModel.Types[target.PatternElement.TypeID] != model.NodeModel.RootType)
#if USE_INSTANCEOF_FOR_TYPECHECKS
                    sourceCode.AppendFrontFormat("if(!(node_cur_{0}.attributes is I{1})) goto {2};\n",
                        target.PatternElement.Name,
                        ((ITypeFramework) model.NodeModel.Types[target.PatternElement.TypeID]).AttributesType.Name,
                        lastOpState.ContinueBeforeUnmapLabel);
#else
                    sourceCode.AppendFrontFormat("if(!{0}.isMyType[node_cur_{1}.type.typeID]) goto {2};\n",
                        model.NodeModel.TypeTypes[target.PatternElement.TypeID].Name,
                        target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
#endif
            }
            else if(target.PatternElement.AllowedTypes.Length == 0) // no type allowed
            {
                sourceCode.AppendFront("goto ");
                sourceCode.Append(lastOpState.ContinueBeforeUnmapLabel);
                sourceCode.Append(";\n");
            }
            else // target.PatternElement.AllowedTypes.Length == 1  // only one type allowed
            {
                sourceCode.AppendFrontFormat("if(node_cur_{0}.type.typeID != {1}) goto {2};\n",
                    target.PatternElement.Name, target.PatternElement.AllowedTypes[0].TypeID, lastOpState.ContinueBeforeUnmapLabel);
            }

            lastOpState.ContinueBeforeUnmapLabelUsed = true;

            foreach (SearchPlanEdgeNode edge in target.OutgoingPatternEdges)
            {
                if(edge == source && opType == SearchOperationType.ImplicitSource) continue;
                if(edge.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if(edge_cur_{0}.source != node_cur_{1}) goto {2};\n",
                        edge.PatternElement.Name, target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
                }
            }
            foreach (SearchPlanEdgeNode edge in target.IncomingPatternEdges)
            {
                if(edge == source && opType == SearchOperationType.ImplicitTarget) continue;
                if(edge.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if(edge_cur_{0}.target != node_cur_{1}) goto {2};\n",
                        edge.PatternElement.Name, target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
                }
            }
            return new OpStateImplicit(opType, target, "node_cur_" + target.PatternElement.Name);
        }

        /// <summary>
        /// Emits code for front part of extend incoming operation or extend outgoing operation on given node
        /// </summary>
        private OperationState EmitExtendFront(SearchPlanNodeNode source, SearchPlanEdgeNode target, SearchOperationType opType,
            LGSPRulePattern rulePattern, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Extend" + (opType == SearchOperationType.Incoming ? "Incoming" : "Outgoing")
                        + "(" + source.PatternElement.Name + " -> " + target.PatternElement.Name + ":"
                        + (target.NodeType == PlanNodeType.Node ? model.NodeModel : model.EdgeModel).Types[target.PatternElement.TypeID].Name
                        + ")\n");

            bool isIncoming = opType == SearchOperationType.Incoming;
            String listStr = isIncoming ? "inhead" : "outhead";
            String nodeStr = isIncoming ? "inNode" : "outNode";

            String sourceName = "node_cur_" + source.PatternElement.Name;
            String headName = "edge_head_" + target.PatternElement.Name;
            String curName = "edge_cur_" + target.PatternElement.Name;

#if RANDOM_LOOKUP_LIST_START
            String randomName = "edge_random_" + target.PatternElement.Name;
            sourceCode.AppendFront("int " + randomName + " = random.Next(" + listName + ".Count);\n");
            sourceCode.AppendFront("LGSPEdge xx" + headName + " = " + listName + ".head;\n");
            sourceCode.AppendFront("for(int i = 0; i < " + randomName + "; i++) xx" + headName + " = xx" + headName + "." + nodeStr +".typeNext;\n");
            if(isIncoming)
                sourceCode.AppendFront(sourceName + ".MoveInHeadAfter(xx" + headName + ");\n");
            else
                sourceCode.AppendFront(sourceName + ".MoveOutHeadAfter(xx" + headName + ");\n");
#endif

            sourceCode.AppendFront("LGSPEdge " + headName + " = " + sourceName + "." + listStr + ";\n");
            sourceCode.AppendFront("if(" + headName + " != null)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("LGSPEdge " + curName + " = " + headName + ";\n");
            sourceCode.AppendFront("do\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(target.PatternElement.IsAllowedType != null)         // some types allowed
            {
                sourceCode.AppendFrontFormat("if(!{0}_IsAllowedType[{1}.type.typeID]) continue;\n",
                    rulePattern.GetType().Name + "." + target.PatternElement.Name, curName);
            }
            else if(target.PatternElement.AllowedTypes == null)     // all subtypes of pattern element type allowed
            {
#if USE_INSTANCEOF_FOR_TYPECHECKS
                sourceCode.AppendFrontFormat("if(!({0}.attributes is I{1})) continue;\n",
                    curName, ((ITypeFramework)model.EdgeModel.Types[target.PatternElement.TypeID]).AttributesType.Name);
#else
                if(model.EdgeModel.Types[target.PatternElement.TypeID] != model.EdgeModel.RootType)
                    sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.type.typeID]) continue;\n",
                        model.EdgeModel.TypeTypes[target.PatternElement.TypeID].Name, curName);
#endif
            }
            else if(target.PatternElement.AllowedTypes.Length == 0) // no type allowed
            {
                sourceCode.AppendFront("continue\n");
            }
            else // target.PatternElement.AllowedTypes.Length == 1  // only one type allowed
            {
                sourceCode.AppendFrontFormat("if({0}.type.typeID != {1}) continue;\n",
                    curName, target.PatternElement.AllowedTypes[0].TypeID);
            }

            if(isIncoming)
            {
                if(target.PatternEdgeSource != null && target.PatternEdgeSource.Visited)    // check op?        
                {
                    sourceCode.AppendFrontFormat("if({0}.source != node_cur_{1}) continue;\n", curName,
                        target.PatternEdgeSource.PatternElement.Name);
                }
            }
            else // outgoing
            {
                if(target.PatternEdgeTarget != null && target.PatternEdgeTarget.Visited)    // check op?       
                {
                    sourceCode.AppendFrontFormat("if({0}.target != node_cur_{1}) continue;\n", curName, 
                        target.PatternEdgeTarget.PatternElement.Name);
                }
            }

            return new OpStateExtend(opType, target, curName, sourceName, headName);
        }

        /// <summary>
        /// Emits code for tail part of extend operation
        /// </summary>
        private void EmitExtendTail(OperationState loop, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Tail Extend" + (loop.OperationType == SearchOperationType.Incoming ? "Incoming" : "Outgoing")
                        + "(" + loop.CurPosVar + ")\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("while((" + loop.CurPosVar + " = " + loop.CurPosVar + "."
                + (loop.OperationType == SearchOperationType.Incoming ? "inNext" : "outNext") + ") != "
                + ((OpStateExtend) loop).HeadName + ");\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        /// <summary>
        /// Emits code for front part of preset operation
        /// </summary>
        private OperationState EmitMaybePresetFront(SearchPlanNode target, LGSPRulePattern rulePattern, OperationState lastOpState,
            SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("// Preset(" + target.PatternElement.Name + ":"
                    + (target.NodeType == PlanNodeType.Node ? model.NodeModel : model.EdgeModel).Types[target.PatternElement.TypeID].Name
                    + ")\n");

            String curPosVar;
            String elemType;
            String typeListVar;
            String typeIterVar;
            String graphList;
//            String listVar;
            String headVar;
            String wasSetVar;
            ITypeModel typeModel;
            bool isNode = target.NodeType == PlanNodeType.Node;

            if(isNode)
            {
                curPosVar = "node_cur_" + target.PatternElement.Name;
                elemType = "LGSPNode";
                typeListVar = "node_typelist_" + target.PatternElement.Name;
                typeIterVar = "node_typeiter_" + target.PatternElement.Name;
                graphList = "nodes";
//                listVar = "node_list_" + target.PatternElement.Name;
                headVar = "node_listhead_" + target.PatternElement.Name;
                wasSetVar = "node_parWasSet_" + target.PatternElement.Name;
                typeModel = model.NodeModel;
            }
            else // edge
            {
                curPosVar = "edge_cur_" + target.PatternElement.Name;
                elemType = "LGSPEdge";
                typeListVar = "edge_typelist_" + target.PatternElement.Name;
                typeIterVar = "edge_typeiter_" + target.PatternElement.Name;
                graphList = "edges";
//                listVar = "edge_list_" + target.PatternElement.Name;
                headVar = "node_listhead_" + target.PatternElement.Name;
                wasSetVar = "edge_parWasSet_" + target.PatternElement.Name;
                typeModel = model.EdgeModel;
            }

            sourceCode.AppendFrontFormat("ITypeFramework[] {0} = null;\n", typeListVar);
            sourceCode.AppendFrontFormat("int {0} = 0;\n", typeIterVar);
//            sourceCode.AppendFrontFormat("LinkedGrList<{0}> {1} = null;\n", elemType, listVar);
            sourceCode.AppendFrontFormat("{0} {1} = null;\n", elemType, headVar);
            sourceCode.AppendFrontFormat("bool {0};\n", wasSetVar);
            sourceCode.AppendFrontFormat("{0} {1} = ({0}) parameters[{2}];\n", elemType, curPosVar, target.PatternElement.ParameterIndex);
            sourceCode.AppendFrontFormat("if({0} != null)\n", curPosVar);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFrontFormat("{0} = true;\n", wasSetVar);
            if(target.PatternElement.IsAllowedType != null)         // some types allowed
            {
                sourceCode.AppendFrontFormat("if(!{0}_IsAllowedType[{1}.type.typeID]) goto {2};\n",
                    rulePattern.GetType().Name + "." + target.PatternElement.Name,
                    curPosVar, lastOpState.ContinueBeforeUnmapLabel);
            }
            else if(target.PatternElement.AllowedTypes == null)     // all subtypes of pattern element type allowed
            {
                if(typeModel.Types[target.PatternElement.TypeID] != typeModel.RootType)
                    sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.type.typeID]) goto {2};\n",
                        typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                        curPosVar, lastOpState.ContinueBeforeUnmapLabel);
            }
            else if(target.PatternElement.AllowedTypes.Length == 0) // no type allowed
            {
                sourceCode.AppendFront("goto ");
                sourceCode.Append(lastOpState.ContinueBeforeUnmapLabel);
                sourceCode.Append(";\n");
            }
            else // target.PatternElement.AllowedTypes.Length == 1  // only one type allowed
            {
                sourceCode.AppendFrontFormat("if({0}.type.typeID != {1}) goto {2};\n",
                    curPosVar, target.PatternElement.AllowedTypes[0].TypeID, lastOpState.ContinueBeforeUnmapLabel);
            }
            lastOpState.ContinueBeforeUnmapLabelUsed = true;
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("else\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFrontFormat("{0} = false;\n", wasSetVar);
            if(target.PatternElement.AllowedTypes == null)
            {
                sourceCode.AppendFrontFormat("{0} = {1}.typeVar.subOrSameTypes;\n", typeListVar,
                    typeModel.TypeTypes[target.PatternElement.TypeID].Name);
            }
            else if(target.PatternElement.AllowedTypes.Length == 0)
            {
                sourceCode.AppendFront("goto ");
                sourceCode.Append(lastOpState.ContinueBeforeUnmapLabel);
                sourceCode.Append(";\n");
            }
            else
            {
                sourceCode.AppendFront(typeListVar + " = " + rulePattern.GetType().Name + "." + target.PatternElement.Name + "_AllowedTypes;\n");
            }

//            sourceCode.AppendFrontFormat("{0} = lgraph.{1}[{2}[{3}].typeID];\n", listVar, graphList, typeListVar, typeIterVar);
//            sourceCode.AppendFrontFormat("{0} = {1}.head.typeNext;\n", curPosVar, listVar);
            sourceCode.AppendFrontFormat("{0} = graph.{1}ByTypeHeads[{2}[{3}].typeID];\n", headVar, graphList, typeListVar, typeIterVar);
            sourceCode.AppendFrontFormat("{0} = {1}.typeNext;\n", curPosVar, headVar);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("while(true)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // Iterated through all elements of current type?
//            sourceCode.AppendFrontFormat("while(!{0} && {1} == {2}.head)\n", wasSetVar, curPosVar, listVar);
            sourceCode.AppendFrontFormat("while(!{0} && {1} == {2})\n", wasSetVar, curPosVar, headVar);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // Increment type iteration index
            sourceCode.AppendFront(typeIterVar + "++;\n");

            // If all compatible types checked, preset operation is finished
            sourceCode.AppendFrontFormat("if({0} >= {1}.Length) goto {2};\n", typeIterVar, typeListVar, lastOpState.ContinueBeforeUnmapLabel);

            // Initialize next lookup iteration
//            sourceCode.AppendFrontFormat("{0} = lgraph.{1}[{2}[{3}].TypeID];\n", listVar,
//                isNode ? "nodes" : "edges", typeListVar, typeIterVar);
//            sourceCode.AppendFrontFormat("{0} = {1}.head.typeNext;\n", curPosVar, listVar);
            sourceCode.AppendFrontFormat("{0} = graph.{1}ByTypeHeads[{2}[{3}].typeID];\n", headVar, graphList, typeListVar, typeIterVar);
            sourceCode.AppendFrontFormat("{0} = {1}.typeNext;\n", curPosVar, headVar);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            if(target.NodeType != PlanNodeType.Node)
            {
                SearchPlanEdgeNode edge = (SearchPlanEdgeNode) target;
                if(edge.PatternEdgeSource != null && edge.PatternEdgeSource.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.source != node_cur_{1}) continue;\n", curPosVar,
                        edge.PatternEdgeSource.PatternElement.Name);
                }
                if(edge.PatternEdgeTarget != null && edge.PatternEdgeTarget.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.target != node_cur_{1}) continue;\n", curPosVar,
                        edge.PatternEdgeTarget.PatternElement.Name);
                }
            }
            else
            {
                SearchPlanNodeNode node = (SearchPlanNodeNode) target;
                foreach(SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.source != {1}) continue;\n",
                            edge.PatternElement.Name, curPosVar);
                    }
                }
                foreach(SearchPlanEdgeNode edge in node.IncomingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.target != {1}) continue;\n",
                            edge.PatternElement.Name, curPosVar);
                    }
                }
            }

//            return new OpStatePreset(isNode, curPosVar, listVar, wasSetVar);
            return new OpStatePreset(target, curPosVar, null, wasSetVar);
        }

        /// <summary>
        /// Emits code for tail part of preset operation
        /// </summary>
        private void EmitMaybePresetTail(OperationState opState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Tail Preset(" + opState.CurPosVar + ")\n");

            OpStatePreset state = (OpStatePreset) opState;

            // If preset was not undefined, break out of preset while loop
            sourceCode.AppendFrontFormat("if({0}) break;\n", state.WasSetVar);

            // Lookup next element
            sourceCode.AppendFrontFormat("{0} = {0}.typeNext;\n", state.CurPosVar);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        /// <summary>
        /// Emits code for negative pattern
        /// </summary>
        private void EmitNegativePattern(ScheduledSearchPlan negSSP, LGSPRulePattern rulePattern, OperationState lastOpState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// NegativePattern\n");

            LinkedList<OperationState> operationStack = new LinkedList<OperationState>();
            operationStack.AddFirst(lastOpState);
            OperationState dummyOp = new OperationState((SearchOperationType) (-1), null, "", null);
            operationStack.AddFirst(dummyOp);
            foreach(SearchOperation op in negSSP.Operations)
            {
                OperationState lastNegOpState = operationStack.First.Value;
                switch(op.Type)
                {
                    case SearchOperationType.Lookup:
                        operationStack.AddFirst(EmitLookupFront((SearchPlanNode) op.Element, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                        operationStack.AddFirst(EmitImplicit((SearchPlanEdgeNode) op.SourceSPNode, (SearchPlanNodeNode) op.Element,
                            op.Type, rulePattern, lastNegOpState, sourceCode));
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        operationStack.AddFirst(EmitExtendFront((SearchPlanNodeNode) op.SourceSPNode, (SearchPlanEdgeNode) op.Element,
                            op.Type, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.NegPreset:
                    {
                        SearchPlanNode node = (SearchPlanNode) op.Element;
                        String curPos = ((node.NodeType == PlanNodeType.Node) ? "node_cur_" : "edge_cur_") + node.PatternElement.Name;
                        operationStack.AddFirst(new OperationState(SearchOperationType.NegPreset, node, curPos, null));
                        break;
                    }

                    case SearchOperationType.Condition:
                        EmitCondition((Condition) op.Element, rulePattern.GetType().Name, lastNegOpState, sourceCode);
                        break;

                    case SearchOperationType.MaybePreset:
                        throw new Exception("Preset operation in negative searchplan!");
                    case SearchOperationType.NegativePattern:
                        throw new Exception("Negative pattern in negative searchplan!");
                }
                if(op.Type != SearchOperationType.Condition)
                {
                    ((SearchPlanNode) op.Element).Visited = true;
                    operationStack.First.Value.Operation = op;
                }

                if(op.Isomorphy.MustCheckMapped)
                {
#if OLDMAPPEDFIELDS
                    if(op.Isomorphy.HomomorphicID != 0)
                    {
                        sourceCode.AppendFrontFormat("if({0}.negMappedTo != 0 && {0}.negMappedTo != {1})",
                            operationStack.First.Value.CurPosVar, op.Isomorphy.HomomorphicID);
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("if({0}.negMappedTo != 0)",
                            operationStack.First.Value.CurPosVar);
                    }
#else
                    sourceCode.AppendFrontFormat("if({0}.isNegMapped", operationStack.First.Value.CurPosVar);
                    if(op.Isomorphy.HomomorphicID != 0)
                        EmitHomomorphicCheck((SearchPlanNode) op.Element, rulePattern.patternGraph, operationStack, sourceCode);
                    sourceCode.Append(")");
#endif
                    sourceCode.Append(" goto " + operationStack.First.Value.ContinueAfterUnmapLabel + ";\n");
                    operationStack.First.Value.ContinueAfterUnmapLabelUsed = true;
                }
                if(op.Isomorphy.MustSetMapped)    // can be only true, if op.Type != SearchOperationType.NegativePattern
                {                       // && op.Type != SearchOperationType.Condition
                    int mapid = op.Isomorphy.HomomorphicID;
                    if(mapid == 0) mapid = ((SearchPlanNode) op.Element).ElementID;
#if OLDMAPPEDFIELDS
                    else sourceCode.AppendFrontFormat("int {0}_prevNegMappedTo = {0}.negMappedTo;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.negMappedTo = {1};\n", operationStack.First.Value.CurPosVar, mapid);
#else
                    else sourceCode.AppendFrontFormat("bool {0}_prevIsNegMapped = {0}.isNegMapped;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.isNegMapped = true;\n", operationStack.First.Value.CurPosVar);
#endif
                    operationStack.First.Value.MappedVar = operationStack.First.Value.CurPosVar;
                }
            }

            //
            // Here the the negative pattern has been found
            //

            // Unmap mapped elements
            foreach(OperationState opState in operationStack)
            {
                if(opState == lastOpState) continue;
                if(opState.MappedVar != null)
                {
                    if(opState.Operation == null || opState.Operation.Isomorphy == null)
                        Console.WriteLine("ARGH!");

#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = {0}_prevNegMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = {0}_prevIsNegMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = false;\n", opState.MappedVar);
#endif
                }
            }

            sourceCode.AppendFrontFormat("goto {0};\n", lastOpState.ContinueBeforeUnmapLabel);
            lastOpState.ContinueBeforeUnmapLabelUsed = true;

            // close all loops

            foreach(OperationState opState in operationStack)
            {
                if(opState == lastOpState) continue;
                if(opState.ContinueBeforeUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueBeforeUnmapLabel);
                if(opState.MappedVar != null)
                {
#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = {0}_prevNegMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = {0}_prevIsNegMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = false;\n", opState.MappedVar);
#endif
                }
                if(opState.ContinueAfterUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueAfterUnmapLabel);
                switch(opState.OperationType)
                {
                    case SearchOperationType.Lookup:
                        EmitLookupTail(opState, sourceCode);
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        EmitExtendTail(opState, sourceCode);
                        break;
                }
            }
            if(CommentSourceCode)
                sourceCode.AppendFront("// End of NegativePattern\n");
        }

        /// <summary>
        /// Emits code for condition
        /// </summary>
        private void EmitCondition(Condition condition, String rulePatternTypeName, OperationState lastOpState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Condition[" + condition.ID + "]\n");

            sourceCode.AppendFrontFormat("if(!{0}.Condition_{1}(", rulePatternTypeName, condition.ID);
            bool first = true;
            foreach(String neededNode in condition.NeededNodes)
            {
                if(!first) sourceCode.Append(", ");
                else first = false;
                sourceCode.Append("node_cur_" + neededNode);
            }
            foreach(String neededEdge in condition.NeededEdges)
            {
                if(!first) sourceCode.Append(", ");
                else first = false;
                sourceCode.Append("edge_cur_" + neededEdge);
            }

            sourceCode.AppendFormat(")) goto {0};\n", lastOpState.ContinueBeforeUnmapLabel);
            lastOpState.ContinueBeforeUnmapLabelUsed = true;
        }
#else
        class ListState
        {
            public LocalBuilder listVar;
            public LocalBuilder listHeadVar;
            public LocalBuilder lastCurPosVar;
        }

        enum LoopType { Lookup, Incoming, Outgoing };

        class LoopState
        {
            public LoopType loopType;
            public LocalBuilder curPos;
            public LocalBuilder checkVar;
            public Label loopLabel;
            public Label continueLabel;
            public Label checkCondLabel;
            public bool isNode;
            public String elemName;
        }

        private void GenerateMatcher(SearchPlanGraph searchPlanGraph, LGSPAction action)
        {
            Type[] methodArgs = { typeof(LGSPAction), typeof(BaseGraph), typeof(int) };
            DynamicMethod matcher = new DynamicMethod(String.Format("Dynamic matcher for action {0}", action.Name),
                typeof(Matches), methodArgs, typeof(LGSPAction));

            ILGenerator ilMatcher = matcher.GetILGenerator();

            /* Arguments:
             *  [0] this (LGSPAction)
             *  [1] BaseGraph graph
             *  [2] int maxMatches
             */

            //ilMatcher.EmitWriteLine("Entered matcher");

            // [0] Matches matches = new Matches(this);

            ilMatcher.DeclareLocal(typeof(Matches));
            ilMatcher.Emit(OpCodes.Ldarg_0);
            ilMatcher.Emit(OpCodes.Newobj, typeof(Matches).GetConstructor(new Type[1] { typeof(IAction) }));
            ilMatcher.Emit(OpCodes.Stloc_0);

            // [1] LGSPGraph lgraph = (LGSPGraph) graph;

            ilMatcher.DeclareLocal(typeof(LGSPGraph));
            ilMatcher.Emit(OpCodes.Ldarg_1);
            ilMatcher.Emit(OpCodes.Castclass, typeof(LGSPGraph));
            ilMatcher.Emit(OpCodes.Stloc_1);

            // Build ListState objects to manage the lists
            // (this implementation expects node and elem names in patterns to be distinct)

            Dictionary<int, ListState> listTypeMap = new Dictionary<int, ListState>();
            Dictionary<string, ListState> listElemNameMap = new Dictionary<string, ListState>(searchPlanGraph.Root.OutgoingEdges.Count);
            Dictionary<string, LoopState> loopElemNameMap = new Dictionary<string, LoopState>(searchPlanGraph.Root.OutgoingEdges.Count);
            Dictionary<string, LocalBuilder> varElemNameMap = new Dictionary<string, LocalBuilder>(searchPlanGraph.Nodes.Length);

            foreach(SearchPlanEdge edge in searchPlanGraph.Edges) //searchPlanGraph.Root.OutgoingEdges)
            {
                ListState listState;
                LoopState loopState;
                LocalBuilder curPos;

                if(edge.Type == PlanEdgeType.Lookup || edge.Type == PlanEdgeType.Incoming || edge.Type == PlanEdgeType.Outgoing)
                {
                    int typeid = edge.Target.TypeID;
                    if(edge.Target.NodeType == PlanNodeType.Edge)
                        typeid += graph.Model.NodeModel.NumTypes;
                    // Only one ListState object per graph element type
                    if(!listTypeMap.TryGetValue(typeid, out listState))
                    {
                        listState = new ListState();
                        listTypeMap[typeid] = listState;

                        // TODO: If here, mappedTo must be used??
                    }
                    listElemNameMap[edge.Target.Name] = listState;
                    if(edge.Target.NodeType == PlanNodeType.Node)
                    {
                        listState.listVar = ilMatcher.DeclareLocal(typeof(LinkedGrList<LGSPNode>));
                        listState.listHeadVar = ilMatcher.DeclareLocal(typeof(LGSPNode));
                        curPos = ilMatcher.DeclareLocal(typeof(LGSPNode));
                    }
                    else
                    {
                        listState.listVar = ilMatcher.DeclareLocal(typeof(LinkedGrList<LGSPEdge>));
                        listState.listHeadVar = ilMatcher.DeclareLocal(typeof(LGSPEdge));
                        curPos = ilMatcher.DeclareLocal(typeof(LGSPEdge));
                    }
                    listState.lastCurPosVar = curPos;

                    loopState = new LoopState();
                    loopState.curPos = curPos;
                    loopState.loopLabel = ilMatcher.DefineLabel();
                    loopState.continueLabel = ilMatcher.DefineLabel();
                    loopState.checkCondLabel = ilMatcher.DefineLabel();
                    loopState.elemName = edge.Target.Name;
                    loopElemNameMap[edge.Target.Name] = loopState;
                }
                else // edge.Type == PlanEdgeType.ImplicitSource || edge.Type == PlanEdgeType.ImpliciTarget 
                {
                    curPos = ilMatcher.DeclareLocal(typeof(LGSPNode));
                }

                varElemNameMap[edge.Target.Name] = curPos;
            }

            // Free ListTypeMap as it is not used anymore
            listTypeMap = null;

            LocalBuilder nodesVar = ilMatcher.DeclareLocal(typeof(INode[]));
            LocalBuilder edgesVar = ilMatcher.DeclareLocal(typeof(IEdge[]));

            int remainingPlanNodes = searchPlanGraph.Nodes.Length;
            Dictionary<SearchPlanNode, int> NodeMap = new Dictionary<SearchPlanNode, int>(searchPlanGraph.Nodes.Length);
            LinkedList<LoopState> loopStack = new LinkedList<LoopState>();
            LinkedList<SearchPlanEdge> activeEdges = new LinkedList<SearchPlanEdge>();
            foreach(SearchPlanEdge edge in searchPlanGraph.Root.OutgoingEdges)
                activeEdges.AddLast(edge);
            searchPlanGraph.Root.Visited = true;

            bool firstextend = true;

            while(remainingPlanNodes > 0)
            {
                float minCost = float.MaxValue;
                SearchPlanEdge minEdge = null;
                for(LinkedListNode<SearchPlanEdge> llnode = activeEdges.First; llnode != null; )
                {
                    SearchPlanEdge edge = llnode.Value;
                    if(edge.Target.Visited)
                    {
                        LinkedListNode<SearchPlanEdge> oldllnode = llnode;
                        llnode = llnode.Next;
                        activeEdges.Remove(oldllnode);
                        continue;
                    }
                    else if(edge.Cost < minCost)
                    {
                        minCost = edge.Cost;
                        minEdge = edge;
                    }
                    llnode = llnode.Next;
                }
                if(minEdge == null)
                    break;
                activeEdges.Remove(minEdge);

                //
                // TODO: check op fehlt !!!!!!!!!!!!!!!!!!!!!!!!!!!
                //

                switch(minEdge.Type)
                {
                    case PlanEdgeType.Lookup:
                        Console.WriteLine("Lookup {0} \"{1}\"", (minEdge.Target.NodeType == PlanNodeType.Node) ? "node" : "edge",
                            minEdge.Target.Name);
                        loopStack.AddFirst(EmitLookupFront(minEdge, listElemNameMap, loopElemNameMap, ilMatcher));
                        break;
                    case PlanEdgeType.Incoming:
                    case PlanEdgeType.Outgoing:
                        if(!firstextend) Console.Write("* ");
                        Console.WriteLine("Extend {0} {1} \"{2}\" -> \"{3}\"",
                            (minEdge.Type == PlanEdgeType.Incoming) ? "incoming" : "outgoing",
                            (minEdge.Target.NodeType == PlanNodeType.Node) ? "node" : "edge",
                            minEdge.Source.Name, minEdge.Target.Name);
                        //if(firstextend)
                        {
                            loopStack.AddFirst(EmitExtendInOut(minEdge, listElemNameMap, loopElemNameMap, varElemNameMap, ilMatcher, firstextend));
                            firstextend = false;
                        }
                        break;
                    case PlanEdgeType.ImplicitSource:
                    case PlanEdgeType.ImplicitTarget:
                        Console.WriteLine("Implicit {0} {1} \"{2}\" -> \"{3}\"",
                            (minEdge.Type == PlanEdgeType.ImplicitSource) ? "implicit source" : "implicit target",
                            (minEdge.Target.NodeType == PlanNodeType.Node) ? "node" : "edge",
                            minEdge.Source.Name, minEdge.Target.Name);
                        EmitImplicit(minEdge, loopElemNameMap, varElemNameMap, ilMatcher);
                        break;
                }
                foreach(SearchPlanEdge edge in minEdge.Target.OutgoingEdges)
                    activeEdges.AddLast(edge);
                minEdge.Target.Visited = true;
            }

            //ilMatcher.EmitWriteLine("match found");

            // INode[] nodes = new INode[<num nodes in pattern>] { <all nodes> }

            EmitLoadConst(ilMatcher, action.RulePattern.PatternGraph.Nodes.Length);
            ilMatcher.Emit(OpCodes.Newarr, typeof(INode));
            ilMatcher.Emit(OpCodes.Stloc, nodesVar);
            int i = 0;
            foreach(PatternNode patNode in action.RulePattern.PatternGraph.Nodes)
            {
                ilMatcher.Emit(OpCodes.Ldloc, nodesVar);
                EmitLoadConst(ilMatcher, i++);
                ilMatcher.Emit(OpCodes.Ldloc, varElemNameMap[patNode.Name]);
                ilMatcher.Emit(OpCodes.Stelem_Ref);
            }

            //ilMatcher.EmitWriteLine("nodes created");

            // IEdge[] edges = new IEdge[<num edges in pattern>] { <all edges> }

            EmitLoadConst(ilMatcher, action.RulePattern.PatternGraph.Edges.Length);
            ilMatcher.Emit(OpCodes.Newarr, typeof(IEdge));
            ilMatcher.Emit(OpCodes.Stloc, edgesVar);
            i = 0;
            foreach(PatternEdge patEdge in action.RulePattern.PatternGraph.Edges)
            {
                ilMatcher.Emit(OpCodes.Ldloc, edgesVar);
                EmitLoadConst(ilMatcher, i++);
                ilMatcher.Emit(OpCodes.Ldloc, varElemNameMap[patEdge.Name]);
                ilMatcher.Emit(OpCodes.Stelem_Ref);
            }

            //ilMatcher.EmitWriteLine("edges created");

            // matches.MatchesList.AddFirst(match) (first part)

            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ldfld, typeof(Matches).GetField("MatchesList"));

            //ilMatcher.EmitWriteLine("match add initialized");

            // Match match = new Match(nodes, edges)

            ilMatcher.Emit(OpCodes.Ldloc, nodesVar);
            ilMatcher.Emit(OpCodes.Ldloc, edgesVar);
            ilMatcher.Emit(OpCodes.Newobj, typeof(Match).GetConstructor(new Type[2] { typeof(INode[]), typeof(IEdge[]) }));

            //ilMatcher.EmitWriteLine("match created");

            // matches.MatchesList.AddFirst(match) (second part)

            ilMatcher.Emit(OpCodes.Call, typeof(SingleLinkedList<Match>).GetMethod("AddFirst"));

            //ilMatcher.EmitWriteLine("match added");

            // if(maxMatches <= 0 || matches.MatchesList.length < maxMatches) continue;

            ilMatcher.Emit(OpCodes.Ldarg_2);
            ilMatcher.Emit(OpCodes.Ldc_I4_0);
            ilMatcher.Emit(OpCodes.Ble, loopStack.First.Value.continueLabel);

            //ilMatcher.EmitWriteLine("check second if condition");
            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ldfld, typeof(Matches).GetField("MatchesList"));
            ilMatcher.Emit(OpCodes.Ldfld, typeof(SingleLinkedList<Match>).GetField("length"));
            ilMatcher.Emit(OpCodes.Ldarg_2);
            //ilMatcher.EmitWriteLine("before blt");
            ilMatcher.Emit(OpCodes.Blt, loopStack.First.Value.continueLabel);

            // return matches

            //ilMatcher.EmitWriteLine("return matches");
            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ret);

            // close remaining loops

            foreach(LoopState loop in loopStack)
            {
                switch(loop.loopType)
                {
                    case LoopType.Lookup:
                        EmitLookupTail(loop, ilMatcher);
                        break;
                    case LoopType.Incoming:
                    case LoopType.Outgoing:
                        EmitExtendInOutTail(loop, ilMatcher);
                        break;
                }
            }

            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ret);

            try
            {
                MonoILReader ilReader = new MonoILReader(matcher);
                StreamWriter writer = new StreamWriter(action.Name + ".il");
                writer.WriteLine("Label fixup was {0}", ilReader.FixupSuccess ? "successful" : "not successful");
                foreach(ClrTest.Reflection.ILInstruction instr in ilReader)
                {
                    OpCode opcode = instr.OpCode;
                    OpCode otheropcode = OpCodes.Nop;

                    // if(instr.OpCode == OpCodes.Nop)   
                    // lgspActions.cs(1171,20): error CS0019: Operator `==' cannot be applied to operands of type
                    // `ClrTest.Reflection.ILInstruction.OpCode' and `System.Reflection.Emit.OpCodes.Nop'

                    // if(opcode == otheropcode)
                    // causes: lgspActions.cs(1173,20): error CS0019: Operator `==' cannot be applied to operands of type
                    // `System.Reflection.Emit.OpCode' and `System.Reflection.Emit.OpCode'

                    if(opcode.Equals(otheropcode)) writer.Write('.');
                    //                    else writer.WriteLine(instr.ToString());
                    else writer.WriteLine("   IL_{0:x4}:  {1,-10} {2}", instr.Offset, instr.OpCode, instr.RawOperand);
                }
                writer.Close();
                Console.WriteLine("MonoILReader succeeded");
            }
            catch
            {
                Console.WriteLine("MonoILReader failed");
            }
            action.DynamicMatch = (MatchInvoker) matcher.CreateDelegate(typeof(MatchInvoker), action);
        }

        private LoopState EmitLookupFront(SearchPlanEdge edge, Dictionary<String, ListState> listElemNameMap,
            Dictionary<String, LoopState> loopElemNameMap, ILGenerator il)
        {
            String elemName = edge.Target.Name;
            bool isNode = (edge.Target.NodeType == PlanNodeType.Node);
            Type elemType = isNode ? typeof(LGSPNode) : typeof(LGSPEdge);
            Type listType = isNode ? typeof(LinkedGrList<LGSPNode>) : typeof(LinkedGrList<LGSPEdge>);

            ListState listState = listElemNameMap[elemName];
            LoopState loopState = loopElemNameMap[elemName];

            //il.EmitWriteLine(String.Format("Entered EmitLookupFront for \"{0}\"", loopState.elemName));

            // LinkedGrList<LGSPNode/LGSPEdge> list = lgraph.nodes/edges[<<edge.Target.TypeID>>];

            il.Emit(OpCodes.Ldloc_1);
            il.Emit(OpCodes.Ldfld, typeof(LGSPGraph).GetField(isNode ? "nodes" : "edges"));
            if(isNode)
                Console.WriteLine("lookup node \"{0}\" with TypeID {1}", edge.Target.Name, edge.Target.TypeID);
            else
                Console.WriteLine("lookup edge \"{0}\" with TypeID {1}", edge.Target.Name, edge.Target.TypeID);
            EmitLoadConst(il, edge.Target.TypeID);
            il.Emit(OpCodes.Ldelem_Ref);
            //            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, listState.listVar);

            // head = list.head 
            il.Emit(OpCodes.Ldloc, listState.listVar);      // instead of dup

            il.Emit(OpCodes.Ldfld, listType.GetField("head"));
            //            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, listState.listHeadVar);

            // curpos = head.next
            il.Emit(OpCodes.Ldloc, listState.listHeadVar);  // instead of dup

            il.Emit(OpCodes.Ldfld, elemType.GetField("next"));
            il.Emit(OpCodes.Stloc, loopState.curPos);
            listState.lastCurPosVar = loopState.curPos;

            //il.EmitWriteLine("LookupFront curPos set to ");
            //il.EmitWriteLine(loopState.curPos);

            // jump to checkCondition

            il.Emit(OpCodes.Br, loopState.checkCondLabel);

            // mark next instruction as loop start

            il.MarkLabel(loopState.loopLabel);

            loopState.loopType = LoopType.Lookup;
            loopState.checkVar = listState.listHeadVar;
            loopState.isNode = isNode;
            return loopState;
        }

        private void EmitLookupTail(LoopState loop, ILGenerator il)
        {
            Type elemType = loop.isNode ? typeof(LGSPNode) : typeof(LGSPEdge);
            il.MarkLabel(loop.continueLabel);
            //il.EmitWriteLine("LookupTail continueLabel");
            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldfld, elemType.GetField("next"));
            il.Emit(OpCodes.Stloc, loop.curPos);

            //il.EmitWriteLine("LookupTail curPos set to ");
            //il.EmitWriteLine(loop.curPos);

            il.MarkLabel(loop.checkCondLabel);
            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldloc, loop.checkVar);
            //il.EmitWriteLine("LookupTail before Bne_Un");
            il.Emit(OpCodes.Bne_Un, loop.loopLabel);
            //il.EmitWriteLine("LookupTail after Bne_Un");
        }

        // edge.source = graph node, edge.target = graph edge
        private LoopState EmitExtendInOut(SearchPlanEdge edge, Dictionary<String, ListState> listElemNameMap,
            Dictionary<String, LoopState> loopElemNameMap, Dictionary<string, LocalBuilder> varElemNameMap, ILGenerator il, bool first)
        {
            ListState targetListState = listElemNameMap[edge.Target.Name];
            LoopState targetLoopState = loopElemNameMap[edge.Target.Name];
            bool isIncoming = edge.Type == PlanEdgeType.Incoming;

            //il.EmitWriteLine(String.Format("Entered EmitExtendInOut for \"{0}\"", targetLoopState.elemName));

            // head = sourceNode.<incoming/outgoing>.head 

            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Source.Name]);
            if(isIncoming)
            {
                il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("incoming", BindingFlags.Instance | BindingFlags.Public));
                il.Emit(OpCodes.Ldfld, typeof(LinkedGrInList).GetField("head"));
            }
            else
            {
                il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("outgoing", BindingFlags.Instance | BindingFlags.Public));
                il.Emit(OpCodes.Ldfld, typeof(LinkedGrOutList).GetField("head"));
            }
            //            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, targetListState.listHeadVar);

            // curpos = head.<inNode/outNode>.next
            il.Emit(OpCodes.Ldloc, targetListState.listHeadVar);        // instead of dup

            il.Emit(OpCodes.Ldflda, typeof(LGSPEdge).GetField(isIncoming ? "inNode" : "outNode", BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(InOutListNode).GetField("next"));
            il.Emit(OpCodes.Stloc, targetLoopState.curPos);

            //il.EmitWriteLine("ExtendInOut curPos set to ");
            //il.EmitWriteLine(targetLoopState.curPos);

            targetListState.lastCurPosVar = targetLoopState.curPos;

            // jump to checkCondition

            il.Emit(OpCodes.Br, targetLoopState.checkCondLabel);

            // mark next instruction as loop start

            il.MarkLabel(targetLoopState.loopLabel);

            // check type

            if(first)
            {
                il.Emit(OpCodes.Ldsfld, graph.Model.EdgeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
                il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("type", BindingFlags.Instance | BindingFlags.Public));
                il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
                il.Emit(OpCodes.Ldelem_I1);
                il.Emit(OpCodes.Brfalse, targetLoopState.continueLabel);
            }
            else
            {
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldsfld");
                il.Emit(OpCodes.Ldsfld, graph.Model.EdgeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldloc curPos = ");
                //il.EmitWriteLine(targetLoopState.curPos);
                il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldfld curPos.type");
                il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("type", BindingFlags.Instance | BindingFlags.Public));
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldsfld curPos.type.typeID");
                il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldelem_I1");
                il.Emit(OpCodes.Ldelem_I1);
                //il.Emit(OpCodes.Pop);
                //il.EmitWriteLine("!1. ExtendInOut before Brfalse");
                il.Emit(OpCodes.Brfalse, targetLoopState.continueLabel);
                //il.EmitWriteLine("!1. ExtendInOut after Brfalse");
            }

            if(isIncoming)
            {
                if(edge.Target.PatternEdgeSource.Visited)   // check op?
                {
                    il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                    il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("source"));
                    il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Target.PatternEdgeSource.Name]);
                    il.Emit(OpCodes.Bne_Un, targetLoopState.continueLabel);
                }
            }
            else
            {
                if(edge.Target.PatternEdgeTarget.Visited)   // check op?
                {
                    il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                    il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("target"));
                    il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Target.PatternEdgeTarget.Name]);
                    il.Emit(OpCodes.Bne_Un, targetLoopState.continueLabel);
                }
            }
            targetLoopState.loopType = isIncoming ? LoopType.Incoming : LoopType.Outgoing;
            targetLoopState.checkVar = targetListState.listHeadVar;
            return targetLoopState;
        }

        public static void ShowEdge(LGSPEdge edge)
        {
            LGSPEdge innext = edge.inNode.next;
            LGSPEdge outnext = edge.outNode.next;

            Console.WriteLine("ShowEdge: edge = {0}  innext = {1}  outnext = {2}", edge, innext, outnext);
        }

        private void EmitExtendInOutTail(LoopState loop, ILGenerator il)
        {
            il.MarkLabel(loop.continueLabel);
            //il.EmitWriteLine("ExtendInOutTail continueLabel \"" + loop.elemName + "\"");
            //            il.Emit(OpCodes.Ldloc, loop.curPos);
            //            il.Emit(OpCodes.Call, typeof(LGSPActions).GetMethod("AddFirst", BindingFlags.Static | BindingFlags.Public));

            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldflda, typeof(LGSPEdge).GetField((loop.loopType == LoopType.Incoming) ? "inNode" : "outNode",
                BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("next"));
            il.Emit(OpCodes.Stloc, loop.curPos);

            //il.EmitWriteLine("ExtendInOutTail curPos set to ");
            //il.EmitWriteLine(loop.curPos);

            //il.EmitWriteLine("ExtendInOutTail before checkCondLabel");
            il.MarkLabel(loop.checkCondLabel);
            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldloc, loop.checkVar);
            //il.EmitWriteLine("ExtendInOutTail before Bne_Un");
            il.Emit(OpCodes.Bne_Un, loop.loopLabel);
            //il.EmitWriteLine("ExtendInOutTail after Bne_Un");
        }

        // edge.source = graph edge, edge.target = graph node
        private void EmitImplicit(SearchPlanEdge edge, Dictionary<String, LoopState> loopElemNameMap, Dictionary<string, LocalBuilder> varElemNameMap, ILGenerator il)
        {
            //il.EmitWriteLine("Entered EmitImplicit");

#if OLDVERSION
            // begin of type check
            il.Emit(OpCodes.Ldsfld, graph.Model.NodeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));

            // elem = edge.<source/target>

            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Source.Name]);
            il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField((edge.Type == PlanEdgeType.ImplicitSource) ? "source" : "target"));
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, varElemNameMap[edge.Target.Name]);

            //il.EmitWriteLine("Implicit elem \"" + edge.Target.Name + "\" set to ");
            //il.EmitWriteLine(varElemNameMap[edge.Target.Name]);

            // check type

/*            FieldInfo[] fields = typeof(LGSPNode).GetFields(BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("EmitImplicit: fields of LGSPNode:");
            foreach(FieldInfo field in fields)
            {
                Console.WriteLine(field.Name);
            }*/
            il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("type", BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
            il.Emit(OpCodes.Ldelem_I1);
            il.Emit(OpCodes.Brfalse, loopElemNameMap[edge.Source.Name].continueLabel); 
#else
            // elem = edge.<source/target>

            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Source.Name]);
            il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField((edge.Type == PlanEdgeType.ImplicitSource) ? "source" : "target"));
            il.Emit(OpCodes.Stloc, varElemNameMap[edge.Target.Name]);

            //il.EmitWriteLine("Implicit elem \"" + edge.Target.Name + "\" set to ");
            //il.EmitWriteLine(varElemNameMap[edge.Target.Name]);

            // check type

            il.Emit(OpCodes.Ldsfld, graph.Model.NodeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Target.Name]);
            il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("type", BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
            il.Emit(OpCodes.Ldelem_I1);
            il.Emit(OpCodes.Brfalse, loopElemNameMap[edge.Source.Name].continueLabel);
#endif
        }

        private void EmitLoadConst(ILGenerator il, int val)
        {
            switch(val)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }
            if(val >= -128 && val <= 127)
                il.Emit(OpCodes.Ldc_I4_S, (sbyte) val);
            else
                il.Emit(OpCodes.Ldc_I4, val);
        }
#endif

        public LGSPAction[] GenerateSearchPlans(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, 
            params LGSPAction[] actions)
        {
            if(actions.Length == 0) throw new ArgumentException("No actions provided!");

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            sourceCode.Append("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + model.GetType().Namespace + ";\nusing " + actions[0].RulePattern.GetType().Namespace + ";\n\n"
                + "namespace de.unika.ipd.grGen.lgspActions\n{\n");

            foreach(LGSPAction action in actions)
            {
                PlanGraph planGraph = GeneratePlanGraph(graph, (PatternGraph) action.RulePattern.PatternGraph, false);
                MarkMinimumSpanningArborescence(planGraph, action.Name);
                SearchPlanGraph searchPlanGraph = GenerateSearchPlanGraph(planGraph);

                SearchPlanGraph[] negSearchPlanGraphs = new SearchPlanGraph[action.RulePattern.NegativePatternGraphs.Length];
                for(int i = 0; i < action.RulePattern.NegativePatternGraphs.Length; i++)
                {
                    PlanGraph negPlanGraph = GeneratePlanGraph(graph, (PatternGraph) action.RulePattern.NegativePatternGraphs[i], true);
                    MarkMinimumSpanningArborescence(negPlanGraph, action.Name + "_neg_" + (i + 1));
                    negSearchPlanGraphs[i] = GenerateSearchPlanGraph(negPlanGraph);
                }

                ScheduledSearchPlan scheduledSearchPlan = ScheduleSearchPlan(searchPlanGraph, negSearchPlanGraphs);

                CalculateNeededMaps(scheduledSearchPlan);

                sourceCode.Append("    public class DynAction_" + action.Name + " : LGSPAction\n    {\n"
                    + "        public DynAction_" + action.Name + "() { rulePattern = "
                    + action.RulePattern.GetType().Name + ".Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, "
                    + action.RulePattern.PatternGraph.Nodes.Length + ", " + action.RulePattern.PatternGraph.Edges.Length + "); "
                    + "matchesList = matches.matches; }\n"
                    + "        public override string Name { get { return \"" + action.Name + "\"; } }\n"
                    + "        private LGSPMatches matches;\n"
                    + "        private LGSPMatchesList matchesList;\n");

                sourceCode.Append(GenerateMatcherSourceCode(scheduledSearchPlan, action.Name, action.rulePattern));
            }
            sourceCode.Append("}");

            if(DumpDynSourceCode)
            {
                using(StreamWriter writer = new StreamWriter("dynamic_" + actions[0].Name + ".cs"))
                    writer.Write(sourceCode.ToString());
            }

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyName);
            compParams.ReferencedAssemblies.Add(actionsAssemblyName);

            compParams.GenerateInMemory = true;
#if PRODUCE_UNSAFE_MATCHERS
            compParams.CompilerOptions = "/optimize /unsafe";
#else
            compParams.CompilerOptions = "/optimize";
#endif

            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                throw new ArgumentException("Internal error: Illegal dynamic C# source code produced: " + errorMsg);
            }

            LGSPAction[] newActions = new LGSPAction[actions.Length];
            for(int i = 0; i < actions.Length; i++)
            {
                newActions[i] = (LGSPAction) compResults.CompiledAssembly.CreateInstance(
                    "de.unika.ipd.grGen.lgspActions.DynAction_" + actions[i].Name);
                if(newActions[i] == null)
                    throw new ArgumentException("Internal error: Generated assembly does not contain action '"
                        + actions[i].Name + "'!");
            }
            return newActions;
        }

        public LGSPAction GenerateSearchPlan(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, LGSPAction action)
        {
            return GenerateSearchPlans(graph, modelAssemblyName, actionsAssemblyName, action)[0];
        }

        //################################################################################
        // new source code generator - eja
        //################################################################################   
        // todo: erst implicit node, dann negative, auch wenn negative mit erstem implicit moeglich wird

        /// <summary>
        /// Base class for all search program operations, containing concatenation fields,
        /// so that search program operations can form a linked search program list
        /// double linked list; next points to the following list element or null
        /// previous points to the preceding list element 
        /// or the enclosing search program operation within the list anchor element
        /// </summary>
        private abstract class SearchProgramOperation
        {
            public SearchProgramOperation Next;
            public SearchProgramOperation Previous;

            /// <summary>
            /// dumps search program operation (as string) into source builder
            /// </summary>
            public abstract void Dump(SourceBuilder builder);

            /// <summary>
            /// emits c# code implementing search program operation into source builder
            /// </summary>
            public abstract void Emit(SourceBuilder sourceCode);

            /// <summary>
            /// appends the new element to the search program operations list
            /// whose closing element until now was this
            /// returns the new closing element - the new elment
            /// </summary>
            public SearchProgramOperation Append(SearchProgramOperation newElement)
            {
                Debug.Assert(Next == null, "Append only at end of list");
                Debug.Assert(newElement.Previous == null, "Append only of element without predecessor");
                Next = newElement;
                newElement.Previous = this;
                return newElement;
            }

            /// <summary>
            /// insert the new element into the search program operations list
            /// between this and the succeeding element
            /// returns the element after this - the new element
            /// </summary>
            public SearchProgramOperation Insert(SearchProgramOperation newElement)
            {
                Debug.Assert(newElement.Previous == null, "Insert only of single unconnected element (previous)");
                Debug.Assert(newElement.Next == null, "Insert only of single unconnected element (next)");
                
                if (Next == null)
                {
                    return Append(newElement);
                }

                SearchProgramOperation Successor = Next;
                Next = newElement;
                newElement.Next = Successor;
                Successor.Previous = newElement;
                newElement.Previous = this;           
                return newElement;
            }

            /// <summary>
            /// returns whether operation is a nesting operation 
            /// containing other elements within some list inside
            /// which might be nesting operations themselves -> yes
            /// or is an elementary search program operation 
            /// or an operation which can only contain elementary operations -> no
            /// (check preset returns false, too, despite further check is nested within,
            /// more general: check failed operations are not to be regarded as nested, 
            /// only search iteration operations)
            /// </summary>
            public abstract bool IsNestingOperation();

            /// <summary>
            /// returns the nested list anchor
            /// null if list not created or IsNestingOperation == false
            /// </summary>
            public abstract SearchProgramOperation GetNestedOperationsList();
        }

        /// <summary>
        /// returns operation enclosing the operation handed in
        /// </summary>
        SearchProgramOperation GetEnclosingOperation(SearchProgramOperation nestedOperation)
        {
            // iterate list leftwards, leftmost list element is list anchor element,
            // which contains uplink to enclosing operation in it's previous member
            do
            {
                nestedOperation = nestedOperation.Previous;
            }
            while (!nestedOperation.IsNestingOperation());

            return nestedOperation;
        }

        /// <summary>
        /// Search program list anchor element,
        /// containing first list element within inherited Next member
        /// Inherited to be able to access the first element via Next
        /// Previous points to enclosing search program operation
        /// </summary>
        private class SearchProgramList : SearchProgramOperation
        {
            public SearchProgramList(SearchProgramOperation enclosingOperation)
            {
                Previous = enclosingOperation;
            }

            public override void Dump(SourceBuilder builder)
            {
                SearchProgramOperation currentOperation = Next;

                // depth first walk over nested search program lists
                // walk current list here, recursive descent within local dump-methods
                while (currentOperation != null)
                {
                    currentOperation.Dump(builder);
                    currentOperation = currentOperation.Next;
                }
            }

            /// <summary>
            /// Emits code for search program list
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                SearchProgramOperation currentOperation = Next;

                // depth first walk over nested search program lists
                // walk current list here, recursive descent within local Emit-methods
                while (currentOperation != null)
                {
                    currentOperation.Emit(sourceCode);
                    currentOperation = currentOperation.Next;
                }
            }

            public override bool IsNestingOperation()
            {
                return false; // starts list, but doesn't contain one
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }
        }

        /// <summary>
        /// Class representing a search program,
        /// which is a list of search program operations
        ///   some search program operations contain nested search program operations,
        ///   yielding a search program tree in fact
        /// representing/assembling a backtracking search program
        /// for finding a homomorphic mapping of the pattern graph within the host graph.
        /// is itself the outermost enclosing operation
        /// list forming concatenation field used for adding search subprograms
        /// </summary>
        private class SearchProgram : SearchProgramOperation
        {
            public SearchProgram(string name, 
                string[] parameters, 
                bool[] parameterIsNode)
            {
                Name = name;

                Parameters = parameters;
                ParameterIsNode = parameterIsNode;

                IsSubprogram = parameters!=null;
            }

            /// <summary>
            /// Dumps search program followed by search subprograms
            /// </summary>
            public override void Dump(SourceBuilder builder)
            {
                // first dump local content
                builder.AppendFrontFormat("Search {0}program {1}",
                    IsSubprogram ? "sub" : "", Name);
                // parameters
                if (Parameters != null)
                {
                    for (int i = 0; i < Parameters.Length; ++i)
                    {
                        string typeOfParameterVariableContainingCandidate =
                            ParameterIsNode[i] ? "LGSPNode" : "LGSPEdge";
                        string parameterVariableContainingCandidate =
                            NameOfCandidateVariable(Parameters[i],
                                ParameterIsNode[i]);
                        builder.AppendFormat(", {0} {1}",
                            typeOfParameterVariableContainingCandidate,
                            parameterVariableContainingCandidate);
                    }
                }
                builder.Append("\n");

                // then nested content
                if (OperationsList != null)
                {
                    builder.Indent();
                    OperationsList.Dump(builder);
                    builder.Unindent();
                }

                // then next search subprogram
                if (Next != null)
                {
                    Next.Dump(builder);
                }
            }

            /// <summary>
            /// Emits the matcher source code for all search programs
            /// first head of matching function of the current search program
            /// then the search program operations list in dept first walk over search program operations list
            /// then tail of matching function of the current search progran
            /// and finally continue in search program list by emitting following search program
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
#if RANDOM_LOOKUP_LIST_START
                sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

                if (IsSubprogram)
                {
#if PRODUCE_UNSAFE_MATCHERS
                    sourceCode.AppendFront("unsafe ");
#endif
                    sourceCode.AppendFrontFormat("public void {0}(LGSPGraph graph, int maxMatches, IGraphElement[] parameters", Name);
                    for (int i = 0; i < Parameters.Length; ++i)
                    {
                        string typeOfParameterVariableContainingCandidate =
                            ParameterIsNode[i] ? "LGSPNode" : "LGSPEdge";
                        string parameterVariableContainingCandidate =
                            NameOfCandidateVariable(Parameters[i],
                                ParameterIsNode[i]);
                        sourceCode.AppendFormat(", {0} {1}",
                            typeOfParameterVariableContainingCandidate,
                            parameterVariableContainingCandidate);
                    }
                    sourceCode.Append(")\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    OperationsList.Emit(sourceCode);

                    sourceCode.AppendFront("return;\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // emit next search subprogram
                    if (Next != null)
                    {
                        Next.Emit(sourceCode);
                    }
                }
                else // !IsSubprogram
                {
                    sourceCode.Indent(); // we're within some namespace
                    sourceCode.Indent(); // we're within some class

#if PRODUCE_UNSAFE_MATCHERS
                    soureCode.AppendFront("unsafe ");
#endif
                    sourceCode.AppendFrontFormat("public LGSPMatches {0}(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)\n", Name);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    //            // [0] Matches matches = new Matches(this);
                    //            sourceCode.AppendFront("LGSPMatches matches = new LGSPMatches(this);\n");
                    sourceCode.AppendFront("matches.matches.Clear();\n");

                    OperationsList.Emit(sourceCode);

                    sourceCode.AppendFront("return matches;\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                    //            int compileGenMatcher = Environment.TickCount;
                    //            long genSourceTicks = startGenMatcher.ElapsedTicks;
                    
                    // emit search subprograms
                    if (Next != null)
                    {
                        Next.Emit(sourceCode);
                    }

                    // ugly close class not opened here
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }

            public override bool IsNestingOperation()
            {
                return true; // contains complete nested search program
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return OperationsList;
            }

            public string Name;
            public bool IsSubprogram;

            public string[] Parameters;
            public bool[] ParameterIsNode;

            public SearchProgramList OperationsList;
        }

        /// <summary>
        /// Base class for search program check operations
        /// contains list anchor for operations to execute when check failed
        /// </summary>
        private abstract class CheckOperation : SearchProgramOperation
        {
            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            // (nested) operations to execute when check failed
            public SearchProgramList CheckFailedOperations;
        }

        /// <summary>
        /// Base class for search program type determining operations,
        /// setting current type for following get candidate operation
        /// </summary>
        private abstract class GetType : SearchProgramOperation
        {
        }

        /// <summary>
        /// Available types of GetTypeByIteration operations
        /// </summary>
        private enum GetTypeByIterationType
        {
            ExplicitelyGiven, // iterate the explicitely given types
            AllCompatible // iterate all compatible types of the pattern element type
        }

        /// <summary>
        /// Class representing "iterate over the allowed types" operation,
        /// setting type id to use in the following get candidate by element iteration
        /// </summary>
        private class GetTypeByIteration : GetType
        {
            public GetTypeByIteration(
                GetTypeByIterationType type,
                string patternElementName,
                string rulePatternTypeNameOrTypeName,
                bool isNode)
            {
                Type = type;
                PatternElementName = patternElementName;
                if (type == GetTypeByIterationType.ExplicitelyGiven) {
                    TypeName = rulePatternTypeNameOrTypeName;
                } else { // type == GetTypeByIterationType.AllCompatible
                    RulePatternTypeName = rulePatternTypeNameOrTypeName;
                }
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump local content
                builder.AppendFront("GetType ByIteration ");
                if(Type==GetTypeByIterationType.ExplicitelyGiven) {
                    builder.Append("ExplicitelyGiven ");
                    builder.AppendFormat("on {0} in {1} node:{2}\n", 
                        PatternElementName, TypeName, IsNode);
                } else { // Type==GetTypeByIterationType.AllCompatible
                    builder.Append("AllCompatible ");
                    builder.AppendFormat("on {0} in {1} node:{2}\n",
                        PatternElementName, RulePatternTypeName, IsNode);
                }
                // then nested content
                if (NestedOperationsList != null)
                {
                    builder.Indent();
                    NestedOperationsList.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for get type by iteration search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

                // todo: randomisierte auswahl des typen wenn RANDOM_LOOKUP_LIST_START ?

                // emit type iteration loop header
                string variableContainingTypeForCandidate = NameOfTypeForCandidateVariable(
                    PatternElementName, IsNode);
                string containerWithAvailableTypes;
                if (Type == GetTypeByIterationType.ExplicitelyGiven)
                {
                    containerWithAvailableTypes = TypeName
                        + "." + PatternElementName + "_AllowedTypes";
                }
                else //(Type == GetTypeByIterationType.AllCompatible)
                {
                    containerWithAvailableTypes = RulePatternTypeName
                        + ".typeVar.SubOrSameTypes";
                }
                
                sourceCode.AppendFrontFormat("foreach(ITypeFramework {0} in {1})\n",
                    variableContainingTypeForCandidate, containerWithAvailableTypes);

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit type id setting and loop body 
                string variableContainingTypeIDForCandidate = NameOfTypeIdForCandidateVariable(
                    PatternElementName, IsNode);
                sourceCode.AppendFrontFormat("int {0} = {1}.typeID;\n",
                    variableContainingTypeIDForCandidate, variableContainingTypeForCandidate);

                NestedOperationsList.Emit(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            public override bool IsNestingOperation()
            {
                return true;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return NestedOperationsList;
            }

            public GetTypeByIterationType Type;
            public string PatternElementName;

            public string RulePatternTypeName; // only valid if ExplicitelyGiven
            public string TypeName; // only valid if AllCompatible

            public bool IsNode; // node|edge

            public SearchProgramList NestedOperationsList;
        }

        /// <summary>
        /// Class representing "get the allowed type" operation,
        /// setting type id to use in the following get candidate by element iteration
        /// </summary>
        private class GetTypeByDrawing : GetType
        {
            public GetTypeByDrawing(
                string patternElementName,
                string typeID,
                bool isNode)
            {
                PatternElementName = patternElementName;
                TypeID = typeID;
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("GetType GetTypeByDrawing ");
                builder.AppendFormat("on {0} id:{1} node:{2}\n",
                    PatternElementName, TypeID, IsNode);
            }

            /// <summary>
            /// Emits code for get type by drawing search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

                string variableContainingTypeIDForCandidate = NameOfTypeIdForCandidateVariable(
                    PatternElementName, IsNode);
                sourceCode.AppendFrontFormat("int {0} = {1};\n",
                    variableContainingTypeIDForCandidate, TypeID);
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public string PatternElementName;
            public string TypeID;
            public bool IsNode; // node|edge
        }

        /// <summary>
        /// Base class for search program candidate determining operations,
        /// setting current candidate for following check candidate operation
        /// </summary>
        private abstract class GetCandidate : SearchProgramOperation
        {
            public string PatternElementName;
        }

        /// <summary>
        /// Available types of GetCandidateByIteration operations
        /// </summary>
        private enum GetCandidateByIterationType
        {
            GraphElements, // available graph elements
            IncidentEdges // incident edges
        }

        /// <summary>
        /// Class representing "get candidate by iteration" operations,
        /// setting current candidate for following check candidate operation
        /// </summary>
        private class GetCandidateByIteration : GetCandidate
        {
            public GetCandidateByIteration(
                GetCandidateByIterationType type,
                string patternElementName,
                bool isNode)
            {
                Debug.Assert(type == GetCandidateByIterationType.GraphElements);
                Type = type;
                PatternElementName = patternElementName;
                IsNode = isNode;
            }

            public GetCandidateByIteration(
                GetCandidateByIterationType type,
                string patternElementName,
                string startingPointNodeName,
                bool getIncoming)
            {
                Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
                Type = type;
                PatternElementName = patternElementName;
                StartingPointNodeName = startingPointNodeName;
                GetIncoming = getIncoming;
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump local content
                builder.AppendFront("GetCandidate ByIteration ");
                if (Type == GetCandidateByIterationType.GraphElements) {
                    builder.Append("GraphElements ");
                    builder.AppendFormat("on {0} node:{1}\n",
                        PatternElementName, IsNode);
                } else { //Type==GetCandidateByIterationType.IncidentEdges
                    builder.Append("IncidentEdges ");
                    builder.AppendFormat("on {0} from {1} incoming:{2}\n",
                        PatternElementName, StartingPointNodeName, GetIncoming);
                }
                // then nested content
                if (NestedOperationsList != null)
                {
                    builder.Indent();
                    NestedOperationsList.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for get candidate by iteration search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if (Type == GetCandidateByIterationType.GraphElements)
                {
                    // --- emit loop header ---
                    // open loop header 
                    sourceCode.AppendFrontFormat("for(");
                    // emit declaration of variable containing graph elements list head
                    string typeOfVariableContainingListHead =
                        IsNode ? "LGSPNode" : "LGSPEdge";
                    string variableContainingListHead = NameOfCandidateIterationListHead(
                        PatternElementName, IsNode);
                    sourceCode.AppendFormat("{0} {1}",
                        typeOfVariableContainingListHead, variableContainingListHead);
                    // emit initialization of variable containing graph elements list head
                    string graphMemberContainingListHeadByType =
                        IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                    string variableContainingTypeIDForCandidate = NameOfTypeIdForCandidateVariable(
                        PatternElementName, IsNode);
                    sourceCode.AppendFormat(" = graph.{0}[{1}], ",
                        graphMemberContainingListHeadByType, variableContainingTypeIDForCandidate);
                    // emit declaration and initialization of variable containing candidates
                    string variableContainingCandidate = NameOfCandidateVariable(
                        PatternElementName, IsNode);
                    sourceCode.AppendFormat("{0} = {1}.typeNext; ",
                        variableContainingCandidate, variableContainingListHead);
                    // emit loop condition: check for head reached again 
                    sourceCode.AppendFormat("{0} != {1}; ",
                        variableContainingCandidate, variableContainingListHead);
                    // emit loop increment: switch to next element of same type
                    sourceCode.AppendFormat("{0} = {0}.typeNext",
                        variableContainingCandidate);
                    // close loop header
                    sourceCode.Append(")\n");

                    // open loop
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
                else //Type==GetCandidateByIterationType.IncidentEdges
                {
                    if(sourceCode.CommentSourceCode)
                        sourceCode.AppendFrontFormat("// Extend {0} {1} from {2} \n",
                                GetIncoming ? "incoming" : "outgoing",
                                PatternElementName, StartingPointNodeName);

                    // emit declaration of variable containing incident edges list head
                    string typeOfVariableContainingListHead = "LGSPEdge";
                    string variableContainingListHead = NameOfCandidateIterationListHead(
                        PatternElementName, false);
                    sourceCode.AppendFrontFormat("{0} {1}",
                        typeOfVariableContainingListHead, variableContainingListHead);
                    // emit initialization of variable containing incident edges list head
                    string variableContainingStartingPointNode = NameOfCandidateVariable(
                        StartingPointNodeName, true);
                    string memberOfNodeContainingListHead =
                        GetIncoming ? "inhead" : "outhead";
                    sourceCode.AppendFormat(" = {0}.{1};\n",
                        variableContainingStartingPointNode, memberOfNodeContainingListHead);

                    // emit execute the following code only if head != null
                    // todo: replace by check == null and continue
                    sourceCode.AppendFrontFormat("if({0} != null)\n", variableContainingListHead);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates
                    string typeOfVariableContainingCandidate = "LGSPEdge";
                    string variableContainingCandidate = NameOfCandidateVariable(
                        PatternElementName, false);
                    sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                        typeOfVariableContainingCandidate, variableContainingCandidate,
                        variableContainingListHead);
                    // open loop
                    sourceCode.AppendFront("do\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // emit loop tail
                    // - emit switch to next edge in list within assignment expression
                    string memberOfEdgeContainingNextEdge =
                        GetIncoming ? "inNext" : "outNext";
                    sourceCode.AppendFrontFormat("while( ({0} = {0}.{1})",
                        variableContainingCandidate, memberOfEdgeContainingNextEdge);
                    // - check condition that head has been reached again (compare with assignment value)
                    sourceCode.AppendFormat(" != {0} );\n", variableContainingListHead);

                    // close the head != null check
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }

            public override bool IsNestingOperation()
            {
                return true;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return NestedOperationsList;
            }

            public GetCandidateByIterationType Type;

            public bool IsNode; // node|edge - only available if GraphElements

            public string StartingPointNodeName; // from pattern - only available if IncidentEdges
            public bool GetIncoming; // incoming|outgoing - only available if IncidentEdges

            public SearchProgramList NestedOperationsList;
        }

        /// <summary>
        /// Available types of GetCandidateByDrawing operations
        /// </summary>
        private enum GetCandidateByDrawingType
        {
            NodeFromEdge, // draw node from given edge
            FromInputs // draw element from inputs
        }

        /// <summary>
        /// Class representing "get node by drawing" operation,
        /// setting current candidate for following check candidate operations
        /// </summary>
        private class GetCandidateByDrawing : GetCandidate
        {
            public GetCandidateByDrawing(
                GetCandidateByDrawingType type,
                string patternElementName,
                string patternElementTypeName,
                string startingPointEdgeName,
                bool getSource)
            {
                Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
                Type = type;
                PatternElementName = patternElementName;
                PatternElementTypeName = patternElementTypeName;
                StartingPointEdgeName = startingPointEdgeName;
                GetSource = getSource;
            }

            public GetCandidateByDrawing(
                GetCandidateByDrawingType type,
                string patternElementName,
                string inputIndex,
                bool isNode)
            {
                Debug.Assert(type == GetCandidateByDrawingType.FromInputs);
                Type = type;
                PatternElementName = patternElementName;
                InputIndex = inputIndex;
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("GetCandidate ByDrawing ");
                if(Type==GetCandidateByDrawingType.NodeFromEdge) {
                    builder.Append("NodeFromEdge ");
                    builder.AppendFormat("on {0} of {1} from {2} source:{3}\n",
                        PatternElementName, PatternElementTypeName, 
                        StartingPointEdgeName, GetSource);
                } else { // Type==GetCandidateByDrawingType.FromInputs
                    builder.Append("FromInputs ");
                    builder.AppendFormat("on {0} index:{1} node:{2}\n",
                        PatternElementName, InputIndex, IsNode);
                }
            }

            /// <summary>
            /// Emits code for get candidate by drawing search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if(Type==GetCandidateByDrawingType.NodeFromEdge)
                {
                    if(sourceCode.CommentSourceCode)
                        sourceCode.AppendFrontFormat("// Implicit {0} {1} from {2} \n",
                                GetSource ? "source" : "target",
                                PatternElementName, StartingPointEdgeName);

                    // emit declaration of variable containing candidate node
                    string typeOfVariableContainingCandidate = "LGSPNode";
                    string variableContainingCandidate = NameOfCandidateVariable(
                        PatternElementName, true);
                    sourceCode.AppendFrontFormat("{0} {1}",
                        typeOfVariableContainingCandidate, variableContainingCandidate);
                    // emit initialization with demanded node from variable containing edge
                    string variableContainingStartingPointEdge = NameOfCandidateVariable(
                        StartingPointEdgeName, false);
                    string whichImplicitNode = 
                        GetSource ? "source" : "target";
                    sourceCode.AppendFormat(" = {0}.{1};\n",
                        variableContainingStartingPointEdge, whichImplicitNode);
                }
                else //Type==GetCandidateByDrawingType.FromInputs
                {
                    if(sourceCode.CommentSourceCode)
                        sourceCode.AppendFrontFormat("// Preset {0} \n", PatternElementName);

                    // emit declaration of variable containing candidate node
                    string typeOfVariableContainingCandidate =
                        IsNode ? "LGSPNode" : "LGSPEdge";
                    string variableContainingCandidate = NameOfCandidateVariable(
                        PatternElementName, IsNode);
                    sourceCode.AppendFrontFormat("{0} {1}",
                        typeOfVariableContainingCandidate, variableContainingCandidate);
                    // emit initialization with element from input parameters array
                    sourceCode.AppendFormat(" = ({0}) parameters[{1}];\n",
                        typeOfVariableContainingCandidate, InputIndex);
                }
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public GetCandidateByDrawingType Type;

            public string PatternElementTypeName; // only valid if NodeFromEdge
            public string StartingPointEdgeName; // from pattern - only valid if NodeFromEdge
            public bool GetSource; // source|target - only valid if NodeFromEdge

            public string InputIndex; // only valid if FromInputs
            public bool IsNode; // node|edge - only valid if FromInputs

        }

        /// <summary>
        /// Base class for search program candidate filtering operations
        /// </summary>
        private abstract class CheckCandidate : CheckOperation
        {
            public string PatternElementName;
        }

        /// <summary>
        /// Available types of CheckCandidateForType operations
        /// </summary>
        private enum CheckCandidateForTypeType
        {
            ByIsAllowedType, //check by inspecting the IsAllowedType array of the rule pattern
            ByIsMyType, // check by inspecting the IsMyType array of the graph element's type model
            ByAllowedTypes // check by comparing against a member of the AllowedTypes array of the rule pattern
        }

        /// <summary>
        /// Class representing "check whether candidate is of allowed type" operation
        /// </summary>
        private class CheckCandidateForType : CheckCandidate
        {
            public CheckCandidateForType(
                CheckCandidateForTypeType type,
                string patternElementName,
                string rulePatternTypeNameOrTypeNameOrTypeID,
                bool isNode)
            {
                Type = type;
                PatternElementName = patternElementName;
                if (type == CheckCandidateForTypeType.ByIsAllowedType) {
                    RulePatternTypeName = rulePatternTypeNameOrTypeNameOrTypeID;
                } else if (type == CheckCandidateForTypeType.ByIsMyType) {
                    TypeName = rulePatternTypeNameOrTypeNameOrTypeID;
                } else { // CheckCandidateForTypeType.ByAllowedTypes
                    TypeID = rulePatternTypeNameOrTypeNameOrTypeID;
                }
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckCandidate ForType ");
                if (Type == CheckCandidateForTypeType.ByIsAllowedType) {
                    builder.Append("ByIsAllowedType ");
                    builder.AppendFormat("on {0} in {1} node:{2}\n",
                        PatternElementName, RulePatternTypeName, IsNode);
                } else if (Type == CheckCandidateForTypeType.ByIsMyType) {
                    builder.Append("ByIsMyType ");
                    builder.AppendFormat("on {0} in {1} node:{2}\n",
                        PatternElementName, TypeName, IsNode);
                } else { // Type == CheckCandidateForTypeType.ByAllowedTypes
                    builder.Append("ByAllowedTypes ");
                    builder.AppendFormat("on {0} id:{1} node:{2}\n",
                        PatternElementName, TypeID, IsNode);
                }
                
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check candidate for type search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // emit check decision
                string variableContainingCandidate = NameOfCandidateVariable(
                    PatternElementName, IsNode);
                if (Type == CheckCandidateForTypeType.ByIsAllowedType)
                {
                    string isAllowedTypeArrayMemberOfRulePattern =
                        PatternElementName + "_IsAllowedType";
                    sourceCode.AppendFrontFormat("if(!{0}.{1}[{2}.type.typeID]) ",
                        RulePatternTypeName, isAllowedTypeArrayMemberOfRulePattern,
                        variableContainingCandidate);
                }
                else if (Type == CheckCandidateForTypeType.ByIsMyType)
                {
                    sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.type.typeID]) ",
                        TypeName, variableContainingCandidate);
                }
                else // Type == CheckCandidateForTypeType.ByAllowedTypes)
                {
                    sourceCode.AppendFrontFormat("if({0}.type.typeID != {1}) ",
                        variableContainingCandidate, TypeID);
                }
                // emit check failed code
                sourceCode.Append("{\n");
                sourceCode.Indent();
                CheckFailedOperations.Emit(sourceCode);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            public CheckCandidateForTypeType Type;

            public string RulePatternTypeName; // only valid if ByIsAllowedType
            public string TypeName; // only valid if ByIsMyType
            public string TypeID; // only valid if ByAllowedTypes

            public bool IsNode; // node|edge
        }

        /// <summary>
        /// Class representing some check candidate operation,
        /// which was determined at generation time to always fail 
        /// </summary>
        private class CheckCandidateFailed : CheckCandidate
        {
            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckCandidate Failed \n");
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check candidate failed search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // emit check failed code
                CheckFailedOperations.Emit(sourceCode);
            }
        }

        /// <summary>
        /// Class representing "check whether candidate is connected to the elements
        ///   it should be connected to, according to the pattern" operation
        /// </summary>
        private class CheckCandidateForConnectedness : CheckCandidate
        {
            public CheckCandidateForConnectedness(
                string patternElementName, 
                string patternNodeName,
                string patternEdgeName,
                bool checkSource)
            {
                // pattern element is the candidate to check, either node or edge
                PatternElementName = patternElementName;
                PatternNodeName = patternNodeName;
                PatternEdgeName = patternEdgeName;
                CheckSource = checkSource; 
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckCandidate ForConnectedness ");
                builder.AppendFormat("{0}=={1}.{2}\n",
                    PatternNodeName, PatternEdgeName, CheckSource ? "source" : "target");
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check candidate for connectedness search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // emit check decision for is candidate connected to already found partial match
                // i.e. edge source/target equals node
                sourceCode.AppendFrontFormat("if({0}.{1} != {2}) ",
                    NameOfCandidateVariable(PatternEdgeName, false),
                    CheckSource ? "source" : "target",
                    NameOfCandidateVariable(PatternNodeName, true));
                // emit check failed code
                sourceCode.Append("{\n");
                sourceCode.Indent();
                CheckFailedOperations.Emit(sourceCode);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            public string PatternNodeName;
            public string PatternEdgeName;
            public bool CheckSource; // source|target
        }

        /// <summary>
        /// Class representing "check whether candidate is not already mapped 
        ///   to some other pattern element, to ensure required isomorphy" operation
        /// </summary>
        private class CheckCandidateForIsomorphy : CheckCandidate
        {
            public CheckCandidateForIsomorphy(
                string patternElementName,
                string homomorphicID,
                bool positive,
                bool isNode)
            {
                PatternElementName = patternElementName;
                HomomorphicID = homomorphicID;
                Positive = positive;
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckCandidate ForIsomorphy ");
                builder.AppendFormat("on {0} hom-id:{1} positive:{2} node:{3}\n",
                    PatternElementName, HomomorphicID, Positive, IsNode);
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check candidate for isomorphy search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // open decision whether to fail
                sourceCode.AppendFront("if(");

                // fail if graph element contained within candidate was already matched
                // (to another pattern element)
                // as this would cause a homomorphic match
                string variableContainingCandidate = NameOfCandidateVariable(
                    PatternElementName, IsNode);
                string mappedToMember = Positive ? "mappedTo" : "negMappedTo";
                sourceCode.AppendFormat("{0}.{1} != 0",
                    variableContainingCandidate, mappedToMember);

                // but only if isomorphy is demanded (homomorphic id == 0)
                // otherwise homomorphy is allowed, then we only fail if the graph element 
                // is not matched to the same homomorphy class as the candidate
                if (HomomorphicID != "0")
                {
                    sourceCode.AppendFormat("&& {0}.{1} != {2}",
                        variableContainingCandidate, mappedToMember,
                        HomomorphicID);
                }

                // close decision
                sourceCode.Append(") ");

                // emit check failed code
                sourceCode.Append("{\n");
                sourceCode.Indent();
                CheckFailedOperations.Emit(sourceCode);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            public string HomomorphicID; // if 0 check for isomorphy, 
                                         // otherwise homomorphic mapping allowed, 
                                         // then check for given homomorphy class 
            public bool Positive; // positive|negative (mappedTo/negMappedTo)
            public bool IsNode; // node|edge
        }

        /// <summary>
        /// Class representing "check whether candidate was preset (not null)" operation
        /// </summary>
        private class CheckCandidateForPreset : CheckCandidate
        {
            public CheckCandidateForPreset(
                string patternElementName,
                bool isNode)
            {
                PatternElementName = patternElementName;
                IsNode = isNode;
            }

            public void CompleteWithArguments(
                List<string> neededElements,
                List<bool> neededElementIsNode)
            {
                NeededElements = new string[neededElements.Count];
                NeededElementIsNode = new bool[neededElementIsNode.Count];
                int i = 0;
                foreach (string ne in neededElements)
                {
                    NeededElements[i] = ne;
                    ++i;
                }
                i = 0;
                foreach (bool nein in neededElementIsNode)
                {
                    NeededElementIsNode[i] = nein;
                    ++i;
                }
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckCandidate ForPreset ");
                builder.AppendFormat("on {0} node:{1} ",
                    PatternElementName, IsNode);
                if (NeededElements != null)
                {
                    builder.Append("with ");
                    foreach (string neededElement in NeededElements)
                    {
                        builder.AppendFormat("{0} ", neededElement);
                    }
                }
                builder.Append("\n");
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check candidate for preset search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // emit check whether candidate was preset (not null)
                string variableContainingCandidate = NameOfCandidateVariable(
                    PatternElementName, IsNode);
                sourceCode.AppendFrontFormat("if({0} == null) ",
                    variableContainingCandidate);

                // emit check failed code
                sourceCode.Append("{\n");
                sourceCode.Indent();
                // emit call to search program doing lookup if candidate was not preset
                string nameOfMissingPresetHandlingMethod = NameOfMissingPresetHandlingMethod(
                    PatternElementName);
                sourceCode.AppendFrontFormat("{0}",
                    nameOfMissingPresetHandlingMethod);
                // emit call arguments
                sourceCode.Append("(");
                sourceCode.Append("graph, maxMatches, parameters");
                for (int i = 0; i < NeededElements.Length; ++i)
                {
                    sourceCode.AppendFormat(", {0}", NameOfCandidateVariable(
                        NeededElements[i], NeededElementIsNode[i]));
                }
                sourceCode.Append(")");
                sourceCode.Append(";\n");

                // emit further check failed code
                CheckFailedOperations.Emit(sourceCode);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            public string[] NeededElements;
            public bool[] NeededElementIsNode;

            public bool IsNode; // node|edge
        }

        /// <summary>
        /// Base class for search program operations
        /// filtering partial match
        /// (of the pattern part under construction)
        /// </summary>
        private abstract class CheckPartialMatch : CheckOperation
        {
        }

        /// <summary>
        /// Class representing "check whether the negative pattern applies" operation
        /// </summary>
        private class CheckPartialMatchByNegative : CheckPartialMatch
        {
            public CheckPartialMatchByNegative(string[] neededElements)
            {
                NeededElements = neededElements;
            }

            public override void Dump(SourceBuilder builder)
            {
                Debug.Assert(CheckFailedOperations == null, "check negative without direct check failed code");
                // first dump local content
                builder.AppendFront("CheckPartialMatch ByNegative with ");
                foreach (string neededElement in NeededElements)
                {
                    builder.AppendFormat("{0} ", neededElement);
                }
                builder.Append("\n");
                // then nested content
                if (NestedOperationsList != null)
                {
                    builder.Indent();
                    NestedOperationsList.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check partial match by negative pattern search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// NegativePattern \n");
                // currently needed because of multiple negMapped backup variables with same name
                // todo: assign names to negatives, mangle that name in, then remove block again
                // todo: remove (neg)mapped backup variables altogether, then remove block again
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                NestedOperationsList.Emit(sourceCode);

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                //if(sourceCode.CommentSourceCode) reinsert when block is removed
                //    sourceCode.AppendFront("// NegativePattern end\n");
            }

            public override bool IsNestingOperation()
            {
                return true;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return NestedOperationsList;
            }

            public string[] NeededElements;

            // search program of the negative pattern
            public SearchProgramList NestedOperationsList;
        }

        /// <summary>
        /// Class representing "check whether the condition applies" operation
        /// </summary>
        private class CheckPartialMatchByCondition : CheckPartialMatch
        {
            public CheckPartialMatchByCondition(
                string conditionID,
                string rulePatternTypeName,
                string[] neededNodes,
                string[] neededEdges)
            {
                ConditionID = conditionID;
                RulePatternTypeName = rulePatternTypeName;
                
                int i = 0;
                NeededElements = new string[neededNodes.Length + neededEdges.Length];
                NeededElementIsNode = new bool[neededNodes.Length + neededEdges.Length];
                foreach (string neededNode in neededNodes)
                {
                    NeededElements[i] = neededNode;
                    NeededElementIsNode[i] = true;
                    ++i;
                }
                foreach (string neededEdge in neededEdges)
                {
                    NeededElements[i] = neededEdge;
                    NeededElementIsNode[i] = false;
                    ++i;
                }
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckPartialMatch ByCondition ");
                builder.AppendFormat("id:{0} in {1} with ",
                    ConditionID, RulePatternTypeName);
                foreach (string neededElement in NeededElements)
                {
                    builder.AppendFormat("{0} ", neededElement);
                }
                builder.Append("\n");
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }

            }

            /// <summary>
            /// Emits code for check partial match by condition search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// Condition \n");

                // open decision
                sourceCode.AppendFront("if(");
                // emit call to condition checking code
                sourceCode.AppendFormat("!{0}.Condition_{1}",
                    RulePatternTypeName, ConditionID);
                // emit call arguments
                sourceCode.Append("(");
                bool first = true;
                for (int i = 0; i < NeededElements.Length; ++i)
                {
                    if (!first)
                    {
                        sourceCode.Append(", ");
                    }
                    else
                    {
                        first = false;
                    }
                    sourceCode.Append(NameOfCandidateVariable(
                        NeededElements[i], NeededElementIsNode[i]));
                }
                sourceCode.Append(")");
                // close decision
                sourceCode.Append(") ");

                // emit check failed code
                sourceCode.Append("{\n");
                sourceCode.Indent();
                CheckFailedOperations.Emit(sourceCode);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            public string ConditionID;
            public string RulePatternTypeName;
            public string[] NeededElements;
            public bool[] NeededElementIsNode;
        }

        /// <summary>
        /// Base class for search program operations 
        /// to execute upon candidate checking succeded
        /// (of the pattern part under construction)
        /// </summary>
        private abstract class AcceptIntoPartialMatch : SearchProgramOperation
        {
        }

        /// <summary>
        /// Class representing "write information to graph, to what pattern element 
        ///   is graph element mapped to, for isomorphy checking later on" operation
        /// </summary>
        private class AcceptIntoPartialMatchWriteIsomorphy : AcceptIntoPartialMatch
        {
            public AcceptIntoPartialMatchWriteIsomorphy(
                string patternElementName,
                string homomorphicID,
                bool positive,
                bool isNode)
            {
                PatternElementName = patternElementName;
                HomomorphicID = homomorphicID;
                Positive = positive;
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("AcceptIntoPartialMatch WriteIsomorphy ");
                builder.AppendFormat("on {0} hom-id:{1} positive:{2} node:{3}\n",
                    PatternElementName, HomomorphicID, Positive, IsNode);
            }

            /// <summary>
            /// Emits code for accept into partial match write isomorphy search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                string variableContainingCandidate = NameOfCandidateVariable(
                    PatternElementName, IsNode);
                string mappedToMember = Positive ? "mappedTo" : "negMappedTo";
                string variableContainingBackupOfMappedMember = NameOfVariableWithBackupOfMappedMember(
                    PatternElementName, IsNode, Positive);
                sourceCode.AppendFrontFormat("int {0} = {1}.{2};\n",
                    variableContainingBackupOfMappedMember, variableContainingCandidate, mappedToMember);
                sourceCode.AppendFrontFormat("{0}.{1} = {2};\n",
                    variableContainingCandidate, mappedToMember, HomomorphicID);
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public string PatternElementName;
            public string HomomorphicID; // homomorphy class
            public bool Positive; // positive|negative (mappedTo/negMappedTo)
            public bool IsNode; // node|edge
        }

        /// <summary>
        /// Base class for search program operations
        /// undoing effects of candidate acceptance 
        /// when performing the backtracking step
        /// (of the pattern part under construction)
        /// </summary>
        private abstract class WithdrawFromPartialMatch : SearchProgramOperation
        {
        }

        /// <summary>
        /// Class representing "remove information from graph, to what pattern element
        ///   is graph element mapped to, as not needed any more" operation
        /// </summary>
        private class WithdrawFromPartialMatchRemoveIsomorphy : WithdrawFromPartialMatch
        {
            public WithdrawFromPartialMatchRemoveIsomorphy(
                string patternElementName,
                bool positive,
                bool isNode)
            {
                PatternElementName = patternElementName;
                Positive = positive;
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("WithdrawFromPartialMatch RemoveIsomorphy ");
                builder.AppendFormat("on {0} positive:{1} node:{2}\n",
                    PatternElementName, Positive, IsNode);
            }

            /// <summary>
            /// Emits code for withdraw from partial match remove isomorphy search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                string variableContainingCandidate = NameOfCandidateVariable(
                    PatternElementName, IsNode);
                string mappedToMember = Positive ? "mappedTo" : "negMappedTo";
                string variableContainingBackupOfMappedMember = NameOfVariableWithBackupOfMappedMember(
                    PatternElementName, IsNode, Positive);
                sourceCode.AppendFrontFormat("{0}.{1} = {2};\n",
                    variableContainingCandidate, mappedToMember, variableContainingBackupOfMappedMember);
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public string PatternElementName;
            public bool Positive; // positive|negative (mappedTo/negMappedTo)
            public bool IsNode; // node|edge
        }

        /// <summary>
        /// Base class for search program operations
        /// to be executed when a partial match becomes a complete match
        /// (of the pattern part under construction)
        /// </summary>
        private abstract class PartialMatchComplete : SearchProgramOperation
        {
        }

        /// <summary>
        /// Class yielding operations to be executed 
        /// when a partial positive match becomes a complete match
        /// </summary>
        private class PartialMatchCompletePositive : PartialMatchComplete
        {
            public PartialMatchCompletePositive()
            {
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("PartialMatchComplete Positive \n");

                if (MatchBuildingOperations != null)
                {
                    builder.Indent();
                    MatchBuildingOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for partial match of positive pattern complete search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                sourceCode.AppendFront("LGSPMatch match = matchesList.GetNewMatch();\n");

                // emit match building operations
                MatchBuildingOperations.Emit(sourceCode);

                sourceCode.AppendFront("matchesList.CommitMatch();\n");
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public SearchProgramList MatchBuildingOperations;
        }

        /// <summary>
        /// Class yielding operations to be executed 
        /// when a partial negative match becomes a complete match
        /// </summary>
        private class PartialMatchCompleteNegative : PartialMatchComplete
        {
            public PartialMatchCompleteNegative()
            {
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("PartialMatchComplete Negative \n");
            }

            /// <summary>
            /// Emits code for partial match of negative pattern complete search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // nothing to emit
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }
        }

        /// <summary>
        /// Class representing "partial match is complete, now build match object" operation
        /// </summary>
        private class PartialMatchCompleteBuildMatchObject : PartialMatchComplete
        {
            public PartialMatchCompleteBuildMatchObject(
                string patternElementName,
                string matchIndex,
                bool isNode)
            {
                PatternElementName = patternElementName;
                MatchIndex = matchIndex;
                IsNode = isNode;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("PartialMatchComplete BuildMatchObject ");
                builder.AppendFormat("on {0} index:{1} node:{2}\n", 
                    PatternElementName, MatchIndex, IsNode);
            }

            /// <summary>
            /// Emits code for partial match complete build match object search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                string variableContainingCandidate = NameOfCandidateVariable(
                    PatternElementName, IsNode);
                string matchObjectElementMember =
                    IsNode ? "nodes" : "edges";
                sourceCode.AppendFrontFormat("match.{0}[{1}] = {2};\n",
                    matchObjectElementMember, MatchIndex,
                    variableContainingCandidate);
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public string PatternElementName;
            public string MatchIndex;
            public bool IsNode; // node|edge
        }

        /// <summary>
        /// Available types of AdjustListHeads operations
        /// </summary>
        private enum AdjustListHeadsTypes
        {
            GraphElements,
            IncidentEdges
        }

        /// <summary>
        /// Class representing "adjust list heads" operation ("listentrick")
        /// </summary>
        private class AdjustListHeads : SearchProgramOperation
        {
            public AdjustListHeads(
                AdjustListHeadsTypes type,
                string patternElementName,
                bool isNode)
            {
                Debug.Assert(type == AdjustListHeadsTypes.GraphElements);
                Type = type;
                PatternElementName = patternElementName;
                IsNode = isNode;
            }

            public AdjustListHeads(
                AdjustListHeadsTypes type,
                string patternElementName,
                string startingPointNodeName,
                bool isIncoming)
            {
                Debug.Assert(type == AdjustListHeadsTypes.IncidentEdges);
                Type = type;
                PatternElementName = patternElementName;
                StartingPointNodeName = startingPointNodeName;
                IsIncoming = isIncoming;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("AdjustListHeads ");
                if(Type==AdjustListHeadsTypes.GraphElements) {
                    builder.Append("GraphElements ");
                    builder.AppendFormat("on {0} node:{1}\n",
                        PatternElementName, IsNode);
                } else { // Type==AdjustListHeadsTypes.IncidentEdges
                    builder.Append("IncidentEdges ");
                    builder.AppendFormat("on {0} from:{1} incoming:{2}\n",
                        PatternElementName, StartingPointNodeName, IsIncoming);
                }
            }

            /// <summary>
            /// Emits code for adjust list heads search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if (Type == AdjustListHeadsTypes.GraphElements)
                {
                    sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                        NameOfCandidateVariable(PatternElementName, IsNode));
                }
                else //Type == AdjustListHeadsTypes.IncidentEdges
                {
                    if (IsIncoming)
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                            NameOfCandidateVariable(StartingPointNodeName, true),
                            NameOfCandidateVariable(PatternElementName, false));
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                            NameOfCandidateVariable(StartingPointNodeName, true),
                            NameOfCandidateVariable(PatternElementName, false));
                    }
                }
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public AdjustListHeadsTypes Type;

            public string PatternElementName;

            public bool IsNode; // node|edge - only valid if GraphElements

            public string StartingPointNodeName; // only valid if IncidentEdges
            public bool IsIncoming; // only valid if IncidentEdges
        }

        /// <summary>
        /// Base class for search program operations
        /// to check whether to continue the matching process 
        /// (of the pattern part under construction)
        /// </summary>
        private abstract class CheckContinueMatching : CheckOperation
        {
        }

        /// <summary>
        /// Class representing "check if matching process is to be aborted because
        /// the maximum number of matches has been reached" operation
        /// listHeadAdjustment==false prevents listentrick
        /// </summary>
        private class CheckContinueMatchingMaximumMatchesReached : CheckContinueMatching
        {
            public CheckContinueMatchingMaximumMatchesReached(bool listHeadAdjustment)
            {
                ListHeadAdjustment = listHeadAdjustment;
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached ");
                if (ListHeadAdjustment) {
                    builder.Append("ListHeadAdjustment\n");
                } else {
                    builder.Append("\n");
                }
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check whether to continue matching for are maximum matches reached search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                sourceCode.AppendFront("if(maxMatches > 0 && matchesList.Count >= maxMatches)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                CheckFailedOperations.Emit(sourceCode);

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            public bool ListHeadAdjustment;
        }

        /// <summary>
        /// Class representing check abort matching process operation
        /// which was determined at generation time to always succeed.
        /// Check of abort negative matching process always succeeds
        /// </summary>
        private class CheckContinueMatchingFailed : CheckContinueMatching
        {
            public CheckContinueMatchingFailed()
            {
            }

            public override void Dump(SourceBuilder builder)
            {
                // first dump check
                builder.AppendFront("CheckContinueMatching Failed \n");
                // then operations for case check failed
                if (CheckFailedOperations != null)
                {
                    builder.Indent();
                    CheckFailedOperations.Dump(builder);
                    builder.Unindent();
                }
            }

            /// <summary>
            /// Emits code for check whether to continue matching failed search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // nothing locally, just emit check failed code
                CheckFailedOperations.Emit(sourceCode);
            }
        }

        /// <summary>
        /// Available types of ContinueOperation operations
        /// </summary>
        private enum ContinueOperationType
        {
            ByReturn,
            ByContinue,
            ByGoto
        }

        /// <summary>
        /// Class representing "continue matching there" control flow operations
        /// </summary>
        private class ContinueOperation : SearchProgramOperation
        {
            public ContinueOperation(ContinueOperationType type,
                bool returnMatches)
            {
                Debug.Assert(type == ContinueOperationType.ByReturn);
                Type = type;
                ReturnMatches = returnMatches;
            }

            public ContinueOperation(ContinueOperationType type)
            {
                Debug.Assert(type == ContinueOperationType.ByContinue);
                Type = type;
            }

            public ContinueOperation(ContinueOperationType type,
                string labelName)
            {
                Debug.Assert(type == ContinueOperationType.ByGoto);
                Type = type;
                LabelName = labelName;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("ContinueOperation ");
                if(Type==ContinueOperationType.ByReturn) {
                    builder.Append("ByReturn ");
                    builder.AppendFormat("return matches:{0}\n", ReturnMatches);
                } else if(Type==ContinueOperationType.ByContinue) {
                    builder.Append("ByContinue\n");
                } else { // Type==ContinueOperationType.ByGoto
                    builder.Append("ByGoto ");
                    builder.AppendFormat("{0}\n", LabelName);
                }
            }

            /// <summary>
            /// Emits code for continue search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                if (Type == ContinueOperationType.ByReturn)
                {
                    if (ReturnMatches)
                    {
                        sourceCode.AppendFront("return matches;\n");
                    }
                    else
                    {
                        sourceCode.AppendFront("return;\n");
                    }
                }
                else if (Type == ContinueOperationType.ByContinue)
                {
                    sourceCode.AppendFront("continue;\n");
                }
                else //Type == ContinueOperationType.ByGoto
                {
                    sourceCode.AppendFrontFormat("goto {0};\n", LabelName);
                }
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public ContinueOperationType Type;

            public bool ReturnMatches; // only valid if ByReturn
            public string LabelName; // only valid if ByGoto
        }

        /// <summary>
        /// Class representing location within code named with label,
        /// potential target of goto operation
        /// </summary>
        private class GotoLabel : SearchProgramOperation
        {
            public GotoLabel()
            {
                LabelName = "label" + labelId.ToString();
                ++labelId;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("Goto Label ");
                builder.AppendFormat("{0}\n", LabelName);
            }

            /// <summary>
            /// Emits code for goto label search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                sourceCode.AppendFormat("{0}: ;\n", LabelName);
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public string LabelName;

            private static int labelId = 0;
        }

        /// <summary>
        /// Available types of RandomizeListHeads operations
        /// </summary>
        private enum RandomizeListHeadsTypes
        {
            GraphElements,
            IncidentEdges
        }

        /// <summary>
        /// Class representing "adjust list heads" operation ("listentrick")
        /// </summary>
        private class RandomizeListHeads : SearchProgramOperation
        {
            public RandomizeListHeads(
                RandomizeListHeadsTypes type,
                string patternElementName,
                bool isNode)
            {
                Debug.Assert(type == RandomizeListHeadsTypes.GraphElements);
                Type = type;
                PatternElementName = patternElementName;
                IsNode = isNode;
            }

            public RandomizeListHeads(
                RandomizeListHeadsTypes type,
                string patternElementName,
                string startingPointNodeName,
                bool isIncoming)
            {
                Debug.Assert(type == RandomizeListHeadsTypes.IncidentEdges);
                Type = type;
                PatternElementName = patternElementName;
                StartingPointNodeName = startingPointNodeName;
                IsIncoming = isIncoming;
            }

            public override void Dump(SourceBuilder builder)
            {
                builder.AppendFront("RandomizeListHeads ");
                if (Type == RandomizeListHeadsTypes.GraphElements)
                {
                    builder.Append("GraphElements ");
                    builder.AppendFormat("on {0} node:{1}\n",
                        PatternElementName, IsNode);
                }
                else
                { // Type==RandomizeListHeadsTypes.IncidentEdges
                    builder.Append("IncidentEdges ");
                    builder.AppendFormat("on {0} from:{1} incoming:{2}\n",
                        PatternElementName, StartingPointNodeName, IsIncoming);
                }
            }

            /// <summary>
            /// Emits code for randomize list heads search program operation
            /// </summary>
            public override void Emit(SourceBuilder sourceCode)
            {
                // --- move list head from current position to random position ---

                if (Type == RandomizeListHeadsTypes.GraphElements)
                {
                    // emit declaration of variable containing random position to move list head to
                    string variableContainingRandomPosition =
                        "random_position_" + PatternElementName;
                    sourceCode.AppendFormat("int {0}", variableContainingRandomPosition);
                    // emit initialization with ramdom position
                    string graphMemberContainingElementListCountsByType =
                        IsNode ? "nodesByTypeCounts" : "edgesByTypeCounts";
                    string variableContainingTypeIDForCandidate = NameOfTypeIdForCandidateVariable(
                            PatternElementName, IsNode);
                    sourceCode.AppendFormat(" = random.Next(graph.{0}[{1}]);\n",
                        graphMemberContainingElementListCountsByType,
                        variableContainingTypeIDForCandidate);
                    // emit declaration of variable containing element at random position
                    string typeOfVariableContainingElementAtRandomPosition =
                        IsNode ? "LGSPNode" : "LGSPEdge";
                    string variableContainingElementAtRandomPosition =
                        "random_element_" + PatternElementName;
                    sourceCode.AppendFrontFormat("{0} {1}",
                        typeOfVariableContainingElementAtRandomPosition,
                        variableContainingElementAtRandomPosition);
                    // emit initialization with element list head
                    string graphMemberContainingElementListHeadByType =
                        IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                    sourceCode.AppendFormat(" = graph.{0}[{1}];\n",
                        graphMemberContainingElementListHeadByType, variableContainingTypeIDForCandidate);
                    // emit iteration to get element at random position
                    sourceCode.AppendFrontFormat(
                        "for(int i = 0; i < {0}; ++i) {1} = {1}.Next;\n",
                        variableContainingRandomPosition, variableContainingElementAtRandomPosition);
                    // iteration left, element is the one at the requested random position
                    // move list head after element at random position, 
                    sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                        variableContainingElementAtRandomPosition);
                    // effect is new random starting point for following iteration
                }
                else //Type == RandomizeListHeadsTypes.IncidentEdges
                {
                    // emit "randomization only if list is not empty"
                    string variableContainingStartingPointNode = NameOfCandidateVariable(
                        StartingPointNodeName, true);
                    string memberOfNodeContainingListHead =
                        IsIncoming ? "inhead" : "outhead";
                    sourceCode.AppendFrontFormat("if({0}.{1}!=null)\n",
                        variableContainingStartingPointNode, memberOfNodeContainingListHead);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration of variable containing random position to move list head to, initialize it to 0 
                    string variableContainingRandomPosition =
                        "random_position_" + PatternElementName;
                    sourceCode.AppendFrontFormat("int {0} = 0;", variableContainingRandomPosition);
                    // misuse variable to store length of list which is computed within the follwing iteration
                    string memberOfEdgeContainingNextEdge =
                        IsIncoming ? "inNext" : "outNext";
                    sourceCode.AppendFrontFormat("for(LGSPEdge edge = {0}.{1}; edge!={0}.{1}; edge=edge.{2}) ++{3};\n",
                        variableContainingStartingPointNode, memberOfNodeContainingListHead,
                        memberOfEdgeContainingNextEdge, variableContainingRandomPosition);
                    // emit initialization of variable containing ramdom position
                    // now that the necessary length of the list is known after the iteration
                    // given in the variable itself
                    sourceCode.AppendFrontFormat("{0} = random.Next({0});\n",
                        variableContainingRandomPosition);
                    // emit declaration of variable containing edge at random position
                    string variableContainingEdgeAtRandomPosition =
                        "random_element_" + PatternElementName;
                    sourceCode.AppendFrontFormat("LGSPEdge {0}",
                        variableContainingEdgeAtRandomPosition);
                    // emit initialization with edge list head
                    sourceCode.AppendFormat(" = {0}.{1};\n",
                        variableContainingStartingPointNode, memberOfNodeContainingListHead);
                    // emit iteration to get edge at random position
                    sourceCode.AppendFrontFormat(
                        "for(int i = 0; i < {0}; ++i) {1} = {1}.{2};\n",
                        variableContainingRandomPosition,
                        variableContainingEdgeAtRandomPosition,
                        memberOfEdgeContainingNextEdge);
                    // iteration left, edge is the one at the requested random position
                    // move list head after edge at random position, 
                    if (IsIncoming)
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                            variableContainingStartingPointNode,
                            variableContainingEdgeAtRandomPosition);
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                            variableContainingStartingPointNode,
                            variableContainingEdgeAtRandomPosition);
                    }

                    // close list is not empty check
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // effect is new random starting point for following iteration
                }
            }

            public override bool IsNestingOperation()
            {
                return false;
            }

            public override SearchProgramOperation GetNestedOperationsList()
            {
                return null;
            }

            public RandomizeListHeadsTypes Type;

            public string PatternElementName;

            public bool IsNode; // node|edge - only valid if GraphElements

            public string StartingPointNodeName; // only valid if IncidentEdges
            public bool IsIncoming; // only valid if IncidentEdges
        }

        /// <summary>
        /// class of environment for build pass
        /// </summary>
        private struct EnvironmentForBuild
        {
            /// <summary>
            /// the scheduled search plan to build
            /// </summary>
            public ScheduledSearchPlan ScheduledSearchPlan;
            /// <summary>
            /// name of the rule pattern type of the rule pattern that is built
            /// </summary>
            public string NameOfRulePatternType;
            /// <summary>
            /// the innermost enclosing positive candidate iteration operation 
            /// at the current insertion point of the nested negative pattern
            /// not null if negative pattern is currently built, null otherwise
            /// </summary>
            public SearchProgramOperation EnclosingPositiveOperation;
            /// <summary>
            /// the rule pattern that is built 
            /// (it's pattern graph is needed for match object building)
            /// </summary>
            public LGSPRulePattern RulePattern;
        }
        /// <summary>
        /// environment is global variable only for build process
        /// </summary>
        private EnvironmentForBuild environmentForBuild;

        /// <summary>
        /// Builds search program from scheduled search plan
        /// </summary>
        /// <param name="scheduledSearchPlan"></param>
        /// <param name="nameOfRulePatternType"></param>
        /// <returns></returns>
        private SearchProgram BuildSearchProgram(
            ScheduledSearchPlan scheduledSearchPlan,
            string nameOfSearchProgram,
            List<string> parametersList,
            List<bool> parameterIsNodeList,
            LGSPRulePattern rulePattern)
        {
            string[] parameters = null;
            bool[] parameterIsNode = null;
            if (parametersList != null)
            {
                parameters = new string[parametersList.Count];
                parameterIsNode = new bool[parameterIsNodeList.Count];
                int i = 0;
                foreach(string p in parametersList)
                {
                    parameters[i] = p;
                    ++i;
                }
                i = 0;
                foreach(bool pis in parameterIsNodeList)
                {
                    parameterIsNode[i] = pis;
                    ++i;
                }
            }

            SearchProgram searchProgram = new SearchProgram(
                nameOfSearchProgram, parameters, parameterIsNode);
            searchProgram.OperationsList = new SearchProgramList(searchProgram);

            environmentForBuild.ScheduledSearchPlan = scheduledSearchPlan;
            environmentForBuild.NameOfRulePatternType = rulePattern.GetType().Name;
            environmentForBuild.EnclosingPositiveOperation = null;
            environmentForBuild.RulePattern = rulePattern;

            // start building with first operation in scheduled search plan
            BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                searchProgram.OperationsList);

            return searchProgram;
        }

        /// <summary>
        /// Builds search program operations from scheduled search plan operation.
        /// Decides which specialized build procedure is to be called.
        /// The specialized build procedure then calls this procedure again, 
        /// in order to process the next search plan operation.
        /// </summary>
        private SearchProgramOperation BuildScheduledSearchPlanOperationIntoSearchProgram(
            int indexOfScheduledSearchPlanOperationToBuild,
            SearchProgramOperation insertionPointWithinSearchProgram)
        {
            if (indexOfScheduledSearchPlanOperationToBuild >=
                environmentForBuild.ScheduledSearchPlan.Operations.Length)
            { // end of scheduled search plan reached, stop recursive iteration
                return buildMatchComplete(insertionPointWithinSearchProgram);
            }

            SearchOperation op = environmentForBuild.ScheduledSearchPlan.
                Operations[indexOfScheduledSearchPlanOperationToBuild];
        
            // for current scheduled search plan operation 
            // insert corresponding search program operations into search program
            switch (op.Type)
            {
                case SearchOperationType.Void:
                    return BuildScheduledSearchPlanOperationIntoSearchProgram(
                        indexOfScheduledSearchPlanOperationToBuild + 1,
                        insertionPointWithinSearchProgram);

                case SearchOperationType.MaybePreset:
                    return buildMaybePreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.NegPreset:
                    return buildNegPreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.Lookup:
                    return buildLookup(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.ImplicitSource:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        true);

                case SearchOperationType.ImplicitTarget:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        false);

                case SearchOperationType.Incoming:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        true);

                case SearchOperationType.Outgoing:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        false);

                case SearchOperationType.NegativePattern:
                    return buildNegative(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (ScheduledSearchPlan)op.Element);

                case SearchOperationType.Condition:
                    return buildCondition(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (Condition)op.Element);

                default:
                    Debug.Assert(false, "Unknown search operation");
                    return insertionPointWithinSearchProgram;
            }
        }

        /// <summary>
        /// Search program operations implementing the
        /// MaybePreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildMaybePreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            Debug.Assert(positive, "Positive maybe preset in negative search plan");

            // get candidate from inputs
            GetCandidateByDrawing fromInputs =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromInputs,
                    target.PatternElement.Name,
                    target.PatternElement.ParameterIndex.ToString(),
                    isNode);
            insertionPoint = insertionPoint.Append(fromInputs);

            // check whether candidate was preset (not null)
            // continues with missing preset searching and continuing search program on fail
            CheckCandidateForPreset checkPreset = new CheckCandidateForPreset(
                target.PatternElement.Name,
                isNode);
            insertionPoint = insertionPoint.Append(checkPreset);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex+1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// NegPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            Debug.Assert(!positive, "Negative preset in positive search plan");

            // check candidate for isomorphy 
            if (isomorphy.MustCheckMapped)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        false,
                        isNode);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped)
            {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        false,
                        isNode);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        false,
                        isNode);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Lookup search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildLookup(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex, 
            SearchPlanNode target, 
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            
            // decide on and insert operation determining type of candidate
            SearchProgramOperation continuationPointAfterTypeIteration;
            SearchProgramOperation insertionPointAfterTypeIteration = 
                decideOnAndInsertGetType(insertionPoint, target, 
                out continuationPointAfterTypeIteration);
            insertionPoint = insertionPointAfterTypeIteration;

#if RANDOM_LOOKUP_LIST_START
            // insert list heads randomization, thus randomized lookup
            RandomizeListHeads randomizeListHeads =
                new RandomizeListHeads(
                    RandomizeListHeadsTypes.GraphElements,
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(randomizeListHeads);
#endif

            // iterate available graph elements
            GetCandidateByIteration elementsIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.GraphElements,
                    target.PatternElement.Name,
                    isNode);
            SearchProgramOperation continuationPoint = 
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList = 
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check connectedness of candidate
            if (isNode) {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNode(
                    insertionPoint, (SearchPlanNodeNode)target, null);
            } else {
                bool dontcare = true||false;
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdge(
                    insertionPoint, (SearchPlanEdgeNode)target, null, dontcare);
            }

            // check candidate for isomorphy 
            if (isomorphy.MustCheckMapped) {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped) {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list at type iteration nesting level
            insertionPoint = continuationPoint;

            // everything nested within type iteration built by now
            // continue at the end of the list handed in
            if (insertionPointAfterTypeIteration != continuationPointAfterTypeIteration)
            {
                // if type was drawn then the if is not entered (insertion point==continuation point)
                // thus we continue at the continuation point of the candidate iteration
                // otherwise if type was iterated
                // we continue at the continuation point of the type iteration
                insertionPoint = continuationPointAfterTypeIteration;
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Implicit source|target search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildImplicit(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanEdgeNode source,
            SearchPlanNodeNode target,
            IsomorphyInformation isomorphy,
            bool getSource)
        {
            // get candidate = demanded node from edge
            GetCandidateByDrawing nodeFromEdge = 
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.NodeFromEdge,
                    target.PatternElement.Name,
                    model.NodeModel.Types[target.PatternElement.TypeID].Name,
                    source.PatternElement.Name,
                    getSource);
            insertionPoint = insertionPoint.Append(nodeFromEdge);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfNode(
                insertionPoint, target, source);

            // check candidate for isomorphy 
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            if (isomorphy.MustCheckMapped) {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped) {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Extend Incoming|Outgoing search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildIncident(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNodeNode source,
            SearchPlanEdgeNode target,
            IsomorphyInformation isomorphy,
            bool getIncoming)
        {
#if RANDOM_LOOKUP_LIST_START
            // insert list heads randomization, thus randomized extend
            RandomizeListHeads randomizeListHeads =
                new RandomizeListHeads(
                    RandomizeListHeadsTypes.IncidentEdges,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    getIncoming);
            insertionPoint = insertionPoint.Append(randomizeListHeads);
#endif

            // iterate available incident edges
            GetCandidateByIteration incidentIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.IncidentEdges,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    getIncoming);
            SearchProgramOperation continuationPoint = 
                insertionPoint.Append(incidentIteration);
            incidentIteration.NestedOperationsList =
                new SearchProgramList(incidentIteration);
            insertionPoint = incidentIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfEdge(
                insertionPoint, target, source, getIncoming);

            // check candidate for isomorphy 
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            if (isomorphy.MustCheckMapped) {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped) {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            // everything nested within incident iteration built by now -
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Negative search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegative(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            ScheduledSearchPlan negativeScheduledSearchPlan)
        {
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            Debug.Assert(positive, "Nested negative");

            // fill needed elements array for CheckPartialMatchByNegative
            int numberOfNeededElements = 0;
            foreach (SearchOperation op in negativeScheduledSearchPlan.Operations) {
                if (op.Type == SearchOperationType.NegPreset) {
                    ++numberOfNeededElements;
                }
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach (SearchOperation op in negativeScheduledSearchPlan.Operations) {
                if (op.Type == SearchOperationType.NegPreset) {
                    SearchPlanNode element = ((SearchPlanNode)op.Element);
                    neededElements[i] = element.PatternElement.Name;
                    ++i;
                }
            }

            CheckPartialMatchByNegative checkNegative =
               new CheckPartialMatchByNegative(neededElements);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(checkNegative);

            checkNegative.NestedOperationsList = 
                new SearchProgramList(checkNegative);
            insertionPoint = checkNegative.NestedOperationsList;

            environmentForBuild.EnclosingPositiveOperation =
                GetEnclosingOperation(checkNegative);
            ScheduledSearchPlan positiveScheduledSearchPlan =
                environmentForBuild.ScheduledSearchPlan;
            environmentForBuild.ScheduledSearchPlan = negativeScheduledSearchPlan;

            //---------------------------------------------------------------------------
            // build negative pattern
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);
            //---------------------------------------------------------------------------

            // negative pattern built by now
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;
            environmentForBuild.EnclosingPositiveOperation = null;
            environmentForBuild.ScheduledSearchPlan = positiveScheduledSearchPlan;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Condition search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildCondition(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            Condition condition)
        {
            // check condition with current partial match
            CheckPartialMatchByCondition checkCondition =
                new CheckPartialMatchByCondition(condition.ID.ToString(),
                    environmentForBuild.NameOfRulePatternType, 
                    condition.NeededNodes, 
                    condition.NeededEdges);
            insertionPoint = insertionPoint.Append(checkCondition);

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations completing the matching process
        /// after all pattern elements have been found 
        /// are created and inserted into the program
        /// </summary>
        private SearchProgramOperation buildMatchComplete(
            SearchProgramOperation insertionPoint)
        {
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;

            if (positive)
            {
                // build the match complete operation signaling pattern was found
                PartialMatchCompletePositive matchComplete =
                    new PartialMatchCompletePositive();
                SearchProgramOperation continuationPoint =
                    insertionPoint.Append(matchComplete);
                matchComplete.MatchBuildingOperations =
                    new SearchProgramList(matchComplete);
                insertionPoint = matchComplete.MatchBuildingOperations;

                // fill the match object with the candidates 
                // which have passed all the checks for being a match
                LGSPRulePattern rulePattern = environmentForBuild.RulePattern;
                PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;
                for (int i = 0; i < patternGraph.nodes.Length; i++)
                {
                    PartialMatchCompleteBuildMatchObject buildMatch =
                        new PartialMatchCompleteBuildMatchObject(
                            patternGraph.nodes[i].Name,
                            i.ToString(),
                            true);
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
                for (int i = 0; i < patternGraph.edges.Length; i++)
                {
                    PartialMatchCompleteBuildMatchObject buildMatch =
                        new PartialMatchCompleteBuildMatchObject(
                            patternGraph.edges[i].Name,
                            i.ToString(),
                            false);
                    insertionPoint = insertionPoint.Append(buildMatch);
                }

                // check wheter to continue the matching process
                // or abort because the maximum desired number of maches was reached
                CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
#if NO_ADJUST_LIST_HEADS
                    new CheckContinueMatchingMaximumMatchesReached(false);
#else
                    new CheckContinueMatchingMaximumMatchesReached(true);
#endif
                insertionPoint = continuationPoint.Append(checkMaximumMatches);

                return insertionPoint;
            }
            else
            {
                // build the match complete operation signaling negative patter was found
                PartialMatchCompleteNegative matchComplete =
                    new PartialMatchCompleteNegative();
                insertionPoint = insertionPoint.Append(matchComplete);

                // abort the matching process
                CheckContinueMatchingFailed abortMatching =
                    new CheckContinueMatchingFailed();
                insertionPoint = insertionPoint.Append(abortMatching);

                return insertionPoint;
            }
        }

        /// <summary>
        /// Decides which get type operation to use and inserts it
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within type iteration was built
        /// if type drawing was sufficient, insertion point == continuation point
        /// </summary>
        private SearchProgramOperation decideOnAndInsertGetType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target,
            out SearchProgramOperation continuationPoint)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? model.NodeModel : model.EdgeModel;

            if (target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed
                if (typeModel.Types[target.PatternElement.TypeID].HasSubTypes)
                { // more than the type itself -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.AllCompatible,
                            target.PatternElement.Name,
                            typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                            isNode);
                    continuationPoint = insertionPoint.Append(typeIteration);
                    
                    typeIteration.NestedOperationsList = 
                        new SearchProgramList(typeIteration);
                    insertionPoint = typeIteration.NestedOperationsList;                    
                }
                else
                { // only the type itself -> no iteration needed
                    GetTypeByDrawing typeDrawing =
                        new GetTypeByDrawing(
                            target.PatternElement.Name,
                            target.PatternElement.TypeID.ToString(),
                            isNode);
                    insertionPoint = insertionPoint.Append(typeDrawing);
                    continuationPoint = insertionPoint;
                }
            }
            else //(target.PatternElement.AllowedTypes != null)
            { // the allowed types are given explicitely
                if (target.PatternElement.AllowedTypes.Length != 1)
                { // more than one allowed type -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.ExplicitelyGiven,
                            target.PatternElement.Name,
                            environmentForBuild.NameOfRulePatternType,
                            isNode);
                    continuationPoint = insertionPoint.Append(typeIteration);

                    typeIteration.NestedOperationsList =
                        new SearchProgramList(typeIteration);
                    insertionPoint = typeIteration.NestedOperationsList;
                }
                else
                { // only one allowed type -> no iteration needed
                    GetTypeByDrawing typeDrawing =
                        new GetTypeByDrawing(
                            target.PatternElement.Name,
                            target.PatternElement.AllowedTypes[0].TypeID.ToString(),
                            isNode);
                    insertionPoint = insertionPoint.Append(typeDrawing);
                    continuationPoint = insertionPoint;
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check type operation to build and inserts it into search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? model.NodeModel : model.EdgeModel;

            if (target.PatternElement.IsAllowedType != null)
            { // the allowed types are given by an array for checking against them
                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByIsAllowedType,
                        target.PatternElement.Name,
                        environmentForBuild.NameOfRulePatternType,
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);

                return insertionPoint;
            }

            if (target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed

                if (typeModel.Types[target.PatternElement.TypeID] == typeModel.RootType)
                { // every type matches the root type == element type -> no check needed
                    return insertionPoint;
                }

                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByIsMyType,
                        target.PatternElement.Name,
                        typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);
                
                return insertionPoint;
            }

            // the allowed types are given by allowed types array
            // if there are multiple ones, is allowed types must have been not null before
            Debug.Assert(target.PatternElement.AllowedTypes.Length > 1, "More than one allowed type");

            if (target.PatternElement.AllowedTypes.Length == 0) 
            { // no type allowed
                CheckCandidateFailed checkFailed = new CheckCandidateFailed();
                insertionPoint = insertionPoint.Append(checkFailed);
            }
            else // (target.PatternElement.AllowedTypes.Length == 1)
            { // only one type allowed
                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByAllowedTypes,
                        target.PatternElement.Name,
                        target.PatternElement.AllowedTypes[0].TypeID.ToString(),
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfNode(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            SearchPlanEdgeNode originatingEdge)
        {
            // check whether incoming/outgoing-edges of the candidate node 
            // are the same as the already found edges to which the node must be incident
            // only for the edges required by the pattern to be incident with the node
            // only if edge is already matched by now (signaled by visited)
            // only if the node was not taken from the given originating edge
            foreach (SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if (edge.Visited)
                {
                    if (edge != originatingEdge)
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                node.PatternElement.Name,
                                node.PatternElement.Name,
                                edge.PatternElement.Name,
                                true);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }
            foreach (SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if (edge.Visited)
                {
                    if (edge != originatingEdge)
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                node.PatternElement.Name,
                                node.PatternElement.Name,
                                edge.PatternElement.Name,
                                false);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdge(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode originatingNode,
            bool edgeIncomingAtOriginatingNode)
        {
            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            // don't need to, if the edge is not required by the pattern 
            //  to be incident to some given node
            // or that node is not matched by now (signaled by visited)
            // or if the edge was taken from the given originating node
            if (edge.PatternEdgeSource != null)
            {
                if (edge.PatternEdgeSource.Visited)
                {
                    if (!(!edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeSource == originatingNode))
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                edge.PatternElement.Name,
                                edge.PatternEdgeSource.PatternElement.Name,
                                edge.PatternElement.Name,
                                true);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            if (edge.PatternEdgeTarget != null)
            {
                if (edge.PatternEdgeTarget.Visited)
                {
                    if (!(edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeTarget == originatingNode))
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                edge.PatternElement.Name,
                                edge.PatternEdgeTarget.PatternElement.Name,
                                edge.PatternElement.Name,
                                false);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Create an extra search subprogram per MaybePreset operation.
        /// Created search programs are added to search program next field, forming list.
        /// Destroys the scheduled search program.
        /// </summary>
        private void BuildAddionalSearchSubprograms(ScheduledSearchPlan scheduledSearchPlan,
            SearchProgram searchProgram, LGSPRulePattern rulePattern)
        {
            // insertion point for next search subprogram
            SearchProgramOperation insertionPoint = searchProgram;
            // for stepwise buildup of parameter lists
            List<string> neededElementsInRemainderProgram = new List<string>();
            List<bool> neededElementInRemainderProgramIsNode = new List<bool>();

            foreach (SearchOperation op in scheduledSearchPlan.Operations)
            {
                switch(op.Type)
                {
                // candidates bound in the program up to the maybe preset operation
                // are handed in to the "lookup missing element and continue"
                // remainder search program as parameters 
                // - so they don't need to be searched any more
                case SearchOperationType.Lookup:
                case SearchOperationType.Outgoing:
                case SearchOperationType.Incoming:
                case SearchOperationType.ImplicitSource:
                case SearchOperationType.ImplicitTarget:
                    // don't search
                    op.Type = SearchOperationType.Void;
                    // make parameter in remainder program
                    SearchPlanNode element = (SearchPlanNode)op.Element;
                    neededElementsInRemainderProgram.Add(element.PatternElement.Name);
                    neededElementInRemainderProgramIsNode.Add(element.NodeType == PlanNodeType.Node);
                    break;
                // check operations were already executed in the calling program 
                // they don't need any treatement in the remainder search program
                case SearchOperationType.NegativePattern:
                case SearchOperationType.Condition:
                    op.Type = SearchOperationType.Void;
                    break;
                // the maybe preset operation splitting off an extra search program
                // to look the missing element up and continue within that 
                // remainder search program until the end (or the next split)
                // replace the maybe preset by a lookup within that program
                // and build the extra program (with the rest unmodified (yet))
                // insert the search program into the search program list
                case SearchOperationType.MaybePreset:
                    // change maybe preset on element which was not handed in  
                    // into lookup on element, then build search program with lookup
                    op.Type = SearchOperationType.Lookup;
                    element = (SearchPlanNode)op.Element;
                    SearchProgramOperation searchSubprogram =
                        BuildSearchProgram(
                            scheduledSearchPlan,
                            NameOfMissingPresetHandlingMethod(element.PatternElement.Name),
                            neededElementsInRemainderProgram,
                            neededElementInRemainderProgramIsNode,
                            rulePattern);
                    insertionPoint = insertionPoint.Append(searchSubprogram);
                    // search calls to this new search program and complete arguments in
                    CompleteCallsToMissingPresetHandlingMethodInAllSearchPrograms(
                        searchProgram,
                        NameOfMissingPresetHandlingMethod(element.PatternElement.Name),
                        neededElementsInRemainderProgram,
                        neededElementInRemainderProgramIsNode);
                    // make parameter in remainder program
                    neededElementsInRemainderProgram.Add(element.PatternElement.Name);
                    neededElementInRemainderProgramIsNode.Add(element.NodeType == PlanNodeType.Node);
                    // handled, now do same as with other candidate binding operations
                    op.Type = SearchOperationType.Void;
                    break;
                // operations which are not allowed in a positive scheduled search plan
                // after ssp creation and/or the buildup pass
                case SearchOperationType.Void:
                case SearchOperationType.NegPreset:
                default:
                    Debug.Assert(false, "At this pass/position not allowed search operation");
                    break;                    
                }
            }
        }

        /// <summary>
        /// Completes calls to missing preset handling methods in all search programs
        ///   (available at the time the functions is called)
        /// </summary>
        private void CompleteCallsToMissingPresetHandlingMethodInAllSearchPrograms(
            SearchProgram searchProgram,
            string nameOfMissingPresetHandlingMethod,
            List<string> arguments,
            List<bool> argumentIsNode)
        {
            /// Iterate all search programs 
            /// search for calls to missing preset handling methods 
            /// within check preset opertions which are the only ones utilizing them
            /// and complete them with the arguments given
            do
            {
                CompleteCallsToMissingPresetHandlingMethod(
                    searchProgram.GetNestedOperationsList(),
                    nameOfMissingPresetHandlingMethod,
                    arguments,
                    argumentIsNode);
                searchProgram = searchProgram.Next as SearchProgram;
            }
            while (searchProgram != null);
        }

        /// <summary>
        /// Completes calls to missing preset handling methods 
        /// by inserting the initially not given arguments
        /// </summary>
        private void CompleteCallsToMissingPresetHandlingMethod(
            SearchProgramOperation searchProgramOperation,
            string nameOfMissingPresetHandlingMethod,
            List<string> arguments,
            List<bool> argumentIsNode)
        {
            // complete calls with arguments 
            // find them in depth first search of search program
            while (searchProgramOperation != null)
            {
                if (searchProgramOperation is CheckCandidateForPreset)
                {
                    CheckCandidateForPreset checkPreset = 
                        (CheckCandidateForPreset) searchProgramOperation;
                    if (NameOfMissingPresetHandlingMethod(checkPreset.PatternElementName)
                        == nameOfMissingPresetHandlingMethod)
                    {
                        checkPreset.CompleteWithArguments(arguments, argumentIsNode);
                    }
                }
                else if (searchProgramOperation.IsNestingOperation())
                { // depth first
                    CompleteCallsToMissingPresetHandlingMethod(
                        searchProgramOperation.GetNestedOperationsList(),
                        nameOfMissingPresetHandlingMethod, 
                        arguments,
                        argumentIsNode);
                }

                // breadth
                searchProgramOperation = searchProgramOperation.Next;
            }

        }

        /// <summary>
        /// Iterate all search programs to complete check operations within each one
        /// </summary>
        private void CompleteCheckOperationsInAllSearchPrograms(
            SearchProgram searchProgram)
        {
            do
            {
                CompleteCheckOperations(
                    searchProgram.GetNestedOperationsList(),
                    searchProgram, 
                    null);
                searchProgram = searchProgram.Next as SearchProgram;
            }
            while (searchProgram != null);
        }   

        /// <summary>
        /// Completes check operations in search program from given currentOperation on
        /// (taking borderlines set by enclosing search program and check negative into account)
        /// Completion:
        /// - determine continuation point
        /// - insert remove isomorphy opertions needed for continuing there
        /// - insert continuing operation itself
        /// </summary>
        private void CompleteCheckOperations(
            SearchProgramOperation currentOperation,
            SearchProgramOperation enclosingSearchProgram,
            CheckPartialMatchByNegative enclosingCheckNegative)
        {
            // mainly dispatching and iteration method, traverses search program
            // real completion done in MoveOutwardsAppendingRemoveIsomorphyAndJump
            // outermost operation for that function is computed here, regarding negative patterns

            // complete check operations by inserting failure code
            // find them in depth first search of search program
            while (currentOperation != null)
            {
                //////////////////////////////////////////////////////////
                if (currentOperation is CheckCandidate)
                //////////////////////////////////////////////////////////
                {
                    if (currentOperation is CheckCandidateForPreset)
                    {
                        // when the call to the preset handling returns
                        // it might be because it found all available matches from the given parameters on
                        //   then we want to continue the matching
                        // or it might be because it found the required number of matches
                        //   then we want to abort the matching process
                        // so we have to add an additional maximum matches check to the check preset
                        CheckCandidateForPreset checkPreset =
                            (CheckCandidateForPreset)currentOperation;
                        checkPreset.CheckFailedOperations = 
                            new SearchProgramList(checkPreset);
                        CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                            new CheckContinueMatchingMaximumMatchesReached(false);
                        checkPreset.CheckFailedOperations.Append(checkMaximumMatches);

                        string[] neededElementsForCheckOperation = new string[1];
                        neededElementsForCheckOperation[0] = checkPreset.PatternElementName;
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkPreset,
                            neededElementsForCheckOperation,
                            enclosingCheckNegative ?? enclosingSearchProgram);

                        // unlike all other check operations with their flat check failed code
                        // check preset has a further check operations nested within check failed code
                        // give it its special bit of attention here
                        CompleteCheckOperations(checkPreset.CheckFailedOperations,
                            enclosingSearchProgram,
                            enclosingCheckNegative);
                    }
                    else
                    {
                        CheckCandidate checkCandidate =
                            (CheckCandidate)currentOperation;
                        checkCandidate.CheckFailedOperations =
                            new SearchProgramList(checkCandidate);
                        string[] neededElementsForCheckOperation = new string[1];
                        neededElementsForCheckOperation[0] = checkCandidate.PatternElementName;
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkCandidate,
                            neededElementsForCheckOperation,
                            enclosingCheckNegative ?? enclosingSearchProgram);
                    }
                }
                //////////////////////////////////////////////////////////
                else if (currentOperation is CheckPartialMatch)
                //////////////////////////////////////////////////////////
                {
                    if (currentOperation is CheckPartialMatchByCondition)
                    {
                        CheckPartialMatchByCondition checkCondition = 
                            (CheckPartialMatchByCondition)currentOperation;
                        checkCondition.CheckFailedOperations =
                            new SearchProgramList(checkCondition);
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkCondition,
                            checkCondition.NeededElements,
                            enclosingCheckNegative ?? enclosingSearchProgram);
                    }
                    else if (currentOperation is CheckPartialMatchByNegative)
                    {
                        CheckPartialMatchByNegative checkNegative =
                            (CheckPartialMatchByNegative)currentOperation;

                        // ByNegative is handled in CheckContinueMatchingFailed 
                        // of the negative case - enter negative case
                        CompleteCheckOperations(checkNegative.NestedOperationsList,
                            enclosingCheckNegative ?? enclosingSearchProgram, 
                            checkNegative);
                    }
                    else
                    {
                        Debug.Assert(false, "unknown check partial match operation");
                    }

                }
                //////////////////////////////////////////////////////////
                else if (currentOperation is CheckContinueMatching)
                //////////////////////////////////////////////////////////
                {
                    if(currentOperation is CheckContinueMatchingMaximumMatchesReached)
                    {
                        CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                            (CheckContinueMatchingMaximumMatchesReached)currentOperation;
                        checkMaximumMatches.CheckFailedOperations =
                            new SearchProgramList(checkMaximumMatches);

                        if (checkMaximumMatches.ListHeadAdjustment)
                        {
                            MoveOutwardsAppendingListHeadAdjustment(checkMaximumMatches);
                        }

                        string[] neededElementsForCheckOperation = new string[0];
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkMaximumMatches,
                            neededElementsForCheckOperation,
                            enclosingSearchProgram);
                    }
                    else if (currentOperation is CheckContinueMatchingFailed)
                    {
                        CheckContinueMatchingFailed checkFailed =
                            (CheckContinueMatchingFailed)currentOperation;
                        checkFailed.CheckFailedOperations =
                            new SearchProgramList(checkFailed);
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkFailed,
                            enclosingCheckNegative.NeededElements,
                            enclosingSearchProgram);
                    }
                    else
                    {
                        Debug.Assert(false, "unknown check abort matching operation");
                    }
                }
                //////////////////////////////////////////////////////////
                else if (currentOperation.IsNestingOperation())
                //////////////////////////////////////////////////////////
                {
                    // depth first
                    CompleteCheckOperations(
                        currentOperation.GetNestedOperationsList(),
                        enclosingSearchProgram,
                        enclosingCheckNegative);
                }

                // breadth
                currentOperation = currentOperation.Next;
            }
        }

        /// <summary>
        /// "listentrick": append search program operations to adjust list heads
        /// i.e. set list entry point to element after last found,
        /// so that next searching starts there - preformance optimization
        /// (leave graph in the state of our last visit (searching it))
        /// </summary>
        private void MoveOutwardsAppendingListHeadAdjustment(
            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches)
        {
            // todo: avoid adjusting list heads twice for lookups of same type

            // insertion point for candidate failed operations
            SearchProgramOperation insertionPoint =
                checkMaximumMatches.CheckFailedOperations;
            SearchProgramOperation op = checkMaximumMatches;
            do
            {
                if(op is GetCandidateByIteration)
                {
                    GetCandidateByIteration candidateByIteration = 
                        op as GetCandidateByIteration;
                    if (candidateByIteration.Type==GetCandidateByIterationType.GraphElements)
                    {
                        AdjustListHeads adjustElements =
                            new AdjustListHeads(
                                AdjustListHeadsTypes.GraphElements,
                                candidateByIteration.PatternElementName,
                                candidateByIteration.IsNode);
                        insertionPoint = insertionPoint.Append(adjustElements);
                    }
                    else //candidateByIteration.Type==GetCandidateByIterationType.IncidentEdges
                    {
                        AdjustListHeads adjustIncident =
                            new AdjustListHeads(
                                AdjustListHeadsTypes.IncidentEdges,
                                candidateByIteration.PatternElementName,
                                candidateByIteration.StartingPointNodeName,
                                candidateByIteration.GetIncoming);
                        insertionPoint = insertionPoint.Append(adjustIncident);
                    }
                }

                op = op.Previous;
            }
            while(op!=null);
        }

        /// <summary>
        /// move outwards from check operation until operation to continue at is found
        /// appending remove isomorphy for isomorphy written on the way
        /// and final jump to operation to continue
        /// </summary>
        private void MoveOutwardsAppendingRemoveIsomorphyAndJump(
            CheckOperation checkOperation,
            string[] neededElementsForCheckOperation,
            SearchProgramOperation outermostOperation)
        {
            // insertion point for candidate failed operations
            SearchProgramOperation insertionPoint =
                checkOperation.CheckFailedOperations;
            while (insertionPoint.Next != null)
            {
                insertionPoint = insertionPoint.Next;
            }
            // currently focused operation on our way outwards
            SearchProgramOperation op = checkOperation;
            // move outwards until operation to continue at is found
            bool creationPointOfDominatingElementFound = false; 
            bool iterationReached = false;
            do
            {
                op = op.Previous;

                // insert code to clean up isomorphy information 
                // written in between the operation to continue and the check operation
                if (op is AcceptIntoPartialMatchWriteIsomorphy)
                {
                    AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                        op as AcceptIntoPartialMatchWriteIsomorphy;
                    WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                        new WithdrawFromPartialMatchRemoveIsomorphy(
                            writeIsomorphy.PatternElementName,
                            writeIsomorphy.Positive,
                            writeIsomorphy.IsNode);
                    insertionPoint = insertionPoint.Append(removeIsomorphy);
                }

                // determine operation to continue at
                // found by looking at the graph elements 
                // the check operation depends on / is dominated by
                // its the first element iteration on our way outwards the search program
                // after or at the point of a get element operation 
                // of some dominating element the check depends on
                // (or the outermost operation if no iteration is found until it is reached)
                if (op is GetCandidate)
                {
                    GetCandidate getCandidate = op as GetCandidate;
                    if (creationPointOfDominatingElementFound == false)
                    {
                        foreach (string dominating in neededElementsForCheckOperation)
                        {
                            if (getCandidate.PatternElementName == dominating)
                            {
                                creationPointOfDominatingElementFound = true;
                                iterationReached = false;
                                break;
                            }
                        }
                    }
                    if (op is GetCandidateByIteration)
                    {
                        iterationReached = true;
                    }
                }
            }
            while (!(creationPointOfDominatingElementFound && iterationReached)
                    && op != outermostOperation);
            SearchProgramOperation continuationPoint = op;

            // decide on type of continuing operation, then insert it

            // continue at top nesting level -> return
            while(!(op is SearchProgram))
            {
                op = op.Previous;
            }
            SearchProgram searchProgramRoot = op as SearchProgram;
            if (continuationPoint == searchProgramRoot)
            {
                ContinueOperation continueByReturn = 
                    new ContinueOperation(
                        ContinueOperationType.ByReturn,
                        !searchProgramRoot.IsSubprogram);
                insertionPoint.Append(continueByReturn);

                return;
            }
            
            // continue at directly enclosing nesting level -> continue
            op = checkOperation;
            do
            {
                op = op.Previous;
            }
            while (!op.IsNestingOperation());
            SearchProgramOperation directlyEnclosingOperation = op;
            if (continuationPoint == directlyEnclosingOperation
                // (check negative is enclosing, but not a loop, thus continue wouldn't work)
                && !(directlyEnclosingOperation is CheckPartialMatchByNegative))
            {
                ContinueOperation continueByContinue =
                    new ContinueOperation(ContinueOperationType.ByContinue);
                insertionPoint.Append(continueByContinue);

                return;
            }

            // otherwise -> goto label
            GotoLabel gotoLabel;

            // if our continuation point is a candidate iteration
            // -> append label at the end of the loop body of the candidate iteration loop 
            if (continuationPoint is GetCandidateByIteration)
            {
                GetCandidateByIteration candidateIteration =
                    continuationPoint as GetCandidateByIteration;
                op = candidateIteration.NestedOperationsList;
                while (op.Next != null)
                {
                    op = op.Next;
                }
                gotoLabel = new GotoLabel();
                op.Append(gotoLabel);
            }
            // otherwise our continuation point is a check negative operation
            // -> insert label directly after the check negative operation
            else
            {
                CheckPartialMatchByNegative checkNegative =
                    continuationPoint as CheckPartialMatchByNegative;
                gotoLabel = new GotoLabel();
                op.Insert(gotoLabel);
            }

            ContinueOperation continueByGoto =
                new ContinueOperation(
                    ContinueOperationType.ByGoto,
                    gotoLabel.LabelName); // ByGoto due to parameters
            insertionPoint.Append(continueByGoto);
        }

        /// <summary>
        /// Returns name of the candidate variable which will be created within the seach program
        /// holding over time the candidates for the given pattern element
        /// </summary>
        private static string NameOfCandidateVariable(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_cur_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the type variable which will be created within the search program
        /// holding the type object which will be used for determining the candidates
        /// for the given pattern element
        /// </summary>
        private static string NameOfTypeForCandidateVariable(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_type_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the type id variable which will be created within the search program
        /// holding the type id which will be used for determining the candidates
        /// for the given pattern element   (determined out of type object in iteration)
        /// </summary>
        private static string NameOfTypeIdForCandidateVariable(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_type_id_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the list head variable which will be created within the search program
        /// holding the list head of the list accessed by type id with the graph elements of that type
        /// for finding out when iteration of the candidates for the given pattern element has finished
        /// </summary>
        private static string NameOfCandidateIterationListHead(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_head_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the method called when a maybe preset element is not set
        /// </summary>
        private static string NameOfMissingPresetHandlingMethod(string patternElementName)
        {
            return "MissingPreset_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the variable which will be created within the seach program
        /// backing up the value of the mapped member of the graph element before assigning to it
        /// </summary>
        private static string NameOfVariableWithBackupOfMappedMember(string patternElementName, bool isNode, bool isPositive)
        {
            return NameOfCandidateVariable(patternElementName, isNode) + "_prev" + (isPositive ? "MappedTo" : "NegMappedTo");
        }

        /// <summary>
        /// Generates the source code of the matcher function for the given scheduled search plan
        /// new version building first abstract search program then search program code
        /// </summary>
        public String GenerateMatcherSourceCodeNew(ScheduledSearchPlan scheduledSearchPlan,
            String actionName, LGSPRulePattern rulePattern)
        {
            // build pass: build nested program from scheduled search plan
            SearchProgram searchProgram = BuildSearchProgram(scheduledSearchPlan, 
                "myMatch", null, null, rulePattern);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(actionName + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // build additional: create extra search subprogram per MaybePreset operation
            // will be called when preset element is not available
            BuildAddionalSearchSubprograms(scheduledSearchPlan, 
                searchProgram, rulePattern);

            // complete pass: complete check operations in all search programs
            CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(actionName + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // emit pass: emit source code from search program 
            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            searchProgram.Emit(sourceCode);
            return sourceCode.ToString();
        }
    }
}
