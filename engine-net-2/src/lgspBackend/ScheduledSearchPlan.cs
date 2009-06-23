/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.lgsp
{
    public class IsomorphyInformation : ICloneable
    {
        /// <summary>
        /// if true, the graph element's is-matched-bit must be checked
        /// </summary>
        public bool CheckIsMatchedBit = false;

        /// <summary>
        /// if true, the graph element's is-matched-bit must be set
        /// </summary>
        public bool SetIsMatchedBit = false;

        /// <summary>
        /// pattern elements the current element is not allowed to be homomorph to
        /// </summary>
        public List<SearchPlanNode> PatternElementsToCheckAgainst = null;

        /// <summary>
        /// pattern elements the current element is allowed to be globally homomorph to
        /// </summary>
        public List<SearchPlanNode> GloballyHomomorphPatternElements = null;


        public Object Clone()
        {
            IsomorphyInformation ii = new IsomorphyInformation();
            ii.CheckIsMatchedBit = CheckIsMatchedBit;
            ii.SetIsMatchedBit = SetIsMatchedBit;
            if (PatternElementsToCheckAgainst != null)
            {
                ii.PatternElementsToCheckAgainst = new List<SearchPlanNode>(PatternElementsToCheckAgainst.Count);
                for(int i=0; i<PatternElementsToCheckAgainst.Count; ++i)
                {
                    ii.PatternElementsToCheckAgainst[i] = PatternElementsToCheckAgainst[i];
                }
            }
            if (GloballyHomomorphPatternElements != null)
            {
                ii.GloballyHomomorphPatternElements = new List<SearchPlanNode>(GloballyHomomorphPatternElements.Count);
                for (int i = 0; i < GloballyHomomorphPatternElements.Count; ++i)
                {
                    ii.GloballyHomomorphPatternElements[i] = GloballyHomomorphPatternElements[i];
                }
            }
            return ii;
        }

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

        public List<string> GloballyHomomorphPatternElementsAsListOfStrings()
        {
            if (GloballyHomomorphPatternElements == null)
            {
                return null;
            }

            List<string> result = new List<string>(GloballyHomomorphPatternElements.Count);
            foreach (SearchPlanNode spn in GloballyHomomorphPatternElements)
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
    public class SearchOperation : IComparable<SearchOperation>, ICloneable
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

        public Object Clone()
        {
            SearchOperation so = new SearchOperation(Type, Element, SourceSPNode, CostToEnd);
            so.Isomorphy = (IsomorphyInformation)Isomorphy.Clone();
            return so;
        }

        public static SearchOperation CreateMaybePreset(SearchPlanNode element)
        {
            return new SearchOperation(SearchOperationType.MaybePreset,
                element, null, 0);
        }

        public static SearchOperation CreateNegIdptPreset(SearchPlanNode element)
        {
            return new SearchOperation(SearchOperationType.NegIdptPreset,
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

        public static SearchOperation CreateIndependentPattern(
            ScheduledSearchPlan schedSP, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.IndependentPattern,
                schedSP, null, costToEnd);
        }

        public static SearchOperation CreateCondition(
            PatternCondition condition, float costToEnd)
        {
            return new SearchOperation(SearchOperationType.Condition,
                condition, null, costToEnd);
        }

        public static SearchOperation CreateLockLocalElementsForPatternpath(float costToEnd)
        {
            return new SearchOperation(SearchOperationType.LockLocalElementsForPatternpath,
                null, null, costToEnd);
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
    public class ScheduledSearchPlan : ICloneable
    {
        public PatternGraph PatternGraph; // the pattern graph originating this schedule
        public SearchOperation[] Operations; // the scheduled list of search operations
        public float Cost; // (needed for scheduling nac-subgraphs into the full graph)
        
        public ScheduledSearchPlan(PatternGraph patternGraph, SearchOperation[] ops, float cost)
        {
            PatternGraph = patternGraph;
            Operations = ops;
            Cost = cost;
        }

        private ScheduledSearchPlan(PatternGraph patternGraph, float cost)
        {
            PatternGraph = patternGraph;
            Cost = cost;
        }

        public Object Clone()
        {
            ScheduledSearchPlan ssp = new ScheduledSearchPlan(PatternGraph, Cost);
            ssp.Operations = new SearchOperation[Operations.Length];
            for (int i = 0; i < Operations.Length; ++i)
            {
                ssp.Operations[i] = (SearchOperation)Operations[i].Clone();
            }
            return ssp;
        }
    }
}
