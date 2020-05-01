/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Reflection;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    // TODO: much of this string handling is scruffy, there should be some type representation used throughout the entire backend
    // (some string/object based runtime type handling needed for interpreted sequences)

    /// <summary>
    /// The TypesHelper in this file contains code that creates or handles .net / C# types, as name strings or Type objects
    /// </summary>
    public static partial class TypesHelper
    {
        /// <summary>
        /// Returns C# type string
        /// </summary>
        public static String TypeName(GrGenType type)
        {
            if(type is VarType)
            {
                Type typeOfVar = ((VarType)type).Type;
                if(typeOfVar.IsGenericType)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(typeOfVar.FullName.Substring(0, typeOfVar.FullName.IndexOf('`')));
                    sb.Append('<');
                    bool first = true;
                    foreach(Type typeArg in typeOfVar.GetGenericArguments())
                    {
                        if(first)
                            first = false;
                        else
                            sb.Append(", ");
                        sb.Append(typeArg.FullName.Replace("+", "."));
                    }
                    sb.Append('>');
                    return sb.ToString();
                }

                return typeOfVar.FullName;
            }
            else
            {
                switch(type.Name)
                {
                case "Node": return "GRGEN_LIBGR.INode";
                case "Edge": return "GRGEN_LIBGR.IDEdge";
                case "UEdge": return "GRGEN_LIBGR.IUEdge";
                case "AEdge": return "GRGEN_LIBGR.IEdge";
                default: return "GRGEN_MODEL." + GetPackagePrefixDot(type.Package) + "I" + type.Name;
                }
            }
        }

        /*
        /// <summary>
        /// Returns the qualified type name for the type name given
        /// </summary>
        public static String GetQualifiedTypeName(String typeName, IGraphModel model)
        {
            if(typeName == "de.unika.ipd.grGen.libGr.SetValueType" || typeName == "SetValueType")
                return "de.unika.ipd.grGen.libGr.SetValueType";
            Type type = GetTypeFromNameForContainer(typeName, model);
            return type != null ? type.Namespace + "." + type.Name : null;
        }*/

        public static String ActionClassForMatchType(String matchType)
        {
            String prefixedRule = matchType.Substring(6, matchType.Length - 6 - 1); // remove match< begin and > end
            if(prefixedRule.Contains("::"))
            {
                String packageName = prefixedRule.Substring(0, prefixedRule.IndexOf(':'));
                String ruleName = prefixedRule.Substring(prefixedRule.LastIndexOf(':') + 1);
                return "GRGEN_ACTIONS." + packageName + ".Action_" + ruleName;
            }
            else
            {
                String ruleName = prefixedRule;
                return "GRGEN_ACTIONS.Action_" + ruleName;
            }
        }

        public static String MatchClassFiltererForMatchClassType(String matchClassType)
        {
            String prefixedMatchClass = matchClassType.Substring(12, matchClassType.Length - 12 - 1); // remove match<class begin and > end
            if(prefixedMatchClass.Contains("::"))
            {
                String packageName = prefixedMatchClass.Substring(0, prefixedMatchClass.IndexOf(':'));
                String matchClassName = prefixedMatchClass.Substring(prefixedMatchClass.LastIndexOf(':') + 1);
                return "GRGEN_ACTIONS." + packageName + ".MatchClassFilterer_" + matchClassName;
            }
            else
            {
                String matchClassName = prefixedMatchClass;
                return "GRGEN_ACTIONS.MatchClassFilterer_" + matchClassName;
            }
        }

        /// <summary>
        /// Returns C# type string with correct namespace prefix for the type given
        /// </summary>
        public static string PrefixedTypeFromType(Type type)
        {
            if(type.Name == "INode") return "GRGEN_LIBGR.INode";
            if(type.Name == "IEdge") return "GRGEN_LIBGR.IEdge";
            if(type.Name == "IDEdge") return "GRGEN_LIBGR.IDEdge";
            if(type.Name == "IUEdge") return "GRGEN_LIBGR.IUEdge";

            if(type.Name == "SetValueType") return "GRGEN_LIBGR.SetValueType";

            if(type.Name == "IGraph") return "GRGEN_LIBGR.IGraph";

            switch(type.Name)
            {
            case "SByte": return "sbyte";
            case "Int16": return "short";
            case "Int32": return "int";
            case "Int64": return "long";
            case "Boolean": return "bool";
            case "Single": return "float";
            case "Double": return "double";
            case "String": return "string";
            case "Object": return "object";
            }

            String fullTypeName = type.FullName;
            fullTypeName = fullTypeName.Substring(19); // remove "de.unika.ipd.grGen."
            fullTypeName = fullTypeName.Substring(fullTypeName.IndexOf('.') + 1); // remove "model_XXX."
            return "GRGEN_MODEL." + fullTypeName;
        }

        /// <summary>
        /// Returns C# type string with correct namespace prefix for the type given
        /// </summary>
        public static string XgrsTypeToCSharpType(string type, IGraphModel model)
        {
            if(type == "Node") return "GRGEN_LIBGR.INode";
            if(type == "AEdge") return "GRGEN_LIBGR.IEdge";
            if(type == "Edge") return "GRGEN_LIBGR.IDEdge";
            if(type == "UEdge") return "GRGEN_LIBGR.IUEdge";

            if(type == "short" || type == "int" || type == "long" || type == "bool" || type == "string" || type == "float" || type == "double" || type == "object") return type;
            if(type == "byte") return "sbyte";
            if(type == "boolean") return "bool";

            if(type.StartsWith("set<") || type.StartsWith("map<")) return "Dictionary<" + XgrsTypeToCSharpType(ExtractSrc(type), model) + "," + XgrsTypeToCSharpType(ExtractDst(type), model) + ">";
            if(type.StartsWith("array<")) return "List<" + XgrsTypeToCSharpType(ExtractSrc(type), model) + ">";
            if(type.StartsWith("deque<")) return "GRGEN_LIBGR.Deque<" + XgrsTypeToCSharpType(ExtractSrc(type), model) + ">";

            if(type == "SetValueType") return "GRGEN_LIBGR.SetValueType";
            if(type == "graph") return "GRGEN_LIBGR.IGraph";

            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.PackagePrefixedName == type)
                    return "GRGEN_MODEL." + (enumAttrType.Package != null ? enumAttrType.Package + "." : "") + "ENUM_" + enumAttrType.Name;
            }

            if(type.StartsWith("match<class "))
            {
                String prefixedMatchClass = type.Substring(12, type.Length - 12 - 1);
                if(prefixedMatchClass.Contains("::"))
                {
                    String packageName = prefixedMatchClass.Substring(0, prefixedMatchClass.IndexOf(':'));
                    String matchClassName = prefixedMatchClass.Substring(prefixedMatchClass.LastIndexOf(':') + 1);
                    return "GRGEN_ACTIONS." + packageName + ".IMatch_" + matchClassName;
                }
                else
                {
                    String matchClassName = prefixedMatchClass;
                    return "GRGEN_ACTIONS.IMatch_" + matchClassName;
                }
            }

            if(type.StartsWith("match<"))
            {
                String prefixedRule = type.Substring(6, type.Length - 6 - 1);
                if(prefixedRule.Contains("::"))
                {
                    String packageName = prefixedRule.Substring(0, prefixedRule.IndexOf(':'));
                    String ruleName = prefixedRule.Substring(prefixedRule.LastIndexOf(':') + 1);
                    return "GRGEN_ACTIONS." + packageName + ".Rule_" + ruleName + ".IMatch_" + ruleName;
                }
                else
                {
                    String ruleName = prefixedRule;
                    return "GRGEN_ACTIONS.Rule_" + ruleName + ".IMatch_" + ruleName;
                }
            }

            if(type.Contains("::"))
            {
                String packageName = type.Substring(0, type.IndexOf(':'));
                String typeName = type.Substring(type.LastIndexOf(':') + 1);
                return "GRGEN_MODEL." + packageName + ".I" + typeName;
            }
            return "GRGEN_MODEL.I" + type;
        }

        public static string GetPackagePrefixDot(String package)
        {
            return package != null ? package + "." : "";
        }

        public static string PackagePrefixedNameUnderscore(String package, String name)
        {
            return package != null ? package + "_" + name : name;
        }

        // ------------------------------------------------------------------------------------------------

        public static Type GetType(GrGenType type, IGraphModel model)
        {
            if(type is NodeType)
            {
                NodeType nodeType = (NodeType)type;
                if(Type.GetType(nodeType.NodeInterfaceName) != null) // available in libGr (INode)?
                    return Type.GetType(nodeType.NodeInterfaceName);
                else
                    return Type.GetType(nodeType.NodeInterfaceName + "," + Assembly.GetAssembly(model.GetType()).FullName); // no -> search model assembly
            }
            else if(type is EdgeType)
            {
                EdgeType edgeType = (EdgeType)type;
                if(Type.GetType(edgeType.EdgeInterfaceName) != null) // available in libGr (INode)?
                    return Type.GetType(edgeType.EdgeInterfaceName);
                else
                    return Type.GetType(edgeType.EdgeInterfaceName + "," + Assembly.GetAssembly(model.GetType()).FullName); // no -> search model assembly
            }
            else
            {
                VarType varType = (VarType)type;
                return varType.Type;
            }
        }

        /// <summary>
        /// Returns type object for type name string
        /// </summary>
        /// <param name="typeName">Name of the type we want some type object for</param>
        /// <param name="model">Graph model to be search for enum,node,edge types / enum,node/edge type names</param>
        /// <returns>The type object corresponding to the given string, null if type was not found</returns>
        public static Type GetType(String typeName, IGraphModel model)
        {
            if(typeName == null)
                return null;

            switch(typeName)
            {
            case "boolean": return typeof(bool);
            case "byte": return typeof(sbyte);
            case "short": return typeof(short);
            case "int": return typeof(int);
            case "long": return typeof(long);
            case "float": return typeof(float);
            case "double": return typeof(double);
            case "string": return typeof(string);
            case "object": return typeof(object);
            case "graph": return typeof(IGraph);
            }

            if(model == null)
                return null;

            // No standard type, so check enums
            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.PackagePrefixedName == typeName)
                    return enumAttrType.EnumType;
            }

            Assembly assembly = Assembly.GetAssembly(model.GetType());

            // check node and edge types
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(nodeType.PackagePrefixedName == typeName)
                {
                    Type type = Type.GetType(nodeType.NodeInterfaceName); // available in libGr (INode)?
                    if(type != null)
                        return type;
                    type = Type.GetType(nodeType.NodeInterfaceName + "," + assembly.FullName); // no -> search model assembly
                    return type;
                }
            }
            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                if(edgeType.PackagePrefixedName == typeName)
                {
                    Type type = Type.GetType(edgeType.EdgeInterfaceName); // available in libGr (IEdge)?
                    if(type != null)
                        return type;
                    type = Type.GetType(edgeType.EdgeInterfaceName + "," + assembly.FullName); // no -> search model assembly
                    return type;
                }
            }

            return null;
        }
    }
}