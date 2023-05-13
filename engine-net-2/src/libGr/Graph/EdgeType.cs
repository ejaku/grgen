/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the kind of directedness for an EdgeType
    /// </summary>
    public enum Directedness
    {
        /// <summary>Arbitrary directed. Only for abstract edge types.</summary>
        Arbitrary,
        /// <summary>Directed.</summary>
        Directed,
        /// <summary>Undirected.</summary>
        Undirected
    }

    /// <summary>
    /// A representation of a GrGen edge type.
    /// </summary>
    public abstract class EdgeType : GraphElementType
    {
        /// <summary>
        /// Constructs an EdgeType instance with the given type ID.
        /// </summary>
        /// <param name="typeID">The unique type ID.</param>
        protected EdgeType(int typeID)
            : base(typeID)
        {
        }

        /// <summary>
        /// Always returns false.
        /// </summary>
        public override bool IsNodeType
        {
            [DebuggerStepThrough] get { return false; }
        }

        /// <summary>
        /// This EdgeType describes edges whose real .NET interface type is named as returned (fully qualified).
        /// </summary>
        public abstract String EdgeInterfaceName { get; }

        /// <summary>
        /// This EdgeType describes edges whose real .NET class type is named as returned (fully qualified).
        /// It might be null in case this type IsAbstract.
        /// </summary>
        public abstract String EdgeClassName { get; }

        /// <summary>
        /// Specifies the directedness of this edge type.
        /// </summary>
        public abstract Directedness Directedness { get; }

        /// <summary>
        /// Creates an IEdge object according to this type.
        /// </summary>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The created IEdge object.</returns>
        public abstract IEdge CreateEdge(INode source, INode target);

        /// <summary>
        /// Creates an IEdge object according to this type.
        /// ATTENTION: You must call SetSourceAndTarget() before adding an edge created this way to a graph.
        /// This is an unsafe function that allows to first set the attributes of an edge, as needed in efficient .grs importing.
        /// </summary>
        /// <returns>The created IEdge object.</returns>
        public IEdge CreateEdge()
        {
            return CreateEdge(null, null);
        }

        /// <summary>
        /// Sets the source and target nodes of the edge after creation without.
        /// Must be called before an edge created with CreateEdge() is added to the graph.
        /// </summary>
        /// <param name="edge">The edge to set the source and target for.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        public abstract void SetSourceAndTarget(IEdge edge, INode source, INode target);

        /// <summary>
        /// Creates an IEdge object according to this type and copies all
        /// common attributes from the given edge.
        /// </summary>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="oldEdge">The old edge.</param>
        /// <returns>The created IEdge object.</returns>
        public abstract IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldEdge);

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public EdgeType[] subOrSameTypes;
        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public EdgeType[] directSubTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public EdgeType[] superOrSameTypes;
        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public EdgeType[] directSuperTypes;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public new EdgeType[] SubOrSameTypes
        {
            [DebuggerStepThrough] get { return subOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public new EdgeType[] DirectSubTypes
        {
            [DebuggerStepThrough] get { return directSubTypes; }
        }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new EdgeType[] SuperOrSameTypes
        {
            [DebuggerStepThrough] get { return superOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public new EdgeType[] DirectSuperTypes
        {
            [DebuggerStepThrough] get { return directSuperTypes; }
        }

        /// <summary>
        /// Tells whether the given type is the same or a subtype of this type
        /// </summary>
        public abstract bool IsMyType(int typeID);

        /// <summary>
        /// Tells whether this type is the same or a subtype of the given type
        /// </summary>
        public abstract bool IsA(int typeID);

        /// <summary>
        /// The annotations of the edge type
        /// </summary>
        public abstract Annotations Annotations { get; }
    }
}
