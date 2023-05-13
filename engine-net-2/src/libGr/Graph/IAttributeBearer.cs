/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    // the types derived from the attribute bearer implement abstract concepts ICopy, IClone, and IUniqueId:
    // abstract methods of the abstract concept
    // Copy(IGraph graph, IDictionary<object, object> oldToNewObjectMap); // returns a deep copy of the object, some method instances require more parameters, the exact return types differ
    // Clone(); // returns a shallow clone of the object, some method instances require parameters, the exact return types differ 
    // GetUniqueId(); // returns the unique id, the exact return types differ

    /// <summary>
    /// An interface to IBaseObject (thus IObject, ITransientObject) and IGraphElement (thus INode and IEdge) types, all bearing attributes
    /// </summary>
    public interface IAttributeBearer : ITyped, IDeepEqualityComparer, ICallable
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
