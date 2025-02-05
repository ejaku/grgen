/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
using System.Text;

namespace de.unika.ipd.grGen.grShell
{
    class StatisticsSource
    {
        public StatisticsSource(IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            this.graph = graph;
            this.actionEnv = actionEnv;
        }

        public int MatchesFound
        {
            get { return actionEnv.PerformanceInfo.MatchesFound; }
        }

        public int RewritesPerformed
        {
            get { return actionEnv.PerformanceInfo.RewritesPerformed; }
        }

        public long GraphChanges
        {
            get { return graph.ChangesCounter; }
        }

        public IActionExecutionEnvironment ActionEnv
        {
            get { return actionEnv; }
        }

        IGraph graph;
        IActionExecutionEnvironment actionEnv;
    }

    /// <summary>
    /// GrShellImpl part that controls applying the sequences, optionally utilizing the debugger.
    /// Inherits from the DebuggerEnvironment, required by the debugger (adapting the base implementation meant for non-shell usage by overriding as needed).
    /// form TODO: check whether overriden behaviour of DebuggerEnvironement is really needed
    /// </summary>
    public class GrShellSequenceApplierAndDebugger : DebuggerEnvironment
    {
        private bool silenceExec = false; // print match statistics during sequence execution on timer
        private bool cancelSequence = false;

        private IGuiConsoleDebuggerHost guiConsoleDebuggerHost;
        private IBasicGraphViewerClientHost basicGraphViewerClientHost;

        private bool pendingDebugEnable = false;

        private Sequence curGRS;
        private SequenceRuleCall curRule;

        private int matchNum;

        private readonly IGrShellImplForSequenceApplierAndDebugger impl;

        private GraphViewerTypes graphViewerType = GraphViewerTypes.YComp;
        public GraphViewerTypes GraphViewerType
        {
            get { return graphViewerType; }
        }


        public GrShellSequenceApplierAndDebugger(IGrShellImplForSequenceApplierAndDebugger impl)
            : base(DebuggerConsoleUI.Instance, DebuggerConsoleUI.Instance, null)
        {
            ConsoleUI.consoleIn.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            this.impl = impl;
        }

        public bool OperationCancelled
        {
            get { return cancelSequence; }
        }

        private bool InDebugMode
        {
            get { return debugger != null && !debugger.ConnectionLost; }
        }

        public void QuitDebugModeAsNeeded()
        {
            if(InDebugMode)
                SetDebugMode(false);
        }

        public void RestartDebuggerOnNewGraphAsNeeded()
        {
            if(InDebugMode)
            { // switch to new graph from old graph
                SetDebugMode(false);
                pendingDebugEnable = true;
            }

            if(pendingDebugEnable)
                SetDebugMode(true);
        }

        public void UpdateDebuggerDisplayAsNeeded()
        {
            if(InDebugMode)
                debugger.UpdateGraphViewerDisplay();
        }

        public void DisableDebuggerAfterDeletionAsNeeded(ShellGraphProcessingEnvironment deletedShellGraphProcEnv)
        {
            if(InDebugMode && debugger.DebuggerProcEnv == deletedShellGraphProcEnv)
                SetDebugMode(false);
        }

        public void ChangeDebuggerGraphAsNeeded(ShellGraphProcessingEnvironment curShellProcEnv)
        {
            if(InDebugMode)
                debugger.DebuggerProcEnv = curShellProcEnv; // TODO: this is sufficient for the dependencies within debugger?
        }

        public void DebugDoLayout()
        {
            if(!CheckDebuggerAlive())
            {
                ConsoleUI.outWriter.WriteLine("YComp is not active, yet!");
                return;
            }

            debugger.ForceLayout();
        }

        public void SetDebugLayout(String layout)
        {
            if(InDebugMode)
                debugger.SetLayout(layout);
        }

        public void GetDebugLayoutOptions()
        {
            if(!CheckDebuggerAlive())
            {
                ConsoleUI.errorOutWriter.WriteLine("Layout options can only be read, when YComp is active!");
                return;
            }

            debugger.GetLayoutOptions();
        }

        public bool SetDebugLayoutOption(String optionName, String optionValue)
        {
            if(!CheckDebuggerAlive())
            {
                return true;
            }

            return debugger.SetLayoutOption(optionName, optionValue);
        }

