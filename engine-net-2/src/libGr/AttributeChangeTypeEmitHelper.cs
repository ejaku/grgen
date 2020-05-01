/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class EmitHelper
    {
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
            if(changeType == AttributeChangeType.PutElement)
            {
                Type keyType;
                Type valueType;
                ContainerHelper.GetDictionaryTypes(setmap, out keyType, out valueType);

                if(valueType == typeof(SetValueType))
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
            else if(changeType == AttributeChangeType.RemoveElement)
            {
                Type keyType;
                Type valueType;
                ContainerHelper.GetDictionaryTypes(setmap, out keyType, out valueType);

                if(valueType == typeof(SetValueType))
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
                if(keyValue != null)
                    content += ", " + keyValue.ToString() + ")";
                else
                    content += ")";
            }
            else if(changeType == AttributeChangeType.RemoveElement)
            {
                Type valueType;
                ContainerHelper.GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += ".rem(";
                if(keyValue != null)
                    content += keyValue.ToString() + ")";
                else
                    content += ")";
            }
            else if(changeType == AttributeChangeType.AssignElement)
            {
                Type valueType;
                ContainerHelper.GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += "[" + keyValue.ToString() + "] = " + ToString(newValue, attrType.ValueType, graph);
            }
            else // changeType==AttributeChangeType.Assign
                ToString((IList)newValue, out type, out content, attrType, graph);
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
                ToString((IDeque)newValue, out type, out content, attrType, graph);
        }
    }
}
