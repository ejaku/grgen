/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define USE_HIGHPERFORMANCE_COUNTER

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a rule invocation.
    /// It tells from where (which variables/constants) to get the input values 
    /// and where (which variables) to store the output values.
    /// </summary>
    public class RuleInvocationParameterBindings
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public IAction Action;

        /// <summary>
        /// The name of the rule. Used for generation, where the IAction objects do not exist yet.
        /// </summary>
        public String RuleName;

        /// <summary>
        /// An array of variables used for the parameters.
        /// It must have the same length as Parameters.
        /// If an entry is null, the according entry in parameters is used unchanged.
        /// </summary>
        public SequenceVariable[] ParamVars;

        /// <summary>
        /// An array of variables used for the return values.
        /// </summary>
        public SequenceVariable[] ReturnVars;

        /// <summary>
        /// Buffer to store parameters used by libGr to avoid unneccessary memory allocation.
        /// Also holds constant parameters at the positions where ParamVars has null entries.
        /// </summary>
        public object[] Parameters;

        /// <summary>
        /// Instantiates a new RuleInvocationParameterBindings object
        /// </summary>
        /// <param name="action">The IAction instance to be used</param>
        /// <param name="paramVars">An array of variable used for the parameters</param>
        /// <param name="paramConsts">An array of constants used for the parameters.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public RuleInvocationParameterBindings(IAction action, SequenceVariable[] paramVars, object[] paramConsts, SequenceVariable[] returnVars)
        {
            if(paramVars.Length != paramConsts.Length)
                throw new ArgumentException("Lengths of variable and constant parameter array do not match");

            Action = action;
            if(action != null) RuleName = action.Name;
            else RuleName = "<Unknown rule>";
            ParamVars = paramVars;
            ReturnVars = returnVars;
            Parameters = paramConsts;
        }
    }

    /// <summary>
    /// An object representing an executable rule.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// The name of the action
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The RulePattern object from which this IAction object has been created.
        /// </summary>
        IRulePattern RulePattern { get; }

        /// <summary>
        /// Searches for a graph pattern as specified by RulePattern.
        /// </summary>
        /// <param name="graph">The host graph.</param>
        /// <param name="maxMatches">The maximum number of matches to be searched for, or zero for an unlimited search.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>An IMatches object containing the found matches.</returns>
        IMatches Match(IGraph graph, int maxMatches, object[] parameters);

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// The graph and match object must have the correct type for the used backend (and this action).
        /// </summary>
        /// <returns>An array of objects returned by the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        object[] Modify(IGraph graph, IMatch match);

        /// <summary>
        /// Performs the rule specific modifications to the given graph with all of the given matches.
        /// The graph and match object must have the correct type for the used backend.
        /// No OnRewritingNextMatch events are triggered by this function.
        /// </summary>
        /// <returns>An array of objects returned by the last applicance of the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        object[] ModifyAll(IGraph graph, IMatches matches);

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] Apply(IGraph graph);

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
        object[] Apply(IGraph graph, params object[] parameters);

        /// <summary>
        /// Tries to apply this rule to all occurrences in the given graph "at once".
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten.</param>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] ApplyAll(int maxMatches, IGraph graph);

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
        object[] ApplyAll(int maxMatches, IGraph graph, params object[] parameters);

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>Always returns true.</returns>
        bool ApplyStar(IGraph graph);

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>Always returns true.</returns>
        bool ApplyStar(IGraph graph, params object[] parameters);

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        bool ApplyPlus(IGraph graph);

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        bool ApplyPlus(IGraph graph, params object[] parameters);

        /// <summary>
        /// Applies this rule to the given graph at most max times.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        bool ApplyMinMax(IGraph graph, int min, int max);

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
        bool ApplyMinMax(IGraph graph, int min, int max, params object[] parameters);
    }

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

    /// <summary>
    /// An object accumulating information about needed time, number of found matches and number of performed rewrites.
    /// </summary>
    public class PerformanceInfo
    {
        /// <summary>
        /// Accumulated number of matches found by any rule applied via an BaseActions object.
        /// </summary>
        public int MatchesFound;

        /// <summary>
        /// Accumulated number of rewrites performed by any rule applied via an BaseActions object.
        /// This differs from <see cref="MatchesFound"/> for test rules, tested rules, and undone rules.
        /// </summary>
        public int RewritesPerformed;

#if USE_HIGHPERFORMANCE_COUNTER

        private long totalStart, totalEnd;
        private long localStart;
        private long totalMatchTime;
        private long totalRewriteTime;

        public long LastMatchTime;
        public long LastRewriteTime;

        public int TotalMatchTimeMS { get { return (int) (totalMatchTime * 1000 / perfFreq); } }
        public int TotalRewriteTimeMS { get { return (int) (totalRewriteTime * 1000 / perfFreq); } }
        public int TotalTimeMS { get { return (int) ((totalEnd - totalStart) * 1000 / perfFreq); } }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long perfCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long freq);

        private long perfFreq;

        public PerformanceInfo()
        {
            if(!QueryPerformanceFrequency(out perfFreq))
                throw new Win32Exception();
            Console.WriteLine("Performance counter frequency: {0} Hz", perfFreq);
        }


        public void Start()
        {
            QueryPerformanceCounter(out totalStart);
        }

        public void Stop()
        {
            QueryPerformanceCounter(out totalEnd);
        }

