/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A class ensuring unique ids for nodes and edges with a minimum amount of gaps.
    /// Gets instantiated in case support for unique nodes/edges was declared in the model.
    /// </summary>
    public class LGSPUniquenessEnsurer : IUniquenessHandler
    {
        public LGSPUniquenessEnsurer(LGSPGraph graph)
        {
            if(!graph.Model.GraphElementUniquenessIsEnsured)
                throw new Exception("Internal error, uniqueness ensurer constructed although uniqueness was not requested");

            if(graph.NumNodes > 0 || graph.NumEdges > 0)
                throw new Exception("Graph must be empty!");

            this.graph = graph;
            
            // global counter for fetching a new unique id
            nextNewId = 0;

            // we use an array organized as heap for storing the "free list" of already allocated but currently not used ids 
            heap.Add(-1); // we start at index 1, yields simpler arithmetic

            // subscribe to events we've to listen to ensure unique ids for the graph elements with a minimum amount of gaps
            graph.OnClearingGraph += ClearingGraph;
            graph.OnNodeAdded += NodeAdded;
            graph.OnEdgeAdded += EdgeAdded;
            graph.OnRemovingNode += RemovingNode;
            graph.OnRemovingEdge += RemovingEdge;
            graph.OnRetypingNode += RetypingNode;
            graph.OnRetypingEdge += RetypingEdge;
        }

        public virtual void FillAsClone(LGSPUniquenessEnsurer original, IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            nextNewId = original.nextNewId;

            heap.Clear(); // remove the -1
            heap.Capacity = original.heap.Capacity;
            heap.AddRange(original.heap);
        }

        public virtual void ClearingGraph(IGraph graph)
        {
            nextNewId = 0;

            heap.Clear();
            heap.Add(-1); // we start at index 1, yields simpler arithmetic
        }

        public virtual void NodeAdded(INode node)
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

        public virtual void EdgeAdded(IEdge edge)
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

        public virtual void RemovingNode(INode node)
        {
            LGSPNode nodeUnique = (LGSPNode)node;
            Insert(nodeUnique.uniqueId);
            //nodeUnique.uniqueId = -1; an index needs to access the old id
        }

        public virtual void RemovingEdge(IEdge edge)
        {
            LGSPEdge edgeUnique = (LGSPEdge)edge;
            Insert(edgeUnique.uniqueId);
            //edgeUnique.uniqueId = -1; an index needs to access the old id
        }

        public virtual void RetypingNode(INode oldNode, INode newNode)
        {
            LGSPNode oldNodeUnique = (LGSPNode)oldNode;
            LGSPNode newNodeUnique = (LGSPNode)newNode;
            newNodeUnique.uniqueId = oldNodeUnique.uniqueId;
            //oldNodeUnique.uniqueId = -1; an index needs to access the old id
        }

        public virtual void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            LGSPEdge oldEdgeUnique = (LGSPEdge)oldEdge;
            LGSPEdge newEdgeUnique = (LGSPEdge)newEdge;
            newEdgeUnique.uniqueId = oldEdgeUnique.uniqueId;
            //oldEdgeUnique.uniqueId = -1; an index needs to access the old id
        }

        protected int FetchAndRemoveMinimum()
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
        protected void EnlargeFlagsOfParallelizedMatcherAsNeeded()
        {
            if(graph.flagsPerThreadPerElement[0].Count == graph.flagsPerThreadPerElement[0].Capacity)
            {
                // we need more space to be able to store the flags for the element currently added
                // we do this in parallel, each worker pool thread enlarges its own flags array
                if(graph.flagsPerThreadPerElement.Count > WorkerPool.PoolSize)
                    throw new Exception("Internal error, number of flags arrays higher than number of worker pool threads");
                WorkerPool.Task = EnlargeFlags;
                WorkerPool.StartWork(graph.flagsPerThreadPerElement.Count);
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
            if(numberOfThreads > WorkerPool.PoolSize)
                throw new Exception("Internal error, number of threads announced to flags array filling is higher than number of worker pool threads");
            if(numberOfThreads != graph.flagsPerThreadPerElement.Count)
                throw new Exception("Internal error, number of threads different from number of flags arrays");
            WorkerPool.Task = InitialFillFlags;
            WorkerPool.StartWork(numberOfThreads);
            WorkerPool.WaitForWorkDone();
        }

        void InitialFillFlags()
        {
            // note: we enlarge all threads to the same value, even flags that already exist, cause all must have the same capacity 
            // (EnlargeFlagsOfParallelizedMatcherAsNeeded would not work otherwise); we just use twice the count reached (queryied via nextNewId), to increase waiting time
            graph.flagsPerThreadPerElement[WorkerPool.ThreadId].Capacity = Math.Max(nextNewId, 2) * 2;
            for(int i=graph.flagsPerThreadPerElement[WorkerPool.ThreadId].Count; i < nextNewId; ++i)
            {
                graph.flagsPerThreadPerElement[WorkerPool.ThreadId].Add(0);
            }
        }

        protected readonly LGSPGraph graph;

        protected int nextNewId;

        protected List<int> heap = new List<int>();
    }

    /// <summary>
    /// A class ensuring unique ids for nodes and edges with a minimum amount of gaps,
    /// and allowing to access them by their unique id, similar to an index (and esp. the name map).
    /// Gets instantiated in case support for unique nodes/edges was declared in the model,
    /// and support for accessing nodes/edges by a unique id.
    /// </summary>
    public class LGSPUniquenessIndex : LGSPUniquenessEnsurer
    {
        public LGSPUniquenessIndex(LGSPGraph graph)
            : base(graph)
        {
            if(!graph.Model.GraphElementsAreAccessibleByUniqueId)
                throw new Exception("Internal error, uniqueness index constructed although access by unique id was not requested");

            index = new List<IGraphElement>();
        }

        public override void ClearingGraph(IGraph graph)
        {
            base.ClearingGraph(graph);

            index.Clear();
        }

        public override void FillAsClone(LGSPUniquenessEnsurer original, IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            base.FillAsClone(original, oldToNewMap);
 
            LGSPUniquenessIndex originalIndex = (LGSPUniquenessIndex)original;

            if(originalIndex.index != null)
            {
                index = new List<IGraphElement>(originalIndex.index.Capacity);
                for(int i = 0; i < originalIndex.index.Count; ++i)
                {
                    index.Add(oldToNewMap[originalIndex.index[i]]);
                }
            }
        }

        public override void NodeAdded(INode node)
        {
            LGSPNode nodeUnique = (LGSPNode)node;
            if(heap.Count == 1) // empty (one dummy needed for simpler arithmetic)
            {
                nodeUnique.uniqueId = nextNewId;
                ++nextNewId;

                index.Add(node);
                if(graph.flagsPerThreadPerElement != null) // if not null there's some parallel matcher existing
                    EnlargeFlagsOfParallelizedMatcherAsNeeded();
            }
            else
            {
                nodeUnique.uniqueId = FetchAndRemoveMinimum();
                index[nodeUnique.uniqueId] = nodeUnique;
            }
        }

        public override void EdgeAdded(IEdge edge)
        {
            LGSPEdge edgeUnique = (LGSPEdge)edge;
            if(heap.Count == 1) // empty (one dummy needed for simpler arithmetic)
            {
                edgeUnique.uniqueId = nextNewId;
                ++nextNewId;

                index.Add(edge);
                if(graph.flagsPerThreadPerElement != null) // if not null there's some parallel matcher existing
                    EnlargeFlagsOfParallelizedMatcherAsNeeded();
            }
            else
            {
                edgeUnique.uniqueId = FetchAndRemoveMinimum();
                index[edgeUnique.uniqueId] = edgeUnique;
            }
        }

        public override void RemovingNode(INode node)
        {
            LGSPNode nodeUnique = (LGSPNode)node;
            Insert(nodeUnique.uniqueId);
            //nodeUnique.uniqueId = -1; an index needs to access the old id
            index[nodeUnique.uniqueId] = null;
        }

        public override void RemovingEdge(IEdge edge)
        {
            LGSPEdge edgeUnique = (LGSPEdge)edge;
            Insert(edgeUnique.uniqueId);
            //edgeUnique.uniqueId = -1; an index needs to access the old id
            index[edgeUnique.uniqueId] = null;
        }

        public override void RetypingNode(INode oldNode, INode newNode)
        {
            LGSPNode oldNodeUnique = (LGSPNode)oldNode;
            LGSPNode newNodeUnique = (LGSPNode)newNode;
            newNodeUnique.uniqueId = oldNodeUnique.uniqueId;
            //oldNodeUnique.uniqueId = -1; an index needs to access the old id
            index[newNodeUnique.uniqueId] = newNode;
        }

        public override void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            LGSPEdge oldEdgeUnique = (LGSPEdge)oldEdge;
            LGSPEdge newEdgeUnique = (LGSPEdge)newEdge;
            newEdgeUnique.uniqueId = oldEdgeUnique.uniqueId;
            //oldEdgeUnique.uniqueId = -1; an index needs to access the old id
            index[newEdgeUnique.uniqueId] = newEdge;
        }

        public List<IGraphElement> index;
    }
}
