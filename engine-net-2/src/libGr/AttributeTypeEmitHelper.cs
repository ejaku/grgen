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
                    AppendSet(sb, setmap, attrValueType, graph);
                }
                else
                {
                    type = "map<" + keyType.Name + "," + valueType.Name + ">";
                    AppendMap(sb, setmap, attrKeyType, attrValueType, graph);
                }
            }
            else
                type = "<INVALID>";

            sb.Append("}");
            content = sb.ToString();
        }

        private static void AppendSet(StringBuilder sb, IDictionary set, AttributeType attrValueType, IGraph graph)
        {
            bool first = true;
            foreach(DictionaryEntry entry in set)
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

        private static void AppendMap(StringBuilder sb, IDictionary setmap, 
            AttributeType attrKeyType, AttributeType attrValueType, IGraph graph)
        {
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
                AppendArray(sb, array, attrValueType, graph);
            }
            else
                type = "<INVALID>";

            sb.Append("]");
            content = sb.ToString();
        }

        private static void AppendArray(StringBuilder sb, IList array, AttributeType attrValueType, IGraph graph)
        {
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
                AppendDeque(sb, deque, attrValueType, graph);
            }
            else
                type = "<INVALID>";

            sb.Append("[");
            content = sb.ToString();
        }

        private static void AppendDeque(StringBuilder sb, IDeque deque, AttributeType attrValueType, IGraph graph)
        {
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
                ToString(value, out type, out content, graph);
                return;
            }

            Debug.Assert(attrType.Kind != AttributeKind.SetAttr && attrType.Kind != AttributeKind.MapAttr
                && attrType.Kind != AttributeKind.ArrayAttr && attrType.Kind != AttributeKind.DequeAttr);
            type = TypesHelper.AttributeTypeToXgrsType(attrType);

            if(type == "object")
                content = ToStringObject(value, attrType, graph);
            else
                content = ToString(value, attrType, graph);
        }

        private static void ToString(object value, out string type, out string content, IGraph graph)
        {
            if(value == null)
            {
                type = "<INVALID>";
                content = ToString(value, null, graph);
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
            type = TypesHelper.DotNetTypeToXgrsType(value.GetType().Name, value.GetType().FullName);

            foreach(ExternalType externalType in graph.Model.ExternalTypes)
            {
                if(externalType.Name == value.GetType().Name)
                    type = "object";
            }

            if(type == "object")
                content = ToStringObject(value, null, graph);
            else
                content = ToString(value, null, graph);
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
