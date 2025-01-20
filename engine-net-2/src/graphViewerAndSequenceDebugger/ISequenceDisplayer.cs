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
    /// Interface used to display a sequence or sequence expression.
    /// It is implemented by a Printer printing to the textual console and a Renderer rendering as a graph.
    /// </summary>
    public interface ISequenceDisplayer
    {
        /// <summary>
        /// Displays the given root sequence base according to the display context.
        /// Switches in between displaying a sequence and a sequence expression.
        /// </summary>
        /// <param name="seqBase">The sequence base to be displayed</param>
        /// <param name="context">The display context</param>
        /// <param name="nestingLevel">The level the sequence is nested in, typically displayed as prefix</param>
        /// <param name="prefix">A prefix to be displayed before the sequence</param>
        /// <param name="postfix">A postfix to be displayed after the sequence</param>
        void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix);

        /// <summary>
        /// Displays the given root sequence (adding parentheses if needed) according to the display context.
        /// </summary>
        /// <param name="seq">The sequence to be displayed</param>
        /// <param name="context">The display context</param>
        /// <param name="nestingLevel">The level the sequence is nested in, typically displayed as prefix</param>
        /// <param name="prefix">A prefix to be displayed before the sequence</param>
        /// <param name="postfix">A postfix to be displayed after the sequence</param>
        void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix);

        /// <summary>
        /// Displays the given root sequence expression according to the display context.
        /// </summary>
        /// <param name="seqExpr">The sequence expression to be displayed</param>
        /// <param name="context">The display context</param>
        /// <param name="nestingLevel">The level the sequence is nested in, typically displayed as prefix</param>
        /// <param name="prefix">A prefix to be displayed before the sequence</param>
        /// <param name="postfix">A postfix to be displayed after the sequence</param>
        void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix);


        /// <summary>
        /// Displays the given root sequence base according to the display context.
        /// Switches in between displaying a sequence and a sequence expression.
        /// </summary>
        /// <param name="seqBase">The sequence base to be displayed</param>
        /// <param name="context">The display context</param>
        /// <param name="nestingLevel">The level the sequence is nested in, typically displayed as prefix</param>
        /// <param name="prefix">A prefix to be displayed before the sequence</param>
        /// <param name="postfix">A postfix to be displayed after the sequence</param>
        /// <param name="groupNodeName">If not null, the sequence gets nested into the corresponding group node in graph rendering</param>
        /// <returns>The sequence renderer returns the name of the node added (for wiring into a more encompassing graph), the sequence printer returns null</returns>
        string DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName);

        /// <summary>
        /// Displays the given root sequence (adding parentheses if needed) according to the display context.
        /// </summary>
        /// <param name="seq">The sequence to be displayed</param>
        /// <param name="context">The display context</param>
        /// <param name="nestingLevel">The level the sequence is nested in, typically displayed as prefix</param>
        /// <param name="prefix">A prefix to be displayed before the sequence</param>
        /// <param name="postfix">A postfix to be displayed after the sequence</param>
        /// <param name="groupNodeName">If not null, the sequence gets nested into the corresponding group node in graph rendering</param>
        /// <returns>The sequence renderer returns the name of the node added (for wiring into a more encompassing graph), the sequence printer returns null</returns>
        string DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName);

        /// <summary>
        /// Displays the given root sequence expression according to the display context.
        /// </summary>
        /// <param name="seqExpr">The sequence expression to be displayed</param>
        /// <param name="context">The display context</param>
        /// <param name="nestingLevel">The level the sequence is nested in, typically displayed as prefix</param>
        /// <param name="prefix">A prefix to be displayed before the sequence</param>
        /// <param name="postfix">A postfix to be displayed after the sequence</param>
        /// <param name="groupNodeName">If not null, the sequence gets nested into the corresponding group node in graph rendering</param>
        /// <returns>The sequence renderer returns the name of the node added (for wiring into a more encompassing graph), the sequence printer returns null</returns>
        string DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName);


        // potential TODO: introduce versions without nestingLevel, prefix, postfix
    }
}