        public bool GetDebugOptionTwoPane()
        {
            if(impl.debugOptions.ContainsKey("twopane"))
                return Boolean.Parse(impl.debugOptions["twopane"]);
            else
                return true; // default true, but only applied in case of MSAGL based debugger
        }

        public bool GetDebugOptionGui()
        {
            if(impl.debugOptions.ContainsKey("gui"))
                return Boolean.Parse(impl.debugOptions["gui"]);
            else
                return true; // default true, but only applied in case of MSAGL based debugger
        }

        public void GetDebugOptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("twopane" + " = " + GetDebugOptionTwoPane());
            sb.AppendLine("gui" + " = " + GetDebugOptionGui());
            WriteLine("Available options and their current values:\n\n" + sb.ToString());
        }

        public bool SetDebugOption(String optionName, String optionValue)
        {
            if(optionName == "twopane")
            {
                bool optionValueBoolean;
                if(!Boolean.TryParse(optionValue, out optionValueBoolean))
                    return false;
                else
                {
                    TwoPane = optionValueBoolean; // real storing occurrs in the calling impl upon return with value true
                }
                return true;
            }
            else if(optionName == "gui")
            {
                bool optionValueBoolean;
                if(!Boolean.TryParse(optionValue, out optionValueBoolean))
                    return false;
                else
                {
                    Gui = optionValueBoolean; // real storing occurrs in the calling impl upon return with value true
                }
                return true;
            }
            return false;
        }

        public new bool TwoPane
        {
            get { return base.TwoPane; }
            set
            {
                if(guiConsoleDebuggerHost != null) // make this property true if possible, but debug option must be set in the impl
                {
                    guiConsoleDebuggerHost.TwoPane = value;
                    TheDebuggerConsoleUIForDataRendering = value ? guiConsoleDebuggerHost.OptionalGuiConsoleControl : guiConsoleDebuggerHost.GuiConsoleControl;
                }
            }
        }

        public new bool Gui
        {
            get { return base.Gui; }
            set
            {
                // silent skip is sufficient as of now, TODO: the debug options are a bit scruffy
            }
        }

        public void SetMatchModePre(bool enable)
        {
            if(!InDebugMode)
                return;

            debugger.DetailedModeShowPreMatches = enable;
        }

        public void SetMatchModePost(bool enable)
        {
            if(!InDebugMode)
                return;

            debugger.DetailedModeShowPostMatches = enable;
        }

        public bool SilenceExec
        {
            get
            {
                return silenceExec;
            }
            set
            {
                silenceExec = value;
                if(silenceExec)
                    ConsoleUI.errorOutWriter.WriteLine("Disabled printing match statistics during non-debug sequence execution every second");
                else
                    ConsoleUI.errorOutWriter.WriteLine("Enabled printing match statistics during non-debug sequence execution every second");
            }
        }

        private bool ContainsSpecial(Sequence seq)
        {
            if((seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall)
                && ((SequenceRuleCall)seq).Special)
            {
                return true;
            }
            foreach(Sequence child in seq.Children)
            {
                if(ContainsSpecial(child))
                    return true;
            }
            return false;
        }

