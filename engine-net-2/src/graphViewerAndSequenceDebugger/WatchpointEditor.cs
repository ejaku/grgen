/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    class WatchpointEditor
    {
        UserChoiceMenu handleWatchpointsMenu = new UserChoiceMenu(UserChoiceMenuNames.HandleWatchpointsMenu, new string[] {
            "watchpointEdit", "watchpointToggle", "watchpointDelete", "watchpointInsert", "watchpointAppend", "watchpointAbortReturn" });

        readonly DebuggerGraphProcessingEnvironment debuggerProcEnv;
        readonly IDebuggerEnvironment env;
        readonly IDisplayer displayer;

        public WatchpointEditor(DebuggerGraphProcessingEnvironment debuggerProcEnv, IDebuggerEnvironment env, IDisplayer displayer)
        {
            this.debuggerProcEnv = debuggerProcEnv;
            this.env = env;
            this.displayer = displayer;
        }

        public void HandleWatchpoints()
        {
            while(true)
            {
                displayer.BeginOfDisplay("List of registered watchpoints:");
                for(int i = 0; i < debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count; ++i)
                {
                    env.WriteLineDataRendering(i + " - " + debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[i].ToString());
                }

                env.PrintInstructions(handleWatchpointsMenu, "Press ", ".");

                int num = -1;
                switch(env.LetUserChoose(handleWatchpointsMenu))
                {
                case 'e':
                    num = QueryWatchpoint("edit");
                    if(num == -1)
                        break;
                    EditWatchpoint(num);
                    break;
                case 't':
                    num = QueryWatchpoint("toggle (enable/disable)");
                    if(num == -1)
                        break;
                    ToggleWatchpoint(num);
                    break;
                case 'd':
                    num = QueryWatchpoint("delete");
                    if(num == -1)
                        break;
                    DeleteWatchpoint(num);
                    break;
                case 'i':
                    num = QueryWatchpoint("insert");
                    if(num == -1)
                        break;
                    InsertWatchpoint(num);
                    break;
                case 'p':
                    AppendWatchpoint();
                    break;
                case 'a':
                case 'r':
                    env.WriteLine("Back from watchpoints to debugging.");
                    return;
                default:
                    throw new Exception("Internal error");
                }
            }
        }

        private int QueryWatchpoint(string action)
        {
            do
            {
                int num = env.ShowMsgAskForIntegerNumber("Enter number of watchpoint to " + action + " (-1 for abort)");
                if(num < -1 || num >= debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count)
                {
                    env.WriteLine("You must specify a number between -1 and " + (debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count - 1) + "!");
                    continue;
                }
                return num;
            }
            while(true);
        }

        private void EditWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            cr = EditOrCreateRule(cr);
            if(cr == null)
                env.WriteLine("aborted");
            else
            {
                env.WriteLine("edited entry " + num + " - " + cr.ToString());
                debuggerProcEnv.SubruleDebugConfig.Replace(num, cr);
            }
        }

        private void ToggleWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            cr.Enabled = !cr.Enabled;
            env.WriteLine("toggled entry " + num + " - " + cr.ToString());
        }

        private void DeleteWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            debuggerProcEnv.SubruleDebugConfig.Delete(num);
            env.WriteLine("deleted entry " + num + " - " + cr.ToString());
        }

        private void InsertWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = EditOrCreateRule(null);
            if(cr == null)
                env.WriteLine("aborted");
            else
            {
                debuggerProcEnv.SubruleDebugConfig.Insert(cr, num);
                env.WriteLine("inserted entry " + num + " - " + cr.ToString());
            }
        }

        private void AppendWatchpoint()
        {
            SubruleDebuggingConfigurationRule cr = EditOrCreateRule(null);
            if(cr == null)
                env.WriteLine("aborted");
            else
            {
                debuggerProcEnv.SubruleDebugConfig.Insert(cr);
                env.WriteLine("appended entry " + (debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count - 1) + " - " + cr.ToString());
            }
        }

        private SubruleDebuggingConfigurationRule EditOrCreateRule(SubruleDebuggingConfigurationRule cr)
        {
            // edit or keep type
            SubruleDebuggingEvent sde = DetermineEventTypeToConfigure(cr);
            if(sde == SubruleDebuggingEvent.Undefined)
                return null;

            // for Add, Rem, Emit, Halt, Highlight
            string message = null;
            SubruleMesssageMatchingMode smmm = SubruleMesssageMatchingMode.Undefined;

            // for Match
            IAction action = null;

            // for New, Delete, Retype, SetAttributes
            string graphElementName = null;
            GrGenType graphElementType = null;
            bool only = false;

            if(sde == SubruleDebuggingEvent.Add || sde == SubruleDebuggingEvent.Rem || sde == SubruleDebuggingEvent.Emit 
                || sde == SubruleDebuggingEvent.Halt || sde == SubruleDebuggingEvent.Highlight)
            {
                // edit or keep message matching mode and message
                smmm = DetermineMessageAndMessageMatchingMode(cr, 
                    out message);
                if(smmm == SubruleMesssageMatchingMode.Undefined)
                    return null;
            }
            else if(sde == SubruleDebuggingEvent.Match)
            {
                // edit ok keep action name
                action = DetermineAction(cr);
                if(action == null)
                    return null;
            }
            else if(sde == SubruleDebuggingEvent.New || sde == SubruleDebuggingEvent.Delete
                || sde == SubruleDebuggingEvent.Retype || sde == SubruleDebuggingEvent.SetAttributes)
            {
                // edit or keep choice of type, exact type, name
                bool abort = DetermineMatchGraphElementMode(cr, 
                    out graphElementName, out graphElementType, out only);
                if(abort)
                    return null;
            }

            // edit or keep decision action
            SubruleDebuggingDecision sdd = DetermineDecisionAction(cr);
            if(sdd == SubruleDebuggingDecision.Undefined)
                return null;

            // edit or keep condition if type action or graph change
            SequenceExpression ifClause = null;
            if(sde != SubruleDebuggingEvent.Add && sde != SubruleDebuggingEvent.Rem && sde != SubruleDebuggingEvent.Retype
                && sde != SubruleDebuggingEvent.Halt && sde != SubruleDebuggingEvent.Highlight)
            {
                ifClause = DetermineCondition(cr, sde, action, graphElementType);
            }

            if(sde == SubruleDebuggingEvent.Add || sde == SubruleDebuggingEvent.Rem || sde == SubruleDebuggingEvent.Emit
                || sde == SubruleDebuggingEvent.Halt || sde == SubruleDebuggingEvent.Highlight)
            {
                return new SubruleDebuggingConfigurationRule(sde, message, smmm, sdd);
            }
            else if(sde == SubruleDebuggingEvent.Match)
                return new SubruleDebuggingConfigurationRule(sde, action, sdd, ifClause);
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

        private SubruleDebuggingEvent DetermineEventTypeToConfigure(SubruleDebuggingConfigurationRule cr)
        {
            UserChoiceMenu watchpointDetermineEventTypeToConfigureMenu = new UserChoiceMenu(UserChoiceMenuNames.WatchpointDetermineEventTypeToConfigureMenu, new string[] {
                "watchpointEventEntry" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Add ? "Keep" : ""),
                "watchpointEventExit" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Rem ? "Keep" : ""),
                "watchpointEventReport" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Emit ? "Keep" : ""),
                "watchpointEventHalt" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Halt ? "Keep" : ""),
                "watchpointEventHighlight" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Highlight ? "Keep" : ""),
                "watchpointEventMatch" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Match ? "Keep" : ""),
                "watchpointEventCreation" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.New ? "Keep" : ""),
                "watchpointEventDeletion" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Delete ? "Keep" : ""),
                "watchpointEventRetyping" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Retype ? "Keep" : ""),
                "watchpointEventAssignment" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.SetAttributes ? "Keep" : ""),
                "watchpointAbort" });

            env.PrintInstructionsSeparateByNewline(watchpointDetermineEventTypeToConfigureMenu, "What event to listen to?\n", "");

            do
            {
                char character = env.LetUserChoose(watchpointDetermineEventTypeToConfigureMenu);
                switch(character)
                {
                case '0':
                    return SubruleDebuggingEvent.Add;
                case '1':
                    return SubruleDebuggingEvent.Rem;
                case '2':
                    return SubruleDebuggingEvent.Emit;
                case '3':
                    return SubruleDebuggingEvent.Halt;
                case '4':
                    return SubruleDebuggingEvent.Highlight;
                case '5':
                    return SubruleDebuggingEvent.Match;
                case '6':
                    return SubruleDebuggingEvent.New;
                case '7':
                    return SubruleDebuggingEvent.Delete;
                case '8':
                    return SubruleDebuggingEvent.Retype;
                case '9':
                    return SubruleDebuggingEvent.SetAttributes;
                case 'a':
                    return SubruleDebuggingEvent.Undefined;
                case 'k':
                    return cr.DebuggingEvent;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        private SubruleMesssageMatchingMode DetermineMessageAndMessageMatchingMode(SubruleDebuggingConfigurationRule cr, out string message)
        {
            env.WriteLine("Enter the subrule message to match.");
            if(cr != null)
                env.WriteLine("Empty string for " + cr.MessageToMatch);
            else
                env.WriteLine("Empty string to abort.");

            message = env.ReadLine();
            if(message.Length == 0)
            {
                if(cr != null)
                    message = cr.MessageToMatch;
                else
                    return SubruleMesssageMatchingMode.Undefined;
            }

            UserChoiceMenu watchpointMatchSubruleMessageMenu = new UserChoiceMenu(UserChoiceMenuNames.WatchpointMatchSubruleMessageMenu, new string[] {
                "watchpointMessageMatchingEquals" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Equals ? "Keep" : ""),
                "watchpointMessageMatchingStartsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.StartsWith ? "Keep" : ""),
                "watchpointMessageMatchingEndsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.EndsWith ? "Keep" : ""),
                "watchpointMessageMatchingContains" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Contains ? "Keep" : ""),
                "watchpointAbort" });

            env.PrintInstructionsSeparateByNewline(watchpointMatchSubruleMessageMenu, "How to match the subrule message?\n", "");

            do
            {
                switch(env.LetUserChoose(watchpointMatchSubruleMessageMenu))
                {
                case '0':
                    return SubruleMesssageMatchingMode.Equals;
                case '1':
                    return SubruleMesssageMatchingMode.StartsWith;
                case '2':
                    return SubruleMesssageMatchingMode.EndsWith;
                case '3':
                    return SubruleMesssageMatchingMode.Contains;
                case 'a':
                    return SubruleMesssageMatchingMode.Undefined;
                case 'k':
                    return cr.MessageMatchingMode;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        private IAction DetermineAction(SubruleDebuggingConfigurationRule cr)
        {
            do
            {
                env.WriteLine("Enter the name of the action to match.");
                if(cr != null)
                    env.WriteLine("Empty string for " + cr.ActionToMatch.PackagePrefixedName);
                else
                    env.WriteLine("Empty string to abort.");

                String actionName = env.ReadLine();
                if(actionName.Length == 0)
                {
                    if(cr != null)
                        return cr.ActionToMatch;
                    else
                        return null;
                }

                IAction action = debuggerProcEnv.ProcEnv.Actions.GetAction(actionName);
                if(action == null)
                    env.WriteLine("Unknown action: " + actionName);
                else
                    return action;
            }
            while(true);
        }

        private bool DetermineMatchGraphElementMode(SubruleDebuggingConfigurationRule cr, 
            out string graphElementName, out GrGenType graphElementType, out bool only)
        {
            graphElementName = null;
            graphElementType = null;
            only = false;

            SubruleDebuggingMatchGraphElementMode mode = DetermineMatchGraphElementMode(cr);
            if(mode == SubruleDebuggingMatchGraphElementMode.Undefined)
                return true;

            if(mode == SubruleDebuggingMatchGraphElementMode.ByName)
            {
                bool abort = DetermineMatchGraphElementByName(cr, out graphElementName);
                if(abort)
                    return true;
            }
            else
            {
                bool abort = DetermineMatchGraphElementByType(cr, out graphElementType);
                if(abort)
                    return true;

                SubruleDebuggingMatchGraphElementByTypeMode byTypeMode = DetermineMatchGraphElementByTypeMode(cr);
                if(byTypeMode == SubruleDebuggingMatchGraphElementByTypeMode.Undefined)
                    return true;
            }

            return false;
        }

        private SubruleDebuggingMatchGraphElementMode DetermineMatchGraphElementMode(SubruleDebuggingConfigurationRule cr)
        {
            UserChoiceMenu watchpointDetermineMatchGraphElementModeMenu = new UserChoiceMenu(UserChoiceMenuNames.WatchpointDetermineMatchGraphElementModeMenu, new string[] {
                "watchpointGraphElementMatchingByName" + (cr != null && cr.NameToMatch != null ? "Keep" : ""),
                "watchpointGraphElementMatchingByType" + (cr != null && cr.NameToMatch == null ? "Keep" : ""),
                "watchpointAbort" });

            env.PrintInstructionsSeparateByNewline(watchpointDetermineMatchGraphElementModeMenu, "Match graph element based on name or based on type?\n", "");

            do
            {
                switch(env.LetUserChoose(watchpointDetermineMatchGraphElementModeMenu))
                {
                case '0':
                    return SubruleDebuggingMatchGraphElementMode.ByName;
                case '1':
                    return SubruleDebuggingMatchGraphElementMode.ByType;
                case 'a':
                    return SubruleDebuggingMatchGraphElementMode.Undefined;
                case 'k':
                    return cr.NameToMatch != null ?
                        SubruleDebuggingMatchGraphElementMode.ByName :
                        SubruleDebuggingMatchGraphElementMode.ByType;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        private bool DetermineMatchGraphElementByName(SubruleDebuggingConfigurationRule cr,
            out string graphElementName)
        {
            env.WriteLine("Enter the graph element name to match.");
            if(cr != null)
                env.WriteLine("Empty string for " + cr.NameToMatch);
            else
                env.WriteLine("Empty string to abort.");

            graphElementName = env.ReadLine();
            if(graphElementName.Length == 0)
            {
                if(cr != null)
                    graphElementName = cr.NameToMatch;
                else
                    return true;
            }

            return false;
        }

        private bool DetermineMatchGraphElementByType(SubruleDebuggingConfigurationRule cr,
            out GrGenType graphElementType)
        {
            while(true)
            {
                env.WriteLine("Enter the type of the graph element to match.");
                if(cr != null)
                    env.WriteLine("Empty string for " + cr.TypeToMatch.PackagePrefixedName);
                else
                    env.WriteLine("Empty string to abort.");

                String graphElementTypeName = env.ReadLine();
                if(graphElementTypeName.Length == 0)
                {
                    if(cr != null)
                    {
                        graphElementType = cr.TypeToMatch;
                        break;
                    }
                    else
                    {
                        graphElementType = null;
                        return true;
                    }
                }

                graphElementType = env.GetGraphElementType(graphElementTypeName);
                if(graphElementType == null)
                    env.WriteLine("Unknown graph element type: " + graphElementTypeName);
                else
                    break;
            }

            return false;
        }

        private SubruleDebuggingMatchGraphElementByTypeMode DetermineMatchGraphElementByTypeMode(SubruleDebuggingConfigurationRule cr)
        {
            UserChoiceMenu watchpointDetermineMatchGraphElementByTypeModeMenu = new UserChoiceMenu(UserChoiceMenuNames.WatchpointDetermineMatchGraphElementByTypeModeMenu, new string[] {
                "watchpointGraphElementMatchingByTypeAlsoSubtypes" + (cr != null && !cr.OnlyThisType ? "Keep" : ""),
                "watchpointGraphElementMatchingByTypeOnlyTheType" + (cr != null && cr.OnlyThisType ? "Keep" : ""),
                "watchpointAbort" });

            env.PrintInstructionsSeparateByNewline(watchpointDetermineMatchGraphElementByTypeModeMenu, "Only the graph element type or also subtypes?\n", "");

            while(true)
            {
                switch(env.LetUserChoose(watchpointDetermineMatchGraphElementByTypeModeMenu))
                {
                case '0':
                    return SubruleDebuggingMatchGraphElementByTypeMode.IncludingSubtypes;
                case '1':
                    return SubruleDebuggingMatchGraphElementByTypeMode.OnlyType;
                case 'a':
                    return SubruleDebuggingMatchGraphElementByTypeMode.Undefined;
                case 'k':
                    return cr.OnlyThisType ? 
                        SubruleDebuggingMatchGraphElementByTypeMode.OnlyType :
                        SubruleDebuggingMatchGraphElementByTypeMode.IncludingSubtypes;
                default:
                    throw new Exception("Internal error");
                }
            }
        }

        private SubruleDebuggingDecision DetermineDecisionAction(SubruleDebuggingConfigurationRule cr)
        {
            // edit or keep decision action
            UserChoiceMenu watchpointDetermineDecisionActionMenu = new UserChoiceMenu(UserChoiceMenuNames.WatchpointDetermineDecisionActionMenu, new string[] {
                "watchpointDecisionActionBreak" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Break ? "Keep" : ""),
                "watchpointDecisionActionContinue" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Continue ? "Keep" : ""),
                "watchpointAbort" });

            env.PrintInstructionsSeparateByNewline(watchpointDetermineDecisionActionMenu, "How to react when the event is triggered?\n", "");

            do
            {
                char character = env.LetUserChoose(watchpointDetermineDecisionActionMenu);
                switch(character)
                {
                case '0':
                    return SubruleDebuggingDecision.Break;
                case '1':
                    return SubruleDebuggingDecision.Continue;
                case 'a':
                    return SubruleDebuggingDecision.Undefined;
                case 'k':
                    return cr.DecisionOnMatch;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        private SequenceExpression DetermineCondition(SubruleDebuggingConfigurationRule cr, 
            SubruleDebuggingEvent sde, IAction action, GrGenType graphElementType)
        {
            // edit or keep condition if type action or graph change
            do
            {
                env.WriteLine("Conditional rule via sequence expression?");
                if(cr != null && cr.IfClause != null)
                    env.WriteLine("Press enter to take over " + cr.IfClause.Symbol + ", enter \"-\" to clear the condition, otherwise enter the sequence expression to apply.");
                else
                    env.WriteLine("Press enter if you don't want to add an if part, otherwise enter the sequence expression to apply.");

                String ifClauseStr = env.ReadLine();
                if(ifClauseStr.Length == 0)
                {
                    if(cr != null)
                        return cr.IfClause;
                    else
                        return null;
                }
                if(ifClauseStr == "-")
                    return null;

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
                    SequenceParserEnvironmentInterpretedDebugEventCondition parserEnv = new SequenceParserEnvironmentInterpretedDebugEventCondition(debuggerProcEnv.ProcEnv.Actions, ruleOfMatchThis, typeOfGraphElementThis);
                    List<String> warnings = new List<String>();
                    SequenceExpression ifClause = SequenceParser.ParseSequenceExpression(ifClauseStr, predefinedVariables, parserEnv, warnings);
                    foreach(string warning in warnings)
                    {
                        env.WriteLine("The sequence expression for the if clause reported back: " + warning);
                    }
                    return ifClause;
                }
                catch(SequenceParserException ex)
                {
                    env.WriteLine("Unable to parse sequence expression");
                    DebuggerEnvironment.HandleSequenceParserException(ex);
                }
                catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
                {
                    env.WriteLine("Unable to parse sequence expression: " + ex.Message);
                }
                catch(Exception ex)
                {
                    env.WriteLine("Unable to parse sequence expression : " + ex);
                }
            }
            while(true);
        }
    }
}
