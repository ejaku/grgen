/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

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
        GrShellImpl grShellImpl;
        ShellGraphProcessingEnvironment shellProcEnv;
        ElementRealizers realizers;

        Process viewerProcess = null;
        YCompClient ycompClient = null;
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

        IRulePattern curRulePattern = null;
        int nextAddedNodeIndex = 0;
        int nextAddedEdgeIndex = 0;

        String[] curAddedNodeNames = null;
        String[] curAddedEdgeNames = null;

        Dictionary<INode, String> annotatedNodes = new Dictionary<INode, String>();
        Dictionary<IEdge, String> annotatedEdges = new Dictionary<IEdge, String>();

        LinkedList<Sequence> loopList = new LinkedList<Sequence>();

        Dictionary<INode, bool> addedNodes = new Dictionary<INode, bool>();
        List<String> deletedNodes = new List<String>();
        Dictionary<IEdge, bool> addedEdges = new Dictionary<IEdge, bool>();
        List<String> deletedEdges = new List<String>();
        Dictionary<INode, bool> retypedNodes = new Dictionary<INode, bool>();
        Dictionary<IEdge, bool> retypedEdges = new Dictionary<IEdge, bool>();

        Dictionary<INode, bool> excludedGraphNodesIncluded = new Dictionary<INode, bool>();
        Dictionary<IEdge, bool> excludedGraphEdgesIncluded = new Dictionary<IEdge, bool>();

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

        public Debugger(GrShellImpl grShellImpl)
            : this(grShellImpl, "Orthogonal", null)
        {
        }

        public Debugger(GrShellImpl grShellImpl, String debugLayout)
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
        public Debugger(GrShellImpl grShellImpl, String debugLayout, Dictionary<String, String> layoutOptions)
        {
            this.grShellImpl = grShellImpl;
            this.shellProcEnv = grShellImpl.CurrentShellProcEnv;
            this.realizers = grShellImpl.realizers;

            this.context = new PrintSequenceContext(grShellImpl.Workaround);

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
                            if(illegalOptions == null) illegalOptions = new List<String>();
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
        void UploadGraph(INamedGraph graph)
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
        int GetFreeTCPPort()
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
        Sequence GetParentSequence(Sequence childseq, Sequence seq)
        {
            Sequence res = null;
            foreach(Sequence child in seq.Children)
            {
                if(child == childseq) return seq;
                res = GetParentSequence(childseq, child);
                if(res != null) return res;
            }
            return res;
        }

        /// <summary>
        /// Reads a key from the keyboard using the workaround manager of grShellImpl.
        /// If CTRL+C is pressed, grShellImpl.Cancel() is called.
        /// </summary>
        /// <returns>The ConsoleKeyInfo object for the pressed key.</returns>
        ConsoleKeyInfo ReadKeyWithCancel()
        {
            if(grShellImpl.OperationCancelled)
                grShellImpl.Cancel();
            Console.TreatControlCAsInput = true;
            ConsoleKeyInfo key = grShellImpl.Workaround.ReadKey(true);
            Console.TreatControlCAsInput = false;
            if(key.Key == ConsoleKey.C && (key.Modifiers & ConsoleModifiers.Control) != 0)
                grShellImpl.Cancel();
            return key;
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
                ConsoleKeyInfo key = ReadKeyWithCancel();
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
                    HandleToggleBreakpoints();
                    context.highlightSeq = seq;
                    context.success = false;
                    PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
                    break;
                case 'w':
                    HandleWatchpoints();
                    break;
                case 'c':
                    HandleToggleChoicepoints();
                    context.highlightSeq = seq;
                    context.success = false;
                    PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
                    break;
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
                    PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
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
                    PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
                    break;
                case 'f':
                    HandleFullState();
                    PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
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

        void HandleToggleBreakpoints()
        {
            Console.Write("Available breakpoint positions:\n  ");

            PrintSequenceContext contextBp = new PrintSequenceContext(grShellImpl.Workaround);
            contextBp.bpPosCounter = 0;
            PrintSequence(debugSequences.Peek(), contextBp, debugSequences.Count);
            Console.WriteLine();

            if(contextBp.bpPosCounter == 0)
            {
                Console.WriteLine("No breakpoint positions available!");
                return;
            }

            int pos = HandleTogglePoint("breakpoint", contextBp.bpPosCounter);
            if (pos == -1)
                return;

            TogglePointInAllInstances(pos, false);
        }

        void HandleToggleChoicepoints()
        {
            Console.Write("Available choicepoint positions:\n  ");

            PrintSequenceContext contextCp = new PrintSequenceContext(grShellImpl.Workaround);
            contextCp.cpPosCounter = 0;
            PrintSequence(debugSequences.Peek(), contextCp, debugSequences.Count);
            Console.WriteLine();

            if (contextCp.cpPosCounter == 0)
            {
                Console.WriteLine("No choicepoint positions available!");
                return;
            }

            int pos = HandleTogglePoint("choicepoint", contextCp.cpPosCounter);
            if (pos == -1)
                return;

            TogglePointInAllInstances(pos, true);
        }

        void HandleToggleLazyChoice()
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

        void HandleWatchpoints()
        {
            Console.WriteLine("List of registered watchpoints:");
            for(int i = 0; i < shellProcEnv.SubruleDebugConfig.ConfigurationRules.Count; ++i)
            {
                Console.WriteLine(i + " - " + shellProcEnv.SubruleDebugConfig.ConfigurationRules[i].ToString());
            }

            while(true)
            {
                Console.WriteLine("Press (e) to edit, (t) to toggle (enable/disable), or (d) to delete one of the watchpoints."
                    + " Press (i) to insert at a specified position, or (p) to append a watchpoint."
                    + " Press (a) to abort.");

                int num = -1;
                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case 'e':
                        num = QueryWatchpoint("edit");
                        if(num == -1)
                            break;
                        EditWatchpoint(num);
                        Console.WriteLine("Back from watchpoints to debugging.");
                        return;
                    case 't':
                        num = QueryWatchpoint("toggle (enable/disable)");
                        if(num == -1)
                            break;
                        ToggleWatchpoint(num);
                        Console.WriteLine("Back from watchpoints to debugging.");
                        return;
                    case 'd':
                        num = QueryWatchpoint("delete");
                        if(num == -1)
                            break;
                        DeleteWatchpoint(num);
                        Console.WriteLine("Back from watchpoints to debugging.");
                        return;
                    case 'i':
                        num = QueryWatchpoint("insert");
                        if(num == -1)
                            break;
                        InsertWatchpoint(num);
                        Console.WriteLine("Back from watchpoints to debugging.");
                        return;
                    case 'p':
                        AppendWatchpoint();
                        Console.WriteLine("Back from watchpoints to debugging.");
                        return;
                    case 'a':
                        Console.WriteLine("Back from watchpoints to debugging.");
                        return;
                    default:
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (e)dit, (t)oggle, (d)elete, (i)nsert, a(p)pend, or (a)bort allowed! ");
                        break;
                }
            }
        }

        int QueryWatchpoint(string action)
        {
            Console.Write("Enter number of watchpoint to " + action + " (-1 for abort): ");
            String numStr = Console.ReadLine();
            int num;
            if(int.TryParse(numStr, out num))
            {
                if(num < -1 || num >= shellProcEnv.SubruleDebugConfig.ConfigurationRules.Count)
                {
                    Console.WriteLine("You must specify a number between -1 and " + (shellProcEnv.SubruleDebugConfig.ConfigurationRules.Count - 1) + "!");
                    return -1;
                }
                return num;
            }
            Console.WriteLine("You must enter a valid integer number!");
            return -1;
        }

        void EditWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = shellProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            cr = EditOrCreateRule(cr);
            if(cr == null)
                Console.WriteLine("aborted");
            else
            {
                Console.WriteLine("edited entry " + num + " - " + cr.ToString());
                shellProcEnv.SubruleDebugConfig.Replace(num, cr);
            }
        }

        void ToggleWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = shellProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            cr.Enabled = !cr.Enabled;
            Console.WriteLine("toggled entry " + num + " - " + cr.ToString());
        }

        void DeleteWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = shellProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            shellProcEnv.SubruleDebugConfig.Delete(num);
            Console.WriteLine("deleted entry " + num + " - " + cr.ToString());
        }

        void InsertWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = EditOrCreateRule(null);
            if(cr == null)
                Console.WriteLine("aborted");
            else
            {
                shellProcEnv.SubruleDebugConfig.Insert(cr, num);
                Console.WriteLine("inserted entry " + num + " - " + cr.ToString());
            }
        }

        void AppendWatchpoint()
        {
            SubruleDebuggingConfigurationRule cr = EditOrCreateRule(null);
            if(cr == null)
                Console.WriteLine("aborted");
            else
            {
                shellProcEnv.SubruleDebugConfig.Insert(cr);
                Console.WriteLine("appended entry " + (shellProcEnv.SubruleDebugConfig.ConfigurationRules.Count - 1) + " - " + cr.ToString());
            }
        }

        SubruleDebuggingConfigurationRule EditOrCreateRule(SubruleDebuggingConfigurationRule cr)
        {
            // edit or keep type
            SubruleDebuggingEvent sde;
            while(true)
            {
                Console.WriteLine("What event to listen to?");
                Console.Write("(0) subrule entry aka Debug::add" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Add ? " or (k)eep\n" : "\n"));
                Console.Write("(1) subrule exit aka Debug::rem" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Rem ? " or (k)eep\n" : "\n"));
                Console.Write("(2) subrule report aka Debug::emit" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Emit ? " or (k)eep\n" : "\n"));
                Console.Write("(3) subrule halt aka Debug::halt" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Halt ? " or (k)eep\n" : "\n"));
                Console.Write("(4) subrule highlight aka Debug::highlight" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Highlight ? " or (k)eep\n" : "\n"));
                Console.Write("(5) rule match" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Match ? " or (k)eep\n" : "\n"));
                Console.Write("(6) graph element creation" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.New ? " or (k)eep\n" : "\n"));
                Console.Write("(7) graph element deletion" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Delete ? " or (k)eep\n" : "\n"));
                Console.Write("(8) graph element retyping" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Retype ? " or (k)eep\n" : "\n"));
                Console.Write("(9) graph element attribute assignment" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.SetAttributes ? " or (k)eep\n" : "\n"));
                Console.WriteLine("(a)bort");
                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case '0':
                        sde = SubruleDebuggingEvent.Add;
                        goto after_debugging_event;
                    case '1':
                        sde = SubruleDebuggingEvent.Rem;
                        goto after_debugging_event;
                    case '2':
                        sde = SubruleDebuggingEvent.Emit;
                        goto after_debugging_event;
                    case '3':
                        sde = SubruleDebuggingEvent.Halt;
                        goto after_debugging_event;
                    case '4':
                        sde = SubruleDebuggingEvent.Highlight;
                        goto after_debugging_event;
                    case '5':
                        sde = SubruleDebuggingEvent.Match;
                        goto after_debugging_event;
                    case '6':
                        sde = SubruleDebuggingEvent.New;
                        goto after_debugging_event;
                    case '7':
                        sde = SubruleDebuggingEvent.Delete;
                        goto after_debugging_event;
                    case '8':
                        sde = SubruleDebuggingEvent.Retype;
                        goto after_debugging_event;
                    case '9':
                        sde = SubruleDebuggingEvent.SetAttributes;
                        goto after_debugging_event;
                    case 'a':
                        return null;
                    default:
                        if(key.KeyChar == 'k' && cr != null)
                        {
                            sde = cr.DebuggingEvent;
                            goto after_debugging_event;
                        }
                        else
                        {
                            Console.WriteLine("Illegal choice (Key = " + key.Key
                                + ")! Only (0)...(9), (a)bort allowed! ");
                            break;
                        }
                }
            }
