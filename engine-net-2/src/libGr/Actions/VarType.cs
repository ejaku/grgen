/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A representation of a GrGen variable type.
    /// </summary>
    public class VarType : GrGenType
    {
        static int nextID = 0;

        /// <summary>
        /// A map from .NET types to singleton VarTypes.
        /// </summary>
        private static readonly Dictionary<Type, VarType> varTypeMap = new Dictionary<Type, VarType>();

        private readonly Type type;

        private VarType(Type varType) : base(nextID++)
        {
            type = varType;
        }

        /// <summary>
        /// Gets the singleton VarType object for a given .NET type.
        /// </summary>
        /// <param name="type">The .NET type.</param>
        /// <returns>The singleton VarType object.</returns>
        public static VarType GetVarType(Type type)
        {
            VarType varType;
            if(!varTypeMap.TryGetValue(type, out varType))
                varTypeMap[type] = varType = new VarType(type);
            return varType;
        }

        /// <summary>
        /// The name of the type.
        /// </summary>
        public override string Name
        {
            [DebuggerStepThrough]
            get { return type.Name; }
        }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        public override string Package
        {
            [DebuggerStepThrough]
            get
            {
                if(type.FullName.StartsWith("de.unika.ipd.grGen."))
                {
                    String fullTypeName = type.FullName;
                    fullTypeName = fullTypeName.Substring(19); // remove "de.unika.ipd.grGen."
                    fullTypeName = fullTypeName.Substring(fullTypeName.IndexOf('.') + 1); // remove "model_XXX."
                    string[] packageAndType = fullTypeName.Split('.');
                    if(packageAndType.Length == 2) // otherwise it's only a type name without package/namespace
                        return packageAndType[0];
                }
                return null;
            }
        }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        public override string PackagePrefixedName
        {
            [DebuggerStepThrough]
            get
            {
                if(type.FullName.StartsWith("de.unika.ipd.grGen."))
                {
                    String fullTypeName = type.FullName;
                    fullTypeName = fullTypeName.Substring(19); // remove "de.unika.ipd.grGen."
                    fullTypeName = fullTypeName.Substring(fullTypeName.IndexOf('.') + 1); // remove "model_XXX."
                    return fullTypeName;
                }
                return type.Name;
            }
        }

        /// <summary>
        /// The .NET type of the variable.
        /// </summary>
        public Type Type { [DebuggerStepThrough] get { return type; } }

        /// <summary>
        /// Checks, whether this type is compatible to the given type, i.e. this type is the same type as the given type
        /// or it is a sub type of the given type.
        /// </summary>
        /// <param name="other">The type to be compared to.</param>
        /// <returns>True, if this type is compatible to the given type.</returns>
        public override bool IsA(GrGenType other)
        {
            VarType o = other as VarType;
            if(o == null) return false;
            return o.type.IsAssignableFrom(type);
        }

        /// <summary>
        /// Checks, whether the given type equals this type.
        /// </summary>
        /// <param name="other">The type to be compared to.</param>
        /// <returns>True, if the given type is the same as this type.</returns>
        public override bool Equals(object other)
        {
            VarType o = other as VarType;
            if(o == null) return false;
            return type == o.type;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return type.GetHashCode();
        }
    }
}
