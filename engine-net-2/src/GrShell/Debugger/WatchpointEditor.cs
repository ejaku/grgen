/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.grShell
{
    class WatchpointEditor
    {
        readonly ShellGraphProcessingEnvironment shellProcEnv;
        readonly IDebuggerEnvironment env;

        public WatchpointEditor(ShellGraphProcessingEnvironment shellProcEnv, IDebuggerEnvironment env)
        {
            this.shellProcEnv = shellProcEnv;
            this.env = env;
        }

        public void HandleWatchpoints()
        {
            Console.WriteLine("List of registered watchpoints:");
            for(int i = 0; i < shellProcEnv.SubruleDebugConfig.ConfigurationRules.Count; ++i)
            {
                Console.WriteLine(i + " - " + shellProcEnv.SubruleDebugConfig.ConfigurationRules[i].ToString());
            }

            Console.WriteLine("Press (e) to edit, (t) to toggle (enable/disable), or (d) to delete one of the watchpoints."
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

        private int QueryWatchpoint(string action)
        {
            Console.Write("Enter number of watchpoint to " + action + " (-1 for abort): ");

            do
            {
                String numStr = Console.ReadLine();
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < -1 || num >= shellProcEnv.SubruleDebugConfig.ConfigurationRules.Count)
                    {
                        Console.WriteLine("You must specify a number between -1 and " + (shellProcEnv.SubruleDebugConfig.ConfigurationRules.Count - 1) + "!");
                        continue;
                    }
                    return num;
                }
                Console.WriteLine("You must enter a valid integer number!");
            }
            while(true);
        }

        private void EditWatchpoint(int num)
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

        private void ToggleWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = shellProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            cr.Enabled = !cr.Enabled;
            Console.WriteLine("toggled entry " + num + " - " + cr.ToString());
        }

        private void DeleteWatchpoint(int num)
        {
            SubruleDebuggingConfigurationRule cr = shellProcEnv.SubruleDebugConfig.ConfigurationRules[num];
            shellProcEnv.SubruleDebugConfig.Delete(num);
            Console.WriteLine("deleted entry " + num + " - " + cr.ToString());
        }

        private void InsertWatchpoint(int num)
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

        private void AppendWatchpoint()
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
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(9), (a)bort allowed! ");
                        break;
                    }
                }
            }
            while(true);
        }

        private SubruleMesssageMatchingMode DetermineMessageAndMessageMatchingMode(SubruleDebuggingConfigurationRule cr, out string message)
        {
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
                    return SubruleMesssageMatchingMode.Undefined;
            }

            Console.WriteLine("How to match the subrule message?");
            Console.Write("(0) equals" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Equals ? " or (k)eep\n" : "\n"));
            Console.Write("(1) startsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.StartsWith ? " or (k)eep\n" : "\n"));
            Console.Write("(2) endsWith" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.EndsWith ? " or (k)eep\n" : "\n"));
            Console.Write("(3) contains" + (cr != null && cr.MessageMatchingMode == SubruleMesssageMatchingMode.Contains ? " or (k)eep\n" : "\n"));
            Console.WriteLine("(a)bort");

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
                        Console.WriteLine("Illegal choice (Key = " + key.Key
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
                Console.WriteLine("Enter the name of the action to match.");
                if(cr != null)
                    Console.WriteLine("Empty string for " + cr.ActionToMatch.PackagePrefixedName);
                else
                    Console.WriteLine("Empty string to abort.");

                String actionName = Console.ReadLine();
                if(actionName.Length == 0)
                {
                    if(cr != null)
                        return cr.ActionToMatch;
                    else
                        return null;
                }

                IAction action = shellProcEnv.ProcEnv.Actions.GetAction(actionName);
                if(action == null)
                    Console.WriteLine("Unknown action: " + actionName);
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
            Console.WriteLine("Match graph element based on name or based on type?");
            Console.Write("(0) by name" + (cr != null && cr.NameToMatch != null ? " or (k)eep\n" : "\n"));
            Console.Write("(1) by type" + (cr != null && cr.NameToMatch == null ? " or (k)eep\n" : "\n"));
            Console.WriteLine("(a)bort");

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
                        Console.WriteLine("Illegal choice (Key = " + key.Key
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
                    return true;
            }

            return false;
        }

        private bool DetermineMatchGraphElementByType(SubruleDebuggingConfigurationRule cr,
            out GrGenType graphElementType)
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
                    {
                        graphElementType = null;
                        return true;
                    }
                }

                graphElementType = env.GetGraphElementType(graphElementTypeName);
                if(graphElementType == null)
                    Console.WriteLine("Unknown graph element type: " + graphElementTypeName);
                else
                    break;
            }

            return false;
        }

        private SubruleDebuggingMatchGraphElementByTypeMode DetermineMatchGraphElementByTypeMode(SubruleDebuggingConfigurationRule cr)
        {
            Console.WriteLine("Only the graph element type or also subtypes?");
            Console.Write("(0) also subtypes" + (cr != null && !cr.OnlyThisType ? " or (k)eep\n" : "\n"));
            Console.Write("(1) only the type" + (cr != null && cr.OnlyThisType ? " or (k)eep\n" : "\n"));
            Console.WriteLine("(a)bort");

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
                        Console.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0), (1), (a)bort allowed! ");
                        break;
                    }
                }
            }
        }

        private SubruleDebuggingDecision DetermineDecisionAction(SubruleDebuggingConfigurationRule cr)
        {
            // edit or keep decision action
            Console.WriteLine("How to react when the event is triggered?");
            Console.Write("(0) break" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Break ? " or (k)eep\n" : "\n"));
            Console.Write("(1) continue" + (cr != null && cr.DecisionOnMatch == SubruleDebuggingDecision.Continue ? " or (k)eep\n" : "\n"));
            Console.WriteLine("(a)bort");

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
                        Console.WriteLine("Illegal choice (Key = " + key.Key
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
                Console.WriteLine("Conditional rule via sequence expression?");
                if(cr != null && cr.IfClause != null)
                    Console.WriteLine("Press enter to take over " + cr.IfClause.Symbol + ", enter \"-\" to clear the condition, otherwise enter the sequence expression to apply.");
                else
                    Console.WriteLine("Press enter if you don't want to add an if part, otherwise enter the sequence expression to apply.");

                String ifClauseStr = Console.ReadLine();
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
                    SequenceParserEnvironmentInterpretedDebugEventCondition parserEnv = new SequenceParserEnvironmentInterpretedDebugEventCondition(shellProcEnv.ProcEnv.Actions, ruleOfMatchThis, typeOfGraphElementThis);
                    List<String> warnings = new List<String>();
                    SequenceExpression ifClause = SequenceParser.ParseSequenceExpression(ifClauseStr, predefinedVariables, parserEnv, warnings);
                    foreach(string warning in warnings)
                    {
                        Console.WriteLine("The sequence expression for the if clause reported back: " + warning);
                    }
                    return ifClause;
                }
                catch(SequenceParserException ex)
                {
                    Console.WriteLine("Unable to parse sequence expression");
                    env.HandleSequenceParserException(ex);
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
            while(true);
        }
    }
}
