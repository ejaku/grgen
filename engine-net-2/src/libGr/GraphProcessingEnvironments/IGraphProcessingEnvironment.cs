/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    #region GraphProcessingDelegates

    /// <summary>
    /// Represents a method called directly after a sequence base has been entered
    /// (sequence base comprises sequences, sequence computations, sequence expressions).
    /// </summary>
    /// <param name="seq">The current sequence object.</param>
    public delegate void EnterSequenceHandler(SequenceBase seq);

    /// <summary>
    /// Represents a method called before a sequence base is left
    /// (sequence base comprises sequences, sequence computations, sequence expressions).
    /// </summary>
    /// <param name="seq">The current sequence object.</param>
    public delegate void ExitSequenceHandler(SequenceBase seq);

    /// <summary>
    /// Represents a method called when a loop iteration is ended.
    /// </summary>
    public delegate void EndOfIterationHandler(bool continueLoop, SequenceBase seq);

    #endregion GraphProcessingDelegates


    /// <summary>
    /// An environment for the advanced processing of graphs and the execution of sequences.
    /// With global variables, (sub)graph switching, and transaction management.
    /// </summary>
    public interface IGraphProcessingEnvironment : ISubactionAndOutputAdditionEnvironment
    {
        /// <summary>
        /// Returns the transaction manager of the processing environment.
        /// (Recording and undoing changes in the main graph and all processed subgraphs).
        /// Don't forget to call Commit after a transaction is finished!
        /// </summary>
        ITransactionManager TransactionManager { get; }

        /// <summary>
        /// Duplicates the graph variables of an old just cloned graph, assigns them to the new cloned graph.
        /// </summary>
        /// <param name="old">The old graph.</param>
        /// <param name="clone">The new, cloned version of the graph.</param>
        void CloneGraphVariables(IGraph old, IGraph clone);


        /// <summary>
        /// Apply a rewrite rule.
        /// </summary>
        /// <param name="action">The rule to invoke</param>
        /// <param name="subgraph">The subgraph to invoke the rule in, if not null</param>
        /// <param name="arguments">The input arguments</param>
        /// <param name="which">The index of the match to be rewritten or -1 to rewrite all matches</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on MaxMatches)</param>
        /// <param name="special">Specifies whether the %-modifier has been used for this rule, which may have a special meaning for
        /// the application</param>
        /// <param name="test">If true, no rewrite step is performed.</param>
        /// <param name="filters">The name of the filters to apply to the matches before rewriting, in the order of filtering.</param>
        /// <param name="numMatches">The amount of matches found (output returned).</param>
        /// <returns>The list of outputs (for each match a list element, a list element is an array with the returned values).</returns>
        List<object[]> ApplyRewrite(IAction action, IGraph subgraph, object[] arguments, int which, int localMaxMatches, bool special, bool test, List<FilterCall> filters, out int numMatches);

        /// <summary>
        /// Filters the matches of a rule (all) call with a lambda expression filter (call).
        /// </summary>
        /// <param name="matches">The matches of the rule</param>
        /// <param name="filter">The lambda expression filter to apply</param>
        void Filter(IMatches matches, FilterCallWithLambdaExpression filter);


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
        /// If val is null, the variable is unset.
        /// </summary>
        /// <param name="varName">The name of the variable</param>
        /// <param name="val">The new value of the variable</param>
        void SetVariableValue(string varName, object val);

        /// <summary>
        /// Returns an iterator over all available (non-null) variables
        /// </summary>
        IEnumerable<Variable> Variables { get; }

        /// <summary>
        /// Indexer for accessing the variables by name, via index notation on this object.
        /// </summary>
        /// <param name="name">The name of the variable to access</param>
        /// <returns>The value of the variable accessed (read on get, written on set)</returns>
        object this[string name] { get; set; }

        #endregion Variables management


        #region Special variables management

        /// <summary>
        /// Retrieves the value of a special variable name.
        /// Special variables are used for GrGen-internal data storage (as of now only "this"-reference handling).
        /// </summary>
        /// <param name="name">The name of the special variable to read</param>
        /// <returns>The according value</returns>
        object GetSpecialVariableValue(string name);

        /// <summary>
        /// Sets the given special variable name to the given value.
        /// Special variables are used for GrGen-internal data storage (as of now only "this"-reference handling).
        /// </summary>
        /// <param name="name">The name of the special variable to write</param>
        /// <param name="val">The new value of the special variable</param>
        void SetSpecialVariableValue(string name, object value);

        /// <summary>
        /// Deletes the given special variable name.
        /// Special variables are used for GrGen-internal data storage (as of now only "this"-reference handling).
        /// </summary>
        /// <param name="name">The name of the special variable to delete</param>
        void DeleteSpecialVariable(string name);

        #endregion Special variables management


        #region Transient Object id handling

        /// <summary>
        /// Assigns a unique id to the given transient object (for the same object, always the same id is returned).
        /// Thereafter, the returned id can be used in GetTransientObject to obtain the corresponding object.
        /// </summary>
        long GetUniqueId(ITransientObject transientObject);

        /// <summary>
        /// Returns the transient object belonging to the given id, or null if no transient object is bound to the id.
        /// </summary>
        ITransientObject GetTransientObject(long uniqueId);

        #endregion Transient Object id handling


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
        /// Evaluate a graph rewrite sequence expression (on the currently associated graph).
        /// </summary>
        /// <param name="sequenceExpression">The graph rewrite sequence expression</param>
        /// <returns>The result of the expression</returns>
        object EvaluateGraphRewriteSequenceExpression(SequenceExpression sequenceExpression);

        /// <summary>
        /// Evaluate a graph rewrite sequence expression (on the currently associated graph).
        /// </summary>
        /// <param name="seqExprStr">The graph rewrite sequence expression in form of a string</param>
        /// <returns>The result of the expression</returns>
        object EvaluateGraphRewriteSequenceExpression(String seqExprStr);

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
        /// Parses the given sequence string and generates a Sequence object.
        /// Any actions in the string must refer to actions from the actions contained in this object.
        /// </summary>
        /// <param name="seqStr">The sequence to be parsed in form of a string.</param>
        /// <returns>The sequence object according to the given string.</returns>
        Sequence ParseSequence(String seqStr);

        /// <summary>
        /// Parses the given sequence expression string and generates a SequenceExpression object.
        /// Any actions in the string must refer to actions from the actions contained in this object.
        /// </summary>
        /// <param name="seqExprStr">The sequence expression to be parsed in form of a string.</param>
        /// <returns>The sequence expression object according to the given string.</returns>
        SequenceExpression ParseSequenceExpression(String seqExprStr);


        /// <summary>
        /// The user proxy queried for choices during sequence execution.
        /// By default the compliant user proxy, if debugging the debugger acting on behalf of/controlled by the user.
        /// </summary>
        IUserProxyForSequenceExecution UserProxy { get; set; }

        /// <summary>
        /// Returns a non-interactive user proxy just echoing its inputs.
        /// </summary>
        IUserProxyForSequenceExecution CompliantUserProxy { get; }

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
        /// Fired when a sequence iteration is ended.
        /// </summary>
        event EndOfIterationHandler OnEndOfIteration;


        /// <summary>
        /// Fires an OnEnteringSequence event (sequence includes sequence computations and sequence expressions).
        /// </summary>
        /// <param name="seq">The sequence base to be entered.</param>
        void EnteringSequence(SequenceBase seq);

        /// <summary>
        /// Fires an OnExitingSequence event (sequence includes sequence computations and sequence expressions).
        /// </summary>
        /// <param name="seq">The sequence base to be exited.</param>
        void ExitingSequence(SequenceBase seq);

        /// <summary>
        /// Fires an OnEndOfIteration event. 
        /// This informs the debugger about the end of a loop iteration, so it can display the state at the end of the iteration.
        /// </summary>
        void EndOfIteration(bool continueLoop, SequenceBase seq);

        #endregion Events
    }


    /// <summary>
    /// A proxy simulating an always compliant user for choices during sequence execution,
    /// always returns the suggested choice. Used for sequence execution without debugger.
    /// </summary>
    public class CompliantUserProxyForSequenceExecution : IUserProxyForSequenceExecution
    {
        public CompliantUserProxyForSequenceExecution()
        {
        }

        public int ChooseDirection(int direction, Sequence seq)
        {
            return direction;
        }

        public int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            return seqToExecute;
        }

        public double ChoosePoint(double pointToExecute, SequenceWeightedOne seq)
        {
            return pointToExecute;
        }

        public int ChooseMatch(int totalMatchExecute, SequenceSomeFromSet seq)
        {
            return totalMatchExecute;
        }

        public int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq)
        {
            return matchToApply;
        }

        public int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            return randomNumber;
        }

        public double ChooseRandomNumber(double randomNumber, Sequence seq)
        {
            return randomNumber;
        }

        public object ChooseValue(string type, Sequence seq)
        {
            throw new Exception("Can only query the user for a value if a debugger is available");
        }
    }
}
