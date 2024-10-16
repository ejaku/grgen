/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        readonly DebuggerGraphProcessingEnvironment debuggerProcEnv;
        readonly IDebuggerEnvironment env;

        public WatchpointEditor(DebuggerGraphProcessingEnvironment debuggerProcEnv, IDebuggerEnvironment env)
        {
            this.debuggerProcEnv = debuggerProcEnv;
            this.env = env;
        }

        public void HandleWatchpoints()
        {
            env.outWriter.WriteLine("List of registered watchpoints:");
            for(int i = 0; i < debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count; ++i)
            {
                env.outWriter.WriteLine(i + " - " + debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[i].ToString());
            }

            env.outWriter.WriteLine("Press (e) to edit, (t) to toggle (enable/disable), or (d) to delete one of the watchpoints."
                + " Press (i) to insert at a specified position, or (p) to append a watchpoint."
                + " Press (a) to abort.");

            while(true)
            {
                int num = -1;
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'e':
                    num = QueryWatchpoint("edit");
                    if(num == -1)
                        break;
                    EditWatchpoint(num);
                    env.outWriter.WriteLine("Back from watchpoints to debugging.");
                    return;
                case 't':
                    num = QueryWatchpoint("toggle (enable/disable)");
                    if(num == -1)
                        break;
                    ToggleWatchpoint(num);
                    env.outWriter.WriteLine("Back from watchpoints to debugging.");
                    return;
                case 'd':
                    num = QueryWatchpoint("delete");
                    if(num == -1)
                        break;
                    DeleteWatchpoint(num);
                    env.outWriter.WriteLine("Back from watchpoints to debugging.");
                    return;
                case 'i':
                    num = QueryWatchpoint("insert");
                    if(num == -1)
                        break;
                    InsertWatchpoint(num);
                    env.outWriter.WriteLine("Back from watchpoints to debugging.");
                    return;
                case 'p':
                    AppendWatchpoint();
                    env.outWriter.WriteLine("Back from watchpoints to debugging.");
                    return;
                case 'a':
                    env.outWriter.WriteLine("Back from watchpoints to debugging.");
                    return;
                default:
                    env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (e)dit, (t)oggle, (d)elete, (i)nsert, a(p)pend, or (a)bort allowed! ");
                    break;
                }
            }
        }

        private int QueryWatchpoint(string action)
        {
            env.outWriter.Write("Enter number of watchpoint to " + action + " (-1 for abort): ");

            do
            {
                String numStr = env.inReader.ReadLine();
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < -1 || num >= debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count)
                    {
                        env.outWriter.WriteLine("You must specify a number between -1 and " + (debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count - 1) + "!");
                        continue;
                    }
                    return num;
                }
                env.outWriter.WriteLine("You must enter a valid integer number!");
            }
            while(true);
        }

        private void EditWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            cr = EditOrCreateRule(cr);
            if(cr == null)
                env.outWriter.WriteLine("aborted");
            else
            {
                env.outWriter.WriteLine("edited entry " + num + " - " + cr.ToString());
                debuggerProcEnv.SubruleDebugConfig.Replace(num, cr);
            }
        }

        private void ToggleWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            cr.Enabled = !cr.Enabled;
            env.outWriter.WriteLine("toggled entry " + num + " - " + cr.ToString());
        }

        private void DeleteWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = debuggerProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            debuggerProcEnv.SubruleDebugConfig.Delete(num);
            env.outWriter.WriteLine("deleted entry " + num + " - " + cr.ToString());
        }

        private void InsertWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = EditOrCreateRule(null);
            if(cr == null)
                env.outWriter.WriteLine("aborted");
            else
            {
                debuggerProcEnv.SubruleDebugConfig.Insert(cr, num);
                env.outWriter.WriteLine("inserted entry " + num + " - " + cr.ToString());
            }
        }

        private void AppendWatchpoint()
        {
            SubruleDebuggingConfigurationRule cr = EditOrCreateRule(null);
            if(cr == null)
                env.outWriter.WriteLine("aborted");
            else
            {
                debuggerProcEnv.SubruleDebugConfig.Insert(cr);
                env.outWriter.WriteLine("appended entry " + (debuggerProcEnv.SubruleDebugConfig.ConfigurationRules.Count - 1) + " - " + cr.ToString());
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
            env.outWriter.WriteLine("What event to listen to?");
            env.outWriter.Write("(0) subrule entry aka Debug::add" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Add ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(1) subrule exit aka Debug::rem" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Rem ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(2) subrule report aka Debug::emit" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Emit ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(3) subrule halt aka Debug::halt" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Halt ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(4) subrule highlight aka Debug::highlight" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Highlight ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(5) rule match" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Match ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(6) graph element creation" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.New ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(7) graph element deletion" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Delete ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(8) graph element retyping" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.Retype ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(9) graph element attribute assignment" + (cr != null && cr.DebuggingEvent == SubruleDebuggingEvent.SetAttributes ? " or (k)eep\n" : "\n"));
            env.outWriter.WriteLine("(a)bort");

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
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
                default:
                    if(key.KeyChar == 'k' && cr != null)
                        return cr.DebuggingEvent;
                    else
                    {
                        env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(9), (a)bort allowed! ");
                        break;
                    }
                }
            }
            while(true);
        }

        private SubruleMesssageMatchingMode DetermineMessageAndMessageMatchingMode(SubruleDebuggingConfigurationRule cr, out string message)
        {
            env.outWriter.WriteLine("Enter the subrule message to match.");
            if(cr != null)
                env.outWriter.WriteLine("Empty string for " + cr.MessageToMatch);
            else
                env.outWriter.WriteLine("Empty string to abort.");

            message = env.inReader.ReadLine();
            if(message.Length == 0)
            {
                if(cr != null)
                    message = cr.MessageToMatch;
                else
                    return SubruleMesssageMatchingMode.Undefined;
            }

            env.outWriter.WriteLine("How to match the subrule message?");
            env.outWriter.Write("(0) equals" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Equals ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(1) startsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.StartsWith ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(2) endsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.EndsWith ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(3) contains" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Contains ? " or (k)eep\n" : "\n"));
            env.outWriter.WriteLine("(a)bort");

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
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
                default:
                    if(key.KeyChar == 'k' && cr != null)
                        return cr.MessageMatchingMode;
                    else
                    {
                        env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(3), (a)bort allowed! ");
                        break;
                    }
                }
            }
            while(true);
        }

        private IAction DetermineAction(SubruleDebuggingConfigurationRule cr)
        {
            do
            {
                env.outWriter.WriteLine("Enter the name of the action to match.");
                if(cr != null)
                    env.outWriter.WriteLine("Empty string for " + cr.ActionToMatch.PackagePrefixedName);
                else
                    env.outWriter.WriteLine("Empty string to abort.");

                String actionName = env.inReader.ReadLine();
                if(actionName.Length == 0)
                {
                    if(cr != null)
                        return cr.ActionToMatch;
                    else
                        return null;
                }

                IAction action = debuggerProcEnv.ProcEnv.Actions.GetAction(actionName);
                if(action == null)
                    env.outWriter.WriteLine("Unknown action: " + actionName);
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
            env.outWriter.WriteLine("Match graph element based on name or based on type?");
            env.outWriter.Write("(0) by name" + (cr != null && cr.NameToMatch != null ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(1) by type" + (cr != null && cr.NameToMatch == null ? " or (k)eep\n" : "\n"));
            env.outWriter.WriteLine("(a)bort");

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case '0':
                    return SubruleDebuggingMatchGraphElementMode.ByName;
                case '1':
                    return SubruleDebuggingMatchGraphElementMode.ByType;
                case 'a':
                    return SubruleDebuggingMatchGraphElementMode.Undefined;
                default:
                    if(key.KeyChar == 'k' && cr != null)
                    {
                        return cr.NameToMatch != null ?
                            SubruleDebuggingMatchGraphElementMode.ByName :
                            SubruleDebuggingMatchGraphElementMode.ByType;
                    }
                    else
                    {
                        env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0), (1), (a)bort allowed! ");
                        break;
                    }
                }
            }
            while(true);
        }

        private bool DetermineMatchGraphElementByName(SubruleDebuggingConfigurationRule cr,
            out string graphElementName)
        {
            env.outWriter.WriteLine("Enter the graph element name to match.");
            if(cr != null)
                env.outWriter.WriteLine("Empty string for " + cr.NameToMatch);
            else
                env.outWriter.WriteLine("Empty string to abort.");

            graphElementName = env.inReader.ReadLine();
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
                env.outWriter.WriteLine("Enter the type of the graph element to match.");
                if(cr != null)
                    env.outWriter.WriteLine("Empty string for " + cr.TypeToMatch.PackagePrefixedName);
                else
                    env.outWriter.WriteLine("Empty string to abort.");

                String graphElementTypeName = env.inReader.ReadLine();
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
                    env.outWriter.WriteLine("Unknown graph element type: " + graphElementTypeName);
                else
                    break;
            }

            return false;
        }

        private SubruleDebuggingMatchGraphElementByTypeMode DetermineMatchGraphElementByTypeMode(SubruleDebuggingConfigurationRule cr)
        {
            env.outWriter.WriteLine("Only the graph element type or also subtypes?");
            env.outWriter.Write("(0) also subtypes" + (cr != null && !cr.OnlyThisType ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(1) only the type" + (cr != null && cr.OnlyThisType ? " or (k)eep\n" : "\n"));
            env.outWriter.WriteLine("(a)bort");

            while(true)
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case '0':
                    return SubruleDebuggingMatchGraphElementByTypeMode.IncludingSubtypes;
                case '1':
                    return SubruleDebuggingMatchGraphElementByTypeMode.OnlyType;
                case 'a':
                    return SubruleDebuggingMatchGraphElementByTypeMode.Undefined;
                default:
                    if(key.KeyChar == 'k' && cr != null)
                    {
                        return cr.OnlyThisType ? 
                            SubruleDebuggingMatchGraphElementByTypeMode.OnlyType :
                            SubruleDebuggingMatchGraphElementByTypeMode.IncludingSubtypes;
                    }
                    else
                    {
                        env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0), (1), (a)bort allowed! ");
                        break;
                    }
                }
            }
        }

        private SubruleDebuggingDecision DetermineDecisionAction(SubruleDebuggingConfigurationRule cr)
        {
            // edit or keep decision action
            env.outWriter.WriteLine("How to react when the event is triggered?");
            env.outWriter.Write("(0) break" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Break ? " or (k)eep\n" : "\n"));
            env.outWriter.Write("(1) continue" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Continue ? " or (k)eep\n" : "\n"));
            env.outWriter.WriteLine("(a)bort");

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case '0':
                    return SubruleDebuggingDecision.Break;
                case '1':
                    return SubruleDebuggingDecision.Continue;
                case 'a':
                    return SubruleDebuggingDecision.Undefined;
                default:
                    if(key.KeyChar == 'k' && cr != null)
                        return cr.DecisionOnMatch;
                    else
                    {
                        env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0), (1), (a)bort allowed! ");
                        break;
                    }
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
                env.outWriter.WriteLine("Conditional rule via sequence expression?");
                if(cr != null && cr.IfClause != null)
                    env.outWriter.WriteLine("Press enter to take over " + cr.IfClause.Symbol + ", enter \"-\" to clear the condition, otherwise enter the sequence expression to apply.");
                else
                    env.outWriter.WriteLine("Press enter if you don't want to add an if part, otherwise enter the sequence expression to apply.");

                String ifClauseStr = env.inReader.ReadLine();
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
                        env.outWriter.WriteLine("The sequence expression for the if clause reported back: " + warning);
                    }
                    return ifClause;
                }
                catch(SequenceParserException ex)
                {
                    env.outWriter.WriteLine("Unable to parse sequence expression");
                    DebuggerEnvironment.HandleSequenceParserException(ex);
                }
                catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
                {
                    env.outWriter.WriteLine("Unable to parse sequence expression: " + ex.Message);
                }
                catch(Exception ex)
                {
                    env.outWriter.WriteLine("Unable to parse sequence expression : " + ex);
                }
            }
            while(true);
        }
    }
}
