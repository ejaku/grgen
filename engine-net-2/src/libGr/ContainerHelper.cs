/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
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
    public static class ContainerHelper
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
        /// If deque is Deque, the Deque is returned together with its value type
        /// </summary>
        /// <param name="deque">The object which should be a Deque</param>
        /// <param name="valueType">The value type of the Deque</param>
        /// <returns>The casted input Deque, or null if not a Deque</returns>
        public static IDeque GetDequeType(object deque, out Type valueType)
        {
            if(!(deque is IDeque))
            {
                valueType = null;
                return null;
            }
            Type dequeType = deque.GetType();
            GetDequeType(dequeType, out valueType);
            return (IDeque)deque;
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
        /// <param name="valueType">The value type of the List</param>
        public static void GetListType(Type arrayType, out Type valueType)
        {
            Type[] arrayTypeArgs = arrayType.GetGenericArguments();
            valueType = arrayTypeArgs[0];
        }

        /// <summary>
        /// The value type of the Deque is returned
        /// </summary>
        /// <param name="dequeType">The Deque type</param>
        /// <param name="valueType">The value type of the Deque</param>
        public static void GetDequeType(Type dequeType, out Type valueType)
        {
            Type[] dequeTypeArgs = dequeType.GetGenericArguments();
            valueType = dequeTypeArgs[0];
        }

        /// <summary>
        /// Returns type object for type name string, to be used for container class, i.e. Dictionary, List, Deque
        /// </summary>
        /// <param name="typeName">Name of the type we want some type object for</param>
        /// <param name="graph">Graph to be search for enum,node,edge types / enum,node/edge type names</param>
        /// <returns>The type object corresponding to the given string, null if type was not found</returns>
        public static Type GetTypeFromNameForContainer(String typeName, IGraph graph)
        {
            return GetTypeFromNameForContainer(typeName, graph.Model);
        }

        /// <summary>
        /// Returns type object for type name string, to be used for container class, i.e. Dictionary, List, Deque
        /// </summary>
        /// <param name="typeName">Name of the type we want some type object for</param>
        /// <param name="model">Graph model to be search for enum,node,edge types / enum,node/edge type names</param>
        /// <returns>The type object corresponding to the given string, null if type was not found</returns>
        public static Type GetTypeFromNameForContainer(String typeName, IGraphModel model)
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
                if (enumAttrType.PackagePrefixedName == typeName)
                    return enumAttrType.EnumType;
            }

            Assembly assembly = Assembly.GetAssembly(model.GetType());

            // check node and edge types
            foreach (NodeType nodeType in model.NodeModel.Types)
            {
                if (nodeType.PackagePrefixedName == typeName)
                {
                    Type type = Type.GetType(nodeType.NodeInterfaceName); // available in libGr (INode)?
                    if (type != null) return type;
                    type = Type.GetType(nodeType.NodeInterfaceName + "," + assembly.FullName); // no -> search model assembly
                    return type;
                }
            }
            foreach (EdgeType edgeType in model.EdgeModel.Types)
            {
                if (edgeType.PackagePrefixedName == typeName)
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
            Type type = GetTypeFromNameForContainer(typeName, model);
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
        /// Creates a new Deque of the given value type
        /// </summary>
        /// <param name="valueType">The value type of the Deque to be created</param>
        /// <returns>The newly created Deque, null if unsuccessfull</returns>
        public static IDeque NewDeque(Type valueType)
        {
            if(valueType == null) return null;

            Type genDequeType = typeof(Deque<>);
            Type dequeType = genDequeType.MakeGenericType(valueType);
            return (IDeque)Activator.CreateInstance(dequeType);
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
        /// Creates a new Deque of the given value type,
        /// initialized with the content of the old Deque (clones the old Deque)
        /// </summary>
        /// <param name="valueType">The value type of the Deque to be created</param>
        /// <param name="oldDeque">The old Deque to be cloned</param>
        /// <returns>The newly created Deque, containing the content of the old Deque,
        /// null if unsuccessfull</returns>
        public static IDeque NewDeque(Type valueType, object oldDeque)
        {
            if(valueType == null || oldDeque == null) return null;

            Type genDequeType = typeof(Deque<>);
            Type dequeType = genDequeType.MakeGenericType(valueType);
            return (IDeque)Activator.CreateInstance(dequeType, oldDeque);
        }

        /// <summary>
        /// Creates a shallow clone of the given container.
        /// </summary>
        /// <param name="oldContainer">The container to clone.</param>
        /// <returns>A shallow clone of the container</returns>
        public static object Clone(object oldContainer)
        {
            if(oldContainer is IDictionary)
            {
                Type keyType, valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(
                    oldContainer, out keyType, out valueType);
                return NewDictionary(keyType, valueType, oldContainer);
            }
            else if(oldContainer is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(
                    oldContainer, out valueType);
                return NewList(valueType, oldContainer);
            }
            else if(oldContainer is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(
                    oldContainer, out valueType);
                return NewDeque(valueType, oldContainer);
            }
            return null; // no known container type
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
            // Create empty dictionary.
            Dictionary<K, V> newDict = new Dictionary<K, V>();

            // Add all elements of a also contained in b.
            if(a.Count <= b.Count)
            {
                foreach(KeyValuePair<K, V> entry in a)
                    if(b.ContainsKey(entry.Key))
                        newDict.Add(entry.Key, entry.Value);
            }
            else
            {
                V value;
                foreach(KeyValuePair<K, V> entry in b)
                    if(a.TryGetValue(entry.Key, out value))
                        newDict.Add(entry.Key, value);
            }

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
                    graph.ChangedNodeAttribute(owner, attrType);
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
                    graph.ChangedEdgeAttribute(owner, attrType);
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
                    graph.ChangedNodeAttribute(owner, attrType);
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
                    graph.ChangedEdgeAttribute(owner, attrType);
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
                graph.ChangedNodeAttribute(owner, attrType);
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
                graph.ChangedEdgeAttribute(owner, attrType);
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
                graph.ChangedNodeAttribute(owner, attrType);
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
                graph.ChangedEdgeAttribute(owner, attrType);
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
                graph.ChangedNodeAttribute(owner, attrType);
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
                graph.ChangedEdgeAttribute(owner, attrType);
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
                graph.ChangedNodeAttribute(owner, attrType);
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
                graph.ChangedEdgeAttribute(owner, attrType);
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
            return a.IndexOf(entry);
        }

        public static int IndexOf(IList a, object entry)
        {
            return a.IndexOf(entry);
        }

        public static int IndexOf<V>(List<V> a, V entry, int startIndex)
        {
            return a.IndexOf(entry, startIndex);
        }

        public static int IndexOf(IList a, object entry, int startIndex)
        {
            for(int i = startIndex; i < a.Count; ++i)
                if(a[i].Equals(entry))
                    return i;

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

        public static int LastIndexOf(IList a, object entry)
        {
            for(int i = a.Count - 1; i >= 0; --i)
            {
                if(a[i] == entry)
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
        /// Creates a new dictionary representing a set containing all values from the given list.
        /// </summary>
        public static Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType> ArrayAsSet<V>(List<V> a)
        {
            Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType> newDict =
                new Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType>();

            for(int i = 0; i < a.Count; ++i)
            {
                newDict[a[i]] = null;
            }

            return newDict;
        }

        public static string Implode(List<string> a, string filler)
        {
            StringBuilder sb = new StringBuilder();

            bool first = true;
            for(int i = 0; i < a.Count; ++i)
            {
                if(!first)
                    sb.Append(filler);
                else
                    first = false;
                sb.Append(a[i]);
            }

            return sb.ToString();
        }

        public static List<string> Explode(string input, string separator)
        {
            if(separator.Length == 0)
            {
                List<string> result = new List<string>();
                for(int i = 0; i < input.Length; ++i)
                    result.Add(new string(input[i], 1));
                return result;
            }

            return new List<string>(input.Split(new string[] { separator }, StringSplitOptions.None));
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
                graph.ChangedNodeAttribute(owner, attrType);
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
                graph.ChangedEdgeAttribute(owner, attrType);
            }

            return b.Count>0;
        }

        /// <summary>
        /// Returns the first position of entry in the deque a
        /// </summary>
        /// <param name="a">A Deque, i.e. double ended queue.</param>
        /// <param name="entry">The value to search for.</param>
        /// <returns>The first position of entry in the deque a, -1 if entry not in a.</returns>
        public static int IndexOf<V>(Deque<V> a, V entry)
        {
            return a.IndexOf(entry);
        }

        public static int IndexOf(IDeque a, object entry)
        {
            return a.IndexOf(entry);
        }

        public static int IndexOf<V>(Deque<V> a, V entry, int index)
        {
            return a.IndexOf(entry, index);
        }

        public static int IndexOf(IDeque a, object entry, int index)
        {
            return a.IndexOf(entry, index);
        }

        /// <summary>
        /// Returns the first position from the end inwards of entry in the deque a
        /// </summary>
        /// <param name="a">A Deque, i.e. double ended queue.</param>
        /// <param name="entry">The value to search for.</param>
        /// <returns>The first position from the end inwards of entry in the deque a, -1 if entry not in a.</returns>
        public static int LastIndexOf<V>(Deque<V> a, V entry)
        {
            return a.LastIndexOf(entry);
        }

        public static int LastIndexOf(IDeque a, object entry)
        {
            return a.LastIndexOf(entry);
        }

        /// <summary>
        /// Creates a new deque with length values copied from a from index start on.
        /// </summary>
        /// <param name="a">A Deque, i.e. double ended queue.</param>
        /// <param name="start">A start position in the deque.</param>
        /// <param name="length">The number of elements to copy from start on.</param>
        /// <returns>A new Deque, containing the length first values from start on.</returns>
        public static Deque<V> Subdeque<V>(Deque<V> a, int start, int length)
        {
            Deque<V> newDeque = new Deque<V>();

            for(int i = start; i < start + length; ++i)
            {
                newDeque.Add(a[i]);
            }

            return newDeque;
        }

        /// <summary>
        /// Creates a new dictionary representing a set containing all values from the given list.
        /// </summary>
        public static Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType> DequeAsSet<V>(Deque<V> a)
        {
            Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType> newDict =
                new Dictionary<V, de.unika.ipd.grGen.libGr.SetValueType>();

            for(int i = 0; i < a.Count; ++i)
            {
                newDict[a[i]] = null;
            }

            return newDict;
        }

        /// <summary>
        /// Creates a new deque and appends all values first from
        /// <paramref name="a"/> and then from <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>A new Deque containing a concatenation of the parameter deques.</returns>
        public static Deque<V> Concatenate<V>(Deque<V> a, Deque<V> b)
        {
            // create new deque as a copy of a
            Deque<V> newDeque = new Deque<V>(a);

            // then append b
            foreach(V entry in b)
            {
                newDeque.Enqueue(entry);
            }

            return newDeque;
        }

        public static IDeque ConcatenateIDeque(IDeque a, IDeque b)
        {
            // create new deque as a copy of a
            IDeque newDeque = (IDeque)Activator.CreateInstance(a.GetType(), a);

            // then append b
            foreach(object entry in b)
            {
                newDeque.Enqueue(entry);
            }

            return newDeque;
        }

        /// <summary>
        /// Appends all values from deque <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A Deque to change.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>A truth value telling whether a was changed (i.e. b not empty)</returns>
        public static bool ConcatenateChanged<V>(Deque<V> a, Deque<V> b)
        {
            // Append b to a
            foreach(V entry in b)
            {
                a.Enqueue(entry);
            }

            return b.Count > 0;
        }

        /// <summary>
        /// Appends all values from deque <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A Deque to change.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the node containing the attribute which gets changed.</param>
        /// <param name="owner">The node containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether a was changed (i.e. b not empty)</returns>
        public static bool ConcatenateChanged<V>(Deque<V> a, Deque<V> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            // Append b to a
            foreach(V entry in b)
            {
                graph.ChangingNodeAttribute(owner, attrType, AttributeChangeType.PutElement, entry, null);
                a.Enqueue(entry);
                graph.ChangedNodeAttribute(owner, attrType);
            }

            return b.Count > 0;
        }

        /// <summary>
        /// Appends all values from deque <paramref name="b"/> to <paramref name="a"/>.
        /// </summary>
        /// <param name="a">A Deque to change.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <param name="graph">The graph containing the edge containing the attribute which gets changed.</param>
        /// <param name="owner">The edge containing the attribute which gets changed.</param>
        /// <param name="attrType">The attribute type of the attribute which gets changed.</param>
        /// <returns>A truth value telling whether at least one element was changed in a</returns>
        public static bool ConcatenateChanged<V>(Deque<V> a, Deque<V> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            // Append b to a
            foreach(V entry in b)
            {
                graph.ChangingEdgeAttribute(owner, attrType, AttributeChangeType.PutElement, entry, null);
                a.Enqueue(entry);
                graph.ChangedEdgeAttribute(owner, attrType);
            }

            return b.Count > 0;
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
        /// Returns the value from the dictionary or list or deque at the nth position as defined by the iterator of the dictionary or the index of the list or the iterator of the deque.
        /// </summary>
        /// <param name="obj">A dictionary or a list or a deque.</param>
        /// <param name="num">The number of the element to get in the iteration sequence.</param>
        /// <returns>The element at the position to get.</returns>
        public static object Peek(object obj, int num)
        {
            if(obj is IDictionary)
            {
                IDictionary dict = (IDictionary)obj;
                IDictionaryEnumerator it = dict.GetEnumerator();
                if(num >= 0) it.MoveNext();
                for(int i = 0; i < num; ++i)
                {
                    it.MoveNext();
                }
                return it.Key;
            }
            else if(obj is IList)
            {
                IList list = (IList)obj;
                return list[num];
            }
            else
            {
                IDeque deque = (IDeque)obj;
                if(num == 0)
                    return deque.Front;
                IEnumerator it = deque.GetEnumerator();
                if(num >= 0) it.MoveNext();
                for(int i = 0; i < num; ++i)
                {
                    it.MoveNext();
                }
                return it.Current;
            }
        }

        /// <summary>
        /// Returns the value from the deque begin or array end.
        /// </summary>
        /// <param name="obj">A list or a deque.</param>
        /// <returns>The element at the list end or deque begin.</returns>
        public static object Peek(object obj)
        {
            if(obj is IList)
            {
                IList list = (IList)obj;
                return list[list.Count - 1];
            }
            else if(obj is IDeque)
            {
                IDeque deque = (IDeque)obj;
                return deque.Front;
            }
            else
            {
                throw new Exception("peek() can only be used on array or deque (peek(int) works on all containers)");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////

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
            if(typeof(V) == typeof(de.unika.ipd.grGen.libGr.SetValueType))
            {
                foreach(KeyValuePair<K, V> entry in a)
                    if(!b.ContainsKey(entry.Key))
                        return false;
            }
            else
            {
                foreach(KeyValuePair<K, V> entry in a)
                {
                    if(!b.ContainsKey(entry.Key))
                        return false;
                    if(entry.Value != null ? !entry.Value.Equals(b[entry.Key]) : b[entry.Key] != null)
                        return false;
                }
            }

            return true;
        }

        public static bool LessOrEqualIDictionary(IDictionary a, IDictionary b)
        {
            Type keyType;
            Type valueType;
            ContainerHelper.GetDictionaryTypes(a, out keyType, out valueType);
            if(valueType.Name == "SetValueType")
            {
                foreach(DictionaryEntry entry in a)
                    if(!b.Contains(entry.Key))
                        return false;
            }
            else
            {
                foreach(DictionaryEntry entry in a)
                {
                    if(!b.Contains(entry.Key))
                        return false;
                    if(entry.Value != null ? !entry.Value.Equals(b[entry.Key]) : b[entry.Key] != null)
                        return false;
                }
            }

            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////

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
                if(!EqualityComparer<V>.Default.Equals(a[i], b[i]))
                    return false;

            return true;
        }

        public static bool LessOrEqualIList(IList a, IList b)
        {
            if(a.Count > b.Count) return false;

            for(int i = 0; i < a.Count; ++i)
                if(!Equals(a[i], b[i]))
                    return false;
            
            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Checks if Deque <paramref name="a"/> equals Deque <paramref name="b"/>.
        /// Requires same values at same position for being true.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of Deque comparison.</returns>
        public static bool Equal<V>(Deque<V> a, Deque<V> b)
        {
            if(a.Count != b.Count) return false;
            if(LessOrEqual(a, b)) return true;
            else return false;
        }

        public static bool EqualIDeque(IDeque a, IDeque b)
        {
            if(a.Count != b.Count) return false;
            if(LessOrEqualIDeque(a, b)) return true;
            else return false;
        }

        /// <summary>
        /// Checks if Deque <paramref name="a"/> is not equal Deque <paramref name="b"/>.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of Deque comparison.</returns>
        public static bool NotEqual<V>(Deque<V> a, Deque<V> b)
        {
            if(a.Count != b.Count) return true;
            if(LessOrEqual(a, b)) return false;
            else return true;
        }

        public static bool NotEqualIDeque(IDeque a, IDeque b)
        {
            if(a.Count != b.Count) return true;
            if(LessOrEqualIDeque(a, b)) return false;
            else return true;
        }

        /// <summary>
        /// Checks if Deque <paramref name="a"/> is a proper superdeque of <paramref name="b"/>.
        /// Requires a to contain more entries than b and same values at same position for being true.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of Deque comparison.</returns>
        public static bool GreaterThan<V>(Deque<V> a, Deque<V> b)
        {
            if(a.Count == b.Count) return false;
            return GreaterOrEqual(a, b);
        }

        public static bool GreaterThanIDeque(IDeque a, IDeque b)
        {
            if(a.Count == b.Count) return false;
            return GreaterOrEqualIDeque(a, b);
        }

        /// <summary>
        /// Checks if Deque <paramref name="a"/> is a superdeque of <paramref name="b"/>.
        /// Requires a to contain more or same number of entries than b and same values at same position for being true.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of Deque comparison.</returns>
        public static bool GreaterOrEqual<V>(Deque<V> a, Deque<V> b)
        {
            return LessOrEqual(b, a);
        }

        public static bool GreaterOrEqualIDeque(IDeque a, IDeque b)
        {
            return LessOrEqualIDeque(b, a);
        }

        /// <summary>
        /// Checks if Deque <paramref name="a"/> is a proper subseque of <paramref name="b"/>.
        /// Requires a to contain less entries than b and same values at same position for being true.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of Deque comparison.</returns>
        public static bool LessThan<V>(Deque<V> a, Deque<V> b)
        {
            if(a.Count == b.Count) return false;
            return LessOrEqual(a, b);
        }

        public static bool LessThanIDeque(IDeque a, IDeque b)
        {
            if(a.Count == b.Count) return false;
            return LessOrEqualIDeque(a, b);
        }

        /// <summary>
        /// Checks if Deque <paramref name="a"/> is a subdeque of <paramref name="b"/>.
        /// Requires a to contain less or same number of entries than b and same values at same positions for being true.
        /// </summary>
        /// <param name="a">A Deque.</param>
        /// <param name="b">Another Deque of compatible type to <paramref name="a"/>.</param>
        /// <returns>Boolean result of Deque comparison.</returns>
        public static bool LessOrEqual<V>(Deque<V> a, Deque<V> b)
        {
            if(a.Count > b.Count) return false;

            for(int i = 0; i < a.Count; ++i)
                if(!EqualityComparer<V>.Default.Equals(a[i], b[i]))
                    return false;

            return true;
        }

        public static bool LessOrEqualIDeque(IDeque a, IDeque b)
        {
            if(a.Count > b.Count) return false;

            for(int i = 0; i < a.Count; ++i)
                if(!Equals(a[i], b[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// If the attribute of the given name of the given element is a container attribute
        /// then return a clone of the given container value, otherwise just return the original value;
        /// additionally returns the AttributeType of the attribute of the element.
        /// </summary>
        public static object IfAttributeOfElementIsContainerThenCloneContainer(
                IGraphElement element, String AttributeName, object value, out AttributeType attrType)
        {
            attrType = element.Type.GetAttributeType(AttributeName);
            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
            {
                Type keyType, valueType;
                ContainerHelper.GetDictionaryTypes(element.GetAttribute(AttributeName), out keyType, out valueType);
                return ContainerHelper.NewDictionary(keyType, valueType, value); // by-value-semantics -> clone dictionary
            }
            else if(attrType.Kind == AttributeKind.ArrayAttr)
            {
                Type valueType;
                ContainerHelper.GetListType(element.GetAttribute(AttributeName), out valueType);
                return ContainerHelper.NewList(valueType, value); // by-value-semantics -> clone array
            }
            else if(attrType.Kind == AttributeKind.DequeAttr)
            {
                Type valueType;
                ContainerHelper.GetDequeType(element.GetAttribute(AttributeName), out valueType);
                return ContainerHelper.NewDeque(valueType, value); // by-value-semantics -> clone deque
            }
            return value;
        }

        /// <summary>
        /// If the attribute of the given name of the given element is a conatiner attribute
        /// then return a clone of the given container value, otherwise just return the original value
        /// </summary>
        public static object IfAttributeOfElementIsContainerThenCloneContainer(
                IGraphElement element, String AttributeName, object value)
        {
            AttributeType attrType;
            return IfAttributeOfElementIsContainerThenCloneContainer(
                element, AttributeName, value, out attrType);
        }
    }
}