        public void ApplyRewriteSequenceExpression(SequenceExpression seqExpr, bool debug)
        {
            SequenceDummy seq = new SequenceDummy();

            if(!impl.ActionsExists())
                return;

            if(debug || CheckDebuggerAlive())
            {
                debugger.NotifyOnConnectionLost = true;
                debugger.InitSequenceExpression(seqExpr, debug);
                debugger.DetailedModeShowPreMatches = impl.detailModePreMatchEnabled;
                debugger.DetailedModeShowPostMatches = impl.detailModePostMatchEnabled;
            }

            curGRS = seq;
            curRule = null;

            ConsoleUI.outWriter.WriteLine("Evaluating Sequence Expression (CTRL+C for abort)...");
            if(debug)
                ConsoleUI.outWriter.WriteLine(seqExpr.Symbol);
            cancelSequence = false;
            WorkaroundManager.Workaround.PreventComputerFromGoingIntoSleepMode(true);
            impl.curShellProcEnv.ProcEnv.PerformanceInfo.Reset();
            StatisticsSource statisticsSource = new StatisticsSource(impl.curShellProcEnv.ProcEnv.NamedGraph, impl.curShellProcEnv.ProcEnv);
            Timer timer = null;
            if(!debug && !CheckDebuggerAlive() && !silenceExec)
                timer = new Timer(new TimerCallback(PrintStatistics), statisticsSource, 1000, 1000);

            try
            {
                object result = impl.curShellProcEnv.ProcEnv.EvaluateGraphRewriteSequenceExpression(seqExpr);
                if(timer != null)
                    timer.Dispose();

                seq.ResetExecutionState();
                ConsoleUI.outWriter.WriteLine("Evaluating Sequence Expression done after {0} ms with result: {1}",
                    (impl.curShellProcEnv.ProcEnv.PerformanceInfo.TimeNeeded * 1000).ToString("F1", System.Globalization.CultureInfo.InvariantCulture),
                    EmitHelper.ToStringAutomatic(result, impl.curShellProcEnv.ProcEnv.Graph, false, impl.curShellProcEnv.objectNamerAndIndexer, impl.curShellProcEnv.transientObjectNamerAndIndexer, null));
                if(impl.newGraphOptions.Profile)
                    ConsoleUI.outWriter.WriteLine(" - {0} search steps executed", impl.curShellProcEnv.ProcEnv.PerformanceInfo.SearchSteps);
#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
                ConsoleUI.outWriter.WriteLine(" - {0} matches found in {1} ms", perfInfo.MatchesFound, perfInfo.TotalMatchTimeMS);
#if DEBUGACTIONS
                ConsoleUI.outWriter.WriteLine("\nDetails:");
                ShowSequenceDetails(seq, perfInfo);
#endif
#else
                ConsoleUI.outWriter.WriteLine(" - {0} matches found", impl.curShellProcEnv.ProcEnv.PerformanceInfo.MatchesFound);
#endif
            }
            catch(OperationCanceledException)
            {
                cancelSequence = true;      // make sure cancelSequence is set to true
                if(timer != null)
                    timer.Dispose();
                ConsoleUI.errorOutWriter.WriteLine("Sequence expression aborted!");
                debugger.Close();
                if(guiConsoleDebuggerHost != null)
                {
                    guiConsoleDebuggerHost.Close();
                    GraphViewerClient.GetDoEventsCaller().DoEvents(); // required by mono/Linux so that the windows are really closed
                }
            }
            WorkaroundManager.Workaround.PreventComputerFromGoingIntoSleepMode(false);
            curGRS = null;

            if(InDebugMode)
            {
                debugger.NotifyOnConnectionLost = false;
                debugger.FinishRewriteSequence();
            }

            StreamWriter emitWriter = impl.curShellProcEnv.ProcEnv.EmitWriter as StreamWriter;
            if(emitWriter != null)
                emitWriter.Flush();
        }

