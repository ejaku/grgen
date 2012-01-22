/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A class for recording changes (and their causes) applied to a graph into a file,
    /// so that they can get replayed.
    /// </summary>
    public class Recorder : IRecorder
    {
        INamedGraph graph = null;
        IGraphProcessingEnvironment procEnv = null;

        private IDictionary<string, StreamWriter> recordings = new Dictionary<string, StreamWriter>();
        

        /// <summary>
        /// Create a recorder
        /// </summary>
        /// <param name="graph">The named graph whose changes are to be recorded</param>
        /// <param name="procEnv">The graph processing environment receiving some of the action events, may be null if only graph changes are requested</param>
        public Recorder(INamedGraph graph, IGraphProcessingEnvironment procEnv)
        {
            this.graph = graph;
            this.procEnv = procEnv;
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
                GRSExport.ExportYouMustCloseStreamWriter(graph, writer, pathPrefix);

                recordings.Add(new KeyValuePair<string, StreamWriter>(filename, writer));
            }
        }

        public void StopRecording(string filename)
        {
            if(recordings.ContainsKey(filename))
            {
                recordings[filename].Close();
                recordings.Remove(filename);

                if(recordings.Count == 0)
                    UnsubscribeEvents();
            }
        }

        public bool IsRecording(string filename)
        {
            return recordings.ContainsKey(filename);
        }

        public void Write(string value)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.Write(value);
        }

        public void WriteLine(string value)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.Write(value + "\n");
        }

        public void Flush()
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.Flush();
        }

        private void SubscribeEvents()
        {
            graph.OnNodeAdded += NodeAdded;
            graph.OnEdgeAdded += EdgeAdded;
            graph.OnRemovingNode += RemovingNode;
            graph.OnRemovingEdge += RemovingEdge;
            graph.OnChangingNodeAttribute += ChangingAttribute;
            graph.OnChangingEdgeAttribute += ChangingAttribute;
            graph.OnRetypingNode += RetypingNode;
            graph.OnRetypingEdge += RetypingEdge;

            if(procEnv != null)
            {
                procEnv.OnFinishing += BeforeFinish;
                procEnv.OnRewritingNextMatch += RewriteNextMatch;
                procEnv.OnFinished += AfterFinish;
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
            graph.OnRetypingNode -= RetypingNode;
            graph.OnRetypingEdge -= RetypingEdge;

            if(procEnv != null)
            {
                procEnv.OnFinishing -= BeforeFinish;
                procEnv.OnRewritingNextMatch += RewriteNextMatch;
                procEnv.OnFinished -= AfterFinish;
            }
        }

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Event handler for IGraph.OnNodeAdded.
        /// </summary>
        /// <param name="node">The added node.</param>
        void NodeAdded(INode node)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("new :" + node.Type.Name + "($=\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnEdgeAdded.
        /// </summary>
        /// <param name="edge">The added edge.</param>
        void EdgeAdded(IEdge edge)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("new @(\"" + graph.GetElementName(edge.Source)
                    + "\") -:" + edge.Type.Name + "($=\"" + graph.GetElementName(edge) + "\")-> @(\""
                    + graph.GetElementName(edge.Target) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingNode.
        /// </summary>
        /// <param name="node">The node to be deleted.</param>
        void RemovingNode(INode node)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("delete node @(\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingEdge.
        /// </summary>
        /// <param name="edge">The edge to be deleted.</param>
        void RemovingEdge(IEdge edge)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("delete edge @(\"" + graph.GetElementName(edge) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnChangingNodeAttribute and IGraph.OnChangingEdgeAttribute.
        /// </summary>
        /// <param name="element">The node or edge whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
        void ChangingAttribute(IGraphElement element, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            foreach(StreamWriter writer in recordings.Values)
                switch(changeType)
                {
                case AttributeChangeType.Assign:
                    writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name + " = ");
                    GRSExport.EmitAttribute(attrType, newValue, graph, writer);
                    writer.WriteLine();
                    break;
                case AttributeChangeType.PutElement:
                    writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        writer.Write(".add(");
                        writer.Write(GRSExport.ToString(newValue, attrType.ValueType, graph));
                        writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        writer.Write(".add(");
                        writer.Write(GRSExport.ToString(keyValue, attrType.KeyType, graph));
                        writer.Write(", ");
                        writer.Write(GRSExport.ToString(newValue, attrType.ValueType, graph));
                        writer.WriteLine(")");
                        break;
                    case AttributeKind.ArrayAttr:
                        if(keyValue == null)
                        {
                            writer.Write(".add(");
                            writer.Write(GRSExport.ToString(newValue, attrType.ValueType, graph));
                            writer.WriteLine(")");
                        }
                        else
                        {
                            writer.Write(".add(");
                            writer.Write(GRSExport.ToString(newValue, attrType.ValueType, graph));
                            writer.Write(", ");
                            writer.Write(GRSExport.ToString(keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null), graph));
                            writer.WriteLine(")");
                        }
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                case AttributeChangeType.RemoveElement:
                    writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        writer.Write(".rem(");
                        writer.Write(GRSExport.ToString(newValue, attrType.ValueType, graph));
                        writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        writer.Write(".rem(");
                        writer.Write(GRSExport.ToString(keyValue, attrType.KeyType, graph));
                        writer.WriteLine(")");
                        break;
                    case AttributeKind.ArrayAttr:
                        writer.Write(".rem(");
                        if(keyValue!=null)
                            writer.Write(GRSExport.ToString(keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null), graph));
                        writer.WriteLine(")");
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                case AttributeChangeType.AssignElement:
                    writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.ArrayAttr:
                        writer.Write("[");
                        writer.Write(GRSExport.ToString(keyValue, new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null), graph));
                        writer.Write("] = ");
                        writer.WriteLine(GRSExport.ToString(newValue, attrType.ValueType, graph));
                        break;
                    case AttributeKind.MapAttr:
                        writer.Write("[");
                        writer.Write(GRSExport.ToString(keyValue, attrType.KeyType, graph));
                        writer.Write("] = ");
                        writer.WriteLine(GRSExport.ToString(newValue, attrType.ValueType, graph));
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                default:
                    throw new Exception("Unknown attribute change type");
                }
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingNode.
        /// </summary>
        /// <param name="oldNode">The node to be retyped.</param>
        /// <param name="newNode">The new node with the common attributes, but without the correct connections, yet.</param>
        void RetypingNode(INode oldNode, INode newNode)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("retype @(\"" + graph.GetElementName(oldNode) + "\")<" + newNode.Type.Name + ">");
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingEdge.
        /// </summary>
        /// <param name="oldEdge">The edge to be retyped.</param>
        /// <param name="newEdge">The new edge with the common attributes, but without the correct connections, yet.</param>
        void RetypingEdge(IEdge oldEdge, IEdge  newEdge)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("retype -@(\"" + graph.GetElementName(oldEdge) + "\")<" + newEdge.Type.Name + ">->");
        }

        ////////////////////////////////////////////////////////////////////////

        void BeforeFinish(IMatches matches, bool special)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# rewriting " + matches.Producer.Name + "..");
        }

        void RewriteNextMatch()
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# rewriting next match");
        }

        void AfterFinish(IMatches matches, bool special)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# ..rewritten " + matches.Producer.Name);
        }

        ////////////////////////////////////////////////////////////////////////

        public void TransactionStart(int transactionID)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# begin transaction " + transactionID);
        }

        public void TransactionCommit(int transactionID)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# commit transaction " + transactionID);
        }

        public void TransactionRollback(int transactionID, bool start)
        {
            if(start)
                foreach(StreamWriter writer in recordings.Values)
                    writer.WriteLine("# rolling back transaction " + transactionID + "..");
            else
                foreach(StreamWriter writer in recordings.Values)
                    writer.WriteLine("# ..rolled back transaction " + transactionID);
        }
    }
}
