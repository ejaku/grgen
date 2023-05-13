/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A representation of a GrGen base object type, i.e. class of internal non-node/edge values.
    /// </summary>
    public abstract class BaseObjectType : InheritanceType
    {
        /// <summary>
        /// Constructs a BaseObjectType (non-graph-element internal class) instance with the given type ID.
        /// </summary>
        /// <param name="typeID">The unique type ID.</param>
        protected BaseObjectType(int typeID)
            : base(typeID)
        {
        }

        /// <summary>
        /// This BaseObjectType describes classes whose real .NET interface type is named as returned (fully qualified).
        /// </summary>
        public abstract String BaseObjectInterfaceName { get; }

        /// <summary>
        /// This BaseObjectType describes classes whose real .NET class type is named as returned (fully qualified).
        /// It might be null in case this type IsAbstract.
        /// </summary>
        public abstract String BaseObjectClassName { get; }

        /// <summary>
        /// Creates a base object according to this type.
        /// </summary>
        /// <returns>The created object.</returns>
        public abstract IBaseObject CreateBaseObject();

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public new BaseObjectType[] SubOrSameTypes { get; }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public new BaseObjectType[] DirectSubTypes { get; }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new BaseObjectType[] SuperOrSameTypes { get; }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public new BaseObjectType[] DirectSuperTypes { get; }

        /// <summary>
        /// Tells whether the given type is the same or a subtype of this type
        /// </summary>
        public abstract bool IsMyType(int typeID);

        /// <summary>
        /// Tells whether this type is the same or a subtype of the given type
        /// </summary>
        public abstract bool IsA(int typeID);

        /// <summary>
        /// The annotations of the transient class type
        /// </summary>
        public abstract Annotations Annotations { get; }
    }
}
