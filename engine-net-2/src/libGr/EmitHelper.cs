/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Collections;
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

        public static string Clip(string potentiallyLargeString, int maxLength)
        {
            if(potentiallyLargeString.Length < maxLength)
                return potentiallyLargeString;
            else
                return potentiallyLargeString.Substring(0, maxLength - 3) + "...";
        }

        private static string ToString(IMatch match, IGraph graph)
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
                sb.Append(EmitHelper.ToStringAutomatic(match.getNode(patternNode.UnprefixedName), graph));
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
                sb.Append(EmitHelper.ToStringAutomatic(match.getEdge(patternEdge.UnprefixedName), graph));
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
                sb.Append(EmitHelper.ToStringAutomatic(match.getVariable(patternVar.UnprefixedName), graph));
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string ToString(IObject value, IGraph graph)
        {
            StringBuilder sb = new StringBuilder();
            string objectType = value.Type.PackagePrefixedName;
            sb.Append(objectType + "{");
            bool first = true;
            foreach(AttributeType attrType in value.Type.AttributeTypes)
            {
                if(first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(attrType.Name);
                sb.Append(":");
                sb.Append(EmitHelper.ToStringAutomatic(value.GetAttribute(attrType.Name), graph));
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
