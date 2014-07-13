/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Diagnostics;
using System.IO;

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
        /// Returns the node type with the given type name or null, if no type with this name exists.
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
        /// Returns the edge type with the given type name or null, if no type with this name exists.
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
    /// The description of a single index, base for all kinds of index descriptions.
    /// (You must typecheck and cast to the concrete description type for more information).
    /// </summary>
    public abstract class IndexDescription
    {
        /// <summary>
        /// The name the index was declared with
        /// </summary>
        public readonly String Name;

        public IndexDescription(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// The description of a single attribute index.
    /// </summary>
    public class AttributeIndexDescription : IndexDescription
    {
        /// <summary>
        /// The node or edge type the index is defined for.
        /// (May be a subtype of the type the attribute was defined for first.)
        /// </summary>
        public readonly GrGenType GraphElementType;

        /// <summary>
        /// The attribute type the index is declared on.
        /// </summary>
        public readonly AttributeType AttributeType;

        public AttributeIndexDescription(string name,
            GrGenType graphElementType, AttributeType attributeType)
            : base(name)
        {
            GraphElementType = graphElementType;
            AttributeType = attributeType;
        }
    }

    public enum IncidenceDirection
    {
        OUTGOING,
        INCOMING,
        INCIDENT
    }

    /// <summary>
    /// The description of a single incidence index.
    /// </summary>
    public class IncidenceIndexDescription : IndexDescription
    {
        /// <summary>
        /// The direction of incidence followed.
        /// </summary>
        public readonly IncidenceDirection Direction;

        /// <summary>
        /// The type of the start node that is taken into account for the incidence count.
        /// </summary>
        public readonly NodeType StartNodeType;

        /// <summary>
        /// The type of the incident edge that is taken into account for the incidence count.
        /// </summary>
        public readonly EdgeType IncidentEdgeType;

        /// <summary>
        /// The type of the adjacent node that is taken into account for the incidence count.
        /// </summary>
        public readonly NodeType AdjacentNodeType;

        public IncidenceIndexDescription(string name, IncidenceDirection direction,
            NodeType startNodeType, EdgeType incidentEdgeType, NodeType adjacentNodeType)
            : base(name)
        {
            Direction = direction;
            StartNodeType = startNodeType;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
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
        /// Enumerates all packages declared in this model.
        /// </summary>
        IEnumerable<String> Packages { get; }

        /// <summary>
        /// Enumerates all enum attribute types declared for this model.
        /// </summary>
        IEnumerable<EnumAttributeType> EnumAttributeTypes { get; }

        /// <summary>
        /// Enumerates all ValidateInfo objects describing constraints on the graph structure.
        /// </summary>
        IEnumerable<ValidateInfo> ValidateInfo { get; }

        /// <summary>
        /// Enumerates the descriptions of all attribute and incidence count indices declared in this model.
        /// </summary>
        IEnumerable<IndexDescription> IndexDescriptions { get; }

        /// <summary>
        /// If true you may query the graph elements with GetUniqueId for their unique id
        /// </summary>
        bool GraphElementUniquenessIsEnsured { get; }

        /// <summary>
        /// If true you may query the graph with GetGraphElement for a graph element of a given unique id
        /// </summary>
        bool GraphElementsAreAccessibleByUniqueId { get; }

        /// <summary>
        /// Tells about the number of threads to use for the equalsAny and equalsAnyStructurally functions
        /// The normal non-parallel isomorphy comparison functions are used if this value is below 2
        /// </summary>
        int BranchingFactorForEqualsAny { get; }

        /// <summary>
        /// Called by the graph (generic implementation) to create and bind its index set (generated code).
        /// Always called by an empty graph just constructed.
        /// </summary>
        void CreateAndBindIndexSet(IGraph graph);

        /// <summary>
        /// Called on an index set that was created and bound,
        /// when the graph was copy constructed from an original graph,
        /// to fill in the already available cloned content from the original graph.
        /// </summary>
        void FillIndexSetAsClone(IGraph graph, IGraph originalGraph, IDictionary<IGraphElement, IGraphElement> oldToNewMap);


        #region Emitting and parsing of attributes of object or a user defined type
        
        /// <summary>
        /// Called during .grs import, at exactly the position in the text reader where the attribute begins.
        /// For attribute type object or a user defined type, which is treated as object.
        /// The implementation must parse from there on the attribute type requested.
        /// It must not parse beyond the serialized representation of the attribute, 
        /// i.e. Peek() must return the first character not belonging to the attribute type any more.
        /// Returns the parsed object.
        /// </summary>
        object Parse(TextReader reader, AttributeType attrType, IGraph graph);

        /// <summary>
        /// Called during .grs export, the implementation must return a string representation for the attribute.
        /// For attribute type object or a user defined type, which is treated as object.
        /// The serialized string must be parseable by Parse.
        /// </summary>
        string Serialize(object attribute, AttributeType attrType, IGraph graph);

        /// <summary>
        /// Called during debugging or emit writing, the implementation must return a string representation for the attribute.
        /// For attribute type object or a user defined type, which is treated as object.
        /// The attribute type may be null.
        /// The string is meant for consumption by humans, it does not need to be parseable.
        /// </summary>
        string Emit(object attribute, AttributeType attrType, IGraph graph);

        /// <summary>
        /// Called when the shell hits a line starting with "external".
        /// The content of that line is handed in.
        /// This is typically used while replaying changes containing a method call of an external type
        /// -- after such a line was recorded, by the method called, by writing to the recorder.
        /// This is meant to replay fine-grain changes of graph attributes of external type,
        /// in contrast to full assignments handled by Parse and Serialize.
        /// </summary>
        void External(string line, IGraph graph);

        /// <summary>
        /// Called during debugging on user request, the implementation must return a named graph representation for the attribute.
        /// For attribute type object or a user defined type, which is treated as object.
        /// The attribute type may be null. The return graph must be of the same model as the graph handed in.
        /// The named graph is meant for display in the debugger, to visualize the internal structure of some attribute type.
        /// This way you can graphically inspect your own data types which are opaque to GrGen with its debugger.
        /// </summary>
        INamedGraph AsGraph(object attribute, AttributeType attrType, IGraph graph);

        #endregion Emitting and parsing of attributes of object or a user defined type


        #region Comparison of attributes of object or user defined type, external types in general

        /// <summary>
        /// The external types known to this model, it contains always and at least the object type,
        /// the bottom type of the external attribute types hierarchy.
        /// </summary>
        ExternalType[] ExternalTypes { get; }

        /// <summary>
        /// Tells whether AttributeTypeObjectCopierComparer.IsEqual functions are available,
        /// for object and external types.
        /// </summary>
        bool IsEqualClassDefined { get; }

        /// <summary>
        /// Tells whether AttributeTypeObjectCopierComparer.IsLower functions are available,
        /// for object and external types.
        /// </summary>
        bool IsLowerClassDefined { get; }

        /// <summary>
        /// Calls the AttributeTypeObjectCopierComparer.IsEqual function for object type arguments,
        /// when an attribute of object or external type is compared for equality in the interpreted sequences;
        /// you may dispatch from there to the type exact comparisons, which are called directly from the compiled sequences.
        /// </summary>
        bool IsEqual(object this_, object that);

        /// <summary>
        /// Calls the AttributeTypeObjectCopierComparer.IsLower function for object type arguments,
        /// when an attribute of object or external type is compared for ordering in the interpreted sequences;
        /// you may dispatch from there to the type exact comparisons, which are called directly from the compiled sequences.
        /// </summary>
        bool IsLower(object this_, object that);

        #endregion Comparison of attributes of object or user defined type, external types in general


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

        /// <summary>The attribute is an object (this includes external attribute types).</summary>
        ObjectAttr,

        /// <summary>The attribute is a map.</summary>
        MapAttr,

        /// <summary>The attribute is a set.</summary>
        SetAttr,

        /// <summary>The attribute is an array.</summary>
        ArrayAttr,

        /// <summary>The attribute is a deque.</summary>
        DequeAttr,

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
        /// <param name="valueType">The attribute type of the value of the set, if Kind == AttributeKind.SetAttr; the attribute type of the value of the map, if Kind == AttributeKind.MapAttr; the attribute type of the value of the array, if Kind == AttributeKind.ArrayAttr; the attribute type of the value of the deque, if Kind == AttributeKind.DequeAttr; otherwise null. </param>
        /// <param name="keyType">The attribute type of the key of the map, if Kind == AttributeKind.MapAttr; otherwise null.</param>
        /// <param name="typeName">The name of the attribute type, if Kind == AttributeKind.NodeAttr || Kind == AttributeKind.EdgeAttr; otherwise null.</param>
        /// <param name="package">The package name if this is a node or edge type that is contained in a package, otherwise null.</param>
        /// <param name="packagePrefixedTypeName">The name of the attribute type with the package as prefix if it is contained in a package, if Kind == AttributeKind.NodeAttr || Kind == AttributeKind.EdgeAttr.</param>
        /// <param name="type">The type of the attribute type.</param>
        public AttributeType(String name, GrGenType ownerType, AttributeKind kind,
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
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        public readonly String Package;

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        public readonly String PackagePrefixedName;

        /// <summary>
        /// The .NET type for the enum type.
        /// </summary>
        public readonly Type EnumType;

        private EnumMember[] members;

        /// <summary>
        /// Initializes an EnumAttributeType instance.
        /// </summary>
        /// <param name="name">The name of the enum type.</param>
        /// <param name="name">The package the enum is contained in, or null if it is not contained in a package.</param>
        /// <param name="name">The name of the enum type; prefixed by the package name plus a double colon, in case it is contain in a package.</param>
        /// <param name="enumType">The .NET type for the enum type.</param>
        /// <param name="memberArray">An array of all enum members.</param>
        public EnumAttributeType(String name, String package, String packagePrefixedName, Type enumType, EnumMember[] memberArray)
        {
            Name = name;
            Package = package;
            PackagePrefixedName = packagePrefixedName;
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
        /// Tells whether the given type is the same or a subtype of this type
        /// </summary>
        public abstract bool IsMyType(int typeID);

        /// <summary>
        /// Tells whether this type is the same or a subtype of the given type
        /// </summary>
        public abstract bool IsA(int typeID);

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
        /// Creates an IEdge object according to this type.
        /// ATTENTION: You must call SetSourceAndTarget() before adding an edge created this way to a graph.
        /// This is an unsafe function that allows to first set the attributes of an edge, as needed in efficient .grs importing.
        /// </summary>
        /// <returns>The created IEdge object.</returns>
        public IEdge CreateEdge()
        {
            return CreateEdge(null, null);
        }

        /// <summary>
        /// Sets the source and target nodes of the edge after creation without.
        /// Must be called before an edge created with CreateEdge() is added to the graph.
        /// </summary>
        /// <param name="edge">The edge to set the source and target for.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        public abstract void SetSourceAndTarget(IEdge edge, INode source, INode target);

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
        /// Tells whether the given type is the same or a subtype of this type
        /// </summary>
        public abstract bool IsMyType(int typeID);

        /// <summary>
        /// Tells whether this type is the same or a subtype of the given type
        /// </summary>
        public abstract bool IsA(int typeID);

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

        public override int NumFunctionMethods
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override IEnumerable<IFunctionDefinition> FunctionMethods
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override IFunctionDefinition GetFunctionMethod(String name)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int NumProcedureMethods
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override IEnumerable<IProcedureDefinition> ProcedureMethods
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override IProcedureDefinition GetProcedureMethod(String name)
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

    /// <summary>
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
