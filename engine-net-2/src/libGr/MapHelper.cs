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

namespace de.unika.ipd.grGen.libGr
{
    public static partial class ContainerHelper
    {
        public static IDictionary FillMap(IDictionary mapToCopyTo, string keyTypeName, string valueTypeName, object hopefullyMapToCopy, IGraphModel model)
        {
            if(hopefullyMapToCopy is IDictionary)
                return FillMap(mapToCopyTo, keyTypeName, valueTypeName, (IDictionary)hopefullyMapToCopy, model);
            throw new Exception("Map copy constructor expects map as source.");
        }

        public static IDictionary FillMap(IDictionary mapToCopyTo, string keyTypeName, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(keyTypeName, model);
            if(nodeType != null)
                FillMapWithKeyNode(mapToCopyTo, nodeType, valueTypeName, mapToCopy, model);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(keyTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyEdge(mapToCopyTo, edgeType, valueTypeName, mapToCopy, model);
                else
                {
                    Type varType = TypesHelper.GetType(keyTypeName, model);
                    FillMapWithKeyVar(mapToCopyTo, varType, valueTypeName, mapToCopy, model);
                }
            }
            return mapToCopyTo;
        }

        public static IDictionary FillMapWithKeyNode(IDictionary mapToCopyTo, NodeType keyNodeType, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillMapWithKeyNodeValueNode(mapToCopyTo, keyNodeType, nodeType, mapToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyNodeValueEdge(mapToCopyTo, keyNodeType, edgeType, mapToCopy);
                else
                {
                    Type valueType = TypesHelper.GetType(valueTypeName, model);
                    FillMapWithKeyNodeValueVar(mapToCopyTo, keyNodeType, valueType, mapToCopy);
                }
            }
            return mapToCopyTo;
        }

        public static IDictionary FillMapWithKeyEdge(IDictionary mapToCopyTo, EdgeType keyEdgeType, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillMapWithKeyEdgeValueNode(mapToCopyTo, keyEdgeType, nodeType, mapToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyEdgeValueEdge(mapToCopyTo, keyEdgeType, edgeType, mapToCopy);
                else
                {
                    Type valueType = TypesHelper.GetType(valueTypeName, model);
                    FillMapWithKeyEdgeValueVar(mapToCopyTo, keyEdgeType, valueType, mapToCopy);
                }
            }
            return mapToCopyTo;
        }

        public static IDictionary FillMapWithKeyVar(IDictionary mapToCopyTo, Type keyVarType, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillMapWithKeyVarValueNode(mapToCopyTo, keyVarType, nodeType, mapToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyVarValueEdge(mapToCopyTo, keyVarType, edgeType, mapToCopy);
                else
                {
                    Type valueType = TypesHelper.GetType(valueTypeName, model);
                    FillMapWithKeyVarValueVar(mapToCopyTo, keyVarType, valueType, mapToCopy);
                }
            }
            return mapToCopyTo;
        }

