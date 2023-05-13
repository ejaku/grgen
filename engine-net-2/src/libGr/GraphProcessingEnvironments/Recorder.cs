/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A class holding the state/context of a recording session
    /// </summary>
    class RecordingState
    {
        public RecordingState(StreamWriter writer, MainGraphExportContext mainExportContext)
        {
            this.writer = writer;
            this.mainExportContext = mainExportContext;
        }

        public readonly StreamWriter writer;
        public readonly MainGraphExportContext mainExportContext;
    }

    /// <summary>
    /// A class for recording changes (and their causes) applied to a graph into a file,
    /// so that they can get replayed.
    /// </summary>
    public class Recorder : IRecorder
    {
        INamedGraph graph = null;
        ISubactionAndOutputAdditionEnvironment subOutEnv = null;

        private IDictionary<string, RecordingState> recordings = new Dictionary<string, RecordingState>();

        /// <summary>
        /// Create a recorder
        /// </summary>
        /// <param name="graph">The named graph whose changes are to be recorded</param>
        /// <param name="subOutEnv">The subaction and output environment receiving some of the action events, may be null if only graph changes are requested</param>
        public Recorder(INamedGraph graph, ISubactionAndOutputAdditionEnvironment subOutEnv)
        {
            Initialize(graph, subOutEnv);
        }

        /// <summary>
        /// Initializes a recorder after creation, needed if actions are selected later
        /// </summary>
        /// <param name="graph">The named graph whose changes are to be recorded</param>
        /// <param name="subOutEnv">The subaction and output environment receiving some of the action events, may be null if only graph changes are requested</param>
        public void Initialize(INamedGraph graph, ISubactionAndOutputAdditionEnvironment subOutEnv)
        {
            this.graph = graph;
            this.subOutEnv = subOutEnv;
        }

        public void StartRecording(string filename)
        {
            if(!recordings.ContainsKey(filename))
            {
                if(recordings.Count == 0)
                    SubscribeEvents();

                StreamWriter writer = null;
                if(filename.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase)) {
                    FileStream filewriter = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(new GZipStream(filewriter, CompressionMode.Compress));
                } else {
                    writer = new StreamWriter(filename);
                }

                String pathPrefix = "";
                if(filename.LastIndexOf("/")!=-1 || filename.LastIndexOf("\\")!=-1)
                {
                    int lastIndex = filename.LastIndexOf("/");
                    if(lastIndex==-1) lastIndex = filename.LastIndexOf("\\");
                    pathPrefix = filename.Substring(0, lastIndex+1);
                }
                MainGraphExportContext mainGraphContext = GRSExport.ExportYouMustCloseStreamWriter(graph, writer, pathPrefix, false, null);

                recordings.Add(new KeyValuePair<string, RecordingState>(filename, 
                    new RecordingState(writer, mainGraphContext)));
            }
        }

        public void StopRecording(string filename)
        {
            if(recordings.ContainsKey(filename))
            {
                recordings[filename].writer.Close();
                recordings.Remove(filename);

                if(recordings.Count == 0)
                    UnsubscribeEvents();
            }
        }

        public bool IsRecording(string filename)
        {
            return recordings.ContainsKey(filename);
        }

        public void External(string value)
        {
            foreach(RecordingState recordingState in recordings.Values)
            {
                recordingState.writer.Write("external ");
                recordingState.writer.Write(value);
                recordingState.writer.Write("\n");
            }
        }

        public void Write(string value)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.Write(value);
        }

        public void WriteLine(string value)
        {
            foreach(RecordingState recordingState in recordings.Values)
            {
                recordingState.writer.Write(value);
                recordingState.writer.Write("\n");
            }
        }

        public void Flush()
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.Flush();
        }

        private void SubscribeEvents()
        {
            graph.OnNodeAdded += NodeAdded;
            graph.OnEdgeAdded += EdgeAdded;
            graph.OnRemovingNode += RemovingNode;
            graph.OnRemovingEdge += RemovingEdge;
            graph.OnChangingNodeAttribute += ChangingAttribute;
            graph.OnChangingEdgeAttribute += ChangingAttribute;
            graph.OnChangingObjectAttribute += ChangingAttribute;
            graph.OnRetypingNode += RetypingNode;
            graph.OnRetypingEdge += RetypingEdge;
            graph.OnVisitedAlloc += VisitedAlloc;
            graph.OnVisitedFree += VisitedFree;
            graph.OnSettingVisited += SettingVisited;

            if(subOutEnv != null)
            {
                subOutEnv.OnMatchSelected += MatchSelected;
                subOutEnv.OnRewritingSelectedMatch += RewritingSelectedMatch;
                subOutEnv.OnSwitchingToSubgraph += SwitchToGraph;
                subOutEnv.OnReturnedFromSubgraph += ReturnFromGraph;
            }
        }

        private void UnsubscribeEvents()
        {
            graph.OnNodeAdded -= NodeAdded;
            graph.OnEdgeAdded -= EdgeAdded;
            graph.OnRemovingNode -= RemovingNode;
            graph.OnRemovingEdge -= RemovingEdge;
            graph.OnChangingNodeAttribute -= ChangingAttribute;
            graph.OnChangingEdgeAttribute -= ChangingAttribute;
            graph.OnChangingObjectAttribute -= ChangingAttribute;
            graph.OnRetypingNode -= RetypingNode;
            graph.OnRetypingEdge -= RetypingEdge;
            graph.OnVisitedAlloc -= VisitedAlloc;
            graph.OnVisitedFree -= VisitedFree;
            graph.OnSettingVisited -= SettingVisited;

            if(subOutEnv != null)
            {
                subOutEnv.OnMatchSelected += MatchSelected;
                subOutEnv.OnRewritingSelectedMatch += RewritingSelectedMatch;
                subOutEnv.OnSwitchingToSubgraph -= SwitchToGraph;
                subOutEnv.OnReturnedFromSubgraph -= ReturnFromGraph;
            }
        }

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Event handler for IGraph.OnNodeAdded.
        /// </summary>
        /// <param name="node">The added node.</param>
        void NodeAdded(INode node)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("new :" + node.Type.Name + "($=\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnEdgeAdded.
        /// </summary>
        /// <param name="edge">The added edge.</param>
        void EdgeAdded(IEdge edge)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("new @(\"" + graph.GetElementName(edge.Source)
                    + "\") -:" + edge.Type.Name + "($=\"" + graph.GetElementName(edge) + "\")-> @(\""
                    + graph.GetElementName(edge.Target) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingNode.
        /// </summary>
        /// <param name="node">The node to be deleted.</param>
        void RemovingNode(INode node)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("delete node @(\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingEdge.
        /// </summary>
        /// <param name="edge">The edge to be deleted.</param>
        void RemovingEdge(IEdge edge)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("delete edge @(\"" + graph.GetElementName(edge) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingNode.
        /// </summary>
        /// <param name="oldNode">The node to be retyped.</param>
        /// <param name="newNode">The new node with the common attributes, but without the correct connections, yet.</param>
        void RetypingNode(INode oldNode, INode newNode)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("retype @(\"" + graph.GetElementName(oldNode) + "\")<" + newNode.Type.Name + ">");
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingEdge.
        /// </summary>
        /// <param name="oldEdge">The edge to be retyped.</param>
        /// <param name="newEdge">The new edge with the common attributes, but without the correct connections, yet.</param>
        void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("retype -@(\"" + graph.GetElementName(oldEdge) + "\")<" + newEdge.Type.Name + ">->");
        }

        void EmitObjectAttributeAssignmentCreatingAsNeeded(IObject owner, AttributeType attrType,
            MainGraphExportContext mainExportContext, RecordingState recordingState)
        {
            if(!mainExportContext.HasPersistentName(owner))
            {
                StringBuilder deferredInits = new StringBuilder();
                GRSExport.EmitObjectCreation(mainExportContext, owner.Type, owner, graph, recordingState.writer, deferredInits);
                recordingState.writer.WriteLine();
                recordingState.writer.Write(deferredInits.ToString());
            }
            recordingState.writer.Write("@@(\"" + mainExportContext.GetOrAssignPersistentName((IObject)owner) + "\")." + attrType.Name + " = ");
        }

        /// <summary>
        /// Event handler for IGraph.OnChangingNodeAttribute and IGraph.OnChangingEdgeAttribute and IGraph.OnChangingObjectAttribute.
        /// </summary>
        /// <param name="owner">The node or edge or object whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
        void ChangingAttribute(IAttributeBearer owner, AttributeType attrType,
                AttributeChangeType changeType, object newValue, object keyValue)
        {
            foreach(RecordingState recordingState in recordings.Values)
            {
                MainGraphExportContext mainExportContext = recordingState.mainExportContext;
                StringBuilder deferredInits = new StringBuilder();
                AddSubgraphsAsNeeded(mainExportContext, owner, attrType, newValue, recordingState.writer);
                AddSubgraphsAsNeeded(mainExportContext, owner, attrType, keyValue, recordingState.writer);
                switch(changeType)
                {
                case AttributeChangeType.Assign:
                    if(owner is IGraphElement)
                        recordingState.writer.Write("@(\"" + graph.GetElementName((IGraphElement)owner) + "\")." + attrType.Name + " = ");
                    else
                        EmitObjectAttributeAssignmentCreatingAsNeeded((IObject)owner, attrType, mainExportContext, recordingState);

                    StringBuilder sb = new StringBuilder();
                    GRSExport.EmitAttribute(mainExportContext, attrType, newValue, graph, recordingState.writer, sb);
                    recordingState.writer.WriteLine();
                    recordingState.writer.Write(sb.ToString());
                    break;
                case AttributeChangeType.PutElement:
                    if(owner is IGraphElement)
                        recordingState.writer.Write("@(\"" + graph.GetElementName((IGraphElement)owner) + "\")." + attrType.Name);
                    else
                        EmitObjectAttributeAssignmentCreatingAsNeeded((IObject)owner, attrType, mainExportContext, recordingState);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        recordingState.writer.Write(".add(");
                        GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        recordingState.writer.Write(".add(");
                        GRSExport.EmitAttributeValue(mainExportContext, keyValue, attrType.KeyType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.Write(", ");
                        GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.ArrayAttr:
                        if(keyValue == null)
                        {
                            recordingState.writer.Write(".add(");
                            GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                            recordingState.writer.WriteLine(")");
                        }
                        else
                        {
                            recordingState.writer.Write(".add(");
                            GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                            recordingState.writer.Write(", ");
                            GRSExport.EmitAttributeValue(mainExportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int)), graph, recordingState.writer, deferredInits);
                            recordingState.writer.WriteLine(")");
                        }
                        break;
                    case AttributeKind.DequeAttr:
                        if(keyValue == null)
                        {
                            recordingState.writer.Write(".add(");
                            GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                            recordingState.writer.WriteLine(")");
                        }
                        else
                        {
                            recordingState.writer.Write(".add(");
                            GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                            recordingState.writer.Write(", ");
                            GRSExport.EmitAttributeValue(mainExportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int)), graph, recordingState.writer, deferredInits);
                            recordingState.writer.WriteLine(")");
                        }
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                case AttributeChangeType.RemoveElement:
                    if(owner is IGraphElement)
                        recordingState.writer.Write("@(\"" + graph.GetElementName((IGraphElement)owner) + "\")." + attrType.Name);
                    else
                        EmitObjectAttributeAssignmentCreatingAsNeeded((IObject)owner, attrType, mainExportContext, recordingState);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        recordingState.writer.Write(".rem(");
                        GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        recordingState.writer.Write(".rem(");
                        GRSExport.EmitAttributeValue(mainExportContext, keyValue, attrType.KeyType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.ArrayAttr:
                        recordingState.writer.Write(".rem(");
                        if(keyValue!=null)
                            GRSExport.EmitAttributeValue(mainExportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int)), graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine(")");
                        break;
                    case AttributeKind.DequeAttr:
                        recordingState.writer.Write(".rem(");
                        if(keyValue != null)
                            GRSExport.EmitAttributeValue(mainExportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int)), graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine(")");
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                case AttributeChangeType.AssignElement:
                    if(owner is IGraphElement)
                        recordingState.writer.Write("@(\"" + graph.GetElementName((IGraphElement)owner) + "\")." + attrType.Name);
                    else
                        EmitObjectAttributeAssignmentCreatingAsNeeded((IObject)owner, attrType, mainExportContext, recordingState);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.ArrayAttr:
                        recordingState.writer.Write("[");
                        GRSExport.EmitAttributeValue(mainExportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int)), graph, recordingState.writer, deferredInits);
                        recordingState.writer.Write("] = ");
                        GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine();
                        break;
                    case AttributeKind.DequeAttr:
                        recordingState.writer.Write("[");
                        GRSExport.EmitAttributeValue(mainExportContext, keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int)), graph, recordingState.writer, deferredInits);
                        recordingState.writer.Write("] = ");
                        GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine();
                        break;
                    case AttributeKind.MapAttr:
                        recordingState.writer.Write("[");
                        GRSExport.EmitAttributeValue(mainExportContext, keyValue, attrType.KeyType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.Write("] = ");
                        GRSExport.EmitAttributeValue(mainExportContext, newValue, attrType.ValueType, graph, recordingState.writer, deferredInits);
                        recordingState.writer.WriteLine();
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                default:
                    throw new Exception("Unknown attribute change type");
                }
                recordingState.writer.Write(deferredInits.ToString());
            }
        }

        private bool AddSubgraphsAsNeeded(MainGraphExportContext mainExportContext,
            IAttributeBearer owner, AttributeType attrType, object value, StreamWriter writer)
        {
            if(!GRSExport.IsGraphUsedInAttribute(attrType))
                return false;

            if(value == null)
                return false;

            if(!(value is INamedGraph))
                return false;
            
            bool wasAdded = GRSExport.AddSubgraphAsNeeded(mainExportContext, (INamedGraph)value);
            if(wasAdded)
            {
            restart:
                foreach(KeyValuePair<string, GraphExportContext> kvp in mainExportContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    if(!context.isExported)
                    {
                        wasAdded = GRSExport.ExportSingleGraph(mainExportContext, context, writer);
                        if(wasAdded)
                            goto restart;
                    }
                }
                AddGraphAttributes(mainExportContext, writer);
                writer.WriteLine("in \"" + mainExportContext.graphToContext[graph].name + "\" # after emitting new subgraph for attribute");
                return true;
            }
            return false;
        }

        private static void AddGraphAttributes(MainGraphExportContext mainExportContext, StreamWriter writer)
        {
            foreach(KeyValuePair<string, GraphExportContext> kvp in mainExportContext.nameToContext)
            {
                GraphExportContext context = kvp.Value;
                GRSExport.EmitSubgraphAttributes(mainExportContext, context, writer);
            }
        }

        ////////////////////////////////////////////////////////////////////////
        
        public void VisitedAlloc(int visitorID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# valloc " + visitorID);
        }

        public void VisitedFree(int visitorID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# vfree " + visitorID);
        }

        public void SettingVisited(IGraphElement elem, int visitorID, bool newValue)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# visited[" + visitorID + "] = " + newValue);
        }

        ////////////////////////////////////////////////////////////////////////

        void MatchSelected(IMatch match, bool special, IMatches matches)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# match of " + matches.Producer.Name + " selected");
        }

        void RewritingSelectedMatch()
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# rewriting selected match");
        }

        ////////////////////////////////////////////////////////////////////////

        public void SwitchToGraph(IGraph newGraph)
        {
            IGraph oldGraph = subOutEnv.Graph;

            foreach(RecordingState recordingState in recordings.Values)
            {
                AddSubgraphsAsNeeded((INamedGraph)newGraph, recordingState);

                recordingState.writer.WriteLine("in \"" + recordingState.mainExportContext.graphToContext[(INamedGraph)newGraph].name + "\" # due to switch, before: " + oldGraph.Name);
            }

            graph = (INamedGraph)newGraph;
        }

        public void ReturnFromGraph(IGraph oldGraph)
        {
            INamedGraph newGraph = (INamedGraph)subOutEnv.Graph;
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("in \"" + recordingState.mainExportContext.graphToContext[newGraph].name + "\" # due to return, before: " + oldGraph.Name);

            graph = newGraph;
        }

        private static bool AddSubgraphsAsNeeded(INamedGraph potentialNewGraph, RecordingState recordingState)
        {
            bool wasAdded = GRSExport.AddSubgraphAsNeeded(recordingState.mainExportContext, potentialNewGraph);
            if(wasAdded)
            {
            restart:
                foreach(KeyValuePair<string, GraphExportContext> kvp in recordingState.mainExportContext.nameToContext)
                {
                    GraphExportContext context = kvp.Value;
                    if(!context.isExported)
                    {
                        wasAdded = GRSExport.ExportSingleGraph(recordingState.mainExportContext, context, recordingState.writer);
                        if(wasAdded)
                            goto restart;
                    }
                }
                AddGraphAttributes(recordingState.mainExportContext, recordingState.writer);
                return true;
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////

        public void TransactionStart(int transactionID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# begin transaction " + transactionID);
        }

        public void TransactionCommit(int transactionID)
        {
            foreach(RecordingState recordingState in recordings.Values)
                recordingState.writer.WriteLine("# commit transaction " + transactionID);
        }

        public void TransactionRollback(int transactionID, bool start)
        {
            if(start)
                foreach(RecordingState recordingState in recordings.Values)
                    recordingState.writer.WriteLine("# rolling back transaction " + transactionID + "..");
            else
                foreach(RecordingState recordingState in recordings.Values)
                    recordingState.writer.WriteLine("# ..rolled back transaction " + transactionID);
        }
    }
}
