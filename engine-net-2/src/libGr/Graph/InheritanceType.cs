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
    /// A representation of a GrGen inheritance type.
    /// </summary>
    public abstract class InheritanceType : GrGenType
    {
        /// <summary>
        /// Initializes an InheritanceType object.
        /// </summary>
        /// <param name="typeID">The type id for this inheritance type.</param>
        protected InheritanceType(int typeID)
            : base(typeID)
        {
        }

        /// <summary>
        /// Array containing this type first and following all sub types.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as SubOrSameTypes of NodeType/EdgeType/ObjectType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public InheritanceType[] subOrSameGrGenTypes;

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as DirectSubTypes of NodeType/EdgeType/ObjectType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public InheritanceType[] directSubGrGenTypes;

        /// <summary>
        /// Array containing this type first and following all super types.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as SubOrSameTypes of NodeType/EdgeType/ObjectType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public InheritanceType[] superOrSameGrGenTypes;

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        /// <remarks>
        /// Must be assigned the same array as DirectSuperTypes of NodeType/EdgeType/ObjectType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </remarks>
        public InheritanceType[] directSuperGrGenTypes;

        /// <summary>
        /// Array containing this type first and following all sub types.
        /// </summary>
        public InheritanceType[] SubOrSameTypes
        {
            [DebuggerStepThrough] get { return subOrSameGrGenTypes; }
        }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public InheritanceType[] DirectSubTypes
        {
            [DebuggerStepThrough] get { return directSubGrGenTypes; }
        }

        /// <summary>
        /// Array containing this type first and following all super types.
        /// </summary>
        public InheritanceType[] SuperOrSameTypes
        {
            [DebuggerStepThrough] get { return superOrSameGrGenTypes; }
        }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public InheritanceType[] DirectSuperTypes
        {
            [DebuggerStepThrough] get { return directSuperGrGenTypes; }
        }

        /// <summary>
        /// Enumerates over all real subtypes of this type
        /// Warning: You should not use this property, but SubOrSameTypes starting from index 1,
        ///          because Enumerators in .NET are quite slow!
        /// </summary>
        public IEnumerable<InheritanceType> SubTypes
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
        public IEnumerable<InheritanceType> SuperTypes
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
        /// True, if this type has any super types, i.e. if it is not the root type.
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
        /// Checks, whether the first type is a super type of the second type or the types are the same.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if otherType is compatible to type.</returns>
        public static bool operator <=(InheritanceType type, InheritanceType otherType)
        {
            return otherType.IsA(type);
        }

        /// <summary>
        /// Checks, whether the first type is a sub type of the second type or the types are the same.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is compatible to otherType.</returns>
        public static bool operator >=(InheritanceType type, InheritanceType otherType)
        {
            return type.IsA(otherType);
        }

        /// <summary>
        /// Checks, whether the first type is a super type of the second type.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is a super type of otherType.</returns>
        public static bool operator <(InheritanceType type, InheritanceType otherType)
        {
            return type != otherType && otherType.IsA(type);
        }

        /// <summary>
        /// Checks, whether the first type is a sub type of the second type.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is a sub type of otherType.</returns>
        public static bool operator >(InheritanceType type, InheritanceType otherType)
        {
            return type != otherType && type.IsA(otherType);
        }
    }
}
