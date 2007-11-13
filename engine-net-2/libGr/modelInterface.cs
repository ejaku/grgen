using System;
using System.Collections.Generic;
using System.Reflection.Emit;

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
        Type[] TypeTypes { get; }

        /// <summary>
        /// Enumerates all attribute types of this model.
        /// </summary>
        IEnumerable<AttributeType> AttributeTypes { get; }
    }

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

        public readonly int SourceLower;
        public readonly int SourceUpper;
        public readonly int TargetLower;
        public readonly int TargetUpper;

        public ValidateInfo(EdgeType edgeType, NodeType sourceType, NodeType targetType,
            int sourceLower, int sourceUpper, int targetLower, int targetUpper)
        {
            EdgeType = edgeType;
            SourceType = sourceType;
            TargetType = targetType;
            SourceLower = sourceLower;
            SourceUpper = sourceUpper;
            TargetLower = targetLower;
            TargetUpper = targetUpper;
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
        public int FoundEdges;

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
        public ConnectionAssertionError(CAEType caeType, IGraphElement elem, int found, ValidateInfo valInfo)
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
        String Name { get; }

        /// <summary>
        /// The model of the nodes.
        /// </summary>
        INodeModel NodeModel { get; }

        /// <summary>
        /// The model of the edges.
        /// </summary>
        IEdgeModel EdgeModel { get; }

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
    public enum AttributeKind { IntegerAttr, BooleanAttr, StringAttr, EnumAttr, FloatAttr, DoubleAttr, ObjectAttr }

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
        /// Initializes an AttributeType instance.
        /// </summary>
        /// <param name="name">The name for the attribute.</param>
        /// <param name="ownerType">The owner model type.</param>
        /// <param name="kind">The kind of the attribute.</param>
        /// <param name="enumType">The enum type description, if Kind == AttributeKind.EnumAttr, otherwise null.</param>
        public AttributeType(String name, GrGenType ownerType, AttributeKind kind, EnumAttributeType enumType)
        {
            Name = name;
            OwnerType = ownerType;
            Kind = kind;
            EnumType = enumType;
        }
    }

    /// <summary>
    /// A description of a GrGen enum member.
    /// </summary>
    public class EnumMember
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

        private EnumMember[] members;

        /// <summary>
        /// Initializes an EnumAttributeType instance.
        /// </summary>
        /// <param name="name">The name of the enum type.</param>
        /// <param name="memberArray">An array of all enum members.</param>
        public EnumAttributeType(String name, EnumMember[] memberArray)
        {
            Name = name;
            members = memberArray;
        }

        /// <summary>
        /// Enumerates all enum members.
        /// </summary>
        public IEnumerable<EnumMember> Members { get { return members; } }
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
        /// Must be assigned the same array as SubOrSameTypes of NodeType/EdgeType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </summary>
        public GrGenType[] subOrSameGrGenTypes;

        /// <summary>
        /// Array containing this type first and following all super types.
        /// Must be assigned the same array as SubOrSameTypes of NodeType/EdgeType.
        /// It is ugly, but one of the few ways to override a property of
        /// an abstract class with another return type.
        /// Not meant to be used by users, but public because of assignments from
        /// generated code.
        /// </summary>
        public GrGenType[] superOrSameGrGenTypes;

        /// <summary>
        /// Array containing this type first and following all sub types.
        /// </summary>
        public GrGenType[] SubOrSameTypes { get { return subOrSameGrGenTypes; } }

        /// <summary>
        /// Array containing this type first and following all super types.
        /// </summary>
        public GrGenType[] SuperOrSameTypes { get { return superOrSameGrGenTypes; } }

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
        public bool HasSuperTypes { get { return SuperOrSameTypes.Length > 1; } }

        /// <summary>
        /// True, if this type has any sub types.
        /// </summary>
        public bool HasSubTypes { get { return SubOrSameTypes.Length > 1; } }

        /// <summary>
        /// True, if this type is a node type.
        /// </summary>
        public abstract bool IsNodeType { get; }

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
    }

    public abstract class NodeType : GrGenType
    {
        public NodeType(int typeID) : base(typeID) { }

        /// <summary>
        /// Always returns true.
        /// </summary>
        public override bool IsNodeType { get { return true; } }

        /// <summary>
        /// Creates an INode object according to this type.
        /// </summary>
        /// <returns>The created INode object.</returns>
        public abstract INode CreateNode();

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public NodeType[] subOrSameTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public NodeType[] superOrSameTypes;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public new NodeType[] SubOrSameTypes { get { return subOrSameTypes; } }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new NodeType[] SuperOrSameTypes { get { return superOrSameTypes; } }

        /// <summary>
        /// Retypes a given node of a given graph to a node of this type.
        /// All adjacent nodes and all common attributes are kept.
        /// </summary>
        /// <param name="graph">The graph containing the node.</param>
        /// <param name="oldNode">The old node.</param>
        /// <returns>A new node of this type.</returns>
        public abstract INode Retype(IGraph graph, INode oldNode);
    }

    public abstract class EdgeType : GrGenType
    {
        public EdgeType(int typeID) : base(typeID) { }

        /// <summary>
        /// Always returns false.
        /// </summary>
        public override bool IsNodeType { get { return false; } }

        /// <summary>
        /// Creates an IEdge object according to this type.
        /// </summary>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The created IEdge object.</returns>
        public abstract IEdge CreateEdge(INode source, INode target);

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public EdgeType[] subOrSameTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public EdgeType[] superOrSameTypes;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public EdgeType[] SubOrSameTypes { get { return subOrSameTypes; } }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public EdgeType[] SuperOrSameTypes { get { return superOrSameTypes; } }

        /// <summary>
        /// Retypes a given edge of a given graph to a edge of this type.
        /// Source and target node as well as all common attributes are kept.
        /// </summary>
        /// <param name="graph">The graph containing the edge.</param>
        /// <param name="oldEdge">The old edge.</param>
        /// <returns>A new edge of this type.</returns>
        public abstract IEdge Retype(IGraph graph, IEdge oldEdge);
    }

