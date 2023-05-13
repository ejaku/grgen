/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Element of the search plan graph representing an element within the pattern graph or a root node.
    /// </summary>>
    [DebuggerDisplay("SearchPlanNode ({NodeType} {ToString()})")]
    public class SearchPlanNode
    {
        public readonly PlanNodeType NodeType;
        public readonly List<SearchPlanEdge> OutgoingEdges = new List<SearchPlanEdge>();
        public int ElementID;
        public readonly bool IsPreset;
        public bool Visited; // flag needed in matcher program generation from the scheduled search plan 

        public readonly PatternElement PatternElement;

        public SearchPlanNode(String rootName)
        {
            NodeType = PlanNodeType.Root;
            PatternElement = new PatternNode(-1, null, "", rootName, rootName, null, null, 0.0f, -1, false, null, null, null, null, null, null, false, null);
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
        /// <summary>
        /// IncomingPatternEdges are the search plan nodes which originate from the incoming pattern edges of the pattern node this node represents
        /// </summary>
        public readonly List<SearchPlanEdgeNode> IncomingPatternEdges = new List<SearchPlanEdgeNode>();
        /// <summary>
        /// OutgoingPatternEdges are the search plan nodes which originate from the outgoing pattern edges of the pattern node this node represents
        /// </summary>
        public readonly List<SearchPlanEdgeNode> OutgoingPatternEdges = new List<SearchPlanEdgeNode>();

        public SearchPlanNodeNode(PlanNode planNode)
            : base(planNode)
        {
        }

        public SearchPlanNodeNode(PlanNodeType nodeType, int elemID, bool isPreset, PatternElement patternElem)
            : base(nodeType, elemID, isPreset, patternElem) { }

        /////////////////////////////////////////////////////////////////////////////////////
        // helper stuff for building the interpretation plan for the interpreted matcher

        /// <summary>
        /// the node matcher interpretation plan operation created for this node
        /// </summary>
        public InterpretationPlanNodeMatcher nodeMatcher;
    }

    /// <summary>
    /// Element of the search plan graph representing an edge within the pattern graph.
    /// </summary>>
    public class SearchPlanEdgeNode : SearchPlanNode
    {
        /// <summary>
        /// PatternEdgeSource gives us the search plan node which originated from the source of the pattern edge this node represents
        /// </summary>
        public SearchPlanNodeNode PatternEdgeSource;

        /// <summary>
        /// PatternEdgeTarget gives us the search plan node which originated from the target of the pattern edge this node represents
        /// </summary>
        public SearchPlanNodeNode PatternEdgeTarget;

        public SearchPlanEdgeNode(PlanNode planNode, SearchPlanNodeNode patEdgeSrc, SearchPlanNodeNode patEdgeTgt)
            : base(planNode)
        {
            PatternEdgeSource = patEdgeSrc;
            PatternEdgeTarget = patEdgeTgt;
        }

        public SearchPlanEdgeNode(PlanNodeType nodeType, int elemID, bool isPreset, PatternElement patternElem,
            SearchPlanNodeNode patEdgeSrc, SearchPlanNodeNode patEdgeTgt)
            : base(nodeType, elemID, isPreset, patternElem)
        {
            PatternEdgeSource = patEdgeSrc;
            PatternEdgeTarget = patEdgeTgt;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        // helper stuff for building the interpretation plan for the interpreted matcher

        /// <summary>
        /// the edge matcher interpretation plan operation created for this edge
        /// </summary>
        public InterpretationPlanEdgeMatcher edgeMatcher;

        /// <summary>
        /// the direction variable interpretation plan operation created for this edge
        /// in case this is an edge to be matched bidirectionally in the graph
        /// </summary>
        public InterpretationPlanDirectionVariable directionVariable;
    }

    /// <summary>
    /// A search plan edge represents a matching operation and its costs.
    /// </summary>
    public class SearchPlanEdge : IComparable<SearchPlanEdge>
    {
        public readonly SearchPlanNode Target;
        public float Cost;
        public readonly SearchPlanNode Source;
        public readonly SearchOperationType Type;

        public float LocalCost; // only used in benchmarking

        public SearchPlanEdge(SearchOperationType type, SearchPlanNode source, SearchPlanNode target, float cost)
        {
            Target = target;
            Cost = cost;
            Source = source;
            Type = type;
        }

        // order along costs, needed as priority for priority queue
        public int CompareTo(SearchPlanEdge other)
        {
            // Schedule implicit ops as early as possible
            if(Type == SearchOperationType.ImplicitSource || Type == SearchOperationType.ImplicitTarget)
                return -1;
            if(other.Type == SearchOperationType.ImplicitSource || other.Type == SearchOperationType.ImplicitTarget)
                return 1;

            float epsilon = 0.001f;
            float diff = Cost - other.Cost;
            if(diff < -epsilon)
                return -1;
            else if(diff > epsilon)
                return 1;

            // Choose equally expensive operations in this order: incoming/outgoing, edge lookup, node lookup

            if(Type == SearchOperationType.Incoming || Type == SearchOperationType.Outgoing)
                return -1;
            if(other.Type == SearchOperationType.Incoming || other.Type == SearchOperationType.Outgoing)
                return 1;

            if(Type == SearchOperationType.Lookup && Target.NodeType == PlanNodeType.Edge)
                return -1;
            if(other.Type == SearchOperationType.Lookup && other.Target.NodeType == PlanNodeType.Edge)
                return 1;

            // Both are node lookups...

            if(diff < 0)
                return -1;
            else if(diff > 0)
                return 1;
            else
                return 0;
        }
    }

    /// <summary>
    /// The search plan graph data structure for scheduling.
    /// </summary>
    public class SearchPlanGraph
    {
        public readonly SearchPlanNode Root;
        public readonly SearchPlanNode[] Nodes;
        public readonly SearchPlanEdge[] Edges;
        public readonly int NumPresetElements = 0;
        public readonly int NumIndependentStorageIndexElements = 0;

        public SearchPlanGraph(SearchPlanNode root, SearchPlanNode[] nodes, SearchPlanEdge[] edges)
        {
            Root = root;
            Nodes = nodes;
            Edges = edges;
            NumPresetElements = 0;
            foreach(SearchPlanNode node in nodes)
            {
                if(node.IsPreset)
                    ++NumPresetElements;
            }
            foreach(SearchPlanEdge edge in edges)
            {
                if(edge.Type == SearchOperationType.PickFromStorage
                   || edge.Type == SearchOperationType.MapWithStorage
                   || edge.Type == SearchOperationType.PickFromIndex
                   || edge.Type == SearchOperationType.PickByName
                   || edge.Type == SearchOperationType.PickByUnique)
                {
                    ++NumIndependentStorageIndexElements;
                }
            }
        }
    }
}

