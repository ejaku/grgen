/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the INamedGraph interface extending the default implementation by persistence support.
    /// </summary>
    public class LGSPPersistentNamedGraph : LGSPNamedGraph, IPersistentGraphStatistics
    {
        IPersistenceProvider persistenceProvider;

        /// <summary>
        /// Constructs an LGSPPersistentNamedGraph object with the given model and capacity, and an automatically generated name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="globalVars">The global variables.</param>
        /// <param name="capacity">The initial capacity for the name maps (performance optimization, use 0 if unsure).</param>
        /// <param name="persistenceProviderDllName">The name of the persistence provider dll.</param>
        /// <param name="connectionParameters">The connection parameters to configure the persistence provider.</param>
        public LGSPPersistentNamedGraph(IGraphModel grmodel, IGlobalVariables globalVars, int capacity,
            String persistenceProviderDllName, String connectionParameters)
            : base(grmodel, globalVars, capacity)
        {
            persistenceProvider = GetPersistenceProvider(persistenceProviderDllName);
            persistenceProvider.Open(connectionParameters);
            persistenceProvider.ReadPersistentGraphAndRegisterToListenToGraphModifications(this);
            // TODO: Close
        }

        /// <summary>
        /// Constructs an LGSPPersistentNamedGraph object with the given model, name, and capacity.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="globalVars">The global variables.</param>
        /// <param name="grname">The name for the graph.</param>
        /// <param name="capacity">The initial capacity for the name maps (performance optimization, use 0 if unsure).</param>
        /// <param name="persistenceProviderDllName">The name of the persistence provider dll.</param>
        /// <param name="connectionParameters">The connection parameters to configure the persistence provider.</param>
        public LGSPPersistentNamedGraph(IGraphModel grmodel, IGlobalVariables globalVars, String grname, int capacity,
            String persistenceProviderDllName, String connectionParameters)
            : base(grmodel, globalVars, grname, capacity)
        {
            persistenceProvider = GetPersistenceProvider(persistenceProviderDllName);
            persistenceProvider.Open(connectionParameters);
            persistenceProvider.ReadPersistentGraphAndRegisterToListenToGraphModifications(this);
        }

        /// <summary>
        /// returns a persistence provider from the given dll
        /// </summary>
        private static IPersistenceProvider GetPersistenceProvider(string persistenceProviderDll)
        {
            Type persistenceProviderType = de.unika.ipd.grGen.libConsoleAndOS.TypeCreator.GetSingleImplementationOfInterfaceFromAssembly(persistenceProviderDll, "IPersistenceProvider");
            IPersistenceProvider persistenceProvider = (IPersistenceProvider)Activator.CreateInstance(persistenceProviderType);
            return persistenceProvider;
        }

        public override void RegisterProcessingEnvironment(IGraphProcessingEnvironment procEnv)
        {
            persistenceProvider.RegisterToListenToProcessingEnvironmentEvents(procEnv);
        }

        public int NumNodesInDatabase
        {
            get { return persistenceProvider.NumNodesInDatabase; }
        }

        public int NumEdgesInDatabase
        {
            get { return persistenceProvider.NumEdgesInDatabase; }
        }

        public int NumObjectsInDatabase
        {
            get { return persistenceProvider.NumObjectsInDatabase; }
        }

        public int NumGraphsInDatabase
        {
            get { return persistenceProvider.NumGraphsInDatabase; }
        }

        #region Copy Constructors

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="dataSource">The LGSPPersistentNamedGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning.</param>
        public LGSPPersistentNamedGraph(LGSPPersistentNamedGraph dataSource, String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
            : base(dataSource, newName, out oldToNewMap)
        {
            // TODO new persistence provider, register to
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="dataSource">The LGSPPersistentNamedGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        public LGSPPersistentNamedGraph(LGSPPersistentNamedGraph dataSource, String newName)
            : base(dataSource, newName)
        {
            // TODO new persistence provider, register to
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with anonymous names in the form "$" + GetNextName()
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning</param>
        public LGSPPersistentNamedGraph(LGSPGraph graph, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
            : base(graph, out oldToNewMap)
        {
            // TODO new persistence provider, register to
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with anonymous names in the form "$" + GetNextName()
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        public LGSPPersistentNamedGraph(LGSPGraph graph)
            : base(graph)
        {
            // TODO new persistence provider, register to
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with the names provided in a given attribute each graph element must have
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        /// <param name="nameAttributeName">The name of the attribute to be used for naming</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning</param>
        public LGSPPersistentNamedGraph(LGSPGraph graph, String nameAttributeName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
            : base(graph, nameAttributeName, out oldToNewMap)
        {
            // TODO new persistence provider, register to
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with the names provided in a given attribute each graph element must have
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        /// <param name="nameAttributeName">The name of the attribute to be used for naming</param>
        public LGSPPersistentNamedGraph(LGSPGraph graph, String nameAttributeName)
            : base(graph, nameAttributeName)
        {
            // TODO new persistence provider, register to
        }

        #endregion Copy Constructors

        public override IGraph Clone(String newName)
        {
            return CloneNamed(newName); // TODO
        }

        public override IGraph Clone(String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            return CloneNamed(newName, out oldToNewMap); // TODO
        }

        public override IGraph CreateEmptyEquivalent(String newName)
        {
            return new LGSPNamedGraph(this.model, this.GlobalVariables, newName, 0); // TODO
        }

        public override string ToString()
        {
            return "LGSPPersistentNamedGraph " + Name + " id " + GraphId + " @ " + ChangesCounter;
        }
    }
}
