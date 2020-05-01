/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Text;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class EmitHelper
    {
        /// <summary>
        /// Returns a string representation of the given dictionary
        /// </summary>
        /// <param name="setmap">The dictionary of which to get the string representation</param>
        /// <param name="type">The type as string, e.g set<int> or map<string,boolean> </param>
        /// <param name="content">The content as string, e.g. { 42, 43 } or { "foo"->true, "bar"->false } </param>
        /// <param name="attrType">The attribute type of the dictionary if available, otherwise null</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        public static void ToString(IDictionary setmap, out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(setmap, out keyType, out valueType);

            StringBuilder sb = new StringBuilder(256);
            sb.Append("{");

            AttributeType attrValueType = attrType != null ? attrType.ValueType : null;
            AttributeType attrKeyType = attrType != null ? attrType.KeyType : null;

            if(setmap != null)
            {
                if(valueType == typeof(SetValueType))
                {
                    type = "set<" + keyType.Name + ">";
                    bool first = true;
                    foreach(DictionaryEntry entry in setmap)
                    {
                        if(first)
                            first = false;
                        else
                            sb.Append(",");

                        if(attrValueType != null)
                            sb.Append(ToString(entry.Key, attrValueType, graph));
                        else
                            sb.Append(ToStringAutomatic(entry.Key, graph));
                    }
                }
                else
                {
                    type = "map<" + keyType.Name + "," + valueType.Name + ">";
                    bool first = true;
                    foreach(DictionaryEntry entry in setmap)
                    {
                        if(first)
                            first = false;
                        else
                            sb.Append(",");

                        if(attrKeyType != null)
                            sb.Append(ToString(entry.Key, attrKeyType, graph));
                        else
                            sb.Append(ToStringAutomatic(entry.Key, graph));
                        sb.Append("->");
                        if(attrValueType != null)
                            sb.Append(ToString(entry.Value, attrValueType, graph));
                        else
                            sb.Append(ToStringAutomatic(entry.Value, graph));
                    }
                }
            }
            else
                type = "<INVALID>";

            sb.Append("}");
            content = sb.ToString();
        }

        /// <summary>
        /// Returns a string representation of the given List
        /// </summary>
        /// <param name="array">The List of which to get the string representation</param>
        /// <param name="type">The type as string, e.g array<int></param>
        /// <param name="content">The content as string, e.g. [ 42, 43 ]</param>
        /// <param name="attrType">The attribute type of the array if available, otherwise null</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        public static void ToString(IList array, out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            Type valueType;
            ContainerHelper.GetListType(array, out valueType);

            StringBuilder sb = new StringBuilder(256);
            sb.Append("[");

            AttributeType attrValueType = attrType != null ? attrType.ValueType : null;

            if(array != null)
            {
                type = "array<" + valueType.Name + ">";
                bool first = true;
                foreach(Object entry in array)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    if(attrValueType != null)
                        sb.Append(ToString(entry, attrValueType, graph));
                    else
                        sb.Append(ToStringAutomatic(entry, graph));
                }
            }
            else
                type = "<INVALID>";

            sb.Append("]");
            content = sb.ToString();
        }

        /// <summary>
        /// Returns a string representation of the given Deque
        /// </summary>
        /// <param name="deque">The Deque of which to get the string representation</param>
        /// <param name="type">The type as string, e.g deque<int></param>
        /// <param name="content">The content as string, e.g. ] 42, 43 [</param>
        /// <param name="attrType">The attribute type of the deque if available, otherwise null</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        public static void ToString(IDeque deque, out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            Type valueType;
            ContainerHelper.GetDequeType(deque, out valueType);

            StringBuilder sb = new StringBuilder(256);
            sb.Append("]");

            AttributeType attrValueType = attrType != null ? attrType.ValueType : null;

            if(deque != null)
            {
                type = "deque<" + valueType.Name + ">";
                bool first = true;
                foreach(Object entry in deque)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");

                    if(attrValueType != null)
                        sb.Append(ToString(entry, attrValueType, graph));
                    else
                        sb.Append(ToStringAutomatic(entry, graph));
                }
            }
            else
                type = "<INVALID>";

            sb.Append("[");
            content = sb.ToString();
        }

        /// <summary>
        /// Returns a string representation of the given non-container value
        /// </summary>
        /// <param name="value">The scalar value of which to get the string representation</param>
        /// <param name="type">The type as string, e.g int,string,Foo </param>
        /// <param name="content">The content as string, e.g. 42,"foo",bar } </param>
        /// <param name="attrType">The attribute type of the value (may be null)</param>
        /// <param name="graph">The graph with the model and the element names</param>
        public static void ToString(object value, out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            if(attrType == null)
            {
                if(value == null)
                {
                    type = "<INVALID>";
                    content = ToString(value, attrType, graph);
                    return;
                }

                if(value is IMatch)
                {
                    type = "IMatch";
                    content = ToString((IMatch)value, graph);
                    return;
                }

                Debug.Assert(value.GetType().Name != "Dictionary`2" 
                    && value.GetType().Name != "List`1" && value.GetType().Name != "Deque`1");
                switch(value.GetType().Name)
                {
                case "SByte": type = "sbyte"; break;
                case "Int16": type = "short"; break;
                case "Int32": type = "int"; break;
                case "Int64": type = "long"; break;
                case "Boolean": type = "bool"; break;
                case "String": type = "string"; break;
                case "Single": type = "float"; break;
                case "Double": type = "double"; break;
                case "de.unika.ipd.grGen.libGr.IGraph": type = "graph"; break;
                default:
                    type = "object";
                    if(graph != null && value is Enum)
                    {
                        foreach(EnumAttributeType enumAttrType in graph.Model.EnumAttributeTypes)
                        {
                            if(value.GetType() == enumAttrType.EnumType)
                            {
                                type = enumAttrType.Name;
                                break;
                            }
                        }
                    }
                    else if(value is IGraphElement)
                        type = ((IGraphElement)value).Type.Name;
                    break;
                }

                if(type != "object")
                    content = ToString(value, attrType, graph);
                else
                    content = ToStringObject(value, attrType, graph);
                return;
            }

            Debug.Assert(attrType.Kind != AttributeKind.SetAttr && attrType.Kind != AttributeKind.MapAttr
                && attrType.Kind != AttributeKind.ArrayAttr && attrType.Kind != AttributeKind.DequeAttr);
            switch(attrType.Kind)
            {
            case AttributeKind.ByteAttr: type = "sbyte"; break;
            case AttributeKind.ShortAttr: type = "short"; break;
            case AttributeKind.IntegerAttr: type = "int"; break;
            case AttributeKind.LongAttr: type = "long"; break;
            case AttributeKind.BooleanAttr: type = "bool"; break;
            case AttributeKind.StringAttr: type = "string"; break;
            case AttributeKind.EnumAttr: type = attrType.EnumType.Name; break;
            case AttributeKind.FloatAttr: type = "float"; break;
            case AttributeKind.DoubleAttr: type = "double"; break;
            case AttributeKind.ObjectAttr: type = "object"; break;
            case AttributeKind.GraphAttr: type = "GRGEN_LIBGR.IGraph"; break;
            case AttributeKind.NodeAttr: type = attrType.TypeName; break;
            case AttributeKind.EdgeAttr: type = attrType.TypeName; break;
            default: type = "<INVALID>"; break;
            }

            if(type != "object")
                content = ToString(value, attrType, graph);
            else
                content = ToStringObject(value, attrType, graph);
        }

        /// <summary>
        /// Returns a string representation of the given non-container value
        /// </summary>
        /// <param name="value">The scalar of which to get the string representation</param>
        /// <param name="attrType">The attribute type (may be null)</param>
        /// <param name="graph">The graph with the model and the element names</param>
        /// <returns>String representation of the scalar.</returns>
        private static string ToString(object value, AttributeType attrType, IGraph graph)
        {
            if(value is IMatch)
                return ToString((IMatch)value, graph);

            // enums are bitches, sometimes ToString gives the symbolic name, sometimes only the integer value
            // we always want the symbolic name, enforce this here
            if(attrType != null && attrType.Kind == AttributeKind.EnumAttr)
            {
                Debug.Assert(attrType.Kind != AttributeKind.SetAttr && attrType.Kind != AttributeKind.MapAttr);
                EnumAttributeType enumAttrType = attrType.EnumType;
                EnumMember member = enumAttrType[(int)value];
                if(member != null)
                    return member.Name;
            }
            if(graph != null && value is Enum)
            {
                Debug.Assert(value.GetType().Name != "Dictionary`2" 
                    && value.GetType().Name != "List`1" && value.GetType().Name != "Deque`1");
                foreach(EnumAttributeType enumAttrType in graph.Model.EnumAttributeTypes)
                {
                    if(value.GetType() == enumAttrType.EnumType)
                    {
                        EnumMember member = enumAttrType[(int)value];
                        if(member != null)
                            return member.Name;
                        else
                            break;
                    }
                }
            }

            // for set/map entries of node/edge type return the persistent name
            if(attrType != null && (attrType.Kind == AttributeKind.NodeAttr || attrType.Kind == AttributeKind.EdgeAttr))
            {
                if(graph != null && value != null)
                    return ((INamedGraph)graph).GetElementName((IGraphElement)value);
            }
            if(value is IGraphElement)
            {
                if(graph != null)
                    return ((INamedGraph)graph).GetElementName((IGraphElement)value);
            }

            // we return "" for null as null is a valid string denoting the empty string in GrGen (dubious performance optimization)
            if(value == null)
                return "";
            else
            {
                if(value is double)
                    return ((double)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
                else if(value is float)
                    return ((float)value).ToString(System.Globalization.CultureInfo.InvariantCulture) + "f";
                else
                    return value.ToString();
            }
        }

        /// <summary>
        /// Returns a string representation of the given scalar value of attribute kind object,
        /// i.e. an externally defined type, maybe registered as external type to GrGen, maybe not, only defined as object
        /// </summary>
        /// <param name="value">The scalar of which to get the string representation</param>
        /// <param name="attrType">The attribute type</param>
        /// <param name="graph">The graph with the model and the element names</param>
        /// <returns>String representation of the scalar.</returns>
        private static string ToStringObject(object value, AttributeType attrType, IGraph graph)
        {
            if(graph != null)
                return graph.Model.Emit(value, attrType, graph);
            else
                return value != null ? value.ToString() : "";
        }
    }
}
