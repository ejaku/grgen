/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
// if you uncomment this, take care to also uncomment the same define in GraphStatisticsParserSerializer.cs
// todo: add import/export for 4-dim array instead of one-dim with manual index computations

using System;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public enum LGSPDirection { In, Out };

    /// <summary>
    /// A class for analyzing a graph and storing the statistics about the graph
    /// </summary>
    public class LGSPGraphStatistics
    {
        public readonly IGraphModel graphModel;

#if MONO_MULTIDIMARRAY_WORKAROUND
        public int dim0size, dim1size, dim2size;  // dim3size is always 2
        public int[] vstructs;
#else
        public int[, , ,] vstructs;
#endif

        /// <summary>
        /// The number of compatible nodes in the graph for each type at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public int[] nodeCounts;

        /// <summary>
        /// The number of compatible edges in the graph for each type at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public int[] edgeCounts;

        /// <summary>
        /// The number of edges going out for each node type at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public int[] outCounts;

        /// <summary>
        /// The number of edges coming in for each node type at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public int[] inCounts;
        
        /// <summary>
        /// The mean out degree (independent of edge types) of the nodes of a graph for each node type
        /// at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public float[] meanOutDegree;

        /// <summary>
        /// The mean in degree (independent of edge types) of the nodes of a graph for each node type
        /// at the time of the last analysis.
        /// It is null, if no analysis has been executed, yet.
        /// </summary>
        public float[] meanInDegree;


        /// <summary>
        /// Create the statistics class, binding it to the graph model
        /// </summary>
        public LGSPGraphStatistics(IGraphModel graphModel)
        {
            this.graphModel = graphModel;
        }

        /// <summary>
        /// Copy constructor helper.
        /// </summary>
        /// <param name="dataSource">The LGSPGraph object to get the data from</param>
        public void Copy(LGSPGraph dataSource)
        {
#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = dataSource.statistics.dim0size;
            dim1size = dataSource.statistics.dim1size;
            dim2size = dataSource.statistics.dim2size;
            if(dataSource.statistics.vstructs != null)
                vstructs = (int[])dataSource.statistics.vstructs.Clone();
#else
            if(vstructs != null)
                vstructs = (int[ , , , ]) vstructs.Clone();
#endif
            if(dataSource.statistics.nodeCounts != null)
                nodeCounts = (int[])dataSource.statistics.nodeCounts.Clone();
            if(dataSource.statistics.edgeCounts != null)
                edgeCounts = (int[])dataSource.statistics.edgeCounts.Clone();
            if(dataSource.statistics.outCounts != null)
                outCounts = (int[])dataSource.statistics.outCounts.Clone();
            if(dataSource.statistics.inCounts != null)
                inCounts = (int[])dataSource.statistics.inCounts.Clone();
            if(dataSource.statistics.meanInDegree != null)
                meanInDegree = (float[])dataSource.statistics.meanInDegree.Clone();
            if(dataSource.statistics.meanOutDegree != null)
                meanOutDegree = (float[])dataSource.statistics.meanOutDegree.Clone();
        }

        public void ResetStatisticalData()
        {
#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = dim1size = dim2size = 0;
#endif
            vstructs = null;
            nodeCounts = null;
            edgeCounts = null;
            outCounts = null;
            inCounts = null;
            meanInDegree = null;
            meanOutDegree = null;
        }

        /// <summary>
        /// Analyzes the graph.
        /// The calculated data is used to generate good searchplans for the current graph.
        /// To be called from the graph, not directly, to ensure the changes counter is correctly set.
        /// </summary>
        public void AnalyzeGraph(LGSPGraph graph)
        {
            if(graph.Model != graphModel)
                throw new Exception("Mismatch between model bound to statistics and model in graph to be analyzed!");

            int numNodeTypes = graph.Model.NodeModel.Types.Length;
            int numEdgeTypes = graph.Model.EdgeModel.Types.Length;

            int[,] outgoingVCount = new int[numEdgeTypes, numNodeTypes];
            int[,] incomingVCount = new int[numEdgeTypes, numNodeTypes];

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = numNodeTypes;
            dim1size = numEdgeTypes;
            dim2size = numNodeTypes;
            vstructs = new int[numNodeTypes * numEdgeTypes * numNodeTypes * 2];
#else
            vstructs = new int[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
#endif
            nodeCounts = new int[numNodeTypes];
            edgeCounts = new int[numEdgeTypes];
            outCounts = new int[numNodeTypes];
            inCounts = new int[numNodeTypes];
            meanInDegree = new float[numNodeTypes];
            meanOutDegree = new float[numNodeTypes];

            foreach(NodeType nodeType in graph.Model.NodeModel.Types)
            {
                // Calculate nodeCounts
                foreach(NodeType superType in nodeType.SuperOrSameTypes)
                {
                    nodeCounts[superType.TypeID] += graph.nodesByTypeCounts[nodeType.TypeID];
                }

                for(LGSPNode nodeHead = graph.nodesByTypeHeads[nodeType.TypeID], node = nodeHead.lgspTypeNext; node != nodeHead; node = node.lgspTypeNext)
                {
                    InitializeOutgoingVStructuresCount(numNodeTypes, numEdgeTypes, outgoingVCount);

                    LGSPEdge outhead = node.lgspOuthead;
                    CountOutgoingVStructures(outgoingVCount, nodeType, outhead);

                    InitializeIncomingVStructuresCount(numNodeTypes, numEdgeTypes, incomingVCount);

                    LGSPEdge inhead = node.lgspInhead;
                    CountIncomingVStructures(incomingVCount, nodeType, inhead);

                    WriteVStructuresOutgoing(outhead, outgoingVCount, nodeType);

                    WriteVStructuresIncoming(inhead, incomingVCount, nodeType);
                }

                int numCompatibleNodes = nodeCounts[nodeType.TypeID];
                if(numCompatibleNodes != 0)
                {
                    meanOutDegree[nodeType.TypeID] = outCounts[nodeType.TypeID] / numCompatibleNodes;
                    meanInDegree[nodeType.TypeID] = inCounts[nodeType.TypeID] / numCompatibleNodes;
                }
                else
                {
                    meanOutDegree[nodeType.TypeID] = 0;
                    meanInDegree[nodeType.TypeID] = 0;
                }
            }

            // Calculate edgeCounts
            foreach(EdgeType edgeType in graph.Model.EdgeModel.Types)
            {
                foreach(EdgeType superType in edgeType.superOrSameTypes)
                {
                    edgeCounts[superType.TypeID] += graph.edgesByTypeCounts[edgeType.TypeID];
                }
            }
        }

        private void InitializeOutgoingVStructuresCount(int numNodeTypes, int numEdgeTypes, int[,] outgoingVCount)
        {
            for(int i = 0; i < numEdgeTypes; i++)
            {
                for(int j = 0; j < numNodeTypes; j++)
                {
                    outgoingVCount[i, j] = 0;
                }
            }
        }

        private void CountOutgoingVStructures(int[,] outgoingVCount, NodeType nodeType, LGSPEdge outhead)
        {
            if(outhead == null)
                return;

            LGSPEdge edge = outhead;
            do
            {
                NodeType targetType = edge.lgspTarget.lgspType;
                outCounts[nodeType.TypeID]++;
                foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                {
                    int superTypeID = edgeSuperType.TypeID;
                    foreach(NodeType targetSuperType in targetType.SuperOrSameTypes)
                    {
                        outgoingVCount[superTypeID, targetSuperType.TypeID]++;
                    }
                }
                edge = edge.lgspOutNext;
            }
            while(edge != outhead);
        }

        private void InitializeIncomingVStructuresCount(int numNodeTypes, int numEdgeTypes, int[,] incomingVCount)
        {
            for(int i = 0; i < numEdgeTypes; i++)
            {
                for(int j = 0; j < numNodeTypes; j++)
                {
                    incomingVCount[i, j] = 0;
                }
            }
        }

        private void CountIncomingVStructures(int[,] incomingVCount, NodeType nodeType, LGSPEdge inhead)
        {
            if(inhead == null)
                return;

            LGSPEdge edge = inhead;
            do
            {
                NodeType sourceType = edge.lgspSource.lgspType;
                inCounts[nodeType.TypeID]++;
                foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                {
                    int superTypeID = edgeSuperType.TypeID;
                    foreach(NodeType sourceSuperType in sourceType.superOrSameTypes)
                    {
                        incomingVCount[superTypeID, sourceSuperType.TypeID]++;
                    }
                }
                edge = edge.lgspInNext;
            }
            while(edge != inhead);
        }

        private void WriteVStructuresOutgoing(LGSPEdge outhead, int[,] outgoingVCount, NodeType nodeType)
        {
            if(outhead == null)
                return;

            LGSPEdge edge = outhead;
            do
            {
                NodeType targetType = edge.lgspTarget.lgspType;
                int targetTypeID = targetType.TypeID;

                foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                {
                    int edgeSuperTypeID = edgeSuperType.TypeID;

                    foreach(NodeType targetSuperType in targetType.superOrSameTypes)
                    {
                        int targetSuperTypeID = targetSuperType.TypeID;
                        if(outgoingVCount[edgeSuperTypeID, targetSuperTypeID] > 0)
                        {
                            // int val = (float) Math.Log(outgoingVCount[edgeSuperTypeID, targetSuperTypeID]);     // > 1 im if
                            int val = outgoingVCount[edgeSuperTypeID, targetSuperTypeID];
                            foreach(NodeType nodeSuperType in nodeType.superOrSameTypes)
                            {
#if MONO_MULTIDIMARRAY_WORKAROUND
                                vstructs[((nodeSuperType.TypeID * dim1size + edgeSuperTypeID) * dim2size + targetSuperTypeID) * 2
                                    + (int)LGSPDirection.Out] += val;
#else
                                vstructs[nodeSuperType.TypeID, edgeSuperTypeID, targetSuperTypeID, (int) LGSPDirection.Out] += val;
#endif
                            }
                            outgoingVCount[edgeSuperTypeID, targetSuperTypeID] = 0;
                        }
                    }
                }
                edge = edge.lgspOutNext;
            }
            while(edge != outhead);
        }

        private void WriteVStructuresIncoming(LGSPEdge inhead, int[,] incomingVCount, NodeType nodeType)
        {
            if(inhead == null)
                return;

            LGSPEdge edge = inhead;
            do
            {
                NodeType sourceType = edge.lgspSource.lgspType;
                int sourceTypeID = sourceType.TypeID;

                foreach(EdgeType edgeSuperType in edge.lgspType.superOrSameTypes)
                {
                    int edgeSuperTypeID = edgeSuperType.TypeID;
                    foreach(NodeType sourceSuperType in sourceType.superOrSameTypes)
                    {
                        int sourceSuperTypeID = sourceSuperType.TypeID;
                        if(incomingVCount[edgeSuperTypeID, sourceSuperTypeID] > 0)
                        {
                            // int val = (float) Math.Log(incomingVCount[edgeSuperTypeID, sourceSuperTypeID]);     // > 1 im if
                            int val = incomingVCount[edgeSuperTypeID, sourceSuperTypeID];
                            foreach(NodeType nodeSuperType in nodeType.superOrSameTypes)
                            {
#if MONO_MULTIDIMARRAY_WORKAROUND
                                vstructs[((nodeSuperType.TypeID * dim1size + edgeSuperTypeID) * dim2size + sourceSuperTypeID) * 2
                                    + (int)LGSPDirection.In] += val;
#else
                                vstructs[nodeSuperType.TypeID, edgeSuperTypeID, sourceSuperTypeID, (int) LGSPDirection.In] += val;
#endif
                            }
                            incomingVCount[edgeSuperTypeID, sourceSuperTypeID] = 0;
                        }
                    }
                }
                edge = edge.lgspInNext;
            }
            while(edge != inhead);
        }
    }
}
