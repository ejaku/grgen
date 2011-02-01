/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A named graph-global variable.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The value pointed to by the variable.
        /// </summary>
        public object Value;

        /// <summary>
        /// Initializes a Variable instance.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value pointed to by the variable.</param>
        public Variable(String name, object value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// The changes which might occur to graph element attributes.
    /// </summary>
    public enum AttributeChangeType
    {
        /// <summary>
        /// Assignment of a value to some attribute.
        /// Value semantics, even if assigned attribute is a set or a map, not a primitive type.
        /// </summary>
        Assign,

        /// <summary>
        /// Inserting a value into some set or a key value pair into some map.
        /// </summary>
        PutElement,

        /// <summary>
        /// Removing a value from some set or a key value pair from some map.
        /// </summary>
        RemoveElement
    }

    /// <summary>
    /// An interface for managing graph transactions.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Starts a transaction
        /// </summary>
        /// <returns>A transaction ID to be used with Commit or Rollback</returns>
        int StartTransaction();

        /// <summary>
        /// Removes the rollback data and stops this transaction
        /// </summary>
        /// <param name="transactionID">Transaction ID returned by a StartTransaction call</param>
        void Commit(int transactionID);

        /// <summary>
        /// Undoes all changes during a transaction
        /// </summary>
        /// <param name="transactionID">The ID of the transaction to be rollbacked</param>
        void Rollback(int transactionID);

        /// <summary>
        /// Indicates, whether a transaction is currently active.
        /// </summary>
        bool TransactionActive { get; }

//        ITransactionManager Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap);
    }

    /// <summary>
    /// An interface for recording changes (and their causes) applied to a graph into a file,
    /// so that they can get replayed.
    /// </summary>
    public interface IRecorder
    {
        /// <summary>
        /// Creates a file which initially gets filled with a .grs export of the graph.
        /// Afterwards the changes applied to the graph are recorded into the file,
        /// in the order they occur.
        /// You can start multiple recordings into differently named files.
        /// </summary>
        /// <param name="filename">The name of the file to record to</param>
        void StartRecording(string filename);

        /// <summary>
        /// Stops recording of the changes applied to the graph to the given file.
        /// </summary>
        /// <param name="filename">The name of the file to stop recording to</param>
        void StopRecording(string filename);

        /// <summary>
        /// Returns whether the graph changes get currently recorded into the given file.
        /// </summary>
        /// <param name="filename">The name of the file whose recording status gets queried</param>
        /// <returns>The recording status of the file queried</returns>
        bool IsRecording(string filename);

        /// <summary>
        /// Writes the given string to the currently ongoing recordings
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        void Write(string value);

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Called by the transaction manager when a transaction is started
        /// </summary>
        /// <param name="transactionID">The id of the transaction</param>
        void TransactionStart(int transactionID);

        /// <summary>
        /// Called by the transaction manager when a transaction is committed
        /// </summary>
        /// <param name="transactionID">The id of the transaction</param>
        void TransactionCommit(int transactionID);
        
        /// <summary>
        /// Called by the transaction manager when a transaction is rolled back
        /// </summary>
        /// <param name="transactionID">The id of the transaction</param>
        /// <param name="start">true when called at rollback start, false when called at rollback end</param>
        void TransactionRollback(int transactionID, bool start);
    }

    /// <summary>
    /// The different graph validation modes
    /// </summary>
    public enum ValidationMode
    {
        OnlyMultiplicitiesOfMatchingTypes, // check the multiplicities of the incoming/outgoing edges which match the types specified
        StrictOnlySpecified, // as first and additionally check that edges with connections assertions specified are covered by at least on connection assertion
        Strict // as first and additionally check that all edges are covered by at least one connection assertion
    }
}
