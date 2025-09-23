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
    /// An interface that provides interfaces to access the persistence related functionality (of a persistent graph, including processing support, ulimately implemented by a persistence provider).
    /// </summary>
    public interface IPersistenceInterfacesProvider
    {
        /// <summary>
        /// Returns an interface that allows to fetch some persistence provider statistics (related to the host persistent graph but also reachable entities).
        /// </summary>
        IPersistenceProviderStatistics PersistenceProviderStatistics { get; }

        /// <summary>
        /// Returns an interface that allows to control the transactions of the persistence provider, or null if the persistence provider does not support transactions.
        /// </summary>
        IPersistenceProviderTransactionManager PersistenceProviderTransactionManager { get; }
    }
}
