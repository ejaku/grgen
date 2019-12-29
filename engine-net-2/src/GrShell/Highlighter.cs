/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    class Highlighter
    {
        IGrShellImplForDebugger grShellImpl;
        ShellGraphProcessingEnvironment shellProcEnv;

        ElementRealizers realizers;
        GraphAnnotationAndChangesRecorder renderRecorder;
        YCompClient ycompClient;

        Stack<Sequence> debugSequences;

        public Highlighter(IGrShellImplForDebugger grShellImpl,
            ShellGraphProcessingEnvironment shellProcEnv,
            ElementRealizers realizers,
            GraphAnnotationAndChangesRecorder renderRecorder,
            YCompClient ycompClient,
            Stack<Sequence> debugSequences
        )
        {
            this.grShellImpl = grShellImpl;
            this.shellProcEnv = shellProcEnv;
            this.realizers = realizers;
            this.renderRecorder = renderRecorder;
            this.ycompClient = ycompClient;
            this.debugSequences = debugSequences;
        }

        public void ComputeHighlight(Sequence seq, String str, out List<object> values, out List<string> annotations)
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

        private void ComputeHighlightArgument(Sequence seq, string argument, string annotation, List<object> sources, List<string> annotations)
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
            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            (debugSequences.Peek()).GetLocalVariables(seqVars, containerConstructors, seq);
            foreach(SequenceVariable var in seqVars.Keys)
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
            foreach(Variable var in shellProcEnv.ProcEnv.Variables)
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
            Console.WriteLine("Unknown variable " + argument + "!");
            Console.WriteLine("Use (v)ariables to print variables and visited flags.");
        }

        public void DoHighlight(List<object> sources, List<string> annotations)
        {
            if(ycompClient.dumpInfo.IsExcludedGraph())
            {
                ycompClient.ClearGraph();
            }

            for(int i = 0; i < sources.Count; ++i)
            {
                HighlightValue(sources[i], annotations[i], true);
            }

            if(ycompClient.dumpInfo.IsExcludedGraph())
            {
                // highlight values added in highlight value calls to excludedGraphNodesIncluded
                ycompClient.AddNeighboursAndParentsOfNeededGraphElements();
            }

            renderRecorder.AnnotateGraphElements(ycompClient);

            ycompClient.UpdateDisplay();
            ycompClient.Sync();
            Console.WriteLine("Press any key to continue...");
            grShellImpl.ReadKeyWithCancel();

            for(int i = 0; i < sources.Count; ++i)
            {
                HighlightValue(sources[i], annotations[i], false);
            }

            ycompClient.UpdateDisplay();
            ycompClient.Sync();

            Console.WriteLine("End of highlighting");
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
            if(valueType == typeof(de.unika.ipd.grGen.libGr.SetValueType))
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
                            HighlightSingleValue(entry.Key, name + ".Domain -> " + EmitHelper.ToString(entry.Value, shellProcEnv.ProcEnv.NamedGraph), addAnnotation);
                        if(entry.Value is IGraphElement)
                            HighlightSingleValue(entry.Value, EmitHelper.ToString(entry.Key, shellProcEnv.ProcEnv.NamedGraph) + " -> " + name + ".Range", addAnnotation);
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
                        ycompClient.AddEdge(name + i, name + "[->]", (INode)value[i-1], (INode)value[i]);
                    else
                        ycompClient.DeleteEdge(name + i);
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
                        ycompClient.AddEdge(name + distanceToTop, name + "[->]", (INode)prevElem, (INode)elem);
                    else
                        ycompClient.DeleteEdge(name + distanceToTop);
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
                ycompClient.AddEdge(name + cnt, name, source, target);
            else
                ycompClient.DeleteEdge(name + cnt);
        }

        private void HighlightSingleValue(object value, string name, bool addAnnotation)
        {
            if(value is int)
            {
                List<int> allocatedVisitedFlags = shellProcEnv.ProcEnv.NamedGraph.GetAllocatedVisitedFlags();
                if(allocatedVisitedFlags.Contains((int)value))
                {
                    foreach(INode node in shellProcEnv.ProcEnv.NamedGraph.Nodes)
                        if(shellProcEnv.ProcEnv.NamedGraph.IsVisited(node, (int)value))
                            HighlightNode(node, "visited[" + name + "]", addAnnotation);
                    foreach(IEdge edge in shellProcEnv.ProcEnv.NamedGraph.Edges)
                        if(shellProcEnv.ProcEnv.NamedGraph.IsVisited(edge, (int)value))
                            HighlightEdge(edge, "visited[" + name + "]", addAnnotation);
                }
                else
                {
                    Console.WriteLine("Unknown visited flag id " + (int)(value) + "!");
                    if(name!=null)
                        Console.WriteLine("Which is contained in variable " + name + ".");
                    Console.WriteLine("Use (v)ariables to print variables and visited flags.");
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
                Console.WriteLine("The value " + value + (name!=null ? " contained in " + name : "") + " is neither an integer visited flag id nor a graph element, can't highlight!");
                Console.WriteLine("Use (v)ariables to print variables and visited flags.");
            }
        }

        private void HighlightNode(INode node, string name, bool addAnnotation)
        {
            if(addAnnotation)
            {
                if(ycompClient.dumpInfo.IsExcludedGraph())
                    ycompClient.AddNodeEvenIfGraphExcluded(node);

                ycompClient.ChangeNode(node, realizers.MatchedNodeRealizer);
                renderRecorder.AddNodeAnnotation(node, name);
            }
            else
            {
                ycompClient.ChangeNode(node, null);
                ycompClient.AnnotateElement(node, null);
                renderRecorder.RemoveNodeAnnotation(node);
            }
        }

        private void HighlightEdge(IEdge edge, string name, bool addAnnotation)
        {
            if(addAnnotation)
            {
                if(ycompClient.dumpInfo.IsExcludedGraph())
                    ycompClient.AddEdgeEvenIfGraphExcluded(edge);

                ycompClient.ChangeEdge(edge, realizers.MatchedEdgeRealizer);
                renderRecorder.AddEdgeAnnotation(edge, name);
            }
            else
            {
                ycompClient.ChangeEdge(edge, null);
                ycompClient.AnnotateElement(edge, null);
                renderRecorder.RemoveEdgeAnnotation(edge);
            }
        }
    }
}
