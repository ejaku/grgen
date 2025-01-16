/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// Class used to print debugger output in text form to the user, see IDisplayer for more comments.
    /// Sits in between the debugger and the debugger environment.
    /// </summary>
    public class Printer : IDisplayer
    {
        private IDebuggerEnvironment env;
        private SequencePrinter sequencePrinter;

        public Printer(IDebuggerEnvironment env)
        {
            this.env = env;
            sequencePrinter = new SequencePrinter(env);
        }

        public void BeginOfDisplay(string header)
        {
            env.Clear();
            if(header.Length > 0)
                env.WriteLineDataRendering(header);
        }

        public void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequencePrinter.DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix);
        }

        public void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequencePrinter.DisplaySequence(seq, context, nestingLevel, prefix, postfix);
        }

        public void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequencePrinter.DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix);
        }

        public void DisplayLine(string lineToBeShown)
        {
            env.WriteLineDataRendering(lineToBeShown);
        }
    }
}
