using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models;
using de.unika.ipd.grGen.actions;
using de.unika.ipd.grGen.libGr;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Threading;

namespace spBench
{
    class SearchPlanResult : IComparable<SearchPlanResult>
    {
        public SearchPlanID ID;
        public float Cost;
        public float SPCost;
        public int Time;

        public SearchPlanResult(SearchPlanID id, float cost, float searchPlanCost, int time)
        {
            ID = id;
            Cost = cost;
            SPCost = searchPlanCost;
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
        public LGSPAction Action;
        public SearchPlanID SearchPlanID = new SearchPlanID();
        public float SearchPlanCost = 0;
        public LinkedList<SearchOperation> Schedule = new LinkedList<SearchOperation>();
        public int NumElements;
        public SearchPlanGraph SearchPlanGraph;
        public SearchPlanGraph[] NegSPGraphs;
//        public String[][] NegNeededElements;
        public Dictionary<String, List<int>> ElemToNegs;
        public Dictionary<String, bool>[] NegNeededElements;
        public SearchPlanEdge[] NegEdges;
        public bool[] NegVisited;

        public int CurNegPattern = -1;
        public List<SearchPlanEdge> AvailEdgesBeforeNegPattern = null;

        public Dictionary<String, bool> VisitedElements = new Dictionary<string, bool>();

        public float MaxCost = 0;
        public float MaxSPCost = 0;
        public int MaxTime = 0;
        public List<SearchPlanResult> Results = new List<SearchPlanResult>();

        public ScheduledSearchPlan LibGrSSP;
        public SearchPlanID LibGrSPID = null;

        public GenSPContext(LGSPGraph graph, LGSPMatcherGenerator matchGen, LGSPAction action, SearchPlanGraph spGraph, SearchPlanGraph[] negSPGraphs,
//            String[][] negNeededElements, ScheduledSearchPlan libGrSSP)
            Dictionary<String, List<int>> elemToNegs, Dictionary<String, bool> negNeededElements, SearchPlanEdge[] negEdges, ScheduledSearchPlan libGrSSP)
        {
            Graph = graph;
            MatchGen = matchGen;
            Action = action;
            SearchPlanGraph = spGraph;
            NegSPGraphs = negSPGraphs;
            ElemToNegs = elemToNegs;
            NegNeededElements = negNeededElements;
            NegEdges = negEdges;
            LibGrSSP = libGrSSP;

            NumElements = spGraph.Nodes.Length + 1;
            NegVisited = new bool[NegSPGraphs.Length];      // all elements initialized to false
        }
    }

    class SPBench
    {
        public static IDumperFactory DumperFactory;
        public static DumpInfo DumpInfo;
        public static int N = 40000;
        public static int BenchTimes = 1;
        public static int BenchTimeout = 30000;

        static int BenchmarkActionOnce(LGSPGraph orgGraph, LGSPAction action)
        {
            LGSPGraph graph = (LGSPGraph) orgGraph.Clone("tempGraph");
            TestActions actions = new TestActions(graph, DumperFactory, Assembly.GetAssembly(typeof(MutexModel)).Location, Assembly.GetAssembly(typeof(TestActions)).Location);
            actions.ReplaceAction(action.Name, action);

            Sequence seq = Sequence.NewMax(Sequence.NewConcat(Sequence.NewRule(actions.GetAction("takeRule"), false),
                Sequence.NewConcat(Sequence.NewRule(actions.GetAction("releaseRule"), false), Sequence.NewRule(actions.GetAction("giveRule"), false), false),
                false), N);

            PerformanceInfo perfInfo;
            actions.ApplyGraphRewriteSequence(seq, out perfInfo);
            return perfInfo.TotalTimeMS;
        }

        class BenchObject
        {
            public LGSPGraph Graph;
            public LGSPAction Action;
            public int Time;

            public BenchObject(LGSPGraph graph, LGSPAction action)
            {
                Graph = graph;
                Action = action;
            }
        }

        static void BenchmarkAction(Object param)
        {
            BenchObject benchObject = (BenchObject) param;
            int totaltime = 0;
            for(int i = 0; i < BenchTimes; i++)
            {
                totaltime += BenchmarkActionOnce(benchObject.Graph, benchObject.Action);
            }
            benchObject.Time = totaltime / BenchTimes;
        }

