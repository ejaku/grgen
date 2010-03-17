/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    // TODO: all of this string handling is scruffy, there should be some type representation used throughout the entire backend

    public class TypesHelper
    {
        public static String TypeName(GrGenType type)
        {
            if (type is VarType)
            {
                Type typeOfVar = ((VarType)type).Type;
                if (typeOfVar.IsGenericType)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(typeOfVar.FullName.Substring(0, typeOfVar.FullName.IndexOf('`')));
                    sb.Append('<');
                    bool first = true;
                    foreach (Type typeArg in typeOfVar.GetGenericArguments())
                    {
                        if (first) first = false;
                        else sb.Append(", ");
                        sb.Append(typeArg.FullName);
                    }
                    sb.Append('>');
                    return sb.ToString();
                }

                return typeOfVar.FullName;
            }
            else
            {
                switch (type.Name)
                {
                    case "Node": return "GRGEN_LIBGR.INode";
                    case "Edge": return "GRGEN_LIBGR.IEdge";
                    case "UEdge": return "GRGEN_LIBGR.IEdge";
                    case "AEdge": return "GRGEN_LIBGR.IEdge";
                    default: return "GRGEN_MODEL.I" + type.Name;
                }
            }
        }

        public static GrGenType GetNodeOrEdgeType(String typeName, IGraphModel model)
        {
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(nodeType.Name == typeName) return nodeType;
            }

            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                if(edgeType.Name == typeName) return edgeType;
            }

            return null;
        }

        public static String DotNetTypeToXgrsType(GrGenType type)
        {
            if (type is VarType)
            {
                Type typeOfVar = ((VarType)type).Type;
                if (typeOfVar.IsGenericType)
                {
                    Type keyType;
                    Type valueType;
                    DictionaryHelper.GetDictionaryTypes(typeOfVar, out keyType, out valueType);
                    if (valueType.Name == "SetValueType")
                        return "set<" + DotNetTypeToXgrsType(keyType.Name) + ">";
                    else
                        return "map<" + DotNetTypeToXgrsType(keyType.Name) + "," + DotNetTypeToXgrsType(valueType.Name) + ">";
                }
                return DotNetTypeToXgrsType(type.Name);
            }

            return type.Name;
        }

        private static String DotNetTypeToXgrsType(String typeName)
        {
            switch (typeName)
            {
                case "Int32": return "int";
                case "Boolean": return "boolean";
                case "Single": return "float";
                case "Double": return "double";
                case "String": return "string";
                case "Object": return "object";
            }

            if (typeName.StartsWith("ENUM_")) return typeName.Substring(5);

            return typeName;
        }

        public static String AttributeTypeToXgrsType(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
            case AttributeKind.IntegerAttr:
                return "int";
            case AttributeKind.BooleanAttr:
                return "boolean";
            case AttributeKind.StringAttr:
                return "string";
            case AttributeKind.FloatAttr:
                return "float";
            case AttributeKind.DoubleAttr:
                return "double";
            case AttributeKind.ObjectAttr:
                return "object";
            case AttributeKind.EnumAttr:
                return attributeType.EnumType.Name;
            case AttributeKind.SetAttr:
                return "set<"+AttributeTypeToXgrsType(attributeType.ValueType)+">";
            case AttributeKind.MapAttr:
                return "map<" + AttributeTypeToXgrsType(attributeType.KeyType) + "," + AttributeTypeToXgrsType(attributeType.ValueType) + ">";
            default:
                return null;
            }
        }

        public static String DefaultValue(String typeName, IGraphModel model)
        {
            switch (typeName)
            {
                case "Int32": return "0";
                case "Boolean": return "false";
                case "Single": return "0.0f";
                case "Double": return "0.0";
                case "String": return "\"\"";
            }

            switch (typeName)
            {
                case "int": return "0";
                case "bool": return "false";
                case "float": return "0.0f";
                case "double": return "0.0";
                case "string": return "\"\"";
                case "object": return "null";
            }

            if(typeName == "boolean") return "false";

            foreach (EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if ("ENUM_" + enumAttrType.Name == typeName)
                    return "(GRGEN_MODEL.ENUM_" + enumAttrType.Name + ")0";
                if (enumAttrType.Name == typeName)
                    return "(GRGEN_MODEL.ENUM_" + enumAttrType.Name + ")0";
            }

            return "null"; // object or node type or edge type
        }

        public static String XgrsTypeOfConstant(object constant, IGraphModel model)
        {
            if(constant is int) {
                return "int";
            } else if(constant is bool) {
                return "boolean";
            } else if(constant is float) {
                return "float";
            } else if(constant is double) {
                return "double";
            } else if(constant is string && ((string)constant).Contains("::")) {
                string strConst = (string)constant;
                int separationPos = strConst.IndexOf("::");
                string type = strConst.Substring(0, separationPos);
                string value = strConst.Substring(separationPos + 2);
                foreach(EnumAttributeType attrType in model.EnumAttributeTypes)
                {
                    if(attrType.Name == type)
                    {
                        Type enumType = attrType.EnumType;
                        foreach(EnumMember enumMember in attrType.Members)
                        {
                            if(enumMember.Name == value)
                            {
                                return type;
                            }
                        }
                        return "!value " + value + " of enum " + type + " not found";
                    }
                }
                return "!enum "+type+" not found";
            } else if(constant is string && ((string)constant).StartsWith("set<") && ((string)constant).EndsWith(">")) {
                return "set<"+ExtractSrc((string)constant)+">";
            } else if(constant is string && ((string)constant).StartsWith("map<") && ((string)constant).EndsWith(">")) {
                return "map<"+ExtractSrc((string)constant)+","+ExtractDst((string)constant)+">";
            } else if(constant is string) {
                return "string";
            }

            return "object";
        }

        public static String ExtractSrc(String setmapType)
        {
            if (setmapType == null) return null;
            if (setmapType.StartsWith("set<")) // map<srcType>
            {
                setmapType = setmapType.Remove(0, 4);
                setmapType = setmapType.Remove(setmapType.Length - 1);
                return setmapType;
            }
            else if (setmapType.StartsWith("map<")) // map<srcType,dstType>
            {
                setmapType = setmapType.Remove(0, 4);
                setmapType = setmapType.Remove(setmapType.IndexOf(","));
                return setmapType;
            }
            return null;
        }

        public static String ExtractDst(String setmapType)
        {
            if (setmapType == null) return null;
            if (setmapType.StartsWith("set<")) // set<srcType>
            {
                return "SetValueType";
            }
            else if (setmapType.StartsWith("map<")) // map<srcType,dstType>
            {
                setmapType = setmapType.Remove(0, setmapType.IndexOf(",") + 1);
                setmapType = setmapType.Remove(setmapType.Length - 1);
                return setmapType;
            }
            return null;
        }

        /// <summary>
        /// Returns type with correct namespace prefix for the type given
        /// </summary>
        public static string XgrsTypeToCSharpType(string type, IGraphModel model)
        {
            if (type == "Node") return "GRGEN_LIBGR.INode";
            if (type == "AEdge" || type == "Edge" || type == "UEdge") return "GRGEN_LIBGR.IEdge";
            if (type == "int" || type == "bool" || type == "string" || type == "float" || type == "double" || type == "object") return type;
            if (type == "boolean") return "bool";
            if (type.StartsWith("set<") || type.StartsWith("map<")) return "Dictionary<" + XgrsTypeToCSharpType(ExtractSrc(type), model) + "," + XgrsTypeToCSharpType(ExtractDst(type), model) + ">";
            if (type == "SetValueType") return "GRGEN_LIBGR.SetValueType";

            foreach (EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if (enumAttrType.Name == type)
                    return "GRGEN_MODEL.ENUM_" + type;
            }

            return "GRGEN_MODEL.I" + type;
        }

        public static bool IsSameOrSubtype(string xgrsTypeSameOrSub, string xgrsTypeBase, IGraphModel model)
        {
            if(xgrsTypeSameOrSub == "" || xgrsTypeBase == "")
                return true;

            if(xgrsTypeSameOrSub.StartsWith("set<"))
            {
                if(!xgrsTypeBase.StartsWith("set<")) return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }
            if(xgrsTypeSameOrSub.StartsWith("map<"))
            {
                if(!xgrsTypeBase.StartsWith("map<")) return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase) && ExtractDst(xgrsTypeSameOrSub) == ExtractDst(xgrsTypeBase);
            }

            if(xgrsTypeSameOrSub == "int" || xgrsTypeSameOrSub == "string" || xgrsTypeSameOrSub == "float" || xgrsTypeSameOrSub == "double" || xgrsTypeSameOrSub == "object") 
                return xgrsTypeSameOrSub==xgrsTypeBase;
            if(xgrsTypeSameOrSub == "bool" || xgrsTypeSameOrSub == "boolean") 
                return xgrsTypeBase=="bool" || xgrsTypeBase=="boolean";

            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.Name == xgrsTypeSameOrSub)
                    return xgrsTypeSameOrSub == xgrsTypeBase;
            }

            foreach(NodeType leftNodeType in model.NodeModel.Types)
            {
                if(leftNodeType.Name == xgrsTypeSameOrSub)
                {
                    foreach(NodeType rightNodeType in model.NodeModel.Types)
                    {
                        if(rightNodeType.Name == xgrsTypeBase)
                        {
                            return leftNodeType.IsA(rightNodeType);
                        }
                    }
                }
            }

            foreach(EdgeType leftEdgeType in model.EdgeModel.Types)
            {
                if(leftEdgeType.Name == xgrsTypeSameOrSub)
                {
                    foreach(EdgeType rightEdgeType in model.EdgeModel.Types)
                    {
                        if(rightEdgeType.Name == xgrsTypeBase)
                        {
                            return leftEdgeType.IsA(rightEdgeType);
                        }
                    }
                }
            }

            return false;
        }
    }
}
