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
    /// Interface used to display debugger output (sequences via the sequence displayer part, but also regular output, described by semantic content as fas as possible (targeted)).
    /// It is implemented by a Printer printing to the textual console and a Renderer rendering as a graph.
    /// Display of main content is screen / frame based, begin and implicit end at next begin
    /// - marks frame borders on single console, 
    /// - determines screen content lifetime in case of main console,
    /// - and determines window content lifetime in case of gui Debugger (this works because the MSAGL graph renderer is deterministic - at least it seems to be so as of now).
    /// </summary>
    public interface IDisplayer : ISequenceDisplayer
    {
        /// <summary>
        /// Begins main content display.
        /// </summary>
        /// <param name="header">A header line printed to the console serving as a frame.</param>
        void BeginOfDisplay(string header);

        //void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix); inherited from ISequenceDisplayer
        //void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix); inherited from ISequenceDisplayer
        //void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix); inherited from ISequenceDisplayer
        //string DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName); inherited from ISequenceDisplayer
        //string DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName); inherited from ISequenceDisplayer
        //string DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName); inherited from ISequenceDisplayer

        // displays the interpreted sequences call stack, which may be a shorted version to only the currently executed (i.e. topmost) sequence
        // also displays the subrule computations entry stack if subruleStack is not null, with entries in long form in case of fullSubruleTracesEntries
        // maybe TODO: input of the current debugger DisplaySequenceContext for the top of stack, somewhen, if really needed (instead of only highlighting of the currently executed sequence)
        void DisplayCallStacks(SequenceBase[] callStack, SubruleComputation[] subruleStack, bool fullSubruleTracesEntries);

        // displays the local variables (found from seqStart to seq), and also displays the global variables and visited flags if debuggerProcEnv is not null
        // maybe TODO: usage of SequenceVariables directly instead of sequence would fit better to the abstraction
        void DisplayVariables(SequenceBase seqStart, SequenceBase seq, DebuggerGraphProcessingEnvironment debuggerProcEnv);

        // displays the full state of the interpreted sequences call stack (for every frame: the sequence including all local variables)
        // plus the global variables and visited flags, plus the entries from the subrule computations entry stack (in long form)
        // maybe TODO: input of the current debugger DisplaySequenceContext for the top of stack, somewhen, if really needed (instead of only highlighting of the currently executed sequence)
        void DisplayFullState (SequenceBase[] callStack, SubruleComputation[] subruleStack, DebuggerGraphProcessingEnvironment debuggerProcEnv);

        /// <summary>
        /// Displays a text line.
        /// Semantically poor, but ok for the beginning/maybe later on special tasks, but should be replaced/implemented by semantically richer objects.
        /// </summary>
        void DisplayLine(string lineToBeShown);
    }
}
