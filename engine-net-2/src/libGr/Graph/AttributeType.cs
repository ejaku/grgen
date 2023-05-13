/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the kind of a GrGen attribute.
    /// </summary>
    public enum AttributeKind
    {
        /// <summary>The attribute is a byte (signed).</summary>
        ByteAttr,

        /// <summary>The attribute is a short.</summary>
        ShortAttr,

        /// <summary>The attribute is an integer.</summary>
        IntegerAttr,

        /// <summary>The attribute is a long.</summary>
        LongAttr,

        /// <summary>The attribute is a boolean.</summary>
        BooleanAttr,

        /// <summary>The attribute is a string.</summary>
        StringAttr,

        /// <summary>The attribute is an enum.</summary>
        EnumAttr,

        /// <summary>The attribute is a float.</summary>
        FloatAttr,

        /// <summary>The attribute is a double.</summary>
        DoubleAttr,

        /// <summary>The attribute is an object (this includes external class/attribute types).</summary>
        ObjectAttr,

        /// <summary>The attribute is a map.</summary>
        MapAttr,

        /// <summary>The attribute is a set.</summary>
        SetAttr,

        /// <summary>The attribute is an array.</summary>
        ArrayAttr,

        /// <summary>The attribute is a deque.</summary>
        DequeAttr,

        /// <summary>The attribute is a node (only valid for set/map/array/deque key/value type).</summary>
        NodeAttr,

        /// <summary>The attribute is an edge (only valid for set/map/array/deque key/value type).</summary>
        EdgeAttr,

        /// <summary>The attribute is a graph (created as a subgraph of the host graph).</summary>
        GraphAttr,

        /// <summary>The attribute is an internal class object/attribute type (non-external, non-node/edge).</summary>
        InternalClassObjectAttr,

        /// <summary>The attribute is an internal class transient object/attribute type (non-external, non-node/edge).
        /// Can only appear within transient object classes (not within node class, edge class, class).</summary>
        InternalClassTransientObjectAttr
    }

    /// <summary>
    /// Describes a GrGen attribute.
    /// </summary>
    public class AttributeType
    {
        /// <summary>
        /// The name of the attribute.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The model type owning this attribute, i.e. the type which defined this attribute.
        /// </summary>
        public readonly InheritanceType OwnerType;

        /// <summary>
        /// The kind of the attribute.
        /// </summary>
        public readonly AttributeKind Kind;

        /// <summary>
        /// The enum type description, if Kind == AttributeKind.EnumAttr. Otherwise it is null.
        /// </summary>
        public readonly EnumAttributeType EnumType;

        /// <summary>
        /// The attribute type of the value of the set, if Kind == AttributeKind.SetAttr.
        /// The attribute type of the value of the map, if Kind == AttributeKind.MapAttr.
        /// The attribute type of the value of the array, if Kind == AttributeKind.ArrayAttr.
        /// The attribute type of the value of the deque, if Kind == AttributeKind.DequeAttr.
        /// Undefined otherwise.
        /// </summary>
        public readonly AttributeType ValueType;

        /// <summary>
        /// The attribute type of the key of the map, if Kind == AttributeKind.MapAttr.
        /// Undefined otherwise.
        /// </summary>
        public readonly AttributeType KeyType;

        /// <summary>
        /// The name of the attribute type, if Kind == AttributeKind.NodeAttr || Kind == AttributeKind.EdgeAttr
        /// </summary>
        public readonly String TypeName;

        /// <summary>
        /// The package name if this is a node or edge type that is contained in a package, otherwise null
        /// </summary>
        public readonly String Package;

        /// <summary>
        /// The name of the attribute type with the package as prefix if it is contained in a package, if Kind == AttributeKind.NodeAttr || Kind == AttributeKind.EdgeAttr
        /// </summary>
        public readonly String PackagePrefixedTypeName;

        /// <summary>
        /// The .NET type of the Attribute Type
        /// </summary>
        public readonly Type Type;

        /// <summary>
        /// The annotations of the attribute
        /// </summary>
        public Annotations Annotations { get { return annotations; } }

        /// <summary>
        /// The annotations of the attribute
        /// </summary>
        public readonly Annotations annotations = new Annotations();

        /// <summary>
        /// Initializes an AttributeType instance.
        /// </summary>
        /// <param name="name">The name for the attribute.</param>
        /// <param name="ownerType">The owner model type.</param>
        /// <param name="kind">The kind of the attribute.</param>
        /// <param name="enumType">The enum type description, if Kind == AttributeKind.EnumAttr, otherwise null.</param>
        /// <param name="valueType">The attribute type of the value of the set, if Kind == AttributeKind.SetAttr; the attribute type of the value of the map, if Kind == AttributeKind.MapAttr; the attribute type of the value of the array, if Kind == AttributeKind.ArrayAttr; the attribute type of the value of the deque, if Kind == AttributeKind.DequeAttr; otherwise null. </param>
        /// <param name="keyType">The attribute type of the key of the map, if Kind == AttributeKind.MapAttr; otherwise null.</param>
        /// <param name="typeName">The name of the attribute type, if Kind == AttributeKind.NodeAttr || Kind == AttributeKind.EdgeAttr; otherwise null.</param>
        /// <param name="package">The package name if this is a node or edge type that is contained in a package, otherwise null.</param>
        /// <param name="packagePrefixedTypeName">The name of the attribute type with the package as prefix if it is contained in a package, if Kind == AttributeKind.NodeAttr || Kind == AttributeKind.EdgeAttr.</param>
        /// <param name="type">The type of the attribute type.</param>
        public AttributeType(String name, InheritanceType ownerType, AttributeKind kind,
            EnumAttributeType enumType, AttributeType valueType, AttributeType keyType,
            String typeName, String package, String packagePrefixedTypeName, Type type)
        {
            Name = name;
            OwnerType = ownerType;
            Kind = kind;
            EnumType = enumType;
            ValueType = valueType;
            KeyType = keyType;
            TypeName = typeName;
            Package = package;
            PackagePrefixedTypeName = packagePrefixedTypeName;
            Type = type;
        }

        /// <summary>
        /// Returns the name of the given basic attribute kind (enum,container not supported)
        /// </summary>
        /// <param name="attrKind">The AttributeKind value</param>
        /// <returns>The name of the attribute kind</returns>
        private static String GetKindName(AttributeKind attrKind)
        {
            switch(attrKind)
            {
                case AttributeKind.ByteAttr: return "byte";
                case AttributeKind.ShortAttr: return "short";
                case AttributeKind.IntegerAttr: return "int";
                case AttributeKind.LongAttr: return "long";
                case AttributeKind.BooleanAttr: return "boolean";
                case AttributeKind.StringAttr: return "string";
                case AttributeKind.FloatAttr: return "float";
                case AttributeKind.DoubleAttr: return "double";
                case AttributeKind.ObjectAttr: return "object";
                case AttributeKind.GraphAttr: return "graph";
            }
            return "<INVALID>";
        }

        /// <summary>
        /// Returns the name of the kind
        /// </summary>
        /// <returns>The name of the kind of the attribute</returns>
        public String GetKindName()
        {
            switch(Kind)
            {
                case AttributeKind.EnumAttr: return EnumType.PackagePrefixedName;
                case AttributeKind.MapAttr: return "map<" + KeyType.GetKindName() + "," + ValueType.GetKindName() + ">";
                case AttributeKind.SetAttr: return "set<" + ValueType.GetKindName() + ">";
                case AttributeKind.ArrayAttr: return "array<" + ValueType.GetKindName() + ">";
                case AttributeKind.DequeAttr: return "deque<" + ValueType.GetKindName() + ">";
                case AttributeKind.NodeAttr: return PackagePrefixedTypeName;
                case AttributeKind.EdgeAttr: return PackagePrefixedTypeName;
                case AttributeKind.InternalClassObjectAttr: return PackagePrefixedTypeName;
                case AttributeKind.InternalClassTransientObjectAttr: return PackagePrefixedTypeName;
            }
            return GetKindName(Kind);
        }

        public override String ToString()
        {
            return GetKindName() + " (for " + Name + ")";
        }
    }
}
