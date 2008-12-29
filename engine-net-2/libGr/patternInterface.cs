/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;

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

/*        /// <summary>
        /// The GrGen type of the element.
        /// </summary>
        IType Type { get; }*/
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
        /// i.e. the nodes are contained in different, but locally nested patterns (alternative cases).
        /// </summary>
        bool[,] HomomorphicNodesGlobal { get; }

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge globally,
        /// i.e. the edges are contained in different, but locally nested patterns (alternative cases).
        /// </summary>
        bool[,] HomomorphicEdgesGlobal { get; }

        /// <summary>
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        IPatternGraphEmbedding[] EmbeddedGraphs { get; }

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        IAlternative[] Alternatives { get; }

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
        /// An array with the connections telling how the subpattern is connected to the containing pattern,
        /// that are the pattern elements of the containing pattern used for that purpose
        /// </summary>
        IPatternElement[] Connections { get; }
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
    }

    /// <summary>
    /// A description of a GrGen rule.
    /// </summary>
    public interface IRulePattern : IMatchingPattern
    {
        /// <summary>
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        GrGenType[] Outputs { get; }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match (rewrite part).
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <param name="graph">The host graph for this modification.</param>
        /// <param name="match">The match which is used for this rewrite.</param>
        /// <returns>An array of objects returned by the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        object[] Modify(IGraph graph, IMatch match);
    }
}

