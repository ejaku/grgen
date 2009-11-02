/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Exports graphs to the GRS format.
    /// </summary>
    public class GRSExport : IDisposable
    {
        StreamWriter writer;

        protected GRSExport(String filename)
        {
            writer = new StreamWriter(filename);
        }

        public void Dispose()
        {
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }

        /// <summary>
        /// Exports the given graph to a GRS file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export.</param>
        /// <param name="exportFilename">The filename for the exported file.</param>
        public static void Export(IGraph graph, String exportFilename)
        {
            using(GRSExport export = new GRSExport(exportFilename))
                export.Export(graph);
        }

        protected void Export(IGraph graph)
        {
            Export(graph, writer);
        }

        /// <summary>
        /// Exports the given graph in grs format to the file given by the stream writer.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export.</param>
        /// <param name="sw">The stream writer of the file to export into.</param>
        public static void Export(IGraph graph, StreamWriter sw)
        {
            sw.WriteLine("# begin of graph \"{0}\" saved by GrsExport", graph.Name);
            sw.WriteLine();

            sw.WriteLine("new graph " + StringToTextToken(graph.Model.ModelName) + " " + StringToTextToken(graph.Name));

            if (!(graph is NamedGraph)) {
                // assign arbitrary but unique names, 
                // so that we get a valid dump without a complete coverage of the graph elements by variables
                graph = new NamedGraph(graph);
            }

            int numNodes = 0;
            foreach (INode node in graph.Nodes)
            {
                sw.Write("new :{0}($ = {1}", node.Type.Name, StringToTextToken(graph.GetElementName(node)));
                foreach (AttributeType attrType in node.Type.AttributeTypes)
                {
                    object value = node.GetAttribute(attrType.Name);
                    // TODO: Add support for null values, as the default initializers could assign non-null values!
                    if (value != null)
                        sw.Write(", {0} = {1}", attrType.Name, StringToTextToken(value.ToString()));
                }
                sw.WriteLine(")");
                LinkedList<Variable> vars = graph.GetElementVariables(node);
                if (vars != null)
                {
                    foreach (Variable var in vars)
                    {
                        sw.WriteLine("{0} = @({1})", var.Name, StringToTextToken(graph.GetElementName(node)));
                    }
                }
                numNodes++;
            }
            sw.WriteLine("# total number of nodes: {0}", numNodes);
            sw.WriteLine();

            int numEdges = 0;
            foreach (INode node in graph.Nodes)
            {
                foreach (IEdge edge in node.Outgoing)
                {
                    sw.Write("new @({0}) - :{1}($ = {2}", StringToTextToken(graph.GetElementName(node)),
                        edge.Type.Name, StringToTextToken(graph.GetElementName(edge)));
                    foreach (AttributeType attrType in edge.Type.AttributeTypes)
                    {
                        object value = edge.GetAttribute(attrType.Name);
                        // TODO: Add support for null values, as the default initializers could assign non-null values!
                        if (value != null)
                            sw.Write(", {0} = {1}", attrType.Name, StringToTextToken(value.ToString()));
                    }
                    sw.WriteLine(") -> @({0})", StringToTextToken(graph.GetElementName(edge.Target)));
                    LinkedList<Variable> vars = graph.GetElementVariables(edge);
                    if (vars != null)
                    {
                        foreach (Variable var in vars)
                        {
                            sw.WriteLine("{0} = @({1})", var.Name, StringToTextToken(graph.GetElementName(edge)));
                        }
                    }
                    numEdges++;
                }
            }
            sw.WriteLine("# total number of edges: {0}", numEdges);
            sw.WriteLine();

            sw.WriteLine("# end of graph \"{0}\" saved by GrsExport", graph.Name);
            sw.WriteLine();
        }

        private static String StringToTextToken(String str)
        {
            if (str.IndexOf('\"') != -1) return "\'" + str + "\'";
            else return "\"" + str + "\"";
        }
    }
}