        static int num = 0;

        static void SearchPlanFinished(GenSPContext ctx)
        {
            num++;
//            if(num < 22) return;
//            if(num != 256) return;
            if(num != 65 && num != 243 && num != 256) return;
            Console.Write("{0,4}. ", num);
            float spcost = 0;
            float cost = 0;
            int len = 0;
            foreach(SearchOperation searchOp in ctx.Schedule)
            {
                String typeStr = "  ";
                SearchPlanNode src = searchOp.SourceSPNode as SearchPlanNode;
                SearchPlanNode tgt = searchOp.Element as SearchPlanNode;
                switch(searchOp.Type)
                {
                    case SearchOperationType.Outgoing: typeStr = src.Name + "-" + tgt.Name + "->"; break;
                    case SearchOperationType.Incoming: typeStr = src.Name + "<-" + tgt.Name + "-"; break;
                    case SearchOperationType.ImplicitSource: typeStr = "<-" + src.Name + "-" + tgt.Name; break;
                    case SearchOperationType.ImplicitTarget: typeStr = "-" + src.Name + "->" + tgt.Name; break;
                    case SearchOperationType.Lookup: typeStr = "*" + tgt.Name; break;
                    case SearchOperationType.Preset: typeStr = "p" + tgt.Name; break;
                    case SearchOperationType.NegPreset: typeStr = "np" + tgt.Name; break;
                    case SearchOperationType.Condition: typeStr = " ?"; break;
                    case SearchOperationType.NegativePattern: typeStr = " !"; break;
                }

                Console.Write(" " + typeStr);
                len += typeStr.Length + 1; 
                spcost += searchOp.CostToEnd;
                cost += (float) Math.Exp(spcost);
            }
            cost = (float) Math.Log(cost);
            for(int i = len; i < 50; i++) Console.Write(' ');
            Console.Write(" => cost = {0,9:N4} spcost = {1,9:N4}", cost, spcost);
/*            if(spcost > 10)
            {
                Console.WriteLine(" to high");
                return;
            }*/

            SearchOperation[] ops = new SearchOperation[ctx.Schedule.Count];
            ctx.Schedule.CopyTo(ops, 0);
            ScheduledSearchPlan ssp = new ScheduledSearchPlan(ops, ctx.Action.RulePattern.PatternGraph.HomomorphicNodes, spcost);
            ctx.MatchGen.CalculateNeededMaps(ssp);
//            String outputName = num == 256 ? "badcase.cs" : null;
            String outputName = null;
            LGSPAction newAction = ctx.MatchGen.GenerateMatcher(ssp, ctx.Action, Assembly.GetAssembly(typeof(MutexModel)).Location,
                Assembly.GetAssembly(typeof(TestActions)).Location, outputName);

            Thread benchThread = new Thread(new ParameterizedThreadStart(BenchmarkAction));
            BenchObject benchObject = new BenchObject(ctx.Graph, newAction);
            benchThread.Start(benchObject);
            if(!benchThread.Join(BenchTimeout * BenchTimes))
            {
                benchThread.Abort();
                benchObject.Time = BenchTimeout;
            }

            int time = benchObject.Time;
//            int time = BenchmarkAction(ctx.Graph, newAction);

            Console.WriteLine(" time = {0,5} ms", time);
            ctx.Results.Add(new SearchPlanResult((SearchPlanID) ctx.SearchPlanID.Clone(), cost, spcost, time));
            if(cost > ctx.MaxCost) ctx.MaxCost = cost;
            if(spcost > ctx.MaxSPCost) ctx.MaxSPCost = spcost;
            if(time > ctx.MaxTime) ctx.MaxTime = time;
            foreach(SearchPlanNode node in ctx.SearchPlanGraph.Nodes)
                node.Visited = false;
        }

        static void RecursiveIncludeNegativePattern(GenSPContext ctx, SearchPlanEdge nextEdge, List<SearchPlanEdge> oldAvailEdges, int nextLibGrIndex)
        {

        }

