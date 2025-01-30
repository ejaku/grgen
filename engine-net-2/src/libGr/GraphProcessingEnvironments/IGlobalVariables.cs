/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The global variables shared by the graph processing environments.
    /// </summary>
    public interface IGlobalVariables
    {
        /// <summary>
        /// Duplicates the graph variables of an old just cloned graph, assigns them to the new cloned graph.
        /// </summary>
        /// <param name="old">The old graph.</param>
        /// <param name="clone">The new, cloned version of the graph.</param>
        void CloneGraphVariables(IGraph old, IGraph clone);

        /// <summary>
        /// Adds the custom commands descriptions of this object to the customCommandsToDescriptions.
        /// </summary>
        /// <param name="customCommandsToDescriptions">The map of custom commands to descriptions that gets filled</param>
        void FillCustomCommandDescriptions(Dictionary<String, String> customCommandsToDescriptions);

        /// <summary>
        /// Does action execution environment dependent stuff.
        /// </summary>
        /// <param name="graph">The current host graph</param>
        /// <param name="args">Any kind of parameters for the stuff to do; first parameter has to be the command</param>
        void Custom(IGraph graph, params object[] args);


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


        #region (Internal) Object id handling

        /// <summary>
        /// Fetches a unique id for an internal class object (non-transient).
        /// May be called concurrently from multiple threads.
        /// </summary>
        /// <returns>The fetched id.</returns>
        long FetchObjectUniqueId();

        /// <summary>
        /// Requests a specific unique id for an internal class object (non-transient).
        /// This allows to adapt the internal id source so this id cannot be fetched anymore (as well as all ids that are lower than this one).
        /// </summary>
        /// <param name="idToObtain">The id to request.</param>
        /// <returns>The requested id if successful.</returns>
        long RequestObjectUniqueId(long idToObtain);

        #endregion (Internal) Object id handling
    }
}
