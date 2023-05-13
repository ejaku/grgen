/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
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
        public readonly CAEType CAEType;

        /// <summary>
        /// Specifies the graph element, where the error was found.
        /// </summary>
        public readonly IGraphElement Elem;

        /// <summary>
        /// The number of edges found in the graph, if CAEType != CAEType.EdgeNotSpecified.
        /// </summary>
        public readonly long FoundEdges;

        /// <summary>
        /// The corresponding ValidatedInfo object, if CAEType != CAEType.EdgeNotSpecified.
        /// Otherwise it is null.
        /// </summary>
        public readonly ValidateInfo ValidateInfo;

        /// <summary>
        /// Initializes a ConnectionAssertionError instance.
        /// </summary>
        /// <param name="caeType">The type of error.</param>
        /// <param name="elem">The graph element, where the error was found.</param>
        /// <param name="found">The number of edges found in the graph, if CAEType != CAEType.EdgeNotSpecified.</param>
        /// <param name="valInfo">The corresponding ValidatedInfo object, if CAEType != CAEType.EdgeNotSpecified, otherwise null.</param>
        public ConnectionAssertionError(CAEType caeType, IGraphElement elem, long found, ValidateInfo valInfo)
        {
            CAEType = caeType;
            Elem = elem;
            FoundEdges = found;
            ValidateInfo = valInfo;
        }
    }
}
