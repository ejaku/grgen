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
    /// Base class of classes representing matches.
    /// One exact match class is generated per pattern.
    /// </summary>
    public interface IMatch
    {
        /// <summary>
        /// The match object represents a match of the pattern given by this member.
        /// May be null in case of a match class created by a constructor instead of an action.
        /// </summary>
        IPatternGraph Pattern { get; }

        /// <summary>
        /// The match object represents a match of the match class given by this member.
        /// Only set in case the match class was created by a constructor, otherwise the pattern is given.
        /// </summary>
        IMatchClass MatchClass { get; }

        /// <summary>
        /// The match of the enclosing pattern if this is the pattern of
        /// a subpattern, alternative, iterated or independent; otherwise null
        /// </summary>
        IMatch MatchOfEnclosingPattern { get; }

        /// <summary>
        /// Clone the match
        /// </summary>
        IMatch Clone();

        /// <summary>
        /// Clone the match, mapping the old graph elements to new graph elements according to the oldToNewMap
        /// </summary>
        /// <param name="oldToNewMap">A map of old to corresponding new graph elements.</param>
        IMatch Clone(IDictionary<IGraphElement, IGraphElement> oldToNewMap);

        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Writes a flag to the match, which is remembered; helper for symmetry checking
        /// </summary>
        /// <param name="flag">The boolean value to write</param>
        void Mark(bool flag);

        /// <summary>
        /// Reads a previously written flag (intially false); helper for symmetry checking
        /// </summary>
        /// <returns></returns>
        bool IsMarked();

        /// <summary>
        /// Helper for parallelized matching, for building the matches list as if it was matched sequentially
        /// </summary>
        int IterationNumber { get; set; }

        //////////////////////////////////////////////////////////////////////////
        // Nodes

        /// <summary>
        /// Enumerable returning enumerator over matched nodes (most inefficient access)
        /// </summary>
        IEnumerable<INode> Nodes { get; }

        /// <summary>
        /// Enumerator over matched nodes (efficiency in between getNodeAt and Nodes)
        /// </summary>
        IEnumerator<INode> NodesEnumerator { get; }

        /// <summary>
        /// Number of nodes in the match
        /// </summary>
        int NumberOfNodes { get; }

        /// <summary>
        /// Returns node at position index (most efficient access)
        /// </summary>
        /// <param name="index">The position of the node to return</param>
        /// <returns>The node at the given index</returns>
        INode getNodeAt(int index);

        /// <summary>
        /// Returns node bound to the pattern node of the given name or null if no such pattern node exists
        /// </summary>
        INode getNode(string name);

        void SetNode(string name, INode node);


        //////////////////////////////////////////////////////////////////////////
        // Edges

        /// <summary>
        /// Enumerable returning enumerator over matched edges (most inefficient access)
        /// </summary>
        IEnumerable<IEdge> Edges { get; }

        /// <summary>
        /// Enumerator over matched edges (efficiency in between getEdgeAt and Edges)
        /// </summary>
        IEnumerator<IEdge> EdgesEnumerator { get; }

        /// <summary>
        /// Number of edges in the match
        /// </summary>
        int NumberOfEdges { get; }

        /// <summary>
        /// Returns edge at position index (most efficient access)
        /// </summary>
        /// <param name="index">The position of the edge to return</param>
        /// <returns>The edge at the given index</returns>
        IEdge getEdgeAt(int index);

        /// <summary>
        /// Returns edge bound to the pattern edge of the given name or null if no such pattern edge exists
        /// </summary>
        IEdge getEdge(string name);

        void SetEdge(string name, IEdge edge);


        //////////////////////////////////////////////////////////////////////////
        // Variables

        /// <summary>
        /// Enumerable returning enumerator over matched variables (most inefficient access)
        /// </summary>
        IEnumerable<object> Variables { get; }

        /// <summary>
        /// Enumerator over matched variables (efficiency in between getVariableAt and Variables)
        /// </summary>
        IEnumerator<object> VariablesEnumerator { get; }

        /// <summary>
        /// Number of variables in the match
        /// </summary>
        int NumberOfVariables { get; }

        /// <summary>
        /// Returns variable at position index (most efficient access)
        /// </summary>
        /// <param name="index">The position of the variable to return</param>
        /// <returns>The variable at the given index</returns>
        object getVariableAt(int index);

        /// <summary>
        /// Returns value bound to the pattern variable of the given name or null if no such pattern variable exists
        /// </summary>
        object getVariable(string name);

        void SetVariable(string name, object value);


        //////////////////////////////////////////////////////////////////////////
        // By Name Access to Members = Nodes, Edges, Variables

        /// <summary>
        /// Returns value bound to the member of the given name or null if no such member exists
        /// </summary>
        object GetMember(string name);

        /// <summary>
        /// Sets member value (to be used by post-matches-filtering)
        /// </summary>
        void SetMember(string name, object value);


        //////////////////////////////////////////////////////////////////////////
        // Embedded Graphs

        /// <summary>
        /// Enumerable returning enumerator over submatches due to subpatterns (most inefficient access)
        /// </summary>
        IEnumerable<IMatch> EmbeddedGraphs { get; }

        /// <summary>
        /// Enumerator over submatches due to subpatterns (efficiency in between getEmbeddedGraphAt and EmbeddedGraphs)
        /// </summary>
        IEnumerator<IMatch> EmbeddedGraphsEnumerator { get; }

        /// <summary>
        /// Number of submatches due to subpatterns in the match
        /// </summary>
        int NumberOfEmbeddedGraphs { get; }

        /// <summary>
        /// Returns submatch due to subpattern at position index (most efficient access)
        /// </summary>
        /// <param name="index">The position of the submatch due to subpattern to return</param>
        /// <returns>The submatch due to subpattern at the given index</returns>
        IMatch getEmbeddedGraphAt(int index);

        /// <summary>
        /// Returns submatch bound to the subpattern of the given name or null if no such subpattern exists
        /// </summary>
        IMatch getEmbeddedGraph(string name);


        //////////////////////////////////////////////////////////////////////////
        // Alternatives

        /// <summary>
        /// Enumerable returning enumerator over submatches due to alternatives (most inefficient access)
        /// </summary>
        IEnumerable<IMatch> Alternatives { get; }

        /// <summary>
        /// Enumerator over submatches due to alternatives. (efficiency in between getAlternativeAt and Alternatives)
        /// You can find out which alternative case was matched by inspecting the Pattern member of the submatch.
        /// </summary>
        IEnumerator<IMatch> AlternativesEnumerator { get; }

        /// <summary>
        /// Number of submatches due to alternatives in the match
        /// </summary>
        int NumberOfAlternatives { get; }

        /// <summary>
        /// Returns submatch due to alternatives at position index (most efficient access)
        /// </summary>
        /// <param name="index">The position of the submatch due to alternatives to return</param>
        /// <returns>The submatch due to alternatives at the given index</returns>
        IMatch getAlternativeAt(int index);

        /// <summary>
        /// Returns submatch bound to the pattern alternative of the given name or null if no such pattern alternative exists
        /// </summary>
        IMatch getAlternative(string name);


        //////////////////////////////////////////////////////////////////////////
        // Iterateds

        /// <summary>
        /// Enumerable returning enumerator over submatches due to iterateds (most inefficient access)
        /// The submatch is a list of all matches of the iterated pattern.
        /// </summary>
        IEnumerable<IMatches> Iterateds { get; }

        /// <summary>
        /// Enumerator over submatches due to iterateds. (efficiency in between getIteratedAt and Iterateds)
        /// The submatch is a list of all matches of the iterated pattern.
        /// </summary>
        IEnumerator<IMatches> IteratedsEnumerator { get; }

        /// <summary>
        /// Number of submatches due to iterateds in the match.
        /// Corresponding to the number of iterated patterns, not the number of matches of some iterated pattern.
        /// </summary>
        int NumberOfIterateds { get; }

        /// <summary>
        /// Returns submatch due to iterateds at position index (most efficient access)
        /// The submatch is a list of all matches of the iterated pattern.
        /// </summary>
        /// <param name="index">The position of the submatch due to iterateds to return</param>
        /// <returns>The submatch due to iterateds at the given index</returns>
        IMatches getIteratedAt(int index);

        /// <summary>
        /// Returns submatch bound to the iterated pattern of the given name or null if no such iterated pattern exists
        /// </summary>
        IMatches getIterated(string name);


        //////////////////////////////////////////////////////////////////////////
        // Independents

        /// <summary>
        /// Enumerable returning enumerator over submatches due to independents (most inefficient access)
        /// </summary>
        IEnumerable<IMatch> Independents { get; }

        /// <summary>
        /// Enumerator over submatches due to independents. (efficiency in between getIndependentAt and Independents)
        /// </summary>
        IEnumerator<IMatch> IndependentsEnumerator { get; }

        /// <summary>
        /// Number of submatches due to independents in the match
        /// </summary>
        int NumberOfIndependents { get; }

        /// <summary>
        /// Returns submatch due to independents at position index (most efficient access)
        /// </summary>
        /// <param name="index">The position of the submatch due to independents to return</param>
        /// <returns>The submatch due to independents at the given index</returns>
        IMatch getIndependentAt(int index);

        /// <summary>
        /// Returns submatch bound to the independent pattern of the given name or null if no such independent pattern exists
        /// </summary>
        IMatch getIndependent(string name);
    }
}
