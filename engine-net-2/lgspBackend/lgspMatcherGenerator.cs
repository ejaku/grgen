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
        /// Generate plan graph for given pattern graph with costs from the analyzed host graph.
        /// Plan graph contains nodes representing the pattern elements (nodes and edges)
        /// and edges representing the matching operations to get the elements by.
        /// Edges in plan graph are given in the nodes by incoming list, as needed for MSA computation.
        /// </summary>
        /// <param name="graph">The host graph to optimize the matcher program for, 
        /// providing statistical information about its structure </param>
        public PlanGraph GeneratePlanGraph(LGSPGraph graph, PatternGraph patternGraph, bool negativePatternGraph, bool isSubpattern)
        {
            /// 
            /// If you change this method, chances are high you also want to change GenerateStaticPlanGraph in LGSPGrGen
            /// look there for version without ifdef junk
            /// todo: unify it with GenerateStaticPlanGraph in LGSPGrGen
            /// 

            /// Create root node
            /// Create plan graph nodes for all pattern graph nodes and all pattern graph edges
            /// Create "lookup" plan graph edge from root node to each plan graph node
            /// Create "implicit source" plan graph edge from each plan graph node originating with a pattern edge 
            ///     to the plan graph node created by the source node of the pattern graph edge
            /// Create "implicit target" plan graph edge from each plan graph node originating with a pattern edge 
            ///     to the plan graph node created by the target node of the pattern graph edge
            /// Create "incoming" plan graph edge from each plan graph node originating with a pattern node
            ///     to a plan graph node created by one of the incoming edges of the pattern node
            /// Create "outgoing" plan graph edge from each plan graph node originating with a pattern node
            ///     to a plan graph node created by one of the outgoing edges of the pattern node

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
                if(node.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.PatPreset : SearchOperationType.MaybePreset;
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
                planNodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                planEdges.Add(rootToNodePlanEdge);
                planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);

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
                if(edge.PatternElementType == PatternElementType.Preset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.PatPreset : SearchOperationType.MaybePreset;
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
                planNodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset,
                    edge.source!=null ? edge.source.TempPlanMapping : null,
                    edge.target!=null ? edge.target.TempPlanMapping : null);

                PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                planEdges.Add(rootToNodePlanEdge);
                planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
#else
                SearchOperationType searchOperationType = SearchOperationType.Lookup; // lookup as dummy
                if(edge.PatternElementType == PatternElementType.Preset)
                {
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.PatPreset : SearchOperationType.MaybePreset;
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
                    edge.source!=null ? edge.source.TempPlanMapping : null,
                    edge.target!=null ? edge.target.TempPlanMapping : null);
                if(isPreset)
                {
                    PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], 0);
                    planEdges.Add(rootToNodePlanEdge);
                    planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
                }
