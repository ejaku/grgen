/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

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
        public Nodes_Enumerable(IMatch match)
        {
            this.match = match;
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return new Nodes_Enumerator(match);
        } // KRANKE

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Nodes_Enumerator(match);
        } // SCHEISSE

        readonly IMatch match;
    }

    /// <summary>
    /// enumerator over nodes in match
    /// </summary>
    public class Nodes_Enumerator : IEnumerator<INode>
    {
        public Nodes_Enumerator(IMatch match)
        {
            this.match = match;
            pos = -1;
        }

        public void Reset()
        {
            pos = -1;
        }

        public bool MoveNext()
        {
            ++pos;
            return pos<match.NumberOfNodes;
        }

        public INode Current
        {
            get { return match.getNodeAt(pos); }
        } // KRANKE

        object IEnumerator.Current
        {
            get { return match.getNodeAt(pos); }
        } // SCHEISSE

        public void Dispose()
        {
            /*empty*/;
        }

        readonly IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over edges in match
    /// </summary>
    public class Edges_Enumerable : IEnumerable<IEdge>
    {
        public Edges_Enumerable(IMatch match)
        {
            this.match = match;
        }

        public IEnumerator<IEdge> GetEnumerator()
        {
            return new Edges_Enumerator(match);
        } // KRANKE

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Edges_Enumerator(match);
        } // SCHEISSE

        readonly IMatch match;
    }

    /// <summary>
    /// enumerator over edges in match
    /// </summary>
    public class Edges_Enumerator : IEnumerator<IEdge>
    {
        public Edges_Enumerator(IMatch match)
        {
            this.match = match;
            pos = -1;
        }

        public void Reset()
        {
            pos = -1;
        }

        public bool MoveNext()
        {
            ++pos;
            return pos<match.NumberOfEdges;
        }

        public IEdge Current
        {
            get { return match.getEdgeAt(pos); }
        } // KRANKE

        object IEnumerator.Current
        {
            get { return match.getEdgeAt(pos); }
        } // SCHEISSE

        public void Dispose()
        { 
            /*empty*/;
        }

        readonly IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over variables in match
    /// </summary>
    public class Variables_Enumerable : IEnumerable<object>
    {
        public Variables_Enumerable(IMatch match)
        {
            this.match = match;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return new Variables_Enumerator(match);
        } // KRANKE

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Variables_Enumerator(match);
        } // SCHEISSE

        readonly IMatch match;
    }

    /// <summary>
    /// enumerator over variables in match
    /// </summary>
    public class Variables_Enumerator : IEnumerator<object>
    {
        public Variables_Enumerator(IMatch match)
        {
            this.match = match;
            pos = -1;
        }

        public void Reset()
        {
            pos = -1;
        }

        public bool MoveNext()
        {
            ++pos;
            return pos<match.NumberOfVariables;
        }

        public object Current
        {
            get { return match.getVariableAt(pos); }
        } // KRANKE

        object IEnumerator.Current
        {
            get { return match.getVariableAt(pos); }
        } // SCHEISSE

        public void Dispose()
        { 
            /*empty*/;
        }

        readonly IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to subpatterns
    /// </summary>
    public class EmbeddedGraphs_Enumerable : IEnumerable<IMatch>
    {
        public EmbeddedGraphs_Enumerable(IMatch match)
        {
            this.match = match;
        }

        public IEnumerator<IMatch> GetEnumerator()
        {
            return new EmbeddedGraphs_Enumerator(match);
        } // KRANKE

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EmbeddedGraphs_Enumerator(match);
        } // SCHEISSE

        readonly IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to subpatterns
    /// </summary>
    public class EmbeddedGraphs_Enumerator : IEnumerator<IMatch>
    {
        public EmbeddedGraphs_Enumerator(IMatch match)
        {
            this.match = match;
            pos = -1;
        }

        public void Reset()
        {
            pos = -1;
        }

        public bool MoveNext()
        {
            ++pos;
            return pos<match.NumberOfEmbeddedGraphs;
        }

        public IMatch Current
        {
            get { return match.getEmbeddedGraphAt(pos); }
        } // KRANKE

        object IEnumerator.Current
        {
            get { return match.getEmbeddedGraphAt(pos); }
        } // SCHEISSE

        public void Dispose()
        {
            /*empty*/;
        }

        readonly IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to alternatives
    /// </summary>
    public class Alternatives_Enumerable : IEnumerable<IMatch>
    {
        public Alternatives_Enumerable(IMatch match)
        {
            this.match = match;
        }

        public IEnumerator<IMatch> GetEnumerator()
        {
            return new Alternatives_Enumerator(match);
        } // KRANKE

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Alternatives_Enumerator(match);
        } // SCHEISSE

        readonly IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to alternatives
    /// </summary>
    public class Alternatives_Enumerator : IEnumerator<IMatch>
    {
        public Alternatives_Enumerator(IMatch match)
        {
            this.match = match;
            pos = -1;
        }

        public void Reset()
        {
            pos = -1;
        }

        public bool MoveNext()
        {
            ++pos;
            return pos<match.NumberOfAlternatives;
        }

        public IMatch Current
        {
            get { return match.getAlternativeAt(pos); }
        } // KRANKE

        object IEnumerator.Current
        {
            get { return match.getAlternativeAt(pos); }
        } // SCHEISSE

        public void Dispose()
        {
            /*empty*/;
        }

        readonly IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to iterateds,
    /// with every submatch being a list of matches of the iterated-pattern
    /// </summary>
    public class Iterateds_Enumerable : IEnumerable<IMatches>
    {
        public Iterateds_Enumerable(IMatch match)
        {
            this.match = match;
        }

        public IEnumerator<IMatches> GetEnumerator()
        {
            return new Iterateds_Enumerator(match);
        } // KRANKE

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Iterateds_Enumerator(match);
        } // SCHEISSE

        readonly IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to iterateds,
    /// with every submatch being a list of matches of the iterated-pattern
    /// </summary>
    public class Iterateds_Enumerator : IEnumerator<IMatches>
    {
        public Iterateds_Enumerator(IMatch match)
        {
            this.match = match;
            pos = -1;
        }

        public void Reset()
        {
            pos = -1;
        }

        public bool MoveNext()
        {
            ++pos;
            return pos < match.NumberOfIterateds;
        }

        public IMatches Current
        {
            get { return match.getIteratedAt(pos); }
        } // KRANKE

        object IEnumerator.Current
        {
            get { return match.getIteratedAt(pos); }
        } // SCHEISSE

        public void Dispose()
        {
            /*empty*/;
        }

        readonly IMatch match;
        int pos;
    }


    /// <summary>
    /// enumerable returning enumerator over submatches due to independents
    /// </summary>
    public class Independents_Enumerable : IEnumerable<IMatch>
    {
        public Independents_Enumerable(IMatch match)
        {
            this.match = match;
        }

        public IEnumerator<IMatch> GetEnumerator()
        {
            return new Independents_Enumerator(match);
        } // KRANKE

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Independents_Enumerator(match);
        } // SCHEISSE

        readonly IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to independents
    /// </summary>
    public class Independents_Enumerator : IEnumerator<IMatch>
    {
        public Independents_Enumerator(IMatch match)
        {
            this.match = match;
            pos = -1;
        }

        public void Reset()
        {
            pos = -1;
        }

        public bool MoveNext()
        {
            ++pos;
            return pos < match.NumberOfIndependents;
        }

        public IMatch Current
        {
            get { return match.getIndependentAt(pos); }
        } // KRANKE

        object IEnumerator.Current
        {
            get { return match.getIndependentAt(pos); }
        } // SCHEISSE

        public void Dispose()
        {
            /*empty*/;
        }

        readonly IMatch match;
        int pos;
    }


    /// <summary>
    /// Element of invasively linked list of T, and Match
    /// </summary>
    public abstract class MatchListElement<T> : IMatch
    {
        /// <summary>
        /// The next element in the linked list.
        /// </summary>
        public T next;

        public IMatch _matchOfEnclosingPattern;
        public bool _flag;
        public int _iterationNumber;

        ///////////////////////////////////////////////////////////////

        public abstract IPatternGraph Pattern { get; }
        public abstract IMatchClass MatchClass { get; }
        public IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
        public abstract IMatch Clone();
        public abstract IMatch Clone(IDictionary<IGraphElement, IGraphElement> oldToNewMap);

        public void Mark(bool flag) { _flag = flag; }
        public bool IsMarked() { return _flag; }
        public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

        public abstract IEnumerable<INode> Nodes { get; }
        public abstract IEnumerator<INode> NodesEnumerator { get; }
        public abstract int NumberOfNodes { get; }
        public abstract INode getNodeAt(int index);
        public abstract INode getNode(string name);
        public abstract void SetNode(string name, INode node);

        public abstract IEnumerable<IEdge> Edges { get; }
        public abstract IEnumerator<IEdge> EdgesEnumerator { get; }
        public abstract int NumberOfEdges { get; }
        public abstract IEdge getEdgeAt(int index);
        public abstract IEdge getEdge(string name);
        public abstract void SetEdge(string name, IEdge edge);

        public abstract IEnumerable<object> Variables { get; }
        public abstract IEnumerator<object> VariablesEnumerator { get; }
        public abstract int NumberOfVariables { get; }
        public abstract object getVariableAt(int index);
        public abstract object getVariable(string name);
        public abstract void SetVariable(string name, object value);

        public abstract IEnumerable<IMatch> EmbeddedGraphs { get; }
        public abstract IEnumerator<IMatch> EmbeddedGraphsEnumerator { get; }
        public abstract int NumberOfEmbeddedGraphs { get; }
        public abstract IMatch getEmbeddedGraphAt(int index);
        public abstract IMatch getEmbeddedGraph(string name);

        public abstract IEnumerable<IMatch> Alternatives { get; }
        public abstract IEnumerator<IMatch> AlternativesEnumerator { get; }
        public abstract int NumberOfAlternatives { get; }
        public abstract IMatch getAlternativeAt(int index);
        public abstract IMatch getAlternative(string name);

        public abstract IEnumerable<IMatches> Iterateds { get; }
        public abstract IEnumerator<IMatches> IteratedsEnumerator { get; }
        public abstract int NumberOfIterateds { get; }
        public abstract IMatches getIteratedAt(int index);
        public abstract IMatches getIterated(string name);

        public abstract IEnumerable<IMatch> Independents { get; }
        public abstract IEnumerator<IMatch> IndependentsEnumerator { get; }
        public abstract int NumberOfIndependents { get; }
        public abstract IMatch getIndependentAt(int index);
        public abstract IMatch getIndependent(string name);

        ///////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns value bound to the member of the given name or null if no such member exists
        /// </summary>
        public object GetMember(string name)
        {
            INode node = getNode(name);
            if(node != null)
                return node;
            IEdge edge = getEdge(name);
            if(edge != null)
                return edge;
            return getVariable(name);
        }

        public void SetMember(string name, object value)
        {
            IPatternElement patternElement;
            if(Pattern != null)
                patternElement = Pattern.GetPatternElement(name);
            else
                patternElement = MatchClass.GetPatternElement(name);
            if(patternElement is IPatternNode)
                SetNode(name, (INode)value);
            else if(patternElement is IPatternEdge)
                SetEdge(name, (IEdge)value);
            else //patternElement is IPatternVariable
                SetVariable(name, value);
        }

        public override string ToString()
        {
            return "Match of " + Pattern.Name;
        }
    }


    /// <summary>
    /// An object representing a (possibly empty) set of matches in a graph before the rewrite has been applied.
    /// It is returned by IAction.Match() and given to the OnMatched, OnPreMatched, OnFinishing and OnFinished event.
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
        where Match : MatchListElement<Match>, MatchInterface, new()
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
            for(int i = 0; i < count; ++i, cur = cur.next)
            {
                yield return cur;
            }
        }

        /// <summary>
        /// Returns the first match of exact type (null if no match exists).
        /// </summary>
        public MatchInterface FirstExact
        {
            get { return count > 0 ? root : null; }
        }

        /// <summary>
        /// Returns the first match of exact type (null if no match exists).
        /// </summary>
        public Match FirstImplementation
        {
            get { return count > 0 ? root : null; }
        }

        /// <summary>
        /// Returns the match of exact type with the given index. Invalid indices cause an exception.
        /// This may be slow. If you want to iterate over the elements the MatchesExact IEnumerable should be used.
        /// </summary>
        public MatchInterface GetMatchExact(int index)
        {
            if(index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range: " + index);
            Match cur = root;
            for(int i = 0; i < index; ++i, cur = cur.next)
            {
            }
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
            for(int i = 0; i < index; ++i, prev = cur, cur = cur.next)
            {
            }
            if(prev == null)
                root = cur.next;
            else
                prev.next = cur.next;
            cur.next = last.next;
            last.next = cur;
            --count;
            return cur;
        }

        /// <summary>
        /// Returns an enumerator over all found matches with inexact match interface type.
        /// </summary>
        public IEnumerator<IMatch> GetEnumerator()
        {
            Match cur = root;
            for(int i = 0; i < count; ++i, cur = cur.next)
            {
                yield return cur;
            }
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
        public IAction Producer
        {
            get { return producer; }
        }

        /// <summary>
        /// The number of matches in this list.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

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
            if(index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index out of range: " + index);
            Match cur = root, prev = null;
            for(int i = 0; i < index; ++i, prev = cur, cur = cur.next)
            {
            }
            if(prev == null)
                root = cur.next;
            else
                prev.next = cur.next;
            cur.next = last.next;
            last.next = cur;
            --count;
            return cur;
        }

        // removes the matches that are not available in the matchSet
        public void RemoveUnavailable(Dictionary<IMatch, SetValueType> matchSet)
        {
            while(count > 0 && !matchSet.ContainsKey(root))
            {
                root = root.next;
                --count;
            }
            if(count == 0)
                return;
            Match prev = root;
            Match cur = root.next;
            while(cur != null)
            {
                if(!matchSet.ContainsKey(cur))
                {
                    prev.next = cur.next;
                    cur = cur.next;
                    --count;
                    continue;
                }
                if(cur == last)
                    break;
                prev = cur;
                cur = cur.next;
            }
        }

        /// <summary>
        /// Clone the matches
        /// </summary>
        public IMatches Clone()
        {
            return new LGSPMatchesList<Match, MatchInterface>(this);
        }

        /// <summary>
        /// Clones the matches.
        /// </summary>
        /// <param name="originalToClone">Receives mapping of original to cloned matches</param>
        public IMatches Clone(IDictionary<IMatch, IMatch> originalToClone)
        {
            return new LGSPMatchesList<Match, MatchInterface>(this, originalToClone);
        }

        #endregion

        /// <summary>
        /// Constructs a new LGSPMatchesList instance.
        /// </summary>
        /// <param name="producer">The action object used to generate this LGSPMatchesList object; null if this is the matches list of an iteration</param>
        public LGSPMatchesList(IAction producer)
        {
            if(producer != null)
            {
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
            curThat = curThat.next;
            for(int i = 1; i < that.count; i++, curThat = curThat.next)
            {
                cur.next = (Match)curThat.Clone();
                cur = cur.next;
            }
            last = cur;
        }

        public LGSPMatchesList(LGSPMatchesList<Match, MatchInterface> that, IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            producer = that.producer;
            count = that.count;
            Match curThat = that.root;
            Match cur = root = (Match)curThat.Clone(oldToNewMap);
            curThat = curThat.next;
            for(int i = 1; i < that.count; i++, curThat = curThat.next)
            {
                cur.next = (Match)curThat.Clone(oldToNewMap);
                cur = cur.next;
            }
            last = cur;
        }

        protected LGSPMatchesList(LGSPMatchesList<Match, MatchInterface> that, IDictionary<IMatch, IMatch> originalToClone)
        {
            producer = that.producer;
            count = that.count;
            Match curThat = that.root;
            Match cur = root = (Match)curThat.Clone();
            originalToClone.Add(curThat, cur);
            curThat = curThat.next;
            for(int i = 1; i < that.count; i++, curThat = curThat.next)
            {
                cur.next = (Match)curThat.Clone();
                originalToClone.Add(curThat, cur.next);
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
            ++count;
            if(last.next == null)
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
            if(root == null)
                last = root = match;
            else
            {
                last.next = match;
                last = match;
            }
            ++count;
        }

        /// <summary>
        /// removes the first match object from the the list
        /// </summary>
        public void RemoveFirst()
        {
            Match prevRoot = root;
            root = root.next;
            Match prevLastNext = last.next;
            last.next = prevRoot;
            prevRoot.next = prevLastNext;
            --count;
        }

        /// <summary>
        /// The first match of this list.
        /// </summary>
        public IMatch First
        {
            get { return count > 0 ? root : null; }
        }

        /// <summary>
        /// The root element of the list.
        /// </summary>
        public Match Root
        {
            get { return root; }
        }

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
                if(index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Index out of range: " + index);
                Match cur = root;
                for(int i = 0; i < index; ++i, cur = cur.next)
                {
                }
                return cur;
            }
        }

        /// <summary>
        /// Returns a copy of the content of the current matches list in form of an array.
        /// Attention: matches may get stale when the rule is matched again.
        /// This is only a convenience helper method, unrelated to ToList, and its pairing with FromList, as well as ToListExact, and its pairing with FromListExact.
        /// </summary>
        public List<IMatch> ToListCopy()
        {
            List<IMatch> array = new List<IMatch>(Count);
            Match cur = root;
            for(int i = 0; i < count; i++, cur = cur.next)
            {
                array.Add(cur);
            }
            return array;
        }

        /// <summary>
        /// Returns the content of the current matches list in form of an array which can be efficiently indexed and reordered.
        /// The array is destroyed when this method is called again, the content is destroyed when the rule is matched again (there is only one array existing).
        /// </summary>
        public List<IMatch> ToList()
        {
            array.Clear();
            Match cur = root;
            for(int i = 0; i < count; i++, cur = cur.next)
            {
                array.Add(cur);
            }
            return array;
        }

        /// <summary>
        /// Reincludes the array handed out with ToList, REPLACING the current matches with the ones from the list.
        /// The list might have been reordered, matches might have been removed, or even added.
        /// Elements which were null-ed count as deleted; this gives an O(1) mechanism to remove from the array.
        /// </summary>
        public void FromList()
        {
            // forget about the matches currently stored in the matches list which were handed out in ToList, keeping the "free tail" remaining
            for(int i = 0; i < count; ++i)
            {
                root = root.next;
            }
            count = 0;

            // that's it if the array is empty
            if(array.Count == 0)
                return;
            int startIndex = 0;
            for(; startIndex < array.Count; ++startIndex) // fast forward to first non-null entry
            {
                if(array[startIndex] != null)
                    break;
            }
            if(startIndex >= array.Count) // only null-entries were in array
                return;

            // prepend the matches stored in the array handed out

            // employ the first non-null entry in the array as new list head
            Match oldRoot = root;
            root = (Match)array[startIndex];
            ++count;
            Match cur = root;
            // add all further, non-null entries to list
            for(int i = startIndex + 1; i < array.Count; ++i)
            {
                if(array[i] == null)
                    continue;
                cur.next = (Match)array[i];
                cur = cur.next;
                ++count;
            }

            // append the free tail remaining to new current list
            cur.next = oldRoot;
        }

        /// <summary>
        /// Returns the content of the current matches list in form of an array which can be efficiently indexed and reordered.
        /// The array is destroyed when this method is called again, the content is destroyed when the rule is matched again (there is only one array existing).
        /// </summary>
        public List<MatchInterface> ToListExact()
        {
            arrayExact.Clear();
            Match cur = root;
            for(int i = 0; i < count; i++, cur = cur.next)
            {
                arrayExact.Add(cur);
            }
            return arrayExact;
        }

        /// <summary>
        /// Reincludes the array handed out with ToListExact, REPLACING the current matches with the ones from the list.
        /// The list might have been reordered, matches might have been removed, or even added.
        /// Elements which were null-ed count as deleted; this gives an O(1) mechanism to remove from the array.
        /// </summary>
        public void FromListExact()
        {
            // forget about the matches currently stored in the matches list which were handed out in ToList, keeping the "free tail" remaining
            for(int i = 0; i < count; ++i)
            {
                root = root.next;
            }
            count = 0;

            // that's it if the array is empty
            if(arrayExact.Count == 0)
                return;
            int startIndex = 0;
            for(; startIndex < arrayExact.Count; ++startIndex) // fast forward to first non-null entry
            {
                if(arrayExact[startIndex] != null)
                    break;
            }
            if(startIndex >= arrayExact.Count) // only null-entries were in array
                return;

            // prepend the matches stored in the array handed out

            // employ the first non-null entry in the array as new list head
            Match oldRoot = root;
            root = (Match)arrayExact[startIndex];
            ++count;
            Match cur = root;
            // add all further, non-null entries to list
            for(int i = startIndex + 1; i < arrayExact.Count; ++i)
            {
                if(arrayExact[i] == null)
                    continue;
                cur.next = (Match)arrayExact[i];
                cur = cur.next;
                ++count;
            }

            // append the free tail remaining to new current list
            cur.next = oldRoot;
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepFirstFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_keepFirstFraction(double fraction)
        {
            return FilterExact_keepFirstFraction(fraction);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepLastFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_keepLastFraction(double fraction)
        {
            return FilterExact_keepLastFraction(fraction);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepFirst
        /// </summary>
        /// <param name="count">The number of matches to keep</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_keepFirst(int count)
        {
            return FilterExact_keepFirst(count);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepLast
        /// </summary>
        /// <param name="count">The number of matches to keep</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_keepLast(int count)
        {
            return FilterExact_keepLast(count);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeFirstFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to remove</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_removeFirstFraction(double fraction)
        {
            return FilterExact_removeFirstFraction(fraction);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeLastFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to remove</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_removeLastFraction(double fraction)
        {
            return FilterExact_removeLastFraction(fraction);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeFirst
        /// </summary>
        /// <param name="count">The number of matches to remove</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_removeFirst(int count)
        {
            return FilterExact_removeFirst(count);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeLast
        /// </summary>
        /// <param name="count">The number of matches to remove</param>
        /// <returns>The changed matches list.</returns>
        public IMatches Filter_removeLast(int count)
        {
            return FilterExact_removeLast(count);
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepFirstFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_keepFirstFraction(double fraction)
        {
            return FilterExact_keepFirst((int)Math.Ceiling(fraction * count));
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepLastFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to keep</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_keepLastFraction(double fraction)
        {
            return FilterExact_keepLast((int)Math.Ceiling(fraction * count));
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepFirst
        /// </summary>
        /// <param name="count">The number of matches to keep</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_keepFirst(int count)
        {
            List<MatchInterface> matchesArray = ToListExact();
            for(int i = count; i < matchesArray.Count; ++i)
            {
                matchesArray[i] = default(MatchInterface); // = null
            }
            FromListExact();
            return this;
        }

        /// <summary>
        /// For filtering with the auto-supplied filter keepLast
        /// </summary>
        /// <param name="count">The number of matches to keep</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_keepLast(int count)
        {
            List<MatchInterface> matchesArray = ToListExact();
            for(int i = matchesArray.Count-1 - count; i >= 0; --i)
            {
                matchesArray[i] = default(MatchInterface); // = null
            }
            FromListExact();
            return this;
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeFirstFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to remove</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_removeFirstFraction(double fraction)
        {
            return FilterExact_removeFirst((int)Math.Ceiling(fraction * count));
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeLastFraction
        /// </summary>
        /// <param name="fraction">The fraction of matches to remove</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_removeLastFraction(double fraction)
        {
            return FilterExact_removeLast((int)Math.Ceiling(fraction * count));
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeFirst
        /// </summary>
        /// <param name="count">The number of matches to remove</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_removeFirst(int count)
        {
            List<MatchInterface> matchesArray = ToListExact();
            for(int i = 0; i < Math.Min(count, matchesArray.Count); ++i)
            {
                matchesArray[i] = default(MatchInterface); // = null
            }
            FromListExact();
            return this;
        }

        /// <summary>
        /// For filtering with the auto-supplied filter removeLast
        /// </summary>
        /// <param name="count">The number of matches to remove</param>
        /// <returns>The changed matches list of exact type.</returns>
        public IMatchesExact<MatchInterface> FilterExact_removeLast(int count)
        {
            List<MatchInterface> matchesArray = ToListExact();
            for(int i = matchesArray.Count - 1; i > Math.Max(matchesArray.Count - 1 - count, 0); --i)
            {
                matchesArray[i] = default(MatchInterface); // = null
            }
            FromListExact();
            return this;
        }

        /// <summary>
        /// the action object used to generate this LGSPMatchesList object
        /// </summary>
        public readonly IAction producer;

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

        /// <summary>
        /// the array returned in a call of ToList
        /// </summary>
        private List<IMatch> array = new List<IMatch>();

        /// <summary>
        /// the array returned in a call of ToListExact
        /// </summary>
        private List<MatchInterface> arrayExact = new List<MatchInterface>();
    }
}
