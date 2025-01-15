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
        private SequencePrinter sequencePrinter;

        public Printer(IDebuggerEnvironment env)
        {
            sequencePrinter = new SequencePrinter(env);
        }

        public void BeginOfDisplay(string header)
        {
            ; // TODO: Clear, print header
        }

        public void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel)
        {
            sequencePrinter.DisplaySequenceBase(seqBase, context, nestingLevel);
        }

        public void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel)
        {
            sequencePrinter.DisplaySequence(seq, context, nestingLevel);
        }

        public void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel)
        {
            sequencePrinter.DisplaySequenceExpression(seqExpr, context, nestingLevel);
        }

        public void DisplayLine(string lineToBeShown)
        {
            ; // TODO: write line
        }

        public void EndOfDisplay(string emptyPlaceholder)
        {
            ; // TODO: print newline
        }
    }
}
