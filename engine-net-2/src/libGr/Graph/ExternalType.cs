/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    /// A representation of an external type registered with GrGen.
    /// The bottom type of the external type hierarchy that is always available is type object.
    /// </summary>
    public abstract class ExternalType
    {
        public ExternalType(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public void InitDirectSupertypes(ExternalType[] directSuperTypes)
        {
            this.directSuperTypes = directSuperTypes;
        }

        /// <summary>
        /// The name of the type.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The .NET type of the type.
        /// </summary>
        public readonly Type Type;

        /// <summary>
        /// Array containing all direct super types of this type, the readonly interface.
        /// </summary>
        public ExternalType[] DirectSuperTypes { get { return directSuperTypes; } }

        /// <summary>
        /// Array containing all direct super types of this type, the real array.
        /// </summary>
        private ExternalType[] directSuperTypes;

        /// <summary>
        /// Checks, whether this type is compatible to the given type, i.e. this type is the same type as the given type
        /// or it is a sub type of the given type.
        /// </summary>
        /// <param name="that">The type to be compared to.</param>
        /// <returns>True, if this type is compatible to the given type.</returns>
        public bool IsA(ExternalType that)
        {
            if(this == that)
                return true;
            for(int i = 0; i < directSuperTypes.Length; ++i)
                if(directSuperTypes[i].IsA(that))
                    return true;
            return false;
        }

        /// <summary>
        /// The number of function methods of this type.
        /// </summary>
        public abstract int NumFunctionMethods { get; }

        /// <summary>
        /// Enumerates all function methods of this type.
        /// </summary>
        public abstract IEnumerable<IFunctionDefinition> FunctionMethods { get; }

        /// <summary>
        /// Returns a function definition object for the given function method name.
        /// If this type does not have a function method with this name, null is returned.
        /// </summary>
        /// <param name="name">Name of the function method</param>
        /// <returns>The function definition of the function method matching the name, or null if there is no such</returns>
        public abstract IFunctionDefinition GetFunctionMethod(String name);

        /// <summary>
        /// The number of procedure methods of this type.
        /// </summary>
        public abstract int NumProcedureMethods { get; }

        /// <summary>
        /// Enumerates all procedure methods of this type.
        /// </summary>
        public abstract IEnumerable<IProcedureDefinition> ProcedureMethods { get; }

        /// <summary>
        /// Returns a procedure definition object for the given procedure method name.
        /// If this type does not have a procedure method with this name, null is returned.
        /// </summary>
        /// <param name="name">Name of the procedure method</param>
        /// <returns>The procedure definition of the procedure method matching the name, or null if there is no such</returns>
        public abstract IProcedureDefinition GetProcedureMethod(String name);

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
