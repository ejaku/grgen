/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A type model for node or edge elements.
    /// </summary>
    public interface ITypeModel
    {
        /// <summary>
        /// Specifies whether this type model is model for nodes (= true) or for edges (= false).
        /// </summary>
        bool IsNodeModel { get; }

        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        GrGenType RootType { get; }

        /// <summary>
        /// Returns the element type with the given type name or null, if no type with this name exists.
        /// </summary>
        GrGenType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        GrGenType[] Types { get; }

        /// <summary>
        /// An array of C# types of model types.
        /// </summary>
        System.Type[] TypeTypes { get; }

        /// <summary>
        /// Enumerates all attribute types of this model.
        /// </summary>
        IEnumerable<AttributeType> AttributeTypes { get; }
    }

    /// <summary>
    /// A type model for nodes.
    /// </summary>
    public interface INodeModel : ITypeModel
    {
        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new NodeType RootType { get; }

        /// <summary>
        /// Returns the element type with the given type name or null, if no type with this name exists.
        /// </summary>
        new NodeType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new NodeType[] Types { get; }
    }

    /// <summary>
    /// A type model for edges.
    /// </summary>
    public interface IEdgeModel : ITypeModel
    {
        /// <summary>
        /// The root type of this type model. All other types of this model inherit from the root type (in the GrGen model, not in C#).
        /// </summary>
        new EdgeType RootType { get; }

        /// <summary>
        /// Returns the element type with the given type name or null, if no type with this name exists.
        /// </summary>
        new EdgeType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        new EdgeType[] Types { get; }
    }

    /// <summary>
    /// A representation of a GrGen connection assertion.
    /// Used by BaseGraph.Validate().
    /// </summary>
    public class ValidateInfo
    {
        /// <summary>
        /// The edge type to which this constraint applies.
        /// </summary>
        public readonly EdgeType EdgeType;

        /// <summary>
        /// The node type to which applicable source nodes must be compatible.
        /// </summary>
        public readonly NodeType SourceType;

        /// <summary>
        /// The node type to which applicable target nodes must be compatible.
        /// </summary>
        public readonly NodeType TargetType;

        /// <summary>
        /// The lower bound on the out-degree of the source node according to edges compatible to EdgeType.
        /// </summary>
        public readonly long SourceLower;

        /// <summary>
        /// The upper bound on the out-degree of the source node according to edges compatible to EdgeType.
        /// </summary>
        public readonly long SourceUpper;

        /// <summary>
        /// The lower bound on the in-degree of the target node according to edges compatible to EdgeType.
        /// </summary>
        public readonly long TargetLower;

        /// <summary>
        /// The upper bound on the in-degree of the target node according to edges compatible to EdgeType.
        /// </summary>
        public readonly long TargetUpper;

        /// <summary>
        /// Check the connection assertion in both directions (i.e. for reverse source and target, too)
        /// </summary>
        public readonly bool BothDirections;

        /// <summary>
        /// Constructs a ValidateInfo instance.
        /// </summary>
        /// <param name="edgeType">The edge type to which this constraint applies.</param>
        /// <param name="sourceType">The node type to which applicable source nodes must be compatible.</param>
        /// <param name="targetType">The node type to which applicable target nodes must be compatible.</param>
        /// <param name="sourceLower">The lower bound on the out-degree of the source node according to edges compatible to EdgeType.</param>
        /// <param name="sourceUpper">The upper bound on the out-degree of the source node according to edges compatible to EdgeType.</param>
        /// <param name="targetLower">The lower bound on the in-degree of the target node according to edges compatible to EdgeType.</param>
        /// <param name="targetUpper">The upper bound on the in-degree of the target node according to edges compatible to EdgeType.</param>
        /// <param name="bothDirections">Both directions are to be checked (undirected edge or arbitrary direction)</param>
        public ValidateInfo(EdgeType edgeType, NodeType sourceType, NodeType targetType,
			long sourceLower, long sourceUpper, long targetLower, long targetUpper, bool bothDirections)
        {
            EdgeType = edgeType;
            SourceType = sourceType;
            TargetType = targetType;
            SourceLower = sourceLower;
            SourceUpper = sourceUpper;
            TargetLower = targetLower;
            TargetUpper = targetUpper;
            BothDirections = bothDirections;
        }
    }

    /// <summary>
    /// Specifies the type of a connection assertion error.
    /// </summary>
    public enum CAEType
    {
        /// <summary>
        /// An edge was not specified.
        /// </summary>
        EdgeNotSpecified,

        /// <summary>
        /// A node has too few outgoing edges of some type.
        /// </summary>
        NodeTooFewSources,

        /// <summary>
        /// A node has too many outgoing edges of some type.
        /// </summary>
        NodeTooManySources,

        /// <summary>
        /// A node has too few incoming edges of some type.
        /// </summary>
        NodeTooFewTargets,

        /// <summary>
        /// A node has too many incoming edges of some type.
        /// </summary>
        NodeTooManyTargets
    }

    /// <summary>
    /// A description of an error, found during the validation process.
    /// </summary>
    public struct ConnectionAssertionError
    {
        /// <summary>
        /// The type of error.
        /// </summary>
        public CAEType CAEType;

        /// <summary>
        /// Specifies the graph element, where the error was found.
        /// </summary>
        public IGraphElement Elem;

        /// <summary>
        /// The number of edges found in the graph, if CAEType != CAEType.EdgeNotSpecified.
        /// </summary>
        public long FoundEdges;

        /// <summary>
        /// The corresponding ValidatedInfo object, if CAEType != CAEType.EdgeNotSpecified.
        /// Otherwise it is null.
        /// </summary>
        public ValidateInfo ValidateInfo;

        /// <summary>
        /// Initializes a ConnectionAssertionError instance.
        /// </summary>
        /// <param name="caeType">The type of error.</param>
        /// <param name="elem">The graph element, where the error was found.</param>
        /// <param name="found">The number of edges found in the graph, if CAEType != CAEType.EdgeNotSpecified.</param>
        /// <param name="valInfo">The corresponding ValidatedInfo object, if CAEType != CAEType.EdgeNotSpecified, otherwise null.</param>
        public ConnectionAssertionError(CAEType caeType, IGraphElement elem, long found, ValidateInfo valInfo)
        {
            CAEType = caeType; Elem = elem; FoundEdges = found;  ValidateInfo = valInfo;
        }
    }

    /// <summary>
    /// A model of a GrGen graph.
    /// </summary>
    public interface IGraphModel
    {
        /// <summary>
        /// The name of this model.
        /// </summary>
        String ModelName { get; }

        /// <summary>
        /// The model of the nodes.
        /// </summary>
        INodeModel NodeModel { get; }

        /// <summary>
        /// The model of the edges.
        /// </summary>
        IEdgeModel EdgeModel { get; }

        /// <summary>
        /// Enumerates all enum attribute types declared for this model.
        /// </summary>
        IEnumerable<EnumAttributeType> EnumAttributeTypes { get; }

        /// <summary>
        /// Enumerates all ValidateInfo objects describing constraints on the graph structure.
        /// </summary>
        IEnumerable<ValidateInfo> ValidateInfo { get; }

        /// <summary>
        /// An MD5 hash sum of the model.
        /// </summary>
        String MD5Hash { get; }
    }

    // Attributes

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

        /// <summary>The attribute is an object.</summary>
        ObjectAttr,

        /// <summary>The attribute is a map.</summary>
        MapAttr,

        /// <summary>The attribute is a set.</summary>
        SetAttr,

        /// <summary>The attribute is an array.</summary>
        ArrayAttr,

        /// <summary>The attribute is a node (only valid for set/map/array key/value type).</summary>
        NodeAttr,

        /// <summary>The attribute is an edge (only valid for set/map/array key/value type).</summary>
        EdgeAttr,

        /// <summary>The attribute is a graph (created as a subgraph of the host graph).</summary>
        GraphAttr
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
        public readonly GrGenType OwnerType;

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
        /// The annotations of the attribute
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }

        /// <summary>
        /// The annotations of the attribute
        /// </summary>
        public IDictionary<string, string> annotations = new Dictionary<string, string>();

        /// <summary>
        /// Initializes an AttributeType instance.
        /// </summary>
        /// <param name="name">The name for the attribute.</param>
        /// <param name="ownerType">The owner model type.</param>
        /// <param name="kind">The kind of the attribute.</param>
        /// <param name="enumType">The enum type description, if Kind == AttributeKind.EnumAttr, otherwise null.</param>
        /// <param name="valueType">The attribute type of the value of the set, if Kind == AttributeKind.SetAttr; the attribute type of the value of the map, if Kind == AttributeKind.MapAttr; the attribute type of the value of the array, if Kind == AttributeKind.ArrayAttr; otherwise null. </param>
        /// <param name="keyType">The attribute type of the key of the map, if Kind == AttributeKind.MapAttr; otherwise null.</param>
        /// <param name="typeName">The name of the attribute type, if Kind == AttributeKind.NodeAttr || Kind == AttributeKind.EdgeAttr; otherwise null.</param>
        public AttributeType(String name, GrGenType ownerType, AttributeKind kind,
            EnumAttributeType enumType, AttributeType valueType, AttributeType keyType,
            String typeName)
        {
            Name = name;
            OwnerType = ownerType;
            Kind = kind;
            EnumType = enumType;
            ValueType = valueType;
            KeyType = keyType;
            TypeName = typeName;
        }

        /// <summary>
        /// Returns the name of the given basic attribute kind (not enum,set,map)
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
                case AttributeKind.EnumAttr: return EnumType.Name;
                case AttributeKind.SetAttr: return "set<" + ValueType.GetKindName() + ">";
                case AttributeKind.ArrayAttr: return "array<" + ValueType.GetKindName() + ">";
                case AttributeKind.MapAttr: return "map<" + KeyType.GetKindName() + "," + ValueType.GetKindName() + ">";
                case AttributeKind.NodeAttr: return TypeName;
                case AttributeKind.EdgeAttr: return TypeName;
            }
            return GetKindName(Kind);
        }
    }

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
            if (Value > other.Value) return 1;
            else if (Value < other.Value) return -1;
            else return 0;
        }
    }

    /// <summary>
    /// A description of a GrGen enum type.
    /// </summary>
    public class EnumAttributeType
    {
        /// <summary>
        /// The name of the enum type.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The .NET type for the enum type.
        /// </summary>
        public readonly Type EnumType;

        private EnumMember[] members;

        /// <summary>
        /// Initializes an EnumAttributeType instance.
        /// </summary>
        /// <param name="name">The name of the enum type.</param>
        /// <param name="enumType">The .NET type for the enum type.</param>
        /// <param name="memberArray">An array of all enum members.</param>
        public EnumAttributeType(String name, Type enumType, EnumMember[] memberArray)
        {
            Name = name;
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
                while (lowIndex < highIndex)
                {
                    int midIndex = lowIndex + ((highIndex - lowIndex) / 2);
                    if (value > members[midIndex].Value)
                        lowIndex = midIndex + 1;
                    else
                        highIndex = midIndex;
                }
                // high==low
                if ((lowIndex < members.Length) && (members[lowIndex].Value == value))
                    return members[lowIndex];
                else
                    return null;
            }
        }
    }

    /// <summary>
    /// A dummy type used as value type for dictionaries representing sets.
    /// </summary>
    public abstract class SetValueType : IEquatable<SetValueType>
    {
        public bool Equals(SetValueType other) { return true; }
    }

    /// <summary>
    /// A representation of a GrGen graph element type.
    /// </summary>
    public abstract class GrGenType
    {
        /// <summary>
        /// Initializes a GrGenType object.
        /// </summary>
        /// <param name="typeID">The type id for this GrGen type.</param>
        public GrGenType(int typeID)
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
        public GrGenType[] SubOrSameTypes { [DebuggerStepThrough] get { return subOrSameGrGenTypes; } }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public GrGenType[] DirectSubTypes { [DebuggerStepThrough] get { return directSubGrGenTypes; } }

        /// <summary>
        /// Array containing this type first and following all super types.
        /// </summary>
        public GrGenType[] SuperOrSameTypes { [DebuggerStepThrough] get { return superOrSameGrGenTypes; } }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public GrGenType[] DirectSuperTypes { [DebuggerStepThrough] get { return directSuperGrGenTypes; } }

        /// <summary>
        /// Enumerates over all real subtypes of this type
        /// Warning: You should not use this property, but SubOrSameTypes starting from index 1,
        ///          because Enumerators in .NET are quite slow!
        /// </summary>
        public IEnumerable<GrGenType> SubTypes
        {
            get
            {
                for(int i = 1; i < SubOrSameTypes.Length; i++)
                    yield return SubOrSameTypes[i];
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
                for(int i = 1; i < SuperOrSameTypes.Length; i++)
                    yield return SuperOrSameTypes[i];
            }
        }

        /// <summary>
        /// True, if this type has any super types, i.e. if it is not the node/edge root type.
        /// </summary>
        public bool HasSuperTypes { [DebuggerStepThrough] get { return SuperOrSameTypes.Length > 1; } }

        /// <summary>
        /// True, if this type has any sub types.
        /// </summary>
        public bool HasSubTypes { [DebuggerStepThrough] get { return SubOrSameTypes.Length > 1; } }

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

    /// <summary>
    /// A representation of a GrGen node type.
    /// </summary>
    public abstract class NodeType : GrGenType
    {
        /// <summary>
        /// Constructs a NodeType instance with the given type ID.
        /// </summary>
        /// <param name="typeID">The unique type ID.</param>
        public NodeType(int typeID) : base(typeID) { }

        /// <summary>
        /// Always returns true.
        /// </summary>
        public override bool IsNodeType { [DebuggerStepThrough] get { return true; } }

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
        public new NodeType[] SubOrSameTypes { [DebuggerStepThrough] get { return subOrSameTypes; } }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public new NodeType[] DirectSubTypes { [DebuggerStepThrough] get { return directSubTypes; } }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new NodeType[] SuperOrSameTypes { [DebuggerStepThrough] get { return superOrSameTypes; } }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public new NodeType[] DirectSuperTypes { [DebuggerStepThrough] get { return directSuperTypes; } }

        /// <summary>
        /// The annotations of the node type
        /// </summary>
        public abstract IEnumerable<KeyValuePair<string, string>> Annotations { get; }
    }

    /// <summary>
    /// Specifies the kind of directedness for an EdgeType
    /// </summary>
    public enum Directedness
    {
        /// <summary>Arbitrary directed. Only for abstract edge types.</summary>
        Arbitrary,
        /// <summary>Directed.</summary>
        Directed,
        /// <summary>Undirected.</summary>
        Undirected
    }

    /// <summary>
    /// A representation of a GrGen edge type.
    /// </summary>
    public abstract class EdgeType : GrGenType
    {
        /// <summary>
        /// Constructs an EdgeType instance with the given type ID.
        /// </summary>
        /// <param name="typeID">The unique type ID.</param>
        public EdgeType(int typeID) : base(typeID) { }

        /// <summary>
        /// Always returns false.
        /// </summary>
        public override bool IsNodeType { [DebuggerStepThrough] get { return false; } }

        /// <summary>
        /// This EdgeType describes edges whose real .NET interface type is named as returned (fully qualified).
        /// </summary>
        public abstract String EdgeInterfaceName { get; }

        /// <summary>
        /// This EdgeType describes edges whose real .NET class type is named as returned (fully qualified).
        /// It might be null in case this type IsAbstract.
        /// </summary>
        public abstract String EdgeClassName { get; }

        /// <summary>
        /// Specifies the directedness of this edge type.
        /// </summary>
        public abstract Directedness Directedness { get; }

        /// <summary>
        /// Creates an IEdge object according to this type.
        /// </summary>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The created IEdge object.</returns>
        public abstract IEdge CreateEdge(INode source, INode target);

        /// <summary>
        /// Creates an IEdge object according to this type and copies all
        /// common attributes from the given edge.
        /// </summary>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="oldEdge">The old edge.</param>
        /// <returns>The created IEdge object.</returns>
        public abstract IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldEdge);

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public EdgeType[] subOrSameTypes;
        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public EdgeType[] directSubTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public EdgeType[] superOrSameTypes;
        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public EdgeType[] directSuperTypes;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public new EdgeType[] SubOrSameTypes { [DebuggerStepThrough] get { return subOrSameTypes; } }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public new EdgeType[] DirectSubTypes { [DebuggerStepThrough] get { return directSubTypes; } }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new EdgeType[] SuperOrSameTypes { [DebuggerStepThrough] get { return superOrSameTypes; } }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public new EdgeType[] DirectSuperTypes { [DebuggerStepThrough] get { return directSuperTypes; } }

        /// <summary>
        /// The annotations of the edge type
        /// </summary>
        public abstract IEnumerable<KeyValuePair<string, string>> Annotations { get; }
    }

    /// <summary>
    /// A representation of a GrGen variable type.
    /// </summary>
    public class VarType : GrGenType
    {
        static int nextID = 0;

        /// <summary>
        /// A map from .NET types to singleton VarTypes.
        /// </summary>
        private static Dictionary<Type, VarType> varTypeMap = new Dictionary<Type, VarType>();

        private Type type;

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
        /// The .NET type of the variable.
        /// </summary>
        public Type Type { [DebuggerStepThrough] get { return type; } }

        public override bool IsNodeType
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool IsConst
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool IsAbstract
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override int NumAttributes
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override IEnumerable<AttributeType> AttributeTypes
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override AttributeType GetAttributeType(string name)
        {
            throw new Exception("The method or operation is not implemented.");
        }

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
