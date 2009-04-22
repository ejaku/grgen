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
        /// <summary>
        /// Creates a new graph from the given ECore metamodel.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported.</param>
        /// <param name="modelOverride">If not null, overrides the filename of the graph model to be used.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        public static IGraph Import(String importFilename, String modelOverride, IBackend backend)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(importFilename);

            XmlElement xmielem = doc["xmi:XMI"];
            if(xmielem == null)
                throw new Exception("The document has no xmi:XMI element.");

            StringBuilder sb = new StringBuilder();

            foreach(XmlElement package in xmielem.GetElementsByTagName("ecore:EPackage"))
            {
                String packageName = package.GetAttribute("name");
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

            String modelfilename = importFilename.Substring(0, importFilename.LastIndexOf('.')) + "__ecore.gm";
            using(StreamWriter writer = new StreamWriter(modelfilename))
                writer.Write(sb.ToString());

            return backend.CreateGraph(modelfilename, "defaultname");
        }

        private static String GetGrGenTypeName(String xmitypename, XmlElement xmielem)
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
                    String packageName = packageNode.GetAttribute("name");
                    xmitypename = packageName + "_" + xmitypename;
                    break;
                }
            }
            return xmitypename;
        }

        private static void ParseEClass(StringBuilder sb, XmlElement xmielem, String packageName, XmlElement classifier, String classifierName)
        {
            bool first;

            String abstractStr = classifier.GetAttribute("abstract");
            if(abstractStr == "true")
                sb.Append("abstract ");
            sb.Append("node class " + packageName + "_" + classifierName);

            String superTypesStr = classifier.GetAttribute("eSuperTypes");
            if(superTypesStr != "")
            {
                sb.Append(" extends ");

                String[] superTypes = superTypesStr.Split(' ');
                first = true;
                foreach(String superType in superTypes)
                {
                    if(first) first = false;
                    else sb.Append(", ");

                    String name = GetGrGenTypeName(superType, xmielem);
                    sb.Append(name);
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

                    sb.Append("edge class " + packageName + "_" + classifierName + "_" + refName + ";\n\n");
                }
            }
        }

        private static void ParseEEnum(StringBuilder sb, XmlElement xmielem, String packageName, XmlElement classifier, String classifierName)
        {
            sb.Append("enum " + packageName + "_" + classifierName + " {\n");
            bool first = true;
            foreach(XmlElement item in classifier.GetElementsByTagName("eLiterals"))
            {
                if(first) first = false;
                else sb.Append(",\n");
                String name = item.GetAttribute("name");
                String value = item.GetAttribute("value");

                sb.Append("\t_" + name + " = " + value);
            }
            sb.Append("\n}\n\n");
        }
    }
}
