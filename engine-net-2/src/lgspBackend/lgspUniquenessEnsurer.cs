/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.Reflection.Emit;
using System.Diagnostics;
using System.IO;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A class ensuring unique ids for nodes and edges with a minimum amount of gaps.
    /// Gets instantiated in case support for unique nodes/edges was declared in the model.
    /// </summary>
    public class LGSPUniquenessEnsurer
    {
        public LGSPUniquenessEnsurer(LGSPGraph graph)
        {
            this.graph = graph;
            graph.uniquenessEnsurer = this;

            // global counter for fetching a new unique id
            nextNewId = 0;

            // we use an array organized as heap for storing the "free list" of already allocated but currently not used ids 
            heap.Add(-1); // we start at index 1, yields simpler arithmetic

            // subscribe to events we've to listen to ensure unique ids for the graph elements with a minimum amount of gaps
            graph.OnNodeAdded += NodeAdded;
            graph.OnEdgeAdded += EdgeAdded;
            graph.OnRemovingNode += RemovingNode;
            graph.OnRemovingEdge += RemovingEdge;
            graph.OnRetypingNode += RetypingNode;
            graph.OnRetypingEdge += RetypingEdge;
        }

        public void NodeAdded(INode node)
        {
            LGSPNode nodeUnique = (LGSPNode)node;
            if(heap.Count == 1) // empty (one dummy needed for simpler arithmetic)
            {
                nodeUnique.uniqueId = nextNewId;
                ++nextNewId;

                if(graph.flagsPerThreadPerElement != null) // if not null there's some parallel matcher existing
                    EnlargeFlagsOfParallelizedMatcherAsNeeded();
            }
            else
            {
                nodeUnique.uniqueId = FetchAndRemoveMinimum();
            }
        }

        public void EdgeAdded(IEdge edge)
        {
            LGSPEdge edgeUnique = (LGSPEdge)edge;
            if(heap.Count == 1) // empty (one dummy needed for simpler arithmetic)
            {
                edgeUnique.uniqueId = nextNewId;
                ++nextNewId;
                
                if(graph.flagsPerThreadPerElement != null) // if not null there's some parallel matcher existing
                    EnlargeFlagsOfParallelizedMatcherAsNeeded();
            }
            else
            {
                edgeUnique.uniqueId = FetchAndRemoveMinimum();
            }
        }

        public void RemovingNode(INode node)
        {
            LGSPNode nodeUnique = (LGSPNode)node;
            Insert(nodeUnique.uniqueId);
            nodeUnique.uniqueId = -1;
        }

        public void RemovingEdge(IEdge edge)
        {
            LGSPEdge edgeUnique = (LGSPEdge)edge;
            Insert(edgeUnique.uniqueId);
            edgeUnique.uniqueId = -1;
        }

        public void RetypingNode(INode oldNode, INode newNode)
        {
            LGSPNode oldNodeUnique = (LGSPNode)oldNode;
            LGSPNode newNodeUnique = (LGSPNode)newNode;
            newNodeUnique.uniqueId = oldNodeUnique.uniqueId;
            oldNodeUnique.uniqueId = -1;
        }

        public void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            LGSPEdge oldEdgeUnique = (LGSPEdge)oldEdge;
            LGSPEdge newEdgeUnique = (LGSPEdge)newEdge;
            newEdgeUnique.uniqueId = oldEdgeUnique.uniqueId;
            oldEdgeUnique.uniqueId = -1;
        }

        int FetchAndRemoveMinimum()
        {
            // replace minimum with the last value
            int min = heap[1]; // position 0 is a dummy, we start at 1
            heap[1] = heap[heap.Count - 1];
            int value = heap[1];
            heap.RemoveAt(heap.Count - 1);

            if(heap.Count == 1) // we just removed the last element, only the dummy left
                return min; 

            // compute position where to really store the new maybe minimum, start with position where we just moved it to
            int pos = 1;

            // sink down as long as needed, as long as value is larger than its children, at most until the leaves are reached
            while(true)
            {
                int posOfSmallestValue = pos; // pos of smallest value of the 3: parent, left child, right child
                int posOfLeftChild = 2 * pos;
                if(posOfLeftChild < heap.Count && heap[posOfSmallestValue] > heap[posOfLeftChild])
                    posOfSmallestValue = posOfLeftChild;
                int posOfRightChild = 2 * pos + 1;
                if(posOfRightChild < heap.Count && heap[posOfSmallestValue] > heap[posOfRightChild])
                    posOfSmallestValue = posOfRightChild;

                if(posOfSmallestValue != pos) // at least one child is smaller (if both are we pick the smaller one)
                {
                    heap[pos] = heap[posOfSmallestValue]; // move value from below up
                    pos = posOfSmallestValue;
                }
                else
                    break; // the children were not smaller -> correct position reached
            }

            heap[pos] = value; // finally write value to the position where it belongs, space was freed in loop before

            return min;
        }

        public void Insert(int value)
        {
            heap.Add(-1); // enlarge array with placeholder

            // compute position where to store the new value, start with position of this insert
            int pos = heap.Count - 1;

            // bubble up, at most up to root position, as long as value is smaller than its parent
            while(pos > 1 && value < heap[pos / 2])
            {
                heap[pos] = heap[pos / 2]; // move value from above down
                pos = pos / 2;
            }

            heap[pos] = value; // finally write value to the position where it belongs, space was freed in loop before
        }

        // maintain the flags array used by the parallel matchers, that is indexed by the unique ids
        void EnlargeFlagsOfParallelizedMatcherAsNeeded()
        {
            if(graph.flagsPerThreadPerElement[0].Count == graph.flagsPerThreadPerElement[0].Capacity)
            {
                // we need more space be able to store the flags for the element currently added
                // we do this in parallel, each worker pool thread enlarges its own flags array
                if(WorkerPool.GetPoolSize() != graph.flagsPerThreadPerElement.Count)
                    throw new Exception("Internal error, number of flags arrays different from number of worker pool threads");
                WorkerPool.Task = EnlargeFlags;
                WorkerPool.StartWork(WorkerPool.GetPoolSize());
                WorkerPool.WaitForWorkDone();
            }
            else
            {
                for(int i = 0; i < graph.flagsPerThreadPerElement.Count; ++i)
                {
                    graph.flagsPerThreadPerElement[i].Add(0);
                }
            }
        }

        void EnlargeFlags()
        {
            // capacity limit reached, doubles array in size and copies all elements from old array
            graph.flagsPerThreadPerElement[WorkerPool.ThreadId].Add(0);
        }

        // maintain the flags array used by the parallel matchers, that is indexed by the unique ids
        public void InitialFillFlags(int additionalNumberOfThreads, int numberOfThreads)
        {
            if(additionalNumberOfThreads != numberOfThreads)
                throw new Exception("Different additional number of threads and number of threads currently not supported");
            if(numberOfThreads != WorkerPool.GetPoolSize())
                throw new Exception("Number of threads different from number of worker pool threads not supported");
            if(numberOfThreads != graph.flagsPerThreadPerElement.Count)
                throw new Exception("Number of threads different from number of flags arrays");
            WorkerPool.Task = InitialFillFlags;
            WorkerPool.StartWork(WorkerPool.GetPoolSize());
            WorkerPool.WaitForWorkDone();
        }

        void InitialFillFlags()
        {
            graph.flagsPerThreadPerElement[WorkerPool.ThreadId].Capacity = (graph.NumNodes + graph.NumEdges)*2;
            for(int i=0; i < graph.NumNodes+graph.NumEdges; ++i)
            {
                graph.flagsPerThreadPerElement[WorkerPool.ThreadId].Add(0);
            }
        }

        LGSPGraph graph;

        int nextNewId;

        List<int> heap = new List<int>();
    }
}