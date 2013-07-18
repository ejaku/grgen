/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
// todo: add import/export for 4-dim array instead of one-dim with manual index computations

using System;
using de.unika.ipd.grGen.libGr;
using System.IO;
using System.Text;

namespace de.unika.ipd.grGen.lgsp
{
    public enum LGSPDirection { In, Out };

    /// <summary>
    /// A class for analyzing a graph and storing the statistics about the graph
    /// </summary>
    public class LGSPGraphStatistics
    {
        public IGraphModel graphModel;

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
        /// <param name="newName">Name of the copied graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning,
        /// just forget about it if you don't need it.</param>
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
                    nodeCounts[superType.TypeID] += graph.nodesByTypeCounts[nodeType.TypeID];

                for(LGSPNode nodeHead = graph.nodesByTypeHeads[nodeType.TypeID], node = nodeHead.lgspTypeNext; node != nodeHead; node = node.lgspTypeNext)
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
                                                + (int) LGSPDirection.Out] += val;
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
                                                + (int) LGSPDirection.In] += val;
#else
                                            vstructs[nodeSuperType.TypeID, edgeSuperTypeID, sourceSuperTypeID, (int) LGSPDirection.In] += val;
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
                    edgeCounts[superType.TypeID] += graph.edgesByTypeCounts[edgeType.TypeID];
            }
        }


        // used during parsing of statistics from file, for error output in case of parsing failure
        private int line;

        /// <summary>
        /// Reads the statistics from the specified file path (inverse of Serialize).
        /// </summary>
        public void Parse(string path)
        {
            int numNodeTypes = graphModel.NodeModel.Types.Length;
            int numEdgeTypes = graphModel.EdgeModel.Types.Length;

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

            StreamReader sr = new StreamReader(FixDirectorySeparators(path));
            line = 1;

            while((char)sr.Peek() == 'c')
            {
                ParseCount(sr);
            }
            while(sr.Peek() != -1 && (char)sr.Peek() == 'v')
            {
                ParseVStruct(sr);
            }
        }

        enum CountType { Node, Edge, Out, In };

        void ParseCount(StreamReader sr)
        {
            Eat(sr, 'c');
            Eat(sr, 'o');
            Eat(sr, 'u');
            Eat(sr, 'n');
            Eat(sr, 't');
            Eat(sr, ' ');

            CountType countType;
            if((char)sr.Peek() == 'n')
            {
                Eat(sr, 'n');
                Eat(sr, 'o');
                Eat(sr, 'd');
                Eat(sr, 'e');
                countType = CountType.Node;
            }
            else if((char)sr.Peek() == 'e')
            {
                Eat(sr, 'e');
                Eat(sr, 'd');
                Eat(sr, 'g');
                Eat(sr, 'e');
                countType = CountType.Edge;
            }
            else if((char)sr.Peek() == 'o')
            {
                Eat(sr, 'o');
                Eat(sr, 'u');
                Eat(sr, 't');
                countType = CountType.Out;
            }
            else
            {
                Eat(sr, 'i');
                Eat(sr, 'n');
                countType = CountType.In;
            }
            Eat(sr, ' ');
            string type = EatAlphaNumeric(sr);
            Eat(sr, ' ');
            Eat(sr, '=');
            Eat(sr, ' ');
            string number = EatNumber(sr);
            EatNewline(sr);

            if(countType == CountType.Node)
            {
                nodeCounts[GetNodeTypeIndex(type)] = Int32.Parse(number);
            }
            else if(countType == CountType.Edge)
            {
                edgeCounts[GetEdgeTypeIndex(type)] = Int32.Parse(number);
            }
            else if(countType == CountType.Out)
            {
                outCounts[GetNodeTypeIndex(type)] = Int32.Parse(number);
                meanOutDegree[GetNodeTypeIndex(type)] = outCounts[GetNodeTypeIndex(type)] / nodeCounts[GetNodeTypeIndex(type)];
            }
            else //if(countType == CountType.In)
            {
                inCounts[GetNodeTypeIndex(type)] = Int32.Parse(number);
                meanInDegree[GetNodeTypeIndex(type)] = inCounts[GetNodeTypeIndex(type)] / nodeCounts[GetNodeTypeIndex(type)];
            }
        }

        void ParseVStruct(StreamReader sr)
        {
            Eat(sr, 'v');
            Eat(sr, 's');
            Eat(sr, 't');
            Eat(sr, 'r');
            Eat(sr, 'u');
            Eat(sr, 'c');
            Eat(sr, 't');
            Eat(sr, ' ');
            string nodeType = EatAlphaNumeric(sr);
            Eat(sr, ' ');

            LGSPDirection direction;
            string edgeType;
            if(sr.Peek() == '-')
            {
                direction = LGSPDirection.Out;
                Eat(sr, '-');
                Eat(sr, ' ');
                edgeType = EatAlphaNumeric(sr);
                Eat(sr, ' ');
                Eat(sr, '-');
                Eat(sr, '>');
            }
            else
            {
                direction = LGSPDirection.In;
                Eat(sr, '<');
                Eat(sr, '-');
                Eat(sr, ' ');
                edgeType = EatAlphaNumeric(sr);
                Eat(sr, ' ');
                Eat(sr, '-');
            }
            Eat(sr, ' ');

            string oppositeNodeType = EatAlphaNumeric(sr);
            Eat(sr, ' ');
            Eat(sr, '=');
            Eat(sr, ' ');
            string number = EatNumber(sr);
            EatNewline(sr);

            vstructs[((GetNodeTypeIndex(nodeType) * dim1size + GetEdgeTypeIndex(edgeType)) * dim2size + GetNodeTypeIndex(oppositeNodeType)) * 2 + (int)direction]
                = Int32.Parse(number);
        }

        void Eat(StreamReader sr, char expected)
        {
            if(sr.Peek() != expected)
                throw new Exception("parsing error, expected " + expected + ", but found \"" + sr.ReadLine() + "\" at line " + line);
            sr.Read();
        }

        string EatAlphaNumeric(StreamReader sr)
        {
            StringBuilder sb = new StringBuilder();
            if(!char.IsLetter((char)sr.Peek()))
                throw new Exception("parsing error, expected letter, but found \"" + sr.ReadLine() + "\" at line " + line);
            sb.Append((char)sr.Read());
            while(char.IsLetterOrDigit((char)sr.Peek())) // TODO: is the underscore included?
            {
                sb.Append((char)sr.Read());
            }
            return sb.ToString();
        }

        string EatNumber(StreamReader sr)
        {
            StringBuilder sb = new StringBuilder();
            if(!char.IsNumber((char)sr.Peek()))
                throw new Exception("parsing error, expected number, but found \"" + sr.ReadLine() + "\" at line " + line);
            sb.Append((char)sr.Read());
            while(char.IsNumber((char)sr.Peek()))
            {
                sb.Append((char)sr.Read());
            }
            return sb.ToString();
        }

        void EatNewline(StreamReader sr)
        {
            if(sr.Peek() == '\r')
            {
                sr.Read();
                ++line;
                if(sr.Peek() == '\n')
                    sr.Read();
            }
            else if(sr.Peek() == '\n')
            {
                sr.Read();
                ++line;
            }
            else
                throw new Exception("parsing error, expected newline, but found \"" + sr.ReadLine() + "\" at line " + line);
        }

        int GetNodeTypeIndex(string type)
        {
            for(int i=0; i<graphModel.NodeModel.Types.Length; ++i)
            {
                if(graphModel.NodeModel.Types[i].Name == type)
                    return graphModel.NodeModel.Types[i].TypeID;
            }

            throw new Exception("Unknown node type " + type);
        }

        int GetEdgeTypeIndex(string type)
        {
            for(int i = 0; i < graphModel.EdgeModel.Types.Length; ++i)
            {
                if(graphModel.EdgeModel.Types[i].Name == type)
                    return graphModel.EdgeModel.Types[i].TypeID;
            }

            throw new Exception("Unknown edge type " + type);
        }


        /// <summary>
        /// Writes the statistics to the specified file path (inverse of Parse).
        /// </summary>
        public void Serialize(string path)
        {
            using(StreamWriter sw = new StreamWriter(path))
            {
                // emit node counts
                for(int i = 0; i < graphModel.NodeModel.Types.Length; ++i)
                    sw.WriteLine("count node " + graphModel.NodeModel.Types[i] + " = " + nodeCounts[i].ToString());

                // emit edge counts
                for(int i = 0; i < graphModel.EdgeModel.Types.Length; ++i)
                    sw.WriteLine("count edge " + graphModel.EdgeModel.Types[i] + " = " + edgeCounts[i].ToString());

                // emit out counts
                for(int i = 0; i < graphModel.NodeModel.Types.Length; ++i)
                    sw.WriteLine("count out " + graphModel.NodeModel.Types[i] + " = " + outCounts[i].ToString());

                // emit in counts
                for(int i = 0; i < graphModel.NodeModel.Types.Length; ++i)
                    sw.WriteLine("count in " + graphModel.NodeModel.Types[i] + " = " + inCounts[i].ToString());

                // emit vstructs
                for(int i = 0; i < graphModel.NodeModel.Types.Length; ++i)
                {
                    for(int j = 0; j < graphModel.EdgeModel.Types.Length; ++j)
                    {
                        for(int k = 0; k < graphModel.NodeModel.Types.Length; ++k)
                        {
                            for(int l = 0; l <= 1; ++l)
                            {
                                if(l == 0)
                                    sw.WriteLine("vstruct " + graphModel.NodeModel.Types[i] + " <- " + graphModel.EdgeModel.Types[j] + " - " + graphModel.NodeModel.Types[k] + " = "
                                        + vstructs[((i * dim1size + j) * dim2size + k) * 2 + 0].ToString());
                                else
                                    sw.WriteLine("vstruct " + graphModel.NodeModel.Types[i] + " - " + graphModel.EdgeModel.Types[j] + " -> " + graphModel.NodeModel.Types[k] + " = "
                                        + vstructs[((i * dim1size + j) * dim2size + k) * 2 + 1].ToString());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns a string where all "wrong" directory separator chars are replaced by the ones used by the system
        /// </summary>
        /// <param name="path">The original path string potentially with wrong chars</param>
        /// <returns>The corrected path string</returns>
        static String FixDirectorySeparators(String path)
        {
            if(Path.DirectorySeparatorChar != '\\')
                path = path.Replace('\\', Path.DirectorySeparatorChar);
            if(Path.DirectorySeparatorChar != '/')
                path = path.Replace('/', Path.DirectorySeparatorChar);
            return path;
        }
    }
}