#endif
                // only add implicit source operation if edge source is needed and the edge source is not a preset node
                if(edge.source != null && !edge.source.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implSrcPlanEdge = new PlanEdge(SearchOperationType.ImplicitSource, planNodes[nodesIndex], edge.source.TempPlanMapping, 0);
#else
                    PlanEdge implSrcPlanEdge = new PlanEdge(SearchOperationType.ImplicitSource, planNodes[nodesIndex], edge.source.TempPlanMapping, 1);
#endif
                    planEdges.Add(implSrcPlanEdge);
                    edge.source.TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node
                if(edge.target != null && !edge.target.TempPlanMapping.IsPreset)
                {
#if OPCOST_WITH_GEO_MEAN 
                    PlanEdge implTgtPlanEdge = new PlanEdge(SearchOperationType.ImplicitTarget, planNodes[nodesIndex], edge.target.TempPlanMapping, 0);
#else
                    PlanEdge implTgtPlanEdge = new PlanEdge(SearchOperationType.ImplicitTarget, planNodes[nodesIndex], edge.target.TempPlanMapping, 1);
#endif
                    planEdges.Add(implTgtPlanEdge);
                    edge.target.TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
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
                        PlanEdge outPlanEdge = new PlanEdge(SearchOperationType.Outgoing, edge.source.TempPlanMapping, planNodes[nodesIndex], normCost);
                        planEdges.Add(outPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(outPlanEdge);
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
                        PlanEdge inPlanEdge = new PlanEdge(SearchOperationType.Incoming, edge.target.TempPlanMapping, planNodes[nodesIndex], revCost);
                        planEdges.Add(inPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(inPlanEdge);
                    }
                }

                ++nodesIndex;
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray(), patternGraph);
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
                case SearchOperationType.PatPreset: typeStr = "pp"; break;
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
                case SearchOperationType.PatPreset: typeStr = "pp"; break;
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
                case SearchOperationType.PatPreset: typeStr = "pp"; break;
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
                        searchPlanEdgeNode.PatternEdgeSource.OutgoingPatternEdges.AddLast(searchPlanEdgeNode);
                    }
                    if(planEdge.Target.PatternEdgeTarget != null 
                        && planToSearchPlanNode.TryGetValue(planEdge.Target.PatternEdgeTarget, out patElem))
                    {
                        searchPlanEdgeNode.PatternEdgeTarget = (SearchPlanNodeNode) patElem;
                        searchPlanEdgeNode.PatternEdgeTarget.IncomingPatternEdges.AddLast(searchPlanEdgeNode);
                    }
                }

                ++i;
            }

            return new SearchPlanGraph(searchPlanRoot, searchPlanNodes, searchPlanEdges, planGraph.PatternGraph);
        }

        /// <summary>
        /// Generates a scheduled search plan for a given search plan graph and optional negative search plan graphs
        /// </summary>
        /// <param name="negSpGraphs">a list - possibly empty - if a positive search plan graph is given,
        /// null if a negative search plan graph is scheduled </param>
        public ScheduledSearchPlan ScheduleSearchPlan(SearchPlanGraph spGraph, SearchPlanGraph[] negSpGraphs)
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

                    // note: here a normal preset is converted into a neg preset operation if in negative pattern
                    SearchOperation newOp = new SearchOperation(
                        negSpGraphs == null ? SearchOperationType.NegPreset : edge.Type,
                        edge.Target, spGraph.Root, 0);

                    operations.Add(newOp);
                }
            }

            // iterate over all reachable elements until the whole graph has been scheduled(/visited),
            // choose next cheapest operation, update the reachable elements and the search plan costs
            SearchPlanNode lastNode = spGraph.Root;
            for(int i = 0; i < spGraph.Nodes.Length - spGraph.NumPresetElements; ++i)
            {
                foreach (SearchPlanEdge edge in lastNode.OutgoingEdges)
                    if (!edge.Target.IsPreset) activeEdges.Add(edge);

                SearchPlanEdge minEdge = activeEdges.DequeueFirst();
                lastNode = minEdge.Target;

                SearchOperation newOp = new SearchOperation(minEdge.Type,
                    lastNode, minEdge.Source, minEdge.Cost);

                foreach(SearchOperation op in operations)
                    op.CostToEnd += minEdge.Cost;

                operations.Add(newOp);
            }

            // schedule negative search plans and insert them into the schedule
            InsertNegativesIntoSchedule(negSpGraphs, operations);

            // insert conditions into the schedule
            InsertConditionsIntoSchedule(spGraph.PatternGraph.Conditions, operations);

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            return new ScheduledSearchPlan(operations.ToArray(), spGraph.PatternGraph, cost);
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
            ///////////////////////////////////////////////////////////////////////////
            // first handle special cases pure homomorphy and pure isomorphy

            SearchPlanNode spn_j = (SearchPlanNode)ssp.Operations[j].Element;

            bool[] homToAll;

            if (spn_j.NodeType == PlanNodeType.Node) {
                homToAll = ssp.PatternGraph.HomomorphicToAllNodes;
            }
            else { // (spn_j.NodeType == PlanNodeType.Edge)
                homToAll = ssp.PatternGraph.HomomorphicToAllEdges;
            }

            if (homToAll[spn_j.ElementID - 1])
            {
                // operation is allowed to be homomorph with everything
                // no checks for isomorphy or restricted homomorphy needed
                return;
            }

            ///////////////////////////////////////////////////////////////////////////
            // no pure homomorphy or isomorphy, so we have restricted homomorphy
            // and need to inspect the operations before together with the homomorphy matrix 
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
            for (int i = 0; i < j; ++i)
            {
                // only check operations computing nodes or edges
                if (ssp.Operations[i].Type == SearchOperationType.Condition
                    || ssp.Operations[i].Type == SearchOperationType.NegativePattern)
                {
                    continue;
                }

                SearchPlanNode spn_i = (SearchPlanNode)ssp.Operations[i].Element;

                // don't compare nodes with edges
                if (spn_i.NodeType != spn_j.NodeType)
                {
                    continue;
                }

                // don't check homomorph elements
                if (hom[spn_i.ElementID - 1, spn_j.ElementID - 1])
                {
                    continue;
                }
                
                // find out whether element types are disjoint
                    // todo: optimization: check type constraints
                    // todo: why not check it before and combine it into hom-matrix?
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

                // the generated matcher code has to check 
                // that pattern element j doesn't get bound to the same graph element
                // the pattern element i is already bound to 
                if (ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst == null) {
                    ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst = new List<SearchPlanNode>();
                }
                ssp.Operations[j].Isomorphy.PatternElementsToCheckAgainst.Add(spn_i);

                // order operation to set the is-matched-bit after all checks succeeded
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
        }

        /// <summary>
        /// Returns the elements the given condition needs in order to be evaluated
        /// </summary>
        Dictionary<String, bool> GetNeededElements(Condition cond)
        {
            Dictionary<String, bool> neededElements = new Dictionary<string, bool>();

            foreach (String neededNode in cond.NeededNodes)
                neededElements[neededNode] = true;
            foreach (String neededEdge in cond.NeededEdges)
                neededElements[neededEdge] = true;

            return neededElements;
        }

        /// <summary>
        /// Calculates the elements the given search plan graph doesn't compute locally
        /// but expects to be preset from outwards  (non-recursive)
        /// </summary>
        Dictionary<String, bool> CalculateNeededElements(SearchPlanGraph spGraph)
        {
            Dictionary<String, bool> neededElements = new Dictionary<string, bool>();

            foreach (Condition cond in spGraph.PatternGraph.Conditions)
            {
                foreach (String neededNode in cond.NeededNodes)
                    neededElements[neededNode] = true;
                foreach (String neededEdge in cond.NeededEdges)
                    neededElements[neededEdge] = true;
            }

            foreach (SearchPlanNode node in spGraph.Nodes)
            {
                if (node.IsPreset)
                    neededElements[node.PatternElement.Name] = true;
            }

            return neededElements;
        }

        /// <summary>
        /// First schedules the negative search plan graphs given,
        /// then inserts them into the schedule given by the operations list at their best position 
        /// </summary>
        public void InsertNegativesIntoSchedule(SearchPlanGraph[] negSpGraphs, List<SearchOperation> operations)
        {
            // todo: erst implicit node, dann negative, auch wenn negative mit erstem implicit moeglich wird
            if (negSpGraphs == null)
            {
                return;
            }

            // schedule each negative search plan
            ScheduledSearchPlan[] negScheduledSPs = new ScheduledSearchPlan[negSpGraphs.Length];
            for (int i = 0; i < negSpGraphs.Length; ++i)
                negScheduledSPs[i] = ScheduleSearchPlan(negSpGraphs[i], null);

            // calculate needed elements of each negative search plan / search plan graph
            // (elements from the positive graph needed in order to execute the nac)
            Dictionary<String, bool>[] neededElements = new Dictionary<String, bool>[negSpGraphs.Length];
            for (int i = 0; i < negSpGraphs.Length; ++i)
                neededElements[i] = CalculateNeededElements(negSpGraphs[i]);

            // iterate over all negative scheduled search plans (TODO: order?)
            for (int i = 0; i < negSpGraphs.Length; ++i)
            {
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                // find best place in scheduled search plan for current negative pattern 
                // during search from end of schedule forward until the first element the negative pattern is dependent on is found
                for (int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if (op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.Condition) continue;

                    if (neededElements[i].ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name))
                        break;

                    if (negScheduledSPs[i].Cost <= op.CostToEnd)
                    {
                        // best fit as CostToEnd is monotonously growing towards operation[0]
                        bestFitIndex = j;
                        bestFitCostToEnd = op.CostToEnd;
                    }
                }

                // insert pattern at best position
                operations.Insert(bestFitIndex, new SearchOperation(SearchOperationType.NegativePattern,
                    negScheduledSPs[i], null, bestFitCostToEnd + negScheduledSPs[i].Cost));

                // update costs of operations before best position
                for (int j = 0; j < bestFitIndex; ++j)
                    operations[j].CostToEnd += negScheduledSPs[i].Cost;
            }
        }

        /// <summary>
        /// Inserts conditions into the schedule given by the operations list at their earliest possible position
        /// </summary>
        public void InsertConditionsIntoSchedule(Condition[] conditions, List<SearchOperation> operations)
        {
            // get needed (in order to evaluate it) elements of each condition 
            Dictionary<String, bool>[] neededElements = new Dictionary<String, bool>[conditions.Length];
            for (int i = 0; i < conditions.Length; ++i)
                neededElements[i] = GetNeededElements(conditions[i]);

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
                    if (op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.Condition) continue;

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
        /// Generates the source code of the matcher function for the given scheduled search plan
        /// new version building first abstract search program then search program code
        /// </summary>
        public String GenerateMatcherSourceCode(ScheduledSearchPlan scheduledSearchPlan,
            String actionName, LGSPRulePattern rulePattern)
        {
            // build pass: build nested program from scheduled search plan
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(
                scheduledSearchPlan, 
                "myMatch", null, null, rulePattern, model);

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
            if (!rulePattern.isSubpattern)
                searchProgramBuilder.BuildAddionalSearchSubprograms(
                    scheduledSearchPlan, searchProgram, rulePattern);

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter searchProgramCompleter = new SearchProgramCompleter();
            searchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);
            
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
        /// Generates file header for actions file into given source builer
        /// </summary>
        public void GenerateFileHeaderForActionsFile(SourceBuilder sb,
                String namespaceOfModel, String namespaceOfRulePatterns)
        {
            sb.Append("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + namespaceOfModel + ";\n"
                + "using " + namespaceOfRulePatterns + ";\n\n");
            sb.Append("namespace de.unika.ipd.grGen.lgspActions\n{\n");
        }

        /// <summary>
        /// Generates source code for matcher class into given source builder,
        /// class is generated here, the matcher method(s) itself must be handed in within matcherSourceCode.
        /// name is the prefix-less name of the rule pattern to generate the action for.
        /// isSubpattern says whether code for a rule/test or a subpattern is to be generated.
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClass(SourceBuilder sb, String matcherSourceCode,
                String name, LGSPRulePattern rulePattern, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;

            String namePrefix = rulePattern.isSubpattern ? "PatternAction_" : "Action_";
            namePrefix = (isInitialStatic ? "" : "Dyn") + namePrefix;
            String className = namePrefix + name;
  
            sb.Append("\tpublic class " + className + " : LGSPAction\n    {\n"
                + "\t\tpublic " + className + "() { rulePattern = " + rulePattern.GetType().Name + ".Instance;\n"
                + "\t\t\tDynamicMatch = myMatch; matches = new LGSPMatches(this, " + patternGraph.Nodes.Length + ", " + patternGraph.Edges.Length + "); matchesList = matches.matches;\n");
            for (int i = 0; i < patternGraph.embeddedGraphs.Length; ++i)
            {
                sb.Append("\t\t\tsubpatterns.Add(PatternAction_" + patternGraph.embeddedGraphs[i].ruleOfEmbeddedGraph.name + ".Instance);\n");
            }
            sb.Append("\t\t}\n\n"
                + "\t\tpublic override string Name { get { return \"" + name + "\"; } }\n"
                + "\t\tprivate LGSPMatches matches;\n"
                + "\t\tprivate LGSPMatchesList matchesList;\n\n");
            if(isInitialStatic)
            {
                sb.Append("\t\tpublic static LGSPAction Instance { get { return instance; } }\n"
                + "\t\tprivate static " + className + " instance = new " + className + "();\n\n");
            }
            sb.Append(matcherSourceCode);
            // TODO: GenerateMatcherSourceCode should not close class
        }

        /// <summary>
        /// Generates ScheduledSearchPlan needed for matcher code generation for action/subpattern-action compilation
        /// out of rule pattern and graph with analyze information
        /// </summary>
        public ScheduledSearchPlan GenerateScheduledSearchPlan(LGSPRulePattern rulePattern, LGSPGraph graph, bool isSubpattern)
        {
            PlanGraph planGraph = GeneratePlanGraph(graph, (PatternGraph)rulePattern.PatternGraph, false, isSubpattern);
            MarkMinimumSpanningArborescence(planGraph, rulePattern.name);
            SearchPlanGraph searchPlanGraph = GenerateSearchPlanGraph(planGraph);

            SearchPlanGraph[] negSearchPlanGraphs = new SearchPlanGraph[rulePattern.NegativePatternGraphs.Length];
            for (int i = 0; i < rulePattern.NegativePatternGraphs.Length; ++i)
            {
                PlanGraph negPlanGraph = GeneratePlanGraph(graph, (PatternGraph)rulePattern.NegativePatternGraphs[i], true, isSubpattern);
                MarkMinimumSpanningArborescence(negPlanGraph, rulePattern.name + "_neg_" + (i + 1));
                negSearchPlanGraphs[i] = GenerateSearchPlanGraph(negPlanGraph);
            }

            ScheduledSearchPlan scheduledSearchPlan = ScheduleSearchPlan(searchPlanGraph, negSearchPlanGraphs);
            AppendHomomorphyInformation(scheduledSearchPlan);

            return scheduledSearchPlan;
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
#if PRODUCE_UNSAFE_MATCHERS
            compParams.CompilerOptions = "/optimize /unsafe";
#else
            compParams.CompilerOptions = "/optimize";
#endif
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
            GenerateFileHeaderForActionsFile(sourceCode, model.GetType().Namespace, action.RulePattern.GetType().Namespace);

            // can't generate new subpattern matchers due to missing scheduled search plans for them / missing graph analyze data
            Debug.Assert(action.rulePattern.patternGraph.embeddedGraphs.Length == 0);

            String matcherSourceCode = GenerateMatcherSourceCode(
                scheduledSearchPlan, action.Name, action.rulePattern);

            GenerateMatcherClass(sourceCode, matcherSourceCode,
                action.Name, action.rulePattern, false);

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
        /// Computes all, by the given actions directly or indirectly used subpatterns.
        /// returned in set with rulepatterns of the subpatterns, implemented by abused dictionary as .net lacks a set datatype - argggh
        /// </summary>
        protected Dictionary<LGSPRulePattern, LGSPRulePattern> SubpatternsUsedByTheActions(LGSPAction[] actions)
        {
            // todo: use more efficient worklist algorithm
            Dictionary<LGSPRulePattern, LGSPRulePattern> subpatternRules = new Dictionary<LGSPRulePattern, LGSPRulePattern>();

            // all directly used subpatterns
            foreach (LGSPAction action in actions)
            {
                PatternGraphEmbedding[] embeddedGraphs = ((PatternGraphEmbedding[])
                    action.RulePattern.PatternGraph.EmbeddedGraphs);
                for (int i = 0; i < embeddedGraphs.Length; ++i)
                {
                    subpatternRules.Add(embeddedGraphs[i].ruleOfEmbeddedGraph, null);
                }
            }

            // transitive closure
            bool setChanged = subpatternRules.Count != 0;
            while (setChanged)
            {
                setChanged = false;
                foreach (KeyValuePair<LGSPRulePattern, LGSPRulePattern> subpatternRule in subpatternRules)
                {
                    PatternGraphEmbedding[] embedded = (PatternGraphEmbedding[])subpatternRule.Key.PatternGraph.EmbeddedGraphs;
                    for (int i = 0; i < embedded.Length; ++i)
                    {
                        if (!subpatternRules.ContainsKey(embedded[i].ruleOfEmbeddedGraph))
                        {
                            subpatternRules.Add(embedded[i].ruleOfEmbeddedGraph, null);
                            setChanged = true;
                        }
                    }

                    if (setChanged) break;
                }
            }

            return subpatternRules;
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
            GenerateFileHeaderForActionsFile(sourceCode, model.GetType().Namespace, actions[0].RulePattern.GetType().Namespace);

            // use domain of dictionary as set with rulepatterns of the subpatterns of the actions
            Dictionary<LGSPRulePattern, LGSPRulePattern> subpatternRules;
            subpatternRules = SubpatternsUsedByTheActions(actions);

            // generate code for subpatterns
            foreach (KeyValuePair<LGSPRulePattern, LGSPRulePattern> subpatternRule in subpatternRules)
            {
                LGSPRulePattern rulePattern = subpatternRule.Key;

                ScheduledSearchPlan scheduledSearchPlan = GenerateScheduledSearchPlan(
                    rulePattern, graph, true);

                String matcherSourceCode = GenerateMatcherSourceCode(
                    scheduledSearchPlan, rulePattern.name, rulePattern);

                GenerateMatcherClass(sourceCode, matcherSourceCode,
                    rulePattern.name, rulePattern, false);
            }

            // generate code for actions
            foreach(LGSPAction action in actions)
            {
                ScheduledSearchPlan scheduledSearchPlan = GenerateScheduledSearchPlan(
                    action.rulePattern, graph, false);

                String matcherSourceCode = GenerateMatcherSourceCode(
                    scheduledSearchPlan, action.rulePattern.name, action.rulePattern);

                GenerateMatcherClass(sourceCode, matcherSourceCode,
                    action.rulePattern.name, action.rulePattern, false);
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
