using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Imports a graph model from the ECore format.
    /// </summary>
    class ECoreImport
    {
        class NodeType
        {
            public List<String> SuperTypes = new List<String>();
            public Dictionary<String, String> RefAttrToType = new Dictionary<String, String>();
        }

        IGraph graph;
        Dictionary<String, NodeType> typeMap = new Dictionary<String, NodeType>();
        Dictionary<String, INode> nodeMap = new Dictionary<String, INode>();
        Dictionary<String, Dictionary<String, int>> enumToLiteralToValue = new Dictionary<String, Dictionary<String, int>>();

        /// <summary>
        /// Creates a new graph from the given ECore metamodel.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported.</param>
        /// <param name="ecoreFilename">The ECore model specification file.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        public static IGraph Import(String importFilename, String ecoreFilename, IBackend backend)
        {
            ECoreImport imported = new ECoreImport();
            imported.graph = imported.ImportModel(ecoreFilename, backend);
            imported.ImportGraph(importFilename);
            return imported.graph;
        }

        private IGraph ImportModel(String ecoreFilename, IBackend backend)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ecoreFilename);

            XmlElement xmielem = doc["xmi:XMI"];
            if(xmielem == null)
                throw new Exception("The document has no xmi:XMI element.");

            StringBuilder sb = new StringBuilder();

            foreach(XmlElement package in xmielem.GetElementsByTagName("ecore:EPackage"))
            {
                String packageName = package.GetAttribute("nsPrefix");
                foreach(XmlElement classifier in package.GetElementsByTagName("eClassifiers"))
                {
                    String classifierType = classifier.GetAttribute("xsi:type");
                    String classifierName = classifier.GetAttribute("name");

                    switch(classifierType)
                    {
                        case "ecore:EClass":
                            ParseEClass(sb, xmielem, packageName, classifier, classifierName);
                            break;
                        case "ecore:EEnum":
                            ParseEEnum(sb, xmielem, packageName, classifier, classifierName);
                            break;
                    }
                }
            }

            String modelfilename = ecoreFilename.Substring(0, ecoreFilename.LastIndexOf('.')) + "__ecore.gm";
            using(StreamWriter writer = new StreamWriter(modelfilename))
                writer.Write(sb.ToString());

            return backend.CreateGraph(modelfilename, "defaultname");
        }

        private String GetGrGenTypeName(String xmitypename, XmlElement xmielem, String packageDelim)
        {
            // xmitypename has the syntax "#/<number>/<typename>"
            int lastSlashPos = xmitypename.LastIndexOf('/');

            int rootIndex = int.Parse(xmitypename.Substring(2, lastSlashPos - 2));

            // Remove "#/<number>/" from begining of xmitypename
            xmitypename = xmitypename.Substring(lastSlashPos + 1);

            // Handle some GrGen primitive types
            // All others are considered as normal types
            switch(xmitypename)
            {
                case "String": xmitypename = "string"; break;
                case "Boolean": xmitypename = "boolean"; break;
                case "Integer": xmitypename = "int"; break;
                default:
                {
                    XmlElement packageNode = (XmlElement) xmielem.ChildNodes[rootIndex];
                    String packageName = packageNode.GetAttribute("nsPrefix");
                    xmitypename = packageName + packageDelim + xmitypename;
                    break;
                }
            }
            return xmitypename;
        }

        private String GetGrGenTypeName(String xmitypename, XmlElement xmielem)
        {
            return GetGrGenTypeName(xmitypename, xmielem, "_");
        }

        private void ParseEClass(StringBuilder sb, XmlElement xmielem, String packageName, XmlElement classifier, String classifierName)
        {
            bool first;
            NodeType nodeType = new NodeType();

            String abstractStr = classifier.GetAttribute("abstract");
            if(abstractStr == "true")
                sb.Append("abstract ");
            sb.Append("node class " + packageName + "_" + classifierName);

            typeMap[packageName + ":" + classifierName] = nodeType;

            String superTypePathsStr = classifier.GetAttribute("eSuperTypes");
            if(superTypePathsStr != "")
            {
                sb.Append(" extends ");

                String[] superTypePaths = superTypePathsStr.Split(' ');
                first = true;
                foreach(String superType in superTypePaths)
                {
                    if(first) first = false;
                    else sb.Append(", ");

                    String name = GetGrGenTypeName(superType, xmielem, ":");
                    nodeType.SuperTypes.Add(name);
                    sb.Append(name.Replace(':', '_'));
                }
            }

            // First iterate over all ecore:EAttribute structural features
            first = true;
            foreach(XmlElement item in classifier.GetElementsByTagName("eStructuralFeatures"))
            {
                String itemType = item.GetAttribute("xsi:type");
                if(itemType == "ecore:EAttribute")
                {
                    if(first)
                    {
                        sb.Append(" {\n");
                        first = false;
                    }

                    String attrName = item.GetAttribute("name");
                    String attrType = item.GetAttribute("eType");
                    attrType = GetGrGenTypeName(attrType, xmielem);

                    sb.Append("\t_" + attrName + ":" + attrType + ";\n");
                }
            }
            if(first) sb.Append(";\n\n");
            else sb.Append("}\n\n");

            // Then iterate over all ecore:EReference structural features modelled as edge types
            foreach(XmlElement item in classifier.GetElementsByTagName("eStructuralFeatures"))
            {
                String itemType = item.GetAttribute("xsi:type");
                if(itemType == "ecore:EReference")
                {
                    String refName = item.GetAttribute("name");
                    String refType = item.GetAttribute("eType");

                    sb.Append("edge class " + packageName + "_" + classifierName + "_" + refName + ";\n\n");

                    nodeType.RefAttrToType[refName] = GetGrGenTypeName(refType, xmielem, ":");
                }
            }
        }

        private void ParseEEnum(StringBuilder sb, XmlElement xmielem, String packageName, XmlElement classifier, String classifierName)
        {
            Dictionary<String, int> literalToValue = new Dictionary<String, int>();

            String enumTypeName = packageName + "_" + classifierName;

            sb.Append("enum " + enumTypeName + " {\n");
            bool first = true;
            foreach(XmlElement item in classifier.GetElementsByTagName("eLiterals"))
            {
                if(first) first = false;
                else sb.Append(",\n");

                String name = item.GetAttribute("name");
                String value = item.GetAttribute("value");

                sb.Append("\t_" + name + " = " + value);

                int val = int.Parse(value);
                literalToValue[name] = val;

                String literal = item.GetAttribute("literal");
                if(literal != "") literalToValue[literal] = val;
            }
            sb.Append("\n}\n\n");

            enumToLiteralToValue[enumTypeName] = literalToValue;
        }

        private void ImportGraph(String importFilename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(importFilename);

            // First pass: build the nodes and the parent edges given by nesting
            foreach(XmlNode node in doc.ChildNodes)
            {
                if(!(node is XmlElement)) continue;
                XmlElement elem = (XmlElement) node;

                String tagName = elem.Name;
                String grgenTypeName = tagName.Replace(':', '_');
                INode gnode = graph.AddNode(graph.Model.NodeModel.GetType(grgenTypeName));

                String id = elem.GetAttribute("xmi:id");
                nodeMap[id] = gnode;

                ParseNodeFirstPass(elem, gnode, tagName);
            }

            // Second pass: assign the attributes and edges given by reference attributes
            foreach(XmlNode node in doc.ChildNodes)
            {
                if(!(node is XmlElement)) continue;
                XmlElement elem = (XmlElement) node;

                ParseNodeSecondPass(elem, elem.Name);
            }
        }

        private String FindRefTypeName(String parentTypeName, String tagName)
        {
            NodeType nodeType = typeMap[parentTypeName];
            String attrType;
            if(!nodeType.RefAttrToType.TryGetValue(tagName, out attrType))
            {
                foreach(String superType in nodeType.SuperTypes)
                {
                    attrType = FindRefTypeName(superType, tagName);
                    if(attrType != null) return attrType;
                }
            }
            return attrType;
        }

        private String FindContainingTypeName(String parentTypeName, String tagName)
        {
            NodeType nodeType = typeMap[parentTypeName];
            if(nodeType.RefAttrToType.ContainsKey(tagName)) return parentTypeName;

            foreach(String superType in nodeType.SuperTypes)
            {
                String containingType = FindContainingTypeName(superType, tagName);
                if(containingType != null) return containingType;
            }
            return null;
        }

        private void ParseNodeFirstPass(XmlElement parent, INode parentNode, String parentTypeName)
        {
            foreach(XmlNode node in parent.ChildNodes)
            {
                if(!(node is XmlElement)) continue;
                XmlElement elem = (XmlElement) node;

                String tagName = elem.Name;

                String typeName = elem.GetAttribute("xsi:type");
                if(typeName == "")
                {
                    String qualAttr = parentTypeName + "." + tagName;
                    typeName = FindRefTypeName(parentTypeName, tagName);

                    if(typeName == null)
                    {
                        // Treat it as an attribute
                        AssignAttribute(parentNode, tagName, elem.InnerText);
                        continue;
                    }
                }
                String grgenTypeName = typeName.Replace(':', '_');
                INode gnode = graph.AddNode(graph.Model.NodeModel.GetType(grgenTypeName));

                String id = elem.GetAttribute("xmi:id");
                nodeMap[id] = gnode;

                String edgeTypeName = FindContainingTypeName(parentTypeName, tagName);
                String grgenEdgeTypeName = edgeTypeName.Replace(':', '_') + "_" + tagName;
                graph.AddEdge(graph.Model.EdgeModel.GetType(grgenEdgeTypeName), parentNode, gnode);

                ParseNodeFirstPass(elem, gnode, typeName);
            }
        }

        private void ParseNodeSecondPass(XmlElement curElem, String curTypeName)
        {
            String id = curElem.GetAttribute("xmi:id");
            if(id == "") return;        // probably an attribute in element form (already handled), so ignore

            INode curNode = nodeMap[id];

            foreach(XmlAttribute attr in curElem.Attributes)
            {
                if(attr.Name.StartsWith("xmi:") || attr.Name == "xsi:type" || attr.Name.StartsWith("xmlns:")) continue;

                String attrRefType = FindRefTypeName(curTypeName, attr.Name);
                if(attrRefType != null)
                {
                    String refEdgeTypeName = FindContainingTypeName(curTypeName, attr.Name);
                    String grgenRefEdgeTypeName = refEdgeTypeName.Replace(':', '_') + "_" + attr.Name;

                    String[] destNodeNames = attr.Value.Split(' ');
                    foreach(String destNodeName in destNodeNames)
                        graph.AddEdge(graph.Model.EdgeModel.GetType(grgenRefEdgeTypeName), curNode, nodeMap[destNodeName]);
                }
                else
                {
                    AssignAttribute(curNode, attr.Name, attr.Value);
                }
            }

            foreach(XmlNode childXmlNode in curElem.ChildNodes)
            {
                if(!(childXmlNode is XmlElement)) continue;
                XmlElement childElem = (XmlElement) childXmlNode;

                String tagName = childElem.Name;
                String typeName = childElem.GetAttribute("xsi:type");
                if(typeName == "")
                {
                    String qualAttr = curTypeName + "." + tagName;
                    typeName = FindRefTypeName(curTypeName, tagName);
                }

                ParseNodeSecondPass(childElem, typeName);
            }
        }

        private void AssignAttribute(INode node, String attrname, String attrval)
        {
            AttributeType attrType = node.Type.GetAttributeType("_" + attrname);

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
                    if(Int32.TryParse(attrval, out val)) value = val;
                    else value = enumToLiteralToValue[attrType.EnumType.Name][attrval];
                    break;
                }

                case AttributeKind.IntegerAttr:
                {
                    int val;
                    if(!Int32.TryParse(attrval, out val))
                        throw new Exception("Attribute \"" + attrname + "\" must be an integer!");
                    value = val;
                    break;
                }

                case AttributeKind.StringAttr:
                    value = attrval;
                    break;

                case AttributeKind.FloatAttr:
                {
                    float val;
                    if(!Single.TryParse(attrval, out val))
                        throw new Exception("Attribute \"" + attrname + "\" must be a floating point number!");
                    value = val;
                    break;
                }

                case AttributeKind.DoubleAttr:
                {
                    double val;
                    if(!Double.TryParse(attrval, out val))
                        throw new Exception("Attribute \"" + attrname + "\" must be a floating point number!");
                    value = val;
                    break;
                }

                case AttributeKind.ObjectAttr:
                    throw new Exception("Attribute \"" + attrname + "\" is an object type attribute!\n"
                        + "It is not possible to assign a value to an object type attribute!");

                case AttributeKind.SetAttr:
                case AttributeKind.MapAttr:
                default:
                    throw new Exception("Unsupported attribute value type: \"" + attrType.Kind + "\"");
            }

            node.SetAttribute("_" + attrname, value);
        }
    }
}
