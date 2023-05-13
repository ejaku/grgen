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
    /// A representation of a GrGen transient object type, i.e. class of internal non-node/edge values.
    /// </summary>
    public abstract class TransientObjectType : BaseObjectType
    {
        /// <summary>
        /// Constructs a TransientObjectType (non-graph-element internal class) instance with the given type ID.
        /// </summary>
        /// <param name="typeID">The unique type ID.</param>
        protected TransientObjectType(int typeID)
            : base(typeID)
        {
        }

        /// <summary>
        /// This TransientObjectType describes classes whose real .NET interface type is named as returned (fully qualified).
        /// </summary>
        public abstract String TransientObjectInterfaceName { get; }

        public override String BaseObjectInterfaceName
        {
            get { return TransientObjectInterfaceName; }
        }

        /// <summary>
        /// This TransientObjectType describes classes whose real .NET class type is named as returned (fully qualified).
        /// It might be null in case this type IsAbstract.
        /// </summary>
        public abstract String TransientObjectClassName { get; }

        public override String BaseObjectClassName
        {
            get { return TransientObjectClassName; }
        }

        /// <summary>
        /// Creates a transient object according to this type.
        /// </summary>
        /// <returns>The created transient object.</returns>
        public abstract ITransientObject CreateTransientObject();

        /// <summary>
        /// Creates a transient object according to this type.
        /// </summary>
        /// <returns>The created transient object.</returns>
        public override IBaseObject CreateBaseObject()
        {
            return CreateTransientObject();
        }

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public TransientObjectType[] subOrSameTypes;
        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public TransientObjectType[] directSubTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public TransientObjectType[] superOrSameTypes;
        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public TransientObjectType[] directSuperTypes;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public new TransientObjectType[] SubOrSameTypes
        {
            [DebuggerStepThrough]
            get { return subOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public new TransientObjectType[] DirectSubTypes
        {
            [DebuggerStepThrough]
            get { return directSubTypes; }
        }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new TransientObjectType[] SuperOrSameTypes
        {
            [DebuggerStepThrough]
            get { return superOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public new TransientObjectType[] DirectSuperTypes
        {
            [DebuggerStepThrough]
            get { return directSuperTypes; }
        }

        /// <summary>
        /// Tells whether the given type is the same or a subtype of this type
        /// </summary>
        public override abstract bool IsMyType(int typeID);

        /// <summary>
        /// Tells whether this type is the same or a subtype of the given type
        /// </summary>
        public override abstract bool IsA(int typeID);

        /// <summary>
        /// The annotations of the transient class type
        /// </summary>
        public override abstract Annotations Annotations { get; }
    }
}
