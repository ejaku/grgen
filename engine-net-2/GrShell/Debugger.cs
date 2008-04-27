/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;

namespace de.unika.ipd.grGen.grShell
{
    class Debugger
    {
        GrShellImpl grShellImpl;
        ShellGraph shellGraph;

        Process viewerProcess = null;
        YCompClient ycompClient = null;
        Sequence debugSequence = null;
        bool stepMode = true;
        bool detailedMode = false;
        bool recordMode = false;
        bool alwaysShow = true;
        Sequence curStepSequence = null;

        int matchDepth = 0;

        IRulePattern curRulePattern = null;
        int nextAddedNodeIndex = 0;
        int nextAddedEdgeIndex = 0;

        String[] curAddedNodeNames = null;
        String[] curAddedEdgeNames = null;

        Dictionary<INode, String> markedNodes = new Dictionary<INode, String>();
        Dictionary<IEdge, String> markedEdges = new Dictionary<IEdge, String>();

        LinkedList<Sequence> loopList = new LinkedList<Sequence>();
        Dictionary<INode, bool> addedNodes = new Dictionary<INode, bool>();
        LinkedList<String> deletedNodes = new LinkedList<String>();
        Dictionary<IEdge, bool> addedEdges = new Dictionary<IEdge, bool>();
        LinkedList<String> deletedEdges = new LinkedList<String>();
        Dictionary<INode, bool> retypedNodes = new Dictionary<INode, bool>();
        Dictionary<IEdge, bool> retypedEdges = new Dictionary<IEdge, bool>();

        public Debugger(GrShellImpl grShellImpl) : this(grShellImpl, "Orthogonal", null) {}
        public Debugger(GrShellImpl grShellImpl, String debugLayout) : this(grShellImpl, debugLayout, null) {}

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

            int ycompPort = GetFreeTCPPort();
            if(ycompPort < 0)
            {
                throw new Exception("Didn't find a free TCP port in the range 4242-4251!");
            }
            try
            {
                viewerProcess = Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    + Path.DirectorySeparatorChar + "ycomp", "-p " + ycompPort);
            }
            catch(Exception e)
            {
                throw new Exception("Unable to start ycomp: " + e.ToString());
            }

            try
            {
                ycompClient = new YCompClient(shellGraph.Graph, debugLayout, 20000, ycompPort, shellGraph.DumpInfo);
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to connect to YComp at port " + ycompPort + ": " + ex.Message);
            }

            ycompClient.OnConnectionLost += new ConnectionLostHandler(DebugOnConnectionLost);
            shellGraph.Graph.ReuseOptimization = false;

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
                return;
            }

            RegisterLibGrEvents();
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
            catch(OperationCanceledException) { }
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


