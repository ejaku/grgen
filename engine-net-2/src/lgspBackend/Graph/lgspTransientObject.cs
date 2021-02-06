/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class implementing transient objects in the libGr search plan backend (values of internal non-node/edge classes)
    /// </summary>
    [DebuggerDisplay("LGSPTransientObject ({Type})")]
    public abstract class LGSPTransientObject : ITransientObject
    {
        /// <summary>
        /// The transient object type (class) of the object/value.
        /// </summary>
        public readonly TransientObjectType lgspType;

        /// <summary>
        /// Instantiates an LGSPTransientObject object.
        /// </summary>
        /// <param name="transientObjectType">The transient object type.</param>
        protected LGSPTransientObject(TransientObjectType transientObjectType)
        {
            lgspType = transientObjectType;
        }

        /// <summary>
        /// The TransientObjectType of the object.
        /// </summary>
        public TransientObjectType Type
        {
            [DebuggerStepThrough]
            get { return lgspType; }
        }

        /// <summary>
        /// The BaseObjectType of the base object.
        /// </summary>
        BaseObjectType IBaseObject.Type
        {
            [DebuggerStepThrough]
            get { return lgspType; }
        }

        /// <summary>
        /// The InheritanceType of the attribute bearer.
        /// </summary>
        InheritanceType IAttributeBearer.Type
        {
            [DebuggerStepThrough]
            get { return lgspType; }
        }

        /// <summary>
        /// Returns true, if the transient class object is compatible to the given type.
        /// </summary>
        public bool InstanceOf(GrGenType otherType)
        {
            return lgspType.IsA(otherType);
        }
        
        /// <summary>
        /// Indexer that gives access to the attributes of the transient class object.
        /// </summary>
        public object this[string attrName]
        {
            get { return GetAttribute(attrName); }
            set { SetAttribute(attrName, value); }
        }

        /// <summary>
        /// Returns the attribute with the given attribute name.
        /// If the transient class type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        public abstract object GetAttribute(string attrName);

        /// <summary>
        /// Sets the attribute with the given attribute name to the given value.
        /// If the transient class type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        public abstract void SetAttribute(string attrName, object value);

        /// <summary>
        /// Resets all transient class object attributes to their initial values.
        /// </summary>
        public abstract void ResetAllAttributes();

        /// <summary>
        /// Creates a shallow clone of this transient object.
        /// All attributes will be transferred to the new object.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        public abstract ITransientObject Clone();

        /// <summary>
        /// Creates a deep copy of this transient object.
        /// All attributes will be transferred to the new object.
        /// </summary>
        /// <param name="graph">The graph to fetch the names of the new objects from.</param>
        /// <param name="oldToNewObjectMap">A dictionary mapping objects to their copies, to be supplied as empty dictionary.</param>
        /// <returns>A copy of this object.</returns>
        public abstract ITransientObject Copy(IGraph graph, IDictionary<IBaseObject, IBaseObject> oldToNewObjectMap);

        /// <summary>
        /// Returns whether the attributes of this transient object and that transient object are equal.
        /// If types are unequal the result is false, otherwise the conjunction of equality comparison of the attributes.
        /// </summary>
        public abstract bool AreAttributesEqual(IAttributeBearer that);

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
