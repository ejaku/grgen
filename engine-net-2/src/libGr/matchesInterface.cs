/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Text;
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
        /// </summary>
        IPatternGraph Pattern { get; }

        /// <summary>
        /// The match of the enclosing pattern if this is the pattern of
        /// a subpattern, alternative, iterated or independent; otherwise null
        /// </summary>
        IMatch MatchOfEnclosingPattern { get; }

        /// <summary>
        /// Clone the match
        /// </summary>
        IMatch Clone();


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
    }

    /// <summary>
    /// An object representing a (possibly empty) set of matches in a graph before the rewrite has been applied.
    /// If it is a match of an action, it is returned by IAction.Match() and given to the OnMatched event.
    /// Otherwise it's the match of an iterated-pattern, and the producing action is null.
    /// </summary>
    public interface IMatches : IEnumerable<IMatch>
    {
        /// <summary>
        /// The action object used to generate this IMatches object
        /// </summary>
        IAction Producer { get; }

        /// <summary>
        /// Returns the first match (null if no match exists).
        /// </summary>
        IMatch First { get; }

        /// <summary>
        /// The number of matches found by Producer
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the match with the given index. Invalid indices cause an exception.
        /// This may be slow. If you want to iterate over the elements the Matches IEnumerable should be used.
        /// </summary>
        IMatch GetMatch(int index);

		/// <summary>
		/// Removes the match at the given index and returns it.
		/// </summary>
		/// <param name="index">The index of the match to be removed.</param>
		/// <returns>The removed match.</returns>
		IMatch RemoveMatch(int index);

        /// <summary>
        /// Clone the matches
        /// </summary>
        IMatches Clone();
    }


    /// <summary>
    /// An object representing a (possibly empty) set of matches in a graph before the rewrite has been applied,
    /// capable of handing out enumerators of exact match interface type.
    /// </summary>
    public interface IMatchesExact<MatchInterface> : IMatches
    {
        /// <summary>
        /// Returns enumerator over matches of exact type
        /// </summary>
        IEnumerator<MatchInterface> GetEnumeratorExact();

        /// <summary>
        /// Returns the first match of exact type (null if no match exists).
        /// </summary>
        MatchInterface FirstExact { get; }

        /// <summary>
        /// Returns the match of exact type with the given index. Invalid indices cause an exception.
        /// This may be slow. If you want to iterate over the elements the MatchesExact IEnumerable should be used.
        /// </summary>
        MatchInterface GetMatchExact(int index);

        /// <summary>
        /// Removes the match of exact type at the given index and returns it.
        /// </summary>
        MatchInterface RemoveMatchExact(int index);
    }

    public class MatchPrinter
    {
        public static string ToString(IMatch match, IGraph graph, string indent)
        {
            StringBuilder sb = new StringBuilder(4096);

            sb.Append(indent + "nodes: ");
            foreach(INode node in match.Nodes)
            {
                sb.Append(DictionaryListHelper.ToStringAutomatic(node, graph));
                sb.Append(" ");
            }
            sb.Append("\n");

            sb.Append(indent + "edges: ");
            foreach(IEdge edge in match.Edges)
            {
                sb.Append(DictionaryListHelper.ToStringAutomatic(edge, graph));
                sb.Append(" ");
            }
            sb.Append("\n");

            if(match.NumberOfIndependents > 0)
            {
                sb.Append(indent + "independents: \n");
                foreach(IMatch independent in match.Independents)
                {
                    sb.Append(ToString(independent, graph, indent + "  "));
                }
                sb.Append("\n");
            }

            if(match.NumberOfAlternatives > 0)
            {
                sb.Append(indent + "alternatives: \n");
                foreach(IMatch alternativeCase in match.Alternatives)
                {
                    sb.Append(ToString(alternativeCase, graph, indent + "  "));
                }
                sb.Append("\n");
            }

            if(match.NumberOfIterateds > 0)
            {
                sb.Append(indent + "iterateds: \n");
                foreach(IMatches iterated in match.Iterateds)
                {
                    sb.Append(indent + " iterated: \n");
                    foreach(IMatch iteratedMatches in iterated)
                    {
                        sb.Append(ToString(iteratedMatches, graph, indent + "  "));
                    }
                }
                sb.Append("\n");
            }

            if(match.NumberOfEmbeddedGraphs > 0)
            {
                sb.Append(indent + "subpatterns: \n");
                foreach(IMatch subpattern in match.EmbeddedGraphs)
                {
                    sb.Append(ToString(subpattern, graph, indent + "  "));
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
