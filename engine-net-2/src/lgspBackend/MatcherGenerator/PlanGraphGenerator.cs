/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

#define MONO_MULTIDIMARRAY_WORKAROUND       // must be equally set to the same flag in lgspGraphStatistics.cs!

using System;
using System.Collections.Generic;

using System.IO;
using de.unika.ipd.grGen.libGr;


namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class for generating a plan graph out of a pattern graph.
    /// </summary>
    public static class PlanGraphGenerator
    {
        /// <summary>
        /// Generate plan graph for given pattern graph with costs from the analyzed host graph.
        /// Plan graph contains nodes representing the pattern elements (nodes and edges)
        /// and edges representing the matching operations to get the elements by.
        /// Edges in plan graph are given in the nodes by incoming list, as needed for MSA computation.
        /// </summary>
        public static PlanGraph GeneratePlanGraph(IGraphModel model, LGSPGraphStatistics graphStatistics, PatternGraph patternGraph,
            bool isNegativeOrIndependent, bool isSubpatternLike, bool InlineIndependents,
            IDictionary<PatternElement, SetValueType> presetsFromIndependentInlining)
        {
            // 
            // If you change this method, chances are high you also want to change GenerateStaticPlanGraph
            // look there for version without ifdef junk
            // todo: unify it with GenerateStaticPlanGraph
            // 

            // Create root node
            // Create plan graph nodes for all pattern graph nodes and all pattern graph edges
            // Create "lookup" plan graph edge from root node to each plan graph node
            // Create "implicit source" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the source node of the pattern graph edge
            // Create "implicit target" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the target node of the pattern graph edge
            // Create "incoming" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the incoming edges of the pattern node
            // Create "outgoing" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the outgoing edges of the pattern node
            // Ensured: there's no plan graph edge with a preset element as target besides the lookup,
            //     so presets are only search operation sources
            // Create "pick from storage" plan graph edge for plan graph nodes which are to be picked from a storage,
            //     from root node on, instead of lookup, no other plan graph edge having this node as target
            // Create "pick from storage attribute" plan graph edge from storage attribute owner to storage picking result,
            //     no lookup, no other plan graph edge having this node as target
            // Create "map" by storage plan graph edge from accessor to storage mapping result
            //     no lookup, no other plan graph edge having this node as target
            // Create "pick from index" plan graph edge for plan graph nodes which are to be picked from an index,
            //     from root node on, instead of lookup, no other plan graph edge having this node as target
            // Create "pick from index depending" plan graph edge from node the index expressions depend on,
            //     no lookup, no other plan graph edge having this node as target
            // Create "cast" plan graph edge from element before casting to cast result,
            //     no lookup, no other plan graph edge having this node as target

            int numNodes = patternGraph.nodesPlusInlined.Length;
            if(InlineIndependents && !isNegativeOrIndependent)
                numNodes += PlanGraphGenerator.NumNodesInIndependentsMatchedThere(patternGraph);
            int numEdges = patternGraph.edgesPlusInlined.Length;
            if(InlineIndependents && !isNegativeOrIndependent)
                numEdges += PlanGraphGenerator.NumEdgesInIndependentsMatchedThere(patternGraph);
            PlanNode[] planNodes = new PlanNode[numNodes + numEdges];
            List<PlanEdge> planEdges = new List<PlanEdge>(numNodes + 5 * numEdges); // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)

            Dictionary<PatternNode, PatternNode> originalToInlinedIndependent = new Dictionary<PatternNode, PatternNode>();

            int nodesIndex = 0;
            float zeroCost = 1;

            PlanNode planRoot = new PlanNode("root");

            // create plan nodes and lookup plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; i++)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];

                PlanNode planNode;
                PlanEdge rootToNodePlanEdge;
                CreatePlanNodeAndLookupPlanEdge(node, i + 1, 
                    graphStatistics, patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                    originalToInlinedIndependent, presetsFromIndependentInlining,
                    out planNode, out rootToNodePlanEdge);

                planNodes[nodesIndex] = planNode;
                if(rootToNodePlanEdge != null)
                {
                    planEdges.Add(rootToNodePlanEdge);
                    planNode.IncomingEdges.Add(rootToNodePlanEdge);
                }

                node.TempPlanMapping = planNode;
                ++nodesIndex;
            }
            if(InlineIndependents && !isNegativeOrIndependent) // independent inlining only if not in negative/independent
            {
                foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
                {
                    foreach(PatternNode nodeOriginal in PlanGraphGenerator.NodesInIndependentMatchedThere(independent))
                    {
                        PatternNode node = new PatternNode(nodeOriginal, "_inlined_" + independent.name);
                        originalToInlinedIndependent.Add(nodeOriginal, node);

                        PlanNode planNode;
                        PlanEdge rootToNodePlanEdge;
                        CreatePlanNodeAndLookupPlanEdge(node, -1,
                            graphStatistics, patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                            originalToInlinedIndependent, presetsFromIndependentInlining,
                            out planNode, out rootToNodePlanEdge);

                        planNodes[nodesIndex] = planNode;
                        if(rootToNodePlanEdge != null)
                        {
                            planEdges.Add(rootToNodePlanEdge);
                            planNode.IncomingEdges.Add(rootToNodePlanEdge);
                        }

                        node.TempPlanMapping = planNode;
                        ++nodesIndex;
                    }
                }
            }

            // create plan nodes and lookup plus incidence handling plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];

                bool isPreset;
                PlanNode planNode;
                PlanEdge rootToNodePlanEdge;
                CreatePlanNodeAndLookupPlanEdge(edge, i + 1, 
                    graphStatistics, patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                    originalToInlinedIndependent, presetsFromIndependentInlining,
                    out isPreset, out planNode, out rootToNodePlanEdge);

                planNodes[nodesIndex] = planNode;
                if(rootToNodePlanEdge != null)
                {
                    planEdges.Add(rootToNodePlanEdge);
                    planNode.IncomingEdges.Add(rootToNodePlanEdge);
                }

                CreateSourceTargetIncomingOutgoingPlanEdges(edge, planNode, planEdges,
                    originalToInlinedIndependent, model, graphStatistics, patternGraph, isPreset, zeroCost);

                edge.TempPlanMapping = planNode;
                ++nodesIndex;
            }
            if(InlineIndependents && !isNegativeOrIndependent) // independent inlining only if not in negative/independent
            {
                foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
                {
                    foreach(PatternEdge edgeOriginal in PlanGraphGenerator.EdgesInIndependentMatchedThere(independent))
                    {
                        PatternEdge edge = new PatternEdge(edgeOriginal, "_inlined_" + independent.name);

                        bool isPreset;
                        PlanNode planNode;
                        PlanEdge rootToNodePlanEdge;
                        CreatePlanNodeAndLookupPlanEdge(edge, -1,
                            graphStatistics, patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                            originalToInlinedIndependent, presetsFromIndependentInlining,
                            out isPreset, out planNode, out rootToNodePlanEdge);

                        planNodes[nodesIndex] = planNode;
                        if(rootToNodePlanEdge != null)
                        {
                            planEdges.Add(rootToNodePlanEdge);
                            planNode.IncomingEdges.Add(rootToNodePlanEdge);
                        }

                        CreateSourceTargetIncomingOutgoingPlanEdges(edge, planNode, planEdges,
                            originalToInlinedIndependent, model, graphStatistics, patternGraph, isPreset, zeroCost);

                        edge.TempPlanMapping = planNode;
                        ++nodesIndex;
                    }
                }
            }

            ////////////////////////////////////////////////////////////////////////////
            // second run handling dependent storage and index picking (can't be done in first run due to dependencies between elements)

            // create map/pick/cast/assign plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];

                if(node.PointOfDefinition == patternGraph && !presetsFromIndependentInlining.ContainsKey(node))
                    CreatePickMapCastAssignPlanEdges(node, planEdges, zeroCost);
            }

            // create map/pick/cast/assign plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];

                if(edge.PointOfDefinition == patternGraph && !presetsFromIndependentInlining.ContainsKey(edge))
                    CreatePickMapCastAssignPlanEdges(edge, planEdges, zeroCost);
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray());
        }

        private static void CreatePlanNodeAndLookupPlanEdge(PatternNode node, int elemId, 
            LGSPGraphStatistics graphStatistics, PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpatternLike, PlanNode planRoot, float zeroCost,
            IDictionary<PatternNode, PatternNode> originalToInlinedIndependent, IDictionary<PatternElement, SetValueType> presetsFromIndependentInlining,
            out PlanNode planNode, out PlanEdge rootToNodePlanEdge)
        {
            float cost;
            bool isPreset;
            SearchOperationType searchOperationType;
            if(node.DefToBeYieldedTo)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.DefToBeYieldedTo;
            }
            else if(node.PointOfDefinition == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isSubpatternLike ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
            }
            else if(node.PointOfDefinition != patternGraph && node.OriginalIndependentElement == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
            }
            else if(presetsFromIndependentInlining.ContainsKey(node))
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.NegIdptPreset;
                node.PresetBecauseOfIndependentInlining = true;
            }
            else if(node.Storage != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                }
            }
            else if(node.IndexAccess != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromIndex; // pick from index instead of lookup from graph
                }
            }
            else if(node.NameLookup != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByName; // pick by name instead of lookup from graph
                }
            }
            else if(node.UniqueLookup != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByUnique; // pick by unique instead of lookup from graph
                }
            }
            else if(node.ElementBeforeCasting != null)
            {
                cost = zeroCost;
                isPreset = false;
                searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
            }
            else
            {
                cost = graphStatistics.nodeCounts[node.TypeID];
                cost = CostIncreaseForInlinedIndependent(node, cost);
                isPreset = false;
                searchOperationType = SearchOperationType.Lookup;
            }

            planNode = new PlanNode(node, elemId, isPreset);
            rootToNodePlanEdge = null;
            if(searchOperationType != SearchOperationType.Void)
                rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNode, cost);
        }

        private static void CreatePlanNodeAndLookupPlanEdge(PatternEdge edge, int elemId, 
            LGSPGraphStatistics graphStatistics, PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpatternLike, PlanNode planRoot, float zeroCost,
            IDictionary<PatternNode, PatternNode> originalToInlinedIndependent, IDictionary<PatternElement, SetValueType> presetsFromIndependentInlining,
            out bool isPreset, out PlanNode planNode, out PlanEdge rootToNodePlanEdge)
        {
            float cost;
            SearchOperationType searchOperationType;
            if(edge.DefToBeYieldedTo)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.DefToBeYieldedTo;
            }
            else if(edge.PointOfDefinition == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isSubpatternLike ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
            }
            else if(edge.PointOfDefinition != patternGraph && edge.OriginalIndependentElement == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
            }
            else if(presetsFromIndependentInlining.ContainsKey(edge))
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.NegIdptPreset;
                edge.PresetBecauseOfIndependentInlining = true;
            }
            else if(edge.Storage != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                }
            }
            else if(edge.IndexAccess != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromIndex; // pick from index instead of lookup from graph
                }
            }
            else if(edge.NameLookup != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByName; // pick by name instead of lookup from graph
                }
            }
            else if(edge.UniqueLookup != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByUnique; // pick by unique instead of lookup from graph
                }
            }
            else if(edge.ElementBeforeCasting != null)
            {
                cost = zeroCost;
                isPreset = false;
                searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
            }
            else
            {
                cost = graphStatistics.edgeCounts[edge.TypeID];
                cost = CostIncreaseForInlinedIndependent(edge, cost);

                isPreset = false;
                searchOperationType = SearchOperationType.Lookup;
            }

            PatternNode source = GetSourcePlusInlined(edge, patternGraph, originalToInlinedIndependent);
            PatternNode target = GetTargetPlusInlined(edge, patternGraph, originalToInlinedIndependent);
            planNode = new PlanNode(edge, elemId, isPreset,
                source != null ? source.TempPlanMapping : null,
                target != null ? target.TempPlanMapping : null);
            rootToNodePlanEdge = null;
            if(searchOperationType != SearchOperationType.Void)
                rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNode, cost);
        }

        private static void CreateSourceTargetIncomingOutgoingPlanEdges(PatternEdge edge, PlanNode planNode, 
            List<PlanEdge> planEdges, IDictionary<PatternNode, PatternNode> originalToInlinedIndependent,
            IGraphModel model, LGSPGraphStatistics graphStatistics, PatternGraph patternGraph, bool isPreset, float zeroCost)
        {
            // only add implicit source operation if edge source is needed and the edge source is 
            // not a preset node and not a storage node and not an index node and not a cast node
            PatternNode source = GetSourcePlusInlined(edge, patternGraph, originalToInlinedIndependent);
            if(source != null
                && !source.TempPlanMapping.IsPreset
                && source.Storage == null
                && source.IndexAccess == null
                && source.NameLookup == null
                && source.UniqueLookup == null
                && source.ElementBeforeCasting == null)
            {
                SearchOperationType operation = edge.fixedDirection ?
                    SearchOperationType.ImplicitSource : SearchOperationType.Implicit;
                PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNode,
                    source.TempPlanMapping, zeroCost);
                planEdges.Add(implSrcPlanEdge);
                source.TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
            }
            // only add implicit target operation if edge target is needed and the edge target is
            // not a preset node and not a storage node and not an index node and not a cast node
            PatternNode target = GetTargetPlusInlined(edge, patternGraph, originalToInlinedIndependent);
            if(target != null
                && !target.TempPlanMapping.IsPreset
                && target.Storage == null
                && target.IndexAccess == null
                && target.NameLookup == null
                && target.UniqueLookup == null
                && target.ElementBeforeCasting == null)
            {
                SearchOperationType operation = edge.fixedDirection ?
                    SearchOperationType.ImplicitTarget : SearchOperationType.Implicit;
                PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNode,
                    target.TempPlanMapping, zeroCost);
                planEdges.Add(implTgtPlanEdge);
                target.TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
            }

            // edge must only be reachable from other nodes if it's not a preset and not storage determined and not index determined and not a cast
            if(!isPreset
                && edge.Storage == null
                && edge.IndexAccess == null
                && edge.NameLookup == null
                && edge.UniqueLookup == null
                && edge.ElementBeforeCasting == null)
            {
                // no outgoing on source node if no source
                if(source != null)
                {
                    int targetTypeID;
                    if(target != null)
                        targetTypeID = target.TypeID;
                    else
                        targetTypeID = model.NodeModel.RootType.TypeID;
                    // cost of walking along edge
#if MONO_MULTIDIMARRAY_WORKAROUND
                    float normCost = graphStatistics.vstructs[((source.TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                        + targetTypeID) * 2 + (int)LGSPDirection.Out];
                    if(!edge.fixedDirection)
                    {
                        normCost += graphStatistics.vstructs[((source.TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                            + targetTypeID) * 2 + (int)LGSPDirection.In];
                    }
#else
                    float normCost = graph.statistics.vstructs[source.TypeID, edge.TypeID, targetTypeID, (int) LGSPDirection.Out];
                    if(!edge.fixedDirection)
                        normCost += graph.statistics.vstructs[source.TypeID, edge.TypeID, targetTypeID, (int) LGSPDirection.In];
#endif
                    if(graphStatistics.nodeCounts[source.TypeID] != 0)
                        normCost /= graphStatistics.nodeCounts[source.TypeID];
                    normCost = CostIncreaseForInlinedIndependent(edge, normCost);
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.Outgoing : SearchOperationType.Incident;
                    PlanEdge outPlanEdge = new PlanEdge(operation, source.TempPlanMapping,
                        planNode, normCost);
                    planEdges.Add(outPlanEdge);
                    planNode.IncomingEdges.Add(outPlanEdge);
                }

                // no incoming on target node if no target
                if(target != null)
                {
                    int sourceTypeID;
                    if(source != null)
                        sourceTypeID = source.TypeID;
                    else
                        sourceTypeID = model.NodeModel.RootType.TypeID;
                    // cost of walking in opposite direction of edge
#if MONO_MULTIDIMARRAY_WORKAROUND
                    float revCost = graphStatistics.vstructs[((target.TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                        + sourceTypeID) * 2 + (int)LGSPDirection.In];
                    if(!edge.fixedDirection)
                    {
                        revCost += graphStatistics.vstructs[((target.TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                            + sourceTypeID) * 2 + (int)LGSPDirection.Out];
                    }
#else
                    float revCost = graph.statistics.vstructs[target.TypeID, edge.TypeID, sourceTypeID, (int) LGSPDirection.In];
                    if(!edge.fixedDirection)
                        revCost += graph.statistics.vstructs[target.TypeID, edge.TypeID, sourceTypeID, (int) LGSPDirection.Out];
#endif
                    if(graphStatistics.nodeCounts[target.TypeID] != 0)
                        revCost /= graphStatistics.nodeCounts[target.TypeID];
                    revCost = CostIncreaseForInlinedIndependent(edge, revCost); 
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.Incoming : SearchOperationType.Incident;
                    PlanEdge inPlanEdge = new PlanEdge(operation, target.TempPlanMapping,
                        planNode, revCost);
                    planEdges.Add(inPlanEdge);
                    planNode.IncomingEdges.Add(inPlanEdge);
                }
            }
        }

        private static void CreatePickMapCastAssignPlanEdges(PatternNode node, List<PlanEdge> planEdges, float zeroCost)
        {
            if(node.Storage != null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge storAccessPlanEdge = new PlanEdge(
                    node.StorageIndex != null ? SearchOperationType.MapWithStorageDependent : SearchOperationType.PickFromStorageDependent,
                    node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, zeroCost);
                planEdges.Add(storAccessPlanEdge);
                node.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
            }
            else if(node.IndexAccess != null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge indexAccessPlanEdge = new PlanEdge(
                    SearchOperationType.PickFromIndexDependent,
                    node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, zeroCost);
                planEdges.Add(indexAccessPlanEdge);
                node.TempPlanMapping.IncomingEdges.Add(indexAccessPlanEdge);
            }
            else if(node.NameLookup != null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge nameAccessPlanEdge = new PlanEdge(
                    SearchOperationType.PickByNameDependent,
                    node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, zeroCost);
                planEdges.Add(nameAccessPlanEdge);
                node.TempPlanMapping.IncomingEdges.Add(nameAccessPlanEdge);
            }
            else if(node.UniqueLookup != null && node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge uniqueAccessPlanEdge = new PlanEdge(
                    SearchOperationType.PickByUniqueDependent,
                    node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, node.TempPlanMapping, zeroCost);
                planEdges.Add(uniqueAccessPlanEdge);
                node.TempPlanMapping.IncomingEdges.Add(uniqueAccessPlanEdge);
            }
            else if(node.ElementBeforeCasting != null)
            {
                PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                    node.ElementBeforeCasting.TempPlanMapping, node.TempPlanMapping, zeroCost);
                planEdges.Add(castPlanEdge);
                node.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
            }
            else if(node.AssignmentSource != null)
            {
                PlanEdge assignPlanEdge = new PlanEdge(SearchOperationType.Assign,
                    node.AssignmentSource.TempPlanMapping, node.TempPlanMapping, zeroCost);
                planEdges.Add(assignPlanEdge);
                node.TempPlanMapping.IncomingEdges.Add(assignPlanEdge);

                if(!node.AssignmentSource.TempPlanMapping.IsPreset)
                {
                    PlanEdge assignPlanEdgeOpposite = new PlanEdge(SearchOperationType.Assign,
                        node.TempPlanMapping, node.AssignmentSource.TempPlanMapping, zeroCost);
                    planEdges.Add(assignPlanEdgeOpposite);
                    node.AssignmentSource.TempPlanMapping.IncomingEdges.Add(assignPlanEdgeOpposite);
                }
            }
        }

        private static void CreatePickMapCastAssignPlanEdges(PatternEdge edge, List<PlanEdge> planEdges, float zeroCost)
        {
            if(edge.Storage != null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge storAccessPlanEdge = new PlanEdge(
                    edge.StorageIndex != null ? SearchOperationType.MapWithStorageDependent : SearchOperationType.PickFromStorageDependent,
                    edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, zeroCost);
                planEdges.Add(storAccessPlanEdge);
                edge.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
            }
            else if(edge.IndexAccess != null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge indexAccessPlanEdge = new PlanEdge(
                    SearchOperationType.PickFromIndexDependent,
                    edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, zeroCost);
                planEdges.Add(indexAccessPlanEdge);
                edge.TempPlanMapping.IncomingEdges.Add(indexAccessPlanEdge);
            }
            else if(edge.NameLookup != null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge nameAccessPlanEdge = new PlanEdge(
                    SearchOperationType.PickByNameDependent,
                    edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, zeroCost);
                planEdges.Add(nameAccessPlanEdge);
                edge.TempPlanMapping.IncomingEdges.Add(nameAccessPlanEdge);
            }
            else if(edge.UniqueLookup != null && edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
            {
                PlanEdge uniqueAccessPlanEdge = new PlanEdge(
                    SearchOperationType.PickByUniqueDependent,
                    edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness().TempPlanMapping, edge.TempPlanMapping, zeroCost);
                planEdges.Add(uniqueAccessPlanEdge);
                edge.TempPlanMapping.IncomingEdges.Add(uniqueAccessPlanEdge);
            }
            else if(edge.ElementBeforeCasting != null)
            {
                PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                    edge.ElementBeforeCasting.TempPlanMapping, edge.TempPlanMapping, zeroCost);
                planEdges.Add(castPlanEdge);
                edge.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
            }
            else if(edge.AssignmentSource != null)
            {
                PlanEdge assignPlanEdge = new PlanEdge(SearchOperationType.Assign,
                    edge.AssignmentSource.TempPlanMapping, edge.TempPlanMapping, zeroCost);
                planEdges.Add(assignPlanEdge);
                edge.TempPlanMapping.IncomingEdges.Add(assignPlanEdge);

                if(!edge.AssignmentSource.TempPlanMapping.IsPreset)
                {
                    PlanEdge assignPlanEdgeOpposite = new PlanEdge(SearchOperationType.Assign,
                        edge.TempPlanMapping, edge.AssignmentSource.TempPlanMapping, zeroCost);
                    planEdges.Add(assignPlanEdgeOpposite);
                    edge.AssignmentSource.TempPlanMapping.IncomingEdges.Add(assignPlanEdge);
                }
            }
        }

        /// <summary>
        /// Generate plan graph for given pattern graph with costs from initial static schedule handed in with graph elements.
        /// Plan graph contains nodes representing the pattern elements (nodes and edges)
        /// and edges representing the matching operations to get the elements by.
        /// Edges in plan graph are given in the nodes by incoming list, as needed for MSA computation.
        /// </summary>
        public static PlanGraph GenerateStaticPlanGraph(PatternGraph patternGraph,
            bool isNegativeOrIndependent, bool isSubpatternLike, bool inlineIndependent,
            IDictionary<PatternElement, SetValueType> presetsFromIndependentInlining)
        {
            //
            // If you change this method, chances are high you also want to change GeneratePlanGraph
            // todo: unify it with GeneratePlanGraph
            //

            // the frontend delivers costs in between 1.0 and 10.0
            // the highest prio gets 1.0, zero prio gets 10.0, the others about in between
            // the default if no prio is given is cost 5.5

            // here we assign to a lookup the node.cost or edge.cost from frontend
            // for implicit source/target cost 0
            // and for following incoming/outgoing edges cost (5.5 + edge.cost) / 2
            // this way high prio edges tend to be looked up

            // Create root node
            // Create plan graph nodes for all pattern graph nodes and all pattern graph edges
            // Create "lookup" plan graph edge from root node to each plan graph node
            // Create "implicit source" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the source node of the pattern graph edge
            // Create "implicit target" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the target node of the pattern graph edge
            // Create "incoming" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the incoming edges of the pattern node
            // Create "outgoing" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the outgoing edges of the pattern node
            // Ensured: there's no plan graph edge with a preset element as target besides the lookup,
            //     so presets are only search operation sources
            // Create "pick from storage" plan graph edge for plan graph nodes which are to be picked from a storage,
            //     from root node on, instead of lookup, no other plan graph edge having this node as target
            // Create "pick from storage attribute" plan graph edge from storage attribute owner to storage picking result,
            //     no lookup, no other plan graph edge having this node as target
            // Create "map" by storage plan graph edge from accessor to storage mapping result
            //     no lookup, no other plan graph edge having this node as target
            // Create "pick from index" plan graph edge for plan graph nodes which are to be picked from an index,
            //     from root node on, instead of lookup, no other plan graph edge having this node as target
            // Create "pick from index depending" plan graph edge from node the index expressions depend on,
            //     no lookup, no other plan graph edge having this node as target
            // Create "cast" plan graph edge from element before casting to cast result,
            //     no lookup, no other plan graph edge having this node as target

            int numNodes = patternGraph.nodesPlusInlined.Length;
            if(inlineIndependent && !isNegativeOrIndependent)
                numNodes += PlanGraphGenerator.NumNodesInIndependentsMatchedThere(patternGraph);
            int numEdges = patternGraph.edgesPlusInlined.Length;
            if(inlineIndependent && !isNegativeOrIndependent)
                numEdges += PlanGraphGenerator.NumEdgesInIndependentsMatchedThere(patternGraph);
            PlanNode[] planNodes = new PlanNode[numNodes + numEdges];
            List<PlanEdge> planEdges = new List<PlanEdge>(numNodes + 5 * numEdges); // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)

            Dictionary<PatternNode, PatternNode> originalToInlinedIndependent = new Dictionary<PatternNode, PatternNode>();

            int nodesIndex = 0;
            float zeroCost = 0;

            // create plan nodes and lookup plan edges for all pattern graph nodes
            PlanNode planRoot = new PlanNode("root");
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];

                PlanNode planNode;
                PlanEdge rootToNodePlanEdge;
                CreatePlanNodeAndLookupPlanEdgeStatic(node, i + 1,
                    patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                    originalToInlinedIndependent, presetsFromIndependentInlining,
                    out planNode, out rootToNodePlanEdge);

                planNodes[nodesIndex] = planNode;
                if(rootToNodePlanEdge != null)
                {
                    planEdges.Add(rootToNodePlanEdge);
                    planNode.IncomingEdges.Add(rootToNodePlanEdge);
                }

                node.TempPlanMapping = planNode;
                ++nodesIndex;
            }
            if(inlineIndependent && !isNegativeOrIndependent) // independent inlining only if not in negative/independent
            {
                foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
                {
                    foreach(PatternNode nodeOriginal in PlanGraphGenerator.NodesInIndependentMatchedThere(independent))
                    {
                        PatternNode node = new PatternNode(nodeOriginal, "_inlined_" + independent.name);
                        originalToInlinedIndependent.Add(nodeOriginal, node);

                        PlanNode planNode;
                        PlanEdge rootToNodePlanEdge;
                        CreatePlanNodeAndLookupPlanEdgeStatic(node, -1,
                            patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                            originalToInlinedIndependent, presetsFromIndependentInlining,
                            out planNode, out rootToNodePlanEdge);

                        planNodes[nodesIndex] = planNode;
                        if(rootToNodePlanEdge != null)
                        {
                            planEdges.Add(rootToNodePlanEdge);
                            planNode.IncomingEdges.Add(rootToNodePlanEdge);
                        }

                        node.TempPlanMapping = planNode;
                        ++nodesIndex;
                    }
                }
            }

            // create plan nodes and lookup plus incidence handling plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];

                bool isPreset;
                PlanNode planNode;
                PlanEdge rootToNodePlanEdge;
                CreatePlanNodeAndLookupPlanEdgeStatic(edge, i + 1,
                    patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                    originalToInlinedIndependent, presetsFromIndependentInlining,
                    out isPreset, out planNode, out rootToNodePlanEdge);

                planNodes[nodesIndex] = planNode;
                if(rootToNodePlanEdge != null)
                {
                    planEdges.Add(rootToNodePlanEdge);
                    planNode.IncomingEdges.Add(rootToNodePlanEdge);
                }

                CreateSourceTargetIncomingOutgoingPlanEdgesStatic(edge, planNode, planEdges,
                    originalToInlinedIndependent, patternGraph, isPreset, zeroCost);

                edge.TempPlanMapping = planNode;
                ++nodesIndex;
            }
            if(inlineIndependent && !isNegativeOrIndependent) // independent inlining only if not in negative/independent
            {
                foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
                {
                    foreach(PatternEdge edgeOriginal in PlanGraphGenerator.EdgesInIndependentMatchedThere(independent))
                    {
                        PatternEdge edge = new PatternEdge(edgeOriginal, "_inlined_" + independent.name);

                        bool isPreset;
                        PlanNode planNode;
                        PlanEdge rootToNodePlanEdge;
                        CreatePlanNodeAndLookupPlanEdgeStatic(edge, -1,
                            patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                            originalToInlinedIndependent, presetsFromIndependentInlining,
                            out isPreset, out planNode, out rootToNodePlanEdge);

                        planNodes[nodesIndex] = planNode;
                        if(rootToNodePlanEdge != null)
                        {
                            planEdges.Add(rootToNodePlanEdge);
                            planNode.IncomingEdges.Add(rootToNodePlanEdge);
                        }

                        CreateSourceTargetIncomingOutgoingPlanEdgesStatic(edge, planNode, planEdges,
                            originalToInlinedIndependent, patternGraph, isPreset, zeroCost);

                        edge.TempPlanMapping = planNode;
                        ++nodesIndex;
                    }
                }
            }

            ////////////////////////////////////////////////////////////////////////////
            // second run handling dependent storage and index picking (can't be done in first run due to dependencies between elements)

            // create map/pick/cast/assign plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];

                if(node.PointOfDefinition == patternGraph && !presetsFromIndependentInlining.ContainsKey(node))
                    PlanGraphGenerator.CreatePickMapCastAssignPlanEdges(node, planEdges, zeroCost);
            }

            // create map/pick/cast/assign plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];

                if(edge.PointOfDefinition == patternGraph && !presetsFromIndependentInlining.ContainsKey(edge))
                    PlanGraphGenerator.CreatePickMapCastAssignPlanEdges(edge, planEdges, zeroCost);
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray());
        }

        private static void CreatePlanNodeAndLookupPlanEdgeStatic(PatternNode node, int elemId,
            PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpatternLike, PlanNode planRoot, float zeroCost,
            IDictionary<PatternNode, PatternNode> originalToInlinedIndependent, IDictionary<PatternElement, SetValueType> presetsFromIndependentInlining,
            out PlanNode planNode, out PlanEdge rootToNodePlanEdge)
        {
            float cost;
            bool isPreset;
            SearchOperationType searchOperationType;
            if(node.DefToBeYieldedTo)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.DefToBeYieldedTo;
            }
            else if(node.PointOfDefinition == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isSubpatternLike ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
            }
            else if(node.PointOfDefinition != patternGraph && node.OriginalIndependentElement == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
            }
            else if(presetsFromIndependentInlining.ContainsKey(node))
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.NegIdptPreset;
                node.PresetBecauseOfIndependentInlining = true;
            }
            else if(node.Storage != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                }
            }
            else if(node.IndexAccess != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromIndex; // pick from index instead of lookup from graph
                }
            }
            else if(node.NameLookup != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByName; // pick by name instead of lookup from graph
                }
            }
            else if(node.UniqueLookup != null)
            {
                if(node.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByUnique; // pick by unique instead of lookup from graph
                }
            }
            else if(node.ElementBeforeCasting != null)
            {
                cost = zeroCost;
                isPreset = false;
                searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
            }
            else
            {
                cost = node.Cost;
                cost = PlanGraphGenerator.CostIncreaseForInlinedIndependent(node, cost);
                isPreset = false;
                searchOperationType = SearchOperationType.Lookup;
            }

            planNode = new PlanNode(node, elemId, isPreset);
            rootToNodePlanEdge = null;
            if(searchOperationType != SearchOperationType.Void)
                rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNode, cost);
        }

        private static void CreatePlanNodeAndLookupPlanEdgeStatic(PatternEdge edge, int elemId,
            PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpatternLike, PlanNode planRoot, float zeroCost,
            IDictionary<PatternNode, PatternNode> originalToInlinedIndependent, IDictionary<PatternElement, SetValueType> presetsFromIndependentInlining,
            out bool isPreset, out PlanNode planNode, out PlanEdge rootToNodePlanEdge)
        {
            float cost;
            SearchOperationType searchOperationType;
            if(edge.DefToBeYieldedTo)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.DefToBeYieldedTo;
            }
            else if(edge.PointOfDefinition == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isSubpatternLike ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
            }
            else if(edge.PointOfDefinition != patternGraph && edge.OriginalIndependentElement == null)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
            }
            else if(presetsFromIndependentInlining.ContainsKey(edge))
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = SearchOperationType.NegIdptPreset;
                edge.PresetBecauseOfIndependentInlining = true;
            }
            else if(edge.Storage != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                }
            }
            else if(edge.IndexAccess != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickFromIndex; // pick from index instead of lookup from graph
                }
            }
            else if(edge.NameLookup != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByName; // pick by name instead of lookup from graph
                }
            }
            else if(edge.UniqueLookup != null)
            {
                if(edge.GetPatternElementThisElementDependsOnOutsideOfGraphConnectedness() != null)
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element we depend on is needed, so there is no lookup like operation
                }
                else
                {
                    cost = zeroCost;
                    isPreset = false;
                    searchOperationType = SearchOperationType.PickByUnique; // pick by unique instead of lookup from graph
                }
            }
            else if(edge.ElementBeforeCasting != null)
            {
                cost = zeroCost;
                isPreset = false;
                searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
            }
            else
            {
                cost = edge.Cost;
                cost = PlanGraphGenerator.CostIncreaseForInlinedIndependent(edge, cost);
                isPreset = false;
                searchOperationType = SearchOperationType.Lookup;
            }

            PatternNode source = PlanGraphGenerator.GetSourcePlusInlined(edge, patternGraph, originalToInlinedIndependent);
            PatternNode target = PlanGraphGenerator.GetTargetPlusInlined(edge, patternGraph, originalToInlinedIndependent);
            planNode = new PlanNode(edge, elemId, isPreset,
                source != null ? source.TempPlanMapping : null,
                target != null ? target.TempPlanMapping : null);
            rootToNodePlanEdge = null;
            if(searchOperationType != SearchOperationType.Void)
                rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNode, cost);
        }

        private static void CreateSourceTargetIncomingOutgoingPlanEdgesStatic(PatternEdge edge, PlanNode planNode,
            List<PlanEdge> planEdges, IDictionary<PatternNode, PatternNode> originalToInlinedIndependent,
            PatternGraph patternGraph, bool isPreset, float zeroCost)
        {
            // only add implicit source operation if edge source is needed and the edge source is 
            // not a preset node and not a storage node and not an index node and not a cast node
            PatternNode source = PlanGraphGenerator.GetSourcePlusInlined(edge, patternGraph, originalToInlinedIndependent);
            if(source != null
                && !source.TempPlanMapping.IsPreset
                && source.Storage == null
                && source.IndexAccess == null
                && source.NameLookup == null
                && source.UniqueLookup == null
                && source.ElementBeforeCasting == null)
            {
                SearchOperationType operation = edge.fixedDirection ?
                    SearchOperationType.ImplicitSource : SearchOperationType.Implicit;
                PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNode,
                    source.TempPlanMapping, zeroCost);
                planEdges.Add(implSrcPlanEdge);
                source.TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
            }
            // only add implicit target operation if edge target is needed and the edge target is
            // not a preset node and not a storage node and not an index node and not a cast node
            PatternNode target = PlanGraphGenerator.GetTargetPlusInlined(edge, patternGraph, originalToInlinedIndependent);
            if(target != null
                && !target.TempPlanMapping.IsPreset
                && target.Storage == null
                && target.IndexAccess == null
                && target.NameLookup == null
                && target.UniqueLookup == null
                && target.ElementBeforeCasting == null)
            {
                SearchOperationType operation = edge.fixedDirection ?
                    SearchOperationType.ImplicitTarget : SearchOperationType.Implicit;
                PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNode,
                    target.TempPlanMapping, zeroCost);
                planEdges.Add(implTgtPlanEdge);
                target.TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
            }

            // edge must only be reachable from other nodes if it's not a preset and not storage determined and not index determined and not a cast
            if(!isPreset
                && edge.Storage == null
                && edge.IndexAccess == null
                && edge.NameLookup == null
                && edge.UniqueLookup == null
                && edge.ElementBeforeCasting == null)
            {
                float cost = (edge.Cost + 5.5f) / 2;
                cost = PlanGraphGenerator.CostIncreaseForInlinedIndependent(edge, cost);

                // no outgoing on source node if no source
                if(source != null)
                {
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.Outgoing : SearchOperationType.Incident;
                    PlanEdge outPlanEdge = new PlanEdge(operation, source.TempPlanMapping,
                        planNode, cost);
                    planEdges.Add(outPlanEdge);
                    planNode.IncomingEdges.Add(outPlanEdge);
                }
                // no incoming on target node if no target
                if(target != null)
                {
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.Incoming : SearchOperationType.Incident;
                    PlanEdge inPlanEdge = new PlanEdge(operation, target.TempPlanMapping,
                        planNode, cost);
                    planEdges.Add(inPlanEdge);
                    planNode.IncomingEdges.Add(inPlanEdge);
                }
            }
        }

        private static IEnumerable<PatternNode> NodesInIndependentMatchedThere(PatternGraph independent)
        {
            foreach(PatternNode node in independent.nodesPlusInlined)
            {
                if(node.PointOfDefinition == independent && !node.defToBeYieldedTo)
                    yield return node;
            }
        }

        private static int NumNodesInIndependentsMatchedThere(PatternGraph patternGraph)
        {
            int sum = 0;
            
            foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
            {
                foreach(PatternNode node in independent.nodesPlusInlined)
                {
                    if(node.PointOfDefinition == independent && !node.defToBeYieldedTo)
                        ++sum;
                }
            }
            
            return sum;
        }

        private static IEnumerable<PatternEdge> EdgesInIndependentMatchedThere(PatternGraph independent)
        {
            foreach(PatternEdge edge in independent.edgesPlusInlined)
            {
                if(edge.PointOfDefinition == independent && !edge.defToBeYieldedTo)
                    yield return edge;
            }
        }

        private static int NumEdgesInIndependentsMatchedThere(PatternGraph patternGraph)
        {
            int sum = 0;
            
            foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
            {
                foreach(PatternEdge edge in independent.edgesPlusInlined)
                {
                    if(edge.PointOfDefinition == independent && !edge.defToBeYieldedTo)
                        ++sum;
                }
            }

            return sum;
        }

        private static PatternNode GetSourcePlusInlined(PatternEdge edge, PatternGraph patternGraph, IDictionary<PatternNode, PatternNode> originalToInlinedIndependent)
        {
            PatternNode source = patternGraph.GetSourcePlusInlined(edge);
            if(edge.OriginalIndependentElement != null && source == null)
            {
                PatternNode sourceOriginal = edge.OriginalIndependentElement.pointOfDefinition.GetSourcePlusInlined((PatternEdge)edge.OriginalIndependentElement);
                if(originalToInlinedIndependent.ContainsKey(sourceOriginal))
                    source = originalToInlinedIndependent[sourceOriginal];
                else
                    source = sourceOriginal; // not declared in independent itself, but in some parent
            }
            return source;
        }

        private static PatternNode GetTargetPlusInlined(PatternEdge edge, PatternGraph patternGraph, IDictionary<PatternNode, PatternNode> originalToInlinedIndependent)
        {
            PatternNode target = patternGraph.GetTargetPlusInlined(edge);
            if(edge.OriginalIndependentElement != null && target == null)
            {
                PatternNode targetOriginal = edge.OriginalIndependentElement.pointOfDefinition.GetTargetPlusInlined((PatternEdge)edge.OriginalIndependentElement);
                if(originalToInlinedIndependent.ContainsKey(targetOriginal))
                    target = originalToInlinedIndependent[targetOriginal];
                else
                    target = targetOriginal; // not declared in independent itself, but in some parent
            }
            return target;
        }

        private static float CostIncreaseForInlinedIndependent(PatternElement elem, float cost)
        {
            // use costs a bit higher for elements from independent patterns, so they are not chosen unless they bring a real advantage
            if(elem.OriginalIndependentElement != null)
                cost = (cost + 0.1F) * 1.1F * (cost < 10.0F ? 1.1F : 1.0F);
            return cost;
        }

        /// <summary>
        /// Marks the minimum spanning arborescence of a plan graph by setting the IncomingMSAEdge
        /// fields for all nodes
        /// </summary>
        /// <param name="planGraph">The plan graph to be marked</param>
        /// <param name="dumpName">Names the dump targets if dump compiler flags are set</param>
        /// <param name="dumpSearchPlan">If true, generated search plans are dumped in VCG and TXT files in the current directory.</param>
        public static void MarkMinimumSpanningArborescence(PlanGraph planGraph, String dumpName, bool dumpSearchPlan)
        {
            if(dumpSearchPlan)
                DumpPlanGraph(planGraph, dumpName, "initial");

            // nodes not already looked at
            Dictionary<PlanNode, bool> leftNodes = new Dictionary<PlanNode, bool>(planGraph.Nodes.Length);
            foreach(PlanNode node in planGraph.Nodes)
            {
                leftNodes.Add(node, true);
            }

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
                    if(curNode is PlanNode)
                        leftNodes.Remove((PlanNode) curNode);

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

            if(dumpSearchPlan)
            {
                DumpPlanGraph(planGraph, dumpName, "contracted");
                DumpContractedPlanGraph(planGraph, dumpName);
            }

            // breaks all cycles represented by setting the incoming msa edges of the 
            // representative nodes to the incoming msa edges according to the supernode
            /*no, not equivalent:
            foreach(PlanSuperNode superNode in superNodeStack)
                superNode.Child.IncomingMSAEdge = superNode.IncomingMSAEdge;*/
            foreach(PlanSuperNode superNode in superNodeStack)
            {
                PlanPseudoNode curNode = superNode.IncomingMSAEdge.Target;
                while(curNode.SuperNode != superNode)
                {
                    curNode = curNode.SuperNode;
                }
                curNode.IncomingMSAEdge = superNode.IncomingMSAEdge;
                if(curNode.IncomingMSAEdge == null)
                    throw new Exception();
            }

            if(dumpSearchPlan)
                DumpFinalPlanGraph(planGraph, dumpName);
        }

