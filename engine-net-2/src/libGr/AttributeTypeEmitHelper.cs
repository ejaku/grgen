/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class EmitHelper
    {
        /// <summary>
        /// Returns a string representation of the given dictionary
        /// </summary>
        /// <param name="setmap">The dictionary of which to get the string representation</param>
        /// <param name="type">The type as string, e.g <![CDATA[set<int>]]> or <![CDATA[set<int>map<string,boolean>]]> </param>
        /// <param name="content">The content as string, e.g. { 42, 43 } or { "foo"->true, "bar"->false } </param>
        /// <param name="attrType">The attribute type of the dictionary if available, otherwise null</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        public static void ToString(IDictionary setmap, out string type, out string content,
            AttributeType attrType, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
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
                    AppendSet(sb, setmap, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                }
                else
                {
                    type = "map<" + keyType.Name + "," + valueType.Name + ">";
                    AppendMap(sb, setmap, attrKeyType, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                }
            }
            else
                type = "<INVALID>";

            sb.Append("}");
            content = sb.ToString();
        }

        private static void AppendSet(StringBuilder sb, IDictionary set, AttributeType attrValueType, IGraph graph, 
            bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            bool first = true;
            foreach(DictionaryEntry entry in set)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");

                if(attrValueType != null)
                    sb.Append(ToString(entry.Key, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv));
                else
                    sb.Append(ToStringAutomatic(entry.Key, graph, firstLevelObjectEmitted, nameToObject, procEnv));
            }
        }

        private static void AppendMap(StringBuilder sb, IDictionary setmap, 
            AttributeType attrKeyType, AttributeType attrValueType, IGraph graph,
            bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            bool first = true;
            foreach(DictionaryEntry entry in setmap)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");

                if(attrKeyType != null)
                    sb.Append(ToString(entry.Key, attrKeyType, graph, firstLevelObjectEmitted, nameToObject, procEnv));
                else
                    sb.Append(ToStringAutomatic(entry.Key, graph, firstLevelObjectEmitted, nameToObject, procEnv));
                sb.Append("->");
                if(attrValueType != null)
                    sb.Append(ToString(entry.Value, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv));
                else
                    sb.Append(ToStringAutomatic(entry.Value, graph, firstLevelObjectEmitted, nameToObject, procEnv));
            }
        }

        /// <summary>
        /// Returns a string representation of the given List
        /// </summary>
        /// <param name="array">The List of which to get the string representation</param>
        /// <param name="type">The type as string, e.g <![CDATA[array<int>]]></param>
        /// <param name="content">The content as string, e.g. [ 42, 43 ]</param>
        /// <param name="attrType">The attribute type of the array if available, otherwise null</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        public static void ToString(IList array, out string type, out string content,
            AttributeType attrType, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            Type valueType;
            ContainerHelper.GetListType(array, out valueType);

            StringBuilder sb = new StringBuilder(256);
            sb.Append("[");

            AttributeType attrValueType = attrType != null ? attrType.ValueType : null;

            if(array != null)
            {
                type = "array<" + valueType.Name + ">";
                AppendArray(sb, array, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            }
            else
                type = "<INVALID>";

            sb.Append("]");
            content = sb.ToString();
        }

        private static void AppendArray(StringBuilder sb, IList array, AttributeType attrValueType, IGraph graph,
            bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            bool first = true;
            foreach(object entry in array)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");
                if(attrValueType != null)
                    sb.Append(ToString(entry, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv));
                else
                    sb.Append(ToStringAutomatic(entry, graph, firstLevelObjectEmitted, nameToObject, procEnv));
            }
        }

        /// <summary>
        /// Returns a string representation of the given Deque
        /// </summary>
        /// <param name="deque">The Deque of which to get the string representation</param>
        /// <param name="type">The type as string, e.g <![CDATA[deque<int>]]></param>
        /// <param name="content">The content as string, e.g. ] 42, 43 [</param>
        /// <param name="attrType">The attribute type of the deque if available, otherwise null</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        public static void ToString(IDeque deque, out string type, out string content,
            AttributeType attrType, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            Type valueType;
            ContainerHelper.GetDequeType(deque, out valueType);

            StringBuilder sb = new StringBuilder(256);
            sb.Append("]");

            AttributeType attrValueType = attrType != null ? attrType.ValueType : null;

            if(deque != null)
            {
                type = "deque<" + valueType.Name + ">";
                AppendDeque(sb, deque, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            }
            else
                type = "<INVALID>";

            sb.Append("[");
            content = sb.ToString();
        }

        private static void AppendDeque(StringBuilder sb, IDeque deque, AttributeType attrValueType, IGraph graph,
            bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            bool first = true;
            foreach(object entry in deque)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");

                if(attrValueType != null)
                    sb.Append(ToString(entry, attrValueType, graph, firstLevelObjectEmitted, nameToObject, procEnv));
                else
                    sb.Append(ToStringAutomatic(entry, graph, firstLevelObjectEmitted, nameToObject, procEnv));
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
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        public static void ToString(object value, out string type, out string content,
            AttributeType attrType, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            if(attrType == null)
            {
                ToString(value, out type, out content, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return;
            }

            Debug.Assert(attrType.Kind != AttributeKind.SetAttr && attrType.Kind != AttributeKind.MapAttr
                && attrType.Kind != AttributeKind.ArrayAttr && attrType.Kind != AttributeKind.DequeAttr);
            type = TypesHelper.AttributeTypeToXgrsType(attrType);

            if(type == "object")
                content = ToStringObject(value, attrType, graph);
            else
                content = ToString(value, attrType, graph, firstLevelObjectEmitted, nameToObject, procEnv);
        }

        private static void ToString(object value, out string type, out string content, IGraph graph,
            bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            if(value == null)
            {
                type = "<INVALID>";
                content = ToString(value, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return;
            }

            if(value is IMatch)
            {
                type = "IMatch";
                content = ToString((IMatch)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return;
            }

            if(value is IObject)
            {
                type = ((IObject)value).Type.PackagePrefixedName;
                content = ToString((IObject)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return;
            }

            if(value is ITransientObject)
            {
                type = ((ITransientObject)value).Type.PackagePrefixedName;
                content = ToString((ITransientObject)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return;
            }

            Debug.Assert(value.GetType().Name != "Dictionary`2"
                && value.GetType().Name != "List`1" && value.GetType().Name != "Deque`1");
            type = TypesHelper.DotNetTypeToXgrsType(value.GetType().Name, value.GetType().FullName);

            foreach(ExternalObjectType externalObjectType in graph.Model.ExternalObjectTypes)
            {
                if(externalObjectType.Name == value.GetType().Name)
                    type = "object";
            }

            if(type == "object")
                content = ToStringObject(value, null, graph);
            else
                content = ToString(value, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
        }

        /// <summary>
        /// Returns a string representation of the given non-container value
        /// </summary>
        /// <param name="value">The scalar of which to get the string representation</param>
        /// <param name="attrType">The attribute type (may be null)</param>
        /// <param name="graph">The graph with the model and the element names</param>
        /// <returns>String representation of the scalar.</returns>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        private static string ToString(object value, AttributeType attrType, IGraph graph,
            bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            if(value is IMatch)
                return ToString((IMatch)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            if(value is IObject)
                return ToString((IObject)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            if(value is ITransientObject)
                return ToString((ITransientObject)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);

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
            if(value is IGraph)
            {
                IGraph graphValue = (IGraph)value;
                return "graph{name:" + graphValue.Name + ",id:" + graphValue.GraphId + ",changes:" + graphValue.ChangesCounter + "}";
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
