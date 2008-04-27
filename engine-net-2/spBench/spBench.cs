/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

#define DUMP_MATCHERPROGRAMS
//#define DUMP_INITIALGRAPH
#define NEWCOSTMODEL
//#define OPCOST_WITH_GEO_MEAN

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Threading;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace spBench
{
    class SearchPlanResult : IComparable<SearchPlanResult>
    {
        /// <summary>
        /// A Gödel number for the searchplan
        /// </summary>
        public SearchPlanID ID;

        /// <summary>
        /// Cost of the whole searchplan using the new cost model (Batz: c_Flat(P))
        /// </summary>
        public float CostFlat;

        /// <summary>
        /// Cost of the whole searchplan using the old cost model (Batz: c(P))
        /// </summary>
        public float CostBatz;

        /// <summary>
        /// Cost of the chosen operations (Batz: c*(S))
        /// </summary>
        public float SPCostProduct;

        /// <summary>
        /// Cost of the chosen operations (Implementation: c_Impl*(S))
        /// </summary>
        public float SPCostImplProduct;

        /// <summary>
        /// Cost of the chosen operations (Varró: c+(S))
        /// </summary>
        public float SPCostSum;

        /// <summary>
        /// Time needed for (takeRule; releaseRule; giveRule){N}
        /// </summary>
        public int Time;

        public SearchPlanResult(SearchPlanID id, float costflat, float costbatz, float spcostproduct, float spcostimplproduct, float spcostsum, int time)
        {
            ID = id;
            CostFlat = costflat;
            CostBatz = costbatz;
            SPCostProduct = spcostproduct;
            SPCostImplProduct = spcostimplproduct;
            SPCostSum = spcostsum;
            Time = time;
        }

        public int CompareTo(SearchPlanResult other)
        {
            return Time - other.Time;
        }
    }

    class SearchPlanID : ICloneable
    {
        private List<int> idList = new List<int>();
        public void SetNextDecision(int decisionNum)
        {
            idList.Add(decisionNum);
        }
        public void RevertLastDecision()
        {
            idList.RemoveAt(idList.Count - 1);
        }

        public IEnumerable<int> GetDecisions()
        {
            return idList;
        }

        public Object Clone()
        {
            SearchPlanID newID = new SearchPlanID();
            newID.idList = new List<int>(idList);
            return newID;
        }

        public bool Equals(SearchPlanID other)
        {
            if(other == null) return false;
            if(idList.Count != other.idList.Count) return false;
            for(int i = 0; i < idList.Count; i++)
            {
                if(idList[i] != other.idList[i]) return false;
            }
            return true;
        }
    }

    class GenSPContext
    {
        public LGSPGraph Graph;
        public LGSPMatcherGenerator MatchGen;
        public LGSPActions Actions;
        public LGSPAction Action;
        public SearchPlanID SearchPlanID = new SearchPlanID();
        public LinkedList<SearchOperation> Schedule = new LinkedList<SearchOperation>();
        public int NumElements;
        public PatternGraph PatternGraph;
        public SearchPlanGraph SearchPlanGraph;
        public PatternGraph[] NegPatternGraphs;
        public SearchPlanGraph[] NegSPGraphs;
        public Dictionary<String, List<int>> ElemToNegs;
        public Dictionary<String, bool>[] NegRemainingNeededElements;
        public int[] NegNumNeededElements;
        public SearchPlanEdge[] NegEdges;
        public bool[] NegVisited;

        public Dictionary<String, List<int>> ElemToConds;
        public Dictionary<String, List<int>>[] ElemToNegCondsArray;
        public int[] NumCondsRemainingNeededElements;
        public Dictionary<String, bool>[] CondNeededElementVisitedArray;
        public Dictionary<String, bool>[][] NegCondNeededNegElementVisitedArray;

//        public SearchPlanGraph CurNegPattern = null;
//        public int CurNegVisitedElements = 0;
//        public int CurNumNegElements = 0;
//        public SearchPlanEdge CurNegPatternEdge = null;
//        public List<SearchPlanEdge> AvailEdgesBeforeNegPattern = null;

        public int NumVisitedElements = 0;

        public float MaxCostFlat = 0;
        public float MaxCostBatz = 0;
        public float MaxSPCostProduct = 0;
        public float MaxSPCostSum = 0;
        public int MaxTime = 0;
        public List<SearchPlanResult> Results = new List<SearchPlanResult>();

        public ScheduledSearchPlan LGSPSSP;
        public SearchPlanID LGSPSPID = null;

        public GenSPContext(LGSPGraph graph, LGSPActions actions, LGSPAction action)
        {
            Graph = graph;
            Actions = actions;
            MatchGen = new LGSPMatcherGenerator(graph.Model);
            Action = action;
            PatternGraph = (PatternGraph) action.RulePattern.PatternGraph;
            SearchPlanGraph = GenSPGraphFromPlanGraph(MatchGen.GeneratePlanGraph(graph, PatternGraph, false, false));

//            DumpSearchPlanGraph(GenerateSearchPlanGraphNewCost(graph, (PatternGraph) action.RulePattern.PatternGraph, false), action.Name, "initial");

            LGSPSSP = GenerateLibGrSearchPlan();

            InitNegativePatterns();
            InitConditions();

            NumElements = SearchPlanGraph.Nodes.Length + 1 + NegSPGraphs.Length;
        }

        private ScheduledSearchPlan GenerateLibGrSearchPlan()
        {
/*            LGSPRulePattern rulePattern = Action.rulePattern;
            PatternGraph patternGraph = rulePattern.patternGraph;
            PlanGraph planGraph = MatchGen.GeneratePlanGraph(Graph, patternGraph, false, false);
            MatchGen.MarkMinimumSpanningArborescence(planGraph, Action.Name);
            SearchPlanGraph searchPlanGraph = MatchGen.GenerateSearchPlanGraph(planGraph);
            ScheduledSearchPlan scheduledSearchPlan = MatchGen.ScheduleSearchPlan(searchPlanGraph, patternGraph, false);
            MatchGen.AppendHomomorphyInformation(scheduledSearchPlan);
            patternGraph.Schedule = scheduledSearchPlan;

            SearchPlanGraph[] negSearchPlanGraphs = new SearchPlanGraph[patternGraph.negativePatternGraphs.Length];
            for(int i = 0; i < patternGraph.negativePatternGraphs.Length; i++)
            {
                PatternGraph negPatternGraph = patternGraph.negativePatternGraphs[i];
                PlanGraph negPlanGraph = MatchGen.GeneratePlanGraph(Graph, negPatternGraph, true, false);
                MatchGen.MarkMinimumSpanningArborescence(negPlanGraph, Action.Name + "_neg_" + (i + 1));
                negSearchPlanGraphs[i] = MatchGen.GenerateSearchPlanGraph(negPlanGraph);
            }

            MatchGen.MergeNegativeSchedulesIntoPositiveSchedules(patternGraph);

            return scheduledSearchPlan;*/

            MatchGen.GenerateScheduledSearchPlans(Action.rulePattern.patternGraph, Graph, false, false);
            return Action.rulePattern.patternGraph.Schedule;
        }

/*        private SearchPlanGraph GenerateSearchPlanGraphNewCost(LGSPGraph graph, PatternGraph patternGraph, bool negativePatternGraph)
        {
            SearchPlanNode[] nodes = new SearchPlanNode[patternGraph.Nodes.Length + patternGraph.Edges.Length];
            // upper bound for num of edges (lookup nodes + lookup edges + impl. tgt + impl. src + incoming + outgoing)
            List<SearchPlanEdge> edges = new List<SearchPlanEdge>(patternGraph.Nodes.Length + 5 * patternGraph.Edges.Length);

            int nodesIndex = 0;

            SearchPlanNode root = new SearchPlanNode("root");

            Dictionary<PatternNode, SearchPlanNodeNode> nodeDict = new Dictionary<PatternNode,SearchPlanNodeNode>();

            // create plan nodes and lookup plan edges for all pattern graph nodes
            for(int i = 0; i < patternGraph.Nodes.Length; i++)
            {
                PatternNode node = patternGraph.nodes[i];
                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if(node.PointOfDefinition == null)
                {
                    cost = 1;
                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && node.PointOfDefinition != patternGraph)
                {
                    cost = 1;
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
                    cost = graph.nodeCounts[node.TypeID];
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                nodes[nodesIndex] = nodeDict[node] = new SearchPlanNodeNode(PlanNodeType.Node, i + 1, isPreset, node);
                SearchPlanEdge rootToNodeEdge = new SearchPlanEdge(searchOperationType, root, nodes[nodesIndex], cost);
                rootToNodeEdge.LocalCost = cost;
                edges.Add(rootToNodeEdge);
                root.OutgoingEdges.Add(rootToNodeEdge);
                nodesIndex++;
            }

            // create plan nodes and necessary plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.Edges.Length; i++)
            {
                PatternEdge edge = patternGraph.edges[i];

                bool isPreset;
#if !NO_EDGE_LOOKUP
                float splitcost, localcost;
                SearchOperationType searchOperationType;
                if(edge.PointOfDefinition == null)
                {
                    splitcost = localcost = 1;

                    isPreset = true;
                    searchOperationType = SearchOperationType.MaybePreset;
                }
                else if(negativePatternGraph && edge.PointOfDefinition != patternGraph)
                {
                    splitcost = localcost = 1;

                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else
                {
                    int sourceTypeID;
                    if(edge.source != null) sourceTypeID = edge.source.TypeID;
                    else sourceTypeID = Graph.Model.NodeModel.RootType.TypeID;
                    int targetTypeID;
                    if(edge.target != null) targetTypeID = edge.target.TypeID;
                    else targetTypeID = Graph.Model.NodeModel.RootType.TypeID;
                    splitcost = graph.vstructs[((sourceTypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                        + targetTypeID) * 2 + (int) LGSPDir.Out];
                    localcost = graph.edgeCounts[edge.TypeID];

                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                nodes[nodesIndex] = new SearchPlanEdgeNode(PlanNodeType.Edge, i + 1, isPreset, edge,
                    edge.source != null ? nodeDict[edge.source] : null, edge.target != null ? nodeDict[edge.target] : null);
                SearchPlanEdge rootToNodeEdge = new SearchPlanEdge(searchOperationType, root, nodes[nodesIndex], splitcost);
                rootToNodeEdge.LocalCost = localcost;
                edges.Add(rootToNodeEdge);
                root.OutgoingEdges.Add(rootToNodeEdge);
#else
                SearchOperationType searchOperationType = SearchOperationType.Lookup;       // lookup as dummy
                if(edge.PatternElementType == PatternElementType.Preset)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.Preset;
                }
                else if(negativePatternGraph && edge.PatternElementType == PatternElementType.Normal)
                {
                    isPreset = true;
                    searchOperationType = SearchOperationType.NegPreset;
                }
                else isPreset = false;
                nodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset,
                    edge.source!=null ? edge.source.TempPlanMapping : null,
                    edge.target!=null ? edge.target.TempPlanMapping : null);
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
                    SearchPlanEdge implSrcEdge = new SearchPlanEdge(SearchOperationType.ImplicitSource, nodes[nodesIndex], nodeDict[edge.source], 1);
                    edges.Add(implSrcEdge);
                    nodes[nodesIndex].OutgoingEdges.Add(implSrcEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node
                if(edge.target != null && !edge.target.TempPlanMapping.IsPreset)
                {
                    SearchPlanEdge implTgtEdge = new SearchPlanEdge(SearchOperationType.ImplicitTarget, nodes[nodesIndex], nodeDict[edge.target], 1);
                    edges.Add(implTgtEdge);
                    nodes[nodesIndex].OutgoingEdges.Add(implTgtEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset
                if(!isPreset)
                {
                    // no outgoing if no source
                    if(edge.source != null)
                    {
                        int targetTypeID;
                        if(edge.target != null) targetTypeID = edge.target.TypeID;
                        else targetTypeID = graph.Model.NodeModel.RootType.TypeID;
                        float vstructval = graph.vstructs[((edge.source.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + targetTypeID) * 2 + (int) LGSPDir.Out];
                        int numSrcNodes = graph.nodeCounts[edge.source.TypeID];
                        if(numSrcNodes == 0) numSrcNodes = 1;
                        SearchPlanEdge outEdge = new SearchPlanEdge(SearchOperationType.Outgoing, nodeDict[edge.source], nodes[nodesIndex], vstructval / numSrcNodes);
                        outEdge.LocalCost = graph.meanOutDegree[edge.source.TypeID];
                        edges.Add(outEdge);
                        nodeDict[edge.source].OutgoingEdges.Add(outEdge);
                    }

                    // no incoming if no target
                    if(edge.target != null)
                    {
                        int sourceTypeID;
                        if(edge.source != null) sourceTypeID = edge.source.TypeID;
                        else sourceTypeID = Graph.Model.NodeModel.RootType.TypeID;
                        float vstructval = graph.vstructs[((edge.target.TypeID * graph.dim1size + edge.TypeID) * graph.dim2size
                            + sourceTypeID) * 2 + (int) LGSPDir.In];
                        int numTgtNodes = graph.nodeCounts[edge.target.TypeID];
                        if(numTgtNodes == 0) numTgtNodes = 1;
                        SearchPlanEdge inEdge = new SearchPlanEdge(SearchOperationType.Incoming, nodeDict[edge.target], nodes[nodesIndex], vstructval / numTgtNodes);
                        inEdge.LocalCost = graph.meanInDegree[edge.target.TypeID];
                        edges.Add(inEdge);
                        nodeDict[edge.target].OutgoingEdges.Add(inEdge);
                    }
                }

                nodesIndex++;
            }
            return new SearchPlanGraph(root, nodes, edges.ToArray(), patternGraph);
        }*/

        private String GetDumpName(SearchPlanNode node)
        {
            if(node.NodeType == PlanNodeType.Root) return "root";
            else if(node.NodeType == PlanNodeType.Node) return "node_" + node.PatternElement.Name;
            else return "edge_" + node.PatternElement.Name;
        }

        public void DumpSearchPlanGraph(SearchPlanGraph splanGraph, String dumpname, String namesuffix)
        {
            StreamWriter sw = new StreamWriter(dumpname + "-searchplangraph-" + namesuffix + ".vcg", false);

            sw.WriteLine("graph:{\ninfoname 1: \"Attributes\"\ndisplay_edge_labels: no\nport_sharing: no\nsplines: no\n"
                + "\nmanhattan_edges: no\nsmanhattan_edges: no\norientation: bottom_to_top\nedges: yes\nnodes:yes\nclassname 1: \"normal\"");
            sw.WriteLine("node:{title:\"root\" label:\"ROOT\"}\n");

            sw.WriteLine("graph:{{title:\"pattern\" label:\"{0}\" status:clustered color:lightgrey", dumpname);

            foreach(SearchPlanNode node in splanGraph.Nodes)
            {
                DumpNode(sw, node);
            }
            foreach(SearchPlanEdge edge in splanGraph.Edges)
            {
                DumpEdge(sw, edge, true);
            }
            sw.WriteLine("}\n}\n");
            sw.Close();
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

        private void DumpEdge(StreamWriter sw, SearchPlanEdge edge, bool markRed)
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

            sw.WriteLine("edge:{{sourcename:\"{0}\" targetname:\"{1}\" label:\"{2} / (loc {3:0.00} split {4:0.00}) \"{5}}}",
                GetDumpName(edge.Source), GetDumpName(edge.Target), typeStr, edge.LocalCost, edge.Cost,
                markRed ? " color:red" : "");
        }

        private SearchPlanGraph GenSPGraphFromPlanGraph(PlanGraph planGraph)
        {
            MatchGen.DumpPlanGraph(planGraph, Action.Name, "spBench");

            SearchPlanNode root = new SearchPlanNode("search plan root");
            root.Visited = true;                // inverted logic
            SearchPlanNode[] nodes = new SearchPlanNode[planGraph.Nodes.Length];
//            SearchPlanEdge[] edges = new SearchPlanEdge[planGraph.Nodes.Length - 1 + 1];    // +1 for root
            SearchPlanEdge[] edges = new SearchPlanEdge[planGraph.Edges.Length];
            Dictionary<PlanNode, SearchPlanNode> planMap = new Dictionary<PlanNode, SearchPlanNode>(planGraph.Nodes.Length);
            planMap.Add(planGraph.Root, root);
            int i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                if(node.NodeType == PlanNodeType.Edge)
                    nodes[i] = new SearchPlanEdgeNode(node, null, null);
                else
                    nodes[i] = new SearchPlanNodeNode(node);
                planMap.Add(node, nodes[i]);
                if(node.IsPreset)
                    nodes[i].Visited = false;       // inverted logic. Marked as already visited by the search plan enumerator
                else
                    nodes[i].Visited = true;        // inverted logic needed, as for the matcher generator all elements must be not be "visited" at startup
                i++;
            }
            i = 0;
            foreach(PlanEdge edge in planGraph.Edges)
            {
                edges[i] = new SearchPlanEdge(edge.Type, planMap[edge.Source], planMap[edge.Target], edge.Cost);
                planMap[edge.Source].OutgoingEdges.Add(edges[i]);
//                if(negGraph && edge.Type == PlanEdgeType.NegPreset) planMap[edge.Target].Visited = false;       // inverted logic
                i++;
            }
            foreach(PlanNode node in planGraph.Nodes)
            {
                if(node.NodeType != PlanNodeType.Edge) continue;
                SearchPlanEdgeNode spedgenode = (SearchPlanEdgeNode) planMap[node];
                spedgenode.PatternEdgeSource = (SearchPlanNodeNode) planMap[node.PatternEdgeSource];
                spedgenode.PatternEdgeTarget = (SearchPlanNodeNode) planMap[node.PatternEdgeTarget];
                spedgenode.PatternEdgeSource.OutgoingPatternEdges.Add(spedgenode);
                spedgenode.PatternEdgeTarget.IncomingPatternEdges.Add(spedgenode);
            }
            return new SearchPlanGraph(root, nodes, edges);
        }

        /// <summary>
        /// Generates negative searchplans and analyzes requirements of negative patterns
        /// </summary>
        private void InitNegativePatterns()
        {
            PatternGraph patternGraph = Action.rulePattern.patternGraph;
            NegPatternGraphs = new PatternGraph[patternGraph.negativePatternGraphs.Length];
            NegSPGraphs = new SearchPlanGraph[patternGraph.negativePatternGraphs.Length];
            NegRemainingNeededElements = new Dictionary<String, bool>[patternGraph.negativePatternGraphs.Length];
            NegEdges = new SearchPlanEdge[patternGraph.negativePatternGraphs.Length];
            for(int i = 0; i < patternGraph.negativePatternGraphs.Length; i++)
            {
                PatternGraph negPatternGraph = patternGraph.negativePatternGraphs[i];
                NegPatternGraphs[i] = negPatternGraph;
                NegSPGraphs[i] = GenSPGraphFromPlanGraph(MatchGen.GeneratePlanGraph(Graph, negPatternGraph, true, false));
                NegSPGraphs[i].Root.ElementID = i;
                Dictionary<String, bool> neededElemNames = new Dictionary<String, bool>();
                foreach(PatternNode node in negPatternGraph.Nodes)
                {
                    if(node.PointOfDefinition != negPatternGraph)
                        neededElemNames.Add(node.Name, true);
                }
                foreach(PatternEdge edge in negPatternGraph.Edges)
                {
                    if(edge.PointOfDefinition != negPatternGraph)
                        neededElemNames.Add(edge.Name, true);
                }
                NegRemainingNeededElements[i] = neededElemNames;
                NegEdges[i] = new SearchPlanEdge(SearchOperationType.NegativePattern, SearchPlanGraph.Root, NegSPGraphs[i].Root, 0);
            }
            ElemToNegs = new Dictionary<String, List<int>>();
            foreach(PatternNode node in patternGraph.Nodes)
            {
                for(int i = 0; i < NegSPGraphs.Length; i++)
                {
                    if(NegRemainingNeededElements[i].ContainsKey(node.Name))
                    {
                        List<int> negList;
                        if(!ElemToNegs.TryGetValue(node.Name, out negList))
                        {
                            negList = new List<int>();
                            negList.Add(i);
                            ElemToNegs[node.Name] = negList;
                        }
                        else negList.Add(i);
                    }
                }
            }
            foreach(PatternEdge edge in patternGraph.Edges)
            {
                for(int i = 0; i < NegSPGraphs.Length; i++)
                {
                    if(NegRemainingNeededElements[i].ContainsKey(edge.Name))
                    {
                        List<int> negList;
                        if(!ElemToNegs.TryGetValue(edge.Name, out negList))
                        {
                            negList = new List<int>();
                            negList.Add(i);
                            ElemToNegs[edge.Name] = negList;
                        }
                        else negList.Add(i);
                    }
                }
            }

            NegVisited = new bool[NegSPGraphs.Length];      // all elements initialized to false
            NegNumNeededElements = new int[NegSPGraphs.Length];
            for(int i = 0; i < NegSPGraphs.Length; i++)
                NegNumNeededElements[i] = NegRemainingNeededElements[i].Count;
        }

        /// <summary>
        /// Analyzes the requirements of conditions
        /// </summary>
        private void InitConditions()
        {
            PatternGraph patternGraph = Action.rulePattern.patternGraph;
            CondNeededElementVisitedArray = CalcNeededCondElementsArray(patternGraph);
            NumCondsRemainingNeededElements = new int[patternGraph.Conditions.Length];
            for(int i = 0; i < patternGraph.Conditions.Length; i++)
                NumCondsRemainingNeededElements[i] = CondNeededElementVisitedArray[i].Count;

            NegCondNeededNegElementVisitedArray = new Dictionary<String, bool>[patternGraph.negativePatternGraphs.Length][];
            for(int i = 0; i < patternGraph.negativePatternGraphs.Length; i++)
                NegCondNeededNegElementVisitedArray[i] = CalcNeededCondElementsArray(patternGraph.negativePatternGraphs[i]);

            ElemToConds = CalcCondElementMap(patternGraph, CondNeededElementVisitedArray, false);
            ElemToNegCondsArray = new Dictionary<String, List<int>>[patternGraph.negativePatternGraphs.Length];
            for(int i = 0; i < patternGraph.negativePatternGraphs.Length; i++)
                ElemToNegCondsArray[i] = CalcCondElementMap(patternGraph.negativePatternGraphs[i],
                    NegCondNeededNegElementVisitedArray[i], true);
        }

        private Dictionary<String, bool>[] CalcNeededCondElementsArray(PatternGraph patternGraph)
        {
            Dictionary<String, bool>[] neededElemsArray = new Dictionary<String, bool>[patternGraph.Conditions.Length];
            for(int i = 0; i < patternGraph.Conditions.Length; i++)
            {
                PatternCondition cond = patternGraph.Conditions[i];
                neededElemsArray[i] = new Dictionary<String, bool>();
                foreach(String nodeName in cond.NeededNodes)
                    neededElemsArray[i].Add(nodeName, false);
                foreach(String edgeName in cond.NeededEdges)
                    neededElemsArray[i].Add(edgeName, false);
            }
            return neededElemsArray;
        }

        private Dictionary<String, List<int>> CalcCondElementMap(PatternGraph patternGraph, 
            Dictionary<String, bool>[] neededElemDicts, bool isNegPattern)
        {
            Dictionary<String, List<int>> elemToCond = new Dictionary<String, List<int>>();
            foreach(PatternNode node in patternGraph.Nodes)
            {
                if (node.PointOfDefinition == null
                    || isNegPattern && node.PointOfDefinition != patternGraph)
                {
                    continue;
                }

                for(int i = 0; i < patternGraph.Conditions.Length; i++)
                {
                    if(neededElemDicts[i].ContainsKey(node.Name))
                    {
                        List<int> condList;
                        if(!elemToCond.TryGetValue(node.Name, out condList))
                            elemToCond[node.Name] = condList = new List<int>();
                        condList.Add(i);
                    }
                }
            }
            foreach(PatternEdge edge in patternGraph.Edges)
            {
                if (edge.PointOfDefinition == null
                    || isNegPattern && edge.PointOfDefinition != patternGraph)
                {
                    continue;
                }

                for(int i = 0; i < patternGraph.Conditions.Length; i++)
                {
                    if(neededElemDicts[i].ContainsKey(edge.Name))
                    {
                        List<int> condList;
                        if(!elemToCond.TryGetValue(edge.Name, out condList))
                            elemToCond[edge.Name] = condList = new List<int>();
                        condList.Add(i);
                    }
                }
            }
            return elemToCond;
        }
    }

    class NegContext
    {
        public List<SearchPlanEdge> AvailEdgesBeforeNegPattern;
        public PatternGraph PatternGraph;
        public SearchPlanGraph SP;
        public int NumElements;
        public int VisitedElements;
        public Dictionary<String, List<int>> ElemToNegConds;
        public int[] NumNegCondRemainingNeededNegElements;
        public Dictionary<String, bool>[] NegCondNeededNegElementVisitedArray;
        public LinkedList<SearchOperation> Schedule = new LinkedList<SearchOperation>();
        public SearchPlanEdge SPEdge;
        public ScheduledSearchPlan LGSPSSP;
        public int NextLGSPIndex;
        public GenSPContext Context;

        public NegContext(List<SearchPlanEdge> availEdgesBeforeNegPattern, PatternGraph pg, SearchPlanGraph sp,
                int numElements, SearchPlanEdge spEdge,
                ScheduledSearchPlan lgspSSP, Dictionary<String, bool>[] negCondNeededNegElementVisitedArray,
                Dictionary<String, List<int>> elemToNegConds, int nextLGSPIndex, GenSPContext context)
        {
            AvailEdgesBeforeNegPattern = availEdgesBeforeNegPattern;
            SP = sp;
            PatternGraph = pg;
            NumElements = numElements;
            SPEdge = spEdge;
            LGSPSSP = lgspSSP;
            NegCondNeededNegElementVisitedArray = negCondNeededNegElementVisitedArray;
            NumNegCondRemainingNeededNegElements = new int[NegCondNeededNegElementVisitedArray.Length];
            for(int i = 0; i < NegCondNeededNegElementVisitedArray.Length; i++)
                NumNegCondRemainingNeededNegElements[i] = NegCondNeededNegElementVisitedArray[i].Count;
            ElemToNegConds = elemToNegConds;
            NextLGSPIndex = nextLGSPIndex;
            Context = context;
        }
    }

    class SPBench
    {
        public static int N = 10000;
        public static int BenchTimes = 1;
        public static int BenchTimeout = 5000;

        public const float EPSILON = 0.000000000001F;

        static int numLookups = 0;
        static int allowedLookups = -1;     // unlimited

        static int foundMatches = -1;       // currently unknown

        static String testActionName = "InductionVar";
        static bool noBench = false;
        static bool justCount = false;
        static bool forceImplicit = false;
        static String benchGRS = null;

        static List<LGSPAction> otherActions = new List<LGSPAction>();

        static int BenchmarkActionOnce(LGSPGraph orgGraph, LGSPActions actions)
        {
            LGSPGraph graph = (LGSPGraph) orgGraph.Clone("tempGraph");
            actions.Graph = graph;
            Sequence seq = SequenceParser.ParseSequence(benchGRS, actions);

            PerformanceInfo perfInfo = new PerformanceInfo();
            actions.PerformanceInfo = perfInfo;
            actions.ApplyGraphRewriteSequence(seq);
            if(foundMatches != -1)
            {
                if(perfInfo.MatchesFound != foundMatches)
                    Console.WriteLine("INTERNAL ERROR: Expected " + foundMatches + " matches, but found " + perfInfo.MatchesFound);
            }
            else foundMatches = perfInfo.MatchesFound;
            return perfInfo.TotalTimeMS;
        }

        class BenchObject
        {
            public LGSPGraph Graph;
            public LGSPActions Actions;
            public int Time;

            public BenchObject(LGSPGraph graph, LGSPActions actions)
            {
                Graph = graph;
                Actions = actions;
            }
        }

        static void BenchmarkAction(Object param)
        {
            try
            {
                BenchObject benchObject = (BenchObject) param;
                benchObject.Time += BenchmarkActionOnce(benchObject.Graph, benchObject.Actions);
            }
            catch(TargetInvocationException) { }
        }

        static int num = 0;

#if NEWCOSTMODEL
        static void CalcScheduleCost(GenSPContext ctx, SearchOperation[] ops, out float costflat, out float costbatz, out float spcostproduct,
            out float spcostimplproduct, out float spcostsum)
        {
            float costflatsummand = 1, costbatzsummand = 1;
            costflat = 0;
            costbatz = 0;
            spcostproduct = 1;
            spcostimplproduct = 1;
            spcostsum = 0;

            foreach(SearchOperation op in ops)
            {
                if(op.Type == SearchOperationType.NegativePattern)
                {
                    // TODO: These NAC costs are probably not sensible
                    costflat += costflatsummand * op.CostToEnd;
                    costbatz += costbatzsummand * op.CostToEnd;
                    spcostproduct *= Math.Max(((ScheduledSearchPlan) op.Element).Cost, 1);
                    spcostsum += ((ScheduledSearchPlan) op.Element).Cost;
                }
                else
                {
                    // TODO: Consider AllowedTypes!!

                    float localcost, splitcost, implcost;
                    if(op.Type == SearchOperationType.Incoming)
                    {
                        SearchPlanEdgeNode edge = (SearchPlanEdgeNode) op.Element;
                        SearchPlanNodeNode src = (SearchPlanNodeNode) edge.PatternEdgeSource;
                        SearchPlanNodeNode tgt = (SearchPlanNodeNode) op.SourceSPNode;

                        int numTgtNodes = ctx.Graph.nodeCounts[tgt.PatternElement.TypeID];
                        if(numTgtNodes == 0) numTgtNodes = 1;

                        float vstructVal = ctx.Graph.vstructs[(((tgt.PatternElement.TypeID * ctx.Graph.dim1size)
                            + edge.PatternElement.TypeID) * ctx.Graph.dim2size + src.PatternElement.TypeID) * 2 + (int) LGSPDir.In];
                        if(vstructVal < EPSILON) vstructVal = 1;

                        localcost = ctx.Graph.meanInDegree[tgt.PatternElement.TypeID];
                        splitcost = vstructVal / numTgtNodes;
                        implcost = splitcost;
                    }
                    else if(op.Type == SearchOperationType.Outgoing)
                    {
                        SearchPlanEdgeNode edge = (SearchPlanEdgeNode) op.Element;
                        SearchPlanNodeNode tgt = (SearchPlanNodeNode) edge.PatternEdgeTarget;
                        SearchPlanNodeNode src = (SearchPlanNodeNode) op.SourceSPNode;

                        int numSrcNodes = ctx.Graph.nodeCounts[src.PatternElement.TypeID];
                        if(numSrcNodes == 0) numSrcNodes = 1;

                        float vstructVal = ctx.Graph.vstructs[(((src.PatternElement.TypeID * ctx.Graph.dim1size)
                            + edge.PatternElement.TypeID) * ctx.Graph.dim2size + tgt.PatternElement.TypeID) * 2 + (int) LGSPDir.Out];
                        if(vstructVal < EPSILON) vstructVal = 1;

                        localcost = ctx.Graph.meanOutDegree[src.PatternElement.TypeID];
                        splitcost = vstructVal / numSrcNodes;
                        implcost = splitcost;
                    }
                    else if(op.Type == SearchOperationType.Lookup)
                    {
                        SearchPlanNode spnode = (SearchPlanNode) op.Element;
                        if(spnode.NodeType == PlanNodeType.Node)
                        {
                            localcost = ctx.Graph.nodeCounts[spnode.PatternElement.TypeID];
                            splitcost = localcost;
                        }
                        else
                        {
                            SearchPlanEdgeNode edge = (SearchPlanEdgeNode) spnode;
                            SearchPlanNodeNode src = (SearchPlanNodeNode) edge.PatternEdgeSource;
                            SearchPlanNodeNode tgt = (SearchPlanNodeNode) edge.PatternEdgeTarget;
                            localcost = ctx.Graph.edgeCounts[spnode.PatternElement.TypeID];
                            splitcost = ctx.Graph.vstructs[(((src.PatternElement.TypeID * ctx.Graph.dim1size)
                                    + edge.PatternElement.TypeID) * ctx.Graph.dim2size + tgt.PatternElement.TypeID) * 2 + (int) LGSPDir.Out];
                        }
                        implcost = localcost;
                    }
                    else
                    {
                        localcost = 0;
                        splitcost = 1;
                        implcost = splitcost;
                    }
                    costflat += costflatsummand * localcost;
                    costflatsummand *= (float) Math.Max(splitcost, 0.0001F);
                    costbatz += costbatzsummand * splitcost;
                    costbatzsummand *= (float) Math.Max(splitcost, 0.0001F);
                    spcostproduct *= Math.Max(splitcost, 1);
                    spcostimplproduct *= Math.Max(implcost, 1);
                    spcostsum += splitcost;
                }
            }
        }
#else
        static void CalcScheduleCost(GenSPContext ctx, SearchOperation[] ops, out float cost, out float spcostproduct, out float spcostsum)
        {
            float costsummand = 1;
            cost = 0;
            spcostproduct = 0;
            spcostsum = 0;

            foreach(SearchOperation op in ops)       
            {
                if(op.Type == SearchOperationType.NegativePattern)
                {
                    cost += costsummand * op.CostToEnd;
                    spcostproduct *= Math.Max(((ScheduledSearchPlan) op.Element).Cost, 1);
                    spcostsum += ((ScheduledSearchPlan) op.Element).Cost;
                }
                else
                {
#if OPCOST_WITH_GEO_MEAN
                    costsummand += op.CostToEnd;
                    cost += (float) Math.Exp(costsum);
                    spcost += op.CostToEnd;
#else
                    costsummand *= Math.Max(op.CostToEnd, 0.0001F);
                    cost += costsummand;
                    spcostproduct *= Math.Max(op.CostToEnd, 1);
                    spcostsum += op.CostToEnd;
#endif
                }
            }
#if OPCOST_WITH_GEO_MEAN
            cost = (float) Math.Log(cost);
#endif
        }
#endif

        static String SearchOpToString(SearchOperation op)
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
                    typeStr = " ?(" + String.Join(",", ((PatternCondition) op.Element).NeededNodes) + ")("
                        + String.Join(",", ((PatternCondition) op.Element).NeededEdges) + ")";
                    break;
                case SearchOperationType.NegativePattern:
                    typeStr = " !(" + ScheduleToString(((ScheduledSearchPlan) op.Element).Operations) + " )";
                    break;
            }
            return typeStr;
        }

        static String ScheduleToString(IEnumerable<SearchOperation> schedule)
        {
            StringBuilder str = new StringBuilder();

            foreach(SearchOperation searchOp in schedule)
                str.Append(' ').Append(SearchOpToString(searchOp));

            return str.ToString();
        }

        static void SearchPlanFinished(GenSPContext ctx)
        {
            if(justCount)
            {
                if((num & 0xfffff) == 0)
                {
                    Console.Write("{0,4}. ", num);
                    Console.WriteLine(ScheduleToString(ctx.Schedule));
                }
                return;
            }

            Console.Write("{0,4}. ", num);

/*            SearchOperation[] ops = new SearchOperation[ctx.Schedule.Count];
            ctx.Schedule.CopyTo(ops, 0);*/
            SearchOperation[] ops = new SearchOperation[ctx.Schedule.Count];
            int opind = 0;
            foreach(SearchOperation op in ctx.Schedule)
                ops[opind++] = new SearchOperation(op.Type, op.Element, op.SourceSPNode, op.CostToEnd);

            float costflat, costbatz, spcostproduct, spcostimplproduct, spcostsum;
            CalcScheduleCost(ctx, ops, out costflat, out costbatz, out spcostproduct, out spcostimplproduct, out spcostsum);

            String scheduleStr = ScheduleToString(ctx.Schedule);
            Console.Write(scheduleStr);
            for(int j = scheduleStr.Length; j < 50; j++) Console.Write(' ');
            Console.Write(" => C_Flat(P) = {0,9:N4} C(P) = {1,9:N4} C*(S) = {2,9:N4} C_Impl*(S) = {3,9:N4} C+(S) = {4,9:N4}",
                costflat, costbatz, spcostproduct, spcostimplproduct, spcostsum);

            int time;
            if(!noBench)
            {

                foreach(SearchPlanGraph sp in ctx.NegSPGraphs)
                {
                    foreach(SearchPlanEdge edge in sp.Root.OutgoingEdges)
                    {
                        if(edge.Type == SearchOperationType.NegPreset)
                            edge.Target.Visited = true;
                    }
                }

                ScheduledSearchPlan ssp = new ScheduledSearchPlan(
                    (PatternGraph)ctx.Action.RulePattern.PatternGraph, ops, spcostproduct);

                ctx.MatchGen.AppendHomomorphyInformation(ssp);
                ((PatternGraph) ctx.Action.RulePattern.PatternGraph).Schedule = ssp;
                ctx.MatchGen.MergeNegativeSchedulesIntoPositiveSchedules(ctx.Action.patternGraph);

#if DUMP_MATCHERPROGRAMS
                String outputName = "test.cs";
#else
                String outputName = null;
#endif

                LGSPAction newAction = ctx.MatchGen.GenerateAction(ssp, ctx.Action, Assembly.GetAssembly(ctx.Graph.Model.GetType()).Location,
                    Assembly.GetAssembly(ctx.Actions.GetType()).Location, outputName);

                ctx.Actions.ReplaceAction(ctx.Action.Name, newAction);
                BenchObject benchObject = new BenchObject(ctx.Graph, ctx.Actions);
                int i;
                for(i = 0; i < BenchTimes; i++)
                {
                    Thread benchThread = new Thread(new ParameterizedThreadStart(BenchmarkAction));
                    benchThread.Start(benchObject);
                    if(!benchThread.Join(BenchTimeout))
                    {
                        benchThread.Abort();
                        benchObject.Time = BenchTimeout;
                        break;
                    }
                }
                if(i == BenchTimes) benchObject.Time /= BenchTimes;

                time = benchObject.Time;
            }
            else time = -1;

            Console.WriteLine(" time = {0,5} ms", time);
            ctx.Results.Add(new SearchPlanResult((SearchPlanID) ctx.SearchPlanID.Clone(), costflat, costbatz, spcostproduct, spcostimplproduct, spcostsum, time));

            if(costflat > ctx.MaxCostFlat) ctx.MaxCostFlat = costflat;
            if(costbatz > ctx.MaxCostBatz) ctx.MaxCostBatz = costbatz;
            if(spcostproduct > ctx.MaxSPCostProduct) ctx.MaxSPCostProduct = spcostproduct;
            if(spcostsum > ctx.MaxSPCostSum) ctx.MaxSPCostSum = spcostsum;
            if(time > ctx.MaxTime) ctx.MaxTime = time;

            foreach(SearchPlanNode node in ctx.SearchPlanGraph.Nodes)
                node.Visited = false;

            foreach(SearchPlanGraph sp in ctx.NegSPGraphs)
            {
                foreach(SearchPlanNode node in sp.Nodes)
                {
                    node.Visited = false;
                }
            }
        }

        static int CompareLGSPOperation(SearchOperation curOp, int curLGSPIndex, ScheduledSearchPlan oldLGSPSP, out ScheduledSearchPlan negLGSPSSP)
        {
            int newLGSPIndex = -1;
            negLGSPSSP = null;
            if(curLGSPIndex >= 0 && curLGSPIndex < oldLGSPSP.Operations.Length)
            {
                // Check whether same operation at the same time was chosen by the lgsp matcher generator
                SearchOperation lgspOp = oldLGSPSP.Operations[curLGSPIndex];
                if(curOp.Type == lgspOp.Type && (curOp.SourceSPNode == null && lgspOp.SourceSPNode == null
                        || curOp.SourceSPNode != null && lgspOp.SourceSPNode != null
                            && curOp.SourceSPNode.PatternElement.Name == lgspOp.SourceSPNode.PatternElement.Name
                        || curOp.Type == SearchOperationType.NegativePattern))
                {
                    switch(curOp.Type)
                    {
                        case SearchOperationType.Lookup:
                        case SearchOperationType.Incoming:
                        case SearchOperationType.Outgoing:
                        case SearchOperationType.ImplicitSource:
                        case SearchOperationType.ImplicitTarget:
                        {
                            SearchPlanNode curOpElem = (SearchPlanNode) curOp.Element;
                            SearchPlanNode lgspOpElem = (SearchPlanNode) lgspOp.Element;
                            if(curOpElem.PatternElement.Name == lgspOpElem.PatternElement.Name)
                                newLGSPIndex = curLGSPIndex + 1;
                            break;
                        }
                        case SearchOperationType.NegativePattern:     // check in RecursiveIncludeNegativePattern
                        {
                            newLGSPIndex = curLGSPIndex;
                            negLGSPSSP = (ScheduledSearchPlan) lgspOp.Element;
                            break;
                        }
                        case SearchOperationType.Condition:
                        {
                            PatternCondition curCond = (PatternCondition) curOp.Element;
                            PatternCondition lgspCond = (PatternCondition) lgspOp.Element;
                            if(curCond == lgspCond)
                                newLGSPIndex = curLGSPIndex + 1;
                            break;
                        }
                        
                        // case SearchOperationType.Preset:     already taken out at the begining
                        // case SearchOperationType.NegPreset:  skipped at begining of negative pattern
                    }
                }
            }
            return newLGSPIndex;
        }

        static void RecursiveIncludeNegativePattern(NegContext negCtx, SearchPlanEdge nextEdge, List<SearchPlanEdge> oldAvailEdges,
                int nextNegLGSPIndex)
        {
            SearchPlanNode curNode = nextEdge.Target;
            curNode.Visited = false;                                            // inverted logic

            // Check whether conditions can be added due to curNode
            List<int> relatedConds;
            if(negCtx.ElemToNegConds.TryGetValue(curNode.PatternElement.Name, out relatedConds))
            {
                foreach(int condIndex in relatedConds)
                {
                    if(negCtx.NumNegCondRemainingNeededNegElements[condIndex] == 1)
                    {
                        // curNode is last needed element for condition => add Condition operation

                        SearchOperation newCondOp = new SearchOperation(SearchOperationType.Condition,
                            negCtx.PatternGraph.Conditions[condIndex], null, 0);
                        negCtx.Schedule.AddLast(newCondOp);

                        ScheduledSearchPlan dummy;
                        nextNegLGSPIndex = CompareLGSPOperation(newCondOp, nextNegLGSPIndex, negCtx.LGSPSSP, out dummy);
                    }
                    negCtx.NumNegCondRemainingNeededNegElements[condIndex]--;
                }
            }

            if(negCtx.VisitedElements + 1 == negCtx.NumElements)
            {
                SearchOperation[] ops = new SearchOperation[negCtx.Schedule.Count];
                negCtx.Schedule.CopyTo(ops, 0);

                float costflat, costbatz, spcostproduct, spcostimplproduct, spcostsum;
                CalcScheduleCost(negCtx.Context, ops, out costflat, out costbatz, out spcostproduct,
                    out spcostimplproduct, out spcostsum);          // TODO: spcostsum is not used, yet

                ScheduledSearchPlan negssp = new ScheduledSearchPlan(negCtx.PatternGraph, ops, spcostproduct);

                SearchOperation newOp = new SearchOperation(SearchOperationType.NegativePattern, negssp, null, costflat);
                negCtx.Context.Schedule.AddLast(newOp);

                RecursiveGenAllSearchPlans(negCtx.Context, negCtx.SPEdge, negCtx.AvailEdgesBeforeNegPattern, (nextNegLGSPIndex >= 0 ? negCtx.NextLGSPIndex + 1 : -1));

                negCtx.Context.Schedule.RemoveLast();
            }
            else
            {
                List<SearchPlanEdge> availableEdges = new List<SearchPlanEdge>(oldAvailEdges);
                availableEdges.Remove(nextEdge);
                negCtx.VisitedElements++;

                foreach(SearchPlanEdge outEdge in curNode.OutgoingEdges)
                {
                    if(!outEdge.Target.Visited) continue;
                    availableEdges.Add(outEdge);
                }

                for(int i = 0; i < availableEdges.Count; i++)
                {
                    if(!availableEdges[i].Target.Visited) continue;                 // inverted logic
                    negCtx.Context.SearchPlanID.SetNextDecision(i);

                    SearchOperation newOp = new SearchOperation(availableEdges[i].Type, availableEdges[i].Target,
                        availableEdges[i].Source, availableEdges[i].Cost);
                    negCtx.Schedule.AddLast(newOp);

                    ScheduledSearchPlan dummy;
                    int newNegLGSPIndex = CompareLGSPOperation(newOp, nextNegLGSPIndex, negCtx.LGSPSSP, out dummy);
                    RecursiveIncludeNegativePattern(negCtx, availableEdges[i], availableEdges, newNegLGSPIndex);

                    negCtx.Context.SearchPlanID.RevertLastDecision();
                    negCtx.Schedule.RemoveLast();
                }
                negCtx.VisitedElements--;
            }

            if(relatedConds != null)
            {
                foreach(int condIndex in relatedConds)
                {
                    if(negCtx.NumNegCondRemainingNeededNegElements[condIndex] == 0)
                    {
                        // curNode was last needed element for condition => remove Condition operation
                        negCtx.Schedule.RemoveLast();
                    }
                    negCtx.NumNegCondRemainingNeededNegElements[condIndex]++;
                }
            }

            curNode.Visited = true;                                 // inverted logic
        }

        static void RecursiveGenAllSearchPlans(GenSPContext ctx, SearchPlanEdge nextEdge, List<SearchPlanEdge> oldAvailEdges, int nextLGSPIndex)
        {
            SearchPlanNode curNode = nextEdge.Target;
            curNode.Visited = false;                                // inverted logic

            // Check whether conditions can be added due to curNode
            List<int> relatedConds;
            if(ctx.ElemToConds.TryGetValue(curNode.PatternElement.Name, out relatedConds))
            {
                foreach(int condIndex in relatedConds)
                {
                    if(ctx.NumCondsRemainingNeededElements[condIndex] == 1)
                    {
                        // curNode is last needed element for condition => add Condition operation

                        SearchOperation newCondOp = new SearchOperation(SearchOperationType.Condition,
                            ctx.PatternGraph.Conditions[condIndex], null, 0);
                        ctx.Schedule.AddLast(newCondOp);

                        ScheduledSearchPlan dummy;
                        nextLGSPIndex = CompareLGSPOperation(newCondOp, nextLGSPIndex, ctx.LGSPSSP, out dummy);
                    }
                    ctx.NumCondsRemainingNeededElements[condIndex]--;
                }
            }

            if(ctx.NumVisitedElements + 1 == ctx.NumElements)
            {
                num++;
                SearchPlanFinished(ctx);
                if(nextLGSPIndex >= 0)
                {
                    ctx.LGSPSPID = (SearchPlanID) ctx.SearchPlanID.Clone();
                    Console.WriteLine("LGSP SP found (" + num + ")");
                }
            }
            else
            {
                List<SearchPlanEdge> availableEdges = new List<SearchPlanEdge>(oldAvailEdges);
                availableEdges.Remove(nextEdge);
                ctx.NumVisitedElements++;

                // Check whether negative searchplans become reachable because of curNode
                List<int> relatedNegs;
                if(ctx.ElemToNegs.TryGetValue(curNode.PatternElement.Name, out relatedNegs))
                {
                    foreach(int negnum in relatedNegs)
                    {
                        ctx.NegRemainingNeededElements[negnum].Remove(curNode.PatternElement.Name);
                    }

                    // check negative patterns available
                    for(int i = 0; i < ctx.NegSPGraphs.Length; i++)
                    {
                        if(ctx.NegVisited[i]) continue;
                        if(ctx.NegRemainingNeededElements[i].Count == 0)
                        {
                            availableEdges.Add(ctx.NegEdges[i]);
                            ctx.NegVisited[i] = true;
                        }
                    }
                }

                // Add searchplan edges reachable from curNode to ready set
                foreach(SearchPlanEdge outEdge in curNode.OutgoingEdges)
                {
                    if(!outEdge.Target.Visited) continue;               // inverted logic
                    availableEdges.Add(outEdge);
                }

                bool forcedImplicitAvailable = false;
                if(forceImplicit)
                {
                    foreach(SearchPlanEdge spedge in availableEdges)
                    {
                        if(spedge.Type == SearchOperationType.ImplicitSource || spedge.Type == SearchOperationType.ImplicitTarget)
                        {
                            forcedImplicitAvailable = true;
                            break;
                        }
                    }
                }

                for(int i = 0; i < availableEdges.Count; i++)
                {
                    if(!availableEdges[i].Target.Visited) continue;     // inverted logic

                    if(forcedImplicitAvailable && availableEdges[i].Type != SearchOperationType.ImplicitSource
                            && availableEdges[i].Type != SearchOperationType.ImplicitTarget)
                        continue;

                    if(availableEdges[i].Type == SearchOperationType.Lookup)
                    {
                        if(allowedLookups != -1 && numLookups >= allowedLookups) continue;
                        numLookups++;
                    }
                    ctx.SearchPlanID.SetNextDecision(i);

                    SearchOperation newOp = new SearchOperation(availableEdges[i].Type, availableEdges[i].Target,
                        availableEdges[i].Source, availableEdges[i].Cost);

                    ScheduledSearchPlan negLGSPSSP;
                    int newLGSPIndex = CompareLGSPOperation(newOp, nextLGSPIndex, ctx.LGSPSSP, out negLGSPSSP);

                    if(newOp.Type == SearchOperationType.NegativePattern)
                    {
                        int negIndex = availableEdges[i].Target.ElementID;
                        SearchPlanGraph negSPG = ctx.NegSPGraphs[negIndex];
                        NegContext negCtx = new NegContext(availableEdges, ctx.NegPatternGraphs[negIndex], negSPG,
                            negSPG.Nodes.Length + 1 - ctx.NegNumNeededElements[negIndex], availableEdges[i], negLGSPSSP,
                            ctx.NegCondNeededNegElementVisitedArray[negIndex], ctx.ElemToNegCondsArray[negIndex], newLGSPIndex, ctx);

                        List<SearchPlanEdge> negAvailEdges = new List<SearchPlanEdge>();

                        foreach(SearchPlanEdge rootEdge in negCtx.SP.Root.OutgoingEdges)
                        {
                            if(rootEdge.Type != SearchOperationType.NegPreset) continue;
                            foreach(SearchPlanEdge outEdge in rootEdge.Target.OutgoingEdges)
                            {
                                negAvailEdges.Add(outEdge);
                            }
                        }

                        int nextNegLGSPIndex = (newLGSPIndex >= 0 ? 0 : -1);
                        if(nextNegLGSPIndex >= 0)   // implies that negLGSPSP is non-null
                        {
                            while(nextNegLGSPIndex < negLGSPSSP.Operations.Length
                                    && negLGSPSSP.Operations[nextNegLGSPIndex].Type == SearchOperationType.NegPreset)
                                nextNegLGSPIndex++;
                        }

                        RecursiveIncludeNegativePattern(negCtx, availableEdges[i], negAvailEdges, nextNegLGSPIndex);
                    }
                    else
                    {
                        ctx.Schedule.AddLast(newOp);

                        RecursiveGenAllSearchPlans(ctx, availableEdges[i], availableEdges, newLGSPIndex);

                        ctx.Schedule.RemoveLast();
                    }

                    if(availableEdges[i].Type == SearchOperationType.Lookup)
                    {
                        numLookups--;
                    }

                    ctx.SearchPlanID.RevertLastDecision();
                }

                if(ctx.ElemToNegs.TryGetValue(curNode.PatternElement.Name, out relatedNegs))
                {
                    foreach(int negnum in relatedNegs)
                    {
                        ctx.NegRemainingNeededElements[negnum].Add(curNode.PatternElement.Name, true);
                    }

                    // check negative patterns visited but not available anymore
                    for(int i = 0; i < ctx.NegSPGraphs.Length; i++)
                    {
                        if(!ctx.NegVisited[i]) continue;
                        if(ctx.NegRemainingNeededElements[i].Count != 0)
                            ctx.NegVisited[i] = false;
                    }
                }

                ctx.NumVisitedElements--;
            }

            if(relatedConds != null)
            {
                foreach(int condIndex in relatedConds)
                {
                    if(ctx.NumCondsRemainingNeededElements[condIndex] == 0)
                    {
                        // curNode was last needed element for condition => remove Condition operation
                        ctx.Schedule.RemoveLast();
                    }
                    ctx.NumCondsRemainingNeededElements[condIndex]++;
                }
            }
            curNode.Visited = true;                                 // inverted logic
        }

        public static void Main(string[] args)
        {
            String grgFile = null;
            String initGRS = null;
            String reuseResFile = null;
            String replaceN = null;
            String replaceM = null;
            testActionName = null;
            bool error = false;
            List<String> ruleNamesForGenSP = new List<String>();

            for(int i = 0; i < args.Length && !error; i++)
            {
                if(args[i][0] == '-')
                {
                    switch(args[i])
                    {
                        case "-nobench": noBench = true; break;
                        case "-count": noBench = justCount = true; break;
                        case "-maxlookups":
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.WriteLine("No <n> specified for -maxlookups!");
                                break;
                            }
                            if(!int.TryParse(args[++i], out allowedLookups))
                            {
                                error = true;
                                Console.WriteLine("<n> must be an integer for -maxlookups!");
                                break;
                            }
                            break;
                        case "-forceimplicit": forceImplicit = true; break;
                        case "-initgrs":
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.WriteLine("No xgrs specified for -initgrs!");
                                break;
                            }
                            initGRS = args[++i];
                            break;
                        case "-gensp":
                        {
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.WriteLine("No rules specified for -gensp!");
                                break;
                            }
                            String genSPRuleStr = args[++i];
                            foreach(String name in genSPRuleStr.Split(' '))
                                ruleNamesForGenSP.Add(name);
                            break;
                        }    
                        case "-benchgrs":
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.WriteLine("No xgrs specified for -benchgrs!");
                                break;
                            }
                            benchGRS = args[++i];
                            break;
                        case "-reuse":
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.WriteLine("No file specified for -reuse!");
                                break;
                            }
                            reuseResFile = args[++i];
                            if(!File.Exists(reuseResFile))
                            {
                                error = true;
                                Console.WriteLine("Old result file \"" + reuseResFile + "\" does not exist!");
                            }
                            noBench = true;
                            break;     
                        case "-timeout":
                            if(!int.TryParse(args[++i], out BenchTimeout))
                            {
                                error = true;
                                Console.WriteLine("<n> must be an integer for -timeout!");
                                break;
                            }
                            break;
                        case "-times":
                            if(!int.TryParse(args[++i], out BenchTimes))
                            {
                                error = true;
                                Console.WriteLine("<n> must be an integer for -times!");
                                break;
                            }
                            break;
                        case "-N":
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.WriteLine("No argument specified for -N!");
                                break;
                            }
                            replaceN = args[++i];
                            break;
                        case "-M":
                            if(i + 1 >= args.Length)
                            {
                                error = true;
                                Console.WriteLine("No argument specified for -M!");
                                break;
                            }
                            replaceM = args[++i];
                            break;
                        default:
                            error = true;
                            Console.WriteLine("Illegal option: " + args[i]);
                            break;
                    }                   
                }
                else if(grgFile == null)
                {
                    grgFile = args[i];
                    if(!File.Exists(grgFile))
                    {
                        error = true;
                        Console.WriteLine("Actions specification file \"" + grgFile + "\" does not exist!");
                    }
                }
                else if(testActionName == null)
                {
                    testActionName = args[i];
                }
                else
                {
                    error = true;
                    Console.WriteLine("Too many files given!");
                }
            }

            String resultFile = "spBench-" + testActionName + "-" + BenchTimes + "x" + BenchTimeout + "ms.txt";
            if(reuseResFile != null && reuseResFile == resultFile)
            {
                error = true;
                Console.WriteLine("<oldresfile> may not have the same name as the output file!");
            }

            if(grgFile == null || testActionName == null) error = true;

            if(error)
            {
                Console.WriteLine("Usage: spBench [OPTIONS] <grg-file> <actionName>\n"
                    + "Options:\n"
                    + "  -nobench               No benchmarking will be performed\n"
                    + "  -count                 Only counts the number of possible searchplans.\n"
                    + "                         Implies -nobench.\n"
                    + "  -reuse <oldresfile>    Reuses the results previously written into\n"
                    + "                         <oldresfile>. Implies -nobench.\n"
                    + "  -maxlookups <n>        At most <n> lookups will be allowed in a searchplan\n"
                    + "                         (default: unlimited)\n"
                    + "  -forceimplicit         Forces implicits to be matched as soon as possible\n"
                    + "  -initgrs \"<xgrs>\"      Sets the xgrs used to initialize the graph\n"
                    + "                         (default: none)\n"
                    + "  -gensp \"<rules>\"       Generates new searchplans for all rules in the\n"
                    + "                         space separated list <rules> before benchmarking\n"
                    + "  -benchgrs \"<xgrs>\"     Sets the xgrs used for benchmarking\n"
                    + "                         (default: \"[<actionName>][50]\")\n"
                    + "  -timeout <n>           Sets the timeout in ms for one benchgrs execution\n"
                    + "                         (default: " + BenchTimeout + ")\n"
                    + "  -times <n>             Sets how many times a benchmark should be executed\n"
                    + "                         (default: " + BenchTimes + ")\n"
                    + "  -N <n>                 Replaces \"$N$\" in initgrs and benchgrs by <n>\n"
                    + "  -M <n>                 Replaces \"$M$\" in initgrs and benchgrs by <n>");
                return;
            }

            if(benchGRS == null)
                benchGRS = "[" + testActionName + "][50]";

            if(replaceN != null)
            {
                if(initGRS != null) initGRS = initGRS.Replace("$N$", replaceN);
                if(benchGRS != null) benchGRS = benchGRS.Replace("$N$", replaceN);
            }
            if(replaceM != null)
            {
                if(initGRS != null) initGRS = initGRS.Replace("$M$", replaceM);
                if(benchGRS != null) benchGRS = benchGRS.Replace("$M$", replaceM);
            }

            LGSPGraph graph;
            LGSPActions actions;

            try
            {
                new LGSPBackend().CreateFromSpec(grgFile, "spBenchGraph", out graph, out actions);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if(initGRS != null)
                actions.ApplyGraphRewriteSequence(SequenceParser.ParseSequence(initGRS, actions));

#if DUMP_INITIALGRAPH
            IDumper dumper = new VCGDumper("spBenchTest-Initial.cs", VCGFlags.OrientTopToBottom);
            graph.Dump(dumper, new DumpInfo(graph.GetElementName));
            dumper.FinishDump();
#endif

            graph.AnalyzeGraph();
            if(ruleNamesForGenSP.Count != 0)
                actions.GenerateActions(ruleNamesForGenSP.ToArray());

            LGSPAction action = (LGSPAction) actions.GetAction(testActionName);
            if(action == null)
            {
                Console.WriteLine("Action \"" + testActionName + "\" not found!");
                return;
            }

            GenSPContext ctx = new GenSPContext(graph, actions, action);

            List<SearchPlanEdge> availEdges = new List<SearchPlanEdge>();

            // First add all preset operations in the same order as LGSPBackend
            foreach(SearchPlanNode node in ctx.SearchPlanGraph.Nodes)
            {
                if(!node.IsPreset) continue;

                foreach(SearchPlanEdge edge in node.OutgoingEdges)
                    availEdges.Add(edge);

                SearchOperation newOp = new SearchOperation(SearchOperationType.MaybePreset, node, ctx.SearchPlanGraph.Root, 0);
                ctx.Schedule.AddLast(newOp);
                ctx.NumVisitedElements++;
            }

            RecursiveGenAllSearchPlans(ctx, new SearchPlanEdge(SearchOperationType.MaybePreset, ctx.SearchPlanGraph.Root, ctx.SearchPlanGraph.Root, 0),
                availEdges, ctx.NumVisitedElements);

            Console.WriteLine("Num searchplans: " + num);

            if(reuseResFile != null)
            {
                using(StreamReader reader = new StreamReader(reuseResFile))
                {
                    reader.ReadLine();
                    String line;
                    int i = 0;
                    while((line = reader.ReadLine()) != null)
                    {
                        String[] parts = line.Split(';');
//                        Console.WriteLine(i + "/" + ctx.Results.Count + ": " + line);
                        if(i >= ctx.Results.Count)
                        {
                            Console.WriteLine("Old result file incompatible because of wrong number of searchplans! (" + i + "/" + ctx.Results.Count + "=" + line + ")");
                            return;
                        }
                        int time = int.Parse(parts[parts.Length - 1]);
                        if(time > ctx.MaxTime) ctx.MaxTime = time;
                        ctx.Results[i++].Time = time;
                    }
                }
            }

            using(StreamWriter writer = new StreamWriter(resultFile))
            {
                writer.WriteLine("planid;cflatvp;cbatzvp;cprodvs;cimplprodvs;csumvs;time");
                for(int i = 0; i < ctx.Results.Count; i++)
                {
                    SearchPlanResult res = ctx.Results[i];
                    writer.WriteLine((i + 1) + ";" + res.CostFlat.ToString("G20") + ";" + res.CostBatz.ToString("G20") + ";" + res.SPCostProduct.ToString("G20")
                        + ";" + res.SPCostImplProduct.ToString("G20") + ";" + res.SPCostSum.ToString("G20") + ";" + res.Time);

                    if(res.ID.Equals(ctx.LGSPSPID))
                    {
                        Console.WriteLine("Number of searchplan chosen by LGSPBackend: " + (i + 1));
                    }
                }
            }
            if(ctx.LGSPSPID == null)
            {
                Console.WriteLine("Original LGSP Searchplan not found! Searchplan is: " + ScheduleToString(ctx.LGSPSSP.Operations));
            }
            else
            {
                Console.WriteLine("Searchplan chosen by LGSPBackend: " + ScheduleToString(ctx.LGSPSSP.Operations));
            }

            foreach(SearchOperation op in ctx.LGSPSSP.Operations)
            {
                Console.WriteLine(SearchOpToString(op) + " = " + op.CostToEnd);
            }

            double timeScale = 400.0F / Math.Log10(ctx.MaxTime);
            int height = (int) Math.Round(timeScale * Math.Log10(ctx.MaxTime), MidpointRounding.AwayFromZero);
            float costScale = height / Math.Max(Math.Max(Math.Max(ctx.MaxCostFlat, ctx.MaxSPCostProduct), ctx.MaxSPCostSum), ctx.MaxCostBatz);

            if(ctx.Results.Count == 0 || height < 0 || height > 500)
            {
                Console.WriteLine("No useful data to be painted...");
                return;
            }

            SearchPlanResult[] results = ctx.Results.ToArray();
            Array.Sort<SearchPlanResult>(results);

            Bitmap bitmap = new Bitmap(results.Length, height + 1);
            Color timeCol = Color.Red;
            Color costCol = Color.Green;
            Color spCostProductCol = Color.Blue;
            Color spCostSumCol = Color.Purple;
            Color dashCol = Color.LightGray;
            for(int i = 0; i < results.Length; i++)
            {
                SearchPlanResult res = results[i];
                if(res.ID.Equals(ctx.LGSPSPID))
                {
                    for(int y = 0; y < height; y++)
                        if((y & 2) != 0) bitmap.SetPixel(i, y, dashCol);
                }
                bitmap.SetPixel(i, height - Math.Max((int) (timeScale * Math.Log10(res.Time)), 0), timeCol);
                bitmap.SetPixel(i, height - (int) (costScale * res.CostFlat), costCol);
                bitmap.SetPixel(i, height - (int) (costScale * res.SPCostProduct), spCostProductCol);
                bitmap.SetPixel(i, height - (int) (costScale * res.SPCostSum), spCostSumCol);
            }
            bitmap.Save("spBench-" + N + "-(" + BenchTimes + ").png");
        }
    }
}
