/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    // TODO: create common base for node type and edge type only, one one for node type, edge type, and var type
    // the var type is not building a type hierarchy, as is the case for the node and edge types

    /// <summary>
    /// A representation of a GrGen graph element type.
    /// </summary>
    public abstract class GrGenType
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
        public abstract String Name { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        public abstract String Package { get; }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        public abstract String PackagePrefixedName { get; }

        /// <summary>
        /// Array containing this type first and following all sub types.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as SubOrSameTypes of NodeType/EdgeType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public GrGenType[] subOrSameGrGenTypes;

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as DirectSubTypes of NodeType/EdgeType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public GrGenType[] directSubGrGenTypes;

        /// <summary>
        /// Array containing this type first and following all super types.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as SubOrSameTypes of NodeType/EdgeType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public GrGenType[] superOrSameGrGenTypes;

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as DirectSuperTypes of NodeType/EdgeType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public GrGenType[] directSuperGrGenTypes;

        /// <summary>
        /// Array containing this type first and following all sub types.
        /// </summary>
        public GrGenType[] SubOrSameTypes
        {
            [DebuggerStepThrough] get { return subOrSameGrGenTypes; }
        }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public GrGenType[] DirectSubTypes
        {
            [DebuggerStepThrough] get { return directSubGrGenTypes; }
        }

        /// <summary>
        /// Array containing this type first and following all super types.
        /// </summary>
        public GrGenType[] SuperOrSameTypes
        {
            [DebuggerStepThrough] get { return superOrSameGrGenTypes; }
        }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public GrGenType[] DirectSuperTypes
        {
            [DebuggerStepThrough] get { return directSuperGrGenTypes; }
        }

        /// <summary>
        /// Enumerates over all real subtypes of this type
        /// Warning: You should not use this property, but SubOrSameTypes starting from index 1,
        ///          because Enumerators in .NET are quite slow!
        /// </summary>
        public IEnumerable<GrGenType> SubTypes
        {
            get
            {
                for(int i = 1; i < SubOrSameTypes.Length; ++i)
                {
                    yield return SubOrSameTypes[i];
                }
            }
        }

        /// <summary>
        /// Enumerates over all real supertypes of this type
        /// Warning: You should not use this property, but SuperOrSameTypes starting from index 1,
        ///          because Enumerators in .NET are quite slow!
        /// </summary>
        public IEnumerable<GrGenType> SuperTypes
        {
            get
            {
                for(int i = 1; i < SuperOrSameTypes.Length; ++i)
                {
                    yield return SuperOrSameTypes[i];
                }
            }
        }

        /// <summary>
        /// True, if this type has any super types, i.e. if it is not the node/edge root type.
        /// </summary>
        public bool HasSuperTypes
        {
            [DebuggerStepThrough] get { return SuperOrSameTypes.Length > 1; }
        }

        /// <summary>
        /// True, if this type has any sub types.
        /// </summary>
        public bool HasSubTypes
        {
            [DebuggerStepThrough] get { return SubOrSameTypes.Length > 1; }
        }

        /// <summary>
        /// True, if this type is a node type.
        /// </summary>
        public abstract bool IsNodeType { get; }

        /// <summary>
        /// True, if this type is an abstract element type.
        /// </summary>
        public abstract bool IsAbstract { get; }

        /// <summary>
        /// True, if this type is a const element type.
        /// </summary>
        public abstract bool IsConst { get; }

        /// <summary>
        /// The number of attributes of this type.
        /// </summary>
        public abstract int NumAttributes { get; }

        /// <summary>
        /// Enumerates all attribute types of this type.
        /// </summary>
        public abstract IEnumerable<AttributeType> AttributeTypes { get; }

        /// <summary>
        /// Returns an AttributeType object for the given attribute name.
        /// If this type does not have an attribute with this name, null is returned.
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <returns>The AttributeType matching the name, or null if there is no such</returns>
        public abstract AttributeType GetAttributeType(String name);

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
        /// Checks, whether this type is compatible to the given type, i.e. this type is the same type as the given type
        /// or it is a sub type of the given type.
        /// </summary>
        /// <param name="other">The type to be compared to.</param>
        /// <returns>True, if this type is compatible to the given type.</returns>
        public abstract bool IsA(GrGenType other);

        /// <summary>
        /// Checks, whether the first type is a super type of the second type or the types are the same.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if otherType is compatible to type.</returns>
        public static bool operator <=(GrGenType type, GrGenType otherType)
        {
            return otherType.IsA(type);
        }

        /// <summary>
        /// Checks, whether the first type is a sub type of the second type or the types are the same.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is compatible to otherType.</returns>
        public static bool operator >=(GrGenType type, GrGenType otherType)
        {
            return type.IsA(otherType);
        }

        /// <summary>
        /// Checks, whether the first type is a super type of the second type.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is a super type of otherType.</returns>
        public static bool operator <(GrGenType type, GrGenType otherType)
        {
            return type != otherType && otherType.IsA(type);
        }

        /// <summary>
        /// Checks, whether the first type is a sub type of the second type.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is a sub type of otherType.</returns>
        public static bool operator >(GrGenType type, GrGenType otherType)
        {
            return type != otherType && type.IsA(otherType);
        }

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
