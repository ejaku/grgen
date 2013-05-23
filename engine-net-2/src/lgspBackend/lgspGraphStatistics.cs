/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
//#define OPCOST_WITH_GEO_MEAN

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public enum LGSPDir { In, Out };

    /// <summary>
    /// An implementation of the IGraph interface.
    /// </summary>
    public partial class LGSPGraph : BaseGraph
    {
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

#if OPCOST_WITH_GEO_MEAN
        public float[] nodeLookupCosts, edgeLookupCosts;
#endif

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


#if USE_SUB_SUPER_ENUMERATORS
        public void AnalyseGraph()
        {
            if(changesCounterAtLastAnalyze == changesCounter)
                return;
            changesCounterAtLastAnalyze = changesCounter;

            int numNodeTypes = Model.NodeModel.Types.Length;
            int numEdgeTypes = Model.EdgeModel.Types.Length;

            int[,] outgoingVCount = new int[numEdgeTypes, numNodeTypes];
            int[,] incomingVCount = new int[numEdgeTypes, numNodeTypes];

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = numNodeTypes;
            dim1size = numEdgeTypes;
            dim2size = numNodeTypes;
            vstructs = new float[numNodeTypes*numEdgeTypes*numNodeTypes*2];
#else
            vstructs = new float[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
#endif
            nodeCounts = new int[numNodeTypes];
            edgeCounts = new int[numEdgeTypes];

            foreach(ITypeFramework nodeType in Model.NodeModel.Types)
            {
                foreach(ITypeFramework superType in nodeType.SuperOrSameTypes)
                    nodeCounts[superType.typeID] += nodes[superType.typeID].Count;

                for(LGSPNode nodeHead = nodes[nodeType.typeID].Head, node = nodeHead.next; node != nodeHead; node = node.next)
                {
                    // count outgoing v structures
                    outgoingVCount.Initialize();
                    for(LGSPEdge edgeHead = node.outgoing.Head, edge = edgeHead.outNode.next; edge != edgeHead; edge = edge.outNode.next)
                    {
                        ITypeFramework targetType = edge.target.type;
//                        outgoingVCount[edge.type.TypeID, nodeType.TypeID]++;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;
                            foreach(ITypeFramework targetSuperType in targetType.SuperOrSameTypes)
                            {
                                outgoingVCount[superTypeID, targetSuperType.typeID]++;
                            }
                        }
                    }

                    // count incoming v structures
                    incomingVCount.Initialize();
                    for(LGSPEdge edgeHead = node.incoming.Head, edge = edgeHead.inNode.next; edge != edgeHead; edge = edge.inNode.next)
                    {
                        ITypeFramework sourceType = edge.source.type;
//                        incomingVCount[edge.type.TypeID, nodeType.TypeID]++;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;
                            foreach(ITypeFramework sourceSuperType in sourceType.SuperOrSameTypes)
                            {
                                incomingVCount[superTypeID, sourceSuperType.typeID]++;
                            }
                        }
                    }

                    // finalize the counting and collect resulting local v-struct info

                    for(LGSPEdge edgeHead = node.outgoing.Head, edge = edgeHead.outNode.next; edge != edgeHead; edge = edge.outNode.next)
                    {
                        ITypeFramework targetType = edge.target.type;
                        int targetTypeID = targetType.typeID;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;

                            foreach(ITypeFramework targetSuperType in targetType.SuperOrSameTypes)
                            {
                                if(outgoingVCount[superTypeID, targetSuperType.typeID] > 1)
                                {
                                    float val = (float) Math.Log(outgoingVCount[superTypeID, targetSuperType.typeID]);
                                    foreach(ITypeFramework nodeSuperType in nodeType.SuperOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
//                                        vstructs[((superTypeID * dim1size + nodeSuperType.TypeID) * dim2size + targetTypeID) * 2
//                                            + (int) LGSPDir.Out] += val;
                                        vstructs[((nodeSuperType.typeID * dim1size + superTypeID) * dim2size + targetTypeID) * 2
                                            + (int) LGSPDir.Out] += val;
#else
//                                        vstructs[superTypeID, nodeSuperType.TypeID, targetTypeID, (int) LGSPDir.Out] += val;
                                        vstructs[nodeSuperType.TypeID, superTypeID, targetTypeID, (int) LGSPDir.Out] += val;
#endif
                                    outgoingVCount[superTypeID, targetSuperType.typeID] = 0;
                                }
                            }
                        }
                    }

                    for(LGSPEdge edgeHead = node.incoming.Head, edge = edgeHead.inNode.next; edge != edgeHead; edge = edge.inNode.next)
                    {
                        ITypeFramework sourceType = edge.source.type;
                        int sourceTypeID = sourceType.typeID;

                        foreach(ITypeFramework edgeSuperType in edge.Type.SuperOrSameTypes)
                        {
                            int superTypeID = edgeSuperType.typeID;
                            foreach(ITypeFramework sourceSuperType in sourceType.SuperOrSameTypes)
                            {
                                if(incomingVCount[superTypeID, sourceSuperType.typeID] > 1)
                                {
                                    float val = (float) Math.Log(incomingVCount[superTypeID, sourceSuperType.typeID]);

                                    foreach(ITypeFramework nodeSuperType in nodeType.SuperOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
//                                        vstructs[((superTypeID * dim1size + nodeSuperType.TypeID) * dim2size + sourceTypeID) * 2
//                                            + (int) LGSPDir.In] += val;
                                        vstructs[((nodeSuperType.typeID * dim1size + superTypeID) * dim2size + sourceTypeID) * 2
                                            + (int) LGSPDir.In] += val;
#else
//                                        vstructs[superTypeID, nodeSuperType.TypeID, sourceTypeID, (int) LGSPDir.In] += val;
                                        vstructs[nodeSuperType.TypeID, superTypeID, sourceTypeID, (int) LGSPDir.In] += val;
#endif
                                    incomingVCount[superTypeID, sourceSuperType.typeID] = 0;
                                }
                            }
                        }
                    }
                }
            }

            nodeLookupCosts = new float[numNodeTypes];
            for(int i = 0; i < numNodeTypes; i++)
            {
                if(nodeCounts[i] <= 1)
                    nodeLookupCosts[i] = 0.00001F;                              // TODO: check this value (>0 because of preset elements)
                else
                    nodeLookupCosts[i] = (float) Math.Log(nodeCounts[i]);
            }

            // Calculate edgeCounts
            foreach(ITypeFramework edgeType in Model.EdgeModel.Types)
            {
                edgeCounts[edgeType.typeID] += edges[edgeType.typeID].Count;
                foreach(ITypeFramework superType in edgeType.SuperTypes)
                    edgeCounts[superType.typeID] += edges[superType.typeID].Count;
            }

            edgeLookupCosts = new float[numEdgeTypes];
            for(int i = 0; i < numEdgeTypes; i++)
            {
                if(edgeCounts[i] <= 1)
                    edgeLookupCosts[i] = 0.00001F;                              // TODO: check this value (>0 because of preset elements)
                else
                    edgeLookupCosts[i] = (float) Math.Log(edgeCounts[i]);
            }
        }
#elif OPCOST_WITH_GEO_MEAN
        public void AnalyzeGraph()
        {
            if(changesCounterAtLastAnalyze == changesCounter)
                return;
            changesCounterAtLastAnalyze = changesCounter;

            int numNodeTypes = Model.NodeModel.Types.Length;
            int numEdgeTypes = Model.EdgeModel.Types.Length;

            int[,] outgoingVCount = new int[numEdgeTypes, numNodeTypes];
            int[,] incomingVCount = new int[numEdgeTypes, numNodeTypes];

#if MONO_MULTIDIMARRAY_WORKAROUND
            dim0size = numNodeTypes;
            dim1size = numEdgeTypes;
            dim2size = numNodeTypes;
            vstructs = new float[numNodeTypes*numEdgeTypes*numNodeTypes*2];
#else
            vstructs = new float[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
#endif
            nodeCounts = new int[numNodeTypes];
            edgeCounts = new int[numEdgeTypes];
            nodeIncomingCount = new float[numNodeTypes];
            nodeOutgoingCount = new float[numNodeTypes];

            foreach(ITypeFramework nodeType in Model.NodeModel.Types)
            {
                foreach(ITypeFramework superType in nodeType.superOrSameTypes)
                    nodeCounts[superType.typeID] += nodesByTypeCounts[nodeType.typeID];

                for(LGSPNode nodeHead = nodesByTypeHeads[nodeType.typeID], node = nodeHead.typeNext; node != nodeHead; node = node.typeNext)
                {
                    //
                    // count outgoing v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            outgoingVCount[i, j] = 0;

                    LGSPEdge outhead = node.outhead;
                    if(outhead != null)
                    {
                        LGSPEdge edge = outhead;
                        do
                        {
                            ITypeFramework targetType = edge.target.type;
                            nodeOutgoingCount[nodeType.typeID]++;
                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int superTypeID = edgeSuperType.typeID;
                                foreach(ITypeFramework targetSuperType in targetType.superOrSameTypes)
                                {
                                    outgoingVCount[superTypeID, targetSuperType.typeID]++;
                                }
                            }
                            edge = edge.outNext;
                        }
                        while(edge != outhead);
                    }

                    //
                    // count incoming v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            incomingVCount[i, j] = 0;

                    LGSPEdge inhead = node.inhead;
                    if(inhead != null)
                    {
                        LGSPEdge edge = inhead;
                        do
                        {
                            ITypeFramework sourceType = edge.source.type;
                            nodeIncomingCount[nodeType.typeID]++;
                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int superTypeID = edgeSuperType.typeID;
                                foreach(ITypeFramework sourceSuperType in sourceType.superOrSameTypes)
                                {
                                    incomingVCount[superTypeID, sourceSuperType.typeID]++;
                                }
                            }
                            edge = edge.inNext;
                        }
                        while(edge != inhead);
                    }

                    //
                    // finalize the counting and collect resulting local v-struct info
                    //

                    if(outhead != null)
                    {
                        LGSPEdge edge = outhead;
                        do
                        {
                            ITypeFramework targetType = edge.target.type;
                            int targetTypeID = targetType.typeID;

                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int edgeSuperTypeID = edgeSuperType.typeID;

                                foreach(ITypeFramework targetSuperType in targetType.superOrSameTypes)
                                {
                                    int targetSuperTypeID = targetSuperType.typeID;
                                    if(outgoingVCount[edgeSuperTypeID, targetSuperTypeID] > 1)
                                    {
                                        float val = (float) Math.Log(outgoingVCount[edgeSuperTypeID, targetSuperTypeID]);
                                        foreach(ITypeFramework nodeSuperType in nodeType.superOrSameTypes)
                                        {
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.typeID * dim1size + edgeSuperTypeID) * dim2size + targetSuperTypeID) * 2
                                                + (int) LGSPDir.Out] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, targetSuperTypeID, (int) LGSPDir.Out] += val;
#endif
                                        }
                                        outgoingVCount[edgeSuperTypeID, targetSuperTypeID] = 0;
                                    }
                                }
                            }
                            edge = edge.outNext;
                        }
                        while(edge != outhead);
                    }

                    if(inhead != null)
                    {
                        LGSPEdge edge = inhead;
                        do
                        {
                            ITypeFramework sourceType = edge.source.type;
                            int sourceTypeID = sourceType.typeID;

                            foreach(ITypeFramework edgeSuperType in edge.type.superOrSameTypes)
                            {
                                int edgeSuperTypeID = edgeSuperType.typeID;
                                foreach(ITypeFramework sourceSuperType in sourceType.superOrSameTypes)
                                {
                                    int sourceSuperTypeID = sourceSuperType.typeID;
                                    if(incomingVCount[edgeSuperTypeID, sourceSuperTypeID] > 1)
                                    {
                                        float val = (float) Math.Log(incomingVCount[edgeSuperTypeID, sourceSuperTypeID]);

                                        foreach(ITypeFramework nodeSuperType in nodeType.superOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.typeID * dim1size + edgeSuperTypeID) * dim2size + sourceSuperTypeID) * 2
                                                + (int) LGSPDir.In] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, sourceSuperTypeID, (int) LGSPDir.In] += val;
#endif
                                        incomingVCount[edgeSuperTypeID, sourceSuperTypeID] = 0;
                                    }
                                }
                            }
                            edge = edge.inNext;
                        }
                        while(edge != inhead);
                    }
                }
            }

            nodeLookupCosts = new float[numNodeTypes];
            for(int i = 0; i < numNodeTypes; i++)
            {
                if(nodeCounts[i] <= 1)
                    nodeLookupCosts[i] = 0;
                else
                    nodeLookupCosts[i] = (float) Math.Log(nodeCounts[i]);
            }

            // Calculate edgeCounts
            foreach(ITypeFramework edgeType in Model.EdgeModel.Types)
            {
                foreach(ITypeFramework superType in edgeType.superOrSameTypes)
                    edgeCounts[superType.typeID] += edgesByTypeCounts[edgeType.typeID];
            }

            edgeLookupCosts = new float[numEdgeTypes];
            for(int i = 0; i < numEdgeTypes; i++)
            {
                if(edgeCounts[i] <= 1)
                    edgeLookupCosts[i] = 0;
                else
                    edgeLookupCosts[i] = (float) Math.Log(edgeCounts[i]);
            }
        }
