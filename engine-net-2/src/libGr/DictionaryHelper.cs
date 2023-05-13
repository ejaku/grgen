/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class ContainerHelper
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
            if(!(dict is IDictionary))
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
        /// Creates a new dictionary of the given key type and value type
        /// </summary>
        /// <param name="keyType">The key type of the dictionary to be created</param>
        /// <param name="valueType">The value type of the dictionary to be created</param>
        /// <returns>The newly created dictionary, null if unsuccessfull</returns>
        public static IDictionary NewDictionary(Type keyType, Type valueType)
        {
            if(keyType == null || valueType == null)
                throw new NullReferenceException();

            Type genDictType = typeof(Dictionary<,>);
            Type dictType = genDictType.MakeGenericType(keyType, valueType);
            return (IDictionary)Activator.CreateInstance(dictType);
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
            if(keyType == null || valueType == null || oldDictionary == null)
                throw new NullReferenceException();

            Type genDictType = typeof(Dictionary<,>);
            Type dictType = genDictType.MakeGenericType(keyType, valueType);
            return (IDictionary)Activator.CreateInstance(dictType, oldDictionary);
        }

        /// <summary>
        /// Creates a new dictionary and fills in all key/value pairs from
        /// <paramref name="a"/> and <paramref name="b"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// (this way the common case <![CDATA[a = a | map<int, int> { 7 -> 13 };]]> would update an existing entry
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
            {
                newDict[entry.Key] = entry.Value;
            }

            return newDict;
        }

        public static IDictionary Union(IDictionary a, IDictionary b)
        {
            Type keyType;
            Type valueType;
            IDictionary dict = ContainerHelper.GetDictionaryTypes(a, out keyType, out valueType);

            // Fill new dictionary with all elements from a.
            IDictionary newDict = NewDictionary(keyType, valueType, a);

            // Add all elements from b, potentially overwriting those of a.
            foreach(DictionaryEntry entry in b)
            {
                newDict[entry.Key] = entry.Value;
            }

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
                {
                    if(b.ContainsKey(entry.Key))
                        newDict.Add(entry.Key, entry.Value);
                }
            }
            else
            {
                V value;
                foreach(KeyValuePair<K, V> entry in b)
                {
                    if(a.TryGetValue(entry.Key, out value))
                        newDict.Add(entry.Key, value);
                }
            }

            return newDict;
        }

        public static IDictionary Intersect(IDictionary a, IDictionary b)
        {
            Type keyType;
            Type valueType;
            IDictionary dict = ContainerHelper.GetDictionaryTypes(a, out keyType, out valueType);

            // Create empty dictionary.
            IDictionary newDict = NewDictionary(keyType, valueType);

            // Add all elements of a also contained in b.
            if(a.Count <= b.Count)
            {
                foreach(DictionaryEntry entry in a)
                {
                    if(b.Contains(entry.Key))
                        newDict.Add(entry.Key, entry.Value);
                }
            }
            else
            {
                foreach(DictionaryEntry entry in b)
                {
                    if(a.Contains(entry.Key))
                        newDict.Add(entry.Key, a[entry.Key]);
                }
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
            {
                newDict.Remove(entry.Key);
            }

            return newDict;
        }

        public static IDictionary Except(IDictionary a, IDictionary b)
        {
            Type keyType;
            Type valueType;
            IDictionary dict = ContainerHelper.GetDictionaryTypes(a, out keyType, out valueType);

            // Fill new dictionary with all elements from a.
            IDictionary newDict = NewDictionary(keyType, valueType, a);

            // Remove all elements contained in b.
            foreach(DictionaryEntry entry in b)
            {
                newDict.Remove(entry.Key);
            }

            return newDict;
        }

        /// <summary>
        /// Adds all key/value pairs from set/map <paramref name="b"/> to <paramref name="a"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// (this way the common case <![CDATA[a = a | map<int, int> { 7 -> 13 };]]> would update an existing entry
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
            {
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);
            }

            // Then remove them
            foreach(K key in toBeRemoved)
            {
                a.Remove(key);
            }

            return toBeRemoved.Count > 0;
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
            {
                changed |= a.Remove(entry.Key);
            }

            return changed;
        }

        /// <summary>
        /// Adds all key/value pairs from map <paramref name="b"/> to <paramref name="a"/>.
        /// If both dictionaries contain one key, the value from <paramref name="b"/> takes precedence
        /// (this way the common case <![CDATA[a = a | map<int, int> { 7 -> 13 };]]> would update an existing entry
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
        /// (this way the common case <![CDATA[a = a | map<int, int> { 7 -> 13 };]]> would update an existing entry
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
        public static bool UnionChanged<K>(Dictionary<K, SetValueType> a,
            Dictionary<K, SetValueType> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            bool changed = false;

            // Add all elements from b not contained in a (different values count as not contained, overwriting old value).
            foreach(KeyValuePair<K, SetValueType> entry in b)
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
        public static bool UnionChanged<K>(Dictionary<K, SetValueType> a,
            Dictionary<K, SetValueType> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            bool changed = false;

            // Add all elements from b not contained in a (different values count as not contained, overwriting old value).
            foreach(KeyValuePair<K, SetValueType> entry in b)
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
            {
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);
            }

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
            {
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);
            }

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
        public static bool IntersectChanged<K>(Dictionary<K, SetValueType> a,
            Dictionary<K, SetValueType> b,
            IGraph graph, INode owner, AttributeType attrType)
        {
            // First determine all elements from a not contained in b
            List<K> toBeRemoved = new List<K>(a.Count);
            foreach(KeyValuePair<K, SetValueType> entry in a)
            {
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);
            }

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
        public static bool IntersectChanged<K>(Dictionary<K, SetValueType> a,
            Dictionary<K, SetValueType> b,
            IGraph graph, IEdge owner, AttributeType attrType)
        {
            // First determine all elements from a not contained in b
            List<K> toBeRemoved = new List<K>(a.Count);
            foreach(KeyValuePair<K, SetValueType> entry in a)
            {
                if(!b.ContainsKey(entry.Key))
                    toBeRemoved.Add(entry.Key);
            }

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
        public static bool ExceptChanged<K, W>(Dictionary<K, SetValueType> a,
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
        public static bool ExceptChanged<K, W>(Dictionary<K, SetValueType> a,
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
        /// Returns the value from the dictionary at the nth position as defined by the iterator of the dictionary.
        /// </summary>
        /// <param name="dict">A dictionary.</param>
        /// <param name="num">The number of the element to get in the iteration sequence.</param>
        /// <returns>The element at the position to get.</returns>
        public static K Peek<K, V>(Dictionary<K, V> dict, int num)
        {
            Dictionary<K, V>.Enumerator it = dict.GetEnumerator();
            if(num >= 0)
                it.MoveNext();
            for(int i = 0; i < num; ++i)
            {
                it.MoveNext();
            }

            return it.Current.Key;
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
            if(a == null || b == null)
            {
                if(a == null && b == null)
                    return true;
                else
                    return false;
            }
            if(a.Count != b.Count)
                return false;
            if(LessOrEqual(a, b) && LessOrEqual(b, a))
                return true;
            else
                return false;
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
            if(a == null || b == null)
            {
                if(a == null && b == null)
                    return true;
                else
                    return false;
            }
            if(a.Count != b.Count)
                return false;
            if(LessOrEqualIDictionary(a, b) && LessOrEqualIDictionary(b, a))
                return true;
            else
                return false;
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
            if(a == null || b == null)
            {
                if(a == null && b == null)
                    return false;
                else
                    return true;
            }
            if(a.Count != b.Count)
                return true;
            if(LessOrEqual(a, b) && LessOrEqual(b, a))
                return false;
            else
                return true;
        }

        public static bool NotEqualIDictionary(IDictionary a, IDictionary b)
        {
            if(a == null || b == null)
            {
                if(a == null && b == null)
                    return false;
                else
                    return true;
            }
            if(a.Count != b.Count)
                return true;
            if(LessOrEqualIDictionary(a, b) && LessOrEqualIDictionary(b, a))
                return false;
            else
                return true;
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
            if(GreaterOrEqual(a, b))
                return b.Count != a.Count;
            else
                return false;
        }

        public static bool GreaterThanIDictionary(IDictionary a, IDictionary b)
        {
            if(GreaterOrEqualIDictionary(a, b))
                return b.Count != a.Count;
            else
                return false;
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
            if(LessOrEqual(a, b))
                return a.Count != b.Count;
            else
                return false;
        }

        public static bool LessThanIDictionary(IDictionary a, IDictionary b)
        {
            if(LessOrEqualIDictionary(a, b))
                return a.Count != b.Count;
            else
                return false;
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
                {
                    if(!b.ContainsKey(entry.Key))
                        return false;
                }
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
                {
                    if(!b.Contains(entry.Key))
                        return false;
                }
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

        public static bool DeeplyEqual(IDictionary this_, IDictionary that, IDictionary<object, object> visitedObjects)
        {
            if(this_.Count != that.Count)
                return false;
            if(this_.Count == 0)
                return true;
            if(TypesHelper.DotNetTypeToXgrsType(this_.GetType()) != TypesHelper.DotNetTypeToXgrsType(that.GetType()))
                return false;

            if(TypesHelper.DotNetTypeToXgrsType(this_.GetType()).StartsWith("set<"))
            {
                IDictionaryEnumerator dictEnum = this_.GetEnumerator();
                dictEnum.MoveNext();
                if(((DictionaryEntry)dictEnum.Current).Key is IAttributeBearer)
                {
                    IDictionary<IAttributeBearer, object> matchedObjectsFromThis = new Dictionary<IAttributeBearer, object>();
                    IDictionary<IAttributeBearer, object> matchedObjectsFromThat = new Dictionary<IAttributeBearer, object>();
                    return DeeplyEqualSet(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat);
                }
                else
                {
                    IDictionary<object, object> matchedObjectsFromThis = new Dictionary<object, object>();
                    IDictionary<object, object> matchedObjectsFromThat = new Dictionary<object, object>();
                    return DeeplyEqualSet(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat);
                }
            }
            else
            {
                IDictionaryEnumerator dictEnum = this_.GetEnumerator();
                dictEnum.MoveNext();
                if(((DictionaryEntry)dictEnum.Current).Key is IAttributeBearer)
                {
                    IDictionary<IAttributeBearer, object> matchedObjectsFromThis = new Dictionary<IAttributeBearer, object>();
                    IDictionary<IAttributeBearer, object> matchedObjectsFromThat = new Dictionary<IAttributeBearer, object>();
                    return DeeplyEqualMap(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat);
                }
                else
                {
                    IDictionary<object, object> matchedObjectsFromThis = new Dictionary<object, object>();
                    IDictionary<object, object> matchedObjectsFromThat = new Dictionary<object, object>();
                    return DeeplyEqualMap(this_, that, visitedObjects, matchedObjectsFromThis, matchedObjectsFromThat);
                }
            }
        }

        public static IDictionary Copy(IDictionary dictionary, IGraph graph, IDictionary<object, object> oldToNewObjects)
        {
            Type keyType;
            Type valueType;
            Type dictType = dictionary.GetType();
            GetDictionaryTypes(dictType, out keyType, out valueType);
            IDictionary copy = NewDictionary(keyType, valueType);

            foreach(DictionaryEntry element in dictionary)
            {
                object key = element.Key;
                object value = element.Value;

                if(key is IObject)
                {
                    IObject elem = (IObject)key;
                    key = elem.Copy(graph, oldToNewObjects);
                }
                else if(key is ITransientObject)
                {
                    ITransientObject elem = (ITransientObject)key;
                    key = elem.Copy(graph, oldToNewObjects);
                }

                if(value is IObject)
                {
                    IObject elem = (IObject)value;
                    value = elem.Copy(graph, oldToNewObjects);
                }
                else if(value is ITransientObject)
                {
                    ITransientObject elem = (ITransientObject)value;
                    value = elem.Copy(graph, oldToNewObjects);
                }

                copy.Add(key, value);
            }

            return copy;
        }

        public static Dictionary<K, V> Copy<K, V>(Dictionary<K, V> dictionary, IGraph graph, IDictionary<object, object> oldToNewObjects)
        {
            Dictionary<K, V> copy = new Dictionary<K, V>();

            foreach(KeyValuePair<K, V> element in dictionary)
            {
                K key = element.Key;
                V value = element.Value;

                if(key is IObject)
                {
                    IObject elem = (IObject)key;
                    key = (K)elem.Copy(graph, oldToNewObjects);
                }
                else if(key is ITransientObject)
                {
                    ITransientObject elem = (ITransientObject)key;
                    key = (K)elem.Copy(graph, oldToNewObjects);
                }

                if(value is IObject)
                {
                    IObject elem = (IObject)value;
                    value = (V)elem.Copy(graph, oldToNewObjects);
                }
                else if(value is ITransientObject)
                {
                    ITransientObject elem = (ITransientObject)value;
                    value = (V)elem.Copy(graph, oldToNewObjects);
                }

                copy.Add(key, value);
            }

            return copy;
        }

        public static IDictionary MappingClone(IDictionary dictionary, IDictionary<IGraphElement, IGraphElement> oldToNewElements)
        {
            Type keyType;
            Type valueType;
            Type dictType = dictionary.GetType();
            GetDictionaryTypes(dictType, out keyType, out valueType);
            IDictionary copy = NewDictionary(keyType, valueType);

            foreach(DictionaryEntry element in dictionary)
            {
                object key = element.Key;
                object value = element.Value;

                if(key is IGraphElement)
                {
                    IGraphElement elem = (IGraphElement)key;
                    key = oldToNewElements[elem];
                }

                if(value is IObject)
                {
                    IGraphElement elem = (IGraphElement)value;
                    value = oldToNewElements[elem];
                }

                copy.Add(key, value);
            }

            return copy;
        }

        public static Dictionary<K, V> MappingClone<K, V>(Dictionary<K, V> dictionary, IDictionary<IGraphElement, IGraphElement> oldToNewElements)
        {
            Dictionary<K, V> copy = new Dictionary<K, V>();

            foreach(KeyValuePair<K, V> element in dictionary)
            {
                K key = element.Key;
                V value = element.Value;

                if(key is IGraphElement)
                {
                    IGraphElement elem = (IGraphElement)key;
                    key = (K)oldToNewElements[elem];
                }

                if(value is IGraphElement)
                {
                    IGraphElement elem = (IGraphElement)value;
                    value = (V)oldToNewElements[elem];
                }

                copy.Add(key, value);
            }

            return copy;
        }
    }
}
