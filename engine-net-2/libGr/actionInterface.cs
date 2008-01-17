//#define USE_HIGHPERFORMANCE_COUNTER

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a rule invocation with optional parameter variables and optional return value receiving variables.
    /// Used by the sequence objects and BaseActions.
    /// </summary>
    public class RuleObject
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public IAction Action;

        /// <summary>
        /// An array of variable names used for the parameters
        /// </summary>
        public String[] ParamVars;

        /// <summary>
        /// An array of variable names used for the return values
        /// </summary>
        public String[] ReturnVars;

        /// <summary>
        /// Buffer to store parameters used by libGr to avoid unneccessary memory allocation
        /// </summary>
        public IGraphElement[] Parameters;

        /// <summary>
        /// Instantiates a new RuleObject
        /// </summary>
        /// <param name="action">The IAction instance to be used</param>
        /// <param name="paramVars">An array of variable names used for the parameters</param>
        /// <param name="returnVars">An array of variable names used for the return values</param>
        public RuleObject(IAction action, String[] paramVars, String[] returnVars)
        {
            Action = action;
            ParamVars = paramVars;
            ReturnVars = returnVars;
            Parameters = new IGraphElement[paramVars.Length];
        }
    }

    /// <summary>
    /// An object representing an executable rule.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// The name of the rule
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
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>An IMatches object containing the found matches.</returns>
        IMatches Match(IGraph graph, int maxMatches, IGraphElement[] parameters);

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <returns>An array of elements returned by the rule.</returns>
        IGraphElement[] Modify(IGraph graph, IMatch match);

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
		/// <returns>A possibly empty array of IGraphElement instances returned by the rule,
		/// or null, if no match was found.</returns>
		IGraphElement[] Apply(IGraph graph);

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
		/// <returns>A possibly empty array of IGraphElement instances returned by the rule,
		/// or null, if no match was found.</returns>
		IGraphElement[] Apply(IGraph graph, params IGraphElement[] parameters);

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
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>Always returns true.</returns>
        bool ApplyStar(IGraph graph, params IGraphElement[] parameters);

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
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        bool ApplyPlus(IGraph graph, params IGraphElement[] parameters);

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
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        bool ApplyMinMax(IGraph graph, int min, int max, params IGraphElement[] parameters);
    }

    /// <summary>
    /// An object representing a match.
    /// </summary>
    public interface IMatch
    {
        /// <summary>
        /// Not implemented yet
        /// </summary>
        IPatternGraph Pattern { get; } // niy

        /// <summary>
        /// An array of all nodes in the match.
        /// The order is given by the Nodes array of the according IPatternGraph.
        /// </summary>
        INode[] Nodes { get; }

        /// <summary>
        /// An array of all edges in the match.
        /// The order is given by the Edges array of the according IPatternGraph.
        /// </summary>
        IEdge[] Edges { get; }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        IMatch[] EmbeddedGraphs { get; } // niy
    }

    /// <summary>
    /// An object representing a (possibly empty) set of matches in a graph before the rewrite has been applied.
    /// It is returned by IAction.Match() and given to the OnMatched event.
    /// </summary>
    public interface IMatches : IEnumerable<IMatch>
    {
        /// <summary>
        /// The action object used to generate this IMatches object
        /// </summary>
        IAction Producer { get; }

        /// <summary>
        /// The number of matches found by Producer
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the match with the given index. Invalid indices cause an exception.
        /// This may be slow. If you want to iterate over the elements the Matches IEnumerable should be used.
        /// </summary>
        IMatch GetMatch(int index);
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

        public long LastMatchTime;
        public long LastRewriteTime;

        public int TotalMatchTimeMS { get { return totalMatchTime; } }
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
        /// Stops time measurement and increases the TotalTimeMS by the elapsed time between this call and the last call to Start().
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

        [Conditional("DEBUGACTIONS"), Conditional("MATCHREWRITEDETAIL")]
        public void StartLocal()
        {
            localStart = Environment.TickCount;
        }

        [Conditional("DEBUGACTIONS"), Conditional("MATCHREWRITEDETAIL")]
        public void StopMatch()
        {
            int diff = Environment.TickCount - localStart;
            totalMatchTime += diff;
            LastMatchTime = diff;
            LastRewriteTime = 0;
        }

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
}