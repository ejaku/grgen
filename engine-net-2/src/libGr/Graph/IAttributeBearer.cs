/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An interface to be implemented by classes whose objects are InheritanceType-typed
    /// </summary>
    public interface ITyped
    {
        /// <summary>
        /// The InheritanceType of the typed object
        /// </summary>
        InheritanceType Type { get; }

        /// <summary>
        /// Returns true, if the typed object is compatible to the given type
        /// </summary>
        bool InstanceOf(GrGenType type);
    }

    /// <summary>
    /// An interface to be implemented by classes whose objects can be compared for structural equality
    /// </summary>
    public interface IStructuralEqualityComparer
    {
        /// <summary>
        /// Returns whether this and that are structurally equal,
        /// which means the scalar attributes are equal, the container attributes are memberwise structurally equal, and object attributes are deeply structurally equal.
        /// (If types are unequal the result is false.)
        /// </summary>
        bool IsStructurallyEqual(IStructuralEqualityComparer that, IDictionary<object, object> visitedObjects);
    }

    /// <summary>
    /// An interface to be implemented by classes that offer callable stateless functions / stateful procedures
    /// </summary>
    public interface ICallable
    {
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

    /// <summary>
    /// An interface to IBaseObject (thus IObject, ITransientObject) and IGraphElement (thus INode and IEdge) types, all bearing attributes
    /// </summary>
    public interface IAttributeBearer : ITyped, IStructuralEqualityComparer, ICallable
    {
        /// <summary>
        /// Indexer that gives access to the attributes of the attribute bearer.
        /// </summary>
        object this[string attrName] { get; set; }

        /// <summary>
        /// Returns the attribute with the given attribute name.
        /// If the attribute bearer (type) doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        object GetAttribute(String attrName);

        /// <summary>
        /// Sets the attribute with the given attribute name to the given value.
        /// If the attribute bearer (type) doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        void SetAttribute(String attrName, object value);

        /// <summary>
        /// Resets all attribute bearer attributes to their initial values.
        /// </summary>
        void ResetAllAttributes();
    }
}
