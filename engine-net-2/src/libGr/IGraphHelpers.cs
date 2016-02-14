/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A named graph-global variable.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The value pointed to by the variable.
        /// </summary>
        public object Value;

        /// <summary>
        /// Initializes a Variable instance.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value pointed to by the variable.</param>
        public Variable(String name, object value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// The changes which might occur to graph element attributes.
    /// </summary>
    public enum AttributeChangeType
    {
        /// <summary>
        /// Assignment of a value to some attribute.
        /// Value semantics, even if assigned attribute is a set or a map, not a primitive type.
        /// </summary>
        Assign,

        /// <summary>
        /// Inserting a value into some set or a key value pair into some map or a value into some array.
        /// </summary>
        PutElement,

        /// <summary>
        /// Removing a value from some set or a key value pair from some map or a key/index from some array.
        /// </summary>
        RemoveElement,

        /// <summary>
        /// Assignment of a value to a key/index position in an array, overwriting old element at that position.
        /// </summary>
        AssignElement
    }

    /// <summary>
    /// An interface for undo items of the transaction manager.
    /// Allows to register own ones for external types, will be called on rollback.
    /// </summary>
    public interface IUndoItem
    {
        /// <summary>
        /// Called on rollback by the transaction manager,
        /// in order to undo the effects of some change it was created for.
        /// </summary>
        /// <param name="procEnv">The current graph processing environment</param>
        void DoUndo(IGraphProcessingEnvironment procEnv);

        // Note that ToString() is called by the dump command of the debugger, 
        // writing the output of ToString to the transaction log file (allows to inspect the current state of the undo log).
    }

    /// <summary>
    /// An interface for managing graph transactions.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Starts a transaction
        /// </summary>
        /// <returns>A transaction ID to be used with Commit or Rollback</returns>
        int Start();

        /// <summary>
        /// Pauses the running transactions,
        /// i.e. changes done from now on until resume won't be undone in case of a rollback
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the running transactions after a pause,
        /// i.e. changes done from now on will be undone again in case of a rollback
        /// </summary>
        void Resume();

        /// <summary>
        /// Removes the rollback data and stops this transaction
        /// </summary>
        /// <param name="transactionID">Transaction ID returned by a Start call</param>
        void Commit(int transactionID);

        /// <summary>
        /// Undoes all changes during a transaction
        /// </summary>
        /// <param name="transactionID">The ID of the transaction to be rollbacked</param>
        void Rollback(int transactionID);

        /// <summary>
        /// Indicates, whether a transaction is currently active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Registers an undo item to be called on rollback,
        /// for reverting the changes applied to some graph element attribute of external type.
        /// Only in case a transaction is underway and not paused (and not rolling back) is the undo item added to the undo log,
        /// so you may call this method simply on each change to an external type.
        /// </summary>
        /// <param name="item">An object that is capable of undoing the effects of some change it was created for in case of rollback</param>
        void ExternalTypeChanged(IUndoItem item);
    }

    /// <summary>
    /// An interface for recording changes (and their causes) applied to a graph into a file,
    /// so that they can get replayed.
    /// </summary>
    public interface IRecorder
    {
        /// <summary>
        /// Creates a file which initially gets filled with a .grs export of the graph.
        /// Afterwards the changes applied to the graph are recorded into the file,
        /// in the order they occur.
        /// You can start multiple recordings into differently named files.
        /// </summary>
        /// <param name="filename">The name of the file to record to</param>
        void StartRecording(string filename);

        /// <summary>
        /// Stops recording of the changes applied to the graph to the given file.
        /// </summary>
        /// <param name="filename">The name of the file to stop recording to</param>
        void StopRecording(string filename);

        /// <summary>
        /// Returns whether the graph changes get currently recorded into the given file.
        /// </summary>
        /// <param name="filename">The name of the file whose recording status gets queried</param>
        /// <returns>The recording status of the file queried</returns>
        bool IsRecording(string filename);

        /// <summary>
        /// Writes a line starting with "external ", ending with a new line, 
        /// containing the given string in between (which must not contain "\n" or "\r"),
        /// to the currently ongoing recordings.
        /// This is the format expected for fine-grain external attribute changes, 
        /// the string is passed to External of the graph model on replaying, 
        /// which is forwarding it to external user code for parsing and executing the changes.
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        void External(string value);

        /// <summary>
        /// Writes the given string to the currently ongoing recordings
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        void Write(string value);

        /// <summary>
        /// Writes the given string to the currently ongoing recordings followed by a new line
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        void WriteLine(string value);

        /// <summary>
        /// Flushes the writer
        /// </summary>
        void Flush();

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Called by the transaction manager when a transaction is started
        /// </summary>
        /// <param name="transactionID">The id of the transaction</param>
        void TransactionStart(int transactionID);

        /// <summary>
        /// Called by the transaction manager when a transaction is committed
        /// </summary>
        /// <param name="transactionID">The id of the transaction</param>
        void TransactionCommit(int transactionID);

        /// <summary>
        /// Called by the transaction manager when a transaction is rolled back
        /// </summary>
        /// <param name="transactionID">The id of the transaction</param>
        /// <param name="start">true when called at rollback start, false when called at rollback end</param>
        void TransactionRollback(int transactionID, bool start);
    }

    /// <summary>
    /// The different graph validation modes
    /// </summary>
    public enum ValidationMode
    {
        OnlyMultiplicitiesOfMatchingTypes, // check the multiplicities of the incoming/outgoing edges which match the types specified
        StrictOnlySpecified, // as first and additionally check that edges with connections assertions specified are covered by at least on connection assertion
        Strict // as first and additionally check that all edges are covered by at least one connection assertion
    }

    /// <summary>
    /// An object accumulating information about needed time, number of found matches and number of performed rewrites.
    /// </summary>
    public class PerformanceInfo
    {
        /// <summary>
        /// Stores a profile per action (given by name, that gives the average for the loop and search steps needed to achieve the goal or finally fail)
        /// </summary>
        public Dictionary<string, ActionProfile> ActionProfiles = new Dictionary<string, ActionProfile>();

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

        private Stopwatch stopwatch = new Stopwatch();
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

#if DEBUGACTIONS || MATCHREWRITEDETAIL
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
        public UberEstimator loopStepsSingle = new UberEstimator();
        // computes the average of the number of search steps until a match was found or matching failed
        public UberEstimator searchStepsSingle = new UberEstimator();
        // computes the average of the number of search steps per loop step of the first loop until a match was found or matching failed
        public UberEstimator searchStepsPerLoopStepSingle = new UberEstimator();

        /////////////////////////////////////////////////
        // loop and search steps for the action when applied to find more than one match (most often match-all)

        // computes the average of the number of steps of the first loop until the goal was achieved (equals count of all choices of first loop in case of match-all)
        public UberEstimator loopStepsMultiple = new UberEstimator();
        // computes the average of the number of search steps until the goal was achieved
        public UberEstimator searchStepsMultiple = new UberEstimator();
        // computes the average of the number of search steps per loop step of the first loop until the goal was achieved
        public UberEstimator searchStepsPerLoopStepMultiple = new UberEstimator();
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
