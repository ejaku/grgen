/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    // TODO: much of this string handling is scruffy, there should be some type representation used throughout the entire backend
    // (some string/object based runtime type handling needed for interpreted sequences)

    /// <summary>
    /// The TypesHelper in this file contains code that creates or handles GrGen types, as name strings or libGr-objects
    /// </summary>
    public static partial class TypesHelper
    {
        public static GrGenType GetNodeOrEdgeType(String typeName, IGraphModel model)
        {
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(nodeType.PackagePrefixedName == typeName)
                    return nodeType;
            }

            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                if(edgeType.PackagePrefixedName == typeName)
                    return edgeType;
            }

            return null;
        }

        public static NodeType GetNodeType(String typeName, IGraphModel model)
        {
            foreach(NodeType nodeType in model.NodeModel.Types)
            {
                if(nodeType.PackagePrefixedName == typeName)
                    return nodeType;
            }

            return null;
        }

        public static EdgeType GetEdgeType(String typeName, IGraphModel model)
        {
            foreach(EdgeType edgeType in model.EdgeModel.Types)
            {
                if(edgeType.PackagePrefixedName == typeName)
                    return edgeType;
            }

            return null;
        }

        public static EnumAttributeType GetEnumAttributeType(String typeName, IGraphModel model)
        {
            foreach(EnumAttributeType attrType in model.EnumAttributeTypes)
            {
                if(attrType.PackagePrefixedName == typeName)
                    return attrType;
            }

            return null;
        }

        public static String DotNetTypeToXgrsType(Type type)
        {
            if(type.IsGenericType)
            {
                if(type.Name == "Dictionary`2")
                {
                    Type keyType;
                    Type valueType;
                    ContainerHelper.GetDictionaryTypes(type, out keyType, out valueType);
                    if(valueType.Name == "SetValueType")
                        return "set<" + DotNetTypeToXgrsType(keyType.Name, keyType.FullName) + ">";
                    else
                        return "map<" + DotNetTypeToXgrsType(keyType.Name, keyType.FullName) + "," + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                }
                else if(type.Name == "List`1")
                {
                    Type valueType;
                    ContainerHelper.GetListType(type, out valueType);
                    return "array<" + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                }
                else if(type.Name == "Deque`1")
                {
                    Type valueType;
                    ContainerHelper.GetDequeType(type, out valueType);
                    return "deque<" + DotNetTypeToXgrsType(valueType.Name, valueType.FullName) + ">";
                }
            }
            return DotNetTypeToXgrsType(type.Name, type.FullName);
        }

        public static String DotNetTypeToXgrsType(GrGenType type)
        {
            if(type is VarType)
            {
                Type typeOfVar = ((VarType)type).Type;
                if(typeOfVar.IsGenericType)
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
            switch(typeName)
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

            if(typeName.StartsWith("IMatch_"))
            {
                String potentialRuleName = typeName.Substring(7);
                if(fullTypeName.Contains(".Rule_" + potentialRuleName))
                {
                    if(package == null)
                        return "match<" + potentialRuleName + ">"; // remove "IMatch_"
                    else
                        return "match<" + package + "::" + potentialRuleName + ">"; // remove "IMatch_"
                }
                else
                {
                    if(package == null)
                        return "match<class " + potentialRuleName + ">"; // remove "IMatch_"
                    else
                        return "match<class " + package + "::" + potentialRuleName + ">"; // remove "IMatch_"
                }
            }
            if(typeName == "IMatch")
                return "match<>";

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
                return "set<" + AttributeTypeToXgrsType(attributeType.ValueType) + ">";
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

        public static String SeparatePackage(String packagePrefixedName, out String packageName)
        {
            if(packagePrefixedName.IndexOf("::") != -1)
            {
                packageName = packagePrefixedName.Substring(0, packagePrefixedName.IndexOf("::"));
                return packagePrefixedName.Substring(packagePrefixedName.IndexOf("::") + "::".Length);
            }
            else
            {
                packageName = null;
                return packagePrefixedName;
            }
        }

        public static String ExtractSrc(String genericType)
        {
            if(genericType == null) return null;
            if(genericType.StartsWith("set<")) // set<srcType>
            {
                genericType = genericType.Substring(4);
                genericType = genericType.Remove(genericType.Length - 1);
                return genericType;
            }
            else if(genericType.StartsWith("map<")) // map<srcType,dstType>
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
            else if(genericType.StartsWith("match<class ")) // match<class srcType>
            {
                genericType = genericType.Substring(12);
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
            if(genericType == null)
                return null;
            if(genericType.StartsWith("set<")) // set<srcType>
                return "SetValueType";
            else if(genericType.StartsWith("map<")) // map<srcType,dstType>
            {
                genericType = genericType.Substring(genericType.IndexOf(",") + 1);
                genericType = genericType.Remove(genericType.Length - 1);
                return genericType;
            }
            else if(genericType.StartsWith("array<")) // array<srcType>
                return "int"; // bullshit int return so the type checks testing that src and dst are available don't fail
            else if(genericType.StartsWith("deque<")) // deque<srcType>
                return "int"; // bullshit int return so the type checks testing that src and dst are available don't fail
            return null;
        }

        public static String GetRuleName(String matchType)
        {
            return matchType.Substring(6, matchType.Length - 6 - 1); // remove match< begin and > end
        }

        public static String GetMatchClassName(String matchClassType)
        {
            return matchClassType.Substring(12, matchClassType.Length - 12 - 1); // remove match<class begin and > end
        }

        public static bool IsSameOrSubtype(string xgrsTypeSameOrSub, string xgrsTypeBase, IGraphModel model)
        {
            if(xgrsTypeSameOrSub == "" || xgrsTypeBase == "")
                return true;

            if(xgrsTypeSameOrSub.StartsWith("set<"))
            {
                if(!xgrsTypeBase.StartsWith("set<"))
                    return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }
            else if(xgrsTypeSameOrSub.StartsWith("map<"))
            {
                if(!xgrsTypeBase.StartsWith("map<"))
                    return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase) && ExtractDst(xgrsTypeSameOrSub) == ExtractDst(xgrsTypeBase);
            }
            else if(xgrsTypeSameOrSub.StartsWith("array<"))
            {
                if(!xgrsTypeBase.StartsWith("array<"))
                    return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }
            else if(xgrsTypeSameOrSub.StartsWith("deque<"))
            {
                if(!xgrsTypeBase.StartsWith("deque<"))
                    return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }
            else if(xgrsTypeSameOrSub.StartsWith("match<"))
            {
                if(!xgrsTypeBase.StartsWith("match<"))
                    return false;
                return ExtractSrc(xgrsTypeSameOrSub) == ExtractSrc(xgrsTypeBase);
            }
            else if(xgrsTypeSameOrSub == "short" || xgrsTypeSameOrSub == "int" || xgrsTypeSameOrSub == "long"
                || xgrsTypeSameOrSub == "float" || xgrsTypeSameOrSub == "double"
                || xgrsTypeSameOrSub == "string" || xgrsTypeSameOrSub == "object"
                || xgrsTypeSameOrSub == "graph")
            {
                return xgrsTypeSameOrSub == xgrsTypeBase;
            }
            else if(xgrsTypeSameOrSub == "byte" || xgrsTypeSameOrSub == "sbyte")
                return xgrsTypeBase == "byte" || xgrsTypeBase == "sbyte";
            else if(xgrsTypeSameOrSub == "bool" || xgrsTypeSameOrSub == "boolean")
                return xgrsTypeBase == "bool" || xgrsTypeBase == "boolean";

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
                            return leftNodeType.IsA(rightNodeType);
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
                            return leftEdgeType.IsA(rightEdgeType);
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
                            return leftExternalType.IsA(rightExternalType);
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
                if(enumAttrType.PackagePrefixedName == typename)
                    return true;
            }

            return false;
        }

        public static bool IsExternalTypeIncludingObjectType(string typename, IGraphModel model)
        {
            for(int i = 0; i < model.ExternalTypes.Length; ++i)
            {
                if(model.ExternalTypes[i].Name == typename)
                    return true;
            }

            return false;
        }

        public static string PackagePrefixedNameDoubleColon(String package, String name)
        {
            return package != null ? package + "::" + name : name;
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

        public static String XgrsTypeOfConstant(object constant, IGraphModel model)
        {
            if(constant == null)
                return "";

            if(constant.GetType().IsGenericType)
            {
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
    }
}