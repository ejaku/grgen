/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

#define MONO_MULTIDIMARRAY_WORKAROUND // must be equally set to the same flag in LGSPGraphStatistics.cs!
//#define LOG_ISOMORPHY_CHECKING
#define COMPILE_MATCHERS
//#define DUMP_COMPILED_MATCHER

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using de.unika.ipd.grGen.libGr;
#if LOG_ISOMORPHY_CHECKING
using System.IO;
#endif

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Helper class with stuff needed for IsIsomorph checking of graphs
    /// </summary>
    public class GraphMatchingState
    {
        public GraphMatchingState(LGSPGraph graph)
        {
            ++numGraphsComparedAtLeastOnce;

#if LOG_ISOMORPHY_CHECKING
            if(writer == null)
                writer = new StreamWriter("isocheck_log.txt");

            // print out the names of the type ids referenced in the interpretation plan when the first graph is initialized
            if(numGraphsComparedAtLeastOnce == 1)
            {
                foreach(NodeType nodeType in graph.Model.NodeModel.Types)
                {
                    writer.WriteLine(nodeType.TypeID + " is node type " + nodeType.Name);
                }
                foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
                {
                    writer.WriteLine(edgeType.TypeID + " is edge type " + edgeType.Name);
                }
                writer.Flush();
            }
#endif
        }

        /// <summary>
        /// A pattern graph created out of the original graph, for isomorphy checking
        /// not null if/after comparing type counts and vstructs was insufficient
        /// </summary>
        private PatternGraph patternGraph;

        /// <summary>
        /// The interpretation plan used for isomorphy checking, built from the pattern graph
        /// not null if/after comparing type counts and vstructs was insufficient
        /// </summary>
        private InterpretationPlan interpretationPlan;

        /// <summary>
        /// The changes counter of the graph when the interpretation plan was built
        /// (the compiled matcher depends on this, too)
        /// </summary>
        private long changesCounterAtInterpretationPlanBuilding;

        /// <summary>
        /// The compiled graph comparison matcher, built from the interpretation plan
        /// not null if/after the interpretation plan was emitted and compiled
        /// </summary>
        private GraphComparisonMatcher compiledMatcher;

        // just some statistic for debugging, tells how many iso checks where done with this graph as one partner
        public int numChecks = 0;

        /// <summary>
        ///Tells how many matches were carried out with this interpretation plan or compiled matcher
        /// </summary>
        private int numMatchings = 0;

        // object used to protect the matcher compilation process
        private static object compilationLock = new object();

        /// <summary>
        /// The graphs which were matched so often they gained the status of a candidate 
        /// for the next compilation run
        /// </summary>
        private static List<LGSPGraph> candidatesForCompilation = new List<LGSPGraph>();

        private static int TotalCandidateMatches()
        {
            int count = 0;
            for(int i = 0; i < candidatesForCompilation.Count; ++i)
            {
                count += candidatesForCompilation[i].matchingState.numMatchings;
            }
            return count;
        }

        /// <summary>
        /// We gather the oftenly compared "hot" graphs in a candidate set 
        /// for getting isomorphy-checked with a compiled matcher instead of an interpreted matcher
        /// </summary>
        private const int MATCHES_NEEDED_TO_BECOME_A_CANDIDATE_FOR_COMPILATION = 4;

        /// <summary>
        /// And when the candidates were compared often enough, we compile them.
        /// We can't do this often cause compilation is very expensive.
        /// Using ILGenerator to build a dynamic method would be much better, but is much harder, too :(
        /// </summary>
        private const int TOTAL_CANDIDATE_MATCHES_NEEDED_TO_START_A_COMPILATION = 1200;

        // just some statistics for debugging
        private static int numGraphsComparedAtLeastOnce = 0;
        private static int numInterpretationPlans = 0;
        private static int numCompiledMatchers = 0;
        private static int numCompilationPasses = 0;

#if LOG_ISOMORPHY_CHECKING
        private static StreamWriter writer;
#endif


        public bool IsIsomorph(LGSPGraph this_, LGSPGraph that, bool includingAttributes)
        {
            ++this_.matchingState.numChecks;
            ++that.matchingState.numChecks;

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Check " + this_.Name + " == " + that.Name);
            writer.Flush();
#endif

            if(this_ == that)
            {
#if LOG_ISOMORPHY_CHECKING
                writer.WriteLine("this == that, identical graphs are isomorph");
                writer.Flush();
#endif
                return true;
            }

            // compare number of elements per type
            if(!AreNumberOfElementsEqual(this_, that))
                return false;

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Undecided after type counts");
            writer.Flush();
#endif

            // ensure graphs are analyzed
            if(this_.statistics.vstructs == null || this_.changesCounterAtLastAnalyze != this_.ChangesCounter)
            {
                lock(this_.graphMatchingLock)
                {
                    if(this_.statistics.vstructs == null || this_.changesCounterAtLastAnalyze != this_.ChangesCounter)
                        this_.AnalyzeGraph();
                }
            }
            if(that.statistics.vstructs == null || that.changesCounterAtLastAnalyze != that.ChangesCounter)
            {
                lock(that.graphMatchingLock)
                {
                    if(that.statistics.vstructs == null || that.changesCounterAtLastAnalyze != that.ChangesCounter)
                        that.AnalyzeGraph();
                }
            }
 
            // compare analyze statistics
            if(!AreVstructsEqual(this_, that))
                return false;

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Undecided after vstructs comparison");
            writer.Flush();
#endif

            // invalidate outdated interpretation plans and compiled matchers
            if(this_.matchingState.interpretationPlan != null && this_.matchingState.changesCounterAtInterpretationPlanBuilding != this_.ChangesCounter)
            {
                lock(this_.graphMatchingLock)
                {
                    if(this_.matchingState.interpretationPlan != null && this_.matchingState.changesCounterAtInterpretationPlanBuilding != this_.ChangesCounter)
                    {
                        this_.matchingState.interpretationPlan = null;
                        this_.matchingState.patternGraph = null;
                        lock(GraphMatchingState.compilationLock)
                        {
                            GraphMatchingState.candidatesForCompilation.Remove(this_);
                        }
                        this_.matchingState.compiledMatcher = null;
                        this_.matchingState.numMatchings = 0;
                        this_.matchingState.numChecks = 0;
                    }
                }
            }
            if(that.matchingState.interpretationPlan != null && that.matchingState.changesCounterAtInterpretationPlanBuilding != that.ChangesCounter)
            {
                lock(that.graphMatchingLock)
                {
                    if(that.matchingState.interpretationPlan != null && that.matchingState.changesCounterAtInterpretationPlanBuilding != that.ChangesCounter)
                    {
                        that.matchingState.interpretationPlan = null;
                        that.matchingState.patternGraph = null;
                        lock(GraphMatchingState.compilationLock)
                        {
                            GraphMatchingState.candidatesForCompilation.Remove(that);
                        }
                        that.matchingState.compiledMatcher = null;
                        that.matchingState.numMatchings = 0;
                        that.matchingState.numChecks = 0;
                    }
                }
            }

            // they were the same? then we must try to match this in that, or that in this
            // if a compiled matcher is existing we use the compiled matcher
            // if an interpretation plan is existing we use the interpretation plan for matching
            // if none is existing for neither of the graphs, then we build an interpretation plan 
            // for the older graph and directly use it for matching thereafter
            // executing an interpretation plan or a compiled matcher is sufficient for isomorphy because 
            // - element numbers are the same 
            // - we match only exact types                
            bool result;
            bool matchedWithThis;
            if(this_.matchingState.compiledMatcher != null)
            {
                lock(that.graphMatchingLock)
                {
                    result = this_.matchingState.compiledMatcher.IsIsomorph(this_.matchingState.patternGraph, that, includingAttributes);
                }
                matchedWithThis = true;
#if LOG_ISOMORPHY_CHECKING
                writer.WriteLine("Using compiled interpretation plan of this " + this_.matchingState.compiledMatcher.Name);
#endif
            }
            else if(that.matchingState.compiledMatcher != null)
            {
                lock(this_.graphMatchingLock)
                {
                    result = that.matchingState.compiledMatcher.IsIsomorph(that.matchingState.patternGraph, this_, includingAttributes);
                }
                matchedWithThis = false;
#if LOG_ISOMORPHY_CHECKING
                writer.WriteLine("Using compiled interpretation plan of that " + that.matchingState.compiledMatcher.Name);
#endif
            }
            else if(this_.matchingState.interpretationPlan != null)
            {
                lock(that.graphMatchingLock)
                {
                    result = this_.matchingState.interpretationPlan.Execute(that, includingAttributes, null);
                }
                matchedWithThis = true;
#if LOG_ISOMORPHY_CHECKING
                writer.WriteLine("Using interpretation plan of this " + ((InterpretationPlanStart)this_.matchingState.interpretationPlan).ComparisonMatcherName);
#endif
            }
            else if(that.matchingState.interpretationPlan != null)
            {
                lock(this_.graphMatchingLock)
                {
                    result = that.matchingState.interpretationPlan.Execute(this_, includingAttributes, null);
                }
                matchedWithThis = false;
#if LOG_ISOMORPHY_CHECKING
                writer.WriteLine("Using interpretation plan of that " + ((InterpretationPlanStart)that.matchingState.interpretationPlan).ComparisonMatcherName);
#endif
            }
            else
            {
                // we build the interpretation plan for the older graph, 
                // assuming it will survive while the younger one is the candidate for purging
                if(this_.GraphId < that.GraphId)
                {
                    BuildInterpretationPlan(this_);
                    lock(that.graphMatchingLock)
                    {
                        result = this_.matchingState.interpretationPlan.Execute(that, includingAttributes, null);
                    }
                    matchedWithThis = true;
                }
                else
                {
                    BuildInterpretationPlan(that);
                    lock(this_.graphMatchingLock)
                    {
                        result = that.matchingState.interpretationPlan.Execute(this_, includingAttributes, null);
                    }
                    matchedWithThis = false;
                }
            }

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Result of matching: " + (result ? "Isomorph" : "Different"));
            writer.Flush();
#endif

            // update the statistics, and depending on the statistics we
            // - add candidats to the set of the matchers to be compiled
            // - trigger a compiler run
            if(matchedWithThis)
            {
                ++this_.matchingState.numMatchings;
                if(this_.matchingState.numMatchings == GraphMatchingState.MATCHES_NEEDED_TO_BECOME_A_CANDIDATE_FOR_COMPILATION)
                {
                    lock(GraphMatchingState.compilationLock)
                    {
                        GraphMatchingState.candidatesForCompilation.Add(this_);
                    }
                }
            }
            else
            {
                ++that.matchingState.numMatchings;
                if(that.matchingState.numMatchings == GraphMatchingState.MATCHES_NEEDED_TO_BECOME_A_CANDIDATE_FOR_COMPILATION)
                {
                    lock(GraphMatchingState.compilationLock)
                    {
                        GraphMatchingState.candidatesForCompilation.Add(that);
                    }
                }
            }

            CompileComparisonMatchersAsNeeded();

            return result;
        }

        // state/context for parameterless IsIsomorph executed by worker thread from thread pool
        public LGSPGraph graphToCheck;
        public IEnumerator<KeyValuePair<IGraph, SetValueType>> graphsToCheckAgainstIterator;
        public int iterationLock;
        public bool includingAttributes_;
        public bool wasIso;
        public LGSPGraph graphThatWasIso;

        // Called by worker thread for parallel isomorphy checking
        // Not for normal use!
        public void IsIsomorph()
        {
            while(Interlocked.CompareExchange(ref iterationLock, 1, 0) != 0)
            {
                Thread.SpinWait(10); //lock parallel enumeration with iteration lock
            }
            while(!wasIso && graphsToCheckAgainstIterator.MoveNext())
            {
                LGSPGraph that = (LGSPGraph)graphsToCheckAgainstIterator.Current.Key;
                Interlocked.Exchange(ref iterationLock, 0); //unlock parallel enumeration with iteration lock

                if(IsIsomorph(graphToCheck, that, includingAttributes_, WorkerPool.ThreadId))
                {
                    wasIso = true;
                    graphThatWasIso = that;
                }

                while(Interlocked.CompareExchange(ref iterationLock, 1, 0) != 0)
                {
                    Thread.SpinWait(10); //lock parallel enumeration with iteration lock
                }
            }
            Interlocked.Exchange(ref iterationLock, 0); //unlock parallel enumeration with iteration lock
        }

        // Version to be used by IsIsomorph(IDictionary<IGraph, SetValueType> graphsToCheckAgainst),
        // which is comparing a candidate against an entire set.
        // Not changing the metadata of this, matching each that inside this, not for normal use!
        private bool IsIsomorph(LGSPGraph this_, LGSPGraph that, bool includingAttributes, int threadId)
        {
            if(((LGSPGraph)that).matchingState == null)
                ((LGSPGraph)that).matchingState = new GraphMatchingState((LGSPGraph)that);

            ++that.matchingState.numChecks;

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Check " + this_.Name + " == " + that.Name + " on thread " + threadId);
            writer.Flush();
#endif

            // compare number of elements per type
            if(!AreNumberOfElementsEqual(this_, that))
                return false;

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Undecided after type counts");
            writer.Flush();
#endif

            // ensure that is analyzed, for this it was ensured by our caller
            if(that.statistics.vstructs == null || that.changesCounterAtLastAnalyze != that.ChangesCounter)
            {
                lock(that.graphMatchingLock)
                {
                    if(that.statistics.vstructs == null || that.changesCounterAtLastAnalyze != that.ChangesCounter)
                        that.AnalyzeGraph();
                }
            }

            // compare analyze statistics
            if(!AreVstructsEqual(this_, that))
                return false;

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Undecided after vstructs comparison");
            writer.Flush();
#endif

            // invalidate outdated interpretation plans and compiled matchers of that
            // not needed for this cause we always use the matchers of that, matching inside this
            if(that.matchingState.interpretationPlan != null && that.matchingState.changesCounterAtInterpretationPlanBuilding != that.ChangesCounter)
            {
                lock(that.graphMatchingLock)
                {
                    if(that.matchingState.interpretationPlan != null && that.matchingState.changesCounterAtInterpretationPlanBuilding != that.ChangesCounter)
                    {
                        that.matchingState.interpretationPlan = null;
                        that.matchingState.patternGraph = null;
                        lock(GraphMatchingState.compilationLock)
                        {
                            GraphMatchingState.candidatesForCompilation.Remove(that);
                        }
                        that.matchingState.compiledMatcher = null;
                        that.matchingState.numMatchings = 0;
                        that.matchingState.numChecks = 0;
                    }
                }
            }

            // they were the same? then we must try to match that in this
            // if a compiled matcher is existing we use the compiled matcher
            // if an interpretation plan is existing we use the interpretation plan for matching
            // if none is existing, then we build an interpretation plan for that 
            // and directly use it for matching thereafter
            // executing an interpretation plan or a compiled matcher is sufficient for isomorphy because 
            // - element numbers are the same 
            // - we match only exact types                
            bool result;
            if(that.matchingState.compiledMatcher != null)
            {
                // lock(this_.graphMatchingLock) was employed by our caller
                result = that.matchingState.compiledMatcher.IsIsomorph(that.matchingState.patternGraph, this_, includingAttributes, threadId);
#if LOG_ISOMORPHY_CHECKING
                writer.WriteLine("Using compiled interpretation plan of that " + that.matchingState.compiledMatcher.Name);
#endif
            }
            else if(that.matchingState.interpretationPlan != null)
            {
                // lock(this_.graphMatchingLock) was employed by our caller
                result = that.matchingState.interpretationPlan.Execute(this_, includingAttributes, null, threadId);
#if LOG_ISOMORPHY_CHECKING
                writer.WriteLine("Using interpretation plan of that " + ((InterpretationPlanStart)that.matchingState.interpretationPlan).ComparisonMatcherName);
#endif
            }
            else
            {
                // we build the interpretation plan for that
                BuildInterpretationPlan(that);
                // lock(this_.graphMatchingLock) was employed by our caller
                result = that.matchingState.interpretationPlan.Execute(this_, includingAttributes, null, threadId);
            }

#if LOG_ISOMORPHY_CHECKING
            writer.WriteLine("Result of matching: " + (result ? "Isomorph" : "Different"));
            writer.Flush();
#endif

            // update the statistics, and depending on the statistics we
            // - add candidats to the set of the matchers to be compiled
            // - it's on our caller to trigger a compiler run
            ++that.matchingState.numMatchings;
            if(that.matchingState.numMatchings == GraphMatchingState.MATCHES_NEEDED_TO_BECOME_A_CANDIDATE_FOR_COMPILATION)
            {
                lock(GraphMatchingState.compilationLock)
                {
                    GraphMatchingState.candidatesForCompilation.Add(that);
                }
            }

            return result;
        }

        private bool AreNumberOfElementsEqual(LGSPGraph this_, LGSPGraph that)
        {
            for(int i = 0; i < this_.nodesByTypeCounts.Length; ++i)
            {
                if(this_.nodesByTypeCounts[i] != that.nodesByTypeCounts[i])
                {
#if LOG_ISOMORPHY_CHECKING
                    writer.WriteLine(this_.Model.NodeModel.Types[i].Name + ":" + this_.nodesByTypeCounts[i] + " != " + that.nodesByTypeCounts[i]);
                    writer.WriteLine("out due to type of node count");
#endif
                    return false;
                }
                else
                {
#if LOG_ISOMORPHY_CHECKING
                    writer.WriteLine(this_.Model.NodeModel.Types[i].Name + ":" + this_.nodesByTypeCounts[i] + " == " + that.nodesByTypeCounts[i]);
#endif
                }
            }
            for(int i = 0; i < this_.edgesByTypeCounts.Length; ++i)
            {
                if(this_.edgesByTypeCounts[i] != that.edgesByTypeCounts[i])
                {
#if LOG_ISOMORPHY_CHECKING
                    writer.WriteLine(this_.Model.EdgeModel.Types[i].Name + ":" + this_.edgesByTypeCounts[i] + " != " + that.edgesByTypeCounts[i]);
                    writer.WriteLine("out due to type of edge count");
#endif
                    return false;
                }
                else
                {
#if LOG_ISOMORPHY_CHECKING
                    writer.WriteLine(this_.Model.EdgeModel.Types[i].Name + ":" + this_.edgesByTypeCounts[i] + " == " + that.edgesByTypeCounts[i]);
#endif
                }
            }

            return true;
        }

        private bool AreVstructsEqual(LGSPGraph this_, LGSPGraph that)
        {
            int numNodeTypes = this_.Model.NodeModel.Types.Length;
            int numEdgeTypes = this_.Model.EdgeModel.Types.Length;
            for(int sourceType = 0; sourceType < numNodeTypes; ++sourceType)
            {
                if(this_.nodesByTypeCounts[sourceType] == 0)
                {
#if LOG_ISOMORPHY_CHECKING
                    writer.WriteLine("source == 0");
#endif
                    continue;
                }

                for(int edgeType = 0; edgeType < numEdgeTypes; ++edgeType)
                {
                    if(this_.edgesByTypeCounts[edgeType] == 0)
                    {
#if LOG_ISOMORPHY_CHECKING
                        writer.WriteLine("edge == 0");
#endif
                        continue;
                    }

                    for(int targetType = 0; targetType < numNodeTypes; ++targetType)
                    {
                        if(this_.nodesByTypeCounts[targetType] == 0)
                        {
#if LOG_ISOMORPHY_CHECKING
                            writer.WriteLine("target == 0");
#endif
                            continue;
                        }

                        for(int direction = 0; direction < 2; ++direction)
                        {
#if MONO_MULTIDIMARRAY_WORKAROUND
                            int vthis = this_.statistics.vstructs[((sourceType * this_.statistics.dim1size + edgeType) * this_.statistics.dim2size + targetType) * 2 + direction];
                            int vthat = that.statistics.vstructs[((sourceType * this_.statistics.dim1size + edgeType) * this_.statistics.dim2size + targetType) * 2 + direction];
#else
                            int vthis = this_.statistics.vstructs[sourceType, edgeType, targetType, direction];
                            int vthat = that.statistics.vstructs[sourceType, edgeType, targetType, direction];
#endif
                            if(this_.Model.EdgeModel.Types[edgeType].Directedness != Directedness.Directed)
                            {
                                // for not directed edges the direction information is meaningless, even worse: random, so we must merge before comparing
#if MONO_MULTIDIMARRAY_WORKAROUND
                                vthis += this_.statistics.vstructs[((targetType * this_.statistics.dim1size + edgeType) * this_.statistics.dim2size + sourceType) * 2 + 1];
                                vthat += that.statistics.vstructs[((targetType * this_.statistics.dim1size + edgeType) * this_.statistics.dim2size + sourceType) * 2 + 1];
#else
                                vthis += this_.statistics.vstructs[targetType, edgeType, sourceType, 1];
                                vthat += that.statistics.vstructs[targetType, edgeType, sourceType, 1];
#endif
                                if(vthis != vthat)
                                {
#if LOG_ISOMORPHY_CHECKING
                                    writer.WriteLine(vthis + " != " + vthat);
                                    writer.WriteLine("out due to vstruct undirected");
#endif
                                    return false;
                                }
                                else
                                {
#if LOG_ISOMORPHY_CHECKING
                                    writer.WriteLine(vthis + " == " + vthat);
#endif
                                    continue;
                                }
                            }
                            else
                            {
                                if(vthis != vthat)
                                {
#if LOG_ISOMORPHY_CHECKING
                                    writer.WriteLine(vthis + " != " + vthat);
                                    writer.WriteLine("out due to vstruct");
#endif
                                    return false;
                                }
                                else
                                {
#if LOG_ISOMORPHY_CHECKING
                                    writer.WriteLine(vthis + " == " + vthat);
#endif
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        private static void BuildInterpretationPlan(LGSPGraph graph)
        {
            graph.matchingState.patternGraph = BuildPatternGraph(graph);
            PlanGraph planGraph = PlanGraphGenerator.GeneratePlanGraph(graph.Model, graph.statistics, graph.matchingState.patternGraph, 
                false, false, false, new Dictionary<PatternElement, SetValueType>());
            PlanGraphGenerator.MarkMinimumSpanningArborescence(planGraph, graph.matchingState.patternGraph.name, false);
            SearchPlanGraph searchPlanGraph = SearchPlanGraphGeneratorAndScheduler.GenerateSearchPlanGraph(planGraph);
            ScheduledSearchPlan scheduledSearchPlan = SearchPlanGraphGeneratorAndScheduler.ScheduleSearchPlan(
                searchPlanGraph, graph.matchingState.patternGraph, false, false);
            InterpretationPlanBuilder builder = new InterpretationPlanBuilder(scheduledSearchPlan, searchPlanGraph, graph.Model);
            graph.matchingState.interpretationPlan = builder.BuildInterpretationPlan("ComparisonMatcher_" + graph.GraphId);
            ++GraphMatchingState.numInterpretationPlans;
            graph.matchingState.changesCounterAtInterpretationPlanBuilding = graph.changesCounterAtLastAnalyze;
            Debug.Assert(graph.changesCounterAtLastAnalyze == graph.ChangesCounter);

#if LOG_ISOMORPHY_CHECKING
            SourceBuilder sb = new SourceBuilder();
            graph.matchingState.interpretationPlan.Dump(sb);
            writer.WriteLine();
            writer.WriteLine(sb.ToString());
            writer.WriteLine();
            writer.Flush();
#endif
        }

        /// <summary>
        /// Builds a pattern graph out of the graph.
        /// The pattern graph retains links to the original graph elements and uses them for attribute comparison.
        /// </summary>
        /// <param name="graph">The graph which is to be transfered into a pattern</param>
        /// <returns></returns>
        private static PatternGraph BuildPatternGraph(IGraph graph)
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
                    graph.Name + "_node_" + count, "node_" + count,
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
                    graph.Name + "_edge_" + count, "edge_" + count,
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
            {
                for(int j = 0; j < numNodes; ++j)
                {
                    homNodes[i, j] = false;
                }
            }

            bool[,] homEdges = new bool[numEdges, numEdges];
            for(int i = 0; i < numEdges; ++i)
            {
                for(int j = 0; j < numEdges; ++j)
                {
                    homEdges[i, j] = false;
                }
            }

            bool[,] homNodesGlobal = new bool[numNodes, numNodes];
            for(int i = 0; i < numNodes; ++i)
            {
                for(int j = 0; j < numNodes; ++j)
                {
                    homNodesGlobal[i, j] = false;
                }
            }

            bool[,] homEdgesGlobal = new bool[numEdges, numEdges];
            for(int i = 0; i < numEdges; ++i)
            {
                for(int j = 0; j < numEdges; ++j)
                {
                    homEdgesGlobal[i, j] = false;
                }
            }

            bool[] totallyHomNodes = new bool[numNodes];
            for(int i = 0; i < numNodes; ++i)
            {
                totallyHomNodes[i] = false;
            }

            bool[] totallyHomEdges = new bool[numEdges];
            for(int i = 0; i < numEdges; ++i)
            {
                totallyHomEdges[i] = false;
            }

            List<PatternCondition> pcs = new List<PatternCondition>();
            for(int i = 0; i < numNodes; ++i)
            {
                if(nodes[i].Type.NumAttributes > 0)
                {
                    pcs.Add(new PatternCondition(new expression.AreAttributesEqual(correspondingNodes[i], nodes[i]),
                        new string[] { nodes[i].name }, new string[] { }, new string[] { },
                        new PatternNode[] { nodes[i] }, new PatternEdge[] { }, new PatternVariable[] { }));
                }
            }
            for(int i = 0; i < numEdges; ++i)
            {
                if(edges[i].Type.NumAttributes > 0)
                {
                    pcs.Add(new PatternCondition(new expression.AreAttributesEqual(correspondingEdges[i], edges[i]),
                        new string[] { }, new string[] { edges[i].name }, new string[] { },
                        new PatternNode[] { }, new PatternEdge[] { edges[i] }, new PatternVariable[] { }));
                }
            }
            PatternCondition[] patternConditions = pcs.ToArray();

            PatternGraph patternGraph = new PatternGraph(
                graph.Name,
                nodes, edges,
                patternConditions,
                homNodes, homEdges,
                homNodesGlobal, homEdgesGlobal,
                totallyHomNodes, totallyHomEdges,
                correspondingNodes, correspondingEdges
            );
            foreach(PatternNode node in nodes)
            {
                node.pointOfDefinition = patternGraph;
            }
            foreach(PatternEdge edge in edges)
            {
                edge.pointOfDefinition = patternGraph;
            }

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

        public static void CompileComparisonMatchersAsNeeded()
        {
#if COMPILE_MATCHERS
            lock(compilationLock)
            {
                if(GraphMatchingState.TotalCandidateMatches() >= GraphMatchingState.TOTAL_CANDIDATE_MATCHES_NEEDED_TO_START_A_COMPILATION)
                {
                    CompileComparisonMatchers();
                }
            }
#endif
        }

        private static void CompileComparisonMatchers()
        {
            for(int i = GraphMatchingState.candidatesForCompilation.Count - 1; i >= 0; --i)
            {
                LGSPGraph graph = GraphMatchingState.candidatesForCompilation[i];
                if(graph.matchingState.changesCounterAtInterpretationPlanBuilding != graph.ChangesCounter)
                    GraphMatchingState.candidatesForCompilation.RemoveAt(i);
            }
            
            SourceBuilder sourceCode = new SourceBuilder();
            sourceCode.AppendFront("using System;\n"
                + "using System.Collections.Generic;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n\n");
            sourceCode.AppendFront("namespace de.unika.ipd.grGen.lgspComparisonMatchers\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            foreach(LGSPGraph graph in GraphMatchingState.candidatesForCompilation)
            {
                ((InterpretationPlanStart)graph.matchingState.interpretationPlan).Emit(sourceCode);
            }

            sourceCode.Append("}");

#if DUMP_COMPILED_MATCHER
            using(StreamWriter sw = new StreamWriter("comparison_matcher_" + GraphMatchingState.candidatesForCompilation[0].GraphId + ".cs"))
            sw.Write(sourceCode.ToString());
#endif

            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPGraph)).Location);
            compParams.GenerateInMemory = true;
            compParams.CompilerOptions = "/optimize";

            // building methods with MSIL would be highly preferable, but is much harder of course
            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                {
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                }
                throw new ArgumentException("Internal error: Illegal C# source code produced for graph comparison: " + errorMsg);
            }

            // create comparison matcher instances
            foreach(LGSPGraph graph in GraphMatchingState.candidatesForCompilation)
            {
                graph.matchingState.compiledMatcher = (GraphComparisonMatcher)compResults.CompiledAssembly.CreateInstance(
                    "de.unika.ipd.grGen.lgspComparisonMatchers.ComparisonMatcher_" + graph.GraphId);
                if(graph.matchingState.compiledMatcher == null)
                    throw new ArgumentException("Internal error: Generated assembly does not contain comparison matcher 'ComparisonMatcher_" + graph.GraphId + "'!");
                ++GraphMatchingState.numCompiledMatchers;
            }

            GraphMatchingState.candidatesForCompilation.Clear();
            ++GraphMatchingState.numCompilationPasses;
        }

        public static void EnsureIsAnalyzed(LGSPGraph this_)
        {
            if(this_.statistics.vstructs == null || this_.changesCounterAtLastAnalyze != this_.ChangesCounter)
            {
                this_.AnalyzeGraph();
            }
        }
    }
}
