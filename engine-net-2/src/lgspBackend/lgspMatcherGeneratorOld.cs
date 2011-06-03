/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define PRODUCE_UNSAFE_MATCHERS // todo: what for ?
#define CSHARPCODE_VERSION2
#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define NO_EDGE_LOOKUP
//#define NO_ADJUST_LIST_HEADS
//#define RANDOM_LOOKUP_LIST_START      // currently broken
//#define USE_INSTANCEOF_FOR_TYPECHECKS  // not implemented in new code - todo: needed ?
#define OLDMAPPEDFIELDS
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
    /// </summary>
    public class LGSPMatcherGeneratorOld
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
        /// Instantiates a new instance of LGSPMatcherGenerator with the given graph model.
        /// </summary>
        /// <param name="model">The model for which the matcher functions shall be generated.</param>
        public LGSPMatcherGeneratorOld(IGraphModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Builds a plan graph out of a given pattern graph.
        /// </summary>
        /// <param name="graph">The host graph to optimize the matcher program for,
        /// providing statistical information about its structure </param>
        public PlanGraph GeneratePlanGraph(LGSPGraph graph, PatternGraph patternGraph, bool negativePatternGraph)
        {
            PlanNode[] nodes = new PlanNode[patternGraph.Nodes.Length + patternGraph.Edges.Length];
            // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)
            List<PlanEdge> edges = new List<PlanEdge>(patternGraph.Nodes.Length + 5 * patternGraph.Edges.Length);

            int nodesIndex = 0;

            PlanNode root = new PlanNode("root");

            // create plan nodes and lookup plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.Nodes.Length; i++)
            {
                PatternNode node = patternGraph.nodes[i];
                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(node.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && node.PatternElementType == PatternElementType.Normal)
                {
#if OPCOST_WITH_GEO_MEAN
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
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
                nodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                edges.Add(rootToNodeEdge);
                nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
                node.TempPlanMapping = nodes[nodesIndex];
                nodesIndex++;
            }

            // create plan nodes and necessary plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.Edges.Length; i++)
            {
                PatternEdge edge = patternGraph.edges[i];

                bool isPreset;
#if !NO_EDGE_LOOKUP
                float cost;
                SearchOperationType searchOperationType;
                if(edge.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
#if OPCOST_WITH_GEO_MEAN
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
#if VSTRUCT_VAL_FOR_EDGE_LOOKUP
                    int sourceTypeID;
                    if(edge.source != null) sourceTypeID = edge.source.TypeID;
                    else sourceTypeID = model.NodeModel.RootType.TypeID;
                    int targetTypeID;
                    if(edge.target != null) targetTypeID = edge.target.TypeID;
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
                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset);
                PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                edges.Add(rootToNodeEdge);
                nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
#else
                SearchOperationType searchOperationType = SearchOperationType.Lookup;       // lookup as dummy
                if(edge.PatternElementType == PatternElementType.Preset)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else isPreset = false;
                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset);
                if(isPreset)
                {
                    PlanEdge rootToNodeEdge = new PlanEdge(searchOperationType, root, nodes[nodesIndex], 0);
                    edges.Add(rootToNodeEdge);
                    nodes[nodesIndex].IncomingEdges.Add(rootToNodeEdge);
                }
#endif
                // only add implicit source operation if edge source is needed and the edge source is not a preset node
                if(edge.source != null && !edge.source.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN
                    PlanEdge implSrcEdge = new PlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], edge.source.TempPlanMapping, 0);
#else
                    PlanEdge implSrcEdge = new PlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], edge.source.TempPlanMapping, 1);
#endif
                    edges.Add(implSrcEdge);
                    edge.source.TempPlanMapping.IncomingEdges.Add(implSrcEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node
                if(edge.target != null && !edge.target.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN
                    PlanEdge implTgtEdge = new PlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], edge.target.TempPlanMapping, 0);
#else
                    PlanEdge implTgtEdge = new PlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], edge.target.TempPlanMapping, 1);
