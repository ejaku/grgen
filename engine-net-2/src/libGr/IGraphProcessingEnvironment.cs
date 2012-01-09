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
    /// Represents a method called after all requested matches of an action have been matched.
    /// </summary>
    /// <param name="matches">The matches found.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void AfterMatchHandler(IMatches matches, bool special);

    /// <summary>
    /// Represents a method called before the rewrite step of an action, when at least one match has been found.
    /// </summary>
    /// <param name="matches">The matches found.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void BeforeFinishHandler(IMatches matches, bool special);

    /// <summary>
    /// Represents a method called during rewriting a set of matches before the next match is rewritten.
    /// It is not fired before rewriting the first match.
    /// </summary>
    public delegate void RewriteNextMatchHandler();

    /// <summary>
    /// Represents a method called after the rewrite step of a rule.
    /// </summary>
    /// <param name="matches">The matches found.
    /// This may contain invalid entries, because parts of the matches may have been deleted.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void AfterFinishHandler(IMatches matches, bool special);

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
    /// An environment for the processing of graphs.
    /// Holds a refernce to the current graph.
    /// </summary>
    public interface IGraphProcessingEnvironment
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
        /// If PerformanceInfo is non-null, this object is used to accumulate information about time, found matches and applied rewrites.
        /// By default it should be null.
        /// The user is responsible for resetting the PerformanceInfo object.
        /// </summary>
        PerformanceInfo PerformanceInfo { get; set; }

        /// <summary>
        /// The writer used by emit statements. By default this is Console.Out.
        /// </summary>
        TextWriter EmitWriter { get; set; }

        /// <summary>
        /// The maximum number of matches to be returned for a RuleAll sequence element.
        /// If it is zero or less, the number of matches is unlimited.
        /// </summary>
        int MaxMatches { get; set; }

        /// <summary>
        /// Returns the graph currently focused in processing / sequence execution.
        /// </summary>
        IGraph Graph { get; set; }

        /// <summary>
        /// The actions employed by this graph processing environment
        /// </summary>
        BaseActions Actions { get; set; }

        /// <summary>
        /// Duplicates the graph variables of an old just cloned graph, assigns them to the new cloned graph.
        /// </summary>
        /// <param name="old">The old graph.</param>
        /// <param name="clone">The new, cloned version of the graph.</param>
        void CloneGraphVariables(IGraph old, IGraph clone);

        /// <summary>
        /// Does graph processing environment dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        void Custom(params object[] args);

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


        #region Graph rewriting

        /// <summary>
        /// Retrieves the newest version of an IAction object currently available for this graph.
        /// This may be the given object.
        /// </summary>
        /// <param name="action">The IAction object.</param>
        /// <returns>The newest version of the given action.</returns>
        IAction GetNewestActionVersion(IAction action);

        /// <summary>
        /// Sets the newest action version for a static action.
        /// </summary>
        /// <param name="staticAction">The original action generated by GrGen.exe.</param>
        /// <param name="newAction">A new action instance.</param>
        void SetNewestActionVersion(IAction staticAction, IAction newAction);

        /// <summary>
        /// Executes the modifications of the according rule to the given match/matches.
        /// Fires OnRewritingNextMatch events before each rewrite except for the first one.
        /// </summary>
        /// <param name="matches">The matches object returned by a previous matcher call.</param>
        /// <param name="which">The index of the match in the matches object to be applied,
        /// or -1, if all matches are to be applied.</param>
        /// <returns>A possibly empty array of objects returned by the last applied rewrite.</returns>
        object[] Replace(IMatches matches, int which);

        /// <summary>
        /// Apply a rewrite rule.
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

        #endregion Graph rewriting

        
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
        /// Fired after all requested matches of a rule have been matched.
        /// </summary>
        event AfterMatchHandler OnMatched;

        /// <summary>
        /// Fired before the rewrite step of a rule, when at least one match has been found.
        /// </summary>
        event BeforeFinishHandler OnFinishing;

        /// <summary>
        /// Fired before the next match is rewritten. It is not fired before rewriting the first match.
        /// </summary>
        event RewriteNextMatchHandler OnRewritingNextMatch;

        /// <summary>
        /// Fired after the rewrite step of a rule.
        /// Note, that the given matches object may contain invalid entries,
        /// as parts of the match may have been deleted!
        /// </summary>
        event AfterFinishHandler OnFinished;

        /// <summary>
        /// Fired when a sequence is entered.
        /// </summary>
        event EnterSequenceHandler OnEntereringSequence;

        /// <summary>
        /// Fired when a sequence is left.
        /// </summary>
        event ExitSequenceHandler OnExitingSequence;


        /// <summary>
        /// Fires an OnMatched event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Matched(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnFinishing event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Finishing(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnRewritingNextMatch event.
        /// </summary>
        void RewritingNextMatch();

        /// <summary>
        /// Fires an OnFinished event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher. The elements may be invalid.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Finished(IMatches matches, bool special);

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