        public static void FillMapWithKeyNodeValueNode(IDictionary targetMap, NodeType keyNodeType, NodeType valueNodeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                INode keyNode = entry.Key as INode;
                if(keyNode == null)
                    continue;
                if(!keyNode.InstanceOf(keyNodeType))
                    continue;
                INode valueNode = entry.Value as INode;
                if(valueNode == null)
                    continue;
                if(!valueNode.InstanceOf(valueNodeType))
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyNodeValueEdge(IDictionary targetMap, NodeType keyNodeType, EdgeType valueEdgeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                INode keyNode = entry.Key as INode;
                if(keyNode == null)
                    continue;
                if(!keyNode.InstanceOf(keyNodeType))
                    continue;
                IEdge valueEdge = entry.Value as IEdge;
                if(valueEdge == null)
                    continue;
                if(!valueEdge.InstanceOf(valueEdgeType))
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyNodeValueVar(IDictionary targetMap, NodeType keyNodeType, Type valueVarType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                INode keyNode = entry.Key as INode;
                if(keyNode == null)
                    continue;
                if(!keyNode.InstanceOf(keyNodeType))
                    continue;
                if(entry.Value.GetType() != valueVarType)
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyEdgeValueNode(IDictionary targetMap, EdgeType keyEdgeType, NodeType valueNodeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                IEdge keyEdge = entry.Key as IEdge;
                if(keyEdge == null)
                    continue;
                if(!keyEdge.InstanceOf(keyEdgeType))
                    continue;
                INode valueNode = entry.Value as INode;
                if(valueNode == null)
                    continue;
                if(!valueNode.InstanceOf(valueNodeType))
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyEdgeValueEdge(IDictionary targetMap, EdgeType keyEdgeType, EdgeType valueEdgeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                IEdge keyEdge = entry.Key as IEdge;
                if(keyEdge == null)
                    continue;
                if(!keyEdge.InstanceOf(keyEdgeType))
                    continue;
                IEdge valueEdge = entry.Value as IEdge;
                if(valueEdge == null)
                    continue;
                if(!valueEdge.InstanceOf(valueEdgeType))
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyEdgeValueVar(IDictionary targetMap, EdgeType keyEdgeType, Type valueVarType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                IEdge keyEdge = entry.Key as IEdge;
                if(keyEdge == null)
                    continue;
                if(!keyEdge.InstanceOf(keyEdgeType))
                    continue;
                if(entry.Value.GetType() != valueVarType)
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyVarValueNode(IDictionary targetMap, Type keyVarType, NodeType valueNodeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                if(entry.Key.GetType() != keyVarType)
                    continue;
                INode valueNode = entry.Value as INode;
                if(valueNode == null)
                    continue;
                if(!valueNode.InstanceOf(valueNodeType))
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyVarValueEdge(IDictionary targetMap, Type keyVarType, EdgeType valueEdgeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                if(entry.Key.GetType() != keyVarType)
                    continue;
                IEdge valueEdge = entry.Value as IEdge;
                if(valueEdge == null)
                    continue;
                if(!valueEdge.InstanceOf(valueEdgeType))
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static void FillMapWithKeyVarValueVar(IDictionary targetMap, Type keyVarType, Type valueVarType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                if(entry.Key.GetType() != keyVarType)
                    continue;
                if(entry.Value.GetType() != valueVarType)
                    continue;
                targetMap.Add(entry.Key, entry.Value);
            }
        }

        public static Dictionary<K, V> FillMap<K, V>(Dictionary<K, V> mapToCopyTo, string keyTypeName, string valueTypeName, object hopefullyMapToCopy, IGraphModel model)
        {
            if(hopefullyMapToCopy is IDictionary)
                return FillMap(mapToCopyTo, keyTypeName, valueTypeName, (IDictionary)hopefullyMapToCopy, model);
            throw new Exception("Map copy constructor expects map as source.");
        }

        public static Dictionary<K, V> FillMap<K, V>(Dictionary<K, V> mapToCopyTo, string keyTypeName, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(keyTypeName, model);
            if(nodeType != null)
                FillMapWithKeyNode(mapToCopyTo, nodeType, valueTypeName, mapToCopy, model);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(keyTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyEdge(mapToCopyTo, edgeType, valueTypeName, mapToCopy, model);
                else
                {
                    Type varType = TypesHelper.GetType(keyTypeName, model);
                    FillMapWithKeyVar(mapToCopyTo, varType, valueTypeName, mapToCopy, model);
                }
            }
            return mapToCopyTo;
        }

        public static Dictionary<K, V> FillMapWithKeyNode<K, V>(Dictionary<K, V> mapToCopyTo, NodeType keyNodeType, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillMapWithKeyNodeValueNode(mapToCopyTo, keyNodeType, nodeType, mapToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyNodeValueEdge(mapToCopyTo, keyNodeType, edgeType, mapToCopy);
                else
                {
                    Type valueType = TypesHelper.GetType(valueTypeName, model);
                    FillMapWithKeyNodeValueVar(mapToCopyTo, keyNodeType, valueType, mapToCopy);
                }
            }
            return mapToCopyTo;
        }

        public static Dictionary<K, V> FillMapWithKeyEdge<K, V>(Dictionary<K, V> mapToCopyTo, EdgeType keyEdgeType, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillMapWithKeyEdgeValueNode(mapToCopyTo, keyEdgeType, nodeType, mapToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyEdgeValueEdge(mapToCopyTo, keyEdgeType, edgeType, mapToCopy);
                else
                {
                    Type valueType = TypesHelper.GetType(valueTypeName, model);
                    FillMapWithKeyEdgeValueVar(mapToCopyTo, keyEdgeType, valueType, mapToCopy);
                }
            }
            return mapToCopyTo;
        }

        public static Dictionary<K, V> FillMapWithKeyVar<K, V>(Dictionary<K, V> mapToCopyTo, Type keyVarType, string valueTypeName, IDictionary mapToCopy, IGraphModel model)
        {
            NodeType nodeType = TypesHelper.GetNodeType(valueTypeName, model);
            if(nodeType != null)
                FillMapWithKeyVarValueNode(mapToCopyTo, keyVarType, nodeType, mapToCopy);
            else
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(valueTypeName, model);
                if(edgeType != null)
                    FillMapWithKeyVarValueEdge(mapToCopyTo, keyVarType, edgeType, mapToCopy);
                else
                {
                    Type valueType = TypesHelper.GetType(valueTypeName, model);
                    FillMapWithKeyVarValueVar(mapToCopyTo, keyVarType, valueType, mapToCopy);
                }
            }
            return mapToCopyTo;
        }

        public static void FillMapWithKeyNodeValueNode<K, V>(Dictionary<K, V> targetMap, NodeType keyNodeType, NodeType valueNodeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                INode keyNode = entry.Key as INode;
                if(keyNode == null)
                    continue;
                if(!keyNode.InstanceOf(keyNodeType))
                    continue;
                INode valueNode = entry.Value as INode;
                if(valueNode == null)
                    continue;
                if(!valueNode.InstanceOf(valueNodeType))
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyNodeValueEdge<K, V>(Dictionary<K, V> targetMap, NodeType keyNodeType, EdgeType valueEdgeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                INode keyNode = entry.Key as INode;
                if(keyNode == null)
                    continue;
                if(!keyNode.InstanceOf(keyNodeType))
                    continue;
                IEdge valueEdge = entry.Value as IEdge;
                if(valueEdge == null)
                    continue;
                if(!valueEdge.InstanceOf(valueEdgeType))
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyNodeValueVar<K, V>(Dictionary<K, V> targetMap, NodeType keyNodeType, Type valueVarType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                INode keyNode = entry.Key as INode;
                if(keyNode == null)
                    continue;
                if(!keyNode.InstanceOf(keyNodeType))
                    continue;
                if(entry.Value.GetType() != valueVarType)
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyEdgeValueNode<K, V>(Dictionary<K, V> targetMap, EdgeType keyEdgeType, NodeType valueNodeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                IEdge keyEdge = entry.Key as IEdge;
                if(keyEdge == null)
                    continue;
                if(!keyEdge.InstanceOf(keyEdgeType))
                    continue;
                INode valueNode = entry.Value as INode;
                if(valueNode == null)
                    continue;
                if(!valueNode.InstanceOf(valueNodeType))
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyEdgeValueEdge<K, V>(Dictionary<K, V> targetMap, EdgeType keyEdgeType, EdgeType valueEdgeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                IEdge keyEdge = entry.Key as IEdge;
                if(keyEdge == null)
                    continue;
                if(!keyEdge.InstanceOf(keyEdgeType))
                    continue;
                IEdge valueEdge = entry.Value as IEdge;
                if(valueEdge == null)
                    continue;
                if(!valueEdge.InstanceOf(valueEdgeType))
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyEdgeValueVar<K, V>(Dictionary<K, V> targetMap, EdgeType keyEdgeType, Type valueVarType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                IEdge keyEdge = entry.Key as IEdge;
                if(keyEdge == null)
                    continue;
                if(!keyEdge.InstanceOf(keyEdgeType))
                    continue;
                if(entry.Value.GetType() != valueVarType)
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyVarValueNode<K, V>(Dictionary<K, V> targetMap, Type keyVarType, NodeType valueNodeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                if(entry.Key.GetType() != keyVarType)
                    continue;
                INode valueNode = entry.Value as INode;
                if(valueNode == null)
                    continue;
                if(!valueNode.InstanceOf(valueNodeType))
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyVarValueEdge<K, V>(Dictionary<K, V> targetMap, Type keyVarType, EdgeType valueEdgeType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                if(entry.Key.GetType() != keyVarType)
                    continue;
                IEdge valueEdge = entry.Value as IEdge;
                if(valueEdge == null)
                    continue;
                if(!valueEdge.InstanceOf(valueEdgeType))
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        public static void FillMapWithKeyVarValueVar<K, V>(Dictionary<K, V> targetMap, Type keyVarType, Type valueVarType, IDictionary sourceMap)
        {
            foreach(DictionaryEntry entry in sourceMap)
            {
                if(entry.Key.GetType() != keyVarType)
                    continue;
                if(entry.Value.GetType() != valueVarType)
                    continue;
                targetMap.Add((K)entry.Key, (V)entry.Value);
            }
        }

        /// <summary>
        /// Creates a new dictionary representing a set,
        /// containing all keys from the given dictionary representing a map <paramref name="map"/>.
        /// </summary>
        /// <param name="map">A dictionary representing a map.</param>
        /// <returns>A new set dictionary containing all keys from <paramref name="map"/>.</returns>
        public static Dictionary<K, SetValueType> Domain<K, V>(Dictionary<K, V> map)
        {
            Dictionary<K, SetValueType> newDict =
                new Dictionary<K, SetValueType>();

            // Add all keys of dictionary representing map to new dictionary representing set
            foreach(K key in map.Keys)
            {
                newDict[key] = null;
            }

            return newDict;
        }

        public static IDictionary Domain(IDictionary map)
        {
            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(map, out keyType, out valueType);
            IDictionary newDict = NewDictionary(keyType, typeof(SetValueType));

            foreach(object key in map.Keys)
            {
                newDict[key] = null;
            }

            return newDict;
        }

        /// <summary>
        /// Creates a new dictionary representing a set,
        /// containing all values from the given dictionary representing a map <paramref name="map"/>.
        /// </summary>
        /// <param name="map">A dictionary representing a map.</param>
        /// <returns>A new set dictionary containing all values from <paramref name="map"/>.</returns>
        public static Dictionary<V, SetValueType> Range<K, V>(Dictionary<K, V> map)
        {
            Dictionary<V, SetValueType> newDict =
                new Dictionary<V, SetValueType>();

            // Add all values of dictionary representing map to new dictionary representing set
            foreach(V value in map.Values)
            {
                newDict[value] = null;
            }

            return newDict;
        }

        public static IDictionary Range(IDictionary map)
        {
            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(map, out keyType, out valueType);
            IDictionary newDict = NewDictionary(valueType, typeof(SetValueType));

            foreach(object value in map.Values)
            {
                newDict[value] = null;
            }

            return newDict;
        }

        /// <summary>
        /// Creates a new list representing an array,
        /// containing all values from the given dictionary representing a map <paramref name="map"/> from int to some values.
        /// </summary>
        /// <param name="map">A dictionary representing a map.</param>
        /// <param name="model">The graph model.</param>
        /// <returns>A new list containing all values from <paramref name="map"/>.</returns>
        public static IList MapAsArray(IDictionary map, IGraphModel model)
        {
            int max = 0;
            foreach(int i in map.Keys)
            {
                if(i < 0)
                    throw new Exception("MapAsArray does not support negative indices");
                max = Math.Max(max, i);
            }

            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(map, out keyType, out valueType);
            IList newList = NewList(valueType, max + 1); // yep, if the dict contains max int, contiguous 8GB are needed

            // Add all values of dictionary representing map to new dictionary representing set
            for(int i = 0; i < max + 1; ++i)
            {
                if(map.Contains(i))
                    newList.Add(map[i]);
                else
                    newList.Add(TypesHelper.DefaultValue(valueType.Name, model));
            }

            return newList;
        }

        /// <summary>
        /// Creates a new list representing an array,
        /// containing all values from the given dictionary representing a map <paramref name="map"/> from int to some values.
        /// </summary>
        /// <param name="map">A dictionary representing a map.</param>
        /// <returns>A new list containing all values from <paramref name="map"/>.</returns>
        public static List<V> MapAsArray<V>(Dictionary<int, V> map)
        {
            int max = 0;
            foreach(int i in map.Keys)
            {
                if(i < 0)
                    throw new Exception("MapAsArray does not support negative indices");
                max = Math.Max(max, i);
            }
            List<V> newList = new List<V>(max + 1); // yep, if the dict contains max int, contiguous 8GB are needed

            // Add all values of dictionary representing map to new dictionary representing set
            for(int i = 0; i < max + 1; ++i)
            {
                if(map.ContainsKey(i))
                    newList.Add(map[i]);
                else
                    newList.Add(default(V));
            }

            return newList;
        }

        public static bool DeeplyEqualMap<K,V>(Dictionary<K,V> this_, Dictionary<K,V> that,
            IDictionary<object, object> visitedObjects,
            IDictionary<object, object> matchedObjectsFromThis, IDictionary<object, object> matchedObjectsFromThat)
        {
            if(this_.Count == matchedObjectsFromThis.Count)
                return true;

            foreach(KeyValuePair<K,V> thisEntry in this_)
            {
                K thisElem = thisEntry.Key;
                if(matchedObjectsFromThis.ContainsKey(thisElem))
                    continue;
                matchedObjectsFromThis.Add(thisElem, null);
                foreach(KeyValuePair<K,V> thatEntry in that)
                {
                    K thatElem = thatEntry.Key;
                    if(matchedObjectsFromThat.ContainsKey(thatElem))
                        continue;
                    if(EqualityComparer<K>.Default.Equals(thisElem, thatElem))
                        continue;
                    V thisElemValue = thisEntry.Value;
                    V thatElemValue = thatEntry.Value;
                    if(EqualityComparer<V>.Default.Equals(thisElemValue, thatElemValue))
                        continue;
                    matchedObjectsFromThat.Add(thatElem, null);
                    if(DeeplyEqualMap(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat))
                        return true;
                    matchedObjectsFromThat.Remove(thatElem);
                }
                matchedObjectsFromThis.Remove(thisElem);
            }

            return false;
        }

        public static bool DeeplyEqualMap(IDictionary this_, IDictionary that,
            IDictionary<object, object> visitedObjects,
            IDictionary<object, object> matchedObjectsFromThis, IDictionary<object, object> matchedObjectsFromThat)
        {
            if(this_.Count == matchedObjectsFromThis.Count)
                return true;

            foreach(DictionaryEntry thisEntry in this_)
            {
                object thisElem = thisEntry.Key;
                if(matchedObjectsFromThis.ContainsKey(thisElem))
                    continue;
                matchedObjectsFromThis.Add(thisElem, null);
                foreach(DictionaryEntry thatEntry in that)
                {
                    object thatElem = thatEntry.Key;
                    if(matchedObjectsFromThat.ContainsKey(thatElem))
                        continue;
                    if(!Object.Equals(thisElem, thatElem))
                        continue;
                    object thisElemValue = thisEntry.Value;
                    object thatElemValue = thatEntry.Value;
                    if(thisElemValue != thatElemValue)
                        continue;
                    matchedObjectsFromThat.Add(thatElem, null);
                    if(DeeplyEqualMap(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat))
                        return true;
                    matchedObjectsFromThat.Remove(thatElem);
                }
                matchedObjectsFromThis.Remove(thisElem);
            }

            return false;
        }

        public static bool DeeplyEqualMap<K,V>(Dictionary<K,V> this_, Dictionary<K,V> that,
            IDictionary<object, object> visitedObjects,
            IDictionary<IAttributeBearer, object> matchedObjectsFromThis, IDictionary<IAttributeBearer, object> matchedObjectsFromThat)
            where K : IAttributeBearer
        {
            if(this_.Count == matchedObjectsFromThis.Count)
                return true;

            foreach(KeyValuePair<K,V> thisEntry in this_)
            {
                IAttributeBearer thisElem = thisEntry.Key;
                if(matchedObjectsFromThis.ContainsKey(thisElem))
                    continue;
                matchedObjectsFromThis.Add(thisElem, null);
                foreach(KeyValuePair<K,V> thatEntry in that)
                {
                    IAttributeBearer thatElem = thatEntry.Key;
                    if(matchedObjectsFromThat.ContainsKey(thatElem))
                        continue;
                    if(!DeeplyEqual(thisElem, thatElem, visitedObjects))
                        continue;
                    if(thisEntry.Value is IAttributeBearer)
                    {
                        IAttributeBearer thisElemValue = (IAttributeBearer)thisEntry.Value;
                        IAttributeBearer thatElemValue = (IAttributeBearer)thatEntry.Value;
                        if(!DeeplyEqual(thisElemValue, thatElemValue, visitedObjects))
                            continue;
                    }
                    else
                    {
                        object thisElemValue = thisEntry.Value;
                        object thatElemValue = thatEntry.Value;
                        if(thisElemValue != thatElemValue)
                            continue;
                    }
                    matchedObjectsFromThat.Add(thatElem, null);
                    if(DeeplyEqualMap(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat))
                        return true;
                    matchedObjectsFromThat.Remove(thatElem);
                }
                matchedObjectsFromThis.Remove(thisElem);
            }

            return false;
        }

        public static bool DeeplyEqualMap(IDictionary this_, IDictionary that,
            IDictionary<object, object> visitedObjects,
            IDictionary<IAttributeBearer, object> matchedObjectsFromThis, IDictionary<IAttributeBearer, object> matchedObjectsFromThat)
        {
            if(this_.Count == matchedObjectsFromThis.Count)
                return true;

            foreach(DictionaryEntry thisEntry in this_)
            {
                IAttributeBearer thisElem = (IAttributeBearer)thisEntry.Key;
                if(matchedObjectsFromThis.ContainsKey(thisElem))
                    continue;
                matchedObjectsFromThis.Add(thisElem, null);
                foreach(DictionaryEntry thatEntry in that)
                {
                    IAttributeBearer thatElem = (IAttributeBearer)thatEntry.Key;
                    if(matchedObjectsFromThat.ContainsKey(thatElem))
                        continue;
                    if(!DeeplyEqual(thisElem, thatElem, visitedObjects))
                        continue;
                    if(thisEntry.Value is IAttributeBearer)
                    {
                        IAttributeBearer thisElemValue = (IAttributeBearer)thisEntry.Value;
                        IAttributeBearer thatElemValue = (IAttributeBearer)thatEntry.Value;
                        if(!DeeplyEqual(thisElemValue, thatElemValue, visitedObjects))
                            continue;
                    }
                    else
                    {
                        object thisElemValue = thisEntry.Value;
                        object thatElemValue = thatEntry.Value;
                        if(thisElemValue != thatElemValue)
                            continue;
                    }
                    matchedObjectsFromThat.Add(thatElem, null);
                    if(DeeplyEqualMap(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat))
                        return true;
                    matchedObjectsFromThat.Remove(thatElem);
                }
                matchedObjectsFromThis.Remove(thisElem);
            }

            return false;
        }
    }
}
