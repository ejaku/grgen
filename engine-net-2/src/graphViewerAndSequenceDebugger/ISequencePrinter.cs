/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public interface ISequencePrinter
    {
        /// <summary>
        /// Prints the given root sequence base according to the print context.
        /// Switches in between printing a sequence and a sequence expression.
        /// </summary>
        /// <param name="seq">The sequence base to be printed</param>
        /// <param name="context">The print context</param>
        /// <param name="nestingLevel">The level the sequence is nested in</param>
        void PrintSequenceBase(SequenceBase seqBase, PrintSequenceContext context, int nestingLevel);

        /// <summary>
        /// Prints the given root sequence (adding parentheses if needed) according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="context">The print context</param>
        /// <param name="nestingLevel">The level the sequence is nested in</param>
        void PrintSequence(Sequence seq, PrintSequenceContext context, int nestingLevel);

        /// <summary>
        /// Prints the given root sequence expression according to the print context.
        /// </summary>
        /// <param name="seqExpr">The sequence expression to be printed</param>
        /// <param name="context">The print context</param>
        void PrintSequenceExpression(SequenceExpression seqExpr, PrintSequenceContext context, int nestingLevel);
    }
}
