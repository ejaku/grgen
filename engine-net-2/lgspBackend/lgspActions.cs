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
    public delegate LGSPMatches MatchInvoker(LGSPGraph graph, int maxMatches, object[] parameters);

    /// <summary>
    /// An object representing a match of the LGSPBackend.
    /// </summary>
    public class LGSPMatch : IMatch
    {
        /// <summary>
        /// The match object represents a match of the pattern given in this member
        /// </summary>
        public PatternGraph Pattern { get { return patternGraph; } }

        /// <summary>
        /// member for pattern propertie representing the pattern graph which was matched
        /// </summary>
        public PatternGraph patternGraph;

        /// <summary>
        /// An array of all nodes in the match.
        /// The order is given by the Nodes array of the according IPatternGraph.
        /// </summary>
        public LGSPNode[] Nodes;

        /// <summary>
        /// An array of all edges in the match.
        /// The order is given by the Edges array of the according IPatternGraph.
        /// </summary>
        public LGSPEdge[] Edges;

        /// <summary>
        /// An array of variables given to the matcher method.
        /// The order is given by the Variables array of the according IPatternGraph.
        /// </summary>
        public object[] Variables;

        /// <summary>
        /// An array of all submatches due to subpatterns and alternatives. 
        /// First subpatterns in order of EmbeddedGraphs array of the according IPatternGraph,
        /// then alternatives in order of Alternatives array of the according IPatternGraph.
        /// You can find out which alternative case was matched by inspecting the Pattern member of the submatch.
        /// </summary>
        public LGSPMatch[] EmbeddedGraphs;

        /// <summary>
        /// The next match in the linked list of matches.
        /// </summary>
        public LGSPMatch nextMatch;

        /// <summary>
        /// Constructs a new match object.
        /// </summary>
        /// <param name="nodes">The nodes of the match.</param>
        /// <param name="edges">The edges of the match.</param>
        /// <param name="varParams">The parameter variables of the match.</param>
        /// <param name="embeddedGraphs">The embedded graphs of the match.</param>
        public LGSPMatch(LGSPNode[] nodes, LGSPEdge[] edges, object[] varParams, LGSPMatch[] embeddedGraphs)
        {
            this.Nodes = nodes;
            this.Edges = edges;
            this.Variables = varParams;
            this.EmbeddedGraphs = embeddedGraphs;
        }

        /// <summary>
        /// Returns a string representation of the LGSPMatch object.
        /// Currently not really informative.
        /// </summary>
        /// <returns>A string representation of the LGSPMatch object.</returns>
        public override string ToString()
        {
            return "Match of " + patternGraph.Name;
        }

        #region IMatch Members

        /// <summary>
        /// The match object represents a match of the pattern given in this member
        /// </summary>
        IPatternGraph IMatch.Pattern
        {
            get { return patternGraph; }
        }

        /// <summary>
        /// An array of all nodes in the match.
        /// The order is given by the Nodes array of the according IPatternGraph.
        /// </summary>
        INode[] IMatch.Nodes
        {
            get { return Nodes; }
        }

        /// <summary>
        /// An array of all edges in the match.
        /// The order is given by the Edges array of the according IPatternGraph.
        /// </summary>
        IEdge[] IMatch.Edges
        {
            get { return Edges; }
        }

        /// <summary>
        /// An array of variables given to the matcher method.
        /// The order is given by the Variables array of the according IPatternGraph.
        /// </summary>
        object[] IMatch.Variables
        {
            get { return Variables; }
        }

        /// <summary>
        /// An array of all submatches due to subpatterns and alternatives. 
        /// First subpatterns in order of EmbeddedGraphs array of the according IPatternGraph,
        /// then alternatives in order of Alternatives array of the according IPatternGraph.
        /// You can find out which alternative case was matched by inspecting the Pattern member of the submatch.
        /// </summary>
        IMatch[] IMatch.EmbeddedGraphs
        {
            get { return EmbeddedGraphs; }
        }

        #endregion
    }

    /// <summary>
    /// Every generated Action contains a LGSPMatches-object, which contains a LGSPMatchesList.
    /// A matches list stores the matches found by the last application of the action,
    /// the matches objects within the list are recycled by the next application of the action,
    /// only their content gets updated.
    /// The purpose of this list is to act as a memory manager 
    /// to save new/garbage collection cycles and improve cache footprint.
    /// </summary>
    public class LGSPMatchesList : IEnumerable<IMatch>
    {
        /// <summary>
        /// number of nodes, edges, variables and subpatterns each match object contains
        /// that knowledge allows us to create the match objects here
        /// </summary>
        private int numNodes, numEdges, numVars, numSubpats;

        /// <summary>
        /// number of found matches in the list
        /// </summary>
        private int count;

        /// <summary>
        /// head of list
        /// </summary>
        private LGSPMatch root;

        /// <summary>
        /// logically last element of list, not necessarily physically the last element 
        /// as previously generated matches are kept and recycled 
        /// denotes the next point of logical insertion i.e. physical update
        /// </summary>
        private LGSPMatch last;


        public LGSPMatchesList(int numNodes, int numEdges, int numVars, int numSubpats)
        {
            this.numNodes = numNodes;
            this.numEdges = numEdges;
            this.numVars = numVars;
            this.numSubpats = numSubpats;
            last = root = new LGSPMatch(new LGSPNode[numNodes], new LGSPEdge[numEdges], new object[numVars], new LGSPMatch[numSubpats]);
        }

        public int Count { get { return count; } }
        public LGSPMatch First { get { return count > 0 ? root : null; } }

        /// <summary>
        /// remove all filled and committed elements from the list
        /// </summary>
        public void Clear()
        {
            count = 0;
            last = root;
        }

        /// <summary>
        /// returns an empty match object from the matches list 
        /// to be filled by the matching action with the found nodes, edges and subpatterns.
        /// unless PositionWasFilledFixIt is called you always get the same element
        /// </summary>
        public LGSPMatch GetNextUnfilledPosition()
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
            if (last.nextMatch == null)
                last.nextMatch = new LGSPMatch(new LGSPNode[numNodes], new LGSPEdge[numEdges], new object[numVars], new LGSPMatch[numSubpats]);
            last = last.nextMatch;
        }

        public LGSPMatch this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Index out of range: " + index);
                LGSPMatch cur = root;
                for (int i = 0; i < index; i++, cur = cur.nextMatch) ;
                return cur;
            }
        }

        public IEnumerator<IMatch> GetEnumerator()
        {
            LGSPMatch cur = root;
            for (int i = 0; i < count; i++, cur = cur.nextMatch)
                yield return cur;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// An object representing a (possibly empty) set of matches in a graph before the rewrite has been applied.
    /// It is returned by IAction.Match() and given to the OnMatched, OnFinishing and OnFinished event.
    /// </summary>
    public class LGSPMatches : IMatches
    {
        /// <summary>
        /// The action object used to generate this LGSPMatches object.
        /// </summary>
        public LGSPAction producer;
//        public List<LGSPMatch> matches = new List<LGSPMatch>();
//        public SingleLinkedList<LGSPMatch> matches = new SingleLinkedList<LGSPMatch>();

        /// <summary>
        /// The matches list containing all matches.
        /// </summary>
        public LGSPMatchesList matchesList;

        /// <summary>
        /// Constructs a new LGSPMatches instance.
        /// </summary>
        /// <param name="producer">The action object used to generate this LGSPMatches object</param>
        /// <param name="numNodes">The number of nodes which will be matched by the given action.</param>
        /// <param name="numEdges">The number of edges which will be matched by the given action.</param>
        /// <param name="numVars">The number of variables which will be used by the given action.</param>
        /// <param name="numSubpats">The number of subpatterns which will be matched by the given action.</param>
        public LGSPMatches(LGSPAction producer, int numNodes, int numEdges, int numVars, int numSubpats)
        {
            this.producer = producer;
            matchesList = new LGSPMatchesList(numNodes, numEdges, numVars, numSubpats);
        }

        /// <summary>
        /// The action object used to generate this LGSPMatches object
        /// </summary>
        public IAction Producer { get { return producer; } }

        /// <summary>
        /// The number of matches found by Producer
        /// </summary>
        public int Count { get { return matchesList.Count; } }

        /// <summary>
        /// Returns the match with the given index. Invalid indices cause an IndexOutOfRangeException.
        /// This may be slow. If you want to iterate over the elements the Matches IEnumerable should be used.
        /// </summary>
        public IMatch GetMatch(int index) { return matchesList[index]; }

        /// <summary>
        /// Returns an enumerator over all found matches.
        /// </summary>
        public IEnumerator<IMatch> GetEnumerator()
        {
            return matchesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return matchesList.GetEnumerator();
        }
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
        /// <returns>An LGSPMatches object containing the found matches.</returns>
        public LGSPMatches Match(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            return DynamicMatch(graph, maxMatches, parameters);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// </summary>
        /// <returns>An array of values returned by the rule.</returns>
        public object[] Modify(LGSPGraph graph, LGSPMatch match)
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
        /// <returns>An array of values returned by the rule.</returns>
        public object[] ModifyAll(LGSPGraph graph, LGSPMatches matches)
        {
            object[] retElems = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
            {
                foreach(LGSPMatch match in matches)
                    retElems = rulePattern.Modify(graph, match);
            }
            else
            {
                foreach(LGSPMatch match in matches)
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
        /// or null, if no match was found.</returns>
        public object[] Apply(LGSPGraph graph)
        {
            LGSPMatches matches = DynamicMatch(graph, 1, null);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, matches.matchesList.First);
            else
                return rulePattern.ModifyNoReuse(graph, matches.matchesList.First);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// or null, if no match was found.</returns>
        public object[] Apply(LGSPGraph graph, params object[] parameters)
        {
            LGSPMatches matches = DynamicMatch(graph, 1, parameters);
            if(matches.Count <= 0) return null;

            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, matches.matchesList.First);
            else
                return rulePattern.ModifyNoReuse(graph, matches.matchesList.First);
        }

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten or 0 for no limit.</param>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// or null, if no match was found.</returns>
        public object[] ApplyAll(int maxMatches, LGSPGraph graph)
        {
            LGSPMatches matches = DynamicMatch(graph, maxMatches, null);
            object[] retElems = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
            {
                foreach(LGSPMatch match in matches)
                    retElems = rulePattern.Modify(graph, match);
            }
            else
            {
                foreach(LGSPMatch match in matches)
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
        /// or null, if no match was found.</returns>
        public object[] ApplyAll(int maxMatches, LGSPGraph graph, params object[] parameters)
        {
            LGSPMatches matches = DynamicMatch(graph, maxMatches, parameters);
            object[] retElems = null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
            {
                foreach(LGSPMatch match in matches)
                    retElems = rulePattern.Modify(graph, match);
            }
            else
            {
                foreach(LGSPMatch match in matches)
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
//            return Match((LGSPGraph) graph, maxMatches, parameters);
            return DynamicMatch((LGSPGraph) graph, maxMatches, parameters);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <returns>An array of objects returned by the rule.</returns>
        object[] IAction.Modify(IGraph graph, IMatch match)
        {
//            return Modify((LGSPGraph)graph, (LGSPMatch)match);
//            return rulePattern.Modify((LGSPGraph)graph, (LGSPMatch)match);
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, (LGSPMatch) match);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, (LGSPMatch) match);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with all of the given matches.
        /// No OnRewritingNextMatch events are triggered by this function.
        /// </summary>
        /// <returns>An array of objects returned by the rule.</returns>
        object[] IAction.ModifyAll(IGraph graph, IMatches matches)
        {
            return ModifyAll((LGSPGraph) graph, (LGSPMatches) matches);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// or null, if no match was found.</returns>
        object[] IAction.Apply(IGraph graph)
        {
            LGSPMatches matches = DynamicMatch((LGSPGraph) graph, 1, null);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, matches.matchesList.First);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, matches.matchesList.First);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// or null, if no match was found.</returns>
        object[] IAction.Apply(IGraph graph, params object[] parameters)
        {
            LGSPMatches matches = DynamicMatch((LGSPGraph) graph, 1, parameters);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, matches.matchesList.First);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, matches.matchesList.First);
        }

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// or null, if no match was found.</returns>
        object[] IAction.ApplyAll(int maxMatches, IGraph graph)
        {
            return ApplyAll(maxMatches, (LGSPGraph) graph);
        }

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
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
            LGSPMatches matches;
            while(true)
            {
                matches = DynamicMatch(lgraph, 1, null);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matchesList.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matchesList.First);
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
            LGSPMatches matches;
            while(true)
            {
                matches = DynamicMatch(lgraph, 1, parameters);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matchesList.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matchesList.First);
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
            LGSPMatches matches = DynamicMatch(lgraph, 1, null);
            if(matches.Count <= 0) return false;
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matchesList.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matchesList.First);
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
            LGSPMatches matches = DynamicMatch(lgraph, 1, parameters);
            if(matches.Count <= 0) return false;
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matchesList.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matchesList.First);
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
            LGSPMatches matches;
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch(lgraph, 1, null);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matchesList.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matchesList.First);
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
            LGSPMatches matches;
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch(lgraph, 1, parameters);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matchesList.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matchesList.First);
            }
            return true;
        }
    }

    /// <summary>
    /// A container of rules also managing some parts of rule application with sequences.
    /// </summary>
    public abstract class LGSPActions : BaseActions
    {
        private LGSPGraph graph;
        private LGSPMatcherGenerator matcherGenerator;
        private String modelAssemblyName, actionsAssemblyName;
        int maxMatches = 0;

        protected Dictionary<String, LGSPAction> actions = new Dictionary<String, LGSPAction>();


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

        public LGSPAction GenerateAction(LGSPAction action)
        {
            LGSPAction newAction;
            newAction = matcherGenerator.GenerateAction(graph, modelAssemblyName, actionsAssemblyName, (LGSPAction) action);
            actions[action.Name] = newAction;
            return newAction;
        }

        public LGSPAction GenerateAction(String actionName)
        {
            LGSPAction action = (LGSPAction) GetAction(actionName);
            if(action == null)
                throw new ArgumentException("\"" + actionName + "\" is not the name of an action!\n");
            return GenerateAction(action);
        }

        public LGSPAction[] GenerateActions(params LGSPAction[] oldActions)
        {
            LGSPAction[] newActions = matcherGenerator.GenerateActions(graph, modelAssemblyName,
                actionsAssemblyName, oldActions);
            for(int i = 0; i < oldActions.Length; i++)
                actions[oldActions[i].Name] = newActions[i];

            return newActions;
        }

        public LGSPAction[] GenerateActions(params String[] actionName)
        {
            LGSPAction[] oldActions = new LGSPAction[actionName.Length];
            for(int i = 0; i < oldActions.Length; i++)
            {
                oldActions[i] = (LGSPAction) GetAction((String) actionName[i]);
                if(oldActions[i] == null)
                    throw new ArgumentException("\"" + (String) actionName[i] + "\"' is not the name of an action!");
            }
            return GenerateActions(oldActions);
        }

        public void ReplaceAction(String actionName, LGSPAction newAction)
        {
            actions[actionName] = newAction;
        }

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        public override void Custom(params object[] args)
        {
            if(args.Length == 0) goto invalidCommand;

            switch((String) args[0])
            {
                case "set_max_matches":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: set_max_matches <integer>\nIf <integer> <= 0, all matches will be matched.");

                    int maxMatches;
                    if(!int.TryParse((String) args[1], out maxMatches))
                        throw new ArgumentException("Illegal integer value specified: \"" + (String) args[1] + "\"");
                    MaxMatches = maxMatches;
                    return;

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
                + "- set_max_matches: Sets the maximum number of matches to be found\n"
                + "     during matching\n"
                + "- gen_searchplan:  Generates a new searchplan for a given action\n"
                + "     depending on a previous graph analysis\n"
                + "- dump_sourcecode: Sets dumping of C# files for new searchplans\n"
                + "- dump_searchplan: Sets dumping of VCG and TXT files of new\n"
                + "     searchplans (with some intermediate steps)");
        }

        /// <summary>
        /// The maximum number of matches to be returned for a RuleAll sequence element.
        /// If it is zero or less, the number of matches is unlimited.
        /// </summary>
        public override int MaxMatches { get { return maxMatches; } set { maxMatches = value; } }

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
        /// Searches for the subpattern as specified by RulePattern.
        /// Takes care of search state as given by found partial matches, negLevel to search at
        /// and maximum number of matches to search for (zero = find all matches) 
        /// (and open tasks via this).
        /// </summary>
        public abstract void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel);
    }
}
