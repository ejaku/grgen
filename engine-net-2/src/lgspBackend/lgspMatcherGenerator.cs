/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define NO_EDGE_LOOKUP
//#define RANDOM_LOOKUP_LIST_START      // currently broken
//#define DUMP_SCHEDULED_SEARCH_PLAN
//#define DUMP_SEARCHPROGRAMS
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
    /// <summary>
    /// Class generating matcher programs out of rules.
    /// A PatternGraphAnalyzer must run before the matcher generator is used,
    /// so that the analysis data is written the pattern graphs of the matching patterns to generate code for.
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
        /// If true, the negatives, independents, and evaluations are inserted at the end of the schedule
        /// instead of as early as possible; this is likely less efficient but allows to use checks 
        /// which require that they are only called after a structural match was found
        /// </summary>
        public bool LazyNegativeIndependentConditionEvaluation = false;

        /// <summary>
        /// Instantiates a new instance of LGSPMatcherGenerator with the given graph model.
        /// A PatternGraphAnalyzer must run before the matcher generator is used,
        /// so that the analysis data is written the pattern graphs of the matching patterns to generate code for.
        /// </summary>
        /// <param name="model">The model for which the matcher functions shall be generated.</param>
        public LGSPMatcherGenerator(IGraphModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Builds a pattern graph out of the graph.
        /// The pattern graph retains links to the original graph elements and uses them for attribute comparison.
        /// Attention: if you change the elements(/graph) your comparisons will be done against the old structure but the new attributes! 
        /// </summary>
        /// <param name="graph">The graph which is to be transfered into a pattern</param>
        /// <param name="includingAttributes">Whether the attributes should be included and thus checked</param>
        /// <returns></returns>
        public PatternGraph BuildPatternGraph(LGSPGraph graph, bool includingAttributes)
        {
            int numNodes = graph.NumNodes;
            int numEdges = graph.NumEdges;

            int count = 0;
            PatternNode[] nodes = new PatternNode[numNodes];
            INode[] correspondingNodes = new INode[numNodes];
            foreach(INode node in graph.Nodes)
            {
                LGSPNode n = (LGSPNode)node;
                nodes[count] = new PatternNode(
                    n.Type.TypeID, n.Type.Name,
                    graph.Name+"_node_"+count, "node_"+count,
                    null, null,
                    1.0f, -1, false,
                    null, null, null, null, 
                    null, false
                );
                correspondingNodes[count] = node;
                ++count;
            }

            count = 0;
            PatternEdge[] edges = new PatternEdge[numEdges];
            IEdge[] correspondingEdges = new IEdge[numEdges];
            foreach(IEdge edge in graph.Edges)
            {
                LGSPEdge e = (LGSPEdge)edge;
                edges[count] = new PatternEdge(
                    true,
                    e.Type.TypeID, e.Type.Name,
                    graph.Name+"_edge_"+count, "edge_"+count,
                    null, null,
                    1.0f, -1, false,
                    null, null, null, null, 
                    null, false
                );
                correspondingEdges[count] = edge;
                ++count;
            }

            bool[,] homNodes = new bool[numNodes, numNodes];
            for(int i = 0; i < numNodes; ++i)
                for(int j = 0; j < numNodes; ++j)
                    homNodes[i, j] = false;

            bool[,] homEdges = new bool[numEdges, numEdges];
            for(int i = 0; i < numEdges; ++i)
                for(int j = 0; j < numEdges; ++j)
                    homEdges[i, j] = false;
            
            bool[,] homNodesGlobal = new bool[numNodes, numNodes];
            for(int i = 0; i < numNodes; ++i)
                for(int j = 0; j < numNodes; ++j)
                    homNodesGlobal[i, j] = false;

            bool[,] homEdgesGlobal = new bool[numEdges, numEdges];
            for(int i = 0; i < numEdges; ++i)
                for(int j = 0; j < numEdges; ++j)
                    homEdgesGlobal[i, j] = false;

            bool[] totallyHomNodes = new bool[numNodes];
            for(int i = 0; i < numNodes; ++i)
                totallyHomNodes[i] = false;

            bool[] totallyHomEdges = new bool[numEdges];
            for(int i = 0; i < numEdges; ++i)
                totallyHomEdges[i] = false;

            PatternCondition[] patternConditions = new PatternCondition[0];
            if(includingAttributes)
            {
                patternConditions = new PatternCondition[numNodes + numEdges];
                for(int i = 0; i < numNodes; ++i)
                {
                    patternConditions[i] = new PatternCondition(new expression.AreAttributesEqual(correspondingNodes[i], nodes[i]),
                        new string[] { nodes[i].name }, new string[] { }, new string[] { }, new VarType[] { });
                }
                for(int i = numNodes; i < numNodes + numEdges; ++i)
                {
                    patternConditions[i] = new PatternCondition(new expression.AreAttributesEqual(correspondingEdges[i - numNodes], edges[i - numNodes]),
                        new string[] { }, new string[] { edges[i - numNodes].name }, new string[] { }, new VarType[] { });
                }
            }

            PatternGraph patternGraph = new PatternGraph(
                graph.Name, "",
                false, false,
                nodes, edges, new PatternVariable[0],
                new PatternGraphEmbedding[0], new Alternative[0], new Iterated[0],
                new PatternGraph[0], new PatternGraph[0],
                patternConditions, new PatternYielding[0],
                homNodes, homEdges,
                homNodesGlobal, homEdgesGlobal,
                totallyHomNodes, totallyHomEdges
            );
            foreach(PatternNode node in nodes)
                node.pointOfDefinition = patternGraph;
            foreach(PatternEdge edge in edges)
                edge.pointOfDefinition = patternGraph;
            patternGraph.correspondingNodes = correspondingNodes;
            patternGraph.correspondingEdges = correspondingEdges;

            return patternGraph;
        }

        /// <summary>
        /// Generate plan graph for given pattern graph with costs from the analyzed host graph.
        /// Plan graph contains nodes representing the pattern elements (nodes and edges)
        /// and edges representing the matching operations to get the elements by.
        /// Edges in plan graph are given in the nodes by incoming list, as needed for MSA computation.
        /// </summary>
        /// <param name="graph">The host graph to optimize the matcher program for, 
        /// providing statistical information about its structure </param>
        public PlanGraph GeneratePlanGraph(LGSPGraph graph, PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpattern)
        {
            // 
            // If you change this method, chances are high you also want to change GenerateStaticPlanGraph in LGSPGrGen
            // look there for version without ifdef junk
            // todo: unify it with GenerateStaticPlanGraph in LGSPGrGen
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
            // Create "pick from storage" plan graph edge for plan graph nodes which are to picked from a storage,
            //     from root node on, no lookup here, no other plan graph edge having this node as target
            // Create "pick from storage attribute" plan graph edge from storage attribute owner to storage picking result,
            //     no lookup, no other plan graph edge having this node as target
            // Create "map" by storage plan graph edge from accessor to storage mapping result
            //     no lookup, no other plan graph edge having this node as target
            // Create "cast" plan graph edge from element before casting to cast result,
            //     no lookup, no other plan graph edge having this node as target

            PlanNode[] planNodes = new PlanNode[patternGraph.Nodes.Length + patternGraph.Edges.Length];
            // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)
            List<PlanEdge> planEdges = new List<PlanEdge>(patternGraph.Nodes.Length + 5 * patternGraph.Edges.Length);

            int nodesIndex = 0;

            PlanNode planRoot = new PlanNode("root");

            // create plan nodes and lookup plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.Nodes.Length; i++)
            {
                PatternNode node = patternGraph.nodes[i];

                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(node.DefToBeYieldedTo)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.DefToBeYieldedTo;
                }
                else if(node.PointOfDefinition == null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
                }
                else if (node.PointOfDefinition != patternGraph)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
                }
                else if(node.Storage != null)
                {
                    if(node.Accessor != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        cost = 0;
#else
                        cost = 1;
#endif
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the accessor is needed, so there is no lookup like operation
                    }
                    else
                    {
#if OPCOST_WITH_GEO_MEAN 
                        cost = 0;
#else
                        cost = 1;
#endif
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                    }
                }
                else if(node.StorageAttributeOwner != null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the attribute owner is needed, so there is no lookup like operation
                }
                else if(node.ElementBeforeCasting != null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
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
                planNodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                if(searchOperationType != SearchOperationType.Void)
                {
                    PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                    planEdges.Add(rootToNodePlanEdge);
                    planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
                }
                node.TempPlanMapping = planNodes[nodesIndex];

                ++nodesIndex;
            }

            // create plan nodes and necessary plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.Edges.Length; ++i)
            {
                PatternEdge edge = patternGraph.edges[i];

                bool isPreset;
#if !NO_EDGE_LOOKUP
                float cost;
                SearchOperationType searchOperationType;
                if(edge.DefToBeYieldedTo)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.DefToBeYieldedTo;
                }
                else if(edge.PointOfDefinition == null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.ActionPreset;
                }
                else if (edge.PointOfDefinition != patternGraph)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
                }
                else if(edge.Storage != null)
                {
                    if(edge.Accessor != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        cost = 0;
#else
                        cost = 1;
#endif
                        isPreset = false;
                        searchOperationType = SearchOperationType.Void; // the accessor is needed, so there is no lookup like operation
                    }
                    else
                    {
#if OPCOST_WITH_GEO_MEAN 
                        cost = 0;
#else
                        cost = 1;
#endif
                        isPreset = false;
                        searchOperationType = SearchOperationType.PickFromStorage; // pick from storage instead of lookup from graph
                    }
                }
                else if(edge.StorageAttributeOwner != null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the attribute owner is needed, so there is no lookup like operation
                }
                else if(edge.ElementBeforeCasting != null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = false;
                    searchOperationType = SearchOperationType.Void; // the element before casting is needed, so there is no lookup like operation
                }
                else
                {
#if VSTRUCT_VAL_FOR_EDGE_LOOKUP
                    int sourceTypeID;
                    if(patternGraph.GetSource(edge) != null) sourceTypeID = patternGraph.GetSource(edge).TypeID;
                    else sourceTypeID = model.NodeModel.RootType.TypeID;
                    int targetTypeID;
                    if(patternGraph.GetTarget(edge) != null) targetTypeID = patternGraph.GetTarget(edge).TypeID;
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
                planNodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset,
                    patternGraph.GetSource(edge)!=null ? patternGraph.GetSource(edge).TempPlanMapping : null,
                    patternGraph.GetTarget(edge)!=null ? patternGraph.GetTarget(edge).TempPlanMapping : null);
                if(searchOperationType != SearchOperationType.Void)
                {
                    PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                    planEdges.Add(rootToNodePlanEdge);
                    planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
                }