        public void ApplyRewriteSequence(Sequence seq, bool debug)
        {
            bool installedDumpHandlers = false;

            if(!impl.ActionsExists())
                return;

            if(debug || CheckDebuggerAlive())
            {
                debugger.NotifyOnConnectionLost = true;
                debugger.InitNewRewriteSequence(seq, debug);
                debugger.DetailedModeShowPreMatches = impl.detailModePreMatchEnabled;
                debugger.DetailedModeShowPostMatches = impl.detailModePostMatchEnabled;
            }

            if(!InDebugMode && ContainsSpecial(seq))
            {
                impl.curShellProcEnv.ProcEnv.OnEntereringSequence += DumpOnEntereringSequence;
                impl.curShellProcEnv.ProcEnv.OnExitingSequence += DumpOnExitingSequence;
                installedDumpHandlers = true;
            }
            else
                impl.curShellProcEnv.ProcEnv.OnEntereringSequence += NormalEnteringSequenceHandler;

            curGRS = seq;
            curRule = null;

            ConsoleUI.outWriter.WriteLine("Executing Graph Rewrite Sequence (CTRL+C for abort)...");
            cancelSequence = false;
            WorkaroundManager.Workaround.PreventComputerFromGoingIntoSleepMode(true);
            impl.curShellProcEnv.ProcEnv.PerformanceInfo.Reset();
            StatisticsSource statisticsSource = new StatisticsSource(impl.curShellProcEnv.ProcEnv.NamedGraph, impl.curShellProcEnv.ProcEnv);
            Timer timer = null;
            if(!debug && !CheckDebuggerAlive() && !silenceExec)
                timer = new Timer(new TimerCallback(PrintStatistics), statisticsSource, 1000, 1000);

            try
            {
                bool result = impl.curShellProcEnv.ProcEnv.ApplyGraphRewriteSequence(seq);
                if(timer != null)
                    timer.Dispose();

                seq.ResetExecutionState();
                ConsoleUI.outWriter.WriteLine("Executing Graph Rewrite Sequence done after {0} ms with result: {1}",
                    (impl.curShellProcEnv.ProcEnv.PerformanceInfo.TimeNeeded * 1000).ToString("F1", System.Globalization.CultureInfo.InvariantCulture), 
                    result);
                if(impl.newGraphOptions.Profile)
                    ConsoleUI.outWriter.WriteLine(" - {0} search steps executed", impl.curShellProcEnv.ProcEnv.PerformanceInfo.SearchSteps);
#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
                ConsoleUI.outWriter.WriteLine(" - {0} matches found in {1} ms", perfInfo.MatchesFound, perfInfo.TotalMatchTimeMS);
                ConsoleUI.outWriter.WriteLine(" - {0} rewrites performed in {1} ms", perfInfo.RewritesPerformed, perfInfo.TotalRewriteTimeMS);
#if DEBUGACTIONS
                ConsoleUI.outWriter.WriteLine("\nDetails:");
                ShowSequenceDetails(seq, perfInfo);
#endif
#else
                ConsoleUI.outWriter.WriteLine(" - {0} matches found", impl.curShellProcEnv.ProcEnv.PerformanceInfo.MatchesFound);
                ConsoleUI.outWriter.WriteLine(" - {0} rewrites performed", impl.curShellProcEnv.ProcEnv.PerformanceInfo.RewritesPerformed);
#endif
            }
            catch(OperationCanceledException)
            {
                cancelSequence = true;      // make sure cancelSequence is set to true
                if(timer != null)
                    timer.Dispose();
                if(curRule == null)
                    ConsoleUI.errorOutWriter.WriteLine("Rewrite sequence aborted!");
                else
                {
                    ConsoleUI.errorOutWriter.WriteLine("Rewrite sequence aborted after position:");
                    SequencePrinter.PrintSequence(curGRS, curRule, new DebuggerEnvironment(DebuggerConsoleUI.Instance, DebuggerConsoleUI.Instance, null)); // ensure printing to the console, the gui debugger hosts are in a bad state here...
                    ConsoleUI.errorOutWriter.WriteLine();
                }
                debugger.Close();
                if(guiConsoleDebuggerHost != null)
                {
                    guiConsoleDebuggerHost.Close();
                    GraphViewerClient.GetDoEventsCaller().DoEvents(); // required by mono/Linux so that the windows are really closed
                }
            }
            WorkaroundManager.Workaround.PreventComputerFromGoingIntoSleepMode(false);
            curRule = null;
            curGRS = null;

            if(InDebugMode)
            {
                debugger.NotifyOnConnectionLost = false;
                debugger.FinishRewriteSequence();
            }

            StreamWriter emitWriter = impl.curShellProcEnv.ProcEnv.EmitWriter as StreamWriter;
            if(emitWriter != null)
                emitWriter.Flush();

            if(installedDumpHandlers)
            {
                impl.curShellProcEnv.ProcEnv.OnEntereringSequence -= DumpOnEntereringSequence;
                impl.curShellProcEnv.ProcEnv.OnExitingSequence -= DumpOnExitingSequence;
            }
            else
                impl.curShellProcEnv.ProcEnv.OnEntereringSequence -= NormalEnteringSequenceHandler;
        }

#if DEBUGACTIONS
        private void ShowSequenceDetails(Sequence seq, PerformanceInfo perfInfo)
        {
            switch(seq.OperandClass)
            {
            case Sequence.OperandType.Concat:
                ShowSequenceDetails(seq.LeftOperand, perfInfo);
                ShowSequenceDetails(seq.RightOperand, perfInfo);
                break;
            case Sequence.OperandType.Star:
            case Sequence.OperandType.Max:
                ShowSequenceDetails(seq.LeftOperand, perfInfo);
                break;
            case Sequence.OperandType.Rule:
            case Sequence.OperandType.RuleAll:
                debugOut.WriteLine(" - {0,-18}: Matches = {1,6}  Match = {2,6} ms  Rewrite = {3,6} ms",
                    ((IAction) seq.Value).Name, seq.GetTotalApplied(),
                    perfInfo.TimeDiffToMS(seq.GetTotalMatchTime()), perfInfo.TimeDiffToMS(seq.GetTotalRewriteTime()));
                break;
            }
        }
#endif

