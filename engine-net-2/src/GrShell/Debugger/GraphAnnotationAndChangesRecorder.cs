/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
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

        private IRulePattern curRulePattern = null;

        private int nextAddedNodeIndex = 0;
        private int nextAddedEdgeIndex = 0;

        private readonly List<String> curAddedNodeNames = new List<String>();
        private readonly List<String> curAddedEdgeNames = new List<String>();

        private readonly Dictionary<INode, bool> addedNodes = new Dictionary<INode, bool>();
        private readonly Dictionary<IEdge, bool> addedEdges = new Dictionary<IEdge, bool>();

        private readonly List<String> deletedNodes = new List<String>();
        private readonly List<String> deletedEdges = new List<String>();

        private readonly Dictionary<INode, bool> retypedNodes = new Dictionary<INode, bool>();
        private readonly Dictionary<IEdge, bool> retypedEdges = new Dictionary<IEdge, bool>();


        public void AddNodeAnnotation(INode node, String name)
        {
            if(annotatedNodes.ContainsKey(node))
                annotatedNodes[node] += ", " + name;
            else
                annotatedNodes[node] = name;
        }

        public void AddEdgeAnnotation(IEdge edge, String name)
        {
            if(annotatedEdges.ContainsKey(edge))
                annotatedEdges[edge] += ", " + name;
            else
                annotatedEdges[edge] = name;
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

        public void AnnotateGraphElements(YCompClient ycompClient)
        {
            foreach(KeyValuePair<INode, string> nodeToName in annotatedNodes)
                ycompClient.AnnotateElement(nodeToName.Key, nodeToName.Value);
            foreach(KeyValuePair<IEdge, string> edgeToName in annotatedEdges)
                ycompClient.AnnotateElement(edgeToName.Key, edgeToName.Value);
        }

        public void SetCurrentRule(IRulePattern curRulePattern)
        {
            this.curRulePattern = curRulePattern;
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
            return curAddedNodeNames[nextAddedNodeIndex++];
        }

        public String AddedEdge(IEdge edge)
        {
            addedEdges[edge] = true;
            return curAddedEdgeNames[nextAddedEdgeIndex++];
        }

        public void DeletedNode(String nodeName)
        {
            deletedNodes.Add(nodeName);
        }

        public void DeletedEdge(String edgeName)
        {
            deletedEdges.Add(edgeName);
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

        public void ApplyChanges(YCompClient ycompClient)
        {
            foreach(INode node in addedNodes.Keys)
            {
                ycompClient.ChangeNode(node, null);
                ycompClient.AnnotateElement(node, null);
            }
            foreach(IEdge edge in addedEdges.Keys)
            {
                ycompClient.ChangeEdge(edge, null);
                ycompClient.AnnotateElement(edge, null);
            }

            foreach(String edgeName in deletedEdges)
                ycompClient.DeleteEdge(edgeName);
            foreach(String nodeName in deletedNodes)
                ycompClient.DeleteNode(nodeName);

            foreach(INode node in retypedNodes.Keys)
                ycompClient.ChangeNode(node, null);
            foreach(IEdge edge in retypedEdges.Keys)
                ycompClient.ChangeEdge(edge, null);

            foreach(INode node in annotatedNodes.Keys)
                ycompClient.AnnotateElement(node, null);
            foreach(IEdge edge in annotatedEdges.Keys)
                ycompClient.AnnotateElement(edge, null);
        }
    }
}