        static void RecursiveGenAllSearchPlans(GenSPContext ctx, SearchPlanEdge nextEdge, List<SearchPlanEdge> oldAvailEdges, int nextLibGrIndex)
        {
            if(ctx.VisitedElements.Count + 1 == ctx.NumElements)
            {
                SearchPlanNode curNodeGammel = nextEdge.Target;
                curNodeGammel.Visited = false;                                // inverted logic
                SearchPlanFinished(ctx);
                if(nextLibGrIndex >= 0)
                    ctx.LibGrSPID = (SearchPlanID) ctx.SearchPlanID.Clone();
                curNodeGammel.Visited = true;
                return;
            }
            List<SearchPlanEdge> availableEdges = new List<SearchPlanEdge>(oldAvailEdges);
            availableEdges.Remove(nextEdge);
            SearchPlanNode curNode = nextEdge.Target;
            curNode.Visited = false;                                // inverted logic
            ctx.VisitedElements[curNode.Name] = true;
            List<int> relatedNegs;
            if(ctx.ElemToNegs.TryGetValue(curNode.Name, out relatedNegs))
            {
                foreach(int negnum in relatedNegs)
                {
                    ctx.NegNeededElements[negnum].Remove(curNode.Name);
                }

                // check negative patterns available
                for(int i = 0; i < ctx.NegSPGraphs.Length; i++)
                {
                    if(ctx.NegVisited[i]) continue;
                    if(ctx.NegNeededElements[i].Count == 0)
                    {
                        availableEdges.Add(ctx.NegEdges[i]);
                        ctx.NegVisited[i] = true;
                    }
                }
            }
            foreach(SearchPlanEdge outEdge in curNode.OutgoingEdges)
            {
                if(!outEdge.Target.Visited) continue;               // inverted logic
                availableEdges.Add(outEdge);
            }

            for(int i = 0; i < availableEdges.Count; i++)
            {
                if(!availableEdges[i].Target.Visited) continue;     // inverted logic
                ctx.SearchPlanID.SetNextDecision(i);
                ctx.SearchPlanCost += availableEdges[i].Cost;
                SearchOperation newOp = new SearchOperation((SearchOperationType) availableEdges[i].Type, availableEdges[i].Target,
                    availableEdges[i].Source, availableEdges[i].Cost);
                ctx.Schedule.AddLast(newOp);
                int newLibGrIndex = -1;
                if(nextLibGrIndex >= 0 && nextLibGrIndex < ctx.LibGrSSP.Operations.Length)
                {
                    // Check whether same operation at the same time was chosen by the lgsp matcher generator
                    SearchOperation libGrOp = ctx.LibGrSSP.Operations[nextLibGrIndex];
                    if(newOp.Type == libGrOp.Type && (newOp.SourceSPNode == null && libGrOp.SourceSPNode == null
                            || newOp.SourceSPNode != null && libGrOp.SourceSPNode != null && newOp.SourceSPNode.Name == libGrOp.SourceSPNode.Name))
                    {
                        switch(newOp.Type)
                        {
                            case SearchOperationType.ImplicitSource:
                            case SearchOperationType.Lookup:
                            case SearchOperationType.Outgoing:
                            case SearchOperationType.ImplicitTarget:
                            case SearchOperationType.Incoming:
                            {
                                SearchPlanNode newOpElem = (SearchPlanNode) newOp.Element;
                                SearchPlanNode libGrOpElem = (SearchPlanNode) libGrOp.Element;
                                if(newOpElem.Name == libGrOpElem.Name)
                                    newLibGrIndex = nextLibGrIndex + 1;
                                break;
                            }
                            case SearchOperationType.NegativePattern:     // check in negative pattern recursive function
                            {
//                                ScheduledSearchPlan negSSP = (ScheduledSearchPlan) newOp.Element;
                                newLibGrIndex = nextLibGrIndex;
                            }
                        }
                    }                       
                }
                if(newOp.Type == SearchOperationType.NegativePattern)
                    RecursiveIncludeNegativePattern(ctx, availableEdges[i], availableEdges, newLibGrIndex);
                else
                    RecursiveGenAllSearchPlans(ctx, availableEdges[i], availableEdges, newLibGrIndex);
                ctx.SearchPlanID.RevertLastDecision();
                ctx.SearchPlanCost -= availableEdges[i].Cost;
                ctx.Schedule.RemoveLast();
            }

            if(ctx.ElemToNegs.TryGetValue(curNode.Name, out relatedNegs))
            {
                foreach(int negnum in relatedNegs)
                {
                    ctx.NegNeededElements[negnum].Add(curNode.Name);
                }

                // check negative patterns visited but not available anymore
                for(int i = 0; i < ctx.NegSPGraphs.Length; i++)
                {
                    if(!ctx.NegVisited[i]) continue;
                    if(ctx.NegNeededElements[i].Count != 0)
                        ctx.NegVisited[i] = false;
                }
            }

            curNode.Visited = true;                                 // inverted logic
            ctx.VisitedElements.Remove(curNode.Name);
        }

