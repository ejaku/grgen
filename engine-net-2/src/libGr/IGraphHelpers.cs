/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
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
    /// An interface for managing graph transactions.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Starts a transaction
        /// </summary>
        /// <returns>A transaction ID to be used with Commit or Rollback</returns>
        int StartTransaction();

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
        /// <param name="transactionID">Transaction ID returned by a StartTransaction call</param>
        void Commit(int transactionID);

        /// <summary>
        /// Undoes all changes during a transaction
        /// </summary>
        /// <param name="transactionID">The ID of the transaction to be rollbacked</param>
        void Rollback(int transactionID);

        /// <summary>
        /// Indicates, whether a transaction is currently active.
        /// </summary>
        bool TransactionActive { get; }
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