#endif
                    edges.Add(implTgtEdge);
                    edge.target.TempPlanMapping.IncomingEdges.Add(implTgtEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset
                if(!isPreset)
                {
                    // no outgoing if no source
                    if(edge.source != null)
                    {
                        int targetTypeID;
                        if(edge.target != null) targetTypeID = edge.target.TypeID;
                        else targetTypeID = model.NodeModel.RootType.TypeID;
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float normCost = graph.vstructs[((edge.source.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + targetTypeID) * 2 + (int) LGSPDir.Out];
#else
                        float normCost = graph.vstructs[edge.source.TypeID, edge.TypeID, targetTypeID, (int) LGSPDir.Out];
#endif
                        if(graph.nodeCounts[edge.source.TypeID] != 0)
                            normCost /= graph.nodeCounts[edge.source.TypeID];
                        PlanEdge outEdge = new PlanEdge(SearchOperationType.Outgoing, edge.source.TempPlanMapping, nodes[nodesIndex], normCost);
                        edges.Add(outEdge);
                        nodes[nodesIndex].IncomingEdges.Add(outEdge);
                    }

                    // no incoming if no target
                    if(edge.target != null)
                    {
                        int sourceTypeID;
                        if(edge.source != null) sourceTypeID = edge.source.TypeID;
                        else sourceTypeID = model.NodeModel.RootType.TypeID;
#if MONO_MULTIDIMARRAY_WORKAROUND
                        float revCost = graph.vstructs[((edge.target.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + sourceTypeID) * 2 + (int) LGSPDir.In];
#else
                        float revCost = graph.vstructs[edge.target.TypeID, edge.TypeID, sourceTypeID, (int) LGSPDir.In];
#endif
                        if(graph.nodeCounts[edge.target.TypeID] != 0)
                            revCost /= graph.nodeCounts[edge.target.TypeID];
                        PlanEdge inEdge = new PlanEdge(SearchOperationType.Incoming, edge.target.TempPlanMapping, nodes[nodesIndex], revCost);
                        edges.Add(inEdge);
                        nodes[nodesIndex].IncomingEdges.Add(inEdge);
                    }
                }

                nodesIndex++;
            }
            return new PlanGraph(root, nodes, edges.ToArray(), patternGraph);
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
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
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
                case SearchOperationType.ImplicitSource: typeStr = "IS"; break;
                case SearchOperationType.ImplicitTarget: typeStr = "IT"; break;
                case SearchOperationType.Lookup: typeStr = " *"; break;
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
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
                case SearchOperationType.Outgoing: typeStr = src.PatternElement.Name + "-" + tgt.PatternElement.Name + "->"; break;
                case SearchOperationType.Incoming: typeStr = src.PatternElement.Name + "<-" + tgt.PatternElement.Name + "-"; break;
                case SearchOperationType.ImplicitSource: typeStr = "<-" + src.PatternElement.Name + "-" + tgt.PatternElement.Name; break;
                case SearchOperationType.ImplicitTarget: typeStr = "-" + src.PatternElement.Name + "->" + tgt.PatternElement.Name; break;
                case SearchOperationType.Lookup: typeStr = "*" + tgt.PatternElement.Name; break;
                case SearchOperationType.MaybePreset: typeStr = "p" + tgt.PatternElement.Name; break;
                case SearchOperationType.NegPreset: typeStr = "np" + tgt.PatternElement.Name; break;
                case SearchOperationType.Condition:
                    typeStr = " ?(" + String.Join(",", ((Condition) op.Element).NeededNodes) + ")("
                        + String.Join(",", ((Condition) op.Element).NeededEdges) + ")";
                    break;
                case SearchOperationType.NegativePattern:
                    typeStr = " !(" + ScheduleToString(((ScheduledSearchPlan) op.Element).Operations) + " )";
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
                            case SearchOperationType.MaybePreset:
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
        /// Generate search plan graph out of the plan graph, i.e. construct a new graph with outgoing
        /// edges for nodes and only tree edges
        /// </summary>
        /// <param name="planGraph">The source plan graph</param>
        /// <returns>A new search plan graph</returns>
        public SearchPlanGraph GenerateSearchPlanGraph(PlanGraph planGraph)
        {
            SearchPlanNode root = new SearchPlanNode("search plan root");
            SearchPlanNode[] nodes = new SearchPlanNode[planGraph.Nodes.Length];
            SearchPlanEdge[] edges = new SearchPlanEdge[planGraph.Nodes.Length - 1 + 1];    // +1 for root
            // for generating edges
            Dictionary<PlanNode, SearchPlanNode> planMap = new Dictionary<PlanNode, SearchPlanNode>(planGraph.Nodes.Length);
            planMap.Add(planGraph.Root, root);

            // generate the search plan graph nodes
            int i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                if(node.NodeType == PlanNodeType.Edge)
                    nodes[i] = new SearchPlanEdgeNode(node, null, null);
                else
                    nodes[i] = new SearchPlanNodeNode(node);
                planMap.Add(node, nodes[i]);
                i++;
            }

            // generate the search plan graph edges
            i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                PlanEdge edge = node.IncomingMSAEdge;
                edges[i] = new SearchPlanEdge(edge.Type, planMap[edge.Source], planMap[edge.Target], edge.Cost);
                planMap[edge.Source].OutgoingEdges.Add(edges[i]);
                if(node.NodeType == PlanNodeType.Edge)
                {
                    SearchPlanEdgeNode spedgenode = (SearchPlanEdgeNode) planMap[node];
                    SearchPlanNode patelem;
                    if(edge.Target.PatternEdgeSource != null && planMap.TryGetValue(edge.Target.PatternEdgeSource, out patelem))
                    {
                        spedgenode.PatternEdgeSource = (SearchPlanNodeNode) patelem;
                        spedgenode.PatternEdgeSource.OutgoingPatternEdges.AddLast(spedgenode);
                    }
                    if(edge.Target.PatternEdgeTarget != null && planMap.TryGetValue(edge.Target.PatternEdgeTarget, out patelem))
                    {
                        spedgenode.PatternEdgeTarget = (SearchPlanNodeNode) patelem;
                        spedgenode.PatternEdgeTarget.IncomingPatternEdges.AddLast(spedgenode);
                    }
                }
                i++;
            }
            return new SearchPlanGraph(root, nodes, edges, planGraph.PatternGraph);
        }

        /// <summary>
        /// Generates a scheduled search plan for a given search plan graph and optional negative search plan graphs
        /// </summary>
        /// <param name="negSpGraphs">a list - possibly empty - if a positive search plan graph is given,
        /// null if a negative search plan graph is scheduled </param>
        public ScheduledSearchPlan ScheduleSearchPlan(SearchPlanGraph spGraph, SearchPlanGraph[] negSpGraphs)
        {
            List<SearchOperation> operations = new List<SearchOperation>();

            //
            // schedule positive search plan
            //

            // a set of search plan edges representing the currently reachable not yet visited elements
            PriorityQueue<SearchPlanEdge> activeEdges = new PriorityQueue<SearchPlanEdge>();

            // first schedule all preset elements
            foreach(SearchPlanNode node in spGraph.Nodes)
            {
                if(!node.IsPreset) continue;

                foreach(SearchPlanEdge edge in node.OutgoingEdges)
                    activeEdges.Add(edge);

                SearchOperation newOp = new SearchOperation(
                    negSpGraphs == null ? SearchOperationType.NegPreset : SearchOperationType.MaybePreset,
                    node, spGraph.Root, 0);

                operations.Add(newOp);
            }

            // iterate over all reachable elements until the whole graph has been scheduled(/visited),
            // choose next cheapest operation, update the reachable elements and the search plan costs
            SearchPlanNode lastNode = spGraph.Root;
            for(int i = 0; i < spGraph.Nodes.Length - spGraph.NumPresetElements; i++)
            {
                foreach (SearchPlanEdge edge in lastNode.OutgoingEdges)
                {
                    /* no, that if is needed */
                    if(edge.Type == SearchOperationType.MaybePreset || edge.Type == SearchOperationType.NegPreset) continue;
                    activeEdges.Add(edge);
                }
                SearchPlanEdge minEdge = activeEdges.DequeueFirst();
                lastNode = minEdge.Target;

                SearchOperation newOp = new SearchOperation(minEdge.Type, lastNode,
                    minEdge.Source, minEdge.Cost);

                foreach(SearchOperation op in operations)
                    op.CostToEnd += minEdge.Cost;

                operations.Add(newOp);
            }

            // insert negative search plans into the operation schedule
            if(negSpGraphs != null)
            {
                // schedule each negative search plan

                ScheduledSearchPlan[] negScheduledSPs = new ScheduledSearchPlan[negSpGraphs.Length];
                for(int i = 0; i < negSpGraphs.Length; i++)
                    negScheduledSPs[i] = ScheduleSearchPlan(negSpGraphs[i], null);

                // which elements belong to the positive pattern

                Dictionary<String, bool> positiveElements = new Dictionary<String, bool>();
                foreach(SearchPlanNode spnode in spGraph.Nodes)
                    positiveElements.Add(spnode.PatternElement.Name, true);

                // iterate over all negative scheduled search plans (TODO: order?)
                Dictionary<String, bool> neededElements = new Dictionary<String, bool>();
                foreach(ScheduledSearchPlan negSSP in negScheduledSPs)
                {
                    int bestFitIndex = operations.Count;
                    float bestFitCostToEnd = 0;

                    // calculate needed elements for the negative pattern
                    // (elements from the positive graph needed in order to execute the nac)

                    neededElements.Clear();
                    foreach(SearchOperation op in negSSP.Operations)
                    {
                        if(op.Type == SearchOperationType.NegativePattern) continue;
                        if(op.Type == SearchOperationType.Condition)
                        {
                            Condition cond = (Condition) op.Element;
                            foreach(String neededNode in cond.NeededNodes)
                            {
                                if(positiveElements.ContainsKey(neededNode))
                                    neededElements[neededNode] = true;
                            }
                            foreach(String neededEdge in cond.NeededEdges)
                            {
                                if(positiveElements.ContainsKey(neededEdge))
                                    neededElements[neededEdge] = true;
                            }
                        }
                        else
                        {
                            SearchPlanNode spnode = (SearchPlanNode) op.Element;
                            if(positiveElements.ContainsKey(spnode.PatternElement.Name))
                                neededElements[spnode.PatternElement.Name] = true;
                        }
                    }

                    // find best place for this pattern
                    for(int i = operations.Count - 1; i >= 0; i--)
                    {
                        SearchOperation op = operations[i];
                        if(op.Type == SearchOperationType.NegativePattern
                            || op.Type == SearchOperationType.Condition) continue;
                        // needed element matched at this operation?
                        if(neededElements.ContainsKey(((SearchPlanNode) op.Element).PatternElement.Name))
                            break;                      // yes, stop here
                        if(negSSP.Cost <= op.CostToEnd)
                        {
                            // best fit as CostToEnd is monotonously growing towards operation[0]
                            bestFitIndex = i;
                            bestFitCostToEnd = op.CostToEnd;
                        }
                    }

                    // and insert pattern there
                    operations.Insert(bestFitIndex, new SearchOperation(SearchOperationType.NegativePattern,
                        negSSP, null, bestFitCostToEnd + negSSP.Cost));

                    // update costs of operations before bestFitIndex
                    for(int i = 0; i < bestFitIndex; i++)
                        operations[i].CostToEnd += negSSP.Cost;
                }
            }

            // Schedule conditions at the earliest position possible

            Condition[] conditions = spGraph.PatternGraph.Conditions;
            for(int j = conditions.Length - 1; j >= 0; j--)
            {
                Condition condition = conditions[j];
                int k;
                float costToEnd = 0;
                for(k = operations.Count - 1; k >= 0; k--)
                {
                    SearchOperation op = operations[k];
                    if(op.Type == SearchOperationType.NegativePattern || op.Type == SearchOperationType.Condition) continue;
                    String curElemName = ((SearchPlanNode) op.Element).PatternElement.Name;
                    bool isNeededElem = false;
                    if(((SearchPlanNode) op.Element).NodeType == PlanNodeType.Node)
                    {
                        foreach(String neededNode in condition.NeededNodes)
                        {
                            if(curElemName == neededNode)
                            {
                                isNeededElem = true;
                                costToEnd = op.CostToEnd;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach(String neededEdge in condition.NeededEdges)
                        {
                            if(curElemName == neededEdge)
                            {
                                isNeededElem = true;
                                costToEnd = op.CostToEnd;
                                break;
                            }
                        }
                    }
                    // does the current operation retrieve a needed element ?
                    if(isNeededElem)
                        break;          // yes, so the condition cannot be scheduled earlier
                }
                operations.Insert(k + 1, new SearchOperation(SearchOperationType.Condition, condition, null, costToEnd));
            }

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            return new ScheduledSearchPlan(operations.ToArray(), spGraph.PatternGraph, cost);
        }

        /// <summary>
        /// checks for each operation in the scheduled search plan whether the resulting host graph element
        /// must be mapped to a pattern element, needed for isomorphism checks later on
        /// </summary>
        public void CalculateNeededMaps(ScheduledSearchPlan schedSP)
        {
            // examine all operations pairwise
            // find first element of pair here, second is searched for within CheckMapsForOperation
            for(int i = 0; i < schedSP.Operations.Length; i++)
            {
                SearchOperation op1 = schedSP.Operations[i];

                if(op1.Type == SearchOperationType.Condition) continue;
                if(op1.Type == SearchOperationType.NegativePattern)
                {
                    ScheduledSearchPlan curSSP = (ScheduledSearchPlan) op1.Element;
                    for(int j = 0; j < curSSP.Operations.Length - 1; j++)
                    {
                        op1 = curSSP.Operations[j];
                        if(op1.Type == SearchOperationType.Condition) continue;
                        CheckMapsForOperation(op1, curSSP, j + 1);
                    }
                }
                else
                {
                    if(i == schedSP.Operations.Length - 1) break;
                    CheckMapsForOperation(op1, schedSP, i + 1);
                }
            }
        }

        /// <summary>
        /// Checks whether two non-homomorphic pattern elements might be matched as the same host graph element.
        /// For any pair fulfilling this condition, flags are set letting the first operation(op1) set the
        /// mappedTo/negMappedTo-field of the host graph element and the second operation check this field
        /// </summary>
        private void CheckMapsForOperation(SearchOperation op1, ScheduledSearchPlan curSSP, int op2StartIndex)
        {
            GrGenType[] types;
            bool[,] homArray;
            bool[] homToAllArray;
            SearchPlanNode elem1 = (SearchPlanNode) op1.Element;
            PlanNodeType pntype = elem1.NodeType;
            if(pntype == PlanNodeType.Node)
            {
                types = model.NodeModel.Types;
                homArray = curSSP.PatternGraph.HomomorphicNodes;
                homToAllArray = curSSP.PatternGraph.HomomorphicToAllNodes;
            }
            else
            {
                types = model.EdgeModel.Types;
                homArray = curSSP.PatternGraph.HomomorphicEdges;
                homToAllArray = curSSP.PatternGraph.HomomorphicToAllEdges;
            }

            if(homToAllArray[elem1.ElementID - 1]) return;

            GrGenType type1 = types[elem1.PatternElement.TypeID];

            for(int j = op2StartIndex; j < curSSP.Operations.Length; j++)
            {
                SearchOperation op2 = curSSP.Operations[j];
                if(op2.Type == SearchOperationType.Condition) continue;
                if(op2.Type == SearchOperationType.NegativePattern) continue;

                SearchPlanNode elem2 = (SearchPlanNode) op2.Element;
                if(elem2.NodeType != pntype) continue;          // aren't both elements edges or both nodes?

                if(homToAllArray[elem2.ElementID - 1]) continue;

                if(homArray != null && homArray[elem1.ElementID - 1, elem2.ElementID - 1])
                {
                    if(op1.Isomorphy.HomomorphicID == 0) {
                        op1.Isomorphy.HomomorphicID = op2.Isomorphy.HomomorphicID = elem1.ElementID;
                    } else {
                        op2.Isomorphy.HomomorphicID = op1.Isomorphy.HomomorphicID;
                    }
                }


                // TODO: Check type constraints for optimization!!!
                GrGenType type2 = types[elem2.PatternElement.TypeID];
                foreach(GrGenType subtype1 in type1.SubOrSameTypes)
                {
                    if((type2.IsA(subtype1) || subtype1.IsA(type2)) // IsA==IsSuperTypeOrSameType
                        && (homArray == null || !homArray[elem1.ElementID - 1, elem2.ElementID - 1]))
                    {
                        op1.Isomorphy.MustSetMapped = true;
                        op2.Isomorphy.MustCheckMapped = true;
                    }
                }
            }
        }

#if CSHARPCODE_VERSION2

        /// <summary>
        /// Code generated per operation consists of front part, cut by code for next operation, and code for tail part
        /// Operation state is necessary for generating tail part fitting front part, is remembered on the operation stack
        /// </summary>
        class OperationState
        {
            public static int labelid = 0; // to prevent problems with equally named labels
            public SearchOperationType OperationType; // the type of the operation originating this state
            public SearchPlanNode TargetNode; // and its search plan node
            public bool IsNode; // node/edge?
            public String CurPosVar; // name of local variable containing index into list with candidates
                                     // or name of local variable containing candidate itself
            public String ListVar; // name of local variable containing list of candidates (null if no list used)
            public String MappedVar; // name of local variable to which the graph element was mapped or null
                                     // curious way of encoding whether graph element was mapped to CurPosVar or not
            public String ContinueBeforeUnmapLabel; // label giving code position to continue execution with/before unmapping
            public bool ContinueBeforeUnmapLabelUsed; // label was used ?
            public String ContinueAfterUnmapLabel; // label giving code position to continue execution without/after unmapping
            public bool ContinueAfterUnmapLabelUsed; // label was used ?
            public SearchOperation Operation;

            public OperationState(SearchOperationType type, SearchPlanNode targetNode, String curPosVar, String listVar)
            {
                OperationType = type;
                TargetNode = targetNode;
                IsNode = targetNode != null && targetNode.NodeType == PlanNodeType.Node;
                CurPosVar = curPosVar;
                ListVar = listVar;
                ContinueBeforeUnmapLabel = "contunmap_" + curPosVar + "_" + labelid++;
                ContinueBeforeUnmapLabelUsed = false;
                ContinueAfterUnmapLabel = "cont_" + curPosVar + "_" + labelid++;
                ContinueAfterUnmapLabelUsed = false;
            }
        }

        class OpStateLookup : OperationState
        {
            public bool UsesLoop;

            public OpStateLookup(SearchPlanNode targetNode, String curPosVar, String listVar, bool usesLoop)
                : base(SearchOperationType.Lookup, targetNode, curPosVar, listVar)
            {
                UsesLoop = usesLoop;
            }
        }

        class OpStateImplicit : OperationState
        {
            public OpStateImplicit(SearchOperationType type, SearchPlanNode targetNode, String curPosVar)
                : base(type, targetNode, curPosVar, null) { }
        }

        class OpStateExtend : OperationState
        {
            public String HeadName;

            public OpStateExtend(SearchOperationType type, SearchPlanNode targetNode, String curPosVar, String listVar, String headName)
                : base(type, targetNode, curPosVar, listVar)
            {
                HeadName = headName;
            }
        }

        class OpStatePreset : OperationState
        {
            public String WasSetVar;

            public OpStatePreset(SearchPlanNode targetNode, String curPosVar, String listVar, String wasSetVar)
                : base(SearchOperationType.MaybePreset, targetNode, curPosVar, listVar)
            {
                WasSetVar = wasSetVar;
            }
        }

        class LoopStackItem
        {
            public String LabelName;
            public bool LabelUsed = false;

            public LoopStackItem(String labelName)
            {
                LabelName = labelName;
            }
        }

        private void EmitHomomorphicCheck(SearchPlanNode spnode, PatternGraph patternGraph, LinkedList<OperationState> opStack, SourceBuilder sourceCode)
        {
            sourceCode.Append(" && (false");

            bool[,] homArray;
            GrGenType[] types;
            if(spnode.NodeType == PlanNodeType.Node)
            {
                homArray = patternGraph.homomorphicNodes;
                types = model.NodeModel.Types;
            }
            else if(spnode.NodeType == PlanNodeType.Edge)
            {
                homArray = patternGraph.homomorphicEdges;
                types = model.EdgeModel.Types;
            }
            else
            {
                throw new ArgumentException("Illegal search plan node specified to EmitHomomorphicCheck!");
            }

            String curElemName = opStack.First.Value.CurPosVar;
            GrGenType type1 = types[spnode.PatternElement.TypeID];

            foreach(OperationState opState in opStack)
            {
                if(opState.OperationType == SearchOperationType.NegativePattern
                    || opState.OperationType == SearchOperationType.Condition) continue;

                SearchPlanNode otherNode = opState.TargetNode;
                if(otherNode.NodeType != spnode.NodeType) continue;
                if(homArray != null && homArray[spnode.ElementID - 1, otherNode.ElementID - 1]) continue;

                // TODO: Check type constraints!!!
                GrGenType type2 = types[otherNode.PatternElement.TypeID];
                foreach(GrGenType subtype1 in type1.SubOrSameTypes)
                {
                    if((type2.IsA(subtype1) || subtype1.IsA(type2)))        // IsA==IsSuperTypeOrSameType
                    {
                        sourceCode.Append(" || " + opState.CurPosVar + " == " + curElemName);
                        break;
                    }
                }
            }
            sourceCode.Append(")");
        }

        /// <summary>
        /// Emits code for front part of lookup operation
        /// </summary>
        private OperationState EmitLookupFront(SearchPlanNode target, LGSPRulePattern rulePattern, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Lookup(" + target.PatternElement.Name + ":"
                        + (target.NodeType == PlanNodeType.Node ?
                            (ITypeModel) model.NodeModel : (ITypeModel) model.EdgeModel)
                          .Types[target.PatternElement.TypeID].Name + ")\n");

            String elemName = target.PatternElement.Name;
            bool isNode = (target.NodeType == PlanNodeType.Node);
            ITypeModel typeModel = isNode ? (ITypeModel) model.NodeModel : (ITypeModel) model.EdgeModel;
            String elemTypeName = isNode ? "LGSPNode" : "LGSPEdge";
            String nodeEdgeString = isNode ? "node" : "edge";
            String bigNodeEdgeString = isNode ? "Node" : "Edge";
            String typeName = nodeEdgeString + "_type_" + elemName;
            String listName = nodeEdgeString + "_list_" + elemName;
            String headName = nodeEdgeString + "_head_" + elemName;
            String curName = nodeEdgeString + "_cur_" + elemName;

            // find out which types are to be examined for the target element to emit the lookup for
            String typeIDStr;
            bool usesLoop;
            if(target.PatternElement.AllowedTypes == null)
            {
                // AllowedTypes == null -> all subtypes or the same type are allowed

                // need loop if there are subtypes
                usesLoop = typeModel.Types[target.PatternElement.TypeID].SubTypes.GetEnumerator().MoveNext();

                if(usesLoop)
                {
                    // foreach(<Node/Edge>Type type in <element type>.typeVar.SubOrSameTypes)

                    sourceCode.AppendFrontFormat("foreach(" + bigNodeEdgeString
                        + "Type {0} in {1}.typeVar.SubOrSameTypes)\n",
                        typeName, typeModel.TypeTypes[target.PatternElement.TypeID].Name);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    typeIDStr = typeName + ".TypeID";
                }
                else typeIDStr = target.PatternElement.TypeID.ToString();
            }
            else
            {
                // AllowedTypes != null -> exactly the allowed types are given

                if(target.PatternElement.AllowedTypes.Length == 0)
                {
                    // curious form of assert(false)
                    sourceCode.AppendFront("while(false)\n");
                    sourceCode.AppendFront("{\n");
                    typeIDStr = target.PatternElement.TypeID.ToString();    // dummy name, will not be used at runtime anyway
                    usesLoop = true;
                }
                else if(target.PatternElement.AllowedTypes.Length == 1)
                {
                    typeIDStr = target.PatternElement.AllowedTypes[0].TypeID.ToString();
                    usesLoop = false;
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(" + bigNodeEdgeString
                        + "Type {0} in {1})\n",
                        typeName, rulePattern.GetType().Name + "." + elemName + "_AllowedTypes");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    typeIDStr = typeName + ".TypeID";
                    usesLoop = true;
                }
            }

/*            // LinkedGrList<LGSPNode/LGSPEdge> list = lgraph.nodes/edges[<<typeIDStr>>];

            sourceCode.AppendFrontFormat("LinkedGrList<{0}> {1} = lgraph.{2}s[{3}];\n",
                elemTypeName, listName, nodeEdgeString, typeIDStr);

#if RANDOM_LOOKUP_LIST_START
            String randomName = nodeEdgeString + "_random_" + elemName;
            sourceCode.AppendFront("int " + randomName + " = random.Next(" + listName + ".Count);\n");
            sourceCode.AppendFront(elemTypeName + " xx" + headName + " = " + listName + ".head;\n");
            sourceCode.AppendFront("for(int i = 0; i < " + randomName + "; i++) xx" + headName + " = xx" + headName + ".typeNext;\n");
            sourceCode.AppendFront(listName + ".MoveHeadAfter(xx" + headName + ");\n");
#endif

            // for(<LGSPNode/LGSPEdge> head = list.head, curpos = head.typeNext; curpos != head; curpos = curpos.typeNext)

            sourceCode.AppendFrontFormat("for({0} {1} = {2}.head, {3} = {1}.typeNext; {3} != {1}; {3} = {3}.typeNext)\n",
                elemTypeName, headName, listName, curName);*/

#if RANDOM_LOOKUP_LIST_START
            String randomName = nodeEdgeString + "_random_" + elemName;
            sourceCode.AppendFront("int " + randomName + " = random.Next(graph." + nodeEdgeString + "sByTypeCounts[" + typeIDStr + "]);\n");
            sourceCode.AppendFront(elemTypeName + " xx" + headName + " = graph." + nodeEdgeString + "sByTypeHeads[" + typeIDStr + "];\n");
            sourceCode.AppendFront("for(int i = 0; i < " + randomName + "; i++) xx" + headName + " = xx" + headName + ".typeNext;\n");
            sourceCode.AppendFront(listName + ".MoveHeadAfter(xx" + headName + ");\n");
#endif

            // leads to sth. like "for(<LGSPNode/LGSPEdge> head = lgraph.<<nodes/edges>>ByTypeHeads[<<typeIDStr>>], curpos = head.typeNext; curpos != head; curpos = curpos.typeNext)"
            sourceCode.AppendFrontFormat("for({0} {1} = graph.{2}sByTypeHeads[{3}], {4} = {1}.typeNext; {4} != {1}; {4} = {4}.typeNext)\n",
                elemTypeName, headName, nodeEdgeString, typeIDStr, curName);

            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(!isNode)
            {
                // some edge found by lookup, now emit:
                // check whether source/target-nodes of the edge are the same as
                // the already found nodes to which the edge must be incident
                // don't need to if the edge is not required by the pattern to be incident to some given node
                // or that node is not matched by now (signaled by visited)
                SearchPlanEdgeNode edge = (SearchPlanEdgeNode) target;
                if(edge.PatternEdgeSource != null && edge.PatternEdgeSource.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.source != node_cur_{1}) continue;\n", curName,
                        edge.PatternEdgeSource.PatternElement.Name);
                }
                if(edge.PatternEdgeTarget != null && edge.PatternEdgeTarget.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.target != node_cur_{1}) continue;\n", curName,
                        edge.PatternEdgeTarget.PatternElement.Name);
                }
            }
            else //isNode
            {
                // some node found by lookup, now emit:
                // check whether incoming/outgoing-edges of the node are the same as
                // the already found edges to which the node must be incident
                // only for the edges required by the pattern to be incident with the node
                // only if edge is already matched by now (signaled by visited)
                SearchPlanNodeNode node = (SearchPlanNodeNode) target;
                foreach(SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.source != {1}) continue;\n", edge.PatternElement.Name, curName);
                    }
                }
                foreach(SearchPlanEdgeNode edge in node.IncomingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.target != {1}) continue;\n", edge.PatternElement.Name, curName);
                    }
                }
            }

            return new OpStateLookup(target, curName, listName, usesLoop);
        }

        /// <summary>
        /// Emits code for tail part of lookup operation
        /// </summary>
        private void EmitLookupTail(OperationState state, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Tail of Lookup(" + state.CurPosVar + ")\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            if(((OpStateLookup) state).UsesLoop)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        /// <summary>
        /// Emits code for implicit source operation or implicit target operation on given edge
        /// </summary>
        private OperationState EmitImplicit(SearchPlanEdgeNode source, SearchPlanNodeNode target, SearchOperationType opType,
            LGSPRulePattern rulePattern, OperationState lastOpState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Implicit" + ((opType == SearchOperationType.ImplicitSource) ? "Source" : "Target")
                        + "(" + source.PatternElement.Name + " -> " + target.PatternElement.Name + ":"
                        + (target.NodeType == PlanNodeType.Node ?
                            (ITypeModel) model.NodeModel : (ITypeModel) model.EdgeModel)
                          .Types[target.PatternElement.TypeID].Name
                        + ")\n");

            // curpos = edge.<source/target>

            sourceCode.AppendFrontFormat("LGSPNode node_cur_{0} = edge_cur_{1}.{2};\n", target.PatternElement.Name,
                source.PatternElement.Name, (opType == SearchOperationType.ImplicitSource) ? "source" : "target");

            if(target.PatternElement.IsAllowedType != null)         // some types allowed
            {
                sourceCode.AppendFrontFormat("if(!{0}_IsAllowedType[node_cur_{1}.type.TypeID]) goto {2};\n",
                    rulePattern.GetType().Name + "." + target.PatternElement.Name,
                    target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
            }
            else if(target.PatternElement.AllowedTypes == null)     // all subtypes of pattern element type allowed
            {
                if(model.NodeModel.Types[target.PatternElement.TypeID] != model.NodeModel.RootType)
#if USE_INSTANCEOF_FOR_TYPECHECKS
                    sourceCode.AppendFrontFormat("if(!(node_cur_{0}.attributes is I{1})) goto {2};\n",
                        target.PatternElement.Name,
                        ((ITypeFramework) model.NodeModel.Types[target.PatternElement.TypeID]).AttributesType.Name,
                        lastOpState.ContinueBeforeUnmapLabel);
#else
                    sourceCode.AppendFrontFormat("if(!{0}.isMyType[node_cur_{1}.type.TypeID]) goto {2};\n",
                        model.NodeModel.TypeTypes[target.PatternElement.TypeID].Name,
                        target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
#endif
            }
            else if(target.PatternElement.AllowedTypes.Length == 0) // no type allowed
            {
                sourceCode.AppendFront("goto ");
                sourceCode.Append(lastOpState.ContinueBeforeUnmapLabel);
                sourceCode.Append(";\n");
            }
            else // target.PatternElement.AllowedTypes.Length == 1  // only one type allowed
            {
                sourceCode.AppendFrontFormat("if(node_cur_{0}.type.TypeID != {1}) goto {2};\n",
                    target.PatternElement.Name, target.PatternElement.AllowedTypes[0].TypeID, lastOpState.ContinueBeforeUnmapLabel);
            }

            lastOpState.ContinueBeforeUnmapLabelUsed = true;

            foreach (SearchPlanEdgeNode edge in target.OutgoingPatternEdges)
            {
                if(edge == source) continue;
                if(edge.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if(edge_cur_{0}.source != node_cur_{1}) goto {2};\n",
                        edge.PatternElement.Name, target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
                }
            }
            foreach (SearchPlanEdgeNode edge in target.IncomingPatternEdges)
            {
                if(edge == source) continue;
                if(edge.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if(edge_cur_{0}.target != node_cur_{1}) goto {2};\n",
                        edge.PatternElement.Name, target.PatternElement.Name, lastOpState.ContinueBeforeUnmapLabel);
                }
            }
            return new OpStateImplicit(opType, target, "node_cur_" + target.PatternElement.Name);
        }

        /// <summary>
        /// Emits code for front part of extend incoming operation or extend outgoing operation on given node
        /// </summary>
        private OperationState EmitExtendFront(SearchPlanNodeNode source, SearchPlanEdgeNode target, SearchOperationType opType,
            LGSPRulePattern rulePattern, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Extend" + (opType == SearchOperationType.Incoming ? "Incoming" : "Outgoing")
                        + "(" + source.PatternElement.Name + " -> " + target.PatternElement.Name + ":"
                        + (target.NodeType == PlanNodeType.Node ?
                            (ITypeModel) model.NodeModel : (ITypeModel) model.EdgeModel)
                          .Types[target.PatternElement.TypeID].Name
                        + ")\n");

            bool isIncoming = opType == SearchOperationType.Incoming;
            String listStr = isIncoming ? "inhead" : "outhead";
            String nodeStr = isIncoming ? "inNode" : "outNode";

            String sourceName = "node_cur_" + source.PatternElement.Name;
            String headName = "edge_head_" + target.PatternElement.Name;
            String curName = "edge_cur_" + target.PatternElement.Name;

#if RANDOM_LOOKUP_LIST_START
            String randomName = "edge_random_" + target.PatternElement.Name;
            sourceCode.AppendFront("int " + randomName + " = random.Next(" + listName + ".Count);\n");
            sourceCode.AppendFront("LGSPEdge xx" + headName + " = " + listName + ".head;\n");
            sourceCode.AppendFront("for(int i = 0; i < " + randomName + "; i++) xx" + headName + " = xx" + headName + "." + nodeStr +".typeNext;\n");
            if(isIncoming)
                sourceCode.AppendFront(sourceName + ".MoveInHeadAfter(xx" + headName + ");\n");
            else
                sourceCode.AppendFront(sourceName + ".MoveOutHeadAfter(xx" + headName + ");\n");
#endif

            sourceCode.AppendFront("LGSPEdge " + headName + " = " + sourceName + "." + listStr + ";\n");
            sourceCode.AppendFront("if(" + headName + " != null)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("LGSPEdge " + curName + " = " + headName + ";\n");
            sourceCode.AppendFront("do\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(target.PatternElement.IsAllowedType != null)         // some types allowed
            {
                sourceCode.AppendFrontFormat("if(!{0}_IsAllowedType[{1}.type.TypeID]) continue;\n",
                    rulePattern.GetType().Name + "." + target.PatternElement.Name, curName);
            }
            else if(target.PatternElement.AllowedTypes == null)     // all subtypes of pattern element type allowed
            {
#if USE_INSTANCEOF_FOR_TYPECHECKS
                sourceCode.AppendFrontFormat("if(!({0}.attributes is I{1})) continue;\n",
                    curName, ((ITypeFramework)model.EdgeModel.Types[target.PatternElement.TypeID]).AttributesType.Name);
#else
                if(model.EdgeModel.Types[target.PatternElement.TypeID] != model.EdgeModel.RootType)
                    sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.type.TypeID]) continue;\n",
                        model.EdgeModel.TypeTypes[target.PatternElement.TypeID].Name, curName);
#endif
            }
            else if(target.PatternElement.AllowedTypes.Length == 0) // no type allowed
            {
                sourceCode.AppendFront("continue\n");
            }
            else // target.PatternElement.AllowedTypes.Length == 1  // only one type allowed
            {
                sourceCode.AppendFrontFormat("if({0}.type.TypeID != {1}) continue;\n",
                    curName, target.PatternElement.AllowedTypes[0].TypeID);
            }

            if(isIncoming)
            {
                if(target.PatternEdgeSource != null && target.PatternEdgeSource.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.source != node_cur_{1}) continue;\n", curName,
                        target.PatternEdgeSource.PatternElement.Name);
                }
            }
            else // outgoing
            {
                if(target.PatternEdgeTarget != null && target.PatternEdgeTarget.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.target != node_cur_{1}) continue;\n", curName,
                        target.PatternEdgeTarget.PatternElement.Name);
                }
            }

            return new OpStateExtend(opType, target, curName, sourceName, headName);
        }

        /// <summary>
        /// Emits code for tail part of extend operation
        /// </summary>
        private void EmitExtendTail(OperationState loop, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Tail Extend" + (loop.OperationType == SearchOperationType.Incoming ? "Incoming" : "Outgoing")
                        + "(" + loop.CurPosVar + ")\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("while((" + loop.CurPosVar + " = " + loop.CurPosVar + "."
                + (loop.OperationType == SearchOperationType.Incoming ? "inNext" : "outNext") + ") != "
                + ((OpStateExtend) loop).HeadName + ");\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        /// <summary>
        /// Emits code for front part of preset operation
        /// </summary>
        private OperationState EmitMaybePresetFront(SearchPlanNode target, LGSPRulePattern rulePattern, OperationState lastOpState,
            SourceBuilder sourceCode)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? (ITypeModel) model.NodeModel : (ITypeModel) model.EdgeModel;

            sourceCode.AppendFront("// Preset(" + target.PatternElement.Name + ":"
                    + typeModel.Types[target.PatternElement.TypeID].Name + ")\n");

            String curPosVar;
            String elemType;
            String typeListVar;
            String typeIterVar;
            String graphList;
//            String listVar;
            String headVar;
            String wasSetVar;

            if(isNode)
            {
                curPosVar = "node_cur_" + target.PatternElement.Name;
                elemType = "LGSPNode";
                typeListVar = "node_typelist_" + target.PatternElement.Name;
                typeIterVar = "node_typeiter_" + target.PatternElement.Name;
                graphList = "nodes";
//                listVar = "node_list_" + target.PatternElement.Name;
                headVar = "node_listhead_" + target.PatternElement.Name;
                wasSetVar = "node_parWasSet_" + target.PatternElement.Name;
                sourceCode.AppendFrontFormat("NodeType[] {0} = null;\n", typeListVar);
            }
            else // edge
            {
                curPosVar = "edge_cur_" + target.PatternElement.Name;
                elemType = "LGSPEdge";
                typeListVar = "edge_typelist_" + target.PatternElement.Name;
                typeIterVar = "edge_typeiter_" + target.PatternElement.Name;
                graphList = "edges";
//                listVar = "edge_list_" + target.PatternElement.Name;
                headVar = "node_listhead_" + target.PatternElement.Name;
                wasSetVar = "edge_parWasSet_" + target.PatternElement.Name;
                sourceCode.AppendFrontFormat("EdgeType[] {0} = null;\n", typeListVar);
            }

            sourceCode.AppendFrontFormat("int {0} = 0;\n", typeIterVar);
//            sourceCode.AppendFrontFormat("LinkedGrList<{0}> {1} = null;\n", elemType, listVar);
            sourceCode.AppendFrontFormat("{0} {1} = null;\n", elemType, headVar);
            sourceCode.AppendFrontFormat("bool {0};\n", wasSetVar);
            sourceCode.AppendFrontFormat("{0} {1} = ({0}) parameters[{2}];\n", elemType, curPosVar, target.PatternElement.ParameterIndex);
            sourceCode.AppendFrontFormat("if({0} != null)\n", curPosVar);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFrontFormat("{0} = true;\n", wasSetVar);
            if(target.PatternElement.IsAllowedType != null)         // some types allowed
            {
                sourceCode.AppendFrontFormat("if(!{0}_IsAllowedType[{1}.type.TypeID]) goto {2};\n",
                    rulePattern.GetType().Name + "." + target.PatternElement.Name,
                    curPosVar, lastOpState.ContinueBeforeUnmapLabel);
            }
            else if(target.PatternElement.AllowedTypes == null)     // all subtypes of pattern element type allowed
            {
                if(typeModel.Types[target.PatternElement.TypeID] != typeModel.RootType)
                    sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.type.TypeID]) goto {2};\n",
                        typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                        curPosVar, lastOpState.ContinueBeforeUnmapLabel);
            }
            else if(target.PatternElement.AllowedTypes.Length == 0) // no type allowed
            {
                sourceCode.AppendFront("goto ");
                sourceCode.Append(lastOpState.ContinueBeforeUnmapLabel);
                sourceCode.Append(";\n");
            }
            else // target.PatternElement.AllowedTypes.Length == 1  // only one type allowed
            {
                sourceCode.AppendFrontFormat("if({0}.type.TypeID != {1}) goto {2};\n",
                    curPosVar, target.PatternElement.AllowedTypes[0].TypeID, lastOpState.ContinueBeforeUnmapLabel);
            }
            lastOpState.ContinueBeforeUnmapLabelUsed = true;
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("else\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFrontFormat("{0} = false;\n", wasSetVar);
            if(target.PatternElement.AllowedTypes == null)
            {
                sourceCode.AppendFrontFormat("{0} = {1}.typeVar.subOrSameTypes;\n", typeListVar,
                    typeModel.TypeTypes[target.PatternElement.TypeID].Name);
            }
            else if(target.PatternElement.AllowedTypes.Length == 0)
            {
                sourceCode.AppendFront("goto ");
                sourceCode.Append(lastOpState.ContinueBeforeUnmapLabel);
                sourceCode.Append(";\n");
            }
            else
            {
                sourceCode.AppendFront(typeListVar + " = " + rulePattern.GetType().Name + "." + target.PatternElement.Name + "_AllowedTypes;\n");
            }

//            sourceCode.AppendFrontFormat("{0} = lgraph.{1}[{2}[{3}].typeID];\n", listVar, graphList, typeListVar, typeIterVar);
//            sourceCode.AppendFrontFormat("{0} = {1}.head.typeNext;\n", curPosVar, listVar);
            sourceCode.AppendFrontFormat("{0} = graph.{1}ByTypeHeads[{2}[{3}].TypeID];\n", headVar, graphList, typeListVar, typeIterVar);
            sourceCode.AppendFrontFormat("{0} = {1}.typeNext;\n", curPosVar, headVar);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("while(true)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // Iterated through all elements of current type?
//            sourceCode.AppendFrontFormat("while(!{0} && {1} == {2}.head)\n", wasSetVar, curPosVar, listVar);
            sourceCode.AppendFrontFormat("while(!{0} && {1} == {2})\n", wasSetVar, curPosVar, headVar);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // Increment type iteration index
            sourceCode.AppendFront(typeIterVar + "++;\n");

            // If all compatible types checked, preset operation is finished
            sourceCode.AppendFrontFormat("if({0} >= {1}.Length) goto {2};\n", typeIterVar, typeListVar, lastOpState.ContinueBeforeUnmapLabel);

            // Initialize next lookup iteration
//            sourceCode.AppendFrontFormat("{0} = lgraph.{1}[{2}[{3}].TypeID];\n", listVar,
//                isNode ? "nodes" : "edges", typeListVar, typeIterVar);
//            sourceCode.AppendFrontFormat("{0} = {1}.head.typeNext;\n", curPosVar, listVar);
            sourceCode.AppendFrontFormat("{0} = graph.{1}ByTypeHeads[{2}[{3}].TypeID];\n", headVar, graphList, typeListVar, typeIterVar);
            sourceCode.AppendFrontFormat("{0} = {1}.typeNext;\n", curPosVar, headVar);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            if(target.NodeType != PlanNodeType.Node)
            {
                SearchPlanEdgeNode edge = (SearchPlanEdgeNode) target;
                if(edge.PatternEdgeSource != null && edge.PatternEdgeSource.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.source != node_cur_{1}) continue;\n", curPosVar,
                        edge.PatternEdgeSource.PatternElement.Name);
                }
                if(edge.PatternEdgeTarget != null && edge.PatternEdgeTarget.Visited)    // check op?
                {
                    sourceCode.AppendFrontFormat("if({0}.target != node_cur_{1}) continue;\n", curPosVar,
                        edge.PatternEdgeTarget.PatternElement.Name);
                }
            }
            else
            {
                SearchPlanNodeNode node = (SearchPlanNodeNode) target;
                foreach(SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.source != {1}) continue;\n",
                            edge.PatternElement.Name, curPosVar);
                    }
                }
                foreach(SearchPlanEdgeNode edge in node.IncomingPatternEdges)
                {
                    if(edge.Visited)    // check op?
                    {
                        sourceCode.AppendFrontFormat("if(edge_cur_{0}.target != {1}) continue;\n",
                            edge.PatternElement.Name, curPosVar);
                    }
                }
            }