        static SearchPlanGraph GenSPGraphFromPlanGraph(PlanGraph planGraph, bool[,] homomorphicNodes)
        {
            SearchPlanNode root = new SearchPlanNode(PlanNodeType.Root, 0, "search plan root", 0);
            SearchPlanNode[] nodes = new SearchPlanNode[planGraph.Nodes.Length];
//            SearchPlanEdge[] edges = new SearchPlanEdge[planGraph.Nodes.Length - 1 + 1];    // +1 for root
            SearchPlanEdge[] edges = new SearchPlanEdge[planGraph.Edges.Length];
            Dictionary<PlanNode, SearchPlanNode> planMap = new Dictionary<PlanNode, SearchPlanNode>(planGraph.Nodes.Length);
            planMap.Add(planGraph.Root, root);
            int i = 0;
            foreach(PlanNode node in planGraph.Nodes)
            {
                if(node.NodeType == PlanNodeType.Edge)
                    nodes[i] = new SearchPlanEdgeNode(node.TypeID, node.Name, node.ElementID, null, null);
                else
                    nodes[i] = new SearchPlanNodeNode(node.TypeID, node.Name, node.ElementID);
                planMap.Add(node, nodes[i]);
                nodes[i].Visited = true;        // inverted logic needed, as for the matcher generator all visited false must be false at startup
                i++;
            }
            i = 0;
            foreach(PlanEdge edge in planGraph.Edges)
            {
                edges[i] = new SearchPlanEdge(edge.Type, planMap[edge.Source], planMap[edge.Target], edge.Cost);
                planMap[edge.Source].OutgoingEdges.Add(edges[i]);
                i++;
            }
            foreach(PlanNode node in planGraph.Nodes)
            {
                if(node.NodeType != PlanNodeType.Edge) continue;
                SearchPlanEdgeNode spedgenode = (SearchPlanEdgeNode) planMap[node];
                spedgenode.PatternEdgeSource = (SearchPlanNodeNode) planMap[node.PatternEdgeSource];
                spedgenode.PatternEdgeTarget = (SearchPlanNodeNode) planMap[node.PatternEdgeTarget];
                spedgenode.PatternEdgeSource.OutgoingPatternEdges.AddLast(spedgenode);
                spedgenode.PatternEdgeTarget.IncomingPatternEdges.AddLast(spedgenode);
            }
            return new SearchPlanGraph(root, nodes, edges, planGraph.Conditions, homomorphicNodes);
        }

        public static ScheduledSearchPlan GenerateLibGrSearchPlan(LGSPGraph graph, LGSPAction action, LGSPMatcherGenerator matchGen)
        {
            PlanGraph planGraph = matchGen.GeneratePlanGraph(graph, action.RulePattern.PatternGraph, false);
            //            PlanGraph planGraph = GenerateTestCase2();

            matchGen.MarkMinimumSpanningArborescence(planGraph, action.Name);

            SearchPlanGraph searchPlanGraph = matchGen.GenerateSearchPlanGraph(planGraph, action.RulePattern.PatternGraph.HomomorphicNodes);

            SearchPlanGraph[] negSearchPlanGraphs = new SearchPlanGraph[action.RulePattern.NegativePatternGraphs.Length];
            for(int i = 0; i < action.RulePattern.NegativePatternGraphs.Length; i++)
            {
                PlanGraph negPlanGraph = matchGen.GeneratePlanGraph(graph, action.RulePattern.NegativePatternGraphs[i], true);
                matchGen.MarkMinimumSpanningArborescence(negPlanGraph, action.Name + "_neg_" + (i + 1));
                negSearchPlanGraphs[i] = matchGen.GenerateSearchPlanGraph(negPlanGraph, action.RulePattern.NegativePatternGraphs[i].HomomorphicNodes);
            }

            ScheduledSearchPlan scheduledSearchPlan = matchGen.ScheduleSearchPlan(searchPlanGraph, negSearchPlanGraphs);
            matchGen.CalculateNeededMaps(scheduledSearchPlan);
            return scheduledSearchPlan;
        }

