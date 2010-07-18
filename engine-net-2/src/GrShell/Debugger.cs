/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    public class PrintSequenceContext
    {
        /// <summary>
        /// The workaround for printing highlighted
        /// </summary>
        public IWorkaround workaround;

        /// <summary>
        /// A counter increased for every potential breakpoint position and printed next to a potential breakpoint.
        /// If bpPosCounter is smaller than zero, no such counter is used or printed.
        /// If bpPosCounter is greater than or equal zero, the following highlighting values are irrelvant.
        /// </summary>
        public int bpPosCounter = -1;

        /// <summary>
        /// A counter increased for every potential choice position and printed next to a potential choicepoint.
        /// If cpPosCounter is smaller than zero, no such counter is used or printed.
        /// If cpPosCounter is greater than or equal zero, the following highlighting values are irrelvant.
        /// </summary>
        public int cpPosCounter = -1;

        /// <summary> The sequence to be highlighted or null </summary>
        public Sequence highlightSeq;
        /// <summary> The sequence to be highlighted was already successfully matched? </summary>
        public bool success;
        /// <summary> The sequence to be highlighted requires a direction choice? </summary>
        public bool choice;
        /// <summary> The last successfully processed rule or null </summary>
        public Sequence lastSuccessSeq;
        /// <summary> The last unsuccessfully processed rules (did not match) or null </summary>
        public Dictionary<Sequence, bool> lastFailSeq;

        public PrintSequenceContext(IWorkaround workaround)
        {
            lastFailSeq = new Dictionary<Sequence, bool>();
            Init(workaround);
        }

        public void Init(IWorkaround workaround)
        {
            this.workaround = workaround;

            bpPosCounter = -1;

            cpPosCounter = -1;

            highlightSeq = null;
            success = false;
            choice = false;
            lastSuccessSeq = null;
            lastFailSeq.Clear();
        }
    }

    class Debugger : SequenceExecutionEnvironment
    {
        GrShellImpl grShellImpl;
        ShellGraph shellGraph;

        Process viewerProcess = null;
        YCompClient ycompClient = null;
        Sequence debugSequence = null;
        bool stepMode = true;
        bool dynamicStepMode = false;
        bool skipMode = false;
        bool detailedMode = false;
        bool recordMode = false;
        bool alwaysShow = true;
        Sequence curStepSequence = null;

        Sequence lastlyEntered = null;
        Sequence recentlyMatched = null;

        PrintSequenceContext context = null;

        int matchDepth = 0;

        IRulePattern curRulePattern = null;
        int nextAddedNodeIndex = 0;
        int nextAddedEdgeIndex = 0;

        String[] curAddedNodeNames = null;
        String[] curAddedEdgeNames = null;

        Dictionary<INode, String> annotatedNodes = new Dictionary<INode, String>();
        Dictionary<IEdge, String> annotatedEdges = new Dictionary<IEdge, String>();

        LinkedList<Sequence> loopList = new LinkedList<Sequence>();

        Dictionary<INode, bool> addedNodes = new Dictionary<INode, bool>();
        LinkedList<String> deletedNodes = new LinkedList<String>();
        Dictionary<IEdge, bool> addedEdges = new Dictionary<IEdge, bool>();
        LinkedList<String> deletedEdges = new LinkedList<String>();
        Dictionary<INode, bool> retypedNodes = new Dictionary<INode, bool>();
        Dictionary<IEdge, bool> retypedEdges = new Dictionary<IEdge, bool>();

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
            this.shellGraph = grShellImpl.CurrentShellGraph;

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
                ycompClient = new YCompClient(shellGraph.Graph, debugLayout, 20000, ycompPort, shellGraph.DumpInfo);
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to connect to yComp at port " + ycompPort + ": " + ex.Message);
            }

            shellGraph.Graph.ReuseOptimization = false;
            NotifyOnConnectionLost = true;

            try
            {
                if(layoutOptions != null)
                {
                    LinkedList<String> illegalOptions = null;
                    foreach(KeyValuePair<String, String> option in layoutOptions)
                    {
                        if(!SetLayoutOption(option.Key, option.Value))
                        {
                            if(illegalOptions == null) illegalOptions = new LinkedList<String>();
                            illegalOptions.AddLast(option.Key);
                        }
                    }
                    if(illegalOptions != null)
                    {
                        foreach(String illegalOption in illegalOptions)
                            layoutOptions.Remove(illegalOption);
                    }
                }

                UploadGraph();
            }
            catch(OperationCanceledException)
            {
                throw new Exception("Connection to yComp lost");
            }

            NotifyOnConnectionLost = false;
            RegisterLibGrEvents();
        }

        /// <summary>
        /// returns the named graph on which the sequence is to be executed, containing the names
        /// </summary>
        public NamedGraph GetNamedGraph()
        {
            return shellGraph.Graph;
        }

        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen directions is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        public int ChooseDirection(int direction, Sequence seq)
        {
            context.highlightSeq = seq;
            context.choice = true;
            PrintSequence(debugSequence, seq, context);
            Console.WriteLine();
            context.choice = false;

            Console.Write("Which branch to execute first? (l)eft or (r)ight or (c)ontinue with random choice?  (Random has chosen " + (direction==0 ? "(l)eft" : "(r)ight") + ") ");
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
                case 'c':
                    Console.WriteLine();
                    return direction;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (l)eft branch, (r)ight branch, (c)ontinue allowed! ");
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
            PrintSequence(debugSequence, seq, context);
            Console.WriteLine();
            context.choice = false;
            
            Console.WriteLine("Which match to apply? Showing the match chosen by random. (" + numFurtherMatchesToApply + " following)");
            Console.WriteLine("Press '0'...'9' to show the corresponding match or 'e' to enter the number of the match to show. Press 'c' to commit to the currently shown match and continue.");
            
            if(detailedMode)
            {
                MarkMatches(matches, null, null);
                AnnotateMatches(matches, false);
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
            }

            int newMatchToRewrite = matchToApply;
            while(true)
            {
                MarkMatch(matches.GetMatch(matchToApply), null, null);
                AnnotateMatch(matches.GetMatch(matchToApply), false);
                matchToApply = newMatchToRewrite;
                MarkMatch(matches.GetMatch(matchToApply), ycompClient.MatchedNodeRealizer, ycompClient.MatchedEdgeRealizer);
                AnnotateMatch(matches.GetMatch(matchToApply), true);
                ycompClient.UpdateDisplay();
                ycompClient.Sync();

                Console.WriteLine("Showing match " + matchToApply + " (of " + matches.Count + " matches available)");
                
                ConsoleKeyInfo key = ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case '0': case '1': case '2': case '3': case '4':
                case '5': case '6': case '7': case '8': case '9':
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
                case 'c':
                    MarkMatch(matches.GetMatch(matchToApply), null, null);
                    AnnotateMatch(matches.GetMatch(matchToApply), false);
                    ycompClient.UpdateDisplay();
                    ycompClient.Sync();
                    return matchToApply;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (c)ontinue/commit allowed! ");
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
            context.highlightSeq = seq;
            context.choice = true;
            PrintSequence(debugSequence, seq, context);
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
        /// returns the id/persistent name of a node/edge chosen by the user in yComp
        /// </summary>
        public string ChooseGraphElement()
        {
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
            context.highlightSeq = seq;
            context.choice = true;
            PrintSequence(debugSequence, seq, context);
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

        /// <summary>
        /// Closes the debugger.
        /// </summary>
        public void Close()
        {
            if(ycompClient == null)
                throw new InvalidOperationException("The debugger has already been closed!");

            UnregisterLibGrEvents();

            shellGraph.Graph.ReuseOptimization = true;
            ycompClient.Close();
            ycompClient = null;
            viewerProcess.Close();
            viewerProcess = null;
        }

        public void InitNewRewriteSequence(Sequence seq, bool withStepMode)
        {
            debugSequence = seq;
            curStepSequence = null;
            stepMode = withStepMode;
            recordMode = false;
            alwaysShow = false;
            detailedMode = false;
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

        public ShellGraph CurrentShellGraph
        {
            get { return shellGraph; }
            set
            {
                // switch to new graph in YComp
                UnregisterLibGrEvents();
                ycompClient.ClearGraph();
                shellGraph = value;
                UploadGraph();
                RegisterLibGrEvents();

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
        /// Prints the given sequence adding parentheses if needed according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="parent">The parent of the sequence or null if the sequence is a root</param>
        /// <param name="context">The print context</param>
        private static void PrintSequence(Sequence seq, Sequence parent, PrintSequenceContext context)
        {
            if(parent == null) context.workaround.PrintHighlighted(">", HighlightingMode.SequenceStart);

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
                        if(seqBin.Choice)
                            context.workaround.PrintHighlighted(" " + "[%" + cpPosCounter + "]:", HighlightingMode.Choicepoint);
                        else
                            context.workaround.PrintHighlighted(" " + "%" + cpPosCounter + ":", HighlightingMode.Choicepoint);
                        Console.Write(seq.Symbol + " ");
                        PrintSequence(seqBin.Right, seq, context);
                        break;
                    }

                    if(seqBin == context.highlightSeq && context.choice)
                    {
                        context.workaround.PrintHighlighted("(", HighlightingMode.Choicepoint);
                        PrintSequence(seqBin.Left, seq, context);
                        context.workaround.PrintHighlighted(" " + seq.Symbol + " ", HighlightingMode.Choicepoint);
                        PrintSequence(seqBin.Right, seq, context);
                        context.workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
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
                case SequenceType.For:
                {
                    SequenceFor seqFor = (SequenceFor)seq;
                    Console.Write("for{");
                    Console.Write(seqFor.Var.Name);
                    if(seqFor.VarDst!=null) Console.Write("->" + seqFor.VarDst.Name);
                    Console.Write(" in " + seqFor.Setmap.Name);
                    Console.Write("; ");
                    PrintSequence(seqFor.Seq, seq, context);
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

                // Breakpointable atoms
                case SequenceType.Rule:
                case SequenceType.RuleAll:
                case SequenceType.True:
                case SequenceType.False:
                case SequenceType.VarPredicate:
                {
                    if(context.bpPosCounter >= 0)
                    {
                        if(((SequenceSpecial)seq).Special)
                            context.workaround.PrintHighlighted("[%" + context.bpPosCounter + "]:", HighlightingMode.Breakpoint);
                        else
                            context.workaround.PrintHighlighted("%" + context.bpPosCounter + ":", HighlightingMode.Breakpoint);
                        Console.Write(seq.Symbol);
                        ++context.bpPosCounter;
                        break;
                    }

                    if(context.cpPosCounter >= 0 && seq is SequenceRandomChoice
                        && ((SequenceRandomChoice)seq).Random)
                    {
                        if(((SequenceRandomChoice)seq).Choice)
                            context.workaround.PrintHighlighted("[%" + context.cpPosCounter + "]:", HighlightingMode.Choicepoint);
                        else
                            context.workaround.PrintHighlighted("%" + context.cpPosCounter + ":", HighlightingMode.Choicepoint);
                        Console.Write(seq.Symbol);
                        ++context.cpPosCounter;
                        break;
                    }

                    HighlightingMode mode = HighlightingMode.None;
                    if(seq == context.highlightSeq) {
                        if(context.choice) mode |= HighlightingMode.Choicepoint;
                        else if(context.success) mode |= HighlightingMode.FocusSucces;
                        else mode |= HighlightingMode.Focus;
                    }
                    if(seq == context.lastSuccessSeq) mode |= HighlightingMode.LastSuccess;
                    if(context.lastFailSeq.ContainsKey(seq)) mode |= HighlightingMode.LastFail;
                    context.workaround.PrintHighlighted(seq.Symbol, mode);
                    break;
                }

                // Unary assignment
                case SequenceType.AssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar seqAss = (SequenceAssignSequenceResultToVar)seq;
                    Console.Write("(");
                    Console.Write(seqAss.DestVar.Name);
                    Console.Write("=");
                    PrintSequence(seqAss.Seq, seq, context);
                    Console.Write(")");
                    break;
                }

                // Choice highlightable user assignments
                case SequenceType.AssignUserInputToVar:
                case SequenceType.AssignRandomToVar:
                {
                    if(context.cpPosCounter >= 0 && seq is SequenceAssignRandomToVar)
                    {
                        if(((SequenceRandomChoice)seq).Choice)
                            context.workaround.PrintHighlighted("[%" + context.cpPosCounter + "]:", HighlightingMode.Choicepoint);
                        else
                            context.workaround.PrintHighlighted("%" + context.cpPosCounter + ":", HighlightingMode.Choicepoint);
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

                // Atoms (assignments and queries)
                case SequenceType.Def:
                case SequenceType.AssignVarToVar:
                case SequenceType.AssignConstToVar:
                case SequenceType.AssignAttributeToVar:
                case SequenceType.AssignVarToAttribute:
                case SequenceType.AssignElemToVar:
                case SequenceType.AssignVAllocToVar:
                case SequenceType.AssignSetmapSizeToVar:
                case SequenceType.AssignSetmapEmptyToVar:
                case SequenceType.AssignMapAccessToVar:
                case SequenceType.IsVisited:
                case SequenceType.SetVisited:
                case SequenceType.VFree:
                case SequenceType.VReset:
                case SequenceType.SetmapAdd:
                case SequenceType.SetmapRem:
                case SequenceType.SetmapClear:
                case SequenceType.InSetmap:
                case SequenceType.Emit:
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

        /// <summary>
        /// Called from shell after an debugging abort highlighting the lastly executed rule
        /// </summary>
        public static void PrintSequence(Sequence seq, Sequence highlight, IWorkaround workaround)
        {
            PrintSequenceContext context = new PrintSequenceContext(workaround);
            context.highlightSeq = highlight;
            PrintSequence(seq, null, context);
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

        SequenceSpecial GetSequenceAtBreakpointPosition(Sequence seq, int bpPos, ref int counter)
        {
            if(seq is SequenceSpecial)
            {
                if(counter == bpPos)
                    return (SequenceSpecial) seq;
                counter++;
            }
            foreach(Sequence child in seq.Children)
            {
                SequenceSpecial res = GetSequenceAtBreakpointPosition(child, bpPos, ref counter);
                if(res != null) return res;
            }
            return null;
        }

        void HandleToggleBreakpoints()
        {
            Console.Write("Available breakpoint positions:\n  ");
            PrintSequenceContext contextBp = new PrintSequenceContext(grShellImpl.Workaround);
            contextBp.bpPosCounter = 0;
            PrintSequence(debugSequence, null, contextBp);
            Console.WriteLine();

            if(contextBp.bpPosCounter == 0)
            {
                Console.WriteLine("No breakpoint positions available!");
                return;
            }

            while(true)
            {
                Console.WriteLine("Choose the position of the breakpoint you want to toggle (-1 for no toggle): ");
                String numStr = Console.ReadLine();
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < -1 || num >= contextBp.bpPosCounter)
                    {
                        Console.WriteLine("You must specify a number between -1 and " + (contextBp.bpPosCounter - 1) + "!");
                        continue;
                    }
                    if(num != -1)
                    {
                        int bpCounter = 0;
                        SequenceSpecial bpSeq = GetSequenceAtBreakpointPosition(debugSequence, num, ref bpCounter);
                        bpSeq.Special = !bpSeq.Special;
                    }
                    break;
                }
            }
        }

        SequenceRandomChoice GetSequenceAtChoicepointPosition(Sequence seq, int cpPos, ref int counter)
        {
            if(seq is SequenceRandomChoice && ((SequenceRandomChoice)seq).Random)
            {
                if(counter == cpPos)
                    return (SequenceRandomChoice)seq;
                counter++;
            }
            foreach(Sequence child in seq.Children)
            {
                SequenceRandomChoice res = GetSequenceAtChoicepointPosition(child, cpPos, ref counter);
                if(res != null) return res;
            }
            return null;
        }

        void HandleToggleChoicepoints()
        {
            Console.Write("Available choicepoint positions:\n  ");
            PrintSequenceContext contextCp = new PrintSequenceContext(grShellImpl.Workaround);
            contextCp.cpPosCounter = 0;
            PrintSequence(debugSequence, null, contextCp);
            Console.WriteLine();

            if(contextCp.cpPosCounter == 0)
            {
                Console.WriteLine("No choicepoint positions available!");
                return;
            }

            while(true)
            {
                Console.WriteLine("Choose the position of the choicepoint you want to toggle (-1 for no toggle): ");
                String numStr = Console.ReadLine();
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < -1 || num >= contextCp.cpPosCounter)
                    {
                        Console.WriteLine("You must specify a number between -1 and " + (contextCp.cpPosCounter - 1) + "!");
                        continue;
                    }
                    if(num != -1)
                    {
                        int cpCounter = 0;
                        SequenceRandomChoice cpSeq = GetSequenceAtChoicepointPosition(debugSequence, num, ref cpCounter);
                        cpSeq.Choice = !cpSeq.Choice;
                    }
                    break;
                }
            }
        }

        void DebugEnteringSequence(Sequence seq)
        {
            lastlyEntered = seq;
            recentlyMatched = null;

            // Entering a loop?
            if(seq.SequenceType == SequenceType.IterationMin
                || seq.SequenceType == SequenceType.IterationMinMax
                || seq.SequenceType == SequenceType.For)
            {
                loopList.AddFirst(seq);
            }

            // Breakpoint reached?
            bool breakpointReached = false;
            if((seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll
                || seq.SequenceType == SequenceType.True || seq.SequenceType == SequenceType.False
                || seq.SequenceType == SequenceType.VarPredicate)
                    && ((SequenceSpecial)seq).Special)
            {
                stepMode = true;
                breakpointReached = true;
            }

            if(!stepMode)
            {
                return;
            }

            if(seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll
                || breakpointReached)
            {
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
                context.highlightSeq = seq;
                context.success = false;
                PrintSequence(debugSequence, null, context);
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

            if(seq.SequenceType == SequenceType.IterationMin 
                || seq.SequenceType == SequenceType.IterationMinMax
                || seq.SequenceType == SequenceType.For)
            {
                loopList.RemoveFirst();
            }

            if(seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll
                || seq.SequenceType == SequenceType.True || seq.SequenceType == SequenceType.False
                || seq.SequenceType == SequenceType.VarPredicate)
            {
                if(context.lastSuccessSeq != seq || context.lastSuccessSeq != recentlyMatched)
                    context.lastFailSeq.Add(seq, true);
            }
        }

        void DebugMatched(IMatches matches, bool special)
        {            
            if(matches.Count == 0) // todo: how can this happen?
                return;

            if(dynamicStepMode && !skipMode)
            {
                skipMode = true;
                ycompClient.UpdateDisplay();
                ycompClient.Sync();
                context.highlightSeq = lastlyEntered;
                context.success = true;
                PrintSequence(debugSequence, null, context);
                Console.WriteLine();

                if(!QueryUser(lastlyEntered))
                {
                    recentlyMatched = lastlyEntered;
                    context.lastSuccessSeq = recentlyMatched;
                    context.lastFailSeq.Clear();
                    return;
                }
            }

            recentlyMatched = lastlyEntered;
            context.lastSuccessSeq = recentlyMatched;
            context.lastFailSeq.Clear();

            if(!detailedMode)
                return;

            if(recordMode)
            {
                DebugFinished(null, false);
                matchDepth++;
            }

            if(matchDepth++ > 0)
                Console.WriteLine("Matched " + matches.Producer.Name);

            annotatedNodes.Clear();
            annotatedEdges.Clear();

            curRulePattern = matches.Producer.RulePattern;

            MarkMatches(matches, ycompClient.MatchedNodeRealizer, ycompClient.MatchedEdgeRealizer);
            AnnotateMatches(matches, true);

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to apply rewrite...");
            ReadKeyWithCancel();

            MarkMatches(matches, null, null);

            recordMode = true;
            ycompClient.NodeRealizer = ycompClient.NewNodeRealizer;
            ycompClient.EdgeRealizer = ycompClient.NewEdgeRealizer;
            nextAddedNodeIndex = 0;
            nextAddedEdgeIndex = 0;
        }

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
                    return true;
                case 'u':
                    stepMode = false;
                    dynamicStepMode = false;
                    detailedMode = false;
                    curStepSequence = GetParentSequence(seq, debugSequence);
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
                    PrintSequence(debugSequence, null, context);
                    Console.WriteLine();
                    break;
                case 'c':
                    HandleToggleChoicepoints();
                    context.highlightSeq = seq;
                    context.success = false;
                    PrintSequence(debugSequence, null, context);
                    Console.WriteLine();
                    break;
                case 'a':
                    grShellImpl.Cancel();
                    return false;                               // never reached
                case 'n':
                    stepMode = false;
                    dynamicStepMode = true;
                    detailedMode = false;
                    return false;
                default:
                    Console.WriteLine("Illegal command (Key = " + key.Key
                        + ")! Only (n)ext match, (d)etailed step, (s)tep, step (u)p, step (o)ut, (r)un, toggle (b)reakpoints, toggle (c)hoicepoints and (a)bort allowed!");
                    break;
                }
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
            int i = 0;
            foreach(INode node in match.Nodes)
            {
                if(addAnnotation) {
                    String name = match.Pattern.Nodes[i].UnprefixedName;
                    ycompClient.AnnotateElement(node, name);
                    annotatedNodes[node] = name;
                } else {
                    ycompClient.AnnotateElement(node, null);
                    annotatedNodes.Remove(node);
                }
                i++;
            }
            i = 0;
            foreach(IEdge edge in match.Edges)
            {
                if(addAnnotation) {
                    String name = match.Pattern.Edges[i].UnprefixedName;
                    ycompClient.AnnotateElement(edge, name);
                    annotatedEdges[edge] = name;
                } else {
                    ycompClient.AnnotateElement(edge, null);
                    annotatedEdges.Remove(edge);
                }
                i++;
            }
            AnnotateMatches(match.EmbeddedGraphs, addAnnotation);
            foreach(IMatches iteratedsMatches in match.Iterateds)
                AnnotateMatches(iteratedsMatches, addAnnotation);
            AnnotateMatches(match.Alternatives, addAnnotation);
            AnnotateMatches(match.Independents, addAnnotation);
        }

        private void AnnotateMatches(IEnumerable<IMatch> matches, bool addAnnotation)
        {
            foreach(IMatch match in matches)
            {
                AnnotateMatch(match, addAnnotation);
            }
        }

        void DebugNextMatch()
        {
            nextAddedNodeIndex = 0;
            nextAddedEdgeIndex = 0;
        }

        void DebugNodeAdded(INode node)
        {
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
            if(!recordMode)
            {
                ycompClient.DeleteNode(node);
                if(alwaysShow) ycompClient.UpdateDisplay();
            }
            else
            {
                annotatedNodes.Remove(node);
                ycompClient.ChangeNode(node, ycompClient.DeletedNodeRealizer);

                String name = ycompClient.Graph.GetElementName(node);
                ycompClient.RenameNode(name, "zombie_" + name);
                deletedNodes.AddLast("zombie_" + name);
            }
        }

        void DebugDeletingEdge(IEdge edge)
        {
            if(!recordMode)
            {
                ycompClient.DeleteEdge(edge);
                if(alwaysShow) ycompClient.UpdateDisplay();
            }
            else
            {
                annotatedEdges.Remove(edge);
                ycompClient.ChangeEdge(edge, ycompClient.DeletedEdgeRealizer);

                String name = ycompClient.Graph.GetElementName(edge);
                ycompClient.RenameEdge(name, "zombie_" + name);
                deletedEdges.AddLast("zombie_" + name);
            }
        }

        void DebugClearingGraph()
        {
            ycompClient.ClearGraph();
        }

        void DebugChangingNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            ycompClient.ChangeNodeAttribute(node, attrType, changeType, newValue, keyValue);
        }

        void DebugChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            ycompClient.ChangeEdgeAttribute(edge, attrType, changeType, newValue, keyValue);
        }

        void DebugRetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
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
                ycompClient.ChangeNode(newNode, ycompClient.RetypedNodeRealizer);
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
                ycompClient.ChangeEdge(newEdge, ycompClient.RetypedEdgeRealizer);
                retypedEdges[newEdge] = true;
            }
        }

        void DebugFinished(IMatches matches, bool special)
        {
            if(detailedMode == false) return;

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to continue...");
            ReadKeyWithCancel();

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

            ycompClient.NodeRealizer = null;
            ycompClient.EdgeRealizer = null;

            addedNodes.Clear();
            addedEdges.Clear();
            deletedEdges.Clear();
            deletedNodes.Clear();
            recordMode = false;
            matchDepth--;
        }

        void DebugOnConnectionLost()
        {
            Console.WriteLine("Connection to yComp lost!");
            grShellImpl.Cancel();
        }

        /// <summary>
        /// Registers event handlers for needed LibGr events
        /// </summary>
        void RegisterLibGrEvents()
        {
            shellGraph.Graph.OnNodeAdded += new NodeAddedHandler(DebugNodeAdded);
            shellGraph.Graph.OnEdgeAdded += new EdgeAddedHandler(DebugEdgeAdded);
            shellGraph.Graph.OnRemovingNode += new RemovingNodeHandler(DebugDeletingNode);
            shellGraph.Graph.OnRemovingEdge += new RemovingEdgeHandler(DebugDeletingEdge);
            shellGraph.Graph.OnClearingGraph += new ClearingGraphHandler(DebugClearingGraph);
            shellGraph.Graph.OnChangingNodeAttribute += new ChangingNodeAttributeHandler(DebugChangingNodeAttribute);
            shellGraph.Graph.OnChangingEdgeAttribute += new ChangingEdgeAttributeHandler(DebugChangingEdgeAttribute);
            shellGraph.Graph.OnRetypingNode += new RetypingNodeHandler(DebugRetypingElement);
            shellGraph.Graph.OnRetypingEdge += new RetypingEdgeHandler(DebugRetypingElement);
            shellGraph.Graph.OnSettingAddedNodeNames += new SettingAddedElementNamesHandler(DebugSettingAddedNodeNames);
            shellGraph.Graph.OnSettingAddedEdgeNames += new SettingAddedElementNamesHandler(DebugSettingAddedEdgeNames);

            shellGraph.Graph.OnEntereringSequence += new EnterSequenceHandler(DebugEnteringSequence);
            shellGraph.Graph.OnExitingSequence += new ExitSequenceHandler(DebugExitingSequence);
            shellGraph.Graph.OnMatched += new AfterMatchHandler(DebugMatched);
            shellGraph.Graph.OnRewritingNextMatch += new RewriteNextMatchHandler(DebugNextMatch);
            shellGraph.Graph.OnFinished += new AfterFinishHandler(DebugFinished);
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

        /// <summary>
        /// Unregisters the events previously registered with RegisterLibGrEvents()
        /// </summary>
        void UnregisterLibGrEvents()
        {
            shellGraph.Graph.OnNodeAdded -= new NodeAddedHandler(DebugNodeAdded);
            shellGraph.Graph.OnEdgeAdded -= new EdgeAddedHandler(DebugEdgeAdded);
            shellGraph.Graph.OnRemovingNode -= new RemovingNodeHandler(DebugDeletingNode);
            shellGraph.Graph.OnRemovingEdge -= new RemovingEdgeHandler(DebugDeletingEdge);
            shellGraph.Graph.OnClearingGraph -= new ClearingGraphHandler(DebugClearingGraph);
            shellGraph.Graph.OnChangingNodeAttribute -= new ChangingNodeAttributeHandler(DebugChangingNodeAttribute);
            shellGraph.Graph.OnChangingEdgeAttribute -= new ChangingEdgeAttributeHandler(DebugChangingEdgeAttribute);
            shellGraph.Graph.OnRetypingNode -= new RetypingNodeHandler(DebugRetypingElement);
            shellGraph.Graph.OnRetypingEdge -= new RetypingEdgeHandler(DebugRetypingElement);
            shellGraph.Graph.OnSettingAddedNodeNames -= new SettingAddedElementNamesHandler(DebugSettingAddedNodeNames);
            shellGraph.Graph.OnSettingAddedEdgeNames -= new SettingAddedElementNamesHandler(DebugSettingAddedEdgeNames);

            shellGraph.Graph.OnEntereringSequence -= new EnterSequenceHandler(DebugEnteringSequence);
            shellGraph.Graph.OnExitingSequence -= new ExitSequenceHandler(DebugExitingSequence);
            shellGraph.Graph.OnMatched -= new AfterMatchHandler(DebugMatched);
            shellGraph.Graph.OnRewritingNextMatch -= new RewriteNextMatchHandler(DebugNextMatch);
            shellGraph.Graph.OnFinished -= new AfterFinishHandler(DebugFinished);
        }

        /// <summary>
        /// Uploads the graph to YComp, updates the display and makes a synchonisation
        /// </summary>
        void UploadGraph()
        {
            foreach(INode node in shellGraph.Graph.Nodes)
                ycompClient.AddNode(node);
            foreach(IEdge edge in shellGraph.Graph.Edges)
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
    }
}