after_debugging_event: ;

            // for Add, Rem, Emit, Halt, Highlight
            string message = null;
            SubruleMesssageMatchingMode smmm = SubruleMesssageMatchingMode.Undefined;
            // for Match
            IAction action = null;
            // for New, Delete, Retype, SetAttributes
            string graphElementName = null;
            GrGenType graphElementType = null;
            bool only = false;

            if(sde==SubruleDebuggingEvent.Add || sde==SubruleDebuggingEvent.Rem || sde==SubruleDebuggingEvent.Emit 
                || sde==SubruleDebuggingEvent.Halt || sde==SubruleDebuggingEvent.Highlight)
            {
                // edit or keep message matching mode and message
                Console.WriteLine("Enter the subrule message to match.");
                if(cr != null)
                    Console.WriteLine("Empty string for " + cr.MessageToMatch);
                else
                    Console.WriteLine("Empty string to abort.");

                message = Console.ReadLine();
                if(message.Length == 0)
                {
                    if(cr != null)
                        message = cr.MessageToMatch;
                    else
                        return null;
                }

                while(true)
                {
                    Console.WriteLine("How to match the subrule message?");
                    Console.Write("(0) equals" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Equals ? " or (k)eep\n" : "\n"));
                    Console.Write("(1) startsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.StartsWith ? " or (k)eep\n" : "\n"));
                    Console.Write("(2) endsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.EndsWith ? " or (k)eep\n" : "\n"));
                    Console.Write("(3) contains" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Contains ? " or (k)eep\n" : "\n"));
                    Console.WriteLine("(a)bort");
                    ConsoleKeyInfo key = ReadKeyWithCancel();
                    switch(key.KeyChar)
                    {
                        case '0':
                            smmm = SubruleMesssageMatchingMode.Equals;
                            goto after_message_matching_mode;
                        case '1':
                            smmm = SubruleMesssageMatchingMode.StartsWith;
                            goto after_message_matching_mode;
                        case '2':
                            smmm = SubruleMesssageMatchingMode.EndsWith;
                            goto after_message_matching_mode;
                        case '3':
                            smmm = SubruleMesssageMatchingMode.Contains;
                            goto after_message_matching_mode;
                        case 'a':
                            return null;
                        default:
                            if(key.KeyChar == 'k' && cr != null)
                            {
                                smmm = cr.MessageMatchingMode;
                                goto after_message_matching_mode;
                            }
                            else
                            {
                                Console.WriteLine("Illegal choice (Key = " + key.Key
                                    + ")! Only (0)...(3), (a)bort allowed! ");
                                break;
                            }
                    }
                }
after_message_matching_mode: ;
            }
            else if(sde==SubruleDebuggingEvent.Match)
            {
                // edit ok keep action name
                while(true)
                {
                    Console.WriteLine("Enter the name of the action to match.");
                    if(cr != null)
                        Console.WriteLine("Empty string for " + cr.ActionToMatch.PackagePrefixedName);
                    else
                        Console.WriteLine("Empty string to abort.");

                    String actionName = Console.ReadLine();
                    if(actionName.Length == 0)
                    {
                        if(cr != null)
                        {
                            action = cr.ActionToMatch;
                            break;
                        }
                        else
                            return null;
                    }

                    action = shellProcEnv.ProcEnv.Actions.GetAction(actionName);
                    if(action == null)
                        Console.WriteLine("Unknown action: " + actionName);
                    else
                        break;
                }
            }
            else if(sde==SubruleDebuggingEvent.New || sde==SubruleDebuggingEvent.Delete
                || sde==SubruleDebuggingEvent.Retype || sde==SubruleDebuggingEvent.SetAttributes)
            {
                // edit or keep choice of type, exact type, name
                bool byName;
                while(true)
                {
                    Console.WriteLine("Match graph element based on name or based on type?");
                    Console.Write("(0) by name" + (cr != null && cr.NameToMatch != null ? " or (k)eep\n" : "\n"));
                    Console.Write("(1) by type" + (cr != null && cr.NameToMatch == null ? " or (k)eep\n" : "\n"));
                    Console.WriteLine("(a)bort");
                    ConsoleKeyInfo key = ReadKeyWithCancel();
                    switch(key.KeyChar)
                    {
                        case '0':
                            byName = true;
                            goto after_name_or_type_decision;
                        case '1':
                            byName = false;
                            goto after_name_or_type_decision;
                        case 'a':
                            return null;
                        default:
                            if(key.KeyChar == 'k' && cr != null)
                            {
                                byName = cr.NameToMatch != null;
                                goto after_name_or_type_decision;
                            }
                            else
                            {
                                Console.WriteLine("Illegal choice (Key = " + key.Key
                                    + ")! Only (0), (1), (a)bort allowed! ");
                                break;
                            }
                    }
                }
after_name_or_type_decision: ;

                if(byName)
                {
                    Console.WriteLine("Enter the graph element name to match.");
                    if(cr != null)
                        Console.WriteLine("Empty string for " + cr.NameToMatch);
                    else
                        Console.WriteLine("Empty string to abort.");

                    graphElementName = Console.ReadLine();
                    if(graphElementName.Length == 0)
                    {
                        if(cr != null)
                            graphElementName = cr.NameToMatch;
                        else
                            return null;
                    }
                }
                else
                {
                    while(true)
                    {
                        Console.WriteLine("Enter the type of the graph element to match.");
                        if(cr != null)
                            Console.WriteLine("Empty string for " + cr.TypeToMatch.PackagePrefixedName);
                        else
                            Console.WriteLine("Empty string to abort.");

                        String graphElementTypeName = Console.ReadLine();
                        if(graphElementTypeName.Length == 0)
                        {
                            if(cr != null)
                            {
                                graphElementType = cr.TypeToMatch;
                                break;
                            }
                            else
                                return null;
                        }

                        graphElementType = grShellImpl.GetGraphElementType(graphElementTypeName);
                        if(graphElementType == null)
                            Console.WriteLine("Unknown graph element type: " + graphElementTypeName);
                        else
                            break;
                    }

                    while(true)
                    {
                        Console.WriteLine("Only the graph element type or also subtypes?");
                        Console.Write("(0) also subtypes" + (cr != null && !cr.OnlyThisType ? " or (k)eep\n" : "\n"));
                        Console.Write("(1) only the type" + (cr != null && cr.OnlyThisType ? " or (k)eep\n" : "\n"));
                        Console.WriteLine("(a)bort");
                        ConsoleKeyInfo key = ReadKeyWithCancel();
                        switch(key.KeyChar)
                        {
                            case '0':
                                only = false;
                                goto after_only_decision;
                            case '1':
                                only = true;
                                goto after_only_decision;
                            case 'a':
                                return null;
                            default:
                                if(key.KeyChar == 'k' && cr != null)
                                {
                                    only = cr.OnlyThisType;
                                    goto after_only_decision;
                                }
                                else
                                {
                                    Console.WriteLine("Illegal choice (Key = " + key.Key
                                        + ")! Only (0), (1), (a)bort allowed! ");
                                    break;
                                }
                        }
                    }
after_only_decision: ;
                }
            }
            
            // edit or keep decision action
            SubruleDebuggingDecision sdd;
            while(true)
            {
                Console.WriteLine("How to react when the event is triggered?");
                Console.Write("(0) break" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Break ? " or (k)eep\n" : "\n"));
                Console.Write("(1) continue" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Continue ? " or (k)eep\n" : "\n"));
                Console.WriteLine("(a)bort");
                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case '0':
                        sdd = SubruleDebuggingDecision.Break;
                        goto after_debugging_decision;
                    case '1':
                        sdd = SubruleDebuggingDecision.Continue;
                        goto after_debugging_decision;
                    case 'a':
                        return null;
                    default:
                        if(key.KeyChar == 'k' && cr != null)
                        {
                            sdd = cr.DecisionOnMatch;
                            goto after_debugging_decision;
                        }
                        else
                        {
                            Console.WriteLine("Illegal choice (Key = " + key.Key
                                + ")! Only (0), (1), (a)bort allowed! ");
                            break;
                        }
                }
            }
