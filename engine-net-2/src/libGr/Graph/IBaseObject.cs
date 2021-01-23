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
    /// A GrGen base object (base for values of internal, non-node/edge classes)
    /// </summary>
    public interface IBaseObject : IAttributeBearer
    {
        /// <summary>
        /// The BaseObjectType (class) of the object
        /// </summary>
        new BaseObjectType Type { get; }

        /// <summary>
        /// Creates a copy of this base object.
        /// All attributes will be transferred to the new object.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        IBaseObject Clone();
    }

    /// <summary>
    /// A GrGen object (value of internal, non-node/edge class)
    /// </summary>
    public interface IObject : IBaseObject
    {
        /// <summary>
        /// The ObjectType (class) of the object
        /// </summary>
        new ObjectType Type { get; }

        /// <summary>
        /// Gets the unique id of the class object.
        /// </summary>
        /// <returns>The unique id of the class object.</returns>
        long GetUniqueId();

        /// <summary>
        /// Sets the unique id of the class object.
        /// You have to ensure consistency! (only meant for internal use.)
        /// </summary>
        void SetUniqueId(long uniqueId);

        /// <summary>
        /// Gets the name of the class object (which has the form "%" + uniqueId).
        /// </summary>
        /// <returns>The name of the class object.</returns>
        string GetObjectName();

        /// <summary>
        /// Creates a copy of this object.
        /// All attributes will be transfered to the new object.
        /// A new name will be fetched from the graph.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        IObject Clone(IGraph graph);
    }

    /// <summary>
    /// A GrGen transient object (value of internal, non-node/edge class)
    /// </summary>
    public interface ITransientObject : IBaseObject
    {
        /// <summary>
        /// The TransientObjectType (class) of the object
        /// </summary>
        new TransientObjectType Type { get; }

        /// <summary>
        /// Creates a copy of this transient object.
        /// All attributes will be transferred to the new object.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        new ITransientObject Clone();
    }
}
