/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;
using System.Collections;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// Common base class with shared functionality for displaying debugger data.
    /// It serves as the parent class for a Printer printing to the textual console and a Renderer rendering as a graph.
    /// </summary>
    public abstract class Displayer : IDisplayer
    {
        public abstract void BeginOfDisplay(string header);
        public abstract void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix);
        public abstract void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix);
        public abstract void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix);
        public abstract string DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName);
        public abstract string DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName);
        public abstract string DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName);
        public abstract void DisplayObject(object obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv);
        public abstract void DisplayClassObject(IObject obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv);
        public abstract void DisplayTransientClassObject(ITransientObject obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv);
        public abstract void DisplayLine(string lineToBeShown);

        public void DisplayCallStacks(SequenceBase[] callStack, SubruleComputation[] subruleStack, bool fullSubruleTracesEntries)
        {
            string groupNodeName = null;
            DisplaySequenceContext contextTrace = new DisplaySequenceContext();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                string previousGroupNodeName = groupNodeName;
                groupNodeName = CreateGroupNode(GroupNodeTypes.CallStackFrame);

                SequenceBase currSeq = callStack[i].GetCurrentlyExecutedSequenceBase();
                contextTrace.highlightSeq = currSeq;
                DisplaySequenceBase(callStack[i], contextTrace, callStack.Length - i, "", "", groupNodeName);

                if(previousGroupNodeName != null)
                    LinkTo(previousGroupNodeName, groupNodeName, "called");
            }
            if(subruleStack != null)
            {
                string firstNodeName = PrintDebugTracesStack(subruleStack, fullSubruleTracesEntries);
                LinkTo(groupNodeName, firstNodeName, "entered");
            }
            Show();
        }

        public void DisplayVariables(SequenceBase seqStart, SequenceBase seq, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            PrintGlobalVariables(debuggerProcEnv);
            PrintVisited(debuggerProcEnv);
            PrintVariables(seqStart, seq, debuggerProcEnv, null);
            Show();
        }

        public void DisplayFullState(SequenceBase[] callStack, SubruleComputation[] subruleStack, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            PrintGlobalVariables(debuggerProcEnv);
            PrintVisited(debuggerProcEnv);

            string groupNodeName = null;
            DisplaySequenceContext contextTrace = new DisplaySequenceContext();
            for(int i = callStack.Length - 1; i >= 0; --i)
            {
                string previousGroupNodeName = groupNodeName;
                groupNodeName = CreateGroupNode(GroupNodeTypes.CallStackFrame);

                SequenceBase currSeq = callStack[i].GetCurrentlyExecutedSequenceBase();
                contextTrace.highlightSeq = currSeq;
                DisplaySequenceBase(callStack[i], contextTrace, callStack.Length - i, "", "", groupNodeName);
                PrintVariables(callStack[i], currSeq != null ? currSeq : callStack[i], debuggerProcEnv, groupNodeName);

                if(previousGroupNodeName != null)
                    LinkTo(previousGroupNodeName, groupNodeName, "called");
            }
            if(subruleStack != null)
            {
                string firstNodeName = PrintDebugTracesStack(subruleStack, true);
                LinkTo(groupNodeName, firstNodeName, "entered");
            }
            Show();
        }

        protected abstract void PrintGlobalVariables(DebuggerGraphProcessingEnvironment debuggerProcEnv);
        protected abstract void PrintVisited(DebuggerGraphProcessingEnvironment debuggerProcEnv);
        protected abstract string PrintVariables(SequenceBase seqStart, SequenceBase seq, DebuggerGraphProcessingEnvironment debuggerProcEnv, string groupNodeName);
        protected abstract string PrintDebugTracesStack(SubruleComputation[] computationsEnteredStack, bool full);

        // TODO: shouldn't this be a function in EmitHelper, isn't it already available there?
        protected void VariableToString(object value, out string type, out string content, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            if(value is IDictionary)
                EmitHelper.ToString((IDictionary)value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
            else if(value is IList)
                EmitHelper.ToString((IList)value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
            else if(value is IDeque)
                EmitHelper.ToString((IDeque)value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
            else
                EmitHelper.ToString(value, out type, out content, null, debuggerProcEnv.ProcEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null);
        }

        protected enum GroupNodeTypes
        {
            CallStackFrame,
            LocalVariables,
            GlobalVariables,
            VisitedFlags
        }

        protected abstract string CreateGroupNode(GroupNodeTypes groupNodeType);
        protected abstract string LinkTo(string sourceNodeName, string targetNodeName, string label);
        protected abstract void Show();
    }
}
