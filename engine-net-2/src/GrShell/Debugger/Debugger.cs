/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;

using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;
using System.Text;

namespace de.unika.ipd.grGen.grShell
{
    class Debugger : IUserProxyForSequenceExecution
    {
        IGrShellImplForDebugger grShellImpl;
        ShellGraphProcessingEnvironment shellProcEnv;

        ElementRealizers realizers;
        GraphAnnotationAndChangesRecorder renderRecorder = null;
        YCompClient ycompClient = null;
        Process viewerProcess = null;

        Stack<Sequence> debugSequences = new Stack<Sequence>();
        bool stepMode = true;
        bool dynamicStepMode = false;
        bool skipMode = false;
        bool detailedMode = false;
        bool outOfDetailedMode = false;
        int outOfDetailedModeTarget = -1;
        bool recordMode = false;
        bool alwaysShow = true;
        Sequence curStepSequence = null;

        Sequence lastlyEntered = null;
        Sequence recentlyMatched = null;

        PrintSequenceContext context = null;

        int matchDepth = 0;

        bool lazyChoice = true;

        LinkedList<Sequence> loopList = new LinkedList<Sequence>();

        List<SubruleComputation> computationsEnteredStack = new List<SubruleComputation>(); // can't use stack class, too weak

        public YCompClient YCompClient { get { return ycompClient; } }
        public bool ConnectionLost { get { return ycompClient.ConnectionLost; } }

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
                        ycompClient.OnConnectionLost -= new ConnectionLostHandler(DebugOnConnectionLost);
                    }
                }
                else
                {
                    if(!notifyOnConnectionLost)
                    {
                        notifyOnConnectionLost = true;
                        ycompClient.OnConnectionLost += new ConnectionLostHandler(DebugOnConnectionLost);
                    }
                }
            }
        }

        public Debugger(IGrShellImplForDebugger grShellImpl)
            : this(grShellImpl, "Orthogonal", null)
        {
        }

        public Debugger(IGrShellImplForDebugger grShellImpl, String debugLayout)
            : this(grShellImpl, debugLayout, null)
        {
        }

        /// <summary>
        /// Initializes a new Debugger instance using the given layout and options.
        /// Any invalid options will be removed from layoutOptions.
        /// </summary>
        /// <param name="grShellImpl">An GrShellImpl instance.</param>
        /// <param name="debugLayout">The name of the layout to be used.</param>
        /// <param name="layoutOptions">An dictionary mapping layout option names to their values.
        /// It may be null, if no options are to be applied.</param>
        public Debugger(IGrShellImplForDebugger grShellImpl, String debugLayout, Dictionary<String, String> layoutOptions)
        {
            this.grShellImpl = grShellImpl;
            this.shellProcEnv = grShellImpl.CurrentShellProcEnv;
            this.realizers = grShellImpl.realizers;

            this.context = new PrintSequenceContext(grShellImpl.Workaround);

            this.renderRecorder = new GraphAnnotationAndChangesRecorder();

            int ycompPort = GetFreeTCPPort();
            if(ycompPort < 0)
            {
                throw new Exception("Didn't find a free TCP port in the range 4242-4251!");
            }
            try
            {
                viewerProcess = Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    + Path.DirectorySeparatorChar + "ycomp", "--nomaximize -p " + ycompPort);
            }
            catch(Exception e)
            {
                throw new Exception("Unable to start yComp: " + e.ToString());
            }

            try
            {
                ycompClient = new YCompClient(shellProcEnv.ProcEnv.NamedGraph, debugLayout, 20000, ycompPort, 
                    shellProcEnv.DumpInfo, realizers);
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to connect to yComp at port " + ycompPort + ": " + ex.Message);
            }

            shellProcEnv.ProcEnv.NamedGraph.ReuseOptimization = false;
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
                            layoutOptions.Remove(illegalOption);
                    }
                }

                if(!ycompClient.dumpInfo.IsExcludedGraph())
                    UploadGraph(shellProcEnv.ProcEnv.NamedGraph);
            }
            catch(OperationCanceledException)
            {
                throw new Exception("Connection to yComp lost");
            }

            NotifyOnConnectionLost = false;
            RegisterLibGrEvents(shellProcEnv.ProcEnv.NamedGraph);
        }

        /// <summary>
        /// Uploads the graph to YComp, updates the display and makes a synchonisation
        /// </summary>
        private void UploadGraph(INamedGraph graph)
        {
            foreach(INode node in graph.Nodes)
                ycompClient.AddNode(node);
            foreach(IEdge edge in graph.Edges)
                ycompClient.AddEdge(edge);
            ycompClient.UpdateDisplay();
            ycompClient.Sync();
        }

        /// <summary>
        /// Searches for a free TCP port in the range 4242-4251
        /// </summary>
        /// <returns>A free TCP port or -1, if they are all occupied</returns>
        private int GetFreeTCPPort()
        {
            for(int i = 4242; i < 4252; i++)
            {
                try
                {
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, i);
                    // Check whether the current socket is already open by connecting to it
                    using(Socket socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                    {
                        try
                        {
                            socket.Connect(endpoint);
                            socket.Disconnect(false);
                            // Someone is already listening at the current port, so try another one
                            continue;
                        }
                        catch(SocketException)
                        {
                        } // Nobody there? Good...
                    }

                    // Unable to connect, so try to bind the current port.
                    // Trying to bind directly (without the connect-check before), does not
                    // work on Windows Vista even with ExclusiveAddressUse set to true (which does not work on Mono).
                    // It will bind to already used ports without any notice.
                    using(Socket socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                        socket.Bind(endpoint);
                }
                catch(SocketException)
                {
                    continue;
                }
                return i;
            }
            return -1;
        }

        /// <summary>
        /// Closes the debugger.
        /// </summary>
        public void Close()
        {
            if(ycompClient == null)
                throw new InvalidOperationException("The debugger has already been closed!");

            UnregisterLibGrEvents(shellProcEnv.ProcEnv.NamedGraph);

            shellProcEnv.ProcEnv.NamedGraph.ReuseOptimization = true;
            ycompClient.Close();
            ycompClient = null;
            viewerProcess.Close();
            viewerProcess = null;
        }

        public void InitNewRewriteSequence(Sequence seq, bool withStepMode)
        {
            debugSequences.Clear();
            debugSequences.Push(seq);
            curStepSequence = null;
            stepMode = withStepMode;
            recordMode = false;
            alwaysShow = false;
            detailedMode = false;
            outOfDetailedMode = false;
            outOfDetailedModeTarget = -1;
            dynamicStepMode = false;
            skipMode = false;
            lastlyEntered = null;
            recentlyMatched = null;
            context.Init(grShellImpl.Workaround);
        }

        public void AbortRewriteSequence()
        {
            stepMode = false;
            detailedMode = false;
            loopList.Clear();
        }

        public void FinishRewriteSequence()
        {
            alwaysShow = true;
            try
            {
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
            }
            catch(OperationCanceledException)
            {
            }
        }

        public ShellGraphProcessingEnvironment ShellProcEnv
        {
            get { return shellProcEnv; }
            set
            {
                // switch to new graph in YComp
                UnregisterLibGrEvents(shellProcEnv.ProcEnv.NamedGraph);
                ycompClient.ClearGraph();
                shellProcEnv = value;
                ycompClient.Graph = shellProcEnv.ProcEnv.NamedGraph;
                if(!ycompClient.dumpInfo.IsExcludedGraph())
                    UploadGraph(shellProcEnv.ProcEnv.NamedGraph);
                RegisterLibGrEvents(shellProcEnv.ProcEnv.NamedGraph);

                // TODO: reset any state when inside a rule debugging session
            }
        }

        public void ForceLayout()
        {
            ycompClient.ForceLayout();
        }

        public void UpdateYCompDisplay()
        {
            ycompClient.UpdateDisplay();
        }

        public void SetLayout(String layout)
        {
            ycompClient.SetLayout(layout);
        }

        public void GetLayoutOptions()
        {
            String str = ycompClient.GetLayoutOptions();
            Console.WriteLine("Available layout options and their current values:\n\n" + str);
        }

        /// <summary>
        /// Sets a layout option for the current layout in yComp.
        /// </summary>
        /// <param name="optionName">The name of the option.</param>
        /// <param name="optionValue">The new value for the option.</param>
        /// <returns>True, iff yComp did not report an error.</returns>
        public bool SetLayoutOption(String optionName, String optionValue)
        {
            String errorMessage = ycompClient.SetLayoutOption(optionName, optionValue);
            if(errorMessage != null)
                Console.WriteLine(errorMessage);
            return errorMessage == null;
        }

        /// <summary>
        /// Searches in the given sequence seq for the parent sequence of the sequence childseq.
        /// </summary>
        /// <returns>The parent sequence of childseq or null, if no parent has been found.</returns>
        private Sequence GetParentSequence(Sequence childseq, Sequence seq)
        {
            Sequence res = null;
            foreach(Sequence child in seq.Children)
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
        private bool QueryUser(Sequence seq)
        {
            while(true)
            {
                ConsoleKeyInfo key = grShellImpl.ReadKeyWithCancel();
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
                    curStepSequence = GetParentSequence(seq, debugSequences.Peek());
                    return false;
                case 'o':
                    stepMode = false;
                    dynamicStepMode = false;
                    detailedMode = false;
                    if(loopList.Count == 0)
                        curStepSequence = null;                 // execute until the end
                    else
                        curStepSequence = loopList.First.Value; // execute until current loop has been exited
                    return false;
                case 'r':
                    stepMode = false;
                    dynamicStepMode = false;
                    detailedMode = false;
                    curStepSequence = null;                     // execute until the end
                    return false;
                case 'b':
                    {
                        BreakpointAndChoicepointEditor breakpointEditor = new BreakpointAndChoicepointEditor(grShellImpl, debugSequences);
                        breakpointEditor.HandleToggleBreakpoints();
                        context.highlightSeq = seq;
                        context.success = false;
                        SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                        Console.WriteLine();
                        break;
                    }
                case 'w':
                    {
                        WatchpointEditor watchpointEditor = new WatchpointEditor(shellProcEnv, grShellImpl);
                        watchpointEditor.HandleWatchpoints();
                        break;
                    }
                case 'c':
                    {
                        BreakpointAndChoicepointEditor choicepointEditor = new BreakpointAndChoicepointEditor(grShellImpl, debugSequences);
                        choicepointEditor.HandleToggleChoicepoints();
                        context.highlightSeq = seq;
                        context.success = false;
                        SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                        Console.WriteLine();
                        break;
                    }
                case 'l':
                    HandleToggleLazyChoice();
                    break;
                case 'a':
                    grShellImpl.Cancel();
                    return false;                               // never reached
                case 'n':
                    stepMode = false;
                    dynamicStepMode = true;
                    detailedMode = false;
                    return false;
                case 'v':
                    HandleShowVariable(seq);
                    SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
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
                    SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
                    break;
                case 'f':
                    HandleFullState();
                    SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("Illegal command (Key = " + key.Key
                        + ")! Only (n)ext match, (d)etailed step, (s)tep, step (u)p, step (o)ut, (r)un, toggle (b)reakpoints, toggle (c)hoicepoints, toggle (l)azy choice, (w)atchpoints, show (v)ariables, print stack(t)race, (f)ull state, (h)ighlight, dum(p) graph, as (g)raph, and (a)bort allowed!");
                    break;
                }
            }
        }


        #region Methods for directly handling user commands

        private void HandleToggleLazyChoice()
        {
            if(lazyChoice)
            {
                Console.WriteLine("Lazy choice disabled, always requesting user choice on $%[r] / $%v[r] / $%{...}.");
                lazyChoice = false;
            }
            else
            {
                Console.WriteLine("Lazy choice enabled, only prompting user on $%[r] / $%v[r] / $%{...} if more matches available than rewrites requested.");
                lazyChoice = true;
            }
        }

        private void HandleShowVariable(Sequence seq)
        {
            PrintVariables(null, null);
            PrintVariables(debugSequences.Peek(), seq);
            PrintVisited();
        }

        private void HandleDump()
        {
            string filename = grShellImpl.ShowGraphWith("ycomp", "", false);
            Console.WriteLine("Showing dumped graph " + filename + " with ycomp");

            String undoLog = shellProcEnv.ProcEnv.TransactionManager.ToString();
            if(undoLog.Length > 0)
            {
                filename = "undo.log";
                StreamWriter sw = new StreamWriter(filename, false);
                sw.Write(undoLog);
                sw.Close();
                Console.WriteLine("Written undo log to " + filename);
            }
        }

        private void HandleAsGraph(Sequence seq)
        {
            VariableOrAttributeAccessParserAndValueFetcher parserFetcher = new VariableOrAttributeAccessParserAndValueFetcher(
                grShellImpl, shellProcEnv, debugSequences);
            object toBeShownAsGraph;
            AttributeType attrType;
            bool abort = parserFetcher.FetchObjectToBeShownAsGraph(seq, out toBeShownAsGraph, out attrType);
            if(abort)
            {
                Console.WriteLine("Back from as-graph display to debugging.");
                return;
            }

            INamedGraph graph = shellProcEnv.ProcEnv.Graph.Model.AsGraph(toBeShownAsGraph, attrType, shellProcEnv.ProcEnv.Graph);
            if(graph == null)
            {
                if(toBeShownAsGraph is INamedGraph)
                    graph = toBeShownAsGraph as INamedGraph;
                else if(toBeShownAsGraph is IGraph)
                {
                    Console.WriteLine("Clone and assign names to unnamed graph for display.");
                    graph = (toBeShownAsGraph as IGraph).CloneAndAssignNames();
                }
            }
            if(graph == null)
            {
                Console.WriteLine("Was not able to get a named graph for the object specified.");
                Console.WriteLine("Back from as-graph display to debugging.");
                return;
            }
            Console.WriteLine("Showing graph for the object specified...");
            ycompClient.ClearGraph();
            ycompClient.Graph = graph;
            UploadGraph(graph);

            Console.WriteLine("...press any key to continue...");
            grShellImpl.ReadKeyWithCancel();

            Console.WriteLine("...return to normal graph.");
            ycompClient.ClearGraph();
            ycompClient.Graph = shellProcEnv.ProcEnv.NamedGraph;
            if(!ycompClient.dumpInfo.IsExcludedGraph())
                UploadGraph(shellProcEnv.ProcEnv.NamedGraph);

            Console.WriteLine("Back from as-graph display to debugging.");
        }

        private void HandleUserHighlight(Sequence seq)
        {
            Console.Write("Enter name of variable or id of visited flag to highlight (multiple values may be given comma-separated; just enter for abort): ");
            String str = Console.ReadLine();
            Highlighter highlighter = new Highlighter(grShellImpl, shellProcEnv, realizers, renderRecorder, ycompClient, debugSequences);
            List<object> values;
            List<string> annotations;
            highlighter.ComputeHighlight(seq, str, out values, out annotations);
            highlighter.DoHighlight(values, annotations);
        }

        private void HandleHighlight(List<object> originalValues, List<string> sourceNames)
        {
            Highlighter highlighter = new Highlighter(grShellImpl, shellProcEnv, realizers, renderRecorder, ycompClient, debugSequences);
            highlighter.DoHighlight(originalValues, sourceNames);
        }

        private void HandleStackTrace()
        {
            Console.WriteLine("Current sequence call stack is:");
            PrintSequenceContext contextTrace = new PrintSequenceContext(grShellImpl.Workaround);
            Sequence[] callStack = debugSequences.ToArray();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                contextTrace.highlightSeq = callStack[i].GetCurrentlyExecutedSequence();
                SequencePrinter.PrintSequence(callStack[i], contextTrace, callStack.Length - i);
                Console.WriteLine();
            }
            Console.WriteLine("continuing execution with:");
        }

        private void HandleFullState()
        {
            Console.WriteLine("Current execution state is:");
            PrintVariables(null, null);
            PrintSequenceContext contextTrace = new PrintSequenceContext(grShellImpl.Workaround);
            Sequence[] callStack = debugSequences.ToArray();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                Sequence currSeq = callStack[i].GetCurrentlyExecutedSequence();
                contextTrace.highlightSeq = currSeq;
                SequencePrinter.PrintSequence(callStack[i], contextTrace, callStack.Length - i);
                Console.WriteLine();
                PrintVariables(callStack[i], currSeq != null ? currSeq : callStack[i]);
            }
            PrintVisited();
            Console.WriteLine("continuing execution with:");
        }

        #endregion Methods for directly handling user commands

        #region Print variables

        private void PrintVariables(Sequence seqStart, Sequence seq)
        {
            if(seq != null)
            {
                Console.WriteLine("Available local variables:");
                Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
                List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
                seqStart.GetLocalVariables(seqVars, containerConstructors, seq);
                foreach(SequenceVariable var in seqVars.Keys)
                {
                    string type;
                    string content;
                    if(var.Value is IDictionary)
                        EmitHelper.ToString((IDictionary)var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    else if(var.Value is IList)
                        EmitHelper.ToString((IList)var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    else if(var.Value is IDeque)
                        EmitHelper.ToString((IDeque)var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    else
                        EmitHelper.ToString(var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    Console.WriteLine("  " + var.Name + " = " + content + " : " + type);
                }
            }
            else
            {
                Console.WriteLine("Available global (non null) variables:");
                foreach(Variable var in shellProcEnv.ProcEnv.Variables)
                {
                    string type;
                    string content;
                    if(var.Value is IDictionary)
                        EmitHelper.ToString((IDictionary)var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    else if(var.Value is IList)
                        EmitHelper.ToString((IList)var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    else if(var.Value is IDeque)
                        EmitHelper.ToString((IDeque)var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    else
                        EmitHelper.ToString(var.Value, out type, out content, null, shellProcEnv.ProcEnv.NamedGraph);
                    Console.WriteLine("  " + var.Name + " = " + content + " : " + type);
                }
            }
        }

        private void PrintVisited()
        {
            List<int> allocatedVisitedFlags = shellProcEnv.ProcEnv.NamedGraph.GetAllocatedVisitedFlags();
            Console.Write("Allocated visited flags are: ");
            bool first = true;
            foreach(int allocatedVisitedFlag in allocatedVisitedFlags)
            {
                if(!first)
                    Console.Write(", ");
                Console.Write(allocatedVisitedFlag);
                first = false;
            }
            Console.WriteLine(".");
        }

        #endregion Print variables


        #region Possible user choices during sequence execution

        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen directions is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        public int ChooseDirection(int direction, Sequence seq)
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            return UserChoiceMenu.ChooseDirection(context, grShellImpl, direction, seq);
        }

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        public int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            UserChoiceMenu.ChooseSequencePrintHeader(context, seqToExecute);

            do
            {
                context.highlightSeq = sequences[seqToExecute];
                context.choice = true;
                context.sequences = sequences;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                context.choice = false;
                context.sequences = null;

                bool commit = UserChoiceMenu.ChooseSequence(grShellImpl, ref seqToExecute, sequences, seq);
                if(commit)
                    return seqToExecute;
            } while(true);
        }

        /// <summary>
        /// returns the maybe user altered point within the interval series, denoting the sequence to execute next
        /// the randomly chosen point is supplied; the sequence with the intervals and their corresponding sequences is supplied
        /// </summary>
        public double ChoosePoint(double pointToExecute, SequenceWeightedOne seq)
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            UserChoiceMenu.ChoosePointPrintHeader(context, pointToExecute);

            do
            {
                context.highlightSeq = seq.Sequences[seq.GetSequenceFromPoint(pointToExecute)];
                context.choice = true;
                context.sequences = seq.Sequences;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                context.choice = false;
                context.sequences = null;

                bool commit = UserChoiceMenu.ChoosePoint(grShellImpl, ref pointToExecute, seq);
                if(commit)
                    break;
            } while(true);

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
                context.workaround.PrintHighlighted("Skipping choicepoint ", HighlightingMode.Choicepoint);
                Console.WriteLine("as no choice needed (use the (l) command to toggle this behaviour).");
                return totalMatchToExecute;
            }

            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            UserChoiceMenu.ChooseMatchSomeFromSetPrintHeader(context, totalMatchToExecute);

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, ycompClient);

            do
            {
                int rule; int match;
                seq.FromTotalMatch(totalMatchToExecute, out rule, out match);
                matchMarkerAndAnnotator.Mark(rule, match, seq);
                ycompClient.UpdateDisplay();
                ycompClient.Sync();

                context.highlightSeq = seq.Sequences[rule];
                context.choice = true;
                context.sequences = seq.Sequences;
                context.matches = seq.Matches;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                context.choice = false;
                context.sequences = null;
                context.matches = null;

                bool commit = UserChoiceMenu.ChooseMatch(grShellImpl, ref totalMatchToExecute, seq);
                matchMarkerAndAnnotator.Unmark(rule, match, seq);
                if(commit)
                    break;
            } while(true);

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
            SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            if(matches.Count <= 1 + numFurtherMatchesToApply && lazyChoice)
            {
                context.workaround.PrintHighlighted("Skipping choicepoint ", HighlightingMode.Choicepoint);
                Console.WriteLine("as no choice needed (use the (l) command to toggle this behaviour).");
                return matchToApply;
            }

            UserChoiceMenu.ChooseMatchPrintHeader(context, numFurtherMatchesToApply);

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, ycompClient);

            if(detailedMode)
            {
                matchMarkerAndAnnotator.MarkMatches(matches, null, null);
                matchMarkerAndAnnotator.AnnotateMatches(matches, false);
            }
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            int newMatchToRewrite = matchToApply;
            do
            {
                matchMarkerAndAnnotator.MarkMatch(matches.GetMatch(matchToApply), null, null);
                matchMarkerAndAnnotator.AnnotateMatch(matches.GetMatch(matchToApply), false);
                matchToApply = newMatchToRewrite;
                matchMarkerAndAnnotator.MarkMatch(matches.GetMatch(matchToApply), realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                matchMarkerAndAnnotator.AnnotateMatch(matches.GetMatch(matchToApply), true);
                ycompClient.UpdateDisplay();
                ycompClient.Sync();

                Console.WriteLine("Showing match " + matchToApply + " (of " + matches.Count + " matches available)");

                bool commit = UserChoiceMenu.ChooseMatch(grShellImpl, matchToApply, matches, numFurtherMatchesToApply, seq, out newMatchToRewrite);
                if(commit)
                {
                    matchMarkerAndAnnotator.MarkMatch(matches.GetMatch(matchToApply), null, null);
                    matchMarkerAndAnnotator.AnnotateMatch(matches.GetMatch(matchToApply), false);
                    ycompClient.UpdateDisplay();
                    ycompClient.Sync();
                    return newMatchToRewrite;
                }
            } while(true);
        }

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        public int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            return UserChoiceMenu.ChooseRandomNumber(randomNumber, upperBound, seq);
        }

        /// <summary>
        /// returns the maybe user altered random number in the range 0.0 - 1.0 exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        public double ChooseRandomNumber(double randomNumber, Sequence seq)
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            return UserChoiceMenu.ChooseRandomNumber(randomNumber, seq);
        }

        /// <summary>
        /// returns the id/persistent name of a node/edge chosen by the user in yComp
        /// </summary>
        public string ChooseGraphElement()
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            ycompClient.WaitForElement(true);

            // Allow to abort with ESC
            while(true)
            {
                if(Console.KeyAvailable && grShellImpl.Workaround.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Aborted!");
                    ycompClient.WaitForElement(false);
                    return null;
                }
                if(ycompClient.CommandAvailable)
                    break;
                Thread.Sleep(100);
            }

            String cmd = ycompClient.ReadCommand();
            if(cmd.Length < 7 || !cmd.StartsWith("send "))
            {
                Console.WriteLine("Unexpected yComp command: \"" + cmd + "\"!");
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
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            context.highlightSeq = seq;
            context.choice = true;
            SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            return UserChoiceMenu.ChooseValue(grShellImpl, type, seq);
        }

        #endregion Possible user choices during sequence execution


        #region Partial graph adding on matches for excluded graph debugging

        private void AddNeededGraphElements(IMatch match)
        {
            foreach(INode node in match.Nodes)
            {
                ycompClient.AddNodeEvenIfGraphExcluded(node);
            }
            foreach(IEdge edge in match.Edges)
            {
                ycompClient.AddEdgeEvenIfGraphExcluded(edge);
            }
            AddNeededGraphElements(match.EmbeddedGraphs);
            foreach(IMatches iteratedsMatches in match.Iterateds)
                AddNeededGraphElements(iteratedsMatches);
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

        #endregion Partial graph adding on matches for excluded graph debugging


        #region Event Handling

        private void DebugNodeAdded(INode node)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.New, 
                node, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, node);

            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;

            ycompClient.AddNode(node);
            if(recordMode)
            {
                String nodeName = renderRecorder.AddedNode(node);
                ycompClient.AnnotateElement(node, nodeName);
            }
            else if(alwaysShow)
                ycompClient.UpdateDisplay();
        }

        private void DebugEdgeAdded(IEdge edge)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.New,
                edge, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, edge);

            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            ycompClient.AddEdge(edge);
            if(recordMode)
            {
                String edgeName = renderRecorder.AddedEdge(edge);
                ycompClient.AnnotateElement(edge, edgeName);
            }
            else if(alwaysShow)
                ycompClient.UpdateDisplay();
        }

        private void DebugDeletingNode(INode node)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Delete, 
                node, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, node);

            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            if(!recordMode)
            {
                ycompClient.DeleteNode(node);
                if(alwaysShow)
                    ycompClient.UpdateDisplay();
            }
            else
            {
                renderRecorder.RemoveNodeAnnotation(node);
                ycompClient.ChangeNode(node, realizers.DeletedNodeRealizer);

                String name = ycompClient.Graph.GetElementName(node);
                ycompClient.RenameNode(name, "zombie_" + name);
                renderRecorder.DeletedNode("zombie_" + name);
            }
        }

        private void DebugDeletingEdge(IEdge edge)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Delete, 
                edge, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, edge);

            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            if(!recordMode)
            {
                ycompClient.DeleteEdge(edge);
                if(alwaysShow)
                    ycompClient.UpdateDisplay();
            }
            else
            {
                renderRecorder.RemoveEdgeAnnotation(edge);
                ycompClient.ChangeEdge(edge, realizers.DeletedEdgeRealizer);

                String name = ycompClient.Graph.GetElementName(edge);
                ycompClient.RenameEdge(name, "zombie_" + name);
                renderRecorder.DeletedEdge("zombie_" + name);
            }
        }

        private void DebugClearingGraph()
        {
            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            ycompClient.ClearGraph();
        }

        private void DebugChangedNodeAttribute(INode node, AttributeType attrType)
        {
            if(!ycompClient.dumpInfo.IsExcludedGraph() || recordMode)
                ycompClient.ChangeNodeAttribute(node, attrType);
            
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.SetAttributes,
                node, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, node, attrType.Name);
        }

        private void DebugChangedEdgeAttribute(IEdge edge, AttributeType attrType)
        {
            if(!ycompClient.dumpInfo.IsExcludedGraph() || recordMode)
                ycompClient.ChangeEdgeAttribute(edge, attrType);
            
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.SetAttributes,
                edge, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, edge, attrType.Name);
        }

        private void DebugRetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Retype, 
                oldElem, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, oldElem);

            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            ycompClient.RetypingElement(oldElem, newElem);
            if(!recordMode)
                return;

            if(oldElem is INode)
            {
                INode oldNode = (INode) oldElem;
                INode newNode = (INode) newElem;
                String name;
                if(renderRecorder.WasNodeAnnotationReplaced(oldNode, newNode, out name))
                    ycompClient.AnnotateElement(newElem, name);
                ycompClient.ChangeNode(newNode, realizers.RetypedNodeRealizer);
                renderRecorder.RetypedNode(newNode);
            }
            else
            {
                IEdge oldEdge = (IEdge) oldElem;
                IEdge newEdge = (IEdge) newElem;
                String name;
                if(renderRecorder.WasEdgeAnnotationReplaced(oldEdge, newEdge, out name))
                    ycompClient.AnnotateElement(newElem, name);
                ycompClient.ChangeEdge(newEdge, realizers.RetypedEdgeRealizer);
                renderRecorder.RetypedEdge(newEdge);
            }
        }

        private void DebugSettingAddedNodeNames(string[] namesOfNodesAdded)
        {
            renderRecorder.SetAddedNodeNames(namesOfNodesAdded);
        }

        private void DebugSettingAddedEdgeNames(string[] namesOfEdgesAdded)
        {
            renderRecorder.SetAddedEdgeNames(namesOfEdgesAdded);
        }

        private void DebugMatched(IMatches matches, IMatch match, bool special)
        {
            if(matches.Count == 0) // happens e.g. from compiled sequences firing the event always, but the Finishing only comes in case of Count!=0
                return;

            // integrate matched actions into subrule traces stack
            computationsEnteredStack.Add(new SubruleComputation(matches.Producer.Name));

            SubruleDebuggingConfigurationRule cr;
            SubruleDebuggingDecision d = shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Match, 
                matches, shellProcEnv.ProcEnv, out cr);
            if(d == SubruleDebuggingDecision.Break)
                InternalHalt(cr, matches);
            else if(d == SubruleDebuggingDecision.Continue)
            {
                recentlyMatched = lastlyEntered;
                if(!detailedMode)
                    return;
                if(recordMode)
                {
                    DebugFinished(null, false);
                    matchDepth++;
                    renderRecorder.RemoveAllAnnotations();
                }
                return;
            }

            if(dynamicStepMode && !skipMode)
            {
                skipMode = true;
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
                context.highlightSeq = lastlyEntered;
                context.success = true;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();

                if(!QueryUser(lastlyEntered))
                {
                    recentlyMatched = lastlyEntered;
                    return;
                }
            }

            recentlyMatched = lastlyEntered;

            if(!detailedMode)
                return;

            if(recordMode)
            {
                DebugFinished(null, false);
                matchDepth++;
                if(outOfDetailedMode)
                {
                    renderRecorder.RemoveAllAnnotations();
                    return;
                }
            }

            if(matchDepth++ > 0 || computationsEnteredStack.Count > 0)
                Console.WriteLine("Matched " + matches.Producer.Name);

            renderRecorder.RemoveAllAnnotations();
            renderRecorder.SetCurrentRule(matches.Producer.RulePattern);

            if(ycompClient.dumpInfo.IsExcludedGraph())
            {
                if(!recordMode)
                {
                    ycompClient.ClearGraph();
                }

                // add all elements from match to graph and excludedGraphElementsIncluded
                if(match != null)
                    AddNeededGraphElements(match);
                else
                    AddNeededGraphElements(matches);

                ycompClient.AddNeighboursAndParentsOfNeededGraphElements();
            }

            MatchMarkerAndAnnotator matchMarkerAndAnnotator = new MatchMarkerAndAnnotator(realizers, renderRecorder, ycompClient);

            if(match!=null)
                matchMarkerAndAnnotator.MarkMatch(match, realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
            else
                matchMarkerAndAnnotator.MarkMatches(matches, realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
            if(match!=null)
                matchMarkerAndAnnotator.AnnotateMatch(match, true);
            else
                matchMarkerAndAnnotator.AnnotateMatches(matches, true);

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to apply rewrite...");
            grShellImpl.ReadKeyWithCancel();

            if(match!=null)
                matchMarkerAndAnnotator.MarkMatch(match, null, null);
            else
                matchMarkerAndAnnotator.MarkMatches(matches, null, null);

            recordMode = true;
            ycompClient.NodeRealizerOverride = realizers.NewNodeRealizer;
            ycompClient.EdgeRealizerOverride = realizers.NewEdgeRealizer;
            renderRecorder.ResetAddedNames();
        }

        private void DebugNextMatch()
        {
            renderRecorder.ResetAddedNames();
        }

        private void DebugFinished(IMatches matches, bool special)
        {
            // integrate matched actions into subrule traces stack
            if(matches != null)
                RemoveUpToEntryForExit(matches.Producer.Name);

            if(outOfDetailedMode && (computationsEnteredStack.Count <= outOfDetailedModeTarget || computationsEnteredStack.Count==0))
            {
                detailedMode = true;
                outOfDetailedMode = false;
                outOfDetailedModeTarget = -1;
                return;
            }

            if(!detailedMode)
                return;

            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            QueryContinueOrTrace(false);

            renderRecorder.ApplyChanges(ycompClient);

            ycompClient.NodeRealizerOverride = null;
            ycompClient.EdgeRealizerOverride = null;

            renderRecorder.ResetAllChangedElements();
            recordMode = false;
            matchDepth--;
        }

        private void DebugEnteringSequence(Sequence seq)
        {
            // root node of sequence entered and interactive debugging activated
            if(stepMode && lastlyEntered == null)
            {
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                PrintDebugInstructionsOnEntering();
                QueryUser(seq);
            }

            lastlyEntered = seq;
            recentlyMatched = null;

            // Entering a loop?
            if(IsLoop(seq))
            {
                loopList.AddFirst(seq);
            }

            // Entering a subsequence called?
            if(seq.SequenceType == SequenceType.SequenceDefinitionInterpreted)
            {
                loopList.AddFirst(seq);
                debugSequences.Push(seq);
            }

            // Breakpoint reached?
            bool breakpointReached = false;
            if((seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall
                || seq.SequenceType == SequenceType.BooleanComputation || seq.SequenceType == SequenceType.SequenceCall)
                    && ((SequenceSpecial)seq).Special)
            {
                stepMode = true;
                breakpointReached = true;
            }

            if(!stepMode)
            {
                return;
            }

            if(seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall
                || seq.SequenceType == SequenceType.RuleCountAllCall || seq.SequenceType == SequenceType.SequenceCall
                || breakpointReached)
            {
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
                context.highlightSeq = seq;
                context.success = false;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                QueryUser(seq);
                return;
            }
        }

        private void DebugExitingSequence(Sequence seq)
        {
            skipMode = false;

            if(seq == curStepSequence)
            {
                stepMode = true;
            }

            if(IsLoop(seq))
            {
                loopList.RemoveFirst();
            }

            if(seq.SequenceType == SequenceType.SequenceDefinitionInterpreted)
            {
                debugSequences.Pop();
                loopList.RemoveFirst();
            }

            if(debugSequences.Count == 1 && seq == debugSequences.Peek())
            {
                context.workaround.PrintHighlighted("State at end of sequence ", HighlightingMode.SequenceStart);
                context.highlightSeq = null;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                context.workaround.PrintHighlighted("< leaving", HighlightingMode.SequenceStart);
                Console.WriteLine();
            }
        }

        private void PrintDebugInstructionsOnEntering()
        {
            context.workaround.PrintHighlighted("Debug started", HighlightingMode.SequenceStart);
            Console.Write(" -- available commands are: (n)ext match, (d)etailed step, (s)tep, step (u)p, step (o)ut of loop, (r)un, ");
            Console.Write("toggle (b)reakpoints, toggle (c)hoicepoints, toggle (l)azy choice, (w)atchpoints, ");
            Console.Write("show (v)ariables, print stack(t)race, (f)ull state, (h)ighlight, dum(p) graph, as (g)raph, ");
            Console.WriteLine("and (a)bort (plus Ctrl+C for forced abort).");
        }

        private static bool IsLoop(Sequence seq)
        {
            switch(seq.SequenceType)
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
        private void DebugEndOfIteration(bool continueLoop, Sequence seq)
        {
            if(stepMode || dynamicStepMode)
            {
                if(seq is SequenceBacktrack)
                {
                    SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                    String text;
                    if(seqBack.Seq.ExecutionState == SequenceExecutionState.Success)
                        text = "Success ";
                    else
                        if(continueLoop)
                            text = "Backtracking ";
                        else
                            text = "Backtracking possibilities exhausted, fail ";
                    context.workaround.PrintHighlighted(text, HighlightingMode.SequenceStart);
                    context.highlightSeq = seq;
                    SequencePrinter.PrintSequence(seq, context, debugSequences.Count);
                    if(!continueLoop)
                        context.workaround.PrintHighlighted("< leaving backtracking brackets", HighlightingMode.SequenceStart);
                }
                else if(seq is SequenceDefinition)
                {
                    SequenceDefinition seqDef = (SequenceDefinition)seq;
                    context.workaround.PrintHighlighted("State at end of sequence call ", HighlightingMode.SequenceStart);
                    context.highlightSeq = seq;
                    SequencePrinter.PrintSequence(seq, context, debugSequences.Count);
                    context.workaround.PrintHighlighted("< leaving", HighlightingMode.SequenceStart);
                }
                else
                {
                    context.workaround.PrintHighlighted("State at end of iteration step ", HighlightingMode.SequenceStart);
                    context.highlightSeq = seq;
                    SequencePrinter.PrintSequence(seq, context, debugSequences.Count);
                    if(!continueLoop)
                        context.workaround.PrintHighlighted("< leaving loop", HighlightingMode.SequenceStart);
                }
                Console.WriteLine(" (updating, please wait...)");
            }
        }

        /// <summary>
        /// informs debugger about the change of the graph, so it can switch yComp display to the new one
        /// called just before switch with the new one, the old one is the current graph
        /// </summary>
        private void DebugSwitchToGraph(IGraph newGraph)
        {
            // potential future extension: display the stack of graphs instead of only the topmost one
            // with the one at the forefront being the top of the stack; would save clearing and uploading
            UnregisterLibGrEvents(shellProcEnv.ProcEnv.NamedGraph);
            context.workaround.PrintHighlighted("Entering graph...\n", HighlightingMode.SequenceStart);
            ycompClient.ClearGraph();
            ycompClient.Graph = (INamedGraph)newGraph;
            if(!ycompClient.dumpInfo.IsExcludedGraph())
                UploadGraph((INamedGraph)newGraph);
            RegisterLibGrEvents((INamedGraph)newGraph);
        }

        /// <summary>
        /// informs debugger about the change of the graph, so it can switch yComp display to the new one
        /// called just after the switch with the old one, the new one is the current graph
        /// </summary>
        private void DebugReturnedFromGraph(IGraph oldGraph)
        {
            UnregisterLibGrEvents((INamedGraph)oldGraph);
            context.workaround.PrintHighlighted("...leaving graph\n", HighlightingMode.SequenceStart);
            ycompClient.ClearGraph();
            ycompClient.Graph = shellProcEnv.ProcEnv.NamedGraph;
            if(!ycompClient.dumpInfo.IsExcludedGraph())
                UploadGraph(shellProcEnv.ProcEnv.NamedGraph);
            RegisterLibGrEvents(shellProcEnv.ProcEnv.NamedGraph);
        }

        private void DebugEnter(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Add, 
                message, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, message, values);

            SubruleComputation entry = new SubruleComputation(shellProcEnv.ProcEnv.NamedGraph, 
                SubruleComputationType.Entry, message, values);
            computationsEnteredStack.Add(entry);
            if(detailedMode)
                Console.WriteLine(entry.ToString(false));
        }

        private void DebugExit(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Rem, 
                message, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, message, values);

            RemoveUpToEntryForExit(message);
            if(detailedMode)
            {
                SubruleComputation exit = new SubruleComputation(shellProcEnv.ProcEnv.NamedGraph,
                    SubruleComputationType.Exit, message, values);
                Console.WriteLine(exit.ToString(false));
            }
            if(outOfDetailedMode && (computationsEnteredStack.Count <= outOfDetailedModeTarget || computationsEnteredStack.Count == 0))
            {
                detailedMode = true;
                outOfDetailedMode = false;
                outOfDetailedModeTarget = -1;
            }
        }

        private void RemoveUpToEntryForExit(string message)
        {
            int posOfEntry = 0;
            for(int i = computationsEnteredStack.Count - 1; i >= 0; --i)
            {
                if(computationsEnteredStack[i].type == SubruleComputationType.Entry)
                {
                    posOfEntry = i;
                    break;
                }
            }
            if(computationsEnteredStack[posOfEntry].message != message)
            {
                Console.Error.WriteLine("Trying to remove from debug trace stack the entry for the exit message/computation: " + message);
                Console.Error.WriteLine("But found as enclosing message/computation entry: " + computationsEnteredStack[posOfEntry].message);
                throw new Exception("Mismatch of debug enter / exit, mismatch in Debug::add(message,...) / Debug::rem(message,...)");
            }
            computationsEnteredStack.RemoveRange(posOfEntry, computationsEnteredStack.Count - posOfEntry);
        }

        private void DebugEmit(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Emit, 
                message, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, message, values);

            SubruleComputation emit = new SubruleComputation(shellProcEnv.ProcEnv.NamedGraph,
                SubruleComputationType.Emit, message, values);
            computationsEnteredStack.Add(emit);
            if(detailedMode)
                Console.WriteLine(emit.ToString(false));
        }

        private void DebugHalt(string message, params object[] values)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Halt, 
                message, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Continue)
                return;

            Console.Write("Halting: " + message);
            for(int i = 0; i < values.Length; ++i)
            {
                Console.Write(" ");
                Console.Write(EmitHelper.ToStringAutomatic(values[i], shellProcEnv.ProcEnv.NamedGraph));
            }
            Console.WriteLine();

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            if(!detailedMode)
            {
                context.highlightSeq = lastlyEntered;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                PrintDebugTracesStack(false);
            }

            QueryContinueOrTrace(true);
        }

        private void InternalHalt(SubruleDebuggingConfigurationRule cr, object data, params object[] additionalData)
        {
            context.workaround.PrintHighlighted("Break ", HighlightingMode.Breakpoint);
            Console.WriteLine("because " + cr.ToString(data, shellProcEnv.ProcEnv.NamedGraph, additionalData));

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            if(!detailedMode)
            {
                context.highlightSeq = lastlyEntered;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                PrintDebugTracesStack(false);
            }

            QueryContinueOrTrace(true);
        }

        /// <summary>
        /// highlights the values in the graphs if debugging is active (annotating them with the source names)
        /// </summary>
        private void DebugHighlight(string message, List<object> values, List<string> sourceNames)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Highlight, 
                message, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Continue)
                return;

            Console.Write("Highlighting: " + message);
            if(sourceNames.Count > 0)
                Console.Write(" with annotations");
            for(int i = 0; i < sourceNames.Count; ++i)
            {
                Console.Write(" ");
                Console.Write(sourceNames[i]);
            }
            Console.WriteLine();

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            if(!detailedMode)
            {
                context.highlightSeq = lastlyEntered;
                SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                PrintDebugTracesStack(false);
            }

            ShellProcEnv.ProcEnv.HighlightingUnderway = true;
            HandleHighlight(values, sourceNames);
            ShellProcEnv.ProcEnv.HighlightingUnderway = false;

            QueryContinueOrTrace(true);
        }

        private void PrintDebugTracesStack(bool full)
        {
            Console.WriteLine("Subrule traces stack is:");
            for(int i = 0; i < computationsEnteredStack.Count; ++i)
            {
                if(!full && computationsEnteredStack[i].type != SubruleComputationType.Entry)
                    continue;
                Console.WriteLine(computationsEnteredStack[i].ToString(full));
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

                ConsoleKeyInfo key = grShellImpl.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'a':
                    grShellImpl.Cancel();
                    return;                               // never reached
                case 's':
                    if(isBottomUpBreak && !stepMode)
                    {
                        stepMode = true;
                        dynamicStepMode = false;
                        detailedMode = false;
                        curStepSequence = null;
                    }
                    return;
                case 'r':
                    if(!isBottomUpBreak && computationsEnteredStack.Count > 0)
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
                    if(computationsEnteredStack.Count > 0)
                    {
                        HandleStackTrace();
                        SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                        Console.WriteLine();
                        PrintDebugTracesStack(true);
                        break;
                    }
                    else
                        return;
                case 'f':
                    HandleFullState();
                    SequencePrinter.PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
                    PrintDebugTracesStack(true);
                    break;
                default:
                    return;
                }
            } while(true);
        }

        private void PrintDebugInstructions(bool isBottomUpBreak)
        {
            if(!isBottomUpBreak && computationsEnteredStack.Count == 0)
                Console.WriteLine("Debugging (detailed) continues with any key, besides (f)ull state or (a)bort.");
            else
            {
                if(!isBottomUpBreak)
                {
                    Console.Write("Detailed subrule debugging -- ");
                    if(computationsEnteredStack.Count > 0)
                    {
                        Console.Write("(r)un until end of detail debugging, ");
                        if(TargetStackLevelForUpInDetailedMode() > 0)
                        {
                            Console.Write("(u)p from current entry, ");
                            if(TargetStackLevelForOutInDetailedMode() > 0)
                            {
                                Console.Write("(o)ut of detail debugging entry we are nested in, ");
                            }
                        }
                    }
                }
                else
                    Console.Write("Watchpoint/halt/highlight hit -- ");

                if(isBottomUpBreak && !stepMode)
                    Console.Write("(s)tep mode, ");

                if(computationsEnteredStack.Count > 0)
                    Console.Write("print subrule stack(t)race, (f)ull state, or (a)bort, any other key continues ");
                else
                    Console.Write("(f)ull state, or (a)bort, any other key continues ");

                if(!isBottomUpBreak)
                    Console.WriteLine("detailed debugging.");
                else
                    Console.WriteLine("debugging as before.");
            }
        }

        private int TargetStackLevelForUpInDetailedMode()
        {
            int posOfEntry = 0;
            for(int i = computationsEnteredStack.Count - 1; i >= 0; --i)
            {
                if(computationsEnteredStack[i].type == SubruleComputationType.Entry)
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
                if(computationsEnteredStack[i].type == SubruleComputationType.Entry)
                {
                    posOfEntry = i;
                    break;
                }
            }
            return posOfEntry;
        }

        private void DebugOnConnectionLost()
        {
            Console.WriteLine("Connection to yComp lost!");
            grShellImpl.Cancel();
        }

        /// <summary>
        /// Registers event handlers for needed LibGr events
        /// </summary>
        private void RegisterLibGrEvents(INamedGraph graph)
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
            graph.OnSettingAddedNodeNames += DebugSettingAddedNodeNames;
            graph.OnSettingAddedEdgeNames += DebugSettingAddedEdgeNames;

            shellProcEnv.ProcEnv.OnMatched += DebugMatched;
            shellProcEnv.ProcEnv.OnRewritingNextMatch += DebugNextMatch;
            shellProcEnv.ProcEnv.OnFinished += DebugFinished;

            shellProcEnv.ProcEnv.OnSwitchingToSubgraph += DebugSwitchToGraph;
            shellProcEnv.ProcEnv.OnReturnedFromSubgraph += DebugReturnedFromGraph;

            shellProcEnv.ProcEnv.OnDebugEnter += DebugEnter;
            shellProcEnv.ProcEnv.OnDebugExit += DebugExit;
            shellProcEnv.ProcEnv.OnDebugEmit += DebugEmit;
            shellProcEnv.ProcEnv.OnDebugHalt += DebugHalt;
            shellProcEnv.ProcEnv.OnDebugHighlight += DebugHighlight;

            shellProcEnv.ProcEnv.OnEntereringSequence += DebugEnteringSequence;
            shellProcEnv.ProcEnv.OnExitingSequence += DebugExitingSequence;
            shellProcEnv.ProcEnv.OnEndOfIteration += DebugEndOfIteration;
        }

        /// <summary>
        /// Unregisters the events previously registered with RegisterLibGrEvents()
        /// </summary>
        private void UnregisterLibGrEvents(INamedGraph graph)
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
            graph.OnSettingAddedNodeNames -= DebugSettingAddedNodeNames;
            graph.OnSettingAddedEdgeNames -= DebugSettingAddedEdgeNames;

            shellProcEnv.ProcEnv.OnMatched -= DebugMatched;
            shellProcEnv.ProcEnv.OnRewritingNextMatch -= DebugNextMatch;
            shellProcEnv.ProcEnv.OnFinished -= DebugFinished;

            shellProcEnv.ProcEnv.OnSwitchingToSubgraph -= DebugSwitchToGraph;
            shellProcEnv.ProcEnv.OnReturnedFromSubgraph -= DebugReturnedFromGraph;

            shellProcEnv.ProcEnv.OnDebugEnter -= DebugEnter;
            shellProcEnv.ProcEnv.OnDebugExit -= DebugExit;
            shellProcEnv.ProcEnv.OnDebugEmit -= DebugEmit;
            shellProcEnv.ProcEnv.OnDebugHalt -= DebugHalt;
            shellProcEnv.ProcEnv.OnDebugHighlight -= DebugHighlight;

            shellProcEnv.ProcEnv.OnEntereringSequence -= DebugEnteringSequence;
            shellProcEnv.ProcEnv.OnExitingSequence -= DebugExitingSequence;
            shellProcEnv.ProcEnv.OnEndOfIteration -= DebugEndOfIteration;
        }

        #endregion Event Handling
    }
}
