/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Exports graphs to the GXL format.
    /// </summary>
    public class GXLExport : IDisposable
    {
        StreamWriter writer;

        protected GXLExport(String filename)
        {
            writer = new StreamWriter(filename);
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

        protected void Export(IGraph graph)
        {
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n"
                + "<!DOCTYPE gxl SYSTEM \"http://www.gupro.de/GXL/gxl-1.0.dtd\">\n"
                + "<gxl>");
            WriteModel(graph);
            WriteGraph(graph);
            writer.WriteLine("</gxl>");
        }

        protected String GetDomainID(AttributeKind kind)
        {
            switch(kind)
            {
                case AttributeKind.BooleanAttr: return "DM_bool";
                case AttributeKind.DoubleAttr: return "DM_double";
                case AttributeKind.FloatAttr: return "DM_float";
                case AttributeKind.IntegerAttr: return "DM_int";
                case AttributeKind.StringAttr: return "DM_string";
                default:
                    throw new Exception("Unsupported attribute value type: \"" + kind + "\"");
            }
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

        protected void WriteGXLNode(String id, String gxlname, params Attr[] attrs)
        {
            writer.WriteLine("\t<node id=\"" + id + "\">\n"
                + "\t\t<type xlink:href=\"http://www.gupro.de/GXL/gxl-1.0.gxl#" + gxlname + "\"/>");
            foreach(Attr attr in attrs)
            {
                writer.WriteLine("\t\t<attr name=\"" + attr.Name + "\"><" + attr.Type + ">"
                    + attr.Value + "</" + attr.Type + "></attr>");
            }            
            writer.WriteLine("\t</node>");
        }

        protected void WriteGXLEdge(String fromid, String toid, String gxltype, params Attr[] attrs)
        {
            writer.WriteLine("\t<edge from=\"" + fromid + "\" to=\"" + toid + "\">\n"
                + "\t\t<type xlink:href=\"http://www.gupro.de/GXL/gxl-1.0.gxl#" + gxltype + "\"/>");
            foreach(Attr attr in attrs)
            {
                writer.WriteLine("\t\t<attr name=\"" + attr.Name + "\"><" + attr.Type + ">"
                    + attr.Value + "</" + attr.Type + "></attr>");
            } 
            writer.WriteLine("\t</edge>");
        }

        protected void WriteAttrTypes(ITypeModel typemodel)
        {
            String prefix = typemodel.IsNodeModel ? "NAT_" : "EAT_";
            foreach(AttributeType attrtype in typemodel.AttributeTypes)
            {
                String id = prefix + attrtype.OwnerType.Name + "_" + attrtype.Name;
                WriteGXLNode(id, "AttributeClass", new Attr("name", "string", attrtype.Name));
                WriteGXLEdge(id, GetDomainID(attrtype.Kind), "hasDomain");
            }
        }

        protected void WriteModel(IGraph graph)
        {
            String modelnodeid = graph.Model.ModelName;
            String rootnodeid = GetElemTypeID(graph.Model.NodeModel.RootType);

            writer.WriteLine("<graph id=\"SCE_" + graph.Model.ModelName + "\">\n"
                + "\t<type xlink:href=\"http://www.gupro.de/GXL/gxl-1.0.gxl#gxl-1.0\"/>");

            WriteGXLNode(modelnodeid, "GraphClass",
                new Attr("name", "string", graph.Model.ModelName));

            WriteGXLNode(GetDomainID(AttributeKind.BooleanAttr), "Bool");
            WriteGXLNode(GetDomainID(AttributeKind.IntegerAttr), "Int");
            WriteGXLNode(GetDomainID(AttributeKind.FloatAttr),   "Float");
            WriteGXLNode(GetDomainID(AttributeKind.DoubleAttr),  "Float");
            WriteGXLNode(GetDomainID(AttributeKind.StringAttr),  "String");

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
            writer.WriteLine("</graph>");
        }

        protected void WriteAttributes(IGraphElement elem)
        {
            foreach(AttributeType attrType in elem.Type.AttributeTypes)
            {
                object value = elem.GetAttribute(attrType.Name);

                writer.Write("\t\t<attr name=\"" + attrType.Name + "\">");
                String valType;
                switch(attrType.Kind)
                {
                    case AttributeKind.BooleanAttr:
                        // special case for bool because we need lowercase characters...
                        if((bool) value) writer.WriteLine("<bool>true</bool></attr>");
                        else writer.WriteLine("<bool>false</bool></attr>");
                        continue;

                    case AttributeKind.DoubleAttr:
                    case AttributeKind.FloatAttr:
                        valType = "float";
                        break;

                    case AttributeKind.IntegerAttr: valType = "int"; break;

                    case AttributeKind.StringAttr: valType = "string"; break;

                    default:
                        throw new Exception("Unsupported attribute value type: \"" + attrType.Kind + "\"");
                }
                // TODO: This does not allow differentiating between empty and null strings
                if(value == null)
                    writer.WriteLine("<" + valType + "></" + valType + "></attr>");
                else
                    writer.WriteLine("<" + valType + ">"
                        + value.ToString()
                        + "</" + valType + "></attr>");
            }
        }

        protected void WriteGraph(IGraph graph)
        {
            writer.WriteLine("<graph id=\"" + graph.Name + "\" edgeids=\"true\" edgemode=\"defaultdirected\">\n"
                + "\t<type xlink:href=\"#" + graph.Model.ModelName + "\"/>");

            foreach(INode node in graph.Nodes)
            {
                writer.WriteLine("\t<node id=\"n" + node.GetHashCode() + "\">\n"
                    + "\t\t<type xlink:href=\"#" + GetElemTypeID(node.Type) + "\"/>");
                WriteAttributes(node);
                writer.WriteLine("\t</node>");
            }

            foreach(IEdge edge in graph.Edges)
            {
                writer.WriteLine("\t<edge id=\"e" + edge.GetHashCode()
                    + "\" from=\"n" + edge.Source.GetHashCode()
                    + "\" to=\"n" + edge.Target.GetHashCode()
                    + "\">\n"
                    + "\t\t<type xlink:href=\"#" + GetElemTypeID(edge.Type) + "\"/>");
                WriteAttributes(edge);
                writer.WriteLine("\t</edge>");
            }
            writer.WriteLine("</graph>");
        }

        public void Dispose()
        {
            if(writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }
    }
}
