/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Collections;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the INamedGraph interface.
    /// </summary>
    public class LGSPNamedGraph : LGSPGraph, INamedGraph
    {
        internal Dictionary<String, IGraphElement> NameToElem = new Dictionary<String, IGraphElement>();
        internal Dictionary<IGraphElement, String> ElemToName = new Dictionary<IGraphElement, String>();

        private static IDictionary<IGraphElement, IGraphElement> tmpOldToNewMap; // workaround to hide map parameter passing in copy constructor

        private int nextID = 0;

        private String GetNextName()
        {
            String nameCandidate;
            do
            {
                nameCandidate = String.Format("${0,00000000:X}", nextID++);
            }
            while(NameToElem.ContainsKey(nameCandidate));
            return nameCandidate;
        }


        /// <summary>
        /// Constructs an LGSPNamedGraph object with the given model and an automatically generated name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        public LGSPNamedGraph(IGraphModel grmodel)
            : base(grmodel)
        {
        }

        /// <summary>
        /// Constructs an LGSPNamedGraph object with the given model and name.
        /// </summary>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="grname">The name for the graph.</param>
        public LGSPNamedGraph(IGraphModel grmodel, String grname)
            : base(grmodel, grname)
        {
        }

        /// <summary>
        /// Constructs an LGSPNamedGraph object.
        /// Deprecated.
        /// </summary>
        /// <param name="lgspBackend">The responsible backend object.</param>
        /// <param name="grmodel">The graph model.</param>
        /// <param name="grname">The name for the graph.</param>
        /// <param name="modelassemblyname">The name of the model assembly.</param>
        public LGSPNamedGraph(LGSPBackend lgspBackend, IGraphModel grmodel, String grname, String modelassemblyname)
            : base(lgspBackend, grmodel, grname, modelassemblyname)
        {
        }

        /// <summary>
        /// Constructs an LGSPNameGraph object without initializing it.
        /// </summary>
        /// <param name="grname">The name for the graph.</param>
        protected LGSPNamedGraph(String grname)
            : base(grname)
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="dataSource">The LGSPNamedGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning.</param>
        public LGSPNamedGraph(LGSPNamedGraph dataSource, String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
            : base(dataSource, newName, out oldToNewMap)
        {
            CopyNames(dataSource, oldToNewMap);
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="dataSource">The LGSPNamedGraph object to get the data from</param>
        /// <param name="newName">Name of the copied graph.</param>
        public LGSPNamedGraph(LGSPNamedGraph dataSource, String newName)
            : base(dataSource, newName, out tmpOldToNewMap)
        {
            CopyNames(dataSource, tmpOldToNewMap);
            tmpOldToNewMap = null;
        }

        /// <summary>
        /// Copy constructor helper.
        /// </summary>
        /// <param name="dataSource">The LGSPNamedGraph object to get the data from</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning.</param>
        private void CopyNames(LGSPNamedGraph dataSource, IDictionary<IGraphElement, IGraphElement> oldToNewMap)
        {
            foreach(INode node in dataSource.Nodes)
            {
                NameToElem[dataSource.GetElementName(node)] = oldToNewMap[node];
                ElemToName[oldToNewMap[node]] = dataSource.GetElementName(node);
            }

            foreach(IEdge edge in dataSource.Edges)
            {
                NameToElem[dataSource.GetElementName(edge)] = oldToNewMap[edge];
                ElemToName[oldToNewMap[edge]] = dataSource.GetElementName(edge);
            }

            /* TODO: remove when cloning of graph variables was implemented
             * foreach(KeyValuePair<IGraphElement, LinkedList<Variable>> kvp in dataSource.ElementMap)
            {
                IGraphElement newElem = oldToNewMap[kvp.Key];
                foreach(Variable var in kvp.Value)
                    SetVariableValue(var.Name, newElem);
            }*/
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with anonymous names in the form "$" + GetNextName()
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning</param>
        public LGSPNamedGraph(LGSPGraph graph, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
            : base(graph, graph.Name, out oldToNewMap)
        {
            DoName();
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with anonymous names in the form "$" + GetNextName()
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        public LGSPNamedGraph(LGSPGraph graph)
            : base(graph, graph.Name, out tmpOldToNewMap)
        {
            tmpOldToNewMap = null;
            DoName();
        }

        /// <summary>
        /// Initializes the name maps with anonymous names in the form "$" + GetNextName()
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        public void DoName()
        {
            foreach(INode node in Nodes)
            {
                String name = GetNextName();
                NameToElem[name] = node;
                ElemToName[node] = name;
            }

            foreach(IEdge edge in Edges)
            {
                String name = GetNextName();
                NameToElem[name] = edge;
                ElemToName[edge] = name;
            }
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with the names provided in a given attribute each graph element must have
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        /// <param name="nameAttributeName">The name of the attribute to be used for naming</param>
        /// <param name="oldToNewMap">A map of the old elements to the new elements after cloning</param>
        public LGSPNamedGraph(LGSPGraph graph, String nameAttributeName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap)
            : base(graph, graph.Name, out oldToNewMap)
        {
            DoName(nameAttributeName);
        }

        /// <summary>
        /// Copy and extend constructor, creates a named graph from a normal graph.
        /// Initializes the name maps with the names provided in a given attribute each graph element must have
        /// </summary>
        /// <param name="graph">The graph to be used named</param>
        /// <param name="nameAttributeName">The name of the attribute to be used for naming</param>
        public LGSPNamedGraph(LGSPGraph graph, String nameAttributeName)
            : base(graph, graph.Name, out tmpOldToNewMap)
        {
            tmpOldToNewMap = null;
            DoName(nameAttributeName);
        }

        /// <summary>
        /// </summary>
        /// <param name="nameAttributeName">The name of the attribute to be used for naming</param>
        public void DoName(String nameAttributeName)
        {
            foreach(INode node in Nodes)
            {
                AttributeType attrType = node.Type.GetAttributeType(nameAttributeName);
                if(attrType == null)
                    throw new ArgumentException(String.Format(
                        "Illegal name attribute for node of type {0}!", node.Type.Name));
                if(attrType.Kind != AttributeKind.StringAttr)
                    throw new ArgumentException("Name attribute is not a string attribute in type {0}!", node.Type.Name);

                String name = (String) node.GetAttribute(nameAttributeName);
                if(NameToElem.ContainsKey(name))
                    throw new ArgumentException(String.Format(
                        "The name attributes do not contain unique names (\"{0}\" found twice)!",name));
                NameToElem[name] = node;
                ElemToName[node] = name;
            }

            foreach(IEdge edge in Edges)
            {
                AttributeType attrType = edge.Type.GetAttributeType(nameAttributeName);
                if(attrType == null)
                    throw new ArgumentException(String.Format(
                        "Illegal name attribute for edge of type {0}!", edge.Type.Name));
                if(attrType.Kind != AttributeKind.StringAttr)
                    throw new ArgumentException("Name attribute is not a string attribute in edge type {0}!",
                        edge.Type.Name);

                String name = (String) edge.GetAttribute(nameAttributeName);
                if(NameToElem.ContainsKey(name))
                    throw new ArgumentException(String.Format(
                        "The name attributes do not contain unique names (\"{0}\" found twice)!", name));
                NameToElem[name] = edge;
                ElemToName[edge] = name;
            }
        }

        // todo: remove
        protected void InitializeVariables()
        {
            // Reset variables
            //ElementMap.Clear();
            //VariableMap.Clear();
        }


        public void SetElementName(IGraphElement elem, String name)
        {
            if(DifferentElementWithName(elem, name))
                throw new Exception("The name \"" + name + "\" is already in use!");
            String oldName;
            if(ElemToName.TryGetValue(elem, out oldName))
                NameToElem.Remove(oldName);
            NameToElem[name] = elem;
            ElemToName[elem] = name;
        }

        public void SetElementPrefixName(IGraphElement element, String prefix)
        {
            Console.WriteLine("Set node prefix name {0}, {1}", element, prefix);
            String name = prefix;
            int curr = 0;
            while (DifferentElementWithName(element, name))
            {
                ++curr;
                name = prefix + curr;
            }
            SetElementName(element, name);
        }

        /// <summary>
        /// returns whether another element than the one given already bears the name
        /// </summary>
        protected bool DifferentElementWithName(IGraphElement elem, String name)
        {
            return (NameToElem.ContainsKey(name)) && (NameToElem[name] != elem);
        }

        public String GetElementName(IGraphElement elem)
        {
            String name;
            if(!ElemToName.TryGetValue(elem, out name))
            {
                // element has been generated within a rule execution, so give it a name
                name = GetNextName();
                NameToElem[name] = elem;
                ElemToName[elem] = name;
                //                throw new Exception((elem is INode ? "Node" : "Edge") + " of type \"" + elem.Type.Name + "\" has no name!");
            }
            return name;
        }

        // TODO: What happens, if a named element has been removed by a rule and is asked for with GetGraphElement??

        public IGraphElement GetGraphElement(String name)
        {
            IGraphElement elem;
            if(!NameToElem.TryGetValue(name, out elem)) return null;
            return elem;
        }

        /// <summary>
        /// Tries to set the name of an element.
        /// If the name is already used by another element, the element is removed from the graph and
        /// an ArgumentException is thrown.
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="name"></param>
        private void AddElemName(IGraphElement elem, String name)
        {
            if(NameToElem.ContainsKey(name))
            {
                if(elem is INode)
                    Remove((INode) elem);
                else
                    Remove((IEdge) elem);
                throw new ArgumentException(String.Format("The name \"{0}\" is already used!", name));
            }
            NameToElem[name] = elem;
            ElemToName[elem] = name;
        }


        public void AddNode(INode node, String elemName)
        {
            AddNode((LGSPNode)node, elemName);
        }

        public void AddNode(LGSPNode node, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            AddNodeWithoutEvents(node, node.lgspType.TypeID);

            NameToElem[elemName] = node;
            ElemToName[node] = elemName;

            NodeAdded(node);
        }

        public INode AddNode(NodeType nodeType, String elemName)
        {
            return AddLGSPNode(nodeType, elemName);
        }

        public LGSPNode AddLGSPNode(NodeType nodeType, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            //            LGSPNode node = new LGSPNode(nodeType);
            LGSPNode node = (LGSPNode)nodeType.CreateNode();
            AddNodeWithoutEvents(node, nodeType.TypeID);

            NameToElem[elemName] = node;
            ElemToName[node] = elemName;

            NodeAdded(node);

            return node;
        }

        public override void AddNode(INode node)
        {
            AddNode(node, null);
        }

        public override void AddNode(LGSPNode node)
        {
            AddNode(node, null);
        }

        public override INode AddNode(NodeType nodeType)
        {
            return AddNode(nodeType, null);
        }

        public override LGSPNode AddLGSPNode(NodeType nodeType)
        {
            return AddLGSPNode(nodeType, null);
        }

        public void AddEdge(IEdge edge, String elemName)
        {
            AddEdge((LGSPEdge)edge, elemName);
        }

        public void AddEdge(LGSPEdge edge, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            AddEdgeWithoutEvents(edge, edge.lgspType.TypeID);

            NameToElem[elemName] = edge;
            ElemToName[edge] = elemName;

            EdgeAdded(edge);
        }

        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, String elemName)
        {
            return AddEdge(edgeType, (LGSPNode)source, (LGSPNode)target, elemName);
        }

        public LGSPEdge AddEdge(EdgeType edgeType, LGSPNode source, LGSPNode target, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            //            LGSPEdge edge = new LGSPEdge(edgeType, source, target);
            LGSPEdge edge = (LGSPEdge)edgeType.CreateEdge(source, target);
            AddEdgeWithoutEvents(edge, edgeType.TypeID);

            NameToElem[elemName] = edge;
            ElemToName[edge] = elemName;

            EdgeAdded(edge);

            return edge;
        }

        public override void AddEdge(IEdge edge)
        {
            AddEdge(edge, null);
        }

        public override void AddEdge(LGSPEdge edge)
        {
            AddEdge(edge, null);
        }

        public override IEdge AddEdge(EdgeType edgeType, INode source, INode target)
        {
            return AddEdge(edgeType, source, target, null);
        }

        public override LGSPEdge AddEdge(EdgeType edgeType, LGSPNode source, LGSPNode target)
        {
            return AddEdge(edgeType, source, target, null);
        }

        private void RemoveName(IGraphElement elem)
        {
            String name;
            if(ElemToName.TryGetValue(elem, out name))  // has a name been assigned to the element?
            {
                ElemToName.Remove(elem);
                NameToElem.Remove(name);
            }
        }

        public override void Remove(INode node)
        {
            base.Remove(node);
            RemoveName(node);
        }

        public override void Remove(IEdge edge)
        {
            base.Remove(edge);
            RemoveName(edge);
        }

        public override void Clear()
        {
            base.Clear();
            ElemToName.Clear();
            NameToElem.Clear();
        }

        public override LGSPNode Retype(LGSPNode node, NodeType newNodeType)
        {
            String name;
            if(ElemToName.TryGetValue(node, out name)) // give new node the name of the old node in case it was named
            {
                LGSPNode newNode = (LGSPNode)newNodeType.CreateNodeWithCopyCommons(node);
                ElemToName[newNode] = name;
                RetypingNode(node, newNode);
                ReplaceNode(node, newNode);
                ElemToName.Remove(node);
                NameToElem[name] = newNode;
                return newNode;
            }
            else
                return base.Retype(node, newNodeType);
        }

        public override LGSPEdge Retype(LGSPEdge edge, EdgeType newEdgeType)
        {
            String name;
            if(ElemToName.TryGetValue(edge, out name)) // give new edge the name of the old edge in case it was named
            {
                LGSPEdge newEdge = (LGSPEdge)newEdgeType.CreateEdgeWithCopyCommons(edge.lgspSource, edge.lgspTarget, edge);
                ElemToName[newEdge] = name;
                RetypingEdge(edge, newEdge);
                ReplaceEdge(edge, newEdge);
                ElemToName.Remove(edge);
                NameToElem[name] = newEdge;
                return newEdge;
            }
            else
                return base.Retype(edge, newEdgeType);
        }

        public override IGraph Clone(String newName)
        {
            return CloneNamed(newName);
        }

        public override IGraph CreateEmptyEquivalent(String newName)
        {
            return new LGSPNamedGraph(this.model, newName);
        }

        public INamedGraph CloneNamed(String newName)
        {
            IDictionary<IGraphElement, IGraphElement> oldToNewMap;
            return new LGSPNamedGraph(this, newName, out oldToNewMap);
        }
    }
}