after_debugging_decision: ;

            // edit or keep condition if type action or graph change
            SequenceExpression ifClause = null;
            while(sde != SubruleDebuggingEvent.Add && sde != SubruleDebuggingEvent.Rem && sde != SubruleDebuggingEvent.Retype
                && sde != SubruleDebuggingEvent.Halt && sde != SubruleDebuggingEvent.Highlight) // condition won't change, decides entry only
            {
                Console.WriteLine("Conditional rule via sequence expression?");
                if(cr != null && cr.IfClause != null)
                    Console.WriteLine("Press enter to take over " + cr.IfClause.Symbol + ", enter \"-\" to clear the condition, otherwise enter the sequence expression to apply.");
                else
                    Console.WriteLine("Press enter if you don't want to add an if part, otherwise enter the sequence expression to apply.");

                String ifClauseStr = Console.ReadLine();
                if(ifClauseStr.Length == 0)
                {
                    if(cr != null)
                        ifClause = cr.IfClause;
                    break;
                }
                if(ifClauseStr == "-")
                    break;

                Dictionary<String, String> predefinedVariables = new Dictionary<String, String>();
                predefinedVariables.Add("this", "");
                string ruleOfMatchThis = null;
                if(sde == SubruleDebuggingEvent.Match)
                    ruleOfMatchThis = action.Name;
                string typeOfGraphElementThis = null;
                if(sde == SubruleDebuggingEvent.New || sde == SubruleDebuggingEvent.Delete
                    || sde == SubruleDebuggingEvent.Retype || sde == SubruleDebuggingEvent.SetAttributes)
                {
                    typeOfGraphElementThis = "";
                    if(graphElementType != null)
                        typeOfGraphElementThis = graphElementType.PackagePrefixedName;
                }
                try
                {
                    List<String> warnings = new List<String>();
                    ifClause = SequenceParser.ParseSequenceExpression(ifClauseStr, predefinedVariables, shellProcEnv.ProcEnv.Actions, ruleOfMatchThis, typeOfGraphElementThis, warnings);
                    foreach(string warning in warnings)
                    {
                        Console.WriteLine("The sequence expression for the if clause reported back: " + warning);
                    }
                    break;
                }
                catch(SequenceParserException ex)
                {
                    Console.WriteLine("Unable to parse sequence expression");
                    grShellImpl.HandleSequenceParserException(ex);
                }
                catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
                {
                    Console.WriteLine("Unable to parse sequence expression: " + ex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Unable to parse sequence expression : " + ex);
                }
            }

            if(sde == SubruleDebuggingEvent.Add || sde == SubruleDebuggingEvent.Rem || sde == SubruleDebuggingEvent.Emit
                || sde == SubruleDebuggingEvent.Halt || sde == SubruleDebuggingEvent.Highlight)
            {
                return new SubruleDebuggingConfigurationRule(sde, message, smmm, sdd);
            }
            else if(sde == SubruleDebuggingEvent.Match)
            {
                return new SubruleDebuggingConfigurationRule(sde, action, sdd, ifClause);
            }
            else if(sde == SubruleDebuggingEvent.New || sde == SubruleDebuggingEvent.Delete
                || sde == SubruleDebuggingEvent.Retype || sde == SubruleDebuggingEvent.SetAttributes)
            {
                if(graphElementName != null)
                    return new SubruleDebuggingConfigurationRule(sde, graphElementName, sdd, ifClause);
                else
                    return new SubruleDebuggingConfigurationRule(sde, graphElementType, only, sdd, ifClause);
            }

            return null;
        }

        void HandleShowVariable(Sequence seq)
        {
            PrintVariables(null, null);
            PrintVariables(debugSequences.Peek(), seq);
            PrintVisited();
        }

        void HandleDump()
        {
            string filename = grShellImpl.ShowGraphWith("ycomp", "");
            Console.WriteLine("Showing dumped graph " + filename + " with ycomp");
        }

        void HandleAsGraph(Sequence seq)
        {
            object toBeShownAsGraph = null;
            AttributeType attrType = null;
            while(true)
            {
                Console.WriteLine("Enter name of variable or attribute access to show as graph (just enter for abort): ");
                Console.WriteLine("Examples: \"v\", \"v.a\", \"@(\"$0\").a\" ");
                String str = Console.ReadLine();
                if(str.Length == 0)
                {
                    Console.WriteLine("Back from as-graph display to debugging.");
                    return;
                }

                if(str.StartsWith("@"))
                {
                    // graph element by name
                    string attributeName;
                    IGraphElement elem = ParseAccessByName(str, out attributeName);
                    if(elem == null)
                    {
                        Console.WriteLine("Can't parse graph access / unknown graph element: " + str);
                        continue;
                    }
                    if(attributeName == null)
                    {
                        Console.WriteLine("The result of a graph access is a node or edge, you must access an attribute: " + str);
                        continue;
                    }
                    attrType = elem.Type.GetAttributeType(attributeName);
                    if(attrType == null)
                    {
                        Console.WriteLine("Unknown attribute: " + attributeName);
                        continue;
                    }
                    object attribute = elem.GetAttribute(attributeName);
                    if(attribute == null)
                    {
                        Console.WriteLine("Null-valued attribute: " + attributeName);
                        continue;
                    }
                    toBeShownAsGraph = attribute;
                    break;
                }
                else
                {
                    // variable
                    string attributeName;
                    object value = ParseVariable(str, seq, out attributeName);
                    if(value == null)
                    {
                        Console.WriteLine("Can't parse variable / unknown variable / null-valued variable: " + str);
                        continue;
                    }
                    if(attributeName == null)
                        toBeShownAsGraph = value;
                    else
                    {
                        if(!(value is IGraphElement))
                        {
                            Console.WriteLine("Can't access attribute, the variable value is not a graph element: " + str);
                            continue;
                        }
                        IGraphElement elem = (IGraphElement)value;
                        attrType = elem.Type.GetAttributeType(attributeName);
                        if(attrType == null)
                        {
                            Console.WriteLine("Unknown attribute: " + attributeName);
                            continue;
                        }
                        object attribute = elem.GetAttribute(attributeName);
                        if(attribute == null)
                        {
                            Console.WriteLine("Null-valued attribute: " + attributeName);
                            continue;
                        }
                        toBeShownAsGraph = attribute;
                    }
                    break;
                }
            }
            
            INamedGraph graph = shellProcEnv.ProcEnv.Graph.Model.AsGraph(toBeShownAsGraph, attrType, shellProcEnv.ProcEnv.Graph);
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
            ReadKeyWithCancel();

            Console.WriteLine("...return to normal graph.");
            ycompClient.ClearGraph();
            ycompClient.Graph = shellProcEnv.ProcEnv.NamedGraph;
            if(!ycompClient.dumpInfo.IsExcludedGraph())
                UploadGraph(shellProcEnv.ProcEnv.NamedGraph);

            Console.WriteLine("Back from as-graph display to debugging.");
        }

        IGraphElement ParseAccessByName(string str, out string attribute)
        {
            attribute = null;

            int pos = 0;
            if(str[pos++] != '@')
                return null;
            if(str[pos++] != '(')
                return null;
            if(str[pos++] != '"')
                return null;
            StringBuilder sb = new StringBuilder();
            while(str[pos] != '"')
            {
                sb.Append(str[pos++]);
            }
            if(str[pos++] != '"')
                return null;
            if(str[pos++] != ')')
                return null;
            if(pos == str.Length)
                return grShellImpl.GetElemByName(sb.ToString());
            if(str[pos++] != '.')
                return null;
            attribute = str.Substring(pos);
            return grShellImpl.GetElemByName(sb.ToString());
        }

        object ParseVariable(string str, Sequence seq, out string attribute)
        {
            string varName;
            if(str.Contains("."))
            {
                varName = str.Substring(0, str.LastIndexOf('.'));
                attribute = str.Substring(str.LastIndexOf('.') + 1);
            }
            else
            {
                varName = str;
                attribute = null;
            }

            Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            (debugSequences.Peek()).GetLocalVariables(seqVars, containerConstructors, seq);
            foreach(SequenceVariable var in seqVars.Keys)
            {
                if(var.Name == varName)
                    return var.Value;
            }
            foreach(Variable var in shellProcEnv.ProcEnv.Variables)
            {
                if(var.Name == varName)
                    return var.Value;
            }
            return null;
        }

        void HandleUserHighlight(Sequence seq)
        {
            Console.Write("Enter name of variable or id of visited flag to highlight (multiple values may be given comma-separated; just enter for abort): ");
            String str = Console.ReadLine();
            List<object> values;
            List<string> annotations;
            ComputeHighlight(seq, str, out values, out annotations);
            DoHighlight(values, annotations);
        }

        void HandleHighlight(List<object> originalValues, List<string> sourceNames)
        {
            DoHighlight(originalValues, sourceNames);
        }

        void ComputeHighlight(Sequence seq, String str, out List<object> values, out List<string> annotations)
        {
            values = new List<object>();
            annotations = new List<string>();

            if(str.Length == 0)
                return;

            string[] arguments = str.Split(',');

            for(int i = 0; i < arguments.Length; ++i)
            {
                string argument = arguments[i].Trim();
                if(i + 1 < arguments.Length)
                {
                    string potentialAnnotationArgument = arguments[i + 1];
                    if(potentialAnnotationArgument.StartsWith("\"") && potentialAnnotationArgument.EndsWith("\"")
                        || potentialAnnotationArgument.StartsWith("'") && potentialAnnotationArgument.EndsWith("'"))
                    {
                        ComputeHighlightArgument(seq, argument, potentialAnnotationArgument.Substring(1, argument.Length-2), values, annotations);
                        ++i; // skip the annotation argument
                    }
                }
                else
                    ComputeHighlightArgument(seq, argument, null, values, annotations);
            }
        }

        private void ComputeHighlightArgument(Sequence seq, string argument, string annotation, List<object> sources, List<string> annotations)
        {
            // visited flag directly given as constant
            int num;
            if(int.TryParse(argument, out num))
            {
                sources.Add(num);
                if(annotation != null)
                    annotations.Add(annotation);
                else
                    annotations.Add(num.ToString());
                return;
            }

            // variable
            Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            (debugSequences.Peek()).GetLocalVariables(seqVars, containerConstructors, seq);
            foreach(SequenceVariable var in seqVars.Keys)
            {
                if(var.Name == argument)
                {
                    sources.Add(var.Value);
                    if(annotation != null)
                        annotations.Add(annotation);
                    else
                        annotations.Add(var.Name);
                    return;
                }
            }
            foreach(Variable var in shellProcEnv.ProcEnv.Variables)
            {
                if(var.Name == argument)
                {
                    sources.Add(var.Value);
                    if(annotation != null)
                        annotations.Add(annotation);
                    else
                        annotations.Add(var.Name);
                    return;
                }
            }
            Console.WriteLine("Unknown variable " + argument + "!");
            Console.WriteLine("Use (v)ariables to print variables and visited flags.");
        }

        void DoHighlight(List<object> sources, List<string> annotations)
        {
            if(ycompClient.dumpInfo.IsExcludedGraph())
            {
                ycompClient.ClearGraph();
                excludedGraphNodesIncluded.Clear();
                excludedGraphEdgesIncluded.Clear();
            }

            for(int i = 0; i < sources.Count; ++i)
            {
                HighlightValue(sources[i], annotations[i], true);
            }

            if(ycompClient.dumpInfo.IsExcludedGraph())
            {
                // highlight values added in highlight value calls to excludedGraphNodesIncluded
                AddNeighboursAndParentsOfNeededGraphElements();
            }

            foreach(KeyValuePair<INode, string> nodeToName in annotatedNodes)
                ycompClient.AnnotateElement(nodeToName.Key, nodeToName.Value);
            foreach(KeyValuePair<IEdge, string> edgeToName in annotatedEdges)
                ycompClient.AnnotateElement(edgeToName.Key, edgeToName.Value);

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to continue...");
            ReadKeyWithCancel();

            for(int i = 0; i < sources.Count; ++i)
            {
                HighlightValue(sources[i], annotations[i], false);
            }

            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            Console.WriteLine("End of highlighting");
        }

        void HighlightValue(object value, string name, bool addAnnotation)
        {
            if(value is IDictionary)
                HighlightDictionary((IDictionary)value, name, addAnnotation);
            else if(value is IList)
                HighlightList((IList)value, name, addAnnotation);
            else if(value is IDeque)
                HighlightDeque((IDeque)value, name, addAnnotation);
            else
                HighlightSingleValue(value, name, addAnnotation);
        }

        void HighlightDictionary(IDictionary value, string name, bool addAnnotation)
        {
            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(value.GetType(), out keyType, out valueType);
            if(valueType == typeof(de.unika.ipd.grGen.libGr.SetValueType))
            {
                foreach(DictionaryEntry entry in value)
                {
                    if(entry.Key is IGraphElement)
                        HighlightSingleValue(entry.Key, name, addAnnotation);
                }
            }
            else
            {
                int cnt = 0;
                foreach(DictionaryEntry entry in value)
                {
                    if(entry.Key is INode && entry.Value is INode)
                    {
                        HighlightMapping((INode)entry.Key, (INode)entry.Value, name, cnt, addAnnotation);
                        ++cnt;
                    }
                    else
                    {
                        if(entry.Key is IGraphElement)
                            HighlightSingleValue(entry.Key, name + ".Domain -> " + EmitHelper.ToString(entry.Value, shellProcEnv.ProcEnv.NamedGraph), addAnnotation);
                        if(entry.Value is IGraphElement)
                            HighlightSingleValue(entry.Value, EmitHelper.ToString(entry.Key, shellProcEnv.ProcEnv.NamedGraph) + " -> " + name + ".Range", addAnnotation);
                    }
                }
            }
        }

        void HighlightList(IList value, string name, bool addAnnotation)
        {
            for(int i=0; i<value.Count; ++i)
            {
                if(value[i] is IGraphElement)
                    HighlightSingleValue(value[i], name + "[" + i + "]", addAnnotation);
                if(value[i] is INode && i >= 1)
                {
                    if(addAnnotation)
                        ycompClient.AddEdge(name + i, name + "[->]", (INode)value[i-1], (INode)value[i]);
                    else
                        ycompClient.DeleteEdge(name + i);
                }
            }
        }

        void HighlightDeque(IDeque value, string name, bool addAnnotation)
        {
            int distanceToTop = 0;
            object prevElem = null;
            foreach(object elem in value)
            {
                if(elem is IGraphElement)
                    HighlightSingleValue(elem, name + "@" + distanceToTop, addAnnotation);
                if(elem is INode && distanceToTop >= 1)
                {
                    if(addAnnotation)
                        ycompClient.AddEdge(name + distanceToTop, name + "[->]", (INode)prevElem, (INode)elem);
                    else
                        ycompClient.DeleteEdge(name + distanceToTop);
                }
                prevElem = elem;
                ++distanceToTop;
            }
        }

        void HighlightMapping(INode source, INode target, string name, int cnt, bool addAnnotation)
        {
            HighlightSingleValue(source, name + ".Domain", addAnnotation);
            HighlightSingleValue(target, name + ".Range", addAnnotation);
            if(addAnnotation)
                ycompClient.AddEdge(name + cnt, name, source, target);
            else
                ycompClient.DeleteEdge(name + cnt);
        }

        void HighlightSingleValue(object value, string name, bool addAnnotation)
        {
            if(value is int)
            {
                List<int> allocatedVisitedFlags = shellProcEnv.ProcEnv.NamedGraph.GetAllocatedVisitedFlags();
                if(allocatedVisitedFlags.Contains((int)value))
                {
                    foreach(INode node in shellProcEnv.ProcEnv.NamedGraph.Nodes)
                        if(shellProcEnv.ProcEnv.NamedGraph.IsVisited(node, (int)value))
                            HighlightNode(node, "visited[" + name + "]", addAnnotation);
                    foreach(IEdge edge in shellProcEnv.ProcEnv.NamedGraph.Edges)
                        if(shellProcEnv.ProcEnv.NamedGraph.IsVisited(edge, (int)value))
                            HighlightEdge(edge, "visited[" + name + "]", addAnnotation);
                }
                else
                {
                    Console.WriteLine("Unknown visited flag id " + (int)(value) + "!");
                    if(name!=null)
                        Console.WriteLine("Which is contained in variable " + name + ".");
                    Console.WriteLine("Use (v)ariables to print variables and visited flags.");
                }
            }
            else if(value is IGraphElement)
            {
                if(value is INode)
                    HighlightNode((INode)value, name, addAnnotation);
                else //value is IEdge
                    HighlightEdge((IEdge)value, name, addAnnotation);
            }
            else
            {
                Console.WriteLine("The value " + value + (name!=null ? " contained in " + name : "") + " is neither an integer visited flag id nor a graph element, can't highlight!");
                Console.WriteLine("Use (v)ariables to print variables and visited flags.");
            }
        }

        private void HighlightNode(INode node, string name, bool addAnnotation)
        {
            if(addAnnotation)
            {
                if(ycompClient.dumpInfo.IsExcludedGraph())
                {
                    if(!excludedGraphNodesIncluded.ContainsKey(node))
                    {
                        ycompClient.AddNode(node);
                        excludedGraphNodesIncluded.Add(node, true);
                    }
                }

                ycompClient.ChangeNode(node, realizers.MatchedNodeRealizer);
                if(annotatedNodes.ContainsKey(node))
                    annotatedNodes[node] += ", " + name;
                else
                    annotatedNodes[node] = name;
            }
            else
            {
                ycompClient.ChangeNode(node, null);
                ycompClient.AnnotateElement(node, null);
                annotatedNodes.Remove(node);
            }
        }

        private void HighlightEdge(IEdge edge, string name, bool addAnnotation)
        {
            if(addAnnotation)
            {
                if(ycompClient.dumpInfo.IsExcludedGraph())
                {
                    if(!excludedGraphEdgesIncluded.ContainsKey(edge))
                    {
                        ycompClient.AddEdge(edge);
                        excludedGraphEdgesIncluded.Add(edge, true);
                    }
                }

                ycompClient.ChangeEdge(edge, realizers.MatchedEdgeRealizer);
                if(annotatedEdges.ContainsKey(edge))
                    annotatedEdges[edge] += ", " + name;
                else
                    annotatedEdges[edge] = name;
            }
            else
            {
                ycompClient.ChangeEdge(edge, null);
                ycompClient.AnnotateElement(edge, null);
                annotatedEdges.Remove(edge);
            }
        }

        void HandleStackTrace()
        {
            Console.WriteLine("Current sequence call stack is:");
            PrintSequenceContext contextTrace = new PrintSequenceContext(grShellImpl.Workaround);
            Sequence[] callStack = debugSequences.ToArray();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                contextTrace.highlightSeq = callStack[i].GetCurrentlyExecutedSequence();
                PrintSequence(callStack[i], contextTrace, callStack.Length - i);
                Console.WriteLine();
            }
            Console.WriteLine("continuing execution with:");
        }

        void HandleFullState()
        {
            Console.WriteLine("Current execution state is:");
            PrintVariables(null, null);
            PrintSequenceContext contextTrace = new PrintSequenceContext(grShellImpl.Workaround);
            Sequence[] callStack = debugSequences.ToArray();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                Sequence currSeq = callStack[i].GetCurrentlyExecutedSequence();
                contextTrace.highlightSeq = currSeq;
                PrintSequence(callStack[i], contextTrace, callStack.Length - i);
                Console.WriteLine();
                PrintVariables(callStack[i], currSeq != null ? currSeq : callStack[i]);
            }
            PrintVisited();
            Console.WriteLine("continuing execution with:");
        }

        #endregion Methods for directly handling user commands


        #region Choice/Breakpoint helpers

        int HandleTogglePoint(string pointName, int numPositions)
        {
            while(true)
            {
                Console.WriteLine("Which " + pointName + " to toggle (toggling on is shown by +, off by -)?");
                Console.WriteLine("Press (0)...(9) to toggle the corresponding " + pointName + " or (e) to enter the number of the " + pointName + " to toggle."
                                + " Press (a) to abort.");

                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch (key.KeyChar)
                {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    int num = key.KeyChar - '0';
                    if (num >= numPositions)
                    {
                        Console.WriteLine("You must specify a number between 0 and " + (numPositions - 1) + "!");
                        break;
                    }
                    return num;
                case 'e':
                    Console.Write("Enter number of " + pointName + " to toggle (-1 for abort): ");
                    String numStr = Console.ReadLine();
                    if (int.TryParse(numStr, out num))
                    {
                        if (num < -1 || num >= numPositions)
                        {
                            Console.WriteLine("You must specify a number between -1 and " + (numPositions - 1) + "!");
                            break;
                        }
                        return num;
                    }
                    Console.WriteLine("You must enter a valid integer number!");
                    break;
                case 'a':
                    return -1;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (a)bort allowed! ");
                    break;
                }
            }
        }

        void ToggleChoicepoint(Sequence seq, int cpPos)
        {
            int cpCounter = 0; // dummy
            SequenceRandomChoice cpSeq = GetSequenceAtChoicepointPosition(seq, cpPos, ref cpCounter);
            cpSeq.Choice = !cpSeq.Choice;
        }

        void ToggleBreakpoint(Sequence seq, int bpPos)
        {
            int bpCounter = 0; // dummy
            SequenceSpecial bpSeq = GetSequenceAtBreakpointPosition(seq, bpPos, ref bpCounter);
            bpSeq.Special = !bpSeq.Special;
        }

        SequenceSpecial GetSequenceAtBreakpointPosition(Sequence seq, int bpPos, ref int counter)
        {
            if (seq is SequenceSpecial)
            {
                if (counter == bpPos)
                    return (SequenceSpecial)seq;
                counter++;
            }
            foreach (Sequence child in seq.Children)
            {
                SequenceSpecial res = GetSequenceAtBreakpointPosition(child, bpPos, ref counter);
                if (res != null) return res;
            }
            return null;
        }

        SequenceRandomChoice GetSequenceAtChoicepointPosition(Sequence seq, int cpPos, ref int counter)
        {
            if (seq is SequenceRandomChoice && ((SequenceRandomChoice)seq).Random)
            {
                if (counter == cpPos)
                    return (SequenceRandomChoice)seq;
                counter++;
            }
            foreach (Sequence child in seq.Children)
            {
                SequenceRandomChoice res = GetSequenceAtChoicepointPosition(child, cpPos, ref counter);
                if (res != null) return res;
            }
            return null;
        }
        
        void TogglePointInAllInstances(int pos, bool choice)
        {
            if(debugSequences.Count > 1)
            {
                SequenceDefinitionInterpreted top = (SequenceDefinitionInterpreted)debugSequences.Peek();
                Sequence[] callStack = debugSequences.ToArray();
                for(int i = 0; i <= callStack.Length - 2; ++i) // non definition bottom excluded
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)callStack[i];
                    if(seqDef.SequenceName == top.SequenceName)
                    {
                        if(choice)
                            ToggleChoicepoint(seqDef, pos);
                        else
                            ToggleBreakpoint(seqDef, pos);
                    }
                }

                // additionally handle the internally cached sequences
                foreach(SequenceDefinitionInterpreted seqDef in top.CachedSequenceCopies)
                {
                    if(choice)
                        ToggleChoicepoint(seqDef, pos);
                    else
                        ToggleBreakpoint(seqDef, pos);
                }
            }
            else
            {
                if(choice)
                    ToggleChoicepoint(debugSequences.Peek(), pos);
                else
                    ToggleBreakpoint(debugSequences.Peek(), pos);
            }
        }

        #endregion Choice/Breakpoint helpers


        #region Print sequence and variables

        /// <summary>
        /// Prints the given root sequence adding parentheses if needed according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="context">The print context</param>
        /// <param name="nestingLevel">The level the sequence is nested in</param>
        private static void PrintSequence(Sequence seq, PrintSequenceContext context, int nestingLevel)
        {
            context.workaround.PrintHighlighted(nestingLevel + ">", HighlightingMode.SequenceStart);
            PrintSequence(seq, null, context);
        }

        /// <summary>
        /// Prints the given sequence adding parentheses if needed according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="parent">The parent of the sequence or null if the sequence is a root</param>
        /// <param name="context">The print context</param>
        private static void PrintSequence(Sequence seq, Sequence parent, PrintSequenceContext context)
        {
            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence) Console.Write("(");

            switch(seq.SequenceType)
            {
                // Binary
                case SequenceType.ThenLeft:
                case SequenceType.ThenRight:
                case SequenceType.LazyOr:
                case SequenceType.LazyAnd:
                case SequenceType.StrictOr:
                case SequenceType.Xor:
                case SequenceType.StrictAnd:
                    {
                        SequenceBinary seqBin = (SequenceBinary)seq;

                        if(context.cpPosCounter >= 0 && seqBin.Random)
                        {
                            int cpPosCounter = context.cpPosCounter;
                            ++context.cpPosCounter;
                            PrintSequence(seqBin.Left, seq, context);
                            PrintChoice(seqBin, context);
                            Console.Write(seq.Symbol + " ");
                            PrintSequence(seqBin.Right, seq, context);
                            break;
                        }

                        if(seqBin == context.highlightSeq && context.choice)
                        {
                            context.workaround.PrintHighlighted("(l)", HighlightingMode.Choicepoint);
                            PrintSequence(seqBin.Left, seq, context);
                            context.workaround.PrintHighlighted("(l) " + seq.Symbol + " (r)", HighlightingMode.Choicepoint);
                            PrintSequence(seqBin.Right, seq, context);
                            context.workaround.PrintHighlighted("(r)", HighlightingMode.Choicepoint);
                            break;
                        }

                        PrintSequence(seqBin.Left, seq, context);
                        Console.Write(" " + seq.Symbol + " ");
                        PrintSequence(seqBin.Right, seq, context);
                        break;
                    }
                case SequenceType.IfThen:
                    {
                        SequenceIfThen seqIfThen = (SequenceIfThen)seq;
                        Console.Write("if{");
                        PrintSequence(seqIfThen.Left, seq, context);
                        Console.Write(";");
                        PrintSequence(seqIfThen.Right, seq, context);
                        Console.Write("}");
                        break;
                    }

                // Unary
                case SequenceType.Not:
                    {
                        SequenceNot seqNot = (SequenceNot)seq;
                        Console.Write(seq.Symbol);
                        PrintSequence(seqNot.Seq, seq, context);
                        break;
                    }
                case SequenceType.IterationMin:
                    {
                        SequenceIterationMin seqMin = (SequenceIterationMin)seq;
                        PrintSequence(seqMin.Seq, seq, context);
                        Console.Write("[" + seqMin.Min + ":*]");
                        break;
                    }
                case SequenceType.IterationMinMax:
                    {
                        SequenceIterationMinMax seqMinMax = (SequenceIterationMinMax)seq;
                        PrintSequence(seqMinMax.Seq, seq, context);
                        Console.Write("[" + seqMinMax.Min + ":" + seqMinMax.Max + "]");
                        break;
                    }
                case SequenceType.Transaction:
                    {
                        SequenceTransaction seqTrans = (SequenceTransaction)seq;
                        Console.Write("<");
                        PrintSequence(seqTrans.Seq, seq, context);
                        Console.Write(">");
                        break;
                    }
                case SequenceType.Backtrack:
                    {
                        SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                        Console.Write("<<");
                        PrintSequence(seqBack.Rule, seq, context);
                        Console.Write(";;");
                        PrintSequence(seqBack.Seq, seq, context);
                        Console.Write(">>");
                        break;
                    }
                case SequenceType.Pause:
                    {
                        SequencePause seqPause = (SequencePause)seq;
                        Console.Write("/");
                        PrintSequence(seqPause.Seq, seq, context);
                        Console.Write("/");
                        break;
                    }
                case SequenceType.ForContainer:
                    {
                        SequenceForContainer seqFor = (SequenceForContainer)seq;
                        Console.Write("for{");
                        Console.Write(seqFor.Var.Name);
                        if(seqFor.VarDst != null) Console.Write("->" + seqFor.VarDst.Name);
                        Console.Write(" in " + seqFor.Container.Name);
                        Console.Write("; ");
                        PrintSequence(seqFor.Seq, seq, context);
                        Console.Write("}");
                        break;
                    }
                case SequenceType.ForIntegerRange:
                    {
                        SequenceForIntegerRange seqFor = (SequenceForIntegerRange)seq;
                        Console.Write("for{");
                        Console.Write(seqFor.Var.Name);
                        Console.Write(" in [");
                        Console.Write(seqFor.Left.Symbol);
                        Console.Write(":");
                        Console.Write(seqFor.Right.Symbol);
                        Console.Write("]; ");
                        PrintSequence(seqFor.Seq, seq, context);
                        Console.Write("}");
                        break;
                    }
                case SequenceType.ForIndexAccessEquality:
                    {
                        SequenceForIndexAccessEquality seqFor = (SequenceForIndexAccessEquality)seq;
                        Console.Write("for{");
                        Console.Write(seqFor.Var.Name);
                        Console.Write(" in {");
                        Console.Write(seqFor.IndexName);
                        Console.Write("==");
                        Console.Write(seqFor.Expr.Symbol);
                        Console.Write("}; ");
                        PrintSequence(seqFor.Seq, seq, context);
                        Console.Write("}");
                        break;
                    }
                case SequenceType.ForIndexAccessOrdering:
                    {
                        SequenceForIndexAccessOrdering seqFor = (SequenceForIndexAccessOrdering)seq;
                        Console.Write("for{");
                        Console.Write(seqFor.Var.Name);
                        Console.Write(" in {");
                        if(seqFor.Ascending)
                            Console.Write("ascending");
                        else
                            Console.Write("descending");
                        Console.Write("(");
                        if(seqFor.From() != null && seqFor.To() != null)
                        {
                            Console.Write(seqFor.IndexName);
                            Console.Write(seqFor.DirectionAsString(seqFor.Direction));
                            Console.Write(seqFor.Expr.Symbol);
                            Console.Write(",");
                            Console.Write(seqFor.IndexName);
                            Console.Write(seqFor.DirectionAsString(seqFor.Direction2));
                            Console.Write(seqFor.Expr2.Symbol);
                        }
                        else if(seqFor.From() != null)
                        {
                            Console.Write(seqFor.IndexName);
                            Console.Write(seqFor.DirectionAsString(seqFor.Direction));
                            Console.Write(seqFor.Expr.Symbol);
                        }
                        else if(seqFor.To() != null)
                        {
                            Console.Write(seqFor.IndexName);
                            Console.Write(seqFor.DirectionAsString(seqFor.Direction));
                            Console.Write(seqFor.Expr.Symbol);
                        }
                        else
                        {
                            Console.Write(seqFor.IndexName);
                        }
                        Console.Write(")");
                        Console.Write("}; ");
                        PrintSequence(seqFor.Seq, seq, context);
                        Console.Write("}");
                        break;
                    }
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
                case SequenceType.ForNodes:
                case SequenceType.ForEdges:
                    {
                        SequenceForFunction seqFor = (SequenceForFunction)seq;
                        Console.Write("for{");
                        Console.Write(seqFor.Var.Name);
                        Console.Write(" in ");
                        Console.Write(seqFor.FunctionSymbol + ";");
                        PrintSequence(seqFor.Seq, seq, context);
                        Console.Write("}");
                        break;
                    }
                case SequenceType.ForMatch:
                    {
                        SequenceForMatch seqFor = (SequenceForMatch)seq;
                        Console.Write("for{");
                        Console.Write(seqFor.Var.Name);
                        Console.Write(" in [?");
                        PrintSequence(seqFor.Rule, seq, context);
                        Console.Write("]; ");
                        PrintSequence(seqFor.Seq, seq, context);
                        Console.Write("}");
                        break;
                    }
                case SequenceType.ExecuteInSubgraph:
                    {
                        SequenceExecuteInSubgraph seqExecInSub = (SequenceExecuteInSubgraph)seq;
                        Console.Write("in ");
                        Console.Write(seqExecInSub.SubgraphVar.Name);
                        if(seqExecInSub.AttributeName != null)
                            Console.Write("." + seqExecInSub.AttributeName);
                        Console.Write(" {");
                        PrintSequence(seqExecInSub.Seq, seq, context);
                        Console.Write("}");
                        break;
                    }

                // Ternary
                case SequenceType.IfThenElse:
                    {
                        SequenceIfThenElse seqIf = (SequenceIfThenElse)seq;
                        Console.Write("if{");
                        PrintSequence(seqIf.Condition, seq, context);
                        Console.Write(";");
                        PrintSequence(seqIf.TrueCase, seq, context);
                        Console.Write(";");
                        PrintSequence(seqIf.FalseCase, seq, context);
                        Console.Write("}");
                        break;
                    }

                // n-ary
                case SequenceType.LazyOrAll:
                case SequenceType.LazyAndAll:
                case SequenceType.StrictOrAll:
                case SequenceType.StrictAndAll:
                    {
                        SequenceNAry seqN = (SequenceNAry)seq;

                        if(context.cpPosCounter >= 0)
                        {
                            PrintChoice(seqN, context);
                            ++context.cpPosCounter;
                            Console.Write((seqN.Choice ? "$%" : "$") + seqN.Symbol + "(");
                            bool first = true;
                            foreach(Sequence seqChild in seqN.Children)
                            {
                                if(!first) Console.Write(", ");
                                PrintSequence(seqChild, seqN, context);
                                first = false;
                            }
                            Console.Write(")");
                            break;
                        }

                        bool highlight = false;
                        foreach(Sequence seqChild in seqN.Children)
                            if(seqChild == context.highlightSeq)
                                highlight = true;
                        if(highlight && context.choice)
                        {
                            context.workaround.PrintHighlighted("$%" + seqN.Symbol + "(", HighlightingMode.Choicepoint);
                            bool first = true;
                            foreach(Sequence seqChild in seqN.Children)
                            {
                                if(!first) Console.Write(", ");
                                if(seqChild == context.highlightSeq)
                                    context.workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                                if(context.sequences != null)
                                {
                                    for(int i = 0; i < context.sequences.Count; ++i)
                                    {
                                        if(seqChild == context.sequences[i])
                                            context.workaround.PrintHighlighted("(" + i + ")", HighlightingMode.Choicepoint);
                                    }
                                }

                                Sequence highlightSeqBackup = context.highlightSeq;
                                context.highlightSeq = null; // we already highlighted here
                                PrintSequence(seqChild, seqN, context);
                                context.highlightSeq = highlightSeqBackup;

                                if(seqChild == context.highlightSeq)
                                    context.workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                                first = false;
                            }
                            context.workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                            break;
                        }

                        Console.Write((seqN.Choice ? "$%" : "$") + seqN.Symbol + "(");
                        PrintChildren(seqN, context);
                        Console.Write(")");
                        break;
                    }

                case SequenceType.WeightedOne:
                    {
                        SequenceWeightedOne seqWeighted = (SequenceWeightedOne)seq;

                        if(context.cpPosCounter >= 0)
                        {
                            PrintChoice(seqWeighted, context);
                            ++context.cpPosCounter;
                            Console.Write((seqWeighted.Choice ? "$%" : "$") + seqWeighted.Symbol + "(");
                            bool first = true;
                            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                            {
                                if(first) Console.Write("0.00 ");
                                else Console.Write(" ");
                                PrintSequence(seqWeighted.Sequences[i], seqWeighted, context);
                                Console.Write(" ");
                                Console.Write(seqWeighted.Numbers[i]); // todo: format auf 2 nachkommastellen 
                                first = false;
                            }
                            Console.Write(")");
                            break;
                        }

                        bool highlight = false;
                        foreach(Sequence seqChild in seqWeighted.Children)
                            if(seqChild == context.highlightSeq)
                                highlight = true;
                        if(highlight && context.choice)
                        {
                            context.workaround.PrintHighlighted("$%" + seqWeighted.Symbol + "(", HighlightingMode.Choicepoint);
                            bool first = true;
                            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                            {
                                if(first) Console.Write("0.00 ");
                                else Console.Write(" ");
                                if(seqWeighted.Sequences[i] == context.highlightSeq)
                                    context.workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);

                                Sequence highlightSeqBackup = context.highlightSeq;
                                context.highlightSeq = null; // we already highlighted here
                                PrintSequence(seqWeighted.Sequences[i], seqWeighted, context);
                                context.highlightSeq = highlightSeqBackup;

                                if(seqWeighted.Sequences[i] == context.highlightSeq)
                                    context.workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                                Console.Write(" ");
                                Console.Write(seqWeighted.Numbers[i]); // todo: format auf 2 nachkommastellen 
                                first = false;
                            }
                            context.workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                            break;
                        }

                        Console.Write((seqWeighted.Choice ? "$%" : "$") + seqWeighted.Symbol + "(");
                        bool ffs = true;
                        for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                        {
                            if(ffs) Console.Write("0.00 ");
                            else Console.Write(" ");
                            PrintSequence(seqWeighted.Sequences[i], seqWeighted, context);
                            Console.Write(" ");
                            Console.Write(seqWeighted.Numbers[i]); // todo: format auf 2 nachkommastellen 
                            ffs = false;
                        }
                        Console.Write(")");
                        break;
                    }

                case SequenceType.SomeFromSet:
                    {
                        SequenceSomeFromSet seqSome = (SequenceSomeFromSet)seq;

                        if(context.cpPosCounter >= 0
                            && seqSome.Random)
                        {
                            PrintChoice(seqSome, context);
                            ++context.cpPosCounter;
                            Console.Write(seqSome.Choice ? "$%{<" : "${<");
                            bool first = true;
                            foreach(Sequence seqChild in seqSome.Children)
                            {
                                if(!first) Console.Write(", ");
                                int cpPosCounterBackup = context.cpPosCounter;
                                context.cpPosCounter = -1; // rules within some-from-set are not choicepointable
                                PrintSequence(seqChild, seqSome, context);
                                context.cpPosCounter = cpPosCounterBackup;
                                first = false;
                            }
                            Console.Write(")}");
                            break;
                        }

                        bool highlight = false;
                        foreach(Sequence seqChild in seqSome.Children)
                            if(seqChild == context.highlightSeq)
                                highlight = true;

                        if(highlight && context.choice)
                        {
                            context.workaround.PrintHighlighted("$%{<", HighlightingMode.Choicepoint);
                            bool first = true;
                            int numCurTotalMatch = 0;
                            foreach(Sequence seqChild in seqSome.Children)
                            {
                                if(!first) Console.Write(", ");
                                if(seqChild == context.highlightSeq)
                                    context.workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                                if(context.sequences != null)
                                {
                                    for(int i = 0; i < context.sequences.Count; ++i)
                                    {
                                        if(seqChild == context.sequences[i] && context.matches[i].Count > 0)
                                        {
                                            PrintListOfMatchesNumbers(context, ref numCurTotalMatch, context.matches[i].Count);
                                        }
                                    }
                                }

                                Sequence highlightSeqBackup = context.highlightSeq;
                                context.highlightSeq = null; // we already highlighted here
                                PrintSequence(seqChild, seqSome, context);
                                context.highlightSeq = highlightSeqBackup;

                                if(seqChild == context.highlightSeq)
                                    context.workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                                first = false;
                            }
                            context.workaround.PrintHighlighted(">}", HighlightingMode.Choicepoint);
                            break;
                        }

                        bool succesBackup = context.success;
                        if(highlight) context.success = true;
                        Console.Write(seqSome.Random ? (seqSome.Choice ? "$%{<" : "${<") : "{<");
                        PrintChildren(seqSome, context);
                        Console.Write(">}");
                        context.success = succesBackup;
                        break;
                    }

                // Breakpointable atoms
                case SequenceType.SequenceCall:
                case SequenceType.RuleCall:
                case SequenceType.RuleAllCall:
                case SequenceType.RuleCountAllCall:
                case SequenceType.BooleanComputation:
                    {
                        if(context.bpPosCounter >= 0)
                        {
                            PrintBreak((SequenceSpecial)seq, context);
                            Console.Write(seq.Symbol);
                            ++context.bpPosCounter;
                            break;
                        }

                        if(context.cpPosCounter >= 0 && seq is SequenceRandomChoice
                            && ((SequenceRandomChoice)seq).Random)
                        {
                            PrintChoice((SequenceRandomChoice)seq, context);
                            Console.Write(seq.Symbol);
                            ++context.cpPosCounter;
                            break;
                        }

                        HighlightingMode mode = HighlightingMode.None;
                        if(seq == context.highlightSeq)
                        {
                            if(context.choice) mode |= HighlightingMode.Choicepoint;
                            else if(context.success) mode |= HighlightingMode.FocusSucces;
                            else mode |= HighlightingMode.Focus;
                        }
                        if(seq.ExecutionState == SequenceExecutionState.Success) mode |= HighlightingMode.LastSuccess;
                        if(seq.ExecutionState == SequenceExecutionState.Fail) mode |= HighlightingMode.LastFail;
                        if(context.sequences != null && context.sequences.Contains(seq))
                        {
                            if(context.matches != null && context.matches[context.sequences.IndexOf(seq)].Count > 0)
                                mode |= HighlightingMode.FocusSucces;
                        }
                        context.workaround.PrintHighlighted(seq.Symbol, mode);
                        break;
                    }

                // Unary assignment
                case SequenceType.AssignSequenceResultToVar:
                case SequenceType.OrAssignSequenceResultToVar:
                case SequenceType.AndAssignSequenceResultToVar:
                    {
                        SequenceAssignSequenceResultToVar seqAss = (SequenceAssignSequenceResultToVar)seq;
                        Console.Write("(");
                        PrintSequence(seqAss.Seq, seq, context);
                        if(seq.SequenceType == SequenceType.OrAssignSequenceResultToVar)
                            Console.Write("|>");
                        else if(seq.SequenceType == SequenceType.AndAssignSequenceResultToVar)
                            Console.Write("&>");
                        else //if(seq.SequenceType==SequenceType.AssignSequenceResultToVar)
                            Console.Write("=>");
                        Console.Write(seqAss.DestVar.Name);
                        Console.Write(")");
                        break;
                    }

                // Choice highlightable user assignments
                case SequenceType.AssignUserInputToVar:
                case SequenceType.AssignRandomIntToVar:
                case SequenceType.AssignRandomDoubleToVar:
                    {
                        if(context.cpPosCounter >= 0 
                            && (seq is SequenceAssignRandomIntToVar || seq is SequenceAssignRandomDoubleToVar))
                        {
                            PrintChoice((SequenceRandomChoice)seq, context);
                            Console.Write(seq.Symbol);
                            ++context.cpPosCounter;
                            break;
                        }

                        if(seq == context.highlightSeq && context.choice)
                            context.workaround.PrintHighlighted(seq.Symbol, HighlightingMode.Choicepoint);
                        else
                            Console.Write(seq.Symbol);
                        break;
                    }

                case SequenceType.SequenceDefinitionInterpreted:
                    {
                        SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seq;
                        HighlightingMode mode = HighlightingMode.None;
                        if(seqDef.ExecutionState == SequenceExecutionState.Success) mode = HighlightingMode.LastSuccess;
                        if(seqDef.ExecutionState == SequenceExecutionState.Fail) mode = HighlightingMode.LastFail;
                        context.workaround.PrintHighlighted(seqDef.Symbol + ": ", mode);
                        PrintSequence(seqDef.Seq, seqDef.Seq, context);
                        break;
                    }

                // Atoms (assignments)
                case SequenceType.AssignVarToVar:
                case SequenceType.AssignConstToVar:
                case SequenceType.AssignContainerConstructorToVar:
                case SequenceType.DeclareVariable:
                    {
                        Console.Write(seq.Symbol);
                        break;
                    }

                default:
                    {
                        Debug.Assert(false);
                        Console.Write("<UNKNOWN_SEQUENCE_TYPE>");
                        break;
                    }
            }

            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence) Console.Write(")");
        }

        static void PrintChildren(Sequence seq, PrintSequenceContext context)
        {
            bool first = true;
            foreach(Sequence seqChild in seq.Children)
            {
                if(!first) Console.Write(", ");
                PrintSequence(seqChild, seq, context);
                first = false;
            }
        }

        static void PrintChoice(SequenceRandomChoice seq, PrintSequenceContext context)
        {
            if(seq.Choice)
                context.workaround.PrintHighlighted("-%" + context.cpPosCounter + "-:", HighlightingMode.Choicepoint);
            else
                context.workaround.PrintHighlighted("+%" + context.cpPosCounter + "+:", HighlightingMode.Choicepoint);
        }

        static void PrintBreak(SequenceSpecial seq, PrintSequenceContext context)
        {
            if(seq.Special)
                context.workaround.PrintHighlighted("-%" + context.bpPosCounter + "-:", HighlightingMode.Breakpoint);
            else
                context.workaround.PrintHighlighted("+%" + context.bpPosCounter + "+:", HighlightingMode.Breakpoint);
        }

        static void PrintListOfMatchesNumbers(PrintSequenceContext context, ref int numCurTotalMatch, int numMatches)
        {
            context.workaround.PrintHighlighted("(", HighlightingMode.Choicepoint);
            bool first = true;
            for(int i = 0; i < numMatches; ++i)
            {
                if(!first) context.workaround.PrintHighlighted(",", HighlightingMode.Choicepoint);
                context.workaround.PrintHighlighted(numCurTotalMatch.ToString(), HighlightingMode.Choicepoint);
                ++numCurTotalMatch;
                first = false;
            }
            context.workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
        }

        /// <summary>
        /// Called from shell after an debugging abort highlighting the lastly executed rule
        /// </summary>
        public static void PrintSequence(Sequence seq, Sequence highlight, IWorkaround workaround)
        {
            PrintSequenceContext context = new PrintSequenceContext(workaround);
            context.highlightSeq = highlight;
            PrintSequence(seq, context, 0);
            // TODO: what to do if abort came within sequence called from top sequence?
        }

        void PrintVariables(Sequence seqStart, Sequence seq)
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

        void PrintVisited()
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

        #endregion Print sequence and variables


        #region Match marking and annotation in graph

        void Mark(int rule, int match, SequenceSomeFromSet seq)
        {
            if(seq.NonRandomAll(rule))
            {
                MarkMatches(seq.Matches[rule], realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                AnnotateMatches(seq.Matches[rule], true);
            }
            else
            {
                MarkMatch(seq.Matches[rule].GetMatch(match), realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                AnnotateMatch(seq.Matches[rule].GetMatch(match), true);
            }
        }

        void Unmark(int rule, int match, SequenceSomeFromSet seq)
        {
            if(seq.NonRandomAll(rule))
            {
                MarkMatches(seq.Matches[rule], null, null);
                AnnotateMatches(seq.Matches[rule], false);
            }
            else
            {
                MarkMatch(seq.Matches[rule].GetMatch(match), null, null);
                AnnotateMatch(seq.Matches[rule].GetMatch(match), false);
            }
        }

        private void MarkMatch(IMatch match, String nodeRealizerName, String edgeRealizerName)
        {
            foreach(INode node in match.Nodes)
            {
                ycompClient.ChangeNode(node, nodeRealizerName);
            }
            foreach(IEdge edge in match.Edges)
            {
                ycompClient.ChangeEdge(edge, edgeRealizerName);
            }
            MarkMatches(match.EmbeddedGraphs, nodeRealizerName, edgeRealizerName);
            foreach(IMatches iteratedsMatches in match.Iterateds)
                MarkMatches(iteratedsMatches, nodeRealizerName, edgeRealizerName);
            MarkMatches(match.Alternatives, nodeRealizerName, edgeRealizerName);
            MarkMatches(match.Independents, nodeRealizerName, edgeRealizerName);
        }

        private void MarkMatches(IEnumerable<IMatch> matches, String nodeRealizerName, String edgeRealizerName)
        {
            foreach(IMatch match in matches)
            {
                MarkMatch(match, nodeRealizerName, edgeRealizerName);
            }
        }

        private void AnnotateMatch(IMatch match, bool addAnnotation)
        {
            AnnotateMatch(match, addAnnotation, "", 0, true);
            if(addAnnotation)
            {
                foreach(KeyValuePair<INode, string> nodeToName in annotatedNodes)
                    ycompClient.AnnotateElement(nodeToName.Key, nodeToName.Value);
                foreach(KeyValuePair<IEdge, string> edgeToName in annotatedEdges)
                    ycompClient.AnnotateElement(edgeToName.Key, edgeToName.Value);
            }
        }

        private void AnnotateMatches(IEnumerable<IMatch> matches, bool addAnnotation)
        {
            AnnotateMatches(matches, addAnnotation, "", 0, true);
            if(addAnnotation)
            {
                foreach(KeyValuePair<INode, string> nodeToName in annotatedNodes)
                    ycompClient.AnnotateElement(nodeToName.Key, nodeToName.Value);
                foreach(KeyValuePair<IEdge, string> edgeToName in annotatedEdges)
                    ycompClient.AnnotateElement(edgeToName.Key, edgeToName.Value);
            }
        }

        private void AnnotateMatches(IEnumerable<IMatch> matches, bool addAnnotation, string prefix, int nestingLevel, bool topLevel)
        {
            foreach(IMatch match in matches)
            {
                AnnotateMatch(match, addAnnotation, prefix, nestingLevel, topLevel);
            }
        }

        private void AnnotateMatch(IMatch match, bool addAnnotation, string prefix, int nestingLevel, bool topLevel)
        {
            const int PATTERN_NESTING_DEPTH_FROM_WHICH_ON_TO_CLIP_PREFIX = 7;

            for(int i = 0; i < match.NumberOfNodes; ++i)
            {
                INode node = match.getNodeAt(i);
                IPatternNode patternNode = match.Pattern.Nodes[i];
                if(addAnnotation)
                {
                    if(patternNode.PointOfDefinition == match.Pattern
                        || patternNode.PointOfDefinition == null && topLevel)
                    {
                        String name = match.Pattern.Nodes[i].UnprefixedName;
                        if(nestingLevel > 0)
                        {
                            if(nestingLevel < PATTERN_NESTING_DEPTH_FROM_WHICH_ON_TO_CLIP_PREFIX) name = prefix + "/" + name;
                            else name = "/|...|=" + nestingLevel + "/" + name;
                        }
                        if(annotatedNodes.ContainsKey(node))
                            annotatedNodes[node] += ", " + name;
                        else
                            annotatedNodes[node] = name;
                    }
                }
                else
                {
                    ycompClient.AnnotateElement(node, null);
                    annotatedNodes.Remove(node);
                }
            }
            for(int i = 0; i < match.NumberOfEdges; ++i)
            {
                IEdge edge = match.getEdgeAt(i);
                IPatternEdge patternEdge = match.Pattern.Edges[i];
                if(addAnnotation)
                {
                    if(patternEdge.PointOfDefinition == match.Pattern
                        || patternEdge.PointOfDefinition == null && topLevel)
                    {
                        String name = match.Pattern.Edges[i].UnprefixedName;
                        if(nestingLevel > 0)
                        {
                            if(nestingLevel < PATTERN_NESTING_DEPTH_FROM_WHICH_ON_TO_CLIP_PREFIX) name = prefix + "/" + name;
                            else name = "/|...|=" + nestingLevel + "/" + name;
                        }
                        if(annotatedEdges.ContainsKey(edge))
                            annotatedEdges[edge] += ", " + name;
                        else
                            annotatedEdges[edge] = name;
                    }
                }
                else
                {
                    ycompClient.AnnotateElement(edge, null);
                    annotatedEdges.Remove(edge);
                }
            }
            AnnotateSubpatternMatches(match, addAnnotation, prefix, nestingLevel);
            AnnotateIteratedsMatches(match, addAnnotation, prefix, nestingLevel);
            AnnotateAlternativesMatches(match, addAnnotation, prefix, nestingLevel);
            AnnotateIndependentsMatches(match, addAnnotation, prefix, nestingLevel);
        }

        private void AnnotateSubpatternMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatch> matches = parentMatch.EmbeddedGraphs;
            int i = 0;
            foreach(IMatch match in matches)
            {
                AnnotateMatch(match, addAnnotation, prefix + "/" + pattern.EmbeddedGraphs[i].Name, nestingLevel + 1, true);
                ++i;
            }
        }

        private void AnnotateIteratedsMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatches> iteratedsMatches = parentMatch.Iterateds;
            int numIterated, numOptional, numMultiple, numOther;
            classifyIterateds(pattern, out numIterated, out numOptional, out numMultiple, out numOther);

            int i = 0;
            foreach(IMatches matches in iteratedsMatches)
            {
                String name;
                if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 0) {
                    name = "(.)*";
                    if(numIterated > 1) name += "'" + i;
                } else if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 1) {
                    name = "(.)?";
                    if(numOptional > 1) name += "'" + i;
                } else if(pattern.Iterateds[i].MinMatches == 1 && pattern.Iterateds[i].MaxMatches == 0) {
                    name = "(.)+";
                    if(numMultiple > 1) name += "'" + i;
                } else {
                    name = "(.)[" + pattern.Iterateds[i].MinMatches + ":" + pattern.Iterateds[i].MaxMatches + "]";
                    if(numOther > 1) name += "'" + i;
                }

                int j = 0;
                foreach(IMatch match in matches)
                {
                    AnnotateMatch(match, addAnnotation, prefix + "/" + name + "/" + j, nestingLevel + 1, false);
                    ++j;
                }

                ++i;
            }
        }

        private void AnnotateAlternativesMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatch> matches = parentMatch.Alternatives;
            int i = 0;
            foreach(IMatch match in matches)
            {
                String name = "(.|.)";
                if(pattern.Alternatives.Length>1) name += "'" + i;
                String caseName = match.Pattern.Name;
                AnnotateMatch(match, addAnnotation, prefix + "/" + name + "/" + caseName, nestingLevel + 1, false);
                ++i;
            }
        }

        private void AnnotateIndependentsMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatch> matches = parentMatch.Independents;
            int i = 0;
            foreach(IMatch match in matches)
            {
                String name = "&(.)";
                if(pattern.IndependentPatternGraphs.Length>1) name += "'" + i;
                AnnotateMatch(match, addAnnotation, prefix + "/" + name, nestingLevel + 1, false);
                ++i;
            }
        }

        private void classifyIterateds(IPatternGraph pattern, out int numIterated, out int numOptional, out int numMultiple, out int numOther)
        {
            numIterated = numOptional = numMultiple = numOther = 0;
            for(int i = 0; i < pattern.Iterateds.Length; ++i)
            {
                if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 0) ++numIterated;
                else if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 1) ++numOptional;
                else if(pattern.Iterateds[i].MinMatches == 1 && pattern.Iterateds[i].MaxMatches == 0) ++numMultiple;
                else ++numOther;
            }
        }

        #endregion Match marking and annotation in graph


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
            PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            context.workaround.PrintHighlighted("Please choose: Which branch to execute first?", HighlightingMode.Choicepoint);
            Console.Write(" (l)eft or (r)ight or (s)/(n) to continue with random choice?  (Random has chosen " + (direction == 0 ? "(l)eft" : "(r)ight") + ") ");
            while(true)
            {
                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case 'l':
                        Console.WriteLine();
                        return 0;
                    case 'r':
                        Console.WriteLine();
                        return 1;
                    case 's':
                    case 'n':
                        Console.WriteLine();
                        return direction;
                    default:
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (l)eft branch, (r)ight branch, (s)/(n) to continue allowed! ");
                        break;
                }
            }
        }

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        public int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            context.workaround.PrintHighlighted("Please choose: Which sequence to execute?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Pre-selecting sequence " + seqToExecute + " chosen by random.");
            Console.WriteLine("Press (0)...(9) to pre-select the corresponding sequence or (e) to enter the number of the sequence to show."
                                + " Press (s) or (n) to commit to the pre-selected sequence and continue."
                                + " Pressing (u) or (o) works like (s)/(n) but does not ask for the remaining contained sequences.");

            while(true)
            {
                context.highlightSeq = sequences[seqToExecute];
                context.choice = true;
                context.sequences = sequences;
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                context.choice = false;
                context.sequences = null;

                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        int num = key.KeyChar - '0';
                        if(num >= sequences.Count)
                        {
                            Console.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        break;
                    case 'e':
                        Console.Write("Enter number of sequence to show: ");
                        String numStr = Console.ReadLine();
                        if(int.TryParse(numStr, out num))
                        {
                            if(num < 0 || num >= sequences.Count)
                            {
                                Console.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                                break;
                            }
                            seqToExecute = num;
                            break;
                        }
                        Console.WriteLine("You must enter a valid integer number!");
                        break;
                    case 's':
                    case 'n':
                        return seqToExecute;
                    case 'u':
                    case 'o':
                        seq.Skip = true; // skip remaining rules (reset after exection of seq)
                        return seqToExecute;
                    default:
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(9), (e)nter number, (s)/(n) to commit and continue, (u)/(o) to commit and skip remaining choices allowed! ");
                        break;
                }
            }
        }

        /// <summary>
        /// returns the maybe user altered point within the interval series, denoting the sequence to execute next
        /// the randomly chosen point is supplied; the sequence with the intervals and their corresponding sequences is supplied
        /// </summary>
        public double ChoosePoint(double pointToExecute, SequenceWeightedOne seq)
        {
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            context.workaround.PrintHighlighted("Please choose: Which point in the interval series (corresponding to a sequence) to execute?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Pre-selecting point " + pointToExecute + " chosen by random.");
            Console.WriteLine("Press (e) to enter a point in the interval series of the sequence to show."
                                + " Press (s) or (n) to commit to the pre-selected sequence and continue.");

            while(true)
            {
                context.highlightSeq = seq.Sequences[seq.GetSequenceFromPoint(pointToExecute)];
                context.choice = true;
                context.sequences = seq.Sequences;
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                context.choice = false;
                context.sequences = null;

                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case 'e':
                        double num;
                        Console.Write("Enter point in interval series of sequence to show: ");
                        String numStr = Console.ReadLine();
                        if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out num))
                        {
                            if(num < 0.0 || num > seq.Numbers[seq.Numbers.Count - 1])
                            {
                                Console.WriteLine("You must specify a floating point number between 0.0 and " + seq.Numbers[seq.Numbers.Count - 1] + "!");
                                break;
                            }
                            pointToExecute = num;
                            break;
                        }
                        Console.WriteLine("You must enter a valid floating point number!");
                        break;
                    case 's':
                    case 'n':
                        return pointToExecute;
                    default:
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (e)nter number and (s)/(n) to commit and continue allowed! ");
                        break;
                }
            }
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

            context.workaround.PrintHighlighted("Please choose: Which match to execute?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Pre-selecting match " + totalMatchToExecute + " chosen by random.");
            Console.WriteLine("Press (0)...(9) to pre-select the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) to commit to the pre-selected match and continue.");

            while(true)
            {
                int rule; int match;
                seq.FromTotalMatch(totalMatchToExecute, out rule, out match);
                Mark(rule, match, seq);
                ycompClient.UpdateDisplay();
                ycompClient.Sync();

                context.highlightSeq = seq.Sequences[rule];
                context.choice = true;
                context.sequences = seq.Sequences;
                context.matches = seq.Matches;
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                context.choice = false;
                context.sequences = null;
                context.matches = null;

                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        int num = key.KeyChar - '0';
                        if(num >= seq.NumTotalMatches)
                        {
                            Console.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                            break;
                        }
                        Unmark(rule, match, seq);
                        totalMatchToExecute = num;
                        break;
                    case 'e':
                        Console.Write("Enter number of rule to show: ");
                        String numStr = Console.ReadLine();
                        if(int.TryParse(numStr, out num))
                        {
                            if(num < 0 || num >= seq.NumTotalMatches)
                            {
                                Console.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                                break;
                            }
                            Unmark(rule, match, seq);
                            totalMatchToExecute = num;
                            break;
                        }
                        Console.WriteLine("You must enter a valid integer number!");
                        break;
                    case 's':
                    case 'n':
                        Unmark(rule, match, seq);
                        return totalMatchToExecute;
                    default:
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(9), (e)nter number, (s)/(n) to commit and continue allowed! ");
                        break;
                }
            }
        }

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        public int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq)
        {
            context.highlightSeq = seq;
            context.choice = true;
            PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            if(matches.Count <= 1 + numFurtherMatchesToApply && lazyChoice)
            {
                context.workaround.PrintHighlighted("Skipping choicepoint ", HighlightingMode.Choicepoint);
                Console.WriteLine("as no choice needed (use the (l) command to toggle this behaviour).");
                return matchToApply;
            }

            context.workaround.PrintHighlighted("Please choose: Which match to apply?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Showing the match chosen by random. (" + numFurtherMatchesToApply + " following)");
            Console.WriteLine("Press (0)...(9) to show the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) to commit to the currently shown match and continue.");

            if(detailedMode)
            {
                MarkMatches(matches, null, null);
                AnnotateMatches(matches, false);
            }
            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            int newMatchToRewrite = matchToApply;
            while(true)
            {
                MarkMatch(matches.GetMatch(matchToApply), null, null);
                AnnotateMatch(matches.GetMatch(matchToApply), false);
                matchToApply = newMatchToRewrite;
                MarkMatch(matches.GetMatch(matchToApply), realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                AnnotateMatch(matches.GetMatch(matchToApply), true);
                ycompClient.UpdateDisplay();
                ycompClient.Sync();

                Console.WriteLine("Showing match " + matchToApply + " (of " + matches.Count + " matches available)");

                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        int num = key.KeyChar - '0';
                        if(num >= matches.Count)
                        {
                            Console.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                            break;
                        }
                        newMatchToRewrite = num;
                        break;
                    case 'e':
                        Console.Write("Enter number of match to show: ");
                        String numStr = Console.ReadLine();
                        if(int.TryParse(numStr, out num))
                        {
                            if(num < 0 || num >= matches.Count)
                            {
                                Console.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                                break;
                            }
                            newMatchToRewrite = num;
                            break;
                        }
                        Console.WriteLine("You must enter a valid integer number!");
                        break;
                    case 's':
                    case 'n':
                        MarkMatch(matches.GetMatch(matchToApply), null, null);
                        AnnotateMatch(matches.GetMatch(matchToApply), false);
                        ycompClient.UpdateDisplay();
                        ycompClient.Sync();
                        return matchToApply;
                    default:
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(9), (e)nter number, (s)/(n) to commit and continue allowed! ");
                        break;
                }
            }
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
            PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            while(true)
            {
                Console.Write("Enter number in range [0.." + upperBound + "[ or press enter to use " + randomNumber + ": ");
                String numStr = Console.ReadLine();
                if(numStr == "")
                    return randomNumber;
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < 0 || num >= upperBound)
                    {
                        Console.WriteLine("You must specify a number between 0 and " + (upperBound - 1) + "!");
                        continue;
                    }
                    return num;
                }
                Console.WriteLine("You must enter a valid integer number!");
            }
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
            PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            while(true)
            {
                Console.Write("Enter number in range [0.0 .. 1.0[ or press enter to use " + randomNumber + ": ");
                String numStr = Console.ReadLine();
                if(numStr == "")
                    return randomNumber;
                double num;
                if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out num))
                {
                    if(num < 0.0 || num >= 1.0)
                    {
                        Console.WriteLine("You must specify a number between 0.0 and 1.0 exclusive !");
                        continue;
                    }
                    return num;
                }
                Console.WriteLine("You must enter a valid double number!");
            }
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
            PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
            Console.WriteLine();
            context.choice = false;

            object value = grShellImpl.Askfor(type);

            while(value == null)
            {
                Console.Write("How to proceed? (a)bort user choice (-> value null) or (r)etry:");
                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                    case 'a':
                        Console.WriteLine();
                        return null;
                    case 'r':
                        Console.WriteLine();
                        value = grShellImpl.Askfor(type);
                        break;
                    default:
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (a)bort user choice or (r)etry allowed! ");
                        break;
                }
            }

            return value;
        }

        #endregion Possible user choices during sequence execution


        #region Partial graph adding on matches for excluded graph debugging

        private void AddNeededGraphElements(IMatch match)
        {
            foreach(INode node in match.Nodes)
            {
                if(!excludedGraphNodesIncluded.ContainsKey(node))
                {
                    excludedGraphNodesIncluded.Add(node, true);
                    ycompClient.AddNode(node);
                }
            }
            foreach(IEdge edge in match.Edges)
            {
                if(!excludedGraphEdgesIncluded.ContainsKey(edge))
                {
                    excludedGraphEdgesIncluded.Add(edge, true);
                    ycompClient.AddEdge(edge);
                }
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

        private void AddNeighboursAndParentsOfNeededGraphElements()
        {
            // add all neighbours of elements to graph and excludedGraphElementsIncluded (1-level direct context by default, maybe overriden by user)
            Set<INode> nodesIncluded = new Set<INode>(); // second variable needed to prevent disturbing iteration
            foreach(INode node in excludedGraphNodesIncluded.Keys)
                nodesIncluded.Add(node);
            for(int i = 0; i < ycompClient.dumpInfo.GetExcludeGraphContextDepth(); ++i)
                AddDirectNeighboursOfNeededGraphElements(nodesIncluded);

            // add all parents of elements to graph and excludedGraphElementsIncluded (n-level nesting)
            AddParentsOfNeededGraphElements(nodesIncluded);
        }

        private void AddDirectNeighboursOfNeededGraphElements(Set<INode> nodesIncluded)
        {
            foreach(INode node in nodesIncluded)
            {
                foreach(IEdge edge in node.Incident)
                {
                    if(!excludedGraphNodesIncluded.ContainsKey(edge.Opposite(node)))
                    {
                        excludedGraphNodesIncluded.Add(edge.Opposite(node), true);
                        ycompClient.AddNode(edge.Opposite(node));
                    }
                    if(!excludedGraphEdgesIncluded.ContainsKey(edge))
                    {
                        excludedGraphEdgesIncluded.Add(edge, true);
                        ycompClient.AddEdge(edge);
                    }
                }
            }
            foreach(INode node in excludedGraphNodesIncluded.Keys)
                if(!nodesIncluded.Contains(node))
                    nodesIncluded.Add(node);
        }

        private void AddParentsOfNeededGraphElements(Set<INode> latelyAddedNodes)
        {
            Set<INode> newlyAddedNodes = new Set<INode>();

            // wavefront algorithm, in the following step all nodes added by the previous step are inspected,
            // until the wave collapses cause no not already added node is added any more
            while(latelyAddedNodes.Count > 0)
            {
                foreach(INode node in latelyAddedNodes)
                {
                    bool parentFound = false;
                    foreach(GroupNodeType groupNodeType in ycompClient.dumpInfo.GroupNodeTypes)
                    {
                        foreach(IEdge edge in node.Incoming)
                        {
                            INode parent = edge.Source;
                            if(!groupNodeType.NodeType.IsMyType(parent.Type.TypeID)) continue;
                            GroupMode grpMode = groupNodeType.GetEdgeGroupMode(edge.Type, node.Type);
                            if((grpMode & GroupMode.GroupOutgoingNodes) == 0) continue;
                            if(!excludedGraphNodesIncluded.ContainsKey(parent))
                            {
                                newlyAddedNodes.Add(parent);
                                ycompClient.AddNode(parent);
                                excludedGraphNodesIncluded.Add(parent, true);
                                ycompClient.AddEdge(edge);
                                if(!excludedGraphEdgesIncluded.ContainsKey(edge))
                                    excludedGraphEdgesIncluded.Add(edge, true);
                            }
                            parentFound = true;
                        }
                        if(parentFound)
                            break;
                        foreach(IEdge edge in node.Outgoing)
                        {
                            INode parent = edge.Target;
                            if(!groupNodeType.NodeType.IsMyType(parent.Type.TypeID)) continue;
                            GroupMode grpMode = groupNodeType.GetEdgeGroupMode(edge.Type, node.Type);
                            if((grpMode & GroupMode.GroupIncomingNodes) == 0) continue;
                            if(!excludedGraphNodesIncluded.ContainsKey(parent))
                            {
                                newlyAddedNodes.Add(parent);
                                ycompClient.AddNode(parent);
                                excludedGraphNodesIncluded.Add(parent, true);
                                ycompClient.AddEdge(edge);
                                if(!excludedGraphEdgesIncluded.ContainsKey(edge))
                                    excludedGraphEdgesIncluded.Add(edge, true);
                            }
                            parentFound = true;
                        }
                        if(parentFound)
                            break;
                    }
                }
                Set<INode> tmp = latelyAddedNodes;
                latelyAddedNodes = newlyAddedNodes;
                newlyAddedNodes = tmp;
                newlyAddedNodes.Clear();
            }
        }

        #endregion Partial graph adding on matches for excluded graph debugging


        #region Event Handling

        void DebugNodeAdded(INode node)
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
                addedNodes[node] = true;
                ycompClient.AnnotateElement(node, curAddedNodeNames[nextAddedNodeIndex++]);
            }
            else if(alwaysShow) ycompClient.UpdateDisplay();
        }

        void DebugEdgeAdded(IEdge edge)
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
                addedEdges[edge] = true;
                ycompClient.AnnotateElement(edge, curAddedEdgeNames[nextAddedEdgeIndex++]);
            }
            else if(alwaysShow) ycompClient.UpdateDisplay();
        }

        void DebugDeletingNode(INode node)
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
                if(alwaysShow) ycompClient.UpdateDisplay();
            }
            else
            {
                annotatedNodes.Remove(node);
                ycompClient.ChangeNode(node, realizers.DeletedNodeRealizer);

                String name = ycompClient.Graph.GetElementName(node);
                ycompClient.RenameNode(name, "zombie_" + name);
                deletedNodes.Add("zombie_" + name);
            }
        }

        void DebugDeletingEdge(IEdge edge)
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
                if(alwaysShow) ycompClient.UpdateDisplay();
            }
            else
            {
                annotatedEdges.Remove(edge);
                ycompClient.ChangeEdge(edge, realizers.DeletedEdgeRealizer);

                String name = ycompClient.Graph.GetElementName(edge);
                ycompClient.RenameEdge(name, "zombie_" + name);
                deletedEdges.Add("zombie_" + name);
            }
        }

        void DebugClearingGraph()
        {
            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            ycompClient.ClearGraph();
        }

        void DebugChangingNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            ycompClient.ChangeNodeAttribute(node, attrType, changeType, newValue, keyValue);
        }

        void DebugChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            ycompClient.ChangeEdgeAttribute(edge, attrType, changeType, newValue, keyValue);
        }

        void DebugChangedNodeAttribute(INode node, AttributeType attrType)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.SetAttributes,
                node, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, node, attrType.Name);
        }

        void DebugChangedEdgeAttribute(IEdge edge, AttributeType attrType)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.SetAttributes,
                edge, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, edge, attrType.Name);
        }

        void DebugRetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
            SubruleDebuggingConfigurationRule cr;
            if(shellProcEnv.SubruleDebugConfig.Decide(SubruleDebuggingEvent.Retype, 
                oldElem, shellProcEnv.ProcEnv, out cr) == SubruleDebuggingDecision.Break)
                InternalHalt(cr, oldElem);

            if(ycompClient.dumpInfo.IsExcludedGraph() && !recordMode)
                return;
            
            ycompClient.RetypingElement(oldElem, newElem);
            if(!recordMode) return;

            if(oldElem is INode)
            {
                INode oldNode = (INode) oldElem;
                INode newNode = (INode) newElem;
                String name;
                if(annotatedNodes.TryGetValue(oldNode, out name))
                {
                    annotatedNodes.Remove(oldNode);
                    annotatedNodes[newNode] = name;
                    ycompClient.AnnotateElement(newElem, name);
                }
                ycompClient.ChangeNode(newNode, realizers.RetypedNodeRealizer);
                retypedNodes[newNode] = true;
            }
            else
            {
                IEdge oldEdge = (IEdge) oldElem;
                IEdge newEdge = (IEdge) newElem;
                String name;
                if(annotatedEdges.TryGetValue(oldEdge, out name))
                {
                    annotatedEdges.Remove(oldEdge);
                    annotatedEdges[newEdge] = name;
                    ycompClient.AnnotateElement(newElem, name);
                }
                ycompClient.ChangeEdge(newEdge, realizers.RetypedEdgeRealizer);
                retypedEdges[newEdge] = true;
            }
        }

        void DebugSettingAddedNodeNames(string[] namesOfNodesAdded)
        {
            curAddedNodeNames = namesOfNodesAdded;
            nextAddedNodeIndex = 0;
        }

        void DebugSettingAddedEdgeNames(string[] namesOfEdgesAdded)
        {
            curAddedEdgeNames = namesOfEdgesAdded;
            nextAddedEdgeIndex = 0;
        }

        void DebugMatched(IMatches matches, IMatch match, bool special)
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
                    annotatedNodes.Clear();
                    annotatedEdges.Clear();
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
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
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
                    annotatedNodes.Clear();
                    annotatedEdges.Clear();
                    return;
                }
            }

            if(matchDepth++ > 0 || computationsEnteredStack.Count > 0)
                Console.WriteLine("Matched " + matches.Producer.Name);

            annotatedNodes.Clear();
            annotatedEdges.Clear();

            curRulePattern = matches.Producer.RulePattern;

            if(ycompClient.dumpInfo.IsExcludedGraph())
            {
                if(!recordMode)
                {
                    ycompClient.ClearGraph();
                    excludedGraphNodesIncluded.Clear();
                    excludedGraphEdgesIncluded.Clear();
                }

                // add all elements from match to graph and excludedGraphElementsIncluded
                if(match != null)
                    AddNeededGraphElements(match);
                else
                    AddNeededGraphElements(matches);

                AddNeighboursAndParentsOfNeededGraphElements();
            }

            if(match!=null)
                MarkMatch(match, realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
            else
                MarkMatches(matches, realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
            if(match!=null)
                AnnotateMatch(match, true);
            else
                AnnotateMatches(matches, true);

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to apply rewrite...");
            ReadKeyWithCancel();

            if(match!=null)
                MarkMatch(match, null, null);
            else
                MarkMatches(matches, null, null);

            recordMode = true;
            ycompClient.NodeRealizerOverride = realizers.NewNodeRealizer;
            ycompClient.EdgeRealizerOverride = realizers.NewEdgeRealizer;
            nextAddedNodeIndex = 0;
            nextAddedEdgeIndex = 0;
        }

        void DebugNextMatch()
        {
            nextAddedNodeIndex = 0;
            nextAddedEdgeIndex = 0;
        }

        void DebugFinished(IMatches matches, bool special)
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

            foreach(INode node in addedNodes.Keys)
            {
                ycompClient.ChangeNode(node, null);
                ycompClient.AnnotateElement(node, null);
            }
            foreach(IEdge edge in addedEdges.Keys)
            {
                ycompClient.ChangeEdge(edge, null);
                ycompClient.AnnotateElement(edge, null);
            }

            foreach(String edgeName in deletedEdges)
                ycompClient.DeleteEdge(edgeName);
            foreach(String nodeName in deletedNodes)
                ycompClient.DeleteNode(nodeName);

            foreach(INode node in retypedNodes.Keys)
                ycompClient.ChangeNode(node, null);
            foreach(IEdge edge in retypedEdges.Keys)
                ycompClient.ChangeEdge(edge, null);

            foreach(INode node in annotatedNodes.Keys)
                ycompClient.AnnotateElement(node, null);
            foreach(IEdge edge in annotatedEdges.Keys)
                ycompClient.AnnotateElement(edge, null);

            ycompClient.NodeRealizerOverride = null;
            ycompClient.EdgeRealizerOverride = null;

            addedNodes.Clear();
            addedEdges.Clear();
            deletedEdges.Clear();
            deletedNodes.Clear();
            recordMode = false;
            matchDepth--;
        }

        void DebugEnteringSequence(Sequence seq)
        {
            // root node of sequence entered and interactive debugging activated
            if(stepMode && lastlyEntered == null)
            {
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                context.workaround.PrintHighlighted("Debug started", HighlightingMode.SequenceStart);
                Console.WriteLine(" -- available commands are: (n)ext match, (d)etailed step, (s)tep, step (u)p, step (o)ut of loop, (r)un, toggle (b)reakpoints, toggle (c)hoicepoints, toggle (l)azy choice, (w)atchpoints, show (v)ariables, print stack(t)race, (f)ull state, (h)ighlight, dum(p) graph, as (g)raph, and (a)bort (plus Ctrl+C for forced abort).");
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
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                QueryUser(seq);
                return;
            }
        }

        void DebugExitingSequence(Sequence seq)
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
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                context.workaround.PrintHighlighted("< leaving", HighlightingMode.SequenceStart);
                Console.WriteLine();
            }
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
        void DebugEndOfIteration(bool continueLoop, Sequence seq)
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
                        if(continueLoop) text = "Backtracking ";
                        else text = "Backtracking possibilities exhausted, fail ";
                    context.workaround.PrintHighlighted(text, HighlightingMode.SequenceStart);
                    context.highlightSeq = seq;
                    PrintSequence(seq, context, debugSequences.Count);
                    if(!continueLoop)
                        context.workaround.PrintHighlighted("< leaving backtracking brackets", HighlightingMode.SequenceStart);
                }
                else if(seq is SequenceDefinition)
                {
                    SequenceDefinition seqDef = (SequenceDefinition)seq;
                    context.workaround.PrintHighlighted("State at end of sequence call ", HighlightingMode.SequenceStart);
                    context.highlightSeq = seq;
                    PrintSequence(seq, context, debugSequences.Count);
                    context.workaround.PrintHighlighted("< leaving", HighlightingMode.SequenceStart);
                }
                else
                {
                    context.workaround.PrintHighlighted("State at end of iteration step ", HighlightingMode.SequenceStart);
                    context.highlightSeq = seq;
                    PrintSequence(seq, context, debugSequences.Count);
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
        void DebugSwitchToGraph(IGraph newGraph)
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
        void DebugReturnedFromGraph(IGraph oldGraph)
        {
            UnregisterLibGrEvents((INamedGraph)oldGraph);
            context.workaround.PrintHighlighted("...leaving graph\n", HighlightingMode.SequenceStart);
            ycompClient.ClearGraph();
            ycompClient.Graph = shellProcEnv.ProcEnv.NamedGraph;
            if(!ycompClient.dumpInfo.IsExcludedGraph())
                UploadGraph(shellProcEnv.ProcEnv.NamedGraph);
            RegisterLibGrEvents(shellProcEnv.ProcEnv.NamedGraph);
        }

        void DebugEnter(string message, params object[] values)
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

        void DebugExit(string message, params object[] values)
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

        void RemoveUpToEntryForExit(string message)
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

        void DebugEmit(string message, params object[] values)
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

        void DebugHalt(string message, params object[] values)
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
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                PrintDebugTracesStack(false);
            }

            QueryContinueOrTrace(true);
        }

        void InternalHalt(SubruleDebuggingConfigurationRule cr, object data, params object[] additionalData)
        {
            context.workaround.PrintHighlighted("Break ", HighlightingMode.Breakpoint);
            Console.WriteLine("because " + cr.ToString(data, shellProcEnv.ProcEnv.NamedGraph, additionalData));

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            if(!detailedMode)
            {
                context.highlightSeq = lastlyEntered;
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                PrintDebugTracesStack(false);
            }

            QueryContinueOrTrace(true);
        }

        /// <summary>
        /// highlights the values in the graphs if debugging is active (annotating them with the source names)
        /// </summary>
        void DebugHighlight(string message, List<object> values, List<string> sourceNames)
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
                PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                Console.WriteLine();
                PrintDebugTracesStack(false);
            }

            ShellProcEnv.ProcEnv.HighlightingUnderway = true;
            HandleHighlight(values, sourceNames);
            ShellProcEnv.ProcEnv.HighlightingUnderway = false;

            QueryContinueOrTrace(true);
        }

        void PrintDebugTracesStack(bool full)
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
            while(true)
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
                    if(computationsEnteredStack.Count > 0)
                        Console.Write("print subrule stack(t)race, (f)ull state, or (a)bort, any other key continues ");
                    else
                        Console.Write("(f)ull state, or (a)bort, any other key continues ");
                    if(!isBottomUpBreak)
                        Console.WriteLine("detailed debugging.");
                    else
                        Console.WriteLine("debugging as before.");
                }
                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'a':
                    grShellImpl.Cancel();
                    return;                               // never reached
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
                        PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                        Console.WriteLine();
                        PrintDebugTracesStack(true);
                        break;
                    }
                    else
                        return;
                case 'f':
                    HandleFullState();
                    PrintSequence(debugSequences.Peek(), context, debugSequences.Count);
                    Console.WriteLine();
                    PrintDebugTracesStack(true);
                    break;
                default:
                    return;
                }
            }
        }

        int TargetStackLevelForUpInDetailedMode()
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

        int TargetStackLevelForOutInDetailedMode()
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

        void DebugOnConnectionLost()
        {
            Console.WriteLine("Connection to yComp lost!");
            grShellImpl.Cancel();
        }

        /// <summary>
        /// Registers event handlers for needed LibGr events
        /// </summary>
        void RegisterLibGrEvents(INamedGraph graph)
        {
            graph.OnNodeAdded += DebugNodeAdded;
            graph.OnEdgeAdded += DebugEdgeAdded;
            graph.OnRemovingNode += DebugDeletingNode;
            graph.OnRemovingEdge += DebugDeletingEdge;
            graph.OnClearingGraph += DebugClearingGraph;
            graph.OnChangingNodeAttribute += DebugChangingNodeAttribute;
            graph.OnChangingEdgeAttribute += DebugChangingEdgeAttribute;
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
        void UnregisterLibGrEvents(INamedGraph graph)
        {
            graph.OnNodeAdded -= DebugNodeAdded;
            graph.OnEdgeAdded -= DebugEdgeAdded;
            graph.OnRemovingNode -= DebugDeletingNode;
            graph.OnRemovingEdge -= DebugDeletingEdge;
            graph.OnClearingGraph -= DebugClearingGraph;
            graph.OnChangingNodeAttribute -= DebugChangingNodeAttribute;
            graph.OnChangingEdgeAttribute -= DebugChangingEdgeAttribute;
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
