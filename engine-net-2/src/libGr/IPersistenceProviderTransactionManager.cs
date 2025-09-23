/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An interface for managing the transactions of the underlying database of a persistent graph.
    /// Unrelated to the ITransactionManager, which is for application layer/algorithmical transactions/change rollback;
    /// this one is more about performance due to batchwise processing defined by transaction borders, and it defines the granularity of durability.
    /// When no transaction is used, i.e. started, each and every change to the persistent graph will be durable immediately after its corresponding change event returned (i.e. will be auto-wrapped in a mini-transaction -- this will cause very poor performance).
    /// Nested transactions are not supported, in contrast to the application layer transaction manager.
    /// </summary>
    public interface IPersistenceProviderTransactionManager
    {
        /// <summary>
        /// Starts a database layer transaction, not allowed when one is already active (potential future todo: mapping of nested transactions to a single one).
        /// </summary>
        void Start();

        /// <summary>
        /// Intermediate commit, afterwards the changes up to that point are persistently stored for sure (all writes to the database up till that point are only temporary/pending), and a/the transaction is active.
        /// </summary>
        void CommitAndRestart();

        /// <summary>
        /// Commits the transaction, afterwards the changes up to that point are persistently stored for sure (all writes to the database up till that point are only temporary/pending), and a/the transaction is not active anymore.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back changes since that last CommitAndRestart() or the last Start() if no restart occurred, afterwards the transaction is not active anymore. This also happens with pending changes when no commit occurrs.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Tells whether a/the transaction is active.
        /// </summary>
        bool IsActive { get; }
    }
}
