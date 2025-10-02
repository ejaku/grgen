/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Returns some basic statistics from the persistence provider (the persistent graph).
    /// Note that the number of entities stored in the database are returned, compared to the number of objects in the current host graph,
    /// and that the numbers may be different after (re-)opening (i.e. reading) from to the numbers current at the last time shortly before closing the database, caused by a garbage collection run.
    /// (There is no dedicated interface existing for a persistent graph (at least as of now).)
    /// </summary>
    public interface IPersistenceProviderStatistics
    {
        /// <summary>
        /// Returns the number of nodes in all graphs known to the database.
        /// </summary>
        int NumNodesInDatabase { get; }

        /// <summary>
        /// Returns the number of edges in all graphs known to the database.
        /// </summary>
        int NumEdgesInDatabase { get; }

        /// <summary>
        /// Returns the number of (internal class) objects known to the database.
        /// </summary>
        int NumObjectsInDatabase { get; }

        /// <summary>
        /// Returns the number of graphs known to the database.
        /// </summary>
        int NumGraphsInDatabase { get; }
    }
}
