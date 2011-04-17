/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// author: Moritz Kroll, Nicholas Tung

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Exports graphs to the GXL format.
    /// </summary>
    public class GXLExport : IDisposable
    {
        XmlTextWriter xmlwriter;

        protected GXLExport(XmlTextWriter writer) {
            xmlwriter = writer;
            xmlwriter.Formatting = Formatting.Indented;
            xmlwriter.Indentation = 1;
            xmlwriter.WriteStartDocument();
            xmlwriter.WriteDocType("gxl", null, "http://www.gupro.de/GXL/gxl-1.0.dtd", null);
        }

        protected GXLExport(TextWriter streamwriter)
            : this(new XmlTextWriter(streamwriter)) 
        {
        }

        protected GXLExport(String filename)
            : this(new StreamWriter(filename)) 
        {
        }

        /// <summary>
        /// Exports the given graph to a GXL file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export.</param>
        /// <param name="exportFilename">The filename for the exported file.</param>
        public static void Export(IGraph graph, String exportFilename)
        {
            using(GXLExport export = new GXLExport(exportFilename))
                export.Export(graph);
        }

        /// <summary>
        /// Exports the given graph in GXL format to the given text writer output stream.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="graph">The graph to export.</param>
        /// <param name="streamWriter">The stream writer to export to.</param>
        public static void Export(IGraph graph, TextWriter streamWriter)
        {
            using(GXLExport export = new GXLExport(streamWriter))
                export.Export(graph);
        }

        protected void Export(IGraph graph)
        {
            xmlwriter.WriteStartElement("gxl");
            xmlwriter.WriteAttributeString("xmlns:xlink", "http://www.w3.org/1999/xlink");
            WriteModel(graph);
            WriteGraph(graph);
            xmlwriter.WriteEndElement();
            xmlwriter.WriteEndDocument();
            xmlwriter.WriteRaw("\n");
            xmlwriter.Flush();
        }

        protected String GetModelID(IGraph graph)
        {
            String id = graph.Model.ModelName;
            if(id.EndsWith("__gxl"))
                id = id.Substring(0, id.Length - 5);
            return id;
        }

        /// <summary>
        /// Returns the domain ID for the given attribute type.
        /// </summary>
        /// <param name="kind">The attribute kind</param>
        /// <param name="enumAttrType">For enums, enumAttrType must be valid. Otherwise it may be null.</param>
        /// <returns></returns>
        protected String GetDomainID(AttributeKind kind, EnumAttributeType enumAttrType)
        {
            switch(kind)
            {
                case AttributeKind.BooleanAttr: return "DM_bool";
                case AttributeKind.DoubleAttr: return "DM_double";
                case AttributeKind.FloatAttr: return "DM_float";
                case AttributeKind.IntegerAttr: return "DM_int";
                case AttributeKind.StringAttr: return "DM_string";
                case AttributeKind.EnumAttr: return "DM_enum_" + enumAttrType.Name;
                default:
                    throw new Exception("Unsupported attribute value type: \"" + kind + "\"");
            }
        }

        protected String GetDomainID(AttributeKind kind)
        {
            return GetDomainID(kind, null);
        }

        protected String GetDomainID(AttributeType attrType)
        {
            return GetDomainID(attrType.Kind, attrType.EnumType);
        }

        protected String GetDomainID(EnumAttributeType enumAttrType)
        {
            return GetDomainID(AttributeKind.EnumAttr, enumAttrType);
        }

        protected String GetAttrTypeID(AttributeType attrtype)
        {
            return (attrtype.OwnerType.IsNodeType ? "NAT_" : "EAT_")
                + attrtype.OwnerType.Name + "_" + attrtype.Name;
        }

        protected String GetElemTypeID(GrGenType type)
        {
            return type.Name;
        }

        protected struct Attr
        {
            public String Name;
            public String Type;
            public String Value;

            public Attr(String name, String type, String value)
            {
                Name = name;
                Type = type;
                Value = value;
            }
        }

        protected void WriteTypeElement(string typestr)
        {
            xmlwriter.WriteStartElement("type");
            xmlwriter.WriteAttributeString("xlink:href", typestr);
            xmlwriter.WriteEndElement();
        }

        protected void WriteAttrs(Attr[] attrs)
        {
            foreach(Attr attr in attrs) {
                xmlwriter.WriteStartElement("attr");
                xmlwriter.WriteAttributeString("name", attr.Name);
                xmlwriter.WriteElementString(attr.Type, attr.Value);
                xmlwriter.WriteEndElement();
            }
        }

        protected void WriteEnumType(EnumAttributeType enumType)
        {
            String enumid = GetDomainID(enumType);
            WriteGXLNode(enumid, "Enum");

            foreach(EnumMember member in enumType.Members)
            {
                String memberid = "EV_";
                if((int) member.Value < 0) memberid += "_" + (-member.Value);
                else memberid += member.Value.ToString();
                memberid += "_" + member.Name;
                WriteGXLNode(memberid, "EnumVal", new Attr("value", "string", member.Name));
                WriteGXLEdge(enumid, memberid, "containsValue");
            }
        }

        protected void WriteNode(String id, String typename, Attr[] attrs)
        {
            xmlwriter.WriteStartElement("node");
            xmlwriter.WriteAttributeString("id", id);
            WriteTypeElement(typename);
            WriteAttrs(attrs);
            xmlwriter.WriteEndElement();
        }

        protected void WriteGXLNode(String id, String gxlname, params Attr[] attrs) {
            WriteNode(id, "http://www.gupro.de/GXL/gxl-1.0.gxl#" + gxlname, attrs);
        }

        protected void WriteEdge(String id, String fromid, String toid, String typename, Attr[] attrs)
        {
            xmlwriter.WriteStartElement("edge");
            if (id != null) {
                xmlwriter.WriteAttributeString("id", id);
            }
            xmlwriter.WriteAttributeString("from", fromid);
            xmlwriter.WriteAttributeString("to", toid);
            WriteTypeElement(typename);
            WriteAttrs(attrs);
            xmlwriter.WriteEndElement();
        }

        protected void WriteGXLEdge(String fromid, String toid, String gxltype,
                                    params Attr[] attrs)
        {
            WriteEdge(null, fromid, toid, "http://www.gupro.de/GXL/gxl-1.0.gxl#" + gxltype, attrs);
        }

        protected void WriteAttrTypes(ITypeModel typemodel)
        {
            String prefix = typemodel.IsNodeModel ? "NAT_" : "EAT_";
            foreach(AttributeType attrtype in typemodel.AttributeTypes)
            {
                String id = prefix + attrtype.OwnerType.Name + "_" + attrtype.Name;
                WriteGXLNode(id, "AttributeClass", new Attr("name", "string", attrtype.Name));
                WriteGXLEdge(id, GetDomainID(attrtype), "hasDomain");
            }
        }

        protected void WriteModel(IGraph graph)
        {
            String modelnodeid = GetModelID(graph);
            String rootnodeid = GetElemTypeID(graph.Model.NodeModel.RootType);

            xmlwriter.WriteStartElement("graph");
            xmlwriter.WriteAttributeString("id", "SCE_" + modelnodeid);
            WriteTypeElement("http://www.gupro.de/GXL/gxl-1.0.gxl#gxl-1.0");

            WriteGXLNode(modelnodeid, "GraphClass",
                new Attr("name", "string", modelnodeid));

            WriteGXLNode(GetDomainID(AttributeKind.BooleanAttr), "Bool");
            WriteGXLNode(GetDomainID(AttributeKind.IntegerAttr), "Int");
            WriteGXLNode(GetDomainID(AttributeKind.FloatAttr),   "Float");
            WriteGXLNode(GetDomainID(AttributeKind.DoubleAttr),  "Float");
            WriteGXLNode(GetDomainID(AttributeKind.StringAttr),  "String");

            foreach(EnumAttributeType enumType in graph.Model.EnumAttributeTypes)
                WriteEnumType(enumType);

            WriteAttrTypes(graph.Model.NodeModel);
            WriteAttrTypes(graph.Model.EdgeModel);

            foreach(NodeType nodetype in graph.Model.NodeModel.Types)
            {
                String nodetypeid = GetElemTypeID(nodetype);
                WriteGXLNode(nodetypeid, "NodeClass",
                    new Attr("name", "string", nodetype.Name),
                    new Attr("isabstract", "bool", nodetype.IsAbstract ? "true" : "false"));

                foreach(AttributeType attrtype in nodetype.AttributeTypes)
                {
                    if(attrtype.OwnerType == nodetype)
                        WriteGXLEdge(nodetypeid, GetAttrTypeID(attrtype), "hasAttribute");
                }

                WriteGXLEdge(modelnodeid, nodetypeid, "contains");

                foreach(NodeType subtype in nodetype.DirectSubTypes)
                    WriteGXLEdge(GetElemTypeID(subtype), nodetypeid, "isA");
            }

            foreach(EdgeType edgetype in graph.Model.EdgeModel.Types)
            {
                String edgetypeid = GetElemTypeID(edgetype);
                WriteGXLNode(edgetypeid, "EdgeClass",
                    new Attr("name", "string", edgetype.Name),
                    new Attr("isabstract", "bool", edgetype.IsAbstract ? "true" : "false"),
                    new Attr("isdirected", "bool",
                        edgetype.Directedness == Directedness.Directed ? "true" : "false"));

                WriteGXLEdge(modelnodeid, edgetypeid, "contains");

                foreach(AttributeType attrtype in edgetype.AttributeTypes)
                {
                    if(attrtype.OwnerType == edgetype)
                        WriteGXLEdge(edgetypeid, GetAttrTypeID(attrtype), "hasAttribute");
                }

                foreach(EdgeType subtype in edgetype.DirectSubTypes)
                    WriteGXLEdge(GetElemTypeID(subtype), edgetypeid, "isA");

                // TODO: Use limits from "connect" statements
                WriteGXLEdge(edgetypeid, rootnodeid, "from",
                    new Attr("limits", "tup", "<int>0</int><int>-1</int>"),
                    new Attr("isordered", "bool", "false"));
                WriteGXLEdge(edgetypeid, rootnodeid, "to",
                    new Attr("limits", "tup", "<int>0</int><int>-1</int>"),
                    new Attr("isordered", "bool", "false"));
            }
            xmlwriter.WriteEndElement();
        }

        protected List<Attr> GetAttributes(IGraphElement elem)
        {
            List<Attr> attrs = new List<Attr>();
            foreach(AttributeType attrType in elem.Type.AttributeTypes) {
                object value = elem.GetAttribute(attrType.Name);

                String valType;
                String valuestr = (value == null) ? "" : value.ToString();
                switch(attrType.Kind)
                {
                    case AttributeKind.BooleanAttr:
                        valType = "bool";
                        valuestr = ((bool)value) ? "true" : "false";
                        break;

                    case AttributeKind.DoubleAttr:
                    case AttributeKind.FloatAttr:
                        valType = "float";
                        break;

                    case AttributeKind.IntegerAttr: valType = "int"; break;

                    // TODO: This does not allow differentiating between empty and null strings
                    case AttributeKind.StringAttr: valType = "string"; break;

                    case AttributeKind.EnumAttr: valType = "enum"; break;

                    default:
                        throw new Exception("Unsupported attribute value type: \"" + attrType.Kind + "\"");
                }
                attrs.Add(new Attr(attrType.Name, valType, valuestr));
            }
            return attrs;
        }

        protected void WriteGraph(IGraph graph)
        {
            xmlwriter.WriteStartElement("graph");
            xmlwriter.WriteAttributeString("id", graph.Name);
            xmlwriter.WriteAttributeString("edgeids", "true");
            xmlwriter.WriteAttributeString("edgemode", "defaultdirected");
            WriteTypeElement("#" + GetModelID(graph));

            foreach(INode node in graph.Nodes)
            {
                WriteNode("n" + node.GetHashCode(), "#" + GetElemTypeID(node.Type),
                          GetAttributes(node).ToArray());
            }

            foreach(IEdge edge in graph.Edges)
            {
                WriteEdge("e" + edge.GetHashCode(), "n" + edge.Source.GetHashCode(),
                          "n" + edge.Target.GetHashCode(),
                          "#" + GetElemTypeID(edge.Type),
                          GetAttributes(edge).ToArray());
            }
            xmlwriter.WriteEndElement();
        }

        public void Dispose()
        {
            if (xmlwriter != null) {
                xmlwriter.Close();
            }
        }
    }
}
