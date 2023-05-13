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
    /// A description of a GrGen enum member.
    /// </summary>
    public class EnumMember : IComparable<EnumMember>
    {
        /// <summary>
        /// The integer value of the enum member.
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// The name of the enum member.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// Initializes an EnumMember instance.
        /// </summary>
        /// <param name="value">The value of the enum member.</param>
        /// <param name="name">The name of the enum member.</param>
        public EnumMember(int value, String name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Defines order on enum members along the values (NOT the names)
        /// </summary>
        public int CompareTo(EnumMember other)
        {
            if(Value > other.Value)
                return 1;
            else if(Value < other.Value)
                return -1;
            else
                return 0;
        }
    }

    /// <summary>
    /// A description of a GrGen enum type.
    /// </summary>
    public class EnumAttributeType : INamed
    {
        /// <summary>
        /// The name of the enum type.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// null if this is a global enum type, otherwise the package the enum type is contained in.
        /// </summary>
        public string Package
        {
            get { return package; }
        }

        /// <summary>
        /// The name of the enum type in case of a global enum type,
        /// the name of the enum type prefixed by the name of the package otherwise.
        /// </summary>
        public string PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public readonly String name;
        public readonly String package;
        public readonly String packagePrefixedName;

        /// <summary>
        /// The .NET type for the enum type.
        /// </summary>
        public readonly Type EnumType;

        private readonly EnumMember[] members;

        /// <summary>
        /// Initializes an EnumAttributeType instance.
        /// </summary>
        /// <param name="name">The name of the enum type.</param>
        /// <param name="package">The package the enum is contained in, or null if it is not contained in a package.</param>
        /// <param name="packagePrefixedName">The name of the enum type; prefixed by the package name plus a double colon, in case it is contain in a package.</param>
        /// <param name="enumType">The .NET type for the enum type.</param>
        /// <param name="memberArray">An array of all enum members.</param>
        public EnumAttributeType(String name, String package, String packagePrefixedName, Type enumType, EnumMember[] memberArray)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
            EnumType = enumType;
            members = memberArray;
            Array.Sort<EnumMember>(members); // ensure the ordering needed for the binary search of the [] operator
        }

        /// <summary>
        /// Enumerates all enum members.
        /// </summary>
        public IEnumerable<EnumMember> Members { [DebuggerStepThrough] get { return members; } }

        /// <summary>
        /// Returns an enum member corresponding the given enum member integer or null if no such member exists
        /// </summary>
        public EnumMember this[int value]
        {
            get
            {
                int lowIndex = 0;
                int highIndex = members.Length;
                while(lowIndex < highIndex)
                {
                    int midIndex = lowIndex + ((highIndex - lowIndex) / 2);
                    if(value > members[midIndex].Value)
                        lowIndex = midIndex + 1;
                    else
                        highIndex = midIndex;
                }
                // high==low
                if((lowIndex < members.Length) && (members[lowIndex].Value == value))
                    return members[lowIndex];
                else
                    return null;
            }
        }
    }
}
