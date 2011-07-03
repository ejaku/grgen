/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

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
        class RefType
        {
            public bool Ordered;
            public String TypeName;

            public RefType(bool ordered, String typeName)
            {
                Ordered = ordered;
                TypeName = typeName;
            }
        }

        class NodeType
        {
            public List<String> SuperTypes = new List<String>();
            public Dictionary<String, RefType> RefAttrToRefType = new Dictionary<String, RefType>();
        }

        // document hierarchical structure with the entities neeed to parse references of the form
        // /index of child referenced (zero based count over children)
        // /@element of child referenced.index of such-typed child (zero based count over children of that element)
        // /name of child referenced
        class XMLTree
        {
            public string element; // the opening tag
            public string elementName; // the name attribute
            public INode elementNode; // the graph node this element corrsponds to

            public List<XMLTree> children;
        }

        /// <summary>
        /// no package name mangling into entities if true; requires names to be unique
        /// </summary>
        bool noPackageNamePrefix;

        /// <summary>
        /// the graph we build by importing
        /// </summary>
        IGraph graph;

        /// <summary>
        /// map of package prefixed type name to graph node type
        /// </summary>
        Dictionary<String, NodeType> typeMap = new Dictionary<String, NodeType>();
        
        /// <summary>
        /// map of xmi:id to the graph node it denotes
        /// might be empty for documents which don't use ids to reference elements but paths
        /// </summary>
        Dictionary<String, INode> nodeMap = new Dictionary<String, INode>();

        /// <summary>
        /// root of the xml tree hierarchy
        /// needed to decode path references for documents not using xmi:id to reference elements
        /// </summary>
        XMLTree root;

        /// <summary>
        /// enum type definitions
        /// </summary>
        Dictionary<String, Dictionary<String, int>> enumToLiteralToValue = new Dictionary<String, Dictionary<String, int>>();

        /// <summary>
        /// Creates a new graph from the given ECore metamodels.
        /// If a grg file is given, the graph will use the graph model declared in it and the according
        /// actions object will be associated to the graph.
        /// If a xmi file is given, the model instance will be imported into the graph.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="ecoreFilenames">A list of ECore model specification files. It must at least contain one element.</param>
        /// <param name="grgFilename">A grg file to be used to create the graph, or null.</param>
        /// <param name="xmiFilename">The filename of the model instance to be imported, or null.</param>
        /// <param name="noPackageNamePrefix">Prefix the types with the name of the package? Can only be used if names from the packages are disjoint.</param>
        public static IGraph Import(IBackend backend, List<String> ecoreFilenames, String grgFilename, String xmiFilename, bool noPackageNamePrefix)
        {
            ECoreImport imported = new ECoreImport();
            imported.noPackageNamePrefix = noPackageNamePrefix;
            imported.graph = imported.ImportModels(ecoreFilenames, grgFilename, backend);
            if(xmiFilename != null)
            {
                Console.WriteLine("Importing graph...");
                imported.ImportGraph(xmiFilename);
            }
            return imported.graph;
        }

        private IGraph ImportModels(List<String> ecoreFilenames, String grgFilename, IBackend backend)
        {
            foreach(String ecoreFilename in ecoreFilenames)
            {
                String modelText = ParseModel(ecoreFilename);

                String modelfilename = ecoreFilename.Substring(0, ecoreFilename.LastIndexOf('.')) + "__ecore.gm";

                // Do we have to update the model file (.gm)?
                if(!File.Exists(modelfilename) || File.GetLastWriteTime(ecoreFilename) > File.GetLastWriteTime(modelfilename))
                {
                    Console.WriteLine("Writing model file \"" + modelfilename + "\"...");
                    using(StreamWriter writer = new StreamWriter(modelfilename))
                        writer.Write(modelText);
                }
            }

            if(grgFilename == null)
            {
                grgFilename = "";
                foreach(String ecoreFilename in ecoreFilenames)
                    grgFilename += ecoreFilename.Substring(0, ecoreFilename.LastIndexOf('.')) + "_";
                grgFilename += "_ecore.grg";

                StringBuilder sb = new StringBuilder();
                sb.Append("// Automatically generated\n// Do not change, changes will be lost!\n\nusing ");

                DateTime grgTime;
                if(!File.Exists(grgFilename)) grgTime = DateTime.MinValue;
                else grgTime = File.GetLastWriteTime(grgFilename);

                bool mustWriteGrg = false;

                bool first = true;
                foreach(String ecore in ecoreFilenames)
                {
                    if(first) first = false;
                    else sb.Append(", ");
                    sb.Append(ecore.Substring(0, ecore.LastIndexOf('.')) + "__ecore");

                    if(File.GetLastWriteTime(ecore) > grgTime)
                        mustWriteGrg = true;
                }

                if(mustWriteGrg)
                {
                    sb.Append(";\n");
                    using(StreamWriter writer = new StreamWriter(grgFilename))
                        writer.Write(sb.ToString());
                }
            }

            IGraph graph;
            BaseActions actions;
            backend.CreateFromSpec(grgFilename, "defaultname", ProcessSpecFlags.UseNoExistingFiles, new List<String>(), out graph, out actions);
            graph.Actions = actions;
            return graph;
        }

        private String ParseModel(String ecoreFilename)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("// Automatically generated from \"" + ecoreFilename + "\"\n// Do not change, changes will be lost!\n\n");

            XmlDocument doc = new XmlDocument();
            doc.Load(ecoreFilename);

            XmlElement xmielem = doc["xmi:XMI"];
            if(xmielem == null)
            {
                // Parse the ECore file, create the model file content, and collect important infos used to import a model instance
                foreach(XmlElement package in doc.GetElementsByTagName("ecore:EPackage"))
                    ParsePackageContent(package, sb, null);
            }
            else
            {
                // Parse the ECore file, create the model file content, and collect important infos used to import a model instance
                foreach(XmlElement package in xmielem.GetElementsByTagName("ecore:EPackage"))
                    ParsePackageContent(package, sb, xmielem);
            }

            return sb.ToString();
        }

        private void ParsePackageContent(XmlElement package, StringBuilder sb, XmlElement xmielem)
        {
            foreach(XmlElement classifier in package.GetElementsByTagName("eClassifiers"))
            {
                String classifierType = classifier.GetAttribute("xsi:type");
                String classifierName = classifier.GetAttribute("name");

                switch(classifierType)
                {
                case "ecore:EClass":
                    ParseEClass(sb, xmielem, package, classifier, classifierName);
                    break;
                case "ecore:EEnum":
                    ParseEEnum(sb, xmielem, package, classifier, classifierName);
                    break;
                }
            }
        }

        private String GetGrGenTypeName(String xmitypename, XmlElement xmielem, XmlElement package, String packageDelim)
        {
            // xmitypename has the syntax "#/<number>/<typename>"; number may be empty in case the file contains only one package
            int lastSlashPos = xmitypename.LastIndexOf('/');
            int slashPosBeforeLastSlashPos = xmitypename.Substring(0, lastSlashPos).LastIndexOf('/');

            String fullXmiTypeName = xmitypename;

            // Remove "#/<number>/" from begining of xmitypename
            xmitypename = xmitypename.Substring(lastSlashPos + 1);

            // Handle some GrGen primitive types
            // All others are considered as normal types
            switch(xmitypename)
            {
            case "String": xmitypename = "string"; break;
            case "EChar": xmitypename = "string"; break;
            case "EString": xmitypename = "string"; break;
            case "ECharacterObject": xmitypename = "string"; break;
            case "Boolean": xmitypename = "boolean"; break;
            case "EBoolean": xmitypename = "boolean"; break;
            case "EBooleanObject": xmitypename = "boolean"; break;
            case "Integer": xmitypename = "int"; break;
            case "EInteger": xmitypename = "int"; break;
            case "EInt": xmitypename = "int"; break;
            case "EBigInteger": xmitypename = "int"; break;
            case "EIntegerObject": xmitypename = "int"; break;
            case "EBigDecimal": xmitypename = "int"; break;
            case "UnlimitedNatural": xmitypename = "int"; break;
            case "EShort": xmitypename = "int"; break;
            case "EShortObject": xmitypename = "int"; break;
            case "EFloat": xmitypename = "float"; break;
            case "EFloatObject": xmitypename = "float"; break;
            case "EDouble": xmitypename = "double"; break;
            case "EDoubleObject": xmitypename = "double"; break;
            case "EByte": xmitypename = "byte"; break; // nyi
            case "EByteObject": xmitypename = "byte"; break; // nyi
            case "ELong": xmitypename = "long"; break; // nyi
            case "ELongObject": xmitypename = "long"; break; // nyi
            default:
                {
                    XmlElement packageNode = package;
                    if(xmielem != null) {
                        int rootIndex = int.Parse(fullXmiTypeName.Substring(slashPosBeforeLastSlashPos+1, lastSlashPos-(slashPosBeforeLastSlashPos+1)));
                        packageNode = (XmlElement)xmielem.ChildNodes[rootIndex];
                    }

                    if(noPackageNamePrefix) {
                        xmitypename = packageDelim + xmitypename;
                    } else {
                        String packageName = packageNode.GetAttribute("nsPrefix");
                        xmitypename = packageName + packageDelim + xmitypename;
                    }
                    break;
                }
            }

            return xmitypename;
        }

        private String GetGrGenTypeName(String xmitypename, XmlElement xmielem, XmlElement package)
        {
            return GetGrGenTypeName(xmitypename, xmielem, package, "_");
        }

        private void ParseEClass(StringBuilder sb, XmlElement xmielem, XmlElement package, XmlElement classifier, String classifierName)
        {
            String packageName = package.GetAttribute("nsPrefix");
            if(noPackageNamePrefix) packageName = "";

            bool first;
            NodeType nodeType = new NodeType();

            String abstractStr = classifier.GetAttribute("abstract");
            if(abstractStr == "true")
                sb.Append("abstract ");
            sb.Append("node class " + packageName.Replace('.', '_') + "_" + classifierName);

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

                    String name = GetGrGenTypeName(superType, xmielem, package, ":");
                    nodeType.SuperTypes.Add(name);
                    sb.Append(name.Replace(':', '_').Replace('.', '_'));
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
                    if(attrType == "")
                    {
                        foreach(XmlElement typeItem in classifier.GetElementsByTagName("eType"))
                        {
                            attrType = typeItem.GetAttribute("href");
                            break;
                        }
                    }
                    attrType = GetGrGenTypeName(attrType, xmielem, package);

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

                    String edgeTypeName = packageName + "_" + classifierName + "_" + refName;
                    String typeName = GetGrGenTypeName(refType, xmielem, package, ":");
                    bool ordered = item.GetAttribute("ordered") != "false";          // Default is ordered

                    sb.Append("edge class " + edgeTypeName.Replace('.', '_'));
                    if(ordered)
                        sb.Append(" {\n\tindex:int;\n}\n\n");
                    else
                        sb.Append(";\n\n");

                    nodeType.RefAttrToRefType[refName] = new RefType(ordered, typeName);
                }
            }
        }

        private void ParseEEnum(StringBuilder sb, XmlElement xmielem, XmlElement package, XmlElement classifier, String classifierName)
        {
            String packageName = package.GetAttribute("nsPrefix");
            if(noPackageNamePrefix) packageName = "";

            Dictionary<String, int> literalToValue = new Dictionary<String, int>();

            String enumTypeName = packageName + "_" + classifierName;

            sb.Append("enum " + enumTypeName.Replace('.', '_') + " {\n");
            bool first = true;
            foreach(XmlElement item in classifier.GetElementsByTagName("eLiterals"))
            {
                if(first) first = false;
                else sb.Append(",\n");

                String name = item.GetAttribute("name");
                String value = item.GetAttribute("value");

                if(value != "")
                {
                    sb.Append("\t_" + name + " = " + value);

                    int val = int.Parse(value);
                    literalToValue[name] = val;

                    String literal = item.GetAttribute("literal");
                    if(literal != "") literalToValue[literal] = val;
                }
                else
                    sb.Append("\t_" + name);
            }
            sb.Append("\n}\n\n");

            enumToLiteralToValue[enumTypeName] = literalToValue;
        }

        private void ImportGraph(String importFilename)
        {
            // First pass: build the nodes and the parent edges given by nesting
            using(XmlTextReader reader = new XmlTextReader(importFilename))
            {
                while(reader.Read()) // handle root node
                {
                    if(reader.NodeType != XmlNodeType.Element) continue;

                    root = new XMLTree();
                    if(reader.Name == "xmi:XMI") // if root node is the xmi node we've to read the first level of nodes so we got real graph nodes we can hand down
                    {
                        // relevant for hierarchy but not a real graph node to be created from it
                        root.element = reader.Name;
                        root.elementName = null;
                        root.elementNode = null;
                        root.children = new List<XMLTree>();

                        while(reader.Read())
                        {
                            if(reader.NodeType != XmlNodeType.Element)
                                continue;

                            // create real graph node from it
                            XMLTree rootChild = new XMLTree();
                            String tagName = reader.Name;
                            if(noPackageNamePrefix) 
                                tagName = tagName.Substring(tagName.LastIndexOf(":"));
                            rootChild.element = tagName;
                            if(reader.MoveToAttribute("name"))
                                rootChild.elementName = reader.Value;
                            String grgenTypeName = tagName.Replace(':', '_').Replace(':', '_');
                            INode gnode = graph.AddNode(graph.Model.NodeModel.GetType(grgenTypeName));
                            rootChild.elementNode = gnode;
                            rootChild.children = new List<XMLTree>();
                            root.children.Add(rootChild);

                            if(reader.MoveToAttribute("xmi:id"))
                                nodeMap[reader.Value] = gnode;

                            ParseNodeFirstPass(reader, rootChild, tagName); // descend and munch subtree
                        }
                    }
                    else
                    {
                        // create real graph node from it
                        String tagName = reader.Name;
                        if(noPackageNamePrefix)
                            tagName = tagName.Substring(tagName.LastIndexOf(":"));
                        root.element = tagName;
                        if(reader.MoveToAttribute("name"))
                            root.elementName = reader.Value;
                        String grgenTypeName = tagName.Replace(':', '_').Replace(':', '_');
                        INode gnode = graph.AddNode(graph.Model.NodeModel.GetType(grgenTypeName));
                        root.elementNode = gnode;
                        root.children = new List<XMLTree>();

                        if(reader.MoveToAttribute("xmi:id"))
                            nodeMap[reader.Value] = gnode;

                        ParseNodeFirstPass(reader, root, tagName); // descend and munch subtree
                    }
                }
            }

            // Second pass: assign the attributes and edges given by reference attributes
            using(XmlTextReader reader = new XmlTextReader(importFilename))
            {
                while(reader.Read())
                {
                    if(reader.NodeType != XmlNodeType.Element) continue;

                    ParseNodeSecondPass(reader, root, reader.Name);
                }
            }
        }

        private RefType FindRefType(String parentTypeName, String tagName)
        {
            NodeType nodeType = typeMap[parentTypeName];

            RefType refType;
            if(nodeType.RefAttrToRefType.TryGetValue(tagName, out refType)) return refType;

            foreach(String superType in nodeType.SuperTypes)
            {
                refType = FindRefType(superType, tagName);
                if(refType != null) return refType;
            }
            return null;
        }

        private String FindRefTypeName(String parentTypeName, String tagName)
        {
            RefType refType = FindRefType(parentTypeName, tagName);
            if(refType == null) return null;
            else return refType.TypeName;
        }

        private bool IsRefOrdered(String parentTypeName, String tagName)
        {
            return FindRefType(parentTypeName, tagName).Ordered;
        }

        private String FindContainingTypeName(String parentTypeName, String tagName)
        {
            NodeType nodeType = typeMap[parentTypeName];

            if(nodeType.RefAttrToRefType.ContainsKey(tagName)) return parentTypeName;

            foreach(String superType in nodeType.SuperTypes)
            {
                String containingType = FindContainingTypeName(superType, tagName);
                if(containingType != null) return containingType;
            }
            return null;
        }

        INode GetNode(String name)
        {
            if(nodeMap.ContainsKey(name))
                return nodeMap[name];

            XMLTree curPos = root;
            String[] addressParts = name.Split('/');
            for(int i=1; i<addressParts.Length; ++i) // first is ignored
            {
                XMLTree oldCurPos = curPos;
                String part = addressParts[i];
                int index;
                if(part.Length == 0)
                {
                    continue;
                }
                else if(part.StartsWith("@"))
                {
                    String element = part.Substring(1);
                    index = 0;

                    if(part.LastIndexOf('.') != -1)
                    {
                        element = part.Substring(1, part.LastIndexOf('.') - 1);
                        index = int.Parse(part.Substring(part.LastIndexOf('.') + 1));
                    }

                    int counter = -1;
                    foreach(XMLTree child in curPos.children)
                    {
                        if(child.element == element)
                        {
                            ++counter;
                            if(counter == index)
                            {
                                curPos = child;
                                break;
                            }
                        }
                    }
                }
                else if(int.TryParse(part, out index))
                {
                    curPos = curPos.children[index];
                }
                else
                {
                    foreach(XMLTree child in curPos.children)
                        if(child.elementName == part)
                        {
                            curPos = child;
                            break;
                        }
                }
                if(oldCurPos == curPos)
                    throw new Exception("Can't find address " + name + ", stop at part " + part);
            }

            return curPos.elementNode;
        }

        private void ParseNodeFirstPass(XmlTextReader reader, XMLTree parentNode, String parentTypeName)
        {
            INodeModel nodeModel = graph.Model.NodeModel;
            IEdgeModel edgeModel = graph.Model.EdgeModel;

            Dictionary<String, int> tagNameToNextIndex = new Dictionary<String, int>();

            while(reader.Read())
            {
                if(reader.NodeType == XmlNodeType.EndElement) break; // reached end of current nesting level
                if(reader.NodeType != XmlNodeType.Element) continue;

                bool emptyElem = reader.IsEmptyElement;
                String tagName = reader.Name;
                String id = null;
                if(reader.MoveToAttribute("xmi:id"))
                    id = reader.Value;
                String elementName = null;
                if(reader.MoveToAttribute("name"))
                    elementName = reader.Value;
                String typeName = null;
                if(reader.MoveToAttribute("xsi:type"))
                    typeName = reader.Value;
                else if(reader.MoveToAttribute("xmi:type"))
                    typeName = reader.Value;
                else
                {
                    typeName = FindRefTypeName(parentTypeName, tagName);

                    if(typeName == null)
                    {
                        // Treat it as an attribute
                        AssignAttribute(parentNode.elementNode, tagName, reader.ReadInnerXml());

                        XMLTree attributeChild = new XMLTree();
                        attributeChild.elementNode = null;
                        attributeChild.elementName = elementName;
                        attributeChild.element = tagName;
                        attributeChild.children = null;
                        parentNode.children.Add(attributeChild);
                        continue;
                    }
                }

                if(noPackageNamePrefix) typeName = typeName.Substring(typeName.LastIndexOf(":"));
                String grgenTypeName = typeName.Replace(':', '_').Replace('.', '_');
                INode gnode = graph.AddNode(nodeModel.GetType(grgenTypeName));
                XMLTree child = new XMLTree();
                child.elementNode = gnode;
                child.elementName = elementName;
                child.element = tagName;
                child.children = new List<XMLTree>();
                parentNode.children.Add(child);
                if(id != null)
                    nodeMap[id] = gnode;

                String edgeTypeName = FindContainingTypeName(parentTypeName, tagName);
                if(noPackageNamePrefix) edgeTypeName = edgeTypeName.Substring(edgeTypeName.LastIndexOf(":"));
                String grgenEdgeTypeName = edgeTypeName.Replace(':', '_').Replace('.', '_') + "_" + tagName;
                IEdge parentEdge = graph.AddEdge(edgeModel.GetType(grgenEdgeTypeName), parentNode.elementNode, gnode);
                if(IsRefOrdered(parentTypeName, tagName))
                {
                    int nextIndex;
                    tagNameToNextIndex.TryGetValue(tagName, out nextIndex);
                    parentEdge.SetAttribute("index", nextIndex);
                    tagNameToNextIndex[tagName] = nextIndex + 1;
                }

                if(!emptyElem)
                    ParseNodeFirstPass(reader, child, typeName);
            }
        }

        private void ParseNodeSecondPass(XmlTextReader reader, XMLTree parentNode, String curTypeName)
        {
            if(noPackageNamePrefix) curTypeName = curTypeName.Substring(curTypeName.LastIndexOf(":"));

            if(curTypeName != "xmi:XMI") // happens on/if first node is xmi:XMI
                HandleAttributes(reader, curTypeName, parentNode.elementNode);

            int index = 0;
            while(reader.Read())
            {
                if(reader.NodeType == XmlNodeType.EndElement) break;
                if(reader.NodeType != XmlNodeType.Element) continue;

                bool emptyElem = reader.IsEmptyElement;
                String tagName = reader.Name;

                XMLTree child = null;
                if(parentNode != null)
                {
                    if(parentNode.children == null)
                    {
                        ++index;
                        continue;
                    }
                    child = parentNode.children[index];
                }

                String typeName;
                if(curTypeName == "xmi:XMI")
                    typeName = tagName;
                else if(reader.MoveToAttribute("xsi:type"))
                    typeName = reader.Value;
                else if(reader.MoveToAttribute("xmi:type"))
                    typeName = reader.Value;
                else
                    typeName = FindRefTypeName(curTypeName, tagName);

                if(!emptyElem)
                    ParseNodeSecondPass(reader, child, typeName);
                else
                    HandleAttributes(reader, typeName, child.elementNode); 
                ++index;
            }
        }

        private void HandleAttributes(XmlTextReader reader, String curTypeName, INode curNode)
        {
            for(int attrIndex = 0; attrIndex < reader.AttributeCount; attrIndex++)
            {
                reader.MoveToAttribute(attrIndex);

                // skip attributes not giving references=edges
                String name = reader.Name;
                if(name.StartsWith("xmi:") || name == "xsi:type" || name == "xsi:schemaLocation" || name.StartsWith("xmlns:"))
                    continue;

                String attrRefType = FindRefTypeName(curTypeName, name);
                if(attrRefType != null)
                {
                    // List of references as in attribute separated by spaces

                    String refEdgeTypeName = FindContainingTypeName(curTypeName, name);
                    String grgenRefEdgeTypeName = refEdgeTypeName.Replace(':', '_').Replace('.', '_') + "_" + name;

                    IEdgeModel edgeModel = graph.Model.EdgeModel;
                    String[] destNodeNames = reader.Value.Split(' ');
                    if(IsRefOrdered(curTypeName, name))
                    {
                        int i = 0;
                        foreach(String destNodeName in destNodeNames)
                        {
                            IEdge parentEdge = graph.AddEdge(edgeModel.GetType(grgenRefEdgeTypeName), curNode, GetNode(destNodeName));
                            parentEdge.SetAttribute("index", i);
                            i++;
                        }
                    }
                    else
                    {
                        foreach(String destNodeName in destNodeNames)
                            graph.AddEdge(edgeModel.GetType(grgenRefEdgeTypeName), curNode, GetNode(destNodeName));
                    }
                }
                else
                {
                    if(name == "href") // skip href attributes
                        continue;
                    AssignAttribute(curNode, name, reader.Value);
                }
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
                    // TODO: there might be literals without values, we must cope with them
                    int val;
                    if(Int32.TryParse(attrval, out val)) value = val;
                    else value = enumToLiteralToValue[attrType.EnumType.Name][attrval];
                    break;
                }

                case AttributeKind.IntegerAttr:
                {
                    int val;
                    if(!Int32.TryParse(attrval, out val))
                    {
                        Int64 largerVal; // we map BigInt to Int32 as of now, handle that gracefully, TODO: remove after type long was added to GrGen.NET
                        if(!Int64.TryParse(attrval, out largerVal))
                            val = -2147483648; // might be DecimalValue, may be not representable with integers, but for TTC reengineering case we need to import it (although a java program for sure does not need it, gna)
                            //throw new Exception("Attribute \"" + attrname + "\" must be an integer!");
                        else
                            val = 2147483647;
                    }
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
