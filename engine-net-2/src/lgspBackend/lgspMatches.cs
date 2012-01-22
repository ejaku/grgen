/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// enumerable returning enumerator over nodes in match
    /// </summary>
    public class Nodes_Enumerable : IEnumerable<INode>
    {
        public Nodes_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<INode> GetEnumerator() { return new Nodes_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new Nodes_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over nodes in match
    /// </summary>
    public class Nodes_Enumerator : IEnumerator<INode>
    {
        public Nodes_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos<match.NumberOfNodes; }
        public INode Current { get { return match.getNodeAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getNodeAt(pos); } } // SCHEISSE
        public void Dispose() { /*empty*/; }
        IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over edges in match
    /// </summary>
    public class Edges_Enumerable : IEnumerable<IEdge>
    {
        public Edges_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<IEdge> GetEnumerator() { return new Edges_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new Edges_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over edges in match
    /// </summary>
    public class Edges_Enumerator : IEnumerator<IEdge>
    {
        public Edges_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos<match.NumberOfEdges; }
        public IEdge Current { get { return match.getEdgeAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getEdgeAt(pos); } } // SCHEISSE
        public void Dispose() { /*empty*/; }
        IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over variables in match
    /// </summary>
    public class Variables_Enumerable : IEnumerable<object>
    {
        public Variables_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<object> GetEnumerator() { return new Variables_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new Variables_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over variables in match
    /// </summary>
    public class Variables_Enumerator : IEnumerator<object>
    {
        public Variables_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos<match.NumberOfVariables; }
        public object Current { get { return match.getVariableAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getVariableAt(pos); } } // SCHEISSE
        public void Dispose() { /*empty*/; }
        IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to subpatterns
    /// </summary>
    public class EmbeddedGraphs_Enumerable : IEnumerable<IMatch>
    {
        public EmbeddedGraphs_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<IMatch> GetEnumerator() { return new EmbeddedGraphs_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new EmbeddedGraphs_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to subpatterns
    /// </summary>
    public class EmbeddedGraphs_Enumerator : IEnumerator<IMatch>
    {
        public EmbeddedGraphs_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos<match.NumberOfEmbeddedGraphs; }
        public IMatch Current { get { return match.getEmbeddedGraphAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getEmbeddedGraphAt(pos); } } // SCHEISSE
        public void Dispose() { /*empty*/; }
        IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to alternatives
    /// </summary>
    public class Alternatives_Enumerable : IEnumerable<IMatch>
    {
        public Alternatives_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<IMatch> GetEnumerator() { return new Alternatives_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new Alternatives_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to alternatives
    /// </summary>
    public class Alternatives_Enumerator : IEnumerator<IMatch>
    {
        public Alternatives_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos<match.NumberOfAlternatives; }
        public IMatch Current { get { return match.getAlternativeAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getAlternativeAt(pos); } } // SCHEISSE
        public void Dispose() { /*empty*/; }
        IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to iterateds,
    /// with every submatch being a list of matches of the iterated-pattern
    /// </summary>
    public class Iterateds_Enumerable : IEnumerable<IMatches>
    {
        public Iterateds_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<IMatches> GetEnumerator() { return new Iterateds_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new Iterateds_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to iterateds,
    /// with every submatch being a list of matches of the iterated-pattern
    /// </summary>
    public class Iterateds_Enumerator : IEnumerator<IMatches>
    {
        public Iterateds_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos < match.NumberOfIterateds; }
        public IMatches Current { get { return match.getIteratedAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getIteratedAt(pos); } } // SCHEISSE
        public void Dispose() { /*empty*/; }
        IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to independents
    /// </summary>
    public class Independents_Enumerable : IEnumerable<IMatch>
    {
        public Independents_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<IMatch> GetEnumerator() { return new Independents_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new Independents_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to independents
    /// </summary>
    public class Independents_Enumerator : IEnumerator<IMatch>
    {
        public Independents_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos < match.NumberOfIndependents; }
        public IMatch Current { get { return match.getIndependentAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getIndependentAt(pos); } } // SCHEISSE
        public void Dispose() { /*empty*/; }
        IMatch match;
        int pos;
    }


    /// <summary>
    /// Element of invasive linked list of T
    /// </summary>
    public class ListElement<T>
    {
        /// <summary>
        /// The next element in the linked list.
        /// </summary>
        public T next;
    }


    /// <summary>
    /// An object representing a (possibly empty) set of matches in a graph before the rewrite has been applied.
    /// It is returned by IAction.Match() and given to the OnMatched, OnFinishing and OnFinished event.
    /// Generic to be instantiated with the exact interface and the exact implementation type of the match object
    /// Every generated Action contains a LGSPMatchesList,
    /// the matches contain one LGSPMatchesList per iterated pattern.
    /// A matches list stores the matches found by the last application of the action,
    /// the matches objects within the list are recycled by the next application of the action,
    /// only their content gets updated.
    /// The purpose of this list is to act as a memory manager
    /// to save new/garbage collection cycles and improve cache footprint.
    /// Additionally this list is used for storing the results of an iteration in the matches objects, Producer being null in this case.
    /// Then it is just used as a container for already allocated elements.
    /// </summary>
    public class LGSPMatchesList<Match, MatchInterface> : IMatchesExact<MatchInterface>
        where Match : ListElement<Match>, MatchInterface, new()
        where MatchInterface : IMatch
    {
        #region IMatchesExact

        /// <summary>
        /// Returns an enumerator over all found matches with exact match interface type
        /// </summary>
        /// <returns></returns>
        public IEnumerator<MatchInterface> GetEnumeratorExact()
        {
            Match cur = root;
            for (int i = 0; i < count; i++, cur = cur.next)
                yield return cur;
        }

        /// <summary>
        /// Returns the first match of exact type (null if no match exists).
        /// </summary>
        public MatchInterface FirstExact { get { return count > 0 ? root : null; } }

        /// <summary>
        /// Returns the match of exact type with the given index. Invalid indices cause an exception.
        /// This may be slow. If you want to iterate over the elements the MatchesExact IEnumerable should be used.
        /// </summary>
        public MatchInterface GetMatchExact(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range: " + index);
            Match cur = root;
            for (int i = 0; i < index; i++, cur = cur.next) ;
            return cur;
        }

        /// <summary>
        /// Removes the match of exact type at the given index and returns it.
        /// </summary>
        public MatchInterface RemoveMatchExact(int index)
        {
            if(index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range: " + index);
            Match cur = root, prev = null;
            for(int i = 0; i < index; i++, prev = cur, cur = cur.next) ;
            if(prev == null) root = cur.next;
            else prev.next = cur.next;
            cur.next = last.next;
            last.next = cur;
            count--;
            return cur;
        }

        /// <summary>
        /// Returns an enumerator over all found matches with inexact match interface type.
        /// </summary>
        public IEnumerator<IMatch> GetEnumerator()
        {
            Match cur = root;
            for (int i = 0; i < count; i++, cur = cur.next)
                yield return cur;
        }

        /// <summary>
        /// Returns a non-generic enumerator over all found matches.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// The action object used to generate this LGSPMatchesList object
        /// </summary>
        public IAction Producer { get { return producer; } }

        /// <summary>
        /// The number of matches in this list.
        /// </summary>
        public int Count { get { return count; } }

        /// <summary>
        /// Returns the match with the given index. Invalid indices cause an exception.
        /// This may be slow. If you want to iterate over the elements the Matches IEnumerable should be used.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when index is invalid.</exception>
        public IMatch GetMatch(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Removes the match at the given index and returns it.
        /// </summary>
        /// <param name="index">The index of the match to be removed.</param>
        /// <returns>The removed match.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when index is invalid.</exception>
        public IMatch RemoveMatch(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range: " + index);
            Match cur = root, prev = null;
            for (int i = 0; i < index; i++, prev = cur, cur = cur.next) ;
            if (prev == null) root = cur.next;
            else prev.next = cur.next;
            cur.next = last.next;
            last.next = cur;
            count--;
            return cur;
        }

        /// <summary>
        /// Clone the matches
        /// </summary>
        public IMatches Clone()
        {
            return new LGSPMatchesList<Match, MatchInterface>(this);
        }

        #endregion

        /// <summary>
        /// Constructs a new LGSPMatchesList instance.
        /// </summary>
        /// <param name="producer">The action object used to generate this LGSPMatchesList object; null if this is the matches list of an iteration</param>
        public LGSPMatchesList(IAction producer)
        {
            if (producer != null) {
                this.producer = producer;
                last = root = new Match();
            }
        }

        protected LGSPMatchesList(LGSPMatchesList<Match, MatchInterface> that)
        {
            producer = that.producer;
            count = that.count;
            Match curThat = that.root;
            Match cur = root = (Match)curThat.Clone();
            for(int i = 1; i < that.count; i++, curThat = curThat.next)
            {
                cur.next = (Match)curThat.Clone();
                cur = cur.next;
            }
            last = cur;
        }

        /// <summary>
        /// returns an empty match object from the matches list
        /// to be filled by the matching action with the found nodes, edges and subpatterns.
        /// unless PositionWasFilledFixIt is called you always get the same element
        /// </summary>
        public Match GetNextUnfilledPosition()
        {
            Debug.Assert(producer != null);
            return last;
        }

        /// <summary>
        /// the match object returned by GetNextUnfilledPosition was filled,
        /// now fix it within the list, so that the next call to GetNextUnfilledPosition returns a new element
        /// </summary>
        public void PositionWasFilledFixIt()
        {
            Debug.Assert(producer != null);
            count++;
            if (last.next == null)
                last.next = new Match();
            last = last.next;
        }

        /// <summary>
        /// adds a match object to the end of the list; only applicable if this is the match of an iteration, not an action
        /// </summary>
        public void Add(Match match)
        {
            Debug.Assert(producer == null);
            Debug.Assert(match != null);
            if (root == null) {
                last = root = match;
            } else {
                last.next = match;
                last = match;
            }
            ++count;
        }

        /// <summary>
        /// The first match of this list.
        /// </summary>
        public IMatch First { get { return count > 0 ? root : null; } }

        /// <summary>
        /// The root element of the list.
        /// </summary>
        public Match Root { get { return root; } }

        /// <summary>
        /// remove all filled and committed elements from the list
        /// </summary>
        public void Clear()
        {
            Debug.Assert(producer != null);
            count = 0;
            last = root;
        }

        /// <summary>
        /// Returns the match with the given index.
        /// This may be slow. If you want to iterate over the elements the Matches IEnumerable should be used.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when index is invalid.</exception>
        public Match this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Index out of range: " + index);
                Match cur = root;
                for (int i = 0; i < index; i++, cur = cur.next) ;
                return cur;
            }
        }


        /// <summary>
        /// the action object used to generate this LGSPMatchesList object
        /// </summary>
        public IAction producer;

        /// <summary>
        /// head of list
        /// </summary>
        private Match root;

        /// <summary>
        /// logically last element of list, not necessarily physically the last element
        /// as previously generated matches are kept and recycled
        /// denotes the next point of logical insertion i.e. physical update
        /// </summary>
        private Match last;

        /// <summary>
        /// number of found matches in the list
        /// </summary>
        private int count;
    }
}
