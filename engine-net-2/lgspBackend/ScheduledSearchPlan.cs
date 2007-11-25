using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.lgsp
{
    public class IsomorphyInformation
    {
        // if true, the graph element's is-matched-bit must be checked
        public bool CheckIsMatchedBit = false;
        // if true, the graph element's is-matched-bit must be set
        public bool SetIsMatchedBit = false;
        // pattern elements the current element is not allowed to be homomorph to
        public List<SearchPlanNode> PatternElementsToCheckAgainst = null;

        public List<string> PatternElementsToCheckAgainstAsListOfStrings()
        {
            if (PatternElementsToCheckAgainst == null) 
            {
                return null;
            }

            List<string> result = new List<string>(PatternElementsToCheckAgainst.Count);
            foreach (SearchPlanNode spn in PatternElementsToCheckAgainst)
            {
                result.Add(spn.PatternElement.Name);
            }

            return result;
        }
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

        public SearchOperation(SearchOperationType type, object elem,
            SearchPlanNode srcSPNode, float costToEnd)
        {
            Type = type;
            Element = elem;
            SourceSPNode = srcSPNode;
            CostToEnd = costToEnd;
        }

        public static SearchOperation CreateMaybePreset(SearchPlanNode element)
        {
            return new SearchOperation(SearchOperationType.MaybePreset,
                element, null, 0);
        }

        public static SearchOperation CreateNegPreset(SearchPlanNode element)
        {
            return new SearchOperation(SearchOperationType.NegPreset,
                element, null, 0);
        }

        public static SearchOperation CreateLookup(
            SearchPlanNode element, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Lookup,
                element, null, costToEnd);
        }

        public static SearchOperation CreateOutgoing(
            SearchPlanNodeNode source, SearchPlanEdgeNode outgoingEdge, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Outgoing,
                outgoingEdge, source, costToEnd);
        }

        public static SearchOperation CreateIncoming(
            SearchPlanNodeNode source, SearchPlanEdgeNode outgoingEdge, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Incoming,
                outgoingEdge, source, costToEnd);
        }

        public static SearchOperation CreateImplicitSource(
            SearchPlanEdgeNode edge, SearchPlanNodeNode sourceNode, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.ImplicitSource,
                sourceNode, edge, costToEnd);
        }

        public static SearchOperation CreateImplicitTarget(
            SearchPlanEdgeNode edge, SearchPlanNodeNode targetNode, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.ImplicitTarget,
                targetNode, edge, costToEnd);
        }

        public static SearchOperation CreateNegativePattern(
            ScheduledSearchPlan schedSP, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.NegativePattern,
                schedSP, null, costToEnd);
        }

        public static SearchOperation CreateCondition(
            Condition condition, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Condition,
                condition, null, costToEnd);
        }

        public int CompareTo(SearchOperation other)
        {
            float diff = CostToEnd - other.CostToEnd;
            if (diff < 0) return -1;
            else if (diff > 0) return 1;
            else return 0;
        }
    }

    /// <summary>
    /// The scheduled search plan is a list of search operations,
    /// plus the information which nodes/edges are homomorph
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
}
