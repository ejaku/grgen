#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define NO_EDGE_LOOKUP
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
        public PlanGraph GeneratePlanGraph(LGSPGraph graph, PatternGraph patternGraph, bool negPatternGraph, bool isSubpattern)
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
                if (node.PointOfDefinition == null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.MaybePreset;
                }
                else if (node.PointOfDefinition != patternGraph)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif
                    isPreset = true;
                    searchOperationType = negPatternGraph ? SearchOperationType.NegPreset : SearchOperationType.SubPreset;
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
                if (edge.PointOfDefinition == null)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.MaybePreset;
                }
                else if (edge.PointOfDefinition != patternGraph)
                {
#if OPCOST_WITH_GEO_MEAN 
                    cost = 0;
#else
                    cost = 1;
#endif

                    isPreset = true;
                    searchOperationType = negPatternGraph ? SearchOperationType.NegPreset : SearchOperationType.SubPreset;
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

                PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                planEdges.Add(rootToNodePlanEdge);
                planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
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
                // only add implicit source operation if edge source is needed and the edge source is not a preset node
                if(patternGraph.GetSource(edge) != null && !patternGraph.GetSource(edge).TempPlanMapping.IsPreset)
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
                // only add implicit target operation if edge target is needed and the edge target is not a preset node
                if(patternGraph.GetTarget(edge) != null && !patternGraph.GetTarget(edge).TempPlanMapping.IsPreset)
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

                // edge must only be reachable from other nodes if it's not a preset
                if(!isPreset)
                {
                    // no outgoing if no source
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

                    // no incoming if no target
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
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
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
                case SearchOperationType.MaybePreset: typeStr = " p"; break;
                case SearchOperationType.NegPreset: typeStr = "np"; break;
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
                case SearchOperationType.Outgoing: typeStr = src.PatternElement.Name + "-" + tgt.PatternElement.Name + "->"; break;
                case SearchOperationType.Incoming: typeStr = src.PatternElement.Name + "<-" + tgt.PatternElement.Name + "-"; break;
                case SearchOperationType.Incident: typeStr = src.PatternElement.Name + "<-" + tgt.PatternElement.Name + "->"; break;
                case SearchOperationType.ImplicitSource: typeStr = "<-" + src.PatternElement.Name + "-" + tgt.PatternElement.Name; break;
                case SearchOperationType.ImplicitTarget: typeStr = "-" + src.PatternElement.Name + "->" + tgt.PatternElement.Name; break;
                case SearchOperationType.Implicit: typeStr = "<-" + src.PatternElement.Name + "->" + tgt.PatternElement.Name; break;
                case SearchOperationType.Lookup: typeStr = "*" + tgt.PatternElement.Name; break;
                case SearchOperationType.MaybePreset: typeStr = "p" + tgt.PatternElement.Name; break;
                case SearchOperationType.NegPreset: typeStr = "np" + tgt.PatternElement.Name; break;
                case SearchOperationType.SubPreset: typeStr = "sp"; break;
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
            PatternGraph patternGraph, bool isNegative)
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
                        isNegative ? SearchOperationType.NegPreset : edge.Type,
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
                // no checks for isomorphy or restricted homomorphy needed
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
        /// fill in globally homomorphic elements as exception to global isomorphy check
        /// </summary>
        public void FillInGlobalHomomorphyPatternElements(ScheduledSearchPlan ssp, int j)
        {
            SearchPlanNode spn_j = (SearchPlanNode)ssp.Operations[j].Element;

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
        /// Negative schedules are merged as an operation into their enclosing schedules,
        /// at a position determined by their costs but not before all of their needed elements were computed
        /// </summary>
        public void MergeNegativeSchedulesIntoPositiveSchedules(PatternGraph patternGraph)
        {
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                MergeNegativeSchedulesIntoPositiveSchedules(neg);
            }

            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    MergeNegativeSchedulesIntoPositiveSchedules(altCase);
                }
            }

            InsertNegativesIntoSchedule(patternGraph);
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
        /// Calculates the elements the given pattern graph and it's nested pattern graphs don't compute locally
        /// but expect to be preset from outwards
        /// </summary>
        public static void CalculateNeededElements(PatternGraph patternGraph,
            Dictionary<String, bool> neededNodes, Dictionary<String, bool> neededEdges)
        {
            // algorithm descends top down to the nested patterns,
            // computes within each leaf pattern the locally needed elements
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                CalculateNeededElements(neg, neededNodes, neededEdges);
            }
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    CalculateNeededElements(altCase, neededNodes, neededEdges);
                }
            }

            // and on ascending bottom up
            // a) it adds it's own locally needed elements
            //    - in conditions
            foreach (Condition cond in patternGraph.Conditions)
            {
                foreach (String neededNode in cond.NeededNodes)
                    neededNodes[neededNode] = true;
                foreach (String neededEdge in cond.NeededEdges)
                    neededEdges[neededEdge] = true;
            }
            //    - in the pattern
            foreach (PatternNode node in patternGraph.nodes)
                if (node.PointOfDefinition != patternGraph)
                    neededNodes[node.name] = true;
            foreach (PatternEdge edge in patternGraph.edges)
                if (edge.PointOfDefinition != patternGraph)
                    neededEdges[edge.name] = true;
            //    - as subpattern connections
            foreach (PatternGraphEmbedding sub in patternGraph.embeddedGraphs)
            {
                foreach (PatternElement element in sub.connections)
                {
                    if (element.PointOfDefinition!=patternGraph)
                    {
                        if (element is PatternNode)
                            neededNodes[element.name] = true;
                        else // element is PatternEdge
                            neededEdges[element.name] = true;
                    }
                }
            }

            // b) it filters out the elements needed (by the nested patterns) which are defined locally
            foreach (PatternNode node in patternGraph.nodes)
                if (node.PointOfDefinition == patternGraph)
                    neededNodes.Remove(node.name);
            foreach (PatternEdge edge in patternGraph.edges)
                if (edge.PointOfDefinition == patternGraph)
                    neededEdges.Remove(edge.name);
        }


        /// <summary>
        /// Inserts schedules of negative pattern graphs into the schedule of the positive pattern graph
        /// </summary>
        public void InsertNegativesIntoSchedule(PatternGraph patternGraph)
        {
            // todo: erst implicit node, dann negative, auch wenn negative mit erstem implicit moeglich wird

            Debug.Assert(patternGraph.ScheduleIncludingNegatives == null);
            List<SearchOperation> operations = new List<SearchOperation>();
            for (int i = 0; i < patternGraph.Schedule.Operations.Length; ++i)
                operations.Add(patternGraph.Schedule.Operations[i]);

            // calculate needed elements of each negative search plan / search plan graph
            // (elements from the positive graph needed in order to execute the nac)
            Dictionary<String, bool>[] neededNodes = new Dictionary<String, bool>[patternGraph.negativePatternGraphs.Length];
            Dictionary<String, bool>[] neededEdges = new Dictionary<String, bool>[patternGraph.negativePatternGraphs.Length];
            for (int i = 0; i < patternGraph.negativePatternGraphs.Length; ++i)
            {
                neededNodes[i] = new Dictionary<String, bool>();
                neededEdges[i] = new Dictionary<String, bool>();
                CalculateNeededElements(patternGraph.negativePatternGraphs[i], neededNodes[i], neededEdges[i]);
            }

            // iterate over all negative scheduled search plans (TODO: order?)
            for (int i = 0; i < patternGraph.negativePatternGraphs.Length; ++i)
            {
                ScheduledSearchPlan negSchedule = patternGraph.negativePatternGraphs[i].ScheduleIncludingNegatives;
                int bestFitIndex = operations.Count;
                float bestFitCostToEnd = 0;

                // find best place in scheduled search plan for current negative pattern 
                // during search from end of schedule forward until the first element the negative pattern is dependent on is found
                for (int j = operations.Count - 1; j >= 0; --j)
                {
                    SearchOperation op = operations[j];
                    if (op.Type == SearchOperationType.NegativePattern
                        || op.Type == SearchOperationType.Condition) {
                        continue;
                    }

                    if (neededNodes[i].ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name)
                        || neededEdges[i].ContainsKey(((SearchPlanNode)op.Element).PatternElement.Name)) { 
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

            float cost = operations.Count > 0 ? operations[0].CostToEnd : 0;
            patternGraph.ScheduleIncludingNegatives = new ScheduledSearchPlan(patternGraph, operations.ToArray(), cost);
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
        /// Generates the matcher source code for the given rule pattern into the given source builder
        /// </summary>
        public void GenerateMatcherSourceCode(SourceBuilder sb, LGSPMatchingPattern matchingPattern,
            bool isInitialStatic)
        {
            // generate the search program out of the schedule within the pattern graph of the rule
            SearchProgram searchProgram = GenerateSearchProgram(matchingPattern);

            // emit matcher class head, body, tail; body is source code representing search program
            if(matchingPattern is LGSPRulePattern)
                GenerateMatcherClassHeadAction(sb, (LGSPRulePattern)matchingPattern, isInitialStatic);
            else
                GenerateMatcherClassHeadSubpattern(sb, matchingPattern, isInitialStatic);
            searchProgram.Emit(sb);
            GenerateMatcherClassTail(sb);

            // finally generate matcher source for all the nested alternatives of the pattern graph
            // nested alternatives are the direct alternatives and their nested alternatives
            foreach(Alternative alt in matchingPattern.patternGraph.alternatives)
            {
                GenerateMatcherSourceCode(sb, matchingPattern, alt, isInitialStatic);
            }
            // or the alternatives nested within the negatives
            foreach (PatternGraph neg in matchingPattern.patternGraph.negativePatternGraphs)
            {
                GenerateMatcherSourceCode(sb, matchingPattern, neg, isInitialStatic);
            }
        }

        /// <summary>
        /// Generates the matcher source code for the given alternative into the given source builder
        /// </summary>
        public void GenerateMatcherSourceCode(SourceBuilder sb, LGSPMatchingPattern matchingPattern,
            Alternative alt, bool isInitialStatic)
        {
            // generate the search program out of the schedules within the pattern graphs of the alternative cases
            SearchProgram searchProgram = GenerateSearchProgram(matchingPattern, alt);

            // emit matcher class head, body, tail; body is source code representing search program
            GenerateMatcherClassHeadAlternative(sb, matchingPattern, alt, isInitialStatic);
            searchProgram.Emit(sb);
            GenerateMatcherClassTail(sb);

            // handle nested alternatives
            foreach (PatternGraph altCase in alt.alternativeCases)
            {
                foreach (Alternative nestedAlt in altCase.alternatives)
                {
                    GenerateMatcherSourceCode(sb, matchingPattern, nestedAlt, isInitialStatic);
                }
                foreach (PatternGraph neg in altCase.negativePatternGraphs)
                {
                    GenerateMatcherSourceCode(sb, matchingPattern, neg, isInitialStatic);
                }
            }
        }

        /// <summary>
        /// Generates the matcher source code for the nested alternatives within the given negative pattern graph
        /// into the given source builder
        /// </summary>
        public void GenerateMatcherSourceCode(SourceBuilder sb, LGSPMatchingPattern matchingPattern,
            PatternGraph neg, bool isInitialStatic)
        {
            // nothing to do locally ..

            // .. just move on to the nested alternatives
            foreach (Alternative alt in neg.alternatives)
            {
                GenerateMatcherSourceCode(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach (PatternGraph nestedNeg in neg.negativePatternGraphs)
            {
                GenerateMatcherSourceCode(sb, matchingPattern, nestedNeg, isInitialStatic);
            }
        }

        /// <summary>
        /// Generates the serach program for the pattern graph of the given rule
        /// </summary>
        SearchProgram GenerateSearchProgram(LGSPMatchingPattern matchingPattern)
        {
            PatternGraph patternGraph = matchingPattern.patternGraph;
            ScheduledSearchPlan scheduledSearchPlan = patternGraph.ScheduleIncludingNegatives;

            // build pass: build nested program from scheduled search plan
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram;
            if (matchingPattern is LGSPRulePattern) {
                searchProgram = searchProgramBuilder.BuildSearchProgram(model, 
                    (LGSPRulePattern)matchingPattern, null, null, null);
            } else {
                searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern);
            }
#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(rulePattern.name + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // build additional: create extra search subprogram per MaybePreset operation;
            // will be called when preset element is not available
            if (matchingPattern is LGSPRulePattern)
                searchProgramBuilder.BuildAddionalSearchSubprograms(
                    scheduledSearchPlan, searchProgram, (LGSPRulePattern)matchingPattern);

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter searchProgramCompleter = new SearchProgramCompleter();
            searchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(rulePattern.name + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgram;
        }

        /// <summary>
        /// Generates the search program for the given alternative 
        /// </summary>
        SearchProgram GenerateSearchProgram(LGSPMatchingPattern matchingPattern, Alternative alt)
        {
            ScheduledSearchPlan[] scheduledSearchPlans = new ScheduledSearchPlan[alt.alternativeCases.Length];
            int i=0;
            foreach (PatternGraph altCase in alt.alternativeCases) {
                scheduledSearchPlans[i] = altCase.ScheduleIncludingNegatives;
                ++i;
            }

            // build pass: build nested program from scheduled search plans of the alternative cases
            SearchProgramBuilder searchProgramBuilder = new SearchProgramBuilder();
            SearchProgram searchProgram = searchProgramBuilder.BuildSearchProgram(model, matchingPattern, alt);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(rulePattern.name + "_" + alt.name + "_" + searchProgram.Name + "_built_dump.txt");
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
            writer = new StreamWriter(rulePattern.name + "_" + alt.name + "_" + searchProgram.Name + "_completed_dump.txt");
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
            sb.AppendFront("using System;\nusing System.Collections.Generic;\nusing de.unika.ipd.grGen.libGr;\nusing de.unika.ipd.grGen.lgsp;\n"
                + "using " + namespaceOfModel + ";\n"
                + "using " + namespaceOfRulePatterns + ";\n\n");
            sb.AppendFront("namespace de.unika.ipd.grGen.lgspActions\n");
            sb.AppendFront("{\n");
            sb.Indent(); // namespace level
        }

        /// <summary>
        /// Generates matcher class head source code for the pattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadAction(SourceBuilder sb, LGSPRulePattern rulePattern, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;
                
            String namePrefix = (isInitialStatic ? "" : "Dyn") + "Action_";
            String className = namePrefix + rulePattern.name;

            sb.AppendFront("public class " + className + " : LGSPAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level
            sb.AppendFront("public " + className + "() {\n");
            sb.Indent(); // method body level
            sb.AppendFront("rulePattern = " + rulePattern.GetType().Name + ".Instance;\n");
            sb.AppendFront("patternGraph = rulePattern.patternGraph;\n");
            sb.AppendFront("DynamicMatch = myMatch; matches = new LGSPMatches(this, " 
                + patternGraph.Nodes.Length + ", " 
                + patternGraph.Edges.Length + ", " 
                + patternGraph.EmbeddedGraphs.Length + "+" + patternGraph.Alternatives.Length +
                ");\n");
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            sb.AppendFront("public override string Name { get { return \"" + rulePattern.name + "\"; } }\n");
            sb.AppendFront("private LGSPMatches matches;\n\n");
            if (isInitialStatic)
            {
                sb.AppendFront("public static LGSPAction Instance { get { return instance; } }\n");
                sb.AppendFront("private static " + className + " instance = new " + className + "();\n\n");
            }
        }

        /// <summary>
        /// Generates matcher class head source code for the subpattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadSubpattern(SourceBuilder sb, LGSPMatchingPattern matchingPattern, bool isInitialStatic)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "PatternAction_";
            String className = namePrefix + matchingPattern.name;

            sb.AppendFront("public class " + className + " : LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level
            sb.AppendFront("public " + className + "(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {\n");
            sb.Indent(); // method body level
            sb.AppendFront("graph = graph_; openTasks = openTasks_;\n");
            sb.AppendFront("patternGraph = " + matchingPattern.GetType().Name + ".Instance.patternGraph;\n");
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                PatternNode node = patternGraph.nodes[i];
                if (node.PointOfDefinition == null)
                {
                    sb.AppendFront("public LGSPNode " + node.name + ";\n");
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                PatternEdge edge = patternGraph.edges[i];
                if (edge.PointOfDefinition == null)
                {
                    sb.AppendFront("public LGSPEdge " + edge.name + ";\n");
                }
            }
            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the given alternative into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadAlternative(SourceBuilder sb, LGSPMatchingPattern matchingPattern, Alternative alternative, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "AlternativeAction_";
            String className = namePrefix + alternative.pathPrefix+alternative.name;

            sb.AppendFront("public class " + className + " : LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level
            sb.AppendFront("public " + className + "(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {\n");
            sb.Indent(); // method body level
            sb.AppendFront("graph = graph_; openTasks = openTasks_;\n");
            // pfadausdruck gebraucht, da das alternative-objekt im pattern graph steckt
            sb.AppendFront("patternGraphs = patternGraphs_;\n");
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            Dictionary<string, bool> neededNodes = new Dictionary<string,bool>();
            Dictionary<string, bool> neededEdges = new Dictionary<string,bool>();
            foreach (PatternGraph pg in alternative.alternativeCases)
            {
                CalculateNeededElements(pg, neededNodes, neededEdges);
            }
            foreach(KeyValuePair<string, bool> node in neededNodes)
            {
                sb.AppendFront("public LGSPNode " + node.Key + ";\n");
            }
            foreach(KeyValuePair<string, bool> edge in neededEdges)
            {
                sb.AppendFront("public LGSPEdge " + edge.Key + ";\n");
            }
            sb.AppendFront("\n");
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
            bool isSubpattern, bool isNegative)
        {
            PlanGraph planGraph = GeneratePlanGraph(graph, patternGraph, isNegative, isSubpattern);
            MarkMinimumSpanningArborescence(planGraph, patternGraph.name);
            SearchPlanGraph searchPlanGraph = GenerateSearchPlanGraph(planGraph);
            ScheduledSearchPlan scheduledSearchPlan = ScheduleSearchPlan(
                searchPlanGraph, patternGraph, isNegative);
            AppendHomomorphyInformation(scheduledSearchPlan);
            patternGraph.Schedule = scheduledSearchPlan;

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                GenerateScheduledSearchPlans(neg, graph, isSubpattern, true);
            }

            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    GenerateScheduledSearchPlans(altCase, graph, isSubpattern, false);
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
            GenerateFileHeaderForActionsFile(sourceCode, model.GetType().Namespace, action.RulePattern.GetType().Namespace);

            // can't generate new subpattern matchers due to missing scheduled search plans for them / missing graph analyze data
            Debug.Assert(action.rulePattern.patternGraph.embeddedGraphs.Length == 0);

            GenerateMatcherSourceCode(sourceCode, action.rulePattern, false);

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
        protected Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> SubpatternsUsedByTheActions(LGSPAction[] actions)
        {
            // todo: use more efficient worklist algorithm
            Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPatterns = new Dictionary<LGSPMatchingPattern, LGSPMatchingPattern>();

            // all directly used subpatterns
            foreach (LGSPAction action in actions)
            {
                PatternGraphEmbedding[] embeddedGraphs = ((PatternGraphEmbedding[])
                    action.RulePattern.PatternGraph.EmbeddedGraphs);
                for (int i = 0; i < embeddedGraphs.Length; ++i)
                {
                    subpatternMatchingPatterns.Add(embeddedGraphs[i].matchingPatternOfEmbeddedGraph, null);
                }
            }

            // transitive closure
            bool setChanged = subpatternMatchingPatterns.Count != 0;
            while (setChanged)
            {
                setChanged = false;
                foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPattern in subpatternMatchingPatterns)
                {
                    PatternGraphEmbedding[] embedded = (PatternGraphEmbedding[])subpatternMatchingPattern.Key.PatternGraph.EmbeddedGraphs;
                    for (int i = 0; i < embedded.Length; ++i)
                    {
                        if (!subpatternMatchingPatterns.ContainsKey(embedded[i].matchingPatternOfEmbeddedGraph))
                        {
                            subpatternMatchingPatterns.Add(embedded[i].matchingPatternOfEmbeddedGraph, null);
                            setChanged = true;
                        }
                    }

                    if (setChanged) break;
                }
            }

            return subpatternMatchingPatterns;
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
            Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPatterns;
            subpatternMatchingPatterns = SubpatternsUsedByTheActions(actions);

            // generate code for subpatterns
            foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPattern in subpatternMatchingPatterns)
            {
                LGSPMatchingPattern smp = subpatternMatchingPattern.Key;

                GenerateScheduledSearchPlans(smp.patternGraph, graph, true, false);

                MergeNegativeSchedulesIntoPositiveSchedules(smp.patternGraph);

                GenerateMatcherSourceCode(sourceCode, smp, false);
            }

            // generate code for actions
            foreach(LGSPAction action in actions)
            {
                GenerateScheduledSearchPlans(action.rulePattern.patternGraph, graph, false, false);

                MergeNegativeSchedulesIntoPositiveSchedules(action.rulePattern.patternGraph);

                GenerateMatcherSourceCode(sourceCode, action.rulePattern, false);
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
