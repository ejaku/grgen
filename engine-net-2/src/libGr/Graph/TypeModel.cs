/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A type model for nodes or edges or objects or transient objects (of node classes, edge classes, internal classes, internal transient classes).
    /// </summary>
    public interface ITypeModel
    {
        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        InheritanceType RootType { get; }

        /// <summary>
        /// Returns the element type with the given type name or null, if no type with this name exists.
        /// </summary>
        InheritanceType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        InheritanceType[] Types { get; }

        /// <summary>
        /// An array of C# types of model types.
        /// </summary>
        System.Type[] TypeTypes { get; }

        /// <summary>
        /// Enumerates all attribute types of this model.
        /// </summary>
        IEnumerable<AttributeType> AttributeTypes { get; }
    }

    /// <summary>
    /// A type model for node or edge elements.
    /// </summary>
    public interface IGraphElementTypeModel : ITypeModel
    {
        /// <summary>
        /// Specifies whether this type model is model for nodes (= true) or for edges (= false).
        /// </summary>
        bool IsNodeModel { get; }

        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new GraphElementType RootType { get; }

        /// <summary>
        /// Returns the element type with the given type name or null, if no type with this name exists.
        /// </summary>
        new GraphElementType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new GraphElementType[] Types { get; }
    }

    /// <summary>
    /// A type model for nodes, i.e. node classes.
    /// </summary>
    public interface INodeModel : IGraphElementTypeModel
    {
        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new NodeType RootType { get; }

        /// <summary>
        /// Returns the node type with the given type name or null, if no type with this name exists.
        /// </summary>
        new NodeType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new NodeType[] Types { get; }
    }

    /// <summary>
    /// A type model for edges, i.e. edge classes.
    /// </summary>
    public interface IEdgeModel : IGraphElementTypeModel
    {
        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new EdgeType RootType { get; }

        /// <summary>
        /// Returns the edge type with the given type name or null, if no type with this name exists.
        /// </summary>
        new EdgeType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new EdgeType[] Types { get; }
    }

    /// <summary>
    /// A type model for base objects (internal non graph-element values), i.e. classes.
    /// </summary>
    public interface IBaseObjectTypeModel : ITypeModel
    {
        /// <summary>
        /// Specifies whether this type model is model for internal transient objects (= true) or for internal non-transient objects (= false).
        /// </summary>
        bool IsTransientModel { get; }

        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new BaseObjectType RootType { get; }

        /// <summary>
        /// Returns the object type with the given type name or null, if no type with this name exists.
        /// </summary>
        new BaseObjectType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new BaseObjectType[] Types { get; }
    }

    /// <summary>
    /// A type model for objects (internal non graph-element values), i.e. classes.
    /// </summary>
    public interface IObjectModel : IBaseObjectTypeModel
    {
        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new ObjectType RootType { get; }

        /// <summary>
        /// Returns the object type with the given type name or null, if no type with this name exists.
        /// </summary>
        new ObjectType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new ObjectType[] Types { get; }
    }

    /// <summary>
    /// A type model for transient objects (internal non graph-element values), i.e. classes.
    /// </summary>
    public interface ITransientObjectModel : IBaseObjectTypeModel
    {
        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new TransientObjectType RootType { get; }

        /// <summary>
        /// Returns the transient object type with the given type name or null, if no type with this name exists.
        /// </summary>
        new TransientObjectType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new TransientObjectType[] Types { get; }
    }
}
