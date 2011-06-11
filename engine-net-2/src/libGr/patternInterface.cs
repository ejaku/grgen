/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An element of a rule pattern.
    /// </summary>
    public interface IPatternElement
    {
        /// <summary>
        /// The name of the pattern element.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The pure name of the pattern element as specified in the .grg without any prefixes.
        /// </summary>
        String UnprefixedName { get; }

        /// <summary>
        /// The pattern where this element is contained the first time / gets matched (null if rule parameter).
        /// </summary>
        IPatternGraph PointOfDefinition { get; }

        /// <summary>
        /// Iff true the element is only defined in its PointOfDefinition pattern,
        /// it gets matched in another, nested or called pattern which yields it to the containing pattern.
        /// </summary>
        bool DefToBeYieldedTo { get; }

        /// <summary>
        /// The annotations of the pattern element.
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Annotations { get; }
    }

    /// <summary>
    /// A pattern node of a rule pattern.
    /// </summary>
    public interface IPatternNode : IPatternElement
    {
        // currently empty
    }

    /// <summary>
    /// A pattern edge of a rule pattern.
    /// </summary>
    public interface IPatternEdge : IPatternElement
    {
        // currently empty
    }

    /// <summary>
    /// A pattern variable of a rule pattern.
    /// </summary>
    public interface IPatternVariable : IPatternElement
    {
        // currently empty
    }

    /// <summary>
    /// A pattern graph.
    /// </summary>
    public interface IPatternGraph
    {
        /// <summary>
        /// The name of the pattern graph
        /// </summary>
        String Name { get; }

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

    /// <summary>
    /// Embedding of a subpattern into it's containing pattern
    /// </summary>
    public interface IPatternGraphEmbedding
    {
        /// <summary>
        /// The name of the usage of the subpattern.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The embedded subpattern
        /// </summary>
        IPatternGraph EmbeddedGraph { get; }

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Annotations { get; }
    }

    /// <summary>
    /// An alternative is a pattern graph element containing subpatterns
    /// of which one must get successfully matched so that the entire pattern gets matched successfully
    /// </summary>
    public interface IAlternative
    {
        /// <summary>
        /// Array with the alternative cases
        /// </summary>
        IPatternGraph[] AlternativeCases { get; }
    }

    /// <summary>
    /// An iterated is a pattern graph element containing the subpattern to be matched iteratively
    /// and the information how much matches are needed for success and how much matches to obtain at most
    /// </summary>
    public interface IIterated
    {
        /// <summary>
        ///The iterated pattern to be matched as often as possible within specified bounds.
        /// </summary>
        IPatternGraph IteratedPattern { get; }

        /// <summary>
        /// How many matches to find so the iterated succeeds.
        /// </summary>
        int MinMatches { get; }

        /// <summary>
        /// The upper bound to stop matching at, 0 means unlimited/as often as possible.
        /// </summary>
        int MaxMatches { get; }
    }

    /// <summary>
    /// A description of a GrGen matching pattern, that's a subpattern/subrule or the base for some rule.
    /// </summary>
    public interface IMatchingPattern
    {
        /// <summary>
        /// The main pattern graph.
        /// </summary>
        IPatternGraph PatternGraph { get; }

        /// <summary>
        /// An array of GrGen types corresponding to rule parameters.
        /// </summary>
        GrGenType[] Inputs { get; }

        /// <summary>
        /// An array of the names corresponding to rule parameters.
        /// </summary>
        String[] InputNames { get; }

        /// <summary>
        /// An array of the names of the def elements yielded out of this pattern.
        /// </summary>
        String[] DefNames { get; }

        /// <summary>
        /// The annotations of the matching pattern (test/rule/subpattern)
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Annotations { get; }
    }

    // TODO: split ISubpatternPattern out of IMatching pattern, def elements are a subpattern only thing 
    // -> IMatchingPattern as parent element for IRulePattern and ISubpatternPattern

    /// <summary>
    /// A description of a GrGen rule.
    /// </summary>
    public interface IRulePattern : IMatchingPattern
    {
        /// <summary>
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        GrGenType[] Outputs { get; }
    }
}

