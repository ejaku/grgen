/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                if (SuperNode == null) return null;
                PlanSuperNode current = SuperNode;
                while (current.SuperNode != null)
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
            if (costDiff < -epsilon) return false;
            if (costDiff > epsilon) return true;

            // Choose equally expensive operations in this order: implicit, non-lookup, edge lookups, node lookups

            if (minEdge.Source.NodeType == PlanNodeType.Edge) return false;      // minEdge is implicit op
            if (newEdge.Source.NodeType == PlanNodeType.Edge) return true;       // newEdge is implicit op

            if (minEdge.Source.NodeType != PlanNodeType.Root) return false;      // minEdge is non-lookup op
            if (newEdge.Source.NodeType != PlanNodeType.Root) return true;       // newEdge is non-lookup op

            if (minEdge.Target.NodeType == PlanNodeType.Edge) return false;      // minEdge is edge lookup
            if (newEdge.Target.NodeType == PlanNodeType.Edge) return true;       // newEdge is edge lookup

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
        public List<PlanEdge> IncomingEdges = new List<PlanEdge>();
        public int ElementID;
        public bool IsPreset;

        public PatternElement PatternElement; // the pattern element(node or edge) this plan node represents

        /// <summary>
        /// Only valid if this plan node is representing a pattern edge, 
        /// then PatternEdgeSource gives us the plan node made out of the source node of the edge
        /// then PatternEdgeTarget gives us the plan node made out of the target node of the edge
        /// </summary>
        public PlanNode PatternEdgeSource, PatternEdgeTarget;

        /// <summary>
        /// Instantiates a root plan node.
        /// </summary>
        /// <param name="rootName">The name for the root plan node.</param>
        public PlanNode(String rootName)
        {
            NodeType = PlanNodeType.Root;
            PatternElement = new PatternNode(-1, "", rootName, rootName, null, null, 0.0f, -1, false, null, null, null, null, null, false);
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
        /// <param name="patternEdgeSource">The plan node corresponding to the source of the pattern edge.</param>
        /// <param name="patternEdgeTarget">The plan node corresponding to the target of the pattern edge.</param>
        public PlanNode(PatternEdge patEdge, int elemID, bool isPreset,
            PlanNode patternEdgeSource, PlanNode patternEdgeTarget)
        {
            NodeType = PlanNodeType.Edge;
            ElementID = elemID;
            IsPreset = isPreset;

            PatternElement = patEdge;

            PatternEdgeSource = patternEdgeSource;
            PatternEdgeTarget = patternEdgeTarget;
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

            foreach (PlanEdge edge in IncomingEdges)
            {
                if (excludeTopNode != null && edge.Source.TopNode == excludeTopNode) continue;
                if (PreferNewEdge(minEdge, minCost, edge, edge.mstCost))
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
                if (current.IncomingMSAEdge.mstCost < minCycleEdgeCost)
                    minCycleEdgeCost = current.IncomingMSAEdge.mstCost;
                current = current.IncomingMSAEdge.Source;
                while (current.SuperNode != null && current.SuperNode != this)
                    current = current.SuperNode;
            }
            while (current != Child);

            // Adjust costs of incoming edges
            foreach (PlanEdge edge in Incoming)
            {
                PlanPseudoNode curTarget = edge.Target;
                while (curTarget.SuperNode != this)
                    curTarget = curTarget.SuperNode;
                edge.mstCost -= curTarget.IncomingMSAEdge.mstCost - minCycleEdgeCost;
            }

            // Find cheapest incoming edge and set Child appropriately
            float cost;
            IncomingMSAEdge = GetCheapestIncoming(this, out cost);
            Child = IncomingMSAEdge.Target;
            while (Child.SuperNode != this)
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
                    foreach (PlanEdge edge in current.Incoming)
                    {
                        if (edge.Source.TopSuperNode != currentTopSuperNode)
                            yield return edge;
                    }
                    current = current.IncomingMSAEdge.Source;
                    while (current.SuperNode != this)
                        current = current.SuperNode;
                }
                while (current != Child);
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
                if (curEdge != null)
                {
                    PlanPseudoNode target = curEdge.Target;
                    while (target.SuperNode != this)
                        target = target.SuperNode;

                    if (PreferNewEdge(minEdge, minCost, curEdge, curCost))
                    {
                        minCost = curCost;
                        minEdge = curEdge;
                    }
                }
                current = current.IncomingMSAEdge.Source;
                while (current.SuperNode != this)
                    current = current.SuperNode;
            }
            while (current != Child);
            return minEdge;
        }
    }

    /// <summary>
    /// A plan edge represents a matching operation and its costs.
    /// </summary>
    [DebuggerDisplay("PlanEdge ({Source} -{Type}-> {Target} = {Cost})")]
    public class PlanEdge
    {
        public PlanNode Target;
        public float Cost;
        public PlanNode Source;

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
            mstCost = (float)Math.Max(Math.Log(cost), 1);
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
        public PlanNode Root; // the root node of the plan graph
        public PlanNode[] Nodes; // nodes of the plan graph without the root node, representing pattern elements
        public PlanEdge[] Edges; // edges of the plan graph, representing search operations

        public PlanGraph(PlanNode root, PlanNode[] nodes, PlanEdge[] edges)
        {
            Root = root;
            Nodes = nodes;
            Edges = edges;
        }
    }
}