/*        private int GetPrecedence(Sequence.OperandType type)
        {
            switch(type)
            {
                case Sequence.OperandType.Concat: return 0;
                case Sequence.OperandType.Xor: return 1;
                case Sequence.OperandType.And: return 2;
                case Sequence.OperandType.Star: return 3;
                case Sequence.OperandType.Max: return 3;
                case Sequence.OperandType.Rule: return 4;
                case Sequence.OperandType.RuleAll: return 4;
                case Sequence.OperandType.Def: return 4;
            }
            return -1;
        }*/

        /// <summary>
        /// Prints the given child sequence inside the parent context adding parentheses around the child if needed.
        /// </summary>
        /// <param name="seq">The child to be printed</param>
        /// <param name="parent">The parent of the child or null if the child is a root</param>
        /// <param name="highlightSeq">A sequence to be highlighted or null</param>
        /// <param name="bpposcounter">A counter increased for every potential breakpoint position and printed next to a potential breakpoint.
        ///     If bpposcounter is smaller than zero, no such counter is used or printed.</param>
        private static void PrintChildSequence(Sequence seq, Sequence parent, Sequence highlightSeq, IWorkaround workaround, ref int bpposCounter)
        {
            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence) Console.Write("(");

            switch(seq.SequenceType)
            {
                case SequenceType.LazyOr:
                case SequenceType.LazyAnd:
                case SequenceType.StrictOr:
                case SequenceType.Xor:
                case SequenceType.StrictAnd:
                {
                    bool first = true;
                    foreach(Sequence child in seq.Children)
                    {
                        if(!first)
                            Console.Write(" " + seq.Symbol + " ");
                        else
                            first = false;
                        PrintChildSequence(child, seq, highlightSeq, workaround, ref bpposCounter);
                    }
                    break;
                }
                case SequenceType.Not:
                {
                    foreach(Sequence child in seq.Children)
                    {
                        Console.Write(seq.Symbol);
                        PrintChildSequence(child, seq, highlightSeq, workaround, ref bpposCounter);
                    }
                    break;
                }
                case SequenceType.Min:
                {
                    SequenceMin seqMin = (SequenceMin)seq;
                    PrintChildSequence(seqMin.Seq, seq, highlightSeq, workaround, ref bpposCounter);
                    Console.Write("[" + seqMin.Min + ":*]");
                    break;
                }
                case SequenceType.MinMax:
                {
                    SequenceMinMax seqMinMax = (SequenceMinMax)seq;
                    PrintChildSequence(seqMinMax.Seq, seq, highlightSeq, workaround, ref bpposCounter);
                    Console.Write("[" + seqMinMax.Min + ":" + seqMinMax.Max + "]");
                    break;
                }

                case SequenceType.Rule:
                case SequenceType.RuleAll:
                case SequenceType.True:
                case SequenceType.False:
                {
                    if(bpposCounter >= 0)
                        workaround.PrintHighlighted("<<" + (bpposCounter++) + ">>");
                    goto case SequenceType.Def;                             // fall through
                }
                case SequenceType.Def:
                case SequenceType.AssignVarToVar:
                case SequenceType.AssignElemToVar:
                {
                    if(seq == highlightSeq)
                        workaround.PrintHighlighted(seq.Symbol);
                    else
                        Console.Write(seq.Symbol);
                    break;
                }

                case SequenceType.Transaction:
                {
                    Console.Write("<");
                    IEnumerator<Sequence> iter = seq.Children.GetEnumerator();
                    iter.MoveNext();
                    PrintSequence(iter.Current, highlightSeq, workaround);
                    Console.Write(">");
                    break;
                }
            }

            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence) Console.Write(")");
        }

        public static void PrintSequence(Sequence seq, Sequence highlightSeq, IWorkaround workaround)
        {
            int counter = -1;
            PrintChildSequence(seq, null, highlightSeq, workaround, ref counter);
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
            if(grShellImpl.OperationCancelled) grShellImpl.Cancel();
            Console.TreatControlCAsInput = true;
            ConsoleKeyInfo key = grShellImpl.Workaround.ReadKey(true);
            Console.TreatControlCAsInput = false;
            if(key.Key == ConsoleKey.C && (key.Modifiers & ConsoleModifiers.Control) != 0) grShellImpl.Cancel();
            return key;
        }

        SequenceSpecial GetSequenceAtBreakpointPosition(Sequence seq, int bppos, ref int counter)
        {
            if(seq is SequenceSpecial)
            {
                if(counter == bppos)
                    return (SequenceSpecial) seq;
                counter++;
            }
            foreach(Sequence child in seq.Children)
            {
                SequenceSpecial res = GetSequenceAtBreakpointPosition(child, bppos, ref counter);
                if(res != null) return res;
            }
            return null;
        }

        void HandleToggleBreakpoints()
        {
            Console.Write("Available breakpoint positions:\n  ");
            int numbppos = 0;
            PrintChildSequence(debugSequence, null, null, grShellImpl.Workaround, ref numbppos);
            Console.WriteLine();

            if(numbppos == 0)
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
                    if(num < -1 || num >= numbppos)
                    {
                        Console.WriteLine("You must specify a number between -1 and " + (numbppos - 1) + "!");
                        continue;
                    }
                    if(num != -1)
                    {
                        int bpcounter = 0;
                        SequenceSpecial bpseq = GetSequenceAtBreakpointPosition(debugSequence, num, ref bpcounter);
                        bpseq.Special = !bpseq.Special;
                    }
                    break;
                }
            }
        }

        void DebugEntereringSequence(Sequence seq)
        {
            bool breakpointReached;
            // Entering a loop?
            if(seq.SequenceType == SequenceType.Min || seq.SequenceType == SequenceType.MinMax)
                loopList.AddFirst(seq);

            // Breakpoint reached?
            if((seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll
                || seq.SequenceType == SequenceType.True || seq.SequenceType == SequenceType.False)
                    && ((SequenceSpecial)seq).Special)
            {
                stepMode = true;
                breakpointReached = true;
            }
            else breakpointReached = false;

            if(stepMode)
            {
                if(seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll || breakpointReached)
                {
                    ycompClient.UpdateDisplay();
                    ycompClient.Sync();
                    PrintSequence(debugSequence, seq, grShellImpl.Workaround);
                    Console.WriteLine();

                    while(true)
                    {
                        ConsoleKeyInfo key = ReadKeyWithCancel();
                        switch(key.KeyChar)
                        {
                            case 's':
                                detailedMode = false;
                                return;
                            case 'd':
                                detailedMode = true;
                                return;
                            case 'n':
                                detailedMode = false;
                                stepMode = false;
                                curStepSequence = GetParentSequence(seq, debugSequence);
                                return;
                            case 'o':
                                stepMode = false;
                                detailedMode = false;
                                if(loopList.Count == 0) curStepSequence = null;         // execute until the end
                                else curStepSequence = loopList.First.Value;            // execute until current loop has been exited
                                return;
                            case 'r':
                                stepMode = false;
                                detailedMode = false;
                                curStepSequence = null;                                 // execute until the end
                                return;
                            case 'b':
                                HandleToggleBreakpoints();
                                PrintSequence(debugSequence, seq, grShellImpl.Workaround);
                                Console.WriteLine();
                                break;
                            case 'a':
                                grShellImpl.Cancel();
                                return;                                                 // never reached
                            default:
                                Console.WriteLine("Illegal command (Key = " + key.Key
                                    + ")! Only (s)tep, (n)ext, step (o)ut, (d)etailed step, (r)un, toggle (b)reakpoints and (a)bort allowed!");
                                break;
                        }
                    }
                }
            }
        }

        void DebugExitingSequence(Sequence seq)
        {
            if(stepMode == false && seq == curStepSequence)
                stepMode = true;
            if(seq.SequenceType == SequenceType.Min || seq.SequenceType == SequenceType.MinMax)
                loopList.RemoveFirst();
        }

        void DebugMatched(IMatches matches, bool special)
        {
            if(detailedMode == false || matches.Count == 0) return;

            if(recordMode)
            {
                DebugFinished(null, false);
                matchDepth++;
            }

            if(matchDepth++ > 0)
                Console.WriteLine("Matched " + matches.Producer.Name);

            markedNodes.Clear();
            markedEdges.Clear();

            curRulePattern = matches.Producer.RulePattern;

            MarkMatches(matches, ycompClient.MatchedNodeRealizer, ycompClient.MatchedEdgeRealizer, true);

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to apply rewrite...");
            ReadKeyWithCancel();

            MarkMatches(matches, null, null, false);

            recordMode = true;
            ycompClient.NodeRealizer = ycompClient.NewNodeRealizer;
            ycompClient.EdgeRealizer = ycompClient.NewEdgeRealizer;
            nextAddedNodeIndex = 0;
            nextAddedEdgeIndex = 0;
        }

        private void MarkMatches(IEnumerable<IMatch> matches, String nodeRealizerName, String edgeRealizerName, bool annotateElements)
        {
            foreach(IMatch match in matches)
            {
                int i = 0;
                foreach(INode node in match.Nodes)
                {
                    ycompClient.ChangeNode(node, nodeRealizerName);
                    if(annotateElements)
                    {
                        String name = match.Pattern.Nodes[i].UnprefixedName;
                        ycompClient.AnnotateElement(node, name);
                        markedNodes[node] = name;
                    }
                    i++;
                }
                i = 0;
                foreach(IEdge edge in match.Edges)
                {
                    ycompClient.ChangeEdge(edge, ycompClient.MatchedEdgeRealizer);
                    if(annotateElements)
                    {
                        String name = match.Pattern.Edges[i].UnprefixedName;
                        ycompClient.AnnotateElement(edge, name);
                        markedEdges[edge] = name;
                    }
                    i++;
                }
                MarkMatches(match.EmbeddedGraphs, nodeRealizerName, edgeRealizerName, annotateElements);
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
                markedNodes.Remove(node);
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
                markedEdges.Remove(edge);
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

        void DebugChangingNodeAttribute(INode node, AttributeType attrType, object oldValue, object newValue)
        {
            ycompClient.ChangeNodeAttribute(node, attrType, newValue.ToString());
        }

        void DebugChangingEdgeAttribute(IEdge edge, AttributeType attrType, object oldValue, object newValue)
        {
            ycompClient.ChangeEdgeAttribute(edge, attrType, newValue.ToString());
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
                if(markedNodes.TryGetValue(oldNode, out name))
                {
                    markedNodes.Remove(oldNode);
                    markedNodes[newNode] = name;
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
                if(markedEdges.TryGetValue(oldEdge, out name))
                {
                    markedEdges.Remove(oldEdge);
                    markedEdges[newEdge] = name;
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

            foreach(INode node in markedNodes.Keys)
                ycompClient.AnnotateElement(node, null);
            foreach(IEdge edge in markedEdges.Keys)
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
            Console.WriteLine("Connection to YComp lost!");
            grShellImpl.SetDebugMode(false);
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

            if(shellGraph.Actions != null)
            {
                shellGraph.Actions.OnEntereringSequence += new EnterSequenceHandler(DebugEntereringSequence);
                shellGraph.Actions.OnExitingSequence += new ExitSequenceHandler(DebugExitingSequence);
                shellGraph.Actions.OnMatched += new AfterMatchHandler(DebugMatched);
                shellGraph.Actions.OnRewritingNextMatch += new RewriteNextMatchHandler(DebugNextMatch);
                shellGraph.Actions.OnFinished += new AfterFinishHandler(DebugFinished);
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

            if(shellGraph.Actions != null)
            {
                shellGraph.Actions.OnEntereringSequence -= new EnterSequenceHandler(DebugEntereringSequence);
                shellGraph.Actions.OnExitingSequence -= new ExitSequenceHandler(DebugExitingSequence);
                shellGraph.Actions.OnMatched -= new AfterMatchHandler(DebugMatched);
                shellGraph.Actions.OnRewritingNextMatch -= new RewriteNextMatchHandler(DebugNextMatch);
                shellGraph.Actions.OnFinished -= new AfterFinishHandler(DebugFinished);
            }
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
                        catch(SocketException) { } // Nobody there? Good...
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