        public static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                if(!int.TryParse(args[0], out N))
                {
                    Console.WriteLine("Usage: spBench [<n> [<times> [<timeout>]]]");
                    return;
                }
                if(args.Length > 1)
                {
                    if(!int.TryParse(args[1], out BenchTimes))
                    {
                        Console.WriteLine("Usage: spBench [<n> [<times> [<timeout>]]]");
                        return;
                    }
                    if(args.Length > 2)
                    {
                        if(!int.TryParse(args[2], out BenchTimeout))
                        {
                            Console.WriteLine("Usage: spBench [<n> [<times> [<timeout>]]]");
                            return;
                        }
                    }
                }
            }

//            Console.WindowWidth = 120;
            LGSPBackend lgspBackend = new LGSPBackend();
            LGSPGraph graph = (LGSPGraph) lgspBackend.CreateGraph(new MutexModel(), "spBench graph");
            DumpInfo = new DumpInfo(graph.GetElementName);
            DumperFactory = new VCGDumperFactory(VCGFlags.OrientTopToBottom, DumpInfo);
            TestActions actions = new TestActions(graph, DumperFactory, Assembly.GetAssembly(typeof(MutexModel)).Location, Assembly.GetAssembly(typeof(TestActions)).Location);
            INode p1 = graph.AddNode(MutexNodeType_Process.typeVar);
            INode p2 = graph.AddNode(MutexNodeType_Process.typeVar);
            graph.AddEdge(MutexEdgeType_next.typeVar, p1, p2);
            graph.AddEdge(MutexEdgeType_next.typeVar, p2, p1);

            // TODO: Write rewrite sequence parser to make this easier
            Sequence seq = Sequence.NewConcat(Sequence.NewMax(Sequence.NewRule(actions.GetAction("newRule"), false), N - 2),
                Sequence.NewConcat(Sequence.NewRule(actions.GetAction("mountRule"), false),
                Sequence.NewMax(Sequence.NewRule(actions.GetAction("requestRule"), false), N), false), false);
            PerformanceInfo perfInfo;
            actions.ApplyGraphRewriteSequence(seq, out perfInfo);
/*            IDumper dumper = DumperFactory.CreateDumper("spBenchTest-Initial");
            graph.Dump(dumper, DumpInfo);
            dumper.FinishDump();*/

            graph.AnalyzeGraph();

            LGSPAction action = (LGSPAction) actions.GetAction("takeRule");

