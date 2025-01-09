/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// The GraphAnnotationAndChangesRecorder remembers annotations of graph elements and changes applied to the graph by an action (match and rewrite),
    /// it is used to mark the elements in the graph during rendering.
    /// Not to be mistaken with the Recorder/IRecorder that is used to serialize changes that occur to the graph to a file.
    /// </summary>
    public class GraphAnnotationAndChangesRecorder
    {
        private readonly Dictionary<INode, String> annotatedNodes = new Dictionary<INode, String>();
        private readonly Dictionary<IEdge, String> annotatedEdges = new Dictionary<IEdge, String>();

        private String curRulePatternName = null; // for node/edge addition
        private String curRulePatternNameForMatchAnnotation = null; // for pre matched event

        private int nextAddedNodeIndex = 0;
        private int nextAddedEdgeIndex = 0;

        private readonly List<String> curAddedNodeNames = new List<String>();
        private readonly List<String> curAddedEdgeNames = new List<String>();

        private readonly Dictionary<INode, bool> addedNodes = new Dictionary<INode, bool>();
        private readonly Dictionary<IEdge, bool> addedEdges = new Dictionary<IEdge, bool>();

        private readonly List<KeyValuePair<String, String>> deletedNodes = new List<KeyValuePair<String, String>>();
        private readonly List<KeyValuePair<String, String>> deletedEdges = new List<KeyValuePair<String, String>>();

        private readonly Dictionary<INode, bool> retypedNodes = new Dictionary<INode, bool>();
        private readonly Dictionary<IEdge, bool> retypedEdges = new Dictionary<IEdge, bool>();


        public void AddNodeAnnotation(INode node, String name)
        {
            String rulePrefix = curRulePatternNameForMatchAnnotation != null ? curRulePatternNameForMatchAnnotation + "." : "";
            if(annotatedNodes.ContainsKey(node))
                annotatedNodes[node] += ", " + rulePrefix + name;
            else
                annotatedNodes[node] = rulePrefix + name;
        }

        public void AddEdgeAnnotation(IEdge edge, String name)
        {
            String rulePrefix = curRulePatternNameForMatchAnnotation != null ? curRulePatternNameForMatchAnnotation + "." : "";
            if(annotatedEdges.ContainsKey(edge))
                annotatedEdges[edge] += ", " + rulePrefix + name;
            else
                annotatedEdges[edge] = rulePrefix + name;
        }

        public void RemoveNodeAnnotation(INode node)
        {
            annotatedNodes.Remove(node);
        }

        public void RemoveEdgeAnnotation(IEdge edge)
        {
            annotatedEdges.Remove(edge);
        }

        public void RemoveAllAnnotations()
        {
            annotatedNodes.Clear();
            annotatedEdges.Clear();
        }

        public void AnnotateGraphElements(GraphViewerClient graphViewerClient)
        {
            foreach(KeyValuePair<INode, string> nodeToName in annotatedNodes)
            {
                graphViewerClient.AnnotateElement(nodeToName.Key, nodeToName.Value);
            }
            foreach(KeyValuePair<IEdge, string> edgeToName in annotatedEdges)
            {
                graphViewerClient.AnnotateElement(edgeToName.Key, edgeToName.Value);
            }
        }

        public void SetCurrentRuleName(String curRulePatternName)
        {
            this.curRulePatternName = curRulePatternName;
        }

        public void SetCurrentRuleNameForMatchAnnotation(String curRulePatternName)
        {
            this.curRulePatternNameForMatchAnnotation = curRulePatternName;
        }

        public void SetAddedNodeNames(string[] namesOfNodesAdded)
        {
            curAddedNodeNames.Clear();
            curAddedNodeNames.AddRange(namesOfNodesAdded);
            nextAddedNodeIndex = 0;
        }

        public void SetAddedEdgeNames(string[] namesOfEdgesAdded)
        {
            curAddedEdgeNames.Clear();
            curAddedEdgeNames.AddRange(namesOfEdgesAdded);
            nextAddedEdgeIndex = 0;
        }

        // a match / another of the matches is to be rewritten
        public void ResetAddedNames()
        {
            nextAddedNodeIndex = 0;
            nextAddedEdgeIndex = 0;
        }

        public String AddedNode(INode node)
        {
            addedNodes[node] = true;
            if(nextAddedNodeIndex < curAddedNodeNames.Count)
                return curAddedNodeNames[nextAddedNodeIndex++];
            else if(curRulePatternName != null)
                return "added by " + curRulePatternName;
            else
                return "newly added";
        }

        public String AddedEdge(IEdge edge)
        {
            addedEdges[edge] = true;
            if(nextAddedEdgeIndex < curAddedEdgeNames.Count)
                return curAddedEdgeNames[nextAddedEdgeIndex++];
            else if(curRulePatternName != null)
                return "added by " + curRulePatternName;
            else
                return "newly added";
        }

        public void DeletedNode(String nodeName, String oldNodeName)
        {
            deletedNodes.Add(new KeyValuePair<String, String>(nodeName, oldNodeName)); // special handling for the zombie_ nodes -- GUI TODO: needed at all?
        }

        public void DeletedEdge(String edgeName, String oldEdgeName)
        {
            deletedEdges.Add(new KeyValuePair<String, String>(edgeName, oldEdgeName)); // special handling for the zombie_ edges -- GUI TODO: needed at all?
        }

        public bool WasNodeAnnotationReplaced(INode oldNode, INode newNode, out String name)
        {
            if(annotatedNodes.TryGetValue(oldNode, out name))
            {
                annotatedNodes.Remove(oldNode);
                annotatedNodes[newNode] = name;
                return true;
            }
            return false;
        }

        public bool WasEdgeAnnotationReplaced(IEdge oldEdge, IEdge newEdge, out String name)
        {
            if(annotatedEdges.TryGetValue(oldEdge, out name))
            {
                annotatedEdges.Remove(oldEdge);
                annotatedEdges[newEdge] = name;
                return true;
            }
            return false;
        }

        public void RetypedNode(INode node)
        {
            retypedNodes[node] = true;
        }

        public void RetypedEdge(IEdge edge)
        {
            retypedEdges[edge] = true;
        }

        public void ResetAllChangedElements()
        {
            addedNodes.Clear();
            addedEdges.Clear();
            retypedNodes.Clear();
            retypedEdges.Clear();
            deletedEdges.Clear();
            deletedNodes.Clear();
        }

        // applies changes recorded so far, leaving a graph without visible changes behind (e.g. no annotations)
        public void ApplyChanges(GraphViewerClient graphViewerClient)
        {
            foreach(INode node in addedNodes.Keys)
            {
                graphViewerClient.ChangeNode(node, null);
                graphViewerClient.AnnotateElement(node, null);
            }
            foreach(IEdge edge in addedEdges.Keys)
            {
                graphViewerClient.ChangeEdge(edge, null);
                graphViewerClient.AnnotateElement(edge, null);
            }

            foreach(KeyValuePair<String, String> edgeNames in deletedEdges)
            {
                graphViewerClient.DeleteEdge(edgeNames.Key, edgeNames.Value);
            }
            foreach(KeyValuePair<String, String> nodeNames in deletedNodes)
            {
                graphViewerClient.DeleteNode(nodeNames.Key, nodeNames.Value);
            }

            foreach(INode node in retypedNodes.Keys)
            {
                graphViewerClient.ChangeNode(node, null);
            }
            foreach(IEdge edge in retypedEdges.Keys)
            {
                graphViewerClient.ChangeEdge(edge, null);
            }

            foreach(INode node in annotatedNodes.Keys)
            {
                graphViewerClient.AnnotateElement(node, null);
            }
            foreach(IEdge edge in annotatedEdges.Keys)
            {
                graphViewerClient.AnnotateElement(edge, null);
            }
        }
    }
}
