/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    class DebuggerTask
    {
        public Debugger debugger;
        public IGraphProcessingEnvironment procEnv;
        public bool isActive;
        public bool isParentOfActive;

        public readonly Stack<SequenceBase> debugSequences = new Stack<SequenceBase>();

        public SequenceBase curStepSequence = null;

        public SequenceBase lastlyEntered = null;
        public SequenceBase recentlyMatched = null;

        public readonly LinkedList<Sequence> loopList = new LinkedList<Sequence>();

        public readonly List<SubruleComputation> computationsEnteredStack = new List<SubruleComputation>(); // can't use stack class, too weak

        public readonly List<IPatternMatchingConstruct> patternMatchingConstructsExecuted = new List<IPatternMatchingConstruct>();
        public readonly List<bool> skipMode = new List<bool>();

        public DebuggerTask(Debugger debugger, IGraphProcessingEnvironment procEnv)
        {
            this.debugger = debugger;
            this.procEnv = procEnv;
        }

        /// <summary>
        /// Closes the debugger task.
        /// </summary>
        public void Close()
        {
            UnregisterActionEvents(procEnv);
            UnregisterGraphEvents(procEnv.NamedGraph);
        }


        #region Event Handling

        private void DebugNodeAdded(INode node)
        {
            if(isActive)
                debugger.DebugNodeAdded(node);
        }

        private void DebugEdgeAdded(IEdge edge)
        {
            if(isActive)
                debugger.DebugEdgeAdded(edge);
        }

        private void DebugDeletingNode(INode node)
        {
            if(isActive)
                debugger.DebugDeletingNode(node);
        }

        private void DebugDeletingEdge(IEdge edge)
        {
            if(isActive)
                debugger.DebugDeletingEdge(edge);
        }

        private void DebugClearingGraph(IGraph graph)
        {
            if(isActive)
                debugger.DebugClearingGraph(graph);
        }

        private void DebugChangedNodeAttribute(INode node, AttributeType attrType)
        {
            if(isActive)
                debugger.DebugChangedNodeAttribute(node, attrType);
        }

        private void DebugChangedEdgeAttribute(IEdge edge, AttributeType attrType)
        {
            if(isActive)
                debugger.DebugChangedEdgeAttribute(edge, attrType);
        }

        private void DebugRetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
            if(isActive)
                debugger.DebugRetypingElement(oldElem, newElem);
        }

        private void DebugRedirectingEdge(IEdge edge)
        {
            if(isActive)
                debugger.DebugRedirectingEdge(edge);
        }

        private void DebugSettingAddedNodeNames(string[] namesOfNodesAdded)
        {
            if(isActive)
                debugger.DebugSettingAddedNodeNames(namesOfNodesAdded);
        }

        private void DebugSettingAddedEdgeNames(string[] namesOfEdgesAdded)
        {
            if(isActive)
                debugger.DebugSettingAddedEdgeNames(namesOfEdgesAdded);
        }

        private void DebugBeginExecution(IPatternMatchingConstruct patternMatchingConstruct)
        {
            if(isActive)
                debugger.DebugBeginExecution(patternMatchingConstruct);
        }

        private void DebugMatchedBefore(IList<IMatches> matchesList)
        {
            if(isActive)
                debugger.DebugMatchedBefore(matchesList);
        }

        private void DebugMatchedAfter(IMatches[] matches, bool[] special)
        {
            if(isActive)
                debugger.DebugMatchedAfter(matches, special);
        }

        private void DebugMatchSelected(IMatch match, bool special, IMatches matches)
        {
            if(isActive)
                debugger.DebugMatchSelected(match, special, matches);
        }

        private void DebugRewritingSelectedMatch()
        {
            if(isActive)
                debugger.DebugRewritingSelectedMatch();
        }

        private void DebugSelectedMatchRewritten()
        {
            if(isActive)
                debugger.DebugSelectedMatchRewritten();
        }

        private void DebugFinishedSelectedMatch()
        {
            if(isActive)
                debugger.DebugFinishedSelectedMatch();
        }

        private void DebugFinished(IMatches[] matches, bool[] special)
        {
            if(isActive)
                debugger.DebugFinished(matches, special);
        }

        private void DebugEndExecution(IPatternMatchingConstruct patternMatchingConstruct, object result)
        {
            if(isActive)
                debugger.DebugEndExecution(patternMatchingConstruct, result);
        }

        private void DebugEnteringSequence(SequenceBase seq)
        {
            if(isActive)
                debugger.DebugEnteringSequence(seq);
        }

        private void DebugExitingSequence(SequenceBase seq)
        {
            if(isActive)
                debugger.DebugExitingSequence(seq);
        }

        private void DebugEndOfIteration(bool continueLoop, SequenceBase seq)
        {
            if(isActive)
                debugger.DebugEndOfIteration(continueLoop, seq);
        }

        private void DebugSpawnSequences(SequenceParallel parallel, params ParallelExecutionBegin[] parallelExecutionBegins)
        {
            if(isActive)
                debugger.DebugSpawnSequences(parallel, parallelExecutionBegins);
        }

        private void DebugJoinSequences(SequenceParallel parallel, params ParallelExecutionBegin[] parallelExecutionBegins)
        {
            if(isParentOfActive)
                debugger.DebugJoinSequences(parallel, parallelExecutionBegins);
        }

        private void DebugSwitchToGraph(IGraph newGraph)
        {
            if(isActive)
                debugger.DebugSwitchToGraph(newGraph);
        }

        private void DebugReturnedFromGraph(IGraph oldGraph)
        {
            if(isActive)
                debugger.DebugReturnedFromGraph(oldGraph);
        }

        private void DebugEnter(string message, params object[] values)
        {
            if(isActive)
                debugger.DebugEnter(message, values);
        }

        private void DebugExit(string message, params object[] values)
        {
            if(isActive)
                debugger.DebugExit(message, values);
        }

        private void DebugEmit(string message, params object[] values)
        {
            if(isActive)
                debugger.DebugEmit(message, values);
        }

        private void DebugHalt(string message, params object[] values)
        {
            if(isActive)
                debugger.DebugHalt(message, values);
        }

        private void DebugHighlight(string message, List<object> values, List<string> sourceNames)
        {
            if(isActive)
                debugger.DebugHighlight(message, values, sourceNames);
        }

        /// <summary>
        /// Registers event handlers for needed LibGr-graph events
        /// </summary>
        public void RegisterGraphEvents(INamedGraph graph)
        {
            graph.OnNodeAdded += DebugNodeAdded;
            graph.OnEdgeAdded += DebugEdgeAdded;
            graph.OnRemovingNode += DebugDeletingNode;
            graph.OnRemovingEdge += DebugDeletingEdge;
            graph.OnClearingGraph += DebugClearingGraph;
            graph.OnChangedNodeAttribute += DebugChangedNodeAttribute;
            graph.OnChangedEdgeAttribute += DebugChangedEdgeAttribute;
            graph.OnRetypingNode += DebugRetypingElement;
            graph.OnRetypingEdge += DebugRetypingElement;
            graph.OnRedirectingEdge += DebugRedirectingEdge;
            graph.OnSettingAddedNodeNames += DebugSettingAddedNodeNames;
            graph.OnSettingAddedEdgeNames += DebugSettingAddedEdgeNames;
        }

        /// <summary>
        /// Registers event handlers for needed LibGr-action events
        /// </summary>
        public void RegisterActionEvents(IGraphProcessingEnvironment procEnv)
        {
            procEnv.OnMatchedBefore += DebugMatchedBefore;
            procEnv.OnMatchedAfter += DebugMatchedAfter;
            procEnv.OnMatchSelected += DebugMatchSelected;
            procEnv.OnRewritingSelectedMatch += DebugRewritingSelectedMatch;
            procEnv.OnSelectedMatchRewritten += DebugSelectedMatchRewritten;
            procEnv.OnFinishedSelectedMatch += DebugFinishedSelectedMatch;
            procEnv.OnFinished += DebugFinished;

            procEnv.OnBeginExecution += DebugBeginExecution;
            procEnv.OnEndExecution += DebugEndExecution;

            procEnv.OnSwitchingToSubgraph += DebugSwitchToGraph;
            procEnv.OnReturnedFromSubgraph += DebugReturnedFromGraph;

            procEnv.OnDebugEnter += DebugEnter;
            procEnv.OnDebugExit += DebugExit;
            procEnv.OnDebugEmit += DebugEmit;
            procEnv.OnDebugHalt += DebugHalt;
            procEnv.OnDebugHighlight += DebugHighlight;

            procEnv.OnEntereringSequence += DebugEnteringSequence;
            procEnv.OnExitingSequence += DebugExitingSequence;
            procEnv.OnEndOfIteration += DebugEndOfIteration;
            procEnv.OnSpawnSequences += DebugSpawnSequences;
            procEnv.OnJoinSequences += DebugJoinSequences;
        }

        /// <summary>
        /// Unregisters the events previously registered with RegisterGraphEvents()
        /// </summary>
        public void UnregisterGraphEvents(INamedGraph graph)
        {
            graph.OnNodeAdded -= DebugNodeAdded;
            graph.OnEdgeAdded -= DebugEdgeAdded;
            graph.OnRemovingNode -= DebugDeletingNode;
            graph.OnRemovingEdge -= DebugDeletingEdge;
            graph.OnClearingGraph -= DebugClearingGraph;
            graph.OnChangedNodeAttribute -= DebugChangedNodeAttribute;
            graph.OnChangedEdgeAttribute -= DebugChangedEdgeAttribute;
            graph.OnRetypingNode -= DebugRetypingElement;
            graph.OnRetypingEdge -= DebugRetypingElement;
            graph.OnRedirectingEdge -= DebugRedirectingEdge;
            graph.OnSettingAddedNodeNames -= DebugSettingAddedNodeNames;
            graph.OnSettingAddedEdgeNames -= DebugSettingAddedEdgeNames;
        }

        /// <summary>
        /// Unregisters the events previously registered with RegisterActionEvents()
        /// </summary>
        public void UnregisterActionEvents(IGraphProcessingEnvironment procEnv)
        {
            procEnv.OnMatchedBefore -= DebugMatchedBefore;
            procEnv.OnMatchedAfter -= DebugMatchedAfter;
            procEnv.OnMatchSelected -= DebugMatchSelected;
            procEnv.OnRewritingSelectedMatch -= DebugRewritingSelectedMatch;
            procEnv.OnSelectedMatchRewritten -= DebugSelectedMatchRewritten;
            procEnv.OnFinishedSelectedMatch -= DebugFinishedSelectedMatch;
            procEnv.OnFinished -= DebugFinished;

            procEnv.OnBeginExecution -= DebugBeginExecution;
            procEnv.OnEndExecution -= DebugEndExecution;

            procEnv.OnSwitchingToSubgraph -= DebugSwitchToGraph;
            procEnv.OnReturnedFromSubgraph -= DebugReturnedFromGraph;

            procEnv.OnDebugEnter -= DebugEnter;
            procEnv.OnDebugExit -= DebugExit;
            procEnv.OnDebugEmit -= DebugEmit;
            procEnv.OnDebugHalt -= DebugHalt;
            procEnv.OnDebugHighlight -= DebugHighlight;

            procEnv.OnEntereringSequence -= DebugEnteringSequence;
            procEnv.OnExitingSequence -= DebugExitingSequence;
            procEnv.OnEndOfIteration -= DebugEndOfIteration;
            procEnv.OnSpawnSequences -= DebugSpawnSequences;
            procEnv.OnJoinSequences -= DebugJoinSequences;
        }

        #endregion Event Handling
    }
}
