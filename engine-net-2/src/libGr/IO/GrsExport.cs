/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    public class GraphExportContext
    {
        public GraphExportContext(MainGraphExportContext mainGraphContext, INamedGraph graph)
        {
            this.graph = graph;
            this.mainGraphContext = mainGraphContext ?? (MainGraphExportContext)this;
            if(this.mainGraphContext.nameToContext.ContainsKey(graph.Name))
            {
                int id = 0;
                while(this.mainGraphContext.nameToContext.ContainsKey(graph.Name + "_" + id.ToString()))
                {
                    ++id;
                }
                this.name = graph.Name + "_" + id.ToString();
            }
            else
                this.name = graph.Name;
        }

        public readonly INamedGraph graph;
        public readonly string name;

        public readonly MainGraphExportContext mainGraphContext = null; // points to the main graph context, points to self in the main graph context

        public string modelPathPrefix = null;

        // potential todo: decide based on model
        public bool graphElementUsedInAttribute = false;
        public bool subgraphUsedInAttribute = false;
        public bool internalClassObjectUsedInAttribute = false;

        public bool isExported = false;
        public bool areGraphAttributesExported = false; // needed due to Recorder

        public int numInternalClassObjects = 0;

        private Dictionary<String, IObject> persistentNameToObject = new Dictionary<string, IObject>();

        public bool HasPersistentName(IObject obj)
        {
            return persistentNameToObject.ContainsKey(obj.GetObjectName());
        }

        public string GetOrAssignPersistentName(IObject obj)
        {
            if(HasPersistentName(obj))
                return obj.GetObjectName();
            else
            {
                String persistentName = obj.GetObjectName();
                persistentNameToObject.Add(persistentName, obj);
                return persistentName;
            }
        }
    }

    public class MainGraphExportContext : GraphExportContext
    {
        public MainGraphExportContext(INamedGraph graph)
            : base(null, graph)
        {
        }

        // the name used for export may be different from the real graph name
        // here we ensure the uniqueness needed for an export / for getting an importable serialization
        public readonly Dictionary<INamedGraph, GraphExportContext> graphToContext = new Dictionary<INamedGraph, GraphExportContext>();
        public readonly Dictionary<string, GraphExportContext> nameToContext = new Dictionary<string, GraphExportContext>();

        public bool noNewGraph = false;
        public Dictionary<String, Dictionary<String, String>> typesToAttributesToSkip = null;
    }

    /// <summary>
    /// Exports graphs to the GRS format.
    /// </summary>
    public class GRSExport : IDisposable
    {
        readonly StreamWriter writer;

        protected GRSExport(String filename) 
            : this(new StreamWriter(filename, false, System.Text.Encoding.UTF8))
        {
        }

        protected GRSExport(StreamWriter writer)
        {
            this.writer = writer;
        }

        public void Dispose()
        {
            if(writer != null)
                writer.Dispose();
        }

        /// <summary>
        /// Exports the given graph to a GRS file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="exportFilename">The filename for the exported file.</param>
        public static void Export(INamedGraph graph, String exportFilename)
        {
            using (GRSExport export = new GRSExport(exportFilename))
                export.Export(graph, false, null);
        }

        /// <summary>
        /// Exports the given graph to a GRS file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="exportFilename">The filename for the exported file.</param>
        /// <param name="noNewGraph">If true, the new graph command is not emitted.</param>
        /// <param name="typesToAttributesToSkip">Gives a dictionary with type names containing a dictionary with attribute names that are not to be emitted</param>
        public static void Export(INamedGraph graph, String exportFilename, bool noNewGraph, Dictionary<String, Dictionary<String, String>> typesToAttributesToSkip)
        {
            using(GRSExport export = new GRSExport(exportFilename))
                export.Export(graph, noNewGraph, typesToAttributesToSkip);
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
                export.Export(graph, false, null);
        }

        /// <summary>
        /// Exports the given graph to the file given by the stream writer in grs format.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="writer">The stream writer to export to.</param>
        /// <param name="noNewGraph">If true, the new graph command is not emitted.</param>
        /// <param name="typesToAttributesToSkip">Gives a dictionary with type names containing a dictionary with attribute names that are not to be emitted</param>
        public static void Export(INamedGraph graph, StreamWriter writer, bool noNewGraph, Dictionary<String, Dictionary<String, String>> typesToAttributesToSkip)
        {
            using (GRSExport export = new GRSExport(writer))
                export.Export(graph, noNewGraph, typesToAttributesToSkip);
        }

        protected void Export(INamedGraph graph, bool noNewGraph, Dictionary<String, Dictionary<String, String>> typesToAttributesToSkip)
        {
            ExportYouMustCloseStreamWriter(graph, writer, "", noNewGraph, typesToAttributesToSkip);
        }

        /// <summary>
        /// Exports the given graph to the file given by the stream writer in grs format.
        /// Any errors will be reported by exception.
        /// Returns the graph export context of the main graph.
        /// </summary>
        /// <param name="graph">The graph to export. Must be a named graph.</param>
        /// <param name="sw">The stream writer of the file to export into. The stream writer is not closed automatically.</param>
        /// <param name="modelPathPrefix">Path to the model.</param>
        /// <param name="noNewGraph">If true, the new graph command is not emitted.</param>
        /// <param name="typesToAttributesToSkip">Gives a dictionary with type names containing a dictionary with attribute names that are not to be emitted</param>
        public static MainGraphExportContext ExportYouMustCloseStreamWriter(INamedGraph graph, StreamWriter sw, string modelPathPrefix, bool noNewGraph, Dictionary<String, Dictionary<String, String>> typesToAttributesToSkip)
        {
            MainGraphExportContext mainGraphContext = new MainGraphExportContext(graph);
            mainGraphContext.graphToContext[mainGraphContext.graph] = mainGraphContext;
            mainGraphContext.nameToContext[mainGraphContext.name] = mainGraphContext;
            mainGraphContext.modelPathPrefix = modelPathPrefix;
            mainGraphContext.noNewGraph = noNewGraph;
            if(typesToAttributesToSkip != null)
            {
                mainGraphContext.typesToAttributesToSkip = new Dictionary<string, Dictionary<string, string>>();
                foreach(KeyValuePair<String, Dictionary<String, String>> typeContainingAttributeToSkip in typesToAttributesToSkip)
                {
                    mainGraphContext.typesToAttributesToSkip.Add(typeContainingAttributeToSkip.Key, new Dictionary<string, string>());
                    foreach(KeyValuePair<String, String> attributeToSkip in typeContainingAttributeToSkip.Value)
                    {
                        mainGraphContext.typesToAttributesToSkip[typeContainingAttributeToSkip.Key].Add(attributeToSkip.Key, null);
                    }
                }
            }

            sw.WriteLine("# begin of graph \"{0}\" saved by GrsExport", mainGraphContext.name);
            sw.WriteLine();

            bool subgraphAdded = ExportSingleGraph(mainGraphContext, mainGraphContext, sw);

            if(subgraphAdded)
            {
restart:
                foreach(KeyValuePair<string, GraphExportContext> kvp in mainGraphContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    if(!context.isExported)
                    {
                        subgraphAdded = ExportSingleGraph(mainGraphContext, context, sw);
                        if(subgraphAdded)
                            goto restart;
                    }
                }

                foreach(KeyValuePair<string, GraphExportContext> kvp in mainGraphContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    EmitSubgraphAttributes(mainGraphContext, context, sw);
                }

                sw.WriteLine("in \"" + mainGraphContext.name + "\""); // after import in main graph
            }

            sw.WriteLine("# end of graph \"{0}\" saved by GrsExport", mainGraphContext.name);
            sw.WriteLine();

            return mainGraphContext;
        }

        public static bool ExportSingleGraph(MainGraphExportContext mainGraphContext, 
            GraphExportContext context, StreamWriter sw)
        {
            bool subgraphAdded = false;

            if(context.modelPathPrefix != null)
            {
                if(!mainGraphContext.noNewGraph)
                    sw.WriteLine("new graph \"" + context.modelPathPrefix + context.graph.Model.ModelName + "\" \"" + context.name + "\"");
            }
            else
                sw.WriteLine("add new graph \"" + context.name + "\"");

            // emit nodes
            int numNodes = 0;
            foreach(INode node in context.graph.Nodes)
            {
                sw.Write("new :{0}($ = \"{1}\"", node.Type.PackagePrefixedName, context.graph.GetElementName(node));
                foreach(AttributeType attrType in node.Type.AttributeTypes)
                {
                    if(IsAttributeToBeSkipped(mainGraphContext, attrType))
                        continue;
                    if(IsGraphElementUsedInAttribute(attrType))
                    {
                        context.graphElementUsedInAttribute = true;
                        continue;
                    }
                    if(IsGraphUsedInAttribute(attrType))
                    {
                        context.subgraphUsedInAttribute = true;
                        subgraphAdded |= AddSubgraphAsNeeded(mainGraphContext, node, attrType);
                        continue;
                    }
                    if(IsInternalClassObjectUsedInAttribute(attrType))
                    {
                        context.internalClassObjectUsedInAttribute = true;
                        continue;
                    }

                    object value = node.GetAttribute(attrType.Name);
                    EmitAttributeInitialization(mainGraphContext, attrType, value, context.graph, sw, null);
                }
                sw.WriteLine(")");
                ++numNodes;
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
                        edge.Type.PackagePrefixedName, context.graph.GetElementName(edge));
                    foreach(AttributeType attrType in edge.Type.AttributeTypes)
                    {
                        if(IsAttributeToBeSkipped(mainGraphContext, attrType))
                            continue;
                        if(IsGraphElementUsedInAttribute(attrType))
                        {
                            context.graphElementUsedInAttribute = true;
                            continue;
                        }
                        if(IsGraphUsedInAttribute(attrType))
                        {
                            context.subgraphUsedInAttribute = true;
                            subgraphAdded |= AddSubgraphAsNeeded(mainGraphContext, edge, attrType);
                            continue;
                        }
                        if(IsInternalClassObjectUsedInAttribute(attrType))
                        {
                            context.internalClassObjectUsedInAttribute = true;
                            continue;
                        }

                        object value = edge.GetAttribute(attrType.Name);
                        // TODO: Add support for null values, as the default initializers could assign non-null values!
                        if(value != null)
                        {
                            EmitAttributeInitialization(mainGraphContext, attrType, value, context.graph, sw, null);
                        }
                    }
                    sw.WriteLine(") -> @(\"{0}\")", context.graph.GetElementName(edge.Target));
                    ++numEdges;
                }
            }
            if(context.modelPathPrefix != null)
                sw.WriteLine("# total number of edges: {0}", numEdges);
            else
                sw.WriteLine("# total number of edges in subgraph {0}: {1}", context.name, numEdges);
            sw.WriteLine();

            // emit node/edge valued attributes
            if(context.graphElementUsedInAttribute)
            {
                foreach(INode node in context.graph.Nodes)
                {
                    foreach(AttributeType attrType in node.Type.AttributeTypes)
                    {
                        if(IsAttributeToBeSkipped(mainGraphContext, attrType))
                            continue;
                        if(!IsGraphElementUsedInAttribute(attrType))
                            continue;
                        if(IsGraphUsedInAttribute(attrType))
                            continue;
                        if(IsInternalClassObjectUsedInAttribute(attrType))
                            continue;

                        object value = node.GetAttribute(attrType.Name);
                        sw.Write("@(\"{0}\").{1} = ", context.graph.GetElementName(node), attrType.Name);
                        EmitAttribute(mainGraphContext, attrType, value, context.graph, sw, null);
                        sw.Write("\n");
                    }

                    foreach(IEdge edge in node.Outgoing)
                    {
                        foreach(AttributeType attrType in edge.Type.AttributeTypes)
                        {
                            if(IsAttributeToBeSkipped(mainGraphContext, attrType))
                                continue;
                            if(!IsGraphElementUsedInAttribute(attrType))
                                continue;
                            if(IsGraphUsedInAttribute(attrType))
                                continue;
                            if(IsInternalClassObjectUsedInAttribute(attrType))
                                continue;

                            object value = edge.GetAttribute(attrType.Name);
                            sw.Write("@(\"{0}\").{1} = ", context.graph.GetElementName(edge), attrType.Name);
                            EmitAttribute(mainGraphContext, attrType, value, context.graph, sw, null);
                            sw.Write("\n");
                        }
                    }
                }
            }

            // emit internal class objects
            foreach(INode node in context.graph.Nodes)
            {
                foreach(AttributeType attrType in node.Type.AttributeTypes)
                {
                    if(!IsInternalClassObjectUsedInAttribute(attrType))
                        continue;

                    object value = node.GetAttribute(attrType.Name);
                    EmitAttributeInitialization(mainGraphContext, node, attrType, value, context.graph, sw);
                }
            }
            foreach(IEdge edge in context.graph.Edges)
            {
                foreach(AttributeType attrType in edge.Type.AttributeTypes)
                {
                    if(!IsInternalClassObjectUsedInAttribute(attrType))
                        continue;

                    object value = edge.GetAttribute(attrType.Name);
                    EmitAttributeInitialization(mainGraphContext, edge, attrType, value, context.graph, sw);
                }
            }
            if(context.modelPathPrefix != null)
                sw.WriteLine("# total number of internal class objects: {0}", context.numInternalClassObjects);
            else
                sw.WriteLine("# total number of internal class objects in subgraph {0}: {1}", context.name, context.numInternalClassObjects);
            sw.WriteLine();

            if(!context.subgraphUsedInAttribute)
                context.areGraphAttributesExported = true;
            context.isExported = true;
            return subgraphAdded;
        }

        private static bool AddSubgraphAsNeeded(MainGraphExportContext mainGraphContext,
            IGraphElement elem, AttributeType attrType)
        {
            if(attrType.Kind == AttributeKind.GraphAttr)
                return AddSubgraphAsNeeded(mainGraphContext, (INamedGraph)elem.GetAttribute(attrType.Name));

            bool graphAdded = false;

            if(attrType.Kind == AttributeKind.SetAttr
                || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr
                || attrType.Kind == AttributeKind.DequeAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.GraphAttr)
                {
                    if(attrType.Kind == AttributeKind.SetAttr)
                    {
                        IDictionary set = (IDictionary)elem.GetAttribute(attrType.Name);
                        foreach(DictionaryEntry entry in set)
                        {
                            graphAdded |= AddSubgraphAsNeeded(mainGraphContext, (INamedGraph)entry.Key);
                        }
                    }
                    else if(attrType.Kind == AttributeKind.MapAttr)
                    {
                        IDictionary map = (IDictionary)elem.GetAttribute(attrType.Name);
                        foreach(DictionaryEntry entry in map)
                        {
                            graphAdded |= AddSubgraphAsNeeded(mainGraphContext, (INamedGraph)entry.Value);
                        }
                    }
                    else if(attrType.Kind == AttributeKind.ArrayAttr)
                    {
                        IList array = (IList)elem.GetAttribute(attrType.Name);
                        foreach(object entry in array)
                        {
                            graphAdded |= AddSubgraphAsNeeded(mainGraphContext, (INamedGraph)entry);
                        }
                    }
                    else if(attrType.Kind == AttributeKind.DequeAttr)
                    {
                        IDeque deque = (IDeque)elem.GetAttribute(attrType.Name);
                        foreach(object entry in deque)
                        {
                            graphAdded |= AddSubgraphAsNeeded(mainGraphContext, (INamedGraph)entry);
                        }
                    }
                }
            }

            if(attrType.Kind == AttributeKind.MapAttr)
            {
                if(attrType.KeyType.Kind == AttributeKind.GraphAttr)
                {
                    IDictionary map = (IDictionary)elem.GetAttribute(attrType.Name);
                    foreach(DictionaryEntry entry in map)
                    {
                        graphAdded |= AddSubgraphAsNeeded(mainGraphContext, (INamedGraph)entry.Key);
                    }
                }
            }

            return graphAdded;
        }

        public static bool AddSubgraphAsNeeded(MainGraphExportContext mainGraphContext, 
            INamedGraph graph)
        {
            if(graph!=null && !mainGraphContext.graphToContext.ContainsKey(graph))
            {
                GraphExportContext subgraphContext = new GraphExportContext(mainGraphContext, graph);
                mainGraphContext.graphToContext[graph] = subgraphContext;
                mainGraphContext.nameToContext[subgraphContext.name] = subgraphContext;
                return true;
            }
            else
                return false;
        }

        public static void EmitSubgraphAttributes(MainGraphExportContext mainGraphContext,
            GraphExportContext context, StreamWriter sw)
        {
            if(context.areGraphAttributesExported)
                return;

            sw.WriteLine("in \"" + context.name + "\"");

            foreach(INode node in context.graph.Nodes)
            {
                foreach(AttributeType attrType in node.Type.AttributeTypes)
                {
                    if(IsAttributeToBeSkipped(mainGraphContext, attrType))
                        continue;
                    if(!IsGraphUsedInAttribute(attrType))
                        continue;

                    object value = node.GetAttribute(attrType.Name);
                    sw.Write("@(\"{0}\").{1} = ", context.graph.GetElementName(node), attrType.Name);
                    EmitAttribute(mainGraphContext, attrType, value, context.graph, sw, null);
                    sw.Write("\n");
                }

                foreach(IEdge edge in node.Outgoing)
                {
                    foreach(AttributeType attrType in edge.Type.AttributeTypes)
                    {
                        if(IsAttributeToBeSkipped(mainGraphContext, attrType))
                            continue;
                        if(!IsGraphUsedInAttribute(attrType))
                            continue;

                        object value = edge.GetAttribute(attrType.Name);
                        sw.Write("@(\"{0}\").{1} = ", context.graph.GetElementName(edge), attrType.Name);
                        EmitAttribute(mainGraphContext, attrType, value, context.graph, sw, null);
                        sw.Write("\n");
                    }
                }
            }

            context.areGraphAttributesExported = true;
        }

        /// <summary>
        /// Emits the node/edge attribute initialization code in graph exporting
        /// for an attribute of the given type with the given value into the stream writer.
        /// </summary>
        private static void EmitAttributeInitialization(MainGraphExportContext mainGraphContext,
            AttributeType attrType, object value, INamedGraph graph, StreamWriter sw, StringBuilder deferredInits)
        {
            sw.Write(", {0} = ", attrType.Name);
            EmitAttribute(mainGraphContext, attrType, value, graph, sw, deferredInits);
        }

        /// <summary>
        /// Emits the node/edge/internal class object attribute initialization code in graph exporting
        /// for an attribute of internal class object type with the given value into the stream writer.
        /// </summary>
        private static void EmitAttributeInitialization(MainGraphExportContext mainGraphContext, IAttributeBearer owner,
            AttributeType attrType, object value, INamedGraph graph, StreamWriter sw)
        {
            if(owner is IGraphElement)
            {
                String persistentName = graph.GetElementName((IGraphElement)owner);
                sw.Write("@(\"{0}\").{1} = ", persistentName, attrType.Name);
            }
            else
            {
                String persistentName = mainGraphContext.GetOrAssignPersistentName((IObject)owner);
                sw.Write("@@(\"{0}\").{1} = ", persistentName, attrType.Name);
            }

            StringBuilder deferredInits = new StringBuilder();
            if(value == null)
                sw.Write("null");
            else if(value is IObject)
            {
                IObject obj = (IObject)value;
                EmitObjectFetchingOrCreation(mainGraphContext, obj.Type, obj, graph, sw, deferredInits);
            }
            else // container
                EmitAttribute(mainGraphContext, attrType, value, graph, sw, deferredInits);
            sw.WriteLine();
            sw.Write(deferredInits);
        }

        private static void EmitObjectFetchingOrCreation(MainGraphExportContext mainGraphContext,
            ObjectType objType, IObject obj, INamedGraph graph, StreamWriter sw, StringBuilder deferredInits)
        {
            if(mainGraphContext.HasPersistentName(obj))
                sw.Write("@@(\"{0}\")", mainGraphContext.GetOrAssignPersistentName(obj));
            else
                EmitObjectCreation(mainGraphContext, objType, obj, graph, sw, deferredInits);
        }

        public static void EmitObjectCreation(MainGraphExportContext mainGraphContext,
            ObjectType objType, IObject obj, INamedGraph graph, StreamWriter sw, StringBuilder deferredInits)
        {
            sw.Write("new {0}@(% = \"{1}\"", objType.PackagePrefixedName, mainGraphContext.GetOrAssignPersistentName(obj));
            foreach(AttributeType attrType in objType.AttributeTypes)
            {
                if(IsInternalClassObjectUsedInAttribute(attrType))
                    continue;

                sw.Write(", {0} = ", attrType.Name);
                EmitAttribute(mainGraphContext, attrType, obj.GetAttribute(attrType.Name), graph, sw, deferredInits);
            }
            sw.Write(")");
            ++mainGraphContext.numInternalClassObjects;

            foreach(AttributeType attrType in objType.AttributeTypes)
            {
                if(!IsInternalClassObjectUsedInAttribute(attrType))
                    continue;

                object value = obj.GetAttribute(attrType.Name);
                MemoryStream memStream = new MemoryStream();
                StreamWriter deferredSw = new StreamWriter(memStream);
                    EmitAttributeInitialization(mainGraphContext, obj, attrType, value, graph, deferredSw);
                deferredSw.Flush();
                memStream.Seek(0, SeekOrigin.Begin);
                byte[] buffer = new byte[memStream.Length];
                int read = memStream.Read(buffer, 0, (int)memStream.Length);
                String deferred = System.Text.Encoding.UTF8.GetString(buffer);
                deferredInits.Append(deferred);
            }
        }

        /// <summary>
        /// Emits the attribute value as code
        /// for an attribute of the given type with the given value into the stream writer
        /// Main graph context is needed to get access to the graph -> env dictionary.
        /// </summary>
        public static void EmitAttribute(MainGraphExportContext mainGraphContext,
            AttributeType attrType, object value, INamedGraph graph, StreamWriter sw, StringBuilder deferredInits)
        {
            if(attrType.Kind==AttributeKind.SetAttr)
            {
                IDictionary set = (IDictionary)value;
                sw.Write("{0}{{", attrType.GetKindName());
                bool first = true;
                foreach(DictionaryEntry entry in set)
                {
                    if(first)
                    {
                        EmitAttributeValue(mainGraphContext, entry.Key, attrType.ValueType, graph, sw, deferredInits);
                        first = false;
                    }
                    else
                    {
                        sw.Write(",");
                        EmitAttributeValue(mainGraphContext, entry.Key, attrType.ValueType, graph, sw, deferredInits);
                    }
                }
                sw.Write("}");
            }
            else if(attrType.Kind==AttributeKind.MapAttr)
            {
                IDictionary map = (IDictionary)value;
                sw.Write("{0}{{", attrType.GetKindName());
                bool first = true;
                foreach(DictionaryEntry entry in map)
                {
                    if(first)
                    {
                        EmitAttributeValue(mainGraphContext, entry.Key, attrType.KeyType, graph, sw, deferredInits);
                        sw.Write("->");
                        EmitAttributeValue(mainGraphContext, entry.Value, attrType.ValueType, graph, sw, deferredInits);
                        first = false;
                    }
                    else
                    {
                        sw.Write(",");
                        EmitAttributeValue(mainGraphContext, entry.Key, attrType.KeyType, graph, sw, deferredInits);
                        sw.Write("->");
                        EmitAttributeValue(mainGraphContext, entry.Value, attrType.ValueType, graph, sw, deferredInits);
                    }
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
                    if(first)
                    {
                        EmitAttributeValue(mainGraphContext, entry, attrType.ValueType, graph, sw, deferredInits);
                        first = false;
                    }
                    else
                    {
                        sw.Write(",");
                        EmitAttributeValue(mainGraphContext, entry, attrType.ValueType, graph, sw, deferredInits);
                    }
                }
                sw.Write("]");
            }
            else if(attrType.Kind == AttributeKind.DequeAttr)
            {
                IDeque deque = (IDeque)value;
                sw.Write("{0}[", attrType.GetKindName());
                bool first = true;
                foreach(object entry in deque)
                {
                    if(first)
                    {
                        EmitAttributeValue(mainGraphContext, entry, attrType.ValueType, graph, sw, deferredInits);
                        first = false;
                    }
                    else
                    {
                        sw.Write(",");
                        EmitAttributeValue(mainGraphContext, entry, attrType.ValueType, graph, sw, deferredInits);
                    }
                }
                sw.Write("]");
            }
            else
                EmitAttributeValue(mainGraphContext, value, attrType, graph, sw, deferredInits);
        }

        /// <summary>
        /// Type needed for enum, otherwise null ok.
        /// Graph needed for node/edge, otherwise null ok.
        /// Main graph context needed to get access to the graph -> env dictionary for subgraph.
        /// </summary>
        public static void EmitAttributeValue(MainGraphExportContext mainGraphContext,
            object value, AttributeType type, INamedGraph graph, StreamWriter sw, StringBuilder deferredInits)
        {
            switch(type.Kind)
            {
            case AttributeKind.ByteAttr:
                sw.Write(((sbyte)value).ToString()+"Y");
                return;
            case AttributeKind.ShortAttr:
                sw.Write(((short)value).ToString()+"S");
                return;
            case AttributeKind.IntegerAttr:
                sw.Write(((int)value).ToString());
                return;
            case AttributeKind.LongAttr:
                sw.Write(((long)value).ToString()+"L");
                return;
            case AttributeKind.BooleanAttr:
                sw.Write(((bool)value).ToString());
                return;
            case AttributeKind.StringAttr:
                if(value == null)
                    sw.Write("\"\"");
                else
                    sw.Write("\"" + ((string)value).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"");
                return;
            case AttributeKind.FloatAttr:
                sw.Write(((float)value).ToString(System.Globalization.CultureInfo.InvariantCulture)+"f");
                return;
            case AttributeKind.DoubleAttr:
                sw.Write(((double)value).ToString(System.Globalization.CultureInfo.InvariantCulture));
                return;
            case AttributeKind.ObjectAttr:
                sw.Write(graph.Model.Serialize(value, type, graph));
                return;
            case AttributeKind.GraphAttr:
                if(value != null && mainGraphContext != null && mainGraphContext.graphToContext != null)
                    sw.Write("\"" + mainGraphContext.graphToContext[(INamedGraph)value].name + "\"");
                else
                    sw.Write("null");
                return;
            case AttributeKind.EnumAttr:
                sw.Write(type.EnumType.PackagePrefixedName + "::" + type.EnumType[(int)value].Name);
                return;
            case AttributeKind.NodeAttr:
            case AttributeKind.EdgeAttr:
                if(value != null)
                    sw.Write("@(\"" + graph.GetElementName((IGraphElement)value) + "\")");
                else
                    sw.Write("null");
                return;
            case AttributeKind.InternalClassObjectAttr:
                if(value != null)
                {
                    IObject obj = (IObject)value;
                    EmitObjectFetchingOrCreation(mainGraphContext, obj.Type, obj, graph, sw, deferredInits);
                }
                else
                    sw.Write("null");
                return;
            default:
                throw new Exception("Unsupported attribute kind in export");
            }
        }

        private static bool IsAttributeToBeSkipped(MainGraphExportContext mainGraphContext, AttributeType attrType)
        {
            if(mainGraphContext.typesToAttributesToSkip != null
                && mainGraphContext.typesToAttributesToSkip.ContainsKey(attrType.OwnerType.PackagePrefixedName)
                && mainGraphContext.typesToAttributesToSkip[attrType.OwnerType.PackagePrefixedName].ContainsKey(attrType.Name))
            {
                return true;
            }

            return false;
        }

        private static bool IsGraphElementUsedInAttribute(AttributeType attrType)
        {
            if(attrType.Kind == AttributeKind.NodeAttr
                || attrType.Kind == AttributeKind.EdgeAttr)
            {
                return true;
            }
            if(attrType.Kind == AttributeKind.SetAttr
                || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr
                || attrType.Kind == AttributeKind.DequeAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.NodeAttr
                    || attrType.ValueType.Kind == AttributeKind.EdgeAttr)
                {
                    if(attrType.Kind == AttributeKind.MapAttr
                        && attrType.KeyType.Kind == AttributeKind.InternalClassObjectAttr)
                    {
                        return false; // internal class has priority, is handled in a following step of the calling algorithm
                    }
                    return true;
                }
            }
            if(attrType.Kind == AttributeKind.MapAttr)
            {
                if(attrType.KeyType.Kind == AttributeKind.NodeAttr
                    || attrType.KeyType.Kind == AttributeKind.EdgeAttr)
                {
                    if(attrType.ValueType.Kind == AttributeKind.InternalClassObjectAttr)
                        return false; // internal class has priority, is handled in a following step of the calling algorithm
                    return true;
                }
            }
            return false;
        }

        public static bool IsGraphUsedInAttribute(AttributeType attrType)
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

        private static bool IsInternalClassObjectUsedInAttribute(AttributeType attrType)
        {
            if(attrType.Kind == AttributeKind.InternalClassObjectAttr)
                return true;
            if(attrType.Kind == AttributeKind.SetAttr
                || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr
                || attrType.Kind == AttributeKind.DequeAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.InternalClassObjectAttr)
                    return true;
            }
            if(attrType.Kind == AttributeKind.MapAttr)
            {
                if(attrType.KeyType.Kind == AttributeKind.InternalClassObjectAttr)
                    return true;
            }
            return false;
        }
    }
}
