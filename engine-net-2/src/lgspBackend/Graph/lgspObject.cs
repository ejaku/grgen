/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class implementing objects in the libGr search plan backend (values of internal non-node/edge classes)
    /// </summary>
    [DebuggerDisplay("LGSPObject ({Type})")]
    public abstract class LGSPObject : IObject
    {
        /// <summary>
        /// The object type (class) of the object/value.
        /// </summary>
        public readonly ObjectType lgspType;

        /// <summary>
        /// Contains a unique id (intended use: filled at creation, never changed)
        /// </summary>
        public long uniqueId;


        /// <summary>
        /// Instantiates a LGSPObject object.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="uniqueId">The unique id.</param>
        protected LGSPObject(ObjectType objectType, long uniqueId)
        {
            lgspType = objectType;
            this.uniqueId = uniqueId;
        }

        /// <summary>
        /// The ObjectType of the object.
        /// </summary>
        public ObjectType Type
        {
            [DebuggerStepThrough]
            get { return lgspType; }
        }

        /// <summary>
        /// The BaseObjectType of the (base) object.
        /// </summary>
        BaseObjectType IBaseObject.Type
        {
            [DebuggerStepThrough]
            get { return lgspType; }
        }

        /// <summary>
        /// The InheritanceType of the typed object.
        /// </summary>
        InheritanceType ITyped.Type
        {
            [DebuggerStepThrough]
            get { return lgspType; }
        }

        /// <summary>
        /// Returns true, if the typed object is compatible to the given type.
        /// </summary>
        public bool InstanceOf(GrGenType otherType)
        {
            return lgspType.IsA(otherType);
        }

        /// <summary>
        /// Gets the unique id of the class object.
        /// </summary>
        /// <returns>The unique id of the class object.</returns>
        public long GetUniqueId()
        {
            return uniqueId;
        }

        /// <summary>
        /// Sets the unique id of the class object.
        /// You have to ensure consistency! (only meant for internal use.)
        /// </summary>
        public void SetUniqueId(long uniqueId)
        {
            this.uniqueId = uniqueId;
        }

        /// <summary>
        /// Gets the name of the class object.
        /// </summary>
        /// <returns>The name of the class object.</returns>
        public string GetObjectName()
        {
            return String.Format("%{0,00000000:X}", uniqueId);
        }

        /// <summary>
        /// Indexer that gives access to the attributes of the class object.
        /// </summary>
        public object this[string attrName]
        {
            get { return GetAttribute(attrName); }
            set { SetAttribute(attrName, value); }
        }

        /// <summary>
        /// Returns the attribute with the given attribute name.
        /// If the class type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        public abstract object GetAttribute(string attrName);

        /// <summary>
        /// Sets the attribute with the given attribute name to the given value.
        /// If the class type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        public abstract void SetAttribute(string attrName, object value);

        /// <summary>
        /// Resets all class object attributes to their initial values.
        /// </summary>
        public abstract void ResetAllAttributes();

        /// <summary>
        /// Creates a shallow clone of this object.
        /// All attributes will be transfered to the new object.
        /// A new name will be fetched from the graph.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        public abstract IObject Clone(IGraph graph);

        /// <summary>
        /// Creates a deep copy of this object (i.e. (transient) class objects will be replicated).
        /// All attributes will be transfered to the new object.
        /// A new name will be fetched from the graph.
        /// </summary>
        /// <param name="graph">The graph to fetch the names of the new objects from.</param>
        /// <param name="oldToNewObjectMap">A dictionary mapping objects to their copies, to be supplied as empty dictionary.</param>
        /// <returns>A copy of this object.</returns>
        public abstract IObject Copy(IGraph graph, IDictionary<object, object> oldToNewObjectMap);

        /// <summary>
        /// Returns whether this and that are deeply equal,
        /// which means the scalar attributes are equal, the container attributes are memberwise deeply equal, and object attributes are deeply equal.
        /// (If types are unequal the result is false.)
        /// Visited objects are/have to be stored in the visited objects dictionary in order to detect shortcuts and cycles.
        /// </summary>
        public abstract bool IsDeeplyEqual(IDeepEqualityComparer that, IDictionary<object, object> visitedObjects);

        /// <summary>
        /// Executes the function method given by its name.
        /// Throws an exception if the method does not exists or the parameters are of wrong types.
        /// </summary>
        /// <param name="actionEnv">The current action execution environment.</param>
        /// <param name="graph">The current graph.</param>
        /// <param name="name">The name of the function method to apply.</param>
        /// <param name="arguments">An array with the arguments to the method.</param>
        /// <returns>The return value of function application.</returns>
        public abstract object ApplyFunctionMethod(IActionExecutionEnvironment actionEnv, IGraph graph, string name, object[] arguments);

        /// <summary>
        /// Executes the procedure method given by its name.
        /// Throws an exception if the method does not exists or the parameters are of wrong types.
        /// </summary>
        /// <param name="actionEnv">The current action execution environment.</param>
        /// <param name="graph">The current graph.</param>
        /// <param name="name">The name of the procedure method to apply.</param>
        /// <param name="arguments">An array with the arguments to the method.</param>
        /// <returns>An array with the return values of procedure application. Only valid until the next call of this method.</returns>
        public abstract object[] ApplyProcedureMethod(IActionExecutionEnvironment actionEnv, IGraph graph, string name, object[] arguments);

        /// <summary>
        /// Returns the name of the type of this class.
        /// </summary>
        /// <returns>The name of the type of this class.</returns>
        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
