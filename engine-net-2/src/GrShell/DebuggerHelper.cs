/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
using System.Text;

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

        /// <summary> If not null, gives the sequences to choose amongst </summary>
        public List<Sequence> sequences;
        /// <summary> If not null, gives the matches of the sequences to choose amongst </summary>
        public List<IMatches> matches;

        public PrintSequenceContext(IWorkaround workaround)
        {
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

            sequences = null;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    public enum SubruleComputationType
    {
        Entry,
        Exit,
        Emit,
        Halt,
        Highlight
    }

    public class SubruleComputation
    {
        public SubruleComputation(IGraph graph, SubruleComputationType type, string message, params object[] values)
        {
            this.type = type;
            this.message = message;
            this.parameters = new List<string>();
            for(int i = 0; i < values.Length; ++i)
            {
                parameters.Add(EmitHelper.ToStringAutomatic(values[i], graph));
            }
            this.fakeEntry = false;
        }

        public SubruleComputation(string message)
        {
            this.type = SubruleComputationType.Entry;
            this.message = message;
            this.parameters = new List<string>();
            this.fakeEntry = true;
        }

        public string ToString(bool full)
        {
            StringBuilder sb = new StringBuilder();
            if(fakeEntry)
                sb.Append("(");
            switch(type)
            {
                case SubruleComputationType.Entry: sb.Append("Entry "); break;
                case SubruleComputationType.Exit: sb.Append("Exit "); break;
                case SubruleComputationType.Emit: sb.Append("Emit "); break;
                case SubruleComputationType.Halt: sb.Append("Halt "); break;
                case SubruleComputationType.Highlight: sb.Append("Highlight "); break;
            }
            sb.Append(message);
            for(int i = 0; i < parameters.Count; ++i)
            {
                sb.Append(" ");
                if(full)
                    sb.Append(parameters[i]);
                else
                {
                    if(type == SubruleComputationType.Entry && parameters.Count == 1 && message.Contains("exec")) // print full embedded exec that is entered
                        sb.Append(parameters[i]);
                    else
                        sb.Append(EmitHelper.Clip(parameters[i], 120));
                }
            }
            if(fakeEntry)
                sb.Append(")");
            return sb.ToString();
        }

        // specifies the type of this subrule computation/message
        public SubruleComputationType type;

        // the message/name of the computation entered
        public string message;

        // the additional parameters, transfered into string encoding
        public List<string> parameters;

        // true if this is an entry of an action, added by the debugger as a help, 
        // so the full state is contained on the traces stack
        bool fakeEntry;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    public enum SubruleMesssageMatchingMode
    {
        Undefined,
        Equals,
        StartsWith,
        EndsWith,
        Contains
    }

    public enum SubruleDebuggingEvent
    {
        Add,
        Rem,
        Emit,
        Halt,
        Highlight,
        Match,
        New,
        Delete,
        Retype,
        SetAttributes
    }

    public enum SubruleDebuggingDecision
    {
        Undefined,
        Break,
        Continue
    }

    public class SubruleDebuggingConfigurationRule
    {
        private SubruleDebuggingEvent debuggingEvent;
        private string messageToMatch;
        private SubruleMesssageMatchingMode messageMatchingMode;
        private IAction actionToMatch;
        private GrGenType typeToMatch;
        private bool onlyThisType;
        private string nameToMatch;
        private SubruleDebuggingDecision decisionOnMatch;
        private SequenceExpression ifClause;
        private bool enabled = true;

        public SubruleDebuggingConfigurationRule(SubruleDebuggingEvent sde, string message, 
            SubruleMesssageMatchingMode smmm, SubruleDebuggingDecision sdd)
        {
            this.debuggingEvent = sde;
            this.messageMatchingMode = smmm;
            this.messageToMatch = message;
            this.decisionOnMatch = sdd;
        }

        public SubruleDebuggingConfigurationRule(SubruleDebuggingEvent sde, IAction action,
            SubruleDebuggingDecision sdd, SequenceExpression ifClause)
        {
            this.debuggingEvent = sde;
            this.actionToMatch = action;
            this.decisionOnMatch = sdd;
            this.ifClause = ifClause;
        }
        
        public SubruleDebuggingConfigurationRule(SubruleDebuggingEvent sde, string graphElementName,
            SubruleDebuggingDecision sdd, SequenceExpression ifClause)
        {
            this.debuggingEvent = sde;
            this.nameToMatch = graphElementName;
            this.decisionOnMatch = sdd;
            this.ifClause = ifClause;
        }

        public SubruleDebuggingConfigurationRule(SubruleDebuggingEvent sde, GrGenType graphElementType,
            bool only, SubruleDebuggingDecision sdd, SequenceExpression ifClause)
        {
            this.debuggingEvent = sde;
            this.typeToMatch = graphElementType;
            this.onlyThisType = only;
            this.decisionOnMatch = sdd;
            this.ifClause = ifClause;
        }

        public SubruleDebuggingEvent DebuggingEvent { get { return debuggingEvent; } }
        public string MessageToMatch { get { return messageToMatch; } }
        public SubruleMesssageMatchingMode MessageMatchingMode { get { return messageMatchingMode; } }
        public IAction ActionToMatch { get { return actionToMatch; } }
        public GrGenType TypeToMatch { get { return typeToMatch; } }
        public bool OnlyThisType { get { return onlyThisType; } }
        public string NameToMatch { get { return nameToMatch; } }
        public SubruleDebuggingDecision DecisionOnMatch { get { return decisionOnMatch; } }
        public SequenceExpression IfClause { get { return ifClause; } }
        public bool Enabled { get { return enabled; } set { enabled = value; } }

        public SubruleDebuggingDecision Decide(SubruleDebuggingEvent sde, object data, IGraphProcessingEnvironment procEnv)
        {
            if(!enabled)
                return SubruleDebuggingDecision.Undefined;
            if(debuggingEvent != sde)
                return SubruleDebuggingDecision.Undefined;

            switch(sde)
            {
                case SubruleDebuggingEvent.Add:
                case SubruleDebuggingEvent.Rem:
                case SubruleDebuggingEvent.Emit:
                case SubruleDebuggingEvent.Halt:
                case SubruleDebuggingEvent.Highlight:
                    {
                        string message = (string)data;
                        switch(messageMatchingMode)
                        {
                            case SubruleMesssageMatchingMode.Equals:
                                if(message == messageToMatch)
                                    return decisionOnMatch;
                                break;
                            case SubruleMesssageMatchingMode.StartsWith:
                                if(message.StartsWith(messageToMatch))
                                    return decisionOnMatch;
                                break;
                            case SubruleMesssageMatchingMode.EndsWith:
                                if(message.EndsWith(messageToMatch))
                                    return decisionOnMatch;
                                break;
                            case SubruleMesssageMatchingMode.Contains:
                                if(message.Contains(messageToMatch))
                                    return decisionOnMatch;
                                break;
                            default:
                                throw new Exception("INTERNAL FAILURE: unkonwn message matching mode");
                        }
                    }
                    return SubruleDebuggingDecision.Undefined;

                case SubruleDebuggingEvent.Match:
                    {
                        IMatches matches = (IMatches)data;
                        if(matches.Producer == actionToMatch)
                        {
                            if(ifClause != null)
                            {
                                object oldThis = procEnv.GetVariableValue("this");
                                bool result = false;
                                foreach(IMatch match in matches)
                                {
                                    procEnv.SetVariableValue("this", match);
                                    if((bool)ifClause.Evaluate(procEnv))
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                                procEnv.SetVariableValue("this", oldThis);
                                if(result)
                                    return decisionOnMatch;
                            }
                            else
                                return decisionOnMatch;
                        }
                        return SubruleDebuggingDecision.Undefined;
                    }

                case SubruleDebuggingEvent.New:
                case SubruleDebuggingEvent.Delete:
                case SubruleDebuggingEvent.Retype:
                case SubruleDebuggingEvent.SetAttributes:
                    {
                        IGraphElement elem = (IGraphElement)data;
                        if(nameToMatch != null)
                        {
                            if(procEnv.NamedGraph.GetElementName(elem) == nameToMatch)
                            {
                                if(If(elem, procEnv))
                                    return decisionOnMatch;
                            }
                        }
                        if(typeToMatch != null)
                        {
                            if(elem.Type is NodeType && typeToMatch is NodeType && elem.Type.IsA(typeToMatch)
                                || elem.Type is EdgeType && typeToMatch is EdgeType && elem.Type.IsA(typeToMatch))
                            {
                                if(onlyThisType)
                                {
                                    if(typeToMatch.IsA(elem.Type))
                                    {
                                        if(If(elem, procEnv))
                                            return decisionOnMatch;
                                    }
                                }
                                else
                                {
                                    if(If(elem, procEnv))
                                        return decisionOnMatch;
                                }
                            }
                        }
                        return SubruleDebuggingDecision.Undefined;
                    }

                default:
                    return SubruleDebuggingDecision.Undefined;
            }
        }

        private bool If(IGraphElement element, IGraphProcessingEnvironment procEnv)
        {
            if(ifClause != null)
            {
                object oldThis = procEnv.GetVariableValue("this");
                procEnv.SetVariableValue("this", element);
                bool result = (bool)ifClause.Evaluate(procEnv);
                procEnv.SetVariableValue("this", oldThis);
                return result;
            }
            return true;
        }

        public string ToString(object data, INamedGraph graph, params object[] additionalData)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ToString(debuggingEvent));
            sb.Append(" ");

            switch(debuggingEvent)
            {
                case SubruleDebuggingEvent.Add:
                case SubruleDebuggingEvent.Rem:
                case SubruleDebuggingEvent.Emit:
                case SubruleDebuggingEvent.Halt:
                case SubruleDebuggingEvent.Highlight:
                    {
                        string message = (string)data;
                        sb.Append("\"");
                        sb.Append(message);
                        sb.Append("\"");
                        for(int i = 0; i < additionalData.Length; ++i)
                        {
                            sb.Append(" ");
                            sb.Append(EmitHelper.Clip(EmitHelper.ToStringAutomatic(additionalData[i], graph), 120));
                        }
                        break;
                    }
                case SubruleDebuggingEvent.Match:
                    {
                        IMatches matches = (IMatches)data;
                        sb.Append(matches.Producer.PackagePrefixedName);
                        break;
                    }
                case SubruleDebuggingEvent.New:
                case SubruleDebuggingEvent.Delete:
                case SubruleDebuggingEvent.Retype:
                case SubruleDebuggingEvent.SetAttributes:
                    {
                        IGraphElement elem = (IGraphElement)data;
                        sb.Append(graph.GetElementName(elem));
                        sb.Append(":");
                        sb.Append(elem.Type.Name);
                        if(additionalData.Length > 0)
                        {
                            sb.Append("."); // the attribute name
                            sb.Append((string)additionalData[0]);
                        }
                        break;
                    }
                default:
                    return "INTERNAL FAILURE, unknown SubruleDebuggingConfigurationRule";
            }

            sb.Append(" triggers ");
            sb.Append(ToString());

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(!enabled)
                sb.Append("(disabled) ");
            sb.Append("on ");
            sb.Append(ToString(debuggingEvent));
            sb.Append(" ");
            switch(debuggingEvent)
            {
                case SubruleDebuggingEvent.Add: 
                case SubruleDebuggingEvent.Rem: 
                case SubruleDebuggingEvent.Emit: 
                case SubruleDebuggingEvent.Halt: 
                case SubruleDebuggingEvent.Highlight: 
                    {
                        sb.Append(ToString(messageMatchingMode));
                        sb.Append("(\"");
                        sb.Append(messageToMatch);
                        sb.Append("\") ");
                        break;
                    }
                case SubruleDebuggingEvent.Match: 
                    {
                        sb.Append(actionToMatch.PackagePrefixedName);
                        sb.Append(" ");
                        break;
                    }
                case SubruleDebuggingEvent.New: 
                case SubruleDebuggingEvent.Delete: 
                case SubruleDebuggingEvent.Retype: 
                case SubruleDebuggingEvent.SetAttributes: 
                    {
                        if(nameToMatch != null)
                        {
                            sb.Append("@(");
                            sb.Append(nameToMatch);
                            sb.Append(") ");
                        }
                        else
                        {
                            if(onlyThisType)
                                sb.Append("only ");
                            sb.Append(typeToMatch.PackagePrefixedName);
                            sb.Append(" ");
                        }
                        break;
                    }
                default:
                    return "INTERNAL FAILURE, unknown SubruleDebuggingConfigurationRule";
            }

            if(decisionOnMatch == SubruleDebuggingDecision.Break)
                sb.Append("break ");
            else if(decisionOnMatch == SubruleDebuggingDecision.Continue)
                sb.Append("continue ");
            
            if(ifClause != null)
                sb.Append(ifClause.Symbol);

            return sb.ToString();
        }

        public string ToString(SubruleDebuggingEvent evt)
        {
            switch(evt)
            {
                case SubruleDebuggingEvent.Add: return "add";
                case SubruleDebuggingEvent.Rem: return "rem";
                case SubruleDebuggingEvent.Emit: return "emit";
                case SubruleDebuggingEvent.Halt: return "halt";
                case SubruleDebuggingEvent.Highlight: return "highlight";
                case SubruleDebuggingEvent.Match: return "match";
                case SubruleDebuggingEvent.New: return "new";
                case SubruleDebuggingEvent.Delete: return "delete";
                case SubruleDebuggingEvent.Retype: return "retype";
                case SubruleDebuggingEvent.SetAttributes: return "set attributes";
                default: return "INTERNAL FAILURE, unknown SubruleDebuggingEvent";
            }
        }

        public string ToString(SubruleMesssageMatchingMode mode)
        {
            switch(mode)
            {
                case SubruleMesssageMatchingMode.Equals: return "equals";
                case SubruleMesssageMatchingMode.StartsWith: return "startsWith";
                case SubruleMesssageMatchingMode.EndsWith: return "endsWith";
                case SubruleMesssageMatchingMode.Contains: return "contains";
                default: return "INTERNAL FAILURE, unknown SubruleMesssageMatchingMode";
            }
        }
    }

    public class SubruleDebuggingConfiguration
    {
        private List<SubruleDebuggingConfigurationRule> configurationRules;

        public SubruleDebuggingConfiguration()
        {
            configurationRules = new List<SubruleDebuggingConfigurationRule>();
        }

        public List<SubruleDebuggingConfigurationRule> ConfigurationRules { get { return configurationRules; } }

        public void Replace(int index, SubruleDebuggingConfigurationRule rule)
        {
            configurationRules[index] = rule;
        }

        public void Add(SubruleDebuggingConfigurationRule rule)
        {
            configurationRules.Add(rule);
        }

        public void Delete(int index)
        {
            configurationRules.RemoveAt(index);
        }

        public void Insert(SubruleDebuggingConfigurationRule rule, int index)
        {
            configurationRules.Insert(index, rule);
        }

        public void Insert(SubruleDebuggingConfigurationRule rule)
        {
            configurationRules.Add(rule);
        }

        public SubruleDebuggingDecision Decide(SubruleDebuggingEvent sde, object data, IGraphProcessingEnvironment procEnv, out SubruleDebuggingConfigurationRule cr)
        {
            foreach(SubruleDebuggingConfigurationRule rule in configurationRules)
            {
                SubruleDebuggingDecision result = rule.Decide(sde, data, procEnv);
                if(result != SubruleDebuggingDecision.Undefined)
                {
                    cr = rule;
                    return result;
                }
            }
            cr = null;
            return SubruleDebuggingDecision.Undefined;
        }
    }
}