//            return new OpStatePreset(isNode, curPosVar, listVar, wasSetVar);
            return new OpStatePreset(target, curPosVar, null, wasSetVar);
        }

        /// <summary>
        /// Emits code for tail part of preset operation
        /// </summary>
        private void EmitMaybePresetTail(OperationState opState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Tail Preset(" + opState.CurPosVar + ")\n");

            OpStatePreset state = (OpStatePreset) opState;

            // If preset was not undefined, break out of preset while loop
            sourceCode.AppendFrontFormat("if({0}) break;\n", state.WasSetVar);

            // Lookup next element
            sourceCode.AppendFrontFormat("{0} = {0}.typeNext;\n", state.CurPosVar);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        /// <summary>
        /// Emits code for negative pattern
        /// </summary>
        private void EmitNegativePattern(ScheduledSearchPlan negSSP, LGSPRulePattern rulePattern, OperationState lastOpState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// NegativePattern\n");

            LinkedList<OperationState> operationStack = new LinkedList<OperationState>();
            operationStack.AddFirst(lastOpState);
            OperationState dummyOp = new OperationState((SearchOperationType) (-1), null, "", null);
            operationStack.AddFirst(dummyOp);
            foreach(SearchOperation op in negSSP.Operations)
            {
                OperationState lastNegOpState = operationStack.First.Value;
                switch(op.Type)
                {
                    case SearchOperationType.Lookup:
                        operationStack.AddFirst(EmitLookupFront((SearchPlanNode) op.Element, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                        operationStack.AddFirst(EmitImplicit((SearchPlanEdgeNode) op.SourceSPNode, (SearchPlanNodeNode) op.Element,
                            op.Type, rulePattern, lastNegOpState, sourceCode));
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        operationStack.AddFirst(EmitExtendFront((SearchPlanNodeNode) op.SourceSPNode, (SearchPlanEdgeNode) op.Element,
                            op.Type, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.NegPreset:
                    {
                        SearchPlanNode node = (SearchPlanNode) op.Element;
                        String curPos = ((node.NodeType == PlanNodeType.Node) ? "node_cur_" : "edge_cur_") + node.PatternElement.Name;
                        operationStack.AddFirst(new OperationState(SearchOperationType.NegPreset, node, curPos, null));
                        break;
                    }

                    case SearchOperationType.Condition:
                        EmitCondition((Condition) op.Element, rulePattern.GetType().Name, lastNegOpState, sourceCode);
                        break;

                    case SearchOperationType.MaybePreset:
                        throw new Exception("Preset operation in negative searchplan!");
                    case SearchOperationType.NegativePattern:
                        throw new Exception("Negative pattern in negative searchplan!");
                }
                if(op.Type != SearchOperationType.Condition)
                {
                    ((SearchPlanNode) op.Element).Visited = true;
                    operationStack.First.Value.Operation = op;
                }

                if(op.Isomorphy.MustCheckMapped)
                {
#if OLDMAPPEDFIELDS
                    if(op.Isomorphy.HomomorphicID != 0)
                    {
                        sourceCode.AppendFrontFormat("if({0}.negMappedTo != 0 && {0}.negMappedTo != {1})",
                            operationStack.First.Value.CurPosVar, op.Isomorphy.HomomorphicID);
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("if({0}.negMappedTo != 0)",
                            operationStack.First.Value.CurPosVar);
                    }
#else
                    sourceCode.AppendFrontFormat("if({0}.isNegMapped", operationStack.First.Value.CurPosVar);
                    if(op.Isomorphy.HomomorphicID != 0)
                        EmitHomomorphicCheck((SearchPlanNode) op.Element, rulePattern.patternGraph, operationStack, sourceCode);
                    sourceCode.Append(")");
#endif
                    sourceCode.Append(" goto " + operationStack.First.Value.ContinueAfterUnmapLabel + ";\n");
                    operationStack.First.Value.ContinueAfterUnmapLabelUsed = true;
                }
                if(op.Isomorphy.MustSetMapped)    // can be only true, if op.Type != SearchOperationType.NegativePattern
                {                       // && op.Type != SearchOperationType.Condition
                    int mapid = op.Isomorphy.HomomorphicID;
                    if(mapid == 0) mapid = ((SearchPlanNode) op.Element).ElementID;
#if OLDMAPPEDFIELDS
                    else sourceCode.AppendFrontFormat("int {0}_prevNegMappedTo = {0}.negMappedTo;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.negMappedTo = {1};\n", operationStack.First.Value.CurPosVar, mapid);
#else
                    else sourceCode.AppendFrontFormat("bool {0}_prevIsNegMapped = {0}.isNegMapped;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.isNegMapped = true;\n", operationStack.First.Value.CurPosVar);
#endif
                    operationStack.First.Value.MappedVar = operationStack.First.Value.CurPosVar;
                }
            }

            //
            // Here the the negative pattern has been found
            //

            // Unmap mapped elements
            foreach(OperationState opState in operationStack)
            {
                if(opState == lastOpState) continue;
                if(opState.MappedVar != null)
                {
                    if(opState.Operation == null || opState.Operation.Isomorphy == null)
                        Console.WriteLine("ARGH!");

#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = {0}_prevNegMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = {0}_prevIsNegMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = false;\n", opState.MappedVar);
#endif
                }
            }

            sourceCode.AppendFrontFormat("goto {0};\n", lastOpState.ContinueBeforeUnmapLabel);
            lastOpState.ContinueBeforeUnmapLabelUsed = true;

            // close all loops

            foreach(OperationState opState in operationStack)
            {
                if(opState == lastOpState) continue;
                if(opState.ContinueBeforeUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueBeforeUnmapLabel);
                if(opState.MappedVar != null)
                {
#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = {0}_prevNegMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.negMappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = {0}_prevIsNegMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isNegMapped = false;\n", opState.MappedVar);
#endif
                }
                if(opState.ContinueAfterUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueAfterUnmapLabel);
                switch(opState.OperationType)
                {
                    case SearchOperationType.Lookup:
                        EmitLookupTail(opState, sourceCode);
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        EmitExtendTail(opState, sourceCode);
                        break;
                }
            }
            if(CommentSourceCode)
                sourceCode.AppendFront("// End of NegativePattern\n");
        }

        /// <summary>
        /// Emits code for condition
        /// </summary>
        private void EmitCondition(Condition condition, String rulePatternTypeName, OperationState lastOpState, SourceBuilder sourceCode)
        {
            if(CommentSourceCode)
                sourceCode.AppendFront("// Condition[" + condition.ID + "]\n");

            sourceCode.AppendFrontFormat("if(!{0}.Condition_{1}(", rulePatternTypeName, condition.ID);
            bool first = true;
            foreach(String neededNode in condition.NeededNodes)
            {
                if(!first) sourceCode.Append(", ");
                else first = false;
                sourceCode.Append("node_cur_" + neededNode);
            }
            foreach(String neededEdge in condition.NeededEdges)
            {
                if(!first) sourceCode.Append(", ");
                else first = false;
                sourceCode.Append("edge_cur_" + neededEdge);
            }

            sourceCode.AppendFormat(")) goto {0};\n", lastOpState.ContinueBeforeUnmapLabel);
            lastOpState.ContinueBeforeUnmapLabelUsed = true;
        }
#else //CSHARPCODE_VERSION2
        class ListState
        {
            public LocalBuilder listVar;
            public LocalBuilder listHeadVar;
            public LocalBuilder lastCurPosVar;
        }

        enum LoopType { Lookup, Incoming, Outgoing };

        class LoopState
        {
            public LoopType loopType;
            public LocalBuilder curPos;
            public LocalBuilder checkVar;
            public Label loopLabel;
            public Label continueLabel;
            public Label checkCondLabel;
            public bool isNode;
            public String elemName;
        }

        private void GenerateMatcher(SearchPlanGraph searchPlanGraph, LGSPAction action)
        {
            Type[] methodArgs = { typeof(LGSPAction), typeof(BaseGraph), typeof(int) };
            DynamicMethod matcher = new DynamicMethod(String.Format("Dynamic matcher for action {0}", action.Name),
                typeof(Matches), methodArgs, typeof(LGSPAction));

            ILGenerator ilMatcher = matcher.GetILGenerator();

            /* Arguments:
             *  [0] this (LGSPAction)
             *  [1] BaseGraph graph
             *  [2] int maxMatches
             */

            //ilMatcher.EmitWriteLine("Entered matcher");

            // [0] Matches matches = new Matches(this);

            ilMatcher.DeclareLocal(typeof(Matches));
            ilMatcher.Emit(OpCodes.Ldarg_0);
            ilMatcher.Emit(OpCodes.Newobj, typeof(Matches).GetConstructor(new Type[1] { typeof(IAction) }));
            ilMatcher.Emit(OpCodes.Stloc_0);

            // [1] LGSPGraph lgraph = (LGSPGraph) graph;

            ilMatcher.DeclareLocal(typeof(LGSPGraph));
            ilMatcher.Emit(OpCodes.Ldarg_1);
            ilMatcher.Emit(OpCodes.Castclass, typeof(LGSPGraph));
            ilMatcher.Emit(OpCodes.Stloc_1);

            // Build ListState objects to manage the lists
            // (this implementation expects node and elem names in patterns to be distinct)

            Dictionary<int, ListState> listTypeMap = new Dictionary<int, ListState>();
            Dictionary<string, ListState> listElemNameMap = new Dictionary<string, ListState>(searchPlanGraph.Root.OutgoingEdges.Count);
            Dictionary<string, LoopState> loopElemNameMap = new Dictionary<string, LoopState>(searchPlanGraph.Root.OutgoingEdges.Count);
            Dictionary<string, LocalBuilder> varElemNameMap = new Dictionary<string, LocalBuilder>(searchPlanGraph.Nodes.Length);

            foreach(SearchPlanEdge edge in searchPlanGraph.Edges) //searchPlanGraph.Root.OutgoingEdges)
            {
                ListState listState;
                LoopState loopState;
                LocalBuilder curPos;

                if(edge.Type == PlanEdgeType.Lookup || edge.Type == PlanEdgeType.Incoming || edge.Type == PlanEdgeType.Outgoing)
                {
                    int typeid = edge.Target.TypeID;
                    if(edge.Target.NodeType == PlanNodeType.Edge)
                        typeid += graph.Model.NodeModel.NumTypes;
                    // Only one ListState object per graph element type
                    if(!listTypeMap.TryGetValue(typeid, out listState))
                    {
                        listState = new ListState();
                        listTypeMap[typeid] = listState;

                        // TODO: If here, mappedTo must be used??
                    }
                    listElemNameMap[edge.Target.Name] = listState;
                    if(edge.Target.NodeType == PlanNodeType.Node)
                    {
                        listState.listVar = ilMatcher.DeclareLocal(typeof(LinkedGrList<LGSPNode>));
                        listState.listHeadVar = ilMatcher.DeclareLocal(typeof(LGSPNode));
                        curPos = ilMatcher.DeclareLocal(typeof(LGSPNode));
                    }
                    else
                    {
                        listState.listVar = ilMatcher.DeclareLocal(typeof(LinkedGrList<LGSPEdge>));
                        listState.listHeadVar = ilMatcher.DeclareLocal(typeof(LGSPEdge));
                        curPos = ilMatcher.DeclareLocal(typeof(LGSPEdge));
                    }
                    listState.lastCurPosVar = curPos;

                    loopState = new LoopState();
                    loopState.curPos = curPos;
                    loopState.loopLabel = ilMatcher.DefineLabel();
                    loopState.continueLabel = ilMatcher.DefineLabel();
                    loopState.checkCondLabel = ilMatcher.DefineLabel();
                    loopState.elemName = edge.Target.Name;
                    loopElemNameMap[edge.Target.Name] = loopState;
                }
                else // edge.Type == PlanEdgeType.ImplicitSource || edge.Type == PlanEdgeType.ImpliciTarget
                {
                    curPos = ilMatcher.DeclareLocal(typeof(LGSPNode));
                }

                varElemNameMap[edge.Target.Name] = curPos;
            }

            // Free ListTypeMap as it is not used anymore
            listTypeMap = null;

            LocalBuilder nodesVar = ilMatcher.DeclareLocal(typeof(INode[]));
            LocalBuilder edgesVar = ilMatcher.DeclareLocal(typeof(IEdge[]));

            int remainingPlanNodes = searchPlanGraph.Nodes.Length;
            Dictionary<SearchPlanNode, int> NodeMap = new Dictionary<SearchPlanNode, int>(searchPlanGraph.Nodes.Length);
            LinkedList<LoopState> loopStack = new LinkedList<LoopState>();
            LinkedList<SearchPlanEdge> activeEdges = new LinkedList<SearchPlanEdge>();
            foreach(SearchPlanEdge edge in searchPlanGraph.Root.OutgoingEdges)
                activeEdges.AddLast(edge);
            searchPlanGraph.Root.Visited = true;

            bool firstextend = true;

            while(remainingPlanNodes > 0)
            {
                float minCost = float.MaxValue;
                SearchPlanEdge minEdge = null;
                for(LinkedListNode<SearchPlanEdge> llnode = activeEdges.First; llnode != null; )
                {
                    SearchPlanEdge edge = llnode.Value;
                    if(edge.Target.Visited)
                    {
                        LinkedListNode<SearchPlanEdge> oldllnode = llnode;
                        llnode = llnode.Next;
                        activeEdges.Remove(oldllnode);
                        continue;
                    }
                    else if(edge.Cost < minCost)
                    {
                        minCost = edge.Cost;
                        minEdge = edge;
                    }
                    llnode = llnode.Next;
                }
                if(minEdge == null)
                    break;
                activeEdges.Remove(minEdge);

                //
                // TODO: check op fehlt !!!!!!!!!!!!!!!!!!!!!!!!!!!
                //

                switch(minEdge.Type)
                {
                    case PlanEdgeType.Lookup:
                        Console.WriteLine("Lookup {0} \"{1}\"", (minEdge.Target.NodeType == PlanNodeType.Node) ? "node" : "edge",
                            minEdge.Target.Name);
                        loopStack.AddFirst(EmitLookupFront(minEdge, listElemNameMap, loopElemNameMap, ilMatcher));
                        break;
                    case PlanEdgeType.Incoming:
                    case PlanEdgeType.Outgoing:
                        if(!firstextend) Console.Write("* ");
                        Console.WriteLine("Extend {0} {1} \"{2}\" -> \"{3}\"",
                            (minEdge.Type == PlanEdgeType.Incoming) ? "incoming" : "outgoing",
                            (minEdge.Target.NodeType == PlanNodeType.Node) ? "node" : "edge",
                            minEdge.Source.Name, minEdge.Target.Name);
                        //if(firstextend)
                        {
                            loopStack.AddFirst(EmitExtendInOut(minEdge, listElemNameMap, loopElemNameMap, varElemNameMap, ilMatcher, firstextend));
                            firstextend = false;
                        }
                        break;
                    case PlanEdgeType.ImplicitSource:
                    case PlanEdgeType.ImplicitTarget:
                        Console.WriteLine("Implicit {0} {1} \"{2}\" -> \"{3}\"",
                            (minEdge.Type == PlanEdgeType.ImplicitSource) ? "implicit source" : "implicit target",
                            (minEdge.Target.NodeType == PlanNodeType.Node) ? "node" : "edge",
                            minEdge.Source.Name, minEdge.Target.Name);
                        EmitImplicit(minEdge, loopElemNameMap, varElemNameMap, ilMatcher);
                        break;
                }
                foreach(SearchPlanEdge edge in minEdge.Target.OutgoingEdges)
                    activeEdges.AddLast(edge);
                minEdge.Target.Visited = true;
            }

            //ilMatcher.EmitWriteLine("match found");

            // INode[] nodes = new INode[<num nodes in pattern>] { <all nodes> }

            EmitLoadConst(ilMatcher, action.RulePattern.PatternGraph.Nodes.Length);
            ilMatcher.Emit(OpCodes.Newarr, typeof(INode));
            ilMatcher.Emit(OpCodes.Stloc, nodesVar);
            int i = 0;
            foreach(PatternNode patNode in action.RulePattern.PatternGraph.Nodes)
            {
                ilMatcher.Emit(OpCodes.Ldloc, nodesVar);
                EmitLoadConst(ilMatcher, i++);
                ilMatcher.Emit(OpCodes.Ldloc, varElemNameMap[patNode.Name]);
                ilMatcher.Emit(OpCodes.Stelem_Ref);
            }

            //ilMatcher.EmitWriteLine("nodes created");

            // IEdge[] edges = new IEdge[<num edges in pattern>] { <all edges> }

            EmitLoadConst(ilMatcher, action.RulePattern.PatternGraph.Edges.Length);
            ilMatcher.Emit(OpCodes.Newarr, typeof(IEdge));
            ilMatcher.Emit(OpCodes.Stloc, edgesVar);
            i = 0;
            foreach(PatternEdge patEdge in action.RulePattern.PatternGraph.Edges)
            {
                ilMatcher.Emit(OpCodes.Ldloc, edgesVar);
                EmitLoadConst(ilMatcher, i++);
                ilMatcher.Emit(OpCodes.Ldloc, varElemNameMap[patEdge.Name]);
                ilMatcher.Emit(OpCodes.Stelem_Ref);
            }

            //ilMatcher.EmitWriteLine("edges created");

            // matches.MatchesList.AddFirst(match) (first part)

            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ldfld, typeof(Matches).GetField("MatchesList"));

            //ilMatcher.EmitWriteLine("match add initialized");

            // Match match = new Match(nodes, edges)

            ilMatcher.Emit(OpCodes.Ldloc, nodesVar);
            ilMatcher.Emit(OpCodes.Ldloc, edgesVar);
            ilMatcher.Emit(OpCodes.Newobj, typeof(Match).GetConstructor(new Type[2] { typeof(INode[]), typeof(IEdge[]) }));

            //ilMatcher.EmitWriteLine("match created");

            // matches.MatchesList.AddFirst(match) (second part)

            ilMatcher.Emit(OpCodes.Call, typeof(SingleLinkedList<Match>).GetMethod("AddFirst"));

            //ilMatcher.EmitWriteLine("match added");

            // if(maxMatches <= 0 || matches.MatchesList.length < maxMatches) continue;

            ilMatcher.Emit(OpCodes.Ldarg_2);
            ilMatcher.Emit(OpCodes.Ldc_I4_0);
            ilMatcher.Emit(OpCodes.Ble, loopStack.First.Value.continueLabel);

            //ilMatcher.EmitWriteLine("check second if condition");
            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ldfld, typeof(Matches).GetField("MatchesList"));
            ilMatcher.Emit(OpCodes.Ldfld, typeof(SingleLinkedList<Match>).GetField("length"));
            ilMatcher.Emit(OpCodes.Ldarg_2);
            //ilMatcher.EmitWriteLine("before blt");
            ilMatcher.Emit(OpCodes.Blt, loopStack.First.Value.continueLabel);

            // return matches

            //ilMatcher.EmitWriteLine("return matches");
            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ret);

            // close remaining loops

            foreach(LoopState loop in loopStack)
            {
                switch(loop.loopType)
                {
                    case LoopType.Lookup:
                        EmitLookupTail(loop, ilMatcher);
                        break;
                    case LoopType.Incoming:
                    case LoopType.Outgoing:
                        EmitExtendInOutTail(loop, ilMatcher);
                        break;
                }
            }

            ilMatcher.Emit(OpCodes.Ldloc_0);
            ilMatcher.Emit(OpCodes.Ret);

            try
            {
                MonoILReader ilReader = new MonoILReader(matcher);
                StreamWriter writer = new StreamWriter(action.Name + ".il");
                writer.WriteLine("Label fixup was {0}", ilReader.FixupSuccess ? "successful" : "not successful");
                foreach(ClrTest.Reflection.ILInstruction instr in ilReader)
                {
                    OpCode opcode = instr.OpCode;
                    OpCode otheropcode = OpCodes.Nop;

                    // if(instr.OpCode == OpCodes.Nop)
                    // lgspActions.cs(1171,20): error CS0019: Operator `==' cannot be applied to operands of type
                    // `ClrTest.Reflection.ILInstruction.OpCode' and `System.Reflection.Emit.OpCodes.Nop'

                    // if(opcode == otheropcode)
                    // causes: lgspActions.cs(1173,20): error CS0019: Operator `==' cannot be applied to operands of type
                    // `System.Reflection.Emit.OpCode' and `System.Reflection.Emit.OpCode'

                    if(opcode.Equals(otheropcode)) writer.Write('.');
                    //                    else writer.WriteLine(instr.ToString());
                    else writer.WriteLine("   IL_{0:x4}:  {1,-10} {2}", instr.Offset, instr.OpCode, instr.RawOperand);
                }
                writer.Close();
                Console.WriteLine("MonoILReader succeeded");
            }
            catch
            {
                Console.WriteLine("MonoILReader failed");
            }
            action.DynamicMatch = (MatchInvoker) matcher.CreateDelegate(typeof(MatchInvoker), action);
        }

        private LoopState EmitLookupFront(SearchPlanEdge edge, Dictionary<String, ListState> listElemNameMap,
            Dictionary<String, LoopState> loopElemNameMap, ILGenerator il)
        {
            String elemName = edge.Target.Name;
            bool isNode = (edge.Target.NodeType == PlanNodeType.Node);
            Type elemType = isNode ? typeof(LGSPNode) : typeof(LGSPEdge);
            Type listType = isNode ? typeof(LinkedGrList<LGSPNode>) : typeof(LinkedGrList<LGSPEdge>);

            ListState listState = listElemNameMap[elemName];
            LoopState loopState = loopElemNameMap[elemName];

            //il.EmitWriteLine(String.Format("Entered EmitLookupFront for \"{0}\"", loopState.elemName));

            // LinkedGrList<LGSPNode/LGSPEdge> list = lgraph.nodes/edges[<<edge.Target.TypeID>>];

            il.Emit(OpCodes.Ldloc_1);
            il.Emit(OpCodes.Ldfld, typeof(LGSPGraph).GetField(isNode ? "nodes" : "edges"));
            if(isNode)
                Console.WriteLine("lookup node \"{0}\" with TypeID {1}", edge.Target.Name, edge.Target.TypeID);
            else
                Console.WriteLine("lookup edge \"{0}\" with TypeID {1}", edge.Target.Name, edge.Target.TypeID);
            EmitLoadConst(il, edge.Target.TypeID);
            il.Emit(OpCodes.Ldelem_Ref);
            //            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, listState.listVar);

            // head = list.head
            il.Emit(OpCodes.Ldloc, listState.listVar);      // instead of dup

            il.Emit(OpCodes.Ldfld, listType.GetField("head"));
            //            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, listState.listHeadVar);

            // curpos = head.next
            il.Emit(OpCodes.Ldloc, listState.listHeadVar);  // instead of dup

            il.Emit(OpCodes.Ldfld, elemType.GetField("next"));
            il.Emit(OpCodes.Stloc, loopState.curPos);
            listState.lastCurPosVar = loopState.curPos;

            //il.EmitWriteLine("LookupFront curPos set to ");
            //il.EmitWriteLine(loopState.curPos);

            // jump to checkCondition

            il.Emit(OpCodes.Br, loopState.checkCondLabel);

            // mark next instruction as loop start

            il.MarkLabel(loopState.loopLabel);

            loopState.loopType = LoopType.Lookup;
            loopState.checkVar = listState.listHeadVar;
            loopState.isNode = isNode;
            return loopState;
        }

        private void EmitLookupTail(LoopState loop, ILGenerator il)
        {
            Type elemType = loop.isNode ? typeof(LGSPNode) : typeof(LGSPEdge);
            il.MarkLabel(loop.continueLabel);
            //il.EmitWriteLine("LookupTail continueLabel");
            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldfld, elemType.GetField("next"));
            il.Emit(OpCodes.Stloc, loop.curPos);

            //il.EmitWriteLine("LookupTail curPos set to ");
            //il.EmitWriteLine(loop.curPos);

            il.MarkLabel(loop.checkCondLabel);
            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldloc, loop.checkVar);
            //il.EmitWriteLine("LookupTail before Bne_Un");
            il.Emit(OpCodes.Bne_Un, loop.loopLabel);
            //il.EmitWriteLine("LookupTail after Bne_Un");
        }

        // edge.source = graph node, edge.target = graph edge
        private LoopState EmitExtendInOut(SearchPlanEdge edge, Dictionary<String, ListState> listElemNameMap,
            Dictionary<String, LoopState> loopElemNameMap, Dictionary<string, LocalBuilder> varElemNameMap, ILGenerator il, bool first)
        {
            ListState targetListState = listElemNameMap[edge.Target.Name];
            LoopState targetLoopState = loopElemNameMap[edge.Target.Name];
            bool isIncoming = edge.Type == PlanEdgeType.Incoming;

            //il.EmitWriteLine(String.Format("Entered EmitExtendInOut for \"{0}\"", targetLoopState.elemName));

            // head = sourceNode.<incoming/outgoing>.head

            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Source.Name]);
            if(isIncoming)
            {
                il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("incoming", BindingFlags.Instance | BindingFlags.Public));
                il.Emit(OpCodes.Ldfld, typeof(LinkedGrInList).GetField("head"));
            }
            else
            {
                il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("outgoing", BindingFlags.Instance | BindingFlags.Public));
                il.Emit(OpCodes.Ldfld, typeof(LinkedGrOutList).GetField("head"));
            }
            //            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, targetListState.listHeadVar);

            // curpos = head.<inNode/outNode>.next
            il.Emit(OpCodes.Ldloc, targetListState.listHeadVar);        // instead of dup

            il.Emit(OpCodes.Ldflda, typeof(LGSPEdge).GetField(isIncoming ? "inNode" : "outNode", BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(InOutListNode).GetField("next"));
            il.Emit(OpCodes.Stloc, targetLoopState.curPos);

            //il.EmitWriteLine("ExtendInOut curPos set to ");
            //il.EmitWriteLine(targetLoopState.curPos);

            targetListState.lastCurPosVar = targetLoopState.curPos;

            // jump to checkCondition

            il.Emit(OpCodes.Br, targetLoopState.checkCondLabel);

            // mark next instruction as loop start

            il.MarkLabel(targetLoopState.loopLabel);

            // check type

            if(first)
            {
                il.Emit(OpCodes.Ldsfld, graph.Model.EdgeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
                il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("type", BindingFlags.Instance | BindingFlags.Public));
                il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
                il.Emit(OpCodes.Ldelem_I1);
                il.Emit(OpCodes.Brfalse, targetLoopState.continueLabel);
            }
            else
            {
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldsfld");
                il.Emit(OpCodes.Ldsfld, graph.Model.EdgeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldloc curPos = ");
                //il.EmitWriteLine(targetLoopState.curPos);
                il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldfld curPos.type");
                il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("type", BindingFlags.Instance | BindingFlags.Public));
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldsfld curPos.type.typeID");
                il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
                //il.EmitWriteLine("!1. ExtendInOut check type: Ldelem_I1");
                il.Emit(OpCodes.Ldelem_I1);
                //il.Emit(OpCodes.Pop);
                //il.EmitWriteLine("!1. ExtendInOut before Brfalse");
                il.Emit(OpCodes.Brfalse, targetLoopState.continueLabel);
                //il.EmitWriteLine("!1. ExtendInOut after Brfalse");
            }

            if(isIncoming)
            {
                if(edge.Target.PatternEdgeSource.Visited)   // check op?
                {
                    il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                    il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("source"));
                    il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Target.PatternEdgeSource.Name]);
                    il.Emit(OpCodes.Bne_Un, targetLoopState.continueLabel);
                }
            }
            else
            {
                if(edge.Target.PatternEdgeTarget.Visited)   // check op?
                {
                    il.Emit(OpCodes.Ldloc, targetLoopState.curPos);
                    il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("target"));
                    il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Target.PatternEdgeTarget.Name]);
                    il.Emit(OpCodes.Bne_Un, targetLoopState.continueLabel);
                }
            }
            targetLoopState.loopType = isIncoming ? LoopType.Incoming : LoopType.Outgoing;
            targetLoopState.checkVar = targetListState.listHeadVar;
            return targetLoopState;
        }

        public static void ShowEdge(LGSPEdge edge)
        {
            LGSPEdge innext = edge.inNode.next;
            LGSPEdge outnext = edge.outNode.next;

            Console.WriteLine("ShowEdge: edge = {0}  innext = {1}  outnext = {2}", edge, innext, outnext);
        }

        private void EmitExtendInOutTail(LoopState loop, ILGenerator il)
        {
            il.MarkLabel(loop.continueLabel);
            //il.EmitWriteLine("ExtendInOutTail continueLabel \"" + loop.elemName + "\"");
            //            il.Emit(OpCodes.Ldloc, loop.curPos);
            //            il.Emit(OpCodes.Call, typeof(LGSPActions).GetMethod("AddFirst", BindingFlags.Static | BindingFlags.Public));

            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldflda, typeof(LGSPEdge).GetField((loop.loopType == LoopType.Incoming) ? "inNode" : "outNode",
                BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField("next"));
            il.Emit(OpCodes.Stloc, loop.curPos);

            //il.EmitWriteLine("ExtendInOutTail curPos set to ");
            //il.EmitWriteLine(loop.curPos);

            //il.EmitWriteLine("ExtendInOutTail before checkCondLabel");
            il.MarkLabel(loop.checkCondLabel);
            il.Emit(OpCodes.Ldloc, loop.curPos);
            il.Emit(OpCodes.Ldloc, loop.checkVar);
            //il.EmitWriteLine("ExtendInOutTail before Bne_Un");
            il.Emit(OpCodes.Bne_Un, loop.loopLabel);
            //il.EmitWriteLine("ExtendInOutTail after Bne_Un");
        }

        // edge.source = graph edge, edge.target = graph node
        private void EmitImplicit(SearchPlanEdge edge, Dictionary<String, LoopState> loopElemNameMap, Dictionary<string, LocalBuilder> varElemNameMap, ILGenerator il)
        {
            //il.EmitWriteLine("Entered EmitImplicit");

#if OLDVERSION
            // begin of type check
            il.Emit(OpCodes.Ldsfld, graph.Model.NodeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));

            // elem = edge.<source/target>

            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Source.Name]);
            il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField((edge.Type == PlanEdgeType.ImplicitSource) ? "source" : "target"));
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Stloc, varElemNameMap[edge.Target.Name]);

            //il.EmitWriteLine("Implicit elem \"" + edge.Target.Name + "\" set to ");
            //il.EmitWriteLine(varElemNameMap[edge.Target.Name]);

            // check type

