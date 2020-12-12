/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A GrGen object (value of internal, non-node/edge class)
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// The ObjectType (class) of the object
        /// </summary>
        ObjectType Type { get; }

        /// <summary>
        /// Returns true, if the object is compatible to the given type
        /// </summary>
        bool InstanceOf(GrGenType type);

        /// <summary>
        /// Indexer that gives access to the attributes of the object.
        /// </summary>
        object this[string attrName] { get; set; }

        /// <summary>
        /// Returns the attribute with the given attribute name.
        /// If the object/class type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        object GetAttribute(String attrName);

        /// <summary>
        /// Sets the attribute with the given attribute name to the given value.
        /// If the object/class type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        void SetAttribute(String attrName, object value);

        /// <summary>
        /// Resets all object attributes to their initial values.
        /// </summary>
        void ResetAllAttributes();

        /// <summary>
        /// Returns whether the attributes of this object and that object are equal.
        /// If types are unequal the result is false, otherwise the conjunction of equality comparison of the attributes.
        /// </summary>
        bool AreAttributesEqual(IObject that);

        /// <summary>
        /// Executes the function method given by its name.
        /// Throws an exception if the method does not exists or the parameters are of wrong types.
        /// </summary>
        /// <param name="actionEnv">The current action execution environment.</param>
        /// <param name="graph">The current graph.</param>
        /// <param name="name">The name of the function method to apply.</param>
        /// <param name="arguments">An array with the arguments to the method.</param>
        /// <returns>The return value of function application.</returns>
        object ApplyFunctionMethod(IActionExecutionEnvironment actionEnv, IGraph graph, string name, object[] arguments);

        /// <summary>
        /// Executes the procedure method given by its name.
        /// Throws an exception if the method does not exists or the parameters are of wrong types.
        /// </summary>
        /// <param name="actionEnv">The current action execution environment.</param>
        /// <param name="graph">The current graph.</param>
        /// <param name="name">The name of the procedure method to apply.</param>
        /// <param name="arguments">An array with the arguments to the method.</param>
        /// <returns>An array with the return values of procedure application. Only valid until the next call of this method.</returns>
        object[] ApplyProcedureMethod(IActionExecutionEnvironment actionEnv, IGraph graph, string name, object[] arguments);
    }
}
