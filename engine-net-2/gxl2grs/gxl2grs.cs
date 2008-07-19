/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace gxl2grs
{
    class Program
    {
        static int Main(string[] args)
        {
            int graphIndex = 0;
            if(args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine(
                      "Usage: gxl2grs <gxl-file> [graph-index]\n"
                    + "Note: It is not possible to convert meta model graphs.");
                return 1;
            }
            if(args.Length == 2)
            {
                if(!int.TryParse(args[1], out graphIndex) || graphIndex < 0)
                {
                    Console.WriteLine("The zero-based graph index must be a positive integer.");
                    return 2;
                }
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(args[0]);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to load GXL document: " + ex.Message);
                return 3;
            }

            XmlElement root = doc.DocumentElement; // gxl node
            foreach(XmlNode commentNode in root.SelectNodes("//comment()"))
                commentNode.ParentNode.RemoveChild(commentNode);

            XmlNode graph = null;

            int numGraphs = 0;
            foreach(XmlNode node in root.ChildNodes)
            {
                if(node.Name == "graph")
                {
                    if(numGraphs == graphIndex)
                    {
                        graph = node;
                        break;
                    }
                    numGraphs++;
                }
            }

            if(graph == null)
            {
                Console.WriteLine("Invalid graph index: The given GXL document has only "
                    + numGraphs + " graph nodes!");
                return 4;
            }

            String graphName = graph.Attributes["id"].InnerText;

            using(StreamWriter grs = new StreamWriter(graphName + ".grs", false, Encoding.Default, 4096))
            {
                grs.WriteLine("new graph \"" + graphName + "\"");
                /* for java program graphs, refactoring case grabats 08
                grs.WriteLine("debug set layout Hierarchic");
                grs.WriteLine("debug set layout option NODE_PLACEMENT TREE");
                grs.WriteLine("debug set layout option ORIENTATION BOTTOM_TO_TOP");
                grs.WriteLine("dump set node Package color gold");
                grs.WriteLine("dump set node Class color yellow");
                grs.WriteLine("dump set node Literal color lightgrey");
                grs.WriteLine("dump set node MethodBody color pink");
                grs.WriteLine("dump set node Operation color orchid");
                grs.WriteLine("dump set node Block color darkmagenta");
                grs.WriteLine("dump set node Variabel color cyan");
                grs.WriteLine("dump set node Parameter color lightcyan");
                grs.WriteLine("dump set node Access color lightgreen");
                grs.WriteLine("dump set node Operator color green");
                grs.WriteLine("dump set node Call color green");
                grs.WriteLine("dump set node Update color darkgreen");
                */
                grs.WriteLine();

                foreach(XmlNode element in graph.ChildNodes)
                {
                    if (element.Name == "node")
                    {
                        processNode(element, grs);
                    }
                    else if (element.Name == "edge")
                    {
                        processEdge(element, grs);
                    }
                }
            }
            return 0;
        }

        static void processNode(XmlNode node, StreamWriter grs)
        {
            String name = node.Attributes["id"].Value;
            String type = "";
            List<String> attributes = new List<String>();
            foreach (XmlNode typeNAttr in node.ChildNodes)
            {
                if (typeNAttr.Name == "type")
                {
                    char[] separators = new char[] { '#' };
                    string[] pathAndType = typeNAttr.Attributes[0].Value.Split(separators);
                    bool onlyType = pathAndType.Length==1;
                    type = pathAndType[onlyType ? 0 : 1];
                    if (type == "Variable") type = "Variabel"; // Variable is class name in generated code, can't use as type
                }
                else if (typeNAttr.Name == "attr")
                {
                    String attrName = typeNAttr.Attributes["name"].Value;
                    if (attrName == "isfinal") attrName = "isFinal"; // case insensitive input file or error? 
                    String attrValue = "";
                    if (typeNAttr.ChildNodes[0].Name == "string")
                    {
                        attrValue = "\"" + typeNAttr.ChildNodes[0].InnerText +"\"";
                    }
                    else
                    {
                        attrValue = typeNAttr.ChildNodes[0].InnerText;
                    }
                    attributes.Add(attrName + "=" + attrValue);
                }
            }

            grs.Write("new " + name + ":" + type);

            if(attributes.Count != 0)
            {
                grs.Write("(");
                bool first = true;
                foreach (String attribute in attributes)
                {
                    if (first) first = false;
                    else grs.Write(",");
                    grs.Write(attribute);
                }
                grs.Write(")");
            }
            grs.WriteLine();
        }

        static void processEdge(XmlNode edge, StreamWriter grs)
        {
            String name = edge.Attributes["id"].Value;
            String from = edge.Attributes["from"].Value;
            String to = edge.Attributes["to"].Value;
            String type = "";
            List<String> attributes = new List<String>();
            foreach (XmlNode typeNAttr in edge.ChildNodes)
            {
                if (typeNAttr.Name == "type")
                {
                    char[] separators = new char[] { '#' };
                    string[] pathAndType = typeNAttr.Attributes[0].Value.Split(separators);
                    bool onlyType = pathAndType.Length == 1;
                    type = pathAndType[onlyType ? 0 : 1];
                    if (type == "extends") type = "extendss"; // extends is keyword in gm, can't use as type
                    if (type == "type") type = "ttype"; // type is keyword in .grs, can't use as type
                }
                else if (typeNAttr.Name == "attr")
                {
                    String attrName = typeNAttr.Attributes[0].Value;
                    String attrValue = "";
                    if (typeNAttr.ChildNodes[0].Name == "string")
                    {
                        attrValue = "\"" + typeNAttr.ChildNodes[0].InnerText + "\"";
                    }
                    else
                    {
                        attrValue = typeNAttr.ChildNodes[0].InnerText;
                    }
                    attributes.Add(attrName + "=" + attrValue);
                }
            }

            grs.Write("new " + from + " -" + name + ":" + type);
            if(attributes.Count != 0)
            {
                grs.Write("(");
                bool first = true;
                foreach (String attribute in attributes)
                {
                    if (first) first = false;
                    else grs.Write(",");
                    grs.Write(attribute);
                }
                grs.Write(")");
            }
            grs.WriteLine(" -> " + to);
        }
    }
}
