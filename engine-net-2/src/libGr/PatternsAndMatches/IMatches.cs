/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Text;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
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

        /// <summary>
        /// For filtering with the auto-supplied filter keepFirst
        /// </summary>
        /// <param name="count">The number of matches to keep</param>
        void FilterKeepFirst(int count);
        
        /// <summary>
        /// For filtering with the auto-supplied filter keepLast
        /// </summary>
        /// <param name="count">The number of matches to keep</param>
        void FilterKeepLast(int count);

        /// <summary>
        /// For filtering with the auto-supplied filter removeFirst
        /// </summary>
        /// <param name="count">The number of matches to remove</param>
        void FilterRemoveFirst(int count);

        /// <summary>
        /// For filtering with the auto-supplied filter removeLast
        /// </summary>
        /// <param name="count">The number of matches to remove</param>
        void FilterRemoveLast(int count);

        /// <summary>
        /// For filtering with the auto-supplied filter keepFirstFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        void FilterKeepFirstFraction(double fraction);
        
        /// <summary>
        /// For filtering with the auto-supplied filter keepLastFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        void FilterKeepLastFraction(double fraction);

        /// <summary>
        /// For filtering with the auto-supplied filter removeFirstFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        void FilterRemoveFirstFraction(double fraction);

        /// <summary>
        /// For filtering with the auto-supplied filter removeLastFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        void FilterRemoveLastFraction(double fraction);
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

        /// <summary>
        /// Returns the content of the current matches list in form of an array which can be efficiently indexed and reordered.
        /// The array is destroyed when this method is called again, the content is destroyed when the rule is matched again (there is only one array existing).
        /// </summary>
        List<MatchInterface> ToList();

        /// <summary>
        /// Reincludes the array handed out with ToList, REPLACING the current matches with the ones from the list.
        /// The list might have been reordered, matches might have been removed, or even added.
        /// Elements which were null-ed count as deleted; this gives an O(1) mechanism to remove from the array.
        /// </summary>
        void FromList();
    }
}
