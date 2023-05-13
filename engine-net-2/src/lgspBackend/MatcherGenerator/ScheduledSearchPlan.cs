/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

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

        /// <summary>
        /// if true the element is not to be globally checked against anything, nor does it set any flags
        /// </summary>
        public bool TotallyHomomorph = false;

        /// <summary>
        /// if true the candidate must be locked for all threads of a parallelized action,
        /// in addition to sequential locking, which is used for sequential checking in the header
        /// this is the case for candidates bound in the head of a parallized action
        /// </summary>
        public bool LockForAllThreads = false;

        /// <summary>
        /// if true parallelized isomorphy setting/checking code needs to be emitted, for the current thread
        /// this is the case for iso handling in the body of a parallelized action
        /// </summary>
        public bool Parallel = false;


        public object Clone()
        {
            IsomorphyInformation ii = new IsomorphyInformation();
            ii.CheckIsMatchedBit = CheckIsMatchedBit;
            ii.SetIsMatchedBit = SetIsMatchedBit;
            if(PatternElementsToCheckAgainst != null)
            {
                ii.PatternElementsToCheckAgainst = new List<SearchPlanNode>(PatternElementsToCheckAgainst.Count);
                for(int i=0; i<PatternElementsToCheckAgainst.Count; ++i)
                {
                    ii.PatternElementsToCheckAgainst.Add(PatternElementsToCheckAgainst[i]);
                }
            }
            if(GloballyHomomorphPatternElements != null)
            {
                ii.GloballyHomomorphPatternElements = new List<SearchPlanNode>(GloballyHomomorphPatternElements.Count);
                for(int i = 0; i < GloballyHomomorphPatternElements.Count; ++i)
                {
                    ii.GloballyHomomorphPatternElements.Add(GloballyHomomorphPatternElements[i]);
                }
            }
            ii.TotallyHomomorph = TotallyHomomorph;
            ii.LockForAllThreads = LockForAllThreads;
            ii.Parallel = Parallel;
            return ii;
        }

        public List<string> PatternElementsToCheckAgainstAsListOfStrings()
        {
            if(PatternElementsToCheckAgainst == null) 
                return null;

            List<string> result = new List<string>(PatternElementsToCheckAgainst.Count);
            foreach(SearchPlanNode spn in PatternElementsToCheckAgainst)
            {
                result.Add(spn.PatternElement.Name);
            }

            return result;
        }

        public List<string> GloballyHomomorphPatternElementsAsListOfStrings()
        {
            if(GloballyHomomorphPatternElements == null)
                return null;

            List<string> result = new List<string>(GloballyHomomorphPatternElements.Count);
            foreach(SearchPlanNode spn in GloballyHomomorphPatternElements)
            {
                result.Add(spn.PatternElement.Name);
            }

            return result;
        }
    }

    public class ConnectednessCheck : ICloneable
    {
        public string PatternElementName;
        public string PatternNodeName;
        public string PatternEdgeName;
        public string TheOtherPatternNodeName;


        public object Clone()
        {
            ConnectednessCheck cc = new ConnectednessCheck();
            cc.PatternElementName = PatternElementName;
            cc.PatternNodeName = PatternNodeName;
            cc.PatternEdgeName = PatternEdgeName;
            cc.TheOtherPatternNodeName = TheOtherPatternNodeName;
            return cc;
        }
    }

    /// <summary>
    /// Search operation with information about homomorphic mapping.
    /// Element of the scheduled search plan.
    /// </summary>
    [DebuggerDisplay("SearchOperation ({SourceSPNode} -{Type}-> {Element} = {CostToEnd})")]
    public class SearchOperation : IComparable<SearchOperation>, ICloneable
    {
        public readonly SearchOperationType Type;

        /// <summary>
        /// If Type is NegativePattern or IndependentPattern, Element is a ScheduledSearchPlan object.
        /// If Type is Condition, Element is a Condition object.
        /// If Type is AssignVar, Element is a PatternVariable object.
        /// If Type is DefToBeYieldedTo, Element is a Variable object in case of a variable, or a SearchPlanNode in case of a graph element.
        /// Otherwise Element is the target SearchPlanNode for this operation.
        /// </summary>
        public object Element;

        public readonly SearchPlanNode SourceSPNode; // the source element that must be matched before, this operation depends upon

        public StorageAccess Storage; // set for storage access
        public StorageAccessIndex StorageIndex; // set for storage access with index
        public IndexAccess IndexAccess; // set for index access
        public NameLookup NameLookup; // set for name lookup
        public UniqueLookup UniqueLookup; // set for unique lookup

        public expression.Expression Expression; // set for inlined assignments, for initializations of defs

        public float CostToEnd; // for scheduling

        // used in check for isomorphic elements
        public IsomorphyInformation Isomorphy = new IsomorphyInformation();

        public ConnectednessCheck ConnectednessCheck = new ConnectednessCheck();


        public SearchOperation(SearchOperationType type, object elem,
            SearchPlanNode srcSPNode, float costToEnd)
        {
            Type = type;
            Element = elem;
            SourceSPNode = srcSPNode;
            CostToEnd = costToEnd;
        }

        public object Clone()
        {
            return Clone(Type);
        }

        public object Clone(SearchOperationType searchOperationType)
        {
            // clone the condition as we may need to set the parallelized flag for it, while the original must stay untouched
            object ElementWithConditionCloned = Element is PatternCondition ? (Element as PatternCondition).Clone() : Element;
            SearchOperation so = new SearchOperation(searchOperationType, ElementWithConditionCloned, SourceSPNode, CostToEnd);
            so.Isomorphy = (IsomorphyInformation)Isomorphy.Clone();
            so.ConnectednessCheck = (ConnectednessCheck)ConnectednessCheck.Clone();
            so.Storage = Storage;
            so.StorageIndex = StorageIndex;
            so.IndexAccess = IndexAccess;
            so.NameLookup = NameLookup;
            so.UniqueLookup = UniqueLookup;
            so.Expression = Expression;
            return so;
        }

        public int CompareTo(SearchOperation other)
        {
            float diff = CostToEnd - other.CostToEnd;
            if(diff < 0)
                return -1;
            else if(diff > 0)
                return 1;
            else
                return 0;
        }
    }

    /// <summary>
    /// The scheduled search plan is a list of search operations,
    /// plus the information which nodes/edges are homomorph
    /// </summary>
    public class ScheduledSearchPlan : ICloneable
    {
        public readonly PatternGraph PatternGraph; // the pattern graph originating this schedule
        public readonly SearchOperation[] Operations; // the scheduled list of search operations
        public readonly float Cost; // (needed for scheduling nac-subgraphs into the full graph)
        
        public ScheduledSearchPlan(PatternGraph patternGraph, SearchOperation[] ops, float cost)
        {
            PatternGraph = patternGraph;
            Operations = ops;
            Cost = cost;
        }

        public object Clone()
        {
            ScheduledSearchPlan ssp = new ScheduledSearchPlan(PatternGraph, new SearchOperation[Operations.Length], Cost);
            for(int i = 0; i < Operations.Length; ++i)
            {
                ssp.Operations[i] = (SearchOperation)Operations[i].Clone();
            }
            return ssp;
        }
    }
}
