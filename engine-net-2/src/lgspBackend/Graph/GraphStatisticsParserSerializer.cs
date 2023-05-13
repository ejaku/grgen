/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

#define MONO_MULTIDIMARRAY_WORKAROUND       // not using multidimensional arrays is about 2% faster on .NET because of fewer bound checks
// if you uncomment this, take care to also uncomment the same define in LGSPGraphStatistics.cs

using System;
using System.IO;
using System.Text;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A class for serializing and parsing the statistics from a graph (cf. LGSPGraphStatistics)
    /// </summary>
    public class GraphStatisticsParserSerializer
    {
        // used during parsing of statistics from file, for error output in case of parsing failure
        private int line;

        private LGSPGraphStatistics statistics;


        /// <summary>
        /// Create the statistics parser and serializer, binding it to the statistics object to fill or write out.
        /// </summary>
        public GraphStatisticsParserSerializer(LGSPGraphStatistics statistics)
        {
            this.statistics = statistics;
        }

        /// <summary>
        /// Reads the statistics from the specified file path (inverse of Serialize), stores them in the statistics object given in the constructor.
        /// </summary>
        public void Parse(string path)
        {
            int numNodeTypes = statistics.graphModel.NodeModel.Types.Length;
            int numEdgeTypes = statistics.graphModel.EdgeModel.Types.Length;

#if MONO_MULTIDIMARRAY_WORKAROUND
            statistics.dim0size = numNodeTypes;
            statistics.dim1size = numEdgeTypes;
            statistics.dim2size = numNodeTypes;
            statistics.vstructs = new int[numNodeTypes * numEdgeTypes * numNodeTypes * 2];
#else
            statistics.vstructs = new int[numNodeTypes, numEdgeTypes, numNodeTypes, 2];
#endif
            statistics.nodeCounts = new int[numNodeTypes];
            statistics.edgeCounts = new int[numEdgeTypes];
            statistics.outCounts = new int[numNodeTypes];
            statistics.inCounts = new int[numNodeTypes];
            statistics.meanInDegree = new float[numNodeTypes];
            statistics.meanOutDegree = new float[numNodeTypes];

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
                statistics.nodeCounts[GetNodeTypeIndex(type)] = Int32.Parse(number);
            }
            else if(countType == CountType.Edge)
            {
                statistics.edgeCounts[GetEdgeTypeIndex(type)] = Int32.Parse(number);
            }
            else if(countType == CountType.Out)
            {
                statistics.outCounts[GetNodeTypeIndex(type)] = Int32.Parse(number);
                statistics.meanOutDegree[GetNodeTypeIndex(type)] = statistics.outCounts[GetNodeTypeIndex(type)] / Math.Max(statistics.nodeCounts[GetNodeTypeIndex(type)], 1);
            }
            else //if(countType == CountType.In)
            {
                statistics.inCounts[GetNodeTypeIndex(type)] = Int32.Parse(number);
                statistics.meanInDegree[GetNodeTypeIndex(type)] = statistics.inCounts[GetNodeTypeIndex(type)] / Math.Max(statistics.nodeCounts[GetNodeTypeIndex(type)], 1);
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

            statistics.vstructs[((GetNodeTypeIndex(nodeType) * statistics.dim1size + GetEdgeTypeIndex(edgeType)) * statistics.dim2size + GetNodeTypeIndex(oppositeNodeType)) * 2 + (int)direction]
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
            for(int i=0; i< statistics.graphModel.NodeModel.Types.Length; ++i)
            {
                if(statistics.graphModel.NodeModel.Types[i].Name == type)
                    return statistics.graphModel.NodeModel.Types[i].TypeID;
            }

            throw new Exception("Unknown node type " + type);
        }

        int GetEdgeTypeIndex(string type)
        {
            for(int i = 0; i < statistics.graphModel.EdgeModel.Types.Length; ++i)
            {
                if(statistics.graphModel.EdgeModel.Types[i].Name == type)
                    return statistics.graphModel.EdgeModel.Types[i].TypeID;
            }

            throw new Exception("Unknown edge type " + type);
        }


        /// <summary>
        /// Writes the statistics object (given in the constructor) to the specified file path (inverse of Parse).
        /// </summary>
        public void Serialize(string path)
        {
            using(StreamWriter sw = new StreamWriter(path))
            {
                // emit node counts
                for(int i = 0; i < statistics.graphModel.NodeModel.Types.Length; ++i)
                    sw.WriteLine("count node " + statistics.graphModel.NodeModel.Types[i] + " = " + statistics.nodeCounts[i].ToString());

                // emit edge counts
                for(int i = 0; i < statistics.graphModel.EdgeModel.Types.Length; ++i)
                    sw.WriteLine("count edge " + statistics.graphModel.EdgeModel.Types[i] + " = " + statistics.edgeCounts[i].ToString());

                // emit out counts
                for(int i = 0; i < statistics.graphModel.NodeModel.Types.Length; ++i)
                    sw.WriteLine("count out " + statistics.graphModel.NodeModel.Types[i] + " = " + statistics.outCounts[i].ToString());

                // emit in counts
                for(int i = 0; i < statistics.graphModel.NodeModel.Types.Length; ++i)
                    sw.WriteLine("count in " + statistics.graphModel.NodeModel.Types[i] + " = " + statistics.inCounts[i].ToString());

                // emit vstructs
                for(int i = 0; i < statistics.graphModel.NodeModel.Types.Length; ++i)
                {
                    for(int j = 0; j < statistics.graphModel.EdgeModel.Types.Length; ++j)
                    {
                        for(int k = 0; k < statistics.graphModel.NodeModel.Types.Length; ++k)
                        {
                            for(int l = 0; l <= 1; ++l)
                            {
                                if(l == 0)
                                    sw.WriteLine("vstruct " + statistics.graphModel.NodeModel.Types[i] + " <- " + statistics.graphModel.EdgeModel.Types[j] + " - " + statistics.graphModel.NodeModel.Types[k] + " = "
                                        + statistics.vstructs[((i * statistics.dim1size + j) * statistics.dim2size + k) * 2 + 0].ToString());
                                else
                                    sw.WriteLine("vstruct " + statistics.graphModel.NodeModel.Types[i] + " - " + statistics.graphModel.EdgeModel.Types[j] + " -> " + statistics.graphModel.NodeModel.Types[k] + " = "
                                        + statistics.vstructs[((i * statistics.dim1size + j) * statistics.dim2size + k) * 2 + 1].ToString());
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
