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
    /// Class used to render debugger output in graph form to the user, see IDisplayer for more comments.
    /// Sits in between the debugger and the debugger environment.
    /// </summary>
    public class Renderer : IDisplayer
    {
        private SequenceRenderer sequenceRenderer;

        public Renderer(IDebuggerEnvironment env)
        {
            sequenceRenderer = new SequenceRenderer(env);
        }

        public void BeginOfDisplay(string header)
        {
            ; // TODO: Clear, ignore header
        }

        public void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix);
        }

        public void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequence(seq, context, nestingLevel, prefix, postfix);
        }

        public void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix);
        }

        public void DisplayLine(string lineToBeShown)
        {
            ; // TODO: render linked list of lines
        }
    }
}
