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
    /// Exports graphs to the GRS format.
    /// </summary>
    public class GRSExport : IDisposable
    {
        StreamWriter writer;

        protected GRSExport(String filename) 
            : this(new StreamWriter(filename))
        {
        }

        protected GRSExport(StreamWriter writer)
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
        /// Exports the given graph to a GRS file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="exportFilename">The filename for the exported file.</param>
        public static void Export(INamedGraph graph, String exportFilename)
        {
            using(GRSExport export = new GRSExport(exportFilename))
                export.Export(graph);
        }

        /// <summary>
        /// Exports the given graph to the file given by the stream writer in grs format.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="writer">The stream writer to export to.</param>
        public static void Export(INamedGraph graph, StreamWriter writer)
        {
            using(GRSExport export = new GRSExport(writer))
                export.Export(graph);
        }

        protected void Export(INamedGraph graph)
        {
            ExportYouMustCloseStreamWriter(graph, writer, "");
        }

        /// <summary>
        /// Exports the given graph to the file given by the stream writer in grs format.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="sw">The stream writer of the file to export into. The stream writer is not closed automatically.</param>
        public static void ExportYouMustCloseStreamWriter(INamedGraph graph, StreamWriter sw, string modelPathPrefix)
        {
            sw.WriteLine("# begin of graph \"{0}\" saved by GrsExport", graph.Name);
            sw.WriteLine();

            sw.WriteLine("new graph \"" + modelPathPrefix + graph.Model.ModelName + "\" \"" + graph.Name + "\"");

            bool thereIsNodeOrEdgeValuedSetOrMap = false;

            // emit nodes
            int numNodes = 0;
            foreach (INode node in graph.Nodes)
            {
                sw.Write("new :{0}($ = \"{1}\"", node.Type.Name, graph.GetElementName(node));
                foreach (AttributeType attrType in node.Type.AttributeTypes)
                {
                    if(IsNodeOrEdgeValuedSetOrMapOrArray(attrType)) {
                        thereIsNodeOrEdgeValuedSetOrMap = true;
                        continue;
                    }

                    object value = node.GetAttribute(attrType.Name);
                    // TODO: Add support for null values, as the default initializers could assign non-null values!
                    if(value != null) {
                        EmitAttributeInitialization(attrType, value, graph, sw);
                    }
                }
                sw.WriteLine(")");
                numNodes++;
            }
            sw.WriteLine("# total number of nodes: {0}", numNodes);
            sw.WriteLine();

            // emit edges
            int numEdges = 0;
            foreach (INode node in graph.Nodes)
            {
                foreach (IEdge edge in node.Outgoing)
                {
                    sw.Write("new @(\"{0}\") - :{1}($ = \"{2}\"", graph.GetElementName(node),
                        edge.Type.Name, graph.GetElementName(edge));
                    foreach (AttributeType attrType in edge.Type.AttributeTypes)
                    {
                        if(IsNodeOrEdgeValuedSetOrMapOrArray(attrType)) {
                            thereIsNodeOrEdgeValuedSetOrMap = true;
                            continue;
                        }

                        object value = edge.GetAttribute(attrType.Name);
                        // TODO: Add support for null values, as the default initializers could assign non-null values!
                        if(value != null) {
                            EmitAttributeInitialization(attrType, value, graph, sw);
                        }
                    }
                    sw.WriteLine(") -> @(\"{0}\")", graph.GetElementName(edge.Target));
                    numEdges++;
                }
            }
            sw.WriteLine("# total number of edges: {0}", numEdges);
            sw.WriteLine();

            // emit node/edge valued sets/maps/array
            if(thereIsNodeOrEdgeValuedSetOrMap)
            {
                foreach(INode node in graph.Nodes)
                {
                    foreach(AttributeType attrType in node.Type.AttributeTypes)
                    {
                        if(!IsNodeOrEdgeValuedSetOrMapOrArray(attrType))
                            continue;

                        object value = node.GetAttribute(attrType.Name);
                        sw.Write("{0}.{1} = ", graph.GetElementName(node), attrType.Name);
                        EmitAttribute(attrType, value, graph, sw);
                        sw.Write("\n");
                    }

                    foreach(IEdge edge in node.Outgoing)
                    {
                        foreach(AttributeType attrType in edge.Type.AttributeTypes)
                        {
                            if(!IsNodeOrEdgeValuedSetOrMapOrArray(attrType))
                                continue;

                            object value = edge.GetAttribute(attrType.Name);
                            sw.Write("{0}.{1} = ", graph.GetElementName(edge), attrType.Name);
                            EmitAttribute(attrType, value, graph, sw);
                            sw.Write("\n");
                        }
                    }
                }
            }

            sw.WriteLine("# end of graph \"{0}\" saved by GrsExport", graph.Name);
            sw.WriteLine();
        }

        /// <summary>
        /// Emits the node/edge attribute initialization code in graph exporting
        /// for an attribute of the given type with the given value into the stream writer
        /// </summary>
        private static void EmitAttributeInitialization(AttributeType attrType, object value, INamedGraph graph, StreamWriter sw)
        {
            sw.Write(", {0} = ", attrType.Name);
            EmitAttribute(attrType, value, graph, sw);
        }

        /// <summary>
        /// Emits the attribute value as code
        /// for an attribute of the given type with the given value into the stream writer
        /// </summary>
        public static void EmitAttribute(AttributeType attrType, object value, INamedGraph graph, StreamWriter sw)
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
            else if(attrType.Kind == AttributeKind.ArrayAttr)
            {
                IList array = (IList)value;
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
        public static String ToString(object value, AttributeType type, INamedGraph graph)
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
            case AttributeKind.GraphAttr:
                Console.WriteLine("Warning: Exporting non-null attribute of graph type to null");
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

        public static bool IsNodeOrEdgeValuedSetOrMapOrArray(AttributeType attrType)
        {
            if(attrType.Kind == AttributeKind.SetAttr
                || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.NodeAttr
                    || attrType.ValueType.Kind == AttributeKind.EdgeAttr)
                    return true;
            }
            if(attrType.Kind == AttributeKind.MapAttr)
            {
                if(attrType.KeyType.Kind == AttributeKind.NodeAttr
                    || attrType.KeyType.Kind == AttributeKind.EdgeAttr)
                    return true;
            }
            return false;
        }
    }
}