/*            FieldInfo[] fields = typeof(LGSPNode).GetFields(BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("EmitImplicit: fields of LGSPNode:");
            foreach(FieldInfo field in fields)
            {
                Console.WriteLine(field.Name);
            }*/
            il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("type", BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
            il.Emit(OpCodes.Ldelem_I1);
            il.Emit(OpCodes.Brfalse, loopElemNameMap[edge.Source.Name].continueLabel);
#else
            // elem = edge.<source/target>

            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Source.Name]);
            il.Emit(OpCodes.Ldfld, typeof(LGSPEdge).GetField((edge.Type == PlanEdgeType.ImplicitSource) ? "source" : "target"));
            il.Emit(OpCodes.Stloc, varElemNameMap[edge.Target.Name]);

            //il.EmitWriteLine("Implicit elem \"" + edge.Target.Name + "\" set to ");
            //il.EmitWriteLine(varElemNameMap[edge.Target.Name]);

            // check type

            il.Emit(OpCodes.Ldsfld, graph.Model.NodeModel.TypeTypes[edge.Target.TypeID].GetField("isMyType",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
            il.Emit(OpCodes.Ldloc, varElemNameMap[edge.Target.Name]);
            il.Emit(OpCodes.Ldfld, typeof(LGSPNode).GetField("type", BindingFlags.Instance | BindingFlags.Public));
            il.Emit(OpCodes.Ldfld, typeof(ITypeFramework).GetField("typeID"));
            il.Emit(OpCodes.Ldelem_I1);
            il.Emit(OpCodes.Brfalse, loopElemNameMap[edge.Source.Name].continueLabel);
#endif
        }

        private void EmitLoadConst(ILGenerator il, int val)
        {
            switch(val)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }
            if(val >= -128 && val <= 127)
                il.Emit(OpCodes.Ldc_I4_S, (sbyte) val);
            else
                il.Emit(OpCodes.Ldc_I4, val);
        }