#else
                SearchOperationType searchOperationType = SearchOperationType.Lookup; // lookup as dummy
                if(edge.PatternElementType == PatternElementType.Preset)
                {
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else 
                {
                    isPreset = false;
                }
                planNodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset,
                    patternGraph.GetSource(edge)!=null ? patternGraph.GetSource(edge).TempPlanMapping : null,
                    patternGraph.GetTarget(edge)!=null ? patternGraph.GetTarget(edge).TempPlanMapping : null);
                if(isPreset)
                {
                    PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], 0);
                    planEdges.Add(rootToNodePlanEdge);
                    planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
                }
#endif

                // only add implicit source operation if edge source is needed and the edge source is not a preset node and not a storage node and not a cast node
                if(patternGraph.GetSource(edge) != null 
                    && !patternGraph.GetSource(edge).TempPlanMapping.IsPreset
                    && patternGraph.GetSource(edge).Storage == null
                    && patternGraph.GetSource(edge).StorageAttributeOwner == null
                    && patternGraph.GetSource(edge).ElementBeforeCasting == null)
                {
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.ImplicitSource : SearchOperationType.Implicit;

#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetSource(edge).TempPlanMapping, 0);
#else
                    PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetSource(edge).TempPlanMapping, 1);
#endif
                    planEdges.Add(implSrcPlanEdge);
                    patternGraph.GetSource(edge).TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node and not a storage node and not a cast node
                if(patternGraph.GetTarget(edge) != null 
                    && !patternGraph.GetTarget(edge).TempPlanMapping.IsPreset
                    && patternGraph.GetTarget(edge).Storage == null
                    && patternGraph.GetTarget(edge).StorageAttributeOwner == null
                    && patternGraph.GetTarget(edge).ElementBeforeCasting == null)
                {
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.ImplicitTarget : SearchOperationType.Implicit;
#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetTarget(edge).TempPlanMapping, 0);
#else
                    PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetTarget(edge).TempPlanMapping, 1);
