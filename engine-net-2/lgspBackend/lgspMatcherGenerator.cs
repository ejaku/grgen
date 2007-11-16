//#define PRODUCE_UNSAFE_MATCHERS // todo: what for ?
#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define NO_EDGE_LOOKUP
//#define NO_ADJUST_LIST_HEADS
//#define RANDOM_LOOKUP_LIST_START      // currently broken
//#define DUMP_SEARCHPROGRAMS
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
        /// Instantiates a new instance of LGSPMatcherGenerator with the given graph model.
        /// </summary>
        /// <param name="model">The model for which the matcher functions shall be generated.</param>
        public LGSPMatcherGenerator(IGraphModel model)
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
        
        //################################################################################
        // new source code generator - eja
        //################################################################################   
        // todo: erst implicit node, dann negative, auch wenn negative mit erstem implicit moeglich wird

        /// <summary>
        /// class of environment for build pass
        /// </summary>
        private struct EnvironmentForBuild
        {
            /// <summary>
            /// the scheduled search plan to build
            /// </summary>
            public ScheduledSearchPlan ScheduledSearchPlan;
            /// <summary>
            /// name of the rule pattern type of the rule pattern that is built
            /// </summary>
            public string NameOfRulePatternType;
            /// <summary>
            /// the innermost enclosing positive candidate iteration operation 
            /// at the current insertion point of the nested negative pattern
            /// not null if negative pattern is currently built, null otherwise
            /// </summary>
            public SearchProgramOperation EnclosingPositiveOperation;
            /// <summary>
            /// the rule pattern that is built 
            /// (it's pattern graph is needed for match object building)
            /// </summary>
            public LGSPRulePattern RulePattern;
        }
        /// <summary>
        /// environment is global variable only for build process
        /// </summary>
        private EnvironmentForBuild environmentForBuild;

        /// <summary>
        /// Builds search program from scheduled search plan
        /// </summary>
        /// <param name="scheduledSearchPlan"></param>
        /// <param name="nameOfRulePatternType"></param>
        /// <returns></returns>
        private SearchProgram BuildSearchProgram(
            ScheduledSearchPlan scheduledSearchPlan,
            string nameOfSearchProgram,
            List<string> parametersList,
            List<bool> parameterIsNodeList,
            LGSPRulePattern rulePattern)
        {
            string[] parameters = null;
            bool[] parameterIsNode = null;
            if (parametersList != null)
            {
                parameters = new string[parametersList.Count];
                parameterIsNode = new bool[parameterIsNodeList.Count];
                int i = 0;
                foreach(string p in parametersList)
                {
                    parameters[i] = p;
                    ++i;
                }
                i = 0;
                foreach(bool pis in parameterIsNodeList)
                {
                    parameterIsNode[i] = pis;
                    ++i;
                }
            }

            SearchProgram searchProgram = new SearchProgram(
                nameOfSearchProgram, parameters, parameterIsNode);
            searchProgram.OperationsList = new SearchProgramList(searchProgram);

            environmentForBuild.ScheduledSearchPlan = scheduledSearchPlan;
            environmentForBuild.NameOfRulePatternType = rulePattern.GetType().Name;
            environmentForBuild.EnclosingPositiveOperation = null;
            environmentForBuild.RulePattern = rulePattern;

            // start building with first operation in scheduled search plan
            BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                searchProgram.OperationsList);

            return searchProgram;
        }

        /// <summary>
        /// Builds search program operations from scheduled search plan operation.
        /// Decides which specialized build procedure is to be called.
        /// The specialized build procedure then calls this procedure again, 
        /// in order to process the next search plan operation.
        /// </summary>
        private SearchProgramOperation BuildScheduledSearchPlanOperationIntoSearchProgram(
            int indexOfScheduledSearchPlanOperationToBuild,
            SearchProgramOperation insertionPointWithinSearchProgram)
        {
            if (indexOfScheduledSearchPlanOperationToBuild >=
                environmentForBuild.ScheduledSearchPlan.Operations.Length)
            { // end of scheduled search plan reached, stop recursive iteration
                return buildMatchComplete(insertionPointWithinSearchProgram);
            }

            SearchOperation op = environmentForBuild.ScheduledSearchPlan.
                Operations[indexOfScheduledSearchPlanOperationToBuild];
        
            // for current scheduled search plan operation 
            // insert corresponding search program operations into search program
            switch (op.Type)
            {
                case SearchOperationType.Void:
                    return BuildScheduledSearchPlanOperationIntoSearchProgram(
                        indexOfScheduledSearchPlanOperationToBuild + 1,
                        insertionPointWithinSearchProgram);

                case SearchOperationType.MaybePreset:
                    return buildMaybePreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.NegPreset:
                    return buildNegPreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.Lookup:
                    return buildLookup(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.ImplicitSource:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        true);

                case SearchOperationType.ImplicitTarget:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        false);

                case SearchOperationType.Incoming:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        true);

                case SearchOperationType.Outgoing:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        false);

                case SearchOperationType.NegativePattern:
                    return buildNegative(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (ScheduledSearchPlan)op.Element);

                case SearchOperationType.Condition:
                    return buildCondition(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (Condition)op.Element);

                default:
                    Debug.Assert(false, "Unknown search operation");
                    return insertionPointWithinSearchProgram;
            }
        }

        /// <summary>
        /// Search program operations implementing the
        /// MaybePreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildMaybePreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            Debug.Assert(positive, "Positive maybe preset in negative search plan");

            // get candidate from inputs
            GetCandidateByDrawing fromInputs =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromInputs,
                    target.PatternElement.Name,
                    target.PatternElement.ParameterIndex.ToString(),
                    isNode);
            insertionPoint = insertionPoint.Append(fromInputs);

            // check whether candidate was preset (not null)
            // continues with missing preset searching and continuing search program on fail
            CheckCandidateForPreset checkPreset = new CheckCandidateForPreset(
                target.PatternElement.Name,
                isNode);
            insertionPoint = insertionPoint.Append(checkPreset);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex+1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// NegPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            Debug.Assert(!positive, "Negative preset in positive search plan");

            // check candidate for isomorphy 
            if (isomorphy.MustCheckMapped)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        false,
                        isNode);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped)
            {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        false,
                        isNode);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        false,
                        isNode);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Lookup search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildLookup(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex, 
            SearchPlanNode target, 
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            
            // decide on and insert operation determining type of candidate
            SearchProgramOperation continuationPointAfterTypeIteration;
            SearchProgramOperation insertionPointAfterTypeIteration = 
                decideOnAndInsertGetType(insertionPoint, target, 
                out continuationPointAfterTypeIteration);
            insertionPoint = insertionPointAfterTypeIteration;

#if RANDOM_LOOKUP_LIST_START
            // insert list heads randomization, thus randomized lookup
            RandomizeListHeads randomizeListHeads =
                new RandomizeListHeads(
                    RandomizeListHeadsTypes.GraphElements,
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(randomizeListHeads);
#endif

            // iterate available graph elements
            GetCandidateByIteration elementsIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.GraphElements,
                    target.PatternElement.Name,
                    isNode);
            SearchProgramOperation continuationPoint = 
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList = 
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check connectedness of candidate
            if (isNode) {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNode(
                    insertionPoint, (SearchPlanNodeNode)target, null);
            } else {
                bool dontcare = true||false;
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdge(
                    insertionPoint, (SearchPlanEdgeNode)target, null, dontcare);
            }

            // check candidate for isomorphy 
            if (isomorphy.MustCheckMapped) {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped) {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list at type iteration nesting level
            insertionPoint = continuationPoint;

            // everything nested within type iteration built by now
            // continue at the end of the list handed in
            if (insertionPointAfterTypeIteration != continuationPointAfterTypeIteration)
            {
                // if type was drawn then the if is not entered (insertion point==continuation point)
                // thus we continue at the continuation point of the candidate iteration
                // otherwise if type was iterated
                // we continue at the continuation point of the type iteration
                insertionPoint = continuationPointAfterTypeIteration;
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Implicit source|target search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildImplicit(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanEdgeNode source,
            SearchPlanNodeNode target,
            IsomorphyInformation isomorphy,
            bool getSource)
        {
            // get candidate = demanded node from edge
            GetCandidateByDrawing nodeFromEdge = 
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.NodeFromEdge,
                    target.PatternElement.Name,
                    model.NodeModel.Types[target.PatternElement.TypeID].Name,
                    source.PatternElement.Name,
                    getSource);
            insertionPoint = insertionPoint.Append(nodeFromEdge);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfNode(
                insertionPoint, target, source);

            // check candidate for isomorphy 
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            if (isomorphy.MustCheckMapped) {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped) {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Extend Incoming|Outgoing search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildIncident(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNodeNode source,
            SearchPlanEdgeNode target,
            IsomorphyInformation isomorphy,
            bool getIncoming)
        {
#if RANDOM_LOOKUP_LIST_START
            // insert list heads randomization, thus randomized extend
            RandomizeListHeads randomizeListHeads =
                new RandomizeListHeads(
                    RandomizeListHeadsTypes.IncidentEdges,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    getIncoming);
            insertionPoint = insertionPoint.Append(randomizeListHeads);
#endif

            // iterate available incident edges
            GetCandidateByIteration incidentIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.IncidentEdges,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    getIncoming);
            SearchProgramOperation continuationPoint = 
                insertionPoint.Append(incidentIteration);
            incidentIteration.NestedOperationsList =
                new SearchProgramList(incidentIteration);
            insertionPoint = incidentIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfEdge(
                insertionPoint, target, source, getIncoming);

            // check candidate for isomorphy 
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            if (isomorphy.MustCheckMapped) {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.HomomorphicID.ToString(),
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.MustSetMapped) {
                // no homomorphic id set -> plain isomorphy check, 
                // id unequal null and all homomorphic ids needed -> take element id
                // todo: is it ensured that homomorphic ids are uneqal element ids ?
                int homomorphicID = isomorphy.HomomorphicID != 0 ?
                    isomorphy.HomomorphicID : target.ElementID;
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        homomorphicID.ToString(),
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.MustSetMapped)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            // everything nested within incident iteration built by now -
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Negative search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegative(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            ScheduledSearchPlan negativeScheduledSearchPlan)
        {
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;
            Debug.Assert(positive, "Nested negative");

            // fill needed elements array for CheckPartialMatchByNegative
            int numberOfNeededElements = 0;
            foreach (SearchOperation op in negativeScheduledSearchPlan.Operations) {
                if (op.Type == SearchOperationType.NegPreset) {
                    ++numberOfNeededElements;
                }
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach (SearchOperation op in negativeScheduledSearchPlan.Operations) {
                if (op.Type == SearchOperationType.NegPreset) {
                    SearchPlanNode element = ((SearchPlanNode)op.Element);
                    neededElements[i] = element.PatternElement.Name;
                    ++i;
                }
            }

            CheckPartialMatchByNegative checkNegative =
               new CheckPartialMatchByNegative(neededElements);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(checkNegative);

            checkNegative.NestedOperationsList = 
                new SearchProgramList(checkNegative);
            insertionPoint = checkNegative.NestedOperationsList;

            environmentForBuild.EnclosingPositiveOperation =
                checkNegative.GetEnclosingOperation();
            ScheduledSearchPlan positiveScheduledSearchPlan =
                environmentForBuild.ScheduledSearchPlan;
            environmentForBuild.ScheduledSearchPlan = negativeScheduledSearchPlan;

            //---------------------------------------------------------------------------
            // build negative pattern
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);
            //---------------------------------------------------------------------------

            // negative pattern built by now
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;
            environmentForBuild.EnclosingPositiveOperation = null;
            environmentForBuild.ScheduledSearchPlan = positiveScheduledSearchPlan;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Condition search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildCondition(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            Condition condition)
        {
            // check condition with current partial match
            CheckPartialMatchByCondition checkCondition =
                new CheckPartialMatchByCondition(condition.ID.ToString(),
                    environmentForBuild.NameOfRulePatternType, 
                    condition.NeededNodes, 
                    condition.NeededEdges);
            insertionPoint = insertionPoint.Append(checkCondition);

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations completing the matching process
        /// after all pattern elements have been found 
        /// are created and inserted into the program
        /// </summary>
        private SearchProgramOperation buildMatchComplete(
            SearchProgramOperation insertionPoint)
        {
            bool positive = environmentForBuild.EnclosingPositiveOperation == null;

            if (positive)
            {
                // build the match complete operation signaling pattern was found
                PartialMatchCompletePositive matchComplete =
                    new PartialMatchCompletePositive();
                SearchProgramOperation continuationPoint =
                    insertionPoint.Append(matchComplete);
                matchComplete.MatchBuildingOperations =
                    new SearchProgramList(matchComplete);
                insertionPoint = matchComplete.MatchBuildingOperations;

                // fill the match object with the candidates 
                // which have passed all the checks for being a match
                LGSPRulePattern rulePattern = environmentForBuild.RulePattern;
                PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;
                for (int i = 0; i < patternGraph.nodes.Length; i++)
                {
                    PartialMatchCompleteBuildMatchObject buildMatch =
                        new PartialMatchCompleteBuildMatchObject(
                            patternGraph.nodes[i].Name,
                            i.ToString(),
                            true);
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
                for (int i = 0; i < patternGraph.edges.Length; i++)
                {
                    PartialMatchCompleteBuildMatchObject buildMatch =
                        new PartialMatchCompleteBuildMatchObject(
                            patternGraph.edges[i].Name,
                            i.ToString(),
                            false);
                    insertionPoint = insertionPoint.Append(buildMatch);
                }

                // check wheter to continue the matching process
                // or abort because the maximum desired number of maches was reached
                CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
#if NO_ADJUST_LIST_HEADS
                    new CheckContinueMatchingMaximumMatchesReached(false);
#else
                    new CheckContinueMatchingMaximumMatchesReached(true);
#endif
                insertionPoint = continuationPoint.Append(checkMaximumMatches);

                return insertionPoint;
            }
            else
            {
                // build the match complete operation signaling negative patter was found
                PartialMatchCompleteNegative matchComplete =
                    new PartialMatchCompleteNegative();
                insertionPoint = insertionPoint.Append(matchComplete);

                // abort the matching process
                CheckContinueMatchingFailed abortMatching =
                    new CheckContinueMatchingFailed();
                insertionPoint = insertionPoint.Append(abortMatching);

                return insertionPoint;
            }
        }

        /// <summary>
        /// Decides which get type operation to use and inserts it
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within type iteration was built
        /// if type drawing was sufficient, insertion point == continuation point
        /// </summary>
        private SearchProgramOperation decideOnAndInsertGetType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target,
            out SearchProgramOperation continuationPoint)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? (ITypeModel) model.NodeModel : (ITypeModel) model.EdgeModel;

            if (target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed
                if (typeModel.Types[target.PatternElement.TypeID].HasSubTypes)
                { // more than the type itself -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.AllCompatible,
                            target.PatternElement.Name,
                            typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                            isNode);
                    continuationPoint = insertionPoint.Append(typeIteration);
                    
                    typeIteration.NestedOperationsList = 
                        new SearchProgramList(typeIteration);
                    insertionPoint = typeIteration.NestedOperationsList;                    
                }
                else
                { // only the type itself -> no iteration needed
                    GetTypeByDrawing typeDrawing =
                        new GetTypeByDrawing(
                            target.PatternElement.Name,
                            target.PatternElement.TypeID.ToString(),
                            isNode);
                    insertionPoint = insertionPoint.Append(typeDrawing);
                    continuationPoint = insertionPoint;
                }
            }
            else //(target.PatternElement.AllowedTypes != null)
            { // the allowed types are given explicitely
                if (target.PatternElement.AllowedTypes.Length != 1)
                { // more than one allowed type -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.ExplicitelyGiven,
                            target.PatternElement.Name,
                            environmentForBuild.NameOfRulePatternType,
                            isNode);
                    continuationPoint = insertionPoint.Append(typeIteration);

                    typeIteration.NestedOperationsList =
                        new SearchProgramList(typeIteration);
                    insertionPoint = typeIteration.NestedOperationsList;
                }
                else
                { // only one allowed type -> no iteration needed
                    GetTypeByDrawing typeDrawing =
                        new GetTypeByDrawing(
                            target.PatternElement.Name,
                            target.PatternElement.AllowedTypes[0].TypeID.ToString(),
                            isNode);
                    insertionPoint = insertionPoint.Append(typeDrawing);
                    continuationPoint = insertionPoint;
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check type operation to build and inserts it into search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? (ITypeModel) model.NodeModel : (ITypeModel) model.EdgeModel;

            if (target.PatternElement.IsAllowedType != null)
            { // the allowed types are given by an array for checking against them
                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByIsAllowedType,
                        target.PatternElement.Name,
                        environmentForBuild.NameOfRulePatternType,
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);

                return insertionPoint;
            }

            if (target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed

                if (typeModel.Types[target.PatternElement.TypeID] == typeModel.RootType)
                { // every type matches the root type == element type -> no check needed
                    return insertionPoint;
                }

                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByIsMyType,
                        target.PatternElement.Name,
                        typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);
                
                return insertionPoint;
            }

            // the allowed types are given by allowed types array
            // if there are multiple ones, is allowed types must have been not null before
            Debug.Assert(target.PatternElement.AllowedTypes.Length > 1, "More than one allowed type");

            if (target.PatternElement.AllowedTypes.Length == 0) 
            { // no type allowed
                CheckCandidateFailed checkFailed = new CheckCandidateFailed();
                insertionPoint = insertionPoint.Append(checkFailed);
            }
            else // (target.PatternElement.AllowedTypes.Length == 1)
            { // only one type allowed
                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByAllowedTypes,
                        target.PatternElement.Name,
                        target.PatternElement.AllowedTypes[0].TypeID.ToString(),
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfNode(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            SearchPlanEdgeNode originatingEdge)
        {
            // check whether incoming/outgoing-edges of the candidate node 
            // are the same as the already found edges to which the node must be incident
            // only for the edges required by the pattern to be incident with the node
            // only if edge is already matched by now (signaled by visited)
            // only if the node was not taken from the given originating edge
            foreach (SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if (edge.Visited)
                {
                    if (edge != originatingEdge)
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                node.PatternElement.Name,
                                node.PatternElement.Name,
                                edge.PatternElement.Name,
                                true);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }
            foreach (SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if (edge.Visited)
                {
                    if (edge != originatingEdge)
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                node.PatternElement.Name,
                                node.PatternElement.Name,
                                edge.PatternElement.Name,
                                false);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdge(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode originatingNode,
            bool edgeIncomingAtOriginatingNode)
        {
            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            // don't need to, if the edge is not required by the pattern 
            //  to be incident to some given node
            // or that node is not matched by now (signaled by visited)
            // or if the edge was taken from the given originating node
            if (edge.PatternEdgeSource != null)
            {
                if (edge.PatternEdgeSource.Visited)
                {
                    if (!(!edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeSource == originatingNode))
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                edge.PatternElement.Name,
                                edge.PatternEdgeSource.PatternElement.Name,
                                edge.PatternElement.Name,
                                true);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            if (edge.PatternEdgeTarget != null)
            {
                if (edge.PatternEdgeTarget.Visited)
                {
                    if (!(edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeTarget == originatingNode))
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                edge.PatternElement.Name,
                                edge.PatternEdgeTarget.PatternElement.Name,
                                edge.PatternElement.Name,
                                false);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Create an extra search subprogram per MaybePreset operation.
        /// Created search programs are added to search program next field, forming list.
        /// Destroys the scheduled search program.
        /// </summary>
        private void BuildAddionalSearchSubprograms(ScheduledSearchPlan scheduledSearchPlan,
            SearchProgram searchProgram, LGSPRulePattern rulePattern)
        {
            // insertion point for next search subprogram
            SearchProgramOperation insertionPoint = searchProgram;
            // for stepwise buildup of parameter lists
            List<string> neededElementsInRemainderProgram = new List<string>();
            List<bool> neededElementInRemainderProgramIsNode = new List<bool>();

            foreach (SearchOperation op in scheduledSearchPlan.Operations)
            {
                switch(op.Type)
                {
                // candidates bound in the program up to the maybe preset operation
                // are handed in to the "lookup missing element and continue"
                // remainder search program as parameters 
                // - so they don't need to be searched any more
                case SearchOperationType.Lookup:
                case SearchOperationType.Outgoing:
                case SearchOperationType.Incoming:
                case SearchOperationType.ImplicitSource:
                case SearchOperationType.ImplicitTarget:
                    // don't search
                    op.Type = SearchOperationType.Void;
                    // make parameter in remainder program
                    SearchPlanNode element = (SearchPlanNode)op.Element;
                    neededElementsInRemainderProgram.Add(element.PatternElement.Name);
                    neededElementInRemainderProgramIsNode.Add(element.NodeType == PlanNodeType.Node);
                    break;
                // check operations were already executed in the calling program 
                // they don't need any treatement in the remainder search program
                case SearchOperationType.NegativePattern:
                case SearchOperationType.Condition:
                    op.Type = SearchOperationType.Void;
                    break;
                // the maybe preset operation splitting off an extra search program
                // to look the missing element up and continue within that 
                // remainder search program until the end (or the next split)
                // replace the maybe preset by a lookup within that program
                // and build the extra program (with the rest unmodified (yet))
                // insert the search program into the search program list
                case SearchOperationType.MaybePreset:
                    // change maybe preset on element which was not handed in  
                    // into lookup on element, then build search program with lookup
                    op.Type = SearchOperationType.Lookup;
                    element = (SearchPlanNode)op.Element;
                    SearchProgramOperation searchSubprogram =
                        BuildSearchProgram(
                            scheduledSearchPlan,
                            NamesOfEntities.MissingPresetHandlingMethod(element.PatternElement.Name),
                            neededElementsInRemainderProgram,
                            neededElementInRemainderProgramIsNode,
                            rulePattern);
                    insertionPoint = insertionPoint.Append(searchSubprogram);
                    // search calls to this new search program and complete arguments in
                    CompleteCallsToMissingPresetHandlingMethodInAllSearchPrograms(
                        searchProgram,
                        NamesOfEntities.MissingPresetHandlingMethod(element.PatternElement.Name),
                        neededElementsInRemainderProgram,
                        neededElementInRemainderProgramIsNode);
                    // make parameter in remainder program
                    neededElementsInRemainderProgram.Add(element.PatternElement.Name);
                    neededElementInRemainderProgramIsNode.Add(element.NodeType == PlanNodeType.Node);
                    // handled, now do same as with other candidate binding operations
                    op.Type = SearchOperationType.Void;
                    break;
                // operations which are not allowed in a positive scheduled search plan
                // after ssp creation and/or the buildup pass
                case SearchOperationType.Void:
                case SearchOperationType.NegPreset:
                default:
                    Debug.Assert(false, "At this pass/position not allowed search operation");
                    break;                    
                }
            }
        }

        /// <summary>
        /// Completes calls to missing preset handling methods in all search programs
        ///   (available at the time the functions is called)
        /// </summary>
        private void CompleteCallsToMissingPresetHandlingMethodInAllSearchPrograms(
            SearchProgram searchProgram,
            string nameOfMissingPresetHandlingMethod,
            List<string> arguments,
            List<bool> argumentIsNode)
        {
            /// Iterate all search programs 
            /// search for calls to missing preset handling methods 
            /// within check preset opertions which are the only ones utilizing them
            /// and complete them with the arguments given
            do
            {
                CompleteCallsToMissingPresetHandlingMethod(
                    searchProgram.GetNestedOperationsList(),
                    nameOfMissingPresetHandlingMethod,
                    arguments,
                    argumentIsNode);
                searchProgram = searchProgram.Next as SearchProgram;
            }
            while (searchProgram != null);
        }

        /// <summary>
        /// Completes calls to missing preset handling methods 
        /// by inserting the initially not given arguments
        /// </summary>
        private void CompleteCallsToMissingPresetHandlingMethod(
            SearchProgramOperation searchProgramOperation,
            string nameOfMissingPresetHandlingMethod,
            List<string> arguments,
            List<bool> argumentIsNode)
        {
            // complete calls with arguments 
            // find them in depth first search of search program
            while (searchProgramOperation != null)
            {
                if (searchProgramOperation is CheckCandidateForPreset)
                {
                    CheckCandidateForPreset checkPreset = 
                        (CheckCandidateForPreset) searchProgramOperation;
                    if (NamesOfEntities.MissingPresetHandlingMethod(checkPreset.PatternElementName)
                        == nameOfMissingPresetHandlingMethod)
                    {
                        checkPreset.CompleteWithArguments(arguments, argumentIsNode);
                    }
                }
                else if (searchProgramOperation.IsNestingOperation())
                { // depth first
                    CompleteCallsToMissingPresetHandlingMethod(
                        searchProgramOperation.GetNestedOperationsList(),
                        nameOfMissingPresetHandlingMethod, 
                        arguments,
                        argumentIsNode);
                }

                // breadth
                searchProgramOperation = searchProgramOperation.Next;
            }

        }

        /// <summary>
        /// Iterate all search programs to complete check operations within each one
        /// </summary>
        private void CompleteCheckOperationsInAllSearchPrograms(
            SearchProgram searchProgram)
        {
            do
            {
                CompleteCheckOperations(
                    searchProgram.GetNestedOperationsList(),
                    searchProgram, 
                    null);
                searchProgram = searchProgram.Next as SearchProgram;
            }
            while (searchProgram != null);
        }   

        /// <summary>
        /// Completes check operations in search program from given currentOperation on
        /// (taking borderlines set by enclosing search program and check negative into account)
        /// Completion:
        /// - determine continuation point
        /// - insert remove isomorphy opertions needed for continuing there
        /// - insert continuing operation itself
        /// </summary>
        private void CompleteCheckOperations(
            SearchProgramOperation currentOperation,
            SearchProgramOperation enclosingSearchProgram,
            CheckPartialMatchByNegative enclosingCheckNegative)
        {
            // mainly dispatching and iteration method, traverses search program
            // real completion done in MoveOutwardsAppendingRemoveIsomorphyAndJump
            // outermost operation for that function is computed here, regarding negative patterns

            // complete check operations by inserting failure code
            // find them in depth first search of search program
            while (currentOperation != null)
            {
                //////////////////////////////////////////////////////////
                if (currentOperation is CheckCandidate)
                //////////////////////////////////////////////////////////
                {
                    if (currentOperation is CheckCandidateForPreset)
                    {
                        // when the call to the preset handling returns
                        // it might be because it found all available matches from the given parameters on
                        //   then we want to continue the matching
                        // or it might be because it found the required number of matches
                        //   then we want to abort the matching process
                        // so we have to add an additional maximum matches check to the check preset
                        CheckCandidateForPreset checkPreset =
                            (CheckCandidateForPreset)currentOperation;
                        checkPreset.CheckFailedOperations = 
                            new SearchProgramList(checkPreset);
                        CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                            new CheckContinueMatchingMaximumMatchesReached(false);
                        checkPreset.CheckFailedOperations.Append(checkMaximumMatches);

                        string[] neededElementsForCheckOperation = new string[1];
                        neededElementsForCheckOperation[0] = checkPreset.PatternElementName;
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkPreset,
                            neededElementsForCheckOperation,
                            enclosingCheckNegative ?? enclosingSearchProgram);

                        // unlike all other check operations with their flat check failed code
                        // check preset has a further check operations nested within check failed code
                        // give it its special bit of attention here
                        CompleteCheckOperations(checkPreset.CheckFailedOperations,
                            enclosingSearchProgram,
                            enclosingCheckNegative);
                    }
                    else
                    {
                        CheckCandidate checkCandidate =
                            (CheckCandidate)currentOperation;
                        checkCandidate.CheckFailedOperations =
                            new SearchProgramList(checkCandidate);
                        string[] neededElementsForCheckOperation = new string[1];
                        neededElementsForCheckOperation[0] = checkCandidate.PatternElementName;
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkCandidate,
                            neededElementsForCheckOperation,
                            enclosingCheckNegative ?? enclosingSearchProgram);
                    }
                }
                //////////////////////////////////////////////////////////
                else if (currentOperation is CheckPartialMatch)
                //////////////////////////////////////////////////////////
                {
                    if (currentOperation is CheckPartialMatchByCondition)
                    {
                        CheckPartialMatchByCondition checkCondition = 
                            (CheckPartialMatchByCondition)currentOperation;
                        checkCondition.CheckFailedOperations =
                            new SearchProgramList(checkCondition);
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkCondition,
                            checkCondition.NeededElements,
                            enclosingCheckNegative ?? enclosingSearchProgram);
                    }
                    else if (currentOperation is CheckPartialMatchByNegative)
                    {
                        CheckPartialMatchByNegative checkNegative =
                            (CheckPartialMatchByNegative)currentOperation;

                        // ByNegative is handled in CheckContinueMatchingFailed 
                        // of the negative case - enter negative case
                        CompleteCheckOperations(checkNegative.NestedOperationsList,
                            enclosingCheckNegative ?? enclosingSearchProgram, 
                            checkNegative);
                    }
                    else
                    {
                        Debug.Assert(false, "unknown check partial match operation");
                    }

                }
                //////////////////////////////////////////////////////////
                else if (currentOperation is CheckContinueMatching)
                //////////////////////////////////////////////////////////
                {
                    if(currentOperation is CheckContinueMatchingMaximumMatchesReached)
                    {
                        CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                            (CheckContinueMatchingMaximumMatchesReached)currentOperation;
                        checkMaximumMatches.CheckFailedOperations =
                            new SearchProgramList(checkMaximumMatches);

                        if (checkMaximumMatches.ListHeadAdjustment)
                        {
                            MoveOutwardsAppendingListHeadAdjustment(checkMaximumMatches);
                        }

                        string[] neededElementsForCheckOperation = new string[0];
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkMaximumMatches,
                            neededElementsForCheckOperation,
                            enclosingSearchProgram);
                    }
                    else if (currentOperation is CheckContinueMatchingFailed)
                    {
                        CheckContinueMatchingFailed checkFailed =
                            (CheckContinueMatchingFailed)currentOperation;
                        checkFailed.CheckFailedOperations =
                            new SearchProgramList(checkFailed);
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkFailed,
                            enclosingCheckNegative.NeededElements,
                            enclosingSearchProgram);
                    }
                    else
                    {
                        Debug.Assert(false, "unknown check abort matching operation");
                    }
                }
                //////////////////////////////////////////////////////////
                else if (currentOperation.IsNestingOperation())
                //////////////////////////////////////////////////////////
                {
                    // depth first
                    CompleteCheckOperations(
                        currentOperation.GetNestedOperationsList(),
                        enclosingSearchProgram,
                        enclosingCheckNegative);
                }

                // breadth
                currentOperation = currentOperation.Next;
            }
        }

        /// <summary>
        /// "listentrick": append search program operations to adjust list heads
        /// i.e. set list entry point to element after last found,
        /// so that next searching starts there - preformance optimization
        /// (leave graph in the state of our last visit (searching it))
        /// </summary>
        private void MoveOutwardsAppendingListHeadAdjustment(
            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches)
        {
            // todo: avoid adjusting list heads twice for lookups of same type

            // insertion point for candidate failed operations
            SearchProgramOperation insertionPoint =
                checkMaximumMatches.CheckFailedOperations;
            SearchProgramOperation op = checkMaximumMatches;
            do
            {
                if(op is GetCandidateByIteration)
                {
                    GetCandidateByIteration candidateByIteration = 
                        op as GetCandidateByIteration;
                    if (candidateByIteration.Type==GetCandidateByIterationType.GraphElements)
                    {
                        AdjustListHeads adjustElements =
                            new AdjustListHeads(
                                AdjustListHeadsTypes.GraphElements,
                                candidateByIteration.PatternElementName,
                                candidateByIteration.IsNode);
                        insertionPoint = insertionPoint.Append(adjustElements);
                    }
                    else //candidateByIteration.Type==GetCandidateByIterationType.IncidentEdges
                    {
                        AdjustListHeads adjustIncident =
                            new AdjustListHeads(
                                AdjustListHeadsTypes.IncidentEdges,
                                candidateByIteration.PatternElementName,
                                candidateByIteration.StartingPointNodeName,
                                candidateByIteration.GetIncoming);
                        insertionPoint = insertionPoint.Append(adjustIncident);
                    }
                }

                op = op.Previous;
            }
            while(op!=null);
        }

        /// <summary>
        /// move outwards from check operation until operation to continue at is found
        /// appending remove isomorphy for isomorphy written on the way
        /// and final jump to operation to continue
        /// </summary>
        private void MoveOutwardsAppendingRemoveIsomorphyAndJump(
            CheckOperation checkOperation,
            string[] neededElementsForCheckOperation,
            SearchProgramOperation outermostOperation)
        {
            // insertion point for candidate failed operations
            SearchProgramOperation insertionPoint =
                checkOperation.CheckFailedOperations;
            while (insertionPoint.Next != null)
            {
                insertionPoint = insertionPoint.Next;
            }
            // currently focused operation on our way outwards
            SearchProgramOperation op = checkOperation;
            // move outwards until operation to continue at is found
            bool creationPointOfDominatingElementFound = false; 
            bool iterationReached = false;
            do
            {
                op = op.Previous;

                // insert code to clean up isomorphy information 
                // written in between the operation to continue and the check operation
                if (op is AcceptIntoPartialMatchWriteIsomorphy)
                {
                    AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                        op as AcceptIntoPartialMatchWriteIsomorphy;
                    WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                        new WithdrawFromPartialMatchRemoveIsomorphy(
                            writeIsomorphy.PatternElementName,
                            writeIsomorphy.Positive,
                            writeIsomorphy.IsNode);
                    insertionPoint = insertionPoint.Append(removeIsomorphy);
                }

                // determine operation to continue at
                // found by looking at the graph elements 
                // the check operation depends on / is dominated by
                // its the first element iteration on our way outwards the search program
                // after or at the point of a get element operation 
                // of some dominating element the check depends on
                // (or the outermost operation if no iteration is found until it is reached)
                if (op is GetCandidate)
                {
                    GetCandidate getCandidate = op as GetCandidate;
                    if (creationPointOfDominatingElementFound == false)
                    {
                        foreach (string dominating in neededElementsForCheckOperation)
                        {
                            if (getCandidate.PatternElementName == dominating)
                            {
                                creationPointOfDominatingElementFound = true;
                                iterationReached = false;
                                break;
                            }
                        }
                    }
                    if (op is GetCandidateByIteration)
                    {
                        iterationReached = true;
                    }
                }
            }
            while (!(creationPointOfDominatingElementFound && iterationReached)
                    && op != outermostOperation);
            SearchProgramOperation continuationPoint = op;

            // decide on type of continuing operation, then insert it

            // continue at top nesting level -> return
            while(!(op is SearchProgram))
            {
                op = op.Previous;
            }
            SearchProgram searchProgramRoot = op as SearchProgram;
            if (continuationPoint == searchProgramRoot)
            {
                ContinueOperation continueByReturn = 
                    new ContinueOperation(
                        ContinueOperationType.ByReturn,
                        !searchProgramRoot.IsSubprogram);
                insertionPoint.Append(continueByReturn);

                return;
            }
            
            // continue at directly enclosing nesting level -> continue
            op = checkOperation;
            do
            {
                op = op.Previous;
            }
            while (!op.IsNestingOperation());
            SearchProgramOperation directlyEnclosingOperation = op;
            if (continuationPoint == directlyEnclosingOperation
                // (check negative is enclosing, but not a loop, thus continue wouldn't work)
                && !(directlyEnclosingOperation is CheckPartialMatchByNegative))
            {
                ContinueOperation continueByContinue =
                    new ContinueOperation(ContinueOperationType.ByContinue);
                insertionPoint.Append(continueByContinue);

                return;
            }

            // otherwise -> goto label
            GotoLabel gotoLabel;

            // if our continuation point is a candidate iteration
            // -> append label at the end of the loop body of the candidate iteration loop 
            if (continuationPoint is GetCandidateByIteration)
            {
                GetCandidateByIteration candidateIteration =
                    continuationPoint as GetCandidateByIteration;
                op = candidateIteration.NestedOperationsList;
                while (op.Next != null)
                {
                    op = op.Next;
                }
                gotoLabel = new GotoLabel();
                op.Append(gotoLabel);
            }
            // otherwise our continuation point is a check negative operation
            // -> insert label directly after the check negative operation
            else
            {
                CheckPartialMatchByNegative checkNegative =
                    continuationPoint as CheckPartialMatchByNegative;
                gotoLabel = new GotoLabel();
                op.Insert(gotoLabel);
            }

            ContinueOperation continueByGoto =
                new ContinueOperation(
                    ContinueOperationType.ByGoto,
                    gotoLabel.LabelName); // ByGoto due to parameters
            insertionPoint.Append(continueByGoto);
        }

        /// <summary>
        /// Generates the source code of the matcher function for the given scheduled search plan
        /// new version building first abstract search program then search program code
        /// </summary>
        public String GenerateMatcherSourceCode(ScheduledSearchPlan scheduledSearchPlan,
            String actionName, LGSPRulePattern rulePattern)
        {
            // build pass: build nested program from scheduled search plan
            SearchProgram searchProgram = BuildSearchProgram(scheduledSearchPlan, 
                "myMatch", null, null, rulePattern);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(actionName + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // build additional: create extra search subprogram per MaybePreset operation
            // will be called when preset element is not available
            BuildAddionalSearchSubprograms(scheduledSearchPlan, 
                searchProgram, rulePattern);

            // complete pass: complete check operations in all search programs
            CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(actionName + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // emit pass: emit source code from search program 
            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            searchProgram.Emit(sourceCode);
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
