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
    /// An interface to be implemented by classes that allow to persist changes to a named graph to some kind of repository.
    /// </summary>
    public interface IPersistenceProvider
    {
        /// <summary>
        /// opens the repository / connects to the repository
        /// the persistence provider can receive parameters with the connectionParameters string
        /// errors are reported by exception
        /// </summary>
        void Open(string connectionParameters);

        /// <summary>
        /// fills the given named graph from the content stored in the repository, or creates the repository if it does not exist yet
        /// the graph must be empty (but its model must be known)
        /// the persistence provider registers as listener at the graph (just filled by reading from the repository)
        /// it listens to change events and persists the ongoing modification of the graph to the repository
        /// errors are reported by exception
        /// </summary>
        void ReadPersistentGraphAndRegisterToListenToGraphModifications(INamedGraph namedGraph); // TODO: also procEnv as parameter, or maybe a RegisterToListenToProcessingEnvironmentEvents

        /// <summary>
        /// closes the repository / disconnects from the respository
        /// </summary>
        void Close();
    }
}
