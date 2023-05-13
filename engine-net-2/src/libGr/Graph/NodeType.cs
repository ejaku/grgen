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
    /// A representation of a GrGen node type.
    /// </summary>
    public abstract class NodeType : GraphElementType
    {
        /// <summary>
        /// Constructs a NodeType instance with the given type ID.
        /// </summary>
        /// <param name="typeID">The unique type ID.</param>
        protected NodeType(int typeID)
            : base(typeID)
        {
        }

        /// <summary>
        /// Always returns true.
        /// </summary>
        public override bool IsNodeType
        {
            [DebuggerStepThrough] get { return true; }
        }

        /// <summary>
        /// This NodeType describes nodes whose real .NET interface type is named as returned (fully qualified).
        /// </summary>
        public abstract String NodeInterfaceName { get; }

        /// <summary>
        /// This NodeType describes nodes whose real .NET class type is named as returned (fully qualified).
        /// It might be null in case this type IsAbstract.
        /// </summary>
        public abstract String NodeClassName { get; }

        /// <summary>
        /// Creates an INode object according to this type.
        /// </summary>
        /// <returns>The created INode object.</returns>
        public abstract INode CreateNode();

        /// <summary>
        /// Creates an INode object according to this type and copies all
        /// common attributes from the given node.
        /// </summary>
        /// <param name="oldNode">The old node.</param>
        /// <returns>The created INode object.</returns>
        public abstract INode CreateNodeWithCopyCommons(INode oldNode);

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public NodeType[] subOrSameTypes;
        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public NodeType[] directSubTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public NodeType[] superOrSameTypes;
        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public NodeType[] directSuperTypes;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public new NodeType[] SubOrSameTypes
        {
            [DebuggerStepThrough] get { return subOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public new NodeType[] DirectSubTypes
        {
            [DebuggerStepThrough] get { return directSubTypes; }
        }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new NodeType[] SuperOrSameTypes
        {
            [DebuggerStepThrough] get { return superOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public new NodeType[] DirectSuperTypes
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
        /// The annotations of the node type
        /// </summary>
        public abstract Annotations Annotations { get; }
    }
}
