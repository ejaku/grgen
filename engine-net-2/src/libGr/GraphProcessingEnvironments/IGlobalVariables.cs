/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

        /// <summary>
        /// Fetches a unique id for an internal class object (non-transient).
        /// May be called concurrently from multiple threads.
        /// </summary>
        long FetchObjectUniqueId();

        long FetchObjectUniqueId(long idToObtain);
    }
}
