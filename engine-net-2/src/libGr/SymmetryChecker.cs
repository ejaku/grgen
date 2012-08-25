/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A class which contains the AreSymmetric function to check to matches of a pattern for being symmetric,
    /// i.e. to be matches of a pattern which is automorph to itself.
    /// </summary>
    public class SymmetryChecker
    {
        /// <summary>
        /// Checks whether the matches are symmetric, 
        /// i.e. are covering the same spot in the graph with a permutation of the pattern to graph mapping; 
        /// that might be the case if they are matches of a pattern which is automorph to itself.
        /// This function is employed by the generated automorph filters for rule \ auto.
        /// Ths subpattern derivations must be structurally identical, modulo permutations of matches of the same subpattern type.
        /// </summary>
        /// <param name="this_">The one match to check for being symmetric to the other</param>
        /// <param name="that">The other match to check for being symmetric to the one match</param>
        /// <param name="graph">The graph in which the matches were found</param>
        /// <returns>True if the matches are symmetric, i.e. covering the same spot in the graph, otherwise false.</returns>
        public static bool AreSymmetric(IMatch this_, IMatch that, IGraph graph)
        {
            if(this_.Pattern != that.Pattern)
                return false;

            int id = Mark(this_, graph);
            bool allElementsWereMarked = AreAllMarked(that, graph, id);
            Unmark(this_, graph, id);
            if(!allElementsWereMarked)
                return false;

            // if we'd know there was no hom used, locally and in the subpatterns employed, we could get along without this counter direction check
            id = Mark(that, graph);
            allElementsWereMarked = AreAllMarked(this_, graph, id);
            Unmark(that, graph, id);
            if(!allElementsWereMarked)
                return false;

            // locally the match this_ was a permutation of the match that

            // TODO: check subpatterns et al

            return true;
        }

        /// <summary>
        /// Mark the elements of the match in the graph with a visited flag.
        /// Allocates a visited flag.
        /// </summary>
        /// <param name="match">The match to mark in the graph with a visited flag</param>
        /// <param name="graph">The graph in which the match was found, needed for visited flag access</param>
        /// <returns>The visited flag id, for later checks and the unmarking</returns>
        public static int Mark(IMatch match, IGraph graph)
        {
            int id = graph.AllocateVisitedFlag();

            for(int i = match.NumberOfNodes; i < match.NumberOfNodes; ++i)
                graph.SetVisited(match.getNodeAt(i), id, true);

            for(int i = match.NumberOfEdges; i < match.NumberOfEdges; ++i)
                graph.SetVisited(match.getEdgeAt(i), id, true);

            return id;
        }

        /// <summary>
        /// Unmark the elements of the match in the graph, regarding the given visited flag.
        /// Deallocates the visited flag.
        /// </summary>
        /// <param name="match">The match to mark in the graph with a visited flag</param>
        /// <param name="graph">The graph in which the match was found, needed for visited flag access</param>
        /// <param name="id">The visited flag id</param>
        public static void Unmark(IMatch match, IGraph graph, int id)
        {
            for(int i = match.NumberOfNodes; i < match.NumberOfNodes; ++i)
                graph.SetVisited(match.getNodeAt(i), id, false);

            for(int i = match.NumberOfEdges; i < match.NumberOfEdges; ++i)
                graph.SetVisited(match.getEdgeAt(i), id, false);

            graph.FreeVisitedFlagNonReset(id);
        }

        /// <summary>
        /// Checks whether all elements of the match are marked in the graph, regarding the given visited flag.
        /// </summary>
        /// <param name="match">The match to check for being marked</param>
        /// <param name="graph">The graph in which the match was found, needed for visited flag access</param>
        /// <param name="id">The visited flag id</param>
        public static bool AreAllMarked(IMatch match, IGraph graph, int id)
        {
            for(int i = match.NumberOfNodes; i < match.NumberOfNodes; ++i)
                if(!graph.IsVisited(match.getNodeAt(i), id))
                    return false;

            for(int i = match.NumberOfEdges; i < match.NumberOfEdges; ++i)
                if(!graph.IsVisited(match.getEdgeAt(i), id))
                    return false;

            return true;
        }
    }
}
