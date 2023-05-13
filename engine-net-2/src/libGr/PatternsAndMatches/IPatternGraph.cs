/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A pattern graph.
    /// </summary>
    public interface IPatternGraph : INamed
    {
        /// <summary>
        /// The name of the pattern graph
        /// </summary>
        new string Name { get; }

        /// <summary>
        /// null if this is a global pattern graph, otherwise the package the pattern graph is contained in.
        /// </summary>
        new string Package { get; }

        /// <summary>
        /// The name of the pattern graph in case of a global type,
        /// the name of the pattern graph is prefixed by the name of the package otherwise (package "::" name).
        /// </summary>
        new string PackagePrefixedName { get; }

        /// <summary>
        /// An array of all pattern nodes.
        /// </summary>
        IPatternNode[] Nodes { get; }

        /// <summary>
        /// An array of all pattern edges.
        /// </summary>
        IPatternEdge[] Edges { get; }

        /// <summary>
        /// An array of all pattern variables;
        /// </summary>
        IPatternVariable[] Variables { get; }

        /// <summary>
        /// An enumerable over all pattern elements.
        /// </summary>
        IEnumerable<IPatternElement> PatternElements { get; }

        /// <summary>
        /// Returns the pattern element with the given name if it is available, otherwise null.
        /// </summary>
        IPatternElement GetPatternElement(string name);

        /// <summary>
        /// Returns the source pattern node of the given edge, null if edge dangles to the left
        /// </summary>
        IPatternNode GetSource(IPatternEdge edge);

        /// <summary>
        /// Returns the target pattern node of the given edge, null if edge dangles to the right
        /// </summary>
        IPatternNode GetTarget(IPatternEdge edge);

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node.
        /// </summary>
        bool[,] HomomorphicNodes { get; }

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge.
        /// </summary>
        bool[,] HomomorphicEdges { get; }

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node globally,
        /// i.e. the nodes are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// </summary>
        bool[,] HomomorphicNodesGlobal { get; }

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge globally,
        /// i.e. the edges are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// </summary>
        bool[,] HomomorphicEdgesGlobal { get; }

        /// <summary>
        /// An array telling which pattern node is to be matched non-isomorphic(/independent) against any other node.
        /// </summary>
        bool[] TotallyHomomorphicNodes { get; }

        /// <summary>
        /// An array telling which pattern edge is to be matched non-isomorphic(/independent) against any other edge.
        /// </summary>
        bool[] TotallyHomomorphicEdges { get; }

        /// <summary>
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        IPatternGraphEmbedding[] EmbeddedGraphs { get; }

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        IAlternative[] Alternatives { get; }

        /// <summary>
        /// An array of iterateds, each iterated is matched as often as possible within the specified bounds.
        /// </summary>
        IIterated[] Iterateds { get; }

        /// <summary>
        /// An array of negative pattern graphs which make the search fail if they get matched
        /// (NACs - Negative Application Conditions).
        /// </summary>
        IPatternGraph[] NegativePatternGraphs { get; }

        /// <summary>
        /// An array of independent pattern graphs which must get matched in addition to the main pattern
        /// (PACs - Positive Application Conditions).
        /// </summary>
        IPatternGraph[] IndependentPatternGraphs { get; }

        /// <summary>
        /// The pattern graph which contains this pattern graph, null if this is a top-level-graph
        /// </summary>
        IPatternGraph EmbeddingGraph { get; }
    }
}

