/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using de.unika.ipd.grGen.libGr;

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
    /// GrShellImpl part that controls applying the sequences and the debugger.
    /// </summary>
    public class GrShellSequenceApplierAndDebugger
    {
        private bool silenceExec = false; // print match statistics during sequence execution on timer
        private bool cancelSequence = false;

        private Debugger debugger = null;

        private bool pendingDebugEnable = false;
        private IGrShellUI UserInterface = new GrShellConsoleUI(Console.In, Console.Out);

        private Sequence curGRS;
        private SequenceRuleCall curRule;

        private IGrShellImplForSequenceApplierAndDebugger impl;

        public GrShellSequenceApplierAndDebugger(IGrShellImplForSequenceApplierAndDebugger impl)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

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
                debugger.UpdateYCompDisplay();
        }

        public void DisableDebuggerAfterDeletionAsNeeded(ShellGraphProcessingEnvironment deletedShellGraphProcEnv)
        {
            if(InDebugMode && debugger.ShellProcEnv == deletedShellGraphProcEnv)
                SetDebugMode(false);
        }

        public void ChangeDebuggerGraphAsNeeded(ShellGraphProcessingEnvironment curShellProcEnv)
        {
            if(InDebugMode)
                debugger.ShellProcEnv = curShellProcEnv; // TODO: this is sufficient for the dependencies within debugger?
        }

        public void DebugDoLayout()
        {
            if(!CheckDebuggerAlive())
            {
                impl.debugOut.WriteLine("YComp is not active, yet!");
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
                impl.errOut.WriteLine("Layout options can only be read, when YComp is active!");
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
                    impl.errOut.WriteLine("Disabled printing match statistics during non-debug sequence execution every second");
                else
                    impl.errOut.WriteLine("Enabled printing match statistics during non-debug sequence execution every second");
            }
        }

        private bool ContainsSpecial(Sequence seq)
        {
            if((seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall) 
                && ((SequenceRuleCall)seq).Special)
                return true;

            foreach(Sequence child in seq.Children)
                if(ContainsSpecial(child))
                    return true;

            return false;
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
            }

            if(!InDebugMode && ContainsSpecial(seq))
            {
                impl.curShellProcEnv.ProcEnv.OnEntereringSequence += DumpOnEntereringSequence;
                impl.curShellProcEnv.ProcEnv.OnExitingSequence += DumpOnExitingSequence;
                installedDumpHandlers = true;
            }
            else impl.curShellProcEnv.ProcEnv.OnEntereringSequence += NormalEnteringSequenceHandler;

            curGRS = seq;
            curRule = null;

            impl.debugOut.WriteLine("Executing Graph Rewrite Sequence (CTRL+C for abort) ...");
            cancelSequence = false;
            impl.Workaround.PreventComputerGoingIntoSleepMode(true);
            impl.curShellProcEnv.ProcEnv.PerformanceInfo.Reset();
            StatisticsSource statisticsSource = new StatisticsSource(impl.curShellProcEnv.ProcEnv.NamedGraph, impl.curShellProcEnv.ProcEnv);
            Timer timer = null;
            if(!debug && !silenceExec) timer = new Timer(new TimerCallback(PrintStatistics), statisticsSource, 1000, 1000);

            try
            {
                bool result = impl.curShellProcEnv.ProcEnv.ApplyGraphRewriteSequence(seq);
                if(timer != null)
                    timer.Dispose();

                seq.ResetExecutionState();
                impl.debugOut.WriteLine("Executing Graph Rewrite Sequence done after {0} ms with result {1}:",
                    (impl.curShellProcEnv.ProcEnv.PerformanceInfo.TimeNeeded * 1000).ToString("F1", System.Globalization.CultureInfo.InvariantCulture), result);
                if(impl.newGraphOptions.Profile)
                    impl.debugOut.WriteLine(" - {0} search steps executed", impl.curShellProcEnv.ProcEnv.PerformanceInfo.SearchSteps);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
                impl.debugOut.WriteLine(" - {0} matches found in {1} ms", perfInfo.MatchesFound, perfInfo.TotalMatchTimeMS);
                impl.debugOut.WriteLine(" - {0} rewrites performed in {1} ms", perfInfo.RewritesPerformed, perfInfo.TotalRewriteTimeMS);
#if DEBUGACTIONS
                impl.debugOut.WriteLine("\nDetails:");
                ShowSequenceDetails(seq, perfInfo);
#endif
#else
                impl.debugOut.WriteLine(" - {0} matches found", impl.curShellProcEnv.ProcEnv.PerformanceInfo.MatchesFound);
                impl.debugOut.WriteLine(" - {0} rewrites performed", impl.curShellProcEnv.ProcEnv.PerformanceInfo.RewritesPerformed);
#endif
            }
            catch(OperationCanceledException)
            {
                cancelSequence = true;      // make sure cancelSequence is set to true
                if(timer != null)
                    timer.Dispose();
                if(curRule == null)
                    impl.errOut.WriteLine("Rewrite sequence aborted!");
                else
                {
                    impl.errOut.WriteLine("Rewrite sequence aborted after position:");
                    Debugger.PrintSequence(curGRS, curRule, impl.Workaround);
                    impl.errOut.WriteLine();
                }
            }
            impl.Workaround.PreventComputerGoingIntoSleepMode(false);
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
            else impl.curShellProcEnv.ProcEnv.OnEntereringSequence -= NormalEnteringSequenceHandler;
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
        static void PrintStatistics(Object state)
        {
            StatisticsSource statisticsSource = (StatisticsSource)state;
            if(!statisticsSource.ActionEnv.HighlightingUnderway)
                Console.WriteLine(" ... {0} matches, {1} rewrites, {2} graph changes until now ...", statisticsSource.MatchesFound, statisticsSource.RewritesPerformed, statisticsSource.GraphChanges);
        }

        public void Cancel()
        {
            if(InDebugMode)
                debugger.AbortRewriteSequence();
            throw new OperationCanceledException();                 // abort rewrite sequence
        }

        private void NormalEnteringSequenceHandler(Sequence seq)
        {
            if(cancelSequence)
                Cancel();

            if(seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall)
                curRule = (SequenceRuleCall) seq;
        }

        private void DumpOnEntereringSequence(Sequence seq)
        {
            if(seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall)
            {
                curRule = (SequenceRuleCall) seq;
                if(curRule.Special)
                    impl.curShellProcEnv.ProcEnv.OnFinishing += DumpOnFinishing;
            }
        }

        private void DumpOnExitingSequence(Sequence seq)
        {
            if(seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall)
            {
                SequenceRuleCall ruleSeq = (SequenceRuleCall) seq;
                if(ruleSeq != null && ruleSeq.Special)
                    impl.curShellProcEnv.ProcEnv.OnFinishing -= DumpOnFinishing;
            }

            if(cancelSequence)
                Cancel();
        }

        private void DumpOnFinishing(IMatches matches, bool special)
        {
            int i = 1;
            impl.debugOut.WriteLine("Matched " + matches.Producer.Name + " rule:");
            foreach(IMatch match in matches)
            {
                impl.debugOut.WriteLine(" - " + i + ". match:");
                DumpMatch(match, "   ");
                ++i;
            }
        }

        private void DumpMatch(IMatch match, String indentation)
        {
            int i = 0;
            foreach (INode node in match.Nodes)
                impl.debugOut.WriteLine(indentation + match.Pattern.Nodes[i++].UnprefixedName + ": " + impl.curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node));
            int j = 0;
            foreach (IEdge edge in match.Edges)
                impl.debugOut.WriteLine(indentation + match.Pattern.Edges[j++].UnprefixedName + ": " + impl.curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge));

            foreach(IMatch nestedMatch in match.EmbeddedGraphs)
            {
                impl.debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
            foreach (IMatch nestedMatch in match.Alternatives)
            {
                impl.debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
            foreach (IMatches nestedMatches in match.Iterateds)
            {
                foreach (IMatch nestedMatch in nestedMatches)
                {
                    impl.debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                    DumpMatch(nestedMatch, indentation + "  ");
                }
            }
            foreach (IMatch nestedMatch in match.Independents)
            {
                impl.debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
        }

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if(curGRS == null || cancelSequence)
                return;
            if(curRule == null)
                impl.errOut.WriteLine("Cancelling...");
            else
                impl.errOut.WriteLine("Cancelling: Waiting for \"" + curRule.NameForRuleString + "\" to finish...");
            e.Cancel = true;        // we handled the cancel event
            cancelSequence = true;
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
                    impl.errOut.WriteLine("Debug mode will be enabled as soon as a graph has been created!");
                    pendingDebugEnable = true;
                    return false;
                }
                if(InDebugMode && CheckDebuggerAlive())
                {
                    impl.errOut.WriteLine("You are already in debug mode!");
                    return true;
                }

                Dictionary<String, String> optMap;
                impl.debugLayoutOptions.TryGetValue(impl.debugLayout, out optMap);
                try
                {
                    debugger = new Debugger(impl.GetGrShellImpl(), impl.debugLayout, optMap);
                    impl.curShellProcEnv.ProcEnv.UserProxy = debugger;
                }
                catch(Exception ex)
                {
                    if(ex.Message != "Connection to yComp lost")
                        impl.errOut.WriteLine(ex.Message);
                    return false;
                }
                pendingDebugEnable = false;
            }
            else
            {
                if(impl.curShellProcEnv == null && pendingDebugEnable)
                {
                    impl.debugOut.WriteLine("Debug mode will not be enabled anymore when a graph has been created.");
                    pendingDebugEnable = false;
                    return true;
                }

                if(!InDebugMode)
                {
                    impl.errOut.WriteLine("You are not in debug mode!");
                    return true;
                }

                impl.curShellProcEnv.ProcEnv.UserProxy = null;
                debugger.Close();
                debugger = null;
            }
            return true;
        }

        private bool CheckDebuggerAlive()
        {
            if(!InDebugMode)
                return false;
            if(!debugger.YCompClient.Sync())
            {
                debugger = null;
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
            else debugModeActivated = false;

            ApplyRewriteSequence(seq, true);

            if(debugModeActivated && CheckDebuggerAlive())   // enabled debug mode here and didn't loose connection?
            {
                if(UserInterface.ShowMsgAskForYesNo("Do you want to leave debug mode?"))
                {
                    SetDebugMode(false);
                }
            }
        }

        public object Askfor(String typeName)
        {
            if(typeName == null)
            {
                UserInterface.ShowMsgAskForEnter("Pause..");
                return null;
            }

            if(TypesHelper.GetNodeOrEdgeType(typeName, impl.curShellProcEnv.ProcEnv.NamedGraph.Model) != null) // if type is node/edge type let the user select the element in yComp
            {
                if(!CheckDebuggerAlive())
                {
                    impl.errOut.WriteLine("debug mode must be enabled (yComp available) for asking for a node/edge type");
                    return null;
                }

                impl.debugOut.WriteLine("Select an element of type " + typeName + " by double clicking in yComp (ESC for abort)...");

                String id = debugger.ChooseGraphElement();
                if(id == null)
                    return null;

                impl.debugOut.WriteLine("Received @(\"" + id + "\")");

                IGraphElement elem = impl.curShellProcEnv.ProcEnv.NamedGraph.GetGraphElement(id);
                if(elem == null)
                {
                    impl.errOut.WriteLine("Graph element does not exist (anymore?).");
                    return null;
                }
                if(!TypesHelper.IsSameOrSubtype(elem.Type.PackagePrefixedName, typeName, impl.curShellProcEnv.ProcEnv.NamedGraph.Model))
                {
                    impl.errOut.WriteLine(elem.Type.PackagePrefixedName + " is not the same type as/a subtype of " + typeName + ".");
                    return null;
                }
                return elem;
            }
            else // else let the user type in the value
            {
                String inputValue = UserInterface.ShowMsgAskForString("Enter a value of type " + typeName + ": ");
                StringReader reader = new StringReader(inputValue);
                GrShell shellForParsing = new GrShell(reader);
                shellForParsing.SetImpl(impl.GetGrShellImpl());
                object val = shellForParsing.Constant();
                String valTypeName = TypesHelper.XgrsTypeOfConstant(val, impl.curShellProcEnv.ProcEnv.NamedGraph.Model);
                if(!TypesHelper.IsSameOrSubtype(valTypeName, typeName, impl.curShellProcEnv.ProcEnv.NamedGraph.Model))
                {
                    impl.errOut.WriteLine(valTypeName + " is not the same type as/a subtype of " + typeName + ".");
                    return null;
                }
                return val;
            }
        }
    }
}
