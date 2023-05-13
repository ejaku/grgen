/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class EmitHelper
    {
        /// <summary>
        /// Returns a string representation of the given value, might be a scalar, a dictionary, a list, or a deque
        /// </summary>
        /// <param name="value">The value of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        /// <returns>string representation of value</returns>
        public static string ToStringAutomatic(object value, IGraph graph,
            bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            if(value is IDictionary)
            {
                string type;
                string content;
                ToString((IDictionary)value, out type, out content, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return content;
            }
            else if(value is IList)
            {
                string type;
                string content;
                ToString((IList)value, out type, out content, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return content;
            }
            else if(value is IDeque)
            {
                string type;
                string content;
                ToString((IDeque)value, out type, out content, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return content;
            }
            else
            {
                string type;
                string content;
                ToString(value, out type, out content, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
                return content;
            }
        }

        /// <summary>
        /// Returns a string representation of the given value, which must be not null (for emit,record)
        /// </summary>
        /// <param name="value">The value of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        /// <returns>string representation of the value</returns>
        public static string ToStringNonNull(object value, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            if(value is IDictionary)
                return ToString((IDictionary)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            else if(value is IList)
                return ToString((IList)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            else if(value is IDeque)
                return ToString((IDeque)value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            else
                return ToString(value, graph, firstLevelObjectEmitted, nameToObject, procEnv);
        }

        /// <summary>
        /// Returns a string representation of the given dictionary
        /// </summary>
        /// <param name="setmap">The dictionary of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        /// <returns>string representation of dictionary</returns>
        public static string ToString(IDictionary setmap, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            string type;
            string content;
            ToString(setmap, out type, out content, null, graph, firstLevelObjectEmitted,
                nameToObject, procEnv);
            return content;
        }

        /// <summary>
        /// Returns a string representation of the given List
        /// </summary>
        /// <param name="array">The List of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        /// <returns>string representation of List</returns>
        public static string ToString(IList array, IGraph graph, bool firstLevelObjectEmitted, 
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            string type;
            string content;
            ToString(array, out type, out content, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            return content;
        }

        /// <summary>
        /// Returns a string representation of the given Deque
        /// </summary>
        /// <param name="deque">The Deque of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        /// <returns>string representation of Deque</returns>
        public static string ToString(IDeque deque, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            string type;
            string content;
            ToString(deque, out type, out content, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            return content;
        }

        /// <summary>
        /// Returns a string representation of the given scalar value
        /// </summary>
        /// <param name="value">The scalar value of which to get the string representation</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        /// <param name="firstLevelObjectEmitted">Prevents emitting of further objects and thus infinite regressions</param>
        /// <param name="nameToObject">If not null, the names of visited objects are added</param>
        /// <param name="procEnv">If not null, the processing environment is used for transient object unique id emitting and fetching</param>
        /// <returns>string representation of scalar value</returns>
        public static string ToString(object value, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            string type;
            string content;
            ToString(value, out type, out content, null, graph, firstLevelObjectEmitted, nameToObject, procEnv);
            return content;
        }

        public static string Clip(string potentiallyLargeString, int maxLength)
        {
            if(potentiallyLargeString.Length < maxLength)
                return potentiallyLargeString;
            else
                return potentiallyLargeString.Substring(0, maxLength - 3) + "...";
        }

        private static string ToString(IMatch match, IGraph graph, bool firstLevelObjectEmitted, IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            StringBuilder sb = new StringBuilder();
            string matchName = match.Pattern != null ? match.Pattern.PackagePrefixedName : match.MatchClass.PackagePrefixedName; 
            sb.Append("match<" + matchName + ">{");
            bool first = true;
            IPatternNode[] nodes = match.Pattern != null ? match.Pattern.Nodes : match.MatchClass.Nodes;
            foreach(IPatternNode patternNode in nodes)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(patternNode.UnprefixedName);
                sb.Append(":");
                sb.Append(EmitHelper.ToStringAutomatic(match.getNode(patternNode.UnprefixedName), graph, firstLevelObjectEmitted,
                    nameToObject, procEnv));
            }
            IPatternEdge[] edges = match.Pattern != null ? match.Pattern.Edges : match.MatchClass.Edges;
            foreach(IPatternEdge patternEdge in edges)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(patternEdge.UnprefixedName);
                sb.Append(":");
                sb.Append(EmitHelper.ToStringAutomatic(match.getEdge(patternEdge.UnprefixedName), graph, firstLevelObjectEmitted,
                    nameToObject, procEnv));
            }
            IPatternVariable[] variables = match.Pattern != null ? match.Pattern.Variables : match.MatchClass.Variables;
            foreach(IPatternVariable patternVar in variables)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(patternVar.UnprefixedName);
                sb.Append(":");
                sb.Append(EmitHelper.ToStringAutomatic(match.getVariable(patternVar.UnprefixedName), graph, firstLevelObjectEmitted,
                    nameToObject, procEnv));
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string ToString(IObject value, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            StringBuilder sb = new StringBuilder();
            string objectType = value.Type.PackagePrefixedName;
            sb.Append(objectType);
            sb.Append("{");
            sb.Append("%:" + value.GetObjectName());
            if(nameToObject != null)
            {
                if(!nameToObject.ContainsKey(value.GetObjectName()))
                    nameToObject.Add(value.GetObjectName(), value);
            }
            if(!firstLevelObjectEmitted)
            {
                foreach(AttributeType attrType in value.Type.AttributeTypes)
                {
                    sb.Append(",");
                    sb.Append(attrType.Name);
                    sb.Append(":");
                    sb.Append(EmitHelper.ToStringAutomatic(value.GetAttribute(attrType.Name), graph, true,
                        nameToObject, procEnv));
                }
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string ToString(ITransientObject value, IGraph graph, bool firstLevelObjectEmitted,
            IDictionary<string, IObject> nameToObject, IGraphProcessingEnvironment procEnv)
        {
            StringBuilder sb = new StringBuilder();
            string transientObjectType = value.Type.PackagePrefixedName;
            sb.Append(transientObjectType);
            if(procEnv != null || !firstLevelObjectEmitted)
                sb.Append("{");
            bool first = true;
            if(procEnv != null)
            {
                sb.Append("&:" + procEnv.Graph.GlobalVariables.GetUniqueId(value));
                first = false;
            }
            if(!firstLevelObjectEmitted)
            {
                foreach(AttributeType attrType in value.Type.AttributeTypes)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(attrType.Name);
                    sb.Append(":");
                    sb.Append(EmitHelper.ToStringAutomatic(value.GetAttribute(attrType.Name), graph, true,
                        nameToObject, procEnv));
                }
            }
            if(procEnv != null || !firstLevelObjectEmitted)
                sb.Append("}");
            return sb.ToString();
        }

        public static string GetMessageForAssertion(IGraphProcessingEnvironment procEnv, Func<string> message, params Func<object>[] values)
        {
            if(values.Length == 0)
                return message();

            StringBuilder sb = new StringBuilder();
            sb.Append(message);
            foreach(Func<object> value in values)
            {
                sb.Append(EmitHelper.ToStringAutomatic(value(), procEnv.Graph, false, new Dictionary<string, IObject>(), procEnv));
            }
            return sb.ToString();
        }
    }
}