#else
        /// <summary>
        /// Analyzes the graph.
        /// The calculated data is used to generate good searchplans for the current graph.
        /// </summary>
        public void AnalyzeGraph()
        {
            if(changesCounterAtLastAnalyze == changesCounter)
                return;
            changesCounterAtLastAnalyze = changesCounter;

            int numNodeTypes = Model.NodeModel.Types.Length;
            int numEdgeTypes = Model.EdgeModel.Types.Length;

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
            meanInDegree = new float[numNodeTypes];
            meanOutDegree = new float[numNodeTypes];

#if SCHNELLERER_ANSATZ_NUR_ANGEFANGEN
            foreach(ITypeFramework edgeType in Model.EdgeModel.Types)
            {
                /*                foreach(ITypeFramework superType in nodeType.superOrSameTypes)
                                    nodeCounts[superType.typeID] += nodesByTypeCounts[nodeType.typeID];*/

                for(LGSPEdge edgeHead = edgesByTypeHeads[edgeType.typeID], edge = edgeHead.typeNext; edge != edgeHead; edge = edge.typeNext)
                {
                    ITypeFramework sourceType = edge.source.type;
                    ITypeFramework targetType = edge.target.type;

#if MONO_MULTIDIMARRAY_WORKAROUND
                    vstructs[((sourceType.typeID * dim1size + edgeType.typeID) * dim2size + targetType.typeID) * 2 + (int) LGSPDir.Out] += val;
#else
                    vstructs[nodeSuperType.TypeID, edgeSuperTypeID, targetSuperTypeID, (int) LGSPDir.Out] += val;
#endif
                }
            }
#endif

            foreach(NodeType nodeType in Model.NodeModel.Types)
            {
                foreach(NodeType superType in nodeType.SuperOrSameTypes)
                    nodeCounts[superType.TypeID] += nodesByTypeCounts[nodeType.TypeID];

                for(LGSPNode nodeHead = nodesByTypeHeads[nodeType.TypeID], node = nodeHead.lgspTypeNext; node != nodeHead; node = node.lgspTypeNext)
                {
                    //
                    // count outgoing v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            outgoingVCount[i, j] = 0;

                    LGSPEdge outhead = node.lgspOuthead;
                    if(outhead != null)
                    {
                        LGSPEdge edge = outhead;
                        do
                        {
                            NodeType targetType = edge.lgspTarget.lgspType;
                            meanOutDegree[nodeType.TypeID]++;
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

                    //
                    // count incoming v structures
                    //

                    for(int i = 0; i < numEdgeTypes; i++)
                        for(int j = 0; j < numNodeTypes; j++)
                            incomingVCount[i, j] = 0;

                    LGSPEdge inhead = node.lgspInhead;
                    if(inhead != null)
                    {
                        LGSPEdge edge = inhead;
                        do
                        {
                            NodeType sourceType = edge.lgspSource.lgspType;
                            meanInDegree[nodeType.TypeID]++;
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

                    //
                    // finalize the counting and collect resulting local v-struct info
                    //

                    if(outhead != null)
                    {
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
//                                        int val = (float) Math.Log(outgoingVCount[edgeSuperTypeID, targetSuperTypeID]);     // > 1 im if
                                        int val = outgoingVCount[edgeSuperTypeID, targetSuperTypeID];
                                        foreach(NodeType nodeSuperType in nodeType.superOrSameTypes)
                                        {
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.TypeID * dim1size + edgeSuperTypeID) * dim2size + targetSuperTypeID) * 2
                                                + (int) LGSPDir.Out] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, targetSuperTypeID, (int) LGSPDir.Out] += val;
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

                    if(inhead != null)
                    {
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
//                                        int val = (float) Math.Log(incomingVCount[edgeSuperTypeID, sourceSuperTypeID]);     // > 1 im if
                                        int val = incomingVCount[edgeSuperTypeID, sourceSuperTypeID];
                                        foreach(NodeType nodeSuperType in nodeType.superOrSameTypes)
#if MONO_MULTIDIMARRAY_WORKAROUND
                                            vstructs[((nodeSuperType.TypeID * dim1size + edgeSuperTypeID) * dim2size + sourceSuperTypeID) * 2
                                                + (int) LGSPDir.In] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, sourceSuperTypeID, (int) LGSPDir.In] += val;
#endif
                                        incomingVCount[edgeSuperTypeID, sourceSuperTypeID] = 0;
                                    }
                                }
                            }
                            edge = edge.lgspInNext;
                        }
                        while(edge != inhead);
                    }
                }

                int numCompatibleNodes = nodeCounts[nodeType.TypeID];
                if(numCompatibleNodes != 0)
                {
                    meanOutDegree[nodeType.TypeID] /= numCompatibleNodes;
                    meanInDegree[nodeType.TypeID] /= numCompatibleNodes;
                }
            }


/*            // Calculate nodeCounts
            foreach(ITypeFramework nodeType in Model.NodeModel.Types)
            {
                foreach(ITypeFramework superType in edgeType.superOrSameTypes)
                    nodeCounts[superType.typeID] += nodesByTypeCounts[nodeType.typeID];
            }*/

            // Calculate edgeCounts
            foreach(EdgeType edgeType in Model.EdgeModel.Types)
            {
                foreach(EdgeType superType in edgeType.superOrSameTypes)
                    edgeCounts[superType.TypeID] += edgesByTypeCounts[edgeType.TypeID];
            }
        }
#endif
    }
}
