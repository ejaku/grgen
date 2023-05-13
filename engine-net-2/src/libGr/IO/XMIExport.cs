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
using System.Xml;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Exports graphs to the XMI format (assuming a matching ecore exists).
    /// </summary>
    public class XMIExport : IDisposable
    {
        readonly XmlTextWriter xmlwriter;
        
        public Dictionary<INode, Set<INode>> contains;
        public Dictionary<INode, INode> containedIn;
        public Dictionary<INode, IEdge> containedVia;

        protected XMIExport(XmlTextWriter writer) {
            xmlwriter = writer;
            xmlwriter.Formatting = Formatting.Indented;
            xmlwriter.Indentation = 1;
            xmlwriter.WriteStartDocument();
            contains = new Dictionary<INode, Set<INode>>();
            containedIn = new Dictionary<INode, INode>();
            containedVia = new Dictionary<INode, IEdge>();
        }

        protected XMIExport(TextWriter streamwriter)
            : this(new XmlTextWriter(streamwriter)) 
        {
        }

        protected XMIExport(String filename)
            : this(new StreamWriter(filename)) 
        {
        }

        /// <summary>
        /// Exports the given graph to a XMI file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export.</param>
        /// <param name="exportFilename">The filename for the exported file.</param>
        public static void Export(IGraph graph, String exportFilename)
        {
            using(XMIExport export = new XMIExport(exportFilename))
                export.Export(graph);
        }

        /// <summary>
        /// Exports the given graph in XMI format to the given text writer output stream.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export.</param>
        /// <param name="streamWriter">The stream writer to export to.</param>
        public static void Export(IGraph graph, TextWriter streamWriter)
        {
            using(XMIExport export = new XMIExport(streamWriter))
                export.Export(graph);
        }

        protected void Export(IGraph graph)
        {
            xmlwriter.WriteStartElement("xmi:XMI");
            xmlwriter.WriteAttributeString("xmi:version", "2.0");
            xmlwriter.WriteAttributeString("xmlns:xmi", "http://www.omg.org/XMI");
            xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            foreach(String package in graph.Model.Packages)
            {
                xmlwriter.WriteAttributeString("xmlns:" + RemoveGrGenPrefix(package), RemoveGrGenPrefix(package));
            }

            WriteGraph(graph);

            xmlwriter.WriteEndElement();
            xmlwriter.WriteEndDocument();
            xmlwriter.WriteRaw("\n");
            xmlwriter.Flush();
        }

        private void WriteGraph(IGraph graph)
        {
            // Compute the containment hierarchy
            foreach(EdgeType et in graph.Model.EdgeModel.Types)
            {
                if(!IsContainmentEdge(et))
                    continue;
                foreach(IEdge edge in graph.GetExactEdges(et))
                {
                    if(!contains.ContainsKey(edge.Source))
                        contains.Add(edge.Source, new Set<INode>());
                    contains[edge.Source].Add(edge.Target);
                    containedIn.Add(edge.Target, edge.Source); // an exception here means double containment
                    containedVia.Add(edge.Target, edge);
                }
            }

            // Dump the root nodes, following their containment (first level objects are unordered)
            foreach(INode node in graph.Nodes)
            {
                if(containedIn.ContainsKey(node))
                    continue;
                WriteRootNode(node);
            }
        }

        private static bool IsContainmentEdge(EdgeType et)
        {
            foreach(KeyValuePair<string, string> annotat in et.Annotations)
            {
                if(annotat.Key == "containment" && annotat.Value == "true")
                    return true;
            }
            return false;
        }

        protected void WriteRootNode(INode node)
        {
            xmlwriter.WriteStartElement(XmiFromGrGenTypeName(node.Type.PackagePrefixedName));
            
            xmlwriter.WriteAttributeString("xmi:id", node.GetHashCode().ToString());

            WriteNodeProperties(node);

            xmlwriter.WriteEndElement();
        }

        protected void WriteReachedNode(INode node)
        {
            xmlwriter.WriteStartElement(XmiReferenceFromGrGenEdge(containedVia[node].Type.Name, containedIn[node]));

            xmlwriter.WriteAttributeString("xsi:type", XmiFromGrGenTypeName(node.Type.PackagePrefixedName));
            xmlwriter.WriteAttributeString("xmi:id", node.GetHashCode().ToString());

            WriteNodeProperties(node);

            xmlwriter.WriteEndElement();
        }

        protected void WriteNodeProperties(INode node)
        {
            WriteAttributes(node);

            // first write non-containment references
            foreach(IEdge edge in node.Outgoing)
            {
                if(contains.ContainsKey(node)
                    && contains[node].Contains(edge.Target))
                {
                    continue;
                }
                xmlwriter.WriteAttributeString(XmiReferenceFromGrGenEdge(edge.Type.Name, node), edge.Target.GetHashCode().ToString());
            }

            // then write containment references, follow them
            if(contains.ContainsKey(node))
            {
                List<INode> children = new List<INode>(contains[node]);
                children.Sort(new OrderingComparer(this));
                foreach(INode child in children)
                {
                    WriteReachedNode(child);
                }
            }
        }

        protected void WriteAttributes(IGraphElement elem)
        {
            foreach(AttributeType attrType in elem.Type.AttributeTypes)
            {
                object value = elem.GetAttribute(attrType.Name);
                String valuestr = (value == null) ? "" : value.ToString();
                switch(attrType.Kind)
                {
                case AttributeKind.BooleanAttr:
                case AttributeKind.DoubleAttr:
                case AttributeKind.FloatAttr:
                case AttributeKind.ByteAttr:
                case AttributeKind.ShortAttr:
                case AttributeKind.IntegerAttr:
                case AttributeKind.LongAttr:
                case AttributeKind.StringAttr:
                    xmlwriter.WriteAttributeString(RemoveGrGenPrefix(attrType.Name), valuestr);
                    break;
                case AttributeKind.EnumAttr:
                    xmlwriter.WriteAttributeString(RemoveGrGenPrefix(attrType.Name), RemoveGrGenPrefix(valuestr));
                    break;
                default:
                    throw new Exception("Unsupported attribute value type: \"" + attrType.Kind + "\"");
                }
            }
        }

        private string XmiFromGrGenTypeName(string grgenTypeName)
        {
            String packageName = grgenTypeName.Remove(grgenTypeName.IndexOf(':'));
            String typeName = grgenTypeName.Substring(grgenTypeName.IndexOf(':') + 1 + 1);
            return RemoveGrGenPrefix(packageName) + ":" + RemoveGrGenPrefix(typeName);
        }

        private string XmiReferenceFromGrGenEdge(string edgeTypeName, INode sourceNode)
        {
            string nodeTypeName = null;
            foreach(NodeType nodeType in sourceNode.Type.SuperOrSameTypes)
            {
                if(edgeTypeName.StartsWith(nodeType.Name))
                {
                    if(nodeTypeName == null)
                        nodeTypeName = nodeType.Name;
                    else
                        nodeTypeName = nodeType.Name.Length > nodeTypeName.Length ? nodeType.Name : nodeTypeName;
                }
            }
            return edgeTypeName.Substring(nodeTypeName.Length + 1);
        }

        private string RemoveGrGenPrefix(string grgenName)
        {
            if(grgenName.Length > 0 && grgenName[0] == '_')
                return grgenName.Substring(1);
            else
                return grgenName;
        }

        public void Dispose()
        {
            if(xmlwriter != null)
                xmlwriter.Close();
        }
    }

    public class OrderingComparer : IComparer<INode>
    {
        readonly XMIExport exporter;

        public OrderingComparer(XMIExport exporter)
        {
            this.exporter = exporter;
        }

        public int Compare(INode this_, INode that)
        {
            IEdge thisParentalEdge = exporter.containedVia[this_];
            IEdge thatParentalEdge = exporter.containedVia[that];
            int typeNameComparison = thisParentalEdge.Type.Name.CompareTo(thatParentalEdge.Type.Name);
            if(typeNameComparison != 0)
                return typeNameComparison;
            else
            {
                try
                {
                    int thisOrdering = (int)thisParentalEdge.GetAttribute("ordering");
                    int thatOrdering = (int)thatParentalEdge.GetAttribute("ordering");
                    return thisOrdering.CompareTo(thatOrdering);
                }
                catch(Exception)
                {
                    return this_.GetHashCode().CompareTo(that.GetHashCode());
                }
            }
        }
    }
}
