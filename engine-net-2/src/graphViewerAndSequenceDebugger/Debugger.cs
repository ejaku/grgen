/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

using de.unika.ipd.grGen.libGr;
using System.Text;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class Debugger : IUserProxyForSequenceExecution
    {
        public readonly IDebuggerEnvironment env;
        DebuggerGraphProcessingEnvironment debuggerProcEnv;
        Stack<DebuggerTask> tasks = new Stack<DebuggerTask>();
        DebuggerTask task
        {
            get { return tasks.Peek(); }
        }

        static Dictionary<Sequence, DebuggerTask> sequencesToDebuggerTask = new Dictionary<Sequence, DebuggerTask>();

        public readonly String debugLayout;
        readonly ElementRealizers realizers;
        readonly GraphAnnotationAndChangesRecorder renderRecorder;
        GraphViewerClient graphViewerClient = null;

        bool stepMode = true;
        bool dynamicStepMode = false;
        bool dynamicStepModeSkip = false;
        bool detailedMode = false;
        bool outOfDetailedMode = false;
        int outOfDetailedModeTarget = -1;
        bool detailedModeShowPreMatches;
        bool detailedModeShowPostMatches;
        bool recordMode = false;
        bool topLevelRuleChanged = true;
        bool alwaysShow = true;

        IDisplayer displayer = null;
        DisplaySequenceContext context = null;

        int matchDepth = 0;

        bool lazyChoice = true;

        public GraphViewerClient GraphViewerClient
        {
            get { return graphViewerClient; }
        }

        public bool ConnectionLost
        {
            get { return graphViewerClient.ConnectionLost; }
        }

        private bool notifyOnConnectionLost;
        public bool NotifyOnConnectionLost
        {
            set
            {
                if(!value)
                {
                    if(notifyOnConnectionLost)
                    {
                        notifyOnConnectionLost = false;
                        graphViewerClient.OnConnectionLost -= new ConnectionLostHandler(DebugOnConnectionLost);
                    }
                }
                else
                {
                    if(!notifyOnConnectionLost)
                    {
                        notifyOnConnectionLost = true;
                        graphViewerClient.OnConnectionLost += new ConnectionLostHandler(DebugOnConnectionLost);
                    }
                }
            }
        }

        UserChoiceMenu queryUserMenu;
        UserChoiceMenu queryContinueOrTraceMenu;

        UserChoiceMenu debuggerMainSequenceEnteringMenu = new UserChoiceMenu(UserChoiceMenuNames.DebuggerMainSequenceEnteringMenu, new string[] {
                    "commandNextMatch", "commandDetailedStep", "commandStep", "commandStepUp", "commandStepOut", "commandRun",
                    "commandToggleBreakpoints", "commandToggleChoicepoints", "commandToggleLazyChoice", "commandWatchpoints",
                    "commandShowVariables", "commandShowClassObject", "commandPrintStacktrace", "commandFullState",
                    "commandHighlight", "commandDumpGraph", "commandAsGraph", "commandAbort" });

        UserChoiceMenu continueOnAssertionMenu = new UserChoiceMenu(UserChoiceMenuNames.ContinueOnAssertionMenu, new string[] {
                    "commandAbort", "commandDebugAtSourceCodeLevel", "commandContinue" });

        UserChoiceMenu skipAsRequiredInMatchByMatchProcessingMenu = new UserChoiceMenu(UserChoiceMenuNames.SkipAsRequiredInMatchByMatchProcessingMenu, new string[] {
                    "commandContinueApplyRewrite", "commandSkipSingleMatches" });

        UserChoiceMenu skipAsRequiredMenu = new UserChoiceMenu(UserChoiceMenuNames.SkipAsRequiredMenu, new string[] {
                    "commandContinueShowSingleMatchesAndApplyRewrite", "commandSkipSingleMatches" });

        UserChoiceMenu queryContinueWhenShowPostDisabledMenu = new UserChoiceMenu(UserChoiceMenuNames.QueryContinueWhenShowPostDisabledMenu, new string[] {
                    "commandContinueAnyKey", "commandFullState", "commandAbort" });

        UserChoiceMenu switchRefreshViewAdditionalGuiMenu = new UserChoiceMenu(UserChoiceMenuNames.SwitchRefreshViewMenu, new string[] {
                    "viewSwitch", "viewRefresh" });

        /// <summary>
        /// Initializes a new Debugger instance using the given environments, and layout as well as layout options.
        /// All invalid options will be removed from layoutOptions.
        /// </summary>
        /// <param name="env">The environment to be used by the debugger
        /// (regular implementation by the GrShellSequenceApplierAndDebugger, minimal implementation for API level usage by the DebuggerEnvironment).</param>
        /// <param name="debuggerProcEnv">The debugger graph processing environment to be used by the debugger
        /// (contains the graph processing environment of the top-level graph to be used by the debugger).</param>
        /// <param name="realizers">The element realizers to be used by the debugger.</param>
        /// <param name="graphViewerType">The type of the graph viewer to be used by the debugger.</param>
        /// <param name="debugLayout">The name of the layout to be used.
        /// If null, Orthogonal is used.</param>
        /// <param name="layoutOptions">An dictionary mapping layout option names to their values.
        /// It may be null, if no options are to be applied.</param>
        /// <param name="basicGraphViewerClientHost">An optional basic graph viewer client host, in case the graph viewer type MSAGL is requested, the MSAGL graph viewer form is added to that form (YComp is a standalone application).</param>
        public Debugger(IDebuggerEnvironment env, DebuggerGraphProcessingEnvironment debuggerProcEnv,
            ElementRealizers realizers, GraphViewerTypes graphViewerType, String debugLayout, Dictionary<String, String> layoutOptions,
            IBasicGraphViewerClientHost basicGraphViewerClientHost)
        {
            IGraphProcessingEnvironment procEnv = debuggerProcEnv.ProcEnv;

            this.tasks.Push(new DebuggerTask(this, procEnv));
            this.env = env;
            this.debuggerProcEnv = debuggerProcEnv;

            this.realizers = realizers;

            this.displayer = new Printer(env);
            this.context = new DisplaySequenceContext();

            this.renderRecorder = new GraphAnnotationAndChangesRecorder();

            this.debugLayout = debugLayout;

            graphViewerClient = new GraphViewerClient(procEnv.NamedGraph, graphViewerType, debugLayout ?? "Orthogonal",
                debuggerProcEnv.DumpInfo, realizers, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, basicGraphViewerClientHost);

            procEnv.NamedGraph.ReuseOptimization = false;
            NotifyOnConnectionLost = true;

            try
            {
                if(layoutOptions != null)
                {
                    List<String> illegalOptions = null;
                    foreach(KeyValuePair<String, String> option in layoutOptions)
                    {
                        if(!SetLayoutOption(option.Key, option.Value))
                        {
                            if(illegalOptions == null)
                                illegalOptions = new List<String>();
                            illegalOptions.Add(option.Key);
                        }
                    }
                    if(illegalOptions != null)
                    {
                        foreach(String illegalOption in illegalOptions)
                        {
                            layoutOptions.Remove(illegalOption);
                        }
                    }
                }

                if(!graphViewerClient.dumpInfo.IsExcludedGraph())
                    graphViewerClient.UploadGraph();
            }
            catch(OperationCanceledException)
            {
                throw new Exception("Connection to yComp lost");
            }

            NotifyOnConnectionLost = false;

            this.task.RegisterGraphEvents(procEnv.NamedGraph);
            this.task.RegisterActionEvents(procEnv);
            this.task.isActive = true;
        }

        /// <summary>
        /// Closes the debugger.
        /// </summary>
        public void Close()
        {
            if(graphViewerClient == null)
                throw new InvalidOperationException("The debugger has already been closed!");

            task.UnregisterActionEvents(task.procEnv);
            task.UnregisterGraphEvents(task.procEnv.NamedGraph);

            task.procEnv.NamedGraph.ReuseOptimization = true;
            graphViewerClient.Close();
            graphViewerClient = null;
        }

        public void InitNewRewriteSequence(Sequence seq, bool withStepMode)
        {
            task.debugSequences.Clear();
            task.debugSequences.Push(seq);
            task.curStepSequence = null;
            stepMode = withStepMode;
            recordMode = false;
            alwaysShow = false;
            detailedMode = false;
            outOfDetailedMode = false;
            outOfDetailedModeTarget = -1;
            dynamicStepMode = false;
            dynamicStepModeSkip = false;
            task.lastlyEntered = null;
            task.recentlyMatched = null;
            context = new DisplaySequenceContext();

            sequencesToDebuggerTask.Clear();
            sequencesToDebuggerTask.Add(seq, task);
        }

        public void InitSequenceExpression(SequenceExpression seqExp, bool withStepMode)
        {
            task.debugSequences.Clear();
            task.debugSequences.Push(seqExp);
            task.curStepSequence = null;
            stepMode = withStepMode;
            recordMode = false;
            alwaysShow = false;
            detailedMode = false;
            outOfDetailedMode = false;
            outOfDetailedModeTarget = -1;
            dynamicStepMode = false;
            dynamicStepModeSkip = false;
            task.lastlyEntered = null;
            task.recentlyMatched = null;
            context = new DisplaySequenceContext();
        }

        public void AbortRewriteSequence()
        {
            stepMode = false;
            detailedMode = false;
            task.loopList.Clear();
        }

        public void FinishRewriteSequence()
        {
            alwaysShow = true;
            try
            {
                graphViewerClient.UpdateDisplay();
                graphViewerClient.Sync();
            }
            catch(OperationCanceledException)
            {
            }
        }

        public DebuggerGraphProcessingEnvironment DebuggerProcEnv
        {
            get { return debuggerProcEnv; }
            set
            {
                // switch to new graph in YComp
                task.UnregisterActionEvents(debuggerProcEnv.ProcEnv);
                task.UnregisterGraphEvents(debuggerProcEnv.ProcEnv.NamedGraph);
                graphViewerClient.ClearGraph();
                debuggerProcEnv = value;
                graphViewerClient.Graph = debuggerProcEnv.ProcEnv.NamedGraph;
                if(!graphViewerClient.dumpInfo.IsExcludedGraph())
                    graphViewerClient.UploadGraph();
                task.RegisterGraphEvents(debuggerProcEnv.ProcEnv.NamedGraph);
                task.RegisterActionEvents(debuggerProcEnv.ProcEnv);

                // TODO: reset any state when inside a rule debugging session
            }
        }

        public void ForceLayout()
        {
            graphViewerClient.ForceLayout();
        }

        public void UpdateGraphViewerDisplay()
        {
            graphViewerClient.UpdateDisplay();
        }

        public void SetLayout(String layout)
        {
            graphViewerClient.SetLayout(layout);
        }

        public void GetLayoutOptions()
        {
            String str = graphViewerClient.GetLayoutOptions();
            env.WriteLine("Available layout options and their current values:\n\n" + str);
        }

        /// <summary>
        /// Sets a layout option for the current layout in yComp.
        /// </summary>
        /// <param name="optionName">The name of the option.</param>
        /// <param name="optionValue">The new value for the option.</param>
        /// <returns>True, iff yComp did not report an error.</returns>
        public bool SetLayoutOption(String optionName, String optionValue)
        {
            String errorMessage = graphViewerClient.SetLayoutOption(optionName, optionValue);
            if(errorMessage != null)
                env.WriteLine(errorMessage);
            return errorMessage == null;
        }

        public bool DetailedModeShowPreMatches
        {
            get { return detailedModeShowPreMatches; }
            set { detailedModeShowPreMatches = value; }
        }

        public bool DetailedModeShowPostMatches
        {
            get { return detailedModeShowPostMatches; }
            set { detailedModeShowPostMatches = value; }
        }

        /// <summary>
        /// Searches in the given sequence base seq for the parent sequence base of the sequence base childseq.
        /// </summary>
        /// <returns>The parent sequence base of childseq or null, if no parent has been found.</returns>
        private SequenceBase GetParentSequence(SequenceBase childseq, SequenceBase seq)
        {
            SequenceBase res = null;
            foreach(SequenceBase child in seq.ChildrenBase)
            {
                if(child == childseq)
                    return seq;
                res = GetParentSequence(childseq, child);
                if(res != null)
                    return res;
            }
            return res;
        }

        /// <summary>
        /// Debugger method waiting for user commands
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        private bool QueryUser(SequenceBase seq)
        {
            while(true)
            {
                ConsoleKeyInfo key = env.LetUserChoose(queryUserMenu, switchRefreshViewAdditionalGuiMenu);
                switch(key.KeyChar)
                {
                case 's':
                    stepMode = true;
                    dynamicStepMode = false;
                    detailedMode = false;
                    return false;
                case 'd':
                    stepMode = true;
                    dynamicStepMode = false;
                    detailedMode = true;
                    outOfDetailedMode = false;
                    outOfDetailedModeTarget = -1;
                    return true;
                case 'u':
                    stepMode = false;
                    dynamicStepMode = false;
                    detailedMode = false;
                    task.curStepSequence = GetParentSequence(seq, task.debugSequences.Peek());
                    return false;
                case 'o':
                    stepMode = false;
                    dynamicStepMode = false;
                    detailedMode = false;
                    if(task.loopList.Count == 0)
                        task.curStepSequence = null;                 // execute until the end
                    else
                        task.curStepSequence = task.loopList.First.Value; // execute until current loop has been exited
                    return false;
                case 'r':
                    stepMode = false;
                    dynamicStepMode = false;
                    detailedMode = false;
                    task.curStepSequence = null;                     // execute until the end
                    return false;
                case 'b':
                    {
                        BreakpointAndChoicepointEditor breakpointEditor = new BreakpointAndChoicepointEditor(env, displayer, task.debugSequences);
                        breakpointEditor.HandleToggleBreakpoints();
                        context.highlightSeq = seq;
                        context.success = false;
                        env.Clear();
                        displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                        break;
                    }
                case 'w':
                    {
                        WatchpointEditor watchpointEditor = new WatchpointEditor(debuggerProcEnv, env);
                        watchpointEditor.HandleWatchpoints();
                        context.highlightSeq = seq;
                        context.success = false;
                        env.Clear();
                        displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                        break;
                    }
                case 'c':
                    {
                        BreakpointAndChoicepointEditor choicepointEditor = new BreakpointAndChoicepointEditor(env, displayer, task.debugSequences);
                        choicepointEditor.HandleToggleChoicepoints();
                        context.highlightSeq = seq;
                        context.success = false;
                        env.Clear();
                        displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                        break;
                    }
                case 'l':
                    HandleToggleLazyChoice();
                    break;
                case 'a':
                    env.Cancel();
                    return false;                               // never reached
                case 'n':
                    stepMode = false;
                    dynamicStepMode = true;
                    detailedMode = false;
                    return false;
                case 'v':
                    HandleShowVariable(seq);
                    env.Clear();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    break;
                case 'j':
                    HandleShowClassObject(seq);
                    env.Clear();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    break;
                case 'p':
                    HandleDump();
                    break;
                case 'g':
                    HandleAsGraph(seq);
                    break;
                case 'h':
                    HandleUserHighlight(seq);
                    break;
                case 't':
                    HandleStackTrace();
                    env.Clear();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    break;
                case 'f':
                    HandleFullState();
                    env.Clear();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    break;
                case ' ':
                    if(key.Key == ConsoleKey.F5)
                        HandleRefreshView();
                    else if(key.Key == ConsoleKey.F8)
                        HandleSwitchView();
                    else
                        throw new Exception("Internal error");
                    break;
                default:
                    throw new Exception("Internal error");
                }
            }
        }


        #region Methods for directly handling user commands

        private void HandleToggleLazyChoice()
        {
            if(lazyChoice)
            {
                env.WriteLine("Lazy choice disabled, always requesting user choice on $%[r] / $%v[r] / $%{...}.");
                lazyChoice = false;
            }
            else
            {
                env.WriteLine("Lazy choice enabled, only prompting user on $%[r] / $%v[r] / $%{...} if more matches available than rewrites requested.");
                lazyChoice = true;
            }
        }

        private void HandleShowVariable(SequenceBase seq)
        {
            env.Clear();
            PrintVariables(null, null);
            PrintVariables(task.debugSequences.Peek(), seq);
            PrintVisited();
            if(env.TwoPane)
                env.PauseUntilAnyKeyPressed("Press any key to return from variable display...");
        }

        private void HandleShowClassObject(SequenceBase seq)
        {
            do
            {
                env.Write("Enter id of class object to emit (with % prefix), of transient class object to emit (with & prefix), or name of variable to emit (or just enter for abort): ");
                String argument = env.ReadLine();
                if(argument.Length == 0)
                    return;

                if(argument.StartsWith("%"))
                    HandleShowClassObjectObject(argument);
                else if(argument.StartsWith("&"))
                    HandleShowClassObjectTransientObject(argument);
                else
                    HandleShowClassObjectVariable(seq, argument);
            }
            while(true);
        }

        private void HandleShowClassObjectObject(string argument)
        {
            long uniqueId;
            if(HexToLong(argument.Substring(1), out uniqueId))
            {
                String objectName = String.Format("%{0,00000000:X}", uniqueId);
                IObject obj = debuggerProcEnv.objectNamerAndIndexer.GetObject(objectName);
                if(obj != null)
                    env.WriteLineDataRendering(EmitHelper.ToStringAutomatic(obj, task.procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, task.procEnv));
                else
                    env.WriteLine("Unknown class object id " + objectName + "!");
            }
            else
                env.WriteLine("Invalid class object id " + argument + "!");
        }

        private void HandleShowClassObjectTransientObject(string argument)
        {
            long uniqueId;
            if(HexToLong(argument.Substring(1), out uniqueId))
            {
                if(debuggerProcEnv.transientObjectNamerAndIndexer.GetTransientObject(uniqueId) != null)
                {
                    ITransientObject obj = debuggerProcEnv.transientObjectNamerAndIndexer.GetTransientObject(uniqueId);
                    env.WriteLineDataRendering(EmitHelper.ToStringAutomatic(obj, task.procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, task.procEnv));
                }
                else
                    env.WriteLine("Unknown transient class object id " + argument + "!");
            }
            else
                env.WriteLine("Invalid transient class object id " + argument + "!");
        }

        private void HandleShowClassObjectVariable(SequenceBase seq, string argument)
        {
            if(GetSequenceVariable(argument, task.debugSequences.Peek(), seq) != null
                && GetSequenceVariable(argument, task.debugSequences.Peek(), seq).GetVariableValue(debuggerProcEnv.ProcEnv) != null)
            {
                object value = GetSequenceVariable(argument, task.debugSequences.Peek(), seq).GetVariableValue(debuggerProcEnv.ProcEnv);
                env.WriteLineDataRendering(EmitHelper.ToStringAutomatic(value, task.procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, task.procEnv));
            }
            else if(debuggerProcEnv.ProcEnv.GetVariableValue(argument) != null)
            {
                object value = debuggerProcEnv.ProcEnv.GetVariableValue(argument);
                env.WriteLineDataRendering(EmitHelper.ToStringAutomatic(value, task.procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, task.procEnv));
            }
            else
                env.WriteLine("The given " + argument + " is not a known variable name (of non-null value)!");
        }

        private bool HexToLong(String argument, out long result)
        {
            try
            {
                result = Convert.ToInt64(argument, 16);
                return true;
            }
            catch(Exception)
            {
                result = -1;
                return false;
            }
        }

        private SequenceVariable GetSequenceVariable(String name, SequenceBase seqStart, SequenceBase seq)
        {
            Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqStart.GetLocalVariables(seqVars, constructors, seq);
            foreach(SequenceVariable var in seqVars.Keys)
            {
                if(name == var.Name)
                    return var;
            }
            return null;
        }
        
        private void HandleDump()
        {
            string filename = env.ShowGraphWith("ycomp", "", false);
            env.WriteLine("Showing dumped graph " + filename + " with ycomp");

            String undoLog = task.procEnv.TransactionManager.ToString();
            if(undoLog.Length > 0)
            {
                filename = "undo.log";
                StreamWriter sw = new StreamWriter(filename, false);
                sw.Write(undoLog);
                sw.Close();
                env.WriteLine("Written undo log to " + filename);
            }
        }

        private void HandleAsGraph(SequenceBase seq)
        {
            VariableOrAttributeAccessParserAndValueFetcher parserFetcher = new VariableOrAttributeAccessParserAndValueFetcher(
                env, debuggerProcEnv, task.debugSequences);
            object toBeShownAsGraph;
            AttributeType attrType;
            bool abort = parserFetcher.FetchObjectToBeShownAsGraph(seq, out toBeShownAsGraph, out attrType);
            if(abort)
            {
                env.WriteLine("Back from as-graph display to debugging.");
                return;
            }

            INamedGraph graph = debuggerProcEnv.ProcEnv.Graph.Model.AsGraph(toBeShownAsGraph, attrType, task.procEnv.Graph);
            if(graph == null)
            {
                if(toBeShownAsGraph is INamedGraph)
                    graph = toBeShownAsGraph as INamedGraph;
                else if(toBeShownAsGraph is IGraph)
                {
                    env.WriteLine("Clone and assign names to unnamed graph for display.");
                    graph = (toBeShownAsGraph as IGraph).CloneAndAssignNames();
                }
            }
            if(graph == null)
            {
                env.WriteLine("Was not able to get a named graph for the object specified.");
                env.WriteLine("Back from as-graph display to debugging.");
                return;
            }
            env.WriteLine("Showing graph for the object specified...");
            graphViewerClient.ClearGraph();
            graphViewerClient.Graph = graph;
            graphViewerClient.UploadGraph();

            env.PauseUntilAnyKeyPressed("...press any key to continue...");

            env.WriteLine("...return to normal graph.");
            graphViewerClient.ClearGraph();
            graphViewerClient.Graph = task.procEnv.NamedGraph;
            if(!graphViewerClient.dumpInfo.IsExcludedGraph())
                graphViewerClient.UploadGraph();

            env.WriteLine("Back from as-graph display to debugging.");
        }

        private void HandleUserHighlight(SequenceBase seq)
        {
            env.Write("Enter name of variable or id of visited flag to highlight (multiple values may be given comma-separated; just enter for abort): ");
            String str = env.ReadLine();
            Highlighter highlighter = new Highlighter(env, debuggerProcEnv, realizers, renderRecorder, graphViewerClient, task.debugSequences);
            List<object> values;
            List<string> annotations;
            highlighter.ComputeHighlight(seq, str, out values, out annotations);
            highlighter.DoHighlight(values, annotations);
        }

        private void HandleHighlight(List<object> originalValues, List<string> sourceNames)
        {
            Highlighter highlighter = new Highlighter(env, debuggerProcEnv, realizers, renderRecorder, graphViewerClient, task.debugSequences);
            highlighter.DoHighlight(originalValues, sourceNames);
        }

        private void HandleStackTrace()
        {
            env.Clear();
            env.WriteLineDataRendering("Current sequence call stack is:");
            DisplaySequenceContext contextTrace = new DisplaySequenceContext();
            SequenceBase[] callStack = task.debugSequences.ToArray();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                contextTrace.highlightSeq = callStack[i].GetCurrentlyExecutedSequenceBase();
                displayer.DisplaySequenceBase(callStack[i], contextTrace, callStack.Length - i, "", "");
            }
            if(env.TwoPane)
                env.PauseUntilAnyKeyPressed("Press any key to return from stack trace display...");
            else
                env.WriteLineDataRendering("continuing execution with:");
        }

        private void HandleFullState()
        {
            env.Clear();
            env.WriteLineDataRendering("Current execution state is:");
            PrintVariables(null, null);
            DisplaySequenceContext contextTrace = new DisplaySequenceContext();
            SequenceBase[] callStack = task.debugSequences.ToArray();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                SequenceBase currSeq = callStack[i].GetCurrentlyExecutedSequenceBase();
                contextTrace.highlightSeq = currSeq;
                displayer.DisplaySequenceBase(callStack[i], contextTrace, callStack.Length - i, "", "");
                PrintVariables(callStack[i], currSeq != null ? currSeq : callStack[i]);
            }
            PrintVisited();
            if(env.TwoPane)
                env.PauseUntilAnyKeyPressed("Press any key to return from full state display...");
            else
                env.WriteLineDataRendering("continuing execution with:");
        }

        void HandleRefreshView()
        {
            // refresh from main menu, should also work in submenus, GUI TODO
            env.SuspendImmediateExecution();
            env.Clear();
            displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
            env.RestartImmediateExecution();
        }

        void HandleSwitchView()
        {
            if(displayer is Printer)
                displayer = new Renderer(env);
            else
                displayer = new Printer(env);
        }

        #endregion Methods for directly handling user commands

        #region Print variables

        private void PrintVariables(SequenceBase seqStart, SequenceBase seq)
        {
            if(seq != null)
            {
                env.WriteLineDataRendering("Available local variables:");
                Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
                List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
                seqStart.GetLocalVariables(seqVars, constructors, seq);
                foreach(SequenceVariable var in seqVars.Keys)
                {
                    string type;
                    string content;
                    if(var.LocalVariableValue is IDictionary)
                        EmitHelper.ToString((IDictionary)var.LocalVariableValue, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    else if(var.LocalVariableValue is IList)
                        EmitHelper.ToString((IList)var.LocalVariableValue, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    else if(var.LocalVariableValue is IDeque)
                        EmitHelper.ToString((IDeque)var.LocalVariableValue, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    else
                        EmitHelper.ToString(var.LocalVariableValue, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    env.WriteLineDataRendering("  " + var.Name + " = " + content + " : " + type);
                }
            }
            else
            {
                env.WriteLineDataRendering("Available global (non null) variables:");
                foreach(Variable var in debuggerProcEnv.ProcEnv.Variables)
                {
                    string type;
                    string content;
                    if(var.Value is IDictionary)
                        EmitHelper.ToString((IDictionary)var.Value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    else if(var.Value is IList)
                        EmitHelper.ToString((IList)var.Value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    else if(var.Value is IDeque)
                        EmitHelper.ToString((IDeque)var.Value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    else
                        EmitHelper.ToString(var.Value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
                    env.WriteLineDataRendering("  " + var.Name + " = " + content + " : " + type);
                }
            }
        }

        private void PrintVisited()
        {
            List<int> allocatedVisitedFlags = debuggerProcEnv.ProcEnv.NamedGraph.GetAllocatedVisitedFlags();
            StringBuilder sb = new StringBuilder();
            sb.Append("Allocated visited flags are: ");
            bool first = true;
            foreach(int allocatedVisitedFlag in allocatedVisitedFlags)
            {
                if(!first)
                    sb.Append(", ");
                sb.Append(allocatedVisitedFlag.ToString());
                first = false;
            }
            env.WriteLineDataRendering(sb.ToString() + ".");
        }

        #endregion Print variables


        #region Possible user choices during sequence execution

        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen directions is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        public int ChooseDirection(int direction, Sequence seq)
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            env.Clear();
            displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
            context.choice = false;

            return new UserProxyChoiceMenu(env).ChooseDirection(direction, seq);
        }

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        public int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            UserProxyChoiceMenu menu = new UserProxyChoiceMenu(env);
            menu.ChooseSequencePrintHeader(seqToExecute);

            do
            {
                context.highlightSeq = sequences[seqToExecute];
                context.choice = true;
                context.sequences = sequences;
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                context.choice = false;
                context.sequences = null;

                bool commit = menu.ChooseSequence(ref seqToExecute, sequences, seq);
                if(commit)
                    return seqToExecute;
            }
            while(true);
        }

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        private int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceParallel seq)
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            UserProxyChoiceMenu menu = new UserProxyChoiceMenu(env);
            menu.ChooseSequenceParallelPrintHeader(seqToExecute);

            do
            {
                context.highlightSeq = sequences[seqToExecute];
                context.sequences = sequences;
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                context.sequences = null;

                bool commit = menu.ChooseSequence(ref seqToExecute, sequences, seq);
                if(commit)
                    return seqToExecute;
            }
            while(true);
        }

        /// <summary>
        /// returns the maybe user altered point within the interval series, denoting the sequence to execute next
        /// the randomly chosen point is supplied; the sequence with the intervals and their corresponding sequences is supplied
        /// </summary>
        public double ChoosePoint(double pointToExecute, SequenceWeightedOne seq)
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            UserProxyChoiceMenu menu = new UserProxyChoiceMenu(env);
            menu.ChoosePointPrintHeader(pointToExecute);

            do
            {
                context.highlightSeq = seq.Sequences[seq.GetSequenceFromPoint(pointToExecute)];
                context.choice = true;
                context.sequences = seq.Sequences;
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                context.choice = false;
                context.sequences = null;

                bool commit = menu.ChoosePoint(ref pointToExecute, seq);
                if(commit)
                    break;
            }
            while(true);

            return pointToExecute;
        }

        /// <summary>
        /// returns the maybe user altered rule to execute next for the sequence given
        /// the randomly chosen rule is supplied; the object with all available rules is supplied
        /// a list of all found matches is supplied, too
        /// </summary>
        public int ChooseMatch(int totalMatchToExecute, SequenceSomeFromSet seq)
        {
            if(seq.NumTotalMatches <= 1 && lazyChoice)
            {
                env.PrintHighlightedUserDialog("Skipping choicepoint ", HighlightingMode.Choicepoint);
                env.WriteLine("as no choice needed (use the (l) command to toggle this behaviour).");
                return totalMatchToExecute;
            }

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            UserProxyChoiceMenu menu = new UserProxyChoiceMenu(env);
            menu.ChooseMatchSomeFromSetPrintHeader(totalMatchToExecute);

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, graphViewerClient);

            do
            {
                int rule;
                int match;
                seq.FromTotalMatch(totalMatchToExecute, out rule, out match);
                matchMarkerAndAnnotator.Mark(rule, match, seq);
                graphViewerClient.UpdateDisplay();
                graphViewerClient.Sync();

                context.highlightSeq = seq.Sequences[rule];
                context.choice = true;
                context.sequences = seq.Sequences;
                context.matches = new List<IMatches>(seq.Matches);
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                context.choice = false;
                context.sequences = null;
                context.matches = null;

                bool commit = menu.ChooseMatch(ref totalMatchToExecute, seq);
                matchMarkerAndAnnotator.Unmark(rule, match, seq);
                if(commit)
                    break;
            }
            while(true);

            return totalMatchToExecute;
        }

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        public int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq)
        {
            context.highlightSeq = seq;
            context.choice = true;
            env.Clear();
            displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
            context.choice = false;

            if(matches.Count <= 1 + numFurtherMatchesToApply && lazyChoice)
            {
                env.PrintHighlightedUserDialog("Skipping choicepoint ", HighlightingMode.Choicepoint);
                env.WriteLine("as no choice needed (use the (l) command to toggle this behaviour).");
                return matchToApply;
            }

            UserProxyChoiceMenu menu = new UserProxyChoiceMenu(env);
            menu.ChooseMatchPrintHeader(numFurtherMatchesToApply);

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, graphViewerClient);

            if(detailedMode)
            {
                matchMarkerAndAnnotator.MarkMatches(matches, null, null);
                matchMarkerAndAnnotator.AnnotateMatches(matches, false);
            }
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            int newMatchToRewrite = matchToApply;
            do
            {
                matchMarkerAndAnnotator.MarkMatch(matches.GetMatch(matchToApply), null, null);
                matchMarkerAndAnnotator.AnnotateMatch(matches.GetMatch(matchToApply), false);
                matchToApply = newMatchToRewrite;
                matchMarkerAndAnnotator.MarkMatch(matches.GetMatch(matchToApply), realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                matchMarkerAndAnnotator.AnnotateMatch(matches.GetMatch(matchToApply), true);
                graphViewerClient.UpdateDisplay();
                graphViewerClient.Sync();

                env.WriteLine("Showing match " + matchToApply + " (of " + matches.Count + " matches available)");

                bool commit = menu.ChooseMatch(matchToApply, matches, numFurtherMatchesToApply, seq, out newMatchToRewrite);
                if(commit)
                {
                    matchMarkerAndAnnotator.MarkMatch(matches.GetMatch(matchToApply), null, null);
                    matchMarkerAndAnnotator.AnnotateMatch(matches.GetMatch(matchToApply), false);
                    graphViewerClient.UpdateDisplay();
                    graphViewerClient.Sync();
                    return newMatchToRewrite;
                }
            }
            while(true);
        }

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        public int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            env.Clear();
            displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
            context.choice = false;

            return new UserProxyChoiceMenu(env).ChooseRandomNumber(randomNumber, upperBound, seq);
        }

        /// <summary>
        /// returns the maybe user altered random number in the range 0.0 - 1.0 exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        public double ChooseRandomNumber(double randomNumber, Sequence seq)
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            env.Clear();
            displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
            context.choice = false;

            return new UserProxyChoiceMenu(env).ChooseRandomNumber(randomNumber, seq);
        }

        /// <summary>
        /// returns the id/persistent name of a node/edge chosen by the user in yComp
        /// </summary>
        public string ChooseGraphElement()
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            graphViewerClient.WaitForElement(true);

            // Allow to abort with ESC
            while(true)
            {
                if(env.KeyAvailable && env.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    env.WriteLine("Aborted!");
                    graphViewerClient.WaitForElement(false);
                    return null;
                }
                if(graphViewerClient.CommandAvailable)
                    break;
                Thread.Sleep(100);
            }

            String cmd = graphViewerClient.ReadCommand();
            if(cmd.Length < 7 || !cmd.StartsWith("send "))
            {
                env.WriteLine("Unexpected yComp command: \"" + cmd + "\"!");
                return null;
            }

            // Skip 'n' or 'e'
            return cmd.Substring(6);
        }

        /// <summary>
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        public object ChooseValue(string type, Sequence seq)
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            env.Clear();
            displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
            context.choice = false;

            return new UserProxyChoiceMenu(env).ChooseValue(type, seq, debuggerProcEnv.ProcEnv.NamedGraph);
        }

        /// <summary>
        /// Queries the user whether to continue execution, processes the assertion given the user choice (internally).
        /// </summary>
        public void HandleAssert(bool isAlways, Func<bool> assertion, Func<string> message, params Func<object>[] values)
        {
            if(!isAlways && !task.procEnv.EnableAssertions)
                return;

            if(assertion())
                return;

            string combinedMessage = EmitHelper.GetMessageForAssertion(task.procEnv, message, values);
            task.procEnv.EmitWriterDebug.WriteLine("Assertion failed! (" + combinedMessage + ")");

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            context.highlightSeq = task.lastlyEntered;
            env.Clear();
            displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
            PrintDebugTracesStack(false);

            switch(QueryContinueOnAssertion())
            {
                case AssertionContinuation.Abort:
                    throw new Exception("Assertion failed!");
                case AssertionContinuation.Debug:
                    Trace.Assert(false, combinedMessage);
                    break;
                case AssertionContinuation.Continue:
                    break;
            }
        }

        enum AssertionContinuation
        {
            Abort,
            Debug,
            Continue
        }

        AssertionContinuation QueryContinueOnAssertion()
        {
            env.PrintInstructions(continueOnAssertionMenu, "You may ", "...");

            switch(env.LetUserChoose(continueOnAssertionMenu))
            {
                case 'a':
                    return AssertionContinuation.Abort;
                case 'd':
                    return AssertionContinuation.Debug;
                case 'c':
                    return AssertionContinuation.Continue;
                default:
                    throw new Exception("Internal error");
            }
        }


        #endregion Possible user choices during sequence execution


        #region Partial graph adding on matches for excluded graph debugging

        private void AddNeededGraphElements(IMatch match)
        {
            foreach(INode node in match.Nodes)
            {
                graphViewerClient.AddNodeEvenIfGraphExcluded(node);
            }
            foreach(IEdge edge in match.Edges)
            {
                graphViewerClient.AddEdgeEvenIfGraphExcluded(edge);
            }
            AddNeededGraphElements(match.EmbeddedGraphs);
            foreach(IMatches iteratedsMatches in match.Iterateds)
            {
                AddNeededGraphElements(iteratedsMatches);
            }
            AddNeededGraphElements(match.Alternatives);
            AddNeededGraphElements(match.Independents);
        }

        private void AddNeededGraphElements(IEnumerable<IMatch> matches)
        {
            foreach(IMatch match in matches)
            {
                AddNeededGraphElements(match);
            }
        }

        private void AddNeededGraphElements(IMatches[] matchesArray)
        {
            foreach(IMatches matches in matchesArray)
            {
                AddNeededGraphElements(matches);
            }
        }

        private void AddNeededGraphElements(IList<IMatches> matchesList)
        {
            foreach(IMatches matches in matchesList)
            {
                AddNeededGraphElements(matches);
            }
        }

        #endregion Partial graph adding on matches for excluded graph debugging


        #region Event Handling

        public void DebugNodeAdded(INode node)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.New,
                node, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, node);
            }

            if(graphViewerClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;

            graphViewerClient.AddNode(node);
            if(recordMode)
            {
                String nodeName = renderRecorder.AddedNode(node);
                graphViewerClient.AnnotateElement(node, nodeName);
            }
            else if(alwaysShow)
                graphViewerClient.UpdateDisplay();
        }

        public void DebugEdgeAdded(IEdge edge)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.New,
                edge, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, edge);
            }

            if(graphViewerClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            graphViewerClient.AddEdge(edge);
            if(recordMode)
            {
                String edgeName = renderRecorder.AddedEdge(edge);
                graphViewerClient.AnnotateElement(edge, edgeName);
            }
            else if(alwaysShow)
                graphViewerClient.UpdateDisplay();
        }

        public void DebugDeletingNode(INode node)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Delete,
                node, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, node);
            }

            if(graphViewerClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            if(!recordMode)
            {
                graphViewerClient.DeleteNode(node);
                if(alwaysShow)
                    graphViewerClient.UpdateDisplay();
            }
            else
            {
                renderRecorder.RemoveNodeAnnotation(node);
                graphViewerClient.ChangeNode(node, realizers.DeletedNodeRealizer);

                String name = graphViewerClient.Graph.GetElementName(node);
                graphViewerClient.RenameNode(name, "zombie_" + name);
                renderRecorder.DeletedNode("zombie_" + name, name);
            }
        }

        public void DebugDeletingEdge(IEdge edge)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Delete,
                edge, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, edge);
            }

            if(graphViewerClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            if(!recordMode)
            {
                graphViewerClient.DeleteEdge(edge);
                if(alwaysShow)
                    graphViewerClient.UpdateDisplay();
            }
            else
            {
                renderRecorder.RemoveEdgeAnnotation(edge);
                graphViewerClient.ChangeEdge(edge, realizers.DeletedEdgeRealizer);

                String name = graphViewerClient.Graph.GetElementName(edge);
                graphViewerClient.RenameEdge(name, "zombie_" + name);
                renderRecorder.DeletedEdge("zombie_" + name, name);
            }
        }

        public void DebugClearingGraph(IGraph graph)
        {
            if(graphViewerClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            graphViewerClient.ClearGraph();
        }

        public void DebugChangedNodeAttribute(INode node, AttributeType attrType)
        {
            if(!graphViewerClient.dumpInfo.IsExcludedGraph() || recordMode)
                graphViewerClient.ChangeNodeAttribute(node, attrType);
            
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.SetAttributes,
                node, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, node, attrType.Name);
            }
        }

        public void DebugChangedEdgeAttribute(IEdge edge, AttributeType attrType)
        {
            if(!graphViewerClient.dumpInfo.IsExcludedGraph() || recordMode)
                graphViewerClient.ChangeEdgeAttribute(edge, attrType);
            
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.SetAttributes,
                edge, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, edge, attrType.Name);
            }
        }

        public void DebugRetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Retype,
                oldElem, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, oldElem);
            }

            if(graphViewerClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            graphViewerClient.RetypingElement(oldElem, newElem);
            if(!recordMode)
                return;

            if(oldElem is INode)
            {
                INode oldNode = (INode) oldElem;
                INode newNode = (INode) newElem;
                String name;
                if(renderRecorder.WasNodeAnnotationReplaced(oldNode, newNode, out name))
                    graphViewerClient.AnnotateElement(newElem, name);
                graphViewerClient.ChangeNode(newNode, realizers.RetypedNodeRealizer);
                renderRecorder.RetypedNode(newNode);
            }
            else
            {
                IEdge oldEdge = (IEdge) oldElem;
                IEdge newEdge = (IEdge) newElem;
                String name;
                if(renderRecorder.WasEdgeAnnotationReplaced(oldEdge, newEdge, out name))
                    graphViewerClient.AnnotateElement(newElem, name);
                graphViewerClient.ChangeEdge(newEdge, realizers.RetypedEdgeRealizer);
                renderRecorder.RetypedEdge(newEdge);
            }
        }

        public void DebugSettingAddedNodeNames(string[] namesOfNodesAdded)
        {
            renderRecorder.SetAddedNodeNames(namesOfNodesAdded);
        }

        public void DebugSettingAddedEdgeNames(string[] namesOfEdgesAdded)
        {
            renderRecorder.SetAddedEdgeNames(namesOfEdgesAdded);
        }

        public void DebugBeginExecution(IPatternMatchingConstruct patternMatchingConstruct)
        {
            task.patternMatchingConstructsExecuted.Add(patternMatchingConstruct);
            task.skipMode.Add(false);
            if(task.computationsEnteredStack.Count > 0) // only in subrule debugging, otherwise printed by SequenceEntered
            {
                env.WriteLine("Entry to " + patternMatchingConstruct.Symbol); // subrule traces log
            }
        }

        public void DebugMatchedBefore(IList<IMatches> matchesList)
        {
            if(!stepMode)
                return;

            if(!detailedMode)
                return;

            if(!detailedModeShowPreMatches)
                return;

            if(task.computationsEnteredStack.Count > 0)
                return;

            env.WriteLine("PreMatched " + ProducerNames(matchesList));

            renderRecorder.RemoveAllAnnotations();

            if(graphViewerClient.dumpInfo.IsExcludedGraph())
            {
                if(!recordMode)
                    graphViewerClient.ClearGraph();

                // add all elements from match to graph and excludedGraphElementsIncluded
                AddNeededGraphElements(matchesList);

                graphViewerClient.AddNeighboursAndParentsOfNeededGraphElements();
            }

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, graphViewerClient);

            DebugMatchMark(matchMarkerAndAnnotator, matchesList);

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            env.PauseUntilAnyKeyPressed("Press any key to continue " + (task.debugSequences.Count > 0 ? "(with the matches remaining after filtering/of the selected rule)..." : "..."));

            DebugMatchUnmark(matchMarkerAndAnnotator, matchesList);

            renderRecorder.RemoveAllAnnotations();
        }

        public static string ProducerNames(IList<IMatches> matchesList)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach(IMatches matches in matchesList)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(matches.Producer.Name);
            }
            return sb.ToString();
        }

        private void DebugMatchMark(MatchMarkerAndAnnotator matchMarkerAndAnnotator, IList<IMatches> matchesList)
        {
            if(matchesList.Count == 0)
                return;

            if(matchesList.Count == 1)
            {
                matchMarkerAndAnnotator.MarkMatches(matchesList[0], realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                matchMarkerAndAnnotator.AnnotateMatches(matchesList[0], true);
                return;
            }

            Dictionary<string, int> rulePatternNameToCurrentInstance = GetRulePatternNamesWithMultipleInstances(matchesList);

            foreach(IMatches matches in matchesList)
            {
                String rulePatternName = matches.Producer.RulePattern.PatternGraph.Name;
                if(rulePatternNameToCurrentInstance.ContainsKey(rulePatternName))
                {
                    rulePatternNameToCurrentInstance[rulePatternName] = rulePatternNameToCurrentInstance[rulePatternName] + 1;
                    rulePatternName = rulePatternName + "'" + rulePatternNameToCurrentInstance[rulePatternName];
                }

                DebugMatchMark(matchMarkerAndAnnotator, matches, rulePatternName);
            }
            renderRecorder.SetCurrentRuleNameForMatchAnnotation(null);
        }

        // returns rule pattern names that occur multiple times in the matchesArray (as producer of an IMatches object), mapping them to 0
        private Dictionary<string, int> GetRulePatternNamesWithMultipleInstances(IList<IMatches> matchesList)
        {
            Dictionary<string, int> rulePatternNameToCountInstances = new Dictionary<string, int>();
            foreach(IMatches matches in matchesList)
            {
                String rulePatternName = matches.Producer.RulePattern.PatternGraph.Name;
                if(rulePatternNameToCountInstances.ContainsKey(rulePatternName))
                    rulePatternNameToCountInstances[rulePatternName] = rulePatternNameToCountInstances[rulePatternName] + 1;
                else
                    rulePatternNameToCountInstances[rulePatternName] = 1;
            }

            List<string> rulesWithOnlyOneInstance = new List<string>();
            List<string> rulesWithMultipleInstances = new List<string>();
            foreach(KeyValuePair<string, int> rulePatternNameWithCountInstances in rulePatternNameToCountInstances)
            {
                if(rulePatternNameWithCountInstances.Value == 1)
                    rulesWithOnlyOneInstance.Add(rulePatternNameWithCountInstances.Key);
                else
                    rulesWithMultipleInstances.Add(rulePatternNameWithCountInstances.Key);
            }

            foreach(string ruleWithOnlyOneInstance in rulesWithOnlyOneInstance)
            {
                rulePatternNameToCountInstances.Remove(ruleWithOnlyOneInstance);
            }

            foreach(string ruleWithMultipleInstances in rulesWithMultipleInstances)
            {
                rulePatternNameToCountInstances[ruleWithMultipleInstances] = 0;
            }

            return rulePatternNameToCountInstances;
        }

        private void DebugMatchMark(MatchMarkerAndAnnotator matchMarkerAndAnnotator, IMatches matches, String rulePatternName)
        {
            renderRecorder.SetCurrentRuleNameForMatchAnnotation(rulePatternName);

            matchMarkerAndAnnotator.MarkMatches(matches, realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
            matchMarkerAndAnnotator.AnnotateMatches(matches, true);
        }

        private void DebugMatchUnmark(MatchMarkerAndAnnotator matchMarkerAndAnnotator, IList<IMatches> matchesList)
        {
            if(matchesList.Count == 0)
                return;

            if(matchesList.Count == 1)
            {
                matchMarkerAndAnnotator.MarkMatches(matchesList[0], null, null);
                matchMarkerAndAnnotator.AnnotateMatches(matchesList[0], false);
                return;
            }

            foreach(IMatches matches in matchesList)
            {
                DebugMatchUnmark(matchMarkerAndAnnotator, matches);
            }
        }

        private void DebugMatchUnmark(MatchMarkerAndAnnotator matchMarkerAndAnnotator, IMatches matches)
        {
            matchMarkerAndAnnotator.MarkMatches(matches, null, null);
            matchMarkerAndAnnotator.AnnotateMatches(matches, false);
        }

        public void DebugMatchedAfter(IMatches[] matches, bool[] special)
        {
            if(Count(matches) == 0) // happens e.g. from compiled sequences firing the event always, but the Finishing only comes in case of Count!=0
                return;

            DebugMatchedAfterImpl(matches, special);

            topLevelRuleChanged = false;
        }

        private int Count(IMatches[] matchesArray)
        {
            int count = 0;
            foreach(IMatches matches in matchesArray)
            {
                count += matches.Count;
            }
            return count;
        }

        private bool CurrentlyExecutedPatternMatchingConstructIs(PatternMatchingConstructType constructType)
        {
            return task.patternMatchingConstructsExecuted.Count > 0
                        && task.patternMatchingConstructsExecuted[task.patternMatchingConstructsExecuted.Count - 1].ConstructType == constructType;
        }

        private void DebugMatchedAfterImpl(IMatches[] matches, bool[] special)
        {
            // integrate matched actions into subrule traces stack
            task.computationsEnteredStack.Add(new SubruleComputation(ProducerNames(matches)));

            SubruleDebuggingConfigurationRule cr = null;
            SubruleDebuggingDecision d = SubruleDebuggingDecision.Undefined;
            foreach(IMatches _matches in matches)
            {
                d = debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Match,
                    _matches, task.procEnv, out cr);
                if(d == SubruleDebuggingDecision.Break)
                    break;
            }
            if(d == SubruleDebuggingDecision.Break)
                InternalHalt(cr, matches);
            else if(d == SubruleDebuggingDecision.Continue)
            {
                task.recentlyMatched = task.lastlyEntered;
                if(!detailedMode)
                    return;
                if(recordMode)
                {
                    DebugFinished(null, null);
                    ++matchDepth;
                    renderRecorder.RemoveAllAnnotations();
                }
                return;
            }

            if(dynamicStepMode && !dynamicStepModeSkip)
            {
                dynamicStepModeSkip = true;
                graphViewerClient.UpdateDisplay();
                graphViewerClient.Sync();
                context.highlightSeq = task.lastlyEntered;
                context.success = true;
                env.SuspendImmediateExecution();
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                env.RestartImmediateExecution();

                if(!QueryUser(task.lastlyEntered))
                {
                    task.recentlyMatched = task.lastlyEntered;
                    return;
                }
            }

            task.recentlyMatched = task.lastlyEntered;

            if(!detailedMode)
                return;

            if(recordMode)
            {
                DebugFinished(null, null);
                ++matchDepth;
                if(outOfDetailedMode)
                {
                    renderRecorder.RemoveAllAnnotations();
                    return;
                }
            }

            if(!detailedModeShowPostMatches && task.computationsEnteredStack.Count > 1)
                return;

            if(matchDepth++ > 0 || task.computationsEnteredStack.Count > 0)
            {
                env.WriteLine("Matched " + ProducerNames(matches));
                if(Count(matches) == 1 && CurrentlyExecutedPatternMatchingConstructIs(PatternMatchingConstructType.RuleCall))
                    return;
            }

            renderRecorder.RemoveAllAnnotations();
            renderRecorder.SetCurrentRuleName(ProducerNames(matches));

            if(graphViewerClient.dumpInfo.IsExcludedGraph())
            {
                if(!recordMode)
                    graphViewerClient.ClearGraph();

                // add all elements from match to graph and excludedGraphElementsIncluded
                AddNeededGraphElements(matches);

                graphViewerClient.AddNeighboursAndParentsOfNeededGraphElements();
            }

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, graphViewerClient);

            DebugMatchMark(matchMarkerAndAnnotator, matches);

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            QueryForSkipAsRequired(Count(matches), false);

            //matchMarkerAndAnnotator.MarkMatches(matches, null, null);
            DebugMatchUnmark(matchMarkerAndAnnotator, matches);

            renderRecorder.ApplyChanges(graphViewerClient);
            renderRecorder.RemoveAllAnnotations();
            renderRecorder.ResetAllChangedElements();

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
        }

        private void QueryForSkipAsRequired(int countMatches, bool inMatchByMatchProcessing)
        {
            if(CurrentlyExecutedPatternMatchingConstructIs(PatternMatchingConstructType.RuleAllCall) && countMatches > 1
                || CurrentlyExecutedPatternMatchingConstructIs(PatternMatchingConstructType.RuleCountAllCall) && countMatches > 1
                || CurrentlyExecutedPatternMatchingConstructIs(PatternMatchingConstructType.MultiRuleAllCall)
                || CurrentlyExecutedPatternMatchingConstructIs(PatternMatchingConstructType.SomeFromSet))
            {
                UserChoiceMenu menu = inMatchByMatchProcessing ? skipAsRequiredInMatchByMatchProcessingMenu : skipAsRequiredMenu;
                env.PrintInstructions(menu, "Press ", "...");

                switch(env.LetUserChoose(menu))
                {
                case 'k':
                    task.skipMode[task.skipMode.Count - 1] = true;
                    break;
                case ' ':
                    break;
                default:
                    throw new Exception("Internal error");
                }
            }
            else
            {
                env.PauseUntilAnyKeyPressed(inMatchByMatchProcessing
                    ? "Press any key to apply rewrite..."
                    : "Press any key to show single matches and apply rewrite...");
            }
        }

        private bool SpecialExisting(bool[] specialArray)
        {
            bool specialExisting = false;
            foreach(bool special in specialArray)
            {
                specialExisting |= special;
            }
            return specialExisting;
        }

        public static String ProducerNames(IMatches[] matchesArray)
        {
            StringBuilder combinedName = new StringBuilder();
            bool first = true;
            if(matchesArray != null)
            {
                foreach(IMatches matches in matchesArray)
                {
                    if(first)
                        first = false;
                    else
                        combinedName.Append(",");
                    combinedName.Append(matches.Producer.Name);
                }
            }
            return combinedName.ToString();
        }

        public void DebugMatchSelected(IMatch match, bool special, IMatches matches)
        {
            task.recentlyMatched = task.lastlyEntered;

            if(!detailedMode)
                return;

            if(!detailedModeShowPostMatches && task.computationsEnteredStack.Count > 1)
                return;

            if(task.skipMode.Count > 0 && task.skipMode[task.skipMode.Count - 1])
                return;

            env.WriteLine("Showing single match of " + matches.Producer.Name + "...");

            renderRecorder.ApplyChanges(graphViewerClient);
            renderRecorder.ResetAllChangedElements();
            renderRecorder.RemoveAllAnnotations();
            renderRecorder.SetCurrentRuleName(matches.Producer.RulePattern.PatternGraph.Name);

            if(graphViewerClient.dumpInfo.IsExcludedGraph())
            {
                if(!recordMode)
                    graphViewerClient.ClearGraph();

                // add all elements from match to graph and excludedGraphElementsIncluded
                AddNeededGraphElements(match);
                
                graphViewerClient.AddNeighboursAndParentsOfNeededGraphElements();
            }

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, graphViewerClient);
            matchMarkerAndAnnotator.MarkMatch(match, realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
            matchMarkerAndAnnotator.AnnotateMatch(match, true);

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            QueryForSkipAsRequired(matches.Count, true);

            matchMarkerAndAnnotator.MarkMatch(match, null, null);

            recordMode = true;
            graphViewerClient.NodeRealizerOverride = realizers.NewNodeRealizer;
            graphViewerClient.EdgeRealizerOverride = realizers.NewEdgeRealizer;
            renderRecorder.ResetAddedNames();
        }

        public void DebugRewritingSelectedMatch()
        {
            renderRecorder.ResetAddedNames();
        }

        public void DebugSelectedMatchRewritten()
        {
            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            if(detailedMode && detailedModeShowPostMatches)
            {
                if(task.skipMode.Count > 0 && task.skipMode[task.skipMode.Count - 1])
                    return;

                env.PauseUntilAnyKeyPressed("Rewritten - Debugging detailed continues with any key...");
            }
        }

        public void DebugFinishedSelectedMatch()
        {
            if(!detailedMode)
                return;

            // clear annotations after displaying single match so user can choose match to apply (occurs before on match selected is fired)
            recordMode = false;
            graphViewerClient.NodeRealizerOverride = null;
            graphViewerClient.EdgeRealizerOverride = null;
            renderRecorder.ApplyChanges(graphViewerClient);
            renderRecorder.ResetAllChangedElements();
            renderRecorder.RemoveAllAnnotations();
        }

        public void DebugFinished(IMatches[] matches, bool[] special)
        {
            if(task.skipMode.Count > 0)
                task.skipMode[task.skipMode.Count - 1] = false;

            // integrate matched actions into subrule traces stack
            if(matches != null)
                RemoveUpToEntryForExit(ProducerNames(matches));

            if(outOfDetailedMode && (task.computationsEnteredStack.Count <= outOfDetailedModeTarget || task.computationsEnteredStack.Count==0))
            {
                detailedMode = true;
                outOfDetailedMode = false;
                outOfDetailedModeTarget = -1;
                return;
            }

            if(!detailedMode)
                return;

            if(task.computationsEnteredStack.Count > 3 && !detailedModeShowPostMatches)
                return;

            env.Write("Finished " + ProducerNames(matches) + " - ");
            if(detailedModeShowPostMatches)
            {
                graphViewerClient.UpdateDisplay();
                graphViewerClient.Sync();
                QueryContinueOrTrace(false);
            }
            else
            {
                if(topLevelRuleChanged)
                {
                    QueryContinueWhenShowPostDisabled();
                    return;
                }
            }

            renderRecorder.ApplyChanges(graphViewerClient);

            renderRecorder.ResetAllChangedElements();
            recordMode = false;
            graphViewerClient.NodeRealizerOverride = null;
            graphViewerClient.EdgeRealizerOverride = null;
            matchDepth--;
        }

        public void DebugEndExecution(IPatternMatchingConstruct patternMatchingConstruct, object result)
        {
            Debug.Assert(task.patternMatchingConstructsExecuted[task.patternMatchingConstructsExecuted.Count - 1].Symbol == patternMatchingConstruct.Symbol);
            task.patternMatchingConstructsExecuted.RemoveAt(task.patternMatchingConstructsExecuted.Count - 1);
            task.skipMode.RemoveAt(task.skipMode.Count - 1);

            if(task.patternMatchingConstructsExecuted.Count > 0)
            {
                if(patternMatchingConstruct is SequenceBase)
                {
                    graphViewerClient.UpdateDisplay();
                    graphViewerClient.Sync();
                    context.highlightSeq = (SequenceBase)task.patternMatchingConstructsExecuted[task.patternMatchingConstructsExecuted.Count - 1];
                    context.success = false;
                    if(task.debugSequences.Count > 0)
                    {
                        env.Clear();
                        displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    }

                    /*if(detailedMode && detailedModeShowPostMatches
                        && (seq.HasSequenceType(SequenceType.Backtrack)
                            || seq.HasSequenceType(SequenceType.ForMatch)
                            || IsRuleContainedInComplexConstruct(seq)))
                    {
                        return;
                    }*/

                    /*if(seq is SequenceSequenceCallInterpreted)
                    {
                        SequenceSequenceCallInterpreted seqCall = (SequenceSequenceCallInterpreted)seq;
                        if(seqCall.SequenceDef is SequenceDefinitionCompiled)
                        {
                            PrintDebugInstructions();
                        }
                    }*/

                    if(stepMode)
                        QueryUser((SequenceBase)patternMatchingConstruct);
                }
                else // compiled sequence
                {
                    if(task.computationsEnteredStack.Count > 0) // only in subrule debugging
                    {
                        env.WriteLine("Exit from " + patternMatchingConstruct.Symbol); // subrule traces log
                    }
                }
            }
        }

        private void QueryContinueWhenShowPostDisabled()
        {
            do
            {
                env.PrintInstructions(queryContinueWhenShowPostDisabledMenu, "Debugging (detailed) break, press ", "...");

                switch(env.LetUserChoose(queryContinueWhenShowPostDisabledMenu))
                {
                case 'a':
                    env.Cancel();
                    return;                               // never reached
                case 'f':
                    HandleFullState();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    PrintDebugTracesStack(true);
                    break;
                case ' ':
                    return;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        public void DebugEnteringSequence(SequenceBase seq)
        {
            // root node of sequence entered and interactive debugging activated
            if(stepMode && task.lastlyEntered == null)
            {
                graphViewerClient.UpdateDisplay();
                graphViewerClient.Sync();
                if(task.debugSequences.Count > 0)
                {
                    env.SuspendImmediateExecution();
                    env.Clear();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    env.RestartImmediateExecution();
                }
                PrintDebugInstructionsOnEntering();
                QueryUser(seq);
            }

            task.lastlyEntered = seq;
            task.recentlyMatched = null;

            // Entering a loop?
            if(IsLoop(seq))
                task.loopList.AddFirst((Sequence)seq);

            // Entering a subsequence called?
            if(seq.HasSequenceType(SequenceType.SequenceDefinitionInterpreted))
            {
                task.loopList.AddFirst((Sequence)seq);
                task.debugSequences.Push((Sequence)seq);
            }

            // Breakpoint reached?
            bool breakpointReached = false;
            if(seq is ISequenceSpecial
                && ((ISequenceSpecial)seq).Special)
            {
                stepMode = true;
                breakpointReached = true;
            }

            if(!stepMode)
                return;

            if(seq is IPatternMatchingConstruct || seq.HasSequenceType(SequenceType.SequenceCall)
                || breakpointReached)
            {
                graphViewerClient.UpdateDisplay();
                graphViewerClient.Sync();
                context.highlightSeq = seq;
                context.success = false;
                if(task.debugSequences.Count > 0)
                {
                    env.SuspendImmediateExecution();
                    env.Clear();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    env.RestartImmediateExecution();
                }

                if(detailedMode && detailedModeShowPostMatches
                    && (seq.HasSequenceType(SequenceType.Backtrack)
                        || seq.HasSequenceType(SequenceType.ForMatch)
                        || IsRuleContainedInComplexConstruct(seq)))
                {
                    return;
                }

                if(seq is SequenceSequenceCallInterpreted)
                {
                    SequenceSequenceCallInterpreted seqCall = (SequenceSequenceCallInterpreted)seq;
                    if(seqCall.SequenceDef is SequenceDefinitionCompiled)
                    {
                        PrintDebugInstructionsSubruleDebugging();
                    }
                }

                QueryUser(seq);
            }
        }

        private static bool IsRuleContainedInComplexConstruct(SequenceBase seq)
        {
            if(!seq.HasSequenceType(SequenceType.RuleCall)
                && !seq.HasSequenceType(SequenceType.RuleAllCall)
                && !seq.HasSequenceType(SequenceType.RuleCountAllCall))
            {
                return false;
            }
            SequenceRuleCall ruleCall = (SequenceRuleCall)seq;
            return ruleCall.Parent != null;
        }

        public void DebugExitingSequence(SequenceBase seq)
        {
            dynamicStepModeSkip = false;

            if(seq == task.curStepSequence)
                stepMode = true;

            if(IsLoop(seq))
                task.loopList.RemoveFirst();

            if(seq.HasSequenceType(SequenceType.SequenceDefinitionInterpreted))
            {
                task.debugSequences.Pop();
                task.loopList.RemoveFirst();
            }

            if(task.debugSequences.Count == 1 && seq == task.debugSequences.Peek())
            {
                context.highlightSeq = null;
                env.SuspendImmediateExecution();
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "State at end of sequence ", "< leaving");
                env.RestartImmediateExecution();
                if(env.TwoPane)
                    env.PauseUntilAnyKeyPressed("Showing sequence state when leaving sequence, press any key to continue...");
            }
        }

        private void PrintDebugInstructionsOnEntering()
        {
            env.PrintHighlightedUserDialog("Debug started", HighlightingMode.SequenceStart); // form TODO: give up on it or try hard with color codes embedded?
            env.PrintInstructions(debuggerMainSequenceEnteringMenu, " -- available commands are: ", " (plus Ctrl+C for forced abort).");
            queryUserMenu = debuggerMainSequenceEnteringMenu;
        }

        private static bool IsLoop(SequenceBase seq)
        {
            if(!(seq is Sequence))
                return false;

            switch(((Sequence)seq).SequenceType)
            {
            case SequenceType.IterationMin:
            case SequenceType.IterationMinMax:
            case SequenceType.ForContainer:
            case SequenceType.ForIntegerRange:
            case SequenceType.ForIndexAccessEquality:
            case SequenceType.ForIndexAccessOrdering:
            case SequenceType.ForAdjacentNodes:
            case SequenceType.ForAdjacentNodesViaIncoming:
            case SequenceType.ForAdjacentNodesViaOutgoing:
            case SequenceType.ForIncidentEdges:
            case SequenceType.ForIncomingEdges:
            case SequenceType.ForOutgoingEdges:
            case SequenceType.ForReachableNodes:
            case SequenceType.ForReachableNodesViaIncoming:
            case SequenceType.ForReachableNodesViaOutgoing:
            case SequenceType.ForReachableEdges:
            case SequenceType.ForReachableEdgesViaIncoming:
            case SequenceType.ForReachableEdgesViaOutgoing:
            case SequenceType.ForBoundedReachableNodes:
            case SequenceType.ForBoundedReachableNodesViaIncoming:
            case SequenceType.ForBoundedReachableNodesViaOutgoing:
            case SequenceType.ForBoundedReachableEdges:
            case SequenceType.ForBoundedReachableEdgesViaIncoming:
            case SequenceType.ForBoundedReachableEdgesViaOutgoing:
            case SequenceType.ForNodes:
            case SequenceType.ForEdges:
            case SequenceType.ForMatch:
            case SequenceType.Backtrack:
                return true;
            default:
                return false;
            }
        }

        /// <summary>
        /// informs debugger about the end of a loop iteration, so it can display the state at the end of the iteration
        /// </summary>
        public void DebugEndOfIteration(bool continueLoop, SequenceBase seq)
        {
            if(stepMode || dynamicStepMode)
            {
                env.SuspendImmediateExecution();
                env.Clear();
                if(seq is SequenceBacktrack)
                {
                    SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                    String text;
                    if(seqBack.Seq.ExecutionState == SequenceExecutionState.Success)
                        text = "Success";
                    else
                    {
                        if(continueLoop)
                            text = "Backtracking";
                        else
                            text = "Backtracking possibilities exhausted, fail";
                    }
                    context.highlightSeq = seq;
                    displayer.DisplaySequence((Sequence)seq, context, task.debugSequences.Count, text + ": ", continueLoop ? "" : "< leaving backtracking brackets");
                }
                else if(seq is SequenceDefinition)
                {
                    SequenceDefinition seqDef = (SequenceDefinition)seq;
                    context.highlightSeq = seq;
                    displayer.DisplaySequence((Sequence)seq, context, task.debugSequences.Count, "State at end of sequence call" + ": ", "< leaving");
                }
                else if(seq is SequenceExpressionMappingClause)
                {
                    context.highlightSeq = seq;
                    displayer.DisplaySequenceExpression((SequenceExpression)seq, context, task.debugSequences.Count, "State at end of mapping step" + ": ", continueLoop ? "" : "< leaving mapping");
                }
                else
                {
                    context.highlightSeq = seq;
                    displayer.DisplaySequence((Sequence)seq, context, task.debugSequences.Count, "State at end of iteration step" + ": ", continueLoop ? "" : "< leaving loop");
                }
                env.RestartImmediateExecution();
                if(env.TwoPane)
                    env.PauseUntilAnyKeyPressed("Showing sequence state when leaving construct, press any key to continue...");
                else
                    env.WriteLine(" (updating, please wait...)");
            }
        }

        public void DebugSpawnSequences(SequenceParallel parallel, params ParallelExecutionBegin[] parallelExecutionBegins)
        {
            env.PrintHighlighted("parallel execution start" + ": ", HighlightingMode.SequenceStart);
            context.highlightSeq = parallel;
            displayer.DisplaySequenceBase(parallel, context, task.debugSequences.Count, "", "");

            List<Sequence> sequences = new List<Sequence>(parallel.ParallelChildren);
            int seqToExecute = ChooseSequence(0, sequences, parallel);

            for(int i = 0; i < parallelExecutionBegins.Length; ++i)
            {
                ParallelExecutionBegin parallelExecutionBegin = parallelExecutionBegins[i];

                DebuggerTask debuggerTask = new DebuggerTask(this, parallelExecutionBegin.procEnv);
                sequencesToDebuggerTask.Add(parallelExecutionBegin.sequence, debuggerTask);
                debuggerTask.RegisterGraphEvents(parallelExecutionBegin.procEnv.NamedGraph);
                debuggerTask.RegisterActionEvents(parallelExecutionBegin.procEnv);

                if(i == seqToExecute) // assumption: same amount of parallelExecutionBegins like children in SequenceParallelExecute
                {
                    task.isActive = false;
                    task.isParentOfActive = true;
                    tasks.Push(debuggerTask);
                    task.isActive = true;

                    InitParallelRewriteSequence(parallelExecutionBegin.sequence, stepMode);

                    graphViewerClient.ClearGraph();
                    graphViewerClient.Graph = parallelExecutionBegin.procEnv.NamedGraph;
                    if(!graphViewerClient.dumpInfo.IsExcludedGraph())
                        graphViewerClient.UploadGraph();
                }
            }
        }

        public void InitParallelRewriteSequence(Sequence seq, bool withStepMode)
        {
            task.debugSequences.Clear();
            task.debugSequences.Push(seq);
            task.curStepSequence = null;
            stepMode = withStepMode;
            recordMode = false;
            alwaysShow = false;
            detailedMode = false;
            outOfDetailedMode = false;
            outOfDetailedModeTarget = -1;
            dynamicStepMode = false;
            dynamicStepModeSkip = false;
            task.lastlyEntered = null;
            task.recentlyMatched = null;
        }

        // event arrives for the proc env that spawned, while the currently actively debugged task is a spawned one
        public void DebugJoinSequences(SequenceParallel parallel, params ParallelExecutionBegin[] parallelExecutionBegins)
        {
            task.isActive = false;
            tasks.Pop();
            task.isParentOfActive = false;
            task.isActive = true;
            if(tasks.Count > 1)
            {
                DebuggerTask backup = tasks.Pop();
                task.isParentOfActive = true;
                tasks.Push(backup);
            }

            foreach(ParallelExecutionBegin parallelExecutionBegin in parallelExecutionBegins)
            {
                DebuggerTask debuggerTask = sequencesToDebuggerTask[parallelExecutionBegin.sequence];
                sequencesToDebuggerTask.Remove(parallelExecutionBegin.sequence); // assumption: the very same sequence begin object is used to report the joining
                debuggerTask.Close();
            }

            InitParallelRewriteSequence((Sequence)task.debugSequences.Peek(), stepMode);

            graphViewerClient.ClearGraph();
            graphViewerClient.Graph = task.procEnv.NamedGraph;
            if(!graphViewerClient.dumpInfo.IsExcludedGraph())
                graphViewerClient.UploadGraph();

            env.PrintHighlighted("< leaving parallel execution", HighlightingMode.SequenceStart);
            env.WriteLine();

            dynamicStepMode = false;
        }

        /// <summary>
        /// informs debugger about the change of the graph, so it can switch yComp display to the new one
        /// called just before switch with the new one, the old one is the current graph
        /// </summary>
        public void DebugSwitchToGraph(IGraph newGraph)
        {
            // potential future extension: display the stack of graphs instead of only the topmost one
            // with the one at the forefront being the top of the stack; would save clearing and uploading
            task.UnregisterGraphEvents(task.procEnv.NamedGraph);
            env.PrintHighlightedUserDialog("Entering graph...\n", HighlightingMode.SequenceStart);
            graphViewerClient.ClearGraph();
            graphViewerClient.Graph = (INamedGraph)newGraph;
            if(!graphViewerClient.dumpInfo.IsExcludedGraph())
                graphViewerClient.UploadGraph();
            task.RegisterGraphEvents((INamedGraph)newGraph);
        }

        /// <summary>
        /// informs debugger about the change of the graph, so it can switch yComp display to the new one
        /// called just after the switch with the old one, the new one is the current graph
        /// </summary>
        public void DebugReturnedFromGraph(IGraph oldGraph)
        {
            task.UnregisterGraphEvents((INamedGraph)oldGraph);
            env.PrintHighlightedUserDialog("...leaving graph\n", HighlightingMode.SequenceStart);
            graphViewerClient.ClearGraph();
            graphViewerClient.Graph = task.procEnv.NamedGraph;
            if(!graphViewerClient.dumpInfo.IsExcludedGraph())
                graphViewerClient.UploadGraph();
            task.RegisterGraphEvents(task.procEnv.NamedGraph);
        }

        public void DebugEnter(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Add, 
                message, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, message, values);

            SubruleComputation entry = new SubruleComputation(task.procEnv.NamedGraph, 
                SubruleComputationType.Entry, message, values);
            task.computationsEnteredStack.Add(entry);
            if(detailedMode && detailedModeShowPostMatches)
                env.WriteLineDataRendering(entry.ToString(false)); // subrule traces log interpreted as main data object, entry to embedded sequence
        }

        public void DebugExit(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Rem, 
                message, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, message, values);

            RemoveUpToEntryForExit(message);
            if(detailedMode && detailedModeShowPostMatches)
            {
                SubruleComputation exit = new SubruleComputation(task.procEnv.NamedGraph,
                    SubruleComputationType.Exit, message, values);
                env.WriteLineDataRendering(exit.ToString(false)); // subrule traces log interpreted as main data object, exit from embedded sequence
            }
            if(outOfDetailedMode && (task.computationsEnteredStack.Count <= outOfDetailedModeTarget || task.computationsEnteredStack.Count == 0))
            {
                detailedMode = true;
                outOfDetailedMode = false;
                outOfDetailedModeTarget = -1;
            }
        }

        private void RemoveUpToEntryForExit(string message)
        {
            int posOfEntry = 0;
            for(int i = task.computationsEnteredStack.Count - 1; i >= 0; --i)
            {
                if(task.computationsEnteredStack[i].type == SubruleComputationType.Entry)
                {
                    posOfEntry = i;
                    break;
                }
            }
            if(task.computationsEnteredStack.Count == 0)
            {
                env.WriteLine("Trying to remove from debug trace stack the entry for the exit message/computation: " + message);
                env.WriteLine("But found no enclosing message/computation entry as the debug trace stack is empty!");
                throw new Exception("Mismatch of debug enter / exit, mismatch in Debug::add(message,...) / Debug::rem(message,...)");
            }
            if(task.computationsEnteredStack[posOfEntry].message != message)
            {
                env.WriteLine("Trying to remove from debug trace stack the entry for the exit message/computation: " + message);
                env.WriteLine("But found as enclosing message/computation entry: " + task.computationsEnteredStack[posOfEntry].message);
                throw new Exception("Mismatch of debug enter / exit, mismatch in Debug::add(message,...) / Debug::rem(message,...)");
            }
            task.computationsEnteredStack.RemoveRange(posOfEntry, task.computationsEnteredStack.Count - posOfEntry);
            if(task.computationsEnteredStack.Count == 0)
                topLevelRuleChanged = true; // todo: refine - gives wrong result in case an embedded exec is called from a procedure
        }

        public void DebugEmit(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Emit,
                message, task.procEnv, out cr) == SubruleDebuggingDecision.Break)
            {
                InternalHalt(cr, message, values);
            }

            SubruleComputation emit = new SubruleComputation(task.procEnv.NamedGraph,
                SubruleComputationType.Emit, message, values);
            task.computationsEnteredStack.Add(emit);
            if(detailedMode)
                env.WriteLine(emit.ToString(false)); // subrule traces log
        }

        public void DebugHalt(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Halt,
                message, task.procEnv, out cr) == SubruleDebuggingDecision.Continue)
            {
                return;
            }

            env.Write("Halting: " + message);
            for(int i = 0; i < values.Length; ++i)
            {
                env.Write(" ");
                env.Write(EmitHelper.ToStringAutomatic(values[i], task.procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null));
            }
            env.WriteLine();

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            if(!detailedMode)
            {
                context.highlightSeq = task.lastlyEntered;
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                PrintDebugTracesStack(false);
            }

            QueryContinueOrTrace(true);
        }

        private void InternalHalt(SubruleDebuggingConfigurationRule cr, object data, params object[] additionalData)
        {
            env.PrintHighlightedUserDialog("Break ", HighlightingMode.Breakpoint); // subrule traces log
            env.WriteLine("because " + cr.ToString(data, task.procEnv.NamedGraph, additionalData));

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            if(!detailedMode)
            {
                context.highlightSeq = task.lastlyEntered;
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                PrintDebugTracesStack(false);
            }

            QueryContinueOrTrace(true);
        }

        /// <summary>
        /// highlights the values in the graphs if debugging is active (annotating them with the source names)
        /// </summary>
        public void DebugHighlight(string message, List<object> values, List<string> sourceNames)
        {
            SubruleDebuggingConfigurationRule cr;
            if(debuggerProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Highlight,
                message, task.procEnv, out cr) == SubruleDebuggingDecision.Continue)
            {
                return;
            }

            env.Write("Highlighting: " + message); // subrule traces log
            if(sourceNames.Count > 0)
                env.Write(" with annotations");
            for(int i = 0; i < sourceNames.Count; ++i)
            {
                env.Write(" ");
                env.Write(sourceNames[i]);
            }
            env.WriteLine();

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            if(!detailedMode)
            {
                context.highlightSeq = task.lastlyEntered;
                env.Clear();
                displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                PrintDebugTracesStack(false);
            }

            task.procEnv.HighlightingUnderway = true;
            HandleHighlight(values, sourceNames);
            task.procEnv.HighlightingUnderway = false;

            QueryContinueOrTrace(true);
        }

        private void PrintDebugTracesStack(bool full)
        {
            env.WriteLineDataRendering("Subrule traces stack is:");
            for(int i = 0; i < task.computationsEnteredStack.Count; ++i)
            {
                if(!full && task.computationsEnteredStack[i].type != SubruleComputationType.Entry)
                    continue;
                env.WriteLineDataRendering(task.computationsEnteredStack[i].ToString(full));
            }
        }

        /// <summary>
        /// Asks in case of a breakpoint outside the sequence whether to
        /// - print a full (t)race stack dump or even a (f)ull state dump
        /// - continue execution (any other key)
        /// </summary>
        private void QueryContinueOrTrace(bool isBottomUpBreak)
        {
            do
            {
                PrintDebugInstructions(isBottomUpBreak);

                switch(env.LetUserChoose(queryContinueOrTraceMenu))
                {
                case 'a':
                    env.Cancel();
                    return;                               // never reached
                case 's':
                    if(isBottomUpBreak && !stepMode)
                    {
                        stepMode = true;
                        dynamicStepMode = false;
                        detailedMode = false;
                        task.curStepSequence = null;
                    }
                    return;
                case 'r':
                    if(!isBottomUpBreak && task.computationsEnteredStack.Count > 0)
                    {
                        outOfDetailedMode = true;
                        outOfDetailedModeTarget = 0;
                        detailedMode = false;
                    }
                    return;
                case 'o':
                    if(!isBottomUpBreak && TargetStackLevelForOutInDetailedMode() > 0)
                    {
                        outOfDetailedMode = true;
                        outOfDetailedModeTarget = TargetStackLevelForOutInDetailedMode();
                        detailedMode = false;
                    }
                    return;
                case 'u':
                    if(!isBottomUpBreak && TargetStackLevelForUpInDetailedMode() > 0)
                    {
                        outOfDetailedMode = true;
                        outOfDetailedModeTarget = TargetStackLevelForUpInDetailedMode();
                        detailedMode = false;
                    }
                    return;
                case 't':
                    if(task.computationsEnteredStack.Count > 0)
                    {
                        HandleStackTrace();
                        displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                        PrintDebugTracesStack(true);
                        break;
                    }
                    else
                        return;
                case 'f':
                    HandleFullState();
                    displayer.DisplaySequenceBase(task.debugSequences.Peek(), context, task.debugSequences.Count, "", "");
                    PrintDebugTracesStack(true);
                    break;
                default:
                    return;
                }
            }
            while(true);
        }

        private void PrintDebugInstructions(bool isBottomUpBreak)
        {
            if(!isBottomUpBreak && !EmbeddedSequenceWasEntered())
            {
                UserChoiceMenu anyKeyFullStateAbortMenu = new UserChoiceMenu(UserChoiceMenuNames.QueryContinueOrTraceMenu, new string[] {
                    "commandContinueAnyKey", "commandFullState", "commandAbort" });
                env.PrintInstructions(anyKeyFullStateAbortMenu, "Debugging (detailed) break, press ", "...");
                queryContinueOrTraceMenu = anyKeyFullStateAbortMenu;
            }
            else
            {
                List<string> arrayBuilder = new List<string>();

                if(!isBottomUpBreak)
                {
                    if(EmbeddedSequenceWasEntered())
                    {
                        arrayBuilder.Add("commandRunUntilEndOfDetailedDebugging");
                        if(TargetStackLevelForUpInDetailedMode() > 0)
                        {
                            arrayBuilder.Add("commandUpFromCurrentEntry");
                            if(TargetStackLevelForOutInDetailedMode() > 0)
                                arrayBuilder.Add("commandOutOfDetailedDebuggingEntry");
                        }
                    }
                }

                if(isBottomUpBreak && !stepMode)
                    arrayBuilder.Add("commandStepMode");

                if(EmbeddedSequenceWasEntered())
                {
                    arrayBuilder.Add("commandPrintSubruleStacktrace");
                }
                arrayBuilder.Add("commandFullState");
                arrayBuilder.Add("commandAbort");
                if(!isBottomUpBreak)
                    arrayBuilder.Add("commandContinueDetailedDebugging");
                else
                    arrayBuilder.Add("commandContinueDebuggingAsBefore");

                queryContinueOrTraceMenu = new UserChoiceMenu(UserChoiceMenuNames.QueryContinueOrTraceMenu, arrayBuilder.ToArray());
                if(!isBottomUpBreak)
                    env.PrintInstructions(queryContinueOrTraceMenu, "Detailed subrule debugging, press ", "");
                else
                    env.PrintInstructions(queryContinueOrTraceMenu, "Watchpoint/halt/highlight hit, press ", "");
            }
        }

        private void PrintDebugInstructionsSubruleDebugging()
        {
            List<string> arrayBuilder = new List<string>();
            arrayBuilder.Add("commandRunUntilEndOfDetailedDebugging");
            if(TargetStackLevelForUpInDetailedMode() > 0)
            {
                arrayBuilder.Add("commandUpFromCurrentEntry");
                if(TargetStackLevelForOutInDetailedMode() > 0)
                    arrayBuilder.Add("commandOutOfDetailedDebuggingEntry");
            }
            arrayBuilder.Add("commandPrintSubruleStacktrace");
            arrayBuilder.Add("commandFullState");
            arrayBuilder.Add("commandAbort");
            arrayBuilder.Add("commandContinueAnyKeyDetailedDebugging");

            UserChoiceMenu subruleDebuggingMenu = new UserChoiceMenu(UserChoiceMenuNames.SubruleDebuggingMenu, arrayBuilder.ToArray());

            env.PrintInstructions(subruleDebuggingMenu, "Detailed subrule debugging, press ", "");

            queryUserMenu = subruleDebuggingMenu;
        }

        private bool EmbeddedSequenceWasEntered()
        {
            foreach(SubruleComputation computation in task.computationsEnteredStack)
            {
                if(!computation.fakeEntry)
                    return true;
            }
            return false;
        }

        private int TargetStackLevelForUpInDetailedMode()
        {
            int posOfEntry = 0;
            for(int i = task.computationsEnteredStack.Count - 1; i >= 0; --i)
            {
                if(task.computationsEnteredStack[i].type == SubruleComputationType.Entry)
                {
                    posOfEntry = i;
                    break;
                }
            }
            return posOfEntry;
        }

        private int TargetStackLevelForOutInDetailedMode()
        {
            int posOfEntry = 0;
            for(int i = TargetStackLevelForUpInDetailedMode() - 1; i >= 0; --i)
            {
                if(task.computationsEnteredStack[i].type == SubruleComputationType.Entry)
                {
                    posOfEntry = i;
                    break;
                }
            }
            return posOfEntry;
        }

        private void DebugOnConnectionLost()
        {
            env.WriteLine("Connection to yComp lost!");
            env.Cancel();
        }

        #endregion Event Handling
    }
}
