/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
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
        /// Writes a line starting with "external ", ending with a new line, 
        /// containing the given string in between (which must not contain "\n" or "\r"),
        /// to the currently ongoing recordings.
        /// This is the format expected for fine-grain external attribute changes, 
        /// the string is passed to External of the graph model on replaying, 
        /// which is forwarding it to external user code for parsing and executing the changes.
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        void External(string value);

        /// <summary>
        /// Writes the given string to the currently ongoing recordings
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        void Write(string value);

        /// <summary>
        /// Writes the given string to the currently ongoing recordings followed by a new line
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        void WriteLine(string value);

        /// <summary>
        /// Flushes the writer
        /// </summary>
        void Flush();

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
}
