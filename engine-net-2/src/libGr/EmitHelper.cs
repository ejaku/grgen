/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    public static class EmitHelper
    {
        /// <summary>
        /// Returns a string representation of the given dictionary
        /// </summary>
        /// <param name="setmap">The dictionary of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <returns>string representation of dictionary</returns>
        public static string ToString(IDictionary setmap, IGraph graph)
        {
            string type;
            string content;
            ToString(setmap, out type, out content, null, graph);
            return content;
        }

        /// <summary>
        /// Returns a string representation of the given List
        /// </summary>
        /// <param name="array">The List of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <returns>string representation of List</returns>
        public static string ToString(IList array, IGraph graph)
        {
            string type;
            string content;
            ToString(array, out type, out content, null, graph);
            return content;
        }

        /// <summary>
        /// Returns a string representation of the given Deque
        /// </summary>
        /// <param name="deque">The Deque of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <returns>string representation of Deque</returns>
        public static string ToString(IDeque deque, IGraph graph)
        {
            string type;
            string content;
            ToString(deque, out type, out content, null, graph);
            return content;
        }

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
                if (valueType == typeof(SetValueType))
                {
                    type = "set<" + keyType.Name + ">";
                    bool first = true;
                    foreach (DictionaryEntry entry in setmap)
                    {
                        if (first) { sb.Append(ToString(entry.Key, attrValueType, graph)); first = false; }
                        else { sb.Append(","); sb.Append(ToString(entry.Key, attrValueType, graph)); }
                    }
                }
                else
                {
                    type = "map<" + keyType.Name + "," + valueType.Name + ">";
                    bool first = true;
                    foreach (DictionaryEntry entry in setmap)
                    {
                        if (first) { sb.Append(ToString(entry.Key, attrKeyType, graph)); sb.Append("->"); sb.Append(ToString(entry.Value, attrValueType, graph)); first = false; }
                        else { sb.Append(","); sb.Append(ToString(entry.Key, attrKeyType, graph)); sb.Append("->"); sb.Append(ToString(entry.Value, attrValueType, graph)); }
                    }
                }
            } else {
                type = "<INVALID>";
            }

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
                    if(first) { sb.Append(ToString(entry, attrValueType, graph)); first = false; }
                    else { sb.Append(","); sb.Append(ToString(entry, attrValueType, graph)); }
                }
            }
            else
            {
                type = "<INVALID>";
            }

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
                    if(first) { sb.Append(ToString(entry, attrValueType, graph)); first = false; }
                    else { sb.Append(","); sb.Append(ToString(entry, attrValueType, graph)); }
                }
            }
            else
            {
                type = "<INVALID>";
            }

            sb.Append("[");
            content = sb.ToString();
        }

        /// <summary>
        /// Returns a string representation of the given dictionary
        /// after the given operation with the given parameters was applied
        /// </summary>
        /// <param name="setmap">The base dictionary of the operation</param>
        /// <param name="changeType">The type of the change operation</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.</param>
        /// <param name="type">The type as string, e.g set<int> or map<string,boolean> </param>
        /// <param name="content">The content as string, e.g. { 42, 43 } or { "foo"->true, "bar"->false } </param>
        /// <param name="attrType">The attribute type of the dictionary</param>
        /// <param name="graph">The graph with the model and the element names</param>
        public static void ToString(IDictionary setmap,
            AttributeChangeType changeType, Object newValue, Object keyValue,
            out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            if(changeType==AttributeChangeType.PutElement)
            {
                Type keyType;
                Type valueType;
                ContainerHelper.GetDictionaryTypes(setmap, out keyType, out valueType);

                if(valueType==typeof(SetValueType))
                {
                    ToString(setmap, out type, out content, attrType, graph);
                    content += "|" + ToString(newValue, attrType.ValueType, graph);
                }
                else
                {
                    ToString(setmap, out type, out content, attrType, graph);
                    content += "|" + ToString(keyValue, attrType.KeyType, graph) + "->" + ToString(newValue, attrType.ValueType, graph);
                }
            }
            else if(changeType==AttributeChangeType.RemoveElement)
            {
                Type keyType;
                Type valueType;
                ContainerHelper.GetDictionaryTypes(setmap, out keyType, out valueType);

                if(valueType==typeof(SetValueType))
                {
                    ToString(setmap, out type, out content, attrType, graph);
                    content += "\\" + ToString(newValue, attrType.ValueType, graph);
                }
                else
                {
                    ToString(setmap, out type, out content, attrType, graph);
                    content += "\\" + ToString(keyValue, attrType.KeyType, graph) + "->.";
                }
            }
            else // changeType==AttributeChangeType.Assign
            {
                ToString((IDictionary)newValue, out type, out content, attrType, graph);
            }
        }

        /// <summary>
        /// Returns a string representation of the given List
        /// after the given operation with the given parameters was applied
        /// </summary>
        /// <param name="array">The base List of the operation</param>
        /// <param name="changeType">The type of the change operation</param>
        /// <param name="newValue">The new value to be inserted/added if changeType==PutElement on array.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
        /// <param name="keyValue">The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
        /// <param name="type">The type as string, e.g array<int></param>
        /// <param name="content">The content as string, e.g. [ 42, 43 ] </param>
        /// <param name="attrType">The attribute type of the List</param>
        /// <param name="graph">The graph with the model and the element names</param>
        public static void ToString(IList array,
            AttributeChangeType changeType, Object newValue, Object keyValue,
            out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            if(changeType == AttributeChangeType.PutElement)
            {
                Type valueType;
                ContainerHelper.GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += ".add(" + ToString(newValue, attrType.ValueType, graph);
                if(keyValue != null) content += ", " + keyValue.ToString() + ")";
                else content += ")";
            }
            else if(changeType == AttributeChangeType.RemoveElement)
            {
                Type valueType;
                ContainerHelper.GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += ".rem(";
                if(keyValue != null) content += keyValue.ToString() + ")";
                else content += ")";
            }
            else if(changeType == AttributeChangeType.AssignElement)
            {
                Type valueType;
                ContainerHelper.GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += "[" + keyValue.ToString() + "] = " + ToString(newValue, attrType.ValueType, graph);
            }
            else // changeType==AttributeChangeType.Assign
            {
                ToString((IList)newValue, out type, out content, attrType, graph);
            }
        }

        /// <summary>
        /// Returns a string representation of the given Deque
        /// after the given operation with the given parameters was applied
        /// </summary>
        /// <param name="deque">The base Deque of the operation</param>
        /// <param name="changeType">The type of the change operation</param>
        /// <param name="newValue">The new value to be inserted/added if changeType==PutElement on deque.</param>
        /// <param name="type">The type as string, e.g deque<int></param>
        /// <param name="content">The content as string, e.g. ] 42, 43 [ </param>
        /// <param name="attrType">The attribute type of the Deque</param>
        /// <param name="graph">The graph with the model and the element names</param>
        public static void ToString(IDeque deque,
            AttributeChangeType changeType, Object newValue,
            out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            if(changeType == AttributeChangeType.PutElement)
            {
                Type valueType;
                ContainerHelper.GetDequeType(deque, out valueType);
                ToString(deque, out type, out content, attrType, graph);
                content += ".add(" + ToString(newValue, attrType.ValueType, graph) + ")";
            }
            else if(changeType == AttributeChangeType.RemoveElement)
            {
                Type valueType;
                ContainerHelper.GetDequeType(deque, out valueType);
                ToString(deque, out type, out content, attrType, graph);
                content += ".rem()";
            }
            else // changeType==AttributeChangeType.Assign
            {
                ToString((IDeque)newValue, out type, out content, attrType, graph);
            }
        }

        /// <summary>
        /// Returns a string representation of the given scalar value
        /// </summary>
        /// <param name="value">The scalar value of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <returns>string representation of scalar value</returns>
        public static string ToString(object value, IGraph graph)
        {
            string type;
            string content;
            ToString(value, out type, out content, null, graph);
            return content;
        }

        /// <summary>
        /// Returns a string representation of the given value, which must be not null (for emit,record)
        /// </summary>
        /// <param name="value">The value of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <returns>string representation of the value</returns>
        public static string ToStringNonNull(object value, IGraph graph)
        {
            if(value is IDictionary)
                return ToString((IDictionary)value, graph);
            else if(value is IList)
                return ToString((IList)value, graph);
            else if(value is IDeque)
                return ToString((IDeque)value, graph);
            else
                return ToString(value, graph);
        }

        /// <summary>
        /// Returns a string representation of the given scalar value
        /// </summary>
        /// <param name="value">The scalar value of which to get the string representation</param>
        /// <param name="type">The type as string, e.g int,string,Foo </param>
        /// <param name="content">The content as string, e.g. 42,"foo",bar } </param>
        /// <param name="attrType">The attribute type of the value (may be null)</param>
        /// <param name="graph">The graph with the model and the element names</param>
        public static void ToString(object value, out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            if (attrType == null)
            {
                if(value == null)
                {
                    type = "<INVALID>";
                    content = ToString(value, attrType, graph);
                    return;
                }

                Debug.Assert(value.GetType().Name != "Dictionary`2" && value.GetType().Name != "List`1" && value.GetType().Name != "Deque`1");
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
                        {
                            type = ((IGraphElement)value).Type.Name;
                        }
                        break;
                }

                if(type != "object")
                    content = ToString(value, attrType, graph);
                else
                    content = ToStringObject(value, attrType, graph);
                return;
            }

            Debug.Assert(attrType.Kind != AttributeKind.SetAttr && attrType.Kind != AttributeKind.MapAttr);
            switch (attrType.Kind)
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
        /// Returns a string representation of the given scalar value
        /// </summary>
        /// <param name="value">The scalar of which to get the string representation</param>
        /// <param name="attrType">The attribute type</param>
        /// <param name="graph">The graph with the model and the element names</param>
        /// <returns>String representation of the scalar.</returns>
        private static string ToString(object value, AttributeType attrType, IGraph graph)
        {
            // enums are bitches, sometimes ToString gives the symbolic name, sometimes only the integer value
            // we always want the symbolic name, enforce this here
            if(attrType!=null && attrType.Kind==AttributeKind.EnumAttr)
            {
                Debug.Assert(attrType.Kind!=AttributeKind.SetAttr && attrType.Kind!=AttributeKind.MapAttr);
                EnumAttributeType enumAttrType = attrType.EnumType;
                EnumMember member = enumAttrType[(int)value];
                if(member!=null) return member.Name;
            }
            if(graph!=null && value is Enum)
            {
                Debug.Assert(value.GetType().Name != "Dictionary`2" && value.GetType().Name != "List`1" && value.GetType().Name != "Deque`1");
                foreach(EnumAttributeType enumAttrType in graph.Model.EnumAttributeTypes)
                {
                    if(value.GetType()==enumAttrType.EnumType)
                    {
                        EnumMember member = enumAttrType[(int)value];
                        if(member != null) return member.Name;
                        else break;
                    }
                }
            }

            // for set/map entries of node/edge type return the persistent name
            if(attrType!=null && (attrType.Kind==AttributeKind.NodeAttr || attrType.Kind==AttributeKind.EdgeAttr))
            {
                if(graph!=null && value!=null)
                {
                    return ((INamedGraph)graph).GetElementName((IGraphElement)value);
                }
            }
            if(value is IGraphElement)
            {
                if(graph!=null)
                {
                    return ((INamedGraph)graph).GetElementName((IGraphElement)value);
                }
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
            if(graph!=null)
                return graph.Model.Emit(value, attrType, graph);
            else
                return value!=null ? value.ToString() : "";
        }

        /// <summary>
        /// Returns a string representation of the given value, might be a scalar, a dictionary, a list, or a deque
        /// </summary>
        /// <param name="value">The value of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <returns>string representation of value</returns>
        public static string ToStringAutomatic(object value, IGraph graph)
        {
            if(value is IDictionary)
            {
                string type;
                string content;
                ToString((IDictionary)value, out type, out content, null, graph);
                return content;
            }
            else if(value is IList)
            {
                string type;
                string content;
                ToString((IList)value, out type, out content, null, graph);
                return content;
            }
            else if(value is IDeque)
            {
                string type;
                string content;
                ToString((IDeque)value, out type, out content, null, graph);
                return content;
            }
            else
            {
                string type;
                string content;
                ToString(value, out type, out content, null, graph);
                return content;
            }
        }
    }
}
