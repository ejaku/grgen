/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    #region SubactionDebuggingDelegates

    /// <summary>
    /// Represents a method called directly before graph processing switches to a subgraph.
    /// (Graph processing means rule and sequence execution. Not called when the main graph is replaced.)
    /// </summary>
    /// <param name="graph">The new graph switched to.</param>
    public delegate void SwitchToSubgraphHandler(IGraph graph);

    /// <summary>
    /// Represents a method called directly after graph processing returned back (from a previous switch).
    /// (To the main graph, or a subgraph previously switched to. Graph processing means rule and sequence execution.)
    /// </summary>
    /// <param name="graph">The old graph returned from.</param>
    public delegate void ReturnFromSubgraphHandler(IGraph graph);


    /// <summary>
    /// Represents a method called directly after a computation has been entered (for tracing/debugging purpose).
    /// Applying user-defined computation borders, or e.g. auto-generated rule eval or procedure call borders;
    /// but not interpreted sequences, they receive dedicated treatement in the graph processing environment, are debugged directly.
    /// </summary>
    /// <param name="message">The message = name of the computation entered.</param>
    /// <param name="values">Some values specified at entering, typically input parameters of a computation call.</param>
    public delegate void DebugEnterHandler(string message, params object[] values);

    /// <summary>
    /// Represents a method called directly before a computation is left (for tracing/debugging purpose).
    /// Applying user-defined computation borders, or e.g. auto-generated rule eval or procedure call borders;
    /// but not interpreted sequences, they receive dedicated treatement in the graph processing environment, are debugged directly.
    /// </summary>
    /// <param name="message">The message = name of the computation to be left. (Must be the same as message of the corresponding enter, stored on the debug traces stack.)</param>
    /// <param name="values">Some values specified at exiting, typically output parameters of a computation call.</param>
    public delegate void DebugExitHandler(string message, params object[] values);

    /// <summary>
    /// Represents a method called by the user to emit debugging information, not halting execution.
    /// (Stored on the debug traces stack, removed when its directly nesting debug enter is exited.)
    /// </summary>
    /// <param name="message">The message emitted.</param>
    /// <param name="values">Some further values to be emitted with more detailed information.</param>
    public delegate void DebugEmitHandler(string message, params object[] values);

    /// <summary>
    /// Represents a method called by the user to halt execution, emitting some debugging information.
    /// </summary>
    /// <param name="message">The message emitted and attached to the halt.</param>
    /// <param name="values">Some further values to be emitted with more detailed information.</param>
    public delegate void DebugHaltHandler(string message, params object[] values);

    /// <summary>
    /// Represents a method called by the user to highlight some elements in the graph, halting execution.
    /// </summary>
    /// <param name="message">The message attached to the highlight.</param>
    /// <param name="values">The values to highlight (graph elements, containers of graph elements, visited flag ids).</param>
    /// <param name="annotations">The annotation to highlight the elements with in the graph.</param>
    public delegate void DebugHighlightHandler(string message, List<object> values, List<string> annotations);

    #endregion SubactionDebuggingDelegates


    /// <summary>
    /// An environment extending basic action execution with subaction debugging, plus subgraph nesting, 
    /// and output -- for one textual emits, and for the other graph change recording.
    /// </summary>
    public interface ISubactionAndOutputAdditionEnvironment : IActionExecutionEnvironment
    {
        /// <summary>
        /// Switches the graph to the given (sub)graph.
        /// (One level added to the current graph stack.)
        /// </summary>
        /// <param name="newGraph">The new graph to use as current graph</param>
        void SwitchToSubgraph(IGraph newGraph);

        /// <summary>
        /// Returns from the last switch to subgraph.
        /// (One level back on the current graph stack.)
        /// </summary>
        /// <returns>The lastly used (sub)graph, now not used any more</returns>
        IGraph ReturnFromSubgraph();

        /// <summary>
        /// Returns true when graph processings is currently occuring inside a subgraph,
        /// returns false when the main host graph is currently processed
        /// (i.e. only one entry on the current graph stack is existing).
        /// </summary>
        bool IsInSubgraph { get; }


        /// <summary>
        /// The recorder of the main graph.
        /// Might be null (is set if a named graph is available, then the persistent names are taken from the named graph).
        /// </summary>
        IRecorder Recorder { get; set; }

        /// <summary>
        /// The writer used by emit statements. By default this is ConsoleUI.outWriter (normally forwarded to Console.Out).
        /// </summary>
        TextWriter EmitWriter { get; set; }

        /// <summary>
        /// The writer used by emitdebug statements. This is ConsoleUI.outWriter (normally forwarded to Console.Out), and can't be redirected to a file in contrast to the EmitWriter.
        /// </summary>
        TextWriter EmitWriterDebug { get; }

        
        #region Events

        /// <summary>
        /// Fired when graph processing (rule and sequence execution) is switched to a (sub)graph.
        /// (Not fired when the main graph is replaced by another main graph, or initialized.)
        /// </summary>
        event SwitchToSubgraphHandler OnSwitchingToSubgraph;

        /// <summary>
        /// Fired when graph processing is returning back after a switch.
        /// (To the main graph, or a subgraph previously switched to.)
        /// </summary>
        event ReturnFromSubgraphHandler OnReturnedFromSubgraph;


        /// <summary>
        /// Fired when a debug entity is entered.
        /// </summary>
        event DebugEnterHandler OnDebugEnter;

        /// <summary>
        /// Fired when a debug entity is left.
        /// </summary>
        event DebugExitHandler OnDebugExit;

        /// <summary>
        /// Fired when a debug emit is executed.
        /// </summary>
        event DebugEmitHandler OnDebugEmit;

        /// <summary>
        /// Fired when a debug halt is executed.
        /// </summary>
        event DebugHaltHandler OnDebugHalt;

        /// <summary>
        /// Fired when a debug highlight is executed.
        /// </summary>
        event DebugHighlightHandler OnDebugHighlight;


        /// <summary>
        /// Fires an OnDebugEnter event.
        /// </summary>
        void DebugEntering(string message, params object[] values);

        /// <summary>
        /// Fires an OnDebugExit event.
        /// </summary>
        void DebugExiting(string message, params object[] values);

        /// <summary>
        /// Fires an OnDebugEmit event. 
        /// </summary>
        void DebugEmitting(string message, params object[] values);

        /// <summary>
        /// Fires an OnDebugHalt event. 
        /// </summary>
        void DebugHalting(string message, params object[] values);

        /// <summary>
        /// Fires an OnDebugHighlight event. 
        /// </summary>
        void DebugHighlighting(string message, List<object> values, List<string> annotations);

        #endregion Events
    }
}
