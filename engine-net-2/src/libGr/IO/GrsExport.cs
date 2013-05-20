/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

        public class GraphExportContext
        {
            // the name used for export, may be different from the real graph name
            // here we ensure the uniqueness needed for an export / for getting an importable serialization
            public static Dictionary<INamedGraph, GraphExportContext> graphToContext = null;
            public static Dictionary<string, GraphExportContext> nameToContext = null;

            public GraphExportContext(INamedGraph graph)
            {
                this.graph = graph;
                if(nameToContext.ContainsKey(graph.Name))
                {
                    int id = 0;
                    while(nameToContext.ContainsKey(graph.Name + "_" + id.ToString()))
                    {
                        ++id;
                    }
                    this.name = graph.Name + "_" + id.ToString();
                }
                else
                    this.name = graph.Name;
            }

            public INamedGraph graph;
            public string name;

            public string modelPathPrefix = null; // not null iff this is the main graph
            public bool nodeOrEdgeUsedInAttribute = false;
            public bool subgraphUsedInAttribute = false;

            public bool isExported = false;
        }

        /// <summary>
        /// Exports the given graph to the file given by the stream writer in grs format.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="sw">The stream writer of the file to export into. The stream writer is not closed automatically.</param>
        /// <param name="modelPathPrefix">Path to the model.</param>
        public static void ExportYouMustCloseStreamWriter(INamedGraph graph, StreamWriter sw, string modelPathPrefix)
        {
            GraphExportContext.graphToContext = new Dictionary<INamedGraph, GraphExportContext>();
            GraphExportContext.nameToContext = new Dictionary<string, GraphExportContext>();

            GraphExportContext mainGraphContext = new GraphExportContext(graph);
            mainGraphContext.modelPathPrefix = modelPathPrefix;
            GraphExportContext.graphToContext[mainGraphContext.graph] = mainGraphContext;
            GraphExportContext.nameToContext[mainGraphContext.name] = mainGraphContext;

            sw.WriteLine("# begin of graph \"{0}\" saved by GrsExport", mainGraphContext.name);
            sw.WriteLine();

            bool subgraphAdded = ExportSingleGraph(mainGraphContext, sw);

            if(subgraphAdded)
            {
restart:
                foreach(KeyValuePair<string, GraphExportContext> kvp in GraphExportContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    if(!context.isExported)
                    {
                        subgraphAdded = ExportSingleGraph(context, sw);
                        if(subgraphAdded)
                            goto restart;
                    }
                }

                foreach(KeyValuePair<string, GraphExportContext> kvp in GraphExportContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    EmitSubgraphAttributes(context, sw);
                }

                sw.WriteLine("in \"" + mainGraphContext.name + "\""); // after import in main graph
            }

            sw.WriteLine("# end of graph \"{0}\" saved by GrsExport", mainGraphContext.name);
            sw.WriteLine();

            GraphExportContext.graphToContext = null;
            GraphExportContext.nameToContext = null;
        }

        private static bool ExportSingleGraph(GraphExportContext context, StreamWriter sw)
        {
            bool subgraphAdded = false;

            if(context.modelPathPrefix != null)
                sw.WriteLine("new graph \"" + context.modelPathPrefix + context.graph.Model.ModelName + "\" \"" + context.name + "\"");
            else
                sw.WriteLine("add new graph \"" + context.name + "\"");

            // emit nodes
            int numNodes = 0;
            foreach(INode node in context.graph.Nodes)
            {
                sw.Write("new :{0}($ = \"{1}\"", node.Type.Name, context.graph.GetElementName(node));
                foreach(AttributeType attrType in node.Type.AttributeTypes)
                {
                    if(IsNodeOrEdgeUsedInAttribute(attrType))
                    {
                        context.nodeOrEdgeUsedInAttribute = true;
                        continue;
                    }
                    if(IsGraphUsedInAttribute(attrType))
                    {
                        context.subgraphUsedInAttribute = true;
                        subgraphAdded |= AddSubgraphAsNeeded(node, attrType);
                        continue;
                    }

                    object value = node.GetAttribute(attrType.Name);
                    EmitAttributeInitialization(attrType, value, context.graph, sw);
                }
                sw.WriteLine(")");
                numNodes++;
            }
            if(context.modelPathPrefix != null)
                sw.WriteLine("# total number of nodes: {0}", numNodes);
            else
                sw.WriteLine("# total number of nodes in subgraph {0}: {1}", context.name, numNodes);
            sw.WriteLine();

            // emit edges
            int numEdges = 0;
            foreach(INode node in context.graph.Nodes)
            {
                foreach(IEdge edge in node.Outgoing)
                {
                    sw.Write("new @(\"{0}\") - :{1}($ = \"{2}\"", context.graph.GetElementName(node),
                        edge.Type.Name, context.graph.GetElementName(edge));
                    foreach(AttributeType attrType in edge.Type.AttributeTypes)
                    {
                        if(IsNodeOrEdgeUsedInAttribute(attrType))
                        {
                            context.nodeOrEdgeUsedInAttribute = true;
                            continue;
                        }
                        if(IsGraphUsedInAttribute(attrType))
                        {
                            context.subgraphUsedInAttribute = true;
                            subgraphAdded |= AddSubgraphAsNeeded(edge, attrType);
                            continue;
                        }

                        object value = edge.GetAttribute(attrType.Name);
                        // TODO: Add support for null values, as the default initializers could assign non-null values!
                        if(value != null)
                        {
                            EmitAttributeInitialization(attrType, value, context.graph, sw);
                        }
                    }
                    sw.WriteLine(") -> @(\"{0}\")", context.graph.GetElementName(edge.Target));
                    numEdges++;
                }
            }
            if(context.modelPathPrefix != null)
                sw.WriteLine("# total number of edges: {0}", numEdges);
            else
                sw.WriteLine("# total number of edges in subgraph {0}: {1}", context.name, numEdges);
            sw.WriteLine();

            // emit node/edge valued attributes
            if(context.nodeOrEdgeUsedInAttribute)
            {
                foreach(INode node in context.graph.Nodes)
                {
                    foreach(AttributeType attrType in node.Type.AttributeTypes)
                    {
                        if(!IsNodeOrEdgeUsedInAttribute(attrType))
                            continue;
                        if(IsGraphUsedInAttribute(attrType))
                            continue;

                        object value = node.GetAttribute(attrType.Name);
                        sw.Write("{0}.{1} = ", context.graph.GetElementName(node), attrType.Name);
                        EmitAttribute(attrType, value, context.graph, sw);
                        sw.Write("\n");
                    }

                    foreach(IEdge edge in node.Outgoing)
                    {
                        foreach(AttributeType attrType in edge.Type.AttributeTypes)
                        {
                            if(!IsNodeOrEdgeUsedInAttribute(attrType))
                                continue;
                            if(IsGraphUsedInAttribute(attrType))
                                continue;

                            object value = edge.GetAttribute(attrType.Name);
                            sw.Write("{0}.{1} = ", context.graph.GetElementName(edge), attrType.Name);
                            EmitAttribute(attrType, value, context.graph, sw);
                            sw.Write("\n");
                        }
                    }
                }
            }

            context.isExported = true;
            return subgraphAdded;
        }

        private static bool AddSubgraphAsNeeded(IGraphElement elem, AttributeType attrType)
        {
            INamedGraph graph = (INamedGraph)elem.GetAttribute(attrType.Name);
            if(!GraphExportContext.graphToContext.ContainsKey(graph))
            {
                GraphExportContext subgraphContext = new GraphExportContext(graph);
                GraphExportContext.graphToContext[graph] = subgraphContext;
                GraphExportContext.nameToContext[subgraphContext.name] = subgraphContext;
                return true;
            }
            else
                return false;
        }

        private static void EmitSubgraphAttributes(GraphExportContext context, StreamWriter sw)
        {
            sw.WriteLine("in \"" + context.name + "\"");

            foreach(INode node in context.graph.Nodes)
            {
                foreach(AttributeType attrType in node.Type.AttributeTypes)
                {
                    if(!IsGraphUsedInAttribute(attrType))
                        continue;

                    object value = node.GetAttribute(attrType.Name);
                    sw.Write("{0}.{1} = ", context.graph.GetElementName(node), attrType.Name);
                    EmitAttribute(attrType, value, context.graph, sw);
                    sw.Write("\n");
                }

                foreach(IEdge edge in node.Outgoing)
                {
                    foreach(AttributeType attrType in edge.Type.AttributeTypes)
                    {
                        if(!IsGraphUsedInAttribute(attrType))
                            continue;

                        object value = edge.GetAttribute(attrType.Name);
                        sw.Write("{0}.{1} = ", context.graph.GetElementName(edge), attrType.Name);
                        EmitAttribute(attrType, value, context.graph, sw);
                        sw.Write("\n");
                    }
                }
            }
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
            else if(attrType.Kind == AttributeKind.DequeAttr)
            {
                IDeque deque = (IDeque)value;
                sw.Write("{0}]", attrType.GetKindName());
                bool first = true;
                foreach(object entry in deque)
                {
                    if(first) { sw.Write(ToString(entry, attrType.ValueType, graph)); first = false; }
                    else { sw.Write("," + ToString(entry, attrType.ValueType, graph)); }
                }
                sw.Write("[");
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
                return graph.Model.Serialize(value, type, graph);
            case AttributeKind.GraphAttr:
                if(value != null && GraphExportContext.graphToContext != null)
                    return "\"" + GraphExportContext.graphToContext[(INamedGraph)value].name + "\"";
                else
                    return "null";
            case AttributeKind.EnumAttr:
                return type.EnumType.Name + "::" + type.EnumType[(int)value].Name;
            case AttributeKind.NodeAttr:
            case AttributeKind.EdgeAttr:
                if(value != null)
                    return graph.GetElementName((IGraphElement)value);
                else
                    return "null";
            default:
                throw new Exception("Unsupported attribute kind in export");
            }
        }

        private static bool IsNodeOrEdgeUsedInAttribute(AttributeType attrType)
        {
            if(attrType.Kind == AttributeKind.NodeAttr
                || attrType.Kind == AttributeKind.EdgeAttr)
                return true;

            if(attrType.Kind == AttributeKind.SetAttr
                || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr
                || attrType.Kind == AttributeKind.DequeAttr)
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

        private static bool IsGraphUsedInAttribute(AttributeType attrType)
        {
            if(attrType.Kind == AttributeKind.GraphAttr)
                return true;

            if(attrType.Kind == AttributeKind.SetAttr
                || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr
                || attrType.Kind == AttributeKind.DequeAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.GraphAttr)
                    return true;
            }
            if(attrType.Kind == AttributeKind.MapAttr)
            {
                if(attrType.KeyType.Kind == AttributeKind.GraphAttr)
                    return true;
            }
            return false;
        }
    }
}
