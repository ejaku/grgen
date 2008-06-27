using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace gxl2grs
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(args[0]);

            XmlElement root = doc.DocumentElement; // gxl node

            XmlNode graph = root.ChildNodes.Item(root.ChildNodes.Count==1 ? 0 : int.Parse(args[1])); // graph node
            String graphName = graph.Attributes["id"].InnerText;

            StreamWriter grs = new StreamWriter(graphName+".grs", false, Encoding.Default, 4096);

            grs.WriteLine("new graph \"" + graphName + "\"");
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

            grs.Close();
        }

        static void processNode(XmlNode node, StreamWriter grs)
        {
            String name = node.Attributes["id"].InnerText;
            String type = "";
            List<String> attributes = new List<String>();
            foreach (XmlNode typeNAttr in node.ChildNodes)
            {
                if (typeNAttr.Name == "type")
                {
                    char[] separators = new char[] { '#' };
                    string[] pathAndType = typeNAttr.Attributes.Item(0).InnerText.Split(separators);
                    bool onlyType = pathAndType.Length==1;
                    type = pathAndType[onlyType ? 0 : 1];
                    if (type == "Variable") type = "Variabel"; // Variable is class name in generated code, can't use as type
                }
                else if (typeNAttr.Name == "attr")
                {
                    String attrName = typeNAttr.Attributes.Item(0).InnerText;
                    if (attrName == "isfinal") attrName = "isFinal"; // case insensitive input file or error? 
                    String attrValue = "";
                    if (typeNAttr.ChildNodes.Item(0).Name == "string")
                    {
                        attrValue = "\"" + typeNAttr.ChildNodes.Item(0).InnerText +"\"";
                    }
                    else
                    {
                        attrValue = typeNAttr.ChildNodes.Item(0).InnerText;
                    }
                    attributes.Add(attrName + "=" + attrValue);
                }
            }

            if (attributes.Count == 0)
            {
                grs.WriteLine("new " + name + ":" + type);
            }
            else
            {
                grs.Write("new " + name + ":" + type + "(");
                bool first = true;
                foreach (String attribute in attributes)
                {
                    if (first)
                    {
                        grs.Write(attribute);
                        first = false;
                    }
                    else
                    {
                        grs.Write("," + attribute);
                    }
                }
                grs.WriteLine(")");
            }
        }

        static void processEdge(XmlNode edge, StreamWriter grs)
        {
            String name = edge.Attributes["id"].InnerText;
            String from = edge.Attributes["from"].InnerText;
            String to = edge.Attributes["to"].InnerText;
            String type = "";
            List<String> attributes = new List<String>();
            foreach (XmlNode typeNAttr in edge.ChildNodes)
            {
                if (typeNAttr.Name == "type")
                {
                    char[] separators = new char[] { '#' };
                    string[] pathAndType = typeNAttr.Attributes.Item(0).InnerText.Split(separators);
                    bool onlyType = pathAndType.Length == 1;
                    type = pathAndType[onlyType ? 0 : 1];
                    if (type == "extends") type = "extendss"; // extends is keyword in gm, can't use as type
                    if (type == "type") type = "ttype"; // type is keyword in .grs, can't use as type
                }
                else if (typeNAttr.Name == "attr")
                {
                    String attrName = typeNAttr.Attributes.Item(0).InnerText;
                    String attrValue = "";
                    if (typeNAttr.ChildNodes.Item(0).Name == "string")
                    {
                        attrValue = "\"" + typeNAttr.ChildNodes.Item(0).InnerText + "\"";
                    }
                    else
                    {
                        attrValue = typeNAttr.ChildNodes.Item(0).InnerText;
                    }
                    attributes.Add(attrName + "=" + attrValue);
                }
            }

            if (attributes.Count == 0)
            {
                grs.WriteLine("new " + from + " -" + name + ":" + type + "-> " + to);
            }
            else
            {
                grs.Write("new " + from + " -" + name + ":" + type + "(");
                bool first = true;
                foreach (String attribute in attributes)
                {
                    if (first)
                    {
                        grs.Write(attribute);
                        first = false;
                    }
                    else
                    {
                        grs.Write("," + attribute);
                    }
                }
                grs.WriteLine(") -> " + to);
            }
        }
    }
}
