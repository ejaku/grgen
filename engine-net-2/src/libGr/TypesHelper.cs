/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

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
                    case "Edge": return "GRGEN_LIBGR.IDEdge";
                    case "UEdge": return "GRGEN_LIBGR.IUEdge";
                    case "AEdge": return "GRGEN_LIBGR.IEdge";
                    default: return "GRGEN_MODEL." + GetPackagePrefixDot(type.Package) + "I" + type.Name;
                }
            }
        }

        public static GrGenType GetNodeOrEdgeType(String typeName, IGraphModel model)
        {
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(nodeType.PackagePrefixedName == typeName) return nodeType;
            }

            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                if(edgeType.PackagePrefixedName == typeName) return edgeType;
            }

            return null;
        }

        public static NodeType GetNodeType(String typeName, IGraphModel model)
        {
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(nodeType.PackagePrefixedName == typeName) return nodeType;
            }

            return null;
        }

        public static EdgeType GetEdgeType(String typeName, IGraphModel model)
        {
            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                if(edgeType.PackagePrefixedName == typeName) return edgeType;
            }

            return null;
        }

        public static EnumAttributeType GetEnumAttributeType(String typeName, IGraphModel model)
        {
            foreach(EnumAttributeType attrType in model.EnumAttributeTypes)
            {
                if(attrType.PackagePrefixedName == typeName) return attrType;
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
                        ContainerHelper.GetDictionaryTypes(typeOfVar, out keyType, out valueType);
                        if(valueType.Name == "SetValueType")
                            return "set<" + DotNetTypeToXgrsType(keyType.Name, keyType.FullName) + ">";
                        else
                            return "map<" + DotNetTypeToXgrsType(keyType.Name, keyType.FullName) + "," + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                    }
                    else if(typeOfVar.Name == "List`1")
                    {
                        Type valueType;
                        ContainerHelper.GetListType(typeOfVar, out valueType);
                        return "array<" + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                    }
                    else if(typeOfVar.Name == "Deque`1")
                    {
                        Type valueType;
                        ContainerHelper.GetDequeType(typeOfVar, out valueType);
                        return "deque<" + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                    }
                }
                return DotNetTypeToXgrsType(type.Name, typeOfVar.FullName);
            }

            return type.PackagePrefixedName;
        }

        public static bool IsRefType(VarType varType)
        {
            return varType.Type.IsGenericType;
        }

        private static String DotNetTypeToXgrsType(String typeName, String fullTypeName)
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
                case "de.unika.ipd.grGen.libGr.INamedGraph": return "graph";
                case "IGraph": return "graph";
                case "INamedGraph": return "graph";
                case "LGSPGraph": return "graph";
                case "LGSPNamedGraph": return "graph";
            }

            String package;
            String type = GetNameAndPackageFromFullTypeName(fullTypeName, out package);

            if(typeName.StartsWith("ENUM_"))
            {
                if(package == null)
                    return type.Substring(5); // remove "ENUM_"
                else
                    return package + "::" + type.Substring(5); // remove "ENUM_"
            }

            if(typeName.StartsWith("NodeType_"))
            {
                if(package == null)
                    return type.Substring(9); // remove "NodeType_"
                else
                    return package + "::" + type.Substring(9); // remove "NodeType_"
            }
            if(typeName.StartsWith("EdgeType_"))
            {
                if(package == null)
                    return type.Substring(9); // remove "EdgeType_"
                else
                    return package + "::" + type.Substring(9); // remove "EdgeType_"
            }

            typeName = typeName.Substring(1); // remove I from class name
            if(typeName == "Edge")  // special handling for IEdge,IDEdge,IUEdge, they map to AEdge,Edge,UEdge resp.
                typeName = "AEdge";
            else if(typeName == "DEdge")
                typeName = "Edge";
            else if(typeName == "UEdge")
                typeName = "UEdge";

            if(package == null)
                return typeName;
            else
                return package + "::" + typeName;
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
                return attributeType.EnumType.PackagePrefixedName;
            case AttributeKind.SetAttr:
                return "set<"+AttributeTypeToXgrsType(attributeType.ValueType)+">";
            case AttributeKind.MapAttr:
                return "map<" + AttributeTypeToXgrsType(attributeType.KeyType) + "," + AttributeTypeToXgrsType(attributeType.ValueType) + ">";
            case AttributeKind.ArrayAttr:
                return "array<" + AttributeTypeToXgrsType(attributeType.ValueType) + ">";
            case AttributeKind.DequeAttr:
                return "deque<" + AttributeTypeToXgrsType(attributeType.ValueType) + ">";
            case AttributeKind.NodeAttr:
                return attributeType.PackagePrefixedTypeName;
            case AttributeKind.EdgeAttr:
                return attributeType.PackagePrefixedTypeName;
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

            return false; // object or node/edge or container type that is not null
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

            if(typeName.StartsWith("ENUM_"))
                typeName = typeName.Substring(5);
            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if (enumAttrType.PackagePrefixedName == typeName)
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

            if(typeName.StartsWith("GRGEN_MODEL."))
                typeName = typeName.Substring(12);
            if(typeName.Contains(".ENUM_"))
                typeName = typeName.Substring(0, typeName.IndexOf(".ENUM_")) + "::" + typeName.Substring(typeName.IndexOf(".ENUM_")+6);
            if(typeName.StartsWith("ENUM_"))
                typeName = typeName.Substring(5);
            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.PackagePrefixedName == typeName)
                    return "(GRGEN_MODEL." + (enumAttrType.Package!=null ? enumAttrType.Package+"." : "") + "ENUM_" + enumAttrType.Name + ")0";
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
                    ContainerHelper.GetDictionaryTypes(constant, out keyType, out valueType);
                    if(valueType == typeof(de.unika.ipd.grGen.libGr.SetValueType))
                        return "set<" + DotNetTypeToXgrsType(keyType.Name, keyType.FullName) + ">";
                    else
                        return "map<" + DotNetTypeToXgrsType(keyType.Name, keyType.FullName) + "," + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                }
                else if(constant.GetType().Name == "List`1")
                {
                    Type valueType;
                    ContainerHelper.GetListType(constant.GetType(), out valueType);
                    return "array<" + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                }
                else //if(constant.GetType().Name == "Deque`1")
                {
                    Type valueType;
                    ContainerHelper.GetListType(constant.GetType(), out valueType);
                    return "deque<" + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                }
            }

            object typeinsteadofobject = NodeOrEdgeTypeIfNodeOrEdge(constant);
            return DotNetTypeToXgrsType(typeinsteadofobject.GetType().Name, typeinsteadofobject.GetType().FullName);
        }

        public static object NodeOrEdgeTypeIfNodeOrEdge(object constant)
        {
            if(constant is INode)
                return ((INode)constant).Type;
            else if(constant is IEdge)
                return ((IEdge)constant).Type;
            else
                return constant;
        }

        public static String ExtractSrc(String genericType)
        {
            if (genericType == null) return null;
            if (genericType.StartsWith("set<")) // set<srcType>
            {
                genericType = genericType.Substring(4);
                genericType = genericType.Remove(genericType.Length - 1);
                return genericType;
            }
            else if (genericType.StartsWith("map<")) // map<srcType,dstType>
            {
                genericType = genericType.Substring(4);
                genericType = genericType.Remove(genericType.IndexOf(","));
                return genericType;
            }
            else if(genericType.StartsWith("array<")) // array<srcType>
            {
                genericType = genericType.Substring(6);
                genericType = genericType.Remove(genericType.Length - 1);
                return genericType;
            }
            else if(genericType.StartsWith("deque<")) // deque<srcType>
            {
                genericType = genericType.Substring(6);
                genericType = genericType.Remove(genericType.Length - 1);
                return genericType;
            }
            else if(genericType.StartsWith("match<")) // match<srcType>
            {
                genericType = genericType.Substring(6);
                genericType = genericType.Remove(genericType.Length - 1);
                return genericType;
            }
            return null;
        }

        public static String ExtractDst(String genericType)
        {
            if (genericType == null) return null;
            if (genericType.StartsWith("set<")) // set<srcType>
            {
                return "SetValueType";
            }
            else if (genericType.StartsWith("map<")) // map<srcType,dstType>
            {
                genericType = genericType.Substring(genericType.IndexOf(",") + 1);
                genericType = genericType.Remove(genericType.Length - 1);
                return genericType;
            }
            else if (genericType.StartsWith("array<")) // array<srcType>
            {
                return "int"; // bullshit int return so the type checks testing that src and dst are available don't fail
            }
            else if (genericType.StartsWith("deque<")) // deque<srcType>
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
        /// Returns type with correct namespace prefix for the type given
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
            if(type.StartsWith("match<")) return "Rule_" + ExtractSrc(type) + ".IMatch_" + ExtractSrc(type);
            if(type == "SetValueType") return "GRGEN_LIBGR.SetValueType";
            if(type == "graph") return "GRGEN_LIBGR.IGraph"; 

            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if (enumAttrType.PackagePrefixedName == type)
                    return "GRGEN_MODEL." + (enumAttrType.Package!=null ? enumAttrType.Package+"." : "") + "ENUM_" + enumAttrType.Name;
            }

            if(type.Contains("::"))
            {
                String packageName = type.Substring(0, type.IndexOf(':'));
                String typeName = type.Substring(type.LastIndexOf(':')+1);
                return "GRGEN_MODEL." + packageName + ".I" + typeName;
            }
            return "GRGEN_MODEL.I" + type;
        }

        /// <summary>
        /// Returns type with correct namespace prefix for the node or edge type given
        /// </summary>
        public static string XgrsTypeToCSharpTypeNodeEdge(string type)
        {
            if(type == "Node") return "GRGEN_LIBGR.INode";
            if(type == "AEdge") return "GRGEN_LIBGR.IEdge";
            if(type == "Edge") return "GRGEN_LIBGR.IDEdge";
            if(type == "UEdge") return "GRGEN_LIBGR.IUEdge";

            if(type.Contains("::"))
            {
                String packageName = type.Substring(0, type.IndexOf(':'));
                String typeName = type.Substring(type.LastIndexOf(':') + 1);
                return "GRGEN_MODEL." + packageName + "." + typeName;
            }
            return "GRGEN_MODEL." + type;
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
            else if(xgrsTypeSameOrSub.StartsWith("map<"))
            {
                if(!xgrsTypeBase.StartsWith("map<")) return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase) && ExtractDst(xgrsTypeSameOrSub) == ExtractDst(xgrsTypeBase);
            }
            else if(xgrsTypeSameOrSub.StartsWith("array<"))
            {
                if(!xgrsTypeBase.StartsWith("array<")) return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }
            else if(xgrsTypeSameOrSub.StartsWith("deque<"))
            {
                if(!xgrsTypeBase.StartsWith("deque<")) return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }

            else if(xgrsTypeSameOrSub.StartsWith("match<"))
            {
                if(!xgrsTypeBase.StartsWith("match<")) return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }

            else if(xgrsTypeSameOrSub == "short" || xgrsTypeSameOrSub == "int" || xgrsTypeSameOrSub == "long" 
                || xgrsTypeSameOrSub == "float" || xgrsTypeSameOrSub == "double"
                || xgrsTypeSameOrSub == "string" || xgrsTypeSameOrSub == "object"
                || xgrsTypeSameOrSub == "graph")
                return xgrsTypeSameOrSub==xgrsTypeBase;
            else if (xgrsTypeSameOrSub == "byte" || xgrsTypeSameOrSub == "sbyte")
                return xgrsTypeBase == "byte" || xgrsTypeBase == "sbyte";
            else if (xgrsTypeSameOrSub == "bool" || xgrsTypeSameOrSub == "boolean")
                return xgrsTypeBase=="bool" || xgrsTypeBase=="boolean";

            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.PackagePrefixedName == xgrsTypeSameOrSub)
                    return xgrsTypeSameOrSub == xgrsTypeBase;
            }

            foreach(NodeType leftNodeType in model.NodeModel.Types)
            {
                if(leftNodeType.PackagePrefixedName == xgrsTypeSameOrSub)
                {
                    foreach(NodeType rightNodeType in model.NodeModel.Types)
                    {
                        if(rightNodeType.PackagePrefixedName == xgrsTypeBase)
                        {
                            return leftNodeType.IsA(rightNodeType);
                        }
                    }
                }
            }

            foreach(EdgeType leftEdgeType in model.EdgeModel.Types)
            {
                if(leftEdgeType.PackagePrefixedName == xgrsTypeSameOrSub)
                {
                    foreach(EdgeType rightEdgeType in model.EdgeModel.Types)
                    {
                        if(rightEdgeType.PackagePrefixedName == xgrsTypeBase)
                        {
                            return leftEdgeType.IsA(rightEdgeType);
                        }
                    }
                }
            }

            foreach(ExternalType leftExternalType in model.ExternalTypes)
            {
                if(leftExternalType.Name == xgrsTypeSameOrSub)
                {
                    foreach(ExternalType rightExternalType in model.ExternalTypes)
                    {
                        if(rightExternalType.Name == xgrsTypeBase)
                        {
                            return leftExternalType.IsA(rightExternalType);
                        }
                    }
                }
            }

            return false;
        }

        public static bool IsSameOrSubtypeDisregardingDirectednessOfEdges(string xgrsTypeSameOrSub, string xgrsTypeBase, IGraphModel model)
        {
            if (xgrsTypeSameOrSub == "AEdge" && (xgrsTypeBase == "Edge" || xgrsTypeBase == "UEdge"))
                return true; // not type safe, to be compensated by type checks, allowed so people can work with Edge and UEdge even though graph query functions return AEdge

            return IsSameOrSubtype(xgrsTypeSameOrSub, xgrsTypeBase, model);
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
                if(enumAttrType.PackagePrefixedName == typename)
                    return true;
            }

            return false;
        }

        public static bool IsExternalTypeIncludingObjectType(string typename, IGraphModel model)
        {
            for(int i=0; i<model.ExternalTypes.Length; ++i)
            {
                if(model.ExternalTypes[i].Name == typename)
                    return true;
            }

            return false;
        }

        public static string GetPackagePrefixDot(String package)
        {
            return package != null ? package + "." : "";
        }

        public static string PackagePrefixedNameUnderscore(String package, String name)
        {
            return package != null ? package + "_" + name : name;
        }

        public static string GetNameAndPackageFromFullTypeName(string fullTypeName, out string package)
        {
            fullTypeName = fullTypeName.Substring(19); // remove "de.unika.ipd.grGen."
            fullTypeName = fullTypeName.Substring(fullTypeName.IndexOf('.') + 1); // remove "model_XXX." or "Action_XXX."
            String[] packageAndTypeName = fullTypeName.Split('.');
            if(packageAndTypeName.Length == 1)
            {
                package = null;
                return packageAndTypeName[0];
            }
            else
            {
                package = packageAndTypeName[0];
                return packageAndTypeName[1];
            }
        }

        public static string GetPackagePrefixedNameFromFullTypeName(string fullTypeName)
        {
            String package;
            String type = GetNameAndPackageFromFullTypeName(fullTypeName, out package);
            if(package != null)
                return package + "::" + type;
            else
                return type;
        }

        /// <summary>
        /// Returns a clone of either a graph or a match or a container
        /// </summary>
        /// <param name="toBeCloned">The graph or match or container to be cloned</param>
        /// <returns>The cloned graph or match or container</returns>
        public static object Clone(object toBeCloned)
        {
            if(toBeCloned is IGraph)
                return GraphHelper.Copy((IGraph)toBeCloned);
            else if(toBeCloned is IMatch)
                return ((IMatch)toBeCloned).Clone();
            else
                return ContainerHelper.Clone(toBeCloned);
        }
    }
}
