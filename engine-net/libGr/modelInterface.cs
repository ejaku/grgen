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
        IType RootType { get; }

        /// <summary>
        /// Returns the element type with the given type name or null, if no type with this name exists
        /// </summary>
        IType GetType(String name);

        /// <summary>
        /// An array of all types in this type model.
        /// </summary>
        IType[] Types { get; }

        /// <summary>
        /// An array of C# types of model types.
        /// </summary>
        Type[] TypeTypes { get; }

        /// <summary>
        /// Enumerates all attribute types of this model.
        /// </summary>
        IEnumerable<AttributeType> AttributeTypes { get; }
    }

    /// <summary>
    /// A description of an edge constraint.
    /// This will probably change soon.
    /// </summary>
    public class ValidateInfo
    {
        /// <summary>
        /// The edge type to which this constraint applies.
        /// </summary>
        public readonly IType EdgeType;

        /// <summary>
        /// The node type to which applicable source nodes must be compatible.
        /// </summary>
        public readonly IType SourceType;

        /// <summary>
        /// The node type to which applicable target nodes must be compatible.
        /// </summary>
        public readonly IType TargetType;

        public readonly int SourceLower;
        public readonly int SourceUpper;
        public readonly int TargetLower;
        public readonly int TargetUpper;

        public ValidateInfo(IType edgeType, IType sourceType, IType targetType,
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
        ITypeModel NodeModel { get; }

        /// <summary>
        /// The model of the edges.
        /// </summary>
        ITypeModel EdgeModel { get; }

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
        public readonly IType OwnerType;

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
        public AttributeType(String name, IType ownerType, AttributeKind kind, EnumAttributeType enumType)
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
    /// A description of a GrGen element type.
    /// </summary>
    public interface IType
    {
        /// <summary>
        /// The name of the type.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// An identification number of the type, unique among all other types of the same kind (node/edge) in the owning type model.
        /// </summary>
        int TypeID { get; }

        /// <summary>
        /// True, if this type is a node type.
        /// </summary>
        bool IsNodeType { get; }

        /// <summary>
        /// Checks, whether this type is compatible to the given type, i.e. this type is the same type as the given type
        /// or it is a sub type of the given type.
        /// </summary>
        /// <param name="other">The type to be compared to.</param>
        /// <returns>True, if this type is compatible to the given type.</returns>
        bool IsA(IType other);

        /// <summary>
        /// Enumerates all super types of this type.
        /// </summary>
        IEnumerable<IType> SuperTypes { get; }

        /// <summary>
        /// Enumerates this type and all super types of this type (in this order).
        /// </summary>
        IEnumerable<IType> SuperOrSameTypes { get; }            // this is NOT supported in original libGr!

        /// <summary>
        /// Enumerates all sub types of this type.
        /// </summary>
        IEnumerable<IType> SubTypes { get; }                    // this is NOT supported in original libGr!

        /// <summary>
        /// Enumerates this type and all sub types of this type (in this order).
        /// </summary>
        IEnumerable<IType> SubOrSameTypes { get; }              // this is NOT supported in original libGr!

        /// <summary>
        /// True, if this type has any super types, i.e. if it is not the node/edge root type.
        /// </summary>
        bool HasSuperTypes { get; }

        /// <summary>
        /// True, if this type has any sub types.
        /// </summary>
        bool HasSubTypes { get; }
        
        /// <summary>
        /// The number of attributes of this type.
        /// </summary>
        int NumAttributes { get; }

        /// <summary>
        /// Enumerates all attribute types of this type.
        /// </summary>
        IEnumerable<AttributeType> AttributeTypes { get; }     // this is NOT supported in original libGr!

        /// <summary>
        /// Returns an AttributeType object for the given attribute name.
        /// If this type does not have an attribute with this name, null is returned.
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <returns>The AttributeType matching the name, or null if there is no such</returns>
        AttributeType GetAttributeType(String name);
    }

    /// <summary>
    /// A more concrete representation of a GrGen element type.
    /// </summary>
    public abstract class ITypeFramework : IType
    {
        /// <summary>
        /// An identification number of the type, unique among all other types of the same kind (node/edge) in the owning type model.
        /// </summary>
        public int typeID;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public ITypeFramework[] subOrSameTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public ITypeFramework[] superOrSameTypes;

        /// <summary>
        /// The name of the type.
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// An identification number of the type, unique among all other types of the same kind (node/edge) in the owning type model.
        /// </summary>
        public int TypeID { get { return typeID; } }

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public ITypeFramework[] SubOrSameTypes { get { return subOrSameTypes; } }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public ITypeFramework[] SuperOrSameTypes { get { return superOrSameTypes; } }

        /// <summary>
        /// Enumerates over all real subtypes of this type
        /// Warning: You should not use this property, but SubOrSameTypes starting from index 1,
        ///          because Enumerators in .NET are quite slow!
        /// </summary>
        public IEnumerable<ITypeFramework> SubTypes
        {
            get
            {
                for(int i = 1; i < subOrSameTypes.Length; i++)
                    yield return subOrSameTypes[i];
            }
        }

        /// <summary>
        /// Enumerates over all real supertypes of this type
        /// Warning: You should not use this property, but SuperOrSameTypes starting from index 1,
        ///          because Enumerators in .NET are quite slow!
        /// </summary>
        public IEnumerable<ITypeFramework> SuperTypes
        {
            get
            {
                for(int i = 1; i < superOrSameTypes.Length; i++)
                    yield return superOrSameTypes[i];
            }
        }

        /// <summary>
        /// True, if this type has any super types, i.e. if it is not the node/edge root type.
        /// </summary>
        public bool HasSuperTypes { get { return superOrSameTypes.Length > 1; } }

        /// <summary>
        /// True, if this type has any sub types.
        /// </summary>
        public bool HasSubTypes { get { return subOrSameTypes.Length > 1; } }

        /// <summary>
        /// True, if this type is a node type.
        /// </summary>
        public abstract bool IsNodeType { get; }

        /// <summary>
        /// Creates an IAttributes instance according to this type.
        /// If the the type does not have any attributes, null is returned.
        /// </summary>
        /// <returns>A new IAttributes instance or null, if the type does not have any attributes.</returns>
        public abstract IAttributes CreateAttributes();

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
        public abstract bool IsA(IType other);

        /// <summary>
        /// Checks, whether the first type is a super type of the second type or the types are the same.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if otherType is compatible to type.</returns>
        public static bool operator <=(ITypeFramework type, ITypeFramework otherType)
        {
            return otherType.IsA(type);
        }

        /// <summary>
        /// Checks, whether the first type is a sub type of the second type or the types are the same.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is compatible to otherType.</returns>
        public static bool operator >=(ITypeFramework type, ITypeFramework otherType)
        {
            return type.IsA(otherType);
        }

        /// <summary>
        /// Checks, whether the first type is a super type of the second type.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is a super type of otherType.</returns>
        public static bool operator <(ITypeFramework type, ITypeFramework otherType)
        {
            return type != otherType && otherType.IsA(type);
        }

        /// <summary>
        /// Checks, whether the first type is a sub type of the second type.
        /// </summary>
        /// <param name="type">The first type.</param>
        /// <param name="otherType">The second type.</param>
        /// <returns>True, if type is a sub type of otherType.</returns>
        public static bool operator >(ITypeFramework type, ITypeFramework otherType)
        {
            return type != otherType && type.IsA(otherType);
        }

        #region IType Member

        IEnumerable<IType> IType.SuperOrSameTypes
        {
            get { return superOrSameTypes; }
        }

        IEnumerable<IType> IType.SubOrSameTypes
        {
            get { return subOrSameTypes; }
        }

        IEnumerable<IType> IType.SuperTypes
        {
            get
            {
                for(int i = 1; i < superOrSameTypes.Length; i++)
                yield return superOrSameTypes[i];
            }
        }

        IEnumerable<IType> IType.SubTypes
        {
            get
            {
                for(int i = 1; i < subOrSameTypes.Length; i++)
                yield return subOrSameTypes[i];
            }
        }
        #endregion IType Member
    }

    /// <summary>
    /// A helper class for the type framework
    /// </summary>
    public abstract class TypeFramework<TypeClass, AttributeClass, AttributeInterface> : ITypeFramework
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
            return (this == other) || isA[((ITypeFramework) other).TypeID];          // first condition just for speed up
        }

/*        public override IAttributes CreateAttributes()                                  // grs newRule{79998} mountRule requestRule{80000}
        {
            return (IAttributes) Activator.CreateInstance(AttributesType);            // rewrite: 846 ms
//            return (IAttributes) Activator.CreateInstance<AttributeClass>();          // rewrite: 1733 ms
//            return new AttributeClass();                                                // rewrite: 1811 ms (without class constraint)
        }*/
    }

    /// <summary>
    /// A interface implemented by all GrGen attribute classes.
    /// </summary>
    public interface IAttributes : ICloneable
    {
    }
}