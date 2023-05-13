/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;


namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class for generating a search plan graph out of a plan graph and for creating a schedule out of it.
    /// </summary>
    public static class SearchPlanGraphGeneratorAndScheduler
    {
        /// <summary>
        /// Generate search plan graph out of the plan graph,
        /// search plan graph only contains edges chosen by the MSA algorithm.
        /// Edges in search plan graph are given in the nodes by outgoing list, as needed for scheduling,
        /// in contrast to incoming list in plan graph, as needed for MSA computation.
        /// </summary>
        /// <param name="planGraph">The source plan graph</param>
        /// <returns>A new search plan graph</returns>
        public static SearchPlanGraph GenerateSearchPlanGraph(PlanGraph planGraph)
        {
            SearchPlanNode searchPlanRoot = new SearchPlanNode("search plan root");
            SearchPlanNode[] searchPlanNodes = new SearchPlanNode[planGraph.Nodes.Length];
            SearchPlanEdge[] searchPlanEdges = new SearchPlanEdge[planGraph.Nodes.Length - 1 + 1]; // +1 for root
            Dictionary<PlanNode, SearchPlanNode> planToSearchPlanNode = // for generating edges
                new Dictionary<PlanNode, SearchPlanNode>(planGraph.Nodes.Length);
            planToSearchPlanNode.Add(planGraph.Root, searchPlanRoot);

            // generate the search plan graph nodes, same as plan graph nodes,
            // representing pattern graph nodes and edges
            int i = 0;
            foreach(PlanNode planNode in planGraph.Nodes)
            {
                if(planNode.NodeType == PlanNodeType.Edge)
                    searchPlanNodes[i] = new SearchPlanEdgeNode(planNode, null, null);
                else
                    searchPlanNodes[i] = new SearchPlanNodeNode(planNode);
                planToSearchPlanNode.Add(planNode, searchPlanNodes[i]);

                ++i;
            }

            // generate the search plan graph edges, 
            // that are the plan graph edges chosen by the MSA algorithm, in reversed direction
            // and add references to originating pattern elements
            i = 0;
            foreach(PlanNode planNode in planGraph.Nodes)
            {
                PlanEdge planEdge = planNode.IncomingMSAEdge;
                searchPlanEdges[i] = new SearchPlanEdge(planEdge.Type, planToSearchPlanNode[planEdge.Source], planToSearchPlanNode[planEdge.Target], planEdge.Cost);
                planToSearchPlanNode[planEdge.Source].OutgoingEdges.Add(searchPlanEdges[i]);

                if(planNode.NodeType == PlanNodeType.Edge)
                {
                    SearchPlanEdgeNode searchPlanEdgeNode = (SearchPlanEdgeNode) planToSearchPlanNode[planNode];
                    SearchPlanNode patElem;
                    if(planEdge.Target.PatternEdgeSource != null 
                        && planToSearchPlanNode.TryGetValue(planEdge.Target.PatternEdgeSource, out patElem))
                    {
                        searchPlanEdgeNode.PatternEdgeSource = (SearchPlanNodeNode) patElem;
                        searchPlanEdgeNode.PatternEdgeSource.OutgoingPatternEdges.Add(searchPlanEdgeNode);
                    }
                    if(planEdge.Target.PatternEdgeTarget != null 
                        && planToSearchPlanNode.TryGetValue(planEdge.Target.PatternEdgeTarget, out patElem))
                    {
                        searchPlanEdgeNode.PatternEdgeTarget = (SearchPlanNodeNode) patElem;
                        searchPlanEdgeNode.PatternEdgeTarget.IncomingPatternEdges.Add(searchPlanEdgeNode);
                    }
                }

                ++i;
            }

            return new SearchPlanGraph(searchPlanRoot, searchPlanNodes, searchPlanEdges);
        }

        /// <summary>
        /// Generates a scheduled search plan for a given search plan graph
        /// </summary>
        public static ScheduledSearchPlan ScheduleSearchPlan(SearchPlanGraph spGraph,
            PatternGraph patternGraph, bool isNegativeOrIndependent, bool lazyNegativeIndependentConditionEvaluation)
        {
            // the schedule
            List<SearchOperation> operations = new List<SearchOperation>();
            
            // a set of search plan edges representing the currently reachable not yet visited elements
            PriorityQueue<SearchPlanEdge> activeEdges = new PriorityQueue<SearchPlanEdge>();

            // first schedule all preset elements
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Target.IsPreset && edge.Type != SearchOperationType.DefToBeYieldedTo)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPresetElement in edge.Target.OutgoingEdges)
                    {
                        activeEdges.Add(edgeOutgoingFromPresetElement);
                    }

                    // note: here a normal preset is converted into a neg/idpt preset operation if in negative/independent pattern
                    SearchOperation newOp = new SearchOperation(
                        isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : edge.Type,
                        edge.Target, spGraph.Root, 0);
                    operations.Add(newOp);
                }
            }

            // then schedule all map with storage / pick from index / pick from storage / pick from name index elements not depending on other elements
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.MapWithStorage)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPickedElement in edge.Target.OutgoingEdges)
                    {
                        activeEdges.Add(edgeOutgoingFromPickedElement);
                    }

                    SearchOperation newOp = new SearchOperation(edge.Type,
                        edge.Target, spGraph.Root, 0);
                    newOp.Storage = edge.Target.PatternElement.Storage;
                    newOp.StorageIndex = edge.Target.PatternElement.StorageIndex;
                    operations.Add(newOp);
                }
            }
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.PickFromStorage)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPickedElement in edge.Target.OutgoingEdges)
                    {
                        activeEdges.Add(edgeOutgoingFromPickedElement);
                    }

                    SearchOperation newOp = new SearchOperation(edge.Type,
                        edge.Target, spGraph.Root, 0);
                    newOp.Storage = edge.Target.PatternElement.Storage;
                    operations.Add(newOp);
                }
            }
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.PickFromIndex)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPickedElement in edge.Target.OutgoingEdges)
                    {
                        activeEdges.Add(edgeOutgoingFromPickedElement);
                    }

                    SearchOperation newOp = new SearchOperation(edge.Type,
                        edge.Target, spGraph.Root, 0);
                    newOp.IndexAccess = edge.Target.PatternElement.IndexAccess;
                    operations.Add(newOp);
                }
            }
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.PickByName)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPickedElement in edge.Target.OutgoingEdges)
                    {
                        activeEdges.Add(edgeOutgoingFromPickedElement);
                    }

                    SearchOperation newOp = new SearchOperation(edge.Type,
                        edge.Target, spGraph.Root, 0);
                    newOp.NameLookup = edge.Target.PatternElement.NameLookup;
                    operations.Add(newOp);
                }
            }
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.PickByUnique)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPickedElement in edge.Target.OutgoingEdges)
                    {
                        activeEdges.Add(edgeOutgoingFromPickedElement);
                    }

                    SearchOperation newOp = new SearchOperation(edge.Type,
                        edge.Target, spGraph.Root, 0);
                    newOp.UniqueLookup = edge.Target.PatternElement.UniqueLookup;
                    operations.Add(newOp);
                }
            }

            // iterate over all reachable elements until the whole graph has been scheduled(/visited),
            // choose next cheapest operation, update the reachable elements and the search plan costs
            SearchPlanNode lastNode = spGraph.Root;
            for(int i = 0; i < spGraph.Nodes.Length - spGraph.NumPresetElements - spGraph.NumIndependentStorageIndexElements; ++i)
            {
                foreach(SearchPlanEdge edge in lastNode.OutgoingEdges)
                {
                    if(edge.Target.IsPreset)
                        continue;
                    if(edge.Target.PatternElement.Storage != null 
                        && edge.Target.PatternElement.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness()==null)
                        continue;
                    if(edge.Target.PatternElement.IndexAccess != null
                        && edge.Target.PatternElement.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness()==null)
                        continue;
                    if(edge.Target.PatternElement.NameLookup != null
                        && edge.Target.PatternElement.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness()==null)
                        continue;
                    if(edge.Target.PatternElement.UniqueLookup != null
                        && edge.Target.PatternElement.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness()==null)
                        continue;
                    CostDecreaseForLeavingInlinedIndependent(edge);
                    activeEdges.Add(edge);
                }

                SearchPlanEdge minEdge = activeEdges.DequeueFirst();
                lastNode = minEdge.Target;

                SearchOperation newOp = new SearchOperation(minEdge.Type,
                    lastNode, minEdge.Source, minEdge.Cost);
                newOp.Storage = minEdge.Target.PatternElement.Storage;
                newOp.StorageIndex = minEdge.Target.PatternElement.StorageIndex;
                newOp.IndexAccess = minEdge.Target.PatternElement.IndexAccess;
                newOp.NameLookup = minEdge.Target.PatternElement.NameLookup;
                newOp.UniqueLookup = minEdge.Target.PatternElement.UniqueLookup;

                foreach(SearchOperation op in operations)
                {
                    op.CostToEnd += minEdge.Cost;
                }

                operations.Add(newOp);
            }

            // remove the elements stemming from inlined independents
            // they were added in the hope that they might help in matching this pattern,
            // they don't if they are matched after the elements of this pattern (in fact they only increase the costs then)
            RemoveInlinedIndependentElementsAtEnd(operations);

            // insert inlined element identity check into the schedule in case neither of the possible assignments was scheduled
            InsertInlinedElementIdentityCheckIntoSchedule(patternGraph, operations);

            // insert inlined variable assignments into the schedule
            InsertInlinedVariableAssignmentsIntoSchedule(patternGraph, operations);

            // insert conditions into the schedule
            InsertConditionsIntoSchedule(patternGraph.ConditionsPlusInlined, operations, lazyNegativeIndependentConditionEvaluation);

            // schedule the initialization of all def to be yielded to elements and variables at the end,
            // must come after the pattern elements (and preset elements), as they may be used in the def initialization
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.DefToBeYieldedTo
                    && (!isNegativeOrIndependent || (edge.Target.PatternElement.pointOfDefinition == patternGraph && edge.Target.PatternElement.originalElement == null)))
                {
                    SearchOperation newOp = new SearchOperation(
                        SearchOperationType.DefToBeYieldedTo,
                        edge.Target, spGraph.Root, 0);
                    newOp.Expression = edge.Target.PatternElement.Initialization;
                    operations.Add(newOp);
                }
            }
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(var.defToBeYieldedTo
                    && (!isNegativeOrIndependent || var.pointOfDefinition == patternGraph && var.originalVariable == null))
                {
                    SearchOperation newOp = new SearchOperation(
                        SearchOperationType.DefToBeYieldedTo,
                        var, spGraph.Root, 0);
                    newOp.Expression = var.initialization;
                    operations.Add(newOp);
                }
            }

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            return new ScheduledSearchPlan(patternGraph, operations.ToArray(), cost);
        }

        private static void CostDecreaseForLeavingInlinedIndependent(SearchPlanEdge planEdge)
        {
            // considerably lower the costs for operations leaving the inlined independent part, so this is done early
            if(planEdge.Target is SearchPlanEdgeNode) // we may leave the independent part only with an edge
            {
                if(planEdge.Source.PatternElement.OriginalIndependentElement != null) // when the start is a node from an inlined independent
                {
                    SearchPlanEdgeNode edge = (SearchPlanEdgeNode)planEdge.Target;
                    if(edge.PatternEdgeSource.PatternElement.OriginalIndependentElement != null && edge.PatternEdgeTarget.PatternElement.OriginalIndependentElement == null
                        || edge.PatternEdgeSource.PatternElement.OriginalIndependentElement == null && edge.PatternEdgeTarget.PatternElement.OriginalIndependentElement != null)
                    { // and the edge is incident to elements inside and outside of the inlined independent
                        planEdge.Cost = (float)Math.Sqrt(Math.Sqrt(planEdge.Cost));
                    }
                }
            }
        }

        private static void RemoveInlinedIndependentElementsAtEnd(List<SearchOperation> operations)
        {
            while(operations.Count > 0 &&
                IsOperationAnInlinedIndependentElement(operations[operations.Count - 1]))
            {
                foreach(SearchOperation op in operations)
                {
                    op.CostToEnd -= operations[operations.Count - 1].CostToEnd;
                }

                operations.RemoveAt(operations.Count-1);
            }
        }

        public static bool IsOperationAnInlinedIndependentElement(SearchOperation so)
        {
            if(so.Element is SearchPlanNode)
            {
                if(((SearchPlanNode)so.Element).PatternElement.OriginalIndependentElement != null)
                    return true;
            }

            return false;
        }

        private static void InsertInlinedElementIdentityCheckIntoSchedule(PatternGraph patternGraph, List<SearchOperation> operations)
        {
            for(int i = 0; i < operations.Count; ++i)
            {
                PatternElement assignmentSource = null;
                if(operations[i].Element is SearchPlanNode)
                    assignmentSource = ((SearchPlanNode)operations[i].Element).PatternElement.AssignmentSource;
                if(assignmentSource != null && operations[i].Type != SearchOperationType.Identity)
                {
                    for(int j = 0; j < operations.Count; ++j)
                    {
                        SearchPlanNode binder = null;
                        if(operations[j].Element is SearchPlanNode)
                            binder = (SearchPlanNode)operations[j].Element;
                        if(binder != null
                            && binder.PatternElement == assignmentSource
                            && operations[j].Type != SearchOperationType.Identity)
                        {
                            if(operations[i].Type != SearchOperationType.Assign
                                && operations[j].Type != SearchOperationType.Assign)
                            {
                                int indexOfSecond = Math.Max(i, j);
                                SearchOperation so = new SearchOperation(SearchOperationType.Identity,
                                    operations[i].Element, binder, operations[indexOfSecond].CostToEnd);
                                operations.Insert(indexOfSecond + 1, so);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Inserts conditions into the schedule given by the operations list at their earliest possible position
        /// todo: set/map operations are potentially expensive, they shouldn't be insertes asap, but depending an weight, 
        /// derived from statistics over set/map size for graph elements, quiet well known for anonymous rule sets
        /// </summary>
        private static void InsertConditionsIntoSchedule(PatternCondition[] conditions, List<SearchOperation> operations, bool lazyNegativeIndependentConditionEvaluation)
        {
            // get needed (in order to evaluate it) elements of each condition 
            Dictionary<String, object>[] neededElements = new Dictionary<String, object>[conditions.Length];
            for(int i = 0; i < conditions.Length; ++i)
            {
                neededElements[i] = new Dictionary<string, object>();
                for(int j = 0; j < conditions[i].NeededNodeNames.Length; ++j)
                {
                    String neededNodeName = conditions[i].NeededNodeNames[j];
                    PatternNode neededNode = conditions[i].NeededNodes[j];
                    neededElements[i][neededNodeName] = neededNode;
                }
                for(int j = 0; j < conditions[i].NeededEdgeNames.Length; ++j)
                {
                    String neededEdgeName = conditions[i].NeededEdgeNames[j];
                    PatternEdge neededEdge = conditions[i].NeededEdges[j];
                    neededElements[i][neededEdgeName] = neededEdge;
                }
                for(int j = 0; j <conditions[i].NeededVariableNames.Length; ++j)
                {
                    String neededVariableName = conditions[i].NeededVariableNames[j];
                    PatternVariable neededVariable = conditions[i].NeededVariables[j];
                    neededElements[i][neededVariableName] = neededVariable;
                }
            }

            // iterate over all conditions
            for(int i = 0; i < conditions.Length; ++i)
            {
                int j;
                float costToEnd = 0;

                // find leftmost place in scheduled search plan for current condition
                // by search from end of schedule forward until the first element the condition is dependent on is found
                for(j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if(op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern
                        || op.Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        continue;
                    }

                    if(lazyNegativeIndependentConditionEvaluation)
                        break;

                    if(op.Type == SearchOperationType.AssignVar)
                    {
                        if(neededElements[i].ContainsKey(((PatternVariable)op.Element).Name))
                        {
                            costToEnd = op.CostToEnd;
                            break;
                        }
                        continue;
                    }

                    if(neededElements[i].ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
                    {
                        costToEnd = op.CostToEnd;
                        break;
                    }
                }

                operations.Insert(j + 1, new SearchOperation(SearchOperationType.Condition,
                    conditions[i], null, costToEnd));
            }
        }

        /// <summary>
        /// Inserts inlined variable assignments into the schedule given by the operations list at their earliest possible position
        /// </summary>
        private static void InsertInlinedVariableAssignmentsIntoSchedule(PatternGraph patternGraph, List<SearchOperation> operations)
        {
            // compute the number of inlined parameter variables
            int numInlinedParameterVariables = 0;
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(var.AssignmentSource != null && patternGraph.WasInlinedHere(var.originalSubpatternEmbedding))
                    ++numInlinedParameterVariables;
            }

            if(numInlinedParameterVariables == 0)
                return;

            // get the inlined parameter variables and the elements needed in order to compute their defining expression
            Dictionary<String, PatternElement>[] neededElements = new Dictionary<String, PatternElement>[numInlinedParameterVariables];
            PatternVariable[] inlinedParameterVariables = new PatternVariable[numInlinedParameterVariables];
            int curInlParamVar = 0;
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(var.AssignmentSource == null)
                    continue;
                if(!patternGraph.WasInlinedHere(var.originalSubpatternEmbedding))
                    continue;

                neededElements[curInlParamVar] = new Dictionary<string, PatternElement>();
                for(int i = 0;  i < var.AssignmentDependencies.neededNodeNames.Length; ++i)
                {
                    String neededNodeName = var.AssignmentDependencies.neededNodeNames[i];
                    PatternNode neededNode = var.AssignmentDependencies.neededNodes[i];
                    neededElements[curInlParamVar][neededNodeName] = neededNode;
                }
                for(int i = 0; i < var.AssignmentDependencies.neededEdgeNames.Length; ++i)
                {
                    String neededEdgeName = var.AssignmentDependencies.neededEdgeNames[i];
                    PatternEdge neededEdge = var.AssignmentDependencies.neededEdges[i];
                    neededElements[curInlParamVar][neededEdgeName] = neededEdge;
                }
                inlinedParameterVariables[curInlParamVar] = var;
                
                ++curInlParamVar;
            }

            // iterate over all inlined parameter variables
            for(int i = 0; i < inlinedParameterVariables.Length; ++i)
            {
                int j;
                float costToEnd = 0;

                // find leftmost place in scheduled search plan for current assignment
                // by search from end of schedule forward until the first element the expression assigned is dependent on is found
                for(j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if(op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.Assign
                        || op.Type == SearchOperationType.AssignVar
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern
                        || op.Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        continue;
                    }

                    if(neededElements[i].ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
                    {
                        costToEnd = op.CostToEnd;
                        break;
                    }
                }

                SearchOperation so = new SearchOperation(SearchOperationType.AssignVar,
                    inlinedParameterVariables[i], null, costToEnd);
                so.Expression = inlinedParameterVariables[i].AssignmentSource;
                operations.Insert(j + 1, so);
            }
        }
    }
}
