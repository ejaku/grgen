/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Threading;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A thread pool of workers for parallelized pattern matching
    /// </summary>
    public class WorkerPool
    {
        static WorkerPool()
        {
            executeParallelTask = new AutoResetEvent[0];
            parallelTaskExecuted = new ManualResetEvent[0];
            workerThreads = new Thread[0];
        }

        /// <summary>
        /// Tries to ensure the pool contains numThreads for doing work.
        /// Returns the number of threads ensured, which may be lower due to a lower number of processors available
        /// (but won't be higher, even if another caller requested and was granted more threads).
        /// </summary>
        public static int EnsurePoolSize(int numThreads)
        {
            if(numThreads <= workerThreads.Length)
                return numThreads;

            if(Environment.ProcessorCount <= workerThreads.Length)
                return Environment.ProcessorCount;

            int newNumThreads = Math.Min(numThreads, Environment.ProcessorCount);
            int oldNumThreads = workerThreads.Length;

            AutoResetEvent[] oldExecuteParallelTask = executeParallelTask;
            executeParallelTask = new AutoResetEvent[newNumThreads];
            oldExecuteParallelTask.CopyTo(executeParallelTask, 0);
            for(int i=oldNumThreads; i<newNumThreads; ++i)
                executeParallelTask[i] = new AutoResetEvent(false);

            ManualResetEvent[] oldParallelTaskExecuted = parallelTaskExecuted;
            parallelTaskExecuted = new ManualResetEvent[newNumThreads];
            oldParallelTaskExecuted.CopyTo(parallelTaskExecuted, 0);
            for(int i = oldNumThreads; i < newNumThreads; ++i)
                parallelTaskExecuted[i] = new ManualResetEvent(false);

            Thread[] oldWorkerThreads = workerThreads;
            workerThreads = new Thread[newNumThreads];
            oldWorkerThreads.CopyTo(workerThreads, 0);
            for(int i = oldNumThreads; i < newNumThreads; ++i)
            {
                workerThreads[i] = new Thread(new ThreadStart(Work));
                workerThreads[i].IsBackground = true;
                workerThreads[i].Start();
            }
            for(int i = oldNumThreads; i < newNumThreads; ++i)
            {
                while(!workerThreads[i].IsAlive) 
                    Thread.Sleep(1);
            }

            return newNumThreads;
        }

        public static int GetPoolSize()
        {
            return workerThreads.Length;
        }

        public static ThreadStart Task
        {
            set { task = value; }
        }

        public static int ThreadId
        {
            get { return threadId; }
        }

        /// <summary>
        /// Executes the work inside task with numThreads
        /// </summary>
        public static void StartWork(int numThreads)
        {
            if(numThreads > workerThreads.Length)
                throw new Exception("Too much work for the available number of worker threads.");

            threadsStarted = numThreads;
            for(int i=0; i<threadsStarted; ++i)
            	executeParallelTask[i].Set();
            for(int i=threadsStarted; i<workerThreads.Length; ++i)
            	parallelTaskExecuted[i].Set();
        }

        /// <summary>
        /// Waits until the tasks were executed
        /// </summary>
        public static void WaitForWorkDone()
        {
            ManualResetEvent.WaitAll(parallelTaskExecuted);
            for(int i=0; i<workerThreads.Length; ++i)
            	parallelTaskExecuted[i].Reset();
        }

        private static void Work()
        {
            //parallelized worker setup: await work available, work, signal work done, repeat
            threadId = Array.IndexOf<Thread>(workerThreads, Thread.CurrentThread);
            while(true)
            {
                //Console.WriteLine("fall to sleep of parallel matcher at threadId " + threadId);
                executeParallelTask[threadId].WaitOne();
                //Console.WriteLine("wakeup of parallel matcher at threadId " + threadId);

                task();

                parallelTaskExecuted[threadId].Set();
            }
        }

        private static Thread[] workerThreads;
        private static AutoResetEvent[] executeParallelTask;
        private static ManualResetEvent[] parallelTaskExecuted;
        [ThreadStatic] private static int threadId;
        private static int threadsStarted; // from StartWork till WaitForWorkDone
        private static ThreadStart task; // the task to execute in each worker (will query threadId for distinction)
    }
}
