/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Threading;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A thread pool for parallel sequence execution
    /// </summary>
    public static class ThreadPool
    {
        static ThreadPool()
        {
            executeParallelTask = new AutoResetEvent[0];
            parallelTaskExecuted = new ManualResetEvent[0];
            workerThreads = new Thread[0];
            tasks = new ParameterizedThreadStart[0];
            freeThreads = new List<int>();
        }

        /// <summary>
        /// Tries to set the pool size to numThreads for doing work.
        /// Returns the number of threads really set, which may be lower due to a lower number of processors available.
        /// Only to be called once.
        /// </summary>
        public static int SetPoolSize(int numThreads)
        {
            if(poolSizeSet)
                throw new Exception("Pool size was already set.");

            poolSizeSet = true;

            if(numThreads == 0)
                return 0;

            numThreads = Math.Min(numThreads, Environment.ProcessorCount);

            executeParallelTask = new AutoResetEvent[numThreads];
            for(int i = 0; i < numThreads; ++i)
            {
                executeParallelTask[i] = new AutoResetEvent(false);
            }

            parallelTaskExecuted = new ManualResetEvent[numThreads];
            for(int i = 0; i < numThreads; ++i)
            {
                parallelTaskExecuted[i] = new ManualResetEvent(false);
            }

            tasks = new ParameterizedThreadStart[numThreads];
            arguments = new object[numThreads];

            freeThreads.Clear();
            for(int i = 0; i < numThreads; ++i)
            {
                freeThreads.Add(i);
            }

            workerThreads = new Thread[numThreads];
            for(int i = 0; i < numThreads; ++i)
            {
                workerThreads[i] = new Thread(new ThreadStart(Work));
                workerThreads[i].IsBackground = true;
                workerThreads[i].Start();
            }
            for(int i = 0; i < numThreads; ++i)
            {
                while(!workerThreads[i].IsAlive)
                {
                    Thread.Sleep(1);
                }
            }

            return numThreads;
        }

        public static bool PoolSizeWasSet
        {
            get { return poolSizeSet; }
        }

        public static int PoolSize
        {
            get { return workerThreads.Length; }
        }

        /// <summary>
        /// The internal thread id, not the ManagedThreadId of the thread.
        /// </summary>
        public static int ThreadId
        {
            get { return threadIndex; }
        }

        /// <summary>
        /// Tries to fetch a worker, if this fails (because the thread pool is depleted) -1 is returned.
        /// Otherwise, a worker starts executing the given task, and its internal thread id is returned.
        /// </summary>
        public static int FetchWorkerAndExecuteWork(ParameterizedThreadStart task, object argument)
        {
            int freeThreadIndex = -1;
            lock(freeThreads)
            {
                if(freeThreads.Count == 0)
                    return -1;

                int lastElementIndex = freeThreads.Count - 1;
                freeThreadIndex = freeThreads[lastElementIndex];
                freeThreads.RemoveAt(lastElementIndex);

                ++threadsStarted;
            }

            tasks[freeThreadIndex] = task;
            arguments[freeThreadIndex] = argument;

            executeParallelTask[freeThreadIndex].Set();

            return freeThreadIndex;
        }

        /// <summary>
        /// Waits until the thread of the given internal thread id completed executing its task
        /// </summary>
        public static void WaitForWorkDone(int threadIndex)
        {
            parallelTaskExecuted[threadIndex].WaitOne();
            parallelTaskExecuted[threadIndex].Reset();

            lock(freeThreads)
            {
                --threadsStarted;

                freeThreads.Add(threadIndex);
            }
        }

        private static void Work()
        {
            //parallelized worker setup: await work available, work, signal work done, repeat
            threadIndex = Array.IndexOf<Thread>(workerThreads, Thread.CurrentThread);
            while(true)
            {
                //ConsoleUI.outWriter.WriteLine("fall to sleep of worker thread " + threadId);
                executeParallelTask[threadIndex].WaitOne();
                //ConsoleUI.outWriter.WriteLine("wakeup of worker thread " + threadId);

                tasks[threadIndex](arguments[threadIndex]);

                parallelTaskExecuted[threadIndex].Set();
            }
        }

        private static Thread[] workerThreads;
        private static AutoResetEvent[] executeParallelTask;
        private static ManualResetEvent[] parallelTaskExecuted;
        [ThreadStatic] private static int threadIndex;
        private volatile static int threadsStarted; // from FetchWorkerAndExecuteWork till WaitForWorkDone
        private volatile static ParameterizedThreadStart[] tasks; // the task to execute, per worker
        private volatile static object[] arguments; // the argument to the parameterized thread start, per worker
        private volatile static List<int> freeThreads; // an array of the available threads (currently not carrying out work)
        private volatile static bool poolSizeSet;
    }
}
