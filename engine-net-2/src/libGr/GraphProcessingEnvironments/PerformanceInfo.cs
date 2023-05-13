/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object accumulating information about needed time, number of found matches and number of performed rewrites.
    /// </summary>
    public class PerformanceInfo
    {
        /// <summary>
        /// Stores a profile per action (given by name, that gives the average for the loop and search steps needed to achieve the goal or finally fail)
        /// </summary>
        public readonly Dictionary<string, ActionProfile> ActionProfiles = new Dictionary<string, ActionProfile>();

        /// <summary>
        /// Accumulated number of matches found by any rule since last Reset.
        /// </summary>
        public int MatchesFound;

        /// <summary>
        /// Accumulated number of rewrites performed by any rule since last Reset.
        /// This differs from <see cref="MatchesFound"/> for test rules, tested rules, and undone rules.
        /// </summary>
        public int RewritesPerformed;

        /// <summary>
        /// Accumulated number of search steps carried out since last Reset.
        /// (Number of bindings of a graph element to a pattern element, but bindings where only one choice is available don't count into this.)
        /// Only incremented if gathering of profiling information was requested ("-profile", "new set profile on").
        /// (The per-thread steps are added here after each action call when the threads completed.)
        /// </summary>
        public long SearchSteps;

        /// <summary>
        /// Number of search steps carried out for the current/last action call, per thread (in case a multithreaded matcher is/was used).
        /// </summary>
        public long[] SearchStepsPerThread;

        /// <summary>
        /// Number of loop steps of the first loop executed for the current/last action call, per thread (in case a multithreaded matcher is/was used)
        /// </summary>
        public long[] LoopStepsPerThread;

        public void ResetStepsPerThread(int numberOfThreads)
        {
            if(SearchStepsPerThread == null || SearchStepsPerThread.Length < numberOfThreads)
            {
                SearchStepsPerThread = new long[numberOfThreads];
            }
            for(int i = 0; i < numberOfThreads; ++i)
            {
                SearchStepsPerThread[i] = 0;
            }
            if(LoopStepsPerThread == null || LoopStepsPerThread.Length < numberOfThreads)
            {
                LoopStepsPerThread = new long[numberOfThreads];
            }
            for(int i = 0; i < numberOfThreads; ++i)
            {
                LoopStepsPerThread[i] = 0;
            }
        }

        /// <summary>
        /// The accumulated time of rule and sequence applications in seconds since last Reset,
        /// as defined by the intervals between Start and Stop.
        /// </summary>
        public double TimeNeeded { get { return ((double)totalTime) / Stopwatch.Frequency; } }

        /// <summary>
        /// Starts time measurement.
        /// </summary>
        public void Start()
        {
            stopwatch.Start();
            totalStart = stopwatch.ElapsedTicks;
        }

        /// <summary>
        /// Stops time measurement and increases the TimeNeeded by the elapsed time 
        /// between this call and the last call to Start().
        /// </summary>
        public void Stop()
        {
            totalTime += stopwatch.ElapsedTicks - totalStart;
        }

        private readonly Stopwatch stopwatch = new Stopwatch();
        private long totalStart;
        private long totalTime;

        /// <summary>
        /// Resets all accumulated information.
        /// </summary>
        public void Reset()
        {
            MatchesFound = 0;
            RewritesPerformed = 0;
            SearchSteps = 0;
            totalTime = 0;

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            localStart = 0;
            totalMatchTime = 0;
            totalRewriteTime = 0;
            LastMatchTime = 0;
            LastRewriteTime = 0;
#endif
        }

#if DEBUGACTIONS || MATCHREWRITEDETAIL
        private int localStart;
        private int totalMatchTime;
        private int totalRewriteTime;

        /// <summary>
        /// The time needed for the last matching.
        /// </summary>
        public long LastMatchTime;

        /// <summary>
        /// The time needed for the last rewriting.
        /// </summary>
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
        /// Starts a local time measurement to be used with either StopMatch() or StopRewrite().
        /// </summary>
        public void StartLocal()
        {
            localStart = Environment.TickCount;
        }

        /// <summary>
        /// Stops a local time measurement, sets LastMatchTime to the elapsed time between this call
        /// and the last call to StartLocal() and increases the TotalMatchTime by this amount.
        /// </summary>
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
        public void StopRewrite()
        {
            int diff = Environment.TickCount - localStart;
            totalRewriteTime += diff;
            LastRewriteTime = diff;
        }

        public int TimeDiffToMS(long diff)
        {
            return (int) diff;
        }
#endif
    }

    /// <summary>
    /// A class for collecting an action profile, 
    /// accumulating per-action statistics, over all applications of the corresponding rule or test
    /// </summary>
    public class ActionProfile
    {
        // counts how often the action was called in total
        public long callsTotal = 0;
        // counts how many search steps were carried out over all calls during pattern matching (incl. condition evaluation and yielding)
        public long searchStepsTotal = 0;
        // counts how many loop steps of the first pattern matching loop were executed over all calls
        public long loopStepsTotal = 0;
        // counts how many search steps were carried out over all calls during attribute evaluation (while rewriting)
        public long searchStepsDuringEvalTotal = 0;
        // counts how many search steps were carried out over all calls during exec processing, incl. the effort of all called actions
        public long searchStepsDuringExecTotal = 0;

        // gives the averages, which are collected per thread, 
        // as synchronization to collect them accumulated would be overly expensive,
        // and most often, we are interested in the per-thread values anyway
        public ProfileAverages[] averagesPerThread;
    }

    /// <summary>
    /// A class for collecting average information, for profiling,
    /// per thread that was used in matching
    /// (so in case of a normal single threaded action the values characterize the action)
    /// </summary>
    public class ProfileAverages
    {
        // counts how many search steps were carried out over all calls of this thread (same as in action profile in case of a single thread)
        public long searchStepsTotal = 0;
        // counts how many loop steps of the first loop were executed over all calls of this thread (same as in action profile in case of a single thread)
        public long loopStepsTotal = 0;

        /////////////////////////////////////////////////
        // loop and search steps for the action when applied to find one match

        // computes the average of the number of steps of the first loop until a match was found or matching failed
        public readonly UberEstimator loopStepsSingle = new UberEstimator();
        // computes the average of the number of search steps until a match was found or matching failed
        public readonly UberEstimator searchStepsSingle = new UberEstimator();
        // computes the average of the number of search steps per loop step of the first loop until a match was found or matching failed
        public readonly UberEstimator searchStepsPerLoopStepSingle = new UberEstimator();

        /////////////////////////////////////////////////
        // loop and search steps for the action when applied to find more than one match (most often match-all)

        // computes the average of the number of steps of the first loop until the goal was achieved (equals count of all choices of first loop in case of match-all)
        public readonly UberEstimator loopStepsMultiple = new UberEstimator();
        // computes the average of the number of search steps until the goal was achieved
        public readonly UberEstimator searchStepsMultiple = new UberEstimator();
        // computes the average of the number of search steps per loop step of the first loop until the goal was achieved
        public readonly UberEstimator searchStepsPerLoopStepMultiple = new UberEstimator();
    }
}