//        [Conditional("DEBUG")]
        public void StartLocal()
        {
            QueryPerformanceCounter(out localStart);
        }

//        [Conditional("DEBUG")]
        public void StopMatch()
        {
            long counter;
            QueryPerformanceCounter(out counter);
            totalMatchTime += counter - localStart;
            LastMatchTime = counter - localStart;
            LastRewriteTime = 0;
        }

//        [Conditional("DEBUG")]
        public void StopRewrite()
        {
            long counter;
            QueryPerformanceCounter(out counter);
            totalRewriteTime += counter - localStart;
            LastRewriteTime = counter - localStart;
        }

#if DEBUGACTIONS
        public int TimeDiffToMS(long diff)
        {
            return (int) (diff * 1000 / perfFreq);
        }
#endif

#else

        private int totalStart;
        private int localStart;
        private int totalMatchTime;
        private int totalRewriteTime;
        private int totalTime;

        /// <summary>
        /// The time needed for the last matching.
        /// </summary>
        /// <remarks>Only updated if either DEBUGACTIONS or MATCHREWRITEDETAIL has been defined.</remarks>
        public long LastMatchTime;

        /// <summary>
        /// The time needed for the last rewriting.
        /// </summary>
        /// <remarks>Only updated if either DEBUGACTIONS or MATCHREWRITEDETAIL has been defined.</remarks>
        public long LastRewriteTime;

        /// <summary>
        /// The total time needed for matching.
        /// Due to timer resolution, this should not be used, except for very difficult patterns.
        /// </summary>
        public int TotalMatchTimeMS { get { return totalMatchTime; } }

        /// <summary>
        /// The total time needed for rewriting.
        /// Due to timer resolution, this should not be used, except for very big rewrites.
        /// </summary>
        public int TotalRewriteTimeMS { get { return totalRewriteTime; } }

        /// <summary>
        /// The accumulated time of rule and sequence applications.
        /// </summary>
        public int TotalTimeMS { get { return totalTime; } }

        /// <summary>
        /// Starts time measurement.
        /// </summary>
        public void Start()
        {
            totalStart = Environment.TickCount;
        }

        /// <summary>
        /// Stops time measurement and increases the TotalTimeMS by the elapsed time between this call
        /// and the last call to Start().
        /// </summary>
        public void Stop()
        {
            totalTime += Environment.TickCount - totalStart;
        }

        /// <summary>
        /// Resets all accumulated information.
        /// </summary>
        public void Reset()
        {
            MatchesFound = totalStart = localStart = totalMatchTime = totalRewriteTime = totalTime = 0;
            LastMatchTime = LastRewriteTime = 0;
        }

        /// <summary>
        /// Starts a local time measurement to be used with either StopMatch() or StopRewrite().
        /// </summary>
        /// <remarks>Only usable if either DEBUGACTIONS or MATCHREWRITEDETAIL has been defined.</remarks>
        [Conditional("DEBUGACTIONS"), Conditional("MATCHREWRITEDETAIL")]
        public void StartLocal()
        {
            localStart = Environment.TickCount;
        }

        /// <summary>
        /// Stops a local time measurement, sets LastMatchTime to the elapsed time between this call
        /// and the last call to StartLocal() and increases the TotalMatchTime by this amount.
        /// </summary>
        /// <remarks>Only usable if either DEBUGACTIONS or MATCHREWRITEDETAIL has been defined.</remarks>
        [Conditional("DEBUGACTIONS"), Conditional("MATCHREWRITEDETAIL")]
        public void StopMatch()
        {
            int diff = Environment.TickCount - localStart;
            totalMatchTime += diff;
            LastMatchTime = diff;
            LastRewriteTime = 0;
        }

        /// <summary>
        /// Stops a local time measurement, sets LastRewriteTime to the elapsed time between this call
        /// and the last call to StartLocal() and increases the TotalRewriteTime by this amount.
        /// </summary>
        /// <remarks>Only usable if either DEBUGACTIONS or MATCHREWRITEDETAIL has been defined.</remarks>
        [Conditional("DEBUGACTIONS"), Conditional("MATCHREWRITEDETAIL")]
        public void StopRewrite()
        {
            int diff = Environment.TickCount - localStart;
            totalRewriteTime += diff;
            LastRewriteTime = diff;
        }

#if DEBUGACTIONS
        public int TimeDiffToMS(long diff)
        {
            return (int) diff;
        }
#endif
#endif
    }

    /// <summary>
    /// Describes a range with a minimum and a maximum value.
    /// </summary>
    public struct Range
    {
        /// <summary>
        /// Constant value representing positive infinity for a range.
        /// </summary>
        public const int Infinite = int.MaxValue;

        /// <summary>
        /// The lower bound of the range.
        /// </summary>
        public int Min;

        /// <summary>
        /// The upper bound of the range.
        /// </summary>
        public int Max;

        /// <summary>
        /// Constructs a Range object.
        /// </summary>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