#endif
                    planEdges.Add(implTgtPlanEdge);
                    patternGraph.GetTarget(edge).TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset and not storage determined and not a cast
                if(!isPreset && edge.Storage == null && edge.StorageAttributeOwner == null && edge.ElementBeforeCasting == null)
                {
                    // no outgoing on source node if no source
                    if(patternGraph.GetSource(edge) != null)
                    {
                        int targetTypeID;
                        if(patternGraph.GetTarget(edge) != null) targetTypeID = patternGraph.GetTarget(edge).TypeID;
                        else targetTypeID = model.NodeModel.RootType.TypeID;
                        // cost of walking along edge
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float normCost = graph.vstructs[((patternGraph.GetSource(edge).TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + targetTypeID) * 2 + (int) LGSPDir.Out];
                        if (!edge.fixedDirection) {
                            normCost += graph.vstructs[((patternGraph.GetSource(edge).TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                                + targetTypeID) * 2 + (int)LGSPDir.In];
                        }
#else
                        float normCost = graph.vstructs[patternGraph.GetSource(edge).TypeID, edge.TypeID, targetTypeID, (int) LGSPDir.Out];
                        if (!edge.fixedDirection) {
                            normCost += graph.vstructs[patternGraph.GetSource(edge).TypeID, edge.TypeID, targetTypeID, (int) LGSPDir.In];
                        }
#endif
                        if (graph.nodeCounts[patternGraph.GetSource(edge).TypeID] != 0)
                            normCost /= graph.nodeCounts[patternGraph.GetSource(edge).TypeID];
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Outgoing : SearchOperationType.Incident;
                        PlanEdge outPlanEdge = new PlanEdge(operation, patternGraph.GetSource(edge).TempPlanMapping, 
                            planNodes[nodesIndex], normCost);
                        planEdges.Add(outPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(outPlanEdge);
                    }

                    // no incoming on target node if no target
                    if(patternGraph.GetTarget(edge) != null)
                    {
                        int sourceTypeID;
                        if(patternGraph.GetSource(edge) != null) sourceTypeID = patternGraph.GetSource(edge).TypeID;
                        else sourceTypeID = model.NodeModel.RootType.TypeID;
                        // cost of walking in opposite direction of edge
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float revCost = graph.vstructs[((patternGraph.GetTarget(edge).TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + sourceTypeID) * 2 + (int) LGSPDir.In];
                        if (!edge.fixedDirection) {
                            revCost += graph.vstructs[((patternGraph.GetTarget(edge).TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                                + sourceTypeID) * 2 + (int)LGSPDir.Out];
                        }
#else
                        float revCost = graph.vstructs[patternGraph.GetTarget(edge).TypeID, edge.TypeID, sourceTypeID, (int) LGSPDir.In];
                        if (!edge.fixedDirection) {
                            revCost += graph.vstructs[patternGraph.GetTarget(edge).TypeID, edge.TypeID, sourceTypeID, (int) LGSPDir.Out];
                        }
#endif
                        if (graph.nodeCounts[patternGraph.GetTarget(edge).TypeID] != 0)
                            revCost /= graph.nodeCounts[patternGraph.GetTarget(edge).TypeID];
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Incoming : SearchOperationType.Incident;
                        PlanEdge inPlanEdge = new PlanEdge(operation, patternGraph.GetTarget(edge).TempPlanMapping,
                            planNodes[nodesIndex], revCost);
                        planEdges.Add(inPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(inPlanEdge);
                    }
                }

                ++nodesIndex;
            }

            ////////////////////////////////////////////////////////////////////////////
            // second run handling storage mapping (can't be done in first run due to dependencies between elements)

            // create map with storage plan edges for all pattern graph nodes
            // which are the result of a mapping/picking from attribute operation or element type casting
            for(int i = 0; i < patternGraph.Nodes.Length; ++i)
            {
                PatternNode node = patternGraph.nodes[i];
                if(node.PointOfDefinition == patternGraph)
                {
                    if(node.Accessor != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.MapWithStorage,
                            node.Accessor.TempPlanMapping, node.TempPlanMapping, 0);
#else
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.MapWithStorage,
                            node.Accessor.TempPlanMapping, node.TempPlanMapping, 1);
#endif
                        planEdges.Add(storAccessPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(node.StorageAttributeOwner != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.PickFromStorageAttribute,
                            node.StorageAttributeOwner.TempPlanMapping, node.TempPlanMapping, 0);
#else
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.PickFromStorageAttribute,
                            node.StorageAttributeOwner.TempPlanMapping, node.TempPlanMapping, 1);
#endif
                        planEdges.Add(storAccessPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(node.ElementBeforeCasting != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            node.ElementBeforeCasting.TempPlanMapping, node.TempPlanMapping, 0);
#else
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            node.ElementBeforeCasting.TempPlanMapping, node.TempPlanMapping, 1);
#endif
                        planEdges.Add(castPlanEdge);
                        node.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
                    }
                }
            }

            // create map with storage plan edges for all pattern graph edges which are the result of a mapping/picking from attribute operation
            for(int i = 0; i < patternGraph.Edges.Length; ++i)
            {
                PatternEdge edge = patternGraph.edges[i];
                if(edge.PointOfDefinition == patternGraph)
                {
                    if(edge.Accessor != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.MapWithStorage,
                            edge.Accessor.TempPlanMapping, edge.TempPlanMapping, 0);
#else
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.MapWithStorage,
                            edge.Accessor.TempPlanMapping, edge.TempPlanMapping, 1);
#endif
                        planEdges.Add(storAccessPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(edge.StorageAttribute != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.PickFromStorageAttribute,
                            edge.StorageAttributeOwner.TempPlanMapping, edge.TempPlanMapping, 0);
#else
                        PlanEdge storAccessPlanEdge = new PlanEdge(SearchOperationType.PickFromStorageAttribute,
                            edge.StorageAttributeOwner.TempPlanMapping, edge.TempPlanMapping, 1);
#endif
                        planEdges.Add(storAccessPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(storAccessPlanEdge);
                    }
                    else if(edge.ElementBeforeCasting != null)
                    {
#if OPCOST_WITH_GEO_MEAN 
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            edge.ElementBeforeCasting.TempPlanMapping, edge.TempPlanMapping, 0);
#else
                        PlanEdge castPlanEdge = new PlanEdge(SearchOperationType.Cast,
                            edge.ElementBeforeCasting.TempPlanMapping, edge.TempPlanMapping, 1);
#endif
                        planEdges.Add(castPlanEdge);
                        edge.TempPlanMapping.IncomingEdges.Add(castPlanEdge);
                    }
                }
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray());
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
                case SearchOperationType.Incident: typeStr = "<->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Implicit: typeStr = "IM"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.ActionPreset: typeStr = " p"; break;
                case SearchOperationType.NegIdptPreset: typeStr = "np"; break;
                case SearchOperationType.SubPreset: typeStr = "sp"; break;
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
                case SearchOperationType.Outgoing: typeStr = src.PatternElement.Name + " -" + tgt.PatternElement.Name + "->"; break;
                case SearchOperationType.Incoming: typeStr = src.PatternElement.Name + " <-" + tgt.PatternElement.Name + "-"; break;
                case SearchOperationType.Incident: typeStr = src.PatternElement.Name + " <-" + tgt.PatternElement.Name + "->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "<-" + src.PatternElement.Name + "- " + tgt.PatternElement.Name; break;
                case SearchOperationType.ImplicitTarget: typeStr = "-" + src.PatternElement.Name + "-> " + tgt.PatternElement.Name; break;
                case SearchOperationType.Implicit: typeStr = "<-" + src.PatternElement.Name + "-> " + tgt.PatternElement.Name; break;
                case SearchOperationType.Lookup: typeStr = "*" + tgt.PatternElement.Name; break;
                case SearchOperationType.ActionPreset: typeStr = "p(" + tgt.PatternElement.Name + ")"; break;
                case SearchOperationType.NegIdptPreset: typeStr = "np(" + tgt.PatternElement.Name + ")"; break;
                case SearchOperationType.SubPreset: typeStr = "sp(" + tgt.PatternElement.Name + ")"; break;
                case SearchOperationType.Condition:
                    typeStr = " ?(" + String.Join(",", ((PatternCondition) op.Element).NeededNodes) + ")("
                        + String.Join(",", ((PatternCondition) op.Element).NeededEdges) + ")";
                    break;
                case SearchOperationType.NegativePattern:
                    typeStr = " !(" + ScheduleToString(((ScheduledSearchPlan) op.Element).Operations) + " )";
                    break;
                case SearchOperationType.IndependentPattern:
                    typeStr = " &(" + ScheduleToString(((ScheduledSearchPlan)op.Element).Operations) + " )";
                    break;
                case SearchOperationType.LockLocalElementsForPatternpath:
                    typeStr = ".LPP.";
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
                            case SearchOperationType.ActionPreset:
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
        /// Generate search plan graph out of the plan graph,
        /// search plan graph only contains edges chosen by the MSA algorithm.
        /// Edges in search plan graph are given in the nodes by outgoing list, as needed for scheduling,
        /// in contrast to incoming list in plan graph, as needed for MSA computation.
        /// </summary>
        /// <param name="planGraph">The source plan graph</param>
        /// <returns>A new search plan graph</returns>
        public SearchPlanGraph GenerateSearchPlanGraph(PlanGraph planGraph)
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
        public ScheduledSearchPlan ScheduleSearchPlan(SearchPlanGraph spGraph, 
            PatternGraph patternGraph, bool isNegativeOrIndependent)
        {
            // the schedule
            List<SearchOperation> operations = new List<SearchOperation>();
            
            // a set of search plan edges representing the currently reachable not yet visited elements
            PriorityQueue<SearchPlanEdge> activeEdges = new PriorityQueue<SearchPlanEdge>();

            // first schedule all preset elements
            foreach (SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if (edge.Target.IsPreset)
                {
                    foreach (SearchPlanEdge edgeOutgoingFromPresetElement in edge.Target.OutgoingEdges)
                        activeEdges.Add(edgeOutgoingFromPresetElement);

                    // note: here a normal preset is converted into a neg/idpt preset operation if in negative/independent pattern
                    SearchOperation newOp = new SearchOperation(
                        isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : edge.Type,
                        edge.Target, spGraph.Root, 0);
                    operations.Add(newOp);
                }
            }

            // then schedule all pick from storage elements
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type==SearchOperationType.PickFromStorage)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPickedElement in edge.Target.OutgoingEdges)
                        activeEdges.Add(edgeOutgoingFromPickedElement);

                    SearchOperation newOp = new SearchOperation(edge.Type,
                        edge.Target, spGraph.Root, 0);
                    newOp.Storage = edge.Target.PatternElement.Storage;
                    operations.Add(newOp);
                }
            }

            // iterate over all reachable elements until the whole graph has been scheduled(/visited),
            // choose next cheapest operation, update the reachable elements and the search plan costs
            SearchPlanNode lastNode = spGraph.Root;
            for(int i = 0; i < spGraph.Nodes.Length - spGraph.NumPresetElements - spGraph.NumPickedElements; ++i)
            {
                foreach (SearchPlanEdge edge in lastNode.OutgoingEdges)
                    if (!edge.Target.IsPreset && (edge.Target.PatternElement.Storage==null || edge.Target.PatternElement.Accessor!=null))
                        activeEdges.Add(edge);

                SearchPlanEdge minEdge = activeEdges.DequeueFirst();
                lastNode = minEdge.Target;

                SearchOperation newOp = new SearchOperation(minEdge.Type,
                    lastNode, minEdge.Source, minEdge.Cost);
                newOp.Storage = minEdge.Target.PatternElement.Storage;
                newOp.StorageAttribute = minEdge.Target.PatternElement.StorageAttribute;

                foreach(SearchOperation op in operations)
                    op.CostToEnd += minEdge.Cost;

                operations.Add(newOp);
            }

            // insert conditions into the schedule
            InsertConditionsIntoSchedule(patternGraph.Conditions, operations);

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            return new ScheduledSearchPlan(patternGraph, operations.ToArray(), cost);
        }

        /// <summary>
        /// Appends homomorphy information to each operation of the scheduled search plan
        /// </summary>
        public void AppendHomomorphyInformation(ScheduledSearchPlan ssp)
        {
            // no operation -> nothing which could be homomorph
            if (ssp.Operations.Length == 0)
            {
                return;
            }

            // iterate operations of the search plan to append homomorphy checks
            for (int i = 0; i < ssp.Operations.Length; ++i)
            {
                if (ssp.Operations[i].Type == SearchOperationType.Condition)
                {
                    continue;
                }

                if (ssp.Operations[i].Type == SearchOperationType.NegativePattern)
                {
                    AppendHomomorphyInformation((ScheduledSearchPlan)ssp.Operations[i].Element);
                    continue;
                }

                if (ssp.Operations[i].Type == SearchOperationType.IndependentPattern)
                {
                    AppendHomomorphyInformation((ScheduledSearchPlan)ssp.Operations[i].Element);
                    continue;
                }

                DetermineAndAppendHomomorphyChecks(ssp, i);
            }
        }

        /// <summary>
        /// Determines which homomorphy check operations are necessary 
        /// at the operation of the given position within the scheduled search plan
        /// and appends them.
        /// </summary>
        public void DetermineAndAppendHomomorphyChecks(ScheduledSearchPlan ssp, int j)
        {
            // take care of global homomorphy
            FillInGlobalHomomorphyPatternElements(ssp, j);
        
            ///////////////////////////////////////////////////////////////////////////
            // first handle special case pure homomorphy

            SearchPlanNode spn_j = (SearchPlanNode)ssp.Operations[j].Element;

            bool homToAll = true;
            if (spn_j.NodeType == PlanNodeType.Node)
            {
                for (int i = 0; i < ssp.PatternGraph.nodes.Length; ++i)
                {
                    if (!ssp.PatternGraph.homomorphicNodes[spn_j.ElementID - 1, i])
                    {
                        homToAll = false;
                        break;
                    }
                }
            }
            else // (spn_j.NodeType == PlanNodeType.Edge)
            {
                for (int i = 0; i < ssp.PatternGraph.edges.Length; ++i)
                {
                    if (!ssp.PatternGraph.homomorphicEdges[spn_j.ElementID - 1, i])
                    {
                        homToAll = false;
                        break;
                    }
                }
            }

            if (homToAll)
            {
                // operation is allowed to be homomorph with everything
                // no checks for isomorphy or restricted homomorphy needed at all
                return;
            }

            ///////////////////////////////////////////////////////////////////////////
            // no pure homomorphy, so we have restricted homomorphy or isomorphy
            // and need to inspect the operations before, together with the homomorphy matrix 
            // for determining the necessary homomorphy checks

            GrGenType[] types;
            bool[,] hom;

            if (spn_j.NodeType == PlanNodeType.Node) {
                types = model.NodeModel.Types;
                hom = ssp.PatternGraph.HomomorphicNodes;
            }
            else { // (spn_j.NodeType == PlanNodeType.Edge)
                types = model.EdgeModel.Types;
                hom = ssp.PatternGraph.HomomorphicEdges;
            }

            // order operation to check against all elements it's not allowed to be homomorph to

            // iterate through the operations before our position
            bool homomorphyPossibleAndAllowed = false;
            for (int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if (ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern
                    || ssp.Operations[i].Type == SearchOperationType.IndependentPattern)
                {
                    continue;
                }

                SearchPlanNode spn_i = (SearchPlanNode)ssp.Operations[i].Element;

                // don't compare nodes with edges
                if (spn_i.NodeType != spn_j.NodeType)
                {
                    continue;
                }
                
                // find out whether element types are disjoint
                GrGenType type_i = types[spn_i.PatternElement.TypeID];
                GrGenType type_j = types[spn_j.PatternElement.TypeID];
                bool disjoint = true;
                foreach (GrGenType subtype_i in type_i.SubOrSameTypes)
                {
                    if (type_j.IsA(subtype_i) || subtype_i.IsA(type_j)) // IsA==IsSuperTypeOrSameType
                    {
                        disjoint = false;
                        break;
                    }
                }

                // don't check elements if their types are disjoint
                if (disjoint)
                {
                    continue;
                }

                // at this position we found out that spn_i and spn_j 
                // might get matched to the same host graph element, i.e. homomorphy is possible
                
                // if that's ok we don't need to insert checks to prevent this from happening
                if (hom[spn_i.ElementID - 1, spn_j.ElementID - 1])
                {
                    homomorphyPossibleAndAllowed = true;
                    continue;
                }

                // otherwise the generated matcher code has to check 
                // that pattern element j doesn't get bound to the same graph element
                // the pattern element i is already bound to 
                if (ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst == null) {
                    ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst = new List<SearchPlanNode>();
                }
                ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst.Add(spn_i);

                // if spn_j might get matched to the same host graph element as spn_i and this is not allowed
                // make spn_i set the is-matched-bit so that spn_j can detect this situation
                ssp.Operations[i].Isomorphy.SetIsMatchedBit = true;
            }

            // only if elements, the operation must be isomorph to, were matched before
            // (otherwise there were only elements, the operation is allowed to be homomorph to,
            //  matched before, so no check needed here)
            if (ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst != null
                && ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst.Count > 0)
            {
                // order operation to check whether the is-matched-bit is set
                ssp.Operations[j].Isomorphy.CheckIsMatchedBit = true;
            }

            // if no check for isomorphy was skipped due to homomorphy being allowed
            // pure isomorphy is to be guaranteed - simply check the is-matched-bit and be done
            // the pattern elements to check against are only needed 
            // if spn_j is allowed to be homomorph to some elements but must be isomorph to some others
            if (ssp.Operations[j].Isomorphy.CheckIsMatchedBit && !homomorphyPossibleAndAllowed)
            {
                ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst = null;
            }
        }

        /// <summary>
        /// fill in globally homomorphic elements as exception to global isomorphy check
        /// </summary>
        public void FillInGlobalHomomorphyPatternElements(ScheduledSearchPlan ssp, int j)
        {
            SearchPlanNode spn_j = (SearchPlanNode)ssp.Operations[j].Element;

            if (spn_j.NodeType == PlanNodeType.Node) {
                if(ssp.PatternGraph.totallyHomomorphicNodes[spn_j.ElementID - 1]) {
                    ssp.Operations[j].Isomorphy.TotallyHomomorph = true;
                    return; // iso-exceptions to totally hom are handled with non-global iso checks
                }
            } else {
                if(ssp.PatternGraph.totallyHomomorphicEdges[spn_j.ElementID - 1]) {
                    ssp.Operations[j].Isomorphy.TotallyHomomorph = true;
                    return; // iso-exceptions to totally hom are handled with non-global iso checks
                }
            }

            bool[,] homGlobal;
            if (spn_j.NodeType == PlanNodeType.Node) {
                homGlobal = ssp.PatternGraph.HomomorphicNodesGlobal;
            }
            else { // (spn_j.NodeType == PlanNodeType.Edge)
                homGlobal = ssp.PatternGraph.HomomorphicEdgesGlobal;
            }

            // iterate through the operations before our position
            for (int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if (ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern
                    || ssp.Operations[i].Type == SearchOperationType.IndependentPattern)
                {
                    continue;
                }

                SearchPlanNode spn_i = (SearchPlanNode)ssp.Operations[i].Element;

                // don't compare nodes with edges
                if (spn_i.NodeType != spn_j.NodeType)
                {
                    continue;
                }
           
                // in global isomorphy check at current position 
                // allow globally homomorphic elements as exception
                // if they were already defined(preset)
                if (homGlobal[spn_j.ElementID - 1, spn_i.ElementID - 1])
                {
                    if (ssp.Operations[j].Isomorphy.GloballyHomomorphPatternElements == null)
                    {
                        ssp.Operations[j].Isomorphy.GloballyHomomorphPatternElements = new List<SearchPlanNode>();
                    }
                    ssp.Operations[j].Isomorphy.GloballyHomomorphPatternElements.Add(spn_i);
                }
            }
        }

        /// <summary>
        /// Negative/Independent schedules are merged as an operation into their enclosing schedules,
        /// at a position determined by their costs but not before all of their needed elements were computed
        /// </summary>
        public void MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(PatternGraph patternGraph)
        {
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(neg);
            }

            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(idpt);
            }

            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(altCase);
                }
            }

            foreach (Iterated iter in patternGraph.iterateds)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(iter.iteratedPattern);
            }

            InsertNegativesAndIndependentsIntoSchedule(patternGraph);
        }

        /// <summary>
        /// Inserts schedules of negative and independent pattern graphs into the schedule of the enclosing pattern graph
        /// </summary>
        public void InsertNegativesAndIndependentsIntoSchedule(PatternGraph patternGraph)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                InsertNegativesAndIndependentsIntoSchedule(patternGraph, i);
            }
        }

        /// <summary>
        /// Inserts schedules of negative and independent pattern graphs into the schedule of the enclosing pattern graph
        /// for the schedule with the given array index
        /// </summary>
        public void InsertNegativesAndIndependentsIntoSchedule(PatternGraph patternGraph, int index)
        {
            // todo: erst implicit node, dann negative/independent, auch wenn negative/independent mit erstem implicit moeglich wird

            Debug.Assert(patternGraph.schedulesIncludingNegativesAndIndependents[index] == null);
            List<SearchOperation> operations = new List<SearchOperation>();
            for(int i = 0; i < patternGraph.schedules[index].Operations.Length; ++i)
                operations.Add(patternGraph.schedules[index].Operations[i]);

            // nested patterns on the way to an enclosed patternpath modifier 
            // must get matched after all local nodes and edges, because they require 
            // all outer elements to be known in order to lock them for patternpath processing
            if (patternGraph.patternGraphsOnPathToEnclosedPatternpath
                .Contains(patternGraph.pathPrefix + patternGraph.name))
            {
                operations.Add(new SearchOperation(SearchOperationType.LockLocalElementsForPatternpath, null, null,
                    patternGraph.schedules[index].Operations.Length!=0 ? patternGraph.schedules[index].Operations[patternGraph.schedules[index].Operations.Length-1].CostToEnd : 0));
            }

            // iterate over all negative scheduled search plans (TODO: order?)
            for (int i = 0; i < patternGraph.negativePatternGraphs.Length; ++i)
            {
                ScheduledSearchPlan negSchedule = patternGraph.negativePatternGraphs[i].schedulesIncludingNegativesAndIndependents[0];
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                // find best place in scheduled search plan for current negative pattern 
                // during search from end of schedule forward until the first element the negative pattern is dependent on is found
                for (int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if (op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern)
                    {
                        continue;
                    }

                    if (LazyNegativeIndependentConditionEvaluation)
                        break;

                    if (op.Type == SearchOperationType.LockLocalElementsForPatternpath)
                    {
                        break; // LockLocalElementsForPatternpath is barrier for neg/idpt
                    }

                    if (patternGraph.negativePatternGraphs[i].neededNodes.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name)
                        || patternGraph.negativePatternGraphs[i].neededEdges.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
                    {
                        break;
                    }

                    if (negSchedule.Cost <= op.CostToEnd)
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
                for (int j = 0; j < bestFitIndex; ++j)
                    operations[j].CostToEnd += negSchedule.Cost;
            }

            // iterate over all independent scheduled search plans (TODO: order?)
            for (int i = 0; i < patternGraph.independentPatternGraphs.Length; ++i)
            {
                ScheduledSearchPlan idptSchedule = patternGraph.independentPatternGraphs[i].schedulesIncludingNegativesAndIndependents[0];
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                // find best place in scheduled search plan for current independent pattern 
                // during search from end of schedule forward until the first element the independent pattern is dependent on is found
                for (int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if (op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern)
                    {
                        continue;
                    }

                    if (LazyNegativeIndependentConditionEvaluation)
                        break;

                    if (op.Type == SearchOperationType.LockLocalElementsForPatternpath)
                    {
                        break; // LockLocalElementsForPatternpath is barrier for neg/idpt
                    }

                    if (patternGraph.independentPatternGraphs[i].neededNodes.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name)
                        || patternGraph.independentPatternGraphs[i].neededEdges.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
                    {
                        break;
                    }

                    if (idptSchedule.Cost <= op.CostToEnd)
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
                for (int j = 0; j < bestFitIndex; ++j)
                    operations[j].CostToEnd += idptSchedule.Cost;
            }

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            patternGraph.schedulesIncludingNegativesAndIndependents[index] =
                new ScheduledSearchPlan(patternGraph, operations.ToArray(), cost);
        }

        /// <summary>
        /// Inserts conditions into the schedule given by the operations list at their earliest possible position
        /// todo: set/map operations are potentially expensive, 
        /// they shouldn't be insertes asap, but depending an weight, 
        /// derived from statistics over set/map size for graph elements, quiet well known for anonymous rule sets
        /// </summary>
        public void InsertConditionsIntoSchedule(PatternCondition[] conditions, List<SearchOperation> operations)
        {
            // get needed (in order to evaluate it) elements of each condition 
            Dictionary<String, bool>[] neededElements = new Dictionary<String, bool>[conditions.Length];
            for (int i = 0; i < conditions.Length; ++i)
            {
                neededElements[i] = new Dictionary<string, bool>();
                foreach (String neededNode in conditions[i].NeededNodes)
                    neededElements[i][neededNode] = true;
                foreach (String neededEdge in conditions[i].NeededEdges)
                    neededElements[i][neededEdge] = true;
            }

            // iterate over all conditions
            for (int i = 0; i < conditions.Length; ++i)
            {
                int j;
                float costToEnd = 0;

                // find leftmost place in scheduled search plan for current condition
                // by search from end of schedule forward until the first element the condition is dependent on is found
                for (j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if (op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern)
                    {
                        continue;
                    }

                    if (LazyNegativeIndependentConditionEvaluation)
                        break;

                    if (neededElements[i].ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
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
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the given rule pattern into the given source builder
        /// </summary>
        public void GenerateActionAndMatcher(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, bool isInitialStatic)
        {
            // generate the search program out of the schedule(s) within the pattern graph of the rule
            SearchProgram searchProgram = GenerateSearchProgram(matchingPattern);

            // emit matcher class head, body, tail; body is source code representing search program
            if(matchingPattern is LGSPRulePattern) {
                GenerateActionInterface(sb, (LGSPRulePattern)matchingPattern); // generate the exact action interface
                GenerateMatcherClassHeadAction(sb, (LGSPRulePattern)matchingPattern, isInitialStatic);
                searchProgram.Emit(sb);
                GenerateActionImplementation(sb, (LGSPRulePattern)matchingPattern);
                GenerateMatcherClassTail(sb);
            } else {
                GenerateMatcherClassHeadSubpattern(sb, matchingPattern, isInitialStatic);
                searchProgram.Emit(sb);
                GenerateMatcherClassTail(sb);
            }

            // finally generate matcher source for all the nested alternatives or iterateds of the pattern graph
            // nested inside the alternatives,iterateds,negatives,independents
            foreach(Alternative alt in matchingPattern.patternGraph.alternatives)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach (Iterated iter in matchingPattern.patternGraph.iterateds)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
            }
            foreach (PatternGraph neg in matchingPattern.patternGraph.negativePatternGraphs)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
            }
            foreach (PatternGraph idpt in matchingPattern.patternGraph.independentPatternGraphs)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, idpt, isInitialStatic);
            }
        }

        /// <summary>
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the given alternative into the given source builder
        /// </summary>
        public void GenerateActionAndMatcherOfAlternative(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, Alternative alt, bool isInitialStatic)
        {
            // generate the search program out of the schedules within the pattern graphs of the alternative cases
            SearchProgram searchProgram = GenerateSearchProgramAlternative(matchingPattern, alt);

            // emit matcher class head, body, tail; body is source code representing search program
            GenerateMatcherClassHeadAlternative(sb, matchingPattern, alt, isInitialStatic);
            searchProgram.Emit(sb);
            GenerateMatcherClassTail(sb);

            // handle alternatives or iterateds nested in the alternative cases
            foreach (PatternGraph altCase in alt.alternativeCases)
            {
                // nested inside the alternatives,iterateds,negatives,independents
                foreach (Alternative nestedAlt in altCase.alternatives)
                {
                    GenerateActionAndMatcherOfAlternative(sb, matchingPattern, nestedAlt, isInitialStatic);
                }
                foreach (Iterated iter in altCase.iterateds)
                {
                    GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
                }
                foreach (PatternGraph neg in altCase.negativePatternGraphs)
                {
                    GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
                }
                foreach (PatternGraph idpt in altCase.independentPatternGraphs)
                {
                    GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, idpt, isInitialStatic);
                }
            }
        }

        /// <summary>
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the given iterated pattern into the given source builder
        /// </summary>
        public void GenerateActionAndMatcherOfIterated(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, PatternGraph iter, bool isInitialStatic)
        {
            // generate the search program out of the schedule within the pattern graph of the iterated pattern
            SearchProgram searchProgram = GenerateSearchProgramIterated(matchingPattern, iter);

            // emit matcher class head, body, tail; body is source code representing search program
            GenerateMatcherClassHeadIterated(sb, matchingPattern, iter, isInitialStatic);
            searchProgram.Emit(sb);
            GenerateMatcherClassTail(sb);

            // finally generate matcher source for all the nested alternatives or iterateds of the iterated pattern graph
            // nested inside the alternatives,iterateds,negatives,independents
            foreach (Alternative alt in iter.alternatives)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach (Iterated nestedIter in iter.iterateds)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, nestedIter.iteratedPattern, isInitialStatic);
            }
            foreach (PatternGraph neg in iter.negativePatternGraphs)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
            }
            foreach (PatternGraph idpt in iter.independentPatternGraphs)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, idpt, isInitialStatic);
            }
        }

        /// <summary>
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the alternatives/iterateds nested within the given negative/independent pattern graph into the given source builder
        /// </summary>
        public void GenerateActionAndMatcherOfNestedPatterns(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, PatternGraph negOrIdpt, bool isInitialStatic)
        {
            // nothing to do locally ..

            // .. just move on to the nested alternatives or iterateds
            foreach (Alternative alt in negOrIdpt.alternatives)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach (Iterated iter in negOrIdpt.iterateds)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
            }
            foreach (PatternGraph nestedNeg in negOrIdpt.negativePatternGraphs)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, nestedNeg, isInitialStatic);
            }
            foreach (PatternGraph nestedIdpt in negOrIdpt.independentPatternGraphs)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, nestedIdpt, isInitialStatic);
            }
        }

        /// <summary>
        // generate the exact action interface
        /// </summary>
        void GenerateActionInterface(SourceBuilder sb, LGSPRulePattern matchingPattern)
        {
            String actionInterfaceName = "IAction_"+matchingPattern.name;
            String outParameters = "";
            String refParameters = "";
            for(int i=0; i<matchingPattern.Outputs.Length; ++i) {
                outParameters += ", out " + TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_"+i;
                refParameters += ", ref " + TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_"+i;
            }
            String inParameters = "";
            for(int i=0; i<matchingPattern.Inputs.Length; ++i) {
                inParameters += ", " + TypesHelper.TypeName(matchingPattern.Inputs[i]) + " " + matchingPattern.InputNames[i];
            }
            String matchingPatternClassName = matchingPattern.GetType().Name;
            String patternName = matchingPattern.name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">" ;

            sb.AppendFront("/// <summary>\n");
            sb.AppendFront("/// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.\n");
            sb.AppendFront("/// </summary>\n");
            sb.AppendFrontFormat("public interface {0}\n", actionInterfaceName);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("/// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("{0} Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches{1});\n", matchesType, inParameters);

            sb.AppendFront("/// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} match{1});\n", matchType, outParameters);

            sb.AppendFront("/// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} matches{1});\n", matchesType, outParameters);

            sb.AppendFront("/// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>\n");
            sb.AppendFrontFormat("bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1});\n", inParameters, refParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>\n");
            sb.AppendFrontFormat("bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1});\n", inParameters, refParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0});\n", inParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0});\n", inParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max{0});\n", inParameters);
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("\n");
        }

        /// <summary>
        // generate implementation of the exact action interface,
        // delegate calls of the inexact action interface IAction to the exact action interface
        /// </summary>
        void GenerateActionImplementation(SourceBuilder sb, LGSPRulePattern matchingPattern)
        {
            String inParameters = "";
            String inArguments = "";
            String inArgumentsFromArray = "";
            for (int i = 0; i < matchingPattern.Inputs.Length; ++i)
            {
                inParameters += ", " + TypesHelper.TypeName(matchingPattern.Inputs[i]) + " " + matchingPattern.InputNames[i];
                inArguments += ", " + matchingPattern.InputNames[i];
                inArgumentsFromArray += ", (" + TypesHelper.TypeName(matchingPattern.Inputs[i]) + ") parameters[" + i + "]";
            }

            String outParameters = "";
            String refParameters = "";
            String outLocals = "";
            String refLocals = "";
            String outArguments = "";
            String refArguments = "";
            for (int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                outParameters += ", out " + TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_" + i;
                refParameters += ", ref " + TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_" + i;
                outLocals += TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_" + i + "; ";
                refLocals += TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_" + i + " = " + TypesHelper.DefaultValueString(matchingPattern.Outputs[i].Name, model) + "; ";
                outArguments += ", out output_" + i;
                refArguments += ", ref output_" + i;
            }

            String matchingPatternClassName = matchingPattern.GetType().Name;
            String patternName = matchingPattern.name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";

            // implementation of exact action interface

            sb.AppendFront("/// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>\n");
            sb.AppendFrontFormat("public delegate {0} MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches{1});\n", matchesType, inParameters);

            sb.AppendFront("/// <summary> A delegate pointing to the current matcher program for this rule. </summary>\n");
            sb.AppendFront("public MatchInvoker DynamicMatch;\n");

            sb.AppendFront("/// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>\n");
            sb.AppendFront("public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }\n");

            sb.AppendFrontFormat("public {0} Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches{1})\n", matchesType, inParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches{0});\n", inArguments);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} match{1})\n", matchType, outParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match{0});\n", outArguments);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} matches{1})\n", matchesType, outParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            for (int i = 0; i < matchingPattern.Outputs.Length; ++i) {
                sb.AppendFrontFormat("output_{0} = {1};\n", i, TypesHelper.DefaultValueString(matchingPattern.Outputs[i].Name, model));
            }
            sb.AppendFrontFormat("foreach({0} match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match{1});\n", matchType, outArguments);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1})\n", inParameters, refParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{1});\n", matchesType, inArguments);
            sb.AppendFront("if(matches.Count <= 0) return false;\n");
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.AppendFront("return true;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1})\n", inParameters, refParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches{1});\n", matchesType, inArguments);
            sb.AppendFront("if(matches.Count <= 0) return false;\n");
            sb.AppendFrontFormat("foreach({0} match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match{1});\n", matchType, outArguments);
            sb.AppendFront("return true;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0})\n", inParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches;\n", matchesType);
            sb.AppendFrontFormat("{0}\n", outLocals);

            sb.AppendFront("while(true)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{0});\n", inArguments);
            sb.AppendFront("if(matches.Count <= 0) return true;\n");
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0})\n", inParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{1});\n", matchesType, inArguments);
            sb.AppendFront("if(matches.Count <= 0) return false;\n");
            sb.AppendFrontFormat("{0}\n", outLocals);
            sb.AppendFront("do\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.AppendFrontFormat("matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{0});\n", inArguments);
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("while(matches.Count > 0) ;\n");
            sb.AppendFront("return true;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max{0})\n", inParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches;\n", matchesType);
            sb.AppendFrontFormat("{0}\n", outLocals);
            sb.AppendFront("for(int i = 0; i < max; i++)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{0});\n", inArguments);
            sb.AppendFront("if(matches.Count <= 0) return i >= min;\n");
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return true;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            // implementation of inexact action interface by delegation to exact action interface
            sb.AppendFront("// implementation of inexact action interface by delegation to exact action interface\n");

            sb.AppendFront("public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("return Match(actionEnv, maxMatches{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", outLocals);
            sb.AppendFrontFormat("Modify(actionEnv, ({0})match{1});\n", matchType, outArguments);
            for (int i = 0; i < matchingPattern.Outputs.Length; ++i) {
                sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
            }
            sb.AppendFront("return ReturnArray;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", outLocals);
            sb.AppendFrontFormat("ModifyAll(actionEnv, ({0})matches{1});\n", matchesType, outArguments);
            for (int i = 0; i < matchingPattern.Outputs.Length; ++i) {
                sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
            }
            sb.AppendFront("return ReturnArray;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if (matchingPattern.Inputs.Length == 0) {
                sb.AppendFrontFormat("{0}\n", refLocals);
                sb.AppendFrontFormat("if(Apply(actionEnv{0})) ", refArguments);
                sb.Append("{\n");
                sb.Indent();
                for (int i = 0; i < matchingPattern.Outputs.Length; ++i) {
                    sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
                }
                sb.AppendFront("return ReturnArray;\n");
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.AppendFront("else return null;\n");
            }
            else sb.AppendFront("throw new Exception();\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", refLocals);
            sb.AppendFrontFormat("if(Apply(actionEnv{0}{1})) ", inArgumentsFromArray, refArguments);
            sb.Append("{\n");
            sb.Indent();
            for (int i = 0; i < matchingPattern.Outputs.Length; ++i) {
                sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
            }
            sb.AppendFront("return ReturnArray;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");
            sb.AppendFront("else return null;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if (matchingPattern.Inputs.Length == 0) {
                sb.AppendFrontFormat("{0}\n", refLocals);
                sb.AppendFrontFormat("if(ApplyAll(maxMatches, actionEnv{0})) ", refArguments);
                sb.Append("{\n");
                sb.Indent();
                for (int i = 0; i < matchingPattern.Outputs.Length; ++i) {
                    sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
                }
                sb.AppendFront("return ReturnArray;\n");
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.AppendFront("else return null;\n");
            }
            else sb.AppendFront("throw new Exception();\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", refLocals);
            sb.AppendFrontFormat("if(ApplyAll(maxMatches, actionEnv{0}{1})) ", inArgumentsFromArray, refArguments);
            sb.Append("{\n");
            sb.Indent();
            for (int i = 0; i < matchingPattern.Outputs.Length; ++i) {
                sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
            }
            sb.AppendFront("return ReturnArray;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");
            sb.AppendFront("else return null;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if (matchingPattern.Inputs.Length == 0) sb.AppendFront("return ApplyStar(actionEnv);\n");
            else sb.AppendFront("throw new Exception(); return false;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("return ApplyStar(actionEnv{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if (matchingPattern.Inputs.Length == 0) sb.AppendFront("return ApplyPlus(actionEnv);\n");
            else sb.AppendFront("throw new Exception(); return false;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("return ApplyPlus(actionEnv{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if (matchingPattern.Inputs.Length == 0) sb.AppendFront("return ApplyMinMax(actionEnv, min, max);\n");
            else sb.AppendFront("throw new Exception(); return false;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent(); 
            sb.AppendFrontFormat("return ApplyMinMax(actionEnv, min, max{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");
        }

        /// <summary>
        /// Generates the search program(s) for the pattern graph of the given rule
        /// </summary>
        SearchProgram GenerateSearchProgram(LGSPMatchingPattern matchingPattern)
        {
            PatternGraph patternGraph = matchingPattern.patternGraph;
            SearchProgram searchProgramRoot = null;
            SearchProgram searchProgramListEnd = null;

            for(int i=0; i<patternGraph.schedulesIncludingNegativesAndIndependents.Length; ++i)
            {
                ScheduledSearchPlan scheduledSearchPlan = patternGraph.schedulesIncludingNegativesAndIndependents[i];

#if DUMP_SCHEDULED_SEARCH_PLAN
                StreamWriter sspwriter = new StreamWriter(matchingPattern.name + i + "_ssp_dump.txt");
                float prevCostToEnd = scheduledSearchPlan.Operations.Length > 0 ? scheduledSearchPlan.Operations[0].CostToEnd : 0f;
                foreach (SearchOperation so in scheduledSearchPlan.Operations)
                {
                    sspwriter.Write(SearchOpToString(so) + " ; " + so.CostToEnd + " (+" + (prevCostToEnd-so.CostToEnd) + ")" + "\n");
                    prevCostToEnd = so.CostToEnd;
                }
                sspwriter.Close();
#endif

                // build pass: build nested program from scheduled search plan
                SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
                if(matchingPattern is LGSPRulePattern)
                {
                    SearchProgram sp = searchProgramBuilder.BuildSearchProgram(model,
                        (LGSPRulePattern)matchingPattern, i, null);
                    if(i==0) searchProgramRoot = searchProgramListEnd = sp;
                    else searchProgramListEnd = (SearchProgram)searchProgramListEnd.Append(sp);
                }
                else
                {
                    Debug.Assert(searchProgramRoot==null);
                    searchProgramRoot = searchProgramListEnd = searchProgramBuilder.BuildSearchProgram(
                        model, matchingPattern);
                }
            }

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_" + searchProgramRoot.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter searchProgramCompleter = new SearchProgramCompleter();
            searchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgramRoot);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgramRoot.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_" + searchProgramRoot.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgramRoot;
        }

        /// <summary>
        /// Generates the search program for the given alternative 
        /// </summary>
        SearchProgram GenerateSearchProgramAlternative(LGSPMatchingPattern matchingPattern, Alternative alt)
        {
            /*ScheduledSearchPlan[] scheduledSearchPlans = new ScheduledSearchPlan[alt.alternativeCases.Length];
            int i=0;
            foreach (PatternGraph altCase in alt.alternativeCases) {
                scheduledSearchPlans[i] = altCase.scheduleIncludingNegativesAndIndependents;
                ++i;
            }*/ // todo: needed?

            // build pass: build nested program from scheduled search plans of the alternative cases
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, alt);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_" + alt.name + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter searchProgramCompleter = new SearchProgramCompleter();
            searchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_" + alt.name + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgram;
        }

        /// <summary>
        /// Generates the search program for the given iterated pattern
        /// </summary>
        SearchProgram GenerateSearchProgramIterated(LGSPMatchingPattern matchingPattern, PatternGraph iter)
        {
            // build pass: build nested program from scheduled search plan of the all pattern
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, iter);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_" + iter.name + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter searchProgramCompleter = new SearchProgramCompleter();
            searchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_" + iter.name + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgram;
        }

        /// <summary>
        /// Generates file header for actions file into given source builer
        /// </summary>
        public void GenerateFileHeaderForActionsFile(SourceBuilder sb,
                String namespaceOfModel, String namespaceOfRulePatterns)
        {
            sb.AppendFront("using System;\n"
                + "using System.Collections.Generic;\n"
                + "using System.Collections;\n"
                + "using System.Text;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
                + "using GRGEN_MODEL = " + namespaceOfModel + ";\n"
                + "using " + namespaceOfRulePatterns + ";\n\n");
            sb.AppendFront("namespace de.unika.ipd.grGen.lgspActions\n");
            sb.AppendFront("{\n");
            sb.Indent(); // namespace level
        }

        /// <summary>
        /// Generates matcher class head source code for the pattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadAction(SourceBuilder sb,
            LGSPRulePattern rulePattern, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;
                
            String namePrefix = (isInitialStatic ? "" : "Dyn") + "Action_";
            String className = namePrefix + rulePattern.name;
            String rulePatternClassName = rulePattern.GetType().Name;
            String matchClassName = rulePatternClassName + "." + "Match_" + rulePattern.name;
            String matchInterfaceName = rulePatternClassName + "." + "IMatch_" + rulePattern.name;
            String actionInterfaceName = "IAction_" + rulePattern.name;

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPAction, "
                + "GRGEN_LIBGR.IAction, " + actionInterfaceName + "\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("public " + className + "() {\n");
            sb.Indent(); // method body level
            sb.AppendFront("_rulePattern = " + rulePatternClassName + ".Instance;\n");
            sb.AppendFront("patternGraph = _rulePattern.patternGraph;\n");
            sb.AppendFront("DynamicMatch = myMatch;\n");
            sb.AppendFrontFormat("ReturnArray = new object[{0}];\n", rulePattern.Outputs.Length);
            sb.AppendFront("matches = new GRGEN_LGSP.LGSPMatchesList<" + matchClassName +", " + matchInterfaceName + ">(this);\n");
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            sb.AppendFront("public " + rulePatternClassName + " _rulePattern;\n");
            sb.AppendFront("public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }\n");
            sb.AppendFront("public override string Name { get { return \"" + rulePattern.name + "\"; } }\n");
            sb.AppendFront("private GRGEN_LGSP.LGSPMatchesList<" + matchClassName + ", " + matchInterfaceName + "> matches;\n\n");
            if (isInitialStatic)
            {
                sb.AppendFront("public static " + className + " Instance { get { return instance; } }\n");
                sb.AppendFront("private static " + className + " instance = new " + className + "();\n");
            }

            GenerateIndependentsMatchObjects(sb, rulePatternClassName, patternGraph);

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the subpattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadSubpattern(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, bool isInitialStatic)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "PatternAction_";
            String className = namePrefix + matchingPattern.name;
            String matchingPatternClassName = matchingPattern.GetType().Name;

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            sb.AppendFront("patternGraph = " + matchingPatternClassName + ".Instance.patternGraph;\n");
           
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            GenerateTasksMemoryPool(sb, className, false, false);

            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                PatternNode node = patternGraph.nodes[i];
                if (node.PointOfDefinition == null)
                {
                    sb.AppendFront("public GRGEN_LGSP.LGSPNode " + node.name + ";\n");
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                PatternEdge edge = patternGraph.edges[i];
                if (edge.PointOfDefinition == null)
                {
                    sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + edge.name + ";\n");
                }
            }
            for (int i = 0; i < patternGraph.variables.Length; ++i)
            {
                PatternVariable variable = patternGraph.variables[i];
                sb.AppendFront("public " +TypesHelper.TypeName(variable.Type) + " " + variable.name + ";\n");
            }

            GenerateIndependentsMatchObjects(sb, matchingPatternClassName, patternGraph);

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the given alternative into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadAlternative(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, Alternative alternative, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "AlternativeAction_";
            String className = namePrefix + alternative.pathPrefix+alternative.name;

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            // pfadausdruck gebraucht, da das alternative-objekt im pattern graph steckt
            sb.AppendFront("patternGraphs = patternGraphs_;\n");
            
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            GenerateTasksMemoryPool(sb, className, true, false);

            Dictionary<string, bool> neededNodes = new Dictionary<string,bool>();
            Dictionary<string, bool> neededEdges = new Dictionary<string,bool>();
            Dictionary<string, GrGenType> neededVariables = new Dictionary<string, GrGenType>();
            foreach (PatternGraph altCase in alternative.alternativeCases)
            {
                foreach (KeyValuePair<string, bool> neededNode in altCase.neededNodes)
                    neededNodes[neededNode.Key] = neededNode.Value;
                foreach (KeyValuePair<string, bool> neededEdge in altCase.neededEdges)
                    neededEdges[neededEdge.Key] = neededEdge.Value;
                foreach (KeyValuePair<string, GrGenType> neededVariable in altCase.neededVariables)
                    neededVariables[neededVariable.Key] = neededVariable.Value;
            }
            foreach (KeyValuePair<string, bool> node in neededNodes)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPNode " + node.Key + ";\n");
            }
            foreach (KeyValuePair<string, bool> edge in neededEdges)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + edge.Key + ";\n");
            }
            foreach (KeyValuePair<string, GrGenType> variable in neededVariables)
            {
                sb.AppendFront("public " + TypesHelper.TypeName(variable.Value) + " " + variable.Key + ";\n");
            }
    
            foreach (PatternGraph altCase in alternative.alternativeCases)
            {
                GenerateIndependentsMatchObjects(sb, matchingPattern.GetType().Name, altCase);
            }

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the given iterated pattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadIterated(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, PatternGraph iter, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "IteratedAction_";
            String className = namePrefix + iter.pathPrefix + iter.name;
            String matchingPatternClassName = matchingPattern.GetType().Name;

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            sb.AppendFront("patternGraph = " + matchingPatternClassName + ".Instance.patternGraph;\n");
            int index = -1;
            for (int i=0; i<iter.embeddingGraph.iterateds.Length; ++i) {
                if (iter.embeddingGraph.iterateds[i].iteratedPattern == iter) index = i;
            }
            sb.AppendFrontFormat("minMatchesIter = {0};\n", iter.embeddingGraph.iterateds[index].minMatches);
            sb.AppendFrontFormat("maxMatchesIter = {0};\n", iter.embeddingGraph.iterateds[index].maxMatches);
            sb.AppendFront("numMatchesIter = 0;\n");
            if(iter.isIterationBreaking)
                sb.AppendFront("breakIteration = false;\n");

            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            sb.AppendFront("int minMatchesIter;\n");
            sb.AppendFront("int maxMatchesIter;\n");
            sb.AppendFront("int numMatchesIter;\n");
            if(iter.isIterationBreaking)
                sb.AppendFront("bool breakIteration;\n");
            sb.Append("\n");

            GenerateTasksMemoryPool(sb, className, false, iter.isIterationBreaking);

            Dictionary<string, bool> neededNodes = new Dictionary<string, bool>();
            Dictionary<string, bool> neededEdges = new Dictionary<string, bool>();
            Dictionary<string, GrGenType> neededVariables = new Dictionary<string, GrGenType>();
            foreach (KeyValuePair<string, bool> neededNode in iter.neededNodes)
                neededNodes[neededNode.Key] = neededNode.Value;
            foreach (KeyValuePair<string, bool> neededEdge in iter.neededEdges)
                neededEdges[neededEdge.Key] = neededEdge.Value;
            foreach (KeyValuePair<string, GrGenType> neededVariable in iter.neededVariables)
                neededVariables[neededVariable.Key] = neededVariable.Value;
            foreach (KeyValuePair<string, bool> node in neededNodes)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPNode " + node.Key + ";\n");
            }
            foreach (KeyValuePair<string, bool> edge in neededEdges)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + edge.Key + ";\n");
            }
            foreach (KeyValuePair<string, GrGenType> variable in neededVariables)
            {
                sb.AppendFront("public " + TypesHelper.TypeName(variable.Value) + " " + variable.Key + ";\n");
            }

            GenerateIndependentsMatchObjects(sb, matchingPattern.GetType().Name, iter);

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates match objects of independents (one pre-allocated is part of action class)
        /// </summary>
        private void GenerateIndependentsMatchObjects(SourceBuilder sb,
            string matchingPatternClassName, PatternGraph patternGraph)
        {
            if (patternGraph.pathPrefixesAndNamesOfNestedIndependents != null)
            {
                foreach (Pair<String, String> independentName in patternGraph.pathPrefixesAndNamesOfNestedIndependents)
                {
                    sb.AppendFrontFormat("private {0} {1} = new {0}();",
                        matchingPatternClassName + "." + NamesOfEntities.MatchClassName(independentName.fst + independentName.snd),
                        NamesOfEntities.MatchedIndependentVariable(independentName.fst + independentName.snd));
                }
            }

            foreach (PatternGraph idpt in patternGraph.IndependentPatternGraphs)
            {
                GenerateIndependentsMatchObjects(sb, matchingPatternClassName, idpt);
            }
        }

        /// <summary>
        /// Generates memory pooling code for matching tasks of class given by it's name
        /// </summary>
        private void GenerateTasksMemoryPool(SourceBuilder sb, String className, bool isAlternative, bool isIterationBreaking)
        {
            // getNewTask method handing out new task from pool or creating task if pool is empty
            if (isAlternative)
                sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                    + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {\n");
            else
                sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                    + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {\n");
            sb.Indent();
            sb.AppendFront(className + " newTask;\n");
            sb.AppendFront("if(numFreeTasks>0) {\n");
            sb.Indent();
            sb.AppendFront("newTask = freeListHead;\n"); 
            sb.AppendFront("newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;\n");
            if(isAlternative)
                sb.AppendFront("newTask.patternGraphs = patternGraphs_;\n");
            else if(isIterationBreaking)
                sb.AppendFront("newTask.breakIteration = false;\n");
            sb.AppendFront("freeListHead = newTask.next;\n");
            sb.AppendFront("newTask.next = null;\n");
            sb.AppendFront("--numFreeTasks;\n");
            sb.Unindent();
            sb.AppendFront("} else {\n");
            sb.Indent();
            if (isAlternative)
                sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_, patternGraphs_);\n");
            else
                sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_);\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return newTask;\n");
            sb.Unindent();
            sb.AppendFront("}\n\n");

            // releaseTask method recycling task into pool if pool is not full
            sb.AppendFront("public static void releaseTask(" + className + " oldTask) {\n");
            sb.Indent();
            sb.AppendFront("if(numFreeTasks<MAX_NUM_FREE_TASKS) {\n");
            sb.Indent();
            sb.AppendFront("oldTask.next = freeListHead;\n");
            sb.AppendFront("oldTask.actionEnv = null; oldTask.openTasks = null;\n");
            sb.AppendFront("freeListHead = oldTask;\n");
            sb.AppendFront("++numFreeTasks;\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n\n");

            // tasks pool administration data
            sb.AppendFront("private static " + className + " freeListHead = null;\n");
            sb.AppendFront("private static int numFreeTasks = 0;\n");
            sb.AppendFront("private const int MAX_NUM_FREE_TASKS = 100;\n\n"); // todo: compute antiproportional to pattern size

            sb.AppendFront("private " + className + " next = null;\n\n");
        }

        /// <summary>
        /// Generates matcher class tail source code
        /// </summary>
        public void GenerateMatcherClassTail(SourceBuilder sb)
        {
            sb.Unindent();
            sb.AppendFront("}\n\n");
        }

        /// <summary>
        /// Generates scheduled search plans needed for matcher code generation for action compilation
        /// out of graph with analyze information, 
        /// The scheduled search plans are added to the main and the nested pattern graphs.
        /// </summary>
        public void GenerateScheduledSearchPlans(PatternGraph patternGraph, LGSPGraph graph,
            bool isSubpattern, bool isNegativeOrIndependent)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                patternGraph.AdaptToMaybeNull(i);
                PlanGraph planGraph = GeneratePlanGraph(graph, patternGraph,
                    isNegativeOrIndependent, isSubpattern);
                MarkMinimumSpanningArborescence(planGraph, patternGraph.name);
                SearchPlanGraph searchPlanGraph = GenerateSearchPlanGraph(planGraph);
                ScheduledSearchPlan scheduledSearchPlan = ScheduleSearchPlan(
                    searchPlanGraph, patternGraph, isNegativeOrIndependent);
                AppendHomomorphyInformation(scheduledSearchPlan);
                patternGraph.schedules[i] = scheduledSearchPlan;
                patternGraph.RevertMaybeNullAdaption(i);

                foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
                {
                    GenerateScheduledSearchPlans(neg, graph, isSubpattern, true);
                }

                foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
                {
                    GenerateScheduledSearchPlans(idpt, graph, isSubpattern, true);
                }

                foreach (Alternative alt in patternGraph.alternatives)
                {
                    foreach (PatternGraph altCase in alt.alternativeCases)
                    {
                        GenerateScheduledSearchPlans(altCase, graph, isSubpattern, false);
                    }
                }

                foreach (Iterated iter in patternGraph.iterateds)
                {
                    GenerateScheduledSearchPlans(iter.iteratedPattern, graph, isSubpattern, false);
                }
            }
        }

        /// <summary>
        /// Setup of compiler parameters for recompilation of actions at runtime taking care of analyze information
        /// </summary>
        public CompilerParameters GetDynCompilerSetup(String modelAssemblyLocation, String actionAssemblyLocation)
        {
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyLocation);
            compParams.ReferencedAssemblies.Add(actionAssemblyLocation);

            compParams.GenerateInMemory = true;
            compParams.CompilerOptions = "/optimize";
            return compParams;
        }

        /// <summary>
        /// Generates an LGSPAction object for the given scheduled search plan.
        /// </summary>
        /// <param name="action">Needed for the rule pattern and the name</param>
        /// <param name="sourceOutputFilename">null if no output file needed</param>
        public LGSPAction GenerateAction(ScheduledSearchPlan scheduledSearchPlan, LGSPAction action,
            String modelAssemblyLocation, String actionAssemblyLocation, String sourceOutputFilename)
        {
            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            GenerateFileHeaderForActionsFile(sourceCode, model.GetType().Namespace, action.rulePattern.GetType().Namespace);

            // can't generate new subpattern matchers due to missing scheduled search plans for them / missing graph analyze data
            Debug.Assert(action.rulePattern.patternGraph.embeddedGraphs.Length == 0);

            GenerateActionAndMatcher(sourceCode, action.rulePattern, false);

            // close namespace
            sourceCode.Append("}");

            // write generated source to file if requested
            if(sourceOutputFilename != null)
            {
                StreamWriter writer = new StreamWriter(sourceOutputFilename);
                writer.Write(sourceCode.ToString());
                writer.Close();
            }

            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = GetDynCompilerSetup(modelAssemblyLocation, actionAssemblyLocation);

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

        /// <summary>
        /// Generate new actions for the given actions, doing the same work, 
        /// but hopefully faster by taking graph analysis information into account
        /// </summary>
        public LGSPAction[] GenerateActions(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, 
            params LGSPAction[] actions)
        {
            if(actions.Length == 0) throw new ArgumentException("No actions provided!");

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            GenerateFileHeaderForActionsFile(sourceCode, model.GetType().Namespace, actions[0].rulePattern.GetType().Namespace);

            // use domain of dictionary as set with rulepatterns of the subpatterns of the actions, get them from pattern graph
            Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPatterns 
                = new Dictionary<LGSPMatchingPattern, LGSPMatchingPattern>();
            foreach (LGSPAction action in actions)
            {
                foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpattern 
                    in action.rulePattern.patternGraph.usedSubpatterns)
                {
                    subpatternMatchingPatterns[usedSubpattern.Key] = usedSubpattern.Value;
                }
            }

            // generate code for subpatterns
            foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPattern in subpatternMatchingPatterns)
            {
                LGSPMatchingPattern smp = subpatternMatchingPattern.Key;

                GenerateScheduledSearchPlans(smp.patternGraph, graph, true, false);

                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(smp.patternGraph);

                GenerateActionAndMatcher(sourceCode, smp, false);
            }

            // generate code for actions
            foreach(LGSPAction action in actions)
            {
                GenerateScheduledSearchPlans(action.rulePattern.patternGraph, graph, false, false);

                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(action.rulePattern.patternGraph);

                GenerateActionAndMatcher(sourceCode, action.rulePattern, false);
            }

            // close namespace
            sourceCode.Append("}");

            if(DumpDynSourceCode)
            {
                using(StreamWriter writer = new StreamWriter("dynamic_" + actions[0].Name + ".cs"))
                    writer.Write(sourceCode.ToString());
            }

            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = GetDynCompilerSetup(modelAssemblyName, actionsAssemblyName);
 
            // compile generated code
            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                throw new ArgumentException("Internal error: Illegal dynamic C# source code produced: " + errorMsg);
            }

            // create action instances
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

        /// <summary>
        /// Generate a new action for the given action, doing the same work, 
        /// but hopefully faster by taking graph analysis information into account
        /// </summary>
        public LGSPAction GenerateAction(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, LGSPAction action)
        {
            return GenerateActions(graph, modelAssemblyName, actionsAssemblyName, action)[0];
        }
    }
}
