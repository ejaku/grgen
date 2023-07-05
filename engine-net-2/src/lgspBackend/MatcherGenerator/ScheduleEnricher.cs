/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.expression;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;


namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class enriching schedules with homomorphy information, 
    /// merging negative and independent schedules into the main schedule,
    /// and parallelizing schedules.
    /// </summary>
    public class ScheduleEnricher
    {
        /// <summary>
        /// Appends homomorphy information to each operation of the scheduled search plan
        /// </summary>
        public static void AppendHomomorphyInformation(IGraphModel model, ScheduledSearchPlan ssp)
        {
            // no operation -> nothing which could be homomorph
            if(ssp.Operations.Length == 0)
                return;

            // iterate operations of the search plan to append homomorphy checks
            for(int i = 0; i < ssp.Operations.Length; ++i)
            {
                if(ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.AssignVar
                    || ssp.Operations[i].Type == SearchOperationType.DefToBeYieldedTo
                    || ssp.Operations[i].Type == SearchOperationType.InlinedIndependentCheckForDuplicateMatch)
                {
                    continue;
                }

                if(ssp.Operations[i].Type == SearchOperationType.NegativePattern)
                {
                    AppendHomomorphyInformation(model, (ScheduledSearchPlan)ssp.Operations[i].Element);
                    continue;
                }

                if(ssp.Operations[i].Type == SearchOperationType.IndependentPattern)
                {
                    AppendHomomorphyInformation(model, (ScheduledSearchPlan)ssp.Operations[i].Element);
                    continue;
                }

                DetermineAndAppendHomomorphyChecks(model, ssp, i);
            }
        }

        /// <summary>
        /// Determines which homomorphy check operations are necessary 
        /// at the operation of the given position within the scheduled search plan
        /// and appends them.
        /// </summary>
        private static void DetermineAndAppendHomomorphyChecks(IGraphModel model, ScheduledSearchPlan ssp, int j)
        {
            // take care of global homomorphy
            FillInGlobalHomomorphyPatternElements(ssp, j);
        
            ///////////////////////////////////////////////////////////////////////////
            // first handle special case pure homomorphy

            SearchPlanNode spn_j = (SearchPlanNode)ssp.Operations[j].Element;

            if(spn_j.ElementID == -1)
            {
                // inlined from independent for better matching, independent from the rest
                return;
            }

            bool homToAll = true;
            if(spn_j.NodeType == PlanNodeType.Node)
            {
                for(int i = 0; i < ssp.PatternGraph.nodesPlusInlined.Length; ++i)
                {
                    if(!ssp.PatternGraph.homomorphicNodes[spn_j.ElementID - 1, i])
                    {
                        homToAll = false;
                        break;
                    }
                }
            }
            else //(spn_j.NodeType == PlanNodeType.Edge)
            {
                for(int i = 0; i < ssp.PatternGraph.edgesPlusInlined.Length; ++i)
                {
                    if(!ssp.PatternGraph.homomorphicEdges[spn_j.ElementID - 1, i])
                    {
                        homToAll = false;
                        break;
                    }
                }
            }

            if(homToAll)
            {
                // operation is allowed to be homomorph with everything
                // no checks for isomorphy or restricted homomorphy needed at all
                return;
            }

            ///////////////////////////////////////////////////////////////////////////
            // no pure homomorphy, so we have restricted homomorphy or isomorphy
            // and need to inspect the operations before, together with the homomorphy matrix 
            // for determining the necessary homomorphy checks

            GraphElementType[] types;
            bool[,] hom;

            if(spn_j.NodeType == PlanNodeType.Node)
            {
                types = model.NodeModel.Types;
                hom = ssp.PatternGraph.homomorphicNodes;
            }
            else // (spn_j.NodeType == PlanNodeType.Edge)
            {
                types = model.EdgeModel.Types;
                hom = ssp.PatternGraph.homomorphicEdges;
            }

            // order operation to check against all elements it's not allowed to be homomorph to

            // iterate through the operations before our position
            bool homomorphyPossibleAndAllowed = false;
            for(int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if(ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern
                    || ssp.Operations[i].Type == SearchOperationType.IndependentPattern
                    || ssp.Operations[i].Type == SearchOperationType.Assign
                    || ssp.Operations[i].Type == SearchOperationType.AssignVar
                    || ssp.Operations[i].Type == SearchOperationType.DefToBeYieldedTo
                    || ssp.Operations[i].Type == SearchOperationType.InlinedIndependentCheckForDuplicateMatch)
                {
                    continue;
                }

                SearchPlanNode spn_i = (SearchPlanNode)ssp.Operations[i].Element;

                if(spn_i.NodeType != spn_j.NodeType)
                {
                    // don't compare nodes with edges
                    continue;
                }

                if(spn_i.ElementID == -1)
                {
                    // inlined from independent for better matching, independent from the rest
                    continue;
                }
                
                // find out whether element types are disjoint
                GraphElementType type_i = types[spn_i.PatternElement.TypeID];
                GraphElementType type_j = types[spn_j.PatternElement.TypeID];
                bool disjoint = true;
                foreach(GraphElementType subtype_i in type_i.SubOrSameTypes)
                {
                    if(type_j.IsA(subtype_i) || subtype_i.IsA(type_j)) // IsA==IsSuperTypeOrSameType
                    {
                        disjoint = false;
                        break;
                    }
                }

                if(disjoint)
                {
                    // don't check elements if their types are disjoint
                    continue;
                }

                // at this position we found out that spn_i and spn_j 
                // might get matched to the same host graph element, i.e. homomorphy is possible
                
                // if that's ok we don't need to insert checks to prevent this from happening
                if(hom[spn_i.ElementID - 1, spn_j.ElementID - 1])
                {
                    homomorphyPossibleAndAllowed = true;
                    continue;
                }

                // otherwise the generated matcher code has to check 
                // that pattern element j doesn't get bound to the same graph element
                // the pattern element i is already bound to 
                if(ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst == null)
                    ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst = new List<SearchPlanNode>();
                ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst.Add(spn_i);

                // if spn_j might get matched to the same host graph element as spn_i and this is not allowed
                // make spn_i set the is-matched-bit so that spn_j can detect this situation
                ssp.Operations[i].Isomorphy.SetIsMatchedBit = true;
            }

            // only if elements, the operation must be isomorph to, were matched before
            // (otherwise there were only elements, the operation is allowed to be homomorph to,
            //  matched before, so no check needed here)
            if(ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst != null
                && ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst.Count > 0)
            {
                // order operation to check whether the is-matched-bit is set
                ssp.Operations[j].Isomorphy.CheckIsMatchedBit = true;
            }

            // if no check for isomorphy was skipped due to homomorphy being allowed
            // pure isomorphy is to be guaranteed - simply check the is-matched-bit and be done
            // the pattern elements to check against are only needed 
            // if spn_j is allowed to be homomorph to some elements but must be isomorph to some others
            if(ssp.Operations[j].Isomorphy.CheckIsMatchedBit && !homomorphyPossibleAndAllowed)
                ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst = null;
        }

        /// <summary>
        /// fill in globally homomorphic elements as exception to global isomorphy check
        /// </summary>
        private static void FillInGlobalHomomorphyPatternElements(ScheduledSearchPlan ssp, int j)
        {
            SearchPlanNode spn_j = (SearchPlanNode)ssp.Operations[j].Element;

            if(spn_j.NodeType == PlanNodeType.Node)
            {
                if(spn_j.ElementID == -1 // inlined from independent for better matching, independent from the rest
                    || ssp.PatternGraph.totallyHomomorphicNodes[spn_j.ElementID - 1])
                {
                    ssp.Operations[j].Isomorphy.TotallyHomomorph = true;
                    return; // iso-exceptions to totally hom are handled with non-global iso checks
                }
            }
            else
            {
                if(spn_j.ElementID == -1 // inlined from independent for better matching, independent from the rest
                    || ssp.PatternGraph.totallyHomomorphicEdges[spn_j.ElementID - 1])
                {
                    ssp.Operations[j].Isomorphy.TotallyHomomorph = true;
                    return; // iso-exceptions to totally hom are handled with non-global iso checks
                }
            }

            bool[,] homGlobal;
            if(spn_j.NodeType == PlanNodeType.Node)
                homGlobal = ssp.PatternGraph.homomorphicNodesGlobal;
            else // (spn_j.NodeType == PlanNodeType.Edge)
                homGlobal = ssp.PatternGraph.homomorphicEdgesGlobal;

            // iterate through the operations before our position
            for(int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if(ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern
                    || ssp.Operations[i].Type == SearchOperationType.IndependentPattern
                    || ssp.Operations[i].Type == SearchOperationType.Assign 
                    || ssp.Operations[i].Type == SearchOperationType.AssignVar
                    || ssp.Operations[i].Type == SearchOperationType.DefToBeYieldedTo
                    || ssp.Operations[i].Type == SearchOperationType.InlinedIndependentCheckForDuplicateMatch)
                {
                    continue;
                }

                SearchPlanNode spn_i = (SearchPlanNode)ssp.Operations[i].Element;

                if(spn_i.NodeType != spn_j.NodeType)
                {
                    // don't compare nodes with edges
                    continue;
                }

                if(spn_i.ElementID == -1)
                {
                    // inlined from independent for better matching, independent from the rest
                    continue;
                }
           
                // in global isomorphy check at current position 
                // allow globally homomorphic elements as exception
                // if they were already defined(preset)
                if(homGlobal[spn_j.ElementID - 1, spn_i.ElementID - 1])
                {
                    if(ssp.Operations[j].Isomorphy.GloballyHomomorphPatternElements == null)
                        ssp.Operations[j].Isomorphy.GloballyHomomorphPatternElements = new List<SearchPlanNode>();
                    ssp.Operations[j].Isomorphy.GloballyHomomorphPatternElements.Add(spn_i);
                }
            }
        }

        /// <summary>
        /// Negative/Independent schedules are merged as an operation into their enclosing schedules,
        /// at a position determined by their costs but not before all of their needed elements were computed
        /// </summary>
        public static void MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(PatternGraph patternGraph, bool lazyNegativeIndependentConditionEvaluation)
        {
            foreach(PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(neg, lazyNegativeIndependentConditionEvaluation);
            }

            foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(idpt, lazyNegativeIndependentConditionEvaluation);
            }

            foreach(Alternative alt in patternGraph.alternativesPlusInlined)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(altCase, lazyNegativeIndependentConditionEvaluation);
                }
            }

            foreach(Iterated iter in patternGraph.iteratedsPlusInlined)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(iter.iteratedPattern, lazyNegativeIndependentConditionEvaluation);
            }

            InsertNegativesAndIndependentsIntoSchedule(patternGraph, lazyNegativeIndependentConditionEvaluation);
        }

        /// <summary>
        /// Inserts schedules of negative and independent pattern graphs into the schedule of the enclosing pattern graph
        /// </summary>
        private static void InsertNegativesAndIndependentsIntoSchedule(PatternGraph patternGraph, bool lazyNegativeIndependentConditionEvaluation)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                InsertNegativesAndIndependentsIntoSchedule(patternGraph, i, lazyNegativeIndependentConditionEvaluation);
            }
        }

        /// <summary>
        /// Inserts schedules of negative and independent pattern graphs into the schedule of the enclosing pattern graph
        /// for the schedule with the given array index
        /// </summary>
        private static void InsertNegativesAndIndependentsIntoSchedule(PatternGraph patternGraph, int index, bool lazyNegativeIndependentConditionEvaluation)
        {
            // todo: erst implicit node, dann negative/independent, auch wenn negative/independent mit erstem implicit moeglich wird
            patternGraph.schedulesIncludingNegativesAndIndependents[index] = null; // an explain might have filled this

            List<SearchOperation> operations = new List<SearchOperation>();
            for(int i = 0; i < patternGraph.schedules[index].Operations.Length; ++i)
            {
                operations.Add(patternGraph.schedules[index].Operations[i]);
            }

            // nested patterns on the way to an enclosed patternpath modifier 
            // must get matched after all local nodes and edges, because they require 
            // all outer elements to be known in order to lock them for patternpath processing
            if(patternGraph.patternGraphsOnPathToEnclosedPatternpath
                .Contains(patternGraph.pathPrefix + patternGraph.name))
            {
                operations.Add(new SearchOperation(SearchOperationType.LockLocalElementsForPatternpath, null, null,
                    patternGraph.schedules[index].Operations.Length!=0 ? patternGraph.schedules[index].Operations[patternGraph.schedules[index].Operations.Length-1].CostToEnd : 0));
            }

            // iterate over all negative scheduled search plans (TODO: order?)
            for(int i = 0; i < patternGraph.negativePatternGraphsPlusInlined.Length; ++i)
            {
                ScheduledSearchPlan negSchedule = patternGraph.negativePatternGraphsPlusInlined[i].schedulesIncludingNegativesAndIndependents[0];
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                // find best place in scheduled search plan for current negative pattern 
                // during search from end of schedule forward until the first element the negative pattern is dependent on is found
                for(int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if(op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern
                        || op.Type == SearchOperationType.AssignVar)
                    {
                        continue;
                    }

                    if(lazyNegativeIndependentConditionEvaluation)
                        break;

                    if(op.Type == SearchOperationType.LockLocalElementsForPatternpath
                        || op.Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        break; // LockLocalElementsForPatternpath and DefToBeYieldedTo are barriers for neg/idpt
                    }

                    if(patternGraph.negativePatternGraphsPlusInlined[i].neededNodes.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name)
                        || patternGraph.negativePatternGraphsPlusInlined[i].neededEdges.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
                    {
                        break;
                    }

                    if(negSchedule.Cost <= op.CostToEnd)
                    {
                        // best fit as CostToEnd is monotonously growing towards operation[0]
                        bestFitIndex = j;
                        bestFitCostToEnd = op.CostToEnd;
                    }
                }

                // insert pattern at best position
                operations.Insert(bestFitIndex, new SearchOperation(SearchOperationType.NegativePattern,
                    negSchedule, null, bestFitCostToEnd + negSchedule.Cost));

                // update costs of operations before best position
                for(int j = 0; j < bestFitIndex; ++j)
                {
                    operations[j].CostToEnd += negSchedule.Cost;
                }
            }

            // iterate over all independent scheduled search plans (TODO: order?)
            for(int i = 0; i < patternGraph.independentPatternGraphsPlusInlined.Length; ++i)
            {
                ScheduledSearchPlan idptSchedule = patternGraph.independentPatternGraphsPlusInlined[i].schedulesIncludingNegativesAndIndependents[0];
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                IDictionary<PatternElement, SetValueType> presetsFromIndependentInlining = ExtractOwnElements(patternGraph.schedules[index], patternGraph.independentPatternGraphsPlusInlined[i]);

                // find best place in scheduled search plan for current independent pattern 
                // during search from end of schedule forward until the first element the independent pattern is dependent on is found
                for(int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if(op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern
                        || op.Type == SearchOperationType.AssignVar)
                    {
                        continue;
                    }

                    if(lazyNegativeIndependentConditionEvaluation)
                        break;

                    if(op.Type == SearchOperationType.LockLocalElementsForPatternpath
                        || op.Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        break; // LockLocalElementsForPatternpath and DefToBeYieldedTo are barriers for neg/idpt
                    }

                    PatternElement pe = ((SearchPlanNode)op.Element).PatternElement;
                    if(patternGraph.independentPatternGraphsPlusInlined[i].neededNodes.ContainsKey(pe.Name)
                        || patternGraph.independentPatternGraphsPlusInlined[i].neededEdges.ContainsKey(pe.Name))
                    {
                        break;
                    }

                    if(pe.OriginalIndependentElement != null 
                        && presetsFromIndependentInlining.ContainsKey(pe.OriginalIndependentElement))
                    {
                        break;
                    }

                    if(idptSchedule.Cost <= op.CostToEnd)
                    {
                        // best fit as CostToEnd is monotonously growing towards operation[0]
                        bestFitIndex = j;
                        bestFitCostToEnd = op.CostToEnd;
                    }
                }

                // insert pattern at best position
                operations.Insert(bestFitIndex, new SearchOperation(SearchOperationType.IndependentPattern,
                    idptSchedule, null, bestFitCostToEnd + idptSchedule.Cost));

                // update costs of operations before best position
                for(int j = 0; j < bestFitIndex; ++j)
                {
                    operations[j].CostToEnd += idptSchedule.Cost;
                }
            }

            InsertInlinedIndependentCheckForDuplicateMatch(operations);

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            patternGraph.schedulesIncludingNegativesAndIndependents[index] =
                new ScheduledSearchPlan(patternGraph, operations.ToArray(), cost);
        }

        private static void InsertInlinedIndependentCheckForDuplicateMatch(List<SearchOperation> operations)
        {
            bool isInlinedIndependentElementExisting = false;
            foreach(SearchOperation op in operations)
            {
                if(SearchPlanGraphGeneratorAndScheduler.IsOperationAnInlinedIndependentElement(op))
                {
                    isInlinedIndependentElementExisting = true;
                    break;
                }
            }

            if(isInlinedIndependentElementExisting)
            {
                // insert at end of schedule, just moved ahead over negatives and independents
                // TODO: if we can estimate condition overhead, it makes sense to move ahead over conditions, too
                int i = operations.Count - 1;
                while((operations[i].Type == SearchOperationType.NegativePattern
                        || operations[i].Type == SearchOperationType.IndependentPattern) && i > 0)
                {
                    --i;
                }

                SearchOperation so = new SearchOperation(SearchOperationType.InlinedIndependentCheckForDuplicateMatch,
                    null, null, operations[i].CostToEnd);
                operations.Insert(i + 1, so);
            }
        }

        /// <summary>
        /// Parallelize the scheduled search plan if it is to be parallelized.
        /// An action to be parallelized is split at the first loop into a header part and a body part,
        /// all subpatterns and nested patterns to be parallelized are switched to non- is-matched-flag-based isomorphy checking.
        /// </summary>
        public static void ParallelizeAsNeeded(LGSPMatchingPattern matchingPattern)
        {
            if(matchingPattern.patternGraph.branchingFactor < 2)
                return;

            if(matchingPattern is LGSPRulePattern)
            {
                bool parallelizableLoopFound = false;
                foreach(SearchOperation so in matchingPattern.patternGraph.schedulesIncludingNegativesAndIndependents[0].Operations)
                {
                    if(so.Type == SearchOperationType.Lookup || so.Type == SearchOperationType.Incident
                        || so.Type == SearchOperationType.Incoming || so.Type == SearchOperationType.Outgoing
                        || so.Type == SearchOperationType.PickFromStorage || so.Type == SearchOperationType.PickFromStorageDependent
                        || so.Type == SearchOperationType.PickFromIndex || so.Type == SearchOperationType.PickFromIndexDependent)
                    {
                        parallelizableLoopFound = true;
                        break;
                    }
                }
                if(parallelizableLoopFound)
                    ParallelizeHeadBody((LGSPRulePattern)matchingPattern);
                else {
                    matchingPattern.patternGraph.branchingFactor = 1;
                    ConsoleUI.errorOutWriter.WriteLine("Warning: " + matchingPattern.patternGraph.Name + " not parallelized as no parallelizable loop was found.");
                }
            }
            else
                Parallelize(matchingPattern);
        }

        /// <summary>
        /// Parallelize the scheduled search plan to the branching factor,
        /// splitting it at the first loop into a header part and a body part
        /// </summary>
        public static void ParallelizeHeadBody(LGSPRulePattern rulePattern)
        {
            Debug.Assert(rulePattern.patternGraph.schedulesIncludingNegativesAndIndependents.Length == 1);
            ScheduledSearchPlan ssp = rulePattern.patternGraph.schedulesIncludingNegativesAndIndependents[0];
            
            int indexToSplitAt = 0;
            for(int i = 0; i < ssp.Operations.Length; ++i)
            {
                SearchOperation so = ssp.Operations[i];
                if(so.Type == SearchOperationType.Lookup || so.Type == SearchOperationType.Incident
                    || so.Type == SearchOperationType.Incoming || so.Type == SearchOperationType.Outgoing
                    || so.Type == SearchOperationType.PickFromStorage || so.Type == SearchOperationType.PickFromStorageDependent
                    || so.Type == SearchOperationType.PickFromIndex || so.Type == SearchOperationType.PickFromIndexDependent)
                {
                    indexToSplitAt = i;
                    break;
                }
            }

            rulePattern.patternGraph.parallelizedSchedule = new ScheduledSearchPlan[2];
            List<SearchOperation> headOperations = new List<SearchOperation>();
            List<SearchOperation> bodyOperations = new List<SearchOperation>();
            for(int i = 0; i < rulePattern.Inputs.Length; ++i)
            {
                if(rulePattern.Inputs[i] is VarType) // those don't appear in the schedule, they are only extracted into the search program
                {
                    VarType varType = (VarType)rulePattern.Inputs[i];
                    String varName = rulePattern.InputNames[i];
                    PatternVariable dummy = new PatternVariable(varType, varName, varName, i, false, null);
                    headOperations.Add(new SearchOperation(SearchOperationType.WriteParallelPresetVar,
                        dummy, null, 0));
                    bodyOperations.Add(new SearchOperation(SearchOperationType.ParallelPresetVar,
                        dummy, null, 0));
                }
            }
            for(int i = 0; i < ssp.Operations.Length; ++i)
            {
                SearchOperation so = ssp.Operations[i];
                if(i < indexToSplitAt)
                {
                    SearchOperation clone = (SearchOperation)so.Clone();
                    clone.Isomorphy.Parallel = true;
                    clone.Isomorphy.LockForAllThreads = true;
                    headOperations.Add(clone);
                    switch(so.Type)
                    {
                        // the target binding looping operations can't appear in the header, so we don't treat them here
                        // the non-target binding operations are completely handled by just adding them, happended already above
                        // the target binding non-looping operations are handled below,
                        // by parallel preset writing in the header and reading in the body
                        // with exception of def, its declaration and initializion is just re-executed in the body
                        // some presets can't appear in an action header, they are thus not taken care of
                    case SearchOperationType.ActionPreset:
                    case SearchOperationType.MapWithStorage:
                    case SearchOperationType.MapWithStorageDependent:
                    case SearchOperationType.Cast:
                    case SearchOperationType.Assign:
                    case SearchOperationType.Identity:
                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                    case SearchOperationType.Implicit:
                        headOperations.Add(new SearchOperation(SearchOperationType.WriteParallelPreset,
                            (SearchPlanNode)so.Element, so.SourceSPNode, 0));
                        bodyOperations.Add(new SearchOperation(SearchOperationType.ParallelPreset, 
                            (SearchPlanNode)so.Element, so.SourceSPNode, 0));
                        break;
                    case SearchOperationType.AssignVar:
                        headOperations.Add(new SearchOperation(SearchOperationType.WriteParallelPresetVar,
                            (PatternVariable)so.Element, so.SourceSPNode, 0));
                        bodyOperations.Add(new SearchOperation(SearchOperationType.ParallelPresetVar,
                            (PatternVariable)so.Element, so.SourceSPNode, 0));
                        break;
                    case SearchOperationType.DefToBeYieldedTo:
                        bodyOperations.Add((SearchOperation)so.Clone());
                        break;
                    }                    
                }
                else if(i == indexToSplitAt)
                {
                    SearchOperation cloneHead;
                    SearchOperation cloneBody;
                    switch(so.Type)
                    {
                    case SearchOperationType.Lookup:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelLookup);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelLookup);
                        break;
                    case SearchOperationType.Incident:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelIncident);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelIncident);
                        break;
                    case SearchOperationType.Incoming:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelIncoming);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelIncoming);
                        break;
                    case SearchOperationType.Outgoing:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelOutgoing);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelOutgoing);
                        break;
                    case SearchOperationType.PickFromStorage:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelPickFromStorage);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelPickFromStorage);
                        break;
                    case SearchOperationType.PickFromStorageDependent:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelPickFromStorageDependent);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelPickFromStorageDependent);
                        break;
                    case SearchOperationType.PickFromIndex:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelPickFromIndex);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelPickFromIndex);
                        break;
                    case SearchOperationType.PickFromIndexDependent:
                        cloneHead = (SearchOperation)so.Clone(SearchOperationType.SetupParallelPickFromIndexDependent);
                        cloneBody = (SearchOperation)so.Clone(SearchOperationType.ParallelPickFromIndexDependent);
                        break;
                    default: // failure, operation at this index cannot be parallelized/parallelization not supported
                        cloneHead = null;
                        cloneBody = null;
                        break;
                    }
                    headOperations.Add(cloneHead);
                    cloneBody.Isomorphy.Parallel = true;
                    bodyOperations.Add(cloneBody);
                }
                else
                {
                    SearchOperation clone = (SearchOperation)so.Clone();
                    clone.Isomorphy.Parallel = true;
                    bodyOperations.Add(clone);
                    if(clone.Element is PatternCondition)
                        SetNeedForParallelizedVersion((clone.Element as PatternCondition).ConditionExpression);
                }
            }
            ScheduledSearchPlan headSsp = new ScheduledSearchPlan(
                rulePattern.patternGraph, headOperations.ToArray(), headOperations.Count > 0 ? headOperations[0].CostToEnd : 0);
            rulePattern.patternGraph.parallelizedSchedule[0] = headSsp;
            ScheduledSearchPlan bodySsp = new ScheduledSearchPlan(
                rulePattern.patternGraph, bodyOperations.ToArray(), bodyOperations.Count > 0 ? bodyOperations[0].CostToEnd : 0);
            rulePattern.patternGraph.parallelizedSchedule[1] = bodySsp;
            ParallelizeNegativeIndependent(bodySsp);
            ParallelizeAlternativeIterated(rulePattern.patternGraph);
            ParallelizeYielding(rulePattern.patternGraph);
        }

        /// <summary>
        /// Parallelize the scheduled search plan for usage from a parallelized matcher
        /// (non- is-matched-flag-based isomorphy checking)
        /// </summary>
        public static void Parallelize(LGSPMatchingPattern matchingPattern)
        {
            Debug.Assert(matchingPattern.patternGraph.schedulesIncludingNegativesAndIndependents.Length == 1);
            ScheduledSearchPlan ssp = matchingPattern.patternGraph.schedulesIncludingNegativesAndIndependents[0];
            matchingPattern.patternGraph.parallelizedSchedule = new ScheduledSearchPlan[1];
            List<SearchOperation> operations = new List<SearchOperation>(ssp.Operations.Length);
            for(int i = 0; i < ssp.Operations.Length; ++i)
            {
                SearchOperation so = ssp.Operations[i];
                SearchOperation clone = (SearchOperation)so.Clone();
                clone.Isomorphy.Parallel = true;
                operations.Add(clone);
                if(clone.Element is PatternCondition)
                    SetNeedForParallelizedVersion((clone.Element as PatternCondition).ConditionExpression);
            }
            ScheduledSearchPlan clonedSsp = new ScheduledSearchPlan(
                matchingPattern.patternGraph, operations.ToArray(), operations.Count > 0 ? operations[0].CostToEnd : 0);
            matchingPattern.patternGraph.parallelizedSchedule[0] = clonedSsp;
            ParallelizeNegativeIndependent(clonedSsp);
            ParallelizeAlternativeIterated(matchingPattern.patternGraph);
            ParallelizeYielding(matchingPattern.patternGraph);
        }

        /// <summary>
        /// Non- is-matched-flag-based isomorphy checking for nested alternative cases/iterateds
        /// </summary>
        private static void ParallelizeAlternativeIterated(PatternGraph patternGraph)
        {
            foreach(Alternative alt in patternGraph.alternativesPlusInlined)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    ScheduledSearchPlan ssp = altCase.schedulesIncludingNegativesAndIndependents[0];
                    altCase.parallelizedSchedule = new ScheduledSearchPlan[1];
                    List<SearchOperation> operations = new List<SearchOperation>(ssp.Operations.Length);
                    for(int i = 0; i < ssp.Operations.Length; ++i)
                    {
                        SearchOperation so = ssp.Operations[i];
                        SearchOperation clone = (SearchOperation)so.Clone();
                        clone.Isomorphy.Parallel = true;
                        operations.Add(clone);
                        if(clone.Element is PatternCondition)
                            SetNeedForParallelizedVersion((clone.Element as PatternCondition).ConditionExpression);
                    }
                    ScheduledSearchPlan clonedSsp = new ScheduledSearchPlan(
                        altCase, operations.ToArray(), operations.Count > 0 ? operations[0].CostToEnd : 0);
                    altCase.parallelizedSchedule[0] = clonedSsp;

                    ParallelizeNegativeIndependent(clonedSsp);
                    ParallelizeAlternativeIterated(altCase);
                    ParallelizeYielding(altCase);
                }
            }
            foreach(Iterated iter in patternGraph.iteratedsPlusInlined)
            {
                ScheduledSearchPlan ssp = iter.iteratedPattern.schedulesIncludingNegativesAndIndependents[0];
                iter.iteratedPattern.parallelizedSchedule = new ScheduledSearchPlan[1];
                List<SearchOperation> operations = new List<SearchOperation>(ssp.Operations.Length);
                for(int i = 0; i < ssp.Operations.Length; ++i)
                {
                    SearchOperation so = ssp.Operations[i];
                    SearchOperation clone = (SearchOperation)so.Clone();
                    clone.Isomorphy.Parallel = true;
                    operations.Add(clone);
                    if(clone.Element is PatternCondition)
                        SetNeedForParallelizedVersion((clone.Element as PatternCondition).ConditionExpression);
                }
                ScheduledSearchPlan clonedSsp = new ScheduledSearchPlan(
                        iter.iteratedPattern, operations.ToArray(), operations.Count > 0 ? operations[0].CostToEnd : 0);
                iter.iteratedPattern.parallelizedSchedule[0] = clonedSsp;

                ParallelizeNegativeIndependent(clonedSsp);
                ParallelizeAlternativeIterated(iter.iteratedPattern);
                ParallelizeYielding(iter.iteratedPattern);
            }
        }

        /// <summary>
        /// Non- is-matched-flag-based isomorphy checking for nested negatives/independents, 
        /// patch into already cloned parallel ssp
        /// </summary>
        private static void ParallelizeNegativeIndependent(ScheduledSearchPlan ssp)
        {
            foreach(SearchOperation so in ssp.Operations)
            {
                so.Isomorphy.Parallel = true;

                if(so.Type == SearchOperationType.NegativePattern
                    || so.Type == SearchOperationType.IndependentPattern)
                {
                    ScheduledSearchPlan nestedSsp = (ScheduledSearchPlan)so.Element;
                    List<SearchOperation> operations = new List<SearchOperation>(nestedSsp.Operations.Length);
                    for(int i = 0; i < nestedSsp.Operations.Length; ++i)
                    {
                        SearchOperation nestedSo = nestedSsp.Operations[i];
                        SearchOperation clone = (SearchOperation)nestedSo.Clone();
                        operations.Add(clone);
                        if(clone.Element is PatternCondition)
                            SetNeedForParallelizedVersion((clone.Element as PatternCondition).ConditionExpression);
                    }
                    Debug.Assert(nestedSsp.PatternGraph.parallelizedSchedule == null); // may fire in case explain was used before, ignore it then
                    nestedSsp.PatternGraph.parallelizedSchedule = new ScheduledSearchPlan[1];
                    ScheduledSearchPlan clonedSsp = new ScheduledSearchPlan(
                        nestedSsp.PatternGraph, operations.ToArray(), operations.Count > 0 ? operations[0].CostToEnd : 0);
                    nestedSsp.PatternGraph.parallelizedSchedule[0] = clonedSsp;
                    so.Element = clonedSsp;

                    ParallelizeNegativeIndependent(clonedSsp);
                    ParallelizeAlternativeIterated(nestedSsp.PatternGraph);
                    if(so.Type == SearchOperationType.IndependentPattern)
                        ParallelizeYielding(nestedSsp.PatternGraph);
                }
            }
        }

        private static void ParallelizeYielding(PatternGraph patternGraph)
        {
            patternGraph.parallelizedYieldings = new PatternYielding[patternGraph.YieldingsPlusInlined.Length];
            for(int i = 0; i < patternGraph.YieldingsPlusInlined.Length; ++i)
            {
                patternGraph.parallelizedYieldings[i] = (PatternYielding)patternGraph.YieldingsPlusInlined[i].Clone();
                for(int j = 0; j < patternGraph.parallelizedYieldings[i].ElementaryYieldings.Length; ++j)
                {
                    SetNeedForParallelizedVersion(patternGraph.parallelizedYieldings[i].ElementaryYieldings[j]);
                }
            }
        }

        private static void SetNeedForParallelizedVersion(ExpressionOrYielding expyield)
        {
            expyield.SetNeedForParallelizedVersion(true);
            foreach(ExpressionOrYielding child in expyield)
            {
                SetNeedForParallelizedVersion(child);
            }
        }

        public static IDictionary<PatternElement, SetValueType> ExtractOwnElements(ScheduledSearchPlan nestingScheduledSearchPlan, PatternGraph patternGraph)
        {
            Dictionary<PatternElement, SetValueType> ownElements = new Dictionary<PatternElement, SetValueType>();

            // elements contained in the schedule of the nesting pattern, that are declared in the current pattern graph
            // stem from inlining an indepent, extract them to treat them specially in search plan building (preset from nesting pattern)
            if(nestingScheduledSearchPlan != null)
            {
                for(int i = 0; i < nestingScheduledSearchPlan.Operations.Length; ++i)
                {
                    if(nestingScheduledSearchPlan.Operations[i].Type == SearchOperationType.Condition
                        || nestingScheduledSearchPlan.Operations[i].Type == SearchOperationType.AssignVar
                        || nestingScheduledSearchPlan.Operations[i].Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        continue;
                    }

                    SearchPlanNode spn = (SearchPlanNode)nestingScheduledSearchPlan.Operations[i].Element;
                    if(spn.PatternElement.pointOfDefinition == patternGraph)
                    {
                        ownElements.Add(spn.PatternElement.OriginalIndependentElement, null);
                    }
                }
            }

            return ownElements;
        }
    }
}
