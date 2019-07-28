/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Imports graphs from the GXL format.
    /// </summary>
    public class GXLImport
    {
        protected static String GetGrGenName(String xlink)
        {
            return xlink.Substring(xlink.LastIndexOf('#') + 1);
        }

        protected static String GetTypeName(XmlElement elem)
        {
            XmlElement typeelem = elem["type"];
            if(typeelem == null)
                throw new Exception("Element \"" + elem.GetAttribute("id") + "\" has no type element.");
            String typename = typeelem.GetAttribute("xlink:href");
            if(typename == "")
                throw new Exception("The type element of \"" + elem.GetAttribute("id") + "\" has no xlink:href attribute.");
            return typename;
        }

        protected static String GetGXLAttr(XmlElement elem, String name, String attrkind)
        {
            XmlElement attrelem = (XmlElement) elem.SelectSingleNode("attr[@name='" + name + "']");
            if(attrelem == null)
                throw new Exception("Element \"" + elem.GetAttribute("id")
                    + "\" has no attr element with the name \"" + name + "\".");
            XmlElement valelem = attrelem[attrkind];
            if(valelem == null)
                throw new Exception("Element \"" + elem.GetAttribute("id")
                    + "\" has no \"" + name + "\" attr element of type \"" + attrkind + "\"");
            return valelem.InnerText;
        }

        /// <summary>
        /// Imports the first graph not being of type "gxl-1.0" from a GXL file
        /// with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported.</param>
        /// <param name="modelOverride">If not null, overrides the filename of the graph model to be used.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="actions">Receives the actions object in case a .grg model is given.</param>
        public static IGraph Import(String importFilename, String modelOverride, IBackend backend, out IActions actions)
        {
            return Import(new StreamReader(importFilename), modelOverride, backend, out actions);
        }

        /// <summary>
        /// Imports the first graph not being of type "gxl-1.0" from the given text reader input stream.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="inStream">The text reader input stream import source.</param>
        /// <param name="modelOverride">If not null, overrides the filename of the graph model to be used.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="actions">Receives the actions object in case a .grg model is given.</param>
        public static IGraph Import(TextReader inStream, String modelOverride, IBackend backend, out IActions actions)
        {
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(inStream);

            XmlElement gxlelem = doc["gxl"];
            if(gxlelem == null)
                throw new Exception("The document has no gxl element.");

            XmlElement graphelem = null;
            String graphtype = null;
            foreach(XmlElement curgraphnode in gxlelem.GetElementsByTagName("graph"))
            {
                graphtype = GetTypeName(curgraphnode);
                if(!graphtype.EndsWith("gxl-1.0.gxl#gxl-1.0"))
                {
                    if(graphelem != null)
                        throw new Exception("More than one instance graph included (not yet supported)!");
                    graphelem = curgraphnode;
                }
            }
            if(graphelem == null)
                throw new Exception("No non-meta graph found!");

            String modelfilename;
            if(modelOverride == null)
            {
                XmlDocument modeldoc;
                int hashindex = graphtype.IndexOf('#');
                if(hashindex <= 0)
                    modeldoc = doc;
                else
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ProhibitDtd = false;
                    XmlReader reader = XmlReader.Create(graphtype.Substring(0, hashindex), settings);
                    modeldoc = new XmlDocument();
                    modeldoc.Load(reader);
                }

                String localname = graphtype.Substring(hashindex + 1);
                XmlElement modelnode = (XmlElement) modeldoc.SelectSingleNode(
                    "descendant::graph/node[@id='" + localname + "']");
                if(modelnode == null)
                    throw new Exception("Graph schema \"" + graphtype + "\" not found.");
                if(!GetTypeName(modelnode).EndsWith("gxl-1.0.gxl#GraphClass"))
                    throw new Exception("Graph type \"" + graphtype + "\" must refer to a GraphClass node.");

                String modelname = GetGXLAttr(modelnode, "name", "string");
                if(modelname.EndsWith("__gxl"))
                    modelname = modelname.Substring(0, modelname.Length - 5);
                XmlElement modelgraph = (XmlElement) modelnode.ParentNode;
                modelfilename = ImportModel(modelgraph, modelname);
            }
            else modelfilename = modelOverride;

            IGraph graph;
            String graphname = graphelem.GetAttribute("id");
            if(modelfilename.EndsWith(".grg"))
            {
                backend.CreateFromSpec(modelfilename, graphname, null, 
                    ProcessSpecFlags.UseNoExistingFiles, new List<String>(), 
                    out graph, out actions);
            }
            else
            {
                graph = backend.CreateGraph(modelfilename, graphname);
                actions = null;
            }

            ImportGraph(graph, doc, graphelem);

            return graph;
        }

        /// <summary>
        /// Imports the first graph not being of type "gxl-1.0" from a GXL file
        /// with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported,
        ///     the model specification part will be ignored.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used, 
        ///     it must be conformant to the model used in the file to be imported.</param>
        /// <param name="actions">Receives the actions object in case a .grg model is given.</param>
        public static IGraph Import(String importFilename, IBackend backend, IGraphModel graphModel, out IActions actions)
        {
            return Import(new StreamReader(importFilename), backend, graphModel, out actions);
        }

        /// <summary>
        /// Imports the first graph not being of type "gxl-1.0" from the given text reader input stream.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="inStream">The text reader input stream import source.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used, 
        ///     it must be conformant to the model used in the file to be imported.</param>
        /// <param name="actions">Receives the actions object in case a .grg model is given.</param>
        public static IGraph Import(TextReader inStream, IBackend backend, IGraphModel graphModel, out IActions actions)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inStream);

            XmlElement gxlelem = doc["gxl"];
            if (gxlelem == null)
                throw new Exception("The document has no gxl element.");

            XmlElement graphelem = null;
            String graphtype = null;
            foreach (XmlElement curgraphnode in gxlelem.GetElementsByTagName("graph"))
            {
                graphtype = GetTypeName(curgraphnode);
                if (!graphtype.EndsWith("gxl-1.0.gxl#gxl-1.0"))
                {
                    if (graphelem != null)
                        throw new Exception("More than one instance graph included (not yet supported)!");
                    graphelem = curgraphnode;
                }
            }
            if (graphelem == null)
                throw new Exception("No non-meta graph found!");

            String graphname = graphelem.GetAttribute("id");
            IGraph graph = backend.CreateGraph(graphModel, graphname);

            ImportGraph(graph, doc, graphelem);

            actions = null;
            return graph;
        }

        public enum ThingKind
        {
            Domain,
            EnumDomain,
            EnumValue,
            AttributeClass,
            NodeClass,
            EdgeClass
        }

        protected class AttributeClass
        {
            public String Name;
            public String Type;

            public AttributeClass(String name)
            {
                Name = name;
            }
        }

        protected class EnumDomain
        {
            public String Name;
            public List<EnumMember> Members = new List<EnumMember>();

            public EnumDomain(String name)
            {
                Name = name;
            }
        }

        protected class NodeClass
        {
            public String Name;
            public bool IsAbstract;
            public List<String> SuperClasses = new List<String>();
            public List<AttributeClass> AttrList = new List<AttributeClass>();

            public NodeClass(String name, bool isAbstract)
            {
                Name = name;
                IsAbstract = isAbstract;
            }
        }

        protected class EdgeClass : NodeClass
        {
            public bool IsDirected;

            public EdgeClass(String name, bool isAbstract, bool isDirected)
                : base(name, isAbstract)
            {
                IsDirected = isDirected;
            }
        }

        protected class Thing
        {
            public String ID;
            public ThingKind Kind;
            public object Value;

            public Thing(String id, ThingKind kind, object thing)
            {
                ID = id;
                Kind = kind;
                Value = thing;
            }

            public String AttributeKind
            {
                get
                {
                    if(Kind == ThingKind.Domain)
                        return (String) Value;
                    else if(Kind == ThingKind.EnumDomain)
                        return ((EnumDomain) Value).Name;
                    else
                        throw new Exception("\"" + ID + "\" is not a domain node.");
                }
            }

            public EnumDomain EnumDomain
            {
                get
                {
                    if(Kind != ThingKind.EnumDomain)
                        throw new Exception("\"" + ID + "\" is not an enum domain node.");
                    return (EnumDomain) Value;
                }
            }

            public EnumMember EnumValue
            {
                get
                {
                    if(Kind != ThingKind.EnumValue)
                        throw new Exception("\"" + ID + "\" is not an enum value node.");
                    return (EnumMember) Value;
                }
            }

            public AttributeClass AttributeClass
            {
                get
                {
                    if(Kind != ThingKind.AttributeClass)
                        throw new Exception("\"" + ID + "\" is not an attribute class node.");
                    return (AttributeClass) Value;
                }
            }

            public NodeClass NodeClass
            {
                get
                {
                    if(Kind != ThingKind.NodeClass)
                        throw new Exception("\"" + ID + "\" is not a node class node.");
                    return (NodeClass) Value;
                }
            }

            public NodeClass NodeOrEdgeClass
            {
                get
                {
                    if(Kind != ThingKind.NodeClass && Kind != ThingKind.EdgeClass)
                        throw new Exception("\"" + ID + "\" is not a node or edge class node.");
                    return (NodeClass) Value;
                }
            }

            public EdgeClass EdgeClass
            {
                get
                {
                    if(Kind != ThingKind.EdgeClass)
                        throw new Exception("\"" + ID + "\" is not an edge class node.");
                    return (EdgeClass) Value;
                }
            }
        }

        protected class IDMap : Dictionary<String, Thing>
        {
            public List<NodeClass>  NodeClasses = new List<NodeClass>();
            public List<EdgeClass>  EdgeClasses = new List<EdgeClass>();
            public List<EnumDomain> EnumDomains = new List<EnumDomain>();

            public new Thing this[String key]
            {
                get
                {
                    return base[key];
                }
                set
                {
                    if(value.Kind == ThingKind.NodeClass)
                        NodeClasses.Add((NodeClass) value.Value);
                    else if(value.Kind == ThingKind.EdgeClass)
                        EdgeClasses.Add((EdgeClass) value.Value);
                    else if(value.Kind == ThingKind.EnumDomain)
                        EnumDomains.Add((EnumDomain) value.Value);
                    base[key] = value;
                }
            }
        }

        protected static String ImportModel(XmlElement modelgraph, String modelname)
        {
            IDMap idmap = new IDMap();
            int nextenumval = 1000;

            foreach(XmlElement nodeelem in modelgraph.GetElementsByTagName("node"))
            {
                String nodetype = GetTypeName(nodeelem);

                // skip unknown elements
                int hashchar = nodetype.IndexOf('#');
                if(hashchar == -1 || !nodetype.Substring(0, hashchar).EndsWith("gxl-1.0.gxl"))
                    continue;

                String id = nodeelem.GetAttribute("id");
                nodetype = nodetype.Substring(hashchar + 1);
                switch(nodetype)
                {
                    case "Bool":
                        idmap[id] = new Thing(id, ThingKind.Domain, "boolean");
                        break;

                    case "Int":
                        idmap[id] = new Thing(id, ThingKind.Domain, "int");
                        break;

                    case "Float":
                        idmap[id] = new Thing(id, ThingKind.Domain, "double");
                        break;

                    case "String":
                        idmap[id] = new Thing(id, ThingKind.Domain, "string");
                        break;

                    case "Enum":
                    {
                        String name;
                        if(id.StartsWith("DM_enum_")) name = id.Substring(8);
                        else name = id;
                        idmap[id] = new Thing(id, ThingKind.EnumDomain, new EnumDomain(name));
                        break;
                    }

                    case "EnumVal":
                    {
                        int val;
                        if(id.StartsWith("EV_"))
                        {
                            int ind = id.IndexOf('_', 4);
                            if(id[3] == '_')
                            {
                                val = -int.Parse(id.Substring(4, ind - 4));
                            }
                            else
                            {
                                val = int.Parse(id.Substring(3, ind - 3));
                            }
                        }
                        else
                        {
                            val = nextenumval++;
                        }

                        String name = GetGXLAttr(nodeelem, "value", "string");
                        idmap[id] = new Thing(id, ThingKind.EnumValue, new EnumMember(val, name));
                        break;
                    }

                    case "AttributeClass":
                    {
                        String name = GetGXLAttr(nodeelem, "name", "string");
                        idmap[id] = new Thing(id, ThingKind.AttributeClass, new AttributeClass(name));
                        break;
                    }

                    case "NodeClass":
                    {
                        String name = GetGXLAttr(nodeelem, "name", "string");
                        bool isabstract = GetGXLAttr(nodeelem, "isabstract", "bool") == "true";
                        idmap[id] = new Thing(id, ThingKind.NodeClass, new NodeClass(name, isabstract));
                        break;
                    }

                    case "EdgeClass":
                    {
                        String name = GetGXLAttr(nodeelem, "name", "string");
                        bool isabstract = GetGXLAttr(nodeelem, "isabstract", "bool") == "true";
                        bool isdirected = GetGXLAttr(nodeelem, "isdirected", "bool") == "true";
                        idmap[id] = new Thing(id, ThingKind.EdgeClass, new EdgeClass(name, isabstract, isdirected));
                        break;
                    }
                }
            }

            foreach(XmlElement edgeelem in modelgraph.GetElementsByTagName("edge"))
            {
                String edgetype = GetTypeName(edgeelem);

                // skip unknown elements
                int hashchar = edgetype.IndexOf('#');
                if(hashchar == -1 || !edgetype.Substring(0, hashchar).EndsWith("gxl-1.0.gxl"))
                    continue;

                String fromid = edgeelem.GetAttribute("from");
                String toid   = edgeelem.GetAttribute("to");

                edgetype = edgetype.Substring(hashchar + 1);
                switch(edgetype)
                {
                    case "hasDomain":
                    {
                        AttributeClass attrClass = idmap[fromid].AttributeClass;
                        String         attrKind  = idmap[toid].AttributeKind;
                        attrClass.Type = attrKind;
                        break;
                    }

                    case "containsValue":
                    {
                        EnumDomain enumDomain = idmap[fromid].EnumDomain;
                        EnumMember enumMember = idmap[toid].EnumValue;
                        enumDomain.Members.Add(enumMember);
                        break;
                    }

                    case "isA":
                    {
                        NodeClass nodeClass = idmap[fromid].NodeOrEdgeClass;
                        nodeClass.SuperClasses.Add(toid);
                        break;
                    }

                    case "hasAttribute":
                    {
                        NodeClass nodeClass = idmap[fromid].NodeOrEdgeClass;
                        AttributeClass attrClass = idmap[toid].AttributeClass;
                        nodeClass.AttrList.Add(attrClass);
                        break;
                    }
                }
            }

            String model = BuildModel(idmap);
            String modelfilename = modelname + "__gxl.gm";
            using(StreamWriter writer = new StreamWriter(modelfilename))
                writer.Write(model);
            return modelfilename;
        }

        protected static String BuildModel(IDMap idmap)
        {
            StringBuilder sb = new StringBuilder();

            foreach(EnumDomain enumdomain in idmap.EnumDomains)
            {
                sb.Append("enum " + enumdomain.Name + " { ");

                bool first = true;
                foreach(EnumMember enummember in enumdomain.Members)
                {
                    if(first) first = false;
                    else sb.Append(", ");
                    sb.Append(enummember.Name + "=" + enummember.Value);
                }
                sb.Append(" }\n");
            }

            // TODO: Find the root node type!
            String rootnodetype = "Node";

            foreach(NodeClass nodeclass in idmap.NodeClasses)
            {
                if(nodeclass.Name == rootnodetype)
                    continue;

                if(nodeclass.IsAbstract)
                    sb.Append("abstract ");
                sb.Append("node class " + nodeclass.Name);

                BuildInheritance(sb, nodeclass, rootnodetype);
                BuildBody(sb, nodeclass);
            }

            // TODO: Find the root edge type!
            String rootedgetype = "Edge";

            foreach(EdgeClass edgeclass in idmap.EdgeClasses)
            {
                if(edgeclass.Name == rootedgetype || edgeclass.Name == "AEdge" || edgeclass.Name == "UEdge")
                    continue;

                if(edgeclass.IsAbstract)
                    sb.Append("abstract ");
                if(!edgeclass.IsDirected)
                    sb.Append("undirected ");
                sb.Append("edge class " + edgeclass.Name);

                BuildInheritance(sb, edgeclass, rootedgetype);
                BuildBody(sb, edgeclass);
            }

            return sb.ToString();
        }

        protected static void BuildInheritance(StringBuilder sb, NodeClass elemclass, String roottype)
        {
            bool first = true;
            foreach(String supertype in elemclass.SuperClasses)
            {
                if(supertype == roottype) continue;

                if(first)
                {
                    sb.Append(" extends ");
                    first = false;
                }
                else sb.Append(", ");
                sb.Append(supertype);
            }
        }

        protected static void BuildBody(StringBuilder sb, NodeClass elemclass)
        {
            sb.Append("\n{\n");
            foreach(AttributeClass attrclass in elemclass.AttrList)
            {
                sb.Append('\t');
                sb.Append(attrclass.Name);
                sb.Append(':');
                sb.Append(attrclass.Type);
                sb.Append(";\n");
            }
            sb.Append("}\n");
        }

        protected static void ImportGraph(IGraph graph, XmlDocument doc, XmlElement graphnode)
        {
            IGraphModel model = graph.Model;
            Dictionary<String, INode> nodemap = new Dictionary<string, INode>();
            foreach(XmlElement nodeelem in graphnode.GetElementsByTagName("node"))
            {
                String nodetype = GetGrGenName(GetTypeName(nodeelem));
                NodeType type = model.NodeModel.GetType(nodetype);
                if(type == null)
                    throw new Exception("Unknown node type: \"" + nodetype + "\"");
                INode node = graph.AddNode(type);
                ReadAttributes(node, nodeelem);
                nodemap[nodeelem.GetAttribute("id")] = node;
            }

            foreach(XmlElement edgeelem in graphnode.GetElementsByTagName("edge"))
            {
                String edgetype = GetGrGenName(GetTypeName(edgeelem));
                EdgeType type = model.EdgeModel.GetType(edgetype);
                if(type == null)
                    throw new Exception("Unknown edge type: \"" + edgetype + "\"");
                INode src = GetNode(edgeelem, "from", nodemap);
                INode tgt = GetNode(edgeelem, "to", nodemap);
                IEdge edge = graph.AddEdge(type, src, tgt);
                ReadAttributes(edge, edgeelem);
            }
        }

        private static INode GetNode(XmlElement edgeelem, String attr, Dictionary<string, INode> nodemap)
        {
            String id = edgeelem.GetAttribute(attr);
            INode node;
            if(!nodemap.TryGetValue(id, out node))
                throw new Exception("Unknown \"" + attr + "\" node: \"" + id + "\"");
            return node;
        }

        private static void ReadAttributes(IGraphElement elem, XmlElement xmlelem)
        {
            GrGenType type = elem.Type;
            foreach(XmlElement attrelem in xmlelem.GetElementsByTagName("attr"))
            {
                String attrname = attrelem.GetAttribute("name");
                String attrval = attrelem.InnerText;
                AttributeType attrType = type.GetAttributeType(attrname);

                object value = null;
                switch(attrType.Kind)
                {
                    case AttributeKind.BooleanAttr:
                        if(attrval.Equals("true", StringComparison.OrdinalIgnoreCase))
                            value = true;
                        else if(attrval.Equals("false", StringComparison.OrdinalIgnoreCase))
                            value = false;
                        else
                            throw new Exception("Attribute \"" + attrname + "\" must be either \"true\" or \"false\"!");
                        break;

                    case AttributeKind.EnumAttr:
                    {
                        int val;
                        if(Int32.TryParse(attrval, out val))
                        {
                            value = val;
                        }
                        else
                        {
                            foreach(EnumMember member in attrType.EnumType.Members)
                            {
                                if(attrval == member.Name)
                                {
                                    value = member.Value;
                                    break;
                                }
                            }
                            if(value == null)
                            {
                                String errorText = "Attribute \"" + attrname + "\" must be one of the following values:";
                                foreach(EnumMember member in attrType.EnumType.Members)
                                    errorText += " - " + member.Name + " = " + member.Value;
                                throw new Exception(errorText);
                            }
                        }
                        break;
                    }

                    case AttributeKind.ByteAttr:
                    {
                        sbyte val;
                        if(!SByte.TryParse(attrval, out val))
                            throw new Exception("Attribute \"" + attrname + "\" must be a byte (signed)!");
                        value = val;
                        break;
                    }

                    case AttributeKind.ShortAttr:
                    {
                        short val;
                        if (!Int16.TryParse(attrval, out val))
                            throw new Exception("Attribute \"" + attrname + "\" must be a short!");
                        value = val;
                        break;
                    }

                    case AttributeKind.IntegerAttr:
                    {
                        int val;
                        if (!Int32.TryParse(attrval, out val))
                            throw new Exception("Attribute \"" + attrname + "\" must be an integer!");
                        value = val;
                        break;
                    }

                    case AttributeKind.LongAttr:
                    {
                        long val;
                        if (!Int64.TryParse(attrval, out val))
                            throw new Exception("Attribute \"" + attrname + "\" must be a long!");
                        value = val;
                        break;
                    }

                    case AttributeKind.StringAttr:
                        value = attrval;
                        break;

                    case AttributeKind.FloatAttr:
                    {
                        float val;
                        if(!Single.TryParse(attrval, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out val))
                            throw new Exception("Attribute \"" + attrname + "\" must be a floating point number!");
                        value = val;
                        break;
                    }

                    case AttributeKind.DoubleAttr:
                    {
                        double val;
                        if(!Double.TryParse(attrval, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out val))
                            throw new Exception("Attribute \"" + attrname + "\" must be a floating point number!");
                        value = val;
                        break;
                    }

                    case AttributeKind.ObjectAttr:
                    {
                        throw new Exception("Attribute \"" + attrname + "\" is an object type attribute!\n"
                            + "It is not possible to assign a value to an object type attribute!");
                    }

                    case AttributeKind.SetAttr:
                    case AttributeKind.MapAttr:
                    case AttributeKind.ArrayAttr:
                    case AttributeKind.DequeAttr:
                    default:
                        throw new Exception("Unsupported attribute value type: \"" + attrType.Kind + "\""); // TODO: support set=SetAttr and seq=ArrayAttr, here and in export
                }

                elem.SetAttribute(attrname, value);
            }
        }
    }
}
