/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libGr;
using System.Diagnostics;
using de.unika.ipd.grGen.expression;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An element of a rule pattern.
    /// </summary>
    public abstract class PatternElement : IPatternElement
    {
        /// <summary>
        /// The name of the pattern element.
        /// </summary>
        public String Name { get { return name; } }

        /// <summary>
        /// The pure name of the pattern element as specified in the .grg without any prefixes.
        /// </summary>
        public String UnprefixedName { get { return unprefixedName; } }

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public IPatternGraph PointOfDefinition { get { return pointOfDefinition; } }

        /// <summary>
        /// Iff true the element is only defined in its PointOfDefinition pattern,
        /// it gets matched in another, nested or called pattern which yields it to the containing pattern.
        /// </summary>
        public bool DefToBeYieldedTo { get { return defToBeYieldedTo; } }

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }

        /// <summary>
        /// The GrGen type of the pattern element, fake implementation overriden in subclasses
        /// </summary>
        GrGenType IPatternElement.Type { get { throw new NotImplementedException(); } }
        
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The type ID of the pattern element.
        /// </summary>
        public int TypeID;

        /// <summary>
        /// The name of the type interface of the pattern element.
        /// </summary>
        public String typeName;

        /// <summary>
        /// The name of the pattern element.
        /// </summary>
        public String name;

        /// <summary>
        /// Pure name of the pattern element as specified in the .grg file without any prefixes.
        /// </summary>
        public String unprefixedName;

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public PatternGraph pointOfDefinition;

        /// <summary>
        /// Iff true the element is only defined in its PointOfDefinition pattern,
        /// it gets matched in another, nested or called pattern which yields it to the containing pattern.
        /// </summary>
        public bool defToBeYieldedTo;

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        public IDictionary<string, string> annotations = new Dictionary<string, string>();

        /// <summary>
        /// An array of allowed types for this pattern element.
        /// If it is null, all subtypes of the type specified by typeID (including itself)
        /// are allowed for this pattern element.
        /// </summary>
        public GrGenType[] AllowedTypes;

        /// <summary>
        /// An array containing a bool for each node/edge type (order defined by the TypeIDs)
        /// which is true iff the corresponding type is allowed for this pattern element.
        /// It should be null if allowedTypes is null or empty or has only one element.
        /// </summary>
        public bool[] IsAllowedType;

        /// <summary>
        /// Default cost/priority from frontend, user priority if given.
        /// </summary>
        public float Cost;

        /// <summary>
        /// Specifies to which rule parameter this pattern element corresponds.
        /// Only valid if pattern element is handed in as rule parameter.
        /// </summary>
        public int ParameterIndex;

        /// <summary>
        /// Tells whether this pattern element may be null.
        /// May only be true if pattern element is handed in as rule parameter.
        /// </summary>
        public bool MaybeNull;

        /// <summary>
        /// If not null this pattern element is to be bound by iterating the given storage
        /// (which may mean trying the single value if it is elementary).
        /// </summary>
        public StorageAccess Storage;

        /// <summary>
        /// If not null this pattern element is to be determined by a storage indexed lookup,
        /// with the accessor given here applied as index into the storage given in the Storage field.
        /// </summary>
        public StorageAccessIndex StorageIndex;

        /// <summary>
        /// If not null this pattern element is to be bound by casting the given ElementBeforeCasting to the pattern element type or causing matching to fail.
        /// </summary>
        public PatternElement ElementBeforeCasting;

        /// <summary>
        /// If not null this pattern element is to be bound by assigning the given assignmentSource to the pattern element.
        /// This is needed to fill the pattern parameters of a pattern embedding which was inlined.
        /// </summary>
        public PatternElement AssignmentSource;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Links to the original pattern element in case this element was inlined, otherwise null;
        /// the point of definition of the original element references the original containing pattern
        /// </summary>
        public PatternElement originalElement;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this element was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// plan graph node corresponding to this pattern element, used in plan graph generation, just hacked into this place
        /// </summary>
        public PlanNode TempPlanMapping;

        /// <summary>
        /// visited flag used to compute pattern connectedness for inlining, just hacked into this place
        /// </summary>
        public bool visited;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Instantiates a new PatternElement object.
        /// </summary>
        /// <param name="typeID">The type ID of the pattern element.</param>
        /// <param name="typeName">The name of the type interface of the pattern element.</param>
        /// <param name="name">The name of the pattern element.</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node/edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="cost">Default cost/priority from frontend, user priority if given.</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds.</param>
        /// <param name="maybeNull">Tells whether this pattern element may be null (is a parameter if true).</param>
        /// <param name="storage">If not null this pattern element is to be bound by iterating the given storage.</param>
        /// <param name="storageIndex">If not null this pattern element is to be determined by a storage lookup,
        ///     with the accessor given here applied as index into the storage given in the storage parameter.</param>
        /// <param name="elementBeforeCasting">If not null this pattern node is to be bound by casting the given elementBeforeCasting to the pattern node type or causing matching to fail.</param>
        /// <param name="defToBeYieldedTo">Iff true the element is only defined in its PointOfDefinition pattern,
        ///     it gets matched in another, nested or called pattern which yields it to the containing pattern.</param>
        public PatternElement(int typeID, String typeName, 
            String name, String unprefixedName, 
            GrGenType[] allowedTypes, bool[] isAllowedType, 
            float cost, int parameterIndex, bool maybeNull,
            StorageAccess storage, StorageAccessIndex storageIndex,
            PatternElement elementBeforeCasting, bool defToBeYieldedTo)
        {
            this.TypeID = typeID;
            this.typeName = typeName;
            this.name = name;
            this.unprefixedName = unprefixedName;
            this.AllowedTypes = allowedTypes;
            this.IsAllowedType = isAllowedType;
            this.Cost = cost;
            this.ParameterIndex = parameterIndex;
            this.MaybeNull = maybeNull;
            this.Storage = storage;
            this.StorageIndex = storageIndex;
            this.ElementBeforeCasting = elementBeforeCasting;
            this.defToBeYieldedTo = defToBeYieldedTo;
            // TODO: the last parameters are (mostly) mutually exclusive, 
            // introduce some abstract details class with specialized classed for the different cases,
            // only one instance needed instead of the large amount of mostly null valued variables now
            // better now with the introduction of helper classes for StorageAccess and StorageAccessIndex, but could be improved further
        }

        /// <summary>
        /// Instantiates a new PatternElement object as a copy from an original element, used for inlining.
        /// </summary>
        /// <param name="original">The original pattern element to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new pattern element will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the pattern element (to avoid name collisions).</param>
        public PatternElement(PatternElement original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix)
        {
            TypeID = original.TypeID;
            typeName = original.typeName;
            name = original.name + nameSuffix;
            unprefixedName = original.unprefixedName + nameSuffix;
            pointOfDefinition = newHost;
            defToBeYieldedTo = original.defToBeYieldedTo;
            annotations = original.annotations;
            AllowedTypes = original.AllowedTypes;
            IsAllowedType = original.IsAllowedType;
            Cost = original.Cost;
            ParameterIndex = original.ParameterIndex;
            MaybeNull = original.MaybeNull;
            Storage = original.Storage != null ? new StorageAccess(original.Storage) : null;
            StorageIndex = original.StorageIndex != null ? new StorageAccessIndex(original.StorageIndex) : null;
            ElementBeforeCasting = original.ElementBeforeCasting;
            AssignmentSource = original.AssignmentSource;
            originalElement = original;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding;
        }

        /// <summary>
        /// Converts this instance into a string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            return Name + ":" + TypeID;
        }

        /// <summary>
        /// Returns the pattern element the storage (which must be not null) depends on (or the storage index depends on)
        /// </summary>
        public PatternElement GetPatternElementTheStorageDependsOn()
        {
            // there is at most one pattern element the storage depends on, that is ensured by the frontend
            // it needs to be ensured because the scheduler can only cope with one dependency, better: the search plan graph can only model that

            if(Storage.Attribute != null)
                return Storage.Attribute.Owner; // need the graph element owning the attribute
            if(StorageIndex != null)
            {
                if(StorageIndex.Attribute != null)
                    return StorageIndex.Attribute.Owner; // need the graph element owning the attribute
                if(StorageIndex.GraphElement != null)
                    return StorageIndex.GraphElement; // need the graph element
            }

            return null; // only local variables or global variables required
        }
    }

    /// <summary>
    /// A pattern node of a rule pattern.
    /// </summary>
    public class PatternNode : PatternElement, IPatternNode
    {
        /// <summary>
        /// The GrGen type of the pattern node
        /// </summary>
        public NodeType type;

        /// <summary>
        /// Instantiates a new PatternNode object
        /// </summary>
        /// <param name="typeID">The type ID of the pattern node</param>
        /// <param name="type">The GrGen type of the pattern node.</param>
        /// <param name="typeName">The name of the type interface of the pattern element.</param>
        /// <param name="name">The name of the pattern node</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node/edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="cost"> default cost/priority from frontend, user priority if given</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        /// <param name="maybeNull">Tells whether this pattern node may be null (is a parameter if true).</param>
        /// <param name="storage">If not null this node is to be bound by iterating the given storage.</param>
        /// <param name="storageIndex">If not null this node is to be determined by a storage lookup,
        ///     with the accessor given here applied as index into the storage given in the storage parameter.</param>
        /// <param name="elementBeforeCasting">If not null this pattern node is to be bound by casting the given elementBeforeCasting to the pattern node type or causing matching to fail.</param>
        /// <param name="defToBeYieldedTo">Iff true the element is only defined in its PointOfDefinition pattern,
        ///     it gets matched in another, nested or called pattern which yields it to the containing pattern.</param>
        public PatternNode(int typeID, NodeType type, String typeName,
            String name, String unprefixedName,
            GrGenType[] allowedTypes, bool[] isAllowedType, 
            float cost, int parameterIndex, bool maybeNull,
            StorageAccess storage, StorageAccessIndex storageIndex,
            PatternElement elementBeforeCasting, bool defToBeYieldedTo)
            : base(typeID, typeName, name, unprefixedName, allowedTypes, isAllowedType, 
                cost, parameterIndex, maybeNull, storage, storageIndex,
                elementBeforeCasting, defToBeYieldedTo)
        {
            this.type = type;
        }

        /// <summary>
        /// Instantiates a new PatternNode object as a copy from an original node, used for inlining.
        /// </summary>
        /// <param name="original">The original pattern node to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new pattern node will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the pattern node (to avoid name collisions).</param>
        public PatternNode(PatternNode original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix)
            : base(original, inlinedSubpatternEmbedding, newHost, nameSuffix)
        {
        }

        /// <summary>
        /// The GrGen type of the pattern node.
        /// </summary>
        public NodeType Type { get { return type; } }
        GrGenType IPatternElement.Type { get { return type; } }

        /// <summary>
        /// Converts this instance into a string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            return Name + ":" + TypeID;
        }

        /// <summary>
        /// Links to the original pattern node in case this node was inlined, otherwise null;
        /// the point of definition of the original node references the original containing pattern
        /// </summary>
        public PatternNode originalNode { get { return (PatternNode)originalElement; } }
    }

    /// <summary>
    /// A pattern edge of a rule pattern.
    /// </summary>
    public class PatternEdge : PatternElement, IPatternEdge
    {
        /// <summary>
        /// The GrGen type of the pattern edge
        /// </summary>
        public EdgeType type;
        
        /// <summary>
        /// Indicates, whether this pattern edge should be matched with a fixed direction or not.
        /// </summary>
        public bool fixedDirection;

        /// <summary>
        /// Instantiates a new PatternEdge object
        /// </summary>
        /// <param name="fixedDirection">Whether this pattern edge should be matched with a fixed direction or not.</param>
        /// <param name="typeID">The type ID of the pattern edge.</param>
        /// <param name="type">The GrGen type of the pattern edge.</param>
        /// <param name="typeName">The name of the type interface of the pattern element.</param>
        /// <param name="name">The name of the pattern edge.</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="cost"> default cost/priority from frontend, user priority if given</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        /// <param name="maybeNull">Tells whether this pattern edge may be null (is a parameter if true).</param>
        /// <param name="storage">If not null this edge is to be bound by iterating the given storage.</param>
        /// <param name="storageIndex">If not null this edge is to be determined by a storage lookup,
        ///     with the accessor given here applied as index into the storage given in the storage parameter.</param>
        /// <param name="elementBeforeCasting">If not null this pattern node is to be bound by casting the given elementBeforeCasting to the pattern node type or causing matching to fail.</param>
        /// <param name="defToBeYieldedTo">Iff true the element is only defined in its PointOfDefinition pattern,
        ///     it gets matched in another, nested or called pattern which yields it to the containing pattern.</param>
        public PatternEdge(bool fixedDirection,
            int typeID, EdgeType type, String typeName, 
            String name, String unprefixedName,
            GrGenType[] allowedTypes, bool[] isAllowedType,
            float cost, int parameterIndex, bool maybeNull,
            StorageAccess storage, StorageAccessIndex storageIndex,
            PatternElement elementBeforeCasting, bool defToBeYieldedTo)
            : base(typeID, typeName, name, unprefixedName, allowedTypes, isAllowedType,
                cost, parameterIndex, maybeNull, storage, storageIndex,
                elementBeforeCasting, defToBeYieldedTo)
        {
            this.fixedDirection = fixedDirection;
            this.type = type;
        }

        /// <summary>
        /// Instantiates a new PatternEdge object as a copy from an original edge, used for inlining.
        /// </summary>
        /// <param name="original">The original pattern edge to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new pattern element will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the pattern edge (to avoid name collisions).</param>
        public PatternEdge(PatternEdge original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix)
            : base(original, inlinedSubpatternEmbedding, newHost, nameSuffix)
        {
            fixedDirection = original.fixedDirection;
        }

        /// <summary>
        /// The GrGen type of the pattern edge.
        /// </summary>
        public EdgeType Type { get { return type; } }
        GrGenType IPatternElement.Type { get { return type; } }

        /// <summary>
        /// Converts this instance into a string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            if(fixedDirection)
                return "-" + Name + ":" + TypeID + "->";
            else
                return "<-" + Name + ":" + TypeID + "->";
        }

        /// <summary>
        /// Links to the original pattern edge in case this node was inlined, otherwise null;
        /// the point of definition of the original edge references the original containing pattern
        /// </summary>
        public PatternEdge originalEdge { get { return (PatternEdge)originalElement; } }
    }

    /// <summary>
    /// A pattern variable of a rule pattern.
    /// </summary>
    public class PatternVariable : IPatternVariable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public String Name { get { return name; } }

        /// <summary>
        /// The pure name of the pattern element as specified in the .grg without any prefixes.
        /// </summary>
        public String UnprefixedName { get { return unprefixedName; } }

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public IPatternGraph PointOfDefinition { get { return pointOfDefinition; } }

        /// <summary>
        /// Iff true the element is only defined in its PointOfDefinition pattern,
        /// it gets matched in another, nested or called pattern which yields it to the containing pattern.
        /// </summary>
        public bool DefToBeYieldedTo { get { return defToBeYieldedTo; } }

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }

        /// <summary>
        /// The GrGen type of the pattern variable.
        /// </summary>
        public VarType Type { get { return type; } }
        GrGenType IPatternElement.Type { get { return type; } }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The GrGen type of the pattern variable.
        /// </summary>
        public VarType type;

        /// <summary>
        /// The name of the variable.
        /// </summary>
        public String name;
        
        /// <summary>
        /// Pure name of the variable as specified in the .grg without any prefixes.
        /// </summary>
        public String unprefixedName;

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public PatternGraph pointOfDefinition;

        /// <summary>
        /// Iff true the element is only defined in its PointOfDefinition pattern,
        /// it gets matched in another, nested or called pattern which yields it to the containing pattern.
        /// </summary>
        public bool defToBeYieldedTo;

        /// <summary>
        /// The initialization expression for the variable if some was defined, otherwise null.
        /// </summary>
        public Expression initialization;

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        public IDictionary<string, string> annotations = new Dictionary<string, string>();

        /// <summary>
        /// Specifies to which rule parameter this variable corresponds.
        /// </summary>
        public int ParameterIndex;

        /// <summary>
        /// If not null this pattern element is to be bound by assigning the value of the given assignmentSource expression to the variable.
        /// This is needed to fill the pattern parameters of a pattern embedding which was inlined.
        /// </summary>
        public Expression AssignmentSource;

        /// <summary>
        /// If AssignmentSource is not null this gives the original embedding which was inlined.
        /// It is given as quick access to the needed nodes, edges, and variables for scheduling.
        /// </summary>
        public PatternGraphEmbedding AssignmentDependencies;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Links to the original pattern variable in case this variable was inlined, otherwise null;
        /// the point of definition of the original variable references the original containing pattern
        /// </summary>
        public PatternVariable originalVariable;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this variable was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Instantiates a new PatternVariable object.
        /// </summary>
        /// <param name="type">The GrGen type of the variable.</param>
        /// <param name="name">The name of the variable.</param>
        /// <param name="unprefixedName">Pure name of the variable as specified in the .grg without any prefixes.</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this variable corresponds.</param>
        /// <param name="defToBeYieldedTo">Iff true the element is only defined in its PointOfDefinition pattern,
        ///     it gets matched in another, nested or called pattern which yields it to the containing pattern.</param>
        /// <param name="initialization">The initialization expression for the variable if some was defined, otherwise null.</param>
        public PatternVariable(VarType type, String name, String unprefixedName,
            int parameterIndex, bool defToBeYieldedTo, Expression initialization)
        {
            this.type = type;
            this.name = name;
            this.unprefixedName = unprefixedName;
            this.ParameterIndex = parameterIndex;
            this.defToBeYieldedTo = defToBeYieldedTo;
            this.initialization = initialization;
        }

        /// <summary>
        /// Instantiates a new PatternVariable object as a copy from an original variable, used for inlining.
        /// </summary>
        /// <param name="original">The original pattern variable to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new pattern element will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the pattern variable (to avoid name collisions).</param>
        public PatternVariable(PatternVariable original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix)
        {
            type = original.type;
            name = original.name + nameSuffix;
            unprefixedName = original.unprefixedName + nameSuffix;
            pointOfDefinition = newHost;
            defToBeYieldedTo = original.defToBeYieldedTo;
            initialization = original.initialization;
            annotations = original.annotations;
            ParameterIndex = original.ParameterIndex;
            originalVariable = original;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding;
        }
    }

    /// <summary>
    /// Representation of a storage access, used to bind a pattern element.
    /// </summary>
    public class StorageAccess
    {
        /// <summary>
        /// The storage is a pattern variable if not null.
        /// </summary>
        public PatternVariable Variable;

        /// <summary>
        /// The storage is a global variable if not null.
        /// </summary>
        public GlobalVariableAccess GlobalVariable;

        /// <summary>
        /// The storage is a graph element attribute (qualification) if not null.
        /// </summary>
        public QualificationAccess Attribute;


        public StorageAccess(PatternVariable variable)
        {
            Variable = variable;
        }

        public StorageAccess(GlobalVariableAccess globalVariable)
        {
            GlobalVariable = globalVariable;
        }

        public StorageAccess(QualificationAccess attribute)
        {
            Attribute = attribute;
        }

        // Instantiates a new StorageAccess object as a copy from an original storage access, used for inlining.
        public StorageAccess(StorageAccess original)
        {
            Variable = original.Variable;
            GlobalVariable = original.GlobalVariable;
            Attribute = original.Attribute;
        }

        public override string ToString()
        {
            if(Variable != null)
                return Variable.Name;
            else if(GlobalVariable != null)
                return GlobalVariable.ToString();
            else if(Attribute != null)
                return Attribute.ToString();
            else
                return "null";
        }

        public void PatchUsersOfCopiedElements(
            Dictionary<PatternNode, PatternNode> nodeToCopy,
            Dictionary<PatternEdge, PatternEdge> edgeToCopy,
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            if(Variable != null)
            {
                if(variableToCopy.ContainsKey(Variable))
                    Variable = variableToCopy[Variable];
            }
            if(Attribute != null)
            {
                if(Attribute.Owner is PatternNode)
                {
                    if(nodeToCopy.ContainsKey((PatternNode)Attribute.Owner))
                        Attribute.Owner = nodeToCopy[(PatternNode)Attribute.Owner];
                }
                else
                {
                    if(edgeToCopy.ContainsKey((PatternEdge)Attribute.Owner))
                        Attribute.Owner = edgeToCopy[(PatternEdge)Attribute.Owner];
                }
            }
        }
    }

    /// <summary>
    /// Representation of a storage access index, used to bind a pattern element.
    /// </summary>
    public class StorageAccessIndex
    {
        /// <summary>
        /// The storage index is the given graph element if not null.
        /// </summary>
        public PatternElement GraphElement;

        /// <summary>
        /// The storage index is the given pattern variable if not null.
        /// </summary>
        public PatternVariable Variable;

        /// <summary>
        /// The storage index is the given global variable if not null.
        /// </summary>
        public GlobalVariableAccess GlobalVariable;

        /// <summary>
        /// The storage index is the given graph element attribute (qualification) if not null.
        /// </summary>
        public QualificationAccess Attribute;


        public StorageAccessIndex(PatternElement graphElement)
        {
            GraphElement = graphElement;
        }

        public StorageAccessIndex(PatternVariable variable)
        {
            Variable = variable;
        }

        public StorageAccessIndex(GlobalVariableAccess globalVariable)
        {
            GlobalVariable = globalVariable;
        }

        public StorageAccessIndex(QualificationAccess attribute)
        {
            Attribute = attribute;
        }

        // Instantiates a new StorageAccessIndex object as a copy from an original storage access index, used for inlining.
        public StorageAccessIndex(StorageAccessIndex original)
        {
            GraphElement = original.GraphElement;
            Variable = original.Variable;
            GlobalVariable = original.GlobalVariable;
            Attribute = original.Attribute;
        }

        public override string ToString()
        {
            if(GraphElement!=null)
                return GraphElement.ToString();
            if(Variable != null)
                return Variable.Name;
            else if(GlobalVariable != null)
                return GlobalVariable.ToString();
            else if(Attribute != null)
                return Attribute.ToString();
            else
                return "null";
        }


        public void PatchUsersOfCopiedElements(
            Dictionary<PatternNode, PatternNode> nodeToCopy,
            Dictionary<PatternEdge, PatternEdge> edgeToCopy,
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            if(GraphElement != null)
            {
                if(GraphElement is PatternNode)
                {
                    if(nodeToCopy.ContainsKey((PatternNode)GraphElement))
                        GraphElement = nodeToCopy[(PatternNode)GraphElement];
                }
                else
                {
                    if(edgeToCopy.ContainsKey((PatternEdge)GraphElement))
                        GraphElement = edgeToCopy[(PatternEdge)GraphElement];
                }
            }
            if(Variable != null)
            {
                if(variableToCopy.ContainsKey(Variable))
                    Variable = variableToCopy[Variable];
            }
            if(Attribute != null)
            {
                if(Attribute.Owner is PatternNode)
                {
                    if(nodeToCopy.ContainsKey((PatternNode)Attribute.Owner))
                        Attribute.Owner = nodeToCopy[(PatternNode)Attribute.Owner];
                }
                else
                {
                    if(edgeToCopy.ContainsKey((PatternEdge)Attribute.Owner))
                        Attribute.Owner = edgeToCopy[(PatternEdge)Attribute.Owner];
                }
            }
        }
    }

    /// <summary>
    /// Representation of an owner.attribute qualification
    /// </summary>
    public class QualificationAccess
    {
        /// <summary>
        /// The graph element owning the attribute.
        /// </summary>
        public PatternElement Owner;

        /// <summary>
        /// The attribute.
        /// </summary>
        public AttributeType Attribute;

        public QualificationAccess(PatternElement owner, AttributeType attribute)
        {
            Owner = owner;
            Attribute = attribute;
        }

        public override string ToString()
        {
            return Owner.Name + "." + Attribute.Name;
        }
    }

    /// <summary>
    /// Representation of a global variable accessed from within a pattern (match from storage constructs)
    /// </summary>
    public class GlobalVariableAccess
    {
        /// <summary>
        /// Name of the global variable
        /// </summary>
        public string Name;

        /// <summary>
        /// Statically declared type of the global variable.
        /// (The one given in the rule file declaration defining how it is to be interpreted,
        /// global variables as such are untyped.)
        /// </summary>
        public VarType Type;

        /// <summary>
        /// Instantiates a new global variable access to be used in a match from storage construct.
        /// </summary>
        /// <param name="name">The name of the global variable.</param>
        /// <param name="type">The type the values in the global variable must bear, as declared in the rules file.</param>
        public GlobalVariableAccess(string name, VarType type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return "::" + Name;
        }
    }

    /// <summary>
    /// Representation of some condition which must be true for the pattern containing it to be matched
    /// </summary>
    public class PatternCondition
    {
        /// <summary>
        /// The condition expression to evaluate
        /// </summary>
        public Expression ConditionExpression;

        /// <summary>
        /// An array of node names needed by this condition.
        /// </summary>
        public String[] NeededNodes;

        /// <summary>
        /// An array of edge names needed by this condition.
        /// </summary>
        public String[] NeededEdges;

        /// <summary>
        /// An array of variable names needed by this condition.
        /// </summary>
        public String[] NeededVariables;

        /// <summary>
        /// An array of variable types (corresponding to the variable names) needed by this condition.
        /// </summary>
        public VarType[] NeededVariableTypes;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Links to the original pattern condition in case this condition was inlined, otherwise null
        /// </summary>
        public PatternCondition originalCondition;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this condition was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructs a PatternCondition object.
        /// </summary>
        /// <param name="conditionExpression">The condition expression to evaluate.</param>
        /// <param name="neededNodes">An array of node names needed by this condition.</param>
        /// <param name="neededEdges">An array of edge names needed by this condition.</param>
        /// <param name="neededVariables">An array of variable names needed by this condition.</param>
        /// <param name="neededVariableTypes">An array of variable types (corresponding to the variable names) needed by this condition.</param>
        public PatternCondition(Expression conditionExpression, 
            String[] neededNodes, String[] neededEdges, String[] neededVariables, VarType[] neededVariableTypes)
        {
            ConditionExpression = conditionExpression;
            NeededNodes = neededNodes;
            NeededEdges = neededEdges;
            NeededVariables = neededVariables;
            NeededVariableTypes = neededVariableTypes;
        }

        /// <summary>
        /// Instantiates a new PatternCondition object as a copy from an original condition, used for inlining.
        /// </summary>
        /// <param name="original">The original condition to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="renameSuffix">The rename suffix to be applied to all the nodes, edges, and variables used.</param>
        public PatternCondition(PatternCondition original, PatternGraphEmbedding inlinedSubpatternEmbedding, string renameSuffix)
        {
            originalCondition = original;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding;
            ConditionExpression = (Expression)original.ConditionExpression.Copy(renameSuffix);
            NeededNodes = new String[original.NeededNodes.Length];
            for(int i = 0; i < original.NeededNodes.Length; ++i)
                NeededNodes[i] = original.NeededNodes[i] + renameSuffix;
            NeededEdges = new String[original.NeededEdges.Length];
            for(int i = 0; i < original.NeededEdges.Length; ++i)
                NeededEdges[i] = original.NeededEdges[i] + renameSuffix;
            NeededVariables = new String[original.NeededVariables.Length];
            for(int i = 0; i < original.NeededVariables.Length; ++i)
                NeededVariables[i] = original.NeededVariables[i] + renameSuffix;
            NeededVariableTypes = (VarType[])original.NeededVariableTypes.Clone();
        }
    }

    /// <summary>
    /// Representation of some yielding (a list of elementary yieldings, to be executed after matching completed)
    /// </summary>
    public class PatternYielding
    {
        /// <summary>
        /// The name of the pattern yielding.
        /// </summary>
        public String Name;
        
        /// <summary>
        /// An array of elementary yieldings to execute (e.g. assignments to def variables).
        /// </summary>
        public Yielding[] ElementaryYieldings;

        /// <summary>
        /// An array of node names needed by this yielding.
        /// </summary>
        public String[] NeededNodes;

        /// <summary>
        /// An array of edge names needed by this yielding.
        /// </summary>
        public String[] NeededEdges;

        /// <summary>
        /// An array of variable names needed by this yielding.
        /// </summary>
        public String[] NeededVariables;

        /// <summary>
        /// An array of variable types (corresponding to the variable names) needed by this yielding.
        /// </summary>
        public VarType[] NeededVariableTypes;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Links to the original pattern yielding in case this yielding was inlined, otherwise null
        /// </summary>
        public PatternYielding originalYielding;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this yielding was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructs a PatternYielding object.
        /// </summary>
        /// <param name="name">The name of the yielding to execute.</param>
        /// <param name="elementaryYieldings">An array of elementary yieldings to execute.</param>
        /// <param name="neededNodes">An array of node names needed by this yielding.</param>
        /// <param name="neededEdges">An array of edge names needed by this yielding.</param>
        /// <param name="neededVariables">An array of variable names needed by this yielding.</param>
        /// <param name="neededVariableTypes">An array of variable types (corresponding to the variable names) needed by this yielding.</param>
        public PatternYielding(String name, Yielding[] elementaryYieldings,
            String[] neededNodes, String[] neededEdges, String[] neededVariables, VarType[] neededVariableTypes)
        {
            Name = name;
            ElementaryYieldings = elementaryYieldings;
            NeededNodes = neededNodes;
            NeededEdges = neededEdges;
            NeededVariables = neededVariables;
            NeededVariableTypes = neededVariableTypes;
        }

        /// <summary>
        /// Instantiates a new PatternYielding object as a copy from an original yielding, used for inlining.
        /// </summary>
        /// <param name="original">The original yielding to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="renameSuffix">The rename suffix to be applied to all the nodes, edges, and variables used.</param>
        public PatternYielding(PatternYielding original, PatternGraphEmbedding inlinedSubpatternEmbedding, string renameSuffix)
        {
            originalYielding = original;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding;
            Name = original.Name + renameSuffix;
            ElementaryYieldings = new Yielding[original.ElementaryYieldings.Length];
            for(int i = 0; i < original.ElementaryYieldings.Length; ++i)
                ElementaryYieldings[i] = original.ElementaryYieldings[i].Copy(renameSuffix);
            NeededNodes = new String[original.NeededNodes.Length];
            for(int i = 0; i < original.NeededNodes.Length; ++i)
                NeededNodes[i] = original.NeededNodes[i] + renameSuffix;
            NeededEdges = new String[original.NeededEdges.Length];
            for(int i = 0; i < original.NeededEdges.Length; ++i)
                NeededEdges[i] = original.NeededEdges[i] + renameSuffix;
            NeededVariables = new String[original.NeededVariables.Length];
            for(int i = 0; i < original.NeededVariables.Length; ++i)
                NeededVariables[i] = original.NeededVariables[i] + renameSuffix;
            NeededVariableTypes = (VarType[])original.NeededVariableTypes.Clone();
        }
    }


    /// <summary>
    /// Embedding of a subpattern into it's containing pattern
    /// </summary>
    public class PatternGraphEmbedding : IPatternGraphEmbedding
    {
        /// <summary>
        /// The name of the usage of the subpattern.
        /// </summary>
        public String Name { get { return name; } }

        /// <summary>
        /// The embedded subpattern.
        /// </summary>
        public IPatternGraph EmbeddedGraph { get { return matchingPatternOfEmbeddedGraph.patternGraph; } }

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }

        /// <summary>
        /// The pattern where this complex subpattern element gets matched.
        /// </summary>
        public PatternGraph PointOfDefinition;

        /// <summary>
        /// The name of the usage of the subpattern.
        /// </summary>
        public String name;

        /// <summary>
        /// The embedded subpattern.
        /// </summary>
        public LGSPMatchingPattern matchingPatternOfEmbeddedGraph;

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        public IDictionary<string, string> annotations = new Dictionary<string, string>();

        /// <summary>
        /// An array with the expressions giving the arguments to the subpattern,
        /// that are the pattern variables plus the pattern elements,
        /// with which the subpattern gets connected to the containing pattern.
        /// </summary>
        public Expression[] connections;

        /// <summary>
        /// An array with the output arguments to the subpattern,
        /// that are the pattern variables plus the pattern elements
        /// which the subpattern yields to the containing pattern.
        /// </summary>
        public String[] yields;

        /// <summary>
        /// An array of names of nodes needed by this subpattern embedding.
        /// </summary>
        public String[] neededNodes;

        /// <summary>
        /// An array of names of edges needed by this subpattern embedding.
        /// </summary>
        public String[] neededEdges;

        /// <summary>
        /// An array of names of variable needed by this subpattern embedding.
        /// </summary>
        public String[] neededVariables;

        /// <summary>
        /// An array of variable types (corresponding to the variable names) needed by this embedding.
        /// </summary>
        public VarType[] neededVariableTypes;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Tells whether this pattern usage was inlined.
        /// In this case it is ignored in matcher generation, 
        /// as all elements of the pattern used were added to the elementAndInlined-members of the using pattern.
        /// </summary>
        public bool inlined = false;

        /// <summary>
        /// Links to the original embedding in case this embedding was inlined, otherwise null.
        /// This tells that this embedding was used in another subpattern which was inlined.
        /// </summary>
        public PatternGraphEmbedding originalEmbedding;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this alternative was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructs a PatternGraphEmbedding object.
        /// </summary>
        /// <param name="name">The name of the usage of the subpattern.</param>
        /// <param name="matchingPatternOfEmbeddedGraph">The embedded subpattern.</param>
        /// <param name="connections">An array with the expressions defining how the subpattern is connected
        /// to the containing pattern (graph elements and basic variables) .</param>
        /// <param name="yields">An array with the def elements and variables 
        /// from the containing pattern yielded to from the subpattern.</param>
        /// <param name="neededNodes">An array with names of nodes needed by this embedding.</param>
        /// <param name="neededEdges">An array with names of edges needed by this embedding.</param>
        /// <param name="neededVariables">An array with names of variables needed by this embedding.</param>
        /// <param name="neededVariableTypes">An array with types of variables needed by this embedding.</param>
        public PatternGraphEmbedding(String name, LGSPMatchingPattern matchingPatternOfEmbeddedGraph,
                Expression[] connections, String[] yields,
                String[] neededNodes, String[] neededEdges,
                String[] neededVariables, VarType[] neededVariableTypes)
        {
            this.name = name;
            this.matchingPatternOfEmbeddedGraph = matchingPatternOfEmbeddedGraph;
            this.connections = connections;
            this.yields = yields;
            this.neededNodes = neededNodes;
            this.neededEdges = neededEdges;
            this.neededVariables = neededVariables;
            this.neededVariableTypes = neededVariableTypes;

            this.matchingPatternOfEmbeddedGraph.uses += 1;
        }

        /// <summary>
        /// Instantiates a new pattern graph embedding object as a copy from an original embedding, used for inlining.
        /// </summary>
        /// <param name="original">The original embedding to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new embedding will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the embedding (to avoid name collisions).</param>
        /// Elements were already copied in the containing pattern(s), their copies have to be reused here.
        public PatternGraphEmbedding(PatternGraphEmbedding original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix)
        {
            PointOfDefinition = newHost;
            name = original.name + nameSuffix;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding;
            matchingPatternOfEmbeddedGraph = original.matchingPatternOfEmbeddedGraph;
            annotations = original.annotations;
            connections = new Expression[original.connections.Length];
            for(int i = 0; i < original.connections.Length; ++i)
            {
                connections[i] = original.connections[i].Copy(nameSuffix);
            }
            yields = new String[original.yields.Length];
            for(int i = 0; i < original.yields.Length; ++i)
            {
                yields[i] = original.yields[i] + nameSuffix;
            }
            neededNodes = new String[original.neededNodes.Length];
            for(int i = 0; i < original.neededNodes.Length; ++i)
            {
                neededNodes[i] = original.neededNodes[i] + nameSuffix;
            }
            neededEdges = new String[original.neededEdges.Length];
            for(int i = 0; i < original.neededEdges.Length; ++i)
            {
                neededEdges[i] = original.neededEdges[i] + nameSuffix;
            }
            neededVariables = new String[original.neededVariables.Length];
            for(int i = 0; i < original.neededVariables.Length; ++i)
            {
                neededVariables[i] = original.neededVariables[i] + nameSuffix;
            }
            neededVariableTypes = (VarType[])original.neededVariableTypes.Clone();

            originalEmbedding = original;
        }
    }

    /// <summary>
    /// An alternative is a pattern graph element containing subpatterns
    /// of which one must get successfully matched so that the entire pattern gets matched successfully.
    /// </summary>
    public class Alternative : IAlternative
    {
        /// <summary>
        /// Array with the alternative cases.
        /// </summary>
        public IPatternGraph[] AlternativeCases { get { return alternativeCases; } }

        /// <summary>
        /// Name of the alternative.
        /// </summary>
        public String name;

        /// <summary>
        /// Prefix for name from nesting path.
        /// </summary>
        public String pathPrefix;

        /// <summary>
        /// Array with the alternative cases.
        /// </summary>
        public PatternGraph[] alternativeCases;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Links to the original alternative in case this alternative was inlined, otherwise null
        /// </summary>
        public Alternative originalAlternative;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this alternative was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructs an Alternative object.
        /// </summary>
        /// <param name="name">Name of the alternative.</param>
        /// <param name="pathPrefix">Prefix for name from nesting path.</param>
        /// <param name="cases">Array with the alternative cases.</param>
        public Alternative(String name, String pathPrefix, PatternGraph[] cases)
        {
            this.name = name;
            this.pathPrefix = pathPrefix;
            this.alternativeCases = cases;
        }

        /// <summary>
        /// Instantiates a new alternative object as a copy from an original alternative, used for inlining.
        /// </summary>
        /// <param name="original">The original alternative to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new alternative will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the alternative and its elements (to avoid name collisions).</param>
        /// Elements might have been already copied in the containing pattern(s), their copies have to be reused in this case.
        public Alternative(Alternative original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix, String pathPrefix_,
            Dictionary<PatternNode, PatternNode> nodeToCopy,
            Dictionary<PatternEdge, PatternEdge> edgeToCopy,
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            name = original.name + nameSuffix + "_in_" + inlinedSubpatternEmbedding.PointOfDefinition.pathPrefix + inlinedSubpatternEmbedding.PointOfDefinition.name;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding; 
            pathPrefix = pathPrefix_;

            alternativeCases = new PatternGraph[original.alternativeCases.Length];
            for(int i = 0; i < original.alternativeCases.Length; ++i)
            {
                PatternGraph altCase = original.alternativeCases[i];
                alternativeCases[i] = new PatternGraph(altCase, inlinedSubpatternEmbedding, newHost, nameSuffix, 
                    nodeToCopy, edgeToCopy, variableToCopy);
            }

            originalAlternative = original;
        }
    }

    /// <summary>
    /// An iterated is a pattern graph element containing the subpattern to be matched iteratively
    /// and the information how much matches are needed for success and how much matches to obtain at most
    /// </summary>
    public class Iterated : IIterated
    {
        /// <summary>
        ///The iterated pattern to be matched as often as possible within specified bounds.
        /// </summary>
        public IPatternGraph IteratedPattern { get { return iteratedPattern; } }

        /// <summary>
        /// How many matches to find so the iterated succeeds.
        /// </summary>
        public int MinMatches { get { return minMatches; } }

        /// <summary>
        /// The upper bound to stop matching at, 0 means unlimited/as often as possible.
        /// </summary>
        public int MaxMatches { get { return maxMatches; } }

        /// <summary>
        ///The iterated pattern to be matched as often as possible within specified bounds.
        /// </summary>
        public PatternGraph iteratedPattern;

        /// <summary>
        /// How many matches to find so the iterated succeeds.
        /// </summary>
        public int minMatches;

        /// <summary>
        /// The upper bound to stop matching at, 0 means unlimited.
        /// </summary>
        public int maxMatches;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Links to the original iterated in case this iterated was inlined, otherwise null
        /// </summary>
        public Iterated originalIterated;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this iterated was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructs an Iterated object.
        /// </summary>
        /// <param name="iterated">PatternGraph of the iterated.</param>
        public Iterated(PatternGraph iteratedPattern, int minMatches, int maxMatches)
        {
            this.iteratedPattern = iteratedPattern;
            this.minMatches = minMatches;
            this.maxMatches = maxMatches;
        }

        /// <summary>
        /// Instantiates a new iterated object as a copy from an original iterated, used for inlining.
        /// </summary>
        /// <param name="original">The original iterated to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new iterated will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the iterated and its elements (to avoid name collisions).</param>
        /// Elements might have been already copied in the containing pattern(s), their copies have to be reused in this case.
        public Iterated(Iterated original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix,
            Dictionary<PatternNode, PatternNode> nodeToCopy,
            Dictionary<PatternEdge, PatternEdge> edgeToCopy,
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            iteratedPattern = new PatternGraph(original.iteratedPattern, inlinedSubpatternEmbedding, newHost, nameSuffix, 
                    nodeToCopy, edgeToCopy, variableToCopy);
            minMatches = original.minMatches;
            maxMatches = original.maxMatches;

            originalIterated = original;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding;
        }
    }
}