        // called from a timer while a sequence is executed outside of the debugger 
        // (this may still mean the debugger is open and attached ("debug enable"), but just not under user control)
        static void PrintStatistics(object state)
        {
            StatisticsSource statisticsSource = (StatisticsSource)state;
            if(!statisticsSource.ActionEnv.HighlightingUnderway)
                ConsoleUI.outWriter.WriteLine(" ...{0} matches, {1} rewrites, {2} graph changes until now...", statisticsSource.MatchesFound, statisticsSource.RewritesPerformed, statisticsSource.GraphChanges);
        }

        public override void Cancel()
        {
            if(InDebugMode)
                debugger.AbortRewriteSequence();
            throw new OperationCanceledException();                 // abort rewrite sequence
        }

        private void NormalEnteringSequenceHandler(SequenceBase seq)
        {
            if(cancelSequence)
                Cancel();

            if(seq.HasSequenceType(SequenceType.RuleCall) || seq.HasSequenceType(SequenceType.RuleAllCall) || seq.HasSequenceType(SequenceType.RuleCountAllCall))
                curRule = (SequenceRuleCall) seq;
        }

        private void DumpOnEntereringSequence(SequenceBase seq)
        {
            if(seq.HasSequenceType(SequenceType.RuleCall) || seq.HasSequenceType(SequenceType.RuleAllCall) || seq.HasSequenceType(SequenceType.RuleCountAllCall))
            {
                curRule = (SequenceRuleCall) seq;
                if(curRule.Special)
                {
                    matchNum = 0;
                    impl.curShellProcEnv.ProcEnv.OnMatchedAfter += DumpOnMatchedAfterFiltering;
                    impl.curShellProcEnv.ProcEnv.OnMatchSelected += DumpOnMatchSelected;
                }
            }
        }

        private void DumpOnExitingSequence(SequenceBase seq)
        {
            if(seq.HasSequenceType(SequenceType.RuleCall) || seq.HasSequenceType(SequenceType.RuleAllCall) || seq.HasSequenceType(SequenceType.RuleCountAllCall))
            {
                SequenceRuleCall ruleSeq = (SequenceRuleCall) seq;
                if(ruleSeq != null && ruleSeq.Special)
                {
                    impl.curShellProcEnv.ProcEnv.OnMatchSelected -= DumpOnMatchSelected;
                    impl.curShellProcEnv.ProcEnv.OnMatchedAfter -= DumpOnMatchedAfterFiltering;
                }
            }

            if(cancelSequence)
                Cancel();
        }

        private void DumpOnMatchedAfterFiltering(IMatches[] matches, bool[] special)
        {
            ConsoleUI.outWriter.WriteLine("Matched " + matches[0].Producer.Name + " rule:");
        }

        private void DumpOnMatchSelected(IMatch match, bool special, IMatches matches)
        {
            ConsoleUI.outWriter.WriteLine(" - " + matchNum + ". match:");
            DumpMatch(match, "   ");
            ++matchNum;
        }