            LGSPMatcherGenerator matchGen = new LGSPMatcherGenerator(graph.Model);
            SearchPlanGraph searchPlanGraph = GenSPGraphFromPlanGraph(matchGen.GeneratePlanGraph(graph, action.RulePattern.PatternGraph, false),
                action.RulePattern.PatternGraph.HomomorphicNodes);
            SearchPlanGraph[] negSPGraphs = new SearchPlanGraph[action.RulePattern.NegativePatternGraphs.Length];
//            String[][] negNeededElems = new String[action.RulePattern.NegativePatternGraphs.Length][];
            Dictionary<String, bool>[] negNeededElems = new Dictionary<String, bool>[action.RulePattern.NegativePatternGraphs.Length];
            SearchPlanEdge[] negEdges = new SearchPlanEdge[action.RulePattern.NegativePatternGraphs.Length];
            for(int i = 0; i < action.RulePattern.NegativePatternGraphs.Length; i++)
            {
                negSPGraphs[i] = GenSPGraphFromPlanGraph(matchGen.GeneratePlanGraph(graph, action.RulePattern.NegativePatternGraphs[i], true),
                    action.RulePattern.NegativePatternGraphs[i].HomomorphicNodes);
//                List<String> neededElemNames = new List<string>();
                Dictionary<String, bool> neededElemNames = new Dictionary<String, bool>();
                foreach(PatternNode node in action.RulePattern.NegativePatternGraphs[i].Nodes)
                {
                    if(node.PatternElementType != PatternElementType.NegElement)
                        neededElemNames.Add(node.Name);
                }
                foreach(PatternEdge edge in action.RulePattern.NegativePatternGraphs[i].Edges)
                {
                    if(edge.PatternElementType != PatternElementType.NegElement)
                        neededElemNames.Add(edge.Name);
                }
//                negNeededElems[i] = neededElemNames.ToArray();
                negNeededElems[i] = neededElemNames;
                negEdges[i] = new SearchPlanEdge((PlanEdgeType) SearchOperationType.NegativePattern, searchPlanGraph.Root,
                    new SearchPlanNode((PlanEdgeType) SearchOperationType.NegativePattern, -1, "negPattern " + i, i), 0);
            }
            Dictionary<String, List<int>> elemToNegs = new Dictionary<String, List<int>>();
            foreach(PatternNode node in action.RulePattern.PatternGraph.Nodes)
            {
                for(int i = 0; i < negSPGraphs.Length; i++)
                {
                    if(negNeededElems[i].ContainsKey(node.Name))
                    {
                        List<int> negList;
                        if(!elemToNegs.TryGetValue(node.Name, out negList))
                        {
                            negList = new List<int>();
                            negList.Add(i);
                        }
                        else negList.Add(i);
                    }
                }
            }

            GenSPContext ctx = new GenSPContext(graph, matchGen, action, searchPlanGraph, negSPGraphs, elemToNegs, negNeededElems, negEdges,
                GenerateLibGrSearchPlan(graph, action, matchGen));
            RecursiveGenAllSearchPlans(ctx, new SearchPlanEdge(PlanEdgeType.Preset, searchPlanGraph.Root, searchPlanGraph.Root, 0),
                new List<SearchPlanEdge>(), 0);

            double timeScale = 1;
            /*if(Math.Log10(ctx.MaxTime) > 700)*/ timeScale = 350.0F / Math.Log10(ctx.MaxTime);
            int height = (int) Math.Round(timeScale * Math.Log10(ctx.MaxTime), MidpointRounding.AwayFromZero);
            float costScale = height / Math.Max(ctx.MaxCost, ctx.MaxSPCost);

            using(StreamWriter writer = new StreamWriter("spBench-" + N + "-(" + BenchTimes + ").txt"))
            {
                for(int i = 0; i < ctx.Results.Count; i++)
                {
                    SearchPlanResult res = ctx.Results[i];
                    writer.WriteLine((i+1) + " " + res.Cost + " " + res.SPCost + " " + res.Time);
                }
            }

            SearchPlanResult[] results = ctx.Results.ToArray();
            Array.Sort<SearchPlanResult>(results);

            Bitmap bitmap = new Bitmap(results.Length, height + 1); //, System.Drawing.Imaging.PixelFormat.Format4bppIndexed);
            Color timeCol = Color.Red;
            Color costCol = Color.Green;
            Color spCostCol = Color.Blue;
            Color dashCol = Color.LightGray;
            for(int i = 0; i < results.Length; i++)
            {
                SearchPlanResult res = results[i];
                if(res.ID.Equals(ctx.LibGrSPID))
                {
                    for(int y = 0; y < height; y++)
                        if((y & 2) != 0) bitmap.SetPixel(i, y, dashCol);
                }
//                Console.WriteLine("sorted {0}. cost = {1} (y={2}), time = {3} (y={4})", i, res.EstimatedCost, (int) (costScale * res.EstimatedCost),
//                    Math.Log10(res.Time), (int) (timeScale * Math.Log10(res.Time)));
                bitmap.SetPixel(i, height - Math.Max((int) (timeScale * Math.Log10(res.Time)), 0), timeCol);
                bitmap.SetPixel(i, height - (int) (costScale * res.Cost), costCol);
                bitmap.SetPixel(i, height - (int) (costScale * res.SPCost), spCostCol);
            }
            bitmap.Save("spBench-" + N + "-(" + BenchTimes + ").png");
        }
    }
}
