/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A representation of a GrGen type.
    /// </summary>
    public abstract class GrGenType : INamed
    {
        /// <summary>
        /// Initializes a GrGenType object.
        /// </summary>
        /// <param name="typeID">The type id for this GrGen type.</param>
        protected GrGenType(int typeID)
        {
            TypeID = typeID;
        }

        /// <summary>
        /// An identification number of the type, unique among all other types of the same kind (node/edge) in the owning type model.
        /// </summary>
        public readonly int TypeID;

        /// <summary>
        /// The name of the type.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        public abstract string Package { get; }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        public abstract string PackagePrefixedName { get; }

        /// <summary>
        /// Checks, whether this type is compatible to the given type, i.e. this type is the same type as the given type
        /// or it is a sub type of the given type.
        /// </summary>
        /// <param name="other">The type to be compared to.</param>
        /// <returns>True, if this type is compatible to the given type.</returns>
        public abstract bool IsA(GrGenType other);

        /// <summary>
        /// Returns the name of the type.
        /// </summary>
        /// <returns>The name of the type.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
