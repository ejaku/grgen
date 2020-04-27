/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A type model for node or edge elements.
    /// </summary>
    public interface ITypeModel
    {
        /// <summary>
        /// Specifies whether this type model is model for nodes (= true) or for edges (= false).
        /// </summary>
        bool IsNodeModel { get; }

        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        GrGenType RootType { get; }

        /// <summary>
        /// Returns the element type with the given type name or null, if no type with this name exists.
        /// </summary>
        GrGenType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        GrGenType[] Types { get; }

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
    /// A type model for nodes.
    /// </summary>
    public interface INodeModel : ITypeModel
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
    /// A type model for edges.
    /// </summary>
    public interface IEdgeModel : ITypeModel
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
}
