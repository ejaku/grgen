/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;
using System.Threading;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the IGraphProcessingEnvironment, to be used with LGSPGraphs.
    /// </summary>
    public class LGSPGraphProcessingEnvironment : LGSPSubactionAndOutputAdditionEnvironment, IGraphProcessingEnvironment
    {
        private readonly LGSPTransactionManager transactionManager;
        public readonly LGSPDeferredSequencesManager sequencesManager;
        
        private IUserProxyForSequenceExecution userProxy;
        private IUserProxyForSequenceExecution compliantUserProxy;

        readonly List<object[]> emptyList = new List<object[]>(); // performance optimization (for ApplyRewrite, empty list is only created once)


        public LGSPGraphProcessingEnvironment(LGSPGraph graph, LGSPActions actions)
            : base(graph, actions)
        {
            transactionManager = new LGSPTransactionManager(this);
            sequencesManager = new LGSPDeferredSequencesManager();
            LGSPGlobalVariables globalVariables = (LGSPGlobalVariables)graph.GlobalVariables;
            globalVariables.SetClearVariables(true, graph);
            globalVariables.FillCustomCommandDescriptions(customCommandsToDescriptions);
            compliantUserProxy = new CompliantUserProxyForSequenceExecution(this);
        }

        // esp. called when actions are added, it is possible to create a graph processing environment only with a graph and null actions (it is also possible to set the actions directly afterwards instead of calling Initialize)
        public override void Initialize(LGSPGraph graph, LGSPActions actions)
        {
            ((LGSPGlobalVariables)Graph.GlobalVariables).StopListening(Graph);
            base.Initialize(graph, actions);
            ((LGSPGlobalVariables)graph.GlobalVariables).StartListening(graph);
        }

        public override void SwitchToSubgraph(IGraph newGraph)
        {
            ((LGSPGlobalVariables)Graph.GlobalVariables).StopListening(Graph);
            base.SwitchToSubgraph(newGraph);
            ((LGSPGlobalVariables)newGraph.GlobalVariables).StartListening(newGraph);
        }

        public override IGraph ReturnFromSubgraph()
        {
            IGraph oldGraph = base.ReturnFromSubgraph();
            ((LGSPGlobalVariables)oldGraph.GlobalVariables).StopListening(oldGraph);
            ((LGSPGlobalVariables)Graph.GlobalVariables).StartListening(Graph);
            return oldGraph;
        }

        public ITransactionManager TransactionManager
        { 
            get { return transactionManager; }
        }

        public override void Custom(params object[] args)
        {
            if(args.Length == 0)
                throw new ArgumentException("No command given");

            String command = (String)args[0];
            switch(command)
            {
            case "set_max_matches":
                base.Custom(args);
                break;

            case "enable_assertions":
                base.Custom(args);
                break;

            default:
                ((LGSPGlobalVariables)graph.GlobalVariables).Custom(graph, args); // throws exception if custom command is unknown to global variables, too
                break;
            }
        }

        public override IMatches Match(IAction action, object[] arguments, int localMaxMatches, bool special, List<FilterCall> filters, bool fireDebugEvents)
        {
            int curMaxMatches = (localMaxMatches > 0) ? localMaxMatches : MaxMatches;

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            PerformanceInfo.StartLocal();
#endif
            IMatches matches = action.Match(this, curMaxMatches, arguments);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopMatch();
#endif
            PerformanceInfo.MatchesFound += matches.Count;

            if(fireDebugEvents)
            {
                if(matches.Count > 0)
                    MatchedBeforeFiltering(matches);
            }

            for(int i = 0; i < filters.Count; ++i)
            {
                FilterCall filterCall = filters[i];
                if(filterCall is FilterCallWithLambdaExpression)
                {
                    FilterCallWithLambdaExpression lambdaExpressionFilterCall = (FilterCallWithLambdaExpression)filterCall;
                    Filter(matches, lambdaExpressionFilterCall);
                }
                else
                {
                    FilterCallWithArguments filterCallWithArguments = (FilterCallWithArguments)filterCall;
                    action.Filter(this, matches, filterCallWithArguments);
                }
            }

            if(fireDebugEvents)
            {
                if(matches.Count > 0) // ensure that Matched is only called when a match exists
                    MatchedAfterFiltering(matches, special);
            }

            return matches;
        }

        /// <summary>
        /// Filters the matches of a rule (all) call with a lambda expression filter (call).
        /// </summary>
        /// <param name="matches">The matches of the rule</param>
        /// <param name="filter">The lambda expression filter to apply</param>
        public void Filter(IMatches matches, FilterCallWithLambdaExpression filter)
        {
            if(filter.PlainName == "assign")
                FilterAssign(matches, filter);
            else if(filter.PlainName == "removeIf")
                FilterRemoveIf(matches, filter);
            else if(filter.PlainName == "assignStartWithAccumulateBy")
                FilterAssignStartWithAccumulateBy(matches, filter);
            else
                throw new Exception("Unknown lambda expression filter call (available are assign, removeIf, assignStartWithAccumulateBy)");
        }

        public void FilterAssign(IMatches matches, FilterCallWithLambdaExpression filterCall)
        {
            if(filterCall.arrayAccess != null)
            {
                List<IMatch> matchListCopy = new List<IMatch>();
                foreach(IMatch match in matches)
                {
                    matchListCopy.Add(match.Clone());
                }
                filterCall.arrayAccess.SetVariableValue(matchListCopy, this);
            }
            int index = 0;
            foreach(IMatch match in matches)
            {
                if(filterCall.index != null)
                    filterCall.index.SetVariableValue(index, this);
                filterCall.element.SetVariableValue(match, this);
                object result = filterCall.lambdaExpression.Evaluate(this);
                match.SetMember(filterCall.Entity, result);
                ++index;
            }
        }

        public void FilterRemoveIf(IMatches matches, FilterCallWithLambdaExpression filterCall)
        {
            List<IMatch> matchList = matches.ToList();
            if(filterCall.arrayAccess != null)
            {
                List<IMatch> matchListCopy = new List<IMatch>(matchList);
                filterCall.arrayAccess.SetVariableValue(matchListCopy, this);
            }
            for(int index = 0; index < matchList.Count; ++index)
            {
                if(filterCall.index != null)
                    filterCall.index.SetVariableValue(index, this);
                IMatch match = matchList[index];
                filterCall.element.SetVariableValue(match, this);
                object result = filterCall.lambdaExpression.Evaluate(this);
                if((bool)result)
                    matchList[index] = null;
            }
            matches.FromList();
        }

        public void FilterAssignStartWithAccumulateBy(IMatches matches, FilterCallWithLambdaExpression filterCall)
        {
            List<IMatch> matchListCopy = null;
            if(filterCall.initArrayAccess != null || filterCall.arrayAccess != null)
            {
                matchListCopy = new List<IMatch>();
                foreach(IMatch match in matches)
                {
                    matchListCopy.Add(match.Clone());
                }
            }
            if(filterCall.initArrayAccess != null)
                filterCall.initArrayAccess.SetVariableValue(matchListCopy, this);
            if(filterCall.arrayAccess != null)
                filterCall.arrayAccess.SetVariableValue(matchListCopy, this);

            filterCall.previousAccumulationAccess.SetVariableValue(filterCall.initExpression.Evaluate(this), this);

            int index = 0;
            foreach(IMatch match in matches)
            {
                if(filterCall.index != null)
                    filterCall.index.SetVariableValue(index, this);
                filterCall.element.SetVariableValue(match, this);
                object result = filterCall.lambdaExpression.Evaluate(this);
                match.SetMember(filterCall.Entity, result);
                ++index;
                filterCall.previousAccumulationAccess.SetVariableValue(result, this);
            }
        }

        public List<object[]> ApplyRewrite(IAction action, IGraph subgraph, object[] arguments, int which, 
            int localMaxMatches, bool special, bool test, List<FilterCall> filters, bool fireDebugEvents, out int numMatches)
        {
            if(subgraph != null)
                SwitchToSubgraph(subgraph);

            IMatches matches = Match(action, arguments, localMaxMatches, special, filters, fireDebugEvents);

            if(matches.Count == 0)
            {
                if(subgraph != null)
                    ReturnFromSubgraph();
                numMatches = 0;
                return emptyList;
            }

            if(test)
            {
                if(subgraph != null)
                    ReturnFromSubgraph();
                numMatches = matches.Count;
                return emptyList;
            }

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            PerformanceInfo.StartLocal();
#endif
            List<object[]> retElemsList = Replace(matches, which, special, fireDebugEvents);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopRewrite();
#endif
            if(fireDebugEvents)
                Finished(matches, special);

            if(subgraph != null)
                ReturnFromSubgraph();

            numMatches = matches.Count;
            return retElemsList;
        }

        public IMatches MatchForQuery(IAction action, IGraph subgraph, object[] arguments,
            int localMaxMatches, bool special, bool fireDebugEvents)
        {
            if(subgraph != null)
                SwitchToSubgraph(subgraph);

            IMatches matches = MatchForQuery(action, arguments, localMaxMatches, fireDebugEvents);

            //subrule debugging must be changed to allow this
            //if(matches.Count > 0) {// ensure that Matched is only called when a match exists
            //    procEnv.MatchedAfterFiltering(null, matches, null, special);
            //}

            if(subgraph != null)
                ReturnFromSubgraph();

            return matches;
        }


        #region Variables management

        public LinkedList<Variable> GetElementVariables(IGraphElement elem)
        {
            return graph.GlobalVariables.GetElementVariables(elem);
        }

        public object GetVariableValue(String varName)
        {
            return graph.GlobalVariables.GetVariableValue(varName);
        }

        public INode GetNodeVarValue(string varName)
        {
            return (INode)GetVariableValue(varName);
        }

        /// <summary>
        /// Retrieves the LGSPNode for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an LGSPNode object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according LGSPNode or null.</returns>
        public LGSPNode GetLGSPNodeVarValue(string varName)
        {
            return (LGSPNode)GetVariableValue(varName);
        }

        public IEdge GetEdgeVarValue(string varName)
        {
            return (IEdge)GetVariableValue(varName);
        }

        /// <summary>
        /// Retrieves the LGSPEdge for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an LGSPEdge object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according LGSPEdge or null.</returns>
        public LGSPEdge GetLGSPEdgeVarValue(string varName)
        {
            return (LGSPEdge)GetVariableValue(varName);
        }

        public void SetVariableValue(String varName, object val)
        {
            graph.GlobalVariables.SetVariableValue(varName, val);
        }

        public IEnumerable<Variable> Variables
        {
            get
            {
                foreach(Variable var in graph.GlobalVariables.Variables)
                {
                    yield return var;
                }
            }
        }

        public object this[string name]
        {
            get
            {
                return GetVariableValue(name);
            }

            set
            {
                SetVariableValue(name, value);
            }
        }

        #endregion Variables management




        #region Variables of graph elements convenience

        public void AddNode(INode node, String varName)
        {
            AddNode((LGSPNode)node, varName);
        }

        /// <summary>
        /// Adds an existing LGSPNode object to the graph and assigns it to the given variable.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddNode(LGSPNode node, String varName)
        {
            graph.AddNodeWithoutEvents(node, node.lgspType.TypeID);
            SetVariableValue(varName, node);
            graph.NodeAdded(node);
        }

        /// <summary>
        /// Adds a new node to the graph.
        /// TODO: Slow but provides a better interface...
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        protected INode AddINode(NodeType nodeType, String varName)
        {
            return AddNode(nodeType, varName);
        }

        public INode AddNode(NodeType nodeType, String varName)
        {
            return AddLGSPNode(nodeType, varName);
        }

        /// <summary>
        /// Adds a new LGSPNode to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        public LGSPNode AddLGSPNode(NodeType nodeType, String varName)
        {
            LGSPNode node = (LGSPNode)nodeType.CreateNode();
            graph.AddNodeWithoutEvents(node, nodeType.TypeID);
            SetVariableValue(varName, node);
            graph.NodeAdded(node);
            return node;
        }

        /// <summary>
        /// Adds an existing IEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddEdge(IEdge edge, String varName)
        {
            AddEdge((LGSPEdge)edge, varName);
        }

        /// <summary>
        /// Adds an existing LGSPEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddEdge(LGSPEdge edge, String varName)
        {
            graph.AddEdgeWithoutEvents(edge, edge.lgspType.TypeID);
            SetVariableValue(varName, edge);
            graph.EdgeAdded(edge);
        }

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName)
        {
            return AddEdge(edgeType, (LGSPNode)source, (LGSPNode)target, varName);
        }

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        public LGSPEdge AddEdge(EdgeType edgeType, LGSPNode source, LGSPNode target, String varName)
        {
            LGSPEdge edge = (LGSPEdge)edgeType.CreateEdge(source, target);
            graph.AddEdgeWithoutEvents(edge, edgeType.TypeID);
            SetVariableValue(varName, edge);
            graph.EdgeAdded(edge);
            return edge;
        }

        #endregion Variables of graph elements convenience


        #region Variables of named graph elements convenience

        public void AddNode(INode node, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            namedGraph.AddNode(node, elemName);
            SetVariableValue(varName, node);
        }

        public INode AddNode(NodeType nodeType, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            INode node = namedGraph.AddNode(nodeType, elemName);
            SetVariableValue(varName, node);
            return node;
        }

        public void AddEdge(IEdge edge, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            namedGraph.AddEdge(edge, elemName);
            SetVariableValue(varName, edge);
        }

        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName, String elemName)
        {
            LGSPNamedGraph namedGraph = (LGSPNamedGraph)graph;
            IEdge edge = namedGraph.AddEdge(edgeType, source, target, elemName);
            SetVariableValue(varName, edge);
            return edge;
        }

        #endregion Variables of named graph elements convenience


        #region Sequence handling

        public bool ApplyGraphRewriteSequence(Sequence sequence)
        {
            PerformanceInfo.Start();

            bool res = sequence.Apply(this);

            PerformanceInfo.Stop();
            return res;
        }

        public bool ApplyGraphRewriteSequence(String seqStr)
        {
            return ApplyGraphRewriteSequence(ParseSequence(seqStr));
        }

        public object EvaluateGraphRewriteSequenceExpression(SequenceExpression sequenceExpression)
        {
            PerformanceInfo.Start();

            object res = sequenceExpression.Evaluate(this);

            PerformanceInfo.Stop();
            return res;
        }

        public object EvaluateGraphRewriteSequenceExpression(String seqExprStr)
        {
            return EvaluateGraphRewriteSequenceExpression(ParseSequenceExpression(seqExprStr));
        }

        public bool ValidateWithSequence(Sequence seq)
        {
            SwitchToSubgraph(graph.Clone("clonedGraph"));
            bool valid = seq.Apply(this);
            ReturnFromSubgraph();
            return valid;
        }

        public bool ValidateWithSequence(String seqStr)
        {
            return ValidateWithSequence(ParseSequence(seqStr));
        }

        public Sequence ParseSequence(String seqStr)
        {
            SequenceParserEnvironmentInterpreted parserEnv = new SequenceParserEnvironmentInterpreted(curActions);
            List<string> warnings = new List<string>();
            Sequence seq = SequenceParser.ParseSequence(seqStr, parserEnv, warnings);
            foreach(string warning in warnings)
            {
                ConsoleUI.errorOutWriter.WriteLine(warning);
            }
            return seq;
        }

        public SequenceExpression ParseSequenceExpression(String seqExprStr)
        {
            Dictionary<String, String> predefinedVariables = new Dictionary<string, string>();
            SequenceParserEnvironmentInterpreted parserEnv = new SequenceParserEnvironmentInterpreted(curActions);
            List<string> warnings = new List<string>();
            SequenceExpression seqExpr = SequenceParser.ParseSequenceExpression(seqExprStr, predefinedVariables, parserEnv, warnings);
            foreach(string warning in warnings)
            {
                ConsoleUI.errorOutWriter.WriteLine(warning);
            }
            return seqExpr;
        }

        class LGSPParallelExecutionBegin
        {
            public SequenceExecuteInSubgraph parallelExecutionBegin;
            public LGSPGraphProcessingEnvironment procEnv;
            public int poolThreadId;
            public volatile bool result;
        }

        public List<bool> ParallelApplyGraphRewriteSequences(SequenceParallelExecute parallel)
        {
            return ParallelApplyGraphRewriteSequences(parallel, parallel.InSubgraphExecutions);
        }

        public List<bool> ParallelApplyGraphRewriteSequences(SequenceParallelArrayExecute parallel)
        {
            return ParallelApplyGraphRewriteSequences(parallel, parallel.InSubgraphExecutions);
        }

        private List<bool> ParallelApplyGraphRewriteSequences(SequenceParallel parallel, List<SequenceExecuteInSubgraph> inSubgraphExecutions)
        {
            List<LGSPParallelExecutionBegin> extendedParallelExecutionBegins = new List<LGSPParallelExecutionBegin>();
            List<ParallelExecutionBegin> begunParallelExecutions = new List<ParallelExecutionBegin>();
            foreach(SequenceExecuteInSubgraph inSubgraphExecution in inSubgraphExecutions)
            {
                LGSPGraphProcessingEnvironment procEnv = new LGSPGraphProcessingEnvironment((LGSPGraph)inSubgraphExecution.Subgraph, (LGSPActions)Actions);
                LGSPParallelExecutionBegin extendedParallelExecutionBegin = new LGSPParallelExecutionBegin();
                extendedParallelExecutionBegin.parallelExecutionBegin = inSubgraphExecution;
                extendedParallelExecutionBegin.procEnv = procEnv;
                extendedParallelExecutionBegins.Add(extendedParallelExecutionBegin);
                ParallelExecutionBegin begunParallelExecution = new ParallelExecutionBegin();
                begunParallelExecution.procEnv = procEnv;
                begunParallelExecution.sequence = inSubgraphExecution;
                begunParallelExecution.value = inSubgraphExecution.ValueVariable != null ? inSubgraphExecution.ValueVariable.GetVariableValue(this) : null;
                begunParallelExecutions.Add(begunParallelExecution);
            }

            if(!ThreadPool.PoolSizeWasSet)
                ThreadPool.SetPoolSize(graph.Model.ThreadPoolSizeForSequencesParallelExecution);

            SpawnSequences(parallel, begunParallelExecutions.ToArray());
            for(int i = 0; i < extendedParallelExecutionBegins.Count; ++i)
            {
                LGSPParallelExecutionBegin extendedParallelExecutionBegin = extendedParallelExecutionBegins[i];

                extendedParallelExecutionBegin.poolThreadId = -1;
                if(i < extendedParallelExecutionBegins.Count - 1)
                {
                    extendedParallelExecutionBegin.poolThreadId = ThreadPool.FetchWorkerAndExecuteWork(new ParameterizedThreadStart(ParallelApplyGraphRewriteSequence), extendedParallelExecutionBegin);
                    if(extendedParallelExecutionBegin.poolThreadId != -1)
                        continue;
                }

                ParallelApplyGraphRewriteSequence(extendedParallelExecutionBegin);
            }

            List<bool> results = new List<bool>();
            foreach(LGSPParallelExecutionBegin extendedParallelExecutionBegin in extendedParallelExecutionBegins)
            {
                if(extendedParallelExecutionBegin.poolThreadId != -1)
                    ThreadPool.WaitForWorkDone(extendedParallelExecutionBegin.poolThreadId);
                results.Add(extendedParallelExecutionBegin.result);
            }
            JoinSequences(parallel, begunParallelExecutions.ToArray());

            return results;
        }

        void ParallelApplyGraphRewriteSequence(object value)
        {
            LGSPParallelExecutionBegin extendedParallelExecutionBegin = (LGSPParallelExecutionBegin)value;
            LGSPGraphProcessingEnvironment procEnv = extendedParallelExecutionBegin.procEnv;
            SequenceExecuteInSubgraph inSubgraphExecution = extendedParallelExecutionBegin.parallelExecutionBegin;
            bool result = inSubgraphExecution.Apply(procEnv);
            extendedParallelExecutionBegin.result = result;
        }


        public IUserProxyForSequenceExecution UserProxy
        {
            get { return userProxy ?? compliantUserProxy; }
            set { userProxy = value; }
        }

        public IUserProxyForSequenceExecution CompliantUserProxy
        {
            get { return compliantUserProxy; }
        }

        #endregion Sequence handling
        

        #region Events
       
        public event EnterSequenceHandler OnEntereringSequence;
        public event ExitSequenceHandler OnExitingSequence;

        public event EndOfIterationHandler OnEndOfIteration;

        public event SpawnSequencesHandler OnSpawnSequences;
        public event JoinSequencesHandler OnJoinSequences;

        public void EnteringSequence(SequenceBase seq)
        {
            if(OnEntereringSequence != null)
                OnEntereringSequence(seq);
        }

        public void ExitingSequence(SequenceBase seq)
        {
            if(OnExitingSequence != null)
                OnExitingSequence(seq);
        }

        public void EndOfIteration(bool continueLoop, SequenceBase seq)
        {
            if(OnEndOfIteration != null)
                OnEndOfIteration(continueLoop, seq);
        }

        public void SpawnSequences(SequenceParallel parallel, params ParallelExecutionBegin[] parallelExecutionBegins)
        {
            if(OnSpawnSequences != null)
                OnSpawnSequences(parallel, parallelExecutionBegins);
        }

        public void JoinSequences(SequenceParallel parallel, params ParallelExecutionBegin[] parallelExecutionBegins)
        {
            if(OnJoinSequences != null)
                OnJoinSequences(parallel, parallelExecutionBegins);
        }

        #endregion Events
    }
}
