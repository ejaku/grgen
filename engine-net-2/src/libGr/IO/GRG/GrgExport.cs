/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Exports graphs in GRG format, i.e. as GrGen rules.
    /// </summary>
    public class GRGExport : IDisposable
    {
        StreamWriter writer;

        protected GRGExport(String filename) 
            : this(new StreamWriter(filename))
        {
        }

        protected GRGExport(StreamWriter writer)
        {
            this.writer = writer;
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
        /// Exports the given graph to a GRG file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. If a NamedGraph is given, it will be exported including the names.</param>
        /// <param name="exportFilename">The filename for the exported file.</param>
        public static void Export(IGraph graph, String exportFilename)
        {
            using(GRGExport export = new GRGExport(exportFilename))
                export.Export(graph);
        }

        /// <summary>
        /// Exports the given graph to the file given by the stream writer in grg format, i.e. as GrGen rule.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. If a NamedGraph is given, it will be exported including the names.</param>
        /// <param name="writer">The stream writer to export to.</param>
        public static void Export(IGraph graph, StreamWriter writer)
        {
            using(GRGExport export = new GRGExport(writer))
                export.Export(graph);
        }

        protected void Export(IGraph graph)
        {
            ExportYouMustCloseStreamWriter(graph, writer, "");
        }

        /// <summary>
        /// Exports the given graph to the file given by the stream writer in grg format, i.e. as GrGen rule.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. If a NamedGraph is given, it will be exported including the names.</param>
        /// <param name="sw">The stream writer of the file to export into. The stream writer is not closed automatically.</param>
        public static void ExportYouMustCloseStreamWriter(IGraph graph, StreamWriter sw, string modelPathPrefix)
        {
            // we traverse the graph in one pass, directly writing a creating rule,
            // and writing a matching test to a helper stream which is appended at the end to the primary stream
            using(Stream stream = new MemoryStream())
            {
                using(StreamWriter sw2 = new StreamWriter(stream))
                {
                    sw.WriteLine("// graph \"{0}\" saved by GrgExport, as creating rule and matching test", graph.Name);
                    sw.WriteLine();

                    sw.WriteLine("using " + graph.Model.ModelName + ";");
                    sw.WriteLine();

                    if (!(graph is NamedGraph)) {
                        // assign arbitrary but unique names
                        graph = new NamedGraph(graph);
                    }

                    sw.Write("rule createGraph\n");
                    sw.Write("{\n");
                    sw.Write("\treplace {\n");

                    sw2.Write("test matchGraph\n");
                    sw2.Write("{\n");

                    // emit nodes
                    int numNodes = 0;
                    foreach (INode node in graph.Nodes)
                    {
                        String nodeName = EscapeName(graph.GetElementName(node));
                        sw.Write("\t\t{0}:{1};\n", nodeName, node.Type.Name);
                        sw2.Write("\t{0}:{1};\n", nodeName, node.Type.Name);
                        numNodes++;
                    }
                    sw.WriteLine("\t\t// total number of nodes: {0}", numNodes);
                    sw.WriteLine();
                    sw2.WriteLine("\t// total number of nodes: {0}", numNodes);
                    sw2.WriteLine();

                    // emit node attributes
                    sw.Write("\t\teval {\n");
                    sw.Write("\tif {\n");
                    foreach(INode node in graph.Nodes)
                    {
                        foreach(AttributeType attrType in node.Type.AttributeTypes)
                        {
                            object value = node.GetAttribute(attrType.Name);
                            // TODO: Add support for null values, as the default initializers could assign non-null values!
                            if(value != null) {
                                EmitAttributeInitialization(node, attrType, value, graph, "=", sw);
                                EmitAttributeInitialization(node, attrType, value, graph, "==", sw2);
                            }
                        }
                    }
                    sw.Write("\t\t}\n");
                    sw.WriteLine();
                    sw2.Write("\t}\n");
                    sw2.WriteLine();

                    // emit edges
                    int numEdges = 0;
                    foreach (INode node in graph.Nodes)
                    {
                        foreach (IEdge edge in node.Outgoing)
                        {
                            String sourceName = EscapeName(graph.GetElementName(edge.Source));
                            String edgeName = EscapeName(graph.GetElementName(edge));
                            String targetName = EscapeName(graph.GetElementName(edge.Target));
                            sw.Write("\t\t{0} -{1}:{2} -> {3};\n",
                                sourceName, edgeName, edge.Type.Name, targetName);
                            sw2.Write("\t{0} -{1}:{2} -> {3};\n",
                                sourceName, edgeName, edge.Type.Name, targetName);
                            numEdges++;
                        }
                    }
                    sw.WriteLine("\t\t// total number of edges: {0}", numEdges);
                    sw.WriteLine();
                    sw2.WriteLine("\t// total number of edges: {0}", numEdges);
                    sw2.WriteLine();

                    // emit edge attributes
                    sw.Write("\t\teval {\n");
                    sw2.Write("\teval {\n");
                    foreach(INode node in graph.Nodes)
                    {
                        foreach(IEdge edge in node.Outgoing)
                        {
                            foreach(AttributeType attrType in edge.Type.AttributeTypes)
                            {
                                object value = edge.GetAttribute(attrType.Name);
                                // TODO: Add support for null values, as the default initializers could assign non-null values!
                                if(value != null) {
                                    EmitAttributeInitialization(edge, attrType, value, graph, "=", sw);
                                    EmitAttributeInitialization(edge, attrType, value, graph, "==", sw2);
                                }
                            }
                        }
                    }
                    sw.Write("\t\t}\n");
                    sw2.Write("\t}\n");

                    sw.Write("\t}\n");
                    sw.Write("}\n");
                    sw2.Write("}\n");

                    sw.WriteLine();
                    sw2.WriteLine();

                    // I'm sure now that M$ gave the task of designing the .NET library
                    // to the most incompetent of the incompetent it could find on its campus
                    // while C# is better than Java, the class library is just annoying
                    sw2.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    sw.WriteLine(str); // sw already contains creating rule, now write matching test
                }
            }
        }

        /// <summary>
        /// Emits the node/edge attribute initialization code in graph exporting
        /// for an attribute of the given type with the given value into the stream writer
        /// </summary>
        private static void EmitAttributeInitialization(IGraphElement elem, AttributeType attrType, object value, IGraph graph, String op, StreamWriter sw)
        {
            sw.Write("\t\t\t{0}.{1} {2} ", EscapeName(graph.GetElementName(elem)), attrType.Name, op);
            EmitAttribute(attrType, value, graph, sw);
            sw.WriteLine(";");
        }

        /// <summary>
        /// Emits the attribute value as code
        /// for an attribute of the given type with the given value into the stream writer
        /// </summary>
        public static void EmitAttribute(AttributeType attrType, object value, IGraph graph, StreamWriter sw)
        {
            if(attrType.Kind==AttributeKind.SetAttr)
            {
                IDictionary set=(IDictionary)value;
                sw.Write("{0}{{", attrType.GetKindName());
                bool first = true;
                foreach(DictionaryEntry entry in set)
                {
                    if(first) { sw.Write(ToString(entry.Key, attrType.ValueType, graph)); first = false; }
                    else { sw.Write("," + ToString(entry.Key, attrType.ValueType, graph)); }
                }
                sw.Write("}");
            }
            else if(attrType.Kind==AttributeKind.MapAttr)
            {
                IDictionary map=(IDictionary)value;
                sw.Write("{0}{{", attrType.GetKindName());
                bool first = true;
                foreach(DictionaryEntry entry in map)
                {
                    if(first) { sw.Write(ToString(entry.Key, attrType.KeyType, graph)
                        + "->" + ToString(entry.Value, attrType.ValueType, graph)); first = false;
                    }
                    else { sw.Write("," + ToString(entry.Key, attrType.KeyType, graph)
                        + "->" + ToString(entry.Value, attrType.ValueType, graph)); }
                }
                sw.Write("}");
            }
            else if(attrType.Kind==AttributeKind.ArrayAttr)
            {
                IList array=(IList)value;
                sw.Write("{0}[", attrType.GetKindName());
                bool first = true;
                foreach(object entry in array)
                {
                    if(first) { sw.Write(ToString(entry, attrType.ValueType, graph)); first = false; }
                    else { sw.Write("," + ToString(entry, attrType.ValueType, graph)); }
                }
                sw.Write("]");
            }
            else
            {
                sw.Write("{0}", ToString(value, attrType, graph));
            }
        }

        /// <summary>
        /// type needed for enum, otherwise null ok
        /// graph needed for node/edge in set/map, otherwise null ok
        /// </summary>
        public static String ToString(object value, AttributeType type, IGraph graph)
        {
            switch(type.Kind)
            {
            case AttributeKind.ByteAttr:
                return ((sbyte)value).ToString()+"Y";
            case AttributeKind.ShortAttr:
                return ((short)value).ToString()+"S";
            case AttributeKind.IntegerAttr:
                return ((int)value).ToString();
            case AttributeKind.LongAttr:
                return ((long)value).ToString()+"L";
            case AttributeKind.BooleanAttr:
                return ((bool)value).ToString();
            case AttributeKind.StringAttr:
                if(value == null) return "\"\"";
                if(((string)value).IndexOf('\"') != -1) return "\'" + ((string)value) + "\'";
                else return "\"" + ((string)value) + "\"";
            case AttributeKind.FloatAttr:
                return ((float)value).ToString(System.Globalization.CultureInfo.InvariantCulture)+"f";
            case AttributeKind.DoubleAttr:
                return ((double)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            case AttributeKind.ObjectAttr:
                Console.WriteLine("Warning: Exporting non-null attribute of object type to null");
                return "null";
            case AttributeKind.EnumAttr:
                return type.EnumType.Name + "::" + type.EnumType[(int)value].Name;
            case AttributeKind.NodeAttr:
            case AttributeKind.EdgeAttr:
                return graph.GetElementName((IGraphElement)value);
            default:
                throw new Exception("Unsupported attribute kind in export");
            }
        }

        private static String EscapeName(String unescapedName)
        {
            return unescapedName.Replace('$', '_');
        }
    }
}