#if OLD
    /// <summary>
    /// A helper class for the type framework
    /// </summary>
    public abstract class TypeFramework<TypeClass, AttributeClass, AttributeInterface> : GrGenType
        where TypeClass : class, new()
        where AttributeClass : class, IAttributes, new()
    {
        /// <summary>
        /// A singleton instance of the GrGen element type TypeClass
        /// </summary>
        public static TypeClass typeVar = new TypeClass();

        public static bool[] isA;
        public static bool[] isMyType;

        /// <summary>
        /// Checks, whether this type is compatible to the given type, i.e. this type is the same type as the given type
        /// or it is a sub type of the given type.
        /// </summary>
        /// <param name="other">The type to be compared to.</param>
        /// <returns>True, if this type is compatible to the given type.</returns>
        public override bool IsA(IType other)
        {
            // return other.GetType().IsAssignableFrom(typeof(T));

            // return ((ITypeFramework)other).IsMyType(this);            
            return (this == other) || isA[((GrGenType) other).TypeID];          // first condition just for speed up
        }

/*        public override IAttributes CreateAttributes()                                  // grs newRule{79998} mountRule requestRule{80000}
        {
            return (IAttributes) Activator.CreateInstance(AttributesType);            // rewrite: 846 ms
//            return (IAttributes) Activator.CreateInstance<AttributeClass>();          // rewrite: 1733 ms
//            return new AttributeClass();                                                // rewrite: 1811 ms (without class constraint)
        }*/
    }
#endif

    /// <summary>
    /// A interface implemented by all GrGen attribute classes.
    /// </summary>
    public interface IAttributes : ICloneable
    {
    }
}