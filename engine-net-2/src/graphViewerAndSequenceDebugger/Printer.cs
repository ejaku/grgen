/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// Class used to print debugger output in text form to the user, see IDisplayer for more comments.
    /// Sits in between the debugger and the debugger environment.
    /// </summary>
    public class Printer : Displayer
    {
        private IDebuggerEnvironment env;
        private SequencePrinter sequencePrinter;

        public Printer(IDebuggerEnvironment env)
        {
            this.env = env;
            sequencePrinter = new SequencePrinter(env);
        }

        public override void BeginOfDisplay(string header)
        {
            env.Clear();
            if(header.Length > 0)
                env.WriteLineDataRendering(header);
        }

        public override void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequencePrinter.DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix, null);
        }

        public override void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequencePrinter.DisplaySequence(seq, context, nestingLevel, prefix, postfix, null);
        }

        public override void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequencePrinter.DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix, null);
        }

        public override string DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            return sequencePrinter.DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix, groupNodeName);
        }

        public override string DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            return sequencePrinter.DisplaySequence(seq, context, nestingLevel, prefix, postfix, groupNodeName);
        }

        public override string DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            return sequencePrinter.DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix, groupNodeName);
        }

        //public void DisplayCallStacks(SequenceBase[] callStack, SubruleComputation[] subruleStack, bool fullSubruleTracesEntries); inherited from Displayer
        //public void DisplayVariables(SequenceBase seqStart, SequenceBase seq, DebuggerGraphProcessingEnvironment debuggerProcEnv); inherited from Displayer
        //public void DisplayFullState(SequenceBase[] callStack, SubruleComputation[] subruleStack, DebuggerGraphProcessingEnvironment debuggerProcEnv); inherited from Displayer

        public override void DisplayObject(object obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            env.WriteLineDataRendering(EmitHelper.ToStringAutomatic(obj, procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, procEnv));
        }

        public override void DisplayClassObject(IObject obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            env.WriteLineDataRendering(EmitHelper.ToStringAutomatic(obj, procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, procEnv));
        }
        
        public override void DisplayTransientClassObject(ITransientObject obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            env.WriteLineDataRendering(EmitHelper.ToStringAutomatic(obj, procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, procEnv));
        }

        public override void DisplayLine(string lineToBeShown)
        {
            env.WriteLineDataRendering(lineToBeShown);
        }

        // ----------------------------------------------------------------

        protected override void PrintGlobalVariables(DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            DisplayLine("Available global (non null) variables:");
            foreach(Variable var in debuggerProcEnv.ProcEnv.Variables)
            {
                string type;
                string content;
                VariableToString(var.Value, out type, out content, debuggerProcEnv);
                DisplayLine("  " + var.Name + " = " + content + " : " + type);
            }
        }

        protected override void PrintVisited(DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            List<int> allocatedVisitedFlags = debuggerProcEnv.ProcEnv.NamedGraph.GetAllocatedVisitedFlags();
            StringBuilder sb = new StringBuilder();
            sb.Append("Allocated visited flags are: ");
            bool first = true;
            foreach(int allocatedVisitedFlag in allocatedVisitedFlags)
            {
                if(!first)
                    sb.Append(", ");
                sb.Append(allocatedVisitedFlag.ToString());
                first = false;
            }
            DisplayLine(sb.ToString() + ".");
        }

        protected override string PrintVariables(SequenceBase seqStart, SequenceBase seq, DebuggerGraphProcessingEnvironment debuggerProcEnv, string groupNodeName)
        {
            DisplayLine("Available local variables:");
            Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqStart.GetLocalVariables(seqVars, constructors, seq);
            List<SequenceVariable> arrayBuilder = new List<SequenceVariable>();
            foreach(SequenceVariable seqVar in seqVars.Keys)
            {
                string type;
                string content;
                VariableToString(seqVar.LocalVariableValue, out type, out content, debuggerProcEnv);
                DisplayLine("  " + seqVar.Name + " = " + content + " : " + type);
            }
            return null;
        }

        protected override string PrintDebugTracesStack(SubruleComputation[] computationsEnteredStack, bool full)
        {
            DisplayLine("Subrule traces stack is:");
            for(int i = 0; i < computationsEnteredStack.Length; ++i)
            {
                if(!full && computationsEnteredStack[i].type != SubruleComputationType.Entry)
                    continue;
                DisplayLine(computationsEnteredStack[i].ToString(full));
            }
            return null;
        }

        protected override string CreateGroupNode(GroupNodeTypes groupNodeType)
        {
            return null;
        }

        protected override string LinkTo(string sourceNodeName, string targetNodeName, string label)
        {
            return null;
        }

        protected override void Show()
        {
        }
    }
}
