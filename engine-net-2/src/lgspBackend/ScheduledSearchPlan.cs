/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;


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
                    ii.PatternElementsToCheckAgainst.Add(PatternElementsToCheckAgainst[i]);
                }
            }
            if (GloballyHomomorphPatternElements != null)
            {
                ii.GloballyHomomorphPatternElements = new List<SearchPlanNode>(GloballyHomomorphPatternElements.Count);
                for (int i = 0; i < GloballyHomomorphPatternElements.Count; ++i)
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
        /// If Type is NegativePattern or IndependentPattern, Element is a ScheduledSearchPlan object.
        /// If Type is Condition, Element is a Condition object.
        /// If Type is AssignVar, Element is a PatternVariable object.
        /// If Type is DefToBeYieldedTo, Element is a Variable object in case of a variable, or a SearchPlanNode in case of a graph element.
        /// Otherwise Element is the target SearchPlanNode for this operation.
        /// </summary>
        public object Element;

        public SearchPlanNode SourceSPNode; // the source element that must be matched before, this operation depends upon

        public StorageAccess Storage; // set for storage access
        public StorageAccessIndex StorageIndex; // set for storage access with index
        public IndexAccess IndexAccess; // set for index access

        public expression.Expression Expression; // set for inlined assignments, for initializations of defs

        public float CostToEnd; // for scheduling

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
            so.Storage = Storage;
            so.StorageIndex = StorageIndex;
            so.Expression = Expression;
            return so;
        }

        public int CompareTo(SearchOperation other)
        {
            float diff = CostToEnd - other.CostToEnd;
            if (diff < 0) return -1;
            else if (diff > 0) return 1;
            else return 0;
        }

        public void Explain(SourceBuilder sb, IGraphModel model)
        {
            SearchPlanNode src = SourceSPNode as SearchPlanNode;
            SearchPlanNode tgt = Element as SearchPlanNode;
            switch(Type)
            {
                case SearchOperationType.Outgoing:
                    sb.AppendFront("from " + src.PatternElement.UnprefixedName + " outgoing -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->");
                    break;
                case SearchOperationType.Incoming:
                    sb.AppendFront("from " + src.PatternElement.UnprefixedName + " incoming <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-");
                    break;
                case SearchOperationType.Incident:
                    sb.AppendFront("from " + src.PatternElement.UnprefixedName + " incident <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->"); 
                    break;
                case SearchOperationType.ImplicitSource:
                    sb.AppendFront("from <-" + src.PatternElement.UnprefixedName + "- get source " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name);
                    break;
                case SearchOperationType.ImplicitTarget:
                    sb.AppendFront("from -" + src.PatternElement.UnprefixedName + "-> get target " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name);
                    break;
                case SearchOperationType.Implicit:
                    sb.AppendFront("from <-" + src.PatternElement.UnprefixedName + "-> get implicit " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name); 
                    break;
                case SearchOperationType.Lookup:
                    if(tgt.PatternElement is PatternNode)
                        sb.AppendFront("lookup " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name + " in graph");
                    else
                        sb.AppendFront("lookup -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-> in graph");
                    break;
                case SearchOperationType.ActionPreset:
                    sb.AppendFront("(preset: " + tgt.PatternElement.UnprefixedName + ")");
                    break;
                case SearchOperationType.NegIdptPreset:
                    sb.AppendFront("(preset: " + tgt.PatternElement.UnprefixedName + ")");
                    break;
                case SearchOperationType.SubPreset:
                    sb.AppendFront("(preset: " + tgt.PatternElement.UnprefixedName + ")");
                    break;
                case SearchOperationType.Condition:
                    sb.AppendFront("if { depending on " + String.Join(",", ((PatternCondition)Element).NeededNodes) + ","
                        + String.Join(",", ((PatternCondition)Element).NeededEdges) + " }");
                    break;
                case SearchOperationType.NegativePattern:
                    sb.AppendFront("negative {\n");
                    sb.Indent();
                    ((ScheduledSearchPlan)Element).Explain(sb, model);
                    sb.Append("\n");
                    ((ScheduledSearchPlan)Element).PatternGraph.ExplainNested(sb, model);
                    sb.Unindent();
                    sb.AppendFront("}");
                    break;
                case SearchOperationType.IndependentPattern:
                    sb.AppendFront("independent {\n");
                    sb.Indent();
                    ((ScheduledSearchPlan)Element).Explain(sb, model);
                    sb.Append("\n");
                    ((ScheduledSearchPlan)Element).PatternGraph.ExplainNested(sb, model);
                    sb.Unindent();
                    sb.AppendFront("}");
                    break;
                case SearchOperationType.PickFromStorage:
                case SearchOperationType.PickFromStorageDependent:
                    sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + Storage.ToString() + "}");
                    break;
                case SearchOperationType.MapWithStorage:
                case SearchOperationType.MapWithStorageDependent:
                    sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + Storage.ToString() + "[" + StorageIndex.ToString() + "]}");
                    break;
                case SearchOperationType.PickFromIndex:
                case SearchOperationType.PickFromIndexDependent:
                    sb.AppendFront(tgt.PatternElement.UnprefixedName + "{" + IndexAccess.ToString() + "}");
                    break;
                case SearchOperationType.Cast:
                    sb.AppendFront(tgt.PatternElement.UnprefixedName + "<" + src.PatternElement.UnprefixedName + ">");
                    break;
                case SearchOperationType.Assign:
                    sb.AppendFront("(" + tgt.PatternElement.UnprefixedName + " = " + src.PatternElement.UnprefixedName + ")");
                    break;
                case SearchOperationType.Identity:
                    sb.AppendFront("(" + tgt.PatternElement.UnprefixedName + " == " + src.PatternElement.UnprefixedName + ")");
                    break;
                case SearchOperationType.AssignVar:
                    sb.AppendFront("(" + tgt.PatternElement.UnprefixedName + " = expr" + ")");
                    break;
                case SearchOperationType.LockLocalElementsForPatternpath:
                    sb.AppendFront("lock for patternpath");
                    break;
                case SearchOperationType.DefToBeYieldedTo:
                    if(Element is PatternVariable)
                        sb.AppendFront("def " + ((PatternVariable)Element).Name);
                    else
                        sb.AppendFront("def " + ((SearchPlanNode)Element).PatternElement.Name);
                    break;
                case SearchOperationType.ParallelLookup:
                    if(tgt.PatternElement is PatternNode)
                        sb.AppendFront("parallelized lookup " + tgt.PatternElement.UnprefixedName + ":" + model.NodeModel.Types[tgt.PatternElement.TypeID].Name + " in graph");
                    else
                        sb.AppendFront("parallelized lookup -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-> in graph");
                    break;
                case SearchOperationType.ParallelPickFromStorage:
                case SearchOperationType.ParallelPickFromStorageDependent:
                    sb.AppendFront("parallelized " + tgt.PatternElement.UnprefixedName + "{" + Storage.ToString() + "}");
                    break;
                case SearchOperationType.ParallelOutgoing:
                    sb.AppendFront("parallelized from " + src.PatternElement.UnprefixedName + " outgoing -" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->");
                    break;
                case SearchOperationType.ParallelIncoming:
                    sb.AppendFront("parallelized from " + src.PatternElement.UnprefixedName + " incoming <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "-");
                    break;
                case SearchOperationType.ParallelIncident:
                    sb.AppendFront("parallelized from " + src.PatternElement.UnprefixedName + " incident <-" + tgt.PatternElement.UnprefixedName + ":" + model.EdgeModel.Types[tgt.PatternElement.TypeID].Name + "->");
                    break;
                case SearchOperationType.WriteParallelPreset:
                case SearchOperationType.ParallelPreset:
                case SearchOperationType.WriteParallelPresetVar:
                case SearchOperationType.ParallelPresetVar:
                case SearchOperationType.SetupParallelLookup:
                case SearchOperationType.SetupParallelPickFromStorage:
                case SearchOperationType.SetupParallelPickFromStorageDependent:
                case SearchOperationType.SetupParallelOutgoing:
                case SearchOperationType.SetupParallelIncoming:
                case SearchOperationType.SetupParallelIncident:
                    break; // uninteresting to the user
            }
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

        public void Explain(SourceBuilder sb, IGraphModel model)
        {
            foreach(SearchOperation searchOp in Operations)
            {
                searchOp.Explain(sb, model);
            }
        }
    }
}
