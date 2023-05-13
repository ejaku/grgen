/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An interface for undo items of the transaction manager.
    /// Allows to register own ones for external types, will be called on rollback.
    /// </summary>
    public interface IUndoItem
    {
        /// <summary>
        /// Called on rollback by the transaction manager,
        /// in order to undo the effects of some change it was created for.
        /// </summary>
        /// <param name="procEnv">The current graph processing environment</param>
        void DoUndo(IGraphProcessingEnvironment procEnv);

        // Note that ToString() is called by the dump command of the debugger, 
        // writing the output of ToString to the transaction log file (allows to inspect the current state of the undo log).
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
        int Start();

        /// <summary>
        /// Pauses the running transactions,
        /// i.e. changes done from now on until resume won't be undone in case of a rollback
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the running transactions after a pause,
        /// i.e. changes done from now on will be undone again in case of a rollback
        /// </summary>
        void Resume();

        /// <summary>
        /// Removes the rollback data and stops this transaction
        /// </summary>
        /// <param name="transactionID">Transaction ID returned by a Start call</param>
        void Commit(int transactionID);

        /// <summary>
        /// Undoes all changes during a transaction
        /// </summary>
        /// <param name="transactionID">The ID of the transaction to be rollbacked</param>
        void Rollback(int transactionID);

        /// <summary>
        /// Indicates, whether a transaction is currently active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Registers an undo item to be called on rollback,
        /// for reverting the changes applied to some graph element attribute of external type.
        /// Only in case a transaction is underway and not paused (and not rolling back) is the undo item added to the undo log,
        /// so you may call this method simply on each change to an external type.
        /// </summary>
        /// <param name="item">An object that is capable of undoing the effects of some change it was created for in case of rollback</param>
        void ExternalTypeChanged(IUndoItem item);
    }
}
