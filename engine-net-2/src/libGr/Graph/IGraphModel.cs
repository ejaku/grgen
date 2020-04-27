/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
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
        /// If true, function methods (and functions from the actions based on this model) are also available in a parallelized version, 
        /// and external functions and function methods of external classes are expected to be also available in a parallelized version
        /// (and graph element uniqueness is ensured).
        /// </summary>
        bool AreFunctionsParallelized { get; }

        /// <summary>
        /// Tells about the number of threads to use for the equalsAny and equalsAnyStructurally functions
        /// The normal non-parallel isomorphy comparison functions are used if this value is below 2
        /// </summary>
        int BranchingFactorForEqualsAny { get; }

        /// <summary>
        /// Called by the graph (generic implementation) to create its uniqueness handler (generated code).
        /// Always called by an empty graph just constructed, the uniqueness handler is then directly bound to the graph.
        /// </summary>
        IUniquenessHandler CreateUniquenessHandler(IGraph graph);

        /// <summary>
        /// Called by the graph (generic implementation) to create its index set (generated code).
        /// Always called by an empty graph just constructed, the index set is then directly bound to the graph.
        /// </summary>
        IIndexSet CreateIndexSet(IGraph graph);

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
        /// Called when the grs importer or the shell hits a line starting with "external".
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
        /// Debugging helper. Fails in a debug build with an assertion.
        /// </summary>
        void FailAssertion();

        /// <summary>
        /// An MD5 hash sum of the model.
        /// </summary>
        String MD5Hash { get; }
    }
}
