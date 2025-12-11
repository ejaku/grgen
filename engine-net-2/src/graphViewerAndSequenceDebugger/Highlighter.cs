/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    class Highlighter
    {
        readonly IDebuggerEnvironment env;
        readonly DebuggerGraphProcessingEnvironment debuggerProcEnv;

        readonly ElementRealizers realizers;
        readonly GraphAnnotationAndChangesRecorder renderRecorder;
        readonly GraphViewerClient graphViewerClient;

        readonly Stack<SequenceBase> debugSequences;

        public Highlighter(IDebuggerEnvironment env,
            DebuggerGraphProcessingEnvironment debuggerProcEnv,
            ElementRealizers realizers,
            GraphAnnotationAndChangesRecorder renderRecorder,
            GraphViewerClient graphViewerClient,
            Stack<SequenceBase> debugSequences
        )
        {
            this.env = env;
            this.debuggerProcEnv = debuggerProcEnv;
            this.realizers = realizers;
            this.renderRecorder = renderRecorder;
            this.graphViewerClient = graphViewerClient;
            this.debugSequences = debugSequences;
        }

        public void ComputeHighlight(SequenceBase seq, String str, out List<object> values, out List<string> annotations)
        {
            values = new List<object>();
            annotations = new List<string>();

            if(str.Length == 0)
                return;

            string[] arguments = str.Split(',');

            for(int i = 0; i < arguments.Length; ++i)
            {
                string argument = arguments[i].Trim();
                if(i + 1 < arguments.Length)
                {
                    string potentialAnnotationArgument = arguments[i + 1];
                    if(potentialAnnotationArgument.StartsWith("\"") && potentialAnnotationArgument.EndsWith("\"")
                        || potentialAnnotationArgument.StartsWith("'") && potentialAnnotationArgument.EndsWith("'"))
                    {
                        ComputeHighlightArgument(seq, argument, potentialAnnotationArgument.Substring(1, argument.Length-2), values, annotations);
                        ++i; // skip the annotation argument
                    }
                }
                else
                    ComputeHighlightArgument(seq, argument, null, values, annotations);
            }
        }

        private void ComputeHighlightArgument(SequenceBase seq, string argument, string annotation, List<object> sources, List<string> annotations)
        {
            // visited flag directly given as constant
            int num;
            if(int.TryParse(argument, out num))
            {
                sources.Add(num);
                if(annotation != null)
                    annotations.Add(annotation);
                else
                    annotations.Add(num.ToString());
                return;
            }

            // variable
            Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            (debugSequences.Peek()).GetLocalVariables(seqVars, constructors, seq);
            foreach(SequenceVariable var in seqVars.Keys)
            {
                if(var.Name == argument)
                {
                    sources.Add(var.LocalVariableValue);
                    if(annotation != null)
                        annotations.Add(annotation);
                    else
                        annotations.Add(var.Name);
                    return;
                }
            }
            foreach(Variable var in debuggerProcEnv.ProcEnv.Variables)
            {
                if(var.Name == argument)
                {
                    sources.Add(var.Value);
                    if(annotation != null)
                        annotations.Add(annotation);
                    else
                        annotations.Add(var.Name);
                    return;
                }
            }
            env.WriteLine("Unknown variable " + argument + "!");
            env.WriteLine("Use (v)ariables to print variables and visited flags.");
        }

        public void DoHighlight(List<object> sources, List<string> annotations)
        {
            if(graphViewerClient.dumpInfo.IsExcludedGraph())
                graphViewerClient.ClearGraph();

            for(int i = 0; i < sources.Count; ++i)
            {
                HighlightValue(sources[i], annotations[i], true);
            }

            if(graphViewerClient.dumpInfo.IsExcludedGraph())
            {
                // highlight values added in highlight value calls to excludedGraphNodesIncluded
                graphViewerClient.AddNeighboursAndParentsOfNeededGraphElements();
            }

            renderRecorder.AnnotateGraphElements(graphViewerClient);

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();
            env.PauseUntilAnyKeyPressedToContinueDialog("Press any key to continue...");

            for(int i = 0; i < sources.Count; ++i)
            {
                HighlightValue(sources[i], annotations[i], false);
            }

            graphViewerClient.UpdateDisplay();
            graphViewerClient.Sync();

            env.WriteLine("Back from highlighting to debugging.");
        }

        private void HighlightValue(object value, string name, bool addAnnotation)
        {
            if(value is IDictionary)
                HighlightDictionary((IDictionary)value, name, addAnnotation);
            else if(value is IList)
                HighlightList((IList)value, name, addAnnotation);
            else if(value is IDeque)
                HighlightDeque((IDeque)value, name, addAnnotation);
            else
                HighlightSingleValue(value, name, addAnnotation);
        }

        private void HighlightDictionary(IDictionary value, string name, bool addAnnotation)
        {
            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(value.GetType(), out keyType, out valueType);
            if(valueType == typeof(SetValueType))
            {
                foreach(DictionaryEntry entry in value)
                {
                    if(entry.Key is IGraphElement)
                        HighlightSingleValue(entry.Key, name, addAnnotation);
                }
            }
            else
            {
                int cnt = 0;
                foreach(DictionaryEntry entry in value)
                {
                    if(entry.Key is INode && entry.Value is INode)
                    {
                        HighlightMapping((INode)entry.Key, (INode)entry.Value, name, cnt, addAnnotation);
                        ++cnt;
                    }
                    else
                    {
                        if(entry.Key is IGraphElement)
                            HighlightSingleValue(entry.Key, name + ".Domain -> " + EmitHelper.ToString(entry.Value, debuggerProcEnv.ProcEnv.NamedGraph, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null), addAnnotation);
                        if(entry.Value is IGraphElement)
                            HighlightSingleValue(entry.Value, EmitHelper.ToString(entry.Key, debuggerProcEnv.ProcEnv.NamedGraph, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, null) + " -> " + name + ".Range", addAnnotation);
                    }
                }
            }
        }

        private void HighlightList(IList value, string name, bool addAnnotation)
        {
            for(int i=0; i<value.Count; ++i)
            {
                if(value[i] is IGraphElement)
                    HighlightSingleValue(value[i], name + "[" + i + "]", addAnnotation);
                if(value[i] is INode && i >= 1)
                {
                    if(addAnnotation)
                        graphViewerClient.AddEdge(name + i, name + "[->]", (INode)value[i-1], (INode)value[i]);
                    else
                        graphViewerClient.DeleteEdge(name + i);
                }
            }
        }

        private void HighlightDeque(IDeque value, string name, bool addAnnotation)
        {
            int distanceToTop = 0;
            object prevElem = null;
            foreach(object elem in value)
            {
                if(elem is IGraphElement)
                    HighlightSingleValue(elem, name + "@" + distanceToTop, addAnnotation);
                if(elem is INode && distanceToTop >= 1)
                {
                    if(addAnnotation)
                        graphViewerClient.AddEdge(name + distanceToTop, name + "[->]", (INode)prevElem, (INode)elem);
                    else
                        graphViewerClient.DeleteEdge(name + distanceToTop);
                }
                prevElem = elem;
                ++distanceToTop;
            }
        }

        private void HighlightMapping(INode source, INode target, string name, int cnt, bool addAnnotation)
        {
            HighlightSingleValue(source, name + ".Domain", addAnnotation);
            HighlightSingleValue(target, name + ".Range", addAnnotation);
            if(addAnnotation)
                graphViewerClient.AddEdge(name + cnt, name, source, target);
            else
                graphViewerClient.DeleteEdge(name + cnt);
        }

        private void HighlightSingleValue(object value, string name, bool addAnnotation)
        {
            if(value is int)
            {
                List<int> allocatedVisitedFlags = debuggerProcEnv.ProcEnv.NamedGraph.GetAllocatedVisitedFlags();
                if(allocatedVisitedFlags.Contains((int)value))
                {
                    foreach(INode node in debuggerProcEnv.ProcEnv.NamedGraph.Nodes)
                    {
                        if(debuggerProcEnv.ProcEnv.NamedGraph.IsVisited(node, (int)value))
                            HighlightNode(node, "visited[" + name + "]", addAnnotation);
                    }
                    foreach(IEdge edge in debuggerProcEnv.ProcEnv.NamedGraph.Edges)
                    {
                        if(debuggerProcEnv.ProcEnv.NamedGraph.IsVisited(edge, (int)value))
                            HighlightEdge(edge, "visited[" + name + "]", addAnnotation);
                    }
                }
                else
                {
                    env.WriteLine("Unknown visited flag id " + (int)(value) + "!");
                    if(name!=null)
                        env.WriteLine("Which is contained in variable " + name + ".");
                    env.WriteLine("Use (v)ariables to print variables and visited flags.");
                }
            }
            else if(value is IGraphElement)
            {
                if(value is INode)
                    HighlightNode((INode)value, name, addAnnotation);
                else //value is IEdge
                    HighlightEdge((IEdge)value, name, addAnnotation);
            }
            else
            {
                env.WriteLine("The value " + value + (name!=null ? " contained in " + name : "") + " is neither an integer visited flag id nor a graph element, can't highlight!");
                env.WriteLine("Use (v)ariables to print variables and visited flags.");
            }
        }

        private void HighlightNode(INode node, string name, bool addAnnotation)
        {
            if(addAnnotation)
            {
                if(graphViewerClient.dumpInfo.IsExcludedGraph())
                    graphViewerClient.AddNodeEvenIfGraphExcluded(node);

                graphViewerClient.ChangeNode(node, realizers.MatchedNodeRealizer);
                renderRecorder.AddNodeAnnotation(node, name);
            }
            else
            {
                graphViewerClient.ChangeNode(node, null);
                graphViewerClient.AnnotateElement(node, null);
                renderRecorder.RemoveNodeAnnotation(node);
            }
        }

        private void HighlightEdge(IEdge edge, string name, bool addAnnotation)
        {
            if(addAnnotation)
            {
                if(graphViewerClient.dumpInfo.IsExcludedGraph())
                    graphViewerClient.AddEdgeEvenIfGraphExcluded(edge);

                graphViewerClient.ChangeEdge(edge, realizers.MatchedEdgeRealizer);
                renderRecorder.AddEdgeAnnotation(edge, name);
            }
            else
            {
                graphViewerClient.ChangeEdge(edge, null);
                graphViewerClient.AnnotateElement(edge, null);
                renderRecorder.RemoveEdgeAnnotation(edge);
            }
        }
    }
}
