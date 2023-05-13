/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
                    return DotNetTypeToXgrsType(typeOfVar);
                return DotNetTypeToXgrsType(type.Name, typeOfVar.FullName);
            }

            return type.PackagePrefixedName;
        }

        public static bool IsRefType(VarType varType)
        {
            return varType.Type.IsGenericType;
        }

        // no support for container types, use function with same name but Type input
        // no support for external types -- chops off first character from external type name
        public static String DotNetTypeToXgrsType(String typeName, String fullTypeName)
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
            if(typeName.StartsWith("ObjectType_"))
            {
                if(package == null)
                    return type.Substring(11); // remove "ObjectType_"
                else
                    return package + "::" + type.Substring(11); // remove "ObjectType_"
            }
            if(typeName.StartsWith("TransientObjectType_"))
            {
                if(package == null)
                    return type.Substring(20); // remove "TransientObjectType_"
                else
                    return package + "::" + type.Substring(20); // remove "TransientObjectType_"
            }

            if(typeName.StartsWith("IMatch_") || typeName.StartsWith("Match_"))
            {
                String potentialRuleName = typeName.StartsWith("IMatch_") ? typeName.Substring(7) : typeName.Substring(6); // remove "(I)Match_"
                if(fullTypeName.Contains(".Rule_" + potentialRuleName))
                {
                    if(package == null)
                        return "match<" + potentialRuleName + ">";
                    else
                        return "match<" + package + "::" + potentialRuleName + ">";
                }
                else
                {
                    if(package == null)
                        return "match<class " + potentialRuleName + ">";
                    else
                        return "match<class " + package + "::" + potentialRuleName + ">";
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
            case AttributeKind.InternalClassObjectAttr:
                return attributeType.PackagePrefixedTypeName;
            case AttributeKind.InternalClassTransientObjectAttr:
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

        public static bool IsContainerType(String typeCandidate)
        {
            return typeCandidate.StartsWith("set<")
                || typeCandidate.StartsWith("map<")
                || typeCandidate.StartsWith("array<")
                || typeCandidate.StartsWith("deque<");
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

            foreach(ObjectType leftObjectType in model.ObjectModel.Types)
            {
                if(leftObjectType.PackagePrefixedName == xgrsTypeSameOrSub)
                {
                    foreach(ObjectType rightObjectType in model.ObjectModel.Types)
                    {
                        if(rightObjectType.PackagePrefixedName == xgrsTypeBase)
                            return leftObjectType.IsA(rightObjectType);
                    }
                }
            }

            foreach(TransientObjectType leftTransientObjectType in model.TransientObjectModel.Types)
            {
                if(leftTransientObjectType.PackagePrefixedName == xgrsTypeSameOrSub)
                {
                    foreach(TransientObjectType rightTransientObjectType in model.TransientObjectModel.Types)
                    {
                        if(rightTransientObjectType.PackagePrefixedName == xgrsTypeBase)
                            return leftTransientObjectType.IsA(rightTransientObjectType);
                    }
                }
            }

            foreach(ExternalObjectType leftExternalObjectType in model.ExternalObjectTypes)
            {
                if(leftExternalObjectType.Name == xgrsTypeSameOrSub)
                {
                    foreach(ExternalObjectType rightExternalObjectType in model.ExternalObjectTypes)
                    {
                        if(rightExternalObjectType.Name == xgrsTypeBase)
                            return leftExternalObjectType.IsA(rightExternalObjectType);
                    }
                }
            }

            return false;
        }

        public static bool IsLockableType(string xgrsType, IGraphModel model)
        {
            if(xgrsType == "short" || xgrsType == "int" || xgrsType == "long"
                || xgrsType == "float" || xgrsType == "double"
                || xgrsType == "string"
                || xgrsType == "byte" || xgrsType == "sbyte"
                || xgrsType == "bool" || xgrsType == "boolean")
            {
                return false;
            }

            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.PackagePrefixedName == xgrsType)
                    return false;
            }

            return true;
        }

        public static string GetStorageKeyTypeName(VarType storage)
        {
            return storage.Type.GetGenericArguments()[0].FullName;
        }

        public static string GetStorageValueTypeName(VarType storage)
        {
            return storage.Type.GetGenericArguments()[1].FullName;
        }

        public static bool IsNumericType(String typeName)
        {
            switch(typeName)
            {
            case "byte": return true;
            case "short": return true;
            case "int": return true;
            case "long": return true;
            case "float": return true;
            case "double": return true;
            default: return false;
            }
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

        public static bool IsExternalObjectTypeIncludingObjectType(string typename, IGraphModel model)
        {
            for(int i = 0; i < model.ExternalObjectTypes.Length; ++i)
            {
                if(model.ExternalObjectTypes[i].Name == typename)
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

            object typeInsteadOfObject = InheritanceTypeIfInheritanceTypeValue(constant);
            return DotNetTypeToXgrsType(typeInsteadOfObject.GetType().Name, typeInsteadOfObject.GetType().FullName);
        }

        public static object InheritanceTypeIfInheritanceTypeValue(object constant)
        {
            if(constant is INode)
                return ((INode)constant).Type;
            else if(constant is IEdge)
                return ((IEdge)constant).Type;
            else if(constant is IObject)
                return ((IObject)constant).Type;
            else if(constant is ITransientObject)
                return ((ITransientObject)constant).Type;
            else
                return constant;
        }

        // ------------------------------------------------------------------------------------------------

        public static InheritanceType GetInheritanceType(String typeName, IGraphModel model)
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

            foreach(ObjectType objectType in model.ObjectModel.Types)
            {
                if(objectType.PackagePrefixedName == typeName)
                    return objectType;
            }

            foreach(TransientObjectType transientObjectType in model.TransientObjectModel.Types)
            {
                if(transientObjectType.PackagePrefixedName == typeName)
                    return transientObjectType;
            }

            return null;
        }

        public static GraphElementType GetGraphElementType(String typeName, IGraphModel model)
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

        public static ObjectType GetObjectType(String typeName, IGraphModel model)
        {
            foreach(ObjectType objectType in model.ObjectModel.Types)
            {
                if(objectType.PackagePrefixedName == typeName)
                    return objectType;
            }

            return null;
        }

        public static TransientObjectType GetTransientObjectType(String typeName, IGraphModel model)
        {
            foreach(TransientObjectType transientObjectType in model.TransientObjectModel.Types)
            {
                if(transientObjectType.PackagePrefixedName == typeName)
                    return transientObjectType;
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

        public static AttributeType XgrsTypeToAttributeType(String typeName, IGraphModel model)
        {
            AttributeKind kind = XgrsTypeToAttributeKind(typeName, model);
            if(typeName.StartsWith("set<"))
            {
                String valueTypeName = ExtractSrc(typeName);
                AttributeType valueType = XgrsTypeToAttributeType(valueTypeName, model);
                return new AttributeType("dummy", null, kind, null, valueType, null, null, null, null, null);
            }
            else if(typeName.StartsWith("map<"))
            {
                String keyTypeName = ExtractSrc(typeName);
                AttributeType keyType = XgrsTypeToAttributeType(keyTypeName, model);
                String valueTypeName = ExtractDst(typeName);
                AttributeType valueType = XgrsTypeToAttributeType(valueTypeName, model);
                return new AttributeType("dummy", null, kind, null, valueType, keyType, null, null, null, null);
            }
            else if(typeName.StartsWith("array<"))
            {
                String valueTypeName = ExtractSrc(typeName);
                AttributeType valueType = XgrsTypeToAttributeType(valueTypeName, model);
                return new AttributeType("dummy", null, kind, null, valueType, null, null, null, null, null);
            }
            else if(typeName.StartsWith("deque<"))
            {
                String valueTypeName = ExtractSrc(typeName);
                AttributeType valueType = XgrsTypeToAttributeType(valueTypeName, model);
                return new AttributeType("dummy", null, kind, null, valueType, null, null, null, null, null);
            }
            else
            {
                if(GetEnumAttributeType(typeName, model) != null)
        			return new AttributeType("dummy", null, kind, GetEnumAttributeType(typeName, model), null, null, null, null, null, null);
                else
    			    return new AttributeType("dummy", null, kind, null, null, null, null, null, null, null);
            }
        }

        public static AttributeKind XgrsTypeToAttributeKind(String typeName, IGraphModel model)
        {
            if(typeName.StartsWith("set<"))
                return AttributeKind.SetAttr;
            else if(typeName.StartsWith("map<"))
                return AttributeKind.MapAttr;
            else if(typeName.StartsWith("array<"))
                return AttributeKind.ArrayAttr;
            else if(typeName.StartsWith("deque<"))
                return AttributeKind.DequeAttr;

            switch(typeName)
            {
            case "byte":
                return AttributeKind.ByteAttr;
            case "short":
                return AttributeKind.ShortAttr;
            case "int":
                return AttributeKind.IntegerAttr;
            case "long":
                return AttributeKind.LongAttr;
            case "boolean":
                return AttributeKind.BooleanAttr;
            case "string":
                return AttributeKind.StringAttr;
            case "float":
                return AttributeKind.FloatAttr;
            case "double":
                return AttributeKind.DoubleAttr;
            case "object":
                return AttributeKind.ObjectAttr;
            case "graph":
                return AttributeKind.GraphAttr;
            }

            if(GetEnumAttributeType(typeName, model) != null)
                return AttributeKind.EnumAttr;

            if(GetNodeType(typeName, model) != null)
                return AttributeKind.NodeAttr;
            if(GetEdgeType(typeName, model) != null)
                return AttributeKind.EdgeAttr;
            if(GetObjectType(typeName, model) != null)
                return AttributeKind.InternalClassObjectAttr;
            if(GetTransientObjectType(typeName, model) != null)
                return AttributeKind.InternalClassTransientObjectAttr;

            return AttributeKind.ObjectAttr;
        }
    }
}
