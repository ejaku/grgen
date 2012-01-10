/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    #region GraphProcessingDelegates

    /// <summary>
    /// Represents a method called directly after a sequence has been entered.
    /// </summary>
    /// <param name="seq">The current sequence object.</param>
    public delegate void EnterSequenceHandler(Sequence seq);

    /// <summary>
    /// Represents a method called before a sequence is left.
    /// </summary>
    /// <param name="seq">The current sequence object.</param>
    public delegate void ExitSequenceHandler(Sequence seq);

    #endregion GraphProcessingDelegates


    /// <summary>
    /// An environment for the advanced processing of graphs / execution of sequences.
    /// </summary>
    public interface IGraphProcessingEnvironment : IActionExecutionEnvironment
    {
        /// <summary>
        /// Returns the transaction manager of the graph.
        /// For attribute changes using the transaction manager is the only way to include such changes in the transaction history!
        /// Don't forget to call Commit after a transaction is finished!
        /// </summary>
        ITransactionManager TransactionManager { get; }

        /// <summary>
        /// The recorder of the graph.
        /// Might be null (is set if a named graph is available, then the persistent names are taken from the named graph).
        /// </summary>
        IRecorder Recorder { get; set; }

        /// <summary>
        /// The writer used by emit statements. By default this is Console.Out.
        /// </summary>
        TextWriter EmitWriter { get; set; }

        /// <summary>
        /// Duplicates the graph variables of an old just cloned graph, assigns them to the new cloned graph.
        /// </summary>
        /// <param name="old">The old graph.</param>
        /// <param name="clone">The new, cloned version of the graph.</param>
        void CloneGraphVariables(IGraph old, IGraph clone);


        /// <summary>
        /// Apply a rewrite rule (first computing the parameters with sequence expressions, last assigning result variables).
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule invocation</param>
        /// <param name="which">The index of the match to be rewritten or -1 to rewrite all matches</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on MaxMatches)</param>
        /// <param name="special">Specifies whether the %-modifier has been used for this rule, which may have a special meaning for
        /// the application</param>
        /// <param name="test">If true, no rewrite step is performed.</param>
        /// <returns>The number of matches found</returns>
        int ApplyRewrite(RuleInvocationParameterBindings paramBindings, int which, int localMaxMatches, bool special, bool test);


        #region Variables management

        /// <summary>
        /// Returns a linked list of variables mapping to the given graph element
        /// or null, if no variable points to this element
        /// </summary>
        LinkedList<Variable> GetElementVariables(IGraphElement elem);

        /// <summary>
        /// Retrieves the object for a variable name or null, if the variable isn't set yet or anymore
        /// </summary>
        /// <param name="varName">The variable name to lookup</param>
        /// <returns>The according object or null</returns>
        object GetVariableValue(string varName);

        /// <summary>
        /// Retrieves the INode for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an INode object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        INode GetNodeVarValue(string varName);

        /// <summary>
        /// Retrieves the IEdge for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an IEdge object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        IEdge GetEdgeVarValue(string varName);

        /// <summary>
        /// Sets the value of the given variable to the given value.
        /// If the variable name is null, this function does nothing.
        /// If elem is null, the variable is unset.
        /// </summary>
        /// <param name="varName">The name of the variable</param>
        /// <param name="val">The new value of the variable</param>
        void SetVariableValue(string varName, object val);

        /// <summary>
        /// Returns an iterator over all available (non-null) variables
        /// </summary>
        IEnumerable<Variable> Variables { get; }

        #endregion Variables management


        #region Variables of graph elements convenience

        /// <summary>
        /// Adds an existing INode object to the current graph of this processing environment and assigns it to the given variable.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        void AddNode(INode node, String varName);

        /// <summary>
        /// Adds a new node to the current graph of this processing environment and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        INode AddNode(NodeType nodeType, String varName);

        /// <summary>
        /// Adds an existing IEdge object to the current graph of this processing environment and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        void AddEdge(IEdge edge, String varName);

        /// <summary>
        /// Adds a new edge to the current graph of this processing environment and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        IEdge AddEdge(EdgeType edgeType, INode source, INode target, string varName);

        #endregion Variables of graph elements convenience


        #region Variables of named graph elements convenience

        /// <summary>
        /// Adds an existing node to the graph, names it, and assigns it to the given variable.
        /// </summary>
        /// <param name="node">The existing node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the new node or null if it is to be auto-generated.</param>
        void AddNode(INode node, String varName, String elemName);

        /// <summary>
        /// Adds a new named node to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the new node or null if it is to be auto-generated.</param>
        /// <returns>The newly created node.</returns>
        INode AddNode(NodeType nodeType, String varName, String elemName);

        /// <summary>
        /// Adds an existing edge to the graph, names it, and assigns it to the given variable.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the edge or null if it is to be auto-generated.</param>
        /// <returns>The newly created edge.</returns>
        void AddEdge(IEdge edge, String varName, String elemName);

        /// <summary>
        /// Adds a new named edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the edge or null if it is to be auto-generated.</param>
        /// <returns>The newly created edge.</returns>
        IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName, String elemName);

        #endregion Variables of named graph elements convenience

        
        #region Sequence handling

        /// <summary>
        /// Apply a graph rewrite sequence (to the currently associated graph).
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <returns>The result of the sequence.</returns>
        bool ApplyGraphRewriteSequence(Sequence sequence);

        /// <summary>
        /// Apply a graph rewrite sequence (to the currently associated graph).
        /// </summary>
        /// <param name="seqStr">The graph rewrite sequence in form of a string</param>
        /// <returns>The result of the sequence.</returns>
        bool ApplyGraphRewriteSequence(String seqStr);

        /// <summary>
        /// Apply a graph rewrite sequence (to the currently associated graph).
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>The result of the sequence.</returns>
        bool ApplyGraphRewriteSequence(Sequence sequence, SequenceExecutionEnvironment env);

        /// <summary>
        /// Apply a graph rewrite sequence (to the currently associated graph).
        /// </summary>
        /// <param name="seqStr">The graph rewrite sequence in form of a string</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>The result of the sequence.</returns>
        bool ApplyGraphRewriteSequence(String seqStr, SequenceExecutionEnvironment env);

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        bool ValidateWithSequence(Sequence seq);

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seqStr">The sequence to be executed in form of a string</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        bool ValidateWithSequence(String seqStr);

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        bool ValidateWithSequence(Sequence seq, SequenceExecutionEnvironment env);

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seqStr">The sequence to be executed in form of a string</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        bool ValidateWithSequence(String seqStr, SequenceExecutionEnvironment env);

        /// <summary>
        /// Parses the given XGRS string and generates a Sequence object.
        /// Any actions in the string must refer to actions from the actions contained in this object.
        /// </summary>
        /// <param name="seqStr">The sequence to be parsed in form of an XGRS string.</param>
        /// <returns>The sequence object according to the given string.</returns>
        Sequence ParseSequence(String seqStr);

        #endregion Sequence handling
        
        
        #region Events

        /// <summary>
        /// Fired when a sequence is entered.
        /// </summary>
        event EnterSequenceHandler OnEntereringSequence;

        /// <summary>
        /// Fired when a sequence is left.
        /// </summary>
        event ExitSequenceHandler OnExitingSequence;


        /// <summary>
        /// Fires an OnEnteringSequence event.
        /// </summary>
        /// <param name="seq">The sequence to be entered.</param>
        void EnteringSequence(Sequence seq);

        /// <summary>
        /// Fires an OnExitingSequence event.
        /// </summary>
        /// <param name="seq">The sequence to be exited.</param>
        void ExitingSequence(Sequence seq);

        #endregion Events
    }
}