        private void DumpMatch(IMatch match, String indentation)
        {
            int i = 0;
            foreach(INode node in match.Nodes)
            {
                ConsoleUI.outWriter.WriteLine(indentation + match.Pattern.Nodes[i++].UnprefixedName + ": " + impl.curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node));
            }
            int j = 0;
            foreach(IEdge edge in match.Edges)
            {
                ConsoleUI.outWriter.WriteLine(indentation + match.Pattern.Edges[j++].UnprefixedName + ": " + impl.curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge));
            }

            foreach(IMatch nestedMatch in match.EmbeddedGraphs)
            {
                ConsoleUI.outWriter.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
            foreach (IMatch nestedMatch in match.Alternatives)
            {
                ConsoleUI.outWriter.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
            foreach (IMatches nestedMatches in match.Iterateds)
            {
                foreach (IMatch nestedMatch in nestedMatches)
                {
                    ConsoleUI.outWriter.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                    DumpMatch(nestedMatch, indentation + "  ");
                }
            }
            foreach (IMatch nestedMatch in match.Independents)
            {
                ConsoleUI.outWriter.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
        }

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if(curGRS == null || cancelSequence)
                return;
            if(curRule == null)
                ConsoleUI.errorOutWriter.WriteLine("Cancelling...");
            else
                ConsoleUI.errorOutWriter.WriteLine("Cancelling: Waiting for \"" + curRule.Name + "\" to finish...");
            e.Cancel = true;        // we handled the cancel event
            cancelSequence = true;
        }

        public bool DebugWith(GraphViewerTypes graphViewerType)
        {
            if(InDebugMode)
            {
                ConsoleUI.errorOutWriter.WriteLine("Cannot change debugger/graph viewer while in debug mode!");
                return false;
            }

            this.graphViewerType = graphViewerType;
            return true;
        }

        /// <summary>
        /// Enables or disables debug mode.
        /// </summary>
        /// <param name="enable">Whether to enable or not.</param>
        /// <returns>True, if the mode has the desired value at the end of the function.</returns>
        public bool SetDebugMode(bool enable)
        {
            if(impl.nonDebugNonGuiExitOnError)
            {
                return true;
            }

            if(enable)
            {
                if(impl.curShellProcEnv == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Debug mode will be enabled as soon as a graph has been created!");
                    pendingDebugEnable = true;
                    return false;
                }
                if(InDebugMode && CheckDebuggerAlive())
                {
                    ConsoleUI.errorOutWriter.WriteLine("You are already in debug mode!");
                    return true;
                }

                Dictionary<String, String> optMap;
                impl.debugLayoutOptions.TryGetValue(impl.debugLayout, out optMap);
                try
                {
                    guiConsoleDebuggerHost = null;
                    basicGraphViewerClientHost = null;
                    if(graphViewerType != GraphViewerTypes.YComp)
                    {
                        IHostCreator hostCreator = GraphViewerClient.GetGuiConsoleDebuggerHostCreator();
                        guiConsoleDebuggerHost = GetDebugOptionGui() ? hostCreator.CreateGuiDebuggerHost() : hostCreator.CreateGuiConsoleDebuggerHost(GetDebugOptionTwoPane());
                        basicGraphViewerClientHost = hostCreator.CreateBasicGraphViewerClientHost();

                        if(GetDebugOptionGui())
                            TheDebuggerGUIForDataRendering = ((IGuiDebuggerHost)guiConsoleDebuggerHost).MainWorkObjectGuiGraphRenderer;

                        TheDebuggerConsoleUI = guiConsoleDebuggerHost.GuiConsoleControl;
                        TheDebuggerConsoleUIForDataRendering = GetDebugOptionTwoPane() ? guiConsoleDebuggerHost.OptionalGuiConsoleControl : guiConsoleDebuggerHost.GuiConsoleControl;
                    }
                    debugger = new Debugger(this, impl.curShellProcEnv, impl.realizers, graphViewerType, impl.debugLayout, optMap, basicGraphViewerClientHost);
                    if(graphViewerType != GraphViewerTypes.YComp)
                    {
                        guiConsoleDebuggerHost.Debugger = debugger;
                        guiConsoleDebuggerHost.Show();
                    }
                    debugger.DetailedModeShowPreMatches = impl.detailModePreMatchEnabled;
                    debugger.DetailedModeShowPostMatches = impl.detailModePostMatchEnabled;
                    impl.curShellProcEnv.ProcEnv.UserProxy = debugger;
                }
                catch(Exception ex)
                {
                    if(ex.Message != "Connection to yComp lost")
                        ConsoleUI.errorOutWriter.WriteLine(ex.Message);
                    return false;
                }
                pendingDebugEnable = false;
            }
            else
            {
                if(impl.curShellProcEnv == null && pendingDebugEnable)
                {
                    ConsoleUI.outWriter.WriteLine("Debug mode will not be enabled anymore when a graph has been created.");
                    pendingDebugEnable = false;
                    return true;
                }

                if(!InDebugMode)
                {
                    ConsoleUI.errorOutWriter.WriteLine("You are not in debug mode!");
                    return true;
                }

                impl.curShellProcEnv.ProcEnv.UserProxy = null;
                debugger.Close();
                debugger = null;
                if(guiConsoleDebuggerHost != null)
                    guiConsoleDebuggerHost.Close();
                guiConsoleDebuggerHost = null;
                basicGraphViewerClientHost = null;
                TheDebuggerConsoleUI = DebuggerConsoleUI.Instance;
                TheDebuggerConsoleUIForDataRendering = DebuggerConsoleUI.Instance;
                TheDebuggerGUIForDataRendering = null;
            }
            return true;
        }

        protected override bool CheckDebuggerAlive()
        {
            if(!InDebugMode)
                return false;
            if(!debugger.GraphViewerClient.Sync())
            {
                debugger = null;
                guiConsoleDebuggerHost = null;
                basicGraphViewerClientHost = null;
                TheDebuggerConsoleUI = DebuggerConsoleUI.Instance;
                TheDebuggerConsoleUIForDataRendering = DebuggerConsoleUI.Instance;
                TheDebuggerGUIForDataRendering = null;
                return false;
            }
            return true;
        }

        public void DebugRewriteSequence(Sequence seq)
        {
            if(impl.nonDebugNonGuiExitOnError)
            {
                ApplyRewriteSequence(seq, false);
                return;
            }

            bool debugModeActivated;

            if(!CheckDebuggerAlive())
            {
                if(!SetDebugMode(true))
                    return;
                debugModeActivated = true;
            }
            else
                debugModeActivated = false;

            ApplyRewriteSequence(seq, true);

            if(debugModeActivated && CheckDebuggerAlive())   // enabled debug mode here and didn't loose connection?
            {
                if(ShowMsgAskForYesNo("Do you want to leave debug mode?"))
                    SetDebugMode(false);
            }
        }

        public void DebugRewriteSequenceExpression(SequenceExpression seqExpr)
        {
            if(impl.nonDebugNonGuiExitOnError)
            {
                ApplyRewriteSequenceExpression(seqExpr, false);
                return;
            }

            bool debugModeActivated;

            if(!CheckDebuggerAlive())
            {
                if(!SetDebugMode(true))
                    return;
                debugModeActivated = true;
            }
            else
                debugModeActivated = false;

            ApplyRewriteSequenceExpression(seqExpr, true);

            if(debugModeActivated && CheckDebuggerAlive())   // enabled debug mode here and didn't loose connection?
            {
                if(ShowMsgAskForYesNo("Do you want to leave debug mode?"))
                    SetDebugMode(false);
            }
        }

        /// <summary>
        /// Reads a key from the keyboard using the workaround manager.
        /// If CTRL+C is pressed, grShellImpl.Cancel() is called.
        /// </summary>
        /// <returns>The ConsoleKeyInfo object for the pressed key.</returns>
        public override ConsoleKeyInfo ReadKeyWithCancel()
        {
            if(OperationCancelled)
                Cancel();

            ConsoleKeyInfo key = ReadKeyWithControlCAsInput();

            if(key.Key == ConsoleKey.C && (key.Modifiers & ConsoleModifiers.Control) != 0)
                Cancel();

            return key;
        }

        public override string ShowGraphWith(String programName, String arguments, bool keep)
        {
            if(GraphViewer.IsDotExecutable(programName))
                return GraphViewer.ShowGraphWithDot(impl.curShellProcEnv, programName, arguments, keep);
            else if(programName.Equals("MSAGL", StringComparison.InvariantCultureIgnoreCase))
                return GraphViewer.ShowGraphWithMSAGL(impl.curShellProcEnv, impl.debugLayout, programName, arguments, keep);
            else
                return GraphViewer.ShowVcgGraph(impl.curShellProcEnv, impl.debugLayout, programName, arguments, keep);
        }
    }
}
