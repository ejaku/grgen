/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
    public static class DictionaryListHelper
    {
        /// <summary>
        /// If dict is dictionary, the dictionary is returned together with its key and value type
        /// </summary>
        /// <param name="dict">The object which should be a dictionary</param>
        /// <param name="keyType">The key type of the dictionary</param>
        /// <param name="valueType">The value type of the dictionary</param>
        /// <returns>The casted input dictionary, or null if not a dictionary</returns>
        public static IDictionary GetDictionaryTypes(object dict, out Type keyType, out Type valueType)
        {
            if (!(dict is IDictionary))
            {
                keyType = null;
                valueType = null;
                return null;
            }
            Type dictType = dict.GetType();
            GetDictionaryTypes(dictType, out keyType, out valueType);
            return (IDictionary)dict;
        }

        /// <summary>
        /// If array is List, the List is returned together with its value type
        /// </summary>
        /// <param name="array">The object which should be a List</param>
        /// <param name="valueType">The value type of the List</param>
        /// <returns>The casted input List, or null if not a List</returns>
        public static IList GetListType(object array, out Type valueType)
        {
            if(!(array is IList))
            {
                valueType = null;
                return null;
            }
            Type arrayType = array.GetType();
            GetListType(arrayType, out valueType);
            return (IList)array;
        }

        /// <summary>
        /// The key and value types are returned of the dictionary
        /// </summary>
        /// <param name="dictType">The dictionary type</param>
        /// <param name="keyType">The key type of the dictionary</param>
        /// <param name="valueType">The value type of the dictionary</param>
        public static void GetDictionaryTypes(Type dictType, out Type keyType, out Type valueType)
        {
            Type[] dictTypeArgs = dictType.GetGenericArguments();
            keyType = dictTypeArgs[0];
            valueType = dictTypeArgs[1];
        }

        /// <summary>
        /// The value type of the List is returned
        /// </summary>
        /// <param name="arrayType">The List type</param>
        /// <param name="valueType">The value type of the dictionary</param>
        public static void GetListType(Type arrayType, out Type valueType)
        {
            Type[] arrayTypeArgs = arrayType.GetGenericArguments();
            valueType = arrayTypeArgs[0];
        }

        /// <summary>
        /// Returns type object for type name string, to be used for dictionary or List
        /// </summary>
        /// <param name="typeName">Name of the type we want some type object for</param>
        /// <param name="graph">Graph to be search for enum,node,edge types / enum,node/edge type names</param>
        /// <returns>The type object corresponding to the given string, null if type was not found</returns>
        public static Type GetTypeFromNameForDictionaryOrList(String typeName, IGraph graph)
        {
            return GetTypeFromNameForDictionaryOrList(typeName, graph.Model);
        }

        /// <summary>
        /// Returns type object for type name string, to be used for dictionary or List
        /// </summary>
        /// <param name="typeName">Name of the type we want some type object for</param>
        /// <param name="model">Graph model to be search for enum,node,edge types / enum,node/edge type names</param>
        /// <returns>The type object corresponding to the given string, null if type was not found</returns>
        public static Type GetTypeFromNameForDictionaryOrList(String typeName, IGraphModel model)
        {
            if(typeName == null) return null;

            switch (typeName)
            {
                case "boolean": return typeof(bool);
                case "byte": return typeof(sbyte);
                case "short": return typeof(short);
                case "int": return typeof(int);
                case "long": return typeof(long);
                case "float": return typeof(float);
                case "double": return typeof(double);
                case "string": return typeof(string);
                case "object": return typeof(object);
                case "graph": return typeof(IGraph);
            }

            if (model == null) return null;

            // No standard type, so check enums
            foreach (EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if (enumAttrType.Name == typeName)
                    return enumAttrType.EnumType;
            }

            Assembly assembly = Assembly.GetAssembly(model.GetType());

            // check node and edge types
            foreach (NodeType nodeType in model.NodeModel.Types)
            {
                if (nodeType.Name == typeName)
                {
                    Type type = Type.GetType(nodeType.NodeInterfaceName); // available in libGr (INode)?
                    if (type != null) return type;
                    type = Type.GetType(nodeType.NodeInterfaceName + "," + assembly.FullName); // no -> search model assembly
                    return type;
                }
            }
            foreach (EdgeType edgeType in model.EdgeModel.Types)
            {
                if (edgeType.Name == typeName)
                {
                    Type type = Type.GetType(edgeType.EdgeInterfaceName); // available in libGr (IEdge)?
                    if (type != null) return type;
                    type = Type.GetType(edgeType.EdgeInterfaceName + "," + assembly.FullName); // no -> search model assembly
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the qualified type name for the type name given
        /// </summary>
        public static String GetQualifiedTypeName(String typeName, IGraphModel model)
        {
            if(typeName=="de.unika.ipd.grGen.libGr.SetValueType" || typeName=="SetValueType")
                return "de.unika.ipd.grGen.libGr.SetValueType";
            Type type = GetTypeFromNameForDictionaryOrList(typeName, model);
            return type!=null ? type.Namespace+"."+type.Name : null;
        }

        /// <summary>
        /// Creates a new dictionary of the given key type and value type
        /// </summary>
        /// <param name="keyType">The key type of the dictionary to be created</param>
        /// <param name="valueType">The value type of the dictionary to be created</param>
        /// <returns>The newly created dictionary, null if unsuccessfull</returns>
        public static IDictionary NewDictionary(Type keyType, Type valueType)
        {
            if (keyType == null || valueType == null) return null;

            Type genDictType = typeof(Dictionary<,>);
            Type dictType = genDictType.MakeGenericType(keyType, valueType);
            return (IDictionary)Activator.CreateInstance(dictType);
        }

        /// <summary>
        /// Creates a new List of the given value type
        /// </summary>
        /// <param name="valueType">The value type of the List to be created</param>
        /// <returns>The newly created List, null if unsuccessfull</returns>
        public static IList NewList(Type valueType)
        {
            if(valueType == null) return null;

            Type genListType = typeof(List<>);
            Type listType = genListType.MakeGenericType(valueType);
            return (IList)Activator.CreateInstance(listType);
        }

        /// <summary>
        /// Creates a new dictionary of the given key type and value type,
        /// initialized with the content of the old dictionary (clones the old dictionary)
        /// </summary>
        /// <param name="keyType">The key type of the dictionary to be created</param>
        /// <param name="valueType">The value type of the dictionary to be created</param>
        /// <param name="oldDictionary">The old dictionary to be cloned</param>
        /// <returns>The newly created dictionary, containing the content of the old dictionary,
        /// null if unsuccessfull</returns>
        public static IDictionary NewDictionary(Type keyType, Type valueType, object oldDictionary)
        {
            if (keyType == null || valueType == null || oldDictionary == null) return null;

            Type genDictType = typeof(Dictionary<,>);
            Type dictType = genDictType.MakeGenericType(keyType, valueType);
            return (IDictionary) Activator.CreateInstance(dictType, oldDictionary);
        }

        /// <summary>
        /// Creates a new List of the given value type,
        /// initialized with the content of the old List (clones the old List)
        /// </summary>
        /// <param name="valueType">The value type of the List to be created</param>
        /// <param name="oldList">The old List to be cloned</param>
        /// <returns>The newly created List, containing the content of the old List,
        /// null if unsuccessfull</returns>
        public static IList NewList(Type valueType, object oldList)
        {
            if(valueType == null || oldList == null) return null;

            Type genListType = typeof(List<>);
            Type listType = genListType.MakeGenericType(valueType);
            return (IList)Activator.CreateInstance(listType, oldList);
        }

        /// <summary>
        /// Creates a new dictionary and fills in all key/value pairs from
        /// <paramref name="a"/> and <paramref name="b"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// (this way the common case "a = a | map<int, int> { 7 -> 13 };" would update an existing entry
        /// with key 7 to 13).
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>A new dictionary containing all elements from both parameters.</returns>
        public static Dictionary<K, V> Union<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            // Fill new dictionary with all elements from a.
            Dictionary<K, V> newDict = new Dictionary<K, V>(a);

            // Add all elements from b, potentially overwriting those of a.
            foreach(KeyValuePair<K, V> entry in b)
                newDict[entry.Key] = entry.Value;

            return newDict;
        }

        /// <summary>
        /// Creates a new dictionary containing all key/value pairs from
        /// <paramref name="a"/> whose keys are also contained in <paramref name="b"/>.
        /// If both dictionaries contain one key, the value from <paramref name="a"/> takes precedence, in contrast to union.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>A new dictionary containing all elements from <paramref name="a"/>,
        /// which are also in <paramref name="b"/>.</returns>
        public static Dictionary<K, V> Intersect<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            // Fill new dictionary with all elements from a.
            Dictionary<K, V> newDict = new Dictionary<K, V>(a);

            // Remove all elements of b not contained in a.
            foreach(KeyValuePair<K, V> entry in b)
                if(!a.ContainsKey(entry.Key))
                    newDict.Remove(entry.Key);

            return newDict;
        }

        /// <summary>
        /// Creates a new dictionary containing all key/value pairs from
        /// <paramref name="a"/> whose keys are not contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible key type to <paramref name="a"/>.</param>
        /// <returns>A new dictionary containing all elements from <paramref name="a"/>,
        /// which are not in <paramref name="b"/>.</returns>
        public static Dictionary<K, V> Except<K, V, W>(Dictionary<K, V> a, Dictionary<K, W> b)
        {
            // Fill new dictionary with all elements from a.
            Dictionary<K, V> newDict = new Dictionary<K, V>(a);

            // Remove all elements contained in b.
            foreach(KeyValuePair<K, W> entry in b)
                newDict.Remove(entry.Key);

            return newDict;
        }

        /// <summary>
        /// Adds all key/value pairs from set/map <paramref name="b"/> to <paramref name="a"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// (this way the common case "a = a | map<int, int> { 7 -> 13 };" would update an existing entry
        /// with key 7 to 13).
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool UnionChanged<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            bool changed = false;

            // Add all elements from b not contained in a (different values count as not contained, overwriting old value).
            foreach(KeyValuePair<K, V> entry in b)
            {
                if(!a.ContainsKey(entry.Key) || EqualityComparer<V>.Default.Equals(a[entry.Key], entry.Value))
                {
                    a[entry.Key] = entry.Value;
                    changed = true;
                }
            }

            return changed;
        }

        /// <summary>
        /// Removes all key/value pairs from set/map <paramref name="a"/> whose keys are not also contained in <paramref name="b"/>.
        /// If both dictionaries contain one key, the value from <paramref name="a"/> takes precedence, in contrast to union.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool IntersectChanged<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            // First determine all elements from a not contained in b
            List<K> toBeRemoved = new List<K>(a.Count);
            foreach(KeyValuePair<K, V> entry in a)
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);

            // Then remove them
            foreach(K key in toBeRemoved)
                a.Remove(key);

            return toBeRemoved.Count>0;
        }

        /// <summary>
        /// Removes all key/value pairs from set/map <paramref name="a"/> whose keys are contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible key type to <paramref name="a"/>.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool ExceptChanged<K, V, W>(Dictionary<K, V> a, Dictionary<K, W> b)
        {
            bool changed = false;

            // Remove all elements from a contained in b.
            foreach(KeyValuePair<K, W> entry in b)
                changed |= a.Remove(entry.Key);

            return changed;
        }

        /// <summary>
        /// Adds all key/value pairs from map <paramref name="b"/> to <paramref name="a"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// (this way the common case "a = a | map<int, int> { 7 -> 13 };" would update an existing entry
        /// with key 7 to 13).
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool UnionChanged<K, V>(Dictionary<K, V> a, Dictionary<K, V> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            bool changed = false;

            // Add all elements from b not contained in a (different values count as not contained, overwriting old value).
            foreach(KeyValuePair<K, V> entry in b)
            {
                if(!a.ContainsKey(entry.Key) || EqualityComparer<V>.Default.Equals(a[entry.Key], entry.Value))
                {
                    graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.PutElement, entry.Value, entry.Key);
                    a[entry.Key] = entry.Value;
                    changed = true;
                }
            }

            return changed;
        }

        /// <summary>
        /// Adds all key/value pairs from map <paramref name="b"/> to <paramref name="a"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// (this way the common case "a = a | map<int, int> { 7 -> 13 };" would update an existing entry
        /// with key 7 to 13).
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool UnionChanged<K, V>(Dictionary<K, V> a, Dictionary<K, V> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            bool changed = false;

            // Add all elements from b not contained in a (different values count as not contained, overwriting old value).
            foreach(KeyValuePair<K, V> entry in b)
            {
                if(!a.ContainsKey(entry.Key) || EqualityComparer<V>.Default.Equals(a[entry.Key], entry.Value))
                {
                    graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.PutElement, entry.Value, entry.Key);
                    a[entry.Key] = entry.Value;
                    changed = true;
                }
            }

            return changed;
        }

        /// <summary>
        /// Adds all key/value pairs from set <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool UnionChanged<K>(Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> a,
            Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            bool changed = false;

            // Add all elements from b not contained in a (different values count as not contained, overwriting old value).
            foreach(KeyValuePair<K, de.unika.ipd.grGen.libGr.SetValueType> entry in b)
            {
                if(!a.ContainsKey(entry.Key))
                {
                    graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.PutElement, entry.Key, null);
                    a[entry.Key] = entry.Value;
                    changed = true;
                }
            }

            return changed;
        }

        /// <summary>
        /// Adds all key/value pairs from set <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool UnionChanged<K>(Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> a,
            Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            bool changed = false;

            // Add all elements from b not contained in a (different values count as not contained, overwriting old value).
            foreach(KeyValuePair<K, de.unika.ipd.grGen.libGr.SetValueType> entry in b)
            {
                if(!a.ContainsKey(entry.Key))
                {
                    graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.PutElement, entry.Key, null);
                    a[entry.Key] = entry.Value;
                    changed = true;
                }
            }

            return changed;
        }

        /// <summary>
        /// Removes all key/value pairs from map <paramref name="a"/> whose keys are not also contained in <paramref name="b"/>.
        /// If both dictionaries contain one key, the value from <paramref name="a"/> takes precedence, in contrast to union.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool IntersectChanged<K, V>(Dictionary<K, V> a, Dictionary<K, V> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            // First determine all elements from a not contained in b
            List<K> toBeRemoved = new List<K>(a.Count);
            foreach(KeyValuePair<K, V> entry in a)
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);

            // Then remove them
            foreach(K key in toBeRemoved)
            {
                graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.RemoveElement, null, key);
                a.Remove(key);
            }

            return toBeRemoved.Count > 0;
        }

        /// <summary>
        /// Removes all key/value pairs from map <paramref name="a"/> whose keys are not also contained in <paramref name="b"/>.
        /// If both dictionaries contain one key, the value from <paramref name="a"/> takes precedence, in contrast to union.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool IntersectChanged<K, V>(Dictionary<K, V> a, Dictionary<K, V> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            // First determine all elements from a not contained in b
            List<K> toBeRemoved = new List<K>(a.Count);
            foreach(KeyValuePair<K, V> entry in a)
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);

            // Then remove them
            foreach(K key in toBeRemoved)
            {
                graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.RemoveElement, null, key);
                a.Remove(key);
            }

            return toBeRemoved.Count > 0;
        }

        /// <summary>
        /// Removes all key/value pairs from set <paramref name="a"/> whose keys are not also contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool IntersectChanged<K>(Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> a,
            Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            // First determine all elements from a not contained in b
            List<K> toBeRemoved = new List<K>(a.Count);
            foreach(KeyValuePair<K, de.unika.ipd.grGen.libGr.SetValueType> entry in a)
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);

            // Then remove them
            foreach(K key in toBeRemoved)
            {
                graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.RemoveElement, key, null);
                a.Remove(key);
            }

            return toBeRemoved.Count > 0;
        }

        /// <summary>
        /// Removes all key/value pairs from set <paramref name="a"/> whose keys are not also contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool IntersectChanged<K>(Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> a,
            Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            // First determine all elements from a not contained in b
            List<K> toBeRemoved = new List<K>(a.Count);
            foreach(KeyValuePair<K, de.unika.ipd.grGen.libGr.SetValueType> entry in a)
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);

            // Then remove them
            foreach(K key in toBeRemoved)
            {
                graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.RemoveElement, key, null);
                a.Remove(key);
            }

            return toBeRemoved.Count > 0;
        }

        /// <summary>
        /// Removes all key/value pairs from map <paramref name="a"/> whose keys are contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible key type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool ExceptChanged<K, V, W>(Dictionary<K, V> a, Dictionary<K, W> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            bool changed = false;

            // Remove all elements from a contained in b.
            foreach(KeyValuePair<K, W> entry in b)
            {
                graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.RemoveElement, null, entry.Key);
                changed |= a.Remove(entry.Key);
            }

            return changed;
        }

        /// <summary>
        /// Removes all key/value pairs from map <paramref name="a"/> whose keys are contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible key type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool ExceptChanged<K, V, W>(Dictionary<K, V> a, Dictionary<K, W> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            bool changed = false;

            // Remove all elements from a contained in b.
            foreach(KeyValuePair<K, W> entry in b)
            {
                graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.RemoveElement, null, entry.Key);
                changed |= a.Remove(entry.Key);
            }

            return changed;
        }

        /// <summary>
        /// Removes all key/value pairs from set <paramref name="a"/> whose keys are contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible key type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool ExceptChanged<K, W>(Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> a,
            Dictionary<K, W> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            bool changed = false;

            // Remove all elements from a contained in b.
            foreach(KeyValuePair<K, W> entry in b)
            {
                graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.RemoveElement, entry.Key, null);
                changed |= a.Remove(entry.Key);
            }

            return changed;
        }

        /// <summary>
        /// Removes all key/value pairs from set <paramref name="a"/> whose keys are contained in <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A dictionary to change.</param>
        /// <param name="b">Another dictionary of compatible key type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool ExceptChanged<K, W>(Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> a,
            Dictionary<K, W> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            bool changed = false;

            // Remove all elements from a contained in b.
            foreach(KeyValuePair<K, W> entry in b)
            {
                graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.RemoveElement, entry.Key, null);
                changed |= a.Remove(entry.Key);
            }

            return changed;
        }

        /// <summary>
        /// Returns the first position of entry in the array a
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <param name="entry">The value to search for.</param>
        /// <returns>The first position of entry in the array a, -1 if entry not in a.</returns>
        public static int IndexOf<V>(List<V> a, V entry)
        {
            for(int i = 0; i < a.Count; ++i)
            {
                if(EqualityComparer<V>.Default.Equals(a[i], entry))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the first position from the end inwards of entry in the array a
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <param name="entry">The value to search for.</param>
        /// <returns>The first position from the end inwards of entry in the array a, -1 if entry not in a.</returns>
        public static int LastIndexOf<V>(List<V> a, V entry)
        {
            for(int i = a.Count-1; i >= 0; --i)
            {
                if(EqualityComparer<V>.Default.Equals(a[i], entry))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Creates a new dynamic array with length values copied from a from index start on.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <param name="start">A start position in the dynamic array.</param>
        /// <param name="length">The number of elements to copy from start on.</param>
        /// <returns>A new List, i.e. dynamic array, containing the length first values from start on.</returns>
        public static List<V> Subarray<V>(List<V> a, int start, int length)
        {
            List<V> newList = new List<V>();

            for(int i = start; i < start+length; ++i)
            {
                newList.Add(a[i]);
            }

            return newList;
        }

        /// <summary>
        /// Creates a new dynamic array and appends all values first from
        /// <paramref name="a"/> and then from <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array.</param>
        /// <param name="b">Another List, i.e. dynamic array of compatible type to <paramref name="a"/>.</param>
        /// <returns>A new List, i.e. dynamic array, containing a concatenation of the parameter arrays.</returns>
        public static List<V> Concatenate<V>(List<V> a, List<V> b)
        {
            // create new list as a copy of a
            List<V> newList = new List<V>(a);

            // then append b
            newList.AddRange(b);

            return newList;
        }

        public static IList ConcatenateIList(IList a, IList b)
        {
            // create new list as a copy of a
            IList newList = (IList)Activator.CreateInstance(a.GetType(), a);

            // then append b
            foreach(object elem in b)
            {
                newList.Add(elem);
            }

            return newList;
        }

        /// <summary>
        /// Appends all values from dynamic array <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array to change.</param>
        /// <param name="b">Another List, i.e. dynamic array of compatible type to <paramref name="a"/>.</param>
        /// <returns>A truth value telling whether a was changed (i.e. b not empty)</returns>
        public static bool ConcatenateChanged<V>(List<V> a, List<V> b)
        {
            // Append b to a
            a.AddRange(b);

            return b.Count>0;
        }

        /// <summary>
        /// Appends all values from dynamic array <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array to change.</param>
        /// <param name="b">Another List, i.e. dynamic array of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether a was changed (i.e. b not empty)</returns>
        public static bool ConcatenateChanged<V>(List<V> a, List<V> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            // Append b to a
            foreach(V entry in b)
            {
                graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.PutElement, entry, null);
                a.Add(entry);
            }

            return b.Count>0;
        }

        /// <summary>
        /// Appends all values from dynamic array <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A List, i.e. dynamic array to change.</param>
        /// <param name="b">Another List, i.e. dynamic array of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool ConcatenateChanged<V>(List<V> a, List<V> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            // Append b to a
            foreach(V entry in b)
            {
                graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.PutElement, entry, null);
                a.Add(entry);
            }

            return b.Count>0;
        }

        /// <summary>
        /// Creates a new dictionary representing a set,
        /// containing all keys from the given dictionary representing a map <paramref name="map"/>.
        /// </summary>
        /// <param name="map">A dictionary representing a map.</param>
        /// <returns>A new set dictionary containing all keys from <paramref name="map"/>.</returns>
        public static Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> Domain<K, V>(Dictionary<K, V> map)
        {
            Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType> newDict =
                new Dictionary<K, de.unika.ipd.grGen.libGr.SetValueType>();

            // Add all keys of dictionary representing map to new dictionary representing set
            foreach (K key in map.Keys)
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
        public static Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType> Range<K, V>(Dictionary<K, V> map)
        {
            Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType> newDict =
                new Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType>();

            // Add all values of dictionary representing map to new dictionary representing set
            foreach (V value in map.Values)
            {
                newDict[value] = null;
            }

            return newDict;
        }

        /// <summary>
        /// Returns the value from the dictionary at the nth position as defined by the iterator of the dictionary.
        /// </summary>
        /// <param name="dict">A dictionary.</param>
        /// <param name="num">The number of the element to get in the iteration sequence.</param>
        /// <returns>The element at the position to get.</returns>
        public static K Peek<K, V>(Dictionary<K, V> dict, int num)
        {
            Dictionary<K,V>.Enumerator it = dict.GetEnumerator();
            if(num >= 0) it.MoveNext();
            for(int i = 0; i < num; ++i)
            {
                it.MoveNext();
            }

            return it.Current.Key;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> equals set/map <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool Equal<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            if(a.Count!=b.Count) return false;
            if(LessOrEqual(a, b) && LessOrEqual(b, a)) return true;
            else return false;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> equals set/map <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool EqualIDictionary(IDictionary a, IDictionary b)
        {
            if(a.Count != b.Count) return false;
            if(LessOrEqualIDictionary(a, b) && LessOrEqualIDictionary(b, a)) return true;
            else return false;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is not equal to set/map <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool NotEqual<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            if(a.Count!=b.Count) return true;
            if(LessOrEqual(a, b) && LessOrEqual(b, a)) return false;
            else return true;
        }

        public static bool NotEqualIDictionary(IDictionary a, IDictionary b)
        {
            if(a.Count != b.Count) return true;
            if(LessOrEqualIDictionary(a, b) && LessOrEqualIDictionary(b, a)) return false;
            else return true;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a proper superset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool GreaterThan<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            if(GreaterOrEqual(a, b)) return b.Count!=a.Count;
            else return false;
        }

        public static bool GreaterThanIDictionary(IDictionary a, IDictionary b)
        {
            if(GreaterOrEqualIDictionary(a, b)) return b.Count != a.Count;
            else return false;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a superset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool GreaterOrEqual<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            return LessOrEqual(b, a);
        }

        public static bool GreaterOrEqualIDictionary(IDictionary a, IDictionary b)
        {
            return LessOrEqualIDictionary(b, a);
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a proper subset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool LessThan<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            if(LessOrEqual(a, b)) return a.Count!=b.Count;
            else return false;
        }

        public static bool LessThanIDictionary(IDictionary a, IDictionary b)
        {
            if(LessOrEqualIDictionary(a, b)) return a.Count != b.Count;
            else return false;
        }

        /// <summary>
        /// Checks if set/map <paramref name="a"/> is a subset/map of <paramref name="b"/>.
        /// For a map, key and value must be same to be equal.
        /// </summary>
        /// <param name="a">A dictionary.</param>
        /// <param name="b">Another dictionary of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of set/map comparison.</returns>
        public static bool LessOrEqual<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            if(typeof(V)==typeof(de.unika.ipd.grGen.libGr.SetValueType))
            {
                foreach(KeyValuePair<K, V> entry in a)
                {
                    if(!b.ContainsKey(entry.Key)) return false;
                }
            }
            else
            {
                foreach(KeyValuePair<K, V> entry in a)
                {
                    if(!b.ContainsKey(entry.Key)) return false;
                    if(entry.Value!=null ? !entry.Value.Equals(b[entry.Key]) : b[entry.Key]!=null) return false;
                }
            }
            return true;
        }

        public static bool LessOrEqualIDictionary(IDictionary a, IDictionary b)
        {
            Type keyType;
            Type valueType;
            DictionaryListHelper.GetDictionaryTypes(a, out keyType, out valueType);
            if(valueType.Name == "SetValueType")
            {
                foreach(DictionaryEntry entry in a)
                {
                    if(!b.Contains(entry.Key)) return false;
                }
            }
            else
            {
                foreach(DictionaryEntry entry in a)
                {
                    if(!b.Contains(entry.Key)) return false;
                    if(entry.Value != null ? !entry.Value.Equals(b[entry.Key]) : b[entry.Key] != null) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if List <paramref name="a"/> equals List <paramref name="b"/>.
        /// Requires same values at same index for being true.
        /// </summary>
        /// <param name="a">A List.</param>
        /// <param name="b">Another List of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of List comparison.</returns>
        public static bool Equal<V>(List<V> a, List<V> b)
        {
            if(a.Count != b.Count) return false;
            if(LessOrEqual(a, b)) return true;
            else return false;
        }

        public static bool EqualIList(IList a, IList b)
        {
            if(a.Count != b.Count) return false;
            if(LessOrEqualIList(a, b)) return true;
            else return false;
        }

        /// <summary>
        /// Checks if List <paramref name="a"/> is not equal List <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A List.</param>
        /// <param name="b">Another List of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of List comparison.</returns>
        public static bool NotEqual<V>(List<V> a, List<V> b)
        {
            if(a.Count != b.Count) return true;
            if(LessOrEqual(a, b)) return false;
            else return true;
        }

        public static bool NotEqualIList(IList a, IList b)
        {
            if(a.Count != b.Count) return true;
            if(LessOrEqualIList(a, b)) return false;
            else return true;
        }

        /// <summary>
        /// Checks if List <paramref name="a"/> is a proper superlist of <paramref name="b"/>.
        /// Requires a to contain more entries than b and same values at same index for being true.
        /// </summary>
        /// <param name="a">A List.</param>
        /// <param name="b">Another List of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of List comparison.</returns>
        public static bool GreaterThan<V>(List<V> a, List<V> b)
        {
            if(a.Count == b.Count) return false;
            return GreaterOrEqual(a, b);
        }

        public static bool GreaterThanIList(IList a, IList b)
        {
            if(a.Count == b.Count) return false;
            return GreaterOrEqualIList(a, b);
        }

        /// <summary>
        /// Checks if List <paramref name="a"/> is a superlist of <paramref name="b"/>.
        /// Requires a to contain more or same number of entries than b and same values at same index for being true.
        /// </summary>
        /// <param name="a">A List.</param>
        /// <param name="b">Another List of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of List comparison.</returns>
        public static bool GreaterOrEqual<V>(List<V> a, List<V> b)
        {
            return LessOrEqual(b, a);
        }

        public static bool GreaterOrEqualIList(IList a, IList b)
        {
            return LessOrEqualIList(b, a);
        }

        /// <summary>
        /// Checks if List <paramref name="a"/> is a proper sublist of <paramref name="b"/>.
        /// Requires a to contain less entries than b and same values at same index for being true.
        /// </summary>
        /// <param name="a">A List.</param>
        /// <param name="b">Another List of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of List comparison.</returns>
        public static bool LessThan<V>(List<V> a, List<V> b)
        {
            if(a.Count == b.Count) return false;
            return LessOrEqual(a, b);
        }

        public static bool LessThanIList(IList a, IList b)
        {
            if(a.Count == b.Count) return false;
            return LessOrEqualIList(a, b);
        }

        /// <summary>
        /// Checks if List <paramref name="a"/> is a sublist of <paramref name="b"/>.
        /// Requires a to contain less or same number of entries than b and same values at same index for being true.
        /// </summary>
        /// <param name="a">A List.</param>
        /// <param name="b">Another List of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of List comparison.</returns>
        public static bool LessOrEqual<V>(List<V> a, List<V> b)
        {
            if(a.Count > b.Count) return false;
            for(int i=0; i<a.Count; ++i)
            {
                if(!EqualityComparer<V>.Default.Equals(a[i], b[i])) return false;
            }
            return true;
        }

        public static bool LessOrEqualIList(IList a, IList b)
        {
            if(a.Count > b.Count) return false;
            for(int i = 0; i < a.Count; ++i)
            {
                if(!Equals(a[i], b[i])) return false;
            }
            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////

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
            GetDictionaryTypes(setmap, out keyType, out valueType);

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
        /// <param name="type">The type as string, e.g set<int> or map<string,boolean> </param>
        /// <param name="content">The content as string, e.g. { 42, 43 } or { "foo"->true, "bar"->false } </param>
        /// <param name="attrType">The attribute type of the dictionary if available, otherwise null</param>
        /// <param name="graph">The graph with the model and the element names if available, otherwise null</param>
        public static void ToString(IList array, out string type, out string content,
            AttributeType attrType, IGraph graph)
        {
            Type valueType;
            GetListType(array, out valueType);

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
                GetDictionaryTypes(setmap, out keyType, out valueType);

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
                GetDictionaryTypes(setmap, out keyType, out valueType);

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
                GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += ".add(" + ToString(newValue, attrType.ValueType, graph);
                if(keyValue != null) content += ", " + keyValue.ToString() + ")";
                else content += ")";
            }
            else if(changeType == AttributeChangeType.RemoveElement)
            {
                Type valueType;
                GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += ".rem(";
                if(keyValue != null) content += keyValue.ToString() + ")";
                else content += ")";
            }
            else if(changeType == AttributeChangeType.AssignElement)
            {
                Type valueType;
                GetListType(array, out valueType);
                ToString(array, out type, out content, attrType, graph);
                content += "[" + keyValue.ToString() + "] = " + ToString(newValue, attrType.ValueType, graph);
            }
            else // changeType==AttributeChangeType.Assign
            {
                ToString((IList)newValue, out type, out content, attrType, graph);
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
        /// Returns a string representation of the given scalar value
        /// </summary>
        /// <param name="value">The scalar value of which to get the string representation</param>
        /// <param name="type">The type as string, e.g int,string,Foo </param>
        /// <param name="content">The content as string, e.g. 42,"foo",bar } </param>
        /// <param name="attrType">The attribute type of the value</param>
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

                Debug.Assert(value.GetType().Name != "Dictionary`2" && value.GetType().Name != "List`1");
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
                    case "Object": type = "object"; break;
                    case "de.unika.ipd.grGen.libGr.IGraph": type = "graph"; break;
                    default:
                        type = "<INVALID>";
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

                content = ToString(value, attrType, graph);
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

            content = ToString(value, attrType, graph);
        }

        /// <summary>
        /// If the attribute of the given name of the given element is a set or map or array attribute
        /// then return a clone of the given dictionary or list value, otherwise just return the original value;
        /// additionally returns the AttributeType of the attribute of the element.
        /// </summary>
        public static object IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                IGraphElement element, String AttributeName, object value, out AttributeType attrType)
        {
            attrType = element.Type.GetAttributeType(AttributeName);
            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
            {
                Type keyType, valueType;
                DictionaryListHelper.GetDictionaryTypes(element.GetAttribute(AttributeName), out keyType, out valueType);
                return DictionaryListHelper.NewDictionary(keyType, valueType, value); // by-value-semantics -> clone dictionary
            }
            else if(attrType.Kind == AttributeKind.ArrayAttr)
            {
                Type valueType;
                DictionaryListHelper.GetListType(element.GetAttribute(AttributeName), out valueType);
                return DictionaryListHelper.NewList(valueType, value); // by-value-semantics -> clone array
            }
            return value;
        }

        /// <summary>
        /// If the attribute of the given name of the given element is a set or map or array attribute
        /// then return a clone of the given dictionary or list value, otherwise just return the original value
        /// </summary>
        public static object IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                IGraphElement element, String AttributeName, object value)
        {
            AttributeType attrType;
            return IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                element, AttributeName, value, out attrType);
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
                Debug.Assert(value.GetType().Name != "Dictionary`2" && value.GetType().Name != "List`1");
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

            return value!=null ? value.ToString() : "";
        }

        /// <summary>
        /// Returns a string representation of the given value, might be a scalar, a dictionary, or a list
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