#endif //CSHARPCODE_VERSION2

        //  generated code for an operation looks like
        //  front (match)
        //  map (the mapping graph element -> pattern element saved within the graph itself (for isomorphism checks))
        //  recursive insert of code for further operations
        //beforeunmaplabel: // jump here at mismatch after mapping was written, in order to unmap
        //  unmap (undo the mapping)
        //afterunmaplabel: // jump here at mismatch before mapping was written, no unmapping necessary
        //  tail (for closing loops)

        /// <summary>
        /// Generates the source code of the matcher function for the given scheduled search plan
        /// </summary>
        public String GenerateMatcherSourceCode(ScheduledSearchPlan scheduledSearchPlan,
            String actionName, LGSPRulePattern rulePattern)
        {
            if(DumpSearchPlan)
                DumpScheduledSearchPlan(scheduledSearchPlan, actionName);

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            sourceCode.Indent();
            sourceCode.Indent();

#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

#if PRODUCE_UNSAFE_MATCHERS
            sourceCode.AppendFront("unsafe public Matches myMatch(BaseGraph graph, int maxMatches, IGraphElement[] parameters)\n");
#else
            sourceCode.AppendFront("public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)\n");
#endif
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

/*            // [0] Matches matches = new Matches(this);

            sourceCode.AppendFront("LGSPMatches matches = new LGSPMatches(this);\n");*/
            sourceCode.AppendFront("matches.matches.Clear();\n");

            LinkedList<OperationState> operationStack = new LinkedList<OperationState>();

            OperationState dummyOp = new OperationState((SearchOperationType) (-1), null, "", null);
            dummyOp.ContinueBeforeUnmapLabel = "returnLabel";
            operationStack.AddFirst(dummyOp);

            // iterate over all operations in scheduled search plan
            // for every operation emit front of code and remember tail as operation state to emit later on
            foreach(SearchOperation op in scheduledSearchPlan.Operations)
            {
                OperationState lastOpState = operationStack.First.Value;
                switch(op.Type)
                {
                    case SearchOperationType.MaybePreset:
                        operationStack.AddFirst(EmitMaybePresetFront((SearchPlanNode) op.Element, rulePattern, lastOpState, sourceCode));
                        break;

                    case SearchOperationType.Lookup:
                        operationStack.AddFirst(EmitLookupFront((SearchPlanNode) op.Element, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                        operationStack.AddFirst(EmitImplicit((SearchPlanEdgeNode) op.SourceSPNode, (SearchPlanNodeNode) op.Element,
                            op.Type, rulePattern, lastOpState, sourceCode));
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        operationStack.AddFirst(EmitExtendFront((SearchPlanNodeNode) op.SourceSPNode, (SearchPlanEdgeNode) op.Element,
                            op.Type, rulePattern, sourceCode));
                        break;

                    case SearchOperationType.NegativePattern:
                        EmitNegativePattern((ScheduledSearchPlan) op.Element, rulePattern, lastOpState, sourceCode);
                        break;

                    case SearchOperationType.Condition:
                        EmitCondition((Condition) op.Element, rulePattern.GetType().Name, lastOpState, sourceCode);
                        break;
                }
                // mark element as visited if it was visitable (a SearchPlanNode)
                if(op.Type != SearchOperationType.NegativePattern && op.Type != SearchOperationType.Condition)
                {
                    ((SearchPlanNode) op.Element).Visited = true;
                    operationStack.First.Value.Operation = op;
                }

                // emit isomorphism checks
                if(op.Isomorphy.MustCheckMapped)
                {
#if OLDMAPPEDFIELDS
                    if(op.Isomorphy.HomomorphicID != 0)
                    {
                        sourceCode.AppendFrontFormat("if({0}.mappedTo != 0 && {0}.mappedTo != {1})",
                            operationStack.First.Value.CurPosVar, op.Isomorphy.HomomorphicID);
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("if({0}.mappedTo != 0)", operationStack.First.Value.CurPosVar);
                    }
#else
                    sourceCode.AppendFrontFormat("if({0}.isMapped", operationStack.First.Value.CurPosVar);
                    if(op.Isomorphy.HomomorphicID != 0)
                        EmitHomomorphicCheck((SearchPlanNode) op.Element, rulePattern.patternGraph, operationStack, sourceCode);
                    sourceCode.Append(")");
#endif
                    sourceCode.Append(" goto " + operationStack.First.Value.ContinueAfterUnmapLabel + ";\n");
                    operationStack.First.Value.ContinueAfterUnmapLabelUsed = true;
                }
                if(op.Isomorphy.MustSetMapped)    // can be only true, if op.Type != SearchOperationType.NegativePattern
                {                       // && op.Type != SearchOperationType.Condition
                    int mapid = op.Isomorphy.HomomorphicID;
                    if(mapid == 0) mapid = ((SearchPlanNode) op.Element).ElementID;
#if OLDMAPPEDFIELDS
                    else sourceCode.AppendFrontFormat("int {0}_prevMappedTo = {0}.mappedTo;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.mappedTo = {1};\n", operationStack.First.Value.CurPosVar, mapid);
#else
                    else sourceCode.AppendFrontFormat("bool {0}_prevIsMapped = {0}.isMapped;\n", operationStack.First.Value.CurPosVar);
                    sourceCode.AppendFrontFormat("{0}.isMapped = true;\n", operationStack.First.Value.CurPosVar);
#endif
                    operationStack.First.Value.MappedVar = operationStack.First.Value.CurPosVar;
                }
            }

            // emit match object building code
            sourceCode.AppendFront("LGSPMatch match = matchesList.GetNewMatch();\n");
            PatternGraph patternGraph = (PatternGraph) rulePattern.PatternGraph;
            for(int i = 0; i < patternGraph.nodes.Length; i++)
            {
                PatternNode patNode = patternGraph.nodes[i];
                sourceCode.AppendFront("match.nodes[" + i + "] = node_cur_" + patNode.Name + ";\n");
            }
            for(int i = 0; i < patternGraph.edges.Length; i++)
            {
                PatternEdge patEdge = patternGraph.edges[i];
                sourceCode.AppendFront("match.edges[" + i + "] = edge_cur_" + patEdge.Name + ";\n");
            }
            sourceCode.AppendFront("matchesList.CommitMatch();\n");

            // emit match process aborting code
            sourceCode.AppendFront("if(maxMatches > 0 && matchesList.Count >= maxMatches)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit unmapping code for abort case
            foreach(OperationState opState in operationStack)
            {
                if(opState.MappedVar != null)
                {
#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.mappedTo = {0}_prevMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.mappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isMapped = {0}_prevIsMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isMapped = false;\n", opState.MappedVar);
#endif
                }
            }

#if !NO_ADJUST_LIST_HEADS
            // emit code to adjust list heads for abort case
            // (set list entry point to element after last element found)
            // TODO: avoid adjusting list heads twice for lookups of same type
            for(LinkedListNode<OperationState> opStackNode = operationStack.Last; opStackNode != null; opStackNode = opStackNode.Previous)
            {
                OperationState opState = opStackNode.Value;
                if(opState.ListVar != null && opState.OperationType != SearchOperationType.MaybePreset)
                {
                    if(opState.OperationType == SearchOperationType.Incoming)
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n", opState.ListVar, opState.CurPosVar);
                    else if(opState.OperationType == SearchOperationType.Outgoing)
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n", opState.ListVar, opState.CurPosVar);
                    else
                        sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n", opState.CurPosVar);
//                        sourceCode.AppendFrontFormat("{0}.MoveHeadAfter({1});\n", opState.ListVar, opState.CurPosVar);
                }
            }
#endif
            sourceCode.AppendFront("return matches;\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // emit unmapping code and tail of operation for operations remembered
            foreach(OperationState opState in operationStack)
            {
                if(opState.ContinueBeforeUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueBeforeUnmapLabel);
                if(opState.MappedVar != null)
                {
#if OLDMAPPEDFIELDS
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.mappedTo = {0}_prevMappedTo;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.mappedTo = 0;\n", opState.MappedVar);
#else
                    if(opState.Operation.Isomorphy.HomomorphicID != 0)
                        sourceCode.AppendFrontFormat("{0}.isMapped = {0}_prevIsMapped;\n", opState.MappedVar);
                    else
                        sourceCode.AppendFrontFormat("{0}.isMapped = false;\n", opState.MappedVar);
#endif
                }
                if(opState.ContinueAfterUnmapLabelUsed)
                    sourceCode.AppendFormat("{0}:;\n", opState.ContinueAfterUnmapLabel);
                switch(opState.OperationType)
                {
                    case SearchOperationType.MaybePreset:
                        EmitMaybePresetTail(opState, sourceCode);
                        break;
                    case SearchOperationType.Lookup:
                        EmitLookupTail(opState, sourceCode);
                        break;

                    case SearchOperationType.Incoming:
                    case SearchOperationType.Outgoing:
                        EmitExtendTail(opState, sourceCode);
                        break;
                }
            }

            sourceCode.AppendFront("return matches;\n        }\n    }\n");

//            int compileGenMatcher = Environment.TickCount;
//            long genSourceTicks = startGenMatcher.ElapsedTicks;

            return sourceCode.ToString();
        }

        /// <summary>
        /// Generates an LGSPAction object for the given scheduled search plan.
        /// </summary>
        /// <param name="action">Needed for the rule pattern and the name</param>
        /// <param name="sourceOutputFilename">null if no output file needed</param>
        public LGSPAction GenerateMatcher(ScheduledSearchPlan scheduledSearchPlan, LGSPAction action,
            String modelAssemblyLocation, String actionAssemblyLocation, String sourceOutputFilename)
        {
            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyLocation);
            compParams.ReferencedAssemblies.Add(actionAssemblyLocation);

            compParams.GenerateInMemory = true;
#if PRODUCE_UNSAFE_MATCHERS
            compParams.CompilerOptions = "/optimize /unsafe";
#else
            compParams.CompilerOptions = "/optimize";
#endif

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);

            // generate class setup
            sourceCode.Append("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + model.GetType().Namespace + ";\nusing " + action.RulePattern.GetType().Namespace + ";\n\n"
                + "namespace de.unika.ipd.grGen.lgspActions\n{\n    public class DynAction_" + action.Name + " : LGSPAction\n    {\n"
                + "        public DynAction_" + action.Name + "() { rulePattern = " + action.RulePattern.GetType().Name
                + ".Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, " + action.RulePattern.PatternGraph.Nodes.Length
                + ", " + action.RulePattern.PatternGraph.Edges.Length + "); matchesList = matches.matches; }\n"
                + "        public override string Name { get { return \"" + action.Name + "\"; } }\n"
                + "        private LGSPMatches matches;\n"
                + "        private LGSPMatchesList matchesList;\n");

            // generate the matching function
            sourceCode.Append(GenerateMatcherSourceCode(scheduledSearchPlan, action.Name, action.rulePattern));

            // close namespace   TODO: GenerateMatcherSourceCode should not close class
            sourceCode.Append("}");

            // write generated source to file if requested
            if(sourceOutputFilename != null)
            {
                StreamWriter writer = new StreamWriter(sourceOutputFilename);
                writer.Write(sourceCode.ToString());
                writer.Close();
            }
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

        public LGSPAction[] GenerateSearchPlans(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName,
            params LGSPAction[] actions)
        {
            if(actions.Length == 0) throw new ArgumentException("No actions provided!");

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            sourceCode.Append("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + model.GetType().Namespace + ";\nusing " + actions[0].RulePattern.GetType().Namespace + ";\n\n"
                + "namespace de.unika.ipd.grGen.lgspActions\n{\n");

            foreach(LGSPAction action in actions)
            {
                PlanGraph planGraph = GeneratePlanGraph(graph, (PatternGraph) action.RulePattern.PatternGraph, false);
                MarkMinimumSpanningArborescence(planGraph, action.Name);
                SearchPlanGraph searchPlanGraph = GenerateSearchPlanGraph(planGraph);

                SearchPlanGraph[] negSearchPlanGraphs = new SearchPlanGraph[action.RulePattern.NegativePatternGraphs.Length];
                for(int i = 0; i < action.RulePattern.NegativePatternGraphs.Length; i++)
                {
                    PlanGraph negPlanGraph = GeneratePlanGraph(graph, (PatternGraph) action.RulePattern.NegativePatternGraphs[i], true);
                    MarkMinimumSpanningArborescence(negPlanGraph, action.Name + "_neg_" + (i + 1));
                    negSearchPlanGraphs[i] = GenerateSearchPlanGraph(negPlanGraph);
                }

                ScheduledSearchPlan scheduledSearchPlan = ScheduleSearchPlan(searchPlanGraph, negSearchPlanGraphs);

                CalculateNeededMaps(scheduledSearchPlan);

                sourceCode.Append("    public class DynAction_" + action.Name + " : LGSPAction\n    {\n"
                    + "        public DynAction_" + action.Name + "() { rulePattern = "
                    + action.RulePattern.GetType().Name + ".Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, "
                    + action.RulePattern.PatternGraph.Nodes.Length + ", " + action.RulePattern.PatternGraph.Edges.Length + "); "
                    + "matchesList = matches.matches; }\n"
                    + "        public override string Name { get { return \"" + action.Name + "\"; } }\n"
                    + "        private LGSPMatches matches;\n"
                    + "        private LGSPMatchesList matchesList;\n");

                sourceCode.Append(GenerateMatcherSourceCode(scheduledSearchPlan, action.Name, action.rulePattern));
            }
            sourceCode.Append("}");

            if(DumpDynSourceCode)
            {
                using(StreamWriter writer = new StreamWriter("dynamic_" + actions[0].Name + ".cs"))
                    writer.Write(sourceCode.ToString());
            }

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyName);
            compParams.ReferencedAssemblies.Add(actionsAssemblyName);

            compParams.GenerateInMemory = true;
#if PRODUCE_UNSAFE_MATCHERS
            compParams.CompilerOptions = "/optimize /unsafe";
#else
            compParams.CompilerOptions = "/optimize";
#endif

            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                throw new ArgumentException("Internal error: Illegal dynamic C# source code produced: " + errorMsg);
            }

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

        public LGSPAction GenerateSearchPlan(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, LGSPAction action)
        {
            return GenerateSearchPlans(graph, modelAssemblyName, actionsAssemblyName, action)[0];
        }
    }
}
