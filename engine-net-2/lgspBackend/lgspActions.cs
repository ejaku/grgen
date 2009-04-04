/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

//#define ASSERT_ALL_UNMAPPED_AFTER_MATCH

using System;
using System.Collections;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.IO;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Represents a matcher method.
    /// </summary>
    /// <param name="graph">The host graph.</param>
    /// <param name="maxMatches">The maximum number of matches to be searched for, or zero for an unlimited search.</param>
    /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
    /// The array must contain the correct number of elements.</param>
    /// <returns>An IMatches object containing the found matches.</returns>
    public delegate IMatches MatchInvoker(LGSPGraph graph, int maxMatches, object[] parameters);

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
    /// enumerable returning enumerator over submatches due to alls,
    /// with every submatch being a list of matches of the all-pattern
    /// </summary>
    public class Alls_Enumerable : IEnumerable<IMatches>
    {
        public Alls_Enumerable(IMatch match) { this.match = match; }
        public IEnumerator<IMatches> GetEnumerator() { return new Alls_Enumerator(match); } // KRANKE
        IEnumerator IEnumerable.GetEnumerator() { return new Alls_Enumerator(match); } // SCHEISSE
        IMatch match;
    }

    /// <summary>
    /// enumerator over submatches due to alls,
    /// with every submatch being a list of matches of the all-pattern
    /// </summary>
    public class Alls_Enumerator : IEnumerator<IMatches>
    {
        public Alls_Enumerator(IMatch match) { this.match = match; pos = -1; }
        public void Reset() { pos = -1; }
        public bool MoveNext() { ++pos; return pos < match.NumberOfAlls; }
        public IMatches Current { get { return match.getAllAt(pos); } } // KRANKE
        object IEnumerator.Current { get { return match.getAllAt(pos); } } // SCHEISSE
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
    /// the matches contain one LGSPMatchesList per all pattern.
    /// A matches list stores the matches found by the last application of the action,
    /// the matches objects within the list are recycled by the next application of the action,
    /// only their content gets updated.
    /// The purpose of this list is to act as a memory manager 
    /// to save new/garbage collection cycles and improve cache footprint.
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
            Match cur = root, last = null;
            for (int i = 0; i < index; i++, last = cur, cur = cur.next) ;
            if (last == null) root = cur.next;
            else last.next = cur.next;
            count--;
            return cur;
        }

        #endregion

        /// <summary>
        /// Constructs a new LGSPMatchesList instance.
        /// </summary>
        /// <param name="producer">The action object used to generate this LGSPMatchesList object</param>
        public LGSPMatchesList(LGSPAction producer)
        {
            this.producer = producer;
            last = root = new Match();
        }

        /// <summary>
        /// returns an empty match object from the matches list 
        /// to be filled by the matching action with the found nodes, edges and subpatterns.
        /// unless PositionWasFilledFixIt is called you always get the same element
        /// </summary>
        public Match GetNextUnfilledPosition()
        {
            return last;
        }

        /// <summary>
        /// the match object returned by GetNextUnfilledPosition was filled,
        /// now fix it within the list, so that the next call to GetNextUnfilledPosition returns a new element
        /// </summary>
        public void PositionWasFilledFixIt()
        {
            count++;
            if (last.next == null)
                last.next = new Match();
            last = last.next;
        }

        /// <summary>
        /// The first match of this list.
        /// </summary>
        public IMatch First { get { return count > 0 ? root : null; } }

        /// <summary>
        /// remove all filled and committed elements from the list
        /// </summary>
        public void Clear()
        {
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
        public LGSPAction producer;

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

    /// <summary>
    /// An object representing an executable rule of the LGSPBackend.
    /// </summary>
    public abstract class LGSPAction : IAction
    {
        /// <summary>
        /// The name of the action (without prefixes)
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The LGSPRulePattern object from which this LGSPAction object has been created.
        /// </summary>
        public LGSPRulePattern rulePattern;

        /// <summary>
        /// The PatternGraph object of the main graph
        /// </summary>
        public PatternGraph patternGraph;

        /// <summary>
        /// The RulePattern object from which this LGSPAction object has been created.
        /// </summary>
        public IRulePattern RulePattern { get { return rulePattern; } }

        /// <summary>
        /// A delegate pointing to the current matcher program for this rule.
        /// </summary>
        public MatchInvoker DynamicMatch;

        /// <summary>
        /// Searches for a graph pattern as specified by RulePattern.
        /// </summary>
        /// <param name="graph">The host graph.</param>
        /// <param name="maxMatches">The maximum number of matches to be searched for, or zero for an unlimited search.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>An IMatches object containing the found matches.</returns>
        public IMatches Match(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            return DynamicMatch(graph, maxMatches, parameters);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// </summary>
        /// <returns>An array of objects returned by the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        public object[] Modify(LGSPGraph graph, IMatch match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, match);
            else
                return rulePattern.ModifyNoReuse(graph, match);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with all of the given matches.
        /// No OnRewritingNextMatch events are triggered by this function.
        /// </summary>
        /// <returns>An array of objects returned by the last applicance of the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        public object[] ModifyAll(LGSPGraph graph, IMatches matches)
        {
            object[] retElems = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
            {
                foreach(IMatch match in matches)
                    retElems = rulePattern.Modify(graph, match);
            }
            else
            {
                foreach(IMatch match in matches)
                    retElems = rulePattern.ModifyNoReuse(graph, match);
            }
            return retElems;
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// The rule must not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        public object[] Apply(LGSPGraph graph)
        {
            IMatches matches = DynamicMatch(graph, 1, null);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, matches.First);
            else
                return rulePattern.ModifyNoReuse(graph, matches.First);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        public object[] Apply(LGSPGraph graph, params object[] parameters)
        {
            IMatches matches = DynamicMatch(graph, 1, parameters);
            if(matches.Count <= 0) return null;

            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, matches.First);
            else
                return rulePattern.ModifyNoReuse(graph, matches.First);
        }

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten or 0 for no limit.</param>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        public object[] ApplyAll(int maxMatches, LGSPGraph graph)
        {
            IMatches matches = DynamicMatch(graph, maxMatches, null);
            object[] retElems = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
            {
                foreach(IMatch match in matches)
                    retElems = rulePattern.Modify(graph, match);
            }
            else
            {
                foreach(IMatch match in matches)
                    retElems = rulePattern.ModifyNoReuse(graph, match);
            }
            return retElems;
        }

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten.</param>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        public object[] ApplyAll(int maxMatches, LGSPGraph graph, params object[] parameters)
        {
            IMatches matches = DynamicMatch(graph, maxMatches, parameters);
            object[] retElems = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
            {
                foreach(IMatch match in matches)
                    retElems = rulePattern.Modify(graph, match);
            }
            else
            {
                foreach(IMatch match in matches)
                    retElems = rulePattern.ModifyNoReuse(graph, match);
            }
            return retElems;
        }


        #region IAction Members

        /// <summary>
        /// Searches for a graph pattern as specified by RulePattern.
        /// </summary>
        /// <param name="graph">The host graph.</param>
        /// <param name="maxMatches">The maximum number of matches to be searched for, or zero for an unlimited search.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>An IMatches object containing the found matches.</returns>
        IMatches IAction.Match(IGraph graph, int maxMatches, object[] parameters)
        {
            return DynamicMatch((LGSPGraph) graph, maxMatches, parameters);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <returns>An array of objects returned by the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        object[] IAction.Modify(IGraph graph, IMatch match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, match);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, match);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with all of the given matches.
        /// No OnRewritingNextMatch events are triggered by this function.
        /// </summary>
        /// <returns>An array of objects returned by the last applicance of the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        object[] IAction.ModifyAll(IGraph graph, IMatches matches)
        {
            return ModifyAll((LGSPGraph) graph, matches);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] IAction.Apply(IGraph graph)
        {
            IMatches matches = DynamicMatch((LGSPGraph) graph, 1, null);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, matches.First);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, matches.First);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] IAction.Apply(IGraph graph, params object[] parameters)
        {
            IMatches matches = DynamicMatch((LGSPGraph) graph, 1, parameters);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, matches.First);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, matches.First);
        }

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten or 0 for no limit.</param>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] IAction.ApplyAll(int maxMatches, IGraph graph)
        {
            return ApplyAll(maxMatches, (LGSPGraph) graph);
        }

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten or 0 for no limit.</param>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] IAction.ApplyAll(int maxMatches, IGraph graph, params object[] parameters)
        {
            return ApplyAll(maxMatches, (LGSPGraph) graph, parameters);
        }

        #endregion

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>Always returns true.</returns>
        public bool ApplyStar(IGraph graph)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            IMatches matches;
            while(true)
            {
                matches = DynamicMatch(lgraph, 1, null);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.First);
            }
        }

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>Always returns true.</returns>
        public bool ApplyStar(IGraph graph, params object[] parameters)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            IMatches matches;
            while(true)
            {
                matches = DynamicMatch(lgraph, 1, parameters);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.First);
            }
        }

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        public bool ApplyPlus(IGraph graph)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            IMatches matches = DynamicMatch(lgraph, 1, null);
            if(matches.Count <= 0) return false;
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.First);
                matches = DynamicMatch(lgraph, 1, null);
            }
            while(matches.Count > 0);
            return true;
        }

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        public bool ApplyPlus(IGraph graph, params object[] parameters)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            IMatches matches = DynamicMatch(lgraph, 1, parameters);
            if(matches.Count <= 0) return false;
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.First);
                matches = DynamicMatch(lgraph, 1, parameters);
            }
            while(matches.Count > 0);
            return true;
        }

        /// <summary>
        /// Applies this rule to the given graph at most max times.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        public bool ApplyMinMax(IGraph graph, int min, int max)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            IMatches matches;
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch(lgraph, 1, null);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.First);
            }
            return true;
        }

        /// <summary>
        /// Applies this rule to the given graph at most max times.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        public bool ApplyMinMax(IGraph graph, int min, int max, params object[] parameters)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            IMatches matches;
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch(lgraph, 1, parameters);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.First);
            }
            return true;
        }
    }

    /// <summary>
    /// A container of rules also managing some parts of rule application with sequences.
    /// Abstract base class with empty actions, the derived classes fill the actions dictionary.
    /// </summary>
    public abstract class LGSPActions : BaseActions
    {
        private LGSPGraph graph;
        private LGSPMatcherGenerator matcherGenerator;
        private String modelAssemblyName, actionsAssemblyName;

        /// <summary>
        /// A map from action names to LGSPAction objects.
        /// </summary>
        protected Dictionary<String, LGSPAction> actions = new Dictionary<String, LGSPAction>();


        /// <summary>
        /// Constructs a new LGSPActions instance.
        /// </summary>
        /// <param name="lgspgraph">The associated graph.</param>
        public LGSPActions(LGSPGraph lgspgraph)
        {
            graph = lgspgraph;
            graph.curActions = this;
            matcherGenerator = new LGSPMatcherGenerator(graph.Model);

            modelAssemblyName = Assembly.GetAssembly(graph.Model.GetType()).Location;
            actionsAssemblyName = Assembly.GetAssembly(this.GetType()).Location;

#if ASSERT_ALL_UNMAPPED_AFTER_MATCH
            OnMatched += new AfterMatchHandler(AssertAllUnmappedAfterMatch);
#endif
        }

        /// <summary>
        /// Constructs a new LGSPActions instance.
        /// This constructor is deprecated.
        /// </summary>
        /// <param name="lgspgraph">The associated graph.</param>
        /// <param name="modelAsmName">The name of the model assembly.</param>
        /// <param name="actionsAsmName">The name of the actions assembly.</param>
        public LGSPActions(LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
        {
            graph = lgspgraph;
            graph.curActions = this;
            modelAssemblyName = modelAsmName;
            actionsAssemblyName = actionsAsmName;
            matcherGenerator = new LGSPMatcherGenerator(graph.Model);
#if ASSERT_ALL_UNMAPPED_AFTER_MATCH
            OnMatched += new AfterMatchHandler(AssertAllUnmappedAfterMatch);
#endif
        }

#if ASSERT_ALL_UNMAPPED_AFTER_MATCH
        void AssertAllUnmappedAfterMatch(IMatches matches, bool special)
        {
            foreach(INode node in graph.GetCompatibleNodes(graph.Model.NodeModel.RootType))
            {
                LGSPNode lnode = (LGSPNode) node;
                if(lnode.mappedTo != 0)
                {
                    throw new Exception("Node \"" + graph.GetElementName(lnode) + "\" not unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
                if(lnode.negMappedTo != 0)
                {
                    throw new Exception("Node \"" + graph.GetElementName(lnode) + "\" not neg-unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
            }
            foreach(IEdge edge in graph.GetCompatibleEdges(graph.Model.EdgeModel.RootType))
            {
                LGSPEdge ledge = (LGSPEdge) edge;
                if(ledge.mappedTo != 0)
                {
                    throw new Exception("Edge \"" + graph.GetElementName(ledge) + "\" not unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
                if(ledge.negMappedTo != 0)
                {
                    throw new Exception("Edge \"" + graph.GetElementName(ledge) + "\" not neg-unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
            }
        }
#endif

        /// <summary>
        /// The associated graph.
        /// </summary>
        public override IGraph Graph { get { return graph; } set { graph = (LGSPGraph) value; } }

        /// <summary>
        /// Replaces the given action by a new action instance with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="action">The action to be replaced.</param>
        /// <returns>The new action instance.</returns>
        public LGSPAction GenerateAction(LGSPAction action)
        {
            LGSPAction newAction;
            newAction = matcherGenerator.GenerateAction(graph, modelAssemblyName, actionsAssemblyName, (LGSPAction) action);
            actions[action.Name] = newAction;
            return newAction;
        }

        /// <summary>
        /// Replaces the given action by a new action instance with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="actionName">The name of the action to be replaced.</param>
        /// <returns>The new action instance.</returns>
        public LGSPAction GenerateAction(String actionName)
        {
            LGSPAction action = (LGSPAction) GetAction(actionName);
            if(action == null)
                throw new ArgumentException("\"" + actionName + "\" is not the name of an action!\n");
            return GenerateAction(action);
        }

        /// <summary>
        /// Replaces the given actions by new action instances with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="oldActions">An array of actions to be replaced.</param>
        /// <returns>An array with the new action instances.</returns>
        public LGSPAction[] GenerateActions(params LGSPAction[] oldActions)
        {
            LGSPAction[] newActions = matcherGenerator.GenerateActions(graph, modelAssemblyName,
                actionsAssemblyName, oldActions);
            for(int i = 0; i < oldActions.Length; i++)
                actions[oldActions[i].Name] = newActions[i];

            return newActions;
        }

        /// <summary>
        /// Replaces the given actions by new action instances with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="actionNames">An array of names of actions to be replaced.</param>
        /// <returns>An array with the new action instances.</returns>
        public LGSPAction[] GenerateActions(params String[] actionNames)
        {
            LGSPAction[] oldActions = new LGSPAction[actionNames.Length];
            for(int i = 0; i < oldActions.Length; i++)
            {
                oldActions[i] = (LGSPAction) GetAction((String) actionNames[i]);
                if(oldActions[i] == null)
                    throw new ArgumentException("\"" + (String) actionNames[i] + "\"' is not the name of an action!");
            }
            return GenerateActions(oldActions);
        }

        /// <summary>
        /// Replaces a given action by another one.
        /// </summary>
        /// <param name="actionName">The name of the action to be replaced.</param>
        /// <param name="newAction">The new action.</param>
        public void ReplaceAction(String actionName, LGSPAction newAction)
        {
            actions[actionName] = newAction;
        }

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do</param>
        public override void Custom(params object[] args)
        {
            if(args.Length == 0) goto invalidCommand;

            switch((String) args[0])
            {
                case "gen_searchplan":
                {
                    if(graph.edgeCounts == null)
                        throw new ArgumentException("Graph not analyzed yet!\nPlease execute 'custom graph analyze'!");
                    if(args.Length < 2)
                        throw new ArgumentException("Usage: gen_searchplan <actionname>*");
                    LGSPAction[] oldActions = new LGSPAction[args.Length - 1];
                    for(int i = 0; i < oldActions.Length; i++)
                    {
                        oldActions[i] = (LGSPAction) GetAction((String) args[i + 1]);
                        if(oldActions[i] == null)
                            throw new ArgumentException("'" + (String) args[i + 1] + "' is not the name of an action!\n"
                                + "Please use 'show actions' to get a list of the available names.");
                    }
                    int startticks = Environment.TickCount;
                    LGSPAction[] newActions = matcherGenerator.GenerateActions(graph, modelAssemblyName,
                        actionsAssemblyName, oldActions);
                    int stopticks = Environment.TickCount;
                    Console.Write("Searchplans for actions ");
                    for(int i = 0; i < oldActions.Length; i++)
                    {
                        actions[oldActions[i].Name] = newActions[i];
                        if(i != 0) Console.Write(", ");
                        Console.Write("'" + oldActions[i].Name + "'");
                    }
                    Console.WriteLine(" generated in " + (stopticks - startticks) + " ms.");
                    return;
                }

                case "dump_sourcecode":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: dump_sourcecode <bool>\n"
                                + "If <bool> == true, C# files will be dumped for new searchplans.");

                    if(!bool.TryParse((String) args[1], out matcherGenerator.DumpDynSourceCode))
                        throw new ArgumentException("Illegal bool value specified: \"" + (String) args[1] + "\"");
                    return;

                case "dump_searchplan":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: dump_searchplan <bool>\n"
                                + "If <bool> == true, VCG and TXT files will be dumped for new searchplans.");

                    if(!bool.TryParse((String) args[1], out matcherGenerator.DumpSearchPlan))
                        throw new ArgumentException("Illegal bool value specified: \"" + (String) args[1] + "\"");
                    return;
            }

invalidCommand:
            throw new ArgumentException("Possible commands:\n"
                + "- gen_searchplan:  Generates a new searchplan for a given action\n"
                + "     depending on a previous graph analysis\n"
                + "- dump_sourcecode: Sets dumping of C# files for new searchplans\n"
                + "- dump_searchplan: Sets dumping of VCG and TXT files of new\n"
                + "     searchplans (with some intermediate steps)");
        }

        /// <summary>
        /// Enumerates all actions managed by this LGSPActions instance.
        /// </summary>
        public override IEnumerable<IAction> Actions { get { foreach(IAction action in actions.Values) yield return action; } }

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        public new LGSPAction GetAction(string name)
        {
            LGSPAction action;
            if(!actions.TryGetValue(name, out action)) return null;
            return action;
        }

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        protected override IAction GetIAction(string name)
        {
            return GetAction(name);
        }
    }

    /// <summary>
    /// Abstract base class for generated subpattern matching actions
    /// each object of an inheriting class represents a subpattern matching tasks
    /// which might be stored on the open tasks stack and executed later on.
    /// In addition to user-specified subpatterns, alternatives are mapped to subpattern actions, too.
    /// </summary>
    public abstract class LGSPSubpatternAction
    {
        /// <summary>
        /// The PatternGraph object from which this matching task object has been created
        /// </summary>
        protected PatternGraph patternGraph;

        /// <summary>
        /// The PatternGraph objects from which this matching task object has been created
        /// (non-null in case of an alternative, contains the pattern graphs of the alternative cases then)
        /// </summary>
        protected PatternGraph[] patternGraphs;

        /// <summary>
        /// The host graph in which to search for matches
        /// </summary>
        protected LGSPGraph graph;

        /// <summary>
        /// The subpattern actions which have to be executed until a full match is found
        /// The inheriting class contains the preset subpattern connection elements
        /// </summary>
        protected Stack<LGSPSubpatternAction> openTasks;

        /// <summary>
        /// Entry point to the temporary match object stack representing the pattern nesting from innermost outwards.
        /// Needed for patternpath checking in negatives/independents, used as attachment point / is top of stack.
        /// </summary>
        public IMatch matchOfNestingPattern;

        /// <summary>
        /// Last match at the previous nesting level in the temporary match object stack representing the pattern nesting from innermost outwards.
        /// Needed for patternpath checking in negatives/independents, used as starting point of patternpath isomorphy checks.
        /// </summary>
        public IMatch lastMatchAtPreviousNestingLevel;

        /// <summary>
        /// Tells whether this subpattern has to search the pattern path when matching
        /// </summary>
        public bool searchPatternpath;

        /// <summary>
        /// Searches for the subpattern as specified by RulePattern.
        /// Takes care of search state as given by found partial matches, negLevel to search at
        /// and maximum number of matches to search for (zero = find all matches) 
        /// (and open tasks via this).
        /// </summary>
        public abstract void myMatch(List<Stack<IMatch>> foundPartialMatches, int maxMatches, int negLevel);
    }

    /// <summary>
    /// Class containing global functions for checking whether node/edge is matched on patternpath
    /// </summary>
    public sealed class PatternpathIsomorphyChecker
    { 
        public static bool IsMatched(LGSPNode node, IMatch lastMatchAtPreviousNestingLevel)
        {
            Debug.Assert(lastMatchAtPreviousNestingLevel!=null);

            // move through matches stack backwards to starting rule, 
            // check if node is already matched somewhere on the derivation path
            IMatch match = lastMatchAtPreviousNestingLevel;
            while (match != null)
            {
                for (int i = 0; i < match.NumberOfNodes; ++i)
                {
                    if (match.getNodeAt(i) == node)
                    {
                        return true;
                    }
                }
                match = match.MatchOfEnclosingPattern;
            }
            return false;
        }

        public static bool IsMatched(LGSPEdge edge, IMatch lastMatchAtPreviousNestingLevel)
        {
            Debug.Assert(lastMatchAtPreviousNestingLevel != null);

            // move through matches stack backwards to starting rule, 
            // check if edge is already matched somewhere on the derivation path
            IMatch match = lastMatchAtPreviousNestingLevel;
            while (match != null)
            {
                for (int i = 0; i < match.NumberOfEdges; ++i)
                {
                    if (match.getEdgeAt(i) == edge)
                    {
                        return true;
                    }
                }
                match = match.MatchOfEnclosingPattern;
            }
            return false;
        }
    }
}
