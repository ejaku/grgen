/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_MULTIDIMARRAY_WORKAROUND       // must be equally set to the same flag in lgspGraphStatistics.cs!
//#define RANDOM_LOOKUP_LIST_START      // currently broken
//#define DUMP_SCHEDULED_SEARCH_PLAN
//#define DUMP_SEARCHPROGRAMS
//#define VSTRUCT_VAL_FOR_EDGE_LOOKUP

using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.expression;
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
        /// If true, profiling information is to be collected, i.e. some statistics about search steps executed
        /// </summary>
        public bool Profile = false;

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
        /// </summary>
        /// <param name="graph">The graph which is to be transfered into a pattern</param>
        /// <returns></returns>
        public PatternGraph BuildPatternGraph(LGSPGraph graph)
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
                    n.Type.TypeID, n.Type, n.Type.PackagePrefixedName,
                    graph.Name+"_node_"+count, "node_"+count,
                    null, null,
                    1.0f, -1, false,
                    null, null, null, null, null,
                    null, false, null
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
                    e.Type.TypeID, e.Type, e.Type.PackagePrefixedName,
                    graph.Name+"_edge_"+count, "edge_"+count,
                    null, null,
                    1.0f, -1, false,
                    null, null, null, null, null,
                    null, false, null
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

            List<PatternCondition> pcs = new List<PatternCondition>();
            for(int i = 0; i < numNodes; ++i)
            {
                if(nodes[i].Type.NumAttributes > 0)
                    pcs.Add(new PatternCondition(new expression.AreAttributesEqual(correspondingNodes[i], nodes[i]),
                        new string[] { nodes[i].name }, new string[] { }, new string[] { }, new VarType[] { }));
            }
            for(int i = 0; i < numEdges; ++i)
            {
                if(edges[i].Type.NumAttributes > 0)
                    pcs.Add(new PatternCondition(new expression.AreAttributesEqual(correspondingEdges[i], edges[i]),
                        new string[] { }, new string[] { edges[i].name }, new string[] { }, new VarType[] { }));
            }
            PatternCondition[] patternConditions = pcs.ToArray();

            PatternGraph patternGraph = new PatternGraph(
                graph.Name, "",
                null, graph.Name,
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

            foreach(IEdge edge in graph.Edges)
            {
                int edgeIndex = Array.IndexOf<IEdge>(correspondingEdges, edge);
                int sourceIndex = Array.IndexOf<INode>(correspondingNodes, edge.Source);
                int targetIndex = Array.IndexOf<INode>(correspondingNodes, edge.Target);
                patternGraph.edgeToSourceNode.Add(edges[edgeIndex], nodes[sourceIndex]);
                patternGraph.edgeToTargetNode.Add(edges[edgeIndex], nodes[targetIndex]);
            }

            PatternGraphAnalyzer.PrepareInline(patternGraph);

            return patternGraph;
        }

        /// <summary>
        /// Generate plan graph for given pattern graph with costs from the analyzed host graph.
        /// Plan graph contains nodes representing the pattern elements (nodes and edges)
        /// and edges representing the matching operations to get the elements by.
        /// Edges in plan graph are given in the nodes by incoming list, as needed for MSA computation.
        /// </summary>
        public PlanGraph GeneratePlanGraph(LGSPGraphStatistics graphStatistics, PatternGraph patternGraph, 
            bool isNegativeOrIndependent, bool isSubpatternLike)
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

            PlanNode[] planNodes = new PlanNode[patternGraph.nodesPlusInlined.Length + patternGraph.edgesPlusInlined.Length];
            // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)
            List<PlanEdge> planEdges = new List<PlanEdge>(patternGraph.nodesPlusInlined.Length + 5 * patternGraph.edgesPlusInlined.Length);

            int nodesIndex = 0;
            float zeroCost = 1;

            PlanNode planRoot = new PlanNode("root");

            // create plan nodes and lookup plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; i++)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];

                PlanNode planNode;
                PlanEdge rootToNodePlanEdge;
                createPlanNodeAndLookupPlanEdge(node, i, 
                    graphStatistics, patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
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

            // create plan nodes and lookup plus incidence handling plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];

                bool isPreset;
                PlanNode planNode;
                PlanEdge rootToNodePlanEdge;
                createPlanNodeAndLookupPlanEdge(edge, i, 
                    graphStatistics, patternGraph, isNegativeOrIndependent, isSubpatternLike, planRoot, zeroCost,
                    out isPreset, out planNode, out rootToNodePlanEdge);

                planNodes[nodesIndex] = planNode;
                if(rootToNodePlanEdge != null)
                {
                    planEdges.Add(rootToNodePlanEdge);
                    planNode.IncomingEdges.Add(rootToNodePlanEdge);
                }

                createSourceTargetIncomingOutgoingPlanEdges(edge, planNode, planEdges,
                    graphStatistics, patternGraph, isPreset, zeroCost);

                edge.TempPlanMapping = planNode;
                ++nodesIndex;
            }

            ////////////////////////////////////////////////////////////////////////////
            // second run handling dependent storage and index picking (can't be done in first run due to dependencies between elements)

            // create map/pick/cast/assign plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];

                if(node.PointOfDefinition == patternGraph)
                    createPickMapCastAssignPlanEdges(node, planEdges, zeroCost);
            }

            // create map/pick/cast/assign plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];

                if(edge.PointOfDefinition == patternGraph)
                    createPickMapCastAssignPlanEdges(edge, planEdges, zeroCost);
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray());
        }

        private static void createPlanNodeAndLookupPlanEdge(PatternNode node, int i, 
            LGSPGraphStatistics graphStatistics, PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpatternLike, PlanNode planRoot, float zeroCost,
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
            else if(node.PointOfDefinition != patternGraph)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
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
                isPreset = false;
                searchOperationType = SearchOperationType.Lookup;
            }

            planNode = new PlanNode(node, i + 1, isPreset);
            rootToNodePlanEdge = null;
            if(searchOperationType != SearchOperationType.Void)
                rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNode, cost);
        }

        private static void createPlanNodeAndLookupPlanEdge(PatternEdge edge, int i, 
            LGSPGraphStatistics graphStatistics, PatternGraph patternGraph, bool isNegativeOrIndependent, bool isSubpatternLike, PlanNode planRoot, float zeroCost,
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
            else if(edge.PointOfDefinition != patternGraph)
            {
                cost = zeroCost;
                isPreset = true;
                searchOperationType = isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : SearchOperationType.SubPreset;
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
#if VSTRUCT_VAL_FOR_EDGE_LOOKUP
                    int sourceTypeID;
                    if(patternGraph.GetSourcePlusInlined(edge) != null) sourceTypeID = patternGraph.GetSourcePlusInlined(edge).TypeID;
                    else sourceTypeID = model.NodeModel.RootType.TypeID;
                    int targetTypeID;
                    if(patternGraph.GetTargetPlusInlined(edge) != null) targetTypeID = patternGraph.GetTargetPlusInlined(edge).TypeID;
                    else targetTypeID = model.NodeModel.RootType.TypeID;
#if MONO_MULTIDIMARRAY_WORKAROUND
                    cost = graph.vstructs[((sourceTypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                        + targetTypeID) * 2 + (int) LGSPDirection.Out];
#else
                    cost = graph.vstructs[sourceTypeID, edge.TypeID, targetTypeID, (int) LGSPDirection.Out];
#endif
#else
                cost = graphStatistics.edgeCounts[edge.TypeID];
#endif

                isPreset = false;
                searchOperationType = SearchOperationType.Lookup;
            }

            planNode = new PlanNode(edge, i + 1, isPreset,
                patternGraph.GetSourcePlusInlined(edge) != null ? patternGraph.GetSourcePlusInlined(edge).TempPlanMapping : null,
                patternGraph.GetTargetPlusInlined(edge) != null ? patternGraph.GetTargetPlusInlined(edge).TempPlanMapping : null);
            rootToNodePlanEdge = null;
            if(searchOperationType != SearchOperationType.Void)
                rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNode, cost);
        }

        private void createSourceTargetIncomingOutgoingPlanEdges(PatternEdge edge, PlanNode planNode, 
            List<PlanEdge> planEdges,
            LGSPGraphStatistics graphStatistics, PatternGraph patternGraph, bool isPreset, float zeroCost)
        {
            // only add implicit source operation if edge source is needed and the edge source is 
            // not a preset node and not a storage node and not an index node and not a cast node
            if(patternGraph.GetSourcePlusInlined(edge) != null
                && !patternGraph.GetSourcePlusInlined(edge).TempPlanMapping.IsPreset
                && patternGraph.GetSourcePlusInlined(edge).Storage == null
                && patternGraph.GetSourcePlusInlined(edge).IndexAccess == null
                && patternGraph.GetSourcePlusInlined(edge).NameLookup == null
                && patternGraph.GetSourcePlusInlined(edge).UniqueLookup == null
                && patternGraph.GetSourcePlusInlined(edge).ElementBeforeCasting == null)
            {
                SearchOperationType operation = edge.fixedDirection ?
                    SearchOperationType.ImplicitSource : SearchOperationType.Implicit;
                PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNode,
                    patternGraph.GetSourcePlusInlined(edge).TempPlanMapping, zeroCost);
                planEdges.Add(implSrcPlanEdge);
                patternGraph.GetSourcePlusInlined(edge).TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
            }
            // only add implicit target operation if edge target is needed and the edge target is
            // not a preset node and not a storage node and not an index node and not a cast node
            if(patternGraph.GetTargetPlusInlined(edge) != null
                && !patternGraph.GetTargetPlusInlined(edge).TempPlanMapping.IsPreset
                && patternGraph.GetTargetPlusInlined(edge).Storage == null
                && patternGraph.GetTargetPlusInlined(edge).IndexAccess == null
                && patternGraph.GetTargetPlusInlined(edge).NameLookup == null
                && patternGraph.GetTargetPlusInlined(edge).UniqueLookup == null
                && patternGraph.GetTargetPlusInlined(edge).ElementBeforeCasting == null)
            {
                SearchOperationType operation = edge.fixedDirection ?
                    SearchOperationType.ImplicitTarget : SearchOperationType.Implicit;
                PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNode,
                    patternGraph.GetTargetPlusInlined(edge).TempPlanMapping, zeroCost);
                planEdges.Add(implTgtPlanEdge);
                patternGraph.GetTargetPlusInlined(edge).TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
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
                if(patternGraph.GetSourcePlusInlined(edge) != null)
                {
                    int targetTypeID;
                    if(patternGraph.GetTargetPlusInlined(edge) != null) targetTypeID = patternGraph.GetTargetPlusInlined(edge).TypeID;
                    else targetTypeID = model.NodeModel.RootType.TypeID;
                    // cost of walking along edge
#if MONO_MULTIDIMARRAY_WORKAROUND
                    float normCost = graphStatistics.vstructs[((patternGraph.GetSourcePlusInlined(edge).TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                        + targetTypeID) * 2 + (int)LGSPDirection.Out];
                    if(!edge.fixedDirection)
                    {
                        normCost += graphStatistics.vstructs[((patternGraph.GetSourcePlusInlined(edge).TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                            + targetTypeID) * 2 + (int)LGSPDirection.In];
                    }
#else
                    float normCost = graph.statistics.vstructs[patternGraph.GetSourcePlusInlined(edge).TypeID, edge.TypeID, targetTypeID, (int) LGSPDirection.Out];
                    if (!edge.fixedDirection) {
                        normCost += graph.statistics.vstructs[patternGraph.GetSourcePlusInlined(edge).TypeID, edge.TypeID, targetTypeID, (int) LGSPDirection.In];
                    }
#endif
                    if(graphStatistics.nodeCounts[patternGraph.GetSourcePlusInlined(edge).TypeID] != 0)
                        normCost /= graphStatistics.nodeCounts[patternGraph.GetSourcePlusInlined(edge).TypeID];
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.Outgoing : SearchOperationType.Incident;
                    PlanEdge outPlanEdge = new PlanEdge(operation, patternGraph.GetSourcePlusInlined(edge).TempPlanMapping,
                        planNode, normCost);
                    planEdges.Add(outPlanEdge);
                    planNode.IncomingEdges.Add(outPlanEdge);
                }

                // no incoming on target node if no target
                if(patternGraph.GetTargetPlusInlined(edge) != null)
                {
                    int sourceTypeID;
                    if(patternGraph.GetSourcePlusInlined(edge) != null) sourceTypeID = patternGraph.GetSourcePlusInlined(edge).TypeID;
                    else sourceTypeID = model.NodeModel.RootType.TypeID;
                    // cost of walking in opposite direction of edge
#if MONO_MULTIDIMARRAY_WORKAROUND
                    float revCost = graphStatistics.vstructs[((patternGraph.GetTargetPlusInlined(edge).TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                        + sourceTypeID) * 2 + (int)LGSPDirection.In];
                    if(!edge.fixedDirection)
                    {
                        revCost += graphStatistics.vstructs[((patternGraph.GetTargetPlusInlined(edge).TypeID * graphStatistics.dim1size + edge.TypeID) * graphStatistics.dim2size
                            + sourceTypeID) * 2 + (int)LGSPDirection.Out];
                    }
#else
                    float revCost = graph.statistics.vstructs[patternGraph.GetTargetPlusInlined(edge).TypeID, edge.TypeID, sourceTypeID, (int) LGSPDirection.In];
                    if (!edge.fixedDirection) {
                        revCost += graph.statistics.vstructs[patternGraph.GetTargetPlusInlined(edge).TypeID, edge.TypeID, sourceTypeID, (int) LGSPDirection.Out];
                    }
#endif
                    if(graphStatistics.nodeCounts[patternGraph.GetTargetPlusInlined(edge).TypeID] != 0)
                        revCost /= graphStatistics.nodeCounts[patternGraph.GetTargetPlusInlined(edge).TypeID];
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.Incoming : SearchOperationType.Incident;
                    PlanEdge inPlanEdge = new PlanEdge(operation, patternGraph.GetTargetPlusInlined(edge).TempPlanMapping,
                        planNode, revCost);
                    planEdges.Add(inPlanEdge);
                    planNode.IncomingEdges.Add(inPlanEdge);
                }
            }
        }

        public static void createPickMapCastAssignPlanEdges(PatternNode node, List<PlanEdge> planEdges, float zeroCost)
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

        public static void createPickMapCastAssignPlanEdges(PatternEdge edge, List<PlanEdge> planEdges, float zeroCost)
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

        private void DumpScheduledSearchPlan(ScheduledSearchPlan ssp, String dumpname)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-scheduledsp.txt", false);
            SourceBuilder sb = new SourceBuilder();
            ssp.Explain(sb, model);
            sb.Append("\n");
            sw.WriteLine(sb.ToString());
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
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Target.IsPreset && edge.Type != SearchOperationType.DefToBeYieldedTo)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPresetElement in edge.Target.OutgoingEdges)
                        activeEdges.Add(edgeOutgoingFromPresetElement);

                    // note: here a normal preset is converted into a neg/idpt preset operation if in negative/independent pattern
                    SearchOperation newOp = new SearchOperation(
                        isNegativeOrIndependent ? SearchOperationType.NegIdptPreset : edge.Type,
                        edge.Target, spGraph.Root, 0);
                    operations.Add(newOp);
                }
            }

            // then schedule the initialization of all def to be yielded to elements and variables,
            // must come after the preset elements, as they may be used in the def initialization
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.DefToBeYieldedTo
                    && (!isNegativeOrIndependent || (edge.Target.PatternElement.pointOfDefinition == patternGraph && edge.Target.PatternElement.originalElement==null)))
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

            // then schedule all map with storage / pick from index / pick from storage / pick from name index elements not depending on other elements
            foreach(SearchPlanEdge edge in spGraph.Root.OutgoingEdges)
            {
                if(edge.Type == SearchOperationType.MapWithStorage)
                {
                    foreach(SearchPlanEdge edgeOutgoingFromPickedElement in edge.Target.OutgoingEdges)
                        activeEdges.Add(edgeOutgoingFromPickedElement);

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
                        activeEdges.Add(edgeOutgoingFromPickedElement);

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
                        activeEdges.Add(edgeOutgoingFromPickedElement);

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
                        activeEdges.Add(edgeOutgoingFromPickedElement);

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
                        activeEdges.Add(edgeOutgoingFromPickedElement);

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
                    op.CostToEnd += minEdge.Cost;

                operations.Add(newOp);
            }

            // insert inlined element identity check into the schedule in case neither of the possible assignments was scheduled
            InsertInlinedElementIdentityCheckIntoSchedule(patternGraph, operations);

            // insert inlined variable assignments into the schedule
            InsertInlinedVariableAssignmentsIntoSchedule(patternGraph, operations);

            // insert conditions into the schedule
            InsertConditionsIntoSchedule(patternGraph.ConditionsPlusInlined, operations);

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            return new ScheduledSearchPlan(patternGraph, operations.ToArray(), cost);
        }

        private void InsertInlinedElementIdentityCheckIntoSchedule(PatternGraph patternGraph, List<SearchOperation> operations)
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
                if (ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.AssignVar
                    || ssp.Operations[i].Type == SearchOperationType.DefToBeYieldedTo)
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
                for (int i = 0; i < ssp.PatternGraph.nodesPlusInlined.Length; ++i)
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
                for (int i = 0; i < ssp.PatternGraph.edgesPlusInlined.Length; ++i)
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
                hom = ssp.PatternGraph.homomorphicNodes;
            }
            else { // (spn_j.NodeType == PlanNodeType.Edge)
                types = model.EdgeModel.Types;
                hom = ssp.PatternGraph.homomorphicEdges;
            }

            // order operation to check against all elements it's not allowed to be homomorph to

            // iterate through the operations before our position
            bool homomorphyPossibleAndAllowed = false;
            for (int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if (ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern
                    || ssp.Operations[i].Type == SearchOperationType.IndependentPattern
                    || ssp.Operations[i].Type == SearchOperationType.Assign
                    || ssp.Operations[i].Type == SearchOperationType.AssignVar
                    || ssp.Operations[i].Type == SearchOperationType.DefToBeYieldedTo)
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
                homGlobal = ssp.PatternGraph.homomorphicNodesGlobal;
            }
            else { // (spn_j.NodeType == PlanNodeType.Edge)
                homGlobal = ssp.PatternGraph.homomorphicEdgesGlobal;
            }

            // iterate through the operations before our position
            for (int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if (ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern
                    || ssp.Operations[i].Type == SearchOperationType.IndependentPattern
                    || ssp.Operations[i].Type == SearchOperationType.Assign 
                    || ssp.Operations[i].Type == SearchOperationType.AssignVar
                    || ssp.Operations[i].Type == SearchOperationType.DefToBeYieldedTo)
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
            foreach (PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(neg);
            }

            foreach (PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(idpt);
            }

            foreach (Alternative alt in patternGraph.alternativesPlusInlined)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(altCase);
                }
            }

            foreach (Iterated iter in patternGraph.iteratedsPlusInlined)
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
            patternGraph.schedulesIncludingNegativesAndIndependents[index] = null; // an explain might have filled this

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
            for (int i = 0; i < patternGraph.negativePatternGraphsPlusInlined.Length; ++i)
            {
                ScheduledSearchPlan negSchedule = patternGraph.negativePatternGraphsPlusInlined[i].schedulesIncludingNegativesAndIndependents[0];
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                // find best place in scheduled search plan for current negative pattern 
                // during search from end of schedule forward until the first element the negative pattern is dependent on is found
                for (int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if (op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern
                        || op.Type == SearchOperationType.AssignVar)
                    {
                        continue;
                    }

                    if (LazyNegativeIndependentConditionEvaluation)
                    {
                        break;
                    }

                    if (op.Type == SearchOperationType.LockLocalElementsForPatternpath
                        || op.Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        break; // LockLocalElementsForPatternpath and DefToBeYieldedTo are barriers for neg/idpt
                    }

                    if (patternGraph.negativePatternGraphsPlusInlined[i].neededNodes.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name)
                        || patternGraph.negativePatternGraphsPlusInlined[i].neededEdges.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
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
            for (int i = 0; i < patternGraph.independentPatternGraphsPlusInlined.Length; ++i)
            {
                ScheduledSearchPlan idptSchedule = patternGraph.independentPatternGraphsPlusInlined[i].schedulesIncludingNegativesAndIndependents[0];
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                // find best place in scheduled search plan for current independent pattern 
                // during search from end of schedule forward until the first element the independent pattern is dependent on is found
                for (int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if (op.Type == SearchOperationType.Condition
                        || op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.IndependentPattern
                        || op.Type == SearchOperationType.AssignVar)
                    {
                        continue;
                    }

                    if (LazyNegativeIndependentConditionEvaluation)
                    {
                        break;
                    }

                    if (op.Type == SearchOperationType.LockLocalElementsForPatternpath
                        || op.Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        break; // LockLocalElementsForPatternpath and DefToBeYieldedTo are barriers for neg/idpt
                    }

                    if (patternGraph.independentPatternGraphsPlusInlined[i].neededNodes.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name)
                        || patternGraph.independentPatternGraphsPlusInlined[i].neededEdges.ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
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
                foreach(String neededVariable in conditions[i].NeededVariables)
                    neededElements[i][neededVariable] = true;
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
                        || op.Type == SearchOperationType.IndependentPattern
                        || op.Type == SearchOperationType.DefToBeYieldedTo)
                    {
                        continue;
                    }

                    if (LazyNegativeIndependentConditionEvaluation)
                    {
                        break;
                    }

                    if (op.Type == SearchOperationType.AssignVar)
                    {
                        if(neededElements[i].ContainsKey(((PatternVariable)op.Element).Name))
                        {
                            costToEnd = op.CostToEnd;
                            break;
                        }
                        continue;
                    }

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
        /// Inserts inlined variable assignments into the schedule given by the operations list at their earliest possible position
        /// </summary>
        public void InsertInlinedVariableAssignmentsIntoSchedule(PatternGraph patternGraph, List<SearchOperation> operations)
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
            Dictionary<String, bool>[] neededElements = new Dictionary<String, bool>[numInlinedParameterVariables];
            PatternVariable[] inlinedParameterVariables = new PatternVariable[numInlinedParameterVariables];
            int curInlParamVar = 0;
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(var.AssignmentSource == null)
                    continue;
                if(!patternGraph.WasInlinedHere(var.originalSubpatternEmbedding))
                    continue;

                neededElements[curInlParamVar] = new Dictionary<string, bool>();
                foreach(String neededNode in var.AssignmentDependencies.neededNodes)
                    neededElements[curInlParamVar][neededNode] = true;
                foreach(String neededEdge in var.AssignmentDependencies.neededEdges)
                    neededElements[curInlParamVar][neededEdge] = true;
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

        /// <summary>
        /// Parallelize the scheduled search plan if it is to be parallelized.
        /// An action to be parallelized is split at the first loop into a header part and a body part,
        /// all subpatterns and nested patterns to be parallelized are switched to non- is-matched-flag-based isomorphy checking.
        /// </summary>
        public void ParallelizeAsNeeded(LGSPMatchingPattern matchingPattern)
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
                    Console.Error.WriteLine("Warning: " + matchingPattern.patternGraph.Name + " not parallelized as no parallelizable loop was found.");
                }
            }
            else
            {
                Parallelize(matchingPattern);
            }
        }

        /// <summary>
        /// Parallelize the scheduled search plan to the branching factor,
        /// splitting it at the first loop into a header part and a body part
        /// </summary>
        public void ParallelizeHeadBody(LGSPRulePattern rulePattern)
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
                    SearchOperation cloneHead = (SearchOperation)so.Clone();
                    headOperations.Add(cloneHead);
                    SearchOperation cloneBody = (SearchOperation)so.Clone();
                    cloneBody.Isomorphy.Parallel = true;
                    bodyOperations.Add(cloneBody);
                    switch(so.Type)
                    {
                        case SearchOperationType.Lookup:
                            cloneHead.Type = SearchOperationType.SetupParallelLookup;
                            cloneBody.Type = SearchOperationType.ParallelLookup;
                            break;
                        case SearchOperationType.Incident:
                            cloneHead.Type = SearchOperationType.SetupParallelIncident;
                            cloneBody.Type = SearchOperationType.ParallelIncident;
                            break;
                        case SearchOperationType.Incoming:
                            cloneHead.Type = SearchOperationType.SetupParallelIncoming;
                            cloneBody.Type = SearchOperationType.ParallelIncoming;
                            break;
                        case SearchOperationType.Outgoing:
                            cloneHead.Type = SearchOperationType.SetupParallelOutgoing;
                            cloneBody.Type = SearchOperationType.ParallelOutgoing;
                            break;
                        case SearchOperationType.PickFromStorage:
                            cloneHead.Type = SearchOperationType.SetupParallelPickFromStorage;
                            cloneBody.Type = SearchOperationType.ParallelPickFromStorage;
                            break;
                        case SearchOperationType.PickFromStorageDependent:
                            cloneHead.Type = SearchOperationType.SetupParallelPickFromStorageDependent;
                            cloneBody.Type = SearchOperationType.ParallelPickFromStorageDependent;
                            break;
                        case SearchOperationType.PickFromIndex:
                            cloneHead.Type = SearchOperationType.SetupParallelPickFromIndex;
                            cloneBody.Type = SearchOperationType.ParallelPickFromIndex;
                            break;
                        case SearchOperationType.PickFromIndexDependent:
                            cloneHead.Type = SearchOperationType.SetupParallelPickFromIndexDependent;
                            cloneBody.Type = SearchOperationType.ParallelPickFromIndexDependent;
                            break;
                    }
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
        public void Parallelize(LGSPMatchingPattern matchingPattern)
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
        public void ParallelizeAlternativeIterated(PatternGraph patternGraph)
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
        public void ParallelizeNegativeIndependent(ScheduledSearchPlan ssp)
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
                    Debug.Assert(nestedSsp.PatternGraph.parallelizedSchedule == null);
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

        public void ParallelizeYielding(PatternGraph patternGraph)
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

        public void SetNeedForParallelizedVersion(ExpressionOrYielding expyield)
        {
            expyield.SetNeedForParallelizedVersion(true);
            foreach(ExpressionOrYielding child in expyield)
            {
                SetNeedForParallelizedVersion(child);
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

            // generate the parallelized search program out of the parallelized schedule(s) within the pattern graph of the rule
            SearchProgram searchProgramParallelized = GenerateParallelizedSearchProgramAsNeeded(matchingPattern);

            // emit matcher class head, body, tail; body is source code representing search program
            if(matchingPattern is LGSPRulePattern) {
                GenerateActionInterface(sb, (LGSPRulePattern)matchingPattern); // generate the exact action interface
                GenerateMatcherClassHeadAction(sb, (LGSPRulePattern)matchingPattern, isInitialStatic, searchProgram);
                searchProgram.Emit(sb);
                if(searchProgramParallelized != null)
                    searchProgramParallelized.Emit(sb);
                GenerateActionImplementation(sb, (LGSPRulePattern)matchingPattern);
                GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);
            } else {
                GenerateMatcherClassHeadSubpattern(sb, matchingPattern, isInitialStatic);
                searchProgram.Emit(sb);
                if(searchProgramParallelized != null)
                    searchProgramParallelized.Emit(sb);
                GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);
            }

            // finally generate matcher source for all the nested alternatives or iterateds of the pattern graph
            // nested inside the alternatives,iterateds,negatives,independents
            foreach(Alternative alt in matchingPattern.patternGraph.alternativesPlusInlined)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach (Iterated iter in matchingPattern.patternGraph.iteratedsPlusInlined)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
            }
            foreach (PatternGraph neg in matchingPattern.patternGraph.negativePatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
            }
            foreach (PatternGraph idpt in matchingPattern.patternGraph.independentPatternGraphsPlusInlined)
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

            // generate the parallelized search program out of the parallelized schedules within the pattern graphs of the alternative cases
            SearchProgram searchProgramParallelized = GenerateParallelizedSearchProgramAlternativeAsNeeded(matchingPattern, alt);

            // emit matcher class head, body, tail; body is source code representing search program
            GenerateMatcherClassHeadAlternative(sb, matchingPattern, alt, isInitialStatic);
            searchProgram.Emit(sb);
            if(searchProgramParallelized != null)
                searchProgramParallelized.Emit(sb);
            GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);

            // handle alternatives or iterateds nested in the alternative cases
            foreach (PatternGraph altCase in alt.alternativeCases)
            {
                // nested inside the alternatives,iterateds,negatives,independents
                foreach (Alternative nestedAlt in altCase.alternativesPlusInlined)
                {
                    GenerateActionAndMatcherOfAlternative(sb, matchingPattern, nestedAlt, isInitialStatic);
                }
                foreach (Iterated iter in altCase.iteratedsPlusInlined)
                {
                    GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
                }
                foreach (PatternGraph neg in altCase.negativePatternGraphsPlusInlined)
                {
                    GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
                }
                foreach (PatternGraph idpt in altCase.independentPatternGraphsPlusInlined)
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

            // generate the parallelized search program out of the parallelized schedule within the pattern graph of the iterated pattern
            SearchProgram searchProgramParallelized = GenerateParallelizedSearchProgramIteratedAsNeeded(matchingPattern, iter);

            // emit matcher class head, body, tail; body is source code representing search program
            GenerateMatcherClassHeadIterated(sb, matchingPattern, iter, isInitialStatic);
            searchProgram.Emit(sb);
            if(searchProgramParallelized != null)
                searchProgramParallelized.Emit(sb);
            GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);

            // finally generate matcher source for all the nested alternatives or iterateds of the iterated pattern graph
            // nested inside the alternatives,iterateds,negatives,independents
            foreach (Alternative alt in iter.alternativesPlusInlined)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach (Iterated nestedIter in iter.iteratedsPlusInlined)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, nestedIter.iteratedPattern, isInitialStatic);
            }
            foreach (PatternGraph neg in iter.negativePatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
            }
            foreach (PatternGraph idpt in iter.independentPatternGraphsPlusInlined)
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
            foreach (Alternative alt in negOrIdpt.alternativesPlusInlined)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach (Iterated iter in negOrIdpt.iteratedsPlusInlined)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
            }
            foreach (PatternGraph nestedNeg in negOrIdpt.negativePatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, nestedNeg, isInitialStatic);
            }
            foreach (PatternGraph nestedIdpt in negOrIdpt.independentPatternGraphsPlusInlined)
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

            if(matchingPattern.PatternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", matchingPattern.PatternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

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
            
            if(matchingPattern.PatternGraph.Package != null)
            {
                sb.Unindent();
                sb.AppendFront("}\n");
            }

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
                refLocals += TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_" + i + " = " + TypesHelper.DefaultValueString(matchingPattern.Outputs[i].PackagePrefixedName, model) + "; ";
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
                sb.AppendFrontFormat("output_{0} = {1};\n", i, TypesHelper.DefaultValueString(matchingPattern.Outputs[i].PackagePrefixedName, model));
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

            sb.AppendFront("void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCall filter)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("if(filter.IsAutoSupplied) {\n");
            sb.Indent();
            sb.AppendFront("switch(filter.Name) {\n");
            sb.Indent();
            sb.AppendFront("case \"keepFirst\": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;\n");
            sb.AppendFront("case \"keepLast\": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;\n");
            sb.AppendFront("case \"keepFirstFraction\": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;\n");
            sb.AppendFront("case \"keepLastFraction\": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;\n");
            sb.AppendFront("default: throw new Exception(\"Unknown auto supplied filter name!\");\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return;\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("switch(filter.FullName) {\n");
            sb.Indent();
            foreach(IFilter filter in matchingPattern.Filters)
            {
                if(filter is IFilterAutoGenerated)
                {
                    if(((IFilterAutoGenerated)filter).Entity != null)
                    {
                        sb.AppendFrontFormat("case \"{1}<{2}>\": GRGEN_ACTIONS.{4}MatchFilters.Filter_{0}_{1}_{2}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({3})matches); break;\n",
                            patternName, filter.Name, ((IFilterAutoGenerated)filter).Entity, matchesType, TypesHelper.GetPackagePrefixDot(filter.Package));
                        if(filter.Package != null)
                            sb.AppendFrontFormat("case \"{5}{1}<{2}>\": GRGEN_ACTIONS.{4}MatchFilters.Filter_{0}_{1}_{2}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({3})matches); break;\n",
                                patternName, filter.Name, ((IFilterAutoGenerated)filter).Entity, matchesType, filter.Package + ".", filter.Package + "::");
                    }
                    else // auto
                    {
                        sb.AppendFrontFormat("case \"{1}\": GRGEN_ACTIONS.{3}MatchFilters.Filter_{0}_{1}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({2})matches); break;\n",
                            patternName, filter.Name, matchesType, TypesHelper.GetPackagePrefixDot(filter.Package));
                        if(filter.Package != null)
                            sb.AppendFrontFormat("case \"{4}{1}\": GRGEN_ACTIONS.{3}MatchFilters.Filter_{0}_{1}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({2})matches); break;\n",
                                patternName, filter.Name, matchesType, filter.Package + ".", filter.Package + "::");
                    }
                }
                else
                {
                    IFilterFunction filterFunction = (IFilterFunction)filter;
                    sb.AppendFrontFormat("case \"{0}\": GRGEN_ACTIONS.{1}MatchFilters.Filter_{2}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({3})matches",
                        filterFunction.Name, TypesHelper.GetPackagePrefixDot(filterFunction.Package), filterFunction.Name, matchesType);
                    for(int i=0; i<filterFunction.Inputs.Length; ++i)
                    {
                        sb.AppendFormat(", ({0})(filter.ArgumentExpressions[{1}]!=null ? filter.ArgumentExpressions[{1}].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[{1}])", 
                            TypesHelper.TypeName(filterFunction.Inputs[i]), i);
                    }
                    sb.Append("); break;\n");
                    if(filter.Package != null)
                    {
                        sb.AppendFrontFormat("case \"{4}{0}\": GRGEN_ACTIONS.{1}MatchFilters.Filter_{2}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({3})matches",
                            filterFunction.Name, TypesHelper.GetPackagePrefixDot(filterFunction.Package), filterFunction.Name, matchesType, filter.Package + "::");
                        for(int i = 0; i < filterFunction.Inputs.Length; ++i)
                        {
                            sb.AppendFormat(", ({0})(filter.ArgumentExpressions[{1}]!=null ? filter.ArgumentExpressions[{1}].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[{1}])",
                                TypesHelper.TypeName(filterFunction.Inputs[i]), i);
                        }
                        sb.Append("); break;\n");
                    }
                }
            }
            sb.AppendFront("default: throw new Exception(\"Unknown filter name!\");\n");
            sb.Unindent();
            sb.AppendFront("}\n");
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
                        (LGSPRulePattern)matchingPattern, i, null, false, Profile);
                    if(i==0) searchProgramRoot = searchProgramListEnd = sp;
                    else searchProgramListEnd = (SearchProgram)searchProgramListEnd.Append(sp);
                }
                else
                {
                    Debug.Assert(searchProgramRoot==null);
                    searchProgramRoot = searchProgramListEnd = searchProgramBuilder.BuildSearchProgram(
                        model, matchingPattern, false, Profile);
                }
            }

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgramRoot.Dump(builder);
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
        /// Generates the parallelized search program(s) for the pattern graph of the given rule
        /// </summary>
        SearchProgram GenerateParallelizedSearchProgramAsNeeded(LGSPMatchingPattern matchingPattern)
        {
            PatternGraph patternGraph = matchingPattern.patternGraph;
            if(patternGraph.parallelizedSchedule == null)
                return null;

            SearchProgram searchProgramRoot = null;
            SearchProgram searchProgramListEnd = null;

            for(int i = 0; i < patternGraph.parallelizedSchedule.Length; ++i) // 2 for actions, 1 for subpatterns
            {
                ScheduledSearchPlan scheduledSearchPlan = patternGraph.parallelizedSchedule[i];

#if DUMP_SCHEDULED_SEARCH_PLAN
                StreamWriter sspwriter = new StreamWriter(matchingPattern.name + (i==1 ? "_parallelized_body" : "_parallelized") + "_ssp_dump.txt");
                float prevCostToEnd = scheduledSearchPlan.Operations.Length > 0 ? scheduledSearchPlan.Operations[0].CostToEnd : 0f;
                foreach(SearchOperation so in scheduledSearchPlan.Operations)
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
                    SearchProgram sp = searchProgramBuilder.BuildSearchProgram(model, (LGSPRulePattern)matchingPattern, i, null, true, Profile);
                    if(i == 0) searchProgramRoot = searchProgramListEnd = sp;
                    else searchProgramListEnd = (SearchProgram)searchProgramListEnd.Append(sp);
                }
                else
                {
                    Debug.Assert(searchProgramRoot == null);
                    searchProgramRoot = searchProgramListEnd = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, true, Profile);
                }
            }

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgramRoot.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + (i==1 ? "_parallelized_body" : "_parallelized") + "_" + searchProgramRoot.Name + "_built_dump.txt");
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
            writer = new StreamWriter(matchingPattern.name + (i==1 ? "_parallelized_body" : "_parallelized") + "_" + searchProgramRoot.Name + "_completed_dump.txt");
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
            // build pass: build nested program from scheduled search plans of the alternative cases
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, alt, false, Profile);

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
        /// Generates the parallelized search program for the given alternative 
        /// </summary>
        SearchProgram GenerateParallelizedSearchProgramAlternativeAsNeeded(LGSPMatchingPattern matchingPattern, Alternative alt)
        {
            if(matchingPattern.patternGraph.parallelizedSchedule == null)
                return null;

            // build pass: build nested program from scheduled search plans of the alternative cases
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, alt, true, Profile);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_parallelized_" + alt.name + "_" + searchProgram.Name + "_built_dump.txt");
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
            writer = new StreamWriter(matchingPattern.name + "_parallelized_" + alt.name + "_" + searchProgram.Name + "_completed_dump.txt");
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
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, iter, false, Profile);

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
        /// Generates the parallelized search program for the given iterated pattern
        /// </summary>
        SearchProgram GenerateParallelizedSearchProgramIteratedAsNeeded(LGSPMatchingPattern matchingPattern, PatternGraph iter)
        {
            if(matchingPattern.patternGraph.parallelizedSchedule == null)
                return null;

            // build pass: build nested program from scheduled search plan of the all pattern
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, iter, true, Profile);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_parallelized_" + iter.name + "_" + searchProgram.Name + "_built_dump.txt");
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
            writer = new StreamWriter(matchingPattern.name + "_parallelized_" + iter.name + "_" + searchProgram.Name + "_completed_dump.txt");
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
                + "using System.Threading;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_EXPR = de.unika.ipd.grGen.expression;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
                + "using GRGEN_MODEL = " + namespaceOfModel + ";\n"
                + "using GRGEN_ACTIONS = " + namespaceOfRulePatterns + ";\n"
                + "using " + namespaceOfRulePatterns + ";\n\n");
            sb.AppendFront("namespace de.unika.ipd.grGen.lgspActions\n");
            sb.AppendFront("{\n");
            sb.Indent(); // namespace level
        }

        /// <summary>
        /// Generates matcher class head source code for the pattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        void GenerateMatcherClassHeadAction(SourceBuilder sb, LGSPRulePattern rulePattern, 
            bool isInitialStatic, SearchProgram searchProgram)
        {
            PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;
                
            String namePrefix = (isInitialStatic ? "" : "Dyn") + "Action_";
            String className = namePrefix + rulePattern.name;
            String rulePatternClassName = rulePattern.GetType().Name;
            String matchClassName = rulePatternClassName + "." + "Match_" + rulePattern.name;
            String matchInterfaceName = rulePatternClassName + "." + "IMatch_" + rulePattern.name;
            String actionInterfaceName = "IAction_" + rulePattern.name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPAction, "
                + "GRGEN_LIBGR.IAction, " + actionInterfaceName + "\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("public " + className + "() {\n");
            sb.Indent(); // method body level
            sb.AppendFront("_rulePattern = " + rulePatternClassName + ".Instance;\n");
            sb.AppendFront("patternGraph = _rulePattern.patternGraph;\n");
            if(rulePattern.patternGraph.branchingFactor < 2)
            {
                sb.AppendFront("DynamicMatch = myMatch;\n");
                if(!isInitialStatic)
                    sb.AppendFrontFormat("GRGEN_ACTIONS.Action_{0}.Instance.DynamicMatch = myMatch;\n", rulePattern.name);
            }
            else
            {
                sb.AppendFront("if(Environment.ProcessorCount == 1)\n");
                sb.AppendFront("{\n");
                sb.AppendFront("\tDynamicMatch = myMatch;\n");
                if(!isInitialStatic)
                    sb.AppendFrontFormat("\tGRGEN_ACTIONS.Action_{0}.Instance.DynamicMatch = myMatch;\n", rulePattern.name);
                sb.AppendFront("}\n");
                sb.AppendFront("else\n");
                sb.AppendFront("{\n");
                sb.Indent();
                sb.AppendFront("DynamicMatch = myMatch_parallelized;\n");
                if(!isInitialStatic)
                    sb.AppendFrontFormat("GRGEN_ACTIONS.Action_{0}.Instance.DynamicMatch = myMatch_parallelized;\n", rulePattern.name);
                sb.AppendFrontFormat("numWorkerThreads = GRGEN_LGSP.WorkerPool.EnsurePoolSize({0});\n", rulePattern.patternGraph.branchingFactor);
                sb.AppendFrontFormat("parallelTaskMatches = new GRGEN_LGSP.LGSPMatchesList<{0}, {1}>[numWorkerThreads];\n", matchClassName, matchInterfaceName);
                sb.AppendFront("moveHeadAfterNodes = new List<GRGEN_LGSP.LGSPNode>[numWorkerThreads];\n");
                sb.AppendFront("moveHeadAfterEdges = new List<GRGEN_LGSP.LGSPEdge>[numWorkerThreads];\n");
                sb.AppendFront("moveOutHeadAfter = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[numWorkerThreads];\n");
                sb.AppendFront("moveInHeadAfter = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[numWorkerThreads];\n");
                sb.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                sb.AppendFront("{\n");
                sb.Indent();
                sb.AppendFront("moveHeadAfterNodes[i] = new List<GRGEN_LGSP.LGSPNode>();\n");
                sb.AppendFront("moveHeadAfterEdges[i] = new List<GRGEN_LGSP.LGSPEdge>();\n");
                sb.AppendFront("moveOutHeadAfter[i] = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>();\n");
                sb.AppendFront("moveInHeadAfter[i] = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>();\n");
                sb.Unindent();
                sb.AppendFront("}\n");

                sb.AppendFront("for(int i=0; i<parallelTaskMatches.Length; ++i)\n");
                sb.AppendFrontFormat("\tparallelTaskMatches[i] = new GRGEN_LGSP.LGSPMatchesList<{0}, {1}>(this);\n", matchClassName, matchInterfaceName);
                sb.Unindent();
                sb.AppendFront("}\n");
            }
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
                sb.AppendFront("public static " + className + " Instance { get { return instance; } set { instance = value; } }\n");
                sb.AppendFront("private static " + className + " instance = new " + className + "();\n");
            }

            GenerateIndependentsMatchObjects(sb, rulePattern, patternGraph);

            sb.AppendFront("\n");

            GenerateParallelizationSetupAsNeeded(sb, rulePattern, searchProgram);
        }

        void GenerateParallelizationSetupAsNeeded(SourceBuilder sb, LGSPRulePattern rulePattern, SearchProgram searchProgram)
        {
            if(rulePattern.patternGraph.branchingFactor < 2)
                return;

            foreach(SearchOperation so in rulePattern.patternGraph.parallelizedSchedule[0].Operations)
            {
                switch(so.Type)
                {
                    case SearchOperationType.WriteParallelPreset:
                        if(so.Element is SearchPlanNodeNode)
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0};\n", NamesOfEntities.IterationParallelizationParallelPresetCandidate(((SearchPlanNodeNode)so.Element).PatternElement.Name));
                        else //SearchPlanEdgeNode
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationParallelPresetCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.WriteParallelPresetVar:
                        sb.AppendFrontFormat("{0} {1};\n",
                            TypesHelper.TypeName(((PatternVariable)so.Element).Type),
                            NamesOfEntities.IterationParallelizationParallelPresetCandidate(((PatternVariable)so.Element).Name));
                        break;
                    case SearchOperationType.SetupParallelLookup:
                        if(so.Element is SearchPlanNodeNode)
                        {
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanNodeNode)so.Element).PatternElement.Name));
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanNodeNode)so.Element).PatternElement.Name));
                        }
                        else
                        {
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        }
                        break;
                    case SearchOperationType.SetupParallelPickFromStorage:
                        if(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.type).StartsWith("set") || TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.type).StartsWith("map"))
                        {
                            sb.AppendFrontFormat("IEnumerator<KeyValuePair<{0},{1}>> {2};\n",
                                TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.Type)), model),
                                TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.Type)), model),
                                NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        else
                        {
                            sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                                TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.Type)), model),
                                NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        break;
                    case SearchOperationType.SetupParallelPickFromStorageDependent:
                        if(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute).StartsWith("set") || TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute).StartsWith("map"))
                        {
                            sb.AppendFrontFormat("IEnumerator<KeyValuePair<{0},{1}>> {2};\n",
                               TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute)), model),
                               TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute)), model),
                               NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        else
                        {
                            sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                               TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute)), model),
                               NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        break;
                    case SearchOperationType.SetupParallelPickFromIndex:
                        sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                           TypesHelper.TypeName(so.IndexAccess.Index is AttributeIndexDescription ?
                               ((AttributeIndexDescription)so.IndexAccess.Index).GraphElementType :
                               ((IncidenceIndexDescription)so.IndexAccess.Index).StartNodeType),
                           NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.SetupParallelPickFromIndexDependent:
                        sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                           TypesHelper.TypeName(so.IndexAccess.Index is AttributeIndexDescription ?
                               ((AttributeIndexDescription)so.IndexAccess.Index).GraphElementType :
                               ((IncidenceIndexDescription)so.IndexAccess.Index).StartNodeType),
                           NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.SetupParallelIncoming:
                    case SearchOperationType.SetupParallelOutgoing:
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.SetupParallelIncident:
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        sb.AppendFrontFormat("int {0};\n", NamesOfEntities.IterationParallelizationDirectionRunCounterVariable(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        break;
                }
            }
            sb.AppendFront("\n");

            String rulePatternClassName = rulePattern.GetType().Name;
            String matchClassName = rulePatternClassName + "." + "Match_" + rulePattern.name;
            String matchInterfaceName = rulePatternClassName + "." + "IMatch_" + rulePattern.name;
            sb.AppendFront("private static GRGEN_LGSP.LGSPMatchesList<" + matchClassName + ", " + matchInterfaceName + ">[] parallelTaskMatches;\n");
            sb.AppendFront("private static int numWorkerThreads;\n");
            sb.AppendFront("private static int iterationNumber;\n");
            sb.AppendFront("private static int iterationLock;\n");
            sb.AppendFront("[ThreadStatic] private static int currentIterationNumber;\n");
            sb.AppendFront("[ThreadStatic] private static int threadId;\n");
            sb.AppendFront("private static GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnvParallel;\n");
            sb.AppendFront("private static int maxMatchesParallel;\n");
            sb.AppendFront("private static bool maxMatchesFound = false;\n");

            sb.AppendFront("private static List<GRGEN_LGSP.LGSPNode>[] moveHeadAfterNodes;\n");
            sb.AppendFront("private static List<GRGEN_LGSP.LGSPEdge>[] moveHeadAfterEdges;\n");
            sb.AppendFront("private static List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[] moveOutHeadAfter;\n");
            sb.AppendFront("private static List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[] moveInHeadAfter;\n");
            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the subpattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadSubpattern(SourceBuilder sb, LGSPMatchingPattern matchingPattern,
            bool isInitialStatic)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "PatternAction_";
            String className = namePrefix + matchingPattern.name;
            String matchingPatternClassName = matchingPattern.GetType().Name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            sb.AppendFront("patternGraph = " + matchingPatternClassName + ".Instance.patternGraph;\n");
           
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            GenerateTasksMemoryPool(sb, className, false, false, matchingPattern.patternGraph.branchingFactor);

            for (int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];
                if (node.PointOfDefinition == null)
                {
                    sb.AppendFront("public GRGEN_LGSP.LGSPNode " + node.name + ";\n");
                }
            }
            for (int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];
                if (edge.PointOfDefinition == null)
                {
                    sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + edge.name + ";\n");
                }
            }
            for (int i = 0; i < patternGraph.variablesPlusInlined.Length; ++i)
            {
                PatternVariable variable = patternGraph.variablesPlusInlined[i];
                sb.AppendFront("public " +TypesHelper.TypeName(variable.type) + " " + variable.name + ";\n");
            }

            GenerateIndependentsMatchObjects(sb, matchingPattern, patternGraph);

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the given alternative into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadAlternative(SourceBuilder sb, LGSPMatchingPattern matchingPattern, 
            Alternative alternative, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "AlternativeAction_";
            String className = namePrefix + alternative.pathPrefix+alternative.name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

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

            GenerateTasksMemoryPool(sb, className, true, false, matchingPattern.patternGraph.branchingFactor);

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
                GenerateIndependentsMatchObjects(sb, matchingPattern, altCase);
            }

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the given iterated pattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadIterated(SourceBuilder sb, LGSPMatchingPattern matchingPattern,
            PatternGraph iter, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "IteratedAction_";
            String className = namePrefix + iter.pathPrefix + iter.name;
            String matchingPatternClassName = matchingPattern.GetType().Name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            sb.AppendFront("patternGraph = " + matchingPatternClassName + ".Instance.patternGraph;\n");
            int index = -1;
            for (int i=0; i<iter.embeddingGraph.iteratedsPlusInlined.Length; ++i) {
                if (iter.embeddingGraph.iteratedsPlusInlined[i].iteratedPattern == iter) index = i;
            }
            sb.AppendFrontFormat("minMatchesIter = {0};\n", iter.embeddingGraph.iteratedsPlusInlined[index].minMatches);
            sb.AppendFrontFormat("maxMatchesIter = {0};\n", iter.embeddingGraph.iteratedsPlusInlined[index].maxMatches);
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

            GenerateTasksMemoryPool(sb, className, false, iter.isIterationBreaking, matchingPattern.patternGraph.branchingFactor);

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

            GenerateIndependentsMatchObjects(sb, matchingPattern, iter);

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates match objects of independents (one pre-allocated is part of action class)
        /// </summary>
        private void GenerateIndependentsMatchObjects(SourceBuilder sb,
            LGSPMatchingPattern matchingPatternClass, PatternGraph patternGraph)
        {
            if (patternGraph.nestedIndependents != null)
            {
                foreach (KeyValuePair<PatternGraph, PatternGraph> nestedIndependent in patternGraph.nestedIndependents)
                {
                    if(nestedIndependent.Key.originalPatternGraph != null)
                    {
                        sb.AppendFrontFormat("private {0} {1} = new {0}();",
                            nestedIndependent.Key.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name + "." + NamesOfEntities.MatchClassName(nestedIndependent.Key.originalPatternGraph.pathPrefix + nestedIndependent.Key.originalPatternGraph.name),
                            NamesOfEntities.MatchedIndependentVariable(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name));
                    }
                    else
                    {
                        sb.AppendFrontFormat("private {0} {1} = new {0}();",
                            matchingPatternClass.GetType().Name + "." + NamesOfEntities.MatchClassName(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name),
                            NamesOfEntities.MatchedIndependentVariable(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name));
                    }
                }
            }

            foreach (PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                GenerateIndependentsMatchObjects(sb, matchingPatternClass, idpt);
            }
        }

        /// <summary>
        /// Generates memory pooling code for matching tasks of class given by it's name
        /// </summary>
        private void GenerateTasksMemoryPool(SourceBuilder sb, String className, bool isAlternative, bool isIterationBreaking, int branchingFactor)
        {
            // getNewTask method handing out new task from pool or creating task if pool is empty
            if(isAlternative)
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
            if(isAlternative)
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

            // for parallelized subpatterns/alternatives/iterateds we need a freelist per thread
            if(branchingFactor > 1)
            {
                // getNewTask method handing out new task from pool or creating task if pool is empty
                if(isAlternative)
                    sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                        + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_, int threadId) {\n");
                else
                    sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                        + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, int threadId) {\n");
                sb.Indent();
                sb.AppendFront(className + " newTask;\n");
                sb.AppendFront("if(numFreeTasks_perWorker[threadId]>0) {\n");
                sb.Indent();
                sb.AppendFront("newTask = freeListHead_perWorker[threadId];\n");
                sb.AppendFront("newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;\n");
                if(isAlternative)
                    sb.AppendFront("newTask.patternGraphs = patternGraphs_;\n");
                else if(isIterationBreaking)
                    sb.AppendFront("newTask.breakIteration = false;\n");
                sb.AppendFront("freeListHead_perWorker[threadId] = newTask.next;\n");
                sb.AppendFront("newTask.next = null;\n");
                sb.AppendFront("--numFreeTasks_perWorker[threadId];\n");
                sb.Unindent();
                sb.AppendFront("} else {\n");
                sb.Indent();
                if(isAlternative)
                    sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_, patternGraphs_);\n");
                else
                    sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_);\n");
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.AppendFront("return newTask;\n");
                sb.Unindent();
                sb.AppendFront("}\n\n");

                // releaseTask method recycling task into pool if pool is not full
                sb.AppendFront("public static void releaseTask(" + className + " oldTask, int threadId) {\n");
                sb.Indent();
                sb.AppendFront("if(numFreeTasks_perWorker[threadId]<MAX_NUM_FREE_TASKS/2) {\n");
                sb.Indent();
                sb.AppendFront("oldTask.next = freeListHead_perWorker[threadId];\n");
                sb.AppendFront("oldTask.actionEnv = null; oldTask.openTasks = null;\n");
                sb.AppendFront("freeListHead_perWorker[threadId] = oldTask;\n");
                sb.AppendFront("++numFreeTasks_perWorker[threadId];\n");
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.Unindent();
                sb.AppendFront("}\n\n");

                // tasks pool administration data
                sb.AppendFront("private static " + className + "[] freeListHead_perWorker = new " + className + "[Math.Min(" + branchingFactor + ", Environment.ProcessorCount)];\n");
                sb.AppendFront("private static int[] numFreeTasks_perWorker = new int[Math.Min(" + branchingFactor + ", Environment.ProcessorCount)];\n");
            }
        }

        /// <summary>
        /// Generates matcher class tail source code
        /// </summary>
        public void GenerateMatcherClassTail(SourceBuilder sb, bool containedInPackage)
        {
            sb.Unindent();
            sb.AppendFront("}\n");

            if(containedInPackage)
            {
                sb.Unindent();
                sb.AppendFront("}\n");
            }

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates scheduled search plans needed for matcher code generation for action compilation
        /// out of graph with analyze information, 
        /// The scheduled search plans are added to the main and the nested pattern graphs.
        /// </summary>
        public void GenerateScheduledSearchPlans(PatternGraph patternGraph, LGSPGraph graph,
            bool isSubpatternLike, bool isNegativeOrIndependent)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                patternGraph.AdaptToMaybeNull(i);
                PlanGraph planGraph = GeneratePlanGraph(graph.statistics, patternGraph,
                    isNegativeOrIndependent, isSubpatternLike);
                MarkMinimumSpanningArborescence(planGraph, patternGraph.name);
                SearchPlanGraph searchPlanGraph = GenerateSearchPlanGraph(planGraph);
                ScheduledSearchPlan scheduledSearchPlan = ScheduleSearchPlan(
                    searchPlanGraph, patternGraph, isNegativeOrIndependent);
                AppendHomomorphyInformation(scheduledSearchPlan);
                patternGraph.schedules[i] = scheduledSearchPlan;
                patternGraph.RevertMaybeNullAdaption(i);

                foreach (PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(neg, graph, isSubpatternLike, true);
                }

                foreach (PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(idpt, graph, isSubpatternLike, true);
                }

                foreach (Alternative alt in patternGraph.alternativesPlusInlined)
                {
                    foreach (PatternGraph altCase in alt.alternativeCases)
                    {
                        GenerateScheduledSearchPlans(altCase, graph, isSubpatternLike, false);
                    }
                }

                foreach (Iterated iter in patternGraph.iteratedsPlusInlined)
                {
                    GenerateScheduledSearchPlans(iter.iteratedPattern, graph, isSubpatternLike, false);
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
            Debug.Assert(action.rulePattern.patternGraph.embeddedGraphsPlusInlined.Length == 0);

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
        /// Do the static search planning again so we can explain the search plan
        /// </summary>
        public void FillInStaticSearchPlans(LGSPGraphStatistics graphStatistics, params LGSPAction[] actions)
        {
            if(actions.Length == 0) throw new ArgumentException("No actions provided!");

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

            // build search plans for the subpatterns
            foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPattern in subpatternMatchingPatterns)
            {
                LGSPMatchingPattern smp = subpatternMatchingPattern.Key;

                LGSPGrGen.GenerateScheduledSearchPlans(smp.patternGraph, graphStatistics, this, true, false);

                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(smp.patternGraph);
            }

            // build search plans code for actions
            foreach(LGSPAction action in actions)
            {
                LGSPGrGen.GenerateScheduledSearchPlans(action.rulePattern.patternGraph, graphStatistics, this, false, false);

                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(action.rulePattern.patternGraph);
            }
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

                ParallelizeAsNeeded(smp);

                GenerateActionAndMatcher(sourceCode, smp, false);
            }

            // generate code for actions
            foreach(LGSPAction action in actions)
            {
                GenerateScheduledSearchPlans(action.rulePattern.patternGraph, graph, false, false);

                MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(action.rulePattern.patternGraph);

                ParallelizeAsNeeded(action.rulePattern);

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
