/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

//#define LOG_MATCHES

using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A class which contains the AreSymmetric function to check to matches of a pattern for being symmetric,
    /// i.e. to be matches of a pattern which is automorph to itself.
    /// </summary>
    public static class SymmetryChecker
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
#if LOG_MATCHES
            ConsoleUI.outWriter.WriteLine("match this: " + MatchPrinter.ToString(this_, graph, ""));
            ConsoleUI.outWriter.WriteLine("match that: " + MatchPrinter.ToString(that, graph, ""));
#endif
            if(this_.Pattern != that.Pattern)
                return false;

            int id = Mark(this_, graph);
            bool allElementsWereMarked = AreAllMarked(that, graph, id);
            Unmark(this_, graph, id);
            if(!allElementsWereMarked)
                return false;

            // if we'd know there was no hom used, locally and in the subpatterns employed, we could get along without this counter direction check, which is needed to reject containment
            id = Mark(that, graph);
            allElementsWereMarked = AreAllMarked(this_, graph, id);
            Unmark(that, graph, id);
            if(!allElementsWereMarked)
                return false;

            // locally the match this_ was a permutation of the match that
            // now check globally the nested and subpatterns

            // independents don't need to be checked, there is always only one, existence counts
            // (and negatives prevent a match in the first place, we'd never reach this if they would be found)

            // alternatives/alternative cases must be in a 1:1 correspondence (equal cases are ensured by the identical pattern check)
            for(int i = 0; i < this_.NumberOfAlternatives; ++i)
            {
                if(!AreSymmetric(this_.getAlternativeAt(i), that.getAlternativeAt(i), graph))
                    return false;
            }

            // iterated patterns are all-matches blocker, they are eagerly matched from their inputs on, 
            // with each iterated pattern settling on the first match, not searching for all, even if requested for the action
            // there can't be symmetric matches due to an iterated, only through the elements up to the iterated
            /* the iterateds must be in a 1:1 correspondence -- but the iterations may be permuted, that's handled in the call
            for(int i = 0; i < this_.NumberOfIterateds; ++i)
                if(!AreIterationsSymmetric(this_.getIteratedAt(i), that.getIteratedAt(i), graph))
                    return false;*/

            // the subpatterns of equal type may be permuted, the rest must be in a 1:1 correspondence
            // (by encapsulating the elements above in a subpattern instead of duplicating their content locally they can get symmetry checked)
            if(!AreSubpatternsSymmetric(this_, that, graph))
                return false;

#if LOG_MATCHES
            ConsoleUI.outWriter.WriteLine("this is symmetric to that");
