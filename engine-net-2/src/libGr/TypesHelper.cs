/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Text;

namespace de.unika.ipd.grGen.libGr
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

        public static NodeType GetNodeType(String typeName, IGraphModel model)
        {
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(nodeType.Name == typeName) return nodeType;
            }

            return null;
        }

        public static EdgeType GetEdgeType(String typeName, IGraphModel model)
        {
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
                    if(typeOfVar.Name == "Dictionary`2")
                    {
                        Type keyType;
                        Type valueType;
                        DictionaryListHelper.GetDictionaryTypes(typeOfVar, out keyType, out valueType);
                        if(valueType.Name == "SetValueType")
                            return "set<" + DotNetTypeToXgrsType(keyType.Name) + ">";
                        else
                            return "map<" + DotNetTypeToXgrsType(keyType.Name) + "," + DotNetTypeToXgrsType(valueType.Name) + ">";
                    }
                    else //if(typeOfVar.Name == "List`1")
                    {
                        Type valueType;
                        DictionaryListHelper.GetListType(typeOfVar, out valueType);
                        return "array<" + DotNetTypeToXgrsType(valueType.Name) + ">";
                    }
                }
                return DotNetTypeToXgrsType(type.Name);
            }

            return type.Name;
        }

        private static String DotNetTypeToXgrsType(String typeName)
        {
            switch (typeName)
            {
                case "SByte": return "byte";
                case "Int16": return "short";
                case "Int32": return "int";
                case "Int64": return "long";
                case "Boolean": return "boolean";
                case "Single": return "float";
                case "Double": return "double";
                case "String": return "string";
                case "Object": return "object";
                case "de.unika.ipd.grGen.libGr.IGraph": return "graph";
            }

            if (typeName.StartsWith("ENUM_")) return typeName.Substring(5);

            if (typeName.StartsWith("NodeType_")) return typeName.Substring(9);
            if (typeName.StartsWith("EdgeType_")) return typeName.Substring(9);

            return typeName.Substring(1); // remove I from class name
        }

        public static String AttributeTypeToXgrsType(AttributeType attributeType)
        {
            switch(attributeType.Kind)
            {
            case AttributeKind.ByteAttr:
                return "byte";
            case AttributeKind.ShortAttr:
                return "short";
            case AttributeKind.IntegerAttr:
                return "int";
            case AttributeKind.LongAttr:
                return "long";
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
            case AttributeKind.ArrayAttr:
                return "array<" + AttributeTypeToXgrsType(attributeType.ValueType) + ">";
            case AttributeKind.NodeAttr:
                return attributeType.TypeName;
            case AttributeKind.EdgeAttr:
                return attributeType.TypeName;
            case AttributeKind.GraphAttr:
                return "graph";
            default:
                return null;
            }
        }

        public static bool IsDefaultValue(object value)
        {
            if(value == null)
                return true;

            if (value is SByte) {
                return (SByte)value == 0;
            } else if (value is Int16) {
                return (Int16)value == 0;
            } else if (value is Int32) {
                return (Int32)value == 0;
            } else if (value is Int64) {
                return (Int64)value == 0L;
            } else if (value is Boolean) {
                return (Boolean)value == false;
            } else if (value is Single) {
                return (Single)value == 0.0f;
            } else if (value is Double) {
                return (Double)value == 0.0;
            } else if (value is String) {
                return (String)value == "";
            } else if (value is Enum) {
                return Convert.ToInt32((Enum)value) == 0;
            }

            return false; // object or node/edge or dictionary/list type which is not null
        }

        public static object DefaultValue(String typeName, IGraphModel model)
        {
            switch (typeName)
            {
                case "SByte": return 0;
                case "Int16": return 0;
                case "Int32": return 0;
                case "Int64": return 0L;
                case "Boolean": return false;
                case "Single": return 0.0f;
                case "Double": return 0.0;
                case "String": return "";
            }

            switch (typeName)
            {
                case "byte": return 0;
                case "short": return 0;
                case "int": return 0;
                case "long": return 0L;
                case "bool": return false;
                case "float": return 0.0f;
                case "double": return 0.0;
                case "string": return "";
                case "object": return null;
            }

            if(typeName == "boolean") return false;

            foreach (EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if ("ENUM_" + enumAttrType.Name == typeName)
                    return Enum.Parse(enumAttrType.EnumType, Enum.GetName(enumAttrType.EnumType, 0));
                if (enumAttrType.Name == typeName)
                    return Enum.Parse(enumAttrType.EnumType, Enum.GetName(enumAttrType.EnumType, 0));
            }

            return null; // object or graph or node type or edge type
        }

        public static String DefaultValueString(String typeName, IGraphModel model)
        {
            switch (typeName)
            {
                case "SByte": return "0";
                case "Int16": return "0";
                case "Int32": return "0";
                case "Int64": return "0L";
                case "Boolean": return "false";
                case "Single": return "0.0f";
                case "Double": return "0.0";
                case "String": return "\"\"";
            }

            switch (typeName)
            {
                case "byte": return "0";
                case "short": return "0";
                case "int": return "0";
                case "long": return "0L";
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

            return "null"; // object or graph or node type or edge type
        }

        public static String XgrsTypeOfConstant(object constant, IGraphModel model)
        {
            if(constant == null)
                return "";

            if(constant.GetType().IsGenericType) {
                if(constant.GetType().Name == "Dictionary`2")
                {
                    Type keyType;
                    Type valueType;
                    DictionaryListHelper.GetDictionaryTypes(constant, out keyType, out valueType);
                    if(valueType == typeof(de.unika.ipd.grGen.libGr.SetValueType))
                        return "set<" + DotNetTypeToXgrsType(keyType.Name) + ">";
                    else
                        return "map<" + DotNetTypeToXgrsType(keyType.Name) + "," + DotNetTypeToXgrsType(valueType.Name) + ">";
                }
                else //if(typeOfVar.Name == "List`1")
                {
                    Type valueType;
                    DictionaryListHelper.GetListType(constant.GetType(), out valueType);
                    return "array<" + DotNetTypeToXgrsType(valueType.Name) + ">";
                }
            }

            return DotNetTypeToXgrsType(constant.GetType().Name);
        }

        public static String ExtractSrc(String setmaparrayType)
        {
            if (setmaparrayType == null) return null;
            if (setmaparrayType.StartsWith("set<")) // set<srcType>
            {
                setmaparrayType = setmaparrayType.Remove(0, 4);
                setmaparrayType = setmaparrayType.Remove(setmaparrayType.Length - 1);
                return setmaparrayType;
            }
            else if (setmaparrayType.StartsWith("map<")) // map<srcType,dstType>
            {
                setmaparrayType = setmaparrayType.Remove(0, 4);
                setmaparrayType = setmaparrayType.Remove(setmaparrayType.IndexOf(","));
                return setmaparrayType;
            }
            else if(setmaparrayType.StartsWith("array<")) // array<srcType>
            {
                setmaparrayType = setmaparrayType.Remove(0, 6);
                setmaparrayType = setmaparrayType.Remove(setmaparrayType.Length - 1);
                return setmaparrayType;
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
            else if (setmapType.StartsWith("array<")) // array<srcType>
            {
                return "int"; // bullshit int return so the type checks testing that src and dst are available don't fail
            }
            return null;
        }

        /// <summary>
        /// Returns type string with correct namespace prefix for the type given
        /// </summary>
        public static string PrefixedTypeFromType(Type type)
        {
            if(type.Name == "INode") return "GRGEN_LIBGR.INode";
            if(type.Name == "IEdge") return "GRGEN_LIBGR.IEdge";

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

            return "GRGEN_MODEL." + type.Name;
        }

        /// <summary>
        /// Returns type with correct namespace prefix for the type given
        /// </summary>
        public static string XgrsTypeToCSharpType(string type, IGraphModel model)
        {
            if(type == "Node") return "GRGEN_LIBGR.INode";
            if(type == "AEdge" || type == "Edge" || type == "UEdge") return "GRGEN_LIBGR.IEdge";
            if(type == "short" || type == "int" || type == "long" || type == "bool" || type == "string" || type == "float" || type == "double" || type == "object") return type;
            if(type == "byte") return "sbyte";
            if(type == "boolean") return "bool";
            if(type.StartsWith("set<") || type.StartsWith("map<")) return "Dictionary<" + XgrsTypeToCSharpType(ExtractSrc(type), model) + "," + XgrsTypeToCSharpType(ExtractDst(type), model) + ">";
            if(type.StartsWith("array<")) return "List<" + XgrsTypeToCSharpType(ExtractSrc(type), model) + ">";
            if(type == "SetValueType") return "GRGEN_LIBGR.SetValueType";
            if(type == "graph") return "GRGEN_LIBGR.IGraph"; 

            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
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
            if(xgrsTypeSameOrSub.StartsWith("array<"))
            {
                if(!xgrsTypeBase.StartsWith("array<")) return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }

            if(xgrsTypeSameOrSub == "short" || xgrsTypeSameOrSub == "int" || xgrsTypeSameOrSub == "long" 
                || xgrsTypeSameOrSub == "float" || xgrsTypeSameOrSub == "double"
                || xgrsTypeSameOrSub == "string" || xgrsTypeSameOrSub == "object"
                || xgrsTypeSameOrSub == "graph")
                return xgrsTypeSameOrSub==xgrsTypeBase;
            if (xgrsTypeSameOrSub == "byte" || xgrsTypeSameOrSub == "sbyte")
                return xgrsTypeBase == "byte" || xgrsTypeBase == "sbyte";
            if (xgrsTypeSameOrSub == "bool" || xgrsTypeSameOrSub == "boolean")
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

        public static string GetStorageKeyTypeName(VarType storage)
        {
            return storage.Type.GetGenericArguments()[0].FullName;
        }

        public static string GetStorageValueTypeName(VarType storage)
        {
            return storage.Type.GetGenericArguments()[1].FullName;
        }

        public static bool IsEnumType(string typename, IGraphModel model)
        {
            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.Name == typename)
                    return true;
            }

            return false;
        }
    }
}