#region Dump functions
        private static String GetDumpName(PlanNode node)
        {
            if(node.NodeType == PlanNodeType.Root) return "root";
            else if(node.NodeType == PlanNodeType.Node) return "node_" + node.PatternElement.Name;
            else return "edge_" + node.PatternElement.Name;
        }

        public static void DumpPlanGraph(PlanGraph planGraph, String dumpname, String namesuffix)
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

        private static void DumpNode(StreamWriter sw, PlanNode node)
        {
            if(node.NodeType == PlanNodeType.Edge)
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\" shape:ellipse}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
            else
                sw.WriteLine("node:{{title:\"{0}\" label:\"{1} : {2}\"}}",
                    GetDumpName(node), node.PatternElement.TypeID, node.PatternElement.Name);
        }

        private static void DumpEdge(StreamWriter sw, PlanEdge edge, bool markRed)
        {
            String typeStr = " #";
            switch(edge.Type)
            {
                case SearchOperationType.Outgoing: typeStr = "--"; break;
                case SearchOperationType.Incoming: typeStr = "->"; break;
                case SearchOperationType.Incident: typeStr = "<->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Implicit: typeStr = "IM"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.ActionPreset: typeStr = " p"; break;
                case SearchOperationType.NegIdptPreset: typeStr = "np"; break;
                case SearchOperationType.SubPreset: typeStr = "sp"; break;
            }

            sw.WriteLine("edge:{{sourcename:\"{0}\" targetname:\"{1}\" label:\"{2} / {3:0.00} ({4:0.00}) \"{5}}}",
                GetDumpName(edge.Source), GetDumpName(edge.Target), typeStr, edge.mstCost, edge.Cost,
                markRed ? " color:red" : "");
        }

        private static void DumpSuperNode(StreamWriter sw, PlanSuperNode superNode, Dictionary<PlanSuperNode, bool> dumpedSuperNodes)
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

        private static void DumpContractedPlanGraph(PlanGraph planGraph, String dumpname)
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

        private static void DumpFinalPlanGraph(PlanGraph planGraph, String dumpname)
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
#endregion Dump functions
    }
}