#endif
            return true;
        }

        /// <summary>
        /// Mark the elements of the match in the graph with a visited flag.
        /// Allocates a visited flag.
        /// </summary>
        /// <param name="match">The match to mark in the graph with a visited flag</param>
        /// <param name="graph">The graph in which the match was found, needed for visited flag access</param>
        /// <returns>The visited flag id, for later checks and the unmarking</returns>
        private static int Mark(IMatch match, IGraph graph)
        {
            int id = graph.AllocateVisitedFlag();

            for(int i = 0; i < match.NumberOfNodes; ++i)
            {
                graph.SetVisited(match.getNodeAt(i), id, true);
            }

            for(int i = 0; i < match.NumberOfEdges; ++i)
            {
                graph.SetVisited(match.getEdgeAt(i), id, true);
            }

            return id;
        }

        /// <summary>
        /// Unmark the elements of the match in the graph, regarding the given visited flag.
        /// Deallocates the visited flag.
        /// </summary>
        /// <param name="match">The match to mark in the graph with a visited flag</param>
        /// <param name="graph">The graph in which the match was found, needed for visited flag access</param>
        /// <param name="id">The visited flag id</param>
        private static void Unmark(IMatch match, IGraph graph, int id)
        {
            for(int i = 0; i < match.NumberOfNodes; ++i)
            {
                graph.SetVisited(match.getNodeAt(i), id, false);
            }

            for(int i = 0; i < match.NumberOfEdges; ++i)
            {
                graph.SetVisited(match.getEdgeAt(i), id, false);
            }

            graph.FreeVisitedFlagNonReset(id);
        }

        /// <summary>
        /// Checks whether all elements of the match are marked in the graph, regarding the given visited flag.
        /// </summary>
        /// <param name="match">The match to check for being marked</param>
        /// <param name="graph">The graph in which the match was found, needed for visited flag access</param>
        /// <param name="id">The visited flag id</param>
        private static bool AreAllMarked(IMatch match, IGraph graph, int id)
        {
            for(int i = 0; i < match.NumberOfNodes; ++i)
            {
                if(!graph.IsVisited(match.getNodeAt(i), id))
                    return false;
            }

            for(int i = 0; i < match.NumberOfEdges; ++i)
            {
                if(!graph.IsVisited(match.getEdgeAt(i), id))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether the iterated matches are symmetric, 
        /// i.e. are covering the same spot in the graph with a permutation of the single matches
        /// </summary>
        private static bool AreIterationsSymmetric(IMatches this_, IMatches that, IGraph graph)
        {
            if(this_.Count != that.Count)
                return false;

            return AreIterationsSymmetric(this_, that, graph, 0);
        }

        /// <summary>
        /// Searches for a symmetric partner for the match at the index in this
        /// Returns true if it was possible in the end to map every match in this to a match in that
        /// </summary>
        private static bool AreIterationsSymmetric(IMatches this_, IMatches that, IGraph graph, int index)
        {
            if(index >= this_.Count)
                return true;

            for(int j = 0; j < that.Count; ++j)
            {
                if(that.GetMatch(j).IsMarked())
                    continue;
                if(!AreSymmetric(this_.GetMatch(index), that.GetMatch(j), graph))
                    continue;

                that.GetMatch(j).Mark(true);
                bool wereSymmetric = AreIterationsSymmetric(this_, that, graph, index + 1);
                that.GetMatch(j).Mark(false);

                if(wereSymmetric)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether the subpattern matches are symmetric, 
        /// i.e. are covering the same spot in the graph with a permutation of the matches of same subpattern type
        /// </summary>
        private static bool AreSubpatternsSymmetric(IMatch this_, IMatch that, IGraph graph)
        {
            switch(this_.NumberOfEmbeddedGraphs)
            {
            case 0:
                return true;
            case 1:
                return AreSymmetric(this_.getEmbeddedGraphAt(0), that.getEmbeddedGraphAt(0), graph);
            case 2:
                {
                    if(this_.getEmbeddedGraphAt(0).Pattern != this_.getEmbeddedGraphAt(1).Pattern)
                    {
                        return AreSymmetric(this_.getEmbeddedGraphAt(0), that.getEmbeddedGraphAt(0), graph)
                            && AreSymmetric(this_.getEmbeddedGraphAt(1), that.getEmbeddedGraphAt(1), graph);
                    }
                    else
                    {
                        return ( AreSymmetric(this_.getEmbeddedGraphAt(0), that.getEmbeddedGraphAt(0), graph)
                            && AreSymmetric(this_.getEmbeddedGraphAt(1), that.getEmbeddedGraphAt(1), graph) )
                            || ( AreSymmetric(this_.getEmbeddedGraphAt(0), that.getEmbeddedGraphAt(1), graph)
                            && AreSymmetric(this_.getEmbeddedGraphAt(1), that.getEmbeddedGraphAt(0), graph) );
                    }
                }
            default:
                {
                    // compute partition of subpatterns according to type
                    Dictionary<IPatternGraph, List<int>> partitions = new Dictionary<IPatternGraph, List<int>>();
                    for(int i = 0; i < this_.NumberOfEmbeddedGraphs; ++i)
                    {
                        IMatch match = this_.getEmbeddedGraphAt(i);
                        if(!partitions.ContainsKey(match.Pattern))
                            partitions.Add(match.Pattern, new List<int>());
                        partitions[match.Pattern].Add(i);
                    }

                    // the subpatterns of equal type may be permuted, the rest must be in a 1:1 correspondence
                    foreach(KeyValuePair<IPatternGraph, List<int>> kvp in partitions)
                    {
                        if(!AreSubpatternsSymmetric(this_, that, graph, kvp.Value, 0))
                            return false;
                    }

                    return true;
                }
            }
        }

        /// <summary>
        /// Checks whether the subpattern matches of the partition are symmetric, 
        /// i.e. are covering the same spot in the graph with a permutation of the matches in the partition
        /// Searches for a symmetric partner for the subpattern match at the index in the partition in this
        /// Returns true if it was possible in the end to map every match in the partition in this to a match in that
        /// </summary>
        private static bool AreSubpatternsSymmetric(IMatch this_, IMatch that, IGraph graph, List<int> partition, int index)
        {
            if(index >= partition.Count)
                return true;

            for(int j = 0; j < partition.Count; ++j)
            {
                if(that.getEmbeddedGraphAt(partition[j]).IsMarked())
                    continue;
                if(!AreSymmetric(this_.getEmbeddedGraphAt(partition[index]), that.getEmbeddedGraphAt(partition[j]), graph))
                    continue;

                that.getEmbeddedGraphAt(partition[j]).Mark(true);
                bool wereSymmetric = AreSubpatternsSymmetric(this_, that, graph, partition, index + 1);
                that.getEmbeddedGraphAt(partition[j]).Mark(false);

                if(wereSymmetric)
                    return true;
            }

            return false;
        }
    }
}
